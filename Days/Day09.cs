using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClassLib.helper;
using ClassLib.helper.TspBruteForce;
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
    }
}