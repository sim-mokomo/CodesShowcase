using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore;
using UnityEngine.TextCore.LowLevel;

namespace MokomoGames.Font.Editor
{
    public class FontAssetBridge
    {
        private readonly TMP_FontAsset _fontAsset;
        
        public FontAssetBridge(TMP_FontAsset fontAsset)
        {
            _fontAsset = fontAsset;
        }
        
        public void SetVersion(string version)
        {
            var p = _fontAsset.GetType()
                !.GetProperties(
                    BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.Public
                );
            _fontAsset.GetType()
                !.GetProperty("version",
                    BindingFlags.Instance |
                    BindingFlags.NonPublic |
                    BindingFlags.Public)
                !.SetValue(_fontAsset, version);
        }

        public List<TMP_Glyph> GetGlyphInfoList()
        {
            return (List<TMP_Glyph>)
                _fontAsset.GetType()!.GetField("m_glyphInfoList",
                    BindingFlags.Instance |
                    BindingFlags.NonPublic
                )
                !.GetValue(_fontAsset);
        }

        public void SetGlyphInfoList(List<TMP_Glyph> infoList)
        {
            _fontAsset.GetType()!.GetField("m_glyphInfoList",
                BindingFlags.Instance |
                BindingFlags.NonPublic
            )!.SetValue(_fontAsset, infoList);
        }

        public void SetAtlasRenderMode(GlyphRenderMode renderMode)
        {
            _fontAsset.GetType()
                !.GetProperty("atlasRenderMode")!.SetValue(_fontAsset, renderMode);
        }

        public void SetGlyphTable(List<Glyph> table)
        {
            _fontAsset.GetType()!.GetProperty("glyphTable")!.SetValue(_fontAsset, table);
        }

        public void SetCharacterTable(List<TMP_Character> table)
        {
            _fontAsset.GetType()!.GetProperty("characterTable")!.SetValue(_fontAsset, table);
        }

        public void SetFontFeatureTable(TMP_FontFeatureTable table)
        {
            _fontAsset.GetType()!.GetProperty("fontFeatureTable")!.SetValue(_fontAsset, table);
        }

        public void SetSourceFontFile_EditorRef(UnityEngine.Font font)
        {
            _fontAsset.GetType()
                !.GetField("m_SourceFontFile_EditorRef", BindingFlags.Instance | BindingFlags.NonPublic)
                !.SetValue(_fontAsset, font);
        }

        public void SetSourceFontFileGuid(string guid)
        {
            _fontAsset.GetType()
                !.GetField("m_SourceFontFileGUID", BindingFlags.Instance | BindingFlags.NonPublic)
                !.SetValue(_fontAsset, guid);
        }

        public void SetAtlasTextureIndex(int index)
        {
            _fontAsset.GetType()
                !.GetField("m_AtlasTextureIndex", BindingFlags.Instance | BindingFlags.NonPublic)
                !.SetValue(_fontAsset, index);
        }

        public void SetAtlasWidth(int width)
        {
            _fontAsset.GetType()!.GetProperty("atlasWidth")!.SetValue(_fontAsset, width);
        }

        public void SetAtlasHeight(int height)
        {
            _fontAsset.GetType()!.GetProperty("atlasHeight")!.SetValue(_fontAsset, height);
        }

        public void SetAtlasPadding(int padding)
        {
            _fontAsset.GetType()!.GetProperty("atlasPadding")!.SetValue(_fontAsset, padding);
        }

        public void SortAllTables()
        {
            _fontAsset.GetType()
                !.GetMethod("SortAllTables", BindingFlags.Instance | BindingFlags.NonPublic)!.Invoke(_fontAsset, new object[]{});
        }

        public void SetFreeGlyphRects(List<GlyphRect> list)
        {
            _fontAsset.GetType()
                !.GetProperty("freeGlyphRects", BindingFlags.Instance | BindingFlags.NonPublic)
                !.SetValue(_fontAsset, list);
        }

        public void SetUsedGlyphRects(List<GlyphRect> list)
        {
            _fontAsset.GetType()
                !.GetProperty("usedGlyphRects", BindingFlags.Instance | BindingFlags.NonPublic)
                !.SetValue(_fontAsset, list);
        }
    }
}