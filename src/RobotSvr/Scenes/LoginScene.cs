using SystemModule.Packet;

namespace RobotSvr
{
    public class LoginScene : Scene
    {
        private bool _mBoOpenFirst = false;
        private TUserEntry _mNewIdRetryUe = null;
        private TUserEntryAdd _mNewIdRetryAdd = null;
        public string MSLoginId = string.Empty;
        public string MSLoginPasswd = string.Empty;

        public LoginScene(RobotClient robotClient) : base(SceneType.stLogin, robotClient)
        {
            _mBoOpenFirst = false;
        }

        public override void OpenScene()
        {
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

        public void NewIdRetry(bool boupdate)
        {
            ChangeLoginState(LoginState.LsNewidRetry);
        }

        public void UpdateAccountInfos(TUserEntry ue)
        {
            _mNewIdRetryUe = ue;
            NewIdRetry(true);
        }
    }
}