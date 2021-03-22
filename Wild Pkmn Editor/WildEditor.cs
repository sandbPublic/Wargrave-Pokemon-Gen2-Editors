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

/* from GSC Wild Pokemon Editor/source/main.pas line 446, comments mine
      // Crystal offests
      case Kind of
        0: PkmnStart := $2A5E9; // johto land, last @2B0ED? ends with FF!
 * // then johto water begins! only need one offset? look for pointers
 * gbcPtr: E965
 * 
        1: PkmnStart := $2B274; // kanto land, ptr 7472
        2: PkmnStart := $2B11D; // johto water, ptr 1D71
        3: PkmnStart := $2B7F7; // kanto water, ptr F777
        4: PkmnStart := $2B8D0; // swarm, ptr D078

      // Gold/Silver Offsets
      case Kind of
        0: PkmnStart := $2AB35;
        1: PkmnStart := $2B7C0;
        2: PkmnStart := $2B669;
        3: PkmnStart := $2BD43;
        4: PkmnStart := $2BE1C;
        5: PkmnStart := $2BED9; // union cave special?
        end;
    end;
 * 
 * // data stored as such in rom at pointed locations:
 * five header bytes: Bank NUm m,d,n freq
 * 2*7*3=42 data bytes, level+species 2, seven slots 14, three times 42
 * */

namespace Gen2_Wild_Pkmn_Editor
{
    public partial class WildEditor : Editor_Base_Class.Gen2Editor
    {
        public WildEditor()
        {
            InitializeComponent();

            int[] oTL = { PKMN_NAME_I, WILD_I, MOVESET_PTR_I, AREA_NAME_PTR_I };
            int[] oTS = { WILD_I };
            InitOffsets(oTL, oTS);
        }

        protected override void EnableDataEntry()
        {
            comboRegion.Enabled = true;
            comboArea.Enabled = true;
            comboVersion.Enabled = true;
            managePtrs_TSMI.Enabled = false;
            buttonAnalyze.Enabled = true;

            comboRegion.SelectedIndex = 0;
            comboVersion.SelectedIndex = 0;
            comboArea.SelectedIndex = 0;
        }

        protected override void EnableWrite()
        {
            saveROM_TSMI.Enabled = true;
        }

        protected override void UpdateEditor()
        {
            UpdateRegion();
            UpdateComboArea();
            UpdateArea();
        }

        private void ImportListAWD(List<string> dataStrings, List<AreaWildData> lAWD, ref int stringIndex, int count)
        {

            for (int area_i = 0; area_i < count; area_i++)
            {
                AreaWildData awd = new AreaWildData();

                string[] mapBankAndNum = dataStrings[stringIndex++].Split(' ');
                awd.mapBank = Convert.ToByte(mapBankAndNum[0]);
                awd.mapNum = Convert.ToByte(mapBankAndNum[1]);

                string[] freqs = dataStrings[stringIndex++].Split(' ');
                awd.water = (freqs.Length == 1);

                foreach (int time_i in awd.TimeRange()) awd.freq[time_i] = Convert.ToByte(freqs[time_i]);

                string[] levelAndSpecies = dataStrings[stringIndex++].Split(' ');
                foreach (int time_i in awd.TimeRange())
                {
                    foreach (int slot_j in awd.SlotRange())
                    {
                        int index = 2 * (time_i * awd.Slots() + slot_j);

                        awd.levels[time_i, slot_j] = Convert.ToByte(levelAndSpecies[index]);
                        awd.species[time_i, slot_j] = Convert.ToByte(levelAndSpecies[index + 1]);
                    }
                }

                stringIndex++;
                lAWD.Add(awd);
            }
        }

        protected override void ImportData(List<string> dataStrings)
        {
            List<int> counts = new List<int>();
            // get counts
            string[] firstLine = dataStrings[0].Split(' ');
            foreach (string s in firstLine) counts.Add(Convert.ToInt32(s));

            int stringIndex = 2;
            // read each list in order
            for (int region_i = 0; region_i < 5; region_i++)
            {
                AreaList(region_i).Clear();
                ImportListAWD(dataStrings, AreaList(region_i), ref stringIndex, counts[region_i]);
            }
        }

