namespace SystemModule
{
    public class EDcode
    {
        public static object CSEncode = null;
        public static int BUFFERSIZE = 10000;
        public static byte[] EncBuf;
        public static byte[] TempBuf;
        public static byte[] cTable_src = { 27, 117, 25, 77, 99, 60, 239, 171, 15, 33, 87, 160, 1, 18, 114, 26, 218, 78, 243, 108, 254, 237, 42, 3, 236, 245, 178, 48, 110, 124, 23, 196, 88, 104, 90, 11, 111, 235, 68, 241, 118, 6, 129, 125, 220, 41, 250, 140, 244, 100, 16, 149, 226, 189, 112, 133, 223, 240, 229, 58, 165, 72, 91, 93, 9, 197, 34, 173, 174, 230, 208, 253, 8, 55, 2, 92, 143, 234, 84, 154, 204, 63, 32, 61, 64, 119, 180, 153, 138, 103, 201, 205, 59, 17, 221, 10, 36, 113, 225, 82, 161, 70, 216, 146, 139, 232, 107, 66, 109, 252, 202, 145, 192, 81, 134, 96, 251, 181, 184, 69, 182, 214, 49, 198, 156, 147, 86, 219, 246, 5, 31, 167, 71, 248, 128, 121, 102, 162, 168, 227, 50, 14, 0, 238, 56, 79, 4, 35, 212, 44, 222, 137, 175, 169, 98, 255, 209, 185, 155, 40, 135, 62, 127, 150, 152, 74, 97, 38, 159, 157, 120, 75, 210, 106, 200, 53, 148, 80, 179, 94, 22, 122, 228, 233, 39, 151, 47, 206, 101, 191, 231, 57, 126, 142, 76, 37, 187, 224, 29, 51, 215, 242, 141, 136, 183, 46, 144, 105, 123, 131, 130, 172, 54, 89, 21, 247, 7, 166, 28, 177, 163, 13, 43, 186, 164, 188, 211, 30, 24, 83, 115, 176, 19, 52, 170, 65, 207, 193, 67, 20, 217, 45, 213, 199, 95, 85, 194, 73, 116, 158, 203, 132, 249, 190, 195, 12 };
        public static byte[] dTable_src = { 160, 89, 12, 213, 208, 108, 7, 90, 215, 174, 197, 78, 99, 172, 196, 8, 21, 112, 14, 36, 167, 137, 183, 70, 60, 15, 152, 80, 226, 51, 50, 38, 114, 126, 113, 255, 166, 207, 82, 131, 186, 42, 85, 243, 194, 95, 84, 62, 242, 169, 161, 132, 93, 138, 248, 245, 68, 66, 103, 58, 203, 25, 92, 26, 46, 33, 74, 218, 43, 91, 10, 117, 238, 83, 87, 170, 0, 17, 105, 101, 88, 19, 27, 235, 252, 232, 6, 102, 236, 139, 81, 146, 182, 178, 240, 47, 234, 49, 190, 115, 48, 241, 173, 247, 116, 244, 171, 30, 251, 205, 41, 73, 2, 100, 221, 162, 45, 163, 214, 164, 20, 67, 53, 18, 157, 96, 69, 193, 44, 155, 129, 120, 34, 199, 176, 13, 59, 185, 198, 76, 29, 122, 144, 239, 209, 123, 28, 118, 211, 56, 127, 230, 195, 210, 217, 16, 64, 55, 75, 220, 148, 86, 77, 225, 219, 39, 121, 227, 177, 71, 61, 202, 191, 31, 141, 57, 140, 206, 135, 147, 254, 4, 109, 179, 54, 107, 233, 212, 237, 63, 250, 201, 187, 165, 133, 1, 192, 253, 22, 125, 23, 228, 204, 184, 154, 222, 110, 40, 246, 94, 104, 200, 136, 134, 37, 79, 3, 5, 98, 65, 72, 52, 130, 24, 249, 9, 124, 145, 153, 159, 188, 156, 32, 111, 231, 97, 175, 119, 181, 142, 151, 224, 106, 223, 158, 11, 180, 229, 143, 35, 149, 168, 189, 216, 150, 128 };
        public static byte[] cTable_return = { 141, 151, 7, 132, 73, 83, 91, 250, 156, 205, 157, 72, 170, 123, 99, 144, 148, 70, 3, 207, 229, 230, 30, 178, 120, 245, 254, 27, 92, 108, 163, 255, 248, 76, 65, 98, 134, 46, 239, 211, 137, 201, 1, 213, 240, 235, 169, 66, 183, 237, 8, 212, 232, 189, 80, 63, 172, 112, 119, 231, 51, 222, 173, 190, 252, 77, 198, 160, 214, 152, 168, 114, 12, 162, 106, 24, 75, 36, 196, 82, 199, 79, 117, 43, 202, 194, 217, 68, 216, 143, 111, 100, 206, 90, 110, 154, 227, 180, 45, 4, 38, 253, 19, 13, 175, 32, 115, 89, 64, 26, 200, 177, 138, 113, 14, 130, 191, 0, 86, 241, 50, 187, 33, 44, 17, 121, 131, 93, 42, 128, 226, 219, 220, 57, 105, 6, 182, 215, 242, 238, 52, 59, 54, 208, 236, 29, 181, 166, 35, 126, 56, 186, 60, 185, 233, 203, 23, 107, 116, 124, 125, 224, 155, 221, 243, 184, 78, 47, 101, 149, 103, 49, 104, 15, 139, 164, 140, 147, 247, 84, 167, 118, 61, 102, 142, 249, 197, 161, 234, 34, 25, 171, 62, 74, 193, 122, 209, 87, 69, 133, 192, 135, 11, 127, 10, 22, 225, 20, 176, 81, 67, 18, 218, 95, 223, 96, 165, 37, 55, 246, 159, 145, 195, 9, 97, 28, 53, 204, 210, 58, 85, 2, 150, 244, 31, 41, 228, 129, 179, 158, 109, 188, 136, 5, 48, 174, 146, 71, 16, 40, 88, 39, 94, 153, 251, 21 };
        public static byte[] dTable_return = { 196, 202, 206, 148, 135, 111, 215, 186, 209, 192, 72, 211, 181, 248, 208, 46, 134, 225, 246, 7, 19, 234, 182, 203, 204, 219, 154, 132, 109, 1, 33, 224, 34, 104, 218, 194, 131, 105, 156, 178, 152, 86, 200, 210, 244, 28, 189, 255, 32, 16, 245, 67, 65, 71, 6, 52, 216, 61, 82, 57, 94, 127, 5, 207, 60, 66, 55, 143, 160, 173, 164, 48, 199, 249, 70, 171, 144, 119, 174, 201, 177, 190, 62, 58, 172, 24, 75, 240, 17, 133, 89, 254, 8, 159, 139, 231, 83, 228, 3, 93, 31, 4, 81, 106, 23, 128, 241, 96, 88, 138, 212, 2, 222, 163, 195, 253, 147, 40, 151, 197, 187, 124, 136, 108, 103, 84, 142, 97, 252, 242, 36, 76, 101, 79, 121, 175, 153, 95, 184, 166, 102, 11, 9, 235, 50, 98, 237, 129, 236, 68, 247, 120, 45, 38, 179, 54, 193, 114, 213, 59, 169, 140, 78, 233, 41, 42, 85, 80, 137, 27, 158, 44, 188, 15, 155, 161, 145, 43, 226, 39, 205, 176, 117, 220, 107, 116, 64, 180, 112, 29, 90, 251, 126, 149, 30, 26, 125, 18, 110, 47, 25, 229, 0, 10, 113, 51, 53, 100, 157, 35, 74, 12, 77, 13, 239, 168, 227, 63, 150, 191, 170, 118, 91, 232, 217, 167, 115, 92, 87, 20, 22, 37, 162, 21, 243, 214, 165, 185, 146, 183, 14, 99, 123, 69, 56, 238, 223, 73, 221, 250, 130, 49, 141, 198, 122, 230 };
        public static byte cXorValue = 0x4D;
        public static ushort g_EndeKey = 0xA52E;
        public static byte g_HideTable = 0x97;
        public static byte g_HideBackTable = 0x34;
        public const int ENDECODEMODE = 1;

