using Kelson.Common.Parse;
using System.Collections.Generic;
using System.Linq;

namespace Lua
{
    public static class ________________________________________________________________________________________________
    {
        public static string TokensToString(this IEnumerable<TokenRef<Tkn>> tokens)
            => string.Join(string.Empty, tokens.Select(t => t.ToStringValue()));

        public static IEnumerable<T> Concat<T>(this T value, IEnumerable<T> list)
        {
            yield return value;
            foreach (var item in list)
                yield return item;
        }

        public static dynamic[] Yield(dynamic value)
            => new dynamic[] { value };
    }   
}
