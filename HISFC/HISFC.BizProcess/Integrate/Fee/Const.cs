using System;
using System.Collections.Generic;
using System.Text;

namespace FS.HISFC.BizProcess.Integrate
{
    public class Const
    {
        #region ����������ó���
        
        /// <summary>
        /// ҽԺ����- ҽ���ϴ�����
        /// {1EBEBAA1-4122-47df-8115-47FF9A7BF5AC}
        /// </summary>
        public const string HosCode = "SHCODE";

        #region liuq 2007-8-23 ׷��
        /// <summary>
        /// �Ƿ���ִ�п��ҷַ�Ʊ
        /// </summary>
        public const string IS_SPLIT_INVOICE_BY_EXEDEPT = "MZ0101";//(1 �� 0 ��)

        /// <summary>
        /// �Ƿ�����С���÷ַ�Ʊ
        /// </summary>
        public const string IS_SPLIT_INVOICE_BY_FEECODE = "MZ0102";//(1 �� 0 ��)

        /// <summary>
        /// �Էѷ�Ʊ��С���������Ŀ
        /// </summary>
        public const string SPLIT_INVOICE_BY_FEECODE_ZF_COUNT = "MZ0103";

        /// <summary>
        /// ҽ����Ʊ��С���������Ŀ
        /// </summary>
        public const string SPLIT_INVOICE_BY_FEECODE_YB_COUNT = "MZ0104";

        /// <summary>
        /// ���ѷ�Ʊ��С���������Ŀ
        /// </summary>
        public const string SPLIT_INVOICE_BY_FEECODE_GF_COUNT = "MZ0105"; 
        #endregion

        /// <summary>
        /// �Ƿ����﷢ƱԤ��
        /// </summary>
        public const string PREVIEWINVOICE = "MZ0001";
        /// <summary>
        /// ���﷢Ʊ��ӡ��ʽ
        /// </summary>
        public const string INVOICEPRINT = "MZ0002";
        /// <summary>
        /// ���﹫���㷨����
        /// </summary>
        public const string PUBFEECOMPUTE = "MZ0003";
        /// <summary>
        /// ���ﴦ���������Ƿ�������Ϻ�
        /// </summary>
        public const string DEALCOMBNO = "MZ0004";
        /// <summary>
        /// ���ﵥ�Ŵ������������
        /// </summary> 
        public const string NOTECOUNTS = "MZ0005";
        /// <summary>
        /// �����Ƿ���Էַ�Ʊ
        /// </summary>
        public const string CANSPLIT = "MZ0006";
        /// <summary>
        /// �����Ƿ���Էַ�Ʊ�������
        /// </summary>
        public const string SPLITCOUNTS = "MZ0007";
        /// <summary>
        /// ǰ̨��������������
        /// </summary>
        public const string CALCTYPE = "MZ0008";
        /// <summary>
        /// �ֱҴ������
        /// </summary>
        public const string CENTRULE = "MZ0009";
        /// <summary>
        /// Ĭ�ϼǼ۵�λ
        /// </summary>
        public const string PRICEUNIT = "MZ0010";
        /// <summary>
        /// ҽ���ԷѲ����Ƿ����֧��ҽ����
        /// </summary>
        public const string CANUSEMCARND = "MZ0011";
        /// <summary>
        /// �Ƿ�����޸Ļ��۱�����Ϣ
        /// </summary>
        public const string MODIFY_CHARGE_INFO = "MZ0012";
        /// <summary>
        /// �Ƿ���Ը��ĹҺ���Ϣ.
        /// </summary>
        public const string CAN_MODIFY_REG_INFO = "MZ0013";
        /// <summary>
        /// �����շѹҺ���Ч����
        /// </summary>
        public const string VALID_REG_DAYS = "MZ0014";

