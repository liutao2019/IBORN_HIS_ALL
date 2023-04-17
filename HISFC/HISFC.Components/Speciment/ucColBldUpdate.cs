using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using System.Collections;

namespace FS.HISFC.Components.Speciment
{
    /// <summary>
    /// 说明：该页面用于标本库统一录入采血时间，他们认为在血标本条码录入页面输入采血时间太慢，所以需要批量处理该时间
    /// 1. 首先读取当日操作的标本
    /// 2. 在条码处输入条码，然后找到输入的条码，字体标识为蓝色
    /// </summary>
    public partial class ucColBldUpdate : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private SpecSourceManage specSourceManage = new SpecSourceManage();
        private string sql = "";

        public ucColBldUpdate()
        {
            InitializeComponent();
        }

        private void Query()
        {
            neuSpread1_Sheet1.Rows.Count = 0;
            sql =
                @"  select s.HISBARCODE 源标本条码, s.INPATIENT_NO 住院流水号,
                    p.NAME 姓名, (case p.GENDER when 'F' then '女' else '男' end) 性别, 
 	                (select substr(ss.SUBBARCODE,1,6) from SPEC_SUBSPEC ss where ss.SPECID = s.SPECID fetch first 1 rows only) 标本号 ,
                    date(s.SENDDATE) 采血日期 , hour(s.SENDDATE) 时,  minute(s.SENDDATE)分
	                 from SPEC_SOURCE s join  SPEC_PATIENT p on s.PATIENTID = p.PATIENTID  
  	                 where s.OrgOrBlood = 'B'  and s.OPERTIME between '{0}' and '{1}'";  
            string start = dtStart.Value.Date.ToString();
            string end = dtEnd.Value.AddDays(1.0).Date.ToString();
            DataSet ds = new DataSet();
            string tmpSql = string.Format(sql, new string[] { start, end });
            specSourceManage.ExecQuery(tmpSql, ref ds);
            if (ds == null || ds.Tables.Count <= 0)
                return;
            neuSpread1_Sheet1.DataSource = ds.Tables[0];
            try
            {
                neuSpread1_Sheet1.AutoGenerateColumns = true;
            }
            catch
            { }
        }

        private int Save(string time, string hisbarcode)
        {
            string sql = @" update SPEC_SOURCE set SENDDATE = '{0}' where HISBARCODE = '{1}'";
            string uSql = string.Format(sql, new string[] { time, hisbarcode });
            int result = specSourceManage.ExecNoQuery(uSql);
            return result;
        }

        private void GetData()
        {
            string barCode = txtBarCode.Text.Trim().PadLeft(12, '0');
            SpecSource tmp = new SpecSource();
            tmp = specSourceManage.GetSource("", barCode);
            if (tmp == null)
                return;
            FarPoint.Win.Spread.FilterColumnDefinitionCollection fcdc = new FarPoint.Win.Spread.FilterColumnDefinitionCollection();
            FarPoint.Win.Spread.FilterColumnDefinition fcd = new FarPoint.Win.Spread.FilterColumnDefinition(1, FarPoint.Win.Spread.FilterListBehavior.Default);

            fcdc.Add(fcd);
            FarPoint.Win.Spread.NamedStyle instyle = new FarPoint.Win.Spread.NamedStyle();
            FarPoint.Win.Spread.NamedStyle outstyle = new FarPoint.Win.Spread.NamedStyle();
            instyle.BackColor = Color.Yellow;
            outstyle.BackColor = Color.Gray;
            FarPoint.Win.Spread.StyleRowFilter rf = new FarPoint.Win.Spread.StyleRowFilter(neuSpread1_Sheet1, instyle, outstyle);

            try
            {
                neuSpread1_Sheet1.AutoFilterColumn(0, barCode, 0);
            }
            catch
            { }

            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                string str = neuSpread1_Sheet1.Cells[i, 0].Text.Trim();
                if (str == barCode)
                {
                    neuSpread1.Focus();
                    neuSpread1_Sheet1.Rows[i].BackColor = Color.Aqua;
                    neuSpread1_Sheet1.SetActiveCell(i, 5);
                    break;
                }
            }
        }

        private void txt_Key(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetData();
            }
        }

        public override int Query(object sender, object neuObject)
        {
            this.Query();
            return base.Query(sender, neuObject);
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Query();
            base.OnLoad(e);
        }

        public override int Save(object sender, object neuObject)
        {
            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                string hisbarCode = neuSpread1_Sheet1.Cells[i, 0].Text.Trim();
                string min = neuSpread1_Sheet1.Cells[i, 7].Text.Trim().PadLeft(2,'0');
                if (!neuSpread1_Sheet1.Rows[i].Visible)
                {
                    continue;
                }
                try
                {
                    int tmpMin = Convert.ToInt32(min);
                }
                catch
                {
                    MessageBox.Show("第 " + i.ToString() + " 行， 分钟格式错误！");
                    continue;
                }

                string hour = neuSpread1_Sheet1.Cells[i, 6].Text.Trim().PadLeft(2,'0');
                try
                {
                    int tmpHour = Convert.ToInt32(hour);
                }
                catch
                {
                    MessageBox.Show("第 " + i.ToString() + " 行， 时钟格式错误！");
                    continue;
                }
                string date = neuSpread1_Sheet1.Cells[i, 5].Text.Trim();
                string sendTime = date + " " + hour + ":" + min + ":" + "00";
                try
                {
                    DateTime dt = Convert.ToDateTime(sendTime);
                }
                catch
                {
                    MessageBox.Show("第 " + (i+1).ToString() + " 行， 时间格式错误！");
                    continue;
                }
                if (this.Save(sendTime, hisbarCode) > 0)
                {
                    neuSpread1_Sheet1.Rows[i].Visible = false;
                }
            }
            return base.Save(sender, neuObject);
        }

        private void neuSpread1_KeyDown(object sender, KeyEventArgs e)
        { 
        }

        private void neuSpread1_EditChange(object sender, FarPoint.Win.Spread.EditorNotifyEventArgs e)
        {
          
        }

        private void neuSpread1_LeaveCell(object sender, FarPoint.Win.Spread.LeaveCellEventArgs e)
        {

        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            neuSpread1.SetInputMap();
             if (keyData == Keys.Enter)
            {
                int col = neuSpread1_Sheet1.ActiveColumn.Index; ;
                int row = neuSpread1_Sheet1.ActiveRow.Index;
                if (col == -1 || row == -1)
                {
                    return false;
                }
                if (col == 7)
                {
                    if (row + 1 < neuSpread1_Sheet1.RowCount)
                    {
                        neuSpread1_Sheet1.SetActiveCell(row + 1, 5);
                    }
                    neuSpread1_Sheet1.Rows[row].BackColor = Color.SkyBlue;
                }
                if (col == 6)
                {
                    neuSpread1_Sheet1.SetActiveCell(row, 7);
                }
                if (col == 5)
                {
                    neuSpread1_Sheet1.SetActiveCell(row, 6);
                }
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
