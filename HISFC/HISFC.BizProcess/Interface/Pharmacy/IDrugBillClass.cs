﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.HISFC.BizProcess.Interface.Pharmacy
{
    public interface IDrugBillClass
    {
        /// <summary>
        /// 发药申请发送时获取对应的摆药单
        /// </summary>
        /// <returns></returns>
        FS.HISFC.Models.Pharmacy.DrugBillClass GetDrugBillClass(FS.HISFC.Models.Pharmacy.ApplyOut applyOut);
    }
}
