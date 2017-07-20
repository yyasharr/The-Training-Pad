using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad.Desgin
{
    public class ZigzagIterator
    {
        int listindex;
        List<IList<int>> lists;
        public ZigzagIterator(IList<int> v1, IList<int> v2) //For question 281
        {
            lists = new List<IList<int>>();
            if (v1.Count > 0)
                lists.Add(v1);
            if (v2.Count > 0)
                lists.Add(v2);
            listindex = 0;
        }

        public bool HasNext()
        {
            if (lists.Count > 0)
                return true;
            return false;
        }

        public int Next()
        {
            int res = lists[listindex][0];
            lists[listindex].RemoveAt(0);
            if (lists[listindex].Count == 0)
                lists.RemoveAt(listindex);
            if (lists.Count > 1)
            {
                listindex = (listindex == 1) ? 0 : 1;
            }
            else
                listindex = 0;
            return res;
        }
    }
}
