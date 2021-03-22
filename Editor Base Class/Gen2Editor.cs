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
        protected OpenFileDialog ofdOffsets;
        protected string offsets_FilePath;

        protected OpenFileDialog ofdROM;
        public ROM_FileStream ROM_File;
        protected string ROM_FilePath;

        protected OpenFileDialog ofdData;
        protected SaveFileDialog sfdData;
        protected string data_FilePath;

        public Gen2Editor()
        {
            ofdOffsets = new OpenFileDialog
            {
                Filter = ".WGen2_offsets.txt|*.WGen2_offsets.txt|All Files|*",
                Title = "Open offset file",
                InitialDirectory = Application.StartupPath
            };

            ofdROM = new OpenFileDialog
            {
                Filter = ".gbc|*.gbc|All Files|*",
                Title = "Open ROM file",
                InitialDirectory = Application.StartupPath
            };

            ofdData = new OpenFileDialog
            {
                Filter = ".WGen2_data.txt|*.WGen2_data.txt|All Files|*",
                Title = "Open data file",
                InitialDirectory = Application.StartupPath
            };

            sfdData = new SaveFileDialog
            {
                Filter = ".WGen2_data.txt|*.WGen2_data.txt|All Files|*",
                Title = "Save data file",
                InitialDirectory = Application.StartupPath
            };

            InitializeComponent();

            offsetsToLoad = new List<int>();
        }

        private bool JumpToIfLoading(int offset_i)
        {
            if (loadOffset[offset_i]) ROM_File.Position = offset[offset_i];
            return loadOffset[offset_i];
        }

        public bool JumpToIfSaving(int offset_i)
        {
            if (saveOffset[offset_i]) ROM_File.Position = offset[offset_i];
            return saveOffset[offset_i];
        }

        private void LoadOffsets()
        {
            if (ofdOffsets.ShowDialog() == DialogResult.OK)
            {
                offsets_FilePath = ofdOffsets.FileName;
                ofdOffsets.Dispose();

                List<string> offsetsStrings = new List<string>();
                foreach (string s in File.ReadLines(offsets_FilePath))
                {
                    offsetsStrings.Add(s);
                }

                bool validOffsets = true;
                if (offsetsStrings.Count < NUM_OF_OFFSETS)
                {
                    FormMessage warning = new FormMessage(
                        "Warning: not enough lines in offset file." + Environment.NewLine
                        + offsets_FilePath + Environment.NewLine
                        + "Lines read: " + offsetsStrings.Count + Environment.NewLine
                        + "Lines expected: " + NUM_OF_OFFSETS);
                    warning.Show();

                    validOffsets = false;
                }

                for (int offset_i = 0; offset_i < NUM_OF_OFFSETS; offset_i++)
                {
                    try
                    {
                        string[] s = offsetsStrings[offset_i].Split(' '); // read first word == offset
                        offset[offset_i] = Convert.ToInt32(s[0], 16);
                    }
                    catch (Exception e)
                    { //FormatException
                        FormMessage exception = new FormMessage(
                            "Bad offset file: " + offsets_FilePath + Environment.NewLine
                            + e.Message + Environment.NewLine
                            + "Line# " + (offset_i + 1));
                        exception.Show();

                        validOffsets = false;
                    }
                }

                // do not allow the user to open a ROM 
                // if the offset file was invalid
                if (!validOffsets)
                {
                    offsets_FilePath = null;
                }
            }
        }

        private void LoadFrom(OpenFileDialog ofd, bool fromRom)
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                if (fromRom) ROM_FilePath = ofd.FileName;
                else data_FilePath = ofd.FileName;
                ofd.Dispose();

                if (fromRom)
                {
                    LoadFromRom();
                    exportData_TSMI.Enabled = true;
                    managePtrs_TSMI.Enabled = true;
                    EnableDataEntry();
                }
                else
                {
                    List<string> dataStrings = new List<string>();
                    foreach (string s in File.ReadLines(data_FilePath))
                    {
                        dataStrings.Add(s);
                    }

                    try
                    {
                        ImportData(dataStrings);
                    }
                    catch (Exception e)
                    { //FormatException, ArgumentOutOfRangeException
                        FormMessage exception = new FormMessage("Bad data file: "
                            + data_FilePath + Environment.NewLine + e.Message);
                        exception.Show();
                    }
                }
                EnableWrite();
                UpdateEditor();
            }
        }

        private void PopListArea(List<AreaWildData> area, bool water = false)
        {
            while (ROM_File.ReadByte() != 0xFF && area.Count <= 999)
            {
                ROM_File.Position--;
                area.Add(ROM_File.ReadWildArea(water));
            }
        }

        private void LoadFromRom()
        {
            ROM_File = new ROM_FileStream(ROM_FilePath, FileMode.Open);
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
                // fixed to length 10
                for (int pkmnName_i = 1; pkmnName_i <= offset[NUM_OF_PKMN_I]; pkmnName_i++)
                {
                    byte[] nameInBytes =
                        ROM_File.ReadBytes(offset[PKMN_NAME_I] + 10 * (pkmnName_i - 1), 10);

                    pkmnNames[pkmnName_i] = "";
                    foreach (byte b1 in nameInBytes)
                    {
                        if (b1 != 0x50) pkmnNames[pkmnName_i] += ROM_FileStream.PkmnByteToChar(b1);
                    }
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
            JumpToIfLoading(TR_CLASS_NAME_I);
            while (ROM_File.Position < offset[TR_CLASS_NAME_END_I] - 1)
            {
                if (ROM_File.ReadByte() == 0)
                {
                    break;
                }
                else
                {
                    ROM_File.Position--;
                    string s = ROM_File.PkmnReadString(20);
                    numOfTrC++;
                }
            }

            if (JumpToIfLoading(TR_CLASS_NAME_I))
            {
                trClassNames = new DataBlock<DBString>(offset[TR_CLASS_NAME_I],
                    offset[TR_CLASS_NAME_END_I], false, numOfTrC - 1, 0);
                trClassNames.ReadFromFile(ROM_File);
            }

            // dvs, 4 bits per dv
            if (JumpToIfLoading(TR_CLASS_DV_I))
            {
                trClassDVs = new List<byte>();
                for (int tc_i = trClassNames.start_i; tc_i <= trClassNames.end_i; tc_i++)
                {
                    trClassDVs.Add((byte)ROM_File.ReadByte()); // atkdef
                    trClassDVs.Add((byte)ROM_File.ReadByte()); // speedspec
                }
            }

            // items used and money
            if (JumpToIfLoading(TR_CLASS_ATTRIBUTE_I))
            {
                trClassItems = new List<byte>();
                trClassRewards = new List<byte>();
                for (int tc_i = trClassNames.start_i; tc_i <= trClassNames.end_i; tc_i++)
                {
                    trClassItems.Add((byte)ROM_File.ReadByte());
                    trClassItems.Add((byte)ROM_File.ReadByte());
                    trClassRewards.Add((byte)ROM_File.ReadByte());
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
            ROM_File.Dispose();
        }

        /// <summary>
        /// can disable menu items
        /// </summary>
        protected virtual void EnableDataEntry() { }
        protected virtual void EnableWrite() { }
        protected virtual void UpdateEditor() { }

        protected virtual void ImportData(List<string> dataStrings) { }
        protected virtual void ExportData() { }
        protected virtual void ManagePointers() { } //TODO implement here?

        protected void PrintWarningIfTooLong(string name, int maxLength)
        {
            if (name.Length > maxLength)
            {
                // TODO assignment needed?
                FormMessage msg = new FormMessage("Warning: " + Environment.NewLine +
                    name + " is longer than " + maxLength + " characters");
                msg.Show();
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
            Gen2Editor gsc = new Gen2Editor();
        }
    }
}
