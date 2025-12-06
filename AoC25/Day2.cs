namespace AoC25;

public class Day2() : DayBase(2)
{
    public override long SolvePart2(string input)
    {
        return SumInvalidIdsV3(input);
    }
    
    public override long SolvePart1(string input)
    {
        return SumInvalidIdsV2(input);
    }
    
    private record struct Range(long Lower, long Upper);

    private static (List<Range>, int) BuildRanges(string[] parts)
    {
        var ranges = new List<Range>();
        foreach (var part in parts)
        {
            var bounds = part.Split('-');
            if (bounds.Length != 2)
            {
                throw new ArgumentException("Invalid input format");
            }
            var lowerStr = bounds[0];
            var upperStr = bounds[1];
            
            var lower = long.Parse(lowerStr);
            var upper = long.Parse(upperStr);
            
            ranges.Add(new Range(lower, upper));
        }

        return (ranges, 0);
    }
    
    private static long SumInvalidIdsV3(string input)
    {
        var (_, ranges, _) = SplitAndBuild(input, BuildRanges, ',');

        long total = 0;
        var set = new HashSet<long>();
        foreach (var range in ranges)
        {
            for (var digits = 1; digits <= NumDigits(range.Upper) / 2; digits++)
            {
                for (var num = (int)Math.Pow(10, digits - 1); num < (int)Math.Pow(10, digits); num++)
                {
                    var exp = NumDigits(num);
                    long target = num + num * (long)Math.Pow(10, exp);

                    if (target > range.Upper)
                    {
                        break;
                    }
                    
                    var reps = 2;
                    while (target <= range.Upper)
                    {
                        if (target >= range.Lower)
                        {
                            if (!set.Contains(target))
                            {
                                total += target;
                                set.Add(target);
                            }
                        }

                        target += num * (long)Math.Pow(10, exp * reps);
                        reps++;
                    }
                }
            }
        }

        return total;
    }
    
    private static long SumInvalidIdsV2(string input)
    {
        return SplitLoopAndAdd(input, AddInvalid, ',');

        long AddInvalid(string part)
        {
            var bounds = part.Split('-');
            if (bounds.Length != 2)
            {
                throw new ArgumentException("Invalid input format", nameof(input));
            }
            var lowerStr = bounds[0];
            var upperStr = bounds[1];
            
            var lower = long.Parse(lowerStr);
            var upper = long.Parse(upperStr);

            var halfLower = lowerStr.Length == 1 ? lower : long.Parse(lowerStr[..(lowerStr.Length / 2)]);
            var halfUpper = upperStr.Length == 1 ? upper : long.Parse(upperStr[..(upperStr.Length / 2)]);

            long total = 0;
            for (var num = halfLower; num <= halfUpper; num++)
            {
                var numStr = num.ToString() + num.ToString();
                var target = long.Parse(numStr);
                if (target >= lower && target <= upper)
                {
                    total += target;
                }

                if (target > upper)
                {
                    break;
                }
            }

            return total;
        }
    }
    
    private static int NumDigits(long num)
    {
        return num switch
        {
            < 10L => 1,
            < 100L => 2,
            < 1000L => 3,
            < 10000L => 4,
            < 100000L => 5,
            < 1000000L => 6,
            < 10000000L => 7,
            < 100000000L => 8,
            < 1000000000L => 9,
            < 10000000000L => 10,
            < 100000000000L => 11,
            < 1000000000000L => 12,
            < 10000000000000L => 13,
            < 100000000000000L => 14,
            < 1000000000000000L => 15,
            < 10000000000000000L => 16,
            < 100000000000000000L => 17,
            < 1000000000000000000L => 18,
            _ => 19
        };
    }
}