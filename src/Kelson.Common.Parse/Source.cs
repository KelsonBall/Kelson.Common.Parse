using System;
using System.Collections;
using System.Collections.Generic;

namespace Kelson.Common.Parse
{
    public readonly struct Source<TToken> : IEnumerable<TToken>
    {
        public readonly string RuleName;
        public readonly string RuleLocation;
        
        public readonly bool IsAtEnd;
        public readonly int Index;
        public readonly int AbsoluteIndex;

        internal TokenList<TToken> List { get; }

        public Source(TokenList<TToken> list) : this(list, index: 0) { }

        internal Source(TokenList<TToken> list, int index = 0, string ruleName = "none", string location = "none")
        {
            Index = index;
            AbsoluteIndex = Index + list.Start;
            List = list;
            IsAtEnd = Index == list.Length;
            RuleName = ruleName;
            RuleLocation = location;
        }

        public TToken Peek() => List[Index];

        public Source<TToken> Next() => 
            (!IsAtEnd) 
            ? new Source<TToken>(List, Index + 1, ruleName: RuleName, location: RuleLocation) 
            : throw new IndexOutOfRangeException();        

        public Source<TToken> InRule<TOut>(Rule<TToken, TOut> rule)
            => new Source<TToken>(List, index: Index, ruleName: rule.Name, location: rule.Location);

        public IEnumerator<TToken> GetEnumerator()
        {            
            IEnumerable<TToken> enumerate(Source<TToken> local)
            {
                for (int i = local.Index; i < local.List.Length; i++)
                    yield return local.List[i];
            }

            return enumerate(this).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
