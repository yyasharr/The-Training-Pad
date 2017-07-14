using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class MinStack //For Q155 from LeetCode
    {
        Stack<int> itemstack, minimumstack;
        /** initialize your data structure here. */
        public MinStack()
        {
            itemstack = new Stack<int>();
            minimumstack = new Stack<int>();
        }

        public void Push(int x)
        {
            itemstack.Push(x);
            if (minimumstack.Any())
            {
                if (x < minimumstack.Peek())
                    minimumstack.Push(x);
                else
                    minimumstack.Push(minimumstack.Peek());
            }
            else
                minimumstack.Push(x);
        }

        public void Pop()
        {
            minimumstack.Pop();
            itemstack.Pop();
        }

        public int Top()
        {
            return itemstack.Peek();
        }

        public int GetMin()
        {
            return minimumstack.Peek();
        }
    }
}
