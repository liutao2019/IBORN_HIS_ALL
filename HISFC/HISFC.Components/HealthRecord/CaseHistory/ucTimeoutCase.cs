using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.HealthRecord.CaseHistory
{
    public partial class ucTimeoutCase : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        /// <summary>
        /// 超时病案明细
        /// </summary>
        public ucTimeoutCase()
        {
            InitializeComponent();
            
        }


        /// <summary>
        /// 是否超时
        /// </summary>
        private bool isCallBack=true;


        /// <summary>
        /// 是否查已经回收，并超时的病历
        /// </summary>
        [Category("设置") ,Description("是否是已回收超时查询")]
        public bool IsCallBack 
        {
            get 
            {
                return this.isCallBack;
            }
            set 
            {
                this.isCallBack = value;
            }
        }


        FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack callbackMgr = new FS.HISFC.BizLogic.HealthRecord.CaseHistory.CallBack();
        FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
        /// <summary>
        /// 初始化
        /// </summary>
        private void Init()
        {
            ArrayList al = deptMgr.GetDeptment(FS.HISFC.Models.Base.EnumDepartmentType.I);
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject("ALL", "", "");
            al.Add(obj);
            this.neuComDept.AddItems(al);
            this.neuComDept.SelectedValue = obj;
        }


        private void QueryCaseInfo()
        {
            this.neuSpread1_Sheet1.RowCount = 0;
            ArrayList al = null;
            if (isCallBack)
            {
                 al = callbackMgr.GetTimeOutSpecifyInfo(this.neuComDept.Tag.ToString(), this.neuDateTimePicker1.Value, this.neuDateTimePicker2.Value,"1");

            }
            else 
            {
                 al = callbackMgr.GetTimeOutSpecifyInfo(this.neuComDept.Tag.ToString(), this.neuDateTimePicker1.Value, this.neuDateTimePicker2.Value,"0");
            }
            if (al == null || al.Count == 0)
            {
                return;
            }

            foreach (FS.HISFC.Models.HealthRecord.CaseHistory.CallBack cb in al)
            {
                this.neuSpread1_Sheet1.RowCount++;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 0].Text = cb.Patient.PID.PatientNO ;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 1].Text = cb.Patient.Name;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 2].Value = cb.Patient.PVisit.OutTime.ToShortDateString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 3].Text = cb.Patient.PVisit.PatientLocation.Dept.Name;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 4].Value  = cb.CallbackOper.OperTime.ToShortDateString();
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 5].Text = cb.Patient.PVisit.PatientLocation.NurseCell.Name;
                TimeSpan ts1 = new TimeSpan(cb.CallbackOper.OperTime.Ticks);
                TimeSpan ts2 = new TimeSpan(cb.Patient.PVisit.OutTime.Ticks);
                TimeSpan ts = ts1.Subtract(ts2).Duration();
                int diff = ts.Days;
                this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.RowCount - 1, 6].Text =  Convert.ToString(diff);
            }
        }
        /// <summary>
        /// load 重写
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            this.Init();
            base.OnLoad(e);
        }

        private void neuBtnQuery_Click(object sender, EventArgs e)
        {
            this.QueryCaseInfo();
        }

        private void neuBtnExit_Click(object sender, EventArgs e)
        {
            ((Form)this.Parent).Hide();
        }

        private void neuBtnPrint_Click(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            print.PrintPage(this.neuPanel1.Left, this.neuPanel1.Top, this.neuPanel1);
        }
    }
}
