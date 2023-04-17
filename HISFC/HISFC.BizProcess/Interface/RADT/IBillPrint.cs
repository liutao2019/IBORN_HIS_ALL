using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace FS.HISFC.BizProcess.Interface.RADT
{
    /// <summary>
    ///  住院床头卡打印接口
    /// </summary>
    public interface IBillPrint
    {
        /// <summary>
        /// 打印
        /// </summary>
        int Print();

        /// <summary>
        /// 打印
        /// </summary>
        int PrintPreview();

        /// <summary>
        /// 设置信息
        /// </summary>
        /// <param name="patients"></param>
        int ShowBill(ArrayList alData,FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        /// 导出{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        /// </summary>
        /// <returns></returns>
        int Export(FS.HISFC.Models.RADT.PatientInfo patient);

        /// <summary>
        ///  给要导出的控件绑定数据源{A57CF487-9900-42a4-AEB7-B94BAEC41AD1}
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        int ShowData(DataTable dt, FS.HISFC.Models.RADT.PatientInfo patient);
        
        /// <summary>
        /// 清空
        /// </summary>
        void Clear();
    }
}
