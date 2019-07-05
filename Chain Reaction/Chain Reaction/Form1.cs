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
        public bool isOpened { get; set; }
        //BallsDoc ballsDoc; //treba da se pojavuvaat po odredena postignata vrednost na Radiusot na bigBall
        BigBall bigBall; //treba da se pojavuva na klik
        //public int maxTopcinja { get; set; }
        Color currentColor;

        Timer timer;
        int leftX;
        int topY;
        int width;
        int height;

        private int generateBall = 0;
        Random random;
        public bool custom { get; set; } //flag to know if level is custom
        public Levels levelsDoc { get; set; }

        private String FileName; //za serijalizacija
        public Form1(bool custom)
        {
            InitializeComponent();
            levelsDoc = new Levels();
            levelsDoc.addBallsDoc(custom);
            random = new Random();
            //maxTopcinja = 10;
            this.DoubleBuffered = true;
            currentColor = Color.Red; // pokasno da se napravi da se bira boja
            timer = new Timer();
            timer.Interval = 50; //test
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start(); // ke go koristime timerot za posle odredeno vreme ako ne se napravi pogodok ->GAMEOVER
            leftX = 20;
            topY = 60; //test vrednosti predlog za cela forma 816:489
            width = this.Width - (3 * leftX);
            height = this.Height - (int)(2.5 * topY);
            isOpened = true;
        }



        void timer_Tick(object sender, EventArgs e)
        {
            /*if (levelsDoc.currentLevel.currentLevel == BallsDoc.LEVELS.L1)
            {
                maxTopcinja = 10;
            }
            else if (levelsDoc.currentLevel.currentLevel == BallsDoc.LEVELS.L2)
            {
                maxTopcinja = 15;
            }
            else if (levelsDoc.currentLevel.currentLevel == BallsDoc.LEVELS.L3)
            {
                maxTopcinja = 20;
            }
            else if (levelsDoc.currentLevel.currentLevel == BallsDoc.LEVELS.L4)
            {
                maxTopcinja = 25;
            }
            else if (levelsDoc.currentLevel.currentLevel == BallsDoc.LEVELS.L5)
            {
                maxTopcinja = 30;
            }*/

            if (generateBall < levelsDoc.currentLevel.maxBalls())
            {
                int x = random.Next(leftX + 10, leftX + width - 10); //leftX+10 i  leftX+width-10 zatoa sto ako se pogodi na rabot, lesno moze da izleze nadvor
                int y = random.Next(topY + 10, topY + height - 10);
                levelsDoc.currentLevel.AddBall(new Point(x, y));
                ++generateBall;
            }

            levelsDoc.currentLevel.MoveBalls(leftX, topY, width, height);
            if (levelsDoc.currentLevel.hasClicked) // ako ima mouse click togas da proveruva kolizii
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
                        if(levelsDoc.currentLevel.currentLevel != BallsDoc.LEVELS.L7 && levelsDoc.currentLevel.currentLevel != BallsDoc.LEVELS.CUSTOM)
                        {
                            timer.Start();
                            generateBall = 0; //si prodolzuva na naredno nivo
                        }
                        else
                        {
                            MessageBox.Show(string.Format("Congratulations, you won {0} points!", levelsDoc.poeniOdSiteNivoa().ToString()));
                        }
                        lblNeedToExpand.Text = levelsDoc.currentLevel.needToHit().ToString();
                        lblMaxBalls.Text = levelsDoc.currentLevel.maxBalls().ToString();
                    }
                    else //zavrsuva igrata
                    {
                        MessageBox.Show("Game Over");
                    }


                    //OVDE DA SE DODADE DODAVANJE HIGHSCORE
                }
                    
            }
            
            Invalidate(true);
            //ne dovrseno za game over da se definira posle kolku vreme
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {
            lblNeedToExpand.Text = levelsDoc.currentLevel.needToHit().ToString();
            lblMaxBalls.Text = levelsDoc.currentLevel.maxBalls().ToString();
            
        }

        private void statusStrip1_Paint(object sender, PaintEventArgs e)
        {
            toolStripStatusLabel1.Text = string.Format("Level {0}", levelsDoc.getLevel());
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

        /*private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            width = this.Width - (3 * leftX);
            height = this.Height - (int)(2.5 * topY);
        }*/

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
        

        private void saveFile()
        {
            if (FileName == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "ChainReaction file (*.crf)|*.crf";
                saveFileDialog.Title = "ChainReaction file";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = saveFileDialog.FileName;
                }
            }
            if (FileName != null)
            {
                using (FileStream fileStream = new FileStream(FileName, FileMode.Create))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, levelsDoc);
                }
            }
        }
        private void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "ChainReaction file (*.crf)|*.crf";
            openFileDialog.Title = "ChainReaction file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                try
                {
                    using (FileStream fileStream = new FileStream(FileName, FileMode.Open))
                    {
                        IFormatter formater = new BinaryFormatter();
                        levelsDoc = (Levels)formater.Deserialize(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file: " + FileName);
                    FileName = null;
                    return;
                }
                Invalidate(true);
            }
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

        /*private void timerIncrease_Tick(object sender, EventArgs e)
        {

        }*/
    }
}
