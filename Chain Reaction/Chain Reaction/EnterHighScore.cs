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
    public partial class EnterHighScore : Form
    {
        public string PlayerName { get; set; } 
        public EnterHighScore()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {


        }

        private void EnterHighScore_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            PlayerName = tbPlayerName.Text;
            DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void tbPlayerName_Validating(object sender, CancelEventArgs e)
        {
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
