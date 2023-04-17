using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;

namespace FS.WinForms.Report.BedDayReport
{
    /// <summary>
    /// ��λ�ձ�ͳ��
    /// <˵��>
    ///     1����Ժ״̬ 1 ���� 2 ��ת 3 δ�� 4 ���� 5 ���� 
    ///     2����Ժ�����ܼƣ� ָ������Ժ���޷���Ժ�ϼ�����
    ///     3����Ժ�������� ָ������Ժ����
    ///     4����Ժ��ռ���ܴ�������ָ��Ժ����סԺ�����ܼ�
    /// </˵��>
    /// </summary>
    public partial class ucBedDayReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        public ucBedDayReport()
        {
            InitializeComponent();

        }
        private bool isVisible = true;
        [Category("����"), Description("������Ŀ����true:ȫԺͳ�ƣ�false:������ϸ")]
        public bool IsVisible
        {
            get
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }
            base.OnLoad(e);
        }

        public int initVi()
        {
            if (this.isVisible == true)
            {
                this.neuSpread1_Sheet1.Visible = true;
                this.neuSpread1_Sheet2.Visible = false;

                this.neuLabel2.Visible = false;
                this.neuLabel6.Visible = false;
                this.cmbDept.Visible = false;
                this.neuDateTimePicker2.Visible = false;
                this.neuLabel5.Text = "סԺ���������ձ���";
            }
            else
            {
                this.neuSpread1_Sheet1.Visible = false;
                this.neuSpread1_Sheet2.Visible = true;

                this.neuLabel2.Visible = true;
                this.neuLabel6.Visible = true;
                this.cmbDept.Visible = true;
                this.neuDateTimePicker2.Visible = true;
                this.neuLabel5.Text = "סԺ���������ձ���(̨��)";
            }
            return 1;
        }

        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();

        protected DateTime BeginDate
        {
            get
            {
                return NConvert.ToDateTime(this.neuDateTimePicker1.Text);
            }
        }

        protected DateTime EndDate
        {
            get
            {
                return NConvert.ToDateTime(this.neuDateTimePicker2.Text);
            }
        }



        private void Init()
        {
            FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();
            System.Collections.ArrayList alDept = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);

            if (alDept == null)
            {
                MessageBox.Show("��ȡ�����б�������");
                return;
            }
            this.cmbDept.AddItems(alDept);
            this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);
            this.initVi();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.neuSpread1.ActiveSheet == this.neuSpread1_Sheet1)
            {
                this.QueryHosDayReport();

                #region ��ѯ���ʼ��

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    decimal outTot = 0;
                    outTot = outTot + NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut1].Text);
                    outTot = outTot + NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut2].Text);
                    outTot = outTot + NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut3].Text);
                    outTot = outTot + NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut4].Text);
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutTot].Text = outTot.ToString();
                    if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutTot].Text == "0")
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutTot].Text = "";
                    }
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColStandardNum].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColStandardNum].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColBeginNum].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColBeginNum].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColInNum].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColInNum].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferIn].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferIn].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferOut].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferOut].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTot].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTot].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutTot].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutTot].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut1].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut1].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut2].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut2].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut3].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut3].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut4].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOut4].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColEndNum].Text == "")
                    //{
                    //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColEndNum].Text = "0";
                    //}
                }

                #endregion
            }
            else
            {
                if (this.cmbDept.Tag == null)
                {
                    MessageBox.Show("��ѡ���ѯ����");
                }

                this.QueryDeptDayReport(this.cmbDept.Tag.ToString());

                #region ��ѯ���ʼ��

                for (int i = 0; i < this.neuSpread1_Sheet2.Rows.Count; i++)
                {
                    decimal outTot = 0;
                    outTot = outTot + NConvert.ToDecimal(this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut1].Text);
                    outTot = outTot + NConvert.ToDecimal(this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut2].Text);
                    outTot = outTot + NConvert.ToDecimal(this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut3].Text);
                    outTot = outTot + NConvert.ToDecimal(this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut4].Text);
                    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOutTot].Text = outTot.ToString();
                    if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOutTot].Text == "0")
                    {
                        this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOutTot].Text = "";
                    }
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColStandardNum].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColStandardNum].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColBeginNum].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColBeginNum].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColInNum].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColInNum].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColTransferIn].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColTransferIn].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColTransferOut].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColTransferOut].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColTot].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColTot].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOutTot].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOutTot].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut1].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut1].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut2].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut2].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut3].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut3].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut4].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOut4].Text = "0";
                    //}
                    //if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColEndNum].Text == "")
                    //{
                    //    this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColEndNum].Text = "0";
                    //}      
                }

                #endregion
            }

            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// ��ȡ��λ�ձ���Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ��� ����ұ���Ϊ����ͳ��ȫԺ��λ�ձ���Ϣ</param>
        /// <returns></returns>
        protected int QueryHosDayReport()
        {
            this.lbDate.Text = "���ڣ�" + this.BeginDate.ToString("yyyy-MM-dd");
            this.lbDept.Text = "ͳ�Ʒ�Χ��ȫԺ";

            #region ��λ��ϸ ת�롢ת��

            string execDetailSql = @"select t.dept_code,t.stat_type,t.extend,count(*)
--select *
from met_cas_dayreport_detail t
where t.stat_date = to_date('{0}','yyyy-mm-dd')
and   t.valid_state = '0'
group by t.dept_code,t.stat_type,t.extend
order by t.dept_code";

            execDetailSql = string.Format(execDetailSql, this.BeginDate.ToString("yyyy-MM-dd"));

            DataSet ds = new DataSet();
            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            if (dataManager.ExecQuery(execDetailSql, ref ds) == -1)
            {
                MessageBox.Show("��λ�ձ���ѯ��������" + dataManager.Err);
                return -1;
            }

            this.neuSpread1_Sheet1.Rows.Count = 0;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                #region ��λ�ձ���Ϣ����
                string privDept = "";
                int rowIndex = -1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (privDept != dr[0].ToString())
                    {
                        rowIndex++;

                        this.neuSpread1_Sheet1.Rows.Add(rowIndex, 1);
                        this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColDeptCode].Text = dr[0].ToString();
                        this.neuSpread1_Sheet1.Cells[rowIndex, 0].Text = this.deptHelper.GetName(dr[0].ToString());

                        privDept = dr[0].ToString();
                    }

                    switch (dr[1].ToString())
                    {
                        case "K":       //����
                        case "C":       //�ٻ�
                            decimal originalNum = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColInNum].Text);
                            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColInNum].Text = (originalNum + NConvert.ToDecimal(dr[3])).ToString();
                            break;
                        case "O":       //��Ժ
                            switch (dr[2].ToString())
                            {
                                case "1":
                                    this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColOut1].Text = NConvert.ToDecimal(dr[3]).ToString();
                                    break;
                                case "2":
                                    this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColOut2].Text = NConvert.ToDecimal(dr[3]).ToString();
                                    break;
                                case "3":
                                    this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColOut3].Text = NConvert.ToDecimal(dr[3]).ToString();
                                    break;
                                case "4":
                                    this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColOut4].Text = NConvert.ToDecimal(dr[3]).ToString();
                                    break;
                            }

                            decimal totNum = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColTot].Text);
                            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColTot].Text = (totNum + NConvert.ToDecimal(dr[3])).ToString();

                            break;
                        case "RI":      //ת��
                            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColTransferIn].Text = NConvert.ToDecimal(dr[3]).ToString();
                            break;
                        case "RO":      //ת��
                            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColTransferOut].Text = NConvert.ToDecimal(dr[3]).ToString();
                            break;
                        case "OF":      //�޷���Ժ
                            decimal totOFNum = NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColTot].Text);
                            this.neuSpread1_Sheet1.Cells[rowIndex, (int)ColumnEnum.ColTot].Text = (totOFNum + NConvert.ToDecimal(dr[3])).ToString();
                            break;
                    }
                }

                #endregion
            }

            #endregion

            #region �ڳ�����ĩ����

            string execTotSql = @"select s.dept_code,s.in_normal + s.in_return,s.out_normal + s.out_withdrawal,s.in_transfer,
                                         s.out_transfer,s.bed_stand,s.beginning_num,s.end_num,s.bed_add + s.bed_free
                                  from   met_cas_inpatientdayreport s 
                                  where  s.date_stat = to_date('{0}','yyyy-mm-dd')
                                  order by s.dept_code";

            execTotSql = string.Format(execTotSql, this.BeginDate.ToString("yyyy-MM-dd"));

            DataSet dsTot = new DataSet();
            if (dataManager.ExecQuery(execTotSql, ref dsTot) == -1)
            {
                MessageBox.Show("��λ�ձ���ѯ��������" + dataManager.Err);
                return -1;
            }

            if (dsTot != null && dsTot.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTot.Tables[0].Rows)
                {
                    for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                    {
                        if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColDeptCode].Text == dr[0].ToString())
                        {
                            //��Ժ����
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColInNum].Text = NConvert.ToDecimal(dr[1]).ToString();
                            //��Ժ����
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTot].Text = NConvert.ToDecimal(dr[2]).ToString();
                            //ת������
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferIn].Text = NConvert.ToDecimal(dr[3]).ToString();
                            //ת������
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferOut].Text = NConvert.ToDecimal(dr[4]).ToString();
                            //ʵ����λ
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColStandardNum].Text = NConvert.ToDecimal(dr[5]).ToString();
                            //�ڳ�����
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColBeginNum].Text = NConvert.ToDecimal(dr[6]).ToString();
                            //��ĩ����
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColEndNum].Text = NConvert.ToDecimal(dr[7]).ToString();
                            //�Ӵ���մ�
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColAddBed].Text = NConvert.ToDecimal(dr[8]).ToString();

                            if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColInNum].Text == "0")
                                this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColInNum].Text = "";
                            if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTot].Text == "0")
                                this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTot].Text = "";
                            if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutTot].Text == "0")
                                this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutTot].Text = "";
                            if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferIn].Text == "0")
                                this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferIn].Text = "";
                            if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferOut].Text == "0")
                                this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferOut].Text = "";
                            if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColStandardNum].Text == "0")
                                this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColStandardNum].Text = "";
                            if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColBeginNum].Text == "0")
                                this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColBeginNum].Text = "";
                            if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColEndNum].Text == "0")
                                this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColEndNum].Text = "";
                            if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColAddBed].Text == "0")
                                this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColAddBed].Text = "";
                           
                        }
                    }
                }
            }

            #endregion

            #region ��Ժ�ܴ�����


            string execOutStatSql = @"select  s.dept_code,sum(trunc(decode(s.IN_STATE,'N',s.oper_date,s.out_date)) - trunc(s.in_date))
