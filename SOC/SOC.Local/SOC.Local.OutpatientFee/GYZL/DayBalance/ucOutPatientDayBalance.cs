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

namespace FS.SOC.Local.OutpatientFee.GYZL.DayBalance
{
    /// <summary>
    /// ��ҽ�������﷢Ʊ�ս�
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
        /// �ս����ʱ��
        /// </summary>
        string operateDate = "";

        /// <summary>
        /// �ս᷽����
        /// </summary>
        FS.SOC.Local.OutpatientFee.GYZL.Functions.OutPatientDayBalance clinicDayBalance = new FS.SOC.Local.OutpatientFee.GYZL.Functions.OutPatientDayBalance();
        // �����շ�ҵ���
        FS.HISFC.BizLogic.Fee.Outpatient outpatient = new FS.HISFC.BizLogic.Fee.Outpatient();

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        /// <summary>
        /// ͳ�ƴ���ҵ����
        /// </summary>
        FS.HISFC.BizLogic.Fee.FeeCodeStat feecodeStat = new FS.HISFC.BizLogic.Fee.FeeCodeStat();
        /// <summary>
        /// ��ӡֽ��������
        /// </summary>
        FS.HISFC.BizLogic.Manager.PageSize psManager = new FS.HISFC.BizLogic.Manager.PageSize();
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

