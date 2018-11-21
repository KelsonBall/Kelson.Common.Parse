using Superpower;
using Superpower.Model;
using System.Linq;

namespace Lua.Ast
{
    public abstract class BinaryExpression : Expression
    {
        public readonly Expression Left;
        public readonly Expression Right;

        public BinaryExpression(Expression left, Expression right)
            => (Left, Right) = (left, right);

        public static Expression Create(TokenRef<Tkn> op, Expression lhs, Expression rhs)
        {
            switch (op.Kind)
            {
                case Tkn.Add:
                    return new AdditionExpression(lhs, rhs);
                case Tkn.Sub:
                    return new SubtractionExpression(lhs, rhs);
                case Tkn.Multiply:
                    return new MultiplicationExpression(lhs, rhs);
                case Tkn.Division:
                    return new DivisionExpression(lhs, rhs);
            }
            return null;
        }

        public new static readonly TokenListParser<Tkn, Expression> Parser =            
            from nonbin in Parse.Ref(() => NonBinaryExpressionParser)            
            from op in BinOpParser
            from exp in Parse.Ref(() => Expression.Parser)
            select Create(op, nonbin, exp);
    }
}
