using MirClient.MirControls;
using MirClient.MirGraphics;
using MirClient.MirSounds;
using System.Text;

namespace MirClient.MirScenes
{
    public sealed class SelectScene : MirScene
    {
        public MirImageControl Background, Title;
        public MirLabel ServerLabel;

        public SelectScene()
        {
            SoundManager.PlaySound(SoundList.SelectMusic, true);
            Disposing += (o, e) => SoundManager.StopSound(SoundList.SelectMusic);

            Background = new MirImageControl
            {
                Index = 65,
                Library = Libraries.Prguse,
                Parent = this,
            };

            ServerLabel = new MirLabel
            {
                AutoSize = true,
                Location = new Point(Settings.ScreenWidth / 2 - TextWidth("江山如画") / 2, ((Settings.ScreenHeight - 600) / 2) + 4),
                Parent = Background,
                Text = "江山如画",
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            };
        }

        private int TextWidth(string text)
        {
            var ascii = Encoding.ASCII.GetBytes(text);
            return ascii.Length * 12;
        }

        public override void Process()
        {

        }
    }
}