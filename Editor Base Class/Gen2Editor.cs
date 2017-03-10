﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // open save read write files

namespace Editor_Base_Class {
    /// <summary>
    /// <para>base class for various forms that edit pokemon generation 2 ROMs</para>
    /// <para>contains data on offsets; data structures; and loading, saving, </para>
    /// <para>and version selection methods; as well as other shared methods</para>
    /// </summary>
    public partial class Gen2Editor : Form {
        protected OpenFileDialog ofdOffsets;
        protected string offsets_FilePath;

        protected OpenFileDialog ofdROM;
        public ROM_FileStream ROM_File;
        protected string ROM_FilePath;

        protected OpenFileDialog ofdData;
        protected SaveFileDialog sfdData;
        protected string data_FilePath;

        public Gen2Editor() {
            ofdOffsets = new OpenFileDialog();
            ofdOffsets.Filter = ".WGen2_offsets.txt|*.WGen2_offsets.txt|All Files|*";
            ofdOffsets.Title = "Open offset file";
            ofdOffsets.InitialDirectory = Application.StartupPath;

            ofdROM = new OpenFileDialog();
            ofdROM.Filter = ".gbc|*.gbc|All Files|*";
            ofdROM.Title = "Open ROM file";
            ofdROM.InitialDirectory = Application.StartupPath;

            ofdData = new OpenFileDialog();
            ofdData.Filter = ".WGen2_data.txt|*.WGen2_data.txt|All Files|*";
            ofdData.Title = "Open data file";
            ofdData.InitialDirectory = Application.StartupPath;

            sfdData = new SaveFileDialog();
            sfdData.Filter = ".WGen2_data.txt|*.WGen2_data.txt|All Files|*";
            sfdData.Title = "Save data file";
            sfdData.InitialDirectory = Application.StartupPath;

            InitializeComponent();

            offsetsToLoad = new List<int>();
        }

        private bool jumpToIfLoading(int offset_i) {
            if (loadOffset[offset_i]) {
                ROM_File.Position = offset[offset_i];
                return true;
            }
            return false;
        }

        public bool jumpToIfSaving(int offset_i) {
            if (saveOffset[offset_i]) {
                ROM_File.Position = offset[offset_i];
                return true;
            }
            return false;
        }

        private void loadOffsets() {
            if (ofdOffsets.ShowDialog() == DialogResult.OK) {
                offsets_FilePath = ofdOffsets.FileName;
                ofdOffsets.Dispose();

                List<string> offsetsStrings = new List<string>();
                foreach (string s in File.ReadLines(offsets_FilePath)) {
                    offsetsStrings.Add(s);
                }

                try {
                    for (int offset_i = 0; offset_i < NUM_OF_OFFSETS; offset_i++) {
                        string[] s = offsetsStrings[offset_i].Split(' '); // read first word == offset
                        offset[offset_i] = Convert.ToInt32(s[0], 16);
                    }
                } catch (Exception e) { //FormatException
                    FormMessage exception = new FormMessage("Bad offset file: " 
                        + offsets_FilePath + Environment.NewLine + e.Message);
                    exception.Show();

                    offsets_FilePath = null;
                }
            }
        }

        private void loadFrom(OpenFileDialog ofd, bool fromRom) {
            if (ofd.ShowDialog() == DialogResult.OK) {
                if (fromRom) ROM_FilePath = ofd.FileName;
                else data_FilePath = ofd.FileName;
                ofd.Dispose();

                if (fromRom) {
                    loadFromRom();
                    exportData_TSMI.Enabled = true;
                    managePtrs_TSMI.Enabled = true;
                    enableDataEntry();
                } else {
                    List<string> dataStrings = new List<string>();
                    foreach (string s in File.ReadLines(data_FilePath)) {
                        dataStrings.Add(s);
                    }

                    try {
                        importData(dataStrings);
                    } catch (Exception e) { //FormatException, ArgumentOutOfRangeException
                        FormMessage exception = new FormMessage("Bad data file: "
                            + data_FilePath + Environment.NewLine + e.Message);
                        exception.Show();
                    }
                }
                enableWrite();
                update();
            }
        }

        private void popListArea(List<AreaWildData> area, bool water = false) {
            while (ROM_File.ReadByte() != 0xFF && area.Count <= 999) {
                ROM_File.Position--;
                area.Add(ROM_File.readWildArea(water));
            }
        }

