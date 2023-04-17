using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Neusoft.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [��������: ��Ժ�����б�]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2008-09-3]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��=''
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucPatientCallBack : Neusoft.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPatientCallBack()
        {
            InitializeComponent();
            
        }


        #region �������¼�

        protected override void OnRefreshTree()
        {
            base.OnRefreshTree();
            this.tvOutHosList1.Refresh(deptCode, tempBegin, tempEnd);
        }

        protected override int OnQuery(object sender, object neuObject)
        {
            this.Refresh();
            return base.OnQuery(sender, neuObject);
        }

        #endregion

        #region ����
        private string tempBegin = System.DateTime.Now.AddDays(-3).ToShortDateString();
        private string tempEnd = System.DateTime.Now.AddDays(4).ToShortDateString();
        Neusoft.HISFC.BizProcess.Integrate.Manager deptManager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        Neusoft.FrameWork.Public.ObjectHelper deptHelper = new Neusoft.FrameWork.Public.ObjectHelper();
        Neusoft.HISFC.BizLogic.RADT.InPatient Inpatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();
        Neusoft.HISFC.Models.RADT.PatientInfo patientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
        private string deptCode;//Ĭ�Ͽ���

        private string strBegin
        {
            get
            {
                if (cbTime.Checked)
                {
                    return dtBegin.Value.Year.ToString() + "-" + dtBegin.Value.Month.ToString() + "-" + dtBegin.Value.Day.ToString() + " 00:00:00";
                }
                else
                {
                    return tempBegin;
                }
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        private string strEnd
        {
            get
            {
                if (cbTime.Checked)
                {
                    return dtEnd.Value.Year.ToString() + "-" + dtEnd.Value.Month.ToString() + "-" + dtEnd.Value.Day.ToString() + " 23:59:59";
                }
                else
                {
                    return tempEnd;
                }
            }
        }

        /// <summary>
        /// Ĭ�Ͽ���
        /// </summary>
        public string DeptCode
        {
            get
            {
                return deptCode;
            }
            set
            {
                deptCode = value;
                this.cmbDept.Tag = deptCode;
            }
        }

        /// <summary>
        /// adt�ӿ�
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.IHE.IADT adt = null;

        #endregion

        #region ����
        protected override void OnLoad(EventArgs e)
        {
            this.cmbDept.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
            this.dtBegin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
            this.dtEnd.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
            this.cmbBedNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
            this.cmbAdmittingNur.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
            this.cmbAttendingDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
            this.cmbConsultingDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
            this.cmbDoc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbDept_KeyPress);
            try
            {
                this.tvOutHosList1.Refresh(deptCode, tempBegin, tempEnd);
                this.cmbDept.AddItems(deptManager.QueryDeptmentsInHos(true));
                deptHelper.ArrayObject = this.cmbDept.alItems;
                this.neuTextBox1.Text = this.cmbDept.Text + "-"+"��Ժ�����б�";
                this.neuTextBox1.Enabled = false;
                this.dtBegin.Text = System.DateTime.Now.AddDays(-3).ToShortDateString();
                this.dtEnd.Text = System.DateTime.Now.AddDays(4).ToShortDateString();
                this.cbTime.Checked = false;
            }
            catch { }
            base.OnLoad(e);
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        private void QueryOutHosPatient(string deptCode, string beginTime, string endTime)
        {
            this.tvOutHosList1.Refresh(deptCode, beginTime, endTime);
        }

        private new void Refresh()
        {
            this.neuTextBox1.Text = this.cmbDept.Text + "-" + "��Ժ�����б�";
            deptCode = cmbDept.Tag.ToString();
            tempBegin = dtBegin.Value.ToString();
            tempEnd = dtEnd.Value.ToString();
            QueryOutHosPatient(deptCode, tempBegin, tempEnd);
        }

        /// <summary>
        /// ��������
        /// </summary>
        public virtual int Save()
        {
            //ȡӤ������ʱ����Ϣ��Ϣ
            //Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo = null;

            //ȡ�������µ�סԺ������Ϣ
            //PatientInfo = this.Inpatient.QueryPatientInfoByInpatientNO(this.patientInfo.ID);
            if (this.patientInfo == null)
            {
                MessageBox.Show(this.Inpatient.Err);
                return -1;
            }

            //ȡ�䶯��Ϣ:ȡҽ������ʿ�����ҵ���Ϣ
            //this.GetPatientInfo(PatientInfo);

            //�ж��Ƿ���ѡ��λ
            if (this.cmbBedNo.Text.Trim() == ""
                && patientInfo.IsBaby == false)
            {
                MessageBox.Show("δѡ��λ��");
                return -1;
            }

            //ȡ����ʱ�Ĵ�λ��Ϣ
            Neusoft.HISFC.Models.Base.Bed bed = new Neusoft.HISFC.Models.Base.Bed();
            bed.ID = this.cmbBedNo.Tag.ToString();	//����
            bed.InpatientNO = patientInfo.ID;		//��λ�ϻ���סԺ��ˮ��


            #region ҵ����

            Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

            Neusoft.HISFC.BizProcess.Integrate.RADT managerRADT = new Neusoft.HISFC.BizProcess.Integrate.RADT();

            if (managerRADT.CallBack(patientInfo, bed) == -1)//�����ٻ�ҵ��
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(managerRADT.Err);
                return -1;
            }
            else
            {

            }
            #endregion

            #region addby xuewj 2010-3-15

            if (this.adt == null)
            {
                this.adt = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IHE.IADT)) as Neusoft.HISFC.BizProcess.Interface.IHE.IADT;
            }
            if (this.adt != null)
            {
                this.adt.CancelDischargePatientMessage(patientInfo);
            }

            #endregion

            Neusoft.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(managerRADT.Err);
            this.Refresh();
            this.ClearPatintInfo();
            return 1;
        }



        /// <summary>
        /// ��û��߻�����Ϣ�ӿؼ���PatientInfo
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void GetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
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
        /// ��������Ϣ��ʾ���ؼ���
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo PatientInfo)
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
            this.cmbBedNo.Tag = patientInfo.PVisit.PatientLocation.Bed.ID;
            this.cmbBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;
            //this.cmbBedNo.AddItems(deptManager.QueryUnoccupiedBed(PatientInfo.PVisit.PatientLocation.NurseCell.ID));
            //this.cmbAdmittingNur.AddItems(deptManager.QueryNurse(PatientInfo.PVisit.PatientLocation.NurseCell.ID));
             
            ArrayList alDepts = deptManager.QueryDepartment(PatientInfo.PVisit.PatientLocation.NurseCell.ID);
            try
            {
                //ȡҽ���б�
                ArrayList al = new ArrayList();
                
                foreach (Neusoft.FrameWork.Models.NeuObject dept in alDepts)
                {
                    al.AddRange(deptManager.QueryEmployee(Neusoft.HISFC.Models.Base.EnumEmployeeType.D, dept.ID));
                
                }

                //����סԺҽ���б�
                this.cmbDoc.AddItems(al);
                //��������ҽ���б�
                this.cmbAttendingDoc.AddItems(al);
                //��������ҽ���б�
                this.cmbConsultingDoc.AddItems(al);
                //�������λ�ʿ�б�
                this.cmbAdmittingNur.AddItems(deptManager.QueryNurse(PatientInfo.PVisit.PatientLocation.NurseCell.ID));
                //���ش�λ�б�
                this.cmbBedNo.AddItems(deptManager.QueryUnoccupiedBed(PatientInfo.PVisit.PatientLocation.NurseCell.ID));
                this.cmbDoc.IsListOnly = true;
                this.cmbBedNo.IsListOnly = true;
                this.cmbAdmittingNur.IsListOnly = true;
                this.cmbAttendingDoc.IsListOnly = true;
                this.cmbConsultingDoc.IsListOnly = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            } 
            //this.cmbDoc.AddItems(deptManager.QueryEmployee())
            //��ҽ������Ӥ���ٻز���ѡ��λ,��������ͬ
            if (PatientInfo.IsBaby)
                this.cmbBedNo.Enabled = false;
            else
                this.cmbBedNo.Enabled = true;
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

        #endregion

        #region ��ʼ��

        #region ��ʼ����Ժ�����б�
        /// <summary>
        /// ��ʼ����Ժ�����б�
        /// </summary>
        private void InitTree()
        {

        }
        #endregion

        #region ���ñ���ɫ
        private void SetBackColor()
        {
            this.BackColor = System.Drawing.Color.Azure;
        }
        #endregion
        #endregion

        #region �¼�

        private void cmbDept_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                SendKeys.Send("{tab}");
                e.Handled = true;
            }
        }        

        private void cbTime_CheckedChanged_1(object sender, EventArgs e)
        {
            if (cbTime.Checked)
            {
                dtBegin.Enabled = true;
                dtEnd.Enabled = true;
            }
            else
            {
                dtBegin.Enabled = false;
                dtEnd.Enabled = false;
            }
        }

        /// <summary>
        /// �����¼� 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        private void tvOutHosList1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                return;
            }
            else if(e.Node.Level==1)
            {
                patientInfo = e.Node.Tag as Neusoft.HISFC.Models.RADT.PatientInfo;
                this.SetPatientInfo(patientInfo);
            }   
        }

        private void cmbBedNo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (this.cmbBedNo.Tag == null || this.cmbBedNo.Tag.ToString() == "") return;
                Neusoft.HISFC.Models.Base.Bed obj = deptManager.GetBed(this.cmbBedNo.Tag.ToString());
                if (obj == null) return;
                this.cmbDoc.Tag = obj.Doctor.ID;					//סԺҽ��
                this.cmbAttendingDoc.Tag = obj.AttendingDoctor.ID;	//����ҽ��
                this.cmbConsultingDoc.Tag = obj.ConsultingDoctor.ID;//����ҽ��
                this.cmbAdmittingNur.Tag = obj.AdmittingNurse.ID;	//���λ�ʿ
        }
        #endregion
    }
}
