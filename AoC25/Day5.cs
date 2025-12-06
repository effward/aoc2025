namespace AoC25;

public class Day5() : DayBase(5)
{
    public override long SolvePart2(string input)
    {
        long highest = 0;

        long AddAllFresh(FreshRange freshRange)
        {
            var start = Math.Max(freshRange.Start, highest);
            var diff = freshRange.End - start + 1;

            if (freshRange.End > highest)
            {
                highest = freshRange.End + 1;
            }
            
            return Math.Max(0, diff);
        }
        
        var (_, freshRanges, _) = SplitAndBuild(input, BuildFreshRanges);
        return LoopAndAdd(freshRanges, AddAllFresh);
    }
    
    public override long SolvePart1(string input)
    {
        return SplitBuildLoopAndAdd(input, BuildFreshRanges, AddFresh);

        long AddFresh(string line, IList<FreshRange> freshRanges)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return 0;
            }
            
            var target = long.Parse(line);
            foreach (var freshRange in freshRanges)
            {
                if (target >= freshRange.Start && target <= freshRange.End)
                {
                    return 1;
                }

                if (target < freshRange.Start)
                {
                    // no other possible ranges
                    break;
                }
            }

            return 0;
        }
    }
    
    private readonly record struct FreshRange(long Start, long End);

    private static (IList<FreshRange>, int) BuildFreshRanges(string[] lines)
    {
        var freshRanges = new List<FreshRange>();

        var i = 0;
        for (; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                // Input switches from fresh ranges to lookup queries
                break;
            }
            
            var parts = line.Split('-');
            if (parts.Length != 2)
            {
                throw new Exception("Invalid input");
            }
            
            var start = long.Parse(parts[0]);
            var end = long.Parse(parts[1]);

            var range = new FreshRange(start, end);
            freshRanges.Add(range);
        }
        
        freshRanges.Sort((a, b) => a.Start.CompareTo(b.Start));

        return (freshRanges, i);
    }
}