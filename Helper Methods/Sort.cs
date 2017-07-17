using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    static class Sort<T> where T:IComparable<T>
    {
        /////////////////////Heap Sort////////////////////////////
        public static void HeapSort(T[] array) 
        {
            MinHeap<T> minheap = new MinHeap<T>();
            for(int i=0; i<array.Length; i++)
            {
                minheap.Insert(array[i]);
            }
            for(int i=0; i<array.Length; i++)
            {
                array[i] = minheap.ExtractMin();
            }
        }

        /////////////////////Merge Sort////////////////////////////
        public static void MergeSort(int[] array)
        {
            int n = array.Length;
            if (n < 2) return;
            int mid = n / 2;
            int[] left = new int[mid];
            int[] right = new int[n - mid];

            for (int i = 0; i < mid; i++)
            {
                left[i] = array[i];
            }
            for (int i = mid; i < n; i++)
            {
                right[i - mid] = array[i];
            }

            MergeSort(right);
            MergeSort(left);
            Merge(left, right, array);
        }
        private static void Merge(int[] left, int[] right, int[] array)
        {
            int i = 0; int j = 0; int k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                {
                    array[k] = left[i];
                    i++; k++;
                }
                else
                {
                    array[k] = right[j];
                    j++; k++;
                }
            }
            if (i >= left.Length)
            {
                while (j < right.Length)
                {
                    array[k] = right[j];
                    j++; k++;
                }
            }

            if (j >= right.Length)
            {
                while (i < left.Length)
                {
                    array[k] = left[i];
                    i++; k++;
                }
            }
        }

        /////////////////////Quick Sort////////////////////////////
        public static void QuickSort(int[] array)
        {
            QuickSort(array, 0, array.Length - 1);
        }
        private static void QuickSort(int[] array, int min, int max)
        {
            int index = Partition(array, min, max);
            if (min < index-1)
                QuickSort(array, 0, index - 1);
            if (max > index)
                QuickSort(array, index + 1, max);
        }
        private static int Partition(int [] array, int min, int max)
        {
            int pivot = array[(min + max) / 2];
            while(min<=max)
            {
                while(array[min]<pivot)
                {
                    min++;
                }
                while(array[max]>pivot)
                {
                    max--;
                }
                if(min<=max)
                {
                    Swap(array, min, max);
                    min++;
                    max--;
                }
            }
            return min;
        }
        private static void Swap(int[]array, int n1, int n2)
        {
            int temp = array[n1];
            array[n1] = array[n2];
            array[n2] = temp;
        }
    }
}

//////////////////////////////////////////////↓Speed Comparisons↓//////////////////////////////////////////////////
//int[] test = { 2,3,4,5,6,4,3,2,5,65,67,788,5,3,2,56,2,34,34,645,7,452,3453,564,67,425,234,5,656,86,745,63};

//DateTime qtime = DateTime.Now;
//Sort.QuickSort(test);
//Console.WriteLine("Quicksort: " + (DateTime.Now - qtime).TotalSeconds);

//DateTime mtime = DateTime.Now;
//Sort.MergeSort(test);
//Console.WriteLine("Mergesort: " + (DateTime.Now - mtime).TotalSeconds);

//DateTime htime = DateTime.Now;
//Sort.HeapSort(test);
//Console.WriteLine("Heapsort: " + (DateTime.Now - htime).TotalSeconds);
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////

