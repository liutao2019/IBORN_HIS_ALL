using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FS.SOC.HISFC.BizProcess.CommonInterface;
using System.Collections;

namespace SOC.Local.Assign.ShenZhen.BinHai.IPatientInfo
{
    public partial class ucPatientInfo : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.SOC.HISFC.Assign.Interface.Components.IPatientInfo
    {
        public ucPatientInfo()
        {
            InitializeComponent();
        }

        #region IPatientInfo 成员

        public int ShowPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            this.nlbCardNO.Text = "病历号：" + register.PID.CardNO;
            this.nlbPatientName.Text = "患者：" + register.Name + " " + register.Sex.Name + " " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(register.Birthday) + " " + register.PhoneHome;
            this.nlbRecipDoctor.Text = "医生：" + CommonController.CreateInstance().GetEmployeeName(register.DoctorInfo.Templet.Doct.ID);
            this.nlbRecipeDept.Text = "科室：" + CommonController.CreateInstance().GetDepartmentName(register.DoctorInfo.Templet.Dept.ID);
            this.nlbRegDate.Text = "挂号：" + register.DoctorInfo.SeeDate.ToString();

            return 1;
        }

        public int ShowPatientInfo(FS.SOC.HISFC.Assign.Models.Assign assign)
        {
            this.nlbCardNO.Text = "病历号：" + assign.Register.PID.CardNO;
            ArrayList regList = (new FS.HISFC.BizProcess.Integrate.Registration.Registration()).QueryPatient(assign.Register.ID);
            FS.HISFC.Models.Registration.Register register = new FS.HISFC.Models.Registration.Register();
            if (regList.Count > 0)
            {
                register = regList[0] as FS.HISFC.Models.Registration.Register;
            }
            this.nlbPatientName.Text = "患者：" + assign.Register.Name + " " + assign.Register.Sex.Name + " " + (new FS.FrameWork.Management.DataBaseManger()).GetAge(register.Birthday) + " " + register.PhoneHome;
            this.nlbRecipDoctor.Text = "医生：" + CommonController.CreateInstance().GetEmployeeName(assign.Queue.Doctor.ID);
            this.nlbRecipeDept.Text = "科室：" + CommonController.CreateInstance().GetDepartmentName(assign.Queue.AssignDept.ID);
            string ynSeeDate = assign.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Out ? assign.OutTime.ToString() : "";
            if (register.RegType == FS.HISFC.Models.Base.EnumRegType.Pre)
            {
                this.nlbRegDate.Text = "分诊：" + assign.TirageTime.ToString() + " 进诊：" + assign.InTime.ToString() + " 预约时间：" + register.DoctorInfo.Templet.Begin.ToString("HH:mm")
                                    + "-" + register.DoctorInfo.Templet.End.ToString("HH:mm");// " 已诊：" + ynSeeDate;
            }
            else
            {
                this.nlbRegDate.Text = "分诊：" + assign.TirageTime.ToString() + " 进诊：" + assign.InTime.ToString();// " 已诊：" + ynSeeDate;
            
            }

            return 1;
        }

        #endregion

        #region IClearable 成员

        public int Clear()
        {
            this.nlbCardNO.Text = "病历号：";
            this.nlbPatientName.Text = "患者：";
            this.nlbRecipDoctor.Text = "医生：";
            this.nlbRecipeDept.Text = "科室：";
            this.nlbRegDate.Text = "挂号：";

            return 1;
        }

        #endregion
    }
}
