using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.BizLogic.Manager;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class ucNoNeedDiag : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant conMgr = new FS.HISFC.BizLogic.Manager.Constant();

        DataView dv = new DataView();

        public ucNoNeedDiag()
        {
            InitializeComponent();
        }

        private void SetTitle()
        {
            neuSpread1_Sheet1.ColumnHeaderRowCount = 2;
            neuSpread1_Sheet1.Models.ColumnHeaderSpan.Add(0, 0, 1, neuSpread1_Sheet1.Columns.Count);
            this.neuSpread1_Sheet1.ColumnHeader.Cells[0, 0].Text = "ICD列表";
            neuSpread1_Sheet1.ColumnHeader.Cells[1, 0].Text = "名称";
            neuSpread1_Sheet1.ColumnHeader.Cells[1, 1].Text = "ICD编码";
            neuSpread2_Sheet1.ColumnHeaderRowCount = 2;
            neuSpread2_Sheet1.Models.ColumnHeaderSpan.Add(0, 0, 1, neuSpread2_Sheet1.Columns.Count);
            this.neuSpread2_Sheet1.ColumnHeader.Cells[0, 0].Text = "排除诊断列表";
            neuSpread2_Sheet1.ColumnHeader.Cells[1, 0].Text = "名称";
            neuSpread2_Sheet1.ColumnHeader.Cells[1, 0].Text = "ICD编码";
        }

        /// <summary>
        /// 从COM_Dictionary中获取不需要录取的诊断
        /// </summary>
        private void GetNotNeedData()
        {
            neuSpread2_Sheet1.RowCount = 0;
            ArrayList arrTemp = new ArrayList();
            arrTemp = conMgr.GetAllList("NoNeedDiagForSpec");
            if (arrTemp == null || arrTemp.Count <= 0)
                return;
            neuSpread2_Sheet1.DataSource = arrTemp;
            neuSpread2_Sheet1.ColumnCount = 2;
            neuSpread2_Sheet1.AutoGenerateColumns = false;
            neuSpread2_Sheet1.BindDataColumn(1, "ID");
            neuSpread2_Sheet1.BindDataColumn(0, "Name");
        }

        private void Add()
        {
            try
            {
                int activeRow = neuSpread1_Sheet1.ActiveRowIndex;
                if (activeRow >= 0)
                {
                    neuSpread2_Sheet1.Rows.Add(0, 1);
                    neuSpread2_Sheet1.Cells[0, 0].Text = neuSpread1_Sheet1.Cells[activeRow, 0].Text;
                    neuSpread2_Sheet1.Cells[0, 1].Text = neuSpread1_Sheet1.Cells[activeRow, 1].Text;
                    neuSpread2_Sheet1.Rows[0].Tag = "N";
                    neuSpread2_Sheet1.Rows[0].BackColor = Color.SkyBlue;
                }
            }
            catch
            { }
        }

        private void Save()
        {
            try
            {
                for (int i = 0; i < neuSpread2_Sheet1.RowCount; i++)
                {
                    string name = neuSpread2_Sheet1.Cells[i, 0].Text;
                    string id = neuSpread2_Sheet1.Cells[i, 1].Text;
                    if (neuSpread2_Sheet1.Rows[i].Visible == false)
                    {
                        conMgr.DelConstant("NoNeedDiagForSpec", id);
                    }
                    if (neuSpread2_Sheet1.Rows[i].Tag == null)
                    {
                        continue;
                    }
                    string tag = neuSpread2_Sheet1.Rows[i].Tag.ToString();
                    if (tag.Trim() == "")
                    {
                        continue;
                    }
                    
                    if (tag == "N")
                    {
                        FS.FrameWork.Models.NeuObject o = conMgr.GetConstant("NoNeedDiagForSpec", id);
                        if (o == null || o.ID=="")
                        {
                            FS.HISFC.Models.Base.Const c = new FS.HISFC.Models.Base.Const();

                            c.ID = id;
                            c.SortID = 1;
                            c.IsValid = true;
                            c.Name = name;
                            conMgr.InsertItem("NoNeedDiagForSpec", c);
                        }
                    }
                    if (tag == "D")
                    {
                        conMgr.DelConstant("NoNeedDiagForSpec", id);
                    }
                }
                MessageBox.Show("保存成功!");
                this.GetNotNeedData();

            }
            catch
            { }
        }

        private void Filter()
        {
            try
            {
                string icd = cmbDiag.Tag.ToString();
                if (icd == null || icd.Trim() == "")
                {
                    return;
                }

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
                    neuSpread1_Sheet1.AutoFilterColumn(1, icd, 1);//.AutoFilterColumn(0, barCode, 0);
                }
                catch
                { }

                for (int i = 0; i < neuSpread1_Sheet1.Rows.Count; i++)
                {
                    string tmpIcd = neuSpread1_Sheet1.Cells[i, 1].Text;
                    if (tmpIcd == icd)
                    {
                        neuSpread1.Focus();
                        neuSpread1_Sheet1.Rows[i].BackColor = Color.SkyBlue;
                        neuSpread1_Sheet1.ActiveRowIndex = i;
                        neuSpread1_Sheet1.SelectionUnit = FarPoint.Win.Spread.Model.SelectionUnit.Cell;
                        neuSpread1_Sheet1.SetActiveCell(i, 1);
                        
                        break;
                    }
                }
            }
            catch
            { }
        }

        private void Remove()
        {
            try
            {
                int activeRow = neuSpread2_Sheet1.ActiveRowIndex;
                if (activeRow >= 0)
                {
                    neuSpread2_Sheet1.Rows[activeRow].Tag = "D";
                    neuSpread2_Sheet1.Rows[activeRow].Visible = false;
                }
            }
            catch
            { }
        }

        private void ucNoNeedDiag_Load(object sender, EventArgs e)
        {
            try
            {
                SetTitle();

                DiagnoseManage diagMgr = new DiagnoseManage();
                //FS.HISFC.Integrate.HealthRecord.HealthRecordBase icdMgr = new FS.HISFC.Integrate.HealthRecord.HealthRecordBase();
                string sql = @"SELECT
      icd_name as 诊断名称, 
      icd_code as 诊断码,   --icd10主诊断码       
      spell_code 拼音码,   --拼音码 
      wb_code 五笔,   --五笔  
      icd_name1 as 第二诊断名称,   --疾病名称1 
      icd_name2 as 第三诊断名称
       FROM met_com_icd10 a 
       ";
                DataSet ds = new DataSet();
                diagMgr.ExecQuery(sql, ref ds);
                dv = ds.Tables[0].DefaultView;
                neuSpread1_Sheet1.DataSource = dv;
                
                GetNotNeedData();
            }
            catch
            { }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            Add();
        }

        private void neuSpread2_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            Remove();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Add();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            this.Remove();
        }

        public override int Save(object sender, object neuObject)
        {
            this.Save();
            return base.Save(sender, neuObject);
        }

        private void cmbDiag_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void cmbDiag_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (cmbDiag.Text.Trim() != "" && cmbDiag.Tag != null && cmbDiag.Tag.ToString().Trim() != "")
                {
                    this.Add();
                }
            }
        }

        private void txtFilter_Validated(object sender, EventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            string filter = "诊断名称 like '%" +
                txtFilter.Text + "%' or 诊断码 like '%" +
                txtFilter.Text + "%' or 拼音码 like '%" +
                txtFilter.Text + "%' or 五笔 like '%" +
                txtFilter.Text + "%'";
            dv.RowFilter = filter;
        }

        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            this.Add();
        }

        private void btnRemove_Click_1(object sender, EventArgs e)
        {
            this.Remove();
        }
    }
}
