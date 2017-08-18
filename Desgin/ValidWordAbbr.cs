using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class ValidWordAbbr //For Q288 from LeetCode
    {
        HashSet<string> set = new HashSet<string>();
        public ValidWordAbbr(string[] dictionary)
        {
            foreach (string s in dictionary)
            {
                set.Add((s.Length < 3) ? s : s[0] + (s.Length - 1).ToString() + s[s.Length - 1]);
            }
        }

        public bool IsUnique(string word)
        {
            string check= (word.Length < 3) ? word : word[0] + (word.Length - 1).ToString() + word[word.Length - 1];
            return !set.Contains(check);                
        }
    }
}
