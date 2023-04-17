using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Account;

namespace FS.HISFC.Components.Account.Forms
{
    public partial class frmMedicalPackage : FS.FrameWork.WinForms.Forms.BaseForm
    {

        /// <summary>
        /// 转化类
        /// </summary>
        private FS.FrameWork.Function.NConvert convert = new FS.FrameWork.Function.NConvert();

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();


        /// <summary>
        /// 账户业务层
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Account accountManager = new FS.HISFC.BizLogic.Fee.Account();


        /// <summary>
        /// 套餐状态
        /// </summary>
        private System.Collections.ArrayList statusList = null;

        /// <summary>
        /// 
        /// </summary>
        private bool saveState = false;


        /// <summary>
        /// 是否修改
        /// </summary>
        private bool editMode = false;


        /// <summary>
        /// 当前套餐
        /// </summary>
        private ItemMedical currentPackage = null;



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
        /// 是否保存成功
        /// </summary>
        public bool SaveState
        {
            get { return saveState; }
            set { saveState = value; }
        }



        

        /// <summary>
        /// 当前选中套餐
        /// </summary>
        public ItemMedical CurrentPackage
        {
            get { return this.currentPackage; }
            set
            {
                this.currentPackage = value;
            }
        }


        public frmMedicalPackage()
        {
            InitializeComponent();

            this.Load += new EventHandler(frmPackage_Load);
        }


        private void frmPackage_Load(object sender, EventArgs e)
        {
            this.InitControls();
            this.SetPackageInfo();
        }

        private void InitControls()
        {
            this.cmbValid.Items.Clear();
            this.StatusList = constantMgr.GetList("PACKAGESTATUS");
            this.cmbValid.AddItems(this.StatusList);
        }

        private void SetPackageInfo()
        {
            if (this.CurrentPackage == null || string.IsNullOrEmpty(this.currentPackage.PackageId))
            {
                return;
            }

            this.tbName.Text = this.CurrentPackage.PackageName;
            this.tbMony.Text = this.CurrentPackage.PackageCost.ToString();
            this.cmbValid.Tag = this.CurrentPackage.ValidState;
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
                this.CurrentPackage = new ItemMedical();

                this.CurrentPackage.CreateEnvironment.ID = FS.FrameWork.Management.Connection.Operator.ID;
                this.CurrentPackage.CreateEnvironment.OperTime = DateTime.Now;
            }

            this.CurrentPackage.PackageName = this.tbName.Text;
            this.CurrentPackage.SpellCode = convert.ToSpellCode(this.tbName.Text);
            this.CurrentPackage.InputCode = "";
            this.CurrentPackage.PackageCost = decimal.Parse(this.tbMony.Text);
            this.CurrentPackage.ValidState = this.cmbValid.Tag.ToString();
            this.currentPackage.Memo = this.rbMemo.Text;
            this.CurrentPackage.OperEnvironment.ID = FS.FrameWork.Management.Connection.Operator.ID;
            this.CurrentPackage.OperEnvironment.OperTime = DateTime.Now;


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

            if (string.IsNullOrEmpty(this.tbMony.Text))
            {
                tipStr += "套餐金额不能为空； ";
                rtn = false;
            }

            if (string.IsNullOrEmpty(this.cmbValid.Tag.ToString()))
            {
                tipStr += "套餐状态不能为空； ";
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


            if (this.editMode)
            {
                if (this.accountManager.UpdateMedicalPackage(this.CurrentPackage) <= 0)
                {
                    MessageBox.Show("保存套餐信息失败：" + this.accountManager.Err);
                    return rtn;
                }
            }
            else
            {
                if (this.accountManager.AddMedicalPackage(this.CurrentPackage) < 0)
                {
                    MessageBox.Show("保存套餐信息失败：" + this.accountManager.Err);
                    return rtn;
                }
            }

            this.SaveState = true;
            return true;
        }



        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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


        private void Clear()
        {
            //this.CurrentPackage = null;
            this.tbName.Text = "";
            this.tbMony.Text = "";
            this.cmbValid.Tag = "";
            this.rbMemo.Text = "";
            this.lbTip.Text = "";
            this.editMode = false;
        }
    }
}
