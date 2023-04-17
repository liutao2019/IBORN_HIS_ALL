using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Components.Operation
{
    /// <summary>
    /// [��������: ���߷��ò�ѯ]<br></br>
    /// [�� �� ��: ����ȫ]<br></br>
    /// [����ʱ��: 2007-01-08]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucQueryFee : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucQueryFee()
        {
            InitializeComponent();
            
        }

#region ����


        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        /// <param name="patient"></param>
        private void DispPatientinfo(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            this.neuLabel3.Text = patient.Name + "[" + patient.PID.PatientNO + ", " + patient.Sex.Name + ",  " + patient.PVisit.PatientLocation.Dept.Name + " ]";
        }
#endregion

        private void ucQueryInpatientNo1_myEvent()
        {
            PatientInfo patientInfo = Environment.RadtManager.GetPatientInfomation(this.ucQueryInpatientNo1.InpatientNo);

            if (patientInfo == null)
            {
                MessageBox.Show("û�в鵽�û�����Ϣ");
                return;
            }
                //��ʾ��һ�����߷�����ϸ
            this.ucQueryFeeDrug1.AddItems(patientInfo);
            this.ucQueryFeeUndrug1.AddItems(patientInfo);

            this.DispPatientinfo(patientInfo);
            
            
        }

        protected override int OnPrint(object sender, object neuObject)
        {

			if(this.neuTabControl1.SelectedIndex == 0)
			{
                this.ucQueryFeeDrug1.Print();
			}
            else if (this.neuTabControl1.SelectedIndex == 1)
			{
                this.ucQueryFeeUndrug1.Print();
			}
		
            return base.OnPrint(sender, neuObject);
        }
    }
}