using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // open save read write files
using Editor_Base_Class;

namespace Gen2_Moveset_Editor
{
    public partial class MovesetEditor : Editor_Base_Class.Gen2Editor
    {
        public MovesetEditor()
        {
            InitializeComponent();

            int[] readOnly = {TM_CODE_I, PKMN_NAME_I, MOVE_NAME_I};
            int[] readWrite = {TM_SET_I, MOVESET_PTR_I};
            InitOffsets(readOnly, readWrite);
        }

        protected override void EnableDataEntry()
        {
            spinPkmnID_0.Maximum = offset[NUM_OF_PKMN_I];
            spinPkmnID_1.Maximum = offset[NUM_OF_PKMN_I];
            spinPkmnID_2.Maximum = offset[NUM_OF_PKMN_I];

            spinPkmnID_1.Enabled = true;
            tBoxMoveset0.Enabled = true;
            tBoxMoveset1.Enabled = true;
            tBoxMoveset2.Enabled = true;
            checkConsecMode.Enabled = true;
            buttonCopyTMsA.Enabled = true;
            buttonEditTMs.Enabled = true;
            buttonCopyTMsB.Enabled = true;
            buttonAnalyze.Enabled = true;
        }

        protected override void EnableWrite()
        {
            int bytesFree = movesets.BytesFreeAt((int)spinPkmnID_1.Value);

            tboxFreeBytes.Text = bytesFree + " bytes free";

            saveROM_TSMI.Enabled = bytesFree >= 0;
        }

        protected override void UpdateEditor()
        {
            UpdateColumn(0);
            UpdateColumn(1);
            UpdateColumn(2);
        }

        protected override void ImportData(List<string> dataStrings)
        {
            foreach (int pkmn_i in movesets.Range())
            {
                int stringIndex = 4 * (pkmn_i - movesets.start_i);

                // get line and count
                int numOfLearnData = 0;
                string[] firstLine = dataStrings[stringIndex].Split(' ');
                if (firstLine.Length == 2)
                {
                    movesets.SetRelativePtr(pkmn_i, Convert.ToInt32(firstLine[0]));
                    numOfLearnData = Convert.ToInt32(firstLine[1]);
                }

                string[] lDStrings = dataStrings[stringIndex + 1].Split(' ');
                movesets.data[pkmn_i].learnList.Clear();

                int trueIndex = 0;
                for (int lD_i = 0; lD_i < numOfLearnData; lD_i++)
                {
                    var lD = new LearnData
                    {
                        level = Convert.ToByte(lDStrings[trueIndex++]),
                        move = Convert.ToByte(lDStrings[trueIndex++])
                    };

                    movesets.data[pkmn_i].learnList.Add(lD);
                }

                string[] TMStrings = dataStrings[stringIndex + 2].Split(' ');
                if (TMStrings.Length == 9)
                { // extra "" string
                    byte[] TMbytes = new byte[8];
                    for (int byte_i = 0; byte_i < 8; byte_i++)
                    {
                        TMbytes[byte_i] = Convert.ToByte(TMStrings[byte_i]);
                    }

                    bool[] TMsetBools = ROM_FileStream.TMBoolsFromBytes(TMbytes);
                    for (int bool_j = 0; bool_j < 64; bool_j++)
                    {
                        TMSets[pkmn_i, bool_j] = TMsetBools[bool_j];
                    }
                }
            }
            movesets.MakeContiguous();
        }

        // evodata not saved, considered immutable for this tool (load and maintain from ROM)
        protected override void ExportData(System.IO.StreamWriter file)
        {
            foreach (int pkmn_i in movesets.Range())
                {
                    file.WriteLine(movesets.RelativePtr(pkmn_i) + " "
                        + movesets.data[pkmn_i].learnList.Count);

                    string s = "";
                    foreach (LearnData lD in movesets.data[pkmn_i].learnList)
                    {
                        s += lD.level.ToString() + " ";
                        s += lD.move.ToString() + " ";
                    }
                    file.WriteLine(s);

                    bool[] TMbools = new bool[64];
                    for (int bool_i = 0; bool_i < 64; bool_i++)
                    {
                        TMbools[bool_i] = TMSets[pkmn_i, bool_i];
                    }
                    byte[] TMbytes = ROM_FileStream.TMBytesFromBools(TMbools);

                    s = "";
                    foreach (byte b in TMbytes) s += b.ToString() + " ";
                    file.WriteLine(s);

                    file.WriteLine("");
                }
        }

