﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Base;
using Neusoft.HISFC.BizLogic.Fee;
using Neusoft.HISFC.Models.Fee.Item;
using Neusoft.HISFC.Models.Order.OutPatient;
using Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.Common;
using Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder;
using Neusoft.FrameWork.WinForms.Classes;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM
{
    /// <summary>
    /// 医嘱保存后工厂类
    /// </summary>
    class SaveRecipeFactory : Neusoft.HISFC.BizProcess.Interface.Order.IOutPatientPrint 
    {
        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = string.Empty;

        #region 业务层
        /// <summary>
        /// 医嘱业务层
        /// </summary>
        protected Neusoft.HISFC.BizLogic.Order.OutPatient.Order OrderManagement = new Neusoft.HISFC.BizLogic.Order.OutPatient.Order();

        /// <summary>
        /// 费用业务类
        /// </summary>
        Neusoft.HISFC.BizLogic.Fee.Outpatient outPatientManager = new Neusoft.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// 功能业务类
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Manager managerIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        #endregion

        #region 接口

        /// <summary>
        /// 处方打印接口
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.IRecipePrint iRecipePrint = null;

        /// <summary>
        /// 检查打印接口
        /// </summary>
        Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint iPacsPrint = null;

         /// <summary>
        /// 检验打印接口
        /// </summary>
        Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint iLisPrint = null;

        /// <summary>
        /// 实现门诊院注单打印接口
        /// </summary>
        private Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCInjectBillPrint = null;

        /// <summary>
        /// 实现门诊治疗单打印接口
        /// </summary>
        private Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCTreatmentBillPrint = null;

        /// <summary>
        /// 实现门诊指引单打印接口
        /// </summary>
        private Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCOutPatientGuidePrint = null;

        /// <summary>
        /// 实现门诊指引单打印接口
        /// </summary>
        private Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCOutPatientClinicsPrint = null;

        //private Hashtable hsCombID = new Hashtable();

        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderDictionary"></param>
        /// <param name="order"></param>
        private void SetOrderList(Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> orderDictionary, Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            if (orderDictionary.ContainsKey(order.ReciptNO))
            {
                orderDictionary[order.ReciptNO].Add(order);
            }
            else
            {
                List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                orderList.Add(order);
                orderDictionary.Add(order.ReciptNO, orderList);
            }
        }


        #endregion

        #region ISaveOrder 成员
        public string ErrInfo
        {
            get
            {
                return this.errInfo;
            }
            set
            {
                this.errInfo = value;
            }
        }

        /// <summary>
        /// 获取总药费、总注射费
        /// </summary>
        private string getTotalMoney(Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> drugDictionary, Neusoft.HISFC.Models.Registration.Register regObj)
        {
            decimal phaMoney = 0m;//药费
            decimal injectMoney = 0m;//注射费

            ArrayList alFee;

            string label = string.Empty;

            Hashtable hsCombID = new Hashtable();

            Hashtable hsRecipeSeq = new Hashtable();

            alFee = new ArrayList();
            foreach (string drug in drugDictionary.Keys)
            {
                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in drugDictionary[drug])
                {
                    if (!hsRecipeSeq.Contains(order.ReciptSequence))
                    {
                        alFee.AddRange(this.outPatientManager.QueryFeeDetailByClinicCodeAndRecipeSeq(regObj.ID, order.ReciptSequence, "ALL"));
                        hsRecipeSeq.Add(order.ReciptSequence, null);
                    }
                }
            }

            foreach (string drug in drugDictionary.Keys)
            {
                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in drugDictionary[drug])
                {
                    if (!hsCombID.Contains(order.Combo.ID))
                    {
                        if (alFee != null && alFee.Count >= 1)
                        {
                            foreach (Neusoft.HISFC.Models.Fee.Outpatient.FeeItemList itemlist in alFee)
                            {
                                if (itemlist.Order.Combo.ID == order.Combo.ID)
                                {
                                    if (!itemlist.Item.IsMaterial)
                                    {
                                        itemlist.FT.TotCost = itemlist.FT.OwnCost + itemlist.FT.PayCost + itemlist.FT.PubCost;
                                        phaMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//药品金额
                                    }
                                    else
                                    {
                                        injectMoney += itemlist.FT.PubCost + itemlist.FT.PayCost + itemlist.FT.OwnCost;//注射费
                                    }
                                }
                            }
                        }
                        hsCombID.Add(order.Combo.ID, null);
                    }
                }
            }

            label = "(本次诊疗 药费:￥" + phaMoney.ToString("F2") + " 注射费:￥" + injectMoney.ToString("F2") + " 应付总计:￥" + (phaMoney + injectMoney).ToString("F2") + ")";
            return label;

        }


        /// <summary>
        /// 判断医嘱组合号是否已经存在
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        //private bool GetOrderByCombId(Neusoft.HISFC.Models.Order.OutPatient.Order order)
        //{
        //    if (!hsCombID.Contains(order.ReciptSequence))
        //    {
        //        hsCombID.Add(order.ReciptSequence, order);
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}


        #region 普通处方
        private void CommonDrug(Neusoft.HISFC.Models.Registration.Register regObj, Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> drugDictionary, System.Collections.ArrayList alOrder, List<Control> ControlList, bool isPreview)
        {

            if (drugDictionary.Keys.Count > 0)
            {
                if (iRecipePrint == null)
                {
                    iRecipePrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.HISFC.BizProcess.Interface.IRecipePrint), 3) as Neusoft.HISFC.BizProcess.Interface.IRecipePrint;
                }

                //预览打印 补打的时候 预览打印
                Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.RecipePrint.frmRecipePrint frm = null;

                if (iRecipePrint == null)
                {
                    this.errInfo = "处方打印接口未实现！";
                }
                else
                {
                    int page = drugDictionary.Keys.Count;//总页数
                    int i = 1;//当前页数
                    int j = 1;//当前草药页数
                    int k = 1;//当前西药页数
                    int totalPCC = 0;//草药总数
                    int totalPCZ = 0;//西药总数

                    foreach (string recipeNO in drugDictionary.Keys)
                    {
                        string type = drugDictionary[recipeNO][0].Item.SysClass.ID.ToString() == "PCC" ? "草药" : "西药";
                        if (type == "草药")
                        {
                            totalPCC++;
                        }
                        else
                        {
                            totalPCZ++;
                        }
                    }

                    frm = new Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.RecipePrint.frmRecipePrint();

                    frm.ClearUC();

                    int index = 0;
                    foreach (string recipeNO in drugDictionary.Keys)
                    {
                        string type = drugDictionary[recipeNO][0].Item.SysClass.ID.ToString() == "PCC" ? "草药" : "西药";
                        string labelPage = " 第" + i + "页/共" + page + "页";

                        string labelDrug = " ";

                        if (type == "草药")
                        {
                            labelDrug = type + j.ToString() + "/" + totalPCC.ToString();
                            j++;
                            i++;
                        }
                        else
                        {
                            labelDrug = type + k.ToString() + "/" + totalPCZ.ToString();
                            k++;
                            i++;
                        }

                        //中六药房要求 不打印 本次诊疗药费等
                        if (type != "草药")
                        {
                            regObj.User01 = getTotalMoney(drugDictionary, regObj) + "|" + labelDrug + "|" + labelPage;
                        }
                        else
                        {
                            regObj.User01 = "" + "|" + labelDrug + "|" + labelPage;
                        }

                        iRecipePrint.SetPatientInfo(regObj);
                        iRecipePrint.RecipeNO = recipeNO;

                        if (isPreview)
                        {
                            Neusoft.HISFC.BizProcess.Interface.IRecipePrint iRecipe = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.HISFC.BizProcess.Interface.IRecipePrint), 3) as Neusoft.HISFC.BizProcess.Interface.IRecipePrint;

                            iRecipe.SetPatientInfo(regObj);
                            iRecipe.RecipeNO = recipeNO;

                            ControlList.Add(iRecipe as Control);
                        }
                        else
                        {
                            iRecipePrint.PrintRecipe();//初打
                        }
                    }
                }
            }
        }
        #endregion

        #region 实现院注射单接口
        private void Injection(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview, List<Control> ControlList)
        {
            if (isPreview)
            {

                List<Neusoft.HISFC.Models.Order.OutPatient.Order> dayorderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();

                string type = "Q";

                Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> injectDictionary = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();
                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrder)
                {
                    if (Neusoft.SOC.HISFC.BizProcess.Cache.Common.IsInnerInjectUsage(order.Usage.ID))
                    {
                        type = Neusoft.SOC.HISFC.BizProcess.Cache.Common.GetUsageSystemType(order.Usage.ID) == "IVD" ? "IVD" : "Q";

                        if (injectDictionary.ContainsKey(type))
                        {
                            injectDictionary[type].Add(order);
                        }
                        else
                        {
                            dayorderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                            dayorderList.Add(order);
                            injectDictionary.Add(type, dayorderList);
                        }
                    }
                }
                foreach (string usage in injectDictionary.Keys)
                {
                    ArrayList printList = new ArrayList(injectDictionary[usage]);
                    int iSet = 6;
                    int icount = Neusoft.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(printList.Count) / iSet));
                    for (int i = 1; i <= icount; i++)
                    {
                        Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCInjectBillPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 1) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;



                        ISOCInjectBillPrint.PreviewOutPatientOrderBill(regObj, reciptDept, reciptDoct, printList.Cast<Neusoft.HISFC.Models.Order.OutPatient.Order>().ToList(), isPreview);

                        //this.PrintOnePage(printList, i, icount, regObj, reciptDept, reciptDoct);
                        
                        ControlList.Add(ISOCInjectBillPrint as Control);
                    }
                }



            }
            else 
            {

                if (object.Equals(ISOCInjectBillPrint, null))
                {
                    ISOCInjectBillPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 1) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                }


                if (!object.Equals(ISOCInjectBillPrint, null))
                {
                    ISOCInjectBillPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alOrder, isPreview);
                }
            
            }


        }
        #endregion

        #region 实现院治疗单接口
        /// <summary>
        /// 实现院治疗单接口
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        private void TreatMentItem(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview, List<Control> ControlList)
        {

            if (isPreview)
            {

                Neusoft.FrameWork.Public.ObjectHelper usageHelper = new Neusoft.FrameWork.Public.ObjectHelper();
                Neusoft.HISFC.BizLogic.Manager.Constant constantMgr = new Neusoft.HISFC.BizLogic.Manager.Constant();
                usageHelper.ArrayObject = constantMgr.GetAllList(Neusoft.HISFC.Models.Base.EnumConstant.USAGE);

                ArrayList alSpecial = new ArrayList();
                ArrayList alSpecialInjectBill = constantMgr.GetAllList("TreatmentItem"); //已经维护的需要特别打单的药品

                Hashtable hsPrint = new Hashtable();
                //按照执行科室分组
                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in alOrder)
                {
                    //治疗用法打印
                    if (SOC.HISFC.BizProcess.Cache.Common.IsCure(order.Usage.ID))
                    {
                        if (!hsPrint.Contains(order.ExeDept.ID))
                        {
                            ArrayList al = new ArrayList();
                            al.Add(order);
                            hsPrint.Add(order.ExeDept.ID, al);
                        }
                        else
                        {
                            ArrayList al = hsPrint[order.ExeDept.ID] as ArrayList;
                            al.Add(order);
                            hsPrint[order.ExeDept.ID] = al;
                        }
                    }
                    else if (alSpecialInjectBill != null && alSpecialInjectBill.Count > 0)
                    {
                        foreach (Neusoft.HISFC.Models.Base.Const cnst in alSpecialInjectBill)
                        {
                            if (cnst.ID == order.Item.ID)
                            {
                                if (!hsPrint.Contains(order.ExeDept.ID))
                                {
                                    ArrayList al = new ArrayList();
                                    al.Add(order);
                                    hsPrint.Add(order.ExeDept.ID, al);
                                }
                                else
                                {
                                    ArrayList al = hsPrint[order.ExeDept.ID] as ArrayList;
                                    al.Add(order);
                                    hsPrint[order.ExeDept.ID] = al;
                                }

                                break;
                            }
                        }
                    }


                }

                if (hsPrint.Count > 0)
                {
                    foreach (string key in hsPrint.Keys)
                    {
                        ArrayList alData = hsPrint[key] as ArrayList;
                        int iSet = 8;

                        ArrayList alPrint = new ArrayList();
                        int icount = Neusoft.FrameWork.Function.NConvert.ToInt32(Math.Ceiling(Convert.ToDouble(alData.Count) / iSet));
                        for (int i = 1; i <= icount; i++)
                        {

                            Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint ISOCTreatmentBillPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 9) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;

                            if (i != icount)
                            {
                                alPrint = alData.GetRange(iSet * (i - 1), iSet);
                                ISOCTreatmentBillPrint.PreviewOutPatientOrderBill(regObj, reciptDept, reciptDoct, alPrint.Cast<Neusoft.HISFC.Models.Order.OutPatient.Order>().ToList(), isPreview);
                                ControlList.Add(ISOCTreatmentBillPrint as Control);
                            }
                            else
                            {
                                int num = alData.Count % iSet;
                                if (alData.Count % iSet == 0)
                                {
                                    num = iSet;
                                }
                                alPrint = alData.GetRange(iSet * (i - 1), num);
                                ISOCTreatmentBillPrint.PreviewOutPatientOrderBill(regObj, reciptDept, reciptDoct, alPrint.Cast<Neusoft.HISFC.Models.Order.OutPatient.Order>().ToList(), isPreview);
                                ControlList.Add(ISOCTreatmentBillPrint as Control);
                            }
                        }


                    }
                }
            }
            else
            {
                if (object.Equals(ISOCTreatmentBillPrint, null))
                {
                    ISOCTreatmentBillPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 9) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                }

                if (!object.Equals(ISOCTreatmentBillPrint, null))
                {
                    ISOCTreatmentBillPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alOrder, isPreview);
                }

            }
        }
        #endregion

        #region 实现门诊指引单
        private void Guide(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> IOrderList, bool isPreview)
        {
            if (object.Equals(ISOCOutPatientGuidePrint, null))
            {
                ISOCOutPatientGuidePrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 2) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            }

            if (!object.Equals(ISOCOutPatientGuidePrint, null))
            {
                ISOCOutPatientGuidePrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, IOrderList, isPreview);
            }
        }
        #endregion


        #region 实现门诊诊疗单
        private void ClinicsItem(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> IOrderList, bool isPreview, List<Control> ControlList)
        {

            if (isPreview && IOrderList.Count > 0)
            {

                IOutPatientOrderPrint ISOCOutPatientClinicsPrint = ISOCOutPatientClinicsPrint = UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(IOutPatientOrderPrint), 10) as IOutPatientOrderPrint;

                ISOCOutPatientClinicsPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, IOrderList, isPreview);
                ControlList.Add(ISOCOutPatientClinicsPrint as Control);
            }
            else
            {
                if (object.Equals(ISOCOutPatientClinicsPrint, null))
                {
                    ISOCOutPatientClinicsPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 10) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                }

                if (!object.Equals(ISOCOutPatientClinicsPrint, null) && IOrderList.Count > 0)
                {
                    ISOCOutPatientClinicsPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, IOrderList, isPreview);
                }
            }
        }
        #endregion


        #region 检查项目打印

        private void Examine(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, List<Neusoft.HISFC.Models.Order.OutPatient.Order> pacsList, bool isPreview, List<Control> ControlList)
        {
            Neusoft.HISFC.BizLogic.Fee.Item CheckApply = new Neusoft.HISFC.BizLogic.Fee.Item();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> hsPacsPrint = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();
            List<Neusoft.HISFC.Models.Order.OutPatient.Order> OneItem1 = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();

            Hashtable hsComb = new Hashtable();
            int pageCount = 5;

            //按照每页打印5个分页
            for (int i = 0; i < pacsList.Count; i++)
            {
                if (!hsComb.Contains(pacsList[i].Combo.ID))
                {
                    List<Neusoft.HISFC.Models.Order.OutPatient.Order> al = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                    al.Add(pacsList[i]);
                    hsComb.Add(pacsList[i].Combo.ID, al);
                }
                else
                {
                    List<Neusoft.HISFC.Models.Order.OutPatient.Order> al = hsComb[pacsList[i].Combo.ID] as List<Neusoft.HISFC.Models.Order.OutPatient.Order>;
                    al.Add(pacsList[i]);
                    hsComb[pacsList[i].Combo.ID] = al;
                }
            }

            //画组合号
            foreach (string keys in hsComb.Keys)
            {
                List<Neusoft.HISFC.Models.Order.OutPatient.Order> al = hsComb[keys] as List<Neusoft.HISFC.Models.Order.OutPatient.Order>;
                if (al.Count > 1)
                {
                    for (int i = 0; i < al.Count; i++)
                    {
                        if (i == 0)
                        {
                            al[i].Item.Name = "┌ " + al[i].Item.Name;
                        }
                        else if (i == al.Count - 1)
                        {
                            al[i].Item.Name = "└ " + al[i].Item.Name;
                        }
                        else
                        {
                            al[i].Item.Name = "│ " + al[i].Item.Name; 
                        }
                    }
                }
                else
                {
                    al[0].Item.Name = "   " + al[0].Item.Name;
                }
            }

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> hsPrint = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();
            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> hsNextPrint = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();
            int kk = 0;

            foreach (string keys in hsComb.Keys)
            {
                List<Neusoft.HISFC.Models.Order.OutPatient.Order> al = hsComb[keys] as List<Neusoft.HISFC.Models.Order.OutPatient.Order>;
                string applyName = SOC.HISFC.BizProcess.Cache.Fee.GetItem((al[0] as Neusoft.HISFC.Models.Order.OutPatient.Order).Item.ID).CheckApplyDept;
                (al[0].Item as Neusoft.HISFC.Models.Fee.Item.Undrug).CheckApplyDept = applyName;

                string execDept = (al[0] as Neusoft.HISFC.Models.Order.OutPatient.Order).ExeDept.ID;
                if (!hsPrint.ContainsKey(applyName + execDept))
                {
                    hsPrint.Add(applyName + execDept, al);
                }
                else
                {
                    List<Neusoft.HISFC.Models.Order.OutPatient.Order> alTemp = hsPrint[applyName + execDept] as List<Neusoft.HISFC.Models.Order.OutPatient.Order>;
                    if (alTemp.Count + al.Count > 5)
                    {
                        hsNextPrint.Add(applyName + execDept + kk.ToString(), alTemp);
                        kk += 1;
                        hsPrint[applyName + execDept] = al;
                    }
                    else
                    {
                        alTemp.AddRange(al);
                        hsPrint[applyName + execDept] = alTemp;
                    }
                }
            }

            foreach (string key in hsPrint.Keys)
            {
                iPacsPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 5) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;

                if (iPacsPrint == null)
                {
                    this.errInfo = "处方打印接口未实现！";
                }
                else
                {
                    iPacsPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, hsPrint[key], isPreview);

                    ControlList.Add(iPacsPrint as Control);
                }
            }

            foreach (string key in hsNextPrint.Keys)
            {
                iPacsPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 5) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;

                if (iPacsPrint == null)
                {
                    this.errInfo = "处方打印接口未实现！";
                }
                else
                {
                    iPacsPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, hsNextPrint[key], isPreview);

                    ControlList.Add(iPacsPrint as Control);
                }
            }

            //for (int t = 0; t < pacsList.Count; t++)
            //{
            //    //Undrug ItemTitle = CheckApply.GetUndrugByCode(pacsList[t].Item.ID);
            //    Undrug ItemTitle = SOC.HISFC.BizProcess.Cache.Fee.GetItem(pacsList[t].Item.ID);
            //    ItemTitle.Qty = pacsList[t].Qty;
            //    pacsList[t].Item = ItemTitle;
            //    string CheckCode = ItemTitle.CheckApplyDept;

            //    if (!hsComb.Contains(pacsList[t].Combo.ID))
            //    {
            //        hsComb.Add(pacsList[t].Combo.ID, pacsList[t]);
            //    }
            //    else
            //    {
            //        Undrug undrug = SOC.HISFC.BizProcess.Cache.Fee.GetItem((hsComb[pacsList[t].Combo.ID] as Neusoft.HISFC.Models.Order.OutPatient.Order).Item.ID);
            //        CheckCode = undrug.CheckApplyDept;
            //    }
            //    //else if (string.IsNullOrEmpty(CheckCode))
            //    //{
            //    //    OneItem1.Add(pacsList[t]);
            //    //}
            //    //else 
            //    if (hsPacsPrint.Keys.Contains(CheckCode))
            //    {
            //        List<Neusoft.HISFC.Models.Order.OutPatient.Order> al = hsPacsPrint[CheckCode] as List<Neusoft.HISFC.Models.Order.OutPatient.Order>;
            //        al.Add(pacsList[t]);
            //        hsPacsPrint[CheckCode] = al;
            //    }
            //    else
            //    {
            //        List<Neusoft.HISFC.Models.Order.OutPatient.Order> al = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
            //        al.Add(pacsList[t]);
            //        hsPacsPrint.Add(CheckCode, al);
            //    }
            //}

            //if (OneItem1.Count > 0)
            //{
            //    iPacsPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 5) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            //    iPacsPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, OneItem1, isPreview);

            //    ControlList.Add(iPacsPrint as Control);

            //}

            //if (hsPacsPrint.Count > 0)
            //{
            //    foreach (string key in hsPacsPrint.Keys)
            //    {
            //        iPacsPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 5) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;

            //        if (iPacsPrint == null)
            //        {
            //            this.errInfo = "处方打印接口未实现！";
            //        }
            //        else
            //        {
            //            iPacsPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, hsPacsPrint[key], isPreview);

            //            ControlList.Add(iPacsPrint as Control);
            //        }
            //    }
            //}

        }

        #endregion

        #region 检验项目打印

        private void Inspection(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, List<Neusoft.HISFC.Models.Order.OutPatient.Order> lisList, bool isPreview, List<Control> ControlList)
        {



            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> printList = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();
            for (int i = 0; i < lisList.Count; i++)
            {
                if (printList.ContainsKey(lisList[i].ApplyNo))
                {
                    printList[lisList[i].ApplyNo].Add(lisList[i]);

                }
                else
                {
                    List<Neusoft.HISFC.Models.Order.OutPatient.Order> itemList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                    itemList.Add(lisList[i]);
                    printList.Add(lisList[i].ApplyNo, itemList);
                }
            }

            foreach (string key in printList.Keys)
            {
                iLisPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.HeYuan.HPRM.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 7) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;

                if (iLisPrint == null)
                {
                    this.errInfo = "处方打印接口未实现！";

                }
                else
                {
                    iLisPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, printList[key], isPreview);

                    ControlList.Add(iLisPrint as Control);
                }
            }

        }

        #endregion

        #endregion

        #region IOutPatientPrint 成员


        public int OnOutPatientPrint(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderPrintList, List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderSelectList, bool IsReprint, bool isPreview, string printType, object obj)
        {
            List<Neusoft.HISFC.Models.Order.OutPatient.Order> pacsList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();

            List<Neusoft.HISFC.Models.Order.OutPatient.Order> lisList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();

            List<Neusoft.HISFC.Models.Order.OutPatient.Order> treatmentList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
            List<Neusoft.HISFC.Models.Order.OutPatient.Order> clinicsList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> limitDrugDictionary = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> drugDictionary = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in orderPrintList)
            {
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    Neusoft.HISFC.Models.Pharmacy.Item PharmacyItem = order.Item as Neusoft.HISFC.Models.Pharmacy.Item;

                    this.SetOrderList(drugDictionary, order);
                }
                else if (order.Item.ItemType == EnumItemType.UnDrug)
                {
                    switch (order.Item.SysClass.ID.ToString())
                    {
                        case "UL":
                            lisList.Add(order);
                            break;
                        case "UC":
                            pacsList.Add(order);
                            break;
                        case "UZ":
                            treatmentList.Add(order);
                            clinicsList.Add(order);
                            break;
                        case "UO":
                            treatmentList.Add(order);
                            clinicsList.Add(order);
                            break;
                        default:
                            clinicsList.Add(order);
                            break;
                    }
                }
            }

            Dictionary<Int32, List<Control>> previewDictionary = new Dictionary<Int32, List<Control>>();


            List<Control> recipeControlList = new List<Control>();

            List<Control> clinicsControlList = new List<Control>();

            List<Control> lisControlList = new List<Control>();

            List<Control> pacsControlList = new List<Control>();

            List<Control> injectionControlList = new List<Control>();


            List<Control> treatMentControlList = new List<Control>();

            //根据传递的单据类型打印
            switch ((EnumOutPatientBill)Neusoft.FrameWork.Function.NConvert.ToInt32(printType))
            {
                case EnumOutPatientBill.RecipeBill:
                    CommonDrug(regObj, drugDictionary, new ArrayList(orderPrintList), recipeControlList, isPreview);        //普通处方      
                    if (recipeControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.RecipeBill, recipeControlList);
                    }
                    break;
                case EnumOutPatientBill.ClinicsBill:
                    ClinicsItem(regObj, reciptDept, reciptDoct, clinicsList, isPreview, clinicsControlList);//诊疗单

                    if (clinicsControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.ClinicsBill, clinicsControlList);
                    }

                    break;
                case EnumOutPatientBill.TreatmentBill:
                    TreatMentItem(regObj, reciptDept, reciptDoct, new ArrayList(orderPrintList), isPreview, treatMentControlList);//治疗单
                    if (treatMentControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.PacsBill, treatMentControlList);
                    }
                    break;
                case EnumOutPatientBill.GuideBill:
                    Guide(regObj, reciptDept, reciptDoct, orderPrintList,isPreview);     //指引单
                    break;
                case EnumOutPatientBill.LisBill:
                    Inspection(regObj, reciptDept, reciptDoct, lisList, isPreview, lisControlList);

                    if (lisControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.LisBill, lisControlList);
                    }
                    break;
                case EnumOutPatientBill.PacsBill:
                    Examine(regObj, reciptDept, reciptDoct, pacsList, isPreview, pacsControlList);

                    if (pacsControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.PacsBill, pacsControlList);
                    }
                    break;
                case EnumOutPatientBill.InjectBill:
                    Injection(regObj, reciptDept, reciptDoct, new ArrayList(orderPrintList), isPreview, injectionControlList);    //注射单       
                    if (injectionControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.InjectBill, injectionControlList);
                    }
                    
                    break;
                case EnumOutPatientBill.AllBill:
                    Neusoft.HISFC.Models.Registration.Register regObjClone = regObj.Clone();
                    CommonDrug(regObjClone, drugDictionary, new ArrayList(orderPrintList), recipeControlList, isPreview);        //普通处方

                    if (recipeControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.RecipeBill, recipeControlList);
                    }

                    ClinicsItem(regObj, reciptDept, reciptDoct, clinicsList, isPreview, clinicsControlList);//诊疗单
                    if (clinicsControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.ClinicsBill, clinicsControlList);
                    }

                    Injection(regObj, reciptDept, reciptDoct, new ArrayList(orderPrintList), isPreview, injectionControlList);    //注射单
                    if (injectionControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.InjectBill, injectionControlList);
                    }

                    Inspection(regObj, reciptDept, reciptDoct, lisList, isPreview, lisControlList);
                    if (lisControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.LisBill, lisControlList);
                    }

                    Examine(regObj, reciptDept, reciptDoct, pacsList, isPreview, pacsControlList);
                    if (pacsControlList.Count > 0)
                    {
                        previewDictionary.Add((int)EnumOutPatientBill.PacsBill, pacsControlList);
                    }

                    break;
            }
            if (isPreview && previewDictionary.Keys.Count>0)
            {
                frmPreviewControl frmPreviewControl = new frmPreviewControl(previewDictionary);

                frmPreviewControl.ShowPreviewControlDialog();

                frmPreviewControl.ShowDialog();
            }

            return 1;
        }

        #endregion
    }
}
