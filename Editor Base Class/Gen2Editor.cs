using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO; // open save read write files

namespace Editor_Base_Class
{
    /// <summary>
    /// <para>base class for various forms that edit pokemon generation 2 ROMs</para>
    /// <para>contains data on offsets; data structures; and loading, saving, </para>
    /// <para>and version selection methods; as well as other shared methods</para>
    /// </summary>
    public partial class Gen2Editor : Form
    {
        public Gen2Editor()
        {
            InitializeComponent();

            offsetsToLoad = new List<int>();
        }

        private void LoadOffsets()
        {
            using (var ofdOffsets = new OpenFileDialog
            {
                Filter = ".WGen2_offsets.txt|*.WGen2_offsets.txt|All Files|*",
                Title = "Open offset file",
                InitialDirectory = Application.StartupPath
            })
            {
                if (ofdOffsets.ShowDialog() != DialogResult.OK) return;

                var offsetsStrings = new List<string>();
                foreach (string s in File.ReadLines(ofdOffsets.FileName)) offsetsStrings.Add(s);

                for (int offset_i = 0; offset_i < NUM_OF_OFFSETS; offset_i++)
                {
                    // read first word == offset in hex
                    try
                    {
                        string[] s = offsetsStrings[offset_i].Split(' ');
                        offset[offset_i] = Convert.ToInt32(s[0], 16);
                    }
                    catch (Exception e)
                    {
                        new FormMessage(
                            "Bad offset file: " + ofdOffsets.FileName + Environment.NewLine
                            + e.Message + e.GetType().ToString() + Environment.NewLine
                            + "Problem on line " + (offset_i + 1)).Show();

                        openROM_TSMI.Enabled = false;
                        saveROM_TSMI.Enabled = false;
                        return;
                    }
                }
                openROM_TSMI.Enabled = true;
            }
        }

