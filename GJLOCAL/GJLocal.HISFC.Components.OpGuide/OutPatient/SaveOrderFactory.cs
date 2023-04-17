using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Neusoft.HISFC.Models.Base;
using Neusoft.HISFC.Models.Order.OutPatient;
using Neusoft.SOC.Local.Order.ZhuHai.ZDWY.Common.OrderSplit;
using GJLocal.HISFC.Components.OpGuide.PacsBillPrint;

namespace GJLocal.HISFC.Components.OpGuide
{
    /// <summary>
    /// 医嘱保存后工厂类
    /// </summary>
    public class SaveOrderFactory : Neusoft.HISFC.BizProcess.Interface.Order.IOutPatientPrint
    {
        /// <summary>
        /// 处方打印
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="drugDictionary"></param>
        /// <param name="alOrder"></param>
        /// <param name="ControlList"></param>
        /// <param name="isPreview"></param>
        private void RecipeBill(Neusoft.HISFC.Models.Registration.Register regObj, Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> drugDictionary, System.Collections.ArrayList alOrder, List<Control> ControlList, bool isPreview)
        {
            if (drugDictionary.Keys.Count <= 0)
            {
                return;
            }

            foreach (string recipeNO in drugDictionary.Keys)
            {
                Neusoft.HISFC.Models.Order.OutPatient.Order firstOrder = drugDictionary[recipeNO][0];

                Neusoft.HISFC.BizProcess.Interface.IRecipePrint recipe = null;
                if (firstOrder.Item.SysClass.ID.ToString() == "PCC")
                {
                    recipe = new GJLocal.HISFC.Components.OpGuide.RecipePrint.ucRecipePrintHerb();
                }
                else
                {
                    int speLevl = 1;
                    foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in drugDictionary[recipeNO])
                    {
                        int level = GJLocal.HISFC.Components.OpGuide.OutPatient.Function.GetItemQaulity(order);
                        if (level > speLevl)
                        {
                            speLevl = level;
                        }
                    }

                    //3、毒麻精一；2、精二；1、普通；0、非药品
                    if (speLevl == 3)
                    {
                        recipe = new GJLocal.HISFC.Components.OpGuide.RecipePrint.ucRecipePrintST();
                    }
                    else
                    {
                        recipe = new GJLocal.HISFC.Components.OpGuide.RecipePrint2.ucRecipePrint2();
                    }
                }

                recipe.SetPatientInfo(regObj);
                //已经有列表了 就不需要再用处方号查询了
                //recipe.RecipeNO = recipeNO;
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
        private void BillPrint(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> dicOrder, bool isPreview, List<Control> ControlList, EnumOutPatientBill billType)
        {
            if (dicOrder.Count == 0)
            {
                return;
            }

            int pageRowCount = 0;
            if (billType == EnumOutPatientBill.LisBill)
            {
                pageRowCount = 14;
            }
            else if (billType == EnumOutPatientBill.InjectBill)
            {
                pageRowCount = 9;
            }
            else if (billType == EnumOutPatientBill.TreatmentBill)
            {
                pageRowCount = 9;
            }
            else if (billType == EnumOutPatientBill.PacsBill)
            {
                pageRowCount = 5;
            }

            foreach (string key in dicOrder.Keys)
            {
                if (dicOrder[key].Count <= pageRowCount)
                {
                    PrintOnePage(regObj, reciptDept, reciptDoct, dicOrder[key], isPreview, ref ControlList, billType);
                }
                else
                {
                    List<Neusoft.HISFC.Models.Order.OutPatient.Order> list = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                    int count = 0;
                    foreach (Neusoft.HISFC.Models.Order.OutPatient.Order ord in dicOrder[key])
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

        private void PrintOnePage(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList, bool isPreview, ref List<Control> ControlList, EnumOutPatientBill billType)
        {
            Neusoft.SOC.HISFC.BizProcess.OrderInterface.OutPatientOrder.IOutPatientOrderPrint orderPrint = null;

            if (billType == EnumOutPatientBill.TreatmentBill)
            {
                orderPrint = new GJLocal.HISFC.Components.OpGuide.ClinicsBillPrint.ucClinicsBillPrint();

                orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);

                if (isPreview)
                {
                    ControlList.Add(orderPrint as Control);
                }
            }
            else if (billType == EnumOutPatientBill.InjectBill)
            {
                //orderPrint = new GJLocal.HISFC.Components.OpGuide.InjectBillPrint.ucInjectBillPrint();
                orderPrint = new Neusoft.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.InjectBillPrint.ucInjectBillPrint2();

                orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);

                if (isPreview)
                {
                    ControlList.Add(orderPrint as Control);
                }
            }
            else if (billType == EnumOutPatientBill.LisBill)
            {
                orderPrint = new GJLocal.HISFC.Components.OpGuide.LisBillPrint.ucLisBillPrint();

                orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);

                if (isPreview)
                {
                    ControlList.Add(orderPrint as Control);
                }
            }
            else if (billType == EnumOutPatientBill.PacsBill)
            {
                //先用名字判断检查类型{D81A0FAE-133E-4725-8558-8FC9DCBB1A5E}
                List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderListX = new List<Order>();
                List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderListUnX = new List<Order>();
                foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in orderList)
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
                    orderPrint = new GJLocal.HISFC.Components.OpGuide.PacsBillPrint.ucPacsBillPrintX();
                    orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderListX, isPreview);
                    if (isPreview)
                    {
                        ControlList.Add(orderPrint as Control);
                    }
                }
                if (orderListUnX.Count > 0)
                {
                    orderPrint = new GJLocal.HISFC.Components.OpGuide.PacsBillPrint.ucPacsBillPrint();
                    orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderListUnX, isPreview);
                    if (isPreview)
                    {
                        ControlList.Add(orderPrint as Control);
                    }
                }
                //if (string.IsNullOrEmpty(orderList[0].Item.Extend1))
                //{
                //    orderPrint = new GJLocal.HISFC.Components.OpGuide.PacsBillPrint.ucPacsBillPrint();
                //}
                //else
                //{
                //    orderPrint = new GJLocal.HISFC.Components.OpGuide.PacsBillPrint.ucPacsBillPrintEndoscope();
                //}
                //orderPrint.PrintOutPatientOrderBill(regObj, reciptDept, reciptDoct, orderList, isPreview);

