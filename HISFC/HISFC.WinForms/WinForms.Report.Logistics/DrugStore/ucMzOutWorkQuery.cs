using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using System.Collections;

namespace FS.Report.Logistics.DrugStore
{
    /// <summary>
    /// [��������: ���﹤������ѯ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2007-03]<br></br>
    /// <�޸ļ�¼ 
    ///		 ��ʵ�� Ȩ��ϵͳ����
    ///  />
    /// </summary>
    public partial class ucMzOutWorkQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucMzOutWorkQuery()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ҩ������
        /// </summary>
        ArrayList deptData = new ArrayList();

        /// <summary>
        /// ��Ա����
        /// </summary>
        ArrayList personData = new ArrayList();

        /// <summary>
        /// �Ƿ������ҩ��������ѯ
        /// </summary>
        private bool isDrugTerminalQuery = true;

        /// <summary>
        /// Ȩ�޿���
        /// </summary>
        private FS.FrameWork.Models.NeuObject privDept = null;

        /// <summary>
        /// Ȩ����Ա
        /// </summary>
        private FS.FrameWork.Models.NeuObject privOper = null;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ������ҩ��������ѯ
        /// </summary>
        [Description("�Ƿ������ҩ��������ѯ"), Category("����"), DefaultValue(true)]
        public bool IsDrugTerminalQuery
        {
            get
            {
                return this.isDrugTerminalQuery;
            }
            set
            {
                this.isDrugTerminalQuery = value;

                this.SetParm();
            }
        }

        /// <summary>
        /// �Ƿ�ҩ����ѯ
        /// </summary>
        public bool DrugDeptQuery
        {
            set
            {
                this.rbTerminalShow.Visible = value;
                this.rbOperShow.Visible = value;
            }
        }

        #endregion

        #region ��������Ϣ

        /// <summary>
        /// ���幤��������
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryData();

            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            this.Export();

