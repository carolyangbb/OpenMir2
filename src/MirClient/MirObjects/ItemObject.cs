using MirClient.MirScenes;
using System.Text.RegularExpressions;
using Color = SharpDX.Color;
using WColor = System.Drawing.Color;

namespace MirClient.MirObjects
{
    public class ItemObject : MapObject
    {
        public override ObjectType Race
        {
            get { return ObjectType.Item; }
        }

        public override bool Blocking
        {
            get { return false; }
        }

        public Size Size;


        public ItemObject(uint objectID) : base(objectID)
        {

        }

        public override void Draw()
        {
            if (BodyLibrary != null)
                BodyLibrary.Draw(DrawFrame, DrawLocation, DrawColour);
        }

        public override void Process()
        {
            DrawLocation = new Point((CurrentLocation.X - User.Movement.X + MapControl.OffSetX) * MapControl.CellWidth, (CurrentLocation.Y - User.Movement.Y + MapControl.OffSetY) * MapControl.CellHeight);
            DrawLocation.Offset((MapControl.CellWidth - Size.Width) / 2, (MapControl.CellHeight - Size.Height) / 2);
            DrawLocation.Offset(User.OffSetMove);
            DrawLocation.Offset(GlobalDisplayLocationOffset);
            FinalDrawLocation = DrawLocation;

            DisplayRectangle = new Rectangle(DrawLocation, Size);
        }
        
        public override bool MouseOver(Point p)
        {
            return MapControl.MapLocation == CurrentLocation;
            // return DisplayRectangle.Contains(p);
        }

        public override void DrawName()
        {
            CreateLabel(WColor.Transparent, false, true);

            if (NameLabel == null) return;
            NameLabel.Location = new Point(
                DisplayRectangle.X + (DisplayRectangle.Width - NameLabel.Size.Width) / 2,
                DisplayRectangle.Y + (DisplayRectangle.Height - NameLabel.Size.Height) / 2 - 20);
            NameLabel.Draw();
        }

        public override void DrawBehindEffects(bool effectsEnabled)
        {
        }

        public override void DrawEffects(bool effectsEnabled)
        {


        }

        public void DrawName(int y)
        {
            CreateLabel(WColor.FromArgb(100, 0, 24, 48), true, false);

            NameLabel.Location = new Point(
                DisplayRectangle.X + (DisplayRectangle.Width - NameLabel.Size.Width) / 2,
                DisplayRectangle.Y + y + (DisplayRectangle.Height - NameLabel.Size.Height) / 2 - 20);
            NameLabel.Draw();
        }

        private void CreateLabel(WColor backColour, bool border, bool outline)
        {
            NameLabel = null;

            for (int i = 0; i < LabelList.Count; i++)
            {
                if (LabelList[i].Text != Name || LabelList[i].Border != border || LabelList[i].BackColour != backColour || LabelList[i].ForeColour != NameColour || LabelList[i].OutLine != outline) continue;
                NameLabel = LabelList[i];
                break;
            }

            if (NameLabel != null && !NameLabel.IsDisposed) return;

            NameLabel = new MirControls.MirLabel
            {
                AutoSize = true,
                BorderColour = WColor.Black,
                BackColour = backColour,
                ForeColour = NameColour,
                OutLine = outline,
                Border = border,
                Text = Regex.Replace(Name, @"\d+$", string.Empty),
            };
            LabelList.Add(NameLabel);
        }
    }
}
