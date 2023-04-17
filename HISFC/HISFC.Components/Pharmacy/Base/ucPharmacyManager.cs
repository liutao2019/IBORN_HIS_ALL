using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using FS.FrameWork.Management;

namespace FS.HISFC.Components.Pharmacy.Base
{
    /// <summary>
    /// [��������: ҩƷ������Ϣά���ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <�޸ļ�¼>
    ///    1.�ų���������������С��λ����һ����ɵı����Ϣ��ȡ���ݲ���ȷ���� by Sunjh 2010-8-26 {D6D30303-8AB6-42d4-B35B-D76A4C16168F}
    /// </�޸ļ�¼>
    /// </summary>
    public partial class ucPharmacyManager : FS.FrameWork.WinForms.Controls.ucBaseControl
    {
        public ucPharmacyManager()
        {
            InitializeComponent();

            if (!this.DesignMode)
                this.Init();
        }

        public delegate void SaveItemHandler(FS.HISFC.Models.Pharmacy.Item item);

        public new event SaveItemHandler EndSave;

        public new event SaveItemHandler BeginSave;

        #region ������

        /// <summary>
        /// ҩƷ�����࣭����ҩƷ�������еķ���
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

        /// <summary>
        /// ҩƷ���������࣭ȡҩ�������б� 
        /// </summary>
        private FS.HISFC.BizLogic.Pharmacy.Constant itemConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
     
        /// <summary>
        /// ���������࣭ȡ�����б�
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
      
        /// <summary>
        /// Ƶ�ι����࣭ȡƵ���б�
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Frequency frequencyManager = new FS.HISFC.BizLogic.Manager.Frequency();
      
        /// <summary>
        /// ƴ��������
        /// </summary>
        private FS.HISFC.BizLogic.Manager.Spell spellManager = new FS.HISFC.BizLogic.Manager.Spell();
      
        /// <summary>
        /// ��չ������
        /// </summary>
        private FS.FrameWork.Management.ExtendParam extandManager = new FS.FrameWork.Management.ExtendParam();

        #endregion

        #region �����

        /// <summary>
        /// �������� Update/Insert/Check
        /// </summary>
        private string inputType = "Update";

        /// <summary>
        /// ���Ʋ��� ҩƷ�Ƿ���Ҫ�������
        /// </summary>
        private string checkCtrl = "0";

        /// <summary>
        /// oldValid ���ڱ������ʱȡ����ԭ��¼����Ч״̬�����ȡ��ʱΪ��Ч������Ϊ��Ч�����ݿ��ж�ͬһ���Զ��������ܴ���һ����Ч��Ϣ
        /// ��ʱ���ݿ�������Ѵ���һ����Ч�ļ�¼�����������Ӳ�����ʱ��ֱ���ж����ݿ����Ƿ������Ч�ļ�¼
        /// </summary>
        private bool itemPrivValid;

        /// <summary>
        /// �ؼ��ڲ�����ҩƷʵ��
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item item = null;

        /// <summary>
        /// ������չ��������
        /// </summary>
        private FS.HISFC.Models.Base.ExtendInfo extendInfo = new FS.HISFC.Models.Base.ExtendInfo();

        /// <summary>
        /// �س���ת˳��
        /// </summary>
        private System.Collections.Hashtable hsJudgeOrder = new Hashtable();

        /// <summary>
        /// ԭ����
        /// </summary>
        private FS.HISFC.Models.Pharmacy.Item originalItem = null;

        /// <summary>
        /// ƴ�����Զ����ɷ�ʽ 
        /// </summary>
        private DrugAutoSpellType drugAutoSpell = DrugAutoSpellType.TradeName;

        /// <summary>
        /// �Ƿ����Ʒ�����������ǿ�����ж�
        /// </summary>
        private bool isJudgeTradeName = true;

        /// <summary>
        /// �Ƿ������޸Ļ���������Ϣ
        /// </summary>
        private bool allowAlterDose = false;

        /// <summary>
        /// �Զ���������ַ��� 0 �������� 1 ������λ 2 ��װ���� 3 ��С��λ 4 ��װ��λ
        /// </summary>
        private string autoCreateSpecs = "{0}{1}*{2}{3}/{4}";

        /// <summary>
        /// һ��ҩ������    {6E41A9CD-AEDC-4aae-8E46-1F312F0FA4C6}  ��ֱ��ά������ҩ������
        /// </summary>
        private List<FS.HISFC.Models.Pharmacy.PhaFunction> alLevel1Function = null;

        /// <summary>
        /// ����ҩ������    {6E41A9CD-AEDC-4aae-8E46-1F312F0FA4C6}  ��ֱ��ά������ҩ������
        /// </summary>
        private List<FS.HISFC.Models.Pharmacy.PhaFunction> alLevel2Function = null;

        /// <summary>  
        /// ����ҩ������    {6E41A9CD-AEDC-4aae-8E46-1F312F0FA4C6}  ��ֱ��ά������ҩ������
        /// </summary>  
        private List<FS.HISFC.Models.Pharmacy.PhaFunction> alLevel3Function = null;

        /// <summary>
        /// ͨ��������Ʒ���Ƿ�ϲ�  {4EED03A7-7E22-4a93-9ADC-5FF007D51A92}
        /// </summary>
        private bool isMergerName = false;

        #endregion

        #region ����

        /// <summary>
        /// �������� Update/Insert/Check
        /// </summary>
        public string InputType
        {
            get
            {
                return this.inputType;
            }
            set
            {
                this.inputType = value;
                if (value.ToString().ToUpper() == "UPDATE")
                {
                    this.continueCheckBox.Enabled = false;
                    this.chbIsStop.Enabled = true;
                }
                else if (value.ToUpper().ToUpper() == "INSERT")
                {
                    this.continueCheckBox.Enabled = true;
                }
            }
        }

