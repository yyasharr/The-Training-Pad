using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad //Q295 from LeetCode
{
    public class MedianFinder
    {
        MinHeap minheap;
        MaxHeap maxheap;
        
        /** initialize your data structure here. */
        public MedianFinder()
        {
            minheap = new MinHeap();
            maxheap = new MaxHeap(); ;
        }

        public void AddNum(int num) //Always trying to keep maxheap.Count>=minheap.Count
        {
            if (maxheap.Count == 0) maxheap.Insert(num);
            else if(maxheap.Count>minheap.Count)
            {
                if(num>=maxheap.Max) //it belongs to minheap, and there is enough space
                {
                    minheap.Insert(num);
                }
                else //it belongs to maxheap, but there is no enough space
                {
                    minheap.Insert(maxheap.ExtractMax());
                    maxheap.Insert(num);
                }
            }
            else if (maxheap.Count==minheap.Count)
            {
                if (num >= maxheap.Max) //it belongs to minheap, but there is no enough space
                {
                    maxheap.Insert(minheap.ExtractMin());
                    minheap.Insert(num);
                }
                else //it belongs to maxheap and there is enough space
                {
                    maxheap.Insert(num);
                }
            }

        }

        public double FindMedian()
        {
            int count = minheap.Count + maxheap.Count;
            if (count % 2 == 0) //if even, average of two middle ones
            {
                return Convert.ToDouble(minheap.Min + maxheap.Max) / 2;
            }
            else
                return maxheap.Max;
        }
    }

    /**
     * Your MedianFinder object will be instantiated and called as such:
     * MedianFinder obj = new MedianFinder();
     * obj.AddNum(num);
     * double param_2 = obj.FindMedian();
     */
}
