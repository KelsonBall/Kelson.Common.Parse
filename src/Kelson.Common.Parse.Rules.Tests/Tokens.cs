namespace Kelson.Common.Parse.Rules.Tests
{
    public static class TestData
    {
        public static Source<Tokens> FromTokens(params Tokens[] tokens)
        {
            var list = new TokenList<Tokens>(tokens);
            return new Source<Tokens>(list);
        }
    }

    public enum Tokens
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J
    }
}
