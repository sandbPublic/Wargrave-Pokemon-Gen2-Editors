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

namespace Gen2_Move_Editor
{
    public partial class MoveEditor : Editor_Base_Class.Gen2Editor
    {
        #region DEFAULT_NAMES
        private readonly string[] DEFAULT_MOVE_NAMES = new string[256]{
"Null", "Pound", "Karate Chop", "Double Slap",
"Comet Punch", "Mega Punch", "Pay Day", "Fire Punch",
"Ice Punch", "Thunder Punch", "Scratch", "Vice Grip",
"Guillotine", "Razor Wind", "Swords Dance", "Cut",
//0x10
"Gust", "Wing Attack", "Whirlwind", "Fly",
"Bind", "Slam", "Vine Whip", "Stomp",
"Double Kick", "Mega Kick", "Jump Kick", "Rolling Kick",
"Sand Attack", "Headbutt", "Horn Attack", "Fury Attack",
//2
"Horn Drill", "Tackle", "Body Slam", "Wrap",
"Take Down", "Thrash", "Double Edge", "Tail Whip",
"Poison Sting", "Twineedle", "Pin Missile", "Leer",
"Bite", "Growl", "Roar", "Sing",
//3
"Supersonic", "Sonicboom", "Disable", "Acid",
"Ember", "Flamethrower", "Mist", "Water Gun",
"Hydro Pump", "Surf", "Ice Beam", "Blizzard",
"Psybeam", "Bubblebeam", "Aurora Beam", "Hyper Beam",
//4
"Peck", "Drill Peck", "Submission", "Low Kick",
"Counter", "Seismic Toss", "Strength", "Absorb",
"Mega Drain", "Leech Seed", "Growth", "Razor Leaf",
"Solar Beam", "Poisonpowder", "Stun Spore", "Sleep Powder",
//5
"Petal Dance", "String Shot", "Dragon Rage", "Fire Spin",
"Thundershock", "Thunderbolt", "Thunder Wave", "Thunder",
"Rock Throw", "Earthquake", "Fissure", "Dig",
"Toxic", "Confusion", "Psychic", "Hypnosis",
//6
"Meditate", "Agility", "Quick Attack", "Rage",
"Teleport", "Night Shade", "Mimic", "Screech",
"Double Team", "Recover", "Harden", "Minimize",
"Smoke Screen", "Confuse Ray", "Withdraw", "Defense Curl",
//7
"Barrier", "Light Screen", "Haze", "Reflect",
"Focus Energy", "Bide", "Metronome", "Mirror Move",
"Selfdestruct", "Egg Bomb", "Lick", "Smog",
"Sludge", "Bone Club", "Fire Blast", "Waterfall",
//8
"Clamp", "Swift", "Skull Bash", "Spike Cannon",
"Constrict", "Amnesia", "Kinesis", "Softboiled",
"Hi Jump Kick", "Glare", "Dream Eater", "Poison Gas",
"Barrage", "Leech Life", "Lovely Kiss", "Sky Attack",
//9
"Transform", "Bubble", "Dizzy Punch", "Spore",
"Flash", "Psywave", "Splash", "Acid Armor",
"Crabhammer", "Exposion", "Fury Swipes", "Bonemerang",
"Rest", "Rock Slide", "Hyper Fang", "Sharpen",
//A
"Conversion", "Tri Attack", "Super Fang", "Slash",
"Substitute", "Struggle", "Sketch", "Triple Kick",
"Thief", "Spider Web", "Mind Reader", "Nightmare",
"Flame Wheel", "Snore", "Curse", "Flail",
//B
"Conversion2", "Aeroblast", "Cotton Spore", "Reversal",
"Spite", "Powder Snow", "Protect", "Mach Punch",
"Scary Face", "Faint Attack", "Sweet Kiss", "Belly Drum",
"Sludge Bomb", "Mud-Slap", "Octazooka", "Spikes",
//C
"Zap Cannon", "Foresight", "Destiny Bond", "Perish Song",
"Icy Wind", "Detect", "Bone Rush", "Lock-On",
"Outrage", "Sandstorm", "Giga Drain", "Endure",
"Charm", "Rollout", "False Swipe", "Swagger",
//D
"Milk Drink", "Spark", "Fury Cutter", "Steel Wing",
"Mean Look", "Attract", "Sleep Talk", "Heal Bell",
"Return", "Present", "Frustration", "Safeguard",
"Pain Split", "Sacred Fire", "Magnitude", "Dynamicpunch",
//E
"Megahorn", "Dragonbreath", "Baton Pass", "Encore",
"Pursuit", "Rapid Spin", "Sweet Scent", "Iron Tail",
"Metal Claw", "Vital Throw", "Morning Sun", "Synthesis",
"Moonlight", "Hidden Power", "Cross Chop", "Twister",
//F
"Rain Dance", "Sunny Day", "Crunch", "Mirror Coat",
"Psych Up", "Extremespeed", "Ancientpower", "Shadow Ball",
"Future Sight", "Rock Smash", "Whirlpool", "Beat Up ",
"Null", "Null", "Null", "Null"};
        #endregion

