using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    class LeetCode //Questions from Leetcode.com
    {
        ////////////////////////////Q206/////////////////////////////////////////////////////////////
        public static Node Reverse_Iterative(Node head)
        {
            if (head == null || head.next == null) return head;
            Node prev = null;
            Node Next = head.next;
            while (head != null)
            {
                head.next = prev;
                prev = head;
                head = Next;
                if (Next != null)
                    Next = Next.next;
            }
            return prev;
        }
        public static Node Reverse_Recursively(Node head)
        {
            if (head == null || head.next == null) return head;

            Node Next = head.next;
            head.next = null;
            Node rest = Reverse_Recursively(Next);
            Next.next = head;
            return rest;
        }
    }
}
////////////////////////////Q/////////////////////////////////////////////////////////////