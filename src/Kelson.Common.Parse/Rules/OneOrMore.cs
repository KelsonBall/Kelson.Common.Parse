using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class OneOrMore<TToken, TValue> : Rule<TToken, int>
    {
        public readonly Rule<TToken, TValue> Rule;
        public readonly TValue Value;

        public OneOrMore(Rule<TToken, TValue> rule, [CallerMemberName] string location = null) : base(location)
            => Rule = rule;

        public override string Name => $"One or more of [{Rule.Name}]";

        protected override Result<TToken, int> Evaluate(Source<TToken> source)
        {
            var result = Rule.Parse(source);

            if (result is Failure<TToken, TValue> failure)
                return new Failure<TToken, int>($"Expected one or more of [{Rule.Name}], found zero", source.Next(), failure);

            source = source.Next();
            int count = 1;
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
