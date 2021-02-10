namespace Craftplacer.IRC.Entities
{
    public abstract class IrcEntity
    {
        protected IrcClient Client { get; }

        protected IrcEntity(IrcClient client) => Client = client;
    }
}