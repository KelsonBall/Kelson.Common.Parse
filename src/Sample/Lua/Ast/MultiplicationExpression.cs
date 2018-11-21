namespace Lua.Ast
{
    public class MultiplicationExpression : BinaryExpression
    {
        public MultiplicationExpression(Expression left, Expression right) : base(left, right) { }

        public override dynamic Get(State state)
            => Left.Get(state) * Right.Get(state);
    }
}