        #region ������������

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
        [Description("�������"), Category("����")]
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
        /// <summary>
        /// �����ս�ͳ����Ŀ����ͳ�Ʒ�ʽ
        /// </summary>
        [Category("����"), Description("�����ս�ͳ����Ŀ����ͳ�Ʒ�ʽ")]
        public string StrSetting
        {
            get { return strSetting; }
            set
            {
                strSetting = value;

                try
                {
                    string[] strArr = strSetting.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    if (strArr == null)
                    {
                        lstTitle = new List<string>();
                        lstStaticType = new List<string>();
                        lstPayMode = new List<string>();
                        lstPact = new List<List<string>>();
                        lstValue = new List<decimal>();
                    }
                    else
                    {
                        int iLen = strArr.Length;
                        lstTitle = new List<string>();
                        lstStaticType = new List<string>();
                        lstPayMode = new List<string>();
                        lstPact = new List<List<string>>();
                        lstValue = new List<decimal>();

                        string strTemp = null;
                        string[] strTempArr = null;
                        for (int idx = 0; idx < iLen; idx++)
                        {
                            strTemp = strArr[idx];
                            strTempArr = strTemp.Split(new char[] { '|' });

                            lstTitle.Add(strTempArr[0]);
                            lstStaticType.Add(strTempArr[1]);
                            lstPayMode.Add(strTempArr[2]);

                            if (string.IsNullOrEmpty(strTempArr[3]) || strTempArr[3] == "ALL")
                            {
                                lstPact.Add(new List<string>());
                            }
                            else
                            {
                                List<string> lst = new List<string>();
                                lst.AddRange(strTempArr[3].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries));

                                lstPact.Add(lst);
                            }
                            
                            lstValue.Add(0);
                        }
                    }
                }
                catch (Exception objEx)
                {
                    MessageBox.Show(objEx.Message);
                }

            }

        }

        string strSetting;

        List<string> lstTitle = null;
        List<string> lstStaticType = null;
        List<string> lstPayMode = null;
        List<List<string>> lstPact = null;
        List<decimal> lstValue = null;
        #endregion


        /// <summary>
        /// ȫԺ�սỹ�ǵ����սᣬȫԺ�ս᲻���շ�Ա�ս�
        /// </summary>
        private string isAll = "1";//��0����ʾȫԺ�սᣬ��1����ʾ�����շ�Ա�ս�
        /// <summary>
        /// �ս���
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

        /// <summary>
        /// ��ʾƱ����Ϣ
        /// </summary>
        protected bool blnShowBillInfo = false;
        /// <summary>
        /// ��ʾʹ��Ʊ����Ϣ
        /// </summary>
        protected bool blnShowUsedBill = false;
        /// <summary>
        /// ��ʾ����Ʊ����Ϣ
        /// </summary>
        protected bool blnShowValiBill = false;
        /// <summary>
        /// �Ƿ���ʾƱ����Ϣ
        /// </summary>
        [Category("����"), Description("�Ƿ���ʾƱ����Ϣ")]
        public bool BlnShowBillInfo
        {
            get
            {
                return blnShowBillInfo;
            }
            set
            {
                blnShowBillInfo = value;
            }
        }
        /// <summary>
        /// �Ƿ���ʾʹ��Ʊ����Ϣ
        /// </summary>
        [Category("����"), Description("�Ƿ���ʾʹ��Ʊ����Ϣ")]
        public bool BlnShowUsedBill
        {
            get
            {
                return blnShowUsedBill;
            }
            set
            {
                blnShowUsedBill = value;
            }
        }
        /// <summary>
        /// �Ƿ���ʾƱ����Ϣ
        /// </summary>
        [Category("����"), Description("�Ƿ���ʾƱ����Ϣ")]
        public bool BlnShowValiBill
        {
            get
            {
                return blnShowValiBill;
            }
            set
            {
                blnShowValiBill = value;
            }
        }
        /// <summary>
        /// �Ͻ����
        /// </summary>
        string strCACost = string.Empty;
        /// <summary>
        /// �Ͻ����
        /// </summary>
        [Category("����"), Description("�Ͻ����")]
        public string CACost
        {
            get { return strCACost; }
            set { strCACost = value; }
        }

        string strStatClass = string.Empty;
        /// <summary>
        /// ��ʾͳ�ƴ���
        /// </summary>
        [Category("����"), Description("��ʾͳ�ƴ���")]
        public string StatClass
        {
            get { return strStatClass; }
            set { strStatClass = value; }
        }

        /// <summary>
        /// ͳ�ƴ��������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper StatHelper = new FS.FrameWork.Public.ObjectHelper();
        /// <summary>
        /// ������
        /// </summary>
        int HeadRows = 0;
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
                if (isAll == "1")
                {
                    empBalance = this.clinicDayBalance.Operator as FS.HISFC.Models.Base.Employee;
                }
                else
                {
                    if (empBalance == null)
                    {
                        empBalance = new FS.HISFC.Models.Base.Employee();
                    }

                    empBalance.ID = "T00001";
                    empBalance.Name = "T-ȫԺ";
                }
                // ����ֵ
                int intReturn = 0;

                // ��ȡ���һ���ս�ʱ��
                intReturn = this.GetLastBalanceDate();
                //�����սῪʼʱ��Ϊ�ϴν���ʱ��+1
                this.lastDate = FrameWork.Function.NConvert.ToDateTime(this.lastDate).AddSeconds(1).ToString();
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

                List<FS.HISFC.Models.Fee.FeeCodeStat> lstFeecodeStat = null;
                if (!string.IsNullOrEmpty(strStatClass))
                {
                    lstFeecodeStat = this.feecodeStat.QueryFeeStatNameByReportCode(strStatClass);
                }
                this.ucClinicDayBalanceReportNew1.lstFeecodeStat = lstFeecodeStat;
                this.ucClinicDayBalanceReportNew2.lstFeecodeStat = lstFeecodeStat;


                // ��ʼ���ӿؼ��ı���
                this.ucClinicDayBalanceDateControl1.dtLastBalance = NConvert.ToDateTime(this.lastDate);
                this.ucClinicDayBalanceReportNew1.InitUC(clinicDayBalance.Hospital.Name);
                this.ucClinicDayBalanceReportNew2.InitUC(clinicDayBalance.Hospital.Name);

                this.ucClinicDayBalanceReportNew1.Clear();
                this.ucClinicDayBalanceReportNew2.Clear();
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

                intReturn = outpatient.GetLastBalanceDate(this.empBalance, ref lastDate);
                //if (this.isAll == "0")
                //{
                //    intReturn = outpatient.GetLastBalanceDate(this.empBalance, ref lastDate);
                //}
                //else
                //{
                //    intReturn = outpatient.GetLastBalanceDate(this.currentOperator, ref lastDate);
                //}

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
            //��ʾ������Ϣ
            this.ucClinicDayBalanceReportNew1.Clear(lastDate, dayBalanceDate);

            //��ȡ�ս�������
            ds = new DataSet();

            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            string strEmpID = this.empBalance.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            clinicDayBalance.GetDayBalanceDataMZRJ(strEmpID, this.lastDate, dayBalanceDate, ref ds);


            //this.SetDetial(ds.Tables[0]);

            //����farpoint��ʽ
            //this.ucClinicDayBalanceReportNew1.SetFarPoint(lstTitle);

            //��ʾ��Ʊ��������
            //SetInvoice(this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);

            //��ʾ�������
            //this.SetMoneyValue(this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);

            //��ʾ��Ʊ��������
            SetInvoice(this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);

            this.SetDetial(ds.Tables[0], this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1);
        }

        #endregion

        #region ����Ҫ�ս�Farpoint����

        /// <summary>
        /// ������ʾ��Ŀ����
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetDetial(DataTable table,FarPoint.Win.Spread.SheetView sheet)
        {
            if (table.Rows.Count == 0) return;
            //�ս���ϸ��ʾ�ͷ�Ʊ��ʾһ�¡�
            sheet.Rows.Count += 1;
            //DataView dv = table.DefaultView.ToTable(true, new string[]{"invo_code","invo_name"}).DefaultView;
            DataSet dsFeeStatName = new DataSet ();
            clinicDayBalance.GetDayFeeStatName(ref dsFeeStatName);
            ArrayList FeeStat = new ArrayList();
            if (dsFeeStatName != null&&dsFeeStatName.Tables.Count!=0 && dsFeeStatName.Tables[0].Rows.Count >0)
            {
                FS.FrameWork.Models.NeuObject obj = null;
                foreach (DataRow rows in dsFeeStatName.Tables[0].Rows)
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = rows["fee_stat_cate"].ToString();
                    obj.Name=rows["fee_stat_name"].ToString();
                    FeeStat.Add(obj);
                }
            }
            StatHelper.ArrayObject = FeeStat;
            DataView dv = dsFeeStatName.Tables[0].DefaultView;
            //dv.Sort = "invo_code";
            //dv.RowFilter = "invo_name not in ('��ҩ��','�гɷ�','�вݷ�')";
            dv.RowFilter = "fee_stat_name not in ('��ҩ��','�гɷ�','�вݷ�')";
            if (dv == null || dv.Count <= 0) return;
            
            if (dv.Count + 7 > sheet.ColumnCount)
            {
                sheet.ColumnCount = dv.Count + 3 + 4;
                sheet.ColumnHeaderAutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            }
            //------------------head
            sheet.Rows.Count += 2;
            
            
            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 2, 0].RowSpan = 2;
            sheet.Cells[sheet.RowCount - 2, 0].Text = "����";
            sheet.Cells[sheet.RowCount - 2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            int startHejiRowIndex = sheet.Rows.Count + 1;

            sheet.Cells[sheet.RowCount - 2, 1].ColumnSpan = 4;
            sheet.Cells[sheet.RowCount - 2, 1].Text = "ҩƷ����";
            sheet.Cells[sheet.RowCount - 2, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 1].Text = "��ҩ��";
            sheet.Cells[sheet.RowCount - 1, 1].Tag = "��ҩ��";
            sheet.Cells[sheet.RowCount - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 2].Text = "�гɷ�";
            sheet.Cells[sheet.RowCount - 1, 2].Tag = "�гɷ�";
            sheet.Cells[sheet.RowCount - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 3].Text = "�вݷ�";
            sheet.Cells[sheet.RowCount - 1, 3].Tag = "�вݷ�";
            sheet.Cells[sheet.RowCount - 1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 4].Text = "�ϼ�";
            sheet.Cells[sheet.RowCount - 1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 2, 5].ColumnSpan = dv.Count + 1;
            sheet.Cells[sheet.RowCount - 2, 5].Text = "ҽ������";
            sheet.Cells[sheet.RowCount - 2, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            for (int i = 0; i < dv.Count; i++)
            {
                sheet.Cells[sheet.RowCount - 1, 5 + i].Text = dv[i]["fee_stat_name"].ToString();
                sheet.Cells[sheet.RowCount - 1, 5 + i].Tag = dv[i]["fee_stat_name"].ToString();
                sheet.Cells[sheet.RowCount - 1, 5 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sheet.Cells[sheet.RowCount - 1, 5 + i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].Text = "�ϼ�";
            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].Text = "���ϼ�";
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].RowSpan = 2;
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            this.HeadRows = sheet.RowCount - 1;//������chengym
            //---------------------head

            //-----------data start
            DataView dvData = table.DefaultView;
            string dataRowFilter = "fee_stat_name in ('��ҩ��','�гɷ�','�вݷ�') and  dept_name = {0}";
            Hashtable hs = new Hashtable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string strDptCode = table.Rows[i]["dept_code"].ToString();
                string strDptName = table.Rows[i]["dept_name"].ToString();
                string strFeeStat = table.Rows[i]["fee_stat_name"].ToString();
                decimal strCost = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][4].ToString());
         
                if (hs.Contains(strDptName))
                {
                    FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, strFeeStat);
                    sheet.Cells[sheet.RowCount - 1, cell.Column.Index].Text =  strCost.ToString("0.00");
                }
                else
                {
                    sheet.Rows.Count += 1;
                    hs.Add(strDptName,"");
                    sheet.Cells[sheet.RowCount - 1, 0].Text = strDptName;
                    sheet.Cells[sheet.RowCount - 1, 0].Tag = strDptCode;
                    sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, strFeeStat);
                    sheet.Cells[sheet.RowCount - 1, cell.Column.Index].Text =  strCost.ToString("0.00");            
                }
            }
            //----------data end
            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            //���0
            for (int i = startHejiRowIndex - 1; i < sheet.RowCount; i++)
            {    
                for (int j = 1; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells[i, j].CellType = cellType;
                    if (sheet.Cells[i, j].Text == null || sheet.Cells[i, j].Text == "" || sheet.Cells[i, j].Text == "0") 
                    {
                        sheet.Cells[i, j].Text = "0.00";
                    }
                }
            }

            //ҩƷ����ϼƣ�ҽ������ϼ�
            for (int i = startHejiRowIndex-1; i < sheet.Rows.Count; i++)
            {
                 sheet.Cells[i, 4].Formula = string.Format("sum(B{0}:D{0})",(i+1).ToString());

                 sheet.Cells[i, 5 + dv.Count].Formula = string.Format("sum(F{0}:{1}{0})", (i + 1).ToString(), (char)(69 + dv.Count));

                 sheet.Cells[i, 6 + dv.Count].Formula = string.Format("sum(E{0},{1}{0})", (i + 1).ToString(), (char)(70 + dv.Count));
            }

            //�кϼ�
            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].Text = "�ϼ�:";
            sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;  
            for(int i= 1;i< dv.Count + 7;i++)
            {
                string strFormula = "sum(" + (char)(65 + i) + startHejiRowIndex.ToString() + ":"
                                           + (char)(65 + i) + (sheet.RowCount - 1).ToString() 
                                    + ")";
                sheet.Cells[sheet.RowCount - 1, i].CellType = cellType;
                sheet.Cells[sheet.RowCount - 1, i].Formula = strFormula;
                sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
        }


        /// <summary>
        /// ������ʾ���ս���Ŀ����
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetRePrintDetial(DataTable table, FarPoint.Win.Spread.SheetView sheet)
        {
            if (table.Rows.Count == 0) return;
            //�ս���ϸ��ʾ�ͷ�Ʊ��ʾһ�¡�
            sheet.Rows.Count += 1;
            //DataView dv = table.DefaultView.ToTable(true, new string[]{"invo_code","invo_name"}).DefaultView;
            DataSet dsFeeStatName = new DataSet();
            clinicDayBalance.GetDayFeeStatName(ref dsFeeStatName);
            DataView dv = dsFeeStatName.Tables[0].DefaultView;
            //dv.Sort = "invo_code";
            //dv.RowFilter = "invo_name not in ('��ҩ��','�гɷ�','�вݷ�')";
            dv.RowFilter = "fee_stat_name not in ('��ҩ��','�гɷ�','�вݷ�')";
            if (dv == null || dv.Count <= 0) return;

            if (dv.Count + 7 > sheet.ColumnCount)
            {
                sheet.ColumnCount = dv.Count + 3 + 4;
                sheet.ColumnHeaderAutoText = FarPoint.Win.Spread.HeaderAutoText.Blank;
            }
            //------------------head
            sheet.Rows.Count += 2;

            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 2, 0].RowSpan = 2;
            sheet.Cells[sheet.RowCount - 2, 0].Text = "����";
            sheet.Cells[sheet.RowCount - 2, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            int startHejiRowIndex = sheet.Rows.Count + 1;

            sheet.Cells[sheet.RowCount - 2, 1].ColumnSpan = 4;
            sheet.Cells[sheet.RowCount - 2, 1].Text = "ҩƷ����";
            sheet.Cells[sheet.RowCount - 2, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 1].Text = "��ҩ��";
            sheet.Cells[sheet.RowCount - 1, 1].Tag = "��ҩ��";
            sheet.Cells[sheet.RowCount - 1, 1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 2].Text = "�гɷ�";
            sheet.Cells[sheet.RowCount - 1, 2].Tag = "�гɷ�";
            sheet.Cells[sheet.RowCount - 1, 2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 3].Text = "�вݷ�";
            sheet.Cells[sheet.RowCount - 1, 3].Tag = "�вݷ�";
            sheet.Cells[sheet.RowCount - 1, 3].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 3].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 1, 4].Text = "�ϼ�";
            sheet.Cells[sheet.RowCount - 1, 4].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 4].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 2, 5].ColumnSpan = dv.Count + 1;
            sheet.Cells[sheet.RowCount - 2, 5].Text = "ҽ������";
            sheet.Cells[sheet.RowCount - 2, 5].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 5].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            for (int i = 0; i < dv.Count; i++)
            {
                sheet.Cells[sheet.RowCount - 1, 5 + i].Text = dv[i]["fee_stat_name"].ToString();
                sheet.Cells[sheet.RowCount - 1, 5 + i].Tag = dv[i]["fee_stat_name"].ToString();
                sheet.Cells[sheet.RowCount - 1, 5 + i].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sheet.Cells[sheet.RowCount - 1, 5 + i].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }

            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].Text = "�ϼ�";
            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 5 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].Text = "���ϼ�";
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].RowSpan = 2;
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 2, 6 + dv.Count].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;

            //---------------------head

            //-----------data start
            DataView dvData = table.DefaultView;
            string dataRowFilter = "fee_stat_name in ('��ҩ��','�гɷ�','�вݷ�') and  dept_name = {0}";
            Hashtable hs = new Hashtable();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string strDptName = table.Rows[i]["dept_name"].ToString();
                string strFeeStat = table.Rows[i]["fee_stat_name"].ToString();
                decimal strCost = FS.FrameWork.Function.NConvert.ToDecimal(table.Rows[i][3].ToString());

                if (hs.Contains(strDptName))
                {
                    FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, strFeeStat);
                    sheet.Cells[sheet.RowCount - 1, cell.Column.Index].Text = strCost.ToString("0.00");
                }
                else
                {
                    sheet.Rows.Count += 1;
                    hs.Add(strDptName, "");
                    sheet.Cells[sheet.RowCount - 1, 0].Text = strDptName;
                    sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                    sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
                    FarPoint.Win.Spread.Cell cell = sheet.GetCellFromTag(null, strFeeStat);
                    sheet.Cells[sheet.RowCount - 1, cell.Column.Index].Text = strCost.ToString("0.00");
                }
            }
            //----------data end
            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            //���0
            for (int i = startHejiRowIndex - 1; i < sheet.RowCount; i++)
            {
                for (int j = 1; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells[i, j].CellType = cellType;
                    if (sheet.Cells[i, j].Text == null || sheet.Cells[i, j].Text == "" || sheet.Cells[i, j].Text == "0")
                    {
                        sheet.Cells[i, j].Text = "0.00";
                    }
                }
            }
            //ҩƷ����ϼƣ�ҽ������ϼ�
            for (int i = startHejiRowIndex - 1; i < sheet.Rows.Count; i++)
            {
                sheet.Cells[i, 4].Formula = string.Format("sum(B{0}:D{0})", (i + 1).ToString());

                sheet.Cells[i, 5 + dv.Count].Formula = string.Format("sum(F{0}:{1}{0})", (i + 1).ToString(), (char)(69 + dv.Count));

                sheet.Cells[i, 6 + dv.Count].Formula = string.Format("sum(E{0},{1}{0})", (i + 1).ToString(), (char)(70 + dv.Count));
            }

            //�кϼ�
            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].Text = "�ϼ�:";
            sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
            sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            for (int i = 1; i < dv.Count + 7; i++)
            {
                string strFormula = "sum(" + (char)(65 + i) + startHejiRowIndex.ToString() + ":"
                                           + (char)(65 + i) + (sheet.RowCount - 1).ToString()
                                    + ")";
                sheet.Cells[sheet.RowCount - 1, i].Formula = strFormula;
                sheet.Cells[sheet.RowCount - 1, 0].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Center;
                sheet.Cells[sheet.RowCount - 1, 0].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            }
        }

        /// <summary>
        /// ����ս�ʵ�嵽�б�
        /// </summary>
        private void AddDayBalanceToArray()
        {
            dayBalance.BeginTime = NConvert.ToDateTime(this.lastDate);
            dayBalance.EndTime = NConvert.ToDateTime(this.dayBalanceDate);

            dayBalance.Oper.ID = this.empBalance.ID;
            dayBalance.Oper.Name = this.empBalance.Name;

            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            //if (this.isAll == "0")
            //{
            //    dayBalance.Oper.ID = this.empBalance.ID;
            //    dayBalance.Oper.Name = this.empBalance.Name;
            //}
            //else
            //{
            //    dayBalance.Oper.ID = currentOperator.ID;
            //    dayBalance.Oper.Name = currentOperator.Name;
            //}
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
            string strEmpID = this.empBalance.ID;
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

            int totInvoiceCount = dv.Count;
            //��Ч��Ʊ��
            dv.RowFilter = "cancel_flag='1'";//"trans_type = '1'";
            int totValidInvoiceCount = dv.Count;

           //�˷ѡ�����Ʊ�ݺ� 
            dv.RowFilter = "cancel_flag in('0','2','3') and trans_type='2'";
            int totUnValidInvoiceCount = dv.Count;
            string InvoiceStr = GetInvoiceStr(dv);

            //��Ч��Ʊ����ֹƱ�ݺ�
            dv.RowFilter = "cancel_flag='1'";
            string invoiceStartAndEnd = GetInvoiceStartAndEnd(dv);
            string[] sTemp = invoiceStartAndEnd.Split('��');
            sheet.RowCount = sTemp.Length;
           

            DataSet ds1 = new DataSet();
            int resultValue1 = clinicDayBalance.GetDayInvoiceDataGYZL(strEmpID, this.lastDate, dayBalanceDate, ref ds1);
            if (resultValue1 == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }
            DataTable table1 = ds1.Tables[0];
            if (table1.Rows.Count == 0)
            {
                return;
            }
            DataView dv1 = table1.DefaultView;
            //string strRowFilter = "print_invoiceno in ({0})";
            for (int i = 0; i < sTemp.Length; i++)
            {
                dv1.RowFilter = string.Format("print_invoiceno in ({0})", GetRowFilterInvoiceno(sTemp[i]));
                if (dv1 != null && dv1.Count > 0)
                {
                    sheet.Cells[i, 0].Text = sTemp[i];
                    sheet.Cells[i, 1].Text = GetPrintInvoicenoCount(sTemp[i]).ToString();
                    sheet.Cells[i, 2].Text = dv1.ToTable().Compute("sum(����)", "").ToString();//����
                    sheet.Cells[i, 3].Text = dv1.ToTable().Compute("sum(���дſ�)", "").ToString();//���дſ�
                    sheet.Cells[i, 4].Text = dv1.ToTable().Compute("sum(���дſ�)", "").ToString();//���дſ�
                    sheet.Cells[i, 5].Text = dv1.ToTable().Compute("sum(�ֽ�)", "").ToString();//�ֽ�
                    sheet.Cells[i, 6].Text = dv1.ToTable().Compute("sum(���)", "").ToString();//���
                    sheet.Cells[i, 7].Text = dv1.ToTable().Compute("sum(֧Ʊ)", "").ToString();//֧Ʊ
                    sheet.Cells[i, 8].Text = dv1.ToTable().Compute("sum(�˿�)", "").ToString();//�˿�
                    sheet.Cells[i, 9].Text = dv1.ToTable().Compute("sum(�ϼ�)", "").ToString();//�ϼ�
                }
            }

            sheet.Rows.Count += 1;
            dv1.RowFilter = ""; 
            sheet.Cells[sheet.RowCount - 1, 0].Text = "�ܼ�(��Ʊ��)";
            sheet.Cells[sheet.RowCount - 1, 1].Text = totValidInvoiceCount.ToString();
            sheet.Cells[sheet.RowCount - 1, 2].Text = dv1.Table.Compute("sum(����)", "").ToString();//����
            sheet.Cells[sheet.RowCount - 1, 3].Text = dv1.Table.Compute("sum(���дſ�)", "").ToString();//���дſ�
            sheet.Cells[sheet.RowCount - 1, 4].Text = dv1.Table.Compute("sum(���дſ�)", "").ToString();//���дſ�
            sheet.Cells[sheet.RowCount - 1, 5].Text = dv1.Table.Compute("sum(�ֽ�)", "").ToString();//�ֽ�
            sheet.Cells[sheet.RowCount - 1, 6].Text = dv1.Table.Compute("sum(���)", "").ToString();//���
            sheet.Cells[sheet.RowCount - 1, 7].Text = dv1.Table.Compute("sum(֧Ʊ)", "").ToString();//֧Ʊ
            sheet.Cells[sheet.RowCount - 1, 8].Text = dv1.Table.Compute("sum(�˿�)", "").ToString();//�˿�
            sheet.Cells[sheet.RowCount - 1, 9].Text = dv1.Table.Compute("sum(�ϼ�)", "").ToString();//�ϼ�

            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            //���0
            for (int i = 0; i < sheet.RowCount; i++)
            {
                for (int j = 2; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells[i, j].CellType = cellType;
                    if (sheet.Cells[i, j].Text == null || sheet.Cells[i, j].Text == ""
                        || sheet.Cells[i, j].Text == "0")
                    {
                        sheet.Cells[i, j].Text = "0.00";
                    }
                }
            }


            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].ColumnSpan = 4;
            //string strMsg = "���з�Ʊ" + totInvoiceCount.ToString() + "�ţ�������Ч��Ʊ"
            //    + totValidInvoiceCount.ToString() + "�ţ����Ϸ�Ʊ��" + totUnValidInvoiceCount.ToString()+ "��";
            //���ϵĴ���ͬ���ķ�Ʊ�Ų�ͬ�ļ�¼ ����Ӧ�ü�����������
            string strMsg = "���з�Ʊ" + (totValidInvoiceCount + totUnValidInvoiceCount).ToString() + "�ţ�������Ч��Ʊ"
                + totValidInvoiceCount.ToString() + "�ţ����Ϸ�Ʊ��" + totUnValidInvoiceCount.ToString() + "��";
            sheet.Cells[sheet.RowCount - 1, 0].Text = strMsg;

            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].ColumnSpan = 10;
            sheet.Cells[sheet.RowCount - 1, 0].Text = "���Ϸ�Ʊ����Ϊ��" + InvoiceStr;

            return;
        }

        /// <summary>
        /// ��ѯ����ʾ��Ʊ���� -- 
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        /// <param name="table"></param>
        protected virtual void SetInvoiceReprint(FarPoint.Win.Spread.SheetView sheet)
        {
            //��ȡ��Ʊ����
            DataSet ds = new DataSet();
            string strEmpID = this.empBalance.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            int resultValue = clinicDayBalance.GetDayInvoiceDataNewReprint(strEmpID, this.lastDate, dayBalanceDate, ref ds);
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
            int totInvoiceCount = dv.Count;
            //��Ч��Ʊ��
            dv.RowFilter = "cancel_flag='1'";//"trans_type = '1'";
            int totValidInvoiceCount = dv.Count;

            //�˷ѡ�����Ʊ�ݺ� 
            dv.RowFilter = "cancel_flag in('0','2','3') and trans_type='2'";
            int totUnValidInvoiceCount = dv.Count;
            string InvoiceStr = GetInvoiceStr(dv);

            //��Ч��Ʊ����ֹƱ�ݺ�
            dv.RowFilter = "cancel_flag='1'";
            string invoiceStartAndEnd = GetInvoiceStartAndEnd(dv);
            string[] sTemp = invoiceStartAndEnd.Split('��');
            sheet.RowCount = sTemp.Length;


            DataSet ds1 = new DataSet();
            int resultValue1 = clinicDayBalance.GetDayInvoiceYJDataGYZL(strEmpID, this.lastDate, dayBalanceDate, ref ds1);
            if (resultValue1 == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }
            DataTable table1 = ds1.Tables[0];
            if (table1.Rows.Count == 0)
            {
                return;
            }
            DataView dv1 = table1.DefaultView;
            //string strRowFilter = "print_invoiceno in ({0})";
            for (int i = 0; i < sTemp.Length; i++)
            {
                dv1.RowFilter = string.Format("print_invoiceno in ({0})", GetRowFilterInvoiceno(sTemp[i]));
                if (dv1 != null && dv1.Count > 0)
                {
                    sheet.Cells[i, 0].Text = sTemp[i];
                    sheet.Cells[i, 1].Text = GetPrintInvoicenoCount(sTemp[i]).ToString();
                    sheet.Cells[i, 2].Text = dv1.ToTable().Compute("sum(�ϼ�)", "").ToString();//�ϼ�
                    sheet.Cells[i, 3].Text = dv1.ToTable().Compute("sum(����)", "").ToString();//����
                    sheet.Cells[i, 4].Text = dv1.ToTable().Compute("sum(���дſ�)", "").ToString();//���дſ�
                    sheet.Cells[i, 5].Text = dv1.ToTable().Compute("sum(���дſ�)", "").ToString();//���дſ�
                    sheet.Cells[i, 6].Text = dv1.ToTable().Compute("sum(�ֽ�)", "").ToString();//�ֽ�
                    sheet.Cells[i, 7].Text = dv1.ToTable().Compute("sum(���)", "").ToString();//���
                    sheet.Cells[i, 8].Text = dv1.ToTable().Compute("sum(֧Ʊ)", "").ToString();//֧Ʊ
                    sheet.Cells[i, 9].Text = dv1.ToTable().Compute("sum(�˿�)", "").ToString();//�˿�
                }
            }

            sheet.Rows.Count += 1;
            dv1.RowFilter = "";
            sheet.Cells[sheet.RowCount - 1, 0].Text = "�ܼ�(��Ʊ��)";
            sheet.Cells[sheet.RowCount - 1, 1].Text = totValidInvoiceCount.ToString();
            sheet.Cells[sheet.RowCount - 1, 2].Text = dv1.Table.Compute("sum(�ϼ�)", "").ToString();//�ϼ�
            sheet.Cells[sheet.RowCount - 1, 3].Text = dv1.Table.Compute("sum(����)", "").ToString();//����
            sheet.Cells[sheet.RowCount - 1, 4].Text = dv1.Table.Compute("sum(���дſ�)", "").ToString();//���дſ�
            sheet.Cells[sheet.RowCount - 1, 5].Text = dv1.Table.Compute("sum(���дſ�)", "").ToString();//���дſ�
            sheet.Cells[sheet.RowCount - 1, 6].Text = dv1.Table.Compute("sum(�ֽ�)", "").ToString();//�ֽ�
            sheet.Cells[sheet.RowCount - 1, 7].Text = dv1.Table.Compute("sum(���)", "").ToString();//���
            sheet.Cells[sheet.RowCount - 1, 8].Text = dv1.Table.Compute("sum(֧Ʊ)", "").ToString();//֧Ʊ
            sheet.Cells[sheet.RowCount - 1, 9].Text = dv1.Table.Compute("sum(�˿�)", "").ToString();//�˿�

            FarPoint.Win.Spread.CellType.NumberCellType cellType = new FarPoint.Win.Spread.CellType.NumberCellType();
            //���0
            for (int i = 0; i < sheet.RowCount; i++)
            {
                for (int j = 2; j < sheet.ColumnCount; j++)
                {
                    sheet.Cells[i, j].CellType = cellType;
                    if (sheet.Cells[i, j].Text == null || sheet.Cells[i, j].Text == ""
                        || sheet.Cells[i, j].Text == "0")
                    {
                        sheet.Cells[i, j].Text = "0.00";
                    }
                }
            }

            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].ColumnSpan = 4;
            string strMsg = "���з�Ʊ" + totInvoiceCount.ToString() + "�ţ�������Ч��Ʊ"
                + totValidInvoiceCount.ToString() + "�ţ����Ϸ�Ʊ��" + totUnValidInvoiceCount.ToString() + "��";
            sheet.Cells[sheet.RowCount - 1, 0].Text = strMsg;

            sheet.Rows.Count += 1;
            sheet.Cells[sheet.RowCount - 1, 0].ColumnSpan = 10;
            sheet.Cells[sheet.RowCount - 1, 0].Text = "���Ϸ�Ʊ����Ϊ��" + InvoiceStr;

            return;
        }

        /// <summary>
        /// ������ϡ��˷�Ʊ�ݺ�
        /// </summary>
        /// <param name="dv">DataView</param>
        /// <param name="aMod">���ϻ����˷�1������ 0���˷�</param>
        /// <returns></returns>
        private string GetInvoiceStr(DataView dv)
        {
            if (dv != null && dv.Count != 0)
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
                        sb.Append(GetPrintInvoiceno(dv[i]) + "|");

                    }
                }
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        #region �����ʼ����ֹƱ�ݺ�

        private string GetInvoiceStartAndEnd(DataView dv)
        {
            if (dv != null && dv.Count != 0)
            {
                StringBuilder sb = new StringBuilder();
                int count = dv.Count - 1;
                string minStr = GetPrintInvoiceno(dv[0]);
                string maxStr = GetPrintInvoiceno(dv[0]);
                for (int i = 0; i < count ; i++)
                    for (int j = i + 1; j <= count; j++)
                    {
                        long froInt = Convert.ToInt64(GetPrintInvoiceno(dv[i]));
                        long nxtInt = Convert.ToInt64(GetPrintInvoiceno(dv[j]));
                        long chaInt = nxtInt - froInt;
                        if (chaInt > 1)
                        {
                            maxStr = GetPrintInvoiceno(dv[i]);
                            if (maxStr.Equals(minStr))
                            {
                                sb.Append(minStr + "��");
                            }
                            else
                            {
                                sb.Append(minStr + "��" + maxStr + "��");
                            }
                            minStr = GetPrintInvoiceno(dv[j]);
                            break;
                        }
                        else
                        {
                            break;
                        }

                    }
                maxStr = GetPrintInvoiceno(dv[count]);
                if (minStr == maxStr)
                {
                    sb.Append(maxStr);
                }
                else
                {
                    sb.Append(minStr + "��" + maxStr);
                }
                return sb.ToString();
            }
            else
            {
                return "";
            }
        }

        private string GetPrintInvoiceno(DataRowView drv)
        {
            string str = drv["print_invoiceno"].ToString();
            str = str.TrimStart(new char[] { '0' });
            str = str.PadLeft(8, '0');

            return str;
        }

        private int GetPrintInvoicenoCount(string invoicenoStartEnd)
        {
            int count = 1;
            if (invoicenoStartEnd.Contains("��"))
            {
                string[] invoiceStartEnd = invoicenoStartEnd.Split('��');
                long startInt = Convert.ToInt64(invoiceStartEnd[0].TrimStart(new char[] { '0' }).PadLeft(12, '0'));
                long endInt = Convert.ToInt64(invoiceStartEnd[1].TrimStart(new char[] { '0' }).PadLeft(12, '0'));
                count = int.Parse((endInt - startInt+1).ToString());
            }
            return count;
        }


        private string GetRowFilterInvoiceno(string invoicenoStartEnd)
        {
            StringBuilder sb = new StringBuilder();
            if (invoicenoStartEnd.Contains("��"))
            {
                string[] invoiceStartEnd = invoicenoStartEnd.Split('��');
                long startInt = Convert.ToInt64(invoiceStartEnd[0].TrimStart(new char[] { '0' }).PadLeft(12, '0'));
                long endInt = Convert.ToInt64(invoiceStartEnd[1].TrimStart(new char[] { '0' }).PadLeft(12, '0'));
                int count = int.Parse((endInt - startInt).ToString());
                for (int i = 0; i <= count; i++)
                {
                    sb.Append("'");
                    sb.Append(startInt.ToString().PadLeft(12,'0'));
                    sb.Append("'");
                    if(i!=count)
                    sb.Append(",");
                    startInt = startInt + 1;
                }
            }
            else
            {
                sb.Append("'");
                sb.Append(invoicenoStartEnd.ToString().PadLeft(12,'0'));
                sb.Append("'");
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
            string employeeID = this.empBalance.ID;
            if (this.isAll == "0")
            {
                employeeID = "ALL";
            }

            // ִ�зǳ��������� 

            resultValue = clinicDayBalance.GetDayBalanceCancelMoney(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                money1 += money;
            }
            //���Ͻ��
            resultValue = clinicDayBalance.GetDayBalanceFalseMoney(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
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
        /// ������ʾ�������
        /// {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
        /// </summary>
        protected virtual void SetMoneyValueReprint(FarPoint.Win.Spread.SheetView sheet)
        {
            decimal money = 0;
            int resultValue;
            int resultValue1;


            #region ���Ͻ��

            decimal money1 = 0;

            //�˷ѽ��
            // {4348FDC9-6C18-47f4-9DA1-60864DF1EF3E}
            string employeeID = this.empBalance.ID;
            if (this.isAll == "0")
            {
                employeeID = "ALL";
            }

            // ִ�зǳ��������� 

            resultValue = clinicDayBalance.GetDayBalanceCancelMoneyReprint(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                money1 += money;
            }
            //���Ͻ��
            resultValue = clinicDayBalance.GetDayBalanceFalseMoneyReprint(employeeID, this.lastDate, dayBalanceDate, ref money);
            if (resultValue != -1)
            {
                money1 += money;
            }

            SetOneCellText(sheet, EnumCellName.A004, money1.ToString());

            #endregion

            DataSet ds = new DataSet();
            DataSet ds2 = new DataSet();

            //֧����ʽ���
            resultValue = clinicDayBalance.GetDayBalancePayTypeMoneyReprint(employeeID, this.lastDate, dayBalanceDate, ref ds);
            if (resultValue == -1)
            {
                MessageBox.Show(clinicDayBalance.Err);
                return;
            }


            resultValue = clinicDayBalance.QueryDayBalancePactPubMoneyReprint(employeeID, this.lastDate, dayBalanceDate, ref ds2);
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
        }

        /// <summary>
        /// ��ʾ���ѽ������
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetPublicValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
        }

        //{0EA3CF1A-2F03-46c3-83AE-1543B00BBDDB}
        /// <summary>
        /// ��ʾ����������
        /// </summary>
        /// <param name="sheet">SheetView</param>
        /// <param name="ds">DataSet</param>
        protected virtual void SetRebateValue(FarPoint.Win.Spread.SheetView sheet, DataSet ds)
        {
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

            if (lstValue != null && lstValue.Count > 0)
            {
                for (int idx = 0; idx < lstValue.Count; idx++)
                {
                    lstValue[idx] = 0;
                }
            }

            decimal protectCost = 0;

            #region ͳ�Ƹ�����ֵ
            int iCount = lstTitle.Count;
            for (int idx = 0; idx < iCount; idx++)
            {
                if (lstStaticType[idx] == "pay")
                {
                    foreach (DataRow dr in dtPayMode.Rows)
                    {
                        pact = dr[0].ToString();
                        payMode = dr[1].ToString();
                        detailCost = FS.FrameWork.Function.NConvert.ToDecimal(dr[2]);

                        if (lstPayMode[idx].Contains(payMode))
                        {
                            if (lstPact[idx] == null || lstPact[idx].Count <= 0)
                            {
                                lstValue[idx] += detailCost;
                            }
                            else if (lstPact[idx].Contains(pact))
                            {
                                lstValue[idx] += detailCost;
                            }
                        }
                    }
                }
                else if (lstStaticType[idx] == "pub")
                {
                    // ҽ������
                    foreach (DataRow drTemp in dtPactFSSI.Rows)
                    {
                        pact = drTemp[0].ToString();
                        protectCost = FS.FrameWork.Function.NConvert.ToDecimal(drTemp[1]);

                        if (lstPact[idx].Contains(pact))
                        {
                            lstValue[idx] += protectCost;
                        }
                    }
                }
            }

            // �Ͻ����
            if (!string.IsNullOrEmpty(strCACost))
            {
                if (lstTitle.Contains(strCACost))
                {
                    decimal decCA = lstValue[lstTitle.IndexOf(strCACost)];
                    sheet.Cells[4, 1].Text = decCA.ToString();
                    sheet.Cells[4, 3].Text = FS.FrameWork.Public.String.LowerMoneyToUpper(lstValue[lstTitle.IndexOf(strCACost)]);
                }
            }

            #endregion

            int iCellCount = 8;
            string strTemp = "";
            int iTitleIdx = 0;

            for (int iRow = 0; iRow < sheet.RowCount; iRow++)
            {
                for (int iCell = 0; iCell < iCellCount; iCell++)
                {
                    if (sheet.Cells[iRow, iCell].Tag == null)
                    {
                        continue;
                    }
                    strTemp = sheet.Cells[iRow, iCell].Tag.ToString();
                    if (string.IsNullOrEmpty(strTemp) || !lstTitle.Contains(strTemp))
                    {
                        continue;
                    }

                    iTitleIdx = lstTitle.IndexOf(strTemp);

                    sheet.Cells[iRow, iCell].Text = lstValue[iTitleIdx].ToString();

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

            intReturn = this.clinicDayBalance.GetBalanceRecord(this.empBalance, dtFrom, dtTo, ref balanceRecord);
            //if (this.isAll == "0")
            //{
            //    intReturn = this.clinicDayBalance.GetBalanceRecord(this.empBalance, dtFrom, dtTo, ref balanceRecord);
            //}
            //else
            //{
            //    intReturn = this.clinicDayBalance.GetBalanceRecord(this.currentOperator, dtFrom, dtTo, ref balanceRecord);
            //}
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
            string strEmpID = this.empBalance.ID;
            if (this.isAll == "0")
            {
                strEmpID = "ALL";
            }
            //�ս��ش�������ķ���
            //clinicDayBalance.GetDayBalanceDataMZRJ(strEmpID, this.lastDate, this.dayBalanceDate, ref ds);
            clinicDayBalance.GetDayBalanceDataMZRJReprint(strEmpID, this.lastDate, this.dayBalanceDate, ref ds);

            //this.SetRePrintDetial(ds.Tables[0]);

            //����farpoint��ʽ
            //this.ucClinicDayBalanceReportNew2.SetFarPoint(lstTitle);

            //��ʾ��Ʊ��������
            //�ս��ش�������ķ���
            //SetInvoice(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);
            //SetInvoiceReprint(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);

            //��ʾ�������
            //this.SetMoneyValue(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);
            //this.SetMoneyValueReprint(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);

            //��ʾ��Ʊ��������
            SetInvoiceReprint(this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);

            this.SetRePrintDetial(ds.Tables[0],this.ucClinicDayBalanceReportNew2.neuSpread1_Sheet1);

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
            this.ucClinicDayBalanceReportNew2.SetFarPoint(lstTitle);
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
            #region �жϸ���ʼʱ���Ƿ��Ѿ��ս���

            string dtLastTemp = "";
            int returnValue = this.outpatient.GetLastBalanceDate(this.empBalance, ref dtLastTemp);
            if (returnValue == -1)
            {
                MessageBox.Show("��ȡ�ϴ��ս�ʱ��ʧ�ܣ����ܽ����ս������");
                return;
            }
            else if (returnValue == 0)
            {
                //û���ս���������ü����սᣬɶҲ������
            }
            else
            {
                //�ս��
                if (!this.lastDate.Equals(FS.FrameWork.Function.NConvert.ToDateTime(dtLastTemp).AddSeconds(1).ToString()))
                {
                    MessageBox.Show("�Ѿ������սᣬ���˳��������½���");
                    return;
                }
            }

            #endregion 

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

                string strEmpID = this.empBalance.ID;
                string strOperID = this.empBalance.ID;
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

                //this.ucClinicDayBalanceDateControl1.dtpBalanceDate.Value = this.ucClinicDayBalanceDateControl1.dtLastBalance;
                //this.QueryDayBalanceData();
            }
        }
        #endregion

        #region �¼�
        private void ucOutPatientDayBalance_Load(object sender, EventArgs e)
        {
            this.InitUC();
            this.ucClinicDayBalanceReportNew1.enentFarpiontClick += new ucOutPatientDayBalanceReport.FarpiontClick(ucClinicDayBalanceReportNew1_enentFarpiontClick);
        }
        /// <summary>
        /// ˫������ϸ�¼�
        /// </summary>
        void ucClinicDayBalanceReportNew1_enentFarpiontClick()
        {
            FarPoint.Win.Spread.SheetView sheet = this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet1;
            if (sheet.Cells[sheet.ActiveRowIndex, 0].Tag == null)
            {
                MessageBox.Show("δ��ȡ����һ�еĿ��ұ��룬����ϵ��Ϣ�ƣ�");
                return;
            }
            string deptCode = sheet.Cells[sheet.ActiveRowIndex, 0].Tag.ToString();//���ұ���
            string dtBegin = this.lastDate;
            string dtEnd = this.dayBalanceDate;
            string StatCode = StatHelper.GetID(sheet.Cells[this.HeadRows,sheet.ActiveColumnIndex].Text);
            string strEmpID = this.empBalance.ID;
            DataSet ds = new DataSet();

            clinicDayBalance.GetDayBalanceDataMZRJDetail(strEmpID, dtBegin,dtEnd,deptCode,StatCode ,ref ds);
            this.ucClinicDayBalanceReportNew1.neuSpread1.ActiveSheetIndex = 1;
            FarPoint.Win.Spread.SheetView sheet1 = this.ucClinicDayBalanceReportNew1.neuSpread1_Sheet2;
            sheet1.DataSource = ds;
            sheet1.Rows.Add(sheet1.RowCount, 1);
            sheet1.Cells[sheet1.RowCount - 1, 0].Text = "�ϼƣ�";
            sheet1.Cells[sheet1.RowCount - 1, 7].Formula = "SUM(H1:" + "H" + (sheet1.RowCount - 1).ToString() + ")";
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
            toolBarService.AddToolButton("ȡ���ս�", "ȡ�����һ���ս���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H�ϲ�ȡ��, true, false, null);

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
            else if (e.ClickedItem.Text == "ȡ���ս�")
            {
                if (System.Windows.Forms.MessageBox.Show("�Ƿ�ȡ�����һ���ս���Ϣ��", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    return;
                }

                UnDoDayBalance();
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        #region ȡ�����һ���ս�
        /// <summary>
        /// ȡ�����һ���ս�
        /// </summary>
        public void UnDoDayBalance()
        {
            string strOperID = this.empBalance.ID;
            FS.FrameWork.Models.NeuObject balance = null;

            int iRes = this.clinicDayBalance.QueryLastBalanceRecord(strOperID, out balance);
            if (iRes <= 0)
            {
                MessageBox.Show(this.clinicDayBalance.Err);
                return;
            }

            if (balance.User02 == "1")
            {
                MessageBox.Show("���һ���ս���Ϣ����ˣ�������ȡ����");

                return;
            }

            iRes = this.clinicDayBalance.UnDoOperDayBalance(balance.ID, balance.Name, balance.Memo);

            if (iRes <= 0)
            {
                MessageBox.Show("����ʧ�ܣ�" + this.clinicDayBalance.Err);
            }
            else
            {
                MessageBox.Show("�����ɹ���");

                alData.Clear();
                this.InitUC();
            }
        }

        #endregion

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

            // ��ӡֽ������
            FS.HISFC.Models.Base.PageSize ps = null;
            ps = psManager.GetPageSize("MZRJ");

            if (ps == null)
            {
                ps = new FS.HISFC.Models.Base.PageSize("MZRJ", 787, 550);
                ps.Top = 0;
                ps.Left = 0;

            }

            print.SetPageSize(ps);

            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(0, 0, panelPrint);

        }

        public override int Export(object sender, object neuObject)
        {
            this.ExportInfo();
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        private void ExportInfo()
        {

            bool tr = false;
            string fileName = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "excel|*.xls";
            saveFile.Title = "������Excel";

            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(saveFile.FileName))
                {
                    fileName = saveFile.FileName;
                    tr = this.ucClinicDayBalanceReportNew1.neuSpread1.SaveExcel(fileName, FarPoint.Excel.ExcelSaveFlags.SaveCustomColumnHeaders, new FarPoint.Excel.ExcelWarningList());
                }
                else
                {
                    MessageBox.Show("�ļ����ֲ���Ϊ��!");
                    return;
                }

                if (tr)
                {
                    MessageBox.Show("�����ɹ�!");
                }
                else
                {
                    MessageBox.Show("����ʧ��!");
                }

            }
        }

        #endregion

    }
}
