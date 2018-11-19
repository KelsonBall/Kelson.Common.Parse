namespace Kelson.Common.Parse
{
    public readonly struct TokenRef<TToken>
    {
        public readonly TToken[] Source;
        public readonly TToken Token;
        public readonly int Index;

        internal TokenRef(TToken[] source, int index)
        {
            Source = source;
            Token = source[index];
            Index = index;
        }
    }
}
