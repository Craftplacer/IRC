﻿namespace Craftplacer.IRC
{
    public enum ServerReply
    {
        RPL_BOUNCE = 010,
        RPL_UMODEIS = 221,
        RPL_LUSERCLIENT = 251,
        RPL_LUSEROP = 252,
        RPL_LUSERUNKNOWN = 253,
        RPL_LUSERCHANNELS = 254,
        RPL_LUSERME = 255,
        RPL_ADMINME = 256,
        RPL_ADMINLOC1 = 257,
        RPL_ADMINLOC2 = 258,
        RPL_ADMINEMAIL = 259,
        RPL_TRYAGAIN = 263,
        RPL_LOCALUSERS = 265,
        RPL_GLOBALUSERS = 266,
        RPL_WHOISCERTFP = 276,
        RPL_NONE = 300,
        RPL_AWAY = 301,
        RPL_USERHOST = 302,
        RPL_ISON = 303,
        RPL_UNAWAY = 305,
        RPL_NOWAWAY = 306,
        RPL_WHOISUSER = 311,
        RPL_WHOISSERVER = 312,
        RPL_WHOISOPERATOR = 313,
        RPL_WHOWASUSER = 314,
        RPL_WHOISIDLE = 317,
        RPL_ENDOFWHOIS = 318,
        RPL_WHOISCHANNELS = 319,
        RPL_LISTSTART = 312,
        RPL_LIST = 322,
        RPL_LISTEND = 323,
        RPL_CHANNELMODEIS = 324,
        RPL_CREATIONTIME = 329,
        RPL_NOTOPIC = 331,
        RPL_TOPIC = 332,
        RPL_TOPICWHOTIME = 333,
        RPL_INVITING = 341,
        RPL_INVITELIST = 346,
        RPL_ENDOFINVITELIST = 347,
        RPL_EXCEPTLIST = 348,
        RPL_ENDOFEXCEPTLIST = 349,
        RPL_VERSION = 351,
        RPL_NAMREPLY = 353,
        RPL_ENDOFNAMES = 366,
        RPL_BANLIST = 367,
        RPL_ENDOFBANLIST = 368,
        RPL_ENDOFWHOWAS = 369,
        RPL_MOTD = 372,
        RPL_MOTDSTART = 375,
        RPL_MOTDEND = 376,
        RPL_YOUREOPER = 381,
        RPL_REHASHING = 382,
        ERR_UNKNOWNERROR = 400,
        /* ... */
        ERR_NONICKNAMEGIVEN = 431,
        ERR_ERRONEUSNICKNAME = 432,
        ERR_NICKNAMEINUSE = 433,
        ERR_NICKCOLLISION = 436,
    }
}
