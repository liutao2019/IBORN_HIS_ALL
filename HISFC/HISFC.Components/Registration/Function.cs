using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.Components.Registration
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

        #region add by lijp 2012-08-24 {5195341F-12C4-41cb-B2E0-EB35365990FC}
        /// <summary>
        /// 身份证校验
        /// </summary>
        /// <param name="idNO"></param>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public static int ProcessIDENNO(string idNO, EnumCheckIDNOType enumType)
        {
            string errText = string.Empty;

            //校验身份证号
            string idNOTmp = string.Empty;

            if (idNO.Length == 15)
            {
                idNOTmp = FS.FrameWork.WinForms.Classes.Function.TransIDFrom15To18(idNO);
            }
            else
            {
                idNOTmp = idNO;
            }

            //校验身份证号
            int returnValue = FS.FrameWork.WinForms.Classes.Function.CheckIDInfo(idNOTmp, ref errText);

            if (returnValue < 0)
            {
                System.Windows.Forms.MessageBox.Show(errText);
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 判断身份证//{6B6167F7-3A9B-4f6c-9326-C5CD6AA3AC98}身份证信息
        /// </summary>
        public enum EnumCheckIDNOType
        {
            /// <summary>
            /// 保存之前校验
            /// </summary>
            BeforeSave = 0,

            /// <summary>
            /// 保存时校验
            /// </summary>
            Saveing
        }
        #endregion

        /// <summary>
        /// 读卡接口
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperCard(ref string cardNO, ref string errInfo)
        {
            if (InterfaceManager.GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = InterfaceManager.GetIOperCard().ReadCardNO(ref cardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 读取物理卡号
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public static int OperMCard(ref string McardNO, ref string errInfo)
        {
            if (InterfaceManager.GetIOperCard() == null)
            {
                errInfo = "没有维护读卡接口！";
                return -1;
            }

            int result = InterfaceManager.GetIOperCard().ReadMCardNO(ref McardNO, ref  errInfo);
            if (result == -1)
            {
                errInfo = "读卡失败，请确认是否正确放置诊疗卡！";
                return -1;
            }

            return 1;
        }
    }
}
