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

namespace FS.Report.Finance.FinOpb
{
    public partial class ucClinicDayBalanceNew : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucClinicDayBalanceNew()
        {
            InitializeComponent();
        }

        #region ��������
        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        NeuObject currentOperator = new NeuObject();

        /// <summary>
        /// �ս᷽����
        /// </summary>
        Function.ClinicDayBalance clinicDayBalance = new FS.Report.Finance.FinOpb.Function.ClinicDayBalance();

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
        /// �Ƿ�����ս�
        /// </summary>
        bool enableBalance = true;
        /// <summary>
        /// �ս����ʱ��
        /// </summary>
        string operateDate = "";
        /// <summary>
        /// Ҫ�ս������
        /// </summary>
        public List<Class.ClinicDayBalanceNew> alData = new List<FS.Report.Finance.FinOpb.Class.ClinicDayBalanceNew>();
        private Class.ClinicDayBalanceNew dayBalance = null;
        
        #region ���Ų�ƽ֮��������
        //{0EA3CF1A-2F03-46c3-83AE-1543B00BBDDB}
        decimal d�ϼ�, d�б��˻�, d�б�ͳ��, d�б����, dʡ���˻�, dʡ��ͳ��, dʡ�����, dʡ������Ա, d�Ͻ��ֽ�, d����ҽ��,d�����˻�, d��������,d������;

        #endregion


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
                currentOperator = this.clinicDayBalance.Operator;

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
                this.ucClinicDayBalanceReportNew1.InitUC("�����շ�Ա�ɿ��ձ���");
                this.ucClinicDayBalanceReportNew2.InitUC("�����շ�Ա�ɿ��ձ���");
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

        #region ��ѯҪ�ս������
        /// <summary>
        /// ��ѯҪ�ս������
        /// </summary>
        private void QueryDayBalanceData()
        {
            this.alData.Clear();
            DataSet ds;
            int intReturn = 0;
            //
            // ��ȡ�ս��ֹʱ��
            //
            intReturn = this.ucClinicDayBalanceDateControl1.GetBalanceDate(ref dayBalanceDate);
            if (intReturn == -1)
            {
                this.enableBalance = false;
                return;
            }
            //��ʾ������Ϣ
            this.SetInfo(lastDate, dayBalanceDate, 0);
            //�������
            FarPoint.Win.Spread.SheetView sheet = ucClinicDayBalanceReportNew1.neuSpread1_Sheet1;
            if (sheet.Rows.Count > 0)
                sheet.Rows.Remove(0, sheet.Rows.Count);

            //��ȡ�ս�������
            ds = new DataSet();
            //clinicDayBalance.GetDayBalanceDataNew(this.currentOperator.ID, this.lastDate, dayBalanceDate, ref ds);
            clinicDayBalance.GetDayBalanceDataMZRJ(this.currentOperator.ID, this.lastDate, dayBalanceDate, ref ds);
            if (ds != null && ds.Tables.Count >= 0 && ds.Tables[0].Rows.Count > 0)
            {
                this.SetDetial(ds.Tables[0]);
            }
            else
            {
                MessageBox.Show("�ö�ʱ����û��Ҫ�ս�����ݣ�");
                return;
            }
            //����farpoint��ʽ
            this.ucClinicDayBalanceReportNew1.SetFarPoint();
            //��ʾ��Ʊ����
            SetInvoice(sheet);
            //��ʾ�������
            this.SetMoneyValue(sheet);

            this.enableBalance = true;
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
            if (ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count > 0)
                ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Remove(0, ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count - 1);
            //����Farpoint������
            int count = table.Rows.Count;
            decimal countMoney = 0;
            if (count % 2 == 0)
            {
                ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count = Convert.ToInt32(count / 2);
            }
            else
            {
                ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count = Convert.ToInt32(count / 2) + 1;
            }

            //��ʾ��Ŀ����
            for (int i = 0; i < count; i++)
            {
                int index = Convert.ToInt32(i / 2);
                int intMod = (i + 1) % 2;
                if (intMod > 0)
                {
                    ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Models.Span.Add(index, 0, 1, 2);
                    ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[index, 0].Text = table.Rows[i][1].ToString();
                    ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[index, 2].Text = table.Rows[i][2].ToString();
                }
                else
                {
                    ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Models.Span.Add(index, 3, 1, 2);
                    ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[index, 3].Text = table.Rows[i][1].ToString();
                    ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[index, 5].Text = table.Rows[i][2].ToString();
                }
                #region ����ʵ��
                dayBalance = new FS.Report.Finance.FinOpb.Class.ClinicDayBalanceNew();
                dayBalance.InvoiceNO.ID = table.Rows[i][0].ToString();
                dayBalance.InvoiceNO.Name = table.Rows[i][1].ToString();
                dayBalance.TotCost = NConvert.ToDecimal(table.Rows[i][2]);
                dayBalance.TypeStr = "4";
                dayBalance.SortID = "TOT_COST";
                this.SetDayBalance();
                #endregion
                countMoney += Convert.ToDecimal(table.Rows[i][2]);

            }
            if (count % 2 > 0)
            {
                ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Models.Span.Add(ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count - 1, 3, 1, 2);
            }
            //��ʾ�ϼ�
            ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count += 1;
            count = ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count;
            ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Models.Span.Add(count - 1, 0, 1, 2);
            ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[count - 1, 0].Text = "�ϼƣ�";
            ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Models.Span.Add(count - 1, 2, 1, 4);
            ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[count - 1, 2].Text = countMoney.ToString();
            this.d�ϼ� = countMoney;

        }

