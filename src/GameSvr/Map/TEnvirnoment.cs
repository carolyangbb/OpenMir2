using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SystemModule;

namespace GameSvr
{
    public class TEnvirnoment
    {
        public string MapName = String.Empty;
        public string NewMapName = String.Empty;
        public bool UseNewMap = false;
        public string MapTitle = String.Empty;
        public TMapInfo[] MMap;
        public int MiniMap = 0;
        public int Server = 0;
        public int NeedLevel = 0;
        public ushort MapWidth = 0;
        public ushort MapHeight = 0;
        public bool Darkness = false;
        public bool Dawn = false;
        public bool DayLight = false;
        public IList<TDoorInfo> DoorList = null;
        public bool BoCanGetItem = false;
        public bool LawFull = false;
        public bool FightZone = false;
        public bool Fight2Zone = false;
        public bool Fight3Zone = false;
        public bool Fight4Zone = false;
        public bool QuizZone = false;
        public bool NoReconnect = false;
        public bool NeedHole = false;
        public bool NoRecall = false;
        public bool NoRandomMove = false;
        public bool NoEscapeMove = false;
        public bool NoTeleportMove = false;
        public bool NoDrug = false;
        public int MineMap = 0;
        public bool NoPositionMove = false;
        public string BackMap = String.Empty;
        public Object MapQuest = null;
        public int NeedSetNumber = 0;
        public int NeedSetValue = 0;
        public int AutoAttack = 0;
        public int GuildAgit = 0;
        public bool NoChat = false;
        public bool NoGroup = false;
        public bool NoThrowItem = false;
        public bool NoDropItem = false;
        public bool BoStall = false;
        public bool NoDeal = false;
        public IList<TMapQuestInfo> MapQuestList = null;
        public int[] MapQuestParams;

        public TEnvirnoment()
        {
            MapName = "";
            NewMapName = "";
            UseNewMap = false;
            Server = 0;
            MMap = null;
            MiniMap = 0;
            MapWidth = 0;
            MapHeight = 0;
            Darkness = false;
            Dawn = false;
            DayLight = false;
            DoorList = new List<TDoorInfo>();
            MapQuestList = new List<TMapQuestInfo>();
            //FillChar(MapQuestParams, sizeof(MapQuestParams), '\0');
        }

        ~TEnvirnoment()
        {
            //DoorList.Free();
            //MapQuestList.Free();
            //base.Destroy();
        }

        private void ResizeMap(long xsize, long ysize)
        {
            int i;
            int j;
            if ((xsize > 1) && (ysize > 1))
            {
                if (MMap != null)
                {
                    for (i = 0; i < MapWidth; i++)
                    {
                        for (j = 0; j < MapHeight; j++)
                        {
                            if (MMap[i * MapHeight + j].OBJList != null)
                            {
                                MMap[i * MapHeight + j].OBJList.Free();
                            }
                        }
                    }
                    FreeMem(MMap);
                    MMap = null;
                }
                MapWidth = (ushort)xsize;
                MapHeight = (ushort)ysize;
                MMap = AllocMem(MapWidth * MapHeight * sizeof(TMapInfo));
            }
        }

