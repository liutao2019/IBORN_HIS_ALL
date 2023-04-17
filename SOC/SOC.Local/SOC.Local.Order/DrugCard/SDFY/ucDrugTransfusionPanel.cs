using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.SOC.Local.Order.DrugCard.SDFY
{
    /// <summary>
    /// [功能描述: 输液卡控件]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class ucDrugTransfusionPanel : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.IPrintTransFusion
    {
        /// <summary>
        /// 
        /// </summary>
        public ucDrugTransfusionPanel()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 瓶贴卡
        /// </summary>
        InfusionLabel.ucInfusionLabel ucLabel = new FS.SOC.Local.Order.DrugCard.SDFY.InfusionLabel.ucInfusionLabel();

        #region IPrintTransFusion 成员
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        ArrayList curValues = null; //当前显示的数据
        bool isPrint = false;
        List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;
        DateTime dt1;
        DateTime dt2;
        string usage = "";
        string speOrderType = "";

        #region {47D5BD74-2263-4275-9CF8-18DD973E31E7}
        /// <summary>
        /// 参数业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlManagemnt = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        /// <summary>
        /// 获取需要打印的瓶签
        /// </summary>
        /// <returns></returns>
        private ArrayList GetValidControl()
        {
            ArrayList al = new ArrayList();

            FS.SOC.Local.Order.DrugCard.SDFY.InfusionLabel.ucInfusionLabel cTemp = null;
            foreach (System.Windows.Forms.Control c in this.pnCard.Controls)
            {
                if (c.GetType().ToString() == "FS.SOC.Local.Order.DrugCard.SDFY.InfusionLabel.ucInfusionLabel")
                {
                    cTemp = c as FS.SOC.Local.Order.DrugCard.SDFY.InfusionLabel.ucInfusionLabel;
                    if (cTemp != null && cTemp.Tag != null && !cTemp.IsSelected)
                    {
                        ArrayList alTemp = cTemp.Tag as ArrayList;
                        if (alTemp.Count > 0)
                        {
                            for (int i = 0; i < alTemp.Count; i++)
                            {
                                (alTemp[i] as FS.HISFC.Models.Order.ExecOrder).Order.Patient.Pact.UserCode = "NONE";
                            }

                            al.Add(alTemp);
                        }
                    }
                }
            }
            return al;
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            try
            {
                FS.HISFC.Components.Common.Classes.Function.GetPageSize("Nurse3", ref print);

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //print.IsDataAutoExtend = true;

                #region {47D5BD74-2263-4275-9CF8-18DD973E31E7}

                //FS.HISFC.PC.MNS.Implement.OrderExcBill ppcExecBillMgr = null;
                //bool isHavePccExecBill = this.controlManagemnt.GetControlParam<bool>("200211", false, false);
                //if (isHavePccExecBill)
                //{
                //    ppcExecBillMgr = new FS.HISFC.PC.MNS.Implement.OrderExcBill();
                //}

                #endregion

                #region 打印瓶贴卡
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //p.PrintPreview(5, 5, this.pnCard);
                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize();
                pSize = pgMgr.GetPageSize("Nurse5");
                if (pSize != null)
                {
                    p.SetPageSize(pSize);
                }
                ArrayList alTemp = this.GetValidControl();
                ucLabel.SetPrint();
                FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alTemp, ucLabel, this.pnCard, new Size(850, 550), 1);

                //隐藏打印复选框
                FS.SOC.Local.Order.DrugCard.SDFY.InfusionLabel.ucInfusionLabel cTemp = null;
                foreach (System.Windows.Forms.Control c in this.pnCard.Controls)
                {
                    if (c.GetType().ToString() == "FS.SOC.Local.Order.DrugCard.SDFY.InfusionLabel.ucInfusionLabel")
                    {
                        cTemp = c as FS.SOC.Local.Order.DrugCard.SDFY.InfusionLabel.ucInfusionLabel;
                        if (cTemp != null && cTemp.Tag != null)
                        {
                            cTemp.SetPrint();
                        }
                    }
                }

                p.PrintPage(0, 0, this.pnCard);
                //p.PrintPreview(0, 0, this.pnCard);

                #endregion

                #region 更新已经打印标记

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                for (int i = 0; i < alTemp.Count; i++)
                {
                    ArrayList al = alTemp[i] as ArrayList;
                    foreach (FS.HISFC.Models.Order.ExecOrder obj in al)
                    {
                        if (this.orderManager.UpdateTransfusionPrinted(obj.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("更新打印标记失败!" + orderManager.Err);
                            return;
                        }

                        #region {47D5BD74-2263-4275-9CF8-18DD973E31E7}
                        //if (isHavePccExecBill)
                        //{
                        //    FS.HISFC.PC.Object.ExcBill ppcExecBill = new FS.HISFC.PC.Object.ExcBill();

                        //    ppcExecBill.BarCode = obj.Order.Combo.ID + obj.DateUse.ToString("yyMMddHHmm");
                        //    string ppcExecType = ppcExecBillMgr.QueryExcTypeByUseName(obj.Order.Usage.Name);
                        //    ppcExecBill.ExeType = ppcExecType;
                        //    ppcExecBill.ExecSqn = obj.ID;
                        //    ppcExecBill.DoseUnit = obj.Order.DoseUnit;
                        //    ppcExecBill.DoseOnce = obj.Order.DoseOnce;
                        //    ppcExecBill.ExecName = obj.Order.Item.Name;
                        //    ppcExecBill.InpatientNo = obj.Order.Patient.ID;
                        //    ppcExecBill.QtyTot = obj.Order.Qty;
                        //    ppcExecBill.FqName = obj.Order.Frequency.Name;
                        //    ppcExecBill.UseName = obj.Order.Usage.Name;
                        //    ppcExecBill.UseTime = obj.DateUse;
                        //    ppcExecBill.GroupNo = obj.Order.Combo.ID;

                        //    int ippcReturn = ppcExecBillMgr.InsertExcBill(ppcExecBill);
                        //}
                        #endregion

                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                #endregion

                this.Query(myPatients, usage, dt1, dt2, isPrint);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }

        /// <summary>
        /// 打印
        /// </summary>
        public void PrintPanel()
        {
            try
            {
                FS.HISFC.Components.Common.Classes.Function.GetPageSize("Nurse3", ref print);

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //p.PrintPreview(5, 5, this.pnCard);
                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize();
                pSize = pgMgr.GetPageSize("Nurse5");
                if (pSize != null)
                {
                    p.SetPageSize(pSize);
                }
                p.PrintPage(0, 0, this.pnCard);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        public void PrintSet()
        {
            print.ShowPrintPageDialog();
            this.Print();
        }

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtTime, bool isPrinted)
        {
            return;
        }

        private FS.FrameWork.Public.ObjectHelper frequencyHelper = null;

        /// <summary>
        /// 临时医嘱是否按照频次分开打印瓶签
        /// </summary>
        private int isSplitLZOrders = -1;

        /// <summary>
        /// 按照频次拆分临时医嘱
        /// </summary>
        /// <param name="al"></param>
        private ArrayList SplitLZOrders(ArrayList al)
        {
            if (this.isSplitLZOrders == -1)
            {
                FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                isSplitLZOrders = controlMgr.GetControlParam<int>("HNZY11", true, 0);
            }
            if (this.isSplitLZOrders != 1)
            {
                return al;
            }

            ArrayList alNew = new ArrayList();
            FS.FrameWork.Models.NeuObject trueFrequency = null;
            FS.HISFC.Models.Order.ExecOrder execOrder = null;
            for (int i = 0; i < al.Count; i++)
            {
                ArrayList alTemp = al[i] as ArrayList;
                alNew.Add(alTemp);
                execOrder = alTemp[0] as FS.HISFC.Models.Order.ExecOrder;
                //临嘱
                if (!execOrder.Order.OrderType.IsDecompose)
                {
                    if (this.frequencyHelper == null || this.frequencyHelper.ArrayObject.Count == 0)
                    {
                        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                        this.frequencyHelper = new FS.FrameWork.Public.ObjectHelper(interMgr.QuereyFrequencyList());
                    }
                    trueFrequency = this.frequencyHelper.GetObjectFromID(execOrder.Order.Frequency.ID);
                    if (trueFrequency != null)
                    {
                        execOrder.Order.Frequency = trueFrequency as FS.HISFC.Models.Order.Frequency;
                    }
                    else
                    {
                        continue;
                    }
                    //执行时间
                    for (int j = 0; j <= execOrder.Order.Frequency.Times.GetUpperBound(0); j++)
                    {
                        DateTime dt = new DateTime();
                        try
                        {
                            dt = FS.FrameWork.Function.NConvert.ToDateTime(execOrder.Order.Frequency.Times[j]);
                            if (j == 0)
                            {
                                execOrder.DateUse = execOrder.DateUse.Date.AddHours(dt.Hour);
                            }
                        }
                        catch
                        {
                        }
                        if (dt.GetType().ToString() != "System.DateTime")
                        {
                            //return -1;
                        }
                        else
                        {
                            if (j != 0)
                            {
                                ArrayList alAdd = new ArrayList();
                                FS.HISFC.Models.Order.ExecOrder execOrderAdd = null;
                                for (int k = 0; k < alTemp.Count; k++)
                                {
                                    execOrderAdd = (alTemp[k] as FS.HISFC.Models.Order.ExecOrder).Clone();
                                    if (k == 0)
                                    {
                                        execOrderAdd.DateUse = execOrderAdd.DateUse.Date.AddHours(dt.Hour);
                                    }
                                    alAdd.Add(execOrderAdd);
                                }
                                alNew.Add(alAdd);
                            }
                        }
                    }
                }
            }
            return alNew;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usageCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isPrinted"></param>
        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted)
        {
            ArrayList al = new ArrayList();//获得的全部信息

            for (int i = 0; i < patients.Count; i++)
            {
                ArrayList alOrder = orderManager.QueryOrderExec(((FS.FrameWork.Models.NeuObject)patients[i]).ID, dtBegin, dtEnd, usageCode, isPrinted);
                if (alOrder == null) //为提示出错 
                {
                    MessageBox.Show(orderManager.Err);
                    return;
                }

                string strDiff = "";//分组用
                ArrayList alObjects = null;//分组用
                int iComboNum = 0;
                int MaxComboNum = 70;        //每页最多5个药
                int iPage = 0;//分页用

                FS.HISFC.Models.Order.Inpatient.Order orderObj = new FS.HISFC.Models.Order.Inpatient.Order();
                for (int j = 0; j < alOrder.Count; j++)//医嘱信息
                {
                    //赋全患者信息
                    ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Patient = ((FS.HISFC.Models.RADT.PatientInfo)patients[i]).Clone();//患者信息付数值

                    #region 停止医嘱后的瓶签不打印
                    orderObj = this.orderManager.QueryOneOrder(((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.ID);
                    if (orderObj == null)
                    {
                        MessageBox.Show("查询医嘱出错：" + this.orderManager.Err);
                        return;
                    }

                    //重整、停止医嘱
                    if ("3,4".Contains(orderObj.Status.ToString()))
                    {
                        if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse > orderObj.DCOper.OperTime)
                        {
                            continue;
                        }
                    }

                    //未收费的，不打印
                    if (!((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).IsExec)
                    {
                        continue;
                    }

                    #endregion

                    //按组合分组给输液卡对象
                    if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Combo.ID != "0"
                        && ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Combo.ID != "")
                    {
                        if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.OrderType.IsDecompose)
                        {
                            execOrderID = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Combo.ID + ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse.ToString();
                        }
                        else
                        {
                            execOrderID = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Combo.ID;
                        }

                        if (strDiff != execOrderID)
                        {
                            iComboNum = 0;
                            if (alObjects != null)
                            {
                                al.Add(alObjects);//不同的
                            }
                            alObjects = new ArrayList();

                            strDiff = execOrderID;
                            alObjects.Add(alOrder[j]);

                            iPage = 1;//分页页码第一页

                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).User01 = iPage.ToString(); //存页码
                        }
                        else//相同的
                        {
                            iComboNum++;
                            if ((iComboNum % MaxComboNum) == 0)
                            {
                                if (alObjects != null)
                                {
                                    al.Add(alObjects);//分页
                                }
                                alObjects = new ArrayList();
                            }
                            iPage = (int)(iComboNum / MaxComboNum) + 1;//分页页码

                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).User01 = iPage.ToString();

                            alObjects.Add(alOrder[j]);
                        }
                    }
                    else//没有组合号的
                    {
                        if (alObjects != null)
                        {
                            al.Add(alObjects);
                        }
                        alObjects = new ArrayList();
                        alObjects.Add(alOrder[j]);
                        iPage = 1;//分页页码第一页

                        ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).User01 = iPage.ToString();
                    }
                }
                if (alObjects != null)
                {
                    al.Add(alObjects);
                }
            }

            ArrayList alNew = this.SplitLZOrders(al);
            al = alNew;

            int iPages = 1;
            for (int i = al.Count - 1; i >= 0; i--)
            {
                ArrayList alTemp = al[i] as ArrayList;
                FS.HISFC.Models.Order.ExecOrder order = alTemp[0] as FS.HISFC.Models.Order.ExecOrder;
                if (FS.FrameWork.Function.NConvert.ToInt32(order.User01) > 1)
                {
                    if (iPages > 1)//已经有最高页了
                    {
                        order.User02 = iPages.ToString();
                    }
                    else//新的
                    {
                        iPages = FS.FrameWork.Function.NConvert.ToInt32(order.User01);//获得最高页
                        order.User02 = iPages.ToString();
                    }
                }
                else if (FS.FrameWork.Function.NConvert.ToInt32(order.User01) <= 1) //到第一页了
                {
                    order.User02 = iPages.ToString();
                    iPages = 1;
                }
            }

            this.SetValues(al);

            this.isPrint = isPrinted;//是否补打
            usage = usageCode;
            dt1 = dtBegin;
            dt2 = dtEnd;
            myPatients = patients;

            if (this.tabControl1.TabPages.Contains(tpBill))
            {
                this.tabControl1.TabPages.Remove(tpBill);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usageCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isPrinted"></param>
        /// <param name="orderType">All 全部 LONG 长期 SHORT 临时</param>
        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted,string orderType)
        {
            ArrayList al = new ArrayList();//获得的全部信息

            for (int i = 0; i < patients.Count; i++)
            {
                ArrayList alOrder = orderManager.QueryOrderExec(((FS.FrameWork.Models.NeuObject)patients[i]).ID, dtBegin, dtEnd, usageCode, isPrinted);

                if (alOrder == null) //为提示出错 
                {
                    MessageBox.Show(orderManager.Err);
                    return;
                }
                else
                {
                    ArrayList alFinal = new ArrayList();

                    ArrayList alOrderNew = new ArrayList();

                    //不设置，默认查看全部
                    if (this.speOrderType.Length <= 0)
                    {
                        foreach (FS.HISFC.Models.Order.ExecOrder order in alOrder)
                        {
                            alOrderNew.Add(order);
                        }
                    }
                    else
                    {
                        //只要包含一个，就认为可以看到
                        string[] speStr = this.speOrderType.Split(',');

                        foreach (FS.HISFC.Models.Order.ExecOrder order in alOrder)
                        {
                            bool isAdd = false;

                            foreach (string s in speStr)
                            {
                                if (order.Order.SpeOrderType == "" || order.Order.SpeOrderType.IndexOf(s) >= 0)
                                {
                                    isAdd = true;
                                    break;
                                }
                            }

                            if (isAdd)
                                alOrderNew.Add(order);
                        }
                    }

                    alOrder = alOrderNew;

                    foreach (FS.HISFC.Models.Order.ExecOrder exe in alOrder)
                    {
                        if (orderType == "ALL")
                        {
                            alFinal.Add(exe);
                        }

                        if (orderType == "LONG")
                        {
                            if (exe.Order.OrderType.IsDecompose)
                            {
                                alFinal.Add(exe);
                            }
                        }

                        if (orderType == "SHORT")
                        {
                            if (!exe.Order.OrderType.IsDecompose)
                            {
                                alFinal.Add(exe);
                            }
                        }
                    }

                    alOrder = alFinal;
                }

                string strDiff = "";//分组用
                ArrayList alObjects = null;//分组用
                int iComboNum = 0;
                int MaxComboNum = 70;        //每页最多5个药
                int iPage = 0;//分页用

                FS.HISFC.Models.Order.Inpatient.Order orderObj = new FS.HISFC.Models.Order.Inpatient.Order();

                FS.HISFC.Models.Order.ExecOrder execOrder = new FS.HISFC.Models.Order.ExecOrder();
                for (int j = 0; j < alOrder.Count; j++)//医嘱信息
                {
                    execOrder = (FS.HISFC.Models.Order.ExecOrder)alOrder[j] as FS.HISFC.Models.Order.ExecOrder;
                    //赋全患者信息
                    execOrder.Order.Patient = ((FS.HISFC.Models.RADT.PatientInfo)patients[i]).Clone();//患者信息付数值

                    #region 停止医嘱后的瓶签不打印
                    orderObj = this.orderManager.QueryOneOrder(execOrder.Order.ID);
                    if (orderObj == null)
                    {
                        MessageBox.Show("查询医嘱出错：" + this.orderManager.Err);
                        return;
                    }

                    execOrder.Order.SortID = orderObj.SortID;

                    if (!execOrder.IsExec)
                    {
                        continue;
                    }

                    //重整、停止医嘱
                    if ("3,4".Contains(orderObj.Status.ToString()))
                    {
                        if (execOrder.DateUse > orderObj.DCOper.OperTime)
                        {
                            continue;
                        }
                    }

                    #endregion

                    //按组合分组给输液卡对象
                    if (execOrder.Order.Combo.ID != "0"
                        && execOrder.Order.Combo.ID != "")
                    {
                        if (execOrder.Order.OrderType.IsDecompose)
                        {
                            execOrderID = execOrder.Order.Combo.ID + execOrder.DateUse.ToString();
                        }
                        else
                        {
                            execOrderID = execOrder.Order.Combo.ID;
                        }

                        if (strDiff != execOrderID)
                        {
                            iComboNum = 0;
                            if (alObjects != null)
                            {
                                al.Add(alObjects);//不同的
                            }
                            alObjects = new ArrayList();

                            strDiff = execOrderID;
                            alObjects.Add(alOrder[j]);

                            iPage = 1;//分页页码第一页

                            execOrder.User01 = iPage.ToString(); //存页码
                        }
                        else//相同的
                        {
                            iComboNum++;
                            if ((iComboNum % MaxComboNum) == 0)
                            {
                                if (alObjects != null)
                                {
                                    al.Add(alObjects);//分页
                                }
                                alObjects = new ArrayList();
                            }
                            iPage = (int)(iComboNum / MaxComboNum) + 1;//分页页码

                            execOrder.User01 = iPage.ToString();

                            alObjects.Add(alOrder[j]);
                        }
                    }
                    else//没有组合号的
                    {
                        if (alObjects != null)
                        {
                            al.Add(alObjects);
                        }
                        alObjects = new ArrayList();
                        alObjects.Add(alOrder[j]);
                        iPage = 1;//分页页码第一页

                        execOrder.User01 = iPage.ToString();
                    }
                }
                if (alObjects != null)
                {
                    al.Add(alObjects);
                }
            }

            ArrayList alNew = this.SplitLZOrders(al);
            al = alNew;

            int iPages = 1;
            for (int i = al.Count - 1; i >= 0; i--)
            {
                ArrayList alTemp = al[i] as ArrayList;
                FS.HISFC.Models.Order.ExecOrder order = alTemp[0] as FS.HISFC.Models.Order.ExecOrder;
                if (FS.FrameWork.Function.NConvert.ToInt32(order.User01) > 1)
                {
                    if (iPages > 1)//已经有最高页了
                    {
                        order.User02 = iPages.ToString();
                    }
                    else//新的
                    {
                        iPages = FS.FrameWork.Function.NConvert.ToInt32(order.User01);//获得最高页
                        order.User02 = iPages.ToString();
                    }
                }
                else if (FS.FrameWork.Function.NConvert.ToInt32(order.User01) <= 1) //到第一页了
                {
                    order.User02 = iPages.ToString();
                    iPages = 1;
                }
            }

            if (this.tabControl1.TabPages.Contains(tpBill))
            {
                this.tabControl1.TabPages.Remove(tpBill);
            }

            this.SetValues(al);

            this.isPrint = isPrinted;//是否补打
            usage = usageCode;
            dt1 = dtBegin;
            dt2 = dtEnd;
            myPatients = patients;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usageCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isPrinted"></param>
        /// <param name="orderType">All 全部 LONG 长期 SHORT 临时</param>
        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin,DateTime dtEnd, bool isPrinted, string orderType,bool isFirst)
        {
            ArrayList al = new ArrayList();//获得的全部信息

            for (int i = 0; i < patients.Count; i++)
            {
                ArrayList alOrder = orderManager.QueryOrderExec(((FS.FrameWork.Models.NeuObject)patients[i]).ID, dtBegin, dtEnd, usageCode, isPrinted);

                if (alOrder == null) //为提示出错 
                {
                    MessageBox.Show(orderManager.Err);
                    return;
                }
                else
                {
                    ArrayList alFinal = new ArrayList();

                    ArrayList alOrderNew = new ArrayList();

                    //不设置，默认查看全部
                    if (this.speOrderType.Length <= 0)
                    {
                        foreach (FS.HISFC.Models.Order.ExecOrder order in alOrder)
                        {
                            if (isFirst)
                            {
                                if (order.Order.BeginTime.ToShortDateString() == order.DateUse.ToShortDateString())
                                {
                                    alOrderNew.Add(order);
                                }
                            }
                            else
                            {
                                alOrderNew.Add(order);
                            }
                        }
                    }
                    else
                    {
                        //只要包含一个，就认为可以看到
                        string[] speStr = this.speOrderType.Split(',');

                        foreach (FS.HISFC.Models.Order.ExecOrder order in alOrder)
                        {
                            bool isAdd = false;

                            foreach (string s in speStr)
                            {
                                if (order.Order.SpeOrderType == "" || order.Order.SpeOrderType.IndexOf(s) >= 0)
                                {
                                    isAdd = true;
                                    break;
                                }
                            }

                            if (isAdd)
                                alOrderNew.Add(order);
                        }
                    }

                    alOrder = alOrderNew;

                    foreach (FS.HISFC.Models.Order.ExecOrder exe in alOrder)
                    {
                        if (orderType == "ALL")
                        {
                            alFinal.Add(exe);
                        }

                        if (orderType == "LONG")
                        {
                            if (exe.Order.OrderType.IsDecompose)
                            {
                                alFinal.Add(exe);
                            }
                        }

                        if (orderType == "SHORT")
                        {
                            if (!exe.Order.OrderType.IsDecompose)
                            {
                                alFinal.Add(exe);
                            }
                        }
                    }

                    alOrder = alFinal;
                }

                string strDiff = "";//分组用
                ArrayList alObjects = null;//分组用
                int iComboNum = 0;
                int MaxComboNum = 70;        //每页最多5个药
                int iPage = 0;//分页用

                FS.HISFC.Models.Order.Inpatient.Order orderObj = new FS.HISFC.Models.Order.Inpatient.Order();

                FS.HISFC.Models.Order.ExecOrder execOrder = new FS.HISFC.Models.Order.ExecOrder();
                for (int j = 0; j < alOrder.Count; j++)//医嘱信息
                {
                    execOrder = (FS.HISFC.Models.Order.ExecOrder)alOrder[j] as FS.HISFC.Models.Order.ExecOrder;
                    //赋全患者信息
                    execOrder.Order.Patient = ((FS.HISFC.Models.RADT.PatientInfo)patients[i]).Clone();//患者信息付数值

                    #region 停止医嘱后的瓶签不打印
                    orderObj = this.orderManager.QueryOneOrder(execOrder.Order.ID);
                    if (orderObj == null)
                    {
                        MessageBox.Show("查询医嘱出错：" + this.orderManager.Err);
                        return;
                    }

                    execOrder.Order.SortID = orderObj.SortID;

                    if (!execOrder.IsExec)
                    {
                        continue;
                    }

                    //重整、停止医嘱
                    if ("3,4".Contains(orderObj.Status.ToString()))
                    {
                        if (execOrder.DateUse > orderObj.DCOper.OperTime)
                        {
                            continue;
                        }
                    }

                    #endregion

                    //按组合分组给输液卡对象
                    if (execOrder.Order.Combo.ID != "0"
                        && execOrder.Order.Combo.ID != "")
                    {

                        if (execOrder.Order.OrderType.IsDecompose)
                        {
                            execOrderID = execOrder.Order.Combo.ID + execOrder.DateUse.ToString();
                        }
                        else
                        {
                            execOrderID = execOrder.Order.Combo.ID;
                        }

                        if (strDiff != execOrderID)
                        {
                            iComboNum = 0;
                            if (alObjects != null)
                            {
                                al.Add(alObjects);//不同的
                            }
                            alObjects = new ArrayList();

                            strDiff = execOrderID;
                            alObjects.Add(alOrder[j]);

                            iPage = 1;//分页页码第一页

                            execOrder.User01 = iPage.ToString(); //存页码
                        }
                        else//相同的
                        {
                            iComboNum++;
                            if ((iComboNum % MaxComboNum) == 0)
                            {
                                if (alObjects != null)
                                {
                                    al.Add(alObjects);//分页
                                }
                                alObjects = new ArrayList();
                            }
                            iPage = (int)(iComboNum / MaxComboNum) + 1;//分页页码

                            execOrder.User01 = iPage.ToString();

                            alObjects.Add(alOrder[j]);
                        }
                    }
                    else//没有组合号的
                    {
                        if (alObjects != null)
                        {
                            al.Add(alObjects);
                        }
                        alObjects = new ArrayList();
                        alObjects.Add(alOrder[j]);
                        iPage = 1;//分页页码第一页

                        execOrder.User01 = iPage.ToString();
                    }
                }
                if (alObjects != null)
                {
                    al.Add(alObjects);
                }
            }

            ArrayList alNew = this.SplitLZOrders(al);
            al = alNew;

            int iPages = 1;
            for (int i = al.Count - 1; i >= 0; i--)
            {
                ArrayList alTemp = al[i] as ArrayList;
                FS.HISFC.Models.Order.ExecOrder order = alTemp[0] as FS.HISFC.Models.Order.ExecOrder;
                if (FS.FrameWork.Function.NConvert.ToInt32(order.User01) > 1)
                {
                    if (iPages > 1)//已经有最高页了
                    {
                        order.User02 = iPages.ToString();
                    }
                    else//新的
                    {
                        iPages = FS.FrameWork.Function.NConvert.ToInt32(order.User01);//获得最高页
                        order.User02 = iPages.ToString();
                    }
                }
                else if (FS.FrameWork.Function.NConvert.ToInt32(order.User01) <= 1) //到第一页了
                {
                    order.User02 = iPages.ToString();
                    iPages = 1;
                }
            }

            if (this.tabControl1.TabPages.Contains(tpBill))
            {
                this.tabControl1.TabPages.Remove(tpBill);
            }

            this.SetValues(al);

            this.isPrint = isPrinted;//是否补打
            usage = usageCode;
            dt1 = dtBegin;
            dt2 = dtEnd;
            myPatients = patients;
        }

        /// <summary>
        /// 按时间区分的医嘱执行情况ID
        /// </summary>
        string execOrderID = "";

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, bool isFirst)
        {
            return;
        }
        #endregion

        /// <summary>
        /// 赋值显示
        /// </summary>
        /// <param name="alValues"></param>
        protected void SetValues(ArrayList alValues)
        {
            curValues = alValues;

            if (alValues != null)
            {
                FS.HISFC.Models.Base.PageSize page = FS.HISFC.Components.Common.Classes.Function.GetPageSize("Nurse3");

                System.Drawing.Size size = new Size(850, 550);
                if (page != null)
                {
                    size = new Size(page.Height, page.Width);
                }
                foreach (Control c in pnInfusion.Controls)
                {
                    c.Dispose();
                }

                foreach (Control c in pnCard.Controls)
                {
                    c.Dispose();
                }
                FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alValues, typeof(ucDrugTransfusionControl), this.pnInfusion, size);
                FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alValues, ucLabel, this.pnCard, new Size(850, 550), 1);
            }
        }

        #region IPrintTransFusion 成员


        public void SetSpeOrderType(string speStr)
        {
            throw new NotImplementedException();
        }

        #endregion

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control con in pnCard.Controls)
            {
                try
                {
                    FS.SOC.Local.Order.DrugCard.SDFY.InfusionLabel.ucInfusionLabel ucIn = con as FS.SOC.Local.Order.DrugCard.SDFY.InfusionLabel.ucInfusionLabel;

                    ucIn.cbxIsPrint.Checked = this.chkAll.Checked;
                }
                catch { }
            }
        }

        #region IPrintTransFusion 成员

        public bool DCIsPrint
        {
            get;
            set;
        }

        public bool NoFeeIsPrint
        {
            get;
            set;
        }

        public bool QuitFeeIsPrint
        {
            get;
            set;
        }

        #endregion
    }
}
