using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace API.GZSI.Print.uc4101
{
    public partial class ucICUInfo : UserControl
    {
        Models.Request.RequestGzsiModel4101 requestModel = new API.GZSI.Models.Request.RequestGzsiModel4101();
        /// <summary>
        /// 请求对象
        /// </summary>
        public Models.Request.RequestGzsiModel4101 RequestModel
        {
            get { return requestModel; }
            set
            {
                requestModel = value;
                this.SetValue();
            }
        }

        public ucICUInfo()
        {
            InitializeComponent();
        }

        private int SetValue()
        {
            this.lbBldCat.Text = requestModel.setlinfo.bld_cat;
            this.lbBldAmt.Text = requestModel.setlinfo.bld_amt;
            this.lbBldUnt.Text = requestModel.setlinfo.bld_unt;
            this.lbSpgaNurscareDays.Text = requestModel.setlinfo.spga_nurscare_days;
            this.lbLv1NurscareDays.Text = requestModel.setlinfo.lv1_nurscare_days;
            this.lbScdNurscareDays.Text = requestModel.setlinfo.scd_nurscare_days;
            this.lbLv3NurscareDays.Text = requestModel.setlinfo.lv3_nurscare_days;
            this.lbDscgWay.Text = requestModel.setlinfo.dscg_way;
            if (requestModel.setlinfo.dscg_way == "2")
            {
                this.lbAcpMedinsName.Text = requestModel.setlinfo.acp_medins_name;
                this.lbAcpOptinsCode.Text = requestModel.setlinfo.acp_optins_code;
                this.lbAcpMedinsName2.Text = "";
                this.lbAcpOptinsCode2.Text = "";
            }
            else if (requestModel.setlinfo.dscg_way == "3")
            {
                this.lbAcpMedinsName.Text = "";
                this.lbAcpOptinsCode.Text = "";
                this.lbAcpMedinsName2.Text = requestModel.setlinfo.acp_medins_name;
                this.lbAcpOptinsCode2.Text = requestModel.setlinfo.acp_optins_code;
            }
            else
            {
                this.lbAcpMedinsName.Text = "";
                this.lbAcpOptinsCode.Text = "";
                this.lbAcpMedinsName2.Text = "";
                this.lbAcpOptinsCode2.Text = ""; 
            }
            this.lbDaysRinpFlag31.Text = requestModel.setlinfo.days_rinp_flag_31;
            this.lbDaysRinpPup31.Text = requestModel.setlinfo.days_rinp_pup_31;
            this.lbChfpdrName.Text = requestModel.setlinfo.chfpdr_name;
            this.lbChfpdrCode.Text = requestModel.setlinfo.chfpdr_code;
            return 1;
        }
    }
}
