using System;
using System.Collections.Generic;
using System.Linq;
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
        //////https://leetcode.com/problems/binary-tree-level-order-traversal-ii /////////////////////
        static List<List<int>> Bottom_Level_Order(Treenode root)
        {
            List<List<int>> result = new List<List<int>>();
            if (root == null) return result;
            List<List<int>> temp = Bottom_Level_Order(root, 0, result);
            temp.Reverse();
            return temp;

        }
        static List<List<int>> Bottom_Level_Order(Treenode root, int level, List<List<int>> result)
        {
            if (result.Count <= level)
            {
                List<int> current = new List<int>();
                current.Add(root.data);
                result.Add(current);
            }
            else
            {
                List<int> current = result[level];
                current.Add(root.data);
            }
            if (root.left != null) Bottom_Level_Order(root.left, level + 1, result);
            if (root.right != null) Bottom_Level_Order(root.right, level + 1, result);
            return result;
        }
        /// ///////////////////////////////////////////////////////////////////////////////////////////
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
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        static int MinDistance(string word1, string word2) //https://leetcode.com/problems/edit-distance/?tab=Description
        {//wrapper class. Can call either method (recursive or tabular solution)
            return MinDistance(word1, word2, word1.Length, word2.Length);
            //return MinDistance_tabulation(word1, word2, word1.Length, word2.Length);
        }
        static int MinDistance(string word1, string word2, int ln1, int ln2)
        {
            if (ln1 == 0) return ln2;
            if (ln2 == 0) return ln1;

            if (word1.ElementAt(ln1 - 1) == word2.ElementAt(ln2 - 1))
            {
                return MinDistance(word1, word2, ln1 - 1, ln2 - 1);
            }

            return 1 + MinimumOfThree(MinDistance(word1, word2, ln1, ln2 - 1), MinDistance(word1, word2, ln1 - 1, ln2), MinDistance(word1, word2, ln1 - 1, ln2 - 1));
        }
        static int MinDistance_tabulation(string word1, string word2, int ln1, int ln2)
        {
            int[,] table = new int[ln1 + 1, ln2 + 1];

            for (int i = 0; i <= ln1; i++)
            {
                for (int j = 0; j <= ln2; j++)
                {
                    if (i == 0) table[i, j] = j;
                    else if (j == 0) table[i, j] = i;

                    else if (word1[i - 1] == word2[j - 1])
                    {
                        return MinDistance_tabulation(word1, word2, ln1 - 1, ln2 - 1);
                    }

                    else table[i, j] = 1 + MinimumOfThree(MinDistance_tabulation(word1, word2, ln1 - 1, ln2 - 1)
                                                      , MinDistance_tabulation(word1, word2, ln1 - 1, ln2)
                                                      , MinDistance_tabulation(word1, word2, ln1, ln2 - 1));
                }
            }
            return table[ln1, ln2];
        }
        static int MinimumOfThree(int i1, int i2, int i3)
        {
            int min = i1;
            if (i2 < min) min = i2;
            if (i3 < min) min = i3;
            return min;
        }
        ////////////////////////////////////////////////////////////////////////////////////////////////////
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
        /////////////////////////////////////////////////////////////////////////////
        ////////////////////////////Expression Add Operators/////////////////////////URL: https://leetcode.com/problems/expression-add-operators/
        static List<string> AddOperators(string num, int Target)
        {
            return AddOperators(num, Target, new List<string>());
        }
        static List<string> AddOperators(string num, int target, List<string> total_strings)
        {
            int n = num.Length;
            for (int i = 1; i < Math.Pow(4, n - 1); i++)
            {
                string operators = Convert.ToString(i, 2).PadLeft((n - 1) * 2, '0'); //SO IMPORTANT AND F*#&ing COOL: Generating two digit codes for each operator and n-1 spots to put them between. 00: nothing; 01: '+'; 10: '*'; 11: '-'
                string expression = num[0].ToString();
                for (int j = 1; j <= n - 1; j++)
                {
                    string Operator = operators.Substring((j - 1) * 2, 2);
                    //if (Operator == "00") SKIP; //not necessary
                    if (Operator == "01") expression += '+';
                    if (Operator == "10") expression += '*';
                    if (Operator == "11") expression += '-';
                    expression += num[j];
                }
                if (Calculator(expression) == target)
                    total_strings.Add(expression);

            }
            return total_strings;
        }
        static int Calculator(string exp)
        {
            Stack<int> st = new Stack<int>();
            int current_num = 0;
            char sign = '+';
            int result = 0;

            for (int i = 0; i < exp.Length; i++)
            {
                if (char.IsDigit(exp[i]))
                    current_num = current_num * 10 + int.Parse(exp[i].ToString());
                if (!char.IsDigit(exp[i]) || i == exp.Length - 1)
                {
                    if (sign == '+')
                        st.Push(current_num);
                    if (sign == '-')
                        st.Push(-1 * current_num);
                    if (sign == '*')
                    {
                        int temp = st.Pop();
                        st.Push(temp * current_num);
                    }
                    sign = exp[i];
                    current_num = 0;
                }
            }
            while (st.Any())
            {
                result += st.Pop();
            }
            return result;
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
        static void Main(string[] args)
        {
            char[,] grid = new char[4,5];
            grid[0, 0] = '1';
            grid[0, 1] = '1';
            grid[0, 2] = '1';
            grid[0, 3] = '1';
            grid[0, 4] = '0';

            grid[1, 0] = '1';
            grid[1, 1] = '1';
            grid[1, 2] = '0';
            grid[1, 3] = '1';
            grid[1, 4] = '0';

            grid[2, 0] = '1';
            grid[2, 1] = '1';
            grid[2, 2] = '0';
            grid[2, 3] = '0';
            grid[2, 4] = '0';

            grid[3, 0] = '0';
            grid[3, 1] = '0';
            grid[3, 2] = '0';
            grid[3, 3] = '0';
            grid[3, 4] = '0';

            Console.WriteLine(LeetCode.NumIslands(grid));
            Console.WriteLine("doe");
            Console.ReadLine();
        }
    }
}
