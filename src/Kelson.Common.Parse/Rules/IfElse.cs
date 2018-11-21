using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class IfElse<TToken, TValue> : Rule<TToken, TValue>
    {
        public readonly Rule<TToken, bool> Condition;

        public readonly Rule<TToken, TValue> IfTrue;

        public readonly Rule<TToken, TValue> IfFalse;

        public override string Name => $"If/Else";

        public IfElse(Rule<TToken, bool> condition, Rule<TToken, TValue> ifTrue, Rule<TToken, TValue> ifFalse, [CallerMemberName] string location = null) : base(location)
        {
            Condition = condition;
            IfTrue = ifTrue;
            IfFalse = ifFalse;
        }

        protected override Result<TToken, TValue> Evaluate(Source<TToken> source)
        {
            var result = Condition.Parse(source);

            if (result is Success<TToken, bool> condition)
            {
                if (condition.Value)
                    return IfTrue.Parse(condition.Remaining);
                else
                    return IfFalse.Parse(condition.Remaining);
            }
            else            
                return new Failure<TToken, TValue>("Condition rule failed", result.Remaining, (IFailure<TToken>)result);            
        }
    }
}
