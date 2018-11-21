using System;

namespace Kelson.Common.Parse
{
    public class TokenList<TToken>
    {
        public readonly Guid Id = Guid.NewGuid();

        public readonly TToken[] Values;

        public readonly TextPosition[] Positions;

        public readonly TextSource Source;

        public int Start { get; }

        public int Length { get; }

        public TokenList<TToken> Root { get; }

        public TokenRef<TToken> this[int index]
        {
            get
            {
                if (index > Length)
                    throw new IndexOutOfRangeException();
                else
                    return new TokenRef<TToken>(Values, index + Start, Source, (index + Start < Positions.Length) ? Positions[index + Start] : default);
            }
        }

        public TokenList(TToken[] tokens)
        {
            Values = tokens;
            Start = 0;
            Length = Values.Length;
            Positions = new TextPosition[0];
            Root = this;
        }

        public TokenList(TToken[] tokens, TextPosition[] positions, TextSource source)
        {
            Values = tokens;
            Start = 0;
            Length = Values.Length;
            Positions = positions;
            Source = source;
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
