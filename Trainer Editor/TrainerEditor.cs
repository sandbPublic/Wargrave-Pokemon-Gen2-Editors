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

namespace Gen2_Trainer_Editor
{
    public partial class TrainerEditor : Editor_Base_Class.Gen2Editor
    {
        // using a comboBox for TC and Tr names causes problems when trying to edit the names
        // instead use a spin and a text box
        // not ideal because multiple names can't be seen at once...

        public TrainerEditor()
        {
            InitializeComponent();
            InitializeArrayComponents();

            int[] oTL = { TM_CODE_I, TM_SET_I, MOVESET_PTR_I, ITEM_NAME_I, PKMN_NAME_I,
                            MOVE_NAME_I, TRAINER_PTR_I, TR_CLASS_NAME_I, TR_CLASS_DV_I,
                            TR_CLASS_ATTRIBUTE_I};
            int[] oTS = { TRAINER_PTR_I, TR_CLASS_NAME_I, TR_CLASS_DV_I, TR_CLASS_ATTRIBUTE_I };
            InitOffsets(oTL, oTS);
        }

        protected override void EnableDataEntry()
        {
            #region POPULATE BOXES
            foreach (ComboBox CB in cboxSpecies) PopulateComboBox(CB, pkmnNames);

            // one list for all 24 ComboBoxes may be more efficient?
            // but CB.Items cannot be assigned to
            foreach (ComboBox CB in cboxMoves) PopulateComboBox(CB, moveNames);

            PopulateComboBox(cboxItems0, itemNames);
            PopulateComboBox(cboxItems1, itemNames);
            foreach (ComboBox CB in cboxItems) PopulateComboBox(CB, itemNames);

            spinTC.Maximum = trClassNames.end_i;
            #endregion
            #region ENABLE
            spinTC.Enabled = true;
            tboxTCName.Enabled = true;
            spinReward.Enabled = true;
            spinDVs0.Enabled = true;
            spinDVs1.Enabled = true;
            cboxItems0.Enabled = true;
            cboxItems1.Enabled = true;
            buttonAddTrainer.Enabled = true;
            buttonRemoveTrainer.Enabled = true;
            buttonAnalyze.Enabled = true;
            #endregion
        }
        
        protected override void EnableWrite()
        {
            UpdateTCNameBytesUsed();
            UpdateTrainerBytesUsed();
        }
        
        protected override void UpdateEditor()
        {
            UpdateTrainerClassArea();
        }

        protected override void ImportData(List<string> dataStrings)
        {
            int curIndex = 0;
            int numClasses = Convert.ToInt32(dataStrings[curIndex++]);

            for (int class_i = 0; class_i < numClasses; class_i++)
            {
                trClassNames.data[class_i] = dataStrings[curIndex++];

                int numOfTrainers = 0;
                string[] attributes = dataStrings[curIndex++].Split(' ');
                if (attributes.Length == 7)
                {
                    trClassDVs[2 * class_i] = Convert.ToByte(attributes[0]);
                    trClassDVs[2 * class_i + 1] = Convert.ToByte(attributes[1]);
                    trClassItems[2 * class_i] = Convert.ToByte(attributes[2]);
                    trClassItems[2 * class_i + 1] = Convert.ToByte(attributes[3]);
                    trClassRewards[class_i] = Convert.ToByte(attributes[4]);
                    trainerLists.SetRelativePtr(class_i, Convert.ToInt32(attributes[5]));
                    numOfTrainers = Convert.ToInt32(attributes[6]);
                }

                trainerLists.data[class_i] = new DBTrainerList();
                for (int trainer_i = 0; trainer_i < numOfTrainers; trainer_i++)
                {
                    Trainer t = new Trainer
                    {
                        name = dataStrings[curIndex++]
                    };
                    int numOfPkmn = 0;
                    string[] has = dataStrings[curIndex++].Split(' ');
                    if (has.Length == 3)
                    {
                        t.hasItems = (has[0] == "1");
                        t.hasMoves = (has[1] == "1");
                        numOfPkmn = Convert.ToInt32(has[2]);
                    }

                    t.team = new List<TeamMember>();
                    for (int pkmn_i = 0; pkmn_i < numOfPkmn; pkmn_i++)
                    {
                        string[] pkmnString = dataStrings[curIndex++].Split(' ');
                        var tm = new TeamMember();
                        if (pkmnString.Length == 7)
                        {
                            tm.level = Convert.ToByte(pkmnString[0]);
                            tm.species = Convert.ToByte(pkmnString[1]);
                            tm.item = Convert.ToByte(pkmnString[2]);

                            for (int move_i = 0; move_i < 4; move_i++)
                            {
                                tm.moves[move_i] = Convert.ToByte(pkmnString[move_i + 3]);
                            }
                        }
                        t.team.Add(tm);
                    } // end pkmn loop
                    trainerLists.data[class_i].LT.Add(t);
                } // end trainer loop
                curIndex++;
            }// end class loop
            trainerLists.MakeContiguous();
        }

