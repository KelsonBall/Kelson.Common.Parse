using System;
using System.Collections.Generic;
using System.Linq;

namespace Kelson.Common.Parse
{
    public readonly struct TextPosition
    {
        public readonly char Character;
        public readonly int Index;
        public readonly int Length;
        public readonly int Line;
        public readonly int Column;

        public TextPosition(char character, int index, int length, int line, int column) =>
            (Character, Index, Length, Line, Column) = (character, index, length, line, column);
        
        public static TextPosition Union(params TextPosition[] positions)
        {
            TextPosition first = positions.First();
            TextPosition last = first;
            int length = last.Length;
            foreach (var position in positions.Skip(1))
            {
                if (position.Index == last.Index + last.Length)
                {
                    last = position;
                    length += position.Length;
                }
                else
                    throw new InvalidOperationException("Cannot union non consecutive text positions");
            }

            return new TextPosition(first.Character, first.Index, length, first.Length, first.Column);
        }

        public override string ToString() => 
            Length == 1 
            ? $"Character {Character} at Line: {Line}, Column: {Column} ({Index})"
            : $"Text starting with {Character} at Line: {Line}, Column: {Column} ({Index})";
    }    
}