        #region EFFECT_DESCS
        private readonly string[] effectDescs = new string[256]{
"None", "Sleeps foe", "% poison", "Drain HP",
"% burn", "% freeze", "% paralyze", "KO self, double power",
"Dream Eater", "Mirror Move", "Attack +1", "Defense +1",
"Speed +1", "Sp Atk +1", "Sp Def +1", "Accuracy +1", // sweet scent
//
"Evasion +1", "Never miss", "Foe's attack -1", "Foe's defense -1",
"Foe's speed -1", "Foe's Sp Atk -1", "Foe's Sp Def -1", "Foe's accuracy -1",
"Foe's evasion -1", "Cancel stat changes", "Bide", "Attack 2-3 turns, then confuses self",
"Force foe to switch/flee", "2-5 hits", "Conversion", "% flinch", 
//0x20
"Recover 1/2 or rest", "Badly poisons foe", "Pay Day", "Light Screen",
"% burn, freeze, or paralyze foe", "? 0x25", "1HKO", "Razor Wind",
"50% damage", "Damage = power", "Trap for 2-5 turns & 1/16 damage at end of turn", "? 0x2B Damaging",
"2 hits", "1/8 recoil if attack fails", "Immune to stat reduction for 5 turns", "Buff crit ratio +1",
//
"1/4 recoil", "Confuses foe", "Attack +2", "Defense +2",
"Speed +2", "Sp Atk +2", "Sp Def +2", "Accuracy +2",
"Evasion +2", "Transform", "Foe's attack -2", "Foe's defense -2",
"Foe's speed -2", "Foe's Sp Atk -2", "Foe's Sp Def -2", "Foe's accuracy -2", 
//0x40
"Foe's evasion -2", "Reflect", "Poisons foe", "Paralyzes foe",
"% foe's attack -1", "% foe's defense -1", "% foe's speed -1", "% foe's Sp Atk -1",
"% foe's Sp Def -1", "% foe's accuracy -1", "% foe's evasion -1", "Sky Attack",
"% confuse", "2 hits, % poison", "? 0x4E Damaging", "Substitute", 
//
"Charge 1st turn, attack 2nd", "Rage", "Mimic", "Metronome",
"Leech Seed", "Splash", "Disable", "Damage = level",
"Damage = [0.5~1.5] * level", "Counter", "Encore", "Pain Split",
"Only works when asleep", "Conversion2", "Next attack will hit", "Sketch", 
//0x60
"? 0x60 Non damaging", "Uses random move when asleep", "Destiny Bond", "Powers up at low HP",
"Lowers PP of foe's last move by 1-5", "Won't KO", "Cure team status", "Attacks first",
"Triple Kick", "Steal item", "Prevents switch / flee", "Nightmare",
"Thaw self and % burn", "Curse", "? 0x6E Damaging", "Protect",
//
"Spikes", "Foresight", "Perish Song", "Sandstorm",
"Endure", "Rollout", "Swagger", "Fury Cutter",
"Attract", "Power = happiness/2.5", "Present", "Power = (255-Happiness)/2.5",
"Immune to status for 5 turns", "Sacred Fire", "Magnitude", "Baton Pass",
// 0x80
"Pursuit", "Rapid Spin", "? 0x82 Damaging, Misses", "? 0x83",
"Morning Sun", "Synthesis", "Moonlight", "Hidden Power",
"Rain Dance", "Sunny Day", "% defense +1", "% attack +1",
"% all stats +1", "? 0x8D Non damaging flinch, can miss", "Belly Drum", "Copy foe's stat changes",
//
"Counter special", "1st turn defense +1, 2nd turn attack",
"% flinch, damage*2 if foe used Fly", "Damage*2 if foe used Dig",
"Future Sight", "Damage*2 if foe used Fly",
"% flinch, damage*2 if foe used Minimize", "Charges 1st turn or in sun",
"% paralyze, never miss in rain", "Flee battle", "Beat Up", "2 turn attack, 1st turn invulnerable",
"Defense +1, powers up Rollout", "?", "?", "?", // all by 9F?
//0xA0
"?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?",
"?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?",
"?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?",
"?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?",
"?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?",
"?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?", "?"
        };