        private void LoadFromROM()
        {
            using (var ofdROM = new OpenFileDialog
            {
                Filter = ".gbc|*.gbc|All Files|*",
                Title = "Load from ROM file",
                InitialDirectory = Application.StartupPath
            })
            {
                if (ofdROM.ShowDialog() != DialogResult.OK) return;

                using (var ROM_File = new ROM_FileStream(ofdROM.FileName, FileMode.Open))
                {
                    bool JumpToIfLoading(int offset_i)
                    {
                        if (loadOffset[offset_i]) ROM_File.Position = offset[offset_i];
                        return loadOffset[offset_i];
                    }

                    #region ITEM
                    if (JumpToIfLoading(ITEM_STRUCT_I))
                    {
                        items = new byte[offset[NUM_OF_ITEMS_I] + 1, 7];
                        for (int item_i = 1; item_i <= offset[NUM_OF_ITEMS_I]; item_i++)
                        {
                            for (int byte_j = 0; byte_j < 7; byte_j++)
                            {
                                items[item_i, byte_j] = (byte)ROM_File.ReadByte();
                            }
                        }
                    }

                    if (JumpToIfLoading(ITEM_ASM_I))
                    {
                        // data after 0xBD does not seem to be the asm pointers anymore
                        // tms have completely different asm
                        itemASM = new int[offset[LAST_NON_TM_ITEM_I] + 1];
                        for (int asm_i = 1; asm_i <= offset[LAST_NON_TM_ITEM_I]; asm_i++)
                        {
                            itemASM[asm_i] = ROM_File.ReadByte() * 0x100 + ROM_File.ReadByte();
                        }
                    }

                    if (JumpToIfLoading(ITEM_NAME_I))
                    {
                        itemNames = new DataBlock<DBString>(offset[ITEM_NAME_I],
                            offset[ITEM_NAME_END_I], false, offset[NUM_OF_ITEMS_I]);
                        itemNames.ReadFromFile(ROM_File);
                    }

                    if (JumpToIfLoading(ITEM_DESC_PTR_I))
                    {
                        itemDescs = new DataBlock<DBString>(offset[ITEM_DESC_PTR_I],
                            offset[ITEM_DESC_END_I], true, offset[LAST_NON_TM_ITEM_I]);
                        itemDescs.ReadFromFile(ROM_File);
                    }
                    #endregion
                    #region MOVE
                    if (JumpToIfLoading(MOVE_STRUCT_I))
                    {
                        moves = new byte[offset[NUM_OF_MOVES_I] + 1, 7];
                        for (int move_i = 1; move_i <= offset[NUM_OF_MOVES_I]; move_i++)
                        {
                            for (int byte_j = 0; byte_j < 7; byte_j++)
                            {
                                moves[move_i, byte_j] = (byte)ROM_File.ReadByte();
                            }
                        }
                    }

                    if (JumpToIfLoading(MOVE_NAME_I))
                    {
                        moveNames = new DataBlock<DBString>(offset[MOVE_NAME_I], offset[MOVE_NAME_END_I],
                            false, offset[NUM_OF_MOVES_I]);

                        moveNames.ReadFromFile(ROM_File);
                    }

                    if (JumpToIfLoading(MOVE_DESC_PTR_I))
                    {
                        moveDescs = new DataBlock<DBString>(offset[MOVE_DESC_PTR_I], offset[MOVE_DESC_END_I],
                            true, offset[NUM_OF_MOVES_I], 1);
                        moveDescs.ReadFromFile(ROM_File);
                    }

                    if (JumpToIfLoading(TYPE_NAME_PTR_I))
                    {
                        typeNames = new DataBlock<DBString>(offset[TYPE_NAME_PTR_I], 0,
                            true, offset[NUM_OF_TYPES_I] - 1, 0);
                        typeNames.ReadFromFile(ROM_File, true);
                    }

                    if (JumpToIfLoading(CRIT_LIST_PTR_I))
                    {
                        moveIsCrit = new bool[offset[NUM_OF_MOVES_I] + 1];
                        ROM_File.Position = ROM_File.ReadGBCPtr();
                        for (int crit_i = 0; crit_i <= offset[NUM_OF_MOVES_I]; crit_i++)
                        {
                            moveIsCrit[crit_i] = false;
                        }
                        byte C = (byte)ROM_File.ReadByte();
                        while (C != 0xFF)
                        {
                            moveIsCrit[C] = true;
                            C = (byte)ROM_File.ReadByte();
                        }
                    }
                    #endregion
                    #region MOVESET
                    if (JumpToIfLoading(PKMN_NAME_I))
                    {
                        pkmnNames = new string[offset[NUM_OF_PKMN_I] + 1];
                        for (int pkmnName_i = 1; pkmnName_i <= offset[NUM_OF_PKMN_I]; pkmnName_i++)
                        { // fixed to length 10
                            ROM_File.Position = offset[PKMN_NAME_I] + 10 * (pkmnName_i - 1);
                            pkmnNames[pkmnName_i] = ROM_File.PkmnReadString(10);
                        }
                    }

                    if (JumpToIfLoading(MOVESET_PTR_I))
                    {
                        movesets = new DataBlock<EvoAndLearnset>(offset[MOVESET_PTR_I], offset[MOVESET_END_I],
                            true, offset[NUM_OF_PKMN_I]);

                        movesets.ReadFromFile(ROM_File);
                    }

                    if (JumpToIfLoading(TM_CODE_I))
                    {
                        TMCodes = ROM_File.ReadBytes(offset[TM_CODE_I], 50);
                        HMCodes = ROM_File.ReadBytes(offset[TM_CODE_I] + 50, 8);
                    }

                    if (JumpToIfLoading(TM_SET_I))
                    {
                        for (int pkmn_i = 1; pkmn_i <= offset[NUM_OF_PKMN_I]; pkmn_i++)
                        {
                            byte[] TMsetBytes = ROM_File.ReadBytes(offset[TM_SET_I] + 0x20 * (pkmn_i - 1), 8);
                            bool[] TMsetBools = ROM_FileStream.TMBoolsFromBytes(TMsetBytes);
                            for (int bool_j = 0; bool_j < 64; bool_j++)
                            {
                                TMSets[pkmn_i, bool_j] = TMsetBools[bool_j];
                            }
                        }
                    }
                    #endregion
                    #region TRAINER
                    int numOfTrC = 0;
                    JumpToIfLoading(TR_GROUP_NAME_I);
                    while (ROM_File.Position < offset[TR_CLASS_NAME_END_I] - 1)
                    {
                        if (ROM_File.ReadByte() == 0) break;
                        else
                        {
                            ROM_File.Position--;
                            ROM_File.PkmnReadString(20); // just skipping forward
                            numOfTrC++;
                        }
                    }

                    if (JumpToIfLoading(TR_GROUP_NAME_I))
                    {
                        trGroupNames = new DataBlock<DBString>(offset[TR_GROUP_NAME_I],
                            offset[TR_CLASS_NAME_END_I], false, numOfTrC - 1, 0);
                        trGroupNames.ReadFromFile(ROM_File);
                    }

                    // dvs, 4 bits per dv
                    if (JumpToIfLoading(TR_GROUP_DV_I))
                    {
                        trGroupDVs = new List<byte>();
                        for (int tc_i = trGroupNames.start_i; tc_i <= trGroupNames.end_i; tc_i++)
                        {
                            trGroupDVs.Add((byte)ROM_File.ReadByte()); // atkdef
                            trGroupDVs.Add((byte)ROM_File.ReadByte()); // speedspec
                        }
                    }

                    // items used and money
                    if (JumpToIfLoading(TR_GROUP_ATTRIBUTE_I))
                    {
                        trGroupItems = new List<byte>();
                        trGroupRewards = new List<byte>();
                        for (int tc_i = trGroupNames.start_i; tc_i <= trGroupNames.end_i; tc_i++)
                        {
                            trGroupItems.Add((byte)ROM_File.ReadByte());
                            trGroupItems.Add((byte)ROM_File.ReadByte());
                            trGroupRewards.Add((byte)ROM_File.ReadByte());
                            ROM_File.Position += 4; // skip AI behavior bytes
                        }
                    }

                    if (JumpToIfLoading(TRAINER_PTR_I))
                    {
                        trainerLists = new DataBlock<DBTrainerList>(offset[TRAINER_PTR_I],
                            offset[TRAINER_END_I], true, numOfTrC - 1, 0);
                        trainerLists.ReadFromFile(ROM_File);
                        trainerLists.PurgeDupes();
                    }
                    #endregion
                    #region ANIMATION
                    if (JumpToIfLoading(ANIM_PTR_I))
                    {
                        animations = new DataBlock<AnimationCode>(offset[ANIM_PTR_I],
                            offset[ANIM_END_I], true, offset[NUM_OF_ANIMS_I], 0);
                        animations.ReadFromFile(ROM_File);
                    }
                    #endregion
                    #region WILD
                    if (JumpToIfLoading(WILD_I))
                    {
                        johtoLand = new List<AreaWildData>();
                        johtoWater = new List<AreaWildData>();
                        kantoLand = new List<AreaWildData>();
                        kantoWater = new List<AreaWildData>();
                        swarm = new List<AreaWildData>();

                        void PopListArea(List<AreaWildData> area, bool water = false)
                        {
                            while (ROM_File.ReadByte() != 0xFF && area.Count <= 999)
                            {
                                ROM_File.Position--;
                                area.Add(ROM_File.ReadWildArea(water));
                            }
                        }

                        PopListArea(johtoLand);
                        PopListArea(johtoWater, true);
                        PopListArea(kantoLand);
                        PopListArea(kantoWater, true);
                        PopListArea(swarm);
                    }
                    if (JumpToIfLoading(AREA_NAME_PTR_I))
                    {
                        areaNames = new DataBlock<DBString>(offset[AREA_NAME_PTR_I],
                            offset[AREA_NAME_END_I], true, offset[NUM_OF_AREA_NAMES_I], 0);
                        areaNames.ReadFromFile(ROM_File);
                    }
                    #endregion
                }

                exportData_TSMI.Enabled = true;
                managePtrs_TSMI.Enabled = true;
                EnableDataEntry();
                EnableWrite();
                UpdateEditor();
            }
        }

