using Superpower;
using Superpower.Parsers;
using System.Collections.Generic;
using System.Linq;

namespace Lua.Ast
{
    public class ExpressionList
    {
        public readonly Expression[] Values;

        public ExpressionList(IEnumerable<Expression> expressions)
            => Values = expressions.ToArray();        

        public static readonly TokenListParser<Tkn, ExpressionList> Parser =
            from expressions in Expression.Parser.ManyDelimitedBy(Token.EqualTo(Tkn.Comma))
            select new ExpressionList(expressions);
    }
}
