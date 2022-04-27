using MirClient.MirControls;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.Mathematics.Interop;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Blend = SharpDX.Direct3D9.Blend;
using Color = SharpDX.Color;
using WColor = System.Drawing.Color;
using Point = System.Drawing.Point;
using Rectangle = SharpDX.Rectangle;
using MirClient.MirScenes;

namespace MirClient.MirGraphics
{
    class DXManager
    {
        public static List<MImage> TextureList = new List<MImage>();
        public static List<MirControl> ControlList = new List<MirControl>();

        public static Device Device;
        public static Sprite Sprite;
        public static Line Line;

        public static Surface CurrentSurface;
        public static Surface MainSurface;
        public static PresentParameters Parameters;
        public static bool DeviceLost;
        public static float Opacity = 1F;
        public static bool Blending;
        public static float BlendingRate;
        public static BlendMode BlendingMode;


        public static Texture RadarTexture;
        public static List<Texture> Lights = new List<Texture>();
        public static Texture PoisonDotBackground;

        /// <summary>
        /// 地图纹理
        /// </summary>
        public static Texture MapTexture;
        /// <summary>
        /// 黑暗纹理
        /// </summary>
        public static Texture LightTexture;
        /// <summary>
        /// 地图画布
        /// </summary>
        public static Surface MapSurface;
        /// <summary>
        /// 黑暗画布
        /// </summary>
        public static Surface LightSurface;

        public static PixelShader GrayScalePixelShader;
        public static PixelShader NormalPixelShader;
        public static PixelShader MagicPixelShader;
        public static PixelShader ShadowPixelShader;

        public static bool GrayScale;

        public static Point[] LightSizes =
        {
            new Point(125,95),
            new Point(205,156),
            new Point(285,217),
            new Point(365,277),
            new Point(445,338),
            new Point(525,399),
            new Point(605,460),
            new Point(685,521),
            new Point(765,581),
            new Point(845,642),
            new Point(925,703)
        };

        public static void Create()
        {
            Parameters = new PresentParameters
            {
                BackBufferFormat = Format.X8R8G8B8,
                PresentFlags = PresentFlags.LockableBackBuffer,
                BackBufferWidth = Settings.ScreenWidth,
                BackBufferHeight = Settings.ScreenHeight,
                SwapEffect = SwapEffect.Discard,
                PresentationInterval = Settings.FPSCap ? PresentInterval.One : PresentInterval.Immediate,
                Windowed = !Settings.FullScreen
            };

            Direct3D d3d = new Direct3D();

            Capabilities devCaps = d3d.GetDeviceCaps(0, DeviceType.Hardware);
            DeviceType devType = DeviceType.Reference;
            CreateFlags devFlags = CreateFlags.HardwareVertexProcessing;

            if (devCaps.VertexShaderVersion.Major >= 2 && devCaps.PixelShaderVersion.Major >= 2)
                devType = DeviceType.Hardware;

            if ((devCaps.DeviceCaps & DeviceCaps.HWTransformAndLight) != 0)
                devFlags = CreateFlags.HardwareVertexProcessing;


            if ((devCaps.DeviceCaps & DeviceCaps.PureDevice) != 0)
                devFlags |= CreateFlags.PureDevice;

            Device = new Device(d3d, 0, devType, Program.Form.Handle, devFlags, Parameters);

            Device.DialogBoxMode = true;

            LoadTextures();
            LoadPixelsShaders();
        }

        private static unsafe void LoadPixelsShaders()
        {
            var shaderNormalPath = Settings.ShadersPath + "normal.ps";
            var shaderGrayScalePath = Settings.ShadersPath + "grayscale.ps";
            var shaderMagicPath = Settings.ShadersPath + "magic.ps";

            if (File.Exists(shaderNormalPath))
            {
                using (var gs = ShaderBytecode.AssembleFromFile(shaderNormalPath, ShaderFlags.None))
                    NormalPixelShader = new PixelShader(Device, gs);
            }
            if (File.Exists(shaderGrayScalePath))
            {
                using (var gs = ShaderBytecode.AssembleFromFile(shaderGrayScalePath, ShaderFlags.None))
                    GrayScalePixelShader = new PixelShader(Device, gs);
            }
            if (File.Exists(shaderMagicPath))
            {
                using (var gs = ShaderBytecode.AssembleFromFile(shaderMagicPath, ShaderFlags.None))
                    MagicPixelShader = new PixelShader(Device, gs);
            }
        }

