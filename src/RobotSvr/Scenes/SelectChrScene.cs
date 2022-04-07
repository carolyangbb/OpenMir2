using System;

namespace RobotSvr
{
    public class SelectChrScene : Scene
    {
        private bool _createChrMode = false;
        public int NewIndex = 0;
        public SelChar[] ChrArr;

        public SelectChrScene(RobotClient robotClient) : base(SceneType.stSelectChr, robotClient)
        {
            _createChrMode = false;
            ChrArr = new SelChar[2];
            ChrArr[0].FreezeState = true;
            ChrArr[0].UserChr = new TUserCharacterInfo();
            ChrArr[1].FreezeState = true;
            ChrArr[1].UserChr = new TUserCharacterInfo();
            NewIndex = 0;
        }

        public override void OpenScene()
        {

        }

        public override void CloseScene()
        {

        }

        public void SelChrSelect1Click()
        {
            if ((!ChrArr[0].Selected) && ChrArr[0].Valid && ChrArr[0].FreezeState)
            {
                //ClMain.frmMain.SelectChr(ChrArr[0].UserChr.Name);
                ChrArr[0].Selected = true;
                ChrArr[1].Selected = false;
                ChrArr[0].Unfreezing = true;
                ChrArr[0].AniIndex = 0;
                ChrArr[0].DarkLevel = 0;
                ChrArr[0].EffIndex = 0;
                ChrArr[0].StartTime = MShare.GetTickCount();
                ChrArr[0].Moretime = MShare.GetTickCount();
                ChrArr[0].Startefftime = MShare.GetTickCount();
            }
        }

        public void SelChrSelect2Click()
        {
            if ((!ChrArr[1].Selected) && ChrArr[1].Valid && ChrArr[1].FreezeState)
            {
                robotClient.SelectChr(ChrArr[1].UserChr.Name);
                ChrArr[1].Selected = true;
                ChrArr[0].Selected = false;
                ChrArr[1].Unfreezing = true;
                ChrArr[1].AniIndex = 0;
                ChrArr[1].DarkLevel = 0;
                ChrArr[1].EffIndex = 0;
                ChrArr[1].StartTime = MShare.GetTickCount();
                ChrArr[1].Moretime = MShare.GetTickCount();
                ChrArr[1].Startefftime = MShare.GetTickCount();
            }
        }

        public void SelChrStartClick()
        {
            string chrname = "";
            if (ChrArr[0].Valid && ChrArr[0].Selected)
            {
                chrname = ChrArr[0].UserChr.Name;
            }
            if (ChrArr[1].Valid && ChrArr[1].Selected)
            {
                chrname = ChrArr[1].UserChr.Name;
            }
            if (!string.IsNullOrEmpty(chrname))
            {
                if (!MShare.g_boDoFadeOut && !MShare.g_boDoFadeIn)
                {
                    MShare.g_boDoFastFadeOut = true;
                    MShare.g_nFadeIndex = 29;
                }
                robotClient.SendSelChr(chrname);
            }
            else
            {
                Console.WriteLine("开始游戏前你应该先创建一个新角色！\\点击<创建角色>按钮创建一个游戏角色。");
            }
        }

        public void SelChrNewChrClick()
        {
            if (!ChrArr[0].Valid || !ChrArr[1].Valid)
            {
                if (!ChrArr[0].Valid)
                {
                    MakeNewChar(0);
                }
                else
                {
                    MakeNewChar(1);
                }
            }
            else
            {
                Console.WriteLine("一个帐号最多只能创建 2 个游戏角色！");
            }
        }

        public void SelChrEraseChrClick()
        {
            int n;
            n = 0;
            if (ChrArr[0].Valid && ChrArr[0].Selected)
            {
                n = 0;
            }
            if (ChrArr[1].Valid && ChrArr[1].Selected)
            {
                n = 1;
            }
            if (ChrArr[n].Valid && (!ChrArr[n].FreezeState) && (ChrArr[n].UserChr.Name != ""))
            {
                robotClient.SendDelChr(ChrArr[n].UserChr.Name);
            }
        }

        public void SelChrCreditsClick()
        {
            // [失败] 没有找到被删除的角色。
            // [失败] 客户端版本错误。
            // [失败] 你没有这个角色。
            // [失败] 角色已被删除。
            // [失败] 角色数据读取失败，请稍候再试。
            // [失败] 你选择的服务器用户满员。
        }

        public void SelChrExitClick()
        {
            //ClMain.frmMain.Close();
        }

        public void ClearChrs()
        {
            ChrArr[0].FreezeState = false;
            ChrArr[1].FreezeState = true;
            ChrArr[0].Selected = true;
            ChrArr[1].Selected = false;
            ChrArr[0].UserChr.Name = "";
            ChrArr[1].UserChr.Name = "";
        }

        public void AddChr(string uname, int job, int hair, int level, int sex)
        {
            int n;
            if (!ChrArr[0].Valid)
            {
                n = 0;
            }
            else if (!ChrArr[1].Valid)
            {
                n = 1;
            }
            else
            {
                return;
            }
            ChrArr[n].UserChr.Name = uname;
            ChrArr[n].UserChr.Job = (byte)job;
            ChrArr[n].UserChr.hair = (byte)hair;
            ChrArr[n].UserChr.Level = (byte)level;
            ChrArr[n].UserChr.Sex = (byte)sex;
            ChrArr[n].Valid = true;
        }

        private void MakeNewChar(int index)
        {
            _createChrMode = true;
            NewIndex = index;
            ChrArr[NewIndex].Valid = true;
            ChrArr[NewIndex].FreezeState = false;
            SelectChr(NewIndex);
        }

