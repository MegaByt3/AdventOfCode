namespace AdventOfCode
{
    public abstract class BaseDay<T> : IDay
    {
        protected abstract string Year { get; }
        protected abstract string Day { get; }
        protected abstract T[] Inputs { get; set; }

        abstract public object SolvePart1();
        abstract public object SolvePart2();
    }
}
