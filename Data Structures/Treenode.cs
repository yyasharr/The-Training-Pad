using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    public class Treenode : IComparable
    {
        public int data;
        public Treenode left;
        public Treenode right;

        public Treenode(int d)
        {
            data = d;
            left = null;
            right = null;
        }
        //Traversals:
        public List<int> InOrderTraversal()
        {
            List<int> nodes = new List<int>();
            this.InOrderTraversal_Helper(nodes);
            return nodes;
        }
        private void InOrderTraversal_Helper(List<int> nodes)
        {
            if (this == null) return;

            if (this.left != null)
                this.left.InOrderTraversal_Helper(nodes);

            nodes.Add(this.data);

            if (this.right != null)
                this.right.InOrderTraversal_Helper(nodes);

        }

        public List<int> PreOrderTraversal()
        {
            List<int> nodes = new List<int>();
            this.PreOrderTraversal_Helper(nodes);
            return nodes;
        }

        private void PreOrderTraversal_Helper(List<int> nodes)
        {
            if (this == null) return;

            nodes.Add(this.data);

            if (this.left != null)
                this.left.PreOrderTraversal_Helper(nodes);

            if (this.right != null)
                this.right.PreOrderTraversal_Helper(nodes);
        }

        public List<int> PostOrderTraversal()
        {
            List<int> nodes = new List<int>();
            this.PostOrderTraversal_Helper(nodes);
            return nodes;
        }

        private void PostOrderTraversal_Helper(List<int> nodes)
        {
            if (this == null)
                return;

            if (this.left != null)
                this.left.PostOrderTraversal_Helper(nodes);

            if (this.right != null)
                this.right.PostOrderTraversal_Helper(nodes);

            nodes.Add(this.data);
        }

        /// <summary>
        /// Returns hegiht of a given Treenode.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int Height(Treenode root)
        {
            if (root == null)
                return 0;
            return Math.Max(Height(root.left), Height(root.right)) + 1;
        }

        public int Height()
        {
            return Treenode.Height(this);
        }

        public void Visualize()
        {
            if (this == null) return;
            int height = Height(this);
            PerfectTree perfecttree = new PerfectTree(height);
            PerfectTreeNode proot = perfecttree.root;
            PrintComplete(this, proot);
        }
        public class PerfectTree
        {
            int height;
            public PerfectTreeNode root;
            public PerfectTree(int h)
            {
                height = h;
                root = BuildPerfectTree(h); //build an empty perfect tree

            }
            PerfectTreeNode BuildPerfectTree(int h)
            {
                if (h == 0)
                    return null;
                int increasecol = Convert.ToInt32(Math.Pow(2, h));
                PerfectTreeNode root = new PerfectTreeNode();
                Queue<PerfectTreeNode> q = new Queue<PerfectTreeNode>();
                q.Enqueue(root);
                int level = 1;
                while (level <= h)
                {
                    int startcol = Convert.ToInt32(Math.Pow(2, (h - level)) - 1); //2^(h-level)-1;
                    int r = (level - 1) * 2;
                    int count = q.Count;
                    while (count > 0)
                    {
                        PerfectTreeNode curr = q.Dequeue();
                        curr.row = r;
                        curr.col = startcol;
                        PerfectTreeNode left = new PerfectTreeNode();
                        PerfectTreeNode right = new PerfectTreeNode();
                        curr.left = left;
                        curr.right = right;
                        q.Enqueue(left);
                        q.Enqueue(right);
                        count--;
                        startcol += increasecol;
                        increasecol /= 2;

                    }
                    level++;
                }
                return root;
            }

        }
        public void PrintComplete(Treenode broot, PerfectTreeNode proot) //Print BinaryTree elements with corresponding spots from complete tree
        {
            if (broot == null) return;

            //proot.value = broot.data.ToString();

            Console.SetCursorPosition(proot.col, proot.row);
            Console.Write(broot.data);

            PrintComplete(broot.left, proot.right);
            PrintComplete(broot.right, proot.right);
        }
        public class PerfectTreeNode
        {
            public string value = "";
            public PerfectTreeNode left;
            public PerfectTreeNode right;
            public int row = 0;
            public int col = 0;
            public PerfectTreeNode()
            {
            }

        }

        /// <summary>
        /// Returns true if the tree starting at this Treenode is a Binary Search Tree.
        /// </summary>
        /// <returns></returns>
        public bool ValidateBST()
        {
            return ValidateBST(this, int.MinValue, int.MaxValue);
        }
        private bool ValidateBST(Treenode root, int minValue, int maxValue)
        {
            if (root == null) return true;

            if (root.data >= maxValue || root.data < minValue) return false;

            return ValidateBST(root.left, minValue, root.data) && ValidateBST(root.right, root.data, maxValue);
        }
        public Treenode DeleteNode_BST(int key)
        {
            if (this == null || this.ValidateBST() == false) return this;

            if (key > this.data)
                this.right = this.right.DeleteNode_BST(key);

            else if (key < this.data)
                this.left = this.left.DeleteNode_BST(key);

            else
            {
                if (this.left == null)
                    return this.right;

                if (this.right == null)
                    return this.left;

                this.data = this.left.FindBiggestOnLeft();

                this.left = this.left.DeleteNode_BST(this.data);
            }
            return this;
        }
        private int FindBiggestOnLeft()
        {
            if (this.right == null) return this.data;

            return this.right.FindBiggestOnLeft();
        }

        public override string ToString()
        {
            return this.data.ToString();
        }

        public int CompareTo(object obj)
        {
            return data.CompareTo(obj);
        }
        public int CompareTo(Treenode obj)
        {
            return data.CompareTo(obj.data);
        }
        public int Diameter()
        {
            return Diameter(0);
        }
        private int Diameter(int max)
        {
            if (this == null) return 0;

            int leftheight = (this.left != null) ? this.left.Height() : 0;
            int rightheight = (this.right != null) ? this.right.Height() : 0;

            max = Math.Max(max, leftheight + rightheight);

            if (this.left!=null) this.left.Diameter(max);
            if (this.right!=null) this.right.Diameter(max);

            return max;
        }
    }



}

