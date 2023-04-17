using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Printing;

namespace FS.SOC.HISFC.OutpatientFee.Components.Report.Common.Setting
{
    public class PrintInfo
    {
        PaperSize paperSize = new PaperSize("A4", 850,1169);
        bool selectPage = false;

        /// <summary>
        /// 纸张设置
        /// </summary>
        public PaperSize PaperSize
        {
            get
            {
                return paperSize;
            }
            set
            {
                paperSize = value;
            }
        }

        /// <summary>
        /// 是否选页打印
        /// </summary>
        public bool SelectPage
        {
            get
            {
                return selectPage;
            }
            set
            {
                selectPage = value;
            }
        }

    }
}
