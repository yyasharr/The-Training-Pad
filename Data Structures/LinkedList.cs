using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    class LinkedList
    {
        Node _head;

        public Node Head
        {
            get
            {
                return _head;
            }
            set
            { _head = value; }
        }

        public LinkedList(int n)
        {
            _head = new Node(n);
        }
        public LinkedList(Node head)
        {
            _head = head;
        }



        /// <summary>
        /// Replaces (swaps) two nodes with the given values in the linkedlist.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void ReplaceNode(int x, int y)
        {
            Node N1 = Head; Node PreN1 = null; //defining N1 and it's previous.
            Node N2 = Head; Node PreN2 = null; //defining N2 and it's previous.

            while (N1.data != x && N1 != null) //Search for X
            {
                PreN1 = N1;
                N1 = N1.next;
            }

            while (N2.data != y && N2 != null)
            {
                PreN2 = N2;
                N2 = N2.next;
            }

            if (N1 == null || N2 == null) return; //If one doesn't exist, no replacement!

            if (PreN1 != null) //if N1 is not head
                PreN1.next = N2;
            else //if N1 is head
                Head = N2;

            if (PreN2 != null) //if N2 is not head
                PreN2.next = N1;
            else //if N2 is head
                Head = N1;

            //replace the rest of the pointers
            Node temp = N1.next;
            N1.next = N2.next;
            N2.next = temp;
        }
    }
}
