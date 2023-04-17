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
    public partial class ucMatInputTotalAndDetail : Base.BaseReport
    {
        public ucMatInputTotalAndDetail()
        {
            InitializeComponent();
        }

        EnumInTotalType totType = EnumInTotalType.按供货公司汇总;

        [Category("本地设置"), Description("报表的汇总方式")]
        public EnumInTotalType TotType
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

            if (this.totType == EnumInTotalType.按供货公司汇总)
            {
                this.MainTitle = "按供货公司汇总、明细入库表";
                this.SQLIndexs = "SOC.Local.Material.Report.Company.InputTotal";
                this.DetailSQLIndexs = "SOC.Local.Material.Report.Company.InputDetail";
            }
            else if (this.totType == EnumInTotalType.按类别汇总)
            {
                this.MainTitle = "按类别汇总、明细入库表";
                this.SQLIndexs = "SOC.Local.Material.Report.Kind.InputTotal";
                this.DetailSQLIndexs = "SOC.Local.Material.Report.Kind.InputDetail";
            }
            else if (this.totType == EnumInTotalType.按入库单汇总)
            {
                this.MainTitle = "按入库单汇总、明细入库表";
                this.SQLIndexs = "SOC.Local.Material.Report.Bill.InputTotal";
                this.DetailSQLIndexs = "SOC.Local.Material.Report.Bill.InputDetail";
            }
            else if (this.totType == EnumInTotalType.按物品汇总)
            {
                this.MainTitle = "按物品汇总、明细入库表";
                this.SQLIndexs = "SOC.Local.Material.Report.Gods.InputTotal";
                this.DetailSQLIndexs = "SOC.Local.Material.Report.Gods.InputDetail";
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

    public enum EnumInTotalType
    {
        按供货公司汇总,
        按入库单汇总,
        按物品汇总,
        按类别汇总
    };
}
