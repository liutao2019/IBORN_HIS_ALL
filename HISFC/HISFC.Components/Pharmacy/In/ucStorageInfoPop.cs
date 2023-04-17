using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.Pharmacy.In
{
    /// <summary>
    /// [控件描述: 药品库存明细弹出查看专用控件 {3CF95FE3-773C-4b9a-90FA-ECAE01642FAE}]
    /// [创 建 人: 孙久海]
    /// [创建时间; 2010-9-25]
    /// </summary>
    public partial class ucStorageInfoPop : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        #region 构造函数

        public ucStorageInfoPop()
        {
            InitializeComponent();
        }

        public ucStorageInfoPop(string drugCode, string StockDept, bool isShowPackUnitQty)
        {
            InitializeComponent();
            this.isShowPackUnit = isShowPackUnitQty;
            this.gb2.Visible = false;
            this.gb1.Dock = DockStyle.Fill;
            this.ShowStorageInfo(drugCode, StockDept, "");            
        }

        public ucStorageInfoPop(string drugCode, string StockDept1, string StockDept2, bool isShowPackUnitQty)
        {
            InitializeComponent();
            this.isShowPackUnit = isShowPackUnitQty;
            this.ShowStorageInfo(drugCode, StockDept1, StockDept2);
        }

        #endregion

        #region 变量

        /// <summary>
        /// 是否显示包装单位，false显示最小单位
        /// </summary>
        private bool isShowPackUnit = false;
        
        /// <summary>
        /// 药品管理业务类
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// 药品常数管理业务类
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();

        /// <summary>
        /// 基础管理整合业务类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        #endregion

        #region 属性

        /// <summary>
        /// 是否显示包装单位，false显示最小单位
        /// </summary>
        public bool IsShowPackUnit
        {
            get 
            { 
                return isShowPackUnit; 
            }
            set 
            { 
                isShowPackUnit = value; 
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 显示库存明细信息
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="StockDept1"></param>
        /// <param name="StockDept2"></param>
        private void ShowStorageInfo(string drugCode, string StockDept1, string StockDept2)
        {            
            //Set groupbox title
            FS.HISFC.Models.Base.Department deptObj = this.managerIntegrate.GetDepartment(StockDept1);
            this.gb1.Text = "【" + deptObj.Name + "】库存信息";
            deptObj = this.managerIntegrate.GetDepartment(StockDept2);
            this.gb2.Text = "【" + deptObj.Name + "】库存信息";
            //Load main info of drug
            FS.HISFC.Models.Pharmacy.Item itemObj = this.itemManager.GetItem(drugCode);
            if (itemObj.Product.Producer.ID != "")
            {
                FS.HISFC.Models.Pharmacy.Company compObj = this.consManager.QueryCompanyByCompanyID(itemObj.Product.Producer.ID);
                this.lblDrugInfo.Text = itemObj.Name + " （" + itemObj.Specs + "）<" + compObj.Name + ">";
            }
            else
            {
                this.lblDrugInfo.Text = itemObj.Name + " （" + itemObj.Specs + "）";
            }

            //Show storage info 1
            if (StockDept1 != "")
            {
                decimal qtyAll = 0;
                decimal qtyValid = 0;
                decimal qtyInvalid = 0;

                ArrayList alStorage1 = this.itemManager.QueryStorageList(StockDept1, drugCode);
                this.fpStorageList1.RowCount = 0;
                for (int i = 0; i < alStorage1.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.Storage storeObj = alStorage1[i] as FS.HISFC.Models.Pharmacy.Storage;
                    this.fpStorageList1.RowCount = i + 1;
                    if (this.isShowPackUnit)
                    {
                        decimal packNum = storeObj.StoreQty / storeObj.Item.PackQty;
                        this.fpStorageList1.Cells[i, 0].Text = packNum.ToString("0.00");
                    }
                    else
                    {
                        this.fpStorageList1.Cells[i, 0].Text = storeObj.StoreQty.ToString("0.00");
                    }                    
                    this.fpStorageList1.Cells[i, 1].Text = storeObj.Item.MinUnit;
                    this.fpStorageList1.Cells[i, 2].Text = storeObj.ValidTime.ToString();
                    this.fpStorageList1.Cells[i, 3].Text = storeObj.BatchNO;
                    if (storeObj.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                    {
                        this.fpStorageList1.Cells[i, 4].Text = "在用";
                        qtyValid += storeObj.StoreQty;
                    }
                    else
                    {
                        this.fpStorageList1.Cells[i, 4].Text = "停用";
                        qtyInvalid += storeObj.StoreQty;
                    }
                    qtyAll += storeObj.StoreQty;
                }
                if (this.isShowPackUnit)
                {
                    qtyAll = qtyAll / itemObj.PackQty;
                    qtyInvalid = qtyInvalid / itemObj.PackQty;
                    qtyValid = qtyValid / itemObj.PackQty;
                    this.lblStorageInfo1.Text = "库存总量: " + qtyAll.ToString("0.00") + itemObj.PackUnit + " 其中<在用>: " + qtyValid.ToString("0.00") + itemObj.PackUnit + " <停用>: " + qtyInvalid.ToString("0.00") + itemObj.PackUnit;
                }
                else
                {
                    this.lblStorageInfo1.Text = "库存总量: " + qtyAll.ToString("0.00") + itemObj.MinUnit + " 其中<在用>: " + qtyValid.ToString("0.00") + itemObj.MinUnit + " <停用>: " + qtyInvalid.ToString("0.00") + itemObj.MinUnit;
                }
            }            

            //Show storage info 2
            if (StockDept2 != "")
            {
                decimal qtyAll = 0;
                decimal qtyValid = 0;
                decimal qtyInvalid = 0;

                ArrayList alStorage2 = this.itemManager.QueryStorageList(StockDept2, drugCode);
                this.fpStorageList2.RowCount = 0;
                for (int i = 0; i < alStorage2.Count; i++)
                {
                    FS.HISFC.Models.Pharmacy.Storage storeObj = alStorage2[i] as FS.HISFC.Models.Pharmacy.Storage;
                    this.fpStorageList2.RowCount = i + 1;
                    if (this.isShowPackUnit)
                    {
                        decimal packNum = storeObj.StoreQty / storeObj.Item.PackQty;
                        this.fpStorageList2.Cells[i, 0].Text = packNum.ToString("0.00");
                    }
                    else
                    {
                        this.fpStorageList2.Cells[i, 0].Text = storeObj.StoreQty.ToString("0.00");
                    }
                    this.fpStorageList2.Cells[i, 1].Text = storeObj.Item.MinUnit;
                    this.fpStorageList2.Cells[i, 2].Text = storeObj.ValidTime.ToString();
                    this.fpStorageList2.Cells[i, 3].Text = storeObj.BatchNO;
                    if (storeObj.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                    {
                        this.fpStorageList2.Cells[i, 4].Text = "在用";
                        qtyValid += storeObj.StoreQty;
                    }
                    else
                    {
                        this.fpStorageList2.Cells[i, 4].Text = "停用";
                        qtyInvalid += storeObj.StoreQty;
                    }
                    qtyAll += storeObj.StoreQty;
                }
                if (this.isShowPackUnit)
                {
                    qtyAll = qtyAll / itemObj.PackQty;
                    qtyInvalid = qtyInvalid / itemObj.PackQty;
                    qtyValid = qtyValid / itemObj.PackQty;
                    this.lblStorageInfo2.Text = "库存总量: " + qtyAll.ToString("0.00") + itemObj.PackUnit + " 其中<在用>: " + qtyValid.ToString("0.00") + itemObj.PackUnit + " <停用>: " + qtyInvalid.ToString("0.00") + itemObj.PackUnit;
                }
                else
                {
                    this.lblStorageInfo2.Text = "库存总量: " + qtyAll.ToString("0.00") + itemObj.MinUnit + " 其中<在用>: " + qtyValid.ToString("0.00") + itemObj.MinUnit + " <停用>: " + qtyInvalid.ToString("0.00") + itemObj.MinUnit;
                }                
            }
            this.neuButton1.Select();
            this.neuButton1.Focus();
        }

        #endregion

        #region 事件

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion

    }
}
