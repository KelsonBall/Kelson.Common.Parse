﻿using Superpower;
using Superpower.Parsers;
using System.Linq;

namespace Lua.Ast
{
    public class LambdaDefinitionExpression : Expression
    {
        public readonly IdentifierList Args;
        public readonly Block Body;

        public LambdaDefinitionExpression(IdentifierList args, Block body)
            => (Args, Body) = (args, body);

        public override dynamic Get(State state) => new Function(Args, Body);

        public new static readonly TokenListParser<Tkn, Expression> Parser =            
            from lparen in Token.EqualTo(Tkn.LParen)
            from args in IdentifierList.Parser
            from rparen in Token.EqualTo(Tkn.RParen)
            from @do in Token.EqualTo(Tkn.Keyword_Do)
            from block in Block.Parser
            from end in Token.EqualTo(Tkn.Keyword_End)
            select (Expression)new FunctionDefinitionExpression(args, block);
    }
}
