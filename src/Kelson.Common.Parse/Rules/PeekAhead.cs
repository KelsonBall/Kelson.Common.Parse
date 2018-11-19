using System;

namespace Kelson.Common.Parse.Rules
{
    public class PeekAhead<TToken, TValue> : Rule<TToken, TValue>
    {
        public readonly Rule<TToken, TValue> Rule;
        public readonly int Distance;

        public PeekAhead(int distance, Rule<TToken, TValue> rule) : base()
            => Rule = rule;

        public override string Name => throw new NotImplementedException();

        protected override Result<TToken, TValue> Evaluate(Source<TToken> source)
        {
            source = new Source<TToken>(
                source.List, 
                index: source.Index + Distance, 
                ruleName: source.RuleName, 
                location: source.RuleLocation);

            return Rule.Parse(source);
        }
    }
}
