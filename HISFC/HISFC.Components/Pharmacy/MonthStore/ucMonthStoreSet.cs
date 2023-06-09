using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Pharmacy.MonthStore
{
    public partial class ucMonthStoreSet : UserControl
    {
        public ucMonthStoreSet()
        {
            InitializeComponent();
        }

        private DialogResult result = DialogResult.Cancel;

        /// <summary>
        /// 结果类型
        /// </summary>
        public DialogResult Result
        {
            get
            {
                return this.result;
            }
        }

        /// <summary>
        /// 下次执行时间
        /// </summary>
        public DateTime NextTime
        {
            get
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(this.dtpEnd.Text);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        private void Close()
        {
            if (this.ParentForm != null)
                this.ParentForm.Close();
        }

        /// <summary>
        /// Job设置
        /// </summary>
        /// <param name="job"></param>
        public void SetJob(FS.HISFC.Models.Base.Job job, DateTime maxDate)
        {
            this.lbInfo.Text = string.Format("上次月结截至时间为{0} ", job.LastTime);

            this.dtpEnd.Value = maxDate;

            this.dtpEnd.MinDate = job.LastTime.AddSeconds(1);

            this.dtpEnd.MaxDate = maxDate;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            this.result = DialogResult.OK;

            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.result = DialogResult.Cancel;

            this.Close();
        }

    }
}
