using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chain_Reaction
{
    public partial class CustomGame : Form
    {
        public int needToHit { get; set; }
        public int MaxBalls { get; set; }
        public CustomGame()
        {
            InitializeComponent();
        }

        private void nudMaxBalls_Validating(object sender, CancelEventArgs e)
        {
            if (nudNeedToHit.Value > nudMaxBalls.Value)
            {
                errorProvider1.SetError(nudMaxBalls, "The total number of balls in the game has to be bigger than the number of balls needed to expand");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(nudMaxBalls, null);
                e.Cancel = false;
            }
        }

        private void nudNeedToHit_Validating(object sender, CancelEventArgs e)
        {
            if (nudNeedToHit.Value > nudMaxBalls.Value)
            {
                errorProvider1.SetError(nudNeedToHit, "The total number of balls in the game has to be bigger than the number of balls needed to expand");
                e.Cancel = true;
            }
            else
            {
                errorProvider1.SetError(nudNeedToHit, null);
                e.Cancel = false;
            }
        }

        private void btnPlayCustom_Click(object sender, EventArgs e)
        {
            needToHit = (int)nudNeedToHit.Value;
            MaxBalls = (int)nudMaxBalls.Value;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
