using MirClient.MirObjects;
using MirClient.MirScenes.Game;
using MirClient.MirSounds;
using Color = SharpDX.Color;
using WColor = System.Drawing.Color;

namespace MirClient.MirScenes
{
    public class GameScene : MirScene
    {
        public static GameScene Scene;
        public static UserObject User
        {
            get { return MapObject.User; }
            set { MapObject.User = value; }
        }
        public static long MoveTime, AttackTime, NextRunTime, LogTime, LastRunTime;
        public static bool CanMove, CanRun;
        public static long UseItemTime, PickUpTime, DropViewTime, TargetDeadTime;
        public LightSetting Lights;
        public MapControl MapControl;
        public MainDialog MainDialog;

        public GameScene()
        {
            MapControl.AutoRun = false;
            MapControl.AutoHit = false;

            Scene = this;
            BackColour = WColor.Transparent;
            MoveTime = GameFrm.Time;

            MainDialog = new MainDialog { Parent = this };

            User = new UserObject();
            User.Movement = new Point(333, 333);
            User.Movement.X = 333;
            User.Movement.Y = 333;

            if (MapControl != null && !MapControl.IsDisposed)
                MapControl.Dispose();
            MapControl = new MapControl { Index = 0, FileName = Path.Combine(Settings.MapPath, "3.map"), Title = "土城" };
            MapControl.LoadMap();
            InsertControl(0, MapControl);
        }

        public override void Process()
        {
            if (MapControl == null || User == null)
                return;

            MapControl.Process();

            SoundManager.ProcessDelayedSounds();
        }

        protected internal override void DrawControl()
        {
            if (MapControl != null && !MapControl.IsDisposed)
                MapControl.DrawControl();
            base.DrawControl();
        }


        public void OutputMessage(string message, OutputMessageType type = OutputMessageType.Normal)
        {
            
        }
    }
}
