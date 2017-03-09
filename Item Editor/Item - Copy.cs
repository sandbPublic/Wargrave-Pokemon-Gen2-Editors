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
using CommonCode;

namespace Wargrave_GSC_Item_Editor {
    public partial class ItemEditor : Form {
        private OpenFileDialog ofd = new OpenFileDialog();
        private FileStream ROM_File {
            get { return CommonFns.ROM_File; }
            set { CommonFns.ROM_File = value; }
        }
        private string ROM_FilePath;

        public int itemStructOffset;

        // end at E6C0
        const int COST1 = 0, // lower byte eg 100
        COST2 = 1, // upper byte eg 9800
        HELD_ITEM_ID = 2, // For held items only
        PARAM = 3, // Heal amount, probability of effect in hex, etc
        FLAG = 4, // Key items and TMs = C0; Bike, Rods, and Itemfinder = 80; all others 40       
        POCKET = 5, 
        USE_RESTRICTION = 6;
        private readonly string[] POCKETS = { "Item", "Key", "Ball", "Tm" };
        private byte[,] items = new byte[256, 7];
        private int getItemCost(int item_i) {
            return items[item_i, COST1] + 0x100 * items[item_i, COST2];
        }
        private void setItemCost(int item_i, int cost) {
            items[item_i, COST1] = (byte)(cost / 0x100);
            items[item_i, COST1] = (byte)(cost % 0x100);
        }

        //asm code offsets
        // pointers to the code to execute?
        public int asmOffset;
        // to E8A0 :
        private int[] asm = new int[256]; // TODO treat as GBCptrs?

        const int LAST_NON_TM_ITEM = 0xBD;
        // TMs are special in a few ways: they don't have item descriptions
        // but instead use the corresponding move desc. They also don't have
        // their asm pointers in the same table

        public int namesOffset;
        private string[] names = new string[256];
        public int namesEnd;
        private int nameBytesUsed() {
            int ret = 0;
            for (int name_i = 1; name_i <= 0xFF; name_i++) {
                ret += names[name_i].Length + 1;
            }
            return ret;
        }
        private int nameBytesAvailable() {
            return namesEnd - namesOffset;
        }

        public int descPtrsOffset;
        private int[] descPtrs = new int[256];
        private string[] descs = new string[256];
        public int descsEnd;
        private int descBytesUsed() {
            int ret = 0;
            for (int desc_i = 1; desc_i <= LAST_NON_TM_ITEM; desc_i++) {
                ret += descs[desc_i].Length + 1;
            }
            return ret;
        }
        private int descBytesAvailable() {
            return descsEnd - descPtrs[1];
        }

        private void setOffsets(int version) {
            itemStructOffset = Offsets.itemStructs[version];
            asmOffset = Offsets.itemASM[version];

            namesOffset = Offsets.itemNames[version];
            namesEnd = Offsets.itemNamesEnd[version];

            descPtrsOffset = Offsets.itemDescPtrs[version];
            descsEnd = Offsets.itemDescsEnd[version];
        }

        public ItemEditor() {
            InitializeComponent();

            setOffsets(2);
            cboxVersion.SelectedIndex = 2;

            ofd.Filter = ".gbc|*.gbc|All Files|*";
        }

