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

namespace Neusoft.HISFC.Components.RADT.Controls
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
    public partial class ucPatientOut : Neusoft.FrameWork.WinForms.Controls.ucBaseControl, Neusoft.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucPatientOut()
        {
            InitializeComponent();
        }

        private void ucPatientOut_Load(object sender, EventArgs e)
        {

        }

        #region ����

        Neusoft.HISFC.BizProcess.Integrate.Manager manager = new Neusoft.HISFC.BizProcess.Integrate.Manager();
        Neusoft.HISFC.BizProcess.Integrate.RADT radt = new Neusoft.HISFC.BizProcess.Integrate.RADT();
        Neusoft.HISFC.BizLogic.RADT.InPatient inpatient = new Neusoft.HISFC.BizLogic.RADT.InPatient();

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        private Neusoft.HISFC.Models.RADT.PatientInfo myPatientInfo = null;
        //Neusoft.HISFC.BizLogic.HealthRecord.ICD icdManager = new Neusoft.HISFC.BizLogic.HealthRecord.ICD();

        /// <summary>
        /// ����������{28C63B3A-9C64-4010-891D-46F846EA093D}
        /// </summary>
        private Neusoft.FrameWork.Management.ControlParam ctlMgr = new Neusoft.FrameWork.Management.ControlParam();

        /// <summary>
        /// סԺ�����㷨�ӿ�
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob iDealSubjob = null;

        Neusoft.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Fee();

        private Neusoft.HISFC.BizProcess.Integrate.Order orderIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Order();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        Neusoft.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new Neusoft.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ��ǰ��½��Ա��������Ϣ���ݣ�
        /// </summary>
        private Neusoft.HISFC.Models.Base.Employee Oper = new Neusoft.HISFC.Models.Base.Employee();

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        string Err = "";

        /// <summary>
        /// ��Ժ�Ǽǵ��ýӿ�
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.RADT.IPatientOut IPatientOut = null;

        /// <summary>
        /// ��Ժ֪ͨ����ӡ�ӿ�
        /// </summary>
        private Neusoft.HISFC.BizProcess.Interface.IPrintInHosNotice IPrintInHosNotice = null;

        /// <summary>
        /// ��ǰ����סԺ��ˮ��
        /// </summary>
        string strInpatientNo;

        /// <summary>
        /// �Ƿ񱣴����ʾ��ӡ��Ժ֪ͨ��
        /// </summary>
        private bool isPrintOutNote = false;

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

        #endregion

        #region ����

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        private void InitControl()
        {
            try
            {
                this.cmbZg.AddItems(manager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.ZG));
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
                    IPatientOut = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.RADT.IPatientOut)) as Neusoft.HISFC.BizProcess.Interface.RADT.IPatientOut;
                }

                if (IPrintInHosNotice == null)
                {
                    IPrintInHosNotice = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IPrintInHosNotice)) as Neusoft.HISFC.BizProcess.Interface.IPrintInHosNotice;
                }

                if (isOutDCOrder == -1)
                {
                    Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam controlMgr = new Neusoft.HISFC.BizProcess.Integrate.Common.ControlParam();
                    isOutDCOrder = controlMgr.GetControlParam<int>("HNZY20", true, 0);
                }

                //��ӳ�Ժ���
                this.cmbDiag.AddItems(SOC.HISFC.BizProcess.Cache.Order.GetICD10());

                //���Ѫ��
                this.cmbBloodType.AddItems(Neusoft.HISFC.Models.RADT.BloodTypeEnumService.List());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ���û�����Ϣ���ؼ�
        /// </summary>
        /// <param name="PatientInfo"></param>
        private void SetPatientInfo(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo)
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

            Neusoft.FrameWork.Public.ObjectHelper helper = new Neusoft.FrameWork.Public.ObjectHelper(manager.GetConstantList(Neusoft.HISFC.Models.Base.EnumConstant.PAYKIND));
            this.txtBalKind.Text = helper.GetName(patientInfo.Pact.PayKind.ID);

            this.cmbDiag.Text = myPatientInfo.MainDiagnose; //��Ժ���

            this.txtBedNo.Text = patientInfo.PVisit.PatientLocation.Bed.ID;	//����
            this.txtFreePay.Text = patientInfo.FT.LeftCost.ToString();		//ʣ��Ԥ����
            this.txtTotcost.Text = patientInfo.FT.TotCost.ToString();		//�ܽ��

            //[2011-6-2] zhaozf ת��Ĭ��ѡ���һ��
            if (patientInfo.PVisit.ZG.ID != "0")
            {
                this.cmbZg.Text = "";
                this.cmbZg.Tag = patientInfo.PVisit.ZG.ID;						//ת��    
            }

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

                foreach (Neusoft.HISFC.Models.Invalid.CShiftData myCShiftDate in alShiftDataInfo)
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
                if (myPatientInfo.PVisit.PatientLocation.NurseCell.ID != ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Nurse.ID)
                {
                    MessageBox.Show("�����Ѳ��ڱ�����,��ˢ�µ�ǰ����", "��ʾ");
                    myPatientInfo = new Neusoft.HISFC.Models.RADT.PatientInfo();
                }

                if (IPatientOut != null)
                {
                    if (IPatientOut.BeforePatientOut(myPatientInfo, Neusoft.FrameWork.Management.Connection.Operator) < 0)
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
        public int DealSubjobByInpatient(Neusoft.HISFC.Models.RADT.PatientInfo patientInfo, Neusoft.HISFC.Models.Order.Inpatient.Order order, ArrayList alOrders, ref ArrayList alSubOrders, ref string errInfo)
        {
            if (iDealSubjob == null)
            {
                iDealSubjob = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.BizProcess.Integrate.Order), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob)) as Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob;
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
        Neusoft.HISFC.Models.Fee.Item.Undrug subUndrug = new Neusoft.HISFC.Models.Fee.Item.Undrug();

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
        private Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList CreateFeeItemList(Neusoft.HISFC.Models.Base.Item item, Neusoft.HISFC.Models.RADT.PatientInfo patient)
        {
            subUndrug = new Neusoft.HISFC.Models.Fee.Item.Undrug();
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
            Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = new Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList();
            feeItem.IsBaby = patient.IsBaby;
            feeItem.Item = subUndrug;
            feeItem.NoBackQty = subUndrug.Qty;
            feeItem.RecipeNO = feeIntegrate.GetUndrugRecipeNO();
            feeItem.Patient.Pact.PayKind.ID = patient.Pact.PayKind.ID;
            feeItem.TransType = Neusoft.HISFC.Models.Base.TransTypes.Positive;
            feeItem.FeeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            ((Neusoft.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.NurseCell.ID = patient.PVisit.PatientLocation.NurseCell.ID;
            feeItem.RecipeOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            feeItem.ExecOper.Dept.ID = patient.PVisit.PatientLocation.Dept.ID;
            if (patient.PVisit.AdmittingDoctor.ID == null || patient.PVisit.AdmittingDoctor.ID == "")
                patient.PVisit.AdmittingDoctor.ID = "�ռƷ�";

            feeItem.RecipeOper.ID = patient.PVisit.AdmittingDoctor.ID;
            feeItem.PayType = Neusoft.HISFC.Models.Base.PayTypes.Balanced;
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
            feeItem.FTSource = new Neusoft.HISFC.Models.Fee.Inpatient.FTSource("220");//��Ժ���գ��ٻغ��˷�
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

                if (Neusoft.FrameWork.Function.NConvert.ToInt32(rev) > 0)
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
                iDealSubjob = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(Neusoft.HISFC.BizProcess.Integrate.Order), typeof(Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob)) as Neusoft.HISFC.BizProcess.Interface.Order.IDealSubjob;
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
            foreach (Neusoft.HISFC.Models.Order.ExecOrder execOrder in alExecOrders)
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
                int rev = this.DealSubjobByInpatient(myPatientInfo, (Neusoft.HISFC.Models.Order.Inpatient.Order)alOrders[0],
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
                    Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = null;
                    foreach (Neusoft.HISFC.Models.Base.Item item in alSubOrders)
                    {
                        feeItem = new Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList();
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
            //Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
            foreach (Neusoft.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeItems)
            {
                if (this.feeIntegrate.FeeItem(myPatientInfo, feeItem) == -1)
                {
                    //Neusoft.FrameWork.Management.PublicTrans.RollBack();
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
                    //Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    errInfo = orderIntegrate.Err;
                    this.ctlMgr.WriteErr();
                    return -1;
                }
            }
            catch (Exception ex)
            {
                //Neusoft.FrameWork.Management.PublicTrans.RollBack();
                errInfo = ex.Message;
                this.ctlMgr.WriteErr();
                return -1;
            }

            //Neusoft.FrameWork.Management.PublicTrans.Commit();

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
                Neusoft.HISFC.Models.Order.Inpatient.Order orderObj = null;
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
                        this.dtOutDate.Value.ToString("yyyy��MM��dd��") + "  �Ƿ������", "��ʾ",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.No)
                    {
                        this.Err = "";
                        return -1;
                    }
                }
            }

            if (this.CheckOut() != 1)
            {
                return -1;
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
            if (myPatientInfo.PVisit.PatientLocation.NurseCell.ID != ((Neusoft.HISFC.Models.Base.Employee)Neusoft.FrameWork.Management.Connection.Operator).Nurse.ID)
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

                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                try
                {
                    //�Ǽǻ���Ѫ��
                    myPatientInfo.BloodType.ID = this.cmbBloodType.Tag;
                    if (this.inpatient.UpdateBloodType(myPatientInfo.ID, myPatientInfo.BloodType.ID.ToString()) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����Ѫ��ʧ�ܣ�" + inpatient.Err);
                        return -1;
                    }

                    //�����޸�Ҫע�⣡����������������������
                    //ҽ��ֻ�Ǳ���ת����ѣ���Ҫ�޸Ļ���״̬
                    if (this.inpatient.UpdateZGInfo(myPatientInfo, myPatientInfo.PVisit.InState.ID.ToString()) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("������Ϣʧ�ܣ�" + inpatient.Err);
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = ex.Message;
                    return -1;
                }
                Neusoft.FrameWork.Management.PublicTrans.Commit();

                Err = "������Ϣ�ɹ�����֪ͨ��ʿ�����Ժ������";
                #endregion
            }
            else
            {
                #region ��ʿ�����Ժ

                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();

                try
                {

                    //�Ǽǻ���Ѫ��
                    myPatientInfo.BloodType.ID = this.cmbBloodType.Tag;
                    if (this.inpatient.UpdateBloodType(myPatientInfo.ID, myPatientInfo.BloodType.ID.ToString()) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����Ѫ��ʧ�ܣ�" + inpatient.Err);
                        return -1;
                    }

                    //����סԺ�����
                    if (this.inpatient.UpdatePatientDiag(myPatientInfo.ID, this.cmbDiag.Text) < 0)
                    {
                        this.Err = "���»�����ϳ���!";
                        return -1;
                    }

                    //��Ժ�Ǽ�

                    ///���ӹ̶����õ��շ�
                    if (this.feeIntegrate.SupplementBedFee(myPatientInfo) == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = this.feeIntegrate.Err;
                        return -1;
                    }


                    int i = radt.OutPatient(myPatientInfo);
                    this.Err = radt.Err;
                    if (i == -1)��//ʧ��
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        return -1;
                    }
                    else if (i == 0)//ȡ��
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "";
                        return 0;
                    }

                    if (this.isAutoDcOrder && isOutDCOrder == 1)
                    {
                        if (this.AutoDcOrder(ref Err) == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            return -1;
                        }
                    }

                    if (this.OutFee() == -1)
                    {
                        Neusoft.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = errInfo;
                        return -1;
                    }

                    if (IPatientOut != null)
                    {
                        if (IPatientOut.OnPatientOut(this.myPatientInfo, this.inpatient.Operator) == -1)
                        {
                            Neusoft.FrameWork.Management.PublicTrans.RollBack();
                            this.Err = IPatientOut.ErrInfo;
                            return -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Neusoft.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = ex.Message;
                    return -1;
                }

                Neusoft.FrameWork.Management.PublicTrans.Commit();

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
        private int IsAddOutOrder(ref Neusoft.HISFC.Models.Order.Inpatient.Order orderObj, ref string errInfo)
        {
            int rev = this.orderIntegrate.GetOutOrder(myPatientInfo.ID, ref orderObj);
            if (rev == -1)
            {
                errInfo = orderIntegrate.Err;
                return -1;
            }
            else if (rev == 0)
            {
                errInfo = "����" + myPatientInfo.Name + "��δ������Ժҽ����";
                return 0;
            }

            if (orderObj == null || orderObj.ReciptDoctor == null || string.IsNullOrEmpty(orderObj.ReciptDoctor.ID))
            {
                errInfo = "����" + myPatientInfo.Name + "��δ������Ժҽ����";
                return 0;
            }
            return 1;
        }

        /// <summary>
        /// ����Ƿ���Լ��������Ժ�Ǽ�
        /// </summary>
        /// <returns></returns>
        private int CheckOut()
        {
            //��������ʾͳһ�ŵ�һ��
            if (string.IsNullOrEmpty(this.cmbDiag.Text) || this.cmbDiag.Tag == null)
            {
                MessageBox.Show("��Ժ��ϲ���Ϊ��,����д��", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return -1;
            }


            //��Ҫ��ʾѡ��Ķ���
            string checkMessage = "";

            //��ʾ��ֹ�Ķ���
            string stopMessage = "";

            this.Err = "";

            //ʵʱ��ȡ���µľ����ߡ�������Ϣ
            Neusoft.HISFC.BizLogic.RADT.InPatient inpatientMgr = new Neusoft.HISFC.BizLogic.RADT.InPatient();
            Neusoft.HISFC.Models.RADT.PatientInfo pInfo = inpatientMgr.QueryPatientInfoByInpatientNO(myPatientInfo.ID);
            if (pInfo != null)
            {
                myPatientInfo = pInfo;
            }

            Classes.Function funMgr = new Neusoft.HISFC.Components.RADT.Classes.Function();

            Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid obj = Neusoft.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid)) as Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid;
            if (obj != null)
            {
                bool bl = obj.IsPatientShiftValid(myPatientInfo, Neusoft.HISFC.Models.Base.EnumPatientShiftValid.O, ref stopMessage);
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
                string ReturnApplyItemInfo = Neusoft.HISFC.Components.RADT.Classes.Function.CheckReturnApply(this.myPatientInfo.ID);
                if (ReturnApplyItemInfo != null)
                {
                    string[] item = ReturnApplyItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {
                        if (i <= 2)
                        {
                            tip += item[i] + "\r";
                            if (i == item.Length - 1 || i == 2)
                            {
                                tip += "   ��";
                            }
                        }
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
                //���Ӳ�ѯ�����Ƿ���δ��˵���ҩ��¼,Ϊ��Ժ�Ǽ��ж���
                int returnValue = this.pharmacyIntegrate.QueryNoConfirmQuitApply(myPatientInfo.ID);
                if (returnValue == -1)
                {
                    MessageBox.Show("��ѯ������ҩ������Ϣ����!" + this.pharmacyIntegrate.Err);

                    return -1;
                }
                if (returnValue > 0) //�����뵫��û�к�׼����ҩ��Ϣ
                {
                    if (this.isCanOutWhenQuitDrugApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�����δ��˵���ҩ������Ϣ��";
                        //DialogResult result = MessageBox.Show("�û�����δ��˵���ҩ������Ϣ!�Ƿ���������Ժ?", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                        //if (result == DialogResult.No)
                        //{
                        //    this.Err = "����ϵҩ��ȷ����ҩ������ȡ����ҩ��";
                        //    return -1;
                        //}
                    }
                    else if (this.isCanOutWhenQuitDrugApplay == CheckState.No)
                    {
                        //this.Err += "�û�����δ��˵���ҩ������Ϣ!����ϵҩ��ȷ����ҩ������ȡ����ҩ��" + "\r\n";
                        stopMessage += "\r\n\r\n�����δ��˵���ҩ������Ϣ��";
                        //return -1;
                    }
                }
            }

            #endregion

            #region 3���жϻ����Ǵ���δ��ҩ��ҩƷ ��ʾ�Ƿ����

            if (isCanOutWhenDrugApplay != CheckState.Yes)
            {
                string msg = Neusoft.HISFC.Components.RADT.Classes.Function.CheckDrugApplay(this.myPatientInfo.ID);
                if (msg != null)
                {
                    string[] item = msg.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {
                        if (i <= 2)
                        {
                            tip += item[i] + "\r";
                            if (i == item.Length - 1 || i == 2)
                            {
                                tip += "   ��";
                            }
                        }
                    }

                    if (this.isCanOutWhenDrugApplay == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n�����δ��ҩ��ҩƷ��Ŀ��\r\n" + tip;
                        //if (MessageBox.Show("��������δ��ҩ��ҩƷ��Ŀ���Ƿ���������Ժ��\n" + msg, "��ʾ"
                        //    , MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                        //{
                        //    this.Err = "����ϵҩ��ȷ�Ϸ�ҩ��";
                        //    return -1;
                        //}
                    }
                    else if (isCanOutWhenDrugApplay == CheckState.No)
                    {
                        //this.Err += "��������δ��ҩ��ҩƷ��Ŀ�����ҩ��������Ժ�Ǽǣ�\n" + msg;

                        stopMessage += "\r\n\r\n�����δ��ҩ��ҩƷ��Ŀ��\r\n" + msg;
                        //return -1;
                    }
                }
            }
            #endregion

            #region 4������δ�ն�ȷ����Ŀ����ʾ�Ƿ����

            if (isCanOutWhenUnConfirm != CheckState.Yes)
            {
                string UnConfirmItemInfo = Neusoft.HISFC.Components.RADT.Classes.Function.CheckUnConfirm(this.myPatientInfo.ID);
                if (UnConfirmItemInfo != null)
                {
                    string[] item = UnConfirmItemInfo.Split('\r');
                    string tip = "";
                    for (int i = 0; i < item.Length; i++)
                    {
                        if (i <= 2)
                        {
                            tip += item[i] + "\r";
                            if (i == item.Length - 1 || i == 2)
                            {
                                tip += "   ��";
                            }
                        }
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
                Neusoft.HISFC.Models.Order.Inpatient.Order inOrder = null;

                int rtn = this.orderIntegrate.IsOwnOrders(this.myPatientInfo.ID);
                if (rtn == -1)
                {
                    MessageBox.Show("��ѯ����ҽ������!\r", "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return -1;
                }
                else if (rtn == 1)  //�п�����ҽ���Ĳż���Ƿ��г�Ժҽ��
                {
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
            }

            #endregion

            #region 6���жϳ����Ƿ�ȫͣ

            if (isCanOutWhenNoDcOrder != CheckState.Yes)
            {
                if (!funMgr.CheckIsAllLongOrderStop(myPatientInfo.ID))
                {
                    if (isCanOutWhenNoDcOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n��" + funMgr.Err;
                    }
                    else if (isCanOutWhenNoDcOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n��" + funMgr.Err;
                    }
                }
            }

            #endregion

            #region 7���ж��Ƿ���δ���ҽ��


            if (isCanOutWhenUnConfirmOrder != CheckState.Yes)
            {
                if (!funMgr.CheckIsAllOrderConfirm(myPatientInfo.ID))
                {
                    if (isCanOutWhenUnConfirmOrder == CheckState.Check)
                    {
                        checkMessage += "\r\n\r\n��" + funMgr.Err;
                    }
                    else if (isCanOutWhenUnConfirmOrder == CheckState.No)
                    {
                        stopMessage += "\r\n\r\n��" + funMgr.Err;
                    }
                }
            }

            #endregion

            #region 8���ж��Ƿ���δ�շѵķ�ҩƷҽ��ִ�е�

            if (isCanOutWhenNoFeeExecUndrugOrder != CheckState.Yes)
            {
                //////=============����һ���ַ��� -- unfee|||countWarnings
                string returnStr = Neusoft.HISFC.Components.RADT.Classes.Function.CheckExecOrderCharge(this.myPatientInfo.ID);
                if (returnStr != null)
                {
                    string[] strArray = returnStr.Split(new char[3] { '|', '|', '|' });

                    if (Convert.ToInt32(strArray[3]) > 0)
                    {
                        string[] item = strArray[0].Split('\r');
                        string tip = "";
                        for (int i = 0; i < item.Length; i++)
                        {
                            if (i <= 2)
                            {
                                tip += item[i] + "\r";
                                if (i == item.Length - 1 || i == 2)
                                {
                                    tip += "   ��";
                                }
                            }
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

            #region 9����Ժ���������Ϊ��

            if (this.cmbZg.Tag == null || string.IsNullOrEmpty(cmbZg.Tag.ToString()))
            {
                stopMessage += "\r\n\r\n�ﲡ����Ҫ�󣬳�Ժ���������Ϊ�գ�\r\n";
                //this.Err = "���ݲ�����Ҫ�󣬳�Ժ���������Ϊ�գ�";
                //return -1;
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

                string rev = funMgr.ExecSqlReturnOne(string.Format(sql, myPatientInfo.ID));
                try
                {
                    if (Neusoft.FrameWork.Function.NConvert.ToInt32(rev) > 0)
                    {
                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n�����δ��ɵ��������뵥��";
                        }
                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n�����δ��ɵ��������뵥��";
                        }
                        //System.Windows.Forms.MessageBox.Show("���ߡ�" + myPatientInfo.Name + "������δ��ɵ�����������������Ժ������\r\r\r\n�������������ϵ�����ң�", "��ʾ", System.Windows.Forms.MessageBoxButtons.OK);
                        //return false;
                    }
                }
                catch
                {
                }
            }

            #endregion

            #region Ƿ���ж�

            if (isCanOutWhenLackFee != CheckState.Yes)
            {
                try
                {
                    if (myPatientInfo.PVisit.MoneyAlert != 0 && myPatientInfo.FT.LeftCost < this.myPatientInfo.PVisit.MoneyAlert)
                    {
                        if (isCanOutWhenUnFeeUOApply == CheckState.Check)
                        {
                            checkMessage += "\r\n\r\n���Ѿ�Ƿ�ѣ�\r\n�� " + myPatientInfo.FT.LeftCost.ToString() + "\r\n�����ߣ� " + myPatientInfo.PVisit.MoneyAlert.ToString();
                        }
                        else if (isCanOutWhenUnFeeUOApply == CheckState.No)
                        {
                            stopMessage += "\r\n\r\n���Ѿ�Ƿ�ѣ�\r\n�� " + myPatientInfo.FT.LeftCost.ToString() + "\r\n�����ߣ� " + myPatientInfo.PVisit.MoneyAlert.ToString();
                        }

                        //if (System.Windows.Forms.MessageBox.Show(myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ��" + myPatientInfo.Name + "�� " + "�Ѿ�Ƿ�ѣ�\r\n\r\n�� " + myPatientInfo.FT.LeftCost.ToString() + "\r\n\r\n�����ߣ� " + myPatientInfo.PVisit.MoneyAlert.ToString() + "\r\n\r\n�Ƿ���������Ժ��", "ѯ��", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.No)
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

            if (!string.IsNullOrEmpty(checkMessage))
            {
                if (MessageBox.Show(myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + myPatientInfo.Name + "��\r\n������������δ����,�Ƿ���������Ժ��\r\n\r\n" + checkMessage, "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return -1;
                }
            }

            if (!string.IsNullOrEmpty(stopMessage))
            {
                MessageBox.Show(myPatientInfo.PVisit.PatientLocation.Bed.ID.Substring(4) + "�� ���ߡ�" + myPatientInfo.Name + "��\r\n������������δ����,���ܼ��������Ժ��\r\n\r\n" + stopMessage, "����", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
                if (((Neusoft.HISFC.Models.Base.Employee)this.inpatient.Operator).IsManager)
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

        #endregion

        #region �¼�

        private void button1_Click(object sender, System.EventArgs e)
        {
            ((Control)sender).Enabled = false;
            if (this.Save() > 0)//�ɹ�
            {
                MessageBox.Show(Err);
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
            this.strInpatientNo = (neuObject as Neusoft.FrameWork.Models.NeuObject).ID;
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

        protected override Neusoft.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
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
                return new Type[] { typeof(Neusoft.HISFC.BizProcess.Interface.IPatientShiftValid) };
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
