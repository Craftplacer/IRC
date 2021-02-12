using System;

namespace Craftplacer.IRC
{
    internal static class ServerReply_Classifications
    {
        // https://tools.ietf.org/html/rfc1459.html#section-6.3
        internal const string _obsolete = "This numeric is either no longer in use, reserved or not a generic feature.";
    }

    public enum ServerReply : ushort
    {
        RPL_WELCOME = 001,
        RPL_YOURHOST = 002,
        RPL_CREATED = 003,
        RPL_MYINFO = 004,
        RPL_ISUPPORT = 005,
        RPL_BOUNCE = 010,
        /* No codes between 006-208 */

        // The RPL_TRACE* are all returned by the server in response to the TRACE message.
        // How many are returned is dependent on the the TRACE message and whether it was sent by an operator or not.
        // There is no predefined order for which occurs first.
        // Replies RPL_TRACEUNKNOWN, RPL_TRACECONNECTING and RPL_TRACEHANDSHAKE are all used for connections which have not been fully established and are either unknown, still attempting to connect or in the process of completing the 'server handshake'.
        // RPL_TRACELINK is sent by any server which handles a TRACE message and has to pass it on to another server.
        // The list of RPL_TRACELINKs sent in response to a TRACE command traversing the IRC network should reflect the actual connectivity of the servers themselves along that path.
        // RPL_TRACENEWTYPE is to be used for any connection which does not fit in the other categories but is being displayed anyway.
        RPL_TRACELINK = 200,

        RPL_TRACECONNECTING = 201,
        RPL_TRACEHANDSHAKE = 202,
        RPL_TRACEUNKNOWN = 203,
        RPL_TRACEOPERATOR = 204,
        RPL_TRACEUSER = 205,
        RPL_TRACESERVER = 206,
        /* ... */
        RPL_TRACENEWTYPE = 208,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_TRACECLASS = 209,

        RPL_STATSLINKINFO = 211,
        RPL_STATSCOMMANDS = 212,

        #region Obsolete line stats 213 - 218

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSCLINE = 213,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSNLINE = 214,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSILINE = 215,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSKLINE = 216,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSQLINE = 217,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSYLINE = 218,

        #endregion Obsolete line stats 213 - 218

        RPL_ENDOFSTATS = 219,

        /* No codes between 210-220 */
        RPL_UMODEIS = 221,

        #region Obsolete services 231 - 235

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_SERVICEINFO = 231,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_ENDOFSERVICES = 232,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_SERVICE = 233,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_SERVLIST = 234,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_SERVLISTEND = 235,

        #endregion Obsolete services 231 - 235

        #region Obsolete line stats 240 - 241

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSVLINE = 240,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSLLINE = 241,

        #endregion Obsolete line stats 240 - 241

        RPL_STATSUPTIME = 242,
        RPL_STATSOLINE = 243,

        #region Obsolete line stats 244 - 250

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSHLINE = 244,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSSLINE = 245,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSPING = 246,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSBLINE = 247,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_STATSDLINE = 250,

        #endregion Obsolete line stats 244 - 250

        RPL_LUSERCLIENT = 251,
        RPL_LUSEROP = 252,
        RPL_LUSERUNKNOWN = 253,
        RPL_LUSERCHANNELS = 254,
        RPL_LUSERME = 255,
        RPL_ADMINME = 256,
        RPL_ADMINLOC1 = 257,
        RPL_ADMINLOC2 = 258,
        RPL_ADMINEMAIL = 259,
        /* ... */
        RPL_TRACELOG = 261,
        RPL_TRYAGAIN = 263,
        /* ... */
        RPL_LOCALUSERS = 265,
        RPL_GLOBALUSERS = 266,
        /* ... */
        RPL_WHOISCERTFP = 276,
        /* ... */

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_NONE = 300,

