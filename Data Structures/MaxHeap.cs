using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    class MaxHeap
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
        /// Returns the Maximum element of the heap without removing it. Throws an error if empty.
        /// </summary>
        public int Max
        {
            get
            {
                if (Heap.Count == 0) throw new NullReferenceException();
                else return Heap[0];
            }
        }

        /// <summary>
        /// Constructs an empty Max Heap.
        /// </summary>
        public MaxHeap()
        {
            Heap = new List<int>();
        }

        /// <summary>
        /// Inserts a new integar to the Max Heap and reforms the heap.
        /// </summary>
        /// <param name="n"></param>
        public void Insert(int n)
        {
            Heap.Add(n);
            int childindex = Heap.Count - 1;
            int parentindex = (childindex - 1) / 2;
            while (parentindex >= 0 && Heap[childindex] > Heap[parentindex])
            {
                Swap(childindex, parentindex);
                childindex = parentindex;
                parentindex = (childindex - 1) / 2;
            }
        }

        /// <summary>
        /// Returns the Maximum integar of the Max Heap, removes the max and reforms the heap. Throws an error if empty.
        /// </summary>
        /// <returns></returns>
        public int ExtractMin()
        {
            if (Heap.Count == 0) throw new NullReferenceException("There is no integar in the list!");
            else
            {
                int max = Heap[0];
                Heap[0] = Heap[Heap.Count - 1];
                Heap.RemoveAt(Heap.Count - 1);
                int parentindex = 0;
                if (Heap.Count > 1)
                {
                    while (2 * parentindex + 1 < Heap.Count)
                    {
                        if (Heap[parentindex] < Heap[parentindex * 2 + 1] || Heap[parentindex] < Heap[parentindex * 2 + 2])
                        {
                            if (parentindex * 2 + 2 <= Heap.Count - 1)
                            {
                                if (Heap[parentindex * 2 + 1] > Heap[parentindex * 2 + 2])
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
                return max;
            }
        }

        /// <summary>
        /// Returns the list of integars in the Max Heap.
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
