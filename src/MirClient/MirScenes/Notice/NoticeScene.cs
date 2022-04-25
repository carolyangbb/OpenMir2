using MirClient.MirControls;
using MirClient.MirGraphics;
using Color = SharpDX.Color;
using WColor = System.Drawing.Color;

namespace MirClient.MirScenes
{
    public class NoticeScene : MirScene
    {
        public MirImageControl Background;
        public MirMessageBox MirMessageBox;

        public NoticeScene()
        {
            Background = new MirImageControl
            {
                Parent = this,
                BackColour = WColor.Black
            };

            var noticeStr = "\n\n\n\n健康游戏公告\n\n\n\n抵制不良游戏，拒绝盗版游戏。\n 注意自我保护，谨防受骗上当。\n 适度游戏益脑，沉迷游戏伤身。 \n 合理安排时间，享受健康生活。\n\n\n\n热血管理";

            var size = Libraries.Prguse.GetTrueSize(380);
            MirMessageBox = new MirMessageBox(noticeStr);
            MirMessageBox.Parent = this;
            MirMessageBox.Index = 380;
            MirMessageBox.Library = Libraries.Prguse;
            MirMessageBox.Location = new Point((Settings.ScreenWidth - size.Width) / 2, (Settings.ScreenHeight - size.Height) / 2);
            MirMessageBox.Label.Size = new Size(200, 300);
            MirMessageBox.Size = new Size(90, 305);
            MirMessageBox.OKButton.Location = new Point(90, 305);
            MirMessageBox.OKButton.Click += (o, i) =>
            {
                StartGame();
            };
        }

        public void StartGame()
        {
            switch (Settings.Resolution)
            {
                case 1024:
                    Settings.Resolution = 1024;
                    GameFrm.DisplayChange(1024, 768);
                    break;
                case 1280:
                    GameFrm.DisplayChange(1280, 800);
                    break;
                case 1366:
                    GameFrm.DisplayChange(1366, 768);
                    break;
                case 1920:
                    GameFrm.DisplayChange(1920, 1080);
                    break;
            }
            ActiveScene = new GameScene();
            Dispose();
        }

        public override void Process()
        {

        }
    }
}