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

namespace Gen2_Item_Editor
{
    public partial class ItemEditor : Editor_Base_Class.Gen2Editor
    {
        private int GetItemCost(int item_i)
        {
            return items[item_i, COST1_I] + 0x100 * items[item_i, COST2_I];
        }
        private void SetItemCost(int item_i, int cost)
        {
            items[item_i, COST2_I] = (byte)(cost / 0x100);
            items[item_i, COST1_I] = (byte)(cost % 0x100);
        }

        public ItemEditor()
        {
            InitializeComponent();

            int[] oTL = { ITEM_STRUCT_I, ITEM_ASM_I, ITEM_NAME_I, ITEM_DESC_PTR_I };
            InitOffsets(oTL, oTL);
        }

        protected override void EnableDataEntry()
        {
            spinItemID.Maximum = offset[NUM_OF_ITEMS_I];

            spinItemID.Enabled = true;
            tboxName.Enabled = true;
            tboxDesc.Enabled = true;
            spinCost.Enabled = true;
            spinHeldItemID.Enabled = true;
            spinParam.Enabled = true;
            cboxFlagtext.Enabled = true;
            cboxPocket.Enabled = true;
            spinASM.Enabled = true;
            cboxUseRestriction.Enabled = true;
        }

        protected override void EnableWrite()
        {
            tboxDeltaNameChars.Text = itemNames.BytesFreeAt(sIV()) + " bytes free for name";
            tboxDeltaDescChars.Text = itemDescs.BytesFreeAt(sIV()) + " bytes free for desc";

            saveROM_TSMI.Enabled = (itemNames.BytesFreeAt(0) >= 0
                && itemDescs.BytesOverlapAt() == -1);
        }

        protected override void UpdateEditor()
        {
            spinCost.Value = GetItemCost(sIV());
            spinHeldItemID.Value = items[sIV(), HELD_ITEM_ID_I];
            spinParam.Value = items[sIV(), PARAM_I];
            cboxFlagtext.SelectedIndex = items[sIV(), FLAG_I] / 0x40;
            cboxPocket.SelectedIndex = items[sIV(), POCKET_I] - 1;
            byte sI = 0;
            switch (items[sIV(), USE_RESTRICTION_I])
            {
                case 0: sI = 0; break;
                case 6: sI = 1; break;
                case 0x40: sI = 2; break;
                case 0x50: sI = 3; break;
                case 0x55: sI = 4; break;
                case 0x60: sI = 5; break;
                default: sI = 6; break;
            }
            cboxUseRestriction.SelectedIndex = sI;
            tboxName.Text = itemNames.data[sIV()];

            // desc data count < name data count
            bool nonTM = (sIV() <= itemDescs.end_i);
            if (nonTM)
            {
                tboxDesc.Text = itemDescs.data[sIV()];
                spinASM.Value = itemASM[sIV()];
            }
            else tboxDesc.Text = "MOVE DESCRIPTION";

            tboxDesc.Enabled = nonTM;
            spinASM.Enabled = nonTM;

            EnableWrite();
        }

        protected override void ImportData(List<string> dataStrings)
        {
            foreach (int item_i in itemNames.Range())
            {
                int stringIndex = 4 * (item_i - itemNames.start_i);

                itemNames.data[item_i] = dataStrings[stringIndex];
                itemDescs.data[item_i] = dataStrings[stringIndex + 1];

                string[] dataStruct = dataStrings[stringIndex + 2].Split(' ');

                if (dataStruct.Length == 8)
                {
                    itemDescs.SetRelativePtr(item_i, Convert.ToInt32(dataStruct[0]));
                    SetItemCost(item_i, Convert.ToInt32(dataStruct[1]));
                    items[item_i, HELD_ITEM_ID_I] = Convert.ToByte(dataStruct[2]);
                    items[item_i, PARAM_I] = Convert.ToByte(dataStruct[3]);
                    items[item_i, FLAG_I] = Convert.ToByte(dataStruct[4]);
                    items[item_i, POCKET_I] = Convert.ToByte(dataStruct[5]);
                    items[item_i, USE_RESTRICTION_I] = Convert.ToByte(dataStruct[6]);
                    //itemASM[item_i] = Convert.ToInt32(dataStruct[7]); 
                    // omit, ASM is not portable across ROMs
                }
            }
            itemDescs.MakeContiguous();
        }

