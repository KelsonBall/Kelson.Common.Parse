using System.Collections.Generic;

namespace Lua.Ast
{
    public class Function : IFunction
    {
        private readonly IdentifierList names; 
        private readonly Block body;

        public Function(IdentifierList names, Block block)
        {
            this.body = block;
        }

        public dynamic[] Invoke(State state, params dynamic[] values)
        {
            var local = new State(state);
            for (int i = 0; i < names.Values.Length; i++)
            {
                if (i < values.Length)
                    local.SetLocal(names.Values[i].Value, values[i]);
                else
                    local.SetLocal(names.Values[i].Value, null);
            }

            body.Evaluate(local);

            var results = new List<dynamic>();
            while (local.StackHeight > 0)
                results.Add(local.Pop());
            return results.ToArray();
        }
    }
}
