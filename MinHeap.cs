using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    class MinHeap
    {
        private List<int> Heap;

        /// <summary>
        /// Returns the number of the integars in the Min Heap.
        /// </summary>
        public int Count
        {
            get { return Heap.Count; }
        }

        /// <summary>
        /// Returns the Minimum element of the heap without removing it. Throws an error if empty.
        /// </summary>
        public int Min
        {
            get
            {
                if (Heap.Count == 0) throw new NullReferenceException();
                else return Heap[0];
            }
        }

        /// <summary>
        /// Create an empty Min Heap.
        /// </summary>
        public MinHeap()
        {
            Heap = new List<int>();
        }

        /// <summary>
        /// Inserts a new integar to the Min Heap and reforms the heap.
        /// </summary>
        /// <param name="n"></param>
        public void Insert(int n)
        {
            Heap.Add(n);
            int childindex = Heap.Count - 1;
            int parentindex = (childindex - 1) / 2;
            while (parentindex >= 0 && Heap[childindex] < Heap[parentindex])
            {
                Swap(childindex, parentindex);
                childindex = parentindex;
                parentindex = (childindex - 1) / 2;
            }
        }
    
        /// <summary>
        /// Returns the minimum integar of the Min Heap, remove the min and reforms the heap. Throws an error if empty.
        /// </summary>
        /// <returns></returns>
        public int ExtractMin()
        {
            if (Heap.Count == 0) throw new NullReferenceException("There is no integar in the list!");
            else
            {
                int min = Heap[0];
                Heap[0] = Heap[Heap.Count - 1];
                Heap.RemoveAt(Heap.Count - 1);
                int parentindex = 0;
                if(Heap.Count>1)
                {
                    while(2*parentindex+1 < Heap.Count)
                    {
                        if(Heap[parentindex]>Heap[parentindex*2+1] || Heap[parentindex] > Heap[parentindex * 2 + 2])
                        {
                            if (parentindex * 2 + 2 <= Heap.Count - 1)
                            {
                                if (Heap[parentindex * 2 + 1] < Heap[parentindex * 2 + 2])
                                {
                                    Swap(parentindex, parentindex * 2 + 1);
                                    parentindex = parentindex * 2 + 1;
                                }
                                else
                                {
                                    Swap(parentindex, parentindex * 2 + 2);
                                    parentindex = parentindex * 2 + 2;
                                }
                            }
                            else
                            {
                                Swap(parentindex, parentindex * 2 + 1);
                                parentindex = parentindex * 2 + 1;
                            }
                        }
                    }
                }

                return min;
            }
        }

        /// <summary>
        /// Returns the list of integars in the Min Heap.
        /// </summary>
        /// <returns></returns>
        public List<int> GetHeap()
        {
            return Heap;
        }

        private void Swap(int index1, int index2)
        {
            int temp = Heap[index1];
            Heap[index1] = Heap[index2];
            Heap[index2] = temp;
        }
    }
}
