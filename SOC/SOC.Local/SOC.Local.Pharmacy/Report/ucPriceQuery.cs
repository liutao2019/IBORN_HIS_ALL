using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.Pharmacy.Report
{
    public partial class ucPriceQuery : Base.BaseReport
    {
        public ucPriceQuery()
        {
            InitializeComponent();

            this.SQLIndexs = "SOC.Pharmacy.Report.Item.Price.Query";
            this.IsTimeAsCondition = false;
            this.IsDeptAsCondition = false;
            this.LeftAdditionTitle = "注意：本查询仅包含库房在用药品";
            this.MidAdditionTitle = "";
            this.RightAdditionTitle = "";
            this.MainTitle = "药品价格查询";
            this.FilerType = EnumFilterType.汇总过滤;
            this.Filters = "编码,名称,拼音码,五笔码";
        }
    }
}
