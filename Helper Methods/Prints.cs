using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    static class Prints
    {

        /// <summary>
        /// Prints a list of lists of integers.
        /// </summary>
        /// <param name="input"></param>
        public static void Print(List<List<int>> input)
        {
            foreach(List<int> temp in input)
            {
                foreach(int num in temp)
                {
                    Console.Write(num + "\t");
                }
                Console.WriteLine();
            }
        }


        /// <summary>
        /// Prints a list of strings
        /// </summary>
        /// <param name="input"></param>
        public static void Print(List<string> input)
        {
            foreach (string s in input)
                Console.WriteLine(s);
        }


        /// <summary>
        /// Prints list of integers.
        /// </summary>
        /// <param name="input"></param>
        public static void Print(List<int> input)
        {
            foreach(int n in input)
            {
                Console.WriteLine(n);
            }
        }

        /// <summary>
        /// Prints array of integers 
        /// </summary>
        /// <param name="array"></param>
        public static void Print(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.WriteLine(array[i]);
        }


        /// <summary>
        /// Prints a matrix of integers.
        /// </summary>
        /// <param name="matrix"></param>
        public static void Print(int[,]matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            for(int i=0; i<n; i++)
            {
                for(int j=0; j<m; j++)
                {
                    Console.Write(matrix[i, j]+"\t");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Prints contents of a integer IList
        /// </summary>
        /// <param name="input"></param>
        public static void Print(IList<int> input)
        {
            foreach (int n in input)
                Console.WriteLine(n);
        }

    }
}
