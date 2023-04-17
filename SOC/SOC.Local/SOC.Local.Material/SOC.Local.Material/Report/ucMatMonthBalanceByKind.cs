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
    /// <summary>
    /// 查询某一段时间的入库金额汇总
    /// 开始时间+5天  到结束时间的
    /// </summary>
    public partial class ucMatMonthBalanceByKind : Base.BaseReport
    {
        public ucMatMonthBalanceByKind()
        {
            InitializeComponent();
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

            this.QueryDataWhenInit = false;

            this.MainTitle = "结存分类汇总表";
            this.SQLIndexs = "SOC.Local.Material.Report.Month.Balance.Kind";

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
}
