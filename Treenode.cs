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

    }
}
///////////////////////////////////↓↓↓↓Sample Initializer///////////////////////////////////↓↓↓↓
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
////////////////////////////////////////////////////////////////////////////////////////////////////
