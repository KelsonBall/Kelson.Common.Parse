namespace Lua.Ast
{
    public interface IFunction
    {
        dynamic[] Invoke(State state, params dynamic[] values);
    }
}