        protected override void ExportData()
        {
            // num of classes
            // class data
            //   name
            //   attributes, list ptr, list count
            // trainer list
            //   name
            //   has items, has moves, list count
            //   list of pkmn
            // blank line

            var file = new System.IO.StreamWriter(data_FilePath);
            file.WriteLine(trClassNames.end_i - trClassNames.start_i + 1); // num classes
            for (int class_i = trClassNames.start_i; class_i <= trClassNames.end_i; class_i++)
            {
                file.WriteLine(trClassNames.data[class_i]);

                string s = trClassDVs[2 * class_i]
                    + " " + trClassDVs[2 * class_i + 1]
                    + " " + trClassItems[2 * class_i]
                    + " " + trClassItems[2 * class_i + 1]
                    + " " + trClassRewards[class_i]
                    + " " + trainerLists.RelativePtr(class_i)
                    + " " + trainerLists.data[class_i].LT.Count;
                file.WriteLine(s);

                foreach (Trainer t in trainerLists.data[class_i].LT)
                {
                    file.WriteLine(t.name);

                    s = (t.hasItems ? "1 " : "0 ") +
                        (t.hasMoves ? "1 " : "0 ") +
                        t.team.Count;
                    file.WriteLine(s);

                    foreach (TeamMember tm in t.team)
                    {
                        s = tm.level + " " + tm.species + " " + tm.item
                            + " " + tm.moves[0] + " " + tm.moves[1]
                            + " " + tm.moves[2] + " " + tm.moves[3];
                        file.WriteLine(s);
                    }
                }
                file.WriteLine("");
            }
            file.Dispose();
        }

        protected override void ManagePointers()
        {
            new PointerManager<DBTrainerList>(trainerLists, false).Show();
        }



        /// <summary>
        /// selected TrainerClass value
        /// </summary>
        /// <returns></returns>
        private int sTcV()
        {
            return (int)spinTC.Value;
        }

        /// <summary>
        /// selected Trainer List
        /// </summary>
        /// <returns></returns>
        private List<Trainer> sTrList()
        {
            return trainerLists.data[sTcV()].LT;
        }

        /// <summary>
        /// selected Trainer
        /// </summary>
        /// <returns></returns>
        private Trainer sTr()
        {
            return sTrList()[(int)spinTrainerTeamID.Value];
        }

        private void UpdateTrainerClassArea()
        {
            // update team area
            if (sTrList().Count <= 0) EnOrDisableTeamArea(false);
            else
            {
                EnOrDisableTeamArea(true);
                spinTrainerTeamID.Maximum = sTrList().Count - 1;
                UpdateTrainerTeamArea();
            }

            tboxTCName.Text = trClassNames.data[sTcV()];
            spinReward.Value = trClassRewards[sTcV()];
            UpdateDVArea();
            cboxItems0.SelectedIndex = trClassItems[2 * sTcV()];
            cboxItems1.SelectedIndex = trClassItems[2 * sTcV() + 1];
            tboxTrainerCount.Text = sTrList().Count.ToString() + " total";
            txtClassPtr.Text = "0x" + ((int)trainerLists.ptrs[sTcV()]).ToString("X");
        }

        private static readonly string[] HIDDEN_POWER_TYPES = {
            "Fighting", "Flying", "Poison", "Ground", "Rock", "Bug", "Ghost", "Steel",
            "Fire", "Water", "Grass", "Electric", "Psychic", "Ice", "Dragon", "Dark"
        };

