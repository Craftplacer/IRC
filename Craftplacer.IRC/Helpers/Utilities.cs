using System.Collections.Generic;
using System.Linq;

namespace Craftplacer.IRC.Helpers
{
    public static class Utilities
    {
        public static (string Nickname, string Username, string Host) ExtractHostmask(string hostmask)
        {
            var hostSplit = hostmask.Split('@', 2);
            var host = hostSplit[1];
            var userSplit = hostSplit[0].Split('!', 2);
            return (userSplit[0], userSplit[1], host);
        }

        public static bool SafeSequenceEqual<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            if (a == null)
            {
                return b == null;
            }

            if (b == null)
            {
                return false;
            }

            return a.SequenceEqual(b);
        }
    }
}