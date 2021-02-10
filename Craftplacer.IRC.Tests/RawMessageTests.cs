using System.Collections.Generic;

using Craftplacer.IRC.Raw.Messages;

using Xunit;

namespace Craftplacer.IRC.Tests
{
    public class RawMessageTests
    {
        public static IEnumerable<object[]> RawMessageCases => new object[][]
        {
            new object[]
            {
                ":irc.example.com CAP * LIST :",
                new RawMessage("CAP",
                               source: "irc.example.com",
                               parameters: new string[]{ "*", "LIST", "" },
                               tags: null),
            },
            new object[]
            {
                "CAP * LS :multi-prefix sasl",
                new RawMessage("CAP",
                               source: null,
                               parameters: new string[]{ "*", "LS", "multi-prefix sasl" },
                               tags: null)
            },
            new object[]
            {
                "CAP REQ :sasl message-tags foo",
                new RawMessage("CAP",
                               source: null,
                               parameters: new string[]{ "REQ", "sasl message-tags foo" },
                               tags: null),
            },
            new object[]
            {
                ":dan!d@localhost PRIVMSG #chan Hey!",
                new RawMessage("PRIVMSG",
                               source: "dan!d@localhost",
                               parameters: new string[]{ "#chan", "Hey!" },
                               tags: null)
            },
            new object[]
            {
                ":Macha!~macha@unaffiliated/macha PRIVMSG #botwar :Test response",
                new RawMessage("PRIVMSG",
                               source: "Macha!~macha@unaffiliated/macha",
                               parameters: new string[]{ "#botwar", "Test response" },
                               tags: null)
            },
            new object[]
            {
                "@id=234AB :dan!d@localhost PRIVMSG #chan :Hey what's up!",
                new RawMessage("PRIVMSG",
                               source: "dan!d@localhost",
                               parameters: new string[]{ "#chan", "Hey what's up!" },
                               tags: new Dictionary<string, string> { { "id", "234AB" } })
            }
        };

        [Theory]
        [MemberData(nameof(RawMessageCases))]
        public void TestParse(string raw, RawMessage expected)
        {
            var actual = RawMessage.Parse(raw);
            Assert.Equal(expected, actual);
        }

        [Theory]
        [MemberData(nameof(RawMessageCases))]
        public void TestSerialization(string expected, RawMessage message)
        {
            var actual = message.ToString();
            Assert.Equal(expected, actual);
        }
    }
}