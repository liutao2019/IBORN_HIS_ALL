using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.FrameWork.Management;
namespace FS.HISFC.Components.InpatientFee.Maintenance
{
    /// <summary>
    /// [��������: ��Ժ�Ǽ����]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPatientOut : System.Windows.Forms.UserControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer//{2A467990-BDA3-4cb4-BB89-5801796EBC95} FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientOut()
        {
            InitializeComponent();
        }

        private void ucPatientOut_Load(object sender, EventArgs e)
        {
            this.InitControl();
        }

        #region ����
        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();

        FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.Fee.InPatient inpatient = new FS.HISFC.BizLogic.Fee.InPatient();
        //FS.HISFC.BizLogic.RADT.InPatient inpatient = new FS.HISFC.BizLogic.RADT.InPatient();
        FS.HISFC.Models.RADT.PatientInfo PatientInfo = null;

         /// <summary>
        /// ����������{28C63B3A-9C64-4010-891D-46F846EA093D}
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();
  

        /// <summary>
        /// adt�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IHE.IADT adt = null;
        #endregion

        #region ����
        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControl()
        {
            try
            {
                this.cmbZg.AddItems(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.ZG));
                //��Ժ�Ǽǵ�ʱ��Ĭ��Ϊϵͳʱ��
                this.dtOutDate.Value = this.inpatient.GetDateTimeFromSysDateTime();
                this.FindForm().Text = "��Ժ�Ǽ�";
                this.ActiveControl = this.txtPatientNo;
                txtPatientNo.myEvent+=new FS.HISFC.Components.Common.Controls.myEventDelegate(txtPatientNo_myEvent);
            }
            catch { }

        }


        /// <summary>
        /// ���û�����Ϣ���ؼ�
        /// </summary>
        /// <param name="PatientInfo"></param>
        protected virtual void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {

            this.txtPatientNo.Text = patientInfo.PID.PatientNO;		//סԺ��
            this.txtCard.Text = patientInfo.PID.CardNO;	//���￨��
            this.txtPatientNo.Tag = patientInfo.ID;				//סԺ��ˮ��
            this.txtName.Text = patientInfo.Name;						//����
            this.txtSex.Text = patientInfo.Sex.Name;					//�Ա�
            this.txtIndate.Text = patientInfo.PVisit.InTime.ToString();	//��Ժ����
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;	//��������
            this.txtDept.Tag = patientInfo.PVisit.PatientLocation.Dept.ID;	//���ұ���

            FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYKIND));
            this.txtBalKind.Text = helper.GetName(patientInfo.Pact.PayKind.ID);


            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;	//����
            this.txtFreePay.Text = patientInfo.FT.LeftCost.ToString();		//ʣ��Ԥ����
            this.txtTotcost.Text = patientInfo.FT.TotCost.ToString();		//�ܽ��
            this.cmbZg.Tag = patientInfo.PVisit.ZG.ID;						//ת��
            //{DDE7D11F-61CF-4e09-BC2D-F49B03446261}
            //�����patientInfo.PVisit.OutTime��0002-1-1�������������Ƿ���ԣ�
            if (patientInfo.PVisit.OutTime < this.dtOutDate.MinDate || patientInfo.PVisit.OutTime > this.dtOutDate.MaxDate)
            {
                this.dtOutDate.Value = this.inpatient.GetDateTimeFromSysDateTime();
            }
            else
            {
                this.dtOutDate.Value = patientInfo.PVisit.OutTime;				//��Ժ����
            }
            // this.dtOutDate.Value = patientInfo.PVisit.OutTime;				//��Ժ����
            //{DDE7D11F-61CF-4e09-BC2D-F49B03446261}


            //��Ժ�Ǽ��޸�ʱ�䴦�� {28C63B3A-9C64-4010-891D-46F846EA093D}

            string rtn = this.ctlMgr.QueryControlerInfo("ZY0002");
            if (rtn == null || rtn == "-1" || rtn == "")
            {
                rtn = "0";
            }
            else
            {
                rtn = "1";
            }

            if (rtn == "1")//
            {
               System.Collections.ArrayList alShiftDataInfo = this.radt.QueryPatientShiftInfoNew(this.PatientInfo.ID);

                if (alShiftDataInfo == null)
                {
                    MessageBox.Show("��ȡ������¼��Ϣ����");
                    return;
                }

                bool isExitInfo = false;

                foreach (FS.HISFC.Models.Invalid.CShiftData myCShiftDate in alShiftDataInfo)
                {
                    if (myCShiftDate.ShitType == "BB") //�н����ٻ�
                    {
                        this.dtOutDate.Enabled = true;
                        isExitInfo = true;
                        break;


                    }
                }


                this.dtOutDate.Enabled = isExitInfo;


            }
            else
            {
                this.dtOutDate.Enabled = false;
            }


            this.dtOutDate.Focus();
        }


        /// <summary>
        /// �ӿؼ���ó�Ժ�Ǽ���Ϣ
        /// </summary>
        protected virtual void GetOutInfo()
        {
            PatientInfo.PVisit.ZG.ID = this.cmbZg.Tag.ToString();
            PatientInfo.PVisit.ZG.Name = this.cmbZg.Text;
            PatientInfo.PVisit.PreOutTime = this.dtOutDate.Value;
        }

        /// <summary>
        /// ����
        /// </summary>
        protected virtual void Clear()
        {
            foreach (Control c in this.neuGroupBox1.Controls)
            { 
                if(c.GetType()==typeof(FS.FrameWork.WinForms.Controls.NeuTextBox))
                {
                    c.Text="";
                }
            }
            this.cmbZg.Text = "";
            this.cmbZg.Tag = "";
            this.dtOutDate.Value = this.inpatient.GetDateTimeFromSysDateTime();
            this.txtPatientNo.Text = "";
            this.txtPatientNo.Focus();
        }
           
        string Err = "";
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            if (cmbZg.Tag == null || cmbZg.Tag.ToString().Trim() == string.Empty)
            {
                this.Err = "�������Ժ���";
                this.cmbZg.Focus();
                return -1;
            }
            //������߲��ǵ����Ժ��ʾ
            if (this.dtOutDate.Value.Date != this.inpatient.GetDateTimeFromSysDateTime().Date)
            {
                DialogResult dr = MessageBox.Show("�û��ߵĳ�Ժ�����ǣ� " +
                    this.dtOutDate.Value.ToString("yyyy��MM��dd��") + "  �Ƿ������", "��ʾ",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                    MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.No)
                {
                    this.Err = "";
                    return -1;
                }
            }

            if (this.PatientInfo == null || string.IsNullOrEmpty(this.PatientInfo.ID))
            {
                this.Err = "��س�ȷ��סԺ���Ƿ����!";

                return -1;
            }

            //ȡ�������µ�סԺ������Ϣ
            PatientInfo = this.radt.GetPatientInfomation(this.PatientInfo.ID);
            if (PatientInfo == null)
            {
                this.Err = this.radt.Err;
                return -1;
            }
            this.Err = "";

            //���������Ժ״̬�����仯,���������

            string in_State = PatientInfo.PVisit.InState.ID.ToString();
            if (PatientInfo.PVisit.InState.ID.ToString() != "I")
            {
                this.Err = "�û���û�н���";
                return -1;
            }

            //ȡ��Ժ�Ǽ���Ϣ
            this.GetOutInfo();

            //{2A467990-BDA3-4cb4-BB89-5801796EBC95}
            FS.HISFC.BizProcess.Interface.IPatientShiftValid obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid)) as FS.HISFC.BizProcess.Interface.IPatientShiftValid;
            if (obj != null)
            {
                string err = string.Empty;
                bool bl = obj.IsPatientShiftValid(PatientInfo, FS.HISFC.Models.Base.EnumPatientShiftValid.O, ref err);
                if (!bl)
                {
                    MessageBox.Show(err);
                    return -1;
                }
            }
            //��Ժ�Ǽ�
            HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();
            long returnValue = medcareInterfaceProxy.SetPactCode(this.PatientInfo.Pact.ID);
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            radt.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            medcareInterfaceProxy.BeginTranscation();

            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڻ�ú�ͬ��λʧ��" + medcareInterfaceProxy.ErrMsg));
                return -1;
            }

            returnValue = medcareInterfaceProxy.Connect();
            {
                if (returnValue != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڻ�ú�ͬ��λʧ��" + medcareInterfaceProxy.ErrMsg));
                    return -1;
                }
            }
            medcareInterfaceProxy.BeginTranscation();
            //returnValue = medcareInterfaceProxy.GetRegInfoInpatient(this.PatientInfo);
            //if (returnValue != 1)
            //{
            //    t.RollBack();
            //    medcareInterfaceProxy.Rollback();
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڻ�û�����Ϣʧ��" + medcareInterfaceProxy.ErrMsg));
            //    return -1;
            //}
            //��Ժ�Ǽ�(����)
            returnValue = medcareInterfaceProxy.LogoutInpatient(this.PatientInfo);
            if (returnValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڻ��Ժ�Ǽ�ʧ��" + medcareInterfaceProxy.ErrMsg));
                return -1;
            }
            int i = radt.OutPatient(PatientInfo);
            this.Err = radt.Err;
            if (i == -1)��//ʧ��
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                return -1;
            }
            else if (i == 0)//ȡ��
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                this.Err = "";
                return 0;
            }
            //zhangjunyi ע�͵� ��Ϊcommit
            //medcareInterfaceProxy.Rollback(); 
            if (medcareInterfaceProxy.Commit() < 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                medcareInterfaceProxy.Rollback();
                this.Err = "ҽ���ӿ��ύ���������������������Ƿ���ȷ";
                return -1;
            }

            #region addby xuewj 2010-3-15

            if (this.adt == null)
            {
                this.adt = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IHE.IADT)) as FS.HISFC.BizProcess.Interface.IHE.IADT;
            }
            if (this.adt != null)
            {
                this.adt.DischargeInpatient(this.PatientInfo);
            }

            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();



            return 1;
        }
        #endregion

        #region �¼�
        private void button1_Click(object sender, System.EventArgs e)
        {
            ((Control)sender).Enabled = false;
            if (this.Save() > 0)//�ɹ�
            {
                MessageBox.Show(Err);
                Clear();
                ((Control)sender).Enabled = true;
                return;
            }
            else
            {
                if (Err != "") MessageBox.Show(Err);
            }
            ((Control)sender).Enabled = true;

        }

        private void txtPatientNo_myEvent()
        {
            if (this.txtPatientNo.InpatientNo == null || this.txtPatientNo.InpatientNo.Trim() == string.Empty)
            {
                MessageBox.Show(Language.Msg("û�и�סԺ��,����֤������") + this.txtPatientNo.Err);
                this.txtPatientNo.Focus();
                this.txtPatientNo.TextBox.SelectAll();
                return;
            }
            PatientInfo = this.radt.GetPatientInfomation(this.txtPatientNo.InpatientNo);
            if (PatientInfo == null)
            {
                MessageBox.Show(Language.Msg("��û��߻�����Ϣ����!") + this.txtPatientNo.Err);
                this.txtPatientNo.Focus();
                this.txtPatientNo.TextBox.SelectAll();
                return;
            }
            this.SetPatientInfo(PatientInfo);
            
        }

        private void dtOutDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                this.cmbZg.Focus();
            }
        }

        private void cmbZg_KeyDown(object sender, KeyEventArgs e)
        {
            this.btnSave.Focus();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #endregion



        #region IInterfaceContainer {2A467990-BDA3-4cb4-BB89-5801796EBC95} ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid) };
            }
        }

        #endregion
    }
}
