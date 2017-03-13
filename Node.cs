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


public void print()
        {
            Console.Write(data + "->");
            if (next != null)
            {
                next.print();
            }
            else
            {
                Console.Write("null");
            }
        }
        public Node(int d)
        {
            data = d;
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
    }


}
/////////////////////////////////Sample initializer////////////////////////////////////////////
//Node head = new Node(1);
//head.next = new Node(2);
//head.next.next = new Node(3);
//head.next.next.next = new Node(4);
//head.next.next.next.next= new Node(5);
//head.next.next.next.next.next = new Node(6);
////head= [1]->[2]->[3]->[4]->[5]->[6]->null