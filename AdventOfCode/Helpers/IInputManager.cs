using System.Collections.Generic;

namespace AdventOfCode.Helpers
{
    public interface IInputManager
    {
        List<T> GetInputs<T>(string year, string day);
    }
}
