using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.WinForms.Report.Pharmacy
{
    public partial class ucYearReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucYearReport()
        {
            InitializeComponent();
        }

        #region �����

        /// <summary>
        /// ����Sql
        /// </summary>
        private string sqlTotal = "";

        /// <summary>
        /// ��ϸSql
        /// </summary>
        private string sqlDetail = "";

        #endregion

        #region ����

        /// <summary>
        /// ��ǰ������Ա����
        /// </summary>
        private FS.FrameWork.Models.NeuObject OperDept
        {
            get
            {
                if (this.cmbDept.Tag == null || this.cmbDept.Text == "")
                {
                    MessageBox.Show("��ѡ���ѯ�ⷿ");
                    return new FS.FrameWork.Models.NeuObject();
                }
                FS.FrameWork.Models.NeuObject dept = new FS.FrameWork.Models.NeuObject();
                dept.ID = this.cmbDept.Tag.ToString();
                dept.Name = this.cmbDept.Text;

                return dept;
            }
        }

        /// <summary>
        /// ��ʼʱ��
        /// </summary>
        private DateTime BeginTime
        {
            get
            {
                return new DateTime(this.dtYear.Value.Year, 1, 1);
            }
        }

        /// <summary>
        /// ��ֹʱ��
        /// </summary>
        private DateTime EndTime
        {
            get
            {
                return new DateTime(this.dtYear.Value.Year, 12, 30);
            }
        }

        #endregion

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QueryTotal();

            return base.OnQuery(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
            return base.Export(sender, neuObject);
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int Init()
        {
            #region Sql��ʼ��

            this.sqlTotal = @"select t.from_date ��ʼʱ��,t.to_date ��ֹʱ��, sum(t.last_month_cost) as ���ڽ��, 
            sum(t.in_cost) as �������, 
            sum(t.out_cost) as ���ڳ���, 
            sum(t.special_in_cost) as �������, 
            sum(t.special_out_cost) as �������, 
            sum(t.check_profit_cost + t.check_loss_cost) as �̵�ӯ��, 
            sum(t.adjust_profit_cost + t.adjust_loss_cost) as ����ӯ��, 
            sum(t.current_store_cost) as ���ڽ�� 
from   pha_com_ms_dept t
where  t.drug_dept_code = '{0}'
and    t.from_date > to_date('{1}','yyyy-mm-dd hh24:mi:ss')
and    t.to_date < to_date('{2}','yyyy-mm-dd hh24:mi:ss')
group by t.from_date,t.to_date";

            this.sqlDetail = @" select  t.trade_name ��Ʒ����,t.specs ���,
            t.last_month_cost as ���ڽ��, 
            t.in_cost as �������, 
            t.out_cost as ���ڳ���, 
            t.special_in_cost as �������, 
            t.special_out_cost as �������, 
            t.check_profit_cost + t.check_loss_cost as �̵�ӯ��, 
            t.adjust_profit_cost + t.adjust_loss_cost as ����ӯ��, 
            t.current_store_cost as ���ڽ�� 
    from    pha_com_ms_drug t 
    where   t.parent_code =  fun_get_parentcode  
    and     t.current_code =  fun_get_currentcode  
    and     t.drug_dept_code = '{0}' 
    and     t.from_date >= to_date('{1}','yyyy-mm-dd HH24:mi:ss') 
    and     t.to_date <= to_date('{2}','yyyy-mm-dd HH24:mi:ss') ";

            #endregion

            #region ��ȡ����

            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            ArrayList al = deptManager.GetDeptmentAll();

            ArrayList alDept = new ArrayList();
            foreach (FS.HISFC.Models.Base.Department info in al)
            {
                if (info.DeptType.ID.ToString() == "P" || info.DeptType.ID.ToString() == "PI")
                    alDept.Add(info);
            }

            this.cmbDept.AddItems(alDept);

            this.cmbDept.SelectedIndex = 0;

            #endregion

            this.fpHead.DefaultStyle.Locked = true;

            return 1;
        }

        /// <summary>
        /// ���
        /// </summary>
        protected void Clear()
        {
            this.fpHead.Rows.Count = 0;

            this.fpDetail.Rows.Count = 0;
        }

        /// <summary>
        /// ���ܲ�ѯ
        /// </summary>
        /// <returns></returns>
        protected int QueryTotal()
        {
            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

            if (this.OperDept.ID == "")
                return -1;

            this.Clear();

            DataSet ds = new DataSet();

            this.lbDept.Text = "������:" + this.OperDept.Name;
            this.lbDate.Text = "ͳ������:" + this.BeginTime.ToString() + "��" + this.EndTime.ToString();

            string exeTotal = string.Format(this.sqlTotal, this.OperDept.ID,this.BeginTime.ToString(),this.EndTime.ToString());

            if (dataManager.ExecQuery(exeTotal, ref ds) == -1)
            {
                MessageBox.Show(Language.Msg("��ȡ�����ܼ�¼��������"));
                return -1;
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                this.fpHead.DataSource = ds;

                this.Sum();
            }

            return 1;
        }

        /// <summary>
        /// ��ϸ��ѯ
        /// </summary>
        /// <param name="iRowIndex">������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected int QueryDetail(int iRowIndex)
        {
            if (this.neuSpread1.ActiveSheet == this.fpDetail)
            {
                return -1;
            }
            if (iRowIndex == this.fpHead.Rows.Count - 1)
            {
                return -1;
            }

            if (this.OperDept.ID == "")
                return -1;

            if (this.fpHead.Rows.Count <= 0)
                return -1;

            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();

            DateTime dtBegin = NConvert.ToDateTime(this.fpHead.Cells[iRowIndex, 0].Text);
            DateTime dtEnd = NConvert.ToDateTime(this.fpHead.Cells[iRowIndex, 1].Text);

            string exeDetail = string.Format(this.sqlDetail, this.OperDept.ID, dtBegin.ToString(), dtEnd.ToString());

            DataSet ds = new DataSet();

            if (dataManager.ExecQuery(exeDetail, ref ds) == -1)
            {
                MessageBox.Show(Language.Msg("��ȡ�½���ϸ��������"));
                return -1;
            }

            if (ds != null && ds.Tables.Count > 0)
            {
                this.fpDetail.DataSource = ds;

                this.neuSpread1.ActiveSheet = this.fpDetail;
            }

            return 1;
        }

        /// <summary>
        /// �ϼƼ���
        /// </summary>
        protected void Sum()
        {
            if (this.fpHead.Rows.Count <= 0)
            {
                return;
            }

            int rowCount = this.fpHead.Rows.Count;
            this.fpHead.Rows.Add(rowCount, 1);

            for (int i = 2; i < this.fpHead.Columns.Count; i++)
            {
                this.fpHead.Cells[rowCount, i].Formula = string.Format("SUM({0}1:{0}{1})",((char)(65 + i)).ToString(),rowCount.ToString());
            }

            this.fpHead.Rows[rowCount].Font = new Font("����", 9F, FontStyle.Bold);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Init();

            base.OnLoad(e);
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.RowHeader || e.ColumnHeader)
            {
                return;
            }

            this.QueryDetail(e.Row); 
        }
    }
}
