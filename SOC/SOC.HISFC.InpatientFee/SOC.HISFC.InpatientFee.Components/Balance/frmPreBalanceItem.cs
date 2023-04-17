using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.SOC.HISFC.InpatientFee.Components.Balance
{
    public delegate int DelegateArrSet(string inpatientno);
    public partial class frmPreBalanceItem : Form
    {
        public event DelegateArrSet SetSelectedItem;//  //{C4231074-D350-4df9-AF7C-C37124B44B80}
        public frmPreBalanceItem()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 预结算管理类
        /// </summary>
        /// 
        FS.HISFC.BizLogic.Fee.PreBalanceLogic prebalancelogic = new FS.HISFC.BizLogic.Fee.PreBalanceLogic();

        public void SetFeeItemList( string prebalanceno)
        {
          ArrayList arList=  prebalancelogic.QueryPreBalanceDetailByPreBalanceNo(prebalanceno);
          FpItemList_Sheet1.RowCount=0;
          foreach (FS.HISFC.Models.Fee.Inpatient.PreBalanceList detail in arList)
          {

              this.FpItemList_Sheet1.Rows.Add(FpItemList_Sheet1.RowCount,1);
              this.FpItemList_Sheet1.Rows[FpItemList_Sheet1.RowCount - 1].Tag = detail;
              this.FpItemList_Sheet1.Cells[FpItemList_Sheet1.RowCount - 1, (int)PreDetailCols.ItemName].Text = detail.ITEM_NAME;
              this.FpItemList_Sheet1.Cells[FpItemList_Sheet1.RowCount - 1, (int)PreDetailCols.Spec].Text = detail.Spec+" "+detail.PACKAGE_NAME;
              this.FpItemList_Sheet1.Cells[FpItemList_Sheet1.RowCount - 1, (int)PreDetailCols.Price].Text = detail.UNIT_PRICE.ToString();
              this.FpItemList_Sheet1.Cells[FpItemList_Sheet1.RowCount - 1, (int)PreDetailCols.Unit].Text = detail.CURRENT_UNIT;
              this.FpItemList_Sheet1.Cells[FpItemList_Sheet1.RowCount - 1, (int)PreDetailCols.Qty].Text = detail.QTY.ToString();
              this.FpItemList_Sheet1.Cells[FpItemList_Sheet1.RowCount - 1, (int)PreDetailCols.PriceUnit].Text = detail.PRICEUNIT;
          }
        }

        /// <summary>
        /// 选中明细列枚举
        /// </summary>
        private enum PreDetailCols
        {
            /// <summary>
            /// 项目名称
            /// </summary>
            ItemName = 0,
            /// <summary>
            /// 规格
            /// </summary>
            Spec = 1,
            /// <summary>
            /// 单价
            /// </summary>
            Price = 2,
            /// <summary>
            /// 计价单位
            /// </summary>
           PriceUnit = 3,
            /// <summary>
            /// 总数量
            /// </summary>
            Qty = 4,
            /// <summary>
            /// 单位
            /// </summary>
            Unit = 5 
            
        }
    }
}
