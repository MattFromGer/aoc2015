using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Text.Json;
using ClassLib.util;

namespace ClassLib
{
    public class Day12 : AocDay
    {
        private readonly Regex _onlyNumbersRegex = new Regex(@"(-?\d{1,})");
        private readonly string _forbiddenString = "red";

        public int GetTotalSumOfNumbers()
        {
            return ExtractNumbers(Input[0]).Sum();
        }

        public int GetTotalSumOfNumbersWithoutRed()
        {
            var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(Input[0]);

            return ProcessObj(dict);
        }

        private int ProcessObj(IDictionary<string, object> parent)
        {
            try
            {
                return parent.Sum(kvp => ExtractNumber((JsonElement) kvp.Value));
            }
            catch (CharacterNowAllowedException)
            {
                return 0;
            }
        }

        private int ExtractNumber(JsonElement elem, JsonValueKind parentKind = JsonValueKind.Object)
        {
            switch (elem.ValueKind)
            {
                case JsonValueKind.Array:
                    var arr = JsonSerializer.Deserialize<IList<JsonElement>>(elem.GetRawText());
                    return arr.Sum(x => ExtractNumber(x, JsonValueKind.Array));
                case JsonValueKind.Number:
                    return elem.GetInt32();
                case JsonValueKind.String:
                    if (parentKind != JsonValueKind.Array && elem.GetString().Contains(_forbiddenString))
                        throw new CharacterNowAllowedException();
                    return ExtractNumbers(elem.GetString()).Sum();
                case JsonValueKind.Object:
                    var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(elem.GetRawText());
                    return ProcessObj(dict);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private IEnumerable<int> ExtractNumbers(string json)
        {
            return _onlyNumbersRegex
                .Matches(json)
                .Select(x => Convert.ToInt32(x.Value));
        }

        private class CharacterNowAllowedException : Exception
        {
        }
    }
}