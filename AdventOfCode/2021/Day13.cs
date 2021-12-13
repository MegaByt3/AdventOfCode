using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day13
{
    // https://adventofcode.com/2021/day/13
    public class Day13 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "13";

        private bool[][] _dots;
        private List<(string direction, int pos)> _foldInstructions;

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        public Day13(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            _dots = null;
            _foldInstructions = new List<(string, int)>();
            var rawInputs = InputManager.GetInputs<string>(year, day);

            var dots = new List<(int x, int y)>();
            var regex = new Regex(@"(\w)=(\d+)");

            foreach (var input in rawInputs)
            {
                if (string.IsNullOrEmpty(input)) continue;
                if (input.StartsWith("fold"))
                {
                    var match = regex.Matches(input)[0];
                    _foldInstructions.Add((match.Groups[1].Value, int.Parse(match.Groups[2].Value)));
                    continue;
                }

                var coords = input.Split(",").Select(x => int.Parse(x)).ToList();
                dots.Add((coords[0], coords[1]));
            }

            _dots = new bool[dots.Max(x => x.x) + 1][];
            for (int i = 0; i < _dots.Length; i++)
            {
                _dots[i] = new bool[dots.Max(x => x.y) + 1];
            }

            foreach (var dot in dots)
            {
                _dots[dot.x][dot.y] = true;
            }

            Logger.LogDebug($"# dots: {_dots.Count(x => x)}");
            Logger.LogDebug($"# fold instructions: {_foldInstructions.Count}");
        }

        // Part1 result: 755
        public override object SolvePart1()
        {
            Fold(_foldInstructions.First());
            return _dots.Count(x => x);
        }

        // Part2 result: BLKJRBAG
        public override object SolvePart2()
        {
            Init("2021", "13");
            foreach (var foldInstruction in _foldInstructions)
            {
                Fold(foldInstruction);
            }

            return _dots.Print(x => x ? "#" : ".");
        }

        private void Fold((string direction, int pos) foldInstruction)
        {
            if (foldInstruction.direction == "x")
            {
                for (int i = foldInstruction.pos + 1; i < _dots.Length; i++)
                {
                    for (int j = 0; j < _dots[i].Length; j++)
                    {
                        if (_dots[i][j]) _dots[i - (2 * (i - foldInstruction.pos))][j] = true;
                    }
                }

                _dots = _dots.Take(foldInstruction.pos).ToArray();
            }
            else
            {
                for (int i = 0; i < _dots.Length; i++)
                {
                    for (int j = foldInstruction.pos + 1; j < _dots[i].Length; j++)
                    {
                        if (_dots[i][j]) _dots[i][j - (2 * (j - foldInstruction.pos))] = true;
                    }

                    _dots[i] = _dots[i].Take(foldInstruction.pos).ToArray();
                }
            }
        }
    }
}
