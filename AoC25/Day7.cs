namespace AoC25;

public class Day7() : DayBase(7)
{
    public override long SolvePart2(string input)
    {
        if (_RootNode is null)
        {
            throw new Exception("You need to run part 1 before part 2");
        }

        return _Sum;
    }

    private static long _Sum = 0;
    private static Node? _RootNode = null;
    private static Dictionary<Position, Node> _NodePositions = new ();
    private record struct Position(int Row, int Col);

    private class Node(Position position, ISet<Node> parents, ISet<Node> children, long count)
    {
        public Position Position { get; } = position;
        public ISet<Node> Parents { get; } = parents;
        public ISet<Node> Children { get; } = children;
        public long Count { get; set; } = count;
    }
    
    private static Node? NewNode(int row, int col, long count)
    {
        var position = new Position(row, col);
        var node = new Node(position, new HashSet<Node>(), new HashSet<Node>(), count);
        if (!_NodePositions.TryAdd(node.Position, node))
        {
            // Node already exists at this location
            return null;
        }

        return node;
    }

    public override long SolvePart1(string input)
    {
        var grid = BuildGrid(input);
        // PrintGrid(grid);

        var curRow = 0;
        var curCol = -1;
        Node rootNode = default;

        for (var i = 0; i < grid[0].Length; i++)
        {
            if (grid[0][i] == 'S')
            {
                curCol = i;
                rootNode = NewNode(0, curCol, 1) ?? throw new Exception("Graph should be empty when root node created");
                _RootNode = rootNode;
                break;
            }
        }

        if (curCol == -1 || rootNode == default)
        {
            PrintGrid(grid);
            throw new ArgumentException("Invalid input");
        }

        var nodes = new List<Node>
        {
            rootNode
        };
        var nextNodes = new List<Node>();
        var totalSplits = 0;
        while (curRow < grid.Length)
        {
            nextNodes = [];
            foreach (var node in nodes)
            {
                var nodePosition = node.Position;
                nodePosition.Row++;
                grid[nodePosition.Row][nodePosition.Col] = '|';
                nodePosition.Row++;

                if (nodePosition.Row >= grid.Length)
                {
                    continue;
                }
                
                var curr = grid[nodePosition.Row][nodePosition.Col];
                if (curr == '^')
                {
                    var leftNode = BuildNode(nodePosition.Row, nodePosition.Col - 1, node);
                    var rightNode = BuildNode(nodePosition.Row, nodePosition.Col + 1, node);

                    if (leftNode is not null || rightNode is not null)
                    {
                        totalSplits++;
                    }
                }
                else if (curr == '.')
                {
                    var nextNode = BuildNode(nodePosition.Row, nodePosition.Col, node);
                    if (nextNode is null)
                    {
                        totalSplits--;
                    }
                    grid[nodePosition.Row][nodePosition.Col] = '|';
                }
                else if (curr == '|')
                {
                    var existingNode = _NodePositions[nodePosition];
                    existingNode.Count += node.Count;
                }
                else
                {
                    throw new ArgumentException("Invalid input, unexpected char: " + curr);
                }
            }

            if (nextNodes.Count == 0)
            {
                break;
            }
            
            nodes = nextNodes;
            curRow += 2;
        }
        
        var n = string.Join(", ", nodes.Select(x => x.Count.ToString()));
        Console.WriteLine($"Nodes: [{n}]");
        var sum = nodes.Sum(x => x.Count);
        Console.WriteLine($"Sum: {sum}");
        _Sum = sum;
        
        PrintGrid(grid);
        return totalSplits;

        Node? BuildNode(int row, int col, Node parent)
        {
            var node = NewNode(row, col, parent.Count);
            if (node is null)
            {
                var position = new Position(row, col);
                if (!_NodePositions.TryGetValue(position, out var existingNode))
                {
                    throw new Exception(
                        "Node just couldn't be created because of an existing node at this position, but now that can't be found");
                }

                if (!existingNode.Parents.Add(parent))
                {
                    throw new Exception("Existing node already has this parent");
                }
                
                existingNode.Count += parent.Count;

                if (!parent.Children.Add(existingNode))
                {
                    throw new Exception("Parent already has existing Node as child");
                }
                
                return null;
            }

            if (!node.Parents.Add(parent))
            {
                throw new ArgumentException("Parent already exists on this brand new node");
            }
            if (!parent.Children.Add(node))
            {
                // TODO: Is this a problem?
                throw new ArgumentException("Invalid input, this node is already a child on this parent");
            }
            
            grid[node.Position.Row][node.Position.Col] = '|';
            nextNodes.Add(node);

            return node;
        }
    }
}