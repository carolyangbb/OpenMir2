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
            EHum.Value = svMain.HumLimitTime;
            EMon.Value = svMain.MonLimitTime;
            EZen.Value = svMain.ZenLimitTime;
            ESoc.Value = svMain.SocLimitTime;
            EDec.Value = svMain.DecLimitTime;
            ENpc.Value = svMain.NpcLimitTime;
            ESendBlock.Value = svMain.SENDBLOCK;
            ECheckBlock.Value = svMain.SENDCHECKBLOCK;
            EAvailableBlock.Value = svMain.SENDAVAILABLEBLOCK;
            EGateLoad.Value = svMain.GATELOAD;
            CbViewHack.Checked = svMain.BoViewHackCode;
            CkViewAdmfail.Checked = svMain.BoViewAdmissionfail;
            if (this.ShowModal == System.Windows.Forms.DialogResult.OK)
            {
                svMain.HumLimitTime = _MIN(150, EHum.Value);
                svMain.MonLimitTime = _MIN(150, EMon.Value);
                svMain.ZenLimitTime = _MIN(150, EZen.Value);
                svMain.SocLimitTime = _MIN(150, ESoc.Value);
                svMain.DecLimitTime = _MIN(150, EDec.Value);
                svMain.NpcLimitTime = _MIN(150, ENpc.Value);
                svMain.SENDBLOCK = _MAX(10, ESendBlock.Value);
                svMain.SENDCHECKBLOCK = _MAX(10, ECheckBlock.Value);
                svMain.SENDAVAILABLEBLOCK = _MAX(10, EAvailableBlock.Value);
                svMain.GATELOAD = EGateLoad.Value;
                svMain.BoViewHackCode = CbViewHack.Checked;
                svMain.BoViewAdmissionfail = CkViewAdmfail.Checked;
                result = true;
            }
            return result;
        }

        // Note: the original parameters are Object Sender, ref char Key
        public void EHumKeyPress(System.Object Sender, System.Windows.Forms.KeyPressEventArgs _e1)
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