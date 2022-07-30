using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SystemModule;
using SystemModule.Common;

namespace GameSvr
{
    public struct TRefillCretInfo
    {
        public int x;
        public int y;
        public byte size;
        public byte count;
        public byte race;
    } // end TRefillCretInfo

    public class TUserEngine
    {
        private readonly ArrayList ReadyList = null;
        // µ¿±âÈ­ ÇÊ¿ä
        private readonly ArrayList RunUserList = null;
        private readonly ArrayList OtherUserNameList = null;
        // ´Ù¸¥ ¼­¹ö¿¡ ÀÖ´Â »ç¿ëÀÚ ¸®½ºÆ®
        private readonly ArrayList ClosePlayers = null;
        private readonly ArrayList SaveChangeOkList = null;
        private object FUserCS = null;
        private long timer10min = 0;
        private long timer10sec = 0;
        private long timer1min = 0;
        private long opendoorcheck = 0;
        private long missiontime = 0;
        // ¹Ì¼ÇÀº 1ÃÊ¿¡ ÇÑ¹ø Æ½ÀÌ µÈ´Ù.
        private long onezentime = 0;
        // Á¨À» Á¶±Ý¾¿ ÇÑ´Ù.
        private long runonetime = 0;
        private long hum200time = 0;
        private readonly long usermgrcheck = 0;
        private long eventitemtime = 0;
        // À¯´ÏÅ© ¾ÆÀÌÅÛ ÀÌº¥Æ®ÀÇ º¯¼ö
        private int GenCur = 0;
        private int MonCur = 0;
        private int MonSubCur = 0;
        private int HumCur = 0;
        private int HumRotCount = 0;
        private int MerCur = 0;
        private int NpcCur = 0;
        private int gaCount = 0;
        private int gaDecoItemCount = 0;
        // 2003/06/20 ÀÌº¥Æ®¸÷ Á¨ ¸Þ¼¼Áö ¸®½ºÆ®
        public ArrayList GenMsgList = null;
        public ArrayList StdItemList = null;
        public ArrayList MonDefList = null;
        public ArrayList MonList = null;
        public IList<TDefMagic> DefMagicList = null;
        public ArrayList AdminList = null;
        public StringList ChatLogList = null;
        public ArrayList MerchantList = null;
        public ArrayList NpcList = null;
        public ArrayList MissionList = null;
        // ¹Ì¼Ç...
        public ArrayList WaitServerList = null;
        public ArrayList HolySeizeList = null;
        // °á°èÀÇ ¸®½ºÆ®
        // ShopItemList: TGList;
        public int MonCount = 0;
        public int MonCurCount = 0;
        public int MonRunCount = 0;
        public int MonCurRunCount = 0;
        public bool BoUniqueItemEvent = false;
        public int UniqueItemEventInterval = 0;
        // 2003/03/18 Å×½ºÆ® ¼­¹ö ÀÎ¿ø Á¦ÇÑ
        public int FreeUserCount = 0;
        // TUserEngine
        //Constructor  Create()
        public TUserEngine() : base()
        {
            RunUserList = new ArrayList();
            OtherUserNameList = new ArrayList();
            ClosePlayers = new ArrayList();
            SaveChangeOkList = new ArrayList();
            // 2003/06/20 ÀÌº¥Æ®¸÷ Á¨ ¸Þ¼¼Áö ¸®½ºÆ®
            GenMsgList = new ArrayList();
            MonList = new ArrayList();
            MonDefList = new ArrayList();
            ReadyList = new ArrayList();
            // µ¿±âÈ­  ÇÊ¿ä
            StdItemList = new ArrayList();
            // Index°¡ TUserItem¿¡¼­ ¸®ÆÛ·±½º ÇÏ¹Ç·Î ¼ø¼­°¡ º¯°æµÇ¾î¼­´Â ¾ÈµÈ´Ù.
            DefMagicList = new ArrayList();
            AdminList = new ArrayList();
            ChatLogList = new ArrayList();
            MerchantList = new ArrayList();
            NpcList = new ArrayList();
            MissionList = new ArrayList();
            WaitServerList = new ArrayList();
            HolySeizeList = new ArrayList();
            timer10min = HUtil32.GetTickCount();
            timer10sec = HUtil32.GetTickCount();
            timer1min = HUtil32.GetTickCount();
            opendoorcheck = HUtil32.GetTickCount();
            missiontime = HUtil32.GetTickCount();
            onezentime = HUtil32.GetTickCount();
            hum200time = HUtil32.GetTickCount();
            usermgrcheck = HUtil32.GetTickCount();
            GenCur = 0;
            MonCur = 0;
            MonSubCur = 0;
            HumCur = 0;
            HumRotCount = 0;
            MerCur = 0;
            NpcCur = 0;
            // 2003/03/18 Å×½ºÆ® ¼­¹ö ÀÎ¿ø Á¦ÇÑ
            FreeUserCount = 0;
            gaCount = 0;
            gaDecoItemCount = 0;
            BoUniqueItemEvent = false;
            UniqueItemEventInterval = 30 * 60 * 1000;
            eventitemtime = HUtil32.GetTickCount();
            FUserCS = new object();
        }

        ~TUserEngine()
        {
            //int i;
            //for (i = 0; i < RunUserList.Count; i++)
            //{
            //    ((TUserHuman)(RunUserList.Values[i])).Free();
            //}
            //RunUserList.Free();
            //OtherUserNameList.Free();
            //ClosePlayers.Free();
            //SaveChangeOkList.Free();
            //GenMsgList.Free();
            //for (i = 0; i < MonList.Count; i++)
            //{
            //    Dispose(((TZenInfo)(MonList[i])));
            //}
            //MonList.Free();
            //MonDefList.Free();
            //for (i = 0; i < DefMagicList.Count; i++)
            //{
            //    Dispose(((TDefMagic)(DefMagicList[i])));
            //}
            //DefMagicList.Free();
            //ReadyList.Free();
            //for (i = 0; i < StdItemList.Count; i++)
            //{
            //    Dispose(((TStdItem)(StdItemList[i])));
            //}
            //StdItemList.Free();
            //ChatLogList.Free();
            //AdminList.Free();
            //MerchantList.Free();
            //NpcList.Free();
            //MissionList.Free();
            //WaitServerList.Free();
            //HolySeizeList.Free();
            //FUserCS.Free();
            //base.Destroy();
        }

        // StdItem
        // -------------------- StdItemList ----------------------
        // ´Ù¸¥ ½º·¡µå¿¡¼­ »ç¿ë ºÒ°¡ !!
        public string GetStdItemName(int itemindex)
        {
            string result;
            itemindex = itemindex - 1;
            // TUserItemÀÇ Index´Â +1µÈ °ÍÀÓ.  0Àº ºó°ÍÀ¸·Î °£ÁÖÇÔ.
            if ((itemindex >= 0) && (itemindex <= StdItemList.Count - 1))
            {
                result = ((TStdItem)StdItemList[itemindex]).Name;
            }
            else
            {
                result = "";
            }
            return result;
        }

        // ´Ù¸¥ ½º·¡µå¿¡¼­ »ç¿ë ºÒ°¡ !!
        public int GetStdItemIndex(string itmname)
        {
            int result;
            int i;
            result = -1;
            if (itmname == "")
            {
                return result;
            }
            for (i = 0; i < StdItemList.Count; i++)
            {
                if (((TStdItem)StdItemList[i]).Name.ToLower().CompareTo(itmname.ToLower()) == 0)
                {
                    result = i + 1;
                    break;
                }
            }
            return result;
        }

        // ´Ù¸¥ ½º·¡µå¿¡¼­ »ç¿ë ºÒ°¡ !!
        public int GetStdItemWeight(int itemindex, int Cnt)
        {
            int result;
            TStdItem psd;
            itemindex = itemindex - 1;
            // TUserItemÀÇ Index´Â +1µÈ °ÍÀÓ.  0Àº ºó°ÍÀ¸·Î °£ÁÖÇÔ.
            if ((itemindex >= 0) && (itemindex <= StdItemList.Count - 1))
            {
                psd = (TStdItem)StdItemList[itemindex];
                if (psd.OverlapItem == 1)
                {
                    result = psd.Weight + psd.Weight * (Cnt / 10);
                }
                else if (psd.OverlapItem >= 2)
                {
                    result = psd.Weight * Cnt;
                }
                else
                {
                    result = psd.Weight;
                }
            }
            else
            {
                result = 0;
            }
            return result;
        }

        // ´Ù¸¥ ½º·¡µå¿¡¼­ »ç¿ë ºÒ°¡ !!
        public TStdItem GetStdItem(int index)
        {
            TStdItem result;
            index = index - 1;
            if ((index >= 0) && (index < StdItemList.Count))
            {
                result = (TStdItem)StdItemList[index];
                // ÀÌ¸§ÀÌ ¾ø´Â ¾ÆÀÌÅÛÀº »ç¶óÁø ¾ÆÀÌÅÛ
                if (result.Name == "")
                {
                    result = null;
                }
            }
            else
            {
                result = null;
            }
            return result;
        }

        // ´Ù¸¥ ½º·¡µå¿¡¼­ »ç¿ë ºÒ°¡ !!
        public TStdItem GetStdItemFromName(string itmname)
        {
            TStdItem result;
            int i;
            result = null;
            if (itmname == "")
            {
                return result;
            }
            for (i = 0; i < StdItemList.Count; i++)
            {
                if (((TStdItem)StdItemList[i]).Name.ToLower().CompareTo(itmname.ToLower()) == 0)
                {
                    result = (TStdItem)StdItemList[i];
                    break;
                }
            }
            return result;
        }

        // ´Ù¸¥ ½º·¡µå¿¡¼­ »ç¿ë ºÒ°¡ !!
        public bool CopyToUserItem(int itmindex, ref TUserItem uitem)
        {
            bool result;
            result = false;
            itmindex = itmindex - 1;
            if ((itmindex >= 0) && (itmindex < StdItemList.Count))
            {
                uitem.Index = (ushort)(itmindex + 1);
                // Index=0Àº ºó°ÍÀ¸·Î ÀÎ½Ä
                uitem.MakeIndex = svMain.GetItemServerIndex();
                uitem.Dura = ((TStdItem)StdItemList[itmindex]).DuraMax;
                uitem.DuraMax = ((TStdItem)StdItemList[itmindex]).DuraMax;
                result = true;
            }
            return result;
        }

        // ´Ù¸¥ ½º·¡µå¿¡¼­ »ç¿ë ºÒ°¡ !!
        public bool CopyToUserItemFromName(string itmname, ref TUserItem uitem)
        {
            bool result;
            int i;
            result = false;
            if (itmname == "")
            {
                return result;
            }
            for (i = 0; i < StdItemList.Count; i++)
            {
                if (((TStdItem)StdItemList[i]).Name.ToLower().CompareTo(itmname.ToLower()) == 0)
                {
                    //FillChar(uitem, sizeof(TUserItem), '\0');
                    uitem.Index = (ushort)(i + 1);
                    // Index=0Àº ºó°ÍÀ¸·Î ÀÎ½Ä
                    uitem.MakeIndex = svMain.GetItemServerIndex();
                    // »õ MakeIndex ¹ß±Þ
                    // Ä«¿îÆ®¾ÆÀÌÅÛ °³¼ö 0°³ ¹ö±× ¼öÁ¤(sonmg 2004/02/17)
                    if (((TStdItem)StdItemList[i]).OverlapItem >= 1)
                    {
                        if (((TStdItem)StdItemList[i]).DuraMax == 0)
                        {
                            uitem.Dura = 1;
                        }
                        else
                        {
                            uitem.Dura = ((TStdItem)StdItemList[i]).DuraMax;
                        }
                    }
                    else
                    {
                        uitem.Dura = ((TStdItem)StdItemList[i]).DuraMax;
                    }
                    uitem.DuraMax = ((TStdItem)StdItemList[i]).DuraMax;
                    result = true;
                    break;
                }
            }
            return result;
        }

        // ´Ù¸¥ ½º·¡µå¿¡¼­ »ç¿ë ºÒ°¡ !!(2004/03/16)
        public string GetStdItemNameByShape(int stdmode, int shape)
        {
            string result;
            int i;
            TStdItem pstd;
            result = "";
            for (i = 0; i < StdItemList.Count; i++)
            {
                pstd = (TStdItem)StdItemList[i];
                if (pstd != null)
                {
                    if ((pstd.StdMode == stdmode) && (pstd.Shape == shape))
                    {
                        result = pstd.Name;
                        break;
                    }
                }
            }
            return result;
        }

        public TShopItem GetShopItemByName(string sItemName)
        {
            TShopItem result;
            int i;
            TShopItem pShopItem;
            result = null;
            svMain.ShopItemList.Lock;
            try
            {
                for (i = 0; i < svMain.ShopItemList.Count; i++)
                {
                    pShopItem = svMain.ShopItemList.Items[i];
                    if (sItemName.ToLower().CompareTo(pShopItem.sItemName.ToLower()) == 0)
                    {
                        result = pShopItem;
                        break;
                    }
                }
            }
            finally
            {
                svMain.ShopItemList.UnLock;
            }
            return result;
        }

