using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.RADT;

namespace FS.HISFC.Components.RADT.Controls
{
    /// <summary>
    /// [��������: ��ʿվ���������л��ؼ�]<br></br>
    /// [�� �� ��: wolf]<br></br>
    /// [����ʱ��: 2006-11-30]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucRADT : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// ����
        /// </summary>
        public ucRADT()
        {
            InitializeComponent();

            this.wapType = 3;
        }

        #region ����
        protected TreeView tv = null;
        protected TreeNode node = null;
        protected FS.HISFC.Models.RADT.PatientInfo patient = null;

        /// <summary>
        ///������Ĵ��� 
        /// </summary>
        protected string arrangeBedNO;

        /// <summary>
        /// ����ҵ����{81987883-BFB0-42f7-8B99-CF44CA44BDDA}
        /// </summary>
        FS.HISFC.BizLogic.RADT.InPatient inpatientManager = new FS.HISFC.BizLogic.RADT.InPatient();
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            try
            {
                tv = sender as TreeView;
            }
            catch { }
            //this.neuTabControl1.TabPages.Clear();
            //this.neuTabControl1.TabPages.Add(this.tbBedView);//Ĭ����ʾ����
            //ucBedListView uc = new ucBedListView();
            //uc.WapType = wapType;
            //uc.arrangeBed += new ucBedListView.ArrangeBed(uc_arrangeBed);
            //uc.ListViewItemChanged += new ListViewItemSelectionChangedEventHandler(uc_ListViewItemChanged);
            //uc.Dock = DockStyle.Fill;
            //uc.Visible = true;
            //FS.FrameWork.WinForms.Forms.IControlable ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
            //if (ic != null)
            //{
            //    ic.Init(this.tv, null, null);
            //    ic.SetValue(patient, this.tv.Nodes[0]);
            //    ic.RefreshTree += new EventHandler(ic_RefreshTree);
            //    ic.SendParamToControl += new FS.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
            //    ic.StatusBarInfo += new FS.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

            //}
            //this.tbBedView.Controls.Add(uc);