        /// <summary>
        /// ������ʾ��Ʊ����
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoice(FarPoint.Win.Spread.SheetView sheet)
        {
            //��ȡ��Ʊ����
            DataSet ds = new DataSet();
            int resultValue = clinicDayBalance.GetDayInvoiceDataNew(this.currentOperator.ID, this.lastDate, dayBalanceDate, ref ds);
            if (resultValue == -1) return;
            DataTable table = ds.Tables[0];
            if (table.Rows.Count == 0) return;
            //��ֹƱ�ݺ�
            this.SetOneCellText(sheet, "A00101", GetInvoiceStartAndEnd(ds.Tables[0]));//luoff
           // this.SetOneCellText(sheet, "A00102", table.Rows[table.Rows.Count - 1][0].ToString());//luoff
            DataView dv = table.DefaultView;
            //��Ʊ����
            dv.RowFilter = "trans_type='1'";
            this.SetOneCellText(sheet, "A002", dv.Count.ToString());

            //��ЧƱ��
            dv.RowFilter = "cancel_flag='1'";
            this.SetOneCellText(sheet, "A003", dv.Count.ToString());

            //�˷�Ʊ��
            dv.RowFilter = "cancel_flag='0' and trans_type='2'";
            this.SetOneCellText(sheet, "A00401", dv.Count.ToString());
            //�˷�Ʊ�ݺ� 
            string InvoiceStr = GetInvoiceStr(dv);
            this.SetOneCellText(sheet, "A00402", InvoiceStr);

            //����Ʊ��
            dv.RowFilter = "cancel_flag in ('2','3') and trans_type='2'";
            this.SetOneCellText(sheet, "A00501", dv.Count.ToString());
            //����Ʊ�ݺ�
            InvoiceStr = GetInvoiceStr(dv);
            this.SetOneCellText(sheet, "A00502", InvoiceStr);


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

        #region �����ʼ����ֹƱ�ݺ�  luoff

        private string GetInvoiceStartAndEnd(DataTable dt)
        {
            StringBuilder sb = new StringBuilder();
            int count = dt.Rows.Count-1;
            string minStr = dt.Rows[0][0].ToString();
            string maxStr = dt.Rows[0][0].ToString();
            for (int i = 0; i < count - 1; i++)
                for (int j = i + 1; j < count; j++)
                {
                    long froInt = Convert.ToInt32(dt.Rows[i][0].ToString());
                    long nxtInt = Convert.ToInt32(dt.Rows[j][0].ToString());
                    long chaInt = nxtInt - froInt;
                    if (chaInt > 1 )
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
            decimal money = 0;
            int resultValue;
            //�˷ѽ��
            resultValue = clinicDayBalance.GetDayBalanceCancelMoney(this.currentOperator.ID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                SetOneCellText(sheet, "A006", money.ToString());
            }
            //���Ͻ��
            resultValue = clinicDayBalance.GetDayBalanceFalseMoney(this.currentOperator.ID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                SetOneCellText(sheet, "A007", money.ToString());
            }
            //��������
            resultValue = clinicDayBalance.GetDayBalanceModeMoney(this.currentOperator.ID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                SetOneCellText(sheet, "A011", money.ToString());
            }
            DataSet ds = new DataSet();
            //���ѡ�ʡ����ҽ�����
            resultValue = clinicDayBalance.GetDayBalanceProtectMoney(this.currentOperator.ID, this.lastDate, dayBalanceDate, ref ds);
            if (resultValue != -1)
            {
                SetProtectValue(sheet, ds);
            }

            ds = new DataSet();//{0EA3CF1A-2F03-46c3-83AE-1543B00BBDDB}
            //��ѯ����
            resultValue = clinicDayBalance.GetDayBalancePublicMoney(this.currentOperator.ID, this.lastDate, dayBalanceDate, "03", ref ds);
            if (resultValue != -1)
            {
                SetPublicValue(sheet, ds);
                
            }
            //{0EA3CF1A-2F03-46c3-83AE-1543B00BBDDB}
            ds = new DataSet();
            //��ѯ����
            resultValue = clinicDayBalance.GetDayBalanceRebateMoney(this.currentOperator.ID, this.lastDate, dayBalanceDate, ref ds);
            if (resultValue != -1)
            {
                SetRebateValue(sheet, ds);

            }

            ds = new DataSet();
            //֧����ʽ���
            resultValue = clinicDayBalance.GetDayBalancePayTypeMoney(this.currentOperator.ID, this.lastDate, dayBalanceDate, ref ds);
            if (resultValue != -1)
            {
                SetPayTypeValue(sheet, ds);
            }
        }
        /// <summary>
        /// ��ʾҽ���������
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetProtectValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                switch (dr["pact_code"].ToString())
                {
                    case "4"://����
                        {
                            SetOneCellText(sheet, "A012", dr["pub_cost"].ToString()); //����ҽ��
                            this.d����ҽ�� = NConvert.ToDecimal(dr["pub_cost"]);
                            SetOneCellText(sheet, "A013", dr["pay_cost"].ToString());//�����Ը�
                            break;
                        }
                    case "2"://�л�
                        {
                            SetOneCellText(sheet, "A014", dr["own_cost"].ToString());//�б��Ը�

                            SetOneCellText(sheet, "A015", dr["pay_cost"].ToString());//�б��˻�

                            SetOneCellText(sheet, "A016", dr["pub_cost"].ToString());//�б�ͳ��   

                            SetOneCellText(sheet, "A017", dr["over_cost"].ToString());//�б����
                            this.d�б��˻� = NConvert.ToDecimal(dr["pay_cost"]);
                            this.d�б�ͳ�� = NConvert.ToDecimal(dr["pub_cost"]);
                            this.d�б���� = NConvert.ToDecimal(dr["over_cost"]);

                            break;
                        }
                    case "3"://ʡ��
                        {
                            SetOneCellText(sheet, "A018", dr["own_cost"].ToString());//ʡ���Ը�

                            SetOneCellText(sheet, "A019", dr["pay_cost"].ToString());//ʡ���˻�

                            SetOneCellText(sheet, "A020", dr["pub_cost"].ToString());//ʡ��ͳ��

                            SetOneCellText(sheet, "A021", dr["over_cost"].ToString());//ʡ�����

                            SetOneCellText(sheet, "A022", dr["official_cost"].ToString());//ʡ����Ա
                            this.dʡ���˻� = NConvert.ToDecimal(dr["pay_cost"]);
                            this.dʡ��ͳ�� = NConvert.ToDecimal(dr["pub_cost"]);
                            this.dʡ����� = NConvert.ToDecimal(dr["over_cost"]);
                            this.dʡ������Ա = NConvert.ToDecimal(dr["official_cost"]);

                            break;
                        }
                }
            }
        }