        private static unsafe void LoadTextures()
        {
            Sprite = new Sprite(Device);
            Line = new Line(Device) { Width = 1F };

            MainSurface = Device.GetBackBuffer(0, 0);
            CurrentSurface = MainSurface;
            Device.SetRenderTarget(0, MainSurface);

            if (RadarTexture == null || RadarTexture.IsDisposed)
            {
                RadarTexture = new Texture(Device, 2, 2, 1, Usage.None, Format.A8R8G8B8, Pool.Managed);

                DataRectangle stream = RadarTexture.LockRectangle(0, LockFlags.Discard);
                using (Bitmap image = new Bitmap(2, 2, 8, PixelFormat.Format32bppArgb, stream.DataPointer))
                using (Graphics graphics = Graphics.FromImage(image))
                    graphics.Clear(WColor.White);
                RadarTexture.UnlockRectangle(0);
            }
            if (PoisonDotBackground == null || PoisonDotBackground.IsDisposed)
            {
                PoisonDotBackground = new Texture(Device, 5, 5, 1, Usage.None, Format.A8R8G8B8, Pool.Managed);

                DataRectangle stream = PoisonDotBackground.LockRectangle(0, LockFlags.Discard);
                using (Bitmap image = new Bitmap(5, 5, 20, PixelFormat.Format32bppArgb, stream.DataPointer))
                using (Graphics graphics = Graphics.FromImage(image))
                    graphics.Clear(WColor.White);
                PoisonDotBackground.UnlockRectangle(0);
            }
            CreateLights();
        }

        private static unsafe void CreateLights()
        {
            for (int i = Lights.Count - 1; i >= 0; i--)
                Lights[i].Dispose();

            Lights.Clear();

            for (int i = 1; i < LightSizes.Length; i++)
            {
                // int width = 125 + (57 *i);
                //int height = 110 + (57 * i);
                int width = LightSizes[i].X;
                int height = LightSizes[i].Y;

                Texture light = new Texture(Device, width, height, 1, Usage.None, Format.A8R8G8B8, Pool.Managed);

                DataRectangle stream = light.LockRectangle(0, LockFlags.Discard);
                using (Bitmap image = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, stream.DataPointer))
                {
                    using (Graphics graphics = Graphics.FromImage(image))
                    {
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            path.AddEllipse(new System.Drawing.RectangleF(0, 0, width, height));
                            using (PathGradientBrush brush = new PathGradientBrush(path))
                            {
                                WColor[] blendColours = { WColor.White,
                                                     WColor.FromArgb(255,210,210,210),
                                                     WColor.FromArgb(255,160,160,160),
                                                     WColor.FromArgb(255,70,70,70),
                                                     WColor.FromArgb(255,40,40,40),
                                                     WColor.FromArgb(0,0,0,0)};

                                float[] radiusPositions = { 0f, .20f, .40f, .60f, .80f, 1.0f };

                                ColorBlend colourBlend = new ColorBlend();
                                colourBlend.Colors = blendColours;
                                colourBlend.Positions = radiusPositions;

                                graphics.Clear(WColor.FromArgb(0, 0, 0, 0));
                                brush.InterpolationColors = colourBlend;
                                brush.SurroundColors = blendColours;
                                brush.CenterColor = WColor.White;
                                graphics.FillPath(brush, path);
                                graphics.Save();
                            }
                        }
                    }
                }

