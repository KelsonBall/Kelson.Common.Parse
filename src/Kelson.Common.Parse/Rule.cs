namespace Kelson.Common.Parse
{
    public abstract class Result<TToken, TValue>
    {        
        public Source<TToken> Remaining { get; }

        internal Result() { }

        internal Result(Source<TToken> remainder)
            => Remaining = remainder;
    }

    public class Success<TToken, TValue> : Result<TToken, TValue>
    {
        public TValue Value { get; }

        public Success(Source<TToken> remainder, TValue value) : base(remainder)
            => Value = value;
    }

    public interface IFailure<TToken>
    {
        string Message { get; }
        TToken[] Expectations { get; }
        IFailure<TToken>[] InnerFailures { get; }
        Source<TToken> Remaining { get; }
    }

    public class Failure<TToken, TValue> : Result<TToken, TValue>, IFailure<TToken>
    {
        public string Message { get; }
        public TToken[] Expectations { get; }
        public IFailure<TToken>[] InnerFailures { get; }

        public Failure(string message, Source<TToken> remainder) : base(remainder)
        {
            Message = message;
            Expectations = new TToken[0];
            InnerFailures = new IFailure<TToken>[0];
        }

        public Failure(string message, Source<TToken> remainder, params TToken[] expect) : base(remainder)
        {
            Message = message;            
            Expectations = expect;
            InnerFailures = new IFailure<TToken>[0];
        }

        public Failure(string message, Source<TToken> remainder, params IFailure<TToken>[] failures) : base(remainder)
        {
            Message = message;
            Expectations = new TToken[0];
            InnerFailures = failures;
        }

    }

    public abstract class Rule<TIn, TOut>
    {
        public abstract string Name { get; }
        public string Location { get; internal set; }

        public Result<TIn, TOut> Parse(Source<TIn> source) => Evaluate(source.InRule(this));
        
        protected abstract Result<TIn, TOut> Evaluate(Source<TIn> source);

        protected Rule(string location) => Location = location;

        protected Rule() => Location = "None";
    }
}
