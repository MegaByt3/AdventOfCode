using System;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day06
{
    // https://adventofcode.com/2021/day/7
    public class Day07 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "07";

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }


        public Day07(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day).SelectMany(x => x.Split(",")).Select(x => int.Parse(x)).ToList();

            //Logger.LogDebug($"# fish: {_fish.Count}");
        }

        // Part1 result: ?
        public override object SolvePart1()
        {
            throw new NotImplementedException();
        }

        // Part2 result: ?
        public override object SolvePart2()
        {
            throw new NotImplementedException();
        }
    }
}
