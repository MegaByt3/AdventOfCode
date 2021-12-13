using System;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day14
{
    // https://adventofcode.com/2021/day/14
    public class Day14 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "14";

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        public Day14(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day);

            //Logger.LogDebug($"# dots: {_dots.Count(x => x)}");
            //Logger.LogDebug($"# fold instructions: {_foldInstructions.Count}");
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
