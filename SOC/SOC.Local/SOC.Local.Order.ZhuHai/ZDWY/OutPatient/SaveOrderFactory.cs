using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Order.OutPatient;
using FS.SOC.Local.Order.ZhuHai.ZDWY.Common.OrderSplit;
using FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.PacsBillPrint;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient
{
    /// <summary>
    /// 医嘱保存后工厂类
    /// </summary>
    public class SaveOrderFactory : FS.HISFC.BizProcess.Interface.Order.IOutPatientPrint
    {

        /// <summary>
        /// 参数控制类
        /// </summary>
        private static FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// 常数控制类
        /// </summary>
        private static FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();


        private int i = 1;

        /// <summary>
        /// 处方打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="drugDictionary"></param>
        /// <param name="alOrder"></param>
        /// <param name="ControlList"></param>
        /// <param name="isPreview"></param>
        private void RecipeBill(FS.HISFC.Models.Registration.Register regObj, Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> drugDictionary, System.Collections.ArrayList alOrder, List<Control> ControlList, bool isPreview)
        {
            if (drugDictionary.Keys.Count <= 0)
            {
                return;
            }
            int speLevl1 = 1;
            foreach (string recipeNO in drugDictionary.Keys)
            {
                FS.HISFC.Models.Order.OutPatient.Order firstOrder = drugDictionary[recipeNO][0];

                FS.HISFC.BizProcess.Interface.IRecipePrint recipe = null;
                if (firstOrder.Item.SysClass.ID.ToString() == "PCC")
                {
                    recipe = new FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.IBORN.ucRecipePrintHerbIBORN();
                }
                else
                {
                    //int speLevl = 1;
                    //foreach (FS.HISFC.Models.Order.OutPatient.Order order in drugDictionary[recipeNO])
                    //{
                    //    int level = ZDWY.Function.GetItemQaulity(order);
                    //    if (level > speLevl)
                    //    {
                    //        speLevl = level;
                    //    }
                    //}
                    // {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                    ////3、毒麻精一；2、精二；1、普通；0、非药品
                    //if (speLevl == 3 || speLevl == 2)
                    //{
                    //    speLevl1 = speLevl;
                    //    ArrayList drugDictionaryList = new ArrayList(drugDictionary[recipeNO]);
                    //    ArrayList drugDictionaryOne;
                    //    for (int i = 0; i < drugDictionaryList.Count; i++)
                    //    {
                    //        recipe = new FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.IBORN.ucRecipePrintIBORN();

                    //        drugDictionaryOne = new ArrayList();
                    //        recipe.SetPatientInfo(regObj);

                    //        drugDictionaryOne.Add(drugDictionaryList[i]);
                    //        recipe.PrintRecipeView(drugDictionaryOne);
                    //        if (isPreview)
                    //        {
                    //            ControlList.Add(recipe as Control);
                    //        }
                    //        else
                    //        {
                    //            recipe.PrintRecipe();
                    //        }
                    //    }
                    //}
                    //else if (speLevl == 2)
                    //{

                    //    speLevl1 = speLevl;
                    //    ArrayList drugDictionaryList = new ArrayList(drugDictionary[recipeNO]);
                    //    ArrayList drugDictionaryOne;
                    //    for (int i = 0; i < drugDictionaryList.Count; i++)
                    //    {
                    //        recipe = new FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.IBORN.ucRecipePrintIBORN();

                    //        drugDictionaryOne = new ArrayList();
                    //        recipe.SetPatientInfo(regObj);

                    //        drugDictionaryOne.Add(drugDictionaryList[i]);
                    //        recipe.PrintRecipeView(drugDictionaryOne);
                    //        if (isPreview)
                    //        {
                    //            ControlList.Add(recipe as Control);
                    //        }
                    //        else
                    //        {
                    //            recipe.PrintRecipe();
                    //        }

                    //    }
                    //}
                    //{92F1E785-D681-4a0d-8806-8A330BED5228}
                    //if (currDept.HospitalID == "BELLAIRE")// {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB}
                    //{
                    //    recipe = new FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.ucRecipePrintBLE();

                    //}
                    //else
                    {
                        recipe = new FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RecipePrint.IBORN.ucRecipePrintIBORN();
                    }
                }

                //已经有列表了 就不需要再用处方号查询了
                //recipe.RecipeNO = recipeNO;
                //if (speLevl1 != 3 && speLevl1 != 2)
                //{
                //}
                // {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                recipe.SetPatientInfo(regObj);
                recipe.PrintRecipeView(new ArrayList(drugDictionary[recipeNO]));
                if (isPreview)
                {
                    ControlList.Add(recipe as Control);
                }
                else
                {
                    recipe.PrintRecipe();
                }

            }
        }

        /// <summary>
        /// 检验项目打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="lisDictionary"></param>
        /// <param name="isPreview"></param>
        /// <param name="ControlList"></param>
        private void BillPrint(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> dicOrder, bool isPreview, List<Control> ControlList, EnumOutPatientBill billType)
        {
            if (dicOrder.Count == 0)
            {
                return;
            }

            int pageRowCount = 0;
            if (billType == EnumOutPatientBill.LisBill)
            {
                pageRowCount = 13;// {3FF25BA5-1251-4dbb-8895-F94D8FF26BAB}
            }
            else if (billType == EnumOutPatientBill.InjectBill)
            {
                pageRowCount = 5;
            }
            else if (billType == EnumOutPatientBill.TreatmentBill)
            {
                pageRowCount = 5;
            }
            else if (billType == EnumOutPatientBill.PacsBill)
            {
                pageRowCount = 10;
            }
            else if (billType == EnumOutPatientBill.MaterialBill)
            {
                pageRowCount = 5;
            }

            else if (billType == EnumOutPatientBill.LisReceiptBill)// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
            {
                pageRowCount = 10;
            }

            else if (billType == EnumOutPatientBill.MedicalReportBill)// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
            {
                pageRowCount = 10;
            }

            foreach (string key in dicOrder.Keys)
            {
                if (dicOrder[key].Count <= pageRowCount)
                {
                    PrintOnePage(regObj, reciptDept, reciptDoct, dicOrder[key], isPreview, ref ControlList, billType);
                }
                else
                {
                    List<FS.HISFC.Models.Order.OutPatient.Order> list = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                    int count = 0;
                    foreach (FS.HISFC.Models.Order.OutPatient.Order ord in dicOrder[key])
                    {
                        list.Add(ord);
                        count++;
                        if (list.Count == pageRowCount
                            || count == dicOrder[key].Count)
                        {
                            PrintOnePage(regObj, reciptDept, reciptDoct, list, isPreview, ref ControlList, billType);
                            list.Clear();
                        }
                    }
                }
            }
        }

        private void PrintOnePage(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, List<FS.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview, ref List<Control> ControlList, EnumOutPatientBill billType)
        {
            FS.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint orderPrint = null;

            if (billType == EnumOutPatientBill.TreatmentBill)
            {
                // {92F1E785-D681-4a0d-8806-8A330BED5228}
                //if (currDept.HospitalID == "BELLAIRE")// {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB}
                //{
                //    orderPrint = new ZDWY.OutPatient.ClinicsBillPrint.ucClinicsBillPrintBLE();
                //}
                //else
                {

                    orderPrint = new ZDWY.OutPatient.ClinicsBillPrint.ucClinicsBillPrintIBORN();
                }

                orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);

                if (isPreview)
                {
                    ControlList.Add(orderPrint as Control);
                }
            }
            else if (billType == EnumOutPatientBill.InjectBill)
            {
                //{92F1E785-D681-4a0d-8806-8A330BED5228}
                //if (currDept.HospitalID == "BELLAIRE")// {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB}
                //{
                //    //orderPrint = new ZDWY.OutPatient.InjectBillPrint.ucInjectBillPrintBLE();
                //    //orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);

                //    //if (isPreview)
                //    //{
                //    //    ControlList.Add(orderPrint as Control);
                //    //}

                //    //{2745F03D-6B4B-4888-988A-F0FC8C7353A6}
                //    //爱博恩医疗门诊部调整打印单
                //    List<FS.HISFC.Models.Order.OutPatient.Order> newOrderList = null;
                //    ArrayList recipeNoList = new ArrayList();
                //    foreach (FS.HISFC.Models.Order.OutPatient.Order order in orderList)
                //    {
                //        if (!recipeNoList.Contains(order.ReciptNO))
                //        {
                //            recipeNoList.Add(order.ReciptNO);
                //        }
                //    }
                //    foreach (string reciptNO in recipeNoList)
                //    {
                //        newOrderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                //        foreach (FS.HISFC.Models.Order.OutPatient.Order order1 in orderList)
                //        {
                //            if (reciptNO == order1.ReciptNO)
                //            {
                //                newOrderList.Add(order1);
                //            }
                //        }

                //        orderPrint = new ZDWY.OutPatient.InjectBillPrint.ucInjectBillPrintBLENew();
                //        orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, newOrderList, isPreview);

                //        if (isPreview)
                //        {
                //            ControlList.Add(orderPrint as Control);
                //        }
                //    }
                //}
                //else
                {// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                    List<FS.HISFC.Models.Order.OutPatient.Order> newOrderList = null;
                    ArrayList recipeNoList = new ArrayList();
                    foreach (FS.HISFC.Models.Order.OutPatient.Order order in orderList)
                    {
                        if (!recipeNoList.Contains(order.ReciptNO))
                        {
                            recipeNoList.Add(order.ReciptNO);
                        }
                    }
                    foreach (string reciptNO in recipeNoList)
                    {
                        newOrderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                        foreach (FS.HISFC.Models.Order.OutPatient.Order order1 in orderList)
                        {
                            if (reciptNO == order1.ReciptNO)
                            {
                                newOrderList.Add(order1);
                            }
                        }

                        orderPrint = new ZDWY.OutPatient.InjectBillPrint.ucInjectBillPrintIBORN();
                        orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, newOrderList, isPreview);

                        if (isPreview)
                        {
                            ControlList.Add(orderPrint as Control);
                        }
                    }
                }
            }
            else if (billType == EnumOutPatientBill.LisBill)
            {

                //if (currDept.HospitalID == "BELLAIRE")// {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB}
                if (false)// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}
                {
                    int ii = 0;//以下方法是进行检验单超过4项则分页
                    List<FS.HISFC.Models.Order.OutPatient.Order> orderone = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                    foreach (FS.HISFC.Models.Order.OutPatient.Order orderthis in orderList)
                    {
                        ii++;
                        orderone.Add(orderthis);
                        if (ii % 3 == 0)
                        {
                            orderPrint = new ZDWY.OutPatient.LisBillPrint.ucLisBillPrintBLE();
                            orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderone, isPreview);
                            if (isPreview)
                            {
                                ControlList.Add(orderPrint as Control);
                            }
                            orderone = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                        }

                        if (ii == orderList.Count && orderList.Count % 3 > 0)
                        {
                            orderPrint = new ZDWY.OutPatient.LisBillPrint.ucLisBillPrintBLE();
                            orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderone, isPreview);
                            if (isPreview)
                            {
                                ControlList.Add(orderPrint as Control);
                            }
                        }
                    }
                }
                else
                {

                    List<FS.HISFC.Models.Order.OutPatient.Order> newOrderList = null;
                    ArrayList recipeNoList = new ArrayList();
                    foreach (FS.HISFC.Models.Order.OutPatient.Order order in orderList)
                    {
                        if (!recipeNoList.Contains(order.ReciptNO))
                        {
                            recipeNoList.Add(order.ReciptNO);
                        }
                    }
                    foreach (string reciptNO in recipeNoList)
                    {
                        newOrderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                        foreach (FS.HISFC.Models.Order.OutPatient.Order order1 in orderList)
                        {
                            if (reciptNO == order1.ReciptNO)
                            {
                                newOrderList.Add(order1);
                            }
                        }

                        orderPrint = new ZDWY.OutPatient.LisBillPrint.ucLisBillPrintIBORN();
                        orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, newOrderList, isPreview);

                        if (isPreview)
                        {
                            ControlList.Add(orderPrint as Control);
                        }
                    }


                }
            }

            else if (billType == EnumOutPatientBill.LisReceiptBill)// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
            {
                orderPrint = new ZDWY.OutPatient.LisBillPrint.ucLisReceiptBillPrintIBORN();
                orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);

                if (isPreview)
                {
                    ControlList.Add(orderPrint as Control);
                }

            }
            else if (billType == EnumOutPatientBill.PacsBill)
            {
                //目前只有一种申请单，以后增加再写条件就行了 {D793A341-AD35-4685-8817-5614217969AD} 2014-12-16 by lixuelong

                //if (currDept.HospitalID == "BELLAIRE")// {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB}
                if (false)// {CDB01BF4-B40F-4cdc-9F0D-23F074290136}
                {
                    //先用名字判断检查类型{D81A0FAE-133E-4725-8558-8FC9DCBB1A5E}
                    List<FS.HISFC.Models.Order.OutPatient.Order> orderListX = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                    List<FS.HISFC.Models.Order.OutPatient.Order> orderListUnX = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                    foreach (FS.HISFC.Models.Order.OutPatient.Order order in orderList)
                    {
                        if (order.Item.Name.Contains("MR")
                            || order.Item.Name.Contains("CT")
                            || order.Item.Name.Contains("DR")
                            || order.Item.Name.Contains("MRI")
                            || order.Item.Name.Contains("X线"))
                        {
                            orderListX.Add(order);
                        }
                        else
                        {
                            orderListUnX.Add(order);
                        }
                    }
                    if (orderListX.Count > 0)
                    {
                        orderPrint = new ZDWY.OutPatient.PacsBillPrint.ucPacsBillPrintXBLE();
                        orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderListX, isPreview);
                        if (isPreview)
                        {
                            ControlList.Add(orderPrint as Control);
                        }
                    }
                    if (orderListUnX.Count > 0)
                    {
                        orderPrint = new ZDWY.OutPatient.PacsBillPrint.ucPacsBillPrintBLE();
                        orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderListUnX, isPreview);
                        if (isPreview)
                        {
                            ControlList.Add(orderPrint as Control);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(orderList[0].RefundReason))
                    {
                        orderPrint = new ZDWY.OutPatient.PacsBillPrint.ucPacsInformedBillPrintIBORN();
                    }
                    else if (string.IsNullOrEmpty(orderList[0].Item.Extend1))
                    {
                        orderPrint = new ZDWY.OutPatient.PacsBillPrint.ucPacsBillPrintIBORN();
                    }
                    else
                    {
                        orderPrint = new ZDWY.OutPatient.PacsBillPrint.ucPacsBillPrintEndoscopeIBORN();
                    }
                    orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);

                    if (isPreview)
                    {
                        ControlList.Add(orderPrint as Control);
                    }
                }

            }

            else if (billType == EnumOutPatientBill.MaterialBill)
            {

                //if (currDept.HospitalID == "BELLAIRE")// {FCA8E55A-6BAD-4ed7-9641-B01D188C07EB}
                //{
                //    orderPrint = new ZDWY.OutPatient.MaterialsBillPrint.ucMaterialsBillPrintIBORN();
                //    orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);

                //    if (isPreview)
                //    {
                //        ControlList.Add(orderPrint as Control);
                //    }
                //}
                //else
                {
                    List<FS.HISFC.Models.Order.OutPatient.Order> newOrderList = null;
                    ArrayList recipeNoList = new ArrayList();
                    foreach (FS.HISFC.Models.Order.OutPatient.Order order in orderList)
                    {
                        if (!recipeNoList.Contains(order.ReciptNO))
                        {
                            recipeNoList.Add(order.ReciptNO);
                        }
                    }
                    foreach (string reciptNO in recipeNoList)
                    {
                        newOrderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                        foreach (FS.HISFC.Models.Order.OutPatient.Order order1 in orderList)
                        {
                            if (reciptNO == order1.ReciptNO)
                            {
                                newOrderList.Add(order1);
                            }
                        }

                        orderPrint = new ZDWY.OutPatient.MaterialsBillPrint.ucMaterialsBillPrintIBORN();
                        orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, newOrderList, isPreview);

                        if (isPreview)
                        {
                            ControlList.Add(orderPrint as Control);
                        }
                    }
                }
            }
            else if (billType == EnumOutPatientBill.MedicalReportBill)
            {
                if (currDept.HospitalID == "BELLAIRE")
                {
                    FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RegistionExtend.ucMedicalReportBLEA5 ucMedicalReport = new FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RegistionExtend.ucMedicalReportBLEA5();
                    ucMedicalReport.RegId = regObj.ID;
                    bool isPrint = ucMedicalReport.IsPrint;
                    if (isPrint)
                    {
                        if (isPreview)
                        {
                            ControlList.Add(ucMedicalReport as Control);
                        }
                    }
                }
                else
                {
                    RegistionExtend.ucMedicalReportIBORNA5 ucMedicalReport = new FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.RegistionExtend.ucMedicalReportIBORNA5();
                    ucMedicalReport.RegId = regObj.ID;
                    bool isPrint = ucMedicalReport.IsPrint;
                    if (isPrint)
                    {
                        if (isPreview)
                        {
                            ControlList.Add(ucMedicalReport as Control);
                        }
                    }
                }
            }
        }

        #region IOutPatientPrint 成员
        /// <summary>
        /// 当前科室
        /// </summary>
        Department currDept = new Department();
        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = string.Empty;

        /// <summary>
        /// 错误信息
        /// </summary>
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
        FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
        /// <summary>
        /// 保存后打印
        /// </summary>
        /// <param name="regObj">挂号实体</param>
        /// <param name="reciptDept">开立科室</param>
        /// <param name="reciptDoct">开立医生</param>
        /// <param name="orderPrintList">医嘱列表</param>
        /// <param name="orderSelectList">选择的医嘱列表（目前无用）</param>
        /// <param name="IsReprint">是否补打（目前无用）</param>
        /// <param name="isPreview">是否预览</param>
        /// <param name="printType">打印类型，EnumOutPatientBill枚举</param>
        /// <param name="obj">貌似没用吧</param>
        /// <returns></returns>
        public int OnOutPatientPrint(FS.HISFC.Models.Registration.Register regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, List<FS.HISFC.Models.Order.OutPatient.Order> orderPrintList, List<FS.HISFC.Models.Order.OutPatient.Order> orderSelectList, bool IsReprint, bool isPreview, string printType, object obj)
        {
            #region 规则

            //目前确认的打印规则如下：
            //1、检查申请单：所有检查项目打印，按照单个项目打印单据
            //2、检验申请单，所有检验项目打印，分单规则待李楠跟检验科确认
            //3、治疗单：所有治疗项目打印，不分单，所有项目打印在一张单
            //4、注射单：所有注射、输液用法的药品打印，不分单，控制每张单上面打印的数量
            //5、处方单：按照处方管理办法自动分方并打印单据
            //
            //  处置单先不打印，未提供打印格式
            //6、处置单：除了检查、检验、治疗外的非药品，可能包含手术项目、床位费（留观）、护理费（留观）、材料等
            #endregion

            #region 医嘱分类列表

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> dicDrug = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> dicPACS = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> dicLIS = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> dicTreatment = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> dicInject = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> dicMaterial = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            //重点品种精二
            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> dicImpDrug2 = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            //重点品种全麻
            Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> dicImpDrugM = new Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>>();

            #endregion

            #region 存放单据控件，用于预览

            Dictionary<Int32, List<Control>> dicPreview = new Dictionary<Int32, List<Control>>();

            List<Control> recipeControlList = new List<Control>();

            List<Control> lisControlList = new List<Control>();

            List<Control> lisReceiptControlList = new List<Control>();

            List<Control> pacsControlList = new List<Control>();

            List<Control> treatmentControlList = new List<Control>();

            List<Control> injectControlList = new List<Control>();

            List<Control> materialControlList = new List<Control>();

            List<Control> medicalReportControlList = new List<Control>();

            i = 1;
            #endregion
            currDept = (Department)(((Employee)FS.FrameWork.Management.Connection.Operator).Dept);


            foreach (FS.HISFC.Models.Order.OutPatient.Order order in orderPrintList)
            {

                item = FS.SOC.HISFC.BizProcess.Cache.Pharmacy.GetItem(order.Item.ID);//获取药品的基本信息
                //药品按照处方号分单
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    if (item.Type.ID.ToString() == "O")// {99D2DB54-67B7-4ed1-A59E-97BE8612FE43}
                    {
                        this.AddToList(dicMaterial, order);
                    }
                    else if (item.Quality.ID.ToString() == "Q" && item.SpecialFlag4.ToString() == "13")  //重点品种全麻
                    {
                        this.AddToList(dicImpDrugM, order);
                    }
                    else if (item.Quality.ID.ToString() == "P2" && item.SpecialFlag4.ToString() == "13")  //重点品种精二
                    {
                        this.AddToList(dicImpDrug2, order);
                    }
                    else
                    {

                        this.AddToList(dicDrug, order);

                        if (SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(order.Usage.ID))
                        {
                            //AddToList(dicInject, order);
                            if (dicInject.ContainsKey(order.SeeNO))
                            {
                                dicInject[order.SeeNO].Add(order);
                            }
                            else
                            {
                                List<FS.HISFC.Models.Order.OutPatient.Order> list = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                                list.Add(order);
                                dicInject.Add(order.SeeNO, list);
                            }
                        }
                    }
                }
                //检验不分单
                else if ("UL" == order.Item.SysClass.ID.ToString())
                {
                    this.AddToList(dicLIS, order);
                }
                //检查按照执行科室分单
                //增加按照申请单类型分单{D793A341-AD35-4685-8817-5614217969AD} 2014-12-16 by lixuelong
                else if ("UC" == order.Item.SysClass.ID.ToString())
                {
                    this.AddToList(dicPACS, order);
                }
                //剩余所有项目打印治疗单
                //信息科阿文说，只有治疗项目才打印治疗单，其他的应该打印处置单，但是目前院方不打印
                //治疗不分单
                else //if (order.Item.SysClass.ID.ToString() == "UZ")
                {
                    this.AddToList(dicTreatment, order);
                }
            }

            //根据传递的单据类型打印
            EnumOutPatientBill billType = (EnumOutPatientBill)FS.FrameWork.Function.NConvert.ToInt32(printType);
            switch (billType)
            {
                case EnumOutPatientBill.RecipeBill://普通处方
                    RecipeBill(regObj, dicDrug, new ArrayList(orderPrintList), recipeControlList, isPreview);
                    RecipeBill(regObj, dicImpDrugM, new ArrayList(orderPrintList), recipeControlList, isPreview);//重点品种全麻
                    RecipeBill(regObj, dicImpDrug2, new ArrayList(orderPrintList), recipeControlList, isPreview);//重点品种精二
                    break;
                case EnumOutPatientBill.PacsBill://检查
                    BillPrint(regObj, reciptDept, reciptDoct, dicPACS, isPreview, pacsControlList, billType);
                    break;
                case EnumOutPatientBill.LisBill://检验
                    BillPrint(regObj, reciptDept, reciptDoct, dicLIS, isPreview, lisControlList, billType);
                    break;
                case EnumOutPatientBill.TreatmentBill://治疗单
                    BillPrint(regObj, reciptDept, reciptDoct, dicTreatment, isPreview, treatmentControlList, billType);
                    break;
                case EnumOutPatientBill.InjectBill://注射单
                    this.BillPrint(regObj, reciptDept, reciptDoct, dicInject, isPreview, injectControlList, billType);
                    break;

                case EnumOutPatientBill.MaterialBill://材料单// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                    this.BillPrint(regObj, reciptDept, reciptDoct, dicMaterial, isPreview, materialControlList, billType);//材料单
                    break;

                case EnumOutPatientBill.LisReceiptBill://检验单回执// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                    BillPrint(regObj, reciptDept, reciptDoct, dicLIS, isPreview, lisReceiptControlList, billType);//检验单回执
                    break;

                case EnumOutPatientBill.MedicalReportBill://病历// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                    PrintOnePage(regObj, reciptDept, reciptDoct, null, isPreview, ref medicalReportControlList, billType);//病历
                    break;

                case EnumOutPatientBill.AllBill://全部
                    RecipeBill(regObj, dicDrug, new ArrayList(orderPrintList), recipeControlList, isPreview);//普通处方
                    RecipeBill(regObj, dicImpDrugM, new ArrayList(orderPrintList), recipeControlList, isPreview);//重点品种全麻
                    RecipeBill(regObj, dicImpDrug2, new ArrayList(orderPrintList), recipeControlList, isPreview);//重点品种精二
                    this.BillPrint(regObj, reciptDept, reciptDoct, dicInject, isPreview, injectControlList, EnumOutPatientBill.InjectBill);//注射单

                    BillPrint(regObj, reciptDept, reciptDoct, dicPACS, isPreview, pacsControlList, EnumOutPatientBill.PacsBill);//检查
                    BillPrint(regObj, reciptDept, reciptDoct, dicLIS, isPreview, lisControlList, EnumOutPatientBill.LisBill);//检验
                    BillPrint(regObj, reciptDept, reciptDoct, dicLIS, isPreview, lisReceiptControlList, EnumOutPatientBill.LisReceiptBill);//检验单回执
                    BillPrint(regObj, reciptDept, reciptDoct, dicTreatment, isPreview, treatmentControlList, EnumOutPatientBill.TreatmentBill);//诊疗单
                    this.BillPrint(regObj, reciptDept, reciptDoct, dicMaterial, isPreview, materialControlList, EnumOutPatientBill.MaterialBill);//材料单

                    PrintOnePage(regObj, reciptDept, reciptDoct, null, isPreview, ref medicalReportControlList, EnumOutPatientBill.MedicalReportBill);//病历

                    break;
                default:
                    return 0;
            }

            if (recipeControlList.Count > 0)
            {
                dicPreview.Add((int)EnumOutPatientBill.RecipeBill, recipeControlList);
            }
            if (injectControlList.Count > 0)
            {
                dicPreview.Add((int)EnumOutPatientBill.InjectBill, injectControlList);
            }
            if (pacsControlList.Count > 0)
            {
                dicPreview.Add((int)EnumOutPatientBill.PacsBill, pacsControlList);
            }
            if (lisControlList.Count > 0)
            {
                dicPreview.Add((int)EnumOutPatientBill.LisBill, lisControlList);
            }
            if (lisReceiptControlList.Count > 0)
            {
                dicPreview.Add((int)EnumOutPatientBill.LisReceiptBill, lisReceiptControlList);// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
            }
            if (treatmentControlList.Count > 0)
            {
                dicPreview.Add((int)EnumOutPatientBill.TreatmentBill, treatmentControlList);
            }

            if (materialControlList.Count > 0)
            {
                dicPreview.Add((int)EnumOutPatientBill.MaterialBill, materialControlList);
            }
            if (medicalReportControlList.Count > 0)
            {
                dicPreview.Add((int)EnumOutPatientBill.MedicalReportBill, medicalReportControlList);//病历// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
            }

            if (isPreview && dicPreview.Keys.Count > 0)
            {
                frmPreviewControl frmPreviewControl = new frmPreviewControl(dicPreview);
                frmPreviewControl.ShowPreviewControlDialog();
                frmPreviewControl.ShowDialog();
            }
            return 1;
        }


        /// <summary>
        /// 设置数组
        /// </summary>
        /// <param name="orderDictionary"></param>
        /// <param name="order"></param>
        void AddToList(Dictionary<string, List<FS.HISFC.Models.Order.OutPatient.Order>> orderDictionary, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            List<FS.HISFC.Models.Order.OutPatient.Order> orderList = null;

            string key = order.ReciptNO;
            //增加按照申请单类型分单{D793A341-AD35-4685-8817-5614217969AD} 2014-12-16 by lixuelong
            if (order.Item.SysClass.ID.ToString() == "UC")
            {
                bool IsPscsInUse = ctlMgr.QueryControlerInfo("PSCS01") == "1";
                if (IsPscsInUse)
                {
                    FS.FrameWork.Models.NeuObject obj = constManager.GetConstant("PSCSINFORMED", order.Item.ID);

                    if (obj != null && !string.IsNullOrEmpty(obj.ID))
                    {
                        order.RefundReason = obj.Memo;

                        key = order.ExeDept.ID + "|" + order.Item.Extend1 + obj.Memo + i;

                        if (orderDictionary.ContainsKey(key))
                        {
                            int k = orderDictionary[key].Count;
                            if (k < 2)
                            {

                            }
                            else
                            {
                                i++;
                                key = key.Substring(0, key.Length - 1);
                                key = key + i;
                            }
                        }
                    }
                    else
                    {
                        key = order.ExeDept.ID + "|" + order.Item.Extend1;
                    }

                }
                else
                {
                    //key = order.ExeDept.ID;
                    key = order.ExeDept.ID + "|" + order.Item.Extend1;
                }

            }
            else if (order.Item.SysClass.ID.ToString() == "UL")
            {
                //有些病理科执行的也是检验项目
                //key = order.Item.SysClass.ID.ToString() + order.ExecOper.Dept.ID;
                key = order.ReciptNO;
            }
            //{F42D581C-3406-46a9-973B-4C451F4E8CEB}
            //治疗单项目根据执行科室进行分单
            else if (order.Item.ItemType != EnumItemType.Drug)
            {
                //key = order.SeeNO;
                key = order.ExeDept.ID;
            }

            if (orderDictionary.ContainsKey(key))
            {
                orderDictionary[key].Add(order);
            }
            else
            {
                orderList = new List<FS.HISFC.Models.Order.OutPatient.Order>();
                orderList.Add(order);
                orderDictionary.Add(key, orderList);
            }
        }


        #endregion
    }
}
