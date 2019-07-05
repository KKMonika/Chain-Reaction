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
    public partial class MainPage : Form
    {

        Timer timer = new Timer();
        Form1 form1;
      
        public MainPage()
        {
            InitializeComponent();
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            

        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (form1 != null && !form1.isOpened)
                this.Show();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            form1 = new Form1(false);
            form1.Show();
            this.Hide();
        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            //ovde se otvara prozor za instrukciite
        }

        private void btnCustomPlay_Click(object sender, EventArgs e)
        {
            CustomGame cgForm = new CustomGame();
            
            if (cgForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                form1 = new Form1(true);
                form1.levelsDoc.currentLevel.customMaxBalls = cgForm.MaxBalls;
                form1.levelsDoc.currentLevel.customNeedToHit = cgForm.needToHit;
                form1.Show();
                this.Hide();
            }
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            

               
        }
    }
}
