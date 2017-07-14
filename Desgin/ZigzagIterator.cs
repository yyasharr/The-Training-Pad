using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class ZigzagIterator //For question 281 --> Follow up question
    {
        IList<IList<int>> list;
        int nextlist;
        public ZigzagIterator(IList<IList<int>> vec2d)
        {
            list = vec2d;
            nextlist = 0;
        }

        public bool HasNext()
        {
            return list.Any();
        }

        public int Next()
        {
            if (!HasNext()) throw new NullReferenceException("There is no Next element remaining to print.");
            int ret = list[nextlist][0];
            list[nextlist].RemoveAt(0);

            //if there was only one integars in the current list and it got removed, remove the whole list
            if(list[nextlist].Count==0)
            {
                list.RemoveAt(nextlist);
                //and don't increase the next list index, since the next list's index will be replaced.
            }
            else
            {
                nextlist = (nextlist == list.Count - 1) ? 0 : nextlist+1;
            }
            return ret;            
        }
    }
}
