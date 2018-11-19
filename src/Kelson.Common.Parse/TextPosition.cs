namespace Kelson.Common.Parse
{
    public readonly struct TextPosition
    {
        public readonly char Character;
        public readonly int Index;
        public readonly int Line;
        public readonly int Column;

        public TextPosition(char character, int index, int line, int column) =>
            (Character, Index, Line, Column) = (character, index, line, column);

        public void Deconstruct(out char character, out int index, out int line, out int column)
        {
            character = Character;
            index = Index;
            line = Line;
            column = Column;
        }
    }
}
