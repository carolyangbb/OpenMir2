using MirClient.MirControls;
using MirClient.MirGraphics;
using MirClient.MirObjects.Effects;
using MirClient.MirScenes;
using MirClient.MirSounds;
using SharpDX;
using Color = SharpDX.Color;
using WColor = System.Drawing.Color;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

namespace MirClient.MirObjects
{
    class MonsterObject : MapObject
    {
        public override ObjectType Race
        {
            get { return ObjectType.Monster; }
        }

        public override bool Blocking
        {
            get { return AI == 64 || (AI == 81 && Direction == (MirDirection)6) ? false : !Dead; }
        }

        public Point ManualLocationOffset
        {
            get
            {
                switch (BaseImage)
                {
                    case Monster.EvilMir:
                        return new Point(-21, -15);
                    case Monster.PalaceWall2:
                    case Monster.PalaceWallLeft:
                    case Monster.PalaceWall1:
                    case Monster.GiGateSouth:
                    case Monster.GiGateWest:
                    case Monster.SSabukWall1:
                    case Monster.SSabukWall2:
                    case Monster.SSabukWall3:
                        return new Point(-10, 0);
                    case Monster.GiGateEast:
                        return new Point(-45, 7);
                    default:
                        return new Point(0, 0);
                }
            }
        }

        public Monster BaseImage;
        public byte Effect;
        public bool Skeleton;

        public FrameSet Frames = new FrameSet();
        public Frame Frame;
        public int FrameIndex, FrameInterval, EffectFrameIndex, EffectFrameInterval;

        public uint TargetID;
        public Point TargetPoint;

        public bool Stoned;
        public byte Stage;
        public int BaseSound;

        public long ShockTime;
        public bool BindingShotCenter;

        public WColor OldNameColor;

        public MonsterObject(uint objectID) : base(objectID) { }

        public void ProcessBuffs()
        {
            for (int i = 0; i < Buffs.Count; i++)
            {
                AddBuffEffect(Buffs[i]);
            }
        }