        private void ExportListAWD(System.IO.StreamWriter file, List<AreaWildData> lAWD)
        {
            // four lines per awd, one blank
            foreach (AreaWildData awd in lAWD)
            {
                file.WriteLine(awd.mapBank + " " + awd.mapNum);
                string freqs = "";
                foreach (int time_i in awd.TimeRange())
                {
                    freqs += (time_i == 0 ? "" : " ") + awd.freq[time_i];
                }
                file.WriteLine(freqs);
                string data = "";
                foreach (int time_i in awd.TimeRange())
                {
                    foreach (int slot_j in awd.SlotRange())
                    {
                        data += (time_i + slot_j == 0 ? "" : " ") +
                            awd.levels[time_i, slot_j] + " " +
                            awd.species[time_i, slot_j];
                    }
                }
                file.WriteLine(data);
                file.WriteLine("");
            }
        }

        protected override void ExportData()
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(data_FilePath);

            // export list of counts
            string counts = johtoLand.Count + " " + johtoWater.Count + " "
                + kantoLand.Count + " " + kantoWater.Count + " " + swarm.Count;

            file.WriteLine(counts);
            file.WriteLine("");

            // then export lists
            for (int region_i = 0; region_i < 5; region_i++) ExportListAWD(file, AreaList(region_i));

            file.Dispose();
        }

        //protected override void ManagePointers() { }

        private List<AreaWildData> AreaList(int region_i)
        {
            switch (region_i)
            {
                default: return johtoLand;
                case 1: return johtoWater;
                case 2: return kantoLand;
                case 3: return kantoWater;
                case 4: return swarm;
            }
        }

        private int sRegion_i()
        {
            return (comboRegion.SelectedIndex < 0 ? 0 : comboRegion.SelectedIndex);
        }
        private List<AreaWildData> sList()
        {
            return AreaList(sRegion_i());
        }

        private void UpdateRegion()
        {
            comboArea.Items.Clear();
            for (int area_i = 0; area_i < sList().Count; area_i++) comboArea.Items.Add(area_i.ToString());
            TextPkmn(0).BackColor = System.Drawing.SystemColors.Window;
            TextPkmn(1).BackColor = System.Drawing.SystemColors.Window;
            TextPkmn(2).BackColor = System.Drawing.SystemColors.Window;
        }

