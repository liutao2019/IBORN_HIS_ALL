using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.OutpatientFee.GuangZhou.Zdly.IOutpatientPopupFee
{
    public partial class ucSplitInvoice : UserControl
    {
        public ucSplitInvoice()
        {
            InitializeComponent();
        }

        #region ����

        ucSplitUnit splitUnit = new ucSplitUnit();
        private FS.HISFC.Models.Fee.Outpatient.Balance invoice = new FS.HISFC.Models.Fee.Outpatient.Balance();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
        private FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParam = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        private ArrayList invoiceDetails = new ArrayList();
        private string beginInvoiceNo = "";//��ʼ��Ʊ��
        private string beginRealInvoiceNo = "";//��ʼʵ�ʷ�Ʊ��
        private string endInvoiceNo = "";//������Ʊ��
        private string endRealInvoiceNo = "";//����ʵ�ʷ�Ʊ��
        private string invoiceType = "";//��Ʊ���
        private bool isAuto = false;//�Ƿ��Զ���Ʊ
        private int count = 0;//��Ʊ����
        private ArrayList splitInvoices = new ArrayList();//�ַ�Ʊ�������Ʊ����
        private ArrayList splitInvoiceDetails = new ArrayList();//�ַ�Ʊ��ķ�Ʊ��ϸ
        private int days = 1;//�������
        private bool isConfirm = false; //�Ƿ�ȷ�Ϸ�Ʊ
        private string invoiceNoType;//��Ʊ�ŷ�ʽ
        private string STATCODEJC = string.Empty;
        private string STATCODEZL = string.Empty;

        #endregion

        #region ����

        /// <summary>
        /// ��Ʊ�ŷ�ʽ
        /// </summary>
        public string InvoiceNoType
        {
            set
            {
                invoiceNoType = value;
            }
        }

        /// <summary>
        /// �Ƿ�ȷ�Ϸ�Ʊ
        /// </summary>
        public bool IsConfirm
        {
            get
            {
                return this.isConfirm;
            }
        }

        /// <summary>
        /// ȷ������
        /// </summary>
        public int Days
        {
            set
            {
                days = value;
            }
        }

        /// <summary>
        /// �ַ�Ʊ�������Ʊ����
        /// </summary>
        public ArrayList SplitInvoices
        {
            get
            {
                splitInvoices = GetSplitInvoices("1");
                return splitInvoices;
            }
        }

        /// <summary>
        /// �ַ�Ʊ��ķ�Ʊ��ϸ
        /// </summary>
        public ArrayList SplitInvoiceDetails
        {
            get
            {
                splitInvoiceDetails = GetSplitInvoices("2");
                return splitInvoiceDetails;
            }
        }

        /// <summary>
        /// ��Ʊ����
        /// </summary>
        public int Count
        {
            set
            {
                count = value;
                this.tbCount.Text = count.ToString();
            }
        }

        /// <summary>
        /// �Ƿ��Զ���Ʊ
        /// </summary>
        public bool IsAuto
        {
            set
            {
                isAuto = value;

                this.cbAuto.Checked = isAuto;
            }
        }

        /// <summary>
        /// ��Ʊ���
        /// </summary>
        public string InvoiceType
        {
            set
            {
                invoiceType = value;
            }
        }

        /// <summary>
        /// ��ʼ��Ʊ��
        /// </summary>
        public string BeginInvoiceNo
        {
            set
            {
                beginInvoiceNo = value;
                if (beginInvoiceNo == null || beginInvoiceNo == "")
                {
                    MessageBox.Show("�����뷢Ʊ��!");
                    return;
                }
            }
        }

        /// <summary>
        /// ��ʼʵ�ʷ�Ʊ��
        /// </summary>
        public string BeginRealInvoiceNo
        {
            set
            {
                beginRealInvoiceNo = value;
            }
        }

        /// <summary>
        /// ������Ʊ��
        /// </summary>
        public string EndInvoiceNo
        {
            get
            {
                return endInvoiceNo;
            }
        }

        /// <summary>
        /// ����ʵ�ʷ�Ʊ��
        /// </summary>
        public string EndRealInvoiceNo
        {
            get
            {
                return endRealInvoiceNo;
            }
        }

        /// <summary>
        /// ��Ʊ��ϸʵ��
        /// </summary>
        ///
        public ArrayList InvoiceDetails
        {
            get
            {
                return invoiceDetails;
            }
            set
            {
                invoiceDetails = value;
            }
        }

        /// <summary>
        /// ��Ʊʵ��
        /// </summary>
        public FS.HISFC.Models.Fee.Outpatient.Balance Invoice
        {
            get
            {
                return invoice;
            }
            set
            {
                invoice = value;
            }
        }

        #endregion

        #region ����
        /// <summary>
        /// ��õ�����ķ�Ʊ��Ϣ
        /// </summary>
        /// <param name="flag">1����Ʊ���� 2 ��Ʊ��ϸ��Ϣ</param>
        /// <returns></returns>
        public ArrayList GetSplitInvoices(string flag)
        {
            ArrayList alTempAll = new ArrayList();
            ArrayList alTempInvoice = new ArrayList();
            ArrayList alTempInvoiceDetails = new ArrayList();
            for (int i = 0; i < this.plSplitUnits.Controls.Count; i++)
            {
                try
                {
                    ((ucSplitUnit)plSplitUnits.Controls[i]).Invoice.PrintTime = ((ucSplitUnit)plSplitUnits.Controls[i]).Invoice.PrintTime;
                    
                    alTempInvoice.Add(((ucSplitUnit)plSplitUnits.Controls[i]).Invoice);

                    alTempInvoiceDetails.Add(((ucSplitUnit)plSplitUnits.Controls[i]).InvoiceDetails);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            if (flag == "1")//����Ʊ��Ϣ
            {
                return alTempInvoice;
            }
            else
            {
                return alTempInvoiceDetails;
            }
        }
        /// <summary>
        /// Ĭ�Ϸַ�Ʊ
        /// </summary>
        /// <param name="count">��Ʊ������</param>
        /// <param name="splitFlag">���� 1�Զ� 0 �Զ���</param>
        public void AddInvoiceUnits(int count, string splitFlag)
        {
            //��տؼ�
            ClearInvoicUnits();

            int xPoint = 0, yPoint = 0;
            DateTime dtTempPrint = this.invoice.PrintTime;
            for (int i = 0; i < count; i++)
            {
                splitUnit = new ucSplitUnit();

                if (Math.IEEERemainder(i, 2) == 0)
                {
                    xPoint = 28;
                }
                else
                {
                    xPoint = 16 * 2 + splitUnit.Width + 25;
                }

                yPoint = 14 + (i / 2) * (16 + splitUnit.Height);
                if (i > 0)
                {
                    dtTempPrint = dtTempPrint.AddDays(this.days);
                }
                splitUnit.Location = new System.Drawing.Point(xPoint, yPoint);
                splitUnit.Name = "splitUnit" + i.ToString();
                splitUnit.InvoiceDate = dtTempPrint;
                splitUnit.TabIndex = i + 1;
                splitUnit.CostChanged += new ucSplitUnit.ChangeCost(splitUnit_CostChanged);
                splitUnit.ModifyFinished += new ucSplitUnit.ChangeFocus(splitUnit_ModifyFinished);
                if (splitFlag == "1")//�Զ��ַ�Ʊ
                {
                    splitUnit.IsLast = true;
                }
                else
                {
                    if (i == count - 1)
                    {
                        splitUnit.IsLast = true;
                    }
                    else
                    {
                        splitUnit.IsLast = false;
                    }
                }

                this.plSplitUnits.Controls.Add(splitUnit);
            }

            this.ucInvoicePreviewGF1.InvoiceType = invoiceType;
            ArrayList alTempInvoice = new ArrayList();
            alTempInvoice.Add(invoice);
            ArrayList alTempInvoiceAndDetails = new ArrayList();
            alTempInvoiceAndDetails.Add(alTempInvoice);
            ArrayList tempInvoiceDetails = new ArrayList();
            ArrayList tempInvoiceDetailsTow = new ArrayList();
            tempInvoiceDetailsTow.Add(invoiceDetails);
            tempInvoiceDetails.Add(tempInvoiceDetailsTow);

            alTempInvoiceAndDetails.Add(tempInvoiceDetails);
            this.ucInvoicePreviewGF1.InvoiceAndDetails = alTempInvoiceAndDetails;

            ArrayList tempSplit = new ArrayList();
            decimal errorOwnCost = 0, errorPubCost = 0, errorTotCost = 0, errorPayCost = 0;//�������Ʊʱ�����
            decimal errCTFee = 0, errMRIFee = 0, errSXFee = 0, errSYFee = 0;//CT�ѵ����
            decimal sumOwnCost = 0, sumPubCost = 0, sumTotCost = 0, sumPayCost = 0;//���ݷ�Ʊ��ϸ���ܵ÷�Ʊ���
            FS.HISFC.Models.Fee.Outpatient.Balance tempInvoice = null;

            FS.HISFC.Models.Fee.Outpatient.BalanceList detailTemp = null;
            ArrayList invoiceDetailsSplit = new ArrayList();
            ArrayList invoicesSplit = new ArrayList();
            int iReturn = 0;
            for (int i = 0; i < count; i++)
            {
                ArrayList invoiceDetailsToInvoice = new ArrayList();
                string beginTempInvoiceNo = "";
                string beginTempRealInvoiceNo = "";
                string errText = "";

                if (invoiceNoType == "2")//��ͨģʽ��ҪTrans֧��
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    iReturn = this.feeIntegrate.GetNextInvoiceNO(invoiceNoType, beginInvoiceNo, beginRealInvoiceNo, ref beginTempInvoiceNo, ref beginTempRealInvoiceNo, i + 1, ref errText);
                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);
                        
                        this.plSplitUnits.Controls.Clear();

                        return;
                    }

                    FS.FrameWork.Management.PublicTrans.RollBack();//��Ϊ��ʱ��һ���������ݿ�,���Իع�,���ַ�Ʊ������
                }
                else
                {
                    iReturn = this.feeIntegrate.GetNextInvoiceNO(this.invoiceNoType, beginInvoiceNo, this.beginRealInvoiceNo, ref beginTempInvoiceNo, ref beginTempRealInvoiceNo, i, ref errText);
                    if (iReturn < 0)
                    {
                        MessageBox.Show(errText);
                        return;
                    }
                }
                foreach (FS.HISFC.Models.Fee.Outpatient.BalanceList detail in invoiceDetails)
                {
                    detailTemp = detail.Clone();

                    detailTemp.BalanceBase.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((detail.BalanceBase.FT.TotCost / (decimal)count), 2);
                    detailTemp.BalanceBase.FT.PayCost = FS.FrameWork.Public.String.FormatNumber((detail.BalanceBase.FT.PayCost / (decimal)count), 2);
                    detailTemp.BalanceBase.FT.OwnCost = FS.FrameWork.Public.String.FormatNumber((detail.BalanceBase.FT.OwnCost / (decimal)count), 2);
                    detailTemp.BalanceBase.FT.PubCost = FS.FrameWork.Public.String.FormatNumber((detail.BalanceBase.FT.PubCost / (decimal)count), 2);
                    detailTemp.CTFee = FS.FrameWork.Public.String.FormatNumber((detail.CTFee / (decimal)count), 2);
                    detailTemp.MRIFee = FS.FrameWork.Public.String.FormatNumber((detail.MRIFee / (decimal)count), 2);
                    detailTemp.SXFee = FS.FrameWork.Public.String.FormatNumber((detail.SXFee / (decimal)count), 2);
                    detailTemp.SYFee = FS.FrameWork.Public.String.FormatNumber((detail.SYFee / (decimal)count), 2);
                    detailTemp.BalanceBase.Invoice.ID = beginTempInvoiceNo;

                    errorOwnCost = detail.BalanceBase.FT.OwnCost - detailTemp.BalanceBase.FT.OwnCost * count;
                    errorPubCost = detail.BalanceBase.FT.PubCost - detailTemp.BalanceBase.FT.PubCost * count;
                    errorPayCost = detail.BalanceBase.FT.PayCost - detailTemp.BalanceBase.FT.PayCost * count;
                    errorTotCost = detail.BalanceBase.FT.TotCost - detailTemp.BalanceBase.FT.TotCost * count;
                    errCTFee = detail.CTFee - detailTemp.CTFee * count;
                    errMRIFee = detail.MRIFee - detailTemp.MRIFee * count;
                    errSXFee = detail.SXFee - detailTemp.SXFee * count;
                    errSYFee = detail.SYFee - detailTemp.SYFee * count;

                    if (i == 0)
                    {
                        detailTemp.BalanceBase.FT.TotCost = detailTemp.BalanceBase.FT.TotCost + errorTotCost;
                        detailTemp.BalanceBase.FT.PayCost = detailTemp.BalanceBase.FT.PayCost + errorPayCost;
                        detailTemp.BalanceBase.FT.OwnCost = detailTemp.BalanceBase.FT.OwnCost + errorOwnCost;
                        detailTemp.BalanceBase.FT.PubCost = detailTemp.BalanceBase.FT.PubCost + errorPubCost;

                        detailTemp.CTFee += errCTFee;
                        detailTemp.MRIFee += errMRIFee;
                        detailTemp.SXFee += errSXFee;
                        detailTemp.SYFee += errSYFee;
                    }

                    invoiceDetailsToInvoice.Add(detailTemp);
                }

                invoiceDetailsSplit.Add(invoiceDetailsToInvoice);
            }
            for (int i = 0; i < count; i++)
            {
                tempInvoice = invoice.Clone();

                string beginTempInvoiceNo = "";
                string beginTempRealInvoiceNo = "";
                string errText = "";

                if (invoiceNoType == "2")//��ͨģʽ��ҪTrans֧��
                {
                    FS.FrameWork.Management.PublicTrans.BeginTransaction();
                    this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                    iReturn = this.feeIntegrate.GetNextInvoiceNO(invoiceNoType, beginInvoiceNo, this.beginRealInvoiceNo, ref beginTempInvoiceNo, ref beginTempRealInvoiceNo, i + 1, ref errText);
                    if (iReturn < 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(errText);

                        this.plSplitUnits.Controls.Clear();

                        return;
                    }

                    beginTempRealInvoiceNo = (FS.FrameWork.Function.NConvert.ToInt32(beginTempRealInvoiceNo) - 1).ToString();

                    FS.FrameWork.Management.PublicTrans.RollBack();//��Ϊ��ʱ��һ���������ݿ�,���Իع�,���ַ�Ʊ������
                }
                else
                {
                    iReturn = this.feeIntegrate.GetNextInvoiceNO(this.invoiceNoType, beginInvoiceNo, this.beginRealInvoiceNo, ref beginTempInvoiceNo, ref beginTempRealInvoiceNo, i, ref errText);
                    if (iReturn < 0)
                    {
                        MessageBox.Show(errText);
                        return;
                    }
                }
                //���¼�����
                ArrayList invoiceDetailsTemp = invoiceDetailsSplit[i] as ArrayList;
                foreach (FS.HISFC.Models.Fee.Outpatient.BalanceList detail in invoiceDetailsTemp)
                {
                    sumOwnCost += detail.BalanceBase.FT.OwnCost;
                    sumPubCost += detail.BalanceBase.FT.PubCost;
                    sumPayCost += detail.BalanceBase.FT.PayCost;
                    sumTotCost += detail.BalanceBase.FT.TotCost;
                }

                tempInvoice.FT.TotCost = sumTotCost;
                tempInvoice.FT.PayCost = sumPayCost;
                tempInvoice.FT.OwnCost = sumOwnCost;
                tempInvoice.FT.PubCost = sumPubCost;
                tempInvoice.Invoice.ID = beginTempInvoiceNo;
                tempInvoice.PrintedInvoiceNO = beginTempRealInvoiceNo;

                sumTotCost = 0;
                sumPayCost = 0;
                sumOwnCost = 0;
                sumPubCost = 0;

                invoicesSplit.Add(tempInvoice);
            }
            //�����Ϣ���ؼ�.
            for (int i = 0; i < count; i++)
            {
                try
                {
                    ((ucSplitUnit)plSplitUnits.Controls[i]).Invoice = invoicesSplit[i] as FS.HISFC.Models.Fee.Outpatient.Balance;
                    ((ucSplitUnit)plSplitUnits.Controls[i]).InvoiceDetails = invoiceDetailsSplit[i] as ArrayList;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void splitUnit_ModifyFinished(int seq)
        {
            if (seq == count)
            {
                seq = 1;
            }
            foreach (Control ctrl in this.plSplitUnits.Controls)
            {
                if (((ucSplitUnit)ctrl).SeqNO == seq)
                {
                    ((ucSplitUnit)ctrl).IsFocus = true;
                }
            }
        }

        bool splitUnit_CostChanged(string feeStat, decimal orgCost, decimal newCost, ref FS.HISFC.Models.Base.FT ft, ref decimal CTFee, ref decimal MRIFee, ref decimal SXFee, ref decimal SYFee)
        {
             ucSplitUnit tempLastSpitUnit = new ucSplitUnit();
            foreach (Control crtl in this.plSplitUnits.Controls)
            {
                if (((ucSplitUnit)crtl).IsLast)
                {
                    tempLastSpitUnit = (ucSplitUnit)crtl;
                    ArrayList detailsTemp = ((ucSplitUnit)crtl).InvoiceDetails;
                    FS.HISFC.Models.Fee.Outpatient.Balance invoiceTemp = ((ucSplitUnit)crtl).Invoice;
                    foreach (FS.HISFC.Models.Fee.Outpatient.BalanceList detail in detailsTemp)
                    {
                        if (detail.FeeCodeStat.ID == feeStat)
                        {
                            if (detail.BalanceBase.FT.TotCost + orgCost < newCost)
                            {
                                MessageBox.Show("��Ʊ�Ľ����ڿɷֽ��!");

                                return false;
                            }
                            else
                            {
                                decimal errCost = newCost - orgCost;

                                if (feeStat == this.STATCODEJC)//�����Ŀ
                                {
                                    detail.CTFee += CTFee;
                                    detail.MRIFee += MRIFee;
                                    MRIFee = 0;
                                    CTFee = 0;

                                }
                                if (feeStat == this.STATCODEZL)//�����Ŀ
                                {
                                    detail.SXFee += SXFee;
                                    detail.SYFee += SYFee;
                                    SXFee = 0;
                                    SYFee = 0;
                                }

                                detail.BalanceBase.FT.TotCost = detail.BalanceBase.FT.TotCost - errCost;
                                detail.BalanceBase.FT.OwnCost = detail.BalanceBase.FT.OwnCost - errCost;

                                invoiceTemp.FT.TotCost = invoiceTemp.FT.TotCost - errCost;
                                invoiceTemp.FT.OwnCost = invoiceTemp.FT.OwnCost - errCost;

                                if (feeStat == this.STATCODEJC)//���
                                {
                                    if ((detail.BalanceBase.FT.TotCost) < detail.CTFee + detail.MRIFee)
                                    {
                                        decimal tempCost = 0;
                                        decimal errCostOut = detail.CTFee + detail.MRIFee - detail.BalanceBase.FT.TotCost;
                                        tempCost = errCost - detail.CTFee;
                                        if (tempCost <= 0)
                                        {
                                            CTFee = tempCost;
                                            detail.CTFee = detail.CTFee - tempCost;
                                            MRIFee = 0;
                                        }
                                        else
                                        {
                                            CTFee = detail.CTFee;
                                            detail.CTFee = 0;
                                            MRIFee = tempCost - detail.CTFee;
                                            detail.MRIFee = detail.MRIFee - MRIFee;
                                        }
                                    }
                                }
                                if (feeStat == this.STATCODEZL)//���
                                {
                                    if ((detail.BalanceBase.FT.TotCost) < detail.SYFee + detail.SXFee)
                                    {
                                        decimal tempCost = 0;
                                        decimal errCostOut = detail.SYFee + detail.SXFee - detail.BalanceBase.FT.TotCost;
                                        tempCost = errCost - detail.SYFee;
                                        if (tempCost <= 0)
                                        {
                                            SYFee = tempCost;
                                            SXFee = 0;
                                        }
                                        else
                                        {
                                            SYFee = detail.SYFee;
                                            SXFee = tempCost - detail.SYFee;
                                        }
                                    }
                                }
                            }

                            tempLastSpitUnit.Invoice = invoiceTemp;
                            tempLastSpitUnit.InvoiceDetails = detailsTemp;
                        }
                    }
                }
            }

            return true;
        }
        /// <summary>
        /// �����ʾ�Ŀؼ�
        /// </summary>
        private void ClearInvoicUnits()
        {
            plSplitUnits.Controls.Clear();
            plSplitUnits.Refresh();
        }

        #endregion

        #region �¼�
        private void cbAuto_CheckedChanged(object sender, System.EventArgs e)
        {
            isAuto = this.cbAuto.Checked;
            string temp = this.tbCount.Text.Trim();
            int tempCount = 0;
            try
            {
                tempCount = FS.FrameWork.Function.NConvert.ToInt32(temp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("��Ʊ���������벻�Ϸ�!" + ex.Message);
                return;
            }
            if (tempCount == 0)
            {
                MessageBox.Show("��Ʊ�������ܵ���0");
                return;
            }
            if (tempCount > 9)
            {
                MessageBox.Show("��Ʊ�������ܴ���9");
                return;
            }
            string tempFlag = null;
            if (isAuto)
            {
                tempFlag = "1";
            }
            else
            {
                tempFlag = "0";
            }
            this.AddInvoiceUnits(tempCount, tempFlag);
        }

        private void btnSplit_Click(object sender, System.EventArgs e)
        {
            string temp = this.tbCount.Text.Trim();
            int tempCount = 0;
            try
            {
                tempCount = Convert.ToInt32(temp);
            }
            catch (Exception ex)
            {
                MessageBox.Show("��Ʊ���������벻�Ϸ�!" + ex.Message);
                return;
            }
            if (tempCount <= 0)
            {
                MessageBox.Show("��Ʊ��������С�ڻ��ߵ���0");
                return;
            }

            int splitCounts = this.controlParam.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.Const.SPLITCOUNTS, false, 9);

            if (tempCount > splitCounts)
            {
                MessageBox.Show("��Ʊ�������ܴ���9");
                return;
            }
            string tempFlag = null;
            if (isAuto)
            {
                tempFlag = "1";
            }
            else
            {
                tempFlag = "0";
            }
            this.AddInvoiceUnits(tempCount, tempFlag);
        }

        private void tbOK_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.isConfirm = true;
                this.FindForm().Close();
            }
            catch { }
        }

        private bool splitUnit_CostChanged(string feeStat, decimal orgCost, decimal newCost, ref decimal ownCost, ref decimal payCost, ref decimal pubCost, ref decimal CTFee, ref decimal MRIFee, ref decimal SXFee, ref decimal SYFee)
        {
           

                            #region ��ʱ����
                            //								decimal tempTotCost = detail.BalanceBase.FT.TotCost;
                            //								decimal orgPayCost =0, orgOwnCost = 0, orgPubCost = 0;
                            //
                            //								if(detail.BalanceBase.FT.TotCost + orgCost == newCost)
                            //								{
                            //									detail.BalanceBase.FT.TotCost = 0;
                            //									detail.BalanceBase.FT.OwnCost = 0;
                            //									detail.FT.Pub_Cost = 0;
                            //									detail.FT.Pay_Cost = 0;
                            //
                            //									ownCost = ownCost + detail.BalanceBase.FT.OwnCost;
                            //									payCost = payCost + detail.FT.Pay_Cost;
                            //									pubCost = pubCost + detail.FT.Pub_Cost;
                            //
                            //									invoiceTemp.FT.TotCost = invoiceTemp.FT.TotCost - detail.BalanceBase.FT.TotCost;
                            //									invoiceTemp.FT.Pay_Cost = invoiceTemp.FT.Pay_Cost - detail.FT.Pay_Cost;
                            //									invoiceTemp.FT.Pub_Cost = invoiceTemp.FT.Pub_Cost - detail.FT.Pub_Cost;
                            //									invoiceTemp.FT.OwnCost = invoiceTemp.FT.OwnCost - detail.BalanceBase.FT.OwnCost;
                            //								}
                            //								else
                            //								{
                            //									orgPayCost = detail.FT.Pay_Cost;
                            //									orgOwnCost = detail.BalanceBase.FT.OwnCost;
                            //									orgPubCost = detail.FT.Pub_Cost;
                            //
                            //									if(orgOwnCost > 0)//�Է�
                            //									{
                            //										detail.BalanceBase.FT.OwnCost = orgOwnCost - (newCost - orgCost);
                            //										detail.BalanceBase.FT.TotCost = detail.BalanceBase.FT.TotCost - (newCost - orgCost);
                            //										invoiceTemp.FT.TotCost = invoiceTemp.FT.TotCost - (newCost - orgCost);
                            //										invoiceTemp.FT.Pay_Cost = 0;
                            //										invoiceTemp.FT.Pub_Cost = 0;
                            //										invoiceTemp.FT.OwnCost = invoiceTemp.FT.TotCost;
                            //									}
                            //									else //����
                            //									{
                            //										detail.BalanceBase.FT.TotCost = detail.BalanceBase.FT.TotCost - (newCost - orgCost);
                            //										detail.FT.Pay_Cost = orgPayCost - FS.neFS.HISFC.Components.Public.String.FormatNumber(orgPayCost/tempTotCost * orgPayCost, 2);
                            //										detail.FT.Pub_Cost = orgPubCost - (newCost - orgCost) - detail.FT.Pay_Cost;
                            //
                            //										invoiceTemp.FT.TotCost = invoiceTemp.FT.TotCost - (newCost - orgCost);
                            //										invoiceTemp.FT.Pay_Cost = invoiceTemp.FT.Pay_Cost - FS.neFS.HISFC.Components.Public.String.FormatNumber(orgPayCost/tempTotCost * orgPayCost, 2);
                            //										invoiceTemp.FT.Pub_Cost = invoiceTemp.FT.Pub_Cost - (newCost - orgCost) + detail.FT.Pay_Cost;
                            //										invoiceTemp.FT.OwnCost = 0;
                            //									}
                            //								}
                            #endregion

                       
            return true;
        }

        private void tbCancel_Click(object sender, System.EventArgs e)
        {
            try
            {
                this.isConfirm = false;
                this.FindForm().Close();
            }
            catch { }
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                try
                {
                    this.isConfirm = false;
                    this.FindForm().Close();
                }
                catch { };
            }
            return base.ProcessDialogKey(keyData);
        }

        #endregion
    }
}
