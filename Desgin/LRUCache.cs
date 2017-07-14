using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class LRUCache
    {
        Dictionary<int, FileNode> dict;
        int Capacity;
        FileNode First;
        FileNode Last;
        public LRUCache(int capacity)
        {
            dict = new Dictionary<int, FileNode>();
            Capacity = capacity;
        }

        public int Get(int key)
        {
            if (!dict.ContainsKey(key)) return -1;
            FileNode ret = dict[key];
            MoveFirst(ret); //since we already have it, we just need to move it to the first of list
            return ret.value;
        }

        private void MoveFirst(FileNode curr)
        {
            if (curr == First) return; //if it's already first element, do nothing!
            if (Last == curr) Last = curr.prev;
            if (curr.prev != null) curr.prev.next = curr.next;
            if (curr.next != null) curr.next.prev = curr.prev;
            curr.prev = null;
            curr.next = First;
            First.prev = curr;
            First = curr;
        }

        public void Put(int key, int value)
        {
            if (dict.ContainsKey(key)) //we already have it, just update the value and move first.
            {
                dict[key].value = value;
                MoveFirst(dict[key]);
            }
            else //it's a new file, create it, if needed remove last, and move this first.
            {
                //create the new file
                FileNode newfile = new FileNode(key, value);
                dict.Add(key, newfile);
                if (dict.Count <= Capacity) //if there is enough space
                {
                    if (dict.Count == 1) //if it first file to be cached
                    {
                        First = newfile;
                        Last = newfile;
                    }
                    else //if its not the first file we are caching
                    {
                        MoveFirst(newfile);
                    }
                }
                else //if there is not enough space
                {
                    dict.Remove(Last.key);
                    Last = Last.prev;
                    if (Last == null)//if there was only 1 element (if capacity==1)
                    {
                        Last = newfile;
                        First = newfile;
                    }
                    else
                    {
                        Last.next = null;
                        MoveFirst(newfile);
                    }
                }
            }
        }
    }
    public class FileNode
    {
        public int key;
        public int value;
        public FileNode next;
        public FileNode prev;

        public FileNode(int Key, int Value)
        {
            key = Key;
            value = Value;
        }
    }
}
