using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class AtLeastOneDelimetedBy<TToken, TRule,TDelimeter> : Rule<TToken, TRule[]>
    {
        public readonly Rule<TToken, TRule> Rule;

        public readonly Rule<TToken, TDelimeter> Delimeter;

        public override string Name => $"At least one [{Rule.Name}] delimeted by [{Delimeter.Name}]";

        public AtLeastOneDelimetedBy(Rule<TToken, TRule> rule, Rule<TToken, TDelimeter> delimeter, [CallerMemberName] string location = null) : base(location)
        {
            Rule = rule;
            Delimeter = delimeter;
        }

        protected override Result<TToken, TRule[]> Evaluate(Source<TToken> source)
        {
            var results = new List<TRule>();

            Result<TToken, TRule> result = Rule.Parse(source);

            if (result is Failure<TToken, TRule> first_failure)
                return new Failure<TToken, TRule[]>($"Expected at least one [{Rule.Name}]", first_failure.Remaining, first_failure);
            else if (result is Success<TToken, TRule> first_success)
            {
                results.Add(first_success.Value);
                source = first_success.Remaining;
                var delimeter = Delimeter.Parse(source);
                if (delimeter is Failure<TToken, TDelimeter> f)
                    return new Success<TToken, TRule[]>(delimeter.Remaining, results.ToArray());
                source = delimeter.Remaining;
            }

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
