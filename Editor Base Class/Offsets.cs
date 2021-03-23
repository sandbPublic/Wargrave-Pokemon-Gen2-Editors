using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System;
using System.IO; // open save read write files

namespace Editor_Base_Class
{
    partial class Gen2Editor
    {
        /// <summary>
        /// Index to arrays of offsets etc, basically enums
        /// </summary>
        protected const int
                ITEM_STRUCT_I = 0, ITEM_ASM_I = 1,
                TM_CODE_I = 2, TM_SET_I = 3,
                CRIT_LIST_PTR_I = 4, NEW_CRIT_LIST_I = 5, NEW_CRIT_LIST_END_I = 6,
                MOVE_STRUCT_I = 7,
                MOVESET_PTR_I = 8, MOVESET_END_I = 9,
                TYPE_NAME_PTR_I = 10,
                ITEM_NAME_I = 11, ITEM_NAME_END_I = 12,
                PKMN_NAME_I = 13,
                MOVE_NAME_I = 14, MOVE_NAME_END_I = 15,
                MOVE_DESC_PTR_I = 16, MOVE_DESC_END_I = 17,
                ITEM_DESC_PTR_I = 18, ITEM_DESC_END_I = 19,
                TRAINER_PTR_I = 20, TRAINER_END_I = 21,
                TR_GROUP_NAME_I = 22, TR_CLASS_NAME_END_I = 23, TR_GROUP_DV_I = 24, TR_GROUP_ATTRIBUTE_I = 25,
                ANIM_PTR_I = 26, ANIM_END_I = 27,
                WILD_I = 28, // wild pkmn lists
                AREA_NAME_PTR_I = 29, AREA_NAME_END_I = 30,

                NUM_OF_PKMN_I = 31, NUM_OF_MOVES_I = 32, NUM_OF_ITEMS_I = 33,
                LAST_NON_TM_ITEM_I = 34, NUM_OF_TYPES_I = 35, NUM_OF_ANIMS_I = 36,
                NUM_OF_AREA_NAMES_I = 37,

                NUM_OF_OFFSETS = 38;

        public int[] offset = new int[NUM_OF_OFFSETS];
        public bool[] loadOffset = new bool[NUM_OF_OFFSETS];
        public bool[] saveOffset = new bool[NUM_OF_OFFSETS];
        public List<int> offsetsToLoad; // don't load every offset for every editor

        /// <summary>
        /// DOES NOT LOAD OFFSET VALUES
        /// </summary>
        /// <param name="oTL">offsets to load</param>
        /// <param name="oTS">offsets to save</param>
        protected void InitOffsets(int[] oTL, int[] oTS)
        {
            offsetsToLoad.AddRange(oTL);

            foreach (int offset_i in offsetsToLoad) loadOffset[offset_i] = true;

            foreach (int offset_i in oTS) saveOffset[offset_i] = true;
        }
    }
}