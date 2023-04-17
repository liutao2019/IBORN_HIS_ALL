using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.HISFC.Components.Pharmacy.Common.AdjustPrice
{
    public partial class ucAdjustMonitorReport : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        public ucAdjustMonitorReport()
        {
            InitializeComponent();
            this.btnExport.Click +=new EventHandler(btnExport_Click);
        }

        private DataTable dtDetail = null;

        private FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();

        public int Init(string adjustNum)
        {
            this.Clear();
            string strSQL = @"select * from pha_com_adjustmonitorrecord  t where t.adjust_num = '{0}'";

            strSQL = string.Format(strSQL, adjustNum);

            DataSet ds = new DataSet();

            consMgr.ExecQuery(strSQL,ref ds);

            if(ds.Tables[0]!= null && ds.Tables[0].Rows.Count >0)
            {
                dtDetail = ds.Tables[0];
                this.neuSpreadDetail_Sheet1.DataSource = dtDetail.DefaultView;
                this.SetFarpointWith();
            }
            return 1;
        }

        private void SetFarpointWith()
        {
            this.neuSpreadDetail_Sheet1.Columns.Get(0).Width = 59F;
            this.neuSpreadDetail_Sheet1.Columns.Get(1).Width = 70F;
            this.neuSpreadDetail_Sheet1.Columns.Get(2).Width = 68F;
            this.neuSpreadDetail_Sheet1.Columns.Get(3).Width = 150F;
            this.neuSpreadDetail_Sheet1.Columns.Get(4).Width = 98F;
            this.neuSpreadDetail_Sheet1.Columns.Get(5).Width = 45F;
            this.neuSpreadDetail_Sheet1.Columns.Get(6).Width = 62F;
            this.neuSpreadDetail_Sheet1.Columns.Get(7).Width = 69F;
            this.neuSpreadDetail_Sheet1.Columns.Get(8).Width = 52F;
            this.neuSpreadDetail_Sheet1.Columns.Get(9).Width = 58F;
            this.neuSpreadDetail_Sheet1.Columns.Get(10).Width = 58F;
            this.neuSpreadDetail_Sheet1.Columns.Get(11).Width = 58F;
            this.neuSpreadDetail_Sheet1.Columns.Get(12).Width = 58F;
            this.neuSpreadDetail_Sheet1.Columns.Get(13).Width = 127F;
            this.neuSpreadDetail_Sheet1.Columns.Get(14).Width = 0F;
        }


        private void Clear()
        {
            this.dtDetail = null;
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
}
