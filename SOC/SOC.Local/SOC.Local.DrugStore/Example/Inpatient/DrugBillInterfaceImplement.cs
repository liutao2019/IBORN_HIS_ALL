using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.Example.Inpatient
{
    /// <summary>
    /// [功能描述: 住院药房摆药单本地化实例]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2011-1]<br></br>
    /// 说明：
    /// 1、主要是实现接口函数逻辑
    /// 2、SaveDefaultBill函数是本地需要的单据，其中退药单R、非医嘱摆药单P是系统硬编码的，任何项目都必须包含
    /// 3、不可以MessageBox
    /// </summary>>
    public class DrugBillInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IDrugBillClass
    {
        Hashtable hsDrugBillClass = new Hashtable();

        #region IDrubBillClass 成员

        public FS.HISFC.Models.Pharmacy.DrugBillClass GetDrugBillClass(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            FS.HISFC.Models.Pharmacy.DrugBillClass billClass = new FS.HISFC.Models.Pharmacy.DrugBillClass();

            if (hsDrugBillClass.Count == 0)
            {
                ArrayList drugBillClassList = new ArrayList();
                FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
                drugBillClassList = drugStoreMgr.QueryDrugBillClassList();
                if (drugBillClassList == null)
                {
                    return null;
                }
                foreach (FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass in drugBillClassList)
                {
                    if (!hsDrugBillClass.Contains(drugBillClass.ID))
                    {
                        hsDrugBillClass.Add(drugBillClass.ID, drugBillClass);
                    }
                }
            }


            //草药医嘱，发送到草药摆药单
            if (SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
            {
                billClass.ID = "C";
                if (hsDrugBillClass.Contains(billClass.ID))
                {
                    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                }
                if (billClass != null && billClass.IsValid)
                {
                    return billClass;
                }
            }
            //出院带药、请假带药发送到出院带药摆药单
            if (applyOut.OrderType.ID == "CD" || applyOut.OrderType.ID == "QL")
            {
                billClass.ID = "O";
                if (hsDrugBillClass.Contains(billClass.ID))
                {
                    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                }
                if (billClass != null && billClass.IsValid)
                {
                    return billClass;
                }
            }
            //根据药品性质，把毒麻药、精一类、精二类 单独发送到毒麻药摆药单
            if (SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "S" || SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "P")
            {
                billClass.ID = "S";
                if (hsDrugBillClass.Contains(billClass.ID))
                {
                    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                }
                if (billClass != null && billClass.IsValid)
                {
                    return billClass;
                }
            }
            //贵重药发送到贵重药摆药单
            if (SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "V")
            {
                billClass.ID = "V";
                if (hsDrugBillClass.Contains(billClass.ID))
                {
                    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                }
                if (billClass != null && billClass.IsValid)
                {
                    return billClass;
                }
            }
            //大输液发送到大输液摆药单
            if (SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "T")
            {
                billClass.ID = "T";
                if (hsDrugBillClass.Contains(billClass.ID))
                {
                    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                }
                if (billClass != null && billClass.IsValid)
                {
                    return billClass;
                }
            }
            //口服药发送到明细摆药单、
            if (SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(applyOut.Usage.ID))
            {
                if (applyOut.OrderType.ID == "CZ")
                {
                    billClass.ID = "DC";
                }
                if (hsDrugBillClass.Contains(billClass.ID))
                {
                    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                }
                if (billClass != null && billClass.IsValid)
                {
                    return billClass;
                }
            }

            //口服临时医嘱摆药单
            if (SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(applyOut.Usage.ID))
            {
                billClass.ID = "DL";
                if (hsDrugBillClass.Contains(billClass.ID))
                {
                    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                }
                if (billClass != null && billClass.IsValid)
                {
                    return billClass;
                }
            }

            //针剂长期医嘱摆药单
            if (applyOut.OrderType.ID == "CZ")
            {
                billClass.ID = "TC";
            }

            if (hsDrugBillClass.Contains(billClass.ID))
            {
                billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
            }
            if (billClass != null && billClass.IsValid)
            {
                return billClass;
            }

            //针剂临时医嘱摆药单
            billClass.ID = "TL";

            if (hsDrugBillClass.Contains(billClass.ID))
            {
                billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
            }
            if (billClass != null && billClass.IsValid)
            {
                return billClass;
            }

            //最终保留综合摆药单
            billClass.ID = "A";
            billClass.Name = "综合摆药单";

            return billClass;
        }

        public List<FS.HISFC.Models.Pharmacy.DrugBillClass> GetList()
        {
            ArrayList drugBillClassList = new ArrayList();
            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreMgr = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();
            drugBillClassList = drugStoreMgr.QueryDrugBillClassList();
            if (drugBillClassList == null)
            {
                return null;
            }

            if (drugBillClassList.Count == 0)
            {
                drugBillClassList = this.SaveDefaultBill();
            }
            List<FS.HISFC.Models.Pharmacy.DrugBillClass> list = new List<FS.HISFC.Models.Pharmacy.DrugBillClass>();
            foreach (FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass in drugBillClassList)
            {
                list.Add(drugBillClass);
            }
            return list;
        }

        #endregion

        #region 保存默认的摆药单
        /// <summary>
        /// 默认摆药单保存
        /// </summary>
        private ArrayList SaveDefaultBill()
        {
            ArrayList al = new ArrayList();

            FS.HISFC.Models.Pharmacy.DrugBillClass rDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            rDrugBill.ID = "R";
            rDrugBill.Name = "退药单";                       //摆药分类名称
            rDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            rDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            rDrugBill.IsValid = true;              //是否有效
            rDrugBill.Memo = "系统必须包含的摆药单，不能更改、删除";               //备注
            al.Add(rDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass pDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            pDrugBill.ID = "P";
            pDrugBill.Name = "非医嘱摆药单";                       //摆药分类名称
            pDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            pDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            pDrugBill.IsValid = true;              //是否有效
            pDrugBill.Memo = "系统必须包含的摆药单，不能更改、删除";               //备注
            al.Add(pDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass cDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            cDrugBill.ID = "C";
            cDrugBill.Name = "草药摆药单";                       //摆药分类名称
            cDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            cDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            cDrugBill.IsValid = true;              //是否有效
            cDrugBill.Memo = "系统默认摆药单";               //备注
            al.Add(cDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass oDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            oDrugBill.ID = "O";
            oDrugBill.Name = "出院带药摆药单";                       //摆药分类名称
            oDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            oDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            oDrugBill.IsValid = true;              //是否有效
            oDrugBill.Memo = "系统默认摆药单";               //备注
            al.Add(oDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass aDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            aDrugBill.ID = "A";
            aDrugBill.Name = "综合摆药单";                       //摆药分类名称
            aDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            aDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            aDrugBill.IsValid = true;              //是否有效
            aDrugBill.Memo = "系统必须包含的摆药单，不能更改、删除";               //备注
            al.Add(aDrugBill);


            FS.HISFC.Models.Pharmacy.DrugBillClass dcDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            dcDrugBill.ID = "DC";
            dcDrugBill.Name = "西药长期医嘱口服摆药单";                       //摆药分类名称
            dcDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            dcDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            dcDrugBill.IsValid = true;              //是否有效
            dcDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(dcDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass dlDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            dlDrugBill.ID = "DL";
            dlDrugBill.Name = "西药临时医嘱口服摆药单";                       //摆药分类名称
            dlDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            dlDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            dlDrugBill.IsValid = true;              //是否有效
            dlDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(dlDrugBill);


            FS.HISFC.Models.Pharmacy.DrugBillClass tcDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            tcDrugBill.ID = "TC";
            tcDrugBill.Name = "西药长期医嘱针剂摆药单";                       //摆药分类名称
            tcDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            tcDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            tcDrugBill.IsValid = true;              //是否有效
            tcDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(tcDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass tlDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            tlDrugBill.ID = "TL";
            tlDrugBill.Name = "西药临时医嘱针剂摆药单";                       //摆药分类名称
            tlDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            tlDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            tlDrugBill.IsValid = true;              //是否有效
            tlDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(tlDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass gDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            gDrugBill.ID = "G";
            gDrugBill.Name = "大输液摆药单";                       //摆药分类名称
            gDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            gDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            gDrugBill.IsValid = true;              //是否有效
            gDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(gDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass sDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            sDrugBill.ID = "S";
            sDrugBill.Name = "毒麻精摆药单";                       //摆药分类名称
            sDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            sDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            sDrugBill.IsValid = true;              //是否有效
            sDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(sDrugBill);

            FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.SOC.HISFC.BizLogic.Pharmacy.DrugStore();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            foreach (FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass in al)
            {
                if (drugStoreManager.InsertOneDrugBillClass(drugBillClass) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    return null;
                }
            }
            FS.FrameWork.Management.PublicTrans.Commit();

            return al;
        }
        #endregion

    }
}
