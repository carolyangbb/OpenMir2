using System;
using System.Drawing;

namespace MirClient
{
    public static class Functions
    {
        public static string PointToString(Point p)
        {
            return String.Format("{0}, {1}", p.X, p.Y);
        }

        public static bool InRange(Point a, Point b, int i)
        {
            return Math.Abs(a.X - b.X) <= i && Math.Abs(a.Y - b.Y) <= i;
        }
    }
}
