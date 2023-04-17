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

namespace FS.HISFC.Components.Speciment.DataOper
{
    public partial class ucOrgColTime : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private string sql = "";
        private SpecSourceManage specSourceManage = new SpecSourceManage();

        public ucOrgColTime()
        {
            InitializeComponent();
        }

        private void SetSql()
        {
            sql = @"
                   select distinct    s.specid,s.HISBARCODE Դ�걾����,s.INPATIENT_NO סԺ��ˮ��,
                   s.NAME ����, s.GENDER �Ա�, s.SUBBARCODE �걾�� ,s.SPECIMENTNAME �걾����,
                   date(s.STORETIME) ȡ��ʱ�� , hour(s.STORETIME) ʱ,  minute(s.STORETIME)��, s.SPECTYPEID
                   from
                    (
                      select   distinct s.specid,s.HISBARCODE ,s.INPATIENT_NO ,
                      p.NAME , (case p.GENDER when 'F' then 'Ů' else '��' end) GENDER, 
 	                  (select substr(ss.SUBBARCODE,1,6) from SPEC_SUBSPEC ss where ss.SPECID = s.SPECID fetch first 1 rows only) SUBBARCODE,
                      t.SPECIMENTNAME ,
                                     (select max(sT1.STORETIME) from SPEC_SOURCE_STORE st1 
									  where st1.SPECID = st.SPECID and st1.SPECTYPEID = st.SPECTYPEID 
									  group by st1.SPECID,st1.SPECTYPEID,sT1.STORETIME fetch first 1 rows only) STORETIME, st.SPECTYPEID
	               from SPEC_SOURCE s join  SPEC_PATIENT p on s.PATIENTID = p.PATIENTID 
				   join SPEC_SOURCE_STORE st on s.SPECID = st.SPECID
				   join SPEC_TYPE t on st.SPECTYPEID = t.SPECIMENTTYPEID									 
  	               where s.OrgOrBlood = 'O'  and s.OPERTIME between '{0}' and '{1}'
				    ) s
                    order by Դ�걾����
				   ";
        }

        private void Query()
        {
            SetSql();
            neuSpread1_Sheet1.RowCount = 0;
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
                neuSpread1_Sheet1.Columns[0].Visible = false;
                neuSpread1_Sheet1.Columns[neuSpread1_Sheet1.ColumnCount - 1].Visible = false;
                for (int i = 0; i < neuSpread1_Sheet1.ColumnCount; i++)
                {
                    neuSpread1_Sheet1.Columns[i].Width = 100;
                }
                neuSpread1_Sheet1.Columns[1].Width = 150;
                neuSpread1_Sheet1.Columns[2].Width = 150;
                for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                {
                    neuSpread1_Sheet1.Rows[i].Height = 28;
                }
            }
                
            catch
            { }
        }

        private int Save(string time, string specid, string specTypeId)
        {
            string sql = @" update SPEC_SOURCE_STORE set STORETIME = '{0}' where specid = {1} and SPECTYPEID = {2}";
            string uSql = string.Format(sql, new string[] { time, specid, specTypeId });
            int result = specSourceManage.ExecNoQuery(uSql);
            return result;
        }

        private void GetData()
        {
            string barCode = txtBarCode.Text.Trim().PadLeft(12, '0');
            //SpecSource tmp = new SpecSource();
            //tmp = specSourceManage.GetSource("", barCode);
            //if (tmp == null)
            //    return;
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

            for (int i = neuSpread1_Sheet1.RowCount - 1; i >0; i--)
            {
                string str = neuSpread1_Sheet1.Cells[i, 1].Text.Trim();
                if (str == barCode)
                {
                    neuSpread1.Focus();
                    neuSpread1_Sheet1.Rows[i].BackColor = Color.Aqua;
                    neuSpread1_Sheet1.SetActiveCell(i, 7);
                    //break;
                }
            }
            txtBarCode.Text = "";
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

        public override int Save(object sender, object neuObject)
        {
            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {
                string specId = neuSpread1_Sheet1.Cells[i, 0].Text.Trim();
                string specTypeId = neuSpread1_Sheet1.Cells[i, neuSpread1_Sheet1.ColumnCount - 1].Text.Trim();
                string min = neuSpread1_Sheet1.Cells[i, 9].Text.Trim().PadLeft(2, '0');
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
                    MessageBox.Show("�� " + i.ToString() + " �У� ���Ӹ�ʽ����");
                    continue;
                }

                string hour = neuSpread1_Sheet1.Cells[i, 8].Text.Trim().PadLeft(2, '0');
                try
                {
                    int tmpHour = Convert.ToInt32(hour);
                }
                catch
                {
                    MessageBox.Show("�� " + i.ToString() + " �У� ʱ�Ӹ�ʽ����");
                    continue;
                }
                string date = neuSpread1_Sheet1.Cells[i, 7].Text.Trim();
                string sendTime = date + " " + hour + ":" + min + ":" + "00";
                try
                {
                    DateTime dt = Convert.ToDateTime(sendTime);
                }
                catch
                {
                    MessageBox.Show("�� " + (i + 1).ToString() + " �У� ʱ���ʽ����");
                    continue;
                }
                if (this.Save(sendTime, specId, specTypeId) > 0)
                {
                    neuSpread1_Sheet1.Rows[i].Visible = false;
                }
            }
            return base.Save(sender, neuObject);
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
                if (col == 9)
                {
                    if (row + 1 < neuSpread1_Sheet1.RowCount)
                    {
                        neuSpread1_Sheet1.SetActiveCell(row + 1, 7);
                    }
                    neuSpread1_Sheet1.Rows[row].BackColor = Color.SkyBlue;
                }
                if (col == 8)
                {
                    neuSpread1_Sheet1.SetActiveCell(row, 9);
                }
                if (col == 7)
                {
                    neuSpread1_Sheet1.SetActiveCell(row, 8);
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        private void ucOrgColTime_Load(object sender, EventArgs e)
        {
            this.Query();
        }
    }
}
