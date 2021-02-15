namespace Craftplacer.IRC.Formatting.Tokens
{
    public class ResetFormattingToken : IToken
    {
        public override bool Equals(object obj)
        {
            return obj is ResetFormattingToken;
        }

        public override int GetHashCode()
        {
            return int.MinValue;
        }
    }
}