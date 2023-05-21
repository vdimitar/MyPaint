using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyPaint
{
    public partial class Form1 : Form
    {
        private bool isDrawing = false;
        private Point firstPoint = Point.Empty;
        private ToolStripButton currentTool = null;
        private Bitmap bitmap;
        private Pen currentPen;
        private const int EraserSize = 10;  // You can adjust this size for your eraser

        public Form1()
        {
            InitializeComponent();

            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            currentPen = new Pen(Color.Black);

            toolStripButton1.Click += (sender, e) =>
            {
                currentTool = toolStripButton1;
            };

            toolStripButton2.Click += (sender, e) =>
            {
                currentTool = toolStripButton2;
            };

            toolStripButton3.Click += (sender, e) =>
            {
                currentTool = toolStripButton3;
            };

            toolStripButton4.Click += (sender, e) =>
            {
                currentTool = toolStripButton4;
            };

            toolStripButton5.Click += (sender, e) =>
            {
                currentTool = toolStripButton5;
            };

            toolStripButton6.Click += (sender, e) =>
            {
                using (ColorDialog colorDialog = new ColorDialog())
                {
                    if (colorDialog.ShowDialog() == DialogResult.OK)
                    {
                        currentPen.Color = colorDialog.Color;
                    }
                }
            };
        }

        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            pictureBox1.Refresh();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Media.SystemSounds.Beep.Play();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (currentTool != null)
            {
                isDrawing = true;
                firstPoint = e.Location;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    if (currentTool == toolStripButton1)
                    {
                        g.DrawLine(currentPen, firstPoint, e.Location);
                        firstPoint = e.Location;
                    }
                    else if (currentTool == toolStripButton5)  // eraser tool
                    {
                        g.FillRectangle(Brushes.White, e.X - EraserSize / 2, e.Y - EraserSize / 2, EraserSize, EraserSize);
                    }
                }
                pictureBox1.Refresh();
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    if (currentTool == toolStripButton2)
                    {
                        g.DrawLine(currentPen, firstPoint, e.Location);
                    }
                    else if (currentTool == toolStripButton3)
                    {
                        Rectangle rect = GetRectangleFromPoints(firstPoint, e.Location);
                        g.DrawRectangle(currentPen, rect);
                    }
                    else if (currentTool == toolStripButton4)
                    {
                        Rectangle rect = GetRectangleFromPoints(firstPoint, e.Location);
                        g.DrawEllipse(currentPen, rect);
                    }
                }
                pictureBox1.Refresh();
                isDrawing = false;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(bitmap, Point.Empty);
        }

        private Rectangle GetRectangleFromPoints(Point p1, Point p2)
        {
            return new Rectangle(Math.Min(p1.X, p2.X), Math.Min(p1.Y, p2.Y), Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
        }
    }
}

