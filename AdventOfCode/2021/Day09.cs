using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day08
{
    // https://adventofcode.com/2021/day/9
    public class Day09 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "09";

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        private List<(List<string> inputSegments, List<string> outputSegments)> _inputs = new List<(List<string>, List<string>)>();

        public Day09(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day);
            var regex = new Regex(@"(\w+)");

            foreach (var rawInput in rawInputs)
            {
                var matches = regex.Matches(rawInput);
                var segments = matches.Select(x => x.Value).ToList();
                segments = segments.Select(x => x.SortString()).ToList();

                var inputSegments = segments.Take(10).ToList();
                var outputSegments = segments.Skip(10).ToList();
                _inputs.Add((inputSegments, outputSegments));
            }

            Logger.LogDebug($"# inputs: {rawInputs.Count}");
        }

        // Part1 result: 416
        public override object SolvePart1()
        {
            var result = new List<int>();

            foreach (var input in _inputs)
            {
                var codes = Decode(input.inputSegments);

                foreach(var outputSegment in input.outputSegments)
                {
                    result.Add(codes.Single(x => x.Value.Equals(outputSegment)).Key);
                }
            }

            return result.Count(x => x == 1 || x == 4 || x == 7 || x == 8);
        }

        // Part2 result: 1043697
        public override object SolvePart2()
        {
            var result = 0;

            foreach (var input in _inputs)
            {
                var codes = Decode(input.inputSegments);

                var number = 0;
                var devider = 1000;
                foreach (var outputSegment in input.outputSegments)
                {
                    number += codes.Single(x => x.Value.Equals(outputSegment)).Key * devider;
                    devider /= 10;
                }

                result += number;
            }

            return result;
        }

        private Dictionary<int, string> Decode(List<string> inputSegments)
        {
            var codes = new Dictionary<int, string>();

            codes.Add(1, inputSegments.Single(x => x.Length == 2));
            codes.Add(4, inputSegments.Single(x => x.Length == 4));
            codes.Add(7, inputSegments.Single(x => x.Length == 3));
            codes.Add(8, inputSegments.Single(x => x.Length == 7));

            var sixSegmentNumbers = inputSegments.Where(x => x.Length == 6).ToList();
            codes.Add(6, sixSegmentNumbers.Single(x => x.Except(codes[1]).Count() == 5));
            sixSegmentNumbers.Remove(codes[6]);
            codes.Add(9, sixSegmentNumbers.Single(x => x.Except(codes[4]).Count() == 2));
            sixSegmentNumbers.Remove(codes[9]);
            codes.Add(0, sixSegmentNumbers.Single());

            var fiveSegmentNumbers = inputSegments.Where(x => x.Length == 5).ToList();
            codes.Add(3, fiveSegmentNumbers.Single(x => x.Except(codes[1]).Count() == 3));
            fiveSegmentNumbers.Remove(codes[3]);
            codes.Add(5, fiveSegmentNumbers.Single(x => x.Except(codes[4]).Count() == 2));
            fiveSegmentNumbers.Remove(codes[5]);
            codes.Add(2, fiveSegmentNumbers.Single());

            return codes;
        }
    }
}
