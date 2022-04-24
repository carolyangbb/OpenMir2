using MirClient.MirControls;
using MirClient.MirGraphics;
using MirClient.MirScenes;
using MirClient.MirSounds;
using SharpDX.Direct3D9;
using SharpDX.Mathematics.Interop;
using SharpDX.Windows;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using System.Windows.Forms;

namespace MirClient
{
    public partial class GameFrm : RenderForm
    {
        public static readonly Stopwatch Timer = Stopwatch.StartNew();
        public static readonly DateTime StartTime = DateTime.UtcNow;
        public static DateTime Now { get { return StartTime.AddMilliseconds(Time); } }
        public static MirControl DebugBaseLabel, HintBaseLabel;
        public static MirLabel DebugTextLabel, HintTextLabel, ScreenshotTextLabel;
        public static long Time;
        public static Graphics Graphics;
        public static Point MPoint;
        public static bool Shift, Alt, Ctrl, Tilde;
        private static long _fpsTime;
        private static int _fps;
        private static long _cleanTime;
        private static long _drawTime;
        public static int FPS;
        public static int DPS;
        public static int DPSCounter;
        public static Cursor[] Cursors;
        public static MouseCursor CurrentCursor = MouseCursor.None;

        public GameFrm()
        {
            InitializeComponent();

            //MouseClick += CMain_MouseClick;
            //MouseDown += CMain_MouseDown;
            //MouseUp += CMain_MouseUp;
            //MouseMove += CMain_MouseMove;
            //MouseDoubleClick += CMain_MouseDoubleClick;
            //KeyPress += CMain_KeyPress;
            //KeyDown += CMain_KeyDown;
            //KeyUp += CMain_KeyUp;
            //Deactivate += CMain_Deactivate;
            //MouseWheel += CMain_MouseWheel;

            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.Selectable, true);
            FormBorderStyle = Settings.FullScreen || Settings.Borderless ? FormBorderStyle.FixedSingle : FormBorderStyle.FixedDialog;

            Graphics = CreateGraphics();
            Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            Graphics.CompositingQuality = CompositingQuality.HighQuality;
            Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            Graphics.TextContrast = 0;

            Application.Idle += Application_Idle;
        }

