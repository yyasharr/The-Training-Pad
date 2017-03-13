using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    class EPI //Questions from the book: Elements of Programming Interview
    {
        
        public static int Parity(int x)
        {
            int count = 0;
            while (x != 0)
            {
                int ns = x & 1;
                if ((x & 1) == 1) count++;
                x >>= 1;
            }
            return count % 2;
        } //Q5.1

        ////////////////////Q6.1 - Trying to solve in O(n) time and O(1) space////////////////////////////////////////////////////////////
        public static void PivotSort_NoDup(int[] array, int pivot) //Q6.1 --If there was no duplicate (no duplicate on pivot actually)
        {
            int left = 0;
            int right = array.Length - 1;

            while (right > left)
            {
                while (array[left] < pivot)
                {
                    left++;
                }
                while (array[right] > pivot)
                {
                    right--;
                }
                if (left < right)
                {
                    Swap(array, left, right);
                }
            }
        }
        public static int[] PivotSort_ExtraMemory(int [] array, int pivot) //Q6.1 --It supports duplicates, but uses O(n) memory
        {
            int[] result = new int[array.Length];
            int left = 0;
            int right = result.Length - 1;
            for(int i=0 ; i<array.Length ; i++)
            {
                if(array[i]<pivot)
                {
                    result[left] = array[i];
                    left++;
                }
                else if(array[i]>pivot)
                {
                    result[right] = array[i];
                    right--;
                }
            }
            for(int j=left ; j<=right ; j++)
            {
                result[j] = pivot;
            }
            return result;
        }
        public static void PivotSort(int[] array, int pivot) //Q6.1 --Trying to solve it in O(1) memory and support duplicates
        {
            int left = 0; int right = array.Length - 1;
            for(int i=0 ; i<array.Length ; i++)
            {
                if(array[i]<pivot)
                {
                    Swap(array, i, left);
                    left++;
                }
            }
            for(int i=array.Length-1 ; i>=0 && array[i]>=pivot;i--)
            {
                if(array[i]>pivot)
                {
                    Swap(array, i, right);
                    right--;
                }
            }
        }
        /////////////////////////////////////////Q17.1////////////////////////////////////////////////////////////////////////////////////
        public static int ScoreCombinations(int target, int[]options)
        {
            if (target == 0) return 1;
            if (target < 0) return 0;

            int count=0;

            for (int i = 0; i < options.Length; i++)
                count += ScoreCombinations(target - options[i],options);
            return count;
        }

        static void Swap(int[] array, int left, int right)
        {
            int temp = array[left];
            array[left] = array[right];
            array[right] = temp;
        }
    }
}
