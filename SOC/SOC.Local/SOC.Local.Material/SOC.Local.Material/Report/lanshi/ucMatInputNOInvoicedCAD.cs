using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Material.Report.lanshi
{
    public partial class ucMatInputNOInvoicedCAD : Base.ucCrossQueryBaseForFarPoint
    {
        public ucMatInputNOInvoicedCAD()
        {
            InitializeComponent();
        }

        FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        string deptCode = string.Empty;

        protected override void OnLoad()
        {
            FS.HISFC.Models.Base.Employee emp = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            ArrayList al = dept.GetDeptmentByType("L");
            this.cmbStorage.AddItems(al);
            //deptCode = emp.Dept.ID;
            //this.cmbStorage.Tag = emp.Dept.ID;

            //this.neuSpread1_Sheet1.SetText(0, 0, Title);
            base.OnLoad();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QuerySqlTypeValue = QuerySqlType.id;

            this.QuerySql = "SOC.Local.Material.Report.Input.NOInvoiced.CompAndKind";

            QueryParams.Clear();
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", deptCode, ""));
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", this.dtpBeginTime.Value.ToString(), ""));
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", this.dtpEndTime.Value.ToString(), ""));
            
            FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();

            employee = (FS.HISFC.Models.Base.Employee)this.dataBaseManager.Operator;

            //this.neuSpread1_Sheet1.SetText(0, 0, Title);

            this.DataBeginColumnIndex = 0;
            this.DataBeginRowIndex = 2;
            this.DataCrossColumns = "1";
            this.DataCrossRows = "0";
            this.DataCrossValues = "2";

            base.OnQuery(sender, neuObject);

            this.neuSpread1_Sheet1.SetText(1, 0, "统计科别：" + employee.Dept.Name);

            this.neuSpread1_Sheet1.SetText(1, 1, "时间范围：" + this.dtpBeginTime.Value.ToString() + "---" + this.dtpEndTime.Value.ToString());

            return 1;
        }

        private void cmbStorage_SelectedIndexChanged(object sender, EventArgs e)
        {
            deptCode = this.cmbStorage.Tag.ToString();
        }

        protected override int OnExport()
        {
            this.neuSpread1_Sheet1.ClearSpanCells();
            string fileName = "";
            SaveFileDialog sfg = new SaveFileDialog();
            sfg.DefaultExt = ".xls";
            sfg.Filter = "Microsoft Excel 工作薄 (*.xls)|*.*";
            if (sfg.ShowDialog() == DialogResult.OK)
            {
                fileName = sfg.FileName;
                this.neuSpread1.SaveExcel(fileName, FarPoint.Win.Spread.Model.IncludeHeaders.BothCustomOnly);
            }

            return 1;
        }
    }
}
