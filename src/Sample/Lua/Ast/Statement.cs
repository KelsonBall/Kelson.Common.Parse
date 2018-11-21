using Superpower;
using Superpower.Parsers;

namespace Lua.Ast
{
    public abstract class Statement
    {
        public abstract void Evaluate(State state);

        public static TokenListParser<Tkn, Statement> Parser =
            from _ in Token.EqualTo(Tkn.WhiteSpace).Many()
            from stat in FunctionDefinitionStatement.Parser
                .Or(AssignmentStatement.Parser.Try().Or(FunctionCallStatement.Parser))                
            from ws in Token.EqualTo(Tkn.WhiteSpace).AtLeastOnce()          
            select stat;
    }
}
