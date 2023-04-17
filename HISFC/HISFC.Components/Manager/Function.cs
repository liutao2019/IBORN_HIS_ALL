using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager
{
    public class Function
    {
        /// <summary>
        /// 信息发送
        /// </summary>
        /// <param name="alInfo">所有信息</param>
        /// <param name="operType">操作类别</param>
        /// <param name="infoType">数据类别</param>
        /// <param name="errInfo">错误信息</param>
        /// <returns>-1发送失败</returns>
        public static int SendBizMessage(ArrayList alInfo, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumOperType operType, FS.SOC.HISFC.BizProcess.MessagePatternInterface.EnumInfoType infoType, ref string errInfo)
        {
            object MessageSender = InterfaceManager.GetBizInfoSenderImplement();
            if (MessageSender is FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender)
            {
                return ((FS.SOC.HISFC.BizProcess.MessagePatternInterface.ISender)MessageSender).Send(alInfo, operType, infoType, ref errInfo);
            }
            else if (MessageSender == null)
            {
                errInfo = "没维护接口：FS.SOC.HISFC.BizProcess.MessagePaternInterface.ISender的实现";
                return 0;
            }

            //测试一下
            //FS.SOC.HISFC.BizLogic.MessagePattern.MessageSenderInterfaceImplement MessageSenderInterfaceImplement = new FS.SOC.HISFC.BizLogic.MessagePattern.MessageSenderInterfaceImplement();
            //return MessageSenderInterfaceImplement.Send(alInfo, operType, infoType, ref errInfo);

            errInfo = "接口实现不是指定类型：FS.SOC.HISFC.BizProcess.MessagePaternInterface.ISender";
            return -1;
        }
        /// <summary>
        /// 显示消息，MessageBox的统一风格
        /// </summary>
        /// <param name="text">提示内容</param>
        /// <param name="messageBoxIcon">图标</param>
        public static void ShowMessage(string text, System.Windows.Forms.MessageBoxIcon messageBoxIcon)
        {

            string caption = "";
            switch (messageBoxIcon)
            {
                case System.Windows.Forms.MessageBoxIcon.Warning:
                    caption = "警告>>";
                    break;
                case System.Windows.Forms.MessageBoxIcon.Error:
                    caption = "错误>>";
                    break;
                default:
                    caption = "提示>>";
                    break;
            }
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            System.Windows.Forms.MessageBox.Show(text, caption, System.Windows.Forms.MessageBoxButtons.OK, messageBoxIcon);
        }
        #region 等待对话框
        private static FS.HISFC.Components.Manager.Forms.frmCaWait frmWaitForm = new FS.HISFC.Components.Manager.Forms.frmCaWait();

        /// <summary>
        /// 当前等待窗口
        /// </summary>
        public static FS.HISFC.Components.Manager.Forms.frmCaWait WaitForm
        {
            get
            {
                return frmWaitForm;
            }
            set
            {
                frmWaitForm = value;
            }
        }

        /// <summary>
        /// 显示等待窗口
        /// </summary>
        /// <param name="tip"></param>
        public static void ShowWaitForm(string tip, int Progress, int Max, bool IsShowCancelButton)
        {
            FS.HISFC.Components.Manager.Function.WaitForm.progressBar1.Maximum = Max;
            if (frmWaitForm == null) frmWaitForm = new FS.HISFC.Components.Manager.Forms.frmCaWait();
            if (tip != "") frmWaitForm.Tip = tip;
            if (Progress >= 0) frmWaitForm.Progress = Progress;
            frmWaitForm.IsShowCancelButton = IsShowCancelButton;
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor;
            if (frmWaitForm.Visible == false)
            {
                frmWaitForm.Show();
            }
        }

        /// <summary>
        /// 关闭等待窗口
        /// </summary>
        public static void HideWaitForm()
        {
            Cursor.Current = System.Windows.Forms.Cursors.Default;
            WaitForm.Hide();
        }
        #endregion
    }
}
