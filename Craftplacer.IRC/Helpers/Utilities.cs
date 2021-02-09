using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Craftplacer.IRC.Helpers
{
    public static class Utilities
    {
        internal static (string Nickname, string Username, string Host) ExtractHostmask(string hostmask) => throw new NotImplementedException();
    
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