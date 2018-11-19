using System;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public static class Extensions
    {        
        public static Rule<TIn, TOut> AtLocation<TIn, TOut>(this Rule<TIn, TOut> rule, string location)
        {
            rule.Location = location;
            return rule;
        }

        public static Rule<TIn, TOut> Or<TIn, TOut>(this Rule<TIn, TOut> a, Rule<TIn, TOut> b, [CallerMemberName] string location = null)
            => new FirstOf<TIn, TOut>(a, b).AtLocation(location);

        public static Rule<TIn, T2> ThenWithLast<TIn, T1, T2>(this Rule<TIn, T1> a, Rule<TIn, T2> b, [CallerMemberName] string location = null)
            => new Select<TIn, (T1, T2), T2>(new Sequence<TIn, T1, T2>(a, b), ab => ab.Item2, location: location);

        public static Rule<TIn, T1> ThenWithFirst<TIn, T1, T2>(this Rule<TIn, T1> a, Rule<TIn, T2> b, [CallerMemberName] string location = null)
            => new Select<TIn, (T1, T2), T1>(new Sequence<TIn, T1, T2>(a, b), ab => ab.Item1, location: location);

        public static Sequence<TIn, T1, T2> Then<TIn, T1, T2>(this Rule<TIn, T1> a, Rule<TIn, T2> b, [CallerMemberName] string location = null)
            => new Sequence<TIn, T1, T2>(a, b, location: location);

        public static Sequence<TIn, T1, T2, T3> Then<TIn, T1, T2, T3>(this Sequence<TIn, T1, T2> a, Rule<TIn, T3> b, [CallerMemberName] string location = null)
            => new Sequence<TIn, T1, T2, T3>(a.R1, a.R2, b, location: location);

        public static Sequence<TIn, T1, T2, T3, T4> Then<TIn, T1, T2, T3, T4>(this Sequence<TIn, T1, T2, T3> a, Rule<TIn, T4> b, [CallerMemberName] string location = null)
            => new Sequence<TIn, T1, T2, T3, T4>(a.R1, a.R2, a.R3, b, location: location);

        public static Sequence<TIn, T1, T2, T3, T4, T5> Then<TIn, T1, T2, T3, T4, T5>(this Sequence<TIn, T1, T2, T3, T4> a, Rule<TIn, T5> b, [CallerMemberName] string location = null)
            => new Sequence<TIn, T1, T2, T3, T4, T5>(a.R1, a.R2, a.R3, a.R4, b, location: location);

        public static Rule<TIn, TOut> Select<TIn, TFrom, TOut>(this Rule<TIn, TFrom> rule, Func<TFrom, TOut> factory, [CallerMemberName] string location = null)
            => new Select<TIn, TFrom, TOut>(rule, factory, location: location);

        public static Rule<TIn, int> Many<TIn, TOut>(this Rule<TIn, TOut> rule, [CallerMemberName] string location = null)
            => new ZeroOrMore<TIn, TOut>(rule, location: location);

        public static Rule<TIn, int> AtLeastOnce<TIn, TOut>(this Rule<TIn, TOut> rule, [CallerMemberName] string location = null)
            => new OneOrMore<TIn, TOut>(rule, location: location);
    }
}
