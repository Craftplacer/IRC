using System.Collections.Generic;
using System.Linq;

using Craftplacer.IRC.Formatting;
using Craftplacer.IRC.Formatting.Tokens;

using Xunit;
using Xunit.Abstractions;

namespace Craftplacer.IRC.Tests
{
    public class FormattingTests
    {
        private readonly ITestOutputHelper _output;
        
        public FormattingTests(ITestOutputHelper output)
        {
            _output = output;
        }
        
        // https://modern.ircdocs.horse/formatting.html#examples
        public static IEnumerable<object[]> FormattingCases => new[]
        {
            new object[]
            {
                "I love \u00033IRC! \u0003It is the \u00037best protocol ever!",
                new IToken[]
                {
                    new PlainTextToken("I love "),
                    new FormattingColorToken(3),
                    new PlainTextToken("IRC! "),
                    new FormattingColorToken(),
                    new PlainTextToken("It is the "),
                    new FormattingColorToken(7),
                    new PlainTextToken("best protocol ever!"),
                }
            },
            new object[]
            {
                "This is a \u001D\u000313,9cool \u0003message",
                new IToken[]
                {
                    new PlainTextToken("This is a "),
                    new FormattingToggleToken(FormattingToggleTokenType.Italic),
                    new FormattingColorToken(13, 9),
                    new PlainTextToken("cool "),
                    new FormattingColorToken(),
                    new PlainTextToken("message"),
                }
            },
            new object[]
            {
                "text\u0003text",
                new IToken[]
                {
                    new PlainTextToken("text"),
                    new FormattingColorToken(),
                    new PlainTextToken("text"),
                }
            },
            new object[]
            {
                "text\u0003,text",
                new IToken[]
                {
                    new PlainTextToken("text"),
                    new FormattingColorToken(),
                    new PlainTextToken(",text"),
                }
            },
            new object[]
            {
                "text\u00030text",
                new IToken[]
                {
                    new PlainTextToken("text"),
                    new FormattingColorToken(0),
                    new PlainTextToken("text"),
                }
            },
            new object[]
            {
                "text\u00030,text",
                new IToken[]
                {
                    new PlainTextToken("text"),
                    new FormattingColorToken(0),
                    new PlainTextToken(",text"),
                }
            },
            new object[]
            {
                "text\u00030,0text",
                new IToken[]
                {
                    new PlainTextToken("text"),
                    new FormattingColorToken(0,0),
                    new PlainTextToken("text"),
                }
            },
        };

        [Theory]
        [MemberData(nameof(FormattingCases))]
        public void TestFormatting(string message, IToken[] expectedTokens)
        {
            var actual = MircParser.Deformat(message);
            
            _output.WriteLine("Message: {0}", message);
            
            _output.WriteLine(string.Empty);
            
            _output.WriteLine("Expected:");
            foreach (var token in expectedTokens)
            {
                _output.WriteLine("- '{0}' ({1})", token, token.GetType().Name);
            }
            
            _output.WriteLine("Actual:");
            foreach (var token in actual)
            {
                _output.WriteLine("- '{0}' ({1})", token, token.GetType().Name);
            }

            Assert.True(actual.SequenceEqual(expectedTokens));
        }
    }
}