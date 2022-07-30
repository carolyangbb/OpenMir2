using System.Collections;
using System.IO;
using SystemModule.Common;

namespace GameSvr
{
    public class TNoticeList
    {
        public string Name;
        public StringList List;
        public bool Valid;
    }

    public class TNoticeManager
    {
        public TNoticeList[] Notices;

        public TNoticeManager()
        {
            int i;
            for (i = 0; i < NoticeM.MAXNOTICE; i++)
            {
                Notices[i].Name = "";
                Notices[i].List = null;
                Notices[i].Valid = true;
            }
        }

        ~TNoticeManager()
        {
            int i;
            for (i = 0; i < NoticeM.MAXNOTICE; i++)
            {
                if (Notices[i].List != null)
                {
                    Notices[i].List.Free();
                }
            }
        }

        public void RefreshNoticeList()
        {
            string flname;
            for (var i = 0; i < NoticeM.MAXNOTICE; i++)
            {
                flname = NoticeM.NoticeDir + Notices[i].Name + ".txt";
                if (File.Exists(flname))
                {
                    try
                    {
                        if (Notices[i].List == null)
                        {
                            Notices[i].List = new StringList();
                        }
                        Notices[i].List.LoadFromFile(flname);
                    }
                    catch
                    {
                        svMain.MainOutMessage("Error in loading notice text. file name is " + flname);
                    }
                }
            }
        }

        public bool GetNoticList(string nname, ArrayList slist)
        {
            string flname;
            bool result = false;
            bool noentry = true;
            for (var i = 0; i < NoticeM.MAXNOTICE; i++)
            {
                if (Notices[i].Name.ToLower().CompareTo(nname.ToLower()) == 0)
                {
                    if (Notices[i].List != null)
                    {
                        result = true;
                    }
                    noentry = false;
                    break;
                }
            }
            if (noentry)
            {
                for (var i = 0; i < NoticeM.MAXNOTICE; i++)
                {
                    if (Notices[i].Name == "")
                    {
                        flname = NoticeM.NoticeDir + nname + ".txt";
                        if (File.Exists(flname))
                        {
                            try
                            {
                                if (Notices[i].List == null)
                                {
                                    Notices[i].List = new StringList();
                                }
                                Notices[i].List.LoadFromFile(flname);
                            }
                            catch
                            {
                                svMain.MainOutMessage("Error in loading notice text. file name is " + flname);
                            }
                            Notices[i].Name = nname;
                            result = true;
                        }
                        break;
                    }
                }
            }
            return result;
        }
    }
}

namespace GameSvr
{
    public class NoticeM
    {
        public const int MAXNOTICE = 100;
        public const string NoticeDir = ".\\Notice\\";
    } 
}

