using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClassLib.util;

namespace ClassLib
{
    public class Day14 : AocDay
    {
        private static readonly Regex InputRegex =
            new Regex(@"(\w*) can fly (\d*) km\/s for (\d*) seconds, but then must rest for (\d*) seconds.");

        public decimal GetDistanceOfWinnerAfter(int durationInSeconds)
        {
            var reindeer = ParseInput(Input);
            var distances = reindeer.ToDictionary(d => d, d => d.CalculateDistance(durationInSeconds));

            return distances.Values.Max();
        }

        public int GetPointsOfWinner(int durationInSeconds)
        {
            var reindeer = ParseInput(Input);
            var olympics = new ReindeerPointOlympics(reindeer);
            var result = olympics.StartRace(durationInSeconds);

            return result.Values.Max();
        }

        private IEnumerable<Reindeer> ParseInput(string[] input)
        {
            return input
                .Select(instr => InputRegex.Match(instr))
                .Select(match =>
                    new Reindeer(
                        Convert.ToString(match.Groups[1].Value),
                        Convert.ToInt32(match.Groups[2].Value),
                        Convert.ToInt32(match.Groups[3].Value),
                        Convert.ToInt32(match.Groups[4].Value))
                );
        }

        private class ReindeerPointOlympics
        {
            private readonly IEnumerable<Reindeer> _reindeer;
            
            public ReindeerPointOlympics(IEnumerable<Reindeer> reindeer)
            {
                _reindeer = reindeer.ToList();
            }

            public IDictionary<Reindeer, int> StartRace(int durationInSeconds)
            {
                for (int i = 1; i <= durationInSeconds; i++)
                {
                    foreach (var deer in _reindeer)
                    {
                        deer.MakeMove();
                    }

                    var firstReindeer = _reindeer
                        .Where(x => x.CurrentPosition == _reindeer.Max(d => d.CurrentPosition))
                        .ToList();
                    
                    firstReindeer.ForEach(x => x.CurrentPoints++);
                }

                return _reindeer.ToDictionary(x => x, y => y.CurrentPoints);
            }
        }

        private enum ReindeerState
        {
            Flying,
            Resting
        }

        private class Reindeer
        {
            private string Name { get; }
            private int Velocity { get; }
            private int MaxFlyDuration { get; }
            private int RestDuration { get; }
            private int SecondsToStateChange { get; set; }
            private ReindeerState State { get; set; } = ReindeerState.Flying;

            public int CurrentPosition { get; private set; } 
            public int CurrentPoints { get; set; }

            public Reindeer(string name, int velocity, int maxFlyDuration, int restDuration)
            {
                Name = name;
                Velocity = velocity;
                MaxFlyDuration = maxFlyDuration;
                RestDuration = restDuration;
                SecondsToStateChange = MaxFlyDuration;
            }

            public void MakeMove()
            {
                CurrentPosition += State switch
                {
                    ReindeerState.Flying => Velocity,
                    ReindeerState.Resting => 0,
                    _ => throw new ArgumentOutOfRangeException()
                };

                SecondsToStateChange--;
                
                if (SecondsToStateChange <= 0)
                {
                    switch(State)
                    {
                        case ReindeerState.Flying:
                            State = ReindeerState.Resting;
                            SecondsToStateChange = RestDuration;
                            break;
                        case ReindeerState.Resting:
                            State = ReindeerState.Flying;
                            SecondsToStateChange = MaxFlyDuration;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    };
                }
            }

            public decimal CalculateDistance(decimal durationInSeconds)
            {
                var noOfParts = Math.Floor(durationInSeconds / (MaxFlyDuration + RestDuration));
                var modulo = durationInSeconds % (MaxFlyDuration + RestDuration);

                var distance = noOfParts * Velocity * MaxFlyDuration;

                if (modulo >= MaxFlyDuration)
                {
                    distance += Velocity * MaxFlyDuration;
                }

                return distance;
            }
        }
    }
}