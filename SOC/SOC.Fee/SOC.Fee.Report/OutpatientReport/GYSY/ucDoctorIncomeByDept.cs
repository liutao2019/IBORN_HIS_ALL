using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace SOC.Fee.Report.OutpatientReport.GYSY
{
    public partial class ucDoctorIncomeByDept : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucDoctorIncomeByDept()
        {
            InitializeComponent();
        }

        Neusoft.HISFC.BizLogic.Manager.Constant conMgr = new Neusoft.HISFC.BizLogic.Manager.Constant();
        Neusoft.SOC.HISFC.BizProcess.Fee.Report.InpatientFee inpatientReport = new Neusoft.SOC.HISFC.BizProcess.Fee.Report.InpatientFee();
        Neusoft.FrameWork.Management.DataBaseManger DSManager = new Neusoft.FrameWork.Management.DataBaseManger();       
        
        DateTime dtBegin = DateTime.MinValue;
        DateTime dtEnd = DateTime.MinValue;
        DataSet ds;
        string sql, Result;
        string strFrom = ""; //开始时间
        string strTo = ""; //结束时间

        //查询
        private void btnQuery_Click(object sender, EventArgs e)
        {
            strFrom = this.dtpFrom.Text.ToString();
            strTo = this.dtpTo.Text.ToString();
            this.lbTime.Text = strFrom + " 到 " + strTo;
            ds = new DataSet();       
            try
            {
                if (DSManager.Sql.GetSql("SOC.OutpatientReport.DoctorIncomeByDept", ref sql) == -1)
                {
                    MessageBox.Show("获取查询数据的sql出错，请与系统管理员联系并报告错误：");
                    return;
                } 
                sql = string.Format(sql, strFrom, strTo);
                ds = null;
                ds = new DataSet();
                if (DSManager.ExecQuery(sql, ref ds) == -1)
                {                    
                    MessageBox.Show("执行门诊sql出错" + DSManager.Err);
                    return;
                }
            }
            catch (Exception ee)
            {                
                Result = ee.Message;
                MessageBox.Show(Result);
                return;
            }            
            this.neuSpread1.DataSource = ds;
            ShowData();
        }
              
        //导出
        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = ".xls|*.*";
            sfd.FilterIndex = 1;
            sfd.OverwritePrompt = true;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName + ".xls"))
                {
                    if (MessageBox.Show("已有相同的文件，是否覆盖？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.neuSpread1.SaveExcel(sfd.FileName + ".xls", FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                    }
                    else
                    {
                        sfd.Dispose();
                    }
                }
                else
                {
                    this.neuSpread1.SaveExcel(sfd.FileName + ".xls", FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                }
            }
        }

        private void ucDoctorIncomeByDept_Load(object sender, EventArgs e)
        {
            dtEnd = conMgr.GetDateTimeFromSysDateTime();
            dtBegin = dtEnd.AddDays(-1);
            this.dtpFrom.Value = dtBegin;
            this.dtpTo.Value = dtEnd;
        }

        private void ShowData()
        {
            int rowTost = this.ds.Tables[0].Rows.Count;       //总行数
            int columnTost = this.ds.Tables[0].Columns.Count;    //总列数
            this.neuSpread1_Sheet1.AddColumns(columnTost, 1);  //增加合计列
            this.neuSpread1_Sheet1.Columns[columnTost].Label = "合计";            
            string Dept = this.neuSpread1_Sheet1.Cells[0, 0].Value.ToString();        //第一行的科室
            int temRowIndex = 1;
      
            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {             
                this.neuSpread1_Sheet1.Cells[i, columnTost].Formula = string.Format("sum(C{0}:N{0})", i+1);
                string newDept = this.neuSpread1_Sheet1.Cells[i, 0].Value == null ? "" : this.neuSpread1_Sheet1.Cells[i, 0].Value.ToString();
                if (newDept != Dept)
                {
                    Dept = newDept;
                    this.neuSpread1_Sheet1.AddRows(i, 1);  //如果科室不相同则增加小计行         
                    this.neuSpread1_Sheet1.Cells[i, 1].Value = "小计";                    
                    this.neuSpread1_Sheet1.Cells[i, 2].Formula = string.Format("sum(C{0}:C{1})", temRowIndex,i);
                    this.neuSpread1_Sheet1.Cells[i, 3].Formula = string.Format("sum(D{0}:D{1})", temRowIndex, i);
                    this.neuSpread1_Sheet1.Cells[i, 4].Formula = string.Format("sum(E{0}:E{1})", temRowIndex, i);
                    this.neuSpread1_Sheet1.Cells[i, 5].Formula = string.Format("sum(F{0}:F{1})", temRowIndex, i);
                    this.neuSpread1_Sheet1.Cells[i, 6].Formula = string.Format("sum(G{0}:G{1})", temRowIndex, i);
                    this.neuSpread1_Sheet1.Cells[i, 7].Formula = string.Format("sum(H{0}:H{1})", temRowIndex, i);
                    this.neuSpread1_Sheet1.Cells[i, 8].Formula = string.Format("sum(I{0}:I{1})", temRowIndex, i);
                    this.neuSpread1_Sheet1.Cells[i, 9].Formula = string.Format("sum(J{0}:J{1})", temRowIndex, i);
                    this.neuSpread1_Sheet1.Cells[i, 10].Formula = string.Format("sum(K{0}:K{1})", temRowIndex, i);
                    this.neuSpread1_Sheet1.Cells[i, 11].Formula = string.Format("sum(L{0}:L{1})", temRowIndex, i);
                    this.neuSpread1_Sheet1.Cells[i, 12].Formula = string.Format("sum(M{0}:M{1})", temRowIndex, i);
                    this.neuSpread1_Sheet1.Cells[i, 13].Formula = string.Format("sum(N{0}:N{1})", temRowIndex, i);
                    this.neuSpread1_Sheet1.Cells[i, 14].Formula = string.Format("sum(O{0}:O{1})", temRowIndex, i);
                    temRowIndex = i+2;
                }                
            }
        }
    }
}
