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
            g = Graphics.FromImage(pictureBox1.Image);
            size = 25;
            score = 0;
            linesRemoved = 0;
            currentShape = new Shape(3, 0);
            whiteBrush = new SolidBrush(Color.White);
            
        }

        int oldTimerInterval;
        int size;
        int score;
        int linesRemoved;
        int[,] map = new int[16, 8];
        Shape currentShape;
        SolidBrush whiteBrush;
        Graphics g;


        private void timer1_Tick(object sender, EventArgs e)
        {            
            g.FillRectangle(whiteBrush, 0, 0, pictureBox1.Width, pictureBox1.Height);
            DrawGrid(g);                        
            Marge();
            DrawMap(g);
            resetArea();

            if (!collide())
                currentShape.MoveDown();
            else
            {
                Marge();                
                currentShape = new Shape(3, 0);
                timer1.Interval = oldTimerInterval;
            }
            sliseMap();


            pictureBox1.Invalidate();
        }

        public void sliseMap()
        {
            int count = 0;
            int curRemovedLines = 0;
            for (int i = 0; i < 16; i++)
            {
                count = 0;
                for (int j = 0; j < 8; j++)
                {
                    if (map[i, j] != 0) count++;
                }
                if (count==8)
                {
                    curRemovedLines++;
                    for (int k = i; k > 1; k--)                // k>0?
                    {
                        for (int o = 0; o < 8; o++)
                        {
                            map[k, o] = map[k - 1, o];
                        }
                    }
                }
            }

            g.FillRectangle(whiteBrush, 0, 0, pictureBox1.Width, pictureBox1.Height);
            DrawGrid(g);
            Marge();
            DrawMap(g);
            resetArea();
            pictureBox1.Invalidate();

            for (int i = 0; i <= curRemovedLines; i++)            
                score += 10 * i;                      

            linesRemoved += curRemovedLines;

            label1.Text = "Score: " + score;
            label2.Text = "Lines: " + linesRemoved;
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

        public bool collide()
        {
            for (int i = currentShape.y+currentShape.sizeMatrix-1; i >= currentShape.y; i--)
            {
                for (int j = currentShape.x ; j < currentShape.x + currentShape.sizeMatrix; j++)
                {
                    if (currentShape.matrix[i-currentShape.y,j-currentShape.x]!=0)
                    {
                    if (i + 1 == 16) return true;
                    if (map[i + 1, j] != 0) return true;
                    }
                }
            }
            return false;
        }

        public bool collide(int dir)
        {
            for (int i = currentShape.y; i < currentShape.y + currentShape.sizeMatrix; i++)
            {
                for (int j = currentShape.x ; j < currentShape.x + currentShape.sizeMatrix; j++)
                {
                    if (currentShape.matrix[i - currentShape.y, j - currentShape.x] != 0)
                    {
                        if (j + 1 * dir > 7 || j + 1 * dir < 0) return true;
                        if (map[i, j + 1 * dir] != 0) return true;                   
                    }
                }
            }
            return false;
        }

        public void Marge()
        {
            for (int i = currentShape.y; i < currentShape.y + currentShape.sizeMatrix; i++)
            {
                for (int j = currentShape.x; j < currentShape.x + currentShape.sizeMatrix; j++)
                {
                    if (currentShape.matrix[i - currentShape.y, j - currentShape.x] != 0)
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
                        e.FillRectangle(Brushes.Red, new Rectangle(50 + j * size+1, 50 + i * size+1, size-1, size-1));
                    if (map[i, j] == 2)
                        e.FillRectangle(Brushes.Blue, new Rectangle(50 + j * size + 1, 50 + i * size + 1, size - 1, size - 1));
                    if (map[i, j] == 3)
                        e.FillRectangle(Brushes.Orange, new Rectangle(50 + j * size + 1, 50 + i * size + 1, size - 1, size - 1));
                    if (map[i, j] == 4)
                        e.FillRectangle(Brushes.Gray, new Rectangle(50 + j * size + 1, 50 + i * size + 1, size - 1, size - 1));
                    if (map[i, j] == 5)
                        e.FillRectangle(Brushes.Green, new Rectangle(50 + j * size + 1, 50 + i * size + 1, size - 1, size - 1));
                }
            }
        }
        public void resetArea()
        {
            for (int i = currentShape.y; i < currentShape.y + currentShape.sizeMatrix; i++)
            {
                for (int j = currentShape.x; j < currentShape.x + currentShape.sizeMatrix; j++)
                {
                    if (i >= 0 && j >= 0 && i <= 16 && j <= 8)
                    {                       
                        if(currentShape.matrix[i-currentShape.y,j-currentShape.x]!=0)
                           map[i, j] = 0;
                    }
                }

            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Down)
            {
                oldTimerInterval = timer1.Interval;
                timer1.Interval = 1;
            }
            if (e.KeyCode==Keys.Up)
            {
                currentShape.rotateShape();
                g.FillRectangle(whiteBrush, 0, 0, pictureBox1.Width, pictureBox1.Height);
                DrawGrid(g);
                Marge();
                DrawMap(g);
                resetArea();
                pictureBox1.Invalidate();
            }
            if (e.KeyCode==Keys.Left)
            {
                if (!collide(-1))
                {
                    currentShape.MoveLeft();                    
                    g.FillRectangle(whiteBrush, 0, 0, pictureBox1.Width, pictureBox1.Height);
                    DrawGrid(g);
                    Marge();
                    DrawMap(g);
                    resetArea();
                    pictureBox1.Invalidate();
                }
            }
            if (e.KeyCode==Keys.Right)
            {
                if (!collide(1))
                {
                    currentShape.MoveRight();                    
                    g.FillRectangle(whiteBrush, 0, 0, pictureBox1.Width, pictureBox1.Height);
                    DrawGrid(g);
                    Marge();
                    DrawMap(g);
                    resetArea();
                    pictureBox1.Invalidate();
                }
            }
        }
    }
}
