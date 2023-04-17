using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FS.HISFC.Components.DrugStore.Outpatient
{
    /// <summary>
    /// [��������: �����䷢ҩ�ؼ�]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-11]<br></br>
    /// <˵��>
    ///     1��ʹ�ñ��ؼ�ʱ ���ȵ��ó�ʼ��Init����
    ///                     ����OperDept OperInfo  ����
    ///     2��ʵ�ֽӿ�IOutpatientShow ���Զ��廼����ʾ��ʽ  ʹ��ʱ��̳�ucBaseControl ʵ�ֽӿ�IOutpatientShow
    ///                     �����ʵ�� �����Ĭ�Ϸ�ʽ��ʾ
    /// </˵��>
    /// <�޸ļ�¼ 
    ///	 ֱ�ӷ�ҩ �б���ص����ڵ�״̬Ϊ " 1 "	
    /// 
    ///  />
    /// </summary>
    public partial class ucClinicDrug : DrugStore.Outpatient.ucClinicBase,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        public ucClinicDrug()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ����ǰ
        /// </summary>
        public new event System.EventHandler BeginSave;

        /// <summary>
        /// �����
        /// </summary>
        public new event System.EventHandler EndSave;

        /// <summary>
        /// ����ʾ����ʾ��Ϣ
        /// </summary>
        public event System.EventHandler MessageEvent;

        #region ������

        /// <summary>
        /// Ƶ�ΰ�����
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper frequencyHelper = null;

        /// <summary>
        /// �÷�������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper usageHelper = null;

        /// <summary>
        /// ��Ա������
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper personHelper = null;

        /// <summary>
        /// ���Ұ�����
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper deptHelper = null;

        /// <summary>
        /// �ն���Ϣ
        /// </summary>
        protected FS.FrameWork.Public.ObjectHelper terminalHelper = null;

        #endregion

        #region �����

        /// <summary>
        /// �Բ�ҩ����ʾ �Ƿ�ͬʱ��ʾ����
        /// </summary>
        private bool isShowDays = false;

        /// <summary>
        /// �Ƿ��ӡ����
        /// </summary>
        private bool isPrintRecipe = false;

        /// <summary>
        /// �Ƿ��ӡ��ҩ�嵥
        /// </summary>
        private bool isPrintListing = false;

        /// <summary>
        /// ���δ���Ĵ���������Ϣ
        /// </summary>
        private FS.HISFC.Models.Pharmacy.DrugRecipe tempDrugRecipe = null;

        /// <summary>
        /// �Ƿ���ʾ������ϸ��Ϣ
        /// </summary>
        private bool isPatientDetail = false;

        /// <summary>
        /// �Ƿ�����ҩȷ��ʱ���´���������Ϣ
        /// </summary>
        private bool isAdjustInDrug = true;

        /// <summary>
        /// ��汨��ʱ�Ƿ��ж�����
        /// </summary>
        private bool judgeWarnPreStore = true;

        /// <summary>
        /// ��汨��ʱ�Ƿ��ÿ�������ж�
        /// </summary>
        private bool judgeWarnLowQty = true;

        /// <summary>
        /// ������ҩʱ�Ƿ���п�澯���ж�
        /// </summary>
        private bool isJudgeWarDruged = false;

        /// <summary>
        /// ���﷢ҩʱ�Ƿ���п�澯���ж�
        /// </summary>
        private bool isJudgeWarnSend = false;

        #endregion

        #region ����

        /// <summary>
        /// �Բ�ҩ����ʾ �Ƿ�ͬʱ��ʾ����
        /// </summary>
        [Description("�Բ�ҩ����ʾ �Ƿ�ͬʱ��ʾ����"),Category("����"),DefaultValue(false)]
        public bool IsShowDays
        {
            get
            {
                return this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColDays].Visible;
            }
            set
            {
                this.neuSpread1_Sheet1.Columns[(int)ColumnSet.ColDays].Visible = value;
                this.isShowDays = value;
            }
        }

        /// <summary>
        /// ��ҩ���Ƿ��ӡ����
        /// </summary>
        [Description("��ҩ���Ƿ��ӡ����"), Category("����"), DefaultValue(false)]
        public bool IsPrintRecipe
        {
            get
            {
                return this.isPrintRecipe;
            }
            set
            {
                this.isPrintRecipe = value;
            }
        }

        /// <summary>
        /// ��ҩ���Ƿ��ӡ��ҩ�嵥
        /// </summary>
        [Description("��ҩ���Ƿ��ӡ��ҩ�嵥"), Category("����"), DefaultValue(false)]
        public bool IsPrintListing
        {
            get
            {
                return this.isPrintListing;
            }
            set
            {
                this.isPrintListing = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ������ϸ��Ϣ
        /// </summary>
        [Description("�Ƿ���ʾ������ϸ��Ϣ"),Category("����"),DefaultValue(false)]
        public bool IsPatientDetail
        {
            get
            {
                return this.isPatientDetail;
            }
            set
            {
                this.isPatientDetail = value;
            }
        }

        /// <summary>
        /// ���߻�����Ϣ
        /// </summary>
        [Description("�Ƿ���ʾ���߻�����Ϣ"), Category("����"), DefaultValue(true)]
        public bool IsShowPatientBaseInfo
        {
            get
            {
                return this.lbBasePatientInfo.Visible;
            }
            set
            {
                this.lbBasePatientInfo.Visible = value;
            }
        }

        /// <summary>
        /// ���߱��ο�����Ϣ
        /// </summary>
        [Description("�Ƿ���ʾ���߱��ο�����Ϣ"), Category("����"), DefaultValue(true)]
        public bool IsShowPatientFeeInfo
        {
            get
            {
                return this.lblPatientInfo.Visible;
            }
            set
            {
                this.lblPatientInfo.Visible = value;
            }
        }

        /// <summary>
        /// ���߱��ΰ�ҩ��Ϣ
        /// </summary>
        [Description("�Ƿ���ʾ���߱��ΰ�ҩ��Ϣ"), Category("����"), DefaultValue(true)]
        public bool IsShowDrugSendInfo
        {
            get
            {
                return this.lbDrugSendInfo.Visible;
            }
            set
            {
                this.lbDrugSendInfo.Visible = value;

                if (value)
                {
                    this.splitContainer1.SplitterDistance = 90;
                }
                else
                {
                    this.splitContainer1.SplitterDistance = 60;
                }
            }
        }

        /// <summary>
        /// Fp�߿�����
        /// </summary>
        [Description("Fp�߿��ʽ����"), Category("����"), DefaultValue(System.Windows.Forms.BorderStyle.None)]
        public System.Windows.Forms.BorderStyle FpBorder
        {
            get
            {
                return this.neuSpread1.BorderStyle;
            }
            set
            {
                this.neuSpread1.BorderStyle = value;
            }
        }

        /// <summary>
        /// Lb��ʽ����
        /// </summary>
        [Description("Lb����ɫ����"), Category("����")]
        public System.Drawing.Color LabelBackColor
        {
            get
            {
                return this.lbBasePatientInfo.BackColor;
            }
            set
            {
                this.lbBasePatientInfo.BackColor = value;
                this.lbDrugSendInfo.BackColor = value;
                this.lblPatientInfo.BackColor = value;
            }
        }

        /// <summary>
        /// ��汨��ʱ�Ƿ��ж�����
        /// </summary>
        [Description("��汨��ʱ�Ƿ��ж�����"), Category("����"),DefaultValue(true)]
        public bool IsJudgeWarnPreStore
        {
            get
            {
                return this.judgeWarnPreStore;
            }
            set
            {
                this.judgeWarnPreStore = value;
            }
        }

        /// <summary>
        /// ��汨��ʱ�Ƿ��ÿ�������ж�
        /// </summary>
        [Description("��汨��ʱ�Ƿ��ÿ�������ж�"), Category("����"), DefaultValue(true)]
        public bool IsJudgeWarLowQty
        {
            get
            {
                return this.judgeWarnLowQty;
            }
            set
            {
                this.judgeWarnLowQty = value;
            }            
        }


        /// <summary>
        /// ������ҩʱ�Ƿ���п�澯���ж�
        /// </summary>
        [Description("������ҩʱ�Ƿ���п�澯���ж�"), Category("����"), DefaultValue(false)]
        public bool IsJudgeWarnDruged
        {
            get
            {
                return this.isJudgeWarDruged;
            }
            set
            {
                this.isJudgeWarDruged = value;
            }
        }

        /// <summary>
        /// ���﷢ҩʱ�Ƿ���п�澯���ж�
        /// </summary>
        [Description("���﷢ҩʱ�Ƿ���п�澯���ж�"), Category("����"), DefaultValue(false)]
        public bool IsJudgeWarnSend
        {
            get
            {
                return this.isJudgeWarnSend;
            }
            set
            {
                this.isJudgeWarnSend = value;
            }
        }
        #endregion

        #region ����

        /// <summary>
        /// ���Ʋ�����ʼ��
        /// </summary>
        private void IntiControlParam()
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.IsShowDays = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Show_Days, true, false);
            this.IsPrintListing = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Print_List, true, false);
            this.IsPrintRecipe = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Print_Recipe, true, false);

            this.IsJudgeWarnDruged = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Warn_Druged, true, false);
            this.IsJudgeWarnSend = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Warn_Send, true, false);
        }

        /// <summary>
        /// ��ʼ��
        /// </summary>
        public virtual void Init()
        {
            FS.FrameWork.WinForms.Classes.Function.ShowWaitForm(FS.FrameWork.Management.Language.Msg("���ڼ��ص��ݴ�ӡ��������..."));
            Application.DoEvents();

            #region ��ȡ������Ϣ ���ڽ���������ʾ

            //�������Ƶ����Ϣ 
            FS.HISFC.BizLogic.Manager.Frequency frequencyManagement = new FS.HISFC.BizLogic.Manager.Frequency();
            ArrayList alFrequency = frequencyManagement.GetAll("Root");
            this.frequencyHelper = new FS.FrameWork.Public.ObjectHelper(alFrequency);
            //��ȡ�����÷�
            FS.HISFC.BizLogic.Manager.Constant c = new FS.HISFC.BizLogic.Manager.Constant();
            ArrayList alUsage = c.GetList(FS.HISFC.Models.Base.EnumConstant.USAGE);
            if (alUsage == null)
            {
                MessageBox.Show("��ȡ�÷��б����!");
                return;
            }
            this.usageHelper = new FS.FrameWork.Public.ObjectHelper(alUsage);

            FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();
            //��ȡ������Ա
            ArrayList alEmployee = managerIntegrate.QueryEmployeeAll();
            this.personHelper = new FS.FrameWork.Public.ObjectHelper(alEmployee);
            //��ȡ���п���
            ArrayList alDept = managerIntegrate.GetDepartment();
            this.deptHelper = new FS.FrameWork.Public.ObjectHelper(alDept);

            //��ȡ���������ն�
            FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
            ArrayList alDruged = drugStoreManager.QueryDrugTerminalByDeptCode(this.OperDept.ID, "0");
            ArrayList alSend = drugStoreManager.QueryDrugTerminalByDeptCode(this.OperDept.ID, "1");
            alDruged.AddRange(alSend);
            this.terminalHelper = new FS.FrameWork.Public.ObjectHelper(alDruged);

            #endregion

            #region ��ȡ���Ʋ�����Ϣ ���ڿ��Ƶ����������·�ʽ

            FS.FrameWork.Management.ExtendParam extManager = new FS.FrameWork.Management.ExtendParam();
            try
            {
                FS.HISFC.Models.Base.ExtendInfo deptExt = extManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT,"AdjustGist", this.OperDept.ID);
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

            FS.FrameWork.WinForms.Classes.Function.HideWaitForm();

            this.IntiControlParam();
        }

        /// <summary>
        /// �����ʾ��Ϣ
        /// </summary>
        public virtual void Clear()
        {
            try
            {
                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count; i++)
                {
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColCost].Formula = "";
                    this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.SystemColors.WindowText;
                }
                this.neuSpread1_Sheet1.Rows.Count = 0;
            }
            catch { }
        }

        /// <summary>
        /// ������Ϣ��ʾ
        /// </summary>
        /// <param name="drugRecipe">���ﴦ��������Ϣ</param>
        /// <param name="state">���ﴦ��״̬</param>
        public virtual void ShowData(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            if (drugRecipe == null)
                return;

            this.tempDrugRecipe = drugRecipe;

            this.ShowPatientInfo(drugRecipe);

            string state = "";
            switch (drugRecipe.RecipeState)
            {
                case "0":
                case "1":
                    state = "0";
                    break;
                case "2":
                    state = "1";
                    break;
                case "3":
                    state = "2";
                    break;
            }

            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            ArrayList al = itemManager.QueryApplyOutListForClinic(this.OperDept.ID, "M1", state, drugRecipe.RecipeNO);
            if (al == null)
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("���ݵ�����Ϣ��ȡ������ϸ��Ϣ��������") + itemManager.Err);
                return;
            }

            this.ShowData(al);
        }

        /// <summary>
        /// ���ݴ����������ʾ����
        /// </summary>
        /// <param name="alApplyOut"></param>
        internal virtual void ShowData(ArrayList alApplyOut)
        {
            //���������ʾ
            this.Clear();

            this.neuSpread1_Sheet1.Rows.Count = alApplyOut.Count;
            FS.HISFC.Models.Pharmacy.ApplyOut info;
            for (int i = 0; i < alApplyOut.Count; i++)
            {
                info = alApplyOut[i] as FS.HISFC.Models.Pharmacy.ApplyOut;
                
                if (info.Item.PackQty == 0)
                    info.Item.PackQty = 1;

                if (info.Days <= 0)
                    info.Days = 1;
                try
                {
                    
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value = true;
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColName].Text = info.Item.Name + "[" + info.Item.Specs + "]";
          
                    if (this.isShowDays)
                    {
                        int outMinQty;
                        int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty), (int)info.Item.PackQty, out outMinQty);

                        if (outPackQty == 0)
                        {
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text = (info.Operation.ApplyQty).ToString() + info.Item.MinUnit;
                        }
                        else if (outMinQty == 0)
                        {
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text = outPackQty.ToString() + info.Item.PackUnit;
                        }
                        else
                        {
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text = outPackQty.ToString() + info.Item.PackUnit + outMinQty.ToString() + info.Item.MinUnit;
                        }
                    }
                    else
                    {
                        int outMinQty;
                        int outPackQty = System.Math.DivRem((int)(info.Operation.ApplyQty * info.Days), (int)info.Item.PackQty, out outMinQty);

                        if (outPackQty == 0)
                        {
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text = (info.Operation.ApplyQty * info.Days).ToString() + info.Item.MinUnit;
                        }
                        else if (outMinQty == 0)
                        {
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text = outPackQty.ToString() + info.Item.PackUnit;
                        }
                        else                        
                        {
                            this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColNum].Text = outPackQty.ToString() + info.Item.PackUnit + outMinQty.ToString() + info.Item.MinUnit ;
                        }
                    }
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColDays].Text = info.Days.ToString();                    

                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColUseNum].Text = info.DoseOnce.ToString() + info.Item.DoseUnit;
                    if (this.frequencyHelper != null)
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColFrequency].Text = this.frequencyHelper.GetName(info.Frequency.ID);
                    }
                    if (this.usageHelper != null)
                    {
                        this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColUse].Text = this.usageHelper.GetName(info.Usage.ID);
                    }
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColPrice].Text = Math.Round(info.Item.Price / info.Item.PackQty, 4).ToString();
                    this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColCost].Text = Math.Round(info.Operation.ApplyQty * info.Days / info.Item.PackQty * info.Item.PriceCollection.RetailPrice, 2).ToString();
                    this.neuSpread1_Sheet1.Rows[i].Tag = info;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }

                if (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                {
                    this.neuSpread1_Sheet1.Rows[i].ForeColor = System.Drawing.Color.Red;
                }
            }

            this.ComputeSum();
        }

        /// <summary>
        /// ������Ϣ��ʾ
        /// </summary>
        protected virtual void ShowPatientInfo(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            try
            {
                if (this.IPatientShow != null)
                {
                    this.IPatientShow.ShowInfo(drugRecipe);
                    return;
                }

                //�ô����ķ�Ʊ�ţ��￨�ţ������������ӴַŴ󣩣��Ա����䣬�շ�Ա���ţ�ҽ���������շ�ʱ��
                string strBase = "";
                string strFee = "";

                #region ������ʾ��Ϣ��ʼ�ַ���

                string strDrugSend = "  ��ҩʱ�䣺{0} ��ҩ�ˣ�{1} ��ҩ̨��{2} ��ҩʱ�䣺{3} ��ҩ�ˣ�{4} ��ҩ̨��{5}";

                if (this.isPatientDetail)
                {
                    strBase = " �� Ʊ �ţ�{0}  �� �� �ţ�{1}  �� ����{2}  �� ��{3}  �� �䣺{4}  ��ϵ��ʽ��{5}  ��ͥסַ��{6}";
                    strFee = " �Һ����ڣ�{0} �շ��ˣ�{1} �շ�ʱ�䣺{2} ������ң�{3} ҽ ����{4} ��ϣ�{5}";
                }
                else
                {
                    strBase = " �� Ʊ �ţ�{0}  ����ţ�{1}  ������{2}  �Ա�{3}  ���䣺{4}";
                    strFee = " �Һ����ڣ�{0} �շ��ˣ�{1} �շ�ʱ�䣺{2} ������ң�{3} ҽ ����{4}";
                }

                #endregion

                FS.FrameWork.Management.DataBaseManger dataBase = new FS.FrameWork.Management.DataBaseManger();
                string strAge = dataBase.GetAge(drugRecipe.Age);
                if (drugRecipe.RecipeNO != "" && drugRecipe.RecipeNO != null)
                {
                    drugRecipe.Doct.Name = this.personHelper.GetName(drugRecipe.Doct.ID);
                    drugRecipe.DoctDept.Name = this.deptHelper.GetName(drugRecipe.PatientDept.ID);

                    drugRecipe.DrugTerminal.Name = this.terminalHelper.GetName(drugRecipe.DrugTerminal.ID);
                    drugRecipe.SendTerminal.Name = this.terminalHelper.GetName(drugRecipe.SendTerminal.ID);

                    if (this.isPatientDetail)
                    {
                        #region ��ʾ������ϸ��Ϣ  �漰�Һ�/����ҵ���� ��ʱ�Ȳ�д

                        //FS.HISFC.Management.Registration.Register regMgr = new FS.HISFC.Management.Registration.Register();
                        //FS.HISFC.Object.Registration.Register register = regMgr.QueryByClinic(drugRecipe.ClinicCode);
                        //if (register != null)
                        //    this.lbBasePatientInfo.Text = string.Format(strBase, drugRecipe.InvoiceNo, drugRecipe.CardNo, drugRecipe.PatientName, drugRecipe.Sex.Name, strAge, register.Phone, register.Address);
                        //else
                        //    this.lbBasePatientInfo.Text = string.Format(strBase, drugRecipe.InvoiceNo, drugRecipe.CardNo, drugRecipe.PatientName, drugRecipe.Sex.Name, strAge);

                        //FS.HISFC.Management.Case.Diagnose diagnoseMgr = new FS.HISFC.Management.Case.Diagnose();
                        //ArrayList alDiagnose = diagnoseMgr.QueryCaseDiagnoseForClinic(drugRecipe.ClinicCode, FS.HISFC.Management.Case.frmTypes.DOC);
                        //string diagnose = "";
                        //if (alDiagnose != null && alDiagnose.Count > 0)
                        //{
                        //    FS.HISFC.Object.Case.Diagnose diagnoseObj = alDiagnose[0] as FS.HISFC.Object.Case.Diagnose;
                        //    diagnose = diagnoseObj.DiagInfo.ICD10.Name;
                        //}
                        //this.lblPatientInfo.Text = string.Format(strFee, drugRecipe.RegDate.ToString(), drugRecipe.FeeOper, drugRecipe.FeeDate.ToString(), drugRecipe.DoctDept.Name, drugRecipe.Doct.Name, diagnose);

                        #endregion
                    }
                    else
                    {
                        this.lbBasePatientInfo.Text = string.Format(strBase, drugRecipe.InvoiceNO, drugRecipe.CardNO, drugRecipe.PatientName, drugRecipe.Sex.Name, strAge);
                        this.lblPatientInfo.Text = string.Format(strFee, drugRecipe.RegTime.ToString(), this.personHelper.GetName(drugRecipe.FeeOper.ID), drugRecipe.FeeOper.OperTime.ToString(), drugRecipe.DoctDept.Name, drugRecipe.Doct.Name);
                    }

                    this.lbDrugSendInfo.Text = string.Format(strDrugSend, drugRecipe.DrugedOper.OperTime.ToString(), 
                        this.personHelper.GetName(drugRecipe.DrugedOper.ID), 
                        drugRecipe.DrugTerminal.Name, drugRecipe.SendOper.OperTime.ToString(), 
                        this.personHelper.GetName(drugRecipe.SendOper.ID), drugRecipe.SendTerminal.Name);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// ������Ϣ��ʾ
        /// </summary>
        /// <param name="basePatientInfo">���߻�����Ϣ</param>
        /// <param name="feePatientInfo">���߱��ο�����Ϣ</param>
        /// <param name="drugSendInfo">���߰�/��ҩ��Ϣ</param>
        public void ShowPatientInfo(string basePatientInfo, string feePatientInfo, string drugSendInfo)
        {
            this.lbBasePatientInfo.Text = basePatientInfo;
            this.lblPatientInfo.Text = feePatientInfo;
            this.lbDrugSendInfo.Text = drugSendInfo;
        }

        /// <summary>
        /// ����ϼƽ��
        /// </summary>
        private void ComputeSum()
        {
            try
            {
                int rowCount = this.neuSpread1_Sheet1.Rows.Count;
                if (rowCount <= 0)
                    return;
                this.neuSpread1_Sheet1.Rows.Add(rowCount, 1);
                this.neuSpread1_Sheet1.Cells[rowCount, (int)ColumnSet.ColName].Text = "�ϼ�:";
                decimal totCost = 0;
                FS.HISFC.Models.Pharmacy.ApplyOut info;
                for (int i = 0; i < rowCount; i++)
                {
                    info = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (info.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
                        continue;
                    totCost = totCost + FS.FrameWork.Function.NConvert.ToDecimal(this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColCost].Text);

                }
                this.neuSpread1_Sheet1.Cells[rowCount, (int)ColumnSet.ColCost].Text = totCost.ToString();
                //				this.neuSpread1_Sheet1.Cells[rowCount,(int)ColumnSet.ColCost].Formula = "SUM(" + (char)(65 + (int)ColumnSet.ColCost) + "1:" + (char)(65 + (int)ColumnSet.ColCost) + (rowCount).ToString() + ")";
            }
            catch
            { }
        }

        /// <summary>
        /// ��ȡ��ǰѡ�е�����
        /// </summary>
        /// <returns></returns>
        internal List<FS.HISFC.Models.Pharmacy.ApplyOut> GetData()
        {
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            List<FS.HISFC.Models.Pharmacy.ApplyOut> alSelectData = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();
            for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count - 1; i++)
            {
                if ((bool)this.neuSpread1_Sheet1.Cells[i, (int)ColumnSet.ColSelect].Value)
                {
                    //��֤�Ƿ���ڷ���״̬���̵㵥 by Sunjh 2010-11-2 {60232B8F-906A-490a-A9F7-4C86679CF7D2}
                    FS.HISFC.Models.Pharmacy.ApplyOut appTemp = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;
                    if (itemManager.JudgeCheckState(appTemp.Item.ID, appTemp.StockDept.ID, "0", "0"))
                    {
                        if (MessageBox.Show("ҩƷĿ" + appTemp.Item.Name + "ǰ�ڱ��ⷿ���ڷ���״̬���Ƿ�������棿", "������ʾ", MessageBoxButtons.YesNo) == DialogResult.No)
                        {
                            return null;
                        }
                    }

                    alSelectData.Add(this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut);
                }
            }

            return alSelectData;
        }

        /// <summary>
        /// ����
        /// </summary>
        public virtual int Save()
        {
            if (this.OperInfo == null || this.OperInfo.ID == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ò���Ա"));
                return -1;
            }
            if (this.OperDept == null || this.OperDept.ID == "")
            {
                MessageBox.Show(FS.FrameWork.Management.Language.Msg("�����ò�������"));
                return -1;
            }

            if (this.BeginSave != null)
                this.BeginSave(null, System.EventArgs.Empty);

            List<FS.HISFC.Models.Pharmacy.ApplyOut> alData = this.GetData();
            if (alData == null || alData.Count <= 0)
                return -1;

            int parm = 1;
            //���ݲ�ͬ��������ò�ͬ����ʵ��
            switch (this.funModle)
            {
                case OutpatientFun.Drug:
                    //��ҩ ��ǩ�Զ���ӡ ����״̬���ۿ�
                    parm = Function.OutpatientDrug(alData, this.terminal, this.ApproveDept, this.OperInfo,this.isAdjustInDrug);
                    if (parm == 1)
                    {
                        if (this.isPrintListing && ListingPrint != null)            //��ӡ��ҩ�嵥
                        {
                            ListingPrint.AddAllData(new ArrayList(alData.ToArray()), this.tempDrugRecipe);
                            ListingPrint.Print();
                        }
                    }
                    break;
                case OutpatientFun.Send:                //����ۿ�
                    //�ж��Ƿ��ѽ��й���ҩ����
                    if (alData != null && alData.Count > 0)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        if (applyOut.State == "2")                        
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ҩƷ�ѷ�ҩ ���豣��"));
                            return -1;
                        }
                    }
                    try
                    {
                        //{60453BF5-EFFA-4cd5-832F-D63FD1B91CD2} ��׼���Ҹ�ֵ  ����������Ŀ�����޸�
                        foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
                        {
                            applyOut.Operation.ApproveOper.Dept = this.ApproveDept;
                        }

                        parm = Function.OutpatientSend(alData, this.terminal, this.ApproveDept, this.OperInfo, false, !this.isAdjustInDrug);
                        if (parm == 1)
                        {
                            if (this.isPrintRecipe && RecipePrint != null)              //��ӡ����
                            {
                                RecipePrint.AddAllData(new ArrayList(alData.ToArray()), this.tempDrugRecipe);
                                RecipePrint.Print();

                            }
                        }
                    }
                    catch (Exception ee)
                    {
                        MessageBox.Show(ee.Message);
                    }
                    break;
                case OutpatientFun.DirectSend:          //ֱ�ӱ��� ��ǩ�Զ���ӡ��ɺ� ����ۿ�  �����жϵ������� ÿ�ζ�����
                    //�ж��Ƿ��ѽ��й���ҩ����
                    if (alData != null && alData.Count > 0)
                    {
                        FS.HISFC.Models.Pharmacy.ApplyOut applyOut = alData[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
                        if (applyOut.State == "2")
                        {
                            MessageBox.Show(FS.FrameWork.Management.Language.Msg("��ҩƷ�ѷ�ҩ ���豣��"));
                            return -1;
                        }
                    }
                    //{60453BF5-EFFA-4cd5-832F-D63FD1B91CD2} ��׼���Ҹ�ֵ  ����������Ŀ�����޸�
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alData)
                    {
                        applyOut.Operation.ApproveOper.Dept = this.ApproveDept;
                    }

                    parm = Function.OutpatientSend(alData, this.terminal, this.ApproveDept, this.OperInfo, true, true);
                    if (parm == 1)
                    {
                        if (this.isPrintRecipe && RecipePrint != null)              //��ӡ����
                        {
                            RecipePrint.AddAllData(new ArrayList(alData.ToArray()), this.tempDrugRecipe);
                            RecipePrint.Print();
                        }
                    }
                    break;
                case OutpatientFun.Back:
                    parm = Function.OutpatientBack(alData, this.OperInfo);                    
                    break;
            }

            this.JudgeWarnStore();

            if (parm == 1)
                this.Clear();
            else
                return -1;

            if (this.EndSave != null)
                this.EndSave(null, System.EventArgs.Empty);
           
            return 1;
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        public virtual void Print()
        {
             List<FS.HISFC.Models.Pharmacy.ApplyOut> alData = this.GetData();

            //���ݲ�ͬ��������ò�ͬ����ʵ��
             switch (this.funModle)
             {
                 case OutpatientFun.Drug:
                     if (this.isPrintListing && ListingPrint != null)            //��ӡ��ҩ�嵥
                     {
                         ListingPrint.AddAllData(new ArrayList(alData.ToArray()), this.tempDrugRecipe);
                         ListingPrint.Print();
                     }
                     break;
                 case OutpatientFun.Send:
                     if (this.isPrintRecipe && RecipePrint != null)              //��ӡ����
                     {
                         RecipePrint.AddAllData(new ArrayList(alData.ToArray()), this.tempDrugRecipe);
                         RecipePrint.Print();
                     }
                     break;
                 case OutpatientFun.DirectSend:
                     if (this.isPrintRecipe && RecipePrint != null)              //��ӡ����
                     {
                         RecipePrint.AddAllData(new ArrayList(alData.ToArray()), this.tempDrugRecipe);
                         RecipePrint.Print();
                     }
                     break;
             }
        }

        /// <summary>
        /// ��澯�����ж�
        /// </summary>
        /// <param name="drugCode"></param>
        public virtual void JudgeWarnStore()
        {
            if ((this.funModle == OutpatientFun.Drug && this.IsJudgeWarnDruged) || (this.funModle == OutpatientFun.Send && this.IsJudgeWarnSend))
            {
                FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();

                for (int i = 0; i < this.neuSpread1_Sheet1.Rows.Count - 1; i++)
                {
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.neuSpread1_Sheet1.Rows[i].Tag as FS.HISFC.Models.Pharmacy.ApplyOut;

                    if (itemManager.JudgeIsWarnStore(this.ApproveDept.ID, applyOut.Item.ID, this.judgeWarnPreStore, this.judgeWarnLowQty))
                    {
                        if (this.MessageEvent != null)
                        {
                            this.MessageEvent(this.neuSpread1_Sheet1.Cells[i, 1].Text + " �Ѵﵽ��澯���ߣ���", System.EventArgs.Empty);
                        }
                    }
                }
            }
        }
        #endregion

        #region ������Ϣ��ʾ�ӿ�

        /// <summary>
        /// ������Ϣ��ʾ�ӿ�
        /// </summary>
        private FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientShow IPatientShow = null;

        /// <summary>
        /// ������Ϣ��ʾ�ӿڴ���
        /// </summary>
        public int SetShowInterface()
        {
            object[] o = new object[] { };

            try
            {
                this.IPatientShow = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject( this.GetType(), typeof( FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientShow ) ) as FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientShow;
            }
            catch
            {
                FS.FrameWork.WinForms.Classes.Function.HideWaitForm();
                //ʹ��Ĭ�ϻ�����Ϣ��ʾ��ʽ
                this.IPatientShow = null;

                return -1;
            }

            return 1;
        }

        #endregion

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                //��ʼ����Ϊ���ⲿ���ڵ���
                //this.Init();

                this.SetShowInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            base.OnLoad(e);
        }

        /// <summary>
        /// ���ô��ڹ���
        /// </summary>
        public override void SetFunMode(DrugStore.OutpatientFun winFunMode)
        {
            this.funModle = winFunMode;
        }

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                Type[] printType = new Type[1];
                printType[0] = typeof( FS.HISFC.BizProcess.Interface.Pharmacy.IOutpatientShow );

                return printType;
            }
        }

        #endregion
    }
}
