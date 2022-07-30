using System;
using System.Windows.Forms;

namespace GameSvr
{
    public class M2Server
    {
        [STAThread]
        public static void Main(string[] args)
        {
            // Application.Initialize;
            svMain.FrmMain = new TFrmMain();
            //LocalDB.FrmDB = new TFrmDB();
            //IdSrvClient.FrmIDSoc = new TFrmIDSoc();
            //FSrvValue.Units.FSrvValue.FrmServerValue = new TFrmServerValue();
            //InterServerMsg.FrmSrvMsg = new TFrmSrvMsg();
            //InterMsgClient.FrmMsgClient = new TFrmMsgClient();
            //FGameSet.Units.FGameSet.FrmGameConfig = new TFrmGameConfig();
            Application.Run();
        }
    }
}

