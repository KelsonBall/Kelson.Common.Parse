namespace Lua.Ast
{
    public class ReturnStatement : Statement
    {
        public readonly ExpressionList Expressions;

        public override void Evaluate(State state)
        {
            foreach (var value in Expressions.Values)
                state.Push(value.Get(state));
        }
    }
}
