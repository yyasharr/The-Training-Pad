using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace My_Training_Pad
{
    class Program
    {
        static bool Search(int num, int[,] matrix, int m, int n) //m: number of rows, n: number of columns
        {
            if (num < matrix[0, 0] || num > matrix[m - 1, n - 1]) return false;
            return SearchUtil(num, matrix, 0, m, 0, n);
        }
        static bool SearchUtil(int num, int[,] matrix, int minI, int maxI, int minJ, int maxJ)
        {
            while (minI <= maxI)
            {
                int midI = (minI + maxI) / 2;
                int midJ = (minJ + maxJ) / 2;

                if (matrix[midI, midJ] == num)
                    return true;

                if (num > matrix[midI, midJ]) //If the number is greater than the middle element, it's either on the same row on middle's right side, or the next rows anywere.
                {
                    if (num <= matrix[midI, maxJ - 1]) //if it's smaller or equal to the largest element in this row; 
                    {
                        return SearchUtil(num, matrix, midI, midI, midJ, maxJ); //it's on the same row, to the right of the middle element.
                    }
                    else //if it's bigger than the middle element;
                    {
                        return SearchUtil(num, matrix, midI + 1, maxI, 0, maxJ); //it's for sure on one of the next rows.
                    }
                }

                if (num < matrix[midI, midJ]) //If the number is smaller than the middle element, it's either on the same row on middle's left side, or the previous rows anywere.
                {
                    if (num >= matrix[midI, 0]) //if it's greater or equal to the smallest element in this row; 
                    {
                        return SearchUtil(num, matrix, midI, midI, 0, midJ); //it's on the same row, to the left of the middle element.
                    }
                    else
                    {
                        return SearchUtil(num, matrix, 0, midI - 1, 0, maxJ); //it's for sure on one of the previous rows.
                    }
                }
            }

            return false;

        }

        ///////////////////////////////Linked Lists/////////////////////////////////////////
        static void remove_duplicate(Node head)
        {
            HashSet<int> set = new HashSet<int>();
            if (head.next == null || head == null) return;

            Node prev = null;
            while (head != null)
            {
                if (!set.Contains(head.data))
                {
                    set.Add(head.data);
                    prev = head;
                }
                else
                {
                    prev.next = head.next;
                }
                head = head.next;
            }
        }
        static Node Reverse(Node head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }
            Node Next = head.next;
            head.next = null;
            Node rest = Reverse(Next);
            Next.next = head;
            return rest;
        }
        //Swap 2 nodes given as int values

        ///////////////////////////////Trees/////////////////////////////////////////
        //↓Find Path: find the path between root and a given node in binary tree, empty list if doesn't exist
        static bool FindPath(Treenode root, int val, List<Treenode> path)
        {
            if (root == null) return false;

            path.Add(root);

            if (root.data == val) return true;

            //if (root.left != null)
            if (FindPath(root.left, val, path)) return true;
            //if (root.right != null)
            if (FindPath(root.right, val, path)) return true;

            path.RemoveAt(path.Count - 1);
            return false;

        }

        ////////////////// STRING PRINTS /////////////////////
        static void Permutations(string input, string output)
        {
            if (input == "")
                Console.WriteLine(output);
            for (int i = 0; i < input.Length; i++)
            {
                string next_output = output + input[i];
                string next_input = input.Substring(0, i) + input.Substring(i + 1);

                Permutations(next_input, next_output);
            }
        }
        static void Subsets(string input, string output)
        {
            if (input == "")
                Console.WriteLine(output);
            else
            {
                string test = "1";
                int test1 = int.Parse(test);
                Subsets(input.Substring(1), output + input[0]);
                Subsets(input.Substring(1), output);
            }
        }
        static void Place_Spaces(string input, string output)
        {
            if (input == "")
                return;
            if (output != "")
            {
                Console.WriteLine(output + " " + input);
            }
            //either send the next output with space or without space
            Place_Spaces(input.Substring(1), output + input[0]);
            if (output != "")
            {
                Place_Spaces(input.Substring(1), output + " " + input[0]);
            }

        }
        ///////////////////////////////////////////////////////
        static int FindDup(int[] array)
        {
            int index1 = 0;
            int index2 = 0;
            while (true)
            {
                index2 = array[index1];
                if (index2 == -1)
                    return index1;
                array[index1] = -1;
                index1 = index2;
            }
        }
        static void FindSubstringInWraproundString(string input, string output)
        {
            if (input == "")
            {
                //if (output.Length == 1)
                Console.WriteLine(output);

            }
            else
            {
                FindSubstringInWraproundString(input.Substring(1), output);
                if (output == "" || input[0] - output[output.Length - 1] == 1 || (output[output.Length - 1] == 'z' && input[0] == 'a'))
                {
                    FindSubstringInWraproundString(input.Substring(1), output + input[0]);
                }
            }
        }
        ////////////////// Path Sums /////////////////////
        // Path Sum 1 (https://leetcode.com/problems/path-sum/)
        static bool PathSum1(Treenode root, int sum)
        {
            return PathSum1(root, sum, 0);
        }
        static bool PathSum1(Treenode root, int sum, int cur_sum)
        {
            if (root == null) return false;
            if (root.left == null && root.right == null)
            {
                cur_sum += root.data;
                if (cur_sum == sum)
                    return true;
            }

            return (PathSum1(root.left, sum, cur_sum + root.data) || PathSum1(root.right, sum, cur_sum + root.data));
        }
        // Path Sum2 
        static List<List<int>> PathSum2(Treenode root, int sum)
        {
            if (root == null) return new List<List<int>>();
            List<List<int>> total_path = new List<List<int>>();
            PathSum2(root, sum, new List<int>(), total_path);
            return total_path;
        }
        static void PathSum2(Treenode root, int sum, List<int> cur_path, List<List<int>> total_path)
        {
            cur_path.Add(root.data);
            if (root.left == null && root.right == null)
                if (sum == root.data)
                    total_path.Add(cur_path);
            if (root.left != null)
                PathSum2(root.left, sum - root.data, cur_path, total_path);
            if (root.right != null)
                PathSum2(root.right, sum - root.data, cur_path, total_path);
            cur_path.Remove(cur_path.Count - 1);
        }
        // Path Sum3

        //////////////////////////////////////////////////
        static int[] TwoSum(int[] array, int sum)
        {
            Dictionary<int, int> valueindexmap = new Dictionary<int, int>();
            return TwoSum(array, sum, valueindexmap);
        }
        static int[] TwoSum(int[] array, int sum, Dictionary<int, int> valueindexmap)
        {
            for (int i = 0; i < array.Length; i++)
            {
                int required = sum - array[i];
                if (valueindexmap.ContainsKey(required))
                {
                    return new int[] { valueindexmap[required], i };
                }
                else
                {
                    valueindexmap.Add(array[i], i);
                }
            }
            return new int[] { };
        }
        //////////////Subsets//////////////////
        static List<List<int>> Subs(int[] nums)
        {
            List<int> sub = new List<int>();
            List<List<int>> total_subs = new List<List<int>>();
            return Subs(nums, 0, sub, total_subs);
        }
        static List<List<int>> Subs(int[] nums, int index, List<int> sub, List<List<int>> total_subs)
        {
            if (index >= nums.Length)
            {
                total_subs.Add(sub);
            }
            if (index < nums.Length)
            {
                List<int> next_sub = new List<int>(sub);
                next_sub.Add(nums[index]);
                Subs(nums, index + 1, sub, total_subs);
                Subs(nums, index + 1, next_sub, total_subs);
            }
            return total_subs;
        }
        static List<List<int>> Subset_Sum(int[] array, int target)
        {
            List<List<int>> Total_Subs = new List<List<int>>();
            List<int> Current_Sub = new List<int>();

            return Subset_Sum(array, target, Current_Sub, Total_Subs, 0);

        }
        static List<List<int>> Subset_Sum(int[] array, int target, List<int> current_sub, List<List<int>> total_subs, int index)
        {
            if (index >= array.Length)
            {
                if (target == 0)
                    total_subs.Add(current_sub);
            }
            else
            {
                List<int> next_sub = new List<int>(current_sub);
                next_sub.Add(array[index]);

                Subset_Sum(array, target - array[index], next_sub, total_subs, index + 1);
                Subset_Sum(array, target, current_sub, total_subs, index + 1);

            }
            return total_subs;
        }
        //////////////////////////////////////////////////////////////////////////////////////////////
        static int minimumdepth(Treenode root)
        {
            if (root == null)
                return 0;
            if (root.left == null && root.right == null)
                return 1;
            return Math.Min(minimumdepth(root.left), minimumdepth(root.right)) + 1;
        }
        static void PrintSkyline(int[] array)
        {
            int i = 0; int max = 0;
            for (i = 0; i < array.Length; i++)
            {
                if (array[i] >= max) max = array[i];
            }
            i = 0;
            while (i < max)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    if (array[j] + i >= max)
                        Console.Write("*" + "\t");
                    else
                        Console.Write(" " + "\t");
                }
                Console.WriteLine();
                i++;
            }
        }
        ////////////////////////REMOVE DUPLICATE CHARACTER////////////////////////////
        static string RemoveDuplicateChar(string str)
        {
            char next = ' ';
            Dictionary<char, int> alphabet = new Dictionary<char, int>();

            for (char c = 'a'; c <= 'z'; c++)
            {
                alphabet.Add(c, -1);
            }
            for (int i = 0; i < str.Length; i++)
            {
                char cur = str[i];
                if (alphabet[cur] == -1)
                {
                    alphabet[cur] = i;
                }
                else
                {
                    if (alphabet[cur] - 1 != i)
                    {

                        for (int j = alphabet[cur] + 1; j < i; j++)
                        {
                            if (alphabet.ContainsValue(j))
                            {
                                foreach (char ch in alphabet.Keys)
                                {
                                    if (alphabet[ch] == j)
                                        next = ch;
                                    break;
                                }
                            }
                        }
                        if (cur >= next)
                        {

                            alphabet[cur] = i;
                        }

                    }
                }
            }
            string res = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (alphabet.ContainsValue(i))
                {
                    foreach (char ch in alphabet.Keys)
                    {
                        if (alphabet[ch] == i)
                            res += ch;
                    }
                }
            }
            return res;
        }
        ////////////////////↓Longest Palindromic Subsequence↓///////////////////////////
        static int LPS(string str)
        {
            return LPS(str, 0, str.Length - 1);
        }
        static int LPS(string str, int start, int end)
        {
            if (start > end) return 0;
            if (start == end) return 1;
            if (str[start] == str[end]) return 2 + LPS(str, start + 1, end - 1);
            return Math.Max(LPS(str, start + 1, end), LPS(str, start, end - 1));
        }
        ////////////////↓longest substring without repeating characters↓////////////////
        static int longestUniqueSubsttr(string str)
        {//http://www.geeksforgeeks.org/length-of-the-longest-substring-without-repeating-characters/

            int cur_len = 0;
            int max_len = 0;
            Dictionary<char, int> dict = new Dictionary<char, int>();

            for (int i = 0; i < str.Length; i++)
            {
                if (!dict.ContainsKey(str[i]))
                {
                    cur_len++;
                    if (max_len < cur_len)
                        max_len = cur_len;
                    dict.Add(str[i], i);
                }
                else if (dict.ContainsKey(str[i]))
                {
                    int last_index = dict[str[i]];
                    if (i - cur_len > last_index)
                    {
                        cur_len++;
                        if (cur_len > max_len)
                            max_len = cur_len;
                    }
                    else
                    {
                        cur_len = i - last_index;
                    }
                    dict[str[i]] = i;
                }
            }
            return max_len;
        }
        //////////////////////////////Cutting a Rod//////////////////////////////////////
        //http://www.geeksforgeeks.org/dynamic-programming-set-13-cutting-a-rod/ (Dynamic Programming)
        static int Cutting_Rod(int[] array, int n) //Naive solution. Recursively
        {
            if (n <= 0) return 0;

            int max = int.MinValue;

            for (int i = 0; i < n; i++)
            {
                max = Math.Max(max, array[i] + Cutting_Rod(array, n - i - 1));
            }
            return max;
        }
        static int Cutting_Rod_DP(int[] array, int n)
        {
            int[] DP = new int[n + 1];
            DP[0] = 0;

            for (int i = 1; i <= n; i++)
            {
                int max = int.MinValue;
                for (int j = 0; j < i; j++)
                {
                    max = Math.Max(max, array[j] + DP[i - j - 1]);
                }
                DP[i] = max;
            }
            return DP[n];
        }
        /////////////////////////////////////////////////////////////////////////////////
        static bool ValidateBST(Treenode root)
        {
            return ValidateBST(root, int.MinValue, int.MaxValue);
        }
        static bool ValidateBST(Treenode root, int minValue, int maxValue)
        {
            if (root == null) return true;
            if (root.data >= maxValue || root.data < minValue) return false;
            return (ValidateBST(root.left, minValue, root.data) && ValidateBST(root.right, root.data, maxValue));
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        static void AllCombinations(int Sum, int n) //Question: https://www.careercup.com/question?id=4917596202729472
        {
            AllCombinations(Sum, n, "");
        }
        private static void AllCombinations(int sum, int n, string output)
        {
            if (n == 1)
            {
                Console.WriteLine(output + sum);
            }
            else
            {
                for (int i = sum; i >= 0; i--)
                {
                    string new_output = output + i + ",";
                    AllCombinations(sum - i, n - 1, new_output);
                }
            }
        }
        /////////////////////////////////Remove a node from BST/////////////////////////////////////////////
        static Treenode DeleteNode(Treenode root, int key) //removes the given node and return the new head
        {
            if (root == null) return null;

            if (key > root.data)
                root.right = DeleteNode(root.right, key);
            else if (key < root.data)
                root.left = DeleteNode(root.left, key);

            else
            {
                if (root.left == null)
                    return root.right;
                else if (root.right == null)
                    return root.left;

                //if the code reaches here, it means the root has two children!
                //we can either find biggest node of left or smallest node on right, and upadte root's value with that and remove it.
                //here we do it with left's biggest
                root.data = FindBiggest(root.left);

                //recursively remove the biggest node on left
                root.left = DeleteNode(root.left, root.data);

            }
            return root;
        }
        private static int FindBiggest(Treenode root)
        {
            if (root.right == null)
                return root.data;

            return FindBiggest(root.right);
        }
        ///////////////////////////////Finding the shortest path in Maze using BFS///////////////////////////////////////
        class QueueNode
        {
            public Point Loc; //location
            public int Distance; //distance of the current cell from source
            public QueueNode(Point loc, int distance)
            {
                Loc = loc;
                Distance = distance;
            }
        }
        static int ShortestPath(int[,] maze, Point src, Point dst)
        {
            int m = maze.GetLength(0);
            int n = maze.GetLength(1);
            if (maze == null || (m == 0 && n == 0)) return 0;
            int[,] visited = new int[m, n];
            Point[] dirs = { new Point(0, 1), new Point(0, -1), new Point(1, 0), new Point(-1, 0) };
            int count = 0;
            Queue<QueueNode> q = new Queue<QueueNode>();
            q.Enqueue(new QueueNode(src, 0));
            while (q.Any())
            {
                QueueNode qn = q.Dequeue();
                Point curr = qn.Loc;
                count = qn.Distance;
                if (curr.X < 0 || curr.Y < 0 || curr.X >= m || curr.Y >= n || curr.X == 1 || visited[curr.X, curr.Y] == 1)//if current cell is not valid
                {
                    continue;
                }
                else
                {
                    visited[curr.X, curr.Y] = 1;
                    count++;
                    if (curr.X == dst.X && curr.Y == dst.Y)
                    {
                        return qn.Distance;
                    }
                    else
                    {
                        for (int i = 0; i < dirs.Length; i++)
                        {
                            q.Enqueue(new QueueNode(new Point(curr.X + dirs[i].X, curr.Y + dirs[i].Y), count));
                        }
                    }
                }
            }
            return count;
        }

        static void Main(string[] args)
        {
            /////////////////////////initializers Below///////////////////////////
            Treenode one = new Treenode(1);
            Treenode two = new Treenode(2);
            Treenode three = new Treenode(3);
            one.left = two;
            one.right = three;
            /////////////////////////Start Time Below//////////////////////////////
            DateTime start = DateTime.Now;
            /////////////////////////Functions Below//////////////////////////////
            Console.WriteLine(LeetCode.LowestCommonAncestor(one, two, three).data);
            /////////////////////////End Time Below///////////////////////////////
            Console.WriteLine("time: " + (DateTime.Now - start).TotalSeconds);

            
            
            Console.ReadKey();

        }
    }
}
