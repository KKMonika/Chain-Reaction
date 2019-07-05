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
            setHighScoresPanel();
            


        }
        public void changeView(int view)
        {
            if (view == 1) // Main Game Panel is Visible
            {
                this.Text = "Chain Reaction";
                MainPanel.Visible = true;
                HighScoresPanel.Visible = false;

            }
            else if (view == 2) //High Scores Panel is Visible
            {
                this.Text = "High Scores";
                MainPanel.Visible = false;
                HighScoresPanel.Visible = true;
                //treba da se implementira za Custom i Instructions vo poseben panel
                // za da nemame poveke formi za otvaranje
            }
            
            else
            {
                MessageBox.Show("Ova uste ne e implementirano");
            }
        }
        public void setHighScoresPanel()
        {
            List<Label> visibleNames = new List<Label>();
            visibleNames.Add(label9);
            visibleNames.Add(label10);
            visibleNames.Add(label11);
            visibleNames.Add(label12);
            visibleNames.Add(label13);

            List<Label> visiblePoints = new List<Label>();
            visiblePoints.Add(label3);
            visiblePoints.Add(label4);
            visiblePoints.Add(label5);
            visiblePoints.Add(label6);
            visiblePoints.Add(label17);

            HighScores toBeShown = new HighScores();

            for (int i = 0; i < toBeShown.highScores.Count; i++)
            {
                visibleNames[i].Text = toBeShown.highScores[i].player;
                visiblePoints[i].Text = toBeShown.highScores[i].points.ToString();
                
            }
            for (int i = toBeShown.highScores.Count; i < 5; i++)
            {
                visibleNames[i].Text = "";
                visiblePoints[i].Text = "";
            }

        }

        public void submitHighScore(string name, int points)
        {
            HighScoreItem item = new HighScoreItem();
            item.player = name;
            item.points = points;
            hs.add(item);

            
        }
        /// <summary>
        /// Serialize a Scores object to a binary file.
        /// </summary>
        /// <param name="HS">T object that needs to be serialized.</param>
        private static void BinarySerializeScores(HighScores HS)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            using (FileStream str = File.Create(path + "\\HighScores.hs"))
            {
                File.SetAttributes(path + "\\HighScores.hs", File.GetAttributes(path + "\\HighScores.hs") | FileAttributes.Hidden);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(str, HS);
            }
        }
        /// <summary>
        /// Deserialize a Scores object from a binary file.
        /// </summary>
        /// <returns>Scores()</returns>
        private static HighScores BinaryDeserializeScores()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            HighScores HS = null;
            try
            {

                using (FileStream str = File.OpenRead(path + "\\HighScores.hs"))
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    HS = (HighScores)bf.Deserialize(str);
                }

                File.Delete(path + "\\HighScores.hs");

                return HS;
            }
            catch (FileNotFoundException)
            {
                return new HighScores();
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
