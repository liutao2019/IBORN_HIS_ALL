using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace GJLocal.HISFC.Components.OpGuide.InDrugStore.InPatient
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
    public class DrugBillInterfaceImplement : FS.SOC.HISFC.BizProcess.PharmacyInterface.DrugStore.IDrugBillClass,FS.HISFC.BizProcess.Interface.Pharmacy.IDrugBillClassP
    {
        Hashtable hsDrugBillClass = new Hashtable();

        FS.FrameWork.Public.ObjectHelper InjectObjHelper = new FS.FrameWork.Public.ObjectHelper();

        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();

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

            #region 特殊处理部分针剂药品
            if (InjectObjHelper.ArrayObject.Count == 0)
            {
                ArrayList allInjectDrug = new ArrayList();
                allInjectDrug = conMgr.GetAllList("SPECIALINJECTITEM");
                if (allInjectDrug != null && allInjectDrug.Count > 0)
                {
                    InjectObjHelper.ArrayObject = allInjectDrug;
                }
            }

            if (InjectObjHelper != null && InjectObjHelper.ArrayObject.Count > 0 && InjectObjHelper.GetObjectFromID(applyOut.Item.ID) != null)
            {
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
            }
            #endregion

            //草药医嘱，发送到草药摆药单
            if (FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSysClass(applyOut.Item.ID) == "PCC")
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

            //临购药品，发送到临购摆药单
            if (FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSpecial(applyOut.Item.ID) == "3" || FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItemSpecial(applyOut.Item.ID) == "6")
            {
                billClass.ID = "L";
                if (hsDrugBillClass.Contains(billClass.ID))
                {
                    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                }
                if (billClass != null && billClass.IsValid)
                {
                    return billClass;
                }
            }

            //出院带药、请假带药的麻精类药品发送到麻精一摆药单
            if ((applyOut.OrderType.ID == "CD" || applyOut.OrderType.ID == "QL") && (FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "S" || FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "P"))
            {
                billClass.ID = "OP1";
                if (hsDrugBillClass.Contains(billClass.ID))
                {
                    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                }
                if (billClass != null && billClass.IsValid)
                {
                    return billClass;
                }
            }
            //出院带药、请假带药的其他类药品发送到出院带药摆药单
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

            if (FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "S" || FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "P")
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
            if (FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "V")
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
            if (FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "T")
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
            ////口服药发送到明细摆药单、
            //if (SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(applyOut.Usage.ID))
            //{
            //    if (applyOut.OrderType.ID == "CZ")
            //    {
            //        billClass.ID = "DC";
            //    }
            //    if (hsDrugBillClass.Contains(billClass.ID))
            //    {
            //        billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
            //    }
            //    if (billClass != null && billClass.IsValid)
            //    {
            //        return billClass;
            //    }
            //}

            //口服临时医嘱摆药单
            if (FS.SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(applyOut.Usage.ID))
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

            ////针剂长期医嘱摆药单
            //if (applyOut.OrderType.ID == "CZ")
            //{
            //    billClass.ID = "TC";
            //}

            //if (hsDrugBillClass.Contains(billClass.ID))
            //{
            //    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
            //}
            //if (billClass != null && billClass.IsValid)
            //{
            //    return billClass;
            //}

            if (!FS.SOC.HISFC.BizProcess.Cache.Common.IsPOUsage(applyOut.Usage.ID))
            {
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

            FS.HISFC.Models.Pharmacy.DrugBillClass pDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            pDrugBill.ID = "P";
            pDrugBill.Name = "非医嘱摆药单";                       //摆药分类名称
            pDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
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

            FS.HISFC.Models.Pharmacy.DrugBillClass lDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            lDrugBill.ID = "L";
            lDrugBill.Name = "临购或不常用药品摆药单";                       //摆药分类名称
            lDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            lDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            lDrugBill.IsValid = true;              //是否有效
            lDrugBill.Memo = "系统默认摆药单";               //备注
            al.Add(lDrugBill);


            FS.HISFC.Models.Pharmacy.DrugBillClass op1DrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            op1DrugBill.ID = "OP1";
            op1DrugBill.Name = "出院带药(麻精一)摆药单";                       //摆药分类名称
            op1DrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            op1DrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            op1DrugBill.IsValid = true;              //是否有效
            op1DrugBill.Memo = "系统默认摆药单";               //备注
            al.Add(op1DrugBill);


            FS.HISFC.Models.Pharmacy.DrugBillClass oDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            oDrugBill.ID = "O";
            oDrugBill.Name = "出院带药摆药单(普药)";                       //摆药分类名称
            oDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            oDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            oDrugBill.IsValid = true;              //是否有效
            oDrugBill.Memo = "系统默认摆药单";               //备注
            al.Add(oDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass gDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            gDrugBill.ID = "T";
            gDrugBill.Name = "大输液摆药单";                       //摆药分类名称
            gDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            gDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            gDrugBill.IsValid = true;              //是否有效
            gDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(gDrugBill);

            //FS.HISFC.Models.Pharmacy.DrugBillClass dcDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            //dcDrugBill.ID = "DC";
            //dcDrugBill.Name = "西药长期医嘱口服摆药单";                       //摆药分类名称
            //dcDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            //dcDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            //dcDrugBill.IsValid = true;              //是否有效
            //dcDrugBill.Memo = "系统默认的摆药单";               //备注
            //al.Add(dcDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass dlDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            dlDrugBill.ID = "DL";
            dlDrugBill.Name = "口服摆药单";                       //摆药分类名称
            dlDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            dlDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            dlDrugBill.IsValid = true;              //是否有效
            dlDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(dlDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass cpDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            cpDrugBill.ID = "CP";// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
            cpDrugBill.Name = "长嘱口服摆药单";                       //摆药分类名称
            cpDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
            cpDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            cpDrugBill.IsValid = true;              //是否有效
            cpDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(cpDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass lpDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            lpDrugBill.ID = "LP";// {F9B890A9-D02C-4e38-BB39-F64251AF8F64}
            lpDrugBill.Name = "临嘱口服摆药单";                       //摆药分类名称
            lpDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            lpDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            lpDrugBill.IsValid = true;              //是否有效
            lpDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(lpDrugBill);

            //FS.HISFC.Models.Pharmacy.DrugBillClass tcDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            //tcDrugBill.ID = "TC";
            //tcDrugBill.Name = "西药长期医嘱针剂摆药单";                       //摆药分类名称
            //tcDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            //tcDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            //tcDrugBill.IsValid = true;              //是否有效
            //tcDrugBill.Memo = "系统默认的摆药单";               //备注
            //al.Add(tcDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass tlDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            tlDrugBill.ID = "TL";
            tlDrugBill.Name = "针剂摆药单";                       //摆药分类名称
            tlDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            tlDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            tlDrugBill.IsValid = true;              //是否有效
            tlDrugBill.Memo = "系统默认的摆药单";               //备注
            al.Add(tlDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass aDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            aDrugBill.ID = "A";
            aDrugBill.Name = "综合摆药单";                       //摆药分类名称
            aDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            aDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            aDrugBill.IsValid = true;              //是否有效
            aDrugBill.Memo = "系统必须包含的摆药单，不能更改、删除";               //备注
            al.Add(aDrugBill);

            FS.HISFC.Models.Pharmacy.DrugBillClass rDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            rDrugBill.ID = "R";
            rDrugBill.Name = "退药单";                       //摆药分类名称
            rDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.T;           //打印类型
            rDrugBill.DrugAttribute.ID = FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute.T; //摆药类型
            rDrugBill.IsValid = true;              //是否有效
            rDrugBill.Memo = "系统必须包含的摆药单，不能更改、删除";               //备注
            al.Add(rDrugBill);


            FS.HISFC.Models.Pharmacy.DrugBillClass sDrugBill = new FS.HISFC.Models.Pharmacy.DrugBillClass();
            sDrugBill.ID = "S";
            sDrugBill.Name = "麻精一摆药单";                       //摆药分类名称
            sDrugBill.PrintType.ID = FS.HISFC.Models.Pharmacy.BillPrintType.enuBillPrintType.D;           //打印类型
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

        #region IDrugBillClassP 非医嘱摆药单的分单规则

        /// <summary>
        /// 医技收费的非医嘱摆药单的分单规则
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Pharmacy.DrugMessage GetDrugMessage(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
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

            FS.HISFC.Models.Pharmacy.DrugBillClass billClass = new FS.HISFC.Models.Pharmacy.DrugBillClass();

            FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();

            //毒麻药品发送到毒麻药单
            if (FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "S" || FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "P")
            {
                billClass.ID = "S";
                if (hsDrugBillClass.Contains(billClass.ID))
                {
                    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                }
                if (billClass != null && billClass.IsValid)
                {
                    drugMessage = this.GetDrugMessageP(billClass,applyOut);
                    return drugMessage;
                }
            }
            //大输液
            if (FS.SOC.HISFC.BizProcess.Cache.Common.GetDrugQualitySystem(applyOut.Item.Quality.ID) == "T")
            {
                billClass.ID = "T";
                if (hsDrugBillClass.Contains(billClass.ID))
                {
                    billClass = hsDrugBillClass[billClass.ID] as FS.HISFC.Models.Pharmacy.DrugBillClass;
                }
                if (billClass != null && billClass.IsValid)
                {
                    drugMessage = this.GetDrugMessageP(billClass, applyOut);
                    return drugMessage;
                }
            }

            drugMessage.ApplyDept = applyOut.ApplyDept;      //科室或者病区
            drugMessage.DrugBillClass.ID = "P";             //摆药单分类编码：非医嘱摆药单 P
            drugMessage.DrugBillClass.Name = "非医嘱摆药单";//摆药单分类名称：非医嘱摆药单
            drugMessage.SendType = 1;                       //发送类型0全部,1-集中,2-临时
            drugMessage.SendFlag = 0;                       //状态0-通知,1-已摆
            drugMessage.StockDept = applyOut.StockDept;     //发药科室

            return drugMessage;
        }

        private FS.HISFC.Models.Pharmacy.DrugMessage GetDrugMessageP(FS.HISFC.Models.Pharmacy.DrugBillClass billClass, FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
            drugMessage.DrugBillClass = billClass;
            drugMessage.ApplyDept = applyOut.ApplyDept;      //科室或者病区
            drugMessage.SendType = 1;                       //发送类型0全部,1-集中,2-临时
            drugMessage.SendFlag = 0;                       //状态0-通知,1-已摆
            drugMessage.StockDept = applyOut.StockDept;     //发药科室
            return drugMessage;
        }

        #endregion
    }
}
