using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class ucSumQuery :  FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private string statSql = "";
        private string bldSql = "";
        private string orgSql = "";
        private string mctSql = "";
        private string mctDet = "";

        private FS.HISFC.BizLogic.Speciment.SubSpecManage subMgr = new FS.HISFC.BizLogic.Speciment.SubSpecManage();

        public ucSumQuery()
        {
            InitializeComponent();
        }

        /// <summary>
        /// sql���
        /// </summary>
        private void SetSql()
        {
            statSql = @"select type ����, DISEASENAME ����,count(*) ����
                      from
                      (
                      select distinct s2.SPECID, dt.DISEASENAME , case s2.ORGORBLOOD when 'O' then '��֯' else 'Ѫ' end type
                      from SPEC_SOURCE s2
                      left join SPEC_DISEASETYPE dt on s2.DISEASETYPEID = dt.DISEASETYPEID
                      where s2.SENDDATE between '{0}' and '{1}'
                      ) a
                      group by a.DISEASENAME,a.type
                      order by  a.type,a.DISEASENAME";
            bldSql = @"select count(*) from SPEC_SOURCE s2 
                      where s2.SENDDATE between '{0}' and '{1}'
                      and s2.ORGORBLOOD = 'B'
                      ";
            orgSql = @"select count(*)
                      from
                      (
                      select distinct s2.SPEC_NO, s2.DISEASETYPEID
                      from SPEC_SOURCE s2
                      where s2.SENDDATE between  '{0}' and '{1}'
                      and s2.ORGORBLOOD = 'O'
                      )a";
            mctSql =  @"select count(*)
                      from
                      (
                      select distinct s2.SPEC_NO, s2.DISEASETYPEID
                      from SPEC_SOURCE s2
                      where s2.SENDDATE between  '{0}' and '{1}'
                      and s2.ORGORBLOOD = 'O' and s2.MATCHFLAG ='1'
                      )a";
            mctDet = @"select  DISEASENAME ����, count(*) ����
                      from
                      (
                      select distinct s2.SPEC_NO, dt.DISEASENAME 
                      from  SPEC_SOURCE s2
                      left join SPEC_DISEASETYPE dt on s2.DISEASETYPEID = dt.DISEASETYPEID
                      where s2.SENDDATE between  '{0}' and '{1}'
                      and s2.ORGORBLOOD = 'O'
                      and s2.MATCHFLAG ='1'
                      ) a
                      group by a.DISEASENAME 
                      order by a.DISEASENAME";
        }

        private void Query()
        {
            SetSql();
            neuSpread1_Sheet1.RowCount = 0;
            neuSpread1_sheetView2.RowCount = 0;

            string start = dtpStartDate.Value.Date.ToString();
            string end = dtpEndTime.Value.Date.AddDays(1).Date.ToString();

            string tmp1 = string.Format(statSql, start, end);
            string tmp2 = string.Format(bldSql, start, end);
            string tmp3 = string.Format(orgSql, start, end);
            string tmp4 = string.Format(mctSql, start, end);
            string tmp5 = string.Format(mctDet, start, end);

            DataSet ds1 = new DataSet();
            DataSet ds5 = new DataSet();

            subMgr.ExecQuery(tmp1, ref ds1);
            subMgr.ExecQuery(tmp5, ref ds5);

            lblBld.Text = "Ѫ�걾��: " + subMgr.ExecSqlReturnOne(tmp2) + " ��";
            lblOrg.Text = "��֯�걾��: " + subMgr.ExecSqlReturnOne(tmp3) + " ��";
            lblMatch.Text = "�������: " + subMgr.ExecSqlReturnOne(tmp4) + " ��";            

            neuSpread1_Sheet1.DataSource = ds1;
            neuSpread1_sheetView2.DataSource = ds5;

            for (int i = 0; i < neuSpread1_sheetView2.ColumnCount; i++)
            {
                neuSpread1_sheetView2.Columns[i].AllowAutoFilter = true;
                neuSpread1_sheetView2.Columns[i].AllowAutoSort = true;
            }
            for (int i = 0; i < neuSpread1_Sheet1.ColumnCount; i++)
            {
                neuSpread1_Sheet1.Columns[i].AllowAutoFilter = true;
                neuSpread1_Sheet1.Columns[i].AllowAutoSort = true;
            }
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            try
            {
                this.Query();
            }
            catch
            { }
            return base.OnQuery(sender, neuObject);
        }

        public override int Export(object sender, object neuObject)
        { 
            string path = string.Empty;
            SaveFileDialog saveFileDiaglog = new SaveFileDialog();

            saveFileDiaglog.Title = "���������ѡ��Excel�ļ�����λ��";
            saveFileDiaglog.RestoreDirectory = true;
            saveFileDiaglog.InitialDirectory = Application.StartupPath;
            saveFileDiaglog.Filter = "Excel files (*.xls)|*.xls";
            saveFileDiaglog.FileName = DateTime.Now.ToShortDateString() + "�걾";
            DialogResult dr = saveFileDiaglog.ShowDialog();

            if (dr == DialogResult.OK)
            {
                neuSpread1.SaveExcel(saveFileDiaglog.FileName, FarPoint.Excel.ExcelSaveFlags.NoFlagsSet);
            }
                 
             
            return base.Export(sender, neuObject);
        }
    }
}
