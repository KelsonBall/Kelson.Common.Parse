using Xunit;
using FluentAssertions;

namespace Kelson.Common.Parse.Rules.Tests
{
    public class MatchAndProduce_Should
    {
        [Fact]
        public void PassAndProduceOnMatchingToken()
        {
            var rule = new MatchTokenAndProduce<Tokens, int>(Tokens.A, 3);
            var data = TestData.FromTokens(Tokens.A);

            var result = rule.Parse(data);

            if (result is Success<Tokens, int> success)
                success.Value.Should().Be(3);
            else
                result.Should().BeAssignableTo<Success<Tokens, int>>();
        }

        [Fact]
        public void InvokeFactoryOnlyOnPass()
        {
            int count = 0;
            var rule = new MatchTokenAndProduce<Tokens, int>(Tokens.A, () => count++);

            var pass_data = TestData.FromTokens(Tokens.A);
            var fail_data = TestData.FromTokens(Tokens.B);

            var expect_fail = rule.Parse(fail_data);
            expect_fail.Should().BeAssignableTo<Failure<Tokens, int>>();
            count.Should().Be(0);
            var expect_pass = rule.Parse(pass_data);
            expect_pass.Should().BeAssignableTo<Success<Tokens, int>>();
            count.Should().Be(1);            
        }
    }
}
