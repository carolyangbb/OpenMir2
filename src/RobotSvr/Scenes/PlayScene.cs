using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using SystemModule;

namespace RobotSvr
{
    public class PlayScene : Scene
    {
        public bool MBoPlayChange = false;
        public long MDwPlayChangeTick = 0;
        private readonly long _mDwMoveTime = 0;
        private readonly long _mDwAniTime = 0;
        private readonly int _mNAniCount = 0;
        private readonly int _mNDefXx = 0;
        private readonly int _mNDefYy = 0;
        public IList<TActor> m_ActorList = null;
        public ArrayList MEffectList = null;
        public ArrayList MFlyList = null;
        public long MDwBlinkTime = 0;
        public bool MBoViewBlink = false;
        public ProcMagic ProcMagic = null;

        public PlayScene() : base(SceneType.stPlayGame)
        {
            ProcMagic.NTargetX = -1;
            m_ActorList = new List<TActor>();
            MEffectList = new ArrayList();
            MFlyList = new ArrayList();
            MDwBlinkTime = MShare.GetTickCount();
            MBoViewBlink = false;
            _mDwMoveTime = MShare.GetTickCount();
            _mDwAniTime = MShare.GetTickCount();
            _mNAniCount = 0;
        }

        public bool CanDrawTileMap()
        {
            return true;
        }

        public override void OpenScene()
        {

        }

        public override void CloseScene()
        {

        }

        public override void OpeningScene()
        {
        }

        public void CleanObjects()
        {
            //int i;
            //for (i = MActorList.Count - 1; i >= 0; i--)
            //{
            //    // (TActor(m_ActorList[i]) <> g_MySelf.m_SlaveObject)
            //    if ((((TActor)(MActorList[i])) != MShare.g_MySelf) && (((TActor)(MActorList[i])) != MShare.g_MySelf.m_HeroObject) && !PlayScn.IsMySlaveObject(((TActor)(MActorList[i]))))
            //    {
            //        // BLUE
            //        ((TActor)(MActorList[i])).Free;
            //        MActorList.RemoveAt(i);
            //    }
            //}
            //MShare.g_TargetCret = null;
            //MShare.g_FocusCret = null;
            //MShare.g_MagicTarget = null;
            //for (i = 0; i < MEffectList.Count; i++)
            //{
            //    ((TMagicEff)(MEffectList[i])).Free;
            //}
            //MEffectList.Clear();
        }

        public void DrawTileMap()
        {

        }

        private void ClearDropItemA()
        {
            int i;
            TDropItem dropItem;
            for (i = MShare.g_DropedItemList.Count - 1; i >= 0; i--)
            {
                dropItem = MShare.g_DropedItemList[i];
                if (dropItem == null)
                {
                    MShare.g_DropedItemList.RemoveAt(i);
                    // Continue;
                    break;
                }
                if ((Math.Abs(dropItem.X - MShare.g_MySelf.m_nCurrX) > 20) || (Math.Abs(dropItem.Y - MShare.g_MySelf.m_nCurrY) > 20))
                {
                    Dispose(dropItem);
                    MShare.g_DropedItemList.RemoveAt(i);
                    break;
                }
            }
        }

        public void BeginScene()
        {
            //ClMain.Map.UpdateMapPos(MShare.g_MySelf.m_nRx, MShare.g_MySelf.m_nRy);
        }

        public void PlaySurface(Object sender)
        {

        }

        public void MagicSurface(Object sender)
        {

        }

        public void NewMagic(TActor aowner, int magid, int magnumb, int cx, int cy, int tx, int ty, int targetCode, MagicType mtype, bool recusion, int anitime, ref bool boFly, int maglv, int poison)
        {

        }

        public void DelMagic(int magid)
        {
            //for (var i = 0; i < MEffectList.Count; i++)
            //{
            //    if (((TMagicEff)MEffectList[i]).ServerMagicId == magid)
            //    {
            //        ((TMagicEff)MEffectList[i]).Free;
            //        MEffectList.RemoveAt(i);
            //        break;
            //    }
            //}
        }