        private void loadFromRom() {
            ROM_File = new ROM_FileStream(ROM_FilePath, FileMode.Open);
            #region ITEM
            if (jumpToIfLoading(ITEM_STRUCT_I)) {
                items = new byte[offset[NUM_OF_ITEMS_I]+1, 7];
                for (int item_i = 1; item_i <= offset[NUM_OF_ITEMS_I]; item_i++) {
                    for (int byte_j = 0; byte_j < 7; byte_j++) {
                        items[item_i, byte_j] = (byte)ROM_File.ReadByte();
                    }
                }
            }

            if (jumpToIfLoading(ITEM_ASM_I)) {
                // data after 0xBD does not seem to be the asm pointers anymore
                // tms have completely different asm
                itemASM = new int[offset[LAST_NON_TM_ITEM_I]+1];
                for (int asm_i = 1; asm_i <= offset[LAST_NON_TM_ITEM_I]; asm_i++) {
                    itemASM[asm_i] = ROM_File.ReadByte() * 0x100 + ROM_File.ReadByte();
                }
            }

            if (jumpToIfLoading(ITEM_NAME_I)) {
                itemNames = new DataBlock<DBString>(offset[ITEM_NAME_I],
                    offset[ITEM_NAME_END_I], false, offset[NUM_OF_ITEMS_I]);
                itemNames.readFromFile(ROM_File);
            }

            if (jumpToIfLoading(ITEM_DESC_PTR_I)) {
                itemDescs = new DataBlock<DBString>(offset[ITEM_DESC_PTR_I],
                    offset[ITEM_DESC_END_I], true, offset[LAST_NON_TM_ITEM_I]);
                itemDescs.readFromFile(ROM_File);
            }
            #endregion
            #region MOVE
            if (jumpToIfLoading(MOVE_STRUCT_I)){
                moves = new byte[offset[NUM_OF_MOVES_I]+1, 7];
                for (int move_i = 1; move_i <= offset[NUM_OF_MOVES_I]; move_i++) {
                    for (int byte_j = 0; byte_j < 7; byte_j++) {
                        moves[move_i, byte_j] = (byte)ROM_File.ReadByte();
                    }
                }
            }

            if (jumpToIfLoading(MOVE_NAME_I)){
                moveNames = new DataBlock<DBString>(offset[MOVE_NAME_I], offset[MOVE_NAME_END_I],
                    false, offset[NUM_OF_MOVES_I]);

                moveNames.readFromFile(ROM_File);
            }

            if (jumpToIfLoading(MOVE_DESC_PTR_I)){
                moveDescs = new DataBlock<DBString>(offset[MOVE_DESC_PTR_I], offset[MOVE_DESC_END_I],
                    true, offset[NUM_OF_MOVES_I], 1);
                moveDescs.readFromFile(ROM_File);
            }

            if (jumpToIfLoading(TYPE_NAME_PTR_I)){
                typeNames = new DataBlock<DBString>(offset[TYPE_NAME_PTR_I], 0,
                    true, offset[NUM_OF_TYPES_I] - 1, 0);
                typeNames.readFromFile(ROM_File, true);
            }

            if (jumpToIfLoading(CRIT_LIST_PTR_I)) {
                moveIsCrit = new bool[offset[NUM_OF_MOVES_I] + 1];
                ROM_File.Position = ROM_File.readGBCPtr();
                for (int crit_i = 0; crit_i <= offset[NUM_OF_MOVES_I]; crit_i++) {
                    moveIsCrit[crit_i] = false;
                }
                byte C = (byte)ROM_File.ReadByte();
                while (C != 0xFF) {
                    moveIsCrit[C] = true;
                    C = (byte)ROM_File.ReadByte();
                }
            }
            #endregion
            #region MOVESET
            if (jumpToIfLoading(PKMN_NAME_I)){
                pkmnNames = new string[offset[NUM_OF_PKMN_I]+1];
                // fixed to length 10
                for (int pkmnName_i = 1; pkmnName_i <= offset[NUM_OF_PKMN_I]; pkmnName_i++) {
                    byte[] nameInBytes =
                        ROM_File.readBytes(offset[PKMN_NAME_I] + 10 * (pkmnName_i - 1), 10);

                    pkmnNames[pkmnName_i] = "";
                    foreach (byte b1 in nameInBytes) {
                        if (b1 != 0x50) pkmnNames[pkmnName_i] += ROM_FileStream.pkmnByteToChar(b1);
                    }
                }
            }

            if (jumpToIfLoading(MOVESET_PTR_I)){
                movesets = new DataBlock<EvoAndLearnset>(offset[MOVESET_PTR_I], offset[MOVESET_END_I],
                    true, offset[NUM_OF_PKMN_I]);

                movesets.readFromFile(ROM_File);
            }

            if (jumpToIfLoading(TM_CODE_I)){
                TMCodes = ROM_File.readBytes(offset[TM_CODE_I], 50);
                HMCodes = ROM_File.readBytes(offset[TM_CODE_I] + 50, 8);
            }

            if (jumpToIfLoading(TM_SET_I)) {
                for (int pkmn_i = 1; pkmn_i <= offset[NUM_OF_PKMN_I]; pkmn_i++) {
                    byte[] TMsetBytes = ROM_File.readBytes(offset[TM_SET_I] + 0x20 * (pkmn_i - 1), 8);
                    bool[] TMsetBools = ROM_FileStream.TMBoolsFromBytes(TMsetBytes);
                    for (int bool_j = 0; bool_j < 64; bool_j++) {
                        TMSets[pkmn_i, bool_j] = TMsetBools[bool_j];
                    }
                }
            }
            #endregion
            #region TRAINER
            int numOfTrC = 0;
            jumpToIfLoading(TR_CLASS_NAME_I);
            while (ROM_File.Position < offset[TR_CLASS_NAME_END_I] - 1) {
                if (ROM_File.ReadByte() == 0) {
                    break;
                } else {
                    ROM_File.Position--;
                    string s = ROM_File.pkmnReadString(20);
                    numOfTrC++;
                }
            }

            if (jumpToIfLoading(TR_CLASS_NAME_I)) {
                trClassNames = new DataBlock<DBString>(offset[TR_CLASS_NAME_I], 
                    offset[TR_CLASS_NAME_END_I], false, numOfTrC - 1, 0);
                trClassNames.readFromFile(ROM_File);
            }

            // dvs, 4 bits per dv
            if (jumpToIfLoading(TR_CLASS_DV_I)) {
                trClassDVs = new List<byte>();
                for (int tc_i = trClassNames.start_i; tc_i <= trClassNames.end_i; tc_i++) {
                    trClassDVs.Add((byte)ROM_File.ReadByte()); // atkdef
                    trClassDVs.Add((byte)ROM_File.ReadByte()); // speedspec
                }
            }

            // items used and money
            if (jumpToIfLoading(TR_CLASS_ATTRIBUTE_I)) {
                trClassItems = new List<byte>();
                trClassRewards = new List<byte>();
                for (int tc_i = trClassNames.start_i; tc_i <= trClassNames.end_i; tc_i++) {
                    trClassItems.Add((byte)ROM_File.ReadByte());
                    trClassItems.Add((byte)ROM_File.ReadByte());
                    trClassRewards.Add((byte)ROM_File.ReadByte());
                    ROM_File.Position += 4; // skip AI behavior bytes
                }
            }

            if (jumpToIfLoading(TRAINER_PTR_I)) {
                trainerLists = new DataBlock<DBTrainerList>(offset[TRAINER_PTR_I], 
                    offset[TRAINER_END_I], true, numOfTrC - 1, 0);
                trainerLists.readFromFile(ROM_File);
                trainerLists.purgeDupes();
            }
            #endregion
            #region ANIMATION
            if (jumpToIfLoading(ANIM_PTR_I)) {
                animations = new DataBlock<AnimationCode>(offset[ANIM_PTR_I],
                    offset[ANIM_END_I], true, offset[NUM_OF_ANIMS_I], 0);
                animations.readFromFile(ROM_File);
            }
            #endregion
            #region WILD
            if (jumpToIfLoading(WILD_I)) {
                johtoLand = new List<AreaWildData>();
                johtoWater = new List<AreaWildData>();
                kantoLand = new List<AreaWildData>();
                kantoWater = new List<AreaWildData>();
                swarm = new List<AreaWildData>();
                popListArea(johtoLand);
                popListArea(johtoWater, true);
                popListArea(kantoLand);
                popListArea(kantoWater, true);
                popListArea(swarm);
            }
            if (jumpToIfLoading(AREA_NAME_PTR_I)) {
                areaNames = new DataBlock<DBString>(offset[AREA_NAME_PTR_I],
                    offset[AREA_NAME_END_I], true, offset[NUM_OF_AREA_NAMES_I], 0);
                areaNames.readFromFile(ROM_File);
            }
            #endregion
            ROM_File.Dispose();
        }

