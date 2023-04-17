using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Neusoft.HISFC.Models.Base;
using Neusoft.HISFC.Models.Order.OutPatient;

namespace Neusoft.SOC.Local.Order.OutPatientOrder.GYSY
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
        #endregion

        #region 接口

        /// <summary>
        /// 处方打印接口
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.IRecipePrint iRecipePrint = null;

        /// <summary>
        /// 麻毒精处方打印接口
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.IRecipePrint iLimitRecipePrint = null;

        /// <summary>
        /// 检查打印接口
        /// </summary>
        Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint iPacsPrint = null;
        //Neusoft.HISFC.BizProcess.Interface.IRecipePrint iPacsPrint = null;

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
        /// PACS申请单打印接口
        /// </summary>
        Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint IPacsReportPrint = null;

        #endregion
        
        List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList = null;

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
                orderList = orderDictionary[order.ReciptNO];
                if (orderList == null)
                {
                    orderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                    orderList.Add(order);
                    orderDictionary[order.ReciptNO] = orderList;
                }
                else
                {
                    orderList.Add(order);
                    orderDictionary[order.ReciptNO] = orderList;
                }
            }
            else
            {
                orderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
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
        /// 实现住院医嘱保存完的后续操作
        /// </summary>
        /// <param name="patientInfo">挂号实体</param>
        /// <param name="reciptDept">开立科室</param>
        /// <param name="reciptDoct">开立医生</param>
        /// <param name="alOrder">医嘱列表</param>
        /// <returns></returns>
        public int OnSaveOrderForInPatient(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {
            return 1;
        }

        /// <summary>
        /// 实现门诊处方保存完的后续操作
        /// </summary>
        /// <param name="regObj">住院患者实体</param>
        /// <param name="reciptDept">开立科室</param>
        /// <param name="reciptDoct">开立医生</param>
        /// <param name="alOrder">医嘱列表</param>
        /// <returns></returns>
        public int OnSaveOrderForOutPatient(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        {

            return 1;
        }

        #region 普通处方
        private void CommonDrug(Neusoft.HISFC.Models.Registration.Register regObj, Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> drugDictionary, bool isPreview)
        {
            if (drugDictionary.Keys.Count > 0)
            {
                if (iRecipePrint == null)
                {
                    iRecipePrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.GYSY.SaveRecipeFactory), typeof(Neusoft.HISFC.BizProcess.Interface.IRecipePrint), 3) as Neusoft.HISFC.BizProcess.Interface.IRecipePrint;
                }
                if (iRecipePrint == null)
                {
                    this.errInfo = "处方打印接口未实现！";
                }
                else
                {
                    if (isPreview)        //补打
                    {
                        foreach (string recipeNO in drugDictionary.Keys)
                        {
                            iRecipePrint.SetPatientInfo(regObj);
                            iRecipePrint.RecipeNO = recipeNO;
                            iRecipePrint.PrintRecipeView(new ArrayList());
                        }
                    }
                    else
                    {           //初打
                   

                        foreach (string recipeNO in drugDictionary.Keys)
                        {
                            iRecipePrint.SetPatientInfo(regObj);
                            iRecipePrint.RecipeNO = recipeNO;
                            iRecipePrint.PrintRecipe();
                        }
                    }
                }
            }
        }
        #endregion

        #region 麻毒精处方
        private void AnestheticChlorpromazine(Neusoft.HISFC.Models.Registration.Register regObj, Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> limitDrugDictionary, bool isPreview)
        {
            if (limitDrugDictionary.Keys.Count > 0)
            {
                if (iLimitRecipePrint == null)
                {
                    iLimitRecipePrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.GYSY.SaveRecipeFactory), typeof(Neusoft.HISFC.BizProcess.Interface.IRecipePrint), 4) as Neusoft.HISFC.BizProcess.Interface.IRecipePrint;
                }
                if (iLimitRecipePrint == null)
                {
                    this.errInfo = "麻毒精处方打印接口未实现！";
                }
                else
                {
                    if (isPreview)        //初打
                    {
                        foreach (string recipeNO in limitDrugDictionary.Keys)
                        {
                            iLimitRecipePrint.SetPatientInfo(regObj);
                            iLimitRecipePrint.RecipeNO = recipeNO;
                            iLimitRecipePrint.PrintRecipeView(new ArrayList());
                        }
                    }
                    else
                    {

                        foreach (string recipeNO in limitDrugDictionary.Keys)
                        {
                            iLimitRecipePrint.SetPatientInfo(regObj);
                            iLimitRecipePrint.RecipeNO = recipeNO;
                            iLimitRecipePrint.PrintRecipe();
                        }

                   
                    }
                }
            }
        }
        #endregion

        #region 实现院注射单接口
        //private void Injection(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder)
        //{
        //    if (object.Equals(ISOCInjectBillPrint, null))
        //    {
        //        ISOCInjectBillPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.GYSY.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 1) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
        //    }

        //    if (!object.Equals(ISOCInjectBillPrint, null))
        //    {
        //        ISOCInjectBillPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alOrder);
        //    }
        //}
        #endregion

        #region 实现院治疗单接口
        /// <summary>
        /// 实现院治疗单接口
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="alOrder"></param>
        private void TreatMentItem(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, System.Collections.ArrayList alOrder, bool isPreview)
        {
            if (object.Equals(ISOCTreatmentBillPrint, null))
            {
                ISOCTreatmentBillPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.GYSY.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 9) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            }

            if (!object.Equals(ISOCTreatmentBillPrint, null))
            {
                ISOCTreatmentBillPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, alOrder, isPreview);
            }
        }
        #endregion

        #region 实现门诊指引单
        private void Guide(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, IList<Neusoft.HISFC.Models.Order.OutPatient.Order> IOrderList, bool isPreview)
        {
            if (object.Equals(ISOCOutPatientGuidePrint, null))
            {
                ISOCOutPatientGuidePrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.GYSY.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 2) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
            }

            if (!object.Equals(ISOCOutPatientGuidePrint, null))
            {
                ISOCOutPatientGuidePrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, IOrderList, isPreview);
            }
        }
        #endregion


        #region 检查申请单打印

        /// <summary>
        /// PACS申请单打印
        /// </summary>
        public void PacsReportPrint(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, ArrayList allOrder, bool isPreview)
        {
           
            if (IPacsReportPrint == null)
            {
                this.IPacsReportPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint)) as Neusoft.HISFC.BizProcess.Interface.Order.IPacsReportPrint;
            }

            if (IPacsReportPrint == null)
            {
                MessageBox.Show("PACS打印接口未实现！请联系信息科！\r\n" + Neusoft.FrameWork.WinForms.Classes.UtilInterface.Err, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IPacsReportPrint.PacsReportPrintForOutPatient(regObj, reciptDept, reciptDoct, allOrder) == -1)
            {
                MessageBox.Show(IPacsReportPrint.ErrInfo);
                return;
            }
        }

        #endregion


        #region 检查项目打印
        private void Examine(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> pacsDictionary, bool isPreview)
        {
            if (pacsDictionary.Keys.Count > 0)
            {
                if (iPacsPrint == null)
                {
                    iPacsPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.GYSY.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 5) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                }
                if (iPacsPrint == null)
                {
                    this.errInfo = "处方打印接口未实现！";
                }
                else
                {
                    foreach (string item in pacsDictionary.Keys)
                    {
                        iPacsPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, pacsDictionary[item], isPreview);
                    }
                }
            }
        }
        #endregion

        #region 检验项目打印
        private void Inspection(Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> lisDictionary, bool isPreview)
        {
            if (lisDictionary.Keys.Count > 0)
            {
                if (iLisPrint == null)
                {
                    iLisPrint = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.SOC.Local.Order.OutPatientOrder.GYSY.SaveRecipeFactory), typeof(Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint), 4) as Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint;
                }

                if (iLisPrint == null)
                {
                    this.errInfo = "处方打印接口未实现！";
                }
                else
                {
                    //foreach (string recipeNO in pacsDictionary.Keys)
                    //{
                    //    iLisPrint.SetPatientInfo(regObj);
                    //    iLisPrint.RecipeNO = recipeNO;
                    //    iLisPrint.PrintRecipe();
                    //}
                }
            }
        }
        #endregion

        #endregion

        #region IOutPatientPrint 成员


        public int OnOutPatientPrint(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderPrintList, List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderSelectList, bool IsReprint, bool isPreview, string printType, object obj)
        {


            List<Neusoft.HISFC.Models.Order.OutPatient.Order> sublList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> limitDrugDictionary = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> drugDictionary = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> pacsDictionary = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> lisDictionary = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> treatmentDictionary = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> sublDictionary = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in orderPrintList)
            {
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    Neusoft.HISFC.Models.Pharmacy.Item PharmacyItem = order.Item as Neusoft.HISFC.Models.Pharmacy.Item;

                    if (PharmacyItem.Quality.ID.Equals("P") || PharmacyItem.Quality.ID.Equals("P1") || PharmacyItem.Quality.ID.Equals("P2") || PharmacyItem.Quality.ID.Equals("S"))
                    {
                        this.SetOrderList(limitDrugDictionary, order);
                    }
                    else
                    {
                        this.SetOrderList(drugDictionary, order);
                    }
                }
                else if (order.Item.ItemType == EnumItemType.UnDrug)
                {
                    switch (order.Item.SysClass.ID.ToString())
                    {
                        case "UL":
                            this.SetOrderList(lisDictionary, order);
                            break;
                        case "UC":
                            this.SetOrderList(pacsDictionary, order);
                            break;
                        case "UZ":
                            this.SetOrderList(treatmentDictionary, order);
                            break;
                        case "UO":
                            this.SetOrderList(treatmentDictionary, order);
                            break;
                        default:
                            sublList.Add(order);
                            break;
                    }
                }
                else if (order.Item.ItemType == EnumItemType.MatItem)
                {

                }
            }

            //根据传递的单据类型打印
            switch ((EnumOutPatientBill)Neusoft.FrameWork.Function.NConvert.ToInt32(printType))
            {
                case EnumOutPatientBill.RecipeBill:
                    CommonDrug(regObj, drugDictionary, isPreview);        //普通处方
                    AnestheticChlorpromazine(regObj, limitDrugDictionary, isPreview);  //麻毒精处方         
                    break;
                case EnumOutPatientBill.TreatmentBill:
                    TreatMentItem(regObj, reciptDept, reciptDoct, new ArrayList(orderPrintList), isPreview);//治疗单
                    break;
                case EnumOutPatientBill.GuideBill:
                    Guide(regObj, reciptDept, reciptDoct, orderPrintList, isPreview);     //指引单
                    break;
                case EnumOutPatientBill.LisBill:
                    Inspection(lisDictionary, isPreview);       //检验       
                    break;
                case EnumOutPatientBill.PacsBill:
                    PacsReportPrint(regObj, reciptDept, reciptDoct, new ArrayList(orderSelectList), isPreview);
                    break;
                case EnumOutPatientBill.AllBill:
                    CommonDrug(regObj, drugDictionary, isPreview);        //普通处方
                    AnestheticChlorpromazine(regObj, limitDrugDictionary, isPreview);  //麻毒精处方   
                    TreatMentItem(regObj, reciptDept, reciptDoct, new ArrayList(orderPrintList), isPreview);//治疗单
                    Guide(regObj, reciptDept, reciptDoct, orderPrintList, isPreview);     //指引单
                    break;
            }
            return 1;
        }

        #endregion
    }
}