from    met_cas_dayreport_detail t,fin_ipr_inmaininfo s
where   t.stat_date = to_date('{0}','yyyy-mm-dd')
and     t.valid_state = '0'
and     t.stat_type in ('O','OF')
and     t.inpatient_no = s.inpatient_no
group by s.dept_code";

            execOutStatSql = string.Format(execOutStatSql, this.BeginDate.ToString("yyyy-MM-dd"));

            DataSet dsOutStat = new DataSet();
            if (dataManager.ExecQuery(execOutStatSql, ref dsOutStat) == -1)
            {
                MessageBox.Show("��λ�ձ���ѯ��������" + dataManager.Err);
                return -1;
            }

            if (dsOutStat != null && dsOutStat.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsOutStat.Tables[0].Rows)
                {
                    for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                    {
                        if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColDeptCode].Text == dr[0].ToString())
                        {
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutStat].Text = NConvert.ToDecimal(dr[1]).ToString();
                        }
                    }
                }
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// ��ȡ��λ�ձ���Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ��� </param>
        /// <returns></returns>
        protected int QueryDeptDayReport(string deptCode)
        {
            this.lbDate.Text = "���ڣ�" + this.BeginDate.ToString("yyyy-MM-dd") + " �� " + this.EndDate.ToString("yyyy-MM-dd");
            this.lbDept.Text = "ͳ�Ʒ�Χ��" + this.cmbDept.Text;

            #region ��λ��ϸ ת�롢ת��

            string execDetailSql = @"select t.stat_date,t.stat_type,t.extend,count(*)
--select *
from met_cas_dayreport_detail t
where t.stat_date >= to_date('{0}','yyyy-mm-dd')
and   t.stat_date <= to_date('{1}','yyyy-mm-dd')
and   t.dept_code = '{2}'
and   t.valid_state = '0'
group by t.stat_date,t.stat_type,t.extend
order by t.stat_date";

            execDetailSql = string.Format(execDetailSql, this.BeginDate.ToString("yyyy-MM-dd"), this.EndDate.ToString("yyyy-MM-dd"), deptCode);

            DataSet ds = new DataSet();
            FS.FrameWork.Management.DataBaseManger dataManager = new FS.FrameWork.Management.DataBaseManger();
            if (dataManager.ExecQuery(execDetailSql, ref ds) == -1)
            {
                MessageBox.Show("��λ�ձ���ѯ��������" + dataManager.Err);
                return -1;
            }

            this.neuSpread1_Sheet2.Rows.Count = 0;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                #region ��λ�ձ���Ϣ����
                string privDate = "";
                int rowIndex = -1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    if (privDate != NConvert.ToDateTime(dr[0]).ToString("yyyy-MM-dd"))
                    {
                        rowIndex++;

                        this.neuSpread1_Sheet2.Rows.Add(rowIndex, 1);
                        this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColDateStat].Text = NConvert.ToDateTime(dr[0]).ToString("yyyy-MM-dd");

                        privDate = NConvert.ToDateTime(dr[0]).ToString("yyyy-MM-dd");
                    }

                    switch (dr[1].ToString())
                    {
                        case "K":       //����
                        case "C":       //�ٻ�
                            decimal originalNum = NConvert.ToDecimal(this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColInNum].Text);
                            this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColInNum].Text = (originalNum + NConvert.ToDecimal(dr[3])).ToString();
                            break;
                        case "O":       //��Ժ
                            switch (dr[2].ToString())
                            {
                                case "1":
                                    this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColOut1].Text = NConvert.ToDecimal(dr[3]).ToString();
                                    break;
                                case "2":
                                    this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColOut2].Text = NConvert.ToDecimal(dr[3]).ToString();
                                    break;
                                case "3":
                                    this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColOut3].Text = NConvert.ToDecimal(dr[3]).ToString();
                                    break;
                                case "4":
                                    this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColOut4].Text = NConvert.ToDecimal(dr[3]).ToString();
                                    break;
                            }

                            decimal totNum = NConvert.ToDecimal(this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColTot].Text);
                            this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColTot].Text = (totNum + NConvert.ToDecimal(dr[3])).ToString();

                            break;
                        case "RI":      //ת��
                            this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColTransferIn].Text = NConvert.ToDecimal(dr[3]).ToString();
                            break;
                        case "RO":      //ת��
                            this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColTransferOut].Text = NConvert.ToDecimal(dr[3]).ToString();
                            break;
                        case "OF":      //�޷���Ժ
                            decimal totOFNum = NConvert.ToDecimal(this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColTot].Text);
                            this.neuSpread1_Sheet2.Cells[rowIndex, (int)ColumnDetailEnum.ColTot].Text = (totOFNum + NConvert.ToDecimal(dr[3])).ToString();
                            break;
                    }
                }

                #endregion
            }

            #endregion

            #region �ڳ�����ĩ����

            string execTotSql = @"select s.date_stat,s.in_normal + s.in_return,s.out_normal + s.out_withdrawal,s.in_transfer,
       s.out_transfer,s.bed_stand,s.beginning_num,s.end_num
