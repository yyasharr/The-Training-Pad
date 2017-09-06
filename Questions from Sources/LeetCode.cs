using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Data.Linq;
using System.Collections;

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
        ////////////////////////////Q148/////////////////////////////////////////////////////////////
        public static Node SortList(Node head)
        {
            if (head == null || head.next == null) return head;

            //cut the linkedlist in half.
            //one half= head, other half= slow
            Node slow = head; Node fast = head; Node prev = null;
            while (fast.next != null && fast != null)
            {
                prev = slow;
                slow = slow.next;
                fast = fast.next;
            }
            prev.next = null;

            Node N1 = SortList(head);
            Node N2 = SortList(slow);

            return MergeList(N1, N2);
        }
        private static Node MergeList(Node h1, Node h2)
        {
            Node res = new Node(0);
            Node temp = res;

            while (h1 != null && h2 != null)
            {
                if (h1.data < h2.data)
                {
                    res.next = h1;
                    res = res.next;
                    h1 = h1.next;
                }
                else
                {
                    res.next = h2;
                    res = res.next;
                    h2 = h2.next;
                }
            }
            if (h1 != null)
            {
                res.next = h1;
            }
            if (h2 != null)
            {
                res.next = h2;
            }

            return temp.next;
        }
        ////////////////////////////Q48/////////////////////////////////////////////////////////////
        public static void Rotate(int[,] matrix)
        {
            if (matrix == null) return;

            int low = 0;
            int high = matrix.GetLength(0) - 1;

            while (high > low)
            {
                for (int i = 0; i <= high - low - 1; i++)
                {
                    int temp1 = matrix[low + i, high];
                    matrix[low + i, high] = matrix[low, low + i];

                    int temp2 = matrix[high, high - i];
                    matrix[high, high - i] = temp1;

                    temp1 = matrix[high - i, low];
                    matrix[high - i, low] = temp2;

                    matrix[low, low + i] = temp1;
                }

                low++;
                high--;
            }
        }
        ////////////////////////////Q221/////////////////////////////////////////////////////////////
        public static int MaximalSquare(char[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);

            int[,] DP = new int[n, m];
            int max = 0;
            for (int i = 0; i < m; i++)
            {
                DP[0, i] = int.Parse(matrix[0, i].ToString());
                if (matrix[0, i] == '1') max = 1;
            }
            for (int i = 0; i < n; i++)
            {
                DP[i, 0] = int.Parse(matrix[i, 0].ToString());
                if (matrix[i, 0] == '1') max = 1;
            }


            for (int i = 1; i < n; i++)
            {
                for (int j = 1; j < m; j++)
                {
                    if (matrix[i, j] == '1')
                    {
                        DP[i, j] = Math.Min(DP[i - 1, j - 1], Math.Min(DP[i - 1, j], DP[i, j - 1])) + 1;
                        max = Math.Max(max, DP[i, j]);
                    }
                }
            }
            return max * max;

        }
        ////////////////////////////Q82/////////////////////////////////////////////////////////////
        public static Node DeleteDuplicates(Node head)
        {
            HashSet<int> visited = new HashSet<int>();

            Node prev = null;
            Node curr = head;

            while (curr != null)
            {
                if (visited.Contains(curr.data))
                {
                    prev.next = curr.next;
                }
                else
                {
                    visited.Add(curr.data);
                }
                prev = curr;
                curr = curr.next;
            }
            return head;
        } //O(n) space
        public static Node DeleteDuplicates1(Node head) //O(1) space -- Nice idea by me!
        {
            if (head == null) return head;
            Node prev = head;
            Node curr = head.next;

            while (curr != null)
            {
                if (prev.data != curr.data)
                    prev = curr;
                curr = curr.next;
                prev.next = curr;
            }

            return head;
        }
        ////////////////////////////Q110/////////////////////////////////////////////////////////////
        public bool IsBalanced(Treenode root)
        {
            if (root == null) return true;

            int left_height = Height(root.left);
            int right_height = Height(root.right);

            if (Math.Abs(left_height - right_height) > 1) return false;

            return IsBalanced(root.left) & IsBalanced(root.right);
        }
        private int Height(Treenode root)
        {
            if (root == null) return 0;
            return 1 + Math.Max(Height(root.left), Height(root.right));
        }
        ////////////////////////////Q241/////////////////////////////////////////////////////////////
        public static IList<int> DiffWaysToCompute(string input)
        {
            IList<int> res = new List<int>();
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '-' || input[i] == '+' || input[i] == '*')
                {
                    string left = input.Substring(0, i);
                    string right = input.Substring(i + 1);

                    IList<int> list1 = DiffWaysToCompute(left);
                    IList<int> list2 = DiffWaysToCompute(right);

                    for (int j = 0; j < list1.Count; j++)
                    {
                        for (int k = 0; k < list2.Count; k++)
                        {
                            int result = 0;
                            switch (input[i])
                            {
                                case '+':
                                    result = list1[j] + list2[k];
                                    break;
                                case '-':
                                    result = list1[j] - list2[k];
                                    break;
                                case '*':
                                    result = list1[j] * list2[k];
                                    break;
                            }
                            res.Add(result);
                        }
                    }
                }

            }
            if (res.Count == 0)
                res.Add(int.Parse(input));
            return res;
        }
        ////////////////////////////Q565/////////////////////////////////////////////////////////////
        static int max_count = 0;
        public static int ArrayNesting(int[] nums)
        {
            int[] General_visited = new int[nums.Length];

            for (int i = 0; i < nums.Length; i++)
            {
                if (General_visited[i] == 0)
                {
                    int[] Current_visited = new int[nums.Length];
                    ArrayNesting_helper(nums, General_visited, Current_visited, 0, i);
                }
            }
            return max_count;
        }
        private static void ArrayNesting_helper(int[] nums, int[] general_visited, int[] current_visited, int current_count, int current_index)
        {
            while (current_visited[current_index] == 0)
            {
                current_count++;
                max_count = Math.Max(max_count, current_count);
                current_visited[current_index] = 1;
                general_visited[current_index] = 1;
                current_index = nums[current_index];
            }
        }
        ////////////////////////////Q112/////////////////////////////////////////////////////////////
        public bool HasPathSum(Treenode root, int sum)
        {
            if (root == null)
                return false;

            if (root.left == null && root.right == null) //if leaf
            {
                if (root.data == sum) return true;
            }

            return (HasPathSum(root.left, sum - root.data) || HasPathSum(root.right, sum - root.data));
        }
        ////////////////////////////Q113/////////////////////////////////////////////////////////////
        public static IList<IList<int>> PathSum(Treenode root, int sum)
        {
            IList<IList<int>> res = new List<IList<int>>();
            List<int> curr = new List<int>();

            PathSum(root, sum, res, curr);

            return res;

        }

        private static void PathSum(Treenode root, int sum, IList<IList<int>> res, List<int> curr)
        {
            if (root == null) return;

            curr.Add(root.data);

            PathSum(root.left, sum - root.data, res, curr);

            if (root.left == null && root.right == null) //if root is leaf
            {
                if (sum == root.data)
                    res.Add(new List<int>(curr));
            }

            PathSum(root.right, sum - root.data, res, curr);

            curr.RemoveAt(curr.Count - 1);
        }
        ////////////////////////////Q107/////////////////////////////////////////////////////////////
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
        ////////////////////////////Q72/////////////////////////////////////////////////////////////
        static int MinDistanceBF(string word1, string word2)//Brute Force, DP solution down
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
        ////////////////////////////Q282/////////////////////////////////////////////////////////////        
        public IList<string> AddOperators(string num, int Target)
        {
            return AddOperators(num, Target, new List<string>());
        }
        List<string> AddOperators(string num, int target, List<string> total_strings)
        {
            int n = num.Length;
            for (int i = 1; i < Math.Pow(4, n - 1); i++)
            {
                string operators = Convert.ToString(i, 2).PadLeft((n - 1) * 2, '0'); //SO IMPORTANT AND F*#&ing COOL: Generating two digit codes for each operator and n-1 spots to put them between. 00: nothing; 01: '+'; 10: '*'; 11: '-'
                StringBuilder exp = new StringBuilder();
                exp.Append(num[0]);
                for (int j = 1; j <= n - 1; j++)
                {
                    string Operator = operators.Substring((j - 1) * 2, 2);
                    //if (Operator == "00") SKIP; //not necessary
                    if (Operator == "01") exp.Append('+');
                    if (Operator == "10") exp.Append('*');
                    if (Operator == "11") exp.Append('-');

                    if (exp[exp.Length - 1] == '0')
                    {
                        exp.Remove(exp.Length - 1, 1);
                    }
                    else
                        exp.Append(num[j]);
                }

                if (Calculator(exp.ToString()) == target)
                    total_strings.Add(exp.ToString());

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
        ////////////////////////////Q437/////////////////////////////////////////////////////////////
        static int result;
        public static int PathSumIII(Treenode root, int sum)
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            dict.Add(0, 1);

            PathSumIII_helper(root, 0, sum, dict);

            return result;
        }
        private static void PathSumIII_helper(Treenode root, int curr_sum, int target_sum, Dictionary<int, int> dict)
        {
            if (root == null) return;

            curr_sum += root.data;
            int find = curr_sum - target_sum;
            if (dict.ContainsKey(find)) result += dict[find];

            AddOrIncrement(dict, curr_sum);

            PathSumIII_helper(root.left, curr_sum, target_sum, dict);
            PathSumIII_helper(root.right, curr_sum, target_sum, dict);

            RemoveOrDecrement(dict, curr_sum);
            curr_sum -= root.data;
        }
        private static void AddOrIncrement(Dictionary<int, int> dict, int sum)
        {
            if (!dict.ContainsKey(sum))
                dict.Add(sum, 0);
            dict[sum]++;
        }
        private static void RemoveOrDecrement(Dictionary<int, int> dict, int curr_sum)
        {
            if (dict[curr_sum] == 1) dict.Remove(curr_sum);
            else dict[curr_sum]--;
        }
        ////////////////////////////Q560/////////////////////////////////////////////////////////////
        static int maxSubArraySum(int[] a)
        {
            if (a.Length == 0 || a == null) return int.MinValue;

            int max_sum = int.MinValue;
            int curr_sum = a[0];
            for (int i = 1; i < a.Length; i++)
            {
                if (curr_sum + a[i] > 0)
                {
                    curr_sum += a[i];
                    max_sum = Math.Max(max_sum, curr_sum);
                }
                else
                {
                    curr_sum = 0;
                }
            }

            return max_sum;
        }
        static int countSubarraySum(int[] a, int target)//find number of subarrays that sum up to int target
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();
            //dict.Add(0, 1);
            int count = 0;
            int curr_sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                curr_sum += a[i];
                int find = curr_sum - target;

                if (dict.ContainsKey(find))
                    count += dict[find];

                if (dict.ContainsKey(curr_sum))
                    dict[curr_sum]++;
                else
                    dict.Add(curr_sum, 1);
            }
            return count;
        }
        ////////////////////////////Q515/////////////////////////////////////////////////////////////        
        public IList<int> LargestValues(Treenode root)
        {
            IList<int> Max = new List<int>();
            LargestValues(root, 0, Max);
            return Max;
        }
        private void LargestValues(Treenode root, int level, IList<int> max)
        {
            if (root == null) return;
            if (max.Count == level)
                max.Add(root.data);
            else
                max[level] = Math.Max(max[level], root.data);

            LargestValues(root.left, level + 1, max);
            LargestValues(root.right, level + 1, max);

        }
        ////////////////////////////Q624/////////////////////////////////////////////////////////////
        public int MaxDistance(IList<IList<int>> arrays)
        {
            int max = int.MinValue;
            int min = int.MaxValue;

            foreach (List<int> list in arrays)
            {
                int prev_max =
                max = Math.Max(max, list[list.Count - 1]);
                min = Math.Min(min, list[0]);
            }

            return Math.Abs(max - min);
        }
        ////////////////////////////Q623/////////////////////////////////////////////////////////////
        public Treenode AddOneRow(Treenode root, int v, int d)
        {
            if (d == 1)
            {
                Treenode res = new Treenode(v);
                res.left = root;
                return res;
            }
            Queue<Treenode> q = new Queue<Treenode>();
            q.Enqueue(root);
            int level = 1;
            while (q.Any() && level < d) //while queue is not empty
            {
                int count = q.Count;
                while (count != 0) //in the same level
                {
                    Treenode curr = q.Dequeue();
                    if (level == d - 1)
                    {
                        Treenode templeft = curr.left;
                        Treenode tempright = curr.right;
                        Treenode newleft = new Treenode(v);
                        Treenode newright = new Treenode(v);
                        newleft.left = templeft;
                        newright.right = tempright;
                        curr.left = newleft;
                        curr.right = newright;
                    }
                    if (curr.left != null) q.Enqueue(curr.left);
                    if (curr.right != null) q.Enqueue(curr.right);
                    count--;
                }
                level++;
            }
            return root;
        }
        ////////////////////////////Q78/////////////////////////////////////////////////////////////
        public static IList<IList<int>> Subsets(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            Subsets(0, new List<int>(), nums, res);
            return res;
        }
        private static void Subsets(int index, List<int> output, int[] nums, IList<IList<int>> res)
        {
            if (index == nums.Length)
            {
                res.Add(output);
            }
            else
            {
                List<int> new_output = new List<int>(output);
                new_output.Add(nums[index]);
                Subsets(index + 1, output, nums, res);
                Subsets(index + 1, new_output, nums, res);
            }
        }
        ////////////////////////////Q496/////////////////////////////////////////////////////////////           
        public int[] NextGreaterElement(int[] findNums, int[] nums)
        {
            int i1 = 0;
            int i2 = 0;
            Array.Sort(findNums);
            Array.Sort(nums);

            int[] res = new int[findNums.Length];
            while (i2 < nums.Length && nums[i2] <= findNums[i1]) i2++;

            while (i1 < findNums.Length)
            {
                if (i2 == nums.Length)
                {
                    res[i1] = -1;
                    i1++;
                }
                else
                {
                    if (nums[i2] > findNums[i1])
                    {
                        res[i1] = nums[i2];
                        i1++;
                    }
                    else
                    {
                        i2++;
                    }

                }
            }
            return res;
        }
        ////////////////////////////Q121/////////////////////////////////////////////////////////////        
        public static int maxProfit(int[] prices) //Best Time to Buy and Sell Stock
        {
            int[] res = new int[2]; //res[0]=Min Profit ; res[1]=Max Profit
            res[0] = int.MaxValue;

            for (int i = 0; i < prices.Length; i++)
            {
                if (prices[i] < res[0])
                    res[0] = prices[i];
                else if (prices[i] - res[0] > res[1])
                    res[1] = prices[i] - res[0];
            }
            return res[1];
        }
        ////////////////////////////Q122/////////////////////////////////////////////////////////////
        public static int MaxProfitII(int[] prices)
        {
            int total = 0;
            for (int i = 0; i < prices.Length - 1; i++)
            {
                if (prices[i + 1] > prices[i]) total += prices[i + 1] - prices[i];
            }

            return total;
        }
        ////////////////////////////Q375/////////////////////////////////////////////////////////////
        public int GetMoneyAmount(int n)
        {
            if (n == 1) return 0;
            return GetMoneyAmount(1, n);
        }
        private int GetMoneyAmount(int first, int last)
        {
            if (last - first == 1 || last == first) return last;

            int mid = (first + last) / 2;

            return mid + GetMoneyAmount(mid, last);
        }
        ////////////////////////////Q451/////////////////////////////////////////////////////////////
        public static string FrequencySort(string s)
        {
            Dictionary<char, int> dict = new Dictionary<char, int>();
            List<char>[] list = new List<char>[s.Length + 1];

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (dict.ContainsKey(c)) //if already exists, needs to be updated in dict, removed from current list, and added to the new list
                {
                    //remove from the previous list.
                    int n = dict[c];
                    RemoveFromList(list, n, c);
                    //add to new list
                    AddtoList(list, n + 1, c);
                    //update dict
                    dict[c]++;
                }
                else
                {
                    dict.Add(c, 1);
                    AddtoList(list, 1, c);
                }
            }
            string res = "";
            for (int i = list.Length - 1; i >= 1; i--)
            {
                if (list[i] != null)
                {
                    List<char> l = list[i];
                    foreach (char ch in l)
                    {
                        for (int j = 0; j < i; j++)
                        {
                            res += ch;
                        }
                    }
                }
            }

            return res;
        }
        private static void AddtoList(List<char>[] list, int n, char c)
        {
            if (list[n] == null)
                list[n] = new List<char>();
            list[n].Add(c);
        }
        private static void RemoveFromList(List<char>[] list, int n, char c)
        {
            if (list[n].Count == 1) list[n] = null;
            else
                list[n].Remove(c);
        }
        public static string FrequencySortII(string s) //Trying Lambda
        {
            int[] chars = new int[128];
            Dictionary<int, List<char>> dict = new Dictionary<int, List<char>>();

            foreach (char c in s)
            {
                int count = chars[c];
                //if this char has already been seen, needs to be removed from previous count
                if (count > 0)
                {
                    dict[count].Remove(c);
                }
                count++;
                chars[c] = count;
                //adding this char to it's new number in dict
                if (dict.ContainsKey(count))
                {
                    dict[count].Add(c);
                }
                else
                {
                    List<char> nl = new List<char>();
                    nl.Add(c);
                    dict.Add(count, nl);
                }
            }
            string res = "";
            Array.Sort(chars);
            int i = 127;
            while (chars[i] > 0)
            {
                int eachchar = chars[i];
                char curr = dict[eachchar][0];
                dict[eachchar].RemoveAt(0);
                for (int j = 0; j < eachchar; j++)
                {
                    res += curr;
                }
                i--;
            }
            return res;
        }
        ////////////////////////////Q/////////////////////////////////////////////////////////////
        public int MaximumProduct(int[] nums) //Attemp1: Accepted: 279 ms - beats 15%
        {
            Array.Sort(nums);
            return Math.Max(nums[0] * nums[1] * nums[nums.Length - 1], nums[nums.Length - 1] * nums[nums.Length - 2] * nums[nums.Length - 3]);
        }
        public static int MaximumProductII(int[] nums) //Attemp2: Accepted: ? ms - beats 80%
        {
            int[] MinMax = new int[5];
            MinMax[0] = int.MaxValue;
            MinMax[1] = int.MaxValue;
            MinMax[2] = int.MinValue;
            MinMax[3] = int.MinValue;
            MinMax[4] = int.MinValue;

            for (int i = 0; i < nums.Length; i++)
            {
                int n = nums[i];
                //comparing with big numbers
                if (n >= MinMax[4]) //greater than all
                {
                    MinMax[2] = MinMax[3];
                    MinMax[3] = MinMax[4];
                    MinMax[4] = n;
                }
                else if (n >= MinMax[3])
                {
                    MinMax[2] = MinMax[3];
                    MinMax[3] = n;
                }
                else if (n >= MinMax[2])
                {
                    MinMax[2] = n;
                }
                //comparing with small numbers
                if (n <= MinMax[0])
                {
                    MinMax[1] = MinMax[0];
                    MinMax[0] = n;
                }
                else if (n <= MinMax[1])
                {
                    MinMax[1] = n;
                }
            }
            return Math.Max(MinMax[0] * MinMax[1] * MinMax[4], MinMax[4] * MinMax[3] * MinMax[2]);
        }
        ////////////////////////////Q70/////////////////////////////////////////////////////////////
        public int ClimbStairs(int n)
        {
            int[] Memo = new int[n + 1];
            return ClimbStairs(0, n, Memo);
        }
        private int ClimbStairs(int curr, int n, int[] memo)
        {
            if (curr == n)
                return 1;
            if (curr > n)
                return 0;
            if (memo[curr] > 0)
                return memo[curr];
            memo[curr] = ClimbStairs(curr + 1, n, memo) + ClimbStairs(curr + 2, n, memo);
            return memo[curr];
        }
        ////////////////////////////Q101/////////////////////////////////////////////////////////////
        public bool IsSymmetric(Treenode root) //Recursive
        {
            if (root == null) return true;
            return IsSymmetric(root.left, root.right);
        }
        private bool IsSymmetric(Treenode left, Treenode right)
        {
            if (left == null && right == null) return true;
            if (left == null || right == null) return false;

            return left.data == right.data && IsSymmetric(left.left, right.right) && IsSymmetric(right.left, left.right);
        }
        public bool IsSymmetricII(Treenode root) //Iterative
        {
            if (root == null) return true;

            Queue<Treenode> q = new Queue<Treenode>();
            q.Enqueue(root);
            q.Enqueue(root);

            while (q.Any())
            {
                Treenode t1 = q.Dequeue();
                Treenode t2 = q.Dequeue();
                if (t1 == null && t2 == null) continue;
                if (t1 == null || t2 == null || t1.data != t2.data) return false;

                q.Enqueue(t1.left);
                q.Enqueue(t2.right);
                q.Enqueue(t1.right);
                q.Enqueue(t2.left);
            }
            return true;
        }
        ////////////////////////////Q236///////////////////////////////////////////////////////////// 
        public static Treenode LowestCommonAncestor(Treenode root, Treenode p, Treenode q)
        {
            if (!FindTreeNode(root, p) || !FindTreeNode(root, q)) return null;
            return LowestCommonAncestor_Helper(root, p, q);
        }
        private static Treenode LowestCommonAncestor_Helper(Treenode root, Treenode p, Treenode q)
        {
            if (root == null) return null;

            if (root.data == q.data)
                return q;
            if (root.data == p.data)
                return p;

            bool IsPLeft = FindTreeNode(root.left, p);
            bool IsQLeft = FindTreeNode(root.left, q);

            if (IsPLeft == true)
            {
                if (IsQLeft == true) //if both left
                    return LowestCommonAncestor_Helper(root.left, p, q);
                else return root;
            }
            else //if IsPLeft==false
            {
                if (IsQLeft == false) //if both right
                    return LowestCommonAncestor_Helper(root.right, p, q);
                else return root;
            }
        }
        private static bool FindTreeNode(Treenode root, Treenode value)
        {
            if (root == null) return false;
            if (root == value) return true;
            return FindTreeNode(root.left, value) || FindTreeNode(root.right, value);
        }
        ////////////////////////////Q138/////////////////////////////////////////////////////////////Mock interview
        public RandomListNode CopyRandomList(RandomListNode head)
        {
            if (head == null) return head;
            Dictionary<int, RandomListNode> dic = new Dictionary<int, RandomListNode>();
            RandomListNode Dummy = new RandomListNode(0);
            RandomListNode res = Dummy;
            while (head != null)
            {
                RandomListNode current;
                if (dic.ContainsKey(head.label))
                {
                    current = dic[head.label];
                }
                else
                {
                    current = new RandomListNode(head.label);
                    dic.Add(current.label, current);
                }
                RandomListNode currentrandom;
                if (head.random == null)
                {
                    currentrandom = null;
                }
                else if (dic.ContainsKey(head.random.label))
                {
                    currentrandom = dic[head.random.label];
                }
                else
                {
                    currentrandom = new RandomListNode(head.random.label);
                    dic.Add(currentrandom.label, currentrandom);
                }
                res.next = current;
                current.random = currentrandom;
                res = res.next;
                head = head.next;
            }
            return Dummy.next;
        }
        ////////////////////////////Q94///////////////////////////////////////////////////////////// 
        public IList<int> InorderTraversal_Iterative(Treenode root)
        {
            Queue<Treenode> q = new Queue<Treenode>();
            Stack<Treenode> stack = new Stack<Treenode>();
            List<int> order = new List<int>();

            if (root == null) return order;

            q.Enqueue(root);

            while (q.Any() || stack.Any())
            {

                if (q.Any())
                {
                    Treenode temp = q.Dequeue();
                    if (temp.left == null)
                    {
                        order.Add(temp.data);
                        if (temp.right != null)
                            q.Enqueue(temp.right);
                    }
                    else
                    {
                        stack.Push(temp);
                        q.Enqueue(temp.left);
                    }
                }
                else if (stack.Any())
                {
                    Treenode temp = stack.Pop();
                    order.Add(temp.data);
                    if (temp.right != null)
                        q.Enqueue(temp.right);
                }
            }
            return order;

        }
        ////////////////////////////Q88///////////////////////////////////////////////////////////// 
        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int i = m - 1;
            int j = n - 1;
            int k = m + n - 1;

            while (i >= 0 && j >= 0)
            {
                if (nums1[i] > nums2[j])
                {
                    nums1[k] = nums1[i];
                    i--;
                }
                else
                {
                    nums1[k] = nums2[j];
                    j--;
                }
                k--;
            }
            while (i >= 0)
            {
                nums1[k] = nums1[i];
                i--;
                k--;
            }
            while (j >= 0)
            {
                nums1[k] = nums2[j];
                j--;
                k--;
            }
        }
        ////////////////////////////Q75///////////////////////////////////////////////////////////// 
        public void SortColors(int[] nums)
        {
            //first pass: move zeros to front
            int start = 0; int end = nums.Length - 1;

            while (start < end)
            {
                while (start < end && nums[start] == 0) start++;
                while (end > start && nums[end] != 0) end--;
                if (nums[end] == 0 && nums[start] != 0)
                {
                    int temp = nums[start];
                    nums[start] = nums[end];
                    nums[end] = temp;
                    start++; end--;
                }
            }
            start = 0; end = nums.Length - 1;
            while (start < end)
            {
                while (end > start && nums[end] == 2) end--;
                while (start < end && nums[start] != 2) start++;
                if (nums[end] != 2 && nums[start] == 2)
                {
                    int temp = nums[start];
                    nums[start] = nums[end];
                    nums[end] = temp;
                    start++; end--;
                }
            }
        }
        ////////////////////////////Q105///////////////////////////////////////////////////////////// 
        public Treenode BuildTree1(int[] preorder, int[] inorder)
        {
            if (preorder.Length == 0 || preorder == null || inorder.Length == 0 || inorder == null) return null;

            Treenode root = new Treenode(preorder[0]);

            int LocationofRoot = RootLocation(root.data, inorder); //finding the location of root node in inorder traversal

            int[] leftinorder = BuildSubArray(0, LocationofRoot - 1, inorder);
            int[] rightinorder = BuildSubArray(LocationofRoot + 1, inorder.Length - 1, inorder);
            int[] leftpreorder = BuildSubArray(1, leftinorder.Length, preorder);
            int[] rightpreorder = BuildSubArray(leftinorder.Length + 1, preorder.Length - 1, preorder);

            root.left = BuildTree(leftpreorder, leftinorder);
            root.right = BuildTree(rightpreorder, rightinorder);

            return root;
        }
        private int[] BuildSubArray(int start, int end, int[] Array)
        {
            int[] res = new int[end - start + 1];
            for (int i = 0; i < res.Length; i++)
            {
                res[i] = Array[start + i];
            }
            return res;
        }
        private int RootLocation(int data, int[] list)
        {
            for (int i = 0; i < list.Length; i++)
                if (list[i] == data) return i;
            return -1;
        }
        ////////////////////////////Q106/////////////////////////////////////////////////////////////
        public static Treenode BuildTree(int[] inorder, int[] postorder)
        {
            Dictionary<int, int> inorderindices = new Dictionary<int, int>();
            for (int i = 0; i < inorder.Length; i++)
            {
                inorderindices.Add(inorder[i], i);
            }
            return BuildTree(inorder, 0, inorder.Length - 1, postorder, 0, postorder.Length - 1, inorderindices);
        }
        private static Treenode BuildTree(int[] inorder, int startinorder, int endinorder, int[] postorder, int startpostorder, int endpostorder, Dictionary<int, int> inorderindices)
        {
            if (startinorder > endinorder || startpostorder > endpostorder) return null;
            if (startinorder == endinorder) return new Treenode(inorder[startinorder]);

            Treenode root = new Treenode(postorder[endpostorder]);
            int rootlocation = inorderindices[root.data];

            root.left = BuildTree(inorder, startinorder, rootlocation - 1, postorder, startpostorder, rootlocation - 1, inorderindices);
            root.right = BuildTree(inorder, rootlocation + 1, endinorder, postorder, rootlocation, endpostorder - 1, inorderindices);

            return root;
        }
        ////////////////////////////Q637///////////////////////////////////////////////////////////// 
        public IList<double> AverageOfLevels(Treenode root)
        {
            List<double> res = new List<double>();
            if (root == null) return res;
            Queue<Treenode> q = new Queue<Treenode>();
            q.Enqueue(root);
            while (q.Any())//while q is not empty
            {
                int count = q.Count;
                double sum = 0;
                for (int i = 0; i < count; i++)
                {
                    Treenode curr = q.Dequeue();
                    sum += curr.data;
                    if (curr.left != null) q.Enqueue(curr.left);
                    if (curr.right != null) q.Enqueue(curr.right);
                }
                res.Add(sum / count);
            }
            return res;
        }
        ////////////////////////////Q643///////////////////////////////////////////////////////////// 
        public double FindMaxAverage(int[] nums, int k)
        {
            int currsum = 0;
            for (int i = 0; i < k; i++)
            {
                currsum += nums[i];
            }
            int maxsum = currsum;
            int start = 0;
            int end = k - 1;
            while (end < nums.Length - 1)
            {
                currsum -= nums[start];
                currsum += nums[end + 1];
                maxsum = Math.Max(maxsum, currsum);
                start++; end++;
            }
            return maxsum / (double)k;
        }
        ////////////////////////////Q62///////////////////////////////////////////////////////////// 
        public static int UniquePaths(int m, int n)
        {
            int[,] DP = new int[m, n];
            DP[m - 1, n - 1] = 1;

            for (int i = 0; i < m - 1; i++)
            {
                DP[i, n - 1] = 1;
            }
            for (int i = 0; i < n - 1; i++)
            {
                DP[m - 1, i] = 1;
            }

            for (int i = m - 2; i >= 0; i--)
            {
                for (int j = n - 2; j >= 0; j--)
                {
                    DP[i, j] = DP[i, j + 1] + DP[i + 1, j];
                }
            }
            return DP[0, 0];
        }
        ////////////////////////////Q63///////////////////////////////////////////////////////////// 
        public static int UniquePathsWithObstacles(int[,] obstacleGrid)
        {
            if (obstacleGrid[0, 0] == 1) return 0;

            int m = obstacleGrid.GetLength(0);
            int n = obstacleGrid.GetLength(1);

            int[,] dp = new int[m, n];

            for (int i = m - 1; i >= 0; i--)
            {
                if (obstacleGrid[i, n - 1] == 0)
                {
                    dp[i, n - 1] = 1;
                }
                else
                {
                    break;
                }
            }

            for (int i = n - 1; i >= 0; i--)
            {
                if (obstacleGrid[m - 1, i] == 0)
                {
                    dp[m - 1, i] = 1;
                }
                else
                {
                    break;
                }
            }

            for (int i = m - 2; i >= 0; i--)
            {
                for (int j = n - 2; j >= 0; j--)
                {
                    if (obstacleGrid[i, j] == 1)
                    {
                        dp[i, j] = 0;
                    }
                    else
                    {
                        int n1 = 0;
                        int n2 = 0;
                        if (i + 1 < m)
                            n1 = dp[i + 1, j];
                        if (j + 1 < n)
                            n2 = dp[i, j + 1];
                        dp[i, j] = n1 + n2;
                    }
                }
            }
            return dp[0, 0];
        }
        ////////////////////////////Q646///////////////////////////////////////////////////////////// 
        public static int FindLongestChain(int[,] pairs)
        {
            int n = pairs.GetLength(0);
            int[] dp = new int[pairs.Length];
            for (int i = 0; i < n; i++)
            {
                dp[i] = 1;
            }
            #region Sorting the pairs array!!!
            TwoDArray[] temparray = new TwoDArray[n];
            for (int i = 0; i < n; i++)
            {
                temparray[i] = new TwoDArray(new int[] { pairs[i, 0], pairs[i, 1] });
            }
            Array.Sort(temparray);
            for (int i = 0; i < n; i++)
            {
                pairs[i, 0] = temparray[i].TheArray[0];
                pairs[i, 1] = temparray[i].TheArray[1];
            }
            #endregion
            int max = 1;
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                {
                    if (pairs[j, 1] < pairs[i, 0] && dp[i] <= dp[j])
                    {
                        dp[i] = dp[j] + 1;
                        max = Math.Max(max, dp[i]);
                    }
                }
            }

            return max;
        }
        ////////////////////////////Q648///////////////////////////////////////////////////////////// 
        public static string ReplaceWords(IList<string> dict, string sentence)
        {
            HashSet<string> set = new HashSet<string>();
            foreach (var item in dict)
            {
                set.Add(item);
            }

            string[] words = sentence.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                string checker = WordValidater(words[i], set);
                if (checker != null)
                    words[i] = checker;
                if (i < words.Length - 1)
                    words[i] += " ";
            }
            return string.Join("", words);
        }

        private static string WordValidater(string word, HashSet<string> set)
        {
            string s = "";
            for (int i = 0; i < word.Length; i++)
            {
                s += word[i];
                if (set.Contains(s))
                    return s;
            }
            return null;
        }
        public static string ReplaceWords1(IList<string> dict, string sentence)//Second solution using trie
        {
            Trie trie = new Trie();
            foreach (var item in dict)
            {
                trie.AddWord(item);
            }

            string[] words = sentence.Split(' ');

            for (int i = 0; i < words.Length; i++)
            {
                string checker = SearchInTrie(words[i], trie);
                if (checker != null)
                    words[i] = checker;
                if (i < words.Length - 1)
                    words[i] += " ";
            }
            return string.Join("", words);


        }

        static string SearchInTrie(string word, Trie trie)
        {
            string s = "";
            for (int i = 0; i < word.Length; i++)
            {
                s += word[i];
                if (trie.Search(s))
                    return s;
            }
            return null;
        }
        ////////////////////////////Q649///////////////////////////////////////////////////////////// 
        public static string PredictPartyVictory(string senate)
        {
            List<char> list = senate.ToList<char>();
            int index = 0;
            while (true)
            {
                if (list[index] == 'R')
                {
                    int nextD = FindNext('D', index, list);
                    if (nextD == -1) return "Radiant";
                    list.RemoveAt(nextD);
                }
                else //found D
                {
                    int NextR = FindNext('R', index, list);
                    if (NextR == -1) return "Dire";
                    list.RemoveAt(NextR);
                }
                index = (index >= list.Count - 1) ? 0 : index + 1;
            }
        }

        private static int FindNext(char c, int index, List<char> list)
        {
            int count = list.Count;
            while (count > 0)
            {
                if (list[index] == c)
                    return index;
                else
                {
                    index = (index >= list.Count - 1) ? 0 : index + 1;
                }
                count--;
            }
            return -1; //not found;
        }
        ////////////////////////////Q127/////////////////////////////////////////////////////////////         
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            Queue<string> q = new Queue<string>();
            HashSet<string> set = new HashSet<string>();
            foreach (var item in wordList)
            {
                set.Add(item);
            }
            q.Enqueue(beginWord);
            set.Remove(beginWord);

            int level = 2;

            while (q.Any())
            {
                int size = q.Count;

                for (int i = size; i > 0; i--)
                {
                    string curr = q.Dequeue();

                    List<string> templist = new List<string>();

                    foreach (var item in set)
                    {
                        if (HaveOneDifference(curr, item))
                        {
                            if (item == endWord)
                                return level;
                            q.Enqueue(item);
                            templist.Add(item);
                        }

                    }
                    foreach (var item in templist)
                    {
                        set.Remove(item);
                    }
                }
                level++;
            }

            return 0;
        }

        private bool HaveOneDifference(string str1, string str2)
        {
            bool seen = false;
            for (int i = 0; i < str1.Length; i++)
            {
                if (str1[i] != str2[i])
                {
                    if (seen == true)
                        return false;
                    seen = true;
                }
            }
            return true;
        }
        ////////////////////////////Q616///////////////////////////////////////////////////////////// 
        public string AddBoldTag(string s, string[] dict)
        {
            Trie trie = FillTrie(dict); //Creates a Trie with given strings
            int[] ToBeBold = BoldRangeCreater(trie, s); //Creates a 0/1 array to show whether or not the ith character should be bold.
            return AddTagsToString(s, ToBeBold); //Creates the final string by adding tags when it's necessary.
        }
        private string AddTagsToString(string s, int[] ToBeBold)
        {
            string result = "";
            string curr = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (ToBeBold[i] == 1)
                {
                    curr += s[i];
                    if (i == s.Length - 1)
                        result += Boldize(curr);
                }
                else
                {
                    if (curr != "")
                    {
                        result += Boldize(curr);
                        curr = "";
                    }
                    result += s[i];
                }
            }
            return result;
        }
        private int[] BoldRangeCreater(Trie trie, string s)
        {
            int[] ToBeBold = new int[s.Length];
            for (int i = 0; i < s.Length; i++)
            {
                if (trie.StartsWith(s[i].ToString()))
                {
                    int[] array = SubsequenceFound(trie, s, i);
                    if (array != null)
                    {
                        for (int j = array[0]; j <= array[1]; j++)
                            ToBeBold[j] = 1;
                    }
                }
            }
            return ToBeBold;
        }
        private string Boldize(string curr)
        {
            return "<b>" + curr + "</b>";
        }
        private int[] SubsequenceFound(Trie trie, string s, int i)
        {
            if (trie.Search(s)) return new int[] { i, s.Length - 1 };
            int right = i;
            string str = "";
            while (right < s.Length)
            {
                str += s[right];
                if (trie.StartsWith(str))
                {
                    if (trie.Search(str))
                    {
                        return new int[] { i, right };
                    }

                }
                else
                    return null;
                right++;
            }
            return null;
        }
        private Trie FillTrie(string[] dict)
        {
            Trie trie = new Trie();
            foreach (var item in dict)
            {
                trie.AddWord(item);
            }
            return trie;

        }
        ////////////////////////////Q388///////////////////////////////////////////////////////////// 
        public int LengthLongestPath(string input)
        {
            List<List<int>> levelPathCount = new List<List<int>>();
            int level = 0;
            string current = "";
            bool isCurrentStringFile = false;
            int max = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (i == input.Length - 1 || input[i] == '\n')
                {
                    if (i == input.Length - 1) current += input[i];
                    if (level >= levelPathCount.Count) levelPathCount.Add(new List<int>());
                    int currentLength = (level == 0) ? current.Length : levelPathCount[level - 1][levelPathCount[level - 1].Count - 1] + current.Length + 1;
                    if (isCurrentStringFile == true)
                        max = Math.Max(max, currentLength);
                    levelPathCount[level].Add(currentLength);

                    level = 0;
                    i += 1;
                    while (i < input.Length && input[i] == '\t')
                    {
                        level++;
                        i += 1;
                    }
                    i--;
                    current = "";
                    isCurrentStringFile = false;
                }
                else
                {
                    current += input[i];
                    if (input[i] == '.')
                        isCurrentStringFile = true;
                }
            }
            return max;
        }
        public int LengthLongestPath1(string input)
        {
            string[] dirs = input.Split('\n');
            List<int> levels = new List<int>();
            levels.Add(dirs[0].Length);
            int max = 0;

            for (int i = 1; i < dirs.Length; i++)
            {
                int level = FindLevel(dirs[i]);
                int currentLength = (level == 0) ? dirs[i].Length : levels[level - 1] + dirs[i].Length - level + 1;
                if (level >= levels.Count) levels.Add(currentLength);
                else levels[level] = currentLength;
                if (dirs[i].Contains('.'))
                {
                    max = Math.Max(max, currentLength);
                }
            }
            return max;
        }

        private int FindLevel(string s)
        {
            int level = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\t') level++;
                else break;
            }
            return level;
        }
        ////////////////////////////Q482///////////////////////////////////////////////////////////// 
        public string LicenseKeyFormatting(string S, int K)
        {
            if (S == "-") return "";
            string withOutDashes = RemoveDashes(S);
            int d = withOutDashes.Length % K;
            int counter = d;
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < withOutDashes.Length; i++)
            {
                if (i == counter)
                {
                    if (i != 0)
                        res.Append("-");
                    counter += K;
                }
                res.Append(withOutDashes[i]);
            }
            return res.ToString().ToUpper();
        }
        private string RemoveDashes(string s)
        {
            StringBuilder str = new StringBuilder();
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != '-') str.Append(s[i]);
            }
            return str.ToString();
        }
        ////////////////////////////Q298///////////////////////////////////////////////////////////// 
        int MaximumSequence = 1;
        public int LongestConsecutive(Treenode root)
        {
            if (root == null) return 0;
            LongestConsecutive(root, null, 1);
            return MaximumSequence;
        }
        private void LongestConsecutive(Treenode root, Treenode prev, int currSeq)
        {
            if (root == null) return;

            currSeq = (prev != null && root.data - prev.data == 1) ? currSeq + 1 : 1;

            MaximumSequence = Math.Max(MaximumSequence, currSeq);

            LongestConsecutive(root.left, root, currSeq);
            LongestConsecutive(root.right, root, currSeq);
        }
        ////////////////////////////Q/////////////////////////////////////////////////////////////Contest 8/5/17 (Brute Force)
        IList<int> resultList = new List<int>();
        int minCoins = int.MaxValue;
        public IList<int> CheapestJump(int[] A, int B)
        {
            DFS(A, B, 0, 0, new List<int>());
            return resultList;
        }
        private void DFS(int[] A, int B, int startIndex, int currentCoins, List<int> currentPath)
        {
            List<int> newlist = new List<int>(currentPath);
            newlist.Add(startIndex + 1);
            currentCoins += A[startIndex];

            if (startIndex == A.Length - 1)
            {
                if (currentCoins < minCoins)
                {
                    minCoins = currentCoins;
                    resultList = newlist;
                }
            }
            else
            {
                for (int i = startIndex + 1; i <= startIndex + B; i++)
                {
                    if (i >= A.Length || A[i] == -1) continue;

                    DFS(A, B, i, currentCoins + A[i], newlist);
                }
            }

        }
        ////////////////////////////Q467///////////////////////////////////////////////////////////// 
        public int CountSubstrings(String s)
        {
            int count = 0;
            if (s == null || s.Length == 0) return count;

            for (int i = 0; i < s.Length; i++)
            {
                count += CheckForPalindrome(i, i, s) + CheckForPalindrome(i, i + 1, s);
            }
            return count;
        }
        private int CheckForPalindrome(int start, int end, string s)
        {
            int count = 0;
            while (start >= 0 && end < s.Length && s[start] == s[end])
            {
                count++;
                start--;
                end++;
            }
            return count;
        }
        ////////////////////////////Q653/////////////////////////////////////////////////////////////Contest 8/5/17
        public bool FindTarget(Treenode root, int k)
        {
            if (root == null) return false;
            HashSet<int> set = new HashSet<int>();
            Queue<Treenode> q = new Queue<Treenode>();
            q.Enqueue(root);

            while (q.Any())
            {
                Treenode temp = q.Dequeue();
                int required = k - temp.data;
                if (set.Contains(required)) return true;
                else set.Add(temp.data);
                if (temp.left != null) q.Enqueue(temp.left);
                if (temp.right != null) q.Enqueue(temp.right);
            }
            return false;
        }
        ////////////////////////////Q654/////////////////////////////////////////////////////////////Contest 8/5/17
        public Treenode ConstructMaximumBinaryTree(int[] nums)
        {
            return ConstructMaximumBinaryTree(nums, 0, nums.Length - 1);
        }
        private Treenode ConstructMaximumBinaryTree(int[] nums, int start, int end)
        {
            if (start > end) return null;

            int maxIndex = FindMaxIndex(nums, start, end);

            Treenode root = new Treenode(nums[maxIndex]);

            root.left = ConstructMaximumBinaryTree(nums, start, maxIndex - 1);
            root.right = ConstructMaximumBinaryTree(nums, maxIndex + 1, end);

            return root;
        }
        private int FindMaxIndex(int[] nums, int start, int end)
        {
            int maxIndex = -1;
            int max = int.MinValue;
            for (int i = start; i <= end; i++)
            {
                if (nums[i] >= max)
                {
                    max = nums[i];
                    maxIndex = i;
                }
            }
            return maxIndex;
        }
        ////////////////////////////Q655/////////////////////////////////////////////////////////////Contest 8/5/17
        public IList<IList<string>> PrintTree(Treenode root)
        {
            int m = Height(root);
            int n = (m == 1) ? 1 : (int)Math.Pow(2, m) - 1;

            IList<IList<string>> lists = CreateLists(m, n);

            FillLists(root, 0, 0, n - 1, lists);

            return lists;
        }
        private IList<IList<string>> CreateLists(int m, int n)
        {
            IList<IList<string>> lists = new List<IList<string>>();

            for (int i = 0; i < m; i++)
            {
                lists.Add(new List<string>());
                for (int j = 0; j < n; j++)
                {
                    lists[i].Add("");
                }
            }
            return lists;
        }
        private void FillLists(Treenode root, int index, int start, int end, IList<IList<string>> lists)
        {
            if (root == null) return;

            int mid = (start + end) / 2;

            lists[index][mid] = root.data.ToString();

            if (root.left != null)
                FillLists(root.left, index + 1, start, mid - 1, lists);
            if (root.right != null)
                FillLists(root.right, index + 1, mid + 1, end, lists);
        }
        ////////////////////////////Q79/////////////////////////////////////////////////////////////Brute Force (time out)      
        bool wordSearchResult = false;
        public bool Exist(char[,] board, string word)
        {
            if ((board.GetLength(0) == 0 && board.GetLength(1) == 0) || board == null) return false;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (word[0] == board[i, j])
                    {
                        WordSearchDFS(i, j, word, 0, board, new bool[board.GetLength(0), board.GetLength(1)]);
                        if (wordSearchResult == true) return true;
                    }
                }
            }
            return false;
        }
        private void WordSearchDFS(int i, int j, string word, int index, char[,] board, bool[,] visited)
        {
            if (i < 0 || i >= board.GetLength(0) || j < 0 || j >= board.GetLength(1) || board[i, j] != word[index] || visited[i, j] == true)
                return;

            if (index == word.Length - 1)
            {
                wordSearchResult = true;
                return;
            }

            visited[i, j] = true;
            int[,] directions = { { 1, 0 }, { -1, 0 }, { 0, -1 }, { 0, 1 } };

            for (int k = 0; k < directions.GetLength(0); k++)
            {
                WordSearchDFS(i + directions[k, 0], j + directions[k, 1], word, index + 1, board, visited);
            }

        }
        ////////////////////////////Q79/////////////////////////////////////////////////////////////Without visited, using stack for keeping track of visiteds.
        public bool ExistII(char[,] board, string word)
        {
            if (word == null || board == null || (board.GetLength(0) == 0 && board.GetLength(1) == 0) || word.Length == 0) return false;

            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (word[0] == board[i, j])
                        if (DFSWordSearch(board, i, j, 0, word))
                            return true;
                }
            }

            return false;
        }
        Stack<char> tempCharStack = new Stack<char>();
        private bool DFSWordSearch(char[,] board, int i, int j, int index, string word)
        {
            if (index == word.Length)
                return true;

            if (i < 0 || j < 0 || i == board.GetLength(0) || j == board.GetLength(1) || word[index] != board[i, j])
                return false;

            tempCharStack.Push(board[i, j]);
            board[i, j] = '\n';

            bool check = DFSWordSearch(board, i, j + 1, index + 1, word) ||
                        DFSWordSearch(board, i + 1, j, index + 1, word) ||
                        DFSWordSearch(board, i, j - 1, index + 1, word) ||
                        DFSWordSearch(board, i - 1, j, index + 1, word);

            board[i, j] = tempCharStack.Pop();

            if (check == true)
                return true;

            return check;
        }
        ////////////////////////////Q209///////////////////////////////////////////////////////////// 
        public int MinSubArrayLen(int s, int[] nums)
        {
            if (s == 0 || nums.Length == 0)
                return 0;

            int sum = 0, min = int.MaxValue, left = 0;

            for (int i = 0; i < nums.Length; i++)
            {
                sum += nums[i];

                while (sum >= s)
                {
                    min = Math.Min(min, i - left + 1);
                    sum -= nums[left];
                    left++;
                }
            }

            return (min == int.MaxValue) ? 0 : min;
        }
        ////////////////////////////Q418///////////////////////////////////////////////////////////// 
        public int WordsTyping(string[] sentence, int rows, int cols)
        {
            int index = 0, count = 0, currRow = 0, currCol = 0;

            while (currRow < rows)
            {
                int currLength = sentence[index].Length;

                if (currCol + currLength <= cols) //it fits in the current row
                {
                    if (index == sentence.Length - 1)//if last word
                    {
                        count++;
                        index = 0;
                    }
                    else
                    {
                        index++;
                    }

                    if (currCol + currLength - 1 >= cols - 2) //filled until the last postition in this row or one less than last.
                    {
                        currRow++;
                        currCol = 0;
                    }
                    else
                    {
                        currCol = currCol + currLength + 1;
                    }
                }
                else
                {
                    currCol = 0;
                    currRow++;
                }

            }
            return count;
        }
        ////////////////////////////Q657///////////////////////////////////////////////////////////// 
        public bool JudgeCircle(string moves)
        {
            if (moves == null || moves.Length == 0) return false;

            int dirs = 0;

            for (int i = 0; i < moves.Length; i++)
            {
                switch (moves[i])
                {
                    case 'U':
                        dirs += 2;
                        break;
                    case 'D':
                        dirs -= 2;
                        break;
                    case 'R':
                        dirs += 1;
                        break;
                    case 'L':
                        dirs -= 1;
                        break;
                    default:
                        break;
                }
            }
            return (dirs == 0);
        }
        ////////////////////////////Q658///////////////////////////////////////////////////////////// 
        //public IList<int> FindClosestElements(IList<int> arr, int k, int x)
        //{
        //    List<int> res = new List<int>();

        //    int index = 0;
        //    while (x<arr.Count-1 && arr[index] < x) index++;

        //    int right = index + 1;
        //    int left = index - 1;

        //    res.Add(arr[index]);

        //    int count = k-1;
        //    while(count>0)
        //    {
        //        if(right>arr.Count-1)
        //        {
        //            res.Add(arr[left]);
        //            left--;
        //        }
        //        else if(left<0)
        //        {
        //            res.Add(arr[right]);
        //            right++;
        //        }
        //        else
        //        {
        //            if(Math.Abs(arr[left]-x)<Math.Abs(arr[right]-x))
        //            {
        //                res.Add(arr[left]);
        //                left--;
        //            }
        //            else
        //            {
        //                res.Add(arr[right]);
        //                right++;
        //            }
        //        }
        //        count--;
        //    }

        //    res.Sort();
        //    return res;
        //}

        public IList<int> FindClosestElements(IList<int> arr, int k, int x)
        {
            while (arr.Count > k)
            {
                int first = 0;
                int last = arr.Count - 1;
                if (Math.Abs(arr[first] - x) <= Math.Abs(arr[last] - x))
                    arr.RemoveAt(last);
                else
                    arr.RemoveAt(first);
            }
            return arr;
        }
        ////////////////////////////Q659///////////////////////////////////////////////////////////// 
        public bool IsPossible(int[] nums)
        {
            Dictionary<int, int> lastOfSeq = new Dictionary<int, int>();
            List<int> checkLater = new List<int>();
            List<int> currSeqList = new List<int>(); //only stores the first 2 numbers of the sequnce:
            //uses: 1)to check last nums, 2)if the last sequence is unfinished.

            for (int i = 0; i < nums.Length; i++)
            {
                int currNum = nums[i];
                if (currSeqList.Count == 0) //first number in current sequence
                {
                    currSeqList.Add(currNum);
                }
                else
                {
                    int lastNum = currSeqList.Last<int>();

                    if (lastNum == currNum) //duplicate in current sequence
                    {
                        checkLater.Add(currNum);
                    }
                    else
                        if (currNum - lastNum > 1) return false;
                    else
                    {
                        if (currSeqList.Count == 2) //current number is the last number in current sequence
                        {
                            if (lastOfSeq.ContainsKey(currNum)) lastOfSeq[currNum]++;
                            else lastOfSeq.Add(currNum, 1);

                            currSeqList = new List<int>();
                        }
                        else //if it's second number in current sequence
                        {
                            currSeqList.Add(currNum);
                        }
                    }

                }
            }
            foreach (int n in currSeqList)
                checkLater.Add(n);

            foreach (int n in checkLater)
            {
                if (lastOfSeq.ContainsKey(n - 1) && lastOfSeq[n - 1] > 0)
                {
                    lastOfSeq[n - 1]--;
                    if (lastOfSeq.ContainsKey(n)) lastOfSeq[n]++;
                    else lastOfSeq.Add(n, 1);
                }
                else return false;
            }

            return true;
        }
        ////////////////////////////Q163///////////////////////////////////////////////////////////// 
        public IList<string> FindMissingRanges(int[] nums, int lower, int upper)
        {
            IList<string> res = new List<string>();
            if (nums.Length == 0 || nums == null)
            {
                res.Add((lower == upper) ? upper.ToString() : lower + "->" + upper);
            }

            int find = lower;

            for (int i = 0; i < nums.Length; i++)
            {
                if (i > 0 && nums[i] == nums[i - 1]) continue;

                if (find != nums[i])
                    res.Add((nums[i] - find == 1) ? (find).ToString() : find + "->" + (nums[i] - 1));

                find = nums[i] + 1;

                if (i == nums.Length - 1 && nums[i] != upper)
                    res.Add((find == upper) ? upper.ToString() : find + "->" + upper);
            }

            return res;
        }
        ////////////////////////////Q66///////////////////////////////////////////////////////////// 
        public int[] PlusOne(int[] digits)
        {
            int n = digits.Length;

            for (int i = n - 1; i >= 0; i--)
            {
                if (digits[i] < 9)
                {
                    digits[i]++;
                    return digits;
                }
                digits[i] = 0;
            }
            int[] ret = new int[n + 1];
            ret[0] = 1;

            return ret;
        }
        ////////////////////////////Q72///////////////////////////////////////////////////////////// 
        public int MinDistance(string word1, string word2)
        {
            if (word1.Length == 0) return word2.Length;
            if (word2.Length == 0) return word1.Length;

            int m = word1.Length + 1;
            int n = word2.Length + 1;

            int[,] DP = new int[m, n];

            DP[0, 0] = 0;

            for (int i = 1; i < m; i++)
                DP[i, 0] = i;

            for (int i = 1; i < n; i++)
                DP[0, i] = i;

            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    int min = Math.Min(DP[i - 1, j - 1], Math.Min(DP[i - 1, j], DP[i, j - 1]));
                    if (word1[i - 1] == word2[j - 1])
                        DP[i, j] = DP[i - 1, j - 1];
                    else
                        DP[i, j] = min + 1;
                }
            }
            return DP[m - 1, n - 1];
        }
        ////////////////////////////Q47///////////////////////////////////////////////////////////// 
        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            List<IList<int>> res = new List<IList<int>>();
            List<int> input = nums.ToList<int>();
            PermuteUnique(input, new List<int>(), res);
            return res;
        }
        private void PermuteUnique(List<int> input, List<int> output, List<IList<int>> res)
        {
            if (input.Count == 0)
            {
                res.Add(output);
            }
            else
            {
                HashSet<int> set = new HashSet<int>();
                int count = input.Count;
                for (int i = 0; i < count; i++)
                {
                    if (!set.Add(input[i])) continue;
                    List<int> new_input = new List<int>(input);
                    List<int> new_output = new List<int>(output);
                    new_output.Add(input[i]);
                    new_input.RemoveAt(i);
                    PermuteUnique(new_input, new_output, res);
                }
            }
        }
        ////////////////////////////Q662/////////////////////////////////////////////////////////////Contest 8/19/17
        public int WidthOfBinaryTree(Treenode root)
        {
            if (root == null) return 0;
            Queue<Treenode> Q = new Queue<Treenode>();
            int max = 0;

            Q.Enqueue(root);

            while (Q.Any()) //while Q is not empty
            {
                int currLevelCount = Q.Count;
                int pendingNullCount = 0;
                int actualLevelCount = 0;
                bool notNullSeen = false;

                while (currLevelCount > 0)
                {
                    Treenode currNode = Q.Dequeue();

                    if (currNode == null)
                    {
                        if (notNullSeen == true)
                        {
                            pendingNullCount++;
                            Q.Enqueue(null);
                            Q.Enqueue(null);
                        }
                    }
                    else
                    {
                        notNullSeen = true;
                        actualLevelCount += pendingNullCount + 1;
                        pendingNullCount = 0;

                        Q.Enqueue(currNode.left);
                        Q.Enqueue(currNode.right);
                    }
                    currLevelCount--;
                }
                if (notNullSeen == false) break;
                max = Math.Max(actualLevelCount, max);
            }
            return max;
        }
        ////////////////////////////Q663/////////////////////////////////////////////////////////////Contest 8/19/17
        public bool CheckEqualTree(Treenode root)
        {
            UpdateTreeSumDFS(root);
            return CheckEqualDFS(root, root.data);
        }
        private void UpdateTreeSumDFS(Treenode root)
        {
            if (root == null) return;

            UpdateTreeSumDFS(root.left);
            UpdateTreeSumDFS(root.right);

            if (root.left != null)
                root.data += root.left.data;

            if (root.right != null)
                root.data += root.right.data;
        }
        private bool CheckEqualDFS(Treenode root, int rootSum)
        {
            if (root == null) return false;

            if (root.left != null)
            {
                if (rootSum - root.left.data == root.left.data)
                    return true;
            }

            if (root.right != null)
            {
                if (rootSum - root.right.data == root.right.data)
                    return true;
            }

            return CheckEqualDFS(root.left, rootSum) || CheckEqualDFS(root.right, rootSum);

        }
        ////////////////////////////Q661/////////////////////////////////////////////////////////////Contest 8/19/17
        public int[,] ImageSmoother(int[,] M)
        {
            int m = M.GetLength(0);
            int n = M.GetLength(1);

            int[,] Avg = new int[m, n];

            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int startI = (i - 1 >= 0) ? i - 1 : i;
                    int endI = (i + 1 < m) ? i + 1 : i;
                    int startJ = (j - 1 >= 0) ? j - 1 : j;
                    int endJ = (j + 1 < n) ? j + 1 : j;

                    Avg[i, j] = FindAverage(M, startI, endI, startJ, endJ);
                }
            }
            return Avg;
        }
        private int FindAverage(int[,] m, int startI, int endI, int startJ, int endJ)
        {
            int count = 0;
            int sum = 0;
            for (int i = startI; i <= endI; i++)
            {
                for (int j = startJ; j <= endJ; j++)
                {
                    sum += m[i, j];
                    count++;
                }
            }
            return sum / count;
        }
        ////////////////////////////Q///////////////////////////////////////////////////////////// 
        public IList<Interval> MergeII(IList<Interval> intervals)
        {
            List<Interval> res = new List<Interval>();
            if (intervals == null || intervals.Count == 0) return res;

            int[] starts = new int[intervals.Count];
            int[] ends = new int[intervals.Count];

            for (int i = 0; i < intervals.Count; i++)
            {
                starts[i] = intervals[i].start;
                ends[i] = intervals[i].end;
            }

            Array.Sort(starts);
            Array.Sort(ends);

            List<int> order = new List<int>();

            for (int i = 1; i < starts.Length; i++)
            {
                if (ends[i - 1] + 2 <= starts[i])
                {
                    order.Add(ends[i - 1]);
                    order.Add(starts[i]);
                }
            }

            res.Add(new Interval(starts[0], order[0]));

            for (int i = 1; i < order.Count - 1; i += 2)
            {
                res.Add(new Interval(order[i], order[i + 1]));
            }

            res.Add(new Interval(order[order.Count - 1], ends[ends.Length - 1]));
            return res;
        }
        ////////////////////////////Q85///////////////////////////////////////////////////////////// 
        public int MaximalRectangle(char[,] matrix)
        {
            int m = matrix.GetLength(0);
            int n = matrix.GetLength(1);

            if (m == 0 || n == 0) return 0;

            CellTracker[,] DP = new CellTracker[m, n];

            DP[0, 0] = new CellTracker();
            DP[0, 0].R = (matrix[0, 0] == '1') ? 1 : 0;
            DP[0, 0].D = (matrix[0, 0] == '1') ? 1 : 0;
            int max = (matrix[0, 0] == '1') ? 1 : 0;

            for (int i = 1; i < n; i++)
            {
                DP[0, i] = new CellTracker();
                DP[0, i].R = (matrix[0, i] == '0') ? 0 : DP[0, i - 1].R + 1;
                DP[0, i].D = (matrix[0, i] == '0') ? 0 : 1;
                max = Math.Max(DP[0, i].R, max);
            }

            for (int j = 1; j < m; j++)
            {
                DP[j, 0] = new CellTracker();
                DP[j, 0].D = (matrix[j, 0] == '0') ? 0 : DP[j - 1, 0].D + 1;
                DP[j, 0].R = (matrix[j, 0] == '0') ? 0 : 1;
                max = Math.Max(DP[j, 0].D, max);
            }

            for (int i = 1; i < m; i++)
            {
                for (int j = 1; j < n; j++)
                {
                    DP[i, j] = new CellTracker();
                    if (matrix[i, j] == '0')
                        continue;

                    DP[i, j].R = DP[i, j - 1].R + 1;
                    DP[i, j].D = DP[i - 1, j].D + 1;

                    int down = (DP[i, j - 1].D == 0) ? DP[i, j].D : Math.Min(DP[i, j].D, DP[i, j - 1].D);
                    int right = (DP[i - 1, j].R == 0) ? DP[i, j].R : Math.Min(DP[i, j].R, DP[i - 1, j].R);

                    max = Math.Max(max, down * right);
                }
            }

            return max;
        }
        ////////////////////////////Q665///////////////////////////////////////////////////////////// 
        public bool CheckPossibility(int[] nums)
        {
            if (nums.Length == 0 || nums == null) return false;
            if (nums.Length == 1) return true;

            bool changed = false;

            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] > nums[i + 1])
                {
                    if (changed) return false;
                    changed = true;

                    if (i > 0 && nums[i + 1] < nums[i - 1])
                        nums[i + 1] = nums[i];
                    else nums[i] = nums[i + 1];
                }
            }
            return true;
        }
        ////////////////////////////Q666///////////////////////////////////////////////////////////// 
        public int PathSum2(int[] nums)
        {
            if (nums.Length == 0 || nums == null) return 0;

            List<int[]> currentSum = new List<int[]>();
            int totalSum = nums[0] % 10; //value of the root
            currentSum.Add(new int[1]);
            currentSum[0][0] = nums[0] % 10;

            for (int i = 1; i < nums.Length; i++)
            {
                int level = (nums[i] / 100) % 10;
                int pos = nums[i] / 10 % 10 - 1;
                int val = nums[i] % 10;

                if (level > currentSum.Count) //new level
                {
                    int[] array = new int[(int)Math.Pow(2, level - 1)];
                    int pathSum = currentSum[level - 2][pos / 2] + val;
                    array[pos] = pathSum;
                    currentSum.Add(array);
                    totalSum += val;
                }
                else //level already exist, so new path
                {
                    int[] array = currentSum[level - 1];
                    int pathSum = currentSum[level - 2][pos / 2] + val;
                    array[pos] = pathSum;
                    totalSum += pathSum;
                }
            }

            return totalSum;
        }
        public int PathSum(int[] nums)
        {
            if (nums.Length == 0 || nums == null) return 0;
            List<int[]> sumSoFar = new List<int[]>();
            int totalSum = 0;
            string lastParent = "No Parent!";

            for (int i = 0; i < nums.Length; i++)
            {
                int level = (nums[i] / 100) % 10 - 1;
                int pos = nums[i] / 10 % 10 - 1;
                int val = nums[i] % 10;
                int parentLevel = level - 1;
                int parentPosition = pos / 2;
                int[] currentLevelSum = null;

                if (sumSoFar.Count == level)
                {
                    currentLevelSum = new int[(int)Math.Pow(2, level)];
                    sumSoFar.Add(currentLevelSum);
                }
                else
                    currentLevelSum = sumSoFar[level];

                int currentPathSum = (i == 0) ? val : val + sumSoFar[parentLevel][parentPosition];

                currentLevelSum[pos] = parentPosition;

                if (lastParent != "" + parentLevel + parentPosition)
                    totalSum += val;
                else
                    totalSum += currentPathSum;

                lastParent = "" + parentLevel + parentPosition;
            }
            return totalSum;
        }
        /////////////////////////////////////////Q671////////////////////////////////////////////////////////////////////////////////////Contest 9/2/2017
        public int FindSecondMinimumValue(Treenode root)
        {
            int rootVal = root.data;
            int secondSmall = int.MaxValue;
            bool diffFound = false;
            Queue<Treenode> Q = new Queue<Treenode>();
            Q.Enqueue(root);

            while (Q.Any()) //while Q is not empty
            {
                Treenode curr = Q.Dequeue();
                if (curr.data != rootVal && curr.data <= secondSmall)
                {
                    secondSmall = curr.data;
                    diffFound = true;
                }
                if (curr.left != null)
                {
                    Q.Enqueue(curr.left);
                    Q.Enqueue(curr.right);
                }
            }
            return (secondSmall == int.MaxValue && !diffFound) ? -1 : secondSmall;
        }
        ////////////////////////////Q672/////////////////////////////////////////////////////////////Contest 9/2/2017
        public Treenode TrimBST(Treenode root, int L, int R)
        {
            if (root == null) return null;

            if (root.data < L)
                return TrimBST(root.right, L, R);
            if (root.data > R)
                return TrimBST(root.left, L, R);

            root.left = TrimBST(root.left, L, R);
            root.right = TrimBST(root.right, L, R);

            return root;
        }
        ////////////////////////////Q673/////////////////////////////////////////////////////////////Contest 9/2/2017
        public int MaximumSwap(int num)
        {
            int[] digits = new int[num.ToString().Length];

            for (int i = 0; i < digits.Length; i++)
            {
                digits[digits.Length - 1 - i] = num % 10;
                num /= 10;
            }

            int[] sortedDigits = new int[digits.Length];
            digits.CopyTo(sortedDigits, 0);
            Array.Sort(sortedDigits);
            Array.Reverse(sortedDigits);

            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i] != sortedDigits[i])
                {
                    int j = digits.Length - 1;
                    while (j > i && digits[j] != sortedDigits[i])
                    {
                        j--;
                    }

                    if (j > i)
                    {
                        int temp = digits[j];
                        digits[j] = digits[i];
                        digits[i] = temp;
                    }
                    break;
                }
            }
            int res = 0;
            for (int i = 0; i < digits.Length; i++)
            {
                res += digits[digits.Length - i - 1] * (int)Math.Pow(10, i);
            }

            return res;
        }
    }
}

////////////////////////////Q///////////////////////////////////////////////////////////// 