using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.Models.Base;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class ucDiagNoseQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private SubSpecManage subMgr = new SubSpecManage();
        private SpecSourceManage sorMgr = new SpecSourceManage();
        private string sql = "";

        public DateTime StartTime
        {
            get
            {
                return dtpStart.Value;
            }
        }

        public DateTime EndTime
        {
            get
            {
                return dtpEnd.Value;
            }
        }

        private bool dataFromGetDiag = false;
        public bool DataFromGetDiag
        {
            get
            {
                return dataFromGetDiag;
            }
            set
            {
                dataFromGetDiag = value;
            }
 
        }

        private bool btnVisible = false;
        public bool BtnVisible
        {
            set
            {
                btnVisible = value;
            }
        }

        //public delegate void SetContainerId(object sender, EventArgs e);
        //public event SetContainerId OnSetContainerId;

        public ucDiagNoseQuery()
        {
            InitializeComponent();
        }

        private void SetSql()
        {
            sql = @"select  s.INPATIENT_NO 住院流水号, p.NAME 病人姓名, (case o.SEX_CODE when 'F' then '女' else '男' end) 性别,
                    (select substr(ss.SUBBARCODE,1,7) from SPEC_SUBSPEC ss where ss.SPECID = s.SPECID fetch first 1 rows only) 标本号,
                    (case s.ORGORBLOOD when 'B' then '血' when 'O' then '组织' else '' end) 标本种类,                     
                    std.DISEASENAME 病种,  (case s.ISINBASE when '1' then '已录' else '未录' end) 录入情况,
                     sd.INBASETIME  录入时间, s.OPERTIME 操作时间,   ( case nvl(r.c1,0) when 0 then '无' else '有' end) 病案库诊断,
                      (case o.IN_STATE when 'O' then '出院' when 'B' then '出院登记' else '在院' end) 在院状态,OUT_DATE 出院日期,r.OPER_DATE 病案录入日期
                     from SPEC_SOURCE s left join FIN_IPR_INMAININFO o on s.INPATIENT_NO = o.INPATIENT_NO
                     left join SPEC_DIAGNOSE sd on sd.SPECID = s.SPECID
                     left join 
                     (
                     select count(*) c1, d.INPATIENT_NO, d.OPER_DATE
                     from  MET_CAS_DIAGNOSE d ,SPEC_SOURCE s
                     where s.INPATIENT_NO = d.INPATIENT_NO and d.OPER_TYPE = '2' group by d.INPATIENT_NO,OPER_DATE
                     ) r
                     on s.INPATIENT_NO = r.INPATIENT_NO
                     left join SPEC_DISEASETYPE std on s.DISEASETYPEID = std.DISEASETYPEID
					 left join SPEC_PATIENT p on s.PATIENTID = p.PATIENTID
                     where s.SPECID >0 ";
        }

        private string GenerateSql()
        {
            SetSql();
            if (chkAll.Checked)
            {
                return sql;
            }
            string start = dtpStart.Value.Date.ToString();
            string end = dtpEnd.Value.Date.AddDays(1).ToString();
            string cardNo=" ";
            string notIn = chkNotIn.Checked ? "0" : "1";
            sql += " and s.OPERTIME between '{0}' and '{1}'";
            
            if (txtCardNo.Text.Trim() != "")
            {
                cardNo = txtCardNo.Text.Trim().PadLeft(10, '0');
                sql += " and substr(s.INPATIENT_NO,5,10) = '{2}'";
            }
            
             sql += " and s.ISINBASE = '{3}' ";
            
            return string.Format(sql, new string[] { start, end, cardNo, notIn });

        }

        public void Query()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("正在查询诊断信息，请稍候...");

            string tmpSql = GenerateSql();
            DataSet ds = new DataSet();
            subMgr.ExecQuery(tmpSql, ref ds);
            neuSpread1_Sheet1.DataSource = ds;
            neuSpread1_Sheet1.Columns[0].Width = 130;
            neuSpread1_Sheet1.ColumnHeader.Rows[0].Height = 40;
            for (int i = 1; i < neuSpread1_Sheet1.ColumnCount; i++)
            {
                neuSpread1_Sheet1.Columns[i].Width = 85;
                neuSpread1_Sheet1.Columns[i].AllowAutoFilter = true;
                neuSpread1_Sheet1.Columns[i].AllowAutoSort = true;
            }
            lblCount.Text = "记录: 共" + neuSpread1_Sheet1.RowCount.ToString() + "条";
            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        public override int Query(object sender, object neuObject)
        {

            this.Query();
            return base.Query(sender, neuObject);
        }

        private void btnGetData_Click(object sender, EventArgs e)
        {
            dataFromGetDiag = true;
            //OnSetContainerId(this, new EventArgs());
        }

        private void ucDiagNoseQuery_Load(object sender, EventArgs e)
        {
            btnGetData.Visible = btnVisible;
        }
    }
}
