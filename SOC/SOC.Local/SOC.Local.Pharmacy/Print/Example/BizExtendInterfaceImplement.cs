using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.Pharmacy.Print.Example
{
    public class BizExtendInterfaceImplement:SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend
    {
        #region IPharmacyBizExtend 成员

        public int AfterSave(string class2Code, string class3code, System.Collections.ArrayList alData, ref string errInfo)
        {
            return 1;
        }

        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting GetChooseDataSetting(string class2Code, string class3MeaningCode, string class3Code, string listType, ref string errInfo)
        {
            //使用核心默认的
            FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting chooseDataSetting = new FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.ChooseDataSetting();
            chooseDataSetting.IsDefault = true;
            return chooseDataSetting;
        }

        public uint GetCostDecimals(string class2Code, string class3MeaningCode, string type)
        {
            return 2;
        }

        public System.Collections.ArrayList SetCheckDetail(string stockDeptNO)
        {
            //返回null使用核心默认的
            return null;
        }

        public System.Collections.ArrayList SetInnerInputApply(string applyDeptNO, string stockDeptNO, System.Collections.ArrayList alData)
        {
            //返回null使用核心默认的
            return null;
        }

        public System.Collections.ArrayList SetInputPlan(string stockDeptNO, System.Collections.ArrayList alData)
        {
            //返回null使用核心默认的
            return null;
        }

        #endregion

        #region IPharmacyBizExtend 成员


        public string GetBillNO(string stockDeptNO, string class2Code, string class3code, ref string errInfo)
        {
            string billNO = "";
            FS.HISFC.BizProcess.Integrate.Pharmacy phaIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

            billNO = phaIntegrate.GetInOutListNO(stockDeptNO, (class2Code == "0310"));
            if (billNO == null)
            {
                errInfo = "获取最新入库单号出错" + phaIntegrate.Err;
                return "-1";
            }
            return billNO;
        }


        #endregion

        #region IPharmacyBizExtend 成员


        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl GetInputInfoControl(string class3code, bool isSpecial)
        {
            return null;
        }

        #endregion

        #region IPharmacyBizExtend 成员 入库录入信息


        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl GetInputInfoControl(string class3code, bool isSpecial, FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl defaultInputInfoControl)
        {

            return defaultInputInfoControl;

        }

        #endregion

        #region IPharmacyBizExtend 成员 药品基本信息扩展

        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IItemExtendControl GetItemExtendControl(FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IItemExtendControl defaultItemExtendControl)
        {
            return defaultItemExtendControl;
        }

        #endregion
    }
}
