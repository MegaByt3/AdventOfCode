using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode.Helpers
{
    public class InputManager : IInputManager
    {
        public List<T> GetInputs<T>(string year, string day)
        {
            var intputFileName = $"{year}//Inputs//Input-day{day}.txt";
            if (typeof(T) == typeof(int))
            {
                return File.ReadLines(intputFileName).Select(x => int.Parse(x)).Cast<T>().ToList();
            }
            else if (typeof(T) == typeof(string))
            {
                return File.ReadLines(intputFileName).Cast<T>().ToList();
            }

            throw new ArgumentException("Invalid type to load from file.");
        }
    }
}