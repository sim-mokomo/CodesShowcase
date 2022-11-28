using System;
using TMPro;
using UnityEngine.TextCore.LowLevel;

namespace MokomoGames.Font.Editor
{
    public static class TMPGlyphPairAdjustmentRecordBridge
    {
        public static TMP_GlyphPairAdjustmentRecord CreateAdjustmentRecord(GlyphPairAdjustmentRecord record)
        {
            var type = Type.GetType("TMPro.TMP_GlyphPairAdjustmentRecord,Unity.TextMeshPro");
            return (TMP_GlyphPairAdjustmentRecord)Activator.CreateInstance(type!, record);
        }
    }
}