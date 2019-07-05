using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chain_Reaction
{
    public partial class MainPage : Form
    {
        public HighScores hs;
        Timer timer = new Timer();
        Form1 form1;
      
        public MainPage()
        {
            InitializeComponent();
            timer.Interval = 10;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();
            //setHighScoresPanel();
            changeView(1);
        }
        public void changeView(int view)
        {
            if (view == 1) // Main Game Panel is Visible
            {
                this.Text = "Chain Reaction";
                MainPanel.Visible = true;
                HighScoresPanel.Visible = false;
                InstructionsPanel.Visible = false;

            }
            else if (view == 2) //High Scores Panel is Visible
            {
                this.Text = "High Scores";
                MainPanel.Visible = false;
                InstructionsPanel.Visible = false;
                HighScoresPanel.Visible = true;
            }
            else if (view == 3) //Instructions are visible
            {
                this.Text = "Instructions";
                lblInstructions.Text =
                    string.Format("The objective of the game is to cause a chain reaction among the balls on the screen\n" +
                    "On each level you have a number of balls that need to be expanded in order to pass the level.\n" +
                    "In order to save your score, you have to pass all 7 levels, and add it on the leaderboard.\n" +
                    "On the main page, you also have the option to play a custom game where you can adjust the number\n" +
                    "of balls that appear on the screen and the number of balls needed to expand to pass the level.\n" +
                    "The scores from the custom game cannot be saved, because that mode is intended only for fun and practicing.\n" +
                    "Good luck and let the games begin!");
                
                MainPanel.Visible = false;
                HighScoresPanel.Visible = false;
                InstructionsPanel.Visible = true;

            }
            
            else
            {
                MessageBox.Show("Ova uste ne e implementirano");
            }
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
            changeView(3);
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

        private void MainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        

        private void btnBack_Click(object sender, EventArgs e)
        {
            changeView(1);
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            changeView(1);
        }
    }
}
