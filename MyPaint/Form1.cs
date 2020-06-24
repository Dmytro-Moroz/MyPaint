using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Form1 : Form
    {
        private Bitmap bitmap;
        private Bitmap bitmap1;
        private int x1, y1;
        private int x2, y2;
        private string mode;

        public Form1()
        {
            mode = "Line";
            InitializeComponent();
            bitmap = new Bitmap(1000, 1000);
            bitmap1 = new Bitmap(1000, 1000);
            SolidBrush solidBrush = new SolidBrush(Color.White);
            Graphics.FromImage(bitmap).FillRectangle(solidBrush, 0, 0, bitmap.Width, bitmap.Height);
            Graphics.FromImage(bitmap1).FillRectangle(solidBrush, 0, 0, bitmap1.Width, bitmap1.Height);
            x1 = y1 = 0;
            pictureBox1.Image = bitmap;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            button7.BackColor = b.BackColor;
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
                bitmap.Save(saveFileDialog1.FileName);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "")
            {
                bitmap = (Bitmap)Image.FromFile(openFileDialog1.FileName);
                pictureBox1.Image = bitmap;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();

        }

        private void button8_Click(object sender, EventArgs e)
        {
            mode = "Line";
        }

        private void button9_Click(object sender, EventArgs e)
        {
            mode = "Rectangle";
        }

        private void button10_Click(object sender, EventArgs e)
        {
            mode = "Ellipse";
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            Graphics graphics = Graphics.FromImage(bitmap);
            Pen pen = new Pen(button7.BackColor, trackBar1.Value);
            if (mode == "Rectangle")
            {
                graphics.DrawRectangle(pen, x2, y2, e.X - x2, e.Y - y2);
            }
            if (mode == "Ellipse")
            {
                graphics.DrawEllipse(pen, x2, y2, e.X - x2, e.Y - y2);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            button7.BackColor = Color.White;
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            x2 = e.X;
            y2 = e.Y;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            Pen pen = new Pen(button7.BackColor, trackBar1.Value);
            Graphics graphics = Graphics.FromImage(bitmap);

            Graphics graphics1 = Graphics.FromImage(bitmap1);


            pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
            pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

            if (e.Button == MouseButtons.Left)
            {
                if (mode == "Line")
                {
                    graphics.DrawLine(pen, x1, y1, e.X, e.Y);
                }

                if (mode == "Rectangle")
                {
                    graphics1.Clear(Color.White);
                    int x, y;
                    x = x2;
                    y = y2;
                    if (x > e.X) x = e.X;
                    if (y > e.Y) y = e.Y;
                    graphics1.DrawRectangle(pen, x, y, Math.Abs(e.X - x2), Math.Abs(e.Y - y2));
                }
                if (mode == "Ellipse")
                {
                    graphics1.Clear(Color.White);
                    graphics1.DrawEllipse(pen, x2, y2, e.X - x2, e.Y - y2);
                }
                graphics1.DrawImage(bitmap, 0 , 0);

                pictureBox1.Image = bitmap1;
            }

            x1 = e.X;
            y1 = e.Y;
        }
    }
}
