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
    public partial class ucPackageInfo : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 常数管理类
        /// </summary>
        FS.HISFC.BizLogic.Manager.Constant constantMgr = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// 套餐维护业务类
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.MedicalPackage.Package packageProcess = new FS.HISFC.BizProcess.Integrate.MedicalPackage.Package();


        /// <summary>
        /// 当前选中套餐
        /// </summary>
        private FS.HISFC.Models.MedicalPackage.Package currentPackage = null;

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
                this.currentPackage = (value == null)?null:value.Clone();
                setInfoDisplay();
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
            }
        }

        public ucPackageInfo()
        {
            InitializeComponent();
            try
            {
                this.categoryList = constantMgr.GetList("PACKAGETYPE");
            }
            catch { }
        }

        /// <summary>
        /// 设置信息显示
        /// </summary>
        private void setInfoDisplay()
        {
            this.InfoDisplayClear();
            if (this.currentPackage != null)
            {
                this.lblName.Text = this.currentPackage.Name;
                this.lblStatus.Text = this.currentPackage.IsValid ? "启用" : "禁用";
                this.lblTotFee.Text = "";
                this.lblCreateTime.Text = this.currentPackage.CreateTime.ToString();
                this.lblMemo.Text = this.currentPackage.Memo;

                foreach (FS.HISFC.Models.Base.Const cst in categoryList)
                {
                    if (this.currentPackage.PackageType == cst.ID)
                    {
                        this.lblType.Text = cst.Name;
                        break;
                    }
                }

                switch (this.currentPackage.UserType)
                {
                    case FS.HISFC.Models.Base.ServiceTypes.C:
                        this.lblRange.Text = "门诊";
                        break;
                    case FS.HISFC.Models.Base.ServiceTypes.I:
                        this.lblRange.Text = "住院";
                        break;
                    case FS.HISFC.Models.Base.ServiceTypes.T:
                        this.lblRange.Text = "体检";
                        break;
                    case FS.HISFC.Models.Base.ServiceTypes.A:
                        this.lblRange.Text = "全部";
                        break;
                }

                this.SetTotFee();
            }
        }

        /// <summary>
        /// 清除显示信息
        /// </summary>
        private void InfoDisplayClear()
        {
            this.lblName.Text = "";
            this.lblType.Text = "";
            this.lblRange.Text = "";
            this.lblStatus.Text = "";
            this.lblTotFee.Text = "";
            this.lblCreateTime.Text = "";
            this.lblMemo.Text = "";
        }

        public void SetTotFee()
        {
            decimal tot_fee = 0.00m;
            ArrayList childPackageList = this.packageProcess.QueryPackageByParentCode(this.currentPackage.ID);
            try
            {
                if (childPackageList != null)
                {
                    foreach (FS.HISFC.Models.MedicalPackage.Package childPackage in childPackageList)
                    {
                        ArrayList detailList = this.packageProcess.GetPackageItemByPackageID(childPackage.ID);

                        foreach (FS.HISFC.Models.MedicalPackage.PackageDetail detail in detailList)
                        {
                            if (detail.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                            {
                                if (detail.UnitFlag.Equals("0"))
                                {
                                    tot_fee += Math.Round((detail.Item.Price / detail.Item.PackQty) * detail.Item.Qty, 2);
                                }
                                else
                                {
                                    tot_fee += detail.Item.Price * detail.Item.Qty;
                                }
                            }
                            else
                            {
                                tot_fee += detail.Item.Price * detail.Item.Qty;
                            }
                        }

                    }
                }

                this.lblTotFee.Text = "￥" + tot_fee.ToString();
            }
            catch
            {
                this.lblTotFee.Text = "获取价格失败！";
            }

                
        }
    }
}
