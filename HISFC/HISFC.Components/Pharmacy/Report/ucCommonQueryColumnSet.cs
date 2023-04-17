using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.Report
{
    /// <summary>
    /// [控件描述: 药品通用查询列设置]
    /// [创 建 人: 孙久海]
    /// [创建时间: 2010-9-6]
    /// <修改记录>
    ///    1.增加列合计属性 by Sunjh 2010-10-27 {121ACBC1-230B-4224-90AC-D826AD4392BC}
    /// </修改记录>
    /// </summary>
    public partial class ucCommonQueryColumnSet : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucCommonQueryColumnSet()
        {
            InitializeComponent();
        }

        #region 变量

        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrete = new FS.HISFC.BizProcess.Integrate.Manager();

        private FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();

        #endregion

        #region 方法

        private void QueryData()
        {
            this.fpDataView.RowCount = 0;
            ArrayList al = this.managerIntegrete.GetConstantList("PHAQUERYCOLUMN");
            for (int i = 0; i < al.Count; i++)
            {
                this.fpDataView.RowCount = i + 1;
                FS.HISFC.Models.Base.Const constObj = al[i] as FS.HISFC.Models.Base.Const;
                this.fpDataView.Cells[i, 0].Text = constObj.ID;
                this.fpDataView.Cells[i, 1].Text = constObj.Name;
                this.fpDataView.Cells[i, 2].Text = constObj.Memo == "hide" ? "False" : "True";                
                this.fpDataView.Cells[i, 3].Text = constObj.WBCode;
                this.fpDataView.Cells[i, 4].Text = constObj.UserCode;
                this.fpDataView.Rows[i].Tag = constObj;
            }
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            this.cbbColumnType.SelectedIndex = 0;
            this.QueryData();
            base.OnLoad(e);
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (this.fpDataView.ActiveRowIndex >= 0)
            {
                FS.HISFC.Models.Base.Const constObj = this.fpDataView.ActiveRow.Tag as FS.HISFC.Models.Base.Const;
                this.txtColumnName.Text = constObj.ID;
                this.txtColumnWidth.Text = constObj.Name;
                this.chkShowColumn.Checked = FS.FrameWork.Function.NConvert.ToBoolean(constObj.Memo);
                if (constObj.UserCode == "Text")
                {
                    this.cbbColumnType.SelectedIndex = 1;
                }
                else if (constObj.UserCode == "Number")
                {
                    this.cbbColumnType.SelectedIndex = 2;
                }
                else if (constObj.UserCode == "CheckBox")
                {
                    this.cbbColumnType.SelectedIndex = 3;
                }
                else
                {
                    this.cbbColumnType.SelectedIndex = 0;
                }
                //增加列合计属性 by Sunjh 2010-10-27 {121ACBC1-230B-4224-90AC-D826AD4392BC}
                this.chkShowTotal.Checked = FS.FrameWork.Function.NConvert.ToBoolean(constObj.WBCode);
            }            
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Base.Const constObj = new FS.HISFC.Models.Base.Const();
            constObj.ID = this.txtColumnName.Text;
            constObj.Name = this.txtColumnWidth.Text;
            constObj.Memo = this.chkShowColumn.Checked.ToString();
            constObj.IsValid = true;
            if (this.cbbColumnType.SelectedIndex == 1)
            {
                constObj.UserCode = "Text";
            }
            else if (this.cbbColumnType.SelectedIndex == 2)
            {
                constObj.UserCode = "Number";
            }
            else if (this.cbbColumnType.SelectedIndex == 3)
            {
                constObj.UserCode = "CheckBox";
            }
            else
            {
                constObj.UserCode = "Default";
            }
            //增加列合计属性 by Sunjh 2010-10-27 {121ACBC1-230B-4224-90AC-D826AD4392BC}
            constObj.WBCode = this.chkShowTotal.Checked.ToString();

            if (this.managerIntegrete.InsertConstant("PHAQUERYCOLUMN", constObj) == -1)
            {
                MessageBox.Show("增加列设置保存失败!");

                return;
            }

            MessageBox.Show("增加成功!");
            this.QueryData();
        }

        private void neuButton2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要删除当前选择的数据么?", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (this.consManager.DelConstant("PHAQUERYCOLUMN", this.fpDataView.Cells[this.fpDataView.ActiveRowIndex, 0].Text) == -1)
                {
                    MessageBox.Show("删除失败!");

                    return;
                }

                MessageBox.Show("删除成功!");
                this.QueryData();
            }
        }

        private void neuButton3_Click(object sender, EventArgs e)
        {
            FS.HISFC.Models.Base.Const constObj = new FS.HISFC.Models.Base.Const();
            constObj.ID = this.txtColumnName.Text;
            constObj.Name = this.txtColumnWidth.Text;
            constObj.Memo = this.chkShowColumn.Checked.ToString();
            constObj.IsValid = true;
            if (this.cbbColumnType.SelectedIndex == 1)
            {
                constObj.UserCode = "Text";
            }
            else if (this.cbbColumnType.SelectedIndex == 2)
            {
                constObj.UserCode = "Number";
            }
            else if (this.cbbColumnType.SelectedIndex == 3)
            {
                constObj.UserCode = "CheckBox";
            }
            else
            {
                constObj.UserCode = "Default";
            }
            //增加列合计属性 by Sunjh 2010-10-27 {121ACBC1-230B-4224-90AC-D826AD4392BC}
            constObj.WBCode = this.chkShowTotal.Checked.ToString();

            if (this.managerIntegrete.UpdateConstant("PHAQUERYCOLUMN", constObj) == -1)
            {
                MessageBox.Show("修改列设置保存失败!");

                return;
            }

            MessageBox.Show("修改成功!");
            this.QueryData();
        }

        private void neuButton4_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

    }
}