        private void UpdateDVArea()
        {
            byte AD = trClassDVs[2 * sTcV()];
            byte SSpc = trClassDVs[2 * sTcV() + 1];

            spinDVs0.Value = AD;
            spinDVs1.Value = SSpc;

            int Attack = AD / 0x10;
            int Defense = AD % 0x10;
            int Speed = SSpc / 0x10;
            int Special = SSpc % 0x10;
            int HP = 0;
            if (Attack % 2 == 1) HP += 8;
            if (Defense % 2 == 1) HP += 4;
            if (Speed % 2 == 1) HP += 2;
            if (Special % 2 == 1) HP += 1;

            tboxHPDV.Text = HP.ToString("X");

            // male/female determined by gender ratio and Atk DV,
            // not certainly consistent among all pokemon in a trainer class

            string info = "";

            // hidden power
            int hpPowerParam = 0;
            if (Attack >= 8) hpPowerParam += 8;
            if (Defense >= 8) hpPowerParam += 4;
            if (Speed >= 8) hpPowerParam += 2;
            if (Special >= 8) hpPowerParam += 1;

            int hpPower = 31 + ((5 * hpPowerParam + (Special % 4)) / 2);
            int hpType = 4 * (Attack % 4) + (Defense % 4);

            info += hpPower.ToString() + " " + HIDDEN_POWER_TYPES[hpType];

            // shiny
            bool shiny = ((Attack % 4 == 0x2 || Attack % 4 == 0x3) &&
                Defense == 0xA && Speed == 0xA && Special == 0xA);

            info += Environment.NewLine + (shiny ? "Shiny" : "Not shiny");

            tboxDVinfo.Text = info;
        }

        private void SpinTC_ValueChanged(object sender, EventArgs e)
        {
            UpdateTrainerClassArea();
        }

        private void TboxTCName_TextChanged(object sender, EventArgs e)
        {
            trClassNames.data[sTcV()] = tboxTCName.Text;

            PrintWarningIfTooLong(((string)trClassNames.data[sTcV()]), 12);
            if (spinTrainerTeamID.Enabled)
            {
                PrintWarningIfTooLong(
                    ((string)trClassNames.data[sTcV()]) + " " + sTr().name, 18);
            }

            UpdateTCNameBytesUsed();
        }

        // for TCs like Pokemon Prof that have no trainers at all
        private void EnOrDisableTeamArea(bool enable)
        {
            spinTrainerTeamID.Enabled = enable;
            tboxTrainerName.Enabled = enable;
            checkItems.Enabled = enable;
            checkMoveset.Enabled = enable;

            for (int pkmn_i = 0; pkmn_i < 6; pkmn_i++)
            {
                spinLevels[pkmn_i].Enabled = enable;
                cboxSpecies[pkmn_i].Enabled = enable;
                cboxItems[pkmn_i].Enabled = enable;
                for (int move_j = 0; move_j < 4; move_j++)
                {
                    cboxMoves[pkmn_i, move_j].Enabled = enable;
                }
            }
        }

        private void SpinReward_ValueChanged(object sender, EventArgs e)
        {
            trClassRewards[sTcV()] = (byte)spinReward.Value;
        }
        private void SpinDVs0_ValueChanged(object sender, EventArgs e)
        {
            trClassDVs[2 * sTcV()] = (byte)spinDVs0.Value;
            UpdateDVArea();
        }
        private void SpinDVs1_ValueChanged(object sender, EventArgs e)
        {
            trClassDVs[2 * sTcV() + 1] = (byte)spinDVs1.Value;
            UpdateDVArea();
        }
        private void CboxItems0_SelectedIndexChanged(object sender, EventArgs e)
        {
            trClassItems[2 * sTcV()] = (byte)cboxItems0.SelectedIndex;
        }
        private void CboxItems1_SelectedIndexChanged(object sender, EventArgs e)
        {
            trClassItems[2 * sTcV() + 1] = (byte)cboxItems1.SelectedIndex;
        }

        private void ButtonAddTrainer_Click(object sender, EventArgs e)
        {
            Trainer Tr = new Trainer
            {
                name = "new",
                hasItems = false,
                hasMoves = false,
                team = new List<TeamMember>()
            };

            sTrList().Insert((int)spinTrainerTeamID.Value, Tr);

            if (sTrList().Count > 0) EnOrDisableTeamArea(true);

            UpdateTrainerBytesUsed();
            UpdateTrainerTeamArea();
            tboxTrainerCount.Text = sTrList().Count.ToString() + " total";
        }

        private void ButtonRemoveTrainer_Click(object sender, EventArgs e)
        {
            if (sTrList().Count > 0)
            {
                sTrList().RemoveAt((int)spinTrainerTeamID.Value);

                if (sTrList().Count <= 0) EnOrDisableTeamArea(false);
                else UpdateTrainerTeamArea();

                UpdateTrainerBytesUsed();
                tboxTrainerCount.Text = sTrList().Count.ToString() + " total";
            }
        }

