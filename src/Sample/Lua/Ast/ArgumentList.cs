using Superpower;
using Superpower.Parsers;
using System.Linq;

namespace Lua.Ast
{
    public class ArgumentList
    {
        public readonly ExpressionList Expressions;

        public ArgumentList(ExpressionList expressions)
            => Expressions = expressions;

        public static readonly TokenListParser<Tkn, ArgumentList> Parser =           
           from lparen in Token.EqualTo(Tkn.LParen)
           from expressions in ExpressionList.Parser
           from rparen in Token.EqualTo(Tkn.RParen)
           select new ArgumentList(expressions);
    }
}
