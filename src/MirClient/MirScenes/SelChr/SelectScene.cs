using MirClient.MirControls;
using MirClient.MirGraphics;
using MirClient.MirSounds;
using SharpDX;
using System.Text;
using Point = System.Drawing.Point;
using Color = SharpDX.Color;
using WColor = System.Drawing.Color;

namespace MirClient.MirScenes
{
    public sealed class SelectScene : MirScene
    {
        public MirImageControl Background, Title;
        public MirLabel ServerLabel;
        public MirButton DscStart, DscNewChr, DscDelChr, DscCredits, ExitGame;
        public CharacterButton[] CharacterButtons;
        public MirMessageBox MirMessageBox;
        public MirAnimatedControl CharacterDisplay;
        public List<UserCharacterInfo> Characters = new List<UserCharacterInfo>();
        private int _selected;

        public SelectScene()
        {
            SoundManager.PlaySound(SoundList.SelectMusic, true);
            Disposing += (o, e) => SoundManager.StopSound(SoundList.SelectMusic);

            Characters.Add(new UserCharacterInfo()
            {
                Hair = 1,
                Job = 0,
                Sex = 0,
                Level = 1,
                Name = "gm01"
            });

            //Characters.Add(new UserCharacterInfo()
            //{
            //    Hair = 0,
            //    Job = 1,
            //    Sex = 1,
            //    Level = 10,
            //    Name = "gm02"
            //});

            Background = new MirImageControl
            {
                Index = 65,
                Library = Libraries.Prguse,
                Parent = this,
            };

            ServerLabel = new MirLabel
            {
                Enabled = false,
                AutoSize = true,
                Location = new Point(Settings.ScreenWidth / 2 - TextWidth("江山如画") / 2, ((Settings.ScreenHeight - 600) / 2) + 4),
                Parent = Background,
                Text = "江山如画",
                DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter
            };

            CharacterDisplay = new MirAnimatedControl
            {
                Animated = true,
                AnimationCount = 16,
                AnimationDelay = 250,
                FadeIn = true,
                FadeInDelay = 75,
                FadeInRate = 0.1F,
                Index = 220,
                Library = Libraries.ChrSel,
                Location = new Point((Settings.ScreenWidth - 800) / 2 + 65, (Settings.ScreenHeight - 600) / 2 + 90),
                Parent = Background,
                UseOffSet = true,
                Visible = false
            };
            CharacterDisplay.AfterDraw += (o, e) =>
            {
                Libraries.ChrSel.DrawBlend(CharacterDisplay.Index + 560, CharacterDisplay.DisplayLocationWithoutOffSet, Color.White, true);
            };

            CharacterButtons = new CharacterButton[2];

            CharacterButtons[0] = new CharacterButton(this)
            {
                Enabled = true,
                Index = 66,
                Library = Libraries.Prguse,
                Location = new Point((Settings.ScreenWidth - 800) / 2 + 134, (Settings.ScreenHeight - 600) / 2 + 454),
                Parent = Background,
                Sound = SoundList.ButtonA
            };

            CharacterButtons[1] = new CharacterButton(this)
            {
                Enabled = true,
                Index = 67,
                Library = Libraries.Prguse,
                Location = new Point((Settings.ScreenWidth - 800) / 2 + 685, (Settings.ScreenHeight - 600) / 2 + 454),
                Parent = Background,
                Sound = SoundList.ButtonA
            };

            DscStart = new MirButton()
            {
                Enabled = true,
                Index = 68,
                Library = Libraries.Prguse,
                Location = new Point((Settings.ScreenWidth - 800) / 2 + 385, (Settings.ScreenHeight - 600) / 2 + 456),
                Parent = Background,
                PressedIndex = 68
            };
            DscStart.Click += (o, i) =>
            {
                ActiveScene = new NoticeScene();
                Dispose();
                //MessageBoxDlg("一开始你应该创建一个新角色。\r\n选择<创建角色>你就能建立一个新角色。");
            };

            DscNewChr = new MirButton()
            {
                Enabled = true,
                Index = 69,
                Library = Libraries.Prguse,
                Location = new Point((Settings.ScreenWidth - 800) / 2 + 348, (Settings.ScreenHeight - 600) / 2 + 486),
                Parent = Background,
                PressedIndex = 325
            };

            DscDelChr = new MirButton()
            {
                Enabled = true,
                Index = 70,
                Library = Libraries.Prguse,
                Location = new Point((Settings.ScreenWidth - 800) / 2 + 347, (Settings.ScreenHeight - 600) / 2 + 506),
                Parent = Background,
                PressedIndex = 70
            };
            DscDelChr.Click += (o, i) =>
            {
                MessageBoxDlg("'[ChrArr[n].UserChr.Name] 被删除的角色是不能被恢复的\r\n 一段时间内你将不能使用相同的角色名。\r\n 你真的要删除吗？", MirMessageBoxButtons.YesNo);
            };


            DscCredits = new MirButton()
            {
                Enabled = true,
                Index = 71,
                Library = Libraries.Prguse,
                Location = new Point((Settings.ScreenWidth - 800) / 2 + 362, (Settings.ScreenHeight - 600) / 2 + 527),
                Parent = Background,
                PressedIndex = 325
            };

            ExitGame = new MirButton()
            {
                Enabled = true,
                Index = 72,
                Library = Libraries.Prguse,
                Location = new Point((Settings.ScreenWidth - 800) / 2 + 379, (Settings.ScreenHeight - 600) / 2 + 547),
                Parent = Background,
                PressedIndex = 72
            };
            ExitGame.Click += (o, e) => Program.Form.Close();

            UpdateInterface();
        }

