using MokomoGames.Environment;
using UnityEditor;

namespace MokomoGames.Editor.Builds.Common
{
    public class OptionParser
    {
        public static readonly string Prefix = "option";

        public static string CreateOptionKey(string key)
        {
            return $"{Prefix}:{key}";
        }

        public Option Parse(string[] args)
        {
            var option = new Option
            {
                BuildTarget = CommandLineArgsParser.GetEnumOption<BuildTarget>(CreateOptionKey("platform"), args),
                Env = CommandLineArgsParser.GetEnumOption<Env>(CreateOptionKey("environment"), args),
                BundleVersion = CommandLineArgsParser.GetStringOption(CreateOptionKey("version"), args),
                ProductName = CommandLineArgsParser.GetStringOption(CreateOptionKey("productName"), args),
                IsHeadlessMode = CommandLineArgsParser.GetBoolOption(CreateOptionKey("headlessMode"), args),
                OutputPath = CommandLineArgsParser.GetStringOption(CreateOptionKey("output_path"), args),
                AppId = CommandLineArgsParser.GetStringOption(CreateOptionKey("app_id"), args)
            };
            return option;
        }
    }
}