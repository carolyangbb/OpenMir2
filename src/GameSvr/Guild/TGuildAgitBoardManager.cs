using System.Collections;
using SystemModule;

namespace GameSvr
{
    public class TGuildAgitBoardManager
    {
        private readonly ArrayList GaBoardList = null;

        public TGuildAgitBoardManager()
        {
            if (svMain.GuildAgitMaxNumber > Guild.MAXGUILDAGITCOUNT)
            {
                svMain.MainOutMessage("[Exception] TGuildAgitBoardManager : GuildAgitMaxNumber > MAXGUILDAGITCOUNT");
                return;
            }
            GaBoardList = new ArrayList();
        }

        public bool LoadOneGaBoard(string uname, int nAgitNum)
        {
            if (nAgitNum < 0)
            {
                return false;
            }
            string gname = svMain.GuildAgitMan.GetGuildNameFromAgitNum(nAgitNum);
            if (gname == "")
            {
                return false;
            }
            SqlEngn.SqlEngine.RequestLoadGuildAgitBoard(uname, gname);
            return true;
        }

        public bool LoadAllGaBoardList(string uname)
        {
            bool result = false;
            GaBoardList.Clear();
            for (var i = svMain.GuildAgitStartNumber; i <= svMain.GuildAgitMaxNumber; i++)
            {
                LoadOneGaBoard(uname, i);
                result = true;
            }
            return result;
        }

        public void AddGaBoardList(TSearchGaBoardList pSearchInfo)
        {
            TSearchGaBoardList pSearchList;
            TGaBoardArticleLoad pArticleLoad;
            ArrayList rList;
            if (pSearchInfo.ArticleList != null)
            {
                pSearchList = new TSearchGaBoardList();
                pSearchList.AgitNum = pSearchInfo.AgitNum;
                pSearchList.GuildName = pSearchInfo.GuildName;
                pSearchList.OrgNum = pSearchInfo.OrgNum;
                pSearchList.SrcNum1 = pSearchInfo.SrcNum1;
                pSearchList.SrcNum2 = pSearchInfo.SrcNum2;
                pSearchList.SrcNum3 = pSearchInfo.SrcNum3;
                pSearchList.Kind = pSearchInfo.Kind;
                rList = new ArrayList();
                for (var i = 0; i < pSearchInfo.ArticleList.Count; i++)
                {
                    pArticleLoad = new TGaBoardArticleLoad();
                    pArticleLoad.AgitNum = ((TGaBoardArticleLoad)pSearchInfo.ArticleList[i]).AgitNum;
                    pArticleLoad.GuildName = ((TGaBoardArticleLoad)pSearchInfo.ArticleList[i]).GuildName;
                    pArticleLoad.OrgNum = ((TGaBoardArticleLoad)pSearchInfo.ArticleList[i]).OrgNum;
                    pArticleLoad.SrcNum1 = ((TGaBoardArticleLoad)pSearchInfo.ArticleList[i]).SrcNum1;
                    pArticleLoad.SrcNum2 = ((TGaBoardArticleLoad)pSearchInfo.ArticleList[i]).SrcNum2;
                    pArticleLoad.SrcNum3 = ((TGaBoardArticleLoad)pSearchInfo.ArticleList[i]).SrcNum3;
                    pArticleLoad.Kind = ((TGaBoardArticleLoad)pSearchInfo.ArticleList[i]).Kind;
                    pArticleLoad.UserName = ((TGaBoardArticleLoad)pSearchInfo.ArticleList[i]).UserName;
                    pArticleLoad.Content = ((TGaBoardArticleLoad)pSearchInfo.ArticleList[i]).Content;
                    rList.Add(pArticleLoad);
                }
                pSearchList.ArticleList = rList;
                GaBoardList.Add(pSearchList);
            }
            else
            {
                // GaBoardList.Add( nil );
            }
        }

