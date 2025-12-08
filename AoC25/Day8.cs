namespace AoC25;

public class Day8() : DayBase(8)
{
    public override long SolvePart2(string input)
    {
        var (lines, junctions, _) = SplitAndBuild(input, BuildJunctions);
        
        var distances = new Dictionary<int, Dictionary<int, double>>();
        var distList = new List<(double Distance, int Id1, int Id2)>();
        for (var i = 0; i < junctions.Count - 1; i++)
        {
            for (var j = i + 1; j < junctions.Count; j++)
            {
                var distance = GetDistance(junctions[i], junctions[j]);
                if (distances.TryGetValue(i, out var d))
                {
                    d[j] = distance;
                }
                else
                {
                    var addSuccess = distances.TryAdd(i, new Dictionary<int, double>
                    {
                        [j] = distance,
                    });
                    if (!addSuccess)
                    {
                        throw new Exception("Distance already exists for that combo");
                    }
                }
                
                distList.Add((distance, i, j));
            }
        }
        
        distList.Sort((a, b) => a.Distance.CompareTo(b.Distance));

        var circuitIdCount = 0;
        var circuits = new Dictionary<int, Circuit>();
        var circuitMembers = new Dictionary<int, int>(); // junctionID -> circuitID
        
        foreach (var dist in distList)
        {
            //Console.WriteLine($"Dist: {dist.Distance}, [{dist.Id1}, {dist.Id2}], ({junctions[dist.Id1]}), ({junctions[dist.Id2]})");
            
            var junction1 = junctions[dist.Id1];
            var junction2 = junctions[dist.Id2];

            if (circuitMembers.TryGetValue(junction1.Id, out var circuitId1))
            {
                if (circuitMembers.TryGetValue(junction2.Id, out var circuitId2))
                {
                    // Both already in circuits
                    if (circuitId1 == circuitId2)
                    {
                        // Both in the same circuit
                        //max++; // do one extra
                        continue;
                    }

                    var circuit1 = circuits[circuitId1];
                    var circuit2 = circuits[circuitId2];
                    foreach (var junction in circuit2.Junctions)
                    {
                        circuitMembers[junction.Id] = circuitId1;
                        circuit1.Junctions.Add(junction);
                    }

                    circuit2.Junctions.Clear();
                    circuits.Remove(circuitId2);
                }
                else
                {
                    // Junction1 in circuit, Junction2 not
                    var circuit1 = circuits[circuitId1];
                    circuit1.Junctions.Add(junction2);
                    circuitMembers[junction2.Id] = circuitId1;
                }
            }
            else
            {
                if (circuitMembers.TryGetValue(junction2.Id, out var circuitId2))
                {
                    // Junction2 in circuit, Junction 1 not
                    var circuit2 =  circuits[circuitId2];
                    circuit2.Junctions.Add(junction1);
                    circuitMembers[junction1.Id] = circuitId2;
                }
                else
                {
                    // Neither in a circuit
                    var circuit = new Circuit(circuitIdCount++, [junction1, junction2]);
                    
                    circuits.Add(circuit.Id, circuit);
                    circuitMembers.Add(junction1.Id, circuit.Id);
                    circuitMembers.Add(junction2.Id, circuit.Id);
                }
            }

            if (circuits.Count == 1 && circuitMembers.Count >= junctions.Count)
            {
                return junction1.X * junction2.X;
            }
        }
        
        return 42;
    }

