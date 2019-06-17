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
        SmallBalls balls;
        Color currentColor;
        Timer timer;
        int leftX;
        int topY;
        int width;
        int height;
        bool flag = false;
        private int generateBall = 0;
        Random random;
        private String FileName;
        public Form1()
        {
            InitializeComponent();
            ballsDoc = new BallsDoc(); //treba da se doraboti
            random = new Random();
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
            //golema topka ne inicijalizirame tuku toa ke se pravi so nastanot MOUSE CLICK

        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (generateBall % 10 == 0)
            {
                int y = random.Next(height, width);
                int x = -SmallBalls.RADIUS;
                ballsDoc.AddBall(new Point(x, y));
            }
            ++generateBall; //test
            ballsDoc.MoveBalls(leftX, topY, width, height);
            //CheckColisions();
            Invalidate(true);
            //ne dovrseno za game over da se definira posle kolku vreme

        }

        /*void CheckColisions()
        {
            //da se proveraat koi topcinja se vo golemata
            //i da se zgolemuva radiusot na goleamta so toa
            if (flag)
            {
                foreach (SmallBalls ball in ballsDoc.Balls)
                {
                    if (bigBall.Touches(ball))
                    {
                        ball.isColided = true;
                        bigBall.changeRadius();
                    }
                }
                for(int i = ballsDoc.Balls.Count; i >=0; i--)
                {
                    if(ballsDoc.Balls[i].isColided)
                    {
                        ballsDoc.Balls.RemoveAt(i);
                    }
                }
            }
        }
        */


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void statusStrip1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            Pen pen = new Pen(Color.Black, 3);
            e.Graphics.DrawRectangle(pen, leftX, topY, width, height);
            pen.Dispose();
            ballsDoc.Draw(e.Graphics);
            if (flag)
            {
                bigBall.Draw(e.Graphics);
            }

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            width = this.Width - (3 * leftX);
            height = this.Height - (int)(2.5 * topY);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            bigBall = new BigBall(e.Location, Color.Black);
            Invalidate(true);
            flag = true;
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
    }
}
