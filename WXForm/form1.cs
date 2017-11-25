using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WXForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
      
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //this.BackColor = ColorTranslator.FromHtml("#87CEFA"); // light sky blue

            this.BackColor = ColorTranslator.FromHtml("#a3a3a3");

        }

        private void ribbonButton1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Azure,e.ClipRectangle.Left,e.ClipRectangle.Top,e.ClipRectangle.Width - 1,e.ClipRectangle.Height - 1);
            base.OnPaint(e);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Azure, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            base.OnPaint(e);
        }

        private void label3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Transparent, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            base.OnPaint(e);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Pens.Transparent, e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
            base.OnPaint(e);
        }
    }
}
