using System;
using Superpower;
using System.Linq;

namespace Lua.Ast
{
    public class PrintStatement : Statement
    {
        private readonly Identifier identifier;

        public PrintStatement(Identifier identifier) => this.identifier = identifier;

        public override void Evaluate(State state) => Console.WriteLine(state[identifier.Value]?.ToString() ?? "null");

        public new static readonly TokenListParser<Tkn, Statement> Parser =            
            from name in Identifier.Parser
            select (Statement)new PrintStatement(name);
    }
}
