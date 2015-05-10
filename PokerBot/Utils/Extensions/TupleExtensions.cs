using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerBot.Utils.Extensions
{
    public static class TupleExtensions
    {
        public static IEnumerable<T> ToCollection<T>(this Tuple<T, T> tuple)
        {
            yield return tuple.Item1;
            yield return tuple.Item2;
        }
    }
}