        private int GetAllPage(int WholeLine)
        {
            int result;
            if (WholeLine <= Guild.GABOARD_NOTICE_LINE)
            {
                result = 1;
            }
            else
            {
                int iTemp = (WholeLine - Guild.GABOARD_NOTICE_LINE - 1) / (Guild.GABOARD_COUNT_PER_PAGE - Guild.GABOARD_NOTICE_LINE);
                result = iTemp + 1;
            }
            return result;
        }

        private void ConvertNumSeriesToInteger(ref string NumSeries, ref int OrgNum, ref int SrcNum1, ref int SrcNum2, ref int SrcNum3)
        {
            string strOrgNum = string.Empty;
            string strSrcNum1 = string.Empty;
            string strSrcNum2 = string.Empty;
            string strSrcNum3 = string.Empty;
            OrgNum = 0;
            SrcNum1 = 0;
            SrcNum2 = 0;
            SrcNum3 = 0;
            NumSeries = HUtil32.GetValidStr3(NumSeries, ref strOrgNum, new string[] { "/" });
            if (NumSeries == "")
            {
                return;
            }
            NumSeries = HUtil32.GetValidStr3(NumSeries, ref strSrcNum1, new string[] { "/" });
            if (NumSeries == "")
            {
                return;
            }
            NumSeries = HUtil32.GetValidStr3(NumSeries, ref strSrcNum2, new string[] { "/" });
            if (NumSeries == "")
            {
                return;
            }
            NumSeries = HUtil32.GetValidStr3(NumSeries, ref strSrcNum3, new char[] { '/', '\0' });
            OrgNum = HUtil32.Str_ToInt(strOrgNum, 0);
            SrcNum1 = HUtil32.Str_ToInt(strSrcNum1, 0);
            SrcNum2 = HUtil32.Str_ToInt(strSrcNum2, 0);
            SrcNum3 = HUtil32.Str_ToInt(strSrcNum3, 0);
        }

