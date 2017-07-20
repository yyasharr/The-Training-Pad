using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad.Desgin
{
    public class NestedIterator
    {
        Queue<int> q;
        public NestedIterator(IList<NestedInteger> nestedList)
        {
            q = new Queue<int>();
            FillQ(nestedList);
        }

        private void FillQ(IList<NestedInteger> list)
        {
            foreach(NestedInteger ni in list)
            {
                if (ni.IsInteger())
                    q.Enqueue(ni.GetInteger());
                else
                    FillQ(ni.GetList());
            }
        }

        public bool HasNext()
        {
            return q.Any();
        }

        public int Next()
        {
            return q.Dequeue();
        }
    }

    public interface NestedInteger
    {

        // @return true if this NestedInteger holds a single integer, rather than a nested list.
        bool IsInteger();

        // @return the single integer that this NestedInteger holds, if it holds a single integer
        // Return null if this NestedInteger holds a nested list
        int GetInteger();

        // @return the nested list that this NestedInteger holds, if it holds a nested list
        // Return null if this NestedInteger holds a single integer
        IList<NestedInteger> GetList();
    }
}
