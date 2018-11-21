using Kelson.Common.Parse.Rules;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kelson.Common.Parse
{
    public class TokenizerBuilder<TToken>
    {
        private readonly List<Rule<char, TToken>> rules = new List<Rule<char, TToken>>();

        public TokenizerBuilder<TToken> Produce(TToken token, params char[] on)
        {
            if (on.Length == 0)
                throw new IndexOutOfRangeException("character parameters cannot be empty");

            if (on.Length == 1)
                rules.Add(Match<char>.EqualTo(on[0], location: "Tokenizer").Select(c => token));
            else
                rules.Add(
                    First<char, TToken>.Of(
                            on.Select(c => 
                                Match<char>.EqualTo(c, location: "Tokenizer")
                                    .Select(_ => token))
                            .ToArray())
                        .Select(cs => token)
                        .AtLocation("Tokenizer"));

            return this;
        }

        public TokenizerBuilder<TToken> Produce(TToken token, Rule<char, bool> rule)
        {
            rules.Add(new IfElse<char, TToken>(rule, new Value<char, TToken>(token), new Fail<char, TToken>()));

            return this;
        }

        public TokenizerBuilder<TToken> Produce(TToken token, string on)
        {
            if (string.IsNullOrEmpty(on))
                throw new IndexOutOfRangeException("text parameters cannot be empty");

            if (on.Length == 1)
                rules.Add(Match<char>.EqualTo(on[0], location: "Tokenizer").Select(c => token));
            else
                rules.Add(
                    Sequence<char, TToken>.Of(
                            on.Select(c =>
                                Match<char>.EqualTo(c, location: "Tokenizer")
                                    .Select(_ => token))
                            .ToArray())
                        .Select(cs => token)
                        .AtLocation("Tokenizer"));

            return this;
        }

        public Tokenizer<TToken> Build() => new Tokenizer<TToken>(rules);

    }

    public class Tokenizer<TToken>
    {
        private readonly List<Rule<char, TToken>> Rules;

        internal Tokenizer(List<Rule<char, TToken>> rules) => Rules = rules;

        public TokenList<TToken> Scan(string text)
        {
            var characters = text.ToCharArray();
            var text_source = new TextSource(characters);
            var char_source = new Source<char>(new TokenList<char>(characters));

            var tokens = new List<TToken>();
            var positions = new List<TextPosition>();

            foreach (var position in text_source.Positions())
            {
                if (position.Index < char_source.Index)
                    positions[positions.Count - 1] = TextPosition.Union(positions.Last(), position);
                else
                {
                    foreach (var rule in Rules)
                    {
                        if (rule.Parse(char_source) is Success<char, TToken> success)
                        {
                            tokens.Add(success.Value);
                            positions.Add(position);
                            char_source = success.Remaining;
                            goto Next;
                        }
                    }
                    throw new Exception($"Could not parse {position.ToString()}");
                }
                Next:;
            }

            return new TokenList<TToken>(tokens.ToArray(), positions.ToArray(), text_source);
        }
    }
}
