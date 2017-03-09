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

namespace Gen2_Evolution_Editor {
    public partial class EvolutionEditor : Editor_Base_Class.Gen2Editor {
        public EvolutionEditor() {
            InitializeComponent();

            int[] oTL = { MOVESET_PTR_I, ITEM_NAME_I, PKMN_NAME_I};
            int[] oTS = { MOVESET_PTR_I };

            initOffsets(oTL, oTS);
        }

        protected override void enableDataEntry() {
            spinEvoIndex.Maximum = offset[NUM_OF_PKMN_I];

            populateComboBox(comboEvolveFrom, pkmnNames);
            populateComboBox(comboEvolveTo, pkmnNames);
            populateComboBox(comboItems, itemNames);

            comboEvolveFrom.Enabled = true;
            btnAddEvo.Enabled = true;
            enOrDisable(true);

            comboEvolveFrom.SelectedIndex = 0;
        }
        
        protected override void enableWrite() {
            int bytesFree = movesets.bytesFreeAt(sFrom_I());

            tboxFreeBytes.Text = bytesFree + " bytes free";

            saveROM_TSMI.Enabled = bytesFree >= 0;
        }

        protected override void update() { 
            //spin evo index
            enOrDisable(comboEvolveFrom.SelectedIndex > 0);
            if (comboEvolveFrom.SelectedIndex > 0) {
                txtNumOfEvos.Text = "/" + numberOfEvos();
                spinEvoIndex.Maximum = numberOfEvos();

                enOrDisable(numberOfEvos() > 0);
                if (numberOfEvos() > 0) {
                    spinEvoIndex.Minimum = 1;

                    comboEvolveTo.SelectedIndex = sEvoData().species;

                    if (sEvoData().method <= 5) {
                        comboEvoMethod.SelectedIndex =
                            sEvoData().method - 1; // index from 1
                    }

                    enableItems();

                    spinDVbyte.Enabled = sEvoData().tyrogue();
                    spinDVbyte.Value = sEvoData().DVparam;

                    spinEvoParam.Value = sEvoData().param;
                    comboItems.SelectedIndex = sEvoData().param;
                } else {
                    spinEvoIndex.Minimum = 0;
                }
            }

            enableWrite();
        }

        private void enableItems() { // item or trade evolution
            comboItems.Enabled = (sEvoData().method == 2 || 
                (sEvoData().method == 3 && sEvoData().param != 0xFF));
        }

        private void enOrDisable(bool enable) {
            spinEvoIndex.Enabled = enable;
            comboEvolveTo.Enabled = enable;
            comboEvoMethod.Enabled = enable;
            spinEvoParam.Enabled = enable;
            btnRemoveEvo.Enabled = enable;
            spinDVbyte.Enabled = enable;
        }

        private byte sFrom_I() {
            return (byte)comboEvolveFrom.SelectedIndex;
        }

        private int sEvo_I() {
            return (int) spinEvoIndex.Value - 1;
        }

        private EvoData sEvoData() {
            return movesets.data[sFrom_I()].evoList[sEvo_I()];
        }

        private int numberOfEvos() {
            return movesets.data[sFrom_I()].evoList.Count;
        }

        protected override void importData(List<string> dataStrings) {
            foreach (int pkmn_i in movesets.range()) {
                int stringIndex = 3 * (pkmn_i - movesets.start_i);

                // get line and count
                int numOfEvoData = 0;
                string[] firstLine = dataStrings[stringIndex].Split(' ');
                if (firstLine.Length == 2) {
                    movesets.setRelativePtr(pkmn_i, Convert.ToInt32(firstLine[0]));
                    numOfEvoData = Convert.ToInt32(firstLine[1]);
                }

                string[] eDStrings = dataStrings[stringIndex + 1].Split(' ');
                movesets.data[pkmn_i].evoList.Clear();

                int trueIndex = 0;
                for (int eD_i = 0; eD_i < numOfEvoData; eD_i++) {
                    EvoData eD = new EvoData();

                    eD.method = Convert.ToByte(eDStrings[trueIndex++]);
                    eD.param = Convert.ToByte(eDStrings[trueIndex++]);
                    if (eD.tyrogue()) eD.DVparam = Convert.ToByte(eDStrings[trueIndex++]);
                    eD.species = Convert.ToByte(eDStrings[trueIndex++]);

                    movesets.data[pkmn_i].evoList.Add(eD);
                }
            }
            movesets.makeContiguous();
        }

