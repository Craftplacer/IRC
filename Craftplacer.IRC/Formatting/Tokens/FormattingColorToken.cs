using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Craftplacer.IRC.Formatting.Tokens
{
    public class FormattingColorToken : IToken
    {
        public FormattingColorToken(byte? fg = null, byte? bg = null)
        {
            if (fg != null && fg >= 100)
            {
                throw new ArgumentOutOfRangeException(nameof(fg));
            }
            
            ForegroundColor = fg;
            
            if (bg != null && bg >= 100)
            {
                throw new ArgumentOutOfRangeException(nameof(fg));
            }
            
            BackgroundColor = bg;
        }

        [MaybeNull]
        public byte? BackgroundColor { get; }

        [MaybeNull]
        public byte? ForegroundColor { get; }

        public bool Reset => ForegroundColor == null && BackgroundColor == null;

        public override bool Equals(object obj)
        {
            if (obj is FormattingColorToken token)
            {
                return BackgroundColor.Equals(token.BackgroundColor) &&
                       ForegroundColor.Equals(token.ForegroundColor);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ForegroundColor, BackgroundColor);
        }

        public override string ToString()
        {
            var sb = new StringBuilder("\x03", 6);

            if (ForegroundColor != null)
            {
                sb.Append(ForegroundColor.ToString());
            }
            
            if (BackgroundColor != null)
            {
                sb.Append(',');
                sb.Append(BackgroundColor.ToString());
            }

            return sb.ToString();
        }
    }
}