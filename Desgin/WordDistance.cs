using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class WordDistance
    {
        Dictionary<string, List<int>> indices;
        public WordDistance(string[] words)
        {
            indices = new Dictionary<string, List<int>>();

            for (int i = 0; i < words.Length; i++)
            {
                if (indices.ContainsKey(words[i]))
                {
                    indices[words[i]].Add(i);
                    indices[words[i]].Sort();
                }
                else
                {
                    List<int> temp = new List<int>();
                    temp.Add(i);
                    indices.Add(words[i], temp);
                }
            }
        }

        public int Shortest(string word1, string word2)
        {
            List<int> l1 = indices[word1];
            List<int> l2 = indices[word2];

            int diff = int.MaxValue;

            for(int i=0; i<l1.Count; i++)
            {
                for(int j=0; j<l2.Count;j++)
                {
                    diff = Math.Min(diff, Math.Abs(l1[i] - l2[j]));
                }
            }
            return diff;
        }
    }
}
