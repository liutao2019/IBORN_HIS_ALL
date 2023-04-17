using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.DrugCard
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

        #region IPrintTransFusion ��Ա

        /// <summary>
        /// ���ϵ�ִ�е��Ƿ��ӡ
        /// </summary>
        private bool dcIsPrint = false;

        /// <summary>
        /// ���ϵ�ִ�е��Ƿ��ӡ
        /// </summary>
        public bool DCIsPrint
        {
            get
            {
                return dcIsPrint;
            }
            set
            {
                dcIsPrint = value;
            }
        }

        /// <summary>
        /// δ�շ��Ƿ������ӡ
        /// </summary>
        private bool noFeeIsPrint = false;

        /// <summary>
        /// δ�շ��Ƿ������ӡ
        /// </summary>
        public bool NoFeeIsPrint
        {
            get
            {
                return noFeeIsPrint;
            }
            set
            {
                noFeeIsPrint = value;
            }
        }

        /// <summary>
        /// �˷Ѻ���Ƿ��ӡ
        /// </summary>
        private bool quitFeeIsPrint = true;

        /// <summary>
        /// �˷Ѻ���Ƿ��ӡ
        /// </summary>
        public bool QuitFeeIsPrint
        {
            get
            {
                return quitFeeIsPrint;
            }
            set
            {
                quitFeeIsPrint = value;
            }
        }

        FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();

        ArrayList curValues = null; //��ǰ��ʾ������
        bool isPrint = false;
        List<FS.HISFC.Models.RADT.PatientInfo> myPatients = null;
        DateTime dt1;
        DateTime dt2;
        string usage = "";
        string speOrderType = "";

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlManagemnt = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private ArrayList GetValidControl()
        {
            ArrayList al = new ArrayList();
            foreach (System.Windows.Forms.Control c in this.pnCard.Controls)
            {
                if (c.GetType().ToString() == "SOC.Local.NurseWorkStation.ZhangCha.ucInfusionLabel")
                {
                    ucInfusionLabelNew cTemp = c as ucInfusionLabelNew;// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
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
        /// 
        /// </summary>
        public void PrintSet()
        {
            //print.ShowPrintPageDialog();
            //this.Print();
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            try
            {
                #region ��ӡƿ����
                // {D59C1D74-CE65-424a-9CB3-7F9174664504}
                string printerName = FS.HISFC.Components.Common.Classes.Function.ChoosePrinter();
                if (string.IsNullOrEmpty(printerName))
                {
                    return;
                }
                FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
                print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;

                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = pgMgr.GetPageSize("DrugCardPaper");

                if (pSize != null)
                {
                    print.SetPageSize(pSize);
                }

                ArrayList alPrint = new ArrayList();

                Classes.LogManager.Write("��ʼ��ӡ������" + pnCard.Controls.Count.ToString() + "\r\n");

                int index = 0;
                foreach (Control c in this.pnCard.Controls)
                {// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
                    FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.DrugCard.ucInfusionLabelNew cTemp = c as FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.DrugCard.ucInfusionLabelNew;
                    if (cTemp.IsSelected)
                    {
                        if (pSize != null)
                        {
                            pSize.Height = cTemp.Height;
                            //pSize.Width = cTemp.Width;

                            print.SetPageSize(pSize);
                        }

                        print.PrintDocument.PrinterSettings.PrinterName = printerName;
                        cTemp.SetPrint();
                        if (((FS.HISFC.Models.Base.Employee)pgMgr.Operator).IsManager)
                        {
                            print.PrintPreview(pSize.Left, pSize.Top, c);
                            Classes.LogManager.Write("��ӡ��" + index.ToString() + "��\r\n");
                        }
                        else
                        {
                            print.PrintPage(pSize.Left, pSize.Top, c);
                            Classes.LogManager.Write("��ӡ��" + index.ToString() + "��\r\n");
                        }
                        alPrint.Add(index);
                    }
                    index++;
                }
                Classes.LogManager.Write("���´�ӡ���������" + alPrint.Count.ToString() + "\r\n");
                
                #endregion

                #region �����Ѿ���ӡ���

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                for (int i = 0; i < alPrint.Count; i++)
                {
                    int j = FS.FrameWork.Function.NConvert.ToInt32(alPrint[i]);
                    ArrayList al = curValues[j] as ArrayList;
                    foreach (FS.HISFC.Models.Order.ExecOrder obj in al)
                    {
                        if (this.orderManager.UpdateTransfusionPrinted(obj.ID) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���´�ӡ���ʧ��!" + orderManager.Err);
                            return;
                        }

                        #region �ƶ�����վ
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

                //if (((FS.HISFC.Models.Base.Employee)pgMgr.Operator).IsManager)
                //{
                    this.Query(myPatients, usage, dt1, dt2, isPrint, this.orderType, this.isFirst);
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return;
        }

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
                    trueFrequency = SOC.HISFC.BizProcess.Cache.Order.GetFrequency(execOrder.Order.Frequency.ID);
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
                                for (int k = 0; k < alTemp.Count; k++)
                                {
                                    (alTemp[k] as FS.HISFC.Models.Order.ExecOrder).DateUse = execOrder.DateUse.Date.AddHours(dt.Hour);
                                   
                                }
                                //execOrder.DateUse = execOrder.DateUse.Date.AddHours(dt.Hour);
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
                                    execOrderAdd.DateUse = execOrderAdd.DateUse.Date.AddHours(dt.Hour);

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

        Hashtable hsAllComb = new Hashtable();

        /// <summary>
        /// ��ʱ�����ֵ�ҽ��ִ�����ID
        /// </summary>
        string execOrderID = "";

        /// <summary>
        /// ҽ������ allȫ����1 ������0 ����
        /// </summary>
        private string orderType;

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        private bool isFirst;

        /// <summary>
        /// ÿҳ��ӡ��ҽ������
        /// </summary>
        int maxPagNum = 7;

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType,bool isFirst)
        {
            this.orderType = orderType;
            this.isFirst = isFirst;
            this.isPrint = isPrinted;//�Ƿ񲹴�
            usage = usageCode;
            dt1 = dtBegin;
            dt2 = dtEnd;
            myPatients = patients;


            ArrayList alPrint = new ArrayList();//��õ�ȫ����Ϣ

            #region ����������ѯ

            string paramPatient = "";
            //���in�Ļ���id����
            Dictionary<string, FS.HISFC.Models.RADT.PatientInfo> hsPatientInfo = new Dictionary<string, FS.HISFC.Models.RADT.PatientInfo>();

            for (int i = 0; i < patients.Count; i++)
            {
                FS.HISFC.Models.RADT.PatientInfo p = patients[i] as FS.HISFC.Models.RADT.PatientInfo;
                paramPatient = "'" + p.ID + "'," + paramPatient;

                if (!hsPatientInfo.ContainsKey(p.ID))
                {
                    hsPatientInfo.Add(p.ID, p);
                }
            }

            if (paramPatient == "")
            {
                paramPatient = "''";
            }
            else
            {
                paramPatient = paramPatient.Substring(0, paramPatient.Length - 1);//ȥ������Ķ���
            }

            ArrayList alExecOrder = orderManager.QueryOrderExec(paramPatient, dtBegin, dtEnd, usageCode, isPrinted);
            if (alExecOrder == null)
            {
                MessageBox.Show(orderManager.Err);
                return;
            }

            Dictionary<string, ArrayList> dicCombNo = new Dictionary<string, ArrayList>();
            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrder)
            {
                //ҽ��ͣ�ú���Զ�����ֹͣʱ����ִ�е�������ֻ��Ҫ�ж�ִ�е��Ƿ���Ч����
                if (!dcIsPrint && !execOrder.IsValid)
                {
                    continue;
                }

                //����
                if (orderType == "1")
                {
                    if (!execOrder.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }
                //����
                else if (orderType == "0")
                {
                    if (execOrder.Order.OrderType.IsDecompose)
                    {
                        continue;
                    }
                }

                //ֻ��ӡ������
                if (isFirst)
                {
                    if (execOrder.Order.MOTime.Date != execOrder.DateUse.Date)
                    {
                        continue;
                    }
                }

                if (execOrder.Order.OrderType.IsDecompose)
                {
                    if (!dicCombNo.ContainsKey(execOrder.Order.Combo.ID + execOrder.DateUse.ToString()))
                    {
                        ArrayList alTemp = new ArrayList();
                        alTemp.Add(execOrder);
                        dicCombNo.Add(execOrder.Order.Combo.ID + execOrder.DateUse.ToString(), alTemp);
                    }
                    else
                    {
                        ArrayList alTemp = dicCombNo[execOrder.Order.Combo.ID + execOrder.DateUse.ToString()];
                        alTemp.Add(execOrder);
                        dicCombNo[execOrder.Order.Combo.ID + execOrder.DateUse.ToString()] = alTemp;
                    }
                }
                else
                {
                    string execTime = this.orderManager.ExecSqlReturnOne(string.Format("select exec_times from met_ipm_order where mo_order='{0}'", execOrder.Order.ID), "");
                    if (!string.IsNullOrEmpty(execTime))
                    {
                        string[] times = execTime.Split('-');
                        for (int i = 0; i < times.Length; i++)
                        {
                            DateTime dt = execOrder.DateUse.Date.AddHours(FS.FrameWork.Function.NConvert.ToInt32(times[i].Substring(0, times[i].IndexOf(':'))));

                            execOrder.DateUse = dt;
                            if (!dicCombNo.ContainsKey(execOrder.Order.Combo.ID + dt.ToString()))
                            {
                                ArrayList alTemp = new ArrayList();
                                alTemp.Add(execOrder.Clone());
                                dicCombNo.Add(execOrder.Order.Combo.ID + dt.ToString(), alTemp);
                            }
                            else
                            {
                                ArrayList alTemp = dicCombNo[execOrder.Order.Combo.ID + dt.ToString()];
                                alTemp.Add(execOrder.Clone());
                                dicCombNo[execOrder.Order.Combo.ID + dt.ToString()] = alTemp;
                            }
                        }
                    }
                }
            }

            foreach (string key in dicCombNo.Keys)
            {
                if (dicCombNo[key].Count < maxPagNum)
                {
                    alPrint.Add(dicCombNo[key]);
                }
                else
                {
                    int page = (Int32)Math.Ceiling((decimal)dicCombNo[key].Count / maxPagNum);
                    for (int i = 0; i < page; i++)
                    {
                        ArrayList alTemp = new ArrayList();
                        for (int k = 0; k < dicCombNo[key].Count; k++)
                        {
                            if (k >= i * maxPagNum && k < (i + 1) * maxPagNum)
                            {
                                ((FS.HISFC.Models.Order.ExecOrder)dicCombNo[key][k]).Order.User03 = (i + 1).ToString() + "/" + page.ToString();
                                alTemp.Add(dicCombNo[key][k]);
                            }
                        }
                        alPrint.Add(alTemp);
                    }
                }
            }


            #endregion

            #region �ɵ�����

            /*

            for (int i = 0; i < patients.Count; i++)
            {
                ArrayList alOrder = orderManager.QueryOrderExec(((FS.FrameWork.Models.NeuObject)patients[i]).ID, dtBegin, dtEnd, usageCode, isPrinted);

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
                        if (orderType.ToUpper() == "ALL")
                        {
                            alFinal.Add(exe);
                        }

                        if (orderType == "1")
                        {
                            if (exe.Order.OrderType.IsDecompose)
                            {
                                alFinal.Add(exe);
                            }
                        }

                        if (orderType == "0")
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
                int MaxComboNum = 9;        //ÿҳ���5��ҩ
                int iPage = 0;//��ҳ��

                FS.HISFC.Models.Order.Inpatient.Order orderObj = new FS.HISFC.Models.Order.Inpatient.Order();

                for (int j = 0; j < alOrder.Count; j++)//ҽ����Ϣ
                {
                    //��ȫ������Ϣ
                    ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Patient.Name = ((FS.HISFC.Models.RADT.PatientInfo)patients[i]).Name;//������Ϣ����ֵ

                    #region ֹͣҽ�����ƿǩ����ӡ

                    //ҽ��ͣ�ú���Զ�����ֹͣʱ����ִ�е�������ֻ��Ҫ�ж�ִ�е��Ƿ���Ч����
                    if (!dcIsPrint && !((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).IsValid)
                    {
                        continue;
                    }

                    //����
                    if (orderType == "1")
                    {
                        if (!((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.OrderType.IsDecompose)
                        {
                            continue;
                        }
                    }
                    //����
                    else if (orderType == "0")
                    {
                        if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.OrderType.IsDecompose)
                        {
                            continue;
                        }
                    }

                    //ֻ��ӡ������
                    if (isFirst)
                    {
                        if (((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.MOTime.Date != ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse.Date)
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
                                alPrint.Add(alObjects);//��ͬ��
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
                                    alPrint.Add(alObjects);//��ҳ
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
                            alPrint.Add(alObjects);
                        }
                        alObjects = new ArrayList();
                        alObjects.Add(alOrder[j]);
                        iPage = 1;//��ҳҳ���һҳ

                        ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).User01 = iPage.ToString();
                    }
                }
                if (alObjects != null)
                {
                    alPrint.Add(alObjects);
                }
            }

            ArrayList alNew = this.SplitLZOrders(alPrint);
            alPrint = alNew;

            int iPages = 1;
            for (int i = alPrint.Count - 1; i >= 0; i--)
            {
                ArrayList alTemp = alPrint[i] as ArrayList;
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
            **/
            #endregion

            this.SetValues(alPrint);          
        }

        #endregion

        protected void SetValues(ArrayList alValues)
        {
            curValues = alValues;

            if (alValues != null)
            {

                Classes.LogManager.Write("\r\n\r\n");
                Classes.LogManager.Write("��ѯƿǩ������" + alValues.Count.ToString() + "\r\n");

                alValues.Sort(new ComparerExecOrder());

                FS.HISFC.Models.Base.PageSize page = FS.HISFC.Components.Common.Classes.Function.GetPageSize("Nurse5");

                System.Drawing.Size size = new Size(800, 1200);
                if (page != null)
                {
                    size = new Size(page.Height, page.Width);
                }

                foreach (Control c in pnCard.Controls)
                {
                    c.Dispose();
                }

                FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.DrugCard.ucInfusionLabelNew ucLabel = new ucInfusionLabelNew();// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}

                FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alValues, ucLabel, this.pnCard, new Size(1100, 1250), 1);

                Classes.LogManager.Write("��¼������ʾ��������" + pnCard.Controls.Count.ToString() + "\r\n");
            }
        }

        #region IPrintTransFusion ��Ա


        public void SetSpeOrderType(string speStr)
        {
           
        }

        #endregion

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control c in this.pnCard.Controls)
            {

                FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.DrugCard.ucInfusionLabelNew cTemp = c as FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.DrugCard.ucInfusionLabelNew;

                cTemp.IsSelected = chkAll.Checked;
            }
        }
    }

    /// <summary>
    /// ����
    /// </summary>
    public class ComparerExecOrder : IComparer
    {
        #region IComparer ��Ա

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        public int Compare(object x, object y)
        {
            try
            {
                FS.HISFC.Models.Order.ExecOrder execOrder1 = ((ArrayList)x)[0] as FS.HISFC.Models.Order.ExecOrder;
                FS.HISFC.Models.Order.ExecOrder execOrder2 = ((ArrayList)y)[0] as FS.HISFC.Models.Order.ExecOrder;
                //string aa = manager.GetBed(execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID).SortID.ToString().PadLeft(4, '0') + execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID;
                //string bb = manager.GetBed(execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID).SortID.ToString().PadLeft(4, '0') + execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID;

                string aa = execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4).PadLeft(8, '0') + execOrder1.Order.Patient.PVisit.PatientLocation.Bed.ID;
                string bb = execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID.Substring(4).PadLeft(8, '0') + execOrder2.Order.Patient.PVisit.PatientLocation.Bed.ID;

                string cc = execOrder1.Order.SubCombNO.ToString().PadLeft(5,'0') + execOrder1.DateUse.ToString() + execOrder1.Order.SortID.ToString() + execOrder1.Order.Combo.ID + execOrder1.Order.ID;
                string dd = execOrder2.Order.SubCombNO.ToString().PadLeft(5, '0') + execOrder2.DateUse.ToString() + execOrder2.Order.SortID.ToString() + execOrder2.Order.Combo.ID + execOrder2.Order.ID;

                if (string.Compare(aa, bb) > 0)
                {
                    return 1;
                }
                else if (string.Compare(aa, bb) == 0)
                {
                    return string.Compare(cc, dd);
                }
                else
                {
                    return -1;
                }
            }
            catch
            {
                return 0;
            }
        }
        #endregion
    }
}
