using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class Predicate<TToken> : Rule<TToken, bool>
    {
        public Func<Source<TToken>, (bool result, Source<TToken> remaining)> PredicateFunction;

        public override string Name => "Predicate";

        public Predicate(Func<Source<TToken>, (bool result, Source<TToken> remaining)> predicate, [CallerMemberName] string location = null) : base(location)
        {
            PredicateFunction = predicate;
        }

        protected override Result<TToken, bool> Evaluate(Source<TToken> source)
        {
            try
            {
                var (result, remaining) = PredicateFunction(source);
                return new Success<TToken, bool>(remaining, result);
            }
            catch (Exception e)
            {
                return new Failure<TToken, bool>($"Predicate encountered an exception: {e.GetType()}, {e.Message}", source);
            }
        }
    }
}