        //
        // Trainer Area
        //

        // don't actually give the TeamMember the moveset here; we want old moveset to still exist
        // "underneath" so that unchecking custom moveset is reversible
        private byte[] DefaultMoveset(TeamMember TM)
        {
            byte[] ret = new byte[4];
            if (TM.species >= movesets.start_i && TM.species <= movesets.end_i)
            {
                int movesLearned = 0; // crash with TM.ID == 0?
                foreach (LearnData lD in movesets.data[TM.species].learnList)
                {
                    if (lD.level <= TM.level)
                    { // TM reached this level
                        bool newMove = true; // don't learn redundant moves
                        for (int move_j = 0; move_j < 4; move_j++)
                        {
                            if (ret[move_j] == lD.move) newMove = false;
                        }
                        if (newMove)
                        {
                            if (movesLearned < 4)
                            {
                                ret[movesLearned] = lD.move;
                                movesLearned++;
                            }
                            else
                            {
                                ret[0] = ret[1];
                                ret[1] = ret[2];
                                ret[2] = ret[3];
                                ret[3] = lD.move;
                            }
                        }
                    }
                }
            }
            return ret;
        }

        private void UpdateTrainerTeamArea()
        {
            if (spinTrainerTeamID.Enabled)
            {
                spinTrainerTeamID.Maximum = sTrList().Count - 1;
                tboxTrainerName.Text = sTr().name;
                checkItems.Checked = sTr().hasItems;
                checkMoveset.Checked = sTr().hasMoves;
                for (int teamMemb_i = 0; teamMemb_i < 6; teamMemb_i++) UpdateTrainerTeamRow(teamMemb_i);
                EnOrDisableItems(checkItems.Checked);
                EnOrDisableMoves(checkMoveset.Checked);
                UpdateTrainerBytesUsed();
            }
        }

        private void UpdateTrainerTeamRow(int teamMemb_i)
        {
            bool enableRow = (teamMemb_i < sTr().team.Count);

            spinLevels[teamMemb_i].Enabled = enableRow;
            cboxSpecies[teamMemb_i].Enabled = enableRow;

            if (enableRow)
            {
                spinLevels[teamMemb_i].Value = sTr().team[teamMemb_i].level;
                cboxSpecies[teamMemb_i].SelectedIndex = sTr().team[teamMemb_i].species;
                cboxItems[teamMemb_i].SelectedIndex = sTr().team[teamMemb_i].item;
                for (int move_j = 0; move_j < 4; move_j++)
                {
                    cboxMoves[teamMemb_i, move_j].SelectedIndex = sTr().team[teamMemb_i].moves[move_j];
                }
                UpdateForCanLearn(teamMemb_i);
            }
            else if (teamMemb_i == sTr().team.Count)
            { // allow adding new pokemon
                cboxSpecies[teamMemb_i].SelectedIndex = 0;
                cboxSpecies[teamMemb_i].Enabled = true;
            }
            else
            {
                spinLevels[teamMemb_i].Value = 0;
                cboxSpecies[teamMemb_i].SelectedIndex = 0;
                cboxItems[teamMemb_i].SelectedIndex = 0;

                for (int move_j = 0; move_j < 4; move_j++) cboxMoves[teamMemb_i, move_j].SelectedIndex = 0;
            }
        }

        private void SpinTrainerTeamID_ValueChanged(object sender, EventArgs e)
        {
            UpdateTrainerTeamArea();
        }

        private void CheckItems_CheckedChanged(object sender, EventArgs e)
        {
            sTr().hasItems = checkItems.Checked;
            EnOrDisableItems(checkItems.Checked);
            UpdateTrainerBytesUsed();
        }

        private void CheckMoveset_CheckedChanged(object sender, EventArgs e)
        {
            sTr().hasMoves = checkMoveset.Checked;
            EnOrDisableMoves(checkMoveset.Checked);
            UpdateTrainerBytesUsed();
        }

        private void TboxTrainerName_TextChanged(object sender, EventArgs e)
        {
            sTr().name = tboxTrainerName.Text;

            if (spinTrainerTeamID.Enabled)
            {
                PrintWarningIfTooLong(sTr().name, 12);
                PrintWarningIfTooLong(((string)trClassNames.data[sTcV()]) + " " + sTr().name, 18);
            }

            UpdateTrainerBytesUsed();
        }

