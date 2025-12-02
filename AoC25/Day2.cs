namespace AoC25;

public class Day2
{
    public static long SolvePart2(string input)
    {
        return SumInvalidIds(input, IsValidId);

        bool IsValidId(long num)
        {
            var numStr = num.ToString();
            for (var digits = 1; digits <= numStr.Length / 2; digits++)
            {
                if (numStr.Length % digits != 0)
                {
                    continue;
                }

                var target = numStr[..digits];
                bool allMatch = true;
                for (var i = 0; i < numStr.Length; i += digits)
                {
                    for (var c = 0; c < target.Length; c++)
                    {
                        if (target[c] != numStr[i + c])
                        {
                            allMatch = false;
                            break;
                        }
                    }

                    if (!allMatch)
                    {
                        break;
                    }
                }

                if (allMatch)
                {
                    return false;
                }
            }

            return true;
        }
    }
    
    public static long SolvePart1(string input)
    {
        return SumInvalidIds(input, IsValidId);
        
        bool IsValidId(long num)
        {
            var numStr = num.ToString();
            if (numStr.Length % 2 != 0)
            {
                // odd number of digits
                return true;
            }
                
            var target = numStr[..(numStr.Length / 2)];
            if (numStr.EndsWith(target))
            {
                return false;
            }

            return true;
        }
    }
    
    private static long SumInvalidIds(string input, Func<long, bool> isValid)
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
                if (!isValid(num))
                {
                    total += num;
                }
            }
        }

        return total;
    }
}