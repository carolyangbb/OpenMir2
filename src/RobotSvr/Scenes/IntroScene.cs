namespace RobotSvr
{
    public class IntroScene : Scene
    {
        public bool MBoOnClick = false;
        public long m_dwStartTime = 0;

        public IntroScene(RobotClient robotClient) : base(SceneType.stIntro, robotClient)
        {

        }

        public override void OpenScene()
        {
            MBoOnClick = false;
            m_dwStartTime = MShare.GetTickCount() + 2 * 1000;
        }

        public override void CloseScene()
        {
        }

        public override void PlayScene()
        {
            if (MShare.GetTickCount() > m_dwStartTime)
            {
                MBoOnClick = true;
                robotClient.DScreen.ChangeScene(SceneType.stLogin);
                if (!MShare.g_boDoFadeOut && !MShare.g_boDoFadeIn)
                {
                    MShare.g_boDoFadeIn = true;
                    MShare.g_nFadeIndex = 0;
                }
            }
        }
    }
}