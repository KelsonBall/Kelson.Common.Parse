using FluentAssertions;
using Xunit;

namespace Lua.Tests
{
    public class LuaTokenizer_Should
    {
        [Fact]
        public void CreateTokensForValidContent()
        {
            string source = @"a = 1";

            var result = LuaTokenizer.Scan(source);

            result.Values.Should().BeEquivalentTo(Tkn.Letter, Tkn.WhiteSpace, Tkn.Assign, Tkn.WhiteSpace, Tkn.Digit);

            source = @"if a == 43 then print(a) end ";

            result = LuaTokenizer.Scan(source);

            result.Values.Should().BeEquivalentTo(
                Tkn.Keyword_If, Tkn.Letter, Tkn.WhiteSpace, Tkn.Equality, Tkn.WhiteSpace, Tkn.Digit, Tkn.Digit, Tkn.WhiteSpace, Tkn.Keyword_Then,
                Tkn.Letter, Tkn.Letter, Tkn.Letter, Tkn.Letter, Tkn.Letter, Tkn.LParen, Tkn.Letter, Tkn.RParen,
                Tkn.WhiteSpace, Tkn.Keyword_End);
        }
    }
}
