using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Components.Manager
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
    }
}
