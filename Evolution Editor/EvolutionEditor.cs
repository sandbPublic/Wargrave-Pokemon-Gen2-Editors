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

namespace Gen2_Evolution_Editor
{
    public partial class EvolutionEditor : Editor_Base_Class.Gen2Editor
    {
        public EvolutionEditor()
        {
            InitializeComponent();

            int[] readOnly = {ITEM_NAME_I, PKMN_NAME_I};
            int[] readWrite = {MOVESET_PTR_I};
            InitOffsets(readOnly, readWrite);
        }

        protected override void EnableDataEntry()
        {
            spinEvoIndex.Maximum = offset[NUM_OF_PKMN_I];

            PopulateComboBox(comboEvolveFrom, pkmnNames);
            PopulateComboBox(comboEvolveTo, pkmnNames);
            PopulateComboBox(comboItems, itemNames);

            comboEvolveFrom.Enabled = true;
            btnAddEvo.Enabled = true;
            EnOrDisable(true);

            comboEvolveFrom.SelectedIndex = 0;
        }
        
        protected override void EnableWrite()
        {
            int bytesFree = movesets.BytesFreeAt(sFrom_I());

            tboxFreeBytes.Text = bytesFree + " bytes free";

            saveROM_TSMI.Enabled = bytesFree >= 0;
        }
        
        protected override void UpdateEditor()
        {
            //spin evo index
            EnOrDisable(comboEvolveFrom.SelectedIndex > 0);
            if (comboEvolveFrom.SelectedIndex > 0)
            {
                txtNumOfEvos.Text = "/" + NumberOfEvos();
                spinEvoIndex.Maximum = NumberOfEvos();

                EnOrDisable(NumberOfEvos() > 0);
                if (NumberOfEvos() > 0)
                {
                    spinEvoIndex.Minimum = 1;

                    comboEvolveTo.SelectedIndex = sEvoData().species;

                    if (sEvoData().method <= 5)
                    {
                        comboEvoMethod.SelectedIndex = sEvoData().method - 1; // index from 1
                    }

                    EnableItems();

                    spinDVbyte.Enabled = sEvoData().IsTyrogueEvoMethod();
                    spinDVbyte.Value = sEvoData().DVparam;

                    spinEvoParam.Value = sEvoData().param;
                    comboItems.SelectedIndex = sEvoData().param;
                }
                else spinEvoIndex.Minimum = 0;
            }

            EnableWrite();
        }

        protected override void ImportData(List<string> dataStrings)
        {
            foreach (int pkmn_i in movesets.Range())
            {
                int stringIndex = 3 * (pkmn_i - movesets.start_i);

                // get line and count
                int numOfEvoData = 0;
                string[] firstLine = dataStrings[stringIndex].Split(' ');
                if (firstLine.Length == 2)
                {
                    movesets.SetRelativePtr(pkmn_i, Convert.ToInt32(firstLine[0]));
                    numOfEvoData = Convert.ToInt32(firstLine[1]);
                }

                string[] eDStrings = dataStrings[stringIndex + 1].Split(' ');
                movesets.data[pkmn_i].evoList.Clear();

                int trueIndex = 0;
                for (int eD_i = 0; eD_i < numOfEvoData; eD_i++)
                {
                    EvoData eD = new EvoData
                    {
                        method = Convert.ToByte(eDStrings[trueIndex++]),
                        param = Convert.ToByte(eDStrings[trueIndex++])
                    };
                    if (eD.IsTyrogueEvoMethod()) eD.DVparam = Convert.ToByte(eDStrings[trueIndex++]);
                    eD.species = Convert.ToByte(eDStrings[trueIndex++]);

                    movesets.data[pkmn_i].evoList.Add(eD);
                }
            }
            movesets.MakeContiguous();
        }
        
