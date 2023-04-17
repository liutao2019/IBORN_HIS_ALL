using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Report.Finance.FinOpb
{
    public partial class ucFinOpbItemsPerDoct : FSDataWindow.Controls.ucQueryBaseForDataWindow 
    {
        
        public ucFinOpbItemsPerDoct()
        {
            InitializeComponent();

            DateTime nowTime = this.inpatientManager.GetDateTimeFromSysDateTime();
            this.dtpEndTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 23, 59, 59);
            this.dtpBeginTime.Value = new DateTime(nowTime.Year, nowTime.Month, nowTime.Day, 00, 00, 00);
            InitCbo();
        }
        /// <summary>
        /// 
        /// </summary>
        private string doctID = "";
        private string doctName = "";
        private FS.HISFC.BizProcess.Integrate.Manager manger = new FS.HISFC.BizProcess.Integrate.Manager();
        protected FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
        private ArrayList alDoct = new ArrayList();
        ArrayList alCbo = new ArrayList();

        private void getDoctData()
        {
            alDoct = manger.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
        }
        private void InitCbo()
        {
            getDoctData();
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            obj.ID = "ALL";
            obj.Name = "È«²¿";
            alCbo.Add(obj);
            alCbo.AddRange(alDoct);
            this.cboDoct.AddItems(alCbo);
            this.cboDoct.SelectedIndex = 0;          
        }

        private void cboDoct_SelectedIndexChanged(object sender, EventArgs e)
        {
            doctID = ((FS.FrameWork.Models.NeuObject)alCbo[cboDoct.SelectedIndex]).ID.ToString();
            doctName = ((FS.FrameWork.Models.NeuObject)alCbo[cboDoct.SelectedIndex]).Name.ToString();
        }

        protected override int OnRetrieve(params object[] objects)
        {
            if (base.GetQueryTime() == -1)
            {
                return -1;
            }

           

            return base.OnRetrieve(this.dtpBeginTime.Value,this.dtpEndTime.Value,this.doctID,this.doctName);
        }

    }
}
