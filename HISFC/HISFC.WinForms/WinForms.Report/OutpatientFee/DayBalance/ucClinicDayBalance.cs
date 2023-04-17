using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;
using FS.FrameWork.Function;

namespace FS.WinForms.Report.OutpatientFee.DayBalance
{
    public partial class ucClinicDayBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucClinicDayBalance()
        {
            InitializeComponent();
        }

        #region ��������
        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        NeuObject currentOperator = new NeuObject();
        /// <summary>
        /// ��ǰ����
        /// </summary>
        NeuObject currentDepartment = new NeuObject();

        /// <summary>
        /// �ս᷽����
        /// </summary>
        Function.ClinicDayBalance clinicDayBalance = new Report.OutpatientFee.Function.ClinicDayBalance();

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        /// <summary>
        /// �ϴ��ս�ʱ��
        /// </summary>
        string lastDate = "";

        /// <summary>
        /// �����ս�ʱ��
        /// </summary>
        string dayBalanceDate = "";

        /// <summary>
        /// �ս����ʱ��
        /// </summary>
        string operateDate = "";

        /// <summary>
        /// �Ƿ���Խ����սᣨtrue-����/false-�����ԣ�
        /// </summary>
        public bool enableBalance = false;

        /// <summary>
        /// Ҫ�ս������
        /// </summary>
        ArrayList alBalanceData = new ArrayList();
        #endregion

        #region ����

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
                currentOperator = this.clinicDayBalance.Operator;

                // ��ȡ��ǰ����
                currentDepartment = ((FS.HISFC.Models.Base.Employee)(this.clinicDayBalance.Operator)).Dept;

                // ��ȡ���һ���ս�ʱ��
                intReturn = this.GetLastBalanceDate();
                if (intReturn == -1)
                {
                    MessageBox.Show("��ȡ�ϴ��ս�ʱ��ʧ�ܣ����ܽ����ս������");
                    return;
                }
                else if (intReturn == 0)
                {
                    // û�������սᣬ�����ϴ��ս�ʱ��Ϊ��Сʱ��
                    this.lastDate = System.DateTime.MinValue.ToString();
                    this.ucClinicDayBalanceDateControl1.tbLastDate.Text = System.DateTime.MinValue.ToString();
                }
                else
                {
                    // �����ս�
                    this.ucClinicDayBalanceDateControl1.tbLastDate.Text = this.lastDate.ToString();
                    this.ucClinicDayBalanceDateControl1.dtpBalanceDate.Value = this.clinicDayBalance.GetDateTimeFromSysDateTime();
                }

