using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.OutpatientFee.Query
{
    public partial class frmInvoiceList : Form
    {
        public frmInvoiceList()
        {
            InitializeComponent();
        }

        #region 变量

        /// <summary>
        /// 定义时间选择发票委托
        /// </summary>
        /// <param name="invoiceNo"></param>
        public delegate void SelectInvoice(string invoiceNo);

        /// <summary>
        /// 选择发票事件
        /// </summary>
        public event SelectInvoice SelectInvoiceNO;

        /// <summary>
        /// 费用业务类
        /// </summary>
        private FS.HISFC.BizLogic.Fee.FeeReport feeMgr = new FS.HISFC.BizLogic.Fee.FeeReport();

        #endregion

        #region 方法

        public void Query(string queryType, string value, string begin, string end)
        {
            DataSet dsResult = new DataSet();

            if (this.feeMgr.QueryInvoiceList(queryType, value, begin, end, ref dsResult) == -1)
            {
                MessageBox.Show("查询发票的费用明细失败!");
                return;
            }

            DataTable dt = dsResult.Tables[0];

            if (dt.Rows.Count <= 0)
            {
                return;
            }
            else if (dt.Rows.Count == 1)
            {
                if (this.SelectInvoiceNO != null)
                {
                    this.SelectInvoiceNO(dt.Rows[0][0].ToString());
                }
                
            }
            else
            {
                this.neuSpread1_Sheet1.Rows.Count = 0;
                int index = this.neuSpread1_Sheet1.Rows.Count;

                foreach (DataRow dr in dt.Rows)
                {
                    this.neuSpread1_Sheet1.Rows.Add(index, 1);
                    FarPoint.Win.Spread.CellType.TextCellType txtType = new FarPoint.Win.Spread.CellType.TextCellType();
                    FarPoint.Win.Spread.CellType.NumberCellType numType = new FarPoint.Win.Spread.CellType.NumberCellType();

                    this.neuSpread1_Sheet1.Cells[index, 0].CellType = txtType;
                    this.neuSpread1_Sheet1.Cells[index, 0].Text = dr[0].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 1].CellType = txtType;
                    this.neuSpread1_Sheet1.Cells[index, 1].Text = dr[1].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 2].CellType = txtType;
                    this.neuSpread1_Sheet1.Cells[index, 2].Text = dr[2].ToString();

                    this.neuSpread1_Sheet1.Cells[index, 3].CellType = numType;
                    this.neuSpread1_Sheet1.Cells[index, 3].Text = dr[3].ToString();
                    this.neuSpread1_Sheet1.Cells[index, 4].Text = dr[4].ToString();

                    index++;
                }

                this.ShowDialog();
            }
        }

        #endregion

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            string invoiceNo = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRow.Index, 0].Text;

            if (this.SelectInvoiceNO != null)
            {
                this.SelectInvoiceNO(invoiceNo);
            }
            

            if (this != null )
            {
                this.Close();
            }
        }

    }
}
