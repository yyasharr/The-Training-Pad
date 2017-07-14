using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My_Training_Pad
{
    class MyInterviews
    {
        //Micosoft OneDrive Interview 6/22/17 (https://onedrive.live.com/?v=TextFileEditor&id=FB43B40742F9A0FC%213482&cid=FB43B40742F9A0FC&parId=FB43B40742F9A0FC%213480)
        public static int ShortestPath(Point Frog, Point dst, int[,] Maze)
        {
            int m = Maze.GetLength(0);
            int n = Maze.GetLength(1);

            if (Maze==null || (m==0 && n==0) //if maze is not declared correctly
                ||Frog.X >= m || Frog.Y >= n || Frog.X<0 || Frog.Y<0 //if frog is out of the maze
                ||dst.X >= m || dst.Y >= n ||dst.X<0 || dst.Y<0 //if destination is out of the maze
                || Frog.X<dst.X || Frog.Y>dst.Y //if destination is not accessible from where frog is.
                )
                return -1;

            int[,] Paths = new int[m, n];
            int count=0;
            for(int i=dst.Y;i>=0; i--)
            {
                Paths[dst.X, i] += count++;
                if (Frog.X == dst.X && Frog.Y == i) return Paths[dst.X, i];
            }
            count = 0;
            for(int i=dst.X; i<m; i++)
            {
                Paths[i, dst.Y] += count++;
                if (Frog.X == i && Frog.Y == dst.Y) return Paths[i, dst.Y];
            }
            for(int i=dst.X+1; i<m; i++)
            {
                for(int j=dst.Y-1; j>=0; j--)
                {
                    Paths[i, j] = 1 + Math.Min(Paths[i, j + 1], Paths[i -1, j]);
                }
            }
            return Paths[Frog.X,Frog.Y];
        }
        //Microsoft Mobile Interview 7/12/17 (https://onedrive.live.com/?cid=FB43B40742F9A0FC&id=FB43B40742F9A0FC%213583&parId=FB43B40742F9A0FC%213582&o=OneUp)
        public static int[,] Scaler(int[] items, int height, int width, int scale)
        {
            if (items.Length != height * width) return null;

            int[,] res = new int[height * scale, width * scale];
            int counter = 0;

            for(int i=0; i<height; i++)
            {
                for(int j=0; j<width; j++)
                {
                    int num = items[counter];
                    int startrow = i * height;
                    int startcol = j * width;
                    for (int k = startrow; k <= startrow + scale- 1; k++)
                    {
                        for (int l = startcol; l <= startcol + scale - 1; l++)
                        {
                            res[k, l] = num;
                        }
                    }
                    counter++;
                }
            }
            return res;
        }
    }
}
