using UnityEngine;

namespace MokomoGames.Extensions
{
    public static class ColorExtensions
    {
        public static Color GetInvisibleColor(this Color self)
        {
            var invisibleColor = self;
            invisibleColor.a = 0f;
            return invisibleColor;
        }

        public static Color GetRandomColor()
        {
            return new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
            );
        }

        public static Color ConvertProtoBufColorToUnityColor(Protobuf.Color color)
        {
            return new Color(color.R, color.G, color.B, color.A);
        }

        public static Protobuf.Color ConvertColorToProtoBufColor(Color color)
        {
            return new Protobuf.Color()
            {
                R = color.r,
                G = color.g,
                B = color.b,
                A = color.a
            };
        }
    }
}