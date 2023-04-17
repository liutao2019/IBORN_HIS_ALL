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

namespace FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy
{
    public class Function
    {

        /// <summary>
        /// 返回项目比例
        /// </summary>
        /// <param name="pactId">合同单位编码</param>
        /// <param name="f">费用明细</param>
        /// <returns></returns>
        public static FS.HISFC.Models.Base.PactItemRate PactRate(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f, ref string errMsg)
        {
            FS.HISFC.Models.Base.PactItemRate pRate = new FS.HISFC.Models.Base.PactItemRate();
            pRate.Rate.RebateRate = 0;
            return pRate;
        }


        /// <summary>
        /// 返回执行科室
        /// </summary>
        /// <param name="recipeDept"></param>
        /// <param name="item"></param>
        /// <param name="errorInfo"></param>
        /// <returns></returns>
        public static ArrayList GetExecDept(FS.FrameWork.Models.NeuObject recipeDept, FS.HISFC.Models.Fee.Item.Undrug item, ref string errorInfo)
        {
            FS.HISFC.BizProcess.Interface.Fee.IExecDept IExecDept = InterfaceManager.GetIExecDept();

            if (IExecDept != null)
            {
                return IExecDept.GetExecDept(recipeDept, item, ref errorInfo);
            }

            return null;
        }

        public static DataTable GetGFJZ()
        {
            FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();
            DataSet ds = new DataSet();
            outpatientManager.GetInvoiceClass("MZJZ", ref ds);

            return ds.Tables[0];
        }

    }
}
