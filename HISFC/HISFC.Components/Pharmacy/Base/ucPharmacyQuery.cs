using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using FS.FrameWork.WinForms.Forms;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [��������: ҩƷ��ҳ��Ϣά��]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <�޸ļ�¼>
    ///    1.ʵ��ҩƷ�ֵ��б���Զ������������ by Sunjh 2010-9-26 {D1523F8E-E81A-4f94-BF39-CCFD16886BDE}
    ///    2.ʵ��ҩƷ�ֵ��б�Ĵ�ӡ���� by Sunjh 2010-9-26 {037BD776-60DA-4b8f-8E92-49846FDE1ABD}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucPharmacyQuery : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPharmacyQuery()
        {
            InitializeComponent();
        }

        #region ������

        /// <summary>
        /// ҩƷ���ʰ�����
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper qualityHelper = null;

        /// <summary>
        /// ҩƷ��������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper drugTypeHelper = null;

        /// <summary>
        /// ���Ͱ�����
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper dosageFormHelper = null;

        /// <summary>
        /// �۸���ʽ������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper priceFormHelper = null;

        /// <summary>
        /// ҩƷ�ȼ�������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper itemGradeHelper = null;

        /// <summary>
        /// �÷�������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper usageHelper = null;

        /// <summary>
        /// ҩ�����ð�����
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper phyFunctionHelper = null;

        /// <summary>
        /// ϵͳ��������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper sysClassHelper = null;

        /// <summary>
        /// ��������������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper storeContionHelper = null;

        /// <summary>
        /// Ƶ�ΰ�����
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper frequencyHelper = null;

        #region{383E129A-909E-48b6-BD59-2A8C55E5606E}
        /// <summary>
        /// ������˾������
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper companyHelper = null;

        /// <summary>
        /// �������Ұ�����
        /// </summary>
        private FS.FrameWork.Public.ObjectHelper producHelper = null;

        /// <summary>
        /// ������˾����������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant phaCons = new FS.HISFC.BizLogic.Pharmacy.Constant();
        #endregion

        #endregion

        #region �����

        /// <summary>
        /// ҩƷ���ݼ�
        /// </summary>
        private DataTable dt = null;

        /// <summary>
        /// ������ͼ
        /// </summary>
        private DataView dv = null;

        /// <summary>
        ///  ��ʽ�ļ��洢·��
        /// </summary>
        private string filePath = Application.StartupPath + "\\" + FS.FrameWork.WinForms.Classes.Function.SettingPath + "\\PharmacyManager.xml";

        /// <summary>
        /// �Ƿ�����޸�Ȩ��
        /// </summary>
        private bool isEditExpediency = true; 

        /// <summary>
        /// ҩƷ������
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ҩƷά������
        /// </summary>
        private System.Windows.Forms.Form MainteranceForm = null;

        /// <summary>
        /// ҩƷά���ؼ�
        /// </summary>
        private ucPharmacyManager MainteranceUC = null;

        /// <summary>
        /// ��ǰ����ҩƷʵ��
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item itemTemp = new FS.HISFC.Models.Pharmacy.Item();

        /// <summary>
        /// ����ԭʼ�ַ���
        /// </summary>
        private string filterStr = "((���� LIKE '{0}') OR (ƴ���� LIKE '{0}') OR (����� LIKE '{0}') OR (�Զ����� LIKE '{0}') OR " +
                "(ҩƷ���� LIKE '{0}') OR (ͨ����ƴ���� LIKE '{0}') OR (ͨ��������� LIKE '{0}') OR " +
                "(Ӣ����Ʒ�� LIKE '{0}') OR (ͨ���� LIKE '{0}') )";

        /// <summary>
        /// ����Ա���޸�Ȩ��ʱ �Ƿ������ҩƷ��Ϣ����/��ӡ
        /// </summary>
        private bool isExportWhenNoExpediency = true;

        /// <summary>
        /// �޲���Ȩ��ʱ�Ƿ������ѯ
        /// </summary>
        private bool isQueryWhenNoExpediency = true;

        /// <summary>
        /// �����ÿؼ�
        /// </summary>
        private FS.HISFC.Components.Common.Controls.ucSetColumn  ucSetColumn = null;

        /// <summary>
        /// ���õ���ά����ʽ
        /// </summary>
        private bool isPopSetType = true;

        /// <summary>
        /// ҩƷ��Ϣά��ʱ���ڰ�װ����������������λ��
        /// </summary>
        private int packQtyNumPrecision = 4;

        /// <summary>
        /// ҩƷ��Ϣά��ʱ���ڻ�������������������λ��
        /// </summary>
        private int baseDoseNumPrecision = 10;

        /// <summary>
        /// ҩƷ��Ϣά��ʱ���ڼ۸�������������λ��
        /// </summary>
        private int priceNumPrecision = 12;

        /// <summary>
        /// ��Ʒ���Զ�����������������λ��
        /// </summary>
        private int nameUserCodeMaxLength = 16;

        /// <summary>
        /// �����Զ�����������������λ��
        /// </summary>
        private int otherUserCodeMaxLength = 16;

        /// <summary>
        /// �Ƿ��Ƿ�����ͨ����ά�����Tab˳��
        /// </summary>
        private bool isRegularTabOrder = true;

        /// <summary>
        /// �Ƿ���Ӣ����ά�����Tab˳��
        /// </summary>
        private bool isEnglishTabOrder = false;

        /// <summary>
        /// �Ƿ��������ά�����Tab˳��
        /// </summary>
        private bool isCodeTabOrder = false;

        /// <summary>
        /// ҩƷ��������ʾ��Ϣ����  {6F6120F5-6D88-47ce-AF9C-0CF781DE412F}  ���ԭ��������
        /// </summary>
        private string itemSpeInformationSetting = "";

        /// <summary>
        /// ƴ�����Զ����ɷ�ʽ 
        /// </summary>
        private DrugAutoSpellType drugAutoSpell = DrugAutoSpellType.TradeName;

        /// <summary>
        /// �Ƿ������޸ļ�����Ϣ
        /// </summary>
        private bool isAllowAlterDose = false;

        /// <summary>
        /// �Զ���������ַ��� 0 �������� 1 ������λ 2 ��װ���� 3 ��С��λ 4 ��װ��λ
        /// </summary>
        private string autoCreateSpecs = "{0}{1}*{2}{3}/{4}";

        /// <summary>
        /// �Ƿ�������Ա��ҩ����
        /// </summary>
        private bool isUseDrugControlSet = true;

        /// <summary>
        /// ͨ��������Ʒ���Ƿ�ϲ�  {4EED03A7-7E22-4a93-9ADC-5FF007D51A92}
        /// </summary>
        private bool isMergerName = false;
       
        #endregion

        #region ����

        /// <summary>
        /// ��ʽ�ļ��洢·��
        /// </summary>
        [Description("�б��ʽ�ļ�Xml�洢·��"), Category("����"), DefaultValue(".\\Profile\\PharmacyManager.xml")]
        public string FilePath
        {
            get
            {
                return this.filePath;
            }
            set
            {
                this.filePath = value;
            }
        }

        ///// <summary>
        ///// �޲���Ȩ��ʱ�Ƿ������ӡ/����
        ///// </summary>
        //[Description("����Ա���޸�Ȩ��ʱ �Ƿ������ҩƷ��Ϣ����/��ӡ"), Category("����"), DefaultValue(true), Browsable(false)]
        //public bool IsExportWhenNoExpediency
        //{
        //    get
        //    {
        //        return this.isExportWhenNoExpediency;
        //    }
        //    set
        //    {
        //        this.isExportWhenNoExpediency = value;
        //    }
        //}

        ///// <summary>
        ///// ����Ա���޸�Ȩ��ʱ �Ƿ������ѯ
        ///// </summary>
        //[Description("����Ա���޸�Ȩ��ʱ �Ƿ������ҩƷ��Ϣ��ѯ���"), Category("����"), DefaultValue(true), Browsable(false)]
        //public bool IsQueryWhenExpediency
        //{
        //    get
        //    {
        //        return this.isQueryWhenNoExpediency;
        //    }
        //    set
        //    {
        //        this.isQueryWhenNoExpediency = value;
        //        this.Enabled = value;
        //    }
        //}

        /// <summary>
        /// �Ƿ����ҩƷ��Ϣά��Ȩ��
        /// </summary>
        public bool IsEditExpediency
        {
            set
            {
                this.isEditExpediency = value;

                if (!value)
                {
                    //�������¿��Ʋ���
                    //if (!this.isQueryWhenNoExpediency)
                    //{
                    //    this.Enabled = false;
                    //}

                    this.IsAddBarEnabled = false;
                    this.IsModifyBarEnabled = false;
                    this.IsDelBarEnabled = false;
                    this.IsCopyBarEnabled = false;
                    this.IsSaveBarEnabled = false;

                    //�������¿��Ʋ���
                    //if (this.isExportWhenNoExpediency)
                    //{
                    //    this.IsExportBarEnabled = true;
                    //}
                    //else
                    //{
                    //    this.IsExportBarEnabled = false;
                    //}
                }
            }
        }

        /// <summary>
        /// ���Ӱ�ť�Ƿ����
        /// </summary>
        [Description("���Ӱ�ť�Ƿ����"), Category("��ť����"), DefaultValue(true),Browsable(false)]
        public bool IsAddBarEnabled
        {
            get
            {
                if (this.toolBarService.GetToolButton("����") != null)
                    return this.toolBarService.GetToolButton("����").Enabled;
                else
                    return false;
            }
            set
            {
                this.toolBarService.SetToolButtonEnabled("����", value);
            }
        }

        /// <summary>
        /// �޸İ�ť�Ƿ����
        /// </summary>
        [Description("�޸İ�ť�Ƿ����"), Category("��ť����"), DefaultValue(true), Browsable(false)]
        public bool IsModifyBarEnabled
        {
            get
            {
                if (this.toolBarService.GetToolButton("�޸�") != null)
                    return this.toolBarService.GetToolButton("�޸�").Enabled;
                else
                    return false;
            }
            set
            {
                this.toolBarService.SetToolButtonEnabled("�޸�", value);
            }
        }

        /// <summary>
        /// ɾ����ť�Ƿ����
        /// </summary>
        [Description("ɾ����ť�Ƿ����"), Category("��ť����"), DefaultValue(true), Browsable(false)]
        public bool IsDelBarEnabled
        {
            get
            {
                if (this.toolBarService.GetToolButton("ɾ��") != null)
                    return this.toolBarService.GetToolButton("ɾ��").Enabled;
                else
                    return false;
            }
            set
            {
                this.toolBarService.SetToolButtonEnabled("ɾ��", value);
            }
        }

        /// <summary>
        /// ���ư�ť�Ƿ����
        /// </summary>
        [Description("���ư�ť�Ƿ����"), Category("��ť����"), DefaultValue(true), Browsable(false)]
        public bool IsCopyBarEnabled
        {
            get
            {
                if (this.toolBarService.GetToolButton("����") != null)
                    return this.toolBarService.GetToolButton("����").Enabled;
                else
                    return false;
            }
            set
            {
                this.toolBarService.SetToolButtonEnabled("����", value);
            }
        }

        /// <summary>
        /// ������ť�Ƿ����
        /// </summary>
        [Description("������ť�Ƿ����"), Category("��ť����"), DefaultValue(true), Browsable(false)]
        public bool IsExportBarEnabled
        {
            get
            {
                if (this.toolBarService.GetToolButton("����") != null)
                    return this.toolBarService.GetToolButton("����").Enabled;
                else
                    return false;
            }
            set
            {
                this.toolBarService.SetToolButtonEnabled("����", value);
            }
        }

        /// <summary>
        /// ��ӡ��ť�Ƿ����
        /// </summary>
        [Description("��ӡ��ť�Ƿ����"), Category("��ť����"), DefaultValue(true), Browsable(false)]
        public bool IsPrintBarEnabled
        {
            get
            {
                if (this.toolBarService.GetToolButton("��ӡ") != null)
                    return this.toolBarService.GetToolButton("��ӡ").Enabled;
                else
                    return false;
            }
            set
            {
                this.toolBarService.SetToolButtonEnabled("��ӡ", value);
            }
        }

        /// <summary>
        /// ���水ť�Ƿ����
        /// </summary>
        [Description("���水ť�Ƿ����"), Category("��ť����"), DefaultValue(true), Browsable(false)]
        public bool IsSaveBarEnabled
        {
            get
            {
                if (this.toolBarService.GetToolButton("����") != null)
                    return this.toolBarService.GetToolButton("����").Enabled;
                else
                    return false;
            }
            set
            {
                this.toolBarService.SetToolButtonEnabled("����", value);
            }
        }

        /// <summary>
        /// ���ð�ť�Ƿ����
        /// </summary>
        [Description("���ð�ť�Ƿ����"), Category("��ť����"), DefaultValue(true), Browsable(false)]
        public bool IsSetBarEnabled
        {
            get
            {
                if (this.toolBarService.GetToolButton("����") != null)
                    return this.toolBarService.GetToolButton("����").Enabled;
                else
                    return false;
            }
            set
            {
                this.toolBarService.SetToolButtonEnabled("����", value);
            }
        }

        /// <summary>
        /// ҩƷ��Ϣά��ʱ���ڰ�װ����������������λ��
        /// </summary>
        [Description("ҩƷ��Ϣά��ʱ���ڰ�װ����������������λ��"), Category("��Ч�Լ���"), DefaultValue(4), Browsable(false)]
        public int PackQtyNumPrecision
        {
            get
            {
                return this.packQtyNumPrecision;
            }
            set
            {
                this.packQtyNumPrecision = value;
            }
        }

        /// <summary>
        /// ҩƷ��Ϣά��ʱ���ڻ�������������������λ��
        /// </summary>
        [Description("ҩƷ��Ϣά��ʱ���ڻ�������������������λ��"), Category("��Ч�Լ���"), DefaultValue(10), Browsable(false)]
        public int BaseDoseNumPrecision
        {
            get
            {
                return this.baseDoseNumPrecision;
            }
            set
            {
                this.baseDoseNumPrecision = value;
            }
        }

        /// <summary>
        /// ҩƷ��Ϣά��ʱ���ڼ۸�������������λ��
        /// </summary>
        [Description("ҩƷ��Ϣά��ʱ���ڼ۸�������������λ��"), Category("��Ч�Լ���"), DefaultValue(12), Browsable(false)]
        public int PriceNumPrecision
        {
            get
            {
                return this.priceNumPrecision;
            }
            set
            {
                this.priceNumPrecision = value;
            }
        }

        /// <summary>
        /// ��Ʒ���Զ�����������������λ��
        /// </summary>
        [Description("��Ʒ���Զ�����������������λ��"), Category("��Ч�Լ���"), DefaultValue(16), Browsable(false)]
        public int NameUserCodeMaxLength
        {
            get
            {
                return this.nameUserCodeMaxLength;
            }
            set
            {
                this.nameUserCodeMaxLength = value;
            }
        }

        /// <summary>
        /// �����Զ�����������������λ��
        /// </summary>
        [Description("�����Զ�����������������λ��"), Category("��Ч�Լ���"), DefaultValue(16), Browsable(false)]
        public int OtherUserCodeMaxLength
        {
            get
            {
                return this.otherUserCodeMaxLength;
            }
            set
            {
                this.otherUserCodeMaxLength = value;
            }
        }

        /// <summary>
        /// �Ƿ�����ͨ����ά�����Tab˳�� 
        /// </summary>
        [Description("�Ƿ�����ͨ����ά�����Tab˳��"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool IsRegularTabOrder
        {
            get
            {
                return this.isRegularTabOrder;
            }
            set
            {
                this.isRegularTabOrder = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ӣ����ά�����Tab˳��
        /// </summary>
        [Description("�Ƿ�����Ӣ����ά�����Tab˳��"), Category("����"), DefaultValue(false), Browsable(false)]
        public bool IsEnglishTabOrder
        {
            get
            {
                return this.isEnglishTabOrder;
            }
            set
            {
                this.isEnglishTabOrder = value;
            }
        }

        /// <summary>
        /// �Ƿ��������ά�����Tab˳��
        /// </summary>
        [Description("�Ƿ��������ά�����Tab˳��"), Category("����"), DefaultValue(false), Browsable(false)]
        public bool IsCodeTabOrder
        {
            get
            {
                return this.isCodeTabOrder;
            }
            set
            {
                this.isCodeTabOrder = value;
            }
        }

        /// <summary>
        /// ƴ���롢������Զ����ɷ�ʽ
        /// </summary>
        [Description("ƴ���롢������Զ����ɷ�ʽ"), Category("����"), DefaultValue(true)]
        public DrugAutoSpellType DrugAutoSpell
        {
            get
            {
                return this.drugAutoSpell;
            }
            set
            {
                this.drugAutoSpell = value;
            }
        }

        /// <summary>
        /// �Ƿ������޸ļ�����Ϣ
        /// </summary>
        [Description("�Ƿ������޸ļ�����Ϣ"), Category("����"), DefaultValue(false)]
        public bool AllowAlterDose
        {
            get
            {
                return this.isAllowAlterDose;
            }
            set
            {
                this.isAllowAlterDose = value;
            }
        }

        /// <summary>
        ///  ����Զ����ɸ�ʽ���ַ��� ����˳��0 �������� 1 ������λ 2 ��װ���� 3 ��С��λ 4 ��װ��λ
        /// 
        ///  {773D56E7-4828-48d4-99C8-C80428112EBC}  ����ʽ��
        /// </summary>
        [Description("����Զ����ɸ�ʽ���ַ��� ����˳��0 �������� 1 ������λ 2 ��װ���� 3 ��С��λ 4 ��װ��λ"), Category("����"), DefaultValue("{0}{1}*{2}{3}/{4}")]
        public string AutoSpecsFormat
        {
            get
            {
                return this.autoCreateSpecs;
            }
            set
            {
                this.autoCreateSpecs = value;
            }
        }

        /// <summary>
        /// �Ƿ�������Ա��ҩ����
        /// </summary>
        [Description("�Ƿ�������Ա��ҩ����"), Category("����"), DefaultValue(true), Browsable(false)]
        public bool IsUseDrugControlSet
        {
            get
            {
                return this.isUseDrugControlSet;
            }
            set
            {
                this.isUseDrugControlSet = value;
            }
        }

        /// <summary>
        /// ͨ��������Ʒ���Ƿ�ϲ�  {4EED03A7-7E22-4a93-9ADC-5FF007D51A92}
        /// </summary>
        public bool IsMergerName
        {
            get
            {
                return isMergerName;
            }
            set
            {
                this.isMergerName = value;
            }
        }
        #endregion

        #region ���ݳ�ʼ��

        /// <summary>
        /// ���Ʋ�����ʼ��
        /// </summary>
        private void InitControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            //{6F6120F5-6D88-47ce-AF9C-0CF781DE412F}  ���ԭ��������
            this.itemSpeInformationSetting = ctrlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Set_Item_SpecialFlag, true, "");
            
            //�������������Ʋ���
            //this.IsQueryWhenExpediency = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Query_No_EditPriv, true, true);
            //this.IsExportWhenNoExpediency = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Export_No_EditPriv, true, true);
            
            this.PackQtyNumPrecision = ctrlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Max_PackQty_Digit, true, 4);
            this.BaseDoseNumPrecision = ctrlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Max_BaseDose_Digit, true, 10);
            this.PriceNumPrecision = ctrlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Max_Price_Digit, true, 12);
            this.NameUserCodeMaxLength = ctrlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Max_NameCustomeCode_Digit, true, 16);
            this.OtherUserCodeMaxLength = ctrlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Max_CustomeCode_Digit, true, 16);

            this.IsRegularTabOrder = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Have_Regular_Tab, true, false);
            this.IsEnglishTabOrder = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Have_English_Tab, true, false);
            this.IsCodeTabOrder = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Have_Code_Tab, true, false);
        }

        /// <summary>
        /// ��������
        /// </summary>
        private void InitData()
        {
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            FS.HISFC.BizLogic.Pharmacy.Constant itemConstantManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.BizLogic.Manager.Frequency frequencyManager = new FS.HISFC.BizLogic.Manager.Frequency();

            if (this.qualityHelper == null)
            {
                ArrayList alQuality = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY);
                this.qualityHelper = new FS.FrameWork.Public.ObjectHelper(alQuality);
            }
            if (this.drugTypeHelper == null)
            {
                ArrayList alItemType = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
                this.drugTypeHelper = new FS.FrameWork.Public.ObjectHelper(alItemType);
            }
            if (this.dosageFormHelper == null)
            {
                ArrayList alDosageForm = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM);
                this.dosageFormHelper = new FS.FrameWork.Public.ObjectHelper(alDosageForm);
            }
            if (this.priceFormHelper == null)
            {
                ArrayList alPriceForm = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.PRICEFORM);
                this.priceFormHelper = new FS.FrameWork.Public.ObjectHelper(alPriceForm);
            }
            if (this.usageHelper == null)
            {
                ArrayList alUsage = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);
                this.usageHelper = new FS.FrameWork.Public.ObjectHelper(alUsage);
            }
            if (this.itemGradeHelper == null)
            {
                ArrayList alItemGrade = consManager.GetList("DRUGGRADE");
                this.itemGradeHelper = new FS.FrameWork.Public.ObjectHelper(alItemGrade);
            }
            if (this.phyFunctionHelper == null)
            {
                ArrayList alPhy = itemConstantManager.QueryPhaFunction();
                this.phyFunctionHelper = new FS.FrameWork.Public.ObjectHelper(alPhy);
            }
            if (this.storeContionHelper == null)
            {
                ArrayList alStore = consManager.GetList("STORECONDITION");
                this.storeContionHelper = new FS.FrameWork.Public.ObjectHelper(alStore);
            }
            if (this.sysClassHelper == null)
            {
                this.sysClassHelper = new FS.FrameWork.Public.ObjectHelper(FS.HISFC.Models.Base.SysClassEnumService.List());
            }
            if (this.frequencyHelper == null)
            {
                this.frequencyHelper = new FS.FrameWork.Public.ObjectHelper(frequencyManager.GetList("ROOT"));
            }
            #region{383E129A-909E-48b6-BD59-2A8C55E5606E}
            if (this.producHelper == null)
            {
                this.producHelper = new FS.FrameWork.Public.ObjectHelper(phaCons.QueryCompany("0"));
            }
            if (this.companyHelper == null)
            {
                this.companyHelper = new FS.FrameWork.Public.ObjectHelper(phaCons.QueryCompany("1"));
            }
            #endregion
        }

        /// <summary>
        /// ��ʼ��ҩƷ�б�
        /// </summary>
        private void InitDrug()
        {
            this.SetToolBarEnableForSetType(false);

            this.isPopSetType = true;

            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].ResetCellType();
            }

            this.neuSpread1_Sheet1.DataAutoCellTypes = true;

            List<FS.HISFC.Models.Pharmacy.Item> al = new List<FS.HISFC.Models.Pharmacy.Item>();
            al = this.itemManager.QueryItemList();
            if (al == null)
            {
                MessageBox.Show("��ȡҩƷ�б�������" + this.itemManager.Err, "������ʾ");
                return;
            }

            List<FS.HISFC.Models.Pharmacy.Item> filterItemList = this.FilterPrivDrug(al);

            this.SetDataSet(filterItemList);

            FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, this.filePath);
			
        }

        /// <summary>
        /// ���ݲ���Ա��ҩȨ�� ����ҩƷ������
        /// </summary>
        /// <param name="alList"></param>
        private List<FS.HISFC.Models.Pharmacy.Item> FilterPrivDrug(List<FS.HISFC.Models.Pharmacy.Item> alList)
        {
            FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            List<FS.HISFC.Models.Pharmacy.DrugConstant> drugConstantList = phaConsManager.QueryDrugConstant(Function.DrugTypePriv_ConsType, phaConsManager.Operator.ID);

            if (drugConstantList != null && drugConstantList.Count > 0)
            {
                System.Collections.Hashtable hsTypePriv = new Hashtable();
                foreach (FS.HISFC.Models.Pharmacy.DrugConstant info in drugConstantList)
                {
                    hsTypePriv.Add(info.DrugType, null);
                }

                List<FS.HISFC.Models.Pharmacy.Item> filterList = new List<FS.HISFC.Models.Pharmacy.Item>();
                foreach (FS.HISFC.Models.Pharmacy.Item item in alList)
                {
                    if (hsTypePriv.ContainsKey(item.Type.ID))
                    {
                        filterList.Add(item.Clone());
                    }
                }

                return filterList;
            }
            else
            {
                return alList;
            }
        }

        /// <summary>
        /// Ȩ�޳�ʼ������
        /// </summary>
        private void InitExpediency()
        {
            //�жϲ���Ա�Ƿ����޸�Ȩ��
            this.IsEditExpediency = FS.HISFC.BizProcess.Integrate.Pharmacy.ChoosePiv("0301");
        }

        /// <summary>
        /// �����б��ʼ��
        /// </summary>
        protected virtual void InitTreeView()
        {
            this.tvType.ImageList = this.tvType.groupImageList;

            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alDrugType = consManager.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE);
            if (alDrugType == null)
            {
                MessageBox.Show("��ȡҩƷ����б�������" + consManager.Err);
                return;
            }

            TreeNode root = new TreeNode("ȫ��ҩƷ��Ϣ", 0, 0);
            root.Tag = "1=1";

            this.tvType.Nodes.Add(root);

            foreach (FS.FrameWork.Models.NeuObject objType in alDrugType)
            {
                //{3AB5D24A-E70A-4760-8EA6-617834C3B7E0}  ��ȡ�����Ƿ�������Ա��ҩ����
                if (this.IsUseDrugControlSet == true)
                {
                    FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
                    List<FS.HISFC.Models.Pharmacy.DrugConstant> drugConstantList = phaConsManager.QueryDrugConstant(Function.DrugTypePriv_ConsType, phaConsManager.Operator.ID);
                    if (drugConstantList != null && drugConstantList.Count > 0)
                    {
                        System.Collections.Hashtable hsTypePriv = new Hashtable();
                        foreach (FS.HISFC.Models.Pharmacy.DrugConstant info in drugConstantList)
                        {
                            hsTypePriv.Add(info.DrugType, null);
                        }
                        if (hsTypePriv.ContainsKey(objType.ID))
                        {
                            TreeNode type = new TreeNode(objType.Name, 2, 4);
                            type.Tag = "ҩƷ���� = '" + objType.Name.ToString() + "'";
                            root.Nodes.Add(type);
                        }
                    }
                    else
                    {
                        TreeNode type = new TreeNode(objType.Name, 2, 4);
                        type.Tag = "ҩƷ���� = '" + objType.Name.ToString() + "'";
                        root.Nodes.Add(type);
                    }
                }
                else
                {
                    TreeNode type = new TreeNode(objType.Name, 2, 4);
                    type.Tag = "ҩƷ���� = '" + objType.Name.ToString() + "'";
                    root.Nodes.Add(type);
                }
            }

            root.Expand();
        }

        #region ��ʼ����ť

        protected FS.FrameWork.WinForms.Forms.ToolBarService toolBarService = new FS.FrameWork.WinForms.Forms.ToolBarService();
        protected override ToolBarService OnInit(object sender,object neuObject, object param)
        {
            //FS.FrameWork.WinForms.Classes.Function.GetImage(FS.FrameWork.WinForms.Classes.EnumImageList.T���);
            toolBarService.AddToolButton("����", "����ҩƷ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.T���, true, false, null);
            toolBarService.AddToolButton("�޸�", "�޸ĵ�ǰҩƷ��Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X�޸�, true, false, null);
            toolBarService.AddToolButton("ɾ��", "ɾ����ǰҩƷ��Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Sɾ��, true, false, null);
            toolBarService.AddToolButton("����", "���Ƶ�ǰ����ҩƷ��Ϣ ������ҩ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.F����, true, false, null);
            toolBarService.AddToolButton("����ά��", "���õ����б�ʽά��ҩƷ��Ϣ", (int)FS.FrameWork.WinForms.Classes.EnumImageList.X��Ϣ, true, false, null);
            toolBarService.AddToolButton("ֱ��ά��", "������Ҫֱ��ά������", (int)FS.FrameWork.WinForms.Classes.EnumImageList.Zע��, true, false, null);

            return this.toolBarService;
        }
        public override void ToolStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "����":
                    this.Add();
                    break;
                case "�޸�":
                    this.Modify();
                    break;
                case "ɾ��":
                    this.Delete();
                    break;
                case "����":
                    this.Copy();
                    break;
                case "����ά��":
                    FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("����Ӧ��ά����ʽ��� ����Ϊ����ά����ʽ ���Ժ�..");
                    Application.DoEvents();
                    this.InitDrug();                    
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                    break;
                case "ֱ��ά��":
                    this.DirectSaveColSet();                    
                    break;
            }
            base.ToolStrip_ItemClicked(sender, e);
        }

        protected override int OnSave(object sender, object neuObject)
        {
            return this.DirectSave();
        }

        public override int SetPrint(object sender, object neuObject)
        {
            this.SetColumn();
            return 1;
        }

        public override int Export(object sender, object neuObject)
        {
            this.Export();
            return 1;
        }

        protected override int OnPrint(object sender, object neuObject)
        {
            this.Print();
            return 1;
        }

        #endregion

        #endregion

        #region ����DataTable����

        /// <summary>
        /// ������������е����ݱ�����dt��
        /// </summary>
        /// <param name="al">ҩƷ�ֵ�����</param>
        public int SetDataSet(List<FS.HISFC.Models.Pharmacy.Item> al)
        {
            this.dt = new DataTable();

            //��XML�ж�ȡ��˳��,���ȵ�����
            this.SetDataTable(this.dt);

            DataRow newRow;
            foreach (FS.HISFC.Models.Pharmacy.Item myItem in al)
            {
                if (myItem == null)
                    continue;
                newRow = this.dt.NewRow();
                this.SetRow(newRow, myItem);
                this.dt.Rows.Add(newRow);
            }   

            this.dt.AcceptChanges();

            this.dv = this.dt.DefaultView;
            this.dv.AllowNew = true;
            this.Filter();
            this.neuSpread1_Sheet1.DataSource = this.dv;

            return 1;
        }

        /// <summary>
        /// ���ݱ���Xml�����ļ�����DataTable��ʽ��ʾ �粻���������ļ� �����Ĭ������
        /// </summary>
        /// <param name="dt">�����õ�DataTable</param>
        protected virtual void SetDataTable(DataTable table)
        {
            if (System.IO.File.Exists(this.filePath))
            {
                #region ��Xml�����ļ��ڶ�ȡ������
                XmlDocument doc = new XmlDocument();
                try
                {
                    System.IO.StreamReader sr = new System.IO.StreamReader(this.filePath, System.Text.Encoding.Default);
                    string streamXml = sr.ReadToEnd();
                    sr.Close();
                    doc.LoadXml(streamXml);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Language.Msg("��ȡXml�����ļ��������� ���������ļ��Ƿ���ȷ") + ex.Message);
                    return; 
                }

                XmlNodeList nodes = doc.SelectNodes("//Column");

                string tempString = "";

                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes["type"].Value == "TextCellType" || node.Attributes["type"].Value == "ComboBoxCellType")
                    {
                        tempString = "System.String";
                    }
                    else if (node.Attributes["type"].Value == "CheckBoxCellType")
                    {
                        tempString = "System.Boolean";
                    }

                    table.Columns.Add(new DataColumn(node.Attributes["displayname"].Value,
                        System.Type.GetType(tempString)));
                }

                #endregion
            }
            else
            {
                #region ����Ĭ��DataTable���� ��ʾ 

                //��������
                System.Type dtStr = System.Type.GetType("System.String");
                System.Type dtDec = System.Type.GetType("System.Decimal");
                System.Type dtDTime = System.Type.GetType("System.DateTime");
                System.Type dtBool = System.Type.GetType("System.Boolean");
                table.Columns.AddRange(new DataColumn[]{new DataColumn("����", dtStr),
															new DataColumn("ƴ����", dtStr),
															new DataColumn("�Զ�����", dtStr),   
															new DataColumn("ҩƷ����", dtStr),  
															new DataColumn("���", dtStr),                                     
															new DataColumn("���ۼ�", dtStr),     
															new DataColumn("��װ��λ", dtStr),   
															new DataColumn("��װ����", dtStr),   
															new DataColumn("��С��λ", dtStr),   
															new DataColumn("��������", dtStr),   
															new DataColumn("������λ", dtStr),   
                                                            new DataColumn("�������", dtStr),
                                                            new DataColumn("�������", dtStr),
															new DataColumn("ҩƷ����", dtStr),   
															new DataColumn("ҩƷ����", dtStr),   
                                                            new DataColumn("ϵͳ���", dtStr),
															new DataColumn("����", dtStr),       
															new DataColumn("�۸���ʽ", dtStr),   
															new DataColumn("ҩƷ�ȼ�", dtStr),   
															new DataColumn("������", dtStr),     
															new DataColumn("�����", dtStr),     
															new DataColumn("������ۼ�", dtStr), 
															new DataColumn("ͣ��", dtBool),       
															new DataColumn("����", dtBool),       
															new DataColumn("����", dtBool),       
															new DataColumn("GMP", dtBool),        
															new DataColumn("OTC", dtBool),        
															new DataColumn("��ʾ", dtBool),       
															new DataColumn("ʹ�÷���", dtStr),   
															new DataColumn("һ������", dtStr),   
															new DataColumn("Ƶ��", dtStr),       
															new DataColumn("ע������", dtStr),   
															new DataColumn("��Ч�ɷ�", dtStr),   
															new DataColumn("��������", dtStr),   
															new DataColumn("ִ�б�׼", dtStr),   
															new DataColumn("һ��ҩ������", dtStr),  
															new DataColumn("����ҩ������", dtStr), 
															new DataColumn("����ҩ������", dtStr), 
															new DataColumn("��������", dtStr),   
															new DataColumn("������Ϣ", dtStr),   
															new DataColumn("ע���̱�", dtStr),   
															new DataColumn("����", dtStr),       
															new DataColumn("������˾", dtStr),   
															new DataColumn("������", dtStr),     
															new DataColumn("ѧ��", dtStr),       
															new DataColumn("����", dtStr),       
															new DataColumn("Ӣ����Ʒ��", dtStr),   
															new DataColumn("Ӣ�ı���", dtStr),   
															new DataColumn("Ӣ��ͨ����", dtStr), 
															new DataColumn("�б�ҩ", dtStr),     
															new DataColumn("��ע", dtStr),     
															new DataColumn("�����", dtStr),     															  
															new DataColumn("ͨ����", dtStr),     
															new DataColumn("ͨ����ƴ����", dtStr),     
															new DataColumn("ͨ���������", dtStr),
                                                            new DataColumn("ѧ��ƴ����",    dtStr),
                                                            new DataColumn("ѧ�������",    dtStr),
                                                            new DataColumn("����ƴ����",    dtStr),
                                                            new DataColumn("���������",    dtStr),
                                                            new DataColumn("ѧ���Զ�����",  dtStr),
                                                            new DataColumn("�����Զ�����",  dtStr)
														});

                this.neuSpread1_Sheet1.DataSource = table;
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);

                #endregion
            }

            DataColumn[] keys = new DataColumn[1];
            keys[0] = table.Columns["����"];
            table.PrimaryKey = keys;
        }

        /// <summary>
        /// ��DataSet�в�������
        /// </summary>
        /// <param name="row">�����в������ݵ���</param>
        /// <param name="itemManager">�������ݵ�ҩƷʵ��</param>
        protected virtual void SetRow(DataRow row, FS.HISFC.Models.Pharmacy.Item myItem)
        {
            row["����"] = myItem.ID.ToString();
            row["ƴ����"] = myItem.NameCollection.SpellCode;
            row["�Զ�����"] = myItem.NameCollection.UserCode;

            row["ҩƷ����"] = myItem.Name.ToString();
            row["ͨ����"] = myItem.NameCollection.RegularName;

            row["���"] = myItem.Specs.ToString();
            row["���ۼ�"] = myItem.PriceCollection.RetailPrice.ToString();
            row["��װ��λ"] = myItem.PackUnit.ToString();
            row["��װ����"] = myItem.PackQty.ToString();
            row["��С��λ"] = myItem.MinUnit.ToString();
            row["��������"] = myItem.BaseDose.ToString();
            row["������λ"] = myItem.DoseUnit.ToString();

            if (myItem.SplitType == "0")
            {
                row["�������"] = "��С��λ����ȡ��";
            }
            else if (myItem.SplitType == "1")
            {
                row["�������"] = "��װ��λ����ȡ��";
            }
            else if (myItem.SplitType == "2")
            {
                row["�������"] = "��С��λÿ��ȡ��";
            }
            else if (myItem.SplitType == "3")
            {
                row["�������"] = "��װ��λÿ��ȡ��";
            }
            else
            {
                row["�������"] = "";
            }

            //by cube 2011-03-28 סԺ����ҽ�������������
            if (myItem.CDSplitType == "0")
            {
                row["�������"] = "��С��λ����ȡ��";
            }
            else if (myItem.CDSplitType == "1")
            {
                row["�������"] = "��װ��λ����ȡ��";
            }
            else if (myItem.CDSplitType == "2")
            {
                row["�������"] = "��С��λÿ��ȡ��";
            }
            else if (myItem.CDSplitType == "3")
            {
                row["�������"] = "��װ��λÿ��ȡ��";
            }
            else if (myItem.CDSplitType == "4")
            {
                row["�������"] = "��С��λ�ɲ��";
            }
            else
            {
                row["�������"] = "";
            }
            //end by

            if (this.qualityHelper != null)
                row["ҩƷ����"] = this.qualityHelper.GetName(myItem.Quality.ID);
            if (this.drugTypeHelper != null)
                row["ҩƷ����"] = this.drugTypeHelper.GetName(myItem.Type.ID);
            if (this.dosageFormHelper != null)
                row["����"] = this.dosageFormHelper.GetName(myItem.DosageForm.ID);
            if (this.sysClassHelper != null)
                row["ϵͳ���"] = this.sysClassHelper.GetName(myItem.SysClass.ID.ToString());

            if (this.priceFormHelper != null)
                row["�۸���ʽ"] = this.priceFormHelper.GetName(myItem.PriceCollection.PriceForm.ID.ToString());
            if (this.itemGradeHelper != null)
                row["ҩƷ�ȼ�"] = this.itemGradeHelper.GetName(myItem.Grade.ToString());

            row["������"] = myItem.PriceCollection.WholeSalePrice.ToString();
            row["�����"] = myItem.PriceCollection.PurchasePrice.ToString();
            row["������ۼ�"] = myItem.PriceCollection.TopRetailPrice.ToString();

            if (this.usageHelper!= null)
                row["ʹ�÷���"] = this.usageHelper.GetName(myItem.Usage.ID.ToString());

            if (this.phyFunctionHelper != null)
            {
                row["һ��ҩ������"] = this.phyFunctionHelper.GetName(myItem.PhyFunction1.ID.ToString());
                row["����ҩ������"] = this.phyFunctionHelper.GetName(myItem.PhyFunction2.ID.ToString());
                row["����ҩ������"] = this.phyFunctionHelper.GetName(myItem.PhyFunction3.ID.ToString());
            }

          
            row["ͣ��"] = myItem.IsStop.ToString();
            row["����"] = myItem.Product.IsSelfMade.ToString();
            row["����"] = myItem.IsAllergy.ToString();
            row["GMP"] = myItem.IsGMP.ToString();
            row["OTC"] = myItem.IsOTC.ToString();
            row["��ʾ"] = myItem.IsShow.ToString();
            row["һ������"] = myItem.OnceDose.ToString();
            row["Ƶ��"] = myItem.Frequency.Name.ToString();//{383E129A-909E-48b6-BD59-2A8C55E5606E}
            row["ע������"] = myItem.Product.Caution.ToString();
            row["��Ч�ɷ�"] = myItem.Ingredient.ToString();
            row["��������"] = myItem.Product.StoreCondition.ToString();
            row["ִ�б�׼"] = myItem.ExecuteStandard.ToString();
            row["��������"] = this.producHelper.GetName(myItem.Product.Producer.ID.ToString());//{383E129A-909E-48b6-BD59-2A8C55E5606E}
            row["������Ϣ"] = myItem.Product.ApprovalInfo.ToString();
            row["ע���̱�"] = myItem.Product.Label.ToString();
            row["����"] = myItem.Product.ProducingArea.ToString();
            row["������˾"] = this.companyHelper.GetName(myItem.Product.Company.ID.ToString());//{383E129A-909E-48b6-BD59-2A8C55E5606E}
            row["������"] = myItem.Product.BarCode.ToString();
            row["ѧ��"] = myItem.NameCollection.FormalName.ToString();
            row["����"] = myItem.NameCollection.OtherName.ToString();
            row["Ӣ����Ʒ��"] = myItem.NameCollection.EnglishName.ToString();
            row["Ӣ�ı���"] = myItem.NameCollection.EnglishOtherName.ToString();
            row["Ӣ��ͨ����"] = myItem.NameCollection.EnglishRegularName.ToString();
            if (myItem.TenderOffer.IsTenderOffer)//{2F238CB9-DD02-44f0-A5B8-EF21F1BA6E9B}
            {
                row["�б�ҩ"] = "��";
            }
            //row["�б�ҩ"] = myItem.TenderOffer.IsTenderOffer.ToString();
            row["��ע"] = myItem.Memo.ToString();
            row["�����"] = myItem.WBCode;
    
            row["ͨ����ƴ����"] = myItem.NameCollection.RegularSpell.SpellCode;
            row["ͨ���������"] = myItem.NameCollection.RegularSpell.WBCode;

            row["ѧ��ƴ����"] = myItem.NameCollection.FormalSpell.SpellCode;
            row["ѧ�������"] = myItem.NameCollection.FormalSpell.WBCode;
            row["����ƴ����"] = myItem.NameCollection.OtherSpell.SpellCode;
            row["���������"] = myItem.NameCollection.OtherSpell.WBCode;

            row["ѧ���Զ�����"] = myItem.NameCollection.FormalSpell.UserCode;
            row["�����Զ�����"] = myItem.NameCollection.OtherSpell.UserCode;
        }

        #endregion

        #region ҩƷά����������

        /// <summary>
        /// ҩƷά���������� ��̳���UFC.Pharmacy.Base.ucPharmacyManager
        /// </summary>
        public FS.HISFC.Components.Pharmacy.Base.ucPharmacyManager MaintenancePopUC
        {
            set
            {
                if (value != null && value as HISFC.Components.Pharmacy.Base.ucPharmacyManager == null)
                {
                    System.Windows.Forms.MessageBox.Show("��ά���ؼ���̳���UFC.Pharmacy.Base.ucPharmacyManager");
                }
                else
                {
                    this.MainteranceUC = value as HISFC.Components.Pharmacy.Base.ucPharmacyManager;

                    this.MainteranceUC.EndSave -= new ucPharmacyManager.SaveItemHandler(MainteranceUC_EndSave);
                    this.MainteranceUC.EndSave += new ucPharmacyManager.SaveItemHandler(MainteranceUC_EndSave);
                }
            }
        }

        /// <summary>
        /// ����ҩƷά������
        /// </summary>
        private void InitMaintenanceForm()
        {
            if (this.MainteranceUC == null)
            {
                this.MainteranceUC = new ucPharmacyManager();
                this.MainteranceUC.EndSave -= new ucPharmacyManager.SaveItemHandler(MainteranceUC_EndSave);
                this.MainteranceUC.EndSave += new ucPharmacyManager.SaveItemHandler(MainteranceUC_EndSave);
            }
            if (this.MainteranceForm == null)
            {
                this.MainteranceForm = new Form();
                this.MainteranceForm.Width = this.MainteranceUC.Width + 10;
                this.MainteranceForm.Height = this.MainteranceUC.Height + 25;
                this.MainteranceForm.Text = "ҩƷ��ϸ��Ϣά��";
                this.MainteranceForm.StartPosition = FormStartPosition.CenterScreen;
                this.MainteranceForm.ShowInTaskbar = false;
                this.MainteranceForm.HelpButton = false;
                this.MainteranceForm.MaximizeBox = false;
                this.MainteranceForm.MinimizeBox = false;
                this.MainteranceForm.FormBorderStyle = FormBorderStyle.FixedDialog;                
            }
           

            this.MainteranceUC.Dock = DockStyle.Fill;
            this.MainteranceForm.Controls.Add(this.MainteranceUC);
        }

        /// <summary>
        /// ҩƷά��������ʾ
        /// </summary>
        private void ShowMaintenanceForm(string inputType,FS.HISFC.Models.Pharmacy.Item item,bool isShow)
        {
            if (this.MainteranceForm == null || this.MainteranceUC == null)
                this.InitMaintenanceForm();

            this.MainteranceUC.PackQtyNumPrecision = this.packQtyNumPrecision;
            this.MainteranceUC.BaseDoseNumPrecision = this.baseDoseNumPrecision;
            this.MainteranceUC.PriceNumPrecision = this.priceNumPrecision;
            this.MainteranceUC.NameUserCodeMaxLength = this.nameUserCodeMaxLength;
            this.MainteranceUC.OtherUserCodeMaxLength = this.otherUserCodeMaxLength;
            //{6F6120F5-6D88-47ce-AF9C-0CF781DE412F}  ���ԭ��������
            this.MainteranceUC.ItemSpeInformationSetting = this.itemSpeInformationSetting;
            this.MainteranceUC.IsRegularTabOrder = this.isRegularTabOrder;
            this.MainteranceUC.IsEnglishTabOrder = this.isEnglishTabOrder;
            this.MainteranceUC.IsCodeTabOrder = this.isCodeTabOrder;
            this.MainteranceUC.InputType = inputType;
            this.MainteranceUC.AllowAlterDose = this.AllowAlterDose;

            //{773D56E7-4828-48d4-99C8-C80428112EBC}  ����ʽ��
            this.MainteranceUC.AutoSpecsFormat = this.autoCreateSpecs;

            this.MainteranceUC.Item = item;
            this.MainteranceUC.ReadOnly = !this.isEditExpediency;
            this.MainteranceUC.DrugAutoSpell = this.drugAutoSpell;

             //{4EED03A7-7E22-4a93-9ADC-5FF007D51A92} ҩƷ���ƺ���Ʒ���ϲ�
            this.MainteranceUC.IsMergerName = this.isMergerName;
            

            if (isShow)
            {
                this.MainteranceForm.ShowDialog();
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// �����ǰ������ʾ
        /// </summary>
        protected virtual void Clear()
        {
            this.neuSpread1.Reset();
        }

        /// <summary>
        /// �ؼ���������ʾһ������
        /// </summary>
        /// <param name="item">������ҩƷ��Ϣʵ��</param>
        public void AddNewRow(FS.HISFC.Models.Pharmacy.Item item)
        {
            if (this.MainteranceUC != null && this.MainteranceUC.InputType == "UPDATE")
                return;

            DataRow findRow;
            findRow = this.dt.Rows.Find(item.ID.ToString());
            if (findRow == null)
            {
                DataRow newRow = this.dt.NewRow();
                this.SetRow(newRow, item);
                this.dt.Rows.Add(newRow);
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public void Add()
        {
            if (this.neuSpread1_Sheet1.Rows.Count < 0)
                return;

            this.ShowMaintenanceForm("Insert", null, true);
        }

        /// <summary>
        /// �޸�����
        /// </summary>
        public void Modify()
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0)
                return;

            if (!this.isPopSetType)
                return;

            this.itemTemp = this.itemManager.GetItem(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.dt.Columns.IndexOf("����")].Value.ToString());

            this.ShowMaintenanceForm("Update", this.itemTemp, true);
        }

        /// <summary>
        /// ��������
        /// </summary>
        public void Copy()
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0)
                return;

            itemTemp = this.itemManager.GetItem(this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.dt.Columns.IndexOf("����")].Value.ToString());

            itemTemp.ID = "";
            itemTemp.IsStop = true;
            itemTemp.ShiftMark = "����ҩƷ�Զ�ͣ��";

            this.ShowMaintenanceForm("Insert", this.itemTemp, true);
           
        }

        /// <summary>
        /// ɾ������
        /// </summary>
        /// <returns>�ɹ�ɾ������1 ʧ�ܷ���-1 �޲�������0</returns>
        public int Delete()
        {
            if (this.neuSpread1_Sheet1.Rows.Count == 0)
                return 0;

            string drugNO = this.neuSpread1_Sheet1.Cells[this.neuSpread1_Sheet1.ActiveRowIndex, this.dt.Columns.IndexOf("����")].Value.ToString();

            #region ɾ���ж�  ȷ��

            //ȡҩƷ�ڿ����е�����,�������0,������ɾ��
            int count = this.itemManager.GetDrugStorageRowNum(drugNO);
            if (count > 0)
            {
                MessageBox.Show("��ҩƷ�ڿ�����Ѵ���,������ɾ��!", "ɾ����ʾ");
                return -1;
            }

            //ѯ���û��Ƿ�ȷ��ɾ��
            System.Windows.Forms.DialogResult dr;
            dr = MessageBox.Show("���Ƿ�Ҫɾ������ҩƷ?", "��ʾ!", System.Windows.Forms.MessageBoxButtons.YesNo);
            if (dr == DialogResult.No)
            {
                return 0;
            }

            #endregion

            #region ���ݿ�ɾ������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            //ɾ��ҩƷ
            if (this.itemManager.DeleteItem(drugNO) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show("ִ��ҩƷɾ������ʧ��" + this.itemManager.Err);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show("ɾ���ɹ���");

            #endregion

            #region �����¼

            FS.HISFC.BizProcess.Integrate.Function funIntegrate = new FS.HISFC.BizProcess.Integrate.Function();

            funIntegrate.SaveChange<FS.HISFC.Models.Pharmacy.Item>(false, true,drugNO, null, null);

            #endregion

            #region ����DataTable��ʾ ��DataTable��ɾ����

            DataRow findRow;
            Object[] obj = new object[1];
            obj[0] = drugNO.ToString();
            findRow = dt.Rows.Find(obj);
            if (findRow != null)
            {
                this.dt.Rows.Remove(findRow);
            }

            #endregion

            return 1;
        }

        // <summary>
        /// ����
        /// </summary>
        protected virtual void Filter()
        {
            this.Filter(false);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="ignoreFilterCondition">�Ƿ���Ե�ǰ�Ĺ�������</param>
        protected virtual void Filter(bool ignoreFilterCondition)
        {
            string filterInput = "1=1";
            string filterValid = "1=1";
            string filterTree = "1=1";

            if (!ignoreFilterCondition)
            {

                #region ���������

                string queryCode = this.txtInputCode.Text;

                queryCode = FS.FrameWork.Public.String.TakeOffSpecialChar(queryCode);

                if (this.chbMistyFilter.Checked)
                {
                    queryCode = "%" + queryCode + "%";
                }
                else
                {
                    queryCode = queryCode + "%";
                }

                //���ù�������
                filterInput = string.Format(this.filterStr, queryCode);

                #endregion

                #region ״̬����

                //���ù�������
                switch (this.cmbValid.Text)
                {
                    case "ȫ��":
                        filterValid = "1=1";
                        break;
                    case "����":
                        filterValid = "ͣ�� = 'False'";
                        break;
                    case "ͣ��":
                        filterValid = "ͣ�� = 'True'";
                        break;
                    default:
                        filterValid = "1=1";
                        break;
                }

                #endregion

                #region ҩƷ������
                if (this.tvType.SelectedNode != null)
                    filterTree = this.tvType.SelectedNode.Tag.ToString();
                #endregion
            }

            //��Ϲ�������
            string filter = filterTree + " AND " + filterInput + " AND " + filterValid;
           
            this.dv.RowFilter = filter;
                    
            this.neuSpread1_Sheet1.RowCount =this.dv.Count ;
        }

        #endregion

        /// <summary>
        /// ��������
        /// </summary>
        protected new void Focus()
        {
            this.txtInputCode.Focus();
        }

        /// <summary>
        /// ����
        /// </summary>
        public void Export()
        {
            if (this.neuSpread1.Export() == 1)
            {
                MessageBox.Show(Language.Msg("�����ɹ�"));
            }
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public void Print()
        {
            //ʵ��ҩƷ�ֵ��б�Ĵ�ӡ���� by Sunjh 2010-9-26 {037BD776-60DA-4b8f-8E92-49846FDE1ABD}
            FS.FrameWork.WinForms.Classes.Print pp = new FS.FrameWork.WinForms.Classes.Print();
            pp.PrintPreview(5, 5, this.neuPanel2);
        }

        /// <summary>
        /// ����ʾ�н�����Ϣ����
        /// </summary>
        private void SetColumn()
        {
            if (this.ucSetColumn == null)
            {
                this.ucSetColumn = new FS.HISFC.Components.Common.Controls.ucSetColumn();
                this.ucSetColumn.DisplayEvent += new EventHandler(ucSetColumn_DisplayEvent);
            }

            this.isPopSetType = true;
            this.ucSetColumn.SetDataTable(this.filePath, this.neuSpread1_Sheet1);
            this.ucSetColumn.IsShowUpDonw = true;
            this.ucSetColumn.SetColVisible(true, true, true, false);

            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "��ʾ����";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucSetColumn);
        }

        /// <summary>
        /// ���ݲ�ͬά����ʽ���ð�ť��ʾ
        /// </summary>
        /// <param name="isDirectSet">�Ƿ�ֱ��ά��</param>
        private void SetToolBarEnableForSetType(bool isDirectSet)
        {
            this.IsAddBarEnabled = !isDirectSet;
            this.IsModifyBarEnabled = !isDirectSet;
            this.IsDelBarEnabled = !isDirectSet;
            this.IsCopyBarEnabled = !isDirectSet;
            this.IsSaveBarEnabled = isDirectSet;
            this.IsSetBarEnabled = !isDirectSet;

            if (isDirectSet)
            {
                base.OnStatusBarInfo(null, "��ǰά����ʽΪֱ��ά��������������ҩƷ");
            }
            else
            {
                base.OnStatusBarInfo(null, "��ǰά����ʽΪ����ά������������ҩƷ");
            }
        }

        #region ҩƷ����ֱ��ά������

        /// <summary>
        /// ������Ҫֱ��ά������
        /// </summary>
        private void DirectSaveColSet()
        {
            if (this.ucSetColumn == null)
            {
                this.ucSetColumn = new FS.HISFC.Components.Common.Controls.ucSetColumn();
                this.ucSetColumn.DisplayEvent += new EventHandler(ucSetColumn_DisplayEvent);
            }

            this.neuSpread1_Sheet1.DataAutoCellTypes = false;

            if (this.isPopSetType == true)         //��ֱ��ά��״̬
            {
                this.isPopSetType = false;

                this.ucSetColumn.SetDataTable( this.neuSpread1_Sheet1, this.GetIndexByColName( "ҩƷ����" ), this.GetIndexByColName( "ϵͳ���" ), this.GetIndexByColName( "���" ),
                   this.GetIndexByColName("ҩƷ����"), this.GetIndexByColName("����"), this.GetIndexByColName("�������"), this.GetIndexByColName("�������"), this.GetIndexByColName("ʹ�÷���"),
                  this.GetIndexByColName( "Ƶ��" ), this.GetIndexByColName( "��������" ) );
                this.ucSetColumn.SetColVisible( false, false, false, true );
                this.ucSetColumn.IsShowUpDonw = false;
            }

            FS.FrameWork.WinForms.Classes.Function.PopForm.Text = "ֱ��ά��������";
            FS.FrameWork.WinForms.Classes.Function.PopShowControl(this.ucSetColumn);

            //{68500353-8194-41b0-9A14-539395DF2D74}  ���ӶԲ�������ķ���
            if (this.ucSetColumn.Result == DialogResult.OK)
            {
                isPopSetType = false;
            }
            else
            {
                isPopSetType = true;
            }
        }

        /// <summary>
        /// ֱ��ά��״̬��ʼ��������
        /// </summary>
        private void InitDirectSave()
        {
            if (this.ucSetColumn == null)
                return;

            this.dt.AcceptChanges();

            List<string> checkCol = this.ucSetColumn.GetCheckCol(FS.HISFC.Components.Common.Controls.CheckCol.Set);
            if (checkCol.Count > 0)
            {
                this.SetToolBarEnableForSetType(true);
            }
            foreach (string str in checkCol)
            {
                int iIndex = this.GetIndexByColName(str);
                if (iIndex == -1)
                    return;
                this.neuSpread1_Sheet1.Columns[iIndex].Locked = false;
                switch (str)
                {
                    #region ���ݼ�����ʾ

                    case "ҩƷ����":
                        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbDrugCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                        cmbDrugCell.Items = this.GetStrByHelper(this.drugTypeHelper,true);
                        this.neuSpread1_Sheet1.Columns[iIndex].CellType = cmbDrugCell;
                        break;
                    case "ϵͳ���":
                        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbSysClassCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                        cmbSysClassCell.Items = this.GetStrByHelper(this.sysClassHelper,true);
                        this.neuSpread1_Sheet1.Columns[iIndex].CellType = cmbSysClassCell;
                        break;
                    case "ҩƷ����":
                        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbQualityCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                        cmbQualityCell.Items = this.GetStrByHelper(this.qualityHelper,true);
                        this.neuSpread1_Sheet1.Columns[iIndex].CellType = cmbQualityCell;
                        break;
                    case "����":
                        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbDosageCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                        cmbDosageCell.Items = this.GetStrByHelper( this.dosageFormHelper, true );
                        this.neuSpread1_Sheet1.Columns[iIndex].CellType = cmbDosageCell;
                        break;
                    case "�������":
                        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbDivCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                        cmbDivCell.Items = new string[] { "��С��λ����ȡ��", "��װ��λ����ȡ��", "��С��λÿ��ȡ��", "��װ��λÿ��ȡ��" };
                        this.neuSpread1_Sheet1.Columns[iIndex].CellType = cmbDivCell;
                        break;
                    case "�������":
                        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbDivCell2 = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                        cmbDivCell2.Items = new string[] { "��С��λ����ȡ��", "��װ��λ����ȡ��", "��С��λÿ��ȡ��", "��װ��λÿ��ȡ��,��С��λ�ɲ��" };
                        this.neuSpread1_Sheet1.Columns[iIndex].CellType = cmbDivCell2;
                        break;
                    case "ʹ�÷���":
                        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbUsageCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                        cmbUsageCell.Items = this.GetStrByHelper(this.usageHelper,true);
                        this.neuSpread1_Sheet1.Columns[iIndex].CellType = cmbUsageCell;
                        break;
                    case "Ƶ��":
                        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbFreCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                        cmbFreCell.Items = this.GetStrByHelper(this.frequencyHelper,false);
                        this.neuSpread1_Sheet1.Columns[iIndex].CellType = cmbFreCell;
                        break;
                    case "��������":
                        FarPoint.Win.Spread.CellType.ComboBoxCellType cmbStoreCell = new FarPoint.Win.Spread.CellType.ComboBoxCellType();
                        cmbStoreCell.Items = this.GetStrByHelper(this.storeContionHelper,true);
                        this.neuSpread1_Sheet1.Columns[iIndex].CellType = cmbStoreCell;
                        break;

                    #endregion
                }
            }
        }

        /// <summary>
        /// ���������Ʒ���������
        /// </summary>
        /// <param name="colName">������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int GetIndexByColName(string colName)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (this.neuSpread1_Sheet1.Columns[i].Label == colName)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// ��Helper�ڷ����ַ���
        /// </summary>
        /// <param name="helper">������</param>
        /// <param name="isNameProperty">�Ƿ�ȡName���� ����ȡID����</param>
        /// <returns>�ɹ�����Name�ַ�������</returns>
        private string[] GetStrByHelper(FS.FrameWork.Public.ObjectHelper helper,bool isNameProperty)
        {
            string[] strName = new string[helper.ArrayObject.Count];
            int i = 0;
            foreach (FS.FrameWork.Models.NeuObject neuObj in helper.ArrayObject)
            {
                if (isNameProperty)
                    strName[i] = neuObj.Name;
                else
                    strName[i] = neuObj.ID;
                i++;
            }
            return strName;
        }

        /// <summary>
        /// ��ֱ��ά�������ݽ��б���
        /// </summary>
        ///<returns>����ɹ�����1 ʧ�ܷ���-1</returns>
        private int DirectSave()
        {
            this.Filter(true);
            for (int i = 0; i < dv.Count; i++)
            {
                this.dv[i].EndEdit();
            }
            DataTable dtModify = this.dt.GetChanges(DataRowState.Modified);
            if (dtModify != null && dtModify.Rows.Count > 0)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();

                //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
                //t.BeginTransaction();

                this.itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

                foreach (DataRow dr in dtModify.Rows)
                {
                    if (this.SaveModify(dr) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("����ҩƷ��Ϣʧ��" + this.itemManager.Err);
                        return -1;
                    }
                }
                FS.FrameWork.Management.PublicTrans.Commit();
                MessageBox.Show("����ɹ�");

                this.dt.AcceptChanges();
            }
            return 1;
        }

        /// <summary>
        /// ������ĵ�ҩƷ��Ϣ
        /// </summary>
        /// <param name="dr">�豣���ҩƷ��Ϣ</param>
        /// <returns>�ɹ����淵��1 �������󷵻�-1</returns>
        private int SaveModify(DataRow dr)
        {
            string drugNO = dr["����"].ToString();
            if (drugNO != "")
            {
                FS.HISFC.Models.Pharmacy.Item itemTemp = this.itemManager.GetItem(drugNO);
                if (itemTemp != null)
                {
                    itemTemp.Type.ID = this.drugTypeHelper.GetID(dr["ҩƷ����"].ToString());
                    itemTemp.SysClass.ID = this.sysClassHelper.GetID(dr["ϵͳ���"].ToString());
                    itemTemp.Specs = dr["���"].ToString();
                    itemTemp.Quality.ID = this.qualityHelper.GetID(dr["ҩƷ����"].ToString());
                    itemTemp.DosageForm.ID = this.dosageFormHelper.GetID(dr["����"].ToString());
                   
                    //by cube 2011-03-28 סԺ����ҽ���������
                    //itemTemp.SplitType = dr["������"].ToString() == "���ﲻ�ɲ�ְ�װ��λ" ? "1" : "0";
                    string splitType = dr["�������"].ToString();
                    if (splitType == "��С��λ����ȡ��")
                    {
                        itemTemp.SplitType = "0";
                    }
                    else if (splitType == "��װ��λ����ȡ��")
                    {
                        itemTemp.SplitType = "1";
                    }
                    else if (splitType == "��С��λÿ��ȡ��")
                    {
                        itemTemp.SplitType = "2";
                    }
                    else if (splitType == "��װ��λÿ��ȡ��")
                    {
                        itemTemp.SplitType = "3";
                    }

                    splitType = dr["�������"].ToString();
                    if (splitType == "��С��λ����ȡ��")
                    {
                        itemTemp.CDSplitType = "0";
                    }
                    else if (splitType == "��װ��λ����ȡ��")
                    {
                        itemTemp.CDSplitType = "1";
                    }
                    else if (splitType == "��С��λÿ��ȡ��")
                    {
                        itemTemp.CDSplitType = "2";
                    }
                    else if (splitType == "��װ��λÿ��ȡ��")
                    {
                        itemTemp.CDSplitType = "3";
                    }
                    else if (splitType == "��С��λ�ɲ��")
                    {
                        itemTemp.CDSplitType = "4";
                    }
                    //end by

                    itemTemp.Usage.ID = this.usageHelper.GetID(dr["ʹ�÷���"].ToString());
                    itemTemp.Frequency.ID = dr["Ƶ��"].ToString();
                    itemTemp.Product.StoreCondition = dr["��������"].ToString();

                    if (this.itemManager.SetItem(itemTemp) == -1)
                        return -1;
                }
            }
            return 1;
        }

        #endregion

        private void ucSetColumn_DisplayEvent(object sender, EventArgs e)
        {
            if (this.isPopSetType)
            {
                #region Ӧ��������

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("����Ӧ��������...���Ժ�");
                Application.DoEvents();

                try
                {
                    FS.FrameWork.WinForms.Classes.CustomerFp.ReadColumnProperty(this.neuSpread1_Sheet1, this.filePath);
                    List<FS.HISFC.Models.Pharmacy.Item> al = new List<FS.HISFC.Models.Pharmacy.Item>();
                    al = this.itemManager.QueryItemList();
                    if (al == null)
                    {
                        MessageBox.Show(this.itemManager.Err, "������ʾ");
                        return;
                    }
                    if (this.SetDataSet( al ) != 1)
                    {
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ӧ��������ʧ��" + ex.Message);
                }
                finally
                {
                    FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                }

                #endregion
            }
            else
            {
                this.InitDirectSave();
            }
        }

        private void ucPharmacyQuery_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm("���ڼ������� ���Ժ�...");
                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(10);
                Application.DoEvents();

                this.InitControlParam();

                this.InitData();

                this.InitTreeView();

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(20);

                this.InitDrug();

                FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(80);

                this.InitExpediency();

                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData.GetHashCode() == Keys.Control.GetHashCode() + Keys.C.GetHashCode())
            {
                this.Copy();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void MainteranceUC_EndSave(FS.HISFC.Models.Pharmacy.Item item)
        {
            this.AddNewRow(item);
        }

        private void neuSpread1_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            if (this.isPopSetType)
                FS.FrameWork.WinForms.Classes.CustomerFp.SaveColumnProperty(this.neuSpread1_Sheet1, this.filePath);
        }

        private void neuTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (this.dt.Rows.Count == 0)
            {
                return;
            }

            Dictionary<int, bool> sortStateDictionary = new Dictionary<int, bool>();

            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                sortStateDictionary.Add(i, this.neuSpread1_Sheet1.Columns[i].AllowAutoSort);

                this.neuSpread1_Sheet1.Columns[i].AllowAutoSort = false;
            }

            if (this.ckRealTimeFilter.Checked)
            {
                this.Filter();
            }

            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                this.neuSpread1_Sheet1.Columns[i].AllowAutoSort = sortStateDictionary[i];
            }
        }

        private void cmbFilterField_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Filter();
        }

        private void txtInputCode_KeyDown(object sender, KeyEventArgs e)
        {
            //�ϼ�ͷѡ����һ����¼
            if (e.KeyCode == Keys.Up)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex > 0)
                {
                    this.neuSpread1_Sheet1.ActiveRowIndex--;
                    return;
                }
            }
            //�¼�ͷѡ����һ����¼
            if (e.KeyCode == Keys.Down)
            {
                if (this.neuSpread1_Sheet1.ActiveRowIndex < this.neuSpread1_Sheet1.RowCount)
                {
                    this.neuSpread1_Sheet1.ActiveRowIndex++;
                    return;
                }
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (this.ckRealTimeFilter.Checked)
                {
                    this.txtInputCode.SelectAll();
                    this.Modify();
                }
                else
                {
                    this.Filter();
                }
            }
        }

        private void neuSpread1_CellDoubleClick(object sender, FarPoint.Win.Spread.CellClickEventArgs e)
        {
            if (e.ColumnHeader) return;
            this.Modify();
        }

        private void tvType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            this.Filter();
        }

        private void neuSpread1_AutoSortingColumn(object sender, FarPoint.Win.Spread.AutoSortingColumnEventArgs e)
        {
            for (int i = 0; i < this.neuSpread1_Sheet1.Columns.Count; i++)
            {
                if (i != e.Column)
                {
                    this.neuSpread1_Sheet1.Columns[i].SortIndicator = FarPoint.Win.Spread.Model.SortIndicator.None;
                }
            }
            string sortString = string.Empty;


            switch (this.neuSpread1_Sheet1.ColumnHeader.Columns[e.Column].SortIndicator)
            {
                case FarPoint.Win.Spread.Model.SortIndicator.Ascending:
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[e.Column].SortIndicator = FarPoint.Win.Spread.Model.SortIndicator.Descending;
                    sortString = this.neuSpread1_Sheet1.Columns[e.Column].DataField + " DESC";
                    break;
                case FarPoint.Win.Spread.Model.SortIndicator.Descending:
                    this.neuSpread1_Sheet1.Columns[e.Column].SortIndicator = FarPoint.Win.Spread.Model.SortIndicator.None;

                    sortString = this.neuSpread1_Sheet1.Columns[e.Column].DataField + " ";

                    break;
                case FarPoint.Win.Spread.Model.SortIndicator.None:
                    this.neuSpread1_Sheet1.ColumnHeader.Columns[e.Column].SortIndicator = FarPoint.Win.Spread.Model.SortIndicator.Ascending;
                    sortString = string.Empty;
                    break;
                default:
                    break;
            }
            this.dv.Sort = sortString;
            e.Cancel = true;
        }

        #region ʵ��ҩƷ�ֵ��б���Զ������������ by Sunjh 2010-9-26 {D1523F8E-E81A-4f94-BF39-CCFD16886BDE}
        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.neuSpread1_Sheet1.ActiveColumnIndex >= 0)
            {
                this.neuSpread1_Sheet1.ActiveColumn.AllowAutoFilter = !this.neuSpread1_Sheet1.ActiveColumn.AllowAutoFilter;
            }
        }

        //private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (this.neuSpread1_Sheet1.ActiveColumnIndex >= 0)
        //    {
        //        this.neuSpread1_Sheet1.ActiveColumn.AllowAutoSort = !this.neuSpread1_Sheet1.ActiveColumn.AllowAutoSort;
        //    }
        //}
        #endregion

    }
}