        RPL_AWAY = 301,
        RPL_USERHOST = 302,
        RPL_ISON = 303,
        RPL_UNAWAY = 305,
        RPL_NOWAWAY = 306,
        /* ... */
        RPL_WHOISUSER = 311,
        RPL_WHOISSERVER = 312,
        RPL_WHOISOPERATOR = 313,
        RPL_WHOWASUSER = 314,
        RPL_ENDOFWHO = 315,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_WHOISCHANOP = 316,

        RPL_WHOISIDLE = 317,
        RPL_ENDOFWHOIS = 318,
        RPL_WHOISCHANNELS = 319,
        /* ... */
        RPL_LISTSTART = 321,
        RPL_LIST = 322,
        RPL_LISTEND = 323,
        RPL_CHANNELMODEIS = 324,
        RPL_UNIQOPIS = 325,

        /* ... */
        RPL_CREATIONTIME = 329,
        /* ... */
        RPL_NOTOPIC = 331,
        RPL_TOPIC = 332,
        RPL_TOPICWHOTIME = 333,
        RPL_INVITING = 341,
        RPL_SUMMONING = 342,
        /* ... */
        RPL_INVITELIST = 346,
        RPL_ENDOFINVITELIST = 347,
        RPL_EXCEPTLIST = 348,
        RPL_ENDOFEXCEPTLIST = 349,
        /* ... */
        RPL_VERSION = 351,
        RPL_WHOREPLY = 352,
        RPL_NAMREPLY = 353,
        /* ... */

        #region Obsolete closes and kills 361 - 363

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_KILLDONE = 361,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_CLOSING = 362,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_CLOSEEND = 363,

        #endregion Obsolete closes and kills 361 - 363

        RPL_LINKS = 364,
        RPL_ENDOFLINKS = 365,
        RPL_ENDOFNAMES = 366,
        RPL_BANLIST = 367,
        RPL_ENDOFBANLIST = 368,
        RPL_ENDOFWHOWAS = 369,
        /* ... */
        RPL_INFO = 371,
        RPL_MOTD = 372,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_INFOSTART = 373,

        RPL_ENDOFINFO = 374,
        RPL_MOTDSTART = 375,
        RPL_MOTDEND = 376,
        /* ... */
        RPL_YOUREOPER = 381,
        RPL_REHASHING = 382,
        RPL_YOURESERVICE = 383,

        [Obsolete(ServerReply_Classifications._obsolete)]
        RPL_MYPORTIS = 384,

        /* ... */

        /// <summary>
        /// When replying to the TIME message, a server must send the reply using the RPL_TIME format above.The  showing the time need only contain the correct day and time there.There is no further requirement for the time string.
        /// </summary>
        RPL_TIME = 391,

        RPL_USERSSTART = 392,
        RPL_USERS = 393,
        RPL_ENDOFUSERS = 394,

        /// <summary>
        /// If the USERS message is handled by a server, the replies RPL_USERSTART, RPL_USERS, RPL_ENDOFUSERS and RPL_NOUSERS are used.  RPL_USERSSTART must be sent first, following by either a sequence of RPL_USERS or a single RPL_NOUSER.  Following this is RPL_ENDOFUSERS.
        /// </summary>
        RPL_NOUSERS = 395,

        ERR_UNKNOWNERROR = 400,
        ERR_NOSUCHNICK = 401,
        ERR_NOSUCHSERVER = 402,
        ERR_NOSUCHCHANNEL = 403,
        ERR_CANNOTSENDTOCHAN = 404,
        ERR_TOOMANYCHANNELS = 405,
        ERR_WASNOSUCHNICK = 406,
        ERR_TOOMANYTARGETS = 407,

        /// <summary>
        /// Returned to a client which is attempting to send a SQUERY to a service which does not exist.
        /// </summary>
        ERR_NOSUCHSERVICE = 408,

        /// <summary>
        /// PING or PONG message missing the originator parameter.
        /// </summary>
        ERR_NOORIGIN = 409,

        /* ... */
        ERR_NORECIPIENT = 411,

