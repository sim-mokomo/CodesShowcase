using MokomoGames.Environment;
using UnityEditor;

namespace MokomoGames.Editor.Builds.iOS
{
    public class OptionParser
    {
        public Option Parse(string[] args)
        {
            var option = new Option
            {
                CommonOption = new Common.OptionParser().Parse(args),
                ProvisioningProfileName =
                    CommandLineArgsParser.GetStringOption(
                        Common.OptionParser.CreateOptionKey("provisioningProfileName"), args),
                TeamId = CommandLineArgsParser.GetStringOption(Common.OptionParser.CreateOptionKey("teamId"), args),
                AdmobApplicationIdentify =
                    CommandLineArgsParser.GetStringOption(
                        Common.OptionParser.CreateOptionKey("admob_application_identify"), args)
            };
            return option;
        }
    }
}