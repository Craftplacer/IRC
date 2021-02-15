namespace Craftplacer.IRC.Formatting.Tokens
{
    public class FormattingToggleToken : IToken
    {
        public FormattingToggleToken(FormattingToggleTokenType type)
        {
            Type = type;
        }

        public FormattingToggleTokenType Type { get; }

        public override bool Equals(object obj)
        {
            if (obj is FormattingToggleToken token)
            {
                return Type.Equals(token.Type);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }

        public override string ToString()
        {
            return Type.ToString();
        }
    }
}