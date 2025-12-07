namespace AoC25;

public class Day4() : DayBase(4)
{
    public override long SolvePart2(string input)
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
    
    public override long SolvePart1(string input)
    {
        var grid = BuildGrid(input);
        return CountAndMarkAccessibleSpots(grid);
    }

    private static void CleanGrid(char[][] grid)
    {
        foreach (var row in grid)
        {
            for (var col = 0; col < row.Length; col++)
            {
                if (row[col] == 'X')
                {
                    row[col] = '.';
                }
            }
        }
    }

    private static long CountAndMarkAccessibleSpots(char[][] grid)
    {
        var accessibleSpots = 0;

        for (var row = 0; row < grid.Length; row++)
        {
            for (var col = 0; col < grid[row].Length; col++)
            {
                var curr = grid[row][col];

                if (curr != '@')
                {
                    continue;
                }
                
                if (IsAccessible(row, col))
                {
                    grid[row][col] = 'X';
                    accessibleSpots++;
                }
            }
        }

        return accessibleSpots;

        bool IsAccessible(int row, int col)
        {
            var emptySpots = 0;
            for (var x = -1; x <= 1; x++)
            {
                for (var y = -1; y <= 1; y++)
                {
                    var checkRow = row + x;
                    var checkCol = col + y;
                    if (checkRow < 0 || checkCol < 0 || checkRow >= grid.Length || checkCol >= grid[row].Length)
                    {
                        // accessible - off the grid
                        emptySpots++;
                        continue;
                    }

                    if (grid[checkRow][checkCol] == '.')
                    {
                        emptySpots++;
                    }
                }
            }
            
            return emptySpots >= 5;
        }
    }
}