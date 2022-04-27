using MirClient.MirControls;
using MirClient.MirObjects;
using MirClient.MirScenes.Game;
using MirClient.MirSounds;
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
        public int g_TileMapOffSetX = 9;
        public int g_TileMapOffSetY = 9;
        public MirLabel[] OutputLines = new MirLabel[10];
        public List<OutPutMessage> OutputMessages = new List<OutPutMessage>();

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


            MapControl = new MapControl
            {
                Index = 0,
                MapName = "n0",
                Title = "土城",
                MiniMap = 1,
                BigMap = 1,
                Lights = LightSetting.Day,
                Lightning = true,
                Fire = true,
                MapDarkLight =1,
                Music = 1
            };

            MapControl.LoadMap();

            InsertControl(0, MapControl);

            for (int i = 0; i < OutputLines.Length; i++)
                OutputLines[i] = new MirLabel
                {
                    AutoSize = true,
                    BackColour = WColor.Transparent,
                    Font = new System.Drawing.Font(Settings.FontName, 10F),
                    ForeColour = WColor.LimeGreen,
                    Location = new Point(20, 25 + i * 13),
                    OutLine = true,
                };
        }

        public override void Process()
        {
            if (MapControl == null || User == null)
                return;

            if (GameFrm.Time >= MoveTime)
            {
                MoveTime += 100; //Move Speed
                CanMove = true;
                MapControl.AnimationCount++;
                MapControl.TextureValid = false;
            }
            else
                CanMove = false;

            MapControl.Process();
            //MainDialog.Process();

            ProcessOuput();

            SoundManager.ProcessDelayedSounds();
        }

        protected internal override void DrawControl()
        {
            if (MapControl != null && !MapControl.IsDisposed)
            {
                MapControl.DrawControl();
            }

            //if (MapControl != null && !MapControl.IsDisposed && MapControl.CanDrawTileMap())
            //    MapControl.DrawMap();

            base.DrawControl();

            for (int i = 0; i < OutputLines.Length; i++)
                OutputLines[i].Draw();
        }

        /// <summary>
        /// 画游戏正式场景
        /// </summary>
        public void BeginScene()
        {
            MapControl.m_ClientRect.Left = 333 - g_TileMapOffSetX; //左
            MapControl.m_ClientRect.Right = 333 + g_TileMapOffSetX; //上
            MapControl.m_ClientRect.Top = 333 - g_TileMapOffSetY;  //右
            MapControl.m_ClientRect.Bottom = 333 + g_TileMapOffSetY; //下
            //装载地图定义
            MapControl.Map.UpdateMapPos(333, 333);
            //drawingbottomline = Settings.ScreenHeight;
            //SOFFX = 0;
            //SOFFY = 0;
        }

        public void OutputMessage(string message, OutputMessageType type = OutputMessageType.Normal)
        {

        }

        private void ProcessOuput()
        {
            for (int i = 0; i < OutputMessages.Count; i++)
            {
                if (GameFrm.Time >= OutputMessages[i].ExpireTime)
                    OutputMessages.RemoveAt(i);
            }

            for (int i = 0; i < OutputLines.Length; i++)
            {
                if (OutputMessages.Count > i)
                {
                    Color color;
                    switch (OutputMessages[i].Type)
                    {
                        case OutputMessageType.Quest:
                            color = Color.Gold;
                            break;
                        case OutputMessageType.Guild:
                            color = Color.DeepPink;
                            break;
                        default:
                            color = Color.LimeGreen;
                            break;
                    }

                    OutputLines[i].Text = OutputMessages[i].Message;
                    OutputLines[i].ForeColour = color;
                    OutputLines[i].Visible = true;
                }
                else
                {
                    OutputLines[i].Text = string.Empty;
                    OutputLines[i].Visible = false;
                }
            }
        }
    }

    public class OutPutMessage
    {
        public string Message;
        public long ExpireTime;
        public OutputMessageType Type;
    }
}