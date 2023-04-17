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

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class ucCheckLoc : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        private SubSpecManage subMgr = new SubSpecManage();
        private string sqlLocIndex = "";
        private DataSet ds = new DataSet();
        private DataSet dsBox = new DataSet();
        private bool init = false;

        /// <summary>
        /// 设置打印机名字
        /// </summary>
        private string printer = "";

        public string Printer
        {
            get
            {
                return printer;
            }
            set
            {
                printer = value;
            }
        }
        
        /// <summary>
        /// 每个病种的最后一个标本号 sql索引
        /// </summary>
        public string SqlLocIndex
        {
            get
            {
                return sqlLocIndex;
            }
            set
            {
                sqlLocIndex = value;
            }
        }

        private string sqlAllIndex = "";
        /// <summary>
        /// 所有标本按时间段，按病种，按标本类型查询的sql 索引
        /// </summary>
        public string SqlAllIndex
        {
            get
            {
                return sqlAllIndex;
            }
            set
            {
                sqlAllIndex = value;
            }
        }

        private string sqlChkByBox = "";

        /// <summary>
        /// 按标本盒显示的sql索引
        /// </summary>
        public string SqlChkByBox
        {
            get
            {
                return sqlChkByBox;
            }
            set
            {
                sqlChkByBox = value;
            }
        }

        /// <summary>
        /// 如果是修改标本盒名称，tabpage1 可以不显示
        /// </summary>
        private bool specVisible = true;
        public bool SpecVisible
        {
            get
            {
                return specVisible;
            }
            set
            {
                specVisible = value;
            }
        }

        private void Filter()
        {
            
            string filter = "";
            string text = cmbDiseaseType.Text.Trim();
            string type = cmbSpecType.Text.Trim();
            if (text == "" && type == "")
            {
                return;
            }
            if (text != "")
            {
                if (text != "全部")
                {
                    filter = "(病种 like '%" + text.Trim() + "%')";
                }
                else
                {
                    filter = "(病种 <> '')";
                }
            }
            if (type != "")
            {
                if (type != "全部")
                {
                    filter += " and (标本类型 like '%" + type.Trim() + "%')";
                }
                else
                {
                    filter += " and (标本类型 <> '')";
                }
            }
            try
            {
                if (tabResult.SelectedTab == tpSpec)
                {
                    DataView dv = new DataView(ds.Tables[0]);
                    dv.RowFilter = filter;
                    BindingData(dv);
                }
                if (tabResult.SelectedTab == tpBox)
                {
                    DataView dv = new DataView(dsBox.Tables[0]);
                    dv.RowFilter = filter;
                    BindingDataBox(dv);
                }
            }
            catch { }
        }

        private void BindingDataBox(DataView dv)
        {
            neuSpread1_Sheet1.RowCount = 0;
            neuSpread1_Sheet1.DataSource = dv;

            neuSpread1_Sheet1.Columns[4].Visible = false;
            //string curBoxid = neuSpread1_Sheet1.Cells[0, 4].Text.Trim();
            try
            {
                for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
                {

                    string dis = neuSpread1_Sheet1.Cells[i, 2].Text.Trim();
                    string type = neuSpread1_Sheet1.Cells[i, 3].Text.Trim();
                    if (!cmbSpecType.Items.Contains(type))
                    {
                        cmbSpecType.Items.Add(type);
                    }
                    if (!cmbDiseaseType.Items.Contains(dis))
                    {
                        cmbDiseaseType.Items.Add(dis);
                    }
                }//neuSpread1_Sheet1
                neuSpread1_Sheet1.Columns[0].Width = 80;
                neuSpread1_Sheet1.Columns[1].Width = 330;
                neuSpread1_Sheet1.Columns[2].Width = 60;
                neuSpread1_Sheet1.Columns[3].Width = 60;
                neuSpread1_Sheet1.Columns[4].Width = 10;
                neuSpread1_Sheet1.Columns[5].Width = 80;
                neuSpread1_Sheet1.Columns[6].Width = 80;
                neuSpread1_Sheet1.Columns[4].Visible = false;
            }
            catch
            { }
        }

        private void BindingData(DataView dv)
        {
            neuSpread2_Sheet1.RowCount = 0;
            neuSpread2_Sheet1.DataSource = dv;
            
            neuSpread2_Sheet1.Columns[4].Visible = false;
            string curBoxid = neuSpread2_Sheet1.Cells[0, 4].Text.Trim();
            try
            {
                for (int i = 0; i < neuSpread2_Sheet1.RowCount; i++)
                {

                    string dis = neuSpread2_Sheet1.Cells[i, 1].Text.Trim();
                    string type = neuSpread2_Sheet1.Cells[i, 2].Text.Trim();
                    if (!cmbSpecType.Items.Contains(type))
                    {
                        cmbSpecType.Items.Add(type);
                    }
                    if (!cmbDiseaseType.Items.Contains(dis))
                    {
                        cmbDiseaseType.Items.Add(dis);
                    }
                   
                    string nextBoxId = "";
                    if (i + 1 < neuSpread2_Sheet1.RowCount)
                    {
                        nextBoxId = neuSpread2_Sheet1.Cells[i + 1, 4].Text.Trim();
                    }
                    if (curBoxid == nextBoxId)
                    {
                        neuSpread2_Sheet1.Cells[i + 1, 3].ForeColor = Color.White;//neuSpread2_Sheet1.Rows[i + 1].BackColor;
                        neuSpread2_Sheet1.Cells[i + 1, 1].ForeColor = neuSpread2_Sheet1.Rows[i + 1].BackColor;
                        neuSpread2_Sheet1.Cells[i + 1, 2].ForeColor = neuSpread2_Sheet1.Rows[i + 1].BackColor;
                    }
                    else
                    {
                        curBoxid = nextBoxId;
                    }
                }
                //init = true;
                cmbDiseaseType.Text = "";
                cmbSpecType.Text = "";
                neuSpread2_Sheet1.Columns[0].Width = 120;
                neuSpread2_Sheet1.Columns[1].Width = 60;
                neuSpread2_Sheet1.Columns[2].Width = 60;
                neuSpread2_Sheet1.Columns[3].Width = 330;
                neuSpread2_Sheet1.Columns[4].Width = 10;
                neuSpread2_Sheet1.Columns[5].Width = 30;
                neuSpread2_Sheet1.Columns[6].Width = 30;
            }
            catch
            { }
        }

        private void Query()
        {
            //tabResult.SelectedIndex = 0;
            neuSpread1_Sheet1.RowCount = 0;
            string tmpSql = @"select db2_sql from COM_SQL where ID = '{0}'";
            if (specVisible)
            {
                ds = new DataSet();
                if (rbtAll.Checked)
                {
                    tmpSql = string.Format(tmpSql, sqlAllIndex);
                    tmpSql = subMgr.ExecSqlReturnOne(tmpSql);
                    tmpSql = string.Format(tmpSql, dtpStartDate.Value.Date.ToString(), dtpEndTime.Value.Date.AddDays(1).ToString());
                }
                if (rbtLocLast.Checked)
                {
                    tabResult.SelectedTab = tpSpec;
                    tmpSql = string.Format(tmpSql, sqlLocIndex);
                    tmpSql = subMgr.ExecSqlReturnOne(tmpSql);
                    tmpSql = string.Format(tmpSql);
                }
                subMgr.ExecQuery(tmpSql, ref ds);
                if (ds != null && ds.Tables.Count > 0)
                {
                    BindingData(ds.Tables[0].DefaultView);
                }
            }

            //if (tabResult.SelectedTab == tpBox)
            //{
                //if (rbtAll.Checked)
                //{
                tmpSql = @"select db2_sql from COM_SQL where ID = '{0}'";
                    tmpSql = string.Format(tmpSql, sqlChkByBox);
                    tmpSql = subMgr.ExecSqlReturnOne(tmpSql);
                    tmpSql = string.Format(tmpSql, dtpStartDate.Value.Date.ToString(), dtpEndTime.Value.Date.AddDays(1).ToString());
                    dsBox = new DataSet();
                //}
                subMgr.ExecQuery(tmpSql, ref dsBox);
                if (dsBox != null && dsBox.Tables.Count > 0)
                {
                    BindingDataBox(dsBox.Tables[0].DefaultView);
                }
            //}

        }

        public ucCheckLoc()
        {
            InitializeComponent();
        }

        public override int Query(object sender, object neuObject)
        {
            tabResult.SelectedIndex = 0;
            this.Query();
            return base.Query(sender, neuObject);
        }

        private void ucCheckLoc_Load(object sender, EventArgs e)
        {
            try
            {
                label11.Visible = specVisible;
                label1.Visible = specVisible;
                dtpEndTime.Visible = specVisible;
                dtpStartDate.Visible = specVisible;
                rbtAll.Visible = specVisible;
                rbtLocLast.Visible = specVisible;
                if (!specVisible)
                {
                    tabResult.TabPages[1].Dispose();//.Width = 0;
                    tpBox.Text = "修改标本盒名称";
                    dtpEndTime.Value = DateTime.Now;
                    dtpStartDate.Value = new DateTime (1999,2,1);
                    //tpBox.Text = "";
                }
                this.tabResult.SelectedIndexChanged += new System.EventHandler(this.Tab_IndexChange);
                cmbSpecType.Items.Add("全部");
                cmbDiseaseType.Items.Add("全部");
                Query();
            }
            catch
            { }
        }

        private void cmbDiseaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbDiseaseType.Text.Trim() != "")
            {
                this.Filter();
            }
        }

        private void cmbSpecType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbSpecType.Text.Trim() != "")
            {
                this.Filter();

            }
        }

        public override int Export(object sender, object neuObject)
        {
            try
            {
                string fileName = "";
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".xls";
                dlg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
                DialogResult result = dlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    fileName = dlg.FileName;
                    FS.FrameWork.WinForms.Controls.NeuSpread view = tabResult.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Controls.NeuSpread;
                    view.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.ColumnHeadersCustomOnly);
                   // this.ShowBalloonTip(5000, "温馨提示", "导出成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出数据发生错误>>" + ex.Message);
                return -1;
            }
            return base.Export(sender, neuObject);
        }

        public override int Print(object sender, object neuObject)
        {
        //    neuSpread1_Sheet1.PrintInfo.Printer = printer;
        //    this.neuSpread1.PrintSheet(neuSpread1_Sheet1);
            FS.FrameWork.WinForms.Controls.NeuSpread view = tabResult.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Controls.NeuSpread;
            //view.Sheets[0].PrintInfo.Preview = true;
            view.Sheets[0].PrintInfo.Printer = printer;
            //view.Sheets[0].PrintInfo.ShowPrintDialog = true;
            view.PrintSheet(view.Sheets[0]);
            return base.Print(sender, neuObject);
        }

        public override int PrintPreview(object sender, object neuObject)
        {
            FS.FrameWork.WinForms.Controls.NeuSpread view = tabResult.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Controls.NeuSpread;
            view.Sheets[0].PrintInfo.Preview = true;
            view.Sheets[0].PrintInfo.Printer = printer;
            view.Sheets[0].PrintInfo.ShowPrintDialog = true;
            view.PrintSheet(view.Sheets[0]);
            return base.PrintPreview(sender, neuObject);
        }

        private void rbtLocLast_CheckedChanged(object sender, EventArgs e)
        {
            //cmbSpecType.Items.Clear();
            //cmbDiseaseType.Items.Clear();
            //cmbDiseaseType.Items.Add("全部");
            //cmbSpecType.Items.Add("全部");
        }

        private void Tab_IndexChange(object sender, EventArgs e)
        {
            if (!specVisible)
            {
                return;
            }
            if (rbtLocLast.Checked)
            {
                tabResult.SelectedTab = tpSpec;
            }
        }

        public override int Save(object sender, object neuObject)
        {
            for (int i = 0; i < neuSpread1_Sheet1.RowCount; i++)
            {           
                string updateBoxSql = "update SPEC_BOX set COMMENT = '{0}' where BOXID = {1}";
                string boxId = neuSpread1_Sheet1.Cells[i, 4].Text.Trim();
                string boxName = neuSpread1_Sheet1.Cells[i, 0].Text.Trim();
                updateBoxSql = string.Format(updateBoxSql, new string[] { boxName, boxId });
                if (subMgr.ExecNoQuery(updateBoxSql) <= 0)
                {
                    MessageBox.Show(boxName + " 更新失败！位于" + (i + 1).ToString() + "行");
                }
            }
            MessageBox.Show("更新成功！");
            return base.Save(sender, neuObject);
        }
    }
}
