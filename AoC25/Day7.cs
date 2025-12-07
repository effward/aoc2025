namespace AoC25;

public class Day7() : DayBase(7)
{
    public override long SolvePart2(string input)
    {
        return _Sum;
    }

    private static long _Sum = 0;
    private static Dictionary<Position, Node> _NodePositions = new ();

    public override long SolvePart1(string input)
    {
        var grid = BuildGrid(input);
        
        var nodes = new List<Node>();
        var nextNodes = new List<Node>
        {
            FindRootNode(grid)
        };
        
        var totalSplits = 0;
        while (nextNodes.Count > 0)
        {
            nodes = nextNodes;
            nextNodes = [];
            foreach (var node in nodes)
            {
                ProcessNode(node);
            }
        }
        
        var sum = nodes.Sum(x => x.Count);
        _Sum = sum;
        
        return totalSplits;

        void ProcessNode(Node node)
        {
            var nodePosition = node.Position;
            nodePosition.Row++;
            grid[nodePosition.Row][nodePosition.Col] = '|';
            nodePosition.Row++;

            if (nodePosition.Row >= grid.Length)
            {
                return;
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

        Node? BuildNode(int row, int col, Node parent)
        {
            var node = NewNode(row, col, parent.Count);
            if (node is null)
            {
                var position = new Position(row, col);
                var existingNode = _NodePositions[position];
                existingNode.Parents.Add(parent);
                existingNode.Count += parent.Count;
                parent.Children.Add(existingNode);
                
                return null;
            }

            node.Parents.Add(parent);
            parent.Children.Add(node);
            
            grid[node.Position.Row][node.Position.Col] = '|';
            nextNodes.Add(node);

            return node;
        }
    }

    private static Node FindRootNode(char[][] grid)
    {
        Node? rootNode = null;

        for (var i = 0; i < grid[0].Length; i++)
        {
            if (grid[0][i] == 'S')
            {
                rootNode = NewNode(0, i, 1) ?? throw new Exception("Graph should be empty when root node created");
                break;
            }
        }

        if (rootNode is null)
        {
            PrintGrid(grid);
            throw new ArgumentException("Invalid input");
        }
        
        return rootNode;
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
    
    private record struct Position(int Row, int Col);
    
    private class Node(Position position, ISet<Node> parents, ISet<Node> children, long count)
    {
        public Position Position { get; } = position;
        public ISet<Node> Parents { get; } = parents;
        public ISet<Node> Children { get; } = children;
        public long Count { get; set; } = count;
    }
    
}