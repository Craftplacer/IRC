using System;

namespace Craftplacer.IRC.Formatting.Tokens
{
    public class PlainTextToken : IToken
    {
        public PlainTextToken(string text)
        {
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public string Text { get; }

        public override bool Equals(object obj)
        {
            if (obj is PlainTextToken token)
            {
                return Text.Equals(token.Text);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode();
        }

        public override string ToString()
        {
            return Text;
        }
    }
}