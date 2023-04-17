using System;
using System.Collections;
using System.Text;
using FS.FrameWork.Models;
using FS.FrameWork.Function;
using FS.HISFC.Models.Registration;
using System.Data;
using FS.HISFC.Models.Fee.Outpatient;
using FS.HISFC.BizProcess.Interface.FeeInterface;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections.Generic;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Gyzl.IOrder
{
    public class Function
    {
        /// <summary>
        /// 获取公费报表统计大类
        /// </summary>
        /// <returns></returns>
        public static DataTable GetGFReportDataFeeCodeStat()
        {
            FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
            DataSet ds = new DataSet();
            outpatientManager.GetInvoiceClass("MZGF", ref ds);

            return ds.Tables[0];
        }

    }
}
