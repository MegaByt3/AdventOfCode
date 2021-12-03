using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/4
    public class Day04 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "04";
        protected List<BitArray> Inputs { get; set; }

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        private int _inputLength;

        public Day04(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day);
            throw new NotImplementedException();
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