        protected override void exportData() {
            System.IO.StreamWriter file = new System.IO.StreamWriter(data_FilePath);

            foreach (int pkmn_i in movesets.range()) {
                file.WriteLine(movesets.relativePtr(pkmn_i) + " "
                    + movesets.data[pkmn_i].evoList.Count);

                string s = "";
                foreach (EvoData eD in movesets.data[pkmn_i].evoList) {
                    s += eD.method.ToString() + " ";
                    s += eD.param.ToString() + " ";
                    if (eD.tyrogue()) s += eD.DVparam.ToString() + " ";
                    s += eD.species.ToString() + " ";
                }
                s += "0";
                file.WriteLine(s);
                file.WriteLine("");
            }

            file.Dispose();
        }

        private void comboEvolveFrom_SelectedIndexChanged(object sender, EventArgs e) {
            update();
        }

        private void spinEvoIndex_ValueChanged(object sender, EventArgs e) {
            update();
        }

        private void comboEvolveTo_SelectedIndexChanged(object sender, EventArgs e) {
            sEvoData().species = (byte)comboEvolveTo.SelectedIndex;
        }

        private void comboEvoMethod_SelectedIndexChanged(object sender, EventArgs e) {
            sEvoData().method = (byte)(comboEvoMethod.SelectedIndex+1);
            if (spinDVbyte.Enabled != sEvoData().tyrogue()) {
                spinDVbyte.Enabled = sEvoData().tyrogue();
                movesets.updatePtrs(sFrom_I());
                update();
            }
            enableItems();
        }

        private void spinEvoParam_ValueChanged(object sender, EventArgs e) {
            sEvoData().param = (byte)spinEvoParam.Value;
            comboItems.SelectedIndex = (int) spinEvoParam.Value;
            enableItems(); // for trade evo -> item trade evo
        }

        private void comboItems_SelectedIndexChanged(object sender, EventArgs e) {
            sEvoData().param = (byte)comboItems.SelectedIndex;
            spinEvoParam.Value = comboItems.SelectedIndex;
        }

        private void spinDVbyte_ValueChanged(object sender, EventArgs e) {
            sEvoData().DVparam = (byte)spinDVbyte.Value;
            movesets.updatePtrs(sFrom_I());
            enableWrite();
        }

        private void btnAddEvo_Click(object sender, EventArgs e) {
            if (comboEvolveFrom.SelectedIndex > 0) {
                EvoData eD = new EvoData();
                eD.method = 1;
                eD.species = 1;
                //copy from current evo for convenience
                if (movesets.data[sFrom_I()].evoList.Count > 0) {
                    eD.method = sEvoData().method;
                    eD.param = sEvoData().param;
                    eD.species = sEvoData().species;
                    eD.DVparam = sEvoData().DVparam;
                }

                movesets.data[sFrom_I()].evoList.Add(eD);
                // just Add, don't bother Inserting, would need checks for empty & a way to add at end
                movesets.updatePtrs(sFrom_I());
                update();
            }
        }

        private void btnRemoveEvo_Click(object sender, EventArgs e) {
            movesets.data[sFrom_I()].evoList.RemoveAt(sEvo_I());
            update();
        }

        protected override void managePointers() {
            PointerManager<EvoAndLearnset> pm = new PointerManager<EvoAndLearnset>(movesets);
            pm.Show();
        }
    }
}