        public static void Encode6BitBuf(string src, byte[] dest, int srclen, int destlen)
        {
            int restcount = 0;
            byte rest = 0;
            int destpos = 0;
            for (var i = 0; i < srclen; i++)
            {
                if (destpos >= destlen)
                {
                    break;
                }
                byte ch1 = ((byte)src[i]);
                byte made = ((byte)((byte)(rest | (ch1 >> (2 + restcount))) & 0x3F));
                rest = ((byte)((byte)((ch1 << (8 - (2 + restcount))) >> 2) & 0x3F));
                restcount += 2;
                if (restcount < 6)
                {
                    dest[destpos] = (byte)(made + 0x3C);
                    destpos++;
                }
                else
                {
                    if (destpos < destlen - 1)
                    {
                        dest[destpos] = (byte)(made + 0x3C);
                        dest[destpos + 1] = (byte)(rest + 0x3C);
                        destpos += 2;
                    }
                    else
                    {
                        dest[destpos] = (byte)(made + 0x3C);
                        destpos++;
                    }
                    restcount = 0;
                    rest = 0;
                }
            }
            if (restcount > 0)
            {
                dest[destpos] = (byte)(rest + 0x3C);
                destpos++;
            }
            dest[destpos] = (byte)'\0';
        }

        public static void Decode6BitBuf(string Source, byte[] buf, int buflen)
        {
            byte[] Masks = { 0xFC, 0xF8, 0xF0, 0xE0, 0xC0 };
            int len = Source.Length;
            int bitpos = 2;
            int madebit = 0;
            int bufpos = 0;
            byte tmp = 0;
            byte ch1 = 0;
            for (var i = 0; i < len; i++)
            {
                if ((Source[i] - 0x3C >= 0) && (Source[i] - 0x3C <= 64))
                {
                    ch1 = (byte)(((byte)Source[i]) - 0x3C);
                }
                else
                {
                    bufpos = 0;
                    break;
                }
                if (bufpos >= buflen)
                {
                    break;
                }
                if ((madebit + 6) >= 8)
                {
                    byte _byte = ((byte)(tmp | ((ch1 & 0x3F) >> (6 - bitpos))));
                    buf[bufpos] = _byte;
                    bufpos++;
                    madebit = 0;
                    if (bitpos < 6)
                    {
                        bitpos += 2;
                    }
                    else
                    {
                        bitpos = 2;
                        continue;
                    }
                }
                tmp = (byte)(((byte)(ch1 << bitpos)) & Masks[bitpos]);
                madebit += 8 - bitpos;
            }
            buf[bufpos] = (byte)'\0';
        }

