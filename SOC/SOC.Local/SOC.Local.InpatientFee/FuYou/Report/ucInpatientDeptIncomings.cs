using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.FuYou.Report
{
    public partial class ucInpatientDeptIncomings : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInpatientDeptIncomings()
        {
            InitializeComponent();
            al = this.inpatientFeeMgr.QueryFinanceAssorting();
            foreach (FS.FrameWork.Models.NeuObject obj in al)
            {
                if (!ht.ContainsKey(obj.ID))
                {
                    ht.Add(obj.ID, obj.Name);
                }
            }
        }

        Function.InpatientFee inpatientFeeMgr = new Function.InpatientFee();
        ArrayList al = new ArrayList();
        Hashtable ht = new Hashtable();

        bool isQueryAllDept = true;

        public bool IsQueryAllDept
        {
            get
            {
                return this.isQueryAllDept;
            }
            set
            {
                this.isQueryAllDept = value;
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            DataSet ds = new DataSet();
            string strDeptID = "ALL";
            this.dtpBeginDate.Value = new DateTime(this.dtpBeginDate.Value.Year, 
                this.dtpBeginDate.Value.Month, this.dtpBeginDate.Value.Day, 00, 00, 00);
            this.dtpEndDate.Value = new DateTime(this.dtpEndDate.Value.Year, 
                this.dtpEndDate.Value.Month, this.dtpEndDate.Value.Day, 23, 59, 59);

            this.lbQueryDate.Text = "统计日期：" + this.dtpBeginDate.Value.ToString() + "至" + this.dtpEndDate.Value.ToString();
            if (isQueryAllDept)
            {
                strDeptID = "ALL";
            }
            else
            {
                strDeptID = ((FS.HISFC.Models.Base.Employee)this.inpatientFeeMgr.Operator).Dept.ID;
            }
            if (this.inpatientFeeMgr.QueryInpatientDeptIncoming(strDeptID, this.dtpBeginDate.Value, this.dtpEndDate.Value, ref ds) == -1)
            {
                MessageBox.Show("查询失败！");
                return -1;
            }
            this.neuSpread1_Sheet1.DataSource = ds.Tables[0].DefaultView;
            for (int i = 1; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                int m = 0;
                for (int j = 0; j < this.neuSpread1_Sheet1.RowCount; j++)
                {
                    if (this.neuSpread1_Sheet1.Cells[j, i].Text == "0.00")
                    {
                        m++;
                    }
                }
                if (m == this.neuSpread1_Sheet1.RowCount)
                {
                    this.neuSpread1_Sheet1.Columns[i].Visible = false;
                }
            }
            this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
            this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = "合计：";
            this.neuSpread1_Sheet1.RowHeader.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            for (int i = 1; i < this.neuSpread1_Sheet1.ColumnCount; i++)
            {
                decimal totCost = 0;
                for (int j = 0; j < this.neuSpread1_Sheet1.RowCount - 1; j++)
                {
                    totCost += FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[j, i].Text);
                }
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, i].Text = totCost.ToString();
            }
            this.neuSpread1_Sheet1.Columns.Add(this.neuSpread1_Sheet1.Columns.Count, 1);
            this.neuSpread1_Sheet1.Columns[this.neuSpread1_Sheet1.Columns.Count - 1].Label = "合计";
            this.neuSpread1_Sheet1.ColumnHeader.Columns[this.neuSpread1_Sheet1.Columns.Count - 1].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.neuSpread1_Sheet1.Columns[this.neuSpread1_Sheet1.Columns.Count - 1].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                decimal totCost = 0;
                for (int j = 1; j < this.neuSpread1_Sheet1.ColumnCount - 1; j++)
                {
                    if (ht.ContainsValue(this.neuSpread1_Sheet1.Columns[j].Label))
                    {
                        totCost += FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, j].Text);
                        this.neuSpread1_Sheet1.ColumnHeader.Columns[j].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                        this.neuSpread1_Sheet1.Columns[j].Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                    }
                }
                this.neuSpread1_Sheet1.Cells[i, this.neuSpread1_Sheet1.Columns.Count - 1].Text = totCost.ToString();
            }

            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.None;
            //print.IsCanCancel = false;
            //print.PrintDocument.DefaultPageSettings.Landscape = true;
            //print.PrintPreview(0, 0, this.pMain);
            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            return this.neuSpread1.Export();
        }
    }
}
