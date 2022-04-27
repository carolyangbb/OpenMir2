using MirClient.MirControls;
using MirClient.MirGraphics;
using MirClient.MirSounds;
using Color = SharpDX.Color;
using WColor = System.Drawing.Color;
using Point = System.Drawing.Point;
 
namespace MirClient.MirScenes.Game
{
    public sealed class MainDialog : MirImageControl
    {
        public MirImageControl ExperienceBar, WeightBar, LeftCap, RightCap;
        public MirButton InventoryButton, CharacterButton, SkillButton;
        public MirControl HealthOrb;
        public MirLabel HealthLabel, ManaLabel, TopLabel, BottomLabel, LevelLabel, CharacterName, ExperienceLabel, GoldLabel, WeightLabel, SpaceLabel, AModeLabel, PModeLabel, SModeLabel;
        public MirButton HeroMenuButton, HeroSummonButton;

        public MainDialog()
        {
            Index = 1;
            Library = Libraries.Prguse;
            Location = new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)), Settings.ScreenHeight - Size.Height);
            PixelDetect = true;

            //LeftCap = new MirImageControl
            //{
            //    Index = 12,
            //    Library = Libraries.Prguse,
            //    Location = new Point(-67, this.Size.Height - 96),
            //    Parent = this,
            //    Visible = false
            //};

            //RightCap = new MirImageControl
            //{
            //    Index = 13,
            //    Library = Libraries.Prguse,
            //    Location = new Point(1024, this.Size.Height - 104),
            //    Parent = this,
            //    Visible = false
            //};

            //if (Settings.Resolution > 1024)
            //{
            //    LeftCap.Visible = true;
            //    RightCap.Visible = true;
            //}

            //InventoryButton = new MirButton
            //{
            //    HoverIndex = 1904,
            //    Index = 1903,
            //    Library = Libraries.Prguse,
            //    Location = new Point(this.Size.Width - 96, 76),
            //    Parent = this,
            //    PressedIndex = 1905,
            //    Sound = SoundList.ButtonA
            //};
            //InventoryButton.Click += (o, e) =>
            //{
  
            //};

            //CharacterButton = new MirButton
            //{
            //    HoverIndex = 1901,
            //    Index = 1900,
            //    Library = Libraries.Prguse,
            //    Location = new Point(this.Size.Width - 119, 76),
            //    Parent = this,
            //    PressedIndex = 1902,
            //    Sound = SoundList.ButtonA
            //};
            //CharacterButton.Click += (o, e) =>
            //{
            //    //if (GameScene.Scene.CharacterDialog.Visible && GameScene.Scene.CharacterDialog.CharacterPage.Visible)
            //    //    GameScene.Scene.CharacterDialog.Hide();
            //    //else
            //    //{
            //    //    GameScene.Scene.CharacterDialog.Show();
            //    //    GameScene.Scene.CharacterDialog.ShowCharacterPage();
            //    //}
            //};

            //SkillButton = new MirButton
            //{
            //    HoverIndex = 1907,
            //    Index = 1906,
            //    Library = Libraries.Prguse,
            //    Location = new Point(this.Size.Width - 73, 76),
            //    Parent = this,
            //    PressedIndex = 1908,
            //    Sound = SoundList.ButtonA
            //};
       
            //HealthOrb = new MirControl
            //{
            //    Parent = this,
            //    Location = new Point(0, 30),
            //    NotControl = true,
            //};

            //HealthOrb.BeforeDraw += HealthOrb_BeforeDraw;

            //HealthLabel = new MirLabel
            //{
            //    AutoSize = true,
            //    Location = new Point(0, 27),
            //    Parent = HealthOrb
            //};
            //HealthLabel.SizeChanged += Label_SizeChanged;

            //ManaLabel = new MirLabel
            //{
            //    AutoSize = true,
            //    Location = new Point(0, 42),
            //    Parent = HealthOrb
            //};
            //ManaLabel.SizeChanged += Label_SizeChanged;

            //TopLabel = new MirLabel
            //{
            //    Size = new Size(85, 30),
            //    DrawFormat = TextFormatFlags.HorizontalCenter,
            //    Location = new Point(9, 20),
            //    Parent = HealthOrb,
            //};

            //BottomLabel = new MirLabel
            //{
            //    Size = new Size(85, 30),
            //    DrawFormat = TextFormatFlags.HorizontalCenter,
            //    Location = new Point(9, 50),
            //    Parent = HealthOrb,
            //};

            //LevelLabel = new MirLabel
            //{
            //    AutoSize = true,
            //    Parent = this,
            //    Location = new Point(5, 108)
            //};

            //CharacterName = new MirLabel
            //{
            //    DrawFormat = TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter,
            //    Parent = this,
            //    Location = new Point(6, 120),
            //    Size = new Size(90, 16)
            //};


            //ExperienceBar = new MirImageControl
            //{
            //    Index = Settings.Resolution != 800 ? 8 : 7,
            //    Library = Libraries.Prguse,
            //    Location = new Point(9, 143),
            //    Parent = this,
            //    DrawImage = false,
            //    NotControl = true,
            //};
            //ExperienceBar.BeforeDraw += ExperienceBar_BeforeDraw;

            //ExperienceLabel = new MirLabel
            //{
            //    AutoSize = true,
            //    Parent = ExperienceBar,
            //    NotControl = true,
            //};

            //GoldLabel = new MirLabel
            //{
            //    DrawFormat = TextFormatFlags.VerticalCenter,
            //    Font = new Font(Settings.FontName, 8F),
            //    Location = new Point(this.Size.Width - 105, 119),
            //    Parent = this,
            //    Size = new Size(99, 13),
            //    Sound = SoundList.Gold,
            //};
            //GoldLabel.Click += (o, e) =>
            //{
            //    //if (GameScene.SelectedCell == null)
            //    //    GameScene.PickedUpGold = !GameScene.PickedUpGold && GameScene.Gold > 0;
            //};

            //WeightBar = new MirImageControl
            //{
            //    Index = 76,
            //    Library = Libraries.Prguse,
            //    Location = new Point(this.Size.Width - 105, 103),
            //    Parent = this,
            //    DrawImage = false,
            //    NotControl = true,
            //};
            //WeightBar.BeforeDraw += WeightBar_BeforeDraw;

            //WeightLabel = new MirLabel
            //{
            //    Parent = this,
            //    Location = new Point(this.Size.Width - 105, 101),
            //    Size = new Size(40, 14),
            //};

            //SpaceLabel = new MirLabel
            //{
            //    Parent = this,
            //    Location = new Point(this.Size.Width - 30, 101),
            //    Size = new Size(26, 14),
            //};

            //AModeLabel = new MirLabel
            //{
            //    AutoSize = true,
            //    ForeColour = WColor.Yellow,
            //    OutLineColour = WColor.Black,
            //    Parent = this,
            //    Location = new Point(Settings.Resolution != 800 ? 899 : 675, Settings.Resolution != 800 ? -448 : -280),
            //    Visible = Settings.ModeView
            //};

            //PModeLabel = new MirLabel
            //{
            //    AutoSize = true,
            //    ForeColour = WColor.Orange,
            //    OutLineColour = WColor.Black,
            //    Parent = this,
            //    Location = new Point(230, 125),
            //    Visible = Settings.ModeView
            //};

            //SModeLabel = new MirLabel
            //{
            //    AutoSize = true,
            //    ForeColour = WColor.LimeGreen,
            //    OutLineColour = WColor.Black,
            //    Parent = this,
            //    Location = new Point(Settings.Resolution != 800 ? 899 : 675, Settings.Resolution != 800 ? -463 : -295),
            //    Visible = Settings.ModeView
            //};
        }

        public void Process()
        {
            //if (Settings.HPView)
            //{
            //    HealthLabel.Text = string.Format("HP {0}/{1}", User.HP, User.Stats[Stat.HP]);
            //    ManaLabel.Text = HPOnly ? "" : string.Format("MP {0}/{1} ", User.MP, User.Stats[Stat.MP]);
            //    TopLabel.Text = string.Empty;
            //    BottomLabel.Text = string.Empty;
            //}
            //else
            //{
            //    if (HPOnly)
            //    {
            //        TopLabel.Text = string.Format("{0}\n" + "--", User.HP);
            //        BottomLabel.Text = string.Format("{0}", User.Stats[Stat.HP]);
            //    }
            //    else
            //    {
            //        TopLabel.Text = string.Format(" {0}    {1} \n" + "---------------", User.HP, User.MP);
            //        BottomLabel.Text = string.Format(" {0}    {1} ", User.Stats[Stat.HP], User.Stats[Stat.MP]);
            //    }
            //    HealthLabel.Text = string.Empty;
            //    ManaLabel.Text = string.Empty;
            //}

            //LevelLabel.Text = User.Level.ToString();
            //ExperienceLabel.Text = string.Format("{0:#0.##%}", User.Experience / (double)User.MaxExperience);
            //ExperienceLabel.Location = new Point((ExperienceBar.Size.Width / 2) - 20, -10);
            //GoldLabel.Text = GameScene.Gold.ToString("###,###,##0");
            //CharacterName.Text = User.Name;
            //SpaceLabel.Text = User.Inventory.Count(t => t == null).ToString();
            //WeightLabel.Text = (MapObject.User.Stats[Stat.BagWeight] - MapObject.User.CurrentBagWeight).ToString();
        }

        private void Label_SizeChanged(object sender, EventArgs e)
        {
            if (!(sender is MirLabel l)) return;

            l.Location = new Point(50 - (l.Size.Width / 2), l.Location.Y);
        }

        private void HealthOrb_BeforeDraw(object sender, EventArgs e)
        {
            //if (Libraries.Prguse == null) return;

            //int height;
            //if (User != null && User.HP != User.Stats[Stat.HP])
            //    height = (int)(80 * User.HP / (float)User.Stats[Stat.HP]);
            //else
            //    height = 80;

            //if (height < 0) height = 0;
            //if (height > 80) height = 80;

            //int orbImage = 4;

            //bool hpOnly = false;

            //if (HPOnly)
            //{
            //    hpOnly = true;
            //    orbImage = 6;
            //}

            //RawRectangle r = new RawRectangle(0, 80 - height, hpOnly ? 100 : 50, height);
            //Libraries.Prguse.Draw(orbImage, r, new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)), HealthOrb.DisplayLocation.Y + 80 - height), Color4.White, false);

            //if (hpOnly) return;

            //if (User.MP != User.Stats[Stat.MP])
            //    height = (int)(80 * User.MP / (float)User.Stats[Stat.MP]);
            //else
            //    height = 80;

            //if (height < 0) height = 0;
            //if (height > 80) height = 80;
            //r = new RawRectangle(51, 80 - height, 50, height);

            //Libraries.Prguse.Draw(4, r, new Point(((Settings.ScreenWidth / 2) - (Size.Width / 2)) + 51, HealthOrb.DisplayLocation.Y + 80 - height), Color4.White, false);
        }

        private void ExperienceBar_BeforeDraw(object sender, EventArgs e)
        {
            //if (ExperienceBar.Library == null) return;

            //double percent = MapObject.User.Experience / (double)MapObject.User.MaxExperience;
            //if (percent > 1) percent = 1;
            //if (percent <= 0) return;

            ////RawRectangle section = new RawRectangle
            ////{
            ////    Size = new Size((int)((ExperienceBar.Size.Width - 3) * percent), ExperienceBar.Size.Height)
            ////};

            //ExperienceBar.Library.Draw(ExperienceBar.Index, section, ExperienceBar.DisplayLocation, Color4.White, false);
        }

        private void WeightBar_BeforeDraw(object sender, EventArgs e)
        {
            //if (WeightBar.Library == null) return;
            //double percent = MapObject.User.CurrentBagWeight / (double)MapObject.User.Stats[Stat.BagWeight];
            //if (percent > 1) percent = 1;
            //if (percent <= 0) return;

            //RawRectangle section = new RawRectangle
            //{
            //    Size = new Size((int)((WeightBar.Size.Width - 2) * percent), WeightBar.Size.Height)
            //};

            //WeightBar.Library.Draw(WeightBar.Index, section, WeightBar.DisplayLocation, Color4.White, false);
        }
    }

}
