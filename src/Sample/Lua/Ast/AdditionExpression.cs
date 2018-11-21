namespace Lua.Ast
{
    public class AdditionExpression : BinaryExpression
    {
        public AdditionExpression(Expression left, Expression right) : base(left, right) { }

        public override dynamic Get(State state) 
            => Left.Get(state) + Right.Get(state);
    }
}
