using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    /// <summary>
    /// 基本数据缓存
    /// </summary>
    public class BaseCache
    {
        private string settingFile = FS.FrameWork.WinForms.Classes.Function.CurrentPath + "\\Profile\\PharmacyItemExendControlSetting.xml";
        /*
            * ArrayList alDrugType, 
            * ArrayList alMinFee, 
            * ArrayList alQuality, 
            * ArrayList alDoseForm, 
            * ArrayList alPactUnit, 
            * ArrayList alMinUnit, 
            * ArrayList alDoseUnit, 
            * ArrayList alPriceForm, 
            * ArrayList alUage, 
            * ArrayList alFrequence, 
            * ArrayList alStoreCondition, 
            * ArrayList alGrade, 
            * ArrayList alCompany, 
            * ArrayList alProducer, 
            * ArrayList alFunction1, 
            * ArrayList alFunction2, 
            * ArrayList alFunction3
         */

        public FS.FrameWork.Public.ObjectHelper drugTypeHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper minFeeHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper drugQualityHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper doseFormHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper pactUnitHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper bigPackUnitHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper minUnitHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper doseUnitHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper priceFormHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper usageHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper frequencyHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper storeConditionHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper gradeHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper companyHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper producerHelper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper function1Helper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper function2Helper = new FS.FrameWork.Public.ObjectHelper();
        public FS.FrameWork.Public.ObjectHelper function3Helper = new FS.FrameWork.Public.ObjectHelper();
        public System.Collections.Hashtable hsFormula = new System.Collections.Hashtable();
        public System.Collections.Hashtable hsExtendInfo = new System.Collections.Hashtable();

        public int Init()
        {
            try
            {
                FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

                this.drugTypeHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
                this.minFeeHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
                this.drugQualityHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
                this.doseFormHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM);
                this.pactUnitHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.PACKUNIT);
                this.minUnitHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.MINUNIT);
                this.doseUnitHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.DOSEUNIT);
                this.priceFormHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.PRICEFORM);
                this.usageHelper.ArrayObject = constantMgr.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);
                this.bigPackUnitHelper.ArrayObject = constantMgr.GetList("BIGPACKUNIT");

                FS.HISFC.BizLogic.Manager.Frequency frequencyMgr = new FS.HISFC.BizLogic.Manager.Frequency();
                this.frequencyHelper.ArrayObject = frequencyMgr.GetList("ROOT");

                FS.SOC.HISFC.BizLogic.Pharmacy.Constant phaConstantMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Constant();

                this.storeConditionHelper.ArrayObject = constantMgr.GetList("STORECONDITION");
                this.gradeHelper.ArrayObject = constantMgr.GetList("DRUGGRADE");
                this.companyHelper.ArrayObject = phaConstantMgr.QueryCompany("1");
                this.producerHelper.ArrayObject = phaConstantMgr.QueryCompany("0");

                this.function1Helper.ArrayObject = new System.Collections.ArrayList(phaConstantMgr.QueryPhaFunctionByLevel(1));
                this.function2Helper.ArrayObject = new System.Collections.ArrayList(phaConstantMgr.QueryPhaFunctionByLevel(2));
                this.function3Helper.ArrayObject = new System.Collections.ArrayList(phaConstantMgr.QueryPhaFunctionByLevel(3));


                FS.SOC.HISFC.BizLogic.Pharmacy.Adjust adjustMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.Adjust();

                System.Collections.ArrayList alFormula = adjustMgr.QueryAdjustPriceFormula();
                if (alFormula != null && alFormula.Count > 0)
                {
                    for (int i = 0; i < alFormula.Count; i++)
                    {
                        FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula adjust = alFormula[i] as FS.SOC.HISFC.Models.Pharmacy.Adjust.RetailPriceFormula;
                        if (hsFormula.Contains(adjust.DrugType.ID))
                        {
                            System.Collections.ArrayList allDataTemp = hsFormula[adjust.DrugType.ID] as System.Collections.ArrayList;
                            allDataTemp.Add(adjust);
                        }
                        else
                        {
                            System.Collections.ArrayList allDataTemp = new System.Collections.ArrayList();
                            allDataTemp.Add(adjust);
                            hsFormula.Add(adjust.DrugType.ID, allDataTemp);
                        }
                    }
                }

                if (System.IO.File.Exists(this.settingFile))
                {
                    System.Xml.XmlNode constantNode = SOC.Public.XML.File.GetNode(this.settingFile, "Constant");
                    if (constantNode != null)
                    {
                        foreach (System.Xml.XmlNode node in constantNode.ChildNodes)
                        {
                            if (node.Attributes.Count > 0)
                            {
                                System.Collections.ArrayList al = constantMgr.GetList(node.Attributes[0].InnerText);
                                if (al != null)
                                {
                                    hsExtendInfo.Add(node.Name, al);
                                }
                            }

                        }
                    }

                    constantNode = SOC.Public.XML.File.GetNode(this.settingFile, "Items");
                    if (constantNode != null)
                    {
                        foreach (System.Xml.XmlNode node in constantNode.ChildNodes)
                        {
                            if (node.Attributes.Count > 0)
                            {
                                int index = 0;
                                string items = SOC.Public.XML.SettingFile.ReadSetting(this.settingFile, "Items", node.Name, "否,是");
                                System.Collections.ArrayList alState = new System.Collections.ArrayList();
                                foreach (string item in items.Split(',', ' ', '|'))
                                {
                                    FS.FrameWork.Models.NeuObject state = new FS.FrameWork.Models.NeuObject();
                                    state.ID = index.ToString();
                                    state.Name = item;
                                    alState.Add(state);
                                    index++;
                                }
                                hsExtendInfo.Add(node.Name, alState);
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Function.ShowMessage("初始化药品基本信息需要的基础数据发生错误：" + ex.Message, System.Windows.Forms.MessageBoxIcon.Error);
                return -1;
            }

            return 1;
        }

        /// <summary>
        /// 获取扩展字段名称
        /// </summary>
        /// <param name="fieldCode">字段名称</param>
        /// <param name="valueCode">值编码</param>
        /// <param name="defaultName">值名称</param>
        /// <returns></returns>
        public string GetExtendFieldName(string fieldCode, string valueCode, string defaultName)
        {
            if (hsExtendInfo.Contains(fieldCode))
            {
                System.Collections.ArrayList al = hsExtendInfo[fieldCode] as System.Collections.ArrayList;
                foreach (FS.FrameWork.Models.NeuObject o in al)
                {
                    if (o.ID == valueCode)
                    {
                        return o.Name;
                    }
                    
                }
            }
            return defaultName;
        }
    }
}
