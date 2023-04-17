using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Controls
{
    public partial class ucConsultationNotice : UserControl
    {
        public ucConsultationNotice()
        {
            InitializeComponent();
            GetHospLogo();
        }

        /// <summary>
        /// 获取医院名称和医院LOGO
        /// </summary>
        private void GetHospLogo()
        {
            try
            {
                this.lblHosInfo.Text = FS.FrameWork.Management.Connection.Hospital.Name;
                lblHosInfo.Visible = true;

                string erro = "出错";
                string imgpath = FS.FrameWork.WinForms.Classes.Function.CurrentPath + FS.HISFC.Components.Order.Classes.Function.GetHospitalLogo("Xml\\HospitalLogoInfo.xml", "Hospital", "Logo", erro);
                picbLogo.Image = Image.FromFile(imgpath);

                this.lblHosInfo.Visible = false;
            }
            catch
            {
            }
        }

        /// <summary>
        /// 设置会诊通知单 值
        /// </summary>
        public void SetValues(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Order.Consultation consultation)
        {
            //姓名
            this.lblName.Text = patient.Name;
            //姓别
            this.lblSex.Text = patient.Sex.Name;
            //年龄
            this.lblAge.Text = patient.Age;
            //病区
            this.lblPatiArea.Text = patient.PVisit.PatientLocation.Dept.Name;

            //床号  patientInfo.PVisit.PatientLocation.Bed.ID;
            if (patient.PVisit.PatientLocation.Bed.ID.Length > 3)
            {
                this.lblBedNo.Text = patient.PVisit.PatientLocation.Bed.ID.Substring(4);
            }
            else
            {
                this.lblBedNo.Text = patient.PVisit.PatientLocation.Bed.ID;
            }

            //住院号
            this.lblPatiNo.Text = patient.PID.PatientNO.TrimStart('0');
    
            //目前诊断---会诊摘要
            this.lblDiag.Text = consultation.Name; 
            
            //邀请科室---会诊科室
            this.lblDept.Text = consultation.DeptConsultation.Name;

            //邀请医师---会诊医师
            this.lblDoct.Text = consultation.DoctorConsultation.Name;

            //会诊目的---会诊意见
            this.lblPurpose.Text = consultation.Result;

            //会诊日期---会诊日期
            if (consultation.Memo.Equals("no"))
            {
                this.lblTime.Text = "";
            }
            else
            {
                this.lblTime.Text = consultation.ConsultationTime.ToString();
            }

            //会诊地点
            this.lblPlace.Text = consultation.Dept.Name;

            //标记紧急与否
            if (consultation.IsEmergency)
            {
                this.chk2.Checked = true;
                this.chk1.Checked = false;
            }
            else
            {
                this.chk2.Checked = false;
                this.chk1.Checked = true;
            }

            //院外会诊 与 是否开立医嘱
            if (consultation.IsCreateOrder)
            {
                this.chk4.Checked = true;
                this.chk3.Checked = false;
            }
            else
            {
                this.chk4.Checked = false;
            }
        }

    }
}