        public override void Process()
        {
            bool update = GameFrm.Time >= NextMotion || GameScene.CanMove;
            SkipFrames = ActionFeed.Count > 1;

            ProcessFrames();

            if (Frame == null)
            {
                DrawFrame = 0;
                DrawWingFrame = 0;
            }
            else
            {
                DrawFrame = Frame.Start + (Frame.OffSet * (byte)Direction) + FrameIndex;
                DrawWingFrame = Frame.EffectStart + (Frame.EffectOffSet * (byte)Direction) + EffectFrameIndex;
            }


            #region Moving OffSet

            switch (CurrentAction)
            {
                case MirAction.Walking:
                case MirAction.Running:
                case MirAction.Pushed:
                case MirAction.Jump:
                case MirAction.DashL:
                case MirAction.DashR:
                case MirAction.DashAttack:
                    if (Frame == null)
                    {
                        OffSetMove = Point.Empty;
                        Movement = CurrentLocation;
                        break;
                    }
                    int i = CurrentAction == MirAction.Running ? 2 : 1;

                    if (CurrentAction == MirAction.Jump) i = -JumpDistance;
                    if (CurrentAction == MirAction.DashAttack) i = JumpDistance;

                    Movement = Functions.PointMove(CurrentLocation, Direction, CurrentAction == MirAction.Pushed ? 0 : -i);

                    int count = Frame.Count;
                    int index = FrameIndex;

                    if (CurrentAction == MirAction.DashR || CurrentAction == MirAction.DashL)
                    {
                        count = 3;
                        index %= 3;
                    }

                    switch (Direction)
                    {
                        case MirDirection.Up:
                            OffSetMove = new Point(0, (int)((MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                        case MirDirection.UpRight:
                            OffSetMove = new Point((int)((-MapControl.CellWidth * i / (float)(count)) * (index + 1)), (int)((MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                        case MirDirection.Right:
                            OffSetMove = new Point((int)((-MapControl.CellWidth * i / (float)(count)) * (index + 1)), 0);
                            break;
                        case MirDirection.DownRight:
                            OffSetMove = new Point((int)((-MapControl.CellWidth * i / (float)(count)) * (index + 1)), (int)((-MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                        case MirDirection.Down:
                            OffSetMove = new Point(0, (int)((-MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                        case MirDirection.DownLeft:
                            OffSetMove = new Point((int)((MapControl.CellWidth * i / (float)(count)) * (index + 1)), (int)((-MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                        case MirDirection.Left:
                            OffSetMove = new Point((int)((MapControl.CellWidth * i / (float)(count)) * (index + 1)), 0);
                            break;
                        case MirDirection.UpLeft:
                            OffSetMove = new Point((int)((MapControl.CellWidth * i / (float)(count)) * (index + 1)), (int)((MapControl.CellHeight * i / (float)(count)) * (index + 1)));
                            break;
                    }

                    OffSetMove = new Point(OffSetMove.X % 2 + OffSetMove.X, OffSetMove.Y % 2 + OffSetMove.Y);
                    break;
                default:
                    OffSetMove = Point.Empty;
                    Movement = CurrentLocation;
                    break;
            }

            #endregion

            DrawY = Movement.Y > CurrentLocation.Y ? Movement.Y : CurrentLocation.Y;

            DrawLocation = new Point((Movement.X - User.Movement.X + MapControl.OffSetX) * MapControl.CellWidth, (Movement.Y - User.Movement.Y + MapControl.OffSetY) * MapControl.CellHeight);
            DrawLocation.Offset(-OffSetMove.X, -OffSetMove.Y);
            DrawLocation.Offset(User.OffSetMove);
            DrawLocation = DrawLocation.Add(ManualLocationOffset);
            DrawLocation.Offset(GlobalDisplayLocationOffset);

            if (BodyLibrary != null && update)
            {
                FinalDrawLocation = DrawLocation.Add(BodyLibrary.GetOffSet(DrawFrame));
                DisplayRectangle = new Rectangle(DrawLocation, BodyLibrary.GetTrueSize(DrawFrame));
            }

            for (int i = 0; i < Effects.Count; i++)
                Effects[i].Process();

            Color colour = DrawColour;
            if (Poison == PoisonType.None)
                DrawColour = Color.White;
            if (Poison.HasFlag(PoisonType.Green))
                DrawColour = Color.Green;
            if (Poison.HasFlag(PoisonType.Red))
                DrawColour = Color.Red;
            if (Poison.HasFlag(PoisonType.Bleeding))
                DrawColour = Color.DarkRed;
            if (Poison.HasFlag(PoisonType.Slow))
                DrawColour = Color.Purple;
            if (Poison.HasFlag(PoisonType.Stun) || Poison.HasFlag(PoisonType.Dazed))
                DrawColour = Color.Yellow;
            if (Poison.HasFlag(PoisonType.Blindness))
                DrawColour = Color.MediumVioletRed;
            if (Poison.HasFlag(PoisonType.Frozen))
                DrawColour = Color.Blue;
            if (Poison.HasFlag(PoisonType.Paralysis) || Poison.HasFlag(PoisonType.LRParalysis))
                DrawColour = Color.Gray;
            if (Poison.HasFlag(PoisonType.DelayedExplosion))
                DrawColour = Color.Orange;
            if (colour != DrawColour) GameScene.Scene.MapControl.TextureValid = false;
        }

        public bool SetAction()
        {
            if (NextAction != null && !GameScene.CanMove)
            {
                switch (NextAction.Action)
                {
                    case MirAction.Walking:
                    case MirAction.Running:
                    case MirAction.Pushed:
                        return false;
                }
            }

            //IntelligentCreature
            switch (BaseImage)
            {
                case Monster.BabyPig:
                case Monster.Chick:
                case Monster.Kitten:
                case Monster.BabySkeleton:
                case Monster.Baekdon:
                case Monster.Wimaen:
                case Monster.BlackKitten:
                case Monster.BabyDragon:
                case Monster.OlympicFlame:
                case Monster.BabySnowMan:
                case Monster.Frog:
                case Monster.BabyMonkey:
                case Monster.AngryBird:
                case Monster.Foxey:
                case Monster.MedicalRat:
                    BodyLibrary = Libraries.Pets[((ushort)BaseImage) - 10000];
                    break;
            }

            if (ActionFeed.Count == 0)
            {
                CurrentAction = Stoned ? MirAction.Stoned : MirAction.Standing;
                if (CurrentAction == MirAction.Standing) CurrentAction = SitDown ? MirAction.SitDown : MirAction.Standing;

                Frames.TryGetValue(CurrentAction, out Frame);

                if (MapLocation != CurrentLocation)
                {
                    GameScene.Scene.MapControl.RemoveObject(this);
                    MapLocation = CurrentLocation;
                    GameScene.Scene.MapControl.AddObject(this);
                }

                FrameIndex = 0;

                if (Frame == null) return false;

                FrameInterval = Frame.Interval;
            }
            else
            {
                QueuedAction action = ActionFeed[0];
                ActionFeed.RemoveAt(0);

                CurrentActionLevel = 0;
                CurrentAction = action.Action;
                CurrentLocation = action.Location;
                Direction = action.Direction;

                Point temp;
                switch (CurrentAction)
                {
                    case MirAction.Walking:
                    case MirAction.Running:
                    case MirAction.Pushed:
                        int i = CurrentAction == MirAction.Running ? 2 : 1;
                        temp = Functions.PointMove(CurrentLocation, Direction, CurrentAction == MirAction.Pushed ? 0 : -i);
                        break;
                    case MirAction.Jump:
                    case MirAction.DashAttack:
                        temp = Functions.PointMove(CurrentLocation, Direction, JumpDistance);
                        break;
                    default:
                        temp = CurrentLocation;
                        break;
                }

                temp = new Point(action.Location.X, temp.Y > CurrentLocation.Y ? temp.Y : CurrentLocation.Y);

                if (MapLocation != temp)
                {
                    GameScene.Scene.MapControl.RemoveObject(this);
                    MapLocation = temp;
                    GameScene.Scene.MapControl.AddObject(this);
                }


                switch (CurrentAction)
                {
                    case MirAction.Pushed:
                        Frames.TryGetValue(MirAction.Walking, out Frame);
                        break;
                    case MirAction.Jump:
                        Frames.TryGetValue(MirAction.Jump, out Frame);
                        break;
                    case MirAction.DashAttack:
                        Frames.TryGetValue(MirAction.DashAttack, out Frame);
                        break;
                    case MirAction.AttackRange1:
                        if (!Frames.TryGetValue(CurrentAction, out Frame))
                            Frames.TryGetValue(MirAction.Attack1, out Frame);
                        break;
                    case MirAction.AttackRange2:
                        if (!Frames.TryGetValue(CurrentAction, out Frame))
                            Frames.TryGetValue(MirAction.Attack2, out Frame);
                        break;
                    case MirAction.AttackRange3:
                        if (!Frames.TryGetValue(CurrentAction, out Frame))
                            Frames.TryGetValue(MirAction.Attack3, out Frame);
                        break;
                    case MirAction.Special:
                        if (!Frames.TryGetValue(CurrentAction, out Frame))
                            Frames.TryGetValue(MirAction.Attack1, out Frame);
                        break;
                    case MirAction.Skeleton:
                        if (!Frames.TryGetValue(CurrentAction, out Frame))
                            Frames.TryGetValue(MirAction.Dead, out Frame);
                        break;
                    case MirAction.Hide:
                        switch (BaseImage)
                        {
                            case Monster.Shinsu1:
                                BodyLibrary = Libraries.Monsters[(ushort)Monster.Shinsu];
                                BaseImage = Monster.Shinsu;
                                BaseSound = (ushort)BaseImage * 10;
                                //Frames = BodyLibrary.Frames ?? FrameSet.DefaultMonster;
                                Frames.TryGetValue(CurrentAction, out Frame);
                                break;
                            default:
                                Frames.TryGetValue(CurrentAction, out Frame);
                                break;
                        }
                        break;
                    case MirAction.Dead:
                        switch (BaseImage)
                        {
                            case Monster.Shinsu:
                            case Monster.Shinsu1:
                            case Monster.HolyDeva:
                            case Monster.GuardianRock:
                            case Monster.CharmedSnake://SummonSnakes
                            case Monster.HellKnight1:
                            case Monster.HellKnight2:
                            case Monster.HellKnight3:
                            case Monster.HellKnight4:
                            case Monster.HellBomb1:
                            case Monster.HellBomb2:
                            case Monster.HellBomb3:
                                Remove();
                                return false;
                            default:
                                Frames.TryGetValue(CurrentAction, out Frame);
                                break;
                        }
                        break;
                    default:
                        Frames.TryGetValue(CurrentAction, out Frame);
                        break;

                }

                FrameIndex = 0;

                if (Frame == null) return false;

                FrameInterval = Frame.Interval;

                Point front = Functions.PointMove(CurrentLocation, Direction, 1);

                switch (CurrentAction)
                {
                    case MirAction.Appear:
                        PlaySummonSound();
                        switch (BaseImage)
                        {
                            case Monster.HellKnight1:
                            case Monster.HellKnight2:
                            case Monster.HellKnight3:
                            case Monster.HellKnight4:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)BaseImage], 448, 10, 600, this));
                                break;
                        }
                        break;
                    case MirAction.Show:
                        PlayPopupSound();
                        break;
                    case MirAction.Pushed:
                        FrameIndex = Frame.Count - 1;
                        GameScene.Scene.Redraw();
                        break;
                    case MirAction.Jump:
                        PlayJumpSound();
                        switch (BaseImage)
                        {
                            // Sanjian
                            case Monster.FurbolgGuard:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgGuard], 414 + (int)Direction * 6, 6, Frame.Count * Frame.Interval, this));
                                break;




                            case Monster.Armadillo:
                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)BaseImage], 592, 12, 800, CurrentLocation, GameFrm.Time + 500));
                                break;
                        }
                        break;
                    case MirAction.DashAttack:
                        PlayDashSound();
                        break;
                    case MirAction.Walking:
                        GameScene.Scene.Redraw();
                        break;
                    case MirAction.Running:
                        PlayRunSound();
                        GameScene.Scene.Redraw();
                        break;
                    case MirAction.Attack1:
                        PlayAttackSound();
                        CurrentActionLevel = (byte)action.Params[1];
                        switch (BaseImage)
                        {
                            case Monster.FlamingWooma:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlamingWooma], 224 + (int)Direction * 7, 7, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.ZumaTaurus:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ZumaTaurus], 244 + (int)Direction * 8, 8, 8 * FrameInterval, this));
                                break;
                            case Monster.MinotaurKing:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MinotaurKing], 272 + (int)Direction * 6, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.FlamingMutant:
                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlamingMutant], 314, 6, 600, front));
                                break;
                            case Monster.DemonWolf:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DemonWolf], 336 + (int)Direction * 9, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.AncientBringer:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AncientBringer], 512 + (int)Direction * 6, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Manticore:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Manticore], 505 + (int)Direction * 3, 3, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.YinDevilNode:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.YinDevilNode], 26, 26, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.YangDevilNode:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.YangDevilNode], 26, 26, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.GreatFoxSpirit:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GreatFoxSpirit], 355, 20, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.EvilMir:
                                Effects.Add(new Effect(Libraries.Dragon, 60, 8, 8 * Frame.Interval, this));
                                Effects.Add(new Effect(Libraries.Dragon, 68, 14, 14 * Frame.Interval, this));
                                byte random = (byte)GameFrm.Random.Next(7);
                                for (int i = 0; i <= 7 + random; i++)
                                {
                                    Point source = new Point(User.CurrentLocation.X + GameFrm.Random.Next(-7, 7), User.CurrentLocation.Y + GameFrm.Random.Next(-7, 7));

                                    MapControl.Effects.Add(new Effect(Libraries.Dragon, 230 + (GameFrm.Random.Next(5) * 10), 5, 400, source, GameFrm.Time + GameFrm.Random.Next(1000)));
                                }
                                break;
                            case Monster.CrawlerLave:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrawlerLave], 224 + (int)Direction * 6, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.HellKeeper:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellKeeper], 32, 8, 8 * Frame.Interval, this));
                                break;
                            case Monster.IcePillar:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePillar], 12, 6, 6 * 100, this));
                                break;
                            case Monster.TrollKing:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TrollKing], 288, 6, 6 * Frame.Interval, this));
                                break;
                            case Monster.HellBomb1:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 61, 7, 7 * Frame.Interval, this));
                                break;
                            case Monster.HellBomb2:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 79, 9, 9 * Frame.Interval, this));
                                break;
                            case Monster.HellBomb3:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellLord], 97, 8, 8 * Frame.Interval, this));
                                break;
                            case Monster.WitchDoctor:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WitchDoctor], 328, 20, 20 * Frame.Interval, this));
                                break;
                            case Monster.SeedingsGeneral:
                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SeedingsGeneral], 1064 + (int)Direction * 9, 9, 9 * Frame.Interval, front, GameFrm.Time));
                                break;
                            case Monster.RestlessJar:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 384, 7, 7 * Frame.Interval, this));
                                break;
                            case Monster.AssassinBird:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AssassinBird], 392, 9, 9 * Frame.Interval, this));
                                break;
                            case Monster.Nadz:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Nadz], 280 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                break;
                            case Monster.AvengingWarrior:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengingWarrior], 272 + (int)Direction * 5, 5, 7 * Frame.Interval, this));
                                break;
                            case Monster.FlyingStatue:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 334, 10, 10 * 100, this));
                                break;
                            case Monster.HoodedSummoner:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedSummoner], 352, 12, 12 * Frame.Interval, this));
                                break;
                        }
                        break;
                    case MirAction.Attack2:
                        PlaySecondAttackSound();
                        CurrentActionLevel = (byte)action.Params[1];
                        switch (BaseImage)
                        {
                            case Monster.CrystalSpider:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CrystalSpider], 272 + (int)Direction * 10, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Yimoogi:
                            case Monster.RedYimoogi:
                            case Monster.Snake10:
                            case Monster.Snake11:
                            case Monster.Snake12:
                            case Monster.Snake13:
                            case Monster.Snake14:
                            case Monster.Snake15:
                            case Monster.Snake16:
                            case Monster.Snake17:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)BaseImage], 304, 6, Frame.Count * Frame.Interval, this));
                                Effects.Add(new Effect(Libraries.Magic2, 1280, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.HellCannibal:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellCannibal], 310 + (int)Direction * 12, 12, 12 * Frame.Interval, this));
                                break;
                            case Monster.ManectricKing:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManectricKing], 640 + (int)Direction * 10, 10, 10 * 100, this));
                                break;
                            case Monster.AncientBringer:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AncientBringer], 568 + (int)Direction * 10, 10, 13 * Frame.Interval, this));
                                break;
                            case Monster.DarkBeast:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkBeast], 296 + (int)Direction * 4, 4, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.LightBeast:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.LightBeast], 296 + (int)Direction * 4, 4, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.FireCat:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FireCat], 248 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.BloodBaboon:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BloodBaboon], 312 + (int)Direction * 7, 7, 7 * Frame.Interval, this));
                                break;
                            case Monster.TwinHeadBeast:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TwinHeadBeast], 352 + (int)Direction * 7, 7, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Nadz:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Nadz], 320 + (int)Direction * 7, 7, 7 * Frame.Interval, this));
                                break;
                            case Monster.DarkCaptain:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkCaptain], 1214, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.DragonWarrior:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DragonWarrior], 576, 7, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.FlyingStatue:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 352, 10, 10 * 100, this));
                                break;
                            case Monster.HornedSorceror:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedSorceror], 552 + (int)Direction * 9, 9, 900, this));
                                break;
                            case Monster.HoodedSummoner:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedSummoner], 364, 10, 10 * Frame.Interval, this));
                                break;
                        }

                        if ((ushort)BaseImage >= 10000)
                        {
                            PlayPetSound();
                        }
                        break;
                    case MirAction.Attack3:
                        PlayThirdAttackSound();
                        CurrentActionLevel = (byte)action.Params[1];
                        switch (BaseImage)
                        {
                            case Monster.Yimoogi:
                            case Monster.RedYimoogi:
                            case Monster.Snake10:
                            case Monster.Snake11:
                            case Monster.Snake12:
                            case Monster.Snake13:
                            case Monster.Snake14:
                            case Monster.Snake15:
                            case Monster.Snake16:
                            case Monster.Snake17:
                                SoundManager.PlaySound(BaseSound + 9);
                                Effects.Add(new Effect(Libraries.Magic2, 1330, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Behemoth:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Behemoth], 697 + (int)Direction * 7, 7, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.SandSnail:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SandSnail], 448, 9, 900, this) { Blend = true });
                                break;
                            case Monster.PeacockSpider:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PeacockSpider], 755, 21, 21 * Frame.Interval, this));
                                break;
                            case Monster.DragonWarrior:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DragonWarrior], 632 + (int)Direction * 4, 4, 8 * Frame.Interval, this));
                                break;
                        }
                        break;
                    case MirAction.Attack4:
                        PlayFourthAttackSound();
                        CurrentActionLevel = (byte)action.Params[1];
                        switch (BaseImage)
                        {
                            case Monster.DarkOmaKing:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkOmaKing], 1702, 13, 13 * Frame.Interval, this));
                                break;
                        }
                        break;
                    case MirAction.Attack5:
                        PlayFithAttackSound();
                        CurrentActionLevel = (byte)action.Params[1];
                        break;
                    case MirAction.AttackRange1:
                        PlayRangeSound();
                        TargetID = (uint)action.Params[0];
                        CurrentActionLevel = (byte)action.Params[4];
                        switch (BaseImage)
                        {
                            case Monster.KingScorpion:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingScorpion], 272 + (int)Direction * 8, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.DarkDevil:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkDevil], 272 + (int)Direction * 8, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.ShamanZombie:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShamanZombie], 232 + (int)Direction * 12, 6, Frame.Count * Frame.Interval, this));
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShamanZombie], 328, 12, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.GuardianRock:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GuardianRock], 12, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.GreatFoxSpirit:
                                byte random = (byte)GameFrm.Random.Next(4);
                                for (int i = 0; i <= 4 + random; i++)
                                {
                                    Point source = new Point(User.CurrentLocation.X + GameFrm.Random.Next(-7, 7), User.CurrentLocation.Y + GameFrm.Random.Next(-7, 7));

                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GreatFoxSpirit], 375 + (GameFrm.Random.Next(3) * 20), 20, 1400, source, GameFrm.Time + GameFrm.Random.Next(600)));
                                }
                                break;
                            case Monster.EvilMir:
                                Effects.Add(new Effect(Libraries.Dragon, 90 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.DragonStatue:
                                Effects.Add(new Effect(Libraries.Dragon, 310 + ((int)Direction / 3) * 20, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.TurtleKing:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TurtleKing], 946, 10, Frame.Count * Frame.Interval, User));
                                break;
                            case Monster.HellBolt:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellBolt], 304, 11, 11 * 100, this));
                                break;
                            case Monster.WitchDoctor:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WitchDoctor], 304, 9, 9 * 100, this));
                                break;
                            case Monster.FlyingStatue:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 304, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.ManectricKing:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManectricKing], 720, 12, 12 * 100, this));
                                break;
                            case Monster.IcePillar:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePillar], 26, 6, 8 * 100, this) { Start = GameFrm.Time + 750 });
                                break;
                            case Monster.ElementGuard:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ElementGuard], 320 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                break;
                            case Monster.KingGuard:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingGuard], 737, 9, 9 * Frame.Interval, this));
                                break;
                            case Monster.CatShaman:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CatShaman], 738, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.SeedingsGeneral:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SeedingsGeneral], 1184 + (int)Direction * 9, 9, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.RestlessJar:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 471, 5, 500, this));
                                break;
                            case Monster.AvengingSpirit:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengingSpirit], 344 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                break;
                            case Monster.ClawBeast:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ClawBeast], 504 + (int)Direction * 8, 8, 8 * Frame.Interval, this));
                                break;
                            case Monster.DarkCaptain:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkCaptain], 1200, 13, 13 * Frame.Interval, this));
                                break;
                            case Monster.FloatingRock:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FloatingRock], 216, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.FrozenKnight:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenKnight], 384 + (int)Direction * 9, 9, 9 * Frame.Interval, this));
                                break;
                            case Monster.IcePhantom:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 672, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.BlackTortoise:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackTortoise], 404 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                break;
                            case Monster.AssassinScroll:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AssassinScroll], 296, 3, 3 * Frame.Interval, this));
                                break;
                            case Monster.PurpleFaeFlower:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PurpleFaeFlower], 328, 3, 3 * Frame.Interval, this));
                                break;
                            case Monster.WarriorScroll:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WarriorScroll], 296, 8, 8 * Frame.Interval, this));
                                break;
                            case Monster.WizardScroll:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WizardScroll], 296, 4, 4 * Frame.Interval, this));
                                break;
                        }
                        break;
                    case MirAction.AttackRange2:
                        PlaySecondRangeSound();
                        TargetID = (uint)action.Params[0];
                        CurrentActionLevel = (byte)action.Params[4];
                        switch (BaseImage)
                        {
                            case Monster.TurtleKing:
                                byte random = (byte)GameFrm.Random.Next(4);
                                for (int i = 0; i <= 4 + random; i++)
                                {
                                    Point source = new Point(User.CurrentLocation.X + GameFrm.Random.Next(-7, 7), User.CurrentLocation.Y + GameFrm.Random.Next(-7, 7));

                                    Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.TurtleKing], GameFrm.Random.Next(2) == 0 ? 922 : 934, 12, 1200, source, GameFrm.Time + GameFrm.Random.Next(600));
                                    //ef.Played += (o, e) => SoundManager.PlaySound(20000 + (ushort)Spell.HellFire * 10 + 1);
                                    MapControl.Effects.Add(ef);
                                }
                                break;
                            case Monster.SeedingsGeneral:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SeedingsGeneral], 1256, 9, 900, this));
                                break;
                            case Monster.PeacockSpider: //BROKEN
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PeacockSpider], 776, 30, 30 * Frame.Interval, this));
                                break;
                            case Monster.DarkCaptain:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkCaptain], 1234, 13, 13 * Frame.Interval, this));
                                break;
                            case Monster.IcePhantom:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 672, 10, 10 * Frame.Interval, this));
                                break;
                        }
                        break;
                    case MirAction.AttackRange3:
                        PlayThirdRangeSound();
                        TargetID = (uint)action.Params[0];
                        CurrentActionLevel = (byte)action.Params[4];
                        switch (BaseImage)
                        {
                            case Monster.TurtleKing:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TurtleKing], 946, 10, Frame.Count * Frame.Interval, User));
                                break;
                        }
                        break;
                    case MirAction.Struck:
                        uint attackerID = (uint)action.Params[0];
                        StruckWeapon = -2;
                        for (int i = 0; i < MapControl.Objects.Count; i++)
                        {
                            MapObject ob = MapControl.Objects[i];
                            if (ob.ObjectID != attackerID) continue;
                            if (ob.Race != ObjectType.Player) break;
                            PlayerObject player = ((PlayerObject)ob);
                            StruckWeapon = player.Weapon;
                            if (StruckWeapon == -1) break; //Archer?
                            StruckWeapon = 1;
                            break;
                        }
                        PlayFlinchSound();
                        PlayStruckSound();


                        // Sanjian
                        switch (BaseImage)
                        {
                            case Monster.GlacierBeast:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GlacierBeast], 304, 6, 400, this));
                                break;
                        }



                        break;
                    case MirAction.Die:
                        switch (BaseImage)
                        {
                            case Monster.ManectricKing:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManectricKing], 504 + (int)Direction * 9, 9, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.DarkDevil:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkDevil], 336, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.ShamanZombie:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ShamanZombie], 224, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.RoninGhoul:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RoninGhoul], 224, 10, Frame.Count * FrameInterval, this));
                                break;
                            case Monster.BoneCaptain:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BoneCaptain], 224 + (int)Direction * 10, 10, Frame.Count * FrameInterval, this));
                                break;
                            case Monster.RightGuard:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RightGuard], 296, 5, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.LeftGuard:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.LeftGuard], 296 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                break;
                            case Monster.FrostTiger:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrostTiger], 304, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Yimoogi:
                            case Monster.RedYimoogi:
                            case Monster.Snake10:
                            case Monster.Snake11:
                            case Monster.Snake12:
                            case Monster.Snake13:
                            case Monster.Snake14:
                            case Monster.Snake15:
                            case Monster.Snake16:
                            case Monster.Snake17:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Yimoogi], 352, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.YinDevilNode:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.YinDevilNode], 52, 20, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.YangDevilNode:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.YangDevilNode], 52, 20, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.BlackFoxman:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackFoxman], 224, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.VampireSpider:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.VampireSpider], 296, 5, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.CharmedSnake:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CharmedSnake], 40, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.Manticore:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Manticore], 592, 9, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.ValeBat:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ValeBat], 224, 20, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.SpiderBat:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SpiderBat], 224, 20, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.VenomWeaver:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.VenomWeaver], 224, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.HellBolt:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellBolt], 325, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.SabukGate:
                                Effects.Add(new Effect(Libraries.Gates[(ushort)Monster.SabukGate - 950], 24, 10, Frame.Count * Frame.Interval, this) { Light = -1 });
                                break;
                            case Monster.WingedTigerLord:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WingedTigerLord], 650 + (int)Direction * 5, 5, Frame.Count * FrameInterval, this));
                                break;
                            case Monster.HellKnight1:
                            case Monster.HellKnight2:
                            case Monster.HellKnight3:
                            case Monster.HellKnight4:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)BaseImage], 448, 10, 600, this));
                                break;
                            case Monster.IceGuard:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IceGuard], 256, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.DeathCrawler:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DeathCrawler], 313, 11, Frame.Count * Frame.Interval, this));
                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DeathCrawler], 304, 9, 900, CurrentLocation, GameFrm.Time + 900));
                                break;
                            case Monster.BurningZombie:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BurningZombie], 373, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.FrozenZombie:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenZombie], 360, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.EarthGolem:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.EarthGolem], 432, 9, 9 * Frame.Interval, this));
                                break;
                            case Monster.CreeperPlant:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CreeperPlant], 266, 6, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.SackWarrior:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SackWarrior], 384, 9, Frame.Count * Frame.Interval, this));
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SackWarrior], 393, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.FrozenSoldier:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenSoldier], 256, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.FrozenGolem:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenGolem], 456, 7, Frame.Count * Frame.Interval, this));
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenGolem], 463, 10, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.DragonWarrior:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DragonWarrior], 504 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                break;
                            case Monster.FloatingRock:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FloatingRock], 152, 8, Frame.Count * Frame.Interval, this));
                                break;
                            case Monster.AvengingSpirit:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengingSpirit], 442 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                break;
                            case Monster.TaoistScroll:
                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TaoistScroll], 282, 11, 11 * Frame.Interval, this));
                                break;
                        }
                        PlayDieSound();
                        break;
                    case MirAction.Dead:
                        GameScene.Scene.Redraw();
                        GameScene.Scene.MapControl.SortObject(this);
                        if (MouseObject == this) MouseObjectID = 0;
                        if (TargetObject == this) TargetObjectID = 0;
                        if (MagicObject == this) MagicObjectID = 0;

                        for (int i = 0; i < Effects.Count; i++)
                            Effects[i].Remove();

                        DeadTime = GameFrm.Time;

                        break;
                }

            }

            GameScene.Scene.MapControl.TextureValid = false;

            NextMotion = GameFrm.Time + FrameInterval;

            return true;
        }

        public void SetCurrentEffects()
        {
            //BindingShot
            if (BindingShotCenter && ShockTime > GameFrm.Time)
            {
                int effectid = TrackableEffect.GetOwnerEffectID(ObjectID);
                if (effectid >= 0)
                    TrackableEffect.effectlist[effectid].RemoveNoComplete();

                TrackableEffect NetDropped = new TrackableEffect(new Effect(Libraries.MagicC, 7, 1, 1000, this) { Repeat = true, RepeatUntil = (ShockTime - 1500) });
                NetDropped.Complete += (o1, e1) =>
                {
                    SoundManager.PlaySound(20000 + 130 * 10 + 6);//sound M130-6
                    Effects.Add(new TrackableEffect(new Effect(Libraries.MagicC, 8, 8, 700, this)));
                };
                Effects.Add(NetDropped);
            }
            else if (BindingShotCenter && ShockTime <= GameFrm.Time)
            {
                int effectid = TrackableEffect.GetOwnerEffectID(ObjectID);
                if (effectid >= 0)
                    TrackableEffect.effectlist[effectid].Remove();

                //SoundManager.PlaySound(20000 + 130 * 10 + 6);//sound M130-6
                //Effects.Add(new TrackableEffect(new Effect(Libraries.ArcherMagic, 8, 8, 700, this)));

                ShockTime = 0;
                BindingShotCenter = false;
            }
        }


        private void ProcessFrames()
        {
            if (Frame == null) return;

            switch (CurrentAction)
            {
                case MirAction.Walking:
                    if (!GameScene.CanMove) return;

                    GameScene.Scene.MapControl.TextureValid = false;

                    if (SkipFrames) UpdateFrame();

                    if (UpdateFrame() >= Frame.Count)
                    {
                        FrameIndex = Frame.Count - 1;
                        SetAction();
                    }
                    else
                    {
                        switch (FrameIndex)
                        {
                            case 1:
                                PlayWalkSound(true);
                                break;
                            case 4:
                                PlayWalkSound(false);
                                break;
                        }
                    }
                    break;
                case MirAction.Running:
                    if (!GameScene.CanMove) return;

                    GameScene.Scene.MapControl.TextureValid = false;

                    if (SkipFrames) UpdateFrame();

                    if (UpdateFrame() >= Frame.Count)
                    {
                        FrameIndex = Frame.Count - 1;
                        SetAction();
                    }
                    break;
                case MirAction.Pushed:
                    if (!GameScene.CanMove) return;

                    GameScene.Scene.MapControl.TextureValid = false;

                    FrameIndex -= 2;

                    if (FrameIndex < 0)
                    {
                        FrameIndex = 0;
                        SetAction();
                    }
                    break;
                case MirAction.Jump:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.DashAttack:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.HornedSorceror:
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedSorceror], 644 + (int)Direction * 5, 5, 500, CurrentLocation));
                                                break;
                                        }
                                    }
                                    break;
                            }
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.Show:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            switch (BaseImage)
                            {
                                case Monster.ZumaStatue:
                                case Monster.ZumaGuardian:
                                case Monster.RedThunderZuma:
                                case Monster.FrozenRedZuma:
                                case Monster.FrozenZumaStatue:
                                case Monster.FrozenZumaGuardian:
                                case Monster.ZumaTaurus:
                                case Monster.DemonGuard:
                                case Monster.EarthGolem:
                                case Monster.Turtlegrass:
                                case Monster.ManTree:
                                case Monster.AssassinScroll:
                                case Monster.WarriorScroll:
                                case Monster.TaoistScroll:
                                case Monster.WizardScroll:
                                case Monster.PurpleFaeFlower:
                                    Stoned = false;
                                    break;
                                case Monster.Shinsu:
                                    BodyLibrary = Libraries.Monsters[(ushort)Monster.Shinsu1];
                                    BaseImage = Monster.Shinsu1;
                                    BaseSound = (ushort)BaseImage * 10;
                                    //Frames = BodyLibrary.Frames ?? FrameSet.DefaultMonster;
                                    break;
                            }

                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.CreeperPlant:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CreeperPlant], 250, 6, 6 * 100, this));
                                                break;
                                        }
                                        break;
                                    }
                            }

                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.Hide:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            switch (BaseImage)
                            {
                                case Monster.CannibalPlant:
                                case Monster.WaterDragon:
                                case Monster.CreeperPlant:
                                case Monster.EvilCentipede:
                                case Monster.DigOutZombie:
                                case Monster.Armadillo:
                                case Monster.ArmadilloElder:
                                    Remove();
                                    return;
                                case Monster.ZumaStatue:
                                case Monster.ZumaGuardian:
                                case Monster.RedThunderZuma:
                                case Monster.FrozenRedZuma:
                                case Monster.FrozenZumaStatue:
                                case Monster.FrozenZumaGuardian:
                                case Monster.ZumaTaurus:
                                case Monster.DemonGuard:
                                case Monster.EarthGolem:
                                case Monster.Turtlegrass:
                                case Monster.ManTree:
                                case Monster.AssassinScroll:
                                case Monster.WarriorScroll:
                                case Monster.TaoistScroll:
                                case Monster.WizardScroll:
                                case Monster.PurpleFaeFlower:
                                    Stoned = true;
                                    return;
                            }


                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.Appear:
                case MirAction.Standing:
                case MirAction.Stoned:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            if (CurrentAction == MirAction.Standing)
                            {
                                switch (BaseImage)
                                {
                                    case Monster.SnakeTotem://SummonSnakes Totem
                                        if (TrackableEffect.GetOwnerEffectID(this.ObjectID, "SnakeTotem") < 0)
                                            Effects.Add(new TrackableEffect(new Effect(Libraries.Monsters[(ushort)Monster.SnakeTotem], 2, 10, 1500, this) { Repeat = true }, "SnakeTotem"));
                                        break;
                                    case Monster.PalaceWall1:
                                        //Effects.Add(new Effect(Libraries.Effect, 196, 1, 1000, this) { DrawBehind = true, d });
                                        //Libraries.Effect.Draw(196, DrawLocation, Color4.White, true);
                                        //Libraries.Effect.DrawBlend(196, DrawLocation, Color4.White, true);
                                        break;
                                }
                            }

                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.Attack1:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        Point front = Functions.PointMove(CurrentLocation, Direction, 1);

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            if (SetAction())
                            {
                                switch (BaseImage)
                                {
                                    case Monster.EvilCentipede:
                                        Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.EvilCentipede], 42, 10, 600, this));
                                        break;
                                    case Monster.ToxicGhoul:
                                        SoundManager.PlaySound(BaseSound + 4);
                                        Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ToxicGhoul], 224 + (int)Direction * 6, 6, 600, this));
                                        break;
                                }
                            }
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.Kirin:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Kirin], 776 + (int)Direction * 5, 5, 500, this));
                                                break;
                                            case Monster.Bear:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Bear], 321 + (int)Direction * 4, 4, Frame.Count * Frame.Interval, this));
                                                break;
                                            case Monster.Jar1:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Jar1], 128 + (int)Direction * 3, 3, 300, this));
                                                break;
                                            case Monster.Jar2:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Jar2], 624 + (int)Direction * 8, 8, 800, this));
                                                break;
                                            case Monster.AntCommander:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AntCommander], 368 + (int)Direction * 8, 8, 8 * Frame.Interval, this));
                                                break;
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        switch (BaseImage)
                                        {
                                            // Sanjian
                                            case Monster.FurbolgCommander:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgCommander], 325 + (int)Direction * 4, 4, 4 * Frame.Interval, this));
                                                break;



                                            case Monster.StainHammerCat:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StainHammerCat], 240 + (int)Direction * 4, 4, Frame.Count * Frame.Interval, this));
                                                break;
                                            case Monster.HornedWarrior:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedWarrior], 752 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.FrozenFighter:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenFighter], 336 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.PurpleFaeFlower: // Slash effect on mob
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PurpleFaeFlower], 436 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                        }
                                    }
                                    break;
                                case 3:
                                    {
                                        PlaySwingSound();
                                        switch (BaseImage)
                                        {
                                            // Sanjian
                                            case Monster.Furball:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Furball], 256 + (int)Direction * 4, 4, 4 * Frame.Interval, this));
                                                break;

                                            case Monster.FurbolgWarrior:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgWarrior], 320 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                                break;





                                            case Monster.RightGuard:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RightGuard], 272 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.LeftGuard:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.LeftGuard], 272 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.Shinsu1:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Shinsu1], 224 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.DeathCrawler:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DeathCrawler], 248 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.FrozenZombie:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenZombie], 312 + (int)Direction * 5, 5, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.TucsonWarrior:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonWarrior], 296 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.ArmadilloElder:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ArmadilloElder], 488 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.Mandrill:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Mandrill], 264 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.ArmedPlant:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ArmedPlant], 256 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.AvengerPlant:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengerPlant], 224 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.AvengingSpirit:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengingSpirit], 280 + (int)Direction * 8, 8, 8 * Frame.Interval, this));
                                                break;
                                            case Monster.SackWarrior:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SackWarrior], 344 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.Bear:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Bear], 321 + (int)Direction * 4, 4, 4 * Frame.Interval, this));
                                                break;
                                            case Monster.FrozenKnight:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenKnight], 360 + (int)Direction * 3, 3, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.IcePhantom:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 640 + (int)Direction * 4, 4, 4 * Frame.Interval, this));
                                                break;
                                            case Monster.DragonWarrior:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DragonWarrior], 552 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.BurningZombie:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BurningZombie], 312 + (int)Direction * 5, 5, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.DarkCaptain:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkCaptain], 1168 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.BlackTortoise:
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackTortoise], 360, 6, 6 * Frame.Interval, front, GameFrm.Time));
                                                break;
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.GeneralMeowMeow:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GeneralMeowMeow], 416 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                                break;
                                            case Monster.Armadillo:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Armadillo], 480 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.StainHammerCat:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StainHammerCat], 272 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.StrayCat:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StrayCat], 528 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                                break;
                                            case Monster.OmaSlasher:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaSlasher], 304 + (int)Direction * 4, 4, 4 * Frame.Interval, this));
                                                break;
                                            case Monster.AvengerPlant:
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AvengerPlant], 248, 6, 600, front, GameFrm.Time));
                                                break;
                                            case Monster.ManTree:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManTree], 472 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.HornedMage:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedMage], 783, 9, 800, this));
                                                break;
                                            case Monster.DarkWraith:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkWraith], 720 + (int)Direction * 3, 3, 300, this));
                                                Effect darkWraithEffect = new Effect(Libraries.Monsters[(ushort)Monster.DarkWraith], 744, 6, 600, front, GameFrm.Time);
                                                MapControl.Effects.Add(darkWraithEffect);
                                                break;
                                            case Monster.FightingCat:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FightingCat], 208 + (int)Direction * 3, 3, 4 * Frame.Interval, this) { Blend = true });
                                                break;
                                            case Monster.FlamingMutant:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlamingMutant], 304, 6, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.DarkOmaKing:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkOmaKing], 1568 + (int)Direction * 4, 4, 4 * Frame.Interval, this));
                                                break;
                                            case Monster.WaterDragon:
                                                Effect waterDragonEffect = new Effect(Libraries.Monsters[(ushort)Monster.WaterDragon], 905, 9, 900, front, GameFrm.Time + 300);
                                                MapControl.Effects.Add(waterDragonEffect);
                                                break;
                                            case Monster.PurpleFaeFlower: // Animation on the target.
                                                Effect purpleFaeFlowerEffect = new Effect(Libraries.Monsters[(ushort)Monster.PurpleFaeFlower], 483, 7, 700, front, GameFrm.Time + 300);
                                                MapControl.Effects.Add(purpleFaeFlowerEffect);
                                                break;
                                        }
                                    }
                                    break;
                                case 5:
                                    {
                                        switch (BaseImage)
                                        {
                                            // Sanjian
                                            case Monster.FurbolgGuard:
                                                MapObject ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgGuard], 384, 7, 600, ob));
                                                break;



                                            case Monster.FlyingStatue:
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 344, 8, 800, front, GameFrm.Time));
                                                break;
                                            case Monster.OmaAssassin:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaAssassin], 312 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                                break;
                                            case Monster.PlagueCrab:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PlagueCrab], 488 + (int)Direction * 7, 7, 7 * Frame.Interval, this));
                                                break;
                                            case Monster.ScalyBeast:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ScalyBeast], 344 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.HornedSorceror:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedSorceror], 536 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.SnowWolfKing:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SnowWolfKing], 456 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.FrozenAxeman:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenAxeman], 528 + (int)Direction * 3, 3, 300, this));
                                                break;
                                            case Monster.FrozenMagician:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenMagician], 840 + (int)Direction * 4, 4, 400, this));
                                                Effect frozenMagicianEffect = new Effect(Libraries.Monsters[(ushort)Monster.FrozenMagician], 872, 6, 600, front, GameFrm.Time + 300);
                                                MapControl.Effects.Add(frozenMagicianEffect);
                                                break;
                                        }
                                        break;
                                    }
                                case 6:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.FrozenMiner:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenMiner], 432 + (int)Direction * 3, 3, 300, this));
                                                Point source = Functions.PointMove(CurrentLocation, Direction, 1);
                                                Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.FrozenMiner], 456, 6, 600, source, GameFrm.Time + 300);
                                                MapControl.Effects.Add(ef);
                                                break;
                                            case Monster.SnowYeti:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SnowYeti], 504 + (int)Direction * 4, 4, 400, this));
                                                break;
                                            case Monster.IceCrystalSoldier:
                                                source = Functions.PointMove(CurrentLocation, Direction, 1);
                                                ef = new Effect(Libraries.Monsters[(ushort)Monster.IceCrystalSoldier], 464, 6, 600, source, GameFrm.Time);
                                                MapControl.Effects.Add(ef);
                                                break;
                                        }
                                        break;
                                    }
                                case 7:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.AxePlant:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AxePlant], 256 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                                break;
                                            case Monster.TreeQueen://Fire Bombardment
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TreeQueen], 35, 13, 1300, this) { Blend = true });
                                                break;
                                            case Monster.HornedCommander:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedCommander], 784 + (int)Direction * 3, 3, 300, this) { Blend = true });
                                                break;
                                        }
                                        break;
                                    }
                                case 8:
                                    {
                                        MapObject ob = MapControl.GetObject(TargetID);

                                        switch (BaseImage)
                                        {
                                            case Monster.FurbolgCommander:
                                                if (ob != null)
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgCommander], 320, 5, 500, ob));
                                                break;
                                        }
                                        break;
                                    }
                                case 9:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.SeedingsGeneral:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SeedingsGeneral], 736 + (int)Direction * 9, 9, 900, this));
                                                break;
                                        }
                                        break;
                                    }
                            }
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.SitDown:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.Attack2:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        MapObject ob = null;

                        Point front = Functions.PointMove(CurrentLocation, Direction, 1);

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.BabySnowMan:
                                                if (FrameIndex == 1)
                                                {
                                                    if (TrackableEffect.GetOwnerEffectID(this.ObjectID, "SnowmanSnow") < 0)
                                                        Effects.Add(new TrackableEffect(new Effect(Libraries.Pets[((ushort)BaseImage) - 10000], 208, 11, 1500, this), "SnowmanSnow"));
                                                }
                                                break;
                                            case Monster.CannibalTentacles:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CannibalTentacles], 400 + (int)Direction * 9, 9, 9 * Frame.Interval, this));
                                                break;
                                            case Monster.DarkOmaKing:
                                                SoundManager.PlaySound(BaseSound + 6, false, 800);
                                                SoundManager.PlaySound(BaseSound + 6, false, 1500);
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkOmaKing], 1600, 30, 30 * Frame.Interval, this));
                                                break;
                                        }
                                    }
                                    break;
                                case 2:
                                    {
                                        switch (BaseImage)
                                        {
                                            // Sanjian
                                            case Monster.FurbolgWarrior:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgWarrior], 360 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                                break;


                                            case Monster.BlackHammerCat:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackHammerCat], 648 + (int)Direction * 11, 11, 11 * Frame.Interval, this));
                                                break;
                                            case Monster.HornedCommander:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedCommander], 1142 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.SnowWolfKing:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SnowWolfKing], 480, 9, 9 * Frame.Interval, this));
                                                break;
                                        }
                                    }
                                    break;
                                case 3:
                                    {
                                        switch (BaseImage)
                                        {
                                            // Sanjian
                                            case Monster.GlacierBeast:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GlacierBeast], 310 + (int)Direction * 4, 4, 4 * Frame.Interval, this));
                                                break;



                                            case Monster.KingGuard:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingGuard], 773, 10, 1000, this) { Blend = true });
                                                break;
                                            case Monster.Behemoth:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Behemoth], 768, 10, Frame.Count * Frame.Interval, this));
                                                break;
                                            case Monster.FlameQueen:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlameQueen], 720, 9, Frame.Count * Frame.Interval, this));
                                                break;
                                            case Monster.DemonGuard:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DemonGuard], 288 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.TucsonMage:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonMage], 296 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                                break;
                                            case Monster.Armadillo:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Armadillo], 496 + (int)Direction * 12, 12, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.CatWidow:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CatWidow], 256 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.SnowWolf:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SnowWolf], 328, 9, Frame.Count * Frame.Interval, this));
                                                break;
                                            case Monster.BlackTortoise:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackTortoise], 366 + (int)Direction * 4, 4, 4 * Frame.Interval, this));
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackTortoise], 398, 7, 7 * Frame.Interval, front, GameFrm.Time));
                                                break;
                                            case Monster.TreeQueen:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TreeQueen], 66, 16, 16 * Frame.Interval, this));
                                                break;
                                            case Monster.RhinoWarrior:
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RhinoWarrior], 376, 8, 800, front, GameFrm.Time));
                                                break;
                                            case Monster.FrozenFighter:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenFighter], 384 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                                break;
                                        }
                                    }
                                    break;
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {
                                            // Sanjian
                                            case Monster.GlacierSnail:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GlacierSnail], 344 + (int)Direction * 5, 5, 5 * Frame.Interval, this));
                                                break;





                                            case Monster.DemonWolf:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DemonWolf], 312 + (int)Direction * 3, 3, 300, this));
                                                break;
                                            case Monster.TucsonWarrior:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonWarrior], 344 + (int)Direction * 9, 9, 9 * Frame.Interval, this));
                                                break;
                                            case Monster.PeacockSpider:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PeacockSpider], 592 + (int)Direction * 9, 9, 9 * Frame.Interval, this));
                                                break;
                                            case Monster.SackWarrior:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SackWarrior], 368 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.ManTree:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManTree], 488 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                            case Monster.DarkWraith:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkWraith], 750 + (int)Direction * 5, 5, 500, this));
                                                Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.DarkWraith], 790, 6, 600, front, GameFrm.Time);
                                                MapControl.Effects.Add(ef);
                                                break;
                                            case Monster.HornedWarrior:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedWarrior], 832 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                                break;
                                            case Monster.Turtlegrass:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Turtlegrass], 360 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.AntCommander:
                                                Effect ef1 = new Effect(Libraries.Monsters[(ushort)Monster.AntCommander], 484, 6, 600, front, GameFrm.Time);
                                                MapControl.Effects.Add(ef1);
                                                break;
                                        }
                                        break;
                                    }
                                    break;
                                case 5:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.GeneralMeowMeow:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GeneralMeowMeow], 456 + (int)Direction * 7, 7, 7 * Frame.Interval, this) { Blend = true });
                                                break;
                                            case Monster.TucsonGeneral:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonGeneral], 544, 8, 8 * Frame.Interval, this));
                                                break;
                                            case Monster.FrozenMiner:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenMiner], 462 + (int)Direction * 5, 5, 500, this));
                                                break;
                                            case Monster.IceCrystalSoldier:
                                                Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.IceCrystalSoldier], 470, 6, 600, front, GameFrm.Time);
                                                MapControl.Effects.Add(ef);
                                                break;
                                            case Monster.Bear:
                                                Effect bleedEffect = new Effect(Libraries.Monsters[(ushort)Monster.Bear], 312, 9, 900, front, GameFrm.Time);
                                                MapControl.Effects.Add(bleedEffect);
                                                break;
                                            case Monster.FlyingStatue:
                                                Effect flyingStatueEffect1 = new Effect(Libraries.Monsters[(ushort)Monster.FlyingStatue], 362, 8, 800, front, GameFrm.Time);
                                                MapControl.Effects.Add(flyingStatueEffect1);
                                                break;
                                            case Monster.HornedCommander:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedCommander], 784 + (int)Direction * 3, 3, 300, this) { Blend = true });
                                                break;
                                        }
                                        break;

                                    }
                                    break;
                                case 6:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.SeedingsGeneral:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SeedingsGeneral], 1136 + (int)Direction * 6, 6, 600, this));
                                                break;
                                            case Monster.OmaBlest:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaBlest], 392 + (int)Direction * 5, 5, 500, this));
                                                break;
                                            case Monster.WereTiger:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WereTiger], 344 + (int)Direction * 6, 6, 600, this));
                                                break;
                                            case Monster.Kirin:
                                                Point source2 = Functions.PointMove(CurrentLocation, Direction, 2);
                                                Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.Kirin], 816, 8, 800, source2, GameFrm.Time);
                                                MapControl.Effects.Add(ef);
                                                break;
                                            case Monster.FrozenAxeman:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenAxeman], 558 + (int)Direction * 3, 3, 300, this));
                                                break;
                                            case Monster.SnowYeti:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SnowYeti], 536 + (int)Direction * 3, 3, 300, this));
                                                break;
                                        }
                                    }
                                    break;
                                case 7:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.ElephantMan:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ElephantMan], 368, 9, 900, this) { Blend = true });
                                                break;
                                            case Monster.ScalyBeast:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ScalyBeast], 368 + (int)Direction * 3, 3, 3 * Frame.Interval, this) { Blend = false });
                                                break;
                                        }
                                    }
                                    break;
                                case 8:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.StrayCat:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StrayCat], 584 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                        }
                                    }
                                    break;
                                case 9:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.ScalyBeast:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ScalyBeast], 392, 3, 300, this) { Blend = true });
                                                break;
                                        }
                                    }
                                    break;
                                case 10:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.StoningStatue:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StoningStatue], 642, 15, 15 * 100, this));
                                                SoundManager.PlaySound(BaseSound + 7);
                                                break;
                                        }
                                    }
                                    break;
                                case 11:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.BlackHammerCat:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlackHammerCat], 736 + (int)Direction * 8, 8, 800, this));
                                                break;
                                        }
                                    }
                                    break;
                                case 19:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.StoningStatue:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StoningStatue], 624, 8, 8 * 100, this));
                                                break;
                                        }
                                    }
                                    break;
                            }
                            if (FrameIndex == 3) PlaySwingSound();
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.Attack3:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    switch (BaseImage)
                                    {
                                        case Monster.OlympicFlame:
                                            if (TrackableEffect.GetOwnerEffectID(this.ObjectID, "CreatureFlame") < 0)
                                                Effects.Add(new TrackableEffect(new Effect(Libraries.Pets[((ushort)BaseImage) - 10000], 280, 4, 800, this), "CreatureFlame"));
                                            break;
                                        case Monster.GasToad:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GasToad], 440, 9, 9 * Frame.Interval, this));
                                            break;
                                        case Monster.HornedCommander:
                                            {
                                                //Spin
                                                int loops = CurrentActionLevel;
                                                int duration = 7 * FrameInterval;
                                                int totalDuration = loops * duration;

                                                if (FrameLoop == null)
                                                {
                                                    for (int i = 0; i < loops; i++)
                                                    {
                                                        SoundManager.PlaySound(8451, false, 0 + (i * duration));
                                                    }

                                                    Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedCommander], 808 + (int)Direction * 7, 7, duration, this) { Repeat = true, RepeatUntil = GameFrm.Time + totalDuration });
                                                }

                                                LoopFrame(FrameIndex, 3, FrameInterval, totalDuration);

                                            }
                                            break;
                                        case Monster.SnowWolfKing:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SnowWolfKing], 489 + (int)Direction * 9, 9, 9 * Frame.Interval, this));
                                            break;
                                    }
                                    break;
                                case 2:
                                    break;
                                case 3:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.WingedTigerLord:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WingedTigerLord], 632, 8, 600, this, 0, true));
                                                break;
                                            case Monster.DarkWraith:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkWraith], 796 + (int)Direction * 5, 5, 500, this));
                                                break;
                                            case Monster.HornedSorceror:
                                                {
                                                    int loops = CurrentActionLevel;
                                                    int duration = 5 * FrameInterval;
                                                    int totalDuration = loops * duration;

                                                    if (FrameLoop == null)
                                                    {
                                                        for (int i = 0; i < loops; i++)
                                                        {
                                                            SoundManager.PlaySound(BaseSound + 7, false, 0 + (i * duration));
                                                        }
                                                        Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedSorceror], 971 + (int)Direction * 5, 5, duration, this) { Repeat = true, RepeatUntil = GameFrm.Time + totalDuration });
                                                    }

                                                    LoopFrame(FrameIndex, 1, FrameInterval, totalDuration);
                                                }
                                                break;
                                        }
                                    }
                                    break;
                                case 4:
                                    switch (BaseImage)
                                    {
                                        case Monster.OlympicFlame:
                                            if (TrackableEffect.GetOwnerEffectID(this.ObjectID, "CreatureSmoke") < 0)
                                                Effects.Add(new TrackableEffect(new Effect(Libraries.Pets[((ushort)BaseImage) - 10000], 256, 3, 1000, this), "CreatureSmoke"));
                                            break;
                                        case Monster.Kirin:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Kirin], 824 + (int)Direction * 7, 7, 700, this));
                                            break;
                                        case Monster.FrozenAxeman:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenAxeman], 588 + (int)Direction * 3, 3, 300, this));
                                            break;
                                        case Monster.ManTree:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManTree], 504 + (int)Direction * 2, 2, 200, this));
                                            break;
                                        case Monster.DragonWarrior:
                                            Point source = Functions.PointMove(CurrentLocation, Direction, 1);
                                            Effect effect = new Effect(Libraries.Monsters[(ushort)Monster.DragonWarrior], 664, 6, 600, source, GameFrm.Time + 300);
                                            MapControl.Effects.Add(effect);
                                            break;
                                    }
                                    break;
                                case 5:
                                    switch (BaseImage)
                                    {
                                        case Monster.WhiteMammoth:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WhiteMammoth], 376, 5, Frame.Count * Frame.Interval, this));
                                            break;
                                        case Monster.ManTree:
                                            Point source = Functions.PointMove(CurrentLocation, Direction, 1);
                                            Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.ManTree], 520, 8, 800, source, GameFrm.Time, drawBehind: true);
                                            MapControl.Effects.Add(ef);
                                            break;
                                        case Monster.HornedSorceror:
                                            SoundManager.PlaySound(BaseSound + 5);
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedSorceror], 624, 10, 10 * Frame.Interval, this));
                                            break;
                                        case Monster.HornedCommander:
                                            SoundManager.PlaySound(8452, false);
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedCommander], 864, 8, 8 * Frame.Interval, this));
                                            break;
                                    }
                                    break;
                                case 6:
                                    switch (BaseImage)
                                    {
                                        case Monster.RestlessJar:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 512, 7, 700, this));
                                            break;
                                    }
                                    break;
                                case 10:
                                    switch (BaseImage)
                                    {
                                        case Monster.StrayCat:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.StrayCat], 728 + (int)Direction * 10, 10, 1000, this));
                                            break;
                                    }
                                    break;
                            }

                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.Attack4:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {

                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    switch (BaseImage)
                                    {
                                        case Monster.HornedCommander:
                                            {
                                                //Rockfall
                                                int loops = CurrentActionLevel;
                                                int duration = 5 * FrameInterval;
                                                int totalDuration = loops * duration;

                                                if (FrameLoop == null)
                                                {
                                                    for (int i = 0; i < loops; i++)
                                                    {
                                                        SoundManager.PlaySound(8453, false, 0 + (i * duration));
                                                    }

                                                    Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedCommander], 1026 + (int)Direction * 5, 5, duration, this) { Repeat = true, RepeatUntil = GameFrm.Time + totalDuration });
                                                }

                                                LoopFrame(FrameIndex, 3, FrameInterval, totalDuration);
                                            }
                                            break;
                                    }
                                    break;
                                case 3:
                                    switch (BaseImage)
                                    {
                                        case Monster.SnowWolfKing:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SnowWolfKing], 581 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                            break;
                                    }
                                    break;
                                case 5:
                                    switch (BaseImage)
                                    {
                                        case Monster.HornedCommander:
                                            SoundManager.PlaySound(8454);
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedCommander], 1078 + (int)Direction * 8, 8, 8 * Frame.Interval, this));
                                            break;
                                    }
                                    break;
                            }

                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.Attack5:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.HornedCommander:
                                                LoopFrame(FrameIndex, 2, FrameInterval, CurrentActionLevel * 1000);
                                                break;
                                        }
                                        break;
                                    }
                                    break;
                            }

                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.AttackRange1:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            switch (BaseImage)
                            {
                                case Monster.DragonStatue:
                                    MapObject ob = MapControl.GetObject(TargetID);
                                    if (ob != null)
                                    {
                                        ob.Effects.Add(new Effect(Libraries.Dragon, 350, 35, 1200, ob));
                                        SoundManager.PlaySound(BaseSound + 6);
                                    }
                                    break;



                            }
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            if (FrameIndex == 2) PlaySwingSound();
                            MapObject ob = null;
                            switch (FrameIndex)
                            {
                                case 1:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.GuardianRock:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic2, 1410, 10, 400, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.OmaWitchDoctor:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaWitchDoctor], 792 + (int)Direction * 7, 7, 7 * Frame.Interval, this));
                                                break;
                                            case Monster.FloatingRock:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FloatingRock], 159 + (int)Direction * 7, 7, 7 * Frame.Interval, this));
                                                break;
                                            case Monster.KingHydrax:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingHydrax], 368 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                            case Monster.BlueSoul:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BlueSoul], 240, 10, 700, ob));
                                                    SoundManager.PlaySound(BaseSound + 7);
                                                }
                                                break;
                                            case Monster.HornedCommander:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedCommander], 872 + (int)Direction * 7, 7, 7 * Frame.Interval, this));
                                                break;
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.LeftGuard:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.LeftGuard], 336 + (int)Direction * 3, 3, 3 * Frame.Interval, this));
                                                break;
                                            case Monster.DreamDevourer:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DreamDevourer], 264, 7, 7 * Frame.Interval, ob));
                                                }
                                                break;
                                            case Monster.DarkDevourer:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkDevourer], 480, 7, 7 * Frame.Interval, ob));
                                                }
                                                break;
                                            case Monster.ManectricClaw:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManectricClaw], 304 + (int)Direction * 10, 10, 10 * Frame.Interval, this));
                                                break;
                                            case Monster.FlameSpear:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlameSpear], 544 + (int)Direction * 10, 10, 10 * 100, this));
                                                break;
                                            case Monster.FrozenMagician:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenMagician], 512 + (int)Direction * 6, 6, 6 * Frame.Interval, this));
                                                break;
                                        }
                                        break;
                                    }
                                case 3:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.HardenRhino:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HardenRhino], 392, 5, 5 * Frame.Interval, this));
                                                break;
                                            case Monster.HornedArcher:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedArcher], 336 + (int)Direction * 3, 3, 3 * Frame.Interval, this, drawBehind: true));
                                                break;
                                            case Monster.ColdArcher:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ColdArcher], 336 + (int)Direction * 4, 4, 4 * Frame.Interval, this, drawBehind: true));
                                                break;
                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {
                                            // Sanjian
                                            case Monster.FurbolgArcher:
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    CreateProjectile(344, Libraries.Monsters[(ushort)Monster.FurbolgArcher], false, 5, 30, 0);
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.AxeSkeleton:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(224, Libraries.Monsters[(ushort)Monster.AxeSkeleton], false, 3, 30, 0);
                                                break;
                                            case Monster.Dark:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(224, Libraries.Monsters[(ushort)Monster.Dark], false, 3, 30, 0);
                                                break;
                                            case Monster.ZumaArcher:
                                            case Monster.BoneArcher:
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    CreateProjectile(224, Libraries.Monsters[(ushort)Monster.ZumaArcher], false, 1, 30, 0);
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.RedThunderZuma:
                                            case Monster.FrozenRedZuma:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Dragon, 400 + GameFrm.Random.Next(3) * 10, 5, 300, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.BoneLord:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(784, Libraries.Monsters[(ushort)Monster.BoneLord], true, 6, 30, 0, direction16: false);
                                                break;
                                            case Monster.RightGuard:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic2, 10, 5, 300, ob));
                                                }
                                                break;
                                            case Monster.LeftGuard:
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    CreateProjectile(10, Libraries.Magic, true, 6, 30, 4);
                                                }
                                                break;
                                            case Monster.MinotaurKing:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MinotaurKing], 320, 20, 1000, ob));
                                                }
                                                break;
                                            case Monster.FrostTiger:
                                                if (MapControl.GetObject(TargetID) != null)
                                                {
                                                    CreateProjectile(410, Libraries.Magic2, true, 4, 30, 6);
                                                }
                                                break;
                                            case Monster.Yimoogi:
                                            case Monster.RedYimoogi:
                                            case Monster.Snake10:
                                            case Monster.Snake11:
                                            case Monster.Snake12:
                                            case Monster.Snake13:
                                            case Monster.Snake14:
                                            case Monster.Snake15:
                                            case Monster.Snake16:
                                            case Monster.Snake17:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic2, 1250, 15, 1000, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.HolyDeva:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Magic2, 10, 5, 300, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.CrossbowOma:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.CrossbowOma], false, 1, 30, 6);
                                                break;
                                            case Monster.DarkCrossbowOma:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.DarkCrossbowOma], false, 1, 30, 6);
                                                break;
                                            case Monster.FlamingMutant:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlamingMutant], 320, 10, 1000, ob.CurrentLocation, GameFrm.Time) { Blend = true });
                                                    //SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.RedFoxman:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RedFoxman], 224, 9, 300, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.TrapRock:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TrapRock], 26, 10, 600, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.HedgeKekTal:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.HedgeKekTal], false, 4, 30, 6);
                                                break;
                                            case Monster.BigHedgeKekTal:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.BigHedgeKekTal], false, 4, 30, 6);
                                                break;
                                            case Monster.ArcherGuard:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.ArcherGuard], false, 3, 30, 6);
                                                break;
                                            case Monster.SpittingToad:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(280, Libraries.Monsters[(ushort)Monster.SpittingToad], true, 6, 30, 0);
                                                break;
                                            case Monster.ArcherGuard2:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(38, Libraries.Monsters[(ushort)Monster.ArcherGuard], false, 3, 30, 6);
                                                break;
                                            case Monster.ArcherGuard3:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(104, Libraries.Monsters[(ushort)Monster.ArcherGuard3], false, 3, 30, 0);
                                                break;
                                            case Monster.HellBolt:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HellBolt], 315, 10, 600, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.WingedTigerLord:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WingedTigerLord], 640, 10, 800, ob, GameFrm.Time + 400, false));
                                                }
                                                break;
                                            case Monster.FlameScythe:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlameScythe], 586, 9, 900, ob));
                                                }
                                                break;
                                            case Monster.FlameQueen:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FlameQueen], 729, 10, Frame.Count * Frame.Interval, this));
                                                break;
                                            case Monster.IceGuard:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IceGuard], 262, 6, 600, ob) { Blend = true });
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.ElementGuard:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ElementGuard], 360 + (int)ob.Direction * 7, 7, 7 * Frame.Interval, ob) { Blend = true });
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.KingGuard:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingGuard], 746, 7, 700, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.BurningZombie:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BurningZombie], 361, 12, 1000, ob));
                                                }
                                                break;
                                            case Monster.FrozenZombie:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenZombie], 352, 8, 1000, ob));
                                                }
                                                break;
                                            case Monster.CatShaman:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CatShaman], 720, 12, 1500, ob) { Blend = false });
                                                }
                                                break;
                                            case Monster.GeneralMeowMeow:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GeneralMeowMeow], 512, 10, 1000, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.CannibalTentacles:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(472, Libraries.Monsters[(ushort)Monster.CannibalTentacles], true, 8, 100, 0, direction16: false);
                                                break;
                                            case Monster.SwampWarrior:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SwampWarrior], 392, 8, 800, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.RhinoPriest:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RhinoPriest], 376, 9, 900, ob));
                                                }
                                                break;
                                            case Monster.TreeGuardian:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TreeGuardian], 544, 8, 800, ob));
                                                }
                                                break;
                                            case Monster.CreeperPlant:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CreeperPlant], 250, 6, 600, ob.CurrentLocation, GameFrm.Time) { Blend = true });
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CreeperPlant], 256, 10, 1000, ob.CurrentLocation, GameFrm.Time) { Blend = false });
                                                }
                                                break;
                                            case Monster.LightningBead:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.LightningBead], 65, 12, 1200, ob));
                                                }
                                                break;
                                            case Monster.HealingBead:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HealingBead], 61, 11, 1100, ob));
                                                }
                                                break;
                                            case Monster.PowerUpBead:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PowerUpBead], 64, 6, 600, ob));
                                                }
                                                break;
                                            case Monster.DarkOmaKing:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.DarkOmaKing], 1715, 13, 1300, ob.CurrentLocation));
                                                }
                                                break;
                                            case Monster.ManTree:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ManTree], 520, 8, 1000, ob));
                                                }
                                                break;
                                            case Monster.ClawBeast:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ClawBeast], 568, 7, 700, ob));
                                                }
                                                break;
                                            case Monster.DragonArcher:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(416, Libraries.Monsters[(ushort)Monster.DragonArcher], true, 5, 50, 0);
                                                break;
                                            case Monster.HornedMage:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedMage], 768, 9, 800, ob) { Blend = false });
                                                }
                                                break;
                                            case Monster.KingHydrax:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingHydrax], 416, 9, 900, ob) { DrawBehind = true });
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.AssassinScroll:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AssassinScroll], 299, 8, 800, ob));
                                                }
                                                break;
                                            case Monster.TaoistScroll:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TaoistScroll], 272, 10, 1000, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.WarriorScroll:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WarriorScroll], 304, 11, 1100, ob) { Blend = true });
                                                }
                                                break;
                                        }
                                        break;
                                    }//end of case 4
                                case 5:
                                    switch (BaseImage)
                                    {
                                        case Monster.OmaWitchDoctor:
                                            ob = MapControl.GetObject(TargetID);
                                            if (ob != null)
                                            {
                                                SoundManager.PlaySound(BaseSound + 7);
                                                ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaWitchDoctor], 848, 11, 1100, ob) { Blend = true });
                                            }
                                            break;
                                        case Monster.MudZombie:
                                            ob = MapControl.GetObject(TargetID);
                                            if (ob != null)
                                            {
                                                ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MudZombie], 304, 7, 700, ob) { Blend = false });
                                            }
                                            break;
                                    }

                                    break;
                                case 6:
                                    {
                                        switch (BaseImage)
                                        {
                                            // Sanjian
                                            case Monster.FurbolgCommander:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgCommander], 357, 6, 600, ob) { DrawBehind = true });
                                                }
                                                break;


                                            case Monster.HornedMage:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedMage], 777, 6, 800, ob) /*{ Blend = false }*/);
                                                }
                                                break;
                                            case Monster.FloatingRock:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FloatingRock], 226, 10, 1000, ob) /*{ Blend = false }*/);
                                                }
                                                break;
                                            case Monster.FrozenArcher:
                                                if (MapControl.GetObject(TargetID) != null)
                                                    CreateProjectile(264, Libraries.Monsters[(ushort)Monster.FrozenArcher], true, 5, 80, 0);
                                                break;
                                            case Monster.IceCrystalSoldier:
                                                Point source = Functions.PointMove(CurrentLocation, Direction, 1);
                                                Effect ef = new Effect(Libraries.Monsters[(ushort)Monster.IceCrystalSoldier], 476, 8, 800, source, GameFrm.Time);
                                                MapControl.Effects.Add(ef);
                                                break;
                                            case Monster.ColdArcher:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ColdArcher], 368 + (int)Direction * 2, 2, 2 * FrameInterval, this));
                                                break;
                                            case Monster.HoodedSummoner:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HoodedSummoner], 374, 13, 1300, ob) { Blend = true });
                                                }
                                                break;
                                        }
                                        break;
                                    }
                                case 7:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.FrozenKnight:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenKnight], 456, 10, 1000, ob));
                                                }
                                                break;
                                            case Monster.IcePhantom:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 682, 10, 1000, ob) { Blend = true });
                                                    SoundManager.PlaySound(BaseSound + 6); //is this the correct sound for this mob attack?
                                                }
                                                break;
                                            case Monster.FloatingRock:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FloatingRock], 226, 10, 1000, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                        }
                                        break;
                                    }
                                case 8:
                                    {
                                        switch (BaseImage)
                                        {
                                        }
                                        break;
                                    }
                                case 9:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.ColdArcher:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.ColdArcher], 384, 10, 1000, ob));
                                                }
                                                break;

                                        }
                                        break;
                                    }
                            }
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.AttackRange2:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            MapObject ob = null;
                            switch (FrameIndex)
                            {
                                case 1:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.RestlessJar:
                                                var point = Functions.PointMove(CurrentLocation, Direction, 2);
                                                MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RestlessJar], 391 + (int)Direction * 10, 10, 10 * Frame.Interval, point));
                                                break;
                                            case Monster.KingHydrax:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingHydrax], 425 + (int)Direction * 6, 6, 600, this));
                                                break;
                                        }
                                        break;
                                    }
                                case 2:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.FrozenMagician:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FrozenMagician], 662 + (int)Direction * 9, 9, 9 * Frame.Interval, this));
                                                break;

                                        }
                                        break;
                                    }
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {

                                            case Monster.RedFoxman:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RedFoxman], 233, 10, 400, ob));
                                                    SoundManager.PlaySound(BaseSound + 7);
                                                }
                                                break;
                                            case Monster.AncientBringer:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.AncientBringer], 740, 14, 2000, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.IceGuard:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IceGuard], 268, 5, 500, ob) { Blend = true });
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.CatShaman:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CatShaman], 732, 6, 500, ob));
                                                    SoundManager.PlaySound(BaseSound + 6);
                                                }
                                                break;
                                            case Monster.RhinoPriest:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.RhinoPriest], 448, 7, 700, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.TreeGuardian:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TreeGuardian], 648 + ((int)Direction * 10), 10, 1000, ob.CurrentLocation));
                                                }
                                                break;
                                            case Monster.OmaWitchDoctor:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.OmaWitchDoctor], 859, 10, 1000, ob) { Blend = true });
                                                }
                                                break;
                                            case Monster.IcePhantom:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 772, 7, 700, ob) { Blend = true });
                                                    SoundManager.PlaySound(BaseSound + 6); //is this the correct sound for this mob attack?
                                                }
                                                break;
                                            case Monster.HornedCommander:
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedCommander], 938 + (int)Direction * 9, 9, 9 * Frame.Interval, this));
                                                Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.HornedCommander], 1010 + (int)Direction * 2, 2, 2 * Frame.Interval, this));
                                                break;
                                        }
                                        break;
                                    }
                            }
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.AttackRange3:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            MapObject ob = null;
                            switch (FrameIndex)
                            {
                                case 4:
                                    {
                                        switch (BaseImage)
                                        {
                                            case Monster.TucsonGeneral:
                                                ob = MapControl.GetObject(TargetID);
                                                if (ob != null)
                                                {
                                                    ob.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.TucsonGeneral], 745, 17, 1000, ob) { Blend = true });
                                                }
                                                break;
                                        }
                                    }
                                    break;
                            }
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.Struck:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            SetAction();
                        }
                        else
                        {
                            NextMotion += FrameInterval;
                        }
                    }
                    break;

                case MirAction.Die:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            ActionFeed.Clear();
                            ActionFeed.Add(new QueuedAction { Action = MirAction.Dead, Direction = Direction, Location = CurrentLocation });
                            SetAction();
                        }
                        else
                        {
                            switch (FrameIndex)
                            {
                                case 1:
                                    switch (BaseImage)
                                    {

                                        // Sanjian
                                        case Monster.FurbolgCommander:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.FurbolgCommander], 320, 5, 5 * Frame.Interval, this));
                                            break;
                                        case Monster.Furball:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Furball], 288, 8, Frame.Count * Frame.Interval, this));
                                            break;
                                        case Monster.GlacierBeast:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.GlacierBeast], 342, 12, 1200, this) { Blend = true });
                                            break;


                                        case Monster.PoisonHugger:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.PoisonHugger], 224, 5, Frame.Count * FrameInterval, this));
                                            break;
                                        case Monster.Hugger:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Hugger], 256, 8, Frame.Count * FrameInterval, this));
                                            break;
                                        case Monster.MutatedHugger:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.MutatedHugger], 128, 7, Frame.Count * FrameInterval, this));
                                            break;
                                        case Monster.CyanoGhast:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.CyanoGhast], 681, 7, Frame.Count * FrameInterval, this));
                                            break;
                                        case Monster.Hydrax:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Hydrax], 240, 5, 5 * Frame.Interval, this));
                                            break;
                                    }
                                    break;
                                case 3:
                                    PlayDeadSound();
                                    switch (BaseImage)
                                    {
                                        case Monster.BoneSpearman:
                                        case Monster.BoneBlademan:
                                        case Monster.BoneArcher:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BoneSpearman], 224, 8, Frame.Count * FrameInterval, this));
                                            break;
                                        case Monster.WoodBox:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.WoodBox], 104, 6, 6 * Frame.Interval, this) { Blend = true });
                                            break;
                                        case Monster.BoulderSpirit:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.BoulderSpirit], 64, 8, 8 * Frame.Interval, this) { Blend = true });
                                            break;
                                    }
                                    break;
                                // Sanjian
                                case 4:
                                    PlayDeadSound();
                                    switch (BaseImage)
                                    {
                                    }
                                    break;
                                case 5:
                                    switch (BaseImage)
                                    {
                                        case Monster.KingHydrax:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.KingHydrax], 543 + (int)Direction * 7, 7, 700, this));
                                            break;
                                        case Monster.Bear:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.Bear], 353, 9, Frame.Count * Frame.Interval, this));
                                            break;
                                        case Monster.SnowWolfKing:
                                            Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.SnowWolfKing], 605, 10, 10 * Frame.Interval, this) { DrawBehind = true });
                                            break;
                                    }
                                    break;
                                case 9:
                                    switch (BaseImage)
                                    {
                                        case Monster.IcePhantom:
                                            MapControl.Effects.Add(new Effect(Libraries.Monsters[(ushort)Monster.IcePhantom], 692 + (int)Direction * 10, 10, 10 * Frame.Interval, CurrentLocation));
                                            break;
                                    }
                                    break;
                            }

                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.Revive:
                    if (GameFrm.Time >= NextMotion)
                    {
                        GameScene.Scene.MapControl.TextureValid = false;

                        if (SkipFrames) UpdateFrame();

                        if (UpdateFrame() >= Frame.Count)
                        {
                            FrameIndex = Frame.Count - 1;
                            ActionFeed.Clear();
                            ActionFeed.Add(new QueuedAction { Action = MirAction.Standing, Direction = Direction, Location = CurrentLocation });
                            SetAction();
                        }
                        else
                        {
                            if (FrameIndex == 3)
                                PlayReviveSound();
                            NextMotion += FrameInterval;
                        }
                    }
                    break;
                case MirAction.Dead:
                    break;

            }

            if ((CurrentAction == MirAction.Standing || CurrentAction == MirAction.SitDown) && NextAction != null)
                SetAction();
            else if (CurrentAction == MirAction.Dead && NextAction != null && (NextAction.Action == MirAction.Skeleton || NextAction.Action == MirAction.Revive))
                SetAction();
        }

        public int UpdateFrame()
        {
            if (Frame == null) return 0;

            if (FrameLoop != null)
            {
                if (FrameLoop.CurrentCount > FrameLoop.Loops)
                {
                    FrameLoop = null;
                }
                else if (FrameIndex >= FrameLoop.End)
                {
                    FrameIndex = FrameLoop.Start - 1;
                    FrameLoop.CurrentCount++;
                }
            }

            if (Frame.Reverse) return Math.Abs(--FrameIndex);

            return ++FrameIndex;
        }

        public override object CreateProjectile(int baseIndex, MLibrary library, bool blend, int count, int interval, int skip, int lightDistance = 6, bool direction16 = true, Color? lightColour = null, uint targetID = 0)
        {
            if (targetID == 0)
            {
                targetID = TargetID;
            }

            MapObject ob = MapControl.GetObject(targetID);

            //var targetPoint = TargetPoint;

            //if (ob != null) targetPoint = ob.CurrentLocation;

            //int duration = Functions.MaxDistance(CurrentLocation, targetPoint) * 50;

            //Missile missile = new Missile(library, baseIndex, duration / interval, duration, this, targetPoint, direction16)
            //{
            //    Target = ob,
            //    Interval = interval,
            //    FrameCount = count,
            //    Blend = blend,
            //    Skip = skip,
            //    Light = lightDistance,
            //    LightColour = lightColour == null ? Color4.White : (Color)lightColour
            //};

            //Effects.Add(missile);

            return null;
        }

        private void PlaySummonSound()
        {
            switch (BaseImage)
            {
                case Monster.HellKnight1:
                case Monster.HellKnight2:
                case Monster.HellKnight3:
                case Monster.HellKnight4:
                case Monster.LightningBead:
                case Monster.HealingBead:
                case Monster.PowerUpBead:
                    SoundManager.PlaySound(BaseSound + 0);
                    return;
                case Monster.BoneFamiliar:
                case Monster.Shinsu:
                case Monster.HolyDeva:
                    SoundManager.PlaySound(BaseSound + 5);
                    return;
            }
        }
        private void PlayWalkSound(bool left = true)
        {
            if (left)
            {
                switch (BaseImage)
                {
                    case Monster.WingedTigerLord:
                    case Monster.PoisonHugger:
                    case Monster.SnowWolfKing:
                    case Monster.Catapult:
                    case Monster.ChariotBallista:
                        SoundManager.PlaySound(BaseSound + 8);
                        return;
                }
            }
            else
            {
                switch (BaseImage)
                {
                    case Monster.WingedTigerLord:
                    case Monster.AvengerPlant:
                        SoundManager.PlaySound(BaseSound + 8);
                        return;
                    case Monster.PoisonHugger:
                    case Monster.SnowWolfKing:
                        SoundManager.PlaySound(BaseSound + 9);
                        return;
                }
            }
        }
        public void PlayAppearSound()
        {
            switch (BaseImage)
            {
                case Monster.CannibalPlant:
                case Monster.WaterDragon:
                case Monster.EvilCentipede:
                case Monster.CreeperPlant:
                    return;
                case Monster.ZumaArcher:
                case Monster.ZumaStatue:
                case Monster.ZumaGuardian:
                case Monster.RedThunderZuma:
                case Monster.FrozenRedZuma:
                case Monster.FrozenZumaStatue:
                case Monster.FrozenZumaGuardian:
                case Monster.ZumaTaurus:
                case Monster.DemonGuard:
                case Monster.Turtlegrass:
                case Monster.ManTree:
                case Monster.EarthGolem:
                case Monster.AssassinScroll:
                case Monster.WarriorScroll:
                case Monster.TaoistScroll:
                case Monster.WizardScroll:
                case Monster.PurpleFaeFlower:
                    if (Stoned) return;
                    break;
            }

            SoundManager.PlaySound(BaseSound);
        }
        public void PlayPopupSound()
        {
            switch (BaseImage)
            {
                case Monster.ZumaTaurus:
                case Monster.DigOutZombie:
                case Monster.Armadillo:
                case Monster.ArmadilloElder:
                    SoundManager.PlaySound(BaseSound + 5);
                    return;
                case Monster.Shinsu:
                    SoundManager.PlaySound(BaseSound + 6);
                    return;
            }
            SoundManager.PlaySound(BaseSound);
        }

        public void PlayRunSound()
        {
            switch (BaseImage)
            {
                case Monster.HardenRhino:
                    SoundManager.PlaySound(BaseSound + 8);
                    break;
            }
        }

        public void PlayJumpSound()
        {
            switch (BaseImage)
            {
                case Monster.Armadillo:
                case Monster.ArmadilloElder:
                case Monster.ChieftainArcher:
                    SoundManager.PlaySound(BaseSound + 8);
                    break;
            }
        }

        public void PlayDashSound()
        {
            switch (BaseImage)
            {
                case Monster.HornedSorceror:
                    SoundManager.PlaySound(BaseSound + 9);
                    break;
            }
        }

        public void PlayFlinchSound()
        {
            switch (BaseImage)
            {
                default:
                    SoundManager.PlaySound(BaseSound + 2);
                    break;
            }
        }
        public void PlayStruckSound()
        {
            switch (StruckWeapon)
            {
                case 0:
                case 23:
                case 28:
                case 40:
                    SoundManager.PlaySound(SoundList.StruckWooden);
                    break;
                case 1:
                case 12:
                    SoundManager.PlaySound(SoundList.StruckShort);
                    break;
                case 2:
                case 8:
                case 11:
                case 15:
                case 18:
                case 20:
                case 25:
                case 31:
                case 33:
                case 34:
                case 37:
                case 41:
                    SoundManager.PlaySound(SoundList.StruckSword);
                    break;
                case 3:
                case 5:
                case 7:
                case 9:
                case 13:
                case 19:
                case 24:
                case 26:
                case 29:
                case 32:
                case 35:
                    SoundManager.PlaySound(SoundList.StruckSword2);
                    break;
                case 4:
                case 14:
                case 16:
                case 38:
                    SoundManager.PlaySound(SoundList.StruckAxe);
                    break;
                case 6:
                case 10:
                case 17:
                case 22:
                case 27:
                case 30:
                case 36:
                case 39:
                    SoundManager.PlaySound(SoundList.StruckShort);
                    break;
                case 21:
                    SoundManager.PlaySound(SoundList.StruckClub);
                    break;
            }
        }
        public void PlayAttackSound()
        {
            switch (BaseImage)
            {
                default:
                    SoundManager.PlaySound(BaseSound + 1);
                    break;
            }
        }

        public void PlaySecondAttackSound()
        {
            switch (BaseImage)
            {
                default:
                    SoundManager.PlaySound(BaseSound + 6);
                    break;

            }
        }

        public void PlayThirdAttackSound()
        {
            switch (BaseImage)
            {
                case Monster.DarkCaptain:
                case Monster.HornedSorceror:
                case Monster.HornedCommander:
                    return;
                default:
                    SoundManager.PlaySound(BaseSound + 7);
                    return;
            }
        }

        public void PlayFourthAttackSound()
        {
            switch (BaseImage)
            {
                case Monster.HornedCommander:
                    return;
                case Monster.SnowWolfKing:
                    SoundManager.PlaySound(BaseSound + 5);
                    return;
                default:
                    SoundManager.PlaySound(BaseSound + 8);
                    return;
            }
        }

        public void PlayFithAttackSound()
        {
            SoundManager.PlaySound(BaseSound + 9);
        }

        public void PlaySwingSound()
        {
            switch (BaseImage)
            {
                case Monster.DarkCaptain:
                    return;
                default:
                    SoundManager.PlaySound(BaseSound + 4);
                    return;
            }
        }
        public void PlayDieSound()
        {
            switch (BaseImage)
            {
                default:
                    SoundManager.PlaySound(BaseSound + 3);
                    return;
            }
        }
        public void PlayDeadSound()
        {
            switch (BaseImage)
            {
                case Monster.CaveBat:
                case Monster.HellKnight1:
                case Monster.HellKnight2:
                case Monster.HellKnight3:
                case Monster.HellKnight4:
                case Monster.CyanoGhast:
                case Monster.WoodBox:
                    SoundManager.PlaySound(BaseSound + 5);
                    return;
            }
        }
        public void PlayReviveSound()
        {
            switch (BaseImage)
            {
                case Monster.ClZombie:
                case Monster.NdZombie:
                case Monster.CrawlerZombie:
                    SoundManager.PlaySound(SoundList.ZombieRevive);
                    return;
            }
        }
        public void PlayRangeSound()
        {
            switch (BaseImage)
            {
                case Monster.RedThunderZuma:
                case Monster.FrozenRedZuma:
                case Monster.KingScorpion:
                case Monster.DarkDevil:
                case Monster.Khazard:
                case Monster.BoneLord:
                case Monster.LeftGuard:
                case Monster.RightGuard:
                case Monster.FrostTiger:
                case Monster.GreatFoxSpirit:
                case Monster.BoneSpearman:
                case Monster.MinotaurKing:
                case Monster.WingedTigerLord:
                case Monster.ManectricClaw:
                case Monster.ManectricKing:
                case Monster.HellBolt:
                case Monster.WitchDoctor:
                case Monster.FlameSpear:
                case Monster.FlameMage:
                case Monster.FlameScythe:
                case Monster.FlameAssassin:
                case Monster.FlameQueen:
                case Monster.DarkDevourer:
                case Monster.DreamDevourer:
                case Monster.FlyingStatue:
                case Monster.IceGuard:
                case Monster.ElementGuard:
                case Monster.KingGuard:
                case Monster.Yimoogi:
                case Monster.RedYimoogi:
                case Monster.Snake10:
                case Monster.Snake11:
                case Monster.Snake12:
                case Monster.Snake13:
                case Monster.Snake14:
                case Monster.Snake15:
                case Monster.Snake16:
                case Monster.Snake17:
                case Monster.BurningZombie:
                case Monster.MudZombie:
                case Monster.FrozenZombie:
                case Monster.UndeadWolf:
                case Monster.CatShaman:
                case Monster.CannibalTentacles:
                case Monster.SwampWarrior:
                case Monster.GeneralMeowMeow:
                case Monster.RhinoPriest:
                case Monster.HardenRhino:
                case Monster.TreeGuardian:
                case Monster.OmaCannibal:
                case Monster.OmaMage:
                case Monster.OmaWitchDoctor:
                case Monster.CreeperPlant:
                case Monster.AvengingSpirit:
                case Monster.AvengingWarrior:
                case Monster.PeacockSpider:
                case Monster.FlamingMutant:
                case Monster.KingHydrax:
                case Monster.DarkCaptain:
                case Monster.DarkOmaKing:
                case Monster.HornedMage:
                case Monster.FrozenKnight:
                case Monster.IcePhantom:
                case Monster.WaterDragon:
                case Monster.BlackTortoise:
                    SoundManager.PlaySound(BaseSound + 5);
                    return;
                case Monster.AncientBringer:
                case Monster.SeedingsGeneral:
                    SoundManager.PlaySound(BaseSound + 7);
                    return;
                case Monster.RestlessJar:
                    SoundManager.PlaySound(BaseSound + 8);
                    return;
                case Monster.TucsonGeneral:
                    return;
                default:
                    PlayAttackSound();
                    return;
            }
        }
        public void PlaySecondRangeSound()
        {
            switch (BaseImage)
            {
                case Monster.TucsonGeneral:
                    SoundManager.PlaySound(BaseSound + 5);
                    return;
                case Monster.TurtleKing:
                    return;
                case Monster.KingGuard:
                case Monster.TreeGuardian:
                case Monster.DarkCaptain:
                case Monster.HornedCommander:
                    SoundManager.PlaySound(BaseSound + 7);
                    return;
                case Monster.AncientBringer:
                case Monster.SeedingsGeneral:
                    SoundManager.PlaySound(BaseSound + 8);
                    return;
                default:
                    PlaySecondAttackSound();
                    return;
            }
        }

        public void PlayThirdRangeSound()
        {
            switch (BaseImage)
            {
                case Monster.TucsonGeneral:
                    SoundManager.PlaySound(BaseSound + 7);
                    return;
                default:
                    PlayThirdAttackSound();
                    return;
            }
        }

        public void PlayPickupSound()
        {
            SoundManager.PlaySound(SoundList.PetPickup);
        }

        public void PlayPetSound()
        {
            int petSound = (ushort)BaseImage - 10000 + 10500;

            switch (BaseImage)
            {
                case Monster.Chick:
                case Monster.BabyPig:
                case Monster.Kitten:
                case Monster.BabySkeleton:
                case Monster.Baekdon:
                case Monster.Wimaen:
                case Monster.BlackKitten:
                case Monster.BabyDragon:
                case Monster.OlympicFlame:
                case Monster.BabySnowMan:
                case Monster.Frog:
                case Monster.BabyMonkey:
                case Monster.AngryBird:
                case Monster.Foxey:
                case Monster.MedicalRat:
                    SoundManager.PlaySound(petSound);
                    break;
            }
        }
        public override void Draw()
        {
            DrawBehindEffects(Settings.Effect);

            float oldOpacity = DXManager.Opacity;
            if (Hidden && !DXManager.Blending) DXManager.SetOpacity(0.5F);

            if (BodyLibrary == null || Frame == null) return;

            if (!DXManager.Blending && Frame.Blend)
                BodyLibrary.DrawBlend(DrawFrame, DrawLocation, DrawColour, true);
            else
                BodyLibrary.Draw(DrawFrame, DrawLocation, DrawColour, true);

            DXManager.SetOpacity(oldOpacity);
        }


        public override bool MouseOver(Point p)
        {
            return MapControl.MapLocation == CurrentLocation || BodyLibrary != null && BodyLibrary.VisiblePixel(DrawFrame, p.Subtract(FinalDrawLocation), false);
        }

        public override void DrawBehindEffects(bool effectsEnabled)
        {
            for (int i = 0; i < Effects.Count; i++)
            {
                if (!Effects[i].DrawBehind) continue;
                Effects[i].Draw();
            }
        }

        public override void DrawEffects(bool effectsEnabled)
        {
            if (!effectsEnabled) return;

            for (int i = 0; i < Effects.Count; i++)
            {
                if (Effects[i].DrawBehind) continue;
                Effects[i].Draw();
            }
        }

        public override void DrawName()
        {
            if (!Name.Contains("_"))
            {
                base.DrawName();
                return;
            }

            string[] splitName = Name.Split('_');

            //IntelligentCreature
            int yOffset = 0;
            switch (BaseImage)
            {
                case Monster.Chick:
                    yOffset = -10;
                    break;
                case Monster.BabyPig:
                case Monster.Kitten:
                case Monster.BabySkeleton:
                case Monster.Baekdon:
                case Monster.Wimaen:
                case Monster.BlackKitten:
                case Monster.BabyDragon:
                case Monster.OlympicFlame:
                case Monster.BabySnowMan:
                case Monster.Frog:
                case Monster.BabyMonkey:
                case Monster.AngryBird:
                case Monster.Foxey:
                case Monster.MedicalRat:
                    yOffset = -20;
                    break;
            }

            for (int s = 0; s < splitName.Count(); s++)
            {
                CreateMonsterLabel(splitName[s], s);

                TempLabel.Text = splitName[s];
                TempLabel.Location = new Point(DisplayRectangle.X + (48 - TempLabel.Size.Width) / 2, DisplayRectangle.Y - (32 - TempLabel.Size.Height / 2) + (Dead ? 35 : 8) - (((splitName.Count() - 1) * 10) / 2) + (s * 12) + yOffset);
                TempLabel.Draw();
            }
        }

        public void CreateMonsterLabel(string word, int wordOrder)
        {
            TempLabel = null;

            for (int i = 0; i < LabelList.Count; i++)
            {
                if (LabelList[i].Text != word) continue;
                TempLabel = LabelList[i];
                break;
            }

            if (TempLabel != null && !TempLabel.IsDisposed && NameColour == OldNameColor) return;

            OldNameColor = NameColour;

            TempLabel = new MirLabel
            {
                AutoSize = true,
                BackColour = WColor.Transparent,
                ForeColour = NameColour,
                OutLine = true,
                OutLineColour = WColor.Black,
                Text = word,
            };

            TempLabel.Disposing += (o, e) => LabelList.Remove(TempLabel);
            LabelList.Add(TempLabel);
        }

        public override void DrawChat()
        {
            if (ChatLabel == null || ChatLabel.IsDisposed) return;

            if (GameFrm.Time > ChatTime)
            {
                ChatLabel.Dispose();
                ChatLabel = null;
                return;
            }

            //IntelligentCreature
            int yOffset = 0;
            switch (BaseImage)
            {
                case Monster.Chick:
                    yOffset = 30;
                    break;
                case Monster.BabyPig:
                case Monster.Kitten:
                case Monster.BabySkeleton:
                case Monster.Baekdon:
                case Monster.Wimaen:
                case Monster.BlackKitten:
                case Monster.BabyDragon:
                case Monster.OlympicFlame:
                case Monster.BabySnowMan:
                case Monster.Frog:
                case Monster.BabyMonkey:
                case Monster.AngryBird:
                case Monster.Foxey:
                case Monster.MedicalRat:
                    yOffset = 20;
                    break;
            }

            ChatLabel.ForeColour = Dead ? WColor.Gray : WColor.White;
            ChatLabel.Location = new Point(DisplayRectangle.X + (48 - ChatLabel.Size.Width) / 2, DisplayRectangle.Y - (60 + ChatLabel.Size.Height) - (Dead ? 35 : 0) + yOffset);
            ChatLabel.Draw();
        }
    }

    public enum Monster : ushort
    {
        Guard = 0,
        TaoistGuard = 1,
        Guard2 = 2,
        Hen = 3,
        Deer = 4,
        Scarecrow = 5,
        HookingCat = 6,
        RakingCat = 7,
        Yob = 8,
        Oma = 9,
        CannibalPlant = 10,
        ForestYeti = 11,
        SpittingSpider = 12,
        ChestnutTree = 13,
        EbonyTree = 14,
        LargeMushroom = 15,
        CherryTree = 16,
        OmaFighter = 17,
        OmaWarrior = 18,
        CaveBat = 19,
        CaveMaggot = 20,
        Scorpion = 21,
        Skeleton = 22,
        BoneFighter = 23,
        AxeSkeleton = 24,
        BoneWarrior = 25,
        BoneElite = 26,
        Dung = 27,
        Dark = 28,
        WoomaSoldier = 29,
        WoomaFighter = 30,
        WoomaWarrior = 31,
        FlamingWooma = 32,
        WoomaGuardian = 33,
        WoomaTaurus = 34, //BOSS
        WhimperingBee = 35,
        GiantWorm = 36,
        Centipede = 37,
        BlackMaggot = 38,
        Tongs = 39,
        EvilTongs = 40,
        EvilCentipede = 41,
        BugBat = 42,
        BugBatMaggot = 43,
        WedgeMoth = 44,
        RedBoar = 45,
        BlackBoar = 46,
        SnakeScorpion = 47,
        WhiteBoar = 48,
        EvilSnake = 49,
        BombSpider = 50,
        RootSpider = 51,
        SpiderBat = 52,
        VenomSpider = 53,
        GangSpider = 54,
        GreatSpider = 55,
        LureSpider = 56,
        BigApe = 57,
        EvilApe = 58,
        GrayEvilApe = 59,
        RedEvilApe = 60,
        CrystalSpider = 61,
        RedMoonEvil = 62,
        BigRat = 63,
        ZumaArcher = 64,
        ZumaStatue = 65,
        ZumaGuardian = 66,
        RedThunderZuma = 67,
        ZumaTaurus = 68, //BOSS
        DigOutZombie = 69,
        ClZombie = 70,
        NdZombie = 71,
        CrawlerZombie = 72,
        ShamanZombie = 73,
        Ghoul = 74,
        KingScorpion = 75,
        KingHog = 76,
        DarkDevil = 77,
        BoneFamiliar = 78,
        Shinsu = 79,
        Shinsu1 = 80,
        SpiderFrog = 81,
        HoroBlaster = 82,
        BlueHoroBlaster = 83,
        KekTal = 84,
        VioletKekTal = 85,
        Khazard = 86,
        RoninGhoul = 87,
        ToxicGhoul = 88,
        BoneCaptain = 89,
        BoneSpearman = 90,
        BoneBlademan = 91,
        BoneArcher = 92,
        BoneLord = 93, //BOSS
        Minotaur = 94,
        IceMinotaur = 95,
        ElectricMinotaur = 96,
        WindMinotaur = 97,
        FireMinotaur = 98,
        RightGuard = 99,
        LeftGuard = 100,
        MinotaurKing = 101, //BOSS
        FrostTiger = 102,
        Sheep = 103,
        Wolf = 104,
        ShellNipper = 105,
        Keratoid = 106,
        GiantKeratoid = 107,
        SkyStinger = 108,
        SandWorm = 109,
        VisceralWorm = 110,
        RedSnake = 111,
        TigerSnake = 112,
        Yimoogi = 113,
        GiantWhiteSnake = 114,
        BlueSnake = 115,
        YellowSnake = 116,
        HolyDeva = 117,
        AxeOma = 118,
        SwordOma = 119,
        CrossbowOma = 120,
        WingedOma = 121,
        FlailOma = 122,
        OmaGuard = 123,
        YinDevilNode = 124,
        YangDevilNode = 125,
        OmaKing = 126, //BOSS
        BlackFoxman = 127,
        RedFoxman = 128,
        WhiteFoxman = 129,
        TrapRock = 130,
        GuardianRock = 131,
        ThunderElement = 132,
        CloudElement = 133,
        GreatFoxSpirit = 134, //BOSS
        HedgeKekTal = 135,
        BigHedgeKekTal = 136,
        RedFrogSpider = 137,
        BrownFrogSpider = 138,
        ArcherGuard = 139,
        KatanaGuard = 140,
        ArcherGuard2 = 141,
        Pig = 142,
        Bull = 143,
        Bush = 144,
        ChristmasTree = 145,
        HighAssassin = 146,
        DarkDustPile = 147,
        DarkBrownWolf = 148,
        Football = 149,
        GingerBreadman = 150,
        HalloweenScythe = 151,
        GhastlyLeecher = 152,
        CyanoGhast = 153,
        MutatedManworm = 154,
        CrazyManworm = 155,
        MudPile = 156,
        TailedLion = 157,
        Behemoth = 158, //BOSS
        DarkDevourer = 159,
        PoisonHugger = 160,
        Hugger = 161,
        MutatedHugger = 162,
        DreamDevourer = 163,
        Treasurebox = 164,
        SnowPile = 165,
        Snowman = 166,
        SnowTree = 167,
        GiantEgg = 168,
        RedTurtle = 169,
        GreenTurtle = 170,
        BlueTurtle = 171,
        Catapult1 = 172, //SPECIAL TODO
        Catapult2 = 173, //SPECIAL TODO
        OldSpittingSpider = 174,
        SiegeRepairman = 175, //SPECIAL TODO
        BlueSanta = 176,
        BattleStandard = 177,
        Blank1 = 178,
        RedYimoogi = 179,
        LionRiderMale = 180, //Not Monster - Skin / Transform
        LionRiderFemale = 181, //Not Monster - Skin / Transform
        Tornado = 182,
        FlameTiger = 183,
        WingedTigerLord = 184, //BOSS
        TowerTurtle = 185,
        FinialTurtle = 186,
        TurtleKing = 187, //BOSS
        DarkTurtle = 188,
        LightTurtle = 189,
        DarkSwordOma = 190,
        DarkAxeOma = 191,
        DarkCrossbowOma = 192,
        DarkWingedOma = 193,
        BoneWhoo = 194,
        DarkSpider = 195, //AI 8
        ViscusWorm = 196,
        ViscusCrawler = 197,
        CrawlerLave = 198,
        DarkYob = 199,
        FlamingMutant = 200,
        StoningStatue = 201, //BOSS
        FlyingStatue = 202,
        ValeBat = 203,
        Weaver = 204,
        VenomWeaver = 205,
        CrackingWeaver = 206,
        ArmingWeaver = 207,
        CrystalWeaver = 208,
        FrozenZumaStatue = 209,
        FrozenZumaGuardian = 210,
        FrozenRedZuma = 211,
        GreaterWeaver = 212,
        SpiderWarrior = 213,
        SpiderBarbarian = 214,
        HellSlasher = 215,
        HellPirate = 216,
        HellCannibal = 217,
        HellKeeper = 218, //BOSS
        HellBolt = 219,
        WitchDoctor = 220,
        ManectricHammer = 221,
        ManectricClub = 222,
        ManectricClaw = 223,
        ManectricStaff = 224,
        NamelessGhost = 225,
        DarkGhost = 226,
        ChaosGhost = 227,
        ManectricBlest = 228,
        ManectricKing = 229,
        Blank2 = 230,
        IcePillar = 231,
        FrostYeti = 232,
        ManectricSlave = 233,
        TrollHammer = 234,
        TrollBomber = 235,
        TrollStoner = 236,
        TrollKing = 237, //BOSS
        FlameSpear = 238,
        FlameMage = 239,
        FlameScythe = 240,
        FlameAssassin = 241,
        FlameQueen = 242, //BOSS
        HellKnight1 = 243,
        HellKnight2 = 244,
        HellKnight3 = 245,
        HellKnight4 = 246,
        HellLord = 247, //BOSS
        WaterGuard = 248,
        IceGuard = 249,
        ElementGuard = 250,
        DemonGuard = 251,
        KingGuard = 252,
        Snake10 = 253,
        Snake11 = 254,
        Snake12 = 255,
        Snake13 = 256,
        Snake14 = 257,
        Snake15 = 258,
        Snake16 = 259,
        Snake17 = 260,
        DeathCrawler = 261,
        BurningZombie = 262,
        MudZombie = 263,
        FrozenZombie = 264,
        UndeadWolf = 265,
        DemonWolf = 266,
        WhiteMammoth = 267,
        DarkBeast = 268,
        LightBeast = 269,//AI 112
        BloodBaboon = 270, //AI 112
        HardenRhino = 271,
        AncientBringer = 272,
        FightingCat = 273,
        FireCat = 274, //AI 44
        CatWidow = 275, //AI 112
        StainHammerCat = 276,
        BlackHammerCat = 277,
        StrayCat = 278,
        CatShaman = 279,
        Jar1 = 280,
        Jar2 = 281,
        SeedingsGeneral = 282,
        RestlessJar = 283,
        GeneralMeowMeow = 284, //BOSS
        Bunny = 285,
        Tucson = 286,
        TucsonFighter = 287, //AI 44
        TucsonMage = 288,
        TucsonWarrior = 289,
        Armadillo = 290,
        ArmadilloElder = 291,
        TucsonEgg = 292, //EFFECT 0/1
        PlaguedTucson = 293,
        SandSnail = 294,
        CannibalTentacles = 295,
        TucsonGeneral = 296, //BOSS
        GasToad = 297,
        Mantis = 298,
        SwampWarrior = 299,

        AssassinBird = 300,
        RhinoWarrior = 301,
        RhinoPriest = 302,
        ElephantMan = 303,
        StoneGolem = 304,
        EarthGolem = 305,
        TreeGuardian = 306,
        TreeQueen = 307,
        PeacockSpider = 308,
        DarkBaboon = 309, //AI 112
        TwinHeadBeast = 310, //AI 112
        OmaCannibal = 311,
        OmaBlest = 312,
        OmaSlasher = 313,
        OmaAssassin = 314,
        OmaMage = 315,
        OmaWitchDoctor = 316,
        LightningBead = 317, //Effect 0, AI 149
        HealingBead = 318, //Effect 1, AI 149
        PowerUpBead = 319, //Effect 2, AI 14
        DarkOmaKing = 320, //BOSS
        CaveStatue = 321,
        Mandrill = 322,
        PlagueCrab = 323,
        CreeperPlant = 324,
        FloatingWraith = 325, //AI 8
        ArmedPlant = 326,
        AvengerPlant = 327,
        Nadz = 328,
        AvengingSpirit = 329,
        AvengingWarrior = 330,
        AxePlant = 331,
        WoodBox = 332,
        ClawBeast = 333, //AI 8
        DarkCaptain = 334, //BOSS
        SackWarrior = 335,
        WereTiger = 336, //AI 112
        KingHydrax = 337,
        Hydrax = 338,
        HornedMage = 339,
        BlueSoul = 340,
        HornedArcher = 341,
        ColdArcher = 342,
        HornedWarrior = 343,
        FloatingRock = 344,
        ScalyBeast = 345,
        HornedSorceror = 346,
        BoulderSpirit = 347,
        HornedCommander = 348, //BOSS

        MoonStone = 349,
        SunStone = 350,
        LightningStone = 351,
        Turtlegrass = 352,
        ManTree = 353,
        Bear = 354,  //Effect 1, AI 112
        Leopard = 355,
        ChieftainArcher = 356,
        ChieftainSword = 357, //BOSS TODO
        StoningSpider = 358, //Archer Spell mob (not yet coded)
        VampireSpider = 359, //Archer Spell mob
        SpittingToad = 360, //Archer Spell mob
        SnakeTotem = 361, //Archer Spell mob
        CharmedSnake = 362, //Archer Spell mob
        FrozenSoldier = 363,
        FrozenFighter = 364, //AI 44
        FrozenArcher = 365, //AI 8
        FrozenKnight = 366,
        FrozenGolem = 367,
        IcePhantom = 368, //TODO - AI needs revisiting (blue explosion and snakes)
        SnowWolf = 369,
        SnowWolfKing = 370, //BOSS
        WaterDragon = 371,
        BlackTortoise = 372,
        Manticore = 373, //TODO
        DragonWarrior = 374, //Done (DG)
        DragonArcher = 375, //TODO - Wind Arrow spell
        Kirin = 376, // Done (jxtulong)
        Guard3 = 377,
        ArcherGuard3 = 378,
        Bunny2 = 379,
        FrozenMiner = 380, // Done (jxtulong)
        FrozenAxeman = 381, // Done (jxtulong)
        FrozenMagician = 382, // Done (jxtulong)
        SnowYeti = 383, // Done (jxtulong)
        IceCrystalSoldier = 384, // Done (jxtulong)
        DarkWraith = 385, // Done (jxtulong)
        DarkSpirit = 386, // Use AI 8 (AxeSkeleton)
        CrystalBeast = 387,
        RedOrb = 388,
        BlueOrb = 389,
        YellowOrb = 390,
        GreenOrb = 391,
        WhiteOrb = 392,
        FatalLotus = 393,
        AntCommander = 394,
        CargoBoxwithlogo = 395, // Done - Use CargoBox AI.
        Doe = 396, // TELEPORT = EFFECT 9
        Reindeer = 397, //frames not added
        AngryReindeer = 398,
        CargoBox = 399, // Done - Basically a Pinata.

        Ram1 = 400,
        Ram2 = 401,
        Kite = 402,
        PurpleFaeFlower = 403,
        Furball = 404,
        GlacierSnail = 405,
        FurbolgWarrior = 406,
        FurbolgArcher = 407,
        FurbolgCommander = 408,
        RedFaeFlower = 409,
        FurbolgGuard = 410,
        GlacierBeast = 411,
        GlacierWarrior = 412,
        ShardGuardian = 413,
        WarriorScroll = 414, // Use AI "HoodedSummonerScrolls" - Info.Effect = 0
        TaoistScroll = 415, // Use AI "HoodedSummonerScrolls" - Info.Effect = 1
        WizardScroll = 416, // Use AI "HoodedSummonerScrolls" - Info.Effect = 2
        AssassinScroll = 417, // Use AI "HoodedSummonerScrolls" - Info.Effect = 3
        HoodedSummoner = 418, //Summons Scrolls
        HoodedIceMage = 419,
        HoodedPriest = 420,
        ShardMaiden = 421,
        KingKong = 422,
        WarBear = 423,
        ReaperPriest = 424,
        ReaperWizard = 425,
        ReaperAssassin = 426,
        LivingVines = 427,
        BlueMonk = 428,
        MutantBeserker = 429,
        MutantGuardian = 430,
        MutantHighPriest = 431,
        MysteriousMage = 432,
        FeatheredWolf = 433,
        MysteriousAssassin = 434,
        MysteriousMonk = 435,
        ManEatingPlant = 436,
        HammerDwarf = 437,
        ArcherDwarf = 438,
        NobleWarrior = 439,
        NobleArcher = 440,
        NoblePriest = 441,
        NobleAssassin = 442,
        Swain = 443,
        RedMutantPlant = 444,
        BlueMutantPlant = 445,
        UndeadHammerDwarf = 446,
        UndeadDwarfArcher = 447,
        AncientStoneGolem = 448,
        Serpentirian = 449,

        Butcher = 450,
        Riklebites = 451,
        FeralTundraFurbolg = 452,
        FeralFlameFurbolg = 453,
        ArcaneTotem = 454,
        SpectralWraith = 455,
        BabyMagmaDragon = 456,
        BloodLord = 457,
        SerpentLord = 458,
        MirEmperor = 459,
        MutantManEatingPlant = 460,
        MutantWarg = 461,
        GrassElemental = 462,
        RockElemental = 463,

        //Special
        EvilMir = 900,
        EvilMirBody = 901,
        DragonStatue = 902,
        HellBomb1 = 903,
        HellBomb2 = 904,
        HellBomb3 = 905,

        //Siege
        Catapult = 940,
        ChariotBallista = 941,
        Ballista = 942,
        Trebuchet = 943,
        CanonTrebuchet = 944,

        //Gates
        SabukGate = 950,
        PalaceWallLeft = 951,
        PalaceWall1 = 952,
        PalaceWall2 = 953,
        GiGateSouth = 954,
        GiGateEast = 955,
        GiGateWest = 956,
        SSabukWall1 = 957,
        SSabukWall2 = 958,
        SSabukWall3 = 959,
        NammandGate1 = 960, //Not Coded
        NammandGate2 = 961, //Not Coded
        SabukWallSection = 962, //Not Coded
        NammandWallSection = 963, //Not Coded
        FrozenDoor = 964, //Not Coded

        //Flags 1000 ~ 1100

        //Creatures
        BabyPig = 10000,//Permanent
        Chick = 10001,//Special
        Kitten = 10002,//Permanent
        BabySkeleton = 10003,//Special
        Baekdon = 10004,//Special
        Wimaen = 10005,//Event
        BlackKitten = 10006,//unknown
        BabyDragon = 10007,//unknown
        OlympicFlame = 10008,//unknown
        BabySnowMan = 10009,//unknown
        Frog = 10010,//unknown
        BabyMonkey = 10011,//unknown
        AngryBird = 10012,
        Foxey = 10013,
        MedicalRat = 10014,
    }
}
