using System.Collections;

namespace GameSvr
{
    public class TFrmGameConfig
    {
        public TFrmGameConfig()
        {

        }

        public void EditExpErienceLevelChange(object Sender, System.EventArgs _e1)
        {
            // n := StrToInt(Text);
            // if n > 255 then n := 255;
            // if n < 1 then n := 1;
        }

        public void EditExpErienceLevelKeyPress(object Sender, ref char Key)
        {
            if (!new ArrayList(new string[] { "0", "\08" }).Contains(Key))
            {
                Key = '\0';
            }
        }

        public bool Execute()
        {
            bool result;
            result = false;
            //CheckBoxGameAssist.Checked = M2Share.g_GameConfig.boGameAssist;
            //// ��Ϸ����
            //CheckBoxWhisperRecord.Checked = M2Share.g_GameConfig.boWhisperRecord;
            //// ˽�ļ�¼
            //CheckBoxNoFog.Checked = M2Share.g_GameConfig.boNoFog;
            //// ������
            //CheckBoxStallSystem.Checked = M2Share.g_GameConfig.boStallSystem;
            //// ��̯ϵͳ
            //CheckBoxSafeZoneStall.Checked = M2Share.boSafeZoneStall;
            //// ��ȫ����̯
            //CheckBoxShowHpBar.Checked = M2Share.g_GameConfig.boShowHpBar;
            //// ��ʾѪ��
            //CheckShowHpNumber.Checked = M2Share.g_GameConfig.boShowHpNumber;
            //// ������Ѫ
            //CheckBoxNoStruck.Checked = M2Share.g_GameConfig.boNoStruck;
            //// �޺���
            //CheckBoxFastMove.Checked = M2Share.g_GameConfig.boFastMove;
            //// ������
            //CheckBoxNoWeight.Checked = M2Share.g_GameConfig.boNoWeight;
            //// �޸���
            //CheckShowFriend.Checked = M2Share.g_GameConfig.boShowFriend;
            //// ��ʾ���ѽ���
            //CheckShowRelationship.Checked = M2Share.g_GameConfig.boShowRelationship;
            //// ��ʾ��ϵ����
            //CheckShowMail.Checked = M2Share.g_GameConfig.boShowMail;
            //// ��ʾ�ʼ�����
            //CheckShowRecharging.Checked = M2Share.g_GameConfig.boShowRecharging;
            //CheckShowHelp.Checked = M2Share.g_GameConfig.boShowHelp;
            //CheckBoxGameShop.Checked = M2Share.g_GameConfig.boShowGameShop;
            //CheckBopath.Checked = M2Share.g_GameConfig.boGamepath;
            //CheckBoxSecondCard.Checked = M2Share.boSecondCardSystem;
            //EditExpErienceLevel.Text = (M2Share.g_nExpErienceLevel).ToString();
            //EditBadNameHomeMap.Text = M2Share.BADMANHOMEMAP;
            //EditBadNameStartX.Text = (M2Share.BADMANSTARTX).ToString();
            //EditBadNameStartY.Text = (M2Share.BADMANSTARTY).ToString();
            //EditBadNameCoziMap.Text = M2Share.RECHARGINGMAP;
            //// ��ֵ��ͼ
            //if (this.ShowModal == System.Windows.Forms.DialogResult.OK)
            //{
            //    M2Share.g_GameConfig.boNoFog = CheckBoxNoFog.Checked;
            //    M2Share.g_GameConfig.boStallSystem = CheckBoxStallSystem.Checked;
            //    M2Share.boSafeZoneStall = CheckBoxSafeZoneStall.Checked;
            //    M2Share.g_GameConfig.boShowHpBar = CheckBoxShowHpBar.Checked;
            //    M2Share.g_GameConfig.boShowHpNumber = CheckShowHpNumber.Checked;
            //    M2Share.g_GameConfig.boShowFriend = CheckShowFriend.Checked;
            //    M2Share.g_GameConfig.boShowRelationship = CheckShowRelationship.Checked;
            //    M2Share.g_GameConfig.boShowMail = CheckShowMail.Checked;
            //    M2Share.g_GameConfig.boShowRecharging = CheckShowRecharging.Checked;
            //    M2Share.g_GameConfig.boShowHelp = CheckShowHelp.Checked;
            //    M2Share.g_GameConfig.boWhisperRecord = CheckBoxWhisperRecord.Checked;
            //    M2Share.g_GameConfig.boShowGameShop = CheckBoxGameShop.Checked;
            //    M2Share.g_GameConfig.boFastMove = CheckBoxFastMove.Checked;
            //    M2Share.g_GameConfig.boNoStruck = CheckBoxNoStruck.Checked;
            //    M2Share.g_GameConfig.boNoWeight = CheckBoxNoWeight.Checked;
            //    M2Share.g_GameConfig.boGamepath = CheckBopath.Checked;
            //    M2Share.boSecondCardSystem = CheckBoxSecondCard.Checked;
            //    M2Share.g_nExpErienceLevel = Convert.ToInt32(EditExpErienceLevel.Text);
            //    M2Share.BADMANHOMEMAP = EditBadNameHomeMap.Text;
            //    M2Share.BADMANSTARTX = Convert.ToInt32(EditBadNameStartX.Text);
            //    M2Share.BADMANSTARTY = Convert.ToInt32(EditBadNameStartY.Text);
            //    M2Share.RECHARGINGMAP = EditBadNameCoziMap.Text;
            //    svMain.UserEngine.ApplyGameConfig();
            //    svMain.MainOutMessage("��Ϸ���óɹ�...");
            //    result = true;
            //}
            return result;
        }

    }
}

namespace GameSvr
{
    public class FGameSet
    {
        public static TFrmGameConfig FrmGameConfig = null;
    } // end FGameSet
}