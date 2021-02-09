using System.Diagnostics.CodeAnalysis;

using Craftplacer.IRC.Entities;

namespace Craftplacer.IRC.Events
{
    public class NoticeReceivedEventArgs : IrcEventArgs
    {
        public NoticeReceivedEventArgs([NotNull] IrcClient client, [NotNull] IrcNotice notice) : base(client)
        {
            Notice = notice;
        }
        
        public IrcNotice Notice { get; }
    }
}