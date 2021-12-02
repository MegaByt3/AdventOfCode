using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/2
    public class Day02 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "02";
        protected List<(string direction, int distance)> Inputs { get; set; }

        public IInputManager InputManager { get; }

        public Day02(IInputManager inputManager)
        {
            InputManager = inputManager;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day);
            Inputs = rawInputs.Select(x =>
            {
                var splitInput = x.Split(" ");
                return (splitInput[0], int.Parse(splitInput[1]));
            }).ToList();
        }

        public override object SolvePart1()
        {
            var result = 0;
            var horizontalPosition = 0;
            var depth = 0;

            var directionGroups = Inputs.GroupBy(x => new
            {
                x.direction,
            })
            .Select(x => new
            {
                x.Key.direction,
                distances = x.Select(y => y.distance),
            });

            foreach (var directionGroup in directionGroups)
            {
                switch (directionGroup.direction)
                {
                    case "forward":
                        horizontalPosition += directionGroup.distances.Sum();
                        break;
                    case "down":
                        depth += directionGroup.distances.Sum();
                        break;
                    case "up":
                        depth -= directionGroup.distances.Sum();
                        break;
                    default:
                        throw new ArgumentException("Invalid direction");
                }
            }

            result = horizontalPosition * depth;
            return result;
        }

        public override object SolvePart2()
        {
            var result = 0;
            var horizontalPosition = 0;
            var depth = 0;
            var aim = 0;

            foreach (var input in Inputs)
            {
                switch (input.direction)
                {
                    case "forward":
                        horizontalPosition += input.distance;
                        depth += input.distance * aim;
                        break;
                    case "down":
                        aim += input.distance;
                        break;
                    case "up":
                        aim -= input.distance;
                        break;
                    default:
                        throw new ArgumentException("Invalid direction");
                }
            }

            result = horizontalPosition * depth;
            return result;
        }
    }
}
