using System;
using System.Linq;
using UnityEngine;

namespace MokomoGames.Utilities
{
    public static class ProjectUtilities
    {
        public static string GetProjectRootPath()
        {
            var paths = Application.dataPath.Split('/');
            return string.Join("/", paths.Take(paths.Length - 3));
        }
        
        public static string GetProjectName()
        {
            var paths = Application.dataPath.Split('/');
            return paths[paths.Length - 3];
        }

        public static string GetGameServerBuildsDirPath()
        {
            return $"{GetProjectRootPath()}\\server\\battle\\cluster\\gameserver\\builds";
        }
    }
}