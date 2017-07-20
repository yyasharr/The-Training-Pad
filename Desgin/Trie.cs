using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad.Desgin
{
    public class Trie //For solving Q208 from Leetcode
    {
        TrieNode root;

        /** Initialize your data structure here. */
        public Trie()
        {
            root = new TrieNode();
        }

        /** Inserts a word into the trie. */
        public void Insert(string word)
        {
            TrieNode[] children = root.Children;
            for (int i = 0; i < word.Length; i++)
            {
                int ch = word[i] - 'a';
                if (children[ch] != null) //if we have seen it before
                {
                    if (i == word.Length - 1)
                        children[ch].isLeaf = true;
                }
                //if we have not seen this at this level.
                else
                {
                    children[ch] = new TrieNode(word[i]);
                    if (i == word.Length - 1)//last char
                    {
                        children[ch].isLeaf = true;
                    }
                }
                children = children[ch].Children;
            }
        }

        /** Returns if the word is in the trie. */
        public bool Search(string word)
        {
            TrieNode[] children = root.Children;

            for (int i = 0; i < word.Length; i++)
            {
                int ch = word[i] - 'a';
                if (children[ch] == null)
                    return false;
                else
                {
                    if (i == word.Length - 1 && children[ch].isLeaf == true)//last character
                        return true;

                    children = children[ch].Children;
                }
            }
            return false;
        }

        public List<string> Suggestions(string prefix)
        {
            TrieNode current_node = getCurrentNode(prefix);
            List<string> result = new List<string>();

            if (current_node == null) //means we couldn't find the prefix in the trienode
                return result;

            GenerateSuggestions(prefix, current_node, result);

            return result;

        }

        private void GenerateSuggestions(string prefix, TrieNode current_node, List<string> result)
        {
            for (int i = 0; i < 26; i++)
            {
                if (current_node.Children[i] != null) //means we can continue from here
                {
                    char ch = Convert.ToChar(i + 'a');
                    string new_prefix = prefix + ch;
                    if (current_node.Children[i].isLeaf == true)
                    {
                        result.Add(new_prefix);
                    }
                    GenerateSuggestions(new_prefix, current_node.Children[i], result);
                }
            }
        }

        private TrieNode getCurrentNode(string prefix)
        {
            TrieNode curr = root;

            for (int i = 0; i < prefix.Length; i++)
            {
                int ch = prefix[i] - 'a';
                if (curr.Children[ch] != null)//
                {
                    curr = curr.Children[ch];
                    if (i == prefix.Length - 1)//last char of prefix found!
                    {
                        return curr;
                    }
                }
                else
                    return null;
            }
            return null;
        }

        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            TrieNode[] children = root.Children;
            for (int i = 0; i < prefix.Length; i++)
            {
                int ch = prefix[i] - 'a';
                if (children[ch] == null)
                    return false;
                else
                {
                    if (i == prefix.Length - 1)
                        return true;
                }
                children = children[ch].Children;
            }
            return false;
        }
    }

    public class TrieNode
    {
        public char c;
        public TrieNode[] Children;
        public bool isLeaf;
        public TrieNode()
        {
            Children = new TrieNode[26];
        }

        public TrieNode(char ch)
        {
            c = ch;
            Children = new TrieNode[26];
        }
    }
    

}
//******* FOR TRYING SUGGESTION SYSTEM, YOU CAN TRY THE BELOW SAMPLE INITIALIZER IN program.cs FILE ******
//Desgin.Trie trie = new Desgin.Trie();
//trie.Insert("yashar");
//trie.Insert("yield");
//trie.Insert("yaghoub");
//trie.Insert("yalda");
//trie.Insert("yadollah");
//trie.Insert("yasser");
//trie.Insert("yalan");
//trie.Insert("yousef");
//trie.Insert("yanamna");
//trie.Insert("apple");
//trie.Insert("applet");
//trie.Insert("applause");
//trie.Insert("apologize");
//trie.Insert("appear");
//trie.Insert("apes");
//trie.Insert("api");
//trie.Insert("adidas");

//string pref = "";
//while(true)
//{
//    Console.SetCursorPosition(0,0);
//    Console.Write(pref);
//    pref += Console.ReadKey().KeyChar;
//    Console.Clear();
//    Console.SetCursorPosition(0, 1);
//    Print(trie.Suggestions(pref));


