using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTetris
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            size = 25;
            currentShape = new Shape(3, 0);

            
        }

        int size;
        int[,] map = new int[16, 8];
        Shape currentShape;


        private void timer1_Tick(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            DrawGrid(g);
            currentShape.MoveDown();
            Marge();
            DrawMap(g);



            pictureBox1.Invalidate();
        }

        public void DrawGrid(Graphics g)
        {
            for (int i = 0; i <= 16; i++)
            {
                g.DrawLine(Pens.Black, new Point(50, 50 + i * size), new Point(50 + 8 * size, 50 + i * size));

            }
            for (int i = 0; i <= 8; i++)
            {
                g.DrawLine(Pens.Black, new Point(50 + i * size, 50), new Point(50 + i * size, 50 + 16 * size));

            }

        }

        public void Marge()
        {
            for (int i = currentShape.y; i < currentShape.y + 3; i++)
            {
                for (int j = currentShape.x; j < currentShape.x + 3; j++)
                {
                    map[i, j] = currentShape.matrix[i - currentShape.y, j - currentShape.x];

                }

            }
        }
        public void DrawMap(Graphics e)
        {
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (map[i, j] == 1)
                    {
                        e.FillRectangle(Brushes.Red, new Rectangle(50 + j * size, 50 + i * size, size, size));
                    }
                }
            }
        }
    }
}
