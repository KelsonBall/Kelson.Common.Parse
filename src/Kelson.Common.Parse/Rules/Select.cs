using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class Select<TToken, TRule, TValue> : Rule<TToken, TValue>
    {
        public readonly Rule<TToken, TRule> Rule;
        public readonly Func<TRule, TValue> Factory;

        public Select(Rule<TToken, TRule> rule, Func<TRule, TValue> factory, [CallerMemberName] string location = null) : base(location)
        {
            Rule = rule;
            Factory = factory;
        }

        public override string Name => $"Select from rule [{Rule.Name}]";

        protected override Result<TToken, TValue> Evaluate(Source<TToken> source)
        {
            var result = Rule.Parse(source);
            if (result is Success<TToken, TRule> success)
                return new Success<TToken, TValue>(success.Remaining, Factory(success.Value));
            else if (result is Failure<TToken, TRule> failure)
                return new Failure<TToken, TValue>("Select failed", result.Remaining, failure);
            throw new InvalidOperationException();
        }
    }
}
