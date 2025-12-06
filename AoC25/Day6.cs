namespace AoC25;

public class Day6() : DayBase(6)
{
    public override long SolvePart2(string input)
    {
        var (_, stacks, _) = SplitAndBuild(input, BuildCephalopodStacks);

        return LoopAndAdd(stacks, AddCephalopodProblem, 0);

        long AddCephalopodProblem((string Op, Stack<long> Stack) problem)
        {
            var op = problem.Op;
            var stack = problem.Stack;
            
            return op switch
            {
                "*" => stack.Aggregate(1L, (t, num) => t * num),
                "+" => stack.Sum(),
                _ => throw new ArgumentException("Unknown operator: " + op),
            };
        }
    }

    public override long SolvePart1(string input)
    {
        var (_, stacks, _) = SplitAndBuild(input, BuildStacks);

        return LoopAndAdd(stacks, AddMathProblems, 0);

        long AddMathProblems(Stack<string> mathProblem)
        {
            var op = mathProblem.Pop();
            return op switch
            {
                "*" => mathProblem.Aggregate(1L, (total, numStr) => total * long.Parse(numStr)),
                "+" => mathProblem.Aggregate(0L, (total, numStr) => total + long.Parse(numStr)),
                _ => throw new ArgumentException("Unknown operator: " + op)
            };
        }
    }

    private static (List<(string, Stack<long>)>, int) BuildCephalopodStacks(string[] lines)
    {
        var (grid, ops) = BuildGrid(lines);
        
        var stacks =  new List<(string, Stack<long>)>();
        var stack = new Stack<long>();
        var opIdx = 0;
        for (var col = 0; col < grid[0].Length; col++)
        {
            var allWhitespace = true;
            var numberStack = new Stack<char>();
            foreach (var row in grid)
            {
                var c =  row[col];
                if (char.IsDigit(c))
                {
                    allWhitespace = false;
                    numberStack.Push(c);
                }
            }

            if (!allWhitespace)
            {
                long number = 0;
                var i = 0;
                while (numberStack.Count > 0)
                {
                    var exp = (long)Math.Pow(10, i++);
                    var digit = long.Parse(numberStack.Pop().ToString());
                    number += digit * exp;
                }
            
                stack.Push(number);
            }
            else
            {
                stacks.Add((ops[opIdx++], stack));
                stack = new Stack<long>();
            }
        }

        return (stacks, 0);
    }

    private static (char[][], string[]) BuildGrid(string[] lines)
    {
        var grid = new char[lines.Length - 1][];

        for (var row = 0; row < lines.Length - 1; row++)
        {
            var line = lines[row];
            var gridLine = new char[line.Length];
            
            for (var col = 0; col < line.Length; col++)
            {
                gridLine[col] = line[col];
            }
            
            grid[row] = gridLine;
        }
        
        var ops = SplitLine(lines[^1]);
        return (grid, ops);
    }

    private static (List<Stack<string>>, int) BuildStacks(string[] lines)
    {
        var count = SplitLine(lines[0]);
        var stacks = count.Select(_ => new Stack<string>()).ToList();
        foreach (var line in lines)
        {
            var parts = SplitLine(line);
            if (parts.Length != stacks.Count)
            {
                throw new ArgumentException("Invalid input, mismatched row lengths");
            }

            for (var i = 0; i < parts.Length; i++)
            {
                stacks[i].Push(parts[i]);
            }
        }

        return (stacks, 0);
    }

    private static string[] SplitLine(string line) =>
        line.Split().Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
}