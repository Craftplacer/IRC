namespace Craftplacer.IRC.Entities
{
    public class IrcNotice
    {
        public string User;
        public string Message;
        public string Source;

        public IrcNotice(string source, string user, string message)
        {
            Source = source;
            User = user;
            Message = message;
        }
    }
}