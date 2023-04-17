using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Base.Inpatient
{
    /// <summary>
    /// 选择合同单位
    /// </summary>
    public partial class frmSiRegisterInfo : Form
    {
        public frmSiRegisterInfo()
        {
            InitializeComponent();

            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            this.isOK = false;
            this.Close();
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            this.isOK = true;
            this.Close();
        }

        public void Init()
        {
            FS.SOC.HISFC.RADT.BizProcess.InpatientFee feeManager = new FS.SOC.HISFC.RADT.BizProcess.InpatientFee();
            ArrayList al = feeManager.QueryInpatientPact();
            if (al == null)
            {
               CommonController.CreateInstance().MessageBox("获取合同单位信息失败，原因：" + feeManager.Err, MessageBoxIcon.Error);
               return;
            }
            this.cmbNewPact.AddItems(al);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// 默认的合同单位编号
        /// </summary>
        public string DefaultPact
        {
            get
            {
                return cmbNewPact.Tag == null ? "" : cmbNewPact.Tag.ToString();
            }
            set
            {
                this.cmbNewPact.Tag = value;
            }
        }

        private bool isOK = false;
        public bool IsOK
        {
            get
            {
                return isOK;
            }
        }
    }
}
