using System;
using System.Collections.Generic;

namespace Kelson.Common.Parse.Rules
{
    public class FirstOf<TToken, TValue> : Rule<TToken, TValue>
    {
        public readonly Rule<TToken, TValue>[] Rules;

        public FirstOf(params Rule<TToken, TValue>[] rules) : base()        
            => Rules = rules;        

        public override string Name => throw new NotImplementedException();

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
    }
}
