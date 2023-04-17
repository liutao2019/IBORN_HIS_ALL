using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//{28C63B3A-9C64-4010-891D-46F846EA093D}
using System.Collections;
using System.Text.RegularExpressions;

namespace FS.HISFC.Components.RADT.Controls
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
    public partial class ucPatientOut : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPatientOut()
        {
            InitializeComponent();

            this.AutoScroll = true;
        }

        private void ucPatientOut_Load(object sender, EventArgs e)
        {

        }

        #region ����

        FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
        FS.HISFC.BizProcess.Integrate.RADT radt = new FS.HISFC.BizProcess.Integrate.RADT();
        FS.HISFC.BizLogic.RADT.InPatient inpatient = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo myPatientInfo = null;

        /// <summary>
        /// ����������{28C63B3A-9C64-4010-891D-46F846EA093D}
        /// </summary>
        private FS.FrameWork.Management.ControlParam ctlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// סԺ�����㷨�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Order.IDealSubjob iDealSubjob = null;

        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        private FS.HISFC.BizProcess.Integrate.Order orderIntegrate = new FS.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ��ǰ��½��Ա��������Ϣ���ݣ�
        /// </summary>
        private FS.HISFC.Models.Base.Employee Oper = new FS.HISFC.Models.Base.Employee();

        /// <summary>
        /// ���תҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.RADT.InPatient inPatienMgr = new FS.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        string Err = "";

        /// <summary>
        /// ��Ժ�Ǽǵ��ýӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.RADT.IPatientOut IPatientOut = null;

        /// <summary>
        /// ��Ժ֪ͨ����ӡ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.IPrintInHosNotice IPrintInHosNotice = null;

        /// <summary>
        /// ��ǰ����סԺ��ˮ��
        /// </summary>
        string strInpatientNo;

        /// <summary>
        /// �Ƿ񱣴����ʾ��ӡ��Ժ֪ͨ��
        /// </summary>
        private bool isPrintOutNote = false;

        /// <summary>
        /// �Ƿ���΢��������ʾ�
        /// {97CCCEDA-BD81-447e-A104-F016BB61735B}ͣ�õ������
        /// </summary>
        private bool isSendWechat = false;

        /// <summary>
        /// �Ƿ��Ժ�Զ�ֹͣҽ�������Ʋ���HNZY20������Ϊfalseʱ�����Ƴ�Ժ����ȫͣ���� 
        /// </summary>
        private int isOutDCOrder = -1;

        /// <summary>
        /// �Ƿ��Ժ�Ǽ��Զ�ֹͣҽ��
        /// </summary>
        private bool isAutoDcOrder = false;

        /// <summary>
        /// �Ƿ��Ժ�Ǽ��Զ�ֹͣҽ��
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("�Ƿ��Ժ�Ǽ��Զ�ֹͣҽ�� ����Ҫ�ο����Ʋ���HNZY20����Ժ�Ƿ��Զ�ֹͣҽ����")]
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

        /// <summary>
        /// ��Ժ�Զ�ֹͣ������ֹͣҽ��
        /// </summary>
        private AotuDcDoct autoDcDoct = AotuDcDoct.ExecutOutDoct;

        /// <summary>
        /// ��Ժ�Զ�ֹͣ������ֹͣҽ��
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("��Ժ�Զ�ֹͣ������ֹͣҽ��")]
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

        FS.HISFC.BizProcess.Interface.Common.IClinicPath iClinicPath = null;

        #endregion

        #region ����

        #region ��Ժ�Ǽ�����

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
        private CheckState isCanOutWhenDrugApplay = CheckState.Check;

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
        private CheckState isCanOutWhenUnConfirm = CheckState.Check;

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

        /// <summary>
        /// ����δ��ɵĻ��������Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenUnConsultation = CheckState.No;

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
        /// ����δ�շѵ��������뵥�Ƿ���������Ժ�Ǽ�
        /// </summary>
        private CheckState isCanOutWhenUnFeeUOApply = CheckState.Check;

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
        private CheckState isCanOutWhenLackFee = CheckState.Check;

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

        #endregion

        /// <summary>
        /// �Ƿ񱣴����ʾ��ӡ��Ժ֪ͨ��
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("�Ƿ񱣴����ʾ��ӡ��Ժ֪ͨ��")]
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
        /// ҩƷδִ��ȷ���Ƿ���������Ժ��Ҫ��ִ��ȷ�����̣�
        /// </summary>
        private bool isCanOutWhenExecDrugLimit = true;

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
        /// ����һ������Ʒ�ʱ��Ϊ��Ժ����
        /// </summary>
        private CheckState isCanOutWhenYJHLFeeDate = CheckState.Check;

        /// <summary>
        /// ���ڷ�ҩ�����Ƿ���������Ժ�Ǽ�
        /// </summary>
        [Category("��Ժ�Ǽ�"), Description("����һ������Ʒ�ʱ��Ϊ��Ժ����")]
        public CheckState IsCanOutWhenYJHLFeeDate
        {
            get
            {
                return this.isCanOutWhenYJHLFeeDate;
            }
            set
            {
                this.isCanOutWhenYJHLFeeDate = value;
            }
        }

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
                //[2011-6-2] zhaozf ת��Ĭ��ѡ���һ��
                if (this.cmbZg.Items.Count > 0)
                {
                    this.cmbZg.SelectedIndex = 0;
                }

                //��Ժ�Ǽǵ�ʱ��Ĭ��Ϊϵͳʱ��
                this.dtOutDate.Value = this.inpatient.GetDateTimeFromSysDateTime();

                this.Oper = manager.GetEmployeeInfo(this.inpatient.Operator.ID);
                if (this.Oper == null)
                {
                    MessageBox.Show("��ȡ��Ա������Ϣ����:" + manager.Err);
                }

                if (IPatientOut == null)
                {
                    IPatientOut = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.RADT.IPatientOut)) as FS.HISFC.BizProcess.Interface.RADT.IPatientOut;
                }

                if (IPrintInHosNotice == null)
                {
                    IPrintInHosNotice = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPrintInHosNotice)) as FS.HISFC.BizProcess.Interface.IPrintInHosNotice;
                }

                if (isOutDCOrder == -1)
                {
                    FS.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                    isOutDCOrder = controlMgr.GetControlParam<int>("HNZY20", true, 0);
                }

                ////���ҽ������
                //System.Collections.ArrayList alHealthCareType = manager.GetConstantList("HealthCareType");
                //if (alHealthCareType == null)
                //{
                //    MessageBox.Show("��ѯҽ�����ͳ���" + manager.Err);
                //    return;
                //}
                //else if (alHealthCareType.Count == 0)
                //{
                //    FS.FrameWork.WinForms.Classes.Function.ShowBalloonTip(3, "����", "û��ά��ҽ�����ͣ����ܵ���ҽ�����߰����Ժ�������⣡\r\n������������ϵϵͳ����Ա��", ToolTipIcon.Warning);
                //    //return;
                //}

                //this.cbxHealthCareType.AddItems(alHealthCareType);

                //��ӳ�Ժ���
                //this.cmbDiag.AddItems(SOC.HISFC.BizProcess.Cache.Order.GetICD10());

                //���Ѫ��
                this.cmbBloodType.AddItems(FS.HISFC.Models.RADT.BloodTypeEnumService.List());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private FS.HISFC.Models.HealthRecord.Diagnose GetOutDiag(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            //FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
            //ArrayList al = diagMgr.QueryDiagenoseByPateintID(patientInfo.ID, FS.HISFC.Models.Base.ServiceTypes.I);

            FS.HISFC.Models.HealthRecord.Diagnose outDiag = null;
            //if (al != null && al.Count > 0)
            //{
            //    foreach (FS.HISFC.Models.HealthRecord.Diagnose diag in al)
            //    {
            //        if (outDiag == null)
            //        {
            //            //�����
            //            if (diag.DiagInfo.DiagType.ID == "1")
            //            {
            //                outDiag = diag;
            //            }
            //        }
            //        //��Ժ���
            //        if (diag.DiagInfo.DiagType.ID == "14")
            //        {
            //            outDiag = diag;
            //        }
            //    }
            //}
            return outDiag;
        }
        /// <summary>
        /// ���û�����Ϣ���ؼ�
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void SetPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.txtPatientNo.Text = patientInfo.PID.PatientNO;		//סԺ��
            this.txtCard.Text = patientInfo.PID.CardNO;	//���￨��
            this.txtPatientNo.Tag = patientInfo.ID;				//סԺ��ˮ��
            this.txtName.Text = patientInfo.Name;						//����
            this.txtSex.Text = patientInfo.Sex.Name;					//�Ա�
            this.cmbBloodType.Tag = myPatientInfo.BloodType.ID;  //Ѫ��
            this.txtIndate.Text = patientInfo.PVisit.InTime.ToString();	//��Ժ����
            this.txtDept.Text = patientInfo.PVisit.PatientLocation.Dept.Name;	//��������
            this.txtDept.Tag = patientInfo.PVisit.PatientLocation.Dept.ID;	//���ұ���

            FS.FrameWork.Public.ObjectHelper helper = new FS.FrameWork.Public.ObjectHelper(manager.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PAYKIND));
            this.txtBalKind.Text = helper.GetName(patientInfo.Pact.PayKind.ID);

            this.cmbDiag.Text = myPatientInfo.MainDiagnose; //��Ժ���
            lblDiag.Text = myPatientInfo.MainDiagnose; //��Ժ���

            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;	//����
            this.txtFreePay.Text = patientInfo.FT.LeftCost.ToString();		//ʣ��Ԥ����
            this.txtTotcost.Text = patientInfo.FT.TotCost.ToString();		//�ܽ��

            //[2011-6-2] zhaozf ת��Ĭ��ѡ���һ��
            if (patientInfo.PVisit.ZG.ID != "0")
            {
                this.cmbZg.Text = "";
                this.cmbZg.Tag = patientInfo.PVisit.ZG.ID;						//ת��    
            }

            //add by zhaorong 2013-7-24 ҽ������
            //if (!string.IsNullOrEmpty(patientInfo.PVisit.HealthCareType))       //ҽ������ 0��ͨ  1���� 2��������  3ICU   4�ھ�   5�ۿ�    6�ǿ�ָ������  7��������
            //{
            //    this.cbxHealthCareType.Tag = Convert.ToInt32(patientInfo.PVisit.HealthCareType);
            //}

            this.dtpInDate.Value = patientInfo.PVisit.InTime; //��Ժʱ��
            if (this.myPatientInfo.PVisit.PreOutTime >= new DateTime(2000, 1, 1))
            {
                this.dtOutDate.Value = this.myPatientInfo.PVisit.PreOutTime;
            }
            else
            {
                this.dtOutDate.Value = this.inpatient.GetDateTimeFromSysDateTime();				//��Ժ���� �������޸�Ϊϵͳʱ��
            }

            //��Ժ�Ǽ��޸�ʱ�䴦�� {28C63B3A-9C64-4010-891D-46F846EA093D}
            string rtn = this.ctlMgr.QueryControlerInfo("ZY0002");
            if (rtn == null || rtn == "-1" || rtn == "")
            {
                rtn = "0";
            }

            if (rtn == "1")//
            {
                ArrayList alShiftDataInfo = this.inpatient.QueryPatientShiftInfoNew(this.myPatientInfo.ID);

                if (alShiftDataInfo == null)
                {
                    MessageBox.Show("��ȡ������¼��Ϣ����");
                    return;
                }

                bool isExitInfo = false;

                foreach (FS.HISFC.Models.Invalid.CShiftData myCShiftDate in alShiftDataInfo)
                {
                    if (myCShiftDate.ShitType == "C" || myCShiftDate.ShitType == "O") //�н����ٻػ��а����Ժ�Ǽ�
                    {
                        this.dtOutDate.Enabled = true;
                        isExitInfo = true;
                        break;
                    }
                }
                this.dtOutDate.Enabled = isExitInfo;
            }
            else if (rtn == "2")
            {
                // �����ǣ��κ�����������޸�
                this.dtOutDate.Enabled = true;
            }
            else
            {
                this.dtOutDate.Enabled = false;
            }
            this.panel1.AutoScrollPosition = new Point(0, 0);
            this.lblWarningInfo.Text = "";
            this.CheckOut(false);
        }

        /// <summary>
        /// �ӿؼ���ó�Ժ�Ǽ���Ϣ
        /// </summary>
        private void GetOutInfo()
        {
            myPatientInfo.PVisit.ZG.ID = this.cmbZg.Tag.ToString();
            myPatientInfo.PVisit.ZG.Name = this.cmbZg.Text;
            myPatientInfo.PVisit.PreOutTime = this.dtOutDate.Value;
            myPatientInfo.PVisit.OutTime = this.dtOutDate.Value;
            //��ȡ����ҽ������
            //myPatientInfo.PVisit.HealthCareType = this.cbxHealthCareType.Tag.ToString();
        }

        /// <summary>
        ///����
        /// </summary>
        private void ClearPatintInfo()
        {
            //[2011-6-2] zhaozf ת��Ĭ��ѡ���һ��
            //this.cmbZg.Text = "";
            //this.cmbZg.Tag = "";
            if (this.cmbZg.Items.Count > 0)
            {
                this.cmbZg.SelectedIndex = 0;
            }
            this.dtOutDate.Value = this.inpatient.GetDateTimeFromSysDateTime();
        }

        /// <summary>
        /// ˢ�»�����Ϣ
        /// </summary>
        /// <param name="inPatientNo"></param>
        public void RefreshList(string inPatientNo)
        {
            try
            {
                myPatientInfo = this.inpatient.QueryPatientInfoByInpatientNO(inPatientNo);

                //��������Ѳ��ڱ���,���������
                if (myPatientInfo.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
                {
                    MessageBox.Show("�����Ѳ��ڱ�����,��ˢ�µ�ǰ����", "��ʾ");
                    myPatientInfo = new FS.HISFC.Models.RADT.PatientInfo();
                }

                if (IPatientOut != null)
                {
                    if (IPatientOut.BeforePatientOut(myPatientInfo, FS.FrameWork.Management.Connection.Operator) < 0)
                    {
                        MessageBox.Show(IPatientOut.ErrInfo, "��ʾ");
                    }
                }

                this.SetPatientInfo(myPatientInfo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region ��Ժ�Ǽ���ȡ���ķ���

        /// <summary>
        /// ���Ľӿ�
        /// </summary>
        /// <param name="patientInfo"></param>
        /// <param name="order"></param>
        /// <param name="alOrders"></param>
        /// <param name="alSubOrders"></param>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        public int DealSubjobByInpatient(FS.HISFC.Models.RADT.PatientInfo patientInfo, FS.HISFC.Models.Order.Inpatient.Order order, ArrayList alOrders, ref ArrayList alSubOrders, ref string errInfo)
        {
            if (iDealSubjob == null)
            {
                iDealSubjob = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Order), typeof(FS.HISFC.BizProcess.Interface.Order.IDealSubjob)) as FS.HISFC.BizProcess.Interface.Order.IDealSubjob;
            }
            if (iDealSubjob != null)
            {
                //���Ĵ���
                return iDealSubjob.DealSubjob(patientInfo, false, order, alOrders, ref alSubOrders, ref errInfo);
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// �����շ���Ŀ
        /// </summary>
        FS.HISFC.Models.Fee.Item.Undrug subUndrug = new FS.HISFC.Models.Fee.Item.Undrug();

        /// <summary>
        /// �����շѴ�����Ϣ
        /// </summary>
        string errInfo = "";

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="item"></param>
        /// <param name="patient"></param>
        /// <returns></returns>
        private FS.HISFC.Models.Fee.Inpatient.FeeItemList CreateFeeItemList(FS.HISFC.Models.Base.Item item, FS.HISFC.Models.RADT.PatientInfo patient)
        {
            subUndrug = new FS.HISFC.Models.Fee.Item.Undrug();
            subUndrug = feeIntegrate.GetItem(item.ID);
            if (subUndrug == null)
            {
                errInfo = "��ȡ��ҩƷʧ�ܣ�" + feeIntegrate.Err;
                return null;
            }
            else if (!subUndrug.IsValid)
            {
                errInfo = "��ҩƷ��Ŀ" + subUndrug.Name + "�Ѿ�ͣ�ã�";
                return null;
            }
            subUndrug.Qty = item.Qty;

            //ʵ�帳ֵ
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
            feeItem.IsBaby = patient.IsBaby;
            feeItem.Item = subUndrug;
            feeItem.NoBackQty = subUndrug.Qty;
            feeItem.RecipeNO = feeIntegrate.GetUndrugRecipeNO();
            feeItem.Patient.Pact.PayKind.ID = patient.Pact.PayKind.ID;
            feeItem.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
            feeItem.FeeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.NurseCell.ID = patient.PVisit.PatientLocation.NurseCell.ID;
            feeItem.RecipeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            feeItem.ExecOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            if (patient.PVisit.AdmittingDoctor.ID == null || patient.PVisit.AdmittingDoctor.ID == "")
                patient.PVisit.AdmittingDoctor.ID = "�ռƷ�";

            feeItem.RecipeOper.ID = patient.PVisit.AdmittingDoctor.ID;
            feeItem.PayType = FS.HISFC.Models.Base.PayTypes.Balanced;
            feeItem.ChargeOper.ID = "�ռƷ�";

            decimal price = 0;
            decimal orgPrice = 0;
            if (feeIntegrate.GetPriceForInpatient(patient, feeItem.Item, ref price, ref orgPrice) != -1)
            {
                if (price > 0)
                {
                    feeItem.Item.Price = price;
                    feeItem.Item.DefPrice = orgPrice;
                }
            }

            //����ʱ���¼ʵ����ȡ����Ŀ������
            feeItem.ChargeOper.OperTime = this.ctlMgr.GetDateTimeFromSysDateTime();
            //feeItem.ChargeOper.OperTime = this.orderMgr.GetDateTimeFromSysDateTime();
            feeItem.FeeOper.ID = "�ռƷ�";
            feeItem.FeeOper.OperTime = this.ctlMgr.GetDateTimeFromSysDateTime();
            feeItem.SequenceNO = 0;
            feeItem.BalanceNO = 0;
            feeItem.BalanceState = "0";
            feeItem.FT.TotCost = subUndrug.Qty * subUndrug.Price;
            feeItem.FT.OwnCost = subUndrug.Qty * subUndrug.Price;
            feeItem.FTSource = new FS.HISFC.Models.Fee.Inpatient.FTSource("220");//��Ժ���գ��ٻغ��˷�
            return feeItem;
        }

        /// <summary>
        /// �Ƿ�����ȡ�����ķ���
        /// </summary>
        /// <param name="inPatientNo"></param>
        /// <param name="feeDate"></param>
        /// <returns></returns>
        private bool CheckIsFeeed(string inPatientNo, DateTime feeDate)
        {
            string sql = @"select count(*) from com_job_log t
                                where t.job_code='{0}'
                                and t.exec_oper='{1}'
                                and t.exec_date=to_date('{2}','yyyy-mm-dd hh24:mi:ss')";
            try
            {
                sql = string.Format(sql, "Sub_Fee", inPatientNo, feeDate.Date);

                string rev = this.ctlMgr.ExecSqlReturnOne(sql);

                if (FS.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// ������ȡ
        /// </summary>
        /// <returns>1 �ɹ� ��1 ʧ��</returns>
        public int SubFee()
        {
            //���ж��Ƿ�ά���ӿڣ�û�нӿڵĲ�����ֱ�ӷ���
            if (iDealSubjob == null)
            {
                iDealSubjob = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Order), typeof(FS.HISFC.BizProcess.Interface.Order.IDealSubjob)) as FS.HISFC.BizProcess.Interface.Order.IDealSubjob;
            }
            if (iDealSubjob == null)
            {
                errInfo = "�����շѽӿ�δʵ�֣����Զ���ȡ���ķ��ã�";
                this.ctlMgr.WriteErr();
                return 1;
            }

            ArrayList alFeeItems = new ArrayList();

            ArrayList alExecOrders = new ArrayList();

            //�����޸Ĺ̶��շ�ʱ�� ���շ��ã��˴��ж��Ѿ���ȡ�Ĳ����շ�
            if (this.CheckIsFeeed(this.myPatientInfo.ID, this.ctlMgr.GetDateTimeFromSysDateTime().Date))
            {
                return 1;
            }

            ArrayList alOrders = new ArrayList();
            ArrayList alSubOrders = new ArrayList();

            alExecOrders = this.orderIntegrate.QueryExecOrder(myPatientInfo.ID, "1",
                this.ctlMgr.GetDateTimeFromSysDateTime().Date,
                this.ctlMgr.GetDateTimeFromSysDateTime().AddDays(1).Date);
            if (alExecOrders == null)
            {
                errInfo = orderIntegrate.Err;
                this.ctlMgr.WriteErr();
                return -1;
            }
            else if (alExecOrders.Count == 0)
            {
                return 1;
            }

            string strOrderID = "'";
            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in alExecOrders)
            {
                strOrderID += execOrder.Order.ID + "','";
            }
            strOrderID = strOrderID + "'";

            alOrders = this.orderIntegrate.QueryOrder(myPatientInfo.ID, "1", strOrderID);
            if (alOrders == null)
            {
                errInfo = this.orderIntegrate.Err;
                this.ctlMgr.WriteErr();
                return -1;
            }
            else if (alOrders.Count == 0)
            {
                return 1;
            }

            if (alOrders.Count > 0)
            {
                int rev = this.DealSubjobByInpatient(myPatientInfo, (FS.HISFC.Models.Order.Inpatient.Order)alOrders[0],
                    alOrders, ref alSubOrders, ref errInfo);
                if (rev == -1)
                {
                    return -1;
                }
                else if (rev == 0)
                {
                    errInfo = "�����շѽӿ�δʵ�֣�";
                    this.ctlMgr.WriteErr();
                    return 1;
                }
                else
                {
                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = null;
                    foreach (FS.HISFC.Models.Base.Item item in alSubOrders)
                    {
                        feeItem = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
                        feeItem = this.CreateFeeItemList(item, myPatientInfo);
                        if (feeItem == null)
                        {
                            this.ctlMgr.WriteErr();
                            return -1;
                        }
                        alFeeItems.Add(feeItem);
                    }
                }
            }

            #region ���˵����շ�
            //FS.FrameWork.Management.PublicTrans.BeginTransaction();
            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeItems)
            {
                if (this.feeIntegrate.FeeItem(myPatientInfo, feeItem) == -1)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = this.feeIntegrate.Err;
                    this.ctlMgr.WriteErr();
                    return -1;
                }
            }

            try
            {
                string sqlInsert = @"INSERT INTO com_job_log   --��ʱ�շ���־
                                          ( job_code,   --��������
                                            job_name,   --��������
                                            exec_date,   --ִ��ʱ��
                                            exec_oper )  --ִ����
                                     VALUES 
                                          ('{0}' ,   --��������
                                           '{1}' ,   --��������
                                            to_date('{2}','yyyy-mm-dd hh24:mi:ss'),   --ִ��ʱ��
                                            '{3}') --ִ����";
                sqlInsert = string.Format(sqlInsert, "Sub_Fee", "סԺ�����շ�", this.ctlMgr.GetDateTimeFromSysDateTime().Date, myPatientInfo.ID);
                if (this.ctlMgr.ExecNoQuery(sqlInsert) == -1)
                {
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = orderIntegrate.Err;
                    this.ctlMgr.WriteErr();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                //FS.FrameWork.Management.PublicTrans.RollBack();
                errInfo = ex.Message;
                this.ctlMgr.WriteErr();
                return -1;
            }

            //FS.FrameWork.Management.PublicTrans.Commit();

            #endregion

            return 1;
        }

        /// <summary>
        /// ��Ժ�Ǽ���ȡ����
        /// </summary>
        /// <returns></returns>
        private int OutFee()
        {
            if (this.SubFee() == -1)
            {
                return -1;
            }


            return 1;
        }

        #endregion

        /// <summary>
        /// ��Ժ�Զ�ֹͣȫ������
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int AutoDcOrder(ref string errInfo)
        {
            //������Ժҽ����ҽ��
            if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                FS.HISFC.Models.Order.Inpatient.Order orderObj = null;
                int rev = this.IsAddOutOrder(ref orderObj, ref errInfo);
                if (rev == -1)
                {
                    return -1;
                }
                else if (rev == 0)
                {
                    //errInfo = "����" + myPatientInfo.Name + "��δ������Ժҽ����";
                    return -1;
                }

                if (orderObj == null || orderObj.ReciptDoctor == null || string.IsNullOrEmpty(orderObj.ReciptDoctor.ID))
                {
                    //errInfo = "����" + myPatientInfo.Name + "��δ������Ժҽ����";
                    return -1;
                }

                if (this.orderIntegrate.AutoDcOrder(myPatientInfo.ID, orderObj.ReciptDoctor, this.Oper, "", "��Ժ�Ǽ��Զ�ֹͣ") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }
            //����ҽ��
            else if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                if (this.myPatientInfo.PVisit.AttendingDirector == null ||
                    string.IsNullOrEmpty(myPatientInfo.PVisit.AttendingDirector.ID))
                {
                    errInfo = "����" + myPatientInfo.Name + "û��ά������ҽʦ�������Զ�ֹͣҽ����";
                    return -1;
                }

                if (this.orderIntegrate.AutoDcOrder(myPatientInfo.ID, myPatientInfo.PVisit.AttendingDirector, this.Oper, "", "��Ժ�Ǽ��Զ�ֹͣ") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }
            //�ܴ�ҽ��
            else if (autoDcDoct == AotuDcDoct.ExecutOutDoct)
            {
                if (this.orderIntegrate.AutoDcOrder(myPatientInfo.ID, myPatientInfo.PVisit.AdmittingDoctor, this.Oper, "", "��Ժ�Ǽ��Զ�ֹͣ") == -1)
                {
                    errInfo = this.orderIntegrate.Err;
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public virtual int Save()
        {
            #region �ٴ�·������

            if (this.iClinicPath == null)
            {   // {A196A50F-36E2-49b4-B530-DCC38D9D4464}
                //iClinicPath = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Common.IClinicPath))
                //                as FS.HISFC.BizProcess.Interface.Common.IClinicPath;
            }
            if (iClinicPath != null)
            {
                if (iClinicPath.PatientOutByNurse(this.myPatientInfo.ID, this.dtOutDate.Value))
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "δ�˳��ٴ�·�����ܳ�Ժ�Ǽ�!";
                    return 0;
                }
            }
            #endregion

            //������߲��ǵ����Ժ��ʾ
            if (this.dtOutDate.Value.Date < this.myPatientInfo.PVisit.InTime.Date)
            {
                MessageBox.Show("��Ժ���ڲ���С����Ժ���ڣ�", "��ʾ");
                return -1;
            }
            else
            {
                if (this.dtOutDate.Value.Date != this.inpatient.GetDateTimeFromSysDateTime().Date)
                {
                    DialogResult dr = MessageBox.Show("�û��ߵĳ�Ժ�����ǣ� " +
                        this.dtOutDate.Value.ToString("yyyy��MM��dd��") + "  �Ƿ������\r\n\r\n��Ժ���ڱȽ���Ҫ������д��ȷ��", "����",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2);
                    if (dr == DialogResult.No)
                    {
                        this.Err = "";
                        return -1;
                    }
                }
            }
//{218B24CF-17BB-4ddf-8149-D2154A245D11}
            if (this.CheckOut(true) != 1)
            {
                return -1;
            }
             //����ǰ���ֹ��Ժ���ѯ����Ӥ��
            ArrayList alBaby = inPatienMgr.QueryBabiesByMother(myPatientInfo.ID);
                for (int i = 0; i < alBaby.Count; i++)
                {
                    FS.HISFC.Models.RADT.PatientInfo babyInfo = alBaby[i] as FS.HISFC.Models.RADT.PatientInfo;
                    if (this.CheckOutbyPatient(true, babyInfo) != 1)
                    {
                       return -1;
                    }
                }
            //ȡ�������µ�סԺ������Ϣ
            myPatientInfo = this.inpatient.QueryPatientInfoByInpatientNO(this.myPatientInfo.ID);
            if (myPatientInfo == null)
            {
                this.Err = this.inpatient.Err;
                return -1;
            }
            this.Err = "";

            //��������Ѳ��ڱ���,���������---������ת�ƺ�,���������û�йر�,����ִ������
            if (myPatientInfo.PVisit.PatientLocation.NurseCell.ID != ((FS.HISFC.Models.Base.Employee)FS.FrameWork.Management.Connection.Operator).Nurse.ID)
            {
                this.Err = "�����Ѳ��ڱ�����,��ˢ�µ�ǰ����";
                return -1;
            }

            //���������Ժ״̬�����仯,���������
            if (myPatientInfo.PVisit.InState.ID.ToString() != "I")
            {
                this.Err = "������Ϣ�ѷ����仯,��ˢ�µ�ǰ����";
                return -1;
            }

            //ȡ��Ժ�Ǽ���Ϣ
            this.GetOutInfo();

            if (IPatientOut != null)
            {
                if (IPatientOut.BeforePatientOut(this.myPatientInfo.Clone(), this.inpatient.Operator) == -1)
                {
                    MessageBox.Show(IPatientOut.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (isDoctZG
                && this.Oper.EmployeeType.ID.ToString() == "D")
            {
                #region ҽ������ת�����

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                try
                {
                    //�Ǽǻ���Ѫ��
                    myPatientInfo.BloodType.ID = this.cmbBloodType.Tag;
                    if (this.inpatient.UpdateBloodType(myPatientInfo.ID, myPatientInfo.BloodType.ID.ToString()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����Ѫ��ʧ�ܣ�" + inpatient.Err);
                        return -1;
                    }

                    //�����޸�Ҫע�⣡����������������������
                    //ҽ��ֻ�Ǳ���ת����ѣ���Ҫ�޸Ļ���״̬
                    if (this.inpatient.UpdateZGInfo(myPatientInfo, myPatientInfo.PVisit.InState.ID.ToString()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("������Ϣʧ�ܣ�" + inpatient.Err);
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = ex.Message;
                    return -1;
                }
                FS.FrameWork.Management.PublicTrans.Commit();

                Err = "������Ϣ�ɹ�����֪ͨ��ʿ�����Ժ������";
                #endregion
            }
            else
            {
                #region ��ʿ�����Ժ

                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                try
                {

                    //�Ǽǻ���Ѫ��
                    myPatientInfo.BloodType.ID = this.cmbBloodType.Tag;
                    if (this.inpatient.UpdateBloodType(myPatientInfo.ID, myPatientInfo.BloodType.ID.ToString()) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����Ѫ��ʧ�ܣ�" + inpatient.Err);
                        return -1;
                    }

                    //����סԺ�����

                    //if (this.inpatient.UpdatePatientDiag(myPatientInfo.ID, this.cmbDiag.Text) < 0)
                    if (this.inpatient.UpdatePatientDiag(myPatientInfo.ID, this.lblDiag.Text) < 0)
                    {
                        this.Err = "���»�����ϳ���!";
                        return -1;
                    }

                    //��Ժ�Ǽ�

                    ///���ӹ̶����õ��շ�
                    if (this.feeIntegrate.SupplementBedFee(myPatientInfo) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = this.feeIntegrate.Err;
                        return -1;
                    }


                    int i = radt.OutPatient(myPatientInfo);
                    this.Err = radt.Err;
                    if (i == -1)��//ʧ��
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    else if (i == 0)//ȡ��
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "";
                        return 0;
                    }

                    if (this.isAutoDcOrder && isOutDCOrder == 1)
                    {
                        if (this.AutoDcOrder(ref Err) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                    }

                    if (this.OutFee() == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = errInfo;
                        return -1;
                    }

                    if (IPatientOut != null)
                    {
                        if (IPatientOut.OnPatientOut(this.myPatientInfo, this.inpatient.Operator) == -1)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            this.Err = IPatientOut.ErrInfo;
                            return -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = ex.Message;
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

                if (IPatientOut != null)
                {
                    if (IPatientOut.AfterPatientOut(this.myPatientInfo, this.inpatient.Operator) == -1)
                    {
                        MessageBox.Show(IPatientOut.ErrInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                #endregion

                //***************��ӡ��Ժ��ҩ��**************
                if (isPrintOutNote)
                {
                    if (MessageBox.Show("�Ƿ��ӡ��Ժ֪ͨ����", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.PrintOutNote();
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// �Ƿ�����Ժҽ��
        /// </summary>
        /// <param name="errInfo"></param>
        /// <returns></returns>
        private int IsAddOutOrder(ref FS.HISFC.Models.Order.Inpatient.Order orderObj, ref string errInfo)
        {
            int rev = this.orderIntegrate.GetOutOrder(myPatientInfo.ID, ref orderObj);
            if (rev == -1)
            {
                errInfo = orderIntegrate.Err;
                return -1;
            }
            else if (rev == 0)
            {
                errInfo = "��δ������Ժҽ����";
                return 0;
            }

            if (orderObj == null || orderObj.ReciptDoctor == null || string.IsNullOrEmpty(orderObj.ReciptDoctor.ID))
            {
                errInfo = "��δ������Ժҽ����";
                return 0;
            }
            return 1;
        }

        private int CheckOut(bool isSave)
        {
            //��������ʾͳһ�ŵ�һ��

            //��Ҫ��ʾѡ��Ķ���
            string checkMessage = "";

            //��ʾ��ֹ�Ķ���
            string stopMessage = "";

            this.Err = "";

            //ʵʱ��ȡ���µľ����ߡ�������Ϣ
            FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.Models.RADT.PatientInfo pInfo = inpatientMgr.QueryPatientInfoByInpatientNO(myPatientInfo.ID);
            if (pInfo != null)
            {
                myPatientInfo = pInfo;
            }

            Classes.Function funMgr = new FS.HISFC.Components.RADT.Classes.Function();

            FS.HISFC.BizProcess.Interface.IPatientShiftValid obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid)) as FS.HISFC.BizProcess.Interface.IPatientShiftValid;
            if (obj != null)
            {
                bool bl = obj.IsPatientShiftValid(pInfo, FS.HISFC.Models.Base.EnumPatientShiftValid.O, ref stopMessage);
                if (!bl)
                {
                    if (!string.IsNullOrEmpty(stopMessage))
                    {
                        MessageBox.Show(stopMessage);
                    }
                    return -1;
                }
            }


            //ע�ⲻҪ��ҵ��㵯��MessageBox������

            /*
             * һ������
             *  1�������˷����룬����������Ժ�Ǽ�
             * ����ҩƷ
             *  1��������ҩ���룬����������Ժ�Ǽ�
             *  2�����ڷ�ҩ���룬��ʾ�Ƿ�����Ժ�Ǽ�
             * �����ն�ȷ��
             *  1������δȷ����Ŀ�����������ʾ�Ƿ���������Ժ�Ǽ�
             * 
             * ����������������ýӿڱ��ػ�ʵ��
             * 1���Ƿ���ȫͣ
             * 2���Ƿ�����Ժҽ��
             * 3���Ƿ���δ���ҽ��
             * 4���жϴ�λ���ͻ���ѵ���ȡ�Ƿ���ȷ
             * */


            #region 1�������˷����룬����������Ժ�Ǽ�

            if (isCanOutWhenQuitFeeApplay != CheckState.Yes)
            {
                string ReturnApplyItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckReturnApply(pInfo.ID);
                if (ReturnApplyItemInfo != null)
                {
                    string[] item = ReturnApplyItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {
                        tip += item[i] + "\r";
                    }

                    if (isCanOutWhenQuitFeeApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n�����δȷ�ϵ��˷����룡\r\n" + tip;

                        //if (MessageBox.Show("����δȷ�ϵ��˷����룬�Ƿ���������Ժ��" + ReturnApplyItemInfo, "��ʾ"
                        //    , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //{
                        //    return 0;
                        //}
                    }
                    else if (isCanOutWhenQuitFeeApplay == CheckState.No)
                    {
                        //�����˷����벻��������Ժ�Ǽ�
                        //this.Err += "����δȷ�ϵ��˷�����,��ȷ�ϻ�ȡ���˷Ѻ�������Ժ�Ǽ�!\n" + ReturnApplyItemInfo;
                        stopMessage += "\r\n�����δȷ�ϵ��˷����룡\r\n" + tip;
                        //return -1;
                    }
                }
            }
            #endregion

            #region 2��������ҩ���룬��ʾ�Ƿ����

            if (isCanOutWhenQuitDrugApplay != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckQuitDrugApplay(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (this.isCanOutWhenQuitDrugApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�����δ��˵���ҩ������Ϣ��" + "\r" + msg;
                    }
                    else if (this.isCanOutWhenQuitDrugApplay == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n�����δ��˵���ҩ������Ϣ��" + "\r" + msg;
                    }
                }

            }

            #endregion

            #region 3���жϻ����Ǵ���δ��ҩ��ҩƷ ��ʾ�Ƿ����

            if (isCanOutWhenDrugApplay != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckDrugApplayWithOutQuit(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (this.isCanOutWhenDrugApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�����δ��ҩ��ҩƷ��Ŀ��\r\n" + msg;

                    }
                    else if (isCanOutWhenDrugApplay == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n�����δ��ҩ��ҩƷ��Ŀ��\r\n" + msg;
                    }
                }
            }
            #endregion

            #region 4������δ�ն�ȷ����Ŀ����ʾ�Ƿ����

            if (isCanOutWhenUnConfirm != CheckState.Yes)
            {
                string UnConfirmItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckUnConfirm(pInfo.ID);
                if (UnConfirmItemInfo != null)
                {
                    string[] item = UnConfirmItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {

                        tip += item[i] + "\r";
                    }

                    if (this.isCanOutWhenUnConfirm == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�����δȷ���շѵ��ն���Ŀ��\r\n" + tip;
                        //if (MessageBox.Show("��������δȷ���շѵ��ն���Ŀ���Ƿ���������Ժ��\n" + UnConfirmItemInfo, "��ʾ"
                        //           , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //{
                        //    this.Err = "����ϵ��ؿ���ȷ��ִ���շѣ�";
                        //    return -1;
                        //}
                    }
                    else if (isCanOutWhenUnConfirm == CheckState.No)
                    {
                        //this.Err += "��������δȷ���շѵ��ն���Ŀ������ϵ����ȷ���շѣ�\n" + UnConfirmItemInfo;

                        stopMessage += "\r\n\r\n�����δȷ���շѵ��ն���Ŀ��\r\n" + tip;
                        //return -1;
                    }
                }
            }

            #endregion

            #region 5���ж��Ƿ�����Ժҽ��

            if (isCanOutWhenNoOutOrder != CheckState.Yes)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = null;

                int rev = IsAddOutOrder(ref inOrder, ref errInfo);

                if (rev < 0)
                {
                    MessageBox.Show("��ѯ��Ժҽ������!\r\n" + errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return -1;
                }
                else if (rev == 0)
                {
                    if (isCanOutWhenNoOutOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n��" + errInfo;
                    }
                    else if (isCanOutWhenNoOutOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n��" + errInfo;
                    }
                }
            }

            #endregion

            #region 6���жϳ����Ƿ�ȫͣ

            if (isCanOutWhenNoDcOrder != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllLongOrderUnStop(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (isCanOutWhenNoDcOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�ﳤ��ҽ��û��ȫ��ֹͣ\r\n" + msg;
                    }
                    else if (isCanOutWhenNoDcOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n�ﳤ��ҽ��û��ȫ��ֹͣ\r\n" + msg;
                    }
                }
            }

            #endregion

            #region 7���ж��Ƿ���δ���ҽ��


            if (isCanOutWhenUnConfirmOrder != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllOrderUnConfirm(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (isCanOutWhenNoDcOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�����δ���ҽ��\r\n" + msg;
                    }
                    else if (isCanOutWhenNoDcOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n�����δ���ҽ��\r\n" + msg;
                    }
                }
            }

            #endregion

            #region 8���ж��Ƿ���δ�շѵķ�ҩƷҽ��ִ�е�

            if (isCanOutWhenNoFeeExecUndrugOrder != CheckState.Yes)
            {
                //////=============����һ���ַ��� -- unfee|||countWarnings
                string returnStr = FS.HISFC.Components.RADT.Classes.Function.CheckExecOrderCharge(pInfo.ID);
                if (returnStr != null)
                {
                    string[] strArray = returnStr.Split(new char[3] { '|', '|', '|' });

                    if (Convert.ToInt32(strArray[3]) > 0)
                    {
                        string[] item = strArray[0].Split('\r');
                        string tip = "";
                        for (int i = 0; i < item.Length; i++)
                        {
                            tip += item[i] + "\r";
                        }

                        if (this.isCanOutWhenNoFeeExecUndrugOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n�����δ�շ���Ŀ��\r\n" + tip;
                            //if (MessageBox.Show(strArray[0] + "\r\n��δ�շѣ��Ƿ���������Ժ��", "��ʾ"
                            //    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                            //{
                            //    return -1;
                            //}
                        }
                        else if (this.isCanOutWhenNoFeeExecUndrugOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n�����δ�շ���Ŀ��\r\n" + tip;
                            //this.Err += "������Ŀ��δ�շѣ���ȷ�Ϸ�����ȡ���ٰ����Ժ��\n" + strArray[0];
                            //return 0;
                        }
                    }
                }
            }

            #endregion


            #region 10������δ�շ��������뵥����������Ժ

            if (isCanOutWhenUnFeeUOApply != CheckState.Yes)
            {
                string sql = @"select count(*) from met_ops_apply f
                                                                    where f.clinic_code='{0}'
                                                                    and f.ynvalid='1'
                                                                        and f.execstatus!='4'
                                                                        and f.execstatus!='5'";

                string rev = funMgr.ExecSqlReturnOne(string.Format(sql, pInfo.ID));
                try
                {
                    if (FS.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                    {
                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n�����δ��ɵ��������뵥��";
                        }
                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n�����δ��ɵ��������뵥��";
                        }
                        //System.Windows.Forms.MessageBox.Show("���ߡ�" + pInfo.Name + "������δ��ɵ�����������������Ժ������\r\r\r\n�������������ϵ�����ң�", "��ʾ", System.Windows.Forms.MessageBoxButtons.OK);
                        //return false;
                    }
                }
                catch
                {
                }
            }

            #endregion


            #region 10������δ�շ��������뵥����������Ժ

            if (isCanOutWhenUnFeeUOApply != CheckState.Yes)
            {
                string sql = @"select  f.operationno,f.apply_date,f.pre_date,a.item_name,f.apply_dpcd from met_ops_apply f,met_ops_operationitem a
                                                        where a.operationno=f.operationno
                                                        and f.clinic_code = a.clinic_code  
                                                        and f.clinic_code='{0}'
                                                        and f.ynvalid='1'
                                                        and a.main_flag = '1'
                                                        and f.execstatus!='4'
                                                        and f.execstatus!='5'
order by a.sort_no";
                try
                {
                    DataSet dsTemp = null;
                    string msg = null;
                    int rev = funMgr.ExecQuery(string.Format(sql, myPatientInfo.ID), ref dsTemp);

                    if (rev > 0)
                    {
                        if (dsTemp != null && dsTemp.Tables.Count > 0)
                        {
                            DataTable dtTemp = dsTemp.Tables[0];

                            string strapplyNo = string.Empty;
                            string strApplyDate = string.Empty;
                            string strItemName = string.Empty;
                            string strPreDate = string.Empty;
                            string applyDpcd = string.Empty;
                            ArrayList alDifOper = new ArrayList();
                            foreach (DataRow drRow in dtTemp.Rows)
                            {
                                strapplyNo = drRow["operationno"].ToString();
                                strApplyDate = drRow["apply_date"].ToString();
                                strItemName = drRow["item_name"].ToString();
                                strPreDate = drRow["pre_date"].ToString();
                                applyDpcd = drRow["apply_dpcd"].ToString();

                                if (!alDifOper.Contains(strapplyNo))
                                {
                                    msg = msg + "����ʱ�䣺" + strApplyDate + "    " + "ԤԼʱ�䣺" + strPreDate + "\r\n" + "������ң�" + SOC.HISFC.BizProcess.Cache.Common.GetDept(applyDpcd) + "    �������ƣ�" + strItemName + "\r\n";
                                }
                                else
                                {
                                    msg = msg + "������ң�" + SOC.HISFC.BizProcess.Cache.Common.GetDept(applyDpcd) + "    �������ƣ�" + strItemName + "\r\n";
                                }

                                alDifOper.Add(strapplyNo);

                            }

                            if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                            {
                                checkMessage += "\r\n\r\n�����δ��ɵ��������뵥��\r\n" + msg;
                            }
                            else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                            {
                                stopMessage += "\r\n\r\n�����δ��ɵ��������뵥��\r\n" + msg;
                            }
                            //System.Windows.Forms.MessageBox.Show("���ߡ�" + myPatientInfo.Name + "������δ��ɵ�����������������Ժ������\r\r\r\n�������������ϵ�����ң�", "��ʾ", System.Windows.Forms.MessageBoxButtons.OK);
                            //return false;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("����δ��ɵ��������뵥");
                }
            }

            #endregion

            #region 11������δ��ɻ���������Ժ

            if (isCanOutWhenUnConsultation != CheckState.Yes)
            {
                string strSQL = @"select --f.state,--0:����δ���� 1:�ѽ��� 2:����� 3:�Ѿܾ�
                                   count(1)
                            from CONS_CONSULTATION f
                            where f.state in('0','1')
                            and f.inpatient_id=(
                            select g.id from 
                            bhemr.pt_inpatient_cure g
                            where g.inpatient_code='{0}'
                            )";
                strSQL = string.Format(strSQL, myPatientInfo.ID);

                string errMsg = null;
                if (this.ctlMgr.ExecSqlReturnOne(strSQL) != "0")
                {
                    errMsg = "\r\n\r\n�����δ��ɵĻ������룡\n";
                }

                if (isCanOutWhenUnConsultation == CheckState.Check)
                {
                    checkMessage += errMsg;
                }
                else if (isCanOutWhenUnConsultation == CheckState.No)
                {
                    stopMessage += errMsg;
                }
            }
            #endregion

            #region 12���������ҩƷִ��ȷ�Ϲ��ܣ����ƴ���δȷ�ϵ�ҩƷ��Ŀ�������Ժ
            if (this.IsCanOutWhenExecDrugLimit)
            {
                string execMsg = FS.HISFC.Components.RADT.Classes.Function.CheckExecDrugWithOutQuit(pInfo.ID);
                if (!string.IsNullOrEmpty(execMsg))
                {
                    checkMessage += "\r\n\r\n�����δִ��ȷ�ϵ�ҩƷ��Ŀ��\r\n" + execMsg;
                    stopMessage += "\r\n\r\n�����δִ��ȷ�ϵ�ҩƷ��Ŀ��\r\n" + execMsg;
                }
            }
            #endregion


            #region Ƿ���ж�

            if (isCanOutWhenLackFee != CheckState.Yes)
            {
                try
                {
                    if (pInfo.PVisit.MoneyAlert != 0 && pInfo.FT.LeftCost < pInfo.PVisit.MoneyAlert)
                    {
                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n���Ѿ�Ƿ�ѣ�\r\n�� " + pInfo.FT.LeftCost.ToString() + "\r\n�����ߣ� " + pInfo.PVisit.MoneyAlert.ToString();
                        }
                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n���Ѿ�Ƿ�ѣ�\r\n�� " + pInfo.FT.LeftCost.ToString() + "\r\n�����ߣ� " + pInfo.PVisit.MoneyAlert.ToString();
                        }

                        //if (System.Windows.Forms.MessageBox.Show(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ��" + pInfo.Name + "�� " + "�Ѿ�Ƿ�ѣ�\r\n\r\n�� " + pInfo.FT.LeftCost.ToString() + "\r\n\r\n�����ߣ� " + pInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n�Ƿ���������Ժ��", "ѯ��", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        //{
                        //    return false;
                        //}
                    }
                }
                catch
                {
                }
            }
            #endregion

            #region 13���ж��Ƿ����һ������Ʒ�ʱ��Ϊ��Ժ����

            if (isCanOutWhenYJHLFeeDate != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckYJHLFeeDate(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (this.isCanOutWhenYJHLFeeDate == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n���Ժ�������һ������ļƷ���Ŀ����ȷ�ϣ�\r\n" + msg;

                    }
                    else if (isCanOutWhenYJHLFeeDate == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n���Ժ�������һ������ļƷ���Ŀ����ȷ�ϣ�\r\n" + msg;
                    }
                }
            }

            #endregion

            #region ��Ҫ��������Ϣ��ʾ�ڽ�����
            if (!isSave)
            {
                this.lblWarningInfo.Text = GetPaientInfo(pInfo) + "\r\n\r\n" + stopMessage;
                return -1;
            }
            #endregion

            #region 9����Ժ���������Ϊ��

            if (this.cmbZg.Tag == null || string.IsNullOrEmpty(cmbZg.Tag.ToString()))
            {
                stopMessage += "\r\n\r\n�ﲡ����Ҫ�󣬳�Ժ���������Ϊ�գ�\r\n";
                //this.Err = "���ݲ�����Ҫ�󣬳�Ժ���������Ϊ�գ�";
                //return -1;
            }
            #endregion

            FS.HISFC.Components.RADT.Forms.frmMessageShow frmMessage = new FS.HISFC.Components.RADT.Forms.frmMessageShow();
            frmMessage.Clear();
            if (!string.IsNullOrEmpty(checkMessage))
            {
                frmMessage.SetPatientInfo(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + myPatientInfo.Name + "��");
                frmMessage.SetTipMessage("������������δ����,�Ƿ���������Ժ��");
                frmMessage.SetMessage(checkMessage);
                frmMessage.SetPerfectWidth();
                if (frmMessage.ShowDialog() == DialogResult.No)
                {
                    return -1;
                }
            }

            if (!string.IsNullOrEmpty(stopMessage))
            {
                frmMessage.SetPatientInfo(myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + myPatientInfo.Name + "��");
                frmMessage.SetTipMessage("������������δ����,���ܼ��������Ժ��");
                frmMessage.SetMessage(stopMessage);
                frmMessage.SetPerfectWidth();
                frmMessage.HideNoButton();
                frmMessage.ShowDialog();
                return -1;
            }

            if (!string.IsNullOrEmpty(this.Err))
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// �жϻ����ܷ��Ժ����������ĸ��
        /// </summary>
        /// <param name="patientinfo"></param>
        /// <returns></returns>// {218B24CF-17BB-4ddf-8149-D2154A245D11}
        /// 
        private int CheckOutbyPatient(bool isSave, FS.HISFC.Models.RADT.PatientInfo babyinfo)
        {
            ArrayList alBaby = inPatienMgr.QueryBabiesByMother(babyinfo.ID);
            //��������ʾͳһ�ŵ�һ��

            //��Ҫ��ʾѡ��Ķ���
            string checkMessage = "";

            //��ʾ��ֹ�Ķ���
            string stopMessage = "";

            this.Err = "";

            //ʵʱ��ȡ���µľ����ߡ�������Ϣ
            FS.HISFC.BizLogic.RADT.InPatient inpatientMgr = new FS.HISFC.BizLogic.RADT.InPatient();
            FS.HISFC.Models.RADT.PatientInfo pInfo = inpatientMgr.QueryPatientInfoByInpatientNO(babyinfo.ID);
            if (pInfo != null)
            {
                babyinfo = pInfo;
            }

            Classes.Function funMgr = new FS.HISFC.Components.RADT.Classes.Function();

            FS.HISFC.BizProcess.Interface.IPatientShiftValid obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.IPatientShiftValid)) as FS.HISFC.BizProcess.Interface.IPatientShiftValid;
            if (obj != null)
            {
                bool bl = obj.IsPatientShiftValid(pInfo, FS.HISFC.Models.Base.EnumPatientShiftValid.O, ref stopMessage);
                if (!bl)
                {
                    if (!string.IsNullOrEmpty(stopMessage))
                    {
                        MessageBox.Show(stopMessage);
                    }
                    return -1;
                }
            }


            //ע�ⲻҪ��ҵ��㵯��MessageBox������

            /*
             * һ������
             *  1�������˷����룬����������Ժ�Ǽ�
             * ����ҩƷ
             *  1��������ҩ���룬����������Ժ�Ǽ�
             *  2�����ڷ�ҩ���룬��ʾ�Ƿ�����Ժ�Ǽ�
             * �����ն�ȷ��
             *  1������δȷ����Ŀ�����������ʾ�Ƿ���������Ժ�Ǽ�
             * 
             * ����������������ýӿڱ��ػ�ʵ��
             * 1���Ƿ���ȫͣ
             * 2���Ƿ�����Ժҽ��
             * 3���Ƿ���δ���ҽ��
             * 4���жϴ�λ���ͻ���ѵ���ȡ�Ƿ���ȷ
             * */


            #region 1�������˷����룬����������Ժ�Ǽ�

            if (isCanOutWhenQuitFeeApplay != CheckState.Yes)
            {
                string ReturnApplyItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckReturnApply(pInfo.ID);
                if (ReturnApplyItemInfo != null)
                {
                    string[] item = ReturnApplyItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {
                        tip += item[i] + "\r";
                    }

                    if (isCanOutWhenQuitFeeApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n�����δȷ�ϵ��˷����룡\r\n" + tip;

                        //if (MessageBox.Show("����δȷ�ϵ��˷����룬�Ƿ���������Ժ��" + ReturnApplyItemInfo, "��ʾ"
                        //    , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //{
                        //    return 0;
                        //}
                    }
                    else if (isCanOutWhenQuitFeeApplay == CheckState.No)
                    {
                        //�����˷����벻��������Ժ�Ǽ�
                        //this.Err += "����δȷ�ϵ��˷�����,��ȷ�ϻ�ȡ���˷Ѻ�������Ժ�Ǽ�!\n" + ReturnApplyItemInfo;
                        stopMessage += "\r\n�����δȷ�ϵ��˷����룡\r\n" + tip;
                        //return -1;
                    }
                }
            }
            #endregion

            #region 2��������ҩ���룬��ʾ�Ƿ����

            if (isCanOutWhenQuitDrugApplay != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckQuitDrugApplay(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (this.isCanOutWhenQuitDrugApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�����δ��˵���ҩ������Ϣ��" + "\r" + msg;
                    }
                    else if (this.isCanOutWhenQuitDrugApplay == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n�����δ��˵���ҩ������Ϣ��" + "\r" + msg;
                    }
                }

            }

            #endregion

            #region 3���жϻ����Ǵ���δ��ҩ��ҩƷ ��ʾ�Ƿ����

            if (isCanOutWhenDrugApplay != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckDrugApplayWithOutQuit(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (this.isCanOutWhenDrugApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�����δ��ҩ��ҩƷ��Ŀ��\r\n" + msg;

                    }
                    else if (isCanOutWhenDrugApplay == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n�����δ��ҩ��ҩƷ��Ŀ��\r\n" + msg;
                    }
                }
            }
            #endregion

            #region 4������δ�ն�ȷ����Ŀ����ʾ�Ƿ����

            if (isCanOutWhenUnConfirm != CheckState.Yes)
            {
                string UnConfirmItemInfo = FS.HISFC.Components.RADT.Classes.Function.CheckUnConfirm(pInfo.ID);
                if (UnConfirmItemInfo != null)
                {
                    string[] item = UnConfirmItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {

                        tip += item[i] + "\r";
                    }

                    if (this.isCanOutWhenUnConfirm == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�����δȷ���շѵ��ն���Ŀ��\r\n" + tip;
                        //if (MessageBox.Show("��������δȷ���շѵ��ն���Ŀ���Ƿ���������Ժ��\n" + UnConfirmItemInfo, "��ʾ"
                        //           , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //{
                        //    this.Err = "����ϵ��ؿ���ȷ��ִ���շѣ�";
                        //    return -1;
                        //}
                    }
                    else if (isCanOutWhenUnConfirm == CheckState.No)
                    {
                        //this.Err += "��������δȷ���շѵ��ն���Ŀ������ϵ����ȷ���շѣ�\n" + UnConfirmItemInfo;

                        stopMessage += "\r\n\r\n�����δȷ���շѵ��ն���Ŀ��\r\n" + tip;
                        //return -1;
                    }
                }
            }

            #endregion

            #region 5���ж��Ƿ�����Ժҽ��

            if (isCanOutWhenNoOutOrder != CheckState.Yes)
            {
                FS.HISFC.Models.Order.Inpatient.Order inOrder = null;

                int rev = IsAddOutOrder(ref inOrder, ref errInfo);

                if (rev < 0)
                {
                    MessageBox.Show("��ѯ��Ժҽ������!\r\n" + errInfo, "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                    return -1;
                }
                else if (rev == 0)
                {
                    if (isCanOutWhenNoOutOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n��" + errInfo;
                    }
                    else if (isCanOutWhenNoOutOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n��" + errInfo;
                    }
                }
            }

            #endregion

            #region 6���жϳ����Ƿ�ȫͣ

            if (isCanOutWhenNoDcOrder != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllLongOrderUnStop(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (isCanOutWhenNoDcOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�ﳤ��ҽ��û��ȫ��ֹͣ\r\n" + msg;
                    }
                    else if (isCanOutWhenNoDcOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n�ﳤ��ҽ��û��ȫ��ֹͣ\r\n" + msg;
                    }
                }
            }

            #endregion

            #region 7���ж��Ƿ���δ���ҽ��


            if (isCanOutWhenUnConfirmOrder != CheckState.Yes)
            {
                string msg = FS.HISFC.Components.RADT.Classes.Function.CheckAllOrderUnConfirm(pInfo.ID);
                if (!string.IsNullOrEmpty(msg))
                {
                    if (isCanOutWhenNoDcOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�����δ���ҽ��\r\n" + msg;
                    }
                    else if (isCanOutWhenNoDcOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n�����δ���ҽ��\r\n" + msg;
                    }
                }
            }

            #endregion

            #region 8���ж��Ƿ���δ�շѵķ�ҩƷҽ��ִ�е�

            if (isCanOutWhenNoFeeExecUndrugOrder != CheckState.Yes)
            {
                //////=============����һ���ַ��� -- unfee|||countWarnings
                string returnStr = FS.HISFC.Components.RADT.Classes.Function.CheckExecOrderCharge(pInfo.ID);
                if (returnStr != null)
                {
                    string[] strArray = returnStr.Split(new char[3] { '|', '|', '|' });

                    if (Convert.ToInt32(strArray[3]) > 0)
                    {
                        string[] item = strArray[0].Split('\r');
                        string tip = "";
                        for (int i = 0; i < item.Length; i++)
                        {
                            tip += item[i] + "\r";
                        }

                        if (this.isCanOutWhenNoFeeExecUndrugOrder == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n�����δ�շ���Ŀ��\r\n" + tip;
                            //if (MessageBox.Show(strArray[0] + "\r\n��δ�շѣ��Ƿ���������Ժ��", "��ʾ"
                            //    , MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                            //{
                            //    return -1;
                            //}
                        }
                        else if (this.isCanOutWhenNoFeeExecUndrugOrder == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n�����δ�շ���Ŀ��\r\n" + tip;
                            //this.Err += "������Ŀ��δ�շѣ���ȷ�Ϸ�����ȡ���ٰ����Ժ��\n" + strArray[0];
                            //return 0;
                        }
                    }
                }
            }

            #endregion


            #region 10������δ�շ��������뵥����������Ժ

            if (isCanOutWhenUnFeeUOApply != CheckState.Yes)
            {
                string sql = @"select count(*) from met_ops_apply f
                                                                    where f.clinic_code='{0}'
                                                                    and f.ynvalid='1'
                                                                        and f.execstatus!='4'
                                                                        and f.execstatus!='5'";

                string rev = funMgr.ExecSqlReturnOne(string.Format(sql, pInfo.ID));
                try
                {
                    if (FS.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                    {
                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n�����δ��ɵ��������뵥��";
                        }
                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n�����δ��ɵ��������뵥��";
                        }
                        //System.Windows.Forms.MessageBox.Show("���ߡ�" + pInfo.Name + "������δ��ɵ�����������������Ժ������\r\r\r\n�������������ϵ�����ң�", "��ʾ", System.Windows.Forms.MessageBoxButtons.OK);
                        //return false;
                    }
                }
                catch
                {
                }
            }

            #endregion


            #region 10������δ�շ��������뵥����������Ժ

            if (isCanOutWhenUnFeeUOApply != CheckState.Yes)
            {
                string sql = @"select  f.operationno,f.apply_date,f.pre_date,a.item_name,f.apply_dpcd from met_ops_apply f,met_ops_operationitem a
                                                        where a.operationno=f.operationno
                                                        and f.clinic_code = a.clinic_code  
                                                        and f.clinic_code='{0}'
                                                        and f.ynvalid='1'
                                                        and a.main_flag = '1'
                                                        and f.execstatus!='4'
                                                        and f.execstatus!='5'
order by a.sort_no";
                try
                {
                    DataSet dsTemp = null;
                    string msg = null;
                    int rev = funMgr.ExecQuery(string.Format(sql, babyinfo.ID), ref dsTemp);

                    if (rev > 0)
                    {
                        if (dsTemp != null && dsTemp.Tables.Count > 0)
                        {
                            DataTable dtTemp = dsTemp.Tables[0];

                            string strapplyNo = string.Empty;
                            string strApplyDate = string.Empty;
                            string strItemName = string.Empty;
                            string strPreDate = string.Empty;
                            string applyDpcd = string.Empty;
                            ArrayList alDifOper = new ArrayList();
                            foreach (DataRow drRow in dtTemp.Rows)
                            {
                                strapplyNo = drRow["operationno"].ToString();
                                strApplyDate = drRow["apply_date"].ToString();
                                strItemName = drRow["item_name"].ToString();
                                strPreDate = drRow["pre_date"].ToString();
                                applyDpcd = drRow["apply_dpcd"].ToString();

                                if (!alDifOper.Contains(strapplyNo))
                                {
                                    msg = msg + "����ʱ�䣺" + strApplyDate + "    " + "ԤԼʱ�䣺" + strPreDate + "\r\n" + "������ң�" + SOC.HISFC.BizProcess.Cache.Common.GetDept(applyDpcd) + "    �������ƣ�" + strItemName + "\r\n";
                                }
                                else
                                {
                                    msg = msg + "������ң�" + SOC.HISFC.BizProcess.Cache.Common.GetDept(applyDpcd) + "    �������ƣ�" + strItemName + "\r\n";
                                }

                                alDifOper.Add(strapplyNo);

                            }

                            if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                            {
                                checkMessage += "\r\n\r\n�����δ��ɵ��������뵥��\r\n" + msg;
                            }
                            else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                            {
                                stopMessage += "\r\n\r\n�����δ��ɵ��������뵥��\r\n" + msg;
                            }
                            //System.Windows.Forms.MessageBox.Show("���ߡ�" + babyinfo.Name + "������δ��ɵ�����������������Ժ������\r\r\r\n�������������ϵ�����ң�", "��ʾ", System.Windows.Forms.MessageBoxButtons.OK);
                            //return false;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("����δ��ɵ��������뵥");
                }
            }

            #endregion

            #region 11������δ��ɻ���������Ժ

            if (isCanOutWhenUnConsultation != CheckState.Yes)
            {
                string strSQL = @"select --f.state,--0:����δ���� 1:�ѽ��� 2:����� 3:�Ѿܾ�
                                   count(1)
                            from CONS_CONSULTATION f
                            where f.state in('0','1')
                            and f.inpatient_id=(
                            select g.id from 
                            bhemr.pt_inpatient_cure g
                            where g.inpatient_code='{0}'
                            )";
                strSQL = string.Format(strSQL, babyinfo.ID);

                string errMsg = null;
                if (this.ctlMgr.ExecSqlReturnOne(strSQL) != "0")
                {
                    errMsg = "\r\n\r\n�����δ��ɵĻ������룡\n";
                }

                if (isCanOutWhenUnConsultation == CheckState.Check)
                {
                    checkMessage += errMsg;
                }
                else if (isCanOutWhenUnConsultation == CheckState.No)
                {
                    stopMessage += errMsg;
                }
            }
            #endregion

            #region 12���������ҩƷִ��ȷ�Ϲ��ܣ����ƴ���δȷ�ϵ�ҩƷ��Ŀ�������Ժ
            if (this.IsCanOutWhenExecDrugLimit)
            {
                string execMsg = FS.HISFC.Components.RADT.Classes.Function.CheckExecDrugWithOutQuit(pInfo.ID);
                if (!string.IsNullOrEmpty(execMsg))
                {
                    checkMessage += "\r\n\r\n�����δִ��ȷ�ϵ�ҩƷ��Ŀ��\r\n" + execMsg;
                    stopMessage += "\r\n\r\n�����δִ��ȷ�ϵ�ҩƷ��Ŀ��\r\n" + execMsg;
                }
            }
            #endregion


            #region Ƿ���ж�

            if (isCanOutWhenLackFee != CheckState.Yes)
            {
                try
                {
                    if (pInfo.PVisit.MoneyAlert != 0 && pInfo.FT.LeftCost < pInfo.PVisit.MoneyAlert)
                    {
                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n���Ѿ�Ƿ�ѣ�\r\n�� " + pInfo.FT.LeftCost.ToString() + "\r\n�����ߣ� " + pInfo.PVisit.MoneyAlert.ToString();
                        }
                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n���Ѿ�Ƿ�ѣ�\r\n�� " + pInfo.FT.LeftCost.ToString() + "\r\n�����ߣ� " + pInfo.PVisit.MoneyAlert.ToString();
                        }

                        //if (System.Windows.Forms.MessageBox.Show(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ��" + pInfo.Name + "�� " + "�Ѿ�Ƿ�ѣ�\r\n\r\n�� " + pInfo.FT.LeftCost.ToString() + "\r\n\r\n�����ߣ� " + pInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n�Ƿ���������Ժ��", "ѯ��", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
                        //{
                        //    return false;
                        //}
                    }
                }
                catch
                {
                }
            }
            #endregion

            #region ��Ҫ��������Ϣ��ʾ�ڽ�����
            if (!isSave)
            {
                this.lblWarningInfo.Text = GetPaientInfo(pInfo) + "\r\n\r\n" + stopMessage;
                return -1;
            }
            #endregion

            #region 9����Ժ���������Ϊ��

            if (this.cmbZg.Tag == null || string.IsNullOrEmpty(cmbZg.Tag.ToString()))
            {
                stopMessage += "\r\n\r\n�ﲡ����Ҫ�󣬳�Ժ���������Ϊ�գ�\r\n";
                //this.Err = "���ݲ�����Ҫ�󣬳�Ժ���������Ϊ�գ�";
                //return -1;
            }
            #endregion

            FS.HISFC.Components.RADT.Forms.frmMessageShow frmMessage = new FS.HISFC.Components.RADT.Forms.frmMessageShow();
            frmMessage.Clear();
            if (!string.IsNullOrEmpty(checkMessage))
            {
                frmMessage.SetPatientInfo(pInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + babyinfo.Name + "��");
                frmMessage.SetTipMessage("������������δ����,�Ƿ���������Ժ��");
                frmMessage.SetMessage(checkMessage);
                frmMessage.SetPerfectWidth();
                if (frmMessage.ShowDialog() == DialogResult.No)
                {
                    return -1;
                }
            }

            if (!string.IsNullOrEmpty(stopMessage))
            {
                frmMessage.SetPatientInfo(babyinfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + babyinfo.Name + "��");
                frmMessage.SetTipMessage("������������δ����,���ܼ��������Ժ��");
                frmMessage.SetMessage(stopMessage);
                frmMessage.SetPerfectWidth();
                frmMessage.HideNoButton();
                frmMessage.ShowDialog();
                return -1;
            }

            if (!string.IsNullOrEmpty(this.Err))
            {
                return -1;
            }
            else
            {
                return 1;
            }
        }



        private string GetPaientInfo(FS.HISFC.Models.RADT.PatientInfo pInfo)
        {
            return pInfo.PID.PatientNO//סԺ��
                        + "  " + pInfo.Name //����
                        + "  " + pInfo.Sex.Name //�Ա�
                        + "  " + this.inpatient.GetAge(pInfo.Birthday)//����
                        + "  " + pInfo.Pact.Name//��ͬ��λ
                        + "  סԺ���ڣ�" + pInfo.PVisit.InTime.ToString("yyyy.MM.dd")//סԺ����
                        ;
        }

        /// <summary>
        /// ��ӡ��Ժ֪ͨ��
        /// </summary>
        private void PrintOutNote()
        {
            if (myPatientInfo == null)
            {
                return;
            }
            this.GetOutInfo();

            if (IPrintInHosNotice != null)
            {
                IPrintInHosNotice.SetValue(myPatientInfo);
                if (((FS.HISFC.Models.Base.Employee)this.inpatient.Operator).IsManager)
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
                print.SetPatientInfo(myPatientInfo);
                //print.NameFlag = this.cmbOutpatientAim.Tag.ToString().Trim();
                //print.PrintPreview();
                print.Print();
            }
        }

        /// <summary>
        /// ��������ȵ����his service�ӿ�
        /// {1E0B1D27-05E1-43a6-B3D1-C33BC970C1DD}
        /// </summary>
        /// <returns></returns>
        public void postHisSatisfiction(string patientid, string departmentid)
        {
            string url = @"http://192.168.34.9:8020/IbornMobileService.asmx";
            //string url = @"http://localhost:8080/IbornMobileService.asmx";
            #region ������װ��ʽ��req

            //<?xml version="1.0" encoding="UTF-8"?>
            //<req>��
            //  <patientId>his����</patientId>           ��   his����
            //  <departmentId>���Ҳ���id</departmentId>   ��   ���Ҳ���id
            //  <patientName>����</patientName> ��   ����
            //  <hospitalId>ҽԺid</hospitalId> �� ҽԺ����
            //  <questionnaireId>�ʾ�id</questionnaireId> �� �ʾ�id
            //</req>
            #endregion

            string requestStr = @"<?xml version='1.0' encoding='UTF-8'?>
                            <req>
                               <patientId>{0}</patientId>
                               <patientName>{1}</patientName> 
                               <healthCardNo>{2}</healthCardNo>
                               <departmentId>{3}</departmentId>
                               <questionnaireId>{4}</questionnaireId> 
                               <hospitalId>{5}</hospitalId>
                            </req>";

            string relay = string.Format(requestStr, patientid, "", patientid, departmentid/*����id*/, "", "");

            //��������ȵķ��Ͷ��ţ�������֤�ɴ���
            string resultXml = FS.HISFC.BizProcess.Integrate.WSHelper.InvokeWebService(url, "getCRMWechatApi", new string[] { relay }) as string;
        }

        #endregion

        #region �¼�

        private void button1_Click(object sender, System.EventArgs e)
        {
            ((Control)sender).Enabled = false;
            if (this.Save() > 0)//�ɹ�
            {
                MessageBox.Show(Err);

                string cardNo = myPatientInfo.PID.CardNO;
                //his service����������ʾ�
                //�ж��Ƿ�����
                //{C2C33FC9-E95F-4484-B01B-E3148809FABB}
                if(this.isSendWechat)
                    this.postHisSatisfiction(cardNo, "סԺ�շ�");

                base.OnRefreshTree();
                ((Control)sender).Enabled = true;
                return;
            }
            else
            {
                if (Err != "")
                {
                    MessageBox.Show(Err, "����", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            ((Control)sender).Enabled = true;
        }

        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            this.strInpatientNo = (neuObject as FS.FrameWork.Models.NeuObject).ID;
            if (this.strInpatientNo != null || this.strInpatientNo != "")
            {
                try
                {
                    RefreshList(strInpatientNo);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                }
            }
            return 0;
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.InitControl();
            return null;
        }

        /// <summary>
        /// ��Ժ֪ͨ����ӡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintOutNote();
            MessageBox.Show("�Ѵ�ӡ��Ժ֪ͨ�����뼰ʱ�����Ժ�Ǽǣ�");
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

    }

    /// <summary>
    /// ѡ��״̬
    /// </summary>
    public enum CheckState
    {
        /// <summary>
        /// ������
        /// </summary>
        No,

        /// <summary>
        /// ����
        /// </summary>
        Yes,

        /// <summary>
        /// ��ʾѡ��
        /// </summary>
        Check
    }


    /// <summary>
    /// ��Ժ�Զ�ֹͣҽ����ֹͣҽ��
    /// </summary>
    public enum AotuDcDoct
    {
        /// <summary>
        /// סԺҽʦ���ܴ���
        /// </summary>
        InpatientDoct,

        /// <summary>
        /// ����ҽ��
        /// </summary>
        DirectorDoct,

        /// <summary>
        /// ������Ժҽ����ҽ��
        /// </summary>
        ExecutOutDoct
    }

}
