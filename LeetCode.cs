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
                        CheckAdjecent(i, j, grid,visited);
                    }
                    visited[i, j] = true;
                }

            }
            return count;
        }
        private static void CheckAdjecent(int i, int j,char[,] grid, bool[,] visited)
        {
            int m = visited.GetLength(0);
            int n = visited.GetLength(1);
            Point[] adj = { new Point(0, 1), new Point(1, 0), new Point(-1, 0), new Point(0, -1) };
            for(int k=0; k<=3; k++)
            {
                int new_i = i + adj[k].X;
                int new_j = j + adj[k].Y;
                if (new_i >= 0 && new_i < m && new_j>= 0 && new_j<n && visited[new_i,new_j]==false && grid[new_i,new_j]=='1')
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
        public static IList<int[]>GetSkyline(int[,] buildings)//The Skyline Problem (Perfect job by me)
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
            for(int i=0; i<silhouette.Length; i++)
            {
                if(silhouette[i]>flow)
                {
                    result.Add(new int[] { i,silhouette[i] });
                    flow = silhouette[i];
                }
                if(silhouette[i]<flow)
                {
                    result.Add(new int[] { i - 1, silhouette[i] });
                    flow = silhouette[i];
                }
            }
            return result;


        }
        private static int MostRightBuilding(int[,]buildings) //finding where the last building finishes
        {
            int max = 0;
            int n = buildings.GetLength(0); //number of buildings
            for(int i=0; i<n; i++)
            {
                int right = buildings[i, 1]; //the middle element of each building which is its right point
                if (right > max) max = right;
            }
            return max;
        }
        private static void FillSilhouette(int[,] buildings, int[] silhouette)
        {
            int n = buildings.GetLength(0);
            for(int i=0; i<n; i++)
            {
                for(int j=buildings[i,0]; j<=buildings[i,1]; j++)
                {
                    if(silhouette[j]<buildings[i,2])
                        silhouette[j] = buildings[i, 2];
                }
            }
        }
        ////////////////////////////Q53/////////////////////////////////////////////////////////////        
        public static int MaxSubArray(int[] nums)
        {
            int sum = 0; int min_sum = 0; int max_sum = 0;

            for(int i=0; i<nums.Length; i++)
            {
                sum += nums[i];
                if(sum<min_sum)
                {
                    min_sum = sum;
                }
                if(sum-min_sum>max_sum)
                {
                    max_sum = sum-min_sum;
                }
            }
            return max_sum;
        }
        ////////////////////////////Q103/////////////////////////////////////////////////////////////
        public static List<List<int>>ZigzaglevelOrder(Treenode root)
        {

        }
    }


}
////////////////////////////Q/////////////////////////////////////////////////////////////