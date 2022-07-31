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
            //// 游戏辅助
            //CheckBoxWhisperRecord.Checked = M2Share.g_GameConfig.boWhisperRecord;
            //// 私聊记录
            //CheckBoxNoFog.Checked = M2Share.g_GameConfig.boNoFog;
            //// 免蜡烛
            //CheckBoxStallSystem.Checked = M2Share.g_GameConfig.boStallSystem;
            //// 摆摊系统
            //CheckBoxSafeZoneStall.Checked = M2Share.boSafeZoneStall;
            //// 安全区摆摊
            //CheckBoxShowHpBar.Checked = M2Share.g_GameConfig.boShowHpBar;
            //// 显示血条
            //CheckShowHpNumber.Checked = M2Share.g_GameConfig.boShowHpNumber;
            //// 数字显血
            //CheckBoxNoStruck.Checked = M2Share.g_GameConfig.boNoStruck;
            //// 无后仰
            //CheckBoxFastMove.Checked = M2Share.g_GameConfig.boFastMove;
            //// 免助跑
            //CheckBoxNoWeight.Checked = M2Share.g_GameConfig.boNoWeight;
            //// 无负重
            //CheckShowFriend.Checked = M2Share.g_GameConfig.boShowFriend;
            //// 显示好友界面
            //CheckShowRelationship.Checked = M2Share.g_GameConfig.boShowRelationship;
            //// 显示关系界面
            //CheckShowMail.Checked = M2Share.g_GameConfig.boShowMail;
            //// 显示邮件界面
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
            //// 充值地图
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
            //    svMain.MainOutMessage("游戏设置成功...");
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