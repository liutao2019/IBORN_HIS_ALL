using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Function;
using System.Collections;
using FS.HISFC.Models.Base;

namespace FS.WinForms.Report.BedDayReport
{
    /// <summary>
    /// 床位日报统计
    /// <说明>
    ///     1、出院状态 1 治愈 2 好转 3 未愈 4 死亡 5 其他 
    ///     2、出院人数总计： 指正常出院和无费退院合计人数
    ///     3、出院病人数： 指正常出院人数
    ///     4、出院者占用总床日数：指出院患者住院日期总计
    /// </说明>
    /// </summary>
    public partial class ucDepartmentBedDayReportDetail : FS.FrameWork.WinForms.Controls.ucBaseControl
    {

        public ucDepartmentBedDayReportDetail()
        {
            InitializeComponent();

        }

        protected override void OnLoad(EventArgs e)
        {
            
            this.InitData();
            base.OnLoad(e);
        }

        private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
        //private FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        private FS.HISFC.BizLogic.Manager.Department deptManager = new FS.HISFC.BizLogic.Manager.Department();

        protected override int OnQuery(object sender, object neuObject)
        {
            //if (this.cmbDept.Tag == null || string.IsNullOrEmpty(this.cmbDept.Text))
            //{
            //    MessageBox.Show("请选择查询科室");
            //}

            this.SetTitle();            
            DataSet dsOutDetail = new DataSet();
            //dsOutDetail = inpatientMgr.QueryInpatientDepartmentBedReportDetail(this.neuDateTimePicker1.Value, ((FS.HISFC.Models.Base.Employee)(inpatientMgr.Operator)).Dept.ID, ((FS.HISFC.Models.Base.Employee)(inpatientMgr.Operator)).Nurse.ID);//this.cmbDept.Tag.ToString()
            
            RefreshFP(dsOutDetail);
            
            SetSum();

            return base.OnQuery(sender, neuObject);
        }


        private void SetTitle()
        {
            this.lbDate.Text = "日期：" + this.neuDateTimePicker1.Text;
            //if (!string.IsNullOrEmpty(this.cmbDept.Text))
            //{
            //    this.lblDeptName.Text = "科室:" + this.cmbDept.Text;
            //}
        }
      
        protected void RefreshFP(DataSet ds)
        {
            this.neuSpread1_Sheet1.RowCount = 0;

            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {


                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = ds.Tables[0].Rows[i][0].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = ds.Tables[0].Rows[i][1].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Text = ds.Tables[0].Rows[i][2].ToString() == "1" ? "√" : "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = ds.Tables[0].Rows[i][12].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = ds.Tables[0].Rows[i][3].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = ds.Tables[0].Rows[i][4].ToString();

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = ds.Tables[0].Rows[i][5].ToString();

                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = ds.Tables[0].Rows[i][6].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = ds.Tables[0].Rows[i][7].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = ds.Tables[0].Rows[i][8].ToString() == "1" ? "√" : "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 10].Text = ds.Tables[0].Rows[i][8].ToString() == "2" ? "√" : "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 11].Text = ds.Tables[0].Rows[i][8].ToString() == "3" ? "√" : "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 12].Text = ds.Tables[0].Rows[i][8].ToString() == "4" ? "√" : "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 13].Text = ds.Tables[0].Rows[i][8].ToString() == "5" ? "√" : "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 14].Text = ds.Tables[0].Rows[i][13].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 15].Text = ds.Tables[0].Rows[i][9].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 16].Text = ds.Tables[0].Rows[i][10].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 17].Text = ds.Tables[0].Rows[i][11].ToString();
                }

            }
        }

        protected void SetSum()
        {

            DataSet dsSum = new DataSet();
            //dsSum = inpatientMgr.QueryInpatientDepartmentBedReportSummary(((FS.HISFC.Models.Base.Employee)(inpatientMgr.Operator)).Nurse.ID,this.neuDateTimePicker1.Value);

            //原有人数
            string LastNum = "";
            //
            string NewIn = "";
            string Out = "";
            string InFromOtherDept = "";
            string OutToOtherDept = "";
            string CurrentNum = "";
            string BabyNum = "";
            string CurrentNum1 = "";

            if (dsSum != null && dsSum.Tables[0].Rows.Count > 0)
            {

                LastNum = dsSum.Tables[0].Rows[0][0].ToString();
                NewIn = dsSum.Tables[0].Rows[0][1].ToString();
                Out = dsSum.Tables[0].Rows[0][2].ToString();
                InFromOtherDept = dsSum.Tables[0].Rows[0][3].ToString();
                OutToOtherDept = dsSum.Tables[0].Rows[0][4].ToString();
                CurrentNum = dsSum.Tables[0].Rows[0][5].ToString();
                BabyNum = dsSum.Tables[0].Rows[0][6].ToString();
                CurrentNum1 = dsSum.Tables[0].Rows[0][7].ToString();

                string strFirstRow = string.Format("    原有病人 {0} 人+新入院 {1} 人+转入 {2} 人-出院 {3} 人-转出 {4} 人=现有 {5} 人",
                    string.IsNullOrEmpty(LastNum) ? CurrentNum1 : LastNum, NewIn, InFromOtherDept, Out, OutToOtherDept, string.IsNullOrEmpty(CurrentNum) ? CurrentNum1 : CurrentNum);
                string strSecRow = string.Format("    婴儿数 {0} 人______临时加床______张______陪人日数______填报人: {1}", string.IsNullOrEmpty(BabyNum) ? "0" : BabyNum, ""/*this.inpatientMgr.Operator.Name*/);

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = strFirstRow;

                this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.RowCount, 1);
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].ColumnSpan = this.neuSpread1_Sheet1.ColumnCount;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = strSecRow;

                FarPoint.Win.IBorder nonBorder1 = new FarPoint.Win.LineBorder(Color.White, 0, false, false, false, true);
                this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 1].Border = nonBorder1;
                this.neuSpread1_Sheet1.Rows[this.neuSpread1_Sheet1.RowCount - 2].Border = nonBorder1;

            }
        }

        private void InitData()
        {
            //this.lblNurseName.Text = "护士站:" + ((FS.HISFC.Models.Base.Employee)(inpatientMgr.Operator)).Dept.Name;
            
            //System.Collections.ArrayList alDept = deptManager.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);

            //if (alDept == null)
            //{
            //    MessageBox.Show("获取科室列表发生错误");
            //    return;
            //}
            //this.cmbDept.AddItems(alDept);
        }

       
        #region 打印
        /// <summary>
        /// 打印
        /// </summary>
        public void Print()
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.ControlBorder = FS.FrameWork.WinForms.Classes.enuControlBorder.Line;
            print.PrintPage(30, 10, this.plResult);

        }
        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return base.OnPrint(sender, neuObject);
        }
        #endregion

        #region 导出
        /// <summary>
        /// 导出
        /// </summary>
        private void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                // MessageBox.Show(Language.Msg("导出成功"));
            }
        }
        public override int Export(object sender, object neuObject)
        {
            this.Export();

            return base.Export(sender, neuObject);
        }
        #endregion

       
    }
}
