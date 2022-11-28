using System.Collections.Generic;
using UnityEditor.Build;

namespace MokomoGames.Editor.DefineSymbol
{
    public class DefineSymbolConfig
    {
        public static readonly IReadOnlyCollection<NamedBuildTarget> SupportPlatformList = new List<NamedBuildTarget>
        {
            NamedBuildTarget.Android,
            NamedBuildTarget.iOS,
            NamedBuildTarget.Standalone
        };
    }
}