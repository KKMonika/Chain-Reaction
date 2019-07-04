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
            if (form1 != null && !form1.isOpened) this.Show();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            //ovde treba da se otvora Form1 vo ist prozor, ne vo nov\
            form1 = new Form1();
            form1.Show();
            this.Hide();




        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            //ovde se otvara prozor za instrukciite
        }

        private void btnCustomPlay_Click(object sender, EventArgs e)
        {
            
        }

        private void MainPage_Load(object sender, EventArgs e)
        {
            

               
        }
    }
}
