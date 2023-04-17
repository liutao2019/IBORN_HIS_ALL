using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.Report.Finance.FinReg
{
    public partial class ucFinRegBookFeeDetail: FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        public ucFinRegBookFeeDetail()
        {
            InitializeComponent();
        }

        /// <summary>
        /// סԺ�շ�ҵ���
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

        #region �¼�

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucFinRegBookFeeDetail_Load(object sender, EventArgs e)
        {
            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();

            this.dtpEndTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 07, 59, 59);
            this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day - 1, 08, 0, 0);
            this.MainDWDataObject = "d_fin_reg_bookfeedetail";


            this.MainDWLabrary = "Report\\finreg.pbd;Report\\finreg.pbl";
        }

        #endregion
    }
}