using MirClient.MirScenes;
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

        public static int MaxDistance(Point p1, Point p2)
        {
            return Math.Max(Math.Abs(p1.X - p2.X), Math.Abs(p1.Y - p2.Y));
        }

        public static MirDirection ReverseDirection(MirDirection dir)
        {
            switch (dir)
            {
                case MirDirection.Up:
                    return MirDirection.Down;
                case MirDirection.UpRight:
                    return MirDirection.DownLeft;
                case MirDirection.Right:
                    return MirDirection.Left;
                case MirDirection.DownRight:
                    return MirDirection.UpLeft;
                case MirDirection.Down:
                    return MirDirection.Up;
                case MirDirection.DownLeft:
                    return MirDirection.UpRight;
                case MirDirection.Left:
                    return MirDirection.Right;
                case MirDirection.UpLeft:
                    return MirDirection.DownRight;
                default:
                    return dir;
            }
        }

        public static Point PointMove(Point p, MirDirection d, int i)
        {
            switch (d)
            {
                case MirDirection.Up:
                    p.Offset(0, -i);
                    break;
                case MirDirection.UpRight:
                    p.Offset(i, -i);
                    break;
                case MirDirection.Right:
                    p.Offset(i, 0);
                    break;
                case MirDirection.DownRight:
                    p.Offset(i, i);
                    break;
                case MirDirection.Down:
                    p.Offset(0, i);
                    break;
                case MirDirection.DownLeft:
                    p.Offset(-i, i);
                    break;
                case MirDirection.Left:
                    p.Offset(-i, 0);
                    break;
                case MirDirection.UpLeft:
                    p.Offset(-i, -i);
                    break;
            }
            return p;
        }

        public static MirDirection NextDir(MirDirection d)
        {
            switch (d)
            {
                case MirDirection.Up:
                    return MirDirection.UpRight;
                case MirDirection.UpRight:
                    return MirDirection.Right;
                case MirDirection.Right:
                    return MirDirection.DownRight;
                case MirDirection.DownRight:
                    return MirDirection.Down;
                case MirDirection.Down:
                    return MirDirection.DownLeft;
                case MirDirection.DownLeft:
                    return MirDirection.Left;
                case MirDirection.Left:
                    return MirDirection.UpLeft;
                case MirDirection.UpLeft:
                    return MirDirection.Up;
                default: return d;
            }
        }

        public static MirDirection PreviousDir(MirDirection d)
        {
            switch (d)
            {
                case MirDirection.Up:
                    return MirDirection.UpLeft;
                case MirDirection.UpRight:
                    return MirDirection.Up;
                case MirDirection.Right:
                    return MirDirection.UpRight;
                case MirDirection.DownRight:
                    return MirDirection.Right;
                case MirDirection.Down:
                    return MirDirection.DownRight;
                case MirDirection.DownLeft:
                    return MirDirection.Down;
                case MirDirection.Left:
                    return MirDirection.DownLeft;
                case MirDirection.UpLeft:
                    return MirDirection.Left;
                default: return d;
            }
        }

        public static MirDirection DirectionFromPoint(Point source, Point dest)
        {
            if (source.X < dest.X)
            {
                if (source.Y < dest.Y)
                    return MirDirection.DownRight;
                if (source.Y > dest.Y)
                    return MirDirection.UpRight;
                return MirDirection.Right;
            }

            if (source.X > dest.X)
            {
                if (source.Y < dest.Y)
                    return MirDirection.DownLeft;
                if (source.Y > dest.Y)
                    return MirDirection.UpLeft;
                return MirDirection.Left;
            }

            return source.Y < dest.Y ? MirDirection.Down : MirDirection.Up;
        }

    }
}
