using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.HISFC.Components.DCP
{
    /// <summary>
    /// frmSampleInput<br></br>
    /// [功能描述: frmSampleInput输入内容后不可以关闭窗口]<br></br>
    /// [创 建 者: zengft]<br></br>
    /// [创建时间: 2008-8-21]<br></br>
    /// <修改记录 
    ///		修改人='' 
    ///		修改时间='yyyy-mm-dd' 
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class frmSampleInput : Form
    {
        public frmSampleInput()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 原始输入值
        /// </summary>
        private string text = "";

        /// <summary>
        /// 获取输入内容
        /// </summary>
        public string InputText
        {
            get
            {
                return this.rtInput.Text;
            }
            set
            {
                this.rtInput.Text = value;
                this.text = value;
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            set
            {
                this.lbTitle.Text = value;
            }
        }

        #region 事件

        //确定按钮
        private void bttOk_Click(object sender, System.EventArgs e)
        {
            if (this.rtInput.Text == "")
            {
                return;
            }
            this.DialogResult = DialogResult.OK;
            this.Visible = false;
        }

        //关闭按钮
        private void frmCaseInput_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.rtInput.Text == "")
            {
                return;
            }
        }

        //取消按钮
        private void bttCancel_Click(object sender, System.EventArgs e)
        {
            this.InputText = this.text;
            this.DialogResult = DialogResult.Cancel;
            this.Visible = false;
        }
        #endregion
    }
}