        // -------------------- Background and system ----------------------
        private void SendRefMsgEx(TEnvirnoment envir, int x, int y, short msg, short wparam, long lParam1, long lParam2, long lParam3, string str)
        {
            int i;
            int j;
            int k;
            int stx;
            int sty;
            int enx;
            int eny;
            TCreature cret;
            TMapInfo pm = null;
            bool inrange;
            stx = x - 12;
            enx = x + 12;
            sty = y - 12;
            eny = y + 12;
            for (i = stx; i <= enx; i++)
            {
                for (j = sty; j <= eny; j++)
                {
                    inrange = envir.GetMapXY(i, j, ref pm);
                    if (inrange)
                    {
                        if (pm.OBJList != null)
                        {
                            for (k = 0; k < pm.OBJList.Count; k++)
                            {
                                // creature//
                                if (pm.OBJList[k] != null)
                                {
                                    if (((TAThing)pm.OBJList[k]).Shape == Grobal2.OS_MOVINGOBJECT)
                                    {
                                        cret = (TCreature)((TAThing)pm.OBJList[k]).AObject;
                                        if (cret != null)
                                        {
                                            if (!cret.BoGhost)
                                            {
                                                if (cret.RaceServer == Grobal2.RC_USERHUMAN)
                                                {
                                                    cret.SendMsg(cret, (ushort)msg, (ushort)wparam, lParam1, lParam2, lParam3, str);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        // sys
        public bool OpenDoor(TEnvirnoment envir, int dx, int dy)
        {
            bool result;
            TDoorInfo pd;
            result = false;
            pd = envir.FindDoor(dx, dy);
            if (pd != null)
            {
                if ((!pd.PCore.DoorOpenState) && (!pd.PCore.__Lock))
                {
                    // ÀÌ¹Ì ¿­·Á ÀÖ°Å³ª, Àá°ÜÀÖÁö¾ÊÀ¸¸é.
                    pd.PCore.DoorOpenState = true;
                    pd.PCore.OpenTime = HUtil32.GetTickCount();
                    SendRefMsgEx(envir, dx, dy, Grobal2.RM_OPENDOOR_OK, 0, dx, dy, 0, "");
                    result = true;
                }
            }
            return result;
        }

        public bool CloseDoor(TEnvirnoment envir, TDoorInfo pd)
        {
            bool result;
            result = false;
            if (pd != null)
            {
                if (pd.PCore.DoorOpenState)
                {
                    pd.PCore.DoorOpenState = false;
                    SendRefMsgEx(envir, pd.DoorX, pd.DoorY, Grobal2.RM_CLOSEDOOR, 0, pd.DoorX, pd.DoorY, 0, "");
                    result = true;
                }
            }
            return result;
        }

        private void CheckOpenDoors()
        {
            int k;
            int i;
            TDoorInfo pd;
            TEnvirnoment e;
            try
            {
                for (k = 0; k < svMain.GrobalEnvir.Count; k++)
                {
                    for (i = 0; i < ((TEnvirnoment)svMain.GrobalEnvir[k]).DoorList.Count; i++)
                    {
                        e = (TEnvirnoment)svMain.GrobalEnvir[k];
                        if (e.DoorList[i].PCore.DoorOpenState)
                        {
                            pd = e.DoorList[i];
                            if (HUtil32.GetTickCount() - pd.PCore.OpenTime > 5000)
                            {
                                CloseDoor(e, pd);
                            }
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("EXCEPTION : CHECKOPENDOORS");
            }
        }

        // -------------------- Npc & Monster ----------------------
        private void LoadRefillCretInfos()
        {
        }

        // Merchant
        public TCreature GetMerchant(int npcid)
        {
            TCreature result;
            int i;
            result = null;
            for (i = 0; i < MerchantList.Count; i++)
            {
                if (((int)MerchantList[i]) == npcid)
                {
                    result = (TCreature)MerchantList[i];
                    break;
                }
            }
            return result;
        }

        // npcid´Â TCreatureÀÓ.
        public int GetMerchantXY(TEnvirnoment envir, int x, int y, int wide, ArrayList npclist)
        {
            int result;
            int i;
            for (i = 0; i < MerchantList.Count; i++)
            {
                if ((((TCreature)MerchantList[i]).PEnvir == envir) && (Math.Abs(((TCreature)MerchantList[i]).CX - x) <= wide) && (Math.Abs(((TCreature)MerchantList[i]).CY - y) <= wide))
                {
                    npclist.Add(MerchantList[i]);
                }
            }
            result = npclist.Count;
            return result;
        }

        public void InitializeMerchants()
        {
            int i;
            TMerchant m;
            string frmcap;
            frmcap = svMain.FrmMain.Text;
            for (i = MerchantList.Count - 1; i >= 0; i--)
            {
                m = (TMerchant)MerchantList[i];
                m.PEnvir = svMain.GrobalEnvir.GetEnvir(m.MapName);
                if (m.PEnvir != null)
                {
                    m.Initialize();
                    if (m.ErrorOnInit)
                    {
                        svMain.MainOutMessage("Merchant Initalize fail... " + m.UserName);
                        m.Free();
                        MerchantList.RemoveAt(i);
                    }
                    else
                    {
                        m.LoadMerchantInfos();
                        m.LoadMarketSavedGoods();
                        m.LoadMemorialCount();
                    }
                }
                else
                {
                    svMain.MainOutMessage("Merchant Initalize fail... (m.PEnvir=nil) " + m.UserName);
                    m.Free();
                    MerchantList.RemoveAt(i);
                }
                svMain.FrmMain.Text = "Merchant Loading.. " + (MerchantList.Count - i + 1).ToString() + "/" + MerchantList.Count.ToString();
                svMain.FrmMain.RefreshForm();
            }
            svMain.FrmMain.Text = frmcap;
        }

        public TCreature GetNpc(int npcid)
        {
            TCreature result;
            int i;
            result = null;
            for (i = 0; i < NpcList.Count; i++)
            {
                if (((int)NpcList[i]) == npcid)
                {
                    result = (TCreature)NpcList[i];
                    break;
                }
            }
            return result;
        }

        public int GetNpcXY(TEnvirnoment envir, int x, int y, int wide, ArrayList list)
        {
            int result;
            int i;
            for (i = 0; i < NpcList.Count; i++)
            {
                if ((((TCreature)NpcList[i]).PEnvir == envir) && (Math.Abs(((TCreature)NpcList[i]).CX - x) <= wide) && (Math.Abs(((TCreature)NpcList[i]).CY - y) <= wide))
                {
                    list.Add(NpcList[i]);
                }
            }
            result = list.Count;
            return result;
        }

        public void InitializeNpcs()
        {
            int i;
            TNormNpc npc;
            string frmcap;
            frmcap = svMain.FrmMain.Text;
            for (i = NpcList.Count - 1; i >= 0; i--)
            {
                npc = (TNormNpc)NpcList[i];
                npc.PEnvir = svMain.GrobalEnvir.GetEnvir(npc.MapName);
                if (npc.PEnvir != null)
                {
                    npc.Initialize();
                    if (npc.ErrorOnInit && !npc.BoInvisible)
                    {
                        svMain.MainOutMessage("Npc Initalize fail... " + npc.UserName);
                        npc.Free();
                        NpcList.RemoveAt(i);
                    }
                    else
                    {
                        npc.LoadNpcInfos();
                        npc.LoadMemorialCount();
                    }
                }
                else
                {
                    svMain.MainOutMessage("Npc Initalize fail... [Mapinfo or Map] (npc.PEnvir=nil) " + npc.UserName);
                    npc.Free();
                    NpcList.RemoveAt(i);
                }
                svMain.FrmMain.Text = "Npc loading.. " + (NpcList.Count - i + 1).ToString() + "/" + NpcList.Count.ToString();
                svMain.FrmMain.RefreshForm();
            }
            svMain.FrmMain.Text = frmcap;
        }

        public TCreature GetDefaultNpc(int npcid)
        {
            TCreature result;
            result = null;
            if (((int)svMain.DefaultNpc) == npcid)
            {
                result = svMain.DefaultNpc;
            }
            return result;
        }

        public void InitializeDefaultNpcs()
        {
            TNormNpc npc;
            npc = svMain.DefaultNpc;
            npc.PEnvir = svMain.GrobalEnvir.GetEnvir(npc.MapName);
            if (npc.PEnvir != null)
            {
                npc.Initialize();
                if (npc.ErrorOnInit && !npc.BoInvisible)
                {
                    svMain.MainOutMessage("DefaultNpc Initalize fail... " + npc.UserName);
                    npc.Free();
                }
                else
                {
                    npc.LoadNpcInfos();
                }
            }
            else
            {
                svMain.MainOutMessage("DefaultNpc Initalize fail... [Mapinfo or Map] (npc.PEnvir=nil) " + npc.UserName);
                npc.Free();
            }
        }

        // Monster, NPC
        public int GetMonRace(string monname)
        {
            int result;
            int i;
            result = -1;
            for (i = 0; i < MonDefList.Count; i++)
            {
                if (((TMonsterInfo)MonDefList[i]).Name.ToLower().CompareTo(monname.ToLower()) == 0)
                {
                    result = ((TMonsterInfo)MonDefList[i]).Race;
                    break;
                }
            }
            return result;
        }

        public int GetMonLevel(string monname)
        {
            int result;
            int i;
            result = -1;
            for (i = 0; i < MonDefList.Count; i++)
            {
                if (((TMonsterInfo)MonDefList[i]).Name.ToLower().CompareTo(monname.ToLower()) == 0)
                {
                    result = ((TMonsterInfo)MonDefList[i]).Level;
                    break;
                }
            }
            return result;
        }

        public void ApplyMonsterAbility(TCreature cret, string monname)
        {
            int i;
            TMonsterInfo pm;
            for (i = 0; i < MonDefList.Count; i++)
            {
                if (((TMonsterInfo)MonDefList[i]).Name.ToLower().CompareTo(monname.ToLower()) == 0)
                {
                    pm = (TMonsterInfo)MonDefList[i];
                    cret.RaceServer = pm.Race;
                    cret.RaceImage = pm.RaceImg;
                    cret.Appearance = pm.Appr;
                    cret.Abil.Level = pm.Level;
                    cret.LifeAttrib = pm.LifeAttrib;
                    cret.CoolEye = pm.CoolEye;
                    cret.FightExp = pm.Exp;
                    cret.Abil.HP = pm.HP;
                    cret.Abil.MaxHP = pm.HP;
                    cret.Abil.MP = pm.MP;
                    cret.Abil.MaxMP = pm.MP;
                    cret.Abil.AC = HUtil32.MakeWord(pm.AC, pm.AC);
                    cret.Abil.MAC = HUtil32.MakeWord(pm.MAC, pm.MAC);
                    cret.Abil.DC = HUtil32.MakeWord(pm.DC, pm.MaxDC);
                    cret.Abil.MC = HUtil32.MakeWord(pm.MC, pm.MC);
                    cret.Abil.SC = HUtil32.MakeWord(pm.SC, pm.SC);
                    cret.SpeedPoint = pm.Speed;
                    cret.AccuracyPoint = pm.Hit;
                    cret.NextWalkTime = pm.WalkSpeed;
                    cret.WalkStep = pm.WalkStep;
                    cret.WalkWaitTime = pm.WalkWait;
                    cret.NextHitTime = pm.AttackSpeed;
                    cret.Tame = pm.Tame;
                    cret.AntiPush = pm.AntiPush;
                    cret.AntiUndead = pm.AntiUndead;
                    cret.SizeRate = pm.SizeRate;
                    cret.AntiStop = pm.AntiStop;
                    break;
                }
            }
        }

        public void RandomUpgradeItem(TUserItem pu)
        {
            TStdItem pstd;
            pstd = GetStdItem(pu.Index);
            if (pstd != null)
            {
                switch (pstd.StdMode)
                {
                    case 5:
                    case 6:
                        // ¹«±â
                        svMain.ItemMan.UpgradeRandomWeapon(pu);
                        break;
                    case 10:
                    case 11:
                        // ³²ÀÚ¿Ê, ¿©ÀÚ¿Ê
                        svMain.ItemMan.UpgradeRandomDress(pu);
                        break;
                    case 19:
                        // ¸ñ°ÉÀÌ (¸¶¹ýÈ¸ÇÇ, Çà¿î)
                        svMain.ItemMan.UpgradeRandomNecklace19(pu);
                        break;
                    case 20:
                    case 21:
                    case 24:
                        // ¸ñ°ÉÀÌ ÆÈÂî
                        svMain.ItemMan.UpgradeRandomNecklace(pu);
                        break;
                    case 26:
                        svMain.ItemMan.UpgradeRandomBarcelet(pu);
                        break;
                    case 22:
                        // ¹ÝÁö
                        svMain.ItemMan.UpgradeRandomRings(pu);
                        break;
                    case 23:
                        // ¹ÝÁö
                        svMain.ItemMan.UpgradeRandomRings23(pu);
                        break;
                    case 15:
                        // Çï¸ä
                        svMain.ItemMan.UpgradeRandomHelmet(pu);
                        break;
                }
            }
        }

        public void RandomSetUnknownItem(TUserItem pu)
        {
            TStdItem pstd;
            pstd = GetStdItem(pu.Index);
            if (pstd != null)
            {
                switch (pstd.StdMode)
                {
                    case 15:
                        // Åõ±¸
                        svMain.ItemMan.RandomSetUnknownHelmet(pu);
                        break;
                    case 22:
                    case 23:
                        // ¹ÝÁö
                        svMain.ItemMan.RandomSetUnknownRing(pu);
                        break;
                    case 24:
                    case 26:
                        // ÆÈÂî
                        svMain.ItemMan.RandomSetUnknownBracelet(pu);
                        break;
                }
            }
        }

        public bool GetUniqueEvnetItemName(ref string iname, ref int numb)
        {
            bool result = false;
            if ((HUtil32.GetTickCount() - eventitemtime > UniqueItemEventInterval) && (svMain.EventItemList.Count > 0))
            {
                eventitemtime = HUtil32.GetTickCount();
                int n = new System.Random(svMain.EventItemList.Count).Next();
                iname = (string)svMain.EventItemList[n];
                numb = (int)svMain.EventItemList.Values[n];
                svMain.EventItemList.Remove(n);
                result = true;
            }
            return result;
        }

        public void ReloadAllMonsterItems()
        {
            int i;
            for (i = 0; i < MonDefList.Count; i++)
            {
                LocalDB.FrmDB.LoadMonItems(((TMonsterInfo)MonDefList[i]).Name, ref ((TMonsterInfo)MonDefList[i]).ItemList);
            }
        }

        public int MonGetRandomItems(TCreature mon)
        {
            int result;
            int i;
            ArrayList list;
            string iname;
            TMonItemInfo pmi;
            TUserItem pu;
            TStdItem pstd;
            list = null;
            for (i = 0; i < MonDefList.Count; i++)
            {
                if (((TMonsterInfo)MonDefList[i]).Name.ToLower().CompareTo(mon.UserName.ToLower()) == 0)
                {
                    list = ((TMonsterInfo)MonDefList[i]).ItemList;
                    break;
                }
            }
            if (list != null)
            {
                for (i = 0; i < list.Count; i++)
                {
                    pmi = (TMonItemInfo)list[i];
                    if (pmi.SelPoint >= new System.Random(pmi.MaxPoint).Next())
                    {
                        if (pmi.ItemName.ToLower().CompareTo("½ð±Ò".ToLower()) == 0)
                        {
                            // mon.Gold := mon.Gold + (pmi.Count div 2) + Random(pmi.Count);
                            mon.IncGold((pmi.Count / 2) + new System.Random(pmi.Count).Next());
                        }
                        else
                        {
                            // À¯´ÏÅ© ¾ÆÀÌÅÛ ÀÌº¥Æ®....
                            iname = "";
                            // //if (BoUniqueItemEvent) and (not mon.BoAnimal) then begin
                            // //   if GetUniqueEvnetItemName (iname, numb) then begin
                            // numb; //iname
                            // //   end;
                            // //end;
                            if (iname == "")
                            {
                                iname = pmi.ItemName;
                            }
                            pu = new TUserItem();
                            if (CopyToUserItemFromName(iname, ref pu))
                            {
                                // ³»±¸¼ºÀÌ º¯°æµÇ¾î ÀÖÀ½..
                                pu.Dura = (ushort)HUtil32.MathRound(pu.DuraMax / 100 * (20 + new System.Random(80).Next()));
                                pstd = GetStdItem(pu.Index);
                                // //if pstd <> nil then
                                // //   if pstd.StdMode = 50 then begin  //»óÇ°±Ç
                                // //      pu.Dura := numb;
                                // //   end;
                                // ³·Àº È®·ü·Î
                                // ¾ÆÀÌÅÛÀÇ ¾÷±×·¹ÀÌµåµÈ ³»¿ë Àû¿ë
                                if (new System.Random(10).Next() == 0)
                                {
                                    RandomUpgradeItem(pu);
                                }
                                if (pstd != null)
                                {
                                    // ¹ÌÁö ½Ã¸®Áî ¾ÆÀÌÅÛÀÎ °æ¿ì
                                    if (new ArrayList(new int[] { 15, 19, 20, 21, 22, 23, 24, 26, 52, 53, 54 }).Contains(pstd.StdMode))
                                    {
                                        if ((pstd.Shape == ObjBase.RING_OF_UNKNOWN) || (pstd.Shape == ObjBase.BRACELET_OF_UNKNOWN) || (pstd.Shape == ObjBase.HELMET_OF_UNKNOWN))
                                        {
                                            svMain.UserEngine.RandomSetUnknownItem(pu);
                                        }
                                    }
                                    if (pstd.OverlapItem >= 1)
                                    {
                                        pu.Dura = 1;
                                        // gadget:Ä«¿îÆ®¾ÆÀÌÅÛ
                                    }
                                }
                                mon.ItemList.Add(pu);
                            }
                            else
                            {
                                Dispose(pu);
                            }
                        }
                    }
                }
            }
            result = 1;
            return result;
        }

        public TCreature AddCreature(string map, short x, short y, int race, string monname)
        {
            TCreature result;
            TEnvirnoment env;
            TCreature cret;
            int i;
            int stepx;
            int edge;
            object outofrange;
            result = null;
            cret = null;
            env = svMain.GrobalEnvir.GetEnvir(map);
            if (env == null)
            {
                return result;
            }
            switch (race)
            {
                case Grobal2.RC_DOORGUARD:
                    cret = new TSuperGuard();
                    break;
                case Grobal2.RC_ANIMAL + 1:
                    // ´ß
                    cret = new TMonster();
                    cret.BoAnimal = true;
                    cret.MeatQuality = 3000 + new System.Random(3500).Next();
                    // ±âº»°ª.
                    cret.BodyLeathery = 50;
                    break;
                case Grobal2.RC_RUNAWAYHEN:
                    // ±âº»°ª
                    // ´Þ¾Æ³ª´Â ´ß(sonmg 2004/12/27)
                    cret = new TChickenDeer();
                    // ´Þ¾Æ³²
                    cret.BoAnimal = true;
                    cret.MeatQuality = 3000 + new System.Random(3500).Next();
                    // ±âº»°ª.
                    cret.BodyLeathery = 50;
                    break;
                case Grobal2.RC_DEER:
                    // ±âº»°ª
                    // »ç½¿
                    if (new System.Random(30).Next() == 0)
                    {
                        cret = new TChickenDeer();
                        // °ÌÀïÀÌ »ç½¿, ´Þ¾Æ³²
                        cret.BoAnimal = true;
                        cret.MeatQuality = 10000 + new System.Random(20000).Next();
                        cret.BodyLeathery = 150;
                        // ±âº»°ª
                    }
                    else
                    {
                        cret = new TMonster();
                        cret.BoAnimal = true;
                        cret.MeatQuality = 8000 + new System.Random(8000).Next();
                        // ±âº»°ª.
                        cret.BodyLeathery = 150;
                        // ±âº»°ª
                    }
                    break;
                case Grobal2.RC_WOLF:
                    cret = new TATMonster();
                    cret.BoAnimal = true;
                    cret.MeatQuality = 8000 + new System.Random(8000).Next();
                    // ±âº»°ª.
                    cret.BodyLeathery = 150;
                    break;
                case Grobal2.RC_TRAINER:
                    // ±âº»°ª
                    // ¼ö·ÃÁ¶±³
                    cret = new TTrainer();
                    cret.RaceServer = Grobal2.RC_TRAINER;
                    break;
                case Grobal2.RC_MONSTER:
                    cret = new TMonster();
                    break;
                case Grobal2.RC_OMA:
                    cret = new TATMonster();
                    break;
                case Grobal2.RC_BLACKPIG:
                    cret = new TATMonster();
                    if (new System.Random(2).Next() == 0)
                    {
                        cret.BoFearFire = true;
                    }
                    break;
                case Grobal2.RC_SPITSPIDER:
                    cret = new TSpitSpider();
                    break;
                case Grobal2.RC_SLOWMONSTER:
                    cret = new TSlowATMonster();
                    break;
                case Grobal2.RC_SCORPION:
                    // Àü°¥
                    cret = new TScorpion();
                    break;
                case Grobal2.RC_KILLINGHERB:
                    cret = new TStickMonster();
                    break;
                case Grobal2.RC_SKELETON:
                    // ÇØ°ñ
                    cret = new TATMonster();
                    break;
                case Grobal2.RC_DUALAXESKELETON:
                    // ½Öµµ³¢ÇØ°ñ
                    cret = new TDualAxeMonster();
                    break;
                case Grobal2.RC_HEAVYAXESKELETON:
                    // Å«µµ³¢ÇØ°ñ
                    cret = new TATMonster();
                    break;
                case Grobal2.RC_KNIGHTSKELETON:
                    // ÇØ°ñÀü»ç
                    cret = new TATMonster();
                    break;
                case Grobal2.RC_BIGKUDEKI:
                    // ´ëÇü±¸µ¥±â
                    cret = new TGasAttackMonster();
                    break;
                case Grobal2.RC_COWMON:
                    // ¿ì¸é±Í
                    cret = new TCowMonster();
                    if (new System.Random(2).Next() == 0)
                    {
                        cret.BoFearFire = true;
                    }
                    break;
                case Grobal2.RC_MAGCOWFACEMON:
                    cret = new TMagCowMonster();
                    break;
                case Grobal2.RC_COWFACEKINGMON:
                    cret = new TCowKingMonster();
                    break;
                case Grobal2.RC_THORNDARK:
                    cret = new TThornDarkMonster();
                    break;
                case Grobal2.RC_LIGHTINGZOMBI:
                    cret = new TLightingZombi();
                    break;
                case Grobal2.RC_DIGOUTZOMBI:
                    cret = new TDigOutZombi();
                    if (new System.Random(2).Next() == 0)
                    {
                        cret.BoFearFire = true;
                    }
                    break;
                case Grobal2.RC_ZILKINZOMBI:
                    cret = new TZilKinZombi();
                    if (new System.Random(4).Next() == 0)
                    {
                        cret.BoFearFire = true;
                    }
                    break;
                case Grobal2.RC_WHITESKELETON:
                    cret = new TWhiteSkeleton();
                    break;
                case Grobal2.RC_ANGEL:
                    // ¼ÒÈ¯¹é°ñ
                    cret = new TAngelMon();
                    break;
                case Grobal2.RC_CLONE:
                    // Ãµ³à(¿ù·É)
                    cret = new TCloneMon();
                    break;
                case Grobal2.RC_FIREDRAGON:
                    // ºÐ½Å
                    cret = new TDragon();
                    break;
                case Grobal2.RC_DRAGONBODY:
                    // È­·æ
                    cret = new TDragonBody();
                    break;
                case Grobal2.RC_DRAGONSTATUE:
                    // È­·æ¸ö
                    cret = new TDragonStatue();
                    break;
                case Grobal2.RC_SCULTUREMON:
                    // ¿ë¼®»ó
                    cret = new TScultureMonster();
                    cret.BoFearFire = true;
                    break;
                case Grobal2.RC_SCULKING:
                    cret = new TScultureKingMonster();
                    break;
                case Grobal2.RC_SCULKING_2:
                    cret = new TScultureKingMonster();
                    ((TScultureKingMonster)cret).BoCallFollower = false;
                    break;
                case Grobal2.RC_BEEQUEEN:
                    cret = new TBeeQueen();
                    break;
                case Grobal2.RC_ARCHERMON:
                    // ¹úÅë
                    cret = new TArcherMonster();
                    break;
                case Grobal2.RC_GASMOTH:
                    // ¸¶±Ã»ç
                    // °¡½º½î´Â ½û±â³ª¹æ
                    cret = new TGasMothMonster();
                    break;
                case Grobal2.RC_DUNG:
                    // ¸¶ºñ°¡½º, µÕ
                    cret = new TGasDungMonster();
                    break;
                case Grobal2.RC_CENTIPEDEKING:
                    // ÃË·æ½Å, Áö³×¿Õ
                    cret = new TCentipedeKingMonster();
                    break;
                case Grobal2.RC_BIGHEARTMON:
                    cret = new TBigHeartMonster();
                    break;
                case Grobal2.RC_BAMTREE:
                    // Ç÷°Å¿Õ, ½ÉÀå
                    cret = new TBamTreeMonster();
                    break;
                case Grobal2.RC_SPIDERHOUSEMON:
                    cret = new TSpiderHouseMonster();
                    break;
                case Grobal2.RC_EXPLOSIONSPIDER:
                    // °Å¹ÌÁý,  Æø¾È°Å¹Ì
                    cret = new TExplosionSpider();
                    break;
                case Grobal2.RC_HIGHRISKSPIDER:
                    // ÆøÁÖ
                    cret = new THighRiskSpider();
                    break;
                case Grobal2.RC_BIGPOISIONSPIDER:
                    cret = new TBigPoisionSpider();
                    break;
                case Grobal2.RC_BLACKSNAKEKING:
                    // Èæ»ç¿Õ, ´õºí °ø°Ý
                    cret = new TDoubleCriticalMonster();
                    break;
                case Grobal2.RC_NOBLEPIGKING:
                    // ±Íµ·¿Õ, °­·Â °ø°Ý(´õºí ¾Æ´Ô)
                    cret = new TATMonster();
                    break;
                case Grobal2.RC_FEATHERKINGOFKING:
                    // ÈæÃµ¸¶¿Õ
                    cret = new TDoubleCriticalMonster();
                    break;
                case Grobal2.RC_SKELETONKING:
                    // 2003/02/11 ÇØ°ñ ¹Ý¿Õ, ºÎ½Ä±Í, ÇØ°ñº´Á¹
                    // ÇØ°ñ¹Ý¿Õ
                    cret = new TSkeletonKingMonster();
                    break;
                case Grobal2.RC_TOXICGHOST:
                    // ºÎ½Ä±Í
                    cret = new TGasAttackMonster();
                    break;
                case Grobal2.RC_SKELETONSOLDIER:
                    // ÇØ°ñº´Á¹
                    cret = new TSkeletonSoldier();
                    break;
                case Grobal2.RC_BANYAGUARD:
                    // 2003/03/04 ¹Ý¾ßÁÂ»ç, ¿ì»ç, »ç¿ìÃµ¿Õ
                    // ¹Ý¾ßÁÂ/¿ì»ç
                    cret = new TBanyaGuardMonster();
                    break;
                case Grobal2.RC_DEADCOWKING:
                    // »ç¿ìÃµ¿Õ
                    cret = new TDeadCowKingMonster();
                    break;
                case Grobal2.RC_PBOMA1:
                    // 2003/07/15 °ú°ÅºñÃµ Ãß°¡¸÷
                    // ³¯°³¿À¸¶
                    cret = new TArcherMonster();
                    break;
                case Grobal2.RC_PBOMA2:
                case Grobal2.RC_PBOMA3:
                case Grobal2.RC_PBOMA4:
                case Grobal2.RC_PBOMA5:
                    // ¼è¹¶Ä¡»ó±Þ¿À¸¶
                    // ¸ùµÕÀÌ»ó±Þ¿À¸¶
                    // Ä®ÇÏ±Þ¿À¸¶
                    // µµ³¢ÇÏ±Þ¿À¸¶
                    cret = new TATMonster();
                    break;
                case Grobal2.RC_PBOMA6:
                    // È°ÇÏ±Þ¿À¸¶
                    cret = new TArcherMonster();
                    break;
                case Grobal2.RC_PBGUARD:
                    // °ú°ÅºñÃµ Ã¢°æºñ
                    cret = new TSuperGuard();
                    break;
                case Grobal2.RC_PBMSTONE1:
                    // ¸¶°è¼®1
                    cret = new TStoneMonster();
                    break;
                case Grobal2.RC_PBMSTONE2:
                    // ¸¶°è¼®2
                    cret = new TStoneMonster();
                    break;
                case Grobal2.RC_PBKING:
                    // °ú°ÅºñÃµ º¸½º
                    cret = new TPBKingMonster();
                    break;
                case Grobal2.RC_GOLDENIMUGI:
                    // È²±ÝÀÌ¹«±â(ºÎ·æ±Ý»ç)
                    cret = new TGoldenImugi();
                    break;
                case Grobal2.RC_CASTLEDOOR:
                    // ¼º¹®
                    cret = new TCastleDoor();
                    break;
                case Grobal2.RC_WALL:
                    cret = new TWallStructure();
                    break;
                case Grobal2.RC_ARCHERGUARD:
                    // ±Ã¼ö°æºñ
                    cret = new TArcherGuard();
                    break;
                case Grobal2.RC_ARCHERMASTER:
                    // ±Ã¼öÈ£À§º´
                    cret = new TArcherMaster();
                    break;
                case Grobal2.RC_ARCHERPOLICE:
                    // ±Ã¼ö°æÂû
                    cret = new TArcherPolice();
                    break;
                case Grobal2.RC_ELFMON:
                    cret = new TElfMonster();
                    break;
                case Grobal2.RC_ELFWARRIORMON:
                    // ½Å¼ö º¯½ÅÀü
                    cret = new TElfWarriorMonster();
                    break;
                case Grobal2.RC_SOCCERBALL:
                    // ½Å¼ö º¯½ÅÈÄ
                    cret = new TSoccerBall();
                    break;
                case Grobal2.RC_MINE:
                    cret = new TMineMonster();
                    break;
                case Grobal2.RC_EYE_PROG:
                    // »ç¾ÈÃæ -> ¼³ÀÎ´ëÃæ
                    cret = new TEyeProg();
                    break;
                case Grobal2.RC_STON_SPIDER:
                    // È¯¸¶¼®°Å¹Ì -> ½Å¼®µ¶¸¶ÁÖ
                    cret = new TStoneSpider();
                    break;
                case Grobal2.RC_GHOST_TIGER:
                    // È¯¿µÇÑÈ£
                    cret = new TGhostTiger();
                    break;
                case Grobal2.RC_JUMA_THUNDER:
                    // ÁÖ¸¶·Ú°ÝÀå -> ÁÖ¸¶°Ý·ÚÀå
                    cret = new TJumaThunder();
                    break;
                case Grobal2.RC_SUPEROMA:
                    cret = new TSuperOma();
                    break;
                case Grobal2.RC_TOGETHEROMA:
                    cret = new TTogetherOma();
                    break;
                case Grobal2.RC_STICKBLOCK:
                    // È£È¥¼®
                    cret = new TStickBlockMonster();
                    break;
                case Grobal2.RC_FOXWARRIOR:
                    // ºñ¿ù¿©¿ì(Àü»ç) ºñ¿ùÈæÈ£
                    cret = new TFoxWarrior();
                    break;
                case Grobal2.RC_FOXWIZARD:
                    // ºñ¿ù¿©¿ì(¼ú»ç) ºñ¿ùÀûÈ£
                    cret = new TFoxWizard();
                    break;
                case Grobal2.RC_FOXTAOIST:
                    // ºñ¿ù¿©¿ì(µµ»ç) ºñ¿ù¼ÒÈ£
                    cret = new TFoxTaoist();
                    break;
                case Grobal2.RC_PUSHEDMON:
                    // È£±â¿¬
                    cret = new TPushedMon();
                    ((TPushedMon)cret).AttackWide = 1;
                    break;
                case Grobal2.RC_PUSHEDMON2:
                    // È£±â¿Á
                    cret = new TPushedMon();
                    ((TPushedMon)cret).AttackWide = 2;
                    break;
                case Grobal2.RC_FOXPILLAR:
                    // È£È¥±â¼®
                    cret = new TFoxPillar();
                    break;
                case Grobal2.RC_FOXBEAD:
                    // ºñ¿ùÃµÁÖ
                    cret = new TFoxBead();
                    break;
            }
            if (cret != null)
            {
                ApplyMonsterAbility(cret, monname);
                cret.PEnvir = env;
                cret.MapName = map;
                cret.CX = x;
                cret.CY = y;
                cret.Dir = (byte)new System.Random(8).Next();
                cret.UserName = monname;
                cret.WAbil = cret.Abil;
                // Àº½Å º¼ È®·ü
                if (new System.Random(100).Next() < cret.CoolEye)
                {
                    cret.BoViewFixedHide = true;
                }
                MonGetRandomItems(cret);
                cret.Initialize();
                if (cret.ErrorOnInit)
                {
                    // Á¨ÀÚ¸®°¡ ¸ø¿òÁ÷ÀÌ´Â ÀÚ¸®
                    outofrange = null;
                    if (cret.PEnvir.MapWidth < 50)
                    {
                        stepx = 2;
                    }
                    else
                    {
                        stepx = 3;
                    }
                    if (cret.PEnvir.MapHeight < 250)
                    {
                        if (cret.PEnvir.MapHeight < 30)
                        {
                            edge = 2;
                        }
                        else
                        {
                            edge = 20;
                        }
                    }
                    else
                    {
                        edge = 50;
                    }
                    for (i = 0; i <= 30; i++)
                    {
                        if (!cret.PEnvir.CanWalk(cret.CX, cret.CY, true))
                        {
                            if (cret.CX < cret.PEnvir.MapWidth - edge - 1)
                            {
                                cret.CX += (short)stepx;
                            }
                            else
                            {
                                cret.CX = (short)(edge + new System.Random(cret.PEnvir.MapWidth / 2).Next());
                                if (cret.CY < cret.PEnvir.MapHeight - edge - 1)
                                {
                                    cret.CY += (short)stepx;
                                }
                                else
                                {
                                    cret.CY = (short)(edge + new System.Random(cret.PEnvir.MapHeight / 2).Next());
                                }
                            }
                        }
                        else
                        {
                            outofrange = cret.PEnvir.AddToMap(cret.CX, cret.CY, Grobal2.OS_MOVINGOBJECT, cret);
                            break;
                        }
                    }
                    if (outofrange == null)
                    {
                        // ¿Õ¸÷Á¨ ½ºÅµµÇÁö ¾Ê°Ô(Å×½ºÆ®)
                        if ((race == Grobal2.RC_SKELETONKING) || (race == Grobal2.RC_DEADCOWKING) || (race == Grobal2.RC_FEATHERKINGOFKING) || (race == Grobal2.RC_PBKING))
                        {
                            cret.RandomSpaceMoveInRange(0, 0, 5);
                            svMain.MainOutMessage("Outofrange Nil - Race : " + race.ToString());
                        }
                        else
                        {
                            cret.Free();
                            cret = null;
                        }
                    }
                }
            }
            result = cret;
            return result;
        }

        public TCreature AddCreatureSysop(string map, int x, int y, string monname)
        {
            TCreature result;
            TCreature cret;
            int n;
            int race;
            race = svMain.UserEngine.GetMonRace(monname);
            cret = AddCreature(map, (short)x, (short)y, race, monname);
            if (cret != null)
            {
                n = MonList.Count - 1;
                if (n < 0)
                {
                    n = 0;
                }
                ((TZenInfo)MonList[n]).Mons.Add(cret);
            }
            result = cret;
            return result;
        }

        public bool RegenMonsters(TZenInfo pz, int zcount)
        {
            bool result;
            int i;
            int n;
            int zzx;
            int zzy;
            long start;
            TCreature cret;
            string str;
            result = true;
            start = HUtil32.GetTickCount();
            try
            {
                n = zcount;
                // pz.Count - pz.Mons.Count;
                // race := GetMonRace (pz.MonName);
                if (pz.MonRace > 0)
                {
                    if (new System.Random(100).Next() < pz.SmallZenRate)
                    {
                        // Á¨ÀÌ ¸ô·Á¼­ µÈ´Ù.
                        zzx = pz.X - pz.Area + new System.Random(pz.Area * 2 + 1).Next();
                        zzy = pz.Y - pz.Area + new System.Random(pz.Area * 2 + 1).Next();
                        for (i = 0; i < n; i++)
                        {
                            cret = AddCreature(pz.MapName, (short)(zzx - 10 + new System.Random(20).Next()), (short)(zzy - 10 + new System.Random(20).Next()), pz.MonRace, pz.MonName);
                            // 2003/06/20
                            if (cret != null)
                            {
                                pz.Mons.Add(cret);
                                if ((pz.TX != 0) && (pz.TY != 0))
                                {
                                    cret.BoHasMission = true;
                                    cret.Mission_X = pz.TX;
                                    cret.Mission_Y = pz.TY;
                                    // Á¨½Ã ¿ÜÄ¡´Â Á¤º¸°¡ 0º¸´Ù Ä¿¾ßµÊ
                                    if (pz.ZenShoutMsg < GenMsgList.Count)
                                    {
                                        str = (string)GenMsgList[pz.ZenShoutMsg];
                                    }
                                    else
                                    {
                                        str = "";
                                    }
                                    if (str != "")
                                    {
                                        switch (pz.ZenShoutType)
                                        {
                                            case 1:
                                                // ¼­¹ö ÀüÃ¼ ¿ÜÄ¡±â
                                                SysMsgAll(str);
                                                break;
                                            case 2:
                                                // ±×³É ¿ÜÄ¡±â
                                                // wide
                                                CryCry(Grobal2.RM_CRY, cret.PEnvir, cret.CX, cret.CY, 50, str);
                                                break;
                                        }
                                    }
                                }
                            }
                            if (HUtil32.GetTickCount() - start > svMain.ZenLimitTime)
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        for (i = 0; i < n; i++)
                        {
                            zzx = pz.X - pz.Area + new System.Random(pz.Area * 2 + 1).Next();
                            zzy = pz.Y - pz.Area + new System.Random(pz.Area * 2 + 1).Next();
                            cret = AddCreature(pz.MapName, (short)zzx, (short)zzy, pz.MonRace, pz.MonName);
                            if (cret != null)
                            {
                                pz.Mons.Add(cret);
                                if ((pz.TX != 0) && (pz.TY != 0))
                                {
                                    cret.BoHasMission = true;
                                    cret.Mission_X = pz.TX;
                                    cret.Mission_Y = pz.TY;
                                    if (pz.ZenShoutMsg < GenMsgList.Count)
                                    {
                                        str = (string)GenMsgList[pz.ZenShoutMsg];
                                    }
                                    else
                                    {
                                        str = "";
                                    }
                                    if (str != "")
                                    {
                                        switch (pz.ZenShoutType)
                                        {
                                            case 1:
                                                SysMsgAll(str);
                                                break;
                                            case 2:
                                                CryCry(Grobal2.RM_CRY, cret.PEnvir, cret.CX, cret.CY, 50, str);
                                                break;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (pz.MonRace == Grobal2.RC_SKELETONKING)
                                {
                                    svMain.MainOutMessage("RegenMon Nil : ÇØ°ñ¹Ý¿Õ-NIL");
                                    svMain.MainOutMessage(pz.MapName + " " + zzx.ToString() + "," + zzy.ToString() + " " + pz.MonRace.ToString() + " " + pz.MonName);
                                }
                                if (pz.MonRace == Grobal2.RC_DEADCOWKING)
                                {
                                    svMain.MainOutMessage("RegenMon Nil : »ç¿ìÃµ¿Õ-NIL");
                                    svMain.MainOutMessage(pz.MapName + " " + zzx.ToString() + "," + zzy.ToString() + " " + pz.MonRace.ToString() + " " + pz.MonName);
                                }
                                if (pz.MonRace == Grobal2.RC_FEATHERKINGOFKING)
                                {
                                    svMain.MainOutMessage("RegenMon Nil : ÈæÃµ¸¶¿Õ-NIL");
                                    svMain.MainOutMessage(pz.MapName + " " + zzx.ToString() + "," + zzy.ToString() + " " + pz.MonRace.ToString() + " " + pz.MonName);
                                }
                                if (pz.MonRace == Grobal2.RC_PBKING)
                                {
                                    svMain.MainOutMessage("RegenMon Nil : ÆÄÈ²¸¶½Å-NIL");
                                    svMain.MainOutMessage(pz.MapName + " " + zzx.ToString() + "," + zzy.ToString() + " " + pz.MonRace.ToString() + " " + pz.MonName);
                                }
                            }
                            if (HUtil32.GetTickCount() - start > svMain.ZenLimitTime)
                            {
                                result = false;
                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[TUserEngine] RegenMonsters exception");
            }
            return result;
        }

        public int GetMonCount(TZenInfo pz)
        {
            int n = 0;
            for (var i = 0; i < pz.Mons.Count; i++)
            {
                if (!((TCreature)pz.Mons[i]).Death && !((TCreature)pz.Mons[i]).BoGhost)
                {
                    n++;
                }
            }
            return n;
        }

        public int GetGenCount(string mapname)
        {
            TZenInfo pz;
            int count = 0;
            for (var i = 0; i < MonList.Count; i++)
            {
                pz = (TZenInfo)MonList[i];
                if (pz != null)
                {
                    if (pz.MapName.ToLower().CompareTo(mapname.ToLower()) == 0)
                    {
                        count = count + GetMonCount(pz);
                    }
                }
            }
            return count;
        }

        public int GetMapMons(TEnvirnoment penvir, ArrayList list)
        {
            int count = 0;
            if (penvir == null)
            {
                return 0;
            }
            for (var i = 0; i < MonList.Count; i++)
            {
                TZenInfo pz = (TZenInfo)MonList[i];
                if (pz != null)
                {
                    for (var k = 0; k < pz.Mons.Count; k++)
                    {
                        TCreature cret = (TCreature)pz.Mons[k];
                        if (!cret.BoGhost && !cret.Death && (cret.PEnvir == penvir))
                        {
                            if (list != null)
                            {
                                list.Add(cret);
                            }
                            count++;
                        }
                    }
                }
            }
            return count;
        }

        public int GetMapMonsNoRecallMob(TEnvirnoment penvir, ArrayList list)
        {
            int result;
            int i;
            int k;
            int count;
            TZenInfo pz;
            TCreature cret;
            count = 0;
            result = 0;
            if (penvir == null)
            {
                return result;
            }
            for (i = 0; i < MonList.Count; i++)
            {
                pz = (TZenInfo)MonList[i];
                if (pz != null)
                {
                    for (k = 0; k < pz.Mons.Count; k++)
                    {
                        cret = (TCreature)pz.Mons[k];
                        if (!cret.BoGhost && !cret.Death && (cret.PEnvir == penvir) && (cret.Master == null))
                        {
                            if (list != null)
                            {
                                list.Add(cret);
                            }
                            count++;
                        }
                    }
                }
            }
            result = count;
            return result;
        }

        public TDefMagic GetDefMagic(string magname)
        {
            TDefMagic result = null;
            for (var i = 0; i < DefMagicList.Count; i++)
            {
                if (DefMagicList[i].MagicName.ToLower().CompareTo(magname.ToLower()) == 0)
                {
                    result = DefMagicList[i];
                    break;
                }
            }
            return result;
        }

        public TDefMagic GetDefMagicFromId(int Id)
        {
            TDefMagic result;
            int i;
            result = null;
            for (i = 0; i < DefMagicList.Count; i++)
            {
                if (DefMagicList[i].MagicId == Id)
                {
                    result = DefMagicList[i];
                    break;
                }
            }
            return result;
        }

        // User
        // ---------------------------------------------------------
        public void AddNewUser(TUserOpenInfo ui)
        {
            // (hum: TUserHuman);
            try
            {
                svMain.usLock.Enter();
                ReadyList.Add(ui.Name, ui as Object);
            }
            finally
            {
                svMain.usLock.Leave();
            }
        }

        // ½ÌÅ©¸Â¿ö¾ßÇÔ
        public void ClosePlayer(TUserHuman hum)
        {
            hum.GhostTime = HUtil32.GetTickCount();
            ClosePlayers.Add(hum);
        }

        public void SavePlayer(TUserHuman hum)
        {
            TSaveRcd p = new TSaveRcd();
            //FillChar(p, sizeof(TSaveRcd), 0);
            p.uid = hum.UserId;
            p.uname = hum.UserName;
            p.certify = hum.Certification;
            p.hum = hum;
            RunDB.FDBMakeHumRcd(hum, p.rcd);
            int savelistcount = svMain.FrontEngine.AddSavePlayer(p);
        }

        public void ChangeAndSaveOk(TChangeUserInfo pc)
        {
            TChangeUserInfo pcu = new TChangeUserInfo();
            pcu = pc;
            // »õ·Î º¹»çÇØ¾ß ÇÔ
            try
            {
                svMain.usLock.Enter();
                SaveChangeOkList.Add(pcu);
            }
            finally
            {
                svMain.usLock.Leave();
            }
        }

        public int GetMyDegree(string uname)
        {
            int result;
            int i;
            result = Grobal2.UD_USER;
            for (i = 0; i < AdminList.Count; i++)
            {
                if (AdminList[i].ToLower().CompareTo(uname.ToLower()) == 0)
                {
                    result = (int)AdminList.Values[i];
                    break;
                }
            }
            return result;
        }

        public TUserHuman GetUserHuman(string who)
        {
            TUserHuman result;
            int i;
            result = null;
            for (i = 0; i < RunUserList.Count; i++)
            {
                if (RunUserList[i].ToLower().CompareTo(who.ToLower()) == 0)
                {
                    if (!((TUserHuman)RunUserList.Values[i]).BoGhost)
                    {
                        result = (TUserHuman)RunUserList.Values[i];
                        break;
                    }
                }
            }
            return result;
        }

        public bool FindOtherServerUser(string who, ref int svindex)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < OtherUserNameList.Count; i++)
            {
                if (OtherUserNameList[i].ToLower().CompareTo(who.ToLower()) == 0)
                {
                    svindex = (int)OtherUserNameList.Values[i];
                    result = true;
                    break;
                }
            }
            return result;
        }

        // ´Ù¸¥¼­¹ö¿¡ Á¢¼ÓÇÏ°í ÀÖ´ÂÁö
        public int GetUserCount()
        {
            int result;
            result = RunUserList.Count + OtherUserNameList.Count;
            return result;
        }

        public int GetRealUserCount()
        {
            int result;
            result = RunUserList.Count;
            return result;
        }

        public int GetAreaUserCount(TEnvirnoment env, int x, int y, int wide)
        {
            int result;
            int i;
            int n;
            TUserHuman hum;
            n = 0;
            for (i = 0; i < RunUserList.Count; i++)
            {
                hum = (TUserHuman)RunUserList.Values[i];
                if ((!hum.BoGhost) && (hum.PEnvir == env))
                {
                    if ((Math.Abs(hum.CX - x) < wide) && (Math.Abs(hum.CY - y) < wide))
                    {
                        n++;
                    }
                }
            }
            result = n;
            return result;
        }

        // ´©¿öÀÖ´Â »ç¶÷±îÁö ¸®½ºÆ® º¸³¿
        public int GetAreaUsers(TEnvirnoment env, int x, int y, int wide, ArrayList ulist)
        {
            int result;
            int i;
            int n;
            TUserHuman hum;
            n = 0;
            for (i = 0; i < RunUserList.Count; i++)
            {
                hum = (TUserHuman)RunUserList.Values[i];
                if ((!hum.BoGhost) && (hum.PEnvir == env))
                {
                    if ((Math.Abs(hum.CX - x) < wide) && (Math.Abs(hum.CY - y) < wide))
                    {
                        ulist.Add(hum);
                        n++;
                    }
                }
            }
            result = n;
            return result;
        }

        public int GetAreaAllUsers(TEnvirnoment env, ArrayList ulist)
        {
            int result;
            int i;
            int n;
            TUserHuman hum;
            n = 0;
            for (i = 0; i < RunUserList.Count; i++)
            {
                hum = (TUserHuman)RunUserList.Values[i];
                if ((!hum.BoGhost) && (hum.PEnvir == env))
                {
                    ulist.Add(hum);
                    n++;
                }
            }
            result = n;
            return result;
        }

        public int GetHumCount(string mapname)
        {
            int result;
            int i;
            int n;
            TUserHuman hum;
            n = 0;
            for (i = 0; i < RunUserList.Count; i++)
            {
                hum = (TUserHuman)RunUserList.Values[i];
                if ((!hum.BoGhost) && (!hum.Death) && (hum.PEnvir.MapName.ToLower().CompareTo(mapname.ToLower()) == 0))
                {
                    n++;
                }
            }
            result = n;
            return result;
        }

        public void CryCry(int msgtype, TEnvirnoment env, int x, int y, int wide, string saying)
        {
            int i;
            TUserHuman hum;
            for (i = 0; i < RunUserList.Count; i++)
            {
                hum = (TUserHuman)RunUserList.Values[i];
                if ((!hum.BoGhost) && (hum.PEnvir == env) && hum.BoHearCry)
                {
                    if ((Math.Abs(hum.CX - x) < wide) && (Math.Abs(hum.CY - y) < wide))
                    {
                        hum.SendMsg(null, msgtype, 0, System.Drawing.Color.Black, System.Drawing.Color.Yellow, 0, saying);
                    }
                }
            }
        }

        public void GuildAgitCry(int msgtype, TEnvirnoment env, int x, int y, int wide, string saying)
        {
            int i;
            TUserHuman hum;
            for (i = 0; i < RunUserList.Count; i++)
            {
                hum = (TUserHuman)RunUserList.Values[i];
                if ((!hum.BoGhost) && hum.BoHearCry)
                {
                    if ((env.GetGuildAgitRealMapName() == svMain.GuildAgitMan.GuildAgitMapName[0]) || (env.GetGuildAgitRealMapName() == svMain.GuildAgitMan.GuildAgitMapName[1]) || (env.GetGuildAgitRealMapName() == svMain.GuildAgitMan.GuildAgitMapName[2]) || (env.GetGuildAgitRealMapName() == svMain.GuildAgitMan.GuildAgitMapName[3]))
                    {
                        if (hum.PEnvir.GuildAgit == env.GuildAgit)
                        {
                            hum.SysMsg(saying, 2);
                        }
                    }
                }
            }
        }

        public void SysMsgAll(string saying)
        {
            int i;
            TUserHuman hum;
            for (i = 0; i < RunUserList.Count; i++)
            {
                hum = (TUserHuman)RunUserList.Values[i];
                if (!hum.BoGhost)
                {
                    hum.SysMsg(saying, 0);
                }
            }
        }

        public void KickDoubleConnect(string uname)
        {
            int i;
            for (i = 0; i < RunUserList.Count; i++)
            {
                if (RunUserList[i].ToLower().CompareTo(uname.ToLower()) == 0)
                {
                    ((TUserHuman)RunUserList.Values[i]).UserRequestClose = true;
                    break;
                }
            }
        }

        public void GuildMemberReLogin(TGuild guild)
        {
            int i;
            int n;
            for (i = 0; i < RunUserList.Count; i++)
            {
                if (((TUserHuman)RunUserList.Values[i]).MyGuild == guild)
                {
                    guild.MemberLogin((TUserHuman)RunUserList.Values[i], ref n);
                }
            }
        }

        // ´Ù¸¥ ¼­¹ö·ÎºÎÅÍ ´ë±âÀÚ¸¦ ¹ÞÀ½
        public bool AddServerWaitUser(TServerShiftUserInfo psui)
        {
            bool result;
            psui.waittime = HUtil32.GetTickCount();
            WaitServerList.Add(psui);
            result = true;
            return result;
        }

        public void CheckServerWaitTimeOut()
        {
            int i;
            for (i = WaitServerList.Count - 1; i >= 0; i--)
            {
                if (HUtil32.GetTickCount() - ((TServerShiftUserInfo)WaitServerList[i]).waittime > 30 * 1000)
                {
                    Dispose((TServerShiftUserInfo)WaitServerList[i]);
                    WaitServerList.RemoveAt(i);
                }
            }
        }

        public void CheckHolySeizeValid()
        {
            // °á°è°¡ ±úÁ³´ÂÁö °Ë»çÇÑ´Ù.
            int i;
            int k;
            THolySeizeInfo phs;
            TCreature cret;
            for (i = HolySeizeList.Count - 1; i >= 0; i--)
            {
                phs = (THolySeizeInfo)HolySeizeList[i];
                if (phs != null)
                {
                    for (k = phs.seizelist.Count - 1; k >= 0; k--)
                    {
                        // °á°è¿¡ °É¸° ¸ó½ºÅÍ°¡ Á×¾ú°Å³ª, Ç®·È´ÂÁö °Ë»ç
                        cret = (TCreature)phs.seizelist[k];
                        if (cret.Death || cret.BoGhost || (!cret.BoHolySeize))
                        {
                            phs.seizelist.RemoveAt(k);
                        }
                    }
                    // °á°è¿¡ ÀâÀÎ ¸÷ÀÌ ¾ø°Å³ª, 3ºÐÀÌ °æ°úÇÑ °æ¿ì, (°á°èÀÇ Á¦ÇÑ ½Ã°£Àº 3ºÐÀÌ´Ù)
                    if ((phs.seizelist.Count <= 0) || (HUtil32.GetTickCount() - phs.OpenTime > phs.SeizeTime) || (HUtil32.GetTickCount() - phs.OpenTime > 3 * 60 * 1000))
                    {
                        phs.seizelist.Free();
                        for (k = 0; k <= 7; k++)
                        {
                            if (phs.earr[k] != null)
                            {
                                ((TEvent)phs.earr[k]).Close();
                            }
                        }
                        Dispose(phs);
                        HolySeizeList.RemoveAt(i);
                    }
                }
            }
        }

        public TServerShiftUserInfo GetServerShiftInfo(string uname, int certify)
        {
            TServerShiftUserInfo result;
            int i;
            result = null;
            for (i = 0; i < WaitServerList.Count; i++)
            {
                if ((((TServerShiftUserInfo)WaitServerList[i]).UserName.ToLower().CompareTo(uname.ToLower()) == 0) && (((TServerShiftUserInfo)WaitServerList[i]).Certification == certify))
                {
                    result = (TServerShiftUserInfo)WaitServerList[i];
                    break;
                }
            }
            return result;
        }

        public void MakeServerShiftData(TUserHuman hum, ref TServerShiftUserInfo sui)
        {
            int i;
            TCreature cret;
            //FillChar(sui, sizeof(TServerShiftUserInfo), '\0');
            sui.UserName = hum.UserName;
            RunDB.FDBMakeHumRcd(hum, sui.rcd);
            sui.Certification = hum.Certification;
            if (hum.GroupOwner != null)
            {
                sui.GroupOwner = hum.GroupOwner.UserName;
                for (i = 0; i < hum.GroupOwner.GroupMembers.Count; i++)
                {
                    sui.GroupMembers[i] = hum.GroupOwner.GroupMembers[i];
                }
            }
            sui.BoHearCry = hum.BoHearCry;
            sui.BoHearWhisper = hum.BoHearWhisper;
            sui.BoHearGuildMsg = hum.BoHearGuildMsg;
            sui.BoSysopMode = hum.BoSysopMode;
            sui.BoSuperviserMode = hum.BoSuperviserMode;
            sui.BoSlaveRelax = hum.BoSlaveRelax;
            for (i = 0; i < hum.WhisperBlockList.Count; i++)
            {
                if (i <= 9)
                {
                    sui.WhisperBlockNames[i] = (string)hum.WhisperBlockList[i];
                }
            }
            for (i = 0; i < hum.SlaveList.Count; i++)
            {
                cret = (TCreature)hum.SlaveList[i];
                if (i <= 4)
                {
                    sui.Slaves[i].SlaveName = cret.UserName;
                    sui.Slaves[i].SlaveExp = cret.SlaveExp;
                    sui.Slaves[i].SlaveExpLevel = cret.SlaveExpLevel;
                    sui.Slaves[i].SlaveMakeLevel = cret.SlaveMakeLevel;
                    sui.Slaves[i].RemainRoyalty = (int)((cret.MasterRoyaltyTime - GetTickCount) / 1000);
                    sui.Slaves[i].HP = cret.WAbil.HP;
                    sui.Slaves[i].MP = cret.WAbil.MP;
                }
            }
            // Ãß°¡ (sonmg 2005/06/03)
            for (i = 0; i < Grobal2.STATUSARR_SIZE; i++)
            {
                sui.StatusValue[i] = hum.StatusValue[i];
            }
            for (i = 0; i < Grobal2.EXTRAABIL_SIZE; i++)
            {
                sui.ExtraAbil[i] = hum.ExtraAbil[i];
                if (hum.ExtraAbilTimes[i] > GetTickCount)
                {
                    // ³²Àº ½Ã°£¸¸ ÀúÀåÇÔ
                    sui.ExtraAbilTimes[i] = hum.ExtraAbilTimes[i] - GetTickCount;
                }
                else
                {
                    sui.ExtraAbilTimes[i] = 0;
                }
            }
        }

        public void LoadServerShiftData(TServerShiftUserInfo psui, ref TUserHuman hum)
        {
            int i;
            TSlaveInfo pslave;
            if (psui.GroupOwner != "")
            {
                // ±×·ìÃ³¸®´Â ´ÙÀ½¿¡ ÇÑ´Ù. (º¹ÀâÇÏ´Ù)
            }
            hum.BoHearCry = psui.BoHearCry;
            hum.BoHearWhisper = psui.BoHearWhisper;
            hum.BoHearGuildMsg = psui.BoHearGuildMsg;
            hum.BoSysopMode = psui.BoSysopMode;
            hum.BoSuperviserMode = psui.BoSuperviserMode;
            hum.BoSlaveRelax = psui.BoSlaveRelax;
            // (sonmg 2005/01/21)
            for (i = 0; i <= 9; i++)
            {
                if (psui.WhisperBlockNames[i] != "")
                {
                    hum.WhisperBlockList.Add(psui.WhisperBlockNames[i]);
                    break;
                }
            }
            for (i = 0; i <= 4; i++)
            {
                if (psui.Slaves[i].SlaveName != "")
                {
                    pslave = new TSlaveInfo();
                    pslave = psui.Slaves[i];
                    // 2003/06/12 ½½·¹ÀÌºê ÆÐÄ¡
                    hum.PrevServerSlaves.Add(pslave);
                    // ½º·¹µå¿¡ ¾ÈÀüÇÏÁö ¾ÊÀ½
                    // hum.SendDelayMsg(hum, RM_MAKE_SLAVE, 0, integer(pslave), 0, 0, '', 500);
                }
            }
            for (i = 0; i < Grobal2.EXTRAABIL_SIZE; i++)
            {
                hum.ExtraAbil[i] = psui.ExtraAbil[i];
                if (psui.ExtraAbilTimes[i] > 0)
                {
                    // ÀúÀåµÈ ½Ã°£Àº ³²Àº ½Ã°£ÀÓ
                    hum.ExtraAbilTimes[i] = (int)(psui.ExtraAbilTimes[i] + GetTickCount);
                }
                else
                {
                    hum.ExtraAbilTimes[i] = 0;
                }
            }
        }

        public void ClearServerShiftData(TServerShiftUserInfo psui)
        {
            int i;
            for (i = 0; i < WaitServerList.Count; i++)
            {
                if (((TServerShiftUserInfo)WaitServerList[i]) == psui)
                {
                    Dispose((TServerShiftUserInfo)WaitServerList[i]);
                    WaitServerList.RemoveAt(i);
                    break;
                }
            }
        }

        public string WriteShiftUserData(TServerShiftUserInfo psui)
        {
            string result;
            string flname;
            int i;
            int fhandle;
            int checksum;
            long shifttime;
            shifttime = HUtil32.GetTickCount();
            result = "";
            flname = "$_" + svMain.ServerIndex.ToString() + "_$_" + svMain.ShareFileNameNum.ToString() + ".shr";
            svMain.ShareFileNameNum++;
            try
            {
                checksum = 0;
                for (i = 0; i < sizeof(TServerShiftUserInfo); i++)
                {
                    checksum = checksum + ((byte)psui + i);
                }
                fhandle = File.Create(svMain.ShareBaseDir + flname);
                if (fhandle > 0)
                {
                    FileWrite(fhandle, psui, sizeof(TServerShiftUserInfo));
                    FileWrite(fhandle, checksum, sizeof(int));
                    fhandle.Close();
                    result = flname;
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] WriteShiftUserData..");
            }
            return result;
        }

        public void SendInterServerMsg(string msgstr)
        {
            svMain.usIMLock.Enter();
            try
            {
                if (svMain.ServerIndex == 0)
                {
                    // ¸¶½ºÅÍ ¼­¹öÀÎ°æ¿ì
                    InterServerMsg.FrmSrvMsg.SendServerSocket(msgstr);
                }
                else
                {
                    // ½½·¡ÀÌºê ¼­¹öÀÎ°æ¿ì
                    InterMsgClient.FrmMsgClient.SendSocket(msgstr);
                }
            }
            finally
            {
                svMain.usIMLock.Leave();
            }
        }

        // °°Àº ¼­¹ö±º¿¡¼­ ¼­¹öµé »çÀÌÀÇ ¸Þ¼¼Áö Àü´Þ
        public void SendInterMsg(int ident, int svidx, string msgstr)
        {
            svMain.usIMLock.Enter();
            try
            {
                if (svMain.ServerIndex == 0)
                {
                    // ¸¶½ºÅÍ ¼­¹öÀÎ°æ¿ì
                    InterServerMsg.FrmSrvMsg.SendServerSocket(ident.ToString() + "/" + EDcode.EncodeString(svidx.ToString()) + "/" + EDcode.EncodeString(msgstr));
                }
                else
                {
                    // ½½·¡ÀÌºê ¼­¹öÀÎ°æ¿ì
                    InterMsgClient.FrmMsgClient.SendSocket(ident.ToString() + "/" + EDcode.EncodeString(svidx.ToString()) + "/" + EDcode.EncodeString(msgstr));
                }
            }
            finally
            {
                svMain.usIMLock.Leave();
            }
        }

        public bool UserServerChange(TUserHuman hum, int svindex)
        {
            bool result;
            string flname;
            TServerShiftUserInfo sui;
            result = false;
            MakeServerShiftData(hum, ref sui);
            flname = WriteShiftUserData(sui);
            if (flname != "")
            {
                hum.TempStr = flname;
                // ³ªÁß¿¡ ÀÌµ¿ÇÏ·Á´Â ¼­¹ö¿¡¼­ Àß ¹Þ¾Ò´ÂÁö È®ÀÎÇÏ´Âµ¥ ¾²ÀÓ
                SendInterServerMsg(Grobal2.ISM_USERSERVERCHANGE.ToString() + "/" + EDcode.EncodeString(svindex.ToString()) + "/" + EDcode.EncodeString(flname));
                result = true;
            }
            return result;
        }

        // 2003/06/12 ½½·¹ÀÌºê ÆÐÄ¡
        public void GetISMChangeServerReceive(string flname)
        {
            int i;
            TUserHuman hum;
            for (i = 0; i < ClosePlayers.Count; i++)
            {
                hum = (TUserHuman)ClosePlayers[i];
                if (hum.TempStr == flname)
                {
                    hum.BoChangeServerOK = true;
                    break;
                }
            }
        }

        public bool DoUserChangeServer(TUserHuman hum, int svindex)
        {
            bool result;
            string naddr;
            int nport;
            result = false;
            // Å¬¶óÀÌ¾ðÆ®¿¡ ´ÙÀ½ÀÇ ÀçÁ¢¼Ó ÁÖ¼Ò¿Í Æ÷Æ®·Î ÀçÁ¢¼ÓÀ» À¯µµÇÑ´Ù.
            if (svMain.GetMultiServerAddrPort((byte)svindex, ref naddr, ref nport))
            {
                hum.SendDefMessage(Grobal2.SM_RECONNECT, 0, 0, 0, 0, naddr + "/" + nport.ToString());
                result = true;
            }
            return result;
        }

        public void OtherServerUserLogon(int snum, string uname)
        {
            int i;
            string name;
            string apmode;
            apmode = HUtil32.GetValidStr3(uname, ref name, new string[] { ":" });
            for (i = OtherUserNameList.Count - 1; i >= 0; i--)
            {
                if (OtherUserNameList[i].ToLower().CompareTo(name.ToLower()) == 0)
                {
                    OtherUserNameList.Remove(i);
                }
            }
            OtherUserNameList.Add(name, snum as Object);
            if (HUtil32.Str_ToInt(apmode, 0) == 1)
            {
                FreeUserCount++;
            }
            // TO_PDS: Add User To UserMgr When Other Server Login...
            svMain.UserMgrEngine.AddUser(name, 0, snum + 4, 0, 0, 0);
        }

        public void OtherServerUserLogout(int snum, string uname)
        {
            int i;
            string name;
            string apmode;
            apmode = HUtil32.GetValidStr3(uname, ref name, new string[] { ":" });
            for (i = 0; i < OtherUserNameList.Count; i++)
            {
                if ((OtherUserNameList[i].ToLower().CompareTo(name.ToLower()) == 0) && (((int)OtherUserNameList.Values[i]) == snum))
                {
                    OtherUserNameList.Remove(i);
                    // TO_PDS: Add User To UserMgr When Other Server Login...
                    svMain.UserMgrEngine.DeleteUser(name);
                    break;
                }
            }
            // 3
            if (HUtil32.Str_ToInt(apmode, 0) == 1)
            {
                FreeUserCount -= 1;
            }
        }

        public void AccountExpired(string uid)
        {
            int i;
            for (i = 0; i < RunUserList.Count; i++)
            {
                if (((TUserHuman)RunUserList.Values[i]).UserId.ToLower().CompareTo(uid.ToLower()) == 0)
                {
                    ((TUserHuman)RunUserList.Values[i]).BoAccountExpired = true;
                    break;
                }
            }
        }

        public bool TimeAccountExpired(string uid)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < RunUserList.Count; i++)
            {
                if (((TUserHuman)RunUserList.Values[i]).UserId.ToLower().CompareTo(uid.ToLower()) == 0)
                {
                    result = ((TUserHuman)RunUserList.Values[i]).SetExpiredTime(5);
                    // ºÐ ÀÔ·Â
                    break;
                }
            }
            return result;
        }

        public bool ProcessUserHumans_OnUse(string uname)
        {
            bool result;
            int k;
            result = false;
            if (svMain.FrontEngine.IsDoingSave(uname))
            {
                // ¾ÆÁ÷ ÀúÀåÀÌ Ã¤ µÇÁö ¾Ê¾ÒÀ½
                result = true;
                return result;
            }
            for (k = 0; k < RunUserList.Count; k++)
            {
                if (RunUserList[k].ToLower().CompareTo(uname.ToLower()) == 0)
                {
                    // ÇöÀç Á¢¼ÓÁß
                    result = true;
                    break;
                }
            }
            return result;
        }

        public TUserHuman ProcessUserHumans_MakeNewHuman(TUserOpenInfo pui)
        {
            TUserHuman result;
            int i;
            TEnvirnoment mapenvir;
            TUserHuman hum;
            string hmap;
            TServerShiftUserInfo pshift;
            result = null;
            try
            {
                hum = new TUserHuman();
                if (hum == null)
                {
                    svMain.MainOutMessage("[TUserEngine.ProcessUserHumans]TUserHuman.Create Error");
                }
                if (!svMain.BoVentureServer)
                {
                    // ¼­¹öÀÌµ¿ÁßÀÎ µ¥ÀÌÅ¸°¡ ÀÖÀ¸¸é °¡Á®¿Â´Ù.
                    pshift = GetServerShiftInfo(pui.Name, pui.readyinfo.Certification);
                }
                else
                {
                    pshift = null;
                    // ¸ðÇè¼­¹öÀÇ Shift Á¤º¸¸¦ ÀÐ´Â´Ù.
                }
                if (pshift == null)
                {
                    // ¼­¹ö ÀÌµ¿ÀÌ ¾Æ´Ô
                    RunDB.FDBLoadHuman(pui.rcd, ref hum);
                    hum.RaceServer = Grobal2.RC_USERHUMAN;
                    if (hum.HomeMap == "")
                    {
                    // ¾Æ¹«°Íµµ ¼³Á¤µÇ¾î ÀÖÁö ¾ÊÀ½...
                    ERROR_MAP:
                        GetRandomDefStart(ref hmap, ref (int)hum.HomeX, ref (int)hum.HomeY);
                        hum.HomeMap = hmap;
                        hum.MapName = hum.HomeMap;
                        // HomeMapÀ» ±âÁØÀ¸·Î
                        hum.CX = (short)hum.GetStartX();
                        hum.CY = (short)hum.GetStartY();
                        if (hum.Abil.Level == 0)
                        {
                            // ¾ÆÀÌµð¸¦ Ã³À½ ¸¸µç °æ¿ì
                            TAbility _wvar1 = hum.Abil;
                            _wvar1.Level = 1;
                            _wvar1.AC = 0;
                            _wvar1.MAC = 0;
                            _wvar1.DC = HUtil32.MakeWord(1, 2);
                            _wvar1.MC = HUtil32.MakeWord(1, 2);
                            _wvar1.SC = HUtil32.MakeWord(1, 2);
                            _wvar1.MP = 15;
                            _wvar1.HP = 15;
                            _wvar1.MaxHP = 15;
                            _wvar1.MaxMP = 15;
                            _wvar1.Exp = 0;
                            _wvar1.MaxExp = 100;
                            _wvar1.Weight = 0;
                            _wvar1.MaxWeight = 30;
                            hum.FirstTimeConnection = true;
                        }
                    }
                    mapenvir = svMain.GrobalEnvir.ServerGetEnvir(svMain.ServerIndex, hum.MapName);
                    if (mapenvir != null)
                    {
                        // ¹®ÆÄ ´ë·Ã ÀÌº¥Æ® ¹æ¿¡ ÀÖ´Â °æ¿ì °Ë»ç
                        if (mapenvir.Fight3Zone)
                        {
                            // ¹®ÆÄ ´ë·Ã ÀÌº¥Æ® ¹æ¿¡ ÀÖÀ½.
                            // Á×Àº °æ¿ì
                            if (hum.Abil.HP <= 0)
                            {
                                if (hum.FightZoneDieCount < 3)
                                {
                                    hum.Abil.HP = hum.Abil.MaxHP;
                                    hum.Abil.MP = hum.Abil.MaxMP;
                                    hum.MustRandomMove = true;
                                }
                            }
                        }
                        else
                        {
                            hum.FightZoneDieCount = 0;
                        }
                    }
                    hum.MyGuild = svMain.GuildMan.GetGuildFromMemberName(hum.UserName);
                    if (mapenvir != null)
                    {
                        if ((svMain.UserCastle.CorePEnvir == mapenvir) || (svMain.UserCastle.BoCastleUnderAttack && svMain.UserCastle.IsCastleWarArea(mapenvir, hum.CX, hum.CY)))
                        {
                            if (!svMain.UserCastle.IsCastleMember(hum))
                            {
                                hum.MapName = hum.HomeMap;
                                hum.CX = (short)(hum.HomeX - 2 + new System.Random(5).Next());
                                hum.CY = (short)(hum.HomeY - 2 + new System.Random(5).Next());
                            }
                            else
                            {
                                if (svMain.UserCastle.CorePEnvir == mapenvir)
                                {
                                    hum.MapName = svMain.UserCastle.GetCastleStartMap();
                                    hum.CX = svMain.UserCastle.GetCastleStartX();
                                    hum.CY = svMain.UserCastle.GetCastleStartY();
                                }
                            }
                        }
                    }
                    if ((hum.DBVersion <= 1) && (hum.Abil.Level >= 1))
                    {
                        hum.DBVersion = 2;
                    }
#if FOR_ABIL_POINT
                    // 4/16ÀÏ ºÎÅÍ Àû¿ë
                    // º¸³Ê½º Æ÷ÀÎÆ®¸¦ Àû¿ëÇß´ÂÁö °Ë»ç
                    if (hum.BonusApply <= 3)
                    {
                        hum.BonusApply = 4;
                        hum.BonusPoint = M2Share.GetLevelBonusSum(hum.Job, hum.Abil.Level);
                                                FillChar(hum.BonusAbil, sizeof(TNakedAbility), '\0');
                                                FillChar(hum.CurBonusAbil, sizeof(TNakedAbility), '\0');
                        hum.MapName = hum.HomeMap;
                        // ¸¶À»¿¡¼­ ½ÃÀÛÇÏ°Ô ÇÑ´Ù. (Ã¼·ÂÀÌ ¶³¾îÁ® ÀÖ±â ¶§¹®¿¡)
                        hum.CX = hum.HomeX - 2 + (new System.Random(5)).Next();
                        hum.CY = hum.HomeY - 2 + (new System.Random(5)).Next();
                    }
#endif
                    if (svMain.GrobalEnvir.GetEnvir(hum.MapName) == null)
                    {
                        hum.Abil.HP = 0;
                    }
                    if (hum.Abil.HP <= 0)
                    {
                        hum.ResetCharForRevival();
                        if (hum.PKLevel() < 2)
                        {
                            if (svMain.UserCastle.BoCastleUnderAttack && svMain.UserCastle.IsCastleMember(hum))
                            {
                                hum.MapName = svMain.UserCastle.CastleMap;
                                hum.CX = svMain.UserCastle.GetCastleStartX();
                                hum.CY = svMain.UserCastle.GetCastleStartY();
                            }
                            else
                            {
                                hum.MapName = hum.HomeMap;
                                hum.CX = (short)(hum.HomeX - 2 + new System.Random(5).Next());
                                hum.CY = (short)(hum.HomeY - 2 + new System.Random(5).Next());
                            }
                        }
                        else
                        {
                            hum.MapName = M2Share.BADMANHOMEMAP;
                            hum.CX = (short)(M2Share.BADMANSTARTX - 6 + new System.Random(13).Next());
                            hum.CY = (short)(M2Share.BADMANSTARTY - 6 + new System.Random(13).Next());
                        }
                        hum.Abil.HP = 14;
                    }
                    hum.InitValues();
                    mapenvir = svMain.GrobalEnvir.ServerGetEnvir(svMain.ServerIndex, hum.MapName);
                    if (mapenvir == null)
                    {
                        hum.Certification = pui.readyinfo.Certification;
                        hum.UserHandle = pui.readyinfo.Shandle;
                        hum.GateIndex = pui.readyinfo.GateIndex;
                        hum.UserGateIndex = pui.readyinfo.UserGateIndex;
                        hum.WAbil = hum.Abil;
                        hum.ChangeToServerNumber = svMain.GrobalEnvir.GetServer(hum.MapName);
                        if (hum.Abil.HP != 14)
                        {
                            svMain.MainOutMessage("chg-server-fail-1 [" + svMain.ServerIndex.ToString() + "] -> [" + hum.ChangeToServerNumber.ToString() + "] [" + hum.MapName);
                        }
                        UserServerChange(hum, hum.ChangeToServerNumber);
                        DoUserChangeServer(hum, hum.ChangeToServerNumber);
                        hum.Free();
                        return result;
                    }
                    else
                    {
                        // ÇöÀç ¼­¹ö¿¡ Á¢¼Ó..
                        for (i = 0; i <= 4; i++)
                        {
                            if (!mapenvir.CanWalk(hum.CX, hum.CY, true))
                            {
                                hum.CX = (short)(hum.CX - 3 + new System.Random(6).Next());
                                hum.CY = (short)(hum.CY - 3 + new System.Random(6).Next());
                            }
                            else
                            {
                                break;
                            }
                        }
                        if (!mapenvir.CanWalk(hum.CX, hum.CY, true))
                        {
                            svMain.MainOutMessage("chg-server-fail-2 [" + svMain.ServerIndex.ToString() + "] " + hum.CX.ToString() + ":" + hum.CY.ToString() + " [" + hum.MapName);
                            hum.MapName = svMain.DefHomeMap;
                            mapenvir = svMain.GrobalEnvir.GetEnvir(svMain.DefHomeMap);
                            hum.CX = svMain.DefHomeX;
                            hum.CY = svMain.DefHomeY;
                        }
                    }
                    hum.PEnvir = mapenvir;
                    if (hum.PEnvir == null)
                    {
                        svMain.MainOutMessage("[Error] hum.PEnvir = nil");
                        goto ERROR_MAP;
                    }
                    hum.ReadyRun = false;
                }
                else
                {
                    RunDB.FDBLoadHuman(pshift.rcd, ref hum);
                    hum.MapName = pshift.rcd.Block.DBHuman.MapName;
                    hum.CX = pshift.rcd.Block.DBHuman.CX;
                    hum.CY = pshift.rcd.Block.DBHuman.CY;
                    hum.Abil.Level = pshift.rcd.Block.DBHuman.Abil_Level;
                    hum.Abil.HP = pshift.rcd.Block.DBHuman.Abil_HP;
                    hum.Abil.MP = pshift.rcd.Block.DBHuman.Abil_MP;
                    hum.Abil.Exp = pshift.rcd.Block.DBHuman.Abil_EXP;
                    LoadServerShiftData(pshift, ref hum);
                    ClearServerShiftData(pshift);
                    mapenvir = svMain.GrobalEnvir.ServerGetEnvir(svMain.ServerIndex, hum.MapName);
                    if (mapenvir == null)
                    {
                        svMain.MainOutMessage("chg-server-fail-3 [" + svMain.ServerIndex.ToString() + "]  " + hum.CX.ToString() + ":" + hum.CY.ToString() + " [" + hum.MapName);
                        hum.MapName = svMain.DefHomeMap;
                        mapenvir = svMain.GrobalEnvir.GetEnvir(svMain.DefHomeMap);
                        hum.CX = svMain.DefHomeX;
                        hum.CY = svMain.DefHomeY;
                    }
                    else
                    {
                        if (!mapenvir.CanWalk(hum.CX, hum.CY, true))
                        {
                            svMain.MainOutMessage("chg-server-fail-4 [" + svMain.ServerIndex.ToString() + "]  " + hum.CX.ToString() + ":" + hum.CY.ToString() + " [" + hum.MapName);
                            hum.MapName = svMain.DefHomeMap;
                            mapenvir = svMain.GrobalEnvir.GetEnvir(svMain.DefHomeMap);
                            hum.CX = svMain.DefHomeX;
                            hum.CY = svMain.DefHomeY;
                        }
                    }
                    hum.InitValues();
                    hum.PEnvir = mapenvir;
                    if (hum.PEnvir == null)
                    {
                        svMain.MainOutMessage("[Error] hum.PEnvir = nil");
                        goto ERROR_MAP;
                    }
                    hum.ReadyRun = false;
                    hum.LoginSign = true;
                    hum.BoServerShifted = true;
                }
                hum.UserId = pui.readyinfo.UserId;
                hum.UserAddress = pui.readyinfo.UserAddress;
                hum.UserHandle = pui.readyinfo.Shandle;
                hum.UserGateIndex = pui.readyinfo.UserGateIndex;
                hum.GateIndex = pui.readyinfo.GateIndex;
                hum.Certification = pui.readyinfo.Certification;
                hum.ApprovalMode = pui.readyinfo.ApprovalMode;
                hum.AvailableMode = pui.readyinfo.AvailableMode;
                hum.UserConnectTime = pui.readyinfo.ReadyStartTime;
                hum.ClientVersion = pui.readyinfo.ClientVersion;
                hum.LoginClientVersion = pui.readyinfo.LoginClientVersion;
                hum.ClientCheckSum = pui.readyinfo.ClientCheckSum;
                result = hum;
            }
            catch
            {
                svMain.MainOutMessage("[TUserEngine] MakeNewHuman exception");
            }
            return result;
        }

        protected void ProcessUserHumans()
        {
            int i;
            long k;
            long start;
            int tcount;
            TUserOpenInfo pui;
            TChangeUserInfo pc;
            TUserHuman hum;
            ArrayList newlist;
            ArrayList cuglist;
            ArrayList cuhlist;
            int bugcount;
            bool lack;
            bugcount = 0;
            start = HUtil32.GetTickCount();
            if (HUtil32.GetTickCount() - hum200time > 200)
            {
                try
                {
                    hum200time = HUtil32.GetTickCount();
                    newlist = null;
                    cuglist = null;
                    cuhlist = null;
                    try
                    {
                        svMain.usLock.Enter();
                        // °ÔÀÓ ÁØºñ¸¦ ¸¶Ä£ À¯Àúµé...
                        for (i = 0; i < ReadyList.Count; i++)
                        {
                            if (!svMain.FrontEngine.HasServerHeavyLoad() && !ProcessUserHumans_OnUse((string)ReadyList[i]))
                            {
                                pui = (TUserOpenInfo)ReadyList.Values[i];
                                hum = ProcessUserHumans_MakeNewHuman(pui);
                                if (hum != null)
                                {
                                    RunUserList.Add(ReadyList[i], hum);
                                    SendInterMsg(Grobal2.ISM_USERLOGON, svMain.ServerIndex, hum.UserName + ":" + hum.ApprovalMode.ToString());
                                    if (hum.ApprovalMode == 1)
                                    {
                                        svMain.UserEngine.FreeUserCount++;
                                    }
                                    if (newlist == null)
                                    {
                                        newlist = new ArrayList();
                                    }
                                    newlist.Add(hum);
                                    // TO PDS Add To UserMgr ... 4 = Connext SercerIndex 0 ...
                                    svMain.UserMgrEngine.AddUser(hum.UserName, (int)hum, svMain.ServerIndex + 4, hum.GateIndex, hum.UserGateIndex, hum.UserHandle);
                                }
                            }
                            else
                            {
                                KickDoubleConnect((string)ReadyList[i]);
                                // //MainOutMessage ('[Dup] ' + ReadyList[i]); //Áßº¹Á¢¼Ó
                                if (cuglist == null)
                                {
                                    cuglist = new ArrayList();
                                    cuhlist = new ArrayList();
                                }
                                cuglist.Add(((TUserHuman)ReadyList.Values[i]).GateIndex as object);
                                // thread lockdownÀ» ÇÇÇÏ±â À§ÇØ¼­
                                cuhlist.Add(((TUserHuman)ReadyList.Values[i]).UserHandle as object);
                            }
                            Dispose((TUserOpenInfo)ReadyList.Values[i]);
                        }
                        ReadyList.Clear();
                        // º¯°æÀÌ ¿Ï·áµÈ ¸®½ºÆ®
                        for (i = 0; i < SaveChangeOkList.Count; i++)
                        {
                            pc = (TChangeUserInfo)SaveChangeOkList[i];
                            hum = GetUserHuman(pc.CommandWho);
                            if (hum != null)
                            {
                                hum.RCmdUserChangeGoldOk(pc.UserName, pc.ChangeGold);
                            }
                            Dispose(pc);
                        }
                        SaveChangeOkList.Clear();
                    }
                    finally
                    {
                        svMain.usLock.Leave();
                    }
                    if (newlist != null)
                    {
                        svMain.usLock.Enter();
                        try
                        {
                            for (i = 0; i < newlist.Count; i++)
                            {
                                hum = (TUserHuman)newlist[i];
                                svMain.RunSocket.UserLoadingOk(hum.GateIndex, hum.UserHandle, hum);
                            }
                        }
                        finally
                        {
                            svMain.usLock.Leave();
                        }
                        newlist.Free();
                    }
                    if (cuglist != null)
                    {
                        svMain.usLock.Enter();
                        try
                        {
                            for (i = 0; i < cuglist.Count; i++)
                            {
                                svMain.RunSocket.CloseUser((int)cuglist[i], (int)cuhlist[i]);
                            }
                        }
                        finally
                        {
                            svMain.usLock.Leave();
                        }
                        cuglist.Free();
                        cuhlist.Free();
                    }
                }
                catch
                {
                    svMain.MainOutMessage("[UsrEngn] Exception Ready, Save, Load... ");
                }
            }
            try
            {
                // 5ºÐ Áö³ª¸é Free ½ÃÅ´
                for (i = 0; i < ClosePlayers.Count; i++)
                {
                    hum = (TUserHuman)ClosePlayers[i];
                    if (HUtil32.GetTickCount() - hum.GhostTime > 5 * 60 * 1000)
                    {
                        try
                        {
                            ((TUserHuman)ClosePlayers[i]).Free();
                            // ÀÜ»óÀÌ ³²´Â´Ù¸é ¿¡·¯°¡³¯ ¼ö ÀÖ´Ù.
                        }
                        catch
                        {
                            svMain.MainOutMessage("[UsrEngn] ClosePlayer.Delete - Free");
                        }
                        ClosePlayers.RemoveAt(i);
                        break;
                    }
                    else
                    {
                        if (hum.BoChangeServer)
                        {
                            if (hum.BoSaveOk)
                            {
                                // ÀúÀåÀ» ÇÏ°í ³­ ÈÄ¿¡ ¼­¹ö ÀÌµ¿À» ½ÃÅ²´Ù.
                                if (UserServerChange(hum, hum.ChangeToServerNumber) || (hum.WriteChangeServerInfoCount > 20))
                                {
                                    hum.BoChangeServer = false;
                                    hum.BoChangeServerOK = false;
                                    hum.BoChangeServerNeedDelay = true;
                                    hum.ChangeServerDelayTime = HUtil32.GetTickCount();
                                }
                                else
                                {
                                    hum.WriteChangeServerInfoCount++;
                                }
                            }
                        }
                        if (hum.BoChangeServerNeedDelay)
                        {
                            if (hum.BoChangeServerOK || (HUtil32.GetTickCount() - hum.ChangeServerDelayTime > 10 * 1000))
                            {
                                hum.ClearAllSlaves();
                                // ºÎÇÏµéÀ» ¸ðµÎ ¾ø¾Ø´Ù.
                                hum.BoChangeServerNeedDelay = false;
                                DoUserChangeServer(hum, hum.ChangeToServerNumber);
                            }
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[UsrEngn] ClosePlayer.Delete");
            }
            lack = false;
            try
            {
                tcount = GetCurrentTime;
                i = HumCur;
                while (true)
                {
                    if (i >= RunUserList.Count)
                    {
                        break;
                    }
                    hum = (TUserHuman)RunUserList.Values[i];
                    if (tcount - hum.RunTime > hum.RunNextTick)
                    {
                        hum.RunTime = tcount;
                        if (!hum.BoGhost)
                        {
                            if (!hum.LoginSign)
                            {
                                try
                                {
                                    // pvDecodeSocketData (hum);
                                    hum.RunNotice();
                                    // °øÁö»çÇ×À» º¸³½´Ù.
                                }
                                catch
                                {
                                    svMain.MainOutMessage("[UsrEngn] Exception RunNotice in ProcessHumans");
                                }
                            }
                            else
                            {
                                try
                                {
                                    if (!hum.ReadyRun)
                                    {
                                        hum.Initialize();
                                        // Ä³¸¯ ¼³Á¤À» Á¡°ËÇÏ°í ·Î±×ÀÎ
                                        hum.ReadyRun = true;
                                    }
                                    else
                                    {
                                        if (HUtil32.GetTickCount() - hum.SearchTime > hum.SearchRate)
                                        {
                                            hum.SearchTime = HUtil32.GetTickCount();
                                            hum.SearchViewRange();
                                            hum.ThinkEtc();
                                        }
                                        if (HUtil32.GetTickCount() - hum.LineNoticeTime > 5 * 60 * 1000)
                                        {
                                            hum.LineNoticeTime = HUtil32.GetTickCount();
                                            if (hum.LineNoticeNumber < svMain.LineNoticeList.Count)
                                            {
                                                // LineNoticeList¿Í HumÀÌ °°Àº ½º·¡µå ÀÌ±â ¶§¹®¿¡ »ó°ü ¾ø´Ù
                                                // ÇÏÁö¸¸ ´Ù¸¥ ½º·¡µå°¡ µÈ´Ù¸é LineNoticeList´Â ¹Ýµå½Ã lock ½ÃÄÑ¾ß ÇÑ´Ù.
                                                hum.SysMsg((string)svMain.LineNoticeList[hum.LineNoticeNumber], 2);
                                            }
                                            hum.LineNoticeNumber++;
                                            if (hum.LineNoticeNumber >= svMain.LineNoticeList.Count)
                                            {
                                                hum.LineNoticeNumber = 0;
                                            }
                                        }
                                        hum.Operate();
                                        // ÀúÀå°£°Ý 10ºÐ¿¡¼­ 15ºÐ º¯°æ ->30ºÐ º¯°æ
                                        if ((!svMain.FrontEngine.HasServerHeavyLoad()) && (HUtil32.GetTickCount() > (30 * 60 * 1000 + hum.LastSaveTime)))
                                        {
                                            // À½¼ö°¡  ³ª¿Ã¼ö ÀÖÀ¸¹Ç·Î º¯°æ
                                            hum.LastSaveTime = GetTickCount + ((long)new System.Random(10 * 60 * 1000).Next());
                                            // 1 ºÐ ·£´ý->10ºÐ ¹ø°æ
                                            hum.ReadySave();
                                            SavePlayer(hum);
                                        }
                                    }
                                }
                                catch
                                {
                                    svMain.MainOutMessage("[UsrEngn] Exception Hum.Operate in ProcessHumans");
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                RunUserList.Remove(i);
                                bugcount = 2;
                                hum.Finalize();
                                bugcount = 3;
                            }
                            catch
                            {
                                svMain.MainOutMessage("[UsrEngn] Exception Hum.Finalize in ProcessHumans " + bugcount.ToString());
                            }
                            try
                            {
                                // TO PDS: Delete User From UserMgr...
                                svMain.UserMgrEngine.DeleteUser(hum.UserName);
                                ClosePlayer(hum);
                                bugcount = 4;
                                hum.ReadySave();
                                SavePlayer(hum);
                                svMain.usLock.Enter();
                                try
                                {
                                    svMain.RunSocket.CloseUser(hum.GateIndex, hum.UserHandle);
                                }
                                finally
                                {
                                    svMain.usLock.Leave();
                                }
                            }
                            catch
                            {
                                svMain.MainOutMessage("[UsrEngn] Exception RunSocket.CloseUser in ProcessHumans " + bugcount.ToString());
                            }
                            SendInterMsg(Grobal2.ISM_USERLOGOUT, svMain.ServerIndex, hum.UserName + ":" + hum.ApprovalMode.ToString());
                            // 3
                            if (hum.ApprovalMode == 1)
                            {
                                svMain.UserEngine.FreeUserCount -= 1;
                            }
                            continue;
                        }
                    }
                    i++;
                    if (HUtil32.GetTickCount() - start > svMain.HumLimitTime)
                    {
                        // ·º ¹ß»ý, ´ÙÀ½À¸·Î ¹Ì·é´Ù.
                        lack = true;
                        HumCur = i;
                        break;
                    }
                }
                if (!lack)
                {
                    HumCur = 0;
                }
            }
            catch
            {
                svMain.MainOutMessage("[UsrEngn] ProcessHumans");
            }
            HumRotCount++;
            if (HumCur == 0)
            {
                // ÇÑ¹ÙÄû µµ´Âµ¥ °É¸®´Â ½Ã°£
                HumRotCount = 0;
                svMain.humrotatecount = HumRotCount;
                k = GetTickCount - svMain.humrotatetime;
                svMain.curhumrotatetime = (int)k;
                svMain.humrotatetime = HUtil32.GetTickCount();
                if (svMain.maxhumrotatetime < k)
                {
                    svMain.maxhumrotatetime = (int)k;
                }
            }
            svMain.curhumtime = (int)(HUtil32.GetTickCount() - start);
            if (svMain.maxhumtime < svMain.curhumtime)
            {
                svMain.maxhumtime = svMain.curhumtime;
            }
        }

        public long ProcessMonsters_GetZenTime(long ztime)
        {
            long result;
            // »ç¿ëÀÚÀÇ ¼ö¿¡ µû¶ó¼­ Á¨ÀÇ ºü¸£±â°¡ ¹Ù²ñ.
            double r;
            if (ztime < 30 * 60 * 1000)
            {
                r = (GetUserCount() - svMain.UserFullCount) / svMain.ZenFastStep;
                // ¸Å 200¸íÀÌ ´Ã¶§¸¶´Ù 10%¾¿ ¸÷À» ´õ Á¨ ½ÃÅ´
                if (r > 0)
                {
                    if (r > 6)
                    {
                        r = 6;
                    }
                    result = ztime - HUtil32.MathRound(ztime / 10 * r);
                }
                else
                {
                    result = ztime;
                }
            }
            else
            {
                result = ztime;
            }
            return result;
        }

        protected void ProcessMonsters()
        {
            int i;
            int k;
            int zcount;
            long start;
            int tcount;
            TCreature cret;
            TZenInfo pz;
            bool lack;
            bool goodzen;
            start = HUtil32.GetTickCount();
            pz = null;
            try
            {
                lack = false;
                tcount = GetCurrentTime;
                // GetTickCount;
                pz = null;
                if (HUtil32.GetTickCount() - onezentime > 200)
                {
                    onezentime = HUtil32.GetTickCount();
                    if (GenCur < MonList.Count)
                    {
                        pz = (TZenInfo)MonList[GenCur];
                    }
                    if (GenCur < MonList.Count - 1)
                    {
                        GenCur++;
                    }
                    else
                    {
                        GenCur = 0;
                    }
                    if (pz != null)
                    {
                        if ((pz.MonName != "") && (!svMain.BoVentureServer))
                        {
                            // ¸ðÇè¼­¹ö¿¡¼­´Â ÀéÀÌ ¾ÈµÈ´Ù.
                            if ((pz.StartTime == 0) || (HUtil32.GetTickCount() - pz.StartTime > ProcessMonsters_GetZenTime(pz.MonZenTime)))
                            {
                                zcount = GetMonCount(pz);
                                goodzen = true;
                                if (pz.Count > zcount)
                                {
                                    goodzen = RegenMonsters(pz, pz.Count - zcount);
                                }
                                if (goodzen)
                                {
                                    if (pz.MonZenTime == 180)
                                    {
                                        if (HUtil32.GetTickCount() >= 60 * 60 * 1000)
                                        {
                                            pz.StartTime = GetTickCount - (60 * 60 * 1000) + ((long)new System.Random(120 * 60 * 1000).Next());
                                        }
                                        else
                                        {
                                            pz.StartTime = HUtil32.GetTickCount();
                                        }
                                    }
                                    else
                                    {
                                        pz.StartTime = HUtil32.GetTickCount();
                                    }
                                }
                            }
                            svMain.LatestGenStr = pz.MonName + "," + GenCur.ToString() + "/" + MonList.Count.ToString();
                        }
                    }
                }
                MonCurRunCount = 0;
                for (i = MonCur; i < MonList.Count; i++)
                {
                    pz = (TZenInfo)MonList[i];
                    if (MonSubCur < pz.Mons.Count)
                    {
                        k = MonSubCur;
                    }
                    else
                    {
                        k = 0;
                    }
                    MonSubCur = 0;
                    while (true)
                    {
                        if (k >= pz.Mons.Count)
                        {
                            break;
                        }
                        cret = (TCreature)pz.Mons[k];
                        if (!cret.BoGhost)
                        {
                            if (tcount - cret.RunTime > cret.RunNextTick)
                            {
                                cret.RunTime = tcount;
                                if (HUtil32.GetTickCount() > (cret.SearchRate + cret.SearchTime))
                                {
                                    cret.SearchTime = HUtil32.GetTickCount();
                                    // 2003/03/18
                                    if ((cret.RefObjCount > 0) || cret.HideMode)
                                    {
                                        cret.SearchViewRange();
                                    }
                                    else
                                    {
                                        cret.RefObjCount = 0;
                                    }
                                }
                                try
                                {
                                    // 2003-09-09  PDS ¿¡·¯¹ß»ý½Ã ¸ó½ºÅÍ ¸®½ºÆ®¿¡¼­ »èÁ¦
                                    cret.Run();
                                    MonCurRunCount++;
                                }
                                catch
                                {
                                    pz.Mons.RemoveAt(k);
                                    // cret.Free();
                                    cret = null;
                                }
                            }
                            MonCurCount++;
                        }
                        else
                        {
                            // 5ºÐÀÌ Áö³ª¸é free ½ÃÅ²´Ù.
                            if (HUtil32.GetTickCount() > (5 * 60 * 1000 + cret.GhostTime))
                            {
                                pz.Mons.RemoveAt(k);
                                cret.Free();
                                cret = null;
                                continue;
                            }
                        }
                        k++;
                        if ((cret != null) && (HUtil32.GetTickCount() - start > svMain.MonLimitTime))
                        {
                            // ·º ¹ß»ý, ¸ó½ºÆ® ¿òÁ÷ÀÓÀº ¿ì¼±¼øÀ§°¡ ³·À½
                            svMain.LatestMonStr = cret.UserName + "/" + i.ToString() + "/" + k.ToString();
                            lack = true;
                            MonSubCur = k;
                            break;
                        }
                    }
                    if (lack)
                    {
                        break;
                    }
                }
                if (i >= MonList.Count)
                {
                    MonCur = 0;
                    MonCount = MonCurCount;
                    MonCurCount = 0;
                    MonRunCount = (MonRunCount + MonCurRunCount) / 2;
                }
                if (!lack)
                {
                    MonCur = 0;
                }
                else
                {
                    MonCur = i;
                }
            }
            catch
            {
                if (pz != null)
                {
                    svMain.MainOutMessage("[UsrEngn] ProcessMonsters : " + pz.MonName + "/" + pz.MapName + "/" + pz.X.ToString() + "," + pz.Y.ToString());
                }
                else
                {
                    svMain.MainOutMessage("[UsrEngn] ProcessMonsters");
                }
            }
            svMain.curmontime = (int)(HUtil32.GetTickCount() - start);
            if (svMain.maxmontime < svMain.curmontime)
            {
                svMain.maxmontime = svMain.curmontime;
            }
        }

        protected void ProcessDefaultNpcs()
        {
            int tcount;
            TCreature cret;
            try
            {
                tcount = GetCurrentTime;
                cret = svMain.DefaultNpc;
                if (!cret.BoGhost)
                {
                    if (tcount - cret.RunTime > cret.RunNextTick)
                    {
                        if (HUtil32.GetTickCount() - cret.SearchTime > cret.SearchRate)
                        {
                            cret.SearchTime = HUtil32.GetTickCount();
                            cret.SearchViewRange();
                        }
                        if (tcount - cret.RunTime > cret.RunNextTick)
                        {
                            cret.RunTime = tcount;
                            cret.Run();
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[UsrEngn] ProcessDefaultNpcs");
            }
        }

        protected void ProcessMerchants()
        {
            int i;
            long start;
            int tcount;
            TCreature cret;
            bool lack;
            start = HUtil32.GetTickCount();
            lack = false;
            try
            {
                tcount = GetCurrentTime;
                for (i = MerCur; i < MerchantList.Count; i++)
                {
                    cret = (TCreature)MerchantList[i];
                    if (!cret.BoGhost)
                    {
                        if (tcount - cret.RunTime > cret.RunNextTick)
                        {
                            if (HUtil32.GetTickCount() - cret.SearchTime > cret.SearchRate)
                            {
                                cret.SearchTime = HUtil32.GetTickCount();
                                cret.SearchViewRange();
                            }
                            if (tcount - cret.RunTime > cret.RunNextTick)
                            {
                                cret.RunTime = tcount;
                                cret.Run();
                            }
                        }
                    }
                    if (HUtil32.GetTickCount() - start > svMain.NpcLimitTime)
                    {
                        // ½ÃÀÛ ÃÊ°ú ´ÙÀ½¿¡ Ã³¸®
                        MerCur = i;
                        lack = true;
                        break;
                    }
                }
                if (!lack)
                {
                    MerCur = 0;
                }
            }
            catch
            {
                svMain.MainOutMessage("[UsrEngn] ProcessMerchants");
            }
        }

        protected void ProcessNpcs()
        {
            int i;
            int tcount;
            long start;
            TCreature cret;
            bool lack;
            start = HUtil32.GetTickCount();
            lack = false;
            try
            {
                tcount = GetCurrentTime;
                for (i = NpcCur; i < NpcList.Count; i++)
                {
                    cret = (TCreature)NpcList[i];
                    if (!cret.BoGhost)
                    {
                        if (tcount - cret.RunTime > cret.RunNextTick)
                        {
                            if (HUtil32.GetTickCount() - cret.SearchTime > cret.SearchRate)
                            {
                                cret.SearchTime = HUtil32.GetTickCount();
                                cret.SearchViewRange();
                            }
                            if (tcount - cret.RunTime > cret.RunNextTick)
                            {
                                cret.RunTime = tcount;
                                cret.Run();
                            }
                        }
                    }
                    if (HUtil32.GetTickCount() - start > svMain.NpcLimitTime)
                    {
                        // ½ÃÀÛ ÃÊ°ú ´ÙÀ½¿¡ Ã³¸®
                        NpcCur = i;
                        lack = true;
                        break;
                    }
                }
                if (!lack)
                {
                    NpcCur = 0;
                }
            }
            catch
            {
                svMain.MainOutMessage("[UsrEngn] ProcessNpcs");
            }
        }

        // ¹Ì¼Ç
        // -------------------------- Missions ----------------------------
        public bool LoadMission(string flname)
        {
            bool result;
            TMission mission;
            mission = new TMission(flname);
            if (!mission.BoPlay)
            {
                mission.Free();
                result = false;
            }
            else
            {
                MissionList.Add(mission);
                result = true;
            }
            return result;
        }

        // ¹Ì¼Ç ÆÄÀÏÀ» ÀÐ¾î¼­ ¹Ì¼ÇÀ» È®¼ºÈ­ ½ÃÅ´
        public bool StopMission(string missionname)
        {
            bool result;
            int i;
            for (i = 0; i < MissionList.Count; i++)
            {
                if (((TMission)MissionList[i]).MissionName == missionname)
                {
                    ((TMission)MissionList[i]).BoPlay = false;
                    break;
                }
            }
            result = true;
            return result;
        }

        public void GetRandomDefStart(ref string map, ref int sx, ref int sy)
        {
            int n;
            if (svMain.StartPoints.Count > 0)
            {
                if (svMain.StartPoints.Count > 1)
                {
                    n = new System.Random(2).Next();
                }
                else
                {
                    n = 0;
                }
                map = svMain.StartPoints[n].mapName;
                sx = svMain.StartPoints[n].nY;
                sy = svMain.StartPoints[n].nY;
            }
            else
            {
                map = svMain.DefHomeMap;
                sx = svMain.DefHomeX;
                sy = svMain.DefHomeY;
            }
        }

        protected void ProcessMissions()
        {
            int i;
            try
            {
                for (i = MissionList.Count - 1; i >= 0; i--)
                {
                    if (((TMission)MissionList[i]).BoPlay)
                    {
                        ((TMission)MissionList[i]).Run();
                    }
                    else
                    {
                        ((TMission)MissionList[i]).Free();
                        MissionList.RemoveAt(i);
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[UsrEngn] ProcessMissions");
            }
        }

        protected void ProcessDragon()
        {
            svMain.gFireDragon.Run();
        }

        // ----------------------- ExecuteRun --------------------------
        public void Initialize()
        {
            int i;
            TZenInfo pz;
            LoadRefillCretInfos();
            // ¸ó½ºÅÍ ¸®Á¨ Á¤º¸¸¦ ÀÐ´Â´Ù.
            InitializeMerchants();
            InitializeNpcs();
            InitializeDefaultNpcs();
            // pz, ¸ó½ºÅÍÀÇ MonName À¸·Î MonRace¸¦ ¾ò¾î ³õ´Â´Ù.
            for (i = 0; i < MonList.Count; i++)
            {
                pz = (TZenInfo)MonList[i];
                if (pz != null)
                {
                    pz.MonRace = GetMonRace(pz.MonName);
                }
            }
        }

        public void ExecuteRun()
        {
            int i;
            runonetime = HUtil32.GetTickCount();
            try
            {
                ProcessUserHumans();
                ProcessMonsters();
                ProcessMerchants();
                ProcessNpcs();
                ProcessDefaultNpcs();
                if (HUtil32.GetTickCount() - missiontime > 1000)
                {
                    missiontime = HUtil32.GetTickCount();
                    ProcessMissions();
                    CheckServerWaitTimeOut();
                    CheckHolySeizeValid();
                }
                // if
                if (HUtil32.GetTickCount() - opendoorcheck > 500)
                {
                    opendoorcheck = HUtil32.GetTickCount();
                    CheckOpenDoors();
                }
                // if
                if (HUtil32.GetTickCount() - timer10min > 10 * 60 * 1000)
                {
                    // 10ºÐ¿¡ ÇÑ ¹ø
                    timer10min = HUtil32.GetTickCount();
                    svMain.NoticeMan.RefreshNoticeList();
                    svMain.MainOutMessage(DateTime.Now.ToString() + " User = " + GetUserCount().ToString());
                    svMain.UserCastle.SaveAll();
                    // Àå¿ø²Ù¹Ì±â ¾ÆÀÌÅÛ ³»±¸ ¾÷µ¥ÀÌÆ®
                    gaDecoItemCount++;
                    if (gaDecoItemCount >= 6)
                    {
                        gaDecoItemCount = 0;
                    }
                    // 6*10ºÐ = 1½Ã°£¿¡ ÇÑ ¹ø
                    if (gaDecoItemCount == 0)
                    {
                        svMain.GuildAgitMan.DecreaseDecoMonDurability();
                    }
#if DEBUG
                    // sonmg
                    // ÀÓ½Ã 10ºÐ¿¡ ÇÑ¹ø...(sonmg)
                    svMain.GuildAgitMan.DecreaseDecoMonDurability();
#endif
                }
                // if
                if (HUtil32.GetTickCount() - timer10sec > 10 * 1000)
                {
                    timer10sec = HUtil32.GetTickCount();
                    IdSrvClient.FrmIDSoc.SendUserCount(GetRealUserCount());
                    svMain.GuildMan.CheckGuildWarTimeOut();
                    svMain.UserCastle.Run();
                    if (HUtil32.GetTickCount() - timer1min > 60 * 1000)
                    {
                        // 1ºÐ¿¡ ÇÑ ¹ø
                        timer1min = HUtil32.GetTickCount();
                        gaCount++;
                        if (gaCount >= 10)
                        {
                            gaCount = 0;
                        }
                        if (svMain.GuildAgitMan.CheckGuildAgitTimeOut(gaCount))
                        {
                            // Àå¿ø°Ô½ÃÆÇ ¸®·Îµå.
                            svMain.GuildAgitBoardMan.LoadAllGaBoardList("");
                        }
                    }
                    // Ã¤±Ý ½Ã°£ÀÌ ³¡³µ´ÂÁö °Ë»ç
                    for (i = svMain.ShutUpList.Count - 1; i >= 0; i--)
                    {
                        if (GetCurrentTime > ((int)svMain.ShutUpList.Objects[i]))
                        {
                            svMain.ShutUpList.Delete(i);
                        }
                    }
                }
                // if
                svMain.gFireDragon.Run();
            }
            catch
            {
                svMain.MainOutMessage("[UsrEngn] Raise Exception..");
            }
            svMain.curusrcount = (int)(HUtil32.GetTickCount() - runonetime);
            if (svMain.maxusrtime < svMain.curusrcount)
            {
                svMain.maxusrtime = svMain.curusrcount;
            }
        }

        // Ã¤ÆÃ·Î±×
        public bool FindChatLogList(string whostr, ref int idx)
        {
            bool result;
            int i;
            result = false;
            for (i = 0; i < ChatLogList.Count; i++)
            {
                if (ChatLogList[i] == whostr)
                {
                    result = true;
                    idx = i;
                    return result;
                }
            }
            return result;
        }

        public void ProcessUserMessage(TUserHuman hum, TDefaultMessage pmsg, string pbody)
        {
            string body = string.Empty;
            try
            {
                if (pmsg == null)
                {
                    return;
                }
                if (pbody == null)
                {
                    body = "";
                }
                else
                {
                    body = pbody;
                }
                switch (pmsg.Ident)
                {
                    case Grobal2.CM_TURN:
                    case Grobal2.CM_WALK:
                    case Grobal2.CM_RUN:
                    case Grobal2.CM_HIT:
                    case Grobal2.CM_POWERHIT:
                    case Grobal2.CM_LONGHIT:
                    case Grobal2.CM_WIDEHIT:
                    case Grobal2.CM_HEAVYHIT:
                    case Grobal2.CM_BIGHIT:
                    case Grobal2.CM_FIREHIT:
                    case Grobal2.CM_CROSSHIT:
                    case Grobal2.CM_TWINHIT:
                    case Grobal2.CM_SITDOWN:
                        // 2003/03/15 ½Å±Ô¹«°ø
                        hum.SendMsg(hum, pmsg.Ident, pmsg.Tag, HUtil32.LoWord(pmsg.Recog), HUtil32.HiWord(pmsg.Recog), 0, "");
                        break;
                    case Grobal2.CM_SPELL:
                        hum.SendMsg(hum, pmsg.Ident, pmsg.Tag, HUtil32.LoWord(pmsg.Recog), HUtil32.HiWord(pmsg.Recog), HUtil32.MakeLong(pmsg.Param, pmsg.Series), "");
                        break;
                    case Grobal2.CM_QUERYUSERNAME:
                        // x
                        // y
                        hum.SendMsg(hum, pmsg.Ident, 0, pmsg.Recog, pmsg.Param, pmsg.Tag, "");
                        break;
                    case Grobal2.CM_SAY:
                        hum.SendMsg(hum, Grobal2.CM_SAY, 0, 0, 0, 0, EDcode.DecodeString(body));
                        break;
                    case Grobal2.CM_DROPITEM:
                    case Grobal2.CM_TAKEONITEM:
                    case Grobal2.CM_TAKEOFFITEM:
                    case Grobal2.CM_EXCHGTAKEONITEM:
                    case Grobal2.CM_MERCHANTDLGSELECT:
                    case Grobal2.CM_MERCHANTQUERYSELLPRICE:
                    case Grobal2.CM_MERCHANTQUERYREPAIRCOST:
                    case Grobal2.CM_USERSELLITEM:
                    case Grobal2.CM_USERREPAIRITEM:
                    case Grobal2.CM_USERSTORAGEITEM:
                    case Grobal2.CM_USERBUYITEM:
                    case Grobal2.CM_USERGETDETAILITEM:
                    case Grobal2.CM_CREATEGROUP:
                    case Grobal2.CM_CREATEGROUPREQ_OK:
                    case Grobal2.CM_CREATEGROUPREQ_FAIL:
                    case Grobal2.CM_ADDGROUPMEMBER:
                    case Grobal2.CM_ADDGROUPMEMBERREQ_OK:
                    case Grobal2.CM_ADDGROUPMEMBERREQ_FAIL:
                    case Grobal2.CM_DELGROUPMEMBER:
                    case Grobal2.CM_DEALTRY:
                    case Grobal2.CM_DEALADDITEM:
                    case Grobal2.CM_DEALDELITEM:
                    case Grobal2.CM_USERTAKEBACKSTORAGEITEM:
                    case Grobal2.CM_USERMAKEDRUGITEM:
                    case Grobal2.CM_GUILDADDMEMBER:
                    case Grobal2.CM_GUILDDELMEMBER:
                    case Grobal2.CM_GUILDUPDATENOTICE:
                    case Grobal2.CM_GUILDUPDATERANKINFO:
                    case Grobal2.CM_LM_DELETE:
                    case Grobal2.CM_LM_DELETE_REQ_OK:
                    case Grobal2.CM_LM_DELETE_REQ_FAIL:
                    case Grobal2.CM_UPGRADEITEM:
                    case Grobal2.CM_DROPCOUNTITEM:
                    case Grobal2.CM_USERMAKEITEMSEL:
                    case Grobal2.CM_USERMAKEITEM:
                    case Grobal2.CM_ITEMSUMCOUNT:
                    case Grobal2.CM_MARKET_LIST:
                    case Grobal2.CM_MARKET_SELL:
                    case Grobal2.CM_MARKET_BUY:
                    case Grobal2.CM_MARKET_CANCEL:
                    case Grobal2.CM_MARKET_GETPAY:
                    case Grobal2.CM_MARKET_CLOSE:
                    case Grobal2.CM_GUILDAGITLIST:
                    case Grobal2.CM_GUILDAGIT_TAG_ADD:
                    case Grobal2.CM_GABOARD_LIST:
                    case Grobal2.CM_GABOARD_READ:
                    case Grobal2.CM_GABOARD_ADD:
                    case Grobal2.CM_GABOARD_EDIT:
                    case Grobal2.CM_GABOARD_DEL:
                    case Grobal2.CM_GABOARD_NOTICE_CHECK:
                    case Grobal2.CM_DECOITEM_BUY:
                        // string ÆÄ¶ó¸ÞÅÍ°¡ ÀÖÀ½.
                        // ±×·ì °á¼º È®ÀÎ
                        // ±×·ì °á¼º È®ÀÎ
                        // ±×·ì °á¼º È®ÀÎ
                        // ±×·ì °á¼º È®ÀÎ
                        // added by sonmg.2003/10/02
                        // added by sonmg.2003/10/11
                        // added by sonmg.2003/11/3
                        // added by sonmg.2003/11/3
                        // added by sonmg.2003/11/10
                        // Àå¿ø ÂÊÁö
                        // Àå¿ø°Ô½ÃÆÇ¸ñ·Ï
                        // added by sonmg.2004/08/04
                        hum.SendMsg(hum, pmsg.Ident, pmsg.Series, pmsg.Recog, pmsg.Param, pmsg.Tag, EDcode.DecodeString(body));
                        break;
                    case Grobal2.CM_ADJUST_BONUS:
                    case Grobal2.CM_UPDATESTALLITEM:
                    case Grobal2.CM_OPENSTALL:
                    case Grobal2.CM_BUYSHOPITEM:
                    case Grobal2.CM_SHOPPRESEND:
                        hum.SendMsg(hum, pmsg.Ident, pmsg.Series, pmsg.Recog, pmsg.Param, pmsg.Tag, body);
                        break;
                    case Grobal2.CM_FRIEND_ADD:
                    case Grobal2.CM_FRIEND_DELETE:
                    case Grobal2.CM_FRIEND_EDIT:
                    case Grobal2.CM_FRIEND_LIST:
                    case Grobal2.CM_TAG_ADD:
                    case Grobal2.CM_TAG_DELETE:
                    case Grobal2.CM_TAG_SETINFO:
                    case Grobal2.CM_TAG_LIST:
                    case Grobal2.CM_TAG_NOTREADCOUNT:
                    case Grobal2.CM_TAG_REJECT_LIST:
                    case Grobal2.CM_TAG_REJECT_ADD:
                    case Grobal2.CM_TAG_REJECT_DELETE:
                        // Ä£±¸Ãß°¡
                        // Ä£±¸»èÁ¦
                        // Ä£±¸¼³¸í º¯°æ
                        // Ä£±¸ ¸®½ºÆ® ¿äÃ»
                        // ÂÊÁö Ãß°¡
                        // ÂÊÁö »èÁ¦
                        // ÂÊÁö »óÅÂ º¯°æ
                        // ÂÊÁö ¸®½ºÆ® ¿äÃ»
                        // ÀÐÁö¾ÊÀº ÂÊÁö °³¼ö ¿äÃ»
                        // °ÅºÎÀÚ ¸®½ºÆ®
                        // °ÅºÎÀÚ Ãß°¡
                        // °ÅºÎÀÚ »èÁ¦
                        // --------------------------------------------------------
                        // ¿¬ÀÎÀÇ ÀÌ¸§°ú °°À¸¸é »èÁ¦ÇÏÁö ¾Ê´Â´Ù.(2004/11/04)
                        // --------------------------------------------------------
                        if (pmsg.Ident == Grobal2.CM_FRIEND_DELETE)
                        {
                            if (hum.fLover.GetLoverName != EDcode.DecodeString(body))
                            {
                                svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stInterServer, svMain.ServerIndex, hum.GateIndex, hum.UserGateIndex, hum.UserHandle, hum.UserName, pmsg, EDcode.DecodeString(body));
                            }
                            else
                            {
                                hum.BoxMsg("A lovers\' relationship cannot be deleted.", 0);
                            }
                        }
                        else
                        {
                            svMain.UserMgrEngine.ExternSendMsg(TSendTarget.stInterServer, svMain.ServerIndex, hum.GateIndex, hum.UserGateIndex, hum.UserHandle, hum.UserName, pmsg, EDcode.DecodeString(body));
                        }
                        break;
                    default:
                        hum.SendMsg(hum, pmsg.Ident, pmsg.Series, pmsg.Recog, pmsg.Param, pmsg.Tag, "");
                        break;
                }
                // ¸Þ¼¼Áö¸¦ ¹ÞÀ¸¸é ¹Ù·Î Ã³¸®ÇÑ´Ù.  (¸ÖÆ¼½º·¡µåÀÎ°æ¿¡´Â »ç¿ëÇÒ ¼ö ¾ø´Ù)
                if (hum.ReadyRun)
                {
                    switch (pmsg.Ident)
                    {
                        case Grobal2.CM_TURN:
                        case Grobal2.CM_WALK:
                        case Grobal2.CM_RUN:
                        case Grobal2.CM_HIT:
                        case Grobal2.CM_POWERHIT:
                        case Grobal2.CM_LONGHIT:
                        case Grobal2.CM_WIDEHIT:
                        case Grobal2.CM_HEAVYHIT:
                        case Grobal2.CM_BIGHIT:
                        case Grobal2.CM_FIREHIT:
                        case Grobal2.CM_CROSSHIT:
                        case Grobal2.CM_TWINHIT:
                        case Grobal2.CM_SITDOWN:
                            // 2003/03/15 ½Å±Ô¹«°ø
                            hum.RunTime = hum.RunTime - 100;
                            break;
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[Exception] ProcessUserMessage..");
            }
        }

        // ´Ù¸¥ ¾²·¹µå¿¡ ¸Þ¼¼Áö ³ÖÀ»¶§ »ç¿ë
        public void ExternSendMessage(string UserName, short Ident, short wparam, long lParam1, long lParam2, long lParam3, string str)
        {
            TUserHuman hum;
            FUserCS.Enter();
            try
            {
                hum = GetUserHuman(UserName);
                if (hum != null)
                {
                    hum.SendMsg(hum, (ushort)Ident, (ushort)wparam, lParam1, lParam2, lParam3, str);
                }
            }
            finally
            {
                FUserCS.Leave();
            }
        }

        // ¸Ê¿¡ ¾ÆÀÌÅÛ Á¨ ½ÃÅ°±â(sonmg)
        public int MakeItemToMap(string DropMapName, string ItemName, int Amount, int dx, int dy)
        {
            int result;
            TStdItem ps;
            TUserItem newpu;
            TMapItem pmi;
            TMapItem pr;
            int iTemp;
            TEnvirnoment dropenvir;
            result = 0;
            if (ItemName == "½ð±Ò")
            {
                // '±ÝÀü'
                ItemName = Envir.NAME_OF_GOLD;
                Amount = new System.Random((Amount / 2) + 1).Next() + (Amount / 2);
            }
            try
            {
                ps = GetStdItemFromName(ItemName);
                if (ps != null)
                {
                    newpu = new TUserItem();
                    if (CopyToUserItemFromName(ps.Name, ref newpu))
                    {
                        pmi = new TMapItem();
                        pmi.UserItem = newpu;
                        if (ItemName == Envir.NAME_OF_GOLD)
                        {
                            pmi.Name = Envir.NAME_OF_GOLD;
                            pmi.Count = Amount;
                            pmi.Looks = GetGoldLooks(Amount);
                            pmi.Ownership = null;
                            pmi.Droptime = HUtil32.GetTickCount();
                            pmi.Droper = null;
                        }
                        else
                        {
                            if (ps.OverlapItem >= 1)
                            {
                                iTemp = newpu.Dura;
                                if (iTemp > 1)
                                {
                                    pmi.Name = ps.Name + "(" + iTemp.ToString() + ")";
                                }
                                else
                                {
                                    pmi.Name = ps.Name;
                                }
                            }
                            else
                            {
                                pmi.Name = ps.Name;
                            }
                            pmi.Looks = ps.Looks;
                            if (ps.StdMode == 45)
                            {
                                pmi.Looks = GetRandomLook(pmi.Looks, ps.Shape);
                            }
                            pmi.AniCount = ps.AniCount;
                            pmi.Reserved = 0;
                            pmi.Count = 1;
                            pmi.Ownership = null;
                            pmi.Droptime = HUtil32.GetTickCount();
                            pmi.Droper = null;
                        }
                        dropenvir = svMain.GrobalEnvir.GetEnvir(DropMapName);
                        pr = (TMapItem)dropenvir.AddToMap(dx, dy, Grobal2.OS_ITEMOBJECT, pmi);
                        if (pr == pmi)
                        {
                            result = pmi.UserItem.MakeIndex;
                            svMain.MainOutMessage("[DragonItemGen] " + pmi.Name + "(" + dx.ToString() + "," + dy.ToString());
                        }
                        else
                        {
                            Dispose(pmi);
                        }
                    }
                    if (newpu != null)
                    {
                        Dispose(newpu);
                    }
                }
                // ///////////////////////////////////////////
            }
            catch
            {
                svMain.MainOutMessage("[Exception] TUserEngine.MakeItemToMap");
            }
            return result;
        }

        // Ó¦ÓÃ·þÎñ¶ËÉèÖÃ
        public void ApplyGameConfig()
        {
            TUserHuman hum;
            for (var i = 0; i < RunUserList.Count; i++)
            {
                hum = (TUserHuman)RunUserList.Values[i];
                if ((hum != null) && (!hum.BoGhost) && (!hum.Death))
                {
                    hum.SendGameConfig();
                }
            }
        }

        public void SendBroadCastMsg(string sMsg, int MsgType)
        {
            int i;
            TUserHuman PlayObject;
            for (i = 0; i < RunUserList.Count; i++)
            {
                PlayObject = (TUserHuman)RunUserList.Values[i];
                if (!PlayObject.BoGhost && !PlayObject.Death)
                {
                    PlayObject.SysMsg(sMsg, MsgType);
                }
            }
        }

        public int GetTickCount => System.Environment.TickCount;

        public void Dispose(object obj)
        {
            if (obj != null)
            {
                obj = null;
            }
        }
    }
}