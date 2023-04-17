using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterfaceInstanceDefault.IValidPactItemChoose
{
    public class IValidPactItemChooseDefault : FS.HISFC.BizProcess.Interface.FeeInterface.IValidPactItemChoose
    {

        #region 变量
        FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        #endregion

        #region IValidPactItemChoose 成员

        public bool ValidPactItemChoose(string pactCode, FS.HISFC.Models.Base.Item baseItem, ref string errText)
        {
            //谁这么得儿啊，居然这样改啊！！屏蔽了
            return true;
            //不是合作医疗目录中的药品，不进行维护
            if (pactCode == "6" || pactCode == "7")
            {
                if (baseItem.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    errText = "非药品不能维护优惠比例！";
                    return false;
                }
                FS.HISFC.Models.Pharmacy.Item item= phaIntegrate.GetItem(baseItem.ID);
                if (item == null)
                {
                    errText = "查询药品信息失败！" + phaIntegrate.Err;
                    return false;
                }
                if (item.SpecialFlag != "1")
                {
                    errText = "不是合作医疗目录药品，不能维护优惠比例！";
                    return false;
                }
            }
            return true;
        }

        #endregion
    }
}
