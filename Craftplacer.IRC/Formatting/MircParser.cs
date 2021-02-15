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
        public static IToken[] Deformat(string message)
        {
            var list = new List<IToken>();

            var textBuffer = new StringBuilder();

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
                switch (@char)
                {
                    case '\x02': // bold
                    {
                        FinishTextToken();
                        list.Add(new FormattingToggleToken(FormattingToggleTokenType.Bold));
                        break;
                    }
                    case '\x1D': // italic
                    {
                        FinishTextToken();
                        list.Add(new FormattingToggleToken(FormattingToggleTokenType.Italic));
                        break;
                    }
                    case '\x1f': // underline
                    {
                        FinishTextToken();
                        list.Add(new FormattingToggleToken(FormattingToggleTokenType.Underline));
                        break;
                    }
                    case '\x1e': // strikethrough
                    {
                        FinishTextToken();
                        list.Add(new FormattingToggleToken(FormattingToggleTokenType.Strikethrough));
                        break;
                    }
                    case '\x11': // monospace
                    {
                        FinishTextToken();
                        list.Add(new FormattingToggleToken(FormattingToggleTokenType.Monospace));
                        break;
                    }
                    case '\x0F':
                    {
                        FinishTextToken();
                        list.Add(new ResetFormattingToken());
                        break;
                    }
                    case '\x03': // color
                    {
                        FinishTextToken();
                        
                        i++;
                        
                        byte? ReadDigits()
                        {
                            var digitStartI = i;
                            
                            for (var j = 0; j < 2; j++, i++)
                            {
                                if (!char.IsDigit(message[i]))
                                    break;
                            }

                            var diff = i - digitStartI;
                            if (diff == 0)
                                return null;

                            var digitsSubstring = message[digitStartI..i];
                            return byte.Parse(digitsSubstring);
                        }

                        byte? fg = ReadDigits();
                        byte? bg = null;

                        if (fg != null)
                        {
                            if (message[i] == ',')
                            {
                                i++;

                                bg = ReadDigits();

                                if (bg == null)
                                {
                                    i--;
                                }

                                i--;
                            }
                            else
                            {
                                i--;
                            }
                        }
                        else
                        {
                            
                            i--;
                        }
                            

                        list.Add(new FormattingColorToken(fg, bg));

                        break;
                    }
                    default:
                    {
                        textBuffer.Append(@char);
                        break;
                    }
                }
            }

            FinishTextToken();

            return list.ToArray();
        }
    }
}