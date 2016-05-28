using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Node
{
    public Node(int id)
    {
        Id = id;
        Links = new List<Node>();
    }
    public int Id;
    public List<Node> Links;
}

class TeadsSponsoredContest
{
    static void Main(string[] args)
    {
        var linkCount = int.Parse(Console.ReadLine()); // the number of adjacency relations
        var nodes = new Dictionary<int, Node>();
        for (var i = 0; i < linkCount; i++)
        {
            var inputs = Console.ReadLine().Split(' ');
            var xi = int.Parse(inputs[0]); // the ID of a person which is adjacent to yi
            var yi = int.Parse(inputs[1]); // the ID of a person which is adjacent to xi
            Node nodeX, nodeY;
            if (!nodes.TryGetValue(xi, out nodeX))
            {
                nodeX = new Node(xi);
                nodes.Add(xi, nodeX);
            }
            if (!nodes.TryGetValue(yi, out nodeY))
            {
                nodeY = new Node(yi);
                nodes.Add(yi, nodeY);
            }
            nodeX.Links.Add(nodeY);
            nodeY.Links.Add(nodeX);
        }

        var leaf = nodes.Values.First(n => n.Links.Count == 1);
        var maxDist = GetMaxDistance(leaf, leaf);

        Console.WriteLine(Math.Round(maxDist / (float)2, MidpointRounding.AwayFromZero));
    }

    private static int GetMaxDistance(Node from, Node a)
    {
        return a.Links.Where(l => l != from).Select(node => 1 + GetMaxDistance(a, node)).Concat(new[] {0}).Max();
    }
}