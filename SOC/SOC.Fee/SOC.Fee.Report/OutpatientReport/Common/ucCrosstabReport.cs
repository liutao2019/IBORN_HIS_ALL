using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SOC.Fee.Report.OutpatientReport.Common
{
    /// <summary>
    /// ucCrosstabReport<br></br>
    /// <Font color='#FF1111'>[��������: ͨ��DataSetʵ�ֽ��汨��]</Font><br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2009-4-13]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		/>
    /// </summary>
    public partial class ucCrosstabReport : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucCrosstabReport()
        {
            InitializeComponent();
        }
        #endregion ���캯��

        #region ����

        #region ˽�б���

        /// <summary>
        /// �Ƿ�ʹ�ÿ��Ҳ�ѯ����
        /// </summary>
        private bool isHaveDept = false;

        /// <summary>
        /// ��������
        /// </summary>
        //private Neusoft.HISFC.Object.Base.EnumDepartmentType deptType;

        /// <summary>
        /// ����Ȩ�޴��룬ͨ��Ȩ�޼��ؿ��ң�Ϊ��ʱ�������п���
        /// </summary>
        private string privClass3Code = "";

        /// <summary>
        /// ����
        /// </summary>
        private string title = "";

        /// <summary>
        /// sql���id
        /// </summary>
        private string sqlId = "";

        /// <summary>
        /// sql���id2
        /// ���ڰ����Ҳ�ѯ
        /// </summary>
        private string sqlId2 = "";

        /// <summary>
        /// �Ƿ�����ϼ���
        /// </summary>
        private bool haveSum = true;

        /// <summary>
        /// �Ƿ�����ϼ���
        /// </summary>
        private bool haveRowSum = true;

        /// <summary>
        /// �Ƿ񽫿�ֵתΪ��
        /// </summary>
        private bool replaceNullToZero = true;
        /// <summary>
        /// ���ò�ѯ����
        /// </summary>
        private string queryDeptcode = "";

        /// <summary>
        /// ʱ���ʽ
        /// </summary>
        private string dataTiemFromat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Ĭ�ϲ�ѯ��ʱ�䷶Χ����
        /// </summary>
        private int queryDays = 1;
        /// <summary>
        /// ��������
        /// </summary>
        private int frozen = 0;

        #endregion ˽�б���

        #region ��������
        #endregion ��������

        #region ��������

        #endregion ��������

        #endregion ����

        #region ����

        /// <summary>
        /// �Ƿ�ʹ�ÿ��Ҳ�ѯ����
        /// </summary>
        [Category("��ѯ����"), Description("�Ƿ���ӿ��Ҳ�ѯ����,true���,false�����"), DefaultValue("false")]
        public bool IsHaveDept
        {
            get
            {
                return isHaveDept;
            }
            set
            {
                isHaveDept = value;
                this.lbDept.Visible = value;
                this.cmbDept.Visible = value;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        //[Category("��ѯ����"), Description("��������")]
        //public Neusoft.HISFC.Object.Base.EnumDepartmentType DeptType
        //{
        //    get
        //    {
        //        return deptType;
        //    }
        //    set
        //    {
        //        deptType = value;
        //    }
        //}

        /// <summary>
        /// ����Ȩ�޴��룬ͨ��Ȩ�޼��ؿ��ң�Ϊ��ʱ�������п���
        /// </summary>
        [Category("��ѯ����"), Description("����Ȩ�޴��룬ͨ��Ȩ�޼��ؿ��ң�Ϊ��ʱ�������п���")]
        public string PrivClass3Code
        {
            get
            {
                return privClass3Code;
            }
            set
            {
                privClass3Code = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        [Category("��ѯ����"), Description("����")]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        /// <summary>
        /// sql���id
        /// </summary>
        [Category("��ѯ����"), Description("sql���id")]
        public string SqlId
        {
            get
            {
                return sqlId;
            }
            set
            {
                sqlId = value;
            }
        }

        /// <summary>
        /// sql���id
        /// </summary>
        [Category("����������"), Description("�����Ƿ񶳽�")]
        public int Frozen
        {
            get
            {
                return frozen;
            }
            set
            {
                frozen = value;
            }
        }
        /// <summary>
        /// �Ƿ���Ӻϼ�
        /// </summary>
        [Category("��ѯ����"), Description("�Ƿ���Ӻϼ�,true���,false�����"), DefaultValue("true")]
        public bool HaveSum
        {
            get
            {
                return haveSum;
            }
            set
            {
                haveSum = value;
            }
        }

        /// <summary>
        /// �Ƿ�����ϼ���
        /// </summary>
        [Category("��ѯ����"), Description("�Ƿ�����кϼ�,true���,false�����"), DefaultValue("true")]
        public bool HaveRowSum
        {
            get
            {
                return haveRowSum;
            }
            set
            {
                haveRowSum = value;
            }
        }

        /// <summary>
        /// �Ƿ񽫿�ֵתΪ��
        /// </summary>
        [Category("��ѯ����"), Description("�Ƿ񽫿�ֵתΪ��,trueת��,false��ת��"), DefaultValue("true")]
        public bool ReplaceNullToZero
        {
            get
            {
                return replaceNullToZero;
            }
            set
            {
                replaceNullToZero = value;
            }
        }

        /// <summary>
        /// ʱ���ʽ
        /// </summary>
        [Category("��ѯ����"), Description("ʱ���ʽ��Ĭ�ϣ�yyyy-MM-dd HH:mm:ss"), DefaultValue("yyyy-MM-dd HH:mm:ss")]
        public string DataTiemFromat
        {
            get
            {
                return dataTiemFromat;
            }
            set
            {
                dataTiemFromat = value;
            }
        }

        /// <summary>
        /// Ĭ�ϲ�ѯ��ʱ�䷶Χ����
        /// </summary>
        [Category("��ѯ����"), Description("Ĭ�ϲ�ѯ��ʱ�䷶Χ����λ���죬Ĭ��1��"), DefaultValue("1")]
        public int QueryDays
        {
            get
            {
                return queryDays;
            }
            set
            {
                queryDays = value;
            }
        }

        private string dataFromat = "";

        [Category("��ѯ����"), Description("������ʾ��ʽ"), DefaultValue("")]
        public string DataFromat
        {
            get
            {
                return dataFromat;
            }
            set
            {
                dataFromat = value;
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        [Category("���ò�ѯ����"), Description("���ò�ѯ����")]
        public string QueryDeptcode
        {
            get
            {
                return queryDeptcode; ;
            }
            set
            {
                queryDeptcode = value;
            }
        }
        #endregion ����

        #region ����

        #region ��Դ�ͷ�
        #endregion ��Դ�ͷ�

        #region ��¡
        #endregion ��¡

        #region ˽�з���

        private void Init()
        {
            //���Ҳ�ѯ����
            if (this.isHaveDept)
            {
                if (string.IsNullOrEmpty(this.privClass3Code.Trim()))
                {
                   Neusoft.HISFC.BizLogic.Manager.Department interMgr = new Neusoft.HISFC.BizLogic.Manager.Department();
                    ArrayList alDept = interMgr.GetDeptmentAll();
                    this.cmbDept.AddItems(alDept);
                }
                else
                {
                    Neusoft.HISFC.BizLogic.Manager.UserPowerDetailManager managerIntegrate = new Neusoft.HISFC.BizLogic.Manager.UserPowerDetailManager();
                    List<   Neusoft.FrameWork.Models.NeuObject> alPrivDept = managerIntegrate.QueryUserPriv(Neusoft.FrameWork.Management.Connection.Operator.ID, this.privClass3Code.Trim());
                    if (alPrivDept != null)
                    {
                        this.cmbDept.AddItems(new ArrayList(alPrivDept.ToArray()));
                    }
                }
            }
            this.lbDept.Visible = this.isHaveDept;
            this.cmbDept.Visible = this.isHaveDept;
            this.cmbDept.Tag = this.queryDeptcode;
            //��ͷ
            this.lbTitle.Text = this.title;
            //hn ����
            this.neuSpread1_Sheet1.SetColumnAllowAutoSort(1, true);
            //ʱ���ʽ
            this.dtpFromDate.CustomFormat = this.dataTiemFromat;
            this.dtpEndDate.CustomFormat = this.dataTiemFromat;
            this.dtpFromDate.Value = new DateTime(this.dtpFromDate.Value.Year, this.dtpFromDate.Value.Month, this.dtpFromDate.Value.Day, 0, 0, 0);
            this.dtpEndDate.Value = new DateTime(this.dtpEndDate.Value.Year, this.dtpEndDate.Value.Month, this.dtpEndDate.Value.Day, 23, 59, 59);
            this.dtpFromDate.Value = this.dtpEndDate.Value.AddDays(-this.queryDays).AddSeconds(1);
            //��ӳ�ʼʱ��
            if (this.queryDays < 1)
            {
                this.dtpFromDate.Value = new DateTime(this.dtpFromDate.Value.Year, dtpFromDate.Value.Month, 01);
            }
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            try
            {
                   Neusoft.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ե�");
                Application.DoEvents();
                //ִ��sql��䣬��ȡDataTable
                DataTable dt = this.GetDataTableBySql();
                if (dt == null)
                {
                       Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                //����sql��ѯ�����DataTable���ɽ�����DataTable
                DataTable dtCross = this.GetCrossDataTable(dt);
                if (dtCross == null)
                {
                       Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                if (dt.Columns.Count != 4)
                {
                    //��Ӻϼ�
                    this.ComputeSum(dtCross);
                }
                else
                {
                    // ��ҽ����ҽ��ͳ�Ʋ�ѯ
                    this.ComputeSum2(dtCross);
                }
                //����ֵתΪ��
                this.ConverNullToZero(dtCross);
                this.ConverDataFromat(dtCross);
                //FarPoint��ֵ�����ø�ʽ
                this.SetFp(dtCross);
                   Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                   Neusoft.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("��ѯ���ݷ�������" + ex.Message);
            }
        }

        private void ConverDataFromat(DataTable dtCross)
        {
            if (this.dataFromat.Trim() == "")
            {
                return;
            }
            for (int i = 0; i < dtCross.Rows.Count; i++)
            {
                for (int j = 1; j < dtCross.Columns.Count; j++)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(dtCross.Rows[i][j].ToString()))
                        {
                        }
                        else
                        {
                            dtCross.Rows[i][j] = Neusoft.FrameWork.Function.NConvert.ToDecimal(dtCross.Rows[i][j].ToString()).ToString(this.dataFromat);//System.Math.Round(Neusoft.FrameWork.Function.NConvert.ToDecimal(dtCross.Rows[i][j].ToString()), 3);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        /// <summary>
        /// ִ��sql��䣬��ȡDataTable
        /// </summary>
        private DataTable GetDataTableBySql()
        {
            //ִ��sql���
            if (string.IsNullOrEmpty(this.sqlId.Trim()))
            {
                return null;
            }
            if (this.dtpFromDate.Value > this.dtpEndDate.Value)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ�ֹʱ�䣬����������");
                return null;
            }
            Neusoft.HISFC.BizLogic.Manager.Report reportMgr = new Neusoft.HISFC.BizLogic.Manager.Report();

            string[] parm = null;
            if (this.isHaveDept)
            {
                parm = new string[] { this.dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.cmbDept.Tag.ToString() };
            }
            else
            {
                parm = new string[] { this.dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss") };
            }

            DataSet ds = new DataSet();
            if (reportMgr.ExecQuery(this.sqlId, ref ds, parm) < 0)
            {
                MessageBox.Show("��ѯ���ݳ���" + reportMgr.Err);
                return null;
            }
            return ds.Tables[0];
        }

        int intColumns = 0;

        /// <summary>
        /// ����sql��ѯ�����DataTable���ɽ������DataTable
        /// </summary>
        /// <param name="dt">ԭDataTable</param>
        /// <returns>CrossDataTable</returns>
        private DataTable GetCrossDataTable(DataTable dt)
        {
            this.intColumns = dt.Columns.Count;
            if (dt.Columns.Count < 3)
            {
                MessageBox.Show("sql�����󣺽��汨�������в�������3��");
                return null;
            }
            else if (dt.Columns.Count == 4)
            {
                DataTable dtCross = new DataTable();
                //�����
                //��һ�С�����Ϊ��
                dtCross.Columns.Add(new DataColumn("ҽ��"));
                //�ڶ��С���������
                dtCross.Columns.Add(new DataColumn("����"));
                //�����С���Ӻϼ�
                dtCross.Columns.Add(new DataColumn("�ϼ�"));
                //�����С���Ӻϼ�
                //dtCross.Columns.Add(new DataColumn("ҩƷ��"));
                foreach (DataRow drCol in dt.Rows)
                {
                    string colName = drCol[2].ToString();
                    if (dtCross.Columns.Contains(colName))
                    {
                        continue;
                    }
                    dtCross.Columns.Add(colName);
                }

                //��Ӻϼ���
                DataRow drSum = dtCross.NewRow();
                dtCross.Rows.Add(drSum);
                drSum[1] = "�ϼ�:";


                //�������
                Hashtable htRow = new Hashtable();

                foreach (DataRow drRow in dt.Rows)
                {
                    string rowName = drRow[0].ToString();
                    string deptName = drRow[1].ToString();
                    string key = rowName + "|" + deptName;
                    if (htRow.ContainsKey(key))
                    {
                        DataRow drAdded = htRow[key] as DataRow;
                        drAdded[drRow[2].ToString()] = (Neusoft.FrameWork.Function.NConvert.ToDecimal(drAdded[drRow[2].ToString()].ToString())
                                                                                + Neusoft.FrameWork.Function.NConvert.ToDecimal(drRow[3].ToString())).ToString("#.####");
                    }
                    else
                    {
                        DataRow drNew = dtCross.NewRow();
                        htRow.Add(key, drNew);
                        dtCross.Rows.Add(drNew);
                        drNew[0] = rowName;
                        drNew[1] = drRow[1].ToString();
                        drNew[drRow[2].ToString()] = Neusoft.FrameWork.Function.NConvert.ToDecimal(drRow[3].ToString()).ToString("#.####");
                    }
                }
                return dtCross;
            }
            else
            {
                DataTable dtCross = new DataTable();
                //�����
                //��һ�С�����Ϊ��
                dtCross.Columns.Add(new DataColumn(" "));
                //�ڶ��С���Ӻϼ�
                dtCross.Columns.Add(new DataColumn("�ϼ�"));
                //�����С���ӱ�
                //dtCross.Columns.Add(new DataColumn("ҩƷ��"));
                foreach (DataRow drCol in dt.Rows)
                {
                    string colName = drCol[1].ToString();
                    if (dtCross.Columns.Contains(colName))
                    {
                        continue;
                    }
                    dtCross.Columns.Add(colName);
                }
                //�������
                Hashtable htRow = new Hashtable();

                foreach (DataRow drRow in dt.Rows)
                {
                    string rowName = drRow[0].ToString();

                    if (htRow.ContainsKey(rowName))
                    {
                        DataRow drAdded = htRow[rowName] as DataRow;
                        drAdded[drRow[1].ToString()] = (Neusoft.FrameWork.Function.NConvert.ToDecimal(drAdded[drRow[1].ToString()].ToString())
                                                                                + Neusoft.FrameWork.Function.NConvert.ToDecimal(drRow[2].ToString())).ToString("#.####");
                    }
                    else
                    {
                        DataRow drNew = dtCross.NewRow();
                        htRow.Add(rowName, drNew);
                        dtCross.Rows.Add(drNew);
                        drNew[0] = rowName;
                        drNew[drRow[1].ToString()] = Neusoft.FrameWork.Function.NConvert.ToDecimal(drRow[2].ToString()).ToString("#.####");
                    }
                }
                return dtCross;
            }
        }

        /// <summary>
        /// ��Ӻϼ�
        /// </summary>
        /// <param name="dtCross">CrossDataTable</param>
        private void ComputeSum(DataTable dtCross)
        {
            //�кϼ�
            if (this.haveSum)
            {
                DataRow drSum = dtCross.NewRow();
                dtCross.Rows.Add(drSum);
                drSum[0] = "�ϼ�:";
                for (int i = 1; i < dtCross.Columns.Count; i++)
                {
                    DataColumn dc = dtCross.Columns[i];
                    decimal sum = 0;
                    foreach (DataRow dr in dtCross.Rows)
                    {
                        sum += Neusoft.FrameWork.Function.NConvert.ToDecimal(dr[dc.ColumnName].ToString());
                    }
                    drSum[dc.ColumnName] = Math.Round(sum, 2); ;//sum.ToString("#.####");
                }
            }
            //�кϼ�
            if (this.haveRowSum)
            {
                //DataColumn dcSum = new DataColumn("�ϼ�");
                //dtCross.Columns.Add(dcSum);
                for (int i = 0; i < dtCross.Rows.Count; i++)
                {
                    DataRow drR = dtCross.Rows[i];
                    decimal rowSum = 0;
                    for (int j = 1; j < dtCross.Columns.Count; j++)
                    {
                        rowSum += Neusoft.FrameWork.Function.NConvert.ToDecimal(drR[dtCross.Columns[j].ColumnName].ToString());
                    }
                    drR["�ϼ�"] = Math.Round(rowSum,2);//rowSum.ToString("#.####");
                }

            }
            //�б���
            if (this.haveRowSum)
            {
                //DataColumn dcSum = new DataColumn("�ϼ�");
                //dtCross.Columns.Add(dcSum);
                decimal Rat = 0m;
                for (int i = 0; i < dtCross.Rows.Count; i++)
                {
                    DataRow drR = dtCross.Rows[i];
                    decimal rowSum = Neusoft.FrameWork.Function.NConvert.ToDecimal(drR["�ϼ�"]);
                    decimal rowSum1 = 0;
                    for (int j = 1; j < dtCross.Columns.Count; j++)
                    {
                        if (dtCross.Columns[j].ColumnName.ToString() == "��ҩ" || dtCross.Columns[j].ColumnName.ToString() == "�в�ҩ" ||
                            dtCross.Columns[j].ColumnName.ToString() == "�г�ҩ")
                        {
                            rowSum1 += Neusoft.FrameWork.Function.NConvert.ToDecimal(drR[dtCross.Columns[j].ColumnName].ToString());
                        }
                        else { continue; }

                    }
                    if (rowSum > 0)
                    {
                        Rat = Math.Round(rowSum1 / rowSum, 4) * 100;
                    }
                    //drR["ҩƷ��"] = Rat.ToString("#.####");//rowSum.ToString("#.####");
                }

            }
        }

        /// <summary>
        /// ��ҽ����ҽ��ͳ������кϼ�
        /// </summary>
        /// <param name="dtCross"></param>
        private void ComputeSum2(DataTable dtCross)
        {
            //�кϼ�
            if (this.haveSum)
            {
                DataRow drSum = dtCross.Rows[0];
                for (int i = 2; i < dtCross.Columns.Count; i++)
                {
                    DataColumn dc = dtCross.Columns[i];
                    decimal sum = 0;
                    foreach (DataRow dr in dtCross.Rows)
                    {
                        sum += Neusoft.FrameWork.Function.NConvert.ToDecimal(dr[dc.ColumnName].ToString());
                    }
                    drSum[dc.ColumnName] = sum.ToString("#.####");
                }
            }
            //�кϼ�
            if (this.haveRowSum)
            {
                //DataColumn dcSum = new DataColumn("�ϼ�");
                //dtCross.Columns.Add(dcSum);
                for (int i = 0; i < dtCross.Rows.Count; i++)
                {
                    DataRow drR = dtCross.Rows[i];
                    decimal rowSum = 0;
                    for (int j = 2; j < dtCross.Columns.Count; j++)
                    {
                        rowSum += Neusoft.FrameWork.Function.NConvert.ToDecimal(drR[dtCross.Columns[j].ColumnName].ToString());
                    }
                    drR["�ϼ�"] = rowSum.ToString("#.####");
                }

            }
        }

        /// <summary>
        /// FarPoint��ֵ�����ø�ʽ
        /// </summary>
        /// <param name="dtCross">CrossDataTable</param>
        private void SetFp(DataTable dtCross)
        {
            Neusoft.HISFC.BizLogic.Manager.Constant Constanst=new Neusoft.HISFC.BizLogic.Manager.Constant();
            string OperDept = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Dept.Name;
            string OperId = ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).ID;
            //��ѯ��Ϣ��ֵ
            this.lbQueryInfo.Text = "ʱ�䷶Χ��" + this.dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + " �� " + this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (isHaveDept)
            {
                this.lbQueryInfo.Text += "  ��ѯ���ң�" + this.cmbDept.Text;
            }
            //����Դ
            this.neuSpread1_Sheet1.DataSource = dtCross;
            //bool Operpriv = Constanst.ReturnPriv(OperId);
            //hn�¼ӵġ�������Աȥ������������Ϣ
            //for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            //{
            //    if (this.neuSpread1_Sheet1.Cells[i, 0].Text != OperDept && (Operpriv == false) && (IsHaveDept == false))
            //    {

            //        this.neuSpread1_Sheet1.Rows[i].Visible = false;
            //    }
            //}
            //���
            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                
                this.neuSpread1_Sheet1.Columns[i].Width = this.neuSpread1_Sheet1.Columns[i].GetPreferredWidth() + 8;
            }
            //��ͷλ��
            //int windowX = this.Width;
            decimal spreadWith = 0;
            foreach (FarPoint.Win.Spread.Column fpCol in this.neuSpread1_Sheet1.Columns)
            {
                spreadWith += (decimal)fpCol.Width;
            }
            if (spreadWith > this.plPrint.Width)
            {
                spreadWith = this.plPrint.Width;
            }

            int titleWidth = this.lbTitle.Size.Width;
            int titleX = Neusoft.FrameWork.Function.NConvert.ToInt32((spreadWith - titleWidth) / 2);
            if (titleX <= 0)
            {
                titleX = 1;
            }
            this.lbTitle.Location = new Point(titleX, this.lbTitle.Location.Y);
            //Fp��������
            if (this.Frozen > 0)
            {
                this.neuSpread1_Sheet1.FrozenColumnCount = frozen;
            }

            FarPoint.Win.Spread.CellType.NumberCellType numberCellType1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            for (int i = 0; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                if (i < this.intColumns - 2)
                {
                    continue;
                }
                for (int j = 0; j < this.neuSpread1_Sheet1.RowCount; j++)
                {
                    this.neuSpread1_Sheet1.Cells[j, i].CellType = numberCellType1;
                }
            }
        }

        /// <summary>
        /// ����ֵתΪ��
        /// </summary>
        /// <param name="dtCross"></param>
        private void ConverNullToZero(DataTable dtCross)
        {
            if (!this.replaceNullToZero)
            {
                return;
            }
            for (int i = 0; i < dtCross.Rows.Count; i++)
            {
                for (int j = 1; j < dtCross.Columns.Count; j++)
                {
                    if (string.IsNullOrEmpty(dtCross.Rows[i][j].ToString()))
                    {
                        dtCross.Rows[i][j] = "0";
                    }
                }
            }
        }

        #endregion ˽�з���

        #region ��������
        #endregion ��������

        #region ��������
        #endregion ��������

        #endregion ����

        #region �¼�

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucCrosstabReport_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        /// <summary>
        /// ��ѯ�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            this.Query();
            return 1;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrint(object sender, object neuObject)
        {
            if (MessageBox.Show("�Ƿ��ӡ?", "��ʾ��Ϣ", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return 1;
            }
               Neusoft.FrameWork.WinForms.Classes.Print print = new    Neusoft.FrameWork.WinForms.Classes.Print();
            try
            {
                print.IsLandScape = this.isLandScape;
            }
            catch
            {
            }

            // print.ShowPageSetup();
            //print.SetPageSize(new Neusoft.HISFC.Object.Base.PageSize("asdf", 1145, 800));
            //System.Drawing.Printing.PaperKind paperKind = System.Drawing.Printing.PaperKind.
            //print.SetPageSize(System.Drawing.Printing.PaperKind.Custom);
            //print.PrintPreview(0, 0, this.plPrint);
           
            print.PrintPage(0, 0, this.plPrint);

            return 1;
        }

        bool isLandScape = false;

        public bool IsLandScape
        {
            get
            {
                return isLandScape;
            }
            set
            {
                isLandScape = value;

            }
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show("�����ɹ�");
            }

            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// ��ӡԤ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnPrintPreview(object sender, object neuObject)
        {
               Neusoft.FrameWork.WinForms.Classes.Print print = new    Neusoft.FrameWork.WinForms.Classes.Print();
            try
            {
                print.IsLandScape = this.isLandScape;
            }
            catch
            {
            }
            print.PrintPreview(0, 0, this.plPrint);

            return 1;
        }

        #endregion �¼�

        #region �ӿ�ʵ��
        #endregion �ӿ�ʵ��

    }
}