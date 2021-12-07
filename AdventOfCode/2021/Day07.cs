using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day07
{
    // https://adventofcode.com/2021/day/7
    public class Day07 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "07";

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        private List<int> _input = new List<int>();
        private Dictionary<int, int> _fuelCosts = new Dictionary<int, int>();

        public Day07(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day).SelectMany(x => x.Split(",")).Select(x => int.Parse(x));

            _input = rawInputs.ToList();

            Logger.LogDebug($"# numbers: {_input.Count}");
            Logger.LogDebug($"Min: {_input.Min()}");
            Logger.LogDebug($"Max: {_input.Max()}");
        }

        // Part1 result: 352707
        public override object SolvePart1()
        {
            InitFuelCosts((i) => i);

            return ExecuteCalculation();
        }

        // Part2 result: 95519693
        public override object SolvePart2()
        {
            InitFuelCosts((i) => CalculateFuelUseIncrease(i));
           
            return ExecuteCalculation();
        }

        private void InitFuelCosts(Func<int, int> calculation)
        {
            _fuelCosts.Clear();
            for (int i = 0; i < _input.Max(); i++)
            {
                _fuelCosts.Add(i, calculation(i));
            }
        }

        private (int, long) ExecuteCalculation()
        {
            var index1 = _input.Count / 2;
            var index2 = index1 + 1;
            //var index1 = 0;
            //var index2 = _input.Count - 1;
            var fuelCost1 = CalculateFuelUse(_input, index1);
            var fuelCost2 = CalculateFuelUse(_input, index2);

            return LineairSearchCheapestFuelCost((index1, fuelCost1), fuelCost2 < fuelCost1 ? 1 : -1);
            //return BinarySearchCheapestFuelCost((index1, fuelCost1), (index2, fuelCost2));
        }

        private (int, long) LineairSearchCheapestFuelCost((int index, long fuelCost) previousPosition, int offset)
        {
            (int index, long fuelCost) newPosition;

            newPosition = (previousPosition.index + offset, CalculateFuelUse(_input, previousPosition.index + offset));

            if (newPosition.fuelCost < previousPosition.fuelCost)
            {
                return LineairSearchCheapestFuelCost(newPosition, offset);
            }
            else
            {
                return previousPosition;
            }
        }

        //private (int, long) BinarySearchCheapestFuelCost((int index, long fuelCost) position1, (int index, long fuelCost) position2)
        //{
        //    if (position1.fuelCost < position2.fuelCost)
        //    {
        //        position2.index = (int)Math.Ceiling((position1.index + position2.index) / 2.0);
        //        position2.fuelCost = CalculateFuelUse(_input, position2.index);
        //    }
        //    else
        //    {
        //        position1.index = (int)Math.Floor((position1.index + position2.index) / 2.0);
        //        position1.fuelCost = CalculateFuelUse(_input, position1.index);
        //    }

        //    if (Math.Abs(position1.index - position2.index) == 1)
        //    {
        //        return position1.fuelCost < position2.fuelCost ? position1 : position2;
        //    }

        //    return BinarySearchCheapestFuelCost(position1, position2);
        //}

        private int CalculateFuelUse(List<int> inputs, int position)
        {
            var fuelCost = 0;
            foreach (var input in inputs)
            {
                fuelCost += _fuelCosts[Math.Abs(position - input)];
            }

            return fuelCost;
        }

        private int CalculateFuelUseIncrease(int distance)
        {
            var fuelCost = 0;

            while (distance != 0)
            {
                fuelCost += distance;
                distance--;
            }

            return fuelCost;
        }
    }
}
