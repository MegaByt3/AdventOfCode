using System;
using AdventOfCode;
using AdventOfCode.Helpers;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/1
    public class Day02 : BaseDay<int>
    {
        protected override string Year => "2021";
        protected override string Day => "02";
        protected override int[] Inputs { get; set; }

        public IInputManager InputManager { get; }

        public Day02(IInputManager inputManager)
        {
            InputManager = inputManager;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            Inputs = InputManager.GetInputs<int>(year, day).ToArray();
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
