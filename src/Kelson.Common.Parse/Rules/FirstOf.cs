using System;
using System.Collections.Generic;

namespace Kelson.Common.Parse.Rules
{
    public class First<TToken, TValue> : Rule<TToken, TValue>
    {
        public readonly Rule<TToken, TValue>[] Rules;

        public First(params Rule<TToken, TValue>[] rules) : base()        
            => Rules = rules;        

        public override string Name => "First";

        protected override Result<TToken, TValue> Evaluate(Source<TToken> source)
        {
            var failures = new List<Failure<TToken, TValue>>();

            foreach (var rule in Rules)
            {
                var result = rule.Parse(source);
                if (result is Success<TToken, TValue> success)
                    return success;
                else if (result is Failure<TToken, TValue> failure)
                    failures.Add(failure);

            }

            return new Failure<TToken, TValue>("No options matched", source.Next(), failures.ToArray());
        }

        public static First<TToken, TValue> Of(params Rule<TToken, TValue>[] rules)
            => new First<TToken, TValue>(rules);
    }
}