from   met_cas_inpatientdayreport s 
where  s.date_stat >= to_date('{0}','yyyy-mm-dd')
and    s.date_stat <= to_date('{1}','yyyy-mm-dd')
and    s.dept_code = '{2}'
order by s.date_stat";

            execTotSql = string.Format(execTotSql, this.BeginDate.ToString("yyyy-MM-dd"), this.EndDate.ToString("yyyy-MM-dd"), deptCode);

            DataSet dsTot = new DataSet();
            if (dataManager.ExecQuery(execTotSql, ref dsTot) == -1)
            {
                MessageBox.Show("��λ�ձ���ѯ��������" + dataManager.Err);
                return -1;
            }

            if (dsTot != null && dsTot.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsTot.Tables[0].Rows)
                {
                    for (int i = 0; i < this.neuSpread1_Sheet2.Rows.Count; i++)
                    {
                        if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColDateStat].Text == NConvert.ToDateTime(dr[0]).ToString("yyyy-MM-dd"))
                        {
                            //��Ժ����
                            this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColInNum].Text = NConvert.ToDecimal(dr[1]).ToString();
                            //��Ժ����
                            this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColTot].Text = NConvert.ToDecimal(dr[2]).ToString();
                            //ת������
                            this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColTransferIn].Text = NConvert.ToDecimal(dr[3]).ToString();
                            //ת������
                            this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColTransferOut].Text = NConvert.ToDecimal(dr[4]).ToString();
                            //ʵ����λ
                            this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColStandardNum].Text = NConvert.ToDecimal(dr[5]).ToString();
                            //�ڳ�����
                            this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColBeginNum].Text = NConvert.ToDecimal(dr[6]).ToString();
                            //��ĩ����
                            this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColEndNum].Text = NConvert.ToDecimal(dr[7]).ToString();


                            if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColInNum].Text == "0")
                                this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColInNum].Text = "";
                            if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColTot].Text == "0")
                                this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColTot].Text = "";
                            if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColTransferIn].Text == "0")
                                this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColTransferIn].Text = "";
                            if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColTransferOut].Text == "0")
                                this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColTransferOut].Text = "";
                            if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColStandardNum].Text == "0")
                                this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColStandardNum].Text = "";
                            if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColBeginNum].Text == "0")
                                this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColBeginNum].Text = "";
                            if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColEndNum].Text == "0")
                                this.neuSpread1_Sheet2.Cells[i, (int)ColumnEnum.ColEndNum].Text = "";
                        }
                    }
                }
            }

            #endregion

            #region ��Ժ�ܴ�����


            string execOutStatSql = @"select  t.stat_date,sum(trunc(decode(s.IN_STATE,'N',s.oper_date,s.out_date)) - trunc(s.in_date))
