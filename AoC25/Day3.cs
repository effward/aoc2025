namespace AoC25;

public class Day3
{
    public static long SolvePart1(string input)
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
            var bankJoltage = GetMaxJoltage(trimmedBank);
            totalJoltage += bankJoltage;
        }

        return totalJoltage;
    }

    private static int GetMaxJoltage(string bank)
    {
        var first = FindHighest(bank, 0, 1);
        var second = FindHighest(bank, first.Index + 1, 0);

        return first.Value * 10 + second.Value;
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