        public bool LoadMap(string map)
        {
            bool result;
            int i;
            int j;
            int k;
            int fhandle;
            int t;
            int h;
            int door;
            TMapHeader header;
            TMapHeader_AntiHack header2;
            TMapFileInfo[] mbuf;
            TMapInfo pm=null;
            TDoorInfo pd;
            string TempStr;
            bool EncodeMap;
            result = false;
            TempStr = MapName.ToUpper();
            EncodeMap = false;
            if ((TempStr == "LABY01") || (TempStr == "LABY02") || (TempStr == "LABY03") || (TempStr == "LABY04") || (TempStr == "SNAKE"))
            {
                EncodeMap = true;
            }
            if (File.Exists(map))
            {
                fhandle = File.Open(map, (FileMode)FileAccess.Read | FileShare.None);
                if (fhandle > 0)
                {
                    if (EncodeMap)
                    {
                        FileRead(fhandle, header2, sizeof(TMapHeader_AntiHack));
                        header2.Width = (ushort)(header2.Width ^ header2.CheckKey);
                        header2.Height = (ushort)(header2.Height ^ header2.CheckKey);
                        MapWidth = header2.Width;
                        MapHeight = header2.Height;
                    }
                    else
                    {
                        FileRead(fhandle, header, sizeof(TMapHeader));
                        MapWidth = header.Width;
                        MapHeight = header.Height;
                    }
                    ResizeMap(MapWidth, MapHeight);
                    t = sizeof(TMapFileInfo) * MapWidth * MapHeight;
                    mbuf = AllocMem(t);
                    FileRead(fhandle, mbuf, t);
                    for (i = 0; i < MapWidth; i++)
                    {
                        h = i * MapHeight;
                        for (j = 0; j < MapHeight; j++)
                        {
                            if (EncodeMap)
                            {
                                mbuf[h + j].BkImg = (ushort)(mbuf[h + j].BkImg ^ header2.CheckKey);
                                mbuf[h + j].MidImg = (ushort)(mbuf[h + j].MidImg ^ header2.CheckKey);
                                mbuf[h + j].FrImg = (ushort)(mbuf[h + j].FrImg ^ header2.CheckKey);
                            }
                            if ((mbuf[h + j].BkImg & 0x8000) != 0)
                            {
                                pm = MMap[h + j];
                                pm.MoveAttr = 1;
                            }
                            if ((mbuf[h + j].FrImg & 0x8000) != 0)
                            {
                                pm = MMap[h + j];
                                pm.MoveAttr = 2;
                            }
                            if ((mbuf[h + j].DoorIndex & 0x80) != 0)
                            {
                                door = mbuf[h + j].DoorIndex & 0x7F;
                                if (door > 0)
                                {
                                    pd = new TDoorInfo();
                                    pd.DoorX = i;
                                    pd.DoorY = j;
                                    pd.DoorNumber = door;
                                    pd.PCore = null;
                                    for (k = 0; k < DoorList.Count; k++)
                                    {
                                        // 鞍篮 Door 贸府
                                        if ((Math.Abs(pd.DoorX - (DoorList[k] as TDoorInfo).DoorX) <= 10) && (Math.Abs(pd.DoorY - (DoorList[k] as TDoorInfo).DoorY) <= 10) && (door == (DoorList[k] as TDoorInfo).DoorNumber))
                                        {
                                            pd.PCore = (DoorList[k] as TDoorInfo).PCore;
                                            break;
                                        }
                                    }
                                    if (pd.PCore == null)
                                    {
                                        pd.PCore = new TDoorCore();
                                        pd.PCore.DoorOpenState = false;
                                        pd.PCore.__Lock = false;
                                        pd.PCore.LockKey = 0;
                                        pd.PCore.OpenTime = 0;
                                    }
                                    DoorList.Add(pd);
                                }
                            }
                        }
                    }
                    Dispose(mbuf);
                    fhandle.Close();
                    result = true;
                }
            }
            return result;
        }

        public bool GetMapXY(int x, int y, ref TMapInfo pm)
        {
            bool result;
            pm = null;
            result = false;
            if ((x >= 0) && (x < MapWidth) && (y >= 0) && (y < MapHeight))
            {
                pm = MMap[x * MapHeight + y];
                if (pm != null)
                {
                    result = true;
                }
            }
            return result;
        }

