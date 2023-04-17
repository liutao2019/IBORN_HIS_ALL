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
    public partial class ucMatInvoiceItemRecordNew : Base.BaseReport
    {
        public ucMatInvoiceItemRecordNew()
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
            this.IsUseCustomType = true;

            //this.CustomTypeSQL = "SOC.Local.Material.Report.CustomType";

            this.CustomTypeSQL = @"SELECT P.ITEM_CODE,
                                                    P.ITEM_NAME,
                                                    P.KIND_CODE,
                                                    P.SPELL_CODE,
                                                    P.WB_CODE,
                                                    P.SPECS
                                                    FROM MAT_COM_BASEINFO P
                                                  WHERE P.VALID_FLAG = '1'";

            this.MainTitle = "项目汇总表";

            this.SQLIndexs = "SOC.Local.Material.Report.Item.Record.Current";

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
