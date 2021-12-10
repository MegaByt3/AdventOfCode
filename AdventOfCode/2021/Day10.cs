using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day10
{
    // https://adventofcode.com/2021/day/10
    public class Day10 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "10";

        private List<string> _inputs = new List<string>();
        private readonly List<(char open, char close)> _charSets = new List<(char open, char close)>();
        private readonly Dictionary<char, int> _pointsA = new Dictionary<char, int>();
        private readonly Dictionary<char, int> _pointsB = new Dictionary<char, int>();

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        public Day10(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day);
            _inputs = rawInputs;

            _charSets.Add(('(', ')'));
            _charSets.Add(('[', ']'));
            _charSets.Add(('{', '}'));
            _charSets.Add(('<', '>'));

            _pointsA.Add(')', 3);
            _pointsA.Add(']', 57);
            _pointsA.Add('}', 1197);
            _pointsA.Add('>', 25137);

            _pointsB.Add('(', 1);
            _pointsB.Add('[', 2);
            _pointsB.Add('{', 3);
            _pointsB.Add('<', 4);
        }

        // Part1 result: 339411
        public override object SolvePart1()
        {
            var score = 0;

            foreach (var input in _inputs)
            {
                var corruptedChar = GetCorruptedChar(input);
                if (corruptedChar != null)
                {
                    score += _pointsA[corruptedChar.Value];
                }
            }

            return score;
        }

        // Part2 result: 2289754624
        public override object SolvePart2()
        {
            var scores = new List<long>();

            foreach (var input in _inputs.ToList())
            {
                var remainingOpenInput = GetRemainingOpenInput(input);
                if (remainingOpenInput != null)
                {
                    var score = 0L;
                    foreach (var @char in remainingOpenInput)
                    {
                        score *= 5;
                        score += _pointsB[@char];
                    }

                    scores.Add(score);
                }
            }

            scores.Sort();

            return scores.ElementAt(scores.Count / 2);
        }

        private char? GetCorruptedChar(string input)
        {
            var stack = new Stack<char>();
            foreach (var @char in input)
            {
                var charSet = _charSets.SingleOrDefault(x => x.close == @char);
                if (charSet.close == default(char))
                {
                    stack.Push(@char);
                    continue;
                }

                if (charSet.open == stack.Pop())
                {
                    continue;
                }

                return charSet.close;
            }

            return null;
        }

        private Stack<char> GetRemainingOpenInput(string input)
        {
            var stack = new Stack<char>();
            foreach (var @char in input)
            {
                var charSet = _charSets.SingleOrDefault(x => x.close == @char);
                if (charSet.close == default(char))
                {
                    stack.Push(@char);
                    continue;
                }

                if (charSet.open == stack.Pop())
                {
                    continue;
                }

                return null;
            }

            return stack;
        }
    }
}
