using MokomoGames.Environment;

namespace MokomoGames.Editor.Builds.Linux
{
    public class OptionParser
    {
        public Option Parse(string[] args)
        {
            var option = new Option
            {
                CommonOption = new Common.OptionParser().Parse(args),
                IsHeadlessMode =
                    CommandLineArgsParser.GetBoolOption(Common.OptionParser.CreateOptionKey("headlessMode"), args)
            };
            return option;
        }
    }
}