using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISFC.Components.Package.Fee.Forms
{
    public partial class frmPayModeChoose : Form
    {
        /// <summary>
        /// 选中支付方式
        /// </summary>
        public string PayModeCode = string.Empty;

        /// <summary>
        /// 支付方式列表
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper helpPayMode = new FS.FrameWork.Public.ObjectHelper();        
        
        /// <summary>
        /// 管理业务层
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        public frmPayModeChoose()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            this.btnOK.Click += new EventHandler(btnOK_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
            this.Disposed += new EventHandler(frmPayModeChoose_Disposed);

            //支付方式
            ArrayList payModes = this.managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYMODES);
            this.helpPayMode.ArrayObject = new ArrayList(payModes.Cast<FS.HISFC.Models.Base.Const>()
                                                                 .Where(t => t.Memo == "true")
                                                                 .Where(t => t.UserCode != "ACD")
                                                                 .Where(t => t.UserCode != "ACY")
                                                                 .Where(t => t.UserCode != "ECO")
                                                                 .ToArray());

            this.neuComboBox1.AddItems(this.helpPayMode.ArrayObject);
        }

        void frmPayModeChoose_Disposed(object sender, EventArgs e)
        {
            PayModeCode = string.Empty;
            this.Close();
        }

        public void SetDefalutPaymode(string paymode)
        {
            this.neuComboBox1.Tag = paymode;
        }

        void btnOK_Click(object sender, EventArgs e)
        {
            this.PayModeCode = this.neuComboBox1.Tag.ToString();
            this.Close();
        }

        void btnCancel_Click(object sender, EventArgs e)
        {
            PayModeCode = string.Empty;
            this.Close();
        }
    }
}