        private void btnOpenROM_Click(object sender, EventArgs e) {
            if (ofd.ShowDialog() == DialogResult.OK) {
                txtboxFilename.Text = ofd.SafeFileName;
                ROM_FilePath = ofd.FileName;
                ofd.Dispose();
                ROM_File = new FileStream(ROM_FilePath, FileMode.Open);

                ROM_File.Position = itemStructOffset;
                for (int item_i = 1; item_i <= 0xFF; item_i++) {
                    for (int byte_i = 0; byte_i < 7; byte_i++) {
                        items[item_i, byte_i] = (byte)ROM_File.ReadByte();
                    }
                }

                ROM_File.Position = asmOffset;
                // data after 0xBD does not seem to be the asm pointers anymore
                // tms have completely different asm
                for (int asm_i = 1; asm_i <= LAST_NON_TM_ITEM; asm_i++) {
                    asm[asm_i] = ROM_File.ReadByte() * 0x100 + ROM_File.ReadByte();
                }

                ROM_File.Position = namesOffset;
                for (int name_i = 1; name_i <= 0xFF; name_i++) {
                    names[name_i] = CommonFns.pkmnReadString();
                }

                ROM_File.Position = descPtrsOffset;
                for (int descPtr_i = 1; descPtr_i <= LAST_NON_TM_ITEM; descPtr_i++) {
                    descPtrs[descPtr_i] = CommonFns.GBCPtr();
                }

                // descs only extend up to 0xBD + 9, however the 9 ?~ should not be changed?
                for (int desc_i = 1; desc_i <= LAST_NON_TM_ITEM; desc_i++) {
                    ROM_File.Position = descPtrs[desc_i];
                    descs[desc_i] = CommonFns.pkmnReadString();
                }

                ROM_File.Dispose();

                spinItemID.Enabled = true;
                tboxName.Enabled = true;
                tboxDesc.Enabled = true;
                btnWriteROM.Enabled = true;
                spinCost.Enabled = true;
                spinHeldItemID.Enabled = true;
                spinParam.Enabled = true;
                spinFlag.Enabled = true;
                cboxPocket.Enabled = true;
                spinASM.Enabled = true;
                cboxUseRestriction.Enabled = true;

                enableWrite();

                spinItemID.Value = 2;
                spinItemID.Value = 1;
            }
        }

        private void btnWriteROM_Click(object sender, EventArgs e) {
            ROM_File = new FileStream(ROM_FilePath, FileMode.Open);

            ROM_File.Position = itemStructOffset;
            for (int item_i = 1; item_i < items.Length; item_i++) {
                for (int byte_i = 0; byte_i < 7; byte_i++) {
                    ROM_File.WriteByte(items[item_i, byte_i]);
                }
            }

            ROM_File.Position = asmOffset;
            for (int asm_i = 1; asm_i <= LAST_NON_TM_ITEM; asm_i++) {
                ROM_File.WriteByte((byte)(asm[asm_i]/0x100));
                ROM_File.WriteByte((byte)(asm[asm_i]%0x100));
            }

            ROM_File.Position = namesOffset;
            for (int name_i = 1; name_i < names.Length; name_i++) {
                CommonFns.pkmnWriteString(names[name_i]);
            }


            ROM_File.Position = descPtrsOffset;
            for (int descPtr_i = 1; descPtr_i <= LAST_NON_TM_ITEM; descPtr_i++) {
                CommonFns.writeLocalGBCPtr(descPtrs[descPtr_i]);
            }

            for (int desc_i = 1; desc_i <= LAST_NON_TM_ITEM; desc_i++) {
                ROM_File.Position = descPtrs[desc_i];
                CommonFns.pkmnWriteString(descs[desc_i]);
            }

            ROM_File.Dispose();
        }

        // don't allow saving beyond alloted space
        private void enableWrite() {
            tboxDeltaNameChars.Text = "Used name bytes: " +
                nameBytesUsed().ToString() + "/" + nameBytesAvailable().ToString();
            tboxDeltaDescChars.Text = "Used desc bytes: " +
                descBytesUsed().ToString() + "/" + descBytesAvailable().ToString();

            btnWriteROM.Enabled =
                (nameBytesUsed() <= nameBytesAvailable() &&
                descBytesUsed() <= descBytesAvailable() &&
                tboxName.Text.Length <= 12);
        }

