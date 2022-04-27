using MirClient.MirObjects;

namespace MirClient.Maps
{
    public class CellInfo
    {
        public ushort BackIndex;
        public int BackImage;
        public ushort MiddleIndex;
        public int MiddleImage;
        public ushort FrontIndex;
        public int FrontImage;
        public byte DoorIndex;
        public byte DoorOffset;
        public byte btAniFrame;
        public byte btAniTick;
        public byte btArea;
        public byte Light;
        public byte TitleIndex;
        public byte btsmTiles;
        public ushort wBkImg2;
        public ushort wMidImg2;
        public ushort wFrImg2;
        public byte btDoorIndex2;
        public byte btDoorOffset2;
        public ushort wAniFrame2;
        public byte btArea2;
        public byte btLight2;
        public byte btTiles2;
        public byte btsmTiles2;

        public byte FrontAnimationFrame;
        public byte FrontAnimationTick;

        public short TileAnimationImage;
        public short TileAnimationOffset;
        public byte TileAnimationFrames;


        public List<MapObject> CellObjects;

        public void AddObject(MapObject ob)
        {
            if (CellObjects == null) CellObjects = new List<MapObject>();

            CellObjects.Insert(0, ob);
            Sort();
        }

        public void RemoveObject(MapObject ob)
        {
            if (CellObjects == null) return;

            CellObjects.Remove(ob);

            if (CellObjects.Count == 0) CellObjects = null;
            else Sort();
        }

        public MapObject FindObject(uint ObjectID) => CellObjects.Find(delegate (MapObject mo)
                                                                   {
                                                                       return mo.ObjectID == ObjectID;
                                                                   });

        public void DrawObjects()
        {
            if (CellObjects == null) return;

            for (int i = 0; i < CellObjects.Count; i++)
            {
                if (!CellObjects[i].Dead)
                {
                    CellObjects[i].Draw();
                    continue;
                }

                if (CellObjects[i].Race == ObjectType.Monster)
                {
                    switch (((MonsterObject)CellObjects[i]).BaseImage)
                    {
                        case Monster.PalaceWallLeft:
                        case Monster.PalaceWall1:
                        case Monster.PalaceWall2:
                        case Monster.SSabukWall1:
                        case Monster.SSabukWall2:
                        case Monster.SSabukWall3:
                        case Monster.HellLord:
                            CellObjects[i].Draw();
                            break;
                        default:
                            continue;
                    }
                }
            }
        }

        public void DrawDeadObjects()
        {
            if (CellObjects == null) return;
            for (int i = 0; i < CellObjects.Count; i++)
            {
                if (!CellObjects[i].Dead) continue;

                if (CellObjects[i].Race == ObjectType.Monster)
                {
                    switch (((MonsterObject)CellObjects[i]).BaseImage)
                    {
                        case Monster.PalaceWallLeft:
                        case Monster.PalaceWall1:
                        case Monster.PalaceWall2:
                        case Monster.SSabukWall1:
                        case Monster.SSabukWall2:
                        case Monster.SSabukWall3:
                        case Monster.HellLord:
                            continue;
                    }
                }
                CellObjects[i].Draw();
            }
        }

        public void Sort()
        {
            CellObjects.Sort(delegate (MapObject ob1, MapObject ob2)
            {
                if (ob1.Race == ObjectType.Item && ob2.Race != ObjectType.Item)
                    return -1;
                if (ob2.Race == ObjectType.Item && ob1.Race != ObjectType.Item)
                    return 1;
                if (ob1.Race == ObjectType.Spell && ob2.Race != ObjectType.Spell)
                    return -1;
                if (ob2.Race == ObjectType.Spell && ob1.Race != ObjectType.Spell)
                    return 1;

                int i = ob2.Dead.CompareTo(ob1.Dead);
                return i == 0 ? ob1.ObjectID.CompareTo(ob2.ObjectID) : i;
            });
        }

        public static CellInfo ToMap(byte[] buffer)
        {
            var cellInfo = new CellInfo();
            var binaryReader = new BinaryReader(new MemoryStream(buffer));
            cellInfo.BackIndex = binaryReader.ReadUInt16();
            cellInfo.MiddleIndex = binaryReader.ReadUInt16();
            cellInfo.FrontIndex = binaryReader.ReadUInt16();
            cellInfo.DoorIndex = binaryReader.ReadByte();
            cellInfo.DoorOffset = binaryReader.ReadByte();
            cellInfo.btAniFrame = binaryReader.ReadByte();
            cellInfo.btAniTick = binaryReader.ReadByte();
            cellInfo.btArea = binaryReader.ReadByte();
            cellInfo.Light = binaryReader.ReadByte();
            if (buffer.Length > 12)
            {
                cellInfo.TitleIndex = binaryReader.ReadByte();
                cellInfo.btsmTiles = binaryReader.ReadByte();
                cellInfo.wBkImg2 = binaryReader.ReadUInt16();
                cellInfo.wMidImg2 = binaryReader.ReadUInt16();
                cellInfo.wFrImg2 = binaryReader.ReadUInt16();
                cellInfo.btDoorIndex2 = binaryReader.ReadByte();
                cellInfo.btDoorOffset2 = binaryReader.ReadByte();
                cellInfo.wAniFrame2 = binaryReader.ReadUInt16();
                cellInfo.btArea2 = binaryReader.ReadByte();
                cellInfo.btLight2 = binaryReader.ReadByte();
                cellInfo.btTiles2 = binaryReader.ReadByte();
                cellInfo.btsmTiles2 = binaryReader.ReadByte();
            }
            return cellInfo;
        }
    }
}