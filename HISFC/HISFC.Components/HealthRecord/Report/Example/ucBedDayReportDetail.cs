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

namespace FS.HISFC.Components.HealthRecord.Report.Example
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
    public partial class ucBedDayReportDetail : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 
        /// </summary>
        public ucBedDayReportDetail()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.InitData();

            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
            this.cmbNurse.SelectedIndexChanged += new EventHandler(cmbNurse_SelectedIndexChanged);

            base.OnLoad(e);
        }

        void cmbNurse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbDept.Enabled || !cmbNurse.Enabled)
            {
                return;
            }

            this.cmbDept.SelectedIndexChanged -= new EventHandler(cmbDept_SelectedIndexChanged);

            SetDeptInfo(cmbNurse.Tag.ToString());

            this.cmbDept.SelectedIndexChanged += new EventHandler(cmbDept_SelectedIndexChanged);
        }

        void cmbDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cmbDept.Enabled || !cmbNurse.Enabled)
            {
                return;
            }
            this.cmbNurse.SelectedIndexChanged -= new EventHandler(cmbNurse_SelectedIndexChanged);

            SetDeptInfo(cmbDept.Tag.ToString());

            this.cmbNurse.SelectedIndexChanged += new EventHandler(cmbNurse_SelectedIndexChanged);
        }

        //private FS.FrameWork.Public.ObjectHelper deptHelper = new FS.FrameWork.Public.ObjectHelper();
        private FS.HISFC.BizProcess.Integrate.Manager inteMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.BizLogic.HealthRecord.DayReport dayReportMgr = new FS.HISFC.BizLogic.HealthRecord.DayReport();

        /// <summary
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnQuery(object sender, object neuObject)
        {
            if (this.cmbDept.Tag == null || string.IsNullOrEmpty(this.cmbDept.Text))
            {
                MessageBox.Show("请选择查询科室");
            }

            this.SetTitle();

            DataSet dsOutDetail = new DataSet();
            //string[] parm = new string[] { this.neuDateTimePicker1.Value.ToString(), ((FS.HISFC.Models.Base.Employee)(dayReportMgr.Operator)).Dept.ID, this.cmbDept.Tag.ToString() };
            string[] parm = new string[] { this.neuDateTimePicker1.Value.ToString(), this.cmbNurse.Tag.ToString(), this.cmbDept.Tag.ToString() };

            dayReportMgr.ExecQuery("HealthRecord.DayReport.Example.Detail", ref dsOutDetail, parm);

            RefreshFP(dsOutDetail);

            SetSum();

            return base.OnQuery(sender, neuObject);
        }


        private void SetTitle()
        {
            this.lbDate.Text = "日期：" + this.neuDateTimePicker1.Text;
            if (!string.IsNullOrEmpty(this.cmbDept.Text))
            {
                this.lblDeptName.Text = "科室:" + this.cmbDept.Text;
            }
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
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = ds.Tables[0].Rows[i][3].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Text = ds.Tables[0].Rows[i][4].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = ds.Tables[0].Rows[i][5].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text = ds.Tables[0].Rows[i][6].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 7].Text = ds.Tables[0].Rows[i][7].ToString() == "1" ? "√" : "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 8].Text = ds.Tables[0].Rows[i][7].ToString() == "2" ? "√" : "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 9].Text = ds.Tables[0].Rows[i][7].ToString() == "3" ? "√" : "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 10].Text = ds.Tables[0].Rows[i][7].ToString() == "4" ? "√" : "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 11].Text = ds.Tables[0].Rows[i][7].ToString() == "5" ? "√" : "";
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 12].Text = ds.Tables[0].Rows[i][8].ToString();
                    this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 13].Text = ds.Tables[0].Rows[i][9].ToString();
                }

            }
        }

        protected void SetSum()
        {
            DataSet dsSum = new DataSet();


            string[] parm = new string[] { this.neuDateTimePicker1.Value.ToString(), ((FS.HISFC.Models.Base.Employee)(dayReportMgr.Operator)).Dept.ID, this.cmbDept.Tag.ToString() };
            dayReportMgr.ExecQuery("HealthRecord.DayReport.Example.Detail.1", ref dsSum, parm);

            if (dsSum != null && dsSum.Tables[0].Rows.Count > 0)
            {

                string LastNum = dsSum.Tables[0].Rows[0][0].ToString();
                string NewIn = dsSum.Tables[0].Rows[0][1].ToString();
                string Out = dsSum.Tables[0].Rows[0][2].ToString();
                string InFromOtherDept = dsSum.Tables[0].Rows[0][3].ToString();
                string OutToOtherDept = dsSum.Tables[0].Rows[0][4].ToString();
                string CurrentNum = dsSum.Tables[0].Rows[0][5].ToString();
                string BabyNum = dsSum.Tables[0].Rows[0][6].ToString();
                //string CurrentNum1 = dsSum.Tables[0].Rows[0][7].ToString();

                string strFirstRow = string.Format("    原有病人 {0} 人  +  新入院 {1} 人  +   转入 {2} 人    -   出院 {3} 人  -   转出 {4} 人  =   现有 {5} 人",
                                                                            LastNum, NewIn, InFromOtherDept, Out, OutToOtherDept, CurrentNum);
                string strSecRow = string.Format("    婴儿数  {0}  人______临时加床______张______陪人日数______填报人: {1}", BabyNum, dayReportMgr.Operator.Name);

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

        private void SetDeptInfo(string dept)
        {
            ArrayList alDept = new ArrayList();

            alDept.AddRange(inteMgr.QueryDepartment(dept));
            alDept.AddRange(this.inteMgr.QueryNurseStationByDept(new FS.FrameWork.Models.NeuObject(dept, "", "")));

            if (deptHelper.GetObjectFromID(dept) != null)
            {
                this.cmbDept.Tag = dept;

                if (!cmbDept.Enabled || !cmbNurse.Enabled)
                {
                    this.cmbNurse.AddItems(alDept);

                    this.cmbNurse.Enabled = true;
                    if (alDept.Count > 0)
                    {
                        cmbNurse.SelectedIndex = 0;
                    }
                }
                else
                {
                    this.cmbNurse.Tag = ((FS.FrameWork.Models.NeuObject)alDept[0]).ID;
                }
            }
            else if (this.nurseHelper.GetObjectFromID(dept) != null)
            {
                this.cmbNurse.Tag = dept;

                if (!cmbDept.Enabled || !cmbNurse.Enabled)
                {
                    this.cmbDept.AddItems(alDept);
                    this.cmbDept.Enabled = true;
                    if (alDept.Count > 0)
                    {
                        cmbDept.SelectedIndex = 0;
                    }
                }
                else
                {
                    this.cmbDept.Tag = ((FS.FrameWork.Models.NeuObject)alDept[0]).ID;
                }
            }
            else
            {
                return;
            }
        }

        private FS.FrameWork.Public.ObjectHelper deptHelper = null;

        private FS.FrameWork.Public.ObjectHelper nurseHelper = null;

        private void InitData()
        {
            if (deptHelper == null)
            {
                deptHelper = new FS.FrameWork.Public.ObjectHelper();
                deptHelper.ArrayObject = inteMgr.GetDepartment(EnumDepartmentType.I);
            }
            if (nurseHelper == null)
            {
                nurseHelper = new FS.FrameWork.Public.ObjectHelper();
                nurseHelper.ArrayObject = inteMgr.GetDepartment(EnumDepartmentType.N);
            }
            this.cmbDept.AddItems(deptHelper.ArrayObject);
            this.cmbNurse.AddItems(nurseHelper.ArrayObject);
            this.cmbDept.Enabled = true;
            this.cmbDept.Enabled = true;

            this.SetDeptInfo(((FS.HISFC.Models.Base.Employee)(dayReportMgr.Operator)).Dept.ID);
            if (deptHelper.GetObjectFromID(((FS.HISFC.Models.Base.Employee)(dayReportMgr.Operator)).Dept.ID) != null)
            {
                cmbDept.Enabled = false;
            }
            else if (this.nurseHelper.GetObjectFromID(((FS.HISFC.Models.Base.Employee)(dayReportMgr.Operator)).Dept.ID) != null)
            {
                this.cmbNurse.Enabled = false;
            }

            //this.lblNurseName.Text = "护士站:" + ((FS.HISFC.Models.Base.Employee)(dayReportMgr.Operator)).Dept.Name;

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
