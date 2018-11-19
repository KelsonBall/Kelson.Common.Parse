using System;

namespace Kelson.Common.Parse
{
    public class TokenList<TToken>
    {
        public readonly Guid Id = Guid.NewGuid();

        private readonly TToken[] Values;

        public int Start { get; }

        public int Length { get; }

        public TokenList<TToken> Root { get; }

        public TToken this[int index]
        {
            get
            {
                if (index > Length)
                    throw new IndexOutOfRangeException();
                else
                    return Values[index + Start];
            }
        }

        public TokenList(TToken[] tokens)
        {
            Values = tokens;
            Start = 0;
            Length = Values.Length;
            Root = this;
        }

        internal TokenList(TokenList<TToken> root, int start = 0, int? length = null)
        {            
            Root = root;
            Start = start;
            Length = length ?? Root.Length;
            Values = root.Values;
        }
    }
}
