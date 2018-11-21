using Superpower;
using System.Linq;

namespace Lua.Ast
{
    public class Block
    {
        private readonly Statement[] statements;

        public Block(params Statement[] statements)
            => this.statements = statements;

        public void Evaluate(State state)
        {
            foreach (var statement in statements)
                statement.Evaluate(state);            
        }

        public static readonly TokenListParser<Tkn, Block> Parser =
            from statements in Statement.Parser.Many()
            select new Block(statements.ToArray());
    }
}