    public override long SolvePart1(string input)
    {
        var (lines, junctions, _) = SplitAndBuild(input, BuildJunctions);

        var max = 10;
        if (lines.Length > 21)
        {
            max = 1000;
        }
        
        var distances = new Dictionary<int, Dictionary<int, double>>();
        var distList = new List<(double Distance, int Id1, int Id2)>();
        for (var i = 0; i < junctions.Count - 1; i++)
        {
            for (var j = i + 1; j < junctions.Count; j++)
            {
                var distance = GetDistance(junctions[i], junctions[j]);
                if (distances.TryGetValue(i, out var d))
                {
                    d[j] = distance;
                }
                else
                {
                    var addSuccess = distances.TryAdd(i, new Dictionary<int, double>
                    {
                        [j] = distance,
                    });
                    if (!addSuccess)
                    {
                        throw new Exception("Distance already exists for that combo");
                    }
                }
                
                distList.Add((distance, i, j));
            }
        }
        
        distList.Sort((a, b) => a.Distance.CompareTo(b.Distance));

        var circuitIdCount = 0;
        var circuits = new Dictionary<int, Circuit>();
        var circuitMembers = new Dictionary<int, int>(); // junctionID -> circuitID
        
        for (var i = 0; i < max; i++)
        {
            var dist = distList[i];
            //Console.WriteLine($"Dist: {dist.Distance}, [{dist.Id1}, {dist.Id2}], ({junctions[dist.Id1]}), ({junctions[dist.Id2]})");
            
            var junction1 = junctions[dist.Id1];
            var junction2 = junctions[dist.Id2];

            if (circuitMembers.TryGetValue(junction1.Id, out var circuitId1))
            {
                if (circuitMembers.TryGetValue(junction2.Id, out var circuitId2))
                {
                    // Both already in circuits
                    if (circuitId1 == circuitId2)
                    {
                        // Both in the same circuit
                        //max++; // do one extra
                        continue;
                    }

                    var circuit1 = circuits[circuitId1];
                    var circuit2 = circuits[circuitId2];
                    foreach (var junction in circuit2.Junctions)
                    {
                        circuitMembers[junction.Id] = circuitId1;
                        circuit1.Junctions.Add(junction);
                    }

                    circuit2.Junctions.Clear();
                    circuits.Remove(circuitId2);
                }
                else
                {
                    // Junction1 in circuit, Junction2 not
                    var circuit1 = circuits[circuitId1];
                    circuit1.Junctions.Add(junction2);
                    circuitMembers[junction2.Id] = circuitId1;
                }
            }
            else
            {
                if (circuitMembers.TryGetValue(junction2.Id, out var circuitId2))
                {
                    // Junction2 in circuit, Junction 1 not
                    var circuit2 =  circuits[circuitId2];
                    circuit2.Junctions.Add(junction1);
                    circuitMembers[junction1.Id] = circuitId2;
                }
                else
                {
                    // Neither in a circuit
                    var circuit = new Circuit(circuitIdCount++, [junction1, junction2]);
                    
                    circuits.Add(circuit.Id, circuit);
                    circuitMembers.Add(junction1.Id, circuit.Id);
                    circuitMembers.Add(junction2.Id, circuit.Id);
                }
            }
        }

        var sortedCircuits = circuits.Values.ToList();
        sortedCircuits.Sort((c1, c2) => c1.Junctions.Count.CompareTo(c2.Junctions.Count));
        
        var top1 = sortedCircuits[^1];
        var top2 = sortedCircuits[^2];
        var top3 = sortedCircuits[^3];
        var product = top1.Junctions.Count * top2.Junctions.Count * top3.Junctions.Count;
        
        //Console.WriteLine($"Top1: {top1.Junctions.Count},  Top2: {top2.Junctions.Count}, Top3: {top3.Junctions.Count}, Product: {product}");
        
        return product;
    }

    private static (Dictionary<int, Junction>, int) BuildJunctions(string[] lines)
    {
        var id = 0;
        var junctions = new Dictionary<int, Junction>();
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            var junction = new Junction(id++, long.Parse(parts[0]), long.Parse(parts[1]), long.Parse(parts[2]));
            junctions[junction.Id] = junction;
        }

        return (junctions, 0);
    }

    private static double GetDistance(Junction j1, Junction j2)
    {
        return Math.Sqrt(Math.Pow(j1.X - j2.X, 2) + Math.Pow(j1.Y - j2.Y, 2) + Math.Pow(j1.Z - j2.Z, 2));
    }
    
    private readonly record struct Circuit(int Id, List<Junction> Junctions);

    private readonly record struct Junction(int Id, long X, long Y, long Z);
}