        private void SaveToROM()
        {
            using (var sfdROM = new SaveFileDialog
            {
                Filter = ".gbc|*.gbc|All Files|*",
                Title = "Save ROM file",
                InitialDirectory = Application.StartupPath
            })
            { 
                if (sfdROM.ShowDialog() != DialogResult.OK) return;

                using (var ROM_File = new ROM_FileStream(sfdROM.FileName, FileMode.Open))
                {
                    bool JumpToIfSaving(int offset_i)
                    {
                        if (saveOffset[offset_i]) ROM_File.Position = offset[offset_i];
                        return saveOffset[offset_i];
                    }

                    #region ITEM
                    if (JumpToIfSaving(ITEM_STRUCT_I))
                    {
                        for (int item_i = 1; item_i <= offset[NUM_OF_ITEMS_I]; item_i++)
                        {
                            for (int byte_i = 0; byte_i < 7; byte_i++)
                            {
                                ROM_File.WriteByte(items[item_i, byte_i]);
                            }
                        }
                    }

                    if (JumpToIfSaving(ITEM_ASM_I))
                    {
                        for (int asm_i = 1; asm_i <= offset[LAST_NON_TM_ITEM_I]; asm_i++)
                        {
                            ROM_File.WriteByte((byte)(itemASM[asm_i] / 0x100));
                            ROM_File.WriteByte((byte)(itemASM[asm_i] % 0x100));
                        }
                    }

                    if (JumpToIfSaving(ITEM_NAME_I)) itemNames.WriteToFile(ROM_File);

                    if (JumpToIfSaving(ITEM_DESC_PTR_I)) itemDescs.WriteToFile(ROM_File);

                    #endregion
                    #region MOVE
                    if (JumpToIfSaving(MOVE_STRUCT_I))
                    {
                        for (int move_i = 1; move_i <= offset[NUM_OF_MOVES_I]; move_i++)
                        {
                            for (int byte_j = 0; byte_j < 7; byte_j++)
                            {
                                ROM_File.WriteByte(moves[move_i, byte_j]);
                            }
                        }
                    }

                    if (JumpToIfSaving(MOVE_NAME_I)) moveNames.WriteToFile(ROM_File);

                    if (JumpToIfSaving(MOVE_DESC_PTR_I)) moveDescs.WriteToFile(ROM_File);

                    if (JumpToIfSaving(CRIT_LIST_PTR_I))
                    {
                        ROM_File.WriteLocalGBCPtr(offset[NEW_CRIT_LIST_I]);

                        ROM_File.Position = offset[NEW_CRIT_LIST_I];
                        for (int move_i = 0; move_i < offset[NUM_OF_MOVES_I]; move_i++)
                        {
                            if (moveIsCrit[move_i])
                            {
                                ROM_File.WriteByte((byte)move_i);
                            }
                        }
                        ROM_File.WriteByte(0xFF);
                    }
                    #endregion
                    #region MOVESET
                    if (JumpToIfSaving(MOVESET_PTR_I)) movesets.WriteToFile(ROM_File);

                    if (JumpToIfSaving(TM_SET_I))
                    {
                        for (int pkmn_i = 1; pkmn_i <= offset[NUM_OF_PKMN_I]; pkmn_i++)
                        {
                            bool[] TMsetBools = new bool[64];
                            for (int bool_j = 0; bool_j < 64; bool_j++)
                            {
                                TMsetBools[bool_j] = TMSets[pkmn_i, bool_j];
                            }
                            byte[] TMsetBytes = ROM_FileStream.TMBytesFromBools(TMsetBools);
                            ROM_File.WriteBytes(TMsetBytes, offset[TM_SET_I] + 0x20 * (pkmn_i - 1));
                        }
                    }
                    #endregion
                    #region TRAINER
                    if (JumpToIfSaving(TR_GROUP_NAME_I)) trGroupNames.WriteToFile(ROM_File);

                    if (JumpToIfSaving(TR_GROUP_DV_I))
                    {
                        foreach (byte DV in trGroupDVs)
                        {
                            ROM_File.WriteByte(DV);
                        }
                    }

                    if (JumpToIfSaving(TR_GROUP_ATTRIBUTE_I))
                    {
                        for (int tc_i = trGroupNames.start_i; tc_i <= trGroupNames.end_i; tc_i++)
                        {
                            ROM_File.WriteByte(trGroupItems[2 * tc_i]);
                            ROM_File.WriteByte(trGroupItems[2 * tc_i + 1]);
                            ROM_File.WriteByte(trGroupRewards[tc_i]);
                            ROM_File.Position += 4; // skip AI behavior bytes
                        }
                    }

                    if (JumpToIfSaving(TRAINER_PTR_I)) trainerLists.WriteToFile(ROM_File);

                    #endregion
                    if (JumpToIfSaving(ANIM_PTR_I)) animations.WriteToFile(ROM_File);
                    #region WILD
                    if (JumpToIfSaving(WILD_I))
                    {
                        ROM_File.WriteWildAreaList(johtoLand);
                        ROM_File.WriteWildAreaList(johtoWater);
                        ROM_File.WriteWildAreaList(kantoLand);
                        ROM_File.WriteWildAreaList(kantoWater);
                        ROM_File.WriteWildAreaList(swarm);
                    }
                    #endregion

                }

                new FormMessage("Saved to " + sfdROM.FileName).Show();
            }
        }

