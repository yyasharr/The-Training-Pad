using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace My_Training_Pad
{
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
        ////////////////////////////Q563/////////////////////////////////////////////////////////////
        public static int FindTilt(Treenode root)
        {
            int sum = 0;
            FindTilt(root, sum);
            return sum;
        }
        static int FindTilt(Treenode root, int sum)
        {
            if (root == null) return 0;

            int left = FindTilt(root.left, sum);
            int right = FindTilt(root.right, sum);

            sum += Math.Abs(left - right);
            return left + right + root.data;
        }
        ////////////////////////////Q557/////////////////////////////////////////////////////////////
        public static string ReverseWords(string s)
        {
            int i = 0; int j = 0;
            char[] chars = s.ToCharArray();
            while (j <= s.Length)
            {
                if (j == s.Length)
                {
                    Reverse_helper(chars, i, j - 1);
                    j++;
                }
                else if (s[j] == ' ')
                {
                    Reverse_helper(chars, i, j - 1);
                    j++;
                    i = j;
                }
                else
                {
                    j++;
                }
            }
            return new string(chars);
        }
        static void Reverse_helper(char[] chars, int i, int j)
        {
            while (i < j)
            {
                char temp = chars[i];
                chars[i] = chars[j];
                chars[j] = temp;
                i++; j--;
            }
        }
        ////////////////////////////Q567 - Brute Force/////////////////////////////////////////////////////////////
        public static bool CheckInclusion_brute(string s1, string s2)
        {
            if (s2.Length < s1.Length || s1.Length == 0 || s2.Length == 0) return false;
            int n = s1.Length;
            for (int i = 0; i <= s2.Length - n; i++)
            {
                if (CheckInclusion_brute_helper(s2.Substring(i, n), s1) == true)
                    return true;
            }
            return false;
        }
        private static bool CheckInclusion_brute_helper(string s1, string substr)
        {
            int[] count = new int[26];
            for (int i = 0; i < s1.Length; i++)
            {
                count[s1[i] - 'a']++;
            }
            for (int i = 0; i < substr.Length; i++)
            {
                count[substr[i] - 'a']--;
            }
            for (int i = 0; i < count.Length; i++)
            {
                if (count[i] != 0)
                    return false;
            }
            return true;
        }
        ////////////////////////////Q567 - Optimized/////////////////////////////////////////////////////////////
        public static bool CheckInclusion_optimize(string s1, string s2)
        {
            if (s1.Length > s2.Length || s1.Length == 0 || s1 == "") return false;
            int[] count = new int[26];
            for (int i = 0; i < s1.Length; i++)
            {
                count[s1[i] - 'a']++;
                count[s2[i] - 'a']--;
            }
            int j = 0;
            while (j <= s2.Length - s1.Length)
            {
                if (CheckInclusion_optimize_helper(count) == true) return true;
                if (j == s2.Length - s1.Length) break;
                count[s2[j] - 'a']++;
                count[s2[j + s1.Length] - 'a']--;
                j++;
            }
            return false;
        }
        private static bool CheckInclusion_optimize_helper(int[] count)
        {
            for (int i = 0; i < count.Length; i++)
                if (count[i] != 0)
                    return false;
            return true;
        }
        ////////////////////////////Q560/////////////////////////////////////////////////////////////
        public static int SubarraySum(int[] nums, int k)
        {
            if (nums == null) return 0;
            int sum = 0; int count = 0;
            Dictionary<int, int> map = new Dictionary<int, int>();
            map.Add(0, 1);
            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];
                if (map.ContainsKey(sum - k))
                    count += map[sum - k];
                AddorUpdateDict(map, sum);
            }
            return count;
        }
        static void AddorUpdateDict(Dictionary<int, int> map, int sum)
        {
            if (map.ContainsKey(sum))
                map[sum]++;
            else
                map.Add(sum, 1);
        }
        ////////////////////////////Q554/////////////////////////////////////////////////////////////
        public static int LeastBricks(List<List<int>> wall)
        {
            int stopAt = 0;
            int max = 0;
            //storing number of times we stop at each spot
            Dictionary<int, int> Count = new Dictionary<int, int>();
            for (int i = 0; i < wall.Count; i++)
            {
                List<int> row = wall[i];
                for (int j = 0; j < row.Count; j++)
                {
                    stopAt += row[j];
                    if (j == row.Count - 1)
                        stopAt = 0;
                    else
                    {
                        if (Count.ContainsKey(stopAt))
                        {
                            Count[stopAt]++;
                        }
                        else
                        {
                            Count.Add(stopAt, 1);
                        }
                        max = Math.Max(max, Count[stopAt]);
                    }
                }
            }
            return wall.Count - max;
        }
        ////////////////////////////Q547/////////////////////////////////////////////////////////////
        public static int FindCircleNum(int[,] M)
        {
            int n = M.GetLength(0);
            int[] visited = new int[n];
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                if (visited[i] == 0)
                {
                    visited[i] = 1;
                    count++;
                    DFS(M, i, visited);
                }
            }
            return count;
        }
        private static void DFS(int[,] M, int i, int[] visited)
        {
            for (int j = 0; j < visited.Length; j++)
            {
                if (visited[j] == 0 && M[i, j] == 1)
                {
                    visited[j] = 1;
                    DFS(M, j, visited);
                }
            }
        }
        ////////////////////////////Q545/////////////////////////////////////////////////////////////
        public static List<int> BoundaryOfBinaryTree1(Treenode root) //Has bug
        {
            if (root == null) return new List<int>();
            int level = 0;
            Queue<Treenode> queue = new Queue<Treenode>();
            Stack<int> rightSide = new Stack<int>();
            List<int> leaves = new List<int>();
            List<int> res = new List<int>();
            queue.Enqueue(root);
            while (queue.Any())
            {
                int level_size = queue.Count;
                for (int i = 0; i < level_size; i++)
                {
                    Treenode temp = queue.Dequeue();


                    if (temp.left != null)
                        queue.Enqueue(temp.left);
                    if (temp.right != null)
                        queue.Enqueue(temp.right);
                    if (temp.left == null && temp.right == null)
                        leaves.Add(temp.data);
                    else
                    {
                        if (i == 0)
                        {
                            res.Add(temp.data);
                        }
                        else if (i == level_size - 1)
                        {
                            rightSide.Push(temp.data);
                        }
                    }
                }


            }

            while (leaves.Any())
            {
                res.Add(leaves[0]);
                leaves.RemoveAt(0);
            }
            while (rightSide.Any())
            {
                res.Add(rightSide.Pop());
            }
            return res;
        } //This version has a bug
        public static List<int> BoundaryOfBinaryTree(Treenode root) //Also has bug
        {
            List<int> result = new List<int>();
            //step1: finding the left side view and directly add the values to the result!
            LeftSideView(root, result, 0, 0);
            //step2: finding the leaves and directly add them to the result!
            FindLeaves(root, result);
            //step3: finding the right side of the tree and store it in a stack.
            Stack<int> stack = new Stack<int>();
            ReversedRightSideView(root, stack, 0);
            //step4: adding right side view to the result and done!
            while (stack.Count > 1) //we let the last element remain in the stack, which is our root and already printed!
            {
                result.Add(stack.Pop());
            }
            return result;
        }
        private static void LeftSideView(Treenode root, List<int> result, int level, int count)
        {
            if (root == null)
                return;
            if (count == level)
            {
                if (root.left != null || root.right != null)
                    result.Add(root.data);
                count++;
            }
            LeftSideView(root.left, result, level + 1, count);
            LeftSideView(root.right, result, level + 1, count);
        }
        private static void FindLeaves(Treenode root, List<int> result)
        {
            if (root == null) return;
            if (root.left == null && root.right == null)
                result.Add(root.data);
            FindLeaves(root.left, result);
            FindLeaves(root.right, result);
        }
        private static void ReversedRightSideView(Treenode root, Stack<int> stack, int level)
        {
            if (root == null || (root.left == null && root.right == null)) return;
            if (stack.Count == level)
            {
                stack.Push(root.data);
            }
            ReversedRightSideView(root.right, stack, level + 1);
            ReversedRightSideView(root.left, stack, level + 1);
        }
        ////////////////////////////Q543/////////////////////////////////////////////////////////////
        static int maxDistance = 0;
        public static int DiameterOfBinaryTree(Treenode root)
        {
            if (root == null)
                return 0;
            int leftDepth = Treenode.Height(root.left) + 1;
            int rightDepth = Treenode.Height(root.right) + 1;

            maxDistance = Math.Max(maxDistance, leftDepth + rightDepth);
            DiameterOfBinaryTree(root.left);
            DiameterOfBinaryTree(root.right);

            return maxDistance;
        }
        ////////////////////////////Q538/////////////////////////////////////////////////////////////
        static int sum = 0;
        public static Treenode ConvertBST(Treenode root)
        {
            ConvertBST_helper(root);
            return root;
        }
        static void ConvertBST_helper(Treenode root)
        {
            if (root == null)
                return;

            ConvertBST_helper(root.right);
            sum += root.data;
            root.data = sum;
            ConvertBST(root.left);
        }
        ////////////////////////////Q536/////////////////////////////////////////////////////////////
        public static Treenode Str2tree(string s)
        {
            if (s == "") return null;
            Treenode root;
            int first_par = s.IndexOf('('); //finding the first parentheses.
            if (first_par == -1)
            {
                root = new Treenode(int.Parse(s));
                return root;
            }
            int value = int.Parse(s.Substring(0, first_par));
            root = new Treenode(value);
            string leftchild = "";
            string rightchild = "";
            //Finding string for left child
            int left_par = 1;
            int i = first_par + 1;
            while (i < s.Length)
            {
                if (s[i] == '(') left_par++;
                if (s[i] == ')')
                {
                    left_par--;
                    if (left_par == 0)
                    {
                        i++;
                        break;
                    }
                }
                leftchild += s[i];
                i++;
            }
            //Finding string for right child
            left_par = 1;
            i++;
            while (i < s.Length)
            {
                if (s[i] == '(') left_par++;
                if (s[i] == ')')
                {
                    left_par--;
                    if (left_par == 0)
                        break;
                }
                rightchild += s[i];
                i++;
            }
            root.left = Str2tree(leftchild);
            root.right = Str2tree(rightchild);

            return root;
        }
        ////////////////////////////Q531/////////////////////////////////////////////////////////////
        public static int FindLonelyPixel(char[,] picture)
        {

            if (picture == null || (picture.GetLength(0) == 0 && picture.GetLength(1) == 0)) return 0;
            int n = picture.GetLength(0);
            int m = picture.GetLength(1);

            int[] rows = new int[n];
            int[] cols = new int[m];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (picture[i, j] == 'B')
                    {
                        rows[i]++; cols[j]++;
                    }
                }
            }
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    if (picture[i, j] == 'B' && rows[i] == 1 && cols[j] == 1)
                        count++;
                }
            }
            return count;
        }
        ////////////////////////////Q530/////////////////////////////////////////////////////////////
        static int min = int.MaxValue;
        static bool SeenFirst = false;//need to fill prev first.
        static int prev = 0;
        public static int GetMinimumDifference(Treenode root)
        {
            if (root == null) return min;

            GetMinimumDifference(root.left);

            if (SeenFirst == true)
                min = Math.Min(min, root.data - prev);

            SeenFirst = true;
            prev = root.data;

            GetMinimumDifference(root.right);

            return min;
        }
        ////////////////////////////Q385/////////////////////////////////////////////////////////////        
        public NestedInteger Deserialize(string s) //I supposed there is no [] as an input as isn't there any [[]], [[[]]], etc.
        {
            if (s == "" || s == null)
                return null;
            int value;
            NestedInteger NI;
            if (s[0] == '[')
            {
                int i = 1;
                while (char.IsDigit(s[i]))
                {
                    i++;
                }
                value = int.Parse(s.Substring(1, i));
                s = s.Substring(i + 2, s.Length - i - 3);//removing the number, the comma after the number and open and closing brackets ([]).
                NI = new NestedInteger(value);
                NI.Add(Deserialize(s));
            }
            else
            {
                NI = new NestedInteger(int.Parse(s));
            }
            return NI;
        }
        ////////////////////////////Q588/////////////////////////////////////////////////////////////
        public static int FindUnsortedSubarray(int[] nums)
        {
            if (nums.Length == 0 || nums == null) return 0;

            int[] SortedNums = new int[nums.Length];
            Array.Copy(nums, SortedNums, nums.Length);
            Array.Sort(SortedNums);

            int left = 0; int right = nums.Length - 1;

            while (left <= nums.Length - 1)
            {
                if (nums[left] == SortedNums[left])
                    left++;
                else break;
            }
            while (right >= 0)
            {
                if (nums[right] == SortedNums[right])
                    right--;
                else break;
            }

            return (right < left) ? 0 : right - left + 1;
        }
        ////////////////////////////Q582/////////////////////////////////////////////////////////////

        public static IList<int> KillProcess(IList<int> pid, IList<int> ppid, int kill)
        {
            if (pid.Count == 0 || pid == null) return null;
            IList<int> res = new List<int>();
            DFS582(pid, ppid, kill, res);
            return res;
        }
        static void DFS582(IList<int> pid, IList<int> ppid, int current, IList<int> res)
        {
            res.Add(current);
            for (int i = 0; i < ppid.Count; i++)
            {
                if (ppid[i] == current)
                    DFS582(pid, ppid, pid[i], res);
            }
        }
        ////////////////////////////Q64/////////////////////////////////////////////////////////////
        public static int MinPathSum(int[,] grid) //Dynamic Programming
        {
            int row = grid.GetLength(0);
            int col = grid.GetLength(1);

            int[,] DP = new int[row, col];
            DP[row - 1, col - 1] = grid[row - 1, col - 1];//filling the lower right most cell.

            for (int i = col - 2; i >= 0; i--) //filling last row
            {
                DP[row - 1, i] = DP[row - 1, i + 1] + grid[row - 1, i];
            }
            for (int i = row - 2; i >= 0; i--) //filling last column
            {
                DP[i, col - 1] = DP[i + 1, col - 1] + grid[i, col - 1];
            }

            for (int i = row - 2; i >= 0; i--)
            {
                for (int j = col - 2; j >= 0; j--)
                {
                    DP[i, j] = grid[i, j] + Math.Min(DP[i, j + 1], DP[i + 1, j]);
                }
            }
            return DP[0, 0];
        }
        ////////////////////////////280/////////////////////////////////////////////////////////////
        public void WiggleSort(int[] nums)
        {
            Array.Sort(nums);
            if (nums.Length <= 2) return;

            int i = 1;
            while (i <= nums.Length - 2)
            {
                int temp = nums[i];
                nums[i] = nums[i + 1];
                nums[i + 1] = temp;
                i += 2;
            }
        }
        ////////////////////////////Q593/////////////////////////////////////////////////////////////
        public bool ValidSquare(int[] p1, int[] p2, int[] p3, int[] p4)
        {
            int[,] points = { };
            points[0, 0] = p1[0];
            points[0, 1] = p1[1];
            points[1, 0] = p2[0];
            points[1, 1] = p2[1];
            points[2, 0] = p3[0];
            points[2, 1] = p3[1];
            points[3, 0] = p4[0];
            points[3, 1] = p4[1];

            bool[,] visited = new bool[4, 2];

            int row = 0;
            int col = 0;
            int count = 0;

            while (count < 8)
            {
                if (visited[row, col] == true) return false;
                else visited[row, col] = true;

                count++;
                if (count == 8) return true;

                col = (col == 1) ? 0 : 1;

                int lookingfor = points[row, col];
                visited[row, col] = true;

                count++;
                if (count == 8) return true;

                for (int j = 0; j < 4; j++)
                {
                    if (visited[j, col] == false && points[j, col] == lookingfor)
                    {
                        row = j;
                        break;
                    }
                    if (j == 3) return false;
                }
            }
            return true;
        }
        ////////////////////////////Q598/////////////////////////////////////////////////////////////
        public static int MaxCount(int m, int n, int[,] ops)
        {
            int minrow = m;
            int mincol = n;

            for (int i = 0; i < ops.GetLength(0); i++)
            {
                minrow = Math.Min(minrow, ops[i, 0]);
                mincol = Math.Min(mincol, ops[i, 1]);
            }
            return minrow * mincol;
        }

    }
}
////////////////////////////Q/////////////////////////////////////////////////////////////