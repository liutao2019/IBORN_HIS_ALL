using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;
using System.Collections;
using FS.FrameWork.Function;
using FS.SOC.Local.InpatientFee.FuYou.Models;

namespace FS.SOC.Local.InpatientFee.FuYou.DayBalance
{
    /// <summary>
    /// סԺ��Ʊ�ս�
    /// </summary>
    public partial class ucInpatientDayBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInpatientDayBalance()
        {
            InitializeComponent();
        }

        #region ��������

        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        NeuObject currentOperator = new NeuObject();
        /// <summary>
        /// �ս����ʱ��
        /// </summary>
        string operateDate = "";
        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();


        /// <summary>
        /// �ϴ��ս�ʱ��
        /// </summary>
        public string lastDate = "";

        /// <summary>
        /// �����ս�ʱ��
        /// </summary>
        string dayBalanceDate = "";

        /// <summary>
        /// Ҫ�ս������
        /// </summary>
        public List<InpatientDayBalance> alData = new List<InpatientDayBalance>();

        /// <summary>
        /// �����ս�ʵ��
        /// </summary>
        private InpatientDayBalance dayBalance = null;

        /// <summary>
        /// Ԥ�����ս�ʵ��
        /// </summary>
        private PrepayDayBalance prepayBalance = null;

        /// <summary>
        /// �˿����Ƿ�ͳ��Ԥ����֧��
        /// </summary>
        private string isCountPrepayPay = "0";//��0����ʾ��ͳ�ƣ���1����ʾͳ��

        /// <summary>
        /// �˿����Ƿ�ͳ��Ԥ����֧��
        /// </summary>
        [Description("�˿����Ƿ�ͳ��Ԥ����֧������0����ʾ��ͳ�ƣ���1����ʾͳ��"), Category("����")]
        public string IsCountPrepayPay
        {
            get
            {
                return this.isCountPrepayPay;
            }
            set
            {
                this.isCountPrepayPay = value;
            }
        }

        #region ����

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime beginDate = DateTime.MinValue;

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime endDate = DateTime.MinValue;
        /// <summary>
        /// �ս�ҵ���
        /// </summary>
        Function.InpatientDayBalanceManage inpatientDayBalanceManage = new Function.InpatientDayBalanceManage();

        #endregion

        #region �������

        /// <summary>
        /// �������
        /// </summary>
        private string reportTitle = "";

        /// <summary>
        /// �������
        /// </summary>
        [Description("�������"), Category("��������")]
        public string ReportTitle
        {
            get
            {
                return reportTitle;
            }
            set
            {
                reportTitle = value;
            }
        }

        #endregion


        /// <summary>
        /// ȫԺ�սỹ�ǵ����սᣬȫԺ�ս᲻���շ�Ա�ս�
        /// </summary>
        private string isAll = "1";//��0����ʾȫԺ�սᣬ��1����ʾ�����շ�Ա�ս�
        /// <summary>
        /// ȫԺ�ս���
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        private FS.HISFC.Models.Base.Employee empBalance = null;
        /// <summary>
        /// ȫԺ�սỹ�ǵ����սᡮ0����ʾȫԺ�սᣬ��1����ʾ�����շ�Ա�ս�
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        [Description("ȫԺ�սỹ�ǵ����սᣬ��0����ʾȫԺ�սᣬ��1����ʾ�����շ�Ա�ս�"), Category("����")]
        public string IsShow
        {
            get
            {
                return this.isAll;
            }
            set
            {
                this.isAll = value;
                if (value == "0")
                {
                    empBalance = new FS.HISFC.Models.Base.Employee();
                    empBalance.ID = "T00001";
                    empBalance.Name = "T-ȫԺ";
                }
            }
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public void InitUC()
        {
            try
            {
                // ����ֵ
                int intReturn = 0;

                // ��ȡ��ǰ����Ա
                currentOperator = this.inpatientDayBalanceManage.Operator;

                // ��ȡ���һ���ս�ʱ��
                string strLastDayBalanceDate = string.Empty;
                string strCurrentDate = string.Empty;
                intReturn = this.inpatientDayBalanceManage.GetLastDayBalanceDate(currentOperator.ID, out strLastDayBalanceDate, out strCurrentDate);
                if (intReturn == -1)
                {
                    MessageBox.Show("��ȡ�ϴ��ս�ʱ��ʧ�ܣ����ܽ����ս������");
                    return;
                }

                if (string.IsNullOrEmpty(strLastDayBalanceDate))
                {
                    this.ucInpatientDayBalanceDateControl1.dtLastBalance = DateTime.MinValue;
                    this.lastDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                    this.ucInpatientDayBalanceDateControl1.tbLastDate.Text = this.lastDate;

                }
                else
                {
                    this.ucInpatientDayBalanceDateControl1.dtLastBalance = NConvert.ToDateTime(strLastDayBalanceDate);
                    this.ucInpatientDayBalanceDateControl1.tbLastDate.Text = strLastDayBalanceDate;
                    this.lastDate = strLastDayBalanceDate;
                }
                this.ucInpatientDayBalanceDateControl1.dtpBalanceDate.Value = NConvert.ToDateTime(strCurrentDate);

                // ��ʼ���ӿؼ��ı���
                this.ucInpatientDayBalanceDateControl1.dtLastBalance = NConvert.ToDateTime(this.lastDate);
                this.ucInpatientDayBalanceReportNew1.InitUC(this.inpatientDayBalanceManage.Hospital.Name + reportTitle);
                this.ucInpatientDayBalanceReportNew2.InitUC(inpatientDayBalanceManage.Hospital.Name + reportTitle);
                this.ucInpatientDayBalanceReportNew1.SetDetailName();

                // ��ʼ�������嵥ҳ��
                this.lblTitle.Text = this.inpatientDayBalanceManage.Hospital.Name + "�շ��嵥";
                this.lblInvoiceInfo.Text = "";
                this.lblSumary.Text = "���ܺţ�";
                this.lblOper.Text = "�շ�Ա��" + currentOperator.Name;

                this.neuSpread1_Sheet1.RowCount = 0;
                this.neuSpread1_Sheet1.RowHeader.Visible = false;
                this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.BackColor = System.Drawing.Color.White;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
        }

        #endregion

        #region ��ѯҪ�ս������

        /// <summary>
        /// ��ѯҪ�ս������
        /// </summary>
        private void QueryDayBalanceData()
        {
            this.alData.Clear();
            int intReturn = 0;

            intReturn = this.ucInpatientDayBalanceDateControl1.GetBalanceDate(ref dayBalanceDate);
            if (intReturn == -1)
            {
                return;
            }
            //��ʾ������Ϣ
            this.ucInpatientDayBalanceReportNew1.Clear(lastDate, dayBalanceDate);
            string strEmpID = this.currentOperator.ID;

            DataTable dtCostStat = null;
            this.inpatientDayBalanceManage.QueryDayBalanceCostByStat(strEmpID, this.lastDate, dayBalanceDate, out dtCostStat);

            this.SetDetial(dtCostStat);

            //����farpoint��ʽ
            this.ucInpatientDayBalanceReportNew1.SetFarPoint();

            //��ʾ��Ʊ��������
            SetInvoice(this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1);

            //��ʾ��Ʊ��ֹ�ţ����������Ϣ
            DataTable dtInvoice = null;
            this.inpatientDayBalanceManage.QueryDayBalanceInvoiceDetial(strEmpID, this.lastDate, dayBalanceDate, out dtInvoice);
            SetInvoiceBeginAndEnd(dtInvoice, this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1);

            //��ʾ�������
            this.SetMoneyValue(this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1);
        }

        #endregion

        #region ����Ҫ�ս�Farpoint����

        /// <summary>
        /// ������ʾ��Ŀ����
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetDetial(DataTable table)
        {
            if (table.Rows.Count == 0) return;
            //�������
            if (ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count > 0)
            {
                //��������ݣ�Ȼ���ʼ��3��
                ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count = 0;
                ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count = 4;
                ucInpatientDayBalanceReportNew1.SetDetailName();
            }
            //�ս���ϸ��ʾ�ͷ�Ʊ��ʾһ�¡�
            //

            //��ʾ��Ŀ����
            int sortID = 0;
            decimal countMoney = 0;
            decimal decTemp = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                sortID = FS.FrameWork.Function.NConvert.ToInt32(table.Rows[i][3]);
                //int rowIndex = Convert.ToInt32(sortID % 3);
                //int colIndex = Convert.ToInt32(sortID/3)*2;
                int rowIndex = Convert.ToInt32(Math.Ceiling(sortID / 4.0)) - 1;
                int colIndex = (sortID % 4) * 2 == 0 ? 7 : (sortID % 4) * 2 - 1;

                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][2]);
                countMoney += decTemp;

                //decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text);
                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text);

