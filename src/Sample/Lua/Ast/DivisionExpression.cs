namespace Lua.Ast
{
    public class DivisionExpression : BinaryExpression
    {
        public DivisionExpression(Expression left, Expression right) : base(left, right) { }

        public override dynamic Get(State state)
            => Left.Get(state) / Right.Get(state);
    }
}
