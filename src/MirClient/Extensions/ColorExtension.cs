using SharpDX;
using WColor = System.Drawing.Color;

namespace MirClient.Extensions
{
    public static class ColorExtension
    {
        public static Color4 ToColor4(this WColor source)
        {
            var color = new Color4();
            color.Alpha = (float)(source.A / 255.0);
            color.Red = (float)(source.R / 255.0);
            color.Green = (float)(source.G / 255.0);
            color.Blue = (float)(source.B / 255.0);
            return color;
        }

        public static WColor ToColor(this Color4 source)
        {
            return WColor.FromArgb((int)(source.Alpha * 255.0), (int)(source.Red * 255.0), (int)(source.Green * 255.0), (int)(source.Blue * 255.0));
        }
    }
}
