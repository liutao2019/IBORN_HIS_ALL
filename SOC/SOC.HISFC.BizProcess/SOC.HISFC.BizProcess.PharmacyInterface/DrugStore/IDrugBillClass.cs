using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore
{
    /// <summary>
    /// [功能描述: 住院药房摆药单类型标接口]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-03]<br></br>
    /// </summary>
    public interface IDrugBillClass : FS.HISFC.BizProcess.Interface.Pharmacy.IDrugBillClass
    {
        /// <summary>
        /// 获取摆药单分类列表
        /// </summary>
        /// <returns></returns>
        List<FS.HISFC.Models.Pharmacy.DrugBillClass> GetList();       
    }
}
