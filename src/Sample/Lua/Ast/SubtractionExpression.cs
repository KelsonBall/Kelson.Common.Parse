namespace Lua.Ast
{
    public class SubtractionExpression : BinaryExpression
    {
        public SubtractionExpression(Expression left, Expression right) : base(left, right) { }

        public override dynamic Get(State state)
            => Left.Get(state) + Right.Get(state);
    }
}