                //ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text = decTemp.ToString("0.00");
                ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text = decTemp.ToString("0.00");
            }
            ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[3, 1].Text = countMoney.ToString("0.00");
            ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[3, 3].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(countMoney);

        }


        /// <summary>
        /// ������ʾ���ս���Ŀ����
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetRePrintDetial(DataTable table)
        {
            if (table.Rows.Count == 0) return;
            //�������
            if (ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count > 0)
            {
                //��������ݣ�Ȼ���ʼ��3��
                ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count = 0;
                ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count = 4;
                ucInpatientDayBalanceReportNew2.SetDetailName();
            }
            //�ս���ϸ��ʾ�ͷ�Ʊ��ʾһ�¡�
            //

            //��ʾ��Ŀ����
            int sortID = 0;
            decimal countMoney = 0;
            decimal decTemp = 0;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                sortID = FS.FrameWork.Function.NConvert.ToInt32(table.Rows[i][3]);
                //int rowIndex = Convert.ToInt32(sortID % 3);
                //int colIndex = Convert.ToInt32(sortID / 3) * 2;
                int rowIndex = Convert.ToInt32(Math.Ceiling(sortID / 4.0)) - 1;
                int colIndex = (sortID % 4) * 2 == 0 ? 7 : (sortID % 4) * 2 - 1;

                decTemp = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][2]);
                countMoney += decTemp;

                //decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text);
                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text);

                //ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text = decTemp.ToString("0.00");
                ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text = decTemp.ToString("0.00");
            }
            ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[3, 1].Text = countMoney.ToString("0.00");
            ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[3, 3].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(countMoney);

        }

        /// <summary>
        /// ����ս�ʵ�嵽�б�
        /// </summary>
        private void AddDayBalanceToArray()
        {
            dayBalance.BeginTime = NConvert.ToDateTime(this.lastDate);
            dayBalance.EndTime = NConvert.ToDateTime(this.dayBalanceDate);

            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            if (this.isAll == "0")
            {
                dayBalance.Oper.ID = this.empBalance.ID;
                dayBalance.Oper.Name = this.empBalance.Name;
            }
            else
            {
                dayBalance.Oper.ID = currentOperator.ID;
                dayBalance.Oper.Name = currentOperator.Name;
            }
            this.alData.Add(dayBalance);
        }

        /// <summary>
        /// ��ѯ����ʾ��Ʊ���� -- 
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoice(FarPoint.Win.Spread.SheetView sheet)
        {
            #region ��������
            //��ȡ��Ʊ����
            string strEmpID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }

            string effectiveBill = string.Empty;
            string uneffectiveBill = string.Empty;
            //��Ч��
            int resultValue = this.inpatientDayBalanceManage.QueryDayBalanceEffectiveBill(strEmpID, this.lastDate, dayBalanceDate, out effectiveBill);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }

            this.SetOneCellText(sheet, EnumCellName.A002, effectiveBill);

            //������
            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceUneffectiveBill(strEmpID, this.lastDate, dayBalanceDate, out uneffectiveBill);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }

            this.SetOneCellText(sheet, EnumCellName.A003, uneffectiveBill);

            //�����ܽ��
            decimal quitTotCost = 0;
            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceUneffectiveBillMoney(strEmpID, this.lastDate, dayBalanceDate, out quitTotCost);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            this.SetOneCellText(sheet, EnumCellName.A004, quitTotCost.ToString("0.00"));

            //��ʾʹ��Ʊ�ݺ�
            DataSet ds1 = new DataSet();
            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceEffectiveInvoiceNo(strEmpID, this.lastDate, dayBalanceDate, ref ds1);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                this.SetOneCellText(sheet, EnumCellName.A005, GetInvoiceStartAndEnd(ds1.Tables[0]));
                FarPoint.Win.Spread.Cell cell1 = sheet.GetCellFromTag(null, EnumCellName.A005);
                string[] sTemp = cell1.Text.Split('��', '��');
                cell1.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;
            }
            //�˷ѡ�����Ʊ�ݺ�
            DataSet ds2 = new DataSet();
            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceUneffectiveInvoiceNo(strEmpID, this.lastDate, dayBalanceDate, ref ds2);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                string InvoiceStr = GetInvoiceStr(ds2.Tables[0].DefaultView);
                this.SetOneCellText(sheet, EnumCellName.A006, InvoiceStr);
                FarPoint.Win.Spread.Cell cell2 = sheet.GetCellFromTag(null, EnumCellName.A006);
                string[] sTemp = cell2.Text.Split('|');
                cell2.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;
            }
            #endregion

            #region Ԥ������
            string beginInvoice = "";
            string endInvoice = "";
            int invoiceCount = 0;
            int invoiceQuitCount = 0;
            decimal quitCost = 0;
            //Ѻ����Ч��
            resultValue = this.inpatientDayBalanceManage.GetPrepayBalanceInvoiceCount(strEmpID, this.lastDate, dayBalanceDate, ref invoiceCount, ref beginInvoice, ref endInvoice);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            this.SetOneCellText(sheet, EnumCellName.A014, invoiceCount.ToString("0"));
            //Ѻ��������
            resultValue = this.inpatientDayBalanceManage.GetPrepayBalanceQuitInvoiceCount(strEmpID, this.lastDate, dayBalanceDate, out invoiceQuitCount);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            this.SetOneCellText(sheet, EnumCellName.A015, invoiceQuitCount.ToString("0"));
            //���Ͻ��
            resultValue = this.inpatientDayBalanceManage.GetPrepayQuitCost(strEmpID, this.lastDate, dayBalanceDate, out quitCost);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            this.SetOneCellText(sheet, EnumCellName.A016, quitCost.ToString("0.00"));
            //��ʾʹ��Ʊ�ݺ�
            DataSet ds3 = new DataSet();
            resultValue = this.inpatientDayBalanceManage.GetPrepayBalanceInvoiceNo(strEmpID, this.lastDate, dayBalanceDate, ref ds3);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            if (ds3 != null && ds3.Tables.Count > 0 && ds3.Tables[0].Rows.Count > 0)
            {
                this.SetOneCellText(sheet, EnumCellName.A017, GetInvoiceStartAndEnd(ds3.Tables[0]));
                FarPoint.Win.Spread.Cell cell1 = sheet.GetCellFromTag(null, EnumCellName.A017);
                string[] sTemp = cell1.Text.Split('��', '��');
                cell1.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;
            }
            //�˷ѡ�����Ʊ�ݺ�
            DataSet ds4 = new DataSet();
            resultValue = this.inpatientDayBalanceManage.GetPrepayBalanceQuitInvoiceNo(strEmpID, this.lastDate, dayBalanceDate, ref ds4);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }
            if (ds4 != null && ds4.Tables.Count > 0 && ds4.Tables[0].Rows.Count > 0)
            {
                string InvoiceStr = GetInvoiceStr(ds4.Tables[0].DefaultView);
                this.SetOneCellText(sheet, EnumCellName.A018, InvoiceStr);
                FarPoint.Win.Spread.Cell cell2 = sheet.GetCellFromTag(null, EnumCellName.A018);
                string[] sTemp = cell2.Text.Split('|');
                cell2.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;
            }
            //Ԥ������Ϣ
            decimal totCost = 0;
            decimal cash = 0;
            decimal pos = 0;
            resultValue = this.inpatientDayBalanceManage.GetPrepayBalanceCost(strEmpID, this.lastDate, dayBalanceDate, out totCost, out cash, out pos);
            if (resultValue == -1)
            {
                MessageBox.Show(this.inpatientDayBalanceManage.Err);
                return;
            }

            if (isCountPrepayPay == "1")
            {
                decimal cacost1 = 0;
                decimal poscost1 = 0;
                decimal chcost1 = 0;
                decimal orcost1 = 0;
                decimal fgcost1 = 0;
                decimal prepayPay = 0;
                this.inpatientDayBalanceManage.GetPrepayPayCost(strEmpID, this.lastDate, dayBalanceDate, out prepayPay, out cacost1, out poscost1, out chcost1, out orcost1, out fgcost1);
                quitCost = quitCost - prepayPay;
                totCost = totCost - prepayPay;
                cash = cash - cacost1;
                pos = pos - poscost1;
            }
            this.SetOneCellText(sheet, EnumCellName.A019, totCost.ToString("0.00"));
            this.SetOneCellText(sheet, EnumCellName.A020, cash.ToString("0.00"));
            this.SetOneCellText(sheet, EnumCellName.A021, pos.ToString("0.00"));

            #region �ս�ʵ�帳ֵ
            prepayBalance = new PrepayDayBalance();
            prepayBalance.BeginDate = this.lastDate;
            prepayBalance.EndDate = dayBalanceDate;
            prepayBalance.BeginInvoice = beginInvoice;
            prepayBalance.EndInvoice = endInvoice;
            prepayBalance.PrepayNum = invoiceCount;
            prepayBalance.RealCost = totCost;
            prepayBalance.QuitCost = quitCost;
            prepayBalance.TotCost = totCost - quitCost;
            prepayBalance.CACost = cash;
            prepayBalance.POSCost = pos;
            prepayBalance.CHCost = 0;
            prepayBalance.ORCost = 0;
            prepayBalance.FGCost = 0;
            prepayBalance.CheckFlag = "0";
            #endregion

            #endregion
            return;

            #region û��

            ////��ֹƱ�ݺ�
            ////this.SetOneCellText(sheet, "A00101", GetInvoiceStartAndEnd(ds.Tables[0]));

            ////��Ʊ����
            //dv.RowFilter = "trans_type='1'";
            //this.SetOneCellText(sheet, EnumCellName.A002, dv.Count.ToString());

            ////�˷�Ʊ�ݺ� 
            //string InvoiceStr = GetInvoiceStr(dv);
            ////aaaaaaaaaaaaaaaaa
            ////this.SetOneCellText(sheet, EnumCellName."A00402", InvoiceStr);

            ////����Ʊ�ݺ�
            //InvoiceStr = GetInvoiceStr(dv);
            ////aaaaaaaaaaaaaaaa
            ////this.SetOneCellText(sheet, "A00502", InvoiceStr);

            ////�˷�Ʊ��
            //dv.RowFilter = "cancel_flag='0' and trans_type='2'";
            //this.SetOneCellText(sheet, EnumCellName.A003, dv.Count.ToString());

            ////����Ʊ��
            //dv.RowFilter = "cancel_flag in ('2','3') and trans_type='2'";
            ////aaaaaaaaaaaaaaaaaa
            ////this.SetOneCellText(sheet, "A00501", dv.Count.ToString());
            #endregion
        }

        /// <summary>
        /// ��ʾ��Ʊ��ֹ�ţ����������Ϣ
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoiceBeginAndEnd(DataTable dtInvoice, FarPoint.Win.Spread.SheetView sheet)
        {
            this.ucInpatientDayBalanceReportNew1.lblinvoiceInfo.Text = "";

            if (dtInvoice == null || dtInvoice.Rows.Count <= 0)
            {
                return;
            }

            int iRowCount = dtInvoice.Rows.Count;

            int idx = 0;
            string strTemp = "";
            DataRow drTemp = null;

            List<string> lstInvoice = new List<string>();

            for (idx = 0; idx < iRowCount; idx++)
            {
                drTemp = dtInvoice.Rows[idx];

                strTemp = drTemp["print_invoiceno"].ToString().TrimStart(new char[] { '0' });
                if (!lstInvoice.Contains(strTemp))
                {
                    lstInvoice.Add(strTemp);
                }

            }
            if (sheet == this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1)
            {
                this.ucInpatientDayBalanceReportNew1.lblinvoiceInfo.Text = "��Ʊ����     " + lstInvoice.Count.ToString() + "     " + lstInvoice[0] + "  -  " + lstInvoice[lstInvoice.Count - 1];
            }
            else
            {
                this.ucInpatientDayBalanceReportNew2.lblinvoiceInfo.Text = "��Ʊ����     " + lstInvoice.Count.ToString() + "     " + lstInvoice[0] + "  -  " + lstInvoice[lstInvoice.Count - 1];
            }

        }

        /// <summary>
        /// ������ϡ��˷�Ʊ�ݺ�
        /// </summary>
        /// <param name="dv">DataView</param>
        /// <param name="aMod">���ϻ����˷�1������ 0���˷�</param>
        /// <returns></returns>
        private string GetInvoiceStr(DataView dv)
        {
            StringBuilder sb = new StringBuilder();
            if (dv.Count == 0)
            {
                sb.Append("��");
            }
            else
            {
                for (int i = 0; i < dv.Count; i++)
                {
                    sb.Append(dv[i][0].ToString() + "|");

                }
            }
            return sb.ToString();
        }

        #region �����ʼ����ֹƱ�ݺ�

        private string GetInvoiceStartAndEnd(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            int count = dt.Rows.Count - 1;
            string minStr = dt.Rows[0][0].ToString();
            string maxStr = dt.Rows[0][0].ToString();
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    long froInt = Convert.ToInt64(dt.Rows[i][0].ToString());
                    long nxtInt = Convert.ToInt64(dt.Rows[j][0].ToString());
                    long chaInt = nxtInt - froInt;
                    if (chaInt > 1)
                    {
                        maxStr = dt.Rows[i][0].ToString();
                        if (maxStr.Equals(minStr))
                        {
                            sb.Append(minStr + ",");
                        }
                        else
                        {
                            sb.Append(minStr + "��" + maxStr + ",");
                        }
                        minStr = dt.Rows[j][0].ToString();
                        break;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            maxStr = dt.Rows[count][0].ToString();
            sb.Append(minStr + "��" + maxStr);
            return sb.ToString();

        }

        #endregion

        /// <summary>
        /// ������ʾ�������
        /// </summary>
        protected virtual void SetMoneyValue(FarPoint.Win.Spread.SheetView sheet)
        {
            int resultValue;

            string employeeID = this.currentOperator.ID;
            DataTable dtBillMoney = null;
            ArrayList al = new ArrayList();

            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceBillMoney(employeeID, this.lastDate, dayBalanceDate, out dtBillMoney);

            resultValue = this.inpatientDayBalanceManage.QueryDayBalanceCACDMoney(employeeID, this.lastDate, dayBalanceDate, ref al);

            SetPayTypeValue(sheet, dtBillMoney, al);

            return;

        }

        /// <summary>
        /// ��������ʾ���
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dtBillMoney"></param>
        protected virtual void SetPayTypeValue(FarPoint.Win.Spread.SheetView sheet, DataTable dtBillMoney, ArrayList al)
        {
            // ˳���籣
            decimal decSDSB = 0;
            // �Ż��Ŵ�
            decimal decYHYD = 0;
            // ��Ժ����
            decimal decBYJM = 0;
            // ��Լ��λ
            decimal decTYDW = 0;
            // Ԥ�������
            decimal decYJJJS = 0;
            //�Ͻɽ��
            decimal decSJJE = 0;
            //�Ͻɿ���
            decimal decSJKS = 0;
            //�ֽ���
            decimal decCA = 0;
            //ˢ�����
            decimal decCD = 0;

            // ְ��ҽ��
            string strPact = "5";
            decSDSB = this.ComputeByPact(strPact, "Sum(pubcost)", dtBillMoney);
            // �������
            // ����ҽ��
            // �Ż��Ŵ�
            // �ݲ�����

            // Ԥ�������
            decYJJJS = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(prepaycost)", ""));

            // ��Ժ����
            decBYJM = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(dercost)", ""));

            //decCA = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(supplycost)", "")) - FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(returncost)", ""));

            //decCA = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(cacost)", ""));

            //decCD = FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute("Sum(cdcost)", ""));

            decimal decCABalCost1 = 0;
            decimal decCDBalCost1 = 0;
            decimal decCAPreCost1 = 0;
            decimal decCDPreCost1 = 0;
            decimal decCABalCost2 = 0;
            decimal decCDBalCost2 = 0;
            decimal decCAPreCost2 = 0;
            decimal decCDPreCost2 = 0;

            foreach(FS.FrameWork.Models.NeuObject obj in al)
            {
                if (obj.User01 == "1")//����
                {
                    if (obj.Memo == "0")//Ԥ����
                    {
                        decCAPreCost1 = FS.FrameWork.Function.NConvert.ToDecimal(obj.ID);
                        decCDPreCost1 = FS.FrameWork.Function.NConvert.ToDecimal(obj.Name);
                    }
                    else//�����
                    {
                        decCABalCost1 = FS.FrameWork.Function.NConvert.ToDecimal(obj.ID);
                        decCDBalCost1 = FS.FrameWork.Function.NConvert.ToDecimal(obj.Name);
                    }
                }
                else//����
                {
                    if (obj.Memo == "0")//Ԥ����
                    {
                        decCAPreCost2 = FS.FrameWork.Function.NConvert.ToDecimal(obj.ID);
                        decCDPreCost2 = FS.FrameWork.Function.NConvert.ToDecimal(obj.Name);
                    }
                    else//�����
                    {
                        decCABalCost2 = FS.FrameWork.Function.NConvert.ToDecimal(obj.ID);
                        decCDBalCost2 = FS.FrameWork.Function.NConvert.ToDecimal(obj.Name);
                    }
                }
            }

            decCA = decCAPreCost1 + decCABalCost1 + decCAPreCost2 - decCABalCost2;
            decCD = decCDPreCost1 + decCDBalCost1 + decCDPreCost2 - decCDBalCost2;

            FarPoint.Win.BevelBorder bevelBorder1 = new FarPoint.Win.BevelBorder(FarPoint.Win.BevelBorderType.Raised, System.Drawing.SystemColors.ControlLightLight, System.Drawing.Color.Black, 2, false, false, false, true);

            //������սḳֵ���ս���棬����ǲ�ѯ�ش���ֵ�ڲ�ѯ�ش����
            //string strTemp = string.Empty;
            if (sheet == this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1)
            {
                decSJJE = decCA + FS.FrameWork.Function.NConvert.ToDecimal(this.GetOneCellText(this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1, EnumCellName.A020)) - decYJJJS;
                decSJKS = decCD + FS.FrameWork.Function.NConvert.ToDecimal(this.GetOneCellText(this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1, EnumCellName.A021));
                //strTemp = FS.FrameWork.Public.String.LowerMoneyToUpper(decSJJE);
                //if (decSJJE < 0)
                //{
                //    strTemp = "��" + strTemp;
                //}
                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows[9].Border = bevelBorder1;

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[4, 1].Text = decSJJE.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[4, 3].Text = decSJKS.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 1].Text = decSDSB.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 3].Text = decYHYD.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 5].Text = decBYJM.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 7].Text = decTYDW.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 1].Text = decCA.ToString("0.00"); //decYJJJS.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 3].Text = decCD.ToString("0.00");

                this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 5].Text = decYJJJS.ToString("0.00");

                //this.ucInpatientDayBalanceReportNew1.neuSpread1_Sheet1.Rows[4].Visible = false;
            }
            else
            {
                decSJJE = decCA + FS.FrameWork.Function.NConvert.ToDecimal(this.GetOneCellText(this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1, EnumCellName.A020)) - decYJJJS;
                decSJKS = decCD + FS.FrameWork.Function.NConvert.ToDecimal(this.GetOneCellText(this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1, EnumCellName.A021));
                //strTemp = FS.FrameWork.Public.String.LowerMoneyToUpper(decSJJE);
                //if (decSJJE < 0)
                //{
                //    strTemp = "�� " + strTemp;
                //}
                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Rows[9].Border = bevelBorder1;

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[4, 1].Text = decSJJE.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[4, 3].Text = decSJKS.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 1].Text = decSDSB.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 3].Text = decYHYD.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 5].Text = decBYJM.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 7].Text = decTYDW.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 1].Text = decCA.ToString("0.00"); //decYJJJS.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 3].Text = decCD.ToString("0.00");

                this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 5].Text = decYJJJS.ToString("0.00");

                //this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1.Rows[4].Visible = false;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="strPactList"></param>
        /// <param name="excepition"></param>
        /// <param name="dtBillMoney"></param>
        /// <returns></returns>
        private decimal ComputeByPact(string strPactList, string excepition, DataTable dtBillMoney)
        {
            string[] strPactArr = strPactList.Split(new char[] { '|' });

            string strPactFilter = "pact_code = '" + strPactArr[0] + "'";
            for (int i = 1; i < strPactArr.Length; i++)
            {
                strPactFilter += " or pact_code = '" + strPactArr[i] + "'";
            }

            return FS.FrameWork.Function.NConvert.ToDecimal(dtBillMoney.Compute(excepition, strPactFilter));
        }


        #endregion

        #region ��ѯ���ս�����

        private void QueryDayBalanceRecord()
        {
            // ����ֵ
            int intReturn = 0;
            // ��ѯ����ʼʱ��
            DateTime dtFrom = DateTime.MinValue;
            // ��ѯ�Ľ�ֹʱ��
            DateTime dtTo = DateTime.MinValue;
            // ���ص���־��¼
            List<FS.FrameWork.Models.NeuObject> lstBalanceRecord = null;
            //Ԥ�����ս��¼
            List<FS.FrameWork.Models.NeuObject> lstPrePayRecord = null;
            // ��ѯ���ռ���ˮ��
            string sequence = "";

            // ��ȡ��ѯʱ��
            intReturn = this.ucReprintDateControl1.GetInputDateTime(ref dtFrom, ref dtTo);
            if (intReturn == -1)
            {
                return;
            }

            intReturn = this.inpatientDayBalanceManage.GetBalanceRecord(this.currentOperator.ID, dtFrom.ToString("yyyy-MM-dd HH:mm:ss"), dtTo.ToString("yyyy-MM-dd HH:mm:ss"), out lstBalanceRecord);

            if (intReturn == -1)
            {
                MessageBox.Show("��ȡ��־��¼ʧ�ܣ�");
                return;
            }

            intReturn = this.inpatientDayBalanceManage.GetPrepayBalanceHistory(this.currentOperator.ID, dtFrom.ToString("yyyy-MM-dd HH:mm:ss"), dtTo.ToString("yyyy-MM-dd HH:mm:ss"), out lstPrePayRecord);

            if (intReturn == -1)
            {
                MessageBox.Show("��ȡԤ�����ս��¼ʧ�ܣ�");
                return;
            }

            // �жϽ����¼���������������ô�����������û�ѡ��
            if (lstBalanceRecord == null || lstBalanceRecord.Count <= 0 || lstPrePayRecord == null || lstPrePayRecord.Count <= 0)
            {
                MessageBox.Show("��ʱ�����û��Ҫ���ҵ����ݣ�");
                return;
            }

            for (int i = 0; i < lstBalanceRecord.Count; i++)
            {
                for (int j = 0; j < lstPrePayRecord.Count; j++)
                {
                    if (lstBalanceRecord[i].Name == lstPrePayRecord[j].Name
                        && lstBalanceRecord[i].Memo == lstPrePayRecord[j].Memo)
                    {
                        lstBalanceRecord[i].User02 = lstPrePayRecord[j].ID;
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            for (int i = 0; i < lstBalanceRecord.Count; i++)
            {
                if (lstBalanceRecord[i].User02 == "")
                {
                    MessageBox.Show("��ȡԤ�����ս��¼ʧ�ܣ�");
                    //return;
                }
                else
                {
                    continue;
                }
            }

            string begin = string.Empty, end = string.Empty;
            
            if (lstBalanceRecord.Count > 1)
            {
                frmConfirmBalanceRecord confirmBalanceRecord = new frmConfirmBalanceRecord();
                confirmBalanceRecord.LstBalanceRecord = lstBalanceRecord;
                if (confirmBalanceRecord.ShowDialog() == DialogResult.OK)
                {
                    sequence = confirmBalanceRecord.neuSpread1.Sheets[0].Cells[confirmBalanceRecord.neuSpread1.Sheets[0].ActiveRowIndex, 0].Text;
                    begin = confirmBalanceRecord.neuSpread1.Sheets[0].Cells[confirmBalanceRecord.neuSpread1.Sheets[0].ActiveRowIndex, 1].Text;
                    end = confirmBalanceRecord.neuSpread1.Sheets[0].Cells[confirmBalanceRecord.neuSpread1.Sheets[0].ActiveRowIndex, 2].Text;
                }
                else
                {
                    return;
                }
            }
            else
            {
                sequence = lstBalanceRecord[0].ID;
                begin = lstBalanceRecord[0].Name;
                end = lstBalanceRecord[0].Memo;
            }

            //ͨ����ѯ��ʱ�սῪʼʱ��ͽ���ʱ����ʵ���ش�
            this.lastDate = begin.ToString();
            this.dayBalanceDate = end.ToString();
            //��ʾ������Ϣ
            this.ucInpatientDayBalanceReportNew2.Clear(lastDate, dayBalanceDate);
            string strEmpID = this.currentOperator.ID;

            DataTable dtCostStat = null;
            this.inpatientDayBalanceManage.QueryDayBalanceCostByStat(strEmpID, this.lastDate, dayBalanceDate, out dtCostStat);

            this.SetRePrintDetial(dtCostStat);

            //����farpoint��ʽ
            this.ucInpatientDayBalanceReportNew2.SetFarPoint();

            //��ʾ��Ʊ��������
            SetInvoice(this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1);

            // ��ʾ�������
            this.SetMoneyValue(this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1);

            // �����շ��嵥
            DataTable dtInvoice = null;
            this.inpatientDayBalanceManage.QueryDayBalanceInvoiceDetial(strEmpID, this.lastDate, dayBalanceDate, out dtInvoice);
            this.SetInvoiceDetial(sequence, dtInvoice);
            SetInvoiceBeginAndEnd(dtInvoice, this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1);

        }

        #endregion

        #region ��ʾ�շ��嵥
        /// <summary>
        /// ��ʾ�շ��嵥
        /// </summary>
        /// <param name="balanceNO"></param>
        /// <param name="dtInvoice"></param>
        private void SetInvoiceDetial(string balanceNO, DataTable dtInvoice)
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            lblSumary.Text = "���ܺţ�  " + balanceNO;
            lblInvoiceInfo.Text = "";



            if (dtInvoice == null || dtInvoice.Rows.Count <= 0)
            {
                return;
            }

            int iRowCount = dtInvoice.Rows.Count;
            this.neuSpread1_Sheet1.RowCount = iRowCount + 1;

            int idx = 0;
            string strTemp = "";
            DataRow drTemp = null;

            List<string> lstInvoice = new List<string>();

            for (idx = 0; idx < iRowCount; idx++)
            {
                drTemp = dtInvoice.Rows[idx];

                strTemp = drTemp["print_invoiceno"].ToString().TrimStart(new char[] { '0' });
                if (!lstInvoice.Contains(strTemp))
                {
                    lstInvoice.Add(strTemp);
                }

                this.neuSpread1_Sheet1.Cells[idx, 0].Text = strTemp;
                this.neuSpread1_Sheet1.Cells[idx, 1].Text = drTemp["invoice_no"].ToString().Trim();
                this.neuSpread1_Sheet1.Cells[idx, 2].Text = drTemp["patient_no"].ToString().Trim();
                this.neuSpread1_Sheet1.Cells[idx, 3].Text = drTemp["name"].ToString().Trim();

                this.neuSpread1_Sheet1.Cells[idx, 4].Text = drTemp["balance_date"] != DBNull.Value ? FS.FrameWork.Function.NConvert.ToDateTime(drTemp["balance_date"]).ToString("yyyy.MM.dd") : "";
                this.neuSpread1_Sheet1.Cells[idx, 5].Text = drTemp["tot_cost"].ToString();
                this.neuSpread1_Sheet1.Cells[idx, 6].Text = drTemp["eco_cost"].ToString();
                this.neuSpread1_Sheet1.Cells[idx, 7].Text = drTemp["owncost"].ToString();
                this.neuSpread1_Sheet1.Cells[idx, 8].Text = drTemp["pub_cost"].ToString();
                this.neuSpread1_Sheet1.Cells[idx, 9].Text = drTemp["empl_name"].ToString().Trim();
                this.neuSpread1_Sheet1.Cells[idx, 10].Text = drTemp["trans_type"].ToString() == "1" ? "" : "����";
            }
            // ��ʾ������Ϣ
            this.neuSpread1_Sheet1.Cells[idx, 4].Text = "�ϼƣ�";
            this.neuSpread1_Sheet1.Cells[idx, 5].Text = dtInvoice.Compute("Sum(tot_cost)", "").ToString();
            this.neuSpread1_Sheet1.Cells[idx, 6].Text = dtInvoice.Compute("Sum(eco_cost)", "").ToString();
            this.neuSpread1_Sheet1.Cells[idx, 7].Text = dtInvoice.Compute("Sum(owncost)", "").ToString();
            this.neuSpread1_Sheet1.Cells[idx, 8].Text = dtInvoice.Compute("Sum(pub_cost)", "").ToString();

            //����һЩ��Ա��Ϣ
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Models.Span.Add(this.neuSpread1_Sheet1.RowCount - 1, 0, 1, 11);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "���:" + "                           " + "����:" + "                           " + "����:" + "                           " + "�շ�Ա:" + currentOperator.Name;

            lblInvoiceInfo.Text = "��Ʊ����     " + lstInvoice.Count.ToString() + "     " + lstInvoice[0] + "  -  " + lstInvoice[lstInvoice.Count - 1];

        }

        #endregion

        #region �������ս�Farpoint����
        private void SetOldFarPointData(DataTable table)
        {
            FarPoint.Win.Spread.SheetView sheet = this.ucInpatientDayBalanceReportNew2.neuSpread1_Sheet1;
            int rowCount = sheet.Rows.Count;
            if (sheet.Rows.Count > 0)
            {
                sheet.Rows.Remove(0, rowCount - 1);
            }
            DataView dv = table.DefaultView;
            //������Ŀ��ϸ
            SetDetialed(sheet, dv);
            this.ucInpatientDayBalanceReportNew2.SetFarPoint();
            this.SetInvoiced(sheet, dv);
            this.SetMoneyed(sheet, dv);
        }

        /// <summary>
        /// �������սᷢƱ��Ϣ
        /// </summary>
        /// <param name="sheet">FarPoint.Win.Spread.SheetView</param>
        /// <param name="dv">DataView</param>
        protected virtual void SetInvoiced(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            dv.RowFilter = "BALANCE_ITEM='5'";
            this.SetFarpointValue(sheet, dv);
        }

        protected virtual void SetMoneyed(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            dv.RowFilter = "BALANCE_ITEM='6'";
            this.SetFarpointValue(sheet, dv);
        }

        protected virtual void SetFarpointValue(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            //if (dv.Count > 0)
            //{
            //    string fieldStr = string.Empty;
            //    string tagStr = string.Empty;
            //    string field = string.Empty;
            //    int Index = 0;
            //    for (int k = 0; k < dv.Count; k++)
            //    {
            //        fieldStr = dv[k]["sort_id"].ToString();
            //        int index = fieldStr.IndexOf('��');
            //        if (index == -1)
            //        {
            //            Index = fieldStr.IndexOf("|");
            //            tagStr = fieldStr.Substring(0, Index);
            //            field = fieldStr.Substring(Index + 1);
            //            SetOneCellText(sheet, tagStr, dv[k][field].ToString());
            //            if (dv[k][1].ToString() == "A023")
            //            {
            //                SetOneCellText(sheet, "A1000", FS.FrameWork.Public.String.LowerMoneyToUpper(NConvert.ToDecimal(dv[k][field])));
            //            }
            //        }
            //        else
            //        {
            //            string[] aField = fieldStr.Split('��');
            //            if (aField.Length == 0) continue;
            //            foreach (string s in aField)
            //            {
            //                Index = s.IndexOf("|");
            //                tagStr = s.Substring(0, Index);
            //                field = s.Substring(Index + 1);
            //                SetOneCellText(sheet, tagStr, dv[k][field].ToString());
            //            }
            //        }
            //    }
            //}
        }

        /// <summary>
        /// �������ս���Ŀ��ϸ
        /// </summary>
        /// <param name="sheet">FarPoint.Win.Spread.SheetView</param>
        /// <param name="dv">DataView</param>
        private void SetDetialed(FarPoint.Win.Spread.SheetView sheet, DataView dv)
        {
            #region ��ʾ��Ŀ����
            //��Ŀ����
            dv.RowFilter = "BALANCE_ITEM='4'";
            int count = dv.Count;
            decimal countMoney = 0;
            if (count > 0)
            {
                if (count % 2 == 0)
                {
                    sheet.Rows.Count = Convert.ToInt32(count / 2);
                }
                else
                {
                    sheet.Rows.Count = Convert.ToInt32(count / 2) + 1;
                }

                //��ʾ��Ŀ����
                for (int i = 0; i < count; i++)
                {
                    int index = Convert.ToInt32(i / 2);
                    int intMod = (i + 1) % 2;
                    if (intMod > 0)
                    {
                        sheet.Models.Span.Add(index, 0, 1, 2);
                        sheet.Cells[index, 0].Text = dv[i]["extent_field1"].ToString();
                        sheet.Cells[index, 2].Text = dv[i]["tot_cost"].ToString();
                    }
                    else
                    {
                        sheet.Models.Span.Add(index, 3, 1, 2);
                        sheet.Cells[index, 3].Text = dv[i]["extent_field1"].ToString();
                        sheet.Cells[index, 5].Text = dv[i]["tot_cost"].ToString();
                    }
                    countMoney += Convert.ToDecimal(dv[i][0]);

                }
                if (count % 2 > 0)
                {
                    sheet.Models.Span.Add(sheet.Rows.Count - 1, 3, 1, 2);
                }
                //��ʾ�ϼ�
                sheet.Rows.Count += 1;
                count = sheet.Rows.Count;
                sheet.Models.Span.Add(count - 1, 0, 1, 2);
                sheet.Cells[count - 1, 0].Text = "�ϼƣ�";
                sheet.Models.Span.Add(count - 1, 2, 1, 4);
                sheet.Cells[count - 1, 2].Text = countMoney.ToString();
            }
            #endregion
        }
        #endregion

        #region ��tag��ȡFarPoint��cell����

        /// <summary>
        /// ���õ���Cell��Text
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="tagStr">Cell��tag</param>
        /// <param name="strText">Ҫ��ʾ��Text</param>
        private void SetOneCellText(FarPoint.Win.Spread.SheetView sheet, EnumCellName tagStr, string strText)
        {
            FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, tagStr);
            if (cell != null)
            {
                FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
                t.Multiline = true;
                t.WordWrap = true;
                cell.CellType = t;
                //���ַ���ת�������ֽ�����ӣ��ɹ���ת���ַ���
                try
                {
                    if (cell.Text == string.Empty || cell.Text == null)
                    {
                        cell.Text = "0";
                    }
                    if (strText == string.Empty || strText == null)
                    {
                        strText = "0";
                    }
                    decimal intText = (Convert.ToDecimal(cell.Text) + Convert.ToDecimal(strText));
                    cell.Text = intText.ToString();
                }
                //���ת��ʧ������ַ������
                catch
                {
                    if (cell.Text == "0")
                    {
                        cell.Text = "";
                    }
                    if (strText == "0")
                    {
                        strText = "";
                    }
                    cell.Text += strText;
                }
                //��ӽ��Ϊ�㣬��ɿ��ַ���
                if (cell.Text == "0")
                {
                    cell.Text = "";
                }
                //      cell.Text += strText;
            }
        }

        private string GetOneCellText(FarPoint.Win.Spread.SheetView sheet, EnumCellName tagStr)
        {
            FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, tagStr);
            if (cell != null)
                return cell.Text;
            return string.Empty;
        }
        #endregion

        #region �����ս�ʵ��

        /// <summary>
        /// ����ս�ʵ��
        /// </summary>
        private void SetDayBalanceData()
        {
            //FarPoint.Win.Spread.SheetView sheet = this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1;
            //string strValue = string.Empty;

            //#region ��ֹ��Ʊ��
            //dayBalance = new Models.ClinicDayBalanceNew();
            ////dayBalance.InvoiceNO.ID = "A001";
            ////dayBalance.InvoiceNO.Name = "��ʼ����Ʊ�ݺ�";
            ////strValue = GetOneCellText(sheet, "A00101");
            ////dayBalance.BegionInvoiceNO = strValue;
            ////strValue = GetOneCellText(sheet, "A00102");
            //dayBalance.EndInvoiceNo = strValue;
            ////����Cell��ʾ���ݵ�Tag���ֶ�����
            //dayBalance.SortID = "A00101|EXTENT_FIELD2��A00102|EXTENT_FIELD3";
            //dayBalance.TypeStr = "5";
            //AddDayBalanceToArray();
            //#endregion

            //#region Ʊ������
            //strValue = GetOneCellText(sheet, EnumCellName.A002);
            //this.SetOneCellDayBalance("A002", "Ʊ������", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region ��ЧƱ��
            //strValue = GetOneCellText(sheet, EnumCellName.A003);
            //this.SetOneCellDayBalance("A003", "��ЧƱ��", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region �˷�Ʊ��
            //dayBalance = new Models.ClinicDayBalanceNew();
            //dayBalance.InvoiceNO.ID = "A004";
            //dayBalance.InvoiceNO.Name = "�˷�Ʊ��";
            ////Ʊ����
            //strValue = this.GetOneCellText(sheet, "A00401");
            //dayBalance.TotCost = NConvert.ToDecimal(strValue);
            ////Ʊ�ݺ�
            //strValue = this.GetOneCellText(sheet, "A00402");
            //dayBalance.CancelInvoiceNo = strValue;
            //dayBalance.TypeStr = "5";
            //dayBalance.SortID = "A00401|TOT_COST��A00402|EXTENT_FIELD5";
            //AddDayBalanceToArray();
            //#endregion

            //#region ����Ʊ��
            //dayBalance = new Models.ClinicDayBalanceNew();
            //dayBalance.InvoiceNO.ID = "A005";
            //dayBalance.InvoiceNO.Name = "����Ʊ��";
            //strValue = this.GetOneCellText(sheet, "A00501");
            //dayBalance.TotCost = NConvert.ToDecimal(strValue);
            //strValue = this.GetOneCellText(sheet, "A00502");
            //dayBalance.FalseInvoiceNo = strValue;
            //dayBalance.TypeStr = "5";
            //dayBalance.SortID = "A00501|TOT_COST��A00502|EXTENT_FIELD4";
            //AddDayBalanceToArray();
            //#endregion

            //#region �˷ѽ��
            //strValue = GetOneCellText(sheet, "A006");
            //this.SetOneCellDayBalance("A006", "�˷ѽ��", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region ���Ͻ��
            //strValue = GetOneCellText(sheet, "A007");
            //this.SetOneCellDayBalance("A007", "���Ͻ��", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region ��ʱ������
            //#region Ѻ����
            //strValue = GetOneCellText(sheet, "A008");
            //this.SetOneCellDayBalance("A008", "Ѻ����", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region ��Ѻ���
            //strValue = GetOneCellText(sheet, "A009");
            //this.SetOneCellDayBalance("A009", "��Ѻ���", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region  ������
            //strValue = GetOneCellText(sheet, "A010");
            //this.SetOneCellDayBalance("A010", "������", NConvert.ToDecimal(strValue), "5");
            //#endregion
            //#endregion

            //#region  ��������
            //strValue = GetOneCellText(sheet, "A011");
            //this.SetOneCellDayBalance("A011", "��������", NConvert.ToDecimal(strValue), "5");
            //#endregion

            //#region ����ҽ��
            //strValue = this.GetOneCellText(sheet, "A012");
            //SetOneCellDayBalance("A012", "����ҽ��", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region �����Ը�
            //strValue = this.GetOneCellText(sheet, "A013");
            //SetOneCellDayBalance("A013", "�����Է�", NConvert.ToDecimal(strValue), "6");
            //#endregion
            //#region �����˻�
            //strValue = this.GetOneCellText(sheet, "A026");
            //SetOneCellDayBalance("A026", "�����˻�", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region �б��Ը�
            //strValue = this.GetOneCellText(sheet, "A014");
            //SetOneCellDayBalance("A014", "�б��Է�", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region �б��˻�
            //strValue = this.GetOneCellText(sheet, "A015");
            //SetOneCellDayBalance("A015", "�б��˻�", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region �б�ͳ��
            //strValue = this.GetOneCellText(sheet, "A016");
            //SetOneCellDayBalance("A016", "�б�ͳ��", NConvert.ToDecimal(strValue), "6");

            //#endregion

            //#region �б����
            //strValue = this.GetOneCellText(sheet, "A017");
            //SetOneCellDayBalance("A017", "�б����", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region ʡ���Ը�
            //strValue = this.GetOneCellText(sheet, "A018");
            //SetOneCellDayBalance("A018", "ʡ���Է�", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region ʡ���˻�
            //strValue = this.GetOneCellText(sheet, "A019");
            //SetOneCellDayBalance("A019", "ʡ���˻�", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region ʡ��ͳ��
            //strValue = this.GetOneCellText(sheet, "A020");
            //SetOneCellDayBalance("A020", "ʡ��ͳ��", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region ʡ�����
            //strValue = this.GetOneCellText(sheet, "A021");
            //SetOneCellDayBalance("A021", "ʡ�����", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region ʡ����Ա
            //strValue = this.GetOneCellText(sheet, "A022");
            //SetOneCellDayBalance("A022", "ʡ����Ա", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region �Ͻ��ֽ��
            //strValue = this.GetOneCellText(sheet, "A023");
            //SetOneCellDayBalance("A023", "�Ͻ��ֽ��", NConvert.ToDecimal(strValue), "6");

            //#endregion

            //#region �Ͻ�֧Ʊ��
            //strValue = this.GetOneCellText(sheet, "A024");
            //SetOneCellDayBalance("A024", "�Ͻ�֧Ʊ��", NConvert.ToDecimal(strValue), "6");
            //#endregion

            //#region �Ͻ�������
            //strValue = this.GetOneCellText(sheet, "A025");
            //SetOneCellDayBalance("A025", "�Ͻ�������", NConvert.ToDecimal(strValue), "6");
            //#endregion
        }

        #endregion

        #region �����ս�����
        /// <summary>
        /// �����ս�����
        /// </summary>
        public void DayBalance()
        {
            if (MessageBox.Show("�Ƿ�����ս�,�ս�����ݽ����ָܻ�?", "�����տ�Ա�ɿ��ձ�", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // �ȴ�����
                FS.FrameWork.WinForms.Forms.frmWait waitForm = new FS.FrameWork.WinForms.Forms.frmWait();

                if (this.alData == null || this.prepayBalance == null)
                {
                    return;
                }
                // �����ȴ�����
                waitForm.Tip = "���ڽ����ս�";
                waitForm.Show();

                string strEmpID = this.currentOperator.ID;
                string strOperID = this.currentOperator.ID;

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                int iRes = this.inpatientDayBalanceManage.DealOperDayBalance(strEmpID, strOperID, this.lastDate, this.dayBalanceDate);
                if (iRes < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    waitForm.Hide();
                    MessageBox.Show("�ս����");
                    return;
                }

                iRes = this.inpatientDayBalanceManage.InsertPrepayStat(this.prepayBalance);
                if (iRes < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    waitForm.Hide();
                    MessageBox.Show("Ѻ���ս����");
                    return;
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                waitForm.Hide();
                MessageBox.Show("�ս�ɹ����");
                //PrintInfo(this.neuPanel1);

                DialogResult dr = DialogResult.No;
                //MessageBox.Show("�Ƿ��ӡ�շ��嵥��", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    // ���ص���־��¼
                    List<FS.FrameWork.Models.NeuObject> lstBalanceRecord = null;
                    iRes = this.inpatientDayBalanceManage.GetBalanceRecord(this.currentOperator.ID, lastDate, dayBalanceDate, out lstBalanceRecord);
                    if (iRes == -1)
                    {
                        MessageBox.Show("��ȡ��־��¼ʧ��");
                        return;
                    }

                    if (lstBalanceRecord == null || lstBalanceRecord.Count <= 0)
                    {
                        MessageBox.Show("��ʱ�����û��Ҫ���ҵ����ݣ�");
                        return;
                    }

                    // �����շ��嵥
                    DataTable dtInvoice = null;
                    this.inpatientDayBalanceManage.QueryDayBalanceInvoiceDetial(strEmpID, this.lastDate, dayBalanceDate, out dtInvoice);
                    this.SetInvoiceDetial(lstBalanceRecord[0].ID, dtInvoice);

                    this.neuTabControl1.SelectedIndex = 2;
                    this.PrintInfo(this.pnlInvoiceDetial);
                }

                alData.Clear();
                this.InitUC();
                this.ucInpatientDayBalanceDateControl1.dtpBalanceDate.Value = this.ucInpatientDayBalanceDateControl1.dtLastBalance;
                this.QueryDayBalanceData();
            }
        }
        #endregion

        #region �¼�
        private void ucOutPatientDayBalance_Load(object sender, EventArgs e)
        {
            this.InitUC();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
            {
                QueryDayBalanceData();
            }
            else
            {
                this.QueryDayBalanceRecord();
            }

            return base.OnQuery(sender, neuObject);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitUC();

            toolBarService.AddToolButton("�ս�", "�����ս���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);

            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "�ս�")
            {
                if (this.neuTabControl1.SelectedIndex == 1 || this.neuTabControl1.SelectedIndex == 2)
                {
                    MessageBox.Show("�ش��շ��嵥���治�����ս�!");
                    return;
                }
                else
                {
                    // �ս�
                    this.DayBalance();
                }
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            switch (neuTabControl1.SelectedIndex)
            {
                case 0:
                    {
                        MessageBox.Show("���ս���ӡ��");
                        break;
                    }
                case 1:
                    {
                        this.PrintInfo(this.panelPrint);

                        break;
                    }
                case 2:
                    PrintInfo(this.pnlInvoiceDetial);
                    break;
            }

            return base.OnPrint(sender, neuObject);
        }

        protected virtual void PrintInfo(FS.FrameWork.WinForms.Controls.NeuPanel panelPrint)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(0, 0, panelPrint);
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuTabControl1.SelectedIndex == 0)
            {
                // ��ȡ���һ���ս�ʱ��
                string strLastDayBalanceDate = string.Empty;
                string strCurrentDate = string.Empty;

                int intReturn = this.inpatientDayBalanceManage.GetLastDayBalanceDate(currentOperator.ID, out strLastDayBalanceDate, out strCurrentDate);
                if (intReturn == -1)
                {
                    MessageBox.Show("��ȡ�ϴ��ս�ʱ��ʧ�ܣ����ܽ����ս������");
                    return;
                }

                if (string.IsNullOrEmpty(strLastDayBalanceDate))
                {
                    this.ucInpatientDayBalanceDateControl1.dtLastBalance = DateTime.MinValue;
                    this.lastDate = DateTime.MinValue.ToString("yyyy-MM-dd HH:mm:ss");
                    this.ucInpatientDayBalanceDateControl1.tbLastDate.Text = this.lastDate;

                }
                else
                {
                    this.ucInpatientDayBalanceDateControl1.dtLastBalance = NConvert.ToDateTime(strLastDayBalanceDate);
                    this.ucInpatientDayBalanceDateControl1.tbLastDate.Text = strLastDayBalanceDate;
                    this.lastDate = strLastDayBalanceDate;
                }
                this.ucInpatientDayBalanceDateControl1.dtpBalanceDate.Value = NConvert.ToDateTime(strCurrentDate);
            }
        }
        #endregion
    }
}
