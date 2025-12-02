// See https://aka.ms/new-console-template for more information

using AoC25;

const string day1Input = """
                L68
                L30
                R48
                L5
                R60
                L55
                L1
                L99
                R14
                L82
                """;

Console.WriteLine("Day 1, Part 1: " + Day1.SolvePart1(day1Input));
Console.WriteLine("Day 1, Part 2: " + Day1.SolvePart2(day1Input));

const string day2Input = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";

Console.WriteLine("Day 2, Part 1: " + Day2.SolvePart1(day2Input));
Console.WriteLine("Day 2, Part 2: " + Day2.SolvePart2(day2Input));