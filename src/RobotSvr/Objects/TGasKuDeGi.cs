using System.Collections;
using SystemModule;

namespace RobotSvr;

public class TGasKuDeGi : TActor
{
    protected int ax = 0;
    protected int ay = 0;
    protected bool BoUseDieEffect;
    protected int bx = 0;
    protected int by = 0;
    protected int fire16dir;
    protected int firedir;

    public TGasKuDeGi(RobotClient robotClient) : base(robotClient)
    {
        m_boUseEffect = false;
        BoUseDieEffect = false;
    }

    public override void CalcActorFrame()
    {
        TMonsterAction pm;
        TActor Actor;
        var scx = 0;
        var scy = 0;
        var stx = 0;
        var sty = 0;
        m_nCurrentFrame = -1;
        //this.m_nBodyOffset = Actor.GetOffset(this.m_wAppearance);
        //pm = Actor.GetRaceByPM(this.m_btRace, this.m_wAppearance);
        if (pm == null) return;
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_TURN:
                m_nStartFrame = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip);
                m_nEndFrame = m_nStartFrame + pm.ActStand.frame - 1;
                m_dwFrameTime = pm.ActStand.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nDefFrameCount = pm.ActStand.frame;
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_WALK:
                m_nStartFrame = pm.ActWalk.start + m_btDir * (pm.ActWalk.frame + pm.ActWalk.skip);
                m_nEndFrame = m_nStartFrame + pm.ActWalk.frame - 1;
                m_dwFrameTime = pm.ActWalk.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nMaxTick = pm.ActWalk.usetick;
                m_nCurTick = 0;
                m_nMoveStep = 1;
                if (m_nCurrentAction == Grobal2.SM_WALK)
                    Shift(m_btDir, m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                else
                    // sm_backstep
                    Shift(ClFunc.GetBack(m_btDir), m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                break;
            case Grobal2.SM_HIT:
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                m_boUseEffect = true;
                firedir = m_btDir;
                m_nEffectFrame = m_nStartFrame;
                m_nEffectStart = m_nStartFrame;
                if (m_btRace == 20)
                    m_nEffectEnd = m_nEndFrame + 1;
                else
                    m_nEffectEnd = m_nEndFrame;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = m_dwFrameTime;
                Actor = robotClient.g_PlayScene.FindActor(m_nTargetRecog);
                if (Actor != null)
                    fire16dir = ClFunc.GetFlyDirection16(scx, scy, stx, sty);
                else
                    fire16dir = firedir * 2;
                break;
            case Grobal2.SM_STRUCK:
                m_nStartFrame = pm.ActStruck.start + m_btDir * (pm.ActStruck.frame + pm.ActStruck.skip);
                m_nEndFrame = m_nStartFrame + pm.ActStruck.frame - 1;
                m_dwFrameTime = m_dwStruckFrameTime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_DEATH:
                m_nStartFrame = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_nStartFrame = m_nEndFrame;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
            case Grobal2.SM_NOWDEATH:
                m_nStartFrame = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip);
                m_nEndFrame = m_nStartFrame + pm.ActDie.frame - 1;
                m_dwFrameTime = pm.ActDie.ftime;
                m_dwStartTime = MShare.GetTickCount();
                if (new ArrayList(new[] {40, 65}).Contains(m_btRace)) BoUseDieEffect = true;
                break;
            case Grobal2.SM_SKELETON:
                m_nStartFrame = pm.ActDeath.start;
                m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                m_dwFrameTime = pm.ActDeath.ftime;
                m_dwStartTime = MShare.GetTickCount();
                break;
        }
    }

    public override int GetDefaultFrame(bool wmode)
    {
        int result;
        int cf;
        TMonsterAction pm;
        result = 0;
        pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return result;
        if (m_boDeath)
        {
            if (m_boSkeleton)
                result = pm.ActDeath.start;
            else
                result = pm.ActDie.start + m_btDir * (pm.ActDie.frame + pm.ActDie.skip) + (pm.ActDie.frame - 1);
        }
        else
        {
            m_nDefFrameCount = pm.ActStand.frame;
            if (m_nCurrentDefFrame < 0)
                cf = 0;
            else if (m_nCurrentDefFrame >= pm.ActStand.frame)
                cf = 0;
            else
                cf = m_nCurrentDefFrame;
            result = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip) + cf;
        }

        return result;
    }

    public override void Run()
    {
        int prv;
        long m_dwEffectFrameTimetime;
        long m_dwFrameTimetime;
        if (m_nCurrentAction == Grobal2.SM_WALK || m_nCurrentAction == Grobal2.SM_BACKSTEP ||
            m_nCurrentAction == Grobal2.SM_RUN || m_nCurrentAction == Grobal2.SM_HORSERUN) return;
        m_boMsgMuch = false;
        if (m_MsgList.Count >= MShare.MSGMUCH) m_boMsgMuch = true;
        RunFrameAction(m_nCurrentFrame - m_nStartFrame);
        if (m_boUseEffect)
        {
            if (m_boMsgMuch)
                m_dwEffectFrameTimetime = HUtil32.Round(m_dwEffectFrameTime * 2 / 3);
            else
                m_dwEffectFrameTimetime = m_dwEffectFrameTime;
            if (MShare.GetTickCount() - m_dwEffectStartTime > m_dwEffectFrameTimetime)
            {
                m_dwEffectStartTime = MShare.GetTickCount();
                if (m_nEffectFrame < m_nEffectEnd)
                    m_nEffectFrame++;
                else
                    m_boUseEffect = false;
            }
        }

        prv = m_nCurrentFrame;
        if (m_nCurrentAction != 0)
        {
            if (m_nCurrentFrame < m_nStartFrame || m_nCurrentFrame > m_nEndFrame) m_nCurrentFrame = m_nStartFrame;
            if (m_boMsgMuch)
                m_dwFrameTimetime = HUtil32.Round(m_dwFrameTime * 2 / 3);
            else
                m_dwFrameTimetime = m_dwFrameTime;
            if (MShare.GetTickCount() - m_dwStartTime > m_dwFrameTimetime)
            {
                if (m_nCurrentFrame < m_nEndFrame)
                {
                    m_nCurrentFrame++;
                    m_dwStartTime = MShare.GetTickCount();
                }
                else
                {
                    m_nCurrentAction = 0;
                    BoUseDieEffect = false;
                }
            }

            m_nCurrentDefFrame = 0;
            m_dwDefFrameTime = MShare.GetTickCount();
        }
        else
        {
            if (MShare.GetTickCount() - m_dwSmoothMoveTime > 200)
            {
                if (MShare.GetTickCount() - m_dwDefFrameTime > 500)
                {
                    m_dwDefFrameTime = MShare.GetTickCount();
                    m_nCurrentDefFrame++;
                    if (m_nCurrentDefFrame >= m_nDefFrameCount) m_nCurrentDefFrame = 0;
                }

                DefaultMotion();
            }
        }

        if (prv != m_nCurrentFrame) m_dwLoadSurfaceTime = MShare.GetTickCount();
    }
}