from    met_cas_dayreport_detail t,fin_ipr_inmaininfo s
where   t.stat_date >= to_date('{0}','yyyy-mm-dd')
and     t.stat_date <= to_date('{1}','yyyy-mm-dd')
and     t.dept_code = '{2}'
and     t.valid_state = '0'
and     t.stat_type in ('O','OF')
and     t.inpatient_no = s.inpatient_no
group by t.stat_date";

            execOutStatSql = string.Format(execOutStatSql, this.BeginDate.ToString("yyyy-MM-dd"), this.EndDate.ToString("yyyy-MM-dd"), deptCode);

            DataSet dsOutStat = new DataSet();
            if (dataManager.ExecQuery(execOutStatSql, ref dsOutStat) == -1)
            {
                MessageBox.Show("��λ�ձ���ѯ��������" + dataManager.Err);
                return -1;
            }

            if (dsOutStat != null && dsOutStat.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsOutStat.Tables[0].Rows)
                {
                    for (int i = 0; i < this.neuSpread1_Sheet2.Rows.Count; i++)
                    {
                        if (this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColDateStat].Text == NConvert.ToDateTime(dr[0]).ToString("yyyy-MM-dd"))
                        {
                            this.neuSpread1_Sheet2.Cells[i, (int)ColumnDetailEnum.ColOutStat].Text = NConvert.ToDecimal(dr[1]).ToString();
                        }
                    }
                }
            }

            #endregion

            #region �ϼ�ͳ��

            this.SetSum((int)ColumnDetailEnum.ColDateStat, (int)ColumnDetailEnum.ColStandardNum, (int)ColumnDetailEnum.ColBeginNum, (int)ColumnDetailEnum.ColInNum,
                (int)ColumnDetailEnum.ColTransferIn, (int)ColumnDetailEnum.ColTransferOut, (int)ColumnDetailEnum.ColTot, (int)ColumnDetailEnum.ColOutTot,
                (int)ColumnDetailEnum.ColOut1, (int)ColumnDetailEnum.ColOut2, (int)ColumnDetailEnum.ColOut3, (int)ColumnDetailEnum.ColOut4,
                (int)ColumnDetailEnum.ColEndNum, (int)ColumnDetailEnum.ColOutStat);

            #endregion

            return 1;
        }

        /// <summary>
        /// ��Ӻϼ���
        /// </summary>
        /// <param name="iTextIndex">"�ϼƣ�"��������</param>
        /// <param name="iIndex">�����ϼƵ�������</param>
        public void SetSum(int iTextIndex, params int[] iIndex)
        {
            int iRowIndex = this.neuSpread1_Sheet2.Rows.Count;
            this.neuSpread1_Sheet2.Rows.Add(iRowIndex, 1);
            this.neuSpread1_Sheet2.Cells[iRowIndex, iTextIndex].Text = "�ϼƣ�";
            if (iRowIndex == 0)
                return;
            for (int i = 0; i < iIndex.Length; i++)
            {
                if (iIndex[i] >= this.neuSpread1_Sheet2.Columns.Count)
                    continue;
                this.neuSpread1_Sheet2.Cells[iRowIndex, iIndex[i]].Formula = "SUM(" + (char)(65 + iIndex[i]) + "1:" + (char)(65 + iIndex[i]) + iRowIndex.ToString() + ")";
            }//Ϊ0��Ϊ�ա���

        }

        private enum ColumnEnum
        {
            /// <summary>
            /// �Ʊ�
            /// </summary>
            ColDept,
            /// <summary>
            /// ʵ����λ��
            /// </summary>
            ColStandardNum,
            /// <summary>
            /// �ڳ���λ��
            /// </summary>
            ColBeginNum,
            /// <summary>
            /// ��Ժ����
            /// </summary>
            ColInNum,
            /// <summary>
            /// ת������
            /// </summary>
            ColTransferIn,
            /// <summary>
            /// ת������
            /// </summary>
            ColTransferOut,
            /// <summary>
            /// �ܼ�
            /// </summary>
            ColTot,
            /// <summary>
            /// ��Ժ�ܼ�
            /// </summary>
            ColOutTot,
            /// <summary>
            /// ����
            /// </summary>
            ColOut1,
            /// <summary>
            /// ��ת
            /// </summary>
            ColOut2,
            /// <summary>
            /// δ��
            /// </summary>
            ColOut3,
            /// <summary>
            /// ����
            /// </summary>
            ColOut4,
            /// <summary>
            /// ��ĩ����
            /// </summary>
            ColEndNum,
            /// <summary>
            /// �Ӵ���մ�
            /// </summary>
            ColAddBed,
            /// <summary>
            /// ��Ժ����ͳ��
            /// </summary>
            ColOutStat,
            /// <summary>
            /// ���ұ���
            /// </summary>
            ColDeptCode
        }

        private enum ColumnDetailEnum
        {
            /// <summary>
            /// ͳ������
            /// </summary>
            ColDateStat,
            /// <summary>
            /// ʵ����λ��
            /// </summary>
            ColStandardNum,
            /// <summary>
            /// �ڳ���λ��
            /// </summary>
            ColBeginNum,
            /// <summary>
            /// ��Ժ����
            /// </summary>
            ColInNum,
            /// <summary>
            /// ת������
            /// </summary>
            ColTransferIn,
            /// <summary>
            /// ת������
            /// </summary>
            ColTransferOut,
            /// <summary>
            /// �ܼ�
            /// </summary>
            ColTot,
            /// <summary>
            /// ��Ժ�ܼ�
            /// </summary>
            ColOutTot,
            /// <summary>
            /// ����
            /// </summary>
            ColOut1,
            /// <summary>
            /// ��ת
            /// </summary>
            ColOut2,
            /// <summary>
            /// δ��
            /// </summary>
            ColOut3,
            /// <summary>
            /// ����
            /// </summary>
            ColOut4,
            /// <summary>
            /// ��ĩ����
            /// </summary>
            ColEndNum,
            /// <summary>
            /// ��Ժ����ͳ��
            /// </summary>
            ColOutStat
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuSpread1_SheetTabClick(object sender, FarPoint.Win.Spread.SheetTabClickEventArgs e)
        {
            if (e.SheetTabIndex == 0)
            {
                //this.neuLabel2.Visible = false;
                //this.neuLabel6.Visible = false;
                //this.cmbDept.Visible = false;
                //this.neuDateTimePicker2.Visible = false;
                //this.neuLabel3.Text = "                                 סԺ���������ձ���";

            }
            else
            {
                //this.neuLabel2.Visible = true;
                //this.neuLabel6.Visible = true;
                //this.cmbDept.Visible = true;
                //this.neuDateTimePicker2.Visible = true;
                //this.neuLabel3.Text = "                                 סԺ���������ձ���(̨��)";
            }

        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Line;
            print.PrintPage(30, 10, this.neuPanel1);

        }
        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return base.OnPrint(sender, neuObject);
        }
        /// <summary>
        /// ����
        /// </summary>
        private void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                // MessageBox.Show(Language.Msg("�����ɹ�"));
            }
        }
        public override int Export(object sender, object neuObject)
        {
            this.Export();

            return base.Export(sender, neuObject);
        }
    }
}
