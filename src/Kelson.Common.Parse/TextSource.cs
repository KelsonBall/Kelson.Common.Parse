using System;
using System.Collections.Generic;

namespace Kelson.Common.Parse
{
    public class TextSource
    {
        public readonly ReadOnlyMemory<char> Text;
        private readonly SortedList<int, int> LineIndecies = new SortedList<int, int>();

        public IEnumerable<TextPosition> Positions()
        {                        
            int line = 1;
            int column = 1;

            for (int index = 0; index < Text.Length; index++)
            {
                var character = Text.Span[index];
                if (character == '\n')
                {
                    line++;
                    column = 1;
                }

                yield return new TextPosition(character, index, line, column);

                column++;
            }
        }
    }
}
