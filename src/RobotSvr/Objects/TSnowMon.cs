using System.Collections;
using SystemModule;

namespace RobotSvr;

public class TSnowMon : TActor
{
    protected int ax = 0;
    protected int ay = 0;
    protected bool BoUseDieEffect;
    protected int bx = 0;
    protected int by = 0;
    protected int fire16dir = 0;
    protected int firedir;
    protected bool m_bowChrEffect;

    public TSnowMon(RobotClient robotClient) : base(robotClient)
    {
        m_boUseEffect = false;
        BoUseDieEffect = false;
        m_bowChrEffect = false;
    }

    public override void CalcActorFrame()
    {
        m_nCurrentFrame = -1;
        m_nBodyOffset = Actor.GetOffset(m_wAppearance);
        var pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
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
                    Shift(ClFunc.GetBack(m_btDir), m_nMoveStep, 0, m_nEndFrame - m_nStartFrame + 1);
                break;
            case Grobal2.SM_HIT:
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
                m_boUseEffect = false;
                if (m_btRace == 51) m_boUseEffect = true;
                break;
            case Grobal2.SM_LIGHTING:
                m_nStartFrame = pm.ActCritical.start + m_btDir * (pm.ActCritical.frame + pm.ActCritical.skip);
                m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                m_dwFrameTime = pm.ActCritical.ftime;
                if (m_nMagicNum == 2 && new ArrayList(new[] {38, 39, 46}).Contains(m_btRace))
                {
                    m_nStartFrame = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                    m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                    m_dwFrameTime = pm.ActDeath.ftime;
                }

                m_dwStartTime = MShare.GetTickCount();
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                m_boUseEffect = true;
                m_nEffectFrame = m_nStartFrame;
                m_nEffectStart = m_nStartFrame;
                m_nEffectEnd = m_nEndFrame;
                m_dwEffectStartTime = MShare.GetTickCount();
                m_dwEffectFrameTime = m_dwFrameTime;
                break;
            case Grobal2.SM_SPELL:
                m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                m_dwFrameTime = pm.ActAttack.ftime;
                m_dwStartTime = MShare.GetTickCount();
                m_nCurEffFrame = 0;
                m_boUseMagic = true;
                m_nMagLight = 2;
                m_nSpellFrame = pm.ActCritical.frame;
                m_dwWaitMagicRequest = MShare.GetTickCount();
                m_boWarMode = true;
                m_dwWarModeTime = MShare.GetTickCount();
                Shift(m_btDir, 0, 0, 1);
                break;
            case Grobal2.SM_STRUCK:
                m_nStartFrame = pm.ActStruck.start + m_btDir * (pm.ActStruck.frame + pm.ActStruck.skip);
                m_nEndFrame = m_nStartFrame + pm.ActStruck.frame - 1;
                m_dwFrameTime = m_dwStruckFrameTime;
                // pm.ActStruck.ftime;
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
                // 38, 39,
                if (new ArrayList(new[] {51}).Contains(m_btRace)) BoUseDieEffect = true;
                break;
            case Grobal2.SM_SKELETON:
                m_nStartFrame = pm.ActDeath.start;
                m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                m_dwFrameTime = pm.ActDeath.ftime;
                m_dwStartTime = MShare.GetTickCount();
                if (m_btRace == 39)
                {
                    m_nStartFrame = pm.ActDeath.start + m_btDir * (pm.ActDeath.frame + pm.ActDeath.skip);
                    m_nEndFrame = m_nStartFrame + pm.ActDeath.frame - 1;
                    m_dwFrameTime = pm.ActDeath.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                    m_dwWarModeTime = MShare.GetTickCount();
                    Shift(m_btDir, 0, 0, 1);
                    m_boUseEffect = true;
                    m_nEffectFrame = m_nStartFrame;
                    m_nEffectStart = m_nStartFrame;
                    m_nEffectEnd = m_nEndFrame;
                    m_dwEffectStartTime = MShare.GetTickCount();
                    m_dwEffectFrameTime = m_dwFrameTime;
                }

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
        long dwEffectFrameTimetime;
        base.Run();
        if (m_boUseEffect)
        {
            if (m_boMsgMuch)
                dwEffectFrameTimetime = HUtil32.Round(m_dwEffectFrameTime * 2 / 3);
            else
                dwEffectFrameTimetime = m_dwEffectFrameTime;
            if (MShare.GetTickCount() - m_dwEffectStartTime > dwEffectFrameTimetime)
            {
                m_dwEffectStartTime = MShare.GetTickCount();
                if (m_nEffectFrame < m_nEffectEnd)
                    m_nEffectFrame++;
                else
                    m_boUseEffect = false;
            }
        }
    }
}