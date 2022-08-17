using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuOnline_Portal
{
    public partial class Splash : Form
    {

        public Splash()
        {
            InitializeComponent();
        }
        public int Time { get ; set ; }
        private void timer1_Tick(object sender, EventArgs e)
        {
            bar.Width += 1;
            if (bar.Width >= 470)
            {
                timer1.Stop();
                new LogIn_Interface().Show();
                this.Hide();
            }
        }
        
    }
}
