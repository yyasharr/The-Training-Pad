using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My_Training_Pad
{
    class LeetCode //Questions from Leetcode.com
    {
        ////////////////////////////Q206/////////////////////////////////////////////////////////////
        public static Node Reverse_Iterative(Node head)
        {
            if (head == null || head.next == null) return head;
            Node prev = null;
            Node Next = head.next;
            while (head != null)
            {
                head.next = prev;
                prev = head;
                head = Next;
                if (Next != null)
                    Next = Next.next;
            }
            return prev;
        }
        public static Node Reverse_Recursively(Node head)
        {
            if (head == null || head.next == null) return head;

            Node Next = head.next;
            head.next = null;
            Node rest = Reverse_Recursively(Next);
            Next.next = head;
            return rest;
        }
        ////////////////////////////Q273/////////////////////////////////////////////////////////////
        private static string[] ones = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" }; //0-9
        private static string[] tens = { "", "", "twenty", "thirty", "fourty", "fifty", "sixty", "seventy", "eighty", "niny" }; //2x-9x
        private static string[] teens = { "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" }; //10-19
        public static string NumberToWords(int num)
        {
            string result = "";
            if (num == 0) return "Zero";
            if (num < 0) return "minus " + NumberToWords(Math.Abs(-num));
            int mod = 0;
            //for billions
            mod = num / 1000000000;
            if (mod > 0)
                result += ones[mod] + " billion ";
            num = num - (num / 1000000000) * 1000000000;
            //for millions
            mod = num / 1000000;
            if (mod > 0)
                result += NumberToWords_per_thousand(num / 1000000) + " million ";
            num = num - (num / 1000000) * 1000000;
            //for thousands
            mod = num / 1000;
            if (mod > 0)
                result += NumberToWords_per_thousand(num / 1000) + " thousand ";
            num = num - (num / 1000) * 1000;
            //for last three digits if they exist
            if (num > 0)
                result += NumberToWords_per_thousand(num);
            return result;

        }
        private static string NumberToWords_per_thousand(int num)
        {
            int mod = 0;
            string result = "";
            //for most significant digit i.e X in: X _ _
            mod = num / 100;
            if (mod != 0)
            {
                result += ones[mod] + " hundred";
            }
            num = num - (num / 100) * 100;
            //for middle digit i.e X in: _ X _
            mod = num / 10;
            if (mod == 1)
            {
                num = num % 10;
                result += " " + teens[num];
                return result;
            }
            else if (mod > 1)
            {
                result += " " + tens[mod];
            }
            //for least significant digit i.e X in: _ _ X
            num = num - (num / 10) * 10;
            if (num > 0)
            {
                result += " " + ones[num];
            }
            return result;
        }
        ////////////////////////////Q455/////////////////////////////////////////////////////////////
        public static Node AddTwoNumbers(Node l1, Node l2) //Add Two Numbers II
        {
            Node revl1 = Reverse_Iterative(l1);
            Node revl2 = Reverse_Iterative(l2);
            int carry = 0;
            int sum = revl1.data + revl2.data;
            if (sum > 9)
            {
                carry = 1;
                sum %= 10;
            }
            Node result = new Node(sum);
            while (revl1.next != null || revl2.next != null)
            {
                sum = 0;
                if (revl1.next != null)
                {
                    sum += revl1.next.data;
                    revl1 = revl1.next;
                }
                if (revl2.next != null)
                {
                    sum += revl2.next.data;
                    revl2 = revl2.next;
                }
                sum += carry;
                carry = 0;
                if (sum > 9)
                {
                    carry = 1;
                    sum %= 10;
                }
                result.AddToEnd(sum);
            }
            if (carry == 1)
                result.AddToEnd(1);

            return Reverse_Iterative(result);
        }
        ////////////////////////////Q200/////////////////////////////////////////////////////////////        
        public static int NumIslands(char[,] grid) //solved using graphs DFS traversal
        {
            int count = 0;
            int m = grid.GetLength(0);
            int n = grid.GetLength(1);
            bool[,] visited = new bool[m, n];
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (visited[i, j] == false && grid[i, j] == '1')
                    {
                        count++;
                        CheckAdjecent(i, j, grid, visited);
                    }
                    visited[i, j] = true;
                }

            }
            return count;
        }
        private static void CheckAdjecent(int i, int j, char[,] grid, bool[,] visited)
        {
            int m = visited.GetLength(0);
            int n = visited.GetLength(1);
            Point[] adj = { new Point(0, 1), new Point(1, 0), new Point(-1, 0), new Point(0, -1) };
            for (int k = 0; k <= 3; k++)
            {
                int new_i = i + adj[k].X;
                int new_j = j + adj[k].Y;
                if (new_i >= 0 && new_i < m && new_j >= 0 && new_j < n && visited[new_i, new_j] == false && grid[new_i, new_j] == '1')
                {
                    visited[new_i, new_j] = true;
                    CheckAdjecent(new_i, new_j, grid, visited);
                }

            }
        }
        ////////////////////////////Q186/////////////////////////////////////////////////////////////
        public static void reverseWords(char[] str)
        {
            reverse(str, 0, str.Length - 1);  // reverse the whole string first
            int r = 0;
            while (r < str.Length)
            {
                int l = r;
                while (r < str.Length && str[r] != ' ')
                    r++;
                reverse(str, l, r - 1);  // reverse each word
                r++;
            }
        }
        private static void reverse(char[] s, int l, int r)
        {
            while (l < r)
            {
                char tmp = s[l];
                s[l++] = s[r];
                s[r--] = tmp;
            }
        }
        ////////////////////////////Q218/////////////////////////////////////////////////////////////
        public static IList<int[]> GetSkyline(int[,] buildings)//The Skyline Problem (Perfect job by me)
        {
            //buildings[,] is list of buildings, each represented by 3 numbers. So
            //it will be actually buildings=new int [n,3] where n is number of buildings
            int width = MostRightBuilding(buildings) + 2; //MostRightBuilding gives out where the last building finishes
            int[] silhouette = new int[width];  //built the array with 1 element longer than we need to
                                                //check for the last building's right point, if it's the last element and 
                                                //make sure we check if it's getting to zero for the last element
            FillSilhouette(buildings, silhouette);
            int flow = 0;
            IList<int[]> result = new List<int[]>();
            for (int i = 0; i < silhouette.Length; i++)
            {
                if (silhouette[i] > flow)
                {
                    result.Add(new int[] { i, silhouette[i] });
                    flow = silhouette[i];
                }
                if (silhouette[i] < flow)
                {
                    result.Add(new int[] { i - 1, silhouette[i] });
                    flow = silhouette[i];
                }
            }
            return result;


        }
        private static int MostRightBuilding(int[,] buildings) //finding where the last building finishes
        {
            int max = 0;
            int n = buildings.GetLength(0); //number of buildings
            for (int i = 0; i < n; i++)
            {
                int right = buildings[i, 1]; //the middle element of each building which is its right point
                if (right > max) max = right;
            }
            return max;
        }
        private static void FillSilhouette(int[,] buildings, int[] silhouette)
        {
            int n = buildings.GetLength(0);
            for (int i = 0; i < n; i++)
            {
                for (int j = buildings[i, 0]; j <= buildings[i, 1]; j++)
                {
                    if (silhouette[j] < buildings[i, 2])
                        silhouette[j] = buildings[i, 2];
                }
            }
        }
        ////////////////////////////Q53/////////////////////////////////////////////////////////////        
        public static int MaxSubArray(int[] nums)
        {
            int sum = 0; int min_sum = 0; int max_sum = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (sum < min_sum)
                {
                    min_sum = sum;
                }
                if (sum - min_sum > max_sum)
                {
                    max_sum = sum - min_sum;
                }
            }
            return max_sum;
        }
        public static int MaxSubArrayII(int[] nums) //thought2
        {
            int current = nums[0];
            int max = nums[0];

            for (int i = 1; i < nums.Length; i++)
            {
                if (current > 0)
                    current = nums[i] + current;
                else current = nums[i];

                if (current > max)
                    max = current;
            }
            return max;
        }

        ////////////////////////////Q103/////////////////////////////////////////////////////////////
        public static List<Node> ZigzaglevelOrder(Treenode root)
        {
            List<Node> result = new List<Node>();
            ZigzaglevelOrder(root, 0, result);
            return result;
        }
        static void ZigzaglevelOrder(Treenode root, int level, List<Node> result)
        {
            if (root == null) return;

            Node current;

            if (level == result.Count)
            {
                current = new Node(root.data);
                result.Add(current);
            }
            else
            {
                current = result[level];
                if (level % 2 == 0)
                {
                    current.AddToEnd(root.data);
                }
                else
                {
                    Node temp = new Node(root.data);
                    temp.next = current;
                    result[level] = temp;
                }
            }

            ZigzaglevelOrder(root.left, level + 1, result);
            ZigzaglevelOrder(root.right, level + 1, result);

        }
        ////////////////////////////Q20/////////////////////////////////////////////////////////////
        public static bool IsValid(string s)
        {
            Stack<char> stack = new Stack<char>();

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '(' || s[i] == '{' || s[i] == '[')
                {
                    if (i == s.Length - 1) return false;
                    stack.Push(s[i]);
                }

                else if (!stack.Any()) return false;

                else if (s[i] == ')' && stack.Pop() != '(') return false;
                else if (s[i] == '}' && stack.Pop() != '{') return false;
                else if (s[i] == ']' && stack.Pop() != '[') return false;


            }
            if (stack.Any()) return false;
            return true;
        }
        ////////////////////////////Q153/////////////////////////////////////////////////////////////
        public static int FindMin(int[] nums) //Find minimum from a sorted rotated array without duplicates
        {
            return FindMin(nums, 0, nums.Length - 1);
        }
        static int FindMin(int[] nums, int left, int right)
        {
            if (left == right)
                return nums[left];

            int mid = left + (right - left) / 2;

            if (mid > 0 && nums[mid - 1] > nums[mid])
                return nums[mid];

            else if (nums[left] < nums[mid] && nums[right] < nums[mid])
                return FindMin(nums, mid + 1, right);

            else
                return FindMin(nums, left, mid);
        }
        ////////////////////////////Q174/////////////////////////////////////////////////////////////
        public static int CalculateMinimumHP(int[,] dungeon) //brute force
        {
            if (dungeon.GetLength(0) == 0 && dungeon.GetLength(1) == 0)
                throw new NullReferenceException();
            return CalculateMinimumHP(dungeon, 0, 0, dungeon[0, 0], int.MaxValue);
        }
        static int CalculateMinimumHP(int[,] dungeon, int i, int j, int cur_value, int min)
        {
            int[,] dir = { { 0, 1 }, { 1, 0 } };
            int m = dungeon.GetLength(0);
            int n = dungeon.GetLength(1);

            //if (i==m-1 && j==n-1)
            //{
            //    Console.WriteLine("i'm in\t"+cur_value+"\t");
            //    if (cur_value < min)
            //        min = cur_value;
            //}

            for (int k = 0; k < 2; k++)
            {
                int next_i = i + dir[k, 0];
                int next_j = j + dir[k, 1];

                if (next_i < m && next_j < n)
                {
                    int new_cur_value = cur_value + dungeon[next_i, next_j];
                    if (next_i == m - 1 && next_j == n - 1 && new_cur_value < min)
                        min = new_cur_value;
                    CalculateMinimumHP(dungeon, next_i, next_j, new_cur_value, min);
                }
            }
            return min;
        }
        ////////////////////////////Q32/////////////////////////////////////////////////////////////
        public static int LongestValidParentheses(string s)
        {
            Stack<int> stack = new Stack<int>();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ')')
                {
                    if (!stack.Any() || s[stack.Peek()] != '(')
                    {
                        stack.Push(i);
                    }
                    if (s[stack.Peek()] == '(')
                    {
                        stack.Pop();
                    }
                }
                else //if(s[i]=='(')
                {
                    stack.Push(i);
                }
            }
            int max = 0;
            int current = 0;
            int prev_index = s.Length;
            int curr_index = 0;

            if (!stack.Any())
                max = s.Length;
            else
            {
                while (stack.Any())
                {
                    curr_index = stack.Pop();
                    current = prev_index - curr_index - 1;
                    if (current > max)
                        max = current;
                    prev_index = curr_index;
                }
                current = prev_index;
                if (current > max)
                    max = current;
            }
            return max;
        }
        ////////////////////////////Q24/////////////////////////////////////////////////////////////
        public static Node SwapPairs(Node head)
        {
            if (head == null || head.next == null)
                return head;
            Node Next = head.next;
            Node NextNext = head.next.next;
            Next.next = head;
            head.next = SwapPairs(NextNext);
            return Next;
        }
        ////////////////////////////Q5/////////////////////////////////////////////////////////////
        public static String longestPalindrome(String s)
        {
            string longest = "";
            int left = 0;
            int right = s.Length - 1;
            for (int i = 0; i < s.Length; i++)
            {
                left = i - 1;
                right = i + 1;
                string current = s[i].ToString();
                while (right < s.Length && s[i] == s[right])
                {
                    current += s[right];
                    right++;
                }

                while (left >= 0 && right <= s.Length - 1 && s[left] == s[right])
                {
                    current = s[left] + current + s[right];
                    left--;
                    right++;
                }
                if (current.Length >= longest.Length)
                    longest = current;
            }
            return longest;
        }
        ////////////////////////////Q46/////////////////////////////////////////////////////////////
        public static List<List<int>> Permute(int[] nums)
        {
            List<int> input = nums.ToList<int>();
            List<List<int>> result = new List<List<int>>();
            return Permute(nums, new List<int>(), result);
        }
        static List<List<int>> Permute(int[] nums, List<int> output, List<List<int>> result)
        {
            if (output.Count == nums.Length)
            {
                result.Add(new List<int>(output));
            }
            else
            {
                IList<IList<int>> test = new List<IList<int>>();
                for (int i = 0; i < nums.Length; i++)
                {
                    if (output.Contains(nums[i])) continue;
                    output.Add(nums[i]);
                    Permute(nums, output, result);
                    output.RemoveAt(output.Count - 1);
                }
            }
            return result;

        }
        ////////////////////////////Q94/////////////////////////////////////////////////////////////
        public static List<int> InorderTraversal(Treenode root)
        {
            List<int> result = new List<int>();
            if (root == null)
                return result;

            Stack<Treenode> stack = new Stack<Treenode>();
            stack.Push(root);

            while (stack.Any())
            {
                Treenode curr = stack.Pop();
                if (curr.left != null)
                {
                    stack.Push(curr);
                    stack.Push(curr.left);
                    curr.left = null;
                }
                else
                {
                    result.Add(curr.data);
                    if (curr.right != null)
                        stack.Push(curr.right);
                }
            }
            return result;
        }
        ////////////////////////////Q56/////////////////////////////////////////////////////////////
        public class Interval
        {
            public int start;
            public int end;
            public Interval() { start = 0; end = 0; }
            public Interval(int s, int e) { start = s; end = e; }
        }
        public static IList<Interval> Merge(IList<Interval> intervals)
        {
            if (intervals.Count < 2) return intervals;

            int index = 1;
            Interval prev = intervals.ElementAt(0);
            while (true)
            {
                Interval curr = intervals.ElementAt(index);

                if (prev.end >= curr.start)
                {
                    prev.end = curr.end;
                    intervals.RemoveAt(index);
                }
                else
                {
                    index++;
                    prev = curr;
                }
                if (index >= intervals.Count - 1)
                    break;
            }
            return intervals;
        }
        ////////////////////////////Q114/////////////////////////////////////////////////////////////
        public static void Flatten(Treenode root)
        {
            Flatten_helper(root);
        }
        public static Treenode Flatten_helper(Treenode root)
        {
            if (root == null || (root.left == null & root.right == null))
                return root;

            if (root.left == null)
                return Flatten_helper(root.right);

            if (root.right == null)
            {
                root.right = root.left;
                root.left = null;
                return Flatten_helper(root.right);
            }

            else
            {
                Treenode temp = root.right;
                root.right = root.left;
                root.left = null;
                temp = Flatten_helper(temp);
                Flatten_helper(root.right).right = temp;
            }
            while (root.right != null)
                root = root.right;
            return root;
        }
        ////////////////////////////Q55/////////////////////////////////////////////////////////////
        public static bool CanJump(int[] nums)
        {
            return CanJump(nums, 0);
        }
        public static bool CanJump(int[] nums, int index)
        {
            if (index == nums.Length - 1)
                return true;

            for (int i = 1; i <= nums[index]; i++)
            {
                if (index + i <= nums.Length - 1)
                    return CanJump(nums, index + i);
            }
            return false;
        }
        ////////////////////////////Q513/////////////////////////////////////////////////////////////
        class storage
        {
            public static int maxlevel = 0;
            public static int bottomleftvalue = 0;
        }
        public static int FindBottomLeftValue(Treenode root)
        {
            FindBottomLeftValue(root, 1);
            return storage.bottomleftvalue;
        }
        public static void FindBottomLeftValue(Treenode root, int currLevel)
        {

            if (currLevel > storage.maxlevel)
            {
                storage.bottomleftvalue = root.data;
                storage.maxlevel++;
                //Console.WriteLine("here"+root.data+"-"+maxLevel);
            }
            if (root.left != null)
                FindBottomLeftValue(root.left, currLevel + 1);

            if (root.right != null)
                FindBottomLeftValue(root.right, currLevel + 1);
        }
        ////////////////////////////Q91/////////////////////////////////////////////////////////////
        public static int numDecodings(String s)
        {
            if (s[0] == '0' || s.Length == 0) return 0;
            if (s.Length == 1) return 1;

            int[] DP = new int[s.Length];

            DP[0] = 1;

            for (int i = 1; i < s.Length; i++)
            {
                if (s[i] == '0')
                {
                    if (int.Parse(s.Substring(i - 1, 2)) <= 26)
                        DP[i] = DP[i - 1] - 1;
                    else
                        DP[i] = 0;
                }
                else
                {

                    if (int.Parse(s.Substring(i - 1, 2)) <= 26)
                        DP[i] = DP[i - 1] + 1;
                    else
                        DP[i] = DP[i - 1];
                }
            }
            return DP[DP.Length - 1];
        }
        ////////////////////////////Q494/////////////////////////////////////////////////////////////
        static int count = 0;
        public static int FindTargetSumWays(int[] nums, int S)
        {
            int[] signs = { -1, 1 };
            FindTargetSumWays(nums, 0, 0, S, signs);
            return count;
            
        }
        static int FindTargetSumWays(int[] nums, int index, int sum, int target, int[] signs)
        {
            if (index == nums.Length)
            {
                if (target == sum)
                    count++;
            }
            else
            {
                for (int i = 0; i < signs.Length; i++)
                {
                    sum += signs[i] * nums[index];
                    FindTargetSumWays(nums, index + 1, sum, target, signs);
                    sum -= signs[i] * nums[index];
                }
            }
            return count;
        }
    }
    ////////////////////////////Q384/////////////////////////////////////////////////////////////
    public class Shuffling
    {
        int[] input;
        public Shuffling(int[] nums)
        {
            input = nums;
        }

        /** Resets the array to its original configuration and return it. */
        public int[] Reset()
        {
            return input;
        }

        /** Returns a random shuffling of the array. */
        public int[] Shuffle()
        {
            List<int> inputlist = new List<int>(input);
            return shuffle_helper(inputlist, new int[input.Count()], 0);
        }
        int[] shuffle_helper(List<int> inputlist, int[] output, int index)
        {
            if (inputlist.Count == 0)
                return output;
            Random rnd = new Random();
            int num = rnd.Next(0, inputlist.Count - 1);

            output[index] = inputlist[num];
            inputlist.RemoveAt(num);
            shuffle_helper(inputlist, output, index + 1);

            return output;
        }
    }
    
}
////////////////////////////Q/////////////////////////////////////////////////////////////