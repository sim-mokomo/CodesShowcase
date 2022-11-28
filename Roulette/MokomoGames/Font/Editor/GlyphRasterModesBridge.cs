using System;
using System.Reflection;

namespace MokomoGames.Font.Editor
{
    public static class GlyphRasterModesBridge
    {
        private static Type GetEnumType()
        {
            return Type.GetType("UnityEngine.TextCore.LowLevel.GlyphRasterModes,UnityEngine.TextCoreFontEngineModule");
        }
        
        public static int GetRasterModeHinted()
        {
            return (int)GetEnumType()!.GetField("RASTER_MODE_HINTED")!.GetValue(null);
        }

        public static int GetRasterModeMono()
        {
            return (int)GetEnumType()!.GetField("RASTER_MODE_MONO")!.GetValue(null);
        }
        
        public static int GetRasterModeBitMap()
        {
            return (int)GetEnumType()!.GetField("RASTER_MODE_BITMAP")!.GetValue(null);
        }
    }
}