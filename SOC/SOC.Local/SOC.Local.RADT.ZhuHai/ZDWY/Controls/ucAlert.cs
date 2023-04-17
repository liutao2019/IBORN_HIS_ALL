using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Models;
using FS.FrameWork.Function;
using System.Collections;

namespace FS.SOC.Local.RADT.ZhuHai.ZDWY.Controls
{
    public partial class ucAlert : FS.FrameWork.WinForms.Controls.ucBaseControl
    { 
        public ucAlert()
        {
            InitializeComponent();
        }
         
        #region 变量

        private NeuObject NurseCode = null;

        private string Err=string.Empty;
        FS.HISFC.BizLogic.RADT.InPatient Patient = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        private FS.HISFC.Models.RADT.PatientInfo mypatientinfo = new FS.HISFC.Models.RADT.PatientInfo();
        FS.HISFC.BizProcess.Interface.FeeInterface.IMoneyAlert IAlterPrint = null;
     
        FS.HISFC.BizProcess.Integrate.Fee FeeInterate = new  FS.HISFC.BizProcess.Integrate.Fee();
        FS.HISFC.Models.Base.Employee employee = ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator);
        /// <summary>
        /// 控制参数业务类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParmMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 催款金额输入方式
        /// 0：不录入
        /// 1：打印催款单时每人弹出输入框
        /// 2：表格界面输入
        /// </summary>
        private string repayType = "0";

        FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region 方法
        /// <summary>
        /// 初始化控件
        /// </summary>
        public void initControl()
        {
            this.cmbType.SelectedIndex = 0;
            this.txtAlert.Text = "0";

            #region 反射读取欠费打印单
            object[] o = new object[] { };
            try
            {
                //入库报表
                FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

                 

              
                //宜康病区，打印费用通知单 ，其他科室打印欠费通知单
                if (Function.IsContainYKDept(employee.Dept.ID))
                {
                    string billValue = ctrlIntegrate.GetControlParam<string>("RADT02", true, "FS.WinForms.Report.InpatientFee.ucPatientMoneyAlter");
                    System.Runtime.Remoting.ObjectHandle objHande = System.Activator.CreateInstance("WinForms.Report", billValue, false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);

                    object oLabel = objHande.Unwrap();

                    IAlterPrint = oLabel as FS.HISFC.BizProcess.Interface.FeeInterface.IMoneyAlert;

                }
                else
                {
                    string billValue = ctrlIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.RADT_MoneyAlter, true, "FS.WinForms.Report.InpatientFee.ucPatientMoneyAlter");
                    System.Runtime.Remoting.ObjectHandle objHande = System.Activator.CreateInstance("WinForms.Report", billValue, false, System.Reflection.BindingFlags.CreateInstance, null, o, null, null, null);

                    object oLabel = objHande.Unwrap();

                    IAlterPrint = oLabel as FS.HISFC.BizProcess.Interface.FeeInterface.IMoneyAlert;
                }


            }
            catch (System.TypeLoadException ex)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                MessageBox.Show( "命名空间无效\n" + ex.Message );
                return;
            }
            #endregion

            #region {80C40729-D5C1-42ce-96C3-7CF09E562BA7}
            this.repayType = this.ctrlParmMgr.GetControlParam<string>("200309", true, "0");

            if (this.repayType == "2")
            {
                this.neuSpread1_Sheet1.Columns[11].Visible = true;
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[11].Visible = false;
            }
            #endregion
        }

        /// <summary>
        /// 更新显示数据信息
        /// </summary>
        /// <param name="myNurse"></param>
        public void RefreshList(string myNurse)
        {
            this.labDay.Text = "打印日期:" + this.Patient.GetSysDate();
            string alertText = "";
            string myWhere = "";
            //判断是否宜康病区
            if (Function.IsContainYKDept(employee.Dept.ID))
            {
                myWhere = @"select d.code from com_dictionary d where d.type='YkDept'";

            }
            else
            {
                myWhere = @"select c.dept_code from com_department c where c.dept_type='I'and c.valid_state=fun_get_valid()";
            
            }
             
           // string myWhere = "'" + myNurse + "'";
             
            ArrayList list = new ArrayList();
            if (string.IsNullOrEmpty(this.txtAlert.Text.Trim()))
            {
                MessageBox.Show("请输入查询金额！");
                return ;
            }
            try
            {
                if (this.cmbType.Text == "按指定标准")
                {
                    list = this.Patient.GetAlertPerson(myWhere, NConvert.ToDecimal(this.txtAlert.Text));
                    alertText = this.txtAlert.Text.ToString();
                    
                    if (alertText[0] == '-')
                        alertText = alertText.Remove(0, 1);
                }
                else if (this.cmbType.Text == "按比例")
                {
                    list = this.Patient.GetAlertPercent(myWhere, NConvert.ToDecimal(this.txtAlert.Text) / 100);
                    alertText = this.txtAlert.Text.ToString();
                    if (alertText[0] == '-')
                        alertText = alertText.Remove(0, 1);

                }
                else if (this.cmbType.Text == "按最底下限")
                {
                    list = this.Patient.GetAlertPerson(myWhere);
                    alertText = this.txtAlert.Text.ToString();
                    if (alertText[0] == '-')
                        alertText = alertText.Remove(0, 1);
                }
                string date = "";

                //添加数据到控件中
                NeuObject obj = null;
                FS.HISFC.Models.RADT.PatientInfo patient = null;
                if (list == null)
                {
                    list = new ArrayList();
                }
                
                this.neuSpread1_Sheet1.RowCount = list.Count;
                for (int i = 0; i < list.Count; i++)
                {
                    patient = list[i] as FS.HISFC.Models.RADT.PatientInfo;
                    if (patient == null) return;
                    
                    obj = this.managerIntegrate.GetConstant(FS.HISFC.Models.Base.EnumConstant.PAYKIND.ToString(),patient.Pact.PayKind.ID);
                     this.neuSpread1_Sheet1.Cells[i, 0].Value = true;
                    this.neuSpread1_Sheet1.Cells[i, 1].Value = patient.PVisit.PatientLocation.NurseCell.Name;
                    this.neuSpread1_Sheet1.Cells[i, 2].Value = patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                    this.neuSpread1_Sheet1.Cells[i, 4].Value = patient.PID.ID;
                    this.neuSpread1_Sheet1.Cells[i, 3].Value = patient.Name;
                    this.neuSpread1_Sheet1.Cells[i, 5].Value = obj.Name;
                    this.neuSpread1_Sheet1.Cells[i, 6].Value = patient.Pact.Name;
                    this.neuSpread1_Sheet1.Cells[i, 7].Value = patient.FT.PrepayCost.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 8].Value = patient.FT.TotCost.ToString();

                    this.neuSpread1_Sheet1.Cells[i, 9].Value = (patient.FT.PrepayCost - patient.FT.LeftCost).ToString();//自付金额
                    this.neuSpread1_Sheet1.Cells[i, 10].Value = patient.FT.LeftCost.ToString();
                    this.neuSpread1_Sheet1.Cells[i, 11].Value = patient.PVisit.MoneyAlert.ToString();//patient.PVisit.PatientLocation.Dept.Name;
                    date = patient.PVisit.InTime.Year.ToString().Substring(2, 2) + "-";
                    if (patient.PVisit.InTime.Month < 10)
                        date = date + "0" + patient.PVisit.InTime.Month.ToString() + "-";
                    else
                        date = date + patient.PVisit.InTime.Month.ToString() + "-";
                    if (patient.PVisit.InTime.Day < 10)
                        date = date + "0" + patient.PVisit.InTime.Day.ToString();
                    else
                        date = date + patient.PVisit.InTime.Day.ToString();


                    this.neuSpread1_Sheet1.Cells[i, 11].Value = date;
                    //this.neuSpread1_Sheet1.Cells[i, 0].Value = true;
                    //this.neuSpread1_Sheet1.Cells[i, 1].Value = patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                    //this.neuSpread1_Sheet1.Cells[i, 3].Value = patient.PID.ID;
                    //this.neuSpread1_Sheet1.Cells[i, 2].Value = patient.Name;
                    //this.neuSpread1_Sheet1.Cells[i, 4].Value = obj.Name;
                    //this.neuSpread1_Sheet1.Cells[i, 5].Value = patient.Pact.Name;
                    //this.neuSpread1_Sheet1.Cells[i, 6].Value = patient.FT.PrepayCost.ToString();
                    //this.neuSpread1_Sheet1.Cells[i, 7].Value = patient.FT.TotCost.ToString();

                    //this.neuSpread1_Sheet1.Cells[i, 8].Value = (patient.FT.PrepayCost - patient.FT.LeftCost).ToString();//自付金额
                    //this.neuSpread1_Sheet1.Cells[i, 9].Value = patient.FT.LeftCost.ToString();
                    //this.neuSpread1_Sheet1.Cells[i, 10].Value = patient.PVisit.MoneyAlert.ToString();//patient.PVisit.PatientLocation.Dept.Name;
                    //date = patient.PVisit.InTime.Year.ToString().Substring(2, 2) + "-";
                    //if (patient.PVisit.InTime.Month < 10)
                    //    date = date + "0" + patient.PVisit.InTime.Month.ToString() + "-";
                    //else
                    //    date = date + patient.PVisit.InTime.Month.ToString() + "-";
                    //if (patient.PVisit.InTime.Day < 10)
                    //    date = date + "0" + patient.PVisit.InTime.Day.ToString();
                    //else
                    //    date = date + patient.PVisit.InTime.Day.ToString();


                    //this.neuSpread1_Sheet1.Cells[i, 10].Value = date;
                    this.neuSpread1_Sheet1.Rows[i].Tag = patient;

                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }
        #endregion

        #region 事件
        private void ucAlert_Load(object sender, EventArgs e)
        {
            try
            {
                NurseCode = (Patient.Operator as FS.HISFC.Models.Base.Employee).Nurse;
                //重新计算医保余额
                ArrayList alPatientInfo = this.Patient.GetAlertPerson("'"+NurseCode.ID+"'", 10000);
                DateTime dtEnd=this.Patient.GetDateTimeFromSysDateTime();
                foreach (FS.HISFC.Models.RADT.PatientInfo patient in alPatientInfo)
                {
                    if (-1 == this.FeeInterate.ComputeSiFreeCost(patient, dtEnd))
                    {
                        continue;
                    }
                }

                initControl();
                RefreshList(NurseCode.ID);
            }
            catch { }
        }

        private void cmbType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.cmbType.Text == "按指定标准")
            {
                this.labShow.Text = "指标：";
                this.labShow.Visible = true;
                this.txtAlert.Visible = true;
                this.labPercent.Visible = false;
                this.lblInfo.Text = "(可用余额 < " + this.txtAlert.Text + ")";
            }
            else if (this.cmbType.Text == "按比例")
            {
                this.labShow.Text = "比例：";
                this.labShow.Visible = true;
                this.txtAlert.Visible = true;
                this.labPercent.Visible = true;
                this.lblInfo.Text = "(余额 / 预交金 <= " + this.txtAlert.Text + "%)";
            }
            else if (this.cmbType.Text == "按最底下限")
            {
                this.labShow.Visible = false;
                this.txtAlert.Visible = false;
                this.labPercent.Visible = false;
                this.lblInfo.Text = "(按最低下限统计)";
            }
        }

        private void txtAlert_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbType.Text == "按指定标准")
            {
                this.labShow.Text = "指标：";
                this.labShow.Visible = true;
                this.txtAlert.Visible = true;
                this.labPercent.Visible = false;
                if (this.chkAll.Checked)
                {
                    this.lblInfo.Text = "";
                }
                else
                {
                    this.lblInfo.Text = "(可用余额 < " + this.txtAlert.Text + ")";
                }
            }
            else if (this.cmbType.Text == "按比例")
            {
                this.labShow.Text = "比例：";
                this.labShow.Visible = true;
                this.txtAlert.Visible = true;
                this.labPercent.Visible = true;
                this.lblInfo.Text = "(余额 / 预交金 <= " + this.txtAlert.Text + "%)";
            }
            else if (this.cmbType.Text == "按最底下限")
            {
                this.labShow.Visible = false;
                this.txtAlert.Visible = false;
                this.labPercent.Visible = false;
                this.lblInfo.Text = "(按最低下限统计)";
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkAll.Checked)
                this.txtAlert.Text = "1000000";
            else
                this.txtAlert.Text = "0";

            this.RefreshList(NurseCode.ID);
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("打印表格", "打印表格", FS.FrameWork.WinForms.Classes.EnumImageList.D打印, true, false, null);
            return toolBarService;
        }

        

        private void btnCompute_Click(object sender, EventArgs e)
        {
            RefreshList(this.NurseCode.ID);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            //FS.FrameWork.WinForms.Classes.Print print = new FS.FrameWork.WinForms.Classes.Print();
            //print.PrintPage(0, 0, this.neuPanel1);
            for(int i =0;i< this.neuSpread1_Sheet1.RowCount;i++)
            {
                if (this.neuSpread1_Sheet1.Cells[i, 0].Value.ToString() == "True")
                {
                    #region {80C40729-D5C1-42ce-96C3-7CF09E562BA7}
                    FS.HISFC.Models.RADT.PatientInfo selectPatient = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.RADT.PatientInfo;
                    if (selectPatient.Pact.PayKind.ID =="02")
                    {
                        selectPatient.FT.OwnCost = selectPatient.FT.PrepayCost - selectPatient.FT.LeftCost;
                        selectPatient.FT.PayCost = 0;
                    }
                    //借用这个字段催款金额输入方式
                    selectPatient.PVisit.AdmittingDoctor.User02 = this.repayType;

                    if (this.repayType == "2")
                    {
                        //借用这个字段存储输入的金额
                        selectPatient.PVisit.AdmittingDoctor.User01 = this.neuSpread1_Sheet1.Cells[i, 11].Text;
                    }

                    this.IAlterPrint.PatientInfo = selectPatient;
                    #endregion
                    this.IAlterPrint.SetPatientInfo();
                }
            }
        }
        #endregion

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader && e.Column == 0 )
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, 0].Value = true;
                }
            }
        }

        /// <summary>
        /// {1C1BD0C8-A5E2-4da2-BE99-B31BB34226EA}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                this.neuSpread1_Sheet1.Cells[i, 0].Value = this.chkSelectAll.Checked;
                
            }
        }

        /// <summary>
        /// {80C40729-D5C1-42ce-96C3-7CF09E562BA7}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrintGrid_Click(object sender, EventArgs e)
        {
            this.neuSpread1_Sheet1.Columns[4].Visible = false;
            this.neuSpread1_Sheet1.Columns[0].Visible = false;
            this.neuSpread1_Sheet1.Columns[11].Visible = false;

            FS.FrameWork.WinForms.Classes.Print p = new FS.FrameWork.WinForms.Classes.Print();

            p.PrintPreview(this.neuPanel1);

            this.neuSpread1_Sheet1.Columns[4].Visible = true;
            this.neuSpread1_Sheet1.Columns[0].Visible = true;
            if (this.repayType == "2")
            {
                this.neuSpread1_Sheet1.Columns[11].Visible = true;
            }
            else
            {
                this.neuSpread1_Sheet1.Columns[11].Visible = false;
            }
            
        }

        protected override int OnPrint(object sender, object neuObject)
        {
             btnPrint_Click(sender, null);
             return 1;
        }


    }
}
