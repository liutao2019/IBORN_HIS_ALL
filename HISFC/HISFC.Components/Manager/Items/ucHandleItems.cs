using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace FS.HISFC.Components.Manager.Items
{
    /// <summary>
    /// ����ɹ���Ļص�����ԭ��
    /// </summary>
    /// <param name="undrug"></param>
    public delegate void SaveSuccessHandler(FS.HISFC.Models.Fee.Item.Undrug undrug);
    public delegate void InsertSuccessHandler(FS.HISFC.Models.Fee.Item.Undrug undrug);

    public partial class ucHandleItems : UserControl
    {
        /// <summary>
        /// ����ɹ�ʱ���¼�
        /// </summary>
        public event SaveSuccessHandler SaveSuccessed;

        /// <summary>
        /// ����ɹ�ʱ���¼�
        /// </summary>
        public event InsertSuccessHandler InsertSuccessed;
        private bool canModifyPrice = false;
        /// <summary>
        /// Managerҵ���
        /// </summary>
        private FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
        /// <summary>
        /// true=insert; false=update
        /// </summary>
        private bool bNew;
        public bool IsNew
        {
            get
            {
                return this.bNew;
            }
        }
       // 2A5608D8-26AD-47d7-82CC-81375722FF72}
        /// <summary>
        /// ��������ݿ��м�����������ѡ�������Ϣ
        /// </summary>
        private string deptList = string.Empty;
        FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
        #region ����
        /// <summary>
        /// �Ƿ������޸ļ۸�
        /// </summary>
        public bool CanModifyPrice
        {
            get
            {
                return canModifyPrice;
            }
            set
            {
                canModifyPrice = value;
            }
        }

        #endregion 
        /// <summary>
        /// �������ĿʱΪtrue,�޸���ĿΪfalse;
        /// </summary>
        private bool isAddLine;
        public bool IsAddLine
        {
            get
            {
                return this.isAddLine;
            }
            set
            {
                this.isAddLine = value;
                if (value )
                {
                    this.tbUnitPrice.ReadOnly = false;
                    this.tbSpecialPrice.ReadOnly = false;
                    this.tbChildPrice.ReadOnly = false;
                }
                else 
                {
                    if (!this.canModifyPrice)
                    {
                        this.tbUnitPrice.ReadOnly = true;
                        this.tbSpecialPrice.ReadOnly = true;
                        this.tbChildPrice.ReadOnly = true;
                    }
                }
            }
        }

        #region {C5A4CFD7-EBDA-4908-9330-16D2E39A8435}
        private FS.HISFC.Models.Fee.Item.Undrug unDrugItem = null;

        public FS.HISFC.Models.Fee.Item.Undrug UnDrugItem
        {
            get { return unDrugItem; }
            set
            {
                unDrugItem = value;
                SetMaterialValue(unDrugItem);
            }
        }

        #endregion
        private void EditPrice(bool bShow)
        {
            
        }

        #region
        /// <summary>
        /// ��ҩƷ��Ŀҵ��
        /// </summary>
        private FS.HISFC.BizLogic.Fee.Item item = new FS.HISFC.BizLogic.Fee.Item();
        private Hashtable htSysType = new Hashtable();
        private Hashtable htFeeType = new Hashtable();
        private Hashtable htExecDept = new Hashtable();
        private FS.FrameWork.Public.ObjectHelper applicabilityAreaHelp = new FS.FrameWork.Public.ObjectHelper();
        #endregion

        //{C5A4CFD7-EBDA-4908-9330-16D2E39A8435}
        private void SetMaterialValue(FS.HISFC.Models.Fee.Item.Undrug UnDrugItem)
        {
            this.tbItemName.Text = UnDrugItem.Name;
            this.tbUnitPrice.Text= UnDrugItem.Price.ToString(); 
            this.tbSpecialPrice.Text=UnDrugItem.SpecialPrice.ToString();
            this.tbChildPrice.Text=UnDrugItem.ChildPrice.ToString();
            this.tbUnit.Text=UnDrugItem.PriceUnit;
            this.tbSpellCode.Text=UnDrugItem.SpellCode;
            this.tbWbCode.Text = UnDrugItem.WBCode;
        }

        public ucHandleItems(bool bNew)
        {
            InitializeComponent();
            this.Init();
            this.ClearForm();
            this.bNew = bNew;
            if (this.bNew)
            {
                GetUndrugItemNO();
            }
            //EditPrice(this.isAddLine);
        }

        private void ucHandleItems_Load(object sender, EventArgs e)
        {
            try
            {
                //FillSysClassType();
                //FillMinFee();
                //FillExecDept();
                this.chkSeries.Visible = false;//ԭ����,��ʱ����,����ɼ�,���ʾ�Ƿ������������
                // 2A5608D8-26AD-47d7-82CC-81375722FF72}
                #region ���ÿؼ���ʾ
                this.neuLabel36.Visible = false;
                this.txtKfDept.Visible = false;
                this.btn_Add.Visible = false;
                this.neuLabel37.Visible = false;
                this.cmbItemPriceType.Visible = false;
                this.chkOrderPrint.Visible = false;
                bool val = (bool)ctrlParamIntegrate.GetControlParam("201026",true,true);
                if (val)
                {
                    this.neuLabel36.Visible = true;
                    this.btn_Add.Visible = true;
                }
                bool val1 = ctrlParamIntegrate.GetControlParam<bool>("B00001", true, true);
                bool val2 = ctrlParamIntegrate.GetControlParam<bool>("B00002", true, true);
                if (val1)
                {
                    this.neuLabel37.Visible = true;
                    this.cmbItemPriceType.Visible = true;
                }
                if (val2)
                {
                    this.chkOrderPrint.Visible = true;
                }
                #endregion
            }
            catch
            {
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.FindForm().Close();
        }

        #region ��ʼ��ʱʹ��
        /// <summary>
        /// ���SysType�ؼ�
        /// </summary>
        private void FillSysClassType()
        {
            ArrayList alSysType = FS.HISFC.Models.Base.SysClassEnumService.List();
            if (alSysType == null)
            {
                return;
            }

            for (int i = 0, j = alSysType.Count; i < j; i++)
            {
                this.htSysType.Add(((FS.FrameWork.Models.NeuObject)alSysType[i]).ID, ((FS.FrameWork.Models.NeuObject)alSysType[i]).Name);
            }

            this.cbClassType.AddItems(alSysType);
            this.cmbItemPriceType.AddItems(managerIntegrate.GetConstantList("ITEMPRICETYPE"));
        }

        /// <summary>
        /// ��ȡ����ִ�п���
        /// </summary>
        private void FillExecDept()
        {
            FS.HISFC.BizLogic.Manager.Department dept = new FS.HISFC.BizLogic.Manager.Department();

            ArrayList alExecDept = dept.GetDeptmentAll();

            if (alExecDept == null)
            {
                return;
            }

            for (int i = 0, j = alExecDept.Count; i < j; i++)
            {
                this.htExecDept.Add(((FS.FrameWork.Models.NeuObject)alExecDept[i]).ID, ((FS.FrameWork.Models.NeuObject)alExecDept[i]).Name);
            }

            this.cbExecDept.AddItems(alExecDept);
        }

        /// <summary>
        /// ��ȡ��鲿λ����������{A5F28DE2-11D0-4d40-90BC-75FD0FFF17A1}
        /// </summary>
        private void FillSample()
        {
            FS.HISFC.BizLogic.Manager.Constant cons = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList CheckList = cons.GetAllList("CHECKPART");//��鲿λ
            ArrayList helpSample = cons.GetAllList("LABSAMPLE");//��������
            ArrayList checkAndSample = new ArrayList();
            checkAndSample.AddRange(CheckList);
            checkAndSample.AddRange(helpSample);
            this.tbSample.AddItems(checkAndSample);
        }

        /// <summary>
        /// �����С���ô���ؼ�
        /// </summary>
        private void FillMinFee()
        {
            FS.HISFC.BizLogic.Manager.Constant cons = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alMinFee = cons.GetAllList(FS.HISFC.Models.Base.EnumConstant.MINFEE);
            if (alMinFee == null)
            {
                return;
            }
            for (int i = 0, j = alMinFee.Count; i < j; i++)
            {
                this.htFeeType.Add(((FS.FrameWork.Models.NeuObject)alMinFee[i]).ID, ((FS.FrameWork.Models.NeuObject)alMinFee[i]).Name);
            }

            this.cbMinFee.AddItems(alMinFee);
            ArrayList applicabilityArea = cons.GetAllList("APPLICABILITYAREA");
            if (applicabilityArea == null)
            {
                MessageBox.Show("��ȡ���÷�Χʧ�� " + cons.Err);
            }
            applicabilityAreaHelp.ArrayObject = applicabilityArea;
            cmbApplicabilityArea.AddItems(applicabilityArea);
            if (cmbApplicabilityArea.Items.Count > 0)
            {
                this.cmbApplicabilityArea.SelectedIndex = 0;
            }
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.ValidateInputString() == -1)
            {
                return;
            }

            FS.HISFC.Models.Fee.Item.Undrug undrug = CreateUndrugItem();

            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            this.item.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            if (this.bNew)
            {
                if (this.item.InsertUndrugItem(undrug) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������ʧ��!" + this.item.Err), "��Ϣ");
                    return;
                }
            }
            else
            {
                if (this.item.UpdateUndrugItem(undrug) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();;
                    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��������ʧ��!" + this.item.Err), "��Ϣ");
                    return;
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();;
            MessageBox.Show(FS.FrameWork.Management.Language.Msg("�������ݳɹ�!"), "��Ϣ");

            if (this.bNew)
            {
                if (InsertSuccessed != null)
                {
                    InsertSuccessed(undrug);
                }
            }
            else
            {
                if (SaveSuccessed != null)
                {
                    SaveSuccessed(undrug);
                }
            }
            
            this.ClearForm();
            //if (!this.chkSeries.Checked)
            //{
            //    this.FindForm().Close();
            //}
            this.FindForm().Close();
        }


        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void tbItemName_KeyDown(object sender, KeyEventArgs e)
        {
            //��������
            FS.HISFC.BizLogic.Manager.Spell spell = new FS.HISFC.BizLogic.Manager.Spell();
            FS.HISFC.Models.Base.ISpell ispell = spell.Get(this.tbItemName.Text);

            this.tbSpellCode.Text = ispell.SpellCode;
            this.tbWbCode.Text = ispell.WBCode;
        }

        /// <summary>
        /// ��֤����Ϊ�յ����Ƿ�Ϊ��
        /// </summary>
        /// <returns>1,�ɹ�����-1,ʧ��</returns>
        private int ValidateInputString()
        {
            if (this.tbItemName.Text == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ŀ���Ʋ���Ϊ��!"), "��Ϣ");
                this.tbItemName.Focus();
                return -1;
            }
            if (this.cbClassType.Text == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("ϵͳ�����Ϊ��!"), "��Ϣ");
                this.cbClassType.Focus();
                return -1;
            }
            if (this.cbValid.Text == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��Ч�Բ���Ϊ��!"), "��Ϣ");
                this.cbValid.Focus();
                return -1;
            }
            //if (this.cbExecDept.Text == "")
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ִ�п��Ҳ���Ϊ��!"), "��Ϣ");
            //    this.cbExecDept.Focus();
            //    return -1;
            //}
            if (this.cbMinFee.Text == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��С���������Ϊ��!"), "��Ϣ");
                this.cbMinFee.Focus();
                return -1;
            }
            //if (this.tbSample.Text == "")
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("��鲿λ��걾����Ϊ��!"), "��Ϣ");
            //    this.tbSample.Focus();
            //    return -1;
            //}
            if (this.tbUnit.Text == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("��λ����Ϊ��!"), "��Ϣ");
                this.tbUnit.Focus();
                return -1;
            }
            //if (this.tbSpec.Text == "")
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����Ϊ��!"), "��Ϣ");
            //    this.tbSpec.Focus();
            //    return -1;
            //}
            if (this.tbUserCode.Text == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�Զ����벻��Ϊ��!"), "��Ϣ");
                this.tbUserCode.Focus();
                return -1;
            }
            //if (this.tbSpellCode.Text == "")
            //{
            //    MessageBox.Show(FS.FrameWork.Management.Language.Msg("ƴ���벻��Ϊ��!"), "��Ϣ");
            //    this.tbSpellCode.Focus();
            //    return -1;
            //}
            if (FS.FrameWork.Function.NConvert.ToInt32(tbUnitPrice.Text) <= 0)
            {
                if (MessageBox.Show(FS.FrameWork.Management.Language.Msg("Ĭ�ϼ�Ϊ0���Ƿ������"), "��Ϣ", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    tbUnitPrice.Focus();
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// ��HashTable�и���value�õ���ֵ
        /// </summary>
        /// <param name="ht">Ŀ���ϣ��</param>
        /// <param name="value">ֵ</param>
        /// <returns>�ɹ�:��, ʧ��:""</returns>
        private string GetKeyByValue(Hashtable ht, string value)
        {
            string key = "";
            foreach (DictionaryEntry de in ht)
            {
                if (de.Value.ToString() == value)
                {
                    key = de.Key.ToString();
                    break;
                }
            }
            return key;
        }
        
        /// <summary>
        /// �����÷�ʹ��,��ʼ������combox�ؼ�.  (�Ժ��ٸ�)
        /// </summary>
        private void Init()
        {
            this.FillSysClassType();
            this.FillMinFee();
            this.FillExecDept();
            this.FillSample();//{A5F28DE2-11D0-4d40-90BC-75FD0FFF17A1}
        }

        /// <summary>
        /// �������ֵ
        /// </summary>
        private void ClearForm()
        {
            this.tbItemCode.Text = "";
            this.tbItemName.Text = "";
            this.cbClassType.Text = "";
            this.cbValid.Text = "";
            this.cbExecDept.Text = "";
            this.tbItemRestrict.Text = "";
            this.tbNotice.Text = "";
            this.tbItemArea.Text = "";

            this.tbOtherPrice1.NumericValue = 0;
            this.tbOtherPrice2.NumericValue = 0;
            this.tbChildPrice.NumericValue = 0;
            this.tbSpecialPrice.NumericValue = 0;
            this.tbEmergePrice.NumericValue = 0;
            this.tbUnitPrice.NumericValue = 0;

            this.tbCheck.Text = "";
            this.cbMinFee.Text = "";
            this.tbSample.Text = "";
            this.tbUnit.Text = "";
            this.tbSpec.Text = "";
            this.tbApplyName.Text = "";
            this.tbIllSort.Text = "";
            this.tbOprCode.Text = "";
            this.tbOprSort.Text = "";
            this.tbOprScale.Text = "";
            this.tbAcademyName.Text = "";
            this.tbMachineNO.Text = "";

            this.tbUserCode.Text = "";
            this.tbSpellCode.Text = "";
            this.tbWbCode.Text = "";
            this.tbNationCode.Text = "";
            this.tbIntCode.Text = "";
            this.tbMedical.Text = "";
            this.tbMemo.Text = "";

            this.chkProvince.Checked = false;
            this.chkTown.Checked = false;
            this.chkSelf.Checked = false;
            this.chkConfirm.Checked = false;
            this.chkPrecontract.Checked = false;
            this.chkFamilyPlane.Checked = false;
            this.chkSpecialItem.Checked = false;
            this.chkConsent.Checked = false;
            this.chkCollate.Checked = false;
            this.chkFirst.Checked = false;
            this.chkSecond.Checked = false;
            this.chkThird.Checked = false;

            //��λ��ʶ(0,��ϸ; 1,����)[2007/01/01  xuweizhe]
            this.chkUnitFlag.Checked = false;

            //undrug.SpecialFlag4 = "" /*��ɽһʹ��,�Ƿ�ǿ�Ƴ���*/;//33
        }

        private FS.HISFC.Models.Fee.Item.Undrug CreateUndrugItem()
        {
            FS.HISFC.Models.Fee.Item.Undrug undrug = new FS.HISFC.Models.Fee.Item.Undrug();
            #region
            // oper_code='{26}' , 
            // oper_date =sysdate,
            // UNIT_PRICE1 ={27}, 
            // UNIT_PRICE2 ={28}, 
            // SPECIAL_FLAG ='{29}',
            // SPECIAL_FLAG1='{30}' ,
            // SPECIAL_FLAG2='{31}' ,
            // SPECIAL_FLAG3 ='{32}',
            // SPECIAL_FLAG4 ='{33}',
            // UNIT_PRICE3={34},
            // UNIT_PRICE4={35},
            // DISEASE_CLASS=  '{36}',
            // SPECIAL_DEPT  ='{37}',
            // CONSENT_FLAG = '{38}',
            // MARK1 = '{39}' ,
            // MARK2 = '{40}' ,
            // MARK3 = '{41}' ,
            // MARK4 = '{42}' ,   --  ������뵥����  
            // needbespeak  = '{43}' ,-- �Ƿ���ҪԤԼ
            // ITEM_AREA = '{44}' , -- ��Ŀ��Χ
            // ITEM_NOAREA = '{45}' --��Ŀ���� 
            // WHERE PARENT_CODE = fun_get_parentcode  AND CURRENT_CODE= fun_get_currentcode  AND item_code   ='{0}' 
            #endregion
            undrug.ID = this.tbItemCode.Text;//0
            undrug.Name = this.tbItemName.Text;//1

            switch (this.cbValid.Text.Trim())//16
            {
                case "����":
                    undrug.ValidState = "1";
                    break;
                case "ͣ��":
                    undrug.ValidState = "0";
                    break;
                case "����":
                    undrug.ValidState = "2";
                    break;
                default:
                    undrug.ValidState = "1";
                    break;
            }

            undrug.SysClass.ID = this.cbClassType.Text == "" ? "" : GetKeyByValue(this.htSysType, this.cbClassType.Text);//2
            undrug.ExecDept = this.cbExecDept.Text == "" ? "" : GetKeyByValue(this.htExecDept, this.cbExecDept.Text);//18
            undrug.MinFee.ID = this.cbMinFee.Text == "" ? "" : GetKeyByValue(this.htFeeType, this.cbMinFee.Text);//3----------

            undrug.ItemException = this.tbItemRestrict.Text;//45
            undrug.Notice = this.tbNotice.Text;//41
            undrug.ItemArea = this.tbItemArea.Text;//44

            #region
            // unit_price='{9}', 
            // stock_unit='{10}',    
            // emerg_scale='{11}',
            // family_plane='{12}',  
            // special_item='{13}',  
            // item_grade  ='{14}', 
            // confirm_flag='{15}',
            // valid_state='{16}',  
            // specs='{17}',	 
            // facility_no='{19}',
            // default_sample='{20}', 
            // operate_code='{21}', 
            // operate_kind='{22}', 
            // operate_type='{23}',  
            // collate_flag='{24}', 
            // mark='{25}',
            #endregion

            undrug.CheckRequest = this.tbCheck.Text;//40
            
            //undrug.CheckBody = this.tbSample.Text;//20
            if (this.tbSample.Tag != string.Empty)//{A5F28DE2-11D0-4d40-90BC-75FD0FFF17A1}
            {
                undrug.CheckBody = this.tbSample.Text;
            }
            //undrug.CheckBody = this.tbSample.Tag.ToString();
            undrug.Price = (decimal)this.tbUnitPrice.NumericValue;//9
            undrug.ChildPrice = (decimal)this.tbChildPrice.NumericValue;//27
            undrug.SpecialPrice = (decimal)this.tbSpecialPrice.NumericValue;//28
            undrug.FTRate.EMCRate = (decimal)this.tbEmergePrice.NumericValue;//11
            
            //������1
            //������2

            undrug.PriceUnit = this.tbUnit.Text;//10
            undrug.Specs = this.tbSpec.Text;//17
            undrug.CheckApplyDept = this.tbApplyName.Text;//42
            undrug.DiseaseType.ID = this.tbIllSort.Text;//36
            undrug.OperationInfo.ID = this.tbOprCode.Text;//21
            undrug.OperationType.ID = this.tbOprSort.Text;//22
            undrug.OperationScale.ID = this.tbOprScale.Text;//23
            undrug.SpecialDept.ID = this.tbAcademyName.Text;//37
            undrug.MachineNO = this.tbMachineNO.Text;//19

            #region
            //fee_code ='{3}',
            #endregion

            //������,[2006/12/15, xuweizhe]��,����������
            undrug.UserCode = this.tbUserCode.Text;//4
            undrug.SpellCode = this.tbSpellCode.Text;//5
            undrug.WBCode = this.tbWbCode.Text;//6

            undrug.GBCode = this.tbNationCode.Text;//7
            undrug.NationCode = this.tbIntCode.Text;//8
            undrug.MedicalRecord = this.tbMedical.Text;//39
            undrug.Memo = this.tbMemo.Text;//25
            if (cmbApplicabilityArea.Tag != null)
            {
                undrug.ApplicabilityArea = cmbApplicabilityArea.Tag.ToString();
            } 
            undrug.SpecialFlag = this.chkProvince.Checked ? "1" : "0";//29
            undrug.SpecialFlag1 = this.chkTown.Checked ? "1" : "0";//30
            undrug.SpecialFlag2 = this.chkSelf.Checked ? "1" : "0";//31
            undrug.IsNeedConfirm = this.chkConfirm.Checked;//15
            undrug.IsNeedBespeak = this.chkPrecontract.Checked /*�Ƿ���ҪԤԼ*/;//43
            undrug.IsFamilyPlanning = this.chkFamilyPlane.Checked /*�ƻ��������*/;//12

            undrug.SpecialFlag3 = this.chkSpecialItem.Checked ? "1" : "0";//32
            undrug.SpecialFlag4 = "1" /*��ɽһʹ��,�Ƿ�ǿ�Ƴ���*/;//33
            undrug.IsConsent = this.chkConsent.Checked /*֪��ͬ����*/;//38
            undrug.IsCompareToMaterial = this.chkCollate.Checked /*����*/;//24

            undrug.Grade = "";
            if (this.chkFirst.Checked)
            {
                undrug.Grade = "1";
            }
            if (this.chkSecond.Checked)
            {
                undrug.Grade = "2";
            }
            if (this.chkThird.Checked)
            {
                undrug.Grade = "3";
            }

            //��λ��ʶ(0,��ϸ; 1,����)[2007/01/01 ]
            undrug.UnitFlag = this.chkUnitFlag.Checked ? "1" : "0";
            //����Ա
            undrug.Oper.ID = item.Operator.ID;
            //{2A5608D8-26AD-47d7-82CC-81375722FF72}
            if (this.txtKfDept.Text == string.Empty)
            {
                undrug.DeptList = "ALL";
            }
            else
            {
                undrug.DeptList = this.txtKfDept.Text;
            }
            //{55CFCB36-B084-4a56-95AD-2CDED962ADC4}
            undrug.ItemPriceType = this.cmbItemPriceType.Text;
            if (this.chkOrderPrint.Checked)
            {
                undrug.IsOrderPrint = "1";
            }
            else
            {
                undrug.IsOrderPrint = "0";
            }
            return undrug;
        }

        public void UpdateUndrugItems(FS.HISFC.Models.Fee.Item.Undrug undrug)
        {
            #region
            // oper_code='{26}' , 
            // oper_date =sysdate,
            // UNIT_PRICE1 ={27}, 
            // UNIT_PRICE2 ={28}, 
            // SPECIAL_FLAG ='{29}',
            // SPECIAL_FLAG1='{30}' ,
            // SPECIAL_FLAG2='{31}' ,
            // SPECIAL_FLAG3 ='{32}',
            // SPECIAL_FLAG4 ='{33}',
            // UNIT_PRICE3={34},
            // UNIT_PRICE4={35},
            // DISEASE_CLASS=  '{36}',
            // SPECIAL_DEPT  ='{37}',
            // CONSENT_FLAG = '{38}',
            // MARK1 = '{39}' ,
            // MARK2 = '{40}' ,
            // MARK3 = '{41}' ,
            // MARK4 = '{42}' ,   --  ������뵥����  
            // needbespeak  = '{43}' ,-- �Ƿ���ҪԤԼ
            // ITEM_AREA = '{44}' , -- ��Ŀ��Χ
            // ITEM_NOAREA = '{45}' --��Ŀ���� 
            // WHERE PARENT_CODE = fun_get_parentcode  AND CURRENT_CODE= fun_get_currentcode  AND item_code   ='{0}' 
            #endregion

            this.tbItemCode.Text = undrug.ID;//0
            this.tbItemName.Text = undrug.Name;//1
            this.tbItemRestrict.Text = undrug.ItemException;//45
            this.tbNotice.Text = undrug.Notice;//41
            this.tbItemArea.Text = undrug.ItemArea;//44

            this.cbClassType.Text = undrug.SysClass.Name;//2
            this.cbExecDept.Text = undrug.ExecDept; //== "" ? "" : this.htExecDept[undrug.ExecDept].ToString();//18
            this.cbMinFee.Text = undrug.MinFee.Name;//3----------
            this.cbValid.Text = undrug.ValidState;//16
            #region
            // unit_price='{9}', 
            // stock_unit='{10}',    
            // emerg_scale='{11}',
            // family_plane='{12}',  
            // special_item='{13}',  
            // item_grade  ='{14}', 
            // confirm_flag='{15}',
            // valid_state='{16}',  
            // specs='{17}',	 
            // facility_no='{19}',
            // default_sample='{20}', 
            // operate_code='{21}', 
            // operate_kind='{22}', 
            // operate_type='{23}',  
            // collate_flag='{24}', 
            // mark='{25}',
            #endregion

            this.tbUnitPrice.NumericValue = undrug.Price;//9
            this.tbChildPrice.NumericValue = undrug.ChildPrice;//27
            this.tbSpecialPrice.NumericValue = undrug.SpecialPrice;//28
            this.tbEmergePrice.NumericValue = undrug.FTRate.EMCRate;//11
            this.tbOtherPrice1.NumericValue = 0;//34
            this.tbOtherPrice2.NumericValue = 0;//35

            this.tbCheck.Text = undrug.CheckRequest;//40
            this.tbSample.Text = undrug.CheckBody;//{A5F28DE2-11D0-4d40-90BC-75FD0FFF17A1}
            //this.tbSample.Text = undrug.CheckBody;//20
            this.tbUnit.Text = undrug.PriceUnit;//10
            this.tbSpec.Text = undrug.Specs;//17
            this.tbApplyName.Text = undrug.CheckApplyDept;//42
            this.tbIllSort.Text = undrug.DiseaseType.ID;//36
            this.tbOprCode.Text = undrug.OperationInfo.ID;//21
            this.tbOprSort.Text = undrug.OperationType.ID;//22
            this.tbOprScale.Text = undrug.OperationScale.ID;//23
            this.tbAcademyName.Text = undrug.SpecialDept.ID;//37
            this.tbMachineNO.Text = undrug.MachineNO;//19

            #region
            //fee_code ='{3}',
            #endregion

            this.tbUserCode.Text = undrug.UserCode;//4
            this.tbSpellCode.Text = undrug.SpellCode;//5
            this.tbWbCode.Text = undrug.WBCode;//6
            this.tbNationCode.Text = undrug.GBCode;//7
            this.tbIntCode.Text = undrug.NationCode;//8
            this.tbMedical.Text = undrug.MedicalRecord;//39
            this.tbMemo.Text = undrug.Memo;//25
            this.cmbApplicabilityArea.Tag = undrug.ApplicabilityArea;
            if (undrug.SpecialFlag /*ʡ����*/ == "1")//29
            {
                this.chkProvince.Checked = true;
            }
            if (undrug.SpecialFlag1 /*������*/ == "1")//30
            {
                this.chkTown.Checked = true;
            }
            if (undrug.SpecialFlag2 /*�Է���Ŀ*/ == "1")//31
            {
                this.chkSelf.Checked = true;
            }
            this.chkConfirm.Checked = undrug.IsNeedConfirm/*�ն�ȷ��*/;//15
            this.chkPrecontract.Checked = undrug.IsNeedBespeak/*�Ƿ���ҪԤԼ*/;//43
            this.chkFamilyPlane.Checked = undrug.IsFamilyPlanning/*�ƻ��������*/;//12

            if (undrug.SpecialFlag3 /*�ض�������Ŀ*/ == "1")//32
            {
                this.chkSpecialItem.Checked = true;
            }
            undrug.SpecialFlag4 = "1" /*��ɽһʹ��,�Ƿ�ǿ�Ƴ���*/;//33
            this.chkConsent.Checked = undrug.IsConsent/*֪��ͬ����*/;//38
            this.chkCollate.Checked = undrug.IsCompareToMaterial/*����*/;//24
            
            #region {2A5608D8-26AD-47d7-82CC-81375722FF72}
            if (undrug.DeptList == "ALL")
            {
                this.txtKfDept.Text = "";
            }
            else
            {
                this.txtKfDept.Text = undrug.DeptList;
            }
            this.deptList = this.txtKfDept.Text;
            //{55CFCB36-B084-4a56-95AD-2CDED962ADC4}
            this.cmbItemPriceType.Text = undrug.ItemPriceType;
            this.chkOrderPrint.Checked = FS.FrameWork.Function.NConvert.ToBoolean(undrug.IsOrderPrint);
            #endregion
            switch (undrug.Grade.Trim())
            {
                case "��":
                    this.chkFirst.Checked = true;
                    break;
                case "��":
                    this.chkSecond.Checked = true;
                    break;
                case "��":
                    this.chkThird.Checked = true;
                    break;
                default: break;
            }

            //��λ��ʶ(0,��ϸ; 1,����)[2007/01/01  xuweizhe]
            this.chkUnitFlag.Checked = undrug.UnitFlag.Trim().Equals("1") ? true : false;
        }
        
        /// <summary>
        /// �²�����ʱ���õ�һ������ˮ��
        /// </summary>
        private void GetUndrugItemNO()
        {
            this.tbItemCode.Text = this.item.GetUndrugCode();
        }

        private void chkPrecontract_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkPrecontract.Checked)
            {
                this.chkConfirm.Checked = true;
            }
        }

        private void chkConfirm_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkConfirm.Checked)
            {
                chkPrecontract.Checked = false;
            }
        }

        private void tbItemName_TextChanged(object sender, EventArgs e)
        {
            //��������
            FS.HISFC.BizLogic.Manager.Spell spell = new FS.HISFC.BizLogic.Manager.Spell();
            FS.HISFC.Models.Base.ISpell ispell = spell.Get(this.tbItemName.Text);
            //��ƴ����������ȡǰ8λ�����棬�����ʱ��Ҳ��8λ
            if (ispell == null)
            {
                this.tbSpellCode.Text = "";
                return;
            }

            if (ispell.SpellCode != null && ispell.SpellCode.Length > 8)
            {
                this.tbSpellCode.Text = ispell.SpellCode.Substring(0, 8);
            }
            else
            {
                this.tbSpellCode.Text = ispell.SpellCode;
            }

            if (ispell.SpellCode != null && ispell.WBCode.Length > 8)
            {
                this.tbWbCode.Text = ispell.WBCode.Substring(0, 8);
            }
            else
            {
                this.tbWbCode.Text = ispell.WBCode;
            }
        }
        //{2A5608D8-26AD-47d7-82CC-81375722FF72}feng.ch����ά��������Ϣ��ʱ��ά�����Կ�������
        /// <summary>
        /// ��ӿ��Կ������ң������ѡ�����п��Ҷ����Կ����������Ŀ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_Add_Click(object sender, EventArgs e)
        {
            Forms.FrmSetDeptForItem frm = new FS.HISFC.Components.Manager.Forms.FrmSetDeptForItem();
            frm.DeptStr = this.deptList;
            frm.ShowDialog();
            if (frm.Rs==DialogResult.OK)
            {
                string chooseDeptList="";
                chooseDeptList = frm.ChooseDeptList;
                if (chooseDeptList.Length <= 0)
                {
                    this.txtKfDept.Text = "";
                    return;
                }
                this.txtKfDept.Text = chooseDeptList;
            }
            else
            {
            }
        }
    }
}