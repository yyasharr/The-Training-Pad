using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class MyQueue //For Q232 from LeetCode
    {
        Stack<int> s1;
        Stack<int> s2;

        /** Initialize your data structure here. */
        public MyQueue()
        {
            s1 = new Stack<int>();
            s2 = new Stack<int>();
        }

        /** Push element x to the back of queue. */
        public void Push(int x)
        {
            AtoB(s2, s1); //if there is anything is s2, move them to s1
            s1.Push(x);
        }
        private void AtoB(Stack<int> A, Stack<int>B)
        {
            if (!A.Any())
                return;
            B.Push(A.Pop());
            AtoB(A, B);
        }
        /** Removes the element from in front of queue and returns that element. */
        public int Pop()
        {
            AtoB(s1, s2);
            return s2.Pop();
        }

        /** Get the front element. */
        public int Peek()
        {
            AtoB(s1, s2);
            return s2.Peek();
        }

        /** Returns whether the queue is empty. */
        public bool Empty()
        {
            return !s1.Any() && !s2.Any();
        }
    }
}

