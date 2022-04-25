using MirClient.MirControls;
using MirClient.MirGraphics;
using System.Drawing;

namespace MirClient.MirScenes.Login
{
    /// <summary>
    /// 选择服务器
    /// </summary>
    public class SelectServerDialog : MirImageControl
    {
        private readonly MirButton CloseButton;
        private readonly MirButton SerButton;
        private readonly MirLabel SerNameLable;
        private readonly LoginScene _loginScene;

        public SelectServerDialog(LoginScene loginScene)
        {
            _loginScene = loginScene;
            Index = 256;
            Library = Libraries.Prguse;
            Location = new Point((Settings.ScreenWidth - Size.Width) / 2, (Settings.ScreenHeight - Size.Height) / 2);
            PixelDetect = false;
            Size = new Size(328, 220);

            CloseButton = new MirButton
            {
                Index = 64,
                Library = Libraries.Prguse,
                Location = new Point(245, 31),
                Parent = this,
                PressedIndex = 331
            };
            CloseButton.Click += (o, e) => Program.Form.Close();

            SerButton = new MirButton
            {
                Enabled = true,
                Size = new Size(42, 42),
                Index = 2,
                Library = Libraries.Prguse3,
                Location = new Point(65, 200),
                Parent = this,
                PressedIndex = 3,
            };
            SerButton.Click += (o, e) =>
            {
                this.Visible = false;
                _loginScene.OpenDoor();
            };

            SerNameLable = new MirLabel()
            {
                AutoSize = true,
                Enabled = false,
                Location = new Point(40, 10),
                Parent = SerButton,
                Text = "江山如画",
            };
            SerNameLable.Font = new Font("宋体", 12, FontStyle.Bold);
            SerNameLable.ForeColour = Color.Yellow;
        }

        public override void Show()
        {
            if (Visible) return;
            Visible = true;
        }
    }
}