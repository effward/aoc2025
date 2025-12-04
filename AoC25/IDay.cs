namespace AoC25;

public interface IDay
{
    long SolvePart2(string input);
    long SolvePart1(string input);
    string Description { get; }
}