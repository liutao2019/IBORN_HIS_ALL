using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;

namespace FS.SOC.HISFC.InpatientFee.Components.Balance
{
    public delegate int DelegateArrSet(ArrayList FeeItems,string Key);
    public partial class frmFeeItems : Form
    {
        /// <summary>
        /// 设置选择的Item
        /// </summary>
        public event DelegateArrSet SetSelectedItem;
 
        public frmFeeItems()
        {
            InitializeComponent();
        }

        public void SetFeeItemList(ArrayList FeeItems,string Key)
        {
            this.FpItemList_Sheet1.Tag = Key;
            this.FpItemList_Sheet1.RowCount = 0;

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList Item in FeeItems.Cast<FS.HISFC.Models.Fee.Inpatient.FeeItemList>()
                                                                                    .OrderBy(t=>t.ID))
            {
                string Spec = string.Empty;
                string Unit = string.Empty;

                //项目为检查检验的时候，规格显示为复合项目的名称
                if (!string.IsNullOrEmpty(Item.UndrugComb.ID) && Item.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.UnDrug)
                {
                    Spec = Item.UndrugComb.Name;
                }
                else
                {
                    Spec = Item.Item.Specs;
                }

                //如果为药品，则单位显示为包装单位
                if (Item.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item pharmacy = Item.Item as FS.HISFC.Models.Pharmacy.Item;
                    Unit = pharmacy.PackUnit;
                }
                else 
                {
                    Unit = Item.Item.PriceUnit;
                }

                this.FpItemList_Sheet1.Rows.Add(this.FpItemList_Sheet1.RowCount, 1);
                this.FpItemList_Sheet1.Cells[this.FpItemList_Sheet1.RowCount - 1, 0].Text = Item.Item.Name;
                this.FpItemList_Sheet1.Cells[this.FpItemList_Sheet1.RowCount - 1, 1].Text = Spec;
                this.FpItemList_Sheet1.Cells[this.FpItemList_Sheet1.RowCount - 1, 2].Value = Item.Item.Qty;
                this.FpItemList_Sheet1.Cells[this.FpItemList_Sheet1.RowCount - 1, 3].Text = Item.Item.PriceUnit;
            }
        }
    }
}
