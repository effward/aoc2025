// See https://aka.ms/new-console-template for more information

using AoC25;

// Input.OverrideInputs();

foreach (var testCase in Inputs.BuildTestCases())
{
    Console.WriteLine($"### {testCase.Day.Description} ###");
    Console.WriteLine($"Part 1: {testCase.Day.SolvePart1(testCase.Input)}");
    Console.WriteLine($"Part 2: {testCase.Day.SolvePart2(testCase.Input)}");
    Console.WriteLine();
}