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
    public partial class ucMatOutputDeptAndKind : Base.ucCrossQueryBaseForFarPoint
    {
        public ucMatOutputDeptAndKind()
        {
            InitializeComponent();
        }

        private string priveClassTwos = "5510";

        [Description("二级权限编码，赋值操作员权限"), Category("本地设置"), Browsable(true)]
        public string PriveClassTwos
        {
            get
            {
                return priveClassTwos;
            }
            set
            {
                priveClassTwos = value;
            }
        }

        private bool isLandScape = false;

        [Description("是否横打"), Category("本地设置"), Browsable(true)]
        public bool IsLandScape
        {
            get { return isLandScape; }
            set { isLandScape = value; }
        }


        FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();
        private FS.HISFC.BizLogic.Manager.UserPowerDetailManager priPowerMgr = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
        string deptCode = string.Empty;

        protected override void OnLoad()
        {
            FS.HISFC.Models.Base.Employee emp = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            List<FS.FrameWork.Models.NeuObject> alDept = priPowerMgr.QueryUserPriv(emp.ID, this.priveClassTwos);
            //ArrayList al = dept.GetDeptmentByType("L");
            this.cmbStorage.AddItems(new ArrayList(alDept));
           
            //deptCode = emp.Dept.ID;
            //this.cmbStorage.Tag = emp.Dept.ID;

            //this.neuSpread1_Sheet1.SetText(0, 0, Title);
            base.OnLoad();
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QuerySqlTypeValue = QuerySqlType.id;

            this.QuerySql = "SOC.Local.Material.Report.Output.DeptAndKind";

            QueryParams.Clear();
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", deptCode, ""));
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", this.dtpBeginTime.Value.ToString(), ""));
            QueryParams.Add(new FS.FrameWork.Models.NeuObject("", this.dtpEndTime.Value.ToString(), ""));
            
            FS.HISFC.Models.Base.Employee employee = new FS.HISFC.Models.Base.Employee();

            employee = (FS.HISFC.Models.Base.Employee)this.dataBaseManager.Operator;

            //this.neuSpread1_Sheet1.SetText(0, 0, Title);

            this.neuSpread1_Sheet1.SetText(1, 0, "统计科别：" + employee.Dept.Name);

            this.neuSpread1_Sheet1.SetText(1, 1, "时间范围：" + this.dtpBeginTime.Value.ToString() + "---" + this.dtpEndTime.Value.ToString());

            //this.DataBeginColumnIndex = 0;
            //this.DataBeginRowIndex = 2;
            //this.DataCrossColumns = "1";
            //this.DataCrossRows = "0";
            //this.DataCrossValues = "2|3";

            return base.OnQuery(sender, neuObject);
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

        public override int Print(object sender, object neuObject)
        {
            FarPoint.Win.Spread.PrintInfo printInfo = new FarPoint.Win.Spread.PrintInfo();
            printInfo.ShowBorder = false;
            printInfo.ShowColumnHeaders = false;
            printInfo.ShowRowHeaders = false;
            //printInfo.BestFitCols = true;
            if (this.isLandScape)
            {
                printInfo.Orientation = FarPoint.Win.Spread.PrintOrientation.Landscape;
            }
            else
            {
                printInfo.Orientation = FarPoint.Win.Spread.PrintOrientation.Portrait;
            }
            this.neuSpread1_Sheet1.PrintInfo = printInfo;
            this.neuSpread1.PrintSheet(this.neuSpread1_Sheet1);
            return 1;
        }
    }
}
