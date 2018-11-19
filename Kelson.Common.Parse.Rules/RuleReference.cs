using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class RuleReference<TToken, TValue> : Rule<TToken, TValue>
    {
        public readonly Func<Rule<TToken, TValue>> Source;

        public RuleReference(Func<Rule<TToken, TValue>> source, [CallerMemberName] string location = null) : base(location)
        {
            Source = source;
        }

        public override string Name => "Rule Reference";

        protected override Result<TToken, TValue> Evaluate(Source<TToken> source) => Source().Parse(source);
    }
}
