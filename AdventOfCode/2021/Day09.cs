using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day09
{
    // https://adventofcode.com/2021/day/9
    public class Day09 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "09";

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        private int[][] _inputs;
        private int _horizontalSize;
        private int _verticalSize;

        public Day09(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputNumbers(year, day);
            _inputs = rawInputs;

            _horizontalSize = rawInputs[0].Length;
            _verticalSize = rawInputs.Length;

            Logger.LogDebug($"# inputs: [{_horizontalSize}][{_verticalSize}] => {_horizontalSize * _verticalSize}");
        }

        // Part1 result: 500
        public override object SolvePart1()
        {
            var result = 0;

            for (int i = 0; i < _verticalSize; i++)
            {
                for (int j = 0; j < _horizontalSize; j++)
                {
                    var localLowPoint = LocalLowPoint((j, i));
                    if (localLowPoint.isLocalLowPoint)
                    {
                        result += localLowPoint.height + 1;
                    }
                }
            }

            return result;
        }

        // Part2 result: 970200
        public override object SolvePart2()
        {
            var result = new List<int>() { 0, 0, 0 };

            for (int i = 0; i < _verticalSize; i++)
            {
                for (int j = 0; j < _horizontalSize; j++)
                {
                    var localLowPoint = LocalLowPoint((j, i));
                    if (localLowPoint.isLocalLowPoint)
                    {
                        var oldPositions = new Dictionary<(int horizontal, int vertical), int>();
                        var size = 1;
                        oldPositions.Add((j, i), 0);
                        CalculateBasin((j, i), ref oldPositions, ref size);
                        result.Add(size);
                        result.Remove(result.Min());
                    }
                }
            }

            return result.Aggregate((a, x) => a * x);
        }

        private (bool isLocalLowPoint, int height) LocalLowPoint((int horizontal, int vertical) position)
        {
            var currentHeight = _inputs[position.vertical][position.horizontal];
            var adjacents = new List<((int horizontal, int vertical) position, int height)>();

            if (position.vertical != 0) adjacents.Add(((position.horizontal, position.vertical - 1), _inputs[position.vertical - 1][position.horizontal])); // up
            if (position.vertical != _verticalSize - 1) adjacents.Add(((position.horizontal, position.vertical + 1), _inputs[position.vertical + 1][position.horizontal])); // down
            if (position.horizontal != 0) adjacents.Add(((position.horizontal - 1, position.vertical), _inputs[position.vertical][position.horizontal - 1])); // left
            if (position.horizontal != _horizontalSize - 1) adjacents.Add(((position.horizontal + 1, position.vertical), _inputs[position.vertical][position.horizontal + 1])); // right

            var lowest = adjacents.First(x => x.height == adjacents.Select(x => x.height).Min());

            return (lowest.height > currentHeight, currentHeight);
        }

        //private ((int horizontal, int vertical) position, int height) CalculateLocalLowPoint((int horizontal, int vertical) position)
        //{
        //    var currentHeight = _inputs[position.vertical][position.horizontal];
        //    var adjacents = new List<((int horizontal, int vertical) position, int height)>();

        //    if (position.vertical != 0) adjacents.Add(((position.horizontal, position.vertical - 1), _inputs[position.vertical - 1][position.horizontal])); // up
        //    if (position.vertical != _verticalSize - 1) adjacents.Add(((position.horizontal, position.vertical + 1), _inputs[position.vertical + 1][position.horizontal])); // down
        //    if (position.horizontal != 0) adjacents.Add(((position.horizontal - 1, position.vertical), _inputs[position.vertical][position.horizontal - 1])); // left
        //    if (position.horizontal != _horizontalSize - 1) adjacents.Add(((position.horizontal + 1, position.vertical), _inputs[position.vertical][position.horizontal + 1])); // right

        //    var lowest = adjacents.First(x => x.height == adjacents.Select(x => x.height).Min());

        //    if (lowest.height < currentHeight)
        //    {
        //        return CalculateLocalLowPoint(lowest.position);
        //    }

        //    return (position, currentHeight);
        //}

        private void CalculateBasin((int horizontal, int vertical) position, ref Dictionary<(int horizontal, int vertical), int> oldPositions, ref int size)
        {
            var adjacents = new List<((int horizontal, int vertical) position, int height)>();

            if (position.vertical != 0) adjacents.Add(((position.horizontal, position.vertical - 1), _inputs[position.vertical - 1][position.horizontal])); // up
            if (position.vertical != _verticalSize - 1) adjacents.Add(((position.horizontal, position.vertical + 1), _inputs[position.vertical + 1][position.horizontal])); // down
            if (position.horizontal != 0) adjacents.Add(((position.horizontal - 1, position.vertical), _inputs[position.vertical][position.horizontal - 1])); // left
            if (position.horizontal != _horizontalSize - 1) adjacents.Add(((position.horizontal + 1, position.vertical), _inputs[position.vertical][position.horizontal + 1])); // right

            foreach(var adjacent in adjacents.Where(x => x.height != 9))
            {
                if (!oldPositions.ContainsKey(adjacent.position))
                {
                    oldPositions.Add(adjacent.position, 0);
                    size++;
                    CalculateBasin(adjacent.position, ref oldPositions, ref size);
                }
            }
        }
    }
}
