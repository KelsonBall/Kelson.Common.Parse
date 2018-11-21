using Superpower;
using Superpower.Parsers;
using System.Linq;

namespace Lua.Ast
{
    public class AssignmentStatement : Statement
    {
        private readonly bool IsLocal;
        private readonly IdentifierList identifiers;
        private readonly ExpressionList expressions;

        public AssignmentStatement(IdentifierList identifiers, ExpressionList expressions, bool isLocal)
            => (this.identifiers, this.expressions, IsLocal) = (identifiers, expressions, isLocal);

        public override void Evaluate(State state)
        {
            int stack = state.StackHeight;
            foreach (var exp in expressions.Values)
                state.Push(exp.Get(state));
            for (int i = identifiers.Values.Length - 1; i >= 0; i--)
            {
                if (state.StackHeight == stack)
                    break;
                else
                {
                    if (IsLocal)
                        state.SetLocal(identifiers.Values[i].Value, state.Pop());
                    else
                        state.Set(identifiers.Values[i].Value, state.Pop());
                }
            }
        }

        public new static readonly TokenListParser<Tkn, Statement> Parser =
            from local in 
                (from l in Token.EqualTo(Tkn.Keyword_Local)
                 from ws in Token.EqualTo(Tkn.WhiteSpace)
                 select l).Optional()
            from name in IdentifierList.Parser
            from _ in Token.EqualTo(Tkn.WhiteSpace).Many()
            from assign in Token.EqualTo(Tkn.Assign)
            from exp in ExpressionList.Parser
            select (Statement)new AssignmentStatement(name, exp, local.HasValue);
    }
}
