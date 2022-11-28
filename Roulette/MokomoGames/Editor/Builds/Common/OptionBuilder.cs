using System.IO;
using System.Linq;
using MokomoGames.Editor.DefineSymbol;
using UnityEditor;
using UnityEditor.Build;

namespace MokomoGames.Editor.Builds.Common
{
    public abstract class OptionBuilder
    {
        protected abstract string GetAppExtension();
        protected readonly Option _option;

        protected OptionBuilder(Option option)
        {
            _option = option;
        }
        
        public virtual BuildPlayerOptions Build()
        {
            var option = new BuildPlayerOptions
            {
                locationPathName = Path.Join(_option.OutputPath, $"game{GetAppExtension()}"),
                target = _option.BuildTarget,
                scenes = EditorBuildSettings.scenes.Select(x => x.path).ToArray()
            };

            if (_option.IsDevelopment)
            {
                option.options |= BuildOptions.Development | BuildOptions.AllowDebugging;
            }
            
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS | BuildTargetGroup.Android, _option.AppId);
            PlayerSettings.SetScriptingBackend(BuildTargetGroup.iOS | BuildTargetGroup.Android,
                ScriptingImplementation.IL2CPP);
            PlayerSettings.SetIncrementalIl2CppBuild(BuildTargetGroup.iOS | BuildTargetGroup.Android, true);
            PlayerSettings.SetIl2CppCompilerConfiguration(BuildTargetGroup.Android | BuildTargetGroup.iOS,
                _option.IsDevelopment ? Il2CppCompilerConfiguration.Debug : Il2CppCompilerConfiguration.Release);
            PlayerSettings.bundleVersion = _option.BundleVersion;
            PlayerSettings.productName = _option.ProductName;
            PlayerSettings.companyName = "";
            EditorUserBuildSettings.il2CppCodeGeneration = _option.IsDevelopment
                ? Il2CppCodeGeneration.OptimizeSize
                : Il2CppCodeGeneration.OptimizeSpeed;

            var defineSymbolSettingList = new DefineSymbolService().LoadDefineSymbol();
            const string devDefineSymbol = "DEV";
            if (_option.Env == Env.Development)
            {
                defineSymbolSettingList.AddSymbol(devDefineSymbol);
            }
            else
            {
                defineSymbolSettingList.RemoveSymbol(devDefineSymbol);
            }

            UnityDefineSymbolRepository.SaveSymbols(defineSymbolSettingList);
            
            return option;
        }
    }
}