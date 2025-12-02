namespace AoC25;

public class Day1
{
    public static int SolvePart2(string input)
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
            
            if (movement > 0)
            {
                // Count multiples of 100 in the range (position, position + movement]
                var firstMultiple = ((start / 100) + 1) * 100;
                var lastMultiple = (end / 100) * 100;

                if (firstMultiple > end)
                {
                    return 0;
                }
                
                return (lastMultiple - firstMultiple) / 100 + 1;
            }
            else // movement < 0
            {
                // Count multiples of 100 in the range [position + movement, position)
                var firstMult = (int)Math.Ceiling(end / 100.0) * 100;
                var lastMult = (int)Math.Floor((start - 1) / 100.0) * 100;

                if (firstMult >= start)
                {
                    return 0;
                }

                if (lastMult < firstMult)
                {
                    return 0;
                }
                
                return (lastMult - firstMult) / 100 + 1;
            }
        }
    }
    
    public static int SolvePart1(string input)
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
        var position = 50;
        var counter = 0;
        
        var inputs = inputStr.Split('\n');
        foreach (var input in inputs)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Input is empty/whitespace");
                continue;
            }

            if (input.Length <= 1)
            {
                Console.WriteLine("Input is invalid");
                continue;
            }

            var direction = input[0];
            var mult = 1;
            switch (direction)
            {
                case 'R':
                    mult = 1;
                    break;
                case 'L':
                    mult = -1;
                    break;
                default:
                    Console.WriteLine("Input is invalid");
                    throw new InvalidOperationException();
            }

            var distanceStr = input.Substring(1);
            var distance = int.Parse(distanceStr);
            var movement = distance * mult;
            counter += countFunc(position, movement);
            
            position = ((position + movement) % 100 + 100) % 100;
        }

        return counter;
    }
}