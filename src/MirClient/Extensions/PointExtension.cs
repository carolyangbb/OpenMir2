using System.Drawing;

namespace MirClient
{
    public static class PointExtension
    {
        public static Point Subtract(this Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point Subtract(this Point p1, int x, int y)
        {
            return new Point(p1.X - x, p1.Y - y);
        }

        public static Point Add(this Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point Add(this Point p1, int x, int y)
        {
            return new Point(p1.X + x, p1.Y + y);
        }
    }
}