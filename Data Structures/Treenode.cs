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
    }

}

//Sample initializers:
///////////////////////////////////↓↓↓↓Sample Initializer1///////////////////////////////////↓↓↓↓
//Treenode one = new Treenode(1);
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
//six.left = one;

//         __5__
//        /     \
//       3       7
//      / \     / \
//     2   4   6   8
//            /
//           1

///////////////////////////////////↓↓↓↓Sample Initializer2///////////////////////////////////↓↓↓↓
//reenode one = new Treenode(1);
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

