using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Neusoft.SOC.HISFC.OutpatientFee.Components.Report.Common.Helper
{
    public class PrintHelper
    {
        /// <summary>
        /// 纸张设置
        /// </summary>
        int PaperSize
        {
            get;
            set;
        }

        /// <summary>
        /// 自定义纸张长度
        /// </summary>
        int CustomPageLength
        {
            get;
            set;
        }

        /// <summary>
        /// 自定义纸张宽度
        /// </summary>
        int CustomPageWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 打印机名称
        /// </summary>
        string PrintName
        {
            get;
            set;
        }

        /// <summary>
        /// 左边距
        /// </summary>
        int MarginLeft
        {
            get;
            set;
        }

        /// <summary>
        /// 右边距
        /// </summary>
        int MarginRight
        {
            get;
            set;
        }

        /// <summary>
        /// 上边距
        /// </summary>
        int MarginTop
        {
            get;
            set;
        }

        /// <summary>
        /// 下边距
        /// </summary>
        int MarginBottom
        {
            get;
            set;
        }
    }
}
