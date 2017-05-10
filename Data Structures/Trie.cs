using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class Trie
    {
        private Trienode Root;

        public Trie()
        {
            Root = new Trienode();
        }

        public void AddWord(string Word)
        {
            Dictionary<char, Trienode> Children = Root.Children;

            for (int i = 0; i < Word.Length; i++)
            {
                Trienode temp;
                if (Children.ContainsKey(Word[i]))
                {
                    temp = Children[Word[i]];
                }
                else
                {
                    temp = new Trienode(Word[i]);
                    Children.Add(Word[i], temp);
                }
                Children = temp.Children;
                if (i == Word.Length - 1)
                {
                    temp.isLeaf = true;
                }
            }
        }

        public bool Search(string Word)
        {
            if (Word == "") return false;
            Dictionary<char, Trienode> Children = Root.Children;
            Trienode temp = null;
            for (int i = 0; i < Word.Length; i++)
            {
                if (!Children.ContainsKey(Word[i]))
                {
                    return false;
                }
                else
                {
                    temp = Children[Word[i]];
                    Children = temp.Children;
                }
            }
            if (temp.isLeaf == true)
                return true;
            return false;
        }
    }
}
