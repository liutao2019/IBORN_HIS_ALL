using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.BizLogic.Material.Object;
using System.Data;

namespace FS.SOC.Local.Material.Print.GYSY
{
    /// <summary>
    /// 广医四院高值耗材接口
    /// </summary>
    public class GYSYBarCode : FS.HISFC.Interface.Material.MatProcess.IBarCode
    {

        #region 变量
        FS.HISFC.BizProcess.Material.Store.StockProcess stockProcess = new FS.HISFC.BizProcess.Material.Store.StockProcess();


        FS.HISFC.BizProcess.Material.Base.BaseProcess baseProcess = new FS.HISFC.BizProcess.Material.Base.BaseProcess();

        
        FS.HISFC.BizLogic.Manager.Constant constant = new FS.HISFC.BizLogic.Manager.Constant();


        string sql = @" SELECT r.INPATIENT_NO,r.DEPT_CODE FROM FIN_IPR_INMAININFO r WHERE r.PATIENT_NO='{0}'  ";
        #endregion


        #region IBarCode 成员

        public FS.HISFC.BizLogic.Material.Object.Input GetBarCodeInfoInput(string barCode, ref string msg)
        {
            if (barCode.Length > 6)
            {
                string customCode = barCode.Substring(0, 6);
                string batchNO = barCode.Substring(6);

                FS.HISFC.BizLogic.Material.Object.MatBase matBase = baseProcess.GetBaseInfoByCustomCode(customCode);
                if (customCode == null)
                {
                    msg = "不存在此物品,自定义码为" + customCode;
                    return null;
                }

                FS.HISFC.BizLogic.Material.Object.Input input = new FS.HISFC.BizLogic.Material.Object.Input();
                input.StockInfo.BaseInfo = matBase;
                input.StockInfo.BatchNo = batchNO;
                return input;

            }
            return null;
        }

        public FS.FrameWork.Models.NeuObject GetPatientInfo(string patientNO, ref string msg)
        {
            if (patientNO.Length < 10)
            {
                patientNO = patientNO.PadLeft(10, '0');
            }

            DataSet dataSet = new DataSet();
            constant. ExecQuery(string.Format(sql,patientNO)  , ref  dataSet);
            if (dataSet.Tables.Count > 0)
            {
                FS.FrameWork.Models.NeuObject patient = new FS.FrameWork.Models.NeuObject();
                patient.ID = dataSet.Tables[0].Rows[0][0].ToString();
                patient.Memo = dataSet.Tables[0].Rows[0][1].ToString();
                return patient;

            }
            return null;

        }

        public FS.HISFC.BizLogic.Material.Object.StockHead GetBarCodeInfoOutput(string barCode, string storageCode, ref string msg)
        {
            if (barCode.Length > 6)
            {
                string customCode = barCode.Substring(0, 6);
                string batchNO = barCode.Substring(6);

                FS.HISFC.BizLogic.Material.Object.MatBase matBase = baseProcess.GetBaseInfoByCustomCode(customCode);
                if (customCode == null)
                {
                    msg = "不存在此物品,自定义码为" + customCode;
                    return null;
                }


                List<StockHead> stockList = stockProcess.QueryStockDetailForVirtual(matBase.ID, storageCode, true, false, true, FS.HISFC.BizLogic.Material.BizLogic.EnumStockCollectType.按供货公司和入库价汇总, batchNO);

                if (stockList.Count > 0)
                {
                    return stockList[0];
                }

            }
            return null;

        #endregion


        }
    }
}
