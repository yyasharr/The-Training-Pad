using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class Trienode
    {
        public char C;
        public bool isLeaf;
        public Dictionary<char, Trienode> Children = new Dictionary<char, Trienode>();

        public Trienode()
        {
        }
        public Trienode(Char c)
        {
            C = c;
        }

        
    }
}
