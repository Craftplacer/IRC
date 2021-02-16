# Craftplacer.IRC
 
![.NET](https://github.com/Craftplacer/IRC/workflows/.NET/badge.svg)
 
Craftplacer.IRC is a modern .NET Standard 2.1 IRC library built from scratch. It aims to support [RFC 1459](https://tools.ietf.org/html/rfc1459.html), [RFC 2812](https://tools.ietf.org/html/rfc2812) and [IRCv3](https://ircv3.net/) specifications.
Unlike other IRC libraries for .NET, this one tries to stay up to date with the new conventions of both .NET and IRC.

## Example

Using Craftplacer.IRC is insanely easy.

```csharp
static IrcClient client;

static async Task MainAsync()
{
    client = new IrcClient();
    client.Welcome += Irc_Welcome;
    
    // The port is set dynamically whether if you have SSL enabled or not.
    await client.ConnectAsync("mycoolirc.net", ssl: true);
}

// Auto-join #general and send a message once the server welcomed us.
static async void Irc_Welcome(object sender, EventArgs e)
{
    await _irc.JoinChannelAsync("#general");
    await _irc.SendMessageAsync("#general", "Hello everyone!");
}
```