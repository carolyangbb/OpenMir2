using System;
using System.Collections;
using System.Drawing;

namespace RobotSvr
{
    public class DrawScreen
    {
        private readonly RobotClient robotClient;
        private readonly long _mDwFrameTime = 0;
        private readonly long _mDwFrameCount = 0;
        private readonly ArrayList _mSysMsgList = null;
        private readonly ArrayList _mSysMsgListEx = null;
        private readonly ArrayList _mSysMsgListEx2 = null;
        public Scene CurrentScene = null;
        public ArrayList ChatStrs = null;
        public ArrayList ChatBks = null;
        public ArrayList MAdList = null;
        public ArrayList MAdList2 = null;


        public DrawScreen(RobotClient robotClient)
        {
            CurrentScene = null;
            _mDwFrameTime = MShare.GetTickCount();
            _mDwFrameCount = 0;
            _mSysMsgList = new ArrayList();
            _mSysMsgListEx = new ArrayList();
            _mSysMsgListEx2 = new ArrayList();
            ChatStrs = new ArrayList();
            MAdList = new ArrayList();
            MAdList2 = new ArrayList();
            ChatBks = new ArrayList();
        }

        public void Initialize()
        {

        }

        public void ChangeScene(SceneType scenetype)
        {
            if (CurrentScene != null)
            {
                CurrentScene.CloseScene();
            }
            switch (scenetype)
            {
                case SceneType.stIntro:
                    CurrentScene = robotClient.IntroScene;
                    robotClient.IntroScene.m_dwStartTime = MShare.GetTickCount() + 2000;
                    break;
                case SceneType.stLogin:
                    CurrentScene = robotClient.LoginScene;
                    break;
                case SceneType.stSelectCountry:
                    break;
                case SceneType.stSelectChr:
                    CurrentScene = robotClient.SelectChrScene;
                    break;
                case SceneType.stNewChr:
                    break;
                case SceneType.stLoading:
                    break;
                case SceneType.stLoginNotice:
                    CurrentScene = robotClient.LoginNoticeScene;
                    break;
                case SceneType.stPlayGame:
                    CurrentScene = robotClient.g_PlayScene;
                    break;
            }
            if (CurrentScene != null)
            {
                CurrentScene.OpenScene();
            }
        }

        public void AddSysMsg(string msg)
        {
            if (_mSysMsgList.Count >= 10)
            {
                _mSysMsgList.Remove(0);
            }
            //_mSysMsgList.Add(msg, MShare.GetTickCount() as Object);
        }

        public void AddSysMsgBottom(string msg)
        {
            if (_mSysMsgListEx.Count >= 10)
            {
                _mSysMsgListEx.Remove(0);
            }
            // _mSysMsgListEx.Add(msg, MShare.GetTickCount() as Object);
        }

        public void AddSysMsgBottom2(string msg)
        {
            if (_mSysMsgListEx2.Count >= 10)
            {
                _mSysMsgListEx2.Remove(0);
            }
            // _mSysMsgListEx2.Add(msg, MShare.GetTickCount() as Object);
        }

        public void AddSysMsgCenter(string msg, Color fc, Color bc, int sec)
        {

        }

        public void AddSysMsgCenter(string msg, int fc, int bc, int sec)
        {

        }

        public void AddChatBoardString(string str, Color fcolor, Color bcolor)
        {
            Console.WriteLine(str);
        }

        public void AddChatBoardString(string str, int fcolor, Color bcolor)
        {
            Console.WriteLine(str);
        }

        public void AddChatBoardString(string str, int fcolor, int bcolor)
        {
            Console.WriteLine(str);
        }

        public void ClearChatBoard()
        {
            _mSysMsgList.Clear();
            _mSysMsgListEx.Clear();
            _mSysMsgListEx2.Clear();
            ChatStrs.Clear();
            ChatBks.Clear();
        }

        public void ClearHint()
        {

        }

        public void DrawHint()
        {

        }
    }
}

