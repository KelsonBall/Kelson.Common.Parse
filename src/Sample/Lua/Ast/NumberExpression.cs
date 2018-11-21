using Superpower;
using Superpower.Model;
using Superpower.Parsers;
using System.Linq;

namespace Lua.Ast
{
    public class NumberExpression : Expression
    {
        public readonly double Value;

        public NumberExpression(TokenRef<Tkn>[] natural, TokenRef<Tkn>[] fractional, bool negative = false)
        {
            string nat = string.Join("", natural.Select(n => n.ToStringValue()));
            if (fractional != null)
            {
                string frac = string.Join("", natural.Select(n => n.ToStringValue()));
                Value = double.Parse($"{nat}.{frac}");
            }
            else
                Value = (double)int.Parse(nat);
            if (negative)
                Value = -Value;
        }

        public override dynamic Get(State state) => Value;

        public new static readonly TokenListParser<Tkn, Expression> Parser =
            from negative in Token.EqualTo(Tkn.Sub).Optional()
            from natural in Token.EqualTo(Tkn.Digit).AtLeastOnce()
            from fractional in
                (from _ in Token.EqualTo(Tkn.Period)
                 from digits in Token.EqualTo(Tkn.Digit).AtLeastOnce()
                 select digits).OptionalOrDefault()
            select (Expression)(new NumberExpression(natural, fractional, negative: negative != null));
    }

    public class StringExpression : Expression
    {
        public readonly string Value;

        public StringExpression(TokenRef<Tkn> value) => Value = value.ToStringValue();
        

        public override dynamic Get(State state) => Value;

        public new static readonly TokenListParser<Tkn, Expression> Parser =
            from text in Token.EqualTo(Tkn.Text)
            select (Expression)new StringExpression(text);
    }
}
