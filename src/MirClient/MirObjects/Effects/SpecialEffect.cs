using MirClient.MirGraphics;

namespace MirClient.MirObjects.Effects
{
    public class SpecialEffect : Effect
    {
        public uint EffectType = 0;

        public SpecialEffect(MLibrary library, int baseIndex, int count, int duration, MapObject owner, bool blend, bool drawBehind, uint type)
            : base(library, baseIndex, count, duration, owner, 0, drawBehind)
        {
            Blend = blend;
            DrawBehind = drawBehind;
            EffectType = type;
            Light = -1;
        }

        public override void Process()
        {
            base.Process();
        }
    }

}
