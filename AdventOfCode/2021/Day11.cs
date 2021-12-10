using System;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day11
{
    // https://adventofcode.com/2021/day/11
    public class Day11 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "11";

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        public Day11(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day);

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
