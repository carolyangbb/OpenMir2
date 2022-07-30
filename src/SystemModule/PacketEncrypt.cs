﻿public class PacketEncrypt
{
    private const byte bySeed = 0xAC;
    private const byte byBase = 0x3C;

    /// <summary>
    /// 加密Buffer
    /// </summary>
    /// <returns></returns>
    public static int EncodeBuf(byte[] Buf, int Len, byte[] DstBuf, int dstOffset = 0)
    {
        var no = 2;
        byte remainder = 0;
        var pos = 0;
        var dstPos = dstOffset;
        for (var i = 0; i < Len; i++)
        {
            var c = (byte)(Buf[pos] ^ bySeed);
            pos++;
            if (no == 6)
            {
                DstBuf[dstPos] = (byte)((c & 0x3F) + byBase);
                dstPos++;
                remainder = (byte)(remainder | ((c >> 2) & 0x30));
                DstBuf[dstPos] = (byte)(remainder + byBase);
                dstPos++;
                remainder = 0;
            }
            else
            {
                var temp = (byte)(c >> 2);
                DstBuf[dstPos] = (byte)(((temp & 0x3C) | (c & 0x3)) + byBase);
                dstPos++;
                remainder = (byte)((remainder << 2) | (temp & 0x3));
            }
            no = no % 6 + 2;
        }
        if (no != 2)
        {
            DstBuf[dstPos] = (byte)(remainder + byBase);
            dstPos++;
        }
        var result = dstPos - dstOffset;
        return result;
    }

    public static byte[] DecodeBuf(byte[] Buf, int Len, ref int decodeLen)
    {
        byte temp;
        byte remainder;
        byte c;
        var nCycles = Len / 4;
        var nBytesLeft = Len % 4;
        var dstPos = 0;
        decodeLen = GetDecodeLen(nCycles, nBytesLeft);
        var dstBuffer = new byte[decodeLen];
        for (var i = 0; i < nCycles; i++)
        {
            var curCycleBegin = i * 4;
            remainder = (byte)((Buf[curCycleBegin + 3]) - byBase);
            temp = (byte)(Buf[curCycleBegin] - byBase);
            c = (byte)(((temp << 2) & 0xF0) | (remainder & 0x0C) | (temp & 0x3));
            dstBuffer[dstPos] = (byte)(c ^ bySeed);
            dstPos++;
            temp = (byte)((Buf[curCycleBegin + 1]) - byBase);
            c = (byte)(((temp << 2) & 0xF0) | ((remainder << 2) & 0x0C) | (temp & 0x3));
            dstBuffer[dstPos] = (byte)(c ^ bySeed);
            dstPos++;
            temp = (byte)(Buf[curCycleBegin + 2] - byBase);
            c = (byte)(temp | ((remainder << 2) & 0xC0));
            dstBuffer[dstPos] = (byte)(c ^ bySeed);
            dstPos++;
        }
        if (nBytesLeft == 2)
        {
            remainder = (byte)(Buf[Len - 1] - byBase);
            temp = (byte)(Buf[Len - 2] - byBase);
            c = (byte)(((temp << 2) & 0xF0) | ((remainder << 2) & 0x0C) | (temp & 0x3));
            dstBuffer[dstPos] = (byte)(c ^ bySeed);
        }
        else if (nBytesLeft == 3)
        {
            remainder = (byte)(Buf[Len - 1] - byBase);
            temp = (byte)(Buf[Len - 3] - byBase);
            c = (byte)(((temp << 2) & 0xF0) | (remainder & 0x0C) | (temp & 0x3));
            dstBuffer[dstPos] = (byte)(c ^ bySeed);
            dstPos++;
            temp = (byte)(Buf[Len - 2] - byBase);
            c = (byte)(((temp << 2) & 0xF0) | ((remainder << 2) & 0x0C) | (temp & 0x3));
            dstBuffer[dstPos] = (byte)(c ^ bySeed);
        }
        return dstBuffer;
    }

    private static int GetDecodeLen(int cycles, int bytesLeft)
    {
        var dstPos = cycles * 3;
        switch (bytesLeft)
        {
            case 2:
                dstPos++;
                break;
            case 3:
                dstPos += 2;
                break;
        }
        return dstPos;
    }

}
