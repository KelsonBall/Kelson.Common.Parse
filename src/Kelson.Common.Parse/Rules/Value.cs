using Kelson.Common.Parse;

namespace Kelson.Common.Parse.Rules
{
    public class Value<TToken, TValue> : Rule<TToken, TValue>
    {
        public readonly TValue Result;

        public Value(TValue value) => Result = value;

        public override string Name => "Value";

        protected override Result<TToken, TValue> Evaluate(Source<TToken> source) => new Success<TToken, TValue>(source, Result);
    }
}