                //if (isPreview)
                //{
                //    ControlList.Add(orderPrint as Control);
                //}
            }
        }

        #region IOutPatientPrint 成员

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
        public int OnOutPatientPrint(Neusoft.HISFC.Models.Registration.Register regObj, Neusoft.FrameWork.Models.NeuObject reciptDept, Neusoft.FrameWork.Models.NeuObject reciptDoct, List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderPrintList, List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderSelectList, bool IsReprint, bool isPreview, string printType, object obj)
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

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> dicDrug = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> dicPACS = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> dicPACSXrade = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> dicLIS = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> dicTreatment = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> dicInject = new Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>>();

            #endregion

            #region 存放单据控件，用于预览

            Dictionary<Int32, List<Control>> dicPreview = new Dictionary<Int32, List<Control>>();

            List<Control> recipeControlList = new List<Control>();

            List<Control> lisControlList = new List<Control>();

            List<Control> pacsControlList = new List<Control>();

            List<Control> treatmentControlList = new List<Control>();

            List<Control> injectControlList = new List<Control>();

            #endregion

            foreach (Neusoft.HISFC.Models.Order.OutPatient.Order order in orderPrintList)
            {
                //药品按照处方号分单
                if (order.Item.ItemType == EnumItemType.Drug)
                {
                    this.AddToList(dicDrug, order);

                    if (Neusoft.SOC.HISFC.BizProcess.Cache.Common.IsInjectUsage(order.Usage.ID))
                    {
                        //AddToList(dicInject, order);
                        if (dicInject.ContainsKey(order.SeeNO))
                        {
                            dicInject[order.SeeNo].Add(order);
                        }
                        else
                        {
                            List<Neusoft.HISFC.Models.Order.OutPatient.Order> list = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                            list.Add(order);
                            dicInject.Add(order.SeeNO, list);
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
                    if (order.Item.Name.Contains("CT")
                        || order.Item.Name.Contains("MR")
                        || order.Item.Name.Contains("RD")
                        || order.Item.Name.Contains("MRI")
                        || order.Item.Name.Contains("X线"))
                    {
                        this.AddToList(dicPACSXrade, order);
                    }
                    else
                    {
                        this.AddToList(dicPACS, order);
                    }
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
            EnumOutPatientBill billType = (EnumOutPatientBill)Neusoft.FrameWork.Function.NConvert.ToInt32(printType);
            switch (billType)
            {
                case EnumOutPatientBill.RecipeBill://普通处方
                    RecipeBill(regObj, dicDrug, new ArrayList(orderPrintList), recipeControlList, isPreview);
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

                case EnumOutPatientBill.AllBill://全部
                    RecipeBill(regObj, dicDrug, new ArrayList(orderPrintList), recipeControlList, isPreview);//普通处方
                    this.BillPrint(regObj, reciptDept, reciptDoct, dicInject, isPreview, injectControlList, EnumOutPatientBill.InjectBill);//注射单

                    BillPrint(regObj, reciptDept, reciptDoct, dicPACS, isPreview, pacsControlList, EnumOutPatientBill.PacsBill);//检查
                    BillPrint(regObj, reciptDept, reciptDoct, dicPACSXrade, isPreview, pacsControlList, EnumOutPatientBill.PacsBill);//检查X线
                    BillPrint(regObj, reciptDept, reciptDoct, dicLIS, isPreview, lisControlList, EnumOutPatientBill.LisBill);//检验
                    BillPrint(regObj, reciptDept, reciptDoct, dicTreatment, isPreview, treatmentControlList, EnumOutPatientBill.TreatmentBill);//诊疗单
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
            if (treatmentControlList.Count > 0)
            {
                dicPreview.Add((int)EnumOutPatientBill.TreatmentBill, treatmentControlList);
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
        void AddToList(Dictionary<string, List<Neusoft.HISFC.Models.Order.OutPatient.Order>> orderDictionary, Neusoft.HISFC.Models.Order.OutPatient.Order order)
        {
            List<Neusoft.HISFC.Models.Order.OutPatient.Order> orderList = null;

            string key = order.ReciptNO;
            //增加按照申请单类型分单{D793A341-AD35-4685-8817-5614217969AD} 2014-12-16 by lixuelong
            if (order.Item.SysClass.ID.ToString() == "UC")
            {
                //key = order.ExeDept.ID;
                key = order.ExeDept.ID + "|" + order.Item.Extend1;
            }
            else if (order.Item.SysClass.ID.ToString() == "UL")
            {
                //有些病理科执行的也是检验项目
                //key = order.Item.SysClass.ID.ToString() + order.ExecOper.Dept.ID;
                key = order.ReciptNO;
            }
            else if (order.Item.ItemType != EnumItemType.Drug)
            {
                key = order.SeeNO;
            }

            if (orderDictionary.ContainsKey(key))
            {
                orderDictionary[key].Add(order);
            }
            else
            {
                orderList = new List<Neusoft.HISFC.Models.Order.OutPatient.Order>();
                orderList.Add(order);
                orderDictionary.Add(key, orderList);
            }
        }


        #endregion
    }
}