        private int GetNewArticleNumber(string gname, ref int OrgNum, ref int SrcNum1, ref int SrcNum2, ref int SrcNum3)
        {
            int IndentationFlag;
            TSearchGaBoardList pEachSearchBoardList;
            ArrayList pArticleList;
            TGaBoardArticleLoad pArticleLoad;
            int newOrgNum;
            int newSrcNum1;
            int newSrcNum2;
            int newSrcNum3;
            int result = -1;
            int newIndex = Guild.GABOARD_NOTICE_LINE;
            for (var i = 0; i < GaBoardList.Count; i++)
            {
                pEachSearchBoardList = (TSearchGaBoardList)GaBoardList[i];
                if (pEachSearchBoardList.GuildName == gname)
                {
                    pArticleList = pEachSearchBoardList.ArticleList;
                    if (pArticleList == null)
                    {
                        continue;
                    }
                    if ((OrgNum <= 0) && (SrcNum1 <= 0) && (SrcNum2 <= 0) && (SrcNum3 <= 0))
                    {
                        OrgNum = 0;
                        for (var j = 0; j < pArticleList.Count; j++)
                        {
                            pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                            if (pArticleLoad != null)
                            {
                                if (OrgNum < pArticleLoad.OrgNum)
                                {
                                    OrgNum = pArticleLoad.OrgNum;
                                    SrcNum1 = 0;
                                    SrcNum2 = 0;
                                    SrcNum3 = 0;
                                }
                                if (j >= Guild.GABOARD_NOTICE_LINE)
                                {
                                    break;
                                }
                            }
                        }
                        OrgNum = OrgNum + 1;
                        result = newIndex;
                    }
                    else
                    {
                        IndentationFlag = 0;
                        newOrgNum = 0;
                        newSrcNum1 = 0;
                        newSrcNum2 = 0;
                        newSrcNum3 = 0;
                        if (pArticleList.Count <= Guild.GABOARD_NOTICE_LINE)
                        {
                            break;
                        }
                        else
                        {
                            for (var j = Guild.GABOARD_NOTICE_LINE; j < pArticleList.Count; j++)
                            {
                                pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                                if (pArticleLoad != null)
                                {
                                    if ((pArticleLoad.OrgNum == OrgNum) && (pArticleLoad.SrcNum1 == SrcNum1) && (pArticleLoad.SrcNum2 == SrcNum2) && (pArticleLoad.SrcNum3 == SrcNum3))
                                    {
                                        newOrgNum = pArticleLoad.OrgNum;
                                        newSrcNum1 = pArticleLoad.SrcNum1;
                                        newSrcNum2 = pArticleLoad.SrcNum2;
                                        newSrcNum3 = pArticleLoad.SrcNum3;
                                        if (SrcNum1 == 0)
                                        {
                                            IndentationFlag = 1;
                                        }
                                        else if (SrcNum2 == 0)
                                        {
                                            IndentationFlag = 2;
                                        }
                                        else if (SrcNum3 == 0)
                                        {
                                            IndentationFlag = 3;
                                        }
                                        else
                                        {
                                            IndentationFlag = 3;
                                        }
                                        newIndex++;
                                        continue;
                                    }
                                    if (IndentationFlag == 1)
                                    {
                                        if (pArticleLoad.OrgNum == OrgNum)
                                        {
                                            newOrgNum = pArticleLoad.OrgNum;
                                            newSrcNum1 = pArticleLoad.SrcNum1;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else if (IndentationFlag == 2)
                                    {
                                        if ((pArticleLoad.OrgNum == OrgNum) && (pArticleLoad.SrcNum1 == SrcNum1))
                                        {
                                            newOrgNum = pArticleLoad.OrgNum;
                                            newSrcNum1 = pArticleLoad.SrcNum1;
                                            newSrcNum2 = pArticleLoad.SrcNum2;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    else if (IndentationFlag == 3)
                                    {
                                        if ((pArticleLoad.OrgNum == OrgNum) && (pArticleLoad.SrcNum1 == SrcNum1) && (pArticleLoad.SrcNum2 == SrcNum2))
                                        {
                                            newOrgNum = pArticleLoad.OrgNum;
                                            newSrcNum1 = pArticleLoad.SrcNum1;
                                            newSrcNum2 = pArticleLoad.SrcNum2;
                                            newSrcNum3 = pArticleLoad.SrcNum3;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    newIndex++;
                                }
                            }
                            if (IndentationFlag == 1)
                            {
                                OrgNum = newOrgNum;
                                SrcNum1 = newSrcNum1 + 1;
                                SrcNum2 = newSrcNum2;
                                SrcNum3 = newSrcNum3;
                            }
                            else if (IndentationFlag == 2)
                            {
                                OrgNum = newOrgNum;
                                SrcNum1 = newSrcNum1;
                                SrcNum2 = newSrcNum2 + 1;
                                SrcNum3 = newSrcNum3;
                            }
                            else if (IndentationFlag == 3)
                            {
                                OrgNum = newOrgNum;
                                SrcNum1 = newSrcNum1;
                                SrcNum2 = newSrcNum2;
                                SrcNum3 = newSrcNum3 + 1;
                            }
                            else
                            {
                                OrgNum = newOrgNum + 1;
                                SrcNum1 = 0;
                                SrcNum2 = 0;
                                SrcNum3 = 0;
                            }
                            result = newIndex;
                        }
                    }
                    break;
                }
            }
            return result;
        }

        public bool GetPageList(string uname, string gname, ref int nPage, ref int nAllPage, ref ArrayList subjectlist)
        {
            TSearchGaBoardList pEachSearchBoardList;
            ArrayList pArticleList;
            TGaBoardArticleLoad pArticleLoad;
            string subject = string.Empty;
            int StartCount;
            int LastCount;
            bool result = false;
            StartCount = 0;
            if (nPage <= 0)
            {
                nPage = 1;
            }
            for (var i = 0; i < GaBoardList.Count; i++)
            {
                pEachSearchBoardList = (TSearchGaBoardList)GaBoardList[i];
                if (pEachSearchBoardList.GuildName == gname)
                {
                    pEachSearchBoardList.UserName = uname;
                    pArticleList = pEachSearchBoardList.ArticleList;
                    if (pArticleList == null)
                    {
                        continue;
                    }
                    if (pArticleList.Count < Guild.GABOARD_NOTICE_LINE)
                    {
                        return result;
                    }
                    for (var j = 0; j < Guild.GABOARD_NOTICE_LINE; j++)
                    {
                        pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                        if (pArticleLoad != null)
                        {
                            //subject = Copy(pArticleLoad.Content, 0, sizeof(subject));
                            subjectlist.Add(pArticleLoad.UserName + "/" + pArticleLoad.OrgNum.ToString() + "/" + pArticleLoad.SrcNum1.ToString() + "/" + pArticleLoad.SrcNum2.ToString() + "/" + pArticleLoad.SrcNum3.ToString() + "/" + subject);
                        }
                    }
                    nAllPage = GetAllPage(pArticleList.Count);
                    StartCount = HUtil32._MAX(Guild.GABOARD_NOTICE_LINE, ((nPage - 1) * (Guild.GABOARD_COUNT_PER_PAGE - Guild.GABOARD_NOTICE_LINE)) + Guild.GABOARD_NOTICE_LINE);
                    LastCount = HUtil32._MIN(pArticleList.Count, nPage * Guild.GABOARD_COUNT_PER_PAGE);
                    for (var j = StartCount; j < LastCount; j++)
                    {
                        pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                        if (pArticleLoad != null)
                        {
                            //subject = Copy(pArticleLoad.Content, 0, sizeof(subject));
                            subjectlist.Add(pArticleLoad.UserName + "/" + pArticleLoad.OrgNum.ToString() + "/" + pArticleLoad.SrcNum1.ToString() + "/" + pArticleLoad.SrcNum2.ToString() + "/" + pArticleLoad.SrcNum3.ToString() + "/" + subject);
                            result = true;
                        }
                    }
                    break;
                }
            }
            return result;
        }

        public bool AddArticle(string gname, int nKind, int AgitNum, string uname, string data)
        {
            bool result;
            int i;
            int j;
            int newIndex;
            int OrgNum = 0;
            int SrcNum1 = 0;
            int SrcNum2 = 0;
            int SrcNum3 = 0;
            TSearchGaBoardList pEachSearchBoardList;
            ArrayList pArticleList;
            TGaBoardArticleLoad pNewArticleLoad;
            TGaBoardArticleLoad pArticleLoad;
            result = false;
            ConvertNumSeriesToInteger(ref data, ref OrgNum, ref SrcNum1, ref SrcNum2, ref SrcNum3);
            if (data.Trim() == "")
            {
                return result;
            }
            newIndex = GetNewArticleNumber(gname, ref OrgNum, ref SrcNum1, ref SrcNum2, ref SrcNum3);
            if (newIndex > 0)
            {
                //GetMem(pNewArticleLoad, sizeof(TGaBoardArticleLoad));
                pNewArticleLoad = new TGaBoardArticleLoad();
                pNewArticleLoad.AgitNum = AgitNum;
                pNewArticleLoad.GuildName = gname;
                pNewArticleLoad.OrgNum = OrgNum;
                pNewArticleLoad.SrcNum1 = SrcNum1;
                pNewArticleLoad.SrcNum2 = SrcNum2;
                pNewArticleLoad.SrcNum3 = SrcNum3;
                pNewArticleLoad.Kind = nKind;
                pNewArticleLoad.UserName = uname;
                //FillChar(pNewArticleLoad.Content, sizeof(pNewArticleLoad.Content), '\0');
                //StrPLCopy(pNewArticleLoad.Content, data, sizeof(pNewArticleLoad.Content) - 1);
                for (i = 0; i < GaBoardList.Count; i++)
                {
                    pEachSearchBoardList = (TSearchGaBoardList)GaBoardList[i];
                    if (pEachSearchBoardList.GuildName == gname)
                    {
                        pEachSearchBoardList.UserName = uname;
                        pArticleList = pEachSearchBoardList.ArticleList;
                        if (pArticleList != null)
                        {
                            if (nKind == SqlEngn.KIND_NOTICE)
                            {
                                for (j = pArticleList.Count - 1; j >= 0; j--)
                                {
                                    pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                                    if (pArticleLoad != null)
                                    {
                                        if (j == Guild.GABOARD_NOTICE_LINE - 1)
                                        {
                                            SqlEngn.SqlEngine.RequestGuildAgitBoardDelArticle(gname, pArticleLoad.OrgNum, pArticleLoad.SrcNum1, pArticleLoad.SrcNum2, pArticleLoad.SrcNum3, uname);
                                            pArticleList.RemoveAt(j);
                                            break;
                                        }
                                    }
                                }
                                newIndex = 0;
                                pArticleList.Insert(newIndex, pNewArticleLoad);
                            }
                            else if (nKind == SqlEngn.KIND_GENERAL)
                            {
                                if (pArticleList.Count >= Guild.GABOARD_MAX_ARTICLE_COUNT)
                                {
                                    for (j = pArticleList.Count - 1; j >= 0; j--)
                                    {
                                        pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                                        if (pArticleLoad != null)
                                        {
                                            if (j >= Guild.GABOARD_MAX_ARTICLE_COUNT - 1)
                                            {
                                                SqlEngn.SqlEngine.RequestGuildAgitBoardDelArticle(gname, pArticleLoad.OrgNum, pArticleLoad.SrcNum1, pArticleLoad.SrcNum2, pArticleLoad.SrcNum3, uname);
                                                pArticleList.RemoveAt(j);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                pArticleList.Insert(newIndex, pNewArticleLoad);
                            }
                            else
                            {
                                return result;
                            }
                        }
                    }
                }
                result = SqlEngn.SqlEngine.RequestGuildAgitBoardAddArticle(gname, OrgNum, SrcNum1, SrcNum2, SrcNum3, nKind, AgitNum, uname, data);
            }
            return result;
        }

        public string GetArticle(string gname, string NumSeries)
        {
            string result;
            int i;
            int j;
            TSearchGaBoardList pEachSearchBoardList;
            ArrayList pArticleList;
            TGaBoardArticleLoad pArticleLoad;
            int OrgNum = 0;
            int SrcNum1 = 0;
            int SrcNum2 = 0;
            int SrcNum3 = 0;
            result = "";
            ConvertNumSeriesToInteger(ref NumSeries, ref OrgNum, ref SrcNum1, ref SrcNum2, ref SrcNum3);
            if (OrgNum <= 0)
            {
                return result;
            }
            for (i = 0; i < GaBoardList.Count; i++)
            {
                pEachSearchBoardList = (TSearchGaBoardList)GaBoardList[i];
                if (pEachSearchBoardList.GuildName == gname)
                {
                    pArticleList = pEachSearchBoardList.ArticleList;
                    if (pArticleList != null)
                    {
                        for (j = 0; j < pArticleList.Count; j++)
                        {
                            pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                            if (pArticleLoad != null)
                            {
                                if ((pArticleLoad.OrgNum == OrgNum) && (pArticleLoad.SrcNum1 == SrcNum1) && (pArticleLoad.SrcNum2 == SrcNum2) && (pArticleLoad.SrcNum3 == SrcNum3))
                                {
                                    result = OrgNum.ToString() + "/" + SrcNum1.ToString() + "/" + SrcNum2.ToString() + "/" + SrcNum3.ToString() + "/" + pArticleLoad.UserName + "/" + (pArticleLoad.Content as string);
                                    return result;
                                }
                            }
                        }
                        OrgNum = 0;
                        SrcNum1 = 0;
                        SrcNum2 = 0;
                        SrcNum3 = 0;
                        break;
                    }
                }
            }
            return result;
        }

        public bool DelArticle(string gname, string uname, string NumSeries)
        {
            int OrgNum = 0;
            int SrcNum1 = 0;
            int SrcNum2 = 0;
            int SrcNum3 = 0;
            TSearchGaBoardList pEachSearchBoardList;
            ArrayList pArticleList;
            TGaBoardArticleLoad pNewArticleLoad;
            TGaBoardArticleLoad pArticleLoad;
            bool result = false;
            ConvertNumSeriesToInteger(ref NumSeries, ref OrgNum, ref SrcNum1, ref SrcNum2, ref SrcNum3);
            for (var i = 0; i < GaBoardList.Count; i++)
            {
                pEachSearchBoardList = (TSearchGaBoardList)GaBoardList[i];
                if (pEachSearchBoardList.GuildName == gname)
                {
                    pArticleList = pEachSearchBoardList.ArticleList;
                    if (pArticleList != null)
                    {
                        for (var j = pArticleList.Count - 1; j >= 0; j--)
                        {
                            pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                            if (pArticleLoad != null)
                            {
                                if ((pArticleLoad.OrgNum == OrgNum) && (pArticleLoad.SrcNum1 == SrcNum1) && (pArticleLoad.SrcNum2 == SrcNum2) && (pArticleLoad.SrcNum3 == SrcNum3))
                                {
                                    if (pArticleLoad.Kind == SqlEngn.KIND_NOTICE)
                                    {
                                        pArticleList.RemoveAt(j);
                                        //GetMem(pNewArticleLoad, sizeof(TGaBoardArticleLoad));
                                        pNewArticleLoad = new TGaBoardArticleLoad();
                                        pNewArticleLoad.AgitNum = 0;
                                        pNewArticleLoad.GuildName = gname;
                                        pNewArticleLoad.OrgNum = 0;
                                        pNewArticleLoad.SrcNum1 = 0;
                                        pNewArticleLoad.SrcNum2 = 0;
                                        pNewArticleLoad.SrcNum3 = 0;
                                        pNewArticleLoad.Kind = SqlEngn.KIND_NOTICE;
                                        pNewArticleLoad.UserName = "GuildMaster";
                                        pNewArticleLoad.Content = "Guild master\'s message is empty.";
                                        pArticleList.Insert(Guild.GABOARD_NOTICE_LINE - 1, pNewArticleLoad);
                                        break;
                                    }
                                    else
                                    {
                                        pArticleList.RemoveAt(j);
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            result = SqlEngn.SqlEngine.RequestGuildAgitBoardDelArticle(gname, OrgNum, SrcNum1, SrcNum2, SrcNum3, uname);
            return result;
        }

        public bool EditArticle(string gname, string uname, string data)
        {
            bool result;
            int j;
            int OrgNum = 0;
            int SrcNum1 = 0;
            int SrcNum2 = 0;
            int SrcNum3 = 0;
            TSearchGaBoardList pEachSearchBoardList;
            ArrayList pArticleList;
            TGaBoardArticleLoad pArticleLoad;
            result = false;
            ConvertNumSeriesToInteger(ref data, ref OrgNum, ref SrcNum1, ref SrcNum2, ref SrcNum3);
            if (data.Trim() == "")
            {
                return result;
            }
            for (var i = 0; i < GaBoardList.Count; i++)
            {
                pEachSearchBoardList = (TSearchGaBoardList)GaBoardList[i];
                if (pEachSearchBoardList.GuildName == gname)
                {
                    pArticleList = pEachSearchBoardList.ArticleList;
                    if (pArticleList != null)
                    {
                        for (j = pArticleList.Count - 1; j >= 0; j--)
                        {
                            pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                            if (pArticleLoad != null)
                            {
                                if ((pArticleLoad.OrgNum == OrgNum) && (pArticleLoad.SrcNum1 == SrcNum1) && (pArticleLoad.SrcNum2 == SrcNum2) && (pArticleLoad.SrcNum3 == SrcNum3))
                                {
                                    // FillChar(pArticleLoad.Content, sizeof(pArticleLoad.Content), '\0');
                                    //  StrPLCopy(pArticleLoad.Content, data, sizeof(pArticleLoad.Content) - 1);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            result = SqlEngn.SqlEngine.RequestGuildAgitBoardEditArticle(gname, OrgNum, SrcNum1, SrcNum2, SrcNum3, uname, data);
            return result;
        }
    }
}