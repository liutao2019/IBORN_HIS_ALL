using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FS.HISFC.Models.Base;
using FS.HISFC.Models.Order.OutPatient;
using FS.SOC.Local.Order.ZhuHai.ZDWY.Common.OrderSplit;
using FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.PacsBillPrint;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient
{
    /// <summary>
    /// 医嘱保存后工厂类
    /// </summary>
    public class SaveOrderFactory : FS.HISFC.BizProcess.Interface.Order.Inpatient.IRecipePrint
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
        /// 检验项目打印(适用超声含有知情同意书)
        /// </summary>
        /// <param name="regObj"></param>
        /// <param name="reciptDept"></param>
        /// <param name="reciptDoct"></param>
        /// <param name="lisDictionary"></param>
        /// <param name="isPreview"></param>
        /// <param name="ControlList"></param>
        private void BillPrint(FS.HISFC.Models.RADT.PatientInfo regObj, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, Dictionary<string, ArrayList> dicOrder, bool isPreview, List<Control> ControlList, EnumOutPatientBill billType)
        {
            if (dicOrder.Count == 0)
            {
                return;
            }

            int pageRowCount = 0;
            if (billType == EnumOutPatientBill.LisBill)
            {
                pageRowCount = 13;
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

            else if (billType == EnumOutPatientBill.LisReceiptBill)
            {
                pageRowCount = 10;
            }

            else if (billType == EnumOutPatientBill.MedicalReportBill)
            {
                pageRowCount = 10;
            }

            foreach (string key in dicOrder.Keys)
            {
                if (dicOrder[key].Count <= pageRowCount)
                {
                    PrintOnePages(regObj, reciptDept, reciptDoct, dicOrder[key], isPreview, ref ControlList, billType);
                }
                else
                {
                    //List<FS.HISFC.Models.Order.Inpatient.Order> list = new List<FS.HISFC.Models.Order.Inpatient.Order>();
                    ArrayList list = new ArrayList();
                    int count = 0;
                    foreach (FS.HISFC.Models.Order.Inpatient.Order ord in dicOrder[key])
                    {
                        list.Add(ord);
                        count++;
                        if (list.Count == pageRowCount
                            || count == dicOrder[key].Count)
                        {
                            PrintOnePages(regObj, reciptDept, reciptDoct, list, isPreview, ref ControlList, billType);
                            list.Clear();
                        }
                    }
                }
            }
        }


        private void PrintOnePages(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList orderList, bool isPreview, ref List<Control> ControlList, EnumOutPatientBill billType)
        {
            FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint orderPrint = null;


            if (billType == EnumOutPatientBill.PacsBill)
            {
                if (orderList != null && orderList.Count > 0)
                {
                    FS.HISFC.Models.Order.Inpatient.Order ord = orderList[0] as FS.HISFC.Models.Order.Inpatient.Order;

                    if (!string.IsNullOrEmpty(ord.RefundReason))
                    {
                        orderPrint = new ZDWY.InPatient.PacsBillPrint.ucPacsInformedBillPrintIBORNA4();

                        orderPrint = new ZDWY.InPatient.PacsBillPrint.ucPacsInformedBillPrintIBORNA4();
                        orderPrint.PrintInPatientOrderBill(patientInfo, "", reciptDept, reciptDoct, orderList, false);
                        //orderPrint.PrintInPatientOrderBill(patientInfo, "", reciptDept, reciptDoct, orderList, false);
                        ControlList.Add(orderPrint as Control);
                    }

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
        private void BillPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList dicOrder, bool isPreview, List<Control> ControlList, EnumOutPatientBill billType)
        {
            if (dicOrder.Count == 0)
            {
                return;
            }

            int pageRowCount = 0;
            if (billType == EnumOutPatientBill.PacsBill)
            {
                pageRowCount = 7;
            }
            else if (billType == EnumOutPatientBill.LisBill)
            {
                pageRowCount = 5;
            }
            else if (billType == EnumOutPatientBill.LisReceiptBill)// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
            {
                pageRowCount = 10;
            }

            //foreach (FS.HISFC.Models.Order.Inpatient.Order order in dicOrder)
            //{


            if (billType == EnumOutPatientBill.LisReceiptBill)
            {
                if (dicOrder.Count <= pageRowCount)
                {
                    PrintOnePage(patientInfo, reciptDept, reciptDoct, dicOrder, isPreview, ref ControlList, billType);
                }
                else
                {
                    ArrayList list = new ArrayList();

                    int count = 0;
                    foreach (FS.HISFC.Models.Order.Inpatient.Order ord in dicOrder)
                    {
                        list.Add(ord);
                        count++;
                        if (list.Count == pageRowCount
                            || count == dicOrder.Count)
                        {
                            PrintOnePage(patientInfo, reciptDept, reciptDoct, list, isPreview, ref ControlList, billType);
                            list.Clear();
                        }
                    }
                }
            }
            else
            {
                ArrayList newOrderList = null;
                ArrayList recipeNoList = new ArrayList();
                foreach (FS.HISFC.Models.Order.Inpatient.Order order in dicOrder)
                {
                    if (!recipeNoList.Contains(order.ExeDept.ID))
                    {
                        recipeNoList.Add(order.ExeDept.ID);
                    }
                }
                foreach (string reciptNO in recipeNoList)
                {
                    newOrderList = new ArrayList();
                    foreach (FS.HISFC.Models.Order.Inpatient.Order order1 in dicOrder)
                    {
                        if (reciptNO == order1.ExeDept.ID)
                        {
                            newOrderList.Add(order1);
                        }
                    }
                    if (newOrderList.Count <= pageRowCount)
                    {
                        PrintOnePage(patientInfo, reciptDept, reciptDoct, newOrderList, isPreview, ref ControlList, billType);
                    }
                    else
                    {
                        ArrayList list = new ArrayList();

                        int count = 0;
                        foreach (FS.HISFC.Models.Order.Inpatient.Order ord in newOrderList)
                        {
                            list.Add(ord);
                            count++;
                            if (list.Count == pageRowCount
                                || count == newOrderList.Count)
                            {
                                PrintOnePage(patientInfo, reciptDept, reciptDoct, list, isPreview, ref ControlList, billType);
                                list.Clear();
                            }
                        }
                    }
                }
            }
        }

        private void PrintOnePage(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList orderList, bool isPreview, ref List<Control> ControlList, EnumOutPatientBill billType)
        {
            FS.SOC.HISFC.BizProcess.OrderInterface.InPatientOrder.IInPatientOrderPrint orderPrint = null;
            if (billType == EnumOutPatientBill.LisBill)
            {


                //ArrayList newOrderList = null;
                //ArrayList recipeNoList = new ArrayList();
                //foreach (FS.HISFC.Models.Order.Inpatient.Order order in orderList)
                //{
                //    if (!recipeNoList.Contains(order.ExeDept.ID))
                //    {
                //        recipeNoList.Add(order.ExeDept.ID);
                //    }
                //}
                //foreach (string reciptNO in recipeNoList)
                //{
                //    newOrderList = new ArrayList();
                //    foreach (FS.HISFC.Models.Order.Inpatient.Order order1 in orderList)
                //    {
                //        if (reciptNO == order1.ExeDept.ID)
                //        {
                //            newOrderList.Add(order1);
                //        }
                //    }

                //    orderPrint = new ZDWY.InPatient.LisBillPrint.ucLisBillPrintIBORN();
                //    orderPrint.PrintInPatientOrderBill(patientInfo, "", reciptDept, reciptDoct, newOrderList, false);

                //    if (true)
                //    {
                //        ControlList.Add(orderPrint as Control);
                //    }
                //}

                orderPrint = new ZDWY.InPatient.LisBillPrint.ucLisBillPrintIBORN();
                orderPrint.PrintInPatientOrderBill(patientInfo, "", reciptDept, reciptDoct, orderList, false);

                if (true)
                {
                    ControlList.Add(orderPrint as Control);
                }

            }

            else if (billType == EnumOutPatientBill.LisReceiptBill)
            {
                orderPrint = new ZDWY.InPatient.LisBillPrint.ucLisReceiptBillPrintIBORN();
                orderPrint.PrintInPatientOrderBill(patientInfo, "", reciptDept, reciptDoct, orderList, false);

                if (true)
                {
                    ControlList.Add(orderPrint as Control);
                }

            }
            else if (billType == EnumOutPatientBill.PacsBill)
            {


                //ArrayList newOrderList = null;
                //ArrayList recipeNoList = new ArrayList();
                //foreach (FS.HISFC.Models.Order.Inpatient.Order order in orderList)
                //{
                //    if (!recipeNoList.Contains(order.ExeDept.ID))
                //    {
                //        recipeNoList.Add(order.ExeDept.ID);
                //    }
                //}
                //foreach (string reciptNO in recipeNoList)
                //{
                //    newOrderList = new ArrayList();
                //    foreach (FS.HISFC.Models.Order.Inpatient.Order order1 in orderList)
                //    {
                //        if (reciptNO == order1.ExeDept.ID)
                //        {
                //            newOrderList.Add(order1);
                //        }
                //    }

                //    orderPrint = new ZDWY.InPatient.PacsBillPrint.ucPacsBillPrintIBORN();
                //    orderPrint.PrintInPatientOrderBill(patientInfo, "", reciptDept, reciptDoct, newOrderList, false);

                //    if (true)
                //    {
                //        ControlList.Add(orderPrint as Control);
                //    }
                //}

                orderPrint = new ZDWY.InPatient.PacsBillPrint.ucPacsBillPrintIBORNA4();
                orderPrint.PrintInPatientOrderBill(patientInfo, "", reciptDept, reciptDoct, orderList, false);

                if (true)
                {
                    ControlList.Add(orderPrint as Control);
                }

            }
        }

        #region IInPatientPrint 成员
        /// <summary>
        /// 当前科室
        /// </summary>
        Department currDept = new Department();
        /// <summary>
        /// 错误信息
        /// </summary>
        string errInfo = string.Empty;

        FS.HISFC.BizLogic.Order.Order orderMgr = new FS.HISFC.BizLogic.Order.Order();
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
        public void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (patientInfo == null)
            {
                return;
            }
            ArrayList orderList = new ArrayList();
            orderList = orderMgr.QueryOrderByState(patientInfo.ID, "0");
            if (orderList == null || orderList.Count <= 0)
            {
                return;
            }
            FS.FrameWork.Models.NeuObject reciptDept = new FS.FrameWork.Models.NeuObject();
            FS.FrameWork.Models.NeuObject reciptDoct = new FS.FrameWork.Models.NeuObject();
            reciptDept.ID = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID;
            reciptDept.Name = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.Name;
            reciptDoct = FS.FrameWork.Management.Connection.Operator;
            this.OnInPatientPrint(patientInfo, reciptDept, reciptDoct, orderList, null, false, true, "ALL", "");
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
        public int OnInPatientPrint(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.FrameWork.Models.NeuObject reciptDept, FS.FrameWork.Models.NeuObject reciptDoct, ArrayList orderPrintList, ArrayList orderSelectList, bool IsReprint, bool isPreview, string printType, object obj)
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

            Dictionary<Int32, List<Control>> dicPreview = new Dictionary<Int32, List<Control>>();

            Dictionary<string, ArrayList> dicTempPACS = new Dictionary<string, ArrayList>();


            ArrayList dicPACS = new ArrayList();

            ArrayList dicLIS = new ArrayList();

            ArrayList dicReturnLIS = new ArrayList();
            #endregion

            #region 存放单据控件，用于预览

            List<Control> lisControlList = new List<Control>();

            List<Control> lisReceiptControlList = new List<Control>();

            List<Control> pacsControlList = new List<Control>();

            List<Control> pacsInformControlList = new List<Control>();

            i = 1;
            #endregion
            currDept = (Department)(((Employee)FS.FrameWork.Management.Connection.Operator).Dept);
            foreach (FS.HISFC.Models.Order.Inpatient.Order order in orderPrintList)
            {
                if ("UL" == order.Item.SysClass.ID.ToString())
                {
                    dicLIS.Add(order);
                    dicReturnLIS.Add(order);
                }
                else if ("UC" == order.Item.SysClass.ID.ToString())
                {
                    bool IsPscsInUse = ctlMgr.QueryControlerInfo("PSCS01") == "1";
                    if (IsPscsInUse)
                    {
                        FS.FrameWork.Models.NeuObject objt = constManager.GetConstant("PSCSINFORMED", order.Item.ID);
                        if (objt != null && !string.IsNullOrEmpty(objt.ID))
                        {
                            this.AddToList(dicTempPACS, order);
                        }
                        else
                        {
                            dicPACS.Add(order);
                        }
                    }
                    else
                    {
                        dicPACS.Add(order);
                    }



                }
            }
            //根据传递的单据类型打印
            EnumOutPatientBill billType = (EnumOutPatientBill)FS.FrameWork.Function.NConvert.ToInt32(printType);
            switch (billType)
            {
                case EnumOutPatientBill.PacsBill://检查
                    BillPrint(patientInfo, reciptDept, reciptDoct, dicPACS, isPreview, pacsControlList, billType);
                    BillPrint(patientInfo, reciptDept, reciptDoct, dicTempPACS, isPreview, pacsInformControlList, billType);
                    break;
                case EnumOutPatientBill.LisBill://检验
                    BillPrint(patientInfo, reciptDept, reciptDoct, dicLIS, isPreview, lisControlList, billType);
                    break;
                case EnumOutPatientBill.LisReceiptBill://检验单回执// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
                    BillPrint(patientInfo, reciptDept, reciptDoct, dicLIS, isPreview, lisReceiptControlList, billType);//检验单回执
                    break;


                case EnumOutPatientBill.AllBill://全部
                    BillPrint(patientInfo, reciptDept, reciptDoct, dicPACS, isPreview, pacsControlList, EnumOutPatientBill.PacsBill);//检查
                    BillPrint(patientInfo, reciptDept, reciptDoct, dicLIS, isPreview, lisControlList, EnumOutPatientBill.LisBill);//检验
                    BillPrint(patientInfo, reciptDept, reciptDoct, dicLIS, isPreview, lisReceiptControlList, EnumOutPatientBill.LisReceiptBill);//检验单回执
                    BillPrint(patientInfo, reciptDept, reciptDoct, dicTempPACS, isPreview, pacsInformControlList, EnumOutPatientBill.PacsBill);
                    break;
                default:
                    return 0;
            }



            if (pacsControlList.Count > 0)
            {
                if (pacsInformControlList.Count > 0)
                {
                    pacsControlList.AddRange(pacsInformControlList);
                }
                dicPreview.Add((int)EnumOutPatientBill.PacsBill, pacsControlList);
            }
            else
            {
                if (pacsInformControlList.Count > 0)
                {
                    dicPreview.Add((int)EnumOutPatientBill.PacsBill, pacsInformControlList);
                }
            }

            

            //暂时不打
            //if (lisControlList.Count > 0)
            //{
            //    dicPreview.Add((int)EnumOutPatientBill.LisBill, lisControlList);
            //}
            //if (lisReceiptControlList.Count > 0)
            //{
            //    dicPreview.Add((int)EnumOutPatientBill.LisReceiptBill, lisReceiptControlList);// {5A6C578A-D565-42c8-B3FA-B4A52D1FABFB}
            //}

            if (isPreview && dicPreview.Keys.Count > 0)
            {
                FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.frmPreviewControl frmPreviewControl = new FS.SOC.Local.Order.ZhuHai.ZDWY.OutPatient.frmPreviewControl(dicPreview);
                frmPreviewControl.ShowPreviewControlDialog();
                frmPreviewControl.ShowDialog();
            }
            return 1;
        }


        void AddToList(Dictionary<string, ArrayList> orderDictionary, FS.HISFC.Models.Order.Inpatient.Order order)
        {
            //List<FS.HISFC.Models.Order.Inpatient.Order> orderList = null;

            ArrayList orderList = null;

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
                            if (k < 5)
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
                orderList = new ArrayList();
                orderList.Add(order);
                orderDictionary.Add(key, orderList);
            }

        }


        #endregion
    }
}
