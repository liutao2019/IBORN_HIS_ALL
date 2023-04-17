using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Fee.BizProcess
{
    public class Pharmacy
    {
        FS.HISFC.BizLogic.Pharmacy.Item itemMgr = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 获取所有的药品信息（简单）
        /// </summary>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryPharmacyBriefInfo()
        {
            return itemMgr.QueryItemList(true);
        }
    }
}
