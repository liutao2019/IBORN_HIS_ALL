using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.SOC.Local.NurseWorkStation.QiaoTou
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
        ucInfusionLabel ucLabel = new ucInfusionLabel();

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

        #region {47D5BD74-2263-4275-9CF8-18DD973E31E7}
        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlManagemnt = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        #endregion

        private ArrayList GetValidControl()
        {
            ArrayList al = new ArrayList();
            foreach (System.Windows.Forms.Control c in this.pnCard.Controls)
            {
                if (c.GetType().ToString() =="FS.SOC.Local.NurseWorkStation.QiaoTou.ucInfusionLabel")
                {
                    ucInfusionLabel cTemp = c as ucInfusionLabel;
                    if (cTemp != null && cTemp.Tag != null && cTemp.IsSelected)
                    {
                        ArrayList alTemp = cTemp.Tag as ArrayList;
                        if (alTemp.Count > 0)
                        {
                            //al.AddRange(alTemp);
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
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            try
            {
                //FS.HISFC.Components.Common.Classes.Function.GetPageSize("Nurse5", ref print);

                //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                ////print.IsDataAutoExtend = true;

                #region {47D5BD74-2263-4275-9CF8-18DD973E31E7}

                //FS.HISFC.PC.MNS.Implement.OrderExcBill ppcExecBillMgr = null;
                //bool isHavePccExecBill = this.controlManagemnt.GetControlParam<bool>("200211", false, false);
                //if (isHavePccExecBill)
                //{
                //    ppcExecBillMgr = new FS.HISFC.PC.MNS.Implement.OrderExcBill();
                //}

                #endregion
                ArrayList alTemp = this.GetValidControl();
                ucLabel.Dispose();
                FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alTemp, ucLabel, this.pnCard, new Size(340, 240), 1);//new Size(800, 1100), 1);
                  

                #region ��ӡƿ����
                FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();
                p.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
                System.Drawing.Printing.PrintDocument PrintDocument = new System.Drawing.Printing.PrintDocument(); 
                FS.HISFC.BizLogic.Manager.PageSize pgMgr = new FS.HISFC.BizLogic.Manager.PageSize();
                FS.HISFC.Models.Base.PageSize pSize = new FS.HISFC.Models.Base.PageSize();
                pSize = pgMgr.GetPageSize("Nurse5");
                if (pSize != null)
                {
                    p.SetPageSize(pSize);
                }
                p.PrintDocument.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0;
                p.PrintDocument.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(pSize.Name, pSize.Width, pSize.Height);
                p.PrintDocument.PrinterSettings.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize(pSize.Name, pSize.Width, pSize.Height);
                //���ش�ӡ��ѡ��
                foreach (System.Windows.Forms.Control c in this.pnCard.Controls)
                {
                    if (c.GetType().ToString() == "FS.SOC.Local.NurseWorkStation.QiaoTou.ucInfusionLabel")
                    {
                        FS.SOC.Local.NurseWorkStation.QiaoTou.ucInfusionLabel cTemp = c as FS.SOC.Local.NurseWorkStation.QiaoTou.ucInfusionLabel;
                        if (cTemp != null && cTemp.Tag != null)
                        {
                            cTemp.SetPrint();
                        }
                    }
                }
                foreach (Control c in this.pnCard.Controls)
                {
                    if (((FS.HISFC.Models.Base.Employee)pgMgr.Operator).IsManager)
                    {
                        p.PrintPreview(0, 0, c);
                    }
                    else
                    {
                       p.PrintPage(10, 0, c);
                        //p.PrintPreview(0, 0, c);
                        //PrintDocument.Print();
                        //p.PrintPreview(c);
                    }

                }
                

                #endregion

                
                #region �����Ѿ���ӡ���

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                this.orderManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                for (int i = 0; i < alTemp .Count ;i++)//this.curValues.Count; i++)
                {
                    ArrayList al = alTemp[i] as ArrayList;//curValues[i] as ArrayList;
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
                            //FS.HISFC.PC.Object.ExcBill ppcExecBill = new FS.HISFC.PC.Object.ExcBill();

                            //ppcExecBill.BarCode = obj.Order.Combo.ID + obj.DateUse.ToString("yyMMddHHmm");
                            //string ppcExecType = ppcExecBillMgr.QueryExcTypeByUseName(obj.Order.Usage.Name);
                            //ppcExecBill.ExeType = ppcExecType;
                            //ppcExecBill.ExecSqn = obj.ID;
                            //ppcExecBill.DoseUnit = obj.Order.DoseUnit;
                            //ppcExecBill.DoseOnce = obj.Order.DoseOnce;
                            //ppcExecBill.ExecName = obj.Order.Item.Name;
                            //ppcExecBill.InpatientNo = obj.Order.Patient.ID;
                            //ppcExecBill.QtyTot = obj.Order.Qty;
                            //ppcExecBill.FqName = obj.Order.Frequency.Name;
                            //ppcExecBill.UseName = obj.Order.Usage.Name;
                            //ppcExecBill.UseTime = obj.DateUse;
                            //ppcExecBill.GroupNo = obj.Order.Combo.ID;

                            //int ippcReturn = ppcExecBillMgr.InsertExcBill(ppcExecBill);
                        //}
                        #endregion

                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                #endregion

                

                this.Query(myPatients, usage, dt1, dt2, isPrint);
                this.chkAll.Checked = true;
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
                FS.HISFC.Components.Common.Classes.Function.GetPageSize("Nurse5", ref print);

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
                //��ʼ����������
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
        /// ��ѯ
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usageCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isPrinted"></param>
        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted)
        {
            ArrayList al = new ArrayList();//��õ�ȫ����Ϣ

            for (int i = 0; i < patients.Count; i++)
            {
                ArrayList alOrder = orderManager.QueryOrderExec(((FS.FrameWork.Models.NeuObject)patients[i]).ID, dtBegin, dtEnd, usageCode, isPrinted);
                if (alOrder == null) //Ϊ��ʾ���� 
                {
                    MessageBox.Show(orderManager.Err);
                    return;
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
                        if (strDiff != ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Combo.ID +
                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse.ToString())
                        {
                            iComboNum = 0;
                            if (alObjects != null)
                            {
                                al.Add(alObjects);//��ͬ��
                            }
                            alObjects = new ArrayList();

                            strDiff = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Combo.ID + ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse.ToString();
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

            this.isPrint = isPrinted;//�Ƿ񲹴�
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
        /// ��ѯ
        /// </summary>
        /// <param name="patients"></param>
        /// <param name="usageCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <param name="isPrinted"></param>
        /// <param name="orderType">All ȫ�� LONG ���� SHORT ��ʱ</param>
        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType)
        {
            ArrayList al = new ArrayList();//��õ�ȫ����Ϣ

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

                    //�����ã�Ĭ�ϲ鿴ȫ��
                    //if (this.speOrderType.Length <= 0)
                    //{
                    foreach (FS.HISFC.Models.Order.ExecOrder order in alOrder)
                    {
                        alOrderNew.Add(order);
                    }
                    //}
                    //else
                    //{
                    //    //ֻҪ����һ��������Ϊ���Կ���
                    //    string[] speStr = this.speOrderType.Split(',');

                    //    foreach (FS.HISFC.Models.Order.ExecOrder order in alOrder)
                    //    {
                    //        bool isAdd = false;

                    //        foreach (string s in speStr)
                    //        {
                    //            if (order.Order.SpeOrderType == "" || order.Order.SpeOrderType.IndexOf(s) >= 0)
                    //            {
                    //                isAdd = true;
                    //                break;
                    //            }
                    //        }

                    //        if (isAdd)
                    //            alOrderNew.Add(order);
                    //    }
                    //}

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
                        if (strDiff != ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Combo.ID +
                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse.ToString())
                        {
                            iComboNum = 0;
                            if (alObjects != null)
                            {
                                al.Add(alObjects);//��ͬ��
                            }
                            alObjects = new ArrayList();

                            strDiff = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Combo.ID + ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse.ToString();
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

            this.isPrint = isPrinted;//�Ƿ񲹴�
            usage = usageCode;
            dt1 = dtBegin;
            dt2 = dtEnd;
            myPatients = patients;

            if (this.tabControl1.TabPages.Contains(tpBill))
            {
                this.tabControl1.TabPages.Remove(tpBill);
            }
        }


        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usageCode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, string orderType,bool isFirst)
        {
            ArrayList al = new ArrayList();//��õ�ȫ����Ϣ

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
                        if (strDiff != ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Combo.ID +
                            ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse.ToString())
                        {
                            iComboNum = 0;
                            if (alObjects != null)
                            {
                                al.Add(alObjects);//��ͬ��
                            }
                            alObjects = new ArrayList();

                            strDiff = ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).Order.Combo.ID + ((FS.HISFC.Models.Order.ExecOrder)alOrder[j]).DateUse.ToString();
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

            this.isPrint = isPrinted;//�Ƿ񲹴�
            usage = usageCode;
            dt1 = dtBegin;
            dt2 = dtEnd;
            myPatients = patients;

            if (this.tabControl1.TabPages.Contains(tpBill))
            {
                this.tabControl1.TabPages.Remove(tpBill);
            }
        }

        public void Query(List<FS.HISFC.Models.RADT.PatientInfo> patients, string usagecode, DateTime dtBegin, DateTime dtEnd, bool isPrinted, bool isFirst)
        {
            return;
        }
        #endregion

        protected void SetValues(ArrayList alValues)
        {
            curValues = alValues;

            if (alValues != null)
            {
                FS.FrameWork.WinForms.Classes.Function.AddControlToPanel(alValues, ucLabel, this.pnCard, new Size(340, 240), 1);//(340, 265), 1);//(262, 1100), 1);
                    //new Size (262,200), 1);//new Size(800, 1100), 1); 
            }
        }

        #region IPrintTransFusion ��Ա


        public void SetSpeOrderType(string speStr)
        {
           
        }

        #endregion
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Control con in pnCard.Controls)
            {
                try
                {
                    ucInfusionLabel ucIn = con as ucInfusionLabel;

                    ucIn.cbxIsPrint.Checked = this.chkAll.Checked;
                }
                catch { }
            }
        }

        private System.Drawing.Printing.PageSettings PrtSetUp(System.Drawing.Printing.PrintDocument printDocument)
        {
            //��������ֵ��PageSettings 
            System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
            //������ʵ����PageSetupDialog 
            PageSetupDialog psDlg = new PageSetupDialog();
            try
            {
                //����ĵ����ĵ�ҳ��Ĭ������ 
                psDlg.Document = printDocument;
                psDlg.PageSettings = printDocument.DefaultPageSettings;
                //��ʾ�Ի��� 
                DialogResult result = psDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    ps = psDlg.PageSettings;
                    printDocument.DefaultPageSettings = psDlg.PageSettings;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "���ִ�ӡ���� ");
            }
            finally
            {
                psDlg.Dispose();
                psDlg = null;
            }
            return ps;
        }

        //{014680EC-6381-408b-98FB-A549DAA49B82}
        #region IPrintTransFusion ��Ա
        // ժҪ:
        //     ֹͣ���Ƿ��ӡ
        public bool DCIsPrint { get; set; }
        //
        // ժҪ:
        //     δ�շ�֪���ӡ
        public bool NoFeeIsPrint { get; set; }
        //
        // ժҪ:
        //     �˷��Ƿ��ӡ
        public bool QuitFeeIsPrint { get; set; }
        #endregion
    }
}