        #endregion

        public MoveEditor()
        {
            InitializeComponent();

            int[] oTL = { CRIT_LIST_PTR_I, MOVE_STRUCT_I, MOVE_NAME_I,
                            MOVE_DESC_PTR_I, TYPE_NAME_PTR_I};
            int[] oTS = { CRIT_LIST_PTR_I, MOVE_STRUCT_I, MOVE_NAME_I, MOVE_DESC_PTR_I };
            InitOffsets(oTL, oTS);
        }

        protected override void EnableDataEntry()
        {
            spinMoveID.Maximum = offset[NUM_OF_MOVES_I];

            PopulateComboBox(cboxType, typeNames);

            spinMoveID.Enabled = true;
            tboxName.Enabled = true;
            tboxDesc.Enabled = true;
            cboxCrit.Enabled = true;
            cboxType.Enabled = true;
            spinAccuracy.Enabled = true;
            spinAnimation.Enabled = true;
            spinEffect.Enabled = true;
            spinEffectChance.Enabled = true;
            spinPower.Enabled = true;
            spinPP.Enabled = true;
        }

        protected override void EnableWrite()
        {
            tboxDeltaNameChars.Text = moveNames.BytesFreeAt(sMV()).ToString();
            tboxDeltaDescChars.Text = moveDescs.BytesFreeAt(sMV()).ToString();
            tboxDeltaCritBytes.Text = (CritBytesAvailable() - CritBytesUsed()).ToString();

            saveROM_TSMI.Enabled = moveNames.BytesFreeAt(0) >= 0 &&
                moveDescs.BytesOverlapAt() == -1 && CritBytesOK();
        }

        protected override void UpdateEditor()
        {
            spinAnimation.Value = moves[sMV(), ANIMATION_I];
            spinEffect.Value = moves[sMV(), EFFECT_I];
            tboxEffect.Text = effectDescs[moves[sMV(), EFFECT_I]];
            spinPower.Value = moves[sMV(), POWER_I];
            cboxType.Text = typeNames.data[moves[sMV(), TYPE_I]];
            spinAccuracy.Value = moves[sMV(), ACCURACY_I];
            spinPP.Value = moves[sMV(), PP_I];
            spinEffectChance.Value = moves[sMV(), EFFECT_CHANCE_I];

            tboxName.Text = moveNames.data[sMV()];
            tboxDesc.Text = moveDescs.data[sMV()];
            cboxCrit.Checked = moveIsCrit[sMV()];

            EnableWrite();
        }

        protected override void ImportData(List<string> dataStrings)
        {
            foreach (int move_i in moveNames.Range())
            {
                int stringIndex = 4 * (move_i - moveNames.start_i);

                moveNames.data[move_i] = dataStrings[stringIndex];
                moveDescs.data[move_i] = dataStrings[stringIndex + 1];

                string[] dataStruct = dataStrings[stringIndex + 2].Split(' ');

                if (dataStruct.Length == 9)
                {
                    moveDescs.SetRelativePtr(move_i, Convert.ToInt32(dataStruct[0]));

                    moveIsCrit[move_i] = (dataStruct[1] == "1");
                    moves[move_i, ANIMATION_I] = Convert.ToByte(dataStruct[2]);
                    moves[move_i, EFFECT_I] = Convert.ToByte(dataStruct[3]);
                    moves[move_i, POWER_I] = Convert.ToByte(dataStruct[4]);
                    moves[move_i, TYPE_I] = Convert.ToByte(dataStruct[5]);
                    moves[move_i, ACCURACY_I] = Convert.ToByte(dataStruct[6]);
                    moves[move_i, PP_I] = Convert.ToByte(dataStruct[7]);
                    moves[move_i, EFFECT_CHANCE_I] = Convert.ToByte(dataStruct[8]);
                }
            }
            moveDescs.MakeContiguous();
        }

