using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day04
{
    // https://adventofcode.com/2021/day/4
    public class Day04 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "04";

        private (int horizontalSize, int verticalSize) _boardSize = (5, 5);

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        private List<int> _numbers = new List<int>();
        private Dictionary<Guid, List<List<int>>> _boards = new Dictionary<Guid, List<List<int>>>();

        public Day04(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day);

            _numbers = rawInputs.First().Split(",").Select(x => int.Parse(x)).ToList();

            var currentBoardArray = new int[_boardSize.horizontalSize, _boardSize.verticalSize];
            var currentBoard = new List<List<int>>();
            var verticalPos = 0;
            foreach (var line in rawInputs.Skip(2))
            {
                if (string.IsNullOrEmpty(line)) continue;

                var boardRow = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToArray();
                currentBoard.Add(boardRow.ToList());
                for (int i = 0; i < boardRow.Length; i++)
                {
                    currentBoardArray[i, verticalPos] = boardRow[i];
                }

                verticalPos++;

                if (verticalPos == _boardSize.verticalSize)
                {
                    for (int horizontalIndex = 0; horizontalIndex < _boardSize.horizontalSize; horizontalIndex++)
                    {
                        var boardColumn = new List<int>();
                        for (int verticalIndex = 0; verticalIndex < _boardSize.verticalSize; verticalIndex++)
                        {
                            boardColumn.Add(currentBoardArray[horizontalIndex, verticalIndex]);
                        }

                        currentBoard.Add(boardColumn);
                    }

                    _boards.Add(Guid.NewGuid(), currentBoard);
                    currentBoard = new List<List<int>>();
                    currentBoardArray = new int[_boardSize.horizontalSize, _boardSize.verticalSize];
                    verticalPos = 0;
                    continue;
                }
            }

            Logger.LogDebug($"# numbers: {_numbers.Count}");
            Logger.LogDebug($"# boards: {_boards.Count}");
        }

        // Part1 result: 31424
        public override object SolvePart1()
        {
            var (winningBoardId, lastNumber) = CalculateWinningBoard(true);
            return CalculateScore(winningBoardId, lastNumber);
        }

        // Part2 result: 23042
        public override object SolvePart2()
        {
            var (winningBoardId, lastNumber) = CalculateWinningBoard(false);
            return CalculateScore(winningBoardId, lastNumber);
        }

        private (Guid, int) CalculateWinningBoard(bool findFirstWinning)
        {
            bool winningBoardFound = false;
            foreach (var number in _numbers)
            {
                foreach (var board in _boards.ToList())
                {
                    foreach (var boardLine in board.Value)
                    {
                        if (boardLine.Contains(number))
                        {
                            boardLine.Remove(number);
                            if (boardLine.Count == 0)
                            {
                                winningBoardFound = true;
                            }
                            continue;
                        }
                    }

                    if (winningBoardFound)
                    {
                        if (findFirstWinning)
                        {
                            return (board.Key, number);
                        }
                        
                        if (_boards.Count == 1)
                        {
                            return (_boards.First().Key, number);
                        }
                        _boards.Remove(board.Key);
                        winningBoardFound = false;
                    }
                }
            }

            throw new Exception("No winning board!");
        }

        private int CalculateScore(Guid winningBoardId, int lastNumber)
        {
            var winningBoard = _boards[winningBoardId];

            var unmarkedSum = winningBoard.Select(x => x.Sum()).Sum() / 2;

            return unmarkedSum * lastNumber;
        }
    }
}
