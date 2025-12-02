namespace AoC25.Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        // Input.OverrideInputs();
    }

    [Test]
    public void Day1Part1Test()
    {
        var day1Part1Output = Day1.SolvePart1(Inputs.Day1Input);
        Assert.That(day1Part1Output, Is.EqualTo(Inputs.Day1Part1Output));
        Assert.Warn($"Day1Part1 Output: {day1Part1Output}");
    }
    
    [Test]
    public void Day1Part2Test()
    {
        var day1Part2Output = Day1.SolvePart2(Inputs.Day1Input);
        Assert.That(day1Part2Output, Is.EqualTo(Inputs.Day1Part2Output));
        Assert.Warn($"Day1Part2 Output: {day1Part2Output}");
    }

    [Test]
    public void Day2Part1Test()
    {
        var day2Output = Day2.SolvePart1(Inputs.Day2Input);
        Assert.That(day2Output, Is.EqualTo(Inputs.Day2Part1Output));
        Assert.Warn($"Day2Part2 Output: {day2Output}");
    }
    
    [Test]
    public void Day2Part2Test()
    {
        var day2Output = Day2.SolvePart2(Inputs.Day2Input);
        Assert.That(day2Output, Is.EqualTo(Inputs.Day2Part2Output));
        Assert.Warn($"Day2Part2 Output: {day2Output}");
    }
}