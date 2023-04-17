using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Forms
{
    /// <summary>
    /// [功能描述: 医嘱停止窗口]<br></br>
    /// [创 建 者: wolf]<br></br>
    /// [创建时间: 2004-10-12]<br></br>
    /// <修改记录
    ///		修改人=''
    ///		修改时间=''
    ///		修改目的=''
    ///		修改描述=''
    ///  />
    /// </summary>
    public partial class frmDCOrder : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public frmDCOrder()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void frmDCOrder_Load(object sender, EventArgs e)
        {
            try
            {
                this.dateTimeBox1.Value = CacheManager.InOrderMgr.GetDateTimeFromSysDateTime().AddMinutes(5);
                this.dateTimeBox1.MinDate = this.dateTimeBox1.Value.Date;
                //this.cmbDC.AddItems(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.DCREASON));
                System.Collections.ArrayList al = CacheManager.GetConList("DCREASON");
                if (al == null || al.Count == 0)
                {
                    MessageBox.Show("查询停止原因出错！" + CacheManager.InterMgr.Err);
                    return;
                }
                this.cmbDC.AddItems(al);
                if (this.cmbDC.Items.Count > 0)
                {
                    this.cmbDC.SelectedIndex = 0;
                }
            }
            catch
            {
            }
        }


        private void btnOK_Click(object sender, System.EventArgs e)
        {
            DateTime now = CacheManager.InterMgr.GetDateTimeFromSysDateTime();
            if (this.dateTimeBox1.Value.Date < now.Date)
            {
                MessageBox.Show("停止日期不能小于当前日期！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                this.DialogResult = DialogResult.OK;
            }
            catch { }
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// 停止时间
        /// </summary>
        public DateTime DCDateTime
        {
            get
            {
                return this.dateTimeBox1.Value;
            }
        }
        /// <summary>
        /// 停止原因
        /// </summary>
        public FS.FrameWork.Models.NeuObject DCReason
        {
            get
            {
                if (this.cmbDC.Text == "")
                {
                    return new FS.FrameWork.Models.NeuObject();
                }
                return this.cmbDC.alItems[this.cmbDC.SelectedIndex] as FS.FrameWork.Models.NeuObject;
            }
        }
    }
}