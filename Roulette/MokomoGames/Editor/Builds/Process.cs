using System;
using MokomoGames.Editor.Builds.Android;
using MokomoGames.Environment;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace MokomoGames.Editor.Builds
{
    public class Process : MonoBehaviour
    {
        public static void BuildFromCui()
        {
            var args = System.Environment.GetCommandLineArgs();
            var platform = CommandLineArgsParser.GetEnumOption<BuildTarget>(Common.OptionParser.CreateOptionKey("platform"), args);
            Common.OptionBuilder optionBuilder = platform switch
            {
                BuildTarget.Android => new OptionBuilder(new Common.OptionParser().Parse(args)),
                BuildTarget.iOS => new iOS.OptionBuilder(new iOS.OptionParser().Parse(args)),
                BuildTarget.StandaloneLinux64 => new Linux.OptionBuilder(new Linux.OptionParser().Parse(args)),
                _ => null
            };

            if (optionBuilder == null)
            {
                throw new Exception($"#{platform}がビルドサポートされていません。");
            }

            var result = BuildPipeline.BuildPlayer(optionBuilder.Build());
            var success = result.summary.result == BuildResult.Succeeded;
            if (!success)
            {
                Debug.Log("[DEBUG] failed to build");
                throw new Exception();
            }
        }
    }
}
