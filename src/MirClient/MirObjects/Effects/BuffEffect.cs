using MirClient.MirGraphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MirClient.MirObjects.Effects
{
    public class BuffEffect : Effect
    {
        public BuffType BuffType;

        public BuffEffect(MLibrary library, int baseIndex, int count, int duration, MapObject owner, bool blend, BuffType buffType)
            : base(library, baseIndex, count, duration, owner, 0)
        {
            Repeat = true;
            Blend = blend;
            BuffType = buffType;
            Light = -1;
        }

        public override void Process()
        {
            base.Process();
        }
    }
}
