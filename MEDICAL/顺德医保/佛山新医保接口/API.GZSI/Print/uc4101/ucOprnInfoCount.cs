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
    public partial class ucOprnInfoCount : UserControl
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

        public ucOprnInfoCount()
        {
            InitializeComponent();
        }

        private int SetValue()
        {
            this.lbOprnOprtCodeCnt.Text = this.RequestModel.oprninfo.Count.ToString();//this.requestModel.setlinfo.oprn_oprt_code_cnt;
            this.lbVentUsedDuraDay.Text = this.requestModel.setlinfo.vent_used_dura;
            this.lbPwcryBfadmComaDura.Text = this.requestModel.setlinfo.pwcry_bfadm_coma_dura;
            this.lbPwcryAfadmComaDura.Text = this.requestModel.setlinfo.pwcry_afadm_coma_dura;
            return 1;
        }
    }
}
