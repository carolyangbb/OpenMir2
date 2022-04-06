using SystemModule;

namespace RobotSvr
{
    public class TWallStructure : TActor
    {
        private const int V = 0;
        private readonly int ax = 0;
        private readonly int ay = 0;
        private readonly int bx = 0;
        private readonly int by = 0;
        private int deathframe = 0;
        private bool bomarkpos = false;

        public TWallStructure(RobotClient robotClient) : base(robotClient)
        {
            this.m_btDir = 0;
            bomarkpos = false;
        }

        public override void CalcActorFrame()
        {
            TMonsterAction pm;
            this.m_boUseEffect = false;
            this.m_nCurrentFrame = -1;
            this.m_nBodyOffset = Actor.GetOffset(this.m_wAppearance);
            pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if (pm == null)
            {
                return;
            }
            this.m_sUserName = " ";
            deathframe = 0;
            this.m_boUseEffect = false;
            switch (this.m_nCurrentAction)
            {
                case Grobal2.SM_NOWDEATH:
                    this.m_nStartFrame = pm.ActDie.start;
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActDie.frame - 1;
                    this.m_dwFrameTime = pm.ActDie.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    deathframe = pm.ActStand.start + this.m_btDir;
                    this.Shift(this.m_btDir, 0, 0, 1);
                    this.m_boUseEffect = true;
                    break;
                case Grobal2.SM_DEATH:
                    this.m_nStartFrame = pm.ActDie.start + pm.ActDie.frame - 1;
                    this.m_nEndFrame = this.m_nStartFrame;
                    this.m_nDefFrameCount = 0;
                    break;
                case Grobal2.SM_DIGUP:
                    this.m_nStartFrame = pm.ActDie.start;
                    this.m_nEndFrame = this.m_nStartFrame + pm.ActDie.frame - 1;
                    this.m_dwFrameTime = pm.ActDie.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    deathframe = pm.ActStand.start + this.m_btDir;
                    this.m_boUseEffect = true;
                    break;
                default:
                    this.m_nStartFrame = pm.ActStand.start + this.m_btDir;
                    this.m_nEndFrame = this.m_nStartFrame;
                    this.m_dwFrameTime = pm.ActStand.ftime;
                    this.m_dwStartTime = MShare.GetTickCount();
                    this.m_nDefFrameCount = 0;
                    this.Shift(this.m_btDir, 0, 0, 1);
                    this.m_boHoldPlace = true;
                    break;
            }
        }

        public override int GetDefaultFrame(bool wmode)
        {
            int result = V;
            this.m_nBodyOffset = Actor.GetOffset(this.m_wAppearance);
            TMonsterAction pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
            if (pm == null)
            {
                return result;
            }
            result = pm.ActStand.start + this.m_btDir;
            return result;
        }

        public override void Run()
        {
            if (this.m_boDeath)
            {
                if (bomarkpos)
                {
                    ClMain.Map.MarkCanWalk(this.m_nCurrX, this.m_nCurrY, true);
                    bomarkpos = false;
                }
            }
            else
            {
                if (!bomarkpos)
                {
                    ClMain.Map.MarkCanWalk(this.m_nCurrX, this.m_nCurrY, false);
                    bomarkpos = true;
                }
            }
            ClMain.g_PlayScene.SetActorDrawLevel(this, 0);
            base.Run();
        }
    }
}

