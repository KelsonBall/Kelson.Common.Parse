using Superpower;
using System.Linq;

namespace Lua.Ast
{
    public class IdentifierExpression : Expression
    {
        public readonly string Name;

        public IdentifierExpression(Identifier id) => Name = id.Value;

        public override dynamic Get(State state) => state[Name];

        public new static readonly TokenListParser<Tkn, Expression> Parser =
            from id in Identifier.Parser select (Expression)new IdentifierExpression(id);
    }
}
