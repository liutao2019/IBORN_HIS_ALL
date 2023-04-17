using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Components.Registration.NewRegister
{
    /// <summary>
    /// 客户图形化标签控件 74958B4A-AD55-4775-BE30-E030DDC47A64
    /// </summary>
    public partial class UCCustomerTag :UserControl
    {
        public UCCustomerTag()
        {
            InitializeComponent();
        }

        FS.HISFC.Models.MedicalPackage.PackageTag model = null;

        private Patient patientInfo;

        public Patient PatientInfo
        {
            get { return patientInfo; }
            set { 
                patientInfo = value;
                if (patientInfo != null)
                {
                    if (patientInfo.VipFlag)
                    {
                        imgVip.Image = imageList1.Images[0]; ;
                    }
                    FS.HISFC.BizLogic.MedicalPackage.Fee.Package pckMgr = new FS.HISFC.BizLogic.MedicalPackage.Fee.Package();
                    model = pckMgr.QueryPakcage(patientInfo.PID.CardNO);
                    if (model != null && model.PackageNames != null && model.PackageNames.Length > 0)
                    {
                        imgPackage.Image = imageList1.Images[2];
                    }
                }
            }
        }

      




      



        private void UCCustomerTag_Load(object sender, EventArgs e)
        {

        }

        private void imgPackage_Click(object sender, EventArgs e)
        {
            if (model != null && !string.IsNullOrEmpty(model.PackageNames))
            {
                PackageQuery();
            }
        }


        /// <summary>
        /// 套餐查询
        /// </summary>
        private void PackageQuery()
        {
            if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.PID.CardNO))
            {
                MessageBox.Show("请先检索患者！");
                return;
            }
            FS.HISFC.BizLogic.Fee.Account accountMgr = new FS.HISFC.BizLogic.Fee.Account();
            FS.HISFC.Components.Common.Forms.frmPackageQuery frmpackage = new FS.HISFC.Components.Common.Forms.frmPackageQuery();
            frmpackage.PatientInfo = accountMgr.GetPatientInfoByCardNO(this.PatientInfo.PID.CardNO);
            frmpackage.DetailVisible = true;
            frmpackage.ShowDialog();
        }

        private void imgPackage_MouseHover(object sender, EventArgs e)
        {
            if (model != null && model.PackageNames != null && model.PackageNames.Length > 0)
            {
                ToolTip p = new ToolTip();
                p.ShowAlways = true;
                p.SetToolTip(this.imgPackage, model.PackageNames);
                imgPackage.MouseHover -= new EventHandler(imgPackage_MouseHover);
            }
        }

    }
}
