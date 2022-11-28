using System.Linq;
using UnityEngine;

namespace Roulette.Utilities
{
    public class CustomEditorUtility
    {
        public static string GetProjectName()
        {
            var paths = Application.dataPath.Split('/').ToList();
            return paths[paths.Count - 2];
        }

        public static string GetProjectNameFolderPath()
        {
            return $"Assets/{GetProjectName()}";
        }

        public static string GetLocalizeFolderPath()
        {
            return $"{GetProjectNameFolderPath()}/Localization";
        }
    }
}