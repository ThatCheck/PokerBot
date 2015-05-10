using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils
{
    public static class Combinator
    {
        public static List<List<T>> AllCombinationsOf<T>(params List<T>[] sets)
        {
            // need array bounds checking etc for production
            var combinations = new List<List<T>>();

            // prime the data
            foreach (var value in sets[0])
                combinations.Add(new List<T> { value });

            foreach (var set in sets.Skip(1))
                combinations = AddExtraSet(combinations, set);

            return combinations;
        }

        private static List<List<T>> AddExtraSet<T>
             (List<List<T>> combinations, List<T> set)
        {
            var newCombinations = from value in set
                                  from combination in combinations
                                  select new List<T>(combination) { value };

            return newCombinations.ToList();
        }
    }
}
