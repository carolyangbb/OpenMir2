using System.Collections;
using SystemModule;

namespace RobotSvr;

public class TNpcActor : TActor
{
    private readonly int m_nEffX = 0;
    private readonly int m_nEffY = 0;
    private bool m_boDigUp;
    private long m_dwUseEffectTick;

    public TNpcActor(RobotClient robotClient) : base(robotClient)
    {
        m_boHitEffect = false;
        m_nHitEffectNumber = 0;
        m_boDigUp = false;
    }

    public override void CalcActorFrame()
    {
        m_boUseMagic = false;
        m_boNewMagic = false;
        m_boUseCboLib = false;
        m_nCurrentFrame = -1;
        m_nBodyOffset = Actor.GetNpcOffset(m_wAppearance);
        var pm = Actor.GetRaceByPM(m_btRace, m_wAppearance);
        if (pm == null) return;
        m_btDir = (byte) (m_btDir % 3);
        switch (m_nCurrentAction)
        {
            case Grobal2.SM_TURN:
                switch (m_wAppearance)
                {
                    // Modify the A .. B: 54 .. 58, 112 .. 117
                    case 54:
                    case 112:
                        break;
                    // Modify the A .. B: 59, 70 .. 75, 81 .. 85, 90 .. 92, 94 .. 98, 118 .. 123, 130, 131, 132
                    case 59:
                    case 70:
                    case 81:
                    case 90:
                    case 94:
                    case 118:
                    case 130:
                    case 131:
                    case 132:
                        m_nStartFrame = pm.ActStand.start;
                        m_nEndFrame = m_nStartFrame + pm.ActStand.frame - 1;
                        m_dwFrameTime = pm.ActStand.ftime;
                        m_dwStartTime = MShare.GetTickCount();
                        m_nDefFrameCount = pm.ActStand.frame;
                        Shift(m_btDir, 0, 0, 1);
                        break;
                    default:
                        m_nStartFrame = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip);
                        m_nEndFrame = m_nStartFrame + pm.ActStand.frame - 1;
                        m_dwFrameTime = pm.ActStand.ftime;
                        m_dwStartTime = MShare.GetTickCount();
                        m_nDefFrameCount = pm.ActStand.frame;
                        Shift(m_btDir, 0, 0, 1);
                        break;
                }

                if (!m_boUseEffect)
                {
                    if (new ArrayList(new[] {33, 34}).Contains(m_wAppearance))
                    {
                        m_boUseEffect = true;
                        m_nEffectFrame = m_nEffectStart;
                        m_nEffectEnd = m_nEffectStart + 9;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 300;
                    }
                    else if (new ArrayList(new[] {54, 94}).Contains(m_wAppearance))
                    {
                        // m_nStartFrame := 0;
                        // m_nEndFrame := 0;
                        m_boUseEffect = true;
                        m_nEffectStart = 0;
                        m_nEffectEnd = 8;
                        m_nEffectFrame = 0;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 150;
                    }
                    else if (m_wAppearance >= 42 && m_wAppearance <= 47)
                    {
                        m_nStartFrame = 20;
                        m_nEndFrame = 10;
                        m_boUseEffect = true;
                        m_nEffectStart = 0;
                        m_nEffectFrame = 0;
                        m_nEffectEnd = 19;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 100;
                    }
                    else if (m_wAppearance >= 118 && m_wAppearance <= 120)
                    {
                        m_boUseEffect = true;
                        m_nEffectStart = 10;
                        m_nEffectEnd = 10 + 16 - 1;
                        m_nEffectFrame = 16;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 200;
                    }
                    else if (m_wAppearance >= 122 && m_wAppearance <= 123)
                    {
                        m_boUseEffect = true;
                        m_nEffectStart = 20;
                        m_nEffectEnd = 20 + 9 - 1;
                        m_nEffectFrame = 9;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 200;
                    }
                    else if (m_wAppearance == 131)
                    {
                        m_boUseEffect = true;
                        m_nEffectStart = 10;
                        m_nEffectEnd = 21;
                        m_nEffectFrame = 12;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 100;
                    }
                    else if (m_wAppearance == 132)
                    {
                        m_boUseEffect = true;
                        m_nEffectStart = 20;
                        m_nEffectEnd = 39;
                        m_nEffectFrame = 20;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 100;
                    }
                    else if (m_wAppearance == 51)
                    {
                        m_boUseEffect = true;
                        m_nEffectStart = 60;
                        m_nEffectFrame = m_nEffectStart;
                        m_nEffectEnd = m_nEffectStart + 7;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 150;
                    }
                    else if (m_wAppearance >= 60 && m_wAppearance <= 67)
                    {
                        m_boUseEffect = true;
                        m_nEffectStart = 0;
                        m_nEffectFrame = m_nEffectStart;
                        m_nEffectEnd = m_nEffectStart + 3;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 500;
                    }
                    else if (new ArrayList(new[] {68}).Contains(m_wAppearance))
                    {
                        m_boUseEffect = true;
                        m_nEffectStart = 60;
                        m_nEffectFrame = m_nEffectStart;
                        m_nEffectEnd = m_nEffectStart + 3;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 500;
                    }
                    else if (new ArrayList(new[] {70, 90}).Contains(m_wAppearance))
                    {
                        m_boUseEffect = true;
                        m_nEffectStart = 4;
                        m_nEffectFrame = m_nEffectStart;
                        m_nEffectEnd = m_nEffectStart + 3;
                        m_dwEffectStartTime = MShare.GetTickCount();
                        m_dwEffectFrameTime = 500;
                    }
                }

                break;
            case Grobal2.SM_HIT:
                switch (m_wAppearance)
                {
                    // Modify the A .. B: 54 .. 58, 104 .. 106, 110, 112 .. 117, 121, 132, 133
                    case 54:
                    case 104:
                    case 110:
                    case 112:
                    case 121:
                    case 132:
                    case 133:
                        break;
                    case 33:
                    case 34:
                    case 52:
                        // 0710
                        m_nStartFrame = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip);
                        m_nEndFrame = m_nStartFrame + pm.ActStand.frame - 1;
                        m_dwStartTime = MShare.GetTickCount();
                        m_nDefFrameCount = pm.ActStand.frame;
                        break;
                    // Modify the A .. B: 59, 70 .. 75, 81 .. 85, 90 .. 92, 94 .. 98, 111, 130, 131, 118 .. 120, 122, 123
                    case 59:
                    case 70:
                    case 81:
                    case 90:
                    case 94:
                    case 111:
                    case 130:
                    case 131:
                    case 118:
                    case 122:
                    case 123:
                        m_nStartFrame = pm.ActAttack.start;
                        m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                        m_dwFrameTime = pm.ActAttack.ftime;
                        m_dwStartTime = MShare.GetTickCount();
                        if (m_wAppearance == 84)
                        {
                            m_boDigUp = true;
                            m_boUseEffect = true;
                            m_nEffectStart = 14;
                            m_nEffectFrame = m_nEffectStart;
                            m_nEffectEnd = m_nEffectStart + 7;
                            m_dwEffectStartTime = MShare.GetTickCount();
                            m_dwEffectFrameTime = m_dwFrameTime;
                        }

                        break;
                    default:
                        m_nStartFrame = pm.ActAttack.start + m_btDir * (pm.ActAttack.frame + pm.ActAttack.skip);
                        m_nEndFrame = m_nStartFrame + pm.ActAttack.frame - 1;
                        m_dwFrameTime = pm.ActAttack.ftime;
                        m_dwStartTime = MShare.GetTickCount();
                        if (m_wAppearance == 51)
                        {
                            m_boUseEffect = true;
                            m_nEffectStart = 60;
                            m_nEffectFrame = m_nEffectStart;
                            m_nEffectEnd = m_nEffectStart + 7;
                            m_dwEffectStartTime = MShare.GetTickCount();
                            m_dwEffectFrameTime = 200;
                        }

                        break;
                }