        private void LoadFromTxt()
        {
            using (var ofdData = new OpenFileDialog
            {
                Filter = ".WGen2_data.txt|*.WGen2_data.txt|All Files|*",
                Title = "Open data file",
                InitialDirectory = Application.StartupPath
            })
            {
                if (ofdData.ShowDialog() != DialogResult.OK) return;

                var dataStrings = new List<string>();
                foreach (string s in File.ReadLines(ofdData.FileName)) dataStrings.Add(s);

                try
                {
                    ImportData(dataStrings);

                    exportData_TSMI.Enabled = true;
                    managePtrs_TSMI.Enabled = true;
                    EnableDataEntry();
                    EnableWrite();
                    UpdateEditor();
                }
                catch (Exception e)
                {
                    new FormMessage("Bad data file: " + ofdData.FileName + Environment.NewLine + e.Message + e.GetType().ToString()).Show();
                }
            }
        }

        private void SaveToTxt()
        {
            using (var sfdData = new SaveFileDialog
            {
                Filter = ".WGen2_data.txt|*.WGen2_data.txt|All Files|*",
                Title = "Save data file",
                InitialDirectory = Application.StartupPath
            })
            {
                if (sfdData.ShowDialog() != DialogResult.OK) return;

                using (var file = new System.IO.StreamWriter(sfdData.FileName))
                {
                    ExportData(file);
                }
            }
        }

