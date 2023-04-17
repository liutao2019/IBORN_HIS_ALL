using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace UFC.Lis
{
    /// <summary>
    /// [��������: ���鵥��ӡ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// </summary>
    public partial class ucPrintLisApply : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Classes.IControlPrintable
    {
        public ucPrintLisApply()
        {
            InitializeComponent();
        }


        protected object controlvalue = null;

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
            get
            {
                return new Size(784, 308);
            }
        }

        public object ControlValue
        {
            get
            {
                return this.controlvalue;
            }
            set
            {
                if (value == null)
                {
                    MessageBox.Show("���鵥����������ʹ���!", "��ʾ");
                    return;
                }

                //סԺ�鵥
                if (value.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
                {
                    FS.HISFC.Models.RADT.PatientInfo p = value as FS.HISFC.Models.RADT.PatientInfo;
                    this.SetItem(p, p.User01, p.User02, p.User03);
                    this.controlvalue = p;
                }
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

        	protected int hNum =0;
        protected int vNum = 0;

        public int HorizontalNum
        {
            get
            {
                return hNum;
            }
            set
            {
                hNum = value;
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
                return 0;
            }
            set
            {
                
            }
        }

        public int VerticalNum
        {
            get
            {
                return vNum;
            }
            set
            {
                vNum = value;
            }
        }

        #endregion

        /// <summary>
        /// ��ǰ������Ŀ
        /// </summary>
        protected virtual void SetItem(FS.HISFC.Models.RADT.PatientInfo patientinfo, string id, string SampleName, string Item)
        {
            try
            {
                if (patientinfo.ExtendFlag1 == "True")
                {
                    this.lbEmc.Visible = true;
                }
                if (patientinfo.ExtendFlag2 == "True")
                {
                    this.lbPrePrint.Visible = true;
                }
                this.lbExecDept.Text += patientinfo.PVisit.User01;

                string strPatientInfo = "������{0}    ���ţ�{1}   �Ա�{2}  ���䣺{3}  סԺ�ţ�{4}  ���ң�{5}    ";
                this.lbPatientInfo.Text = string.Format(strPatientInfo,patientinfo.Name,patientinfo.PVisit.PatientLocation.Bed.Name,patientinfo.Sex.Name,patientinfo.Age,patientinfo.PID.PatientNO,patientinfo.PVisit.PatientLocation.Dept.Name);

                this.lbListID.Text = "���鵥�ţ�" + id;
                this.lbDoc.Text = "����ҽ����" + patientinfo.PVisit.User02;
                this.lbDate.Text = "�ͼ����ڣ�" + patientinfo.PVisit.User03;

                this.lbSampleNam.Text = "�������ͣ�" + SampleName;
                this.lbItem.Text = "�ͼ�Ŀ�ģ�\n" + Item;

                if (patientinfo.Diagnoses.Count > 0)
                {
                    this.lbDiagnose.Text = "��ϣ�" + patientinfo.Diagnoses[0].ToString();
                }
            }
            catch { }
        }

        private string SetAge(DateTime birthday)
        {
            if (birthday == DateTime.MinValue)
            {
                return "";
            }

            DateTime current;
            int year, month, day;

            current = DateTime.Now;
            year = current.Year - birthday.Year;
            month = current.Month - birthday.Month;
            day = current.Day - birthday.Day;

            if (year > 0)
            {
                return year.ToString() + "��";
            }
            else if (month > 0)
            {
                return month.ToString() + "��";
            }
            else if (day > 0)
            {
                return day.ToString() + "��";
            }

            return "";
        }

        protected override void OnLoad(EventArgs e)
        {            
            base.OnLoad(e);
        }

    }
}