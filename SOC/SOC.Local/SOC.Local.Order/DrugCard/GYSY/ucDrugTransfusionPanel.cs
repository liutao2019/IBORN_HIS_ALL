using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.Local.Order.DrugCard.GYSY;
namespace FS.SOC.Local.Order.DrugCard.GYSY
{
    /// <summary>
    /// [��������: ��Һ���ؼ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
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
        /// ƿ����
        /// </summary>
        FS.SOC.Local.Order.DrugCard.GYSY.ucInfusionLabel ucLabel = new FS.SOC.Local.Order.DrugCard.GYSY.ucInfusionLabel();

        #region IPrintTransFusion ��Ա
        FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
        ArrayList curValues = null; //��ǰ��ʾ������
        bool isPrint = false;
        List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;
        DateTime dt1;
        DateTime dt2;
        string usage = "";
        string speOrderType = "";

        /// <summary>
        /// ҽ������ allȫ����1 ������0 ����
        /// </summary>
        private string orderType;

        /// <summary>
        /// �Ƿ񲹴�
        /// </summary>
        bool isRePrint = false;

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        private bool isFirst;

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlManagemnt = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �Ƿ������ӡδִ�У��Ʒѣ�ҽ��
        /// </summary>
        int isPrintNoExecOrder = -1;

        private ArrayList GetValidControl()
        {
            ArrayList al = new ArrayList();
            foreach (System.Windows.Forms.Control c in this.pnCard.Controls)
            {
                if (c.GetType().ToString() == "FS.SOC.Local.NurseWorkStation.ChaoYang.ucInfusionLabel")
                {
                    ucInfusionLabel cTemp = c as ucInfusionLabel;
                 
                    if (cTemp != null && cTemp.Tag != null && cTemp.IsSelected)
                    {
                        ArrayList alTemp = cTemp.Tag as ArrayList;
                        if (alTemp.Count > 0)
                        {
                            al.AddRange(alTemp);
                        }
                    }
                }
            }
            return al;
        }
        /// <summary>
        /// ��ӡ
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

                #region ��ӡƿ����
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //p.PrintPreview(5, 5, this.pnCard);
                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize();
                pSize = pgMgr.GetPageSize("InPatientCureCard");
                if (pSize != null)
                {
                    p.SetPageSize(pSize);
                }

                foreach (Control c in this.pnCard.Controls)
                {
                    //�ж��Ƿ��ѡ��ӡ 2011-12-16  SNIPER
                    FS.SOC.Local.Order.DrugCard.GYSY.ucInfusionLabel cTemp = c as FS.SOC.Local.Order.DrugCard.GYSY.ucInfusionLabel;
                    if (cTemp.IsSelected)
                    {
                        cTemp.SetPrint();
                        if (((FS.HISFC.Models.Base.Employee)pgMgr.Operator).IsManager)
                        {
                            
                            p.PrintPreview(pSize.Left, pSize.Top, c);
                        }
                        else
                        {
                            p.PrintPage(pSize.Left, pSize.Top, c);
                        }
                    }

                }
                #region ������ ��������
                //�����з�ѡ���ƿǩ�����´�panel��
                //ucDrugTransfusionPanel ucTemp = new ucDrugTransfusionPanel();
                //ArrayList alTemp = this.GetValidControl();
                //if (alTemp.Count > 0)
                //{
                //    FS.HISFC.Models.Base.PageSize page = FS.HISFC.Components.Common.Classes.Function.GetPageSize("Nurse3");

                //    System.Drawing.Size size = new Size(800, 1200);
                //    if (page != null)
                //    {
                //        size = new Size(page.Height, page.Width);
                //    }

                //    //FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alTemp, typeof(ucDrugTransfusionControl), ucTemp.pnInfusion, size);

                //    FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alTemp, ucTemp.ucLabel, ucTemp.pnCard, new Size(800, 1100), 1);
                //}
                //else
                //{
                //    MessageBox.Show("û�п��Դ�ӡ��ƿǩ��");
                //    return;
                //}
                ////p.PrintPage(0, 0, this.pnCard);
                ////p.PrintPage(0, 0, ucTemp.pnCard);
                //FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucTemp);
                ////ucTemp.PrintPanel();

                #endregion

                #endregion

