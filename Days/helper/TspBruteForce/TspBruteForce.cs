using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassLib.helper.TspBruteForce
{
    public class TspBruteForce
    {
        private readonly IEnumerable<MstEdge> _edges;
        private readonly IList<string> _nodes;
        private readonly IList<string> _nodesVisited = new List<string>();
        private readonly TspMode _tspMode;
        private readonly bool _isRoundTrip;

        public TspBruteForce(IEnumerable<MstEdge> edges, TspMode mode, bool isRoundTrip = false)
        {
            _tspMode = mode;
            _isRoundTrip = isRoundTrip;
            _edges = edges;
            _nodes = edges
                .Select(x => x.A)
                .Union(edges.Select(y => y.B))
                .ToList();
        }

        public int GetDistance()
        {
            var results = _nodes.Select(GetEdgesRecursive);

            return _tspMode switch
            {
                TspMode.ShortestPath => results.Min(x => x.Sum(e => e.Distance)),
                TspMode.LongestPath => results.Max(x => x.Sum(e => e.Distance)),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private IList<MstEdge> GetEdgesRecursive(string node)
        {
            _nodesVisited.Add(node);

            var edges = new List<MstEdge>();
            var connectedNodes = GetConnectedNodes(node).ToList();
            if (connectedNodes.Any())
            {
                switch (_tspMode)
                {
                    case TspMode.ShortestPath:
                        var e = connectedNodes
                            .Select(n => GetEdges(n, node))
                            .OrderBy(x => x.Sum(e => e.Distance))
                            .First()
                            .ToList();
                        edges.AddRange(e);
                        break;

                    case TspMode.LongestPath:
                        var e2 = connectedNodes
                            .Select(n => GetEdges(n, node))
                            .OrderByDescending(x => x.Sum(e => e.Distance))
                            .First()
                            .ToList();
                        edges.AddRange(e2);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            } 
            
            if (!connectedNodes.Any() && _isRoundTrip)
            {
                var cycleEdge = GetEdge(_nodesVisited.FirstOrDefault(), node);
                edges.Add(cycleEdge);
            }

            _nodesVisited.Remove(node);

            return edges;
        }

        private MstEdge GetEdge(string node1, string node2)
        {
            return _edges.FirstOrDefault(
                e =>
                    (e.A == node1 && e.B == node2) ||
                    (e.A == node2 && e.B == node1));
        }

        private IList<MstEdge> GetEdges(string node, string parentNode)
        {
            var edges = GetEdgesRecursive(node);

            var edge = GetEdge(node, parentNode);
            edges.Add(edge);

            return edges;
        }

        private IEnumerable<string> GetConnectedNodes(string node)
        {
            return GetConnectedEdges(node)
                .SelectMany(x => new string[] {x.A, x.B})
                .Where(s => s != node)
                .Where(s => !_nodesVisited.Contains(s))
                .Distinct();
        }

        private IEnumerable<MstEdge> GetConnectedEdges(string node)
        {
            return _edges.Where(e => e.A == node || e.B == node);
        }
    }
}