        /// <summary>
        /// ����ҩƷ�Ƿ���Ҫ�������
        /// </summary>
        public bool IsCheck
        {
            get
            {
                if (checkCtrl == "1")
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// �Ƿ���ֻ��״̬ �������޸�
        /// </summary>
        public bool ReadOnly
        {
            get
            {
                return this.btnSave.Visible;
            }
            set
            {
                this.btnSave.Visible = !value;
            }
        }

        /// <summary>
        /// �ؼ��ڲ�����ҩƷʵ��
        /// </summary>
        public FS.HISFC.Models.Pharmacy.Item Item
        {
            set
            {
                if (value == null)
                {
                    this.item = new FS.HISFC.Models.Pharmacy.Item();
                }
                else
                {
                    this.item = value;
                }

                this.originalItem = this.item.Clone();

                this.SetItem();
            }
        }

        /// <summary>
        /// ��װ�����ֶ����λ��
        /// </summary>
        public int PackQtyNumPrecision
        {
            get
            {
                return this.txtPackQty.NumericPrecision;
            }
            set
            {
                this.txtPackQty.NumericPrecision = value;
            }
        }

        /// <summary>
        /// ���������ֶ����λ��
        /// </summary>
        public int BaseDoseNumPrecision
        {
            get
            {
                return this.txtBaseDose.NumericPrecision;
            }
            set
            {
                this.txtBaseDose.NumericPrecision = value;
            }
        }

        /// <summary>
        /// �۸��ֶ����λ��
        /// </summary>
        public int PriceNumPrecision
        {
            get
            {
                return this.txtRetailPrice.NumericPrecision;
            }
            set
            {
                this.txtRetailPrice.NumericPrecision = value;
                this.txtTopRetailPrice.NumericPrecision = value;
                this.txtWholesalePrice.NumericPrecision = value;
                this.txtPurchasePrice.NumericPrecision = value;
            }
        }

        /// <summary>
        /// ��Ʒ���Զ�����������������λ��
        /// </summary>
        public int NameUserCodeMaxLength
        {
            get
            {
                return this.txtUserCode.MaxLength;
            }
            set
            {
                this.txtUserCode.MaxLength = value;
            }
        }

        /// <summary>
        /// �����Զ�����������������λ��
        /// </summary>
        public int OtherUserCodeMaxLength
        {
            get
            {
                return this.txtRegularUserCode.MaxLength;
            }
            set
            {
                this.txtRegularUserCode.MaxLength = value;
            }
        }

        /// <summary>
        /// �Ƿ�����ͨ����ά�����Tab˳��
        /// </summary>
        public bool IsRegularTabOrder
        {
            get
            {
                return this.txtRegularName.TabStop;
            }
            set
            {
                this.txtRegularName.TabStop = value;
                this.txtRegularUserCode.TabStop = value;
            }
        }

        /// <summary>
        /// �Ƿ���Ӣ����ά�����Tab˳��
        /// </summary>
        public bool IsEnglishTabOrder
        {
            get
            {
                return this.txtEnglishName.TabStop;
            }
            set
            {
                this.txtEnglishName.TabStop = value;
                this.txtEnglishOtherName.TabStop = value;
                this.txtEnglishRegularName.TabStop = value;
            }
        }

        /// <summary>
        /// �Ƿ��������ά�����Tab˳��
        /// </summary>
        public bool IsCodeTabOrder
        {
            get
            {
                return this.txtGbCode.TabStop;
            }
            set
            {
                this.txtGbCode.TabStop = value;
                this.txtInternationalCode.TabStop = value;
            }
        }

        /// <summary>
        /// ҩƷ��������ʾ��Ϣ����   {6F6120F5-6D88-47ce-AF9C-0CF781DE412F}  ���ԭ��������
        /// </summary>
        public string ItemSpeInformationSetting
        {
            set
            {
                if (value.IndexOf( "A" ) != -1 && value.IndexOf( "B" ) != -1 && value.IndexOf( "C" ) != -1)
                {
                    string strFlag1 = value.Substring( 0, value.IndexOf( "B" ) );
                    string strFlag2 = value.Substring( value.IndexOf( "B" ), value.IndexOf( "C" ) - value.IndexOf( "B" ) );
                    string strFlag3 = value.Substring( value.IndexOf( "C" ) );

                    this.chbSpecialFlag.Visible = FS.FrameWork.Function.NConvert.ToBoolean( strFlag1.Substring( 1, 1 ) );       //�Ƿ�ѡ��
                    this.chbSpecialFlag.Text = strFlag1.Substring( 2 );

                    this.chbSpecialFlag1.Visible = FS.FrameWork.Function.NConvert.ToBoolean( strFlag2.Substring( 1, 1 ) );       //�Ƿ�ѡ��
                    this.chbSpecialFlag1.Text = strFlag2.Substring( 2 );

                    this.chbSpecialFlag2.Visible = FS.FrameWork.Function.NConvert.ToBoolean( strFlag3.Substring( 1, 1 ) );       //�Ƿ�ѡ��
                    this.chbSpecialFlag2.Text = strFlag3.Substring( 2 );
                }
            }
        }

        /// <summary>
        /// ƴ���롢������Զ����ɷ�ʽ
        /// </summary>
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
        /// �Ƿ����Ʒ�����������ǿ�����ж�
        /// </summary>
        public bool IsJudgeTradeName
        {
            get
            {
                return this.isJudgeTradeName;
            }
            set
            {
                this.isJudgeTradeName = value;

                if (!value)
                {
                    this.lbTradeName.ForeColor = System.Drawing.Color.Black;
                    this.lbSpellCode.ForeColor = System.Drawing.Color.Black;
                    this.lbWbCode.ForeColor = System.Drawing.Color.Black;
                    this.lbUserCode.ForeColor = System.Drawing.Color.Black;

                    this.lbRegularName.ForeColor = System.Drawing.Color.Blue;
                    this.lbRegularSpell.ForeColor = System.Drawing.Color.Blue;
                    this.lbRegularWb.ForeColor = System.Drawing.Color.Blue;
                    this.lbRegularUser.ForeColor = System.Drawing.Color.Blue;
                }
            }
        }

        /// <summary>
        /// �Ƿ������޸Ļ���������Ϣ
        /// </summary>
        public bool AllowAlterDose
        {
            get
            {
                return this.allowAlterDose;
            }
            set
            {
                this.allowAlterDose = value;
            }
        }

        /// <summary>
        ///  �Զ���������ַ��� 0 �������� 1 ������λ 2 ��װ���� 3 ��С��λ 4 ��װ��λ
        /// 
        /// {773D56E7-4828-48d4-99C8-C80428112EBC}
        /// </summary>
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

        #region ����

        /// <summary>
        /// �ӿؼ���ȡ���ݣ�������item��
        /// </summary>
        private int GetItem()
        {
            if (this.item == null)
            {
                item = new FS.HISFC.Models.Pharmacy.Item();
            }

            //ͨ��������Ʒ���Ƿ�ϲ�  {4EED03A7-7E22-4a93-9ADC-5FF007D51A92}
            if (this.isMergerName)
            {
                if (string.IsNullOrEmpty( this.txtRegularName.Text ) == false)
                {
                    this.item.Name = this.txtName.Text +"(" + this.txtRegularName.Text + ")";
                    //this.item.NameCollection.RegularName = this.txtRegularName.Text;
                    this.item.NameCollection.RegularName = "";
                }
                else
                {
                    this.item.Name = this.txtName.Text;
                    this.item.NameCollection.RegularName = this.txtRegularName.Text;
                }
            }
            else
            {
                this.item.Name = this.txtName.Text;
                this.item.NameCollection.RegularName = this.txtRegularName.Text;
            }
            
            this.item.SpellCode = this.txtSpellCode.Text;
            this.item.WBCode = this.txtWbCode.Text;
            this.item.UserCode = this.txtUserCode.Text;

            try
            {

                //����ҩƷ�����������β��������и�ֵ
                this.GetItemBefore();

                #region �ֵ���Ϣ��ֵ

               
                this.item.NameCollection.RegularSpell.SpellCode = this.txtRegularSpellCode.Text;
                this.item.NameCollection.RegularSpell.WBCode = this.txtRegularWbCode.Text;
                this.item.NameCollection.RegularSpell.UserCode = this.txtRegularUserCode.Text;
                this.item.NameCollection.FormalName = this.txtFormalName.Text;
                this.item.NameCollection.FormalSpell.SpellCode = this.txtFormalSpellCode.Text;
                this.item.NameCollection.FormalSpell.WBCode = this.txtFormalWbCode.Text;
                this.item.NameCollection.FormalSpell.UserCode = this.txtFormalUserCode.Text;
                this.item.NameCollection.OtherName = this.txtOtherName.Text;
                this.item.NameCollection.OtherSpell.SpellCode = this.txtOtherSpellCode.Text;
                this.item.NameCollection.OtherSpell.WBCode = this.txtOtherWbCode.Text;
                this.item.NameCollection.OtherSpell.UserCode = this.txtOtherUserCode.Text;
                this.item.NameCollection.EnglishName = this.txtEnglishName.Text;
                this.item.NameCollection.EnglishOtherName = this.txtEnglishOtherName.Text;
                this.item.NameCollection.EnglishRegularName = this.txtEnglishRegularName.Text;
                this.item.NameCollection.GbCode = this.txtGbCode.Text;
                this.item.NameCollection.InternationalCode = this.txtInternationalCode.Text;
                this.item.PackUnit = this.cmbPackUnit.Text.ToString().Trim();
                this.item.MinUnit = this.cmbMinUnit.Text.ToString().Trim();
                this.item.DoseUnit = this.cmbDoseUnit.Text.ToString().Trim();
                this.item.Type.ID = this.cmbDrugType.Tag.ToString().Trim();
                this.item.SysClass.ID = this.cmbSysClass.Tag.ToString();
                this.item.MinFee.ID = this.cmbMinFee.Tag.ToString();
                this.item.Quality.ID = this.cmbQuality.Tag.ToString();
                this.item.DosageForm.ID = this.cmbDosageForm.Tag.ToString();
                this.item.PriceCollection.PriceForm.ID = this.cmbPriceForm.Tag.ToString();
                this.item.Grade = this.cmbGrade.Tag.ToString();
                this.item.Specs = this.txtSpecs.Text;
                this.item.Usage.ID = this.cmbUsage.Tag.ToString();
                this.item.Frequency.ID = this.cmbFrequency.Tag.ToString();
                this.item.Product.Caution = this.txtCaution.Text;
                this.item.Ingredient = this.txtIngredient.Text;
                this.item.Product.StoreCondition = this.Text;
                this.item.ExecuteStandard = this.txtExecuteStandard.Text;
                this.item.PhyFunction1.ID = this.cmbPhyFunction1.Tag.ToString();
                this.item.PhyFunction2.ID = this.cmbPhyFunction2.Tag.ToString();
                this.item.PhyFunction3.ID = this.cmbPhyFunction3.Tag.ToString();
                this.item.Product.Producer.ID = this.cmbProducer.Tag.ToString();
                this.item.Memo = this.txtMemo.Text;
                this.item.Product.ApprovalInfo = this.txtApprovalInfo.Text;
                this.item.Product.Label = this.txtLabel.Text;
                this.item.Product.ProducingArea = this.txtProducingArea.Text;
                this.item.Product.Company.ID = this.cmbCompany.Tag.ToString();
                this.item.Product.BarCode = this.txtBarCode.Text;
                this.item.Product.BriefIntroduction = this.txtBriefIntroduction.Text;
                this.item.Product.Manual = this.txtManual.Text;
                this.item.TenderOffer.IsTenderOffer = this.chbIsTenderOffer.Checked;
                this.item.TenderOffer.Company.ID = this.cmbTenderCompancy.Tag.ToString();
                this.item.TenderOffer.ContractNO = this.txtContractCode.Text;
                this.item.TenderOffer.BeginTime = this.dtpBeginDate.Value;
                this.item.TenderOffer.EndTime = this.dtpEndDate.Value;
                //Ĭ�ϵı䶯����Ϊ:�޸�
                this.item.ShiftType.ID = "U";           //�䶯����(U����, M�����޸� ,N��ҩ, Sͣ��, A����)

                //��������õ�ҩƷͣ��,���¼�䶯ʱ��
                if (!this.item.IsStop && this.chbIsStop.Checked)
                {
                    this.item.ShiftType.ID = "S";       //�䶯����(U����, M�����޸� ,N��ҩ, Sͣ��, A����)
                }

                #endregion

                #region �����Ϣ��ֵ

                this.item.Product.IsSelfMade = this.chbIsSelfMade.Checked;
                this.item.IsAllergy = this.chbIsAllergy.Checked;
                this.item.IsGMP = this.chbIsGMP.Checked;
                this.item.IsOTC = this.chbIsOTC.Checked;
                this.item.IsShow = this.chbIsShow.Checked;
                this.item.IsLack = this.chbIsLack.Checked;
                this.item.IsSubtbl = this.chbIsAppend.Checked;
                this.item.Product.StoreCondition = this.cmbStoreCondition.Text;
                this.item.PriceCollection.RetailPrice = FS.FrameWork.Function.NConvert.ToDecimal( this.txtRetailPrice.Text );
                this.item.PriceCollection.WholeSalePrice = FS.FrameWork.Function.NConvert.ToDecimal( this.txtWholesalePrice.Text );
                this.item.PriceCollection.PurchasePrice = FS.FrameWork.Function.NConvert.ToDecimal( this.txtPurchasePrice.Text );
                this.item.PriceCollection.TopRetailPrice = FS.FrameWork.Function.NConvert.ToDecimal( this.txtTopRetailPrice.Text );
                this.item.TenderOffer.Price = FS.FrameWork.Function.NConvert.ToDecimal( this.txtTenderPrice.Text );
                this.item.OnceDose = FS.FrameWork.Function.NConvert.ToDecimal( this.txtOnceDose.Text );
                this.item.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal( this.txtBaseDose.Text );
                this.item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal( this.txtPackQty.Text );
                this.item.SpecialFlag = FS.FrameWork.Function.NConvert.ToInt32( this.chbSpecialFlag.Checked ).ToString();
                this.item.SpecialFlag1 = FS.FrameWork.Function.NConvert.ToInt32( this.chbSpecialFlag1.Checked ).ToString();
                this.item.SpecialFlag2 = FS.FrameWork.Function.NConvert.ToInt32( this.chbSpecialFlag2.Checked ).ToString();
                this.item.IsNostrum = this.ckNostrum.Checked;

                if (this.cmbSpecialFlag3.Text == "")
                {
                    this.cmbSpecialFlag3.SelectedIndex = 0;
                }
                this.item.SpecialFlag3 = this.cmbSpecialFlag3.SelectedIndex.ToString();

                if (this.inputType.Trim().ToUpper() == "INSERT")
                {
                    this.item.ShiftType.ID = "N";                                       //�䶯����(U����, M�����޸� ,N��ҩ, Sͣ��, A����)
                }

                this.item.ShiftTime = this.itemManager.GetDateTimeFromSysDateTime();	//�䶯ʱ��

                if (txtSplitType.SelectedIndex < 0)
                    this.item.SplitType = DBNull.Value.ToString();
                else
                    this.item.SplitType = this.txtSplitType.SelectedIndex.ToString();

                //��ʾ���� 0 ȫԺ  1 סԺ��  2 ����
                if (this.cmbShow.SelectedIndex < 0)
                {
                    this.item.ShowState = DBNull.Value.ToString();
                }
                else
                {
                    this.item.ShowState = this.cmbShow.SelectedIndex.ToString();
                }

                #endregion
            }
            catch (Exception e)
            {
                MessageBox.Show( e.Message );
                return -1;
            }
            //��չ�ֶ�ά�����Բ�ά���ֵ䳣������ֱ������{5F011DFA-2111-4553-AB36-27820A6F65FB}
            if (this.cmbExtend1.Width != 0 && this.cmbExtend1.Tag != null)            //��չ����1 {8ADD2D48-2427-48aa-A521-4B17EECBC8B4}
            {
                this.item.ExtendData1 = this.cmbExtend1.Tag.ToString();
            }
            else if (this.txtExtend1.Width != 0)
            {
                this.item.ExtendData1 = this.txtExtend1.Text;
            }
            if (this.cmbExtend2.Width != 0 && this.cmbExtend2.Tag != null)            //��չ����2 {8ADD2D48-2427-48aa-A521-4B17EECBC8B4}
            {
                this.item.ExtendData2 = this.cmbExtend2.Tag.ToString();
            }
            else if (this.txtExtend2.Width != 0)
            {
                this.item.ExtendData2 = this.txtExtend2.Text;
            }
            this.item.CreateTime = FS.FrameWork.Function.NConvert.ToDateTime( this.dtpCreateDate.Text );
            //Ŀǰ��������ʱ�����޸�
            //if (string.IsNullOrEmpty( this.item.ID ) == true)   //�ֵ佨��ʱ�� {8ADD2D48-2427-48aa-A521-4B17EECBC8B4}
            //{
            //    this.item.CreateTime = FS.FrameWork.Function.NConvert.ToDateTime( this.dtpCreateDate.Text );
            //}
            //{B9303CFE-755D-4585-B5EE-8C1901F79450}���ӵڶ������
            this.item.RetailPrice2 = FS.FrameWork.Function.NConvert.ToDecimal(this.txtRetailPrice2.Text);
            
            //by cube 2011-03-28 ����סԺ����ҽ���������
            if (this.ncmbSplitType.SelectedIndex < 0)
            {
                this.item.CDSplitType = string.Empty;
            }
            else
            {
                this.item.CDSplitType = this.ncmbSplitType.SelectedIndex.ToString();
            }
            //end by

            return 1;
        }

        /// <summary>
        /// ���ݴ����Itemʵ����Ϣ ���ÿؼ���ʾ
        /// </summary>
        private void SetItem()
        {
            this.txtName.Text = this.item.Name;
            this.txtSpellCode.Text = this.item.SpellCode;
            this.txtWbCode.Text = this.item.WBCode;
            this.txtUserCode.Text = this.item.UserCode;
            this.txtRegularName.Text = this.item.NameCollection.RegularName;
            this.txtRegularSpellCode.Text = this.item.NameCollection.RegularSpell.SpellCode;
            this.txtRegularWbCode.Text = this.item.NameCollection.RegularSpell.WBCode;
            this.txtRegularUserCode.Text = this.item.NameCollection.RegularSpell.UserCode;
            this.txtFormalName.Text = this.item.NameCollection.FormalName;
            this.txtFormalSpellCode.Text = this.item.NameCollection.FormalSpell.SpellCode;
            this.txtFormalWbCode.Text = this.item.NameCollection.FormalSpell.WBCode;
            this.txtFormalUserCode.Text = this.item.NameCollection.FormalSpell.UserCode;
            this.txtOtherName.Text = this.item.NameCollection.OtherName;
            this.txtOtherSpellCode.Text = this.item.NameCollection.OtherSpell.SpellCode;
            this.txtOtherWbCode.Text = this.item.NameCollection.OtherSpell.WBCode;
            this.txtOtherUserCode.Text = this.item.NameCollection.OtherSpell.UserCode;
            this.txtEnglishName.Text = this.item.NameCollection.EnglishName;
            this.txtEnglishOtherName.Text = this.item.NameCollection.EnglishOtherName;
            this.txtEnglishRegularName.Text = this.item.NameCollection.EnglishRegularName;
            this.txtGbCode.Text = this.item.NameCollection.GbCode;
            this.txtInternationalCode.Text = this.item.NameCollection.InternationalCode;
            this.cmbDrugType.Tag = this.item.Type.ID;
            this.cmbSysClass.Tag = this.item.SysClass.ID;
            this.cmbMinFee.Tag = this.item.MinFee.ID;
            this.txtSpecs.Text = this.item.Specs;
            this.txtPackQty.Text = this.item.PackQty.ToString();
            this.cmbMinUnit.Text = this.item.MinUnit;
            this.txtBaseDose.Text = this.item.BaseDose.ToString();
            this.cmbDoseUnit.Text = this.item.DoseUnit;
            this.cmbQuality.Tag = this.item.Quality.ID;
            this.cmbDosageForm.Tag = this.item.DosageForm.ID;
            this.cmbPriceForm.Tag = this.item.PriceCollection.PriceForm.ID;
            this.cmbGrade.Tag = this.item.Grade;
            this.txtRetailPrice.Text = this.item.PriceCollection.RetailPrice.ToString();
            this.txtWholesalePrice.Text = this.item.PriceCollection.WholeSalePrice.ToString();
            this.txtPurchasePrice.Text = this.item.PriceCollection.PurchasePrice.ToString();
            this.txtTopRetailPrice.Text = this.item.PriceCollection.TopRetailPrice.ToString();
            this.cmbPackUnit.Text = this.item.PackUnit;
            this.txtMemo.Text = this.item.Memo;
            this.cmbUsage.Tag = this.item.Usage.ID;
            this.txtOnceDose.Text = this.item.OnceDose.ToString();
            this.cmbFrequency.Tag = this.item.Frequency.ID;
            this.txtCaution.Text = this.item.Product.Caution;
            this.txtIngredient.Text = this.item.Ingredient;
            this.cmbStoreCondition.Text = this.item.Product.StoreCondition;
            if (this.item.Product.StoreCondition == "")
                this.cmbStoreCondition.Tag = "";

            this.txtExecuteStandard.Text = this.item.ExecuteStandard;
            this.cmbPhyFunction1.Tag = this.item.PhyFunction1.ID;
            this.cmbPhyFunction2.Tag = this.item.PhyFunction2.ID;
            this.cmbPhyFunction3.Tag = this.item.PhyFunction3.ID;
            this.cmbProducer.Tag = this.item.Product.Producer.ID;
            this.txtApprovalInfo.Text = this.item.Product.ApprovalInfo;
            this.txtLabel.Text = this.item.Product.Label;
            this.txtProducingArea.Text = this.item.Product.ProducingArea;
            this.cmbCompany.Tag = this.item.Product.Company.ID;
            this.txtBarCode.Text = this.item.Product.BarCode;
            this.txtBriefIntroduction.Text = this.item.Product.BriefIntroduction;
            this.txtManual.Text = this.item.Product.Manual;
            this.chbIsTenderOffer.Checked = this.item.TenderOffer.IsTenderOffer;
            this.cmbTenderCompancy.Tag = this.item.TenderOffer.Company.ID;
            this.txtContractCode.Text = this.item.TenderOffer.ContractNO;
            this.txtTenderPrice.Text = this.item.TenderOffer.Price.ToString();
            this.cmbStoreCondition.Text = this.item.Product.StoreCondition;
            try { this.dtpBeginDate.Value = this.item.TenderOffer.BeginTime; }
            catch { };
            try { this.dtpEndDate.Value = this.item.TenderOffer.EndTime; }
            catch { };
            this.chbIsStop.Checked = this.item.IsStop;
            this.chbIsSelfMade.Checked = this.item.Product.IsSelfMade;
            this.chbIsAllergy.Checked = this.item.IsAllergy;
            this.chbIsGMP.Checked = this.item.IsGMP;
            this.chbIsOTC.Checked = this.item.IsOTC;
            this.chbIsShow.Checked = this.item.IsShow;
            this.chbIsNew.Checked = this.item.IsNew;
            this.chbIsLack.Checked = this.item.IsLack;
            this.chbIsAppend.Checked = this.item.IsSubtbl;
            this.chbSpecialFlag.Checked = FS.FrameWork.Function.NConvert.ToBoolean(this.item.SpecialFlag);
            this.chbSpecialFlag1.Checked = FS.FrameWork.Function.NConvert.ToBoolean(this.item.SpecialFlag1);
            this.chbSpecialFlag2.Checked = FS.FrameWork.Function.NConvert.ToBoolean(this.item.SpecialFlag2);
            this.txtStopReason.Text = this.item.ShiftMark;	//�䶯����
            this.ckNostrum.Checked = this.item.IsNostrum;

            //ҩƷ�������� �����ơ�ְ�����ơ��ض�����
            if (this.item.SpecialFlag3 == null || this.item.SpecialFlag3 == "")
            {
                this.item.SpecialFlag3 = "0";
            }

            this.cmbSpecialFlag3.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(this.item.SpecialFlag3);

            if (this.item.SplitType != null && this.item.SplitType.Trim() != "")
                this.txtSplitType.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(this.item.SplitType);
            else
                this.txtSplitType.SelectedIndex = -1;

            //by cube 2011-03-28 ����סԺ����ҽ���������
            if (!string.IsNullOrEmpty(this.item.CDSplitType))
            {
                this.ncmbSplitType.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(this.item.CDSplitType);
            }
            else
            {
                this.ncmbSplitType.SelectedIndex = -1;
            }
            //end by

            if (this.item.ShowState != null && this.item.ShowState.Trim() != "")
                this.cmbShow.SelectedIndex = FS.FrameWork.Function.NConvert.ToInt32(this.item.ShowState);
            else
                this.cmbShow.SelectedIndex = -1;
            itemPrivValid = this.item.IsStop;//����ԭʼͣ��״̬

            this.SetCheckInfo();

            #region ����ж� ����ҩƷ���ڿ��ʱ �������ҩƷ��Ϣ�����޸�

            this.SetControlState();

            #endregion

            //��չ����1��2 �ֵ佨��ʱ�丳ֵ  {8ADD2D48-2427-48aa-A521-4B17EECBC8B4}
            //��չ�ֶ�ά�����Բ�ά���ֵ䳣������ֱ������{5F011DFA-2111-4553-AB36-27820A6F65FB}
            if (this.cmbExtend1.Width != 0)
            {
                this.cmbExtend1.Tag = this.item.ExtendData1;
            }
            else if (this.txtExtend1.Width != 0)
            {
                this.txtExtend1.Text = this.item.ExtendData1;
            }
            if (this.cmbExtend2.Width != 0)
            {
                this.cmbExtend2.Tag = this.item.ExtendData2;
            }
            else if (this.txtExtend2.Width != 0)
            {
                this.txtExtend2.Text = this.item.ExtendData2;
            }
            if (this.item.CreateTime > this.dtpCreateDate.MinDate)
            {
                this.dtpCreateDate.Value = this.item.CreateTime;
            }
            else
            {
                this.dtpCreateDate.Value = this.itemManager.GetDateTimeFromSysDateTime();
            }
            //{B9303CFE-755D-4585-B5EE-8C1901F79450}���ӵڶ����ۼ�
            this.txtRetailPrice2.Text = this.item.RetailPrice2.ToString();
        }

        /// <summary>
        /// ����ҩƷ������ ���öԱ�����Ϣ�Ƿ������޸�
        /// </summary>
        private void SetControlState()
        {
            if (this.inputType != null && this.inputType.Trim().ToUpper() == "UPDATE")
            {
                //int parm = this.itemManager.GetDrugStorageRowNum(this.itemManager.ID);
                int parm = this.itemManager.GetDrugStorageRowNum(this.item.ID);

                if (parm == -1)
                {
                    MessageBox.Show(Language.Msg("��ȡҩƷ�����Ϣʧ��") + this.itemManager.Err);
                    return;
                }
                //����ҩƷ�ȼ���������
                if (parm > 0)
                {
                    this.txtPackQty.Enabled = false;
                    this.cmbPackUnit.Enabled = false;
                    this.cmbMinUnit.Enabled = false;
                    
                    this.txtBaseDose.Enabled = this.allowAlterDose;
                    this.cmbDoseUnit.Enabled = this.allowAlterDose;

                    this.cmbQuality.Enabled = false;
                    this.txtRetailPrice.Enabled = false;
                    this.txtWholesalePrice.Enabled = false;
                }
                else
                {
                    this.txtPackQty.Enabled = true;
                    this.cmbPackUnit.Enabled = true;
                    this.cmbMinUnit.Enabled = true;
                    this.txtBaseDose.Enabled = true;
                    this.cmbDoseUnit.Enabled = true;
                    this.cmbQuality.Enabled = true;
                    this.txtRetailPrice.Enabled = true;
                    this.txtWholesalePrice.Enabled = true;
                }
               
            }
            else
            {
                this.txtPackQty.Enabled = true;
                this.cmbPackUnit.Enabled = true;
                this.cmbMinUnit.Enabled = true;
                this.txtBaseDose.Enabled = true;
                this.cmbDoseUnit.Enabled = true;
                this.cmbQuality.Enabled = true;
                this.cmbDosageForm.Enabled = true;
                this.txtRetailPrice.Enabled = true;
                this.txtWholesalePrice.Enabled = true;
                this.txtRetailPrice.Enabled = true;
                this.txtWholesalePrice.Enabled = true;
            }
        }

        /// <summary>
        /// ��ȡҪ�����ֵҪ���еĲ���,�������
        /// </summary>
        private void GetItemBefore()
        {
            switch (this.inputType.Trim().ToUpper())
            {
                case "CHECK":
                    this.item.IsNew = false;                                //������������ҩ״̬Ϊ��ͨ�����״̬
                    this.item.IsStop = false;                               //���״̬ҩƷΪ��ͣ��
                    this.item.ShiftMark = "";	                            //�䶯����
                    break;
                case "INSERT":
                    if (this.IsCheck)                                       //��Ҫ���
                    {
                        this.item.IsNew = true;                             //����ҩƷΪδ���״̬
                        this.item.IsStop = true;                            //����ҩƷΪͣ��״̬
                        this.item.ShiftMark = "��ҩδ�����";
                    }
                    else
                    {
                        this.item.IsNew = false;                            //�������Ҫ�����״̬Ϊ��ͨ�����״̬
                        this.item.IsStop = this.chbIsStop.Checked;          //ͣ��״̬��Ҫ�ӽ����Ͽ���
                        this.item.ShiftMark = this.txtStopReason.Text;	    //�䶯����
                    }
                    break;
                case "UPDATE":
                    this.item.IsNew = this.chbIsNew.Checked;
                    this.item.IsStop = this.chbIsStop.Checked;
                    this.item.ShiftMark = this.txtStopReason.Text;
                    break;

            }
        }

        /// <summary>
        /// ����ҩƷ�����Ϣ��ʾ
        /// </summary>
        private void SetCheckInfo()
        {
            this.lbCheckInfo.Text = "";
            if (this.IsCheck)
            {
                if (this.item == null)
                    return;

                if (this.inputType.Trim().ToUpper() == "CHECK")
                {
                    if (this.item.IsNew && this.item.IsStop)
                    {
                        this.chbIsStop.Enabled = false;
                        this.lbCheckInfo.Text = "��ҩƷ��δͨ����ˣ�";
                    }
                    else
                    {
                        this.chbIsStop.Enabled = true;
                        this.lbCheckInfo.Text = "";
                    }
                }
                if (inputType.Trim().ToUpper() == "INSERT")
                {
                    this.chbIsStop.Checked = false;
                    this.lbCheckInfo.Text = "��ҩƷ��Ҫ��˲���ʹ�ã�";
                }
                if (inputType.Trim().ToUpper() == "UPDATE")
                {
                    if (this.item.IsNew && this.item.IsStop)
                    {
                        this.chbIsStop.Enabled = false;
                        this.lbCheckInfo.Text = "��ҩƷ��δͨ����ˣ�";
                    }
                    else
                    {
                        this.lbCheckInfo.Text = "";
                    }
                }
            }
        }

        /// <summary>
        /// ���ݴ�����ַ�����ȡƴ����
        /// </summary>
        ///<returns>���ش����ַ�����ƴ����ʵ��</returns>
        private FS.HISFC.Models.Base.Spell GetSpell(string strData)
        {
            FS.HISFC.BizLogic.Manager.Spell spellManager = new FS.HISFC.BizLogic.Manager.Spell();
            FS.HISFC.Models.Base.Spell spellCode = (FS.HISFC.Models.Base.Spell)spellManager.Get(strData.Trim());
            if (spellCode == null)
                return new FS.HISFC.Models.Base.Spell();
            else
                return spellCode;
        }

        /// <summary>
        /// ����ƴ����
        /// </summary>
        private void JudgeEnter()
        {
            //��ׯ�޸� {184F0EA4-41D7-4f09-9545-69F91312B47C}
            if (this.txtName.Focused || this.cmbDrugType.Focused || this.cmbQuality.Focused || this.cmbDosageForm.Focused)
            {
                string spellCode = this.txtSpellCode.Text;
                string wbCode = this.txtWbCode.Text;

                this.GetTradeNameSpellCode(this.txtName.Text.Trim(), this.cmbDrugType.Text.Trim(), this.cmbQuality.Text.Trim(), this.cmbDosageForm.Text.Trim(),ref spellCode,ref wbCode);

                this.txtSpellCode.Text = spellCode;
                this.txtWbCode.Text = wbCode;

                //this.txtSpellCode.Text = this.GetSpell(this.txtName.Text.Trim()).SpellCode;
                //this.txtWbCode.Text = this.GetSpell(this.txtName.Text.Trim()).WBCode;
            }
            if (this.txtRegularName.Focused)
            {
                this.txtRegularSpellCode.Text = this.GetSpell(this.txtRegularName.Text.Trim()).SpellCode;
                this.txtRegularWbCode.Text = this.GetSpell(this.txtRegularName.Text.Trim()).WBCode;
            }
            if (this.txtFormalName.Focused)
            {
                this.txtFormalSpellCode.Text = this.GetSpell(this.txtFormalName.Text.Trim()).SpellCode;
                this.txtFormalWbCode.Text = this.GetSpell(this.txtFormalName.Text.Trim()).WBCode;
            }
            if (this.txtOtherName.Focused)
            {
                this.txtOtherSpellCode.Text = this.GetSpell(this.txtOtherName.Text.Trim()).SpellCode;
                this.txtOtherWbCode.Text = this.GetSpell(this.txtOtherName.Text.Trim()).WBCode;
            }
            if (this.btnSave.Focused)
            {
                if (this.btnSave.Visible && this.btnSave.Enabled)
                    this.btnSave_Click(null, null);
            }
        }
     
        /// <summary>
        /// ����ҩƷ�����Ϣ
        /// </summary>
        private int SaveCheck()
        {
            if (this.inputType.Trim().ToUpper() == "CHECK")
            {
                if (FS.FrameWork.Management.PublicTrans.Trans != null)
                {
                    this.extandManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                }
                string myOperCode = ((FS.HISFC.Models.Base.Employee)this.extandManager.Operator).ID;
                extendInfo.Item.ID = this.item.ID;
                extendInfo.OperEnvironment.ID = myOperCode;
                extendInfo.OperEnvironment.Memo = DateTime.Now.ToString("f");
                extendInfo.PropertyCode = "APPROVECHECK";
                extendInfo.PropertyName = "ҩƷ������Ϣ���";
                extendInfo.NumberProperty = 0;

                if (this.extandManager.SetComExtInfo(extendInfo) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// ���ÿؼ�����
        /// </summary>
        protected new void Focus()
        {
            this.txtName.Focus();
        }

        #endregion

        #region �鷽��

        /// <summary>
        /// ��ʼ���ؼ�
        /// </summary>
        protected virtual void Init()
        {
            try
            {
                #region ���ݳ�ʼ��

                //{6E41A9CD-AEDC-4aae-8E46-1F312F0FA4C6}  ҩ�����ü��ط�ʽ���
                if (this.alLevel1Function == null)
                {
                    this.alLevel1Function = this.itemConsManager.QueryPhaFunctionByLevel( 1 );
                }
                if (alLevel1Function == null)
                {
                    MessageBox.Show( "����һ��ҩ������ʧ��" + this.itemConsManager.Err );
                    return;
                }
                this.cmbPhyFunction1.AddItems( new ArrayList( alLevel1Function.ToArray() ) );

                if (this.alLevel2Function == null)
                {
                    this.alLevel2Function = this.itemConsManager.QueryPhaFunctionByLevel( 2 );
                }
                if (alLevel2Function == null)
                {
                    MessageBox.Show( "���ض���ҩ������ʧ��" + this.itemConsManager.Err );
                    return;
                }
                this.cmbPhyFunction2.AddItems( new ArrayList( alLevel2Function.ToArray() ) );

                if (this.alLevel3Function == null)
                {
                    this.alLevel3Function = this.itemConsManager.QueryPhaFunctionByLevel( 3 );
                }
                if (alLevel3Function == null)
                {
                    MessageBox.Show( "��������ҩ������ʧ��" + this.itemConsManager.Err );
                    return;
                }
                this.cmbPhyFunction3.AddItems( new ArrayList( alLevel3Function.ToArray() ) );
                //{6E41A9CD-AEDC-4aae-8E46-1F312F0FA4C6}  ҩ�����ü��ط�ʽ���

                this.cmbSysClass.AddItems(FS.HISFC.Models.Base.SysClassEnumService.List());
                this.cmbQuality.AddItems(consManager.GetList(FS.HISFC.Models.Base.EnumConstant.DRUGQUALITY));
                this.cmbDrugType.AddItems(consManager.GetList(FS.HISFC.Models.Base.EnumConstant.ITEMTYPE));
                this.cmbPackUnit.AddItems(consManager.GetList(FS.HISFC.Models.Base.EnumConstant.PACKUNIT));
                this.cmbDoseUnit.AddItems(consManager.GetList(FS.HISFC.Models.Base.EnumConstant.DOSEUNIT));
                this.cmbMinUnit.AddItems(consManager.GetList(FS.HISFC.Models.Base.EnumConstant.MINUNIT));
                this.cmbDosageForm.AddItems(consManager.GetList(FS.HISFC.Models.Base.EnumConstant.DOSAGEFORM));
                this.cmbPriceForm.AddItems(consManager.GetList(FS.HISFC.Models.Base.EnumConstant.PRICEFORM));
                this.cmbUsage.AddItems(consManager.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE));
                this.cmbMinFee.AddItems(consManager.GetList(FS.HISFC.Models.Base.EnumConstant.MINFEE));
                this.cmbFrequency.AddItems(frequencyManager.GetList("ROOT"));

                FS.HISFC.BizLogic.Pharmacy.Constant company = new FS.HISFC.BizLogic.Pharmacy.Constant();
                this.cmbCompany.AddItems(company.QueryCompany("1")); //������˾
                this.cmbTenderCompancy.AddItems(company.QueryCompany("1")); //������˾
                this.cmbProducer.AddItems(company.QueryCompany("0")); //��������

                this.cmbGrade.AddItems(consManager.GetList("DRUGGRADE")); //ҩƷ�ȼ� ��ҽ��ְ����Ӧ
                this.cmbStoreCondition.AddItems(consManager.GetList("STORECONDITION"));

                txtSplitType.SelectedIndex = 0;
                cmbShow.SelectedIndex = 0;


                this.SetCheckInfo();

                #endregion

                #region ��ʼ����չ����

                #region ��ȡ�����ļ�·��

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.Load(Application.StartupPath + "\\url.xml");

                System.Xml.XmlNode node = doc.SelectSingleNode("//dir");
                if (node == null)
                {
                    MessageBox.Show(Language.Msg("url����dir������"));
                }

                string serverPath = node.InnerText;
                string configPath = "//Config.xml"; //Զ�������ļ��� 

                #endregion

                //{5B0D15C2-3AFA-4535-AB33-A800B1CFB662}
                bool isCancelConfig = false;
                try
                {
                    doc.Load(serverPath + configPath);
                }
                catch (System.Net.WebException)
                {
                    isCancelConfig = true;
                }
                catch (System.IO.FileNotFoundException)
                {
                    isCancelConfig = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Language.Msg("װ��Config.xmlʧ�ܣ�\n" + ex.Message));
                }

                if (!isCancelConfig)
                {
                    System.Xml.XmlNode extend1Node = doc.SelectSingleNode("/Setting/Group[@ID='Pharmacy']/Fun[@ID='PharmacyExtend1']");
                    if (extend1Node != null)
                    {
                        //��չ�ֶ�ά�����Բ�ά���ֵ䳣������ֱ������{5F011DFA-2111-4553-AB36-27820A6F65FB}Visible�����ж�ֵ����ʹ��Width���������ơ�
                        this.lbExtend1.Text = extend1Node.Attributes["Display"].Value;
                        string constParm = extend1Node.Attributes["ConstantParam"].Value;
                        if (string.IsNullOrEmpty(constParm))
                        {
                            this.cmbExtend1.Width = 0;
                            this.txtExtend1.Width = 120;
                        }
                        else
                        {
                            this.cmbExtend1.Width = 120;
                            this.txtExtend1.Width = 0;
                            this.cmbExtend1.AddItems(consManager.GetList(constParm));
                        }
                    }

                    System.Xml.XmlNode extend2Node = doc.SelectSingleNode("/Setting/Group[@ID='Pharmacy']/Fun[@ID='PharmacyExtend2']");
                    if (extend2Node != null)
                    {
                        //��չ�ֶ�ά�����Բ�ά���ֵ䳣������ֱ������{5F011DFA-2111-4553-AB36-27820A6F65FB}
                        this.lbExtend2.Text = extend2Node.Attributes["Display"].Value;
                        //this.lbExtend2.Location = new System.Drawing.Point(547, 80);
                        string constParm = extend2Node.Attributes["ConstantParam"].Value;
                        if (string.IsNullOrEmpty(constParm))
                        {
                            this.cmbExtend2.Width = 0;
                            this.txtExtend2.Width = 139;
                        }
                        else
                        {
                            this.cmbExtend2.Width = 139;
                            this.txtExtend2.Width = 0;
                            this.cmbExtend2.AddItems(consManager.GetList(constParm));
                        }
                    }

                    System.Xml.XmlNode drugManagmentNode = doc.SelectSingleNode("/Setting/Group[@ID='Pharmacy']/Fun[@ID='DrugManagment']");
                    if (drugManagmentNode != null)
                    {
                        this.IsJudgeTradeName = FS.FrameWork.Function.NConvert.ToBoolean(drugManagmentNode.Attributes["IsJudgeTradeName"].Value);
                        this.lbWholePrice.Text = drugManagmentNode.Attributes["WholePriceDisplay"].Value;
                    }
                }

                #endregion

                #region ���Ʋ���

                //�Ƿ���Ҫ��� 500003
                FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                this.checkCtrl = ctrlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.NewDrug_Need_Approve, true, "0");

                #endregion

                this.cmbSysClass.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmbDrugType.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmbMinFee.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmbGrade.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmbQuality.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmbDosageForm.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmbPackUnit.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmbMinUnit.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmbDoseUnit.DropDownStyle = ComboBoxStyle.DropDownList;
                this.cmbSpecialFlag3.DropDownStyle = ComboBoxStyle.DropDownList;

            }
            catch { }
        }

        /// <summary>
        /// ��տؼ�
        /// </summary>
        protected virtual void Reset()
        {
            foreach (System.Windows.Forms.Control c in this.tpNormal.Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox))
                {
                    foreach (System.Windows.Forms.Control crl in c.Controls)
                    {
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuComboBox))
                        {
                            crl.Text = null;
                            crl.Tag = null;
                            continue;
                        }
                        if (crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuLabel) && crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                            continue;
                        }
                        if (crl.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            ((FS.FrameWork.WinForms.Controls.NeuCheckBox)crl).Checked = false;
                        }
                    }
                }
            }

            foreach (System.Windows.Forms.Control c in this.tpOther.Controls)
            {
                if (c.GetType() == typeof(FS.FrameWork.WinForms.Controls.NeuGroupBox))
                {
                    foreach (System.Windows.Forms.Control crl in c.Controls)
                    {
                        if (crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuLabel) && crl.GetType() != typeof(FS.FrameWork.WinForms.Controls.NeuCheckBox))
                        {
                            crl.Tag = "";
                            crl.Text = "";
                        }
                    }
                }
            }

            this.item = null;
        }

        /// <summary>
        /// �ر�
        /// </summary>
        protected virtual void Close()
        {
            if (this.FindForm() != null)
            {
                this.FindForm().Close();
            }
        }

        /// <summary>
        /// ҩƷ����
        /// </summary>
        /// <returns></returns>
        protected virtual bool SaveJudge()
        {
            //���������Ч��
            if (!this.DataIsValid( true ))
            {
                return false;
            }

            if (this.GetItem() == -1)
            {
                return false;
            }

            List<FS.HISFC.Models.Pharmacy.Item> al = this.itemManager.QueryValidDrugByCustomCode(this.item.UserCode);
            if (al == null)
            {
                MessageBox.Show(Language.Msg("�����Զ������ȡ��ЧҩƷ������������") + this.itemManager.Err);
                return false;
            }
            //{E49F9CEA-2E6D-4b2e-919F-99145BEE3E68}  Э��������λУ��
            if (this.item.IsNostrum)            //Э������
            {
                if (this.item.PackQty > 1)
                {
                    MessageBox.Show( Language.Msg( "Э����������ҩƷ��װ�������ܴ���1�����ٴμ��¼����" ) ,"��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Information );
                    return false;
                }
            }

            int validCount = al.Count;

            switch (this.inputType.Trim().ToUpper())
            {
                case "CHECK":
                    if (validCount > 0)
                    {
                        MessageBox.Show(Language.Msg("�Զ������Ϊ" + this.item.UserCode + "��ҩƷ\n�����ݿ��д�����Ч�ļ�¼\n��ҩƷ����ͨ�����"), "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                    else
                    {
                        DialogResult result;
                        result = MessageBox.Show(Language.Msg("ҩƷͨ����˺�ʱ��Ч��"), "ȷ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result == DialogResult.No)
                            return false;
                    }
                    break;
                case "UPDATE":
                    if (this.itemPrivValid == true && this.item.IsStop == false && validCount > 0)       //���ԭ��״̬Ϊͣ��,����ʱ��Ϊ��Ч,�ж����ݿ����Ƿ�����Ч��¼
                    {
                        MessageBox.Show(Language.Msg("�Զ������Ϊ" + this.item.UserCode + "��ҩƷ\n�����ݿ��д�����Ч�ļ�¼"), "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return false;
                    }
                    break;
                case "INSERT":
                    if (validCount > 0 && item.IsStop == false)
                    {
                        MessageBox.Show(Language.Msg("�Զ������Ϊ" + this.item.UserCode + "��ҩƷ\n�����ݿ��д�����Ч�ļ�¼"), "��ʾ��", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        this.txtUserCode.Focus();
                        this.txtUserCode.SelectAll();
                        return false;
                    }
                    break;
            }
            return true;
        }

        /// <summary>
        /// ����ؼ��е�ҩƷ����
        /// </summary>
        protected virtual int Save()
        {
            if (!this.SaveJudge())
            {
                return -1;
            }

            if (this.BeginSave != null)
            {
                this.BeginSave(this.item);            
            }

            if (this.GetItem() == -1)
            {
                return -1;
            }

            #region �������

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            itemManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.itemManager.SetItem(this.item) == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                if (this.inputType == "Insert")         //�ָ�ԭʼֵ ����������ͻ
                {
                    this.item.ID = "";
                }
                MessageBox.Show(Language.Msg("ҩƷ��Ϣ����ʱ �������� ") + this.itemManager.Err);
                return -1;
            }
            if (this.SaveCheck() == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                MessageBox.Show(Language.Msg("ҩƷ�����Ϣ����ʱ ��������") + this.itemManager.Err);
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            MessageBox.Show(Language.Msg("����ɹ�"));

            #region �����Ϣ����

            FS.HISFC.BizProcess.Integrate.Function funIntegrate = new FS.HISFC.BizProcess.Integrate.Function();

            bool isInsert = false;
            if (inputType.ToUpper() == "INSERT")
            {
                isInsert = true;
            }

            //�ų���������������С��λ����һ����ɵı����Ϣ��ȡ���ݲ���ȷ���� by Sunjh 2010-8-26 {D6D30303-8AB6-42d4-B35B-D76A4C16168F}
            if (this.item.BaseDose == this.originalItem.BaseDose)
            {
                this.originalItem.BaseDose = this.item.BaseDose;
            }

            funIntegrate.SaveChange<FS.HISFC.Models.Pharmacy.Item>(isInsert,false,this.item.ID,this.originalItem, this.item);

            #endregion

            #endregion

            if (this.EndSave != null)
            {
                this.EndSave( this.item );
            }

            return 1;
        }

        /// <summary>
        /// ����������������Լ������߼����Ƿ���Ч
        /// </summary>
        /// <param name="showMsg">�Ƿ񵯳�������ʾ ����ı�ؼ�����ɫ</param>
        /// <returns>��ЧΪTrue ��ЧΪFlase</returns>
        protected virtual bool DataIsValid(bool showMsg)
        {
            #region ���������������ж�

            if (this.isJudgeTradeName)
            {
                if (this.txtName.TextLength == 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ҩƷ���Ʋ���Ϊ��!"));
                    this.txtName.Focus();
                    return false;
                }
                if (this.txtSpellCode.TextLength == 0)
                {
                    MessageBox.Show(Language.Msg("ƴ���벻��Ϊ��!"));
                    this.txtSpellCode.Focus();
                    return false;
                }
                if (this.txtWbCode.TextLength == 0)
                {
                    MessageBox.Show(Language.Msg("����벻��Ϊ��!"));
                    this.txtWbCode.Focus();
                    return false;
                }
            }
            else
            {
                if (this.txtRegularName.TextLength == 0)
                {
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ͨ��������Ϊ��!"));
                    this.txtRegularName.Focus();
                    return false;
                }
                if (this.txtRegularSpellCode.TextLength == 0)
                {
                    MessageBox.Show(Language.Msg("ƴ���벻��Ϊ��!"));
                    this.txtRegularSpellCode.Focus();
                    return false;
                }
                if (this.txtRegularWbCode.TextLength == 0)
                {
                    MessageBox.Show(Language.Msg("����벻��Ϊ��!"));
                    this.txtRegularWbCode.Focus();
                    return false;
                }
            }
            if (this.cmbDrugType.Text == "" || this.cmbDrugType.Text == null)
            {
                MessageBox.Show(Language.Msg("ҩƷ�����Ϊ��!"));
                this.cmbDrugType.Focus();
                return false;
            }
            if (this.cmbSysClass.Text == "" || this.cmbSysClass.Text == null)
            {
                MessageBox.Show(Language.Msg("ϵͳ�����Ϊ��!"));
                this.cmbSysClass.Focus();
                return false;
            }
            if (this.cmbMinFee.Text == "" || this.cmbMinFee.Text == null)
            {
                MessageBox.Show(Language.Msg("��С���ò��ܿ�!"));
                this.cmbMinFee.Focus();
                return false;
            }
            if (this.txtSpecs.TextLength == 0)
            {
                MessageBox.Show(Language.Msg("�����Ϊ��!"));
                this.txtSpecs.Focus();
                return false;
            }
            if (this.txtPackQty.TextLength == 0 || this.txtPackQty.Text.Trim() == "0")
            {
                MessageBox.Show(Language.Msg("��װ��������Ϊ�ջ���0!"));
                this.txtPackQty.Focus();
                return false;
            }
            if (this.cmbPackUnit.Text == "" || this.cmbPackUnit.Text == null)
            {
                MessageBox.Show(Language.Msg("��װ��λ����Ϊ��!"));
                this.cmbPackUnit.Focus();
                return false;
            }
            if (this.cmbMinUnit.Text == "" || this.cmbMinUnit.Text == null)
            {
                MessageBox.Show(Language.Msg("��С��λ����Ϊ��!"));
                this.cmbMinUnit.Focus();
                return false;
            }
            if (this.txtBaseDose.TextLength == 0 || FS.FrameWork.Function.NConvert.ToDecimal(this.txtBaseDose.Text) == 0)
            {
                MessageBox.Show(Language.Msg("������������Ϊ�ջ�0!"));
                this.txtBaseDose.Focus();
                return false;
            }
            if (this.cmbDoseUnit.Text == "" || this.cmbDoseUnit.Text == null)
            {
                MessageBox.Show(Language.Msg("������λ����Ϊ��!"));
                this.cmbDoseUnit.Focus();
                return false;
            }
            if (this.cmbQuality.Text.Length == 0)
            {
                MessageBox.Show(Language.Msg("ҩƷ���ʲ���Ϊ��!"));
                this.cmbQuality.Focus();
                return false;
            }
            if (this.txtSplitType.Text.Length == 0)
            {
                MessageBox.Show(Language.Msg("������Բ���Ϊ��!"));
                this.txtSplitType.Focus();
                return false;
            }
            //by zlw 2006-4-14 ȥ��������Ҫ�ж��Զ�����벻��Ϊ��
            if (this.txtUserCode.Text.Length == 0)
            {
                MessageBox.Show(Language.Msg("ҩƷ�Զ�����벻��Ϊ��!"));
                this.txtUserCode.Focus();
                return false;
            }
            if (this.cmbDosageForm.Text.Length == 0)
            {
                MessageBox.Show(Language.Msg("���Ͳ���Ϊ��!"));
                this.cmbDosageForm.Focus();
                return false;
            }
            if (this.txtRetailPrice.Text.Length == 0 || this.txtRetailPrice.Text.Trim() == "0")
            {
                MessageBox.Show(Language.Msg("���ۼ۲���Ϊ�ջ���0!"));
                this.txtRetailPrice.Focus();
                return false;
            }
            if (this.chbIsStop.Checked)
            {
                if (this.txtStopReason.Text.Length == 0 || this.txtStopReason.Text.Trim() == "0")
                {
                    MessageBox.Show(Language.Msg("ͣ��ԭ����Ϊ��!"));
                    this.txtStopReason.Focus();
                    return false;
                }
            }

            //{2FA38C9F-567D-4e09-8046-C955E7C48467}�б꿪ʼʱ�䲻�ܴ��ڽ���ʱ��
            if (this.dtpBeginDate.Value.Date > this.dtpEndDate.Value.Date)
            {
                MessageBox.Show(Language.Msg("�б���ʼ���ڲ��ܴ��ڽ�������!"));
                this.txtStopReason.Focus();
                return false;
            }
            //{B9303CFE-755D-4585-B5EE-8C1901F79450}�ڶ�����۲���Ϊ0
            if (this.txtRetailPrice2.Text.Length == 0 || this.txtRetailPrice2.Text.Trim() == "0")
            {
                MessageBox.Show(Language.Msg("�ڶ����ۼ۲���Ϊ�ջ���0!"));
                this.txtRetailPrice2.Focus();
                return false;
            }

            #endregion

            #region ���������߼����ж�

            if (this.cmbPackUnit.Text == this.cmbMinUnit.Text && FS.FrameWork.Function.NConvert.ToDecimal(this.txtPackQty.Text) > 1)
            {
                MessageBox.Show(Language.Msg("��װ��λ������С��λʱ ��װ�����������1"));
                this.txtPackQty.Focus();
                return false;
            }
            if (this.cmbDoseUnit.Text == this.cmbMinUnit.Text && FS.FrameWork.Function.NConvert.ToDecimal(this.txtBaseDose.Text) > 1)
            {
                MessageBox.Show(Language.Msg("��С��λ���ڼ�����λʱ ��С�����������1"));
                this.txtBaseDose.Focus();
                return false;
            }
            if (this.cmbSpecialFlag3.SelectedIndex > 0)
            {
                if (this.cmbGrade.Tag == null || this.cmbGrade.Tag.ToString() == "")
                {
                    MessageBox.Show(Language.Msg("��������Ϊ ְ������ �� �������� ��ҩƷ ��������ҩƷ�ȼ�"));
                    this.cmbGrade.Focus();
                    return false;
                }
            }

            #endregion

            //{5C8A7DE7-7EC5-4625-9BF4-C56AC2AF1D08}
            #region ������У��

            if (FS.FrameWork.Function.NConvert.ToDecimal(this.txtPurchasePrice.Text) >
                FS.FrameWork.Function.NConvert.ToDecimal(this.txtRetailPrice.Text))
            {
                MessageBox.Show(Language.Msg("����۲�Ӧ�ô������ۼ�"));
                this.txtPurchasePrice.Focus();
                return false;
            }
            //{B9303CFE-755D-4585-B5EE-8C1901F79450}
            if (FS.FrameWork.Function.NConvert.ToDecimal(this.txtRetailPrice2.Text) >
               FS.FrameWork.Function.NConvert.ToDecimal(this.txtRetailPrice.Text))
            {
                MessageBox.Show(Language.Msg("�ڶ����ۼ۲�Ӧ�ô������ۼ�"));
                this.txtPurchasePrice.Focus();
                return false;
            }
            #endregion

            return true;
        }

        /// <summary>
        /// ��ȡ��Ʒ��ƴ���롢�����
        /// </summary>
        /// <param name="tradeName">��Ʒ��</param>
        /// <param name="drugTpye">ҩƷ���</param>
        /// <param name="drugQuality">ҩƷ����</param>
        /// <param name="dosageForm">ҩƷ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        protected virtual int GetTradeNameSpellCode(string tradeName, string drugTpye, string drugQuality, string dosageForm, ref string spellCode, ref string wbCode)
        {
            string strData = tradeName;

            bool isGetSpell = false;

            switch (this.drugAutoSpell)
            {
                case DrugAutoSpellType.TradeName:
                    if (this.txtName.Focused)
                    {
                        isGetSpell = true;
                    }

                    strData = tradeName;
                    break;
                case DrugAutoSpellType.DosageFormTradeName:
                    if (this.txtName.Focused || this.cmbDosageForm.Focused)
                    {
                        isGetSpell = true;
                    }

                    if (dosageForm != null)
                    {
                        strData = dosageForm + tradeName;
                    }
                    break;
                case DrugAutoSpellType.DrugQualityTradeName:
                    if (this.txtName.Focused || this.cmbQuality.Focused)
                    {
                        isGetSpell = true;
                    }

                    if (drugQuality != null)
                    {
                        strData = drugQuality + tradeName;
                    }
                    break;
                case DrugAutoSpellType.DrugTypeTradeName:
                    if (this.txtName.Focused || this.cmbDrugType.Focused)
                    {
                        isGetSpell = true;
                    }

                    if (drugTpye != null)
                    {
                        strData = drugTpye + tradeName;
                    }
                    break;
                case DrugAutoSpellType.TradeNameDosageForm:
                    if (this.txtName.Focused || this.cmbDosageForm.Focused)
                    {
                        isGetSpell = true;
                    }

                    if (dosageForm != null)
                    {
                        strData = tradeName + dosageForm;
                    }
                    break;
                case DrugAutoSpellType.TradeNameDrugQuality:
                    if (this.txtName.Focused || this.cmbQuality.Focused)
                    {
                        isGetSpell = true;
                    }

                    if (drugQuality != null)
                    {
                        strData = tradeName + drugQuality;
                    }
                    break;
                case DrugAutoSpellType.TradeNameDrugTpye:
                    if (this.txtName.Focused || this.cmbDrugType.Focused)
                    {
                        isGetSpell = true;
                    }

                    if (drugTpye != null)
                    {
                        strData = tradeName + drugTpye;
                    }
                    break;
            }

            if (isGetSpell)
            {
                spellCode = this.GetSpell(strData).SpellCode;
                wbCode = this.GetSpell(strData).WBCode;
            }

            return 1;
        }

        /// <summary>
        /// ���ô�����Ϣ��ʾ
        /// </summary>
        /// <param name="ctrl">���жϿؼ�</param>
        /// <param name="isTopMsg">�Ƿ񵯳���ʾ��Ϣ</param>
        /// <param name="errMsg">������Ϣ</param>
        private void SetErrMsg(System.Windows.Forms.Control ctrl,bool isTopMsg,string errMsg)
        {
            if (isTopMsg)
            {
                MessageBox.Show(errMsg);
                ctrl.Focus();
            }
            else
            {
                ctrl.BackColor = System.Drawing.Color.MistyRose;
                ctrl.Focus();
            }
        }

        #endregion

        #region ����TabPage��չ

        /// <summary>
        /// ��ά�����ڼ����Զ����uc 
        /// </summary>
        /// <param name="tbText">tabpage����</param>
        /// <param name="uc">������uc</param>
        /// <returns>�����¼ӵ�tabpage</returns>
        public TabPage AddTabPage(string tbText,System.Windows.Forms.UserControl uc)
        {
            TabPage tb = new TabPage(tbText);

            uc.Dock = DockStyle.Fill;

            tb.Controls.Add(uc);

            this.neuTabControl2.TabPages.Add(tb);

            return tb;
        }

        #endregion

        #region �¼�

        private void btnSave_Click(object sender, EventArgs e)
        {
            //����
            if (this.Save() == -1) return;

            switch (this.inputType.Trim().ToUpper())
            {
                case "CHECK":
                    return;
                case "UPDATE":
                    this.Close();
                    break;
                case "INSERT":
                    this.Reset();

                    if (!this.continueCheckBox.Checked)
                        this.Close();
                    break;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.JudgeEnter();
                SendKeys.Send("{TAB}");
                return true;
            }
            if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.S.GetHashCode())
            {
                if (this.btnSave.Visible && this.btnSave.Enabled)
                    this.btnSave_Click(null, null);
                return true;
            }
            if (keyData.GetHashCode() == Keys.Alt.GetHashCode() + Keys.C.GetHashCode())
            {
                if (this.btnCancel.Visible && this.btnCancel.Enabled)
                    this.btnCancel_Click(null, null);
                return true;
            }
            if (keyData == Keys.Escape)
            {
                if (this.btnCancel.Visible && this.btnCancel.Enabled)
                    this.btnCancel_Click(null, null);
                return true;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void chbIsStop_CheckedChanged(object sender, EventArgs e)
        {
            this.lblStopReason.Visible = this.chbIsStop.Checked;
            this.txtStopReason.Visible = this.chbIsStop.Checked;
        }

        private void txtDoseUnit_TextChanged(object sender, EventArgs e)
        {
            this.txtDoseShow.Text = this.cmbDoseUnit.Text;
        }

        #region  {6E41A9CD-AEDC-4aae-8E46-1F312F0FA4C6}  ҩ�����ü��ط�ʽ���

        private void cmbPhyFunction1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //���ö���ҩ������
            if (this.cmbPhyFunction1.Tag != null)
            {
                ArrayList alLevel2 = new ArrayList();
                foreach (FS.HISFC.Models.Pharmacy.PhaFunction info in this.alLevel2Function)
                {
                    if (info.ParentNode == this.cmbPhyFunction1.Tag.ToString())
                    {
                        alLevel2.Add( info.Clone() );
                    }
                }
                this.cmbPhyFunction2.AddItems( alLevel2 );
                this.cmbPhyFunction2.Tag = null;
                this.cmbPhyFunction2.Text = "";
            }
            //�������ҩ������
            this.cmbPhyFunction3.AddItems( new ArrayList() );
            this.cmbPhyFunction3.Tag = null;
            this.cmbPhyFunction3.Text = "";
        }

        private void cmbPhyFunction2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //���ö���ҩ������
            if (this.cmbPhyFunction2.Tag != null)
            {
                ArrayList alLevel3 = new ArrayList();
                foreach (   FS.HISFC.Models.Pharmacy.PhaFunction info in this.alLevel3Function)
                {
                    if (info.ParentNode == this.cmbPhyFunction2.Tag.ToString())
                    {
                        alLevel3.Add( info.Clone() );
                    }
                }
                this.cmbPhyFunction3.AddItems( alLevel3 );
                this.cmbPhyFunction3.Tag = null;
                this.cmbPhyFunction3.Text = "";
            }
        }

        private void cmbPhyFunction3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #endregion

        #region �ú�������ҩƷ��� ��Ҫ�����Ż���������

        /// <summary>
        /// 
        /// </summary>
        /// <param name="neuObject"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override int OnSetValue(object neuObject, TreeNode e)
        {
            FS.HISFC.Models.Pharmacy.Item item = e.Tag as FS.HISFC.Models.Pharmacy.Item;
            if (item != null)
            {
                this.Item = item;
                this.inputType = "Check";
                this.checkCtrl = "1";
                this.ReadOnly = true;
            }
            return base.OnSetValue(neuObject, e);
        }

        #endregion

        protected override int OnSave(object sender, object neuObject)
        {
            //return base.OnSave(sender, neuObject);
            return this.Save();
        }

        #region ����ҩƷͼƬ�Ĵ���

        //private void btnBrowse_Click(object sender, EventArgs e)
        //{
        //    System.Windows.Forms.OpenFileDialog flg = new OpenFileDialog();
        //    flg.CheckFileExists = true;
        //    flg.CheckPathExists = true;
        //    flg.Filter = "(JPGͼƬ)|*.jpg|(BMPͼƬ)|*.bmp";
        //    flg.Multiselect = false;
        //    flg.Title = "ҩƷ���ͼƬѡ��";
        //    flg.ShowDialog();

        //    try
        //    {
        //        if (System.IO.File.Exists(flg.FileName))
        //            this.pbImage.Image = System.Drawing.Image.FromFile(flg.FileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void btnImageClear_Click(object sender, EventArgs e)
        //{
        //    this.pbImage.Image = null;
        //}

        #endregion

        /// <summary>
        /// {773D56E7-4828-48d4-99C8-C80428112EBC}
        /// 
        /// ����ʽ���ַ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SpecsData_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.autoCreateSpecs) == false)
            {
                decimal baseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.txtBaseDose.Text);
                string doseUnit = this.cmbDoseUnit.Text;

                decimal packQty = FS.FrameWork.Function.NConvert.ToDecimal(this.txtPackQty.Text);
                string minUnit = this.cmbMinUnit.Text;
                string packUnit = this.cmbPackUnit.Text;

                this.txtSpecs.Text = string.Format(this.autoCreateSpecs, baseDose.ToString(), doseUnit, packQty, minUnit, packUnit);

            }
        }
    }

    /// <summary>
    /// ƴ���롢������Զ����ɷ�ʽ
    /// </summary>
    public enum DrugAutoSpellType
    { 
        TradeName,
        DosageFormTradeName,
        TradeNameDosageForm,
        TradeNameDrugTpye,
        DrugTypeTradeName,
        TradeNameDrugQuality,
        DrugQualityTradeName
    }
}
