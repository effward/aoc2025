namespace AoC25;

public class Day1() : DayBase(1)
{
    public override long SolvePart2(string input)
    {
        return PerformCount(input, CountZeros);

        int CountZeros(int position, int movement)
        {
            if (movement == 0)
            {
                return 0;
            }
            
            var start = position;
            var end = position + movement;

            int firstMultiple, lastMultiple;
            
            if (movement > 0)
            {
                // Count multiples of 100 in the range (position, position + movement]
                firstMultiple = ((start / 100) + 1) * 100;
                lastMultiple = (end / 100) * 100;

                if (firstMultiple > end)
                {
                    return 0;
                }
            }
            else // movement < 0
            {
                // Count multiples of 100 in the range [position + movement, position)
                firstMultiple = (int)Math.Ceiling(end / 100.0) * 100;
                lastMultiple = (int)Math.Floor((start - 1) / 100.0) * 100;

                if (firstMultiple >= start)
                {
                    return 0;
                }

                if (lastMultiple < firstMultiple)
                {
                    return 0;
                }
            }
            
            return (lastMultiple - firstMultiple) / 100 + 1;
        }
    }
    
    public override long SolvePart1(string input)
    {
        return PerformCount(input, CountFinalZeros);

        int CountFinalZeros(int position, int movement)
        {
            var newPos = position + movement;
            return newPos % 100 == 0 ? 1 : 0;
        }
    }

    private static int PerformCount(string inputStr, Func<int, int, int> countFunc)
    {
        const char right = 'R';
        const char left = 'L';
    
        var position = 50;
        var counter = 0;

        var inputs = SplitLines(inputStr);
        foreach (var input in inputs)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                continue;
            }

            var direction = input[0];
            var mult = direction switch
            {
                right => 1,
                left => -1,
                _ => throw new InvalidOperationException()
            };

            var distanceStr = input[1..];
            var distance = int.Parse(distanceStr);
            var movement = distance * mult;
            counter += countFunc(position, movement);
            
            position = ((position + movement) % 100 + 100) % 100;
        }

        return counter;
    }
}