        protected override void ManagePointers()
        {
            new PointerManager<EvoAndLearnset>(movesets).Show();
        }



        private const char SPLITING_CHAR = ':';
        private bool pauseParsing = false;
        private void UpdateColumn(int col_i)
        {
            // update pkmn name and number at header of column
            // select objects in column
            byte pkmn_i = 1;
            TextBox tboxMovesetI = new TextBox();
            TextBox textEvoCond_I = new TextBox();
            if (col_i == 0)
            {
                pkmn_i = (byte)spinPkmnID_0.Value;
                tBoxPkmn_0.Text = pkmnNames[pkmn_i];
                tboxMovesetI = tBoxMoveset0;
                textEvoCond_I = textEvoCond_0;
            }
            else if (col_i == 1)
            {
                pkmn_i = (byte)spinPkmnID_1.Value;
                tBoxPkmn_1.Text = pkmnNames[pkmn_i];
                tboxMovesetI = tBoxMoveset1;
                textEvoCond_I = textEvoCond_1;
            }
            else
            {
                pkmn_i = (byte)spinPkmnID_2.Value;
                tBoxPkmn_2.Text = pkmnNames[pkmn_i];
                tboxMovesetI = tBoxMoveset2;
                textEvoCond_I = textEvoCond_2;
            }
            List<LearnData> sLlD = movesets.data[pkmn_i].learnList;

            pauseParsing = true; // avoid triggering updateMovesetTbox
            tboxMovesetI.Text = "";
            // update level, move name, and move IDs below header 
            // don't want a final blank newline, every line must conform to specification
            tboxMovesetI.AppendText(
                sLlD[0].level.ToString("D3") + SPLITING_CHAR +
                moveNames.data[sLlD[0].move]);

            for (int move_i = 1; move_i < movesets.data[pkmn_i].learnList.Count; move_i++)
            {
                tboxMovesetI.AppendText(
                    Environment.NewLine + sLlD[move_i].level.ToString("D3") +
                    SPLITING_CHAR + moveNames.data[sLlD[move_i].move]);
            }
            pauseParsing = false;

            textEvoCond_I.Text = "";
            foreach (EvoData ed in movesets.data[pkmn_i].evoList)
            {
                textEvoCond_I.Text += ed.param + Environment.NewLine;
            }

            EnableWrite();
        }

        private void CheckConsecMode_CheckedChanged(object sender, EventArgs e)
        {
            spinPkmnID_0.Enabled = !checkConsecMode.Checked;
            spinPkmnID_2.Enabled = !checkConsecMode.Checked;

            if (checkConsecMode.Checked)
            {
                decimal v = spinPkmnID_1.Value;
                if (v - 1 > 0)
                {
                    spinPkmnID_0.Value = v - 1;
                    UpdateColumn(0);
                }
                if (v + 1 <= 251)
                {
                    spinPkmnID_2.Value = v + 1;
                    UpdateColumn(2);
                }
            }
        }

        private void ButtonOpenTMs_Click(object sender, EventArgs e)
        {
            new FormTMdisplay(this, (int)spinPkmnID_1.Value).Show();
        }

