using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinReg
{
    public partial class ucFinRegBookFee  : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinRegBookFee()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 住院收费业务层
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();


        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            return base.OnRetrieve(this.dtpBeginTime.Value, this.dtpEndTime.Value);
        }

        #region 事件

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucFinRegBookFee_Load(object sender, EventArgs e)
        {
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            this.dtpEndTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 08, 00, 00);
            this.dtpBeginTime.Value = this.dtpEndTime.Value.AddDays(-1);
        }

        #endregion

        protected override int OnExport()
        {

            try
            {
                base.OnExport();
            }
            catch
            {
                MessageBox.Show("导出出错，有可能您的文件正在使用...");
           
            }
            return 1;
           
        }

    }
}