using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Neusoft.HISFC.Components.Common.Controls
{
    /// <summary>
    /// [��������: סԺ���߻�����Ϣ]<br></br>
    /// [�� �� ��: ţ��Ԫ]<br></br>
    /// [����ʱ��: 2008-08]<br></br>
    /// <�޸ļ�¼>
    ///     
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucInpatientInfo : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucInpatientInfo()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// סԺ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo patientInfoObj = null;

        #endregion

        #region ����
        /// <summary>
        /// סԺ������Ϣ
        /// </summary>
        public Neusoft.HISFC.Models.RADT.PatientInfo PatientInfoObj
        {
            set
            {
                if (value != null)
                {
                    this.patientInfoObj = value;
                    this.SetValue();
                }
            }
        }

        #endregion

        #region ���з���

        /// <summary>
        /// ���渳ֵ
        /// </summary>
        protected virtual void SetValue()
        {
            //����
            this.txtName.Text = this.patientInfoObj.Name;

            //��ͬ��λ
            this.txtPact.Text = this.patientInfoObj.Pact.Name;

            //סԺ����
            this.txtDept.Text = this.patientInfoObj.PVisit.PatientLocation.Dept.Name;

            //��������
            this.txtNurseStation.Text = this.patientInfoObj.PVisit.PatientLocation.NurseCell.Name;

            //��Ժ����
            this.txtDateIn.Text = this.patientInfoObj.PVisit.InTime.ToShortDateString();

            //סԺҽ��
            this.txtDoctor.Text = this.patientInfoObj.PVisit.AdmittingDoctor.Name;

            //����
            this.txtBedNo.Text = this.patientInfoObj.PVisit.PatientLocation.Bed.ID;

            //��������
            this.txtBirthday.Text = this.patientInfoObj.Birthday.ToShortDateString();

            //�����ܶ�
            this.txtTotCost.Text = this.patientInfoObj.FT.TotCost.ToString();

            //�Էѽ��
            this.txtOwnCost.Text = this.patientInfoObj.FT.OwnCost.ToString();

            //�Ը����
            this.txtPayCost.Text = this.patientInfoObj.FT.PayCost.ToString();

            //���ѽ��
            this.txtPubCost.Text = this.patientInfoObj.FT.PubCost.ToString();

            //Ԥ�����
            this.txtPrepayCost.Text = this.patientInfoObj.FT.PrepayCost.ToString();

            //���
            this.txtFreeCost.Text = this.patientInfoObj.FT.LeftCost.ToString();

        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual void Clear()
        {
            foreach (Control control in this.Controls)
            {
                if (control.GetType() == typeof(Neusoft.FrameWork.WinForms.Controls.NeuTextBox))
                {
                    ((Neusoft.FrameWork.WinForms.Controls.NeuTextBox)control).Text = "";
                }
            }
        }
        #endregion
    }
}