        /// <summary>
        /// can disable menu items
        /// </summary>
        protected virtual void enableDataEntry() {}
        protected virtual void enableWrite() {}
        protected virtual void update() {}

        protected virtual void importData(List<string> dataStrings) { }
        protected virtual void exportData() { }
        protected virtual void managePointers() { }

        protected void printWarningIfTooLong(string name, int maxLength) {
            if (name.Length > maxLength) {
                FormMessage msg = new FormMessage("Warning: " + Environment.NewLine +
                    name + " is longer than " + maxLength + " characters");
                msg.Show();
            }
        }

        protected void populateComboBox(ComboBox CB, string[] strings) {
            CB.Items.Clear();
            foreach (string s in strings) {
                if (s == null) CB.Items.Add("-");
                else CB.Items.Add(s);
            }
        }

        // duplicating this code is simpler than fiddling with array type conversion
        protected void populateComboBox(ComboBox CB, DataBlock<DBString> DB, int increment = 1) {
            CB.Items.Clear();
            for (int name_i = 0; name_i <= DB.end_i; name_i+=increment) {
                if (DB.data[name_i] == null || DB.data[name_i] == ROM_FileStream.INVALID_STRING)
                    CB.Items.Add("-");
                else
                    CB.Items.Add((string)DB.data[name_i]);
            }
        }
    }

    static class Program {
        static void Main() {
            Gen2Editor gsc = new Gen2Editor();
        }
    }
}