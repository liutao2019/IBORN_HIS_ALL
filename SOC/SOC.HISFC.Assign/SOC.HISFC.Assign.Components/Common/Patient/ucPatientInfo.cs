using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;

namespace FS.SOC.HISFC.Assign.Components.Common.Patient
{
    /// <summary>
    /// [功能描述: 门诊分诊患者信息显示]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2011-12]<br></br>
    /// </summary>
    internal partial class ucPatientInfo : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.Assign.Interface.Components.IPatientInfo
    {
        public ucPatientInfo()
        {
            InitializeComponent();
        }

        #region IPatientInfo 成员

        public int ShowPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.nlbCardNO.Text = "病人号：" + register.PID.PatientNO;
            this.nlbPatientName.Text = "患者：" + register.Name + " " + register.Sex.Name + " " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(register.Birthday);
            this.nlbRecipDoctor.Text = "医生：" + CommonController.CreateInstance().GetEmployeeName(register.DoctorInfo.Templet.Doct.ID);
            this.nlbRecipeDept.Text = "科室：" + CommonController.CreateInstance().GetDepartmentName(register.DoctorInfo.Templet.Dept.ID);
            this.nlbRegDate.Text = "挂号：" + register.DoctorInfo.SeeDate.ToString();

            return 1;
        }

        public int ShowPatientInfo(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            this.nlbCardNO.Text = "病人号：" + assign.Register.PID.PatientNO;
            this.nlbPatientName.Text = "患者：" + assign.Register.Name + " " + assign.Register.Sex.Name + " " + assign.Register.Age;
            this.nlbRecipDoctor.Text = "医生：" + CommonController.CreateInstance().GetEmployeeName(assign.Queue.Doctor.ID);
            this.nlbRecipeDept.Text = "科室：" + CommonController.CreateInstance().GetDepartmentName(assign.Queue.AssignDept.ID);
            string ynSeeDate = assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Out ? assign.OutTime.ToString() : "";
            this.nlbRegDate.Text = "分诊：" + assign.TirageTime.ToString() + " 进诊：" + assign.InTime.ToString() + " 已诊：" + ynSeeDate;

            return 1;
        }

        #endregion

        #region IClearable 成员

        public int Clear()
        {
            this.nlbCardNO.Text = "病人号：";
            this.nlbPatientName.Text = "患者：";
            this.nlbRecipDoctor.Text = "医生：";
            this.nlbRecipeDept.Text = "科室：";
            this.nlbRegDate.Text = "挂号：";

            return 1;
        }

        #endregion
    }
}
