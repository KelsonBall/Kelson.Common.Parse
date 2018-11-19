using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class Sequence<TToken, TValue> : Rule<TToken, TValue[]>
    {
        public readonly Rule<TToken, TValue>[] Rules;

        public Sequence(params Rule<TToken, TValue>[] rules)
        {
            Rules = rules;
        }

        public override string Name => $"Sequence TToken -> [{typeof(TValue)}]";

        protected override Result<TToken, TValue[]> Evaluate(Source<TToken> source)
        {
            Result<TToken, TValue> result = null;
            var values = new List<TValue>();
            foreach (var rule in Rules)
            {
                result = rule.Parse(source);
                if (result is Failure<TToken, TValue> failure)
                    return new Failure<TToken, TValue[]>("Sequence failed", failure.Remaining, failure);
                else if (result is Success<TToken, TValue> success)
                {
                    values.Add(success.Value);
                    source = success.Remaining;
                }
            }
            return new Success<TToken, TValue[]>(source, values.ToArray());
        }

        public static Sequence<TToken, TValue> Of(params Rule<TToken, TValue>[] rules)
            => new Sequence<TToken, TValue>(rules);
    }

    public class Sequence<TToken, T1, T2> : Rule<TToken, (T1, T2)>
    {
        public Rule<TToken, T1> R1;
        public Rule<TToken, T2> R2;

        public override string Name => $"Sequence TToken -> [{string.Join(", ", GetType().GenericTypeArguments.Skip(1))}]";

        public Sequence(Rule<TToken, T1> r1, Rule<TToken, T2> r2, [CallerMemberName] string location = null) : base(location)
        {
            R1 = r1;
            R2 = r2;
        }

        protected override Result<TToken, (T1, T2)> Evaluate(Source<TToken> source)
        {
            object r = null;
            if ((r = R1.Parse(source)) is Success<TToken, T1> s1)
                if ((r = R2.Parse(s1.Remaining)) is Success<TToken, T2> s2)
                    return new Success<TToken, (T1, T2)>(
                        s2.Remaining,
                        (s1.Value, s2.Value));
            var f = (IFailure<TToken>)r;
            return new Failure<TToken, (T1, T2)>($"Sequence failed", f.Remaining, f);
        }
    }

    public class Sequence<TToken, T1, T2, T3> : Rule<TToken, (T1, T2, T3)>
    {
        public Rule<TToken, T1> R1;
        public Rule<TToken, T2> R2;
        public Rule<TToken, T3> R3;

        public override string Name => $"Sequence TToken -> [{string.Join(", ", GetType().GenericTypeArguments.Skip(1))}]";

        public Sequence(Rule<TToken, T1> r1, Rule<TToken, T2> r2, Rule<TToken, T3> r3, [CallerMemberName] string location = null) : base(location)
        {
            R1 = r1;
            R2 = r2;
            R3 = r3;
        }

        protected override Result<TToken, (T1, T2, T3)> Evaluate(Source<TToken> source)
        {
            object r = null;
            if ((r = R1.Parse(source)) is Success<TToken, T1> s1)
                if ((r = R2.Parse(s1.Remaining)) is Success<TToken, T2> s2)
                    if ((r = R3.Parse(s2.Remaining)) is Success<TToken, T3> s3)
                        return new Success<TToken, (T1, T2, T3)>(
                            s3.Remaining,
                            (s1.Value, s2.Value, s3.Value));
            var f = (IFailure<TToken>)r;
            return new Failure<TToken, (T1, T2, T3)>($"Sequence failed", f.Remaining, f);
        }
    }

    public class Sequence<TToken, T1, T2, T3, T4> : Rule<TToken, (T1, T2, T3, T4)>
    {
        public Rule<TToken, T1> R1;
        public Rule<TToken, T2> R2;
        public Rule<TToken, T3> R3;
        public Rule<TToken, T4> R4;

        public override string Name => $"Sequence TToken -> [{string.Join(", ", GetType().GenericTypeArguments.Skip(1))}]";

        public Sequence(Rule<TToken, T1> r1, Rule<TToken, T2> r2, Rule<TToken, T3> r3, Rule<TToken, T4> r4, [CallerMemberName] string location = null) : base(location)
        {
            R1 = r1;
            R2 = r2;
            R3 = r3;
            R4 = r4;
        }

        protected override Result<TToken, (T1, T2, T3, T4)> Evaluate(Source<TToken> source)
        {
            object r = null;
            if ((r = R1.Parse(source)) is Success<TToken, T1> s1)
                if ((r = R2.Parse(s1.Remaining)) is Success<TToken, T2> s2)
                    if ((r = R3.Parse(s2.Remaining)) is Success<TToken, T3> s3)
                        if ((r = R4.Parse(s3.Remaining)) is Success<TToken, T4> s4)
                            return new Success<TToken, (T1, T2, T3, T4)>(
                                s4.Remaining,
                                (s1.Value, s2.Value, s3.Value, s4.Value));
            var f = (IFailure<TToken>)r;
            return new Failure<TToken, (T1, T2, T3, T4)>($"Sequence failed", f.Remaining, f);
        }
    }

    public class Sequence<TToken, T1, T2, T3, T4, T5> : Rule<TToken, (T1, T2, T3, T4, T5)>
    {
        public Rule<TToken, T1> R1;
        public Rule<TToken, T2> R2;
        public Rule<TToken, T3> R3;
        public Rule<TToken, T4> R4;
        public Rule<TToken, T5> R5;

        public override string Name => $"Sequence TToken -> [{string.Join(", ", GetType().GenericTypeArguments.Skip(1))}]";

        public Sequence(Rule<TToken, T1> r1, Rule<TToken, T2> r2, Rule<TToken, T3> r3, Rule<TToken, T4> r4, Rule<TToken, T5> r5, [CallerMemberName] string location = null) : base(location)
        {
            R1 = r1;
            R2 = r2;
            R3 = r3;
            R4 = r4;
            R5 = r5;
        }

        protected override Result<TToken, (T1, T2, T3, T4, T5)> Evaluate(Source<TToken> source)
        {
            object r = null;
            if ((r = R1.Parse(source)) is Success<TToken, T1> s1)
                if ((r = R2.Parse(s1.Remaining)) is Success<TToken, T2> s2)
                    if ((r = R3.Parse(s2.Remaining)) is Success<TToken, T3> s3)
                        if ((r = R4.Parse(s3.Remaining)) is Success<TToken, T4> s4)
                            if ((r = R5.Parse(s4.Remaining)) is Success<TToken, T5> s5)
                                return new Success<TToken, (T1, T2, T3, T4, T5)>(
                                    s5.Remaining,
                                    (s1.Value, s2.Value, s3.Value, s4.Value, s5.Value));
            var f = (IFailure<TToken>)r;
            return new Failure<TToken, (T1, T2, T3, T4, T5)>($"Sequence failed", f.Remaining, f);
        }
    }

    public class Sequence<TToken, T1, T2, T3, T4, T5, T6> : Rule<TToken, (T1, T2, T3, T4, T5, T6)>
    {
        public Rule<TToken, T1> R1;
        public Rule<TToken, T2> R2;
        public Rule<TToken, T3> R3;
        public Rule<TToken, T4> R4;
        public Rule<TToken, T5> R5;
        public Rule<TToken, T6> R6;

        public override string Name => $"Sequence TToken -> [{string.Join(", ", GetType().GenericTypeArguments.Skip(1))}]";

        public Sequence(Rule<TToken, T1> r1, Rule<TToken, T2> r2, Rule<TToken, T3> r3, Rule<TToken, T4> r4, Rule<TToken, T5> r5, Rule<TToken, T6> r6, [CallerMemberName] string location = null) : base(location)
        {
            R1 = r1;
            R2 = r2;
            R3 = r3;
            R4 = r4;
            R5 = r5;
            R6 = r6;
        }

        protected override Result<TToken, (T1, T2, T3, T4, T5, T6)> Evaluate(Source<TToken> source)
        {
            object r = null;
            if ((r = R1.Parse(source)) is Success<TToken, T1> s1)
                if ((r = R2.Parse(s1.Remaining)) is Success<TToken, T2> s2)
                    if ((r = R3.Parse(s2.Remaining)) is Success<TToken, T3> s3)
                        if ((r = R4.Parse(s3.Remaining)) is Success<TToken, T4> s4)
                            if ((r = R5.Parse(s4.Remaining)) is Success<TToken, T5> s5)
                                if ((r = R6.Parse(s5.Remaining)) is Success<TToken, T6> s6)
                                    return new Success<TToken, (T1, T2, T3, T4, T5, T6)>(
                                        s6.Remaining,
                                        (s1.Value, s2.Value, s3.Value, s4.Value, s5.Value, s6.Value));
            var f = (IFailure<TToken>)r;
            return new Failure<TToken, (T1, T2, T3, T4, T5, T6)>($"Sequence failed", f.Remaining, f);
        }
    }

    public class Sequence<TToken, T1, T2, T3, T4, T5, T6, T7> : Rule<TToken, (T1, T2, T3, T4, T5, T6, T7)>
    {
        public Rule<TToken, T1> R1;
        public Rule<TToken, T2> R2;
        public Rule<TToken, T3> R3;
        public Rule<TToken, T4> R4;
        public Rule<TToken, T5> R5;
        public Rule<TToken, T6> R6;
        public Rule<TToken, T7> R7;

        public override string Name => $"Sequence TToken -> [{string.Join(", ", GetType().GenericTypeArguments.Skip(1))}]";

        public Sequence(Rule<TToken, T1> r1, Rule<TToken, T2> r2, Rule<TToken, T3> r3, Rule<TToken, T4> r4, Rule<TToken, T5> r5, Rule<TToken, T6> r6, Rule<TToken, T7> r7, [CallerMemberName] string location = null) : base(location)
        {
            R1 = r1;
            R2 = r2;
            R3 = r3;
            R4 = r4;
            R5 = r5;
            R6 = r6;
            R7 = r7;
        }

        protected override Result<TToken, (T1, T2, T3, T4, T5, T6, T7)> Evaluate(Source<TToken> source)
        {
            object r = null;
            if ((r = R1.Parse(source)) is Success<TToken, T1> s1)
                if ((r = R2.Parse(s1.Remaining)) is Success<TToken, T2> s2)
                    if ((r = R3.Parse(s2.Remaining)) is Success<TToken, T3> s3)
                        if ((r = R4.Parse(s3.Remaining)) is Success<TToken, T4> s4)
                            if ((r = R5.Parse(s4.Remaining)) is Success<TToken, T5> s5)
                                if ((r = R6.Parse(s5.Remaining)) is Success<TToken, T6> s6)
                                    if ((r = R7.Parse(s6.Remaining)) is Success<TToken, T7> s7)
                                        return new Success<TToken, (T1, T2, T3, T4, T5, T6, T7)>(
                                            s7.Remaining,
                                            (s1.Value, s2.Value, s3.Value, s4.Value, s5.Value, s6.Value, s7.Value));
            var f = (IFailure<TToken>)r;
            return new Failure<TToken, (T1, T2, T3, T4, T5, T6, T7)>($"Sequence failed", f.Remaining, f);
        }
    }

    public class Sequence<TToken, T1, T2, T3, T4, T5, T6, T7, T8> : Rule<TToken, (T1, T2, T3, T4, T5, T6, T7, T8)>
    {
        public Rule<TToken, T1> R1;
        public Rule<TToken, T2> R2;
        public Rule<TToken, T3> R3;
        public Rule<TToken, T4> R4;
        public Rule<TToken, T5> R5;
        public Rule<TToken, T6> R6;
        public Rule<TToken, T7> R7;
        public Rule<TToken, T8> R8;

        public override string Name => $"Sequence TToken -> [{string.Join(", ", GetType().GenericTypeArguments.Skip(1))}]";

        public Sequence(Rule<TToken, T1> r1, Rule<TToken, T2> r2, Rule<TToken, T3> r3, Rule<TToken, T4> r4, Rule<TToken, T5> r5, Rule<TToken, T6> r6, Rule<TToken, T7> r7, Rule<TToken, T8> r8, [CallerMemberName] string location = null) : base(location)
        {
            R1 = r1;
            R2 = r2;
            R3 = r3;
            R4 = r4;
            R5 = r5;
            R6 = r6;
            R7 = r7;
            R8 = r8;
        }

        protected override Result<TToken, (T1, T2, T3, T4, T5, T6, T7, T8)> Evaluate(Source<TToken> source)
        {
            object r = null;
            if ((r = R1.Parse(source)) is Success<TToken, T1> s1)
                if ((r = R2.Parse(s1.Remaining)) is Success<TToken, T2> s2)
                    if ((r = R3.Parse(s2.Remaining)) is Success<TToken, T3> s3)
                        if ((r = R4.Parse(s3.Remaining)) is Success<TToken, T4> s4)
                            if ((r = R5.Parse(s4.Remaining)) is Success<TToken, T5> s5)
                                if ((r = R6.Parse(s5.Remaining)) is Success<TToken, T6> s6)
                                    if ((r = R7.Parse(s6.Remaining)) is Success<TToken, T7> s7)
                                        if ((r = R8.Parse(s7.Remaining)) is Success<TToken, T8> s8)
                                            return new Success<TToken, (T1, T2, T3, T4, T5, T6, T7, T8)>(
                                                s8.Remaining,
                                                (s1.Value, s2.Value, s3.Value, s4.Value, s5.Value, s6.Value, s7.Value, s8.Value));
            var f = (IFailure<TToken>)r;
            return new Failure<TToken, (T1, T2, T3, T4, T5, T6, T7, T8)>($"Sequence failed", f.Remaining, f);
        }
    }

    public class Sequence<TToken, T1, T2, T3, T4, T5, T6, T7, T8, T9> : Rule<TToken, (T1, T2, T3, T4, T5, T6, T7, T8, T9)>
    {
        public Rule<TToken, T1> R1;
        public Rule<TToken, T2> R2;
        public Rule<TToken, T3> R3;
        public Rule<TToken, T4> R4;
        public Rule<TToken, T5> R5;
        public Rule<TToken, T6> R6;
        public Rule<TToken, T7> R7;
        public Rule<TToken, T8> R8;
        public Rule<TToken, T9> R9;

        public override string Name => $"Sequence TToken -> [{string.Join(", ", GetType().GenericTypeArguments.Skip(1))}]";

        public Sequence(Rule<TToken, T1> r1, Rule<TToken, T2> r2, Rule<TToken, T3> r3, Rule<TToken, T4> r4, Rule<TToken, T5> r5, Rule<TToken, T6> r6, Rule<TToken, T7> r7, Rule<TToken, T8> r8, Rule<TToken, T9> r9, [CallerMemberName] string location = null) : base(location)
        {
            R1 = r1;
            R2 = r2;
            R3 = r3;
            R4 = r4;
            R5 = r5;
            R6 = r6;
            R7 = r7;
            R8 = r8;
            R9 = r9;
        }

        protected override Result<TToken, (T1, T2, T3, T4, T5, T6, T7, T8, T9)> Evaluate(Source<TToken> source)
        {
            object r = null;
            if ((r = R1.Parse(source)) is Success<TToken, T1> s1)
                if ((r = R2.Parse(s1.Remaining)) is Success<TToken, T2> s2)
                    if ((r = R3.Parse(s2.Remaining)) is Success<TToken, T3> s3)
                        if ((r = R4.Parse(s3.Remaining)) is Success<TToken, T4> s4)
                            if ((r = R5.Parse(s4.Remaining)) is Success<TToken, T5> s5)
                                if ((r = R6.Parse(s5.Remaining)) is Success<TToken, T6> s6)
                                    if ((r = R7.Parse(s6.Remaining)) is Success<TToken, T7> s7)
                                        if ((r = R8.Parse(s7.Remaining)) is Success<TToken, T8> s8)
                                            if ((r = R9.Parse(s8.Remaining)) is Success<TToken, T9> s9)
                                                return new Success<TToken, (T1, T2, T3, T4, T5, T6, T7, T8, T9)>(
                                                    s9.Remaining,
                                                    (s1.Value, s2.Value, s3.Value, s4.Value, s5.Value, s6.Value, s7.Value, s8.Value, s9.Value));
            var f = (IFailure<TToken>)r;
            return new Failure<TToken, (T1, T2, T3, T4, T5, T6, T7, T8, T9)>($"Sequence failed", f.Remaining, f);
        }
    }
}