        protected override void ExportData(System.IO.StreamWriter file)
        {
            foreach (int pkmn_i in movesets.Range())
                {
                    file.WriteLine(movesets.RelativePtr(pkmn_i) + " "
                        + movesets.data[pkmn_i].evoList.Count);

                    string s = "";
                    foreach (EvoData eD in movesets.data[pkmn_i].evoList)
                    {
                        s += eD.method.ToString() + " ";
                        s += eD.param.ToString() + " ";
                        if (eD.IsTyrogueEvoMethod()) s += eD.DVparam.ToString() + " ";
                        s += eD.species.ToString() + " ";
                    }
                    s += "0";
                    file.WriteLine(s);
                    file.WriteLine("");
                }
        }
        
        protected override void ManagePointers()
        {
            new PointerManager<EvoAndLearnset>(movesets).Show();
        }



        private void EnableItems()
        { // item or trade evolution
            comboItems.Enabled = (sEvoData().method == 2 ||
                (sEvoData().method == 3 && sEvoData().param != 0xFF));
        }

        private void EnOrDisable(bool enable)
        {
            spinEvoIndex.Enabled = enable;
            comboEvolveTo.Enabled = enable;
            comboEvoMethod.Enabled = enable;
            spinEvoParam.Enabled = enable;
            comboItems.Enabled = enable; // disable in EnableItems if needed
            btnRemoveEvo.Enabled = enable;
            spinDVbyte.Enabled = enable;
        }

        private byte sFrom_I()
        {
            return (byte)comboEvolveFrom.SelectedIndex;
        }

        private int sEvo_I()
        {
            return (int)spinEvoIndex.Value - 1;
        }

        private EvoData sEvoData()
        {
            return movesets.data[sFrom_I()].evoList[sEvo_I()];
        }

        private int NumberOfEvos()
        {
            return movesets.data[sFrom_I()].evoList.Count;
        }

        private void ComboEvolveFrom_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEditor();
        }

        private void SpinEvoIndex_ValueChanged(object sender, EventArgs e)
        {
            UpdateEditor();
        }

        private void ComboEvolveTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            sEvoData().species = (byte)comboEvolveTo.SelectedIndex;
        }

        private void ComboEvoMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            sEvoData().method = (byte)(comboEvoMethod.SelectedIndex + 1);
            if (spinDVbyte.Enabled != sEvoData().IsTyrogueEvoMethod())
            {
                spinDVbyte.Enabled = sEvoData().IsTyrogueEvoMethod();
                movesets.UpdatePtrs(sFrom_I());
                UpdateEditor();
            }
            EnableItems();
        }

        private void SpinEvoParam_ValueChanged(object sender, EventArgs e)
        {
            sEvoData().param = (byte)spinEvoParam.Value;
            comboItems.SelectedIndex = (int)spinEvoParam.Value;
            EnableItems(); // for trade evo -> item trade evo
        }

        private void ComboItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            sEvoData().param = (byte)comboItems.SelectedIndex;
            spinEvoParam.Value = comboItems.SelectedIndex;
        }

        private void SpinDVbyte_ValueChanged(object sender, EventArgs e)
        {
            sEvoData().DVparam = (byte)spinDVbyte.Value;
            movesets.UpdatePtrs(sFrom_I());
            EnableWrite();
        }

        private void BtnAddEvo_Click(object sender, EventArgs e)
        {
            if (comboEvolveFrom.SelectedIndex > 0)
            {
                EvoData eD = new EvoData
                {
                    method = 1,
                    species = 1
                };
                //copy from current evo for convenience
                if (movesets.data[sFrom_I()].evoList.Count > 0)
                {
                    eD.method = sEvoData().method;
                    eD.param = sEvoData().param;
                    eD.species = sEvoData().species;
                    eD.DVparam = sEvoData().DVparam;
                }

                movesets.data[sFrom_I()].evoList.Add(eD);
                // just Add, don't bother Inserting, would need checks for empty & a way to add at end
                movesets.UpdatePtrs(sFrom_I());
                UpdateEditor();
            }
        }

        private void BtnRemoveEvo_Click(object sender, EventArgs e)
        {
            movesets.data[sFrom_I()].evoList.RemoveAt(sEvo_I());
            movesets.UpdatePtrs(sFrom_I());
            UpdateEditor();
        }
    }
}