        private bool CanLearnFn(byte species, byte moveID, byte level)
        {
            return CanLearnFn(species, moveID, level, new List<byte>());
        }

        // note: does not account for egg moves (special or inate) or tutor moves
        private bool CanLearnFn(byte species, byte moveID, byte level, List<byte> attemptedSpecies)
        {
            if (moveID == 0) return true;

            // tm
            for (int TM_i = 0; TM_i < TMCodes.Length; TM_i++)
            {
                if ((TMCodes[TM_i] == moveID) && TMSets[species, TM_i]) return true;
            }
            for (int HM_j = 0; HM_j < HMCodes.Length; HM_j++)
            {
                if ((HMCodes[HM_j] == moveID) && TMSets[species, HM_j + 50]) return true;
            }

            // scan current movelist
            foreach (LearnData lD in movesets.data[species].learnList)
            {
                if (lD.level <= level && lD.move == moveID) return true;
            }

            // if not found, try prevos that haven't been tried recursively
            // create new list
            var attSpec2 = new List<byte>();
            foreach (byte b in attemptedSpecies) attSpec2.Add(b);
            attSpec2.Add(species);

            // scan for prevos
            foreach (byte prevoID in movesets.Range())
            {
                foreach (EvoData eD in movesets.data[prevoID].evoList)
                {
                    if (eD.species == species &&  // if a prevo
                        (eD.method != 1 || eD.param <= level))
                    { // that can evolve by this level

                        bool tried = false;
                        foreach (byte b in attSpec2)
                        {
                            if (b == prevoID) tried = true;
                        }
                        if (!tried && CanLearnFn(prevoID, moveID, level, attSpec2)) return true;
                    }
                }
            }

            // if not found anywhere
            return false;
        }

        // highlight in pink when a move can't be learned
        private void UpdateForCanLearn()
        {
            for (int teamMemb_i = 0; teamMemb_i < 6; teamMemb_i++)
            {
                UpdateForCanLearn(teamMemb_i);
            }
        }

        private void UpdateForCanLearn(int teamMemb_i)
        {
            if (cboxSpecies[teamMemb_i].SelectedIndex > 0)
            {
                for (int move_j = 0; move_j < 4; move_j++)
                {
                    byte moveID = (byte)cboxMoves[teamMemb_i, move_j].SelectedIndex;

                    cboxMoves[teamMemb_i, move_j].BackColor = System.Drawing.SystemColors.Window;

                    if (cboxMoves[teamMemb_i, move_j].SelectedIndex < 0 || // selected index can be -1
                        (moveID != 0 && cboxMoves[teamMemb_i, move_j].Enabled
                        && !CanLearnFn(
                        (byte)cboxSpecies[teamMemb_i].SelectedIndex,
                        moveID,
                        (byte)spinLevels[teamMemb_i].Value)))
                    {
                        cboxMoves[teamMemb_i, move_j].BackColor =
                            System.Drawing.Color.FromArgb(255, 191, 191);
                    }
                }
            }
        }

        //
        // array components
        //

        private void EnOrDisableItems(bool enable)
        {
            for (int pkmn_i = 0; pkmn_i < 6; pkmn_i++)
            {
                cboxItems[pkmn_i].Enabled = (enable && (pkmn_i < sTr().team.Count));

                cboxItems[pkmn_i].SelectedIndex =
                    (cboxItems[pkmn_i].Enabled ? sTr().team[pkmn_i].item : 0);
            }
        }

        private void EnOrDisableMoves(bool enable)
        {
            for (int pkmn_i = 0; pkmn_i < 6; pkmn_i++)
            {
                if (pkmn_i < sTr().team.Count)
                {
                    byte[] dMoves = DefaultMoveset(sTr().team[pkmn_i]);

                    for (int move_j = 0; move_j < 4; move_j++)
                    {
                        cboxMoves[pkmn_i, move_j].Enabled = enable;

                        cboxMoves[pkmn_i, move_j].SelectedIndex =
                            (enable ?
                            sTr().team[pkmn_i].moves[move_j] :
                            dMoves[move_j]);
                    }
                }
                else
                {
                    for (int move_j = 0; move_j < 4; move_j++)
                    {
                        cboxMoves[pkmn_i, move_j].Enabled = false;
                        cboxMoves[pkmn_i, move_j].SelectedIndex = 0;
                    }
                }
            }
        }

