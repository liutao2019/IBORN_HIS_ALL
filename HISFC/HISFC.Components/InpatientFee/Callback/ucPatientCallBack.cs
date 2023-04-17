using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.InpatientFee.Callback
{
    /// <summary>
    /// ���ת�룬�ٻصȻ����ؼ�
    /// </summary>
    public partial class ucPatientCallBack : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientCallBack()
        {
            InitializeComponent();
        }
        protected FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;
        public ArriveType arrivetype = ArriveType.CallBack;
        FS.HISFC.Models.Base.Employee empl;
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //***************��ò����б�*************
            empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            NurseCell  = empl.Nurse.Clone();
            //���ݻ�ʿվ�õ�������Ϣ
            ArrayList alDepts = manager.QueryDepartment(empl.Nurse.ID);
            try
            {
                //ȡҽ���б�
                ArrayList al = new ArrayList();
                
                foreach (FS.FrameWork.Models.NeuObject dept in alDepts)
                {
                    al.AddRange(manager.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, dept.ID));
                
                }

                //����סԺҽ���б�
                this.cmbDoc.AddItems(al);
                //��������ҽ���б�
                this.cmbAttendingDoc.AddItems(al);
                //��������ҽ���б�
                this.cmbConsultingDoc.AddItems(al);
                //�������λ�ʿ�б�
                this.cmbAdmittingNur.AddItems(manager.QueryNurse(empl.Nurse.ID));


                //���ش�λ�б�
                if (this.arrivetype == ArriveType.ChangeDoc)
                {
                    //��ҽ��ʱ,��ʾȫ����λ
                    this.cmbBedNo.AddItems(manager.QueryBedList(empl.Nurse.ID));
                }
                else
                {
                    //����ʱ,ֻ��ʾ�մ�
                    this.cmbBedNo.AddItems(manager.QueryUnoccupiedBed(empl.Nurse.ID));
                }
                this.cmbDoc.IsListOnly = true;
                this.cmbBedNo.IsListOnly = true;
                this.cmbAdmittingNur.IsListOnly = true;
                this.cmbAttendingDoc.IsListOnly = true;
                this.cmbConsultingDoc.IsListOnly = true;
                this.ucQueryInpatientNo.myEvent += new FS.HISFC.Components.Common.Controls.myEventDelegate(ucQueryInpatientNo_myEvent);//{4DA55EB8-FEE8-4f9a-ACEF-ACB794091D4C}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        //{4DA55EB8-FEE8-4f9a-ACEF-ACB794091D4C}
        private void ucQueryInpatientNo_myEvent()
        {
            this.RefreshList(this.ucQueryInpatientNo.InpatientNo);
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.RefreshList(((FS.HISFC.Models.RADT.PatientInfo)neuObject).ID);
            return base.OnSetValue(neuObject, e);
        }
       
        /// <summary>
        /// ��������Ϣ��ʾ���ؼ���
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo PatientInfo)
        {
            this.txtPatientNo.Text = PatientInfo.PID.PatientNO;
            this.txtPatientNo.Tag = PatientInfo.ID;
            this.txtName.Text = PatientInfo.Name;
            this.txtSex.Text = PatientInfo.Sex.Name;
            this.cmbDoc.Text = PatientInfo.PVisit.AdmittingDoctor.Name;
            this.cmbDoc.Tag = PatientInfo.PVisit.AdmittingDoctor.ID;
            this.cmbAttendingDoc.Text = PatientInfo.PVisit.AttendingDoctor.Name;
            this.cmbAttendingDoc.Tag = PatientInfo.PVisit.AttendingDoctor.ID;
            this.cmbConsultingDoc.Text = PatientInfo.PVisit.ConsultingDoctor.Name;
            this.cmbConsultingDoc.Tag = PatientInfo.PVisit.ConsultingDoctor.ID;
            this.cmbAdmittingNur.Text = PatientInfo.PVisit.AdmittingNurse.Name;
            this.cmbAdmittingNur.Tag = PatientInfo.PVisit.AdmittingNurse.ID;
            if (this.arrivetype == ArriveType.ShiftIn)
            {
                this.cmbBedNo.Text = "";
                this.cmbBedNo.Tag = "";

            }
            else
            {
                this.cmbBedNo.Text = PatientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";
                this.cmbBedNo.Tag = PatientInfo.PVisit.PatientLocation.Bed.ID;
            }

            //��ҽ������Ӥ���ٻز���ѡ��λ,��������ͬ
            if (PatientInfo.IsBaby || this.arrivetype == ArriveType.ChangeDoc)
                this.cmbBedNo.Enabled = false;
            else
                this.cmbBedNo.Enabled = true;

        }


        /// <summary>
        /// ��û��߻�����Ϣ�ӿؼ���PatientInfo
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void GetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            //ȡ�ؼ���סԺҽ��
            if (this.cmbDoc.Text != "")
            {
                patientInfo.PVisit.AdmittingDoctor.ID = this.cmbDoc.Tag.ToString();
                patientInfo.PVisit.AdmittingDoctor.Name = this.cmbDoc.Text;
            }
            else
            {
                patientInfo.PVisit.AdmittingDoctor.ID = "";
                patientInfo.PVisit.AdmittingDoctor.Name = "";
            }


            //ȡ�ؼ�������ҽ��
            if (this.cmbAttendingDoc.Text != "")
            {
                patientInfo.PVisit.AttendingDoctor.ID = this.cmbAttendingDoc.Tag.ToString();
                patientInfo.PVisit.AttendingDoctor.Name = this.cmbAttendingDoc.Text;
            }
            else
            {
                patientInfo.PVisit.AttendingDoctor.ID = "";
                patientInfo.PVisit.AttendingDoctor.Name = "";
            }


            //ȡ�ؼ�������ҽ��
            if (this.cmbConsultingDoc.Text != "")
            {
                patientInfo.PVisit.ConsultingDoctor.ID = this.cmbConsultingDoc.Tag.ToString();
                patientInfo.PVisit.ConsultingDoctor.Name = this.cmbConsultingDoc.Text;
            }
            else
            {
                patientInfo.PVisit.ConsultingDoctor.ID = "";
                patientInfo.PVisit.ConsultingDoctor.Name = "";
            }


            //ȡ�ؼ������λ�ʿ
            if (this.cmbAdmittingNur.Text != "")
            {
                patientInfo.PVisit.AdmittingNurse.ID = this.cmbAdmittingNur.Tag.ToString();
                patientInfo.PVisit.AdmittingNurse.Name = this.cmbAdmittingNur.Text;
            }
            else
            {
                patientInfo.PVisit.AdmittingNurse.ID = "";
                patientInfo.PVisit.AdmittingNurse.Name = "";
            }

      

            //����סԺ״̬Ϊ��Ժ�Ǽ�
            patientInfo.PVisit.InState.ID = "I";
        }


        /// <summary>
        /// ����
        /// </summary>
        public virtual void ClearPatintInfo()
        {
            this.cmbDoc.Text = "";
            this.cmbDoc.Tag = "";
            this.cmbAttendingDoc.Text = "";
            this.cmbAttendingDoc.Tag = "";
            this.cmbConsultingDoc.Text = "";
            this.cmbConsultingDoc.Tag = "";
            this.cmbAdmittingNur.Text = "";
            this.cmbAdmittingNur.Tag = "";
            
        }

        FS.HISFC.BizProcess.Integrate.RADT Inpatient = new FS.HISFC.BizProcess.Integrate.RADT();
        protected string strNurseCode = "";
        /// <summary>
        /// ˢ�»�����Ϣ
        /// </summary>
        /// <param name="inPatientNo"></param>
        public virtual void RefreshList(string inPatientNo)
        {
            //���ش�λ�б�
            if (this.arrivetype == ArriveType.ChangeDoc)
            {
                //��ҽ��ʱ,��ʾȫ����λ
                this.cmbBedNo.AddItems(manager.QueryBedList(empl.Nurse.ID));
            }
            else
            {
                //����ʱ,ֻ��ʾ�մ�
                this.cmbBedNo.AddItems(manager.QueryUnoccupiedBed(empl.Nurse.ID));
            }
            ClearPatintInfo();
            try
            {
                this.patientInfo = this.Inpatient.GetPatientInfomation(inPatientNo);
                if (this.patientInfo == null)
                {
                    MessageBox.Show(this.Inpatient.Err);
                    this.patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                }
            }
            catch { }
            
            try
            {
                this.SetPatientInfo(this.patientInfo);
            }
            catch { }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public virtual int Save()
        {
            //ȡӤ������ʱ����Ϣ��Ϣ
            FS.HISFC.Models.RADT.PatientInfo PatientInfo = null;

            //ȡ�������µ�סԺ������Ϣ
            PatientInfo = this.Inpatient.QueryPatientInfoByInpatientNO(this.patientInfo.ID);
            if (this.patientInfo == null)
            {
               MessageBox.Show( this.Inpatient.Err);
                return -1;
            }

            //��ҽ��ʱ,��������Ѳ��ڱ���,���������---������ת�ƺ�,���������û�йر�,����ִ������
            if (PatientInfo.PVisit.PatientLocation.NurseCell.ID != this.NurseCell.ID 
                && this.arrivetype == ArriveType.ChangeDoc)
            {
                MessageBox.Show( "�����Ѳ��ڱ�����,��ˢ�µ�ǰ����");
                return -1;
            }

            //��������Ѳ�����Ժ״̬,���������
            if (PatientInfo.PVisit.InState.ID.ToString() != this.patientInfo.PVisit.InState.ID.ToString())
            {
                MessageBox.Show(  "������Ϣ�ѷ����仯,��ˢ�µ�ǰ����");
                return -1;
            }

            //ȡ�䶯��Ϣ:ȡҽ������ʿ�����ҵ���Ϣ
            this.GetPatientInfo(PatientInfo);

            //�ж��Ƿ���ѡ��λ
            if (this.cmbBedNo.Text.Trim() == ""
                && patientInfo.IsBaby == false)
            {
               MessageBox.Show( "δѡ��λ��");
                return -1;
            }

            //ȡ����ʱ�Ĵ�λ��Ϣ
            FS.HISFC.Models.Base.Bed bed = new FS.HISFC.Models.Base.Bed();
            bed.ID = this.cmbBedNo.Tag.ToString();	//����
            bed.InpatientNO = patientInfo.ID;		//��λ�ϻ���סԺ��ˮ��


            #region ҵ����
            FS.HISFC.BizProcess.Integrate.RADT managerRADT = new FS.HISFC.BizProcess.Integrate.RADT();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            managerRADT.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            
            //ת��
            if (this.arrivetype == ArriveType.ShiftIn)
            {

                if (managerRADT.ShiftIn(PatientInfo, this.NurseCell, this.cmbBedNo.Tag.ToString()) == -1)//����ת��ҵ��
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
                  
                   
                }
            }

            //���令��վΪ��ǰ����վ(ת�����ʱ,��Ҫ����ԭ����վ��Ϣ,�����ڴ˴�����)
            PatientInfo.PVisit.PatientLocation.NurseCell = this.NurseCell;
            PatientInfo.PVisit.PatientLocation.Bed = bed;

            //����
            if (this.arrivetype == ArriveType.Regedit)
            {
                if (managerRADT.ArrivePatient(PatientInfo, bed) == -1)//���ý���ҵ��
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
                  
                   
                }
            }

            //�ٻ�
            if (this.arrivetype == ArriveType.CallBack)
            {
                if (managerRADT.CallBack(PatientInfo, bed) == -1)//�����ٻ�ҵ��
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
                   
                    
                }
            }

            //��ҽʦ
            if (this.arrivetype == ArriveType.ChangeDoc)
            {
                if (managerRADT.ChangeDoc(PatientInfo) == -1)//���û�ҽ��ҵ��
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
            
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(managerRADT.Err);
            base.OnRefreshTree();//ˢ����
            return 1;

        }
        

    


        private void cmbBedNo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.cmbBedNo.Tag == null || this.cmbBedNo.Tag.ToString() == "") return;
            if (this.arrivetype == ArriveType.Regedit)
            {
              
                FS.HISFC.Models.Base.Bed obj = manager.GetBed(this.cmbBedNo.Tag.ToString());
                if (obj == null) return;
                this.cmbDoc.Tag = obj.Doctor.ID;					//סԺҽ��
                this.cmbAttendingDoc.Tag = obj.AttendingDoctor.ID;	//����ҽ��
                this.cmbConsultingDoc.Tag = obj.ConsultingDoctor.ID;//����ҽ��
                this.cmbAdmittingNur.Tag = obj.AdmittingNurse.ID;	//���λ�ʿ
            }
        }

        
        /// <summary>
        /// ��ʿվ
        /// </summary>
        protected FS.FrameWork.Models.NeuObject NurseCell = null;

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void txtPatientNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData.GetHashCode() == Keys.Enter.GetHashCode())
            {
                this.ucQueryInpatientNo.txtInputCode.Text = this.txtPatientNo.Text;
                this.ucQueryInpatientNo.query();
                this.RefreshList(this.ucQueryInpatientNo.InpatientNo);
            }
        }
        

        
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum ArriveType
    {
        /// <summary>
        /// �Ǽ�
        /// </summary>
        Regedit,
        /// <summary>
        /// ת��
        /// </summary>
        ShiftIn,
        /// <summary>
        /// �ٻ�
        /// </summary>
        CallBack,
        /// <summary>
        /// ����ҽʦ����Ϣ
        /// </summary>
        ChangeDoc
    }
}
