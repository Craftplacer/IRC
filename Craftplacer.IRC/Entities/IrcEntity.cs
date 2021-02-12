namespace Craftplacer.IRC.Entities
{
    public abstract class IrcEntity
    {
        protected IrcEntity(IrcClient client)
        {
            Client = client;
        }

        protected IrcClient Client { get; }
    }
}