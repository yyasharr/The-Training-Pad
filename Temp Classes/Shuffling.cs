using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class Shuffling //For Q384 on Leetcode
    {
        int[] input;
        public Shuffling(int[] nums)
        {
            input = nums;
        }
        /** Resets the array to its original configuration and return it. */
        public int[] Reset()
        {
            return input;
        }
        /** Returns a random shuffling of the array. */
        public int[] Shuffle()
        {
            List<int> inputlist = new List<int>(input);
            return shuffle_helper(inputlist, new int[input.Count()], 0);
        }
        int[] shuffle_helper(List<int> inputlist, int[] output, int index)
        {
            if (inputlist.Count == 0)
                return output;
            Random rnd = new Random();
            int num = rnd.Next(0, inputlist.Count - 1);

            output[index] = inputlist[num];
            inputlist.RemoveAt(num);
            shuffle_helper(inputlist, output, index + 1);

            return output;
        }
    }
}
