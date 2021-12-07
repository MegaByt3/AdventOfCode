using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day08
{
    // https://adventofcode.com/2021/day/8
    public class Day08 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "08";

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        private List<int> _input = new List<int>();

        public Day08(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day).SelectMany(x => x.Split(",")).Select(x => int.Parse(x));

            //Logger.LogDebug($"# numbers: {_input.Count}");
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
