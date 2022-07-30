using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TFireBurnEvent : TEvent
    {
        private long ticktime = 0;

        public TFireBurnEvent(TCreature user, int ax, int ay, int etype, int etime, int dam) : base(user.PEnvir, ax, ay, etype, etime, true)
        {
            this.Damage = dam;
            this.OwnCret = user;
        }

        public override void Run()
        {
            int i;
            TCreature cret;
            ArrayList list;
            if (GetTickCount - ticktime > 3000)
            {
                ticktime = GetTickCount;
                list = new ArrayList();
                if (this.PEnvir != null)
                {
                    this.PEnvir.GetAllCreature(this.X, this.Y, true, list);
                    for (i = 0; i < list.Count; i++)
                    {
                        cret = (TCreature)list[i];
                        if (cret != null)
                        {
                            if (this.OwnCret.IsProperTarget(cret))
                            {
                                cret.SendMsg(this.OwnCret, Grobal2.RM_MAGSTRUCK_MINE, 0, this.Damage, 0, 0, "");
                            }
                        }
                    }
                }
                list.Free();
            }
            base.Run();
        }

    } // end TFireBurnEvent

}

