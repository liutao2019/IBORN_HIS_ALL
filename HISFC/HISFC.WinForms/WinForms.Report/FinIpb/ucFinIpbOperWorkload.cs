using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.FinIpb
{
    public partial class ucFinIpbOperWorkload : Report.Common.ucQueryBaseForDataWindow
    {
        public ucFinIpbOperWorkload()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 住院收费业务层

        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

     //   private string deptcode = string.Empty;

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

            return base.OnRetrieve(base.beginTime, base.endTime,this.employee.Dept.ID);
        }

        #region 事件

        /// <summary>
        /// 加载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucFinIpbOperWorkload_Load(object sender, EventArgs e)
        {
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();          
            this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day , 0, 0, 0);
        }

        #endregion
    }
}
