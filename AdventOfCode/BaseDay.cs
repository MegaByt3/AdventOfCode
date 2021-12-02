namespace AdventOfCode
{
    public abstract class BaseDay : IDay
    {
        protected abstract string Year { get; }
        protected abstract string Day { get; }

        abstract public object SolvePart1();
        abstract public object SolvePart2();
    }
}
