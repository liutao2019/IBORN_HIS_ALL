using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISFC.Components.Package.Controls
{
    public partial class ucPackgeEdit : FS.FrameWork.WinForms.Controls.ucBaseControl
    {        
        /// <summary>
        /// 当前选中套餐
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.Package currentPackage = null;


        /// <summary>
        /// 常数管理类
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 套餐类别
        /// </summary>
        private ArrayList categoryList = new ArrayList();

        /// <summary>
        /// 当前选中套餐
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.Package CurrentPackage
        {
            get { return this.currentPackage; }
            set
            {
                this.currentPackage = (value == null) ? null : value.Clone(); ;
                SetInfoDisplay();
            }
        }

        /// <summary>
        /// 套餐类别
        /// </summary>
        public ArrayList CategoryList
        {
            get { return this.categoryList; }
            set
            {
                this.categoryList = value;
                this.cmbpackageType.AddItems(categoryList);
                this.cmbpackageType.ValueMember = "ID";
                this.cmbpackageType.DisplayMember = "Name";
                this.cmbpackageType.DropDownStyle = ComboBoxStyle.DropDownList;
            }
        }

        public ucPackgeEdit()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 设置信息显示
        /// </summary>
        private void SetInfoDisplay()
        {
            this.ClearDisplay();
            if (this.currentPackage != null)
            {
                this.txtName.Text = this.currentPackage.Name;
                this.chkValid.Checked = this.currentPackage.IsValid;
                this.txtUserCode.Text = this.currentPackage.UserCode;
                this.txtMemo.Text = this.currentPackage.Memo;

                if (categoryList != null)
                {
                    foreach (FS.HISFC.Models.Base.Const cst in categoryList)
                    {
                        if (this.currentPackage.PackageType == cst.ID)
                        {
                            this.cmbpackageType.Text = cst.Name;
                            break;
                        }
                    }
                }

                switch (this.currentPackage.UserType)
                {
                    case FS.HISFC.Models.Base.ServiceTypes.C:
                        this.rdOutPatient.Checked = true;
                        break;
                    case FS.HISFC.Models.Base.ServiceTypes.I:
                        this.rdInpatient.Checked = true;
                        break;
                    case FS.HISFC.Models.Base.ServiceTypes.T:
                        this.rdPhysicalExam.Checked = true;
                        break;
                    case FS.HISFC.Models.Base.ServiceTypes.A:
                        this.rdAll.Checked = true;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 清除显示信息
        /// </summary>
        private void ClearDisplay()
        {
            this.txtName.Text = "";
            this.rdAll.Checked = true;
            this.chkValid.Checked = true;
            this.txtUserCode.Text = "";
            this.cmbpackageType.SelectedIndex = -1;
            this.txtMemo.Text = "";
        }

        public bool GetPackageInfo(ref FS.HISFC.Models.MedicalPackage.Package package,ref string ErrMsg)
        {
            try
            {
                FS.FrameWork.Function.NConvert convert = new FS.FrameWork.Function.NConvert();
                if (this.currentPackage == null)
                {
                    this.currentPackage = new FS.HISFC.Models.MedicalPackage.Package();
                }

                this.currentPackage.Name = this.txtName.Text;
                this.currentPackage.SpellCode = convert.ToSpellCode(this.currentPackage.Name);
                this.currentPackage.Memo = this.txtMemo.Text;
                this.currentPackage.UserCode = this.txtUserCode.Text;
                this.currentPackage.PackageType = this.cmbpackageType.SelectedItem.ID;
                this.currentPackage.IsValid = this.chkValid.Checked;

                if (this.rdAll.Checked)
                {
                    this.currentPackage.UserType = FS.HISFC.Models.Base.ServiceTypes.A;
                }

                if (this.rdOutPatient.Checked)
                {
                    this.currentPackage.UserType = FS.HISFC.Models.Base.ServiceTypes.C;
                }

                if (this.rdInpatient.Checked)
                {
                    this.currentPackage.UserType = FS.HISFC.Models.Base.ServiceTypes.I;
                }

                if (this.rdPhysicalExam.Checked)
                {
                    this.currentPackage.UserType = FS.HISFC.Models.Base.ServiceTypes.T;
                }

                package = this.currentPackage;
            }
            catch(Exception ex)
            {
                if (this.cmbpackageType.SelectedItem == null)
                {
                    ErrMsg = "请选择套餐类型！";
                }
                else
                {
                    ErrMsg = ex.Message;
                }
                return false;
            }

            return true;
           
        }


        public void SetTotFee(decimal totFee)
        {
            this.lblMoney.Text = "套餐金额：￥" + totFee.ToString();
        }
    }
}
