using MirClient.MirGraphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MirClient.MirScenes
{
    public class TestScene : MirScene
    {

        public bool te = false;
        public TestScene()
        {
            BackColour = Color.Black;


        }

        public override void Draw()
        {
            if (DXManager.MapTexture == null || DXManager.MapTexture.IsDisposed)
            {
                DXManager.MapTexture = new Texture(DXManager.Device, Settings.ScreenWidth, Settings.ScreenHeight, 1, Usage.RenderTarget, Format.A8R8G8B8, Pool.Default);
                DXManager.MapSurface = DXManager.MapTexture.GetSurfaceLevel(0);
            }

            //if (!te)
            //{
                Surface oldSurface = DXManager.CurrentSurface;

                DXManager.SetSurface(DXManager.MapSurface);
                DXManager.Device.Clear(ClearFlags.Target, SharpDX.Color.Black, 0, 0); //Color.Black

                Libraries.MapLibs[0].Draw(1800, 100, 100);
                Libraries.MapLibs[0].Draw(1802, 120, 120);
                Libraries.MapLibs[0].Draw(1803, 120, 130);
                Libraries.MapLibs[0].Draw(1804, 130, 140);
                Libraries.MapLibs[0].Draw(1805, 140, 150);
                Libraries.MapLibs[0].Draw(1806, 150, 160);


                DXManager.SetSurface(oldSurface);

                DXManager.Draw(DXManager.MapTexture, new SharpDX.Rectangle(0, 0, Settings.ScreenWidth, Settings.ScreenHeight), SharpDX.Vector3.Zero, SharpDX.Color.White);
            //    te = true;
            //}

            //if (ControlTexture == null || ControlTexture.IsDisposed)
            //{
            //    DXManager.ControlList.Add(this);
            //    ControlTexture = new Texture(DXManager.Device, Size.Width, Size.Height, 1, Usage.RenderTarget, Format.A8R8G8B8, Pool.Default);
            //    TextureSize = Size;
            //}

            //oldSurface = DXManager.CurrentSurface;
            //Surface surface = ControlTexture.GetSurfaceLevel(0);
            //DXManager.SetSurface(surface);
            //DXManager.Device.Clear(ClearFlags.Target, SharpDX.Color.Black, 0, 0);//new ColorBGRA(BackColour.ToColor4())




            //DXManager.SetSurface(oldSurface);
            //surface.Dispose();
        }

        public override void Process()
        {
          

        }
    }
}
