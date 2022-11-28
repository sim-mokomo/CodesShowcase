using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace MokomoGames.Font.Editor
{
    public static class FontEngineBridge
    {
        public static float GetGenerationProgress()
        {
            var fieldInfo =
                typeof(FontEngine).GetField("generationProgress", BindingFlags.Static | BindingFlags.NonPublic);
            if (fieldInfo == null)
            {
                return 0;
            }

            return (float)fieldInfo.GetValue(null);
        }

        public static bool TryPackGlyphsInAtlas(
            List<Glyph> glyphsToAdd,
            List<Glyph> glyphsAdded,
            int padding,
            GlyphPackingMode packingMode,
            GlyphRenderMode renderMode,
            int width,
            int height,
            List<GlyphRect> freeGlyphRects,
            List<GlyphRect> usedGlyphRects)
        {
            var methodInfo = typeof(FontEngine).GetMethod("TryPackGlyphsInAtlas", BindingFlags.Static | BindingFlags.NonPublic);
            return (bool)methodInfo!.Invoke(null, new object[]
            {
                glyphsToAdd,
                glyphsAdded,
                padding,
                packingMode,
                renderMode,
                width,
                height,
                freeGlyphRects,
                usedGlyphRects
            });
        }

        public static FontEngineError RenderGlyphsToTexture(
            List<Glyph> glyphs,
            int padding,
            GlyphRenderMode renderMode,
            byte[] texBuffer,
            int texWidth,
            int texHeight)
        {
            var methodInfo = typeof(FontEngine)
                .GetMethod(
                    "RenderGlyphsToTexture",
                    BindingFlags.Static | BindingFlags.NonPublic,
                    null,
                    new []
                    {
                        typeof(List<Glyph>),
                        typeof(int),
                        typeof(GlyphRenderMode),
                        typeof(byte[]),
                        typeof(int),
                        typeof(int)
                    },
                    null
                    );
            return (FontEngineError)methodInfo!.Invoke(null, new object[]
            {
                glyphs,
                padding,
                renderMode,
                texBuffer,
                texWidth,
                texHeight
            });
        }

        public static void ResetAtlasTexture(Texture2D texture2D)
        {
            var methodInfo = typeof(FontEngine).GetMethod("ResetAtlasTexture", BindingFlags.Static | BindingFlags.NonPublic);
            methodInfo!.Invoke(null, new object[] { texture2D });
        }

        public static void SendCancellationRequest()
        {
            var methodInfo = typeof(FontEngine).GetMethod("SendCancellationRequest", BindingFlags.Static | BindingFlags.NonPublic);
            methodInfo!.Invoke(null, new object []{});
        }

        public static GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentTable(uint[] glyphIndexes)
        {
            return (GlyphPairAdjustmentRecord[])typeof(FontEngine).GetMethod("GetGlyphPairAdjustmentTable", BindingFlags.Static | BindingFlags.NonPublic)
                !.Invoke(null, new object[] { glyphIndexes });
        }
    }
}