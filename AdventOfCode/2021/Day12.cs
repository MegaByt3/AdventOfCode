using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day12
{
    // https://adventofcode.com/2021/day/12
    public class Day12 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "12";

        private Dictionary<string, List<string>> _paths = new Dictionary<string, List<string>>();

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        public Day12(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day);

            foreach (var input in rawInputs)
            {
                var path = input.Split("-");
                AddPath(path[0], path[1]);
                AddPath(path[1], path[0]);
            }

            Logger.LogDebug($"# caves: {_paths.Count - 1}");
            Logger.LogDebug($"# paths: {rawInputs.Count()}");
            Logger.LogDebug($"# valid moves: {_paths.SelectMany(x => x.Value).Count()}");
        }

        // Part1 result: 5457
        public override object SolvePart1()
        {
            var nrOfPaths = 0;

            TraverseCaves("start", new List<string>(), ref nrOfPaths);

            return nrOfPaths;
        }

        // Part2 result: 128506
        public override object SolvePart2()
        {
            var nrOfPaths = 0;

            TraverseCavesPart2("start", new List<string>(), false, ref nrOfPaths);

            return nrOfPaths;
        }

        private void AddPath(string origin, string destination)
        {
            if (origin == "end" || destination == "start") return;

            if (_paths.ContainsKey(origin))
            {
                _paths[origin].Add(destination);
            }
            else
            {
                _paths.Add(origin, new List<string> { destination });
            }
        }

        private void TraverseCaves(string originCave, List<string> paths, ref int nrOfPaths)
        {
            if (originCave.All(char.IsLower) && paths.Any(x => x == originCave))
            {
                return;
            }

            paths.Add(originCave);

            if (originCave == "end")
            {
                nrOfPaths++;
                //Logger.LogDebug($"path: {string.Join(",", paths)}");
                return;
            }

            foreach (var destinationCave in _paths[originCave])
            {
                TraverseCaves(destinationCave, paths.ToList(), ref nrOfPaths);
            }
        }

        private void TraverseCavesPart2(string originCave, List<string> paths, bool smallCaveVisitedTwice, ref int nrOfPaths)
        {
            if (originCave.All(char.IsLower) && paths.Any(x => x == originCave))
            {
                if (smallCaveVisitedTwice) return;
                smallCaveVisitedTwice = true;
            }

            paths.Add(originCave);

            if (originCave == "end")
            {
                nrOfPaths++;
                //Logger.LogDebug($"path: {string.Join(",", paths)}");
                return;
            }

            foreach (var destinationCave in _paths[originCave])
            {
                TraverseCavesPart2(destinationCave, paths.ToList(), smallCaveVisitedTwice, ref nrOfPaths);
            }
        }
    }
}
