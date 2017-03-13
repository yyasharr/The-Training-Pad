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
        public static void ArrayofIntegers(int[] array)
        {
            for (int i = 0; i < array.Length; i++)
                Console.WriteLine(array[i]);
        }

    }
}
