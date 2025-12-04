namespace AoC25.Tests;

public class Tests
{
    private static readonly IEnumerable<TestCase> _TestCases;

    static Tests()
    {
        // Input.OverrideInputs();
        _TestCases = Inputs.BuildTestCases();
    }

    [TestCaseSource(nameof(_TestCases))]
    public void TestDays(TestCase testCase)
    {
        var part1Output = testCase.Day.SolvePart1(testCase.Input);
        Assert.That(part1Output, Is.EqualTo(testCase.Part1Output));
        
        var part2Output = testCase.Day.SolvePart2(testCase.Input);
        Assert.That(part2Output, Is.EqualTo(testCase.Part2Output));
        Assert.Warn($"{testCase.Day.Description} - Part1 Output: {part1Output}, Part2 Output: {part2Output}");
    }
}