                // ��ʼ���ӿؼ��ı���
                this.ucClinicDayBalanceDateControl1.dtLastBalance = NConvert.ToDateTime(this.lastDate);
                this.ucClinicDayBalanceReport.labelReportDate.Text = this.clinicDayBalance.GetDateTimeFromSysDateTime().ToLongDateString();
                this.ucClinicDayBalanceReport.InitUC();
                this.ucReportReprint.InitUC();
            }
            catch { }
        }
        #endregion

        #region ��ȡ�տ�Ա�ϴ��ս�ʱ��
        /// <summary>
        /// ��ȡ�տ�Ա�ϴ��ս�ʱ��
        /// </summary>
        /// <returns></returns>
        public int GetLastBalanceDate()
        {

            try
            {
                // ��������
                int intReturn = 0;

                // ��ȡ�տ�Ա�ϴ��ս�ʱ��
                intReturn = clinicDayBalance.GetLastBalanceDate(this.currentOperator, ref lastDate);

                // �жϻ�ȡ���
                if (intReturn == -1)
                {
                    MessageBox.Show("��ȡ�տ�Ա���һ���ս�ʱ��ʧ�ܣ�");
                    return -1;
                }
                return intReturn;
            }
            catch
            {
                return -1;
            }
        }
        #endregion

        #region ��ѯ�����ս������
        /// <summary>
        /// ��ѯ�����ս������
        /// </summary>
        public void QueryDayBalanceData()
        {
            //
            // ��������
            //
            // ���ص��ս�����
            System.Data.DataSet dsBalanceDate = new DataSet();
            // ���ص��õĽ��
            int intReturn = 0;
            // ��ѯ���صĽ��
            ArrayList alDayBalance = new ArrayList();
            // �ȴ�����
            FS.FrameWork.WinForms.Forms.frmWait waitForm = new FS.FrameWork.WinForms.Forms.frmWait();

            //
            // ������б�����ս�����
            //
            this.alBalanceData = null;

            //
            // ��ȡ�ս��ֹʱ��
            //
            intReturn = this.ucClinicDayBalanceDateControl1.GetBalanceDate(ref dayBalanceDate);
            if (intReturn == -1)
            {
                this.enableBalance = false;
                return;
            }

            // ��ʾ�ȴ�����
            waitForm.Tip = "���ڻ�ȡ�������ս�����";
            waitForm.Show();

            //
            // ��ȡ�ս�����
            //
            intReturn = this.clinicDayBalance.GetDayBalanceData(this.currentOperator.ID, this.lastDate, dayBalanceDate, ref dsBalanceDate);
            if (intReturn == -1)
            {
                waitForm.Hide();
                MessageBox.Show("��ȡ�����տ�Ա���ս�����ʧ��" + this.clinicDayBalance.Err);
                this.enableBalance = false;
                return;
            }
            if (dsBalanceDate.Tables[0].Rows.Count == 0)
            {
                waitForm.Hide();
                MessageBox.Show("��ʱ���û�п��õ��ս�����");
                this.enableBalance = false;
                return;
            }

            //
            // �����ս�����
            //
            this.Calculate(dsBalanceDate, ref alDayBalance);

            //
            // ����FarPoint
            //
            this.SetFarPoint(alDayBalance, this.ucClinicDayBalanceReport.fpSpread1_Sheet1);

            //
            // ����˴β�ѯ���ս�����
            //
            this.alBalanceData = alDayBalance;

            this.enableBalance = true;
            waitForm.Hide();
        }
        #endregion

        #region �����ս�����
        /// <summary>
        /// �����ս�����
        /// </summary>
        /// <param name="dsBalanceData">��ȡ���ս�����</param>
        /// <param name="argArrayList">���ص�ʵ������</param>
        public void Calculate(DataSet dsBalanceData, ref ArrayList argArrayList)
        {
            #region ��������

            // ʵ������
            ArrayList alDayBalance = new ArrayList();
            // ǰһ����Ʊ��
            string priviousInvoice = "";
            // ��һ����Ʊ��
            string firstInvoice = "";
            // �Ƿ��������һ�ŷ�Ʊ�Ų�ͬ������
            bool boolDifferent = false;
            // ����ǰ��ͬ�Ĵ���
            long intDifferent = 0;
            // ʵ�ս��
            decimal ownCost = 0;
            // ���ʽ��
            decimal accountCost = 0;
            // �ܽ��
            decimal totalCost = 0;
            // ���ʵ���
            int accountCount = 0;
            // ǰһ���ս���Ŀ
            string priviousBalanceItem = "";
            // �Ƿ������ǰһ�ŷ�Ʊ��ͬ���ս���Ŀ
            bool boolItem = false;
            #endregion

            #region ѭ������
            // �����һ����Ʊ��
            firstInvoice = dsBalanceData.Tables[0].Rows[0][0].ToString();
            // ��ʼ��ǰһ����Ʊ��
            priviousInvoice = dsBalanceData.Tables[0].Rows[0][0].ToString();
            // �����һ���ս���Ŀ
            priviousBalanceItem = dsBalanceData.Tables[0].Rows[0][4].ToString();

            // ѭ������
            foreach (DataRow drData in dsBalanceData.Tables[0].Rows)
            {
                // �ս�ʵ����
                Class.ClinicDayBalance dayBalance = new Class.ClinicDayBalance();
                // ��ǰ��Ʊ��
                string currentInvoice = "";
                // ʵ�ս��
                decimal decOwnCost = 0;
                // ���ʽ��
                decimal decAccountCost = 0;
                // �ܽ��
                decimal decTotalCost = 0;
                // ���ʵ���
                //int intAccountCount = 0;
                // �ս���Ŀ
                string stringItem = "";

                // ��ȡ��ǰ��Ʊ��
                currentInvoice = drData[0].ToString();

                // ��ȡ��ǰ�ս���Ŀ
                stringItem = drData[4].ToString();

                // ��ȡ���ֽ��
                decOwnCost = FS.FrameWork.Function.NConvert.ToDecimal(drData[1].ToString());
                decAccountCost = FS.FrameWork.Function.NConvert.ToDecimal(drData[2].ToString());
                decTotalCost =  FS.FrameWork.Function.NConvert.ToDecimal(drData[5].ToString());

                // ���ǰһ��ѭ��������Ʊ�Ų���������ô���»�ȡ��һ�ŷ�Ʊ��
                if (boolDifferent)
                {
                    firstInvoice = priviousInvoice;
                    boolDifferent = false;
                }

                // ���ǰһ��ѭ��������Ŀ��ͬ����ô���»�ȡǰһ����Ŀ
                if (boolItem)
                {
                    firstInvoice = priviousInvoice;
                    boolItem = false;
                }

                //
                // �����ǰ�ս���Ŀ��ǰһ���ս���Ŀ��ͬ����Ҫ��Ϊһ��ʵ�壬�������õ�һ�ŷ�Ʊ��Ϊ��ǰ��Ʊ��
                //
                if (stringItem != priviousBalanceItem)
                {
                    //
                    // ����ʵ��
                    //
                    this.SetEntity(firstInvoice, priviousInvoice, priviousBalanceItem, ownCost, accountCost, totalCost, accountCount, ref dayBalance);

                    // �洢��ʵ������
                    alDayBalance.Add(dayBalance);

                    // ���ø�������
                    priviousInvoice = currentInvoice;
                    firstInvoice = currentInvoice;
                    boolItem = true;
                    intDifferent++;
                    ownCost = 0;
                    accountCost = 0;
                    totalCost = 0;
                    accountCount = 0;
                }
                else if ((currentInvoice != priviousInvoice) && (long.Parse(priviousInvoice) != (long.Parse(currentInvoice) - 1)))
                {
                    //
                    // ���������Ʊ�Ų�����������㷢Ʊ���ݣ�ͬʱ����
                    //
                    // ����ʵ��
                    //
                    this.SetEntity(firstInvoice, priviousInvoice, priviousBalanceItem, ownCost, accountCost, totalCost, accountCount, ref dayBalance);

                    // �洢��ʵ������
                    alDayBalance.Add(dayBalance);

                    // ���ø�������
                    boolDifferent = true;
                    firstInvoice = currentInvoice;
                    intDifferent++;
                    ownCost = 0;
                    accountCost = 0;
                    totalCost = 0;
                    accountCount = 0;
                }
                //else
                {
                    // ���ܸ��ֽ��
                    ownCost += decOwnCost;
                    accountCost += decAccountCost;
                    totalCost += decTotalCost;

                    // �ϼƼ��ʵ���
                    if (drData[5].ToString() == "2")
                    {
                        accountCount++;
                    }
                }

                // �洢��ǰ��Ʊ����Ϊ�´�ѭ������һ�η�Ʊ��
                priviousInvoice = drData[0].ToString();

                // �洢��ǰ��Ŀ�����Ϊ�´�ѭ������һ���ս���Ŀ
                priviousBalanceItem = drData[4].ToString();
            }

            // ���������Ϊ0��˵��һ����ͬ��Ŀ���ϺŶ�û��
            if (intDifferent == 0)
            {
                // �ս�ʵ����
                Class.ClinicDayBalance dayBalance = new Class.ClinicDayBalance();
                // ��ǰ��Ʊ��
                string currentInvoice = "";
                firstInvoice = dsBalanceData.Tables[0].Rows[0][0].ToString();

                ownCost = 0;
                totalCost = 0;
                accountCost = 0;
                accountCount = 0;
                foreach (DataRow drData in dsBalanceData.Tables[0].Rows)
                {
                    // ��ǰ��Ʊ��
                    currentInvoice = drData[0].ToString();
                    // ʵ�ս��
                    ownCost += decimal.Parse(drData[1].ToString());
                    // ���ʽ��
                    accountCost += decimal.Parse(drData[2].ToString());
                    // �ܽ��
                    totalCost += decimal.Parse(drData[5].ToString());
                    // ���ʵ�����
                    if (drData[5].ToString() == "2")
                    {
                        accountCount++;
                    }
                    // �ս���Ŀ
                    priviousBalanceItem = drData[3].ToString();
                }

                // ����ʵ��
                this.SetEntity(firstInvoice, currentInvoice, priviousBalanceItem, ownCost, accountCost, totalCost, accountCount, ref dayBalance);
                alDayBalance.Add(dayBalance);
            }
            else
            {
                Class.ClinicDayBalance dayBalance = new Class.ClinicDayBalance();
                this.SetEntity(firstInvoice, priviousInvoice, priviousBalanceItem, ownCost, accountCost, totalCost, accountCount, ref dayBalance);
                alDayBalance.Add(dayBalance);
            }
            argArrayList = alDayBalance;
            #endregion
        }
        #endregion

        #region ����ʵ��
        /// <summary>
        /// ����ʵ��
        /// </summary>
        /// <param name="argPriInvoice">ǰһ����Ʊ��</param>
        /// <param name="argCurrInvoice">��ǰ��Ʊ��</param>
        /// <param name="argOwnCost">ʵ�ս��</param>
        /// <param name="argLeftCost">���ʽ��</param>
        /// <param name="argTotalCost">�ܽ��</param>
        /// <param name="argAccount">���ʵ���</param>
        /// <param name="argDayBalance">���ص�ʵ��</param>
        public void SetEntity(string argPriInvoice, string argCurrInvoice, string argItem,
            decimal argOwnCost, decimal argLeftCost, decimal argTotalCost,
            int argAccount, ref Class.ClinicDayBalance argDayBalance)
        {
            // ��Ʊ��
            if (argPriInvoice != argCurrInvoice)
            {
                argDayBalance.InvoiceNo = argPriInvoice + "��" + argCurrInvoice;
            }
            else
            {
                argDayBalance.InvoiceNo = argCurrInvoice;
            }
            // ��Ʊ����
            argDayBalance.Memo = (long.Parse(argCurrInvoice) - long.Parse(argPriInvoice) + 1).ToString();
            // ʵ�ս��
            argDayBalance.Cost.OwnCost = argOwnCost;
            // ���ʽ��
            argDayBalance.Cost.LeftCost = argLeftCost;
            // �ܽ��
            argDayBalance.Cost.TotCost = argTotalCost;
            // �ս���Ŀ
            if (argItem == "1")
            {
                argDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Valid;
            }
            else if (argItem == "0")
            {
                argDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Canceled;
            }
            else if (argItem == "2")
            {
                argDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Reprint;
            }
            else
            {
                argDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.LogOut;
            }
            // ���ʵ���Ŀ
            argDayBalance.AccountNumber = argAccount;
            // ��ʼʱ��
            if (DateTime.MinValue == DateTime.Parse(this.lastDate))
            {
                // ���û�������սᣬ��ô���ϴ��ս�ʱ�����ó�ȥ���������
                DateTime dtToday = new DateTime();
                dtToday = this.clinicDayBalance.GetDateTimeFromSysDateTime();
                this.lastDate = (new System.DateTime(dtToday.Year - 1, dtToday.Month, dtToday.Day, 0, 1, 1)).ToString();
                argDayBalance.BeginDate = DateTime.Parse(this.lastDate);
            }
            else
            {
                argDayBalance.BeginDate = NConvert.ToDateTime(this.lastDate);
            }
            // ��ֹʱ��
            argDayBalance.EndDate = NConvert.ToDateTime(this.dayBalanceDate);
            // ����Ա����
            argDayBalance.BalanceOperator = this.currentOperator;
            // �������
            argDayBalance.CheckFlag = "1";
            // �����
            argDayBalance.CheckOperator = new NeuObject();
            // ���ʱ��
            argDayBalance.CheckDate = DateTime.MinValue;
        }
        #endregion

        #region ����FarPoint
        /// <summary>
        /// ����FarPoint
        /// </summary>
        /// <param name="alDayBalance">ʵ������</param>
        /// <param name="sheet">Ҫ��ʾ��FarPoint</param>
        public void SetFarPoint(ArrayList alDayBalance, FarPoint.Win.Spread.SheetView sheet)
        {
            // ��Ʊ����
            long invoiceCount = 0;
            // ʵ�ս��
            decimal ownCost = 0;
            // ���ʽ��
            decimal leftCost = 0;
            // �ܽ��
            decimal totalCost = 0;
            // ���ʵ�����
            long accountCount = 0;
            // �к�
            int intRow = 0;

            // ���FarPoint
            sheet.RowCount = 0;

            // ѭ����ֵ
            foreach (Class.ClinicDayBalance dayBalance in alDayBalance)
            {
                if (dayBalance.Memo != string.Empty)
                {
                    invoiceCount += long.Parse(dayBalance.Memo);
                }
                ownCost += dayBalance.Cost.OwnCost;
                leftCost += dayBalance.Cost.LeftCost;
                totalCost += dayBalance.Cost.TotCost;
                accountCount += dayBalance.AccountNumber;

                // ��������
                sheet.AddRows(sheet.RowCount, 1);

                // ��ȡ������к�
                intRow = sheet.RowCount - 1;

                // ��Ʊ��
                sheet.Cells[intRow, 0].Text = dayBalance.InvoiceNo;
                // ��Ʊ����
                sheet.Cells[intRow, 1].Text = dayBalance.Memo;
                // ʵ�ս��
                sheet.Cells[intRow, 2].Text = dayBalance.Cost.OwnCost.ToString();
                // ���ʽ��
                sheet.Cells[intRow, 3].Text = dayBalance.Cost.LeftCost.ToString();
                // �ܽ��
                sheet.Cells[intRow, 4].Text = dayBalance.Cost.TotCost.ToString();
                // ���ʵ�����
                sheet.Cells[intRow, 5].Text = dayBalance.AccountNumber.ToString();
                // �ս���Ŀ
                //if (dayBalance.BalanceItem == FS.HISFC.Models.Base.CancelTypes.Valid)
                //{
                //    sheet.Cells[intRow, 6].Text = "����";
                //}
                //else if (dayBalance.BalanceItem == FS.HISFC.Models.Base.CancelTypes.Canceled)
                //{
                //    sheet.Cells[intRow, 6].Text = "�˷�";
                //}
                //else if (dayBalance.BalanceItem == FS.HISFC.Models.Base.CancelTypes.Reprint)
                //{
                //    sheet.Cells[intRow, 6].Text = "�ش�";
                //}
                //else
                //{
                //    sheet.Cells[intRow, 6].Text = "ע��";
                //}
            }

            //
            // �ϼ���Ŀ
            //
            // ��������
            sheet.AddRows(sheet.RowCount, 1);
            // ��ȡ������к�
            intRow = sheet.RowCount - 1;
            // ��ֵ
            sheet.Cells[intRow, 0].Text = "�ϼ�";
            sheet.Cells[intRow, 1].Text = invoiceCount.ToString();
            sheet.Cells[intRow, 2].Text = ownCost.ToString();
            sheet.Cells[intRow, 3].Text = leftCost.ToString();
            sheet.Cells[intRow, 4].Text = totalCost.ToString();
            sheet.Cells[intRow, 5].Text = accountCount.ToString();
            sheet.Cells[intRow, 6].Text = "";

            //
            // �ϲ����
            //
            //��дʵ�ս��
            sheet.AddRows(sheet.RowCount, 1);
            intRow = sheet.RowCount - 1;
            sheet.Models.Span.Add(intRow, 0, 1, 7);
            sheet.Cells[intRow, 0].Text = "ʵ�ս��(��д): " + NConvert.ToCapital(decimal.Parse(sheet.Cells[intRow - 1, 4].Text));
            if (sheet.Cells[intRow - 1, 4].Text == "0")
            {
                sheet.Cells[intRow, 0].Text = "ʵ�ս��(��д): ��Ԫ������";
            }
            // ����Ա��Ϣ
            sheet.AddRows(sheet.RowCount, 1);
            intRow = sheet.RowCount - 1;
            sheet.Models.Span.Add(intRow, 0, 1, 3);
            sheet.Models.Span.Add(intRow, 3, 1, 4);
            sheet.Cells[intRow, 0].Text = "�ɿ���: " + this.currentOperator.Name;
            sheet.Cells[intRow, 3].Text = "�տ�Ա: " + this.currentOperator.Name;
            sheet.Cells[intRow, 3].HorizontalAlignment =  FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            // ����˺ͳ���Ա
            sheet.AddRows(sheet.RowCount, 1);
            intRow = sheet.RowCount - 1;
            sheet.Models.Span.Add(intRow, 0, 1, 3);
            sheet.Models.Span.Add(intRow, 3, 1, 4);
            sheet.Cells[intRow, 0].Text = "�����: " + "".PadLeft(this.currentOperator.Name.Length * 2, ' ');
            sheet.Cells[intRow, 3].Text = "����Ա: " + "".PadLeft(this.currentOperator.Name.Length * 2, ' ');
            sheet.Cells[intRow, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;

            // ͳ��ʱ��
            sheet.AddRows(sheet.RowCount, 1);
            intRow = sheet.RowCount - 1;
            sheet.Models.Span.Add(intRow, 0, 1, 7);
            if (this.tabControl1.SelectedIndex == 0)
            {
                sheet.Cells[intRow, 0].Text = "ͳ��ʱ��: " + this.lastDate + " �� " + this.dayBalanceDate;
            }
            else
            {
                foreach (Class.ClinicDayBalance dayBalance in alDayBalance)
                {
                    sheet.Cells[intRow, 0].Text = "ͳ��ʱ��: " + dayBalance.BeginDate + " �� " + dayBalance.EndDate;
                    return;
                }
            }
        }
        #endregion

        #region ��ӡ
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="argPanel">Ҫ��ӡ��Panel</param>
        public void PrintPanel(System.Windows.Forms.Panel argPanel)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(0, 0, argPanel);
        }
        #endregion

        #region �����ս�����
        /// <summary>
        /// �����ս�����
        /// </summary>
        public void DayBalance()
        {
            if (this.alBalanceData == null)
            {
                return;
            }
            
            if (MessageBox.Show("�Ƿ�����ս�,�ս�����ݽ����ָܻ�?", "�����տ�Ա�ɿ��ձ�", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                //
                // ��������
                //
                // ����ֵ
                int intReturn = 0;
                // �������
                //FS.FrameWork.Management.Transaction transaction = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                // �ȴ�����
                FS.FrameWork.WinForms.Forms.frmWait waitForm = new FS.FrameWork.WinForms.Forms.frmWait();
                // �����շ�ҵ���
                FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();
                // �ս����
                string sequence = "";

                //
                // �жϺϷ���
                //
                if (!this.enableBalance)
                {
                    MessageBox.Show("���ܽ����ս�");
                    return;
                }
                if (this.alBalanceData == null)
                {
                    return;
                }

                // �����ȴ�����
                waitForm.Tip = "���ڽ����ս�";
                waitForm.Show();

                //
                // ��������
                //
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //transaction.BeginTransaction();
                this.clinicDayBalance.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                outpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //
                // �����ս�����
                //
                this.operateDate = this.clinicDayBalance.GetDateTimeFromSysDateTime().ToString();
                // ��ȡ�ս����
                intReturn = this.clinicDayBalance.GetBalanceSequence(ref sequence);
                if (intReturn == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    waitForm.Hide();
                    MessageBox.Show("��ȡ�ս����к�ʧ��");
                    return;
                }
                foreach (Class.ClinicDayBalance tempBalance in this.alBalanceData)
                {
                    tempBalance.BalanceSequence = sequence;
                    tempBalance.BalanceDate = DateTime.Parse(this.operateDate);
                    intReturn = clinicDayBalance.CreateClinicDayBalance(tempBalance);
                    if (intReturn == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        waitForm.Hide();
                        MessageBox.Show("�ս�ʧ��" + outpatient.Err);
                        return;
                    }
                }

                //
                // ����������
                //
                // ���·�Ʊ�����FIN_OPB_INVOICEINFO
                intReturn = outpatient.UpdateInvoiceForDayBalance(DateTime.Parse(this.lastDate),
                    DateTime.Parse(this.dayBalanceDate),
                    "1",
                    sequence,
                    DateTime.Parse(this.operateDate));
                if (intReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    waitForm.Hide();
                    MessageBox.Show("���·�Ʊ����ʧ��" + outpatient.Err);
                    return;
                }
                // ���·�Ʊ��ϸ��FIN_OPB_INVOICEDETAIL
                intReturn = outpatient.UpdateInvoiceDetailForDayBalance(DateTime.Parse(this.lastDate),
                    DateTime.Parse(this.dayBalanceDate),
                    "1",
                    sequence,
                    DateTime.Parse(this.operateDate));
                if (intReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    waitForm.Hide();
                    MessageBox.Show("���·�Ʊ��ϸ��ʧ��");
                    return;
                }
                // ����֧�������FIN_OPB_PAYMODE
                intReturn = outpatient.UpdatePayModeForDayBalance(DateTime.Parse(this.lastDate),
                    DateTime.Parse(this.dayBalanceDate),
                    "1",
                    sequence,
                    DateTime.Parse(this.operateDate));
                if (intReturn <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    waitForm.Hide();
                    MessageBox.Show("����֧�������ʧ��" + outpatient.Err);
                    return;
                }

                //
                // ����ɹ�
                //
                FS.FrameWork.Management.PublicTrans.Commit();
                waitForm.Hide();
                MessageBox.Show("�ս�ɹ����");
                this.PrintPanel(this.panelPrint);

                alBalanceData = null;

                // �����ϴ��ս�ʱ����ʾ
                this.ucClinicDayBalanceDateControl1.tbLastDate.Text = this.dayBalanceDate;
                this.ucClinicDayBalanceDateControl1.dtpBalanceDate.Value = this.clinicDayBalance.GetDateTimeFromSysDateTime();
            }
        }
        #endregion

        #region ��ѯ�ս��¼
        /// <summary>
        /// ��ѯ�ս��¼
        /// </summary>
        public void QueryBalanceRecorde()
        {
            // ����ֵ
            int intReturn = 0;
            // ��ѯ����ʼʱ��
            DateTime dtFrom = DateTime.MinValue;
            // ��ѯ�Ľ�ֹʱ��
            DateTime dtTo = DateTime.MinValue;
            // ���ص���־��¼
            ArrayList balanceRecord = new ArrayList();
            // ���ص���־��ϸ
            ArrayList balanceDetail = new ArrayList();
            // ��ѯ���ռ���ˮ��
            string sequence = "";

            // ��ȡ��ѯʱ��
            intReturn = this.ucReprintDateTime.GetInputDateTime(ref dtFrom, ref dtTo);
            if (intReturn == -1)
            {
                return;
            }

            // ��ȡ��ѯ���
            intReturn = this.clinicDayBalance.GetBalanceRecord(this.currentOperator, dtFrom, dtTo, ref balanceRecord);
            if (intReturn == -1)
            {
                MessageBox.Show("��ȡ��־��¼ʧ��");
                return;
            }

            // �жϽ����¼���������������ô�����������û�ѡ��
            if (balanceRecord.Count > 1)
            {
                frmConfirmBalanceRecord confirmBalanceRecord = new frmConfirmBalanceRecord();
                confirmBalanceRecord.BalanceRecord = balanceRecord;
                if (confirmBalanceRecord.ShowDialog() == DialogResult.OK)
                {
                    sequence = confirmBalanceRecord.fpSpread1.Sheets[0].Cells[confirmBalanceRecord.fpSpread1.Sheets[0].ActiveRowIndex, 0].Text;
                }
                else
                {
                    return;
                }
            }
            else
            {
                foreach (NeuObject obj in balanceRecord)
                {
                    sequence = obj.ID;
                }
            }

            // �����ս���Ż�ȡ�ս���ϸ
            intReturn = this.clinicDayBalance.GetDayBalanceDetail(sequence, ref balanceDetail);
            if (intReturn == -1)
            {
                MessageBox.Show("��ȡ�ս���ϸʧ�ܣ�" + this.clinicDayBalance.Err);
            }

            // ����FarPoint
            this.SetFarPoint(balanceDetail, this.ucReportReprint.fpSpread1_Sheet1);
        }
        #endregion

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitUC();

            toolBarService.AddToolButton("�ս�", "�����ս���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            
            return this.toolBarService;
        }

        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
 	        if(e.ClickedItem.Text == "�ս�")
            {
                // �ս�
                if (this.enableBalance)
                {
                    this.DayBalance();
                }
            }
            
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            // ��ѯ
            if (this.tabControl1.SelectedIndex == 0)
            {
                // ��ѯ�ս�����
                this.QueryDayBalanceData();
            }
            else
            {
                // ��ѯ�ս��¼
                this.QueryBalanceRecorde();
            }
            
            return base.OnQuery(sender, neuObject);
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            // ��ӡ
            if (this.tabControl1.SelectedIndex == 0)
            {
                if (MessageBox.Show("�Ƿ��ӡ��", "��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.PrintPanel(this.panelPrint);
                }
            }
            else
            {
                if (MessageBox.Show("�Ƿ��ӡ��", "��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.PrintPanel(this.panelReprint);
                }
            }
            
            return base.OnPrint(sender, neuObject);
        }


        #endregion

        #region �����¼�
        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.F4)
            {
                // �ս�
                if (this.enableBalance)
                {
                    this.QueryDayBalanceData();
                }
                else
                {
                    MessageBox.Show("���ܽ����ս����!");
                }
                return true;
            }
            else if (keyData == Keys.F5)
            {
                // ��ѯ
                if (this.tabControl1.SelectedIndex == 0)
                {
                    this.QueryDayBalanceData();
                }
                else
                {
                    this.QueryBalanceRecorde();
                }
                return true;
            }
            else if (keyData == Keys.F8)
            {
                // ��ӡ
                if (this.tabControl1.SelectedIndex == 0)
                {
                    if (MessageBox.Show("�Ƿ��ӡ��", "��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.PrintPanel(this.panelPrint);
                    }
                }
                else
                {
                    if (MessageBox.Show("�Ƿ��ӡ��", "��ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.PrintPanel(this.panelReprint);
                    }
                }
                return true;
            }
            else if (keyData == Keys.F12)
            {
                // �˳�
                this.ParentForm.Close();
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        #endregion
    }
}
