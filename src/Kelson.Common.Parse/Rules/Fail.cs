using Kelson.Common.Parse;

namespace Kelson.Common.Parse.Rules
{
    public class Fail<TToken, TValue> : Rule<TToken, TValue>
    {
        public override string Name => "Fail";

        protected override Result<TToken, TValue> Evaluate(Source<TToken> source)
            => new Failure<TToken, TValue>("Encountered fail rule", source);        
    }
}
