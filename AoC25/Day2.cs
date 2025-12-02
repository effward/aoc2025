namespace AoC25;

public class Day2
{
    public static long SolvePart2(string input)
    {
        return SumInvalidIds(input, IsValidId);

        bool IsValidId(string numStr)
        {
            for (var digits = 1; digits <= numStr.Length / 2; digits++)
            {
                if (numStr.Length % digits != 0)
                {
                    continue;
                }

                if (AreDigitsRepeated(numStr, digits))
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
        
        bool IsValidId(string numStr)
        {
            if (numStr.Length % 2 != 0)
            {
                // odd number of digits
                return true;
            }

            return !AreDigitsRepeated(numStr, numStr.Length / 2);
        }
    }
    
    private static bool AreDigitsRepeated(string numStr, int digits)
    {
        var target = numStr[..digits];
        for (var i = 0; i < numStr.Length; i += digits)
        {
            var candidate = numStr.Substring(i, digits);
            if (candidate != target)
            {
                return false;
            }
        }

        return true;
    }
    
    private static long SumInvalidIds(string input, Func<string, bool> isValid)
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
                if (!isValid(num.ToString()))
                {
                    total += num;
                }
            }
        }

        return total;
    }
}