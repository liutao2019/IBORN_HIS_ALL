using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using FS.HISFC.Models.Fee.Outpatient;
using FS.FrameWork.Management;
using FS.FrameWork.Function;
using System.Xml;
using System.IO;
//using FS.HISFC.Components.OutpatientFee.Controls;

namespace FS.HISFC.Components.OutpatientFee.SelfFee
{
    /// <summary>
    /// ucCharge<br></br>
    /// [��������: �����շ�������UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2006-2-28]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucChargeSelf : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.FeeInterface.ISIReadCard
    {
        /// <summary>
        /// ���캯��
        /// </summary>
        public ucChargeSelf()
        {
            InitializeComponent();
        }

        #region ����

        #region �������

        /// <summary>
        /// �Һ���Ϣ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientInfomation registerControl = null;

        /// <summary>
        /// ��Ŀ¼����
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientItemInputAndDisplay itemInputControl = null;

        /// <summary>
        /// �����Ϣ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationLeft leftControl = null;

        /// <summary>
        /// �շѵ����ؼ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee popFeeControl = null;

        /// <summary>
        /// �Ҳ���Ϣ��ʾ�ؼ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight rightControl = null;

        #endregion

        #region �ؼ�����

        /// <summary>
        /// �໼�ߵ�������
        /// </summary>
        protected Form fPopWin = new Form();

        /// <summary>
        /// ��ʾ������Ϣ
        /// </summary>
        protected Controls.ucShowPatients ucShow = new FS.HISFC.Components.OutpatientFee.Controls.ucShowPatients();

        /// <summary>
        /// toolBar
        /// </summary>
        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();

        #endregion

        #region ҵ������

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Pharmacy pharmacyIntegrate = new FS.HISFC.BizProcess.Integrate.Pharmacy();

        /// <summary>
        /// ��ҩƷҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Item undrugManager = new FS.HISFC.BizLogic.Fee.Item();

        /// <summary>
        /// �������ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.Outpatient outpatientManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// ����ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// �����շ�
        /// </summary>
        //{CEA4E2A5-A045-4823-A606-FC5E515D824D}
        protected  FS.HISFC.BizProcess.Integrate.Material.Material materialManager = new FS.HISFC.BizProcess.Integrate.Material.Material();
        #endregion

        #region ��ͨ����

        /// <summary>
        /// �շ���Ϣ
        /// </summary>
        protected ArrayList comFeeItemLists = new ArrayList();

        /// <summary>
        /// toolBarӳ��
        /// </summary>
        protected Hashtable hsToolBar = new Hashtable();

        /// <summary>
        /// ������Ŀ���
        /// </summary>
        protected FS.HISFC.Models.Base.ItemKind itemKind = FS.HISFC.Models.Base.ItemKind.All;
        /// <summary>
        /// �Ƿ����ۼƲ���
        /// </summary>
        private bool isAddUp = false;
        #endregion

        #region ҽ�ƴ����ӿڱ���

        /// <summary>
        /// ҽ�ƴ����ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy medcareInterfaceProxy = new FS.HISFC.BizProcess.Integrate.FeeInterface.MedcareInterfaceProxy();

        #endregion

        #region ���Ʊ���

        /// <summary>
        /// ҽ����HIS����ʱ�շ�
        /// </summary>
        protected bool isCanFeeWhenTotCostDiff = false;

        /// <summary>
        /// �Ƿ��շ�
        /// </summary>
        protected bool isFee = false;

        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        protected string msgInfo = string.Empty;

        /// <summary>
        /// ��ݼ�����·��
        /// </summary>
        protected string filePath = Application.StartupPath + @".\" + FS.FrameWork.WinForms.Classes.Function.SettingPath + @".\clinicShotcut.xml";

        /// <summary>
        /// �Ƿ�ؼ��ڲ�Ԥ����
        /// </summary>
        protected bool isPreFee = false;

        /// <summary>
        /// �Ƿ����ѡ����Ŀ�շ�//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
        /// </summary>
        protected bool isCanSelectItemAndFee = false;

        #endregion

        #region �������뵥�ӿ�
        //houwb ȥ���������뵥���� 2020-2-25 00:08:12
        //FS.ApplyInterface.HisInterface PACSApplyInterface = null;
        #endregion

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ����ѡ����Ŀ�շ�//{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ����ѡ����Ŀ�շ�")]
        public bool IsCanSelectItemAndFee
        {
            get
            {
                return this.isCanSelectItemAndFee;
            }
            set
            {
                this.isCanSelectItemAndFee = value;
            }
        }

        /// <summary>
        /// �Ƿ�ؼ��ڲ�Ԥ����
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ�ؼ��ڲ�Ԥ����")]
        public bool IsPreFee
        {
            get
            {
                return this.isPreFee;
            }
            set
            {
                this.isPreFee = value;
            }
        }

        /// <summary>
        /// ������Ŀ���
        /// </summary>
        [Category("�ؼ�����"), Description("���ص���Ŀ��� All���� Undrug��ҩƷ drugҩƷ")]
        public FS.HISFC.Models.Base.ItemKind ItemKind
        {
            set
            {
                this.itemKind = value;

            }
            get
            {
                return this.itemKind;
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        [Category("�ؼ�����"), Description("false:���� true:�շ�")]
        private bool isValidFee = false;
        public bool IsValidFee
        {
            set
            {
                this.isValidFee = value;

            }
            get
            {
                return this.isValidFee;
            }
        }
        /// <summary>
        /// �Ƿ����ۼƲ���
        /// </summary>
        [Category("�ؼ�����"), Description("�Ƿ����ۼƲ��� true���� false����")]
        public bool IsAddUp
        {
            get
            {
                return isAddUp;
            }
            set
            {
                isAddUp = value;
                if (!value)
                {
                    ToolStripButton tempTb = null;
                    tempTb = toolBarService.GetToolButton("��ʼ�ۼ�");
                    if (tempTb != null)
                    {
                        tempTb.Visible = false;
                    }
                    tempTb = toolBarService.GetToolButton("ȡ���ۼ�");
                    if (tempTb != null)
                    {
                        tempTb.Visible = false;
                    }
                    tempTb = toolBarService.GetToolButton("�����ۼ�");
                    if (tempTb != null)
                    {
                        tempTb.Visible = false;
                    }
                }
            }
        }
        #endregion

        #region ����

        #region ˽�з���

        /// <summary>
        /// ��ʼ�����Ʋ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� 01</returns>
        protected virtual int InitControlParams()
        {
            //ҽ����HIS����ʱ�շ�
            this.isCanFeeWhenTotCostDiff = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.FEE_WHEN_TOTDIFF, true, false);

            return 1;
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int Init()
        {
            this.InitControlParams();

            if (this.LoadPulgIns() == -1)
            {
                return -1;
            }

            this.InitRegisterControl();

            this.InitItemInputControl();

            this.InitRightControl();

            this.InitLeftControl();

            this.InitPopFeeControl();

            this.InitPopShowPatient();

            return 1;
        }

        /// <summary>
        /// ����
        /// </summary>
        protected virtual void ChangeRecipe()
        {
            //ArrayList feeDetails = this.itemInputControl.GetFeeItemListForCharge(false);
            ArrayList feeDetails = this.itemInputControl.GetFeeItemListForCharge();
            this.registerControl.ModifyFeeDetails = (ArrayList)feeDetails.Clone();
            this.registerControl.AddNewRecipe();
        }

        /// <summary>
        /// ��ʼ���໼�ߵ�������
        /// </summary>
        protected virtual void InitPopShowPatient()
        {
            fPopWin.Width = ucShow.Width + 10;
            fPopWin.MinimizeBox = false;
            fPopWin.MaximizeBox = false;
            fPopWin.Controls.Add(ucShow);
            ucShow.Dock = DockStyle.Fill;
            fPopWin.Height = 200;
            fPopWin.Visible = false;
            fPopWin.KeyDown += new KeyEventHandler(fPopWin_KeyDown);
            this.ucShow.SelectedPatient += new FS.HISFC.Components.OutpatientFee.Controls.ucShowPatients.GetPatient(ucShow_SelectedPatient);
        }

        /// <summary>
        /// ѡ�����¼�
        /// </summary>
        /// <param name="register"></param>
        protected virtual void ucShow_SelectedPatient(FS.HISFC.Models.Registration.Register register)
        {
            ((Control)this.registerControl).Focus();
            //this.registerControl.PatientInfo = register;

            if (register == null)
            {
                return;
            }

            //this.itemInputControl.PatientInfo = register;
            //�շ��ж�
            if (this.IsValidFee)
            {
                this.medcareInterfaceProxy.SetPactCode(register.Pact.ID);
                // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
                this.medcareInterfaceProxy.IsLocalProcess = false;

                long returnValue = this.medcareInterfaceProxy.Connect();
                if (returnValue == -1)
                {
                    MessageBox.Show(Language.Msg("���Ӵ����������ݿ�ʧ��!") + this.medcareInterfaceProxy.ErrCode);

                    this.Clear();
                    this.medcareInterfaceProxy.Disconnect();

                    return;
                }

                returnValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(register);
                if (returnValue != 1)
                {
                    MessageBox.Show(Language.Msg("��ô������߻�����Ϣʧ��!") + this.medcareInterfaceProxy.ErrCode);

                    this.Clear();
                    this.medcareInterfaceProxy.Disconnect();

                    return;
                }
            }
            //by niuxinyuan
            this.registerControl.PatientInfo = register;

            if (register == null)
            {
                return;
            }

            this.itemInputControl.PatientInfo = register;
            //this.medcareInterfaceProxy.Disconnect();

            //��û��ߵĻ�����Ϣ
            ArrayList feeItemLists = this.outpatientManager.QueryChargedFeeItemListsByClinicNO(register.ID);
            if (feeItemLists == null)
            {
                MessageBox.Show(Language.Msg("������Ŀʧ��!") + outpatientManager.Err);

                return;
            }
            this.itemInputControl.RecipeSequence = this.registerControl.RecipeSequence;

            //��ʾ���ߵķַ���Ϣ
            this.registerControl.FeeDetails = (ArrayList)feeItemLists.Clone();

            this.itemInputControl.IsCanAddItem = this.registerControl.IsCanAddItem;
            //�õ���ǰ�����շ����к�
            this.itemInputControl.RecipeSequence = this.registerControl.RecipeSequence;
            //���շѿؼ���ʾ���߻��۵���Ϣ
            this.itemInputControl.ChargeInfoList = this.registerControl.FeeDetailsSelected;
        }

        /// <summary>
        /// �ж�����շ���Ŀ�Ƿ�ͣ�õ�
        /// </summary>
        /// <param name="feeItemLists">Ҫ�жϵķ�����ϸ</param>
        /// <returns>�ɹ� true ʧ�� false</returns>
        protected virtual bool IsItemValid(ArrayList feeItemLists)
        {
            string tmpValue = "0";

            bool isJudgeValid = this.controlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.Const.STOP_ITEM_WARNNING, false, false);

            if (!isJudgeValid) //�������Ҫ�жϣ�Ĭ�϶�û��ͣ��
            {
                return true;
            }

            foreach (FeeItemList f in feeItemLists)
            {
                if (f.Item.ID == "999")
                {
                    continue;
                }

                //if (f.Item.IsPharmacy)
                if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    FS.HISFC.Models.Pharmacy.Item drugItem = this.pharmacyIntegrate.GetItem(f.Item.ID);
                    if (drugItem == null)
                    {
                        MessageBox.Show(Language.Msg("��ѯҩƷ��Ŀ����!") + pharmacyIntegrate.Err);

                        return false;
                    }
                    if (drugItem.IsStop)
                    {
                        MessageBox.Show("[" + drugItem.Name + Language.Msg("]�Ѿ�ͣ��!����֤���շ�!"));

                        return false;
                    }
                }
                else
                {
                    FS.HISFC.Models.Fee.Item.Undrug undrugItem = this.undrugManager.GetUndrugByCode(f.Item.ID);
                    if (undrugItem == null)
                    {
                        MessageBox.Show(Language.Msg("��ѯ��ҩƷ��Ŀ����!") + undrugManager.Err);

                        return false;
                    }
                    if (undrugItem.ValidState != "1")//ͣ��
                    {
                        MessageBox.Show("[" + undrugItem.Name + Language.Msg("]�Ѿ�ͣ�û����������֤���շ�!"));

                        return false;
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// ���۱���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SaveCharge()
        {
            if (this.registerControl.PatientInfo == null || this.registerControl.PatientInfo.PID.CardNO == "")
            {
                MessageBox.Show(Language.Msg("û�л�����Ϣ!"));
                ((Control)this.registerControl).Focus();

                return -1;
            }
            this.registerControl.GetRegInfo();
            try
            {
                if (this.registerControl.PatientInfo.PID.CardNO == null || this.registerControl.PatientInfo.PID.CardNO == "")
                {
                    MessageBox.Show(Language.Msg("û�л�����Ϣ!"));
                    ((Control)this.registerControl).Focus();

                    return -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ((Control)this.registerControl).Focus();

                return -1;
            }
            if (!this.registerControl.IsPatientInfoValid())
            {
                ((Control)this.registerControl).Focus();

                return -1;
            }

            if (this.registerControl.PatientInfo.ChkKind == "1" || this.registerControl.PatientInfo.ChkKind == "2")
            {
                MessageBox.Show(Language.Msg("��컼����ʱ��֧�ֻ��۱���!"));

                return -1;
            }

            if (!this.itemInputControl.IsValid)
            {
                return -1;
            }

            this.itemInputControl.StopEdit();

            ArrayList feeDetails = this.registerControl.FeeSameDetails;

            if (feeDetails == null)
            {
                MessageBox.Show(Language.Msg("��÷�����Ϣ����!"));

                return -1;
            }

            int count = 0;

            foreach (ArrayList temp in feeDetails)
            {
                count += temp.Count;
            }

            if (count <= 0)
            {
                MessageBox.Show(Language.Msg("û�з�����Ϣ!"));
                ((Control)this.registerControl).Focus();

                return -1;
            }

            string errText = "";
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            bool returnValue = false;

            foreach (ArrayList temp in feeDetails)
            {
                //zhouxs 2007-11-25
                ArrayList a = new ArrayList();
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in temp)
                {
                    f.Invoice.ID = "";
                    f.FeeOper.OperTime = DateTime.MinValue;
                    f.InvoiceCombNO = "";
                    f.FeeOper.ID = "";
                    a.Add(f);
                }
                returnValue = feeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Save, this.registerControl.PatientInfo, null, null, a, null, null, ref errText);
                //returnValue = feeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Save, this.registerControl.PatientInfo, null, null,temp, null, ref errText);
                //end zhouxs
            }
            if (!returnValue)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                isFee = false;

                this.itemInputControl.SetFocus();//�ȼ��ϣ���֪���в���
                MessageBox.Show(errText);

                return -1;
            }
            else
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            isFee = false;
            msgInfo = Language.Msg("���۳ɹ�!");

            MessageBox.Show(msgInfo);

            this.Clear();

            return 1;
        }

        /// <summary>
        /// �շ�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int SaveFee()
        {
            decimal selfDrugCost = 0;//�Է�ҩ���
            decimal overDrugCost = 0;//����ҩ���
            decimal ownCost = 0;//�Էѽ��
            decimal pubCost = 0;//�籣֧�����
            decimal totCost = 0;//�ܽ��
            decimal payCost = 0;//�Ը����
            string errText = "";//������Ϣ
            decimal formerTotCost = 0;//�Աȵ��ܽ��

            if (this.registerControl.PatientInfo == null || this.registerControl.PatientInfo.PID.CardNO == null || this.registerControl.PatientInfo.PID.CardNO == string.Empty)
            {
                MessageBox.Show(Language.Msg("û�л�����Ϣ!"));
                ((Control)this.registerControl).Focus();

                return -1;
            }

            //�жϻ���¼�����Ƿ���Ϣ����
            if (!this.registerControl.IsPatientInfoValid())
            {
                ((Control)this.registerControl).Focus();

                return -1;
            }

            //���»�ùҺ���Ϣ
            this.registerControl.GetRegInfo();

            if (!this.itemInputControl.IsValid)
            {
                return -1;
            }

            //��Ŀ¼��ؼ�ֹͣ�༭
            this.itemInputControl.StopEdit();

            //��֤����������Ƿ�Ϸ�
            if (!this.leftControl.IsValid())
            {
                MessageBox.Show(this.leftControl.ErrText);
                this.leftControl.SetFocus();

                return -1;
            }

            //��õ�ǰ¼����Ŀ��Ϣ����
            this.comFeeItemLists = this.itemInputControl.GetFeeItemList();
            if (comFeeItemLists == null)
            {
                MessageBox.Show(this.itemInputControl.ErrText);
                ((Control)this.registerControl).Focus();

                return -1;
            }
            if (comFeeItemLists.Count <= 0)
            {
                MessageBox.Show(Language.Msg("û�з�����Ϣ!"));
                ((Control)this.registerControl).Focus();

                return -1;
            }

            //�ж��Ƿ�����Ŀͣ��
            if (!this.IsItemValid(comFeeItemLists))
            {
                this.itemInputControl.SetFocus();

                return -1;
            }



            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //��ʼ��������
            this.medcareInterfaceProxy.BeginTranscation();
            //���ô����ĺ�ͬ��λ����
            this.medcareInterfaceProxy.SetPactCode(this.registerControl.PatientInfo.Pact.ID);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;
            //���Ӵ����ӿ�
            long returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue == -1)
            {
                this.medcareInterfaceProxy.Rollback();
                MessageBox.Show(Language.Msg("ҽ�ƴ����ӿ�����ʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }



            //ɾ��������Ϊ�����������ԭ���ϴ�����ϸ
            returnValue = this.medcareInterfaceProxy.DeleteUploadedFeeDetailsAllOutpatient(this.registerControl.PatientInfo);

            //�����ϴ�������ϸ
            returnValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.registerControl.PatientInfo, ref comFeeItemLists);
            if (returnValue == -1)
            {
                #region {88364E78-EC32-450a-95E4-A589AD361F34}
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                this.medcareInterfaceProxy.Disconnect(); 
                #endregion
                MessageBox.Show(Language.Msg("�ϴ�������ϸʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            ////�ύ���Ͽ������ӿ�
            //this.medcareInterfaceProxy.Commit();

            //�����ӿڽ������,Ӧ�ù��Ѻ�ҽ��
            //returnValue = this.medcareInterfaceProxy.BalanceOutpatient(this.registerControl.PatientInfo, ref comFeeItemLists);
            returnValue = this.medcareInterfaceProxy.PreBalanceOutpatient(this.registerControl.PatientInfo, ref comFeeItemLists);
            if (returnValue == -1)
            {
                #region {88364E78-EC32-450a-95E4-A589AD361F34}
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                this.medcareInterfaceProxy.Disconnect();
                #endregion
                MessageBox.Show(Language.Msg("���ҽ��Ԥ������Ϣʧ��!") + this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }
            //�Ͽ������ӿ�����
            this.medcareInterfaceProxy.Rollback();
            this.medcareInterfaceProxy.Disconnect();
            FS.FrameWork.Management.PublicTrans.RollBack();



            //��õ�ǰϵͳʱ��
            DateTime nowTime = this.undrugManager.GetDateTimeFromSysDateTime();

            //����û�н��д�������ʱ�ķ����ܽ��
            foreach (FeeItemList f in comFeeItemLists)
            {
                //������Ѿ�����ϸ�˻�֧����,���ȿ���ֻ���Էѻ���,��ô���Էѵ���Ϊ0, �˻�֧������Ϊ�Էѽ��.
                if (this.registerControl.PatientInfo.Pact.ID == "1" && f.IsAccounted)
                {
                    if (f.FT.OwnCost > 0)
                    {
                        f.FT.PayCost += f.FT.OwnCost;
                        f.FT.OwnCost = 0;
                    }
                }

                f.FeeOper.OperTime = nowTime;
                formerTotCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
            }

            //���¼�����������ķ��ý��
            decimal rebateRate = 0;//{9A0B9D4F-9D7D-4cfe-9024-41030D6D75A7}
            foreach (FeeItemList f in comFeeItemLists)
            {
                totCost += f.FT.OwnCost + f.FT.PubCost + f.FT.PayCost;
                //if (f.Item.IsPharmacy)
                if (f.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
                {
                    overDrugCost += NConvert.ToDecimal(f.FT.ExcessCost);
                    selfDrugCost += NConvert.ToDecimal(f.FT.DrugOwnCost);
                }

                f.NoBackQty = f.Item.Qty;
                rebateRate += f.FT.RebateCost;//{9A0B9D4F-9D7D-4cfe-9024-41030D6D75A7}
            }
            ownCost = totCost - this.registerControl.PatientInfo.SIMainInfo.PubCost - this.registerControl.PatientInfo.SIMainInfo.PayCost;
            payCost += this.registerControl.PatientInfo.SIMainInfo.PayCost;
            pubCost += this.registerControl.PatientInfo.SIMainInfo.PubCost;

            //�жϴ�������ǰ�ͼ�����Ƿ����
            if (!this.isCanFeeWhenTotCostDiff && this.registerControl.PatientInfo.Pact.PayKind.ID == "02" && this.registerControl.PatientInfo.SIMainInfo.TotCost != formerTotCost)//��������
            {
                MessageBox.Show(Language.Msg("��Ժ�շ�ϵͳ���ܷ�����ҽ��ϵͳ���ܽ�����,������˶ԣ�"), Language.Msg("��ʾ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.itemInputControl.SetFocus();

                return -1;
            }

            //���н���2λС��
            ownCost = FS.FrameWork.Public.String.FormatNumber(ownCost, 2);
            payCost = FS.FrameWork.Public.String.FormatNumber(payCost, 2);
            pubCost = FS.FrameWork.Public.String.FormatNumber(pubCost, 2);
            totCost = FS.FrameWork.Public.String.FormatNumber(totCost, 2);

            //���¶����շѵ������
            this.popFeeControl = new SelfFee.frmDealBalanceSelf();
            this.popFeeControl.Init();
            this.popFeeControl.FeeButtonClicked += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateFee(popFeeControl_FeeButtonClicked);
            this.popFeeControl.ChargeButtonClicked += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(popFeeControl_ChargeButtonClicked);

            //�շѵ��������ֵ
            this.popFeeControl.PatientInfo = this.registerControl.PatientInfo;
            this.popFeeControl.SelfDrugCost = selfDrugCost;
            this.popFeeControl.OverDrugCost = overDrugCost;
            this.popFeeControl.RealCost = ownCost ;
            this.popFeeControl.OwnCost = ownCost - rebateRate;//{9A0B9D4F-9D7D-4cfe-9024-41030D6D75A7}
            this.popFeeControl.PayCost = payCost;
            this.popFeeControl.PubCost = pubCost + rebateRate;
            this.popFeeControl.TotCost = totCost;
            this.popFeeControl.TotOwnCost = ownCost - rebateRate;//{9A0B9D4F-9D7D-4cfe-9024-41030D6D75A7}
            //********************
            #region ������ϸ��ֵ
            this.popFeeControl.FeeDetails = comFeeItemLists;
            #endregion
            //********************

            //if (System.IO.File.Exists(Application.StartupPath + "\\chargeLED.exe") == true)
            //{
            //    try
            //    {
            //        FS.Common.Controls.Function.ShowPatientFee(this.ucRegInfo1.RInfo.Name, payCost + ownCost);
            //    }
            //    catch
            //    { }
            //}

            string invoiceNO = "";//��ǰ�շѷ�Ʊ��
            string realInvoiceNO = this.leftControl.InvoiceNO;//��ǰ��ʾ��Ʊ��

            FS.HISFC.Models.Base.Employee employee = this.managerIntegrate.GetEmployeeInfo(this.undrugManager.Operator.ID);

            string invoiceType = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE, false, "0");

            if (invoiceType == "2")//Ĭ�Ϸ�Ʊģʽ,��Ҫtrans֧��
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
                this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                //��ñ����շ���ʼ��Ʊ��
                int iReturnValue = this.feeIntegrate.GetInvoiceNO(employee, "C",ref invoiceNO, ref realInvoiceNO, ref errText);
                if (iReturnValue == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show(errText);

                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.RollBack();//���ڴ�ʱ����һ��Ҫ�շ�,����ȡ����Ʊ�žͻع�,���ⷢƱ�ߺ�
            }
            else//������ʽ����ҪTrans֧��
            {

                //��ñ����շ���ʼ��Ʊ��
                int iReturnValue = this.feeIntegrate.GetInvoiceNO(employee, "C",ref invoiceNO, ref realInvoiceNO, ref errText);
                if (iReturnValue == -1)
                {
                    MessageBox.Show(errText);

                    return -1;
                }
            }

            //������з�Ʊ�ͷ�Ʊ��ϸ�ļ���
            //********************
            #region ���ɷ�Ʊ����Ʊ��ϸ

            //{18B0895D-9F55-4d93-B374-69E96F296D0D}  ����ȡ��Ʊ������Bug����
            Class.Function.IsQuitFee = false;

            ArrayList balancesAndBalanceLists = Class.Function.MakeInvoice(this.feeIntegrate, this.registerControl.PatientInfo, comFeeItemLists, invoiceNO, realInvoiceNO, ref errText);
            #endregion
            //********************
            if (balancesAndBalanceLists == null)
            {
                MessageBox.Show(errText);

                return -1;
            }


            this.popFeeControl.InvoiceFeeDetails = (ArrayList)balancesAndBalanceLists[2];


            //���շѵ��������ֵ�շѷ�Ʊ��ϸ��Ϣ
            //********************
            #region ��Ʊ��ϸ��ֵ
            this.popFeeControl.InvoiceDetails = (ArrayList)balancesAndBalanceLists[1];
            #endregion
            //********************
            ///�����ҽ������ҽ����Ʊ�����⴦��,����Ϊ��ʱ����

            #region ��δ���
            if (this.registerControl.PatientInfo.Pact.PayKind.ID == "02")
            {
                foreach (Balance balance in (ArrayList)balancesAndBalanceLists[0])
                {
                    if (balance.Memo == "4")//���˷�Ʊ!
                    {
                        balance.FT.PubCost = pubCost;
                        balance.FT.PayCost = payCost;
                        balance.FT.OwnCost = balance.FT.TotCost - pubCost - payCost;
                    }
                    ArrayList tempFeeItemListArray = (ArrayList)balancesAndBalanceLists[2];
                    for (int i = 0; i < tempFeeItemListArray.Count; i++)
                    {

                        FeeItemList tempFeeItemList = ((ArrayList)tempFeeItemListArray[i])[0] as FeeItemList;

                        if (balance.Invoice.ID == tempFeeItemList.Invoice.ID)
                        {

                        }
                    }
                }
            }

            #endregion
            ////���շѵ��������ֵ�շѷ�Ʊ��Ϣ
            //********************
            #region ��Ʊ��ֵ
            this.popFeeControl.Invoices = (ArrayList)balancesAndBalanceLists[0];
            #endregion
            //********************
            //��ʾ�����շѲ��
            if (!((Control)this.popFeeControl).Visible)
            {
                ((Control)this.registerControl).Focus();
                this.popFeeControl.SetControlFocus();
                ((Control)this.popFeeControl).Location = new Point(this.Location.X + 150, this.Location.Y + 50);
                ((Form)this.popFeeControl).ShowDialog();
            }
            if (this.popFeeControl.IsPushCancelButton)
            {
                this.itemInputControl.SetFocus();
            }

            return 1;
        }

        //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť

        /// <summary>
        /// ˢ����Ŀ�б�
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int RefreshItem() 
        {
            (this.itemInputControl as ucDisplay).RefreshItem();

            return 1;
        }

        //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť ���

        #endregion

        /// <summary>
        /// ����
        /// </summary>
        protected virtual void Clear()
        {
            this.itemInputControl.Clear();
            this.registerControl.Clear();
            this.leftControl.Clear();
        }

        /// <summary>
        /// ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int LoadPulgIns()
        {
            //��ʼ�����߻�����Ϣ���;

            try
            {
                this.registerControl =  new ucPatientInfo();

                this.itemInputControl =   new ucDisplay() ;
                this.itemInputControl.ItemKind = itemKind;

                this.leftControl =   new ucInvoicePreview() ;
                //�����ж��շѻ��ǻ���
                this.leftControl.IsValidFee = this.IsValidFee;
                this.leftControl.IsPreFee = this.isPreFee;
                this.itemInputControl.LeftControl = this.leftControl;

                this.popFeeControl =   new SelfFee.frmDealBalanceSelf() ;

                this.rightControl =   new ucCostDisplay()  ;
                this.rightControl.IsPreFee = this.isPreFee;

                this.itemInputControl.RightControl = this.rightControl;
                //{EE98C7B7-AC32-4b2c-93A5-9A62A33D6457}
                ((ucDisplay)this.itemInputControl).IsCanSelectItemAndFee = this.isCanSelectItemAndFee;

                this.rightControl.SetMedcareInterfaceProxy(this.medcareInterfaceProxy);
            }
            catch (Exception e)
            {
                MessageBox.Show(Language.Msg("���� ���߻�����Ϣ���ʧ��!") + e.Message);

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ��ʼ�������շѲ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitPopFeeControl()
        {
            if (this.popFeeControl == null)
            {
                return -1;
            }

            this.popFeeControl.Init();

            //this.popFeeControl.FeeButtonClicked += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateFee(popFeeControl_FeeButtonClicked);
            //this.popFeeControl.ChargeButtonClicked += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(popFeeControl_ChargeButtonClicked);

            return 1;
        }

        /// <summary>
        /// ��Ӧ�����շѿؼ��Ļ��۱����¼�
        /// </summary>
        protected virtual void popFeeControl_ChargeButtonClicked()
        {
            this.SaveCharge();
        }

        /// <summary>
        /// �շѰ�ť����
        /// </summary>
        /// <param name="alPayModes">֧����ʽ��Ϣ</param>
        /// <param name="invoices">��Ʊ��Ϣ��������Ӧ��Ʊ�������Ϣ��ÿ�������Ӧһ����Ʊ��</param>
        /// <param name="invoiceDetails">��Ʊ��ϸ��Ϣ����Ӧ���ν����ȫ��������ϸ��</param>
        /// {283BD55B-83E0-4f35-86E8-152CF0C1DA85}
        /// <param name="invoiceFeeItemDetails">��Ʊ������ϸ��Ϣ������Ʊ�����ķ�����ϸ��ÿ�������Ӧ�÷�Ʊ�µķ�����ϸ��</param>
        protected virtual void popFeeControl_FeeButtonClicked(ArrayList balancePays, ArrayList invoices, ArrayList invoiceDetails, ArrayList invoiceFeeDetails)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            string errText = "";
            this.medcareInterfaceProxy.SetPactCode(this.registerControl.PatientInfo.Pact.ID);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;

            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            this.feeIntegrate.IsNeedUpdateInvoiceNO = true;

            long returnMedcareValue = this.medcareInterfaceProxy.Connect();
            if (returnMedcareValue != 1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.medcareInterfaceProxy.Rollback();
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڳ�ʼ��ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                return;
            }
            //�Ƿ������ϴ�
            if (this.medcareInterfaceProxy.IsUploadAllFeeDetailsOutpatient)
            {
                //�����ϴ��ߺ��ĵ�����

                #region his45 ����

                


                #region �����շ�
                //{143CA424-7AF9-493a-8601-2F7B1D635027}
                foreach (FeeItemList temfItem in comFeeItemLists)
                {
                    if (temfItem.Item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
                    {
                        temfItem.StockOper.Dept.ID = temfItem.ExecOper.Dept.ID;
                    }
                }
                //�����շѴ���
                if (materialManager.MaterialFeeOutput(comFeeItemLists) < 0)
                {
                    //errText = materialManager.Err;
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����շ�ʧ�ܣ�") + materialManager.Err);
                    return ;
                }
                #endregion


                bool returnValue = this.feeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, this.registerControl.PatientInfo,
                   invoices, invoiceDetails, comFeeItemLists, invoiceFeeDetails, balancePays, ref errText);
                this.registerControl.PatientInfo.SIMainInfo.InvoiceNo = ((FS.HISFC.Models.Fee.Outpatient.Balance)invoices[0]).Invoice.ID;


                #region  �����ӿ���(����ǿ���Ϻ�����);
                //���ú�ͬ��λ

                this.medcareInterfaceProxy.SetPactCode(this.registerControl.PatientInfo.Pact.ID);
                // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
                this.medcareInterfaceProxy.IsLocalProcess = false;

                returnMedcareValue = this.medcareInterfaceProxy.Connect();
                if (returnMedcareValue != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿڳ�ʼ��ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                    return;
                }
                returnMedcareValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.registerControl.PatientInfo, ref comFeeItemLists);
                if (returnMedcareValue != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿ��ϴ���ϸʧ��") + this.medcareInterfaceProxy.ErrMsg);
                    return;
                }
                returnMedcareValue = this.medcareInterfaceProxy.BalanceOutpatient(this.registerControl.PatientInfo, ref comFeeItemLists);
                if (returnMedcareValue != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ӿ��������ʧ��") + this.medcareInterfaceProxy.ErrMsg);
                    return;
                }
                #endregion

                if (!returnValue)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.medcareInterfaceProxy.Rollback();
                    if (errText != "")
                    {
                        MessageBox.Show(errText);
                    }

                    isFee = false;

                    return;
                }

                #region ������ȡ�ֽ��·־��
                ArrayList balancePaysClone = new ArrayList();
                foreach (BalancePay balancePay in balancePays)
                {
                    //�Ƿ�ʼ�ۼ�
                    if (registerControl.IsBeginAddUpCost)
                    {
                        if (balancePay.PayType.Name == "�ֽ�")
                        {
                            this.registerControl.AddUpCost += balancePay.FT.TotCost;
                        }
                    }
                    balancePaysClone.Add(balancePay.Clone());
                }
                #endregion



                this.medcareInterfaceProxy.Commit();
                this.medcareInterfaceProxy.Disconnect();
                FS.FrameWork.Management.PublicTrans.Commit();
                #region//��Ʊ��ӡ

                string invoicePrintDll = null;

                invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);

                // ���ķ�Ʊ��ӡ���ȡ��ʽ������ԭ����ʽ
                // 2011-08-04
                // �˴�������ʾ
                //if (invoicePrintDll == null || invoicePrintDll == string.Empty)
                //{
                //    MessageBox.Show("û�����÷�Ʊ��ӡ�������շ���ά��!");

                //}

                this.feeIntegrate.PrintInvoice(invoicePrintDll, this.registerControl.PatientInfo, invoices, invoiceDetails, comFeeItemLists, balancePaysClone, false, ref errText);

                #endregion

                this.popFeeControl.FTFeeInfo.User01 = ((FS.HISFC.Models.Fee.Outpatient.Balance)invoices[0]).DrugWindowsNO;

                //{21659409-F380-421f-954A-5C3378BB9FD6}
                this.rightControl.SetInfomation(this.registerControl.PatientInfo, this.popFeeControl.FTFeeInfo, comFeeItemLists, null);

                //���Ʊ��ιҺŻ�����Ϣ
                this.registerControl.PrePatientInfo = this.registerControl.PatientInfo.Clone();
                this.leftControl.InitInvoice();

                isFee = true;

                msgInfo = Language.Msg("�շѳɹ�!");

                MessageBox.Show(msgInfo);

                #region �������뵥 {6FAEEEC2-CF03-4b2e-B73F-92C1C8CAE1C0} ����������뵥 yangw 20100504
                //string isUseDL = controler.QueryControlerInfo("200212");
                //if (!string.IsNullOrEmpty(isUseDL) && isUseDL == "1")
                //{
                //    if (PACSApplyInterface == null)
                //    {
                //        PACSApplyInterface = new FS.ApplyInterface.HisInterface();
                //    }
                //    foreach (FeeItemList f in comFeeItemLists)
                //    {
                //        if (f.Item.SysClass.ID.ToString() == "UC")
                //        {
                //            try
                //            {
                //                string applyNo = outpatientManager.GetApplyNoByRecipeFeeSeq(f);
                //                int a = PACSApplyInterface.Charge(applyNo, "1");
                //            }
                //            catch (Exception e)
                //            {
                //                MessageBox.Show("�������뵥����" + e.Message);
                //            }
                //        }
                //    }
                //}
                #endregion

                this.Clear();

                #region ��ʾ��ҩ���� ��LIS�ӿ�, ��������
                //if (System.IO.File.Exists(Application.StartupPath + "\\chargeLED.exe") == true)
                //{
                //    try
                //    {
                //        if (this.frmBalance.ucDealBalance1.FTFeeInfo.User01 != null && this.frmBalance.ucDealBalance1.FTFeeInfo.User01.Length > 0)
                //        {
                //            FS.Common.Controls.Function.ShowPatientFee("�뵽" + this.frmBalance.ucDealBalance1.FTFeeInfo.User01 + "ȡҩ", this.frmBalance.ucDealBalance1.PayCost + this.frmBalance.ucDealBalance1.OwnCost);
                //        }
                //    }
                //    catch
                //    { }
                //}
                //if (this.dataToLis)
                //{
                //    #region ����LIS�ӿ�

                //    foreach (FS.HISFC.Models.Fee.OutPatient.FeeItemList feeItem in this.GetArrayToLis(this.ucChargeDisplay1.GetFeeItemListForCharge(), alFee))
                //    {
                //        if (feeItem.SysClass.ID.ToString() == "UL")
                //        {
                //            lisInterface.Function.LisSetClinicData(this.ucRegInfo1.RInfo, feeItem, FS.FrameWork.Management.PublicTrans.Trans);
                //        }
                //    }
                //    #endregion
                //}

                #endregion
                #endregion
            }
            else
            {
                //�������ϴ���С�汾������
                #region his4.5.0.1
                #region ҽ���ӿڳɹ���־λ
                Boolean isSucc = true;
                #endregion

                //ҽ������

                this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                #region �ϴ�ҽ����Ϣ
                //ȫ���߲����ϴ�����
                if (true)
                {
                    #region ��¡һ��֧����Ϣ
                    ArrayList balancePaysClone = new ArrayList();
                    BalancePay balancePayCA = null;
                    //��ͷ�ۼ�
                    decimal changeCost = decimal.Zero;

                    #region ���ֽ�֧���ģ���ͳ��֧���ģ����ʻ�֧���ı��浽��¡��֧����Ϣ�����У�����¼�ֽ�֧������Ϣ��balancePayCA������
                    foreach (BalancePay balancePay in balancePays)
                    {
                        //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                        //if (balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PS.ToString() ||
                        //    balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PB.ToString())

                        //����Ǳ����˻��� ͳ��(ҽԺ�渶)
                        if (balancePay.PayType.ID.ToString() == "PS" ||
                                balancePay.PayType.ID.ToString() == "PB")
                        
                        {
                            balancePaysClone.Add(balancePay.Clone());
                        }
                        // �ֽ�
                        //if (balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.CA.ToString())
                        if (balancePay.PayType.ID.ToString() == "CA")
                        {
                            balancePayCA = balancePay.Clone();
                            balancePaysClone.Add(balancePayCA);
                        }
                        changeCost += balancePay.FT.TotCost - balancePay.FT.RealCost;
                    }
                    #endregion

                    #region ��������֧����Ϣ�����ֽ�֧��������
                    // {93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                    foreach (BalancePay balancePay in balancePaysClone)
                    {
                        //if (!(balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PS.ToString() ||
                        //   balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PB.ToString() ||
                        //   balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.CA.ToString()))
                        //�����ʻ�,ͳ��(ҽԺ�渶),�ֽ�
                        if (!(balancePay.PayType.ID.ToString() == "PS" ||
                            balancePay.PayType.ID.ToString() == "PB" ||
                            balancePay.PayType.ID.ToString() == "CA"))
                        {
                            balancePayCA.FT.TotCost = balancePay.FT.TotCost;
                            balancePayCA.FT.RealCost = balancePay.FT.RealCost;
                        }
                    }
                    #endregion

                    #endregion

                    #region ����֧����ʽ��Ϣ
                    string mainInvoiceNO = string.Empty;
                    string mainInvoiceCombNO = string.Empty;
                    foreach (Balance balance in invoices)
                    {
                        //����Ʊ��Ϣ,������ֻ����ʾ��
                        if (balance.Memo == "5")
                        {
                            mainInvoiceNO = balance.ID;

                            continue;
                        }

                        //�Էѻ��߲���Ҫ��ʾ����Ʊ,��ôȡ��һ����Ʊ����Ϊ����Ʊ��
                        if (mainInvoiceNO == string.Empty)
                        {
                            mainInvoiceNO = balance.Invoice.ID;
                            mainInvoiceCombNO = balance.CombNO;
                        }
                    }

                    int payModeSeq = 1;

                    // ������ҵ���
                    FS.HISFC.BizLogic.Fee.InPatient inpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
                    inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                    foreach (BalancePay p in balancePays)
                    {
                        //p.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                        p.Invoice.ID = mainInvoiceNO;
                        p.TransType = FS.HISFC.Models.Base.TransTypes.Positive;
                        p.Squence = payModeSeq.ToString();
                        p.IsDayBalanced = false;
                        p.IsAuditing = false;
                        p.IsChecked = false;
                        p.InputOper.ID = inpatientManager.Operator.ID;
                        p.InputOper.OperTime = inpatientManager.GetDateTimeFromSysDateTime();
                        if (string.IsNullOrEmpty(p.InvoiceCombNO))
                        {
                            p.InvoiceCombNO = mainInvoiceCombNO;
                        }
                        p.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;

                        payModeSeq++;

                        //realCost += p.FT.RealCost;
                        int iReturn;
                        if (FS.FrameWork.Management.PublicTrans.Trans != null)
                        {
                            outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        }
                        iReturn = outpatientManager.InsertBalancePay(p);
                        if (iReturn == -1)
                        {

                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("����֧����ʽ�����!");
                            return;
                        }
                        FS.FrameWork.Management.PublicTrans.Commit();//��߲帺��¼,��˴��ύû�����⡣




                        #region �����ʻ�����ȡ��
                        //if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.YS.ToString())
                        //{
                        //    bool returnValue = feeIntegrate.AccountPay(this.registerControl.PatientInfo.PID.CardNO, p.FT.TotCost, p.Invoice.ID, p.InputOper.Dept.ID);
                        //    if (!returnValue)
                        //    {
                        //        MessageBox.Show("��ȡ�����˻�ʧ��!");

                        //        return;
                        //    }
                        //} 
                        #endregion
                    }
                    #endregion
                    //�������ս����־
                    bool ProCreateFlag = false;
                    if (registerControl.PatientInfo.SIMainInfo.ProceateLastFlag)
                    {
                        ProCreateFlag = true;
                        registerControl.PatientInfo.SIMainInfo.ProceateLastFlag = false;
                    }
                    //����ز������Ϣ
                    registerControl.PatientInfo.SIMainInfo.OutDiagnose.ID = string.Empty;
                    registerControl.PatientInfo.SIMainInfo.OutDiagnose.Name = string.Empty;

                    int invoicesIndex = 0;
                    int InvoiceCount = invoices.Count;
                    foreach (Balance myBalance in invoices)
                    {


                        InvoiceCount--;
                        if (InvoiceCount == 0 && ProCreateFlag)//��������������һ�ν��� ���һ�ŷ�Ʊ���������
                        {
                            registerControl.PatientInfo.SIMainInfo.ProceateLastFlag = true;
                        }
                        if (isSucc)//�ϴ��ύδ������ܼ���
                        {
                            #region ���½�������
                            FS.FrameWork.Management.PublicTrans.BeginTransaction();
                            this.feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            this.medcareInterfaceProxy.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                            #endregion

                            #region ���������ϸ
                            ArrayList myFeeItemListArray = new ArrayList();
                            for (int i = 0; i < invoiceFeeDetails.Count; i++)
                            {
                                ArrayList tempAarry = new ArrayList();
                                tempAarry = (ArrayList)invoiceFeeDetails[i];
                                for (int j = 0; j < tempAarry.Count; j++)
                                {

                                    ArrayList tempAarry2 = new ArrayList();
                                    tempAarry2 = (ArrayList)tempAarry[j];
                                    for (int k = 0; k < tempAarry2.Count; k++)
                                    {
                                        FeeItemList myFeeItemList = new FeeItemList();
                                        myFeeItemList = (FeeItemList)tempAarry2[k];
                                        if (myBalance.Invoice.ID == myFeeItemList.Invoice.ID)
                                        {
                                            myFeeItemListArray.Add(myFeeItemList);

                                        }
                                    }
                                }
                            }
                            #endregion

                            #region ���÷�Ʊ��
                            this.registerControl.PatientInfo.SIMainInfo.InvoiceNo = myBalance.Invoice.ID;
                            #endregion

                            #region ��ȡҽ��������Ϣ
                            returnMedcareValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(this.registerControl.PatientInfo);
                            #endregion

                            #region �����ӿڶ�������
                            if (returnMedcareValue != 1)
                            {
                                errText = "�����ӿڶ�������" + this.medcareInterfaceProxy.ErrMsg;
                                isSucc = false;
                            }
                            #endregion
                            #region  �����ӿ��ϴ���ϸʧ��
                            //{BE0275DB-0F17-453d-A122-C59D2FBF6B2C}�������ʧ�ܺ���Ȼ�ϴ���ϸ
                            if (isSucc)
                            {
                                returnMedcareValue = this.medcareInterfaceProxy.UploadFeeDetailsOutpatient(this.registerControl.PatientInfo, ref myFeeItemListArray);
                                if (returnMedcareValue != 1 /*&& isSucc*/)
                                {
                                    errText = "�����ӿ��ϴ���ϸʧ��" + this.medcareInterfaceProxy.ErrMsg;
                                    isSucc = false;
                                }
                            }
                            #endregion
                            #region �����ӿ�������� ������ fin_ipr_siinmaininfo
                            //{9E434E9D-FC87-4d85-BC0B-5D0EE99C6EEC}
                            if (isSucc)
                            {
                                returnMedcareValue = this.medcareInterfaceProxy.BalanceOutpatient(this.registerControl.PatientInfo, ref myFeeItemListArray);
                                if (returnMedcareValue != 1/* && isSucc*/)
                                {
                                    errText = "�����ӿ��������ʧ��" + this.medcareInterfaceProxy.ErrMsg;
                                    isSucc = false;
                                }
                            }

                            #endregion
                            if (isSucc)
                            {

                                #region liuq 2007-9-7 �´��룬�����ύ���㣮

                                ArrayList invoicesClinicFee;
                                ArrayList invoiceDetailsClinicFee;
                                ArrayList invoiceFeeDetailsClinicFee;

                                invoicesClinicFee = new ArrayList();
                                invoiceDetailsClinicFee = new ArrayList();
                                invoiceFeeDetailsClinicFee = new ArrayList();

                                invoicesClinicFee.Add(myBalance);
                                ArrayList invoiceDetailsClinicFeeTemp = new ArrayList();
                                invoiceDetailsClinicFeeTemp.Add((invoiceDetails[0] as ArrayList)[invoicesIndex]);
                                invoiceDetailsClinicFee.Add(invoiceDetailsClinicFeeTemp);
                                ArrayList invoiceFeeDetailsClinicFeeTemp = new ArrayList();
                                invoiceFeeDetailsClinicFeeTemp.Add((invoiceFeeDetails[0] as ArrayList)[invoicesIndex]);
                                invoiceFeeDetailsClinicFee.Add(invoiceFeeDetailsClinicFeeTemp);


                                decimal payCost = decimal.Zero;
                                decimal pubCost = decimal.Zero;
                                decimal ownCost = decimal.Zero;


                                ownCost = this.registerControl.PatientInfo.SIMainInfo.OwnCost;

                                payCost = this.registerControl.PatientInfo.SIMainInfo.PayCost;

                                pubCost = this.registerControl.PatientInfo.SIMainInfo.PubCost;
                                //{21EEC08E-53DA-458b-BEA3-0036EF6E3D37}
                                    //+ this.registerControl.PatientInfo.SIMainInfo.OfficalCost
                                    //+ this.registerControl.PatientInfo.SIMainInfo.OverCost;

                                myBalance.FT.OwnCost = ownCost;
                                myBalance.FT.PayCost = payCost;
                                myBalance.FT.PubCost = pubCost;


                                bool returnValue = false;
                                try
                                {
                                    returnValue = this.feeIntegrate.ClinicFeeSaveFee(
                                                           FS.HISFC.Models.Base.ChargeTypes.Fee,
                                                           this.registerControl.PatientInfo,
                                                           invoicesClinicFee,
                                                           invoiceDetailsClinicFee,
                                                           myFeeItemListArray,
                                                           invoiceFeeDetailsClinicFee, null, ref errText);
                                }
                                catch (Exception ex)
                                {
                                    isFee = false;
                                    isSucc = false;
                                }
                                if (!returnValue)
                                {

                                    isFee = false;
                                    isSucc = false;
                                }
                                #endregion
                                if (isSucc)
                                {
                                    if (this.medcareInterfaceProxy.Commit() < 0)
                                    {
                                        #region ҽ�����ύ ��ʧ�� ���� ҽ������������
                                        isSucc = false;
                                        errText = "ҽ���ӿ��ύ���������������������Ƿ���ȷ";
                                        this.medcareInterfaceProxy.Rollback();
                                        FS.FrameWork.Management.PublicTrans.RollBack();
                                        #endregion
                                    }
                                    else
                                    {
                                        #region �ύ���أ���ʱ�����Ǳ����ύ���ɹ������
                                        FS.FrameWork.Management.PublicTrans.Commit();
                                        #endregion
                                        #region ��Ʊ��ӡ
                                        foreach (BalancePay balancePay in balancePaysClone)
                                        {
                                            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                                            //if (balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PS.ToString())
                                            if (balancePay.PayType.ID.ToString() == "PS") //�����˻� 
                                            {
                                                balancePay.FT.TotCost = balancePay.FT.TotCost - payCost;
                                            }
                                            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                                            //if (balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.CA.ToString())
                                            if (balancePay.PayType.ID.ToString() == "CA") //�ֽ�
                                            {
                                                balancePay.FT.TotCost = balancePay.FT.TotCost - ownCost;
                                            }
                                            ////{93E6443C-1FB5-45a7-B89D-F21A92200CF6}

                                            //if (balancePay.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.PB.ToString()) 
                                            if (balancePay.PayType.ID.ToString() == "PB")//ͳ��(ҽԺ�渶)
                                            {
                                                balancePay.FT.TotCost = balancePay.FT.TotCost - pubCost;
                                            }
                                        }
                                        string invoicePrintDll = null;

                                        invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);
                                        this.feeIntegrate.PrintInvoice(invoicePrintDll, this.registerControl.PatientInfo, invoicesClinicFee, invoiceDetailsClinicFee, myFeeItemListArray, invoiceFeeDetailsClinicFee, null, false, ref errText);
                                        #endregion
                                    }
                                }
                                else
                                {
                                    this.medcareInterfaceProxy.Rollback();
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                }

                            }
                            else
                            {
                                this.medcareInterfaceProxy.Rollback();
                                FS.FrameWork.Management.PublicTrans.RollBack();
                            }

                            invoicesIndex++;
                        }
                    }
                    if (!isSucc)
                    {
                        #region ���½�������
                        FS.FrameWork.Management.PublicTrans.BeginTransaction();
                        inpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        outpatientManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                        #endregion

                        #region liuq 2007-9-7 �´��룬�����帺֧����ʽ��Ϣ��
                        #region ����֧����ʽ��Ϣ

                        //zjy ˵�˸�����99
                        payModeSeq = 99;

                        // ������ҵ���
                        foreach (BalancePay p in balancePaysClone)
                        {
                            p.FT.RealCost = p.FT.TotCost - changeCost;
                            //{93E6443C-1FB5-45a7-B89D-F21A92200CF6}
                            //if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.CA.ToString())
                            if (p.PayType.ID.ToString() == "CA")//�ֽ�
                            {
                                //���ʵ�ʽ�Ϊ��
                                if (p.FT.TotCost != decimal.Zero)
                                {
                                    //����ʵ�����,��������ͷ
                                    p.FT.RealCost = p.FT.TotCost - changeCost;
                                }
                            }

                            //p.Invoice.ID = mainInvoiceNO.PadLeft(12, '0');
                            p.Invoice.ID = mainInvoiceNO;
                            p.TransType = FS.HISFC.Models.Base.TransTypes.Negative;
                            p.Squence = payModeSeq.ToString();
                            p.IsDayBalanced = false;
                            p.IsAuditing = false;
                            p.IsChecked = false;
                            p.InputOper.ID = inpatientManager.Operator.ID;
                            p.InputOper.OperTime = inpatientManager.GetDateTimeFromSysDateTime();
                            if (string.IsNullOrEmpty(p.InvoiceCombNO))
                            {
                                p.InvoiceCombNO = mainInvoiceCombNO;
                            }
                            p.CancelType = FS.HISFC.Models.Base.CancelTypes.Canceled;

                            if (p.FT.RealCost != 0)
                            {
                                p.FT.TotCost = -p.FT.TotCost;
                                p.FT.RealCost = -p.FT.RealCost;
                                int iReturn;
                                iReturn = outpatientManager.InsertBalancePay(p);
                                if (iReturn == -1)
                                {
                                    MessageBox.Show("����֧����ʽ�����!");
                                    FS.FrameWork.Management.PublicTrans.RollBack();
                                }
                            }

                            #region �����ʻ�����ȡ��
                            //if (p.PayType.ID.ToString() == FS.HISFC.Models.Fee.EnumPayType.YS.ToString())
                            //{
                            //    returnValue = feeIntegrate.AccountPay(this.registerControl.PatientInfo.PID.CardNO, p.FT.TotCost, p.Invoice.ID, p.InputOper.Dept.ID);
                            //    if (!returnValue)
                            //    {
                            //        MessageBox.Show("��ȡ�����˻�ʧ��!");

                            //        return;
                            //    }
                            //} 
                            #endregion
                            FS.FrameWork.Management.PublicTrans.Commit();
                        }
                        #endregion
                        #endregion
                    }
                }
                #endregion
                else
                {
                    #region liuq 2007-9-7 �ɴ��룬�������㣮
                    bool returnValue = this.feeIntegrate.ClinicFee(FS.HISFC.Models.Base.ChargeTypes.Fee, this.registerControl.PatientInfo,
                       invoices, invoiceDetails, comFeeItemLists, invoiceFeeDetails, balancePays, ref errText);
                    if (!returnValue)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.medcareInterfaceProxy.Rollback();
                        if (errText != "")
                        {
                            MessageBox.Show(errText);
                        }

                        isFee = false;

                        return;
                    }
                    #endregion
                    FS.FrameWork.Management.PublicTrans.Commit();
                    #region//��Ʊ��ӡ

                    string invoicePrintDll = null;

                    invoicePrintDll = controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT, false, string.Empty);

                    // ���ķ�Ʊ��ӡ���ȡ��ʽ������ԭ����ʽ
                    // 2011-08-04
                    // �˴�������ʾ
                    //if (invoicePrintDll == null || invoicePrintDll == string.Empty)
                    //{
                    //    MessageBox.Show("û�����÷�Ʊ��ӡ�������շ���ά��!");

                    //}
                    this.feeIntegrate.PrintInvoice(invoicePrintDll, this.registerControl.PatientInfo, invoices, invoiceDetails, comFeeItemLists, invoiceFeeDetails, null, false, ref errText);

                    #endregion

                }

                this.medcareInterfaceProxy.Disconnect();



                #region ���¹����޶�,��������

                //if (this.ucChargeDisplay1.PactInfo.PayKind.ID == "03")
                //{
                //    FS.HISFC.BizProcess.Fee.SpecialLimit spLimit = new FS.HISFC.BizProcess.Fee.SpecialLimit();
                //    spLimit.SetTrans(t.Trans);
                //    FS.HISFC.Models.Fee.OutPatient.Invoice inv = null;
                //    //invoices[0] as FS.HISFC.Models.Fee.OutPatient.Invoice;
                //    foreach (FS.HISFC.Models.Fee.OutPatient.Invoice invo in invoices)
                //    {
                //        if (invo.InvoiceSeq != null && invo.InvoiceSeq.Length > 0)
                //        {
                //            inv = invo;
                //            break;
                //        }
                //    }
                //    foreach (FS.HISFC.Components.Object.neuObject p in this.Relations)
                //    {
                //        FS.HISFC.Models.Fee.OutPatient.SpecialLimit spObj = new FS.HISFC.Models.Fee.OutPatient.SpecialLimit();
                //        spObj.InvoiceSeq = inv.InvoiceSeq;
                //        spObj.LimitKind = p.ID;
                //        spObj.AlteredCost = FS.HISFC.Components.Function.NConvert.ToDecimal(p.Memo);
                //        spObj.DefaultCost = 0;
                //        foreach (FS.HISFC.Models.Base.PactStatRelation oldp in this.ucRegInfo1.Relation)
                //        {
                //            if (oldp.group.ID == this.ucRegInfo1.RInfo.User03 && oldp.ClassState.ID == p.ID)
                //            {
                //                spObj.DefaultCost = oldp.CostLimit;
                //                continue;
                //            }
                //        }
                //        if (spLimit.InsertItem(spObj) == -1)
                //        {
                //            t.RollBack();
                //            MessageBox.Show("�����޶���ĳ���" + spLimit.Err);
                //            isFee = false;
                //            return;
                //        }
                //    }
                //}

                #endregion





                this.popFeeControl.FTFeeInfo.User01 = ((FS.HISFC.Models.Fee.Outpatient.Balance)invoices[0]).DrugWindowsNO;

                //{21659409-F380-421f-954A-5C3378BB9FD6}
                this.rightControl.SetInfomation(this.registerControl.PatientInfo, this.popFeeControl.FTFeeInfo, comFeeItemLists, null);

                //���Ʊ��ιҺŻ�����Ϣ
                this.registerControl.PrePatientInfo = this.registerControl.PatientInfo.Clone();
                this.leftControl.InitInvoice();

                isFee = true;

                if (isSucc)
                {
                    msgInfo = Language.Msg("�շѳɹ�!");
                }
                else
                {
                    msgInfo = Language.Msg("�շ�ʧ��!" + errText);
                }

                MessageBox.Show(msgInfo);

                this.Clear();

                #region ��ʾ��ҩ���� ��LIS�ӿ�, ��������
                //if (System.IO.File.Exists(Application.StartupPath + "\\chargeLED.exe") == true)
                //{
                //    try
                //    {
                //        if (this.frmBalance.ucDealBalance1.FTFeeInfo.User01 != null && this.frmBalance.ucDealBalance1.FTFeeInfo.User01.Length > 0)
                //        {
                //            FS.Common.Controls.Function.ShowPatientFee("�뵽" + this.frmBalance.ucDealBalance1.FTFeeInfo.User01 + "ȡҩ", this.frmBalance.ucDealBalance1.PayCost + this.frmBalance.ucDealBalance1.OwnCost);
                //        }
                //    }
                //    catch
                //    { }
                //}
                //if (this.dataToLis)
                //{
                //    #region ����LIS�ӿ�

                //    foreach (FS.HISFC.Models.Fee.OutPatient.FeeItemList feeItem in this.GetArrayToLis(this.ucChargeDisplay1.GetFeeItemListForCharge(), alFee))
                //    {
                //        if (feeItem.SysClass.ID.ToString() == "UL")
                //        {
                //            lisInterface.Function.LisSetClinicData(this.ucRegInfo1.RInfo, feeItem, t.Trans);
                //        }
                //    }
                //    #endregion
                //}

                #endregion

                #endregion
            }
        }

        /// <summary>
        /// ��ʼ���Ҳ�ؼ�
        /// </summary>
        /// <returns>�ɹ� 1ʧ�� -1</returns>
        protected virtual int InitRightControl()
        {
            if (this.rightControl == null)
            {
                return -1;
            }

            this.plBottom.Height = ((Control)this.rightControl).Height + 6;

            this.plBRight.Controls.Add((Control)this.rightControl);
            this.plBRight.Height = ((Control)this.rightControl).Height + 5;
            this.plBRight.Width = ((Control)this.rightControl).Width + 5;

            this.rightControl.Init();

            return 1;
        }

        /// <summary>
        /// ��ʼ�������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitLeftControl()
        {
            if (this.leftControl == null)
            {
                return -1;
            }

            if (this.plBottom.Height < ((Control)this.leftControl).Height + 5)
            {
                this.plBottom.Height = ((Control)this.leftControl).Height + 5;
            }

            this.plBLeft.Controls.Add((Control)this.leftControl);
            //this.plBLeft.Height = ((Control)this.leftControl).Height;
            //this.plBLeft.Width = ((Control)this.leftControl).Width;
            ((Control)this.leftControl).Dock = DockStyle.Fill;

            this.plBottom.Height = this.plBRight.Height;

            this.leftControl.Init();
            // {00269495-2E8F-48a8-8F75-7B9AD1405378}��ʾ��ǰ�շ�Ա��Ʊ��
            this.leftControl.InitInvoice();
            //{00269495-2E8F-48a8-8F75-7B9AD1405378}

            this.leftControl.InvoiceUpdated += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(leftControl_InvoiceUpdated);

            return 1;
        }

        /// <summary>
        /// ���ؼ��ķ�Ʊ����������Ϣ�����¼�
        /// </summary>
        protected virtual void leftControl_InvoiceUpdated()
        {
            if (!((Control)this.registerControl).Focus())
            {
                ((Control)this.registerControl).Focus();
            }
            if (this.itemInputControl.IsFocus)
            {
                ((Control)this.registerControl).Focus();
            }
        }

        /// <summary>
        /// ��ʼ�����߻�����Ϣ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitRegisterControl()
        {
            if (this.registerControl == null)
            {
                return -1;
            }

            this.plTop.Controls.Add((Control)this.registerControl);
            ((Control)this.registerControl).Focus();
            this.plTop.Height = ((Control)this.registerControl).Height + 5;
            ((Control)this.registerControl).Dock = DockStyle.Fill;

            this.registerControl.Init();

            this.registerControl.ChangeFocus += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(registerControl_ChangeFocus);
            this.registerControl.PactChanged += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(registerControl_PactChanged);
            this.registerControl.PriceRuleChanaged += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(registerControl_PriceRuleChanaged);
            this.registerControl.RecipeSeqChanged += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeSomething(registerControl_RecipeSeqChanged);
            this.registerControl.RecipeSeqDeleted += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateRecipeDeleted(registerControl_RecipeSeqDeleted);
            this.registerControl.SeeDeptChanaged += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeDoctAndDept(registerControl_SeeDeptChanaged);
            this.registerControl.SeeDoctChanged += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateChangeDoctAndDept(registerControl_SeeDoctChanged);
            this.registerControl.InputedCardAndEnter += new FS.HISFC.BizProcess.Integrate.FeeInterface.DelegateEnter(registerControl_InputedCardAndEnter);

            this.registerControl.IsAddUp = this.IsAddUp;

            return 1;
        }

        /// <summary>
        /// ��ʼ����Ŀ¼����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual int InitItemInputControl()
        {
            if (this.itemInputControl == null)
            {
                return -1;
            }

            this.plMain.Controls.Add((Control)this.itemInputControl);

            ((Control)this.itemInputControl).Dock = DockStyle.Fill;

            this.itemInputControl.Init();

            this.itemInputControl.FeeItemListChanged += new FS.HISFC.BizProcess.Integrate.FeeInterface.delegateFeeItemListChanged(itemInputControl_FeeItemListChanged);

            return 1;
        }

        /// <summary>
        /// ��¼��ؼ���,��Ŀ�����仯�󴥷�
        /// </summary>
        /// <param name="al">�仯����Ŀ����</param>
        protected virtual void itemInputControl_FeeItemListChanged(System.Collections.ArrayList al)
        {
            if (this.registerControl.PatientInfo == null)
            {
                return;
            }

            this.registerControl.ModifyFeeDetails = (ArrayList)al.Clone();
            this.registerControl.DealModifyDetails();
        }

        /// <summary>
        /// �������뻼�߿��Żس�����¼�
        /// </summary>
        /// <param name="cardNO">����</param>
        /// <param name="orgNO">ԭʼ����</param>
        /// <param name="cardLocation">���ŵ�λ��</param>
        /// <param name="cardHeight">���ŵĸ߶�</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        protected virtual bool registerControl_InputedCardAndEnter(string cardNO, string orgNO, Point cardLocation, int cardHeight)
        {
            ucShow.OrgCardNO = orgNO;
            ucShow.CardNO = cardNO;
            ucShow.operType = "1";//ֱ������
            if (ucShow.PersonCount == 0)
            {
                this.itemInputControl.Clear();
                MessageBox.Show(Language.Msg("�û���û�йҺ���Ϣ!"));

                return false;
            }
            if (ucShow.PersonCount > 1)
            {
                fPopWin.Show();
                fPopWin.Hide();
                fPopWin.Location = ((Control)this.registerControl).PointToScreen(new Point(cardLocation.X, cardLocation.Y + cardHeight));
                fPopWin.ShowDialog();
            }
            if (this.registerControl.PatientInfo == null)
            {
                return false;
            }

            this.registerControl.IsCanModifyChargeInfo = this.itemInputControl.IsCanModifyCharge;
            this.itemInputControl.PatientInfo = this.itemInputControl.PatientInfo;

            return true;
        }

        /// <summary>
        /// ������Ϣ¼��ؼ��Ŀ���ҽ�������仯�󴥷�
        /// </summary>
        /// <param name="recipeSeq">��ǰ�շ�����</param>
        /// <param name="deptCode">ҽ�����ڿ��Ҵ���</param>
        /// <param name="changeObj">�仯��ҽ��ID������</param>
        protected virtual void registerControl_SeeDoctChanged(string recipeSeq, string deptCode, FS.FrameWork.Models.NeuObject changeObj)
        {
            this.itemInputControl.RefreshSeeDoc(recipeSeq, deptCode, changeObj);
        }

        /// <summary>
        /// ������Ϣ¼��ؼ��Ŀ�����ҷ����仯�󴥷�
        /// </summary>
        /// <param name="recipeSeq">��ǰ�շ�����</param>
        /// <param name="deptCode">ҽ�����ڿ��Ҵ���</param>
        /// <param name="changeObj">�仯�Ŀ���ID������</param>
        protected virtual void registerControl_SeeDeptChanaged(string recipeSeq, string deptCode, FS.FrameWork.Models.NeuObject changeObj)
        {
            this.itemInputControl.RefreshSeeDept(recipeSeq, changeObj);
        }

        /// <summary>
        /// ɾ���շ����е�ʱ�򴥷�
        /// </summary>
        /// <param name="al">ɾ�������а�������Ŀ</param>
        /// <returns>�ɹ�1 ʧ�� -1</returns>
        protected virtual int registerControl_RecipeSeqDeleted(System.Collections.ArrayList al)
        {
            int iReturn = 0;
            foreach (FeeItemList f in al)
            {
                iReturn = this.itemInputControl.DeleteRow(f);
                if (iReturn == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// �շ����б仯�󴥷�
        /// </summary>
        protected virtual void registerControl_RecipeSeqChanged()
        {
            this.itemInputControl.Clear();
            this.itemInputControl.PatientInfo = this.registerControl.PatientInfo;
            this.itemInputControl.ChargeInfoList = this.registerControl.FeeDetailsSelected;
            this.itemInputControl.RecipeSequence = this.registerControl.RecipeSequence;
            this.itemInputControl.IsCanAddItem = this.registerControl.IsCanAddItem;
        }

        /// <summary>
        /// �۸�������仯�󴥷�,��������,������
        /// </summary>
        protected virtual void registerControl_PriceRuleChanaged()
        {
            this.itemInputControl.ModifyPrice();
        }

        /// <summary>
        /// ��ͬ��λ�仯�󴥷�
        /// </summary>
        protected virtual void registerControl_PactChanged()
        {
            this.itemInputControl.PatientInfo = this.registerControl.PatientInfo;
            this.itemInputControl.RefreshItemForPact();
            this.itemInputControl.SetFocus();
        }

        /// <summary>
        /// ����¼��ؼ������л��󴥷�
        /// </summary>
        protected virtual void registerControl_ChangeFocus()
        {
            ((Control)this.itemInputControl).Focus();
            this.itemInputControl.SetFocus();
            this.itemInputControl.IsFocus = true;

        }

        /// <summary>
        /// ��ʾ��һ������Ϣ
        /// </summary>
        /// <returns></returns>
        protected virtual void DisplayPreRegInfo()
        {
            if (this.registerControl == null || this.itemInputControl == null)
            {
                return;
            }

            if (this.registerControl.PrePatientInfo != null)
            {
                this.registerControl.Clear();
                this.itemInputControl.Clear();
                this.registerControl.PatientInfo = this.registerControl.PrePatientInfo.Clone();
                if (this.registerControl.PatientInfo.ID != null && this.registerControl.PatientInfo.ID != "")
                {
                    this.registerControl.AddNewRecipe();
                }

            }
        }

        /// <summary>
        /// ��ʾ������
        /// </summary>
        /// <returns></returns>
        protected virtual int DisplayCalc()
        {
            string tempValue = this.controlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.Const.CALCTYPE, false, "0");

            if (tempValue == "0")
            {
                System.Diagnostics.Process.Start("CALC.EXE");
            }
            else if (tempValue == "1")
            {
                FS.FrameWork.WinForms.Classes.Function.PopShowControl(
                    new FS.HISFC.Components.Common.Controls.ucCalc());
            }
            else
            {
                System.Diagnostics.Process.Start("CALC.EXE");
            }

            return 1;
        }

        /// <summary>
        /// �л�����
        /// </summary>
        public void ChangeFocus()
        {
            if (this.itemInputControl.IsFocus)
            {
                ((Control)this.registerControl).Focus();
            }
            else
            {
                this.itemInputControl.SetFocus();
            }
        }

        /// <summary>
        /// ������ݼ�XML
        /// </summary>
        /// <param name="hashCode">��ǰ������HashCode</param>
        /// <returns>�ɹ���ǰֵ,ʧ�� string.Empty</returns>
        public string Operation(string hashCode)
        {
            XmlDocument doc = new XmlDocument();
            if (filePath == "") return "";
            try
            {
                StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                doc.LoadXml(cleandown);
                sr.Close();
            }
            catch
            {
                return "";
            }
            XmlNodeList nodes = doc.SelectNodes("//Column");
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes["hash"].Value == hashCode)
                {
                    return node.Attributes["opCode"].Value;
                }
            }

            return "";
        }

        /// <summary>
        /// ִ�п�ݼ�
        /// </summary>
        /// <param name="key">��ǰ����</param>
        public bool ExecuteShotCut(Keys key)
        {
            int iReturn = -1;

            string code = Operation(key.GetHashCode().ToString());

            if (code == "") return false;

            switch (code)
            {
                case "1":
                    iReturn = this.SaveFee();

                    if (iReturn == -1)
                    {

                        return true;
                    }
                    if (this.isFee)
                    {
                        //MessageBox.Show(Language.Msg("�շѳɹ�!"));
                        this.Focus();
                        this.Clear();
                        ((Control)this.registerControl).Focus();
                        this.isFee = false;
                    };

                    break;
                case "2":
                    iReturn = this.SaveCharge();

                    if (iReturn == -1)
                    {
                        return true;
                    }
                    break;
                case "3":

                    if (this.itemInputControl == null)
                    {
                        return true;
                    }

                    this.itemInputControl.AddNewRow();

                    break;
                case "4"://ɾ��

                    if (this.itemInputControl == null)
                    {
                        return true;
                    }

                    this.itemInputControl.DeleteRow();

                    break;
                case "5"://���
                    this.Clear();

                    break;
                case "6"://����
                    break;
                case "7"://�˳�
                    //this.FindForm().Close();
                    break;
                case "8"://������
                    this.DisplayCalc();

                    break;
                case "9"://�����޸ı���
                    //this.ucChargeFee1.ModifyRate();
                    break;
                case "10"://�ݴ�
                    this.ChangeRecipe();

                    break;
                case "11"://��ʷ��Ʊ��ѯ
                    //frmPre = new frmPreCountInvos();
                    //frmPre.Show();
                    //this.Focus();
                    break;
                case "12"://����������Ϣ
                    //this.ucChargeFee1.DisplayPubFeeBills();
                    break;
                case "13"://��һ�շѻ���
                    this.DisplayPreRegInfo();

                    break;
                case "14"://С��
                    this.itemInputControl.SumLittleCost();

                    break;
                case "15"://�޸Ĳ�ҩ����
                    this.itemInputControl.ModifyDays();

                    break;
                case "16":
                    this.ChangeFocus();

                    break;
                case "17":
                    //this.ucChargeFee1.DisplayPatientFeeList();
                    break;
                case "18":
                    //frmQuitFee frmQuitFee = new frmQuitFee();
                    //frmQuitFee.Show();
                    break;
                case "19":
                    //this.ucChargeFee1.ChangeQueryType();
                    break;
                case "20":
                    // this.ucChargeFee1.ucChargeDisplay1.ucInvoicePreview1.SetFocusToInvo();
                    break;
            }

            return true;

        }

        /// <summary>
        /// ����ˢ��ToolBar
        /// </summary>
        public void RefreshToolBar()
        {
            XmlDocument doc = new XmlDocument();
            if (filePath == "")
            {
                return;
            }
            try
            {
                StreamReader sr = new StreamReader(filePath, System.Text.Encoding.Default);
                string cleandown = sr.ReadToEnd();
                doc.LoadXml(cleandown);
                sr.Close();
            }
            catch
            {
                return;
            }
            XmlNodeList nodes = doc.SelectNodes("//Column");
            foreach (XmlNode node in nodes)
            {
                string opKey = node.Attributes["opKey"].Value;
                string cuKey = node.Attributes["cuKey"].Value;
                if (opKey != "")
                {
                    opKey = "Ctrl+";
                }
                if (cuKey == "")
                {
                    cuKey = "";
                }
                else
                {
                    cuKey = "(" + opKey + cuKey + ")";
                }

                ToolStripButton tempButton = new ToolStripButton();

                switch (node.Attributes["opCode"].Value)
                {
                    case "1"://�շ�
                        tempButton = this.toolBarService.GetToolButton("ȷ���շ�");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "ȷ���շ�" + cuKey;

                        this.hsToolBar.Add(tempButton.Text, "ȷ���շ�");

                        break;
                    case "2"://���۱���
                        tempButton = this.toolBarService.GetToolButton("���۱���");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "���۱���" + cuKey;

                        //this.hsToolBar.Add(tempButton.Text, "���۱���");

                        break;
                    case "10"://�ݴ�
                        tempButton = this.toolBarService.GetToolButton("�ݴ�");
                        if (tempButton == null)
                        {
                            return;
                        }

                        tempButton.Text = "�ݴ�" + cuKey;

                        //this.hsToolBar.Add(tempButton.Text, "�ݴ�");

                        break;
                    case "3"://����
                        tempButton = this.toolBarService.GetToolButton("����");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "����" + cuKey;

                        //this.hsToolBar.Add(tempButton.Text, "����");

                        break;
                    case "4"://ɾ��
                        tempButton = this.toolBarService.GetToolButton("ɾ��");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "ɾ��" + cuKey;

                        //this.hsToolBar.Add(tempButton.Text, "ɾ��");

                        break;
                    case "5"://���
                        tempButton = this.toolBarService.GetToolButton("����");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "����" + cuKey;

                        //this.hsToolBar.Add(tempButton.Text, "����");

                        break;
                    case "6"://����
                        tempButton = this.toolBarService.GetToolButton("����");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "����" + cuKey;

                        this.hsToolBar.Add(tempButton.Text, "����");

                        break;
                    case "7"://�˳�
                        tempButton = this.toolBarService.GetToolButton("�˳�");
                        if (tempButton == null)
                        {
                            break;
                        }

                        tempButton.Text = "�˳�" + cuKey;

                        this.hsToolBar.Add(tempButton.Text, "�˳�");

                        break;
                }
            }
        }

        /// <summary>
        /// ��ʼ�ۼ�
        /// </summary>
        protected virtual void BeginAddUpCost()
        {
            this.registerControl.IsBeginAddUpCost = true;
            toolBarService.SetToolButtonEnabled("��ʼ�ۼ�", false);
            toolBarService.SetToolButtonEnabled("ȡ���ۼ�", true);
            toolBarService.SetToolButtonEnabled("�����ۼ�", true);
        }

        /// <summary>
        /// ȡ���ۼ�
        /// </summary>
        protected virtual void CancelAddUpCost()
        {
            this.registerControl.IsBeginAddUpCost = false;

            toolBarService.SetToolButtonEnabled("��ʼ�ۼ�", true);
            toolBarService.SetToolButtonEnabled("ȡ���ۼ�", false);
            toolBarService.SetToolButtonEnabled("�����ۼ�", false);
        }

        /// <summary>
        /// ȡ���ۼ�
        /// </summary>
        protected virtual void EndAddUpCost()
        {
            MessageBox.Show(this.registerControl.AddUpCost.ToString());
            this.registerControl.IsBeginAddUpCost = false;
            toolBarService.SetToolButtonEnabled("��ʼ�ۼ�", true);
            toolBarService.SetToolButtonEnabled("ȡ���ۼ�", false);
            toolBarService.SetToolButtonEnabled("�����ۼ�", false);
        }
        #endregion

        #region �¼�

        /// <summary>
        /// �򿪻��߶�ιҺ�UC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void fPopWin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.fPopWin.Close();
            }
        }

        /// <summary>
        /// �����ؼ�Init�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            toolBarService.AddToolButton("����", "���¼�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Q���, true, false, null);
            toolBarService.AddToolButton("ȷ���շ�", "ȷ���շ���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȷ���շ�, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ��¼��ķ�����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("���۱���", "���滮����Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.H���۱���, true, false, null);
            toolBarService.AddToolButton("�ݴ�", "��ʱ�����շ���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Z�ݴ�, true, false, null);
            toolBarService.AddToolButton("����", "����һ���շ���Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("����", "�򿪰����ļ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť
            toolBarService.AddToolButton("ˢ����Ŀ", "ˢ����Ŀ�б�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.B����, true, false, null);
            //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ���
            toolBarService.AddToolButton("��ʼ�ۼ�", "��ʼ�ۼ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.MĬ��, true, false, null);
            toolBarService.AddToolButton("ȡ���ۼ�", "ȡ���ۼ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȡ��, true, false, null);
            toolBarService.AddToolButton("�����ۼ�", "�����ۼ�", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Qȫѡ, true, false, null);
            return this.toolBarService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "��ʼ�ۼ�":
                    {
                        this.BeginAddUpCost();
                        break;
                    }
                case "ȡ���ۼ�":
                    {
                        this.CancelAddUpCost();
                        break;
                    }
                case "�����ۼ�":
                    {
                        this.EndAddUpCost();
                        break;
                    }
            }

            //string tempText = string.Empty;

            //try
            //{
            //    tempText = this.hsToolBar[e.ClickedItem.Text].ToString();
            //}
            //catch (Exception ex)
            //{
            //    //by niuxinyuan
            //    // MessageBox.Show(ex.Message);

            //    return;
            //}

            switch (e.ClickedItem.Text)
            {
                //case "�˳�":
                //    MessageBox.Show("�˳�!");
                //    this.registerControl = null;
                //    this.itemInputControl = null;
                //    this.popFeeControl = null;
                //    this.leftControl = null;
                //    this.rightControl = null;
                //    break;

                case "ȷ���շ�":
                    this.SaveFee();
                    break;
                case "���۱���":
                    this.SaveCharge();
                    break;
                case "����":
                    this.Clear();
                    break;
                case "ɾ��":
                    this.itemInputControl.DeleteRow();
                    break;
                case "����":
                    this.itemInputControl.AddNewRow();
                    break;
                case "�ݴ�":
                    this.ChangeRecipe();
                    break;
                case "ˢ����Ŀ":
                    //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť
                    this.RefreshItem();
                    //{6ACA3A64-8510-4152-957A-F2E8FB68C92E} ����ˢ����Ŀ�б�ť ���
                    break;
            }

            base.ToolStrip_ItemClicked(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="neuObject"></param>
        /// <returns></returns>
        protected override int OnSave(object sender, object neuObject)
        {
            this.SaveFee();

            return base.OnSave(sender, neuObject);
        }

        /// <summary>
        /// �򿪴���֮ǰִ�е��¼�
        /// </summary>
        protected virtual void OnLoad()
        {

        }

        /// <summary>
        /// �򿪴��ڳ�ʼ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected virtual void ucCharge_Load(object sender, EventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }

            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ�������,���Ժ�...");

            Application.DoEvents();

            //RefreshToolBar();

            this.ParentForm.FormClosing += new FormClosingEventHandler(ParentForm_FormClosing);

            this.OnLoad();

            if (this.Init() == -1)
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

                return;
            }

            this.FindForm().FormClosing += new FormClosingEventHandler(ucChargeSelf_FormClosing);

            this.FindForm().MaximizeBox = false;
            this.FindForm().MinimizeBox = false;
            this.FindForm().WindowState = FormWindowState.Maximized;

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
        }

        void ucChargeSelf_FormClosing(object sender, FormClosingEventArgs e)
        {

            HISFC.Components.Common.Forms.frmValidUserPassWord frm = new FS.HISFC.Components.Common.Forms.frmValidUserPassWord();

            DialogResult dia = frm.ShowDialog();

            if (dia == DialogResult.OK)
            {
            }
            else
            {
                e.Cancel = true;
            }
            return; ;
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.registerControl = null;
            //this.itemInputControl = null;
            //this.popFeeControl = null;
            //this.leftControl = null;
            //this.rightControl = null;
            try
            {
                //{E027D856-6334-4410-8209-5E9E36E31B53} ��Ŀ�б���߳�����
                //�رմ���ǰ,���������Ŀ�б��̻߳�û�н���,ǿ�н���,��������
                (this.itemInputControl as ucDisplay).threadItemInit.Abort();
            }
            catch { }

        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            //if (!this.ExecuteShotCut(keyData))
            //{
            //    return false;
            //}

            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region ISIReadCard ��Ա

        /// <summary>
        /// ͨ��toolBar�Ķ��������ӿ�
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>�ɹ� 1 ʧ�� ��1</returns>
        public int ReadCard(string pactCode)
        {
            long returnValue = 0;

            returnValue = this.medcareInterfaceProxy.SetPactCode(pactCode);
            // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
            this.medcareInterfaceProxy.IsLocalProcess = false;

            if (returnValue != 1)
            {
                MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            returnValue = this.medcareInterfaceProxy.Connect();
            if (returnValue != 1)
            {
                MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            if (this.registerControl.PatientInfo == null)
            {
                this.registerControl.PatientInfo = new FS.HISFC.Models.Registration.Register();
            }

            returnValue = this.medcareInterfaceProxy.GetRegInfoOutpatient(this.registerControl.PatientInfo);
            if (returnValue != 1)
            {
                MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            returnValue = this.medcareInterfaceProxy.Disconnect();
            if (returnValue != 1)
            {
                MessageBox.Show(this.medcareInterfaceProxy.ErrMsg);

                return -1;
            }

            this.registerControl.SetRegInfo();

            return 1;
        }

        /// <summary>
        /// ���ý��滼�߻�����Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� ��1</returns>
        public int SetSIPatientInfo()
        {
            this.registerControl.SetRegInfo();

            return 1;
        }

        #endregion
    }
}
