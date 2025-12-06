namespace AoC25;

public abstract class DayBase(int dayNumber) : IDay
{
    public string Description => $"Day {dayNumber}";

    public abstract long SolvePart2(string input);
    public abstract long SolvePart1(string input);

    protected static string[] SplitLines(string input, char separator = '\n')
    {
        return input.Trim().Split(separator);
    }

    protected static long SplitLoopAndAdd(string input, Func<string, long> add, char separator = '\n')
    {
        var parts = SplitLines(input, separator);
        return LoopAndAdd(parts, add);
    }

    protected static (string[], T, int) SplitAndBuild<T>(string input, Func<string[], (T, int)> build, char separator = '\n')
    {
        var lines = SplitLines(input, separator);
        var (ds, idx) = build(lines);
        return (lines, ds, idx);
    }

    protected static long SplitBuildLoopAndAdd<T>(string input, Func<string[], (T, int)> build, Func<string, T, long> add)
    {
        var (lines, ds, idx) = SplitAndBuild(input, build);

        return LoopAndAdd(lines, line => add(line, ds), idx);
    }

    protected static long LoopAndAdd<T>(IList<T> list, Func<T, long> add, int idx = 0)
    {
        long total = 0;
        for (var i = idx; i < list.Count; i++)
        {
            var line = list[i];
            total += add(line);
        }
        return total;
    }
}