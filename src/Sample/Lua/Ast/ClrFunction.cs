using System;

namespace Lua.Ast
{
    public class ClrFunction : IFunction
    {
        private readonly Func<dynamic[], dynamic[]> body;

        public ClrFunction(Action action)
            => body = args => { action(); return new dynamic[0]; };
        
        public ClrFunction(Action<dynamic[]> action)
            => body = args => { action(args); return new dynamic[0]; };

        public ClrFunction(Func<dynamic[]> func)
            => body = args => func();

        public ClrFunction(Func<dynamic[], dynamic[]> func)
            => body = func;

        public dynamic[] Invoke(State state, params dynamic[] values) => body(values);        
    }
}
