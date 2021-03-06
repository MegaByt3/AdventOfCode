using System;
using System.Diagnostics;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AdventOfCode
{
    public class Startup
    {
        public ILogger Logger { get; }
        public IConfiguration Configuration { get; }
        public IInputManager InputManager { get; }

        public Startup(ILogger<Startup> logger, IConfiguration configuration, IInputManager inputManager)
        {
            Logger = logger;
            Configuration = configuration;
            InputManager = inputManager;
        }

        public void Execute()
        {
            var year = Configuration.GetValue<string>("PuzzleSelection:Year");
            var day = Configuration.GetValue<string>("PuzzleSelection:Day");
            var part1 = Configuration.GetValue<bool>("PuzzleSelection:Parts:Part1");
            var part2 = Configuration.GetValue<bool>("PuzzleSelection:Parts:Part2");

            var type = Type.GetType($"AdventOfCode{year}.Day{day}.Day{day}");

            Console.WriteLine($"Solving puzzle for year {year} day {day}:");

            var sw = new Stopwatch();
            sw.Start();

            if (part1)
            {
                var puzzle = (IDay)Activator.CreateInstance(type, InputManager, Logger);

                Console.WriteLine($"Compute time initializing: {sw.Elapsed:c}");
                sw.Restart();

                var resultPart1 = puzzle.SolvePart1();
                Console.WriteLine($"Part1 result: {resultPart1}");
                Console.WriteLine($"Compute time part 1: {sw.Elapsed:c}");
                sw.Restart();
            }

            if (part2)
            {
                var puzzle = (IDay)Activator.CreateInstance(type, InputManager, Logger);

                Console.WriteLine($"Compute time initializing: {sw.Elapsed:c}");
                sw.Restart();

                var resultPart2 = puzzle.SolvePart2();
                Console.WriteLine($"Part2 result: {resultPart2}");
                Console.WriteLine($"Compute time part 2: {sw.Elapsed:c}");
            }
            sw.Stop();
        }
    }
}