using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Net;

namespace My_Training_Pad
{
    class Program
    {
        #region Print Methods
        public static void Print(List<List<int>> input)
        {
            foreach (List<int> temp in input)
            {
                foreach (int num in temp)
                {
                    Console.Write(num + "\t");
                }
                Console.WriteLine();
            }
        }
        public static void Print(List<string> input)
        {
            foreach (string s in input)
                Console.WriteLine(s);
        }
        public static void Print(List<int> input)
        {
            foreach (int n in input)
            {
                Console.WriteLine(n);
            }
        }
        public static void Print(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.WriteLine(array[i]);
        }
        public static void Print(IEnumerable<int> list)
        {
            foreach (int i in list)
            {
                Console.WriteLine(i);
            }
        }
        public static void Print(int[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }
        }
        public static void Print(IList<int> input)
        {
            foreach (int n in input)
                Console.WriteLine(n);
        }
        public static void Print(IList<IList<int>> input)
        {
            foreach (List<int> temp in input)
            {
                foreach (int num in temp)
                {
                    Console.Write(num + "\t");
                }
                Console.WriteLine();
            }
        }
        public static void Print(Node head)
        {
            while (head != null)
            {
                Console.Write(head.data + "->");
                head = head.next;
            }
            Console.WriteLine("null");
            //Or recursive below:
            //if (head == null) return;
            //string rest = "";
            //rest = (head.next == null) ? "->null\n" : "->";
            //Console.Write(head.data+rest);

            //Print(head.next);
        }
        #endregion
        static void Main(string[] args)
        {
            /////////////////////////Start Time Below//////////////////////////////
            LeetCode lc = new LeetCode();
            
            DateTime start = DateTime.Now;
            /////////////////////////Functions Below///////////////////////////////
            Console.WriteLine(lc.MinDistance("zoologicoarchaeologist","zoogeologist"));

            /////////////////////////End Time Below////////////////////////////////
            Console.WriteLine("time: " + (DateTime.Now - start).TotalSeconds);
            Console.ReadKey();

        }
    }
}