        protected virtual void EnableDataEntry() { }
        protected virtual void EnableWrite() { }
        protected virtual void UpdateEditor() { }
        protected virtual void ImportData(List<string> dataStrings) { }
        protected virtual void ExportData(System.IO.StreamWriter file) { }
        protected virtual void ManagePointers() { }

        protected void BadParse(TextBox tb)
        {
            tb.BackColor = System.Drawing.Color.FromArgb(255, 191, 191);
            saveROM_TSMI.Enabled = false;
        }

        protected void PrintWarningIfTooLong(string name, int maxLength) // TODO color red?
        {
            if (name.Length > maxLength)
            {
                new FormMessage("Warning: " + Environment.NewLine + name 
                    + " is longer than " + maxLength + " characters").Show();
            }
        }

        protected void PopulateComboBox(ComboBox CB, string[] strings)
        {
            CB.Items.Clear();
            foreach (string s in strings)
            {
                if (s == null) CB.Items.Add("-");
                else CB.Items.Add(s);
            }
        }

        // duplicating this function is simpler than fiddling with array type conversion
        protected void PopulateComboBox(ComboBox CB, DataBlock<DBString> DB, int increment = 1)
        {
            CB.Items.Clear();
            for (int name_i = 0; name_i <= DB.end_i; name_i += increment)
            {
                if (DB.data[name_i] == null || DB.data[name_i] == ROM_FileStream.INVALID_STRING)
                    CB.Items.Add("-");
                else
                    CB.Items.Add((string)DB.data[name_i]);
            }
        }
    }

    static class Program
    {
        static void Main()
        {
            _ = new Gen2Editor();
        }
    }
}
