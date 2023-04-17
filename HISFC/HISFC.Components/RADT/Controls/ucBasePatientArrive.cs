using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.HISFC.Models.RADT;
namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// ���ת�룬�ٻصȻ����ؼ�
    /// </summary>
    public partial class ucBasePatientArrive : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucBasePatientArrive()
        {
            InitializeComponent();
            if (IPrintInHosNotice == null)
            {
                IPrintInHosNotice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPrintInHosNotice)) as FS.HISFC.BizProcess.Interface.IPrintInHosNotice;
            }

            this.AutoScroll = true;
        }

        /// <summary>
        /// ��Ժ֪ͨ����ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IPrintInHosNotice IPrintInHosNotice = null;

        #region ����

        /// <summary>
        /// �ۺϹ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        //protected FS.HISFC.BizLogic.Manager.Constant managerConstant = new FS.HISFC.BizLogic.Manager.Constant();

        FS.HISFC.BizLogic.RADT.InPatient Inpatient = new FS.HISFC.BizLogic.RADT.InPatient();

        FS.HISFC.BizProcess.Integrate.RADT managerRADT = new FS.HISFC.BizProcess.Integrate.RADT();
       
        /// <summary>
        /// ��λ�ձ�����
        /// </summary>
        FS.HISFC.BizLogic.RADT.InpatientDayReport dayReportMgr = new FS.HISFC.BizLogic.RADT.InpatientDayReport();

        /// <summary>
        /// ��ǰ����
        /// </summary>
        protected FS.HISFC.Models.RADT.PatientInfo patientInfo = null;

        /// <summary>
        /// �Ƿ���ڿ��Ҳ�����Ӧ��ϵ
        /// </summary>
        bool isNurseDept = false;//{F044FCF3-6736-4aaa-AA04-4088BB194C20}
        /// <summary>
        /// סԺ���Ŵ�λʱ���Ƿ�����ѡ��ȫԺҽ����0-������ҽ����1-ȫԺҽ����2-�е�¼Ȩ��ҽ��
        /// {35CAF42E-C189-4f52-A9FC-EBA030CFBCCE}
        /// </summary>
        string strAllDoc = "0";

        /// <summary>
        /// ������Ĵ���
        /// </summary>
        protected string arrangeBedNO;

        /// <summary>
        /// ��������
        /// </summary>
        public EnumArriveType Arrivetype
        {
            set
            {
                arrivetype = value;
            }
            get
            {
                return arrivetype;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        private EnumArriveType arrivetype;
        
        /// <summary>
        /// ��ʿվ
        /// </summary>
        protected FS.FrameWork.Models.NeuObject NurseCell = null;

        /// <summary>
        /// ������Ա�б�
        /// </summary>
        protected Hashtable hsDeptEmplList = new Hashtable();

        /// <summary>
        /// ��ǰ����Ա
        /// </summary>
        FS.HISFC.Models.Base.Employee empl;

        /// <summary>
        /// �Ƿ���������ٻ�
        /// </summary>
        private bool isAllowCallBackOtherDay = false;

        /// <summary>
        /// �Ƿ���������ٻ�
        /// </summary>
        [Category("��Ժ�ٻ�"), Description("�Ƿ���������ٻ�")]
        public bool IsAllowCallBackOtherDay
        {
            get
            {
                return isAllowCallBackOtherDay;
            }
            set
            {
                isAllowCallBackOtherDay = value;
            }
        }

        /// <summary>
        /// �Ƿ������޸Ĵ�λ�ȼ�
        /// </summary>
        private bool isAllowModifyBedGrad = false;

        /// <summary>
        /// �Ƿ������޸Ĵ�λ�ȼ�
        /// </summary>
        public bool IsAllowModifyBedGrad
        {
            get
            {
                return isAllowModifyBedGrad;
            }
            set
            {
                isAllowModifyBedGrad = value;

                this.cmbBedLevl.Enabled = isAllowModifyBedGrad;
            }
        }

        /// <summary>
        /// סԺ����ٻء�ת�롢��ҽʦ�ӿ�
        /// </summary>
        private FS.SOC.HISFC.BizProcess.NurseInterface.IArriveBase IArriveBase = null;

        /// <summary>
        /// ��λ�б������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper bedListHelper = null;

        #endregion

        #region ����

        /// <summary>
        /// ������Ĵ���
        /// </summary>
        public string ArrangeBedNO
        {
            get
            {
                return arrangeBedNO;
            }
            set
            {
                arrangeBedNO = value;
            }
        }

        /// <summary>
        /// סԺҽʦ�Ƿ����
        /// </summary>
        bool isAdmittingDoctorNeed = false;

        /// <summary>
        /// סԺҽʦ�Ƿ����
        /// </summary>
        public bool IsAdmittingDoctorNeed
        {
            get
            {
                return isAdmittingDoctorNeed;
            }
            set
            {
                isAdmittingDoctorNeed = value;
            }
        }

        /// <summary>
        /// ����ҽʦ�Ƿ����
        /// </summary>
        bool isAttendingDoctorNeed = false;

        /// <summary>
        /// ����ҽʦ�Ƿ����
        /// </summary>
        public bool IsAttendingDoctorNeed
        {
            get
            {
                return isAttendingDoctorNeed;
            }
            set
            {
                isAttendingDoctorNeed = value;
            }
        }

        /// <summary>
        /// ����ҽʦ�Ƿ����
        /// </summary>
        bool isConsultingDoctorNeed = false;

        /// <summary>
        /// ����ҽʦ�Ƿ����
        /// </summary>        
        public bool IsConsultingDoctorNeed
        {
            get
            {
                return isConsultingDoctorNeed;
            }
            set
            {
                isConsultingDoctorNeed = value;
            }
        }

        /// <summary>
        /// �������Ƿ����
        /// </summary>
        bool isDirectorDoctorNeed = false;

        /// <summary>
        /// �������Ƿ����
        /// </summary>        
        public bool IsDirectorDoctorNeed
        {
            get
            {
                return isDirectorDoctorNeed;
            }
            set
            {
                isDirectorDoctorNeed = value;
            }
        }

        /// <summary>
        /// ���λ�ʿ�Ƿ����
        /// </summary>
        bool isAdmittingNurseNeed = false;

        /// <summary>
        /// ���λ�ʿ�Ƿ����
        /// </summary>        
        public bool IsAdmittingNurseNeed
        {
            get
            {
                return isAdmittingNurseNeed;
            }
            set
            {
                isAdmittingNurseNeed = value;
            }
        }

        /// <summary>
        /// ȫ��ҽ����Ա�б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper allDoctHelper = null;

        /// <summary>
        /// ȫ����ʿ��Ա�б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper allNurseHelper = null;

        /// <summary>
        /// ������ҽ����Ա�б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper privDoctHelper = null;

        /// <summary>
        /// ��������ʿ��Ա�б�
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper privNurseHelper = null;

        /// <summary>
        /// ����ʱ�Ƿ��Զ���������ҽʦ��ת��ǰҽʦ
        /// </summary>
        private bool isAddDoctWhenArrive = true;

        /// <summary>
        /// ����ʱ�Ƿ��Զ���������ҽʦ��ת��ǰҽʦ
        /// </summary>
        public bool IsAddDoctWhenArrive
        {
            get
            {
                return isAddDoctWhenArrive;
            }
            set
            {
                isAddDoctWhenArrive = value;
            }
        }

        /// <summary>
        /// �Ƿ�Ĭ�ϼ���ȫԺҽ��
        /// </summary>
        private bool isAddAllDoct = true;

        /// <summary>
        /// �Ƿ�Ĭ�ϼ���ȫԺҽ��
        /// </summary>
        public bool IsAddAllDoct
        {
            get
            {
                return isAddAllDoct;
            }
            set
            {
                isAddAllDoct = value;
                this.cbxAddAllDoct.Checked = isAddAllDoct;
            }
        }
        /// <summary>
        /// �Ƿ������޸Ľ���ʱ��// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
        /// </summary>
        private bool isCanEditArriveInTime = true;

        /// <summary>
        /// �Ƿ������޸Ľ���ʱ��// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
        /// </summary>
        public bool IsCanEditArriveInTime
        {
            get
            {
                return isCanEditArriveInTime;
            }
            set
            {
                isCanEditArriveInTime = value;
            }
        }

        /// <summary>
        /// �Ƿ�Ĭ�ϼ���ȫԺ��ʿ
        /// </summary>
        private bool isAddAllNurse = true;

        /// <summary>
        /// �Ƿ�Ĭ�ϼ���ȫԺ��ʿ
        /// </summary>
        public bool IsAddAllNurse
        {
            get
            {
                return isAddAllNurse;
            }
            set
            {
                isAddAllNurse = value;
                this.cbxAddAllNurse.Checked = isAddAllNurse;
            }
        }

        #endregion

        #region ����

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            empl = FS.FrameWork.Management.Connection.Operator as FS.HISFC.Models.Base.Employee;
            NurseCell = empl.Nurse.Clone();

            FS.HISFC.BizProcess.Integrate.Common.ControlParam controlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            //�Ƿ���ڿ��Ҳ�����Ӧ��ϵ{F044FCF3-6736-4aaa-AA04-4088BB194C20}
            //isNurseDept = controlIntegrate.GetControlParam<bool>("900010", true, false);
            //strAllDoc = controlIntegrate.GetControlParam<string>("ZY0003", false, "0");

            ArrayList alDepts = new ArrayList();
            ArrayList alTemp = new ArrayList();

            alDepts = managerIntegrate.QueryDepartment(empl.Nurse.ID);
            if (alDepts == null)
            {
                MessageBox.Show("��ȡ������Ӧ�Ŀ����б����\r\n" + managerIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }

            allDoctHelper = new FS.FrameWork.Public.ObjectHelper();
            alTemp = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D);
            if (alTemp == null)
            {
                MessageBox.Show("��ѯ��Ա�б����\r\n" + managerIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
            allDoctHelper.ArrayObject = alTemp;

            allNurseHelper = new FS.FrameWork.Public.ObjectHelper();
            alTemp = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N);
            if (alTemp == null)
            {
                MessageBox.Show("��ѯ��Ա�б����\r\n" + managerIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //return;
            }
            allNurseHelper.ArrayObject = alTemp;

            privDoctHelper = new FS.FrameWork.Public.ObjectHelper();
            foreach (FS.FrameWork.Models.NeuObject dept in alDepts)
            {
                alTemp = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.D, dept.ID);
                if (alTemp == null)
                {
                    MessageBox.Show("��ѯ��Ա�б����\r\n" + managerIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
                privDoctHelper.ArrayObject.AddRange(alTemp);
            }

            privNurseHelper = new FS.FrameWork.Public.ObjectHelper();
            foreach (FS.FrameWork.Models.NeuObject dept in alDepts)
            {
                alTemp = managerIntegrate.QueryEmployee(FS.HISFC.Models.Base.EnumEmployeeType.N, dept.ID);
                if (alTemp == null)
                {
                    MessageBox.Show("��ѯ��Ա�б����\r\n" + managerIntegrate.Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //return;
                }
                privNurseHelper.ArrayObject.AddRange(alTemp);
            }

            //����ҽʦʱ��ȡ��ѡ������
            if (this.cbxAddAllDoct.Checked == true)
            {
                //����סԺҽ���б�
                this.cmbDoct.AddItems(allDoctHelper.ArrayObject);
                //��������ҽ���б�
                this.cmbAttendingDoc.AddItems(allDoctHelper.ArrayObject);
                //��������ҽ���б�
                this.cmbConsultingDoc.AddItems(allDoctHelper.ArrayObject);
                //���ؿ������б�
                this.cmbDirector.AddItems(allDoctHelper.ArrayObject);
                //��������ҽʦ�б�
                this.cmbResponsibleDoc.AddItems(allDoctHelper.ArrayObject);
            }
            else
            {
                //����סԺҽ���б�
                this.cmbDoct.AddItems(privDoctHelper.ArrayObject);
                //��������ҽ���б�
                this.cmbAttendingDoc.AddItems(privDoctHelper.ArrayObject);
                //��������ҽ���б�
                this.cmbConsultingDoc.AddItems(privDoctHelper.ArrayObject);
                //���ؿ������б�
                this.cmbDirector.AddItems(privDoctHelper.ArrayObject);
                //��������ҽʦ�б�
                this.cmbResponsibleDoc.AddItems(privDoctHelper.ArrayObject);
            }

            //���ػ�ʿʱ��ȡ��ѡ������
            if (this.cbxAddAllNurse.Checked == true)
            {

                this.cmbAdmittingNur.AddItems(allNurseHelper.ArrayObject);
                this.cmbAdmittingNur.AppendItems(managerIntegrate.GetDeliverEmployee());  //{0a849cd8-db12-48e0-97ff-0b34f287c0a0}
              
            }
            else
            {
                this.cmbAdmittingNur.AddItems(privNurseHelper.ArrayObject);
            }

            if (bedListHelper == null)
            {
                bedListHelper = new FS.FrameWork.Public.ObjectHelper();
                bedListHelper.ArrayObject = managerIntegrate.QueryBedList(empl.Nurse.ID);
            }

            //���ش�λ�б�
            if (this.arrivetype == EnumArriveType.ChangeDoct)
            {
                //��ҽ��ʱ,��ʾȫ����λ
                this.cmbBedNo.AddItems(bedListHelper.ArrayObject);
            }
            else
            {
                //����ʱ,ֻ��ʾ�մ�
                this.cmbBedNo.AddItems(managerIntegrate.QueryUnoccupiedBed(empl.Nurse.ID));
            }

            //Ѫ��
            //this.cmbBloodType.AddItems(this.managerConstant.GetAllList("BloodKindCode"));
            this.cmbBloodType.AddItems(FS.HISFC.Models.RADT.BloodTypeEnumService.List());	//ȡѪ���б�

            this.cmbBedLevl.AddItems(managerIntegrate.GetConstantList(FS.HISFC.Models.Base.EnumConstant.BEDGRADE));

            this.cmbDoct.IsListOnly = true;
            this.cmbBedNo.IsListOnly = true;
            this.cmbAdmittingNur.IsListOnly = true;
            this.cmbAttendingDoc.IsListOnly = true;
            this.cmbConsultingDoc.IsListOnly = true;
            this.cmbDirector.IsListOnly = true;
            this.cmbBedLevl.IsListOnly = true;
            this.cmbResponsibleDoc.IsListOnly = true;//����ҽʦ

            if (this.IArriveBase == null)
            {
                IArriveBase = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.SOC.HISFC.BizProcess.NurseInterface.IArriveBase)) as FS.SOC.HISFC.BizProcess.NurseInterface.IArriveBase;
            }

            this.SetTendCombox();

            if (this.arrivetype == EnumArriveType.CallBack)
            {
                this.btOutBillPrint.Visible = true;
            }
            else
            {
                this.btOutBillPrint.Visible = false;
            }

            this.cbxAddAllDoct.CheckedChanged += new EventHandler(cbxAddAllDoct_CheckedChanged);
            this.cbxAddAllNurse.CheckedChanged += new EventHandler(cbxAddAllNurse_CheckedChanged);
            
            this.cmbBedNo.SelectedIndexChanged += new EventHandler(cmbBedNo_SelectedIndexChanged);

            this.cmbBedLevl.SelectedIndexChanged += new EventHandler(cmbBedLevl_SelectedIndexChanged);


            this.lblDoct.ForeColor = isAdmittingDoctorNeed ? Color.Blue : Color.Black;
            this.lblAttendingDoc.ForeColor = isAttendingDoctorNeed ? Color.Blue : Color.Black;
            this.lblConsultingDoc.ForeColor = isConsultingDoctorNeed ? Color.Blue : Color.Black;
            this.lblDirector.ForeColor = isDirectorDoctorNeed ? Color.Blue : Color.Black;
            this.lblAdmittingNur.ForeColor = isAdmittingNurseNeed ? Color.Blue : Color.Black;
            this.dtpInTime.Value = this.managerIntegrate.GetDateTimeFromSysDateTime();
            if (this.isCanEditArriveInTime)// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
            {
                this.dtpInTime.Enabled = true;
            }
            else
            {
                this.dtpInTime.Enabled = false;
            }
            return null;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this.cmbBedNo.Focused)
                {
                    this.cmbDoct.Focus();
                    return true;
                }
                else if (this.cmbDoct.Focused)
                {
                    this.cmbAttendingDoc.Focus();
                    return true;
                }
                else if (this.cmbAttendingDoc.Focused)
                {
                    this.cmbConsultingDoc.Focus();
                    return true;
                }
                else if (this.cmbConsultingDoc.Focused)
                {
                    cmbAdmittingNur.Focus();
                    return true;
                }
                else if (this.cmbAdmittingNur.Focused)
                {
                    this.cmbResponsibleDoc.Focus();//����ҽʦ
                    return true;
                }
                else if (this.cmbResponsibleDoc.Focused)
                {
                    cmbTend.Focus();
                    return true;
                }
                else if (cmbTend.Focused)
                {
                    cmbDiet.Focus();
                    return true;
                }
                else if (cmbDiet.Focused)
                {
                    if (cmbDirector.Visible)
                    {
                        cmbDirector.Focus();
                        return true;
                    }
                    else
                    {
                        this.btOK.Focus();
                        return true;
                    }
                }
                else if (cmbDirector.Focused)
                {
                    this.btOK.Focus();
                    return true;
                }
                else if (btOK.Focused)
                {
                    this.neuButton1_Click(new object(), null);
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        void cbxAddAllNurse_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxAddAllNurse.Checked)
            {
                this.cmbAdmittingNur.AddItems(allNurseHelper.ArrayObject);
                this.cmbAdmittingNur.AppendItems(managerIntegrate.GetDeliverEmployee());  ////{0a849cd8-db12-48e0-97ff-0b34f287c0a0}
            }
            else
            {
                this.cmbAdmittingNur.AddItems(privNurseHelper.ArrayObject);
            }
            this.SetPatientInfo(this.patientInfo);
        }

        void cbxAddAllDoct_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbxAddAllDoct.Checked)
            {
                //����סԺҽ���б�
                this.cmbDoct.AddItems(allDoctHelper.ArrayObject);
                //��������ҽ���б�
                this.cmbAttendingDoc.AddItems(allDoctHelper.ArrayObject);
                //��������ҽ���б�
                this.cmbConsultingDoc.AddItems(allDoctHelper.ArrayObject);
                //���ؿ������б�
                this.cmbDirector.AddItems(allDoctHelper.ArrayObject);
                //��������ҽʦ�б�
                this.cmbResponsibleDoc.AddItems(allDoctHelper.ArrayObject);
            }
            else
            {
                //����סԺҽ���б�
                this.cmbDoct.AddItems(privDoctHelper.ArrayObject);
                //��������ҽ���б�
                this.cmbAttendingDoc.AddItems(privDoctHelper.ArrayObject);
                //��������ҽ���б�
                this.cmbConsultingDoc.AddItems(privDoctHelper.ArrayObject);
                //���ؿ������б�
                this.cmbDirector.AddItems(privDoctHelper.ArrayObject);
                //��������ҽʦ�б�
                this.cmbResponsibleDoc.AddItems(privDoctHelper.ArrayObject);
            }
            this.SetPatientInfo(this.patientInfo);
        }

        /// <summary>
        /// ���������ʳ
        /// </summary>
        private void SetTendCombox()
        {
           ArrayList alTend=  managerIntegrate.GetConstantList("TENDLEVEL");
           this.cmbTend.AddItems(alTend);

           ArrayList alDiet = managerIntegrate.GetConstantList("DIET");
           this.cmbDiet.AddItems(alDiet);
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
            ClearPatintInfo();

            this.txtPatientNo.Text = PatientInfo.PID.PatientNO;
            this.txtPatientNo.Tag = PatientInfo.ID;
            this.txtName.Text = PatientInfo.Name;
            this.txtSex.Text = PatientInfo.Sex.Name;
            //��ӿ����벡�� add by zhy
            this.txtPatientDept.Text = PatientInfo.PVisit.PatientLocation.Dept.Name;
            this.txtPatientNueseCell.Text = PatientInfo.PVisit.PatientLocation.NurseCell.Name;

            this.cmbBloodType.Tag = PatientInfo.BloodType.ID;

            this.cmbDoct.Tag = PatientInfo.PVisit.AdmittingDoctor.ID;

            this.cmbAttendingDoc.Tag = PatientInfo.PVisit.AttendingDoctor.ID;

            this.cmbConsultingDoc.Tag = PatientInfo.PVisit.ConsultingDoctor.ID;

            this.cmbAdmittingNur.Tag = PatientInfo.PVisit.AdmittingNurse.ID;

            this.cmbDirector.Tag = PatientInfo.PVisit.AttendingDirector.ID;

            this.cmbResponsibleDoc.Tag = PatientInfo.PVisit.ResponsibleDoctor.ID;//����ҽʦ

            PatientInfo.Disease.Memo = this.Inpatient.GetFoodName(PatientInfo.ID);

            this.cmbDiet.Text = PatientInfo.Disease.Memo;
            this.cmbTend.Tag = PatientInfo.Disease.Tend.ID;
            this.cmbTend.Text = PatientInfo.Disease.Tend.Name;

            if (this.arrivetype == EnumArriveType.ShiftIn)
            {
                this.cmbBedNo.Text = "";
                this.cmbBedNo.Tag = "";
            }
            else
            {
                this.cmbBedNo.Text = PatientInfo.PVisit.PatientLocation.Bed.ID.Length > 4 ? PatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) : "";
                this.cmbBedNo.Tag = PatientInfo.PVisit.PatientLocation.Bed.ID;
            }

            //{C29C2D37-D519-49af-AFEA-4981B1A013AE}
            if (this.arrangeBedNO != null)
            {
                this.cmbBedNo.Tag = this.arrangeBedNO;
            }

            //��ҽ������Ӥ���ٻز���ѡ��λ,��������ͬ
            //if (PatientInfo.IsBaby || this.arrivetype == EnumArriveType.ChangeDoct)
            //{
            //    this.cmbBedNo.Enabled = false;
            //}
            //{DF72A3CF-38E6-4616-8287-DC989A4155F9} Ӥ��ת��
            if (this.arrivetype == EnumArriveType.ChangeDoct)
            {
                this.cmbBedNo.Enabled = false;
            }
            else
            {
                if (PatientInfo.IsBaby)// {7FFE7A7E-239D-4019-97B4-D3F80BB79713}
                {
                    this.cmbBedNo.Enabled = false;
                }
                else
                {
                    this.cmbBedNo.Enabled = true;
                }
            }

            //baby�ſ��Ա༭����ҽʦѡ���
            if (PatientInfo.IsBaby)
            {
                this.cmbResponsibleDoc.Enabled = true;
                this.lblResponsibeDoc.ForeColor = Color.Blue;
            }
            else
            {
                this.cmbResponsibleDoc.Enabled = false;
                this.lblResponsibeDoc.ForeColor = Color.Black;
            }

            if (cmbBedNo.Tag != null && !string.IsNullOrEmpty(cmbBedNo.Tag.ToString()))
            {
                this.cmbBedLevl.Tag = (bedListHelper.GetObjectFromID(cmbBedNo.Tag.ToString()) as FS.HISFC.Models.Base.Bed).BedGrade.ID;
                ShowBedFee();
            }

            if (this.arrivetype == EnumArriveType.Accepts)
            {
                if (!this.isAddDoctWhenArrive)
                {
                    this.cmbDoct.Tag = null;

                    this.cmbAttendingDoc.Tag = null;

                    this.cmbConsultingDoc.Tag = null;

                    this.cmbAdmittingNur.Tag = null;

                    this.cmbDirector.Tag = null;

                    this.cmbResponsibleDoc.Tag = null;//����ҽʦ
                }
            }
        }

        /// <summary>
        /// ��û��߻�����Ϣ�ӿؼ���PatientInfo
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void GetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            //ȡ�ؼ���סԺҽ��
            if (this.cmbDoct.Tag != null && !string.IsNullOrEmpty(this.cmbDoct.Tag.ToString()))
            {
                patientInfo.PVisit.AdmittingDoctor.ID = this.cmbDoct.Tag.ToString();
                patientInfo.PVisit.AdmittingDoctor.Name = this.cmbDoct.Text;
            }
            else
            {
                patientInfo.PVisit.AdmittingDoctor.ID = "";
                patientInfo.PVisit.AdmittingDoctor.Name = "";
            }

            //ȡ�ؼ�������ҽ��
            if (this.cmbAttendingDoc.Tag != null && !string.IsNullOrEmpty(this.cmbAttendingDoc.Tag.ToString()))
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
            if (this.cmbConsultingDoc.Tag != null && !string.IsNullOrEmpty(this.cmbConsultingDoc.Tag.ToString()))
            {
                patientInfo.PVisit.ConsultingDoctor.ID = this.cmbConsultingDoc.Tag.ToString();
                patientInfo.PVisit.ConsultingDoctor.Name = this.cmbConsultingDoc.Text;
            }
            else
            {
                patientInfo.PVisit.ConsultingDoctor.ID = "";
                patientInfo.PVisit.ConsultingDoctor.Name = "";
            }

            //ȡ�ؼ�������ҽʦ
            if (this.cmbResponsibleDoc.Tag != null && !string.IsNullOrEmpty(this.cmbResponsibleDoc.Tag.ToString()))
            {
                patientInfo.PVisit.ResponsibleDoctor.ID = this.cmbResponsibleDoc.Tag.ToString();
                patientInfo.PVisit.ResponsibleDoctor.Name = this.cmbResponsibleDoc.Text;
            }
            else
            {
                patientInfo.PVisit.ResponsibleDoctor.ID = "";
                patientInfo.PVisit.ResponsibleDoctor.Name = "";
            }

            //ȡ�ؼ������λ�ʿ
            if (this.cmbAdmittingNur.Tag != null && !string.IsNullOrEmpty(this.cmbAdmittingNur.Tag.ToString()))
            {
                patientInfo.PVisit.AdmittingNurse.ID = this.cmbAdmittingNur.Tag.ToString();
                patientInfo.PVisit.AdmittingNurse.Name = this.cmbAdmittingNur.Text;
            }
            else
            {
                patientInfo.PVisit.AdmittingNurse.ID = "";
                patientInfo.PVisit.AdmittingNurse.Name = "";
            }

            //ȡ�ؼ��п�����
            if (this.cmbDirector.Tag != null && !string.IsNullOrEmpty(this.cmbDirector.Tag.ToString()))
            {
                patientInfo.PVisit.AttendingDirector.ID = this.cmbDirector.Tag.ToString();
                patientInfo.PVisit.AttendingDirector.Name = this.cmbDirector.Text;
            }
            else
            {
                patientInfo.PVisit.AttendingDirector.ID = "";
                patientInfo.PVisit.AttendingDirector.Name = "";
            }

            //ȡ������
            if (this.cmbTend.Tag != null && !string.IsNullOrEmpty(this.cmbTend.Tag.ToString()))
            {
                patientInfo.Disease.Tend.ID = this.cmbTend.Tag.ToString();
            }
            patientInfo.Disease.Tend.Name = this.cmbTend.Text;
            //ȡ��ʳ
            if (!string.IsNullOrEmpty(this.cmbDiet.Text))
            {
                patientInfo.Disease.Memo = this.cmbDiet.Text;
            }

            //����סԺ״̬Ϊ��Ժ�Ǽ�
            patientInfo.PVisit.InState.ID = "I";
            patientInfo.PVisit.RegistTime = this.dtpInTime.Value;// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual void ClearPatintInfo()
        {
            this.cmbDoct.Text = "";
            this.cmbDoct.Tag = "";
            this.cmbAttendingDoc.Text = "";
            this.cmbAttendingDoc.Tag = "";
            this.cmbConsultingDoc.Text = "";
            this.cmbConsultingDoc.Tag = "";
            this.cmbAdmittingNur.Text = "";
            this.cmbAdmittingNur.Tag = "";
            this.cmbDirector.Text = "";
            this.cmbDirector.Tag = "";
            this.cmbDiet.Tag = "";
            this.cmbDiet.Text = "";
            this.cmbTend.Tag = "";
            this.cmbTend.Text = "";

            this.cmbResponsibleDoc.Tag = "";//����ҽʦ
            this.cmbResponsibleDoc.Text = "";

            lblBedLevlFee.Text = "";
        }

        /// <summary>
        /// ˢ�»�����Ϣ
        /// </summary>
        /// <param name="inPatientNo"></param>
        public virtual void RefreshList(string inPatientNo)
        {
            //���ش�λ�б�
            if (this.arrivetype == EnumArriveType.ChangeDoct)
            {
                //��ҽ��ʱ,��ʾȫ����λ
                if (bedListHelper == null)
                {
                    bedListHelper = new FS.FrameWork.Public.ObjectHelper();
                    bedListHelper.ArrayObject = managerIntegrate.QueryBedList(empl.Nurse.ID);
                }
                this.cmbBedNo.AddItems(bedListHelper.ArrayObject);
            }
            else
            {
                //����ʱ,ֻ��ʾ�մ�
                this.cmbBedNo.AddItems(managerIntegrate.QueryUnoccupiedBed(empl.Nurse.ID));
            }

            try
            {
                this.patientInfo = this.Inpatient.QueryPatientInfoByInpatientNO(inPatientNo);
                if (this.patientInfo == null)
                {
                    MessageBox.Show(this.Inpatient.Err);
                    this.patientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                }

                this.cbxAddAllDoct.Checked = this.isAddAllDoct;
                this.cbxAddAllNurse.Checked = this.isAddAllNurse;


                //2012-12-10 ֱ�Ӷ�ȡ��ѡ���жϼ��ص��б�
                if (this.cbxAddAllDoct.Checked == true)
                {
                    //����סԺҽ���б�
                    this.cmbDoct.AddItems(allDoctHelper.ArrayObject);
                    //��������ҽ���б�
                    this.cmbAttendingDoc.AddItems(allDoctHelper.ArrayObject);
                    //��������ҽ���б�
                    this.cmbConsultingDoc.AddItems(allDoctHelper.ArrayObject);
                    //���ؿ������б�
                    this.cmbDirector.AddItems(allDoctHelper.ArrayObject);
                    //��������ҽʦ�б�
                    this.cmbResponsibleDoc.AddItems(allDoctHelper.ArrayObject);
                }
                else
                {
                    //ҽ�����Ǳ�����ҽ��ʱ��ֻ���ر�����ҽ���б��������ȫԺҽ���б�
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.AdmittingDoctor.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.AdmittingDoctor.ID) == null)
                    {
                        //����סԺҽ���б�
                        this.cmbDoct.AddItems(allDoctHelper.ArrayObject);
                    }
                    else
                    {
                        //����סԺҽ���б�
                        this.cmbDoct.AddItems(privDoctHelper.ArrayObject);
                    }
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.AttendingDoctor.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.AttendingDoctor.ID) == null)
                    {
                        //��������ҽ���б�
                        this.cmbAttendingDoc.AddItems(allDoctHelper.ArrayObject);
                    }
                    else
                    {
                        //��������ҽ���б�
                        this.cmbAttendingDoc.AddItems(privDoctHelper.ArrayObject);
                    }
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.ConsultingDoctor.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.ConsultingDoctor.ID) == null)
                    {
                        //��������ҽ���б�
                        this.cmbConsultingDoc.AddItems(allDoctHelper.ArrayObject);
                    }
                    else
                    {
                        //��������ҽ���б�
                        this.cmbConsultingDoc.AddItems(privDoctHelper.ArrayObject);
                    }
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.AttendingDirector.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.AttendingDirector.ID) == null)
                    {
                        //���ؿ������б�
                        this.cmbDirector.AddItems(allDoctHelper.ArrayObject);
                    }
                    else
                    {
                        //���ؿ������б�
                        this.cmbDirector.AddItems(privDoctHelper.ArrayObject);
                    }
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.ResponsibleDoctor.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.ResponsibleDoctor.ID) == null)
                    {
                        //��������ҽʦ�б�
                        this.cmbResponsibleDoc.AddItems(allDoctHelper.ArrayObject);
                    }
                    else
                    {
                        //��������ҽʦ�б�
                        this.cmbResponsibleDoc.AddItems(privDoctHelper.ArrayObject);
                    }
                }

                //2012-12-10 ֱ�Ӷ�ȡ��ѡ���жϼ��ص��б�
                if (this.cbxAddAllNurse.Checked == true)
                {
                    this.cmbAdmittingNur.AddItems(allNurseHelper.ArrayObject);
                    this.cmbAdmittingNur.AppendItems(managerIntegrate.GetDeliverEmployee());  //{0a849cd8-db12-48e0-97ff-0b34f287c0a0}
                }
                else
                {
                    //��ʿ���Ǳ����һ�ʿʱ��ֻ���ر����һ�ʿ�б��������ȫԺ��ʿ�б�
                    if (!string.IsNullOrEmpty(patientInfo.PVisit.AdmittingNurse.ID) &&
                        privDoctHelper.GetObjectFromID(patientInfo.PVisit.AdmittingNurse.ID) == null)
                    {
                        this.cmbAdmittingNur.AddItems(allNurseHelper.ArrayObject);
                        this.cmbAdmittingNur.AppendItems(managerIntegrate.GetDeliverEmployee()); //{0a849cd8-db12-48e0-97ff-0b34f287c0a0}
                    }
                    else
                    {
                        this.cmbAdmittingNur.AddItems(privNurseHelper.ArrayObject);
                    }
                }

                this.SetPatientInfo(this.patientInfo);


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
                MessageBox.Show(this.Inpatient.Err);
                return -1;
            }
            if (PatientInfo.IsBaby)// {7FFE7A7E-239D-4019-97B4-D3F80BB79713}
            {
                string motherID = this.Inpatient.QueryBabyMotherInpatientNO(this.patientInfo.ID);

                FS.HISFC.Models.RADT.PatientInfo motherPatientInfo = null;
                motherPatientInfo = this.Inpatient.QueryPatientInfoByInpatientNO(motherID);
                if (motherPatientInfo == null)
                {
                    MessageBox.Show(this.Inpatient.Err);
                    return -1;
                }

                if (this.NurseCell.ID == motherPatientInfo.PVisit.PatientLocation.NurseCell.ID)
                {
                    PatientInfo.PVisit.PatientLocation.Bed.ID = motherPatientInfo.PVisit.PatientLocation.Bed.ID;
                    
                }
            }
            else
            {
                this.cmbBedNo.Enabled = true;
            }

            //��ҽ��ʱ,��������Ѳ��ڱ���,���������---������ת�ƺ�,���������û�йر�,����ִ������
            if (PatientInfo.PVisit.PatientLocation.NurseCell.ID != this.NurseCell.ID
                && this.arrivetype == EnumArriveType.ChangeDoct)
            {
                MessageBox.Show("�����Ѳ��ڱ�����,��ˢ�µ�ǰ����");
                return -1;
            }

            //��������Ѳ�����Ժ״̬,���������
            if (PatientInfo.PVisit.InState.ID.ToString() != this.patientInfo.PVisit.InState.ID.ToString())
            {
                MessageBox.Show("������Ϣ�ѷ����仯,��ˢ�µ�ǰ����");
                return -1;
            }

            //����ɵĻ��߻�����Ϣ ���ڸ��ֱ����¼����ز���
            FS.HISFC.Models.RADT.PatientInfo oldPatientInfo = PatientInfo.Clone();

            //ȡ�䶯��Ϣ:ȡҽ������ʿ�����ҵ���Ϣ
            this.GetPatientInfo(PatientInfo);

            //�ж��Ƿ�ҽ������ʿ���Ƿ����
            if (this.JudgeClinicWorkersIsNeed(PatientInfo) == -1)
            {
                return -1;
            }

            //�ж��Ƿ���ѡ��λ
            if ((this.cmbBedNo.Tag == null || string.IsNullOrEmpty(this.cmbBedNo.Tag.ToString())) && patientInfo.IsBaby == false)
            {
                MessageBox.Show("δѡ��λ��");
                this.cmbBedNo.Focus();
                return -1;
            }

            FS.HISFC.BizProcess.Interface.IPatientShiftValid obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid)) as FS.HISFC.BizProcess.Interface.IPatientShiftValid;
            if (obj != null)
            {
                string err = string.Empty;
                bool bl = obj.IsPatientShiftValid(PatientInfo, FS.HISFC.Models.Base.EnumPatientShiftValid.C, ref err);
                if (!bl)
                {
                    MessageBox.Show(err);
                    return -1;
                }
            }

            //ȡ����ʱ�Ĵ�λ��Ϣ
            FS.HISFC.Models.Base.Bed bed = new FS.HISFC.Models.Base.Bed();
            bed.ID = this.cmbBedNo.Tag.ToString();	//����
            bed.InpatientNO = patientInfo.ID;		//��λ�ϻ���סԺ��ˮ��

            FS.HISFC.Models.Base.Bed currBed = cmbBedNo.SelectedItem as FS.HISFC.Models.Base.Bed;
            if (currBed != null)
            {
                //O:ռ�� U:�մ�(��ʹ�ô�λ) C:�ر�  H:�Ҵ� W:���� K:��Ⱦ  I: ����,R���
                if (currBed.Status.ID.ToString() == "H")
                {
                    MessageBox.Show(currBed.ID.Substring(4) + "��״̬Ϊ�Ҵ�����������仼�ߣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                else if (currBed.Status.ID.ToString() == "K")
                {
                    MessageBox.Show(currBed.ID.Substring(4) + "��״̬Ϊ��Ⱦ����������仼�ߣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                else if (currBed.Status.ID.ToString() == "C")
                {
                    MessageBox.Show(currBed.ID.Substring(4) + "���ѹرգ���������仼�ߣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return -1;
                }
                else if (currBed.Status.ID.ToString() == "I")
                {
                    if (MessageBox.Show(currBed.ID.Substring(4) + "��״̬Ϊ���룬�Ƿ���仼�ߣ�", "ѯ��", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) != DialogResult.Yes)
                    {
                        return -1;
                    }
                }
            }

            #region ҵ����

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            managerRADT.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);


            #region �����޸Ĵ�λ�ȼ���Ϣ

            if (cmbBedNo.Tag != null && !string.IsNullOrEmpty(cmbBedNo.Tag.ToString()))
            {
                FS.HISFC.Models.Base.Bed bedOld = bedListHelper.GetObjectFromID(cmbBedNo.Tag.ToString()) as FS.HISFC.Models.Base.Bed;

                FS.HISFC.Models.Base.Bed bedNew = bedOld.Clone();
                bedNew.BedGrade.ID = cmbBedLevl.Tag.ToString();
                bedNew.BedGrade.Name = cmbBedLevl.Text;

                if (bedOld.BedGrade.ID != cmbBedLevl.Tag.ToString())
                {
                    string sql = @"UPDATE  com_bedinfo     --������Ϣ��
                                   SET  fee_grade_code = '{1}' --��λ�ȼ�����
                                   WHERE  BED_NO = '{0}'   --����";
                    if (this.Inpatient.ExecNoQuery(sql, bedOld.ID, bedNew.BedGrade.ID) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.Msg("���´�λ�ȼ���Ϣ����!" + this.managerIntegrate.Err, 211);

                        return -1;
                    }

                    //��������Ϣ
                    if (this.managerRADT.InsertShiftData(this.patientInfo.ID, FS.HISFC.Models.Base.EnumShiftType.F, "�޸Ĵ�λ�ȼ�", bedOld.BedGrade, new FS.FrameWork.Models.NeuObject(cmbBedLevl.Tag.ToString(), cmbBedLevl.Text, "")) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        FS.FrameWork.WinForms.Classes.Function.Msg("��������Ϣ����!" + this.managerRADT.Err, 211);

                        return -1;
                    }
                }
            }

            #endregion

            //ת��
            if (this.arrivetype == EnumArriveType.ShiftIn)
            {
                if (this.IArriveBase != null)
                {
                    if (IArriveBase.PatientArrive(EnumArriveType.ShiftIn, oldPatientInfo, PatientInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(IArriveBase.ErrInfo);
                        return -1;
                    }
                }

                if (managerRADT.ShiftIn(PatientInfo, this.NurseCell, this.cmbBedNo.Tag.ToString()) == -1)//����ת��ҵ��
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
                }

                //��λ�ձ�����
                //if (dayReportMgr.ArriveBed(oldPatientInfo, patientInfo) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("����̬�ձ�����\r\n" + dayReportMgr.Err);
                //    return -1;
                //}
            }

            //���令��վΪ��ǰ����վ(ת�����ʱ,��Ҫ����ԭ����վ��Ϣ,�����ڴ˴�����)
            PatientInfo.PVisit.PatientLocation.NurseCell = this.NurseCell;
            PatientInfo.PVisit.PatientLocation.Bed = bed;

            //����
            if (this.arrivetype == EnumArriveType.Accepts)
            {
                //PatientInfo.BloodType.Name = this.cmbBloodType.Text;//��ID����Name
                PatientInfo.BloodType.ID = this.cmbBloodType.Tag;
                 
                if (managerRADT.ArrivePatient(PatientInfo, bed) == -1)//���ý���ҵ��
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
                }

                if (this.IArriveBase != null)
                {
                    if (IArriveBase.PatientArrive(EnumArriveType.Accepts, oldPatientInfo, PatientInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(IArriveBase.ErrInfo);
                        return -1;
                    }
                }

                //��λ�ձ�����
                //if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.K) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("����̬�ձ�����\r\n" + dayReportMgr.Err);
                //    return -1;
                //}
            }

            //�ٻ�
            if (this.arrivetype == EnumArriveType.CallBack)
            {
                if (!this.isAllowCallBackOtherDay & patientInfo.PVisit.OutTime.Date < this.Inpatient.GetDateTimeFromSysDateTime().Date)
                {
                    MessageBox.Show("����������ٻأ�������������ϵ��Ϣ�ƣ�", "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return -1;
                }

                PatientInfo.BloodType.ID = this.cmbBloodType.Tag;
                if (managerRADT.CallBack(PatientInfo, bed) == -1)//�����ٻ�ҵ��
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
                    //���ڳ�Ժ�Ǽ�ʱ���յķ��ý����˷�
                    FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
                    feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    if (feeIntegrate.QuitSupplementFee(PatientInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("�˷�ʧ�ܣ�" + feeIntegrate.Err);
                        return -1;
                    }
                    if (this.IArriveBase != null)
                    {
                        if (IArriveBase.PatientArrive(EnumArriveType.CallBack, oldPatientInfo, PatientInfo) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show(IArriveBase.ErrInfo);
                            return -1;
                        }
                    }
                }

                //��λ�ձ�����
                //if (dayReportMgr.ArriveBed(patientInfo, FS.HISFC.Models.Base.EnumShiftType.C) == -1)
                //{
                //    FS.FrameWork.Management.PublicTrans.RollBack();
                //    MessageBox.Show("����̬�ձ�����\r\n" + dayReportMgr.Err);
                //    return -1;
                //}
            }

            //��ҽʦ
            if (this.arrivetype == EnumArriveType.ChangeDoct)
            {
                PatientInfo.BloodType.ID = this.cmbBloodType.Tag;

                if (managerRADT.ChangeDoc(PatientInfo) == -1)//���û�ҽ��ҵ��
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(managerRADT.Err);
                    return -1;
                }
                else
                {
                }

                if (this.IArriveBase != null)
                {
                    if (IArriveBase.PatientArrive(EnumArriveType.ChangeDoct, oldPatientInfo, PatientInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show(IArriveBase.ErrInfo);
                        return -1;
                    }
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            this.arrangeBedNO = null; //{C29C2D37-D519-49af-AFEA-4981B1A013AE}
            MessageBox.Show(managerRADT.Err);

            base.OnRefreshTree();//ˢ����
            return 1;
        }

        /// <summary>
        /// �жϱ�����
        /// </summary>
        /// <returns></returns>
        private int JudgeClinicWorkersIsNeed(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            if (this.isAdmittingDoctorNeed)
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.AdmittingDoctor.ID))
                {
                    MessageBox.Show("��ѡ���������ҽʦ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbDoct.Focus();
                    return -1;
                }
            }
            if (isAttendingDoctorNeed)
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.AttendingDoctor.ID))
                {
                    MessageBox.Show("��ѡ������ҽʦ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbAttendingDoc.Focus();
                    return -1;
                }
            }
            if (isAdmittingNurseNeed)
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.AdmittingNurse.ID))
                {
                    MessageBox.Show("��ѡ�����λ�ʿ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbAdmittingNur.Focus();
                    return -1;
                }
            }
            if (isConsultingDoctorNeed)
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.ConsultingDoctor.ID))
                {
                    MessageBox.Show("��ѡ������ҽʦ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbConsultingDoc.Focus();
                    return -1;
                }
            }
            if (isDirectorDoctorNeed && this.cmbDirector.Visible)
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.AttendingDirector.ID))
                {
                    MessageBox.Show("��ѡ�������ҽʦ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbDirector.Focus();
                    return -1;
                }
            }

            if (this.cmbResponsibleDoc.Enabled)//������ҽʦ��ѡ�����ݣ�ֻ��baby���Ǳ���ģ�
            {
                if (string.IsNullOrEmpty(patientInfo.PVisit.ResponsibleDoctor.ID))
                {
                    MessageBox.Show("��ѡ������ҽʦ��", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.cmbResponsibleDoc.Focus();
                    return -1;
                }
            }

            if (!isCanAddOrder(patientInfo.PVisit.AdmittingDoctor.ID))
            {
                string doctName = SOC.HISFC.BizProcess.Cache.Common.GetEmployeeName(patientInfo.PVisit.AdmittingDoctor.ID);
                MessageBox.Show("ҽ����" + doctName + "��û�д���Ȩ��������ΪסԺҽʦ��\r\n\r\n������������ϵҽ��ƣ�", "��ֹ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.cmbDoct.Focus();
                return -1;
            }
            if (this.patientInfo.PVisit.InTime > patientInfo.PVisit.RegistTime)
            {
                MessageBox.Show("����ʱ�䲻��С����Ժʱ�䣡", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                this.dtpInTime.Focus();
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ����Ҫ��û�д���Ȩ�Ĳ�����Ϊ�ܴ�ҽ��
        /// </summary>
        /// <returns></returns>
        private bool isCanAddOrder(string emplID)
        {
            string strSQL = @"select count(*) from met_com_authority f
                            where f.empl_code='{0}'
                            and f.popedom_type='1'";

            strSQL = string.Format(strSQL, emplID);

            string rev = Inpatient.ExecSqlReturnOne(strSQL, "0");
            if (rev == "0")
            {
                return false;
            }
            return true;
        }

        #endregion

        #region �¼� 

        private void cmbBedNo_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.cmbBedNo.Tag == null || this.cmbBedNo.Tag.ToString() == "") 
                return;

            this.cmbBedLevl.Tag = (bedListHelper.GetObjectFromID(cmbBedNo.Tag.ToString()) as FS.HISFC.Models.Base.Bed).BedGrade.ID;
            ShowBedFee();
        }

        void cmbBedLevl_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// ��ʾ��λ��
        /// </summary>
        /// <returns></returns>
        private int ShowBedFee()
        {
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();
            if (cmbBedLevl.Tag == null || string.IsNullOrEmpty(cmbBedLevl.Tag.ToString()))
            {
                return -1;
            }
            ArrayList alBedItem = feeIntegrate.QueryBedFeeItemByMinFeeCode(cmbBedLevl.Tag.ToString());
            if (alBedItem == null)
            {
                MessageBox.Show("��ѯ��λ�ѳ�����λ�ȼ���" + cmbBedLevl.Name + "\r\n" + feeIntegrate.Err, "����");
                return -1;
            }

            decimal bedFee = 0;
            DateTime dtNow = feeIntegrate.GetDateTimeFromSysDateTime();
            foreach (FS.HISFC.Models.Fee.BedFeeItem bedItem in alBedItem)
            {
                if (bedItem.IsTimeRelation)
                {
                    DateTime begin = new DateTime(dtNow.Year, bedItem.BeginTime.Month, bedItem.BeginTime.Day);
                    DateTime end = new DateTime(dtNow.Year, bedItem.EndTime.Month, bedItem.EndTime.Day);

                    //��ʼʱ��>����ʱ�� ��Ϊ�Ǵ����꿪ʼ��ʱ��
                    if (begin > end)
                    {
                        begin = begin.AddYears(-1);
                    }

                    if (begin > dtNow || end < dtNow)
                    {
                        continue;
                    }
                }

                FS.HISFC.Models.Fee.Item.Undrug undrug = feeIntegrate.GetItem(bedItem.ID);


                if (undrug != null && undrug.IsValid)
                {
                    if (SOC.HISFC.BizProcess.Cache.Common.IsContainYKDept(((FS.HISFC.Models.Base.Employee)this.Inpatient.Operator).Dept.Clone().ID))
                    {
                        bedFee += bedItem.Qty * undrug.SpecialPrice;

                    }
                    else
                    {
                        bedFee += bedItem.Qty * undrug.Price;

                    }
                }
            }
            lblBedLevlFee.Text = "��λ�ѣ�" + bedFee.ToString("F6").TrimEnd('0').TrimEnd('.') + "Ԫ";
            return 1;
        }

        private void neuButton1_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid) };
            }
        }

        #endregion

        private void btOutBillPrint_Click(object sender, EventArgs e)
        {
            if (this.patientInfo == null)
            {
                MessageBox.Show("δѡ�л��ߣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IPrintInHosNotice != null)
            {
                IPrintInHosNotice.SetValue(this.patientInfo);
                if (((FS.HISFC.Models.Base.Employee)this.managerIntegrate.Operator).IsManager)
                {
                    IPrintInHosNotice.PrintView();
                }
                else
                {
                    IPrintInHosNotice.Print();
                }
            }
            else
            {
                ucOutPrint print = new ucOutPrint();
                print.SetPatientInfo(this.patientInfo);
                //print.NameFlag = this.cmbOutpatientAim.Tag.ToString().Trim();
                //print.PrintPreview();
                print.Print();
            }
           
            MessageBox.Show("�����Ժ֪ͨ���ɹ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


    }
}