        private void GameFrm_Load(object sender, EventArgs e)
        {
            try
            {
                ClientSize = new Size(Settings.ScreenWidth, Settings.ScreenHeight);

                LoadMouseCursors();
                SetMouseCursor(MouseCursor.Default);

                DXManager.Create();
                SoundManager.Create();
                CenterToScreen();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void SetMouseCursor(MouseCursor cursor)
        {
            if (!Settings.UseMouseCursors) return;

            if (CurrentCursor != cursor)
            {
                CurrentCursor = cursor;
                Program.Form.Cursor = Cursors[(byte)cursor];
            }
        }

        private static void LoadMouseCursors()
        {
            Cursors = new Cursor[8];

            Cursors[(int)MouseCursor.None] = Program.Form.Cursor;

            string path = $"{Settings.MouseCursorPath}Cursor_Default.CUR";
            if (File.Exists(path))
                Cursors[(int)MouseCursor.Default] = LoadCustomCursor(path);

            path = $"{Settings.MouseCursorPath}Cursor_Normal_Atk.CUR";
            if (File.Exists(path))
                Cursors[(int)MouseCursor.Attack] = LoadCustomCursor(path);

            path = $"{Settings.MouseCursorPath}Cursor_Compulsion_Atk.CUR";
            if (File.Exists(path))
                Cursors[(int)MouseCursor.AttackRed] = LoadCustomCursor(path);

            path = $"{Settings.MouseCursorPath}Cursor_Npc.CUR";
            if (File.Exists(path))
                Cursors[(int)MouseCursor.NPCTalk] = LoadCustomCursor(path);

            path = $"{Settings.MouseCursorPath}Cursor_TextPrompt.CUR";
            if (File.Exists(path))
                Cursors[(int)MouseCursor.TextPrompt] = LoadCustomCursor(path);

            path = $"{Settings.MouseCursorPath}Cursor_Trash.CUR";
            if (File.Exists(path))
                Cursors[(int)MouseCursor.Trash] = LoadCustomCursor(path);

            path = $"{Settings.MouseCursorPath}Cursor_Upgrade.CUR";
            if (File.Exists(path))
                Cursors[(int)MouseCursor.Upgrade] = LoadCustomCursor(path);
        }

        public static Cursor LoadCustomCursor(string path)
        {
            IntPtr hCurs = LoadCursorFromFile(path);
            if (hCurs == IntPtr.Zero) throw new Win32Exception();
            var curs = new Cursor(hCurs);
            // Note: force the cursor to own the handle so it gets released properly
            var fi = typeof(Cursor).GetField("ownHandle", BindingFlags.NonPublic | BindingFlags.Instance);
            fi.SetValue(curs, true);
            return curs;
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern IntPtr LoadCursorFromFile(string path);

        private static void UpdateTime()
        {
            Time = Timer.ElapsedMilliseconds;
        }

        private static void UpdateFrameTime()
        {
            if (Time >= _fpsTime)
            {
                _fpsTime = Time + 1000;
                FPS = _fps;
                _fps = 0;

                DPS = DPSCounter;
                DPSCounter = 0;
            }
            else
                _fps++;
        }

        private static bool IsDrawTime()
        {
            const int TargetUpdates = 1000 / 60; // 60 frames per second

            if (Time >= _drawTime)
            {
                _drawTime = Time + TargetUpdates;
                return true;
            }
            return false;
        }

        private static bool AppStillIdle
        {
            get
            {
                PeekMsg msg;
                return !PeekMessage(out msg, IntPtr.Zero, 0, 0, 0);
            }
        }

        [SuppressUnmanagedCodeSecurity]
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        private static extern bool PeekMessage(out PeekMsg msg, IntPtr hWnd, uint messageFilterMin,
                                               uint messageFilterMax, uint flags);

        [StructLayout(LayoutKind.Sequential)]
        private struct PeekMsg
        {
            private readonly IntPtr hWnd;
            private readonly Message msg;
            private readonly IntPtr wParam;
            private readonly IntPtr lParam;
            private readonly uint time;
            private readonly Point p;
        }

        private static void Application_Idle(object sender, EventArgs e)
        {
            try
            {
                while (AppStillIdle)
                {
                    UpdateTime();
                    UpdateFrameTime();
                    UpdateEnviroment();

                    if (IsDrawTime())
                        RenderEnvironment();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void UpdateEnviroment()
        {
            if (Time >= _cleanTime)
            {
                _cleanTime = Time + 1000;

                DXManager.Clean(); // Clean once a second.
            }

            //Network.Process();

            if (MirScene.ActiveScene != null)
                MirScene.ActiveScene.Process();

            for (int i = 0; i < MirAnimatedControl.Animations.Count; i++)
                MirAnimatedControl.Animations[i].UpdateOffSet();

            for (int i = 0; i < MirAnimatedButton.Animations.Count; i++)
                MirAnimatedButton.Animations[i].UpdateOffSet();

            //CreateHintLabel();

            if (Settings.DebugMode)
            {
                CreateDebugLabel();
            }
        }

        public static void RenderEnvironment()
        {
            try
            {
                if (DXManager.DeviceLost)
                {
                    DXManager.AttemptReset();
                    Thread.Sleep(1);
                    return;
                }
                DXManager.Device.Clear(ClearFlags.Target, new RawColorBGRA(Color.CornflowerBlue.B, Color.CornflowerBlue.G, Color.CornflowerBlue.R, Color.CornflowerBlue.A), 0, 0);
                DXManager.Device.BeginScene();
                DXManager.Sprite.Begin(SpriteFlags.AlphaBlend);
                DXManager.SetSurface(DXManager.MainSurface);

                if (MirScene.ActiveScene != null)
                    MirScene.ActiveScene.Draw();

                DXManager.Sprite.End();
                DXManager.Device.EndScene();
                DXManager.Device.Present();
            }
            /*catch (Exception ex)
            {
                DXManager.DeviceLost = true;
            }*/
            catch (Exception ex)
            {
                DXManager.AttemptRecovery();
                throw new Exception(ex.Message);
            }
        }

        private static void CMain_Deactivate(object sender, EventArgs e)
        {
            //MapControl.MapButtons = MouseButtons.None;
            Shift = false;
            Alt = false;
            Ctrl = false;
            Tilde = false;
        }

        public static void CMain_KeyDown(object sender, KeyEventArgs e)
        {
            Shift = e.Shift;
            Alt = e.Alt;
            Ctrl = e.Control;

            if (e.KeyCode == Keys.Oem8)
                Tilde = true;

            try
            {
                if (e.Alt && e.KeyCode == Keys.Enter)
                {
                    //ToggleFullScreen();
                    return;
                }

                if (MirScene.ActiveScene != null)
                    MirScene.ActiveScene.OnKeyDown(e);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (Settings.FullScreen || Settings.MouseClip)
                Cursor.Clip = Program.Form.RectangleToScreen(Program.Form.ClientRectangle);

            MPoint = Program.Form.PointToClient(Cursor.Position);

            try
            {
                if (MirScene.ActiveScene != null)
                    MirScene.ActiveScene.OnMouseMove(e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CMain_KeyUp(object sender, KeyEventArgs e)
        {
            Shift = e.Shift;
            Alt = e.Alt;
            Ctrl = e.Control;

            if (e.KeyCode == Keys.Oem8)
                Tilde = false;

            //foreach (KeyBind KeyCheck in CMain.InputKeys.Keylist)
            //{
            //    if (KeyCheck.function != KeybindOptions.Screenshot) continue;
            //    if (KeyCheck.Key != e.KeyCode)
            //        continue;
            //    if ((KeyCheck.RequireAlt != 2) && (KeyCheck.RequireAlt != (Alt ? 1 : 0)))
            //        continue;
            //    if ((KeyCheck.RequireShift != 2) && (KeyCheck.RequireShift != (Shift ? 1 : 0)))
            //        continue;
            //    if ((KeyCheck.RequireCtrl != 2) && (KeyCheck.RequireCtrl != (Ctrl ? 1 : 0)))
            //        continue;
            //    if ((KeyCheck.RequireTilde != 2) && (KeyCheck.RequireTilde != (Tilde ? 1 : 0)))
            //        continue;
            //    Program.Form.CreateScreenShot();
            //    break;
            //}
            try
            {
                if (MirScene.ActiveScene != null)
                    MirScene.ActiveScene.OnKeyUp(e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CMain_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (MirScene.ActiveScene != null)
                    MirScene.ActiveScene.OnKeyPress(e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static void CMain_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (MirScene.ActiveScene != null)
                    MirScene.ActiveScene.OnMouseClick(e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CMain_MouseUp(object sender, MouseEventArgs e)
        {
            //MapControl.MapButtons &= ~e.Button;
            //if (e.Button != MouseButtons.Right || !Settings.NewMove)
            //    GameScene.CanRun = false;

            try
            {
                if (MirScene.ActiveScene != null)
                    MirScene.ActiveScene.OnMouseUp(e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (Program.Form.ActiveControl is TextBox)
            {
                MirTextBox textBox = Program.Form.ActiveControl.Tag as MirTextBox;

                if (textBox != null && textBox.CanLoseFocus)
                    Program.Form.ActiveControl = null;
            }

            //if (e.Button == MouseButtons.Right && (GameScene.SelectedCell != null || GameScene.PickedUpGold))
            //{
            //    GameScene.SelectedCell = null;
            //    GameScene.PickedUpGold = false;
            //    return;
            //}

            try
            {
                if (MirScene.ActiveScene != null)
                    MirScene.ActiveScene.OnMouseDown(e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CMain_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (MirScene.ActiveScene != null)
                    MirScene.ActiveScene.OnMouseClick(e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CMain_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (MirScene.ActiveScene != null)
                    MirScene.ActiveScene.OnMouseWheel(e);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static void CreateDebugLabel()
        {
            string text;

            if (MirControl.MouseControl != null)
            {
                text = string.Format("FPS: {0}", FPS);

                text += string.Format(", DPS: {0}", DPS);

                text += string.Format(", Time: {0:HH:mm:ss UTC}", Now);

                //if (MirControl.MouseControl is MapControl)
                //    text += string.Format(", Co Ords: {0}", MapControl.MapLocation);

                //if (MirControl.MouseControl is MirImageControl)
                //    text += string.Format(", Control: {0}", MirControl.MouseControl.GetType().Name);

                //if (MirScene.ActiveScene is MirScene)
                //    text += string.Format(", Objects: {0}", MapControl.Objects.Count);

                //if (MirScene.ActiveScene is MirScene && !string.IsNullOrEmpty(DebugText))
                //    text += string.Format(", Debug: {0}", DebugText);

                //if (MirObjects.MapObject.MouseObject != null)
                //{
                //    text += string.Format(", Target: {0}", MirObjects.MapObject.MouseObject.Name);
                //}
                //else
                //{
                //    text += string.Format(", Target: none");
                //}
            }
            else
            {
                text = string.Format("FPS: {0}", FPS);
            }

            //text += string.Format(", Ping: {0}", PingTime);
            //text += string.Format(", Sent: {0}, Received: {1}", Functions.ConvertByteSize(BytesSent), Functions.ConvertByteSize(BytesReceived));

            text += string.Format(", TLC: {0}", DXManager.TextureList.Count(x => x.TextureValid));
            text += string.Format(", CLC: {0}", DXManager.ControlList.Count(x => x.IsDisposed == false));

            if (Settings.FullScreen)
            {
                if (DebugBaseLabel == null || DebugBaseLabel.IsDisposed)
                {
                    DebugBaseLabel = new MirControl
                    {
                        BackColour = Color.FromArgb(50, 50, 50),
                        Border = true,
                        BorderColour = Color.Black,
                        DrawControlTexture = true,
                        Location = new Point(5, 5),
                        NotControl = true,
                        Opacity = 0.5F
                    };
                }

                if (DebugTextLabel == null || DebugTextLabel.IsDisposed)
                {
                    DebugTextLabel = new MirLabel
                    {
                        AutoSize = true,
                        BackColour = Color.Transparent,
                        ForeColour = Color.White,
                        Parent = DebugBaseLabel,
                    };

                    DebugTextLabel.SizeChanged += (o, e) => DebugBaseLabel.Size = DebugTextLabel.Size;
                }

                DebugTextLabel.Text = text;
            }
            else
            {
                if (DebugBaseLabel != null && DebugBaseLabel.IsDisposed == false)
                {
                    DebugBaseLabel.Dispose();
                    DebugBaseLabel = null;
                }
                if (DebugTextLabel != null && DebugTextLabel.IsDisposed == false)
                {
                    DebugTextLabel.Dispose();
                    DebugTextLabel = null;
                }

                Program.Form.Text = $"OpenMir2 - {text}";
            }
        }
    }
}
