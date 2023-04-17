using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HISFC.Components.Package.Forms
{
    public partial class frmPackage : FS.FrameWork.WinForms.Forms.BaseForm
    {
        public delegate void RefreshPackage(FS.HISFC.Models.MedicalPackage.Package packagecur);

        public RefreshPackage RefreshPackageInfo;

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();
       
        /// <summary>
        /// 套餐维护业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Package packageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Package();

        /// <summary>
        /// 转化类
        /// </summary>
        private FS.FrameWork.Function.NConvert convert = new FS.FrameWork.Function.NConvert();

        /// <summary>
        /// 套餐分类
        /// </summary>
        private System.Collections.ArrayList categoryList = null;

        /// <summary>
        /// 套餐范围
        /// </summary>
        private System.Collections.ArrayList rangeList = null;

        /// <summary>
        /// 套餐状态
        /// </summary>
        private System.Collections.ArrayList statusList = null;

        /// <summary>
        /// 当前套餐
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.Package currentPackage = null;

        /// <summary>
        /// 是否修改
        /// </summary>
        private bool editMode = false;

        /// <summary>
        /// 套餐类别
        /// </summary>
        public System.Collections.ArrayList CategoryList
        {
            get { return this.categoryList; }
            set { this.categoryList = value;}
        }

        /// <summary>
        /// 套餐范围
        /// </summary>
        public System.Collections.ArrayList RangeList
        {
            get { return rangeList; }
            set { rangeList = value; }
        }

        /// <summary>
        /// 套餐状态
        /// </summary>
        public System.Collections.ArrayList StatusList
        {
            get { return statusList; }
            set { statusList = value; }
        }

        /// <summary>
        /// 是否为修改模式
        /// </summary>
        public bool EditMode
        {
            get { return editMode; }
            set { editMode = value; }
        }

        /// <summary>
        /// 当前选中套餐
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.Package CurrentPackage
        {
            get { return this.currentPackage; }
            set
            {
                this.currentPackage = (value == null) ? null : value.Clone();
            }
        }

        public frmPackage()
        {
            InitializeComponent();
            this.Load += new EventHandler(frmPackage_Load);
            this.BindEvent();
        }

        private void frmPackage_Load(object sender, EventArgs e)
        {
            this.InitControls();
            this.SetPackageInfo();
        }

        private void BindEvent()
        {
            this.tbName.TextChanged += new EventHandler(tbName_TextChanged);
            this.btnSave.Click += new EventHandler(btnSave_Click);
            this.btnCancel.Click += new EventHandler(btnCancel_Click);
        }

        private void InitControls()
        {
            this.cmbPackageType.Items.Clear();
            this.cmbPackageRange.Items.Clear();
            this.cmbValid.Items.Clear();
            this.cmbPackageType.AddItems(this.CategoryList);
            this.cmbPackageRange.AddItems(this.RangeList);
            this.cmbValid.AddItems(this.StatusList);
        }

        private void Clear()
        {
            this.CurrentPackage = null;
            this.tbName.Text = "";
            this.tbUserCode.Text = "";
            this.cmbPackageType.Tag = "";
            this.cmbPackageRange.Tag = "";
            this.cmbValid.Tag = "";
            this.rbMemo.Text = "";
            this.lbTip.Text = "";
            this.editMode = false;
            this.chkComboFlag.Checked = false;
        }

        private void SetPackageInfo()
        {
            if (this.CurrentPackage == null || string.IsNullOrEmpty(this.currentPackage.ID))
            {
                return;
            }

            this.tbName.Text = this.CurrentPackage.Name;
            this.tbUserCode.Text = this.CurrentPackage.UserCode;
            this.cmbValid.Tag = this.CurrentPackage.IsValid ? "1" : "0";
            this.cmbPackageType.Tag = this.CurrentPackage.PackageType;
            this.cmbPackageRange.Tag = (int)this.CurrentPackage.UserType;
            this.chkComboFlag.Checked = this.CurrentPackage.ComboFlag == "1";
            this.rbMemo.Text = this.CurrentPackage.Memo;
        }

        private int GetPackageInfo()
        {
            int rtn = -1;

            if (!ValidCheck())
            {
                return rtn;
            }

            if (this.CurrentPackage == null)
            {
                this.CurrentPackage = new FS.HISFC.Models.MedicalPackage.Package();
            }

            this.CurrentPackage.Name = this.tbName.Text;
            this.CurrentPackage.SpellCode = convert.ToSpellCode(this.currentPackage.Name);
            this.CurrentPackage.UserCode = this.tbUserCode.Text;
            this.CurrentPackage.IsValid = this.cmbValid.Tag.ToString() == "1"?true:false;
            this.CurrentPackage.PackageType = this.cmbPackageType.Tag.ToString();
            this.CurrentPackage.UserType =  (FS.HISFC.Models.Base.ServiceTypes)Enum.ToObject(typeof(FS.HISFC.Models.Base.ServiceTypes),FS.FrameWork.Function.NConvert.ToInt32((this.cmbPackageRange.Tag.ToString())));
            this.CurrentPackage.PackageClass = "1";  // 1-套餐，2-子套餐
            this.CurrentPackage.ParentCode = "";
            this.CurrentPackage.ComboFlag = this.chkComboFlag.Checked ? "1" : "0";
            this.CurrentPackage.MainFlag = "0";
            this.CurrentPackage.SpecialFlag = "0";
            this.currentPackage.Memo = this.rbMemo.Text;

            return 1;
        }

        private bool ValidCheck()
        {
            bool rtn = true;
            string tipStr = string.Empty;

            if (string.IsNullOrEmpty(this.tbName.Text))
            {
                tipStr += "套餐名称不能为空； ";
                rtn = false;
            }

            if (string.IsNullOrEmpty(this.cmbValid.Tag.ToString()))
            {
                tipStr += "套餐状态不能为空； ";
                rtn = false;
            }

            if (string.IsNullOrEmpty(this.cmbPackageType.Tag.ToString()))
            {
                tipStr += "套餐类型不能为空； ";
                rtn = false;
            }

            if (string.IsNullOrEmpty(this.cmbPackageRange.Tag.ToString()))
            {
                tipStr += "套餐范围不能为空； ";
                rtn = false;
            }

            this.lbTip.Text = tipStr;
            return rtn;
        }

        private bool Save()
        {
            bool rtn = false;

            if (this.GetPackageInfo() < 0)
            {
                return rtn;
            }


            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            if (this.editMode)
            {
                if (this.packageProcess.UpdatePackage(this.CurrentPackage) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存套餐信息失败：" + this.packageProcess.Err);
                    return rtn;
                }
            }
            else
            {
                if (this.packageProcess.AddPackage(this.CurrentPackage) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("保存套餐信息失败：" + this.packageProcess.Err);
                    return rtn;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            if (RefreshPackageInfo != null)
            {
                this.RefreshPackageInfo(this.CurrentPackage);
            }

            return true;
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            this.tbUserCode.Text = convert.ToSpellCode(this.tbName.Text);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.Save())
            {
                MessageBox.Show("保存套餐成功!");
                this.Clear();
                this.Hide();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Clear();
            this.Hide();
        }
    }
}
