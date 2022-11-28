using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Callbacks;

namespace MokomoGames.Editor.Builds.Android
{
    public class OptionBuilder : Common.OptionBuilder
    {
        public OptionBuilder(Common.Option option) : base(option)
        {
            EditorUserBuildSettings.buildAppBundle = true;
            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keyaliasName = "release";
            PlayerSettings.Android.keyaliasPass = "";
            PlayerSettings.Android.keystoreName = "";
            PlayerSettings.Android.keystorePass = "";
            PlayerSettings.Android.targetArchitectures = AndroidArchitecture.ARM64;
            PlayerSettings.Android.minSdkVersion = AndroidSdkVersions.AndroidApiLevel24;
            PlayerSettings.Android.targetSdkVersion = AndroidSdkVersions.AndroidApiLevelAuto;
        }

        protected override string GetAppExtension()
        {
            return ".aab";
        }

        [PostProcessBuild]
        public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
        {
            if (target != BuildTarget.Android)
            {
                return;
            }
            PlayerSettings.Android.bundleVersionCode += 1;
        }
    }
}