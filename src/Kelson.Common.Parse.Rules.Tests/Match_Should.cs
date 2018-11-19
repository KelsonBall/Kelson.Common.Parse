using Xunit;
using FluentAssertions;

namespace Kelson.Common.Parse.Rules.Tests
{
    public class Match_Should
    {
        [Fact]
        public void PassOnMatchingToken()
        {
            var rule = new Match<Tokens>(Tokens.A);
            var data = TestData.FromTokens(Tokens.A);
            
            var result = rule.Parse(data);

            result.Should().BeAssignableTo<Success<Tokens, Tokens>>();
        }

        [Fact]
        public void FailOnWrongToken()
        {
            var rule = new Match<Tokens>(Tokens.A);
            var data = TestData.FromTokens(Tokens.B);

            var result = rule.Parse(data);

            result.Should().BeAssignableTo<Failure<Tokens, Tokens>>();
        }
    }
}
