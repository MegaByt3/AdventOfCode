using System;
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
            var rawInputs = InputManager.GetInputNumbers(year, day);

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
