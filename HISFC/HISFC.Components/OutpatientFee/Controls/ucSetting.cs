using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using FS.FrameWork.Function;
using FS.HISFC.BizProcess.Integrate;

namespace FS.HISFC.Components.OutpatientFee.Controls
{
    /// <summary>
    /// ucSetting<br></br>
    /// [��������: �����շѲ�������UC]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-4-4]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public partial class ucSetting : FS.FrameWork.WinForms.Controls.ucBaseControl, FS.HISFC.BizProcess.Interface.Common.IControlParamMaint
    {
        /// <summary>
        /// ���췽��
        /// </summary>
        public ucSetting()
        {
            InitializeComponent();
        }

        #region ����

        /// <summary>
        /// ���ù���ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new FS.HISFC.BizProcess.Integrate.Fee();

        /// <summary>
        /// ��ͬ��λҵ���
        /// </summary>
        FS.HISFC.BizLogic.Fee.PactUnitInfo pactUnitManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// ������ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Manager managerIntegrate = new FS.HISFC.BizProcess.Integrate.Manager();

        /// <summary>
        /// �ҺŹ���ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Registration.Registration registerIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        /// <summary>
        /// ���Ʋ���ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Common.ControlParam controlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ������Ϣ
        /// </summary>
        private string errText = string.Empty;

        /// <summary>
        /// �Ƿ���ʾ��UC�İ�ť
        /// </summary>
        private bool isShowButtons = true;

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        private bool isModify = false;

        #endregion

        #region ����

        /// <summary>
        /// �Ƿ��������
        /// </summary>
        public bool IsModify 
        {
            get 
            {
                return this.isModify;
            }
            set 
            {
                this.isModify = value;
            }
        }

        /// <summary>
        /// �Ƿ���ʾ��UC�İ�ť
        /// </summary>
        public bool IsShowOwnButtons 
        {
            get 
            {
                return this.isShowButtons;
            }
            set 
            {
                this.isShowButtons = value;
                if (!this.isShowButtons)
                {
                    this.plBottom.Height = 0;
                }
                else 
                {
                    this.plBottom.Height = 34;
                }
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ErrText 
        {
            get 
            {
                return this.errText;
            }
            set 
            {
                this.errText = value;
            }
        }

        /// <summary>
        /// �ؼ�����
        /// </summary>
        public string Description 
        {
            get 
            {
                return "�����շѲ�������";
            }
            set 
            {
            
            }
        }

        #endregion

        #region ����

        /// <summary>
        /// Ӧ��
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int Apply() 
        {
            return 1;
        }

        /// <summary>
        /// ��ʼ����������ȡ���еĿ�������Ϣ�����û��ά����ΪĬ��ֵ
        /// </summary>
        /// <returns>-1 ʧ�� 0 �ɹ�</returns>
        public int Init()
        {
            this.tabControl1.TabPages.Remove(this.tabPage3);
            this.tabControl1.TabPages.Remove(this.tabPage4);
            
            string tempControlValue = null;// ��ȡ�Ŀ��Ʋ���ֵ
            
            #region ��ȡ�����Ƿ�Ԥ����Ʊ.

            this.ckbPreviewInvoice.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.PREVIEWINVOICE, true, false);
               
            #endregion

            #region ��ȡ���﷢Ʊ��ӡ��ʽ���Ʋ���

            string[] files = System.IO.Directory.GetFiles(Application.StartupPath + @"\Plugins\InvoicePrint", "*.dll");
            ArrayList tempFiles = new ArrayList();
            FS.FrameWork.Models.NeuObject objFile = new FS.FrameWork.Models.NeuObject();
            foreach (string f in files)
            {
                try
                {
                    Assembly a = Assembly.LoadFrom(f);
                    System.Type[] types = a.GetTypes();
                    foreach (System.Type type in types)
                    {
                        if (type.GetInterface("IInvoicePrint") != null)
                        {
                            objFile = new FS.FrameWork.Models.NeuObject();
                            objFile.ID = f.Replace(Application.StartupPath, string.Empty);
                            objFile.Name = ((FS.HISFC.BizProcess.Interface.FeeInterface.IInvoicePrint)System.Activator.CreateInstance(type)).Description;
                        }
                    }
                    tempFiles.Add(objFile);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            cmbPrintInvoice.AddItems(tempFiles);

            tempControlValue = this.controlParamIntegrate.GetControlParam<string>(Const.INVOICEPRINT, true, string.Empty);
            foreach (FS.FrameWork.Models.NeuObject obj in tempFiles)
            {
                if (obj.ID == tempControlValue)
                {
                    this.cmbPrintInvoice.Tag = obj.ID;
                }
            }

            #endregion

            #region ���﹫���㷨
            ////-----------------------------------------------------------------------------------------
            ////���﹫���㷨
            //files = System.IO.Directory.GetFiles(@".\Plugins\Clinic\PubFee", "*.dll");
            //tempFiles = new ArrayList();
            //objFile = new FS.FrameWork.Models.NeuObject();
            //foreach (string f in files)
            //{
            //    try
            //    {
            //        Assembly a = Assembly.LoadFrom(f);
            //        System.Type[] types = a.GetTypes();
            //        foreach (System.Type type in types)
            //        {
            //            if (type.GetInterface("IComputePubFee") != null)
            //            {
            //                objFile = new FS.FrameWork.Models.NeuObject();
            //                objFile.ID = f;
            //                objFile.Name = ((FS.HISFC.BizProcess.Integrate.FeeInterface.IMedcare)System.Activator.CreateInstance(type)).Description;
            //            }
            //        }
            //        tempFiles.Add(objFile);
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.Message);
            //    }
            //}
            //cmbPubFee.AddItems(tempFiles);

            //tempControlValue = myCrl.GetControlValue(FS.HISFC.BizProcess.Integrate.Const.PUBFEECOMPUTE, string.Empty);
            //foreach (FS.FrameWork.Models.NeuObject obj in tempFiles)
            //{
            //    if (obj.ID == tempControlValue)
            //    {
            //        this.cmbPubFee.Tag = obj.ID;
            //    }
            //}
            #endregion

            #region ���������ȴ������

            this.ckbDealCombs.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.DEALCOMBNO, true, false);

            #endregion

            #region ���Ŵ��������Ŀ��

            this.nudNoteCounts.Value = this.controlParamIntegrate.GetControlParam<int>(Const.NOTECOUNTS, true, 5);

            #endregion

            #region �����Ƿ�����ַ�Ʊ

            this.ckbCanSplit.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CANSPLIT, true, false);

            #endregion

            #region �ַ�Ʊ�������

            this.nudSplitCounts.Value = this.controlParamIntegrate.GetControlParam<int>(Const.SPLITCOUNTS, true, 9);

            #endregion

            #region ҽ�����ýӿ�����

            ////ҽ�����ýӿ�����
            //files = System.IO.Directory.GetFiles(@".\Plugins\MedicareInterface", "*.dll");
            //tempFiles = new ArrayList();
            //objFile = new FS.FrameWork.Models.NeuObject();
            //foreach (string f in files)
            //{
            //    try
            //    {
            //        Assembly a = Assembly.LoadFrom(f);
            //        System.Type[] types = a.GetTypes();
            //        foreach (System.Type type in types)
            //        {
            //            if (type.GetInterface("IInterface") != null)
            //            {
            //                objFile = new FS.FrameWork.Models.NeuObject();
            //                objFile.ID = f;
            //                objFile.Name = ((FS.HISFC.BizProcess.Integrate.FeeInterface.IMedcare)System.Activator.CreateInstance(type)).Description;
            //            }
            //        }
            //        tempFiles.Add(objFile);
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.Message);
            //    }
            //}
            //cmbInterfaceSheng.AddItems(tempFiles);
            //cmbInterfaceShi.AddItems(tempFiles);
            //cmbInterfaceRailway.AddItems(tempFiles);
            //cmbInterfaceOther.AddItems(tempFiles);

            //ArrayList pacts = new ArrayList();
            //pacts = this.myDept.GetConstantList(FS.HISFC.Models.Base.EnumConstant.PACKUNIT);

            //cmbPactSheng.AddItems(pacts);
            //cmbPactShi.AddItems(pacts);
            //cmbPactRailway.AddItems(pacts);
            //cmbPactOther.AddItems(pacts);



            //FS.HISFC.Models.Base.ControlParam ctrl = new FS.HISFC.Models.Base.ControlParam();
            ////ʡҽ��
            //ctrl = myCrl.QueryControlInfoByCode(FS.Common.Interface.Medicare.Const.SHENGINTERFACE);
            //if (ctrl != null)
            //{
            //    foreach (FS.FrameWork.Models.NeuObject obj in tempFiles)
            //    {
            //        if (obj.ID == ctrl.ControlerValue)
            //        {
            //            this.cmbInterfaceSheng.Tag = obj.ID;
            //        }
            //    }
            //    foreach (FS.FrameWork.Models.NeuObject obj in pacts)
            //    {
            //        if (obj.ID == ctrl.Name)
            //        {
            //            this.cmbPactSheng.Tag = obj.ID;
            //        }
            //    }
            //}
            ////��ҽ��
            //ctrl = myCrl.QueryControlInfoByCode(FS.Common.Interface.Medicare.Const.SHIINTERFACE);
            //if (ctrl != null)
            //{
            //    foreach (FS.FrameWork.Models.NeuObject obj in tempFiles)
            //    {
            //        if (obj.ID == ctrl.ControlerValue)
            //        {
            //            this.cmbInterfaceShi.Tag = obj.ID;
            //        }
            //    }
            //    foreach (FS.FrameWork.Models.NeuObject obj in pacts)
            //    {
            //        if (obj.ID == ctrl.Name)
            //        {
            //            this.cmbPactShi.Tag = obj.ID;
            //        }
            //    }
            //}
            ////��·ҽ��
            //ctrl = myCrl.QueryControlInfoByCode(FS.Common.Interface.Medicare.Const.RAILWAYINTERFACE);
            //if (ctrl != null)
            //{
            //    foreach (FS.FrameWork.Models.NeuObject obj in tempFiles)
            //    {
            //        if (obj.ID == ctrl.ControlerValue)
            //        {
            //            this.cmbInterfaceRailway.Tag = obj.ID;
            //        }
            //    }
            //    foreach (FS.FrameWork.Models.NeuObject obj in pacts)
            //    {
            //        if (obj.ID == ctrl.Name)
            //        {
            //            this.cmbPactRailway.Tag = obj.ID;
            //        }
            //    }
            //}
            ////����ҽ��
            //ctrl = myCrl.QueryControlInfoByCode(FS.Common.Interface.Medicare.Const.OTHERINTERFACE);
            //if (ctrl != null)
            //{
            //    foreach (FS.FrameWork.Models.NeuObject obj in tempFiles)
            //    {
            //        if (obj.ID == ctrl.ControlerValue)
            //        {
            //            this.cmbInterfaceOther.Tag = obj.ID;
            //        }
            //    }
            //    foreach (FS.FrameWork.Models.NeuObject obj in pacts)
            //    {
            //        if (obj.ID == ctrl.Name)
            //        {
            //            this.cmbPactOther.Tag = obj.ID;
            //        }
            //    }
            //}
            #endregion

            #region ����ǰ̨���ü���������

            this.cmbCompute.SelectedIndex = this.controlParamIntegrate.GetControlParam<int>(Const.CALCTYPE, true, 0);

            #endregion

            #region �����շѷֱҴ������

            this.cmbCentRule.SelectedIndex = this.controlParamIntegrate.GetControlParam<int>(Const.CENTRULE, true, 0);

            #endregion

            #region "Ĭ�ϼǼ۵�λ"

            this.cmbPriceUnit.SelectedIndex = this.controlParamIntegrate.GetControlParam<int>(Const.PRICEUNIT, true, 0);

            #endregion

            #region ҽ�����Ƿ����֧���ԷѲ���

            this.ckbYBCardPay.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CANUSEMCARND, true, false);

