using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.WinForms.Report.Order
{
    public partial class ucOrderShowQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucOrderShowQuery()
        {
            InitializeComponent();
        }
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new FS.HISFC.BizProcess.Integrate.RADT();
        protected override void OnLoad(EventArgs e)
        {
            this.ucOrderShow1.IsEnabledSubtbl = false;
            this.ucOrderShow1.Init(this,this,this);
            base.OnLoad(e);
        }

        private void ucQueryInpatientNo_myEvent()
        {
            #region 取患者信息
            string errText = "";
            FS.HISFC.Models.RADT.PatientInfo patientInfo;
            //回车触发事件
            this.Clear();

            if (this.ucQueryInpatientNo.InpatientNo == null || this.ucQueryInpatientNo.InpatientNo.Trim() == "")
            {
                FS.FrameWork.WinForms.Classes.Function.Msg("住院号错误，没有找到该患者", 111);
                this.ucQueryInpatientNo.Focus();
                return;
                //return -1;
            }
            try
            {
                  patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

                patientInfo = this.radtIntegrate.GetPatientInfomation(this.ucQueryInpatientNo.InpatientNo);
                //判断患者状态


                //if (this.ValidPatient(this.patientInfo) == -1)
                //{
                //    this.ucQueryInpatientNo.Focus();
                //    return -1;
                //}
                //if (patientInfo.FT.LeftCost < 0 && this.BlanceType != EBlanceType.Owe)
                //{
                //    FS.FrameWork.WinForms.Classes.Function.Msg("此患者已欠费，请缴纳预交金或者进行欠费设置后欠费结算！", 211);
                //    return;
                //}

                //赋上住院号


                //this.ucQueryInpatientNo.TextBox.Text = patientInfo.PID.PatientNO;
                //赋值患者信息


                //this.EvaluatePatientInfo(patientInfo);

            }
            catch (Exception ex)
            {
                FS.FrameWork.WinForms.Classes.Function.Msg(ex.Message, 211);
                return;
                //return -1;
            }

            #endregion
            //FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            //if (neuObject != null)
            //{
            //    if (neuObject.GetType() == patientInfo.GetType())
            //    {
            //        patientInfo = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            //        this.myPatient = patientInfo;
            lblName.Text = "患者姓名：" + patientInfo.Name;
            lblPatientNO.Text = "住院号：" + patientInfo.PID.PatientNO;
            lblDept.Text = "患者科室：" + patientInfo.PVisit.PatientLocation.Dept.Name;
            lblFreeCost.Text = "可用余额：" + patientInfo.FT.LeftCost.ToString();
            //1、界面中显示的患者基本信息中要显示出患者住院所在科室、病区及床号
            lbNurseCellName.Text = "病区：" + patientInfo.PVisit.PatientLocation.NurseCell.Name;
            lbBedName.Text = "床号：" + patientInfo.PVisit.PatientLocation.Bed.Name;
            //        if (this.seeAll)
            //        {
            //            alExecOrder = this.orderManager.QueryExecOrderByDept(patientInfo.ID, "2", false, "all");
            //        }
            //        else
            //        {
            //            alExecOrder = this.orderManager.QueryExecOrderByDept(patientInfo.ID, "2", false, oper.Dept.ID);
            //        }
            //        if (alExecOrder != null)
            //        {
            //            this.fpExecOrder.Sheets[0].RowCount = 0;
            //            this.AddExecOrderToFp(alExecOrder);
            //        }
            //    }
            //}
            //else
            //{

            //    return -1;
            //}
            this.ucOrderShow1.PatientInfo = patientInfo;
            //return base.OnSetValue(neuObject, e);
            return;
        }

        private void Clear()
        {
            lblName.Text = "患者姓名：";
            lblPatientNO.Text = "住院号：";
            lblDept.Text = "患者科室：";
            lblFreeCost.Text = "可用余额：";
            //1、界面中显示的患者基本信息中要显示出患者住院所在科室、病区及床号
            lbNurseCellName.Text = "病区：";
            lbBedName.Text = "床号：";
            try
            {
                //FS.HISFC.Models.RADT.PatientInfo patientInfo= new FS.HISFC.Models.RADT.PatientInfo();

                this.ucOrderShow1.PatientInfo = null;
            }
            catch { }
        }
    }
}
