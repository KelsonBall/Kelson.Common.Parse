using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class Optional<TToken, TValue> : Rule<TToken, bool>
    {
        public readonly Rule<TToken, TValue> Rule;

        public Optional(Rule<TToken, TValue> rule, [CallerMemberName] string location = null) : base(location)
            => Rule = rule;

        public override string Name => $"Optional [{Rule.Name}]";

        protected override Result<TToken, bool> Evaluate(Source<TToken> source)
        {
            if (Rule.Parse(source) is Failure<TToken, TValue> failure)
                return new Success<TToken, bool>(source.Next(), false);
            else
                return new Success<TToken, bool>(source.Next(), true);            
        }
    }

    //public class Ignore<TToken, TValue> : Rule<TToken, bool>
    //{
    //    public readonly Rule<TToken, TValue> Rule;

    //    public Ignore(Rule<TToken, TValue> rule, [CallerMemberName] string location = null) : base(location)
    //        => Rule = rule;
    //}
}
