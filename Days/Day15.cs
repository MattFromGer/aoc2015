using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using ClassLib.util;

namespace ClassLib
{
    public class Day15 : AocDay
    {
        private static readonly Regex InputRegex =
            new Regex(@"(\w*): capacity (-?\d*), durability (-?\d*), flavor (-?\d*), texture (-?\d*), calories (\d*)");

        public int GetScoreOfBestCookie()
        {
            var ingredients = ParseInput(Input);
            var recipe = new CookieRecipe(ingredients);
            var maxScore = recipe.Calculate();

            return maxScore;
        }

        public int GetScoreOfBestCookieAt500Calories()
        {
            var ingredients = ParseInput(Input);
            var recipe = new CookieRecipe(ingredients, 500);
            var maxScore = recipe.Calculate();

            return maxScore;
        }

        private IEnumerable<Ingredient> ParseInput(string[] input)
        {
            return input
                .Select(instr => InputRegex.Match(instr))
                .Select(match =>
                    new Ingredient(
                        Convert.ToString(match.Groups[1].Value),
                        Convert.ToInt32(match.Groups[2].Value),
                        Convert.ToInt32(match.Groups[3].Value),
                        Convert.ToInt32(match.Groups[4].Value),
                        Convert.ToInt32(match.Groups[5].Value),
                        Convert.ToInt32(match.Groups[6].Value))
                );
        }

        private class CookieRecipe
        {
            private const int MaxAmount = 100;
            private readonly int? _calorieTarget;
            private int _bestScore = 0;
            private readonly Ingredient[] _distribution;

            public CookieRecipe(IEnumerable<Ingredient> ingredients, int? calorieTarget = null)
            {
                _distribution = ingredients.ToArray();
                _calorieTarget = calorieTarget;
            }

            private int CalculateScore()
            {
                var scoreCapacity = Math.Max(0,
                    _distribution.Sum(x => x.Capacity * x.CurrentAmount));
                var scoreDurability = Math.Max(0,
                    _distribution.Sum(x => x.Durability * x.CurrentAmount));
                var scoreFlavor = Math.Max(0,
                    _distribution.Sum(x => x.Flavor * x.CurrentAmount));
                var scoreTexture = Math.Max(0,
                    _distribution.Sum(x => x.Texture * x.CurrentAmount));

                var totalScore = scoreCapacity * scoreDurability * scoreFlavor * scoreTexture;

                return totalScore;
            }

            private int CalculateCalories()
            {
                return _distribution.Sum(x => x.Calories * x.CurrentAmount);
            }

            public int Calculate()
            {
                CalculateScoreRecursive(_distribution.Length - 1, MaxAmount);

                return _bestScore;
            }

            private void CalculateScoreRecursive(int ingredientNumber, int amount)
            {
                for (int i = 0; i < amount; i++)
                {
                    var remainingAmount = amount - i;
                    if (ingredientNumber > 0)
                    {
                        _distribution[ingredientNumber].CurrentAmount = i;
                        CalculateScoreRecursive(ingredientNumber - 1, remainingAmount);
                    }
                    else
                    {
                        _distribution[ingredientNumber].CurrentAmount = remainingAmount;
                    }

                    if (_distribution.Sum(x => x.CurrentAmount) != MaxAmount) continue;
                    var score = CalculateScore();
                    if (score > _bestScore)
                    {
                        StoreScore(score);
                    }
                }
            }

            private void StoreScore(int score)
            {
                if (!_calorieTarget.HasValue)
                {
                    _bestScore = score;
                }

                if (_calorieTarget.HasValue)
                {
                    var calories = CalculateCalories();
                    if (calories == _calorieTarget)
                    {
                        _bestScore = score;
                    }
                }
            }
        }

        private class Ingredient
        {
            public string Name { get; }
            public int Capacity { get; }
            public int Durability { get; }
            public int Flavor { get; }
            public int Texture { get; }
            public int Calories { get; }
            public int CurrentAmount { get; set; }

            public Ingredient(string name, int capacity, int durability, int flavor, int texture, int calories)
            {
                Name = name;
                Capacity = capacity;
                Durability = durability;
                Flavor = flavor;
                Texture = texture;
                Calories = calories;
            }
        }
    }
}