#region sample initializers
#region Sample Treenode 1
//treenode one = new Treenode(1);
//Treenode two = new Treenode(2);
//Treenode three = new Treenode(3);
//Treenode four = new Treenode(4);
//Treenode five = new Treenode(5);
//Treenode six = new Treenode(6);
//Treenode seven = new Treenode(7);
//Treenode eight = new Treenode(8);

//five.left = three;
//five.right = seven;
//three.left = two;
//three.right = four;
//seven.left = six;
//seven.right = eight;
//six.left=one;


//         __5__
//        /     \
//       3       7
//      / \     / \
//     2   4   6   8
//            /
//           1
#endregion
#region Sample Treenode 2
//Treenode one = new Treenode(1);
//Treenode two = new Treenode(2);
//Treenode three = new Treenode(3);
//Treenode four = new Treenode(4);
//Treenode five = new Treenode(5);
//Treenode six = new Treenode(6);
//Treenode seven = new Treenode(7);
//Treenode eight = new Treenode(8);
//Treenode nine = new Treenode(9);
//Treenode ten = new Treenode(10);
//Treenode eleven = new Treenode(11);
//Treenode twelve = new Treenode(12);
//Treenode thirteen = new Treenode(13);
//Treenode fourteen = new Treenode(14);


//five.left = three;
//five.right = seven;
//seven.left = six;
//seven.right = eight;
//six.left = one;
//six.right = nine;
//nine.right = ten;
//ten.right = two;
//two.right = thirteen;
//one.left = eleven;
//eleven.left = twelve;
//twelve.left = four;

//          __5__
//        /       \
//       3         7
//                / \
//               6   8
//              / \
//             1   9
//            /     \
//           11      10
//          /         \
//         12          2
//        /             \
//       4               13
//
#endregion
#endregion