using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class Codec //Q297 from LeetCode
    {

        // Encodes a tree to a single string.
        public string serialize(Treenode root)
        {
            if (root == null) return null;
            int height = Height(root);
            Queue<Treenode> q = new Queue<Treenode>();
            q.Enqueue(root);
            int level = 1;
            List<string> nodes = new List<string>();

            while (q.Any())//while q is not empty
            {
                int count = q.Count; //count of elements in this new level
                while (count > 0)
                {
                    Treenode curr = q.Dequeue();
                    if (curr == null) nodes.Add("n");
                    else
                    {
                        nodes.Add(curr.data.ToString());

                        if (curr.left == null)
                        {
                            if (level < height)
                                q.Enqueue(null);
                        }
                        else q.Enqueue(curr.left);

                        if (curr.right == null)
                        {
                            if (level < height)
                                q.Enqueue(null);
                        }
                        else q.Enqueue(curr.right);

                    }
                    count--;
                }
                level++;
            }

            while (nodes[nodes.Count - 1] == "n") nodes.RemoveAt(nodes.Count - 1);

            string result = "";
            for (int i = 0; i < nodes.Count; i++)
            {
                result += nodes[i];
                if (i != nodes.Count - 1)
                    result += ",";
            }
            return result;

        }

        private int Height(Treenode root)
        {
            if (root == null) return 0;
            return Math.Max(Height(root.left), Height(root.right)) + 1;
        }

        // Decodes your encoded data to tree.
        public Treenode deserialize(string data)
        {
            if (data == null) return null;

            string[] nodes = data.Split(',');

            Treenode root = new Treenode(int.Parse(nodes[0]));

            Queue<Treenode> q = new Queue<Treenode>();

            q.Enqueue(root);

            int i = 1;

            while(q.Any())
            {
                Treenode curr = q.Dequeue();

                if(i<nodes.Length) //check left
                {
                    string left = nodes[i];
                    if(left!="n")
                    {
                        Treenode l = new Treenode(int.Parse(left));
                        curr.left = l;
                        q.Enqueue(l);
                    }
                    i++;
                }
                if (i < nodes.Length) //check right
                {
                    string right = nodes[i];
                    if (right != "n")
                    {
                        Treenode r = new Treenode(int.Parse(right));
                        curr.right = r;
                        q.Enqueue(r);
                    }
                    i++;
                }
            }
            return root;
        }
    }
}