        protected override void ExportData()
        {
            using (var file = new System.IO.StreamWriter(data_FilePath))
            {
                foreach (int item_i in itemNames.Range())
                {
                    file.WriteLine(itemNames.data[item_i]);
                    file.WriteLine(itemDescs.data[item_i]);

                    string dataStruct =
                        itemDescs.RelativePtr(item_i) + " " // ptrs
                        + GetItemCost(item_i).ToString() + " "
                        + items[item_i, HELD_ITEM_ID_I] + " "
                        + items[item_i, PARAM_I] + " "
                        + items[item_i, FLAG_I] + " "
                        + items[item_i, POCKET_I] + " "
                        + items[item_i, USE_RESTRICTION_I] + " "
                        + itemASM[item_i].ToString();

                    file.WriteLine(dataStruct);
                    file.WriteLine("");
                }
            }
        }

        protected override void ManagePointers()
        {
            new PointerManager<DBString>(itemDescs).Show();
        }



        private int sIV()
        {
            return (int)spinItemID.Value;
        }

        private void SpinItemID_ValueChanged(object sender, EventArgs e)
        {
            if (spinItemID.Focused) UpdateEditor();
        }

        private void CboxUseRestriction_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboxUseRestriction.Focused)
            {
                byte sI = 1;
                switch (cboxUseRestriction.SelectedIndex)
                {
                    case 0: sI = 0; break;
                    case 1: sI = 6; break;
                    case 2: sI = 0x40; break;
                    case 3: sI = 0x50; break;
                    case 4: sI = 0x55; break;
                    case 5: sI = 0x60; break;
                    default: sI = 1; break;
                }

                items[sIV(), USE_RESTRICTION_I] = sI;
            }
        }
        private void SpinCost_ValueChanged(object sender, EventArgs e)
        {
            SetItemCost(sIV(), (int)spinCost.Value);
        }
        private void SpinHeldItemID_ValueChanged(object sender, EventArgs e)
        {
            items[sIV(), HELD_ITEM_ID_I] = (byte)spinHeldItemID.Value;
        }
        private void SpinParam_ValueChanged(object sender, EventArgs e)
        {
            items[sIV(), PARAM_I] = (byte)spinParam.Value;
        }
        private void CboxFlagtext_SelectedIndexChanged(object sender, EventArgs e)
        {
            items[sIV(), FLAG_I] = (byte)(cboxFlagtext.SelectedIndex * 0x40);
        }

        private void CboxPocket_SelectedIndexChanged(object sender, EventArgs e)
        {
            items[sIV(), POCKET_I] = (byte)(cboxPocket.SelectedIndex + 1);
        }
        private void SpinASM_ValueChanged(object sender, EventArgs e)
        {
            itemASM[sIV()] = (int)spinASM.Value;
        }

        private void TboxName_TextChanged(object sender, EventArgs e)
        {
            if (tboxName.Focused)
            {
                itemNames.data[sIV()] = tboxName.Text;

                EnableWrite();

                PrintWarningIfTooLong(tboxName.Text, 12);
            }
        }
        private void TboxDesc_TextChanged(object sender, EventArgs e)
        {
            if (tboxDesc.Focused)
            {
                if (sIV() <= itemDescs.end_i)
                {
                    itemDescs.data[sIV()] = tboxDesc.Text;

                    itemDescs.UpdatePtrs(sIV());

                    EnableWrite();

                    string[] splitDesc = tboxDesc.Text.Split('|');
                    PrintWarningIfTooLong(splitDesc[0], 18);
                    if (splitDesc.Length >= 2)
                        PrintWarningIfTooLong(splitDesc[1], 18);
                }
            }
        }
    }
}