        private void SpinLevels_ValueChanged(object sender, EventArgs e)
        {
            for (int teamMemb_i = 0; teamMemb_i < sTr().team.Count; teamMemb_i++)
            {
                if (spinLevels[teamMemb_i].Focused)
                {
                    sTr().team[teamMemb_i].level = (byte)spinLevels[teamMemb_i].Value;
                    UpdateForCanLearn(teamMemb_i);
                }
            }
        }

        // change species OR add/remove pokemon
        private void CboxSpecies_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int teamMemb_i = 0; teamMemb_i <= sTr().team.Count && teamMemb_i < 6; teamMemb_i++)
            {
                if (cboxSpecies[teamMemb_i].Focused)
                {
                    byte species = (byte)cboxSpecies[teamMemb_i].SelectedIndex;

                    if (species == 0) sTr().team.RemoveAt(teamMemb_i); // shorten team
                    else
                    {
                        if (teamMemb_i < sTr().team.Count) sTr().team[teamMemb_i].species = species;
                        else
                        { // new pokemon
                            var TM = new TeamMember
                            {
                                species = species,
                                level = 1
                            };

                            sTr().team.Add(TM);
                        }
                    }
                    UpdateTrainerTeamArea();
                }
            }
        }

        private void CboxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int pkmn_i = 0; pkmn_i < sTr().team.Count; pkmn_i++)
            {
                if (cboxItems[pkmn_i].Focused) sTr().team[pkmn_i].item = (byte)cboxItems[pkmn_i].SelectedIndex;
            }
        }

        private void CboxMoves_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int pkmn_i = 0; pkmn_i < sTr().team.Count; pkmn_i++)
            {
                for (int move_j = 0; move_j < 4; move_j++)
                {
                    if (cboxMoves[pkmn_i, move_j].Focused)
                    {
                        sTr().team[pkmn_i].moves[move_j] = (byte)cboxMoves[pkmn_i, move_j].SelectedIndex;
                    }
                }
            }

            UpdateForCanLearn();
        }

        private void UpdateTCNameBytesUsed()
        {
            tboxFreeTCBytes.Text = trClassNames.BytesFreeAt(sTcV()) + " class bytes free";

            saveROM_TSMI.Enabled = trClassNames.BytesFreeAt(0) >= 0 && trainerLists.BytesOverlapAt() == -1;
        }

        private void UpdateTrainerBytesUsed()
        {
            trainerLists.UpdatePtrs(sTcV());
            tboxFreeTrBytes.Text = trainerLists.BytesFreeAt(sTcV()) + " trainer bytes free";

            saveROM_TSMI.Enabled = trClassNames.BytesFreeAt(0) >= 0  && trainerLists.BytesOverlapAt() == -1;
        }

        private void ButtonAnalyze_Click(object sender, EventArgs e)
        {
            int[] usage = new int[pkmnNames.Length];
            foreach (DBTrainerList dbtl in trainerLists.data)
            {
                foreach (Trainer tr in dbtl.LT)
                {
                    foreach (TeamMember tm in tr.team) usage[tm.species]++;
                }
            }

            // evolutionary families
            // for each pokemon
            // if nothing evolves into it, add rarity from everything in it's tree

            var L_ss = new List<SortingString>();
            bool[] usagePassed = new bool[pkmnNames.Length];
            for (int pkName_i = 1; pkName_i < pkmnNames.Length; pkName_i++)
            {
                // get rarity from evolutions
                for (int evo_j = 1; evo_j < pkmnNames.Length; evo_j++)
                {
                    foreach (EvoData ed in movesets.data[pkName_i].evoList)
                    {
                        if (ed.species == evo_j && !usagePassed[evo_j])
                        {
                            usage[pkName_i] += usage[evo_j];
                            usagePassed[evo_j] = true;
                        }
                    }
                }
            }
            // do this seperately, second so that evos that precede their prevos
            // in pokedex order aren't counted as seperate families
            for (int pkName_i = 1; pkName_i < pkmnNames.Length; pkName_i++)
            {
                var ss = new SortingString();
                if (!usagePassed[pkName_i])
                {
                    ss.sortValue = usage[pkName_i];
                    ss.me = usage[pkName_i].ToString("D3") + " - " + pkmnNames[pkName_i];
                    L_ss.Add(ss);
                }
            }

            new FormAnalysis(L_ss).Show();
        }
    }
}