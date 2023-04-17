using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.Report.Base
{
    /// <summary>
    /// [功能描述: 公共函数]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-08]<br></br>
    /// 说明：
    /// 1、dll内共用函数
    /// </summary>
    public class Function
    {
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
