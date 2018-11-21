using System;
using System.Collections.Generic;

namespace Kelson.Common.Parse
{
    public class TextSource
    {
        public readonly ReadOnlyMemory<char> Data;
        private readonly SortedList<int, int> LineIndecies = new SortedList<int, int>();

        internal TextSource(char[] text)
        {
            Data = new ReadOnlyMemory<char>(text);
        }

        public IEnumerable<TextPosition> Positions()
        {                        
            int line = 1;
            int column = 1;

            for (int index = 0; index < Data.Length; index++)
            {
                var character = Data.Span[index];
                if (character == '\n')
                {
                    line++;
                    column = 1;
                    LineIndecies.Add(index, line);
                }

                yield return new TextPosition(character, index, 1, line, column);

                column++;
            }
        }
    }
}
