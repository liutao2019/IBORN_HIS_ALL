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
    public partial class ucCrossSample : Base.ucCrossPrivePowerReport
    {
        public ucCrossSample()
        {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            if (DesignMode)
            {
                return;
            }
            this.MainTitle = "供货公司分类汇总";
            this.DeptType = "PriveDept";
            this.PriveClassTwos = "5510|5520";
            this.RightAdditionTitle = "";
            this.IsShowColHeader = false; //打印不显示列头
            this.IsShowRowHeader = false;
            base.OnLoad(e);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.QuerySqlTypeValue = QuerySqlType.id;
            this.QuerySql = "SOC.Local.Material.Report.Input.CompanyAndKind";

            this.DataBeginColumnIndex = 0;
            this.DataBeginRowIndex = 0;
            this.DataCrossColumns = "1";
            this.DataCrossRows = "0";
            this.DataCrossValues = "2|3";

            base.OnQuery(sender, neuObject);

            return 1;
        }
    }
}
