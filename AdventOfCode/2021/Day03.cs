using System;
using System.Collections.Generic;
using AdventOfCode;
using AdventOfCode.Helpers;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/3
    public class Day03 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "03";
        protected List<(string direction, int distance)> Inputs { get; set; }

        public IInputManager InputManager { get; }

        public Day03(IInputManager inputManager)
        {
            InputManager = inputManager;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            throw new NotImplementedException();
        }

        public override object SolvePart1()
        {
            throw new NotImplementedException();
        }

        public override object SolvePart2()
        {
            throw new NotImplementedException();
        }
    }
}
