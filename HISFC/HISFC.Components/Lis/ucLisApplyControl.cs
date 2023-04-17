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
    /// [��������: ���鵥������ʾ]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2008-03]<br></br>
    /// </summary>
    public partial class ucLisApplyControl : FS.FrameWork.WinForms.Controls.ucBaseControl,FS.FrameWork.WinForms.Classes.IControlPrintable
    {
        public ucLisApplyControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �Ƿ�ѡ��
        /// </summary>
        protected bool bSelected = false;

        /// <summary>
        /// ���
        /// </summary>
        protected string diagNose = "";

        /// <summary>
        /// �Ƿ�ѡ��
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return this.bSelected;
            }
            set
            {
                this.bSelected = value;
                if (value)
                {
                    this.BackColor = Color.FromArgb(224, 224, 224);
                }
                else
                {
                    this.BackColor = Color.FromArgb(255, 255, 255);
                }
            }
        }

        #region IControlPrintable ��Ա

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

        public int BeginVerticalBlankHeight
        {
            get
            {
                return 5;
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

        public ArrayList Comonents
        {
            get
            {
                return null;
            }
            set
            {

            }
        }
        protected int hNum = 0;
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
        /// <summary>
        /// �ؼ���С
        /// </summary>
        public Size ControlSize
        {
            get
            {
                return new Size(584, 112);
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
        protected object controlvalue = null;
        /// <summary>
        /// ��ǰ��ֵ
        /// </summary>
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

                this.diagNose = "";                

                //סԺ�鵥
                if (value.GetType() == typeof(FS.HISFC.Models.RADT.PatientInfo))
                {
                    FS.HISFC.Models.RADT.PatientInfo p = value as FS.HISFC.Models.RADT.PatientInfo;

                    this.SetItem(p, p.User01, p.User02, p.User03);
                    this.controlvalue = p;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
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
                else
                {
                    this.lbEmc.Visible = false;
                }

                this.lbAge.Text = "���䣺" + patientinfo.Age;
                this.lbBed.Text = "����" + patientinfo.PVisit.PatientLocation.Bed.ID;
                this.lbDate.Text = "�ͼ����ڣ�" + patientinfo.PVisit.User03;

                if (patientinfo.Diagnoses.Count > 0)
                {
                    this.lbDiagnose.Text = "��ϣ�" + patientinfo.Diagnoses[0].ToString();
                }
                this.lbDoc.Text = "ҽ����" + patientinfo.PVisit.User02;
                this.lbName.Text = "������" + patientinfo.Name;
                this.lbID.Text = "סԺ�ţ�" + patientinfo.PID.PatientNO;
                this.lbListNO.Text = "ִ�п��ң�" + patientinfo.PVisit.User01;
                this.lbSample.Text = "������" + SampleName;
                this.lbSex.Text = "�Ա�" + patientinfo.Sex.Name;
                this.lbTarget.Text = "�ͼ�Ŀ�ģ�" + Item + "  X" + patientinfo.PVisit.Memo;
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


        private void ucLisApplyControl_Click(object sender, EventArgs e)
        {
            this.IsSelected = !this.IsSelected;
        }

        #region IControlPrintable ��Ա


        public ArrayList Components
        {
            get
            {
                return new ArrayList();
            }
            set
            {

            }
        }

        #endregion
    }
}
