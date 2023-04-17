using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Maintenance
{
    public partial class ucStorageQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        
        public ucStorageQuery()
        {
            InitializeComponent();
            this.btnExport.Click +=new EventHandler(btnExport_Click);
        }


        #region 当前操作的库存汇总信息
        /// <summary>
        /// 当前操作的库存汇总信息
        /// </summary>
        ArrayList alStorage = new ArrayList();

        public ArrayList AlStorage
        {
            get
            {
                return alStorage;
            }
            set
            {
                alStorage = value;
                this.SetData(alStorage);
            }
        }
        #endregion

        /// <summary>
        /// 设置数据显示
        /// </summary>
        /// <param name="alStorage"></param>
        private void SetData(ArrayList alStorage)
        {
            if (alStorage == null || alStorage.Count == 0)
            {
                return;
            }
            this.SetFarpoint();
            this.SetFarpointDetail(alStorage);
        }

     
        private void SetFarpoint()
        {
            this.SetFarpointColumns();
            this.SetFarpontDataType();
        }

        private void SetFarpontDataType()
        {
            for(int index = 0;index < this.neuSpreadDetail_Sheet1.Columns.Count;index++)
            {
                this.neuSpreadDetail_Sheet1.Columns[index].Locked = true;
            }
        }

        private void SetFarpointDetail(ArrayList alStorage)
        {
            decimal totSum = 0m;
            decimal packQty = 0m;
            decimal retaiPrice = 0m;
            string tradeName = string.Empty;
            string specs = string.Empty;
            string packUnit = string.Empty;
            string minUnit = string.Empty;
            foreach (FS.HISFC.Models.Pharmacy.Storage storageInfo in alStorage)
            {
                totSum += storageInfo.StoreQty;
                packQty = storageInfo.Item.PackQty;
                retaiPrice = storageInfo.Item.PriceCollection.RetailPrice;
                tradeName = storageInfo.Item.NameCollection.Name;
                specs = storageInfo.Item.Specs;
                packUnit = storageInfo.Item.PackUnit;
                minUnit = storageInfo.Item.MinUnit;
                this.neuSpreadDetail_Sheet1.Rows.Add(this.neuSpreadDetail_Sheet1.Rows.Count, 1);
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.科室名称].Text = FS.SOC.HISFC.BizProcess.Cache.Common.GetDeptName(storageInfo.StockDept.ID);
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.药品编码].Text = storageInfo.Item.ID;
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.自定义码].Text = storageInfo.Item.NameCollection.UserCode;
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.药品名称].Text = storageInfo.Item.NameCollection.Name;
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.规格].Text = storageInfo.Item.Specs;
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.数量1].Value = storageInfo.StoreQty/storageInfo.Item.PackQty;
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.包装单位].Text = storageInfo.Item.PackUnit;
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.数量2].Value = storageInfo.StoreQty;
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.最小单位].Text = storageInfo.Item.MinUnit;
                this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.库存金额].Value = (storageInfo.StoreQty / storageInfo.Item.PackQty) * storageInfo.Item.PriceCollection.RetailPrice;
            }
            this.nlbTitle.Text = "药品：" + tradeName + "  规格：" + specs +　"   库存总量：" + Math.Round(totSum / packQty,2) + packUnit +"(" + totSum + minUnit + ")";
            this.neuSpreadDetail_Sheet1.Rows.Add(this.neuSpreadDetail_Sheet1.Rows.Count, 1);
            this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.科室名称].Text = "合计：";
            this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.科室名称].Font = new Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.数量1].Value = totSum / packQty;
            this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.数量1].Font = new Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.数量2].Value = totSum;
            this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.数量2].Font = new Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.库存金额].Value = (totSum / packQty) * retaiPrice;
            this.neuSpreadDetail_Sheet1.Cells[this.neuSpreadDetail_Sheet1.Rows.Count - 1, (int)EnumColumnSet.库存金额].Font = new Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));

        }

        private void SetFarpointColumns()
        {
            FarPoint.Win.Spread.CellType.NumberCellType n1 = new FarPoint.Win.Spread.CellType.NumberCellType();
            n1.DecimalPlaces = 2;
            FarPoint.Win.Spread.CellType.TextCellType t1 = new FarPoint.Win.Spread.CellType.TextCellType();
            t1.WordWrap = true;
            System.Drawing.Font f1 = new Font("宋体", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            System.Drawing.Font f2 = new Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpreadDetail_Sheet1.Columns.Count = (int)EnumColumnSet.库存金额 + 1;
            this.neuSpreadDetail_Sheet1.Rows.Add(0, 1);
            this.neuSpreadDetail_Sheet1.Rows[0].Height = 45F;

            #region 设置赋值

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.科室名称].Width = 76F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.科室名称].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.科室名称].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.科室名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.科室名称].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.科室名称].Text = "科室名称";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.科室名称].Font = f1;


            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.药品编码].Width = 90F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.药品编码].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.药品编码].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.药品编码].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.药品编码].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.药品编码].Text = "药品编码";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.药品编码].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.自定义码].Width = 80F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.自定义码].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.自定义码].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.自定义码].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.自定义码].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.自定义码].Text = "自定义码";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.自定义码].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.药品名称].Width = 200F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.药品名称].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.药品名称].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.药品名称].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.药品名称].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.药品名称].Text = "药品名称";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.药品名称].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.规格].Width = 120F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.规格].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.规格].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.规格].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.规格].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.规格].Text = "规格";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.规格].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.数量1].Width = 60F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.数量1].CellType = n1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.数量1].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.数量1].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.数量1].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.数量1].CellType = t1;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.数量1].Text = "数量1";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.数量1].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.包装单位].Width = 55F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.包装单位].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.包装单位].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.包装单位].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.包装单位].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.包装单位].Text = "大单位";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.包装单位].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.数量2].Width = 60F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.数量2].CellType = n1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.数量2].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.数量2].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.数量2].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.数量2].CellType = t1;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.数量2].Text = "数量2";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.数量2].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.最小单位].Width = 55F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.最小单位].CellType = t1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.最小单位].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.最小单位].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Left;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.最小单位].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.最小单位].Text = "小单位";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.最小单位].Font = f1;

            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.库存金额].Width = 80F;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.库存金额].CellType = n1;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.库存金额].Font = f2;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.库存金额].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
            this.neuSpreadDetail_Sheet1.Columns[(int)EnumColumnSet.库存金额].VerticalAlignment = FarPoint.Win.Spread.CellVerticalAlignment.Center;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.库存金额].CellType = t1;
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.库存金额].Text = "库存金额";
            this.neuSpreadDetail_Sheet1.Cells[0, (int)EnumColumnSet.库存金额].Font = f1;

            #endregion
        }

        private void Clear()
        {
            this.neuSpreadDetail_Sheet1.RowCount = 0;
        }

        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            this.neuSpreadDetail.Export();
        }

    }

    enum EnumColumnSet
    { 
        科室名称,
        药品编码,
        自定义码,
        药品名称,
        规格,
        数量1,
        包装单位,
        数量2,
        最小单位,
        库存金额
    }
}