        private void ButtonCopyTMsA_Click(object sender, EventArgs e)
        {
            for (int bool_i = 0; bool_i < 64; bool_i++)
            {
                TMSets[(int)spinPkmnID_0.Value, bool_i] = TMSets[(int)spinPkmnID_1.Value, bool_i];
            }
        }
        private void ButtonCopyTMsB_Click(object sender, EventArgs e)
        {
            for (int bool_i = 0; bool_i < 64; bool_i++)
            {
                TMSets[(int)spinPkmnID_2.Value, bool_i] = TMSets[(int)spinPkmnID_1.Value, bool_i];
            }
        }
        private void SpinPkmnID_0_ValueChanged(object sender, EventArgs e)
        {
            UpdateColumn(0);
        }
        private void SpinPkmnID_1_ValueChanged(object sender, EventArgs e)
        {
            UpdateColumn(1);

            if (checkConsecMode.Checked)
            {
                decimal v = spinPkmnID_1.Value;
                if (v - 1 > 0) spinPkmnID_0.Value = v - 1;
                if (v + 1 <= offset[NUM_OF_PKMN_I]) spinPkmnID_2.Value = v + 1;

                UpdateColumn(0);
                UpdateColumn(2);
            }
        }
        private void SpinPkmnID_2_ValueChanged(object sender, EventArgs e)
        {
            UpdateColumn(2);
        }
        private void UpdateMovesetTbox(byte pkmn_i, TextBox tb)
        {
            if (tb.Focused && !pauseParsing)
            { //attempt to parse
                int length = tb.Lines.Length;
                byte[] movesetLevels = new byte[length];
                byte[] movesetMoves = new byte[length];

                for (int move_i = 0; move_i < length; move_i++)
                {
                    string[] movesetStrs = tb.Lines[move_i].Split(SPLITING_CHAR);

                    if (movesetStrs.Length == 2)
                    {
                        byte i = 0;
                        if (byte.TryParse(movesetStrs[0], out i)) movesetLevels[move_i] = i;
                        else
                        {
                            BadParse(tb);
                            return;
                        }

                        bool nameParsed = false;
                        foreach (int moveName_i in moveNames.Range())
                        {
                            if (moveNames.data[moveName_i] == movesetStrs[1])
                            {
                                movesetMoves[move_i] = (byte)moveName_i;
                                nameParsed = true;
                            }
                        }
                        if (!nameParsed)
                        {
                            BadParse(tb);
                            return;
                        }
                    }
                    else
                    {
                        BadParse(tb);
                        return;
                    }
                }

                //if parse failed/succeded, indicate with color
                //if success, update movesets
                tb.BackColor = System.Drawing.SystemColors.Window;

                movesets.data[pkmn_i].learnList.Clear();
                for (int move_i = 0; move_i < length; move_i++)
                {
                    var lD = new LearnData
                    {
                        level = movesetLevels[move_i],
                        move = movesetMoves[move_i]
                    };

                    movesets.data[pkmn_i].learnList.Add(lD);
                }

                movesets.UpdatePtrs(pkmn_i);

                EnableWrite();
            }
        }

        private void TBox0_TextChanged(object sender, EventArgs e)
        {
            UpdateMovesetTbox((byte)spinPkmnID_0.Value, tBoxMoveset0);
        }
        private void TBox1_TextChanged(object sender, EventArgs e)
        {
            UpdateMovesetTbox((byte)spinPkmnID_1.Value, tBoxMoveset1);
        }
        private void TBox2_TextChanged(object sender, EventArgs e)
        {
            UpdateMovesetTbox((byte)spinPkmnID_2.Value, tBoxMoveset2);
        }