        /// <summary>
        /// ��ʾ���ѽ������
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetPublicValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {   
                SetOneCellText(sheet, "A012", dr["pub_cost"].ToString()); //����ҽ��
                this.d����ҽ�� = NConvert.ToDecimal(dr["pub_cost"]);
                SetOneCellText(sheet, "A013", dr["own_cost"].ToString());//�����Ը�  
                SetOneCellText(sheet,"A026",dr["pay_cost"].ToString());//�����˻�
                this.d�����˻� = NConvert.ToDecimal(dr["pay_cost"]);
                //SetOneCellText(sheet, "A010", dr["rebate_cost"].ToString());
                //this.d������ = NConvert.ToDecimal(dr["rebate_cost"]);
            }
        }
        //{0EA3CF1A-2F03-46c3-83AE-1543B00BBDDB}
        /// <summary>
        /// ��ʾ����������
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetRebateValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            foreach (DataRow dr in dt.Rows)
            {
                SetOneCellText(sheet, "A010", dr["rebate_cost"].ToString());
                this.d������ = NConvert.ToDecimal(dr["rebate_cost"]);
            }
        }


        
        /// <summary>
        /// ��֧��������ʾ���
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="ds"></param>
        protected virtual void SetPayTypeValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
            DataTable dt = ds.Tables[0];
            string payType = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                payType = dr[0].ToString();
                //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                //FS.HISFC.Models.Fee.EnumPayType ePayType = (FS.HISFC.Models.Fee.EnumPayType)
                //    Enum.Parse(typeof(FS.HISFC.Models.Fee.EnumPayType), payType);
                string ePayType = payType;
                switch (ePayType)
                {
                    case "CA": //�ֽ�
                        {
                            //this.SetOneCellText(sheet, "A023", Report.ReportClass.DealCent(FS.FrameWork.Function.NConvert.ToDecimal(dr[1].ToString())).ToString());
                            this.SetOneCellText(sheet, "A023", (this.d�ϼ� - this.dʡ����� - this.dʡ������Ա - this.dʡ��ͳ�� - this.dʡ���˻� - this.d�б���� - this.d�б�ͳ�� - this.d�б��˻� - this.d����ҽ�� - this.d�����˻� - this.d������).ToString());//{0EA3CF1A-2F03-46c3-83AE-1543B00BBDDB}
                            string strValue = GetOneCellText(sheet, "A011");
                            decimal tempTotCost = this.d�ϼ� - this.dʡ����� - this.dʡ������Ա - this.dʡ��ͳ�� - this.dʡ���˻� - this.d�б���� - this.d�б�ͳ�� - this.d�б��˻� - this.d����ҽ�� - this.d�����˻� - this.d������;
                            this.SetOneCellText(sheet, "A1000", FS.FrameWork.Public.String.LowerMoneyToUpper(tempTotCost));
                            //decimal tempTotCost = NConvert.ToDecimal(dr[1]) - NConvert.ToDecimal(strValue);
                            //this.SetOneCellText(sheet, "A1000", FS.FrameWork.Public.String.LowerMoneyToUpper(tempTotCost));
                            break;
                        }
                    case "CH": //֧Ʊ
                        {
                            this.SetOneCellText(sheet, "A024", dr[1].ToString());
                            break;
                        }
                    case "CD"://����
                        {
                            this.SetOneCellText(sheet, "A025", dr[1].ToString());
                            break;
                        }
                }
            }
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
            ArrayList balanceRecord = new ArrayList();
            // ��ѯ���ռ���ˮ��
            string sequence = "";

            //�������
            int count = this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count;
            if (count > 0)
            {
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Remove(0, count);
            }

            // ��ȡ��ѯʱ��
            intReturn = this.ucReprintDateControl1.GetInputDateTime(ref dtFrom, ref dtTo);
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

            string begin = string.Empty, end = string.Empty;

            // �жϽ����¼���������������ô�����������û�ѡ��
            if (balanceRecord.Count > 1)
            {
                frmConfirmBalanceRecord confirmBalanceRecord = new frmConfirmBalanceRecord();
                confirmBalanceRecord.BalanceRecord = balanceRecord;
                if (confirmBalanceRecord.ShowDialog() == DialogResult.OK)
                {
                    sequence = confirmBalanceRecord.fpSpread1.Sheets[0].Cells[confirmBalanceRecord.fpSpread1.Sheets[0].ActiveRowIndex, 0].Text;
                    begin = confirmBalanceRecord.fpSpread1.Sheets[0].Cells[confirmBalanceRecord.fpSpread1.Sheets[0].ActiveRowIndex, 1].Text;
                    end = confirmBalanceRecord.fpSpread1.Sheets[0].Cells[confirmBalanceRecord.fpSpread1.Sheets[0].ActiveRowIndex, 2].Text;
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
                    begin = obj.Name;
                    end = obj.Memo;
                }
            }
            //���ñ�����Ϣ
            this.SetInfo(begin, end, 1);
            //�����ս�����
            DataSet ds = new DataSet();
            intReturn = clinicDayBalance.GetDayBalanceRecord(sequence, ref ds);
            if (intReturn == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }
            if (ds.Tables.Count == 0 || ds == null || ds.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("��ʱ�����û��Ҫ���ҵ����ݣ�");
                return;
            }
            SetOldFarPointData(ds.Tables[0]);
            ds.Dispose();
        }
        #endregion

        #region �������ս�Farpoint����
        private void SetOldFarPointData(DataTable table)
        {
            FarPoint.Win.Spread.SheetView sheet = this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1;
            int rowCount = sheet.Rows.Count;
            if (sheet.Rows.Count > 0)
            {
                sheet.Rows.Remove(0, rowCount - 1);
            }
            DataView dv = table.DefaultView;
            //������Ŀ��ϸ
            SetDetialed(sheet, dv);
            this.ucClinicDayBalanceReportNew2.SetFarPoint();
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
            if (dv.Count > 0)
            {
                string fieldStr = string.Empty;
                string tagStr = string.Empty;
                string field = string.Empty;
                int Index = 0;
                for (int k = 0; k < dv.Count; k++)
                {
                    fieldStr = dv[k]["sort_id"].ToString();
                    int index = fieldStr.IndexOf('��');
                    if (index == -1)
                    {
                        Index = fieldStr.IndexOf("|");
                        tagStr = fieldStr.Substring(0, Index);
                        field = fieldStr.Substring(Index + 1);
                        SetOneCellText(sheet, tagStr, dv[k][field].ToString());
                        if (dv[k][1].ToString() == "A023")
                        {
                            SetOneCellText(sheet, "A1000", FS.FrameWork.Public.String.LowerMoneyToUpper(NConvert.ToDecimal(dv[k][field])));
                        }
                    }
                    else
                    {
                        string[] aField = fieldStr.Split('��');
                        if (aField.Length == 0) continue;
                        foreach (string s in aField)
                        {
                            Index = s.IndexOf("|");
                            tagStr = s.Substring(0, Index);
                            field = s.Substring(Index + 1);
                            SetOneCellText(sheet, tagStr, dv[k][field].ToString());
                        }
                    }
                }
            }
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
        private void SetOneCellText(FarPoint.Win.Spread.SheetView sheet, string tagStr, string strText)
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

        private string GetOneCellText(FarPoint.Win.Spread.SheetView sheet, string tagStr)
        {
            FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, tagStr);
            if (cell != null)
                return cell.Text;
            return string.Empty;
        }
        #endregion

        #region ���ñ�����Ϣ����ֹʱ�䡢����Ա)
        /// <summary>
        /// ���ñ�����Ϣ����ֹʱ�䡢����Ա)
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="aMode">0�������սᡡ1����ѯ��ʷ�սᡡ</param>
        protected virtual void SetInfo(string beginDate, string endDate, int aMode)
        {
            //��ʾ�����ս�ʱ����տ�Ա
            string strSpace = "               ";
            string strInfo = "�շ�Ա��" + currentOperator.Name + strSpace +
                "��ʼʱ�䣺" + beginDate + strSpace + "��ֹʱ�䣺" + endDate;
            if (aMode == 0)
                this.ucClinicDayBalanceReportNew1.lblReportInfo.Text = strInfo;
            else
                this.ucClinicDayBalanceReportNew2.lblReportInfo.Text = strInfo;
        }
        #endregion

        #region �����ս�ʵ��

        /// <summary>
        /// ����ս�ʵ��
        /// </summary>
        private void SetDayBalanceData()
        {
            FarPoint.Win.Spread.SheetView sheet = this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1;
            string strValue = string.Empty;

            #region ��ֹ��Ʊ��
            dayBalance = new FS.Report.Finance.FinOpb.Class.ClinicDayBalanceNew();
            dayBalance.InvoiceNO.ID = "A001";
            dayBalance.InvoiceNO.Name = "��ʼ����Ʊ�ݺ�";
            strValue = GetOneCellText(sheet, "A00101");
            dayBalance.BegionInvoiceNO = strValue;
            strValue = GetOneCellText(sheet, "A00102");
            dayBalance.EndInvoiceNo = strValue;
            //����Cell��ʾ���ݵ�Tag���ֶ�����
            dayBalance.SortID = "A00101|EXTENT_FIELD2��A00102|EXTENT_FIELD3";
            dayBalance.TypeStr = "5";
            this.SetDayBalance();
            #endregion

            #region Ʊ������
            strValue = GetOneCellText(sheet, "A002");
            this.SetOneCellDayBalance("A002", "Ʊ������", NConvert.ToDecimal(strValue), "5");
            #endregion

            #region ��ЧƱ��
            strValue = GetOneCellText(sheet, "A003");
            this.SetOneCellDayBalance("A003", "��ЧƱ��", NConvert.ToDecimal(strValue), "5");
            #endregion

            #region �˷�Ʊ��
            dayBalance = new FS.Report.Finance.FinOpb.Class.ClinicDayBalanceNew();
            dayBalance.InvoiceNO.ID = "A004";
            dayBalance.InvoiceNO.Name = "�˷�Ʊ��";
            //Ʊ����
            strValue = this.GetOneCellText(sheet, "A00401");
            dayBalance.TotCost = NConvert.ToDecimal(strValue);
            //Ʊ�ݺ�
            strValue = this.GetOneCellText(sheet, "A00402");
            dayBalance.CancelInvoiceNo = strValue;
            dayBalance.TypeStr = "5";
            dayBalance.SortID = "A00401|TOT_COST��A00402|EXTENT_FIELD5";
            this.SetDayBalance();
            #endregion

            #region ����Ʊ��
            dayBalance = new FS.Report.Finance.FinOpb.Class.ClinicDayBalanceNew();
            dayBalance.InvoiceNO.ID = "A005";
            dayBalance.InvoiceNO.Name = "����Ʊ��";
            strValue = this.GetOneCellText(sheet, "A00501");
            dayBalance.TotCost = NConvert.ToDecimal(strValue);
            strValue = this.GetOneCellText(sheet, "A00502");
            dayBalance.FalseInvoiceNo = strValue;
            dayBalance.TypeStr = "5";
            dayBalance.SortID = "A00501|TOT_COST��A00502|EXTENT_FIELD4";
            this.SetDayBalance();
            #endregion

            #region �˷ѽ��
            strValue = GetOneCellText(sheet, "A006");
            this.SetOneCellDayBalance("A006", "�˷ѽ��", NConvert.ToDecimal(strValue), "5");
            #endregion

            #region ���Ͻ��
            strValue = GetOneCellText(sheet, "A007");
            this.SetOneCellDayBalance("A007", "���Ͻ��", NConvert.ToDecimal(strValue), "5");
            #endregion

            #region ��ʱ������
            #region Ѻ����
            strValue = GetOneCellText(sheet, "A008");
            this.SetOneCellDayBalance("A008", "Ѻ����", NConvert.ToDecimal(strValue), "5");
            #endregion

            #region ��Ѻ���
            strValue = GetOneCellText(sheet, "A009");
            this.SetOneCellDayBalance("A009", "��Ѻ���", NConvert.ToDecimal(strValue), "5");
            #endregion

            #region  ������
            strValue = GetOneCellText(sheet, "A010");
            this.SetOneCellDayBalance("A010", "������", NConvert.ToDecimal(strValue), "5");
            #endregion
            #endregion

            #region  ��������
            strValue = GetOneCellText(sheet, "A011");
            this.SetOneCellDayBalance("A011", "��������", NConvert.ToDecimal(strValue), "5");
            #endregion

            #region ����ҽ��
            strValue = this.GetOneCellText(sheet, "A012");
            SetOneCellDayBalance("A012", "����ҽ��", NConvert.ToDecimal(strValue), "6");
            #endregion

            #region �����Ը�
            strValue = this.GetOneCellText(sheet, "A013");
            SetOneCellDayBalance("A013", "�����Է�", NConvert.ToDecimal(strValue), "6");
            #endregion
            #region �����˻�
            strValue = this.GetOneCellText(sheet, "A026");
            SetOneCellDayBalance("A026", "�����˻�", NConvert.ToDecimal(strValue), "6");
            #endregion

            #region �б��Ը�
            strValue = this.GetOneCellText(sheet, "A014");
            SetOneCellDayBalance("A014", "�б��Է�", NConvert.ToDecimal(strValue), "6");
            #endregion

            #region �б��˻�
            strValue = this.GetOneCellText(sheet, "A015");
            SetOneCellDayBalance("A015", "�б��˻�", NConvert.ToDecimal(strValue), "6");
            #endregion

            #region �б�ͳ��
            strValue = this.GetOneCellText(sheet, "A016");
            SetOneCellDayBalance("A016", "�б�ͳ��", NConvert.ToDecimal(strValue), "6");

            #endregion

            #region �б����
            strValue = this.GetOneCellText(sheet, "A017");
            SetOneCellDayBalance("A017", "�б����", NConvert.ToDecimal(strValue), "6");
            #endregion

            #region ʡ���Ը�
            strValue = this.GetOneCellText(sheet, "A018");
            SetOneCellDayBalance("A018", "ʡ���Է�", NConvert.ToDecimal(strValue), "6");
            #endregion

            #region ʡ���˻�
            strValue = this.GetOneCellText(sheet, "A019");
            SetOneCellDayBalance("A019", "ʡ���˻�", NConvert.ToDecimal(strValue), "6");
            #endregion

            #region ʡ��ͳ��
            strValue = this.GetOneCellText(sheet, "A020");
            SetOneCellDayBalance("A020", "ʡ��ͳ��", NConvert.ToDecimal(strValue), "6");
            #endregion

            #region ʡ�����
            strValue = this.GetOneCellText(sheet, "A021");
            SetOneCellDayBalance("A021", "ʡ�����", NConvert.ToDecimal(strValue), "6");
            #endregion

            #region ʡ����Ա
            strValue = this.GetOneCellText(sheet, "A022");
            SetOneCellDayBalance("A022", "ʡ����Ա", NConvert.ToDecimal(strValue), "6");
            #endregion

            #region �Ͻ��ֽ��
            strValue = this.GetOneCellText(sheet, "A023");
            SetOneCellDayBalance("A023", "�Ͻ��ֽ��", NConvert.ToDecimal(strValue), "6");

            #endregion

            #region �Ͻ�֧Ʊ��
            strValue = this.GetOneCellText(sheet, "A024");
            SetOneCellDayBalance("A024", "�Ͻ�֧Ʊ��", NConvert.ToDecimal(strValue), "6");
            #endregion

            #region �Ͻ�������
            strValue = this.GetOneCellText(sheet, "A025");
            SetOneCellDayBalance("A025", "�Ͻ�������", NConvert.ToDecimal(strValue), "6");
            #endregion
        }
        /// <summary>
        /// ����Ҫ�ս������
        /// </summary>
        protected virtual void SetDayBalance()
        {
            dayBalance.BeginTime = NConvert.ToDateTime(this.lastDate);
            dayBalance.EndTime = NConvert.ToDateTime(this.dayBalanceDate);
            dayBalance.Oper.ID = currentOperator.ID;
            dayBalance.Oper.Name = currentOperator.Name;
            this.alData.Add(dayBalance);
        }
        /// <summary>
        /// ���õ������ʵ�壨ֻ��һ������������
        /// </summary>
        /// <param name="InvoiceID">ͳ�ƴ������</param>
        /// <param name="InvoiceName">ͳ�ƴ�������</param>
        /// <param name="Money">���</param>
        /// <param name="typeStr">���</param>
        private void SetOneCellDayBalance(string InvoiceID, string InvoiceName, decimal Money, string typeStr)
        {
            dayBalance = new FS.Report.Finance.FinOpb.Class.ClinicDayBalanceNew();
            dayBalance.InvoiceNO.ID = InvoiceID;
            dayBalance.InvoiceNO.Name = InvoiceName;
            dayBalance.TotCost = Money;
            dayBalance.TypeStr = typeStr;
            dayBalance.SortID = InvoiceID + "|" + "TOT_COST";
            this.SetDayBalance();
        }
        #endregion

        #region �����ս�����
        /// <summary>
        /// �����ս�����
        /// </summary>
        public void DayBalance()
        {
            if (this.alData == null || this.alData.Count == 0)
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
                if (this.alData == null)
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
                //����ս�ʵ��
                this.SetDayBalanceData();
                foreach (Class.ClinicDayBalanceNew tempBalance in this.alData)
                {
                    tempBalance.BlanceNO = sequence;
                    tempBalance.Oper.OperTime = DateTime.Parse(this.operateDate);
                    intReturn = clinicDayBalance.InsertClinicDayBalance(tempBalance);
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
                PrintInfo(this.neuPanel1);
                alData.Clear();
                // �����ϴ��ս�ʱ����ʾ
                //this.ucClinicDayBalanceDateControl1.tbLastDate.Text = this.dayBalanceDate;
                //this.ucClinicDayBalanceDateControl1.dtpBalanceDate.Value = this.clinicDayBalance.GetDateTimeFromSysDateTime();
            }
        }
        #endregion

        #region �¼�
        private void ucClinicDayBalanceNew_Load(object sender, EventArgs e)
        {
            this.InitUC();
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.neuTabControl1.SelectedIndex == 0)
                QueryDayBalanceData();
            else
                this.QueryDayBalanceRecord();

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
                // �ս�
                if (this.enableBalance)
                {
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
                        this.PrintInfo(this.neuPanel1);
                        break;
                    }
                case 1:
                    {
                        MessageBox.Show(this.panelPrint.Controls.Count.ToString());
                        this.PrintInfo(this.panelPrint);

                        break;
                    }
            }

            return base.OnPrint(sender, neuObject);
        }

        protected virtual void PrintInfo(FS.FrameWork.WinForms.Controls.NeuPanel panelPrint)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(0, 0, panelPrint);
        }
        #endregion

    }
}
