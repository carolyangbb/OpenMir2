namespace MirClient.Maps
{
    internal class MapReader
    {
        public int Width, Height;
        public CellInfo[,] MapCells;
        private string FileName;
        private FileStream fileStream;
        public TMapHeader m_MapHeader;
        public int m_nCurUnitX;
        public int m_nCurUnitY;
        public int m_nBlockLeft;
        public int m_nBlockTop;
        public int m_nOldLeft;
        public int m_nOldTop;
        public string m_sCurrentMap;
        public string m_sOldMap;
        public static int OffSetX;
        public static int OffSetY;
        const int LOGICALMAPUNIT = 40;
        private byte[] Bytes;

        public MapReader(string FileName)
        {
            this.FileName = FileName;
            LoadMap(FileName, 333, 333);
        }

        public void LoadMap(string sMapName, int nMx, int nMy)
        {
            if (fileStream != null)
            {
                fileStream.Close();
                fileStream.Dispose();
                MapCells = null;
            }
            m_sCurrentMap = sMapName;
            var sFileName = Path.Combine(Settings.MapPath, $"{sMapName}.map");

            Bytes = File.ReadAllBytes(sFileName);

            fileStream = new FileStream(sFileName, FileMode.Open, FileAccess.Read);
            var buffer = new byte[TMapHeader.PackSize];
            fileStream.Read(buffer, 0, buffer.Length);
            m_MapHeader = new TMapHeader(buffer);
            MapCells = new CellInfo[m_MapHeader.wWidth, m_MapHeader.wHeight];
            for (int x = 0; x < m_MapHeader.wWidth; x++)
            {
                for (int y = 0; y < m_MapHeader.wHeight; y++)
                {
                    MapCells[x, y] = new CellInfo();
                }
            }
            UpdateMapPos(nMx, nMy);
        }

        public void UpdateMapPos(int nX, int nY)
        {
            var cX = nX / LOGICALMAPUNIT;
            var cY = nY / LOGICALMAPUNIT;
            m_nBlockLeft = _MAX(0, (cX - 1) * LOGICALMAPUNIT);
            m_nBlockTop = _MAX(0, (cY - 1) * LOGICALMAPUNIT);
            UpdateMapSquare(cX, cY);
            if ((m_nOldLeft != m_nBlockLeft) || (m_nOldTop != m_nBlockTop) || (m_sOldMap != m_sCurrentMap))
            {
                if (m_sCurrentMap == "3")
                {
                    Unmark(nX, nY, 624, 278);
                    Unmark(nX, nY, 627, 278);
                    Unmark(nX, nY, 634, 271);
                    Unmark(nX, nY, 564, 287);
                    Unmark(nX, nY, 564, 286);
                    Unmark(nX, nY, 661, 277);
                    Unmark(nX, nY, 578, 296);
                }
            }
            m_sOldMap = m_sCurrentMap;
            m_nOldLeft = m_nBlockLeft;
            m_nOldTop = m_nBlockTop;
        }


        private void UpdateMapSquare(int cX, int cY)
        {
            if ((cX != m_nCurUnitX) || (cY != m_nCurUnitY))
            {
                LoadMapArr(cX, cY);
                m_nCurUnitX = cX;
                m_nCurUnitY = cY;
            }
        }

        /// <summary>
        /// 加载地图段数据,以当前座标为准
        /// </summary>
        /// <param name="cX"></param>
        /// <param name="cY"></param>
        private void LoadMapArr(int cX, int cY)
        {
            if (m_sCurrentMap.Length > 0)
            {
                var nLx = (cX - 1) * LOGICALMAPUNIT;
                var nRx = (cX + 2) * LOGICALMAPUNIT;
                var nTy = (cY - 1) * LOGICALMAPUNIT;
                var nBy = (cY + 2) * LOGICALMAPUNIT;

                if (nLx < 0) { nLx = 0; }
                if (nTy < 0) { nTy = 0; }
                if (nBy >= m_MapHeader.wHeight) nBy = m_MapHeader.wHeight = 0;
                int nAline = 0;

                if ((Bytes[4] == 0x0F) || (Bytes[4] == 0x03) && (Bytes[18] == 0x0D) && (Bytes[19] == 0x0A))
                {
                    int W = Bytes[0] + (Bytes[1] << 8);
                    int H = Bytes[2] + (Bytes[3] << 8);
                    if (Bytes.Length > (52 + (W * H * 14)))
                    {
                        LoadMapType3();
                        return;
                    }
                    else
                    {
                        LoadMapType2();
                        return;
                    }
                }

                switch (m_MapHeader.Reserved[0])
                {
                    case 6:
                        nAline = 12 * m_MapHeader.wHeight;
                        for (int i = nLx; i < nRx - 1; i++)
                        {
                            if ((i >= 0) && (i < m_MapHeader.wWidth))
                            {
                                //FileSeek(m_nCurrentMap, SizeOf(TMapHeader) + (nAline * i) + (SizeOf(TMapInfo) * nTy), 0);
                                //FileRead(m_nCurrentMap, m_MArr[i - nLx, 0], SizeOf(TMapInfo) * (nBy - nTy));
                            }
                        }
                        break;
                    default:
                        //nAline = 12 * m_MapHeader.wHeight;
                        //for (int i = nLx; i < nRx; i++)
                        //{
                        //    if (i >= 0 && i < m_MapHeader.wWidth)
                        //    {
                        //        var len = ((TMapHeader.PackSize + (nAline * i)) + (12 * nTy));
                        //        fileStream.Position = len;
                        //        if (fileStream.Position == len)
                        //        {
                        //            for (int j = 0; j < nBy - nTy; j++)
                        //            {
                        //                if (i - nLx == 42 && j == 44)
                        //                {
                        //                    fileStream.Position = len;
                        //                }
                        //                var mapBuffer = new byte[12];
                        //                fileStream.Read(mapBuffer, 0, 12);
                        //                MapCells[i - nLx, j] = CellInfo.ToMap(mapBuffer);
                        //            }
                        //        }
                        //    }
                        //}
                        int offset = 0;
                        var Width = BitConverter.ToInt16(Bytes, offset);
                        offset += 2;
                        var Height = BitConverter.ToInt16(Bytes, offset);
                        MapCells = new CellInfo[Width, Height];
                        offset = 52;
                        for (int x = 0; x < Width; x++)
                            for (int y = 0; y < Height; y++)
                            {
                                MapCells[x, y] = new CellInfo();//12
                                MapCells[x, y].BackIndex = 0;
                                MapCells[x, y].MiddleIndex = 1;
                                MapCells[x, y].BackImage = (short)BitConverter.ToInt16(Bytes, offset);
                                offset += 2;
                                MapCells[x, y].MiddleImage = (short)BitConverter.ToInt16(Bytes, offset);
                                offset += 2;
                                MapCells[x, y].FrontImage = (short)BitConverter.ToInt16(Bytes, offset);
                                offset += 2;
                                MapCells[x, y].DoorIndex = (byte)(Bytes[offset++] & 0x7F);
                                MapCells[x, y].DoorOffset = Bytes[offset++];
                                MapCells[x, y].FrontAnimationFrame = Bytes[offset++];
                                MapCells[x, y].FrontAnimationTick = Bytes[offset++];
                                MapCells[x, y].FrontIndex = (ushort)(Bytes[offset++] + 2);
                                MapCells[x, y].Light = Bytes[offset++];
                                if ((MapCells[x, y].BackImage & 0x8000) != 0)
                                    MapCells[x, y].BackImage = (MapCells[x, y].BackImage & 0x7FFF) | 0x20000000;
                            }
                        break;
                }
            }
        }

        private void UpdateMapSeg(int cX, int cY)
        {

        }

        protected void Unmark(int nX, int nY, int cX, int cY)
        {
            if ((cX == nX / LOGICALMAPUNIT) && (cY == nY / LOGICALMAPUNIT))
            {
                var ax = nX - m_nBlockLeft;
                var ay = nY - m_nBlockTop;
                MapCells[ax, ay].FrontIndex = (ushort)(MapCells[ax, ay].FrontIndex & 0x7FFF);
                MapCells[ax, ay].BackIndex = (ushort)(MapCells[ax, ay].BackIndex & 0x7FFF);
            }
        }

        private int _MAX(int n1, int n2)
        {
            return n1 > n2 ? n1 : n2;
        }

        private void LoadMapType2()
        {
            try
            {
                int offset = 0;
                Width = BitConverter.ToInt16(Bytes, offset);
                offset += 2;
                Height = BitConverter.ToInt16(Bytes, offset);
                MapCells = new CellInfo[Width, Height];
                offset = 52;
                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                    {//14
                        MapCells[x, y] = new CellInfo();
                        MapCells[x, y].BackImage = (short)BitConverter.ToInt16(Bytes, offset);
                        offset += 2;
                        MapCells[x, y].MiddleImage = (short)BitConverter.ToInt16(Bytes, offset);
                        offset += 2;
                        MapCells[x, y].FrontImage = (short)BitConverter.ToInt16(Bytes, offset);
                        offset += 2;
                        MapCells[x, y].DoorIndex = (byte)(Bytes[offset++] & 0x7F);
                        MapCells[x, y].DoorOffset = Bytes[offset++];
                        MapCells[x, y].FrontAnimationFrame = Bytes[offset++];
                        MapCells[x, y].FrontAnimationTick = Bytes[offset++];
                        MapCells[x, y].FrontIndex = (ushort)(Bytes[offset++] + 120);
                        MapCells[x, y].Light = Bytes[offset++];
                        MapCells[x, y].BackIndex = (ushort)(Bytes[offset++] + 100);
                        MapCells[x, y].MiddleIndex = (ushort)(Bytes[offset++] + 110);
                        if ((MapCells[x, y].BackImage & 0x8000) != 0)
                            MapCells[x, y].BackImage = (MapCells[x, y].BackImage & 0x7FFF) | 0x20000000;

                    }
            }
            catch (Exception ex)
            {
                
            }

        }

        private void LoadMapType3()
        {
            try
            {
                int offset = 0;
                Width = BitConverter.ToInt16(Bytes, offset);
                offset += 2;
                Height = BitConverter.ToInt16(Bytes, offset);
                MapCells = new CellInfo[Width, Height];
                offset = 52;
                for (int x = 0; x < Width; x++)
                    for (int y = 0; y < Height; y++)
                    {//36
                        MapCells[x, y] = new CellInfo();
                        MapCells[x, y].BackImage = (short)BitConverter.ToInt16(Bytes, offset);
                        offset += 2;
                        MapCells[x, y].MiddleImage = (short)BitConverter.ToInt16(Bytes, offset);
                        offset += 2;
                        MapCells[x, y].FrontImage = (short)BitConverter.ToInt16(Bytes, offset);
                        offset += 2;
                        MapCells[x, y].DoorIndex = (byte)(Bytes[offset++] & 0x7F);
                        MapCells[x, y].DoorOffset = Bytes[offset++];
                        MapCells[x, y].FrontAnimationFrame = Bytes[offset++];
                        MapCells[x, y].FrontAnimationTick = Bytes[offset++];
                        MapCells[x, y].FrontIndex = (ushort)(Bytes[offset++] + 120);
                        MapCells[x, y].Light = Bytes[offset++];
                        MapCells[x, y].BackIndex = (ushort)(Bytes[offset++] + 100);
                        MapCells[x, y].MiddleIndex = (ushort)(Bytes[offset++] + 110);
                        MapCells[x, y].TileAnimationImage = (short)BitConverter.ToInt16(Bytes, offset);
                        offset += 7;//2bytes from tileanimframe, 2 bytes always blank?, 2bytes potentialy 'backtiles index', 1byte fileindex for the backtiles?
                        MapCells[x, y].TileAnimationFrames = Bytes[offset++];
                        MapCells[x, y].TileAnimationOffset = (short)BitConverter.ToInt16(Bytes, offset);
                        offset += 14; //tons of light, blending, .. related options i hope
                        if ((MapCells[x, y].BackImage & 0x8000) != 0)
                            MapCells[x, y].BackImage = (MapCells[x, y].BackImage & 0x7FFF) | 0x20000000;
                    }

            }
            catch (Exception ex)
            {
               
            }
        }
    }

    public class TMapHeader
    {
        public ushort wWidth;
        public ushort wHeight;
        public string sTitle;
        public double UpdateDate;
        public byte[] Reserved;

        public static int PackSize = 52;

        public TMapHeader(byte[] buffer)
        {
            var binaryReader = new BinaryReader(new MemoryStream(buffer));
            wWidth = binaryReader.ReadUInt16();
            wHeight = binaryReader.ReadUInt16();
            sTitle = System.Text.Encoding.Default.GetString(binaryReader.ReadBytes(16));
            UpdateDate = binaryReader.ReadDouble();
            Reserved = binaryReader.ReadBytes(23);
        }
    }
}