            #endregion

            #region �Ƿ�����޸Ļ��۱�����Ϣ

            this.ckbModifyCharge.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.MODIFY_CHARGE_INFO, true, true);

            #endregion

            #region �Ƿ������޸ĹҺ���Ϣ

            this.ckbModifyRegInfo.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CAN_MODIFY_REG_INFO, true, true);

            #endregion

            #region �շ�����ĹҺ���Ч����

            this.nudValidRegDays.Value = this.controlParamIntegrate.GetControlParam<int>(Const.VALID_REG_DAYS, true, 1);

            #endregion

            #region ���������˷ѵ���Ч����

            this.nudValidQuitDays.Value = this.controlParamIntegrate.GetControlParam<int>(Const.VALID_QUIT_DAYS, true, 1);

            #endregion

            #region �Ƿ��жϿ��

            this.ckbJudgeStore.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.JUDGE_STORE, true, false); 

            #endregion

            #region �۸񾯽���

            this.nudPriceWarnning.Value = this.controlParamIntegrate.GetControlParam<int>(Const.TOP_PRICE_WARNNING, true, 100000); 

            #endregion

            #region �۸񾯽���������ɫ

            this.plColor.BackColor = Color.FromArgb(
                this.controlParamIntegrate.GetControlParam<int>(Const.TOP_PRICE_WARNNING_COLOR, true, Color.Red.ToArgb())); 

            #endregion

            #region �Ƿ���������������Ա��Ʊ

            this.ckbQuitOtherOperInvoice.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CAN_QUIT_OTHER_OPER_INVOICE, true, false); 

            #endregion

            #region �Ƿ������˷��ս������Ʊ

            this.ckbQuitBalancedInvoice.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CAN_QUIT_DAYBALANCED_INVOICE, true, false);

            #endregion

            #region �Ƿ������ش���������Ա��Ʊ

            this.ckbReprintOtherOperInvoice.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CAN_REPRINT_OTHER_OPER_INVOICE, true, false);
            
            #endregion

            #region �Ƿ������ش��ս������Ʊ

            this.ckbReprintBalancedInvoice.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CAN_REPRINT_DAYBALANCED_INVOICE, true, false);

            #endregion

            #region �Ƿ�����ȡ����������Ա��Ʊ

            this.ckbCancelOtherOperInvoice.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CAN_CANCEL_OTHER_OPER_INVOICE, true, false);

            #endregion

            #region �Ƿ�����ȡ���ս������Ʊ

            this.ckbCancelBalancedInvoice.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CAN_CANCEL_DAYBALANCED_INVOICE, true, false);

            #endregion

            #region ���龫��ҩƷ��ʾ

            this.ckbSpDrugWarnning.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.SP_DRUG_WARNNING, true, false);

            #endregion

            #region �շ�ʱ�����ж���Ŀ�Ƿ�ͣ��

            this.ckbStopItemWarnning.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.STOP_ITEM_WARNNING, true, false);

            #endregion

            #region δ�ҺŻ��߿��Ų�λ����

            this.cmbNoRegRules.Text = this.controlParamIntegrate.GetControlParam<string>(Const.NO_REG_CARD_RULES, true, "9");

            #endregion

            #region δ�ҺŻ��߿�������Ƿ��ҽ��һ��

            this.ckbDocConfirmDept.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.DOCT_CONFIRM_DEPT, true, false);

            #endregion

            #region ����ҽ���������ȫƥ��

            this.ckbDoctDeptCorrect.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.DOCT_DEPT_INPUT_CORRECT, true, false);

            #endregion

            #region �Ƿ�����޸ķ�Ʊ����

            this.ckbMdifyInvoiceDate.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CAN_MODIFY_INVOICE_DATE, true, false);

            #endregion

            #region �Ƿ������ѻ��߰���

            this.ckbPubHalfQuit.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.PUB_CAN_HALF_QUIT, true, false);

            #endregion

            #region �Ƿ�����ҽ�����߰���

            this.ckbSIHalfQuit.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.SI_CAN_HALF_QUIT, true, false);

            #endregion

            #region ��÷�Ʊ�ŷ���

            this.cmbGetInvoiceNoType.SelectedIndex = this.controlParamIntegrate.GetControlParam<int>(Const.GET_INVOICE_NO_TYPE, true, 0);

            #endregion

            #region �ַ�Ʊ����

            this.cmbAutoSpitInvoice.SelectedIndex = this.controlParamIntegrate.GetControlParam<int>(Const.AUTO_INVOICE_TYPE, true, 0);

            #endregion

            #region �����˷��ֽ����

            this.ckbCashQuit.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.QUIT_PAY_MODE_SELECT, true, true);

            #endregion

            #region ���ﻼ�߽���������ת

            this.ckbSpFocus.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.DEAL_SP_REGLEVEL_FOCUS, true, false);
         
            #endregion

            #region δ�ҺŻ����Զ��Һŷѱ���

            this.tbAutoRegFeeCode.Text = this.controlParamIntegrate.GetControlParam<string>(Const.AUTO_REG_FEE_ITEM_CODE, true, "��");

            #endregion

            #region δ�ҺŻ����Զ��Һŷѽ��

            this.tbAutoRegFeeCost.Text = this.controlParamIntegrate.GetControlParam<decimal>(Const.AUTO_REG_FEE_ITEM_COST, true, 0).ToString();
        
            #endregion

            #region Ƶ����ʾ��ʽ

            this.cmbFreqType.SelectedIndex = this.controlParamIntegrate.GetControlParam<int>(Const.FREQ_DISPLAY_TYPE, true, 0);
            
            #endregion

            #region ��Ʊ�ش�Ĭ����һ�ŷ�Ʊ��

            this.ckbReprintDefalutInvoiceNo.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.REPRINT_SET_DEFAULT_INVOICE, true, false);

            #endregion

            #region �ҺŴ����ż���������Ϣ

            this.ckbRegRecipeNoValid.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.REG_RECIPE_NO_RELPACE_CARD_NO, true, false);

            #endregion

            #region �ҺŴ�������Ч����

            this.nudRecipeValidDays.Value = this.controlParamIntegrate.GetControlParam<int>(Const.REG_RECIPE_NO_VALID_DAYS, true, 1);

            #endregion

            #region Ӧ���û��Զ����ݼ�

            this.ckbUserKeys.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.USE_USER_DEFINE_KEYS, true, false);
       
            #endregion

            #region �Զ����ѱ���

            this.tbAutoDiagCode.Text = this.controlParamIntegrate.GetControlParam<string>(Const.AUTO_PUB_FEE_DIAG_FEE_CODE, true, "��");

            #endregion

            #region �Զ����ѽ��

            this.tbAutoDiagCost.Text = this.controlParamIntegrate.GetControlParam<decimal>(Const.AUTO_PUB_FEE_DIAG_FEE_COST, true, 0).ToString();

            #endregion

            #region ���Ʒ�ͳ�ƴ���

            this.tbStatZL.Text = this.controlParamIntegrate.GetControlParam<string>(Const.STAT_ZL_CODE, true, "��").ToString();

            #endregion

            #region ����ͳ�ƴ���

            this.tbStatJC.Text = this.controlParamIntegrate.GetControlParam<string>(Const.STAT_JC_CODE, true, "��").ToString();

            #endregion

            #region �������ô���

            this.tbMinSY.Text = this.controlParamIntegrate.GetControlParam<string>(Const.XYFEE, true, "��").ToString();

            #endregion

            #region ��Ѫ���ô���

            this.tbMinSX.Text = this.controlParamIntegrate.GetControlParam<string>(Const.SXFEE, true, "��").ToString();

            #endregion

            #region CT���ô���

            this.tbMinCT.Text = this.controlParamIntegrate.GetControlParam<string>(Const.CTFEE, true, "��").ToString();

            #endregion

            #region MRI���ô���

            this.tbMinMRI.Text = this.controlParamIntegrate.GetControlParam<string>(Const.MRIFEE, true, "��").ToString();

            #endregion

            #region ����ʵ�������շ�

            this.ckbEnterToFee.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.ENTER_TO_FEE, true, false);

            #endregion

            #region �����Ŀ����ȫ��

            this.ckbGroupAllQuit.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.GROUP_ITEM_ALLQUIT, true, false);

            #endregion

            #region ��ƱԤ������

            this.cmbInvoicePrivewType.SelectedIndex = this.controlParamIntegrate.GetControlParam<int>(Const.INVOICE_PREVIEW_TYPE, true, 0);

            #endregion

            #region �շ�ʱ������ȡ��

            this.chkQtyCeiling.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.QTY_TO_CEILING, true, false);

            #endregion

            #region �շ�ʱÿ����������Ϊ��

            this.chkDoseOnceNull.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.DOSE_ONCE_NULL, true, false);

            #endregion

            #region �շ�ʱʡ��������

            this.chkSSXFrist.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.PRO_CITY_FIRST, true, false);
            
            #endregion

            #region �շ�ʱ�Է���Ŀ����

            this.chkOwnPayFirst.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.OWNPAY_FIRST, true, false);
         
            #endregion

            #region ��ӡ�շ���ϸ
            
            ArrayList alValues = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("PrintFeeRecipe");
            
            if (alValues != null && alValues.Count >= 1)
            {
                this.chkPrintFeeRecipe.Checked = NConvert.ToBoolean(alValues[0].ToString());
            }

            #endregion

            #region �շ�ʱ�޸ķ�Ʊ��ӡ����

            this.chkModiryPrintDate.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.MODIFY_INVOICE_PRINTDATE, true, false);

            #endregion

            #region ҽ����HIS����ʱ�շ�

            this.chkFeeWhenTotDiff.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.FEE_WHEN_TOTDIFF, true, false);

            #endregion

            #region ҽ������û�йҺ�ʱ���շ�ʱ�Զ��Ǽ�

            this.chkRegWhenFee.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.REG_WHEN_FEE, true, false);

            #endregion

            #region ��������ʾ

            this.chkSpecialCheck.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.MSG_SPECIAL_CHECK, true, false);

            #endregion

            #region �б�����ʾȱҩҩƷ

            this.chkDisplayLackPhamarcy.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.DISPLAY_LACK_PHAMARCY, true, false);

            #endregion

            #region �Է�ҽ��������Ŀȫ���Ը�

            this.chkTotCostToPayCost.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.TOTCOST_TO_PAYCOST, true, false);

            #endregion

            #region �ȼ�����Ĭ�ϵ�һ��

            this.chkClassCodeFirst.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CLASS_CODE_PRE, true, false);

            #endregion

            #region �Է�ҽ�������Է���

            this.chkZFItem.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.ZFYB_HAVE_ZFITEM, true, false);

            #endregion

            #region �����籣��֧����ʽ

            this.chkSocialCard.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.SOCIAL_CARD_DISPLAY, true, false);

            #endregion

            #region �շѽ�����ʾ��С����

            this.chkDisMinFee.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.MINFEE_DISPLAY_WHENFEE, true, false);

            #endregion

            #region �̶���һ���ǼǱ���

            this.chkFIXCLASSCODE.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.FIX_FIRST_CLASSCODE, true, false);

            #endregion

            #region �շ�ʱӦ��ֻ��ʾ�ֽ���

            this.chkDiaplayCashOnly.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.CASH_ONLY_WHENFEE, true, false);

            #endregion

            #region �ԷѺ�ͬ��λ��Ŀ��ʾҽ�����

            this.chkZFDisplyYB.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.OWN_DISPLAY_YB, true, false);

            #endregion

            #region �ִ����ź������

            this.chkDecSysClass.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.DEC_SYS_WHENGETRECIPE, true, false);

            #endregion

            #region �����ݴ湦��

            this.chkEnalbleTempSave.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.ENABLE_TEMP_SAVE, true, false);

            #endregion

            #region ��LIS��������

            this.chkDataToLis.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.DATA_TO_LIS, true, false);

            #endregion

            #region �Է�������

            this.tbOwnDiagFeeCode.Text = this.controlParamIntegrate.GetControlParam<string>(Const.OWN_DIAGFEE_CODE, true, "��");

            #endregion

            #region �Զ������

            //this.tbOwnDiagFeeCost.Text = this.controlParamIntegrate.GetControlParam<decimal>(Const.OWN_DIAGFEE_COST, true, 0);

            #endregion

            #region ����Һż���

            ArrayList alRegLevel = registerIntegrate.QueryRegLevel();

            if (alRegLevel != null)
            {
                this.cmbEmrRegLevel.AddItems(alRegLevel);
                this.cmbCommonRegLevel.AddItems(alRegLevel);
            }

            this.cmbEmrRegLevel.Tag = this.controlParamIntegrate.GetControlParam<string>(Const.EMR_REG_LEVEL, true, string.Empty);

            #endregion

            #region ����Һż���

            this.cmbCommonRegLevel.Tag = this.controlParamIntegrate.GetControlParam<string>(Const.COM_REG_LEVEL, true, string.Empty);

            #endregion

            #region ���﹫��������

            this.tbEmrPubDiagFeeCode.Text = this.controlParamIntegrate.GetControlParam<string>(Const.EMR_PUBDIAG_ITEMCODE, true, "��");

            #endregion

            #region �����Է�������

            this.tbEmrOwnDiagFeeCode.Text = this.controlParamIntegrate.GetControlParam<string>(Const.EMR_OWNDIAG_ITEMCODE, true, "��");

            #endregion

            #region ְ���Һſ���
            
            ArrayList regDepts = this.managerIntegrate.QueryDeptmentsInHos(true);

            this.cmbEmpRegDept.AddItems(regDepts);

            this.cmbEmpRegDept.Tag = this.controlParamIntegrate.GetControlParam<string>(Const.EMPLOYEE_SEE_DEPT, true, string.Empty);
          
            #endregion

            #region ���������������ݴ��¼

            chkDealTemplateSave.Checked = this.controlParamIntegrate.GetControlParam<bool>(Const.���������ȿ��Ƿַ���¼, true, false);

            #endregion

            this.Focus();

            return 1;
        }

        /// <summary>
        /// ������Ʋ�����Ϣ
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int Save()
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();

            ArrayList allControlsValues = GetAllControlValue();
            if (allControlsValues == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();

                return -1;
            }

            this.managerIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            foreach (FS.HISFC.Models.Base.ControlParam c in allControlsValues)
            {
                int iReturn = this.managerIntegrate.InsertControlerInfo(c);
                if (iReturn == -1)
                {
                    //�����ظ���˵���Ѿ����ڲ���ֵ,��ôֱ�Ӹ���
                    if (managerIntegrate.DBErrCode == 1)
                    {
                        iReturn = this.managerIntegrate.UpdateControlerInfo(c);
                        if (iReturn <= 0)
                        {
                            FS.FrameWork.Management.PublicTrans.RollBack();
                            MessageBox.Show("���¿��Ʋ���[" + c.Name + "]ʧ��! ���Ʋ���ֵ:" + c.ID + "\n������Ϣ:" + this.managerIntegrate.Err);

                            return -1;
                        }
                    }
                    else
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        MessageBox.Show("������Ʋ���[" + c.Name + "]ʧ��! ���Ʋ���ֵ:" + c.ID + "\n������Ϣ:" + this.managerIntegrate.Err);
                        
                        return -1;
                    }
                }
            }

            FS.FrameWork.Management.PublicTrans.Commit();

            MessageBox.Show("����ɹ�!");

            return 1;
        }

        /// <summary>
        /// �ӽ����ȡ�Ŀ��Ʋ���ֵ
        /// </summary>
        /// <returns>�ӽ����ȡ�Ŀ��Ʋ���ֵ����</returns>
        private ArrayList GetAllControlValue()
        {
            ArrayList allControlValues = new ArrayList(); //���еĿ����༯��

            FS.HISFC.Models.Base.ControlParam tempControlObj = null;//��ʱ������ʵ��;

            string tempControlValue = null;// �ӽ����ȡ�Ŀ��Ʋ���ֵ
            
            #region ���������Ƿ�Ԥ����Ʊ.
            
            if (this.ckbPreviewInvoice.Checked == true)
            {
                tempControlValue = "1";//Ԥ��
            }
            else
            {
                tempControlValue = "0";//��Ԥ��
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.PREVIEWINVOICE;
            tempControlObj.Name = "�����Ƿ�Ԥ����Ʊ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ���﷢Ʊ��ӡ��ʽ

            if (this.cmbPrintInvoice.Tag == null || this.cmbPrintInvoice.Tag.ToString() == "")
            {
                MessageBox.Show("��ѡ�����﷢Ʊ��ӡ����!");

                return null;
            }

            tempControlValue = cmbPrintInvoice.Tag.ToString();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.INVOICEPRINT;
            tempControlObj.Name = "���﷢Ʊ��ӡ����";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �����㷨

            //if (this.cmbPubFee.Tag == null || this.cmbPubFee.Tag.ToString() == "")
            //{
            //    MessageBox.Show("��ѡ�����﹫���㷨����!");

            //    return null;
            //}
            //tempControlValue = this.cmbPubFee.Tag.ToString();
            //tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            //tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.PUBFEECOMPUTE;
            //tempControlObj.Name = "���﹫����";
            //tempControlObj.ControlerValue = tempControlValue;
            //tempControlObj.IsVisible = true;

            //allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �����������Ƿ�������Ϻ�

            if (this.ckbDealCombs.Checked == true)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.DEALCOMBNO;
            tempControlObj.Name = "�����������Ƿ�������Ϻ�";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion ���ﵥ�Ŵ������������

            #region ���ﵥ�Ŵ������������
            
            tempControlValue = this.nudNoteCounts.Value.ToString();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.NOTECOUNTS;
            tempControlObj.Name = "���ﵥ�Ŵ������������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �����Ƿ�����ַ�Ʊ

            if (this.ckbCanSplit.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CANSPLIT;
            tempControlObj.Name = "�����Ƿ�����ַ�Ʊ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �ַ�Ʊ�������
           
            tempControlValue = this.nudSplitCounts.Value.ToString();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.SPLITCOUNTS;
            tempControlObj.Name = "�ַ�Ʊ�������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ҽ�����ö�̬������
            
            ////ʡҽ��
            //if (this.cmbPactSheng.Tag == null || this.cmbPactSheng.Tag.ToString() == "")
            //{
            //    MessageBox.Show("��ѡ��ʡҽ����ͬ��λ!");
            //    return null;
            //}
            //if (this.cmbInterfaceSheng.Tag == null || this.cmbInterfaceSheng.Tag.ToString() == "")
            //{
            //    MessageBox.Show("��ѡ��ʡҽ�����ýӿ�!");
            //    return null;
            //}
            //tempControlValue = cmbInterfaceSheng.Tag.ToString();
            //tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            //tempControlObj.ID = FS.Common.Interface.Medicare.Const.SHENGINTERFACE;
            //tempControlObj.Name = cmbPactSheng.Tag.ToString();
            //tempControlObj.ControlerValue = tempControlValue;
            //tempControlObj.IsVisible = true;

            //alAllControlValues.Add(tempControlObj.Clone());

            ////��ҽ��
            //if (this.cmbPactShi.Tag == null || this.cmbPactShi.Tag.ToString() == "")
            //{
            //    MessageBox.Show("��ѡ����ҽ����ͬ��λ!");
            //    return null;
            //}
            //if (this.cmbInterfaceShi.Tag == null || this.cmbInterfaceShi.Tag.ToString() == "")
            //{
            //    MessageBox.Show("��ѡ����ҽ�����ýӿ�!");
            //    return null;
            //}
            //tempControlValue = cmbInterfaceShi.Tag.ToString();
            //tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            //tempControlObj.ID = FS.Common.Interface.Medicare.Const.SHIINTERFACE;
            //tempControlObj.Name = cmbPactShi.Tag.ToString();
            //tempControlObj.ControlerValue = tempControlValue;
            //tempControlObj.IsVisible = true;

            //alAllControlValues.Add(tempControlObj.Clone());

            ////��·ҽ��
            //if (this.cmbPactRailway.Tag == null || this.cmbPactRailway.Tag.ToString() == "")
            //{
            //    MessageBox.Show("��ѡ����ҽ����ͬ��λ!");
            //    return null;
            //}
            //if (this.cmbInterfaceRailway.Tag == null || this.cmbInterfaceRailway.Tag.ToString() == "")
            //{
            //    MessageBox.Show("��ѡ����·ҽ�����ýӿ�!");
            //    return null;
            //}
            //tempControlValue = cmbInterfaceRailway.Tag.ToString();
            //tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            //tempControlObj.ID = FS.Common.Interface.Medicare.Const.RAILWAYINTERFACE;
            //tempControlObj.Name = cmbPactRailway.Tag.ToString();
            //tempControlObj.ControlerValue = tempControlValue;
            //tempControlObj.IsVisible = true;

            //alAllControlValues.Add(tempControlObj.Clone());

            ////����ҽ��
            //if (this.cmbPactOther.Tag == null || this.cmbPactOther.Tag.ToString() == "")
            //{
            //    MessageBox.Show("��ѡ����ҽ����ͬ��λ!");
            //    return null;
            //}
            //if (this.cmbInterfaceOther.Tag == null || this.cmbInterfaceOther.Tag.ToString() == "")
            //{
            //    MessageBox.Show("��ѡ����·ҽ�����ýӿ�!");
            //    return null;
            //}
            //tempControlValue = cmbInterfaceOther.Tag.ToString();
            //tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            //tempControlObj.ID = FS.Common.Interface.Medicare.Const.OTHERINTERFACE;
            //tempControlObj.Name = cmbPactOther.Tag.ToString();
            //tempControlObj.ControlerValue = tempControlValue;
            //tempControlObj.IsVisible = true;

            //alAllControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ����ǰ̨���ü���������

            if (this.cmbCompute.Text.Trim() == "")//���û������Ĭ��Ϊ0 ��Windows������
            {
                tempControlValue = "0";//���� 
            }
            else
            {
                tempControlValue = this.cmbCompute.SelectedIndex.ToString();
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CALCTYPE;
            tempControlObj.Name = "����ǰ̨���ü���������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �����շѷֱҴ������

            if (this.cmbCentRule.Text.Trim() == "")//���û������Ĭ��Ϊ0 ������ֱ�
            {
                tempControlValue = "0";
            }
            else
            {
                tempControlValue = this.cmbCentRule.SelectedIndex.ToString();
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CENTRULE;
            tempControlObj.Name = "�����շѷֱҴ������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region Ĭ�ϼǼ۵�λ

            if (this.cmbPriceUnit.Text.Trim() == "")//���û������Ĭ��Ϊ0 ��С��λ
            {
                tempControlValue = "0";
            }
            else
            {
                tempControlValue = this.cmbPriceUnit.SelectedIndex.ToString();
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.PRICEUNIT;
            tempControlObj.Name = "Ĭ�ϼǼ۵�λ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ҽ�����Ƿ����֧���ԷѲ���

            if (this.ckbYBCardPay.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CANUSEMCARND;
            tempControlObj.Name = "ҽ�����Ƿ����֧���ԷѲ���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ�����޸Ļ��۱�����Ϣ

            if (this.ckbModifyCharge.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.MODIFY_CHARGE_INFO;
            tempControlObj.Name = "�Ƿ�����޸Ļ��۱�����Ϣ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ������޸ĹҺ���Ϣ

            if (this.ckbModifyRegInfo.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CAN_MODIFY_REG_INFO;
            tempControlObj.Name = "�Ƿ������޸ĹҺ���Ϣ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �շ�����ĹҺ���Ч����

            tempControlValue = this.nudValidRegDays.Value.ToString();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.VALID_REG_DAYS;
            tempControlObj.Name = "�շ�����ĹҺ���Ч����";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ���������˷ѵ���Ч����

            tempControlValue = this.nudValidQuitDays.Value.ToString();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.VALID_QUIT_DAYS;
            tempControlObj.Name = "���������˷ѵ���Ч����";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ��жϿ��

            if (this.ckbJudgeStore.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.JUDGE_STORE;
            tempControlObj.Name = "�Ƿ��жϿ��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());


            #endregion

            #region �۸񾯽���

            tempControlValue = this.nudPriceWarnning.Value.ToString();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.TOP_PRICE_WARNNING;
            tempControlObj.Name = "�۸񾯽���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �۸񾯽�����ʾ��ɫ

            tempControlValue = this.plColor.BackColor.ToArgb().ToString();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.TOP_PRICE_WARNNING_COLOR;
            tempControlObj.Name = "�۸񾯽�����ʾ��ɫ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ���������������Ա��Ʊ

            if (this.ckbQuitOtherOperInvoice.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CAN_QUIT_OTHER_OPER_INVOICE;
            tempControlObj.Name = "�Ƿ���������������Ա��Ʊ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ������˷��ս������Ʊ

            if (this.ckbQuitBalancedInvoice.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CAN_QUIT_DAYBALANCED_INVOICE;
            tempControlObj.Name = "�Ƿ������˷��ս������Ʊ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ������ش���������Ա��Ʊ

            if (this.ckbReprintOtherOperInvoice.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CAN_REPRINT_OTHER_OPER_INVOICE;
            tempControlObj.Name = "�Ƿ������ش���������Ա��Ʊ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ������ش��ս������Ʊ

            if (this.ckbReprintBalancedInvoice.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CAN_REPRINT_DAYBALANCED_INVOICE;
            tempControlObj.Name = "�Ƿ������ش��ս������Ʊ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ�����ȡ����������Ա��Ʊ

            if (this.ckbCancelOtherOperInvoice.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CAN_CANCEL_OTHER_OPER_INVOICE;
            tempControlObj.Name = "�Ƿ�����ȡ����������Ա��Ʊ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ�����ȡ���ս������Ʊ

            if (this.ckbCancelBalancedInvoice.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CAN_CANCEL_DAYBALANCED_INVOICE;
            tempControlObj.Name = "�Ƿ�����ȡ���ս������Ʊ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ���龫��ҩƷ��ʾ

            if (this.ckbSpDrugWarnning.Checked)
            {
                tempControlValue = "1";//����
            }
            else
            {
                tempControlValue = "0";//������
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.SP_DRUG_WARNNING;
            tempControlObj.Name = "���龫��ҩƷ��ʾ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �շ�ʱ�����ж���Ŀ�Ƿ�ͣ��

            if (this.ckbStopItemWarnning.Checked)
            {
                tempControlValue = "1";//�ж�
            }
            else
            {
                tempControlValue = "0";//���ж�
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.STOP_ITEM_WARNNING;
            tempControlObj.Name = "�շ�ʱ�����ж���Ŀ�Ƿ�ͣ��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region δ�ҺŻ��߿��Ų�λ����

            if (this.cmbNoRegRules.Text.Trim() == "")//���û������Ĭ��Ϊ9 
            {
                tempControlValue = "9";//����
            }
            else
            {
                tempControlValue = this.cmbNoRegRules.Text;
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.NO_REG_CARD_RULES;
            tempControlObj.Name = "δ�ҺŻ��߿��Ų�λ����";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region δ�ҺŻ��߿�������Ƿ��ҽ��һ��

            if (this.ckbDocConfirmDept.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.DOCT_CONFIRM_DEPT;
            tempControlObj.Name = "δ�ҺŻ��߿�������Ƿ��ҽ��һ��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ����ҽ���������ȫƥ��

            if (this.ckbDoctDeptCorrect.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.DOCT_DEPT_INPUT_CORRECT;
            tempControlObj.Name = "����ҽ���������ȫƥ��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ�����޸ķ�Ʊ����


            if (this.ckbMdifyInvoiceDate.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CAN_MODIFY_INVOICE_DATE;
            tempControlObj.Name = "�Ƿ�����޸ķ�Ʊ����";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ������ѻ��߰���

            if (this.ckbPubHalfQuit.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.PUB_CAN_HALF_QUIT;
            tempControlObj.Name = "�Ƿ������ѻ��߰���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Ƿ�����ҽ�����߰���

            if (this.ckbSIHalfQuit.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.SI_CAN_HALF_QUIT;
            tempControlObj.Name = "�Ƿ�����ҽ�����߰���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ��÷�Ʊ�ŷ���

            if (this.cmbGetInvoiceNoType.Text.Trim() == "")//���û������Ĭ��Ϊ0 
            {
                tempControlValue = "0";//��ҽ
            }
            else
            {
                tempControlValue = this.cmbGetInvoiceNoType.SelectedIndex.ToString();
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.GET_INVOICE_NO_TYPE;
            tempControlObj.Name = "��÷�Ʊ�ŷ���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �ַ�Ʊ����

            if (this.cmbAutoSpitInvoice.Text.Trim() == "")//���û������Ĭ��Ϊ0 
            {
                tempControlValue = "0";//��ҽ
            }
            else
            {
                tempControlValue = this.cmbAutoSpitInvoice.SelectedIndex.ToString();
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.AUTO_INVOICE_TYPE;
            tempControlObj.Name = "�ַ�Ʊ����";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �����˷��ֽ����

            if (this.ckbCashQuit.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.QUIT_PAY_MODE_SELECT;
            tempControlObj.Name = "�����˷��ֽ����";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ���ﻼ�߽���������ת

            if (this.ckbSpFocus.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.DEAL_SP_REGLEVEL_FOCUS;
            tempControlObj.Name = "���ﻼ�߽���������ת";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region δ�ҺŻ����Զ��Һŷѱ���

            tempControlValue = this.tbAutoRegFeeCode.Text;
            if (tempControlValue.Trim() == "")
            {
                tempControlValue = "��";
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.AUTO_REG_FEE_ITEM_CODE;
            tempControlObj.Name = "δ�ҺŻ����Զ��Һŷѱ���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region δ�ҺŻ����Զ��Һŷѽ��

            tempControlValue = this.tbAutoRegFeeCost.Text;
            decimal tmpValue = 0;
            try
            {
                //{8E1D49B7-48C1-4c0c-B91B-03319572BFD3}
                //tmpValue = Convert.ToDecimal(tempControlValue);
                tmpValue = FS.FrameWork.Function.NConvert.ToDecimal(tempControlValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show("������Ϸ�������!" + ex.Message);

                return null;
            }
            if (tmpValue > 1000)
            {
                MessageBox.Show("�ҺŷѲ������ó���1000");
                
                return null;
            }
            if (tmpValue < 0)
            {
                MessageBox.Show("�ҺŷѲ���С��0");
                
                return null;
            }
            if (tempControlValue.Trim() == "")
            {
                tempControlValue = "0";
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.AUTO_REG_FEE_ITEM_COST;
            tempControlObj.Name = "δ�ҺŻ����Զ��Һŷѽ��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region Ƶ����ʾ��ʽ

            if (this.cmbFreqType.Text.Trim() == "")//���û������Ĭ��Ϊ0 
            {
                tempControlValue = "0";//����
            }
            else
            {
                tempControlValue = this.cmbFreqType.SelectedIndex.ToString();
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.FREQ_DISPLAY_TYPE;
            tempControlObj.Name = "Ƶ����ʾ��ʽ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ��Ʊ�ش�Ĭ����һ�ŷ�Ʊ��

            if (this.ckbReprintDefalutInvoiceNo.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.REPRINT_SET_DEFAULT_INVOICE;
            tempControlObj.Name = "��Ʊ�ش�Ĭ����һ�ŷ�Ʊ��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �ҺŴ����ż���������Ϣ

            if (this.ckbRegRecipeNoValid.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.REG_RECIPE_NO_RELPACE_CARD_NO;
            tempControlObj.Name = "�ҺŴ����ż���������Ϣ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �ҺŴ�������Ч����

            tempControlValue = this.nudRecipeValidDays.Value.ToString();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.REG_RECIPE_NO_VALID_DAYS;
            tempControlObj.Name = "�ҺŴ�������Ч����";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region Ӧ���û��Զ����ݼ�

            if (this.ckbUserKeys.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.USE_USER_DEFINE_KEYS;
            tempControlObj.Name = "Ӧ���û��Զ����ݼ�";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Զ����ѱ���

            tempControlValue = this.tbAutoDiagCode.Text;
            if (tempControlValue.Trim() == "")
            {
                tempControlValue = "��";
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.AUTO_PUB_FEE_DIAG_FEE_CODE;
            tempControlObj.Name = "�Զ����ѱ���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Զ����ѽ��

            tempControlValue = this.tbAutoDiagCost.Text;
            //{8E1D49B7-48C1-4c0c-B91B-03319572BFD3}
            if (string.IsNullOrEmpty(tempControlValue))
            {
                tempControlValue = "0";
            }
            tmpValue = 0;
            try
            {
                tmpValue = Convert.ToDecimal(tempControlValue);
            }
            catch (Exception ex)
            {
                MessageBox.Show("������Ϸ�������!" + ex.Message);
                return null;
            }
            if (tmpValue > 1000)
            {
                MessageBox.Show("���Ѳ������ó���1000");
                return null;
            }
            if (tmpValue < 0)
            {
                MessageBox.Show("���Ѳ���С��0");
                return null;
            }
            if (tempControlValue.Trim() == "")
            {
                tempControlValue = "0";
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.AUTO_PUB_FEE_DIAG_FEE_COST;
            tempControlObj.Name = "�Զ����ѱ���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �������ô���

            tempControlValue = this.tbMinSY.Text;
            if (tempControlValue.Trim() == "")
            {
                tempControlValue = "��";
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.XYFEE;
            tempControlObj.Name = "�������ô���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ��Ѫ���ô���

            tempControlValue = this.tbMinSX.Text;
            if (tempControlValue.Trim() == "")
            {
                tempControlValue = "��";
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.SXFEE;
            tempControlObj.Name = "��Ѫ���ô���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region CT���ô���

            tempControlValue = this.tbMinCT.Text;
            if (tempControlValue.Trim() == "")
            {
                tempControlValue = "��";
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CTFEE;
            tempControlObj.Name = "CT���ô���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region MRI���ô���

            tempControlValue = this.tbMinMRI.Text;
            if (tempControlValue.Trim() == "")
            {
                tempControlValue = "��";
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.MRIFEE;
            tempControlObj.Name = "MRI���ô���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());
            
            #endregion

            #region ���Ʒ�ͳ�ƴ���

            tempControlValue = this.tbStatZL.Text;
            if (tempControlValue.Trim() == "")
            {
                tempControlValue = "��";
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.STAT_ZL_CODE;
            tempControlObj.Name = "���Ʒ�ͳ�ƴ���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ����ͳ�ƴ���

            tempControlValue = this.tbStatJC.Text;
            if (tempControlValue.Trim() == "")
            {
                tempControlValue = "��";
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.STAT_JC_CODE;
            tempControlObj.Name = "����ͳ�ƴ���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ����ʵ�������շ�

            if (this.ckbEnterToFee.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.ENTER_TO_FEE;
            tempControlObj.Name = "����ʵ�������շ�";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �����Ŀ����ȫ��

            if (this.ckbGroupAllQuit.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.GROUP_ITEM_ALLQUIT;
            tempControlObj.Name = "�����Ŀ����ȫ��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ��ƱԤ������

            if (this.cmbInvoicePrivewType.Text.Trim() == "")//���û������Ĭ��Ϊ0 
            {
                tempControlValue = "0";//Ĭ�Ϸ�ʽ
            }
            else
            {
                tempControlValue = this.cmbInvoicePrivewType.SelectedIndex.ToString();
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.INVOICE_PREVIEW_TYPE;
            tempControlObj.Name = "��ƱԤ������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �շ�ʱ������ȡ��

            if (this.chkQtyCeiling.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.QTY_TO_CEILING;
            tempControlObj.Name = "�շ�ʱ������ȡ��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �շ�ʱÿ������Ϊ��

            if (this.chkDoseOnceNull.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.DOSE_ONCE_NULL;
            tempControlObj.Name = "�շ�ʱÿ������Ϊ��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �շ�ʱÿ����������Ϊ��

            if (this.chkDoseOnceNull.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.DOSE_ONCE_NULL;
            tempControlObj.Name = "�շ�ʱÿ����������Ϊ��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �շ�ʱʡ��������

            if (this.chkSSXFrist.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.PRO_CITY_FIRST;
            tempControlObj.Name = "�շ�ʱʡ��������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �շ�ʱ�Է���Ŀ����

            if (this.chkOwnPayFirst.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.OWNPAY_FIRST;
            tempControlObj.Name = "�շ�ʱ�Է���Ŀ����";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �շ�ʱ�޸ķ�Ʊ��ӡ����

            if (this.chkModiryPrintDate.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.MODIFY_INVOICE_PRINTDATE;
            tempControlObj.Name = "�շ�ʱ�޸ķ�Ʊ��ӡ����";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ҽ����HIS����ʱ�շ�

            if (this.chkFeeWhenTotDiff.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.FEE_WHEN_TOTDIFF;
            tempControlObj.Name = "ҽ����HIS����ʱ�շ�";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ҽ������û�йҺ�ʱ���շ�ʱ�Զ��Ǽ�

            if (this.chkRegWhenFee.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.REG_WHEN_FEE;
            tempControlObj.Name = "ҽ������û�йҺ�ʱ���շ�ʱ�Զ��Ǽ�";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ��������ʾ

            if (this.chkSpecialCheck.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.MSG_SPECIAL_CHECK;
            tempControlObj.Name = "��������ʾ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �б�����ʾȱҩҩƷ

            if (this.chkDisplayLackPhamarcy.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.DISPLAY_LACK_PHAMARCY;
            tempControlObj.Name = "�б�����ʾȱҩҩƷ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Է�ҽ��������Ŀȫ���Ը�

            if (this.chkTotCostToPayCost.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.TOTCOST_TO_PAYCOST;
            tempControlObj.Name = "�Է�ҽ��������Ŀȫ���Ը�";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �ȼ�����Ĭ�ϵ�һ��

            if (this.chkClassCodeFirst.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CLASS_CODE_PRE;
            tempControlObj.Name = "�ȼ�����Ĭ�ϵ�һ��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Է�ҽ�������Է���

            if (this.chkZFItem.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.ZFYB_HAVE_ZFITEM;
            tempControlObj.Name = "�Է�ҽ�������Է���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �����籣��֧����ʽ

            if (this.chkSocialCard.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.SOCIAL_CARD_DISPLAY;
            tempControlObj.Name = "�����籣��֧����ʽ";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �շѽ�����ʾ��С����

            if (this.chkDisMinFee.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.MINFEE_DISPLAY_WHENFEE;
            tempControlObj.Name = "�շѽ�����ʾ��С����";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �̶���һ���ǼǱ���

            if (this.chkFIXCLASSCODE.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.FIX_FIRST_CLASSCODE;
            tempControlObj.Name = "�̶���һ���ǼǱ���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �շ�ʱӦ��ֻ��ʾ�ֽ���

            if (this.chkDiaplayCashOnly.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.CASH_ONLY_WHENFEE;
            tempControlObj.Name = "�շ�ʱӦ��ֻ��ʾ�ֽ���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �ԷѺ�ͬ��λ��Ŀ��ʾҽ������

            if (this.chkZFDisplyYB.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.OWN_DISPLAY_YB;
            tempControlObj.Name = "�ԷѺ�ͬ��λ��Ŀ��ʾҽ������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �ִ����ź������

            if (this.chkDecSysClass.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.DEC_SYS_WHENGETRECIPE;
            tempControlObj.Name = "�ִ����ź������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �����ݴ湦��

            if (this.chkEnalbleTempSave.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.ENABLE_TEMP_SAVE;
            tempControlObj.Name = "�����ݴ湦��";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ��LIS��������

            if (this.chkDataToLis.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.DATA_TO_LIS;
            tempControlObj.Name = "��LIS��������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Զ�������

            tempControlValue = this.tbOwnDiagFeeCode.Text.Trim();

            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.OWN_DIAGFEE_CODE;
            tempControlObj.Name = "�Զ�������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �Է������

            //			tempControlValue = this.tbOwnDiagFeeCost.Text.Trim();
            //			tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            //			tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.OWN_DIAGFEE_COST;
            //			tempControlObj.Name = "�Է������";
            //			tempControlObj.ControlerValue = tempControlValue;
            //			tempControlObj.IsVisible = true;
            //
            //			alAllControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ����Һż���

            tempControlValue = this.cmbEmrRegLevel.Tag.ToString();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.EMR_REG_LEVEL;
            tempControlObj.Name = "����Һż���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ����Һż���

            tempControlValue = this.cmbCommonRegLevel.Tag.ToString();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.COM_REG_LEVEL;
            tempControlObj.Name = "����Һż���";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ���﹫��������

            tempControlValue = this.tbEmrPubDiagFeeCode.Text.Trim();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.EMR_PUBDIAG_ITEMCODE;
            tempControlObj.Name = "���﹫��������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region �����Է�������

            tempControlValue = this.tbEmrOwnDiagFeeCode.Text.Trim();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.EMR_OWNDIAG_ITEMCODE;
            tempControlObj.Name = "�����Է�������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ְ���������

            tempControlValue = this.cmbEmpRegDept.Tag.ToString();
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.EMPLOYEE_SEE_DEPT;
            tempControlObj.Name = "ְ���������";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            #region ���������������ݴ��¼

            if (this.chkDealTemplateSave.Checked)
            {
                tempControlValue = "1";//��
            }
            else
            {
                tempControlValue = "0";//����
            }
            tempControlObj = new FS.HISFC.Models.Base.ControlParam();
            tempControlObj.ID = FS.HISFC.BizProcess.Integrate.Const.���������ȿ��Ƿַ���¼;
            tempControlObj.Name = "���������ȿ��Ƿַ���¼";
            tempControlObj.ControlerValue = tempControlValue;
            tempControlObj.IsVisible = true;

            allControlValues.Add(tempControlObj.Clone());

            #endregion

            return allControlValues;
        }
        
        #endregion

        #region �¼�

        /// <summary>
        /// �����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            this.Save();
        }

        /// <summary>
        /// ȡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (this.isShowButtons)
            {
                this.FindForm().Close();
            }
        }

        protected override FS.FrameWork.WinForms.Forms.ToolBarService OnInit(object sender, object neuObject, object param)
        {
            this.Init();
            
            return base.OnInit(sender, neuObject, param);
        }
        
        /// <summary>
        /// ѡ����ɫ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnColor_Click(object sender, EventArgs e)
        {
            DialogResult result = this.colorDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.plColor.BackColor = colorDialog1.Color;
            }
        }

        #endregion

        private void neuButton1_Click(object sender, EventArgs e)
        {
            string[] s = System.IO.Directory.GetFiles(Application.StartupPath);

            foreach (string file in s) 
            {
                System.Reflection.Assembly a;

                try
                {
                    a = System.Reflection.Assembly.LoadFrom(file);

                    if (a == null)
                    {
                        continue;
                    }
                }
                catch 
                {
                    continue;
                }

                Type[] t = a.GetTypes();

                if (t == null) 
                {
                    continue;
                }

                foreach (Type type in t) 
                {
                    if (type.GetInterface("IControlParamMaint") != null) 
                    {
                        System.Runtime.Remoting.ObjectHandle o = System.Activator.CreateInstance(type.Assembly.ToString(), type.Namespace + "." + type.Name);

                        if (o != null) 
                        {
                            FS.FrameWork.WinForms.Classes.Function.PopShowControl((Control)o.Unwrap());
                        }
                    }
                }
            }
        }
    }
}