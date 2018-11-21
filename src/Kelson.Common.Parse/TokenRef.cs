using System;

namespace Kelson.Common.Parse
{
    public readonly struct TokenRef<TToken>
    {
        public readonly TextSource Text;
        public readonly TextPosition Position;

        public readonly ReadOnlyMemory<TToken> Source;
        public readonly int Index;

        public TToken Token => Source.Span[Index];

        internal TokenRef(TToken[] source, int index, TextSource text, TextPosition position)
        {
            Text = text;
            Position = position;
            Source = source;            
            Index = index;
        }

        public string ToStringValue()
        {
            return Text.Data.Slice(Position.Index, Position.Length).ToString();
        }
    }
}
