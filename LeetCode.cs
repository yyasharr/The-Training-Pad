using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            if (num < 0) return "minus "+NumberToWords(Math.Abs(-num));
            int mod = 0;
            //for billions
            mod = num / 1000000000;
            if (mod > 0)
                result += ones[mod] + " billion ";
            num = num - (num / 1000000000) * 1000000000;
            //for millions
            mod = num / 1000000;
            if (mod > 0)
                result += NumberToWords_per_thousand(num/1000000) + " million ";
            num = num - (num / 1000000) * 1000000;
            //for thousands
            mod = num / 1000;
            if (mod > 0)
                result += NumberToWords_per_thousand(num/1000)+ " thousand ";
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
        ////////////////////////////Q/////////////////////////////////////////////////////////////
    }


}
////////////////////////////Q/////////////////////////////////////////////////////////////