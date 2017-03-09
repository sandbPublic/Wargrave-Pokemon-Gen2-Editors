using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms  ;
using Editor_Base_Class;
using System.Globalization;

// see pokecrystal/battle/anims.asm
// todo more than just move animations?
// how to handle names for non move anims?
// just with hex?
namespace Gen2_Move_Animation_Editor {
    public partial class MoveAnimationEditor : Editor_Base_Class.Gen2Editor {
        public MoveAnimationEditor() {
            InitializeComponent();

            int[] oTL = { MOVE_NAME_I, ANIM_PTR_I };
            int[] oTS = { ANIM_PTR_I };

            initOffsets(oTL, oTS);
        }

        protected override void enableDataEntry() {
            spinAnimID.Maximum = offset[NUM_OF_ANIMS_I]-1;

            spinAnimID.Enabled = true;
            rTxtBytes.Enabled = true;

            importData_TSMI.Enabled = false;
            exportData_TSMI.Enabled = false;
        }

        protected override void enableWrite() {
            int bytesFree = animations.bytesFreeAt((int)spinAnimID.Value);

            txtBytesFree.Text = bytesFree + " bytes free";

            saveROM_TSMI.Enabled = bytesFree >= 0;
        }

        protected override void update() {
            rTxtBytes.BackColor = System.Drawing.SystemColors.Window;
            
            if (moveNames.start_i <= sAnim() && sAnim() <= moveNames.end_i)
                txtMoveName.Text = moveNames.data[sAnim()];
            else txtMoveName.Text = "0x" + sAnim().ToString("X3");

            //populate text boxs
            rTxtBytes.Text = "";
            for (int aI_i = 0; aI_i < sLAI().Count; aI_i++) {
                rTxtBytes.Text += sLAI()[aI_i].byteString()
                    + (aI_i != sLAI().Count - 1 ? Environment.NewLine : "");
            }
            updateCode();

            enableWrite();
        }

        //protected override void importData(List<string> dataStrings) { }
        //protected override void exportData() { }
        protected override void managePointers() {
            PointerManager<AnimationCode> pm = new PointerManager<AnimationCode>(animations);
            pm.Show();
        }

        private int sAnim() {
            return (int)spinAnimID.Value;
        }

        private List<AnimeInstr> sLAI() {
            return animations.data[sAnim()].me;
        }

        private void spinAnimID_ValueChanged(object sender, EventArgs e) {
            update();
        }

        private void updateCode() {
            rTxtCode.Text = "";
            updateCode(animations.data[sAnim()]);
        }

        private void updateCode(AnimationCode aC) {
            long offset = aC.startAddr % 0x10000;

            foreach (AnimeInstr aI in aC.me) {
                rTxtCode.Text += offset.ToString("X") + " | " + aI.codeString()
                    + Environment.NewLine;
                offset += aI.size();
            }

            foreach (AnimationCode jump in aC.jumps) {
                rTxtCode.Text += Environment.NewLine;
                updateCode(jump);
            }
        }

        //attempt to create an AnimationCode
        private void rTxtBytes_TextChanged(object sender, EventArgs e) {
            if (rTxtBytes.Focused) {
                int length = rTxtBytes.Lines.Length;
                List<AnimeInstr> testCode = new List<AnimeInstr>();

                // try parse
                for (int line_i = 0; line_i < length; line_i++) {
                    AnimeInstr aI = new AnimeInstr();
                    string[] animeStrs = rTxtBytes.Lines[line_i].Split(' ');

                    byte i = 0;
                    if (byte.TryParse(animeStrs[0],
                        NumberStyles.HexNumber,
                        null, out i)) {

                        aI.opCode = i;
                        int expectedLength = 1 + aI.expectedParameters() +
                            (aI.expectedPtr() ? 2 : 0);

                        if (animeStrs.Length == expectedLength) {
                            // load params
                            for (int param_i = 1; param_i <= aI.expectedParameters(); param_i++) {
                                byte p = 0;
                                if (byte.TryParse(animeStrs[param_i],
                                    NumberStyles.HexNumber,
                                    null, out p)) {

                                    aI.parameters.Add(p);
                                } else {
                                    badParse(); return;
                                }
                            }

                            // load ptr from last two bytes
                            if (aI.expectedPtr()) {
                                byte x = 0;
                                if (!byte.TryParse(animeStrs[expectedLength - 2],
                                    NumberStyles.HexNumber,
                                    null, out x)) {

                                    badParse(); return;
                                }
                                byte y = 0;
                                if (!byte.TryParse(animeStrs[expectedLength - 1],
                                    NumberStyles.HexNumber,
                                    null, out y)) {

                                    badParse(); return;
                                }
                                aI.ptr = new gbcPtr(x, y, animations.ptrs[sAnim()].ROMbank);
                            }
                        } else {
                            badParse(); return;
                        }
                    } else {
                        badParse(); return;
                    }

                    testCode.Add(aI);
                }

                // update data
                rTxtBytes.BackColor = System.Drawing.SystemColors.Window;
                animations.data[sAnim()].me.Clear();
                foreach (AnimeInstr aI in testCode) {
                    animations.data[sAnim()].me.Add(aI);
                }
                animations.updatePtrs(sAnim());
                updateCode();
                enableWrite();
            }
        }

        private void badParse() {
            rTxtBytes.BackColor = System.Drawing.Color.FromArgb(255, 191, 191);
            saveROM_TSMI.Enabled = false;
        }
    }
}
