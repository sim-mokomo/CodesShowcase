using System;
using System.IO;
using MokomoGames.Editor.Builds.Common;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Callbacks;

#if UNITY_IOS || UNITY_EDITOR_OSX
using UnityEditor.iOS.Xcode;
using UnityEngine;
#endif

namespace MokomoGames.Editor.Builds.iOS
{
    public class OptionBuilder : Common.OptionBuilder
    {
        private static Option _option;
        private static int BuildNumber => int.Parse(PlayerSettings.iOS.buildNumber);

        public OptionBuilder(Option option) : base(option.CommonOption)
        {
            PlayerSettings.iOS.appleEnableAutomaticSigning = false;
            PlayerSettings.iOS.appleDeveloperTeamID = option.TeamId;
            // NOTE: 配布形式で端末にインストールしたいので、ステージング環境でもProfile自体はDevのものを使用する。
            if(option.CommonOption.IsDevelopment || option.CommonOption.Env == Env.Staging){
                PlayerSettings.iOS.iOSManualProvisioningProfileType = ProvisioningProfileType.Development;
            }else{
                PlayerSettings.iOS.iOSManualProvisioningProfileType = ProvisioningProfileType.Distribution;
            }
            PlayerSettings.iOS.iOSManualProvisioningProfileID = option.ProvisioningProfileName;
            _option = option;
        }

        protected override string GetAppExtension()
        {
            return String.Empty;
        }

        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget buildTarget, string pathToBuiltProject)
        {
#if UNITY_IOS || UNITY_EDITOR_OSX
            if (!string.IsNullOrEmpty(_option.AdmobApplicationIdentify))
            {
                UpdatePlistProcess(pathToBuiltProject, plist =>
                {
                    plist.root.SetString("GADApplicationIdentifier", _option.AdmobApplicationIdentify);
                });
            }

            PlayerSettings.iOS.buildNumber = (BuildNumber + 1).ToString();
            
            UpdatePBXProjectProcess(pathToBuiltProject);
#endif
        }
#if UNITY_IOS || UNITY_EDITOR_OSX
        private static void UpdatePBXProjectProcess(string pathToBuiltProject)
        {
            var projectPath = PBXProject.GetPBXProjectPath(pathToBuiltProject);
            var content = File.ReadAllText(projectPath);
            content = content.Replace("ENABLE_BITCODE = YES", "ENABLE_BITCODE = NO");
            File.WriteAllText(projectPath, content);
        }

        private static void UpdatePlistProcess(string pathToBuiltProject, Action<PlistDocument> process)
        {
            var plistPath = Path.Join(pathToBuiltProject, "Info.plist");
            var plist = new PlistDocument();
            plist.ReadFromFile(plistPath);
            process(plist);
            plist.WriteToFile(plistPath);
        }
#endif
    }
}