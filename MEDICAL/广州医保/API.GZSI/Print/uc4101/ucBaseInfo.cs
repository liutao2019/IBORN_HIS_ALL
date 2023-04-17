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
    public partial class ucBaseInfo : UserControl
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

        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        public ucBaseInfo()
        {
            InitializeComponent();
        }

        private int SetValue()
        {
            this.lbPsnName.Text = this.RequestModel.setlinfo.psn_name;
            this.lbGend.Text = this.RequestModel.setlinfo.gend;
            this.lbBrdyYear.Text = (this.RequestModel.setlinfo.brdy.Split('-'))[0];
            this.lbBrdyMonth.Text = (this.RequestModel.setlinfo.brdy.Split('-'))[1];
            this.lbBrdyDay.Text = (this.RequestModel.setlinfo.brdy.Split('-'))[2];
            this.lbAge.Text = this.RequestModel.setlinfo.age;
            this.lbNtly.Text = consMgr.GetConstant("GZSI_nat_regn_code", this.RequestModel.setlinfo.ntly).Name;

            this.lbNwbAge.Text = this.RequestModel.setlinfo.nwb_age;
            this.lbNaty.Text = consMgr.GetConstant("GZSI_naty", this.RequestModel.setlinfo.naty).Name;
            this.lbPatnCertType.Text = consMgr.GetConstant("GZSI_psn_cert_type", this.RequestModel.setlinfo.patn_cert_type).Name;
            this.lbCertno.Text = this.RequestModel.setlinfo.certno;

            this.lbPrfs.Text = this.RequestModel.setlinfo.prfs_name;
            this.lbCurrAddr1.Text = this.RequestModel.setlinfo.curr_addr;
            this.lbCurrAddr2.Text = this.RequestModel.setlinfo.curr_addr;
            this.lbCurrAddr3.Text = this.RequestModel.setlinfo.curr_addr;
            this.lbCurrAddr4.Text = this.RequestModel.setlinfo.curr_addr;

            this.lbEmpName.Text = this.RequestModel.setlinfo.emp_name;
            //工作单位地址截取12个汉字
            if (this.RequestModel.setlinfo.emp_addr != null && this.RequestModel.setlinfo.emp_addr.Length > 12)
            {
                this.lbEmpAddr.Text = this.RequestModel.setlinfo.emp_addr.Substring(0, 12);
            }
            else
            {
                this.lbEmpAddr.Text = this.RequestModel.setlinfo.emp_addr;
            }
            this.lbEmpTel.Text = this.requestModel.setlinfo.emp_tel;
            this.lbPoscode.Text = this.requestModel.setlinfo.poscode;

            this.lbConerName.Text = this.RequestModel.setlinfo.coner_name;
            this.lbPatnRlts.Text = consMgr.GetConstant("GZSI_coner_rlts_code", this.RequestModel.setlinfo.patn_rlts).Name;
            this.lbConerAddr1.Text = this.requestModel.setlinfo.coner_addr;
            this.lbConerAddr2.Text = this.requestModel.setlinfo.coner_addr;
            this.lbConerAddr3.Text = this.requestModel.setlinfo.coner_addr;
            this.lbConerAddr4.Text = this.requestModel.setlinfo.coner_addr;

            this.lbHiType.Text = consMgr.GetConstant("GZSI_insutype", this.requestModel.setlinfo.hi_type).Name;
            this.lbSpPsnType.Text = this.requestModel.setlinfo.sp_psn_type;
            this.lbInsuplc.Text = this.requestModel.setlinfo.insuplc == "440106" ? "广州市天河区" : this.requestModel.setlinfo.insuplc;

            this.lbNwbAdmType.Text = this.requestModel.setlinfo.nwb_adm_type;
            this.lbNwbBirWt.Text = this.requestModel.setlinfo.nwb_bir_wt;
            this.lbNwbAdmWt.Text = this.requestModel.setlinfo.nwb_adm_wt == "0" ? "" : this.requestModel.setlinfo.nwb_adm_wt;

            return 1;
        }

    }
}
