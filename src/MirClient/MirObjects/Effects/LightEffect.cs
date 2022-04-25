using MirClient.MirScenes;
using SharpDX;
using Color = SharpDX.Color;
using Point = System.Drawing.Point;

namespace MirClient.MirObjects.Effects
{
    public class LightEffect : Effect
    {
        public LightEffect(int duration, MapObject owner, long starttime = 0, int lightDistance = 6, Color? lightColour = null)
            : base(null, 0, 0, duration, owner, starttime)
        {
            Light = lightDistance;
            LightColour = (Color)(lightColour == null ? Color.White : lightColour);
        }

        public LightEffect(int duration, Point source, long starttime = 0, int lightDistance = 6, Color? lightColour = null)
            : base(null, 0, 0, duration, source, starttime)
        {
            Light = lightDistance;
            LightColour = (Color)(lightColour == null ? Color.White : lightColour);
        }

        public override void Process()
        {
            if (GameFrm.Time >= Start + Duration)
                Remove();
            GameScene.Scene.MapControl.TextureValid = false;
        }

        public override void Draw()
        {
            if (GameFrm.Time < Start) return;

            if (Owner != null)
            {
                DrawLocation = Owner.DrawLocation;
            }
            else
            {
                DrawLocation = new Point((Source.X - MapObject.User.Movement.X + MapControl.OffSetX) * MapControl.CellWidth,
                                         (Source.Y - MapObject.User.Movement.Y + MapControl.OffSetY) * MapControl.CellHeight);
                DrawLocation.Offset(MapObject.User.OffSetMove);
            }
        }
    }
}
