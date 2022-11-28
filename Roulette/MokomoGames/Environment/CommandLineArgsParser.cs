using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MokomoGames.Environment
{
    public class CommandLineArgsParser
    {
        private static string GetOption(string key, IReadOnlyList<string> args)
        {
            for (var i = 0; i < args.Count - 1; i++)
            {
                if (args[i] != key)
                {
                    continue;
                }

                return args[i + 1];
            }
            return string.Empty;
        }

        public static string GetStringOption(string key, IReadOnlyList<string> args)
        {
            return GetOption(key, args);
        }

        public static int GetIntOption(string key, IReadOnlyList<string> args)
        {
            return int.TryParse(GetOption(key, args), out var value) ? value : 0;
        }

        public static bool GetBoolOption(string key, IReadOnlyList<string> args)
        {
            return bool.TryParse(GetOption(key, args), out var value) && value;
        }
        
        public static T GetEnumOption<T>(string key, IReadOnlyList<string> args) where T : struct
        {
            return Enum.TryParse<T>(GetOption(key, args), out var value) ? value : new T();
        }
    }
}