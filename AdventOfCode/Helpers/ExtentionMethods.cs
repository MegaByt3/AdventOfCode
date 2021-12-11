using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace AdventOfCode.Helpers
{
    public static class ExtentionMethods
    {
        public static BitArray Reverse(this BitArray array)
        {
            int length = array.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++)
            {
                bool bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }

            return new BitArray(array);
        }

        public static int ConvertToNumber(this BitArray bitarray)
        {
            var array = new int[1];
            bitarray.Reverse().CopyTo(array, 0);
            return array[0];
        }

        public static string SortString(this string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }

        public static string Print(this int[][] inputs)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < inputs.Length; i++)
            {
                sb.AppendLine(string.Join("", inputs[i]));
            }

            return sb.ToString();
        }
    }
}
