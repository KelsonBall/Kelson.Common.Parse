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
                    Sequence<char, TToken>.Of(
                            on.Select(c => 
                                Match<char>.EqualTo(c, location: "Tokenizer")
                                    .Select(_ => token))
                            .ToArray())
                        .Select(cs => token)
                        .AtLocation("Tokenizer"));

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



    }

    public class Tokenizer<TToken>
    {
        public TokenList<TToken> Scan(string text)
        {
            throw new NotImplementedException();
        }
    }
}