        /// <summary>
        /// �����˷���Ч����
        /// </summary>
        public const string VALID_QUIT_DAYS = "MZ0015";
        /// <summary>
        /// �Ƿ��жϿ��
        /// </summary>
        public const string JUDGE_STORE = "MZ0016";
        /// <summary>
        /// �۸���ʾ��
        /// </summary>
        public const string TOP_PRICE_WARNNING = "MZ0017";
        /// <summary>
        /// Ԥ����ɫ
        /// </summary>
        public const string TOP_PRICE_WARNNING_COLOR = "MZ0018";
        /// <summary>
        /// �Ƿ��������������Ա�ķ���!
        /// </summary>
        public const string CAN_QUIT_OTHER_OPER_INVOICE = "MZ0019";
        /// <summary>
        /// �Ƿ�������ս�����ķ���!
        /// </summary>
        public const string CAN_QUIT_DAYBALANCED_INVOICE = "MZ0020";
        /// <summary>
        /// �Ƿ�����ش���������Ա��Ʊ
        /// </summary>
        public const string CAN_REPRINT_OTHER_OPER_INVOICE = "MZ0021";
        /// <summary>
        /// �Ƿ�����ش������ս���ķ�Ʊ
        /// </summary>
        public const string CAN_REPRINT_DAYBALANCED_INVOICE = "MZ0022";
        /// <summary>
        /// �Ƿ����ȡ����������Ա�ķ�Ʊ
        /// </summary>
        public const string CAN_CANCEL_OTHER_OPER_INVOICE = "MZ0023";
        /// <summary>
        /// �Ƿ����ȡ�������ս���ķ�Ʊ
        /// </summary>
        public const string CAN_CANCEL_DAYBALANCED_INVOICE = "MZ0024";
        /// <summary>
        /// ����ҩƷ��ʾ
        /// </summary>
        public const string SP_DRUG_WARNNING = "MZ0025";
        /// <summary>
        /// �շ�ʱ�Ƿ��ж���Ŀͣ��
        /// </summary>
        public const string STOP_ITEM_WARNNING = "MZ0026";
        /// <summary>
        /// δ�ҺŻ����շѲ�λ����
        /// </summary>
        public const string NO_REG_CARD_RULES = "MZ0027";
        /// <summary>
        /// δ�ҺŻ����Ƿ�ͨ��ҽ��ȷ���������.
        /// </summary>
        public const string DOCT_CONFIRM_DEPT = "MZ0028";
        /// <summary>
        /// ����,ҽ���Ĵ����Ƿ���Ҫȫƥ��
        /// </summary>
        public const string DOCT_DEPT_INPUT_CORRECT = "MZ0029";
        /// <summary>
        /// �Ƿ�����޸ķ�Ʊ����
        /// </summary>
        public const string CAN_MODIFY_INVOICE_DATE = "MZ0030";
        /// <summary>
        /// �����Ƿ��������
        /// </summary>
        public const string PUB_CAN_HALF_QUIT = "MZ0031";
        /// <summary>
        /// ҽ�������Ƿ��������
        /// </summary>
        public const string SI_CAN_HALF_QUIT = "MZ0032";
        /// <summary>
        /// ��÷�Ʊ�ŷ�ʽ!
        /// </summary>
        public const string GET_INVOICE_NO_TYPE = "MZ0033";
        /// <summary>
        /// ����,ҽ�� �Զ��ַ�Ʊ����
        /// </summary>
        public const string AUTO_INVOICE_TYPE = "MZ0034";
        /// <summary>
        /// �����˷� ֧����ʽѡ��
        /// </summary>
        public const string QUIT_PAY_MODE_SELECT = "MZ0035";
        /// <summary>
        /// �ǹҺŻ����Զ���ȡ�Һŷѱ���
        /// </summary>
        public const string AUTO_REG_FEE_ITEM_CODE = "MZ0036";
        /// <summary>
        /// �ǹҺŻ����Զ���ȡ�Һŷѽ��
        /// </summary>
        public const string AUTO_REG_FEE_ITEM_COST = "MZ0037";
        /// <summary>
        /// ���������⽹����ת
        /// </summary>
        public const string DEAL_SP_REGLEVEL_FOCUS = "MZ0038";
        /// <summary>
        /// Ƶ����ʾ��ʽ 0 ���� 1 ����
        /// </summary>
        public const string FREQ_DISPLAY_TYPE = "MZ0039";
        /// <summary>
        /// ��Ʊ�ش�Ĭ����ʾ��һ�ŷ�Ʊ
        /// </summary>
        public const string REPRINT_SET_DEFAULT_INVOICE = "MZ0040";
        /// <summary>
        /// �ҺŴ����Ŵ������￨�� 1 �� 0 ��
        /// </summary>
        public const string REG_RECIPE_NO_RELPACE_CARD_NO = "MZ0041";
        /// <summary>
        /// �ҺŴ�������Ч����
        /// </summary>
        public const string REG_RECIPE_NO_VALID_DAYS = "MZ0042";
        /// <summary>
        /// �Ƿ�Ӧ���û��Զ����¼�
        /// </summary>
        public const string USE_USER_DEFINE_KEYS = "MZ0043";
        /// <summary>
        /// �û��Զ����ݼ�·��
        /// </summary>
        public const string USER_DIFINE_KEYS_FILE_PATH = @".\profile\clinicShotcut.xml";
        /// <summary>
        /// ���﹫�ѻ����Ƿ��Զ���ȡ���
        /// </summary>
        public const string AUTO_PUB_FEE_DIAG_FEE = "MZ0044";
        /// <summary>
        /// �����Զ���ȡ������
        /// </summary>
        public const string AUTO_PUB_FEE_DIAG_FEE_CODE = "MZ0045";
        /// <summary>
        /// �����Զ���ȡ�����
        /// </summary>
        public const string AUTO_PUB_FEE_DIAG_FEE_COST = "MZ0046";
        /// <summary>
        /// ���Ʒ�ͳ�ƴ������
        /// </summary>
        public const string STAT_ZL_CODE = "MZ0047";
        /// <summary>
        /// ����ͳ�ƴ������
        /// </summary>
        public const string STAT_JC_CODE = "MZ0048";
        /// <summary>
        /// CT����С���ô���
        /// </summary>
        public const string CTFEE = "MZ0049";
        /// <summary>
        /// MRI��С���ô���
        /// </summary>
        public const string MRIFEE = "MZ0050";
        /// <summary>
        /// ������С���ô���
        /// </summary>
        public const string XYFEE = "MZ0051";
        /// <summary>
        /// ��Ѫ����С���ô���
        /// </summary>
        public const string SXFEE = "MZ0052";
        /// <summary>
        /// �Ƿ�س�ֱ���շ�
        /// </summary>
        public const string ENTER_TO_FEE = "MZ0053";
        /// <summary>
        /// ������Ŀ�Ƿ�ȫ�� 1�� 0 ����
        /// </summary>
        public const string GROUP_ITEM_ALLQUIT = "MZ0054";
        /// <summary>
        /// ��ƱԤ����ʾ��ʽ
        /// </summary>
        public const string INVOICE_PREVIEW_TYPE = "MZ0055";
        /// <summary>
        /// �շ�ʱ���������Ƿ���ȡ��
        /// </summary>
        public const string QTY_TO_CEILING = "MZ0056";
        /// <summary>
        /// �շ�ʱÿ�������Ƿ����Ϊ��
        /// </summary>
        public const string DOSE_ONCE_NULL = "MZ0057";
        /// <summary>
        /// �շ�ʱʡ���ޱ�������
        /// </summary>
        public const string PRO_CITY_FIRST = "MZ0058";
        /// <summary>
        /// �շ�ʱ�Է���Ŀ����
        /// </summary>
        public const string OWNPAY_FIRST = "MZ0059";
        /// <summary>
        /// �շ�ʱ�����޸ķ�Ʊ��ӡ����
        /// </summary>
        public const string MODIFY_INVOICE_PRINTDATE = "MZ0060";
        /// <summary>
        /// ҽ����HIS����ʱ�շ�
        /// </summary>
        public const string FEE_WHEN_TOTDIFF = "MZ0061";
        /// <summary>
        /// ҽ������û�йҺ�ʱ���շ�ʱ�Զ��Ǽ� 
        /// </summary>
        public const string REG_WHEN_FEE = "MZ0062";
        /// <summary>
        /// ��������ʾ
        /// </summary>
        public const string MSG_SPECIAL_CHECK = "MZ0063";
        /// <summary>
        ///�б�����ʾȱҩҩƷ
        /// </summary>
        public const string DISPLAY_LACK_PHAMARCY = "MZ0064";
        /// <summary>
        ///��Χ�ڽ���Ƿ�ȫ���Ը�
        /// </summary>
        public const string TOTCOST_TO_PAYCOST = "MZ0065";
        /// <summary>
        ///�Ƿ�Ĭ�ϵȼ�����
        /// </summary>
        public const string CLASS_CODE_PRE = "MZ0066";
        /// <summary>
        ///�Է�ҽ�������Է���Ŀ
        /// </summary>
        public const string ZFYB_HAVE_ZFITEM = "MZ0067";
        /// <summary>
        ///��ʾ�籣��֧����ʽ
        /// </summary>
        public const string SOCIAL_CARD_DISPLAY = "MZ0068";
        /// <summary>
        ///�շѽ�����ʾ��С����
        /// </summary>
        public const string MINFEE_DISPLAY_WHENFEE = "MZ0069";
        /// <summary>
        ///�շѽ�����ʾ��С����
        /// </summary>
        public const string FIX_FIRST_CLASSCODE = "MZ0070";
        /// <summary>
        ///�շ�ʱֻ��ʾӦ���ֽ���
        /// </summary>
        public const string CASH_ONLY_WHENFEE = "MZ0071";
        /// <summary>
        ///�շ�ʱ�ԷѺ�ͬ��λ��ʾҽ�����
        /// </summary>
        public const string OWN_DISPLAY_YB = "MZ0072";
        /// <summary>
        ///�ִ����ź������
        /// </summary>
        public const string DEC_SYS_WHENGETRECIPE = "MZ0073";
        /// <summary>
        ///�����ݴ湦��
        /// </summary>
        public const string ENABLE_TEMP_SAVE = "MZ0074";
        /// <summary>
        ///����lis
        /// </summary>
        public const string DATA_TO_LIS = "MZ0075";
        /// <summary>
        ///�Է�������
        /// </summary>
        public const string OWN_DIAGFEE_CODE = "MZ0076";
        /// <summary>
        ///����Һż���
        /// </summary>
        public const string EMR_REG_LEVEL = "MZ0077";
        /// <summary>
        ///����Һż���
        /// </summary>
        public const string COM_REG_LEVEL = "MZ0078";
        /// <summary>
        ///���﹫��������
        /// </summary>
        public const string EMR_PUBDIAG_ITEMCODE = "MZ0079";
        /// <summary>
        ///�����Է�������
        /// </summary>
        public const string EMR_OWNDIAG_ITEMCODE = "MZ0080";
        /// <summary>
        /// ��Ժְ��Ĭ�Ͽ������
        /// </summary>
        public const string EMPLOYEE_SEE_DEPT = "MZ0081";
        /// <summary>
        /// ��ȡ��Ŀ������ʽ
        /// </summary>
        public const string GET_ITEMRATE_TYPE = "MZ0082";

        /// <summary>
        /// �����շ� ���߻�����Ϣ���
        /// </summary>
        public const string INTERFACE_REGINFO = "MZ0083";

        /// <summary>
        /// �����շ� ��Ŀ¼����
        /// </summary>
        public const string INTERFACE_ITEM_INPUT = "MZ0084";

        /// <summary>
        /// �����շ� �����Ϣ���
        /// </summary>
        public const string INTERFACE_LEFT = "MZ0085";

        /// <summary>
        /// �����շ� �Ҳ���Ϣ���
        /// </summary>
        public const string INTERFACE_RIGHT = "MZ0087";

        /// <summary>
        /// �����շ� �շѵ���ȷ�ϲ��
        /// </summary>
        public const string INTERFACE_POP_FEE = "MZ0088";

        /// <summary>
        /// �����շ� ��Ŀѡ����
        /// </summary>
        public const string INTERFACE_CHOOSE_ITEM = "MZ0099";

        /// <summary>
        /// �����ݴ��,���������ɹ������ȴ����ݴ��¼
        /// </summary>
        public const string ���������ȿ��Ƿַ���¼ = "MZ0100";

        /// <summary>
        /// �����շ��Ƿ�������븺������,�������{0F98A513-A9EA-4110-B35F-E353A390E350}
        /// </summary>
        public const string INPUT_NEGATIVE_QTY = "MZ0201";

        /// <summary>
        /// Э�������Ƿ�����ϸ {ED51E97B-B752-4c32-BD93-F80209A24879}
        /// </summary>
        public const string Split_NostrumDetail = "MZ0202";

