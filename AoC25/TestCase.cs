namespace AoC25;

public readonly struct TestCase(IDay day, string input, long part1Output, long part2Output)
{
    public IDay Day { get; } = day;
    public string Input { get; } = input;
    public long Part1Output { get; } = part1Output;
    public long Part2Output { get; } =  part2Output;
}