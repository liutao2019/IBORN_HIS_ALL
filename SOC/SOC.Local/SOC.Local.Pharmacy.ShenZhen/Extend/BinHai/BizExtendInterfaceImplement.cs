using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.ShenZhen.Extend.BinHai
{
    public class BizExtendInterfaceImplement: FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IPharmacyBizExtend
    {
        ArrayList alPlan = new ArrayList();

        #region IPharmacyBizExtend 成员 其它

        public int AfterSave(string class2Code, string class3code, System.Collections.ArrayList alData, ref string errInfo)
        {
            if (alData != null)
            {
                string sqlStr = string.Empty;
                for (int i = 0; i < alData.Count; i++)
                {
                    //1.入库同步更新药品基本信息生产厂家
                    if (alData[i].GetType() == typeof(FS.HISFC.Models.Pharmacy.Input))
                    {
                        FS.HISFC.Models.Pharmacy.Input input = alData[i] as FS.HISFC.Models.Pharmacy.Input;
                        sqlStr = "UPDATE PHA_COM_BASEINFO SET PRODUCER_CODE='" + input.Producer.ID.ToString() + "' WHERE DRUG_CODE='" + input.Item.ID.ToString() + "'";
                        FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        dbMgr.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);                        
                        if (dbMgr.ExecNoQuery(sqlStr) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            errInfo = dbMgr.Err;
                            return -1;
                        }

                        FS.FrameWork.Management.PublicTrans.Commit();
                    }
                }
            }

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
        #endregion

        #region IPharmacyBizExtend 成员 内部入库申请
        public System.Collections.ArrayList SetInnerInputApply(string applyDeptNO, string stockDeptNO, System.Collections.ArrayList alData)
        {
            return null;
        }
        #endregion

        #region IPharmacyBizExtend 成员 入库计划
        public System.Collections.ArrayList SetInputPlan(string stockDeptNO, System.Collections.ArrayList alData)
        {
            return null;
        }
        
        #endregion

        #region IPharmacyBizExtend 成员 入出库单号


        public string GetBillNO(string stockDeptNO, string class2Code, string class3code, ref string errInfo)
        {
            string billNO = "default";

            return billNO;
        }

        #endregion

        #region IPharmacyBizExtend 成员 入库录入信息


        public FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl GetInputInfoControl(string class3code, bool isSpecial, FS.SOC.HISFC.BizProcess.PharmacyInterface.Pharmacy.IInputInfoControl defaultInputInfoControl)
        {
            if (isSpecial)
            {
                return defaultInputInfoControl;
            }
            else
            {
                return new ucCommonInput();
            }
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
