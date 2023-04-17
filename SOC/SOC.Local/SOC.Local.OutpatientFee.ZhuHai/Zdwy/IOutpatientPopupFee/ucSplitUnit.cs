using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace FS.SOC.Local.OutpatientFee.ZhuHai.Zdwy.IOutpatientPopupFee
{
    public partial class ucSplitUnit : UserControl
    {
        public ucSplitUnit()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        private DateTime dtInvoiceDate = DateTime.MinValue;//��Ʊʱ��
        private FS.HISFC.Models.Fee.Outpatient.Balance invoice = new FS.HISFC.Models.Fee.Outpatient.Balance();//��Ʊʵ��
        private ArrayList invoiceDetails = new ArrayList();//��Ʊ��ϸʵ��
        private int seqNO = 0;//�ڼ��ŷ�Ʊ
        private bool isFocus = false; //�Ƿ��ý���
        private bool isLast = false;//�Ƿ����һ�ŷ�Ʊ������ǲ����޸Ľ��
        public delegate bool ChangeCost(string feeStat, decimal orgCost, decimal newCost, ref FS.HISFC.Models.Base.FT ft, 
            ref decimal CTFee, ref decimal MRIFee, ref decimal SXFee, ref decimal SYFee);
        public event ChangeCost CostChanged;
        public delegate void ChangeFocus(int seq);
        public event ChangeFocus ModifyFinished;

        private string STATCODEJC = "";//����
        private string STATCODEZL = "";//���Ʒ�

        #endregion

        #region ����
        
        /// <summary>
        /// �Ƿ����һ�ŷ�Ʊ������ǲ����޸Ľ��
        /// </summary>
        public bool IsLast
        {
            set
            {
                isLast = value;
                if (isLast)
                {
                    this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.ReadOnly;
                }
                else
                {
                    this.fpSpread1_Sheet1.OperationMode = FarPoint.Win.Spread.OperationMode.RowMode;
                }
            }
            get
            {
                return isLast;
            }
        }

        /// <summary>
        /// ��Ʊ��
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
                this.lbInvoiceNo.Text = invoice.Invoice.ID;
                if (this.invoice != null) 
                {
                    this.invoice.PrintTime = this.dtpInvoiceTime.Value;
                }
                this.lbCost.Text = invoice.FT.TotCost.ToString() + " ��д: " + FS.FrameWork.Public.String.LowerMoneyToUpper(invoice.FT.TotCost);

            }
        }

        /// <summary>
        /// ��Ʊʱ��
        /// </summary>
        public DateTime InvoiceDate
        {
            get
            {
                dtInvoiceDate = this.dtpInvoiceTime.Value.Date;
                return dtInvoiceDate;
            }
            set
            {
                dtInvoiceDate = value;
                this.dtpInvoiceTime.Value = dtInvoiceDate;
                //this.invoice.PrintTime = this.dtpInvoiceTime.Value;
            }
        }

        /// <summary>
        /// ��Ʊ��ϸʵ��
        /// </summary>
        public ArrayList InvoiceDetails
        {
            get
            {
                invoiceDetails = new ArrayList();
                FS.HISFC.Models.Fee.Outpatient.BalanceList detail = null;
                for (int i = 0; i < this.fpSpread1_Sheet1.RowCount; i++)
                {
                    detail = this.fpSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Fee.Outpatient.BalanceList;
                    if (detail.BalanceBase.FT.TotCost >= 0)
                    {
                        invoiceDetails.Add(detail);
                    }
                }
                return invoiceDetails;
            }
            set
            {
                invoiceDetails = value;
                this.fpSpread1_Sheet1.RowCount = invoiceDetails.Count;
                FS.HISFC.Models.Fee.Outpatient.BalanceList detail = null;
                for (int i = 0; i < invoiceDetails.Count; i++)
                {
                    detail = invoiceDetails[i] as FS.HISFC.Models.Fee.Outpatient.BalanceList;
                    this.fpSpread1_Sheet1.Cells[i, 0].Text = detail.FeeCodeStat.Name;
                    this.fpSpread1_Sheet1.Cells[i, 1].Text = detail.BalanceBase.FT.TotCost.ToString();
                    this.fpSpread1_Sheet1.Rows[i].Tag = detail;
                }
            }
        }

        /// <summary>
        /// �ڼ��ŷ�Ʊ
        /// </summary>
        public int SeqNO
        {
            get
            {
                return seqNO;
            }
            set
            {
                seqNO = value;
            }
        }
        /// <summary>
        /// �ؼ��Ƿ��ý���
        /// </summary>
        public bool IsFocus
        {
            set
            {
                isFocus = value;
                if (isFocus)
                {
                    if (this.fpSpread1_Sheet1.RowCount <= 0)
                    {
                        return;
                    }
                    this.fpSpread1_Sheet1.SetActiveCell(0, 1, false);
                }
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ��farpoint,����һЩ�ȼ�
        /// </summary>
        private void InitFp()
        {
            InputMap im;
            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Down, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Up, Keys.None), FarPoint.Win.Spread.SpreadActions.None);

            im = fpSpread1.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            im.Put(new Keystroke(Keys.Escape, Keys.None), FarPoint.Win.Spread.SpreadActions.None);
        }

        #endregion

        #region �¼�

        private void ucSplitUnit_Load(object sender, System.EventArgs e)
        {
            try
            {
                InitFp();

                bool isCanModifyInvoiceDate = controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.CAN_MODIFY_INVOICE_DATE, true, false);
                
                this.dtpInvoiceTime.Enabled = isCanModifyInvoiceDate;
            }
            catch { }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {

            if (this.fpSpread1.ContainsFocus)
            {
                int currRow = this.fpSpread1_Sheet1.ActiveRowIndex;
                int rowCount = this.fpSpread1_Sheet1.RowCount;

                if (keyData == Keys.Enter)
                {
                    if (currRow < rowCount - 1)
                    {
                        this.fpSpread1_Sheet1.SetActiveCell(currRow + 1, 1, false);
                    }
                    if (currRow == rowCount - 1)
                    {
                        ModifyFinished(seqNO);
                    }
                    FS.HISFC.Models.Fee.Outpatient.BalanceList detailTemp =
                        this.fpSpread1_Sheet1.Rows[currRow].Tag as FS.HISFC.Models.Fee.Outpatient.BalanceList;
                    decimal orgCost = detailTemp.BalanceBase.FT.TotCost;
                    decimal newCost = 0;
                    try
                    {
                        newCost = FS.FrameWork.Function.NConvert.ToDecimal(this.fpSpread1_Sheet1.Cells[currRow, 1].Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("������벻�Ϸ�!" + ex.Message);
                        this.fpSpread1_Sheet1.SetActiveCell(currRow, 1, false);
                        return false;
                    }
                    if (newCost < 0)
                    {
                        MessageBox.Show("�����޸ĳɸ���!");
                        this.fpSpread1_Sheet1.SetActiveCell(currRow, 1, false);
                        return false;
                    }
                    decimal CTFee = 0, MRIFee = 0, SXFee = 0, SYFee = 0;
                    if (detailTemp.FeeCodeStat.ID == this.STATCODEJC)//���
                    {
                        if ((newCost) < detailTemp.CTFee + detailTemp.MRIFee)
                        {
                            decimal tempCost = 0;
                            decimal errCost = detailTemp.CTFee + detailTemp.MRIFee - newCost;
                            tempCost = errCost - detailTemp.CTFee;
                            if (tempCost <= 0)
                            {
                                CTFee = tempCost;
                                detailTemp.CTFee = detailTemp.CTFee - CTFee;
                                MRIFee = 0;
                            }
                            else
                            {
                                CTFee = detailTemp.CTFee;
                                detailTemp.CTFee = 0;
                                MRIFee = tempCost - detailTemp.CTFee;
                                detailTemp.MRIFee = detailTemp.MRIFee - MRIFee;
                            }
                        }
                    }
                    if (detailTemp.FeeCodeStat.ID == this.STATCODEZL)//����
                    {
                        if ((newCost) < detailTemp.SYFee + detailTemp.SXFee)
                        {
                            decimal tempCost = 0;
                            decimal errCost = detailTemp.SXFee + detailTemp.SYFee - newCost;
                            tempCost = errCost - detailTemp.SXFee;
                            if (tempCost <= 0)
                            {
                                SXFee = tempCost;
                                detailTemp.SXFee = detailTemp.SXFee - SXFee;
                                SYFee = 0;
                            }
                            else
                            {
                                SXFee = detailTemp.SYFee;
                                detailTemp.SXFee = 0;
                                SYFee = tempCost - detailTemp.SYFee;
                                detailTemp.SYFee = detailTemp.SYFee - SYFee;
                            }
                        }
                    }

                    FS.HISFC.Models.Base.FT ft = new FS.HISFC.Models.Base.FT();

                    ft.OwnCost = detailTemp.BalanceBase.FT.OwnCost;
                    ft.PayCost = detailTemp.BalanceBase.FT.PayCost;
                    ft.PubCost = detailTemp.BalanceBase.FT.PubCost;

                    bool returnValue = this.CostChanged(detailTemp.FeeCodeStat.ID, orgCost, newCost, ref ft, ref CTFee, ref MRIFee, ref SXFee, ref SYFee);
                    if (!returnValue)
                    {
                        this.fpSpread1_Sheet1.Cells[currRow, 1].Text = orgCost.ToString();
                        this.fpSpread1_Sheet1.SetActiveCell(currRow, 1, false);

                        return false;
                    }
                    else
                    {
                        this.fpSpread1_Sheet1.Cells[currRow, 1].Text = newCost.ToString();
                        detailTemp.BalanceBase.FT.TotCost = newCost;
                        detailTemp.BalanceBase.FT.OwnCost = newCost;
                        this.invoice.FT.TotCost = this.invoice.FT.TotCost + (newCost - orgCost);
                        this.invoice.FT.OwnCost = this.invoice.FT.OwnCost + (newCost - orgCost);
                        this.lbCost.Text = invoice.FT.TotCost.ToString() + " ��д: " + FS.FrameWork.Public.String.LowerMoneyToUpper(invoice.FT.TotCost);
                        if (detailTemp.FeeCodeStat.ID == this.STATCODEJC)//���
                        {
                            detailTemp.CTFee += CTFee;
                            detailTemp.MRIFee += MRIFee;
                        }
                        if (detailTemp.FeeCodeStat.ID == this.STATCODEZL)//���
                        {
                            detailTemp.SXFee += SXFee;
                            detailTemp.SYFee += SYFee;
                        }

                    }
                }
                if (keyData == Keys.Down)
                {

                    this.fpSpread1_Sheet1.ActiveRowIndex = currRow + 1;
                    this.fpSpread1_Sheet1.SetActiveCell(currRow + 1, 1);

                }
                if (keyData == Keys.Up)
                {
                    if (currRow > 0)
                    {
                        this.fpSpread1_Sheet1.ActiveRowIndex = currRow - 1;
                        this.fpSpread1_Sheet1.SetActiveCell(currRow - 1, 1);
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void dtpInvoiceTime_ValueChanged(object sender, System.EventArgs e)
        {
            this.invoice.PrintTime = this.dtpInvoiceTime.Value;
        }

        #endregion

    }
}