                //if (print.PrintPage(0, 0, this.pnInfusion) == 0 && isPrint == false)//��ӡԤ��
                //{
                #region �����Ѿ���ӡ���

                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                for (int i = 0; i < this.curValues.Count; i++)
                {
                    
                     ArrayList al = curValues[i] as ArrayList;
                    
                        foreach (FS.HISFC.Models.Order.ExecOrder obj in al)
                        {
                            
                            if (this.orderManager.UpdateTransfusionPrinted(obj.ID) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                MessageBox.Show("���´�ӡ���ʧ��!" + orderManager.Err);
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

                //this.Query(myPatients, usage, dt1, dt2, isPrint);
                //}

                this.Query(myPatients, usage, dt1, dt2, isRePrint, orderType, isFirst);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void PrintPanel()
        {
            try
            {
                FS.HISFC.Components.Common.Classes.Function.GetPageSize("InPatientCureCard", ref print);

                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                //p.PrintPreview(5, 5, this.pnCard);
                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize();
                pSize = pgMgr.GetPageSize("InPatientCureCard");
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

        private FS.FrameWork.Public.ObjectHelper frequencyHelper = null;

        /// <summary>
        /// ��ʱҽ���Ƿ���Ƶ�ηֿ���ӡƿǩ
        /// </summary>
        private int isSplitLZOrders = -1;

        /// <summary>
        /// ����Ƶ�β����ʱҽ��
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
                //����
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
                    //ִ��ʱ��
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
        /// ��ʱ�����ֵ�ҽ��ִ�����ID
        /// </summary>
        string execOrderID = "";

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType, bool isFirst)
        {
            this.orderType = orderType;
            this.isRePrint = isPrinted;
            this.isFirst = isFirst;

            this.dt1 = dtBegin;
            this.dt2 = dtEnd;
            this.usage = usageCode;
            myPatients = patients;


            ArrayList al = new ArrayList();//��õ�ȫ����Ϣ

            for (int i = 0; i < patients.Count; i++)
            {
                //string patientInfo = "'" + ((FS.FrameWork.Models.NeuObject)patients[i]).ID + "'";//ΪʲôҪ����' ���ݿ��ѯ����
                string patientInfo = ((FS.FrameWork.Models.NeuObject)patients[i]).ID;
                ArrayList alOrder = orderManager.QueryOrderExec(patientInfo, dtBegin, dtEnd, usageCode, isPrinted);

                if (alOrder == null) //Ϊ��ʾ����
                {
                    MessageBox.Show(orderManager.Err);
                    return;
                }
                else
                {
                    ArrayList alFinal = new ArrayList();

                    ArrayList alOrderNew = new ArrayList();


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

                string strDiff = "";//������
                ArrayList alObjects = null;//������
                int iComboNum = 0;
                int MaxComboNum = 5;        //ÿҳ���5��ҩ
                int iPage = 0;//��ҳ��

                FS.HISFC.Models.Order.Inpatient.Order orderObj = new FS.HISFC.Models.Order.Inpatient.Order();

                for (int j = 0; j < alOrder.Count; j++)//ҽ����Ϣ
                {
                    //��ȫ������Ϣ
                    ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Patient = ((FS.HISFC.Models.RADT.PatientInfo)patients[i]).Clone();//������Ϣ����ֵ

                    #region ֹͣҽ�����ƿǩ����ӡ
                    orderObj = this.orderManager.QueryOneOrder(((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.ID);
                    if (orderObj == null)
                    {
                        MessageBox.Show("��ѯҽ������" + this.orderManager.Err);
                        return;
                    }

                    //������ֹͣҽ��
                    if ("3,4".Contains(orderObj.Status.ToString()))
                    {
                        if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse > orderObj.DCOper.OperTime)
                        {
                            continue;
                        }
                    }

                    #endregion

                    //����Ϸ������Һ������
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
                                al.Add(alObjects);//��ͬ��
                            }
                            alObjects = new ArrayList();

                            strDiff = execOrderID;
                            alObjects.Add(alOrder[j]);

                            iPage = 1;//��ҳҳ���һҳ

                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).User01 = iPage.ToString(); //��ҳ��
                        }
                        else//��ͬ��
                        {
                            iComboNum++;
                            if ((iComboNum % MaxComboNum) == 0)
                            {
                                if (alObjects != null)
                                {
                                    al.Add(alObjects);//��ҳ
                                }
                                alObjects = new ArrayList();
                            }
                            iPage = (int)(iComboNum / MaxComboNum) + 1;//��ҳҳ��

                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).User01 = iPage.ToString();

                            alObjects.Add(alOrder[j]);
                        }
                    }
                    else//û����Ϻŵ�
                    {
                        if (alObjects != null)
                        {
                            al.Add(alObjects);
                        }
                        alObjects = new ArrayList();
                        alObjects.Add(alOrder[j]);
                        iPage = 1;//��ҳҳ���һҳ

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
                    if (iPages > 1)//�Ѿ������ҳ��
                    {
                        order.User02 = iPages.ToString();
                    }
                    else//�µ�
                    {
                        iPages = FS.FrameWork.Function.NConvert.ToInt32(order.User01);//������ҳ
                        order.User02 = iPages.ToString();
                    }
                }
                else if (FS.FrameWork.Function.NConvert.ToInt32(order.User01) <= 1) //����һҳ��
                {
                    order.User02 = iPages.ToString();
                    iPages = 1;
                }
            }

            this.SetValues(al);

            if (this.tabControl1.TabPages.Contains(tpBill))
            {
                this.tabControl1.TabPages.Remove(tpBill);
            }
        }
        #endregion

        protected void SetValues(ArrayList alValues)
        {
            curValues = alValues;

            if (alValues != null)
            {
                //this.tabControl1.TabPages[this.tabControl1.SelectedIndex].Title = ((FS.neuFC.Object.neuObject)this.tabControl1.SelectedTab.Tag).Name + "��" + value.Count.ToString() + "����";
                FS.HISFC.Models.Base.PageSize page = FS.HISFC.Components.Common.Classes.Function.GetPageSize("InPatientCureCard");

                System.Drawing.Size size = new Size(900, 1200);
                if (page != null)
                {
                    size = new Size(page.Height, page.Width);
                }

                //FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alValues, typeof(ucDrugTransfusionControl), this.pnInfusion, size);

                FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alValues, ucLabel, this.pnCard, new Size(1100, 1250), 1);

            }
        }

        #region IPrintTransFusion ��Ա


        public void SetSpeOrderType(string speStr)
        {

        }

        #endregion

        #region IPrintTransFusion ��Ա

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

        /// <summary>
        /// ȫѡ/��ѡ �¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.pnCard.Controls)
            {
                ucInfusionLabel cTemp = c as ucInfusionLabel;
                if (cTemp.checkBox1.Checked)
                {
                    cTemp.checkBox1.Checked = false;
                }
                else
                {
                    cTemp.checkBox1.Checked = true;
                }
            }
        }
    }
}