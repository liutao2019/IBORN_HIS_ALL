using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.DrugStore.Outpatient
{
    /// <summary>
    /// [��������: �����䷢ҩ�б���ʾ�ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <��ע������
    ///     1��������ʽ�ṩ��Ʊ��/������/�������Ŷ��ַ�ʽ ���������ŵķ�ʽ����һ�ſ����ڶ����ͬ״̬�ĵ� ���ַ�ʽ�µĴ���
    ///         �����������ն���/�������
    ///		
    ///  />
    /// <�޸ļ�¼>
    ///     <�޸�ԭ��>��������HIS����</�޸�ԭ��>
    ///     <�޸�����>
    ///             1��������ҩʱ�������ֶ���ӡ����£������б����Զ�ˢ�µ�����ӡ
    ///             2����ѡ���ն˴�ӡ�ĵ������ͣ���ǩ����ҩ�嵥�����Զ���ӡ������£�ֻ�ܴ�ӡ��ǩ���ֹ���ӡ������¿ɽ���ѡ��
    ///             3����ǩ��ӡʱ��ѡ����ҩ����ӡ��û�жԷ�ҩ���ںŽ��и�ֵ
    ///     </�޸�����>
    ///     <�޸���>������</�޸���>
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucClinicTree : DrugStore.Outpatient.ucClinicBase
    {
        public ucClinicTree()
        {
            InitializeComponent();
        }

        public delegate void ProcessMessageHandler(object sender, string msg);

        public delegate void MyTreeSelectHandler(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe);

        public delegate void MyOperChangedHandler(FS.FrameWork.Models.NeuObject oper);

        /// <summary>
        /// �Դ�ӡ���Ľ��ı���λ��
        /// </summary>
        private delegate void ChangePrintNodeHandler(TreeNode node,bool targetNextNodeType);

        /// <summary>
        /// �������й����з��͵���Ϣ
        /// </summary>
        public event ProcessMessageHandler ProcessMessageEvent;

        /// <summary>
        /// ���б�ѡ���¼�
        /// </summary>
        public event MyTreeSelectHandler MyTreeSelectEvent;

        /// <summary>
        /// ��/��ҩ����Ա�����仯
        /// </summary>
        public event MyOperChangedHandler OperChangedEvent;

        /// <summary>
        /// ����
        /// </summary>
        public event System.EventHandler SaveRecipeEvent;

        #region ��Ϣ���ͽӿ�

        /// <summary>
        /// LED����Ļ��ʾ�ӿ�
        /// </summary>
        FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientLEDShow LEDShowInterface = null;

        #endregion

        #region ������

        /// <summary>
        /// ��Ա������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper personHelper = new FS.FrameWork.Public.ObjectHelper();

        /// <summary>
        /// �ն˰�����  {23E209BB-B4EB-4676-851A-DA053F0D51FD}
        /// </summary>
        protected System.Collections.Hashtable hsTerminalDictionary = new Hashtable();

        #endregion

        #region ����������

        /// <summary>
        /// ���������
        /// </summary>
        private DrugStore.Outpatient.tvClinicTree AddTree = null;

        /// <summary>
        /// ���ݲ�����
        /// </summary>
        private DrugStore.Outpatient.tvClinicTree OperTree = null;

        /// <summary>
        /// ���ݺ����
        /// </summary>
        private DrugStore.Outpatient.tvClinicTree NextTree = null;

        /// <summary>
        /// ������� 0 ��ҩ 1 ��ҩ 2 ��ҩ
        /// </summary>
        private string funType = "0";

        /// <summary>
        /// ��ѯʱ������ʱ������
        /// </summary>
        private DateTime minQueryDate = System.DateTime.MinValue;

        #endregion

        #region �����

        /// <summary>
        /// ҩ������������
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        /// <summary>
        /// ҩƷ������
        /// </summary>
        FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ��ǰ�����ҩʱ��
        /// </summary>
        DateTime maxDrugedDateTime = System.DateTime.MinValue;

        /// <summary>
        /// �򿪴���ʱϵͳ����
        /// </summary>
        DateTime sysDate = System.DateTime.MinValue;

        /// <summary>
        /// Ĭ�ϵ��ݼ�����ʽ
        /// </summary>
        private DrugStore.OutpatientBillType defaultBillType = OutpatientBillType.������;

        /// <summary>
        /// ��ǰ���ݼ�����ʽ
        /// </summary>
        private DrugStore.OutpatientBillType nowBillType = OutpatientBillType.������;

        /// <summary>
        /// ǰһ�β�������
        /// </summary>
        private string privOperID = "";

        /// <summary>
        /// ��Ա���ż���ʱ��λ���� -1 ���貹λ
        /// </summary>
        private int PIDFillNum = -1;

        /// <summary>
        /// �Ƿ����/��ҩ��Ա����Ȩ���ж�
        /// </summary>
        private bool isJudgePriv = false;

        /// <summary>
        /// ��ǩ��ӡ���ݴ��䷽ʽ True һ��ȫ������  False  ����ϴ���
        /// </summary>
        private bool isAllSend = false;

        /// <summary>
        /// ��ǰ���������������Ϣ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.DrugRecipe nowDrugRecipe = null;

        /// <summary>
        /// �Ƿ��ǲ�ҩ��ʽ��ӡ ��ҩ��ӡʱ�Ƿ��ӡ��ҩ��ҩ��������ӡ��ǩ
        /// </summary>
        private bool isHerbalPrint = false;

        /// <summary>
        /// �Դ������ϼ�¼�Ĵ����Ƿ��ӡ
        /// </summary>
        private bool isPrintCancelRecipe = false;

        /// <summary>
        /// ��ӡ��ڵ�λ��ת��ί��
        /// </summary>
        private ChangePrintNodeHandler changeNode = null;

        /// <summary>
        /// ��ǰ�Ƿ����ڽ��б������
        /// </summary>
        private bool isBusySave = false;

        /// <summary>
        /// ͨ�����ݼ���δ���ڴ�����ʱ �Ƿ��δ��ӡ��ǩ�������
        /// </summary>
        private bool isFindToAdd = false;

        /// <summary>
        /// �Ƿ��Զ���ӡ
        /// </summary>
        private bool isAutoPrint = false;

        /// <summary>
        /// �Ƿ���ʾ���췢ҩ��Ϣ
        /// </summary>
        private bool isShowOldInfo = false;

        /// <summary>
        /// �Ƿ�����ҩȷ��ʱ���´���������Ϣ
        /// </summary>
        private bool isAdjustInDrug = true;

        /// <summary>
        /// �Ƿ��Զ���ҩ
        /// </summary>
        private bool isAutoSend = false;

        /// <summary>
        /// �Ƿ��Զ���ҩ
        /// </summary>
        private bool isAutoDruged = false;

        #endregion

        #region ����

        /// <summary>
        /// Ĭ�ϵ��ݼ�����ʽ
        /// </summary>
        [Category("����"),DefaultValue(DrugStore.OutpatientBillType.������)]
        public DrugStore.OutpatientBillType DefaultBillType
        {
            get
            {
                return this.defaultBillType;
            }
            set
            {
                this.defaultBillType = value;
            }
        }

        /// <summary>
        /// ���б������ʱ ���л��⸳ֵ
        /// </summary>
        [Category("����"), DefaultValue(false)]
        public bool IsBusySave
        {
            get
            {
                return this.isBusySave;
            }
            set
            {
                this.isBusySave = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ����һ����ҩ��¼���շ�ʱ��
        /// </summary>
        [Description("�Ƿ���ʾ����һ����ҩ��¼���շ�ʱ��"),Category("����"), DefaultValue(false)]
        public bool IsShowFeeData
        {
            get
            {
                return this.lbFeeDate.Visible;
            }
            set
            {
                this.lbFeeDate.Visible = value;
            }
        }

        /// <summary>
        /// ͨ�����ݼ���δ���ڴ�����ʱ �Ƿ��δ��ӡ��ǩ�������
        /// </summary>
        public bool IsFindToAdd
        {
            get
            {
                return this.isFindToAdd;
            }
            set
            {
                this.isFindToAdd = value;
            }
        }

        /// <summary>
        /// �Ƿ��Զ���ӡ
        /// </summary>
        public bool IsAutoPrint
        {
            get
            {
                return this.isAutoPrint;
            }
            set
            {
                this.isAutoPrint = value;
            }
        }

        #endregion

        #region ��ʼ��

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public virtual void Init()
        {
            this.sysDate = this.drugStoreManager.GetDateTimeFromSysDateTime();

            this.minQueryDate = this.sysDate.Date;

            #region ���Ʋ�����ȡ

            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            //���Ų���λ�� ��1 ����λ
            this.PIDFillNum = ctrlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_OperCode_Length,true, -1);
            //�Ƿ����/��ҩ������Ա����Ȩ���ж�
            this.isJudgePriv = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Need_Priv, true, true);

            //��ǩ��ӡ�ӿ����ݴ��ͷ�ʽ  Ĭ��ֵ False ���÷��鴫�ͷ�ʽ
            this.isAllSend = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_PrintData_SentType, true, false);
           
            //�Ƿ�Դ������ϼ�¼�Ĵ������д�ӡ
            this.isPrintCancelRecipe = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Print_BackRecipe, true, true);

            //�Ƿ���ʾ�������ҩ��Ϣ
            this.isShowOldInfo = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Show_OldSended, true, false);            
            if (this.isShowOldInfo)
            {
                this.minQueryDate = new DateTime(2000, 1, 1, 10, 0, 0);
            }

            //�Ƿ��Զ���ҩ {DBB3C382-BB23-463b-8847-2F73C55F2586}
            this.isAutoSend = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Auto_Send, false, false);

            //�Ƿ��Զ���ҩ {DBB3C382-BB23-463b-8847-2F73C55F2586}
            this.isAutoDruged = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Auto_Druged, false, false);

            #region ��ȡ��ӡʱ�Ƿ��ӡ�嵥������ӡ��ǩ
            //string strErr = "";
            //ArrayList alParm = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("ClinicDrug", "PrintList", out strErr);
            //if (alParm == null || alParm.Count == 0)
            //{
            //    if (this.funModle == OutpatientFun.Drug)
            //    {
            //        MessageBox.Show(Language.Msg("δ���ñ������ò��� ����ҩ̨��ӡʱ�����ձ�ǩ��ʽ��ӡ \n") + strErr);
            //    }
            //    this.isHerbalPrint = false;
            //}
            //else
            //{
            //    if ((alParm[0] as string) == "1")
            //        this.isHerbalPrint = true;
            //    else
            //        this.isHerbalPrint = false;
            //}

            this.isHerbalPrint = true;
            if (this.terminal.TerimalPrintType == FS.HISFC.Models.Pharmacy.EnumClinicPrintType.��ǩ)
            {
                this.isHerbalPrint = false;
            }
            #endregion				

            #endregion

            FS.HISFC.BizProcess.Integrate.Manager manager = new FS.HISFC.BizProcess.Integrate.Manager();
            ArrayList al = manager.QueryEmployeeAll();
            if (al == null)
            {
                MessageBox.Show(Language.Msg("������Ա�б�ʧ��") + manager.Err);
                return;
            }
            this.personHelper = new FS.FrameWork.Public.ObjectHelper(al);

            this.lbnBillType.Text = FS.FrameWork.Management.Language.Msg( this.nowBillType.ToString() );

            #region {F8D76CE8-6A0C-469b-AC43-4F69B2FCBAD8} ��ȡ���Ʋ�����Ϣ ���ڿ��Ƶ����������·�ʽ

            FS.FrameWork.Management.ExtendParam extManager = new FS.FrameWork.Management.ExtendParam();
            try
            {
                FS.HISFC.Models.Base.ExtendInfo deptExt = extManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "AdjustGist", this.OperDept.ID);
                if (deptExt == null)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ȡ������չ��������ҩ��������ʧ�ܣ�"));
                }

                if (deptExt.StringProperty == "1")		//��ҩ
                    this.isAdjustInDrug = false;
                else
                    this.isAdjustInDrug = true;			//��ҩ
            }
            catch { }

            #endregion

            //{23E209BB-B4EB-4676-851A-DA053F0D51FD}  ��ʼ���ն˰�����
            FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManagement = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
            ArrayList alSendTerminalList = drugStoreManagement.QueryDrugTerminalByDeptCode(this.OperDept.ID, "0");
            if (alSendTerminalList != null)
            {
                this.hsTerminalDictionary.Clear();

                foreach (FS.FrameWork.Models.NeuObject info in alSendTerminalList)
                {
                    this.hsTerminalDictionary.Add(info.ID, info.Name);
                }
            }
        }

        /// <summary>
        /// ��ʼ����������
        /// </summary>
        private void InitTreeManager(DrugStore.OutpatientFun winFumModle)
        {
            switch (winFumModle)
            {
                case OutpatientFun.Drug:            //��ҩ

                    #region ��ҩ  ������������

                    this.AddTree = this.tvPrinting;     //�������
                    this.AddTree.State = "0";
                    this.AddTree.ParentTab = this.tpPrinting;

                    this.OperTree = this.tvPrinted;     //���ݲ���
                    this.OperTree.State = "1";
                    this.OperTree.ParentTab = this.tpPrinted;

                    this.NextTree = this.OperTree;       //���ݺ��

                    if (this.neuTabControl1.TabPages.Contains(this.tpDruged))           //����ҩδ��ҩ
                        this.neuTabControl1.TabPages.Remove(this.tpDruged);		       
                    if (this.neuTabControl1.TabPages.Contains(this.tpSend))             //�ѷ�ҩ
                        this.neuTabControl1.TabPages.Remove(this.tpSend);		        
                    if (!this.neuTabControl1.TabPages.Contains(this.tpPrinted))         //�Ѵ�ӡTabҳ
                        this.neuTabControl1.TabPages.Add(this.tpPrinted);
                    if (!this.neuTabControl1.TabPages.Contains(this.tpPrinting))        //δ��ӡTabҳ
                        this.neuTabControl1.TabPages.Add(this.tpPrinting);

                    #endregion

                    break;
                case OutpatientFun.Send:            //��ҩ

                    #region ��ҩ  ��������������

                    this.AddTree = this.tvDruged;       //�������
                    this.AddTree.State = "2";
                    this.AddTree.ParentTab = this.tpDruged;

                    this.OperTree = this.AddTree;      //���ݲ���

                    this.NextTree = this.tvSend;        //���ݺ��
                    this.NextTree.State = "3";
                    this.NextTree.ParentTab = this.tpSend;

                    if (this.neuTabControl1.TabPages.Contains(this.tpPrinted))
                        this.neuTabControl1.TabPages.Remove(this.tpPrinted);		    // �Ѵ�ӡ
                    if (this.neuTabControl1.TabPages.Contains(this.tpPrinting))
                        this.neuTabControl1.TabPages.Remove(this.tpPrinting);		    // δ��ӡ
                    if (!this.neuTabControl1.TabPages.Contains(this.tpDruged))          //�Ѵ�ӡδ��ҩTabҳ
                        this.neuTabControl1.TabPages.Add(this.tpDruged);
                    if (!this.neuTabControl1.TabPages.Contains(this.tpSend))            //�ѷ�ҩTabҳ
                        this.neuTabControl1.TabPages.Add(this.tpSend);

                    #endregion

                    break;
                case OutpatientFun.DirectSend:      //ֱ�ӷ�ҩ

                    #region ֱ�ӷ�ҩ ��������������

                    this.AddTree = this.tvPrinting;     //�������
                    this.AddTree.State = "0";
                    this.AddTree.ParentTab = this.tpPrinting;

                    this.OperTree = this.tvDruged;      //���ݲ���
                    this.OperTree.State = "1";
                    this.OperTree.ParentTab = this.tpDruged;

                    this.NextTree = this.tvSend;        //���ݺ��
                    this.NextTree.State = "3";
                    this.NextTree.ParentTab = this.tpSend;

                    if (this.neuTabControl1.TabPages.Contains(this.tpPrinted))
                        this.neuTabControl1.TabPages.Remove(this.tpPrinted);		    // �Ѵ�ӡ
                    //����ʾδ��ӡTabҳ ֻҪ�ѷ�ҩ��δ��ҩ
                    if (this.neuTabControl1.TabPages.Contains(this.tpPrinting))        //δ��ӡTabҳ
                        this.neuTabControl1.TabPages.Remove(this.tpPrinting);

                    if (!this.neuTabControl1.TabPages.Contains(this.tpDruged))          //�Ѵ�ӡδ��ҩTabҳ
                        this.neuTabControl1.TabPages.Add(this.tpDruged);

                    if (!this.neuTabControl1.TabPages.Contains(this.tpSend))            //�ѷ�ҩTabҳ
                        this.neuTabControl1.TabPages.Add(this.tpSend);

                    #endregion

                    break;
                case OutpatientFun.Back:            //��ҩ

                    #region ��ҩ ��������������

                    this.AddTree = this.tvDruged;       //������� / ����
                    this.AddTree.State = "2";
                    this.AddTree.Parent = this.tpDruged;

                    this.OperTree = this.AddTree;

                    this.neuTabControl1.TabPages.Remove(this.tpPrinted);		//�Ѵ�ӡ
                    this.neuTabControl1.TabPages.Remove(this.tpPrinting);		//δ��ӡ
                    this.neuTabControl1.TabPages.Remove(this.tpSend);			//�Ѻ�׼

                    #endregion

                    break;
            }
        }

        #endregion

        #region ��д����

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="funMode"></param>
        public override void SetFunMode(OutpatientFun winFunMode)
        {
            this.funModle = winFunMode;
            //���ݲ�ͬ���ڹ������ò���
            switch (winFunMode)
            {
                case OutpatientFun.Drug:            //��ҩ
                    this.funType = "1";
                    break;
                case OutpatientFun.Send:            //��ҩ
                    this.funType = "0";
                    break;
                case OutpatientFun.DirectSend:      //ֱ�ӷ�ҩ
                    this.funType = "3";
                    break;
                case OutpatientFun.Back:            //��ҩ
                    this.funType = "2";
                    break;
            }

            this.InitTreeManager(winFunMode);
        }

        /// <summary>
        /// ���������ն�
        /// </summary>
        /// <param name="winTerminal">���������ն�ʵ����Ϣ</param>
        public override void SetTerminal(FS.HISFC.Models.Pharmacy.DrugTerminal winTerminal)
        {
            base.SetTerminal(winTerminal);

            this.processRefreshInterval = (int)winTerminal.RefreshInterval1;
            this.ledRefreshInterval = (int)winTerminal.RefreshInterval2;

            this.IsAutoPrint = winTerminal.IsAutoPrint;

            if (this.terminal.TerminalType == FS.HISFC.Models.Pharmacy.EnumTerminalType.��ҩ����)
            {
                if (this.funModle == OutpatientFun.DirectSend)
                    this.IsShowFeeData = false;
                else
                    this.IsShowFeeData = true;
            }
            else
            {
                this.IsShowFeeData = false;
            }
        }

        /// <summary>
        /// ��ǰ������Ա��Ϣ
        /// </summary>
        public override FS.FrameWork.Models.NeuObject OperInfo
        {
            get
            {
                return base.OperInfo;
            }
            set
            {
                base.OperInfo = value;

                if (value != null)
                {
                    this.txtPID.Text = value.ID;
                    this.operName.Text = value.Name;

                    this.privOperID = value.ID;
                }
                else
                {
                    this.operName.Text = FS.FrameWork.Management.Language.Msg( "��Ȩ����Ա" );
                }
            }
        }

        /// <summary>
        /// ���ý���
        /// </summary>
        public void SetFocus()
        {
            this.txtPID.Select();
            this.txtPID.Focus();
            this.txtPID.SelectAll();
        }
        #endregion

        #region ������

        /// <summary>
        /// ��ʾ�б���������
        /// </summary>
        public void SumPatientNum()
        {
            this.tpPrinted.Text = "�� �� ӡ" + "[" + this.tvPrinted.Nodes.Count.ToString() + "]";
            this.tpPrinting.Text = "δ �� ӡ" + "[" + this.tvPrinting.Nodes.Count.ToString() + "]";
            this.tpDruged.Text = "δ �� ҩ" + "[" + this.tvDruged.Nodes.Count.ToString() + "]";
            this.tpSend.Text = "�� �� ҩ" + "[" + this.tvSend.Nodes.Count.ToString() + "]";
        }

        /// <summary>
        /// �ı�ڵ�λ��
        /// </summary>
        public virtual void ChangeNodeLocation()
        {
            if (this.funModle == OutpatientFun.Drug)
                this.DelNode();
            else
                this.TransferNode();
        }

        /// <summary>
        /// �ı�ڵ�λ��
        /// </summary>
        protected virtual void TransferNode()
        {
            if (this.AddTree == null)
                return;

            TreeNode tempNode = null;
            if (this.funModle == OutpatientFun.DirectSend)      //ֱ�ӷ�ҩ������� �����ڵ�λ��OperTree�� 
                tempNode = this.OperTree.SelectedNode;
            else
                tempNode = this.AddTree.SelectedNode;

            if (tempNode != null)
            {
                this.TransferNode(tempNode,true);
            }
        }

        /// <summary>
        /// �ı�ڵ�λ��
        /// </summary>
        /// <param name="node">��ת�ƽڵ�</param>
        /// <param name="targetToNextTree">�Ƿ�ת����NextTree������ True ת����NextTree False ת����OperTree����</param>
        protected virtual void TransferNode(TreeNode node,bool targetToNextTree)
        {
            //�Ƴ�ԭ�ڵ�
            this.AddTree.Nodes.Remove(node);

            // {F8D76CE8-6A0C-469b-AC43-4F69B2FCBAD8}  �����Զ���ҩ����
            if (this.funModle == OutpatientFun.Drug)        //��ҩ���� 
            {
                if (isAutoDruged)                           //�����Զ���ҩģʽ
                {
                    #region �Զ���ҩģʽ �Զ������ҩ����

                    FS.HISFC.Models.Pharmacy.DrugRecipe info = node.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;

                    FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                    ArrayList al = itemManager.QueryApplyOutListForClinic(this.OperDept.ID, "M1", "0", info.RecipeNO);
                    if (al == null)
                    {
                        if (this.ProcessMessageEvent != null)
                        {
                            this.ProcessMessageEvent(null, "���ݵ�����Ϣ��ȡ������ϸ��Ϣ��������" + itemManager.Err);
                        }
                        return;
                    }

                    List<FS.HISFC.Models.Pharmacy.ApplyOut> applyData = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut tempApply in al)
                    {
                        applyData.Add(tempApply);
                    }

                    if (Function.OutpatientDrug(applyData, this.terminal, this.ApproveDept, this.OperInfo, this.isAdjustInDrug) == -1)
                    {
                        if (this.ProcessMessageEvent != null)
                        {
                            this.ProcessMessageEvent(null, string.Format("��{0}({1})�Ĵ��������Զ���ҩ��������", info.PatientName, info.RecipeNO));
                        }
                    }

                    if (this.ProcessMessageEvent != null)
                    {
                        this.ProcessMessageEvent(null, string.Format("��{0}({1})�Ĵ���������Զ���ҩ", info.PatientName, info.RecipeNO));
                    }

                    return;

                    #endregion
                }
            }

            if (targetToNextTree)
            {
                #region  ���ڵ�ת�Ƶ�NextTree

                if (this.NextTree != null)
                {
                    FS.HISFC.Models.Pharmacy.DrugRecipe tempRecipe = node.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
                    if (tempRecipe != null)
                    {
                        tempRecipe.RecipeState = this.NextTree.State;
                    }

                    
                    //{DF70D8FF-A1DD-421b-8E4A-4637745F1927}
                    //��ӽڵ�ʱ�ж��Ƿ��Ѿ����ڸĻ��ߵĽڵ㣬���û�У����¼ӣ�����У�������ѡ��һ��
                    //this.NextTree.Nodes.Add(node);
                    if (this.NextTree.Nodes.ContainsKey(node.Name) == false)
                    {
                        this.NextTree.Nodes.Add(node);
                    }
                    else
                    {
                        //this.OperTree.Nodes.Remove(this.OperTree.Nodes[node.Name]);
                        //this.OperTree.Nodes.Add(node);
                        this.NextTree.SelectedNode = null;
                        this.NextTree.SelectedNode = this.NextTree.Nodes[node.Name];
                    }
                }

                #endregion
            }
            else
            {
                #region ���ڵ�ת����OperTree

                if (this.OperTree != null)
                {
                    FS.HISFC.Models.Pharmacy.DrugRecipe tempRecipe = node.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
                    if (tempRecipe != null)
                    {
                        tempRecipe.RecipeState = this.OperTree.State;
                    }

                    //{DF70D8FF-A1DD-421b-8E4A-4637745F1927}
                    //��ӽڵ�ʱ�ж��Ƿ��Ѿ����ڸĻ��ߵĽڵ㣬���û�У����¼ӣ�����У�������ѡ��һ��
                    //this.OperTree.Nodes.Add(node);
                    if (this.OperTree.Nodes.ContainsKey(node.Name) == false)
                    {
                        this.OperTree.Nodes.Add(node);
                    }
                    else
                    {
                        //this.OperTree.Nodes.Remove(this.OperTree.Nodes[node.Name]);
                        //this.OperTree.Nodes.Add(node);
                        this.OperTree.SelectedNode = null;
                        this.OperTree.SelectedNode = this.OperTree.Nodes[node.Name];
                    }
                }

                #endregion
            }
        }

        /// <summary>
        /// ɾ���ڵ�
        /// </summary>
        protected virtual void DelNode()
        {
            if (this.OperTree == null)
                return;

            TreeNode tempNode = this.OperTree.SelectedNode;
            if (tempNode != null)
            {
                this.OperTree.Nodes.Remove(tempNode);
            }
        }

        /// <summary>
        /// �������������Ľڵ�
        /// </summary>
        public virtual int FindNode()
        {
            switch (this.nowBillType)
            {
                case OutpatientBillType.��Ʊ��:
                    this.txtBillNO.Text = this.txtBillNO.Text.PadLeft(12, '0');
                    break;
                case OutpatientBillType.��������:
                    this.txtBillNO.Text = this.txtBillNO.Text.PadLeft(10, '0');
                    break;
                default:
                    this.txtBillNO.Text = this.txtBillNO.Text;
                    break;
            }
            string strQueryData = this.txtBillNO.Text;
            if (this.nowBillType == OutpatientBillType.��Ʊ��)
            {
                strQueryData = this.GetInvoiceList(strQueryData);
                if (strQueryData == null)
                {
                    return -1;
                }
                if (strQueryData == "")
                {
                    strQueryData = this.txtBillNO.Text;
                }
            }

            return this.FindNode(strQueryData);
        }

        /// <summary>
        /// �������������Ľڵ�
        /// </summary>
        /// <param name="queryStr">�������ַ���</param>
        /// <returns>1 �ɹ��ҵ� 0 δ�ҵ� ��1 ��������</returns>
        public virtual int FindNode(string queryStr)
        {
            bool nodeInTree = false;
            if (this.OperTree != null)
            {
                nodeInTree = this.FindNodeInTree(queryStr, this.OperTree);
            }
            if (!nodeInTree && this.NextTree != null && this.OperTree != this.NextTree)
            {
                nodeInTree = this.FindNodeInTree(queryStr, this.NextTree);
            }
            if (!nodeInTree && this.AddTree != null && this.AddTree != this.OperTree && this.AddTree != this.NextTree)
            {
                nodeInTree = this.FindNodeInTree(queryStr, this.AddTree);
            }
            if (!nodeInTree)        // δ�ҵ��ڵ�
            {
                #region δ�ҵ��ڵ�

                #region ���ݵ��ݺ����¼�������

                ArrayList al = this.drugStoreManager.QueryDrugRecipe(this.OperDept.ID, "M1", "A", (int)this.nowBillType, queryStr);
                if (al == null)
                {
                    MessageBox.Show(Language.Msg("���ݵ�ǰ���ݼ�����ʽδ�ҵ����������ĵ�����Ϣ") + this.drugStoreManager.Err);
                    return -1;
                }
                if (al.Count == 0)
                {
                    MessageBox.Show(Language.Msg("�õ��ݺŲ����ڻ����׼ ���֤����������"));
                    this.txtPID.Focus();
                    return -1;
                }

                #endregion

                FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipeTemp = al[0] as FS.HISFC.Models.Pharmacy.DrugRecipe;

                string strMsg = "";
                switch (drugRecipeTemp.RecipeState)
                {
                    case "0":
                        strMsg = "�õ�����δ��ӡ��ǩ";
                        break;
                    case "1":
                        strMsg = "�õ����Ѵ�ӡ��ǩ ��δ����ҩȷ��";
                        break;
                    case "2":
                        strMsg = "�õ�������ҩȷ�� ��δ����ҩȷ��";
                        break;
                    case "3":
                        strMsg = "�õ����ѽ��й���ҩ";
                        break;
                }

                MessageBox.Show(strMsg);

                #region ���ݼ�����������״̬���в�� ���벻ͬ״̬���� ��(���ݲ����ż����Ŵ��ڴ�����)

                ArrayList alAdd = new ArrayList();
                ArrayList alOper = new ArrayList();
                ArrayList alNext = new ArrayList();

                foreach (FS.HISFC.Models.Pharmacy.DrugRecipe tempInfo in al)
                {
                    if (this.AddTree != null && this.AddTree.State == tempInfo.RecipeState)
                    {
                        alAdd.Add(tempInfo);
                        continue;
                    }
                    if (this.OperTree != null && this.OperTree.State == tempInfo.RecipeState)
                    {
                        alOper.Add(tempInfo);
                        continue;
                    }
                    if (this.NextTree != null && this.NextTree.State == tempInfo.RecipeState)
                    {
                        alNext.Add(tempInfo);
                        continue;
                    }
                }

                #endregion

                bool isOperActive = true;

                if (this.NextTree != null && alNext.Count > 0)
                {
                    this.NextTree.ShowList(alNext, true);
                    //this.neuTabControl1.SelectedTab = this.NextTree.ParentTab;
                    isOperActive = false;
                }

                //�Ƿ���AddTree��������� ��δ��ӡ��ǩ���д�ӡ������ӽڵ�
                if (this.isFindToAdd) 
                {
                    if (this.AddTree != null && alAdd.Count > 0)
                    {
                        if (this.AddTree.State == "0")          //δ��ӡ��ǩ ���´�ӡ���
                        {
                            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe in alAdd)
                            {
                                int parm = this.drugStoreManager.UpdateDrugRecipeState(this.OperDept.ID, drugRecipe.RecipeNO, "M1", "0", "1");
                                if (parm == -1)
                                {
                                    MessageBox.Show(Language.Msg("���°�ҩ����ϸ δ��ӡ״̬Ϊ�Ѵ�ӡ״̬ʧ��!") + this.drugStoreManager.Err);
                                    return -1;
                                }
                            }
                        }
                        //���ڴ�������������ʱ ������ֱ�Ӹ�����״̬ ������ӵ�AddTree��
                        this.OperTree.ShowList(alAdd, true);

                        //this.neuTabControl1.SelectedTab = this.OperTree.ParentTab;  
                        isOperActive = true;
                    }
                }                        

                if (this.OperTree != null && alOper.Count > 0)
                {
                    this.OperTree.ShowList(alOper, true);
                    //this.neuTabControl1.SelectedTab = this.OperTree.ParentTab;
                    isOperActive = true;
                }
                //�����������  ���ý���TabPage
                if (alAdd.Count > 0 || alOper.Count > 0 || alNext.Count > 0)
                {
                    if (isOperActive)
                    {
                        this.neuTabControl1.SelectedTab = this.OperTree.ParentTab;
                    }
                    else
                    {
                        this.neuTabControl1.SelectedTab = this.NextTree.ParentTab;
                    }
                }

                return 0;

                #endregion
            }

            return 1;
        }

        /// <summary>
        /// �����ڻ�ȡ�ڵ�
        /// </summary>
        /// <param name="queryStr"></param>
        /// <returns></returns>
        private bool FindNodeInTree(string queryStr, DrugStore.Outpatient.tvClinicTree findTree)
        {
            if (findTree.Nodes.Count > 0)
            {
                FS.HISFC.Models.Pharmacy.DrugRecipe temp;
                foreach (TreeNode node in findTree.Nodes)
                {
                    temp = node.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
                    if (this.GetBillFromDrugeRecipe(temp) == queryStr)
                    {
                        findTree.SelectedNode = node;

                        if (findTree.Parent != null)
                        {
                            if ((findTree.Parent as System.Windows.Forms.TabPage) != null)
                            {
                                this.neuTabControl1.SelectedTab = findTree.Parent as System.Windows.Forms.TabPage;
                            }
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// ѡ����һ���ڵ�
        /// </summary>
        private void SelectNext(bool isDown)
        {
            if (this.OperTree != null && this.OperTree.ParentTab == this.neuTabControl1.SelectedTab)
                this.OperTree.SelectNext(isDown);
            else if (this.NextTree != null && this.NextTree.ParentTab == this.neuTabControl1.SelectedTab)
                this.NextTree.SelectNext(isDown);
            else if (this.AddTree != null && this.AddTree.ParentTab == this.neuTabControl1.SelectedTab)
                this.AddTree.SelectNext(isDown);

            this.txtPID.SelectAll();
        }

        #endregion

        /// <summary>
        /// ���õ��ݼ���ģʽ
        /// </summary>
        /// <param name="billType"></param>
        public virtual void SetQueryBillType(OutpatientBillType billType)
        {
            this.nowBillType = billType;

            this.lbnBillType.Text = FS.FrameWork.Management.Language.Msg( this.nowBillType.ToString() );
        }

        /// <summary>
        /// �����б���ʾ ���߳��Զ�ˢ��ʱ����
        /// </summary>
        public virtual void ShowList()
        {
            ArrayList al = new ArrayList();
            al = this.drugStoreManager.QueryList(this.OperDept.ID, this.terminal.ID, this.funType, this.AddTree.State, this.minQueryDate);
            string strMsg = "";
            if (al == null)
                strMsg = "�˴�ˢ�·�������...." + this.drugStoreManager.Err;
            else
                strMsg = "��ȡ" + al.Count + "�����ݣ��ȴ��´�ˢ��....";
            if (this.ProcessMessageEvent != null)
                this.ProcessMessageEvent("ShowList", strMsg);

            if (al.Count == 0)
            {
                return;
            }

            if (this.AddTree.State == "0")
            {
                this.AddTree.ShowList(al, false);
            }
            else
            {   //{DBB3C382-BB23-463b-8847-2F73C55F2586} �����Զ���ҩ           
                if (this.funModle == OutpatientFun.Send && isAutoSend)      //�����Զ���ҩģʽ
                {
                    this.AutoSave(al);                  //ʵ���Զ�����

                    this.tvSend.ShowList(al, true);     //�ڵ�ֱ�Ӹ��µ��ѷ�ҩ������
                }
                else
                {
                    this.AddTree.ShowList(al, true);    //��������
                }
                //{DBB3C382-BB23-463b-8847-2F73C55F2586}
            }

            //�����´β�ѯʱ��
            this.GetMinDrugedDate(al);

            if (this.funModle == OutpatientFun.Drug || this.funModle == OutpatientFun.DirectSend)
            {
                this.AutoPrint();
            }

            this.SetFocus();
        }

        /// <summary>
        /// �Զ�����
        /// 
        /// {DBB3C382-BB23-463b-8847-2F73C55F2586}
        /// </summary>
        /// <returns></returns>
        protected int AutoSave(ArrayList alRecipe)
        {
            foreach (FS.HISFC.Models.Pharmacy.DrugRecipe info in alRecipe)
            {
                #region ��ɷ�ҩ����

                FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
                ArrayList al = itemManager.QueryApplyOutListForClinic(this.OperDept.ID, "M1", "1", info.RecipeNO);
                if (al == null)
                {
                    if (this.ProcessMessageEvent != null)
                    {
                        this.ProcessMessageEvent(null, "���ݵ�����Ϣ��ȡ������ϸ��Ϣ��������" + itemManager.Err);
                    }
                    return -1;
                }

                if (al.Count == 0)
                {
                    if (this.ProcessMessageEvent != null)
                    {
                        this.ProcessMessageEvent(null, "���ݵ�����Ϣδ��ȡ����ϸ������Ϣ");
                    }
                    return 0;
                }
                List<FS.HISFC.Models.Pharmacy.ApplyOut> applyData = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut tempApply in al)
                {
                    applyData.Add(tempApply);
                }

                foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in applyData)
                {
                    applyOut.Operation.ApproveOper.Dept = this.ApproveDept;
                }

                if (Function.OutpatientSend(applyData, this.terminal, this.ApproveDept, this.OperInfo, false, !this.isAdjustInDrug) == -1)
                {
                    if (this.ProcessMessageEvent != null)
                    {
                        this.ProcessMessageEvent(null, string.Format("��{0}({1})�Ĵ��������Զ���ҩ��������", info.PatientName, info.RecipeNO));
                    }
                }

                if (this.ProcessMessageEvent != null)
                {
                    this.ProcessMessageEvent(null, string.Format("��{0}({1})�Ĵ���������Զ���ҩ", info.PatientName, info.RecipeNO));
                }

                #endregion

                #region ���´�����������״̬

                info.RecipeState = "3";
                info.SendOper.ID = this.OperInfo.ID;
                info.SendOper.Name = this.OperInfo.Name;

                #endregion
            }

            return 1;

        }

        /// <summary>
        /// �б��ʼ��/ˢ����ʾ
        /// </summary>
        /// <param name="isInitRefreshDate">�Ƿ񽫼���ʱ�����³�ʼ��(����Ϊ�����������)</param>
        public virtual void RefreshOperList(bool isInitRefreshDate)
        {
            if (isInitRefreshDate)
            {
                this.minQueryDate = this.sysDate.Date;

                if (this.isShowOldInfo)
                {
                    this.minQueryDate = new DateTime(2000, 1, 1, 10, 0, 0);
                }

            }

            //�����շ�ʱ�����
            ArrayList al = new ArrayList();
            al = this.drugStoreManager.QueryList(this.OperDept.ID, this.terminal.ID, this.funType, this.OperTree.State, this.minQueryDate);
            if (al != null)
            {
                this.OperTree.ShowList(al, false);
            }

            //�����´β�ѯʱ��
            this.GetMinDrugedDate(al);

            if (this.funModle == OutpatientFun.Send || this.funModle == OutpatientFun.DirectSend)
            {
                al = this.drugStoreManager.QueryList(this.OperDept.ID, this.terminal.ID, this.funType, this.NextTree.State, this.sysDate.Date);
                if (al != null)
                {
                    this.NextTree.ShowList(al, false);
                }
            }
        }

        /// <summary>
        /// ��ȡ����Ĵ������������������ҩʱ��
        /// </summary>
        /// <param name="drugRecipeAl">������������</param>
        private void GetMinDrugedDate(ArrayList drugRecipeAl)
        {
            if (this.funModle == OutpatientFun.Drug || this.funModle == OutpatientFun.Back || this.funModle == OutpatientFun.DirectSend)
                return;

            if (drugRecipeAl.Count > 0)
            {
                this.minQueryDate = System.DateTime.MinValue;
                foreach (FS.HISFC.Models.Pharmacy.DrugRecipe info in drugRecipeAl)
                {
                    if (info.DrugedOper.OperTime > this.minQueryDate)
                    {
                        this.minQueryDate = info.DrugedOper.OperTime;
                    }
                }
            }

            //���ý���������ҩʱ����ʾ
            this.lbFeeDate.Text = this.minQueryDate.ToString("HH:mm");

            //�����ʱ��������һ�� Sql�����ʹ�õĴ��ڵ���
            this.minQueryDate = this.minQueryDate.AddSeconds(1);
        }

        /// <summary>
        /// ��������һ����Ʊ�Ż�ȡ�����ֵܷ�Ʊ��
        /// </summary>
        /// <param name="invoiceNo">��Ʊ��</param>
        /// <returns>�ɹ����������ֵܷ�Ʊ�����Ӵ� ��,�ָ�</returns>
        private string GetInvoiceList(string invoiceNo)
        {
            string strInvoiceList = "";
            //FS.HISFC.BizLogic.Fee.Outpatient feeOutPatient = new FS.HISFC.BizLogic.Fee.OutPatient();
            //ArrayList alInvoice = feeOutPatient.GetBrotherInvo(invoiceNo);
            //if (alInvoice == null)
            //{
            //    MessageBox.Show(Language.Msg("��������Ʊ�Ų��������ֵܷ�Ʊ����") + feeOutPatient.Err);
            //    return null;
            //}
            //foreach (FS.HISFC.Models.Fee.OutPatient.Invoice info in alInvoice)
            //{
            //    strInvoiceList = strInvoiceList + info.ID + "','";
            //}
            //if (strInvoiceList.Length > 3)
            //    strInvoiceList = strInvoiceList.Substring(0, strInvoiceList.Length - 3);
            return strInvoiceList;
        }

        /// <summary>
        /// ������ѡ��ĵ������� ����ʵ���ڵĶ�Ӧ��Ϣ
        /// </summary>
        /// <param name="info">��������ʵ��</param>
        /// <returns>�ɹ����ض�Ӧ�Ĵ���ʵ������Ϣ</returns>
        private string GetBillFromDrugeRecipe(FS.HISFC.Models.Pharmacy.DrugRecipe info)
        {
            switch (this.nowBillType)
            {
                case OutpatientBillType.������:						//������
                    return info.RecipeNO;
                case OutpatientBillType.��Ʊ��:						//��Ʊ��
                    return info.InvoiceNO;
                default:					                        //��������
                    return info.CardNO;
            }
        }

        /// <summary>
        /// ���
        /// </summary>
        public void Clear()
        {
            if (this.AddTree != null)
                this.AddTree.Nodes.Clear();
            if (this.NextTree != null)
                this.NextTree.Nodes.Clear();
            if (this.OperTree != null)
                this.OperTree.Nodes.Clear();
        }

        /// <summary>
        /// ���ݲ���Ա�����жϸò���Ա�Ƿ��б��ն˲���Ȩ��
        /// ����������ҩ�����˸���ҩ����ҩ
        /// </summary>
        /// <param name="person">���ж���Ա��Ϣ</param>
        /// <returns>����Ȩ�޷���True ���򷵻�False</returns>
        public bool JudgePriv(FS.HISFC.Models.Base.Employee person)
        {
            if (!this.isJudgePriv)
                return true;
            if (person.EmployeeType.ID.ToString() == FS.HISFC.Models.Base.EnumEmployeeType.P.ToString())
                return true;
            return false;
        }

        /// <summary>
        /// �Ƿ����ʣ��δ�����ڵ�
        /// </summary>
        /// <returns></returns>
        public bool SpareNode()
        {
            if (this.AddTree == null || this.NextTree == null)
                return false;

            if (this.AddTree.Nodes.Count > 0 || this.NextTree.Nodes.Count > 0)
                return true;
            else
                return false;
        }

        #region ��ǩ��ӡ / �Զ���ӡ 

        /// <summary>
        /// ��ǩ��ӡ ��ӡ��ǰѡ�нڵ��ǩ
        /// </summary>
        public virtual void Print()
        {
            if (Function.IDrugPrint == null)
            {
                return;
            }

            if (this.OperTree != null && this.neuTabControl1.SelectedTab == this.OperTree.ParentTab)            
            {
                if (this.OperTree.SelectedNode != null)
                {
                    this.Print(this.OperTree,this.OperTree.SelectedNode);

                    return;
                }
            }
            if (this.NextTree != null && this.neuTabControl1.SelectedTab == this.NextTree.ParentTab)
            {
                if (this.NextTree.SelectedNode != null)
                {
                    this.Print(this.NextTree, this.NextTree.SelectedNode);

                    return;
                }
            }
            if (this.AddTree != null && this.neuTabControl1.SelectedTab == this.AddTree.ParentTab)
            {
                if (this.AddTree.SelectedNode != null)                
                {
                    this.Print(this.AddTree, this.AddTree.SelectedNode);

                    return;
                }
            }
        }

        /// <summary>
        /// �Ƿ����ڴ�ӡ ���ڶ��ڶ��߳��ж�
        /// </summary>
        private static bool isBusyPrint = false;

        /// <summary>
        /// �Ƿ����ڱ���  ���ڶԶ��߳��ж�
        /// </summary>
        private bool isBusy = false;

        /// <summary>
        /// �Զ���ӡʱ����
        /// </summary>
        protected virtual int AutoPrint()
        {
            if (isBusyPrint)
            {
                return 1;
            }

            isBusyPrint = true;

            if (this.tvPrinting.Nodes.Count <= 0)
            {
                isBusyPrint = false;
                return -1;
            }

            if (this.changeNode == null)
            {
                changeNode = new ChangePrintNodeHandler(this.TransferNode);
            }

            try
            {
                for (int i = this.tvPrinting.Nodes.Count - 1; i >= 0; i--)
                {
                    if (this.isBusySave)
                    {
                        return 1;
                    }

                    if (this.IsAutoPrint)
                    {
                        if (this.Print(this.tvPrinting, this.tvPrinting.Nodes[i]) != 1)
                            return -1;
                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.DrugRecipe info = this.tvPrinting.Nodes[i].Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;

                        int parm = this.drugStoreManager.UpdateDrugRecipeState(this.OperDept.ID, info.RecipeNO, "M1", "0", "1");
                        if (parm == -1)
                        {
                            MessageBox.Show(Language.Msg("���°�ҩ����ϸ δ��ӡ״̬Ϊ�Ѵ�ӡ״̬ʧ��!") + this.drugStoreManager.Err);
                            return -1;
                        }
                    }

                    //�ı���λ��
                    if (this.ParentForm != null)
                    {
                        if (this.funModle == OutpatientFun.DirectSend)
                            this.ParentForm.Invoke(changeNode, new object[] { this.tvPrinting.Nodes[i],false });
                        else
                            this.ParentForm.Invoke(changeNode, new object[] { this.tvPrinting.Nodes[i], true });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                isBusyPrint = false;
            }

            return 1;
        }

        /// <summary>
        /// ��ӡ��ǰָ���ڵ�����
        /// </summary>
        /// <param name="printNode">���ӡ�ڵ�</param>
        private int Print(FS.HISFC.Components.DrugStore.Outpatient.tvClinicTree parentTree,TreeNode printNode)
        {
            ArrayList alValidate = new ArrayList();
            //���ڽڵ�ת��ʱ û�жԽڵ�Tag��ʵ��״̬�����޸� ���Դ˴�����ʹ��Info.state ���� ״̬����
            FS.HISFC.Models.Pharmacy.DrugRecipe info = printNode.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
            string detailState = "0";
            if (parentTree.State == "0" || parentTree.State == "1")
            {
                detailState = "0";
            }
            else if (parentTree.State == "2")
            {
                detailState = "1";
            }
            else
            {
                detailState = "2";
            }

            ArrayList alInfo = this.itemManager.QueryApplyOutListForClinic(this.OperDept.ID, "M1",detailState, info.RecipeNO);
            if (alInfo == null)
            {
                MessageBox.Show(Language.Msg("��ӡ��ҩ�� ��ȡ����ҩ��ϸ����!") + itemManager.Err);
                return -1;
            }
            //ֻȡ��Ч��¼
            bool isAllValid = true;
            alValidate = new ArrayList();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp in alInfo)
            {
                if (applyOutTemp.ValidState != FS.HISFC.Models.Base.EnumValidState.Invalid)
                    alValidate.Add(applyOutTemp);
                else
                    isAllValid = false;
            }

            //����˸�ҩ��� �򲻽��д�ӡ
            isAllValid = !info.IsModify;

            //���ӡ���ϼ�¼ �� ���м�¼ȫ����Ч ��ִ�д�ӡ
            if (this.isPrintCancelRecipe || isAllValid)
            {
                if (this.Print(info,alValidate) == -1)
                {
                    return -1;
                }
            }

            //���´�ӡ���  //��״̬Ϊ0�����ݸ��´�ӡ���
            if (info.RecipeState == "0")
            {
                int parm = this.drugStoreManager.UpdateDrugRecipeState(this.OperDept.ID, info.RecipeNO, "M1", "0", "1");
                if (parm == -1)
                {
                    MessageBox.Show(Language.Msg("���°�ҩ����ϸ δ��ӡ״̬Ϊ�Ѵ�ӡ״̬ʧ��!") + this.drugStoreManager.Err);
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ִ�д�ӡ
        /// </summary>
        /// <param name="al">��ӡ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        internal int Print(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe, ArrayList al)
        {
            //һ��ֻ��ӡһ�������ŵ�
            //�����ʱ������Ϻš�Ժע��Ƿ��� ���ڴ�ӡ
            //applyOut.User01 ��ҩ���ں� applyOut.User02 Ժע����

            if (al.Count <= 0)
            {
                return 1;
            }

            if (Function.IDrugPrint == null)
            {
                return 0;
            }

            //{23E209BB-B4EB-4676-851A-DA053F0D51FD}  �ն˴��ڸ�ֵ
            if (string.IsNullOrEmpty(this.terminal.SendWindow.Name) == true)
            {
                if (this.hsTerminalDictionary.ContainsKey(this.terminal.SendWindow.ID))
                {
                    this.terminal.SendWindow.Name = this.hsTerminalDictionary[this.terminal.SendWindow.ID].ToString();
                }
            }

            FS.HISFC.Models.Registration.Register patientInfo = null;		//������Ϣ

            #region ������Ϣ��ȡ

            //��ȡ������Ϣ
            FS.HISFC.BizProcess.Integrate.Registration.Registration regManager = new FS.HISFC.BizProcess.Integrate.Registration.Registration();
            patientInfo = regManager.GetByClinic(drugRecipe.ClinicNO);

            patientInfo.InvoiceNO = drugRecipe.InvoiceNO;

            #endregion

            #region ��ҩ����ҩ����ҩ��ӡ
            if (this.isHerbalPrint)
            {
                patientInfo.User01 = drugRecipe.FeeOper.OperTime.ToString();

                patientInfo.DoctorInfo.Templet.Doct.Name = this.personHelper.GetName(drugRecipe.Doct.ID);
                //{EC943D35-7C5C-4e98-97F9-10AD1B70D0E2}  ���ӶԷ�ҩ���ںŵĸ�ֵ               
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut temp in al)
                {
                    temp.SendWindow = this.terminal.SendWindow.Name;
                }

                Function.IDrugPrint.OutpatientInfo = patientInfo;

                Function.IDrugPrint.AddAllData(al);
                Function.IDrugPrint.Print();

                return 1;
            }
            #endregion

            #region ��ȡ��ǩ��ҳ��
            string privCombo = "";												//�ϴ�ҽ����Ϻ�
            int iRecipeTotNum = 0;												//�������ӡ��ǩ��ҳ��
            string recipeNo = "";		//������
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut temp in al)
            {
                temp.SendWindow = this.terminal.SendWindow.Name;
                if (privCombo == temp.CombNO && temp.CombNO != "")
                {
                    continue;
                }
                else
                {
                    iRecipeTotNum = iRecipeTotNum + 1;
                    privCombo = temp.CombNO;
                }

                recipeNo = temp.RecipeNO;
            }
            #endregion

            Function.IDrugPrint.LabelTotNum = iRecipeTotNum;
            Function.IDrugPrint.DrugTotNum = al.Count;
            if (patientInfo != null)
            {
                patientInfo.User02 = al.Count.ToString();
                patientInfo.User01 = drugRecipe.FeeOper.OperTime.ToString();

                patientInfo.DoctorInfo.Templet.Doct.Name = this.personHelper.GetName(drugRecipe.Doct.ID);

                patientInfo.User03 = drugRecipe.RecipeNO;
                
                Function.IDrugPrint.OutpatientInfo = patientInfo;
            }

            privCombo = "-1";
            ArrayList alCombo = new ArrayList();

            if (!this.isAllSend)
            {
                #region ��ǩ��ӡ
                foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in al)
                {
                    info.SendWindow = this.terminal.SendWindow.Name;
                    if (privCombo == "-1" || (privCombo == info.CombNO && info.CombNO != ""))
                    {
                        alCombo.Add(info);
                        privCombo = info.CombNO;
                        continue;
                    }
                    else			//��ͬ������
                    {
                        if (alCombo.Count == 1)
                            Function.IDrugPrint.AddSingle(alCombo[0] as FS.HISFC.Models.Pharmacy.ApplyOut);
                        else
                            Function.IDrugPrint.AddCombo(alCombo);
                        Function.IDrugPrint.Print();

                        privCombo = info.CombNO;
                        alCombo = new ArrayList();

                        alCombo.Add(info);
                    }
                }
                if (alCombo.Count == 0)
                {
                    return 1;
                }
                if (alCombo.Count > 1)
                {
                    Function.IDrugPrint.AddCombo(alCombo);
                }
                else
                {
                    Function.IDrugPrint.AddSingle(alCombo[0] as FS.HISFC.Models.Pharmacy.ApplyOut);
                }

                Function.IDrugPrint.Print();

                #endregion
            }
            else
            {
                Function.IDrugPrint.AddAllData(al);
                Function.IDrugPrint.Print();
            }           

            return 1;
        }

        #endregion

        #region ���߳�ˢ�� �Ժ��Ǹ���Ϊ��Ϣ��ʽ����

        private System.Threading.Timer processRefreshTimer = null;
        private System.Threading.TimerCallback processRefreshCallBack = null;
        private delegate void ShowListDelegate();

        /// <summary>
        /// ����Ļ��ʾˢ��
        /// </summary>
        private System.Timers.Timer ledRefreshTimer = null;
    
        /// <summary>
        /// ����ˢ�¼��
        /// </summary>
        private int processRefreshInterval = 3;

        /// <summary>
        /// LED����Ļ��ʾˢ�¼��
        /// </summary>
        private int ledRefreshInterval = 55;

        private ShowListDelegate refreshFun = null;

        /// <summary>
        /// ���߳�ˢ��
        /// </summary>
        /// <param name="parm"></param>
        private void AutoRefresh(object parm)
        {
            if (this.isBusy)
            {
                if (this.ProcessMessageEvent != null)
                    this.ProcessMessageEvent(this, "���ڽ��б������..������ˢ��");
                return;
            }
            if (this.refreshFun == null)
                this.refreshFun = new ShowListDelegate(this.ShowList);
            this.isBusy = true;

            if (this.ParentForm != null)
                this.ParentForm.Invoke(this.refreshFun);

            this.isBusy = false;
        }

        /// <summary>
        /// ��ʼˢ�� ����ˢ��
        /// </summary>
        public virtual void BeginProcessRefresh(int dueTime)
        {
            if (this.processRefreshCallBack == null)
                this.processRefreshCallBack = new System.Threading.TimerCallback(this.AutoRefresh);
            this.processRefreshTimer = new System.Threading.Timer(this.processRefreshCallBack, null, dueTime, this.processRefreshInterval * 1000);
        }

        /// <summary>
        /// ��ʼˢ��(����ˢ��)
        /// </summary>
        /// <param name="dueTime"></param>
        public virtual new void BeginRefresh(int dueTime)
        {
            this.BeginProcessRefresh(dueTime);
        }

        /// <summary>
        /// ֹͣˢ�� ����ˢ��
        /// </summary>
        public virtual void EndProcessRefresh()
        {
            if (this.processRefreshTimer != null)
                this.processRefreshTimer.Dispose();
        }

        /// <summary>
        /// ֹͣˢ��(����ˢ��)
        /// </summary>
        public virtual new void EndRefresh()
        {
            this.EndProcessRefresh();
        }

        #region ����LED��Ļ��ʾ

        /// <summary>
        /// ����Ļ��Ϣ��ʾ
        /// </summary>
        public void WriteSendMessage()
        {
            if (this.isBusy)
            {
                if (this.ProcessMessageEvent != null)
                    this.ProcessMessageEvent(this, "���ڽ��б������..������ˢ��");
                return;
            }

            this.isBusy = true;

            List<FS.HISFC.Models.Pharmacy.DrugRecipe> alDrugRecipe = new List<FS.HISFC.Models.Pharmacy.DrugRecipe>();
            DateTime maxFeeDate = System.DateTime.MinValue;
            FS.HISFC.Models.Pharmacy.DrugRecipe info = new FS.HISFC.Models.Pharmacy.DrugRecipe();
            for (int i = this.tvDruged.Nodes.Count - 1; i >= 0; i--)
            {
                info = this.tvDruged.Nodes[i].Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
                if (i == this.tvDruged.Nodes.Count)         //ȡ���һ���ڵ���շ�ʱ�� ��ʱ�������µ���ҩ
                    maxFeeDate = info.FeeOper.OperTime;

                alDrugRecipe.Add(info);
            }

            //this.lbFeeDate.Text = maxFeeDate.ToString("HH:mm");

            if (this.LEDShowInterface != null)
            {               
                this.LEDShowInterface.SetShowData(alDrugRecipe);
                this.LEDShowInterface.Show();
            }

            this.isBusy = false;
        }

        #endregion

        /// <summary>
        /// ��ʼˢ�� LED����Ļ��ʾˢ��
        /// </summary>
        /// <param name="isExeAtOnce">�Ƿ�����ִ��</param>
        public virtual void BeginLEDRefresh(bool isExeAtOnce)
        {            
            if (isExeAtOnce)
            {
                this.WriteSendMessage();
            }

            this.ledRefreshTimer = new System.Timers.Timer(this.ledRefreshInterval * 1000);
            this.ledRefreshTimer.Elapsed -= new System.Timers.ElapsedEventHandler(ledRefreshTimer_Elapsed);
            this.ledRefreshTimer.Elapsed += new System.Timers.ElapsedEventHandler(ledRefreshTimer_Elapsed);
            this.ledRefreshTimer.Start();
        }

        private void ledRefreshTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.WriteSendMessage();
        }

        /// <summary>
        /// ֹͣˢ�� LED����Ļֹͣˢ��
        /// </summary>
        public virtual void EndLEDRefresh()
        {
            if (this.ledRefreshTimer != null)
                this.ledRefreshTimer.Dispose();
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                //��ʼ����Ϊ���ⲿ���ڵ���
                //if (!this.DesignMode)
                //{
                //    this.Init();
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            base.OnLoad(e);
        }

        private void tv_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Tag != null)
            {
                this.nowDrugRecipe = e.Node.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
                if (this.nowDrugRecipe == null)
                    return;
                this.txtBillNO.Text = this.GetBillFromDrugeRecipe(this.nowDrugRecipe);

                if (this.MyTreeSelectEvent != null)
                    this.MyTreeSelectEvent(this.nowDrugRecipe);
            }
        }

        private void txtPID_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyData == Keys.Down)
            {
                this.SelectNext(true);
                e.Handled = true;
            }
            if (e.KeyData == Keys.Up)
            {
                this.SelectNext(false);
                e.Handled = true;
            }
        }


        private void txtPID_KeyPress(object sender, KeyPressEventArgs e)
        {          
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (this.txtPID.Text.Length >= 7)
                {
                    #region ��ǰ�����Ϊ������

                    this.txtBillNO.Text = this.txtPID.Text;

                    if (this.FindNode() != 1)
                    {
                        if (this.privOperID != "")
                        {
                            this.txtPID.Text = this.privOperID;
                        }

                        return;
                    }

                    if (this.privOperID != "")
                    {
                        this.txtPID.Text = this.privOperID;
                    }
                    this.txtPID.SelectAll();
                    this.txtPID.Focus();
                    return;

                    #endregion
                }
                else
                {
                    #region Ա���ż���ȷ��

                    if (this.PIDFillNum != -1)
                    {
                        this.txtPID.Text = this.txtPID.Text.PadLeft(this.PIDFillNum, '0');
                    }
                    FS.FrameWork.Models.NeuObject person = this.personHelper.GetObjectFromID(this.txtPID.Text);
                    if (person == null || person.ID == "")
                    {
                        MessageBox.Show(Language.Msg("�����Ա������ ����������"));

                        this.txtPID.SelectAll();
                        this.txtPID.Focus();
                        return;
                    }
                    else
                    {
                        if (this.JudgePriv(person as FS.HISFC.Models.Base.Employee))
                        {
                            this.operName.Text = person.Name;

                            this.isBusy = true;

                            if (this.OperChangedEvent != null)
                            {
                                this.OperChangedEvent(person);
                            }

                            if (this.SaveRecipeEvent != null)
                            {
                                this.SaveRecipeEvent(person, System.EventArgs.Empty);
                            }

                            this.isBusy = false;

                            this.privOperID = person.ID;

                            this.txtPID.SelectAll();
                            this.txtPID.Focus();
                        }
                        else
                        {
                            this.OperInfo = null;
                            MessageBox.Show(Language.Msg("�ò���Ա�޲���Ȩ�� �������Ա��ϵ�����ն�"));

                            if (this.OperChangedEvent != null)
                            {
                                this.OperChangedEvent(this.OperInfo);
                            }

                            this.txtPID.Focus();
                            return;
                        }
                    }

                    #endregion
                }        
            }
        }

        private void lbnBillType_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int iBillIndex = 0;
            System.Math.DivRem(this.nowBillType.GetHashCode() + 1, 3, out iBillIndex);
            this.nowBillType = (OutpatientBillType)iBillIndex;

            this.lbnBillType.Text = FS.FrameWork.Management.Language.Msg( this.nowBillType.ToString() );
        }

        private void neuTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FS.FrameWork.WinForms.Controls.NeuTreeView selectTv = null;
            if (this.neuTabControl1.SelectedTab == null || this.neuTabControl1.SelectedTab.Controls.Count <= 0)
            {
                return;
            }
            selectTv = this.neuTabControl1.SelectedTab.Controls[0] as FS.FrameWork.WinForms.Controls.NeuTreeView;
            if (selectTv != null)
            {
                if (selectTv.Nodes.Count <= 0)
                {
                    return;
                }
                else
                {
                    selectTv.SelectedNode = selectTv.Nodes[selectTv.Nodes.Count - 1];
                }

                this.nowDrugRecipe = selectTv.SelectedNode.Tag as FS.HISFC.Models.Pharmacy.DrugRecipe;
                if (this.nowDrugRecipe == null)
                {
                    return;
                }

                this.txtBillNO.Text = this.GetBillFromDrugeRecipe(this.nowDrugRecipe);

                if (this.MyTreeSelectEvent != null)
                {
                    this.MyTreeSelectEvent(this.nowDrugRecipe);
                }
            }
        }

        private void txtBillNO_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                if (this.FindNode() != 1)
                {
                    return;
                }
                //�����䷢ҩʱˢ�����س�ʱ�Զ����� by Sunjh 2010-11-1 {5F239BB6-0973-40aa-B0DE-EEA7D3A0D819}
                FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                bool isAutoSaveByEnter = ctrlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_AutoSave_ByEnter, true, false);
                if (isAutoSaveByEnter)
                {
                    if (this.SaveRecipeEvent != null)
                    {
                        this.SaveRecipeEvent(null, System.EventArgs.Empty);
                    }
                }                
            }
        }
    }
}
