namespace AoC25;

public class Day4 : IDay
{
    public string Description => "Day 4";
    
    public long SolvePart2(string input)
    {
        var grid = BuildGrid(input);
        long totalSpots = 0;
        long accessibleSpots;

        do
        {
            accessibleSpots = CountAndMarkAccessibleSpots(grid);
            totalSpots += accessibleSpots;
            CleanGrid(grid);
        } while (accessibleSpots > 0);
        
        return totalSpots;
    }
    
    public long SolvePart1(string input)
    {
        var grid = BuildGrid(input);
        return CountAndMarkAccessibleSpots(grid);
    }

    private static char[][] BuildGrid(string input)
    {
        var lines = input.Split('\n');
        var grid = from line in lines
            where !string.IsNullOrWhiteSpace(line)
            select line.Trim().ToCharArray();
        return grid.ToArray();
    }

    private static void CleanGrid(char[][] grid)
    {
        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == 'X')
                {
                    grid[i][j] = '.';
                }
            }
        }
    }

    private static long CountAndMarkAccessibleSpots(char[][] grid)
    {
        var accessibleSpots = 0;

        for (var i = 0; i < grid.Length; i++)
        {
            for (var j = 0; j < grid[i].Length; j++)
            {
                var curr = grid[i][j];

                if (curr == '@')
                {
                    if (IsAccessible(i, j))
                    {
                        grid[i][j] = 'X';
                        accessibleSpots++;
                    }
                }
            }
        }

        return accessibleSpots;

        bool IsAccessible(int i, int j)
        {
            var emptySpots = 0;
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    var checkX = i + x;
                    var checkY = j + y;
                    if (checkX < 0 || checkY < 0 || checkX >= grid.Length || checkY >= grid[i].Length)
                    {
                        // accessible - off the grid
                        emptySpots++;
                        continue;
                    }

                    if (grid[checkX][checkY] == '.')
                    {
                        emptySpots++;
                    }
                }
            }
            
            return emptySpots >= 5;
        }
    }
}