using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Kelson.Common.Parse.Rules
{
    public class RecursionGatedSequence<TToken, T1, T2> : Rule<TToken, (T1, T2)>
    {
        public readonly Rule<TToken, T1> Front;
        public readonly Rule<TToken, T2> Back;

        private readonly Sequence<TToken, T1, T2> sequence;

        public override string Name => $"Sequence TToken -> [{string.Join(", ", GetType().GenericTypeArguments.Skip(1))}]";

        public RecursionGatedSequence(Rule<TToken, T1> front, Rule<TToken, T2> back, [CallerMemberName] string location = null)
        {
            Front = front;
            Back = back;
            sequence = new Sequence<TToken, T1, T2>(Front, Back, location: location);
        }

        private static readonly Dictionary<(Type, Type, Type, Guid), int> IndexMap;

        protected override Result<TToken, (T1, T2)> Evaluate(Source<TToken> source)
        {
            var key = (typeof(TToken), typeof(T1), typeof(T2), source.List.Id);
            if (IndexMap.ContainsKey(key) && IndexMap[key] == source.Index)
                return new Failure<TToken, (T1, T2)>("Loop prevented by recursion gate", source);

            IndexMap[key] = source.Index;

            var result = sequence.Parse(source);

            IndexMap.Remove(key);

            return result;
        }
    }
}
