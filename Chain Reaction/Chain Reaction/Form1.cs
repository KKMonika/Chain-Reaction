using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chain_Reaction
{
    public partial class Form1 : Form
    {
        public bool SaveScoreHasAppeared;
        public bool isOpened { get; set; }
        BigBall bigBall; //treba da se pojavuva na klik

        Timer timer;
        int leftX;
        int topY;
        int width;
        int height;

        private int generateBall = 0;
        Random random;
        public bool custom { get; set; } //flag to know if level is custom
        public Levels levelsDoc { get; set; }

        EnterHighScore enterHighScoreForm;
        public HighScores highScores { get; }
        
        public Form1(bool custom)
        {
            InitializeComponent();
            levelsDoc = new Levels();
            levelsDoc.addBallsDoc(custom);
            random = new Random();
            this.DoubleBuffered = true;
            timer = new Timer();
            timer.Interval = 50; //test
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start(); 
            leftX = 20;
            topY = 60; 
            width = this.Width - (3 * leftX);
            height = this.Height - (int)(2.5 * topY);
            isOpened = true;
            enterHighScoreForm = new EnterHighScore();
            highScores = new HighScores();
        }
       

        private void enterScore()
        {
            int points = levelsDoc.poeniOdSiteNivoa();
            if (enterHighScoreForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                HighScoreItem highScoreItem = new HighScoreItem();
                highScoreItem.player = enterHighScoreForm.PlayerName;
                highScoreItem.points = levelsDoc.poeniOdSiteNivoa();
                highScores.add(highScoreItem);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (generateBall < levelsDoc.currentLevel.maxBalls())
            {
                int x = random.Next(leftX + 10, leftX + width - 10); //leftX+10 i  leftX+width-10 zatoa sto ako se pogodi na rabot, lesno moze da izleze nadvor
                int y = random.Next(topY + 10, topY + height - 10);
                levelsDoc.currentLevel.AddBall(new Point(x, y));
                ++generateBall;
            }

            levelsDoc.currentLevel.MoveBalls(leftX, topY, width, height);
            if (levelsDoc.currentLevel.hasClicked) // ako ima mouse click togas proveruva kolizii
            {
                if (levelsDoc.currentLevel.isActive())
                {
                    levelsDoc.currentLevel.checkCollisions();
                    lblExpanded.Text = String.Format("Expanded so far: {0}", levelsDoc.currentLevel.count);
                }
                    
                else
                {
                    
                    timer.Stop();
                    if (levelsDoc.daliIspolnuva()) //ako e ispolnet uslovot, a ne e posledno ili custom nivo, tajmerot si prodolzuva, prodolzuva so iscrtuvanje za sledno nivo
                    {
                        if(levelsDoc.currentLevel.currentLevel != BallsDoc.LEVELS.NA && levelsDoc.currentLevel.currentLevel != BallsDoc.LEVELS.CUSTOM)
                        {
                            timer.Start();
                            generateBall = 0; //si prodolzuva na naredno nivo
                        }
                        else if (levelsDoc.currentLevel.currentLevel == BallsDoc.LEVELS.NA || levelsDoc.currentLevel.currentLevel == BallsDoc.LEVELS.CUSTOM)
                        {
                            MessageBox.Show(string.Format("Congratulations, you won {0} points!", levelsDoc.poeniOdSiteNivoa().ToString()));
                        }
                        lblNeedToExpand.Text = levelsDoc.currentLevel.needToHit().ToString();
                        lblMaxBalls.Text = levelsDoc.currentLevel.maxBalls().ToString();
                        lblLevel.Text = string.Format("Level: {0}", levelsDoc.getLevel().ToString());
                    }
                    else //zavrsuva igrata
                    {
                        MessageBox.Show("Game Over!");
                    }
                }
                    
            }
            
            Invalidate(true);
         
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            lblNeedToExpand.Text = levelsDoc.currentLevel.needToHit().ToString();
            lblMaxBalls.Text = levelsDoc.currentLevel.maxBalls().ToString();
            lblLevel.Text = string.Format("Level: {0}", levelsDoc.getLevel().ToString());
        }

        private void statusStrip1_Paint(object sender, PaintEventArgs e)
        {
            //toolStripStatusLabel1.Text = string.Format("Level {0}", levelsDoc.getLevel());
            toolStripStatusLabel2.Text = string.Format("Total points: {0}", levelsDoc.poeniOdSiteNivoa());
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(SystemColors.Control);
            Pen pen = new Pen(Color.Black, 3);
            e.Graphics.DrawRectangle(pen, leftX, topY, width, height);
            pen.Dispose();
            levelsDoc.currentLevel.Draw(e.Graphics);
           
            if (levelsDoc.currentLevel.hasClicked) //samo ako e kliknato togas da se iscrta golemata topka
            {
                bigBall.Draw(e.Graphics);
            }

        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (!levelsDoc.currentLevel.hasClicked)
            {
                levelsDoc.currentLevel.hasClicked = true;
                bigBall = new BigBall(e.Location, Color.Black);
                bigBall.isSet = true;
                levelsDoc.currentLevel.bigBall = bigBall;
                levelsDoc.currentLevel.bigBall.increaseRadius();
            }
            Invalidate(true);
        }
        
        

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            isOpened = false;
        }

        private void toolStripStatusLabel3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void backToMainToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            generateBall = 0;
            levelsDoc.Restart(custom);
            lblNeedToExpand.Text = levelsDoc.currentLevel.needToHit().ToString();
            lblMaxBalls.Text = levelsDoc.currentLevel.maxBalls().ToString();
            lblLevel.Text = string.Format("Level: {0}", levelsDoc.getLevel().ToString());
            timer.Start();
            Invalidate();
        }

        
    }
}
