using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad.Desgin
{
    public class StringCodec
    {
        // Encodes a list of strings to a single string.
        public string encode(IList<string> strs)
        {
            StringBuilder ret = new StringBuilder();
            foreach (string s in strs)
            {
                ret.Append(s+"\n");                
            }
            return ret.ToString();
        }

        // Decodes a single string to a list of strings.
        public IList<string> decode(string s)
        {
            IList<string> ret = new List<string>();
            string current = "";

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\n')
                {
                    ret.Add(current);
                    current = "";
                }
                else
                    current += s[i];
            }
            return ret;
        }
    }
}
