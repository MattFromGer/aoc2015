using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using ClassLib.util;

namespace ClassLib
{
    public class Day09 : AocDay
    {
        private readonly Regex _distanceRegex = new Regex(@"(\w*)\sto\s(\w*)\s=\s(\d*)");

        public int GetShortestPath()
        {
            var edges = GenerateListFromInput(Input);
            var tsp = new TspBruteForce(edges, TspMode.ShortestPath);

            return tsp.GetDistance();
        }

        public int GetLongestPath()
        {
            var edges = GenerateListFromInput(Input);
            var tsp = new TspBruteForce(edges, TspMode.LongestPath);

            return tsp.GetDistance();
        }

        private IEnumerable<MstEdge> GenerateListFromInput(string[] input)
        {
            return from connectionStr in input
                select _distanceRegex.Match(connectionStr)
                into match
                let node1 = match.Groups[1].Value
                let node2 = match.Groups[2].Value
                let distance = int.Parse(match.Groups[3].Value)
                select new MstEdge(node1, node2, distance);
        }

        private enum TspMode
        {
            ShortestPath,
            LongestPath
        }

        private class TspBruteForce
        {
            private IEnumerable<MstEdge> Edges;
            private IList<string> Nodes;
            private IList<string> _nodesVisited = new List<string>();
            private TspMode _tspMode;

            public TspBruteForce(IEnumerable<MstEdge> edges, TspMode mode)
            {
                _tspMode = mode;
                Edges = edges;
                Nodes = edges
                    .Select(x => x.A)
                    .Union(edges.Select(y => y.B))
                    .ToList();
            }

            public int GetDistance()
            {
                var results = new List<int>();
                foreach (var node in Nodes)
                {
                    _nodesVisited.Clear();
                    var result = GetDistanceRecursive(node);

                    results.Add(result);
                }

                return _tspMode switch
                {
                    TspMode.ShortestPath => results.Min(),
                    TspMode.LongestPath => results.Max(),
                    _ => throw new ArgumentOutOfRangeException()
                };
            }

            private int GetDistanceRecursive(string node)
            {
                _nodesVisited.Add(node);

                var distance = 0;
                var connectedNodes = GetConnectedNodes(node).ToList();
                if (connectedNodes.Any())
                {
                    distance = _tspMode switch
                    {
                        TspMode.ShortestPath => connectedNodes
                            .Min(n => GetConnectedNodeDistance(n, node)),
                        TspMode.LongestPath => connectedNodes
                            .Max(n => GetConnectedNodeDistance(n, node)),
                        _ => 0
                    };
                }

                _nodesVisited.Remove(node);

                return distance;
            }

            private int GetConnectedNodeDistance(string node, string parentNode)
            {
                var distance = Edges.FirstOrDefault(
                                       e =>
                                           (e.A == node && e.B == parentNode) ||
                                           (e.A == parentNode && e.B == node))?
                                   .Distance ?? 0;

                distance += GetDistanceRecursive(node);

                return distance;
            }

            private IEnumerable<string> GetConnectedNodes(string node)
            {
                return GetEdges(node)
                    .SelectMany(x => new string[] {x.A, x.B})
                    .Where(s => s != node)
                    .Where(s => !_nodesVisited.Contains(s))
                    .Distinct();
            }

            private IEnumerable<MstEdge> GetEdges(string node)
            {
                return Edges.Where(e => e.A == node || e.B == node);
            }
        }

        private class MstEdge
        {
            public int Distance { get; }
            public string A { get; }
            public string B { get; }

            public MstEdge(string node1, string node2, int distance)
            {
                Distance = distance;
                A = node1;
                B = node2;
            }
        }
    }
}