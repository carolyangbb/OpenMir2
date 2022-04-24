using MirClient.MirControls;
using MirClient.MirGraphics;
using MirClient.MirScenes.Login;
using MirClient.MirSounds;
using System.Drawing;
using System.Windows.Forms;

namespace MirClient.MirScenes
{
    /// <summary>
    /// 登陆场景
    /// </summary>
    public sealed class LoginScene : MirScene
    {
        private MirAnimatedControl _background;
        private MirLabel Version;
        private LoginDialog _login;
        private MirMessageBox _connectBox;

        public LoginScene()
        {
            SoundManager.PlaySound(SoundList.IntroMusic, true);
            Disposing += (o, e) => SoundManager.StopSound(SoundList.IntroMusic);

            _background = new MirAnimatedControl
            {
                Animated = false,
                AnimationCount = 19,
                AnimationDelay = 100,
                Index = 22,
                Library = Libraries.ChrSel,
                Loop = false,
                Parent = this
            };

            _login = new LoginDialog { Parent = _background, Visible = false };

            Version = new MirLabel
            {
                AutoSize = true,
                BackColour = Color.FromArgb(200, 50, 50, 50),
                Border = true,
                BorderColour = Color.Black,
                Location = new Point(5, Settings.ScreenHeight - 20),
                Parent = _background,
                Text = string.Format("Build: {0}.{1}.{2}", Globals.ProductCodename, Settings.UseTestConfig ? "Debug" : "Release", Application.ProductVersion),
            };

            _connectBox = new MirMessageBox("游戏连接已关闭...", MirMessageBoxButtons.Cancel);
            _connectBox.CancelButton.Click += (o, e) => Program.Form.Close();
            Shown += (sender, agrs) =>
            {
                //_connectBox.Show();
                _login.Show();
            };
        }

        public override void Process()
        {

        }

        public override void Show()
        {
            if (Visible) return;
            Visible = true;
            //AccountIDTextBox.SetFocus();

            //if (Settings.Password != string.Empty && Settings.AccountID != string.Empty)
            //{
            //    Login();
            //}
        }

        #region Disposable
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _background = null;
                Version = null;
                _login = null;
            }

            base.Dispose(disposing);
        }
        #endregion
    }
}