using System;
using Superpower;
using System.Linq;

namespace Lua.Ast
{
    public class FunctionCallStatement : Statement
    {
        public Identifier Name;
        public ExpressionList Arguments;

        public FunctionCallStatement(Identifier name, ArgumentList args)
            => (Name, Arguments) = (name, args.Expressions);

        public override void Evaluate(State state)
        {
            var func = state[Name.Value];
            if (func is Function function)
                function?.Invoke(state);
            else if (Name.Value == "print")
            {
                var values = Arguments.Values.Select(a => a.Get(state));
                var text = string.Join('\t', values);
                var fg = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(text);
                Console.ForegroundColor = fg;
            }
        }

        public new static readonly TokenListParser<Tkn, Statement> Parser =
           from name in Identifier.Parser
           from args in ArgumentList.Parser
           select (Statement)new FunctionCallStatement(name, args);
    }
}
