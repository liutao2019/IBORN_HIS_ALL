using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Common.Controls
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
    public partial class ucCrosstabReport : FS.FrameWork.WinForms.Controls.ucBaseControl
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
        //private FS.HISFC.Models.Base.EnumDepartmentType deptType;

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
        /// ��ϸSQL���ID
        /// </summary>
        private string sqlDetailId = "";
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
        /// ʱ���ʽ
        /// </summary>
        private string dataTiemFromat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// Ĭ�ϲ�ѯ��ʱ�䷶Χ����
        /// </summary>
        private int queryDays = 1;

        /// <summary>
        /// �Զ�����
        /// </summary>
        private string customColumn = "";

        /// <summary>
        /// ���ָ�ʽ
        /// </summary>
        private string numberFormat = "0.00";

        /// <summary>
        /// ֽ�Ŵ�С
        /// {8A00B362-C6FD-4f2d-B370-ED2AC6537FCC}
        /// </summary>
        private string pageSize = "";
        /// <summary>
        /// ��ϸ���ݱ�
        /// </summary>
        private DataTable dtDetail = new DataTable();

        /// <summary>
        /// ��ϸ���ݱ����
        /// </summary>
        string[] detailParm = null;

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
        //public FS.HISFC.Models.Base.EnumDepartmentType DeptType
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

        /// <summary>
        /// �����С
        /// </summary>
        [Category("��ѯ����"), Description("�����С")]
        public float FontSize
        {
            get
            {
                if (this.neuSpread1_Sheet1.DefaultStyle.Font != null)
                {
                    return this.neuSpread1_Sheet1.DefaultStyle.Font.Size;
                }
                return 10;
            }
            set
            {
                this.neuSpread1_Sheet1.DefaultStyle.Font = new Font("[Font: Name=����, Size=10, Units=3, GdiCharSet=134, GdiVerticalFont=False]", value);
                this.neuSpread1_Sheet1.ColumnHeader.DefaultStyle.Font = new Font("[Font: Name=����, Size=10, Units=3, GdiCharSet=134, GdiVerticalFont=False]", value);
                this.neuSpread1_Sheet1.Columns[0].Font = new Font("[Font: Name=����, Size=10, Units=3, GdiCharSet=134, GdiVerticalFont=False]", value, FontStyle.Bold);
            }
        }

        /// <summary>
        /// �Զ�����
        /// </summary>
        [Category("��ѯ����"), Description("����Զ����У��ù�ʽ��ʾ�������֮���á�,�������������á�[]��ע��������[����]=[��1]+[��2],[����2]=[��1]*[��2]"), DefaultValue("")]
        public string CustomColumn
        {
            get
            {
                return customColumn;
            }
            set
            {
                customColumn = value;
            }
        }

        /// <summary>
        /// ���ָ�ʽ
        /// </summary>
        [Category("��ѯ����"), Description("���ָ�ʽ"), DefaultValue("0.00")]
        public string NumberFormat
        {
            get
            {
                return numberFormat;
            }
            set
            {
                numberFormat = value;
            }
        }

        /// <summary>
        /// ֽ�Ŵ�С
        /// {8A00B362-C6FD-4f2d-B370-ED2AC6537FCC}
        /// </summary>
        [Category("��ѯ����"), Description("����ֽ�Ŵ�С����ʽ��width,height����λ��MM"), DefaultValue("")]
        public string PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value;
            }
        }
        /// <summary>
        /// ��ϸsql���id
        /// </summary>
        [Category("��ѯ����"), Description("��ϸsql���id")]
        public string SqlDetailId
        {
            get
            {
                return sqlDetailId;
            }
            set
            {
                sqlDetailId = value;
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
                    FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
                    ArrayList alDept = interMgr.GetDepartment();
                    this.cmbDept.AddItems(alDept);
                }
                else
                {
                    FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
                    List<FS.FrameWork.Models.NeuObject> alPrivDept = managerIntegrate.QueryUserPriv(FS.FrameWork.Management.Connection.Operator.ID, this.privClass3Code.Trim());
                    if (alPrivDept != null)
                    {
                        this.cmbDept.AddItems(new ArrayList(alPrivDept.ToArray()));
                    }
                }
            }
            this.lbDept.Visible = this.isHaveDept;
            this.cmbDept.Visible = this.isHaveDept;
            //��ͷ
            this.lbTitle.Text = this.title;
            //ʱ���ʽ
            this.dtpFromDate.CustomFormat = this.dataTiemFromat;
            this.dtpEndDate.CustomFormat = this.dataTiemFromat;
            this.dtpFromDate.Value = new DateTime(this.dtpFromDate.Value.Year, this.dtpFromDate.Value.Month, this.dtpFromDate.Value.Day, 0, 0, 0);
            this.dtpEndDate.Value = new DateTime(this.dtpEndDate.Value.Year, this.dtpEndDate.Value.Month, this.dtpEndDate.Value.Day, 23, 59, 59);
            this.dtpFromDate.Value = this.dtpEndDate.Value.AddDays(-this.queryDays).AddSeconds(1);
        }

        /// <summary>
        /// ��ѯ
        /// </summary>
        private void Query()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڲ�ѯ���ݣ����Ե�");
                Application.DoEvents();
                //ִ��sql��䣬��ȡDataTable
                DataTable dt = this.GetDataTableBySql();
                if (dt == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                //����sql��ѯ�����DataTable���ɽ�����DataTable
                DataTable dtCross = this.GetCrossDataTable(dt);
                if (dtCross == null)
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    return;
                }
                //��Ӻϼ�
                this.ComputeSum(dtCross);
                //����Զ�����
                this.AddCustomColumns(dtCross);
                //����ֵתΪ��
                this.ConverNullToZero(dtCross);
                //ȡ��Ҫ��ʾ��DataTable��������Ƹ�ʽ
                DataTable dtShow = this.GetShowDataTable(dtCross);
                //FarPoint��ֵ�����ø�ʽ
                this.SetFp(dtShow);
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show("��ѯ���ݷ�������" + ex.Message);
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
            FS.HISFC.BizLogic.Manager.Report reportMgr = new FS.HISFC.BizLogic.Manager.Report();

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
        /// <summary>
        /// ִ��sql��䣬��ȡDataTable
        /// </summary>
        private DataTable GetDataTableBySql(string sql)
        {
            //ִ��sql���
            if (string.IsNullOrEmpty(sql.Trim()))
            {
                return null;
            }
            if (this.dtpFromDate.Value > this.dtpEndDate.Value)
            {
                MessageBox.Show("��ʼʱ�䲻�ܴ��ڽ�ֹʱ�䣬����������");
                return null;
            }
            FS.HISFC.BizLogic.Manager.Report reportMgr = new FS.HISFC.BizLogic.Manager.Report();

            DataSet ds = new DataSet();
            if (reportMgr.ExecQuery(sql, ref ds, detailParm) < 0)
            {
                MessageBox.Show("��ѯ���ݳ���" + reportMgr.Err);
                return null;
            }
            return ds.Tables[0];
        }

        /// <summary>
        /// ����sql��ѯ�����DataTable���ɽ������DataTable
        /// </summary>
        /// <param name="dt">ԭDataTable</param>
        /// <returns>CrossDataTable</returns>
        private DataTable GetCrossDataTable(DataTable dt)
        {
            if (dt.Columns.Count < 3)
            {
                MessageBox.Show("sql�����󣺽��汨�������в�������3��");
                return null;
            }
            DataTable dtCross = new DataTable();
            //�����
            //��һ�С�����Ϊ��
            dtCross.Columns.Add(new DataColumn(" "));
            foreach (DataRow drCol in dt.Rows)
            {
                string colName = drCol[1].ToString();
                if (dtCross.Columns.Contains(colName))
                {
                    continue;
                }
                dtCross.Columns.Add(colName, typeof(System.Decimal));
            }
            //�������
            Hashtable htRow = new Hashtable();

            foreach (DataRow drRow in dt.Rows)
            {
                string rowName = drRow[0].ToString();

                if (htRow.ContainsKey(rowName))
                {
                    DataRow drAdded = htRow[rowName] as DataRow;
                    drAdded[drRow[1].ToString()] = (FS.FrameWork.Function.NConvert.ToDecimal(drAdded[drRow[1].ToString()].ToString())
                                                                            + FS.FrameWork.Function.NConvert.ToDecimal(drRow[2].ToString())).ToString(this.numberFormat);
                }
                else
                {
                    DataRow drNew = dtCross.NewRow();
                    htRow.Add(rowName, drNew);
                    dtCross.Rows.Add(drNew);
                    drNew[0] = rowName;
                    drNew[drRow[1].ToString()] = FS.FrameWork.Function.NConvert.ToDecimal(drRow[2].ToString()).ToString(this.numberFormat);
                }
            }
            return dtCross;
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
                        sum += FS.FrameWork.Function.NConvert.ToDecimal(dr[dc.ColumnName].ToString());
                    }
                    drSum[dc.ColumnName] = sum.ToString(this.numberFormat);
                }
            }
            //�кϼ�
            if (this.haveRowSum)
            {
                DataColumn dcSum = new DataColumn("�ϼ�", typeof(System.Decimal));
                dtCross.Columns.Add(dcSum);
                for (int i = 0; i < dtCross.Rows.Count; i++)
                {
                    DataRow drR = dtCross.Rows[i];
                    decimal rowSum = 0;
                    for (int j = 1; j < dtCross.Columns.Count; j++)
                    {
                        rowSum += FS.FrameWork.Function.NConvert.ToDecimal(drR[dtCross.Columns[j].ColumnName].ToString());
                    }
                    drR["�ϼ�"] = rowSum.ToString();
                }

            }
        }

        /// <summary>
        /// ����Զ�����
        /// </summary>
        /// <param name="dtCross">CrossDataTable</param>
        private void AddCustomColumns(DataTable dtCross)
        {
            if (string.IsNullOrEmpty(this.customColumn.Trim()))
            {
                return;
            }
            string[] columnArray = this.customColumn.Split(',');
            foreach (string columnExStr in columnArray)
            {
                string[] tmpStr = columnExStr.Split('=');
                if (tmpStr.Length < 2)
                {
                    MessageBox.Show("��" + columnExStr + "���ı��ʽ����ȷ���Ⱥ��д���");
                    return;
                }
                string columnName = tmpStr[0].Trim().Replace("[", "").Replace("]", "");
                string columnExpression = this.converExpression(tmpStr[1], dtCross);
                dtCross.Columns.Add(new DataColumn(columnName, typeof(System.Decimal)));
                try
                {
                    dtCross.Columns[columnName].Expression = columnExpression;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("���㡰" + columnExStr + "��ʱ��������" + ex.Message);
                    continue;
                }

            }

        }

        /// <summary>
        /// ��ȡ���ʽ
        /// </summary>
        /// <param name="str">���ʽ</param>
        /// <param name="dtCross">CrossDataTable</param>
        /// <returns>���ʽ</returns>
        private string converExpression(string str, DataTable dtCross)
        {
            string myStr = str.Trim().Replace(" ", "");
            string[] arrayWithOutOp = myStr.Split('+', '-', '*', '/', '(', ')', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9');

            foreach (string colName in arrayWithOutOp)
            {
                if (string.IsNullOrEmpty(colName.Trim()))
                {
                    continue;
                }
                if (colName.StartsWith("'"))
                {
                    continue;
                }
                if (!colName.StartsWith("["))
                {
                    myStr = myStr.Replace(colName, "0");
                    continue;
                }
                if (dtCross.Columns.Contains(colName.Replace("[", "").Replace("]", "")))
                {
                    continue;
                }
                myStr = myStr.Replace(colName, "0");
            }

            return myStr;
        }

        /// <summary>
        /// ȡ��Ҫ��ʾ��DataTable��������Ƹ�ʽ
        /// </summary>
        /// <param name="dtCross">���汨��</param>
        /// <returns>Ҫ��ʾ�Ľ��汨��</returns>
        private DataTable GetShowDataTable(DataTable dtCross)
        {
            DataTable dtShow = new DataTable();
            foreach (DataColumn dc in dtCross.Columns)
            {
                dtShow.Columns.Add(new DataColumn(dc.ColumnName, typeof(System.String)));
            }
            for (int i = 0; i < dtCross.Rows.Count; i++)
            {
                DataRow drCross = dtCross.Rows[i];
                DataRow drShow = dtShow.NewRow();
                dtShow.Rows.Add(drShow);
                for (int j = 0; j < dtCross.Columns.Count; j++)
                {
                    try
                    {
                        drShow[j] = FS.FrameWork.Function.NConvert.ToDecimal(drCross[j].ToString()).ToString(this.numberFormat);
                    }
                    catch (Exception ex)
                    {
                        drShow[j] = drCross[j].ToString();
                    }
                }
            }
            return dtShow;
        }

        /// <summary>
        /// FarPoint��ֵ�����ø�ʽ
        /// </summary>
        /// <param name="dtCross">CrossDataTable</param>
        private void SetFp(DataTable dtCross)
        {
            //��ѯ��Ϣ��ֵ
            this.lbQueryInfo.Text = "ʱ�䷶Χ��" + this.dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss") + " �� " + this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (isHaveDept)
            {
                this.lbQueryInfo.Text += "  ��ѯ���ң�" + this.cmbDept.Text;
            }
            //����Դ
            this.neuSpread1_Sheet1.DataSource = dtCross;
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
            int titleX = FS.FrameWork.Function.NConvert.ToInt32((spreadWith - titleWidth) / 2);
            if (titleX <= 0)
            {
                titleX = 1;
            }
            this.lbTitle.Location = new Point(titleX, this.lbTitle.Location.Y);
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
                        try
                        {
                            dtCross.Rows[i][j] = "0";
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
            }
        }

        #endregion ˽�з���

        #region ��������
        /// <summary>
        /// ��ϸ��ѯ
        /// </summary>
        protected void ShowDetail()
        {
            try
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�����ϸ��Ϣ...���Ժ�");
                Application.DoEvents();

                this.dtDetail = this.GetDataTableBySql(this.sqlDetailId);

                this.neuSpread1_Sheet2.DataSource = this.dtDetail.DefaultView;

                //this.SetFormat(true);

                this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

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
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //{8A00B362-C6FD-4f2d-B370-ED2AC6537FCC}����ֽ�Ŵ�С����
            if (!string.IsNullOrEmpty(this.pageSize))
            {
                try
                {
                    string[] size = this.pageSize.Split(',');
                    int pwidth = Int32.Parse(size[0]);
                    int pheight = Int32.Parse(size[0]);
                    FS.HISFC.Models.Base.PageSize page = new FS.HISFC.Models.Base.PageSize();
                    page.Name = "crossReport";
                    page.WidthMM = pwidth;
                    page.HeightMM = pheight;
                    print.SetPageSize(page);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("ֽ�Ŵ�С��������");
                    return -1;
                }
            }
            print.PrintPage(0, 0, this.plPrint);

            return 1;
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
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPreview(0, 0, this.plPrint);

            return 1;
        }
        /// <summary>
        /// ˫����Ԫ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader)
            {
                return;
            }

            if (this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text == "�ϼ�:" || this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 1].Text == "�ϼ�:" || this.neuSpread1_Sheet1.Columns[this.neuSpread1_Sheet1.ActiveColumnIndex].Label.ToString() == "�ϼ�")
            {
                return;
            }

            if (string.IsNullOrEmpty(this.sqlDetailId))
            {
                return;
            }

            if (this.isHaveDept)
            {
                detailParm = new string[] { this.dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.cmbDept.Tag.ToString(), this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text.ToString(), this.neuSpread1_Sheet1.Columns[this.neuSpread1_Sheet1.ActiveColumnIndex].Label.ToString() };
            }
            else
            {
                detailParm = new string[] { this.dtpFromDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.dtpEndDate.Value.ToString("yyyy-MM-dd HH:mm:ss"), this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, 0].Text.ToString(), this.neuSpread1_Sheet1.Columns[this.neuSpread1_Sheet1.ActiveColumnIndex].Label.ToString() };
            }
            //��ʾ��ϸ��Ϣ
            this.ShowDetail();
        }
        #endregion �¼�

        #region �ӿ�ʵ��
        #endregion �ӿ�ʵ��

    }
}