namespace AoC25;

public class Day2
{
    public static long Solve(string input)
    {
        long total = 0;
        var parts = input.Split(',');
        foreach (var part in parts)
        {
            var bounds = part.Split('-');
            if (bounds.Length != 2)
            {
                throw new ArgumentException("Invalid input format", nameof(input));
            }
            
            var lower = long.Parse(bounds[0]);
            var upper = long.Parse(bounds[1]);

            for (var num = lower; num <= upper; num++)
            {
                var numStr = num.ToString();
                if (numStr.Length % 2 != 0)
                {
                    // odd number of digits
                    continue;
                }
                
                var target = numStr[..(numStr.Length / 2)];
                if (numStr.EndsWith(target))
                {
                    total += num;
                }
            }
        }

        return total;
    }
}