        public TActor GetCharacter(int x, int y, int wantsel, ref int nowsel, bool liveonly)
        {
            TActor result;
            int k;
            int i;
            int ccy;
            int dx;
            int dy;
            TActor a;
            result = null;
            nowsel = -1;
            for (k = ccy + 8; k >= ccy - 1; k--)
            {
                for (i = m_ActorList.Count - 1; i >= 0; i--)
                {
                    if (m_ActorList[i] != MShare.g_MySelf)
                    {
                        a = m_ActorList[i];
                        if ((!liveonly || !a.m_boDeath) && a.m_boHoldPlace && a.m_boVisible)
                        {
                            if (a.m_nCurrY == k)
                            {
                                dx = (a.m_nRx - ClMain.Map.m_ClientRect.Left) * Grobal2.UNITX + _mNDefXx + a.m_nPx + a.m_nShiftX;
                                dy = (a.m_nRy - ClMain.Map.m_ClientRect.Top - 1) * Grobal2.UNITY + _mNDefYy + a.m_nPy + a.m_nShiftY;
                                if (a.CheckSelect(x - dx, y - dy))
                                {
                                    result = a;
                                    nowsel++;
                                    if (nowsel >= wantsel)
                                    {
                                        return result;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        // 取得鼠标所指坐标的角色
        public TActor GetAttackFocusCharacter(int x, int y, int wantsel, ref int nowsel, bool liveonly)
        {
            TActor result;
            int k;
            int i;
            int ccy = 0;
            int dx = 0;
            int dy = 0;
            int centx = 0;
            int centy = 0;
            TActor a;
            result = GetCharacter(x, y, wantsel, ref nowsel, liveonly);
            if (result == null)
            {
                nowsel = -1;
                for (k = ccy + 8; k >= ccy - 1; k--)
                {
                    for (i = m_ActorList.Count - 1; i >= 0; i--)
                    {
                        if (m_ActorList[i] != MShare.g_MySelf)
                        {
                            a = m_ActorList[i];
                            if ((!liveonly || !a.m_boDeath) && a.m_boHoldPlace && a.m_boVisible)
                            {
                                if (a.m_nCurrY == k)
                                {
                                    dx = (a.m_nRx - ClMain.Map.m_ClientRect.Left) * Grobal2.UNITX + _mNDefXx + a.m_nPx + a.m_nShiftX;
                                    dy = (a.m_nRy - ClMain.Map.m_ClientRect.Top - 1) * Grobal2.UNITY + _mNDefYy + a.m_nPy + a.m_nShiftY;
                                    if (a.CharWidth() > 40)
                                    {
                                        centx = (a.CharWidth() - 40) / 2;
                                    }
                                    else
                                    {
                                        centx = 0;
                                    }
                                    if (a.CharHeight() > 70)
                                    {
                                        centy = (a.CharHeight() - 70) / 2;
                                    }
                                    else
                                    {
                                        centy = 0;
                                    }
                                    if ((x - dx >= centx) && (x - dx <= a.CharWidth() - centx) && (y - dy >= centy) && (y - dy <= a.CharHeight() - centy))
                                    {
                                        result = a;
                                        nowsel++;
                                        if (nowsel >= wantsel)
                                        {
                                            return result;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public bool IsSelectMyself(int x, int y)
        {
            int k;
            int ccy = 0;
            int dx = 0;
            int dy = 0;
            bool result = false;
            for (k = ccy + 2; k >= ccy - 1; k--)
            {
                if (MShare.g_MySelf.m_nCurrY == k)
                {
                    dx = (MShare.g_MySelf.m_nRx - ClMain.Map.m_ClientRect.Left) * Grobal2.UNITX + _mNDefXx + MShare.g_MySelf.m_nPx + MShare.g_MySelf.m_nShiftX;
                    dy = (MShare.g_MySelf.m_nRy - ClMain.Map.m_ClientRect.Top - 1) * Grobal2.UNITY + _mNDefYy + MShare.g_MySelf.m_nPy + MShare.g_MySelf.m_nShiftY;
                    if (MShare.g_MySelf.CheckSelect(x - dx, y - dy))
                    {
                        result = true;
                        return result;
                    }
                }
            }
            return result;
        }

        public TDropItem GetDropItems(int x, int y, ref string inames)
        {
            int i;
            int ccx = 0;
            int ccy = 0;
            TDropItem dropItem;
            TDropItem result = null;
            inames = "";
            for (i = 0; i < MShare.g_DropedItemList.Count; i++)
            {
                dropItem = MShare.g_DropedItemList[i];
                if ((dropItem.X == ccx) && (dropItem.Y == ccy))
                {
                    if (result == null)
                    {
                        result = dropItem;
                    }
                    inames = inames + dropItem.Name + "\\";
                }
            }
            return result;
        }

        public void GetXyDropItemsList(int nX, int nY, ref ArrayList itemList)
        {
            int i;
            TDropItem dropItem;
            for (i = 0; i < MShare.g_DropedItemList.Count; i++)
            {
                dropItem = MShare.g_DropedItemList[i];
                if ((dropItem.X == nX) && (dropItem.Y == nY))
                {
                    itemList.Add(dropItem);
                }
            }
        }

        public TDropItem GetXyDropItems(int nX, int nY)
        {
            TDropItem result;
            int i;
            TDropItem dropItem;
            result = null;
            for (i = 0; i < MShare.g_DropedItemList.Count; i++)
            {
                dropItem = MShare.g_DropedItemList[i];
                if ((dropItem.X == nX) && (dropItem.Y == nY))
                {
                    result = dropItem;
                    // if not g_gcGeneral[7] or DropItem.boShowName then
                    // Break;
                    // not g_gcGeneral[7] or
                    if (MShare.g_boPickUpAll || dropItem.boPickUp)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public bool CanRun(int sX, int sY, int ex, int ey)
        {
            bool result;
            int ndir;
            int rx;
            int ry;
            ndir = ClFunc.GetNextDirection(sX, sY, ex, ey);
            rx = sX;
            ry = sY;
            ClFunc.GetNextPosXY(ndir, ref rx, ref ry);
            if (CanWalkEx(rx, ry) && CanWalkEx(ex, ey))
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public bool CanWalkEx(int mx, int my)
        {
            bool result;
            result = false;
            if (ClMain.Map.CanMove(mx, my))
            {
                result = !CrashManEx(mx, my);
            }
            return result;
        }

        private bool CrashManEx(int mx, int my)
        {
            bool result;
            int i;
            TActor actor;
            result = false;
            for (i = 0; i < m_ActorList.Count; i++)
            {
                actor = m_ActorList[i];
                if (actor == MShare.g_MySelf)
                {
                    continue;
                }
                if (actor.m_boVisible && actor.m_boHoldPlace && (!actor.m_boDeath) && (actor.m_nCurrX == mx) && (actor.m_nCurrY == my))
                {
                    if ((MShare.g_MySelf.m_nTagX == 0) && (MShare.g_MySelf.m_nTagY == 0))
                    {
                        if ((actor.m_btRace == Grobal2.RCC_USERHUMAN) && (MShare.g_boCanRunHuman || MShare.g_boCanRunSafeZone))
                        {
                            continue;
                        }
                        if ((actor.m_btRace == Grobal2.RCC_MERCHANT) && MShare.g_boCanRunNpc)
                        {
                            continue;
                        }
                        if ((actor.m_btRace > Grobal2.RCC_USERHUMAN) && (actor.m_btRace != Grobal2.RCC_MERCHANT) && (MShare.g_boCanRunMon || MShare.g_boCanRunSafeZone))
                        {
                            continue;
                        }
                    }
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool CanWalk(int mx, int my)
        {
            bool result;
            result = false;
            if (ClMain.Map.CanMove(mx, my))
            {
                result = !CrashMan(mx, my);
            }
            return result;
        }

        public bool CrashMan(int mx, int my)
        {
            bool result;
            int i;
            TActor a;
            result = false;
            for (i = 0; i < m_ActorList.Count; i++)
            {
                a = m_ActorList[i];
                if (a == MShare.g_MySelf)
                {
                    continue;
                }
                if (a.m_boVisible && a.m_boHoldPlace && (!a.m_boDeath) && (a.m_nCurrX == mx) && (a.m_nCurrY == my))
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        // function TPlayScene.CrashManPath(mx, my: Integer): Boolean;
        // var
        // i                         : Integer;
        // a                         : TActor;
        // begin
        // Result := False;
        // for i := 0 to m_ActorList.count - 1 do begin
        // a := TActor(m_ActorList[i]);
        // if a = g_MySelf then Continue;
        // if (a.m_boVisible) and (a.m_boHoldPlace) and (not a.m_boDeath) and (a.m_nCurrX = mx) and (a.m_nCurrY = my) then begin
        // Result := True;
        // Break;
        // end;
        // end;
        // end;
        public bool CanFly(int mx, int my)
        {
            bool result;
            result = ClMain.Map.CanFly(mx, my);
            return result;
        }

        // ------------------------ Actor ------------------------
        public TActor FindActor(int id)
        {
            TActor result;
            int i;
            result = null;
            if (id == 0)
            {
                return result;
            }
            for (i = 0; i < m_ActorList.Count; i++)
            {
                if (m_ActorList[i].m_nRecogId == id)
                {
                    result = m_ActorList[i];
                    break;
                }
            }
            return result;
        }

        public TActor FindActor(string sName)
        {
            TActor result;
            int i;
            TActor actor;
            result = null;
            for (i = 0; i < m_ActorList.Count; i++)
            {
                actor = m_ActorList[i];
                if (actor.m_sUserName.ToLower().CompareTo(sName.ToLower()) == 0)
                {
                    result = actor;
                    break;
                }
            }
            return result;
        }

        public TActor FindActorXY(int x, int y)
        {
            TActor result;
            int i;
            TActor a;
            result = null;
            for (i = 0; i < m_ActorList.Count; i++)
            {
                a = m_ActorList[i];
                if ((a.m_nCurrX == x) && (a.m_nCurrY == y))
                {
                    result = a;
                    if (!result.m_boDeath && result.m_boVisible && result.m_boHoldPlace)
                    {
                        break;
                    }
                }
            }
            return result;
        }

        public bool IsValidActor(TActor actor)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < m_ActorList.Count; i++)
            {
                if (m_ActorList[i] == actor)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public TActor NewActor(int chrid, int cx, int cy, short cdir, int cfeature, int cstate)
        {
            TActor actor;
            TActor result = null;
            for (var i = 0; i < m_ActorList.Count; i++)
            {
                if (m_ActorList[i].m_nRecogId == chrid)
                {
                    result = m_ActorList[i];
                    return result;
                }
            }
            if (ClFunc.IsChangingFace(chrid))
            {
                return result;
            }
            switch (Grobal2.RACEfeature(cfeature))
            {
                case 0:
                    actor = new THumActor();
                    break;
                case 9:
                    // 人物
                    actor = new TSoccerBall();
                    break;
                case 13:
                    // 足球
                    actor = new TKillingHerb();
                    break;
                case 14:
                    // 食人花
                    actor = new TSkeletonOma();
                    break;
                case 15:
                    // 骷髅
                    actor = new TDualAxeOma();
                    break;
                case 16:
                    // 掷斧骷髅
                    actor = new TGasKuDeGi();
                    break;
                case 17:
                    // 洞蛆
                    actor = new TCatMon();
                    break;
                case 18:
                    // 钩爪猫
                    actor = new THuSuABi();
                    break;
                case 19:
                    // 稻草人
                    actor = new TCatMon();
                    break;
                case 20:
                    // 沃玛战士
                    actor = new TFireCowFaceMon();
                    break;
                case 21:
                    // 火焰沃玛
                    actor = new TCowFaceKing();
                    break;
                case 22:
                    // 沃玛教主
                    actor = new TDualAxeOma();
                    break;
                case 23:
                    // 黑暗战士
                    actor = new TWhiteSkeleton();
                    break;
                case 24:
                    // 变异骷髅
                    actor = new TSuperiorGuard();
                    break;
                case 25:
                    // 带刀卫士
                    actor = new TKingOfSculpureKingMon();
                    break;
                case 26:
                    actor = new TKingOfSculpureKingMon();
                    break;
                case 27:
                    actor = new TSnowMon();
                    break;
                case 28:
                    actor = new TSnowMon();
                    break;
                case 29:
                    actor = new TSnowMon();
                    break;
                case 30:
                    actor = new TCatMon();
                    break;
                case 31:
                    // 朝俺窿
                    actor = new TCatMon();
                    break;
                case 32:
                    // 角蝇
                    actor = new TScorpionMon();
                    break;
                case 33:
                    // 蝎子
                    actor = new TCentipedeKingMon();
                    break;
                case 34:
                    // 触龙神
                    actor = new TBigHeartMon();
                    break;
                case 35:
                    // 赤月恶魔
                    actor = new TSpiderHouseMon();
                    break;
                case 36:
                    // 幻影蜘蛛
                    actor = new TExplosionSpider();
                    break;
                case 37:
                    // 月魔蜘蛛
                    actor = new TFlyingSpider();
                    break;
                case 38:
                    actor = new TSnowMon();
                    break;
                case 39:
                    actor = new TSnowMon();
                    break;
                case 40:
                    actor = new TZombiLighting();
                    break;
                case 41:
                    // 僵尸1
                    actor = new TZombiDigOut();
                    break;
                case 42:
                    // 僵尸2
                    actor = new TZombiZilkin();
                    break;
                case 43:
                    // 僵尸3
                    actor = new TBeeQueen();
                    break;
                case 44:
                    // 角蝇巢
                    actor = new TSnowMon();
                    break;
                case 45:
                    actor = new TArcherMon();
                    break;
                case 46:
                    // 弓箭手
                    actor = new TSnowMon();
                    break;
                case 47:
                    actor = new TSculptureMon();
                    break;
                case 48:
                    // 祖玛雕像
                    actor = new TSculptureMon();
                    break;
                case 49:
                    actor = new TSculptureKingMon();
                    break;
                case 50:
                    // 祖玛教主
                    actor = new TNpcActor();
                    break;
                case 51:
                    actor = new TSnowMon();
                    break;
                case 52:
                    actor = new TGasKuDeGi();
                    break;
                case 53:
                    // 楔蛾
                    actor = new TGasKuDeGi();
                    break;
                case 54:
                    // 粪虫
                    actor = new TSmallElfMonster();
                    break;
                case 55:
                    // 神兽
                    actor = new TWarriorElfMonster();
                    break;
                case 56:
                    // 神兽1
                    actor = new TAngel();
                    break;
                case 57:
                    actor = new TDualAxeOma();
                    break;
                case 58:
                    // 1234
                    actor = new TDualAxeOma();
                    break;
                case 60:
                    // 1234
                    actor = new TElectronicScolpionMon();
                    break;
                case 61:
                    actor = new TBossPigMon();
                    break;
                case 62:
                    actor = new TKingOfSculpureKingMon();
                    break;
                case 63:
                    actor = new TSkeletonKingMon();
                    break;
                case 64:
                    actor = new TGasKuDeGi();
                    break;
                case 65:
                    actor = new TSamuraiMon();
                    break;
                case 66:
                    actor = new TSkeletonSoldierMon();
                    break;
                case 67:
                    actor = new TSkeletonSoldierMon();
                    break;
                case 68:
                    actor = new TSkeletonSoldierMon();
                    break;
                case 69:
                    actor = new TSkeletonArcherMon();
                    break;
                case 70:
                    actor = new TBanyaGuardMon();
                    break;
                case 71:
                    actor = new TBanyaGuardMon();
                    break;
                case 72:
                    actor = new TBanyaGuardMon();
                    break;
                case 73:
                    actor = new TPBOMA1Mon();
                    break;
                case 74:
                    actor = new TCatMon();
                    break;
                case 75:
                    actor = new TStoneMonster();
                    break;
                case 76:
                    actor = new TSuperiorGuard();
                    break;
                case 77:
                    actor = new TStoneMonster();
                    break;
                case 78:
                    actor = new TBanyaGuardMon();
                    break;
                case 79:
                    actor = new TPBOMA6Mon();
                    break;
                case 80:
                    actor = new TMineMon();
                    break;
                case 81:
                    actor = new TAngel();
                    break;
                case 83:
                    actor = new TFireDragon();
                    break;
                case 84:
                    actor = new TDragonStatue();
                    break;
                case 87:
                    actor = new TDragonStatue();
                    break;
                case 90:
                    actor = new TDragonBody();
                    break;
                case 91:
                    // 龙
                    actor = new TWhiteSkeleton();
                    break;
                case 92:
                    // 变异骷髅
                    actor = new TWhiteSkeleton();
                    break;
                case 93:
                    // 变异骷髅
                    actor = new TWhiteSkeleton();
                    break;
                case 94:
                    // 变异骷髅
                    actor = new TWarriorElfMonster();
                    break;
                case 95:
                    // 神兽1
                    actor = new TWarriorElfMonster();
                    break;
                case 98:
                    actor = new TWallStructure();
                    break;
                case 99:
                    actor = new TCastleDoor();
                    break;
                case 101:
                    actor = new TBanyaGuardMon();
                    break;
                case 102:
                    actor = new TKhazardMon();
                    break;
                case 103:
                    actor = new TFrostTiger();
                    break;
                case 104:
                    actor = new TRedThunderZuma();
                    break;
                case 105:
                    actor = new TCrystalSpider();
                    break;
                case 106:
                    actor = new TYimoogi();
                    break;
                case 109:
                    actor = new TBlackFox();
                    break;
                case 110:
                    actor = new TGreenCrystalSpider();
                    break;
                case 111:
                    actor = new TBanyaGuardMon();
                    break;
                case 113:
                    actor = new TBanyaGuardMon();
                    break;
                case 114:
                    actor = new TBanyaGuardMon();
                    break;
                case 115:
                    actor = new TBanyaGuardMon();
                    break;
                case 117:
                case 118:
                case 119:
                    actor = new TBanyaGuardMon();
                    break;
                case 120:
                    actor = new TFireDragon();
                    break;
                case 121:
                    actor = new TTiger();
                    break;
                case 122:
                    actor = new TDragon();
                    break;
                case 123:
                    actor = new TGhostShipMonster();
                    break;
                default:
                    actor = new TActor();
                    break;
            }
            actor.m_nRecogId = chrid;
            actor.m_nCurrX = cx;
            actor.m_nCurrY = cy;
            actor.m_nRx = actor.m_nCurrX;
            actor.m_nRy = actor.m_nCurrY;
            actor.m_btDir = cdir;
            actor.m_nFeature = cfeature;
            if (MShare.g_boOpenAutoPlay && MShare.g_gcAss[6])
            {
                actor.m_btAFilter = MShare.g_APMobList.IndexOf(actor.m_sUserName) >= 0;
            }
            actor.m_btRace = Grobal2.RACEfeature(cfeature);
            actor.m_btHair = Grobal2.HAIRfeature(cfeature);
            actor.m_btDress = Grobal2.DRESSfeature(cfeature);
            actor.m_btWeapon = Grobal2.WEAPONfeature(cfeature);
            actor.m_wAppearance = Grobal2.APPRfeature(cfeature);
            // if (m_btRace = 50) and (m_wAppearance in [54..48]) then
            // m_boVisible := False;
            actor.m_Action = null;
            if (actor.m_btRace == 0)
            {
                actor.m_btSex = actor.m_btDress % 2;
                if (actor.m_btDress >= 24 && actor.m_btDress <= 27)
                {
                    actor.m_btDress = 18 + actor.m_btSex;
                }
            }
            else
            {
                actor.m_btSex = 0;
            }
            actor.m_nState = cstate;
            actor.m_SayingArr[0] = "";
            m_ActorList.Add(actor);
            result = actor;
            return result;
        }

        public void ActorDied(TActor actor)
        {
            int i;
            bool flag;
            for (i = 0; i < m_ActorList.Count; i++)
            {
                if (m_ActorList[i] == actor)
                {
                    m_ActorList.RemoveAt(i);
                    break;
                }
            }
            flag = false;
            for (i = 0; i < m_ActorList.Count; i++)
            {
                if (!m_ActorList[i].m_boDeath)
                {
                    m_ActorList.Insert(i, actor);
                    flag = true;
                    break;
                }
            }
            if (!flag)
            {
                m_ActorList.Add(actor);
            }
        }

        public void SetActorDrawLevel(TActor actor, int level)
        {
            int i;
            if (level == 0)
            {
                for (i = 0; i < m_ActorList.Count; i++)
                {
                    if (m_ActorList[i] == actor)
                    {
                        m_ActorList.RemoveAt(i);
                        m_ActorList.Insert(0, actor);
                        break;
                    }
                }
            }
        }

        // 清除所有角色
        public void ClearActors()
        {
            m_ActorList.Clear();
            MShare.g_MySelf = null;
            MShare.g_TargetCret = null;
            MShare.g_FocusCret = null;
            MShare.g_MagicTarget = null;
        }

        public TActor DeleteActor(int id, bool boDeath)
        {
            TActor result;
            int i;
            result = null;
            i = 0;
            while (true)
            {
                if (i >= m_ActorList.Count)
                {
                    break;
                }
                if (m_ActorList[i].m_nRecogId == id)
                {
                    if (MShare.g_TargetCret == m_ActorList[i])
                    {
                        MShare.g_TargetCret = null;
                    }
                    if (MShare.g_FocusCret == m_ActorList[i])
                    {
                        MShare.g_FocusCret = null;
                    }
                    if (MShare.g_MagicTarget == m_ActorList[i])
                    {
                        MShare.g_MagicTarget = null;
                    }
                    if (m_ActorList[i] == MShare.g_MySelf.m_HeroObject)
                    {
                        if (!boDeath)
                        {
                            break;
                        }
                    }
                    if (PlayScn.IsMySlaveObject(m_ActorList[i]))
                    {
                        if (!boDeath)
                        {
                            break;
                        }
                    }
                    m_ActorList[i].m_dwDeleteTime = MShare.GetTickCount();
                    MShare.g_FreeActorList.Add(m_ActorList[i]);
                    m_ActorList.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
            return result;
        }

        public TActor DeleteActor(int id)
        {
            return DeleteActor(id, false);
        }

        public void DelActor(Object actor)
        {
            int i;
            for (i = 0; i < m_ActorList.Count; i++)
            {
                if (m_ActorList[i] == actor)
                {
                    m_ActorList[i].m_dwDeleteTime = MShare.GetTickCount();
                    MShare.g_FreeActorList.Add(m_ActorList[i]);
                    m_ActorList.RemoveAt(i);
                    break;
                }
            }
        }

        public TActor ButchAnimal(int x, int y)
        {
            TActor result;
            int i;
            TActor a;
            result = null;
            for (i = 0; i < m_ActorList.Count; i++)
            {
                a = m_ActorList[i];
                // and (a.m_btRace <> 0)
                if (a.m_boDeath)
                {
                    if ((Math.Abs(a.m_nCurrX - x) <= 1) && (Math.Abs(a.m_nCurrY - y) <= 1))
                    {
                        result = a;
                        break;
                    }
                }
            }
            return result;
        }

        public void SendMsg(int ident, int chrid, int x, int y, int cdir, int feature, int state, string str, int ipInfo = 0)
        {
            TActor actor;
            TMessageBodyW mbw;
            switch (ident)
            {
                case Grobal2.SM_CHANGEMAP:
                case Grobal2.SM_NEWMAP:
                    ProcMagic.NTargetX = -1;
                    ClMain.EventMan.ClearEvents();
                    ClMain.g_PathBusy = true;
                    try
                    {
                        if (ClMain.frmMain.TimerAutoMove.Enabled)
                        {
                            ClMain.frmMain.TimerAutoMove.Enabled = false;
                            MapUnit.Units.TPathMap.g_MapPath = new Point[0];
                            MapUnit.Units.TPathMap.g_MapPath = null;
                            ClMain.DScreen.AddChatBoardString("地图跳转，停止自动移动", ClMain.GetRGB(5), System.Drawing.Color.White);
                        }
                        if (MShare.g_boOpenAutoPlay && ClMain.frmMain.TimerAutoPlay.Enabled)
                        {
                            ClMain.frmMain.TimerAutoPlay.Enabled = false;
                            MShare.g_gcAss[0] = false;
                            MShare.g_APMapPath = new Point[0];
                            MShare.g_APMapPath2 = new Point[0];
                            MShare.g_APStep = -1;
                            MShare.g_APLastPoint.X = -1;
                            ClMain.DScreen.AddChatBoardString("[挂机] 地图跳转，停止自动挂机", System.Drawing.Color.Red, System.Drawing.Color.White);
                        }
                        if (MShare.g_MySelf != null)
                        {
                            MShare.g_MySelf.m_nTagX = 0;
                            MShare.g_MySelf.m_nTagY = 0;
                        }
                        if (ClMain.Map.m_MapBuf != null)
                        {
                            FreeMem(ClMain.Map.m_MapBuf);
                            ClMain.Map.m_MapBuf = null;
                        }
                        if (ClMain.Map.m_MapData.Length > 0)
                        {
                            ClMain.Map.m_MapData = new TCellParams[0];
                            ClMain.Map.m_MapData = null;
                        }
                    }
                    finally
                    {
                        ClMain.g_PathBusy = false;
                    }
                    ClMain.Map.LoadMap(str, x, y);
                    if ((ident == Grobal2.SM_NEWMAP) && (MShare.g_MySelf != null))
                    {
                        MShare.g_MySelf.m_nCurrX = x;
                        MShare.g_MySelf.m_nCurrY = y;
                        MShare.g_MySelf.m_nRx = x;
                        MShare.g_MySelf.m_nRy = y;
                        DelActor(MShare.g_MySelf);
                    }
                    break;
                case Grobal2.SM_LOGON:
                    actor = FindActor(chrid);
                    if (actor == null)
                    {
                        actor = NewActor(chrid, x, y, HUtil32.LoByte(cdir), feature, state);
                        actor.m_nChrLight = HUtil32.HiByte(cdir);
                        cdir = HUtil32.LoByte(cdir);
                        actor.SendMsg(Grobal2.SM_TURN, x, y, cdir, feature, state, "", 0);
                    }
                    if (MShare.g_MySelf != null)
                    {
                        if (MShare.g_MySelf.m_HeroObject != null)
                        {
                            MShare.g_MySelf.m_HeroObject = null;
                        }
                        MShare.g_MySelf.m_SlaveObject.Clear();
                        MShare.g_MySelf = null;
                    }
                    MShare.g_MySelf = (THumActor)actor;
                    break;
                case Grobal2.SM_HIDE:
                    actor = FindActor(chrid);
                    if (actor != null)
                    {
                        if (actor.m_boDelActionAfterFinished)
                        {
                            return;
                        }
                        if (actor.m_nWaitForRecogId != 0)
                        {
                            return;
                        }
                        if (actor == MShare.g_MySelf.m_HeroObject)
                        {
                            if (!actor.m_boDeath)
                            {
                                return;
                            }
                            DeleteActor(chrid, true);
                            return;
                        }
                        if (PlayScn.IsMySlaveObject(actor))
                        {
                            if ((cdir != 0) || actor.m_boDeath)
                            {
                                DeleteActor(chrid, true);
                            }
                            return;
                        }
                    }
                    DeleteActor(chrid);
                    break;
                default:
                    actor = FindActor(chrid);
                    if ((ident == Grobal2.SM_TURN) || (ident == Grobal2.SM_RUN) || (ident == Grobal2.SM_HORSERUN) || (ident == Grobal2.SM_WALK) || (ident == Grobal2.SM_BACKSTEP) || (ident == Grobal2.SM_DEATH) || (ident == Grobal2.SM_SKELETON) || (ident == Grobal2.SM_DIGUP) || (ident == Grobal2.SM_ALIVE))
                    {
                        if (actor == null)
                        {
                            actor = NewActor(chrid, x, y, HUtil32.LoByte(cdir), feature, state);
                        }
                        if (actor != null)
                        {
                            if (ipInfo != 0)
                            {
                                actor.m_nIPowerLvl = HUtil32.HiWord(ipInfo);
                            }
                            actor.m_nChrLight = HUtil32.HiByte(cdir);
                            cdir = HUtil32.LoByte(cdir);
                            if (ident == Grobal2.SM_SKELETON)
                            {
                                actor.m_boDeath = true;
                                actor.m_boSkeleton = true;
                            }
                            if (ident == Grobal2.SM_DEATH)
                            {
                                if (HUtil32.HiByte(cdir) != 0)
                                {
                                    actor.m_boItemExplore = true;
                                }
                            }
                        }
                    }
                    if (actor == null)
                    {
                        return;
                    }
                    switch (ident)
                    {
                        case Grobal2.SM_FEATURECHANGED:
                            actor.m_nFeature = feature;
                            actor.m_nFeatureEx = state;
                            if (str != "")
                            {
                                EDcode.DecodeBuffer(str, mbw);
                                actor.m_btTitleIndex = HUtil32.LoWord(mbw.param1);
                            }
                            else
                            {
                                actor.m_btTitleIndex = 0;
                            }
                            actor.FeatureChanged();
                            break;
                        case Grobal2.SM_APPRCHANGED:
                            break;
                        case Grobal2.SM_CHARSTATUSCHANGED:
                            actor.m_nState = feature;
                            actor.m_nHitSpeed = state;
                            break;
                        default:
                            if (ident == Grobal2.SM_TURN)
                            {
                                if (str != "")
                                {
                                    actor.m_sUserName = str;
                                    actor.m_sUserNameOffSet = HGECanvas.Units.HGECanvas.g_DXCanvas.TextWidth(actor.m_sUserName) / 2;
                                }
                            }
                            actor.SendMsg(ident, x, y, cdir, feature, state, "", 0);
                            break;
                    }
                    break;
            }
        }
    }
}
