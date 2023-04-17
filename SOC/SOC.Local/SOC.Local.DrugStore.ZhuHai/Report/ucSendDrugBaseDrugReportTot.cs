using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.ZhuHai.Report
{
    public partial class ucSendDrugBaseDrugReportTot : FS.SOC.Local.Pharmacy.Base.ucPrivePowerReport
    {
        #region SQL
        #endregion
        public ucSendDrugBaseDrugReportTot()
        {
            InitializeComponent();
            this.SQLIndexs = "Pharmacy.NewReport.Out.SendDrugBaseDrugRepot.Tot";
            this.PriveClassTwos = "0320";
            this.MainTitle = "药房基本药物消耗明细查询";
            this.CustomTypeShowType = "基药：";
            this.CustomTypeSQL = "select D.CODE ID,D.NAME NAME,D.SPELL_CODE,D.WB_CODE,'','' from com_dictionary d where d.type = 'BASEDRUGCODE'";
            this.RightAdditionTitle = "";
            this.IsUseCustomType = true;
            this.SumColIndexs = "9,10";
            this.SortColIndexs = "0,1,2,3,4,5,6,7,8,9,10";
        }

        //private void ucSendDrugReport_Load(object sender, EventArgs e)
        //{
        //    this.SQLIndexs = "Pharmacy.NewReport.Out.SendDrugRepot.Detail";
        //    this.PriveClassTwos = "0320";
        //    this.MainTitle = "药房消耗明细查询";
        //    this.RightAdditionTitle = "";
        //    this.Init();
        //}
    }
}
