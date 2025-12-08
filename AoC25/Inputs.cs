namespace AoC25;

public struct TestCase(IDay day, string input, long part1Output, long part2Output)
{
    public IDay Day { get; } = day;
    public string Input { get; } = input;
    public long Part1Output { get; } = part1Output;
    public long Part2Output { get; } =  part2Output;
}

public static class Inputs
{
    // Day 1
    public static int Day1Part1Output { get; set; } = 3;
    public static int Day1Part2Output { get; set; } = 6;
    public static string Day1Input { get; set; } = """

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

    // Day 2
    public static long Day2Part1Output { get; set; } = 1227775554;
    public static long Day2Part2Output { get; set; } = 4174379265;
    public static string Day2Input { get; set; } = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
    
    // Day 3
    public static long Day3Part1Output { get; set; } = 357;
    public static long Day3Part2Output { get; set; } = 3121910778619;
    public static string Day3Input { get; set; } = "987654321111111\n811111111111119\n234234234234278\n818181911112111";
    
    // Day 4
    public static long Day4Part2Output { get; set; } = 43;
    public static long Day4Part1Output { get; set; } = 13;
    public static string Day4Input { get; set; } = """
                                                   ..@@.@@@@.
                                                   @@@.@.@.@@
                                                   @@@@@.@.@@
                                                   @.@@@@..@.
                                                   @@.@@@@.@@
                                                   .@@@@@@@.@
                                                   .@.@.@.@@@
                                                   @.@@@.@@@@
                                                   .@@@@@@@@.
                                                   @.@.@@@.@.
                                                   """;
    
    // Day 5
    public static long Day5Part1Output { get; set; } = 3;
    public static long Day5Part2Output { get; set; } = 14;

    public static string Day5Input { get; set; } = """
                                                   3-5
                                                   10-14
                                                   16-20
                                                   12-18

                                                   1
                                                   5
                                                   8
                                                   11
                                                   17
                                                   32
                                                   """;
    
    // Day 6
    public static long Day6Part1Output { get; set; } = 4277556;
    public static long Day6Part2Output { get; set; } = 3263827;
    public static string Day6Input { get; set; } = """
                                                   123 328  51 64 
                                                    45 64  387 23 
                                                     6 98  215 314
                                                   *   +   *   +  
                                                   """;
    
    // Day 7
    public static long Day7Part1Output { get; set; } = 21;
    public static long Day7Part2Output { get; set; } = 40;

    public static string Day7Input { get; set; } = """
                                                   .......S.......
                                                   ...............
                                                   .......^.......
                                                   ...............
                                                   ......^.^......
                                                   ...............
                                                   .....^.^.^.....
                                                   ...............
                                                   ....^.^...^....
                                                   ...............
                                                   ...^.^...^.^...
                                                   ...............
                                                   ..^...^.....^..
                                                   ...............
                                                   .^.^.^.^.^...^.
                                                   ...............
                                                   """;
    
    // Day 8
    public static long Day8Part1Output { get; set; } = 40;
    public static long Day8Part2Output { get; set; } = 25272;

    public static string Day8Input { get; set; } = """
                                                   162,817,812
                                                   57,618,57
                                                   906,360,560
                                                   592,479,940
                                                   352,342,300
                                                   466,668,158
                                                   542,29,236
                                                   431,825,988
                                                   739,650,466
                                                   52,470,668
                                                   216,146,977
                                                   819,987,18
                                                   117,168,530
                                                   805,96,715
                                                   346,949,466
                                                   970,615,88
                                                   941,993,340
                                                   862,61,35
                                                   984,92,344
                                                   425,690,689
                                                   """;
    
    public static List<TestCase> BuildTestCases() =>
    [
        new (new Day1(), Day1Input, Day1Part1Output, Day1Part2Output),
        new (new Day2(), Day2Input, Day2Part1Output, Day2Part2Output),
        new (new Day3(), Day3Input, Day3Part1Output, Day3Part2Output),
        new (new Day4(), Day4Input, Day4Part1Output, Day4Part2Output),
        new (new Day5(), Day5Input, Day5Part1Output, Day5Part2Output),
        new (new Day6(), Day6Input, Day6Part1Output, Day6Part2Output),
        new (new Day7(), Day7Input, Day7Part1Output, Day7Part2Output),
        new (new Day8(), Day8Input, Day8Part1Output, Day8Part2Output),
    ];
}