using MirClient.MirControls;
using MirClient.MirGraphics;
using MirClient.MirScenes.Login;
using MirClient.MirSounds;
using Color = SharpDX.Color;
using WColor = System.Drawing.Color;

namespace MirClient.MirScenes
{
    /// <summary>
    /// 登陆场景
    /// </summary>
    public sealed class LoginScene : MirScene
    {
        private MirImageControl _background;
        private MirAnimatedControl _openDoor;
        private MirLabel _version;
        private LoginDialog _login;
        private SelectServerDialog _selectServer;
        private MirMessageBox _connectBox;

        public LoginScene()
        {
            SoundManager.PlaySound(SoundList.IntroMusic, true);
            Disposing += (o, e) => SoundManager.StopSound(SoundList.IntroMusic);

            _background = new MirImageControl
            {
                Index = 22,
                Library = Libraries.ChrSel,
                Parent = this,
                Location = new Point((Settings.ScreenWidth - 800) / 2, (Settings.ScreenHeight - 600) / 2)
            };

            _openDoor = new MirAnimatedControl
            {
                Animated = false,
                AnimationCount = 11,
                AnimationDelay = 230,
                Index = 22,
                Library = Libraries.ChrSel,
                Loop = false,
                Parent = _background
            };

            _login = new LoginDialog { Parent = _background, Visible = false };

            _selectServer = new SelectServerDialog(this) { Parent = _background, Visible = false };

            _version = new MirLabel
            {
                AutoSize = true,
                BackColour = WColor.FromArgb(200, 50, 50, 50),
                Border = true,
                BorderColour = WColor.Black,
                ForeColour = WColor.Lime,
                Location = new Point(5, Settings.ScreenHeight - 20),
                Parent = _background,
                Text = string.Format("Build: {0}.{1}.{2}", Globals.ProductCodename, Settings.UseTestConfig ? "Debug" : "Release", Application.ProductVersion)
            };
            _connectBox = new MirMessageBox("游戏连接已关闭...", MirMessageBoxButtons.Cancel);
            _connectBox.CancelButton.Click += (o, e) => Program.Form.Close();
            Shown += (sender, agrs) =>
            {
                //_connectBox.Show();
                _selectServer.Show();
            };
        }

        public void OpenDoor()
        {
            Enabled = false;
            _login.Dispose();

            SoundManager.PlaySound(SoundList.LoginEffect);
            _openDoor.Animated = true;
            _openDoor.Location = new Point((Settings.ScreenWidth - 800) / 2 + 152, (Settings.ScreenHeight - 600) / 2 + 96);
            _openDoor.AfterAnimation += (o, e) =>
            {
                Dispose();
                ActiveScene = new SelectScene();
            };
        }

        public override void Process()
        {

        }

        #region Disposable
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _background = null;
                _version = null;
                _login = null;
                _connectBox = null;
                _openDoor = null;
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}