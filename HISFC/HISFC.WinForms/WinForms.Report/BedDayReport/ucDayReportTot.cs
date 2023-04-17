using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
using FS.FrameWork.Function;

namespace FS.WinForms.Report.BedDayReport
{
    public partial class ucDayReportTot : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDayReportTot()
        {
            InitializeComponent();

            if (System.Diagnostics.Process.GetCurrentProcess().ProcessName.ToUpper() != "DEVENV")
            {
                this.Init();
            }
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
            System.Collections.ArrayList alDept = deptManager.GetDeptmentAll();
            if (alDept == null)
            {
                MessageBox.Show("��ȡ�����б�������");
                return;
            }

            this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);
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

            return base.OnQuery(sender, neuObject);
        }

        /// <summary>
        /// ��ȡ��λ�ձ���Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ��� ����ұ���Ϊ����ͳ��ȫԺ��λ�ձ���Ϣ</param>
        /// <returns></returns>
        protected int QueryHosDayReport()
        {
            this.lbDate.Text = "���ڣ�" + this.BeginDate.ToString("yyyy-MM-dd") + " �� " + this.EndDate.ToString("yyyy-MM-dd");
            this.lbDept.Text = "ͳ�Ʒ�Χ��ȫԺ";

            #region ��λ��ϸ ת�롢ת��

            string execDetailSql = @"select t.dept_code,t.stat_type,t.extend,count(*)
--select *
from met_cas_dayreport_detail t
where t.stat_date >= to_date('{0}','yyyy-mm-dd')
and   t.stat_date <= to_date('{1}','yyyy-mm-dd')
and   t.valid_state = '0'
group by t.dept_code,t.stat_type,t.extend
order by t.dept_code";

            execDetailSql = string.Format(execDetailSql, this.BeginDate.ToString("yyyy-MM-dd"), this.EndDate.ToString("yyyy-MM-dd"));

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

            string execTotSql = @"select s.dept_code,sum(s.in_normal + s.in_return),sum(s.out_normal + s.out_withdrawal),sum(s.in_transfer),
       sum(s.out_transfer),sum(s.bed_stand), (select t.beginning_num from met_cas_inpatientdayreport t 
             where  t.dept_code=s.dept_code and  t.date_stat=to_date('{0}','yyyy-mm-dd' )) as beginning_num,
       (select t.end_num from met_cas_inpatientdayreport t 
             where  t.dept_code=s.dept_code and  t.date_stat=to_date('{1}','yyyy-mm-dd'))as end_num
from   met_cas_inpatientdayreport s 
where  s.date_stat >= to_date('{0}','yyyy-mm-dd')
and    s.date_stat <= to_date('{1}','yyyy-mm-dd')
group by s.dept_code
order by s.dept_code";

            execTotSql = string.Format(execTotSql, this.BeginDate.ToString("yyyy-MM-dd"), this.EndDate.ToString("yyyy-MM-dd"));

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
                          

                        if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColInNum].Text=="0")
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColInNum].Text = "";
                        if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTot].Text == "0")
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTot].Text = "";
                        if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferIn].Text == "0")
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferIn].Text = "";
                        if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferOut].Text == "0")
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColTransferOut].Text ="";
                        if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColStandardNum].Text == "0")
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColStandardNum].Text = "";
                        if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColBeginNum].Text == "0")
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColBeginNum].Text ="";
                        if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColEndNum].Text == "0")
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColEndNum].Text ="";
                        //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutStat].Text == "0")
                        //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutStat].Text = "";
                        //if (this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutTot].Text == "0")
                        //    this.neuSpread1_Sheet1.Cells[i, (int)ColumnEnum.ColOutTot].Text = "";
                        }
                    }
                }
            }

            #endregion

            #region ��Ժ�ܴ�����


            string execOutStatSql = @"select  t.dept_code,sum(trunc(decode(s.IN_STATE,'N',s.oper_date,s.out_date)) - trunc(s.in_date))
from    met_cas_dayreport_detail t,fin_ipr_inmaininfo s
where   t.stat_date >= to_date('{0}','yyyy-mm-dd')
and     t.stat_date <= to_date('{1}','yyyy-mm-dd')
and     t.valid_state = '0'
and     t.stat_type in ('O','OF')
and     t.inpatient_no = s.inpatient_no
group by t.dept_code";

            execOutStatSql = string.Format(execOutStatSql, this.BeginDate.ToString("yyyy-MM-dd"),this.EndDate.ToString("yyyy-MM-dd"));

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

            #region �ϼ�ͳ��

            this.SetSum((int)ColumnEnum.ColDept, (int)ColumnEnum.ColStandardNum, (int)ColumnEnum.ColBeginNum, (int)ColumnEnum.ColInNum,
                (int)ColumnEnum.ColTransferIn, (int)ColumnEnum.ColTransferOut, (int)ColumnEnum.ColTot, (int)ColumnEnum.ColOutTot,
                (int)ColumnEnum.ColOut1, (int)ColumnEnum.ColOut2, (int)ColumnEnum.ColOut3, (int)ColumnEnum.ColOut4,
                (int)ColumnEnum.ColEndNum, (int)ColumnEnum.ColOutStat);

            #endregion

            return 1;
        }


        /// <summary>
        /// ���´�λ�ձ���Ϣ
        /// </summary>
        /// <returns></returns>
        protected int UpdateHostDayReport()
        {
            return 1;
        }



        
        /// <summary>
        /// ��Ӻϼ���
        /// </summary>
        /// <param name="iTextIndex">"�ϼƣ�"��������</param>
        /// <param name="iIndex">�����ϼƵ�������</param>
        public void SetSum(int iTextIndex, params int[] iIndex)
        {
            int iRowIndex = this.neuSpread1_Sheet1.Rows.Count;
            this.neuSpread1_Sheet1.Rows.Add(iRowIndex, 1);
            this.neuSpread1_Sheet1.Cells[iRowIndex, iTextIndex].Text = "�ϼƣ�";
            if (iRowIndex == 0)
                return;
            for (int i = 0; i < iIndex.Length; i++)
            {
                if (iIndex[i] >= this.neuSpread1_Sheet1.Columns.Count)
                    continue;
                this.neuSpread1_Sheet1.Cells[iRowIndex, iIndex[i]].Formula = "SUM(" + (char)(65 + iIndex[i]) + "1:" + (char)(65 + iIndex[i]) + iRowIndex.ToString() + ")";
            }

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
            /// ��Ժ����ͳ��
            /// </summary>
            ColOutStat,
            /// <summary>
            /// ���ұ���
            /// </summary>
            ColDeptCode
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            print.PrintPage(30, 10, this);

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
