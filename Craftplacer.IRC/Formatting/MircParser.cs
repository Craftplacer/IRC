using System;
using System.Collections.Generic;
using System.Text;

using Craftplacer.IRC.Formatting.Tokens;

namespace Craftplacer.IRC.Formatting
{
    /// <summary>
    /// Generates tokens from mIRC-style formatted text.
    /// </summary>
    public static class MircParser
    {
        private static readonly Dictionary<char, Func<IToken>> TokenDictionary = new Dictionary<char, Func<IToken>>
        {
            {'\x02', () => new FormattingToggleToken(FormattingToggleTokenType.Bold) },
            {'\x1D', () => new FormattingToggleToken(FormattingToggleTokenType.Italic) },
            {'\x1E', () => new FormattingToggleToken(FormattingToggleTokenType.Strikethrough) },
            {'\x1F', () => new FormattingToggleToken(FormattingToggleTokenType.Underline) },
            {'\x11', () => new FormattingToggleToken(FormattingToggleTokenType.Monospace) },
            {'\x0F', () => new ResetFormattingToken() }
        };
        
        public static IToken[] Deformat(string message)
        {
            var list = new List<IToken>();
            var textBuffer = new StringBuilder();

            // Turns text left inside the text buffer into a plain text token
            void FinishTextToken()
            {
                if (textBuffer.Length == 0)
                    return;
                
                list.Add(new PlainTextToken(textBuffer.ToString()));
                textBuffer.Clear();
            }

            for (var i = 0; i < message.Length; i++)
            {
                // This caused my machine to half-way lock up because .NET was keep adding tokens to a list
                // because of a faulty index decrease.
                if (list.Count >= message.Length)
                {
                    throw new NotImplementedException("The mIRC parsing code seems to be looping or to be over the expected amount of tokens.");
                }
            
                var @char = message[i];
                if (TokenDictionary.TryGetValue(@char, out var constructor))
                {
                    FinishTextToken();
                    list.Add(constructor.Invoke());
                }
                else if (@char == '\x03')
                {
                    FinishTextToken();
                    list.Add(ParseColorCode(message, ref i));
                }
                else
                {
                    textBuffer.Append(@char);
                }
            }

            // finish up
            FinishTextToken();
            return list.ToArray();
        }

        private static FormattingColorToken ParseColorCode(string message, ref int i)
        {
            i++;
                    
            byte? fg;
            byte? bg = null;

            (fg, i) = ReadDigits(message, i);

            if (fg != null && message[i] == ',')
            {
                i++;

                (bg, i) = ReadDigits(message, i);

                if (bg == null)
                    i--;
            }

            i--;

            return new FormattingColorToken(fg, bg);
        }
        
        /// <summary>
        /// Reads 2 digits until reaching a non-digit.
        /// </summary>
        /// <param name="string"></param>
        /// <param name="i"></param>
        /// <returns>The digits that were read, Digits is null when none were read. Index defines the last index that was worked with.</returns>
        private static (byte? Digits, int Index) ReadDigits(string @string, int i)
        {
            var startI = i;

            while (i - startI < 2)
            {
                if (!char.IsDigit(@string[i]))
                    break;
                
                i++;
            }

            var diff = i - startI;
            if (diff == 0)
                return (null, i);

            var digitsSubstring = @string[startI..i];
            return (byte.Parse(digitsSubstring), i);
        }
    }
}