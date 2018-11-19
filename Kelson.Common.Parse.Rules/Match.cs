using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class Match<TToken> : Rule<TToken, TToken> 
    {
        public readonly TToken Expected;

        public Match(TToken expect, [CallerMemberName] string location = null) : base(location)
            => Expected = expect;
        

        public override string Name => $"Match {Expected}";

        protected override Result<TToken, TToken> Evaluate(Source<TToken> source)
        {
            var top = source.Peek();
            if (source.Peek().Equals(Expected))
                return new Success<TToken, TToken>(source.Next(), top);
            else
                return new Failure<TToken, TToken>($"Expected {Expected} but found {top}", source.Next(), Expected);
        }
    }
}
