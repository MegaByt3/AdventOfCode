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

        private int _steps = 100;

        private int[][] _inputs;
        private int _horizontalSize;
        private int _verticalSize;

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
            var rawInputs = InputManager.GetInputNumbers(year, day);
            _inputs = rawInputs;

            _horizontalSize = rawInputs[0].Length;
            _verticalSize = rawInputs.Length;

            Logger.LogDebug($"# inputs: [{_horizontalSize}][{_verticalSize}] => {_horizontalSize * _verticalSize}");
        }

        // Part1 result: 1785
        public override object SolvePart1()
        {
            var result = 0;

            while (_steps != 0)
            {
                IncrementArray();
                result += ExecuteFlashes();
                _steps--;
                //Logger.LogDebug($"After step {100 - _steps}:");
                //Logger.LogDebug($"{_inputs.Print()}");
            }

            return result;
        }

        // Part2 result: 354
        public override object SolvePart2()
        {
            Init(Year, Day);
            var step = 0;
            while (true)
            {
                step++;
                IncrementArray();
                var flashes = ExecuteFlashes();
                if (flashes == _horizontalSize * _verticalSize) return step;
                //Logger.LogDebug($"After step {step}:");
                //Logger.LogDebug($"{_inputs.Print()}");
            }

            throw new Exception("No simulanious flashes!");
        }

        private int ExecuteFlashes()
        {
            var flashes = 0;
            var alreadyFlashed = new bool[_verticalSize, _horizontalSize];

            for (int i = 0; i < _verticalSize; i++)
            {
                for (int j = 0; j < _horizontalSize; j++)
                {
                    if (_inputs[i][j] > 9 && !alreadyFlashed[i, j])
                    {
                        flashes++;
                        _inputs[i][j] = 0;
                        alreadyFlashed[i, j] = true;
                        IncreaseAdjacent((j, i), alreadyFlashed);
                        i = -1;
                        break;
                    }
                }
            }

            return flashes;
        }

        private void IncrementArray()
        {
            for (int i = 0; i < _verticalSize; i++)
            {
                for (int j = 0; j < _horizontalSize; j++)
                {
                    _inputs[i][j]++;
                }
            }
        }

        private void IncreaseAdjacent((int horizontal, int vertical) position, bool[,] alreadyFlashed)
        {
            if (position.vertical != 0 && position.horizontal != 0 && !alreadyFlashed[position.vertical - 1, position.horizontal - 1])                                   
                _inputs[position.vertical - 1][position.horizontal - 1]++;
            if (position.vertical != 0 && !alreadyFlashed[position.vertical - 1, position.horizontal])                                                               
                _inputs[position.vertical - 1][position.horizontal]++;
            if (position.vertical != 0 && position.horizontal != _horizontalSize - 1 && !alreadyFlashed[position.vertical - 1, position.horizontal + 1])                 
                _inputs[position.vertical - 1][position.horizontal + 1]++;
                                                                                                      
            if (position.horizontal != 0 && !alreadyFlashed[position.vertical, position.horizontal - 1])                                                             
                _inputs[position.vertical][position.horizontal - 1]++;
            if(!alreadyFlashed[position.vertical, position.horizontal])                                                                                          
                _inputs[position.vertical][position.horizontal]++;
            if (position.horizontal != _horizontalSize - 1 && !alreadyFlashed[position.vertical, position.horizontal + 1])                                           
                _inputs[position.vertical][position.horizontal + 1]++;

            if (position.vertical != _verticalSize - 1 && position.horizontal != 0 && !alreadyFlashed[position.vertical + 1, position.horizontal - 1])                   
                _inputs[position.vertical + 1][position.horizontal - 1]++;
            if (position.vertical != _verticalSize - 1 && !alreadyFlashed[position.vertical + 1, position.horizontal])                                               
                _inputs[position.vertical + 1][position.horizontal]++;
            if (position.vertical != _verticalSize - 1 && position.horizontal != _horizontalSize - 1 && !alreadyFlashed[position.vertical + 1, position.horizontal + 1]) 
                _inputs[position.vertical + 1][position.horizontal + 1]++;
        }
    }
}
