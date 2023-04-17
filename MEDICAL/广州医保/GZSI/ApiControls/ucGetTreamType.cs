using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace GZSI.ApiControls
{
    /// <summary>
    /// 待遇类型选择
    /// </summary>
    public partial class ucGetTreamType : UserControl
    {
        public ucGetTreamType()
        {
            InitializeComponent();
        }

        FS.FrameWork.Models.NeuObject treamType = null;
        public FS.FrameWork.Models.NeuObject TreamTypeObj
        {
            get { return treamType; }
        }

        DataTable dtTreamType = null;

        private string biz_type = "11";
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BizType
        {
            get { return biz_type; }
            set { biz_type = value; }
        }

        private ArrayList alTreamType = null;
        /// <summary>
        /// 待遇类型
        /// </summary>
        public ArrayList AlTreamType
        {
            set { alTreamType = value; }
        }

        private void ucGetTreamType_Load(object sender, EventArgs e)
        {
            if (alTreamType !=null && alTreamType.Count > 0)
            {
                DisPlayTreamType(alTreamType);
            }

        }

        private void DisPlayTreamType(ArrayList al)
        {
            if (al == null && al.Count == 0)
            {
                return;
            }
            Type str = typeof(System.String);
            dtTreamType = new DataTable();


            dtTreamType.Columns.AddRange(new DataColumn[]{new DataColumn("待遇类型代码", str),
                                                          new DataColumn("待遇类型名称", str), 
                                                          new DataColumn("备注", str)
                                                       });

            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                DataRow rowDisplay = dtTreamType.NewRow();

                rowDisplay["待遇类型代码"] = obj.ID;
                rowDisplay["待遇类型名称"] = obj.Name;
                rowDisplay["备注"] = obj.Memo;

                dtTreamType.Rows.Add(rowDisplay);
            }
            this.fpSpread1_Sheet1.DataSource = dtTreamType;
        }

        private void GetTreamType()
        {
            treamType = new FS.FrameWork.Models.NeuObject();
            int i = this.fpSpread1_Sheet1.ActiveRowIndex;
            if (this.fpSpread1_Sheet1.RowCount <= 0)
                return;
            treamType.ID = this.fpSpread1_Sheet1.Cells[i, 0].Text;
            treamType.Name = this.fpSpread1_Sheet1.Cells[i, 1].Text;
            treamType.Memo = this.fpSpread1_Sheet1.Cells[i, 2].Text;

            this.FindForm().Close();
        }

        private void fpSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            GetTreamType();
        }

        private void bOk_Click(object sender, EventArgs e)
        {
            GetTreamType();
        }

    }
}
