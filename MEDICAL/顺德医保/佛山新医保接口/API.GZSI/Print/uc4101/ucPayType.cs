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
    public partial class ucPayType : UserControl
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

        public ucPayType()
        {
            InitializeComponent();
        }

        private int SetValue()
        {
            this.lbHiPaymtd.Text = this.requestModel.setlinfo.hi_paymtd;
            return 1;
        }
    }
}
