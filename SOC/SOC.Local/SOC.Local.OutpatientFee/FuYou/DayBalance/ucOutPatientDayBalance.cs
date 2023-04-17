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

namespace FS.SOC.Local.OutpatientFee.FuYou.DayBalance
{
    /// <summary>
    /// �������﷢Ʊ�ս�
    /// {0F9CA67C-2A6A-413f-B1EE-8FC44CD1317A}
    /// </summary>
    public partial class ucOutPatientDayBalance : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOutPatientDayBalance()
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

        /// <summary>
        /// �ս᷽����
        /// </summary>
        Function.OutPatientDayBalance clinicDayBalance = new Function.OutPatientDayBalance();
        // �����շ�ҵ���
        FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

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
        public List<Models.ClinicDayBalanceNew> alData = new List<Models.ClinicDayBalanceNew>();

        private Models.ClinicDayBalanceNew dayBalance = null;

        #region ����

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime beginDate = DateTime.MinValue;

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime endDate = DateTime.MinValue;
      

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
                // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
                if (empBalance == null)
                {
                    empBalance = new FS.HISFC.Models.Base.Employee();
                    empBalance.ID = "T00001"; 
                    empBalance.Name = "T-ȫԺ";
                }

                // ����ֵ
                int intReturn = 0;

                // ��ȡ��ǰ����Ա
                currentOperator = this.clinicDayBalance.Operator;

                // ��ȡ���һ���ս�ʱ��
                intReturn = this.GetLastBalanceDate();
                //�����սῪʼʱ��Ϊ�ϴν���ʱ��+1
                this.lastDate = NConvert.ToDateTime(this.lastDate).AddSeconds(1).ToString();
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
                this.ucClinicDayBalanceReportNew1.InitUC(clinicDayBalance.Hospital.Name + reportTitle);
                this.ucClinicDayBalanceReportNew2.InitUC(clinicDayBalance.Hospital.Name + reportTitle);
                this.ucClinicDayBalanceReportNew1.SetDetailName();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
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
                // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
                if (this.isAll == "0")
                {
                    intReturn = outpatient.GetLastBalanceDate(this.empBalance, ref lastDate);
                }
                else
                {
                    intReturn = outpatient.GetLastBalanceDate(this.currentOperator, ref lastDate);
                }

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

            intReturn = this.ucClinicDayBalanceDateControl1.GetBalanceDate(ref dayBalanceDate);
            if (intReturn == -1)
            {
                return;
            }
            // ��ȡ���һ���ս�ʱ��
            intReturn = this.GetLastBalanceDate();
            //�����սῪʼʱ��Ϊ�ϴν���ʱ��+1
            this.lastDate = NConvert.ToDateTime(this.lastDate).AddSeconds(1).ToString();
            if (intReturn == -1)
            {
                MessageBox.Show("��ȡ�ϴ��ս�ʱ��ʧ�ܣ����ܽ����ս������");
                return;
            }
            else
            {
                this.ucClinicDayBalanceDateControl1.tbLastDate.Text = this.lastDate.ToString();
            }
            //��ʾ������Ϣ
            this.ucClinicDayBalanceReportNew1.Clear(lastDate, dayBalanceDate);

            //��ȡ�ս�������
            ds = new DataSet();

            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            string strEmpID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            clinicDayBalance.GetDayBalanceDataMZRJ(strEmpID, this.lastDate, dayBalanceDate, ref ds);

            this.SetDetial(ds.Tables[0]);

            //����farpoint��ʽ
            this.ucClinicDayBalanceReportNew1.SetFarPoint();

            //��ʾ��Ʊ��������
            SetInvoice(this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);

            //��ʾ�������
            this.SetMoneyValue(this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);
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
            {
                //��������ݣ�Ȼ���ʼ��3��
                ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count = 0;
                ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows.Count = 4;
                ucClinicDayBalanceReportNew1.SetDetailName();
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

                //decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text);
                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text);

