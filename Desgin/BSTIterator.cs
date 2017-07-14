using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class BSTIterator//For Q173 from LeetCode
    {
        List<int> list;
        int next;
        public BSTIterator(Treenode root)
        {
            list = new List<int>();
            GenerateList(root);
            next = 0;
        }
        private void GenerateList(Treenode root)
        {
            if (root == null)
                return;
            GenerateList(root.left);
            list.Add(root.data);
            GenerateList(root.right);
        }

        /** @return whether we have a next smallest number */
        public bool HasNext()
        {
            return next < list.Count;
        }

        /** @return the next smallest number */
        public int Next()
        {
            return list[next++];
        }
    }
}
