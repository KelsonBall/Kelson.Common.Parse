using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class DelimetedBy<TToken, TRule, TDelimeter> : Rule<TToken, TRule[]>
    {
        public readonly Rule<TToken, TRule> Rule;

        public readonly Rule<TToken, TDelimeter> Delimeter;

        public override string Name => $"[{Rule.Name}] delimeted by [{Delimeter.Name}]";

        public DelimetedBy(Rule<TToken, TRule> rule, Rule<TToken, TDelimeter> delimeter, [CallerMemberName] string location = null) : base(location)
        {
            Rule = rule;
            Delimeter = delimeter;
        }

        protected override Result<TToken, TRule[]> Evaluate(Source<TToken> source)
        {
            var results = new List<TRule>();

            Result<TToken, TRule> result = null;

            while ((result = Rule.Parse(source)) is Success<TToken, TRule> success)
            {
                results.Add(success.Value);
                source = success.Remaining;
                var delimeter = Delimeter.Parse(source);
                if (delimeter is Failure<TToken, TDelimeter> failure)
                    break;
                source = delimeter.Remaining;
            }

            return new Success<TToken, TRule[]>(source, results.ToArray());
        }
    }
}
