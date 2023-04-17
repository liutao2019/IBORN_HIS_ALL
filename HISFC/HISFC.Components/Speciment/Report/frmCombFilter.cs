using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Report
{
    public partial class frmCombFilter : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmCombFilter()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 当前列名
        /// </summary>
        private string currentGrpName = string.Empty;
        /// <summary>
        /// 获取当前列名
        /// </summary>
        public string CurrentGrpName
        {
            get
            {
                return this.currentGrpName;
            }
            set
            {
                this.currentGrpName = value;
                this.grbCurrentCol.Text = value;
            }
        }

        /// <summary>
        /// 当前列内容数组
        /// </summary>
        private ArrayList alCurValues = new ArrayList();

        /// <summary>
        /// 当前列内容数组，用于过滤
        /// </summary>
        public ArrayList AlCurValues
        {
            get
            {
                return this.alCurValues;
            }
            set
            {
                this.alCurValues = value;
            }
        }

        /// <summary>
        /// 返回过滤字符
        /// </summary>
        private string rtFilter = string.Empty;

        /// <summary>
        /// 返回过滤字符
        /// </summary>
        public string RtFilter
        {
            set
            {
                this.rtFilter = value;
            }
            get
            {
                return this.rtFilter;
            }
        }

        public int RtResult = -1;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string filterStr1 = string.Empty;

            if (!string.IsNullOrEmpty(this.cmbLj1.Text.Trim()))
            {
                if (string.IsNullOrEmpty(this.cmbValue1.Text.Trim()))
                {
                    MessageBox.Show("请设置过滤条件一的值！");
                    return;
                }
                else
                {
                    filterStr1 = this.CurrentGrpName + " " + this.cmbLj1.Tag.ToString();
                    filterStr1 = string.Format(filterStr1, this.cmbValue1.Text);
                }
            }

            string filterStr2 = string.Empty;
            if (!string.IsNullOrEmpty(this.cmbLj2.Text.Trim()))
            {
                if (string.IsNullOrEmpty(this.cmbValue2.Text.Trim()))
                {
                    MessageBox.Show("请设置过滤条件二的值！");
                    return;
                }
                else
                {
                    filterStr2 = this.CurrentGrpName + " " + this.cmbLj2.Tag.ToString();
                    filterStr2 = string.Format(filterStr2, this.cmbValue2.Text);
                }
            }

            if ((!string.IsNullOrEmpty(filterStr1)) && (!string.IsNullOrEmpty(filterStr2)))
            {
                if (this.rbtnAnd.Checked)
                {
                    this.RtFilter = "(" + filterStr1 + " AND " + filterStr2 + ")";
                }
                else
                {
                    this.RtFilter = "(" + filterStr1 + " OR " + filterStr2 + ")";
                }
            }
            else if ((string.IsNullOrEmpty(filterStr1)) && (!string.IsNullOrEmpty(filterStr2)))
            {
                this.RtFilter = filterStr2;
            }
            else if ((string.IsNullOrEmpty(filterStr2)) && (!string.IsNullOrEmpty(filterStr1)))
            {
                this.RtFilter = filterStr1;
            }
            else
            {
                MessageBox.Show("请选择过滤条件，如不过滤直接点取消！");
                return;
            }

            if (string.IsNullOrEmpty(this.RtFilter))
            {
                MessageBox.Show("请选择过滤条件，如不过滤直接点取消！");
                return;
            }

            RtResult = 1;
            this.Close();
        }

        private void frmCombFilter_Load(object sender, EventArgs e)
        {
            ArrayList alLj = new ArrayList();
            alLj = this.managerIntegrate.GetConstantList("FilterType");
            if ((alLj != null) && (alLj.Count > 0))
            {
                this.cmbLj1.AddItems(alLj);
                this.cmbLj2.AddItems(alLj);
            }
            if ((this.AlCurValues != null) && (this.AlCurValues.Count > 0))
            {
                this.cmbValue1.AddItems(this.AlCurValues);
                this.cmbValue2.AddItems(this.AlCurValues);
            }
        }

        private void rbtnOr_CheckedChanged(object sender, EventArgs e)
        {
            this.rbtnAnd.Checked = false;
        }

        private void rbtnAnd_CheckedChanged(object sender, EventArgs e)
        {
            this.rbtnOr.Checked = false;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.RtFilter = "1=1";
            RtResult = 0;
            this.Close();
        }


    }
}