namespace AoC25;

public class Day7() : DayBase(7)
{
    public override long SolvePart2(string input)
    {
        return 42;
    }

    public override long SolvePart1(string input)
    {
        var grid = BuildGrid(input);
        // PrintGrid(grid);

        var curRow = 0;
        var curCol = -1;

        for (var i = 0; i < grid[0].Length; i++)
        {
            if (grid[0][i] == 'S')
            {
                curCol = i;
                break;
            }
        }

        if (curCol == -1)
        {
            PrintGrid(grid);
            throw new ArgumentException("Invalid input");
        }

        var beams = new Dictionary<int, Beam>
        {
            [curCol] = new (curRow, curCol)
        };
        var totalSplits = 0;
        while (curRow < grid.Length)
        {
            var nextBeams = new Dictionary<int, Beam>();
            foreach (var kv in beams)
            {
                var beam = kv.Value;
                beam.Row++;
                grid[beam.Row][beam.Col] = '|';
                beam.Row++;

                if (beam.Row >= grid.Length)
                {
                    continue;
                }
                
                var curr = grid[beam.Row][beam.Col];
                if (curr == '^')
                {
                    var leftBeam = beam with { Col = beam.Col - 1 };
                    var rightBeam = beam with { Col = beam.Col + 1 };
                    bool eitherAdded = false;
                    if (nextBeams.TryAdd(leftBeam.Col, leftBeam))
                    {
                        grid[leftBeam.Row][leftBeam.Col] = '|';
                        eitherAdded = true;
                    }

                    if (nextBeams.TryAdd(rightBeam.Col, rightBeam))
                    {
                        grid[rightBeam.Row][rightBeam.Col] = '|';
                        eitherAdded = true;
                    }

                    if (eitherAdded)
                    {
                        totalSplits++;
                    }
                }
                else if (curr == '.')
                {
                    if (!nextBeams.TryAdd(beam.Col, beam))
                    {
                        totalSplits--;
                        grid[beam.Row][beam.Col] = '|';
                    }
                }
                else if (curr == '|')
                {
                    // do nothing
                }
                else
                {
                    throw new ArgumentException("Invalid input, unexpected char: " + curr);
                }
            }
            
            beams = nextBeams;
            curRow += 2;
        }
        
        return totalSplits;
    }
    
    private record struct Beam(int Row, int Col);
}