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

        public static string Print<T>(this T[][] inputs, Func<T, string> convert)
        {
            var sb = new StringBuilder();
            sb.Append("\n");

            for (int j = 0; j < inputs[0].Length; j++)
            {
                for (int i = 0; i < inputs.Length; i++)
                {
                    sb.Append(string.Join("", convert(inputs[i][j])));
                }
                sb.Append("\n");
            }

            return sb.ToString();
        }

        public static int Count<T>(this T[][] inputs, Func<T, bool> predicate)
        {
            var count = 0;

            for (int i = 0; i < inputs.Length; i++)
            {
                for (int j = 0; j < inputs[i].Length; j++)
                {
                    if (predicate(inputs[i][j])) count++;
                }
            }

            return count;
        }
    }
}
