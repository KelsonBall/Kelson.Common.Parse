using Xunit;
using FluentAssertions;

namespace Kelson.Common.Parse.Rules.Tests
{
    public class Select_Should
    {
        [Fact]
        public void InvokeTransformOnPass()
        {
            var rule = 
                new Select<Tokens, Tokens, string>(
                    new Match<Tokens>(Tokens.A), 
                    t => t.ToString());

            var data = TestData.FromTokens(Tokens.A);

            var result = rule.Parse(data);

            if (result is Success<Tokens, string> success)
                success.Value.Should().Be(Tokens.A.ToString());
            else
                result.Should().BeAssignableTo<Success<Tokens, string>>();
        }

        [Fact]
        public void ContainInnerFailureOnFail()
        {
            var rule =
                new Select<Tokens, Tokens, string>(
                    new Match<Tokens>(Tokens.A),
                    t => t.ToString());

            var data = TestData.FromTokens(Tokens.B);

            var result = rule.Parse(data);

            if (result is Failure<Tokens, string> failure)
                failure.InnerFailures.Should().Contain(f => f.Message == $"Expected {Tokens.A} but found {Tokens.B}");
            else
                result.Should().BeAssignableTo<Failure<Tokens, string>>();
        }
    }
}
