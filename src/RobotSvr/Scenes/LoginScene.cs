using System;
using SystemModule.Packet;

namespace RobotSvr
{
    public class LoginScene : Scene
    {
        private int _mNCurFrame = 0;
        private int _mNMaxFrame = 0;
        private long _mDwStartTime = 0;
        private bool _mBoOpenFirst = false;
        private TUserEntry _mNewIdRetryUe = null;
        private TUserEntryAdd _mNewIdRetryAdd = null;
        public string MSLoginId = String.Empty;
        public string MSLoginPasswd = String.Empty;
        public long MEditIdHandle = 0;
        public object MEditIdPointer = null;
        public long MEditPassHandle = 0;
        public object MEditPassPointer = null;

        public LoginScene() : base(SceneType.stLogin)
        {
            _mBoOpenFirst = false;
        }


        public override void OpenScene()
        {
            _mNCurFrame = 0;
            _mNMaxFrame = 10;
            MSLoginId = "";
            MSLoginPasswd = "";
            _mBoOpenFirst = true;
        }

        // 离开登录界面
        public override void CloseScene()
        {

        }

        public void PassWdFail()
        {
 
        }

        public void HideLoginBox()
        {
            ChangeLoginState(LoginState.LsCloseAll);
        }

        public void OpenLoginDoor()
        {
            _mDwStartTime = MShare.GetTickCount();
            HideLoginBox();
        }

        public override void PlayScene()
        {
            if (_mBoOpenFirst)
            {
                _mBoOpenFirst = false;
            }
        }

        public void ChangeLoginState(LoginState state)
        {
            switch (state)
            {
                case LoginState.LsLogin:

                    break;
                case LoginState.LsNewidRetry:
                case LoginState.LsNewid:

                    break;
                case LoginState.LsChgpw:

                    break;
                case LoginState.LsCloseAll:

                    break;
            }
        }

        public void NewClick()
        {
            ChangeLoginState(LoginState.LsNewid);
        }

        public void NewIdRetry(bool boupdate)
        {
            ChangeLoginState(LoginState.LsNewidRetry);
        }

        public void UpdateAccountInfos(TUserEntry ue)
        {
            _mNewIdRetryUe = ue; 
            NewIdRetry(true);
        }

        public void OkClick()
        {

        }

        public void ChgPwClick()
        {
            ChangeLoginState(LoginState.LsChgpw);
        }

        private bool CheckUserEntrys()
        {
            bool result;
            result = true;
            return result;
        }

        public void NewAccountOk()
        {
            TUserEntry ue;
            TUserEntryAdd ua;
            if (CheckUserEntrys())
            {
                ue = new TUserEntry();
                ua = new TUserEntryAdd();
                //ue.sAccount = FrmDlg.DEditNewID.Text.ToLower();
                //ue.sPassword = FrmDlg.DEditNewPassWord.Text;
                //ue.sUserName = FrmDlg.DEditNewYourName.Text;
                //ue.sSSNo = FrmDlg.DEditNewIDcard.Text;
                //ue.sQuiz = FrmDlg.DEditNewQuiz1.Text;
                //ue.sAnswer = FrmDlg.DEditNewAnswer1.Text.Trim();
                //ue.sPhone = FrmDlg.DEditNewPhone.Text;
                //ue.sEMail = FrmDlg.DEditNewEMail.Text.Trim();
                //ua.sQuiz2 = FrmDlg.DEditNewQuiz1.Text;
                //ua.sAnswer2 = FrmDlg.DEditNewAnswer1.Text.Trim();
                //ua.sBirthDay = FrmDlg.DEditNewBirthDay.Text;
                //ua.sMobilePhone = FrmDlg.DEditNewMobPhone.Text;
                _mNewIdRetryUe = ue;
                _mNewIdRetryUe.sAccount = "";
                _mNewIdRetryUe.sPassword = "";
                _mNewIdRetryAdd = ua;
                //ClMain.frmMain.SendNewAccount(ue, ua); // 发送注册帐号信息
                NewAccountClose();
                Console.WriteLine("注册账号");
            }
        }

        public void NewAccountClose()
        {
            ChangeLoginState(LoginState.LsLogin);
        }

        public void ChgpwOk()
        {
            
        }

        public void ChgpwCancel()
        {
            ChangeLoginState(LoginState.LsLogin);
        }
    }
}