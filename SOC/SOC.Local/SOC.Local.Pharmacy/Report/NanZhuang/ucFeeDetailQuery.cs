using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Report.NanZhuang
{
    public partial class ucFeeDetailQuery : Base.BaseReport
    {

        private bool isCustomTextToTag = false;
        [Category("Query查询设置"),Description("查询时，是否将Text进行查询（将Text转换为Tag）")]
        public bool IsCustomTextToTag
        {
            get
            {
                return isCustomTextToTag;
            }
            set
            {
                isCustomTextToTag = value;
            }
        }

        public ucFeeDetailQuery()
        {
            InitializeComponent();

            this.SQLIndexs = "NanZhuang.Fee.FeeDetail.Query";
            this.MainTitle = "患者处方、治疗项目明细表";
            this.MidAdditionTitle = "";
            this.RightAdditionTitle = "";
            this.IsDeptAsCondition = false;
            this.QueryDataWhenInit = false;
            this.IsUseCustomType = true;
            this.CustomTypeSQL = @"select p.pact_code id, p.pact_name name, '' memo,fun_get_querycode(p.pact_name,1) spell_code,fun_get_querycode(p.pact_name,0) wb_code,p.pact_code user_code from fin_com_pactunitinfo p";

            this.QueryEndHandler += new DelegateQueryEnd(ucFeeDetailQuery_QueryEndHandler);
            this.OperationStartHandler += new DelegateOperationStart(ucFeeDetailQuery_OperationStartHandler);
        }

        void ucFeeDetailQuery_OperationStartHandler(string type)
        {
            if (type == "query")
            {
                if (this.isCustomTextToTag)
                {
                    this.cmbCustomType.Tag = this.cmbCustomType.Text;
                }
            }
        }

        void ucFeeDetailQuery_QueryEndHandler()
        {
            if (this.fpSpread1_Sheet1.RowCount == 0)
            {
                return;
            }
            //this.fpSpread1_Sheet1.Columns[0].Visible = false;
            string lastRowMemo = this.fpSpread1_Sheet1.Cells[this.fpSpread1_Sheet1.RowCount - 1, 0].Text;
            for (int rowIndex = this.fpSpread1_Sheet1.RowCount - 1; rowIndex > -1; rowIndex--)
            {
                string nowRowMemo = this.fpSpread1_Sheet1.Cells[rowIndex, 0].Text;
               

                if (lastRowMemo != nowRowMemo)
                {
                    this.fpSpread1_Sheet1.AddRows(rowIndex + 1, 1);
                    this.fpSpread1_Sheet1.Cells[rowIndex + 1, 1].ColumnSpan = this.fpSpread1_Sheet1.ColumnCount - 2;
                    this.fpSpread1_Sheet1.Cells[rowIndex + 1, 1].Text = lastRowMemo;
                    this.fpSpread1_Sheet1.Cells[rowIndex + 1, 1].Font = new Font("宋体", this.Font.Size, FontStyle.Bold);
                    this.fpSpread1_Sheet1.Rows[rowIndex + 1].Tag = "SpanRow";
                    lastRowMemo = nowRowMemo;
                }

                if (rowIndex == 0 && lastRowMemo == nowRowMemo)
                {
                    this.fpSpread1_Sheet1.AddRows(0, 1);
                    this.fpSpread1_Sheet1.Cells[0, 1].ColumnSpan = this.fpSpread1_Sheet1.ColumnCount - 2;
                    this.fpSpread1_Sheet1.Cells[0, 1].Text = nowRowMemo;
                    this.fpSpread1_Sheet1.Cells[0, 1].Font = new Font("宋体", this.Font.Size, FontStyle.Bold);
                    this.fpSpread1_Sheet1.Rows[0].Tag = "SpanRow";
                }
            }
        }
    }
}
