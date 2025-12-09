namespace AoC25;

public class Day9() : DayBase(9)
{
    public override long SolvePart2(string input)
    {
        return 42;
    }

    public override long SolvePart1(string input)
    {
        var tiles = new List<Tile>();
        var lines = SplitLines(input);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            tiles.Add(new Tile(long.Parse(parts[0]), long.Parse(parts[1])));
        }

        var areas = new List<long>();
        for (var i = 0; i < tiles.Count; i++)
        {
            for (var j = i + 1; j < tiles.Count; j++)
            {
                areas.Add(AreaOfRectangle(tiles[i], tiles[j]));
            }
        }

        areas.Sort();

        return areas[^1];
    }

    private static long AreaOfRectangle(Tile t1, Tile t2)
    {
        return (Math.Abs(t1.X - t2.X) + 1) * (Math.Abs(t1.Y - t2.Y) + 1);
    }
    
    private readonly record struct Tile(long X, long Y);
}