        private void ComboRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEditor();
        }

        private int sArea_i()
        {
            return (comboArea.SelectedIndex < 0 ? 0 : comboArea.SelectedIndex);
        }
        private AreaWildData sArea()
        {
            return sList()[sArea_i()];
        }
        private NumericUpDown SpinFreq(int time_i)
        {
            switch (time_i)
            {
                default: return spinMornFreq;
                case 1: return spinDayFreq;
                case 2: return spinNightFreq;
            }
        }
        private TextBox TextPkmn(int time_i)
        {
            switch (time_i)
            {
                default: return textPkmnMorn;
                case 1: return textPkmnDay;
                case 2: return textPkmnNight;
            }
        }

        private const char SPLITING_CHAR = ':';
        private bool pauseParsing = false;
        private void UpdateAreaTime(int time_i)
        {
            pauseParsing = true;
            SpinFreq(time_i).Value = sArea().freq[time_i];
            SpinFreq(time_i).Enabled = true;

            ButtonLevel(0, time_i).Enabled = true;
            ButtonLevel(1, time_i).Enabled = true;

            // load text Pkmn
            TextPkmn(time_i).Clear();
            TextPkmn(time_i).Enabled = true;
            foreach (int slot_j in sArea().SlotRange())
            {
                byte level = sArea().levels[time_i, slot_j];
                byte species = sArea().species[time_i, slot_j];

                TextPkmn(time_i).Text += (slot_j == 0 ? "" : Environment.NewLine)
                    + level.ToString("D3") + SPLITING_CHAR + pkmnNames[species];
            }
            pauseParsing = false;
        }

        private void UpdateArea()
        {
            textMapBank.Text = sArea().mapBank.ToString("X2");
            textMapNum.Text = sArea().mapNum.ToString("X2");

            for (int time_i = 0; time_i < 3; time_i++)
            {
                SpinFreq(time_i).Enabled = false;
                TextPkmn(time_i).Enabled = false;
                ButtonLevel(0, time_i).Enabled = false;
                ButtonLevel(1, time_i).Enabled = false;
            }
            foreach (int time_i in sArea().TimeRange()) UpdateAreaTime(time_i);
        }

        private void ComboArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateArea();
        }

        private void SpinMornFreq_ValueChanged(object sender, EventArgs e)
        {
            sArea().freq[0] = (byte)spinMornFreq.Value;
        }
        private void SpinDayFreq_ValueChanged(object sender, EventArgs e)
        {
            sArea().freq[1] = (byte)spinDayFreq.Value;
        }
        private void SpinNightFreq_ValueChanged(object sender, EventArgs e)
        {
            sArea().freq[2] = (byte)spinNightFreq.Value;
        }

        private void TextPkmnMorn_TextChanged(object sender, EventArgs e)
        {
            UpdatePkmnTbox(textPkmnMorn, 0);
        }
        private void TextPkmnDay_TextChanged(object sender, EventArgs e)
        {
            UpdatePkmnTbox(textPkmnDay, 1);
        }
        private void TextPkmnNight_TextChanged(object sender, EventArgs e)
        {
            UpdatePkmnTbox(textPkmnNight, 2);
        }
        private void UpdatePkmnTbox(TextBox tb, int time_i)
        {
            if (tb.Focused && !pauseParsing)
            {//attempt to parse
                int length = tb.Lines.Length;
                if (length != sArea().Slots())
                {
                    BadParse(tb); 
                    return;
                }

                byte[] areaLevels = new byte[length];
                byte[] areaSpecies = new byte[length];

                for (int move_i = 0; move_i < length; move_i++)
                {
                    string[] areaStrs = tb.Lines[move_i].Split(SPLITING_CHAR);

                    if (areaStrs.Length == 2)
                    {
                        byte i = 0;
                        if (byte.TryParse(areaStrs[0], out i)) areaLevels[move_i] = i;
                        else
                        {
                            BadParse(tb);
                            return;
                        }

                        bool nameParsed = false;
                        for (int pkmnName_i = 1; pkmnName_i <= offset[NUM_OF_PKMN_I]; pkmnName_i++)
                        {
                            if (pkmnNames[pkmnName_i] == areaStrs[1])
                            {
                                areaSpecies[move_i] = (byte)pkmnName_i;
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
                for (int slot_i = 0; slot_i < length; slot_i++)
                {
                    sArea().levels[time_i, slot_i] = areaLevels[slot_i];
                    sArea().species[time_i, slot_i] = areaSpecies[slot_i];
                }
                EnableWrite();
            }
        }

        private void ButtonAnalyze_Click(object sender, EventArgs e)
        {
            // iterate through each List Area to count how often each pokemon shows up
            int[] rarity = new int[pkmnNames.Length];
            // select Johto or Kanto
            int minRegion = (sRegion_i() <= 1 ? 0 : 2);
            int maxRegion = (sRegion_i() <= 1 ? 1 : 3);

            for (int region_i = minRegion; region_i <= maxRegion; region_i++)
            {
                foreach (AreaWildData awd in AreaList(region_i))
                {
                    foreach (int time_j in awd.TimeRange())
                    {
                        foreach (int slot_k in awd.SlotRange())
                        {
                            rarity[awd.species[time_j, slot_k]] +=
                                awd.Duration(slot_k) * awd.freq[time_j] *
                                (awd.water ? 7 : 3);// weight because water routes have only 3 slots
                        }
                    }
                }
            }

            // evolutionary families
            // for each pokemon
            // if nothing evolves into it, add rarity from everything in it's tree

            List<SortingString> L_ss = new List<SortingString>();
            bool[] rarityPassed = new bool[pkmnNames.Length];
            for (int pkName_i = 1; pkName_i < pkmnNames.Length; pkName_i++)
            {
                // get rarity from evolutions
                for (int evo_j = 1; evo_j < pkmnNames.Length; evo_j++)
                {
                    foreach (EvoData ed in movesets.data[pkName_i].evoList)
                    {
                        if (ed.species == evo_j && !rarityPassed[evo_j])
                        {
                            rarity[pkName_i] += rarity[evo_j];
                            rarityPassed[evo_j] = true;
                        }
                    }
                }
            }
            // do this seperately, second so that evos that precede their prevos
            // in pokedex order aren't counted as seperate families
            for (int pkName_i = 1; pkName_i < pkmnNames.Length; pkName_i++)
            {
                SortingString ss = new SortingString();
                if (!rarityPassed[pkName_i])
                {
                    ss.sortValue = rarity[pkName_i];
                    ss.me = rarity[pkName_i].ToString("D6") + " - " + pkmnNames[pkName_i];
                    L_ss.Add(ss);
                }
            }

            new FormAnalysis(L_ss).Show();
        }

        #region AREA NAMES
        private readonly string[] JL_AreaNames = {
    "Sprout Tower 2","Sprout Tower 3", "Tin Tower 2","Tin Tower 3",
    "Tin Tower 4","Tin Tower 5","Tin Tower 6","Tin Tower 7",
    "Tin Tower 8","Tin Tower 9","Burned Tower 1","Burned Tower 2",
    "National Park","Ruins of Alph Outer","Ruins of Alph Inner",
    "Union Cave 1","Union Cave 2","Union Cave 3","Slowpoke Well 1",
    "Slowpoke Well 2","Ilex Forest","Mt. Mortar 1","Mt. Mortar 2",
    "Mt. Mortar 3","Mt. Mortar 4","Ice Path 1","Ice Path 2",
    "Ice Path 3","Ice Path 4","Ice Path 5","Whirl Islands 1",
    "Whirl Islands 2","Whirl Islands 3","Whirl Islands 4",
    "Whirl Islands 5","Whirl Islands 6","Whirl Islands 7",
    "Whirl Islands 8","Silver Cave 1","Silver Cave 2","Silver Cave 3",
    "Silver Cave 4","Dark Cave 1","Dark Cave 2","Route 29","Route 30",
    "Route 31","Route 32","Route 33","Route 34","Route 35","Route 36",
    "Route 37","Route 38","Route 39","Route 42","Route 43","Route 44",
    "Route 45","Route 46","Silver Cave Outer"};

        private readonly string[] JWGS_AreaNames = {
    "Violet City","Union Cave 1","Union Cave 2","Union Cave 3",
    "Slowpoke Well 1","Slowpoke Well 2","Ilex Forest",
    "Mt. Mortar 1","Mt. Mortar 2","Mt. Mortar 4",
    "Whirl Islands 3","Whirl Islands 7","Whirl Islands 8",
    "Silver Cave 2","Dark Cave 1","Dark Cave 2","Dragon's Den",
    "Route 30","Route 31","Route 32","Route 34","Route 35","Route 40",
    "Route 41","Route 42","Route 43","Route 44","Route 45",
    "New Bark Town","Cherrygrove City","Violet City","Cianwood City",
    "Olivine City","Ecruteak City","Lake of Rage","Blackthorn City",
    "Silver Cave","Olivine City Dock" };

        private readonly string[] JWC_AreaNames = {
    "Ruins of Alph Outer","Union Cave 1","Union Cave B1","Union Cave B2",
    "Slowpoke Well B1","Slowpoke Well B2","Ilex Forest","Mt. Mortar 1",
    "Mt. Mortar 3","Mt. Mortar 4","Whirl Islands 3","Whirl Islands 7",
    "Whirl Islands 8","Silver Cave 2","Dark Cave 1","Dark Cave 2",
    "Dragon's Den 2","Olivine City Port","Route 30","Route 31",
    "Route 32","Route 34","Route 35","Route 40","Route 41","Route 42",
    "Route 43","Route 44","Route 45","New Bark Town","Cherrygrove City",
    "Violet City","Cianwood City","Olivine City","Ecruteak City","Lake of Rage",
    "Blackthorn City","Silver Cave Outer" };

        private readonly string[] KL_AreaNames = {
    "Diglett's Cave","Mt. Moon","Rock Tunnel 1","Rock Tunnel 2",
    "Victory Road","Tohjo Falls","Route 1","Route 2","Route 3","Route 4",
    "Route 5","Route 6","Route 7","Route 8","Route 9","Route 10","Route 11",
    "Route 13","Route 14","Route 15","Route 16","Route 17","Route 18",
    "Route 21","Route 22","Route 24","Route 25","Route 26","Route 27",
    "Route 28" };

        private readonly string[] KWGS_AreaNames = {
    "Route 4","Route 6","Route 9","Route 10","Route 12","Route 13","Route 19",
    "Route 20","Route 21","Route 22","Route 24","Route 25","Route 26","Route 27",
    "Tohjo Falls","Route 28","Pallet Town","Viridian City","Cerulean City",
    "Vermillion City","Celadon City","Fuschia City","Cinnibar Island",
    "Vermillion City Dock" };

        private readonly string[] KWC_AreaNames = {
    "Tohjo Falls","Vermillion City Dock","Route 4","Route 6","Route 9",
    "Route 10","Route 12","Route 13","Route 19","Route 20","Route 21",
    "Route 22","Route 24","Route 25","Route 26","Route 27","Route 28",
    "Pallet Town","Viridian City","Cerulean City","Vermillion City",
    "Celadon City","Fuchsia City","Cinnabar Island" };

        private readonly string[] SwGS_AreaNames = {
    "Route 35","Route 38","Dark Cave","Mt. Mortar" };
        private readonly string[] SwC_AreaNames = {
    "Dark Cave","Route 35" };
        #endregion

        private void UpdateComboArea()
        {
            // clear area name list
            // fill with names
            comboArea.Items.Clear();

            string[] defaultNames = new string[0];
            if (sRegion_i() == 0) defaultNames = JL_AreaNames; // JohtoLand
            else if (sRegion_i() == 1) // JohtoWater
            { 
                if (comboVersion.SelectedIndex == 0) defaultNames = JWC_AreaNames; // Crystal
                else if (comboVersion.SelectedIndex == 1) defaultNames = JWGS_AreaNames; // Gold/Silver
            }
            else if (sRegion_i() == 2) defaultNames = KL_AreaNames; // KantoLand
            else if (sRegion_i() == 3) // KantoWater
            { 
                if (comboVersion.SelectedIndex == 0) defaultNames = KWC_AreaNames;
                else if (comboVersion.SelectedIndex == 1) defaultNames = KWGS_AreaNames;
            }
            else if (sRegion_i() == 4) // Swarm
            { 
                if (comboVersion.SelectedIndex == 0) defaultNames = SwC_AreaNames;
                else if (comboVersion.SelectedIndex == 1) defaultNames = SwGS_AreaNames;
            }

            for (int area_i = 0; area_i < sList().Count; area_i++)
            {
                if (area_i < defaultNames.Length) comboArea.Items.Add(defaultNames[area_i]);
                else comboArea.Items.Add(area_i.ToString("X3"));
            }
        }

        private void ComboVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateComboArea();
        }

        private void ChangeLevels(int deltaLevel, int time_i)
        {
            foreach (int slot_j in sArea().SlotRange())
            {
                sArea().levels[time_i, slot_j] = (byte)(deltaLevel
                    + sArea().levels[time_i, slot_j]); // awkward, but can't pass -1 as a byte
            }
            UpdateAreaTime(time_i);
        }

        private Button ButtonLevel(int inc_i, int time_j)
        {
            switch (inc_i + 2 * time_j)
            {
                default: return buttonIncLevelsMorn;
                case 1: return buttonDecLevelsMorn;
                case 2: return buttonIncLevelsDay;
                case 3: return buttonDecLevelsDay;
                case 4: return buttonIncLevelsNight;
                case 5: return buttonDecLevelsNight;
            }
        }
        private void ButtonIncLevelsMorn_Click(object sender, EventArgs e)
        {
            ChangeLevels(1, 0);
        }
        private void ButtonDecLevelsMorn_Click(object sender, EventArgs e)
        {
            ChangeLevels(-1, 0);
        }
        private void ButtonIncLevelsDay_Click(object sender, EventArgs e)
        {
            ChangeLevels(1, 1);
        }
        private void ButtonDecLevelsDay_Click(object sender, EventArgs e)
        {
            ChangeLevels(-1, 1);
        }
        private void ButtonIncLevelsNight_Click(object sender, EventArgs e)
        {
            ChangeLevels(1, 2);
        }
        private void ButtonDecLevelsNight_Click(object sender, EventArgs e)
        {
            ChangeLevels(-1, 2);
        }
    }
}
