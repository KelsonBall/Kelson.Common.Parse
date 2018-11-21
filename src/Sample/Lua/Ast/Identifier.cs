using Superpower;
using Superpower.Parsers;
using System.Linq;

namespace Lua.Ast
{
    public class Identifier
    {
        public readonly string Value;

        public Identifier(string name)
        {
            Value = name;
        }

        public Identifier(IdentifierExpression exp)
        {
            Value = exp.Name;
        }

        public static readonly TokenListParser<Tkn, Identifier> Parser =        
            from _ in Token.EqualTo(Tkn.WhiteSpace).Many()
            from first in Token.EqualTo(Tkn.Letter)
            from rest in Token.EqualTo(Tkn.Digit)
                .Or(Token.EqualTo(Tkn.Letter))
                .Or(Token.EqualTo(Tkn.Underscore))
                .Many()
            select new Identifier(first.Concat(rest).TokensToString());
    }    
}
