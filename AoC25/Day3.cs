namespace AoC25;

public class Day3() : DayBase(3)
{
    public override long SolvePart2(string input)
    {
        const int numBatteries = 12;
        
        return SumJoltage(input, numBatteries);
    }
    
    public override long SolvePart1(string input)
    {
        const int numBatteries = 2;
            
        return SumJoltage(input, numBatteries);
    }

    private static long SumJoltage(string input, int numBatteries)
    {
        return SplitLoopAndAdd(input, AddJoltage);

        long AddJoltage(string bank)
        {
            if (string.IsNullOrWhiteSpace(bank))
            {
                return 0;
            }
            
            var trimmedBank = bank.Trim();
            return GetMaxJoltage(trimmedBank, numBatteries);
        }
    }
    
    private static long GetMaxJoltage(string bank, int numBatteries)
    {
        var batteries = new (int, int)[numBatteries];
        for (var i = 0; i < numBatteries; i++)
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
        for (var i = 0; i < numBatteries; i++)
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
            if (!int.TryParse(bank.AsSpan(batteryIndex, 1), out var joltage))
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