namespace MokomoGames.Utilities
{
    public static class ListUtilities
    {
        public static (int x, int y) IndexToXY(int index, int rowLength)
        {
            var x = index % rowLength;
            var y = index / rowLength;
            return (x, y);
        }

        public static int XYToIndex(int x, int y, int rowLength)
        {
            return x * rowLength + y;
        }
    }
}