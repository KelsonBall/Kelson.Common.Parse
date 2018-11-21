using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using System.Linq;

namespace Lua.Ast
{
    public class FunctionDefinitionStatement : Statement
    {
        private readonly bool IsLocal;
        private readonly Identifier Name;
        private readonly IdentifierList Args;
        private readonly Block Body;

        public FunctionDefinitionStatement(Identifier name, IdentifierList args, Block body, bool isLocal)
        {
            IsLocal = isLocal;
            Name = name;
            Args = args;
            Body = body;
        }

        public override void Evaluate(State state)
        {
            if (IsLocal)
                state.SetLocal(Name.Value, new Function(Args, Body));
            else
                state.Set(Name.Value, new Function(Args, Body));
        }

        public new static TokenListParserResult<Tkn, Statement> Parser(TokenList<Tkn> tokens)
        {
            return new TokenListParserResult<Tkn, Statement>()
            {
                
            };
        }

        //public new static readonly TokenListParser<Tkn, Statement> Parser =
        //    from local in Token.EqualTo(Tkn.Keyword_Local).Optional()
        //    from func in Token.EqualTo(Tkn.Keyword_Function)
        //    from space in Token.EqualTo(Tkn.WhiteSpace).AtLeastOnce()
        //    from name in Identifier.Parser
        //    from _ in Token.EqualTo(Tkn.WhiteSpace).Many()
        //    from lparen in Token.EqualTo(Tkn.LParen)
        //    from args in IdentifierList.Parser
        //    from rparen in Token.EqualTo(Tkn.RParen)
        //    from body in Block.Parser
        //    from end in Token.EqualTo(Tkn.Keyword_End)
        //    select (Statement)new FunctionDefinitionStatement(name, args, body, local.HasValue);
    }
}
