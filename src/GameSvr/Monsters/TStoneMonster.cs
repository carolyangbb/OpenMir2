using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TStoneMonster : TMonster
    {
        // 2003/07/15 苞芭厚玫 付拌籍
        //Constructor  Create()
        public TStoneMonster() : base()
        {
            this.ViewRange = 7;
            this.StickMode = true;
        }
        public override void Run()
        {
            int i;
            int ix;
            int iy;
            TCreature cret;
            bool BoRecalc;
            int ixf;
            int ixt;
            int iyf;
            int iyt;
            ArrayList list;
            if ((!this.BoGhost) && (!this.Death))
            {
                // 5檬付促 茄锅究 惯悼
                // NextWalkTime
                if (GetCurrentTime - this.WalkTime > 5000)
                {
                    this.WalkTime = GetCurrentTime;
                    ixf = _MAX(0, this.CX - 3);
                    ixt = _MIN(this.PEnvir.MapWidth - 1, this.CX + 3);
                    iyf = _MAX(0, this.CY - 3);
                    iyt = _MIN(this.PEnvir.MapHeight - 1, this.CY + 3);
                    list = new ArrayList();
                    for (ix = ixf; ix <= ixt; ix++)
                    {
                        for (iy = iyf; iy <= iyt; iy++)
                        {
                            list.Clear();
                            this.PEnvir.GetAllCreature(ix, iy, true, list);
                            for (i = 0; i < list.Count; i++)
                            {
                                cret = (TCreature)list[i];
                                BoRecalc = false;
                                if ((cret != null) && (cret.RaceServer != Grobal2.RC_USERHUMAN) && (cret.Master == null) && (!cret.BoGhost) && (!cret.Death))
                                {
                                    if (this.RaceServer == Grobal2.RC_PBMSTONE1)
                                    {
                                        // 傍拜仿 碍拳 付拌籍
                                        if (cret.ExtraAbil[Grobal2.EABIL_DCUP] == 0)
                                        {
                                            BoRecalc = true;
                                            cret.ExtraAbil[Grobal2.EABIL_DCUP] = 15;
                                            cret.ExtraAbilFlag[Grobal2.EABIL_DCUP] = 0;
                                            cret.ExtraAbilTimes[Grobal2.EABIL_DCUP] = (int)(GetTickCount + 15100);
                                        }
                                    }
                                    else
                                    {
                                        if (cret.StatusArr[Grobal2.STATE_DEFENCEUP] == 0)
                                        {
                                            BoRecalc = true;
                                            cret.StatusArr[Grobal2.STATE_DEFENCEUP] = 8;
                                            cret.StatusTimes[Grobal2.STATE_DEFENCEUP] = GetTickCount;
                                        }
                                        if (cret.StatusArr[Grobal2.STATE_MAGDEFENCEUP] == 0)
                                        {
                                            BoRecalc = true;
                                            cret.StatusArr[Grobal2.STATE_MAGDEFENCEUP] = 8;
                                            cret.StatusTimes[Grobal2.STATE_MAGDEFENCEUP] = GetTickCount;
                                        }
                                    }
                                    if (BoRecalc)
                                    {
                                        cret.RecalcAbilitys();
                                    }
                                }
                                if ((new System.Random(6).Next() == 0) && BoRecalc)
                                {
                                    this.SendRefMsg(Grobal2.RM_HIT, 0, this.CX, this.CY, 0, "");
                                }
                            }
                        }
                    }
                    list.Free();
                    if (new System.Random(2).Next() == 0)
                    {
                        this.SendRefMsg(Grobal2.RM_TURN, 0, this.CX, this.CY, 0, "");
                    }
                }
            }
            base.Run();
        }

    }
}