        public void SelectChr(int index)
        {
            ChrArr[index].Selected = true;
            ChrArr[index].DarkLevel = 30;
            ChrArr[index].StartTime = MShare.GetTickCount();
            ChrArr[index].Moretime = MShare.GetTickCount();
            if (index == 0)
            {
                ChrArr[1].Selected = false;
            }
            else
            {
                ChrArr[0].Selected = false;
            }
        }

        public void SelChrNewClose()
        {
            ChrArr[NewIndex].Valid = false;
            _createChrMode = false;
            ChrArr[NewIndex].Selected = true;
            ChrArr[NewIndex].FreezeState = false;
        }

        public void SelChrNewOk()
        {
            //string chrname = FrmDlg.DEditChrName.Text.Trim();
            //if (chrname != "")
            //{
            //    ChrArr[NewIndex].Valid = false;
            //    _createChrMode = false;
            //    ChrArr[NewIndex].Selected = true;
            //    ChrArr[NewIndex].FreezeState = false;
            //    shair = (1 + new System.Random(5).Next()).ToString();
            //    sjob = ChrArr[NewIndex].UserChr.Job.ToString();
            //    ssex = ChrArr[NewIndex].UserChr.Sex.ToString();
            //    //ClMain.frmMain.SendNewChr(ClMain.frmMain.LoginID, chrname, shair, sjob, ssex);
            //}
        }

        public void SelChrNewJob(int job)
        {
            if (job >= 0 && job <= 2 && (ChrArr[NewIndex].UserChr.Job != job))
            {
                ChrArr[NewIndex].UserChr.Job = (byte)job;
                SelectChr(NewIndex);
            }
        }

        public void SelChrNewm_btSex(int sex)
        {
            if (sex != ChrArr[NewIndex].UserChr.Sex)
            {
                ChrArr[NewIndex].UserChr.Sex = (byte)sex;
                SelectChr(NewIndex);
            }
        }

        public void SelChrNewPrevHair()
        {
        }

        public void SelChrNewNextHair()
        {
        }

        public override void PlayScene()
        {
            if (MShare.g_boOpenAutoPlay && (MShare.g_nAPReLogon == 2))
            {
                if (MShare.GetTickCount() - MShare.g_nAPReLogonWaitTick > MShare.g_nAPReLogonWaitTime)
                {
                    MShare.g_nAPReLogonWaitTick = MShare.GetTickCount();
                    MShare.g_nAPReLogon = 3;
                    if (!MShare.g_boDoFadeOut && !MShare.g_boDoFadeIn)
                    {
                        MShare.g_boDoFastFadeOut = true;
                        MShare.g_nFadeIndex = 29;
                    }
                    robotClient.SendSelChr(robotClient.m_sCharName);
                }
            }
            for (var n = 0; n < 1; n++)
            {
                if (ChrArr[n].Valid)
                {
                    if (ChrArr[n].Unfreezing)
                    {
                        if (MShare.GetTickCount() - ChrArr[n].StartTime > 110)
                        {
                            ChrArr[n].StartTime = MShare.GetTickCount();
                            ChrArr[n].AniIndex = ChrArr[n].AniIndex + 1;
                        }
                        if (MShare.GetTickCount() - ChrArr[n].Startefftime > 110)
                        {
                            ChrArr[n].Startefftime = MShare.GetTickCount();
                            ChrArr[n].EffIndex = ChrArr[n].EffIndex + 1;
                        }
                        if (ChrArr[n].AniIndex > IntroScn.Freezeframe - 1)
                        {
                            ChrArr[n].Unfreezing = false;
                            ChrArr[n].FreezeState = false;
                            ChrArr[n].AniIndex = 0;
                        }
                    }
                    else if (!ChrArr[n].Selected && !ChrArr[n].FreezeState && !ChrArr[n].Freezing)
                    {
                        ChrArr[n].Freezing = true;
                        ChrArr[n].AniIndex = 0;
                        ChrArr[n].StartTime = MShare.GetTickCount();
                    }
                    if (ChrArr[n].Freezing)
                    {
                        if (MShare.GetTickCount() - ChrArr[n].StartTime > 110)
                        {
                            ChrArr[n].StartTime = MShare.GetTickCount();
                            ChrArr[n].AniIndex = ChrArr[n].AniIndex + 1;
                        }
                        if (ChrArr[n].AniIndex > IntroScn.Freezeframe - 1)
                        {
                            ChrArr[n].Freezing = false;
                            ChrArr[n].FreezeState = true;
                            ChrArr[n].AniIndex = 0;
                        }
                    }
                    if (!ChrArr[n].Unfreezing && !ChrArr[n].Freezing)
                    {
                        if (ChrArr[n].Selected)
                        {
                            if (MShare.GetTickCount() - ChrArr[n].StartTime > 230)
                            {
                                ChrArr[n].StartTime = MShare.GetTickCount();
                                ChrArr[n].AniIndex = ChrArr[n].AniIndex + 1;
                                if (ChrArr[n].AniIndex > IntroScn.Selectedframe - 1)
                                {
                                    ChrArr[n].AniIndex = 0;
                                }
                            }
                            if (MShare.GetTickCount() - ChrArr[n].Moretime > 25)
                            {
                                ChrArr[n].Moretime = MShare.GetTickCount();
                                if (ChrArr[n].DarkLevel > 0)
                                {
                                    ChrArr[n].DarkLevel = ChrArr[n].DarkLevel - 1;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}