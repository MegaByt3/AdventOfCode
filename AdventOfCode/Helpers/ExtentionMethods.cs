using System.Collections;

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
    }
}
