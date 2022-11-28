using System;
using System.Reflection;
using UnityEngine;

namespace MokomoGames.Font.Editor
{
    public static class FontEngineEditorUtilitiesBridge
    {
        public static void SetAtlasTextureIsReadable(Texture2D texture, bool isReadable)
        {
            Type
                .GetType("UnityEditor.TextCore.LowLevel.FontEngineEditorUtilities,UnityEditor.TextCoreFontEngineModule")
                !.GetMethod("SetAtlasTextureIsReadable", BindingFlags.Static | BindingFlags.NonPublic)
                !.Invoke(null, new object[] { texture, isReadable });
        }
    }
}