        /// <summary>
        /// �Ƿ��ճ�����Ŀ�������շ�����
        /// </summary>
        public const string IS_SPLIT_RECIPE_SEQ_BY_EXCEED = "MZ0203";

        /// <summary>
        /// ͨ�������� ���ض�Ӧ�ӿ����͵�String
        /// </summary>
        /// <param name="constName">������</param>
        /// <returns>�ɹ� �ӿ��� ʧ��null</returns>
        public static string GetOutpatientPlugInInterfaceNameByConstName(string constName)
        {
            System.Collections.Hashtable hashTable = new System.Collections.Hashtable();

            hashTable.Add(INTERFACE_REGINFO, "FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientInfomation");
            hashTable.Add(INTERFACE_ITEM_INPUT, "FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientItemInputAndDisplay");
            hashTable.Add(INTERFACE_LEFT, "FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationLeft");
            hashTable.Add(INTERFACE_RIGHT, "FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight");
            hashTable.Add(INTERFACE_POP_FEE, "FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee");
            hashTable.Add(INTERFACE_CHOOSE_ITEM, "FS.HISFC.BizProcess.Integrate.FeeInterface.IChooseItemForOutpatient");

            if (hashTable.ContainsKey(constName))
            {
                return hashTable[constName].ToString();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// ͨ���ӿ��� ���ض�Ӧ����
        /// </summary>
        /// <param name="interfaceName">������</param>
        /// <returns>�ɹ� �ӿ��� ʧ��null</returns>
        public static string GetOutpatientPlugInConstNameByInterfaceName(string interfaceName)
        {
            System.Collections.Hashtable hashTable = new System.Collections.Hashtable();

            hashTable.Add("FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientInfomation", INTERFACE_REGINFO);
            hashTable.Add("FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientItemInputAndDisplay", INTERFACE_ITEM_INPUT);
            hashTable.Add("FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationLeft", INTERFACE_LEFT);
            hashTable.Add("FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientOtherInfomationRight", INTERFACE_RIGHT);
            hashTable.Add("FS.HISFC.BizProcess.Integrate.FeeInterface.IOutpatientPopupFee", INTERFACE_POP_FEE);
            hashTable.Add("FS.HISFC.BizProcess.Integrate.FeeInterface.IChooseItemForOutpatient", INTERFACE_CHOOSE_ITEM);

            if (hashTable.ContainsKey(interfaceName))
            {
                return hashTable[interfaceName].ToString();
            }
            else
            {
                return null;
            }
        }

        #endregion

        /// <summary>
        /// Ƿ�Ѵ߽�����ӡdll����
        /// </summary>
        public const string RADT_MoneyAlter = "RADT01";
    }

    /// <summary>
    /// ϵͳ���Ʋ���
    /// </summary>
    public class SysConst
    {
        public SysConst()
        {
            hsDescription = new System.Collections.Hashtable();

            this.hsDescription.Add(Use_Judge_VersionNum, "�Ƿ���Ҫ��ϵͳ���а汾У���ж�");
            this.hsDescription.Add(Use_Inject_Flow, "�Ƿ�ʹ������ע���������");
            this.hsDescription.Add(Use_Drug_ApartFee, "�Ƿ�ʹ��ҩƷִ�С��۷ѷֿ�����(ҽ������ʱ�������շѣ�ҩ��������ҩ)");
            this.hsDescription.Add(Use_Drug_BackFee, "ҩƷ��ҩ��ͬʱ�Ƿ��˷�");
            this.hsDescription.Add(Use_Drug_ApplyNurse, "ҩƷ����������� 0 ���� 1 ����վ");
        }

        #region ϵͳ���̲�������

        /// <summary>
        /// �Ƿ���Ҫ��ϵͳ���а汾У���ж�
        /// </summary>
        public const string Use_Judge_VersionNum = "S00001";

        /// <summary>
        /// �Ƿ�ʹ������ע���������
        /// </summary>
        public const string Use_Inject_Flow = "S00010";

        /// <summary>
        /// �Ƿ�ʹ��ҩƷִ�С��۷ѷֿ�����(ҽ������ʱ�������շѣ�ҩ��������ҩ)
        /// </summary>
        public const string Use_Drug_ApartFee = "S00020";

        /// <summary>
        /// ҩƷ��ҩ��ͬʱ�Ƿ��˷�
        /// </summary>
        public const string Use_Drug_BackFee = "S00021";

        /// <summary>
        /// ҩƷ����������� 0 ���� 1 ����վ
        /// </summary>
        public const string Use_Drug_ApplyNurse = "S00022";

        /// <summary>
        /// ĸӤ��ʱ��,���к��ӵķ��ö����������ͷ��,����û�з��÷��� 1 ��������ͷ�� 0 ����û��ά��,���ڻ���ͷ��
        /// </summary>
        public const string Use_Mother_PayAllFee = "S00023";

        /// <summary>
        /// ʹ��ҩƷ��������ģʽ 0 ��������� 1 ������ڸ����
        /// </summary>
        public const string Use_Negative_DrugStore = "S00024";

        /// <summary>
        /// �����˻������Ƿ�ʹ���ն˿۷�����  1�ն��շ� 0�����շ�
        /// </summary>
        public const string Use_Account_Process = "S00031";

        //{A6DB46E9-EDD4-47d3-B2E5-1D8966DBBA43}
        /// <summary>
        /// ��Ʊ���л��Ƿ���ʾ
        /// </summary>
        public const string Use_CutOverInvoiceNO_Mess = "S00032";
        #endregion

        private System.Collections.Hashtable hsDescription;

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="ctrlCode">��������</param>
        /// <returns>�ɹ��ҵ����ز������� δ�ҵ����ؿ�����</returns>
        public string GetParamDescription(string ctrlCode)
        {
            if (this.hsDescription.ContainsKey(ctrlCode))
            {
                return this.hsDescription[ctrlCode] as string;
            }
            else
            {
                return "";
            }
        }
    }

    /// <summary>
    /// ҩƷ������Ʋ���������
    /// </summary>
    /// <˵��>
    ///     1 ���Ʋ��������빦��ģ���Ӧ��ϵ
    ///         P00101~ P00299  סԺҩ������
    ///         P00301~ P00499  ����ҩ������
    ///         P00501~ P01001  ҩƷ/ҩ��������
    ///     2�����ϲ�����  2010-03-03
    ///         P00111��P00112��P00117��P00503��P00504
    ///         P01000��P01001��P01009��P01010��P01011
    ///         P01012��P01013��P01014��P01015
    /// </˵��>
    public class PharmacyConstant
    {
        #region ҩƷ����������ó���

        /// <summary>
        /// �������ά��ʱ ҩƷ�Ƿ��������Ϊ���ɲ������
        /// </summary>
        public const string Can_Set_NoSplitAtAll = "P00101";

        /// <summary>
        /// �������ά��ʱ ҩƷ�Ƿ��������Ϊ�ɲ�ֲ�ȡ��
        /// </summary>
        public const string Can_Set_SplitAndNoInteger = "P00102";

        /// <summary>
        ///  �������ά��ʱ ҩƷ�Ƿ��������Ϊ�ɲ����ȡ��
        /// </summary>
        public const string Can_Set_SplitAndUpperToInteger = "P00103";

        /// <summary>
        ///  �������ά��ʱ ҩƷ�Ƿ��������Ϊ���ɲ�ֵ���ȡ��
        /// </summary>
        public const string Can_Set_NosplitAndDayToInteger = "P00104";

        /// <summary>
        ///  �������ά��ʱ ҩƷ�Ƿ��������Ϊ�ɲ�ְ�����ȡ��
        /// </summary>
        public const string Can_Set_SplitAndDeptToInteger = "P00105";

        /// <summary>
        ///  �������ά��ʱ ҩƷ�Ƿ��������Ϊ�ɲ�ְ�����ȡ��
        /// </summary>
        public const string Can_Set_SplitAndNurceCellToInteger = "P00106";

        /// <summary>
        ///  �������ά��ʱ ҩƷ�Ƿ��������Ϊ���ɲ�ְ���װ��λȡ�� ���û��߿�� {9D24EE50-57C5-4cae-8B70-8D8C3E7B4BDC}
        /// </summary>
        public const string Can_Set_NoSplitAndPackUnit = "P00107";

        /// <summary>
        /// סԺҩ����ҩ�Ƿ���Ҫ��׼  
        /// </summary>
        public const string InDrug_Need_Approve = "P00109";

        /// <summary>
        /// סԺҩ����ҩ��׼ʱ�Ƿ����Ȩ���ж� ��ֻ��ҩʦ/ҩ��ʦ�����׼����
        /// </summary>
        public const string InDrug_Need_Priv = "P00110";

        /// <summary>
        /// ��������Ӵ���ҩ��Ϣʱ �Ƿ��Զ�ѡ��
        /// </summary>
        public const string InDrug_Auto_Check = "P00113";

        /// <summary>
        /// ҩ����ҩ����ӡ���� �Ƿ���ʾ�������ڻ��߷�ҩ������Ϣ
        /// </summary>
        public const string InDrug_Show_PatientTot = "P00114";

        /// <summary>
        /// ҩ����ҩ����ӡ���� �Ƿ���ʾ�����ݵĿ��һ��ܷ�ҩ��Ϣ
        /// </summary>
        public const string InDrug_Show_DeptTot = "P00115";

        /// <summary>
        /// ����ҩ�����б���ʾʱ �Ƿ񰴿������ȷ�ʽ��ʾ
        /// </summary>
        public const string InDrug_Show_DeptFirst = "P00116";     

        /// <summary>
        /// סԺ�շ��Ƿ�ʹ��Ԥ�ۿ�淽ʽ
        /// </summary>
        public const string InDrug_Pre_Out = "P00200";

        /// <summary>
        /// ���﷢ҩ�ն˱���� �Ƿ�ˢ��
        /// </summary>
        public const string Terminal_Save_Refresh = "P00301";

        /// <summary>
        /// ���﷢ҩ�����Ƿ���ʾ����
        /// </summary>
        public const string OutDrug_Show_Days = "P00302";

        /// <summary>
        /// ��ҩȷ�Ϻ��Ƿ��ӡ��ҩ�嵥
        /// </summary>
        public const string OutDrug_Print_List = "P00303";
       
        /// <summary>
        /// ��ҩȷ�Ϻ��Ƿ��ӡ����
        /// </summary>
        public const string OutDrug_Print_Recipe = "P00304";

        /// <summary>
        /// ��ҩ����ʱ���Ų���λ�� ��1 ���貹λ  ԭ���Ʋ���ֵ 500007
        /// </summary>
        public const string OutDrug_OperCode_Length = "P00305";

        /// <summary>
        /// ��/��ҩ�����Ƿ����Ȩ�޿��� (ֻ��ҩʦ���Բ���) ԭ���Ʋ���ֵ 500011
        /// </summary>
        public const string OutDrug_Need_Priv = "P00306";

        /// <summary>
        /// �����ǩ�Զ���ӡʱ  �Ƿ�Դ����� �ϼ�¼�Ĵ�������ӡ ԭ���Ʋ���ֵ500014
        /// </summary>
        public const string OutDrug_Print_BackRecipe = "P00307";

        /// <summary>
        /// �����ǩ��ӡʱ �ӿ����ݴ��䷽ʽ 0 ���鴫�� 1 һ���Դ���
        /// </summary>
        public const string OutDrug_PrintData_SentType = "P00308";

        /// <summary>
        /// ������ҩʱ �Ƿ���п�澯���ж�
        /// </summary>
        public const string OutDrug_Warn_Druged = "P00309";

        /// <summary>
        /// ���﷢ҩʱ �Ƿ���п�澯���ж�
        /// </summary>
        public const string OutDrug_Warn_Send = "P00310";

        /// <summary>
        /// ���﷢ҩʱ�Ƿ���ʾ�������ҩ��Ϣ
        /// </summary>
        public const string OutDrug_Show_OldSended = "P00311";

        /// <summary>
        /// ��������Զ���ҩģʽ  {F8D76CE8-6A0C-469b-AC43-4F69B2FCBAD8}
        /// </summary>
        public const string OutDrug_Auto_Druged = "P00312";

        /// <summary>
        /// ��������Զ���ҩģʽ  {DBB3C382-BB23-463b-8847-2F73C55F2586}
        /// </summary>
        public const string OutDrug_Auto_Send = "P00313";

        /// <summary>
        /// �����շ�ʱ�Ƿ����Ԥ�ۿ�����
        /// </summary>
        public const string OutDrug_Pre_Out = "P00320";

        /// <summary>
        /// ����ҩƷ�Ƿ�����˺���Ч
        /// </summary>
        public const string NewDrug_Need_Approve = "P00501";

        /// <summary>
        /// ҩƷ��������Ϣά������   {6F6120F5-6D88-47ce-AF9C-0CF781DE412F}
        /// </summary>
        public const string Set_Item_SpecialFlag = "P00502";     

        /// <summary>
        /// ҩƷ��Ϣά��ʱ  ���ڰ�װ����������������λ��
        /// </summary>
        public const string Max_PackQty_Digit = "P00505";

        /// <summary>
        /// ҩƷ��Ϣά��ʱ  ���ڻ�������������������λ��
        /// </summary>
        public const string Max_BaseDose_Digit = "P00506";

        /// <summary>
        /// ҩƷ��Ϣά��ʱ  ���ڼ۸�������������λ��
        /// </summary>
        public const string Max_Price_Digit = "P00507";

        /// <summary>
        /// ��Ʒ���Զ�����������������λ�� 
        /// </summary>
        public const string Max_NameCustomeCode_Digit = "P00508";

        /// <summary>
        /// �����Զ�����������������λ��
        /// </summary>
        public const string Max_CustomeCode_Digit = "P00509";

        /// <summary>
        /// �Ƿ�����ͨ����ά�����Tab˳��
        /// </summary>
        public const string Have_Regular_Tab = "P00510";

        /// <summary>
        /// �Ƿ�����Ӣ����ά�����Tab˳��
        /// </summary>
        public const string Have_English_Tab = "P00511";

        /// <summary>
        /// �Ƿ��������/���ұ���ά�����Tab˳��
        /// </summary>
        public const string Have_Code_Tab = "P00512";

        /// <summary>
        /// Э�������Ƿ������ ����������� ���շ�ʱ�����ϸ ���򲻽��в��
        /// </summary>
        public const string Nostrum_Manage_Store = "P00513";

        /// <summary>
        /// ҩƷ�̵��Ƿ����Ž����̵�
        /// </summary>
        public const string Check_With_Batch = "P00531";

        /// <summary>
        /// ��ʷ�̵㵥��ȡʱ�Ƿ�ֻȡ���״̬ 
        /// </summary>
        public const string Check_History_State = "P00532";

        /// <summary>
        /// �̵�������Ƿ���¼��ʱ���·��ʿ��
        /// 
        /// {F2DA66B0-0AB4-4656-BB21-97CB731ABA4D}  ���ӿ��Ʋ���
        /// </summary>
        public const string Check_UpdateFStore_RealTime = "P00533";

        /// <summary>
        /// �Զ����ɼƻ�ʱ �����վ��������������� ͳ��Ĭ������
        /// </summary>
        public const string Plan_Expand_Days = "P00540";

        /// <summary>
        /// �б�ѡ��ؼ��Ƿ���ʾ�б���
        /// </summary>
        public const string Plan_Show_RowHeader = "P00542";

        /// <summary>
        /// �Ƿ�����ͨ��������ȷ��ѡ������
        /// </summary>
        public const string Plan_Num_SelectRow = "P00543";

        /// <summary>
        /// �Ƿ�Լƻ����Ƿ�Ϊ������ж�
        /// </summary>
        public const string Plan_NumZero_Valid = "P00544";

        /// <summary>
        /// �ɹ��Ƿ���Ҫ��˺���Ч
        /// </summary>
        public const string Stock_Need_Approve = "P00551";

        /// <summary>
        /// �ɹ����ʱ �Ƿ������޸���Ӧ�ļƻ���Ϣ
        /// </summary>
        public const string Stock_Edit_InPlan = "P00553";

        /// <summary>
        /// �ɹ��ƻ�/���ʱ �Ƿ������޸ļƻ������
        /// </summary>
        public const string Stock_Edit_Price = "P00554";

        /// <summary>
        /// �ɹ�ָ��ʱ�Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾
        /// </summary>
        public const string Stock_Use_DefaultData = "P00555";

        /// <summary>
        /// ҩƷ����Ƿ���Ҫ��� ԭ���� 500002
        /// </summary>
        public const string In_Need_Approve = "P00570";

        /// <summary>
        /// �˳�ʱ�Ƿ񱣴���һ�β���Ȩ������
        /// </summary>
        public const string In_Save_Priv = "P00571";

        /// <summary>
        /// ҩƷ��׼���ʱ�Ƿ����ҩƷ�ֵ���Ϣ����ۼ���׼�ĺ� 0 ������ 1 ����  ԭ���� 510002
        /// </summary>
        public const string In_EditPrice_WhenApprove = "P00572";

        /// <summary>
        /// ���ó���ʱĬ�ϼӼ���
        /// </summary>
        public const string Out_Transfer_DefaultRate = "P00573";

        /// <summary>
        /// ���ó���ʱ�Ƿ�ʹ��������
        /// </summary>
        public const string Out_Transfer_UseWholePrice = "P00574";

        /// <summary>
        /// ����ʱ�Ƿ�ѡ�����Ž��г���
        /// {DE934736-B2C2-44a4-A218-2DC38E1620BA}
        /// </summary>
        public const string Out_Choose_BatchNO = "P00575";

        /// <summary>
        /// ����λ�������������λ��
        /// </summary>
        public const string Max_Place_Code = "P00580";

        /// <summary>
        /// �Ƿ�������Ч�ھ�ʾ
        /// </summary>
        public const string Valid_Warn_Enabled = "P00581";

        /// <summary>
        /// ��Ч�����ʾ����
        /// </summary>
        public const string Valid_Warn_Days = "P00582";

        /// <summary>
        /// ��Ч�ھ�ʾ��ɫ
        /// </summary>
        public const string Valid_Warn_Color = "P00583";

        /// <summary>
        /// ��Ч�ڲ���ʵʱ��ȡ��ȡ��ʽ
        /// </summary>
        public const string Valid_Warn_SourceRealTime = "P00584";

        /// <summary>
        /// �Ƿ���ÿ�������ޱ���
        /// </summary>
        public const string Store_Warn_Enabled = "P00585";

        /// <summary>
        /// ����ʱ�Ƿ���õ�����Ϣ��ʽ ��ʹ��������ɫ��ʾ
        /// </summary>
        public const string Store_Warn_Msg = "P00586";

        /// <summary>
        /// ����������ɫ��ʾ
        /// </summary>
        public const string Store_Warn_Color = "P00587";

        /// <summary>
        /// /ҩ��/ҩ��ͨ�ò�ѯ����
        /// </summary>
        public const string Query_Commo_Type = "P00900";

        /// <summary>
        /// ҩ���ѯ����
        /// </summary>
        public const string Query_PI_Type = "P00901";

        /// <summary>
        /// ����Ԥ�ۿ������ 0ҽ��վԤ�� 1�շ�ʱԤ�� by Sunjh 2010-9-28 {0B55D338-67A8-415a-84F1-7287FB1454A5}
        /// </summary>
        public const string OutDrug_Pre_Out_Type = "P01015";

        /// <summary>
        /// һ������Ƿ�����ϴ�����Զ������Ч�ڡ����š������ by Sunjh 2010-10-28 {97C93751-7EED-4160-931A-EC77C1F4E291}
        /// </summary>
        public const string CommonInput_Auto_FillInfo = "P01016";

        /// <summary>
        /// �����䷢ҩʱ�Ƿ񵥺��ڼ������лس����Զ��䷢ҩȷ�� by Sunjh 2010-11-1 {E1FDEF4A-BBA8-4210-BDBA-6FA8130244B9}
        /// </summary>
        public const string OutDrug_AutoSave_ByEnter = "P01017";

        #region �������ϲ���

        /// <summary>
        /// ������ �� ҩ�������Զ���ӡʱ���Ƿ�ͬʱ��ɳ���
        /// </summary>
        //public const string InDrug_AutoPrint_Output = "P00117";

        /// <summary>
        /// ������ �� ����Ա���޸�Ȩ��ʱ �Ƿ������ҩƷ��Ϣ��ѯ���
        /// </summary>
        //public const string Query_No_EditPriv = "P00503";

        /// <summary>
        /// ������ �� ����Ա���޸�Ȩ��ʱ �Ƿ������ҩƷ��Ϣ����/��ӡ
        /// </summary>
        //public const string Export_No_EditPriv = "P00504";

        /// <summary>
        /// ������ ��  סԺҩ����ҩ��׼ʱ�Ƿ������ֺ�׼  
        /// </summary>
        //public const string InDrug_Part_Approve = "P00111";

        /// <summary>
        /// ������ �� ҩ����ҩ����ӡ���� �Ƿ�Ԥ����ʾҪ��ӡ�İ�ҩ��
        /// </summary>
        //public const string InDrug_Priview_Bill = "P00112";

        /// <summary>
        /// �� �� �����ǩ��ӡ�ӿ�ʵ��
        /// </summary>
        //public const string Clinic_Print_Label = "P01000";

        /// <summary>
        /// �� �� �����ҩ�嵥��ӡ�ӿ�ʵ��
        /// </summary>
        //public const string Clinic_Print_Bill = "P01001";

        /// <summary>
        /// �� �� סԺ��ҩ��ӡ�ӿں���
        /// </summary>
        //public const string Inpatient_Print_Fun = "P01009";

        /// <summary>
        /// �� �� סԺ��ҩ���ӿ�ʵ��(���ؼ�)
        /// </summary>
        //public const string Inpatient_Print_Bill = "P01010";

        /// <summary>
        /// �� �� ��ⵥ
        /// </summary>
        //public const string Pha_Input_Bill = "P01011";

        /// <summary>
        ///  �� �� ���ⵥ
        /// </summary>
        //public const string Pha_Output_Bill = "P01012";

        /// <summary>
        /// �� �� ������
        /// </summary>
        //public const string Pha_Chage_Bill = "P01013";

        /// <summary>
        /// �� ��  �������ӿ�
        /// </summary>
        //public const string Clinic_Print_Recipe = "P01014";

        /// <summary>
        /// �� ��  ���ñ�ǩ��ӡ�ӿ�
        /// </summary>
        //public const string Compound_Print_Label = "P01015";

        #endregion

        #endregion

        private System.Collections.Hashtable hsDescription;

        public PharmacyConstant()
        {
            hsDescription = new System.Collections.Hashtable();

            this.hsDescription.Add(Can_Set_NoSplitAtAll, "�������ά��ʱ ҩƷ�Ƿ��������Ϊ���ɲ������");
            this.hsDescription.Add(Can_Set_SplitAndNoInteger, "�������ά��ʱ ҩƷ�Ƿ��������Ϊ�ɲ�ֲ�ȡ��");
            this.hsDescription.Add(Can_Set_SplitAndUpperToInteger, "�������ά��ʱ ҩƷ�Ƿ��������Ϊ�ɲ����ȡ��");
            this.hsDescription.Add(Can_Set_NosplitAndDayToInteger, "�������ά��ʱ ҩƷ�Ƿ��������Ϊ���ɲ�ֵ���ȡ��");
            this.hsDescription.Add(Can_Set_SplitAndDeptToInteger, "�������ά��ʱ ҩƷ�Ƿ��������Ϊ�ɲ�ְ�����ȡ��");
            this.hsDescription.Add(Can_Set_SplitAndNurceCellToInteger, "�������ά��ʱ ҩƷ�Ƿ��������Ϊ�ɲ�ְ�����ȡ��");
            this.hsDescription.Add(Can_Set_NoSplitAndPackUnit, "�������ά��ʱ ҩƷ�Ƿ��������Ϊ���ɲ�ְ���װ��λȡ�����û��߿��");
          
            //
            this.hsDescription.Add(InDrug_Need_Approve, "סԺҩ����ҩ�Ƿ���Ҫ��׼");

            //ucDrugBillApprove ���� InitCtrlParam
            this.hsDescription.Add(InDrug_Need_Priv, "סԺҩ����ҩ��׼ʱ�Ƿ����Ȩ���ж� ��ֻ��ҩʦ/ҩ��ʦ�����׼����");
            //this.hsDescription.Add(InDrug_Part_Approve, "סԺҩ����ҩ��׼ʱ�Ƿ������ֺ�׼");

            //ucDrugDetail ���� InitControlParam
            //this.hsDescription.Add(InDrug_Priview_Bill, "ҩ����ҩ����ӡ���� �Ƿ�Ԥ����ʾҪ��ӡ�İ�ҩ��");

            //ucDrugMessage ���� InitControlParam
            this.hsDescription.Add(InDrug_Auto_Check, "��������Ӵ���ҩ��Ϣʱ �Ƿ��Զ�ѡ��");

            //ucDrugDetail ���� InitControlParam
            this.hsDescription.Add(InDrug_Show_PatientTot, "ҩ����ҩ����ӡ���� �Ƿ���ʾ�������ڻ��߷�ҩ������Ϣ");
            this.hsDescription.Add(InDrug_Show_DeptTot, "ҩ����ҩ����ӡ���� �Ƿ���ʾ�����ݵĿ��һ��ܷ�ҩ��Ϣ");

            //tvDrugMessage ���� InitControlParam
            this.hsDescription.Add(InDrug_Show_DeptFirst, "����ҩ�����б���ʾʱ �Ƿ񰴿������ȷ�ʽ��ʾ");
            //this.hsDescription.Add(InDrug_AutoPrint_Output, "ҩ�������Զ���ӡʱ �Ƿ�ͬʱ��ɳ���");

            this.hsDescription.Add(InDrug_Pre_Out, "סԺ�շ�ʱ �Ƿ�ʹ��Ԥ�ۿ�淽ʽ");

            //{DE934736-B2C2-44a4-A218-2DC38E1620BA}
            this.hsDescription.Add(Out_Choose_BatchNO, "����ʱ�Ƿ�ѡ�����Ž��г���");

            //ucDrugTerminal ���� InitControlParam
            this.hsDescription.Add(Terminal_Save_Refresh, "���﷢ҩ�ն˱���� �Ƿ�ˢ��");

            //ucClinicDrug ���� InitControlParam
            this.hsDescription.Add(OutDrug_Show_Days, "���﷢ҩ�����Ƿ���ʾ����");
            this.hsDescription.Add(OutDrug_Print_List, "��ҩȷ�Ϻ��Ƿ��ӡ��ҩ�嵥");
            this.hsDescription.Add(OutDrug_Print_Recipe, "��ҩȷ�Ϻ��Ƿ��ӡ����");

            //ucClinicTree ���� InitControlParam
            this.hsDescription.Add(OutDrug_OperCode_Length, "��ҩ����ʱ���Ų���λ�� ��1 ���貹λ  ԭ���Ʋ���ֵ 500007");
            this.hsDescription.Add(OutDrug_Need_Priv, "��/��ҩ�����Ƿ����Ȩ�޿��� (ֻ��ҩʦ���Բ���) ԭ���Ʋ���ֵ 500011");
            this.hsDescription.Add(OutDrug_Print_BackRecipe, "�����ǩ�Զ���ӡʱ  �Ƿ�Դ������ϼ�¼�Ĵ�������ӡ ԭ���Ʋ���ֵ500014");

            this.hsDescription.Add(OutDrug_Pre_Out, "�����շ�ʱ�Ƿ����Ԥ�ۿ�����");
            this.hsDescription.Add(OutDrug_Show_OldSended, "���﷢ҩʱ�Ƿ���ʾ�������ҩ��Ϣ");

            //{F8D76CE8-6A0C-469b-AC43-4F69B2FCBAD8} 
            this.hsDescription.Add(OutDrug_Auto_Druged, "������ҩ�����Զ���ҩģʽ");


            //ucPharmacyManager ���� InitControlParam
            this.hsDescription.Add(NewDrug_Need_Approve, "����ҩƷ�Ƿ�����˺���Ч");

            //ucPharmacyQuery ���� InitControlParam
            this.hsDescription.Add( Set_Item_SpecialFlag, "ҩƷ�ֵ���Ϣά���У���������Ϣ����" );         
   
            //�������ϲ���
            //this.hsDescription.Add(Query_No_EditPriv, "����Ա���޸�Ȩ��ʱ �Ƿ������ҩƷ��Ϣ��ѯ���");
            //this.hsDescription.Add(Export_No_EditPriv, " ����Ա���޸�Ȩ��ʱ �Ƿ������ҩƷ��Ϣ����/��ӡ");

            this.hsDescription.Add(Max_PackQty_Digit, "ҩƷ��Ϣά��ʱ  ���ڰ�װ����������������λ��");
            this.hsDescription.Add(Max_BaseDose_Digit, "ҩƷ��Ϣά��ʱ  ���ڻ�������������������λ��");
            this.hsDescription.Add(Max_Price_Digit, "ҩƷ��Ϣά��ʱ  ���ڼ۸�������������λ��");
            this.hsDescription.Add(Max_NameCustomeCode_Digit, "��Ʒ���Զ�����������������λ��");
            this.hsDescription.Add(Max_CustomeCode_Digit, "�����Զ�����������������λ��");
            this.hsDescription.Add(Have_Regular_Tab, "�Ƿ�����ͨ����ά�����Tab˳��");
            this.hsDescription.Add(Have_English_Tab, "�Ƿ�����Ӣ����ά�����Tab˳��");
            this.hsDescription.Add(Have_Code_Tab, "�Ƿ��������/���ұ���ά�����Tab˳��");
            this.hsDescription.Add(Nostrum_Manage_Store, "Э������������ ����������� ���շ�ʱ�����ϸ ���򲻽��в��");

            //ucCheckManager InitData�����ڴ���
            this.hsDescription.Add(Check_With_Batch, " ҩƷ�̵��Ƿ����Ž����̵�");
            this.hsDescription.Add(Check_History_State, "��ʷ�̵㵥��ȡʱ�Ƿ�ֻȡ���״̬ ");

            //ucInPlan  ���� InitControlParam
            this.hsDescription.Add(Plan_Expand_Days, "�Զ����ɼƻ�ʱ �����վ��������������� ͳ��Ĭ������");
            this.hsDescription.Add(Plan_Show_RowHeader, "�б�ѡ��ؼ��Ƿ���ʾ�б���");
            this.hsDescription.Add(Plan_Num_SelectRow, "�Ƿ�����ͨ��������ȷ��ѡ������");
            this.hsDescription.Add(Plan_NumZero_Valid, "�Ƿ�Լƻ����Ƿ�Ϊ������ж�");

            //ucStockPlan InitControlParam
            this.hsDescription.Add(Stock_Need_Approve, "�ɹ��Ƿ���Ҫ��˺���Ч");
            this.hsDescription.Add(Stock_Edit_InPlan, "�ɹ����ʱ �Ƿ������޸���Ӧ�ļƻ���Ϣ");
            this.hsDescription.Add(Stock_Edit_Price, "�ɹ��ƻ�/���ʱ �Ƿ������޸ļƻ������");
            this.hsDescription.Add(Stock_Use_DefaultData, "�ɹ�ָ��ʱ�Ƿ�ʹ���ֵ���Ϣ��Ĭ�ϵĹ�����˾");

            this.hsDescription.Add(In_Need_Approve, "ҩƷ����Ƿ���Ҫ��� ԭ���� 500002");

            //ucIMAInOutBase InitControlParam
            this.hsDescription.Add(In_Save_Priv, "�˳�ʱ�Ƿ񱣴���һ�β���Ȩ������");

            this.hsDescription.Add(In_EditPrice_WhenApprove, "ҩƷ��׼���ʱ�Ƿ����ҩƷ�ֵ���Ϣ����ۼ���׼�ĺ� 0 ������ 1 ����  ԭ���� 510002");
            
            this.hsDescription.Add(Out_Transfer_DefaultRate,"���ó���ʱĬ�ϼӼ���");
            this.hsDescription.Add(Out_Transfer_UseWholePrice,"���ó���ʱ�Ƿ�ʹ��������");

            //ucStorageManager InitControlParam
            this.hsDescription.Add(Max_Place_Code, "����λ�������������λ��");
            this.hsDescription.Add(Valid_Warn_Enabled, "�Ƿ�������Ч�ھ�ʾ");
            this.hsDescription.Add(Valid_Warn_Days, "��Ч�����ʾ����");
            this.hsDescription.Add(Valid_Warn_Color, "��Ч�ھ�ʾ��ɫ");
            this.hsDescription.Add(Valid_Warn_SourceRealTime, "��Ч�ڲ���ʵʱ��ȡ��ȡ��ʽ");
            this.hsDescription.Add(Store_Warn_Enabled, "�Ƿ���ÿ�������ޱ���");
            this.hsDescription.Add(Store_Warn_Msg, "����ʱ�Ƿ���õ�����Ϣ��ʽ ��ʹ��������ɫ��ʾ");
            this.hsDescription.Add(Store_Warn_Color, "����������ɫ��ʾ");

            //ucGeneryColor 
            this.hsDescription.Add(Query_Commo_Type, "ҩ��/ҩ��ͨ�ò�ѯ����");
            this.hsDescription.Add(Query_PI_Type, "ҩ���ѯ����");

            //�������ϲ���
            //this.hsDescription.Add(Clinic_Print_Label, "�����ǩ��ӡ�ӿ�ʵ��");
            //this.hsDescription.Add(Clinic_Print_Bill, "�����ҩ�嵥��ӡ�ӿ�ʵ��");
            //this.hsDescription.Add(Inpatient_Print_Bill, "סԺ��ҩ���ӿ�ʵ��(���ؼ�)");

            //�������ϲ���
            //this.hsDescription.Add(Inpatient_Print_Fun, "סԺ��ҩ��ӡ�ӿں���");

            //�������ϲ���
            //this.hsDescription.Add(Pha_Input_Bill, "��ⵥ��");
            //this.hsDescription.Add(Pha_Output_Bill, "���ⵥ��");
            //this.hsDescription.Add(Pha_Chage_Bill, "��������");

            this.hsDescription.Add(OutDrug_Warn_Druged, "������ҩʱ �Ƿ���п�澯���ж�");
            this.hsDescription.Add(OutDrug_Warn_Send, "���﷢ҩʱ �Ƿ���п�澯���ж�");
            this.hsDescription.Add(OutDrug_Pre_Out_Type, "����Ԥ�ۿ������0ҽ��վԤ��1�շ�ʱԤ��");
            this.hsDescription.Add(CommonInput_Auto_FillInfo, "һ������Ƿ�����ϴ�����Զ������Ч�ڡ����š������");
            this.hsDescription.Add(OutDrug_AutoSave_ByEnter, "�����䷢ҩʱ�Ƿ񵥺��ڼ������лس����Զ��䷢ҩȷ��");
            this.hsDescription.Add(OutDrug_Auto_Send, "����ҩ���Ƿ�����Զ���ҩģʽ");

            //�������ϲ���
            //this.hsDescription.Add(Clinic_Print_Recipe,"��������ӡ�ӿ�ʵ��");

            //this.hsDescription.Add(Compound_Print_Label, "���ñ�ǩ��ӡ�ӿ�");

        }

        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="ctrlCode">��������</param>
        /// <returns>�ɹ��ҵ����ز������� δ�ҵ����ؿ�����</returns>
        public string GetParamDescription(string ctrlCode)
        {
            if (this.hsDescription.ContainsKey(ctrlCode))
            {
                return this.hsDescription[ctrlCode] as string;
            }
            else
            {
                return "";
            }
        }
    }

   /// <summary>
   /// ����ҺŲ���
   /// </summary>
    public class RegistrationConstant
    {
        
       
        /// <summary>
        /// ����Һţ��Һż�����ʾ����
        /// </summary>
        public const string Const_Display_RegLevel_ColumnNumber = "400001";
        /// <summary>
        /// ����Һţ��Һſ�����ʾ����
        /// </summary>
        public const string Const_Display_RegDept_ColumnNumber = "400002";
        /// <summary>
        /// ����Һţ���ͬ��λ��ʾ����
        /// </summary>
        public const string Const_Display_RegPact_ColumnNumber = "400003";
        /// <summary>
        /// ����Һţ����������ʾ����
        /// </summary>
        public const string Const_Display_RegProfessor_ColumnNumber = "400004";
        /// <summary>
        /// ����Һţ���ͬ��λĬ�ϴ���
        /// </summary>
        public const string Const_Display_DefaultPact = "400005";
        /// <summary>
        /// �����˺ţ������˺�����
        /// </summary>
        public const string Const_Allow_QuitReg_Days = "400006";
        /// <summary>
        /// ����Һţ����ѻ��������չҺ��޶�
        /// </summary>
        public const string Const_Allow_PubPatient_RegLimitCost = "400007";
        /// <summary>
        /// ����Һţ�����Ƿ���
        /// </summary>
        public const string Const_Dialog_IsPub = "400008";
        /// <summary>
        /// ҽ�������Ƿ�ά��ʱͬʱ����
        /// </summary>
        public const string Const_Yb_Same_Time = "400009";
        /// <summary>
        /// ����Һţ�ר�Һ��Ƿ��������
        /// </summary>
        public const string Const_RegProfessor_IsFirstDept = "400010";
        /// <summary>
        /// ����Һţ��Ƿ�ֻ��ʾ�������
        /// </summary>
        public const string Const_Display_Only_Dept = "400011";
        /// <summary>
        /// �Ƿ�����ά����������
        /// </summary>
        public const string Const_Is_SYBX = "400012";
        /// <summary>
        /// ����Һţ������ӡ�Һŷ�Ʊ����
        /// </summary>
        public const string Const_RegInvoiceDays = "400014";
        /// <summary>
        /// �Һ��Ƿ��������Ű��޶�
        /// </summary>
        public const string Const_Allow_Beyond_Limit = "400015";
        /// <summary>
        /// ����Һţ�1��������ȡ��Ʊ��,2������Ա��ȡ��Ʊ(����Ʊ��)
        /// </summary>
        public const string Const_GetRecipe_Way = "400019";
        /// <summary>
        /// ����Һţ�����Ƿ�¼��ICD��
        /// </summary>
        public const string Const_IsICD = "400016";
        /// <summary>
        /// ����Һţ���ӡ�����վ�Invoice?Recipe
        /// </summary>
        public const string Const_InvoiceType = "400017";

        /// <summary>
        /// ����Һţ��Ű�Ĭ��ʱ���,0Ĭ��Ϊ�������
        /// </summary>
        public const string Const_Default_Noon = "400018";
        /// <summary>
        /// ����Һţ��Ƿ�����ԤԼ��ˮ�Ŵ�
        /// </summary>
        public const string Const_Jump_To_Yuyue = "400020";
        
        /// <summary>
        /// ����Һţ����ҡ�ҽ�������б��Ƿ���ʾȫԺ,Ĭ����
        /// </summary>
        public const string Const_Alow_Quanyuan = "400022";

        /// <summary>
        /// ����Һţ�����Ƿ�������ԤԼʱ��δ�
        /// </summary>
        public const string Const_Jump_To_YuyueTime = "400023";
        /// <summary>
        /// ����Һţ�����ʱ�Ƿ���ʾ
        /// </summary>
        public const string Const_IsSaveMsg = "400024";
        /// <summary>
        /// 400025	����Һţ��Ű��Ƿ�����ҽ�����
        /// </summary>
        public const string Const_Schama_Doct_IsDoctType = "400025";
        /// <summary>
        /// ����Һţ��Ƿ�ԤԼ�ſ�����������ֳ���ǰ���
        /// </summary>
        public const string Const_IsBookingBeforeLocal = "400026";
        /// <summary>
        /// ����Һţ��Һŷ���otherfee������ 0:����(��ҽר��) 1���������� 2��������
        /// </summary>
        public const string Const_Is_AirCondition = "400027";
        /// <summary>
        /// ����Һţ�ר�Һ��Ƿ����ֽ��ڼ���
        /// </summary>
        public const string Const_IsDivision_ProLevel = "400028";
        /// <summary>
        /// ����Һţ����ź��Ƿ���Ϊ�Ӻ�
        /// </summary>
        public const string Const_IsMultAdd = "400029";
        /// <summary>
        /// ����Һ�-�Ű��Ƿ�����Һż���
        /// </summary>
        public const string Const_IsInputRegLevel = "400030";
        /// <summary>
        /// ����Һţ���÷�Ʊ�ŷ��� 
        /// </summary>
        public const string Const_GetInvoiceWay = "400031";
        /// <summary>
        /// ���캯��
        /// </summary>
        public RegistrationConstant()
        {
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="ctrlCode">��������</param>
        /// <returns>�ɹ��ҵ����ز������� δ�ҵ����ؿ�����</returns>
        //public string GetParamDescription(string ctrlCode)
        //{
        //    if (this.hsDescription.ContainsKey(ctrlCode))
        //    {
        //        return this.hsDescription[ctrlCode] as string;
        //    }
        //    else
        //    {
        //        return "";
        //    }
        //}
    }

    public class AccountConstant
    {
        #region �ʻ��������ó���
        /// <summary>
        /// �½����Ƿ���ȡ���ɱ��ѣ�0=����ȡ��1=��ȡ��2=���������ȡ
        /// </summary>
        public const string IsAcceptCardFee = "800001";
        /// <summary>
        /// �½����ɱ��ѽ��
        /// </summary>
        public const string AcceptCardFee = "800002";
        /// <summary>
        /// �����Ƿ���ȡ���ɱ��� 0=����ȡ��1=��ȡ��2=���������ȡ
        /// </summary>
        public const string IsAcceptChangeCardFee = "800003";
        /// <summary>
        /// ����ʱ�ɱ��ѽ��
        /// </summary>
        public const string AcceptChangeCardFee = "800004";
        /// <summary>
        /// �˿�ʱ�˷� 0=���ˣ�1=�˷�
        /// </summary>
        public const string ReturnCardReturnFee = "800014";

        public const string ChangeCardIsStopAccount = "8000005";
        /// <summary>
        /// ���￨����ʱ���Ƿ�ʵʱ�жϻ��ߺ�ͬ��λ��0-���жϣ�1-�ж�
        /// {C49AFFB1-D0EA-41bf-AD60-9F921D91E93D}
        /// </summary>
        public const string BuildCardIsJudgePact = "800007";
        /// <summary>
        /// Ԥ����۷�ʱ���Ƿ���Ҫ��֤����
        /// 0 = ����Ҫ��1 = ��Ҫ��
        /// {5CCCF7F7-E9A5-4982-A5AF-C3ADB99DD9F0}
        /// </summary>
        public const string NeedCheckAccountPW = "800008";
        /// <summary>
        /// ���ʻ�ʱ���Ƿ���Ҫ��֤��Ч֤����0-��Ҫ��1-����Ҫ
        /// {15148270-E4C9-4724-8227-524C9C0A3076}
        /// </summary>
        public const string JudgeCredentialCreating = "800009";
        /// <summary>
        /// ����ָ����С������Ŀ����ִ�п����޶�
        /// {A3FBD5CE-CB44-4e42-BAE2-4397E4F4AEC0}
        /// </summary>
        public const string MustExcuAccountFee = "800010";
        /// <summary>
        /// ҽ�����ID, ����ԡ�|���ֿ�
        /// {870D6A8C-9B17-4e33-A0B6-DB0F1CF15BAE}
        /// </summary>
        public const string SIPactUnitID = "800011";
        /// <summary>
        /// ��֧���ն˿۷ѵĺ�ͬ��λ
        /// </summary>
        public const string UnTerminalPactCode = "800012";
        #endregion

        System.Collections.Hashtable hasTable = null;
        public AccountConstant()
        {
            hasTable = new System.Collections.Hashtable();
            hasTable.Add(IsAcceptCardFee, "����ʱ�Ƿ���ȡ���ɱ���");
            hasTable.Add(AcceptCardFee, "�����ɱ��ѽ��");
            hasTable.Add(IsAcceptChangeCardFee, "�������Ƿ���ȡ���ɱ���");
            hasTable.Add(AcceptChangeCardFee, "������ʱ�ɱ��ѽ��");
            hasTable.Add(ChangeCardIsStopAccount, "�������Ƿ�ͣ���ʻ�");
            hasTable.Add(BuildCardIsJudgePact, "���￨����ʱ���Ƿ�ʵʱ�жϻ��ߺ�ͬ��λ");
            hasTable.Add(NeedCheckAccountPW, "Ԥ����۷�ʱ���Ƿ���Ҫ��֤����");
            hasTable.Add(JudgeCredentialCreating, "���ʻ�ʱ���Ƿ���Ҫ��֤��Ч֤����0-��Ҫ��1-����Ҫ"); // {15148270-E4C9-4724-8227-524C9C0A3076}
            hasTable.Add(MustExcuAccountFee, "ָ����С������Ŀ����ִ�п����޶�");
            hasTable.Add(SIPactUnitID, "ҽ�����ID"); // {870D6A8C-9B17-4e33-A0B6-DB0F1CF15BAE}
            hasTable.Add(UnTerminalPactCode, "��֧���ն˿۷ѵĺ�ͬ��λ"); // {9635BF11-D633-409e-8880-2DB29CB830F7}
            hasTable.Add(ReturnCardReturnFee, "�˿�ʱ�˷� 0=���ˣ�1=�˷�"); 
        }
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        /// <param name="ctrlCode">��������</param>
        /// <returns>�ɹ��ҵ����ز������� δ�ҵ����ؿ�����</returns>
        public  string GetParamDescription(string ctrlCode)
        {
            if (this.hasTable.ContainsKey(ctrlCode))
            {
                return this.hasTable[ctrlCode] as string;
            }
            else
            {
                return "";
            }
        }
    }



    /// <summary>
    /// ҽ������ ��2��ͷ {97FA5C9D-F454-4aba-9C36-8AF81B7C9CCF}
    /// </summary>
    public class MetConstant
    {
        /// <summary>
        /// ��Һ���������
        /// </summary>
        //public const string CircuitCard_MaxLine = "O00001";

        ///// <summary>
        ///// ������ת�����
        ///// </summary>
        //public const string Death_ZG_ID = "200039";

        ///// <summary>
        ///// ����������ʾ������
        ///// </summary>
        //public const string DeathPatient_View_Days = "200040";

        ///// <summary>
        ///// ��ʿվ�Ƿ�������ʾ��������
        ///// </summary>
        //public const string Is_View_DeathPatient = "200041";

        /// <summary>
        /// Сʱ�Ʒ�ҽ����Ƶ�δ���
        /// </summary>
        public const string Hours_Frequency_ID = "200042";

        /// <summary>
        /// ҽ��������ʽ  {D05BA7C4-3158-48aa-B581-0211E2CAAD4C}  ���ӿ��Ʋ���
        /// </summary>
        public const string Order_RetidyType = "200210";           //ҽ����������ʽ  ��ʽһ������ԭҽ�� ��������״̬ҽ��   ��ʽ�����޸�ԭҽ��Ϊ����״̬ ������Чҽ��

        /// <summary>
        /// �˷ѵ���ӡ�ӿ�
        /// </summary>
        //public const string IQuitFeeInterface = "200043";

        ///// <summary>
        ///// ����ʱ�Ƿ���ȡ����
        ///// </summary>
        //public const string Is_Arive_Fee = "200044";

        ///// <summary>
        ///// �Ƿ��ӡ�˷����뵥
        ///// </summary>
        //public const string Is_Print_QuitFeeSheet = "200045";

        ///// <summary>
        ///// ��ˡ��ֽ�ҽ��ʱ�Ƿ��жϿ��
        ///// </summary>
        //public const string JUDGE_DRUG_STORE = "200046";

        ///// <summary>
        ///// �����۸񴰿�ѡ��ӿ�
        ///// </summary>
        //public const string ISelectUndrugPrice = "200047";
        private System.Collections.Hashtable hsDescription;
        public MetConstant()
        {
            hsDescription = new System.Collections.Hashtable();

            //this.hsDescription.Add(CircuitCard_MaxLine, "ÿ����Һ�����ܴ�ӡ���������");
            //this.hsDescription.Add(Death_ZG_ID, "������ת�����");
            //this.hsDescription.Add(DeathPatient_View_Days, "����������ʾ������");
            //this.hsDescription.Add(Is_View_DeathPatient, "��ʿվ�Ƿ�������ʾ��������");
            //this.hsDescription.Add(IQuitFeeInterface, "�˷ѵ���ӡ�ӿ�");
            //this.hsDescription.Add(Is_Arive_Fee, "����ʱ�Ƿ���ȡ����");
            //this.hsDescription.Add(JUDGE_DRUG_STORE, "�����ʱҽ�����ֽⳤ��ҽ��ʱ�Ƿ��жϿ��");
            //this.hsDescription.Add(ISelectUndrugPrice, "�����۸�ѡ�񴰿�");
        }
    }
}