        protected override void ExportData()
        {
            var file = new System.IO.StreamWriter(data_FilePath);

            foreach (int move_i in moveNames.Range())
            {
                file.WriteLine(moveNames.data[move_i]);
                file.WriteLine(moveDescs.data[move_i]);

                string dataStruct =
                    moveDescs.RelativePtr(move_i) + " " // ptrs
                    + (moveIsCrit[move_i] ? "1" : "0") + " "
                    + moves[move_i, ANIMATION_I] + " "
                    + moves[move_i, EFFECT_I] + " "
                    + moves[move_i, POWER_I] + " "
                    + moves[move_i, TYPE_I] + " "
                    + moves[move_i, ACCURACY_I] + " "
                    + moves[move_i, PP_I] + " "
                    + moves[move_i, EFFECT_CHANCE_I];

                file.WriteLine(dataStruct);
                file.WriteLine("");
            }

            file.Dispose();
        }

        protected override void ManagePointers()
        {
            new PointerManager<DBString>(moveDescs).Show();
        }



        private int sMV()
        {
            return (int)spinMoveID.Value;
        }

        private void SpinMoveID_ValueChanged(object sender, EventArgs e)
        {
            if (spinMoveID.Focused) UpdateEditor();
        }
        private void CboxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            moves[sMV(), TYPE_I] = (byte)cboxType.SelectedIndex;
        }
        private void SpinEffect_ValueChanged(object sender, EventArgs e)
        {
            tboxEffect.Text = effectDescs[(int)spinEffect.Value];
            moves[sMV(), EFFECT_I] = (byte)spinEffect.Value;
        }
        private void SpinPower_ValueChanged(object sender, EventArgs e)
        {
            moves[sMV(), POWER_I] = (byte)spinPower.Value;
        }
        private void SpinPP_ValueChanged(object sender, EventArgs e)
        {
            moves[sMV(), PP_I] = (byte)spinPP.Value;
        }
        private void SpinAccuracy_ValueChanged(object sender, EventArgs e)
        {
            tboxAccuracy.Text = (100 * spinAccuracy.Value / 255).ToString("n2") + "%";
            moves[sMV(), ACCURACY_I] = (byte)spinAccuracy.Value;
        }
        private void SpinEffectChance_ValueChanged(object sender, EventArgs e)
        {
            tboxEffectChance.Text = (100 * spinEffectChance.Value / 255).ToString("n2") + "%";
            moves[sMV(), EFFECT_CHANCE_I] = (byte)spinEffectChance.Value;
        }
        private void SpinAnimation_ValueChanged(object sender, EventArgs e)
        {
            tboxAnimation.Text = DEFAULT_MOVE_NAMES[(int)spinAnimation.Value];
            moves[sMV(), ANIMATION_I] = (byte)spinAnimation.Value;
        }
        private void CboxCrit_CheckedChanged(object sender, EventArgs e)
        {
            moveIsCrit[sMV()] = cboxCrit.Checked;
            EnableWrite();
        }

        private void TboxMoveName_TextChanged(object sender, EventArgs e)
        {
            moveNames.data[sMV()] = tboxName.Text;
            EnableWrite();

            PrintWarningIfTooLong(tboxName.Text, 12);
        }

        private void TboxMoveDesc_TextChanged(object sender, EventArgs e)
        {
            moveDescs.data[sMV()] = tboxDesc.Text;
            moveDescs.UpdatePtrs(sMV());
            EnableWrite();

            string[] splitDesc = tboxDesc.Text.Split('|');
            PrintWarningIfTooLong(splitDesc[0], 18);
            if (splitDesc.Length >= 2) PrintWarningIfTooLong(splitDesc[1], 18);
        }
    }
}