using System;
using System.Collections.Generic;
using System.Linq;

namespace Craftplacer.IRC.Helpers
{
    public static class Utilities
    {
        internal static (string Nickname, string Username, string Host) ExtractHostmask(string hostmask)
        {
            var hostSplit = hostmask.Split('@', 2);
            var host = hostSplit[1];
            var userSplit = hostSplit[0].Split('!', 2);
            return (userSplit[0], userSplit[1], host);
        }

        internal static bool SafeSequenceEqual<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            if (a == null)
            {
                return b == null;
            }
            else if (b == null)
            {
                return false;
            }
            else
            {
                return a.SequenceEqual(b);
            }
        }
    }
}