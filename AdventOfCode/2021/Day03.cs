using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;
using AdventOfCode.Helpers;
using Microsoft.Extensions.Logging;

namespace AdventOfCode2021
{
    // https://adventofcode.com/2021/day/3
    public class Day03 : BaseDay
    {
        protected override string Year => "2021";
        protected override string Day => "03";

        public IInputManager InputManager { get; }
        public ILogger Logger { get; }

        private List<BitArray> Inputs { get; set; }
        private int _inputLength;

        public Day03(IInputManager inputManager, ILogger logger)
        {
            InputManager = inputManager;
            Logger = logger;
            Init(Year, Day);
        }

        private void Init(string year, string day)
        {
            var rawInputs = InputManager.GetInputs<string>(year, day);
            Inputs = rawInputs.Select(x => new BitArray(x.Select(c => c == '1').ToArray())).ToList();
            _inputLength = Inputs[0].Length;
        }

        // Part1 result: 2967914
        public override object SolvePart1()
        {
            var mostCommons = new short[_inputLength];

            for (int i = 0; i < Inputs.Count - 1; i += 2)
            {
                CalculateConsecutives(true, i, ref mostCommons);
                CalculateConsecutives(false, i, ref mostCommons);
            }

            var gammaRateBits = new BitArray(_inputLength);
            for (var i = 0; i < mostCommons.Length; i++)
            {
                if (mostCommons[i] > 0)
                {
                    // If the value on a certain position is positive, it means there are more 1's then 0's
                    gammaRateBits[i] = true;
                }
            }

            var gammaRate = gammaRateBits.ConvertToNumber();

            Logger.LogDebug($"Gamma rate: {gammaRate} - {Convert.ToString(gammaRate, 2)}");
            var epsilonRate = (~gammaRate) & (uint)(Math.Pow(2, _inputLength) - 1);
            Logger.LogDebug($"Epsilon rate: {epsilonRate} - {Convert.ToString(epsilonRate, 2)}");

            return gammaRate * epsilonRate;
        }

        // Part2 result: 7041258
        public override object SolvePart2()
        {
            var oxygenGeneratorRating = Calculate(Inputs, true).ConvertToNumber();
            var co2ScrubberRating = Calculate(Inputs, false).ConvertToNumber();

            Logger.LogDebug($"Oxygen Generator Rating: {oxygenGeneratorRating} - {Convert.ToString(oxygenGeneratorRating, 2)}");
            Logger.LogDebug($"CO2 Scrubber Rating: {co2ScrubberRating} - {Convert.ToString(co2ScrubberRating, 2)}");

            return oxygenGeneratorRating * co2ScrubberRating;
        }

        private void CalculateConsecutives(bool calculateConsecutiveOnes, int inputIndex, ref short[] mostCommons)
        {
            var bits = new BitArray(Inputs[inputIndex]);

            // 1 followed by 0 or other way around can be ignored
            if (calculateConsecutiveOnes)
            {
                // Do AND operation between 2 elements, this indicates position of 2 consecutive 1's
                bits.And(Inputs[inputIndex + 1]);
            }
            else
            {
                // Do NOR operation between 2 elements, this indicates position of 2 consecutive 0's
                bits.Or(Inputs[inputIndex + 1]);
                bits.Not();
            }

            for (int pos = 0; pos < bits.Length; pos++)
            {
                if (bits[pos] == true)
                {
                    if (calculateConsecutiveOnes)
                    {
                        // If 2 consecutive 1's are found on a certain position, increase
                        mostCommons[pos]++;
                    }
                    else
                    {
                        // If 2 consecutive 0's are found on a certain position, decrease
                        mostCommons[pos]--;
                    }
                }
            }
        }

        private BitArray Calculate(List<BitArray> inputs, bool keepMostCommon, int level = 0)
        {
            if (inputs.Count == 1) return inputs[0];
            var mostCommons = 0;

            for (int i = 0; i < inputs.Count - 1; i += 2)
            {
                var andBits = new BitArray(inputs[i]);
                andBits.And(inputs[i + 1]);

                if (andBits[level] == true)
                {
                    mostCommons++;
                }

                var orBits = new BitArray(inputs[i]);
                orBits.Or(inputs[i + 1]);
                orBits.Not();

                if (orBits[level] == true)
                {
                    mostCommons--;
                }
            }

            var remaining = inputs.Where(x => x[level] == (keepMostCommon ? (mostCommons >= 0) : (mostCommons < 0))).ToList();

            return Calculate(remaining, keepMostCommon, level + 1);
        }
    }
}
