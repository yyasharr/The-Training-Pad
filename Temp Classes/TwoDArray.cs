using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class TwoDArray : IComparable<TwoDArray> //for Leetcode question 646
    {
        public int[] TheArray { get; set; }

        public TwoDArray(int[] array)
        {
            TheArray = array;
        }

        public int CompareTo(TwoDArray other)
        {
            return this.TheArray[0].CompareTo(other.TheArray[0]);
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