                light.UnlockRectangle(0);
                //light.Disposing += (o, e) => Lights.Remove(light);
                Lights.Add(light);
            }
        }

        public static void SetSurface(Surface surface)
        {
            if (CurrentSurface == surface)
                return;

            Sprite.Flush();
            CurrentSurface = surface;
            Device.SetRenderTarget(0, surface);
        }

        public static void SetGrayscale(bool value)
        {
            GrayScale = value;

            if (value == true)
            {
                if (Device.PixelShader == GrayScalePixelShader) return;
                Sprite.Flush();
                Device.PixelShader = GrayScalePixelShader;
            }
            else
            {
                if (Device.PixelShader == null) return;
                Sprite.Flush();
                Device.PixelShader = null;
            }
        }

        public static void DrawOpaque(Texture texture, Rectangle? sourceRect, Vector3? position, Color color, float opacity)
        {
            color.A = 255;
            Draw(texture, sourceRect, position, color);
        }

        static RawColor4 ColorToRaw4(Color color)
        {
            const float n = 255f;
            return new RawColor4(color.R / n, color.G / n, color.B / n, color.A / n);
        }

        public static void Draw(Texture texture, Rectangle? sourceRect, Vector3? position, Color color)
        {
            Sprite.Draw(texture, color, sourceRect, Vector3.Zero, position);
            GameFrm.DPSCounter++;
        }

        public static void AttemptReset()
        {
            try
            {
                Result result = Device.TestCooperativeLevel();

                if (result.Code == ResultCode.DeviceLost.Code) return;

                if (result.Code == ResultCode.DeviceNotReset.Code)
                {
                    ResetDevice();
                    return;
                }

                if (result.Code != ResultCode.Success.Code) return;

                DeviceLost = false;
            }
            catch
            {
            }
        }

        public static void ResetDevice()
        {
            CleanUp();
            DeviceLost = true;

            // if (DXManager.Parameters == null) return;

            Size clientSize = Program.Form.ClientSize;

            if (clientSize.Width == 0 || clientSize.Height == 0) return;

            Parameters.Windowed = !Settings.FullScreen;
            Parameters.BackBufferWidth = clientSize.Width;
            Parameters.BackBufferHeight = clientSize.Height;
            Parameters.PresentationInterval = Settings.FPSCap ? PresentInterval.Default : PresentInterval.Immediate;
            Device.Reset(Parameters);
            LoadTextures();
        }

        public static void AttemptRecovery()
        {
            try
            {
                Sprite.End();
            }
            catch
            {
            }

            try
            {
                Device.EndScene();
            }
            catch
            {
            }

            try
            {
                MainSurface = Device.GetBackBuffer(0, 0);
                CurrentSurface = MainSurface;
                Device.SetRenderTarget(0, MainSurface);
            }
            catch
            {
            }
        }

        public static void SetOpacity(float opacity)
        {
            if (Opacity == opacity)
                return;

            Sprite.Flush();
            Device.SetRenderState(RenderState.AlphaBlendEnable, true);
            if (opacity >= 1 || opacity < 0)
            {
                Device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
                Device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceAlpha);
                Device.SetRenderState(RenderState.SourceBlendAlpha, Blend.One);
                Device.SetRenderState(RenderState.BlendFactor, WColor.FromArgb(255, 255, 255, 255).ToArgb());
            }
            else
            {
                Device.SetRenderState(RenderState.SourceBlend, Blend.BlendFactor);
                Device.SetRenderState(RenderState.DestinationBlend, Blend.InverseBlendFactor);
                Device.SetRenderState(RenderState.SourceBlendAlpha, Blend.SourceAlpha);
                Device.SetRenderState(RenderState.BlendFactor, WColor.FromArgb((byte)(255 * opacity), (byte)(255 * opacity), (byte)(255 * opacity), (byte)(255 * opacity)).ToArgb());
            }
            Opacity = opacity;
            Sprite.Flush();
        }

        public static void SetBlend(bool value, float rate = 1F, BlendMode mode = BlendMode.NORMAL)
        {
            if (value == Blending && BlendingRate == rate && BlendingMode == mode) return;

            Blending = value;
            BlendingRate = rate;
            BlendingMode = mode;

            Sprite.Flush();

            Sprite.End();

            if (Blending)
            {
                Sprite.Begin(SpriteFlags.DoNotSaveState);
                Device.SetRenderState(RenderState.AlphaBlendEnable, true);

                switch (BlendingMode)
                {
                    case BlendMode.INVLIGHT:
                        Device.SetRenderState(RenderState.BlendOperation, BlendOperation.Add);
                        Device.SetRenderState(RenderState.SourceBlend, Blend.BlendFactor);
                        Device.SetRenderState(RenderState.DestinationBlend, Blend.InverseSourceColor);
                        break;
                    default:
                        Device.SetRenderState(RenderState.SourceBlend, Blend.SourceAlpha);
                        Device.SetRenderState(RenderState.DestinationBlend, Blend.One);
                        break;
                }
                Device.SetRenderState(RenderState.BlendFactor, new Color((byte)(255 * BlendingRate), (byte)(255 * BlendingRate),
                                                                (byte)(255 * BlendingRate), (byte)(255 * BlendingRate)).ToRgba());
            }
            else
                Sprite.Begin(SpriteFlags.AlphaBlend);

            Device.SetRenderTarget(0, CurrentSurface);
        }

        public static void SetNormal(float blend, Color4 tintcolor)
        {
            if (Device.PixelShader == NormalPixelShader)
                return;

            Sprite.Flush();
            Device.PixelShader = NormalPixelShader;
            Device.SetPixelShaderConstant(0, new RawVector4[] { new Vector4(1.0F, 1.0F, 1.0F, blend) });
            Device.SetPixelShaderConstant(1, new RawVector4[] { new Vector4(tintcolor.Red / 255, tintcolor.Green / 255, tintcolor.Blue / 255, 1.0F) });
            Sprite.Flush();
        }

        public static void SetGrayscale(float blend, Color4 tintcolor)
        {
            if (Device.PixelShader == GrayScalePixelShader)
                return;

            Sprite.Flush();
            Device.PixelShader = GrayScalePixelShader;
            Device.SetPixelShaderConstant(0, new RawVector4[] { new Vector4(1.0F, 1.0F, 1.0F, blend) });
            Device.SetPixelShaderConstant(1, new RawVector4[] { new Vector4(tintcolor.Red / 255, tintcolor.Green / 255, tintcolor.Blue / 255, 1.0F) });
            Sprite.Flush();
        }

        public static void SetBlendMagic(float blend, Color4 tintcolor)
        {
            if (Device.PixelShader == MagicPixelShader || MagicPixelShader == null)
                return;

            Sprite.Flush();
            Device.PixelShader = MagicPixelShader;
            Device.SetPixelShaderConstant(0, new RawVector4[] { new Vector4(1.0F, 1.0F, 1.0F, blend) });
            Device.SetPixelShaderConstant(1, new RawVector4[] { new Vector4(tintcolor.Red / 255, tintcolor.Green / 255, tintcolor.Blue / 255, 1.0F) });
            Sprite.Flush();
        }

        public static void Clean()
        {
            for (int i = TextureList.Count - 1; i >= 0; i--)
            {
                MImage m = TextureList[i];

                if (m == null)
                {
                    TextureList.RemoveAt(i);
                    continue;
                }

                if (GameFrm.Time <= m.CleanTime) continue;

                m.DisposeTexture();
            }

            for (int i = ControlList.Count - 1; i >= 0; i--)
            {
                MirControl c = ControlList[i];

                if (c == null)
                {
                    ControlList.RemoveAt(i);
                    continue;
                }

                if (GameFrm.Time <= c.CleanTime) continue;

                c.DisposeTexture();
            }
        }

        private static void CleanUp()
        {
            if (Sprite != null)
            {
                if (!Sprite.IsDisposed)
                {
                    Sprite.Dispose();
                }

                Sprite = null;
            }

            if (Line != null)
            {
                if (!Line.IsDisposed)
                {
                    Line.Dispose();
                }

                Line = null;
            }

            if (CurrentSurface != null)
            {
                if (!CurrentSurface.IsDisposed)
                {
                    CurrentSurface.Dispose();
                }

                CurrentSurface = null;
            }

            if (PoisonDotBackground != null)
            {
                if (!PoisonDotBackground.IsDisposed)
                {
                    PoisonDotBackground.Dispose();
                }

                PoisonDotBackground = null;
            }

            if (RadarTexture != null)
            {
                if (!RadarTexture.IsDisposed)
                {
                    RadarTexture.Dispose();
                }

                RadarTexture = null;
            }

            if (MapTexture != null)
            {
                if (!MapTexture.IsDisposed)
                {
                    MapTexture.Dispose();
                }

                MapTexture = null;
                GameScene.Scene.MapControl.FloorValid = false;

                if (MapSurface != null && !MapSurface.IsDisposed)
                {
                    MapSurface.Dispose();
                }

                MapSurface = null;
            }

            if (LightTexture != null)
            {
                if (!LightTexture.IsDisposed)
                    LightTexture.Dispose();

                LightTexture = null;

                if (LightSurface != null && !LightSurface.IsDisposed)
                {
                    LightSurface.Dispose();
                }

                LightSurface = null;
            }

            if (Lights != null)
            {
                for (int i = 0; i < Lights.Count; i++)
                {
                    if (!Lights[i].IsDisposed)
                        Lights[i].Dispose();
                }
                Lights.Clear();
            }

            for (int i = TextureList.Count - 1; i >= 0; i--)
            {
                MImage m = TextureList[i];

                if (m == null) continue;

                m.DisposeTexture();
            }
            TextureList.Clear();


            for (int i = ControlList.Count - 1; i >= 0; i--)
            {
                MirControl c = ControlList[i];

                if (c == null) continue;

                c.DisposeTexture();
            }
            ControlList.Clear();
        }

    }
}