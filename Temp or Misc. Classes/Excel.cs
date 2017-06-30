using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class Excel //For Q631 from Leetcode: https://leetcode.com/problems/design-excel-sum-formula/#/description
    {
        int height;
        int width;
        public Cell[,] grid;

        public Excel(int H, char W)
        {
            height = H;
            width = W - 'A' + 1;

            grid = new Cell[height, width];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    grid[i, j] = new Cell(i, j, 0);
                }
            }
        }

        public void Set(int r, char c, int v)
        {
            int[] loc = GetLocation(r, c);
            int row = loc[0];
            int col = loc[1];
            Cell curr = grid[row, col];
            //if this current cell used to be sum of other cells
            //then it should be removed from dependencies of other cells
            while (curr.ItsSumOf.Count > 0)
            {
                Cell n = curr.ItsSumOf[0];
                n.ItsPartOf.Remove(curr);
                curr.ItsSumOf.Remove(n);
            }
            //if this current cell is used in a sum in another cell
            //the sum of the other cell should be updated by this new value
            if (curr.ItsPartOf.Count > 0)
            {
                int diff = v - curr.Value;
                foreach (Cell n in curr.ItsPartOf)
                {
                    n.Value += diff;
                }
            }
            curr.Value = v;
            //check this later:
            grid[row, col] = curr;
        }

        public int Get(int r, char c)
        {
            int[] loc = GetLocation(r, c);
            return grid[loc[0], loc[1]].Value;
        }

        public int Sum(int r, char c, string[] strs)
        {
            int[] loc = GetLocation(r, c);
            int row = loc[0];
            int col = loc[1];
            int sum = 0;

            foreach (string s in strs)
            {
                string range = s;
                if (!range.Contains(':'))
                {
                    range = range + ":" + range;
                }
                sum += SumOfRange(range, loc);
            }
            grid[row, col].Value = sum;
            return sum;
        }
        private int SumOfRange(string range, int[] loc)
        {
            int row = loc[0];
            int col = loc[1];

            string start = range.Substring(0, range.IndexOf(':'));
            string end = range.Substring(range.IndexOf(':') + 1);

            int[] locstart = GetLocation(int.Parse(start.Substring(1)), start[0]);
            int[] locend = GetLocation(int.Parse(end.Substring(1)), end[0]);

            int sum = 0;

            for (int i = locstart[0]; i <= locend[0]; i++)
            {
                for (int j = locstart[1]; j <= locend[1]; j++)
                {
                    //adding the dependencies
                    grid[i, j].ItsPartOf.Add(grid[row, col]);
                    if (!grid[row, col].ItsSumOf.Contains(grid[i, j]))
                    {
                        grid[row, col].ItsSumOf.Add(grid[i, j]);
                    }
                    sum += grid[i, j].Value;
                }
            }
            return sum;
        }
        private int[] GetLocation(int r, char c)
        {
            //first int is row in grid; because row starts at 1
            //second int is col in grid, because col starts at A
            return new int[] { r - 1, c - 'A' };
        }

    }
    public class Cell
    {
        int _value;
        int _row;
        int _col;
        List<Cell> _itssumof; //if this cell is sum of other cells, they are listed here.
        List<Cell> _itspartof; //if this cell is a part of a sum, the cell that contains the sum is listed here.

        public int Row { get { return _row; } set { _row = value; } }
        public int Col { get { return _col; } set { _col = value; } }
        public List<Cell> ItsSumOf { get { return _itssumof; } set { _itssumof = value; } }
        public List<Cell> ItsPartOf { get { return _itspartof; } set { _itspartof = value; } }
        public int Value { get { return _value; } set { _value = value; } }

        public Cell(int r, int c, int v)
        {
            _value = v;
            _row = r;
            _col = c;
            _itspartof = new List<Cell>();
            _itssumof = new List<Cell>();
        }
    }
}
