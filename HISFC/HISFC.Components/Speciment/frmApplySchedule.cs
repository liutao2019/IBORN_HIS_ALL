using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Speciment;
using FS.HISFC.BizLogic.Speciment;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Components.Speciment
{
    public partial class frmApplySchedule : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public frmApplySchedule()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 申请号
        /// </summary>
        private string applyId = string.Empty;
        public string ApplyId
        {
            get
            {
                return this.applyId;
            }
            set
            {
                this.applyId = value;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        private void frmApplySchedule_Load(object sender, EventArgs e)
        {
            string sql = @" select a.APPLICATIONID 申请号,b.OPERNAME 操作人,
                            a.APPLYTIME 申请时间,b.SCHEDULE 进度描述,to_char(b.CURDATE,'yyyy-mm-dd hh24:mi:ss') 进度时间,
                            a.APPLYUSERNAME 申请人
                            from SPEC_APPLICATIONTABLE a,SPEC_USERAPPLICATION b where 
                            a.APPLICATIONID = b.APPLICATIONID and a.APPLICATIONID =" + this.ApplyId + " order by b.CURDATE";
            SpecOutManage outMgr = new SpecOutManage();
            DataSet ds = new DataSet();

            outMgr.ExecQuery(sql, ref ds);

            if (ds == null || ds.Tables.Count <= 0)
            {
                MessageBox.Show("获取申请进度情况失败！");
                return;
            }
            else
            {
                DataTable dt = new DataTable();
                dt = ds.Tables[0];
                this.neuSpread1_Sheet1.DataSource = dt;
            }
        }


    }
}