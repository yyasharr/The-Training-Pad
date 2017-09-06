using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    class Node //Singly Linked List Node
    {
        public int data;
        public Node next = null;

        /// <summary>
        /// Prints the LinkedList
        /// </summary>
        public void Print()
        {
            Console.Write(data + "->");
            if (next != null)
            {
                next.Print();
            }
            else
            {
                Console.WriteLine("null");
            }
        }
        public Node(int d)
        {
            data = d;
        }

        public Node ReverseIterative()
        {
            Node head = this;
            if (head == null || head.next == null) return head;

            Node prev = null;

            while (head != null)
            {
                Node next = head.next;
                head.next = prev;
                prev = head;
                head = next;
            }
            return prev;
        }
        public Node Reverse()
        {
            if (this.next == null || this == null) return this;
            Node head = this;
            Node next = head.next;
            head.next = null;
            Node rest = next.Reverse();
            next.next = head;
            return rest;
        }
        public void RemoveKthFromEnd(int k)
        {
            if (k == 0) return;
            Node slow = this;
            Node fast = this;
            for (int i = 0; i < k; i++)
            {
                fast = fast.next;
            }
            while (fast.next != null)
            {
                fast = fast.next;
                slow = slow.next;
            }
            slow.next = slow.next.next;
        }
        public void AddToEnd(int data)
        {
            Node new_node = new Node(data);
            Node temp = this;
            while (temp.next != null)
            {
                temp = temp.next;
            }
            temp.next = new_node;
        }
        public Node Swap(int a, int b)
        {
            Node A = this;
            Node B = this;

            Node prevA = null;
            while (A != null && A.data != a)
            {
                prevA = A;
                A = A.next;
            }

            Node prevB = null;
            while (B != null && B.data != b)
            {
                prevB = B;
                B = B.next;
            }

            if (B == null || A == null) return this;
            Node ret = this;

            if (prevA != null)
                prevA.next = B;
            else ret = B;

            if (prevB != null)
                prevB.next = A;
            else ret = A;

            Node temp = A.next;
            A.next = B.next;
            B.next = temp;

            return ret;
        }

    }


}

#region Sample Initializers
#region Sample LinkedList Node 1
//Node head = new Node(1);
//head.next = new Node(2);
//head.next.next = new Node(3);
//head.next.next.next = new Node(4);
//head.next.next.next.next= new Node(5);
//head.next.next.next.next.next = new Node(6);
////head= [1]->[2]->[3]->[4]->[5]->[6]->null
#endregion
#region Sample LinkedList Node 2
//Node head = new Node(5);
//head.next = new Node(1);
//head.next.next = new Node(2);
//head.next.next.next = new Node(7);
//head.next.next.next.next = new Node(4);
//head.next.next.next.next.next = new Node(3);
//head.next.next.next.next.next.next = new Node(6);
//head.next.next.next.next.next.next.next = new Node(8);
////head= [5]->[1]->[2]->[7]->[4]->[3]->[6]->[8]->null
#endregion
#endregion