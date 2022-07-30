using System;

namespace GameSvr
{
    public struct TMiniMapInfo
    {
        public string Map;
        public int Idx;
    } 

    public struct TDoorCore
    {
        public bool DoorOpenState;
        // true: open  false: closed
        public bool __Lock;
        public int LockKey;
        public long OpenTime;
    }

    public struct TDoorInfo
    {
        public int DoorX;
        public int DoorY;
        public int DoorNumber;
        public TDoorCore PCore;
    }

    public struct TMapPrjInfo
    {
        public string[] Ident;
        public int ColCount;
        public int RowCount;
    } 

    public struct TMapHeader
    {
        public ushort Width;
        public ushort Height;
        public string[] Title;
        public DateTime UpdateDate;
        public char[] Reserved;
    } 

    public struct TMapHeader_AntiHack
    {
        public string[] Title;
        public ushort Width;
        public ushort CheckKey;
        public ushort Height;
        public DateTime UpdateDate;
        public char[] Reserved;
    } 

    public struct TMapFileInfo
    {
        public ushort BkImg;
        public ushort MidImg;
        public ushort FrImg;
        public byte DoorIndex;
        public byte DoorOffset;
        public byte AniFrame;
        public byte AniTick;
        public byte Area;
        public byte light;
    } 

    public struct TMapQuestInfo
    {
        public int SetNumber;
        public int Value;
        public string MonName;
        public string ItemName;
        public bool EnableGroup;
        public Object QuestNpc;
    }
}

namespace GameSvr
{
    public class Envir
    {
        public const int MAX_MINIMAP = 65;
        public const string NAME_OF_GOLD = "½ð±Ò";
        public const string NAME_OF_MONEY = "Ôª±¦";
        public const double MaxListSize = 1000 * 1000 - 1;
    }
}