        private int TextWidth(string text)
        {
            var ascii = Encoding.ASCII.GetBytes(text);
            return ascii.Length * 12;
        }

        public override void Process()
        {

        }

        private void UpdateInterface()
        {
            for (int i = 0; i < CharacterButtons.Length; i++)
            {
                CharacterButtons[i].Selected = i == _selected;
                CharacterButtons[i].Update(i >= Characters.Count ? null : Characters[i]);
            }

            if (_selected >= 0 && _selected < Characters.Count)
            {
                CharacterDisplay.Visible = true;

                switch (Characters[_selected].Job)
                {
                    case 0:
                        CharacterDisplay.Index = Characters[_selected].Sex == 0 ? 40 : 160; 
                        break;
                    case 1:
                        CharacterDisplay.Index = (byte)Characters[_selected].Sex == 0 ? 80 : 200; 
                        break;
                    case 2:
                        CharacterDisplay.Index = (byte)Characters[_selected].Sex == 0 ? 120 : 240;
                        break;
                }
            }
            else
            {
                CharacterDisplay.Visible = false;
            }
        }

        private void MessageBoxDlg(string message, MirMessageBoxButtons mirButton = MirMessageBoxButtons.OK)
        {
            MirMessageBox = null;
            MirMessageBox = new MirMessageBox(message, mirButton);
            switch (mirButton)
            {
                case MirMessageBoxButtons.OK:
                    MirMessageBox.OKButton.Click += (o, e) => MirMessageBox.Hide();
                    break;
                case MirMessageBoxButtons.YesNo:
                    MirMessageBox.YesButton.Click += (o, e) => MirMessageBox.Hide();
                    MirMessageBox.NoButton.Click += (o, e) => MirMessageBox.Hide();
                    break;
            }
            MirMessageBox.Show();
        }

        public sealed class CharacterButton : MirImageControl
        {
            public MirLabel NameLabel, LevelLabel, ClassLabel;
            public bool Selected;

            public CharacterButton(SelectScene selectScene)
            {
                Sound = SoundList.ButtonA;

                NameLabel = new MirLabel
                {
                    Location = new Point((Settings.ScreenWidth - 800) / 2 + 117, (Settings.ScreenHeight - 600) / 2 + 492 + 2),
                    Parent = selectScene,
                    NotControl = true,
                    Size = new Size(170, 18)
                };

                LevelLabel = new MirLabel
                {
                    Location = new Point((Settings.ScreenWidth - 800) / 2 + 117, (Settings.ScreenHeight - 600) / 2 + 523),
                    Parent = selectScene,
                    NotControl = true,
                    Size = new Size(30, 18)
                };

                ClassLabel = new MirLabel
                {
                    Location = new Point((Settings.ScreenWidth - 800) / 2 + 117, (Settings.ScreenHeight - 600) / 2 + 553),
                    Parent = selectScene,
                    NotControl = true,
                    Size = new Size(100, 18)
                };
            }

            public void Update(UserCharacterInfo info)
            {
                if (info == null)
                {
                    Index = 44;
                    Library = Libraries.Prguse;
                    NameLabel.Text = string.Empty;
                    LevelLabel.Text = string.Empty;
                    ClassLabel.Text = string.Empty;

                    NameLabel.Visible = false;
                    LevelLabel.Visible = false;
                    ClassLabel.Visible = false;

                    return;
                }

                //Library = Libraries.Title;

                //Index = 660 + (byte)info.Job;

                //if (Selected) Index += 5;


                NameLabel.Text = info.Name;
                LevelLabel.Text = info.Level.ToString();
                ClassLabel.Text = info.Job.ToString();

                NameLabel.Visible = true;
                LevelLabel.Visible = true;
                ClassLabel.Visible = true;
            }
        }

        #region Disposable
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Background = null;
                Title = null;
                ServerLabel = null;
                DscStart = null;
                DscNewChr = null;
                DscDelChr = null;
                DscCredits = null;
                ExitGame = null;
                CharacterButtons[0] = null;
                CharacterButtons[1] = null;
                CharacterButtons = null;
                MirMessageBox = null;
            }
            base.Dispose(disposing);
        }
        #endregion
    }

    public class SelChar
    {
        public bool Valid;
        public UserCharacterInfo UserChr;
        public bool Selected;
        public bool FreezeState;
        public bool Unfreezing;
        public bool Freezing;
        public int AniIndex;
        public int DarkLevel;
        public int EffIndex;
        public int StartTime;
        public int moretime;
        public int startefftime;
    }

    public class UserCharacterInfo
    {
        public string Name;
        public byte Job;
        public byte Hair;
        public byte Level;
        public byte Sex;
    }
}