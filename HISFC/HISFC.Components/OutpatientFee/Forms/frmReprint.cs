using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Function;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.OutpatientFee.Forms
{
    /// <summary>
    /// frmReprint<br></br>
    /// [��������: ���﷢Ʊ�ش�]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-3-16]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class frmReprint : Form
    {
        /// <summary>
        /// ���췽��
        /// </summary>
        public frmReprint()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();
        
        /// <summary>
        /// �������ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// �����ۺ�ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ��ǰ�շѷ�Ʊ
        /// </summary>
        protected FS.HISFC.Models.Fee.Outpatient.Balance currentBalance = new FS.HISFC.Models.Fee.Outpatient.Balance();

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        protected string invoiceType = string.Empty;

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        
        /// <summary>
        /// ��Ʊ��ϸ��Ϣ
        /// </summary>
        protected ArrayList comBalanceLists = new ArrayList();

        /// <summary>
        /// ������ϸ��Ϣ
        /// </summary>
        protected ArrayList comFeeItemLists = new ArrayList();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        ArrayList comBalances = new ArrayList();

        /// <summary>
        /// ֧����Ϣ
        /// </summary>
        ArrayList comBalancePays = new ArrayList();

        //{2322FA44-DF37-42fc-9DE4-FDA8322DC03D}feng.ch
        /// <summary>
        /// �Ƿ��ߺ�
        /// </summary>
        bool isRollCode = true;
        public bool IsRollCode
        {
            get
            {
                return isRollCode;
            }
            set
            {
                isRollCode = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ���
        /// </summary>
        protected virtual void Clear()
        {
            comBalanceLists = new ArrayList();
            comFeeItemLists = new ArrayList();
            comBalances = new ArrayList();
            comBalancePays = new ArrayList();
            currentBalance = new FS.HISFC.Models.Fee.Outpatient.Balance();
            this.tbInvoiceDate.Text = "";
            this.tbInvoiceNo.Text = "";
            this.tbPName.Text = "";
            this.tbOperName.Text = "";
            this.tbOwnCost.Text = "";
            this.tbPayCost.Text = "";
            this.tbPubCost.Text = "";
            this.tbTotCost.Text = "";
            this.fpSpread1_Sheet1.RowCount = 0;
            this.fpSpread1_Sheet1.RowCount = 5;
            this.tbInvoiceNo.Focus();
        }

        /// <summary>
        /// ��÷�Ʊ��Ϣ
        /// </summary>
        protected virtual void QueryBalances()
        {

            string invoiceNo = this.tbInvoiceNo.Text.Trim();
            this.Clear();
            //invoiceNo = invoiceNo.PadLeft(12, '0');
            comBalances = outpatientManager.QueryBalancesSameInvoiceCombNOByInvoiceNO(invoiceNo);
            if (comBalances == null)
            {
                MessageBox.Show("��÷�Ʊ��Ϣ����!" + outpatientManager.Err);
                currentBalance = null;

                return;
            }
            if (comBalances.Count == 0)
            {
                MessageBox.Show("������ķ�Ʊ���벻����,���֤������");
                currentBalance = null;
                this.tbInvoiceNo.SelectAll();
                this.tbInvoiceNo.Focus();

                return;
            }

            decimal totCost = 0, ownCost = 0, payCost = 0, pubCost = 0;
            if (comBalances.Count > 1)
            {
                bool isSelect = false;
                string SeqNo = "";
                foreach (Balance balance in comBalances)
                {
                    if (SeqNo == "")
                    {
                        SeqNo = balance.CombNO;

                        continue;
                    }
                    else
                    {
                        if (SeqNo != balance.CombNO)
                        {
                            isSelect = true;
                        }
                    }
                }

                if (isSelect)
                {
                    FS.HISFC.Components.OutpatientFee.Controls.ucInvoiceSelect ucSelect = new FS.HISFC.Components.OutpatientFee.Controls.ucInvoiceSelect();

                    ucSelect.Add(comBalances);

                    FS.FrameWork.WinForms.Classes.Function.PopShowControl(ucSelect);

                    FS.HISFC.Models.Fee.Outpatient.Balance selectInvoice = ucSelect.SelectedBalance;
                    if (selectInvoice == null || selectInvoice.Invoice.ID == null || selectInvoice.Invoice.ID == "")
                    {
                        MessageBox.Show("��û��ѡ��Ʊ������������ѡ��!");
                        currentBalance = null;
                        this.tbInvoiceNo.SelectAll();
                        this.tbInvoiceNo.Focus();

                        return;
                    }

                    comBalances = outpatientManager.QueryBalancesByInvoiceSequence(selectInvoice.CombNO);
                    if (comBalances == null)
                    {
                        MessageBox.Show("��÷�Ʊ��Ϣ����!" + outpatientManager.Err);
                        currentBalance = null;
                        this.tbInvoiceNo.SelectAll();
                        this.tbInvoiceNo.Focus();

                        return;
                    }
                }
                string tempInvoiceNO = "";
                foreach (Balance balance in comBalances)
                {
                    tempInvoiceNO += balance.Invoice.ID + "\n";
                    totCost += balance.FT.TotCost;
                    ownCost += balance.FT.OwnCost;
                    payCost += balance.FT.PayCost;
                    pubCost += balance.FT.PubCost;
                }

                MessageBox.Show("�÷�Ʊ��Ӧ" + comBalances.Count + "��!�ֱ�Ϊ: \n" + tempInvoiceNO + "\n������Ϸ�Ʊ���ջ�!");
            }
            else
            {
                string tempInvoiceNO = "";
                foreach (Balance balance in comBalances)
                {
                    tempInvoiceNO += balance.Invoice.ID + "\n";
                    totCost += balance.FT.TotCost;
                    ownCost += balance.FT.OwnCost;
                    payCost += balance.FT.PayCost;
                    pubCost += balance.FT.PubCost;
                }
            }

            currentBalance = (comBalances[0] as Balance).Clone();
            if (currentBalance.CancelType != FS.HISFC.Models.Base.CancelTypes.Valid)
            {
                MessageBox.Show("������ķ�Ʊ�����Ѿ����ϣ����֤������");
                currentBalance = null;
                this.tbInvoiceNo.SelectAll();
                this.tbInvoiceNo.Focus();

                return;
            }

            this.tbInvoiceNo.Text = currentBalance.Invoice.ID;
            this.tbPName.Text = currentBalance.Patient.Name;

            FS.HISFC.Models.Base.Employee employee = this.managerIntegrate.GetEmployeeInfo(currentBalance.BalanceOper.ID);
            if (employee == null) 
            {
                MessageBox.Show("��õ�ǰ��Ʊ����Ա��Ϣʧ��!" + this.managerIntegrate.Err);
            }

            this.tbOperName.Text = employee.Name;
            this.tbPactInfo.Text = currentBalance.Patient.Pact.Name;


            this.tbTotCost.Text = totCost.ToString();
            this.tbOwnCost.Text = ownCost.ToString();
            this.tbPayCost.Text = payCost.ToString();
            this.tbPubCost.Text = pubCost.ToString();

            this.tbInvoiceDate.Text = currentBalance.BalanceOper.OperTime.ToString();

            if (!FillBalanceLists(currentBalance.CombNO))
            {
                MessageBox.Show("��÷�Ʊ��ϸ��Ϣ����!" + outpatientManager.Err);
                
                return;
            }

            comFeeItemLists = outpatientManager.QueryFeeItemListsByInvoiceSequence(currentBalance.CombNO);
            if (comFeeItemLists == null)
            {
                MessageBox.Show("��û��߷�����ϸ����!");
                
                return;
            }

            this.btOk.Focus();
        }
        /// <summary>
        /// �ش�Ʊ�����ߺţ�ԭ����ӡ��
        /// </summary>
        /// <returns></returns>
        private int PrintNotRollCode()
        {
            int returnValue = 0;
            string currentInvoiceNO = "";
            string currentRealInvoiceNO = "";
            string errText = "";
            DateTime nowTime = new DateTime();
            nowTime = outpatientManager.GetDateTimeFromSysDateTime();
            invoiceType = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");


            //��Ʊ
            ArrayList invoicesPrint = new ArrayList();
            //��Ʊ��ϸ
            ArrayList invoiceDetailsPrintTemp = new ArrayList();
            //��Ʊ��ϸ
            ArrayList invoiceDetailsPrint = new ArrayList();
            //��Ʊ������ϸ
            ArrayList invoicefeeDetailsPrintTemp = new ArrayList();
            //��Ʊ������ϸ
            ArrayList invoicefeeDetailsPrint = new ArrayList();
            //ȫ��������ϸ
            ArrayList feeDetailsPrint = new ArrayList();


            //��ø���Ʊ��ˮ��
            string invoiceSeqNegative = outpatientManager.GetInvoiceCombNO();
            if (invoiceSeqNegative == null || invoiceSeqNegative == "")
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��÷�Ʊ��ˮ��ʧ��!" + outpatientManager.Err);

                return -1;
            }

            //�������Ʊ��ˮ��
            string invoiceSeqPositive = outpatientManager.GetInvoiceCombNO();
            if (invoiceSeqPositive == null || invoiceSeqPositive == "")
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��÷�Ʊ��ˮ��ʧ��!" + outpatientManager.Err);

                return -1;
            }

            Hashtable hsInvoice = new Hashtable();

            FS.HISFC.Models.Base.Employee employee = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;

            foreach (Balance invoice in comBalances)
            {
                returnValue = this.feeIntegrate.GetInvoiceNO(employee,"C", ref currentInvoiceNO, ref currentRealInvoiceNO, ref errText);
                currentInvoiceNO = invoice.Invoice.ID;
                if (returnValue == -1)
                {
                    MessageBox.Show(errText);
                    return -1;
                }
                hsInvoice.Add(invoice.Invoice.ID, currentInvoiceNO);
                invoice.PrintTime = invoice.BalanceOper.OperTime;
                Balance invoClone = invoice.Clone();

                #region ��Ʊ��Ϣ��ֵ
                invoClone.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                invoClone.FT.TotCost = invoClone.FT.TotCost;
                invoClone.FT.OwnCost = invoClone.FT.OwnCost;
                invoClone.FT.PayCost = invoClone.FT.PayCost;
                invoClone.FT.PubCost = invoClone.FT.PubCost;
                invoClone.CancelType = FS.HISFC.Models.Base.CancelTypes.Reprint;
                invoClone.CanceledInvoiceNO = invoice.Invoice.ID;
                invoClone.CancelOper.ID = outpatientManager.Operator.ID;
                invoClone.BalanceOper.ID = outpatientManager.Operator.ID;
                invoClone.BalanceOper.OperTime = nowTime;
                invoClone.CancelOper.OperTime = nowTime;
                invoClone.IsAuditing = false;
                invoClone.AuditingOper.ID = "";
                invoClone.AuditingOper.OperTime = DateTime.MinValue;
                invoClone.IsDayBalanced = false;
                invoClone.DayBalanceOper.OperTime = DateTime.MinValue;
                invoClone.CombNO = invoiceSeqNegative;
                invoClone.Invoice.ID = currentInvoiceNO;
                invoClone.PrintedInvoiceNO = currentRealInvoiceNO;
                invoicesPrint.Add(invoClone); 
                #endregion

                #region ����Ʊ��ϸ����Ϣ
                ArrayList alInvoceDetail = outpatientManager.QueryBalanceListsByInvoiceNOAndInvoiceSequence(invoice.Invoice.ID, currentBalance.CombNO);
                if (comBalanceLists == null)
                {
                    MessageBox.Show("��÷�Ʊ��ϸ����!" + outpatientManager.Err);
                    return -1;
                }

                invoiceDetailsPrintTemp = new ArrayList();
                foreach (BalanceList d in alInvoceDetail)
                {
                    d.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    d.BalanceBase.FT.OwnCost = d.BalanceBase.FT.OwnCost;
                    d.BalanceBase.FT.PubCost = d.BalanceBase.FT.PubCost;
                    d.BalanceBase.FT.PayCost = d.BalanceBase.FT.PayCost;
                    d.BalanceBase.BalanceOper.OperTime = nowTime;
                    d.BalanceBase.BalanceOper.ID = outpatientManager.Operator.ID;
                    d.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Reprint;
                    d.BalanceBase.IsDayBalanced = false;
                    d.BalanceBase.DayBalanceOper.ID = "";
                    d.BalanceBase.DayBalanceOper.OperTime = DateTime.MinValue;
                    ((Balance)d.BalanceBase).CombNO = invoiceSeqNegative;
                    d.FeeCodeStat.SortID = d.InvoiceSquence;
                    d.BalanceBase.Invoice.ID = currentInvoiceNO;
                    d.BalanceBase.FT.TotCost = d.BalanceBase.FT.OwnCost + d.BalanceBase.FT.PayCost + d.BalanceBase.FT.PubCost;
                    invoiceDetailsPrintTemp.Add(d);
                }

                invoiceDetailsPrint.Add(invoiceDetailsPrintTemp); 
                #endregion

                
            }

            #region ����֧����Ϣ
            comBalancePays = outpatientManager.QueryBalancePaysByInvoiceSequence(currentBalance.CombNO);
            if (comBalancePays == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��û���֧����Ϣ����" + outpatientManager.Err);

                return -1;
            }
            foreach (BalancePay p in comBalancePays)
            {
                p.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                p.FT.TotCost = p.FT.TotCost;
                p.FT.RealCost = p.FT.RealCost;
                p.InputOper.OperTime = nowTime;
                p.InputOper.ID = outpatientManager.Operator.ID;
                p.InvoiceCombNO = invoiceSeqNegative;
                p.CancelType = FS.HISFC.Models.Base.CancelTypes.Reprint;
                p.IsChecked = false;
                p.CheckOper.ID = "";
                p.CheckOper.OperTime = DateTime.MinValue;
                p.BalanceOper.ID = "";
                p.BalanceOper.OperTime = DateTime.MinValue;
                p.IsDayBalanced = false;
                p.IsAuditing = false;
                p.AuditingOper.OperTime = DateTime.MinValue;
                p.AuditingOper.ID = "";
            } 
            #endregion


            #region ���������ϸ��Ϣ
            ArrayList feeItemLists = outpatientManager.QueryFeeItemListsByInvoiceSequence(currentBalance.CombNO);
            if (feeItemLists == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��û��߷�����ϸ����!" + outpatientManager.Err);

                return -1;
            }

            foreach (FeeItemList f in feeItemLists)
            {
                f.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                f.FT.OwnCost = f.FT.OwnCost;
                f.FT.PayCost = f.FT.PayCost;
                f.FT.PubCost = f.FT.PubCost;
                f.Item.Qty = f.Item.Qty;
                f.CancelType = FS.HISFC.Models.Base.CancelTypes.Reprint;
                f.FeeOper.ID = outpatientManager.Operator.ID;
                f.FeeOper.OperTime = nowTime;
                f.CancelOper.OperTime = nowTime;
                f.Invoice.ID = currentInvoiceNO;
                f.InvoiceCombNO = invoiceSeqNegative;
            } 
            #endregion

            #region ���ɸ�ֵ��ķ�Ʊ������ϸ
            foreach (Balance b in invoicesPrint)
            {
                ArrayList feeItemListsClone = new ArrayList();
                foreach (FeeItemList f in feeItemLists)
                {
                    feeItemListsClone.Add(f.Clone());
                }
                while (feeItemListsClone.Count > 0)
                {
                    invoicefeeDetailsPrintTemp = new ArrayList();
                    string compareItem = b.Invoice.ID;
                    foreach (FeeItemList f in feeItemListsClone)
                    {
                        if (f.Invoice.ID == compareItem)
                        {
                            invoicefeeDetailsPrintTemp.Add(f);
                        }
                        else
                        {
                            break;
                        }
                    }
                    invoicefeeDetailsPrint.Add(invoicefeeDetailsPrintTemp);
                    foreach (FeeItemList f in invoicefeeDetailsPrintTemp)
                    {
                        feeItemListsClone.Remove(f);
                    }
                }
            }
            #endregion

            string invoicePrintDll = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, string.Empty);
            // ���ķ�Ʊ��ӡ���ȡ��ʽ������ԭ����ʽ
            // 2011-08-04
            // �˴�������ʾ
            //if (invoicePrintDll == null || invoicePrintDll == "")
            //{
            //    MessageBox.Show("û�����÷�Ʊ��ӡ���������ܴ�ӡ!");

            //    return -1; ;
            //}

            #region ���߹Һ���Ϣ
            FS.HISFC.Models.Registration.Register rInfo = new FS.HISFC.Models.Registration.Register();
            Balance invoiceTemp = ((Balance)comBalances[0]);
            rInfo.PID.CardNO = invoiceTemp.Patient.PID.CardNO;
            rInfo.Pact = invoiceTemp.Patient.Pact.Clone();
            rInfo.Name = invoiceTemp.Patient.Name;
            rInfo.SSN = invoiceTemp.Patient.SSN; 
            #endregion


            ArrayList alPrintInvoicefeeDetails = new ArrayList();

            alPrintInvoicefeeDetails.Add(invoicefeeDetailsPrint);
            ArrayList alPrintInvoices = new ArrayList();

            alPrintInvoices.Add(invoiceDetailsPrint);
        
            //��ӡ
            if (this.IsRollCode)
            {
                returnValue = this.feeIntegrate.PrintInvoice(invoicePrintDll, rInfo, invoicesPrint, alPrintInvoices, feeDetailsPrint, alPrintInvoicefeeDetails, comBalancePays, false, ref errText);

            }
            else
            {
                returnValue = this.feeIntegrate.PrintInvoice(invoicePrintDll, rInfo, invoicesPrint, alPrintInvoices, feeDetailsPrint, alPrintInvoicefeeDetails, comBalancePays, true, ref errText);  
            }
            if (returnValue == -1)
            {
                MessageBox.Show(errText);
                return -1;
            }

            currentBalance = null;
            MessageBox.Show("�����ɹ�!");

            Clear();

            return 1;
        }
        /// <summary>
        /// �ش�
        /// </summary>
        /// <returns>�ɹ� true ʧ�� false</returns>
        protected virtual bool Print()
        {
            if (currentBalance == null)
            {
                MessageBox.Show("�÷�Ʊ�Ѿ�����!");
                this.tbInvoiceNo.Focus();
                this.tbInvoiceNo.SelectAll();

                return false;
            }
            if (currentBalance.Invoice.ID == "")
            {
                MessageBox.Show("�����뷢Ʊ��Ϣ!");
                this.tbInvoiceNo.Focus();
                this.tbInvoiceNo.SelectAll();
                
                return false;
            }

            bool isCanQuitOtherOper = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_REPRINT_OTHER_OPER_INVOICE, true, false);

            if(!isCanQuitOtherOper)//���������ش�
            {
                Balance tmpInvoice = comBalances[0] as Balance;

                if (tmpInvoice == null)
                {
                    MessageBox.Show("��Ʊ��ʽת������!");
                    tbInvoiceNo.SelectAll();
                    tbInvoiceNo.Focus();

                    return false;
                }

                if (tmpInvoice.BalanceOper.ID != this.outpatientManager.Operator.ID)
                {
                    MessageBox.Show("�÷�ƱΪ����Ա" + tmpInvoice.BalanceOper.ID + "��ȡ,��û��Ȩ�޽��ش�!");
                    tbInvoiceNo.SelectAll();
                    tbInvoiceNo.Focus();

                    return false;
                }
            }

            bool isCanReprintDayBalance = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_REPRINT_DAYBALANCED_INVOICE, true, false);

            if (!isCanReprintDayBalance)//���������ش�
            {
                Balance tmpInvoice = comBalances[0] as Balance;

                if (tmpInvoice == null)
                {
                    MessageBox.Show("��Ʊ��ʽת������!");
                    tbInvoiceNo.SelectAll();
                    tbInvoiceNo.Focus();

                    return false;
                }

                if (tmpInvoice.IsDayBalanced)
                {
                    MessageBox.Show("�÷�Ʊ�Ѿ��ս�,��û��Ȩ�޽��ش�!");
                    tbInvoiceNo.SelectAll();
                    tbInvoiceNo.Focus();

                    return false;
                }
            }
            //���߷�Ʊ�ţ�ԭ���ش򣬲����
            //{2322FA44-DF37-42fc-9DE4-FDA8322DC03D}
            if (!this.isRollCode)
            {
                if (this.PrintNotRollCode() == -1)
                {
                    MessageBox.Show(Language.Msg("�ش����!"));
                    return false;
                }
                return true;
            }
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            pharmacyIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            controlParamIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
          
            int returnValue = 0;
            string currentInvoiceNO = "";
            string currentRealInvoiceNO = "";
            string errText = "";
            DateTime nowTime = new DateTime();
            
            nowTime = outpatientManager.GetDateTimeFromSysDateTime();
            invoiceType = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");

            //ArrayList invoicesPrint = new ArrayList();
            //ArrayList invoiceDetailsPrintTemp = new ArrayList();
            //ArrayList invoiceDetailsPrint = new ArrayList();
            //ArrayList feeDetailsPrint = new ArrayList();

            //��Ʊ
            ArrayList invoicesPrint = new ArrayList();
            //��Ʊ��ϸ
            ArrayList invoiceDetailsPrintTemp = new ArrayList();
            //��Ʊ��ϸ
            ArrayList invoiceDetailsPrint = new ArrayList();
            //��Ʊ������ϸ
            ArrayList invoicefeeDetailsPrintTemp = new ArrayList();
            //��Ʊ������ϸ
            ArrayList invoicefeeDetailsPrint = new ArrayList();
            //ȫ��������ϸ
            ArrayList feeDetailsPrint = new ArrayList();


            //��ø���Ʊ��ˮ��
            string invoiceSeqNegative = outpatientManager.GetInvoiceCombNO();
            if (invoiceSeqNegative == null || invoiceSeqNegative == "")
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��÷�Ʊ��ˮ��ʧ��!" + outpatientManager.Err);

                return false;
            }

            //�������Ʊ��ˮ��
            string invoiceSeqPositive = outpatientManager.GetInvoiceCombNO();
            if (invoiceSeqPositive == null || invoiceSeqPositive == "")
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��÷�Ʊ��ˮ��ʧ��!" + outpatientManager.Err);

                return false;
            }

            Hashtable hsInvoice = new Hashtable();

            FS.HISFC.Models.Base.Employee employee = this.outpatientManager.Operator as FS.HISFC.Models.Base.Employee;

            foreach (Balance invoice in comBalances)
            {
                returnValue = this.feeIntegrate.GetInvoiceNO(employee,"C", ref currentInvoiceNO, ref currentRealInvoiceNO, ref errText);

                if (returnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(errText);

                    return false;
                }

                hsInvoice.Add(invoice.Invoice.ID, currentInvoiceNO);

                returnValue = outpatientManager.UpdateBalanceCancelType(invoice.Invoice.ID, invoice.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Reprint);
                if (returnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����ԭʼ��Ʊ��Ϣ����!" + outpatientManager.Err);

                    return false;
                }
                if (returnValue == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�÷�Ʊ�Ѿ�����!");

                    return false;
                }
                //���������Ϣ(����¼)
                invoice.PrintTime = invoice.BalanceOper.OperTime;
                Balance invoClone = invoice.Clone();
                invoClone.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                invoClone.FT.TotCost = -invoClone.FT.TotCost;
                invoClone.FT.OwnCost = -invoClone.FT.OwnCost;
                invoClone.FT.PayCost = -invoClone.FT.PayCost;
                invoClone.FT.PubCost = -invoClone.FT.PubCost;
                invoClone.CancelType = FS.HISFC.Models.Base.CancelTypes.Reprint;
                invoClone.CanceledInvoiceNO = invoice.Invoice.ID;
                invoClone.CancelOper.ID = outpatientManager.Operator.ID;
                invoClone.BalanceOper.ID = outpatientManager.Operator.ID;
                invoClone.BalanceOper.OperTime = nowTime;
                invoClone.CancelOper.OperTime = nowTime;
                invoClone.IsAuditing = false;
                invoClone.AuditingOper.ID = "";
                invoClone.AuditingOper.OperTime = DateTime.MinValue;
                invoClone.IsDayBalanced = false;
                invoClone.DayBalanceOper.OperTime = DateTime.MinValue;

                invoClone.CombNO = invoiceSeqNegative;

                returnValue = outpatientManager.InsertBalance(invoClone);
                if (returnValue <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���뷢Ʊ������Ϣ����!!" + outpatientManager.Err);
                    return false;
                }
                invoClone.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                invoClone.FT.TotCost = -invoClone.FT.TotCost;
                invoClone.FT.OwnCost = -invoClone.FT.OwnCost;
                invoClone.FT.PayCost = -invoClone.FT.PayCost;
                invoClone.FT.PubCost = -invoClone.FT.PubCost;
                invoClone.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                invoClone.CanceledInvoiceNO = invoice.Invoice.ID;
                invoClone.CancelOper.OperTime = DateTime.MinValue;
                invoClone.Invoice.ID = currentInvoiceNO;
                invoClone.PrintedInvoiceNO = currentRealInvoiceNO;
                invoClone.BalanceOper.ID = outpatientManager.Operator.ID;
                invoClone.BalanceOper.OperTime = nowTime;
                invoClone.CombNO = invoiceSeqPositive;

                invoicesPrint.Add(invoClone);

                returnValue = outpatientManager.InsertBalance(invoClone);
                if (returnValue <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("�����·�Ʊ��Ϣ����!" + outpatientManager.Err);

                    return false;
                }
  
                //����Ʊ��ϸ����Ϣ
                ArrayList alInvoceDetail = outpatientManager.QueryBalanceListsByInvoiceNOAndInvoiceSequence(invoice.Invoice.ID, currentBalance.CombNO);
                if (comBalanceLists == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("��÷�Ʊ��ϸ����!" + outpatientManager.Err);

                    return false;
                }
                //���Ϸ�Ʊ��ϸ����Ϣ
                returnValue = outpatientManager.UpdateBalanceListCancelType(invoice.Invoice.ID, currentBalance.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Reprint);
                if (returnValue <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���Ϸ�Ʊ��ϸ����!" + outpatientManager.Err);

                    return false;
                }
                invoiceDetailsPrintTemp = new ArrayList();
                foreach (BalanceList d in alInvoceDetail)
                {
                    d.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                    d.BalanceBase.FT.OwnCost = -d.BalanceBase.FT.OwnCost;
                    d.BalanceBase.FT.PubCost = -d.BalanceBase.FT.PubCost;
                    d.BalanceBase.FT.PayCost = -d.BalanceBase.FT.PayCost;
                    d.BalanceBase.BalanceOper.OperTime = nowTime;
                    d.BalanceBase.BalanceOper.ID = outpatientManager.Operator.ID;
                    d.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Reprint;
                    d.BalanceBase.IsDayBalanced = false;
                    d.BalanceBase.DayBalanceOper.ID = "";
                    d.BalanceBase.DayBalanceOper.OperTime = DateTime.MinValue;
                    ((Balance)d.BalanceBase).CombNO = invoiceSeqNegative;
                    //{9D9D4A6E-84D2-4c07-B6F0-5F2C8DB1DFD7}
                    d.FeeCodeStat.SortID = d.InvoiceSquence;

                    returnValue = outpatientManager.InsertBalanceList(d);

                    if (returnValue <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���뷢Ʊ��ϸ������Ϣ����!" + outpatientManager.Err);

                        return false;
                    }
                    d.BalanceBase.Invoice.ID = currentInvoiceNO;
                    d.BalanceBase.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                    d.BalanceBase.FT.OwnCost = -d.BalanceBase.FT.OwnCost;
                    d.BalanceBase.FT.PubCost = -d.BalanceBase.FT.PubCost;
                    d.BalanceBase.FT.PayCost = -d.BalanceBase.FT.PayCost;
                    d.BalanceBase.BalanceOper.OperTime = nowTime;
                    d.BalanceBase.BalanceOper.ID = outpatientManager.Operator.ID;
                    d.BalanceBase.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    ((Balance)d.BalanceBase).CombNO = invoiceSeqPositive;
                    d.BalanceBase.FT.TotCost = d.BalanceBase.FT.OwnCost + d.BalanceBase.FT.PayCost + d.BalanceBase.FT.PubCost;
                   
                    returnValue = outpatientManager.InsertBalanceList(d);
                    if (returnValue <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�����·�Ʊ��ϸ��Ϣ����!" + outpatientManager.Err);

                        return false;
                    }

                    invoiceDetailsPrintTemp.Add(d);
                }

                invoiceDetailsPrint.Add(invoiceDetailsPrintTemp);

                if (invoiceType == "2")
                {
                    returnValue = this.feeIntegrate.UpdateOnlyRealInvoiceNO(currentInvoiceNO, currentRealInvoiceNO, ref errText);
                    if (returnValue <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���²���Ա��Ʊʧ��!" + feeIntegrate.Err);

                        return false;
                    }
                }
                else
                {
                    returnValue = this.feeIntegrate.UpdateInvoiceNO(currentInvoiceNO, currentRealInvoiceNO, ref errText);
                    if (returnValue <= 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("���²���Ա��Ʊʧ��!" + feeIntegrate.Err);

                        return false;
                    }
                }
            }
            //����֧����Ϣ
            comBalancePays = outpatientManager.QueryBalancePaysByInvoiceSequence(currentBalance.CombNO);
            if (comBalancePays == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��û���֧����Ϣ����" + outpatientManager.Err);

                return false;
            }
            returnValue = outpatientManager.UpdateCancelTyeByInvoiceSequence("4", currentBalance.CombNO, FS.HISFC.Models.Base.CancelTypes.Reprint);
            if (returnValue < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("����֧����ʽ����!" + outpatientManager.Err);

                return false;
            }
            foreach (BalancePay p in comBalancePays)
            {
                p.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                p.FT.TotCost = -p.FT.TotCost;
                p.FT.RealCost = -p.FT.RealCost;
                p.InputOper.OperTime = nowTime;
                p.InputOper.ID = outpatientManager.Operator.ID;
                p.InvoiceCombNO = invoiceSeqNegative;
                p.CancelType = FS.HISFC.Models.Base.CancelTypes.Reprint;
                p.IsChecked = false;
                p.CheckOper.ID = "";
                p.CheckOper.OperTime = DateTime.MinValue;
                p.BalanceOper.ID = "";
                p.BalanceOper.OperTime = DateTime.MinValue;
                p.IsDayBalanced = false;
                p.IsAuditing = false;
                p.AuditingOper.OperTime = DateTime.MinValue;
                p.AuditingOper.ID = "";

                returnValue = outpatientManager.InsertBalancePay(p);
                if (returnValue <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����֧������Ϣ����!" + outpatientManager.Err);

                    return false;
                }
                p.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                p.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                p.FT.TotCost = -p.FT.TotCost;
                p.FT.RealCost = -p.FT.RealCost;
                p.InvoiceCombNO = invoiceSeqPositive;
                p.Invoice.ID = currentInvoiceNO;
                returnValue = outpatientManager.InsertBalancePay(p);

                if (returnValue <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("����֧����Ϣ����!" + outpatientManager.Err);

                    return false;
                }
            }

            //���������ϸ��Ϣ
            ArrayList feeItemLists = outpatientManager.QueryFeeItemListsByInvoiceSequence(currentBalance.CombNO);
            if (feeItemLists == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("��û��߷�����ϸ����!" + outpatientManager.Err);

                return false;
            }
            returnValue = outpatientManager.UpdateFeeItemListCancelType(currentBalance.CombNO, nowTime, FS.HISFC.Models.Base.CancelTypes.Reprint);
            if (returnValue <= 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("���ϻ�����ϸ����!" + outpatientManager.Err);

                return false;
            }
            
            foreach (FeeItemList f in feeItemLists)
            {
                f.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                f.FT.OwnCost = -f.FT.OwnCost;
                f.FT.PayCost = -f.FT.PayCost;
                f.FT.PubCost = -f.FT.PubCost;
                f.Item.Qty = -f.Item.Qty;
                f.CancelType = FS.HISFC.Models.Base.CancelTypes.Reprint;
                f.FeeOper.ID = outpatientManager.Operator.ID;
                f.FeeOper.OperTime = nowTime;
                f.CancelOper.OperTime = nowTime;
                f.InvoiceCombNO = invoiceSeqNegative;

                returnValue = outpatientManager.InsertFeeItemList(f);
                if (returnValue <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���������ϸ������Ϣ����!" + outpatientManager.Err);

                    return false;
                }
            }
            
            foreach (FeeItemList f in feeItemLists)
            {

                f.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                f.FT.OwnCost = -f.FT.OwnCost;
                f.FT.PayCost = -f.FT.PayCost;
                f.FT.PubCost = -f.FT.PubCost;
                f.Item.Qty = -f.Item.Qty;
                f.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                f.FeeOper.ID = outpatientManager.Operator.ID;
                f.FeeOper.OperTime = nowTime;
                f.CancelOper.OperTime = nowTime;
                f.Invoice.ID = currentInvoiceNO;
                f.InvoiceCombNO = invoiceSeqPositive;

                returnValue = outpatientManager.InsertFeeItemList(f);
                if (returnValue <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("���������ϸ��Ϣ����!" + outpatientManager.Err);

                    return false;
                }
                feeDetailsPrint.Add(f);
            }
            #region ���ɸ�ֵ��ķ�Ʊ������ϸ
            foreach (Balance b in invoicesPrint)
            {

                #region ��¡һ��������ϸ��Ϣ�б���Ϊ���������Ҫ���б�Ԫ����ɾ��������
                ArrayList feeItemListsClone = new ArrayList();
                foreach (FeeItemList f in feeItemLists)
                {
                    feeItemListsClone.Add(f.Clone());
                }
                #endregion

                while (feeItemListsClone.Count > 0)
                {
                    invoicefeeDetailsPrintTemp = new ArrayList();
                    string compareItem = b.Invoice.ID;
                    foreach (FeeItemList f in feeItemListsClone)
                    {
                        if (f.Invoice.ID == compareItem)
                        {
                            invoicefeeDetailsPrintTemp.Add(f);
                        }
                        else
                        {
                            break;
                        }
                    }
                    invoicefeeDetailsPrint.Add(invoicefeeDetailsPrintTemp);
                    foreach (FeeItemList f in invoicefeeDetailsPrintTemp)
                    {
                        feeItemListsClone.Remove(f);
                    }
                }
            }
            #endregion

            string invoicePrintDll = this.feeIntegrate.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, string.Empty);
            // ���ķ�Ʊ��ӡ���ȡ��ʽ������ԭ����ʽ
            // 2011-08-04
            // �˴�������ʾ
            //if (invoicePrintDll == null || invoicePrintDll == "")
            //{
            //    MessageBox.Show("û�����÷�Ʊ��ӡ���������ܴ�ӡ!");

            //    return false;
            //}

            FS.HISFC.Models.Registration.Register rInfo = new FS.HISFC.Models.Registration.Register();
            Balance invoiceTemp = ((Balance)comBalances[0]);
            rInfo.PID.CardNO = invoiceTemp.Patient.PID.CardNO;
            rInfo.Pact = invoiceTemp.Patient.Pact.Clone();
            rInfo.Name = invoiceTemp.Patient.Name;
            rInfo.SSN = invoiceTemp.Patient.SSN;

            #region 
            ArrayList alPrintInvoicefeeDetails = new ArrayList();

            alPrintInvoicefeeDetails.Add(invoicefeeDetailsPrint);
            ArrayList alPrintInvoices = new ArrayList();

            alPrintInvoices.Add(invoiceDetailsPrint);
            #endregion

            foreach (Balance invo in comBalances)
            {
                if (this.pharmacyIntegrate.UpdateDrugRecipeInvoiceN0(invo.Invoice.ID, hsInvoice[invo.Invoice.ID].ToString()) < 0)
                {
                    MessageBox.Show("���ݾɷ�Ʊ�Ÿ����·�Ʊ�ų���");
                    FS.FrameWork.Management.PublicTrans.RollBack();

                    return false;
                }
            }
            if (this.IsRollCode)
            {
                returnValue = this.feeIntegrate.PrintInvoice(invoicePrintDll, rInfo, invoicesPrint, alPrintInvoices, feeDetailsPrint, alPrintInvoicefeeDetails, comBalancePays, false, ref errText);
            }
            else
            {
                returnValue = this.feeIntegrate.PrintInvoice(invoicePrintDll, rInfo, invoicesPrint, alPrintInvoices, feeDetailsPrint, alPrintInvoicefeeDetails, comBalancePays, true, ref errText);
            }
            if (returnValue == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(errText);

                return false;
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            currentBalance = null;
            MessageBox.Show("�����ɹ�!");

            Clear();

            return true;
        }
        /// <summary>
        /// ��ʾ��Ʊ������Ϣ
        /// </summary>
        /// <param name="invoiceCombNO"></param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        private bool FillBalanceLists(string invoiceCombNO)
        {
            comBalanceLists = outpatientManager.QueryBalanceListsByInvoiceSequence(invoiceCombNO);

            if (comBalanceLists == null)
            {
                return false;
            }

            BalanceList balanceList = new BalanceList();
            for (int i = 0; i < comBalanceLists.Count; i++)
            {
                balanceList = comBalanceLists[i] as BalanceList;
                if (i > 4)
                {
                    this.fpSpread1_Sheet1.Rows.Add(i, 1);
                }
                this.fpSpread1_Sheet1.Cells[i, 0].Text = balanceList.FeeCodeStat.Name;
                this.fpSpread1_Sheet1.Cells[i, 1].Text = balanceList.BalanceBase.FT.OwnCost.ToString();
                this.fpSpread1_Sheet1.Cells[i, 2].Text = balanceList.BalanceBase.FT.PayCost.ToString();
                this.fpSpread1_Sheet1.Cells[i, 3].Text = balanceList.BalanceBase.FT.PubCost.ToString();
                this.fpSpread1_Sheet1.Cells[i, 4].Text = (balanceList.BalanceBase.FT.OwnCost + balanceList.BalanceBase.FT.PayCost + balanceList.BalanceBase.FT.PubCost).ToString();
            }

            return true;
        }

        #endregion

        #region �¼�

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
            }

            return base.ProcessDialogKey(keyData);
        }

        private void tbInvoiceNo_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                QueryBalances();
            }
        }

        private void btExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btOk_Click(object sender, System.EventArgs e)
        {
            DialogResult result = MessageBox.Show("�Ƿ�Ҫ�ش�Ʊ?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (result == DialogResult.Yes)
            {
                this.Print();
            }
        }

        private void frmReprint_Load(object sender, System.EventArgs e)
        {
            //��ȡ�ش�Ʊ�Ƿ��ߺ�{2322FA44-DF37-42fc-9DE4-FDA8322DC03D}
            this.isRollCode = this.controlParamIntegrate.GetControlParam<bool>("MZ2010", true, true);
            try
            {
                string setDefaultInvoice = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.REPRINT_SET_DEFAULT_INVOICE, false, "0");
                if (setDefaultInvoice == "1")//��ҪĬ��
                {
                    string invoiceNO = "";
                    string realInvoiceNO = "";
                    string nextInvoiceNO = "";
                    string nextRealInvoiceNO = "";
                    string errText = "";
                    string invoiceType = string.Empty;

                    FS.HISFC.Models.Base.Employee employee = this.managerIntegrate.GetEmployeeInfo(this.outpatientManager.Operator.ID);
                   
                    invoiceType = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");

                    if (invoiceType == "2") //Ĭ��ȡ��Ʊ��ҪTrans֧��,��ʱ������.
                    {
                        return;
                    }

                    int iReturn = this.feeIntegrate.GetInvoiceNO(employee,"C", ref invoiceNO, ref realInvoiceNO, ref errText);
                    if (iReturn < 0)
                    {
                        MessageBox.Show(errText);
                        this.tbInvoiceNo.Focus();

                        return;
                    }


                    iReturn = this.feeIntegrate.GetNextInvoiceNO(invoiceType, invoiceNO, realInvoiceNO, ref nextInvoiceNO, ref nextRealInvoiceNO, -1, ref errText);

                    this.tbInvoiceNo.Text = nextInvoiceNO;
                    this.tbInvoiceNo.Focus();
                    this.tbInvoiceNo.SelectAll();
                }
            }
               
            catch { }
        }

        #endregion
    }
}