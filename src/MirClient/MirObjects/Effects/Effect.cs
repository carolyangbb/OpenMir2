using MirClient.MirGraphics;
using MirClient.MirScenes;
using SharpDX;
using Point = System.Drawing.Point;
using Color = SharpDX.Color;
using WColor = System.Drawing.Color;

namespace MirClient.MirObjects.Effects
{
    public class Effect
    {
        public MLibrary Library;

        public int BaseIndex, Count, Duration;
        public long Start;

        public int CurrentFrame;
        public long NextFrame;

        public Point Source;
        public MapObject Owner;

        public int Light = 6;
        public Color LightColour = Color.White;

        public bool Blend = true;
        public float Rate = 1F;
        public Point DrawLocation;
        public Point DrawOffset = Point.Empty;
        public bool Repeat;
        public long RepeatUntil;

        public bool DrawBehind = false;

        public long CurrentDelay;
        public long Delay;

        public event EventHandler Complete;
        public event EventHandler Played;

        public Effect(MLibrary library, int baseIndex, int count, int duration, MapObject owner, long starttime = 0, bool drawBehind = false)
        {
            Library = library;
            BaseIndex = baseIndex;
            Count = count == 0 ? 1 : count;
            Duration = duration;
            Start = starttime == 0 ? GameFrm.Time : starttime;

            NextFrame = Start + (Duration / Count) * (CurrentFrame + 1);
            Owner = owner;
            Source = Owner.CurrentLocation;

            DrawBehind = drawBehind;
        }

        public Effect(MLibrary library, int baseIndex, int count, int duration, Point source, long starttime = 0, bool drawBehind = false)
        {
            Library = library;
            BaseIndex = baseIndex;
            Count = count == 0 ? 1 : count;
            Duration = duration;
            Start = starttime == 0 ? GameFrm.Time : starttime;

            NextFrame = Start + (Duration / Count) * (CurrentFrame + 1);
            Source = source;

            DrawBehind = drawBehind;
        }

        public void SetStart(long start)
        {
            Start = start;
            NextFrame = Start + (Duration / Count) * (CurrentFrame + 1);
        }

        public virtual void Process()
        {
            if (CurrentFrame == 1)
            {
                if (Played != null)
                    Played(this, EventArgs.Empty);
            }

            if (GameFrm.Time <= NextFrame) return;

            if (Owner != null && Owner.SkipFrames) CurrentFrame++;

            if (++CurrentFrame >= Count)
            {
                if (Repeat && (RepeatUntil == 0 || GameFrm.Time < RepeatUntil))
                {
                    CurrentFrame = 0;
                    Start = GameFrm.Time + Delay;
                    NextFrame = Start + (Duration / Count) * (CurrentFrame + 1);
                }
                else
                    Remove();
            }
            else NextFrame = Start + (Duration / Count) * (CurrentFrame + 1);

            GameScene.Scene.MapControl.TextureValid = false;
        }

        public virtual void Remove()
        {
            if (Owner != null)
                Owner.Effects.Remove(this);
            else
                MapControl.Effects.Remove(this);

            if (Complete != null)
                Complete(this, EventArgs.Empty);
        }

        public virtual void Draw()
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
                DrawLocation.Offset(DrawOffset);
                DrawLocation.Offset(MapObject.User.OffSetMove);
            }


            if (Blend)
                Library.DrawBlend(BaseIndex + CurrentFrame, DrawLocation, Color.White, true, Rate);
            else
                Library.Draw(BaseIndex + CurrentFrame, DrawLocation, Color.White, true);
        }

        public void Clear()
        {
            Complete = null;
            Played = null;
        }
    }

}
