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
    public partial class ucDiseInfo : UserControl
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

        public ucDiseInfo()
        {
            InitializeComponent();
        }

        private int SetValue()
        {
            this.lbIptMedType.Text = "1";// this.requestModel.setlinfo.ipt_med_type; //1-住院，2-日间手术
            this.lbAdmWay.Text = this.requestModel.setlinfo.adm_way;
            this.lbTrtType.Text = this.requestModel.setlinfo.trt_type??"1";
            this.lbAdmTimeYear.Text = this.requestModel.setlinfo.adm_time.Split('-')[0];
            this.lbAdmTimeMonth.Text = this.requestModel.setlinfo.adm_time.Split('-')[1];
            this.lbAdmTimeDay.Text = (this.requestModel.setlinfo.adm_time.Split('-')[2]).Split()[0];
            this.lbAdmCaty.Text = this.requestModel.setlinfo.adm_caty;
            this.lbRefldeptDept.Text = this.requestModel.setlinfo.refldept_dept;
            this.lbDscgTimeYear.Text = this.requestModel.setlinfo.dscg_time.Split('-')[0];
            this.lbDscgTimeMonth.Text = this.requestModel.setlinfo.dscg_time.Split('-')[1];
            this.lbDscgTimeDay.Text = (this.requestModel.setlinfo.dscg_time.Split('-')[2]).Split()[0];
            this.lbDscgCaty.Text = this.requestModel.setlinfo.dscg_caty;
            this.lbActIptDays.Text = this.requestModel.setlinfo.act_ipt_days;
            this.lbOtpWmDise.Text = this.requestModel.setlinfo.otp_wm_dise;
            this.lbWmDiseCode.Text = this.requestModel.setlinfo.wm_dise_code;
            this.lbOtpTcmDise.Text = this.requestModel.setlinfo.otp_tcm_dise;
            this.lbTcmDiseCode.Text = this.requestModel.setlinfo.tcm_dise_code;
            
            return 1;
        }

    }
}
