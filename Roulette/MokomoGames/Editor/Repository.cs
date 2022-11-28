using UnityEditor;

namespace MokomoGames.Editor
{
    public class Repository
    {
        public static void Refresh()
        {
            AssetDatabase.Refresh();
        }
    }
}