                //ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex + 1].Text = decTemp.ToString("0.00");
                ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text = decTemp.ToString("0.00");
            }
            ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[3, 1].Text = countMoney.ToString("0.00");
            ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[3, 3].Text =FS.FrameWork.Public.String.LowerMoneyToUpper(countMoney);        

        }


        /// <summary>
        /// ������ʾ���ս���Ŀ����
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetRePrintDetial(DataTable table)
        {
            if (table.Rows.Count == 0) return;
            //�������
            if (ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count > 0)
            {
                //��������ݣ�Ȼ���ʼ��3��
                ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count = 0;
                ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count = 4;
                ucClinicDayBalanceReportNew2.SetDetailName();
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

                decTemp += FS.FrameWork.Function.NConvert.ToDecimal(ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text);

                ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[rowIndex, colIndex].Text = decTemp.ToString("0.00");

            }
            ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[3, 1].Text = countMoney.ToString("0.00");
            ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[3, 3].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(countMoney);

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
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoice(FarPoint.Win.Spread.SheetView sheet)
        {
            //��ȡ��Ʊ����
            DataSet ds = new DataSet();
            string strEmpID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            int resultValue = clinicDayBalance.GetDayInvoiceDataNew(strEmpID, this.lastDate, dayBalanceDate, ref ds);
            if (resultValue == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }

            DataTable table = ds.Tables[0];
            if (table.Rows.Count == 0)
            {
                return;
            }

            DataView dv = table.DefaultView;

            //��ЧƱ��
            dv.RowFilter = "cancel_flag='1'";
            this.SetOneCellText(sheet, EnumCellName.A002, dv.Count.ToString());

            //����ֻ��ʾ���Ϻ���

            dv.RowFilter = "cancel_flag in('0','2','3') and trans_type='2'";
            this.SetOneCellText(sheet, EnumCellName.A003, dv.Count.ToString());

            //��ֹƱ�ݺ�
            this.SetOneCellText(sheet, EnumCellName.A005, GetInvoiceStartAndEnd(table));
            FarPoint.Win.Spread.Cell cell1 = sheet.GetCellFromTag(null, EnumCellName.A005);
            string[] sTemp = cell1.Text.Split('��', '��');
            cell1.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;

            //�˷ѡ�����Ʊ�ݺ� 
            dv.RowFilter = "cancel_flag in('0','2','3') and trans_type='2'";
            string InvoiceStr = GetInvoiceStr(dv);
            this.SetOneCellText(sheet, EnumCellName.A006, InvoiceStr);
            FarPoint.Win.Spread.Cell cell2 = sheet.GetCellFromTag(null, EnumCellName.A006);
            sTemp = cell2.Text.Split('|');
            cell2.Row.Height = (float)Math.Ceiling(sTemp.Length / 5.0) * 18;

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
            for (int i = 0; i <= count - 1; i++)
                for (int j = i + 1; j <= count; j++)
                {
                    long froInt = Convert.ToInt64(dt.Rows[i][0].ToString());
                    long nxtInt = Convert.ToInt64(dt.Rows[j][0].ToString());
                    long chaInt = nxtInt - froInt;
                    if (chaInt > 1)
                    {
                        maxStr = dt.Rows[i][0].ToString();
                        if (maxStr.Equals(minStr))
                        {
                            sb.Append(minStr + "��");
                        }
                        else
                        {
                            sb.Append(minStr + "��" + maxStr + "��");
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

            if (maxStr.Equals(minStr))
            {
                sb.Append(minStr);
            }
            else
            {
                sb.Append(minStr + "��" + maxStr);
            }

            return sb.ToString();

        }

        #endregion

        /// <summary>
        /// ������ʾ�������
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        protected virtual void SetMoneyValue(FarPoint.Win.Spread.SheetView sheet)
        {
            decimal money = 0;
            int resultValue;
            int resultValue1;


            #region ���Ͻ��

            decimal money1 = 0;

            //�˷ѽ��
            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            string employeeID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                employeeID = "ALL";
            }

            resultValue = clinicDayBalance.GetDayBalanceCancelMoney(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                //SetOneCellText(sheet, "ddd", money.ToString());
                money1 += money;
            }
            //���Ͻ��
            resultValue = clinicDayBalance.GetDayBalanceFalseMoney(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                //SetOneCellText(sheet, "A007", money.ToString());
                money1 += money;
            }

            SetOneCellText(sheet, EnumCellName.A004, money1.ToString());

            #endregion

            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();

            //֧����ʽ���
            resultValue = clinicDayBalance.GetDayBalancePayTypeMoney(employeeID, this.lastDate, dayBalanceDate, ref ds);
            if (resultValue == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }
            ////������ҽ�����
            //resultValue1 = clinicDayBalance.GetDayBalanceDerateAndProtectMoney(employeeID, this.lastDate, dayBalanceDate, ref ds1);
            //if (resultValue1 == -1)
            //{
            //    MessageBox.Show(clinicDayBalance.Err);
            //    return;
            //}

            resultValue = clinicDayBalance.QueryDayBalancePactPubMoney(employeeID, this.lastDate, dayBalanceDate, ref ds2);
            if (resultValue == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }

            DataTable dt1 = ds.Tables[0];
            DataTable dt2 = ds2.Tables[0];

            SetPayTypeValue(sheet, dt1, dt2);

            return;
      
        }

        /// <summary>
        /// ��ʾҽ���������
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetProtectValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
        //    DataTable dt = ds.Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        switch (dr["pact_code"].ToString())
        //        {
        //            case "4"://����
        //                {
        //                    //aaaaaaaaaaaa
        //                  //  SetOneCellText(sheet, "A012", dr["pub_cost"].ToString()); //����ҽ��
        //                    this.d����ҽ�� = NConvert.ToDecimal(dr["pub_cost"]);
        //                   // SetOneCellText(sheet, "A013", dr["pay_cost"].ToString());//�����Ը�
        //                    break;
        //                }
        //            case "2"://�л�
        //                {
        //                    //SetOneCellText(sheet, "A014", dr["own_cost"].ToString());//�б��Ը�

        //                    //SetOneCellText(sheet, "A015", dr["pay_cost"].ToString());//�б��˻�

        //                    //SetOneCellText(sheet, "A016", dr["pub_cost"].ToString());//�б�ͳ��   

        //                    //SetOneCellText(sheet, "A017", dr["over_cost"].ToString());//�б����
        //                    this.d�б��˻� = NConvert.ToDecimal(dr["pay_cost"]);
        //                    this.d�б�ͳ�� = NConvert.ToDecimal(dr["pub_cost"]);
        //                    this.d�б���� = NConvert.ToDecimal(dr["over_cost"]);

        //                    break;
        //                }
        //            case "3"://ʡ��
        //                {
        //                    //SetOneCellText(sheet, "A018", dr["own_cost"].ToString());//ʡ���Ը�

        //                    //SetOneCellText(sheet, "A019", dr["pay_cost"].ToString());//ʡ���˻�

        //                    //SetOneCellText(sheet, "A020", dr["pub_cost"].ToString());//ʡ��ͳ��

        //                    //SetOneCellText(sheet, "A021", dr["over_cost"].ToString());//ʡ�����

        //                    //SetOneCellText(sheet, "A022", dr["official_cost"].ToString());//ʡ����Ա
        //                    this.dʡ���˻� = NConvert.ToDecimal(dr["pay_cost"]);
        //                    this.dʡ��ͳ�� = NConvert.ToDecimal(dr["pub_cost"]);
        //                    this.dʡ����� = NConvert.ToDecimal(dr["over_cost"]);
        //                    this.dʡ������Ա = NConvert.ToDecimal(dr["official_cost"]);

        //                    break;
        //                }
        //        }
        //    }
        }

        /// <summary>
        /// ��ʾ���ѽ������
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetPublicValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
        //    DataTable dt = ds.Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        //SetOneCellText(sheet, "A012", dr["pub_cost"].ToString()); //����ҽ��
        //        //this.d����ҽ�� = NConvert.ToDecimal(dr["pub_cost"]);
        //        //SetOneCellText(sheet, "A013", dr["own_cost"].ToString());//�����Ը�  
        //        //SetOneCellText(sheet, "A026", dr["pay_cost"].ToString());//�����˻�
        //        this.d�����˻� = NConvert.ToDecimal(dr["pay_cost"]);
        //        //SetOneCellText(sheet, "A010", dr["rebate_cost"].ToString());
        //        //this.d������ = NConvert.ToDecimal(dr["rebate_cost"]);
        //    }
        }

        //{0EA3CF1A-2F03-46c3-83AE-1543B00BBDDB}
        /// <summary>
        /// ��ʾ����������
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetRebateValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
        //    DataTable dt = ds.Tables[0];
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        SetOneCellText(sheet, EnumCellName.A010, dr["rebate_cost"].ToString());
        //        this.d������ = NConvert.ToDecimal(dr["rebate_cost"]);
        //    }
        }

        /// <summary>
        /// ��֧��������ʾ���
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="dtPayMode">֧����ʽ����</param>
        /// <param name="dtPactEco">�������</param>
        /// <param name="dtPactFSSI">�ض��������</param>
        protected virtual void SetPayTypeValue(FarPoint.Win.Spread.SheetView sheet, DataTable dtPayMode, DataTable dtPactFSSI)
        {
            string payMode = string.Empty;
            string pact = string.Empty;
            decimal detailCost = 0;
            //������
            decimal derateCost = 0;
            //ҽ�����
            decimal protectCost = 0;

            //�ֽ�
            decimal CAcost = 0;
            //ˢ��
            decimal CDcost = 0;
            //�˻�
            decimal PScost = 0;
            #region ����
            ////��������
            //decimal TSMZ = 0;
            ////���˼���
            //decimal LRJM = 0;
            ////����ҽ��
            //decimal HZYL = 0;
            ////����Ա
            //decimal GWY = 0;
            ////�Ż�����
            //decimal YHYT = 0;
            ////����ҽ��
            //decimal JMYB = 0;
            ////��Ժ����
            //decimal BYJM = 0;
            #endregion
            //ְ������ҽ��
            decimal ZGYL = 0;
            //�������ҽ��
            decimal JMYL = 0;
            //��Լ��λ
            decimal TYDW = 0;
            //ҽ���Ż�
            decimal YLYH = 0;
            //�ض�����
            decimal TDMZ = 0;

            #region ֧����ʽ
            foreach (DataRow dr in dtPayMode.Rows)
            {
                pact = dr[0].ToString();
                payMode = dr[1].ToString();
                detailCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[2]);

                if (payMode == "CA")
                {
                    CAcost += detailCost;
                }
                else if (payMode == "DB" || payMode == "CD")
                {
                    CDcost += detailCost;
                }
                //�˻�֧����ʽ�ȵ����ֽ�֧��
                else if (payMode == "YS")
                {
                    PScost += detailCost;
                }

                #region ����
                //// ����֧��
                //if (payMode == "RC")
                //{
                //    //if (pact == "2" || pact == "9" || pact == "10" || pact == "11")
                //    //{
                //    //    // �������ﱨ��
                //    //    TSMZ += protectCost;
                //    //}

                //    //if (pact == "3" || pact == "4" || pact == "5" || pact == "6")
                //    //{
                //    //    // ����ҽ������
                //    //    JMYB += protectCost;
                //    //}

                //    if (pact == "6" || pact == "7" || pact == "10")
                //    {
                //        // �������
                //        LRJM += detailCost;
                //    }

                //    if (pact == "4")
                //    {
                //        // ����ҽ�� ����
                //        HZYL += detailCost;
                //    }

                //    if (pact == "5" || pact == "8" || pact == "11")
                //    {
                //        // �Ÿ�����
                //        YHYT += detailCost;
                //    }

                //    if (pact == "9")
                //    {
                //        // ���ݸɲ� -- ��Ժ����
                //        BYJM += detailCost;
                //    }
                //}
                #endregion

            }
            #endregion

            #region �������
            //foreach (DataRow dr1 in dtPactEco.Rows)
            //{
            //    pact = dr1[0].ToString();
            //    derateCost = FS.FrameWork.Function.NConvert.ToDecimal(dr1[1]);
            //    protectCost = FS.FrameWork.Function.NConvert.ToDecimal(dr1[2]);

            //    if (pact == "2" || pact == "9" || pact == "10" || pact == "11")
            //    {
            //        // �������ﱨ��
            //        TSMZ += protectCost;
            //    }

            //    if (pact == "3" || pact == "4" || pact == "5" || pact == "6")
            //    {
            //        // ����ҽ������
            //        JMYB += protectCost;
            //    }

            //    if (pact == "6" || pact == "7" || pact == "10")
            //    {
            //        // �������
            //        LRJM += derateCost;
            //    }

            //    if (pact == "4")
            //    {
            //        // ����ҽ�� ����
            //        HZYL += derateCost;
            //    }

            //    if (pact == "5" || pact == "8" || pact == "11")
            //    {
            //        // �Ÿ�����
            //        YHYT += derateCost;
            //    }

            //    if (pact == "9")
            //    {
            //        // ���ݸɲ� -- ��Ժ����
            //        BYJM += derateCost;
            //    }



            //    //if (pact == "2" )
            //    //{
            //    //    TSMZ += protectCost;
            //    //}
            //    //else if (pact == "3")
            //    //{
            //    //    JMYB += protectCost;
            //    //}
            //    //else if (pact == "4")
            //    //{
            //    //    HZYL += derateCost;
            //    //    JMYB += protectCost;
            //    //}
            //    //else if (pact == "5")
            //    //{
            //    //    YHYT += derateCost;
            //    //    JMYB += protectCost;
            //    //}
            //    //else if (pact == "6")
            //    //{
            //    //    LRJM += derateCost;
            //    //    JMYB += protectCost;
            //    //}
            //    //else if (pact == "7")
            //    //{
            //    //    LRJM += derateCost;
            //    //    JMYB += protectCost;
            //    //}
            //    //else if (pact == "8")
            //    //{
            //    //    YHYT += derateCost;
            //    //    JMYB += protectCost;
            //    //}
            //}
            #endregion

            #region �ض�����/�ӷ����ﱨ��

            foreach (DataRow drTemp in dtPactFSSI.Rows)
            {
                pact = drTemp[0].ToString();
                protectCost = FS.FrameWork.Function.NConvert.ToDecimal(drTemp[1]);

                if (pact == "2")
                {
                    // ְ��ҽ�Ʊ���
                    ZGYL += protectCost;
                }
                else if (pact == "3")
                {
                    // ����ҽ������
                    JMYL += protectCost;
                }
                else if (pact == "6")
                {
                    // ҽ���Ż�
                    YLYH += protectCost;
                }
                else if (pact == "7")
                {
                    // �ض�����
                    TDMZ += protectCost;
                }
                else if (pact == "8" || pact == "9" || pact == "10")
                {
                    // ��Լ��λ
                    TYDW += protectCost;
                }
                else
                {
                    // ҽ���Ż�
                    YLYH += protectCost;
                }
            }

            #endregion
            //������սḳֵ���ս���棬����ǲ�ѯ�ش���ֵ�ڲ�ѯ�ش����
            if (sheet == this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1)
            {
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 3].Text = CDcost.ToString("0.00");
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 5].Text = CAcost.ToString("0.00");
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[4, 1].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(CAcost);
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 1].Text = ZGYL.ToString("0.00");
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 3].Text = JMYL.ToString("0.00");
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 5].Text = TYDW.ToString("0.00");
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[8, 7].Text = YLYH.ToString("0.00");
                #region �Ȳ�ͳ�ƹ���Ա����
                //this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[6, 6].Text = "";
                //this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[6, 7].Text = "";
                #endregion
                this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Cells[9, 1].Text = TDMZ.ToString("0.00");
                //this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows[4].Visible = false;
                //this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1.Rows[5].Visible = false;
            }
            else
            {
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 3].Text = CDcost.ToString("0.00");
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 5].Text = CAcost.ToString("0.00");
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[4, 1].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(CAcost);
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 1].Text = ZGYL.ToString("0.00");
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 3].Text = JMYL.ToString("0.00");
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 5].Text = TYDW.ToString("0.00");
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[8, 7].Text = YLYH.ToString("0.00");
                #region �Ȳ�ͳ�ƹ���Ա����
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[6, 6].Text = "";
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[6, 7].Text = "";
                #endregion
                this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[9, 1].Text = TDMZ.ToString("0.00");
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[7, 3].Text = JMYB.ToString("0.00");
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Cells[7, 5].Text = BYJM.ToString("0.00");
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows[6].Visible = false;
                //this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows[7].Visible = false;
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

            ////�������
            //int count = this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Count;
            //if (count > 0)
            //{
            //    this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1.Rows.Remove(0, count);
            //}

            // ��ȡ��ѯʱ��
            intReturn = this.ucReprintDateControl1.GetInputDateTime(ref dtFrom, ref dtTo);
            if (intReturn == -1)
            {
                return;
            }

            // ��ȡ��ѯ���
            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            if (this.isAll == "0")
            {
                intReturn = this.clinicDayBalance.GetBalanceRecord(this.empBalance, dtFrom, dtTo, ref balanceRecord);
            }
            else
            {
                intReturn = this.clinicDayBalance.GetBalanceRecord(this.currentOperator, dtFrom, dtTo, ref balanceRecord);
            }
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
            else if (balanceRecord.Count < 1)
            {
                MessageBox.Show("��ʱ�����û��Ҫ���ҵ����ݣ�");
                return;
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
            //ͨ����ѯ��ʱ�սῪʼʱ��ͽ���ʱ����ʵ���ش�
            this.lastDate = begin.ToString();
            this.dayBalanceDate = end.ToString();
            //��ʾ������Ϣ
            this.ucClinicDayBalanceReportNew2.Clear(lastDate, dayBalanceDate);
            DataSet ds = new DataSet();
            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            string strEmpID = this.currentOperator.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            clinicDayBalance.GetDayBalanceDataMZRJ(strEmpID, this.lastDate, this.dayBalanceDate, ref ds);

            this.SetRePrintDetial(ds.Tables[0]);

            //����farpoint��ʽ
            this.ucClinicDayBalanceReportNew2.SetFarPoint();

            //��ʾ��Ʊ��������
            SetInvoice(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);

            //��ʾ�������
            this.SetMoneyValue(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);
            ds.Dispose();
            ////�����ս�����
            //DataSet ds = new DataSet();
            //intReturn = clinicDayBalance.GetDayBalanceRecord(sequence, ref ds);
            //if (intReturn == -1)
            //{
            //    MessageBox.Show(clinicDayBalance.Err);
            //    return;
            //}
            //if (ds.Tables.Count == 0 || ds == null || ds.Tables[0].Rows.Count == 0)
            //{
            //    MessageBox.Show("��ʱ�����û��Ҫ���ҵ����ݣ�");
            //    return;
            //}
            //SetOldFarPointData(ds.Tables[0]);
            //ds.Dispose();

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

        /// <summary>
        /// ���õ������ʵ�壨ֻ��һ������������
        /// </summary>
        /// <param name="InvoiceID">ͳ�ƴ������</param>
        /// <param name="InvoiceName">ͳ�ƴ�������</param>
        /// <param name="Money">���</param>
        /// <param name="typeStr">���</param>
        private void SetOneCellDayBalance(string InvoiceID, string InvoiceName, decimal Money, string typeStr)
        {
            dayBalance = new Models.ClinicDayBalanceNew();
            dayBalance.InvoiceNO.ID = InvoiceID;
            dayBalance.InvoiceNO.Name = InvoiceName;
            dayBalance.TotCost = Money;
            dayBalance.TypeStr = typeStr;
            dayBalance.SortID = InvoiceID + "|" + "TOT_COST";
            AddDayBalanceToArray();
        }

        #endregion

        #region �����ս�����
        /// <summary>
        /// �����ս�����
        /// </summary>
        public void DayBalance()
        {
            //if (this.alData == null || this.alData.Count == 0)
            //{
            //    return;
            //}

            if (MessageBox.Show("�Ƿ�����ս�,�ս�����ݽ����ָܻ�?", "�����տ�Ա�ɿ��ձ�", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
               

                // �ȴ�����
                FS.FrameWork.WinForms.Forms.frmWait waitForm = new FS.FrameWork.WinForms.Forms.frmWait();
               
               

                if (this.alData == null)
                {
                    return;
                }
                // �����ȴ�����
                waitForm.Tip = "���ڽ����ս�";
                waitForm.Show();

                string strEmpID = this.currentOperator.ID;
                string strOperID = this.currentOperator.ID;
                if (this.isAll == "0")
                {
                    strEmpID = "ALL";
                    strOperID = this.empBalance.ID;
                }
               
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                int iRes = outpatient.DealOperDayBalance(strEmpID, strOperID, this.lastDate, this.dayBalanceDate);
                if (iRes < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    waitForm.Hide();
                    MessageBox.Show("�ս����");
                    return;
                }

                FS.FrameWork.Management.PublicTrans.Commit();
                waitForm.Hide();
                MessageBox.Show("�ս�ɹ����");
                PrintInfo(this.neuPanel1);
                alData.Clear();
                this.InitUC();
                this.ucClinicDayBalanceDateControl1.dtpBalanceDate.Value = this.ucClinicDayBalanceDateControl1.dtLastBalance;
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
                if (this.neuTabControl1.SelectedIndex == 1)
                {
                    MessageBox.Show("�ش���治�����ս�!");
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
                        this.PrintInfo(this.neuPanel1);
                        break;
                    }
                case 1:
                    {
                        //{C1A4AEEB-6A47-4208-B6EE-6634B00840FD}
                        //MessageBox.Show(this.panelPrint.Controls.Count.ToString());
                        this.PrintInfo(this.panelPrint);

                        break;
                    }
            }

            return base.OnPrint(sender, neuObject);
        }

        protected virtual void PrintInfo(FS.FrameWork.WinForms.Controls.NeuPanel panelPrint)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //{C1A4AEEB-6A47-4208-B6EE-6634B00840FD
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(0, 0, panelPrint);
        }

        #endregion
    }
}
