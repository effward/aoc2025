namespace AoC25;

public class Day5 : IDay
{
    public string Description => "Day 5";
    
    public long SolvePart2(string input)
    {
        return 42;
    }
    
    public long SolvePart1(string input)
    {
        input = input.Trim();
        var lines = input.Split('\n');
        
        var (freshRanges, idx) = BuildFreshRanges(lines);

        var freshCount = 0;
        for (var i = idx; i < lines.Length; i++)
        {
            var line = lines[i];
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }
            
            var target = long.Parse(line);
            foreach (var freshRange in freshRanges)
            {
                if (target >= freshRange.Start && target <= freshRange.End)
                {
                    freshCount++;
                    break;
                }

                if (target < freshRange.Start)
                {
                    // no other possible ranges
                    break;
                }
            }
        }

        return freshCount;
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