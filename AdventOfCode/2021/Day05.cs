using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/5
    public class Day05 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "05";

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        private (int horizontalSize, int verticalSize) _diagramSize;
        private List<Line> _lines = new List<Line>();

        public Day05(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day);
            var regex = new Regex(@"(\d+)");

            foreach (var rawInput in rawInputs)
            {
                var matches = regex.Matches(rawInput);

                var x1 = int.Parse(matches[0].Value);
                var y1 = int.Parse(matches[1].Value);
                var x2 = int.Parse(matches[2].Value);
                var y2 = int.Parse(matches[3].Value);

                _diagramSize.horizontalSize = Math.Max(_diagramSize.horizontalSize, (Math.Max(x1, x2)));
                _diagramSize.verticalSize = Math.Max(_diagramSize.horizontalSize, (Math.Max(y1, y2)));

                var line = new Line
                {
                    Point1 = (x1, y1),
                    Point2 = (x2, y2),
                };
                line.CalculateSlopeAndOffset();

                _lines.Add(line);
            }

            Logger.LogDebug($"# lines: {_lines.Count}");
            Logger.LogDebug($"Diagram size: ({_diagramSize.horizontalSize}, {_diagramSize.verticalSize})");
        }

        // Part1 result: 5124
        public override object SolvePart1()
        {
            var overlappingPoints = 0;

            var linesSubset = _lines.Where(x => x.Point1.x == x.Point2.x || x.Point1.y == x.Point2.y).ToList();

            for (int x = 0; x <= _diagramSize.horizontalSize; x++)
            {
                for (int y = 0; y <= _diagramSize.verticalSize; y++)
                {
                    var nrOfLinesOnPoint = 0;
                    foreach(var line in linesSubset)
                    {
                        if (line.IsPointOnLine((x, y))) nrOfLinesOnPoint++;
                        if (nrOfLinesOnPoint >= 2)
                        {
                            overlappingPoints++;
                            break;
                        }
                    }
                }
            }

            return overlappingPoints;
        }

        // Part2 result: 19771
        public override object SolvePart2()
        {
            var overlappingPoints = 0;

            for (int x = 0; x <= _diagramSize.horizontalSize; x++)
            {
                for (int y = 0; y <= _diagramSize.verticalSize; y++)
                {
                    var nrOfLinesOnPoint = 0;
                    foreach (var line in _lines)
                    {
                        if (line.IsPointOnLine((x, y))) nrOfLinesOnPoint++;
                        if (nrOfLinesOnPoint >= 2)
                        {
                            overlappingPoints++;
                            break;
                        }
                    }
                }
            }

            return overlappingPoints;
        }
    }

    public class Line
    {
        public (int x, int y) Point1 { get; set; }
        public (int x, int y) Point2 { get; set; }
        public double Slope { get; set; }
        public double Offset { get; set; }
        public bool IsVerticalLine { get; private set; }

        public bool IsPointOnLine((int x, int y) point)
        {
            if (point.x < Math.Min(Point1.x, Point2.x) ||
                point.x > Math.Max(Point1.x, Point2.x) ||
                point.y < Math.Min(Point1.y, Point2.y) ||
                point.y > Math.Max(Point1.y, Point2.y)
                ) return false;

            if (IsVerticalLine)
            {
                return point.x == Point1.x;
            }

            return point.y == Slope * point.x + Offset;
        }

        public void CalculateSlopeAndOffset()
        {
            if (Point2.x - Point1.x == 0)
            {
                IsVerticalLine = true;
                return;
            }

            Slope = (Point2.y - Point1.y) / (Point2.x - Point1.x);
            Offset = Point1.y - (Slope * Point1.x);
        }
    }
}
