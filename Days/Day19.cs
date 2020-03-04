using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ClassLib.util;

namespace ClassLib
{
    public class Day19 : AocDay
    {
        private static readonly Regex InputRegex = new Regex(@"(\w*) => (\w*)");

        public int GetNumberOfPossibleMolecules()
        {
            var mappings = ParseInput(Input);
            var molecule = Input.Last();

            var molecules = GetAllPossibleMolecules(mappings, molecule);

            return molecules.Distinct().Count();
        }

        public int GetMinNumberOfSteps()
        {
            var mappings = ParseInput(Input);
            var molecule = Input.Last();
            
            var inputElements = mappings
                .Where(x => !x.IsFinalReaction)
                .Select(x => x.Input)
                .ToList();

            var elementsRnAr = new[] {"Rn", "Ar"};
            var elementsY = new[] {"Y"};
            
            inputElements.AddRange(elementsRnAr);
            inputElements.AddRange(elementsY);
            
            var noOfElements = Regex.Matches(molecule, string.Join("|", inputElements)).Count();
            var noOfElementsRnAr = Regex.Matches(molecule, string.Join("|", elementsRnAr)).Count();
            var noOfElementsY = Regex.Matches(molecule, string.Join("|", elementsY)).Count();

            return noOfElements - noOfElementsRnAr - 2 * noOfElementsY - 1;
        }

        private IEnumerable<string> GetAllPossibleMolecules(IEnumerable<MoleculeMapping> mappings, string haystack)
        {
            return (from mapping in mappings
                let needle = mapping.Input
                from Match match in Regex.Matches(haystack, needle)
                select haystack.Remove(match.Index, needle.Length)
                    .Insert(match.Index, mapping.Output)).ToList();
        }

        private IEnumerable<MoleculeMapping> ParseInput(string[] input)
        {
            return input
                .TakeWhile(s => !string.IsNullOrEmpty(s))
                .Select(s => InputRegex.Match(s))
                .Select(match =>
                    new MoleculeMapping(match.Groups[1].Value, match.Groups[2].Value));
        }

        private class MoleculeMapping
        {
            public string Input { get; }
            public string Output { get; }
            public bool IsFinalReaction => Input == "e";
            
            public MoleculeMapping(string input, string output)
            {
                Input = input;
                Output = output;
            }

            public override string ToString()
            {
               return $"{Input} => {Output}";
            }
        }
    }
}