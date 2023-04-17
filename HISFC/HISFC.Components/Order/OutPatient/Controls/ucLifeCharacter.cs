using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Order.OutPatient.Controls
{
    public partial class ucLifeCharacter : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucLifeCharacter()
        {
            InitializeComponent();
        }

        #region ����
        /// <summary>
        /// ����ʵ��
        /// </summary>
        protected FS.HISFC.Models.RADT.LifeCharacter lifeCharacter = new FS.HISFC.Models.RADT.LifeCharacter();

        /// <summary>
        /// ����Ա
        /// </summary>
        protected FS.HISFC.Models.Base.Employee oper = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

        /// <summary>
        /// �Һ���Ϣʵ��
        /// </summary>
        protected FS.HISFC.Models.Registration.Register patientInfo = new FS.HISFC.Models.Registration.Register();
                
        #endregion

        
        #region ����

        /// <summary>
        /// ���߹Һ���Ϣ
        /// </summary>
        public FS.HISFC.Models.Registration.Register PatientInfo
        {
            set 
            {
                this.patientInfo = value;
            }
        }

        #endregion

        #region ����

        private void Init()
        {
            this.SetPatient();
        }

        /// <summary>
        /// ����
        /// </summary>
        private void Clear()
        {
            this.patientInfo = new FS.HISFC.Models.Registration.Register();
            this.lblName.Text = FS.FrameWork.Management.Language.Msg("������ ");
            this.lblSex.Text = FS.FrameWork.Management.Language.Msg("�Ա� ");
            this.lblAge.Text = FS.FrameWork.Management.Language.Msg("���䣺 ");
            this.lblRegDate.Text = FS.FrameWork.Management.Language.Msg("�Һ�ʱ�䣺 ");
            this.lblRegDept.Text = FS.FrameWork.Management.Language.Msg("�Һſ��ң� ");
            this.lblRegDoct.Text = FS.FrameWork.Management.Language.Msg("�Һ�ҽ���� ");
            this.lblRegLV.Text = FS.FrameWork.Management.Language.Msg("�Һż��� ");
        }

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        private void SetPatient()
        {
            if (this.patientInfo == null)
            {
                return;
            }
            this.lblName.Text = FS.FrameWork.Management.Language.Msg("������ ") + this.patientInfo.Name;
            this.lblSex.Text = FS.FrameWork.Management.Language.Msg("�Ա� ") + this.patientInfo.Sex.Name;
            this.lblAge.Text = FS.FrameWork.Management.Language.Msg("���䣺 ") + CacheManager.OutOrderMgr.GetAge(this.patientInfo.Birthday);
            this.lblRegDate.Text = FS.FrameWork.Management.Language.Msg("�Һ�ʱ�䣺 ") + this.patientInfo.DoctorInfo.SeeDate.ToShortDateString();
            this.lblRegDept.Text = FS.FrameWork.Management.Language.Msg("�Һſ��ң� ") + this.patientInfo.DoctorInfo.Templet.Dept.Name;
            this.lblRegDoct.Text = FS.FrameWork.Management.Language.Msg("�Һ�ҽ���� ") + this.patientInfo.DoctorInfo.Templet.Doct.Name;
            this.lblRegLV.Text = FS.FrameWork.Management.Language.Msg("�Һż��� ") + this.patientInfo.DoctorInfo.Templet.RegLevel.Name;
        }

        /// <summary>
        /// ��������ʵ��
        /// </summary>
        /// <returns></returns>
        private FS.HISFC.Models.RADT.LifeCharacter SetLifeCharacter()
        {
            //����ʵ��
            FS.HISFC.Models.RADT.LifeCharacter myLifeCharacter = new FS.HISFC.Models.RADT.LifeCharacter();
            try
            {
                myLifeCharacter.ID = this.patientInfo.ID;
                myLifeCharacter.Name = this.patientInfo.Name;
                myLifeCharacter.Dept.ID = oper.Dept.ID;
                myLifeCharacter.Dept.Name = oper.Dept.Name;
                myLifeCharacter.NurseStation.ID = oper.Nurse.ID;
                myLifeCharacter.NurseStation.Name = oper.Nurse.Name;
                myLifeCharacter.BedNO = "";
                myLifeCharacter.PID.PatientNO = this.patientInfo.PID.CardNO;
                //myLifeCharacter.InDate = DateTime.Now;
                myLifeCharacter.MeasureDate = CacheManager.OutOrderMgr.GetDateTimeFromSysDateTime();
                myLifeCharacter.Breath = FS.FrameWork.Function.NConvert.ToInt32(this.txtBreath.Text);
                myLifeCharacter.Temperature = FS.FrameWork.Function.NConvert.ToDecimal(this.txtTemperature.Text);
                myLifeCharacter.Pulse = FS.FrameWork.Function.NConvert.ToInt32(this.txtPulse.Text);
                myLifeCharacter.HighBloodPressure = FS.FrameWork.Function.NConvert.ToInt32(this.txtHighBP.Text);
                myLifeCharacter.LowBloodPressure = FS.FrameWork.Function.NConvert.ToInt32(this.txtLowBP.Text);
                myLifeCharacter.Time = 0;
                myLifeCharacter.IsForceHypothermia = false;
                myLifeCharacter.TargetTemperature = 0;
                myLifeCharacter.TemperatureType = "Ҹ��";
                myLifeCharacter.Memo = this.txtRemark.Text.ToString().Trim();
            }
            catch(Exception ex)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("������Ϣʵ�帽ֵʧ�ܣ�") + ex.Message);
                return null;
            }
            return myLifeCharacter;
        }

        /// <summary>
        /// У��������Ч��
        /// </summary>
        /// <param name="lfch"></param>
        /// <returns></returns>
        private int CheckData(FS.HISFC.Models.RADT.LifeCharacter lfch)
        {
            if (lfch.ID == "" || lfch.ID == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Ƿ��Ļ�����Ϣ��"));
                return -1;
            }
            if (!FS.FrameWork.Public.String.ValidMaxLengh(this.txtRemark.Text.ToString().Trim(),80))
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ע������"));
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        private int SaveData()
        {
            FS.HISFC.Models.RADT.LifeCharacter lfch = new FS.HISFC.Models.RADT.LifeCharacter();
            lfch = this.SetLifeCharacter();
            if (this.CheckData(lfch) < 0)
            {
                return -1;
            }
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(CacheManager.OrderMgr.Connection);
            //t.BeginTransaction();
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            CacheManager.RadtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans); //��������
            int iReturn = CacheManager.RadtIntegrate.InsertLifeCharacter(lfch);
            if (iReturn < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();;
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ʧ�ܣ�") + CacheManager.RadtIntegrate.Err);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("����ɹ���"));
            return 0;
        }

        #endregion

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.SaveData();            
        }

        private void ucLifeCharacter_Load(object sender, EventArgs e)
        {
            this.Init();
            //this.txtClinicInput1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClinicInput1_KeyUp);
            //this.txtClinicInput1.KeyDown += new KeyEventHandler(txtClinicInput1_KeyDown);
            //this.txtClinicInput1.selectedEvents += new FS.HISFC.Components.Common.Controls.selectedEventDelegate(txtClinicInput1_selectedEvents);
        }

        //private void txtClinicInput1_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.KeyCode == Keys.OemQuestion)
        //    {
        //        if (this.txtClinicInput1.Text.StartsWith("/"))
        //            this.txtClinicInput1.IsExecQuery = false;
        //    }
        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (this.txtClinicInput1.Text.StartsWith("/"))
        //            this.txtClinicInput1.IsExecQuery = false;
        //        else
        //            this.txtClinicInput1.IsExecQuery = true;
        //    }
            
        //}

        //private void txtClinicInput1_KeyDown(object sender, KeyEventArgs e)
        //{

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (this.txtClinicInput1.Text.StartsWith("/"))
        //        {
        //            DialogResult r = MessageBox.Show("�Ƿ�Ϊ�û��߲���Һ���Ϣ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

        //            if (r == DialogResult.Yes)
        //            {
        //                string name = this.txtClinicInput1.Text.Remove(0, 1);
        //                FS.HISFC.Components.Common.Forms.frmRegistrationByDoctor frmDoctRegistration = new FS.HISFC.Components.Common.Forms.frmRegistrationByDoctor(name);
        //                frmDoctRegistration.ShowDialog();
        //                this.txtClinicInput1.Text = frmDoctRegistration.PatientInfo.PID.CardNO;
        //                //this.txtClinicInput1.IsExecQuery = true;
        //            }
        //            else
        //            {
        //                this.txtClinicInput1.Text = "";
                        
        //            }
        //        }
        //    }
        //}

        
    }
}

