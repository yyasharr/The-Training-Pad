using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class CellTracker //for Leetcode Q85
    {
        int _r; //right
        int _d; //down

        public CellTracker(int r, int d)
        {
            R = r;
            D = d;
        }

        public CellTracker()
        {
            R = 0;
            D = 0;
        }

        public int R { get { return _r; } set { _r = value; } }
        public int D { get { return _d; } set { _d = value; } }
    }
}
