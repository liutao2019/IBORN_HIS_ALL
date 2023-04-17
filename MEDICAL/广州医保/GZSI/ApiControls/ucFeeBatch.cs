using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using GZSI.ApiModel;

namespace GZSI.ApiControls
{
    /// <summary>
    /// 待遇类型选择
    /// </summary>
    public partial class ucFeeBatch : UserControl
    {
        public ucFeeBatch()
        {
            InitializeComponent();
        }

        FeeBatchInfo feeBatch = null;
        public FeeBatchInfo FeeBatch
        {
            get { return feeBatch; }
        }

        DataTable dtFeeBatch = null;

        private ArrayList alFeeBatch = null;
        /// <summary>
        /// 待遇类型
        /// </summary>
        public ArrayList AlFeeBatch
        {
            set { alFeeBatch = value; }
        }

        private void ucFeeBatch_Load(object sender, EventArgs e)
        {
            if (alFeeBatch != null && alFeeBatch.Count > 0)
            {
                DisPlayFeeBatch(alFeeBatch);
            }
        }

        private void DisPlayFeeBatch(ArrayList al)
        {
            if (al == null && al.Count == 0)
            {
                return;
            }
            Type str = typeof(System.String);
            dtFeeBatch = new DataTable();

            dtFeeBatch.Columns.AddRange(new DataColumn[]{new DataColumn("医院编号", str),
                                                          new DataColumn("就医登记号", str), 
                                                          new DataColumn("个人电脑号", str),
                                                          new DataColumn("姓名", str),
                                                          new DataColumn("费用批次", str),
                                                          new DataColumn("费用金额", str)
                                                       });

            foreach (FeeBatchInfo obj in al)
            {
                DataRow rowDisplay = dtFeeBatch.NewRow();

                rowDisplay["医院编号"] = obj.hospital_id;
                rowDisplay["就医登记号"] = obj.serial_no;
                rowDisplay["个人电脑号"] = obj.Indi_id;
                rowDisplay["姓名"] = obj.Name;
                rowDisplay["费用批次"] = obj.fee_batch;
                rowDisplay["费用金额"] = obj.sum_fee;
                dtFeeBatch.Rows.Add(rowDisplay);
            }
            this.fpSpread1_Sheet1.DataSource = dtFeeBatch;
        }

        private void GetFeeBatch()
        {
            feeBatch = new  FeeBatchInfo();
            int i = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (this.fpSpread1_Sheet1.RowCount <= 0)
                return;
            feeBatch.fee_batch = this.fpSpread1_Sheet1.Cells[i, 4].Text;
            this.FindForm().Close();

        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            GetFeeBatch();
        }
    }
}
