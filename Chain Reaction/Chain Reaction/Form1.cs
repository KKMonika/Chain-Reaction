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
        BallsDoc ballsDoc; //treba da se pojavuvaat po odredena postignata vrednost na Radiusot na bigBall
        BigBall bigBall; //treba da se pojavuva na klik
       // SmallBall firstBall;
        int maxTopcinja;
        Color currentColor;

        Timer timer;
        int leftX;
        int topY;
        int width;
        int height;

        private int generateBall = 0;
        int level; // level
        Random random;

        private String FileName; //za serijalizacija
        public Form1()
        {
            InitializeComponent();
            ballsDoc = new BallsDoc(); //treba da se doraboti
            
            random = new Random();
            maxTopcinja = 20;
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

            level = 1;
           

        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (generateBall < maxTopcinja)
            {
                int x = random.Next(leftX+10, leftX + width-10); //leftX+10 i  leftX+width-10 zatoa sto ako se pogodi na rabot, lesno moze da izleze nadvor
                int y = random.Next(topY+10, topY+height-10);
                ballsDoc.AddBall(new Point(x, y));
            }
            ++generateBall; //test
            ballsDoc.MoveBalls(leftX, topY, width, height);
            if (ballsDoc.hasClicked) // ako ima mouse click togas da proveruva kolizii
            {
                if (ballsDoc.isActive())
                    ballsDoc.checkCollisions();
                else
                    timer.Stop();
            }

            ballsDoc.nextLevel(); // proverka za sledno nivo
            Invalidate(true);
            //ne dovrseno za game over da se definira posle kolku vreme
        }

        

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void statusStrip1_Paint(object sender, PaintEventArgs e)
        {
            toolStripStatusLabel2.Text = string.Format("Poeni: {0}", ballsDoc.poeni());
            toolStripStatusLabel1.Text = string.Format("Level: {0}", level);
            
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            Pen pen = new Pen(Color.Black, 3);
            e.Graphics.DrawRectangle(pen, leftX, topY, width, height);
            pen.Dispose();
            ballsDoc.Draw(e.Graphics);
           
            if (ballsDoc.hasClicked) //samo ako e kliknato togas da se iscrta golemata topka
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
            if (!ballsDoc.hasClicked)
            {
                ballsDoc.hasClicked = true;
                bigBall = new BigBall(e.Location, Color.Black);
                bigBall.isSet = true;
                ballsDoc.bigBall = bigBall;
                ballsDoc.bigBall.increaseRadius();
            }
            Invalidate(true);
            /* if (!ballsDoc.hasClicked)
             {
                 ballsDoc.hasClicked = true;
                 firstBall = new SmallBall();
                 firstBall.State = 3;
                 firstBall.bigBall = true;
                 flag = true;
                 //ballsDoc.AddBall(e.Location);
             }
             Invalidate();
             */
        }
        

        private void saveFile()
        {
            if (FileName == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Balls doc file (*.bll)|*.bll";
                saveFileDialog.Title = "Save balls doc";
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
                    formatter.Serialize(fileStream, ballsDoc);
                }
            }
        }
        private void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Polygons balls file (*.bll)|*.bll";
            openFileDialog.Title = "Open balls doc file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                try
                {
                    using (FileStream fileStream = new FileStream(FileName, FileMode.Open))
                    {
                        IFormatter formater = new BinaryFormatter();
                        ballsDoc = (BallsDoc)formater.Deserialize(fileStream);
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

        /*private void timerIncrease_Tick(object sender, EventArgs e)
        {

        }*/
    }
}
