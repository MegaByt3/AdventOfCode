using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021.Day06
{
    // https://adventofcode.com/2021/day/6
    public class Day06 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "06";

        private int _simulationDays;

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        private List<int> _initialFishSpawnDeltas = new List<int>();
        private List<Fish> _fish = new List<Fish>();

        public Day06(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            _initialFishSpawnDeltas = InputManager.GetInputs<string>(year, day).SelectMany(x => x.Split(",")).Select(x => int.Parse(x)).ToList();

            _fish = _initialFishSpawnDeltas.Select(x => new Fish(x)).ToList();

            Logger.LogDebug($"# fish: {_fish.Count}");
        }

        // Part1 result: 371379
        public override object SolvePart1()
        {
            _simulationDays = 80;

            for (int i = 0; i < _simulationDays; i++)
            {
                foreach(var fish in _fish.ToList())
                {
                    fish.SimulateDay(out var newFish);
                    if (newFish != null)
                    {
                        _fish.Add(newFish);
                    }
                }
            }

            return _fish.Count();
        }

        // Part2 result: 1674303997472
        public override object SolvePart2()
        {
            _simulationDays = 256;

            var newFishPerDay = new long[_simulationDays];

            _initialFishSpawnDeltas.ForEach(x =>
            {
                for (int i = x; i < _simulationDays; i += 7)
                {
                    newFishPerDay[i]++;
                }
            });

            for (int i = 0; i < _simulationDays; i++)
            {
                var newFish = newFishPerDay[i];
                if (newFish == 0) continue;

                for (int j = i + 9; j < _simulationDays; j += 7)
                {
                    newFishPerDay[j] += newFish;
                }

            }

            return newFishPerDay.AsParallel().Sum() + _initialFishSpawnDeltas.Count;
        }
    }

    public class Fish
    {
        public int NewSpawnDelta { get; private set; }
        public bool IsNewFish { get; }

        public Fish(int spawnDelta)
        {
            NewSpawnDelta = spawnDelta;
        }

        public void SimulateDay(out Fish newFish)
        {
            newFish = null;

            if (NewSpawnDelta == 0)
            {
                NewSpawnDelta = 6;
                newFish = new Fish(8);
                return;
            }

            NewSpawnDelta--;
        }
    }
}
