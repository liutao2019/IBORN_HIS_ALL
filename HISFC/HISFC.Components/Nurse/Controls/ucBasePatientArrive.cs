using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace FS.HISFC.Components.Nurse.Controls
{
    /// <summary>
    /// ���ת�룬�ٻصȻ����ؼ�
    /// </summary>
    public partial class ucBasePatientArrive : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucBasePatientArrive()
        {
            InitializeComponent();
        }
        protected FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        protected FS.HISFC.Models.Registration.Register register = null;
        public ArriveType arrivetype;
        FS.HISFC.Models.Base.Employee empl;
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            //***************��ò����б�*************
            empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;

            NurseCell  = empl.Dept.Clone();
            //���ݻ�ʿվ�õ�������Ϣ
            //ArrayList alDepts = manager.QueryDepartment(empl.Nurse.ID);
            ArrayList alDepts = manager.QueryDepartment(NurseCell.ID);
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
                //���ؿ������б�
                this.cmbDirector.AddItems(al);




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
               this.cmbDirector.IsListOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.RefreshList(((FS.HISFC.Models.Registration.Register)e.Tag).ID);
            return base.OnSetValue(neuObject, e);
        }
       
        /// <summary>
        /// ��������Ϣ��ʾ���ؼ���
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void SetPatientInfo(FS.HISFC.Models.Registration.Register PatientInfo)
        {
            this.txtPatientNo.Text = PatientInfo.PID.CardNO;
            this.txtClinicNO.Text = PatientInfo.ID;
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
            this.cmbDirector.Text = PatientInfo.PVisit.AttendingDirector.Name;
           this.cmbDirector.Tag = PatientInfo.PVisit.AttendingDirector.ID;

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
        private void GetPatientInfo(FS.HISFC.Models.Registration.Register register)
        {
            //ȡ�ؼ���סԺҽ��
            if (this.cmbDoc.Text != "")
            {
                register.PVisit.AdmittingDoctor.ID = this.cmbDoc.Tag.ToString();
                register.PVisit.AdmittingDoctor.Name = this.cmbDoc.Text;
            }
            else
            {
                register.PVisit.AdmittingDoctor.ID = "";
                register.PVisit.AdmittingDoctor.Name = "";
            }


            //ȡ�ؼ�������ҽ��
            if (this.cmbAttendingDoc.Text != "")
            {
                register.PVisit.AttendingDoctor.ID = this.cmbAttendingDoc.Tag.ToString();
                register.PVisit.AttendingDoctor.Name = this.cmbAttendingDoc.Text;
            }
            else
            {
                register.PVisit.AttendingDoctor.ID = "";
                register.PVisit.AttendingDoctor.Name = "";
            }


            //ȡ�ؼ�������ҽ��
            if (this.cmbConsultingDoc.Text != "")
            {
                register.PVisit.ConsultingDoctor.ID = this.cmbConsultingDoc.Tag.ToString();
                register.PVisit.ConsultingDoctor.Name = this.cmbConsultingDoc.Text;
            }
            else
            {
                register.PVisit.ConsultingDoctor.ID = "";
                register.PVisit.ConsultingDoctor.Name = "";
            }


            //ȡ�ؼ������λ�ʿ
            if (this.cmbAdmittingNur.Text != "")
            {
                register.PVisit.AdmittingNurse.ID = this.cmbAdmittingNur.Tag.ToString();
                register.PVisit.AdmittingNurse.Name = this.cmbAdmittingNur.Text;
            }
            else
            {
                register.PVisit.AdmittingNurse.ID = "";
                register.PVisit.AdmittingNurse.Name = "";
            }


            //ȡ�ؼ��п�����
            if (this.cmbDirector.Text != "")
            {
                register.PVisit.AttendingDirector.ID = this.cmbDirector.Tag.ToString();
                register.PVisit.AttendingDirector.Name = this.cmbDirector.Text;
            }
            else
            {
                register.PVisit.AttendingDirector.ID = "";
                register.PVisit.AttendingDirector.Name = "";
            }

      

            //����סԺ״̬Ϊ��Ժ�Ǽ�
            register.PVisit.InState.ID = "I";
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
            this.cmbDirector.Text = "";
            this.cmbDirector.Tag = "";
            
        }

        FS.HISFC.BizProcess.Integrate.RADT outpatient = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizProcess.Integrate.Registration.Registration regManager = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
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
                this.cmbBedNo.AddItems(manager.QueryBedList(empl.Dept.ID));
            }
            else
            {
                //����ʱ,ֻ��ʾ�մ�
                this.cmbBedNo.AddItems(manager.QueryUnoccupiedBed(empl.Dept.ID));
            }
            ClearPatintInfo();
            try
            {
                this.register = this.regManager.GetByClinic(inPatientNo);
                if (this.register == null)
                {
                    MessageBox.Show(this.regManager.Err);
                    this.register = new FS.HISFC.Models.Registration.Register();
                }
            }
            catch { }
            
            try
            {
                this.SetPatientInfo(this.register);
            }
            catch { }
        }


        /// <summary>
        /// ��������
        /// </summary>
        public virtual int Save()
        {
            //ȡӤ������ʱ����Ϣ��Ϣ
            FS.HISFC.Models.Registration.Register PatientInfo = null;

            //ȡ�������µ�סԺ������Ϣ
            PatientInfo = this.regManager.GetByClinic(this.register.ID);
            if (this.register == null)
            {
                MessageBox.Show(this.outpatient.Err);
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
            if (PatientInfo.PVisit.InState.ID.ToString() != this.register.PVisit.InState.ID.ToString())
            {
                MessageBox.Show(  "������Ϣ�ѷ����仯,��ˢ�µ�ǰ����");
                return -1;
            }
            //����״̬
            string state = PatientInfo.PVisit.InState.ID.ToString();
            //ȡ�䶯��Ϣ:ȡҽ������ʿ�����ҵ���Ϣ
            this.GetPatientInfo(PatientInfo);

            //�ж��Ƿ���ѡ��λ
            if (this.cmbBedNo.Text.Trim() == ""
                && register.IsBaby == false)
            {
               MessageBox.Show( "δѡ��λ��");
                return -1;
            }

            //ȡ����ʱ�Ĵ�λ��Ϣ
            FS.HISFC.Models.Base.Bed bed = new FS.HISFC.Models.Base.Bed();
            bed.ID = this.cmbBedNo.Tag.ToString();	//����
            bed.InpatientNO = register.ID;		//��λ�ϻ���סԺ��ˮ��


            #region ҵ����

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            outpatient.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            
            ////ת��
            //if (this.arrivetype == ArriveType.ShiftIn)
            //{

            //    if (managerRADT.ShiftIn(PatientInfo, this.NurseCell, this.cmbBedNo.Tag.ToString()) == -1)//����ת��ҵ��
            //    {
            //        FS.FrameWork.Management.PublicTrans.RollBack();
            //        MessageBox.Show(managerRADT.Err);
            //        return -1;
            //    }
            //    else
            //    {
    
            //    }
            //}

            //���令��վΪ��ǰ����վ(ת�����ʱ,��Ҫ����ԭ����վ��Ϣ,�����ڴ˴�����)
            PatientInfo.PVisit.PatientLocation.NurseCell = this.NurseCell;
            PatientInfo.PVisit.PatientLocation.Bed = bed;
            
            //����
            if (this.arrivetype == ArriveType.Regedit)
            {
                if (outpatient.EmrArrivePatient(PatientInfo, bed) == -1)//���ý���ҵ��
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(outpatient.Err);
                    return -1;
                }
                else
                {


                }
            }
            //{1C0814FA-899B-419a-94D1-789CCC2BA8FF}
            //�ٻ�
            if (this.arrivetype == ArriveType.CallBack)
            {
                bool isOut = state == "B" ? true : false;
                if (outpatient.CallBack(PatientInfo, bed,isOut) == -1)//�����ٻ�ҵ��
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(outpatient.Err);
                    return -1;
                }
                else
                {


                }
            }

            //��ҽʦ
            if (this.arrivetype == ArriveType.ChangeDoc)
            {
                //if (managerRADT.ChangeDoc(PatientInfo) == -1)//���û�ҽ��ҵ��
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show(managerRADT.Err);
                //    return -1;
                //}
                //else
                //{
            
                //}
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(outpatient.Err);
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
                this.cmbDirector.Tag = obj.AttendingDoctor.ID;//������
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
        ChangeDoc,
        
    }
}
