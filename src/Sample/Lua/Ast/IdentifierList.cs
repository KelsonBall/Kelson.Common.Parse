using Superpower;
using Superpower.Parsers;
using System.Collections.Generic;
using System.Linq;

namespace Lua.Ast
{
    public class IdentifierList
    {
        public readonly Identifier[] Values;

        public IdentifierList(IEnumerable<Identifier> identifiers)
            => Values = identifiers.ToArray();

        public static readonly TokenListParser<Tkn, IdentifierList> Parser =
            from names in Identifier.Parser.ManyDelimitedBy(Token.EqualTo(Tkn.Comma))
            select new IdentifierList(names);
    }
}
