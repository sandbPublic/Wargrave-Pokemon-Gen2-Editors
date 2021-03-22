using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Editor_Base_Class;
using System.Globalization;

// see pokecrystal/battle/anims.asm
// todo more than just move animations?
// how to handle names for non move anims?
// just with hex?
namespace Gen2_Move_Animation_Editor
{
    public partial class MoveAnimationEditor : Editor_Base_Class.Gen2Editor
    {
        public MoveAnimationEditor()
        {
            InitializeComponent();

            int[] oTL = { MOVE_NAME_I, ANIM_PTR_I };
            int[] oTS = { ANIM_PTR_I };

            InitOffsets(oTL, oTS);
        }

        protected override void EnableDataEntry()
        {
            spinAnimID.Maximum = offset[NUM_OF_ANIMS_I] - 1;

            spinAnimID.Enabled = true;
            rTxtBytes.Enabled = true;

            importData_TSMI.Enabled = false;
            exportData_TSMI.Enabled = false;
        }

        protected override void EnableWrite()
        {
            int bytesFree = animations.BytesFreeAt((int)spinAnimID.Value);

            txtBytesFree.Text = bytesFree + " bytes free";

            saveROM_TSMI.Enabled = bytesFree >= 0;
        }

        protected override void UpdateEditor()
        {
            rTxtBytes.BackColor = System.Drawing.SystemColors.Window;

            if (moveNames.start_i <= sAnim() && sAnim() <= moveNames.end_i) txtMoveName.Text = moveNames.data[sAnim()];
            else txtMoveName.Text = "0x" + sAnim().ToString("X3");

            //populate text boxs
            rTxtBytes.Text = "";
            for (int aI_i = 0; aI_i < sLAI().Count; aI_i++)
            {
                rTxtBytes.Text += sLAI()[aI_i].ByteString()  + (aI_i != sLAI().Count - 1 ? Environment.NewLine : "");
            }
            UpdateCode();

            EnableWrite();
        }

        //protected override void ImportData(List<string> dataStrings) { }
        //protected override void ExportData() { }
        protected override void ManagePointers()
        {
            new PointerManager<AnimationCode>(animations).Show();
        }



        private int sAnim()
        {
            return (int)spinAnimID.Value;
        }

        private List<AnimeInstr> sLAI()
        {
            return animations.data[sAnim()].me;
        }

        private void SpinAnimID_ValueChanged(object sender, EventArgs e)
        {
            UpdateEditor();
        }

        private void UpdateCode()
        {
            rTxtCode.Text = "";
            UpdateCode(animations.data[sAnim()]);
        }

        private void UpdateCode(AnimationCode aC)
        {
            long offset = aC.startAddr % 0x10000;

            foreach (AnimeInstr aI in aC.me)
            {
                rTxtCode.Text += offset.ToString("X") + " | " + aI.CodeString()
                    + Environment.NewLine;
                offset += aI.BytesUsed();
            }

            foreach (AnimationCode jump in aC.jumps)
            {
                rTxtCode.Text += Environment.NewLine;
                UpdateCode(jump);
            }
        }

        //attempt to create an AnimationCode
        private void RTxtBytes_TextChanged(object sender, EventArgs e)
        {
            if (rTxtBytes.Focused)
            {
                int length = rTxtBytes.Lines.Length;
                var testCode = new List<AnimeInstr>();

                // try parse
                for (int line_i = 0; line_i < length; line_i++)
                {
                    var aI = new AnimeInstr();
                    string[] animeStrs = rTxtBytes.Lines[line_i].Split(' ');

                    byte i = 0;
                    if (byte.TryParse(animeStrs[0],
                        NumberStyles.HexNumber,
                        null, out i))
                    {

                        aI.opCode = i;
                        int expectedLength = 1 + aI.ExpectedParameters() +
                            (aI.IsPtrExpected() ? 2 : 0);

                        if (animeStrs.Length == expectedLength)
                        {
                            // load params
                            for (int param_i = 1; param_i <= aI.ExpectedParameters(); param_i++)
                            {
                                byte p = 0;
                                if (byte.TryParse(animeStrs[param_i],
                                    NumberStyles.HexNumber,
                                    null, out p))
                                {
                                    aI.parameters.Add(p);
                                }
                                else
                                {
                                    BadParse(); return;
                                }
                            }

                            // load ptr from last two bytes
                            if (aI.IsPtrExpected())
                            {
                                byte x = 0;
                                if (!byte.TryParse(animeStrs[expectedLength - 2],
                                    NumberStyles.HexNumber,
                                    null, out x))
                                {
                                    BadParse(); return;
                                }
                                byte y = 0;
                                if (!byte.TryParse(animeStrs[expectedLength - 1],
                                    NumberStyles.HexNumber,
                                    null, out y))
                                {
                                    BadParse(); return;
                                }
                                aI.ptr = new GbcPtr(x, y, animations.ptrs[sAnim()].ROMbank);
                            }
                        }
                        else
                        {
                            BadParse(); return;
                        }
                    }
                    else
                    {
                        BadParse(); return;
                    }

                    testCode.Add(aI);
                }

                // update data
                rTxtBytes.BackColor = System.Drawing.SystemColors.Window;
                animations.data[sAnim()].me.Clear();
                foreach (AnimeInstr aI in testCode)
                {
                    animations.data[sAnim()].me.Add(aI);
                }
                animations.UpdatePtrs(sAnim());
                UpdateCode();
                EnableWrite();
            }
        }

        private void BadParse()
        {
            rTxtBytes.BackColor = System.Drawing.Color.FromArgb(255, 191, 191);
            saveROM_TSMI.Enabled = false;
        }
    }
}
