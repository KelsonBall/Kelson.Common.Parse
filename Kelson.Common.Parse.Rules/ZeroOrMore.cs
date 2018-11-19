using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class ZeroOrMore<TToken, TValue> : Rule<TToken, int> where TToken : IEquatable<TToken>
    {
        public readonly Rule<TToken, TValue> Rule;        

        public ZeroOrMore(Rule<TToken, TValue> rule,  [CallerMemberName] string location = null) : base(location)
            => Rule = rule;

        public override string Name => $"Zero or more of [{Rule.Name}]";

        protected override Result<TToken, int> Evaluate(Source<TToken> source)
        {
            Success<TToken, TValue> result = null;
            int count = 0;
            while (Rule.Parse(source) is Success<TToken, TValue> success)
            {
                source = source.Next();
                result = success;
                count++;
            }
            
            return new Success<TToken, int>(source, count);
        }
    }
}
