using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SOC.Local.Pharmacy.ZhuHai.Common.ZDWY.Storage
{
    public class ICostShowImplement:FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ICostShow
    {
        #region ICostShow 成员

        /// <summary>
        /// 生成合计数显示方式
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public string GenerateCostShow(string[] args)
        {
            string iCount = args[0];
            string purCost = args[1];
            string wholeSaleCost = args[2];
            string retailCost = args[3];
            return "当前品种数：" + iCount
                    + "，零售金额：" + purCost
                    + "，加成金额：" + wholeSaleCost
                    + "，零售金额：" + retailCost;
        }

        #endregion
    }
}
