using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Editor_Base_Class
{
    public partial class PointerManager<T> : Form where T : IData
    {
        // initialize array of checkboxes
        private int numOfChecks;
        private int CHECK_WIDTH = 64;
        private const int CHECK_HEIGHT = 24;
        private DataBlock<T> MyDB;

        public PointerManager(DataBlock<T> db, bool asHex = true)
        {
            InitializeComponent();
            MyDB = db;
            numOfChecks = MyDB.discontigAt.Length;
            checkDiscontig = new CheckBox[numOfChecks];

            // determine dimensions
            if (!asHex) CHECK_WIDTH += 12;
            int baseHeight = ClientSize.Height;
            int checksPerRow = 1 + ClientSize.Width / CHECK_WIDTH; // round up

            int checksPerColumn = 1;
            double squareRt = Math.Sqrt(numOfChecks);
            int sqrt = 0;
            while (sqrt < squareRt) sqrt++;
            if (sqrt > checksPerRow) checksPerRow = sqrt;
            while (checksPerColumn * checksPerRow < numOfChecks) checksPerColumn++;

            SetClientSizeCore(checksPerRow * CHECK_WIDTH,
                baseHeight + checksPerColumn * CHECK_HEIGHT);

            for (int check_i = 0; check_i < numOfChecks; check_i++)
            {
                checkDiscontig[check_i] = new CheckBox();
                int row_i = check_i / checksPerRow;
                int col_i = check_i % checksPerRow;

                checkDiscontig[check_i].Size = new System.Drawing.Size(CHECK_WIDTH, CHECK_HEIGHT);
                checkDiscontig[check_i].Location = new System.Drawing.Point(
                    5 + CHECK_WIDTH * col_i, baseHeight + CHECK_HEIGHT * row_i);

                checkDiscontig[check_i].Checked = MyDB.discontigAt[check_i];

                checkDiscontig[check_i].Text = check_i.ToString(asHex ? "X2" : "D3");
                checkDiscontig[check_i].CheckedChanged +=
                    new System.EventHandler(this.Check_CheckedChanged);
                Controls.Add(checkDiscontig[check_i]);
            }
        }

        private void Check_CheckedChanged(object sender, EventArgs e)
        {
            for (int check_i = 0; check_i < numOfChecks; check_i++)
            {
                if (checkDiscontig[check_i].Focused)
                {
                    MyDB.discontigAt[check_i] = checkDiscontig[check_i].Checked;
                    return;
                }
            }
        }

        private void BtnCheckAll_Click(object sender, EventArgs e)
        {
            for (int check_i = 0; check_i < numOfChecks; check_i++)
            {
                checkDiscontig[check_i].Checked = true;
                MyDB.discontigAt[check_i] = true;
            }
        }

        private void BtnUncheckAll_Click(object sender, EventArgs e)
        {
            for (int check_i = 0; check_i < numOfChecks; check_i++)
            {
                checkDiscontig[check_i].Checked = false;
                MyDB.discontigAt[check_i] = false;
            }
        }

        private void BtnUpdatePtrs_Click(object sender, EventArgs e)
        {
            MyDB.MakeContiguous();
        }
    }
}