            return base.Export(sender, neuObject);
        }

        #endregion

        /// <summary>
        /// ��ʼ��
        /// </summary>
        private int Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ��ز�ѯ���� ���Ժ�");
            Application.DoEvents();
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();

            DateTime sysTime = deptManager.GetDateTimeFromSysDateTime();

            this.dtBegin.Value = sysTime.Date.AddDays(-1);
            this.dtEnd.Value = sysTime;

            ArrayList al = deptManager.GetDeptmentAll();
            if (al == null)
            {
               Function.ShowMsg("���ؿ����б�ʧ��" + deptManager.Err);
                return -1;
            }
            foreach (FS.HISFC.Models.Base.Department info in al)
            {
                if (info.DeptType.ID.ToString() == "P")
                    this.deptData.Add(info);
            }

            this.personData = personManager.GetEmployeeAll();
            if (this.personData == null)
            {
                Function.ShowMsg("������Ա�б�ʧ��" + personManager.Err);
                return -1;
            }

            this.privDept = ((FS.HISFC.Models.Base.Employee)deptManager.Operator).Dept;
            this.privOper = deptManager.Operator;

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            return 1;
        }

        /// <summary>
        /// ���ݲ������в�ͬ����
        /// </summary>
        protected void SetParm()
        {
            if (this.isDrugTerminalQuery)
                this.lbReportTitle.Text = "��ҩ������ͳ��";
            else
                this.lbReportTitle.Text = "��ҩ������ͳ��";
        }

        /// <summary>
        /// ��ȡSql����
        /// </summary>
        /// <returns></returns>
        protected string GetSqlIndex()
        {
            string sqlIndex = "";
            if (this.rbDept.Checked)		//ҩ����ѯ
            {
                if (this.isDrugTerminalQuery)		//��ҩ��������ѯ
                {
                    if (this.rbTerminalShow.Checked)
                        sqlIndex = "Pharmacy.Item.ClinicQuery.Druged.DrugDept.Terminal";
                    else
                        sqlIndex = "Pharmacy.Item.ClinicQuery.Druged.DrugDept.Oper";
                }
                else
                {
                    if (this.rbTerminalShow.Checked)
                        sqlIndex = "Pharmacy.Item.ClinicQuery.Send.DrugDept.Terminal";
                    else
                        sqlIndex = "Pharmacy.Item.ClinicQuery.Send.DrugDept.Oper";
                }
            }
            if (this.rbPerson.Checked)		//��Ա��ѯ
            {
                if (this.isDrugTerminalQuery)		//��ҩ��������ѯ
                    sqlIndex = "Pharmacy.Item.ClinicQuery.Druged.Person";
                else
                    sqlIndex = "Pharmacy.Item.ClinicQuery.Send.Person";
            }
            return sqlIndex;
        }

        /// <summary>
        /// �ж��Ƿ�Բ�ѯ������ѡ������
        /// </summary>
        /// <returns></returns>
        protected virtual bool IsValid()
        {
            if (this.cmbData.Tag == null)
            {
                MessageBox.Show(Language.Msg("��ѡ���ѯ���һ���Ա"));
                return false;
            }
            if (FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text) >= FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text))
            {
                MessageBox.Show(Language.Msg("��ѯ ��ʼʱ��Ӧ������ֹʱ��"));
                return false;
            }
            
            return true;
        }

        /// <summary>
        /// ���ݼ���
        /// </summary>
        protected void QueryData()
        {
            if (!this.IsValid())
            {
                return;
            }

            this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet1;
            if (this.neuSpread1.Sheets.Contains(this.neuSpread1_Sheet2))
            {
                this.neuSpread1.Sheets.Remove(this.neuSpread1_Sheet2);
            }


            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            string sqlIndex = this.GetSqlIndex();
            string sql = "";
            dataManager.Sql.GetSql(sqlIndex, ref sql);
            sql = string.Format(sql, this.cmbData.Tag.ToString(), FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text).ToString(), FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text).ToString());
            DataSet ds = new DataSet();
            if (dataManager.ExecQuery(sql, ref ds) == -1)
            {
                MessageBox.Show(Language.Msg("��������ѯ���� \n" + dataManager.Err));
                return;
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                this.neuSpread1_Sheet1.DataSource = ds;
            }

            try
            {
                if (this.rbPerson.Checked)
                {
                    this.Sum(2, 3, 4, 5);
                }
                else
                {
                    if (this.rbTerminalShow.Checked)
                    {
                        this.Sum(1, 2, 3, 4);
                    }
                    else
                    {
                        this.Sum(2, 3, 4, 5);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ��ϸ��ѯ
        /// </summary>
        /// <param name="operCode">����ѯ��Ա����</param>
        protected void QueryDetail(string operCode)
        {
            this.neuSpread1_Sheet2.Rows.Count = 0;

            DateTime beginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.dtBegin.Text);
            DateTime endTime = FS.FrameWork.Function.NConvert.ToDateTime(this.dtEnd.Text);

            DateTime queryBeginDate = beginTime;
            DateTime queryEndDate = System.DateTime.MinValue;

            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

            string sqlIndex = "";
            if (this.isDrugTerminalQuery)
                sqlIndex = "Pharmacy.Item.ClinicQuery.Druged.Oper.Detail";
            else
                sqlIndex = "Pharmacy.Item.ClinicQuery.Send.Oper.Detail";

            string sql = "";
            if (dataManager.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                MessageBox.Show(Language.Msg("����������ȡSql������ \n" + dataManager.Err));
                return;
            }

            DataSet dsDetail = null;

            string formatSql = "";

            while (queryEndDate < endTime)
            {
                queryEndDate = queryBeginDate.Date.AddDays(1);
                if (queryEndDate > endTime)
                {
                    queryEndDate = endTime;
                }

                formatSql = string.Format(sql, operCode, queryBeginDate, queryEndDate,this.cmbData.Tag.ToString());
                DataSet ds = new DataSet();
                if (dataManager.ExecQuery(formatSql, ref ds) == -1)
                {
                    MessageBox.Show(Language.Msg("��������ѯ���� \n" + dataManager.Err));
                    return;
                }

                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (dsDetail == null)
                    {
                        dsDetail = ds.Clone();
                    }

                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        dsDetail.Tables[0].Rows.Add(new object[] {
																	dr[0],
																	dr[1],
																	dr[2],
																	dr[3],
																	dr[4],
																	dr[5],
																	dr[6]
																 });
                    }
                }
                queryBeginDate = queryEndDate;
            }

            if (dsDetail != null && dsDetail.Tables.Count > 0 && dsDetail.Tables[0].Rows.Count > 0)
            {
                this.neuSpread1_Sheet2.DataSource = dsDetail;
            }
            if (this.neuSpread1.Sheets.Contains(this.neuSpread1_Sheet2))
            {
                this.neuSpread1.ActiveSheet = this.neuSpread1_Sheet2;
            }
        }

        /// <summary>
        /// ���Ӻϼ�
        /// </summary>
        /// <param name="countColumns">���ۼӵ���</param>
        protected void Sum(params int[] countColumns)
        {
            if (this.neuSpread1_Sheet1.Rows.Count <= 0)
                return;

            int iIndex = this.neuSpread1_Sheet1.Rows.Count;
            this.neuSpread1_Sheet1.Rows.Add(iIndex, 1);
            this.neuSpread1_Sheet1.Cells[iIndex, 0].Text = "�ϼƣ�";

            for (int j = 0; j < countColumns.Length; j++)
            {
                this.neuSpread1_Sheet1.Cells[iIndex, countColumns[j]].Formula = "SUM(" + (char)(65 + countColumns[j]) + "1:" + (char)(65 + countColumns[j]) + iIndex.ToString() + ")";
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                if (!FS.HISFC.Components.Common.Classes.Function.ChoosePiv("0300"))
                {
                    MessageBox.Show(Language.Msg("���޲�ѯȨ��..."));
                    return;
                }

                if (this.Init() == -1)
                {
                    return;
                }

                //�����¼� ����ҩ���б�
                this.rbDept_CheckedChanged(null, System.EventArgs.Empty);

                this.neuSpread1_Sheet1.DefaultStyle.Locked = true;
            }
            catch
            { }

            base.OnLoad(e);
        }

        private void rbDept_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbDept.Checked)
            {
                this.cmbData.alItems = null;
                this.cmbData.AddItems(this.deptData);

                int selectIndex = 0;
                foreach (FS.FrameWork.Models.NeuObject dept in this.deptData)
                {
                    if (this.privDept.ID == dept.ID)
                    {
                        break;
                    }

                    selectIndex++;
                }

                if (selectIndex < this.deptData.Count)
                {
                    this.cmbData.SelectedIndex = selectIndex;
                }

                this.DrugDeptQuery = true;
            }
        }

        private void rbPerson_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbPerson.Checked)
            {
                this.cmbData.alItems = null;
                this.cmbData.AddItems(this.personData);

                int selectIndex = 0;
                foreach (FS.FrameWork.Models.NeuObject person in this.personData)
                {
                    if (this.privOper.ID == person.ID)
                    {
                        break;
                    }

                    selectIndex++;
                }

                if (selectIndex < this.personData.Count)
                {
                    this.cmbData.SelectedIndex = selectIndex;
                }

                this.DrugDeptQuery = false;
            }
        }

        private void rbTerminal_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbTerminalShow.Checked)
            {
                this.QueryData();
            }
        }

        private void rbOper_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rbOperShow.Checked)
            {
                this.QueryData();
            }
        }

        //��ӡԤ��
        public override int PrintPreview(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Classes.Print printview = new FS.FrameWork.WinForms.Classes.Print();

            //printview.PrintPreview(0, 0, this.neuTabControl1.SelectedTab);
            printview.PrintPreview(this.neuPanel2);
            return base.OnPrintPreview(sender, neuObject);
        }

        //��ӡ
        protected override int OnPrint(object sender, object neuObject)
        {
            this.neuSpread1.PrintSheet(0);
            return base.OnPrint(sender, neuObject);
        }
        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.rbPerson.Checked)
            {
                return;
            }

            if (!this.rbOperShow.Checked)
            {
                return;
            }

            if (!this.neuSpread1.Sheets.Contains(this.neuSpread1_Sheet2))
            {
                this.neuSpread1.Sheets.Add(this.neuSpread1_Sheet2);
            }

            if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet1)
            {
                string operCode = this.neuSpread1_Sheet1.Cells[e.Row, 0].Text;

                try
                {
                    this.QueryDetail(operCode);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
