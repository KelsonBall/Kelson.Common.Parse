using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class MatchTokenAndProduce<TToken, TValue> : Rule<TToken, TValue>
    {
        public readonly TToken Expected;

        public readonly Func<TValue> Factory;

        public MatchTokenAndProduce(TToken expect, Func<TValue> factory, [CallerMemberName] string location = null) : base(location)
            => (Expected, Factory) = (expect, factory);


        public MatchTokenAndProduce(TToken expect, TValue value, [CallerMemberName] string location = null) : base(location)
            => (Expected, Factory) = (expect, () => value);

        public override string Name => $"Match {Expected} and create {typeof(TValue)}";

        protected override Result<TToken, TValue> Evaluate(Source<TToken> source)
        {
            var top = source.Peek();
            if (source.Peek().Equals(Expected))
                return new Success<TToken, TValue>(source.Next(), Factory());
            else
                return new Failure<TToken, TValue>($"Expected {Expected} but found {top}", source.Next(), Expected);
        }
    }
}