        public static TDefaultMessage DecodeMessage(string str)
        {
            TDefaultMessage result = null;
            TDefaultMessage msg;
            try
            {
                //EnterCriticalSection(CSEncode);
                Decode6BitBuf(str, EncBuf, 1024);
                //Move(EncBuf, msg, sizeof(TDefaultMessage));
                //result = msg;
            }
            finally
            {
                //LeaveCriticalSection(CSEncode);
            }
            return result;
        }

        public static string DecodeString(string str)
        {
            string result;
            try
            {
                //  EnterCriticalSection(CSEncode);
                Decode6BitBuf(str, EncBuf, BUFFERSIZE);
                result = System.Text.Encoding.Default.GetString(EncBuf);
            }
            finally
            {
                //LeaveCriticalSection(CSEncode);
            }
            return result;
        }

        public static void DecodeBuffer(string src, string buf, int bufsize)
        {
            try
            {
                //EnterCriticalSection(CSEncode);
                Decode6BitBuf(src, EncBuf, BUFFERSIZE);
                //Move(EncBuf, buf, bufsize);
            }
            finally
            {
                // LeaveCriticalSection(CSEncode);
            }
        }

        public static void DecodeBuffer<T>(string src, ref T buf)
        {
            try
            {
                //EnterCriticalSection(CSEncode);
                Decode6BitBuf(src, EncBuf, BUFFERSIZE);
                //Move(EncBuf, buf, bufsize);
            }
            finally
            {
                // LeaveCriticalSection(CSEncode);
            }
        }

        public static string EncodeMessage(TDefaultMessage smsg)
        {
            string result;
            try
            {
                //EnterCriticalSection(CSEncode);
                // Move(smsg, TempBuf, sizeof(TDefaultMessage));
                //Encode6BitBuf(TempBuf, EncBuf, sizeof(TDefaultMessage), 1024);
                result = System.Text.Encoding.Default.GetString(EncBuf); ;
            }
            finally
            {
                // LeaveCriticalSection(CSEncode);
            }
            return result;
        }

        public static string EncodeString(string str)
        {
            string result;
            try
            {
                // EnterCriticalSection(CSEncode);
                Encode6BitBuf(str, EncBuf, str.Length, BUFFERSIZE);
                result = System.Text.Encoding.Default.GetString(EncBuf);
            }
            finally
            {
                //  LeaveCriticalSection(CSEncode);
            }
            return result;
        }

        public static string EncodeBuffer<T>(T buf, int bufsize = 0) where T : ClientPacket
        {
            string result;
            try
            {
                //EnterCriticalSection(CSEncode);
                if (bufsize < BUFFERSIZE)
                {
                    // Move(buf, TempBuf, bufsize);
                    //Encode6BitBuf(TempBuf, EncBuf, bufsize, BUFFERSIZE);
                    result = System.Text.Encoding.Default.GetString(EncBuf);
                }
                else
                {
                    result = "";
                }
            }
            finally
            {
                // LeaveCriticalSection(CSEncode);
            }
            return result;
        }

        public static string EncodeBuffer(string buf, int bufsize = 0)
        {
            string result;
            try
            {
                //EnterCriticalSection(CSEncode);
                if (bufsize < BUFFERSIZE)
                {
                    // Move(buf, TempBuf, bufsize);
                    //Encode6BitBuf(TempBuf, EncBuf, bufsize, BUFFERSIZE);
                    result = System.Text.Encoding.Default.GetString(EncBuf);
                }
                else
                {
                    result = "";
                }
            }
            finally
            {
                // LeaveCriticalSection(CSEncode);
            }
            return result;
        }

        public void initialization()
        {
            //GetMem(EncBuf, BUFFERSIZE + 100);
            //GetMem(TempBuf, BUFFERSIZE + 100);
            //InitializeCriticalSection(CSEncode);
        }

        public void finalization()
        {
            //FreeMem(EncBuf, BUFFERSIZE + 100);
            //FreeMem(TempBuf, BUFFERSIZE + 100);
            //DeleteCriticalSection(CSEncode);
        }
    }
}