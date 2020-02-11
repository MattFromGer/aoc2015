using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClassLib.helper;
using ClassLib.helper.TspBruteForce;
using ClassLib.util;

namespace ClassLib
{
    public class Day13 : AocDay
    {
        private readonly Regex _inputRegex =
            new Regex(@"(\w*) would (lose|gain) (\d*) happiness units by sitting next to (\w*).");


        public int GetMaxHappiness()
        {
            var rules = ParseInput(Input);
            var edges = GenerateEdges(rules);
            
            
            return GetMaxAchievableHappiness(edges);
        }

        public int GetMaxHappinessWithAddedPerson()
        {
            var rules = ParseInput(Input);
            var edges = GenerateEdges(rules);
            
            var extraEdges = rules
                .GroupBy(x => x.Person)
                .Select(p =>
                    new MstEdge("Me", p.Key, 0));

            return GetMaxAchievableHappiness(edges.Concat(extraEdges));
        }

        private IEnumerable<MstEdge> GenerateEdges(IEnumerable<HappinessRule> rules)
        {
            var unified = new List<HappinessRule>();
            foreach (var r in rules.ToList())
            {
                var refRule = rules.First(r2 => r.Person == r2.PersonRef && r.PersonRef == r2.Person);
                r.HappinessValue += refRule.HappinessValue;

                if (!unified.Contains(r))
                {
                    unified.Add(r);
                }
            }
            
            var edges = unified.Select(x => new MstEdge(x.Person, x.PersonRef, x.HappinessValue));

            return edges;
        }

        private int GetMaxAchievableHappiness(IEnumerable<MstEdge> edges)
        {
            var tsp = new TspBruteForce(edges, TspMode.LongestPath, true);
            var maxHappiness = tsp.GetDistance();
            
            return maxHappiness;
        }

        private IEnumerable<HappinessRule> ParseInput(string[] input)
        {
            IList<HappinessRule> rules = new List<HappinessRule>();
            foreach (var str in input)
            {
                var match = _inputRegex.Match(str);
                var name1 = match.Groups[1].Value;
                var name2 = match.Groups[4].Value;
                var value = Convert.ToInt32(match.Groups[3].Value);
                var sign = match.Groups[2].Value;

                value *= sign switch
                {
                    "gain" => 1,
                    "lose" => -1,
                    _ => throw new ArgumentOutOfRangeException()
                };

                rules.Add(new HappinessRule(name1, name2, value));
            }

            return rules;
        }

        private class HappinessRule
        {
            public readonly string Person;
            public readonly string PersonRef;
            public int HappinessValue { get; set; }

            public HappinessRule(string person, string personRef, int value)
            {
                Person = person;
                PersonRef = personRef;
                HappinessValue = value;
            }

            public override bool Equals(object obj)
            {
                if (obj is HappinessRule rule)
                {
                    return (Person == rule.Person && PersonRef == rule.PersonRef)
                           || (Person == rule.PersonRef && PersonRef == rule.Person);
                }

                return base.Equals(obj);
            }

            public override string ToString()
            {
                return $"{Person} {HappinessValue} {PersonRef}";
            }
        }


        // // Kruskal is not returning a complete cycle
        // private class KruskalMST
        // {
        //     private readonly Dictionary<string, string> _parent;
        //     private readonly Dictionary<string, int> _rank;
        //     private readonly IEnumerable<string> _nodes;
        //     private readonly IList<HappinessRule> _rules;
        //
        //     public KruskalMST(IList<HappinessRule> rules)
        //     {
        //         _rules = rules
        //             .OrderByDescending(x => x.HappinessValue)
        //             .ToList();
        //         _nodes = rules
        //             .SelectMany(x => new[] {x.Person, x.PersonRef})
        //             .Distinct()
        //             .ToList();
        //
        //         _rank = _nodes.ToDictionary(x => x, y => 0);
        //         _parent = _nodes.ToDictionary(x => x, y => y);
        //     }
        //
        //     public IList<HappinessRule> GetMaximumSpanningTreeValue()
        //     {
        //         var mst = new List<HappinessRule>();
        //         foreach (var r in _rules)
        //         {
        //             var p1 = FindNode(r.Person);
        //             var p2 = FindNode(r.PersonRef);
        //
        //             if (ShouldBeAddedToMst(p1, p2, mst))
        //             {
        //                 mst.Add(r);
        //                 if (_rank[p1] > _rank[p2])
        //                 {
        //                     _parent[p2] = p1;
        //                 }
        //                 else if (_rank[p2] > _rank[p1])
        //                 {
        //                     _parent[p1] = p2;
        //                 }
        //                 else
        //                 {
        //                     _parent[p1] = p2;
        //                     _rank[p2]++;
        //                 }
        //             }
        //         }
        //
        //         return mst;
        //     }
        //
        //     private static bool ShouldBeAddedToMst(string p1, string p2, IList<HappinessRule> rules)
        //     {
        //         return p1 != p2 &&
        //                rules.Count(x => x.Person == p1 || x.PersonRef == p1) < 2 &&
        //                rules.Count(x => x.Person == p2 || x.PersonRef == p2) < 2;
        //     }
        //
        //     private string FindNode(string node)
        //     {
        //         return _parent[node] == node ? _parent[node] : FindNode(_parent[node]);
        //     }
        // }
    }
}