using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace My_Training_Pad
{
    class Treenode
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
            if (root == null || (root.left == null && root.right == null))
                return 0;
            return Math.Max(Height(root.left),Height(root.right)) + 1;
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

