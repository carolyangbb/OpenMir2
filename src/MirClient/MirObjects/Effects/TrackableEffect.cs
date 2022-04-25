using MirClient.MirScenes;

namespace MirClient.MirObjects.Effects
{
    public class TrackableEffect : Effect
    {
        public static List<TrackableEffect> effectlist = new List<TrackableEffect>();
        public string EffectName = "default";

        public TrackableEffect(Effect baseEffect, string effName = "null")
            : base(baseEffect.Library, baseEffect.BaseIndex, baseEffect.Count, baseEffect.Duration, baseEffect.Owner, baseEffect.Start)
        {
            Repeat = baseEffect.Repeat;
            RepeatUntil = baseEffect.RepeatUntil;
            Blend = baseEffect.Blend;

            EffectName = effName;

            effectlist.Add(this);
        }

        public static int GetOwnerEffectID(uint objectID, string effectName = "null")
        {
            for (int i = 0; i < effectlist.Count; i++)
            {
                if (effectlist[i].Owner.ObjectID != objectID) continue;
                if (effectName != "null" && effectlist[i].EffectName != effectName) continue;
                return i;
            }
            return -1;
        }

        public override void Process()
        {
            base.Process();

            if (Owner == null) Remove();
            else if (Owner.Dead) Remove();
        }

        public override void Remove()
        {
            base.Remove();
            effectlist.Remove(this);
        }

        public void RemoveNoComplete()
        {
            if (Owner != null)
                Owner.Effects.Remove(this);
            else
                MapControl.Effects.Remove(this);
        }
    }
}
