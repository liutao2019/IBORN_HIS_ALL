using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Speciment.Setting
{
    public partial class frmSpecProject : Form
    {
        public frmSpecProject()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口类型
        /// </summary>
        private string frmType = string.Empty;

        /// <summary>
        /// 窗口类型"Setting":设置；"Input":录入。
        /// </summary>
        public string FrmType
        {
            get
            {
                return this.frmType;
            }
            set
            {
                this.frmType = value;
            }
        }

        /// <summary>
        /// 原有数据,用于保存之前数据，方便查询或者修改
        /// </summary>
        private FS.FrameWork.Models.NeuObject original = new FS.FrameWork.Models.NeuObject();

        /// <summary>
        /// 对外原有数据设置获取
        /// </summary>
        public FS.FrameWork.Models.NeuObject Original
        {
            get
            {
                return this.original;
            }
            set
            {
                this.original = value;
            }
        }

        private FS.FrameWork.Models.NeuObject rtObj = new FS.FrameWork.Models.NeuObject();
        /// <summary>
        /// 返回选择内容
        /// </summary>
        public FS.FrameWork.Models.NeuObject RtObj
        {
            get
            {
                return this.rtObj;
            }
            set
            {
                this.rtObj = value;
            }
        }

        /// <summary>
        /// 管理业务层
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        private void frmSpecProject_Load(object sender, EventArgs e)
        {
            ArrayList alPjt = new ArrayList();
            alPjt = this.managerIntegrate.GetConstantList("SpecProject");
            if ((alPjt == null) || (alPjt.Count == 0))
            {
                MessageBox.Show("请维护好常数中的指定项目！");
                return;
            }
            else
            {
                this.neuSpread1_Sheet1.RowCount = alPjt.Count;
                int i = 0;
                foreach (FS.HISFC.Models.Base.Const c in alPjt)
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Text = "□";
                    this.neuSpread1_Sheet1.Cells[i, 1].Text = c.Name;
                    this.neuSpread1_Sheet1.Cells[i, 1].Tag = c.ID;
                    if (!string.IsNullOrEmpty(this.Original.ID))
                    {
                        if (c.ID == this.Original.ID)
                        {
                            this.neuSpread1_Sheet1.Cells[i, 0].Text = "■";
                            this.neuSpread1_Sheet1.Rows[i].BackColor = Color.SkyBlue;
                        }
                    }
                    i++;
                }
            }
            //如果是设置指定项目时只需要选择项目，不需要录入数量，在标本录入时需要录入数量
            if (this.FrmType == "Setting")
            {
                this.neuSpread1_Sheet1.Columns[2].Locked = true;
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[2].Locked = false;
            }
        }

        private void cbSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (cbSelect.Checked)
            {
                for (int j = 0; j <= this.neuSpread1_Sheet1.RowCount - 1; j++)
                {
                    this.neuSpread1_Sheet1.Cells[j, 0].Text = "True";
                }
            }
            else
            {
                for (int k = 0; k <= this.neuSpread1_Sheet1.RowCount - 1; k++)
                {
                    this.neuSpread1_Sheet1.Cells[k, 0].Text = "False";
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.RtObj = new FS.FrameWork.Models.NeuObject();
            this.Close();
        }

        private void neuSpread1_SelectionChanged(object sender, FarPoint.Win.Spread.SelectionChangedEventArgs e)
        {
            int curRow = this.neuSpread1_Sheet1.ActiveRowIndex;
            if (this.neuSpread1_Sheet1.IsSelected(curRow, 0))
            {
                for (int ji = 0; ji <= this.neuSpread1_Sheet1.RowCount - 1; ji++)
                {
                    if (ji != curRow)
                    {
                        this.neuSpread1_Sheet1.Cells[ji, 0].Text = "□";
                        this.neuSpread1_Sheet1.Rows[ji].BackColor = Color.White;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[ji, 0].Text = "■";
                        this.neuSpread1_Sheet1.Rows[ji].BackColor = Color.SkyBlue;
                    }
                }
            }
            else
            {
                this.neuSpread1_Sheet1.Cells[curRow, 0].Text = "■";
                this.neuSpread1_Sheet1.Rows[curRow].BackColor = Color.SkyBlue;
                for (int ji = 0; ji <= this.neuSpread1_Sheet1.RowCount - 1; ji++)
                {
                    if (ji != curRow)
                    {
                        this.neuSpread1_Sheet1.Cells[ji, 0].Text = "□";
                        this.neuSpread1_Sheet1.Rows[ji].BackColor = Color.White;
                    }
                    else
                    {
                        this.neuSpread1_Sheet1.Cells[ji, 0].Text = "■";
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            for (int kk = 0; kk <= this.neuSpread1_Sheet1.RowCount - 1; kk++)
            {
                if (this.neuSpread1_Sheet1.Cells[kk, 0].Text == "■")
                {
                    this.RtObj.Name = this.neuSpread1_Sheet1.Cells[kk, 2].Text.ToString();
                    if (this.FrmType == "Input")
                    {
                        if (string.IsNullOrEmpty(this.RtObj.Name))
                        {
                            MessageBox.Show("请输入数量后确认！");
                            return;
                        }
                    }
                    this.RtObj.ID = this.neuSpread1_Sheet1.Cells[kk, 1].Tag.ToString();
                    this.RtObj.Memo = this.neuSpread1_Sheet1.Cells[kk, 3].Text.ToString();
                    break;
                }
            }
            this.Close();
        }

    }
}