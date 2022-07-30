using SystemModule;

namespace GameSvr
{
    public class TTrainer : TNormNpc
    {
        private long strucktime = 0;
        private int damagesum = 0;
        private int struckcount = 0;

        public TTrainer() : base()
        {
            strucktime = HUtil32.GetTickCount();
            damagesum = 0;
            struckcount = 0;
        }

        public override void RunMsg(TMessageInfo msg)
        {
            switch (msg.Ident)
            {
                case Grobal2.RM_REFMESSAGE:
                    if ((((int)msg.sender) == Grobal2.RM_STRUCK) && (msg.wParam != 0))
                    {
                        damagesum = damagesum + msg.wParam;
                        strucktime = HUtil32.GetTickCount();
                        struckcount++;
                        this.Say("破坏力为" + msg.wParam.ToString() + "，平均值为" + (damagesum / struckcount).ToString() + "。");
                    }
                    break;
                case Grobal2.RM_MAGSTRUCK:
                    if (msg.lParam1 != 0)
                    {
                        damagesum = damagesum + msg.lParam1;
                        strucktime = HUtil32.GetTickCount();
                        struckcount++;
                        this.Say("总破坏力为" + msg.lParam1.ToString() + "，平均破坏力为" + (damagesum / struckcount).ToString() + "。");
                    }
                    break;
            }
        }

        public override void Run()
        {
            if (struckcount > 0)
            {
                if (HUtil32.GetTickCount() - strucktime > 3 * 1000)
                {
                    this.Say("总破坏力为" + damagesum.ToString() + "，平均破坏力为" + (damagesum / struckcount).ToString() + "。");
                    struckcount = 0;
                    damagesum = 0;
                }
            }
            base.Run();
        }
    }
}

