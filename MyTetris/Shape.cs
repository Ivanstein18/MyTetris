using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTetris
{
    class Shape
    {
        public int x;
        public int y;
        public int[,] matrix;
        public int sizeMatrix;

        public int[,] tetr1 = new int[4, 4]
        {
            {0,0,1,0},
            {0,0,1,0},
            {0,0,1,0},
            {0,0,1,0}
        };

        public int[,] tetr2 = new int[3, 3]
        {
            {0,2,0},
            {0,2,2},
            {0,0,2}
        };

        public int[,] tetr3 = new int[3, 3]
       {
            {0,0,0},
            {3,3,3},
            {0,3,0}
       };

        public int[,] tetr4 = new int[3, 3]
       {
            {4,0,0},
            {4,0,0},
            {4,4,0}
       };

        public int[,] tetr5 = new int[2, 2]
       {
            {5,5},
            {5,5}
            
       };

        public Shape(int x, int y)
        {
            this.x = x;
            this.y = y;
            
            generateMatrix();
        }

        public void generateMatrix()
        {
            Random r = new Random();
            
            switch(r.Next(1,6))
            {
                case 1:
                    sizeMatrix = 4;
                    matrix = tetr1;
                    break;
                case 2:
                    sizeMatrix = 3;
                    matrix = tetr2;
                    break;
                case 3:
                    sizeMatrix = 3;
                    matrix = tetr3;
                    break;
                case 4:
                    sizeMatrix = 3;
                    matrix = tetr4;
                    break;
                case 5:
                    sizeMatrix = 2;
                    matrix = tetr5;
                    break;
            }
        }

        public void rotateShape()                            // не работает как надо!
        {
            for (int i = 0; i < sizeMatrix; i++)
            {
                for (int j = i; j < sizeMatrix; j++)
                {
                    int temp = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = temp;
                }
            }
            int offset1 = (8 - (x + sizeMatrix));
            if (offset1<0)
            {
                for (int i = 0; i < Math.Abs(offset1); i++)
                    MoveLeft();
            }
            
            if (x<0)
            {
                for (int i = 0; i < Math.Abs(x)+1; i++)
                    MoveRight();
            }
        }

        public void MoveDown()
        {
            y++;
        }
        public void MoveRight()
        {            
            x++;
        }
        public void MoveLeft()
        {            
            x--;
        }
    }
}
