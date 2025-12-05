// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using AoC25;

Input.OverrideInputs();

foreach (var testCase in Inputs.BuildTestCases())
{
    Console.WriteLine($"### {testCase.Day.Description} ###");
    var stopwatch = Stopwatch.StartNew();
    var part1 = testCase.Day.SolvePart1(testCase.Input);
    Console.WriteLine($"Part 1: {part1} in {stopwatch.ElapsedMilliseconds} ms");
    
    stopwatch.Restart();
    var part2 = testCase.Day.SolvePart2(testCase.Input);
    Console.WriteLine($"Part 2: {part2}  in {stopwatch.ElapsedMilliseconds} ms");
    Console.WriteLine();
}