        private void cboxVersion_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboxVersion.Focused) {
                setOffsets(cboxVersion.SelectedIndex);
            }
        }

        private void buttonCustomOffsets_Click(object sender, EventArgs e) {
            formCustomOffsets fCO = new formCustomOffsets(this);
            fCO.Show();
        }


        private int sMV() {
            return (int)spinItemID.Value;
        }

        private void spinItemID_ValueChanged(object sender, EventArgs e) {
            if (spinItemID.Focused) {
                spinCost.Value = getItemCost(sMV());
                spinHeldItemID.Value = items[sMV(), HELD_ITEM_ID];
                spinParam.Value = items[sMV(), PARAM];
                spinFlag.Value = items[sMV(), FLAG];
                cboxPocket.SelectedIndex = items[sMV(), POCKET] - 1;
                byte sI = 0;
                switch (items[sMV(), USE_RESTRICTION]) {
                    case 0: sI = 0; break;
                    case 6: sI = 1; break;
                    case 0x40: sI = 2; break;
                    case 0x50: sI = 3; break;
                    case 0x55: sI = 4; break;
                    case 0x60: sI = 5; break;
                    default: sI = 6; break;
                }
                cboxUseRestriction.SelectedIndex = sI;
                tboxName.Text = names[sMV()];
                tboxDesc.Text = descs[sMV()];
                spinASM.Value = asm[sMV()];

                spinASM.Enabled = (sMV() <= LAST_NON_TM_ITEM);
            }
        }

        private void cboxUseRestriction_SelectedIndexChanged(object sender, EventArgs e) {
            if (cboxUseRestriction.Focused) {
                byte sI = 1;
                switch (cboxUseRestriction.SelectedIndex) {
                    case 0: sI = 0; break;
                    case 1: sI = 6; break;
                    case 2: sI = 0x40; break;
                    case 3: sI = 0x50; break;
                    case 4: sI = 0x55; break;
                    case 5: sI = 0x60; break;
                    default: sI = 1; break;
                }

                items[sMV(), USE_RESTRICTION] = sI;
            }
        }

        private void spinCost_ValueChanged(object sender, EventArgs e) {
            setItemCost(sMV(), (int)spinCost.Value);
        }
        private void spinHeldItemID_ValueChanged(object sender, EventArgs e) {
            items[sMV(), HELD_ITEM_ID] = (byte) spinHeldItemID.Value;
        }
        private void spinParam_ValueChanged(object sender, EventArgs e) {
            items[sMV(), PARAM] = (byte)spinParam.Value;
        }
        private void spinFlag_ValueChanged(object sender, EventArgs e) {
            items[sMV(), FLAG] = (byte)spinFlag.Value;

            cboxFlagtext.SelectedIndex = (int) (spinFlag.Value / 0x40);
        }
        private void cboxPocket_SelectedIndexChanged(object sender, EventArgs e) {
            items[sMV(), POCKET] = (byte)(cboxPocket.SelectedIndex + 1);
        }
        private void spinASM_ValueChanged(object sender, EventArgs e) {
            asm[sMV()] = (int)spinASM.Value;
        }

        private void tboxName_TextChanged(object sender, EventArgs e) {
            if (tboxName.Focused) {
                names[sMV()] = tboxName.Text;

                // don't allow saving beyond alloted space
                enableWrite();

                if (tboxName.Text.Length > 12) {
                    formCharLimitWarning clw = new formCharLimitWarning();
                    clw.Show();
                }
            }
        }

        private void tboxDesc_TextChanged(object sender, EventArgs e) {
            if (tboxDesc.Focused) {
                if (sMV() < 0xFB) {
                    // inc/decrement pointers
                    int deltaBytes =
                        // bytes actually used
                        tboxDesc.Text.Length + 1 -
                        // bytes expected before adjustment
                        (descPtrs[sMV() + 1] - descPtrs[sMV()]);

                    for (int descPtr_i = sMV() + 1; descPtr_i <= 0xFB; descPtr_i++) {
                        descPtrs[descPtr_i] += deltaBytes;
                    }
                }

                descs[sMV()] = tboxDesc.Text;

                enableWrite();
            }
        }

        public class formCharLimitWarning : Form {
            private TextBox message;

            public formCharLimitWarning() {
                ClientSize = new System.Drawing.Size(320, 70);
                Font = new System.Drawing.Font("Courier New", 14F);
                Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
                Name = "formCharLimitWarning";
                ResumeLayout(false);
                PerformLayout();

                message = new TextBox();
                message.Enabled = false;
                message.Font = new System.Drawing.Font("Courier New", 14.25F);
                message.Location = new System.Drawing.Point(10, 10);
                message.Size = new System.Drawing.Size(300, 50);
                message.Multiline = true;
                message.BorderStyle = BorderStyle.None;
                message.Name = "message";
                message.Text = "Item names should not be longer than 12 characters";
                Controls.Add(message);
            }
        }

        public class formCustomOffsets : Form {
            ItemEditor creator;

            private NumericUpDown[] spin;

            int itemStruct = 0, asm = 1, names = 2, namesEnd = 3,
                descPtrs = 4, descsEnd = 5, NUM_ROWS = 6;

            private TextBox[] tboxLabels;

            public formCustomOffsets(ItemEditor c) {
                creator = c;
                spin = new NumericUpDown[NUM_ROWS];
                tboxLabels = new TextBox[NUM_ROWS];

                Text = "Custom Offsets";
                int ROW_HEIGHT = 35;
                int WIDTH = 90;
                int xOffset = 200;

                for (int row_i = 0; row_i < NUM_ROWS; row_i++) {
                    spin[row_i] = new NumericUpDown();
                    spin[row_i].Location = new System.Drawing.Point(xOffset, 10 + ROW_HEIGHT * row_i);
                    spin[row_i].Size = new System.Drawing.Size(WIDTH, ROW_HEIGHT);
                    spin[row_i].Hexadecimal = true;
                    spin[row_i].Maximum = new decimal(new int[] { 0x1FFFFF, 0, 0, 0 });
                    spin[row_i].TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    spin[row_i].ValueChanged += new System.EventHandler(spin_ValueChanged);
                    Controls.Add(spin[row_i]);

                    tboxLabels[row_i] = new TextBox();
                    tboxLabels[row_i].Location = new System.Drawing.Point(0, 15 + ROW_HEIGHT * row_i);
                    tboxLabels[row_i].Size = new System.Drawing.Size(xOffset - 10, ROW_HEIGHT);
                    tboxLabels[row_i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                    tboxLabels[row_i].Enabled = false;
                    tboxLabels[row_i].TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
                    Controls.Add(tboxLabels[row_i]);
                }

                spin[itemStruct].Value = creator.itemStructOffset;
                spin[asm].Value = creator.asmOffset;

                spin[names].Value = creator.namesOffset;
                spin[namesEnd].Value = creator.namesEnd;

                spin[descPtrs].Value = creator.descPtrsOffset;
                spin[descsEnd].Value = creator.descsEnd;

                tboxLabels[itemStruct].Text = "Item data";
                tboxLabels[asm].Text = "Asm table";

                tboxLabels[names].Text = "Move names";
                tboxLabels[namesEnd].Text = "Move names end";

                tboxLabels[descPtrs].Text = "Description ptrs";
                tboxLabels[descsEnd].Text = "Descriptions end";


                ClientSize = new System.Drawing.Size(xOffset + WIDTH + 10, 20 + ROW_HEIGHT * NUM_ROWS);
                Font = new System.Drawing.Font("Courier New", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
                Name = "formCustomOffsets";
                ResumeLayout(false);
                PerformLayout();
            }

            private void spin_ValueChanged(object sender, EventArgs e) {
                bool focused = false;

                for (int row_i = 0; row_i < NUM_ROWS; row_i++) {
                    if (spin[row_i].Focused) focused = true;
                }

                if (focused) {
                    creator.itemStructOffset = (int)spin[itemStruct].Value;
                    creator.asmOffset = (int)spin[asm].Value;

                    creator.namesOffset = (int)spin[names].Value;
                    creator.namesEnd = (int)spin[namesEnd].Value;

                    creator.descPtrsOffset = (int)spin[descPtrs].Value;
                    creator.descsEnd = (int)spin[descsEnd].Value;
                }
            }
        }
    }
}
