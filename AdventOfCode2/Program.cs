using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day1
{
    // https://adventofcode.com/2021/day/1
    class Program
    {
        static void Main(string[] args)
        {
            var inputs = File.ReadLines("Input.txt").Select(x => int.Parse(x)).ToArray();

            Console.WriteLine(Part1(inputs));
            Console.WriteLine(Part2(inputs, 3));
            Console.ReadLine();
        }

        static public int Part1(int[] numbers)
        {
            var nrOfIncreases = 0;

            for (int i = 0; i < numbers.Length - 1; i++)
            {
                if (numbers[i + 1] > numbers[i]) nrOfIncreases++;
            }

            return nrOfIncreases;
        }

        static public int Part2(int[] numbers, int windowRange)
        {
            var nrOfIncreases = 0;
            var previousWindowSum = numbers[0..windowRange].Sum();

            for (int i = 0; i < numbers.Length - windowRange; i++)
            {
                var nextWindowSum = numbers[(i+1)..(i+1+windowRange)].Sum();
                if (nextWindowSum > previousWindowSum) nrOfIncreases++;
                previousWindowSum = nextWindowSum;
            }

            return nrOfIncreases;
        }
    }
}
