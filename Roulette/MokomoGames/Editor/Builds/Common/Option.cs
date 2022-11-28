using UnityEditor;

namespace MokomoGames.Editor.Builds.Common
{
    public class Option
    {
        public string OutputPath;
        public BuildTarget BuildTarget;
        public bool ShouldRaiseError;
        public bool IsDevelopment => Env == Env.Development;
        public bool IsHeadlessMode;
        public Env Env;
        public string BundleVersion;
        public string ProductName;
        public string AppId;
    }
}