        // 412 - 415 are returned by PRIVMSG to indicate that
        // the message wasn't delivered for some reason.
        // ERR_NOTOPLEVEL and ERR_WILDTOPLEVEL are errors that
        // are returned when an invalid use of
        // "PRIVMSG $<server>" or "PRIVMSG #<host>" is attempted.

        ERR_NOTEXTTOSEND = 412,
        ERR_NOTOPLEVEL = 413,
        ERR_WILDTOPLEVEL = 414,
        ERR_BADMASK = 415,
        /* ... */
        ERR_UNKNOWNCOMMAND = 421,
        ERR_NOMOTD = 422,
        ERR_NOADMININFO = 423,
        ERR_FILEERROR = 424,
        /* No codes between 425-430 */
        ERR_NONICKNAMEGIVEN = 431,
        ERR_ERRONEUSNICKNAME = 432,
        ERR_NICKNAMEINUSE = 433,
        /* No codes between 434-435 */
        ERR_NICKCOLLISION = 436,
        ERR_UNAVAILRESOURCE = 437,
        /* No codes between 438-440 */
        ERR_USERNOTINCHANNEL = 441,
        ERR_NOTONCHANNEL = 442,
        ERR_USERONCHANNEL = 443,

        /// <summary>
        /// Returned by the summon after a SUMMON command for a user was unable to be performed since they were not logged in.
        /// </summary>
        ERR_NOLOGIN = 444,

        ERR_SUMMONDISABLED = 445,
        ERR_USERSDISABLED = 446,
        ERR_NOTREGISTERED = 451,
        /* No codes between 452-460 */
        ERR_NEEDMOREPARAMS = 461,
        ERR_ALREADYREGISTERED = 462,
        ERR_NOPERMFORHOST = 463,
        ERR_PASSWORDMISMATCH = 464,

        /// <summary>
        /// Returned after an attempt to connect and register yourself with a server which has been setup to explicitly deny connections to you.
        /// </summary>
        ERR_YOUREBANNEDCREEP = 465,

        [Obsolete(ServerReply_Classifications._obsolete)]
        ERR_YOUWILLBEBANNED = 466,

        ERR_KEYSET = 467,
        /* ... */
        ERR_CHANNELISFULL = 471,
        ERR_UNKNOWNMODE = 472,
        ERR_INVITEONLYCHAN = 473,
        ERR_BANNEDFROMCHAN = 474,
        ERR_BADCHANNELKEY = 475,

        [Obsolete(ServerReply_Classifications._obsolete)]
        ERR_BADCHANMASK = 476,

        ERR_NOCHANMODES = 477,
        ERR_BANLISTFULL = 478,

        /// <summary>
        /// Any command requiring operator privileges to operate MUST return this error to indicate the attempt was unsuccessful.
        /// </summary>
        ERR_NOPRIVILEGES = 481,

        /// <summary>
        /// Any command requiring 'chanop' privileges (such as MODE messages) MUST return this error if the client making the attempt is not a chanop on the specified channel.
        /// </summary>
        ERR_CHANOPRIVSNEEDED = 482,

        /// <summary>
        /// Any attempts to use the KILL command on a server are to be refused and this error returned directly to the client.
        /// </summary>
        ERR_CANTKILLSERVER = 483,

        /// <summary>
        /// Sent by the server to a user upon connection to indicate the restricted nature of the connection (user mode "+r").
        /// </summary>
        ERR_RESTRICTED = 484,

        /// <summary>
        /// Any MODE requiring "channel creator" privileges MUST return this error if the client making the attempt is not a chanop on the specified channel.
        /// </summary>
        ERR_UNIQOPPRIVSNEEDED = 485,

        /* ... */
        ERR_NOOPERHOST = 491,

        [Obsolete(ServerReply_Classifications._obsolete)]
        ERR_NOSERVICEHOST = 492,

        ERR_UMODEUNKNOWNFLAG = 491,
        ERR_USERSDONTMATCH = 502,
    }
}