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
            int[] test = { 2, 3, 4, 5, 6, 4, 3, 2, 5, 65, 67, 788, 5, 3, 2, 56, 2, 34, 34, 645, 7, 452, 3453, 564, 67, 425, 234, 5, 656, 86, 745, 63,34, 43, 65, 45, 6, 2342, 123, 245, 467, 57, 45, 213, 1, 3456, 467, 68  };
            Sort<int>.MergeSort(test);
            Print(test);
            /////////////////////////End Time Below////////////////////////////////
            Console.WriteLine("time: " + (DateTime.Now - start).TotalSeconds);
            Console.ReadKey();

        }
    }
}
