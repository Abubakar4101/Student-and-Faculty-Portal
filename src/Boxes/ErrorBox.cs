using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CuOnline_Portal.src.Boxes
{
    public partial class ErrorBox : Form
    {
        public string text { get; internal set; }

        public ErrorBox()
        {
            InitializeComponent();
        }

        private void ErrorBox_Load(object sender, EventArgs e)
        {
            ErrorText.Text = text;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
