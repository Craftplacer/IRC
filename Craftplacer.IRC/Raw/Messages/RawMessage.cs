using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;

using Craftplacer.IRC.Helpers;

namespace Craftplacer.IRC.Raw.Messages
{
    // https://modern.ircdocs.horse/index.html#messages
    public class RawMessage
    {
        public RawMessage(string command, params string[] parameters) : this(command, null, parameters, null)
        {
        }

        public RawMessage(string command, string source = null, string[] parameters = null, Dictionary<string, string> tags = null)
        {
            if (string.IsNullOrWhiteSpace(command))
                throw new System.ArgumentException($"'{nameof(command)}' cannot be null or whitespace", nameof(command));

            Command = command;
            Source = source;
            Parameters = parameters;
            Tags = tags;
        }

        [NotNull]
        public string Command { get; }

        [MaybeNull]
        public string[] Parameters { get; }

        [MaybeNull]
        public Dictionary<string, string> Tags { get; }

        [MaybeNull]
        public string Source { get; }

        public static RawMessage Parse(string line)
        {
            // TODO: This *could* be better or more efficient,
            //       but it works.

            var values = line.Split(Constants.SpaceCharacter);

            string source = null;
            string command = null;
            var parameters = new List<string>(values.Length);
            Dictionary<string, string> tags = null;

            for (var i = 0; i < values.Length; i++)
            {
                var value = values[i];

                if (command == null)
                {
                    if (value[0] == Constants.TagCharacter && tags == null)
                    {
                        var pairs = value[1..].Split(Constants.TagSeparatorCharacter);
                        tags = new Dictionary<string, string>(pairs.Select(p =>
                        {
                            var split = p.Split(Constants.TagSetCharacter, 2);

                            if (split.Length == 0 || split.Length > 2)
                            {
                                throw new InvalidDataException("The message contains no or more than 2 equal signs (=) in the tag part.");
                            }

                            return new KeyValuePair<string, string>(split[0], split.Length == 2 ? split[1] : null);
                        }));
                    }
                    else if (value[0] == Constants.SourceCharacter && source == null)
                    {
                        source = value[1..];
                    }
                    else
                    {
                        command = value;
                    }
                }
                else if (value[0] == Constants.TrailingCharacter)
                {
                    // Remove : from this parameter
                    values[i] = value[1..];

                    parameters.Add(string.Join(Constants.SpaceCharacter, values.Skip(i)));

                    // It makes no sense to further parse parameters,
                    // as the : character ignores spaces and takes the rest as parameter.
                    break;
                }
                else
                {
                    parameters.Add(value);
                }
            }

            return new RawMessage(command, source, parameters.ToArray(), tags);
        }

        public override string ToString()
        {
            var values = new List<string>();

            if (Tags != null)
            {
                // Maybe use more pre-existing methods and less logic based code.
                var tagValues = Tags.Select(t =>
                {
                    if (t.Value == null)
                    {
                        return t.Key;
                    }
                    else
                    {
                        return t.Key + Constants.TagSetCharacter + t.Value;
                    }
                });

                values.Add(Constants.TagCharacter + string.Join(Constants.TagSeparatorCharacter, tagValues));
            }

            if (Source != null)
            {
                values.Add(Constants.SourceCharacter + Source);
            }

            if (Command != null)
            {
                values.Add(Command);
            }

            if (Parameters != null)
            {
                for (var i = 0; i < Parameters.Length; i++)
                {
                    var param = Parameters[i];

                    if (param.Length == 0 || param.Contains(' '))
                    {
                        if (i == Parameters.Length - 1)
                        {
                            param = Constants.TrailingCharacter + param;
                        }
                        else
                        {
                            throw new InvalidDataException("Parameter contains unescapable character(s) when it is not the last one");
                        }
                    }

                    values.Add(param);
                }
            }

            return string.Join(Constants.SpaceCharacter, values);
        }

        public override bool Equals(object obj)
        {
            if (obj is RawMessage raw)
            {
                return raw.Command == Command
                    && raw.Source == Source
                    && Utilities.SafeSequenceEqual(raw.Parameters, Parameters)
                    && Utilities.SafeSequenceEqual(raw.Tags, Tags);
            }

            return false;
        }

        public override int GetHashCode() => (Command, Parameters, Tags, Source).GetHashCode();
    }
}