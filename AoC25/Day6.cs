namespace AoC25;

public class Day6() : DayBase(6)
{
    public override long SolvePart2(string input)
    {
        return 42;
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

    private static string[] SplitLine(string line)
    {
        return line.Split().Where(l => !string.IsNullOrWhiteSpace(l)).ToArray();
    }
}