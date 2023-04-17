using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Account.Forms
{
    public partial class frmSelectAccountType : Form
    {
        public frmSelectAccountType()
        {

            InitializeComponent();
        }
        private int returnValue = 0;
        /// <summary>
        /// 返回
        /// </summary>
        public int ReturnValue
        {
            set
            {
                returnValue = value;
            }
            get
            {
                return returnValue;
            }
        }
        private FS.HISFC.Models.Base.Const accountType = new FS.HISFC.Models.Base.Const();   
        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// 所有的账户类型
        /// </summary>
        private ArrayList alAccountType = new ArrayList();
        /// <summary>
        /// 返回账户类型
        /// </summary>
        public FS.HISFC.Models.Base.Const AccountType
        {
            set { accountType = value; }
            get
            {
                if (accountType == null)
                {
                    accountType = new FS.HISFC.Models.Base.Const();
                }
                return accountType; 
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(this.cmbAccountType.Tag.ToString()))
            {
                MessageBox.Show("请选择账户类型！");
                returnValue = 0;
                return;
            }
            else
            {
                accountType.ID = this.cmbAccountType.Tag.ToString();
                accountType.Name = this.cmbAccountType.Text;
            }

            this.DialogResult = DialogResult.OK;
            returnValue = 1;
        }

        private void frmSelectAccountType_Load(object sender, EventArgs e)
        {
            //账户类型
            alAccountType = managerIntegrate.GetAccountTypeList();
            if (alAccountType != null && alAccountType.Count > 0)
            {
                this.cmbAccountType.AddItems(alAccountType);
                this.cmbAccountType.Tag = "1";
            }
        }
    }
}
