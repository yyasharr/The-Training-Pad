using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad.Temp_Classes
{

    /*Question:
     Assume you have been given a M x N matrix, and filled with random number of A or B or 0. Find a set of A and B where they have the smallest distance.
     E.g:
     Input:
     A  0  0  0  0
     0  0  0  B  0
     0  0  A  0  0 
     Outout=2
     If the matrix has only A or B or 0, output -1.
     */
    class FindShortestAtoB
    {
        public char[,] Matrix { get; set; }
        int rowCount;
        int colCount;

        public FindShortestAtoB(char[,]matrix)
        {
            Matrix = matrix;
            rowCount = matrix.GetLength(0);
            colCount= matrix.GetLength(1);
        }

        public int FindShortestPathAtoB()
        {
            List<Point> listOfAs = FindCharPoints('A');
            List<Point> listOfBs = FindCharPoints('B');

            if (AorBMissing(listOfAs, listOfBs) == true) return -1;

            return FindMinimumDistance(listOfAs, listOfBs);
        }

        private int FindMinimumDistance(List<Point> listOfAs, List<Point> listOfBs)
        {
            int minDistance = int.MaxValue;
            for (int i = 0; i < listOfAs.Count; i++)
            {
                for (int j = 0; j < listOfBs.Count; j++)
                {
                    minDistance = Math.Min(minDistance, Distance(listOfAs[i], listOfBs[j]));
                }
            }
            return minDistance;
        }

        private int Distance(Point p1, Point p2)
        {
            return Math.Abs(p1.X - p2.X) + Math.Abs(p1.Y - p2.Y);
        }

        private bool AorBMissing(List<Point> listOfAs, List<Point> listOfBs)
        {
            return listOfAs.Count == 0 || listOfBs.Count == 0;
        }

        private List<Point> FindCharPoints(char ch)
        {
            List<Point> listOfChar = new List<Point>();
            for (int i = 0; i < rowCount; i++)
            {
                for (int j = 0; j < colCount; j++)
                {
                    if(Matrix[i,j]==ch)
                    {
                        listOfChar.Add(new Point(i, j));
                    }
                }
            }
            return listOfChar;
        }

        private class Point
        {
            public int X{ get; set; }
            public int Y { get; set; }
            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
