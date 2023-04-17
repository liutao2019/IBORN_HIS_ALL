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
    public delegate int DeleteDelegate();

    public partial class ucPackageInfoEdit : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 通用转化类
        /// </summary>
        FS.FrameWork.Function.NConvert convert = new FS.FrameWork.Function.NConvert();

        #region 当前对象

        /// <summary>
        /// 当前选中套餐
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.Package currentPackage = null;

        /// <summary>
        /// 当前选中子套餐
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.Package currentChildPackage = null;

        /// <summary>
        /// 当前编辑的明细
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.PackageDetail currentDetail = null;

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

        /// <summary>
        /// 当前选中子套餐
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.Package CurrentChildPackage
        {
            get { return this.currentChildPackage; }
            set
            {
                this.currentChildPackage = (value == null) ? null : value.Clone();
                this.SetChildPackageInfo();
            }
        }

        /// <summary>
        /// 当前编辑的明细
        /// </summary>
        public FS.HISFC.Models.MedicalPackage.PackageDetail CurrentDetail
        {
            get { return this.currentDetail; }
            set 
            {
                this.currentDetail = (value == null) ? null : value.Clone();
                this.ucPackageItemSelect1.CurrentDetail = value;
            }
        }

        #endregion

        #region 下拉框选择字典

        /// <summary>
        /// 子套餐类别
        /// </summary>
        private ArrayList categoryList = new ArrayList();

        /// <summary>
        /// 套餐状态
        /// </summary>
        private ArrayList statusList = new ArrayList();

        /// <summary>
        /// 子套餐范围
        /// </summary>
        private ArrayList rangeList = new ArrayList();

        #endregion

        #region 各类委托

        /// <summary>
        /// 删除明细
        /// </summary>
        //public DeleteDelegate DeleteDetail;

        /// <summary>
        /// 项目选择器选择项目后调用代理函数
        /// </summary>
        public SelectItemDelegate SetNewDetail;

        /// <summary>
        /// 修改函数
        /// </summary>
        public ModifyDetailDelegate ModifyDetail;

        #endregion

        public ucPackageInfoEdit()
        {
            InitializeComponent();
            InitControls();
            BindEvents();
        }

        private void BindEvents()
        {
            this.tbName.TextChanged += new EventHandler(tbName_TextChanged);
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            this.tbUserCode.Text = convert.ToSpellCode(this.tbName.Text);
        }

        private void InitControls()
        {
            try
            {
                this.categoryList = constantMgr.GetList("PACKAGETYPE");
                this.statusList = constantMgr.GetList("PACKAGESTATUS");
                this.rangeList = constantMgr.GetList("PACKAGERANGE");
            }
            catch
            {
            }

            //初始化下拉框
            this.cmbPackageType.AddItems(this.categoryList);
            this.cmbPackageRange.AddItems(this.rangeList);
            this.cmbValid.AddItems(this.statusList);

            //初始化项目选择控件
            this.ucPackageItemSelect1.Init();
            this.ucPackageItemSelect1.SetNewDetail = setNewDetail;
            this.ucPackageItemSelect1.ModifyDetail = modifyDetail;
        }

        /// <summary>
        /// 新增明细
        /// </summary>
        /// <param name="item"></param>
        private void setNewDetail(FS.HISFC.Models.Base.Item item)
        {
            this.currentDetail = this.ucPackageItemSelect1.CurrentDetail.Clone();
            this.SetNewDetail(item);
        }

        /// <summary>
        /// 修改明细
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="field"></param>
        private void modifyDetail(FS.HISFC.Models.MedicalPackage.PackageDetail detail,EnumDetailFieldList field)
        {
            this.ModifyDetail(detail, field);
        }

        /// <summary>
        /// 双击显示明细在选择器上
        /// </summary>
        /// <returns></returns>
        public int PresentDetail()
        {
            return this.ucPackageItemSelect1.PresentDetail();
        }

        private void Clear()
        {
            this.tbName.Text = "";
            this.tbUserCode.Text = "";
            this.cmbPackageType.Tag = "";
            this.cmbPackageRange.Tag = "";
            this.cmbValid.Tag = "";
            this.rbMemo.Text = "";
            this.chkMainFlag.Checked = false;
            this.chkSelfChoose.Checked = false;
            this.lblMoney.Text = "";
        }


        /// <summary>
        /// 设置总金额显示
        /// </summary>
        /// <param name="totFee"></param>
        public void SetTotFee(decimal totFee)
        {
            this.lblMoney.Text = "项目包总金额：" + totFee.ToString();
            //this.SetTotFee(totFee);
        }

        /// <summary>
        /// 设置子套餐信息
        /// </summary>
        public void SetChildPackageInfo()
        {
            this.Clear();
            this.CurrentDetail = null;
            this.ucPackageItemSelect1.CurrentChildPackage = this.CurrentChildPackage;

            if (this.CurrentChildPackage == null || string.IsNullOrEmpty(this.CurrentChildPackage.ID))
            {
                return;
            }

            this.tbName.Text = this.CurrentChildPackage.Name;
            this.tbUserCode.Text = this.CurrentChildPackage.UserCode;
            this.cmbValid.Tag = this.CurrentChildPackage.IsValid ? "1":"0";
            this.cmbPackageType.Tag = this.CurrentChildPackage.PackageType;
            this.cmbPackageRange.Tag = (int)this.CurrentChildPackage.UserType;
            this.chkMainFlag.Checked = this.CurrentChildPackage.MainFlag == "1";
            this.chkSelfChoose.Checked = this.CurrentChildPackage.SpecialFlag == "1";
            this.rbMemo.Text = this.CurrentChildPackage.Memo;
        }

        /// <summary>
        /// 非法判断
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        private bool ValidCheck(ref string errMsg)
        {
            bool rtn = true;
            errMsg = string.Empty;

            if (this.CurrentPackage == null || string.IsNullOrEmpty(this.CurrentPackage.ID))
            {
                errMsg += "当前套餐不能为空； ";
                rtn = false;
            }

            if (string.IsNullOrEmpty(this.tbName.Text))
            {
                errMsg += "项目包名称不能为空； ";
                rtn = false;
            }

            if (string.IsNullOrEmpty(this.cmbValid.Tag.ToString()))
            {
                errMsg += "项目包状态不能为空； ";
                rtn = false;
            }

            if (string.IsNullOrEmpty(this.cmbPackageType.Tag.ToString()))
            {
                errMsg += "项目包类型不能为空； ";
                rtn = false;
            }

            if (string.IsNullOrEmpty(this.cmbPackageRange.Tag.ToString()))
            {
                errMsg += "项目包范围不能为空； ";
                rtn = false;
            }

            return rtn;
        }

        /// <summary>
        /// 获取子套餐信息
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>
        public FS.HISFC.Models.MedicalPackage.Package GetChildPackageInfo(ref string errMsg)
        {
            FS.HISFC.Models.MedicalPackage.Package rtn = null;

            if (!ValidCheck(ref errMsg))
            {
                return rtn;
            }

            if (this.CurrentChildPackage == null)
            {
                this.currentChildPackage = new FS.HISFC.Models.MedicalPackage.Package();
            }

            this.currentChildPackage.Name = this.tbName.Text;
            this.currentChildPackage.SpellCode = convert.ToSpellCode(this.currentPackage.Name);
            this.currentChildPackage.UserCode = this.tbUserCode.Text;
            this.currentChildPackage.IsValid = this.cmbValid.Tag.ToString() == "1" ? true : false;
            this.currentChildPackage.PackageType = this.cmbPackageType.Tag.ToString();
            this.currentChildPackage.UserType = (FS.HISFC.Models.Base.ServiceTypes)Enum.ToObject(typeof(FS.HISFC.Models.Base.ServiceTypes), FS.FrameWork.Function.NConvert.ToInt32((this.cmbPackageRange.Tag.ToString())));
            this.currentChildPackage.PackageClass = "2";  // 1-套餐，2-子套餐
            this.currentChildPackage.ParentCode = this.CurrentPackage.ID;
            this.currentChildPackage.ComboFlag = "0";
            this.currentChildPackage.MainFlag = this.chkMainFlag.Checked ? "1" : "0";
            this.currentChildPackage.SpecialFlag = this.chkSelfChoose.Checked ? "1" : "0";
            this.currentChildPackage.Memo = this.rbMemo.Text;

            return currentChildPackage;
        }

    }
}
