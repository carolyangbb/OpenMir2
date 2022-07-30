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
            bool result;
            string gname;
            result = false;
            if (nAgitNum < 0)
            {
                return result;
            }
            gname = svMain.GuildAgitMan.GetGuildNameFromAgitNum(nAgitNum);
            if (gname == "")
            {
                return result;
            }
            SqlEngn.SqlEngine.RequestLoadGuildAgitBoard(uname, gname);
            result = true;
            return result;
        }

        public bool LoadAllGaBoardList(string uname)
        {
            bool result;
            int i;
            result = false;
            // 刚历 府胶飘甫 瘤款促.
            GaBoardList.Clear();
            // DB俊辑 阿 厘盔喊 霸矫拱阑 葛滴 肺爹茄促.
            for (i = svMain.GuildAgitStartNumber; i <= svMain.GuildAgitMaxNumber; i++)
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
            int i;
            // if pInfo.IsOK <> UMRESULT_SUCCESS then Exit;
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
                for (i = 0; i < pSearchInfo.ArticleList.Count; i++)
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
                // MainOutMessage( PTGaBoardArticleLoad(PTSearchGaBoardList(GaBoardList.Items[0]).ArticleList.Items[0]).Content );
                // MainOutMessage('GaBoardList.Count : ' + IntToStr(GaBoardList.Count));
                // MainOutMessage('PTSearchGaBoardList.Count : ' + IntToStr(pSearchList.ArticleList.Count));
            }
            else
            {
                // GaBoardList.Add( nil );
            }
        }

        // List of List
        private int GetAllPage(int WholeLine)
        {
            int result;
            int iTemp;
            if (WholeLine <= Guild.GABOARD_NOTICE_LINE)
            {
                result = 1;
            }
            else
            {
                // (WholeLine - 4)甫 7肺 唱传 蛤俊...
                iTemp = (WholeLine - Guild.GABOARD_NOTICE_LINE - 1) / (Guild.GABOARD_COUNT_PER_PAGE - Guild.GABOARD_NOTICE_LINE);
                // 1阑 歹茄 蔼捞 其捞瘤啊 凳.
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
            NumSeries = HUtil32.GetValidStr3(NumSeries, ref strSrcNum3, new char[] { "/", '\0' });
            OrgNum = HUtil32.Str_ToInt(strOrgNum, 0);
            SrcNum1 = HUtil32.Str_ToInt(strSrcNum1, 0);
            SrcNum2 = HUtil32.Str_ToInt(strSrcNum2, 0);
            SrcNum3 = HUtil32.Str_ToInt(strSrcNum3, 0);
        }

        // 货 臂 锅龋甫 何咯茄促.
        private int GetNewArticleNumber(string gname, ref int OrgNum, ref int SrcNum1, ref int SrcNum2, ref int SrcNum3)
        {
            int result;
            int i;
            int j;
            int newIndex;
            int IndentationFlag;
            TSearchGaBoardList pEachSearchBoardList;
            ArrayList pArticleList;
            TGaBoardArticleLoad pArticleLoad;
            int newOrgNum;
            int newSrcNum1;
            int newSrcNum2;
            int newSrcNum3;
            result = -1;
            newIndex = Guild.GABOARD_NOTICE_LINE;
            // 厘盔喊 霸矫魄 八祸.
            for (i = 0; i < GaBoardList.Count; i++)
            {
                pEachSearchBoardList = (TSearchGaBoardList)GaBoardList[i];
                // 秦寸 巩颇狼 霸矫魄 府胶飘.
                if (pEachSearchBoardList.GuildName == gname)
                {
                    pArticleList = pEachSearchBoardList.ArticleList;
                    if (pArticleList == null)
                    {
                        continue;
                    }
                    if ((OrgNum <= 0) && (SrcNum1 <= 0) && (SrcNum2 <= 0) && (SrcNum3 <= 0))
                    {
                        // 盔夯臂 静扁捞搁...
                        // 弥辟 傍瘤荤亲 3俺狼 锅龋客 老馆臂狼 霉锅掳 臂 锅龋 吝俊辑
                        // 啊厘 奴 锅龋+1肺 沥茄促.
                        OrgNum = 0;
                        // 0栏肺 檬扁拳.
                        for (j = 0; j < pArticleList.Count; j++)
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
                        // 窍唱 刘啊.
                        result = newIndex;
                    }
                    else
                    {
                        // 翠臂 静扁捞搁...
                        // 檬扁拳.
                        IndentationFlag = 0;
                        newOrgNum = 0;
                        newSrcNum1 = 0;
                        newSrcNum2 = 0;
                        newSrcNum3 = 0;
                        // 傍瘤荤亲 力寇.
                        if (pArticleList.Count <= Guild.GABOARD_NOTICE_LINE)
                        {
                            // 老馆臂篮 绝绊 傍瘤荤亲 观俊 绝栏搁...
                            break;
                        }
                        else
                        {
                            // ///////////////////////////////////////////////////////////
                            // 秦寸 盔夯臂阑 刚历 茫绰促.
                            // 矫累且 甸咯静扁 荐霖(IndentationFlag)阑 汲沥窍绊
                            // 弊 捞饶肺绰 甸咯静扁 荐霖 捞傈狼 臂锅龋啊 崔扼龙 锭鳖瘤 八祸 棺 蔼阑 措涝茄促.
                            // 甸咯静扁 荐霖 捞傈狼 臂锅龋啊 崔扼瘤搁
                            // 盖 付瘤阜俊 措涝茄 臂锅龋俊辑 甸咯静扁 荐霖俊 秦寸窍绰 臂锅龋俊 +1阑 茄促.
                            // ///////////////////////////////////////////////////////////
                            for (j = Guild.GABOARD_NOTICE_LINE; j < pArticleList.Count; j++)
                            {
                                pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                                if (pArticleLoad != null)
                                {
                                    // 秦寸 盔夯臂阑 茫酒辑 弊 关俊 崔妨乐绰 [付瘤阜 翠臂狼 锅龋+1]阑 何咯茄促.
                                    if ((pArticleLoad.OrgNum == OrgNum) && (pArticleLoad.SrcNum1 == SrcNum1) && (pArticleLoad.SrcNum2 == SrcNum2) && (pArticleLoad.SrcNum3 == SrcNum3))
                                    {
                                        newOrgNum = pArticleLoad.OrgNum;
                                        newSrcNum1 = pArticleLoad.SrcNum1;
                                        newSrcNum2 = pArticleLoad.SrcNum2;
                                        newSrcNum3 = pArticleLoad.SrcNum3;
                                        // 翠臂 表捞 敲贰弊 蔼 汲沥.
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
                            // for
                            // 唱柯 搬苞肺 货 翠臂 锅龋 搬沥.
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
                                // 秦寸 盔夯臂捞 绝栏搁...(???)
                                OrgNum = newOrgNum + 1;
                                SrcNum1 = 0;
                                SrcNum2 = 0;
                                SrcNum3 = 0;
                            }
                            result = newIndex;
                        }
                        // if
                    }
                    // if
                    break;
                }
                // if
            }
            // for

            return result;
        }

        public bool GetPageList(string uname, string gname, ref int nPage, ref int nAllPage, ref ArrayList subjectlist)
        {
            bool result;
            int i;
            int j;
            TSearchGaBoardList pEachSearchBoardList;
            ArrayList pArticleList;
            TGaBoardArticleLoad pArticleLoad;
            string[] subject;
            int StartCount;
            int LastCount;
            result = false;
            StartCount = 0;
            if (nPage <= 0)
            {
                nPage = 1;
            }
            // 秦寸 巩颇狼 霸矫魄阑 茄 其捞瘤 佬绢咳.
            for (i = 0; i < GaBoardList.Count; i++)
            {
                pEachSearchBoardList = (TSearchGaBoardList)GaBoardList[i];
                // 秦寸 巩颇狼 霸矫魄 府胶飘.
                if (pEachSearchBoardList.GuildName == gname)
                {
                    pEachSearchBoardList.UserName = uname;
                    pArticleList = pEachSearchBoardList.ArticleList;
                    if (pArticleList == null)
                    {
                        continue;
                    }
                    // 俺荐啊 何练窃(弥家 3俺绰 登绢具 窃).
                    if (pArticleList.Count < Guild.GABOARD_NOTICE_LINE)
                    {
                        return result;
                    }
                    // 傍瘤荤亲篮 公炼扒 拉临俊 眠啊.
                    for (j = 0; j < Guild.GABOARD_NOTICE_LINE; j++)
                    {
                        pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                        if (pArticleLoad != null)
                        {
                            subject = Copy(pArticleLoad.Content, 0, sizeof(subject));
                            subjectlist.Add(pArticleLoad.UserName + "/" + pArticleLoad.OrgNum.ToString() + "/" + pArticleLoad.SrcNum1.ToString() + "/" + pArticleLoad.SrcNum2.ToString() + "/" + pArticleLoad.SrcNum3.ToString() + "/" + subject);
                        }
                    }
                    nAllPage = GetAllPage(pArticleList.Count);
                    StartCount = _MAX(Guild.GABOARD_NOTICE_LINE, ((nPage - 1) * (Guild.GABOARD_COUNT_PER_PAGE - Guild.GABOARD_NOTICE_LINE)) + Guild.GABOARD_NOTICE_LINE);
                    LastCount = _MIN(pArticleList.Count, nPage * Guild.GABOARD_COUNT_PER_PAGE);
                    for (j = StartCount; j < LastCount; j++)
                    {
                        pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                        if (pArticleLoad != null)
                        {
                            subject = Copy(pArticleLoad.Content, 0, sizeof(subject));
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
            // 郴侩捞 绝栏搁 臂静扁甫 且 荐 绝澜.
            if (data.Trim() == "")
            {
                return result;
            }
            // 货臂锅龋甫 积己茄促. 府畔蔼篮 牢郸胶.
            newIndex = GetNewArticleNumber(gname, ref OrgNum, ref SrcNum1, ref SrcNum2, ref SrcNum3);
            if (newIndex > 0)
            {
                // 府胶飘俊 货臂阑 眠啊茄促.
                GetMem(pNewArticleLoad, sizeof(TGaBoardArticleLoad));
                // 佬绰 辆幅 积己
                pNewArticleLoad.AgitNum = AgitNum;
                pNewArticleLoad.GuildName = gname;
                pNewArticleLoad.OrgNum = OrgNum;
                pNewArticleLoad.SrcNum1 = SrcNum1;
                pNewArticleLoad.SrcNum2 = SrcNum2;
                pNewArticleLoad.SrcNum3 = SrcNum3;
                pNewArticleLoad.Kind = nKind;
                pNewArticleLoad.UserName = uname;
                FillChar(pNewArticleLoad.Content, sizeof(pNewArticleLoad.Content), '\0');
                StrPLCopy(pNewArticleLoad.Content, data, sizeof(pNewArticleLoad.Content) - 1);
                for (i = 0; i < GaBoardList.Count; i++)
                {
                    pEachSearchBoardList = (TSearchGaBoardList)GaBoardList[i];
                    // 秦寸 巩颇狼 霸矫魄 府胶飘.
                    if (pEachSearchBoardList.GuildName == gname)
                    {
                        pEachSearchBoardList.UserName = uname;
                        pArticleList = pEachSearchBoardList.ArticleList;
                        if (pArticleList != null)
                        {
                            // 傍瘤荤亲 静扁...
                            if (nKind == SqlEngn.KIND_NOTICE)
                            {
                                // 付瘤阜 傍瘤甫 瘤快绊 霉锅掳 傍瘤俊 眠啊茄促.
                                for (j = pArticleList.Count - 1; j >= 0; j--)
                                {
                                    pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                                    if (pArticleLoad != null)
                                    {
                                        // 傍瘤 付瘤阜 臂阑 昏力茄促.
                                        if (j == Guild.GABOARD_NOTICE_LINE - 1)
                                        {
                                            // DB俊 昏力 夸没.
                                            SqlEngn.SqlEngine.RequestGuildAgitBoardDelArticle(gname, pArticleLoad.OrgNum, pArticleLoad.SrcNum1, pArticleLoad.SrcNum2, pArticleLoad.SrcNum3, uname);
                                            // 府胶飘俊辑 昏力.
                                            pArticleList.RemoveAt(j);
                                            break;
                                        }
                                    }
                                }
                                newIndex = 0;
                                // 臂阑 火涝茄促.
                                pArticleList.Insert(newIndex, pNewArticleLoad);
                            }
                            else if (nKind == SqlEngn.KIND_GENERAL)
                            {
                                // 霸矫拱 荐啊 弥措 荐甫 逞栏搁 啊厘 坷贰等 臂 昏力 饶 眠啊.
                                if (pArticleList.Count >= Guild.GABOARD_MAX_ARTICLE_COUNT)
                                {
                                    for (j = pArticleList.Count - 1; j >= 0; j--)
                                    {
                                        pArticleLoad = (TGaBoardArticleLoad)pArticleList[j];
                                        if (pArticleLoad != null)
                                        {
                                            // 付瘤阜 臂阑 昏力茄促.
                                            if (j >= Guild.GABOARD_MAX_ARTICLE_COUNT - 1)
                                            {
                                                // DB俊 昏力 夸没.
                                                SqlEngn.SqlEngine.RequestGuildAgitBoardDelArticle(gname, pArticleLoad.OrgNum, pArticleLoad.SrcNum1, pArticleLoad.SrcNum2, pArticleLoad.SrcNum3, uname);
                                                // 府胶飘俊辑 昏力.(逞绢啊绰 臂 葛滴 昏力)
                                                pArticleList.RemoveAt(j);
                                            }
                                            else
                                            {
                                                break;
                                            }
                                        }
                                    }
                                }
                                // 老馆臂 眠啊...
                                // 臂阑 火涝茄促.
                                pArticleList.Insert(newIndex, pNewArticleLoad);
                            }
                            else
                            {
                                return result;
                            }
                        }
                    }
                }
                // DB俊 静扁 夸没茄促.
                result = SqlEngn.SqlEngine.RequestGuildAgitBoardAddArticle(gname, OrgNum, SrcNum1, SrcNum2, SrcNum3, nKind, AgitNum, uname, data);
            }
            return result;
        }

        // 臂锅龋俊 秦寸窍绰 臂狼 郴侩阑 佬绢柯促.
        public string GetArticle(string gname, string NumSeries)
        {
            string result;
            int i;
            int j;
            TSearchGaBoardList pEachSearchBoardList;
            ArrayList pArticleList;
            TGaBoardArticleLoad pArticleLoad;
            int OrgNum;
            int SrcNum1;
            int SrcNum2;
            int SrcNum3;
            result = "";
            ConvertNumSeriesToInteger(ref NumSeries, ref OrgNum, ref SrcNum1, ref SrcNum2, ref SrcNum3);
            if (OrgNum <= 0)
            {
                return result;
            }
            for (i = 0; i < GaBoardList.Count; i++)
            {
                pEachSearchBoardList = (TSearchGaBoardList)GaBoardList[i];
                // 秦寸 巩颇狼 霸矫魄 府胶飘.
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
                                // 秦寸 巩颇 霸矫魄俊 臂锅龋啊 鞍篮 臂捞 乐栏搁...
                                if ((pArticleLoad.OrgNum == OrgNum) && (pArticleLoad.SrcNum1 == SrcNum1) && (pArticleLoad.SrcNum2 == SrcNum2) && (pArticleLoad.SrcNum3 == SrcNum3))
                                {
                                    // 臂锅龋/翠臂锅龋1/翠臂锅龋2/翠臂锅龋3/蜡历捞抚/郴侩
                                    result = OrgNum.ToString() + "/" + SrcNum1.ToString() + "/" + SrcNum2.ToString() + "/" + SrcNum3.ToString() + "/" + pArticleLoad.UserName + "/" + (pArticleLoad.Content as string);
                                    return result;
                                    // 狐廉唱皑.
                                }
                            }
                        }
                        // 搬苞啊 绝栏搁 0栏肺 函版.
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
            bool result;
            int i;
            int j;
            int OrgNum = string.Empty;
            int SrcNum1 = string.Empty;
            int SrcNum2 = string.Empty;
            int SrcNum3 = string.Empty;
            TSearchGaBoardList pEachSearchBoardList;
            ArrayList pArticleList;
            TGaBoardArticleLoad pNewArticleLoad;
            TGaBoardArticleLoad pArticleLoad;
            result = false;
            ConvertNumSeriesToInteger(ref NumSeries, ref OrgNum, ref SrcNum1, ref SrcNum2, ref SrcNum3);
            for (i = 0; i < GaBoardList.Count; i++)
            {
                pEachSearchBoardList = (TSearchGaBoardList)GaBoardList[i];
                // 秦寸 巩颇狼 霸矫魄 府胶飘.
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
                                // 秦寸 巩颇 霸矫魄俊 臂锅龋啊 鞍篮 臂捞 乐栏搁 昏力茄促.
                                if ((pArticleLoad.OrgNum == OrgNum) && (pArticleLoad.SrcNum1 == SrcNum1) && (pArticleLoad.SrcNum2 == SrcNum2) && (pArticleLoad.SrcNum3 == SrcNum3))
                                {
                                    if (pArticleLoad.Kind == SqlEngn.KIND_NOTICE)
                                    {
                                        // 傍瘤 昏力绰 昏力饶俊 付瘤阜 臂阑 焊面茄促.
                                        // 昏力...
                                        pArticleList.RemoveAt(j);
                                        // 府胶飘俊 货臂阑 眠啊茄促.
                                        GetMem(pNewArticleLoad, sizeof(TGaBoardArticleLoad));
                                        // 佬绰 辆幅 积己
                                        pNewArticleLoad.AgitNum = 0;
                                        pNewArticleLoad.GuildName = gname;
                                        pNewArticleLoad.OrgNum = 0;
                                        pNewArticleLoad.SrcNum1 = 0;
                                        pNewArticleLoad.SrcNum2 = 0;
                                        pNewArticleLoad.SrcNum3 = 0;
                                        pNewArticleLoad.Kind = SqlEngn.KIND_NOTICE;
                                        pNewArticleLoad.UserName = "GuildMaster";
                                        // 荐沥 (sonmg 2005/05/06)
                                        pNewArticleLoad.Content = "Guild master\'s message is empty.";
                                        pArticleList.Insert(Guild.GABOARD_NOTICE_LINE - 1, pNewArticleLoad);
                                        break;
                                    }
                                    else
                                    {
                                        // 昏力...
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
                // 秦寸 巩颇狼 霸矫魄 府胶飘.
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
                                    FillChar(pArticleLoad.Content, sizeof(pArticleLoad.Content), '\0');
                                    StrPLCopy(pArticleLoad.Content, data, sizeof(pArticleLoad.Content) - 1);
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

