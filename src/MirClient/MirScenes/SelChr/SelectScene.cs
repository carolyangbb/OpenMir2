using MirClient.MirControls;
using MirClient.MirGraphics;
using MirClient.MirSounds;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                Location = new Point(432, 60),
                Parent = Background,
                Size = new Size(155, 17),
                Text = "Legend of Mir 2",
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            };


        }

        public override void Process()
        {
           
        }
    }
}