            return base.OnInit(sender, neuObject, param);
        }

        #region ˽�б���
        private bool sexReadOnly = true;
        private bool birthdayReadOnly = true;
        private bool relationReadOnly = true;
        private bool heightReadOnly = true;
        private bool weightReadOnly = true;
        private bool iDReadOnly = true;
        private bool professionReadOnly = true;
        private bool marryReadOnly = true;
        private bool homeAddrReadOnly = true;
        private bool homeTelReadOnly = true;
        private bool workReadOnly = true;
        private bool linkManReadOnly = true;
        private bool kinAddressReadOnly = true;
        private bool linkTelReadOnly = true;
        private bool memoReadOnly = true;
        private bool tpLeaveVisible = false;

        /// <summary>
        /// Ӥ���Ǽǹ����Ƿ���ʾ
        /// </summary>
        private bool tpNurseVisible = false;


        /// <summary>
        /// �Ƿ�ֻ��ʾӤ���Ǽ�// {1292AA34-6E62-4f01-9BE7-EAE667D34E5D}
        /// </summary>
        private bool tpOnlyBabyVisible = false;

        /// ��ֹ����Ǽ��Ƿ���ʾ
        /// </summary>
        private bool isStopPregnancyVisible = false;

        /// <summary>
        /// ��λ�ȼ���Ϣ�Ƿ���ʾ
        /// </summary>
        private bool isShowPatientBed = false;

        /// <summary>
        /// ��λ�ȼ���Ϣ�Ƿ���ʾ
        /// </summary>
        [Category("tabҳ����"), Description("��λ�ȼ���Ϣ�Ƿ���ʾ")]
        public bool IsShowPatientBed
        {
            get
            {
                return isShowPatientBed;
            }
            set
            {
                isShowPatientBed = value;
            }
        }

        /// <summary>
        /// ת���������Ƿ���ʾ
        /// </summary>
        private bool tpShiftNurseCellVisible = false;

        /// <summary>
        /// ����Ӥ���ǼǵĿ���
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper babyDeptHelper = null;

        #endregion

        #region  ����

        #region ��������

        /// <summary>
        /// ����ʱ�Ƿ��Զ���������ҽʦ��ת��ǰҽʦ
        /// </summary>
        private bool isAddDoctWhenArrive = true;

        /// <summary>
        /// ����ʱ�Ƿ��Զ���������ҽʦ��ת��ǰҽʦ
        /// </summary>
        [Category("��������"), Description("����ʱ�Ƿ��Զ���������ҽʦ��ת��ǰҽʦ������Ϊ��")]
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
        [Category("��������"), Description("�Ƿ�Ĭ�ϼ���ȫԺҽ��")]
        public bool IsAddAllDoct
        {
            get
            {
                return isAddAllDoct;
            }
            set
            {
                isAddAllDoct = value;
            }
        }
        /// <summary>
        /// �Ƿ������޸Ľ���ʱ��// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
        /// </summary>
        private bool isCanEditArriveInTime = true;

        /// <summary>
        /// �Ƿ������޸Ľ���ʱ��
        /// </summary>
        [Category("��������"), Description("�Ƿ������޸Ľ���ʱ��")]
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
        [Category("��������"), Description("�Ƿ�Ĭ�ϼ���ȫԺ��ʿ")]
        public bool IsAddAllNurse
        {
            get
            {
                return isAddAllNurse;
            }
            set
            {
                isAddAllNurse = value;
            }
        }

        #endregion

        #region ��Ժ�Ǽ�����

        /// <summary>
        /// ҩƷδִ��ȷ���Ƿ���������Ժ��Ҫ��ִ��ȷ�����̣�
        /// </summary>
        private bool isCanOutWhenExecDrugLimit = false;

        /// <summary>
        /// ҩƷδִ��ȷ���Ƿ���������Ժ��Ҫ��ִ��ȷ�����̣�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("ҩƷδִ��ȷ���Ƿ���������Ժ��Ҫ��ִ��ȷ�����̣�")]
        public bool IsCanOutWhenExecDrugLimit
        {
            get
            {
                return isCanOutWhenExecDrugLimit;
            }
            set
            {
                isCanOutWhenExecDrugLimit = value;
            }
        }

        /// <summary>
        /// ����δ��ɵĻ��������Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenUnConsultation = CheckState.Check;

        /// <summary>
        /// ����δ��ɵĻ��������Ƿ���������Ժ�Ǽ�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("����δ��ɵĻ��������Ƿ���������Ժ�Ǽ�")]
        public CheckState IsCanOutWhenUnConsultation
        {
            get
            {
                return this.isCanOutWhenUnConsultation;
            }
            set
            {
                isCanOutWhenUnConsultation = value;
            }
        }

        /// <summary>
        /// �����˷������Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenQuitFeeApplay = CheckState.No;

        /// <summary>
        /// ���˷������Ƿ������Ժ�Ǽ�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("�����˷������Ƿ���������Ժ�Ǽ�")]
        public CheckState IsCanOutWhenQuitFeeApplay
        {
            get
            {
                return isCanOutWhenQuitFeeApplay;
            }
            set
            {
                isCanOutWhenQuitFeeApplay = value;
            }
        }

        /// <summary>
        /// ������ҩ�����Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenQuitDrugApplay = CheckState.No;

        /// <summary>
        /// ������ҩ�����Ƿ���������Ժ�Ǽ�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("������ҩ�����Ƿ���������Ժ�Ǽ�")]
        public CheckState IsCanOutWhenQuitDrugApplay
        {
            get
            {
                return isCanOutWhenQuitDrugApplay;
            }
            set
            {
                isCanOutWhenQuitDrugApplay = value;
            }
        }

        /// <summary>
        /// ���ڷ�ҩ�����Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenDrugApplay = CheckState.No;

        /// <summary>
        /// ���ڷ�ҩ�����Ƿ���������Ժ�Ǽ�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("���ڷ�ҩ�����Ƿ���������Ժ�Ǽ�")]
        public CheckState IsCanOutWhenDrugApplay
        {
            get
            {
                return this.isCanOutWhenDrugApplay;
            }
            set
            {
                this.isCanOutWhenDrugApplay = value;
            }
        }

        /// <summary>
        /// ����δȷ����Ŀ�Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenUnConfirm = CheckState.No;

        /// <summary>
        /// ����δȷ����Ŀ�Ƿ���������Ժ�Ǽ�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("����δȷ����Ŀ�Ƿ���������Ժ�Ǽ�")]
        public CheckState IsCanOutWhenUnConfirm
        {
            get
            {
                return this.isCanOutWhenUnConfirm;
            }
            set
            {
                this.isCanOutWhenUnConfirm = value;
            }
        }

        /// <summary>
        /// δ������Ժҽ���Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenNoOutOrder = CheckState.No;

        /// <summary>
        /// δ������Ժҽ���Ƿ���������Ժ�Ǽ�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("δ������Ժҽ���Ƿ���������Ժ�Ǽ�")]
        public CheckState IsCanOutWhenNoOutOrder
        {
            get
            {
                return this.isCanOutWhenNoOutOrder;
            }
            set
            {
                this.isCanOutWhenNoOutOrder = value;
            }
        }

        /// <summary>
        /// δȫ��ֹͣ�����Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenNoDcOrder = CheckState.No;

        /// <summary>
        /// δȫ��ֹͣ�����Ƿ���������Ժ�Ǽ�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("δȫ��ֹͣ�����Ƿ���������Ժ�Ǽ�")]
        public CheckState IsCanOutWhenNoDcOrder
        {
            get
            {
                return this.isCanOutWhenNoDcOrder;
            }
            set
            {
                this.isCanOutWhenNoDcOrder = value;
            }
        }

        /// <summary>
        /// ����δ���ҽ���Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenUnConfirmOrder = CheckState.Check;

        /// <summary>
        /// ����δ���ҽ���Ƿ���������Ժ�Ǽ�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("����δ���ҽ���Ƿ���������Ժ�Ǽ�")]
        public CheckState IsCanOutWhenUnConfirmOrder
        {
            get
            {
                return this.isCanOutWhenUnConfirmOrder;
            }
            set
            {
                this.isCanOutWhenUnConfirmOrder = value;
            }
        }

        /// <summary>
        /// ����δ�շѵ��������뵥�Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenUnFeeUOApply = CheckState.Yes;

        /// <summary>
        /// ����δ�շѵ��������뵥�Ƿ���������Ժ�Ǽ�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("����δ�շѵ��������뵥�Ƿ���������Ժ�Ǽ�")]
        public CheckState IsCanOutWhenUnFeeUOApply
        {
            get
            {
                return this.isCanOutWhenUnFeeUOApply;
            }
            set
            {
                isCanOutWhenUnFeeUOApply = value;
            }
        }

        /// <summary>
        /// Ƿ���Ƿ���������Ժ����
        /// </summary>
        private CheckState isCanOutWhenLackFee = CheckState.Yes;

        /// <summary>
        /// Ƿ���Ƿ���������Ժ����
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("Ƿ���Ƿ���������Ժ����")]
        public CheckState IsCanOutWhenLackFee
        {
            get
            {
                return this.isCanOutWhenLackFee;
            }
            set
            {
                isCanOutWhenLackFee = value;
            }
        }

        /// <summary>
        /// ����δ�շѵķ�ҩƷִ�е��Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenNoFeeExecUndrugOrder = CheckState.Check;

        /// <summary>
        /// ����δ�շѵķ�ҩƷִ�е��Ƿ���������Ժ�Ǽ�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("����δ�շѵķ�ҩƷִ�е��Ƿ���������Ժ�Ǽ�")]
        public CheckState IsCanOutWhenNoFeeExecUndrugOrder
        {
            get
            {
                return this.isCanOutWhenNoFeeExecUndrugOrder;
            }
            set
            {
                isCanOutWhenNoFeeExecUndrugOrder = value;
            }
        }

        #endregion

        #region ת������
        /// <summary>
        /// ת���Ƿ���Ը��Ĳ���
        /// </summary>
        [Category("ת������"), Description("ת���Ƿ���Ը��Ĳ���")]
        public bool IsAbleChangeInpatientArea
        {
            get;
            set;
        }
        #endregion
        /// <summary>
        /// �Ա��Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("�Ա��Ƿ������޸�")]
        public bool SexReadOnly
        {
            get
            {
                return sexReadOnly;
            }
            set
            {
                sexReadOnly = value;
            }
        }
        /// <summary>
        /// �����Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("�����Ƿ������޸�")]
        public bool BirthdayReadOnly
        {
            get
            {
                return birthdayReadOnly;
            }
            set
            {
                birthdayReadOnly = value;
            }
        }
        /// <summary>
        /// ����Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("����Ƿ������޸�")]
        public bool HeightReadOnly
        {
            get
            {
                return heightReadOnly;
            }
            set
            {
                heightReadOnly = value;
            }
        }
        /// <summary>
        /// �����Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("�����Ƿ������޸�")]
        public bool WeightReadOnly
        {
            get
            {
                return weightReadOnly;
            }
            set
            {
                weightReadOnly = value;
            }
        }
        /// <summary>
        /// ���֤���Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("���֤���Ƿ������޸�")]
        public bool IDReadOnly
        {
            get
            {
                return iDReadOnly;
            }
            set
            {
                iDReadOnly = value;
            }
        }
        /// <summary>
        /// ְҵ�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("ְҵ�Ƿ������޸�")]
        public bool ProfessionReadOnly
        {
            get
            {
                return professionReadOnly;
            }
            set
            {
                professionReadOnly = value;
            }
        }
        /// <summary>
        /// ְҵ�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("�����Ƿ������޸�")]
        public bool MarryReadOnly
        {
            get
            {
                return marryReadOnly;
            }
            set
            {
                marryReadOnly = value;
            }
        }
        /// <summary>
        /// ��ͥסַ�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ͥסַ�Ƿ������޸�")]
        public bool HomeAddrReadOnly
        {
            get
            {
                return homeAddrReadOnly;
            }
            set
            {
                homeAddrReadOnly = value;
            }
        }
        /// <summary>
        /// ��ͥ�绰�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ͥ�绰�Ƿ������޸�")]
        public bool HomeTelReadOnly
        {
            get
            {
                return homeTelReadOnly;
            }
            set
            {
                homeTelReadOnly = value;
            }
        }
        /// <summary>
        /// ������λ�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("������λ�Ƿ������޸�")]
        public bool WorkReadOnly
        {
            get
            {
                return workReadOnly;
            }
            set
            {
                workReadOnly = value;
            }
        }
        /// <summary>
        /// ��ϵ���Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ϵ���Ƿ������޸�")]
        public bool LinkManReadOnly
        {
            get
            {
                return linkManReadOnly;
            }
            set
            {
                linkManReadOnly = value;
            }
        }
        /// <summary>
        /// ��ϵ�˵�ַ�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ϵ�˵�ַ�Ƿ������޸�")]
        public bool KinAddressReadOnly
        {
            get
            {
                return kinAddressReadOnly;
            }
            set
            {
                kinAddressReadOnly = value;
            }
        }
        /// <summary>
        /// ��ϵ�˵绰�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ϵ�˵绰�Ƿ������޸�")]
        public bool LinkTelReadOnly
        {
            get
            {
                return linkTelReadOnly;
            }
            set
            {
                linkTelReadOnly = value;
            }
        }
        /// <summary>
        /// ��ϵ�˹�ϵ�Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ϵ�˹�ϵ�Ƿ������޸�")]
        public bool RelationReadOnly
        {
            get
            {
                return relationReadOnly;
            }
            set
            {
                relationReadOnly = value;
            }
        }
        /// <summary>
        /// ��ע�����Ƿ������޸�
        /// </summary>
        [Category("���߻�����Ϣ"), Description("��ע�����Ƿ������޸�")]
        public bool MemoReadOnly
        {
            get
            {
                return memoReadOnly;
            }
            set
            {
                memoReadOnly = value;
            }
        }

        #region ��������

        /// <summary>
        /// סԺҽʦ�Ƿ����
        /// </summary>
        bool isAdmittingDoctorNeed = false;

        /// <summary>
        /// סԺҽʦ�Ƿ����
        /// </summary>
        [Category("��������"), Description("סԺҽʦ�Ƿ����")]
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
        [Category("��������"), Description("����ҽʦ�Ƿ����")]
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
        [Category("��������"), Description("����ҽʦ�Ƿ����")]
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
        [Category("��������"), Description("�������Ƿ����")]
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
        [Category("��������"), Description("���λ�ʿ�Ƿ����")]
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

        #endregion

        /// <summary>
        /// �Ƿ������޸Ĵ�λ�ȼ�
        /// </summary>
        private bool isAllowModifyBedGrad = false;

        /// <summary>
        /// �Ƿ������޸Ĵ�λ�ȼ�
        /// </summary>
        [Category("��������"), Description("�Ƿ������޸Ĵ�λ�ȼ�")]
        public bool IsAllowModifyBedGrad
        {
            get
            {
                return isAllowModifyBedGrad;
            }
            set
            {
                isAllowModifyBedGrad = value;
            }
        }

        [Category("tabҳ����"), Description("tpLeave��ٹ����Ƿ���ʾ")]
        public bool ��ٹ����Ƿ���ʾ
        {
            get
            {
                return tpLeaveVisible;
            }
            set
            {
                tpLeaveVisible = value;
            }
        }

        [Category("tabҳ����"), Description("tpNurseӤ���Ǽǹ����Ƿ���ʾ")]
        public bool Ӥ���Ǽ��Ƿ���ʾ
        {
            get
            {
                return tpNurseVisible;
            }
            set
            {
                tpNurseVisible = value;
            }
        }
        // {1292AA34-6E62-4f01-9BE7-EAE667D34E5D}
        [Category("tabҳ����"), Description("�Ƿ�ֻ��ʾӤ���Ǽ�")]
        public bool �Ƿ�ֻ��ʾӤ���Ǽ�
        {
            get
            {
                return this.tpOnlyBabyVisible;
            }
            set
            {
                tpOnlyBabyVisible = value;
            }
        }
        /// ��ֹ����Ǽ��Ƿ���ʾ
        /// </summary>
        [Category("tabҳ����"), Description("tpStopPregnancy��ֹ����Ǽ��Ƿ���ʾ")]
        public bool IsStopPregnancyVisible
        {
            get { return isStopPregnancyVisible; }
            set { isStopPregnancyVisible = value; }
        }

        #region {9A2D53D3-25BE-4630-A547-A121C71FB1C5}

        [Category("tabҳ����"), Description("ת���������Ƿ���ʾ")]
        public bool ת�����Ƿ���ʾ
        {
            get
            {
                return this.tpShiftNurseCellVisible;
            }
            set
            {
                this.tpShiftNurseCellVisible = value;
            }
        }

        #endregion

        #region ת����ά������

        /// <summary>
        /// ����δ��˵�ҽ���Ƿ��������ת����// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
        /// </summary>
        private CheckState isAllowWhenUnConfirmOrder = CheckState.Check;

        /// <summary>
        /// ����δ��˵�ҽ���Ƿ��������ת����// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
        /// </summary>
        [Category("ת��������"), Description("����δ��˵�ҽ���Ƿ��������ת����")]
        public CheckState IsAllowWhenUnConfirmOrder
        {
            get
            {
                return isAllowWhenUnConfirmOrder;
            }
            set
            {
                isAllowWhenUnConfirmOrder = value;
            }
        }
        #endregion
        #region ��λά������

        /// <summary>
        /// �Ƿ�����Ӵ�
        /// </summary>
        private bool isAllowAddBed = true;

        /// <summary>
        /// �Ƿ�����Ӵ�
        /// </summary>
        [Category("��λά������"), Description("�Ƿ�����Ӵ�")]
        public bool IsAllowAddBed
        {
            get
            {
                return isAllowAddBed;
            }
            set
            {
                isAllowAddBed = value;
            }
        }

        /// <summary>
        /// �Ѿ����ڵĴ����Ƿ������޸Ĵ���(ֻ��Կմ���
        /// </summary>
        private bool isAllowEditBedNo = false;

        /// <summary>
        /// �Ѿ����ڵĴ����Ƿ������޸Ĵ���
        /// </summary>
        [Category("��λά������"), Description("�Ѿ����ڵĴ����Ƿ������޸Ĵ���(ֻ��Կմ���")]
        public bool IsAllowEditBedNo
        {
            get
            {
                return isAllowEditBedNo;
            }
            set
            {
                isAllowEditBedNo = value;
            }
        }


        /// <summary>
        /// �Ƿ�����������д�λ�ȼ�
        /// </summary>
        private bool isAllowAddAllBedLevel = false;

        /// <summary>
        /// �Ƿ�����������д�λ�ȼ�
        /// </summary>
        [Category("��λά������"), Description("�Ƿ�����������д�λ�ȼ�")]
        public bool IsAllowAddAllBedLevel
        {
            get
            {
                return isAllowAddAllBedLevel;
            }
            set
            {
                isAllowAddAllBedLevel = value;
            }
        }
        /// <summary>
        /// �Ƿ������޸Ĵ�λ�ȼ�
        /// </summary>
        private bool isAllowEditBedLevel = false;

        /// <summary>
        /// �Ƿ������޸Ĵ�λ�ȼ�
        /// </summary>
        [Category("��λά������"), Description("�Ƿ������޸Ĵ�λ�ȼ�")]
        public bool IsAllowEditBedLevel
        {
            get
            {
                return isAllowEditBedLevel;
            }
            set
            {
                isAllowEditBedLevel = value;
            }
        }

        /// <summary>
        /// �Ƿ�����������д�λ����
        /// </summary>
        private bool isAllBedWave = false;

        /// <summary>
        /// �Ƿ�����������д�λ����
        /// </summary>
        [Category("��λά������"), Description("�Ƿ����������д�λ���Ʋ���")]
        public bool IsAllBedWave
        {
            get
            {
                return this.isAllBedWave;
            }
            set
            {
                this.isAllBedWave = value;
            }
        }

        /// <summary>
        /// ������������ͣ�0 ȫ�������������1 ȫ�����������2 ҽ�������Ѳ����������Ĭ��1
        /// </summary>
        private int wapType = 1;

        /// <summary>
        /// ������������ͣ�0 ȫ�������������1 ȫ�����������2 ҽ�������Ѳ����������Ĭ��1
        /// </summary>
        [Category("��λά������"), Description("������������ͣ�0 ȫ�������������1 ȫ�����������2 ҽ�������Ѳ����������Ĭ��1")]
        public int WapType
        {
            get
            {
                return wapType;
            }
            set
            {
                wapType = value;
            }
        }

        /// <summary>
        /// �Ƿ�ֻ����մ��ɽ���
        /// </summary>
        private bool isOnlyEmptyBedCanReceive = false;

        /// <summary>
        /// �Ƿ�ֻ����մ��ɽ���
        /// </summary>
        [Category("��λά������"), Description("�Ƿ�ֻ����մ��ɽ���")]
        public bool IsOnlyEmptyBedCanReceive
        {
            set { this.isOnlyEmptyBedCanReceive = value; }
            get { return this.isOnlyEmptyBedCanReceive; }
        }
        #endregion

        /// <summary>
        /// �Ƿ��Ժ�ǼǱ������ʾ��ӡ��Ժ֪ͨ��
        /// </summary>
        private bool isPrintOutNote = false;

        /// <summary>
        /// �Ƿ��Ժ�Ǽ��Զ�ֹͣҽ��
        /// </summary>
        private bool isAutoDcOrder = false;

        /// <summary>
        /// �Ƿ��Ժ�Ǽ��Զ�ֹͣҽ��
        /// </summary>
        [Category("��Ժ����"), Description("�Ƿ��Ժ�Ǽ��Զ�ֹͣҽ��")]
        public bool IsAutoDcOrder
        {
            get
            {
                return isAutoDcOrder;
            }
            set
            {
                isAutoDcOrder = value;
            }
        }

        /// <summary>
        /// �Ƿ�ת���Զ�ֹͣҽ��
        /// </summary>
        private bool isShiftAutoDcOrder = false;

        /// <summary>
        /// �Ƿ�ת���Զ�ֹͣҽ��
        /// </summary>
        [Category("��Ժ����"), Description("�Ƿ�ת���Զ�ֹͣҽ��")]
        public bool IsShiftAutoDcOrder
        {
            get
            {
                return isShiftAutoDcOrder;
            }
            set
            {
                isShiftAutoDcOrder = value;
            }
        }

        #region ת����������


        /// <summary>
        /// ����δ�շѵ��������뵥�Ƿ��������
        /// </summary>
        /// {566f98c2-5ac4-49b3-940b-fdebad959229} ���ת�Ƶ��������ƿؼ����ԡ�
        private CheckState isCanShiftWhenUnFeeUOApply = CheckState.Check;

        /// <summary>
        /// ����δ�շѵ��������뵥�Ƿ��������
        /// </summary>
        /// {566f98c2-5ac4-49b3-940b-fdebad959229} ���ת�Ƶ��������ƿؼ����ԡ�
        [Category("ת��"), Description("����δ�շѵ��������뵥�Ƿ��������Ĭ��ΪУ�顣"), DefaultValue(CheckState.Check)]
        public CheckState IsCanShiftWhenUnFeeUOApply
        {
            get
            {
                return this.isCanShiftWhenUnFeeUOApply;
            }
            set
            {
                isCanShiftWhenUnFeeUOApply = value;
            }
        }

        /// <summary>
        /// �����˷������Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenQuitFeeApplay = CheckState.No;

        /// <summary>
        /// ���˷������Ƿ�����ת������
        /// </summary>
        [Category("ת������"), Description("�����˷������Ƿ�������ת������")]
        public CheckState IsCanShiftWhenQuitFeeApplay
        {
            get
            {
                return isCanShiftWhenQuitFeeApplay;
            }
            set
            {
                isCanShiftWhenQuitFeeApplay = value;
            }
        }

        /// <summary>
        /// ������ҩ�����Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenQuitDrugApplay = CheckState.No;

        /// <summary>
        /// ������ҩ�����Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("������ҩ�����Ƿ�������ת������")]
        public CheckState IsCanShiftWhenQuitDrugApplay
        {
            get
            {
                return isCanShiftWhenQuitDrugApplay;
            }
            set
            {
                isCanShiftWhenQuitDrugApplay = value;
            }
        }

        /// <summary>
        /// ���ڷ�ҩ�����Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenDrugApplay = CheckState.Check;

        /// <summary>
        /// ���ڷ�ҩ�����Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("���ڷ�ҩ�����Ƿ�������ת������")]
        public CheckState IsCanShiftWhenDrugApplay
        {
            get
            {
                return this.isCanShiftWhenDrugApplay;
            }
            set
            {
                this.isCanShiftWhenDrugApplay = value;
            }
        }

        /// <summary>
        /// ����δȷ����Ŀ�Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenUnConfirm = CheckState.Check;

        /// <summary>
        /// ����δȷ����Ŀ�Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("����δȷ����Ŀ�Ƿ�������ת������")]
        public CheckState IsCanShiftWhenUnConfirm
        {
            get
            {
                return this.isCanShiftWhenUnConfirm;
            }
            set
            {
                this.isCanShiftWhenUnConfirm = value;
            }
        }

        /// <summary>
        /// δ����ת��ҽ���Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenNoOutOrder = CheckState.No;

        /// <summary>
        /// δ����ת��ҽ���Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("δ����ת��ҽ���Ƿ�������ת������")]
        public CheckState IsCanShiftWhenNoOutOrder
        {
            get
            {
                return this.isCanShiftWhenNoOutOrder;
            }
            set
            {
                this.isCanShiftWhenNoOutOrder = value;
            }
        }

        /// <summary>
        /// δȫ��ֹͣ�����Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenNoDcOrder = CheckState.No;

        /// <summary>
        /// δȫ��ֹͣ�����Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("δȫ��ֹͣ�����Ƿ�������ת������")]
        public CheckState IsCanShiftWhenNoDcOrder
        {
            get
            {
                return this.isCanShiftWhenNoDcOrder;
            }
            set
            {
                this.isCanShiftWhenNoDcOrder = value;
            }
        }

        /// <summary>
        /// ����δ���ҽ���Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenUnConfirmOrder = CheckState.Check;

        /// <summary>
        /// ����δ���ҽ���Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("����δ���ҽ���Ƿ�������ת������")]
        public CheckState IsCanShiftWhenUnConfirmOrder
        {
            get
            {
                return this.isCanShiftWhenUnConfirmOrder;
            }
            set
            {
                this.isCanShiftWhenUnConfirmOrder = value;
            }
        }

        /// <summary>
        /// ����δ�շѵķ�ҩƷִ�е��Ƿ�������ת������
        /// </summary>
        private CheckState isCanShiftWhenNoFeeExecUndrugOrder = CheckState.Check;

        /// <summary>
        /// ����δ�շѵķ�ҩƷִ�е��Ƿ�������ת������
        /// </summary>
        [Category("ת������"), Description("����δ�շѵķ�ҩƷִ�е��Ƿ�������ת������")]
        public CheckState IsCanShiftWhenNoFeeExecUndrugOrder
        {
            get
            {
                return this.isCanShiftWhenNoFeeExecUndrugOrder;
            }
            set
            {
                isCanShiftWhenNoFeeExecUndrugOrder = value;
            }
        }

        /// <summary>
        /// ����δ��ɵĻ��������Ƿ��������ת������
        /// </summary>
        private CheckState isCanShiftWhenUnConsultation = CheckState.No;

        /// <summary>
        /// ����δ��ɵĻ��������Ƿ��������ת������
        /// </summary>
        [Category("ת������"), Description("����δ��ɵĻ��������Ƿ��������ת������")]
        public CheckState IsCanShiftWhenUnConsultation
        {
            get
            {
                return this.isCanShiftWhenUnConsultation;
            }
            set
            {
                isCanShiftWhenUnConsultation = value;
            }
        }

        /// <summary>
        /// Ƿ���Ƿ��������ת������
        /// </summary>
        private CheckState isCanShiftWhenLackFee = CheckState.Check;

        /// <summary>
        /// Ƿ���Ƿ��������ת������
        /// </summary>
        [Category("ת������"), Description("Ƿ���Ƿ��������ת������")]
        public CheckState IsCanShiftWhenLackFee
        {
            get
            {
                return this.isCanShiftWhenLackFee;
            }
            set
            {
                isCanShiftWhenLackFee = value;
            }
        }

        #endregion

        /// <summary>
        /// �Ƿ�ʹ��ת���Զ�ֹͣҽ������
        /// </summary>
        private bool isUseShiftAutoDcOrder = false;

        /// <summary>
        /// �Ƿ�ʹ��ת���Զ�ֹͣҽ������
        /// </summary>
        [Category("��Ժ����"), Description("�Ƿ�ʹ��ת���Զ�ֹͣҽ�����ܣ�ʹ�ú���뿪��ת��ҽ������ת�ƣ�")]
        public bool IsUseShiftAutoDcOrder
        {
            get
            {
                return isUseShiftAutoDcOrder;
            }
            set
            {
                isUseShiftAutoDcOrder = value;
            }
        }

        /// <summary>
        /// �Ƿ��Ժ�ǼǱ������ʾ��ӡ��Ժ֪ͨ��
        /// </summary>
        [Category("��Ժ����"), Description("�Ƿ��Ժ�ǼǱ������ʾ��ӡ��Ժ֪ͨ��")]
        public bool IsPrintOutNote
        {
            get
            {
                return isPrintOutNote;
            }
            set
            {
                isPrintOutNote = value;
            }
        }

        /// <summary>
        /// �Ƿ�ҽ����дת�飬Ȼ��ʿ�����Ժ
        /// </summary>
        private bool isDoctZG = false;

        /// <summary>
        /// �Ƿ�ҽ����дת�飬Ȼ��ʿ�����Ժ
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("�Ƿ�ҽ����дת�飬Ȼ��ʿ�����Ժ?")]
        public bool IsDoctZG
        {
            get
            {
                return isDoctZG;
            }
            set
            {
                isDoctZG = value;
            }
        }

        private decimal autoCost = 0;
        /// <summary>
        /// ��Ժ���ƽ���ж�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("��Ժ���ƽ���ж�")]
        public decimal AutoCost
        {
            get
            {
                return autoCost;
            }
            set
            {
                autoCost = value;
            }
        }

        /// <summary>
        /// ��Ժ�Զ�ֹͣ������ֹͣҽ��
        /// </summary>
        private AotuDcDoct autoDcDoct = AotuDcDoct.ExecutOutDoct;

        /// <summary>
        /// ��Ժ�Զ�ֹͣ������ֹͣҽ��
        /// </summary>
        [Category("��Ժ����"), Description("��Ժ�Զ�ֹͣ������ֹͣҽ����InpatientDoct��סԺҽʦ��DirectorDoct������ҽʦ��ExecutOutDoct��������Ժҽ����ҽ����")]
        public AotuDcDoct AutoDcDoct
        {
            get
            {
                return autoDcDoct;
            }
            set
            {
                autoDcDoct = value;
            }
        }

        /// <summary>
        /// �Ƿ���������ٻ�
        /// </summary>
        private bool isAllowCallBackOtherDay = false;

        /// <summary>
        /// �Ƿ���������ٻ�
        /// </summary>
        [Category("��Ժ����"), Description("�Ƿ���������ٻ�")]
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

        #region {F4EB69FA-F43E-4bdc-AC85-B53377604818}
        private bool isModifyBirthday = true;
        /// <summary>
        /// �Ƿ�����޸�Ӥ������ʱ��
        /// </summary>
        [Category("Ӥ���Ǽ�����"), Description("�Ƿ�����޸�Ӥ������ʱ��"), DefaultValue(true)]
        public bool IsModifyBirthday
        {
            get
            {
                return this.isModifyBirthday;
            }
            set
            {
                this.isModifyBirthday = value;
            }
        }
        #endregion

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            this.neuTabControl1.TabPages.Clear();
            this.neuTabControl1.TabPages.Add(this.tbBedView);//Ĭ����ʾ����
            ucBedListView uc = new ucBedListView();
            uc.IsOnlyEmptyBedCanReceive = isOnlyEmptyBedCanReceive;
            uc.WapType = wapType;
            uc.arrangeBed += new ucBedListView.ArrangeBed(uc_arrangeBed);
            uc.ListViewItemChanged += new ListViewItemSelectionChangedEventHandler(uc_ListViewItemChanged);
            uc.Dock = DockStyle.Fill;
            uc.Visible = true;
            FS.FrameWork.WinForms.Forms.IControlable ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
            if (ic != null)
            {
                ic.Init(this.tv, null, null);
                ic.SetValue(patient, this.tv.Nodes[0]);
                ic.RefreshTree += new EventHandler(ic_RefreshTree);
                ic.SendParamToControl += new FS.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo += new FS.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

            }
            this.tbBedView.Controls.Add(uc);
            base.OnLoad(e);
        }

        /// <summary>
        /// ��û���
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            string txtNode = "";

            if (e.Tag.GetType() != typeof(FS.HISFC.Models.RADT.PatientInfo))
            {
                //���һ�����ڵ�
                txtNode = e.Tag.ToString();
                //��ʾ��������һ����
                type = EnumPatientType.DeptOrTend;
            }
            else
            {
                //����(����)�ڵ�
                txtNode = e.Parent.Tag.ToString();
                node = e;
                patient = e.Tag as FS.HISFC.Models.RADT.PatientInfo;

                //���ݽڵ����͵Ĳ�ͬ,��ʾ��ͬ������
                if (txtNode == EnumPatientType.In.ToString())
                {
                    type = EnumPatientType.In;
                    arrangeBedNO = null;
                }
                else if (txtNode == EnumPatientType.Arrive.ToString())
                {
                    type = EnumPatientType.Arrive;
                }
                else if (txtNode == EnumPatientType.ShiftIn.ToString())
                {
                    type = EnumPatientType.ShiftIn;
                }
                else if (txtNode == EnumPatientType.ShiftOut.ToString())
                {
                    type = EnumPatientType.ShiftOut;
                    arrangeBedNO = null;
                }
                else if (txtNode == EnumPatientType.Out.ToString())
                {
                    type = EnumPatientType.Out;
                    arrangeBedNO = null;
                }
                else
                {
                    type = EnumPatientType.In;
                    arrangeBedNO = null;
                }
            }

            #region �ɵ�����

            /*
            //����ѡ�еĽڵ��κ����Ͳ�ͬ,��ʾ��ͬ������
            if (e.Parent == null)
            {
                //һ���ڵ�
                txtNode = e.Tag.ToString();
                //��ʾ��������һ����
                type = EnumPatientType.Dept;
            }
            else
            {
                //����(����)�ڵ�
                txtNode = e.Parent.Tag.ToString();

                //���ݽڵ����͵Ĳ�ͬ,��ʾ��ͬ������
                if (txtNode == EnumPatientType.In.ToString())
                {
                    type = EnumPatientType.In;
                    arrangeBedNO = null;
                }
                else if (txtNode == EnumPatientType.Arrive.ToString())
                {
                    type = EnumPatientType.Arrive;
                }
                else if (txtNode == EnumPatientType.ShiftIn.ToString())
                {
                    type = EnumPatientType.ShiftIn;
                }
                else if (txtNode == EnumPatientType.ShiftOut.ToString())
                {
                    type = EnumPatientType.ShiftOut;
                    arrangeBedNO = null;
                }
                else if (txtNode == EnumPatientType.Out.ToString())
                {
                    type = EnumPatientType.Out;
                    arrangeBedNO = null;
                }
                else
                {
                    type = EnumPatientType.In;
                    arrangeBedNO = null;
                }
                node = e;
                patient = e.Tag as FS.HISFC.Models.RADT.PatientInfo;
            }
             * */
            #endregion

            this.neuTabControl1_SelectedIndexChanged(null, null);
            return base.OnSetValue(neuObject, e);
        }

        private EnumPatientType mytype = EnumPatientType.DeptOrTend;
        /// <summary>
        /// ����
        /// </summary>
        protected EnumPatientType type
        {
            get
            {
                return mytype;
            }
            set
            {
                if (mytype == value)
                    return;
                mytype = value;
                try
                {
                    this.neuTabControl1.TabPages.Clear();
                }
                catch { };
                if (mytype == EnumPatientType.DeptOrTend)
                {

                    this.neuTabControl1.TabPages.Add(this.tbBedView);
                }
                else if (mytype == EnumPatientType.In)
                {
                    if (!tpOnlyBabyVisible)// {1292AA34-6E62-4f01-9BE7-EAE667D34E5D}
                    {
                        this.neuTabControl1.TabPages.Add(this.tpPatient);
                        this.neuTabControl1.TabPages.Add(this.tpDept);
                        this.neuTabControl1.TabPages.Add(this.tpChangeDoc);
                        this.neuTabControl1.TabPages.Add(this.tpOut);
                        this.neuTabControl1.TabPages.Add(this.tpCancelArrive);
                    }

                    if (this.tpShiftNurseCellVisible)
                    {
                        this.neuTabControl1.TabPages.Add(this.tpShiftNurseCell);
                    }

                    if (tpLeaveVisible)
                    {
                        this.neuTabControl1.TabPages.Add(this.tpLeave);
                    }
                    if (tpNurseVisible)
                    {
                        #region ��������ʾӤ���Ǽ�

                        if (babyDeptHelper == null)
                        {
                            FS.HISFC.BizProcess.Integrate.Manager constManager = new FS.HISFC.BizProcess.Integrate.Manager();
                            System.Collections.ArrayList al = constManager.GetConstantList("USEBABYDEPT");
                            babyDeptHelper = new FS.FrameWork.Public.ObjectHelper(al);
                        }

                        bool isShowBaby = false;
                        FS.HISFC.Models.Base.Employee myoper = (FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator;
                        if (babyDeptHelper.GetObjectFromID(myoper.Dept.ID) != null
                            || babyDeptHelper.GetObjectFromID(myoper.Nurse.ID) != null)
                        {
                            isShowBaby = true;
                        }

                        //foreach (FS.HISFC.Models.Base.Const obj in al)
                        //{
                        //    if (obj.ID == myoper.Dept.ID || obj.ID == myoper.Nurse.ID)
                        //    {
                        //        isShowBaby = true;
                        //        break;
                        //    }
                        //}
                        if (isShowBaby)
                        {
                            this.neuTabControl1.TabPages.Add(this.tpNurse);
                        }
                        #endregion
                    }
                    if (isStopPregnancyVisible)
                    {
                        this.neuTabControl1.TabPages.Add(this.tpStopPregnancy);
                    }

                    if (isShowPatientBed)
                    {
                        this.neuTabControl1.TabPages.Add(this.tpPatientBed);
                    }
                }
                else if (mytype == EnumPatientType.Out)
                {

                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.neuTabControl1.TabPages.Add(this.tpCallBack);
                    //{5DDFB29F-29B7-4963-9E5F-3110B304307A} Ĭ��ѡ�й��ܲ˵� 20100915
                    this.neuTabControl1.SelectedTab = this.tpCallBack;
                }
                else if (mytype == EnumPatientType.Arrive)
                {

                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.neuTabControl1.TabPages.Add(this.tpArrive);
                    //{5DDFB29F-29B7-4963-9E5F-3110B304307A} Ĭ��ѡ�й��ܲ˵� 20100915
                    this.neuTabControl1.SelectedTab = this.tpArrive;
                }
                else if (mytype == EnumPatientType.ShiftIn)
                {

                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    this.neuTabControl1.TabPages.Add(this.tpArrive);
                    //{5DDFB29F-29B7-4963-9E5F-3110B304307A} Ĭ��ѡ�й��ܲ˵� 20100915
                    this.neuTabControl1.SelectedTab = this.tpArrive;
                }
                else if (mytype == EnumPatientType.ShiftOut)
                {
                    this.neuTabControl1.TabPages.Add(this.tpPatient);
                    #region {81987883-BFB0-42f7-8B99-CF44CA44BDDA}
                    if (this.ת�����Ƿ���ʾ)
                    {
                        if (patient != null)
                        {
                            FS.HISFC.Models.RADT.Location newLocation = new FS.HISFC.Models.RADT.Location();
                            newLocation = this.inpatientManager.QueryShiftNewLocation(this.patient.ID, this.patient.PVisit.PatientLocation.Dept.ID);


                            if (newLocation.Dept.ID == "")
                            {
                                if (newLocation.NurseCell.ID != "")
                                {
                                    this.neuTabControl1.TabPages.Add(this.tpCancelNurseCell);
                                    return;
                                }
                            }
                        }
                    }
                    this.neuTabControl1.TabPages.Add(this.tpCancelDept);

                    #endregion
                }
            }
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (neuTabControl1.SelectedTab == null)
            {
                return;
            }

            //tabControl Selected Changed
            FS.FrameWork.WinForms.Forms.IControlable ic = null;
            if (this.neuTabControl1.SelectedTab == this.tbBedView)//��λһ��
            {
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBedListView uc = new ucBedListView();
                    uc.WapType = wapType;
                    uc.ListViewItemChanged += new ListViewItemSelectionChangedEventHandler(uc_ListViewItemChanged);
                    uc.arrangeBed += new ucBedListView.ArrangeBed(uc_arrangeBed);
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }
            else if (this.neuTabControl1.SelectedTab == this.tpArrive)//����
            {
                #region ����
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBasePatientArrive uc = new ucBasePatientArrive();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.ArrangeBedNO = this.arrangeBedNO;

                    uc.IsAdmittingDoctorNeed = this.isAdmittingDoctorNeed;
                    uc.IsAttendingDoctorNeed = this.isAttendingDoctorNeed;
                    uc.IsConsultingDoctorNeed = this.isConsultingDoctorNeed;
                    uc.IsDirectorDoctorNeed = this.isDirectorDoctorNeed;
                    uc.IsAdmittingNurseNeed = this.isAdmittingNurseNeed;

                    uc.IsAddDoctWhenArrive = this.isAddDoctWhenArrive;
                    uc.IsAllowModifyBedGrad = this.isAllowModifyBedGrad;

                    uc.IsAddAllDoct = this.isAddAllDoct;
                    uc.IsCanEditArriveInTime = this.isCanEditArriveInTime;// {3B757263-9BE5-4e5a-9AD2-0815E9A210C7}
                    uc.IsAddAllNurse = this.isAddAllNurse;

                    if (this.node.Parent != null && this.node.Parent.Tag.ToString() == "ShiftIn")
                    {
                        uc.Arrivetype = EnumArriveType.ShiftIn;

                    }
                    else
                    {
                        uc.Arrivetype = EnumArriveType.Accepts;

                    }

                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);

                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                    ucBasePatientArrive uc = ic as ucBasePatientArrive;
                    uc.ArrangeBedNO = this.arrangeBedNO;

                    uc.IsAdmittingDoctorNeed = this.isAdmittingDoctorNeed;
                    uc.IsAttendingDoctorNeed = this.isAttendingDoctorNeed;
                    uc.IsConsultingDoctorNeed = this.isConsultingDoctorNeed;
                    uc.IsDirectorDoctorNeed = this.isDirectorDoctorNeed;
                    uc.IsAdmittingNurseNeed = this.isAdmittingNurseNeed;

                    uc.IsAddDoctWhenArrive = this.isAddDoctWhenArrive;
                    uc.IsAllowModifyBedGrad = this.isAllowModifyBedGrad;


                    uc.IsAddAllDoct = this.isAddAllDoct;
                    uc.IsAddAllNurse = this.isAddAllNurse;

                    if (this.node.Parent != null && this.node.Parent.Tag.ToString() == "ShiftIn")
                    {
                        uc.Arrivetype = EnumArriveType.ShiftIn;
                    }
                    else
                    {
                        uc.Arrivetype = EnumArriveType.Accepts;

                    }

                }
                //this.neuTabControl1.TabPages.Add(this.tpcancelArrive);//////���ȡ�������ǩ������ҳ�棿��
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpCancelArrive)//ȡ������
            {
                #region ȡ������
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucCancelArrive uc = new ucCancelArrive();

                    if (uc == null)
                    {
                        ucPatientOut ucDefault = new ucPatientOut();

                        ucDefault.Dock = DockStyle.Fill;
                        ucDefault.Visible = true;


                        ic = ucDefault as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                        this.neuTabControl1.SelectedTab.Controls.Add(ucDefault);
                    }
                    else
                    {
                        ((System.Windows.Forms.UserControl)uc).Dock = DockStyle.Fill;
                        ((System.Windows.Forms.UserControl)uc).Visible = true;
                        ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add((System.Windows.Forms.UserControl)uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpCallBack)//�һ�
            {
                #region �ٻ�
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    FS.HISFC.BizProcess.Interface.ICallBackPatient uc = null;
                    uc = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.ICallBackPatient)) as FS.HISFC.BizProcess.Interface.ICallBackPatient;
                    if (uc == null)
                    {
                        ucBasePatientArrive defaultuc = new ucBasePatientArrive();
                        defaultuc.Dock = DockStyle.Fill;
                        defaultuc.Visible = true;
                        defaultuc.Arrivetype = EnumArriveType.CallBack;
                        defaultuc.IsAllowCallBackOtherDay = this.isAllowCallBackOtherDay;

                        defaultuc.IsAdmittingDoctorNeed = this.isAdmittingDoctorNeed;
                        defaultuc.IsAttendingDoctorNeed = this.isAttendingDoctorNeed;
                        defaultuc.IsConsultingDoctorNeed = this.isConsultingDoctorNeed;
                        defaultuc.IsDirectorDoctorNeed = this.isDirectorDoctorNeed;
                        defaultuc.IsAdmittingNurseNeed = this.isAdmittingNurseNeed;

                        defaultuc.IsAddDoctWhenArrive = this.isAddDoctWhenArrive;
                        defaultuc.IsAllowModifyBedGrad = this.isAllowModifyBedGrad;


                        defaultuc.IsAddAllDoct = this.isAddAllDoct;
                        defaultuc.IsAddAllNurse = this.isAddAllNurse;

                        ic = defaultuc as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                        this.neuTabControl1.SelectedTab.Controls.Add((FS.FrameWork.WinForms.Controls.ucBaseControl)defaultuc);
                    }
                    else
                    {
                        ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                        this.neuTabControl1.SelectedTab.Controls.Add((FS.FrameWork.WinForms.Controls.ucBaseControl)uc);
                    }
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpCancelDept)//ȡ��ת��
            {
                #region ȡ��ת��
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientShiftOut uc = new ucPatientShiftOut();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.IsCancel = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpChangeDoc)//��ҽ��
            {
                #region ��ҽ��
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBasePatientArrive uc = new ucBasePatientArrive();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.Arrivetype = EnumArriveType.ChangeDoct;

                    uc.IsAdmittingDoctorNeed = this.isAdmittingDoctorNeed;
                    uc.IsAttendingDoctorNeed = this.isAttendingDoctorNeed;
                    uc.IsConsultingDoctorNeed = this.isConsultingDoctorNeed;
                    uc.IsDirectorDoctorNeed = this.isDirectorDoctorNeed;
                    uc.IsAdmittingNurseNeed = this.isAdmittingNurseNeed;

                    uc.IsAddDoctWhenArrive = this.isAddDoctWhenArrive;
                    uc.IsAllowModifyBedGrad = this.isAllowModifyBedGrad;

                    uc.IsAddAllDoct = this.isAddAllDoct;
                    uc.IsAddAllNurse = this.isAddAllNurse;

                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpDept)//������
            {
                #region ������
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientShiftOut ucPatientShiftOut = new ucPatientShiftOut();
                    ucPatientShiftOut.Dock = DockStyle.Fill;
                    ucPatientShiftOut.Visible = true;
                    ucPatientShiftOut.IsCancel = false;

                    ucPatientShiftOut.IsAbleChangeInpatientArea = this.IsAbleChangeInpatientArea;
                    ucPatientShiftOut.AutoDcDoct = this.autoDcDoct;
                    ucPatientShiftOut.IsAutoDcOrder = this.isShiftAutoDcOrder;
                    ucPatientShiftOut.IsUseShiftAutoDcOrder = this.isUseShiftAutoDcOrder;
                    // {566f98c2-5ac4-49b3-940b-fdebad959229} ���ת�Ƶ��������ƿؼ����ԡ�
                    ucPatientShiftOut.IsCanShiftWhenUnFeeUOApply = this.isCanShiftWhenUnFeeUOApply;

                    ucPatientShiftOut.IsCanShiftWhenQuitFeeApplay = this.isCanShiftWhenQuitFeeApplay;
                    ucPatientShiftOut.IsCanShiftWhenQuitDrugApplay = this.isCanShiftWhenQuitDrugApplay;
                    ucPatientShiftOut.IsCanShiftWhenDrugApplay = this.isCanShiftWhenDrugApplay;
                    ucPatientShiftOut.IsCanShiftWhenUnConfirm = this.isCanShiftWhenUnConfirm;
                    ucPatientShiftOut.IsCanShiftWhenNoOutOrder = this.isCanShiftWhenNoOutOrder;
                    ucPatientShiftOut.IsCanShiftWhenNoDcOrder = this.isCanShiftWhenNoDcOrder;
                    ucPatientShiftOut.IsCanShiftWhenUnConfirmOrder = this.isCanShiftWhenUnConfirmOrder;
                    ucPatientShiftOut.IsCanShiftWhenNoFeeExecUndrugOrder = this.isCanShiftWhenNoFeeExecUndrugOrder;
                    ucPatientShiftOut.IsCanShiftWhenUnConsultation = this.isCanShiftWhenUnConsultation;
                    ucPatientShiftOut.IsCanShiftWhenLackFee = this.isCanShiftWhenLackFee;

                    ic = ucPatientShiftOut as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(ucPatientShiftOut);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpLeave)//���
            {
                #region ���
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientLeave uc = new ucPatientLeave();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpNurse)//Ӥ���Ǽ�
            {
                #region Ӥ���Ǽ�
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucBabyInfo uc = new ucBabyInfo();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    #region {F4EB69FA-F43E-4bdc-AC85-B53377604818}
                    uc.IsModifyBirthday = this.isModifyBirthday;
                    #endregion
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpOut)//��Ժ�Ǽ�
            {
                #region ��Ժ�Ǽ�
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    //ucPatientOut uc = new ucPatientOut();

                    FS.HISFC.BizProcess.Interface.IucOutPatient uc = null;
                    uc = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IucOutPatient)) as FS.HISFC.BizProcess.Interface.IucOutPatient;
                    if (uc == null)
                    {
                        ucPatientOut ucPatientOut = new ucPatientOut();

                        ucPatientOut.Dock = DockStyle.Fill;
                        ucPatientOut.Visible = true;

                        ucPatientOut.IsPrintOutNote = this.isPrintOutNote;
                        ucPatientOut.AutoDcDoct = this.autoDcDoct;
                        ucPatientOut.IsAutoDcOrder = this.isAutoDcOrder;

                        ucPatientOut.IsCanOutWhenDrugApplay = this.isCanOutWhenDrugApplay;
                        ucPatientOut.IsCanOutWhenNoDcOrder = this.isCanOutWhenNoDcOrder;
                        ucPatientOut.IsCanOutWhenNoFeeExecUndrugOrder = this.isCanOutWhenNoFeeExecUndrugOrder;
                        ucPatientOut.IsCanOutWhenNoOutOrder = this.isCanOutWhenNoOutOrder;
                        ucPatientOut.IsCanOutWhenQuitDrugApplay = this.isCanOutWhenQuitDrugApplay;
                        ucPatientOut.IsCanOutWhenQuitFeeApplay = isCanOutWhenQuitFeeApplay;
                        ucPatientOut.IsCanOutWhenUnConfirm = isCanOutWhenUnConfirm;
                        ucPatientOut.IsCanOutWhenUnConfirmOrder = isCanOutWhenUnConfirmOrder;
                        ucPatientOut.IsCanOutWhenUnFeeUOApply = this.isCanOutWhenUnFeeUOApply;
                        ucPatientOut.IsCanOutWhenLackFee = this.isCanOutWhenLackFee;
                        ucPatientOut.IsCanOutWhenUnConsultation = this.isCanOutWhenUnConsultation;
                        ucPatientOut.IsCanOutWhenExecDrugLimit = this.isCanOutWhenExecDrugLimit;
                        ucPatientOut.IsDoctZG = this.isDoctZG;

                        ic = ucPatientOut as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                        this.neuTabControl1.SelectedTab.Controls.Add(ucPatientOut);
                    }
                    else
                    {
                        ((System.Windows.Forms.UserControl)uc).Dock = DockStyle.Fill;
                        ((System.Windows.Forms.UserControl)uc).Visible = true;
                        ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                        if (ic != null)
                        {
                            ic.Init(this.tv, null, null);
                        }
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add((System.Windows.Forms.UserControl)uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpPatient)//���߻�����Ϣ
            {
                #region ���߻�����Ϣ
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientInfo uc = new ucPatientInfo();
                    uc.SexReadOnly = sexReadOnly;
                    uc.BirthdayReadOnly = birthdayReadOnly;
                    uc.RelationReadOnly = relationReadOnly;
                    uc.HeightReadOnly = heightReadOnly;
                    uc.WeightReadOnly = weightReadOnly;
                    uc.IDReadOnly = iDReadOnly;
                    uc.ProfessionReadOnly = professionReadOnly;
                    uc.MarryReadOnly = marryReadOnly;
                    uc.HomeAddrReadOnly = homeAddrReadOnly;
                    uc.HomeTelReadOnly = homeTelReadOnly;
                    uc.WorkReadOnly = workReadOnly;
                    uc.LinkManReadOnly = linkManReadOnly;
                    uc.KinAddressReadOnly = kinAddressReadOnly;
                    uc.LinkTelReadOnly = linkTelReadOnly;
                    uc.MemoReadOnly = memoReadOnly;
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpShiftNurseCell)//{9A2D53D3-25BE-4630-A547-A121C71FB1C5}
            {
                #region ת����{9A2D53D3-25BE-4630-A547-A121C71FB1C5}
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientShiftNurseCell uc = new ucPatientShiftNurseCell();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.IsCancel = false;
                    uc.IsCanWhenUnConfirmOrder = this.IsAllowWhenUnConfirmOrder;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpCancelNurseCell)//{9A2D53D3-25BE-4630-A547-A121C71FB1C5}
            {
                #region ȡ��ת����{9A2D53D3-25BE-4630-A547-A121C71FB1C5}
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientShiftNurseCell uc = new ucPatientShiftNurseCell();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    uc.IsCancel = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpStopPregnancy)
            {
                #region ��ֹ����Ǽ�
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucStopPregnancy uc = new ucStopPregnancy();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else if (this.neuTabControl1.SelectedTab == this.tpPatientBed)
            {
                #region ��λ�ȼ���Ϣ
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    ucPatientBedGrade uc = new ucPatientBedGrade();
                    uc.Dock = DockStyle.Fill;
                    uc.Visible = true;
                    ic = uc as FS.FrameWork.WinForms.Forms.IControlable;
                    if (ic != null)
                    {
                        ic.Init(this.tv, null, null);
                    }
                    this.neuTabControl1.SelectedTab.Controls.Add(uc);
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
                #endregion
            }
            else
            {
                if (this.neuTabControl1.SelectedTab.Controls.Count == 0)
                {
                    return;
                }
                else
                {
                    ic = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Forms.IControlable;
                }
            }

            if (ic != null)
            {
                ic.SetValue(patient, node);
                ic.RefreshTree -= new EventHandler(ic_RefreshTree);
                ic.SendParamToControl -= new FS.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo -= new FS.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

                ic.RefreshTree += new EventHandler(ic_RefreshTree);
                ic.SendParamToControl += new FS.FrameWork.WinForms.Forms.SendParamToControlHandle(ic_SendParamToControl);
                ic.StatusBarInfo += new FS.FrameWork.WinForms.Forms.MessageEventHandle(ic_StatusBarInfo);

            }
        }

        void uc_arrangeBed(string bedNO)
        {
            this.arrangeBedNO = bedNO;
        }

        void uc_ListViewItemChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            FS.HISFC.Models.RADT.PatientInfo myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
            myPatientInfo = e.Item.Tag as FS.HISFC.Models.RADT.PatientInfo;
            if (myPatientInfo != null)
            {
                string strBedInfo = myPatientInfo.PVisit.PatientLocation.Bed.ID;
                strBedInfo = strBedInfo.Length > 4 ? strBedInfo.Substring(4) : strBedInfo;
                e.Item.ToolTipText = myPatientInfo.Name + "-��" + strBedInfo + "����-" + ((EnumBedState)e.Item.ImageIndex).ToString();
                base.OnStatusBarInfo(sender, myPatientInfo.Name + "-��" + strBedInfo + "����-" + ((EnumBedState)e.Item.ImageIndex).ToString());
            }
            else
            {
                base.OnStatusBarInfo(sender, ((EnumBedState)e.Item.ImageIndex).ToString());
            }
        }

        void ic_StatusBarInfo(object sender, string msg)
        {
            this.OnStatusBarInfo(sender, msg);
        }

        void ic_SendParamToControl(object sender, string dllName, string controlName, object objParams)
        {
            this.OnSendParamToControl(sender, dllName, controlName, objParams);
        }

        void ic_SendMessage(object sender, string msg)
        {
            this.OnSendMessage(sender, msg);
        }

        /// <summary>
        /// {997A8EEC-A27E-492f-941A-CDEAA3CC4AE7}
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ic_RefreshTree(object sender, EventArgs e)
        {
            this.OnRefreshTree();
            try
            {
                ucBedListView uc = this.tbBedView.Controls[0] as ucBedListView;
                uc.RefreshView();
                uc.WapType = wapType;
            }
            catch { }
        }

        #endregion

        #region ���к���
        /// <summary>
        /// ���ŵ�Tabpage
        /// </summary>
        /// <param name="control"></param>
        /// <param name="title"></param>
        /// <param name="tag"></param>
        public void AddTabpage(FS.FrameWork.WinForms.Controls.ucBaseControl control, string title, object tag)
        {

            foreach (TabPage tb in this.neuTabControl1.TabPages)
            {
                if (tb.Text == title)
                {
                    this.neuTabControl1.SelectedTab = tb;
                    return;
                }
            }
            TabPage tp = new TabPage(title);
            this.neuTabControl1.TabPages.Add(tp);

            control.Dock = DockStyle.Fill;
            control.Visible = true;

            FS.FrameWork.WinForms.Forms.IControlable ic = control as FS.FrameWork.WinForms.Forms.IControlable;
            if (ic != null)
            {
                ic.Init(this.tv, null, null);
            }
            #region {5DF40042-300D-49b8-BB8D-4E4E906B7BAF}
            if (control.GetType() == typeof(FS.HISFC.Components.RADT.Controls.ucBedManager))
            {
                FS.HISFC.Components.RADT.Controls.ucBedManager uc = control as FS.HISFC.Components.RADT.Controls.ucBedManager;

                uc.IsAllBedWave = this.isAllBedWave;
                uc.IsAllowAddAllBedLevel = this.isAllowAddAllBedLevel;
                uc.IsAllowEditBedLevel = this.IsAllowEditBedLevel;
                uc.IsAllowEditBedNo = this.IsAllowEditBedNo;
                uc.IsAllowAddBed = this.isAllowAddBed;

                tp.Controls.Add(uc);
            }
            else if (control.GetType() == typeof(FS.HISFC.Components.RADT.Controls.ucPatientOut))
            {
                FS.HISFC.Components.RADT.Controls.ucPatientOut uc = control as FS.HISFC.Components.RADT.Controls.ucPatientOut;

                uc.IsAutoDcOrder = this.isAutoDcOrder;
                uc.IsPrintOutNote = this.isPrintOutNote;
                uc.AutoDcDoct = this.autoDcDoct;

                tp.Controls.Add(uc);
            }
            else
            {
                tp.Controls.Add(control);
            }
            #endregion
            if (ic != null)
                ic.SetValue(patient, node);
            this.neuTabControl1.SelectedTab = tp;



        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public enum EnumBedState
        {
            /// <summary>
            /// 
            /// </summary>
            �մ� = 0,
            /// <summary>
            /// 
            /// </summary>
            �� = 1,
            /// <summary>
            /// 
            /// </summary>
            Ů = 2,
            /// <summary>
            /// 
            /// </summary>
            �ر� = 3,
            /// <summary>
            /// 
            /// </summary>
            �������� = 4,
            /// <summary>
            /// 
            /// </summary>
            �������� = 5,
            /// <summary>
            /// 
            /// </summary>
            һ������ = 6,
            /// <summary>
            /// 
            /// </summary>
            ��Σ = 7,
            /// <summary>
            /// 
            /// </summary>
            ��֢ = 8,
            /// <summary>
            /// 
            /// </summary>
            ���� = 9,
            /// <summary>
            /// 
            /// </summary>
            �ż� = 10,
            /// <summary>
            /// 
            /// </summary>
            �Ҵ� = 11,
            /// <summary>
            /// 
            /// </summary>
            �� = 12,
            /// <summary>
            /// 
            /// </summary>
            û�� = 13
        }


        #region IInterfaceContainer ��Ա

        Type[] FS.FrameWork.WinForms.Forms.IInterfaceContainer.InterfaceTypes
        {
            get
            {
                Type[] type = new Type[2];
                type[0] = typeof(FS.HISFC.BizProcess.Interface.IucOutPatient);
                type[1] = typeof(FS.HISFC.BizProcess.Interface.ICallBackPatient);
                return type;
            }
        }

        #endregion
    }
}
