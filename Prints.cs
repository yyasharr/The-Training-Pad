using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    static class Prints
    {
        public static void ListofLists(List<List<int>> input)
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
        public static void ListofStrings(List<string> input)
        {
            foreach (string s in input)
                Console.WriteLine(s);
        }
        public static void ListofIntegers(List<int> input)
        {
            foreach(int n in input)
            {
                Console.WriteLine(n);
            }
        }
        public static void ArrayofIntegers(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.WriteLine(array[i]);
        }
        public static void MatrixofIntegers(int[,]matrix)
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

    }
}
