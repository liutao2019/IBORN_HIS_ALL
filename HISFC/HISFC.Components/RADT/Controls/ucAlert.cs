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

namespace FS.HISFC.Components.RADT.Controls
{
    public partial class ucAlert : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucAlert()
        {
            InitializeComponent();
            if (DesignMode) return;

            this.ntbStartCost.TextChanged += new EventHandler(ntbStartCost_TextChanged);
            this.ntbEnughCost.TextChanged += new EventHandler(ntbEnughCost_TextChanged);
            this.ntxtPatientNo.Leave += new EventHandler(ntxtPatientNo_Leave);
            this.btnCompute.Click += new EventHandler(btnCompute_Click);
            this.btnPrint.Click += new EventHandler(btnPrint_Click);
            this.btnPrintGrid.Click += new EventHandler(btnPrintGrid_Click);
            this.chkSelectAll.CheckedChanged += new EventHandler(chkSelectAll_CheckedChanged);
        }

        void ntxtPatientNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(ntxtPatientNo.Text.Trim()))
            {
                //this.ntxtPatientNo.Text = this.ntxtPatientNo.Text.PadLeft(10, '0');
            }
        }


        void ntbEnughCost_TextChanged(object sender, EventArgs e)
        {

        }

        void ntbStartCost_TextChanged(object sender, EventArgs e)
        {

        }

        #region 变量

        string errInfo = string.Empty;
        NeuObject nurseCode = null;
        FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.BizProcess.Integrate.Manager interMgr = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizProcess.Integrate.Fee feeIntergrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// 催款单打印接口
        /// </summary>
        FS.HISFC.BizProcess.Interface.Fee.IMoneyAlert IMoneyAlert = null;

        /// <summary>
        /// 控制参数业务类
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam _ctrlParmMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// 催款金额输入方式
        /// 0：录入起催金额和催足余额
        /// </summary>
        private string repayType = "0";

        #endregion

        #region 方法
        /// <summary>
        /// 初始化控件
        /// </summary>
        public void initControl()
        {
            if (IMoneyAlert == null)
            {
                IMoneyAlert = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Fee.IMoneyAlert)) as FS.HISFC.BizProcess.Interface.Fee.IMoneyAlert;
            }

            #region {80C40729-D5C1-42ce-96C3-7CF09E562BA7}
            this.repayType = this._ctrlParmMgr.GetControlParam<string>("200309", true, "0");

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

            ArrayList list = new ArrayList();
            if (string.IsNullOrEmpty(this.ntbStartCost.Text.Trim()))
            {
                MessageBox.Show("输入起催金额！");
                return;
            }
            try
            {
                string date = "";
                //添加数据到控件中
                NeuObject obj = null;
                FS.HISFC.Models.RADT.PatientInfo patient = null;
                if (list == null)
                {
                    list = new ArrayList();
                }
                if (string.IsNullOrEmpty(this.ntxtPatientNo.Text.Trim()))
                {
                    list = this.inpatientMgr.GetAlertPerson(this.nurseCode.ID, FS.FrameWork.Function.NConvert.ToDecimal(this.ntbStartCost.Text), this.ntxtPatientNo.Text.ToString().Trim());
                }
                else
                {
                    list = this.inpatientMgr.GetAlertPerson(this.nurseCode.ID, FS.FrameWork.Function.NConvert.ToDecimal(this.ntbStartCost.Text), this.ntxtPatientNo.Text);
                }

                if (list.Count > 0)
                {
                    FS.HISFC.Models.RADT.PatientInfo patientInfo = list[0] as FS.HISFC.Models.RADT.PatientInfo;
                    this.SetTitleInfo(patientInfo);
                }

                this.neuSpread1_Sheet1.RowCount = 0;
                this.neuSpread1_Sheet1.Columns[10].Width = 80F;
                int rowIndex = 0;
                for (int i = 0; i < list.Count; i++)
                {
                    patient = list[i] as FS.HISFC.Models.RADT.PatientInfo;
                    if (this.chkOwnFeePatient.Checked)
                    {
                        if (patient.Pact.PayKind.ID != "01")
                        {
                            continue;
                        }
                    }
                    this.neuSpread1_Sheet1.Rows.Add(this.neuSpread1_Sheet1.Rows.Count, 1);

                    if (patient == null) return;

                    obj = this.interMgr.GetConstant(FS.HISFC.Models.Base.EnumConstant.PAYKIND.ToString(), patient.Pact.PayKind.ID);
                    this.neuSpread1_Sheet1.Cells[rowIndex, 0].Value = true;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 1].Value = patient.PVisit.PatientLocation.Bed.ID.Substring(4);
                    this.neuSpread1_Sheet1.Cells[rowIndex, 3].Value = patient.PID.PatientNO;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 2].Value = patient.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 4].Value = obj.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 5].Value = patient.Pact.Name;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 6].Value = patient.FT.PrepayCost.ToString();
                    this.neuSpread1_Sheet1.Cells[rowIndex, 7].Value = patient.FT.TotCost.ToString();

                    this.neuSpread1_Sheet1.Cells[rowIndex, 8].Value = (patient.FT.PrepayCost - patient.FT.LeftCost).ToString();//自付金额
                    this.neuSpread1_Sheet1.Cells[rowIndex, 9].Value = patient.FT.LeftCost.ToString();
                    this.neuSpread1_Sheet1.Cells[rowIndex, 10].Value = patient.PVisit.MoneyAlert.ToString();//patient.PVisit.PatientLocation.Dept.Name;
                    date = patient.PVisit.InTime.Year.ToString().Substring(2, 2) + "-";
                    if (patient.PVisit.InTime.Month < 10)
                        date = date + "0" + patient.PVisit.InTime.Month.ToString() + "-";
                    else
                        date = date + patient.PVisit.InTime.Month.ToString() + "-";
                    if (patient.PVisit.InTime.Day < 10)
                        date = date + "0" + patient.PVisit.InTime.Day.ToString();
                    else
                        date = date + patient.PVisit.InTime.Day.ToString();


                    //this.neuSpread1_Sheet1.Cells[rowIndex, 10].Value = date;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 12].Value = FS.FrameWork.Function.NConvert.ToDecimal(this.ntbEnughCost.Text) - Math.Floor((patient.FT.LeftCost) / 100) * 100;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 12].Locked = true;
                    this.neuSpread1_Sheet1.Cells[rowIndex, 12].HorizontalAlignment = FarPoint.Win.Spread.CellHorizontalAlignment.Right;
                    //借用字段存应缴金额
                    patient.User01 = this.neuSpread1_Sheet1.Cells[rowIndex, 12].Text;
                    this.neuSpread1_Sheet1.Rows[rowIndex].Tag = patient;
                    this.neuSpread1_Sheet1.Rows[rowIndex].Tag = patient;
                    rowIndex++;
                    //this.neuSpread1_Sheet1.Cells[i, 10].Value = date;


                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return;
            }
        }

        /// <summary>
        /// 设置催款条件
        /// </summary>
        /// <param name="patientInfo"></param>
        private void SetTitleInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.ntbDept.Text = SOC.HISFC.BizProcess.Cache.Common.GetDept(patientInfo.PVisit.PatientLocation.NurseCell.ID).Name;
            this.ntbDept.Enabled = false;
            if (string.IsNullOrEmpty(this.ntbStartCost.Text))
            {
                this.ntbStartCost.Text = "500.00";
            }
            if (string.IsNullOrEmpty(this.ntbEnughCost.Text))
            {
                this.ntbEnughCost.Text = "500.00";
            }
        }
        #endregion

        #region 事件
        private void ucAlert_Load(object sender, EventArgs e)
        {
            try
            {
                nurseCode = (inpatientMgr.Operator as FS.HISFC.Models.Base.Employee).Nurse;
                //重新计算医保余额
                ArrayList alPatientInfo = this.inpatientMgr.GetAlertPerson(nurseCode.ID, -FS.FrameWork.Function.NConvert.ToInt32(this.ntbStartCost.Text), this.ntxtPatientNo.Text.ToString());
                DateTime dtEnd = this.inpatientMgr.GetDateTimeFromSysDateTime();
                foreach (FS.HISFC.Models.RADT.PatientInfo patient in alPatientInfo)
                {
                    if (this.feeIntergrate.ComputeSiFreeCost(patient, dtEnd) == -1)
                    {
                        continue;
                    }
                }

                initControl();
                RefreshList(nurseCode.ID);
            }
            catch { }
        }


        private void btnCompute_Click(object sender, EventArgs e)
        {
            int param = JudgeInput();
            if (param < 1)
            {
                this.ntbStartCost.Text = "500.00";
                this.ntbEnughCost.Text = "500.00";
            }
            RefreshList(this.nurseCode.ID);
        }

        private int JudgeInput()
        {
            decimal enughCost = 0m;
            decimal startCost = 0m;
            if (!string.IsNullOrEmpty(this.ntbEnughCost.Text.Trim()) && !decimal.TryParse(this.ntbEnughCost.Text.TrimStart('-'), out enughCost))
            {
                MessageBox.Show("催足余额不是数字，请确认！", "提示");
                return -1;
            }

            if (!string.IsNullOrEmpty(this.ntbStartCost.Text.Trim()) && !decimal.TryParse(this.ntbStartCost.Text.Trim(), out startCost))
            {
                MessageBox.Show("起催金额不是数字，请确认！", "提示");
                return -1;
            }
            this.ntbStartCost.Text = FS.FrameWork.Function.NConvert.ToDecimal(this.ntbStartCost.Text).ToString();

            this.ntbEnughCost.Text = FS.FrameWork.Function.NConvert.ToDecimal(this.ntbEnughCost.Text).ToString();

            return 1;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (IMoneyAlert == null)
            {
                MessageBox.Show("未维护打印催款单功能（接口），请联系系统管理员！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            decimal payMoney = 0;
            try
            {
                payMoney = FS.FrameWork.Function.NConvert.ToDecimal(ntbEnughCost.Text);
            }
            catch
            {
                MessageBox.Show("催足金额不是数字，请重新输入！", "警告", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            ArrayList alPatient = new ArrayList();
            for (int i = 0; i < this.neuSpread1_Sheet1.RowCount; i++)
            {
                object oValue = this.neuSpread1_Sheet1.Cells[i, 0].Value;
                if (oValue == null)
                {
                    continue;
                }

                string sValue = oValue.ToString().ToLower();
                if (sValue != "true")
                {
                    continue;
                }
                alPatient.Add(this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.RADT.PatientInfo);
            }
            if (alPatient.Count > 0)
            {
                IMoneyAlert.Print(alPatient, payMoney);
            }
        }

        #endregion

        private void neuSpread1_CellClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader && e.Column == 0)
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

            p.PrintPreview(this.neuSpread1);

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
    }
}
