using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.InpatientFee.QiaoTou
{
    public partial class ucQueryUndrugBydeptAndDate : FS.FrameWork.WinForms.Controls.ucBaseControl 
    {
        public ucQueryUndrugBydeptAndDate()
        {
            InitializeComponent();
            this.Load += new EventHandler(ucQueryUndrugBydeptAndDate_Load);
        }

        void ucQueryUndrugBydeptAndDate_Load(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.ColumnCount = 11;
            this.neuSpread1_Sheet1.RowCount = 0;
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "姓名";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "住院号";
            this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "科室";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "项目名称";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "价格";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "数量";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "比率";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "单位";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 8).Value = "合计";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 9).Value = "收费员";
            this.neuSpread1_Sheet1.ColumnHeader.Cells.Get(0, 10).Value = "收费时间";
            this.neuSpread1_Sheet1.Columns.Get(0).Label = "姓名";
            this.neuSpread1_Sheet1.Columns.Get(1).Label = "住院号";
            this.neuSpread1_Sheet1.Columns.Get(1).Width = 90F;
            this.neuSpread1_Sheet1.Columns.Get(2).Label = "科室";
            this.neuSpread1_Sheet1.Columns.Get(3).Label = "项目名称";
            this.neuSpread1_Sheet1.Columns.Get(3).Width = 183F;
            this.neuSpread1_Sheet1.Columns.Get(4).Label = "价格";
            this.neuSpread1_Sheet1.Columns.Get(4).Width = 64F;
            this.neuSpread1_Sheet1.Columns.Get(9).Label = "收费员";
            this.neuSpread1_Sheet1.Columns.Get(9).Width = 81F;
            this.neuSpread1_Sheet1.Columns.Get(10).Label = "收费时间";
            this.neuSpread1_Sheet1.Columns.Get(10).Width = 118F;

            this.neuTabControl1.TabPages[0].Text = "非药品";
            this.neuTabControl1.TabPages.Remove(tabPage2);
            //this.neuTabControl1.TabPages[1].Text = "";
            //this.neuSpread2_Sheet1.ColumnCount = 0;
            //this.neuSpread2_Sheet1.RowCount = 0;
            //this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目名称";
            //this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "价格";
            //this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "数量";
            //this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 0).Value = "项目名称";
            //this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 1).Value = "价格";
            //this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 2).Value = "数量";
            //this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 3).Value = "比率";
            //this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 4).Value = "单位";
            //this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 5).Value = "合计";
            //this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 6).Value = "收费员";
            //this.neuSpread2_Sheet1.ColumnHeader.Cells.Get(0, 7).Value = "收费时间";
            //this.neuSpread2_Sheet1.Columns.Get(0).Label = "项目名称";
            //this.neuSpread2_Sheet1.Columns.Get(0).Width = 163F;
            //this.neuSpread2_Sheet1.Columns.Get(1).Label = "价格";
            //this.neuSpread2_Sheet1.Columns.Get(1).Width = 64F;
            //this.neuSpread2_Sheet1.Columns.Get(6).Label = "收费员";
            //this.neuSpread2_Sheet1.Columns.Get(6).Width = 81F;
            //this.neuSpread2_Sheet1.Columns.Get(7).Label = "收费时间";
            //this.neuSpread2_Sheet1.Columns.Get(7).Width = 108F;
             
            this.neuDateTimePicker1.Value = new DateTime(System .DateTime .Now.Year, System .DateTime .Now.Month, System .DateTime .Now.Day, 0, 0, 0, 0);
            this.neuDateTimePicker2.Value = new DateTime(System .DateTime .Now.Year, System .DateTime .Now.Month, System .DateTime .Now.Day, 23, 59, 59, 999);
            //this.neuDateTimePicker1.Value =new DateTime (System .DateTime .Now .Date,"00:00:00")
            dosageHelper = new FS.FrameWork.Public.ObjectHelper(manager.GetDeptmentAllValid());             
        }

        #region 字段
        private FS.HISFC.BizProcess.Integrate.Fee feeManager = new FS.HISFC.BizProcess.Integrate.Fee();
        private FS.HISFC.BizProcess.Integrate.RADT radtManager = new FS.HISFC.BizProcess.Integrate.RADT();
        private static FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
        private FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.RADT.InPatient Inpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        private FS.FrameWork.Public.ObjectHelper dosageHelper = new FS.FrameWork.Public.ObjectHelper();
        private ArrayList alpatientinfo = new ArrayList();
        private FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();  
            
           


        #endregion
        #region 设置
        private bool isshowDrug = true;
        [Category("控件设置"), Description("ceshi"), DefaultValue(true)]
        public bool IsShowDrug
        {
            get
            {
                return isshowDrug;
            }
            set
            {
                isshowDrug = value;
            }
        }
        #endregion
        #region 方法

        //按执行科室
        public void AddItems(string patientid, string execDeptCode)
        {
            //ArrayList undrugs = feeManager.QueryFeeItemLists(patientInfo.ID, patientInfo.PVisit.InTime, Environment.AnaeManager.GetDateTimeFromSysDateTime());
            ArrayList undrugs = feeManager.QueryFeeItemListsByExecDeptCode(patientid, neuDateTimePicker1.Value, neuDateTimePicker2.Value, execDeptCode);
            decimal totCost = 0;
            this.neuSpread1_Sheet1.RowCount = 0;
            if (undrugs != null)
            {
                foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList undrug in undrugs)
                {
                    neuSpread1_Sheet1.Rows.Add(neuSpread1_Sheet1.RowCount, 1);
                    int row = neuSpread1_Sheet1.RowCount - 1;
                    //姓名
                    neuSpread1_Sheet1.SetValue(row, 0, undrug.Patient.Name.ToString(), false);
                    //住院号
                    //this.alpatientinfo = this.Inpatient.QueryInpatientNOByName(this.Text);
                    this.patientInfo = Inpatient.QueryPatientInfoByInpatientNO(undrug.Patient.ID);
                    //string a=((FS.FrameWork.Models.NeuObject)this.alInpatientNos[0]).ID
                    neuSpread1_Sheet1.SetValue(row, 1, this.patientInfo.PID.PatientNO, false);
                    //科室
                    //neuSpread1_Sheet1.SetValue(row, 10, ((FS.HISFC.Models.RADT.PatientInfo)undrug.Patient).PVisit.PatientLocation.Dept.ID.ToString(), false);
                    neuSpread1_Sheet1.SetValue(row, 2, dosageHelper.GetName(((FS.HISFC.Models.RADT.PatientInfo)undrug.Patient).PVisit.PatientLocation.Dept.ID.ToString()), false);
                    
                    //添加项目名称
                    neuSpread1_Sheet1.SetValue(row, 3, undrug.Item.Name, false);
                    //价格
                    neuSpread1_Sheet1.SetValue(row, 4, undrug.Item.Price, false);
                    //数量
                    neuSpread1_Sheet1.SetValue(row, 5, undrug.Item.Qty, false);
                    //比率
                    neuSpread1_Sheet1.SetValue(row, 6, undrug.FTRate.ItemRate, false);
                    //单位
                    neuSpread1_Sheet1.SetValue(row, 7, undrug.Item.PriceUnit, false);
                    //总额
                    neuSpread1_Sheet1.SetValue(row, 8, undrug.FT.TotCost, false);
                    //收费人
                    neuSpread1_Sheet1.SetValue(row, 9, undrug.FeeOper.ID, false);
                    //收费时间
                    neuSpread1_Sheet1.SetValue(row, 10, undrug.FeeOper.OperTime.ToString(), false);
                    totCost = totCost + undrug.FT.TotCost;
                }
            }
            if (totCost > 0)
            {
                neuSpread1_Sheet1.Rows.Add(neuSpread1_Sheet1.RowCount, 1);
                int row = neuSpread1_Sheet1.RowCount - 1;
                neuSpread1_Sheet1.SetValue(row, 7, "合计", false);
                neuSpread1_Sheet1.SetValue(row, 8, totCost, false);
            }
        }

        public int Print()
        {
            return print.PrintPreview(this);
        }
        protected override int OnPrint(object sender, object neuObject)
        {
            return this.Print();
        }
        protected override int OnQuery(object sender, object neuObject)
        {
            this.ucQueryInpatientNo1_myEvent();
            return 1;
        }
        public void Reset()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
        }
        #endregion

        private void ucQueryInpatientNo1_myEvent()
        {
            if (string.IsNullOrEmpty(this.ucQueryInpatientNo1.InpatientNo))
            {
                this.AddItems("ALL", ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            }
            else
            {
                this.patientInfo = radtManager.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);


                if (this.patientInfo == null)
                {
                    MessageBox.Show("没有查到该患者信息");
                    return;
                }
                this.AddItems(this.patientInfo.ID, ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Dept.ID);
            }
        }
    }
}
