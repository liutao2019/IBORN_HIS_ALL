using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Material.Report
{
    public partial class ucMatOutputTotalAndDetail : Base.BaseReport
    {
        public ucMatOutputTotalAndDetail()
        {
            InitializeComponent();
        }

        EnumOutTotalType totType = EnumOutTotalType.按领用科室汇总;

        [Category("本地设置"), Description("报表的汇总方式")]
        public EnumOutTotalType TotType
        {
            get { return totType; }
            set { totType = value; }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }

            this.DeptType = "PriveDept";
            this.PriveClassTwos = "5510|5520";
            this.RightAdditionTitle = "";
            this.IsNeedDetailData = true;
            this.QueryConditionColIndexs = "0";
            this.DetailDataQueryType = EnumQueryType.汇总条件活动行;

            if (this.totType == EnumOutTotalType.按领用科室汇总)
            {
                this.MainTitle = "按领用科室汇总、明细出库表";
                this.SQLIndexs = "SOC.Local.Material.Report.Company.OutputTotal";
                this.DetailSQLIndexs = "SOC.Local.Material.Report.Company.OutputDetail";
            }
            else if (this.totType == EnumOutTotalType.按类别汇总)
            {
                this.MainTitle = "按类别汇总、明细出库表";
                this.SQLIndexs = "SOC.Local.Material.Report.Kind.OutputTotal";
                this.DetailSQLIndexs = "SOC.Local.Material.Report.Kind.OutputDetail";
            }
            else if (this.totType == EnumOutTotalType.按出库单汇总)
            {
                this.MainTitle = "按出库单汇总、明细出库表";
                this.SQLIndexs = "SOC.Local.Material.Report.Bill.OutputTotal";
                this.DetailSQLIndexs = "SOC.Local.Material.Report.Bill.OutputDetail";
            }
            else if (this.totType == EnumOutTotalType.按物品汇总)
            {
                this.MainTitle = "按物品汇总、明细出库表";
                this.SQLIndexs = "SOC.Local.Material.Report.Gods.OutputTotal";
                this.DetailSQLIndexs = "SOC.Local.Material.Report.Gods.OutputDetail";
            }

            base.OnLoad(e);

            /*
            if (this.cmbDept.alItems != null && this.cmbDept.alItems.Count > 0)
            {
                FS.HISFC.Models.Base.Employee emp = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
                this.cmbDept.Tag = emp.Dept.ID;
            }
            */ 
        }
    }

    public enum EnumOutTotalType
    {
        按领用科室汇总,
        按出库单汇总,
        按物品汇总,
        按类别汇总
    };
}
