using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    class MaxHeap<T> where T:IComparable<T>
    {
        private List<T> Heap;

        public MaxHeap()
        {
            Heap = new List<T>();
        }

        public T Max
        {
            get
            {
                if (Heap.Count == 0) throw new NullReferenceException();
                else return Heap[0];
            }
        }
        public bool IsEmpty()
        {
            return Heap == null || Heap.Count == 0;
        }

        public int Count
        {
            get
            {
                if (Heap == null) return 0;
                else
                {
                    return Heap.Count;
                }
            }
        }

        public void Insert(T data)
        {
            Heap.Add(data);

            int ch = Heap.Count - 1; //ch for child
            while (ch > 0)
            {
                int p = (ch - 1) / 2; //p for parent

                if (Heap[ch].CompareTo(Heap[p]) > 0) //meaning child is bigger than parent -> Swap'm
                {
                    Swap(p, ch);
                    ch = p;
                }
                else
                    break;
            }
        }

        private void Swap(int p, int ch)
        {
            T temp = Heap[p];
            Heap[p] = Heap[ch];
            Heap[ch] = temp;
        }

        public T ExtractMax()
        {
            if (Heap == null || Heap.Count == 0) throw new NullReferenceException();
            T ret = Heap[0];

            Heap[0] = Heap[Heap.Count - 1];
            Heap.RemoveAt(Heap.Count - 1);

            Heapify(0);

            return ret;
        }
        private void Heapify(int i) //i is parent
        {
            int ch1 = 2 * i + 1; //left child
            int ch2 = 2 * i + 2; // right child

            if (ch1 >= Heap.Count && ch2 >= Heap.Count) return;

            int bigger = (ch2 >= Heap.Count) ? ch1 : (Heap[ch1].CompareTo(Heap[ch2]) > 0) ? ch1 : ch2;
            //if ch2 is out of boundary, ch1 is bigger, don't check rest, otherwise, see which one has bigger data

            if (Heap[i].CompareTo(Heap[bigger]) < 0)
            {
                Swap(i, bigger);
                Heapify(bigger);
            }

        }
    }
}