        private void ButtonAnalyze_Click(object sender, EventArgs e)
        {
            //iterate through each pkmn and increment usage for each move it learns
            int[] usage = new int[moveNames.end_i + 1];
            foreach (int species_i in movesets.Range())
            {
                foreach (int moveID_j in moveNames.Range())
                {
                    bool canLearn = false;

                    for (int TM_i = 0; TM_i < TMCodes.Length; TM_i++)
                    {
                        if ((TMCodes[TM_i] == moveID_j) && TMSets[species_i, TM_i])
                        {
                            canLearn = true;
                        }
                    }
                    for (int HM_j = 0; HM_j < HMCodes.Length; HM_j++)
                    {
                        if ((HMCodes[HM_j] == moveID_j) && TMSets[species_i, HM_j + 50])
                        {
                            canLearn = true;
                        }
                    }

                    // scan current movelist
                    foreach (LearnData lD in movesets.data[species_i].learnList)
                    {
                        if (lD.move == moveID_j) canLearn = true;
                    }

                    if (canLearn) usage[moveID_j]++;
                }
            }

            var L_ss = new List<SortingString>();
            for (int moveID_i = 1; moveID_i < usage.Length; moveID_i++)
            {
                var ss = new SortingString
                {
                    sortValue = usage[moveID_i],
                    me = usage[moveID_i].ToString("D3") + " - " + moveNames.data[moveID_i]
                };
                L_ss.Add(ss);
            }

            new FormAnalysis(L_ss).Show();
        }
    }

    public class FormTMdisplay : Form
    {
        private CheckBox[,] TMs;
        private CheckBox[] HMs;
        int NUM_COLS = 5;
        int NUM_ROWS = 10;
        int pkmn_i;
        MovesetEditor creator;

        public FormTMdisplay(MovesetEditor c, int p)
        {
            pkmn_i = p;
            creator = c;
            Text = "TM/HMs";
            int COL_WIDTH = 110;
            int ROW_HEIGHT = 30;
            TMs = new CheckBox[NUM_COLS, NUM_ROWS];
            HMs = new CheckBox[7];

            for (int col_i = 0; col_i < NUM_COLS; col_i++)
            {
                int xOffset = 15 + COL_WIDTH * col_i;
                for (int row_i = 0; row_i < NUM_ROWS; row_i++)
                {
                    int number = (NUM_COLS * row_i) + col_i;

                    TMs[col_i, row_i] = new CheckBox
                    {
                        Location = new System.Drawing.Point(xOffset, 10 + ROW_HEIGHT * row_i),
                        Name = "TM" + number,
                        Text = creator.moveNames.data[creator.TMCodes[number]],
                        Size = new System.Drawing.Size(COL_WIDTH, ROW_HEIGHT),
                        Checked = creator.TMSets[pkmn_i, number]
                    };
                    TMs[col_i, row_i].CheckedChanged +=
                            new System.EventHandler(TMs_CheckedChanged);
                    Controls.Add(TMs[col_i, row_i]);
                }
            }

            for (int HM_i = 0; HM_i < 7; HM_i++)
            {
                int col_i = HM_i % 5;

                HMs[HM_i] = new CheckBox
                {
                    Location = new System.Drawing.Point
                    (15 + COL_WIDTH * col_i, 20 + ROW_HEIGHT * (NUM_ROWS + (HM_i < 5 ? 0 : 1))),
                    Name = "HM" + HM_i,
                    Text = creator.moveNames.data[creator.HMCodes[HM_i]],
                    Size = new System.Drawing.Size(COL_WIDTH, ROW_HEIGHT),
                    Checked = creator.TMSets[pkmn_i, 50 + HM_i]
                };
                HMs[HM_i].CheckedChanged +=
                        new System.EventHandler(TMs_CheckedChanged);
                Controls.Add(HMs[HM_i]);
            }

            ClientSize = new System.Drawing.Size(600, 400);
            Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            Name = "formTMdisplay";
            ResumeLayout(false);
            PerformLayout();
        }

        // checks HMs as well
        private void TMs_CheckedChanged(object sender, EventArgs e)
        {
            for (int col_i = 0; col_i < NUM_COLS; col_i++)
            {
                for (int row_j = 0; row_j < NUM_ROWS; row_j++)
                {
                    if (TMs[col_i, row_j].Focused) creator.TMSets[pkmn_i, (NUM_COLS * row_j) + col_i] = TMs[col_i, row_j].Checked;
                }
            }

            for (int HM_i = 0; HM_i < 7; HM_i++)
            {
                if (HMs[HM_i].Focused) creator.TMSets[pkmn_i, 50 + HM_i] = HMs[HM_i].Checked;
            }
        }
    }
}