        public Object GetCreature(int x, int y, bool aliveonly)
        {
            Object result;
            TMapInfo pm=null;
            int i;
            bool inrange;
            TCreature cret;
            result = null;
            inrange = GetMapXY(x, y, ref pm);
            if (inrange)
            {
                if (pm.OBJList != null)
                {
                    for (i = pm.OBJList.Count - 1; i >= 0; i--)
                    {
                        if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_MOVINGOBJECT)
                        {
                            cret = (TCreature)((TAThing)pm.OBJList[i]).AObject;
                            if (cret != null)
                            {
                                if ((!cret.BoGhost) && cret.HoldPlace && (!aliveonly || !cret.Death))
                                {
                                    result = cret;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public int GetAllCreature(int x, int y, bool aliveonly, ArrayList list)
        {
            int result;
            TMapInfo pm=null;
            int i;
            bool inrange;
            TCreature cret;
            inrange = GetMapXY(x, y, ref pm);
            if (inrange)
            {
                if (pm.OBJList != null)
                {
                    for (i = pm.OBJList.Count - 1; i >= 0; i--)
                    {
                        if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_MOVINGOBJECT)
                        {
                            cret = (TCreature)((TAThing)pm.OBJList[i]).AObject;
                            if (cret != null)
                            {
                                if ((!cret.BoGhost) && cret.HoldPlace && (!aliveonly || !cret.Death))
                                {
                                    list.Add(cret);
                                    // break;
                                }
                            }
                        }
                    }
                }
            }
            result = list.Count;
            return result;
        }

        public int GetCreatureInRange(int x, int y, int wide, bool aliveonly, ArrayList list)
        {
            int result;
            int i;
            int k;
            result = 0;
            for (i = x - wide; i <= x + wide; i++)
            {
                for (k = y - wide; k <= y + wide; k++)
                {
                    GetAllCreature(i, k, aliveonly, list);
                }
            }
            result = list.Count;
            return result;
        }

        public bool IsValidCreature(int x, int y, int checkrange, Object cret)
        {
            bool result;
            TMapInfo pm=null;
            int k;
            int m;
            int i;
            bool inrange;
            result = false;
            for (k = x - checkrange; k <= x + checkrange; k++)
            {
                for (m = y - checkrange; m <= y + checkrange; m++)
                {
                    inrange = GetMapXY(k, m, ref pm);
                    if (inrange)
                    {
                        if (pm.OBJList != null)
                        {
                            for (i = pm.OBJList.Count - 1; i >= 0; i--)
                            {
                                if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_MOVINGOBJECT)
                                {
                                    if (((TAThing)pm.OBJList[i]).AObject == cret)
                                    {
                                        result = true;
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

        // (sonmg 2004/12/28 -> 2005/02/24 犁荐沥)
        public bool IsValidFrontCreature(int x, int y, int checkrange, ref Object cret)
        {
            bool result;
            TMapInfo pm=null;
            int k;
            int m;
            int i;
            bool inrange;
            TCreature cretobj;
            cretobj = null;
            result = false;
            for (k = x - checkrange; k <= x + checkrange; k++)
            {
                for (m = y - checkrange; m <= y + checkrange; m++)
                {
                    inrange = GetMapXY(k, m, ref pm);
                    if (inrange)
                    {
                        if (pm.OBJList != null)
                        {
                            for (i = pm.OBJList.Count - 1; i >= 0; i--)
                            {
                                if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_MOVINGOBJECT)
                                {
                                    cretobj = (TCreature)((TAThing)pm.OBJList[i]).AObject;
                                    if (cretobj.BoAnimal && cretobj.Death && (!cretobj.BoSkeleton))
                                    {
                                        // 啊规俊 酒捞袍捞 乐芭唱 付瘤阜 矫眉捞搁 弊 矫眉甫 逞败淋 酒聪搁 促澜 矫眉 八荤.
                                        if (cretobj.ItemList.Count > 0)
                                        {
                                            cret = cretobj;
                                            result = true;
                                            return result;
                                        }
                                        else
                                        {
                                            // 捞傈 八祸茄 矫眉甫 持绢淋
                                            cret = cretobj;
                                            result = true;
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

        // (sonmg 2004/12/28)
        public TMapItem GetItem(int x, int y)
        {
            TMapItem result;
            TMapInfo pm=null;
            int i;
            bool inrange;
            Object obj;
            result = null;
            BoCanGetItem = false;
            inrange = GetMapXY(x, y, ref pm);
            if (inrange)
            {
                if (pm.MoveAttr == Grobal2.MP_CANMOVE)
                {
                    BoCanGetItem = true;
                    if (pm.OBJList != null)
                    {
                        for (i = pm.OBJList.Count - 1; i >= 0; i--)
                        {
                            if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_ITEMOBJECT)
                            {
                                result = (TMapItem)((TAThing)pm.OBJList[i]).AObject;
                                break;
                            }
                            if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_GATEOBJECT)
                            {
                                BoCanGetItem = false;
                                // 酒捞袍捞 巩俊 尝瘤 给窍霸...
                            }
                            if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_MOVINGOBJECT)
                            {
                                obj = ((TAThing)pm.OBJList[i]).AObject;
                                if (!((TCreature)obj).Death)
                                {
                                    BoCanGetItem = false;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public TMapItem GetItemEx(int x, int y, ref int itemcount)
        {
            TMapItem result;
            TMapInfo pm=null;
            int i;
            bool inrange;
            Object obj;
            result = null;
            itemcount = 0;
            BoCanGetItem = false;
            inrange = GetMapXY(x, y, ref pm);
            if (inrange)
            {
                if (pm.MoveAttr == Grobal2.MP_CANMOVE)
                {
                    BoCanGetItem = true;
                    if (pm.OBJList != null)
                    {
                        for (i = 0; i < pm.OBJList.Count; i++)
                        {
                            if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_ITEMOBJECT)
                            {
                                result = (TMapItem)((TAThing)pm.OBJList[i]).AObject;
                                itemcount++;
                                // break;
                            }
                            if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_GATEOBJECT)
                            {
                                BoCanGetItem = false;
                                // 酒捞袍捞 巩俊 尝瘤 给窍霸...
                            }
                            if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_MOVINGOBJECT)
                            {
                                obj = ((TAThing)pm.OBJList[i]).AObject;
                                if (!((TCreature)obj).Death)
                                {
                                    BoCanGetItem = false;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }

        public Object GetEvent(int x, int y)
        {
            Object result;
            TMapInfo pm=null;
            int i;
            bool inrange;
            result = null;
            BoCanGetItem = false;
            inrange = GetMapXY(x, y, ref pm);
            if (inrange && (pm.OBJList != null))
            {
                for (i = pm.OBJList.Count - 1; i >= 0; i--)
                {
                    if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_EVENTOBJECT)
                    {
                        result = ((TAThing)pm.OBJList[i]).AObject;
                        break;
                    }
                }
            }
            return result;
        }

        public int GetDupCount(int x, int y)
        {
            int result;
            TMapInfo pm=null;
            int i;
            int arr;
            bool inrange;
            Object obj;
            result = 0;
            arr = 0;
            inrange = GetMapXY(x, y, ref pm);
            if (inrange)
            {
                if (pm.OBJList != null)
                {
                    for (i = 0; i < pm.OBJList.Count; i++)
                    {
                        if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_MOVINGOBJECT)
                        {
                            obj = ((TAThing)pm.OBJList[i]).AObject;
                            // 磊府 瞒瘤
                            // 救焊捞绊 见绢 乐绰 葛靛
                            // 家券各篮 眉农 救窃(sonmg 2005/08/25)
                            // 皑矫磊葛靛
                            if ((!((TCreature)obj).BoGhost) && ((TCreature)obj).HoldPlace && (!((TCreature)obj).Death) && (!((TCreature)obj).HideMode) && (((TCreature)obj).Master == null) && (!((TCreature)obj).BoSuperviserMode))
                            {
                                arr++;
                            }
                        }
                    }
                }
            }
            result = arr;
            return result;
        }

        public void GetMarkMovement(int x, int y, bool bocanmove)
        {
            TMapInfo pm=null;
            bool inrange;
            inrange = GetMapXY(x, y, ref pm);
            if (inrange)
            {
                if (bocanmove)
                {
                    // 框流老 荐 乐霸
                    pm.MoveAttr = Grobal2.MP_CANMOVE;
                }
                else
                {
                    pm.MoveAttr = Grobal2.MP_HIGHWALL;
                }
                // 给 框流捞霸
            }
        }

        public bool CanWalk(int x, int y, bool allowdup)
        {
            bool result;
            TMapInfo pm=null;
            int i;
            TCreature cret;
            bool inrange;
            result = false;
            // out of range
            inrange = GetMapXY(x, y, ref pm);
            if (inrange)
            {
                if (pm.MoveAttr == Grobal2.MP_CANMOVE)
                {
                    result = true;
                    if (!allowdup)
                    {
                        if (pm.OBJList != null)
                        {
                            for (i = 0; i < pm.OBJList.Count; i++)
                            {
                                if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_MOVINGOBJECT)
                                {
                                    cret = (TCreature)((TAThing)pm.OBJList[i]).AObject;
                                    if (cret != null)
                                    {
                                        // 磊府 瞒瘤
                                        // 救焊捞绊 见绢 乐绰 葛靛
                                        // 皑矫磊葛靛
                                        if ((!cret.BoGhost) && cret.HoldPlace && (!cret.Death) && (!cret.HideMode) && (!cret.BoSuperviserMode))
                                        {
                                            result = false;
                                            break;
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

        public bool CanFireFly(int x, int y)
        {
            bool result;
            TMapInfo pm=null;
            bool inrange;
            result = true;
            inrange = GetMapXY(x, y, ref pm);
            if (inrange)
            {
                if (pm.MoveAttr == Grobal2.MP_HIGHWALL)
                {
                    result = false;
                }
            }
            return result;
        }

        public bool CanFly(int x, int y, int dx, int dy)
        {
            int rx;
            int ry;
            bool result = true;
            double stepx = (dx - x) / 10;
            double stepy = (dy - y) / 10;
            for (var i = 0; i <= 9; i++)
            {
                rx = HUtil32.MathRound(x + stepx);
                ry = HUtil32.MathRound(y + stepy);
                if (!CanWalk(rx, ry, true))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        public bool CanSafeWalk(int x, int y)
        {
            bool result;
            TMapInfo pm=null;
            int i;
            bool inrange;
            TEvent __event;
            result = true;
            inrange = GetMapXY(x, y, ref pm);
            if (inrange && (pm.OBJList != null))
            {
                for (i = pm.OBJList.Count - 1; i >= 0; i--)
                {
                    if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_EVENTOBJECT)
                    {
                        __event = (TEvent)((TAThing)pm.OBJList[i]).AObject;
                        if (__event.Damage > 0)
                        {
                            result = false;
                        }
                    }
                }
            }
            return result;
        }

        // -1: can't move(map don't movable),  0: can't move,  1: can move
        public int MoveToMovingObject(int x, int y, Object obj, int nx, int ny, bool allowdup)
        {
            int result;
            TMapInfo pm=null;
            TAThing pthing;
            bool inrange;
            bool canmove;
            int i;
            TCreature cret;
            int Down;
            result = 0;
            Down = 0;
            try
            {
                canmove = true;
                if (!allowdup)
                {
                    inrange = GetMapXY(nx, ny, ref pm);
                    // 捞悼且 磊府啊 蜡瓤茄瘤 八荤
                    if (inrange && (pm != null))
                    {
                        Down = 1;
                        if (pm.MoveAttr == Grobal2.MP_CANMOVE)
                        {
                            Down = 2;
                            if (pm.OBJList != null)
                            {
                                Down = 3;
                                for (i = 0; i < pm.OBJList.Count; i++)
                                {
                                    Down = 4;
                                    if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_MOVINGOBJECT)
                                    {
                                        Down = 5;
                                        cret = (TCreature)((TAThing)pm.OBJList[i]).AObject;
                                        if (cret != null)
                                        {
                                            Down = 6;
                                            // 磊府甫 瞒瘤窍绰 葛靛
                                            // 顶加粱厚殿 (救焊捞绰 葛靛)
                                            // 皑矫磊 葛靛
                                            if ((!cret.BoGhost) && cret.HoldPlace && (!cret.Death) && (!cret.HideMode) && (!cret.BoSuperviserMode))
                                            {
                                                canmove = false;
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            result = -1;
                            canmove = false;
                        }
                    }
                }
                Down = 10;
                if (canmove)
                {
                    inrange = GetMapXY(nx, ny, ref pm);
                    if (!inrange || (pm.MoveAttr != Grobal2.MP_CANMOVE))
                    {
                        result = -1;
                    }
                    else
                    {
                        Down = 11;
                        inrange = GetMapXY(x, y, ref pm);
                        if (inrange)
                        {
                            Down = 12;
                            if (pm.OBJList != null)
                            {
                                i = 0;
                                while (true)
                                {
                                    Down = 13;
                                    if (i >= pm.OBJList.Count)
                                    {
                                        break;
                                    }
                                    pthing = (TAThing)pm.OBJList[i];
                                    if ((pthing.Shape == Grobal2.OS_MOVINGOBJECT) && (pthing.AObject == obj))
                                    {
                                        Down = 14;
                                        pm.OBJList.RemoveAt(i);
                                        Down = 142;
                                        try
                                        {
                                            Dispose(pthing);
                                        }
                                        catch
                                        {
                                            svMain.MainOutMessage("DO NOT DISPOSE pthing");
                                        }
                                        if (pm.OBJList.Count <= 0)
                                        {
                                            Down = 15;
                                            try
                                            {
                                                pm.OBJList.Free();
                                            }
                                            finally
                                            {
                                                pm.OBJList = null;
                                            }
                                            break;
                                        }
                                        continue;
                                    }
                                    i++;
                                }
                            }
                        }
                        inrange = GetMapXY(nx, ny, ref pm);
                        if (inrange)
                        {
                            Down = 16;
                            if (pm.OBJList == null)
                            {
                                pm.OBJList = new ArrayList();
                            }
                            Down = 17;
                            try
                            {
                                // 咯扁辑 俊矾啊 唱绰 版快啊 惯积
                                pthing = new TAThing();
                            }
                            catch
                            {
                                svMain.MainOutMessage("DO NOT MEW PTHING");
                                pthing = null;
                            }
                            if (pthing != null)
                            {
                                pthing.Shape = Grobal2.OS_MOVINGOBJECT;
                                Down = 18;
                                pthing.AObject = obj;
                                pthing.ATime = GetTickCount;
                                // 甘俊 眠啊等 矫埃
                                Down = 19;
                                pm.OBJList.Add(pthing);
                                result = 1;
                                // 捞悼 啊瓷
                            }
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[TEnvirnoment] MoveToMovingObject exception " + MapName + "<" + nx.ToString() + ":" + ny.ToString() + ">" + Down.ToString());
            }
            return result;
        }

        public object AddToMap(int x, int y, byte objtype, Object obj)
        {
            object result;
            TMapInfo pm=null;
            TAThing pthing;
            TAThing pthingtemp;
            TMapItem pmitem;
            bool inrange;
            bool flag;
            int i;
            int cnt;
            TStdItem ps;
            int ItemObjCount;
            result = null;
            // out of range 捞芭唱, 角菩 沁阑 版快
            ps = null;
            try
            {
                inrange = GetMapXY(x, y, ref pm);
                flag = false;
                if (inrange)
                {
                    if (pm.MoveAttr == Grobal2.MP_CANMOVE)
                    {
                        if (pm.OBJList == null)
                        {
                            pm.OBJList = new ArrayList();
                        }
                        else
                        {
                            if (objtype == Grobal2.OS_ITEMOBJECT)
                            {
                                // '陛傈'
                                if (((TMapItem)obj).Name == Units.Envir.NAME_OF_GOLD)
                                {
                                    for (i = 0; i < pm.OBJList.Count; i++)
                                    {
                                        pthing = (TAThing)pm.OBJList[i];
                                        if (pthing.Shape == Grobal2.OS_ITEMOBJECT)
                                        {
                                            pmitem = (TMapItem)((TAThing)pm.OBJList[i]).AObject;
                                            // '陛傈'
                                            if (pmitem.Name == Units.Envir.NAME_OF_GOLD)
                                            {
                                                cnt = pmitem.Count + ((TMapItem)obj).Count;
                                                if (cnt <= ObjBase.BAGGOLD)
                                                {
                                                    pmitem.Count = cnt;
                                                    pmitem.Looks = GetGoldLooks(cnt);
                                                    pmitem.AniCount = 0;
                                                    pmitem.Reserved = 0;
                                                    pthing.ATime = GetTickCount;
                                                    // 矫埃 犁汲沥
                                                    result = pmitem;
                                                    // 捞固 乐绰 巴捞搁 弊 器牢磐甫 搬苞蔼栏肺 焊晨
                                                    flag = true;
                                                }
                                            }
                                        }
                                    }
                                }
                                if (!flag)
                                {
                                    ps = svMain.UserEngine.GetStdItem(((TMapItem)obj).UserItem.Index);
                                }
                                if ((ps != null) && (ps.StdMode == ObjBase.STDMODE_OF_DECOITEM) && (ps.Shape == ObjBase.SHAPE_OF_DECOITEM))
                                {
                                    ItemObjCount = 0;
                                    for (i = 0; i < pm.OBJList.Count; i++)
                                    {
                                        pthingtemp = (TAThing)pm.OBJList[i];
                                        if (pthingtemp.Shape == Grobal2.OS_ITEMOBJECT)
                                        {
                                            ItemObjCount++;
                                        }
                                    }
                                    // 惑泅林赣聪绰 1俺 捞惑 给 阶绰促.
                                    if (ItemObjCount >= 1)
                                    {
                                        // 1俺 捞惑 给 阶绰促.
                                        result = null;
                                        // PTMapItem(PTAThing(pm.ObjList[i]).AObject); //俊矾烙..
                                        flag = true;
                                    }
                                }
                                else
                                {
                                    ItemObjCount = 0;
                                    for (i = 0; i < pm.OBJList.Count; i++)
                                    {
                                        pthingtemp = (TAThing)pm.OBJList[i];
                                        if (pthingtemp.Shape == Grobal2.OS_ITEMOBJECT)
                                        {
                                            ItemObjCount++;
                                        }
                                    }
                                    // 老馆 酒捞袍篮 5俺 捞惑 给 阶绰促.
                                    if (ItemObjCount >= 5)
                                    {
                                        // 歹 捞惑 给 阶绰促.(5俺 力茄)
                                        result = null;
                                        // PTMapItem(PTAThing(pm.ObjList[i]).AObject); //俊矾烙..
                                        flag = true;
                                    }
                                }
                            }
                            if (objtype == Grobal2.OS_EVENTOBJECT)
                            {
                            }
                        }
                        if (!flag)
                        {
                            pthing = new TAThing();
                            pthing.Shape = objtype;
                            pthing.AObject = obj;
                            // TCreature(obj), PTUseItem(obj)
                            pthing.ATime = GetTickCount;
                            // 甘俊 眠啊等 矫埃
                            pm.OBJList.Add(pthing);
                            result = obj;
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[TEnvirnoment] AddToMap exception");
            }
            return result;
        }

        public object AddToMapMineEvnet(int x, int y, byte objtype, Object obj)
        {
            object result;
            TMapInfo pm=null;
            TAThing pthing;
            bool inrange;
            bool flag;
            result = null;
            // out of range 捞芭唱, 角菩 沁阑 版快
            try
            {
                inrange = GetMapXY(x, y, ref pm);
                flag = false;
                if (inrange)
                {
                    // 捞悼 给窍绰 镑俊档 缴绰促.
                    if (pm.MoveAttr != Grobal2.MP_CANMOVE)
                    {
                        if (pm.OBJList == null)
                        {
                            pm.OBJList = new ArrayList();
                        }
                        else
                        {
                            if (objtype == Grobal2.OS_EVENTOBJECT)
                            {
                            }
                        }
                        if (!flag)
                        {
                            pthing = new TAThing();
                            pthing.Shape = objtype;
                            pthing.AObject = obj;
                            // TCreature(obj), PTUseItem(obj)
                            pthing.ATime = GetTickCount;
                            // 甘俊 眠啊等 矫埃
                            pm.OBJList.Add(pthing);
                            result = obj;
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[TEnvirnoment] AddToMapMineEvent exception");
            }
            return result;
        }

        public object AddToMapTreasure(int x, int y, byte objtype, Object obj)
        {
            object result;
            TMapInfo pm=null;
            TAThing pthing;
            bool inrange;
            bool flag;
            result = null;
            // out of range 捞芭唱, 角菩 沁阑 版快
            try
            {
                inrange = GetMapXY(x, y, ref pm);
                flag = false;
                if (inrange)
                {
                    // 捞悼 给窍绰 镑俊档 缴绰促.
                    if (pm.OBJList == null)
                    {
                        pm.OBJList = new ArrayList();
                    }
                    else
                    {
                        if (objtype == Grobal2.OS_EVENTOBJECT)
                        {
                        }
                    }
                    if (!flag)
                    {
                        pthing = new TAThing();
                        pthing.Shape = objtype;
                        pthing.AObject = obj;
                        // TCreature(obj), PTUseItem(obj)
                        pthing.ATime = GetTickCount;
                        // 甘俊 眠啊等 矫埃
                        pm.OBJList.Add(pthing);
                        result = obj;
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[TEnvirnoment] AddToMapMineEvent exception");
            }
            return result;
        }

        public int DeleteFromMap(int x, int y, byte objtype, Object obj)
        {
            int result;
            TMapInfo pm=null;
            int i;
            TAThing pthing;
            bool inrange;
            result = -1;
            // FALSE;
            try
            {
                inrange = GetMapXY(x, y, ref pm);
                if (inrange)
                {
                    if (pm != null)
                    {
                        try
                        {
                            if (pm.OBJList != null)
                            {
                                i = 0;
                                while (true)
                                {
                                    if (i >= pm.OBJList.Count)
                                    {
                                        break;
                                    }
                                    pthing = (TAThing)pm.OBJList[i];
                                    if (pthing != null)
                                    {
                                        if ((objtype == pthing.Shape) && (obj == pthing.AObject))
                                        {
                                            pm.OBJList.RemoveAt(i);
                                            Dispose(pthing);
                                            result = 1;
                                            // TRUE;
                                            if (pm.OBJList.Count <= 0)
                                            {
                                                pm.OBJList.Free();
                                                pm.OBJList = null;
                                                break;
                                            }
                                            continue;
                                        }
                                    }
                                    else
                                    {
                                        pm.OBJList.RemoveAt(i);
                                        if (pm.OBJList.Count <= 0)
                                        {
                                            pm.OBJList.Free();
                                            pm.OBJList = null;
                                            break;
                                        }
                                        continue;
                                    }
                                    i++;
                                }
                            }
                            else
                            {
                                result = -2;
                            }
                        }
                        catch
                        {
                            pm = null;
                            svMain.MainOutMessage("[TEnvirnoment] DeleteFromMap -> Except 1 **" + objtype.ToString());
                        }
                    }
                    else
                    {
                        result = -3;
                    }
                }
                else
                {
                    result = 0;
                }
            }
            catch
            {
                svMain.MainOutMessage("[TEnvirnoment] DeleteFromMap -> Except 2 **" + objtype.ToString());
            }
            return result;
        }

        public void VerifyMapTime(int x, int y, Object obj)
        {
            TMapInfo pm=null;
            int i;
            TAThing pthing;
            bool inrange;
            try
            {
                inrange = GetMapXY(x, y, ref pm);
                if (inrange)
                {
                    if (pm != null)
                    {
                        if (pm.OBJList != null)
                        {
                            for (i = 0; i < pm.OBJList.Count; i++)
                            {
                                pthing = (TAThing)pm.OBJList[i];
                                if ((pthing.Shape == Grobal2.OS_MOVINGOBJECT) && (pthing.AObject == obj))
                                {
                                    pthing.ATime = GetTickCount;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[TEnvirnoment] VerifyMapTime exception");
            }
        }

        public void ApplyDoors()
        {
            TDoorInfo pd;
            for (var i = 0; i < DoorList.Count; i++)
            {
                pd = DoorList[i] as TDoorInfo;
                if (null == AddToMap(pd.DoorX, pd.DoorY, Grobal2.OS_DOOR, pd))
                {
                    // MainOutMessage('NOT ApplyDoors'+MapName+','+IntTostr(pd.DoorX)+','+IntTostr( pd.DoorY ));
                }
            }
        }

        public TDoorInfo FindDoor(int x, int y)
        {
            TDoorInfo result = null;
            for (var i = 0; i < DoorList.Count; i++)
            {
                if (((DoorList[i] as TDoorInfo).DoorX == x) && ((DoorList[i] as TDoorInfo).DoorY == y))
                {
                    result = DoorList[i] as TDoorInfo;
                    break;
                }
            }
            return result;
        }

        public bool AroundDoorOpened(int x, int y)
        {
            bool result;
            int i;
            result = true;
            try
            {
                for (i = 0; i < DoorList.Count; i++)
                {
                    if ((Math.Abs((DoorList[i] as TDoorInfo).DoorX - x) <= 1) && (Math.Abs((DoorList[i] as TDoorInfo).DoorY - y) <= 1))
                    {
                        if (!(DoorList[i] as TDoorInfo).PCore.DoorOpenState)
                        {
                            result = false;
                            break;
                        }
                    }
                }
            }
            catch
            {
                svMain.MainOutMessage("[TEnvirnoment] AroundDoorOpened exception");
            }
            return result;
        }

        public bool AddMapQuest(int set1, int val1, string monname, string itemname, string qfile, bool enablegroup)
        {
            bool result;
            TMapQuestInfo mqi;
            TMerchant npc;
            result = false;
            if (set1 >= 0)
            {
                mqi = new TMapQuestInfo();
                mqi.SetNumber = set1;
                if (val1 > 1)
                {
                    val1 = 1;
                }
                mqi.Value = val1;
                if (monname == "*")
                {
                    monname = "";
                }
                mqi.MonName = monname;
                if (itemname == "*")
                {
                    itemname = "";
                }
                mqi.ItemName = itemname;
                if (qfile == "*")
                {
                    qfile = "";
                }
                mqi.EnableGroup = enablegroup;
                npc = new TMerchant();
                npc.MapName = "0";
                npc.CX = 0;
                npc.CY = 0;
                npc.UserName = qfile;
                npc.NpcFace = 0;
                npc.Appearance = 0;
                npc.DefineDirectory = LocalDB.MAPQUESTDIR;
                npc.BoInvisible = true;
                npc.BoUseMapFileName = false;
                svMain.UserEngine.NpcList.Add(npc);
                mqi.QuestNpc = npc;
                MapQuestList.Add(mqi);
                result = true;
            }
            return result;
        }

        public bool HasMapQuest()
        {
            bool result;
            if (MapQuestList.Count > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }

        public Object GetMapQuest(Object who, string monname, string itemname, bool groupcall)
        {
            Object result;
            int i;
            int qval;
            TMapQuestInfo mqi;
            bool flag;
            result = null;
            for (i = 0; i < MapQuestList.Count; i++)
            {
                mqi = MapQuestList[i];
                qval = ((TCreature)who).GetQuestMark(mqi.SetNumber);
                if ((qval == mqi.Value) && ((groupcall == mqi.EnableGroup) || !groupcall))
                {
                    flag = false;
                    if ((mqi.MonName != "") && (mqi.ItemName != ""))
                    {
                        // die or pickup
                        if ((mqi.MonName == monname) && (mqi.ItemName == itemname))
                        {
                            flag = true;
                        }
                    }
                    if ((mqi.MonName != "") && (mqi.ItemName == ""))
                    {
                        // die
                        if ((mqi.MonName == monname) && (itemname == ""))
                        {
                            flag = true;
                        }
                        // (sonmg 2005/06/29)
                        if ((mqi.MonName == "~") && (monname == ""))
                        {
                            flag = true;
                        }
                    }
                    if ((mqi.MonName == "") && (mqi.ItemName != ""))
                    {
                        // pickup
                        if (mqi.ItemName == itemname)
                        {
                            flag = true;
                        }
                        // (sonmg 2005/06/29)
                        if ((mqi.ItemName == "~") && (itemname == ""))
                        {
                            flag = true;
                        }
                    }
                    if (flag)
                    {
                        result = mqi.QuestNpc;
                        break;
                    }
                }
            }
            return result;
        }

        public string GetGuildAgitRealMapName()
        {
            string result = MapName;
            if (GuildAgit > -1)
            {
                result = MapName[1] + MapName[2] + MapName[3];
            }
            if (UseNewMap)
            {
                result = NewMapName;
            }
            return result;
        }

        public object GetMovingObject(int x, int y, bool flag)
        {
            TMapInfo pm =null;
            object result = null;
            bool inrange = GetMapXY(x, y, ref pm);
            if (inrange && (pm != null))
            {
                if (pm.OBJList != null)
                {
                    for (var i = pm.OBJList.Count - 1; i >= 0; i--)
                    {
                        if (((TAThing)pm.OBJList[i]).Shape == Grobal2.OS_MOVINGOBJECT)
                        {
                            TCreature cret = (TCreature)((TAThing)pm.OBJList[i]).AObject;
                            if (cret != null)
                            {
                                if ((!cret.BoGhost) && cret.HoldPlace && (!flag || !cret.Death) && (!cret.HideMode) && (!cret.BoSuperviserMode))
                                {
                                    result = cret;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}

