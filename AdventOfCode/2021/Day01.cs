﻿using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/1
    public class Day01 : BaseDay<int>
    {
        protected override string Year => "2021";
        protected override string Day => "01";
        protected override int[] Inputs { get; set; }
        
        private const int WindowRange = 3;

        public IInputManager InputManager { get; }

        public Day01(IInputManager inputManager)
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
            var result = 0;

            for (int i = 0; i < Inputs.Length - 1; i++)
            {
                if (Inputs[i + 1] > Inputs[i]) result++;
            }

            return result;
        }

        public override object SolvePart2()
        {
            var result = 0;
            var previousWindowSum = Inputs[0..WindowRange].Sum();

            for (int i = 0; i < Inputs.Length - WindowRange; i++)
            {
                var nextWindowSum = Inputs[(i + 1)..(i + 1 + WindowRange)].Sum();
                if (nextWindowSum > previousWindowSum) result++;
                previousWindowSum = nextWindowSum;
            }

            return result;
        }
    }
}
