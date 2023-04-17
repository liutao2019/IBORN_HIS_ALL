using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.SOC.Local.NurseWorkStation.QiaoTou
{
    public partial class ucBedCard : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Classes.IControlPrintable
    {

        private int vnum = 0;
        private int hnum = 0;

        /// <summary>
        /// 
        /// </summary>
        public ucBedCard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orgBedNo"></param>
        /// <returns></returns>
        private string BedDisplay(string orgBedNo)
        {
            if (orgBedNo == "")
            {
                return orgBedNo;
            }

            string tempBedNo = "";
            //int tempBedNoInt = 0;

            if (orgBedNo.Length > 4)
            {
                tempBedNo = orgBedNo.Substring(4);
            }
            else
            {
                return orgBedNo;
            }
            return tempBedNo;
        }

        #region 变量

        //传递患者信息类
        private FS.HISFC.Models.RADT.PatientInfo patient;

        /// <summary>
        /// 接收传过来的患者信息
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo Patient
        {
            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;
                SetInfo(); 
            }

        }
        /// <summary>
        /// 诊断
        /// </summary>
        FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase diagManager = new FS.HISFC.BizProcess.Integrate.HealthRecord.HealthRecordBase();

        #endregion

        #region 方法

        /// <summary>
        /// 传递病人实体信息
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.Patient = neuObject as FS.HISFC.Models.RADT.PatientInfo;
            return base.OnSetValue(neuObject, e);
        }
        /// <summary>
        /// 设置显示信息
        /// </summary>
        private void SetInfo()
        {
            if (this.Patient != null)
            {
                this.lblName.Text = this.Patient.Name.ToString();
                this.lblBed.Text = this.Patient.PVisit.PatientLocation.Bed.ID.ToString();
                this.lblAge.Text = this.Patient.Age.ToString();
                this.lblSex.Text = this.Patient.Sex.ToString();
                this.lblIndate.Text = this.Patient.PVisit.InTime.ToString();
                this.lblPatient.Text = this.Patient.PID.PatientNO.ToString();
            }
        }

        #endregion

        #region IControlPrintable 成员

        public int BeginHorizontalBlankWidth
        {
            get
            {
                return 50;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public int BeginVerticalBlankHeight
        {
            get
            {
                return 10;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public System.Collections.ArrayList Components
        {
            get
            {
                return null;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public Size ControlSize
        {
            get 
            {
                //return new Size(205, 183);
                return new Size (196,243);
                //throw new Exception("The method or operation is not implemented."); 
            }
        }

        public object ControlValue
        {
            get
            {
                return null;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                FS.HISFC.Models.RADT.PatientInfo P = value as FS.HISFC.Models.RADT.PatientInfo;
                if (P == null) return;

                this.lblName.Text = P.Name;
                this.lblBed.Text = this.BedDisplay(P.PVisit.PatientLocation.Bed.ID);
                this.lblSex.Text = P.Sex.Name;
                this.lblAge.Text = P.Age;
                this.lblPatient.Text = P.PID.PatientNO;
                if (P.PVisit.InTime.ToString().Substring(6, 1) != "-")
                    this.lblIndate.Text = P.PVisit.InTime.ToString().Substring(0, 10);
                else
                    this.lblIndate.Text = P.PVisit.InTime.ToString().Substring(0, 9);
                //throw new Exception("The method or operation is not implemented.");

                this.lblDept.Text = P.PVisit.PatientLocation.Dept.Name;
                ArrayList alDiag = diagManager.QueryDiagnoseNoOps(P.ID);

                if (alDiag != null && alDiag.Count > 0)
                {
                    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in alDiag)
                    {
                        if (diag.DiagInfo.DiagType.ID == "1")
                        {
                            this.lblDiagnose.Text = diag.DiagInfo.Name;
                        }
                    }
                }
                else
                {
                    this.lblDiagnose.Text = "";
                }
                this.lblDoctor  .Text = P.PVisit.AdmittingDoctor.Name;
                this.lblNurse .Text = P.PVisit.AdmittingNurse.Name;

                this.lblAllergy.Text = "";

            }
        }

        public int HorizontalBlankWidth
        {
            get
            {
                return 10;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public int HorizontalNum
        {
            get
            {
                return this.hnum;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                this.hnum = value;
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public bool IsCanExtend
        {
            get
            {
                return false;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public bool IsShowGrid
        {
            get
            {
                return false;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public int VerticalBlankHeight
        {
            get
            {
                return 10;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        public int VerticalNum
        {
            get
            {
                return this.vnum;
                //throw new Exception("The method or operation is not implemented.");
            }
            set
            {
                this.vnum = value;
                //throw new Exception("The method or operation is not implemented.");
            }
        }

        #endregion


    }
}
