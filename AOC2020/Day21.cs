using System;
using System.Linq;
using System.Collections.Generic;

namespace AOC2020
{
    public class Day21
    {
        Dictionary<string, HashSet<string>> possibleAllergens = new Dictionary<string, HashSet<string>>();
        Dictionary<string, List<HashSet<string>>> foodsWith = new Dictionary<string, List<HashSet<string>>>();
        List<string> allIngredients = new List<string>();
        List<string> noAllergen = new List<string>();

        public Day21()
        {
            InputReader ir = new InputReader(21);
            string[] input = ir.GetInput(")\n", ")");

            foreach (string s in input)
            {
                string[] split = s.Split(new string[] { " (contains " }, StringSplitOptions.RemoveEmptyEntries);
                HashSet<string> ingredients = new HashSet<string>(split[0].Split(' '));
                HashSet<string> allergens = new HashSet<string>(split[1].Split(new string[] { ", " }, StringSplitOptions.None));

                foreach (string allergen in allergens)
                {
                    if (!foodsWith.ContainsKey(allergen)) foodsWith[allergen] = new List<HashSet<string>>();
                    foodsWith[allergen].Add(ingredients);
                }

                foreach (string ingredient in ingredients)
                {
                    if (!possibleAllergens.ContainsKey(ingredient)) possibleAllergens[ingredient] = new HashSet<string>();
                    possibleAllergens[ingredient].UnionWith(allergens);
                }

                allIngredients.AddRange(ingredients);
            }
        }

        public long RunSilver()
        {
            foreach(var kvp in possibleAllergens)
            {
                if (!kvp.Value.Any(a => foodsWith[a].All(f => f.Contains(kvp.Key))))
                    noAllergen.Add(kvp.Key);
            }
            return noAllergen.Sum(i => allIngredients.Count(ing => ing == i));
        }

        public string RunGold()
        {
            foreach (string s in noAllergen)
            {
                foreach(var k in foodsWith.Keys)
                {
                    foreach (var list in foodsWith[k])
                        list.Remove(s);
                }
            }

            List<(string ing, string aller)> dangerous = new List<(string ing, string aller)>();

            while(foodsWith.Count > 0)
            {
                foreach(var kvp in foodsWith)
                {
                    HashSet<string> possible = new HashSet<string>(kvp.Value[0]);
                    foreach (var food in kvp.Value) possible.IntersectWith(food);
                    if (possible.Count == 1)
                    {
                        dangerous.Add((possible.ElementAt(0), kvp.Key));
                        foodsWith.Remove(kvp.Key);
                        foreach (var v in foodsWith.Values)
                        {
                            foreach (var l in v)
                                l.Remove(possible.ElementAt(0));
                        }
                        break;
                    }
                }
            }

            dangerous.Sort((a, b) => a.aller.CompareTo(b.aller));
            return string.Join(",", dangerous.Select(e => e.ing));
        }
    }
}