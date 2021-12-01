using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/1
    public class Day01 : BaseDay<int>
    {
        private const string Year = "2021";
        private const string Day = "01";
        private const int WindowRange = 3;
        private int[] _inputs;

        public IInputManager InputManager { get; }

        public Day01(IInputManager inputManager)
        {
            InputManager = inputManager;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            _inputs = InputManager.GetInputs<int>(year, day).ToArray();
        }

        public override object SolvePart1()
        {
            var result = 0;

            for (int i = 0; i < _inputs.Length - 1; i++)
            {
                if (_inputs[i + 1] > _inputs[i]) result++;
            }

            return result;
        }

        public override object SolvePart2()
        {
            var result = 0;
            var previousWindowSum = _inputs[0..WindowRange].Sum();

            for (int i = 0; i < _inputs.Length - WindowRange; i++)
            {
                var nextWindowSum = _inputs[(i + 1)..(i + 1 + WindowRange)].Sum();
                if (nextWindowSum > previousWindowSum) result++;
                previousWindowSum = nextWindowSum;
            }

            return result;
        }
    }
}
