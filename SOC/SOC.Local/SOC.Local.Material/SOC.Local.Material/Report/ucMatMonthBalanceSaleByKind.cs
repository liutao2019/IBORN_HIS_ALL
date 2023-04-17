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
    /// 查询某一段时间购入金额、零售金额汇总
    /// </summary>
    public partial class ucMatMonthBalanceSaleByKind : Base.BaseReport
    {
        public ucMatMonthBalanceSaleByKind()
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

            this.IsUseMonthStoreTime = true;
            this.GetMonthStoreTimeSQL = @"SELECT TO_CHAR(C.BEGIN_DATE, 'yyyy-mm-dd hh24:mi:ss') || ' 到 ' ||
                                                                TO_CHAR(MIN(C.END_DATE), 'yyyy-mm-dd hh24:mi:ss')
                                                              FROM (SELECT M.STORAGE_CODE STORAGE,
                                                                                M.COPER_TIME   BEGIN_DATE,
                                                                                N.COPER_TIME   END_DATE
                                                                            FROM MAT_COM_CHECKHEAD M, MAT_COM_CHECKHEAD N
                                                                           WHERE M.STORAGE_CODE = N.STORAGE_CODE
                                                                                 AND N.COPER_TIME > M.COPER_TIME
                                                                                 AND M.CHECK_STATE = '1'  AND N.CHECK_STATE = '1'  AND M.STORAGE_CODE = '{0}') C 
                                                                GROUP BY C.STORAGE, C.BEGIN_DATE
                                                                ORDER BY BEGIN_DATE";

            this.MainTitle = "结存分类汇总表";
            this.SQLIndexs = "SOC.Local.Material.Report.Month.Balance.Kind.All";

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