                break;
            case Grobal2.SM_DIGUP:
                if (m_wAppearance == 52)
                {
                    m_boDigUp = true;
                    m_dwUseEffectTick = MShare.GetTickCount() + 23000;
                    m_boUseEffect = true;
                    m_nEffectStart = 60;
                    m_nEffectFrame = m_nEffectStart;
                    m_nEffectEnd = m_nEffectStart + 11;
                    m_dwEffectStartTime = MShare.GetTickCount();
                    m_dwEffectFrameTime = 100;
                }

                if (new ArrayList(new[] {84, 85}).Contains(m_wAppearance))
                {
                    m_nStartFrame = pm.ActCritical.start;
                    m_nEndFrame = m_nStartFrame + pm.ActCritical.frame - 1;
                    m_dwFrameTime = pm.ActCritical.ftime;
                    m_dwStartTime = MShare.GetTickCount();
                }

                if (m_wAppearance == 85)
                {
                    m_boDigUp = true;
                    m_boUseEffect = true;
                    m_nEffectStart = 127;
                    m_nEffectFrame = m_nEffectStart;
                    m_nEffectEnd = m_nEffectStart + 34;
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
        //this.m_btDir = this.m_btDir % 3;
        if (m_nCurrentDefFrame < 0)
            cf = 0;
        else if (m_nCurrentDefFrame >= pm.ActStand.frame)
            cf = 0;
        else
            cf = m_nCurrentDefFrame;
        if (new ArrayList(new[] {54, 94, 70, 81, 90, 112, 130}).Contains(m_wAppearance))
            result = pm.ActStand.start + cf;
        else
            result = pm.ActStand.start + m_btDir * (pm.ActStand.frame + pm.ActStand.skip) + cf;
        return result;
    }

    public override void Run()
    {
        int nEffectFrame;
        long dwEffectFrameTime;
        base.Run();
        nEffectFrame = m_nEffectFrame;
        if (m_boUseEffect)
        {
            if (m_boUseMagic)
                dwEffectFrameTime = HUtil32.Round(m_dwEffectFrameTime / 3);
            else
                dwEffectFrameTime = m_dwEffectFrameTime;
            if (MShare.GetTickCount() - m_dwEffectStartTime > dwEffectFrameTime)
            {
                m_dwEffectStartTime = MShare.GetTickCount();
                if (m_nEffectFrame < m_nEffectEnd)
                {
                    m_nEffectFrame++;
                }
                else
                {
                    if (m_boDigUp)
                    {
                        if (MShare.GetTickCount() > m_dwUseEffectTick)
                        {
                            m_boUseEffect = false;
                            m_boDigUp = false;
                            m_dwUseEffectTick = MShare.GetTickCount();
                        }

                        m_nEffectFrame = m_nEffectStart;
                    }
                    else
                    {
                        m_nEffectFrame = m_nEffectStart;
                    }

                    m_dwEffectStartTime = MShare.GetTickCount();
                }
            }
        }

        if (nEffectFrame != m_nEffectFrame) m_dwLoadSurfaceTime = MShare.GetTickCount();
    }
}