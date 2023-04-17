using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Nurse.Controls
{
    /// <summary>
    /// [��������: ���߿�Ƭ]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPatientCard : UserControl,
        FS.FrameWork.WinForms.Classes.IControlPrintable
    {
        public ucPatientCard()
        {
            InitializeComponent();
        }

        #region IControlPrintable ��Ա

        public int BeginHorizontalBlankWidth
        {
            get
            {
                return 5;
            }
            set
            {
               
            }
        }

        public int BeginVerticalBlankHeight
        {
            get
            {
                return 0;
            }
            set
            {
                
            }
        }

        public System.Collections.ArrayList Components
        {
            get
            {
                return null;
            }
            set
            {
                
            }
        }

        public Size ControlSize
        {
            get {  return new Size (181, 288); }
        }
        protected int vnum = 0;
        protected int hnum = 0;
        public object ControlValue
        {
            get
            {
                return null;
            }
            set
            {
                FS.HISFC.Models.RADT.PatientInfo PInfo = value as FS.HISFC.Models.RADT.PatientInfo;
                if (PInfo == null) return;
                if (PInfo.PVisit.PatientLocation.Bed.Status.ID.ToString() == "W")
                    this.lblBed.Text = PInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "������";
                else
                    this.lblBed.Text = PInfo.PVisit.PatientLocation.Bed.ID.Length >= 4 ? PInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : PInfo.PVisit.PatientLocation.Bed.ID;

                this.lblName.Text = PInfo.Name;
                this.lblSex.Text = PInfo.Sex.Name;

                if (PInfo.Name != "")
                    this.lblAge.Text = PInfo.Age;

                this.lblInpatientNo.Text = PInfo.PID.PatientNO;
                this.lblFood.Text = PInfo.Disease.Memo;
                this.lblTend.Text = PInfo.Disease.Tend.Name;
                if (PInfo.Diagnoses != null && PInfo.Diagnoses.Count > 0) this.lblDiagnose.Text = PInfo.Diagnoses[0].ToString();
                this.lblIndate.Text = PInfo.PVisit.InTime.ToString().Substring(0, 10);
                if (PInfo.Diagnoses.Count > 0) this.lblDiagnose.Text = PInfo.Diagnoses[0].ToString();
			
            }
        }

        public int HorizontalBlankWidth
        {
            get
            {
                return 10;
            }
            set
            {
                
            }
        }

        public int HorizontalNum
        {
            get
            {
                return hnum;
            }
            set
            {
                hnum = value;
            }
        }

        public bool IsCanExtend
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public bool IsShowGrid
        {
            get
            {
                return false;
            }
            set
            {
                
            }
        }

        public int VerticalBlankHeight
        {
            get
            {
                return 10;
            }
            set
            {
                
            }
        }

        public int VerticalNum
        {
            get
            {
                return vnum;
            }
            set
            {
                vnum = value;
            }
        }

        #endregion
    }
}
