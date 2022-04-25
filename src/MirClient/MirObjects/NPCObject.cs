using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MirClient.MirObjects
{
    public class NPCObject : MapObject
    {
        public override ObjectType Race => throw new NotImplementedException();

        public override bool Blocking => throw new NotImplementedException();

        public override void Draw()
        {
            throw new NotImplementedException();
        }

        public override void DrawBehindEffects(bool effectsEnabled)
        {
            throw new NotImplementedException();
        }

        public override void DrawEffects(bool effectsEnabled)
        {
            throw new NotImplementedException();
        }

        public override bool MouseOver(Point p)
        {
            throw new NotImplementedException();
        }

        public override void Process()
        {
            throw new NotImplementedException();
        }
    }
}
