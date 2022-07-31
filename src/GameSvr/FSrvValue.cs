using System.Windows.Forms;
using SystemModule;

namespace GameSvr
{
    public partial class TFrmServerValue : Form
    {
        public TFrmServerValue()
        {
            InitializeComponent();
        }

        public bool Execute()
        {
            bool result;
            result = false;
            EHum.Value = M2Share.HumLimitTime;
            EMon.Value = M2Share.MonLimitTime;
            EZen.Value = M2Share.ZenLimitTime;
            ESoc.Value = M2Share.SocLimitTime;
            EDec.Value = M2Share.DecLimitTime;
            ENpc.Value = M2Share.NpcLimitTime;
            ESendBlock.Value = M2Share.SENDBLOCK;
            ECheckBlock.Value = M2Share.SENDCHECKBLOCK;
            EAvailableBlock.Value = M2Share.SENDAVAILABLEBLOCK;
            EGateLoad.Value = M2Share.GATELOAD;
            CbViewHack.Checked = M2Share.BoViewHackCode;
            CkViewAdmfail.Checked = M2Share.BoViewAdmissionfail;
            //if (this.ShowModal == System.Windows.Forms.DialogResult.OK)
            //{
            //    svMain.HumLimitTime = HUtil32._MIN(150, EHum.Value);
            //    svMain.MonLimitTime = HUtil32._MIN(150, EMon.Value);
            //    svMain.ZenLimitTime = HUtil32._MIN(150, EZen.Value);
            //    svMain.SocLimitTime = HUtil32._MIN(150, ESoc.Value);
            //    svMain.DecLimitTime = HUtil32._MIN(150, EDec.Value);
            //    svMain.NpcLimitTime = HUtil32._MIN(150, ENpc.Value);
            //    svMain.SENDBLOCK = _MAX(10, ESendBlock.Value);
            //    svMain.SENDCHECKBLOCK = _MAX(10, ECheckBlock.Value);
            //    svMain.SENDAVAILABLEBLOCK = _MAX(10, EAvailableBlock.Value);
            //    svMain.GATELOAD = EGateLoad.Value;
            //    svMain.BoViewHackCode = CbViewHack.Checked;
            //    svMain.BoViewAdmissionfail = CkViewAdmfail.Checked;
            //    result = true;
            //}
            return result;
        }

        // Note: the original parameters are Object Sender, ref char Key
        public void EHumKeyPress(object Sender, System.Windows.Forms.KeyPressEventArgs _e1)
        {
            if (Sender is NumericUpDown)
            {
                (Sender as NumericUpDown).Value = HUtil32.Str_ToInt((Sender as NumericUpDown).Text, 0);
            }
        }
    }
}

namespace GameSvr
{
    public class FSrvValue
    {
        public static TFrmServerValue FrmServerValue = null;
    }
}