using System.Collections.Generic;

using Craftplacer.IRC.Helpers;

using Xunit;

namespace Craftplacer.IRC.Tests
{
    public class Tests
    {
        public static IEnumerable<object[]> HostmaskCases => new[]
        {
            new object[]
            {
                "Nick!User@Host",
                ("Nick", "User", "Host")
            },
        };

        [Theory]
        [MemberData(nameof(HostmaskCases))]
        public void TestHostmaskParsing(string hostmask, (string, string, string) expected)
        {
            var actual = Utilities.ExtractHostmask(hostmask);

            Assert.Equal(expected, actual);
        }
    }
}