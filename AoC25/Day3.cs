namespace AoC25;

public class Day3
{
    public static long SolvePart2(string input)
    {
        const int numBatteries = 12;
        
        return SumJoltage(input, numBatteries);
    }
    
    public static long SolvePart1(string input)
    {
        const int numBatteries = 2;
            
        return SumJoltage(input, numBatteries);
    }

    private static long SumJoltage(string input, int numBatteries)
    {
        var banks = input.Split('\n');

        long totalJoltage = 0;
        foreach (var bank in banks)
        {
            if (string.IsNullOrWhiteSpace(bank))
            {
                continue;
            }
            
            var trimmedBank = bank.Trim();
            var bankJoltage = GetMaxJoltage(trimmedBank, numBatteries);
            totalJoltage += bankJoltage;
        }

        return totalJoltage;
    }
    
    private static long GetMaxJoltage(string bank, int numBatteries)
    {
        var batteries = new (int, int)[numBatteries];
        for (int i = 0; i < numBatteries; i++)
        {
            var toSkipFront = 0;
            if (i > 0)
            {
                toSkipFront = batteries[i - 1].Item2 + 1;
            }

            var highest = FindHighest(bank, toSkipFront, numBatteries - i - 1);
            batteries[i] = highest;
        }
            
        long joltage = 0;
        for (int i = 0; i < numBatteries; i++)
        {
            var exp = numBatteries - i - 1;
            joltage += batteries[i].Item1 * (long)Math.Pow(10, exp);
        }
            
        return joltage;
    }
    
    private static (int Value, int Index) FindHighest(string bank, int toSkipFront, int toSkipBack)
    {
        int highest = 0, highestIndex = -1;
        for (var batteryIndex = toSkipFront; batteryIndex < bank.Length - toSkipBack; batteryIndex++)
        {
            if (!int.TryParse(bank.Substring(batteryIndex, 1), out var joltage))
            {
                continue;
            }
            
            if (joltage > highest)
            {
                highest = joltage;
                highestIndex = batteryIndex;
            }
        }
        
        return (highest, highestIndex);
    }
}