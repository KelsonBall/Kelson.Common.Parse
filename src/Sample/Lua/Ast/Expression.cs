using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using System.Linq;

namespace Lua.Ast
{
    public abstract class Expression
    {
        //public abstract dynamic[] Get(State state);

        public abstract dynamic Get(State state);

        public static readonly TokenListParser<Tkn, TokenRef<Tkn>> BinOpParser =
            from _ in Token.EqualTo(Tkn.WhiteSpace).Many()
            from token in Token.EqualTo(Tkn.Add)
                        .Or(Token.EqualTo(Tkn.Sub))
                        .Or(Token.EqualTo(Tkn.Multiply))
                        .Or(Token.EqualTo(Tkn.Division))
            from __ in Token.EqualTo(Tkn.WhiteSpace).Many()
            select token;

        public static readonly TokenListParser<Tkn, Expression> NonBinaryExpressionParser =
            from _ in Token.EqualTo(Tkn.WhiteSpace).Many()
            from exp in NumberExpression.Parser
                .Or(StringExpression.Parser)
                .Or(FunctionDefinitionExpression.Parser)
                .Or(LambdaDefinitionExpression.Parser)
                .Or(IdentifierExpression.Parser)
            select exp;

        public static readonly TokenListParser<Tkn, Expression> Parser =            
            from exp in Parse.Ref(() =>
                    BinaryExpression.Parser.Try()
                        .Or(NonBinaryExpressionParser))            
            select exp;
    }
}
