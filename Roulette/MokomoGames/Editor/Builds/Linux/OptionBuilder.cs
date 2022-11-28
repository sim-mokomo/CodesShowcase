using UnityEditor;

namespace MokomoGames.Editor.Builds.Linux
{
    public class OptionBuilder : Common.OptionBuilder
    {
        public OptionBuilder(Option option) : base(option.CommonOption)
        {
        }

        protected override string GetAppExtension()
        {
            return ".x86_64";
        }

        public override BuildPlayerOptions Build()
        {
            var options = base.Build();
            if (_option.IsHeadlessMode)
            {
                EditorUserBuildSettings.standaloneBuildSubtarget = StandaloneBuildSubtarget.Server;
            }
            return options;
        }
    }
}