using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.Forms
{
    /// <summary>
    /// [��������: ����Ƶ��]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2004-10-12]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class frmSpecialFrequency : Form
    {
        public frmSpecialFrequency()
        {
            InitializeComponent();
        }

        protected FS.HISFC.Models.Order.Frequency frequency = null;
        /// <summary>
        /// Ƶ��
        /// </summary>
        public FS.HISFC.Models.Order.Frequency Frequency
        {
            get
            {
                GetTime();
                GetDose();
                return frequency;
            }
            set
            {
                frequency = value;
                this.txtFrequency.Text = frequency.Name;
                for (int i = 0; i < frequency.Times.Length; i++)
                {
                    this.SetTime(i+1, frequency.Times[i]);
                }

            }
        }
        private void GetDose()
        {
           
            //�ж��Ƿ�һ��
            string s1 = "", s2 = "";
            bool diff = false;
            for (int i = 1; i <= 5; i++)
            {
                Control c = null;
                switch (i)
                {
                    case 1:
                        c = this.txtDose1;
                        break;
                    case 2:
                        c = this.txtDose2;
                        break;
                    case 3:
                        c = this.txtDose3;
                        break;
                    case 4:
                        c = this.txtDose4;
                        break;
                    case 5:
                        c = this.txtDose5;
                        break;
                }
                if (c.Enabled)
                {

                    if (s1 != "" && s1 != c.Text.Trim())
                        diff = true;
                    s1 = c.Text;
                }
            }

            if (diff == false)
            {
                this.dose = s1; //����һ��dose
                return;
            }
            //*******************************
            string s = "";
            for (int i = 1; i <= 5; i++)
            {
                Control c = null;
                switch (i)
                {
                    case 1:
                        c = this.txtDose1;
                        break;
                    case 2:
                        c = this.txtDose2;
                        break;
                    case 3:
                        c = this.txtDose3;
                        break;
                    case 4:
                        c = this.txtDose4;
                        break;
                    case 5:
                        c = this.txtDose5;
                        break;
                }
                if (c.Enabled)
                    s = s + c.Text + "-";
                
            }

            if (s.Length > 1)
                s = s.Substring(0, s.Length - 1);
            this.dose = s; //���ش�-��dose

        }
        private void GetTime()
        {
            string s = "";
            for (int i = 1; i <= 5; i++)
            {
                ComboBox c = null;
                switch (i)
                {
                    case 1:
                        c = this.cmbTime1;
                        break;
                    case 2:
                        c = this.cmbTime2;
                        break;
                    case 3:
                        c = this.cmbTime3;
                        break;
                    case 4:
                        c = this.cmbTime4;
                        break;
                    case 5:
                        c = this.cmbTime5;
                        break;
                }
                if (c.Enabled)
                    s = s + c.Text + "-";
            }

            if (s.Length > 1)
                s = s.Substring(0, s.Length - 1);
            frequency.Time = s;

        }
        private void SetTime(int i, string time)
        {
            ComboBox c = null;
            switch (i)
            {
                case 1:
                    c = this.cmbTime1 ;
                    break;
                case 2:
                    c =this.cmbTime2 ;
                    break;
                case 3:
                    c = this.cmbTime3;
                    break;
                case 4:
                    c = this.cmbTime4 ;
                    break;
                case 5:
                    c =this.cmbTime5 ;
                    break;
            }
            c.Text = time;
            if (c.Text == "")
            {
                c.Items.Add(time);
                c.Text = time;
            }
            c.Enabled = true;
        }

        private void SetDose(int i, string dose)
        {
            Control c = null;
            switch (i)
            {
                case 1:
                    c = this.txtDose1;
                    break;
                case 2:
                    c = this.txtDose2;
                    break;
                case 3:
                    c = this.txtDose3;
                    break;
                case 4:
                    c = this.txtDose4;
                    break;
                case 5:
                    c = this.txtDose5;
                    break;
            }
            c.Text = dose;
           if(IsDoseCanModified)
               c.Enabled = true;
        }
        protected string dose = "";

        /// <summary>
        /// ����
        /// </summary>
        public string Dose
        {
            get
            {
                return dose;
            }
            set
            {
                dose = value;

                if (dose.IndexOf("-") >= 0)
                {
                    string[] doses = dose.Split('-');
                    for (int i = 0; i < doses.Length; i++)
                    {
                        this.SetDose(i+1, doses[i]);
                    }
                }
                else
                {
                    for (int i = 0; i < frequency.Times.Length; i++)
                    {
                        this.SetDose(i+1, dose);
                    }
                    
                }
            }
        }
        public bool IsDoseCanModified = true;
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 5; i++)
            {
                Control c = null;
                switch (i)
                {
                    case 1:
                        c = this.txtDose1;
                        break;
                    case 2:
                        c = this.txtDose2;
                        break;
                    case 3:
                        c = this.txtDose3;
                        break;
                    case 4:
                        c = this.txtDose4;
                        break;
                    case 5:
                        c = this.txtDose5;
                        break;
                }
                if (c.Enabled)
                {
                    if (c.Text == "0.00" || c.Text =="0")
                    {
                        c.Focus();
                        MessageBox.Show("������������");
                        return;
                    }
                }
            }

            this.DialogResult = DialogResult.OK;
            MessageBox.Show("���óɹ���");
            this.Close();
        }


    }
}