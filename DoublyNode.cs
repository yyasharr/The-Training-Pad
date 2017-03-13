using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    class DoublyNode //Doubly Linked List Node. Try to work on DoublyLinkedList class listed below
    {
        public int data;
        public DoublyNode next=null;
        public DoublyNode prev=null;

        public DoublyNode(int d)
        {
            data = d;
        }

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

    }
    class DoublyLinkedList
    {
        public DoublyNode start = null;
        public DoublyNode end = null;
        public int size = 0;

        public bool IsEmpty()
        {
            return start == null;
        }

        public void AddtoEnd(int d)
        {
            DoublyNode new_node = new DoublyNode(d);
            if (start == null)
            {
                start = new_node;
                end = start;
            }
            else
            {
                new_node.prev = end;
                end.next = new_node;
                end = new_node;
            }
            size++;
        }

        public void AddtoStart(int d)
        {
            DoublyNode new_node = new DoublyNode(d);
            if(start==null)
            {
                start = new_node;
                end = start;
            }
            else
            {
                start.prev = new_node;
                new_node.next = start;
                start = new_node;
            }
            size++;
        }

        
        public void print()
        {
            Console.Write(start.data + "->");
            if (start.next != null)
            {
                start.next.print();
            }
            else
            {
                Console.Write("null");
            }
        }
    }

}
