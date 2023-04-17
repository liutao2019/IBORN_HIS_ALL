using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;
using System.Data;

namespace FS.HISFC.BizProcess.Integrate
{
    /// <summary>
    /// [��������: ҩƷ���ҵ����]<br></br>
    /// [�� �� ��: ������]<br></br>
    /// [����ʱ��: 2006-10]<br></br>
    /// <�޸ļ�¼>
    ///    1.�޸����ɷ�������ⵥ���з������ַ������BUG by Sunjh 2010-8-17 {FA29FD4A-7379-49ae-847E-ED4BAB67E815}
    ///    2.סԺ��ҩ�����Ż����޸ĳ�����Ϊ�˲�Ӱ��סԺ��ҩ֮��ĳ������жϡ� by Sunjh 2010-8-30 {32F6FA1C-0B8E-4b9c-83B6-F9626397AC7C}
    ///    3.ҩƷȫԺ���޹��� by Sunjh 2010-10-3 {1A398A34-0718-47ed-AAE9-36336430265E}
    ///    4.���������շ�(�����÷����Ӳ��Ϸ���ȡ) by Sunjh 2010-10-26 {74D77EE3-F04E-4d94-A2A3-24902B93C619}
    /// </�޸ļ�¼>
    /// </summary>
    public class Pharmacy : IntegrateBase,FS.FrameWork.WinForms.Forms.IInterfaceContainer
    {
        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static Pharmacy()
        {

        }

        #region ��̬��

        /// <summary>
        /// סԺ��ҩ�Ƿ���Ҫ��׼
        /// </summary>
        internal static bool IsNeedApprove = false;

        /// <summary>
        /// סԺ��ҩͬʱ�Ʒ�
        /// </summary>
        internal static bool IsApproveCharge = false;

        /// <summary>
        /// סԺ��ҩ�Ƿ���Ҫ��׼
        /// </summary>
        internal static bool IsReturnNeedApprove = false;

        /// <summary>
        /// סԺ��ҩͬʱ�˷�
        /// </summary>
        internal static bool IsReturnCharge = false;

        /// <summary>
        /// �����Ƿ�Ԥ����
        /// </summary>
        internal static bool IsClinicPreOut = false;

        /// <summary>
        /// סԺ�Ƿ�Ԥ����
        /// </summary>
        internal static bool IsInPatientPreOut = false;

        /// <summary>
        /// Э�������Ƿ������
        /// </summary>
        internal static bool isNostrumManageStore;

        private string originalOutBillCode = string.Empty;
        #endregion

        #region SetDB ���� ���� ��֤�� Err��Ϣ����ͨ��Integrateֱ�ӻ�ȡ ���ص���ҵ���

        #endregion

        #region ����

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;
            ctrlMgr.SetTrans(trans);
            ctrlIntegrate.SetTrans(trans);
            itemManager.SetTrans(trans);
            drugStoreManager.SetTrans(trans);
            feeInpatientManager.SetTrans(trans);
            radtIntegrate.SetTrans(trans);
        }

        /// <summary>
        /// ҩƷ������
        /// </summary>
        protected FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
        
        /// <summary>
        /// ҩ��������
        /// </summary>
        protected FS.HISFC.BizLogic.Pharmacy.DrugStore drugStoreManager = new FS.HISFC.BizLogic.Pharmacy.DrugStore();

        /// <summary>
        /// ����������
        /// </summary>
        protected FS.FrameWork.Management.ControlParam ctrlMgr = new FS.FrameWork.Management.ControlParam();

        /// <summary>
        /// ����������
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

        /// <summary>
        /// ���ù�����
        /// </summary>
        protected FS.HISFC.BizLogic.Fee.InPatient feeInpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();

        /// <summary>
        /// ������
        /// </summary>
        protected FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new RADT();

        protected FS.HISFC.BizLogic.Fee.Outpatient OutPatientfeeManager = new FS.HISFC.BizLogic.Fee.Outpatient();

        /// <summary>
        /// �Һ��ۺ�ҵ���
        /// </summary>
        FS.HISFC.BizProcess.Integrate.Registration.Registration registeIntegrate = new FS.HISFC.BizProcess.Integrate.Registration.Registration();

        #endregion

        #region ���Ʋ�����ȡ

        /// <summary>
        /// ���ݿ���ֵ��ȡ���Ʋ��������ز���ֵ 1 ΪTrue ����ΪFalse
        /// </summary>
        /// <param name="controlCode">����ֵ</param>
        /// <param name="isRefresh">�Ƿ�ˢ����ȡ</param>
        /// <returns></returns>
        public bool QueryControlForBool(string controlCode, bool isRefresh)
        {           
            string ctrlStr = ctrlMgr.QueryControlerInfo(controlCode, isRefresh);
            if (ctrlStr == "1")
                return true;
            else
                return false;
        }

        /// <summary>
        /// ���ݿ���ֵ��ȡ���Ʋ���
        /// </summary>
        /// <param name="controlCode">����ֵ</param>
        /// <param name="isRefresh">�Ƿ�ˢ����ȡ</param>
        /// <returns></returns>
        public string QueryControlForStr(string controlCode, bool isRefresh)
        {
            string ctrlStr = ctrlMgr.QueryControlerInfo(controlCode, isRefresh);
            return ctrlStr;
        }
        #endregion

        #region Ȩ���ж�

        /// <summary>
        /// �ж�ĳ����Ա�Ƿ���ĳһȨ��
        /// </summary>
        /// <param name="class2Code">����Ȩ�ޱ���</param>
        /// <returns>����Ȩ�޷���True ��Ȩ�޷���False</returns>
        public static bool ChoosePiv(string class2Code)
        {
            List<FS.FrameWork.Models.NeuObject> al = null;
            //Ȩ�޹�����
            FS.HISFC.BizLogic.Manager.UserPowerDetailManager privManager = new FS.HISFC.BizLogic.Manager.UserPowerDetailManager();
            //ȡ����Աӵ��Ȩ�޵Ŀ���
            al = privManager.QueryUserPriv(privManager.Operator.ID, class2Code);

            if (al == null || al.Count == 0)
            {
                return false;
            }

            return true;
        }

        #endregion

        #region ��ȡ����/��չ��Ϣ

        //  1��ͨ�õ��ݺŻ�ȡ����
        //  2��ApplyOut�ڻ�ȡPrintLabel��������
        //  3��DrugRecipe�ڻ�ȡ���ҵ�ַ����
        //  4��Э�������Ƿ������

        #region ��ȡͨ�ø�ʽ���ݺ�    {59C9BD46-05E6-43f6-82F3-C0E3B53155CB} ����ⵥ�Ż�ȡ�����޸�

        /// <summary>
        /// ����ⵥ�ݺŻ�ȡ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="isInListNO">�Ƿ���ⵥ��</param>
        /// <returns>�ɹ�������ⵥ��  ʧ�ܷ���null</returns>
        public string GetInOutListNO(string deptCode, bool isInListNO)
        {
            FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.DeptConstant deptCons = phaConsManager.QueryDeptConstant(deptCode);

            string listCode = "";
            if (isInListNO)
            {
                listCode = deptCons.InListNO;
            }
            else
            {
                listCode = deptCons.OutListNO;
            }

            if (string.IsNullOrEmpty(listCode))
            {
                return this.GetCommonListNO(deptCode);
            }
            else
            {
                string nextListCode = this.GetNextListSequence(listCode, true);
                if (isInListNO)
                {
                    deptCons.InListNO = nextListCode;
                }
                else
                {
                    deptCons.OutListNO = nextListCode;
                }
                if (phaConsManager.UpdateDeptConstant(deptCons) == -1)
                {
                    this.Err = "������һ���ݺ����з�������" + phaConsManager.Err;
                    return null;
                }

                return listCode;
            }
        }

        /// <summary>
        /// ����ⵥ������
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="isInListNO">�Ƿ���ⵥ��</param>
        /// <param name="cancelListNO">�������</param>
        /// <returns>�ɹ�����1 ����-1</returns>
        public int CancelInOutListNO(string deptCode, bool isInListNO, string cancelListNO)
        {
            FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.Models.Pharmacy.DeptConstant deptCons = phaConsManager.QueryDeptConstant(deptCode);

            string nowListCode = "";
            if (isInListNO)
            {
                nowListCode = deptCons.InListNO;
            }
            else
            {
                nowListCode = deptCons.OutListNO;
            }

            string tempListCode = this.GetNextListSequence(nowListCode, false);
            if (string.Compare(tempListCode, cancelListNO) == 0)     //˵���Ѿ��������ݺ���� ��������ȡ��
            {
                if (isInListNO)
                {
                    deptCons.InListNO = tempListCode;
                }
                else
                {
                    deptCons.OutListNO = tempListCode;
                }
                return phaConsManager.UpdateDeptConstant(deptCons);
            }

            this.Err = "��һ���е��ݺ���ռ�� ���ܻ���";
            return -1;
        }

        /// <summary>
        /// ��ȡͨ�õ��ݺ� ���ұ���+YYMMDD+��λ��ˮ��
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ������»�ȡ�ĵ��ݺ� ʧ�ܷ���null</returns>
        public string GetCommonListNO(string deptCode)
        {
            FS.FrameWork.Management.ExtendParam extentManager = new FS.FrameWork.Management.ExtendParam();
            this.SetDB(extentManager);

            string ListNO = "";
            decimal iSequence = 0;
            DateTime sysDate = extentManager.GetDateTimeFromSysDateTime().Date;

            //��ȡ��ǰ���ҵĵ��������ˮ��
            FS.HISFC.Models.Base.ExtendInfo deptExt = extentManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "ListCode", deptCode);
            if (deptExt == null)
            {
                return null;
            }
            else
            {
                if (deptExt.Item.ID == "")          //��ǰ�������޼�¼ ��ˮ����Ϊ1
                {
                    iSequence = 1;
                }
                else                                //��ǰ���Ҵ��ڼ�¼ ���������Ƿ�Ϊ���� ȷ����ˮ���Ƿ��1
                {
                    if (deptExt.DateProperty.Date != sysDate)
                    {
                        iSequence = 1;
                    }
                    else
                    {
                        iSequence = deptExt.NumberProperty + 1;
                    }
                }
                //���ɵ��ݺ�
                ListNO = deptCode + sysDate.Year.ToString().Substring(2, 2) + sysDate.Month.ToString().PadLeft(2, '0') + sysDate.Day.ToString().PadLeft(2, '0')
                    + iSequence.ToString().PadLeft(3, '0');

                //���浱ǰ�����ˮ��
                deptExt.Item.ID = deptCode;
                deptExt.DateProperty = sysDate;
                deptExt.NumberProperty = iSequence;
                deptExt.PropertyCode = "ListCode";
                deptExt.PropertyName = "���ҵ��ݺ������ˮ��";

                if (extentManager.SetComExtInfo(deptExt) == -1)
                {
                    return null;
                }
            }
            return ListNO;
        }

        /// <summary>
        /// �����ַ�����ȡ��һ�����ݺŵ���ֵ����
        /// </summary>
        /// <param name="listCode"></param>
        /// <returns></returns>
        private string GetNextListSequence(string listCode, bool isAddSequence)
        {
            string listNum = "";
            string listStr = "";
            //�޸����ɷ�������ⵥ���з������ַ������BUG by Sunjh 2010-8-17 {FA29FD4A-7379-49ae-847E-ED4BAB67E815}
            int numIndex = 0;//listCode.Length;
            for (int i = listCode.Length - 1; i >= 0; i--)
            {
                if (char.IsDigit(listCode[i]))
                {
                    listNum = listCode[i] + listNum;
                }
                else
                {
                    numIndex = i + 1;       //���в��ֽ���λ��
                    break;
                }
            }

            listStr = listCode.Substring(0, numIndex);

            if (string.IsNullOrEmpty(listNum))
            {
                this.Err = "���ݺŸ�ʽ���淶 �޷�������ȡ��һ����";
                return null;
            }
            else
            {
                int listNumLength = listNum.Length;
                string nextListNum = "";
                if (isAddSequence)
                {
                    nextListNum = ((FS.FrameWork.Function.NConvert.ToDecimal(listNum) + 1).ToString()).PadLeft(listNumLength, '0');
                }
                else
                {
                    nextListNum = ((FS.FrameWork.Function.NConvert.ToDecimal(listNum) - 1).ToString()).PadLeft(listNumLength, '0');
                }
                
                return listStr + nextListNum;
            }
        }

        #endregion

        #region ����Sql������ȡ���ݺ� �ô�������ʱ����

        /// <summary>
        /// �������ڼ���ˮ�ŷ�ʽ�����µ��ݺ�
        /// </summary>
        /// <param name="sqlStr">��ȡ���������ˮ�ŵ�sql����</param>
        /// <param name="dateFormat">���ڸ�ʽ�����ɷ�ʽ YYYY MM DD ������ </param>
        /// <param name="iNum">��ˮ��λ��</param>
        /// <param name="formatStr">sql����ʽ���ַ���</param>
        /// <returns>�ɹ����ص��ݺ� ʧ�ܷ���null</returns>
        public string GetCommonListNO(string sqlStr, string dateFormat, int iNum, params string[] formatStr)
        {
            FS.FrameWork.Management.ExtendParam extentManager = new FS.FrameWork.Management.ExtendParam();

            string strSQL = "";
            string tempDate, tempList;
            //��ȡ���ڸ�ʽ���ַ���
            try
            {
                tempDate = extentManager.GetDateTimeFromSysDateTime().ToString(dateFormat, System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ�������ַ������ڴ�����ע���Сд" + ex.Message;
                return null;
            }
            //ȡ���е���󵥺�
            if (extentManager.Sql.GetSql(sqlStr, ref strSQL) == -1)
            {
                this.Err = "û���ҵ�" + sqlStr + "�ֶ�!";
                return null;
            }
            //��ʽ��SQL���
            try
            {
                strSQL = string.Format(strSQL, formatStr);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����Pharmacy.Item.GetMaxBillCode:" + ex.Message;
                return null;
            }
            //ִ��SQL���
            try
            {
                this.Err = "";
                tempList = extentManager.ExecSqlReturnOne(strSQL);
                if (tempList == "-1")
                {
                    this.Err = "SQL���ִ�г���" + this.Err;
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.Err = "SQL���ִ�г���" + ex.Message;
                return null;
            }
            //���õ��ݺ�
            if (tempList.ToString() == "-1" || tempList.ToString() == "")
            {
                tempList = "1".PadLeft(iNum, '0');
            }
            else
            {
                if (tempList.Length < iNum)
                {
                    this.Err = "ָ����ˮ��λ������ �����е��ݺų�ͻ";
                    return null;
                }
                decimal i = FS.FrameWork.Function.NConvert.ToDecimal(tempList.Substring(tempList.Length - iNum, iNum)) + 1;
                tempList = i.ToString().PadLeft(iNum, '0');
            }
            return tempDate + tempList;
        }

        /// <summary>
        /// �������ڼ���ˮ�ŷ�ʽ�����µ��ݺ� Ĭ�ϸ�ʽ YYMMDD + ��λ��ˮ��
        /// </summary>
        /// <param name="sqlStr">��ȡ���������ˮ�ŵ�sql����</param>
        /// <param name="formatStr">sql����ʽ���ַ���</param>
        /// <returns>�ɹ����ص��ݺ� ʧ�ܷ���null</returns>
        public string GetCommonListNO(string sqlStr, params string[] formatStr)
        {
            return this.GetCommonListNO(sqlStr, "yyMMdd", 3, formatStr);
        }

        /// <summary>
        /// �������ڼ���ˮ�ŷ�ʽ�����µ��ݺţ�Ĭ�ϸ�ʽ YYMMDD �� ��λ��ˮ��
        /// </summary>
        /// <param name="sqlStr">��ȡ������󵥾ݺŵ�sql���� Ĭ�ϸ�ʽ������Ϊ ���ұ���  + ���ϵ��ݸ�ʽ�ĵ�����С���� yyMMdd000</param>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ����ص��ݺ� ʧ�ܷ���null</returns>
        public string GetCommonListNO(string sqlStr, string deptCode)
        {
            FS.FrameWork.Management.ExtendParam extentManager = new FS.FrameWork.Management.ExtendParam();

            string tempDate;
            //��ȡ���ڸ�ʽ���ַ���
            try
            {
                tempDate = extentManager.GetDateTimeFromSysDateTime().ToString("yyMMdd", System.Globalization.DateTimeFormatInfo.InvariantInfo);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ�������ַ������ڴ�����ע���Сд" + ex.Message;
                return null;
            }
            return this.GetCommonListNO(sqlStr, deptCode, tempDate);
        }

        #endregion

        /// <summary>
        /// ��ȡ���Ʋ��� Э�������Ƿ������  Ĭ�Ϲ�����
        /// ������� ��Э������ҩƷ����ͨҩƷ���ơ��ɽ�������⡢���ۡ��շѲ������ϸ
        /// ���� Э������ҩƷ���ܽ�������⡢���۲������շѲ����ϸ
        /// </summary>
        public static bool IsNostrumManageStore
        {
            get
            {
                if (isNostrumManageStore == null)
                {
                    FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                    isNostrumManageStore = ctrlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Nostrum_Manage_Store, false, true);
                    return isNostrumManageStore;
                }
                return isNostrumManageStore;
            }
        }

        #endregion

        #region סԺ��ҩ����

         /// <summary>
        /// סԺ��ҩ
        /// </summary>
        /// <param name="alApplyOut">����ҩ������Ϣ</param>
        /// <param name="drugMessage">��ҩ֪ͨ���������°�ҩ֪ͨ(��ҩ������İ�ҩ��������drugMessage.DrugBillClass.Memo��)</param>
        /// <param name="arkDept">ҩ�����</param>
        /// <param name="approveDept">��׼���� Ϊ��ֵʱ����Ϊ��ǰ����</param>
        /// <param name="trans">�ⲿ�������� Ϊ��ֵʱ���Զ���������</param>
        /// <returns></returns>
        public int InpatientDrugConfirm(ArrayList alApplyOut, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, FS.FrameWork.Models.NeuObject arkDept, FS.FrameWork.Models.NeuObject approveDept)
        {
            return InpatientDrugConfirm(alApplyOut, drugMessage, arkDept, approveDept, null);
        }

        /// <summary>
        /// סԺ��ҩ
        /// </summary>
        /// <param name="alApplyOut">����ҩ������Ϣ</param>
        /// <param name="drugMessage">��ҩ֪ͨ���������°�ҩ֪ͨ(��ҩ������İ�ҩ��������drugMessage.DrugBillClass.Memo��)</param>
        /// <param name="arkDept">ҩ�����</param>
        /// <param name="approveDept">��׼���� Ϊ��ֵʱ����Ϊ��ǰ����</param>
        /// <param name="trans">�ⲿ�������� Ϊ��ֵʱ���Զ���������</param>
        /// <returns></returns>
        public int InpatientDrugConfirm(ArrayList alApplyOut, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, FS.FrameWork.Models.NeuObject arkDept, FS.FrameWork.Models.NeuObject approveDept,System.Data.IDbTransaction trans)
        {            
            if (trans == null)      //��������
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
            }

            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region ���ݿ����Ӵ��� ��������
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrateManager = new Fee();
            FS.HISFC.BizLogic.Fee.InPatient feeManager = new FS.HISFC.BizLogic.Fee.InPatient();
            FS.HISFC.BizLogic.Order.Order orderManager = new FS.HISFC.BizLogic.Order.Order();
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new RADT();

            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  Integrate��Ҫ����SetTrans
            feeIntegrateManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            radtIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            ctrlParamIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #endregion

            #region ��ȡ��ҩ����

            int parm = 0;
            //ȡ��ҩ����ˮ�ţ�����������еİ�ҩ���ţ�
            string drugBillID = "";
            if (drugMessage != null)
            {
                drugBillID = drugMessage.DrugBillClass.Memo;
            }
            if (string.IsNullOrEmpty(drugBillID) || drugBillID == "0")
            {
                drugBillID = this.itemManager.GetNewDrugBillNO(approveDept.ID);
                if (drugBillID == null)
                {
                    if (trans == null)      //�����ɱ������ڲ�����
                    {
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                        feeIntegrateManager.Rollback();
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    this.Err = "��ȡ��ҩ����ˮ�ŷ�������" + itemManager.Err;
                    return -1;
                }
                if (drugMessage != null)
                {
                    //�ڰ�ҩ֪ͨ�б����ҩ����,���Է��ظ�������
                    drugMessage.DrugBillClass.Memo = drugBillID;
                }
            }
            //ȡϵͳʱ��
            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            #endregion

            //��ҩʱ�շ���Ŀ
            ArrayList alFee = new ArrayList();
            //�洢������Ϣ
            System.Collections.Hashtable hsPatient = new Hashtable();
            //���ΰ�ҩҩƷ��Ϣ
            System.Collections.Hashtable hsDrugMinFee = new Hashtable();
            //סԺ��ҩ�Ƿ����׼ Ĭ�����׼ ԭ���Ʋ�������  501001
            Pharmacy.IsNeedApprove = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.InDrug_Need_Approve, true, true);

            //סԺ��ҩ�����Ż����޸ĳ�����Ϊ�˲�Ӱ��סԺ��ҩ֮��ĳ������жϡ� by Sunjh 2010-8-30 {32F6FA1C-0B8E-4b9c-83B6-F9626397AC7C}
            #region סԺ��ҩ�����Ż�

            //System.Collections.Hashtable hsDrugStorage = new Hashtable();
            //ArrayList alDrugStorage = new ArrayList();
            //int iCount = 0;
            //foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp in alApplyOut)
            //{
            //    if (hsDrugStorage.ContainsKey(applyOutTemp.StockDept.ID + applyOutTemp.Item.ID))
            //    {
            //        FS.FrameWork.Models.NeuObject objTemp = alDrugStorage[Convert.ToInt32(hsDrugStorage[applyOutTemp.StockDept.ID + applyOutTemp.Item.ID])] as FS.FrameWork.Models.NeuObject;
            //        objTemp.User01 = Convert.ToString(Convert.ToDecimal(objTemp.User01) + applyOutTemp.Operation.ApplyQty * applyOutTemp.Days);
            //    }
            //    else
            //    {
            //        FS.FrameWork.Models.NeuObject objTemp = new NeuObject();
            //        objTemp.ID = applyOutTemp.Item.ID;
            //        objTemp.Name = applyOutTemp.Item.Name;                    
            //        objTemp.Memo = applyOutTemp.StockDept.ID;
            //        objTemp.User01 = Convert.ToString(applyOutTemp.Operation.ApplyQty * applyOutTemp.Days);
            //        alDrugStorage.Add(objTemp);
            //        hsDrugStorage.Add(applyOutTemp.StockDept.ID + applyOutTemp.Item.ID, iCount);
            //        iCount++;
            //    }
            //}

            //FS.FrameWork.Management.ControlParam ctrlManager = new FS.FrameWork.Management.ControlParam();
            //string negativeStore = ctrlManager.QueryControlerInfo("S00024", false);
            //bool isMinusStore = FS.FrameWork.Function.NConvert.ToBoolean(negativeStore);

            //for (int i = 0; i < alDrugStorage.Count; i++)
            //{
            //    decimal storageNum = 0;
            //    decimal totalNum = 0;
            //    FS.FrameWork.Models.NeuObject objTemp = alDrugStorage[i] as FS.FrameWork.Models.NeuObject;
            //    if (this.GetStorageNum(objTemp.Memo, objTemp.ID, out storageNum) == -1)
            //    {
            //        return -1;
            //    }
            //    //�жϿ���Ƿ��㣬�˿�����û�п����߲���
            //    if ((isMinusStore == false) && (storageNum < Convert.ToDecimal(objTemp.User01)) && (Convert.ToDecimal(objTemp.User01) > 0))
            //    {
            //        this.Err = objTemp.Name + "�Ŀ���������㡣�벹����";
            //        this.ErrCode = "2";
            //        return -1;
            //    }
            //}

            #endregion

            ArrayList alSendApplyOut = new ArrayList();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyOut)
            {
                #region ʵ���ֶθ�ֵ
                applyOut.DrugNO = drugBillID;
                applyOut.Operation.ApproveQty = applyOut.Operation.ApplyQty;
                if (approveDept != null && approveDept.ID != "")
                {
                    applyOut.Operation.ApproveOper.Dept = approveDept;
                }
                else
                {
                    applyOut.Operation.ApproveOper.Dept.ID = ((FS.HISFC.Models.Base.Employee)itemManager.Operator).Dept.ID;
                }

                applyOut.Operation.ExamOper.OperTime = sysTime;
                applyOut.Operation.ExamOper.ID = ((FS.HISFC.Models.Base.Employee)itemManager.Operator).ID;
                applyOut.Operation.ExamOper.Dept = applyOut.Operation.ApproveOper.Dept;

                //��ȡ���ҿ����Ϣ ��û�λ��               
                //FS.HISFC.Models.Pharmacy.Storage storage;
                //storage = itemManager.GetStockInfoByDrugCode(applyOut.Operation.ApproveOper.Dept.ID, applyOut.Item.ID);
                //if (storage == null)
                //{
                //    if (trans == null)      //�����ɱ������ڲ�����
                //    {
                //        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                //        feeIntegrateManager.Rollback();
                //        //FS.FrameWork.Management.PublicTrans.RollBack();
                //    }
                //    this.Err = "��ȡ�����Ϣ����" + this.itemManager.Err;
                //    return 0;
                //}
                //applyOut.PlaceNO = storage.PlaceNO;

                //סԺ��ҩ�����Ż� by Sunjh 2010-8-30 {32F6FA1C-0B8E-4b9c-83B6-F9626397AC7C}
                applyOut.PlaceNO = this.itemManager.GetPlaceNoOptimize(applyOut.Operation.ApproveOper.Dept.ID, applyOut.Item.ID);

                #endregion

                #region �����Ƿ���Ҫ��׼ ����������Ϣ״̬��ֵ

                if (Pharmacy.IsNeedApprove)
                {
                    applyOut.State = "1";
                }
                else
                {
                    //��ʾ��׼���� 
                    applyOut.State = "2";
                    applyOut.Operation.ApproveOper.OperTime = sysTime;
                    applyOut.Operation.ApproveOper.ID = ((FS.HISFC.Models.Base.Employee)itemManager.Operator).ID;

                }
                #endregion

                decimal retailPrice = applyOut.Item.PriceCollection.RetailPrice;//סԺ��ҩ���ۼ�ȡҩƷ������Ϣ���ۼۣ�������Ϊ������ۼ�{E8B1C57D-CB92-4F8F-A7E6-81049D7655BC}
                #region ���⴦��
                applyOut.DrugNO = drugBillID;
                applyOut.PrivType = "Z1";
                if (arkDept != null && arkDept.ID != "")
                {
                    parm = itemManager.ArkOutput(applyOut, arkDept);
                    if (parm == -1)
                    {
                        if (trans == null)      //�����ɱ������ڲ�����
                        {
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                            feeIntegrateManager.Rollback();
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                        }

                        if (this.ErrCode == "2")
                            this.Err = this.itemManager.Err;
                        else
                            this.Err = "ҩƷ����ʧ��:" + this.itemManager.Err;

                        return -1;
                    }
                }
                else
                {
                    parm = itemManager.Output(applyOut);
                    if (parm == -1)
                    {
                        if (trans == null)      //�����ɱ������ڲ�����
                        {
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                            feeIntegrateManager.Rollback();
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                        }

                        if (this.ErrCode == "2")
                            this.Err = this.itemManager.Err;
                        else
                            this.Err = "ҩƷ����ʧ��:" + this.itemManager.Err;

                        return -1;
                    }
                }
                #endregion
                applyOut.Item.PriceCollection.RetailPrice = retailPrice;//סԺ��ҩ���ۼ�ȡҩƷ������Ϣ���ۼۣ�������Ϊ������ۼ�{E8B1C57D-CB92-4F8F-A7E6-81049D7655BC}
                #region �Ƿ���Ҫ���мƷѴ��� ����Ҫ�շ� �����շѺ��� ���·��õ���ҩ���

                if (!applyOut.IsCharge)
                {
                    #region ������Ϣ��ֵ����
                    FS.HISFC.Models.RADT.PatientInfo patient = null;
                    if (hsPatient.ContainsKey(applyOut.PatientNO))
                    {
                        patient = hsPatient[applyOut.PatientNO] as FS.HISFC.Models.RADT.PatientInfo;
                    }
                    else
                    {
                        patient = radtIntegrate.QueryPatientInfoByInpatientNO(applyOut.PatientNO);
                        hsPatient.Add(applyOut.PatientNO, patient);
                    }
                    //{389D4EDA-B312-492a-8EDA-B9D0F9A30041} �жϻ����Ƿ���Ժ
                    if (patient.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString()
                    && patient.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.B.ToString()
                        )
                    {
                        if (trans == null)      //�����ɱ������ڲ�����
                        {
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                            feeIntegrateManager.Rollback();
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                        }
                        this.Err = patient.Name + " ���߷���Ժ״̬�����ܽ��з�ҩ�շѲ���";
                        return -1;
                    }

                    #endregion

                    #region ҩƷ��Ϣ��ֵ����
                    if (hsDrugMinFee.ContainsKey(applyOut.Item.ID))
                    {
                        applyOut.Item.MinFee = hsDrugMinFee[applyOut.Item.ID] as FS.FrameWork.Models.NeuObject;
                    }
                    else
                    {
                        FS.HISFC.Models.Pharmacy.Item item = itemManager.GetItem(applyOut.Item.ID);
                        applyOut.Item.MinFee = item.MinFee;
                        hsDrugMinFee.Add(applyOut.Item.ID, item.MinFee);
                    }
                    #endregion

                    #region ����ҩƷ�����շ���Ϣ {92A0CF31-27BC-472e-9C15-1ED2C8A81F36} by zhang.xs 2010.10.19

                    ArrayList alChargeOrders = new ArrayList();
                    FS.HISFC.BizLogic.Fee.UndrugPackAge managerPack = new FS.HISFC.BizLogic.Fee.UndrugPackAge();
                    ArrayList alSubtbl = orderManager.QueryExecOrderSubtblByMainOrder(applyOut.ExecNO, applyOut.CombNO);

                    decimal rate = 1;
                    foreach (FS.HISFC.Models.Order.ExecOrder order in alSubtbl)
                    {
                        string err = string.Empty;
                        FS.HISFC.Models.Order.Inpatient.Order o = order.Order;

                        if (FillFeeItem(ref o, out err, rate) == -1)
                        {
                            feeIntegrateManager.Rollback();
                            this.Err = err;
                            return -1;
                        }
                        #region ����Ǹ�����Ŀ�����ϸ��

                        if (((FS.HISFC.Models.Fee.Item.Undrug)order.Order.Item).UnitFlag == "1")
                        {
                            ArrayList al = managerPack.QueryUndrugPackagesBypackageCode(order.Order.Item.ID);
                            if (al == null)
                            {
                                feeIntegrateManager.Rollback();
                                this.Err = "���ϸ�����" + managerPack.Err;
                                return -1;
                            }
                            
                            foreach (FS.HISFC.Models.Fee.Item.Undrug undrug in al)
                            {
                                FS.HISFC.Models.Order.ExecOrder myorder = null;
                                decimal qty = order.Order.Qty;
                                myorder = order.Clone();
                                myorder.Name = undrug.Name;
                                myorder.Order.Name = undrug.Name;
                                myorder.Order.User03 = order.ID;
                                /*�շ�*/
                                myorder.IsCharge = true;
                                myorder.IsConfirm = true;
                                myorder.IsExec = true;
                                myorder.Order.ExecOper = applyOut.Operation.ExamOper.Clone();
                                myorder.Order.ExecOper.Dept = order.Order.ExeDept;
                                myorder.ChargeOper = applyOut.Operation.ExamOper.Clone();
                                myorder.Order.Oper = applyOut.Operation.ExamOper.Clone();//�շѿ��Ұ��տ���������
                                myorder.Order.Oper.Dept = myorder.Order.ReciptDept; //�շѿ��Ұ��տ���������
                                myorder.Order.Item = undrug.Clone();
                                myorder.Order.Qty = qty * undrug.Qty;//����==������Ŀ����*С��Ŀ����
                                myorder.Order.Item.Qty = qty * undrug.Qty;//����==������Ŀ����*С��Ŀ����

                                #region {10C9E65E-7122-4a89-A0BE-0DF62B65C647} д�븴����Ŀ���롢����
                                myorder.Order.Package.ID = order.Order.Item.ID;
                                myorder.Order.Package.Name = order.Order.Item.Name;
                                #endregion

                                o = myorder.Order;
                                rate = feeIntegrateManager.GetItemRateForZT(order.Order.Item.ID, undrug.ID);
                                if (FillFeeItem(ref o, out err, rate) == -1)
                                {
                                    feeIntegrateManager.Rollback();
                                    this.Err = err;
                                    return -1;
                                }
                                if (myorder.Order.Item.Price > 0)
                                    alChargeOrders.Add(myorder.Order);
                            }
                        }
                        else //��ͨ��Ŀ�շ�
                        {

                            order.Order.User03 = order.ID;

                            /*�շ�*/
                            order.IsCharge = true;
                            order.IsConfirm = true;
                            order.IsExec = true;
                            order.Order.ExecOper = applyOut.Operation.ExamOper.Clone();
                            order.Order.ExecOper.Dept = order.Order.ExeDept;
                            order.Order.Oper = applyOut.Operation.ExamOper.Clone();
                            order.Order.Oper.Dept = order.Order.ReciptDept; //�շѿ��Ұ���������
                            order.ChargeOper = applyOut.Operation.ExamOper.Clone();
                            alChargeOrders.Add(order.Order);
                        }
                        #endregion

                        if (orderManager.UpdateForConfirmExecUnDrug(order, true) == -1)
                        {
                            feeIntegrateManager.Rollback();
                            this.Err = orderManager.Err;
                            return -1;
                        }
                    }
                    if (alChargeOrders.Count > 0)
                    {
                        if (feeIntegrateManager.FeeItem(patient, ref alChargeOrders) == -1)
                        {
                            if (trans == null)      //�����ɱ������ڲ�����
                            {
                                //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                                feeIntegrateManager.Rollback();
                                //FS.FrameWork.Management.PublicTrans.RollBack();
                            }
                            this.Err = feeIntegrateManager.Err;
                            return -1;
                        }
                    }
                    #endregion

                    FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem = this.ConvertApplyOutToFeeItem(applyOut);

                    if (feeIntegrateManager.FeeItem(patient, feeItem) == -1)
                    {
                        if (trans == null)      //�����ɱ������ڲ�����
                        {
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                            feeIntegrateManager.Rollback();
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                        }
                        this.Err = feeIntegrateManager.Err;
                        return -1;
                    }

                    //��ҩƷ��д�շѱ�� ������ ��ˮ��
                    applyOut.IsCharge = true;
                    applyOut.RecipeNO = feeItem.RecipeNO;
                    applyOut.SequenceNO = feeItem.SequenceNO;
                }

                #region ���·��÷�ҩ���
                try
                {
                    parm = feeManager.UpdateMedItemExecInfo(
                        applyOut.RecipeNO,							//������
                        applyOut.SequenceNO,						//��������ˮ��
                        Convert.ToInt32(applyOut.OutBillNO),      //���¿����ˮ��
                        Convert.ToInt32(applyOut.OutBillNO),      //���ⵥ���к�
                        applyOut.StockDept.ID,						//��ҩ����
                        applyOut.Operation.ExamOper.ID,					//��ҩ��
                        sysTime);							//��ҩʱ��
                    if (parm == -1)
                    {
                        if (trans == null)      //�����ɱ������ڲ�����
                        {
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                            feeIntegrateManager.Rollback();
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                        }
                        this.Err = "���·�����ϸ��Ϣ����!" + itemManager.Err + " ������" + applyOut.RecipeNO;
                        return -1;
                    }
                    if (parm == 0)
                    {
                        if (trans == null)      //�����ɱ������ڲ�����
                        {
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                            feeIntegrateManager.Rollback();
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                        }
                        this.Err = "���·�����ϸ��Ϣʧ��! δ�ҵ���Ӧ�ķ�����ϸ��Ϣ\n" + "������" + applyOut.RecipeNO;
                        return -1;
                    }
                }
                catch (Exception ex)
                {
                    if (trans == null)      //�����ɱ������ڲ�����
                    {
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                        feeIntegrateManager.Rollback();
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    this.Err = "���·�����ϸ��Ϣ����" + ex.Message;
                    return -1;
                }
                #endregion

                #endregion

                #region ҽ��ִ�е���ҩ����  Ŀǰ�������ζ�ҽ��ִ�е���״̬����

                if (!string.IsNullOrEmpty(applyOut.ExecNO)&&!string.IsNullOrEmpty(applyOut.OrderType.ID))
                {
                    parm = orderManager.UpdateOrderDruged(applyOut.ExecNO, applyOut.OrderNO, orderManager.Operator.ID, applyOut.Operation.ApproveOper.Dept.ID);
                    if (parm == -1)
                    {
                        if (trans == null)      //�����ɱ������ڲ�����
                        {
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                            feeIntegrateManager.Rollback();
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                        }
                        this.Err = string.Format("����ҩƷҽ��ִ�е�����������Ϣ:{0} \nҩƷ����:{1} ִ�е���ˮ��:{2} ҽ����ˮ��:{3}", orderManager.Err, applyOut.Item.Name, applyOut.ExecNO, applyOut.OrderNO);
                        return -1;
                    }
                }

                #endregion

                #region ���³���������еİ�ҩ��Ϣ

                applyOut.DrugNO = drugBillID;
                parm = this.itemManager.ExamApplyOut(applyOut);
                if (parm != 1)
                {
                    if (trans == null)      //�����ɱ������ڲ�����
                    {
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                        feeIntegrateManager.Rollback();
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    if (parm == 0)
                    {
                        this.Err = "��ǰ���������ϻ����ѱ���������ȷ�ϣ�����ˢ�µ�ǰ����";
                        return 0;
                    }
                    else
                    {
                        this.Err = "��˰�ҩ������Ϣ��������" + itemManager.Err;
                    }
                    return -1;
                }
                #endregion

                FS.HISFC.Models.Pharmacy.ApplyOut sendApplyOut = applyOut.Clone();
                sendApplyOut.DrugNO = drugBillID;
                alSendApplyOut.Add(sendApplyOut);
            }


            if (drugMessage != null)
            {
                #region ��ҩ֪ͨ����
                List<FS.FrameWork.Models.NeuObject> al = itemManager.QueryApplyOutPatientList(drugMessage);
                if (al == null)
                {
                    if (trans == null)      //�����ɱ������ڲ�����
                    {
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                        feeIntegrateManager.Rollback();
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    this.Err = "��ѯ��ҩ����GetApplyOutPatientListʱ����" + itemManager.Err;
                    return -1;
                }

                //���ȫ����׼(û�д���ҩ����)������°�ҩ֪ͨ��Ϣ�����򲻸��°�ҩ֪ͨ��Ϣ
                if (al.Count == 0)
                {
                    //��ҩ�����Ϊ�Ѱ�ҩ����ҩ���0-֪ͨ1-�Ѱ�
                    drugMessage.SendFlag = 1;
                    if (drugStoreManager.SetDrugMessage(drugMessage) == -1)
                    {
                        if (trans == null)      //�����ɱ������ڲ�����
                        {
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                            feeIntegrateManager.Rollback();
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                        }
                        this.Err = "���°�ҩ֪ͨʱ����" + drugStoreManager.Err;
                        return -1;
                    }
                }
                #endregion
            }

            #region HL7��Ϣ����
            object curInterfaceImplement = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Pharmacy), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder));
            if (curInterfaceImplement is FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder)
            {
                FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder curIOrderControl = curInterfaceImplement as FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder;

                int param = curIOrderControl.SendDrugInfo(new FS.HISFC.Models.RADT.PatientInfo(), alSendApplyOut, true);
                if (param == -1)
                {
                    if (trans == null)
                    {
                        feeIntegrateManager.MedcareInterfaceRollback();
                    }
                    this.Err = curIOrderControl.Err;
                    return -1;
                }
            }

            #endregion

            if (trans == null)      //�����ɱ������ڲ�����
            {
                //{3EE6172A-301B-4d16-91C7-E5D8AC94D942} �˴������FeeIntegrate��Commit
                //FS.FrameWork.Management.PublicTrans.Commit();
                feeIntegrateManager.Commit();
            }

            return 1;
        }

        /// <summary>
        /// ��÷�ҩƷ��Ϣ {92A0CF31-27BC-472e-9C15-1ED2C8A81F36} by zhang.xs 2010.10.19
        /// </summary>
        /// <param name="order"></param>
        /// <param name="err"></param>
        /// <returns></returns>
        protected int FillFeeItem(ref FS.HISFC.Models.Order.Inpatient.Order order, out string err, decimal rate)
        {
            err = "";

            FS.HISFC.Models.Fee.Item.Undrug item;
            FS.HISFC.BizProcess.Integrate.Fee tempManagerFee = new FS.HISFC.BizProcess.Integrate.Fee();
            item = tempManagerFee.GetItem(order.Item.ID);

            if (item == null)
            {
                err = "��÷�ҩƷ��Ϣ����" + string.Format("����ԭ�򣬣�{0}�ݷ�ҩƷ�����Ѿ�ͣ�ã�", order.Item.Name);
                return -1;
            }

            ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).IsNeedConfirm = item.IsNeedConfirm;
            order.Item.Price = item.Price * rate;
            order.Item.MinFee = item.MinFee;
            order.Item.SysClass = item.SysClass.Clone();

            ((FS.HISFC.Models.Fee.Item.Undrug)order.Item).UnitFlag = ((FS.HISFC.Models.Fee.Item.Undrug)item).UnitFlag;
            return 0;
        }

         /// <summary>
        /// ���Ѵ�ӡ�İ�ҩ�����к�׼������ҩ��׼��
        /// </summary>
        /// <param name="alApplyOut">����������Ϣ</param>
        /// <param name="approveOperCode">��׼�ˣ���ҩ�ˣ�</param>
        /// <param name="deptCode">��׼����</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        public int InpatientDrugApprove(ArrayList alApplyOut, string approveOperCode, string deptCode)
        {
            return InpatientDrugApprove(alApplyOut, approveOperCode, deptCode, null);
        }
        /// <summary>
        /// ���Ѵ�ӡ�İ�ҩ�����к�׼������ҩ��׼��
        /// </summary>
        /// <param name="alApplyOut">����������Ϣ</param>
        /// <param name="approveOperCode">��׼�ˣ���ҩ�ˣ�</param>
        /// <param name="deptCode">��׼����</param>
        /// <param name="trans">�ⲿ�������񣬴����ֵʱ ���Զ���������</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        public int InpatientDrugApprove(ArrayList alApplyOut, string approveOperCode, string deptCode,System.Data.IDbTransaction trans)
        {
            if (trans == null)      //�ⲿδ��������
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
            }

            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
            #region �����༰��������

            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            ////�������ݿ⴦������
            //FS.FrameWork.Management.Transaction t = null;
            //if (trans == null)
            //{                
            //    t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //    t.BeginTransaction();
            //    this.SetTrans(t.Trans);
            //    ctrlParamIntegrate.SetTrans(t.Trans);
            //}
            //else
            //{
            //    this.SetTrans(trans);
            //    ctrlParamIntegrate.SetTrans(trans);
            //}
            #endregion

            //סԺ��ҩ�Ƿ����׼ Ĭ�����׼ ԭ���Ʋ�������  501001
            Pharmacy.IsNeedApprove = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.InDrug_Need_Approve, true, true);
            if (!Pharmacy.IsNeedApprove)
            {
                return 1;
            }

            DateTime sysDate = this.itemManager.GetDateTimeFromSysDateTime();
            //�԰�ҩ�����к�׼����
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyOut)
            {
                #region ��׼���ݸ�ֵ
                //���������ϵ�����
                if (applyOut.ValidState == FS.HISFC.Models.Base.EnumValidState.Valid)
                {
                    applyOut.State = "2";                  //��ʾ�Ѻ�׼
                    applyOut.Operation.ApproveOper.ID = approveOperCode; //��׼��
                    applyOut.Operation.ApproveOper.Dept.ID = deptCode;        //��׼����
                    applyOut.Operation.ApproveOper.OperTime = sysDate;         //��׼ʱ��
                }
                #endregion

                #region ��׼��ҩ��
                int parm = 0;
                parm = this.itemManager.ApproveApplyOut(applyOut);
                if (parm != 1)
                {
                    if (trans == null)      //�����ɱ������ڲ�����
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    if (parm == 0)
                    {
                        this.Err = "�����ظ���׼��ҩ��������ˢ�µ�ǰ����";
                        return 0;
                    }
                    else
                    {
                        this.Err = "����������Ϣ��׼����!";
                    }
                    return -1;
                }
                #endregion
            }

            if (trans == null)      //�����ɱ������ڲ�����
            {
                FS.FrameWork.Management.PublicTrans.Commit();
            }

            return 1;
        }

        /// <summary>
        /// ����ҩ������к�׼������ҩ��׼��
        /// </summary>
        /// <param name="alApplyOut">����������Ϣ</param>
        /// <param name="drugMessage">��ҩ֪ͨ���������°�ҩ֪ͨ(��ҩ������İ�ҩ��������drugMessage.DrugBillClass.Memo��)</param>
        /// <param name="arkDept">ҩ�����</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        public int InpatientDrugReturnConfirm(ArrayList alApplyOut, FS.HISFC.Models.Pharmacy.DrugMessage drugMessage,FS.FrameWork.Models.NeuObject arkDept,FS.FrameWork.Models.NeuObject approveDept)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region ����������

            //�˷����������
            FS.HISFC.BizLogic.Fee.ReturnApply applyReturn = new FS.HISFC.BizLogic.Fee.ReturnApply();

            //������Ϲ�����
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Fee();

            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴�Integrate��SetTrans
            feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            //this.SetTrans(t.Trans);
            //applyReturn.SetTrans(t.Trans); 
            //feeIntegrate.SetTrans(t.Trans);

            #endregion

            //����ʵ��
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
            //����ʵ����
            FS.HISFC.Models.RADT.PatientInfo patientInfo = new FS.HISFC.Models.RADT.PatientInfo();

            #region ȡ��ҩ����ˮ�ţ�����������еİ�ҩ���ţ�

            //string drugBillID = this.itemManager.GetNewDrugBillNO(approveDept.ID);
            //if (drugBillID == null)
            //{
            //    FS.FrameWork.Management.PublicTrans.RollBack();
            //    feeIntegrate.MedcareInterfaceRollback();
            //    this.Err = this.itemManager.Err;
            //    return -1;
            //}
            ////�ڰ�ҩ֪ͨ�б����ҩ����,���Է��ظ�������
            //drugMessage.DrugBillClass.Memo = drugBillID;

            string drugBillID = "";
            if (drugMessage != null)
            {
                drugBillID = drugMessage.DrugBillClass.Memo;
            }
            if (string.IsNullOrEmpty(drugBillID) || drugBillID == "0")
            {
                drugBillID = this.itemManager.GetNewDrugBillNO(approveDept.ID);
                if (drugBillID == null)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    feeIntegrate.MedcareInterfaceRollback();
                    this.Err = this.itemManager.Err;
                    return -1;
                }
                if (drugMessage != null)
                {
                    //�ڰ�ҩ֪ͨ�б����ҩ����,���Է��ظ�������
                    drugMessage.DrugBillClass.Memo = drugBillID;
                }
            }

            //ȡϵͳʱ��
            DateTime sysTime = this.itemManager.GetDateTimeFromSysDateTime();

            #endregion

            ArrayList alSendApplyOut=new ArrayList();

            //���û�check�����ݽ��з�ҩ����
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in alApplyOut)
            {
                //����ԭOUT_BILL_CODE{B0536663-E701-474e-BCE2-BE13D7257EF2}
                this.originalOutBillCode = applyOut.OutBillNO;
                applyOut.DrugNO = drugBillID;
                patientInfo = this.radtIntegrate.QueryPatientInfoByInpatientNO(applyOut.PatientNO);

                //{389D4EDA-B312-492a-8EDA-B9D0F9A30041} �жϻ����Ƿ���Ժ
                if (patientInfo.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString()
                    && patientInfo.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.B.ToString()
                    )
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    if (trans == null)      //�����ɱ������ڲ�����
                    {
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                        feeIntegrate.Rollback();

                        
                    }
                    applyOut.OutBillNO = this.originalOutBillCode;
                    this.Err = patientInfo.Name + " ���߷���Ժ״̬�����ܽ�����ҩ�˷Ѳ���";
                    return -1;
                }

                #region ʵ���ֶθ�ֵ
                applyOut.Operation.ApproveQty = applyOut.Operation.ApplyQty;
                applyOut.Operation.ExamOper.OperTime = sysTime;
                applyOut.Operation.ExamOper.ID = itemManager.Operator.ID;		//��׼��
                Pharmacy.IsReturnNeedApprove = ctrlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.InDrug_Need_Approve, true, true);
                if (Pharmacy.IsReturnNeedApprove)
                {
                    applyOut.State = "1";		//���׼
                }
                else								//�����׼
                {
                    applyOut.Operation.ApproveOper.ID = itemManager.Operator.ID;	//��׼��
                    applyOut.Operation.ApproveOper.OperTime = sysTime;
                    applyOut.State = "2";	//��ʾ��׼���� 	
                }
                //���³���������еİ�ҩ��Ϣ��
                applyOut.DrugNO = drugBillID;
                if (approveDept != null && approveDept.ID != "")
                {
                    applyOut.Operation.ApproveOper.Dept = approveDept;
                }
                else
                {
                    applyOut.Operation.ApproveOper.Dept.ID = ((FS.HISFC.Models.Base.Employee)itemManager.Operator).Dept.ID;
                }
                //�˿�ʱ,����Ԥ�۵Ŀ��
                applyOut.IsPreOut = true;
                #endregion

                #region �˿⴦��
                applyOut.PrivType = "Z2";
                if (arkDept != null && arkDept.ID != "")
                {
                    if (itemManager.ArkOutput(applyOut,arkDept) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        feeIntegrate.MedcareInterfaceRollback();
                        applyOut.OutBillNO = this.originalOutBillCode;
                        this.Err = itemManager.Err;
                        return -1;
                    }
                }
                else
                {
                    if (itemManager.Output(applyOut) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        feeIntegrate.MedcareInterfaceRollback();
                        applyOut.OutBillNO = this.originalOutBillCode;
                        this.Err = itemManager.Err;
                        return -1;
                    }
                }
                #endregion

                #region �˷Ѻ��������Ϣ �粻�账����� ������˷�������

                int parm = 0;
                //�����ҩ��ͬʱ�˷�,���������Ϣ
                //Pharmacy.IsReturnCharge = Pharmacy.QueryControlForBool("501003", false);
                Pharmacy.IsReturnCharge = this.ctrlIntegrate.GetControlParam<bool>(SysConst.Use_Drug_BackFee, false, false);
            
                if (Pharmacy.IsReturnCharge)
                {
                    #region �˷Ѵ���   ȡ������Ϣ

                    //feeItemList = feeInpatientManager.GetItemListByRecipeNO(applyOut.RecipeNO, applyOut.SequenceNO, true);
                    feeItemList = feeInpatientManager.GetItemListByRecipeNO(applyOut.RecipeNO, applyOut.SequenceNO, FS.HISFC.Models.Base.EnumItemType.Drug);
                    if (feeItemList == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        feeIntegrate.MedcareInterfaceRollback();
                        applyOut.OutBillNO = this.originalOutBillCode;
                        System.Windows.Forms.MessageBox.Show(feeInpatientManager.Err);
                        return -1;
                    }

                    feeItemList.Item.Qty = applyOut.Operation.ApplyQty * applyOut.Days;
                    feeItemList.NoBackQty = 0;
                    feeItemList.FT.TotCost = feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty;
                    feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
                    feeItemList.CancelRecipeNO = applyOut.RecipeNO;
                    feeItemList.CancelSequenceNO = applyOut.SequenceNO;

                    feeItemList.IsNeedUpdateNoBackQty = false;
                    feeItemList.PayType = FS.HISFC.Models.Base.PayTypes.SendDruged;

                    if (feeIntegrate.QuitItem(patientInfo, feeItemList) == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        feeIntegrate.MedcareInterfaceRollback();
                        applyOut.OutBillNO = this.originalOutBillCode;
                        this.Err = FS.FrameWork.Management.Language.Msg( "�˷�ʧ��!" ) + feeIntegrate.Err;
                        return -1;
                    }

                    applyOut.RecipeNO = feeItemList.RecipeNO;
                    applyOut.SequenceNO = feeItemList.SequenceNO;

                    #endregion
                }
                else
                {
                    #region �����˷�����

                    patientInfo = this.radtIntegrate.QueryPatientInfoByInpatientNO(applyOut.PatientNO);

                    //ȡ������Ϣ
                    //feeItemList = feeInpatientManager.GetItemListByRecipeNO(applyOut.RecipeNO, applyOut.SequenceNO, true);
                    feeItemList = feeInpatientManager.GetItemListByRecipeNO(applyOut.RecipeNO, applyOut.SequenceNO, EnumItemType.Drug);
                    if (feeItemList == null)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        feeIntegrate.MedcareInterfaceRollback();
                        applyOut.OutBillNO = this.originalOutBillCode;
                        this.Err = feeInpatientManager.Err;
                        return -1;
                    }

                    //�����˷�����
                    feeItemList.Item.Qty = applyOut.Operation.ApplyQty * applyOut.Days; //�˷�����Ϊ��ҩ������
                    feeItemList.User02 = applyOut.BillNO;						//�˷����뵥�ݺ�
                    feeItemList.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    feeItemList.IsConfirmed = true;

                    parm = applyReturn.Apply(patientInfo, feeItemList, applyOut.Operation.ExamOper.OperTime);
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        feeIntegrate.MedcareInterfaceRollback();
                        applyOut.OutBillNO = this.originalOutBillCode;
                        this.Err = applyReturn.Err;
                        return -1;
                    }

                    #endregion
                }

                #endregion


                #region �˿������׼

                parm = this.itemManager.ExamApplyOut(applyOut);
                if (parm != 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    feeIntegrate.MedcareInterfaceRollback();
                    applyOut.OutBillNO = this.originalOutBillCode;
                    if (parm == 0)
                    {
                        this.Err = "��ǰ���������ϻ����ѱ���������ȷ�ϣ�����ˢ�µ�ǰ����";
                        return 0;
                    }
                    else
                    {
                        this.Err = this.itemManager.Err;
                    }
                    return -1;
                }

                #endregion

               FS.HISFC.Models.Pharmacy.ApplyOut sendApplyOut = applyOut.Clone();
                sendApplyOut.DrugNO = drugBillID;
                alSendApplyOut.Add(sendApplyOut);
            }

            #region ��ҩ֪ͨ����

            //ȡ����ҩ�����б�,���ȫ����׼(û�д���ҩ����)������°�ҩ֪ͨ��Ϣ
            List<FS.FrameWork.Models.NeuObject> al = itemManager.QueryApplyOutPatientList(drugMessage);
            if (al == null)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                feeIntegrate.MedcareInterfaceRollback();
                this.Err = "��ѯ��ҩ����GetApplyOutPatientListʱ����";
                return -1;
            }

            //���ȫ����׼(û�д���ҩ����)������°�ҩ֪ͨ��Ϣ�����򲻸��°�ҩ֪ͨ��Ϣ
            if (al.Count == 0)
            {
                //��ҩ�����Ϊ�Ѱ�ҩ����ҩ���0-֪ͨ1-�Ѱ�
                drugMessage.SendFlag = 1;
                if (drugStoreManager.SetDrugMessage(drugMessage) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    feeIntegrate.MedcareInterfaceRollback();
                    this.Err = "���°�ҩ֪ͨʱ����";
                    return -1;
                }
            }

            #endregion

            #region HL7��Ϣ����
            object curInterfaceImplement = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Pharmacy), typeof(FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder));
            if(curInterfaceImplement is FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder)
            {
                FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder curIOrderControl = curInterfaceImplement as FS.SOC.HISFC.BizProcess.MessagePatternInterface.IOrder;

                int param = curIOrderControl.SendFeeInfo(new FS.HISFC.Models.RADT.PatientInfo(), alSendApplyOut, false);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    feeIntegrate.MedcareInterfaceRollback();
                    this.Err = curIOrderControl.Err;
                    return -1;
                }
            }
            #endregion

            //����ֵ�� 0 �Ǵ���0����ȷ�ģ�
            //���ѽӿ��ύ��
            if (feeIntegrate.MedcareInterfaceCommit() < 0 ) 
            {
                feeIntegrate.MedcareInterfaceRollback();
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.Err = "���ѽӿ��ύʱ�����ύδ�ɹ���";
                return -1;
            }
            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942} �˴������feeIntegrate��Commit
            //FS.FrameWork.Management.PublicTrans.Commit();
            feeIntegrate.Commit();
            return 1;
        }

        /// <summary>
        /// ��ҩƷ������Ϣת��Ϊ������Ϣʵ��
        /// </summary>
        /// <param name="applyOut">ҩƷ������Ϣ</param>
        /// <returns>�ɹ����ط�����Ϣʵ�� ʧ�ܷ���null</returns>
        internal FS.HISFC.Models.Fee.Inpatient.FeeItemList ConvertApplyOutToFeeItem(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = new FS.HISFC.Models.Fee.Inpatient.FeeItemList();
            //����и�ֵ
            applyOut.Item.Price = applyOut.Item.PriceCollection.RetailPrice;

            feeItemList.Item = applyOut.Item.Clone();
            feeItemList.RecipeNO = applyOut.RecipeNO;
            feeItemList.SequenceNO = applyOut.SequenceNO;
            feeItemList.Item.PriceUnit = applyOut.Item.MinUnit;

            feeItemList.UpdateSequence = (int)FS.FrameWork.Function.NConvert.ToDecimal(applyOut.OutBillNO);
            feeItemList.SendSequence = feeItemList.UpdateSequence;
            
            feeItemList.Item.Qty = applyOut.Operation.ApproveQty * applyOut.Days;
            feeItemList.Days = applyOut.Days;
            feeItemList.StockOper = applyOut.Operation.ExamOper;

            feeItemList.RecipeOper = applyOut.RecipeInfo;
            feeItemList.ExecOper.Dept = applyOut.StockDept;
            feeItemList.ExecOper.ID = applyOut.Operation.Oper.ID;

            feeItemList.FT.TotCost = FS.FrameWork.Public.String.FormatNumber((feeItemList.Item.Price * feeItemList.Item.Qty / feeItemList.Item.PackQty), 2);
            feeItemList.FT.OwnCost = feeItemList.FT.TotCost;
            feeItemList.IsBaby = applyOut.IsBaby;

            feeItemList.Order.ID = applyOut.OrderNO;
            feeItemList.ExecOrder.ID = applyOut.ExecNO;
            feeItemList.NoBackQty = feeItemList.Item.Qty;
            feeItemList.FTRate.OwnRate = 1;
            feeItemList.BalanceState = "0";
            feeItemList.ChargeOper = applyOut.Operation.ExamOper.Clone();
            feeItemList.FeeOper = applyOut.Operation.ExamOper.Clone();
            feeItemList.TransType = FS.HISFC.Models.Base.TransTypes.Positive;

            return feeItemList;
        }
        #endregion

        #region ������/��ҩ

        /// <summary>
        /// ������ҩ����
        /// </summary>
        /// <param name="applyOutCollection">��ҩ������Ϣ</param>
        /// <param name="terminal">��ҩ�ն�</param>
        /// <param name="drugedDept">��ҩ������Ϣ</param>
        /// <param name="drugedOper">��ҩ��Ա��Ϣ</param>
        /// <param name="isUpdateAdjustParam">�Ƿ���´���������Ϣ</param>
        /// <returns>��ҩȷ�ϳɹ�����1 ʧ�ܷ���-1</returns>
        public int OutpatientDrug(List<ApplyOut> applyOutCollection, NeuObject terminal, NeuObject drugedDept, NeuObject drugedOper, bool isUpdateAdjustParam)
        {
            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            #region �˻�����(��ҩʱ���˻�)
            bool isAccountTerminal = ctrlIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Account_Process, true, false);
            if (applyOutCollection.Count == 0) return -1;
            ApplyOut tempApply = applyOutCollection[0];
            //��ѯ���߹Һ���Ϣ
            FS.HISFC.Models.Registration.Register r = registeIntegrate.GetByClinic(tempApply.PatientNO);
            if (r == null)
            {
                this.Err = "���һ��߹Һ���Ϣʧ�ܣ�" + registeIntegrate.Err;
                return -1;
            }
            bool isAccountFee = false;
            decimal recipeCost = 0m;
            string recipeNO = string.Empty;
            /// <summary>
            /// �����ۺ�ҵ���
            /// </summary>
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Fee();
            if (isAccountTerminal)
            {
                //�Ƿ��˻�����
                if (r.IsAccount)
                {
                    if (!feeIntegrate.CheckAccountPassWord(r))
                    {
                        this.Err = "�˻���������ʧ�ܣ�";
                        return -1;
                    }
                    decimal vacancy = 0m;
                    if (feeIntegrate.GetAccountVacancy(r.PID.CardNO, ref vacancy) <= 0)
                    {
                        this.Err = feeIntegrate.Err;
                        return -1;
                    }
                    FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe = drugStoreManager.GetDrugRecipe(tempApply.StockDept.ID, tempApply.RecipeNO);
                    if (drugRecipe == null)
                    {
                        this.Err = "��ѯ����������Ϣʧ�ܣ�" + drugStoreManager.Err;
                        return -1;
                    }
                    recipeCost = drugRecipe.Cost;
                    recipeNO = drugRecipe.RecipeNO;
                    //�ڰ���ʱ�ж�ʹ��
                    int resultValue = feeIntegrate.GetDrugUnFeeCount(recipeNO, tempApply.StockDept.ID);
                    if (resultValue < 0)
                    {
                        this.Err = "��ѯҩƷ������Ϣʧ�ܣ�" + feeIntegrate.Err;
                        return -1;
                    }

                    if (resultValue > 0)
                    {
                        if (vacancy < recipeCost)
                        {
                            this.Err = "�˻����㣬�뽻�ѣ�";
                            return -1;
                        }
                        isAccountFee = true;
                    }
                    else
                    {
                        isAccountFee = false;
                    }

                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
            #region �˻�����
            if (isAccountTerminal && r.IsAccount && isAccountFee)
            {
                string deptCode = (drugStoreManager.Operator as Employee).Dept.ID;
                string operCode = drugStoreManager.Operator.ID;
                //���˻����
                if (feeIntegrate.AccountPay(r, recipeCost, "ҩ����ҩ", deptCode, string.Empty) < 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���˻����ʧ�ܣ�" + feeIntegrate.Err;
                    return -1;
                }

                if (drugStoreManager.UpdateStoRecipeFeeOper(recipeNO, deptCode, operCode) <= 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���´���������Ϣʧ�ܣ�" + drugStoreManager.Err;
                    return -1;
                }

            }
            #endregion

            ApplyOut info = new ApplyOut();
            //�����ն���ҩ���� �������ҩ�������ݲ�����ͳ��
            decimal drugedQty = 0;
            for (int i = 0; i < applyOutCollection.Count; i++)
            {
                info = applyOutCollection[i] as ApplyOut;

                //��ҩȷ�� ���³��������������״̬
                if (itemManager.UpdateApplyOutStateForDruged(info.StockDept.ID, "M1", info.RecipeNO, info.SequenceNO, "1", drugedOper.ID, info.Operation.ApplyQty) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���³����������ݳ���!" + this.itemManager.Err;
                    return -1;
                }
                //�������ҩ������� �Դ��ּ�¼�����и���
                if (info.PrintState != "1" || info.BillClassNO == "")
                    drugedQty++;

                ////{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                #region �˻�����
                if (isAccountTerminal && r.IsAccount && isAccountFee)
                {
                    string errTxt = string.Empty;
                    if (!feeIntegrate.SaveFeeToAccount(r, info.RecipeNO, info.SequenceNO, ref errTxt))
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "���·�����ϸ����!" + this.itemManager.Err;
                        return -1;
                    }
                }
                #endregion
            }

            if (isUpdateAdjustParam)
            {
                //���������ն˴���ҩ��Ϣ ����-1ÿ�μ���1
                if (drugStoreManager.UpdateTerminalAdjustInfo(terminal.ID, 0, -drugedQty, 0) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���������ն�����ҩ��Ϣ����" + this.itemManager.Err;
                    return -1;
                }
            }

            #region �����������������ҩ��Ϣ
            int parm = drugStoreManager.UpdateDrugRecipeDrugedInfo(info.StockDept.ID, info.RecipeNO, "M1", drugedOper.ID, drugedDept.ID, terminal.ID,applyOutCollection.Count);
            if (parm == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.Err = "��������������ݳ���!" + drugStoreManager.Err;
                return -1;
            }
            else if (parm == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.Err = "���ݿ����ѱ���׼! ��ˢ������" + drugStoreManager.Err;
                return -1;
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

        /// <summary>
        /// ���﷢ҩ����
        /// </summary>
        /// <param name="applyOutCollection">��ҩ������Ϣ</param>
        /// <param name="terminal">��ҩ�ն�</param>
        /// <param name="sendDept">��ҩ������Ϣ(�ۿ����)</param>
        /// <param name="sendOper">��ҩ��Ա��Ϣ</param>
        /// <param name="isDirectSave">�Ƿ�ֱ�ӱ��� (����ҩ����)</param>
        /// <param name="isUpdateAdjustParam">�Ƿ���´���������Ϣ</param>
        /// <returns>��ҩȷ�ϳɹ�����1 ʧ�ܷ���-1</returns>
        public int OutpatientSend(List<ApplyOut> applyOutCollection, NeuObject terminal, NeuObject sendDept, NeuObject sendOper, bool isDirectSave, bool isUpdateAdjustParam)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.BizLogic.Fee.Outpatient outPatientFeeManager = new FS.HISFC.BizLogic.Fee.Outpatient();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            //this.SetTrans(t.Trans);
            //outPatientFeeManager.SetTrans(t.Trans);

            DateTime sysTime = itemManager.GetDateTimeFromSysDateTime();

            int parm;
            ApplyOut info = new ApplyOut();
            for(int i = 0;i < applyOutCollection.Count;i++)
            {
                info = applyOutCollection[i] as ApplyOut;

                #region �������Ϣ����
                if (this.itemManager.UpdateApplyOutStateForSend(info, "2", sendOper.ID) < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���³����������ݳ���!" + itemManager.Err;
                    return -1;
                }
                if (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                    continue;
                #endregion

                #region ���⴦��
                if (string.IsNullOrEmpty(info.DrugNO))
                {
                    info.DrugNO = "0";
                }
                //��ҩ��Ϣ ��ҩ���ҡ���ҩ��
                if (info.PrintState == "1" && info.BillClassNO != "")
                {
                    info.Operation.ApproveOper.Dept.ID = info.BillClassNO;
                }
                else
                {
                    info.Operation.ApproveOper.Dept = sendDept;
                }
                info.Operation.ApproveQty = info.Operation.ApplyQty;
                info.PrivType = "M1";

                info.Operation.ExamOper.ID = sendOper.ID;
                info.Operation.ExamOper.OperTime = sysTime;

                if (this.itemManager.Output(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���ɳ����¼ ���¿�����  " + itemManager.Err;
                    return -1;
                }

                #endregion

                #region ���·��ñ���ȷ����Ϣ
                //0δȷ��/1��ȷ�� ���� 1δȷ��/2��ȷ��
                parm = outPatientFeeManager.UpdateConfirmFlag(info.RecipeNO, info.OrderNO, "1", sendOper.ID, sendDept.ID, sysTime, 0, info.Operation.ApplyQty);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���·��ñ�ȷ�ϱ��ʧ��" + outPatientFeeManager.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "δ��ȷ���·���ȷ�ϱ�� ���ݿ����ѱ���׼";
                    return -1;
                }
                #endregion

                #region �Ƿ���´���������Ϣ
                if (isUpdateAdjustParam || isDirectSave)
                {
                    //�������ҩ������� �Դ��ּ�¼�����и���
                    if (info.PrintState != "1" || info.BillClassNO == "")
                    {
                        //���������ն˴���ҩ��Ϣ ����-1ÿ�μ���1
                        FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipeTemp = new FS.HISFC.Models.Pharmacy.DrugRecipe();
                        string recipeState = "1";
                        if (isDirectSave)           //ֱ�ӷ�ҩ ״̬Ϊ "1"
                            recipeState = "1";
                        else                        //��/��ҩ���� ״̬Ϊ"2"
                            recipeState = "2";

                        drugRecipeTemp = drugStoreManager.GetDrugRecipe(info.StockDept.ID, "M1", info.RecipeNO, recipeState);
                        if (drugRecipeTemp != null)
                        {
                            if (drugStoreManager.UpdateTerminalAdjustInfo(drugRecipeTemp.DrugTerminal.ID, 0, -1, 0) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                this.Err = "���������ն�����ҩ��Ϣ����" + drugStoreManager.Err;
                                return -1;
                            }
                        }
                    }
                }
                #endregion

            }

            //��������������ڷ�ҩ��Ϣ		

            #region ���µ������ڷ�ҩ��Ϣ
            ArrayList al = itemManager.QueryApplyOutListForClinic(info.StockDept.ID, "M1", "1", info.RecipeNO);
            if (al != null && al.Count <= 0)
            {
                if (isDirectSave)           //ֱ�ӷ�ҩ  ���ȸ�����ҩ��Ϣ
                {
                    parm = drugStoreManager.UpdateDrugRecipeDrugedInfo(info.StockDept.ID, info.RecipeNO, "M1", sendOper.ID, sendDept.ID,applyOutCollection.Count);
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "����������ҩ���ݳ���!" + drugStoreManager.Err;
                        return -1;
                    }
                    else if (parm == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "���ݿ����ѱ���׼! ��ˢ������" + drugStoreManager.Err;
                        return -1;
                    }

                    parm = drugStoreManager.UpdateDrugRecipeSendInfo(info.StockDept.ID, info.RecipeNO, "M1", "2", sendOper.ID, sendDept.ID, terminal.ID);
                }
                else                       //��/��ҩ���� 
                {
                    parm = drugStoreManager.UpdateDrugRecipeSendInfo(info.StockDept.ID, info.RecipeNO, "M1", "1", sendOper.ID, sendDept.ID, terminal.ID);
                }

                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "�������﷢ҩ���ݳ���!" + drugStoreManager.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "����ͷ����Ϣ�����ѱ���׼ ��ˢ������" + drugStoreManager.Err;
                    return -1;
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

        /// <summary>
        /// ���ﻹҩ���� ������ҩȷ�ϵ����� ����Ϊδ��ӡ״̬
        /// </summary>
        /// <param name="applyOutCollection">��ҩ������Ϣ</param>
        /// <param name="backOper">��ҩ��Ա��Ϣ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int OutpatientBack(List<ApplyOut> applyOutCollection, NeuObject backOper)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            //this.SetTrans(t.Trans);

            int parm;
            ApplyOut info = new ApplyOut();
            for (int i = 0; i < applyOutCollection.Count; i++)
            {
                info = applyOutCollection[i] as ApplyOut;

                if (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                    continue;

                //��ҩȷ�� ���³��������������״̬ Ϊ����
                if (this.itemManager.UpdateApplyOutStateForDruged(info.StockDept.ID, "M1", info.RecipeNO, info.SequenceNO, "0", backOper.ID, info.Operation.ApplyQty) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���³����������ݳ���!" + itemManager.Err;
                    return -1;
                }
            }
            //��������������ڻ�ҩ��Ϣ������״̬  ������ҩȷ�ϵ����ݽ��л�ҩ
            parm = this.drugStoreManager.UpdateDrugRecipeBackInfo(info.StockDept.ID, info.RecipeNO, "M1", backOper.ID, "2");
            if (parm == -1)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.Err = "��������������ݳ���!" + drugStoreManager.Err;
                return -1;
            }
            else if (parm == 0)
            {
                FS.FrameWork.Management.PublicTrans.RollBack();
                this.Err = "���ݿ����ѱ���׼! ��ˢ������";
                return -1;
            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }
        #endregion

        #region סԺ���������շ�/�˷�

        /// <summary>
        ///  ���������շ�
        /// </summary>
        /// <param name="arrayApplyOut">סԺ��������</param>
        /// <param name="execDept">ִ�п���</param>
        /// <param name="trans">����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int CompoundFee(ArrayList arrayApplyOut, FS.FrameWork.Models.NeuObject execDept, System.Data.IDbTransaction trans)
        {
            if (trans == null)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
            }
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region �����¼

            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new RADT();
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Fee();

            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴����Integrate����SetTrans
            feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            ////�������ݿ⴦������
            //FS.FrameWork.Management.Transaction t = null;
            //if (trans == null)
            //{
            //    t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //    t.BeginTransaction();
            //    this.SetTrans(t.Trans);
            //    radtIntegrate.SetTrans(t.Trans);
            //    consManager.SetTrans(t.Trans);
            //    feeIntegrate.SetTrans(t.Trans);
            //}
            //else
            //{
            //    this.SetTrans(trans);
            //    radtIntegrate.SetTrans(trans);
            //    consManager.SetTrans(trans);
            //    feeIntegrate.SetTrans(trans);
            //}

            #endregion

            #region �γɴ��շ�����

            string privCombo = "-1";
            ArrayList alGroupApplyOut = new ArrayList();
            ArrayList alCombo = new ArrayList();

            #region �������γ�����

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in arrayApplyOut)
            {
                if ((privCombo == info.CompoundGroup && info.CompoundGroup != ""))        //����һ����ͬһ������ˮ
                {
                    continue;
                }
                else			//��ͬ������ˮ��
                {
                    alGroupApplyOut.Add(info);

                    privCombo = info.CompoundGroup;
                }
            }

            #endregion

            #endregion

            System.Collections.Hashtable hsPatientInfo = new Hashtable();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alGroupApplyOut)
            {
                #region ���û�����Ϣ

                if (hsPatientInfo.Contains(info.PatientNO))
                {
                    FS.HISFC.Models.RADT.PatientInfo patent = hsPatientInfo[info.PatientNO] as FS.HISFC.Models.RADT.PatientInfo;
                    patent.User01 = (FS.FrameWork.Function.NConvert.ToInt32(patent.User01) + 1).ToString();
                }
                else
                {
                    //��ȡ�»�����Ϣ ���������շ����γ�ֵ                        
                    FS.HISFC.Models.RADT.PatientInfo patient = radtIntegrate.QueryPatientInfoByInpatientNO(info.PatientNO);
                    if (patient == null)
                    {
                        if (trans == null)          //�����ɱ������ڲ�����
                        {
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴���Ե���FeeIntegrate�Ľ���RollBack
                            feeIntegrate.Rollback();
                        }
                        this.Err = radtIntegrate.Err;
                        return -1;
                    }

                    patient.User01 = "1";
                    hsPatientInfo.Add(info.PatientNO, patient);
                }

                #endregion
            }

            FS.HISFC.Models.Base.Item item = new FS.HISFC.Models.Base.Item();
            ArrayList alList = consManager.GetAllList("CompoundItem");
            if (alList == null)
            {
                if (trans == null)          //�����ɱ������ڲ�����
                {
                    //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴���Ե���FeeIntegrate�Ľ���RollBack
                    feeIntegrate.Rollback();
                }
                this.Err = consManager.Err;
                return -1;
            }
            if (alList.Count > 0)
            {
                FS.HISFC.Models.Base.Const cons = new FS.HISFC.Models.Base.Const();
                //{110FFB2C-EE8A-4378-9DA8-E1681271749F} ������Ч�ĳ���ά����Ŀ �������շ�
                for (int i = 0; i < alList.Count; i++)
                {
                    cons = alList[i] as FS.HISFC.Models.Base.Const;
                    if (cons.IsValid)       //��Ч
                    {
                        break;
                    }
                    cons = new FS.HISFC.Models.Base.Const();
                }

                if (string.IsNullOrEmpty(cons.ID) == true)
                {
                    if (trans == null)          //�����ɱ������ڲ�����
                    {
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴���Ե���FeeIntegrate�Ľ���RollBack
                        feeIntegrate.Rollback();
                    }
                    this.Err = "δ���������������շѵ���Ŀ �޷���ɷ����Զ���ȡ";
                    //{0C5037B6-06FB-4dd8-AED8-B7412D2A6576}  ���ķ���ֵ ����δ����������Ŀ����-0
                    return 0;
                }

                item = feeIntegrate.GetItem(cons.ID);
                if (item == null)
                {
                    if (trans == null)          //�����ɱ������ڲ�����
                    {
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴���Ե���FeeIntegrate�Ľ���RollBack
                        feeIntegrate.Rollback();
                    }
                    this.Err = "δ���������������շѵ���Ŀ �޷���ɷ����Զ���ȡ";
                    //{0C5037B6-06FB-4dd8-AED8-B7412D2A6576}  ���ķ���ֵ ����δ����������Ŀ����-0
                    return 0;
                }
            }
            else
            {
                if (trans == null)          //�����ɱ������ڲ�����
                {
                    //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴���Ե���FeeIntegrate�Ľ���RollBack
                    feeIntegrate.Rollback();
                }
                this.Err = "δ���������������շѵ���Ŀ �޷���ɷ����Զ���ȡ";
                //{0C5037B6-06FB-4dd8-AED8-B7412D2A6576}  ���ķ���ֵ ����δ����������Ŀ����-0
                return 0;
            }

            foreach (FS.HISFC.Models.RADT.PatientInfo info in hsPatientInfo.Values)
            {
                item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(info.User01);

                //{389D4EDA-B312-492a-8EDA-B9D0F9A30041} �жϻ����Ƿ���Ժ
                if (info.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString())
                {
                    if (trans == null)      //�����ɱ������ڲ�����
                    {
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                        feeIntegrate.Rollback();
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                    }
                    this.Err = info.Name + " ���߷���Ժ״̬�����ܽ������÷���ȡ����";
                    return -1;
                }

                if (feeIntegrate.FeeAutoItem(info, item, execDept.ID) == -1)
                {
                    if (trans == null)          //�����ɱ������ڲ�����
                    {
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴���Ե���FeeIntegrate�Ľ���RollBack
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        feeIntegrate.Rollback();
                    }
                    this.Err = feeIntegrate.Err;
                    return -1;
                }
            }

            if (trans == null)          //�����ɱ������ڲ�����
            {
                //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴���Ե���FeeIntegrate�Ľ���Commit
                //FS.FrameWork.Management.PublicTrans.Commit();
                feeIntegrate.Commit();
            }

            return 1;
        }

        /// <summary>
        /// ���������˷�
        /// </summary>
        /// <param name="alOriginalData">סԺ��������</param>
        /// <param name="approveDept">��׼����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int CompoundBackFee(List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> alFeeData, FS.FrameWork.Models.NeuObject approveDept)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region ����������

            //�˷����������
            FS.HISFC.BizLogic.Fee.ReturnApply applyReturn = new FS.HISFC.BizLogic.Fee.ReturnApply();

            //������Ϲ�����
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Fee();

            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴����Integrate����SetTrans
            feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            //this.SetTrans(t.Trans);
            //applyReturn.SetTrans(t.Trans);
            //feeIntegrate.SetTrans(t.Trans);

            #endregion

            DateTime sysTime = applyReturn.GetDateTimeFromSysDateTime();
            string operCode = applyReturn.Operator.ID;
            FS.HISFC.Models.RADT.PatientInfo patientInfo;
            //�����ҩ��ͬʱ�˷�,���������Ϣ
            Pharmacy.IsReturnCharge = this.ctrlIntegrate.GetControlParam<bool>(SysConst.Use_Drug_BackFee, false, false);

            #region �˿�/�˷Ѳ���

            foreach (FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem in alFeeData)
            {
                //ҩƷ�˿�
                if (this.OutputReturn(feeItem, operCode, sysTime) != 1)
                {
                    //{3EE6172A-301B-4d16-91C7-E5D8AC94D942} �˴������feeIntegrate��RollBack
                    //FS.FrameWork.Management.PublicTrans.RollBack();
                    feeIntegrate.Rollback();
                    return -1;
                }
                //���ݿ��Ʋ��� �����Ƿ������˷������ֱ���˷�
                if (Pharmacy.IsReturnCharge)
                {
                    #region �˷Ѵ���   ȡ������Ϣ

                    feeItem.NoBackQty = 0;
                    feeItem.IsNeedUpdateNoBackQty = false;
                    feeItem.PayType = FS.HISFC.Models.Base.PayTypes.SendDruged;

                    patientInfo = this.radtIntegrate.QueryPatientInfoByInpatientNO(feeItem.Patient.ID);

                    //{389D4EDA-B312-492a-8EDA-B9D0F9A30041} �жϻ����Ƿ���Ժ
                    if (patientInfo.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString())
                    {
                        if (trans == null)      //�����ɱ������ڲ�����
                        {
                            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942}  �˴������FeeIntegrate��RollBack
                            feeIntegrate.Rollback();
                            //FS.FrameWork.Management.PublicTrans.RollBack();
                        }
                        this.Err = patientInfo.Name + " ���߷���Ժ״̬�����ܽ�����ҩ�˷Ѳ���";
                        return -1;
                    }

                    if (feeIntegrate.QuitItem(patientInfo, feeItem.Clone()) == -1)
                    {
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942} �˴������feeIntegrate��RollBack
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        feeIntegrate.Rollback();
                        System.Windows.Forms.MessageBox.Show(FS.FrameWork.Management.Language.Msg("�˷�ʧ��!") + feeIntegrate.Err);
                        return -1;
                    }

                    #endregion
                }
                else
                {
                    #region �����˷�����

                    patientInfo = this.radtIntegrate.QueryPatientInfoByInpatientNO(feeItem.Patient.ID);

                    //�����˷�����
                    feeItem.CancelType = FS.HISFC.Models.Base.CancelTypes.Valid;
                    feeItem.IsConfirmed = true;

                    int parm = applyReturn.Apply(patientInfo, feeItem.Clone(), sysTime);
                    if (parm == -1)
                    {
                        //{3EE6172A-301B-4d16-91C7-E5D8AC94D942} �˴������feeIntegrate��RollBack
                        //FS.FrameWork.Management.PublicTrans.RollBack();
                        feeIntegrate.Rollback();
                        System.Windows.Forms.MessageBox.Show(applyReturn.Err);
                        return -1;
                    }

                    #endregion
                }
            }

            #endregion

            //{3EE6172A-301B-4d16-91C7-E5D8AC94D942} �˴������feeIntegrate��Commit
            //FS.FrameWork.Management.PublicTrans.Commit();
            feeIntegrate.Commit();

            return 1;
        }

        /// <summary>
        /// �����˷�
        /// </summary>
        /// <param name="alCompound">����������Ŀ</param>
        /// <param name="approveDept">��׼����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int CompoundBackFee(ArrayList alCompound, FS.FrameWork.Models.NeuObject approveDept)
        {
            FS.HISFC.BizLogic.Fee.InPatient feeInpatientManager = new FS.HISFC.BizLogic.Fee.InPatient();
            FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList = null;
            List<FS.HISFC.Models.Fee.Inpatient.FeeItemList> alFeeList = new List<FS.HISFC.Models.Fee.Inpatient.FeeItemList>();
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alCompound)
            {
                //feeItemList = feeInpatientManager.GetItemListByRecipeNO(info.RecipeNO, info.SequenceNO, true);
                feeItemList = feeInpatientManager.GetItemListByRecipeNO(info.RecipeNO, info.SequenceNO, EnumItemType.Drug);
                if (feeItemList == null)
                {
                    System.Windows.Forms.MessageBox.Show(feeInpatientManager.Err);
                    return -1;
                }
                alFeeList.Add(feeItemList);
            }

            return this.CompoundBackFee(alFeeList, approveDept);
        }

        /// <summary>
        ///  ���������շ�(�����÷����Ӳ��Ϸ���ȡ) by Sunjh 2010-10-26 {74D77EE3-F04E-4d94-A2A3-24902B93C619}
        /// </summary>
        /// <param name="arrayApplyOut">סԺ��������</param>
        /// <param name="execDept">ִ�п���</param>
        /// <param name="trans">����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int CompoundFeeByUsage(ArrayList arrayApplyOut, FS.FrameWork.Models.NeuObject execDept, System.Data.IDbTransaction trans)
        {
            if (trans == null)
            {
                FS.FrameWork.Management.PublicTrans.BeginTransaction();
            }
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #region �����¼

            FS.HISFC.BizProcess.Integrate.RADT radtIntegrate = new RADT();
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            FS.HISFC.BizProcess.Integrate.Fee feeIntegrate = new Fee();

            feeIntegrate.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            #endregion

            #region �γɴ��շ�����

            string privCombo = "-1";
            ArrayList alGroupApplyOut = new ArrayList();
            ArrayList alCombo = new ArrayList();
            FS.HISFC.BizLogic.Order.AdditionalItem AdditionalItemManagement = new FS.HISFC.BizLogic.Order.AdditionalItem();

            #region �������γ�����

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in arrayApplyOut)
            {
                if ((privCombo == info.CompoundGroup && info.CompoundGroup != ""))        //����һ����ͬһ������ˮ
                {
                    continue;
                }
                else			//��ͬ������ˮ��
                {
                    alGroupApplyOut.Add(info);

                    privCombo = info.CompoundGroup;
                }
            }

            #endregion

            #endregion

            System.Collections.Hashtable hsPatientInfo = new Hashtable();

            //alGroupApplyOut����Ϊÿһ����+ҽ����Ϻű���һ������
            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alGroupApplyOut)
            {
                //��ѯ��ȡ���߻�����Ϣ
                FS.HISFC.Models.RADT.PatientInfo patient = radtIntegrate.QueryPatientInfoByInpatientNO(info.PatientNO);
                if (patient == null)
                {
                    if (trans == null)          //�����ɱ������ڲ�����
                    {
                        feeIntegrate.Rollback();
                    }
                    this.Err = radtIntegrate.Err;
                    return -1;
                }
                patient.User01 = "1";
                
                //��ѯ��ȡ�÷���Ӧ�ĸ���
                ArrayList alList = AdditionalItemManagement.QueryAdditionalItem(true, info.Usage.ID, info.RecipeInfo.Dept.ID);
                if (alList == null)
                {
                    if (trans == null)          //�����ɱ������ڲ�����
                    {
                        feeIntegrate.Rollback();
                    }
                    this.Err = consManager.Err;
                    return -1;
                }
                if (alList.Count > 0)
                {                    
                    //������Ч�ĳ���ά����Ŀ �������շ�
                    for (int i = 0; i < alList.Count; i++)
                    {
                        FS.HISFC.Models.Base.Item item = new FS.HISFC.Models.Base.Item();
                        FS.HISFC.Models.Base.Item tempTtem = new FS.HISFC.Models.Base.Item();
                        tempTtem = alList[i] as FS.HISFC.Models.Base.Item;

                        if (tempTtem == null)
                        {
                            if (trans == null)          //�����ɱ������ڲ�����
                            {
                                feeIntegrate.Rollback();
                            }
                            this.Err = "δ���������������շѵ���Ŀ �޷���ɷ����Զ���ȡ";
                            return 0;
                        }

                        item = feeIntegrate.GetItem(tempTtem.ID);
                        if (item == null)
                        {
                            if (trans == null)          //�����ɱ������ڲ�����
                            {
                                feeIntegrate.Rollback();
                            }
                            this.Err = "δ���������������շѵ���Ŀ �޷���ɷ����Զ���ȡ";
                            return 0;
                        }

                        //׼���շ�
                        item.Qty = FS.FrameWork.Function.NConvert.ToDecimal(tempTtem.Qty);

                        if (patient.PVisit.InState.ID.ToString() != FS.HISFC.Models.Base.EnumInState.I.ToString())
                        {
                            if (trans == null)      //�����ɱ������ڲ�����
                            {
                                feeIntegrate.Rollback();
                            }
                            this.Err = info.Name + " ���߷���Ժ״̬�����ܽ������÷���ȡ����";
                            return -1;
                        }

                        if (feeIntegrate.FeeAutoItem(patient, item, execDept.ID) == -1)
                        {
                            if (trans == null)          //�����ɱ������ڲ�����
                            {
                                feeIntegrate.Rollback();
                            }
                            this.Err = feeIntegrate.Err;
                            return -1;
                        }
                    }                    
                }
                else
                {
                    if (trans == null)          //�����ɱ������ڲ�����
                    {
                        feeIntegrate.Rollback();
                    }
                    this.Err = "δ���������������շѵ���Ŀ �޷���ɷ����Զ���ȡ";
                    return 0;
                }                
            }

            if (trans == null)          //�����ɱ������ڲ�����
            {
                feeIntegrate.Commit();
            }

            return 1;
        }


        #endregion

        #region ��ȡҩƷ��Ϣ/�б�

        /// <summary>
        /// ��ȡҩƷ�������ۼ�
        /// </summary>
        /// <param name="drugCode">ҩƷ����</param>
        /// <param name="newPrice">ҩƷ���ۼ�</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int GetDrugNewPrice(string drugCode, ref decimal newPrice)
        {
            this.SetDB(itemManager);

            return this.itemManager.GetNowPrice(drugCode, ref newPrice);
        }

        /// <summary>
        /// ����ҩƷ������ĳһҩƷ��Ϣ
        /// </summary>
        /// <param name="ID">ҩƷ����</param>
        /// <returns>�ɹ�����ҩƷʵ�� ʧ�ܷ���null</returns>
        public FS.HISFC.Models.Pharmacy.Item GetItem(string ID)
        {
            this.SetDB(this.itemManager);

            return this.itemManager.GetItem(ID);
        }

        /// <summary>
        /// ����ҩƷ����ͻ��߿��ң���ȡסԺҽ�����շ�ʹ�õ�ҩƷ����
        /// </summary>
        /// <param name="deptCode">���߿���</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <returns>ҩƷ���ʵ��</returns>
        public FS.HISFC.Models.Pharmacy.Storage GetItemForInpatient(string deptCode, string drugCode)
        {
            this.SetDB(itemManager);

            return this.itemManager.GetItemForInpatient(deptCode, drugCode);
        }

        #region �����޸�

        /// <summary>
        /// סԺ�Ƿ�ʹ��Ԥ�ۿ�� P00200
        /// </summary>
        private int isUseInDrugPreOut = -1;

        /// <summary>
        /// �����Ƿ�ʹ��Ԥ�ۿ�� P00320
        /// </summary>
        private int isUseOutDrugPreOut = -1;

        /// <summary>
        /// ���ȱҩ��ͣ��
        /// </summary>
        /// <param name="drugDept">�ۿ����</param>
        /// <param name="item">��Ŀ</param>
        /// <param name="IsOutPatient">�Ƿ����￪��</param>
        /// <param name="drugItem">���ص���Ŀ��Ϣ</param>
        /// <param name="errInfo">������Ϣ</param>
        /// <returns></returns>
        [Obsolete("���ϣ��Ƶ�FS.SOC.HISFC.BizProcess.OrderIntegrate.OrderBase����", true)]
        public int CheckDrugState(FS.FrameWork.Models.NeuObject drugDept, FS.HISFC.Models.Base.Item item, bool IsOutPatient, ref FS.HISFC.Models.Pharmacy.Item drugItem, ref string errInfo)
        {
            if (item == null)
            {
                errInfo = "��ĿΪ�գ�";
                return -1;
            }
            if (item.ItemType != FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                return 1;
            }

            #region ��ȡ��Ŀ��Ϣ
            drugItem = this.GetItem(item.ID);

            if (drugItem == null)
            {
                errInfo = "��ѯҩƷ��" + item.Name + "��ʧ��:" + this.Err;
                return -1;
            }
            else if (drugItem.ValidState != EnumValidState.Valid)
            {
                //��ȫԺ��ͣ��
                errInfo = "��" + drugItem.Name + "��ȫԺ��ͣ�ã�";
                return -1;
            }
            #endregion

            #region ��ȡ�����Ϣ
            FS.HISFC.Models.Pharmacy.Storage storage = null;

            storage = itemManager.GetStockInfoByDrugCode(drugDept.ID, item.ID);
            if (storage == null)
            {
                errInfo = "����" + itemManager.Err;
                return -1;
            }
            else if (storage.Item.ID == "")
            {
                //�ڸ�ҩ��������  houwb 2011-5-30 ��Ϊȡҩҩ���ж�
                //errInfo = "��" + drugItem.Name + "����" + drugDept.Name + "������!";
                if (IsOutPatient)
                {
                    errInfo = "��" + drugItem.Name + "���ڱ�����û���ҵ���Ӧ����ϵͳ��ȡҩҩ��!";
                }
                else
                {
                    errInfo = "��" + drugItem.Name + "���ڱ�����û���ҵ���ӦסԺϵͳ��ȡҩҩ��!";
                }
                return -1;
            }
            else if (storage.ValidState != FS.HISFC.Models.Base.EnumValidState.Valid)
            {
                //��ҩ����ͣ��
                errInfo = "��" + drugItem.Name + "����" + drugDept.Name + "��ͣ��!";
                return -1;
            }
            if (IsOutPatient)
            {
                if (!storage.IsUseForOutpatient)
                {
                    //������������
                    errInfo = "��" + drugItem.Name + "����" + drugDept.Name + "����Ϊ��������������ҩ!";
                    return -1;
                }
                //����ȱҩ
                else if (storage.IsLack)
                {
                    //ҩ��ȱҩ
                    errInfo = "��" + drugItem.Name + "��" + drugDept.Name + "��ȱҩ!";
                    return -1;
                }

                if (isUseOutDrugPreOut == -1)
                {
                    isUseOutDrugPreOut = this.ctrlIntegrate.GetControlParam<int>(PharmacyConstant.OutDrug_Pre_Out, true);
                }

                if (isUseOutDrugPreOut == 1)
                {
                    if (storage.StoreQty - storage.PreOutQty <= 0)
                    {
                        errInfo = "��" + drugItem.Name + "����" + drugDept.Name + "���Ϊ0����������!";
                        return -1;
                    }
                }
                else
                {
                    if (storage.StoreQty <= 0)
                    {
                        errInfo = "��" + drugItem.Name + "����" + drugDept.Name + "���Ϊ0����������!";
                        return -1;
                    }
                }
            }
            else
            {
                if (!storage.IsUseForInpatient)
                {
                    //������������
                    errInfo = "��" + drugItem.Name + "����" + drugDept.Name + "����Ϊ��������סԺ��ҩ!";
                    return -1;
                }
                //סԺȱҩ
                else if (storage.IsLackForInpatient)
                {
                    //ҩ��ȱҩ
                    errInfo = "��" + drugItem.Name + "��" + drugDept.Name + "��ȱҩ!";
                    return -1;
                }

                if (isUseInDrugPreOut == -1)
                {
                    isUseInDrugPreOut = this.ctrlIntegrate.GetControlParam<int>(PharmacyConstant.InDrug_Pre_Out, true);
                }

                if (isUseInDrugPreOut == 1)
                {
                    if (storage.StoreQty - storage.PreOutQty <= 0)
                    {
                        errInfo = "��" + drugItem.Name + "����" + drugDept.Name + "���Ϊ0����������!";
                        return -1;
                    }
                }
                else
                {
                    if (storage.StoreQty <= 0)
                    {
                        errInfo = "��" + drugItem.Name + "����" + drugDept.Name + "���Ϊ0����������!";
                        return -1;
                    }
                }
            }
            #endregion
            return 1;
        }

        /// <summary>
        /// ����ҩƷ����ͻ��߿��ң���ȡ��Ӧȡҩҩ���Ŀ��
        /// ���ӷ������� houwb 2011-5-30
        /// </summary>
        /// <param name="deptCode">���߿���</param>
        /// <param name="sendType">��������</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <returns>ҩƷ���ʵ��</returns>
        public FS.HISFC.Models.Pharmacy.Storage GetItemStorage(string deptCode, string sendType, string drugCode)
        {
            this.SetDB(itemManager);

            return this.itemManager.GetItemStorage(deptCode, sendType, drugCode);
        }

        ArrayList alStock = null;

        /// <summary>
        /// ����ҩƷ����ͻ��߿��ң���ȡ��Ӧȡҩҩ���Ŀ��
        /// ���ӷ������� houwb 2011-5-30
        /// </summary>
        /// <param name="order">����ҽ��</param>
        /// <param name="deptCode">���߿���</param>
        /// <param name="sendType">��������</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <returns>ҩƷ���ʵ��</returns>
        public FS.HISFC.Models.Pharmacy.Storage GetItemStorage(FS.HISFC.Models.Order.Inpatient.Order order, string deptCode, string sendType, string drugCode)
        {
            this.SetDB(itemManager);

            FS.HISFC.Models.Pharmacy.Storage storage = this.itemManager.GetItemStorage(deptCode, sendType, drugCode);
            if (storage == null)
            {
                return null;
            }

            if (order.OrderType.ID == "CZ")
            {
                FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();
                if (alStock == null)
                {
                    alStock = constantManager.GetList("CompoundStock");
                    if (alStock == null)
                    {
                        this.Err = "��ȡ�������������" + constantManager.Err;
                    }
                }
                foreach (FS.HISFC.Models.Base.Const consInfo in alStock)
                {
                    if (consInfo.ID == order.Usage.ID)
                    {
                        storage.StockDept.ID = consInfo.Name;
                        break;
                    }
                }
            }

            return storage;
        }

        #endregion

        /// <summary>
        /// ����ҩƷ����ͻ��߿��ң���ȡסԺҽ�����շ�ʹ�õ�ҩƷ����
        /// </summary>
        /// <param name="order">����ҽ��</param>
        /// <param name="deptCode">���߿���</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <returns>ҩƷ���ʵ��</returns>
        public FS.HISFC.Models.Pharmacy.Storage GetItemForInpatient(FS.HISFC.Models.Order.Inpatient.Order order ,string deptCode, string drugCode)
        {
            this.SetDB(itemManager);

            FS.HISFC.Models.Pharmacy.Storage storage = this.itemManager.GetItemForInpatient(deptCode, drugCode);
            if (storage == null)
            {
                return null;
            }

            if (order.OrderType.ID == "CZ")
            {
                FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();            
                ArrayList alStock = constantManager.GetList("CompoundStock");
                if (alStock == null)
                {
                    this.Err = "��ȡ�������������" + constantManager.Err;
                }
                foreach (FS.HISFC.Models.Base.Const consInfo in alStock)
                {
                    if (consInfo.ID == order.Usage.ID)
                    {
                        storage.StockDept.ID = consInfo.Name;
                        break;
                    }
                }
            }

            return storage;
        }

        /// <summary>
        /// ��ȡ����ҽ�����շ�ʹ�õ�ҩƷ����
        /// </summary>
        /// <param name="deptCode">ȡҩ����</param>
        /// <returns>�ɹ�����ҩƷ���� ʧ�ܷ���null</returns>
        public ArrayList QueryItemAvailableListForClinic(string deptCode)
        {
            this.SetDB(itemManager);

            return itemManager.QueryItemAvailableListForClinic(deptCode);
        }

        /// <summary>
        /// ��ȡ�Ƴ��õ�ҩƷ����
        /// </summary>
        /// <param name="deptCode">ȡҩ����</param>
        /// <returns>�ɹ�����ҩƷ���� ʧ�ܷ���null</returns>
        public ArrayList QueryDeptAlwaysUsedItem(string deptCode)
        {
            this.SetDB(itemManager);

            return itemManager.QueryDeptAlwaysUsedItem(deptCode);
        }

        /// <summary>
        /// ��ȡסԺҽ�����շ�ʹ�õ�ҩƷ����
        /// </summary>
        /// <param name="deptCode">ȡҩ����</param>
        /// <returns>�ɹ�����ҩƷ���� ʧ�ܷ���null</returns>
        public ArrayList QueryItemAvailableList(string deptCode)
        {
            this.SetDB(itemManager);

            return itemManager.QueryItemAvailableList(deptCode);
        }

        /// <summary>
        /// ��ȡסԺҽ�����շ�ʹ�õ�ĳһ����ҩƷ����
        /// </summary>
        /// <param name="deptCode">ȡҩ����</param>
        /// <param name="drugType">ҩƷ��� ����ALL��ȡȫ��ҩƷ���</param>
        /// <returns>�ɹ�����ҩƷ�б� ʧ�ܷ���null</returns>
        public ArrayList QueryItemAvailableList(string deptCode, string drugType)
        {
            this.SetDB(itemManager);

            ArrayList al = itemManager.QueryItemAvailableList(deptCode, drugType);

            if (FS.HISFC.BizProcess.Integrate.Pharmacy.IsNostrumManageStore)
            {
                List<FS.HISFC.Models.Pharmacy.Item> nostrumList = itemManager.QueryNostrumList("C");
                if (nostrumList == null)
                {
                    return null;
                }

                al.AddRange(new ArrayList(nostrumList.ToArray()));
            }

            return al;
        }
        
        /// <summary>
        /// ���ȫ��ҩƷ��Ϣ�б����ݲ����ж��Ƿ���ʾ��������
        /// </summary>
        /// <param name="IsShowSimple">�Ƿ���ʾ��������</param>
        /// <returns>�ɹ�����ҩƷ��Ϣ�������� ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemList(bool IsShowSimple)
        {
            this.SetDB(itemManager);

            return itemManager.QueryItemList(IsShowSimple);
        }

        /// <summary>
        /// ��ÿ���ҩƷ��Ϣ�б�
        /// </summary>
        /// <returns>�ɹ�����ҩƷ��Ϣ ʧ�ܷ���null</returns>
        public System.Data.DataSet QueryItemValidList()
        {
            this.SetDB(itemManager);

            return itemManager.QueryItemValidList();
        }

        /// <summary>
        /// ��ÿ���ҩƷ��Ϣ�б�
        /// ����ͨ������ѡ���Ƿ���ʾ���ֻ�����Ϣ�ֶ�
        /// </summary>
        /// <param name="IsShowSimple">�Ƿ���ʾ����Ϣ</param>
        /// <returns>�ɹ�����ҩƷ��Ϣ���� ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemAvailableList(bool IsShowSimple)
        {
            this.SetDB(itemManager);

            return itemManager.QueryItemAvailableList(IsShowSimple);
        }

        /// <summary>
        /// ��ȡҩƷ�б�����Ϣ
        /// </summary>
        /// <param name="deptCode">ȡҩ����</param>
        /// <param name="doctCode">ҽ������</param>
        /// <param name="doctGrade">ҽ���ȼ�</param>
        /// <returns>�ɹ����ؿ����Ϣ���� ʧ�ܷ���null �����ݷ��ؿ�����</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemAvailableList(string deptCode, string doctCode, string doctGrade)
        {
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            this.SetDB(consManager);
            consManager.SetTrans(trans);
            this.SetDB(itemManager);

            ArrayList al = consManager.GetList("SpeDrugGrade");
            if (al == null || al.Count == 0)
            {
                //��ҽ��ְ����ȼ���Ӧ��Ϣ
                return itemManager.QueryItemAvailableList(deptCode, doctCode, null);
            }
            else
            {
                string drugGradeCollection = "";
                foreach (FS.HISFC.Models.Base.Const consInfo in al)
                {
                    //{3972BA6D-5CE4-4995-90AA-30DD281D1660}
                    if (consInfo.ID.IndexOf("|") != -1)
                    {
                        consInfo.ID = consInfo.ID.Substring(0, consInfo.ID.IndexOf("|"));       //����ַ� ��ȡҽ��ְ��
                    }
                    if (consInfo.ID == doctGrade)
                    {
                        //{3972BA6D-5CE4-4995-90AA-30DD281D1660}
                        if (drugGradeCollection == "")
                            drugGradeCollection = consInfo.Name;
                        else
                            drugGradeCollection = drugGradeCollection + "','" + consInfo.Name;
                        //return itemManager.QueryItemAvailableList(deptCode, doctCode, consInfo.Name);
                    }
                }

                if (drugGradeCollection != "")
                {
                    return itemManager.QueryItemAvailableList(deptCode, doctCode, drugGradeCollection);
                }
                //��ҽ��ְ����ȼ���Ӧ��Ϣ
                return itemManager.QueryItemAvailableList(deptCode, doctCode, null);
            }
        }

        /// <summary>
        /// ��ȡҩƷ�б�����Ϣ
        /// </summary>
        /// <param name="deptCode">ȡҩ����</param>
        /// <param name="doctCode">ҽ������</param>
        /// <param name="doctGrade">ҽ���ȼ�</param>
        /// <returns>�ɹ����ؿ����Ϣ���� ʧ�ܷ���null �����ݷ��ؿ�����</returns>
        public ArrayList QueryItemAvailableArrayList(string deptCode, string doctCode, string doctGrade)
        {
            List<FS.HISFC.Models.Pharmacy.Item> al = this.QueryItemAvailableList(deptCode, doctCode, doctGrade);

            if (al == null)
            {
                return null;
            }

            return new ArrayList(al.ToArray());
        }

        #region ����ҩƷ��𡢷�ҩ���ͻ�ȡ�����ϸ

        /// <summary>
        /// ��ȡҽ�����շ�ʹ�õ�ҩƷ����
        /// </summary>
        /// <param name="deptCode">ȡҩ����</param>
        /// <param name="doctCode">ҽ������</param>
        /// <param name="drugGrade">ҩƷ�ȼ�</param>
        /// <param name="sendType">��ҩ���ͣ�O ���ﴦ����I סԺҽ����A ȫ��</param>
        /// <returns>�ɹ�����ҩƷ���� ʧ�ܷ���null �������������ݷ��ؿ�����</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemAvailableListBySendType(string deptCode, string doctCode, string doctGrade, string sendType)
        {
            FS.HISFC.BizLogic.Manager.Constant consManager = new FS.HISFC.BizLogic.Manager.Constant();
            this.SetDB(consManager);
            consManager.SetTrans(trans);
            this.SetDB(itemManager);

            ArrayList al = consManager.GetList("SpeDrugGrade");
            if (al == null || al.Count == 0)
            {
                //��ҽ��ְ����ȼ���Ӧ��Ϣ
                return itemManager.QueryItemAvailableListBySendType(deptCode, doctCode, null, sendType);
            }
            else
            {
                string drugGradeCollection = "";
                foreach (FS.HISFC.Models.Base.Const consInfo in al)
                {
                    //{3972BA6D-5CE4-4995-90AA-30DD281D1660}
                    if (consInfo.ID.IndexOf("|") != -1)
                    {
                        consInfo.ID = consInfo.ID.Substring(0, consInfo.ID.IndexOf("|"));       //����ַ� ��ȡҽ��ְ��
                    }
                    if (consInfo.ID == doctGrade)
                    {
                        //{3972BA6D-5CE4-4995-90AA-30DD281D1660}
                        if (drugGradeCollection == "")
                            drugGradeCollection = consInfo.Name;
                        else
                            drugGradeCollection = drugGradeCollection + "','" + consInfo.Name;
                        //return itemManager.QueryItemAvailableList(deptCode, doctCode, consInfo.Name);
                    }
                }

                if (drugGradeCollection != "")
                {
                    return itemManager.QueryItemAvailableListBySendType(deptCode, doctCode, drugGradeCollection, sendType);
                }
                //��ҽ��ְ����ȼ���Ӧ��Ϣ
                return itemManager.QueryItemAvailableListBySendType(deptCode, doctCode, null, sendType);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="doctCode"></param>
        /// <param name="doctGrade"></param>
        /// <param name="sendType"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemAvailableListByBoJi(string deptCode, string doctCode, string doctGrade, string sendType)
        {
            this.SetDB(itemManager);
            return itemManager.QueryItemAvailableListByBoJi(deptCode, doctCode, null, sendType);

        }

        /// <summary>
        /// ����ҩƷ��š��������һ�ȡҽ�����շ�ʹ�õ�ҩƷ����
        /// </summary>
        /// <param name="deptCode">��������</param>
        /// <param name="sendType">��ҩ���ͣ�O ���ﴦ����I סԺҽ��</param>
        /// <param name="itemCode">ҩƷ���</param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemAvailableListByItemCode(string deptCode, string sendType, string itemCode)
        {
            this.SetDB(itemManager);
            return itemManager.QueryItemAvailableListByItemCode(deptCode, sendType, itemCode);
        }

        /// <summary>
        /// ��ȡҽ�����շ�ʹ�õ�ҩƷ����
        /// </summary>
        /// <param name="deptCode">ȡҩ����</param>
        /// <param name="doctCode">ҽ������</param>
        /// <param name="drugGrade">ҩƷ�ȼ�</param>
        /// <param name="sendType">��ҩ���ͣ�O ���ﴦ����I סԺҽ����A ȫ��</param>
        /// <returns>�ɹ�����ҩƷ���� ʧ�ܷ���null �������������ݷ��ؿ�����</returns>
        public ArrayList QueryItemAvailableArrayListBySendType(string deptCode, string doctCode, string doctGrade, string drugType, string sendType)
        {
            List<FS.HISFC.Models.Pharmacy.Item> al = this.QueryItemAvailableListBySendType(deptCode, doctCode, doctGrade, sendType);

            if (al == null)
            {
                return null;
            }

            return new ArrayList(al.ToArray());
        }

        #endregion

        #endregion

        #region ��ҩ����Ϣ�ж� �ж���Ӧ��ҩƷҽ���Ƿ���ά���˶�Ӧ��ҩƷ��ҩ��

        /// <summary>
        /// �ж���Ӧ��ҩƷҽ���Ƿ���ά���˶�Ӧ��ҩƷ��ҩ��
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool IsHaveDrugBill(FS.HISFC.Models.Order.Order order)
        {
            
            return true;
        }

        /// <summary>
        /// �ж���Ӧ��ҩƷҽ���Ƿ���ά���˶�Ӧ��ҩƷ��ҩ��
        /// </summary>
        /// <param name="orderType">ҽ�����</param>
        /// <param name="usageCode">�÷�</param>
        /// <param name="drugType">ҩƷ���</param>
        /// <param name="drugQuality">ҩƷ����</param>
        /// <param name="dosageFormCode">����</param>
        /// <returns>�Ѵ���ά���ĵ� ����True ���򷵻�False</returns>
        public bool IsHaveDrugBill(string orderType,string usageCode,string drugType,string drugQuality,string dosageFormCode)
        {
            FS.HISFC.Models.Pharmacy.DrugBillClass findDrugBill = drugStoreManager.GetDrugBillClass(orderType, usageCode, drugType, drugQuality, dosageFormCode);
           
            if (findDrugBill == null || findDrugBill.ID == "")
                return false;
            else
                return true;
        }

        #endregion

        #region �������� �Է���/ҽ�� ���� ʹ��  �Ƿ�Ԥ���� סԺ�����Ƿ��ҩʱ�շ��ж� ������ÿ��Ʋ����̵�

        #region סԺ����

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="sequenceNO">������ˮ��</param>
        /// <returns>�ɹ� ������Ϣ ʧ�� null</returns>
        public FS.HISFC.Models.Pharmacy.ApplyOut GetApplyOut(string recipeNO, int sequenceNO)
        {
            this.SetDB(itemManager);

            return itemManager.GetApplyOut(recipeNO, sequenceNO);
        }

        /// <summary>
        /// ���������Ϣ
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <returns>�ɹ� ������Ϣ ʧ�� null</returns>
        public ArrayList QueryApplyOut(string recipeNO)
        {
            this.SetDB(itemManager);

            return itemManager.QueryApplyOut(recipeNO);
        }

        /// <summary>
        /// ������⣭����ҽ����ϵͳ�����ĺ���
        /// </summary>
        /// <param name="execOrder">ҽ��ִ��ʵ��</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="isRefreshStockDept">�Ƿ��������������»�ȡȡҩ����</param>
        /// <returns>0û��ɾ�� 1�ɹ� -1ʧ��</returns>
        public int ApplyOut(FS.HISFC.Models.Order.ExecOrder execOrder, DateTime operDate,bool isRefreshStockDept)
        {
            this.SetDB(itemManager);

            //�����ҩʱ�Ʒ� �򲻽���Ԥ�ۿ����� ���� Ԥ�ۿ��   SysConst.Use_Drug_ApartFee "100003"
            Pharmacy.IsApproveCharge = !this.ctrlIntegrate.GetControlParam<bool>(SysConst.Use_Drug_ApartFee, false, true);
            //��ҩ����������� 0 ���� 1 ����վ
            string applyDeptType = this.ctrlIntegrate.GetControlParam<string>(SysConst.Use_Drug_ApplyNurse, false, "0");

            if (Pharmacy.IsApproveCharge)
            {
                string property = this.GetDrugProperty(execOrder.Order.Item.ID, ((FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item).DosageForm.ID, execOrder.Order.Patient.PVisit.PatientLocation.Dept.ID);
                if (property == "0")
                {
                    execOrder.Order.Qty = (decimal)System.Math.Ceiling((double)execOrder.Order.Qty);
                }
            }
            
            //�Ƿ�ʵ��Ԥ�ۿ�����
            //{F766D3A5-CC25-4dd7-809E-3CBF9B152362}  ����Ԥ�ۿ�涯�������ͳһ���
            Pharmacy.IsInPatientPreOut = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Pre_Out, false, true);

            //{8113BE34-A5E0-4d87-B6FF-B8428BAA8711}  ��{F766D3A5-CC25-4dd7-809E-3CBF9B152362} �Ĳ��� 
            //��ΪApplyOutҵ��㺯����߶�ApplyOutʵ�帳ֵʱʹ���˴������ ���Դ˴�����ֱ�Ӵ���False
            //{2ACF86B5-9C2E-406f-8F58-0500D02EEF94}
            //return itemManager.ApplyOut(execOrder, operDate, Pharmacy.IsInPatientPreOut, applyDeptType, isRefreshStockDept);
            return this.ApplyOut(execOrder, operDate, Pharmacy.IsInPatientPreOut, applyDeptType, isRefreshStockDept);
        }

        /// <summary>
        /// ������⣭����ҽ����ϵͳ�����ĺ���//{4828DBC4-F5CA-4f1c-A3DE-CEBBAD646910}
        /// </summary>
        /// <param name="execOrderList">ҽ��ִ������</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="isRefreshStockDept">�Ƿ��������������»�ȡȡҩ����</param>
        /// <returns>0û��ɾ�� 1�ɹ� -1ʧ��</returns>
        public int ApplyOutByExeOrder(ArrayList execOrderList, DateTime operDate, bool isRefreshStockDept,ref string err)
        {
            this.SetDB(itemManager);

            //�����ҩʱ�Ʒ� �򲻽���Ԥ�ۿ����� ���� Ԥ�ۿ��   SysConst.Use_Drug_ApartFee "100003"
            Pharmacy.IsApproveCharge = !this.ctrlIntegrate.GetControlParam<bool>(SysConst.Use_Drug_ApartFee, false, true);
            //��ҩ����������� 0 ���� 1 ����վ
            string applyDeptType = this.ctrlIntegrate.GetControlParam<string>(SysConst.Use_Drug_ApplyNurse, false, "0");

            if (Pharmacy.IsApproveCharge)
            {
                foreach (FS.HISFC.Models.Order.ExecOrder execOrder in execOrderList)
                {
                    string property = this.GetDrugProperty(execOrder.Order.Item.ID, ((FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item).DosageForm.ID, execOrder.Order.Patient.PVisit.PatientLocation.Dept.ID);
                    if (property == "0")
                    {
                        execOrder.Order.Qty = (decimal)System.Math.Ceiling((double)execOrder.Order.Qty);
                    }
                }
            }

            //�Ƿ�ʵ��Ԥ�ۿ�����
            //{F766D3A5-CC25-4dd7-809E-3CBF9B152362}  ����Ԥ�ۿ�涯�������ͳһ���
            Pharmacy.IsInPatientPreOut = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Pre_Out, false, true);

            //{8113BE34-A5E0-4d87-B6FF-B8428BAA8711}  ��{F766D3A5-CC25-4dd7-809E-3CBF9B152362} �Ĳ��� 
            //��ΪApplyOutҵ��㺯����߶�ApplyOutʵ�帳ֵʱʹ���˴������ ���Դ˴�����ֱ�Ӵ���False
            //{2ACF86B5-9C2E-406f-8F58-0500D02EEF94}
            //return itemManager.ApplyOut(execOrder, operDate, Pharmacy.IsInPatientPreOut, applyDeptType, isRefreshStockDept);
            return this.ApplyOut(execOrderList, operDate, Pharmacy.IsInPatientPreOut, applyDeptType, isRefreshStockDept,ref err);
        }

        /// <summary>
        /// ������� -- ��ҽ����ϵͳ�������� ���ݴ����ҽ�����п��ͳһԤ��
        /// 
        /// {F766D3A5-CC25-4dd7-809E-3CBF9B152362}  ���һ��ҽ���ֽ�Ŀ��ͳһԤ��
        /// </summary>
        /// <param name="execOrderList">ҽ��ִ����Ϣ</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="isRefreshStockDept">�Ƿ��������������»�ȡȡҩ����</param>
        /// <returns>0û�в��� 1�ɹ� -1ʧ��</returns>
        public int InpatientDrugPreOutNum(List<FS.HISFC.Models.Order.ExecOrder> execOrderList, DateTime operDate, bool isRefreshStockDept)
        {
            //{C37BEC96-D671-46d1-BCDD-C634423755A4}  ȡ�����ֿ��Ԥ�۹���ģʽ�����´�������
            return 1;

            //���´�������

            #region ԭ�п��Ԥ�۹���ģʽ����

            ////�Ƿ�ʵ��Ԥ�ۿ�����
            //Pharmacy.IsInPatientPreOut = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Pre_Out, false, true);
            //if (!Pharmacy.IsInPatientPreOut)
            //{
            //    return 1;
            //}

            //this.SetDB(itemManager);

            //Dictionary<string, System.Data.DataRow> storePreOutNum = new Dictionary<string, System.Data.DataRow>();

            //System.Data.DataTable preOutDataTable = new System.Data.DataTable();
            //preOutDataTable.Columns.AddRange(new DataColumn[] {														 
            //                                            new DataColumn("ҩƷ����",  System.Type.GetType("System.String")),
            //                                            new DataColumn("ҩƷ����",  System.Type.GetType("System.String")),
            //                                            new DataColumn("���ұ���",  System.Type.GetType("System.String")),//2
            //                                            new DataColumn("����",   System.Type.GetType("System.Decimal")) 
            //                                        });
            //DataColumn[] keyColumn = new DataColumn[] { preOutDataTable.Columns["ҩƷ����"], preOutDataTable.Columns["���ұ���"] };
            //preOutDataTable.PrimaryKey = keyColumn;

            //foreach (FS.HISFC.Models.Order.ExecOrder info in execOrderList)
            //{
            //    DataRow findDr = preOutDataTable.Rows.Find(new object[] { info.Order.Item.ID, info.Order.StockDept.ID });
            //    if (findDr != null)
            //    {
            //        findDr["����"] = FS.FrameWork.Function.NConvert.ToDecimal(findDr["����"]) + info.Order.Qty;
            //    }
            //    else
            //    {
            //        DataRow newDr = preOutDataTable.NewRow();
            //        newDr["ҩƷ����"] = info.Order.Item.ID;
            //        newDr["ҩƷ����"] = info.Order.Item.Name;
            //        newDr["���ұ���"] = info.Order.StockDept.ID;
            //        newDr["����"] = info.Order.Qty;

            //        preOutDataTable.Rows.Add(newDr);
            //    }
            //}

            //preOutDataTable.DefaultView.Sort = "���ұ���,ҩƷ����";

            //for (int i = 0; i < preOutDataTable.DefaultView.Count; i++)
            //{
            //    DataRow viewRow = preOutDataTable.DefaultView[i].Row;

            //    FS.HISFC.Models.Pharmacy.Storage stockInfo = this.itemManager.GetStockInfoByDrugCode(viewRow["���ұ���"].ToString(), viewRow["ҩƷ����"].ToString());
            //    if (stockInfo == null)
            //    {
            //        return -1;
            //    }
            //    //���ڿ���������жϵĵط� ��Ҫ�ж�Ԥ�ۿ��  {5D32F201-AD50-4d0e-A89E-0231B5F0B488}
            //    if (FS.FrameWork.Function.NConvert.ToDecimal(viewRow["����"]) > (stockInfo.StoreQty - stockInfo.PreOutQty))
            //    {
            //        this.Err = viewRow["ҩƷ����"].ToString() + " ҩƷ��治�㣡";
            //        return -1;
            //    }

            //    if (itemManager.UpdateStoragePreOutNum(viewRow["���ұ���"].ToString(), viewRow["ҩƷ����"].ToString(), FS.FrameWork.Function.NConvert.ToDecimal(viewRow["����"])) == -1)
            //    {
            //        return -1;
            //    }
            //}

            //return 1;

            #endregion
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="applyOut">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        [System.Obsolete("ԭ���������ģʽ���� ����ApplyOut���غ���ʵ��", true)]
        public int InsertApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            this.SetDB(itemManager);

            return itemManager.InsertApplyOut(applyOut);
        }

        /// <summary>
        /// ����������Ϣ
        /// 
        /// {C37BEC96-D671-46d1-BCDD-C634423755A4}
        /// </summary>
        /// <param name="applyOut">������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int ApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            this.SetDB(itemManager);

            applyOut.ID = null;

            if (itemManager.InsertApplyOut(applyOut) == -1)
            {
                return -1;
            }

            //�Ƿ�ʵ��Ԥ�ۿ�����
            Pharmacy.IsInPatientPreOut = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Pre_Out, false, true);

            if (Pharmacy.IsInPatientPreOut)
            {
                return itemManager.InsertPreoutStore(applyOut);
            }

            return 1;
        }
        
        /// <summary>
        /// ������⣭���Է��ù����ĺ���
        /// </summary>
        /// <param name="patient">������Ϣʵ��</param>
        /// <param name="feeItem">���߷�����Ϣʵ��</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="isRefreshStockDept">�Ƿ��������������»�ȡȡҩ����</param>
        /// <returns>0û��ɾ�� 1�ɹ� -1ʧ��</returns>
        public int ApplyOut(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem, DateTime operDate, bool isRefreshStockDept)
        {
            this.SetDB(itemManager);

            ////�����ҩʱ�Ʒ� �򲻽���Ԥ�ۿ����� ���� Ԥ�ۿ��
            //Pharmacy.IsApproveCharge = Pharmacy.QueryControlForBool("501003", false);
            //�����ҩʱ�Ʒ� �򲻽���Ԥ�ۿ����� ���� Ԥ�ۿ��   SysConst.Use_Drug_ApartFee "100003"
            Pharmacy.IsApproveCharge = !this.ctrlIntegrate.GetControlParam<bool>(SysConst.Use_Drug_ApartFee, false, true);
            //��ҩ����������� 0 ���� 1 ����վ
            string applyDeptType = this.ctrlIntegrate.GetControlParam<string>(SysConst.Use_Drug_ApplyNurse, false, "0");

            //�Ƿ�ʵ��Ԥ�ۿ�����
            Pharmacy.IsInPatientPreOut = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Pre_Out, false, true);

            FS.HISFC.BizLogic.Manager.Department deptMgr = new FS.HISFC.BizLogic.Manager.Department();
            FS.HISFC.Models.Base.Department dept = deptMgr.GetDeptmentById(((FS.HISFC.Models.Base.Employee)deptMgr.Operator).Dept.ID);
            if (dept != null && (dept.SpecialFlag == "1" || dept.SpecialFlag == "2"))
            {
                FS.HISFC.Models.Fee.Inpatient.FeeItemList f = feeItem.Clone();
                f.ExecOper.Dept = dept;
                return itemManager.ApplyOut(patient, f, operDate, Pharmacy.IsInPatientPreOut, "0", isRefreshStockDept);

            }

            return itemManager.ApplyOut(patient, feeItem, operDate, Pharmacy.IsInPatientPreOut, applyDeptType, isRefreshStockDept);
        }

        /// <summary>
        /// �����˿⣭���Է�����ϵͳ�����ĺ���
        /// </summary>
        /// <param name="patient">������Ϣʵ��</param>
        /// <param name="feeItem">������Ϣʵ��</param>
        /// <param name="operDate">����ʱ��</param>
        /// <returns>0û�и��� 1�ɹ� -1ʧ��</returns>
        public int ApplyOutReturn(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem, DateTime operDate)
        {
            this.SetDB(itemManager);

            //��ҩ����������� 0 ���� 1 ����վ
            string applyDeptType = this.ctrlIntegrate.GetControlParam<string>(SysConst.Use_Drug_ApplyNurse, false, "0");

            return itemManager.ApplyOutReturn(patient, feeItem, operDate, applyDeptType);
        }

        //{3E83AFA1-C364-4f72-8DFD-1B733CB9379E}
        //���Ӳ�ѯ�����Ƿ���δ��˵���ҩ��¼,Ϊ��Ժ�Ǽ��ж��� Add by ���� 2009.6.10

        /// <summary>
        ///  ��ѯסԺ�����Ƿ���δȷ�ϵ���ҩ����
        /// </summary>
        /// <param name="inpatientNO">����סԺ��ˮ��</param>
        /// <returns>�ɹ� > 0 ��¼ 0 û�м�¼ -1 ����</returns>
        public int QueryNoConfirmQuitApply(string inpatientNO) 
        {
            this.SetDB(itemManager);

            return this.itemManager.QueryNoConfirmQuitApply(inpatientNO);
        }
        ////{3E83AFA1-C364-4f72-8DFD-1B733CB9379E} ������
        #endregion

        #region ��סԺҽ���������� ���ܴ��� ���ڰ�ҩʱ�շ�

        /// <summary>
        /// ������� ���� ҽ�����ܴ��� �����ڰ�ҩʱ�շѵĴ���
        /// ��ͬһҽ����ˮ�Ž��л���
        /// </summary>
        /// <param name="alExeOrder">ҽ��ִ��ʵ������</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="isRefreshStockDept">�Ƿ��������������»�ȡȡҩҩ��</param>
        /// <returns>1 �ɹ� ��1 ʧ��</returns>
        public int ApplyOut(ArrayList alExeOrder, DateTime operDate, bool isRefreshStockDept)
        {
            this.SetDB(itemManager);

            ArrayList alFeeExeOrder = new ArrayList();

            //�����Ѵ�����
            System.Collections.Hashtable hsOrderNO = new Hashtable();
            //ҽ��ѭ������
            foreach (FS.HISFC.Models.Order.ExecOrder exeOrder in alExeOrder)
            {
                #region ҽ�����ܴ���

                if (!exeOrder.Order.OrderType.IsDecompose)      //ҽ�����ֽ⣨��ʱҽ����
                {
                    alFeeExeOrder.Add(exeOrder);
                }
                else
                {                   
                    string feeFlag = "1";
                    bool isFee = false;
                    decimal feeNum = exeOrder.Order.Qty;
                    decimal phaNum = 0;
                    //if (itemManager.PatientStore(exeOrder, ref feeFlag, ref feeNum, ref isFee) == -1)
                    if (this.PatientStoreNew(exeOrder, ref feeFlag, ref feeNum, ref isFee, ref phaNum) == -1)
                    {
                        return -1;
                    }
                    switch (feeFlag)
                    {
                        case "0":           //����ƷѴ���
                            continue;
                        case "1":           //��ָ�������ƷѴ���  ��ʱfeeNum�����ѷ����仯
                        case "2":           //��ԭ���̴���
                            exeOrder.Order.Qty = feeNum;
                            break;
                    }
                    //��ͬһҽ����ˮ�Ž��л���
                    if (hsOrderNO.ContainsKey(exeOrder.Order.ID))
                    {
                        FS.HISFC.Models.Order.ExecOrder feeExeOrder = hsOrderNO[exeOrder.Order.ID] as FS.HISFC.Models.Order.ExecOrder;
                        feeExeOrder.Order.Qty = feeExeOrder.Order.Qty + exeOrder.Order.Qty;
                    }
                    else
                    {
                        hsOrderNO.Add(exeOrder.Order.ID, exeOrder);
                    }
                }

                #endregion
            }

            //��ҩ����������� 0 ���� 1 ����վ
            string applyDeptType = this.ctrlIntegrate.GetControlParam<string>(SysConst.Use_Drug_ApplyNurse, false, "0");
            foreach (FS.HISFC.Models.Order.ExecOrder feeExeOrder in alFeeExeOrder)
            {
                //{2ACF86B5-9C2E-406f-8F58-0500D02EEF94}
                //itemManager.ApplyOut(feeExeOrder, operDate, false, applyDeptType, isRefreshStockDept);
                this.ApplyOut(feeExeOrder, operDate, false, applyDeptType, isRefreshStockDept);
            }
            return 1;
        }

        #endregion
        //{2ACF86B5-9C2E-406f-8F58-0500D02EEF94}
        /// <summary>
        /// ������⣭����ҽ����ϵͳ�����ĺ���
        /// </summary>
        /// <param name="execOrder">ҽ��ִ��ʵ��</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="isPreOut">�Ƿ�Ԥ����</param>
        /// <param name="applyDeptType">����������� 0 ���� 1 ����վ</param>
        /// <param name="getStockDept">�Ƿ����������һ�ȡȡҩҩ��</param>
        /// <returns>0û��ɾ�� 1�ɹ� -1ʧ��</returns>
        private int ApplyOut(FS.HISFC.Models.Order.ExecOrder execOrder, DateTime operDate, bool isPreOut, string applyDeptType, bool getStockDept)
        {
            #region ����ִ�в���
            // ִ�в�����
            // 1��execOrder����תΪ�����������
            // 2��ȡҩƷ�������İ�ҩ��
            // 3�������ҩ֪ͨ
            // 4�������������
            // 5��Ԥ�ۿ��
            #endregion
            this.SetDB(this.itemManager);
            //����ҩ��������
            FS.HISFC.BizLogic.Pharmacy.DrugStore myDrugStore = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
            FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            myDrugStore.SetTrans(this.trans);
            consManager.SetTrans(this.trans);
            this.itemManager.SetTrans(this.trans);
            FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new ApplyOut();

            try
            {
                #region Applyoutʵ�帳ֵ

                applyOut.Item = (FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item;       //ҩƷʵ��

                #region �������/��ҩ���һ�ȡ

                //��ҩ������Ҵ�ҽ��ִ�п��һ�ȡ houwb 2012-3-6
                if (applyDeptType == "0")//�������Ϊ���߿���
                {
                    applyOut.ApplyDept = execOrder.Order.ExeDept;
                }
                else//�������Ϊ����
                {
                    //by cube 2011-03-29�����ҵ���ҩ������Ҳ��ǻ��߿��һ��������ǿ�������
                    if (execOrder.Order.ReciptDept is FS.HISFC.Models.Base.Department)
                    {
                        string specialFlag = ((FS.HISFC.Models.Base.Department)execOrder.Order.ReciptDept).SpecialFlag;
                        //������������Ϊ�����������;ʹ���
                        if (specialFlag == "1" || specialFlag == "2")
                        {
                            applyOut.ApplyDept = execOrder.Order.ExeDept;
                        }
                        else
                        {
                            applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.NurseCell;
                        }
                    }
                    else
                    {
                        applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.NurseCell;
                    }
                    //end by

                    //FS.HISFC.BizProcess.Integrate.Manager inteMgr = new Manager();
                    //ArrayList alNurseCell = inteMgr.QueryNurseStationByDept(execOrder.Order.ExeDept);
                    //if (alNurseCell == null || alNurseCell.Count == 0)
                    //{
                    //    applyOut.ApplyDept = execOrder.Order.ExeDept;
                    //}
                    //else if (alNurseCell.Count == 1)
                    //{
                    //    applyOut.ApplyDept = alNurseCell[0] as FS.FrameWork.Models.NeuObject;
                    //}
                    //else
                    //{
                    //    applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.Dept;
                    //}
                }

                ////by cube 2011-03-29�����ҵ���ҩ������Ҳ��ǻ��߿��һ��������ǿ�������
                //if (execOrder.Order.ReciptDept is FS.HISFC.Models.Base.Department)
                //{
                //    string specialFlag = ((FS.HISFC.Models.Base.Department)execOrder.Order.ReciptDept).SpecialFlag;
                //    //������������Ϊ�����������;ʹ���
                //    if (specialFlag == "1" || specialFlag == "2")
                //    {
                //        applyOut.ApplyDept = execOrder.Order.ReciptDept;
                //    }
                //    else
                //    {
                //        if (applyDeptType == "0")       //�������Ϊ���߿���
                //        {
                //            applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.Dept;
                //        }
                //        else                           //�������Ϊ����
                //        {
                //            applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.NurseCell;
                //        }
                //    }
                //}
                //else
                //{
                //    if (applyDeptType == "0")       //�������Ϊ���߿���
                //    {
                //        applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.Dept;
                //    }
                //    else                           //�������Ϊ����
                //    {
                //        applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.NurseCell;
                //    }
                //}
                //end by

                applyOut.StockDept = execOrder.Order.StockDept;

                if (getStockDept)
                {
                    string strErr = "";
                    FS.FrameWork.Models.NeuObject stockOjb = itemManager.GetStockDeptByDeptCode(applyOut.ApplyDept.ID, applyOut.Item.Type.ID, applyOut.Item.ID, execOrder.Order.Qty,this.trans, ref strErr);
                    if (stockOjb != null)
                    {
                        applyOut.StockDept.ID = stockOjb.ID;
                        applyOut.StockDept.Name = stockOjb.Name;
                    }
                }

                #endregion

                #region ����ж�

                //2011-03-14 by cube ͣ�á�ȱҩ��־���ж���������
                FS.HISFC.Models.Pharmacy.Item item = this.GetItem(execOrder.Order.Item.ID);
                if (item == null)
                {
                    this.Err = "��ȡҩƷ������Ϣʧ��" + this.Err;
                    return -1;
                }
                if (item.ValidState != EnumValidState.Valid)
                {
                    this.Err = item.Name + "�� ҩ����ͣ�� ���ܽ��з�ҩ�շѣ�";
                    return -1;
                }
                FS.HISFC.Models.Pharmacy.Storage storage = this.GetStockInfoByDrugCode(applyOut.StockDept.ID, execOrder.Order.Item.ID);
                if (storage == null || storage.Item.ID == "")
                {
                    this.Err = item.Name + "�� �ڸ�ҩ�������ڿ�� �޷����з�ҩ�շѣ�" + this.Err;
                    return -1;
                }
                if (storage.ValidState != EnumValidState.Valid)
                {
                    this.Err = item.Name + "�� ��ҩ����ͣ�� ���ܽ��з�ҩ�շѣ�";
                    return -1;
                }
                if (storage.IsLackForInpatient)
                {
                    this.Err = item.Name + "�� ��ҩ����ȱҩ ���ܽ��з�ҩ�շѣ�";
                    return -1;
                }
                decimal validStoreQty = storage.StoreQty;
                if (isPreOut)
                {
                    validStoreQty = storage.StoreQty - storage.PreOutQty;
                }
                //������۳������ʱ �����д����ж�
                //�Ƿ�����ҽ������治���ҩƷ��0������1 ��ʾ��2 ����
                int isCheckDrugStock = this.ctrlIntegrate.GetControlParam<Int32>("HNPHA2", false, 0);
                if (validStoreQty < execOrder.Order.Qty)
                {
                    if (isCheckDrugStock == 0)
                    {
                        this.Err = item.Name + "�� ��ҩ����棬�����Խ��б����շѷ�ҩ �����շѣ�";
                        return -1;
                    }
                    else if (isCheckDrugStock == 1)
                    {
                        System.Windows.Forms.NotifyIcon notify = new System.Windows.Forms.NotifyIcon();
                        //notify.Icon = SOC.Local.Order.Properties.Resources.HIS;
                        notify.Visible = true;
                        notify.ShowBalloonTip(4, "��治��", "ҩƷ��" + item.Name + "����ҩ����棬�����Խ��б����շѷ�ҩ��", System.Windows.Forms.ToolTipIcon.Warning);
                    }
                    else
                    { }
                }
                //end by

                #endregion

                #region ������Ϣ����
                //����������ˮ��{16E85CBD-F693-4871-AAF8-96A6AB6FC62D}feng.ch
                //�ӿ�ʵ���㷨
                //FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundGroup obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundGroup)) as FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundGroup;
                //if (obj != null)
                //{
                //    applyOut.CompoundGroup = obj.GetCompoundGroup(execOrder);
                //}
                ////������Ĭ��ʵ��
                //else
                //{
                //    applyOut.CompoundGroup = consManager.GetOrderGroup(execOrder.DateUse);
                //    if (applyOut.CompoundGroup == null)
                //    {
                //        applyOut.CompoundGroup = "4";
                //    }
                //    applyOut.CompoundGroup = applyOut.CompoundGroup + execOrder.DateUse.ToString("yyMMdd") + execOrder.Order.Combo.ID + "C";
                //}
                #endregion

                #region ������Ϣ����

                //by cube 2011-08-03 ����۲���
                applyOut.Item.PriceCollection.PurchasePrice = item.PriceCollection.PurchasePrice;
                applyOut.Item.PriceCollection.WholeSalePrice = item.PriceCollection.WholeSalePrice;
                //end

                applyOut.SystemType = "Z1";                                                     //�������ͣ�"Z1" 
                applyOut.Operation.ApplyOper.OperTime = operDate;                               //����ʱ�䣽����ʱ��
                //by cube 2011-03-14 ���Ʋ�ҩ������
                if (item.SysClass.ID.ToString() == "PCC")
                {
                    applyOut.Days = execOrder.Order.HerbalQty == 0 ? 1 : execOrder.Order.HerbalQty; //��ҩ����
                }
                else
                {
                    applyOut.Days = 1;
                }
                //end by
                applyOut.IsPreOut = isPreOut;                                                   //�Ƿ�Ԥ�ۿ��
                applyOut.IsCharge = execOrder.IsCharge;                                         //�Ƿ��շ�
                applyOut.PatientNO = execOrder.Order.Patient.ID;                                //����סԺ��ˮ��
                applyOut.PatientDept = execOrder.Order.Patient.PVisit.PatientLocation.Dept;     //�������ڿ���
                applyOut.DoseOnce = execOrder.Order.DoseOnce;                                   //ÿ�μ���
                applyOut.Frequency = execOrder.Order.Frequency;                                 //Ƶ��
                applyOut.Usage = execOrder.Order.Usage;                                         //�÷�
                applyOut.OrderType = execOrder.Order.OrderType;                                 //ҽ������
                applyOut.OrderNO = execOrder.Order.ID;                                          //ҽ����ˮ��
                applyOut.CombNO = execOrder.Order.Combo.ID;                                     //������
                applyOut.ExecNO = execOrder.ID;                                                 //ҽ��ִ�е���ˮ��
                applyOut.RecipeNO = execOrder.Order.ReciptNO;                                   //������
                applyOut.SequenceNO = execOrder.Order.SequenceNO;                               //��������ˮ��
                if (applyOut.Item.Quality.ID == "T")
                {
                    applyOut.SendType = 1;                                         //��������1���У�2��ʱ

                }
                else
                {
                    applyOut.SendType = execOrder.DrugFlag;
                }
                applyOut.State = "0";						                                    //��������״̬:0����,1��ҩ,2��׼
                //applyOut.User03 = execOrder.DateUse.ToString();	                                //��ҩʱ��
                applyOut.UseTime = execOrder.DateUse;
                applyOut.Memo = execOrder.Order.Memo;			                                //ҽ����ע
                applyOut.ShowState = "0";
                applyOut.Operation.ApplyQty = execOrder.Order.Qty;// / applyOut.Days;

                applyOut.RecipeInfo.Dept = execOrder.Order.ReciptDept;                          //��������
                applyOut.RecipeInfo.ID = execOrder.Order.ReciptDoctor.ID;                       //����ҽ��
                applyOut.RecipeInfo.Name = execOrder.Order.ReciptDoctor.Name;

                applyOut.IsBaby = execOrder.Order.IsBaby;

                //by cube 2011-03-14 ��λnullֵ����
                if (string.IsNullOrEmpty(applyOut.Item.PackUnit))
                {
                    applyOut.Item.PackUnit = item.PackUnit;
                }

                if (string.IsNullOrEmpty(applyOut.Item.MinUnit))
                {
                    applyOut.Item.MinUnit = item.MinUnit;
                }
                //end by

                #endregion

                #endregion

                if (applyOut.IsCharge)      //�����շѺ�Ž��д˴��ж�
                {
                    if (applyOut.RecipeNO == null || applyOut.RecipeNO == "")
                    {
                        this.Err = "ҽ�����봦����Ϊ��ֵ!";
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ҽ��ִ��ʵ��ת���ɳ�������ʵ��ʱ����" + ex.Message;
                return -1;
            }
            #region ��ȡ��ҩ��
            //���ݳ����������ݣ���ѯ������ҩ�����࣬���������������������У��������ҩ֪ͨ��¼
            //DrugBillClass billClass = myDrugStore.GetDrugBillClass(
            //    applyOut.OrderType.ID,
            //    applyOut.Usage.ID,
            //    applyOut.Item.Type.ID,
            //    applyOut.Item.Quality.ID.ToString(),
            //    applyOut.Item.DosageForm.ID
            //    );
            
            DrugBillClass billClass = GetDrugBill(applyOut);
            //û���ҵ���ҩ����Ҳ�᷵��null
            if (billClass == null || string.IsNullOrEmpty(billClass.ID))
            {
                this.Err = myDrugStore.Err;
                this.ErrCode = myDrugStore.ErrCode;
                return -1;
            }

            #endregion

            #region �����ҩ֪ͨ��¼

            DrugMessage drugMessage = new DrugMessage();
            drugMessage.ApplyDept = applyOut.ApplyDept;    //���һ��߲���
            drugMessage.DrugBillClass = billClass;        //��ҩ������
            drugMessage.SendType = applyOut.SendType;     //��������0ȫ��,1-����,2-��ʱ
            drugMessage.SendFlag = 0;                     //״̬0-֪ͨ,1-�Ѱ�
            drugMessage.StockDept = applyOut.StockDept;   //��ҩ����

            if (myDrugStore.SetDrugMessage(drugMessage) != 1)
            {
                this.Err = myDrugStore.Err;
                return -1;
            }

            #endregion

            #region ����������Ϣ Ԥ�ۿ�����

            //����������������������
            applyOut.BillClassNO = billClass.ID;
            //������������
            int parm = itemManager.InsertApplyOut(applyOut);
            if (parm == -1)
            {
                this.Err = itemManager.Err;
                if (applyOut.ExecNO != "" && applyOut.ExecNO != null)
                {
                    if (itemManager.UpdateApplyOutValidByExecNO(applyOut.ExecNO, true) >= 1)
                    {
                        this.Err = "���뵵��Ϣ�ظ����� \n" + applyOut.ExecNO + this.Err;
                        return -1;

                    }
                }

                return parm;
            }

            //{8113BE34-A5E0-4d87-B6FF-B8428BAA8711}  �˴�����Ԥ��  Ԥ�۲������ⲿIntegrate����
            ////Ԥ�ۿ�棨�Ӳ�����
            if (isPreOut)
            {
                //parm = this.UpdateStoragePreOutNum(applyOut.StockDept.ID, applyOut.Item.ID, applyOut.Operation.ApplyQty);
                //if (parm == -1) return parm;
                parm = itemManager.InsertPreoutStore(applyOut);
                if (parm == -1) return parm;
            }

            #endregion

            return 1;
        }

        #region ��ȡ��ҩ��
        private static FS.HISFC.BizProcess.Interface.Pharmacy.IDrugBillClass myIDrugBillClass = null;
        /// <summary>
        /// ���ݴ���ҽ��ѡ���ҩ���뵥
        /// </summary>
        /// <param name="applyout">��ҩ����ʵ��</param>
        /// <returns>��ҩ���뵥ʵ��</returns>        
        public DrugBillClass GetDrugBill(FS.HISFC.Models.Pharmacy.ApplyOut applyout)
        {
            if (myIDrugBillClass == null)
            {
                object oIDrugBillClass = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Pharmacy), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.IDrugBillClass));
                if (oIDrugBillClass is FS.HISFC.BizProcess.Interface.Pharmacy.IDrugBillClass)
                {
                    myIDrugBillClass = oIDrugBillClass as FS.HISFC.BizProcess.Interface.Pharmacy.IDrugBillClass;
                }
            }
            if (myIDrugBillClass == null)
            {
                return null;
            }
            return myIDrugBillClass.GetDrugBillClass(applyout);
        }

        #endregion

        /// <summary>
        /// �洢��ҩ�����е�ִ�е���ˮ�Ŷ�Ӧ���е�ִ�е���ˮ�ţ�����ҩ���洢����ִ�е���ҩ���
        /// </summary>
        //public Hashtable HsApplyExecSeq = new Hashtable();

        //{2ACF86B5-9C2E-406f-8F58-0500D02EEF94}����һ������ҽ������ķ���
        /// <summary>
        /// ������⣭����ҽ����ϵͳ�����ĺ���
        /// </summary>
        /// <param name="execOrderList">ҽ��ִ��ʵ������</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="isPreOut">�Ƿ�Ԥ����</param>
        /// <param name="applyDeptType">����������� 0 ���� 1 ����վ</param>
        /// <param name="getStockDept">�Ƿ����������һ�ȡȡҩҩ��</param>
        /// <returns>0û��ɾ�� 1�ɹ� -1ʧ��</returns>
        private int ApplyOut(ArrayList execOrderList, DateTime operDate, bool isPreOut, string applyDeptType, bool getStockDept,ref string err)
        {
            #region ����ִ�в���
            // ִ�в�����
            // 1��execOrder����תΪ�����������
            // 2��ȡҩƷ�������İ�ҩ��
            // 3�������ҩ֪ͨ
            // 4�������������
            // 5��Ԥ�ۿ��
            #endregion
            this.SetDB(this.itemManager);
            //����ҩ��������
            FS.HISFC.BizLogic.Pharmacy.DrugStore myDrugStore = new FS.HISFC.BizLogic.Pharmacy.DrugStore();
            FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.BizLogic.Pharmacy.Item itemManager = new FS.HISFC.BizLogic.Pharmacy.Item();
            myDrugStore.SetTrans(this.trans);
            consManager.SetTrans(this.trans);
            this.itemManager.SetTrans(this.trans);        
            //����������ˮ��{16E85CBD-F693-4871-AAF8-96A6AB6FC62D}feng.ch
            //�ӿ�ʵ���㷨
            bool isDefalt = true;
            FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundGroup obj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundGroup)) as FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundGroup;
            if (obj != null)
            {
                if (obj.GetCompoundGroup(execOrderList, ref err) == -1)
                {
                    this.Err = "��ȡ�����㷨����!";
                }
                isDefalt = false; 
            }
            //��Ҫ���͵��������ĵĴ���
            Hashtable hsCombo = new Hashtable();
            //�ж��Ƿ�������������Ŀ�ӿ�
            FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundJudge compoundJudgeObj = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(this.GetType(), typeof(FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundJudge)) as FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundJudge;

            if (compoundJudgeObj != null)
            {
                DateTime dtNow = this.itemManager.GetDateTimeFromSysDateTime();
                compoundJudgeObj.GetComboItems(execOrderList,dtNow,ref hsCombo);
            }

            foreach (FS.HISFC.Models.Order.ExecOrder execOrder in execOrderList)
            {
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new ApplyOut();
                try
                {

                    #region Applyoutʵ�帳ֵ

                    applyOut.Item = (FS.HISFC.Models.Pharmacy.Item)execOrder.Order.Item;       //ҩƷʵ��



                    applyOut.CombNO = execOrder.Order.Combo.ID;                                     //������

                    applyOut.UseTime = execOrder.DateUse;

                    applyOut.OrderNO = execOrder.Order.ID;                                          //ҽ����ˮ��

                    #region �������/��ҩ���һ�ȡ

                    //��ҩ������Ҵ�ҽ��ִ�п��һ�ȡ houwb 2012-3-6
                    if (applyDeptType == "0")//�������Ϊ���߿���
                    {
                        applyOut.ApplyDept = execOrder.Order.ExeDept;
                    }
                    else//�������Ϊ����
                    {
                        //by cube 2011-03-29�����ҵ���ҩ������Ҳ��ǻ��߿��һ��������ǿ�������
                        if (execOrder.Order.ReciptDept is FS.HISFC.Models.Base.Department)
                        {
                            string specialFlag = ((FS.HISFC.Models.Base.Department)execOrder.Order.ReciptDept).SpecialFlag;
                            //������������Ϊ�����������;ʹ���
                            if (specialFlag == "1" || specialFlag == "2")
                            {
                                applyOut.ApplyDept = execOrder.Order.ExeDept;
                            }
                            else
                            {
                                applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.NurseCell;
                            }
                        }
                        else
                        {
                            applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.NurseCell;
                        }
                        //FS.HISFC.BizProcess.Integrate.Manager inteMgr=new Manager();
                        //ArrayList alNurseCell=inteMgr.QueryNurseStationByDept(execOrder.Order.ExeDept);
                        //if (alNurseCell == null || alNurseCell.Count == 0)
                        //{
                        //    applyOut.ApplyDept = execOrder.Order.ExeDept;
                        //}
                        //else if (alNurseCell.Count == 1)
                        //{
                        //    applyOut.ApplyDept = alNurseCell[0] as FS.FrameWork.Models.NeuObject;
                        //}
                        //else
                        //{
                        //    applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.Dept;
                        //}
                    }


                    ////by cube 2011-03-29�����ҵ���ҩ������Ҳ��ǻ��߿��һ��������ǿ�������
                    //if (execOrder.Order.ReciptDept is FS.HISFC.Models.Base.Department)
                    //{
                    //    string specialFlag = ((FS.HISFC.Models.Base.Department)execOrder.Order.ReciptDept).SpecialFlag;
                    //    //������������Ϊ�����������;ʹ���
                    //    if (specialFlag == "1" || specialFlag == "2")
                    //    {
                    //        applyOut.ApplyDept = execOrder.Order.ReciptDept;
                    //    }
                    //}
                    //else
                    //{
                    //    if (applyDeptType == "0")       //�������Ϊ���߿���
                    //    {
                    //        applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.Dept;
                    //    }
                    //    else                           //�������Ϊ����
                    //    {
                    //        applyOut.ApplyDept = execOrder.Order.Patient.PVisit.PatientLocation.NurseCell;
                    //    }
                    //}
                    //end by

                    applyOut.StockDept = execOrder.Order.StockDept;

                    if (getStockDept)
                    {
                        string strErr = "";
                        FS.FrameWork.Models.NeuObject stockOjb = itemManager.GetStockDeptByDeptCode(applyOut.ApplyDept.ID, applyOut.Item.Type.ID, applyOut.Item.ID, execOrder.Order.Qty, this.trans, ref strErr);
                        if (stockOjb != null)
                        {
                            applyOut.StockDept.ID = stockOjb.ID;
                            applyOut.StockDept.Name = stockOjb.Name;
                        }
                    }

                    #endregion

                    //�������Ľӿڴ������ڷ��͵��������ĵ�ҩƷ�жϵ�ͣȱҩ����Ϊ��������
                    if (compoundJudgeObj != null)
                    {
                        if (hsCombo != null && hsCombo.Count > 0)
                        {
                            if (hsCombo.Contains(applyOut.CombNO))
                            {
                                string errInfo = string.Empty;
                                int param = compoundJudgeObj.SetCompoundApply(applyOut, ref errInfo);
                                if (param == -1)
                                {
                                    this.Err = errInfo;
                                    return param;
                                }
                            }
                        }
                    }

                    #region ����ж�

                    //2011-03-14 by cube ͣ�á�ȱҩ��־���ж���������
                    FS.HISFC.Models.Pharmacy.Item item = this.GetItem(execOrder.Order.Item.ID);
                    if (item == null)
                    {
                        this.Err = "��ȡҩƷ������Ϣʧ��" + this.Err;
                        return -1;
                    }
                    if (item.ValidState != EnumValidState.Valid)
                    {
                        this.Err = item.Name + "�� ҩ����ͣ�� ���ܽ��з�ҩ�շѣ�";
                        return -1;
                    }
                    FS.HISFC.Models.Pharmacy.Storage storage = this.GetStockInfoByDrugCode(applyOut.StockDept.ID, execOrder.Order.Item.ID);
                    if (storage == null || storage.Item.ID == "")
                    {
                        this.Err = item.Name + "�� �ڸ�ҩ�������ڿ�� �޷����з�ҩ�շѣ�" + this.Err;
                        return -1;
                    }
                    if (storage.ValidState != EnumValidState.Valid)
                    {
                        this.Err = item.Name + "�� ��ҩ����ͣ�� ���ܽ��з�ҩ�շѣ�";
                        return -1;
                    }
                    if (storage.IsLackForInpatient)
                    {
                        this.Err = item.Name + "�� ��ҩ����ȱҩ ���ܽ��з�ҩ�շѣ�";
                        return -1;
                    }
                    decimal validStoreQty = storage.StoreQty;
                    if (isPreOut)
                    {
                        validStoreQty = storage.StoreQty - storage.PreOutQty;
                    }
                    //������۳������ʱ �����д����ж�
                    //�Ƿ�����ҽ������治���ҩƷ��0������1 ��ʾ��2 ����
                    int isCheckDrugStock = this.ctrlIntegrate.GetControlParam<Int32>("HNPHA2", false, 0);
                    if (validStoreQty < execOrder.Order.Qty)
                    {
                        if (isCheckDrugStock == 0)
                        {
                            this.Err = item.Name + "�� ��ҩ����棬�����Խ��б����շѷ�ҩ �����շѣ�";
                            return -1;
                        }
                        else if (isCheckDrugStock == 1)
                        {
                            System.Windows.Forms.NotifyIcon notify = new System.Windows.Forms.NotifyIcon();
                            //notify.Icon = SOC.Local.Order.Properties.Resources.HIS;
                            notify.Visible = true;
                            notify.ShowBalloonTip(4, "��治��", "ҩƷ��" + item.Name + "����ҩ����棬�����Խ��б����շѷ�ҩ��", System.Windows.Forms.ToolTipIcon.Warning);
                        }
                        else
                        { }
                    }
                    //end by


                    #endregion

                    //#region ������Ϣ����
                    ////{16E85CBD-F693-4871-AAF8-96A6AB6FC62D}
                    //if (isDefalt)
                    //{
                    //    applyOut.CompoundGroup = consManager.GetOrderGroup(execOrder.DateUse);
                    //    if (applyOut.CompoundGroup == null)
                    //    {
                    //        applyOut.CompoundGroup = "4";
                    //    }
                    //    applyOut.CompoundGroup = applyOut.CompoundGroup + execOrder.DateUse.ToString("yyMMdd") + execOrder.Order.Combo.ID + "C";
                    //}
                    //else
                    //{
                    //    applyOut.CompoundGroup = applyOut.Item.User02;
                    //}
                    //#endregion

                    #region ������Ϣ����

                    //by cube 2011-08-03 ����۲���
                    applyOut.Item.PriceCollection.PurchasePrice = item.PriceCollection.PurchasePrice;
                    applyOut.Item.PriceCollection.WholeSalePrice = item.PriceCollection.WholeSalePrice;
                    //end

                    applyOut.SystemType = "Z1";                                                     //�������ͣ�"Z1" 
                    applyOut.Operation.ApplyOper.OperTime = operDate;                               //����ʱ�䣽����ʱ��

                    //by cube 2011-03-14 ���Ʋ�ҩ������
                    if (item.SysClass.ID.ToString() == "PCC")
                    {
                        applyOut.Days = execOrder.Order.HerbalQty == 0 ? 1 : execOrder.Order.HerbalQty; //��ҩ����
                    }
                    else
                    {
                        applyOut.Days = 1;
                    }
                    //end by

                    applyOut.IsPreOut = isPreOut;                                                   //�Ƿ�Ԥ�ۿ��
                    applyOut.IsCharge = execOrder.IsCharge;                                         //�Ƿ��շ�
                    applyOut.PatientNO = execOrder.Order.Patient.ID;                                //����סԺ��ˮ��
                    applyOut.PatientDept = execOrder.Order.Patient.PVisit.PatientLocation.Dept;     //�������ڿ���
                    applyOut.DoseOnce = execOrder.Order.DoseOnce;                                   //ÿ�μ���
                    applyOut.Frequency = execOrder.Order.Frequency;                                 //Ƶ��
                    applyOut.Usage = execOrder.Order.Usage;                                         //�÷�
                    applyOut.OrderType = execOrder.Order.OrderType;                                 //ҽ������
                    applyOut.ExecNO = execOrder.ID;                                                 //ҽ��ִ�е���ˮ��
                    applyOut.RecipeNO = execOrder.Order.ReciptNO;                                   //������
                    applyOut.SequenceNO = execOrder.Order.SequenceNO;                               //��������ˮ��
                    if (applyOut.Item.Quality.ID == "T")
                    {
                        applyOut.SendType = 1;
                    }
                    else
                    {
                        applyOut.SendType = execOrder.DrugFlag;                                         //��������1���У�2��ʱ
                    }
                    //�����������ĵ�״̬�ڽӿ��д���
                    if(string.IsNullOrEmpty(applyOut.State))
                    {
                        applyOut.State = "0";	
                    }
                    //��������״̬:0����,1��ҩ,2��׼
                    //applyOut.User03 = execOrder.DateUse.ToString();	                                //��ҩʱ��
                    applyOut.Memo = execOrder.Order.Memo;			                                //ҽ����ע
                    applyOut.ShowState = "0";
                    applyOut.Operation.ApplyQty = execOrder.Order.Qty ;/// applyOut.Days;

                    applyOut.RecipeInfo.Dept = execOrder.Order.ReciptDept;                          //��������
                    applyOut.RecipeInfo.ID = execOrder.Order.ReciptDoctor.ID;                       //����ҽ��
                    applyOut.RecipeInfo.Name = execOrder.Order.ReciptDoctor.Name;

                    applyOut.IsBaby = execOrder.Order.IsBaby;

                    //by cube 2011-03-14 ��λnullֵ����
                    if (string.IsNullOrEmpty(applyOut.Item.PackUnit))
                    {
                        applyOut.Item.PackUnit = item.PackUnit;
                    }

                    if (string.IsNullOrEmpty(applyOut.Item.MinUnit))
                    {
                        applyOut.Item.MinUnit = item.MinUnit;
                    }
                    //end by

                    #endregion

                    #endregion

                    if (applyOut.IsCharge)      //�����շѺ�Ž��д˴��ж�
                    {
                        if (applyOut.RecipeNO == null || applyOut.RecipeNO == "")
                        {
                            this.Err = "ҽ�����봦����Ϊ��ֵ!";
                            err = applyOut.Item.ID;
                            return -1;
                        }
                    }
                }
                catch (Exception ex)
                {
                    this.Err = "��ҽ��ִ��ʵ��ת���ɳ�������ʵ��ʱ����" + ex.Message;
                    err = applyOut.Item.ID;
                    return -1;
                }


                #region ��ȡ��ҩ��
                //���ݳ����������ݣ���ѯ������ҩ�����࣬���������������������У��������ҩ֪ͨ��¼
                //DrugBillClass billClass = myDrugStore.GetDrugBillClass(
                //    applyOut.OrderType.ID,
                //    applyOut.Usage.ID,
                //    applyOut.Item.Type.ID,
                //    applyOut.Item.Quality.ID.ToString(),
                //    applyOut.Item.DosageForm.ID
                //    );
                DrugBillClass billClass = GetDrugBill(applyOut);

                //û���ҵ���ҩ����Ҳ�᷵��null
                if (billClass == null || string.IsNullOrEmpty(billClass.ID))
                {
                    this.Err = myDrugStore.Err;
                    this.ErrCode = myDrugStore.ErrCode;
                    return -1;
                }
                #endregion

                #region �����ҩ֪ͨ��¼

                DrugMessage drugMessage = new DrugMessage();
                drugMessage.ApplyDept = applyOut.ApplyDept;    //���һ��߲���
                drugMessage.DrugBillClass = billClass;        //��ҩ������
                drugMessage.SendType = applyOut.SendType;     //��������0ȫ��,1-����,2-��ʱ
                drugMessage.SendFlag = 0;                     //״̬0-֪ͨ,1-�Ѱ�
                drugMessage.StockDept = applyOut.StockDept;   //��ҩ����

                if (myDrugStore.SetDrugMessage(drugMessage) != 1)
                {
                    this.Err = myDrugStore.Err;
                    err = applyOut.Item.ID;
                    return -1;
                }

                #endregion

                #region ����������Ϣ Ԥ�ۿ�����

                //����������������������
                applyOut.BillClassNO = billClass.ID;
                //������������
                int parm = itemManager.InsertApplyOut(applyOut);
                if (parm == -1)
                {
                    this.Err = itemManager.Err;
                    if (applyOut.ExecNO != "" && applyOut.ExecNO != null)
                    {
                        if (itemManager.UpdateApplyOutValidByExecNO(applyOut.ExecNO, true) >= 1)
                        {
                            this.Err = "���뵵��Ϣ�ظ����� \n" + applyOut.ExecNO + this.Err;
                            return -1;

                        }
                    }

                    return parm;
                }

                //���Ӳ����Ӧ������ִ�е���ˮ�ż�¼
                //if (HsApplyExecSeq.Contains(applyOut.ExecNO))
                //{
                //    applyOut.ExecSeqAll = applyOut.ExecSeqAll + HsApplyExecSeq[applyOut.ExecNO].ToString();

                //    parm = itemManager.UpdateApplyOutForOrderSeq(applyOut.ID, applyOut.ExecSeqAll);
                //    if (parm == -1)
                //    {
                //        this.Err = itemManager.Err;
                //        if (applyOut.ExecNO != "" && applyOut.ExecNO != null)
                //        {
                //            if (itemManager.UpdateApplyOutValidByExecNO(applyOut.ExecNO, true) >= 1)
                //            {
                //                this.Err = "���뵵��Ϣ�ظ����� \n" + applyOut.ExecNO + this.Err;
                //                return -1;

                //            }
                //        }

                //        return parm;
                //    }
                //}

                //{8113BE34-A5E0-4d87-B6FF-B8428BAA8711}  �˴�����Ԥ��  Ԥ�۲������ⲿIntegrate����
                ////Ԥ�ۿ�棨�Ӳ�����
                if (isPreOut)
                {
                    //parm = this.UpdateStoragePreOutNum(applyOut.StockDept.ID, applyOut.Item.ID, applyOut.Operation.ApplyQty);
                    //if (parm == -1) return parm;

                    parm = itemManager.InsertPreoutStore(applyOut);
                    if (parm == -1) return parm;
                }

                #endregion
            }
            return 1;
        }

        ///// <summary>
        ///// ���³��������Ӧ��ִ�е���ˮ��
        ///// </summary>
        ///// <param name="applyOut"></param>
        ///// <returns></returns>
        //public int UpdateApplyOutForOrderSeq(string applyNum, string execSeqAll)
        //{
        //    this.SetDB(itemManager);
        //    return itemManager.UpdateApplyOutForOrderSeq(applyNum, execSeqAll);
        //}

        ///// <summary>
        ///// ����ĳһ���������¼��ˮ�Ż�ȡ����ִ�е���ˮ��
        ///// </summary>
        ///// <param name="execSeq"></param>
        ///// <param name="applyNum"></param>
        ///// <param name="execSeqAll"></param>
        ///// <returns></returns>
        //public int GetExecSeqAllByExecSeq(string execSeq, ref string applyNum, ref string execSeqAll)
        //{
        //    this.SetDB(itemManager);
        //    return itemManager.GetExecSeqAllByExecSeq(execSeq, ref applyNum, ref execSeqAll);
        //}

        #region ��������

        /// <summary>
        /// �����շѵ��õĳ��⺯��
        /// </summary>
        /// <param name="patient">������Ϣʵ��</param>
        /// <param name="feeAl">������Ϣ����</param>
        /// <param name="feeWindow">�շѴ���</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="isModify">�Ƿ������˸�ҩ</param>
        /// <param name="drugSendInfo">����������Ϣ ��ҩҩ��+��ҩ����</param>
        /// <returns>1 �ɹ� ��1 ʧ��</returns>
        public int ApplyOut(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, string feeWindow, DateTime operDate, bool isModify,out string drugSendInfo)
        {
            FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            if (this.trans != null) 
            {
                constantManager.SetTrans(this.trans);
                ctrlParamIntegrate.SetTrans(this.trans);
            }

            #region ��������Ϣ��ֱ�ӿۿ�����

            ArrayList alSpeDept = constantManager.GetList("PrintLabel");
            if (alSpeDept == null)
            {
                this.Err = "��ȡ�������������" + constantManager.Err;
            }

            #endregion

            //���ڲ�ͬҩ������ʹ�ò�ͬ�ĵ�����ʽ ���Ե�����ʽ(����/ƽ��)��ҵ����ȡ

            this.SetDB(itemManager);

            //{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
            //Pharmacy.IsClinicPreOut = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Pre_Out, false, true);//�Ƿ�Ԥ�ۿ��
            //if (Pharmacy.IsClinicPreOut)
            //{
            Pharmacy.IsClinicPreOut = ctrlParamIntegrate.GetControlParam<bool>("P01015");//�շ�ʱ�Ƿ�Ԥ�� true�շ�ʱԤ�� false����ҽ��Ԥ��
            //}
            //�ж��Ƿ���������ע��������̡���������ע���������ʱ������Ժע����������Ĳ����д���
            bool useInjectFlow = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.SysConst.Use_Inject_Flow, false, false);
            if (useInjectFlow)
            {   
                ArrayList alFilterFee = new ArrayList();
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeInfo in alFilterFee)
                {
                    if (feeInfo.InjectCount <= 0)
                    {
                        alFilterFee.Add(feeInfo);
                    }
                }

                return itemManager.ApplyOut(patient, alFilterFee, operDate, Pharmacy.IsClinicPreOut, isModify, alSpeDept, out drugSendInfo);
            }
            else
            {
                return itemManager.ApplyOut(patient, feeAl, operDate, Pharmacy.IsClinicPreOut, isModify, alSpeDept, out drugSendInfo);
            }
        }

        /// <summary>
        /// �����շѵ��õĳ��⺯��
        /// ��Fee.OutPatient.FeeItemList ת��Ϊ����������� ����������ʽ����ƽ������ �����շѵ���
        /// </summary>
        /// <param name="patient">������Ϣʵ��</param>
        /// <param name="feeAl">������Ϣ����</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="drugSendInfo">����������Ϣ ��ҩҩ��+��ҩ����</param>
        /// <returns>1 �ɹ� ��1 ʧ��</returns>
        public int ApplyOut(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, DateTime operDate, out string drugSendInfo)
        {
            return this.ApplyOut(patient, feeAl, "", operDate, false, out drugSendInfo);
        }

        /// <summary>
        /// ���ݾɷ�Ʊ�Ÿ����·�Ʊ��
        /// </summary>
        /// <param name="orgInvoiceNO">�ɷ�Ʊ��</param>
        /// <param name="newInvoiceNO">�·�Ʊ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int UpdateDrugRecipeInvoiceN0(string orgInvoiceNO, string newInvoiceNO)
        {
            this.SetDB(drugStoreManager);

            return drugStoreManager.UpdateDrugRecipeInvoiceN0(orgInvoiceNO, newInvoiceNO);
        }

        #endregion

        #region ��������

        #region ���뺯������ ���ݴ�������������

        /// <summary>
        /// ȡ�����﷢ҩ����
        /// ���ݴ�����ˮ�ţ��������﷢ҩ���� ������Ԥ�ۿ��
        /// </summary>
        /// <param name="recipeNo">������ˮ��</param>
        /// <returns>��ȷ1,û�ҵ�����0,����1</returns>
        public int CancelApplyOutClinic(string recipeNo)
        {
            return this.CancelApplyOutClinic(recipeNo, -1);
        }



        /// <summary>
        /// ȡ�����﷢ҩ����
        /// ���ݴ�����ˮ�ţ��������﷢ҩ����
        /// </summary>
        /// <param name="recipeNo">������</param>
        /// <param name="sequenceNo">��������Ŀ��ˮ��</param>
        /// <returns>��ȷ1,û�ҵ�����0,����1</returns>
        public int CancelApplyOutClinic(string recipeNo, int sequenceNo)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.SetDB(itemManager);

            string controlValue = ctrlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Pre_Out, false, "#");
            if (controlValue == "0" || controlValue == "1")
            {
                Pharmacy.IsClinicPreOut = true;
            }
            else
            {
                Pharmacy.IsClinicPreOut = false;
            }

            return itemManager.CancelApplyOutClinic(recipeNo, sequenceNo, Pharmacy.IsClinicPreOut);
        }


        /// <summary>
        /// ȡ�����﷢ҩ���� --���������˷�
        /// ���ݴ�����ˮ�ţ��������﷢ҩ����
        /// </summary>
        /// <param name="recipeNo">������</param>
        /// <param name="sequenceNo">��������Ŀ��ˮ��</param>{A56F1E9D-9E9D-48bb-A3EA-8F17A6738619}
        /// <returns>��ȷ1,û�ҵ�����0,����1</returns>
        public int CancelApplyOutClinicMZTF(string recipeNo, int sequenceNo)
        {
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            this.SetDB(itemManager);

            string controlValue = ctrlParamIntegrate.GetControlParam<string>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Pre_Out, false, "#");
            if (controlValue == "0" || controlValue == "1")
            {
                Pharmacy.IsClinicPreOut = true;
            }
            else
            {
                Pharmacy.IsClinicPreOut = false;
            }

            return itemManager.CancelApplyOutClinicMZTF(recipeNo, sequenceNo, Pharmacy.IsClinicPreOut);
        }


        /// <summary>
        /// ȡ����������
        /// ���ݴ�����ˮ�źʹ�������ţ����ϳ�������
        /// </summary>
        /// <param name="recipeNo">������ˮ��</param>
        /// <param name="sequenceNo">���������</param>
        /// <returns>��ȷ1,û�ҵ�����0,����1</returns>
        public int CancelApplyOut(string recipeNo, int sequenceNo)
        {
            this.SetDB(itemManager);

            //�Ƿ�ʵ��Ԥ�ۿ�����
            Pharmacy.IsInPatientPreOut = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Pre_Out, false, true);

            return itemManager.CancelApplyOut(recipeNo, sequenceNo, Pharmacy.IsInPatientPreOut);
        }

        /// <summary>
        /// ����ȡ���������루ȡ�����������̣�
        /// ���ݴ�����ˮ�źʹ�������ţ��������ϳ�������
        /// </summary>
        /// <param name="recipeNo">������ˮ��</param>
        /// <param name="sequenceNo">���������</param>
        /// <returns>��ȷ1,û�ҵ�����0,����1</returns>
        public int UndoCancelApplyOut(string recipeNo, int sequenceNo)
        {
            this.SetDB(itemManager);

            //Pharmacy.IsApproveCharge = Pharmacy.QueryControlForBool("501003", false);
            //�����ҩʱ�Ʒ� �򲻽���Ԥ�ۿ����� ���� Ԥ�ۿ��   SysConst.Use_Drug_ApartFee "100003"
            Pharmacy.IsApproveCharge = !this.ctrlIntegrate.GetControlParam<bool>(SysConst.Use_Drug_ApartFee, false, true);

            //�Ƿ�ʵ��Ԥ�ۿ�����
            Pharmacy.IsInPatientPreOut = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Pre_Out, false, true);

            return itemManager.UndoCancelApplyOut(recipeNo, sequenceNo, Pharmacy.IsInPatientPreOut);
        }

        #endregion

        #region ��������  �����˷�ʵ���������� ���ڲ��������·�������

        /// <summary>
        /// �����˷�����  ����ǲ����� ������ԭ���� ������������
        /// </summary>
        /// <param name="feeItemList">�˷���Ϣʵ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public int CancelApplyOut(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItemList)
        {           
            //FS.HISFC.Models.Fee.Inpatient.FeeItemList originalFee = feeInpatientManager.GetItemListByRecipeNO(feeItemList.RecipeNO,feeItemList.SequenceNO,true);
            FS.HISFC.Models.Fee.Inpatient.FeeItemList originalFee = feeInpatientManager.GetItemListByRecipeNO(feeItemList.RecipeNO, feeItemList.SequenceNO, EnumItemType.Drug);

            if (this.CancelApplyOut(originalFee.RecipeNO, originalFee.SequenceNO) == -1)
            {
                this.Err = "���Ϸ�ҩ������Ϣʧ��";
                return -1;
            }

            if (originalFee.Item.Qty > feeItemList.Item.Qty)      //������ ���·�������
            {
                originalFee.Item.Qty = originalFee.NoBackQty - feeItemList.Item.Qty;
                originalFee.FeeOper = feeItemList.FeeOper;
               
                FS.HISFC.Models.RADT.PatientInfo patient = radtIntegrate.QueryPatientInfoByInpatientNO(feeItemList.Patient.ID);

                if (this.ApplyOut(patient, originalFee, feeInpatientManager.GetDateTimeFromSysDateTime(), true) == -1)
                {
                    return -1;
                }
            }

            return 1;
        }

        #endregion

        #region ���뺯������ ����ִ�е���ˮ����������

        /// <summary>
        /// ȡ����������
        /// ���ݴ�����ˮ�źʹ�������ţ����ϳ�������
        /// </summary>
        /// <param name="orderExecNO">ִ�е���ˮ��</param>
        /// <returns>��ȷ1,û�ҵ�����0,����1</returns>
        public int CancelApplyOut(string orderExecNO)
        {
            this.SetDB(itemManager);

            //Pharmacy.IsApproveCharge = Pharmacy.QueryControlForBool("501003", false);
            //�����ҩʱ�Ʒ� �򲻽���Ԥ�ۿ����� ���� Ԥ�ۿ��   SysConst.Use_Drug_ApartFee "100003"
            Pharmacy.IsApproveCharge = !this.ctrlIntegrate.GetControlParam<bool>(SysConst.Use_Drug_ApartFee, false, true);

            //�Ƿ�ʵ��Ԥ�ۿ�����
            Pharmacy.IsInPatientPreOut = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Pre_Out, false, true);

            return itemManager.CancelApplyOut(orderExecNO, Pharmacy.IsInPatientPreOut);
        }

        /// <summary>
        /// ����ȡ���������루ȡ�����������̣�
        /// ����ִ�е���ˮ�Ÿ��³�������
        /// </summary>
        /// <param name="orderExecNO">ִ�е���ˮ��</param>
        /// <returns>��ȷ1,û�ҵ�����0,����1</returns>
        public int UndoCancelApplyOut(string orderExecNO)
        {
            this.SetDB(itemManager);

            //Pharmacy.IsApproveCharge = Pharmacy.QueryControlForBool("501003", false);
            //�����ҩʱ�Ʒ� �򲻽���Ԥ�ۿ����� ���� Ԥ�ۿ��   SysConst.Use_Drug_ApartFee "100003"
            Pharmacy.IsApproveCharge = !this.ctrlIntegrate.GetControlParam<bool>(SysConst.Use_Drug_ApartFee, false, true);

            //�Ƿ�ʵ��Ԥ�ۿ�����
            Pharmacy.IsInPatientPreOut = this.ctrlIntegrate.GetControlParam<bool>(PharmacyConstant.InDrug_Pre_Out, false, true);

            return itemManager.UndoCancelApplyOut(orderExecNO, Pharmacy.IsInPatientPreOut);
        }

        #endregion

        #region ������Ϣ���� ����סԺ��ˮ�Ž�������

        /// <summary>
        /// ҩƷ��ҩ������Ϣ����
        /// 
        /// {CC0E14C4-A66B-42db-A6D7-82DF31870DDC}  ���ݻ�����Ϣ����ҩƷ����
        /// </summary>
        /// <param name="patientID">סԺ��ˮ��</param>
        /// <param name="drugDeptCode">���ҩ��</param>
        /// <param name="applyDept">�������</param>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <returns>�ɹ�����1  ʧ�ܷ���-1</returns>
        public int CancelApplyOut(string patientID, string drugDeptCode, string applyDept, DateTime beginTime, DateTime endTime)
        {
            ArrayList alApplyList = this.itemManager.GetPatientApply(patientID, drugDeptCode, applyDept, beginTime, endTime, "0");

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in alApplyList)
            {
                //��Ч���ݲŽ��к�������
                if (info.ValidState == EnumValidState.Valid)
                {
                    if (this.CancelApplyOut(info.ExecNO) == -1)
                    {
                        return -1;
                    }
                }
            }

            return 1;
        }

        #endregion

        #endregion

        /// <summary>
        /// ȡ����������
        /// ���ݳ���������ˮ�ţ����ϳ�������
        /// </summary>
        /// <param name="ID">����������ˮ��</param>
        /// <param name="validState">��Ч״̬</param>
        /// <returns>��ȷ1,û�ҵ�����0,����1</returns>
        public int UpdateApplyOutValidState(string ID, string validState)
        {
            this.SetDB(itemManager);

            return itemManager.UpdateApplyOutValidState(ID, validState);
        }

        /// <summary>
        /// ���°�ҩ���봦����
        /// </summary>
        /// <param name="oldRecipeNo">�ɴ�����</param>
        /// <param name="oldSeqNo">�ɴ�������Ŀ���</param>
        /// <param name="newRecipeNo">�´�����</param>
        /// <param name="newSeqNo">�´�������Ŀ���</param>
        /// <returns>�ɹ�����1 ������-1</returns>
        public int UpdateApplyOutRecipe(string oldRecipeNo, int oldSeqNo, string newRecipeNo, int newSeqNo)
        {
            this.SetDB(itemManager);

            return itemManager.UpdateApplyOutRecipe(oldRecipeNo, oldSeqNo, newRecipeNo, newSeqNo);
        }

        #endregion

        #region ֱ���˿����

        /// <summary>
        /// �����˿�
        /// ����˿������У�ָ��ȷ�������Σ��򽫴����μ�¼�˵���
        /// ����������������Ӧ�ĳ����¼�а�����С���˵�ԭ�����˿⴦��
        /// </summary>
        /// <param name="feeInfo">�շѷ���ʵ��</param>
        /// <param name="operCode">����Ա</param>
        /// <param name="operDate">����ʱ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1 �޼�¼����0</returns>
        public int OutputReturn(FS.HISFC.Models.Fee.Outpatient.FeeItemList feeInfo, string operCode, DateTime operDate)
        {
            this.SetDB(itemManager);

            return itemManager.OutputReturn(feeInfo, operCode, operDate);
        }

        /// <summary>
        /// סԺ�˿�
        /// ����˿������У�ָ��ȷ�������Σ��򽫴����μ�¼�˵���
        /// ����������������Ӧ�ĳ����¼�а�����С���˵�ԭ�����˿⴦��
        /// </summary>
        /// <param name="feeInfo">�շѷ���ʵ��</param>
        /// <param name="operCode">����Ա</param>
        /// <param name="operDate">����ʱ��</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1 �޼�¼����0</returns>
        public int OutputReturn(FS.HISFC.Models.Fee.Inpatient.FeeItemList feeInfo, string operCode, DateTime operDate)
        {
            this.SetDB(itemManager);

            return itemManager.OutputReturn(feeInfo, operCode, operDate);
        }        
        #endregion

        #region �����Ϣ

        /// <summary>
        /// ȡĳһҩ����ĳһҩƷ�ڿ����ܱ��е�����
        /// </summary>
        /// <param name="drugCode">ҩƷ����</param>
        /// <param name="deptCode">�ⷿ����</param>
        /// <param name="storageNum">��������������ز�����</param>
        /// <returns>1�ɹ���-1ʧ��</returns>
        public int GetStorageNum(string deptCode, string drugCode, out decimal storageNum)
        {
            this.SetDB(itemManager);

            return itemManager.GetStorageNum(deptCode, drugCode, out storageNum);
        }

        /// <summary>
        /// ȡĳһҩ����ĳһҩƷ�ڿ����ܱ��е�����
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="drugCode"></param>
        /// <param name="storageNum"></param>
        /// <param name="preNum"></param>
        /// <returns></returns>
        public int GetStorageNum(string deptCode, string drugCode,string applyNum ,out decimal storageNum, out decimal preNum)
        {
            this.SetDB(itemManager);

            return itemManager.GetStorageNum(deptCode, drugCode,applyNum, out storageNum, out preNum);
        }

        /// <summary>
        /// ȡĳһҩ����ĳһҩƷ�ڿ����ܱ��е�����
        /// </summary>
        /// <param name="deptCode">ҩ������</param>
        /// <param name="drugQuality">ҩƷ���ʱ���</param>
        /// <returns>�ɹ����ؿ���¼���飬������null</returns>
        public ArrayList QueryStockinfoList(string deptCode, string drugQuality)
        {
            this.SetDB(itemManager);

            return itemManager.QueryStockinfoList(deptCode, drugQuality);
        }

        /// <summary>
        /// ȡĳһҩ�����ڿ����ܱ��еļ�¼
        /// </summary>
        /// <param name="deptCode">�ⷿ����</param>
        /// <returns>����¼���飬������null</returns>
        public ArrayList QueryStockinfoList(string deptCode)
        {
            this.SetDB(itemManager);

            return itemManager.QueryStockinfoList(deptCode);
        }

        /// <summary>
        /// ���¿��
        /// </summary>
        /// <param name="storageBase">������</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int UpdateStorage(StorageBase storageBase)
        {
            this.SetDB(itemManager);

            return itemManager.UpdateStorageNum(storageBase);
        }

        /// <summary>
        /// ͨ�����ұ����ҩƷ�����ÿ����Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <returns>�ɹ� �����Ϣʵ�� ʧ�� null</returns>
        public FS.HISFC.Models.Pharmacy.Storage GetStockInfoByDrugCode(string deptCode, string drugCode) 
        {
            this.SetDB(itemManager);

            return itemManager.GetStockInfoByDrugCode(deptCode, drugCode);
        }

        /// <summary>
        /// ����Ԥ�ۿ�桢Ԥ�����������������ӣ������Ǽ��٣�
        /// 
        /// {C37BEC96-D671-46d1-BCDD-C634423755A4} ���Ĳ�������
        /// </summary>
        /// <param name="drugDeptCode">������</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <param name="alterStoreNum">���仯��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int UpdateStoragePreOutNum(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, decimal alterStoreNum, decimal days)
        {
            this.SetDB(itemManager);

            if (alterStoreNum > 0)
            {
                return this.InsertPreoutStore(applyOut);
            }
            else
            {
                return this.DeletePreoutStore(applyOut);
            }
        }

        #endregion

        #region ��ҩ����/�༶��λ����

        /// <summary>
        /// ��ȡҩƷ��ҩ����
        /// </summary>
        /// <param name="drugCode">ҩƷ����</param>
        /// <param name="doseCode">���ͱ���</param>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ�������ҩ���� 0 ���ɲ�� 1 �ɲ�ֲ�ȡ�� 2 �ɲ����ȡ����ʧ�ܷ���NULL</returns>
        public string GetDrugProperty(string drugCode, string doseCode, string deptCode)
        {
            this.SetDB(itemManager);

            return itemManager.GetDrugProperty(drugCode, doseCode, deptCode);
        }

        /// <summary>
        /// ���ݴ������� ����ȡ��������
        /// </summary>
        /// <param name="drugCode">ҩƷ����</param>
        /// <param name="doseCode">���ͱ���</param>
        /// <param name="doseOnce">ҽ��ÿ����</param>
        /// <param name="baseDose">��������</param>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ�����ȡ��������</returns>
        public decimal ComputeAmount(string drugCode, string doseCode, decimal doseOnce, decimal baseDose, string deptCode)
        {
            string unitSate = this.GetDrugProperty(drugCode, doseCode, deptCode);
            decimal amount = 0;
            if (baseDose == 0) return amount;
            switch (unitSate)
            {
                case "0"://�����ԣ�����ȡ��
                    //amount = (decimal)System.Math.Ceiling((double)doseOnce / (double)baseDose);
                    amount = (decimal)System.Math.Ceiling((double)((decimal)doseOnce / (decimal)baseDose));
                    break;
                case "1"://���ԣ���ҩʱ��ȡ��
                    amount = System.Convert.ToDecimal(doseOnce) / baseDose;
                    break;
                case "2"://���ԣ���ҩʱ��ȡ�� 
                    amount = System.Convert.ToDecimal(doseOnce) / baseDose;
                    break;
                default://
                    amount = System.Convert.ToDecimal(doseOnce) / baseDose;
                    break;
            }
            return amount;
        }

        /// <summary>
        /// ����ָ����� ��ȡȡ��������ⵥλ��ת��ȡ������
        /// ����С��λ������ʾ
        /// </summary>
        /// <param name="unitType">���</param>
        /// <param name="item">ҩƷʵ��</param>
        /// <param name="originalNum">ԭʼ�������� ����С��λ��ʾ</param>
        /// <param name="splitNum">ת����ȡ������ ����С��λ��ʾ</param>
        /// <param name="splitUnit">������Ӧ�����ⵥλ</param>
        /// /// <param name="standNum">ÿ�����ⵥλ��Ӧ��С��λ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int QuerySpeUnit(string unitType, FS.HISFC.Models.Pharmacy.Item item, decimal originalNum, out decimal splitNum, out string splitUnit, out decimal standNum)
        {
            this.SetDB(itemManager);

            return itemManager.QuerySpeUnit(unitType, item, originalNum, out splitNum, out splitUnit, out standNum);
        }
       
        /// <summary>
        /// ��������ȡ������
        /// </summary>
        /// <param name="item">ҩƷʵ��</param>
        /// <param name="originalNum">ԭʼ�������� ����С��λ����</param>
        /// <param name="splitNum">ת����ȡ������ ����С��λ��ʾ</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int QuerySpeUnitForClinic(FS.HISFC.Models.Pharmacy.Item item, decimal originalNum, out decimal splitNum)
        {
            string unit = "";
            decimal standNum;

            return this.QuerySpeUnit("Clinic", item, originalNum, out splitNum, out unit, out standNum);
        }

        #endregion

        #region �����

        /// <summary>
        /// ��ȡ��������ڿ�澯���ߵ�ҩƷ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ�����Storageʵ������ ʧ�ܷ���null</returns>
        public ArrayList QueryWarnStockDrug(string deptCode)
        {
            this.SetDB(itemManager);

            return itemManager.QueryWarnDrugStockInfoList(deptCode);
        }

        /// <summary>
        /// ���ݿ��ұ���/ҩƷ���� ��ȡ��ҩƷ�ڿ����ڿ���Ƿ��ѵ��ھ�����
        /// </summary>
        /// <param name="stockDeptCode">�ⷿ����</param>
        /// <param name="drugCode">ҩƷ����</param>
        /// <returns>����С�ھ����� False ���ھ����� True</returns>
        public bool GetWarnDrugStock(string stockDeptCode, string drugCode)
        {
            this.SetDB(itemManager);

            return itemManager.GetWarnDrugStock(stockDeptCode, drugCode);
        }

        /// <summary>
        /// ��ȡ��������Ч�ڱ�����Ϣ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <returns>�ɹ����ؿ�����Ч�ڱ�����Ϣ ʧ�ܷ���null</returns>
        public ArrayList GetWarnValidStock(string deptCode)
        {
            this.SetDB(itemManager);

            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();

            //������Ĭ������
            int ctrlValid = ctrlParamIntegrate.GetControlParam<int>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.Valid_Warn_Days, true, 30);

            return itemManager.QueryWarnValidDateStockInfoList(deptCode, ctrlValid);
        }

        #endregion

        #region ���߿��

        ///// <summary>
        ///// �Ի��߹������ҩƷ���г��⴦��
        ///// </summary>
        ///// <param name="execOrder">ҽ��ִ��ʵ��</param>
        ///// <param name="feeFlag">�Ʒѱ�־ 0 ���Ʒ� 1 ���ݼƷ�����feeNum���мƷ� 2 ��ԭ���̽��� ����ִ�е���Ϣ�����Ʒ�</param>
        ///// <param name="isFee">�Ƿ����շ� feeFlag Ϊ "0" ʱ�ò�����������</param>
        ///// <param name="feeNum">�Ʒ����� isFeeΪtrueʱ����������Ч</param>
        ///// <param name="phaNum">��ҩ����</param>
        ///// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        //public int PatientStore(FS.HISFC.Models.Order.ExecOrder execOrder, ref string feeFlag, ref decimal feeNum, ref bool isFee, ref decimal phaNum, ref string execSeq)
        //{
        //    this.SetDB(itemManager);

        //    return itemManager.PatientStoreNew(execOrder, ref feeFlag, ref feeNum, ref isFee, ref phaNum, ref execSeq);
        //}


        /// <summary>
        /// �Ի��߹������ҩƷ���г��⴦��
        /// </summary>
        /// <param name="execOrder">ҽ��ִ��ʵ��</param>
        /// <param name="feeFlag">�Ʒѱ�־ 0 ���Ʒ� 1 ���ݼƷ�����feeNum���мƷ� 2 ��ԭ���̽��� ����ִ�е���Ϣ�����Ʒ�</param>
        /// <param name="isFee">�Ƿ����շ� feeFlag Ϊ "0" ʱ�ò�����������</param>
        /// <param name="feeNum">�Ʒ����� isFeeΪtrueʱ����������Ч</param>
        /// <param name="phaNum">��ҩ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int PatientStore(FS.HISFC.Models.Order.ExecOrder execOrder, ref string feeFlag, ref decimal feeNum, ref bool isFee, ref decimal phaNum)
        {
            this.SetDB(itemManager);
            return this.PatientStoreNew(execOrder, ref feeFlag, ref feeNum, ref isFee, ref phaNum);
        }

        /// <summary>
        /// ҽ��ȡ���ӿ�
        /// </summary>
        private FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit iOrderSplit = null;

        /// <summary>
        /// �Ƿ��Ѿ���ѯȡ���ӿڣ�����ӿ�Ϊ�յĻ� ����Ĭ�Ϸ���
        /// </summary>
        private bool isGetSplitInterface = false;
        
        /// <summary>
        /// ҩƷ�б�
        /// </summary>
        private Hashtable hsPhaItems = new Hashtable();

        /// <summary>
        /// �Ի��߹������ҩƷ���г��⴦��
        /// </summary>
        /// <param name="execOrder">ҽ��ִ��ʵ��</param>
        /// <param name="feeFlag">�Ʒѱ�־ 0 ���Ʒ� 1 ���ݼƷ�����feeNum���мƷ� 2 ��ԭ���̽��� ����ִ�е���Ϣ�����Ʒ�</param>
        /// <param name="isFee">�Ƿ����շ� feeFlag Ϊ "0" ʱ�ò�����������</param>
        /// <param name="feeNum">�Ʒ����� isFeeΪtrueʱ����������Ч</param>
        /// <param name="phaNum">��ҩ����</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        private int PatientStoreNew(FS.HISFC.Models.Order.ExecOrder execOrder, ref string feeFlag, ref decimal feeNum, ref bool isFee, ref decimal phaNum)
        {
            if (!execOrder.Order.OrderType.IsDecompose)
            {
                feeFlag = "2";
                return 1;
            }
            if (!execOrder.Order.OrderType.IsCharge || execOrder.Order.Item.ID == "999")
            {
                feeFlag = "2";
                return 1;
            }

            #region ��ʼ��������������Ч���ж�

            if (execOrder.Order.Item.ItemType == EnumItemType.UnDrug)
            {
                this.Err = "��ҩƷ���ܽ��а�ҩ����";
                return -1;
            }
            FS.HISFC.Models.Pharmacy.Item itemPha = execOrder.Order.Item as FS.HISFC.Models.Pharmacy.Item;
            if (itemPha == null)
            {
                this.Err = "�����ҽ��ִ��ʵ������ĿΪ��ҩƷ " + execOrder.Order.Item.Name;
                return -1;
            }

            #endregion

            if (!hsPhaItems.Contains(itemPha.ID))
            {
                itemPha = this.GetItem(itemPha.ID);
                if (itemPha == null)
                {
                    return -1;
                }
                hsPhaItems.Add(itemPha.ID, itemPha);
            }
            else
            {
                itemPha = hsPhaItems[itemPha.ID] as FS.HISFC.Models.Pharmacy.Item;
            }

            feeFlag = "2";
            feeNum = 0;
            phaNum = 0;
            isFee = true;

            string CDSplitType = itemPha.CDSplitType;

            #region �ӿ�ȡ��

            if (iOrderSplit == null && !isGetSplitInterface)
            {
                iOrderSplit = FS.FrameWork.WinForms.Classes.UtilInterface.CreateObject(typeof(FS.HISFC.BizProcess.Integrate.Pharmacy), typeof(FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit)) as FS.SOC.HISFC.BizProcess.OrderInterface.Common.IOrderSplit;
                isGetSplitInterface = true;
            }

            if (iOrderSplit != null)
            {
                //if (iOrderSplit.ComputeOrderQty(execOrder, ref feeFlag, ref feeNum, ref phaNum) == -1)
                //{
                //    Err = iOrderSplit.ErrInfo;
                //    return -1;
                //}

                CDSplitType = iOrderSplit.GetSplitType(2, execOrder.Order);
            }
            #endregion
            #region ���ݲ�ͬ��ҩ���� ������ʱ����ֵ

            /* CDSplitType
                * 0����С��λ����ȡ�� 
                * 1����װ��λ����ȡ�� 
                * 2����С��λÿ��ȡ��
                * 3����װ��λÿ��ȡ��
                * 4����С��λ�ɲ�� ����˲�����ά����Ϊ�գ�Ĭ�ϰ��մ˹�����
                * */
            switch (CDSplitType)
            {
                case "":
                //4����С��λ�ɲ�� 
                case "4":
                    feeFlag = "2";//0 ���Ʒ� 1 ���ݼƷ�����feeNum���мƷ� 2 ��ԭ���̽���
                    break;
                //0����С��λ����ȡ�� 
                case "0":
                    if (this.GetQtyByPatientStore(execOrder, itemPha, false, ref feeFlag, ref feeNum, ref isFee, ref phaNum) == -1)
                    {
                        return -1;
                    }
                    break;
                //1����װ��λ����ȡ�� 
                case "1":
                    if (this.GetQtyByPatientStore(execOrder, itemPha, true, ref feeFlag, ref feeNum, ref isFee, ref phaNum) == -1)
                    {
                        return -1;
                    }
                    break;
                //2����С��λÿ��ȡ��
                case "2":
                    feeFlag = "1";
                    feeNum = (decimal)Math.Ceiling(Math.Round((double)execOrder.Order.DoseOnce / (double)itemPha.BaseDose, 5));
                    phaNum = feeNum;
                    break;
                //3����װ��λÿ��ȡ��
                case "3":
                    feeFlag = "1";
                    feeNum = (decimal)Math.Ceiling(Math.Round(((double)execOrder.Order.DoseOnce / (double)itemPha.BaseDose) / (double)itemPha.PackQty, 5)) * itemPha.PackQty;
                    phaNum = feeNum;
                    break;
                default:
                    feeFlag = "2";
                    break;
            }

            #endregion
            return 1;
        }
        
        /// <summary>
        /// �Ƿ��߿������Ϊ������Ч
        /// </summary>
        private string isPatientStoreDayLimit = "-1";

        /// <summary>
        /// ���ݻ��߿���ȡ�շ�����
        /// </summary>
        /// <param name="execOrder"></param>
        /// <param name="itemPha"></param>
        /// <param name="isPackQty"></param>
        /// <param name="feeFlag"></param>
        /// <param name="feeNum"></param>
        /// <param name="isFee"></param>
        /// <param name="phaNum">��ҩ����</param>
        /// <returns></returns>
        private int GetQtyByPatientStore(FS.HISFC.Models.Order.ExecOrder execOrder, FS.HISFC.Models.Pharmacy.Item itemPha, bool isPackQty, ref string feeFlag, ref decimal feeNum, ref bool isFee, ref decimal phaNum)
        {
            try
            {
                if (isPatientStoreDayLimit == "-1")
                {
                    FS.FrameWork.Management.ControlParam contoleMgr = new FS.FrameWork.Management.ControlParam();
                    isPatientStoreDayLimit = contoleMgr.QueryControlerInfo("HNPHA1", true);
                }

                //��ȡ��ҩ������Ϣ
                string drugProperty = this.GetDrugProperty(execOrder.Order.Item.ID, itemPha.DosageForm.ID, execOrder.Order.Patient.PVisit.PatientLocation.Dept.ID);
                //��ȡ��������
                DateTime sysTime = this.GetDateTimeFromSysDateTime();

                //������Ϣ
                FS.FrameWork.Models.NeuObject patientInfo = new FS.FrameWork.Models.NeuObject();
                //���߿�����Ϣ
                FS.FrameWork.Models.NeuObject patientDeptInfo = new FS.FrameWork.Models.NeuObject();

                //ȡ������ 0 ����ȡ�� 1 ���Ҳ��� 2 ����ȡ��
                string storeType = "0";

                switch (drugProperty)
                {
                    case "3":               //���߿�浱��ȡ��
                        patientInfo.ID = execOrder.Order.Patient.ID;
                        patientInfo.Name = execOrder.Order.Patient.Name;

                        patientDeptInfo.ID = execOrder.Order.Patient.PVisit.PatientLocation.Dept.ID;
                        patientDeptInfo.Name = execOrder.Order.Patient.PVisit.PatientLocation.Dept.Name;

                        storeType = "0";
                        break;
                    case "4":               //���ҿ��ȡ��
                        patientInfo.ID = "AAAA";
                        patientInfo.Name = "���л���";

                        patientDeptInfo.ID = execOrder.Order.Patient.PVisit.PatientLocation.Dept.ID;
                        patientDeptInfo.Name = execOrder.Order.Patient.PVisit.PatientLocation.Dept.Name;

                        storeType = "1";
                        break;
                    case "5":               //�������ȡ��
                        patientInfo.ID = "AAAA";
                        patientInfo.Name = "���л���";

                        patientDeptInfo.ID = execOrder.Order.Patient.PVisit.PatientLocation.NurseCell.ID;
                        patientDeptInfo.Name = execOrder.Order.Patient.PVisit.PatientLocation.NurseCell.Name;

                        storeType = "2";
                        break;
                    //���ά��Ϊ����ȡ����Ĭ�ϰ��ջ���ȡ��
                    case "0":
                        patientInfo.ID = execOrder.Order.Patient.ID;
                        patientInfo.Name = execOrder.Order.Patient.Name;

                        patientDeptInfo.ID = execOrder.Order.Patient.PVisit.PatientLocation.Dept.ID;
                        patientDeptInfo.Name = execOrder.Order.Patient.PVisit.PatientLocation.Dept.Name;

                        storeType = "0";
                        break;
                    default:                //��ҩ���Բ�������ȡ������ ��������
                        feeFlag = "2";      //0 ���Ʒ� 1 ���ݼƷ�����feeNum���мƷ� 2 ��ԭ���̽���

                        return 1;
                }

                FS.HISFC.Models.Pharmacy.PatientStore patientStore = this.itemManager.GetPatientStore(storeType, patientDeptInfo.ID, patientInfo.ID, itemPha.ID);

                if (patientStore == null)
                    return -1;

                //ҽ������
                execOrder.Order.Qty = System.Convert.ToDecimal(execOrder.Order.DoseOnce) / itemPha.BaseDose;

                if (patientStore.PatientInfo.ID == "")
                {
                    #region ���߿�����޸�ҩƷ

                    //�Ƿ��װ��λ����ȡ��
                    if (isPackQty)
                    {
                        phaNum = (decimal)System.Math.Ceiling(Math.Round(((double)execOrder.Order.DoseOnce / (double)itemPha.BaseDose) / (double)itemPha.PackQty, 5)) * itemPha.PackQty;
                    }
                    else
                    {
                        phaNum = (decimal)System.Math.Ceiling(Math.Round((double)execOrder.Order.DoseOnce / (double)itemPha.BaseDose, 5));
                    }

                    //���ҡ���������շ�����������ȡ��
                    if (storeType == "0")
                    {
                        feeNum = phaNum;
                    }
                    else
                    {
                        feeNum = execOrder.Order.Qty;
                    }

                    patientStore.Item = itemPha;			        //��Ŀʵ��
                    patientStore.PatientInfo = patientInfo;         //������Ϣ
                    patientStore.InDept = patientDeptInfo;          //�������ڿ���/����
                    patientStore.Type = storeType;
                    //������� ȡ�����ȥ����ҽ����
                    patientStore.StoreQty = phaNum - execOrder.Order.Qty;
                    patientStore.ValidTime = sysTime.Date;	        //��Ч�� �洢��������
                    patientStore.Oper.ID = this.itemManager.Operator.ID;
                    patientStore.Oper.OperTime = sysTime;
                    patientStore.IsCharge = true;
                    patientStore.FeeOper.ID = this.itemManager.Operator.ID;
                    patientStore.FeeOper.OperTime = sysTime;

                    patientStore.Extend = execOrder.ID;

                    if (this.itemManager.InsertPatientStore(patientStore) == -1)
                    {
                        return -1;
                    }
                    feeFlag = "1";	//0 ���Ʒ� 1 ���ݼƷ�����feeNum���мƷ� 2 ��ԭ���̽��� ����ִ�е���Ϣ�����Ʒ�

                    #endregion

                    return 1;
                }
                else
                {
                    #region ���߿�������и�ҩƷ��¼ ������Ч�ڽ��д���

                    if (patientStore.StoreQty < execOrder.Order.Qty
                        || (patientStore.ValidTime.Date < sysTime.Date && isPatientStoreDayLimit == "1")
                        )
                    {
                        #region ԭ����¼�������� ����Ϊ����Ӧʣ�����

                        //�Ƿ��װ��λ����ȡ��
                        if (isPackQty)
                        {
                            phaNum = (decimal)System.Math.Ceiling(Math.Round(((double)execOrder.Order.DoseOnce / (double)itemPha.BaseDose) / (double)itemPha.PackQty, 5)) * itemPha.PackQty;
                        }
                        else
                        {
                            phaNum = (decimal)System.Math.Ceiling(Math.Round((double)execOrder.Order.DoseOnce / (double)itemPha.BaseDose, 5));
                        }

                        //���ҡ���������շ�����������ȡ��
                        if (storeType == "0")
                        {
                            feeNum = phaNum;
                        }
                        else
                        {
                            feeNum = execOrder.Order.Qty;
                        }

                        patientStore.Item = itemPha;
                        patientStore.PatientInfo = patientInfo;
                        patientStore.InDept = patientDeptInfo;
                        patientStore.Type = storeType;
                        patientStore.StoreQty = phaNum - execOrder.Order.Qty;		//���ԭ����� ����Ϊ������

                        patientStore.ValidTime = sysTime.Date;		//�洢��������
                        patientStore.Oper.ID = this.itemManager.Operator.ID;
                        patientStore.Oper.OperTime = sysTime;
                        patientStore.IsCharge = true;
                        patientStore.FeeOper.ID = this.itemManager.Operator.ID;
                        patientStore.FeeOper.OperTime = sysTime;

                        patientStore.Extend = execOrder.ID;

                        if (this.itemManager.UpdatePatientStore(patientStore) != 1)
                        {
                            return -1;
                        }
                        feeFlag = "1";	//0 ���Ʒ� 1 ���ݼƷ�����feeNum���мƷ� 2 ��ԭ���̽���

                        #endregion

                        return 1;
                    }

                    if (patientStore.StoreQty >= execOrder.Order.Qty)
                    {
                        #region ����������� ���»��߿��

                        patientStore.Item = itemPha;
                        patientStore.PatientInfo = patientInfo;
                        patientStore.InDept = patientDeptInfo;
                        patientStore.Type = storeType;
                        //{9D24EE50-57C5-4cae-8B70-8D8C3E7B4BDC}
                        patientStore.StoreQty -= execOrder.Order.Qty;
                        patientStore.ValidTime = sysTime.Date;		//�洢��������
                        patientStore.Oper.ID = this.itemManager.Operator.ID;
                        patientStore.Oper.OperTime = sysTime;

                        //execSeq = patientStore.Extend;

                        if (this.itemManager.UpdatePatientStoreQty(storeType, patientDeptInfo.ID, patientInfo.ID, itemPha.ID, patientStore.StoreQty) != 1)
                        {
                            return -1;
                        }

                        isFee = patientStore.IsCharge;

                        #endregion

                        //���ҡ���������շ�����������ȡ��
                        if (storeType != "0")
                        {
                            feeNum = execOrder.Order.Qty;
                            phaNum = 0;
                            feeFlag = "1";
                        }
                        else
                        {
                            //feeFlag = "0";//0 ���Ʒ� 1 ���ݼƷ�����feeNum���мƷ� 2 ��ԭ���̽���
                            feeFlag = "1";//���ڲ��۷ѵģ���ȡ����Ϊ0����֤�ֽ⡢�շѡ���ҩһһ��Ӧ
                            feeNum = 0;
                            phaNum = 0;
                        }

                        return 1;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��ÿ���,���߿��
        /// </summary>
        /// <param name="deptCode">���һ��߻���סԺ��ˮ��</param>
        /// <param name="itemCode">��Ŀ����</param>
        /// <returns>�ɹ� ��ÿ���,���߿�� ʧ�� null</returns>
        public ArrayList QueryStorageList(string deptCode, string itemCode)
        {
            this.SetDB(itemManager);

            return itemManager.QueryStorageList(deptCode, itemCode);
        }

        /// <summary>
        /// Ԥ�ۿ�����{E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
        /// </summary>
        /// <param name="applyOut"></param>
        /// <param name="alterStoreNum"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        public int UpdateStockinfoPreOutNum(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, decimal alterStoreNum, decimal days)
        {
            this.SetDB(itemManager);

            if (alterStoreNum > 0)
            {
                return this.InsertPreoutStore(applyOut);
            }
            else
            {
                return this.DeletePreoutStore(applyOut);
            }
        }

        #endregion

        #region �˷�������Ϣ��������

        /// <summary>
        /// ȡĳһҩ����ĳһ������ң�ĳһ���ߴ���ҩ��ϸ�б�
        /// </summary>
        /// <param name="applyDeptCode">������ұ���</param>
        /// <param name="medDeptCode">ҩ������</param>
        /// <param name="patientID">סԺ��ˮ�� ��ѯȫ������סԺ��ˮ�Ŵ����</param>
        /// <returns>�ɹ�����ApplyOutʵ������ ʧ�ܷ���null</returns>
        public ArrayList QueryDrugReturn(string applyDeptCode, string medDeptCode, string patientID)
        {
            this.SetDB(itemManager);

            return itemManager.QueryDrugReturn(applyDeptCode, medDeptCode, patientID);
        }

        public List<FS.HISFC.Models.Fee.ReturnApply> QueryReturnApply(string applyDeptCode, string medDeptCode, string patientID)
        {
            ArrayList al = this.QueryDrugReturn(applyDeptCode, medDeptCode, patientID);
            if (al == null)
            {
                return null;
            }

            List<FS.HISFC.Models.Fee.ReturnApply> returnApplyList = new List<FS.HISFC.Models.Fee.ReturnApply>();

            foreach (FS.HISFC.Models.Pharmacy.ApplyOut info in al)
            {
                FS.HISFC.Models.Fee.ReturnApply temp = new FS.HISFC.Models.Fee.ReturnApply();

                temp.Item = info.Item;

                //applyOut.ID = applyReturn.ID;								//������ˮ��
                //applyOut.BillCode = applyReturn.BillCode;					//���뵥�ݺ�
                //applyOut.RecipeNo = applyReturn.RecipeNo;					//������
                //applyOut.SequenceNo = applyReturn.SequenceNo;				//��������Ŀ��ˮ��
                //applyOut.ApplyDept.ID = applyReturn.OperDpcd;				//�������
                //applyOut.Item.Name = applyReturn.Item.Name;					//��Ŀ����
                //applyOut.Item.ID = applyReturn.Item.ID;						//��Ŀ����
                //applyOut.Item.Specs = applyReturn.Item.Specs;				//���
                //applyOut.Item.Price = applyReturn.Item.Price;				//���ۼ�  ����С��λ��������ۼ�
                //applyOut.ApplyNum = applyReturn.Item.Amount;				//������ҩ���������Ը��������������
                //applyOut.Item.PackQty = applyReturn.Item.PackQty;
                //applyOut.Days = applyReturn.Days;							//����
                //applyOut.Item.MinUnit = applyReturn.Item.PriceUnit;			//�Ƽ۵�λ
                //applyOut.User01 = "0";										//��־�������ɲ����˷�������� ��applyReturnʵ���ȡ
                //applyOut.BillCode = applyReturn.BillCode;
            }

            return null;
        }

        #endregion

        #region ��ȡȡҩҩ���б�

        public ArrayList QueryReciveDrugDept(string roomCode,string drugType)
        {
            FS.HISFC.BizLogic.Pharmacy.Constant phaConsManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            this.SetDB(phaConsManager);

            return phaConsManager.QueryReciveDrugDept(roomCode, drugType);
        }

        #endregion

        #region �Ƽ����۳�����Ʒ���

        /// <summary>
        /// �Ƽ�����ԭ�Ͽ��۳�.�����¼���ɡ�
        /// </summary>
        /// <param name="materialItem">����ԭ�ϳ�����Ϣ</param>
        /// <param name="outDept">�������</param>
        /// <param name="qty">��������</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int ProduceOutput(FS.HISFC.Models.Pharmacy.Item materialItem,FS.HISFC.Models.Preparation.Expand expand,FS.FrameWork.Models.NeuObject outDept)
        {
            this.SetDB(itemManager);

            return itemManager.ProduceOutput(materialItem, expand, outDept);
        }

        /// <summary>
        /// �Ƽ�ԭ������
        /// </summary>
        /// <param name="item">������Ŀ��Ϣ</param>
        /// <param name="expand">�Ƽ�������Ϣ</param>
        /// <param name="applyDept">�������</param>
        /// <param name="stockDept">������</param>
        /// <returns></returns>
        public int ProduceApply(FS.HISFC.Models.Pharmacy.Item item, FS.HISFC.Models.Preparation.Expand expand, FS.FrameWork.Models.NeuObject applyDept, FS.FrameWork.Models.NeuObject stockDept)
        {
            this.SetDB(itemManager);

            return itemManager.ProduceApply(item, expand, applyDept, stockDept);
        }

        /// <summary>
        /// �Ƽ��������
        /// </summary>
        /// <param name="preparationList">����Ƽ���Ϣ</param>
        /// <returns></returns>
        public int ProduceInput(List<FS.HISFC.Models.Preparation.Preparation> preparationList,FS.FrameWork.Models.NeuObject pprDept,FS.FrameWork.Models.NeuObject stockDept,bool isApply)
        {
            this.SetDB(itemManager);

            return itemManager.ProduceInput(preparationList, pprDept,stockDept,isApply);
        }
        #endregion

        #region ҩ������

        /// <summary>
        /// ��ѯҩ������Ҷ�ӽڵ����� by Sunjh 2009-6-5 {D7977C2D-3047-406f-A0D2-4B7245CB0088}
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPhaFunction()
        {
            FS.HISFC.BizLogic.Pharmacy.Constant consManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            return consManager.QueryPhaFunctionLeafage();
        }

        #endregion

        #region ҽ��Ȩ��

        /// <summary>
        /// ҽ��Ȩ����֤�������ṩҽ��վʹ�ã� {4D5E0EB4-E673-478b-AE8C-6A537F49FC5C}
        /// </summary>
        /// <param name="operCode">ҽ������</param>
        /// <param name="drugInfo">ҩƷʵ��</param>
        /// <returns> -1ʧ�� 0��Ȩ�� ����0��Ȩ��</returns>
        public int CheckPopedom(string operCode, FS.HISFC.Models.Pharmacy.Item drugInfo)
        {
            string doctLevel = "";
            int retCode = -1;
            FS.HISFC.BizLogic.Pharmacy.Constant constantManager = new FS.HISFC.BizLogic.Pharmacy.Constant();
            FS.HISFC.BizLogic.Manager.Person personManager = new FS.HISFC.BizLogic.Manager.Person();
            FS.HISFC.Models.Base.Employee emplObj = personManager.GetPersonByID(operCode);
            if (emplObj != null)
            {
                doctLevel = emplObj.Level.ID;
            }

            if (doctLevel == "")
            {
                return -1;
            }

            if (drugInfo.Quality.ID != "")
            {
                retCode = constantManager.QueryPopedom(doctLevel, drugInfo.Quality.ID, 0);
                if (retCode > 0)
                {
                    if (drugInfo.PhyFunction1.ID != "")
                    {
                        retCode = constantManager.QueryPopedom(doctLevel, drugInfo.PhyFunction1.ID, 1);
                    }
                    else
                    {
                        retCode = 1;
                    }
                }
                else
                {
                    return retCode;
                }
            }
            else
            {
                //ҩƷ����Ϊ��ʱĬ��Ϊ�п���Ȩ�ޣ�����ҽ��...
                retCode = 0;
            }            

            return retCode;
        }

        #endregion

        #region �˻�����  //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
        /// <summary>
        /// ɾ������ҩƷ��ҩ����
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="recipeSequenceNO">��������Ŀ��ˮ��</param>
        /// <returns></returns>
        public int DelApplyOut(FS.HISFC.Models.Order.Order order)
        {
            this.SetDB(itemManager);
            this.SetDB(OutPatientfeeManager);
            this.SetDB(drugStoreManager);
            string recipeNO = order.ReciptNO;
            string recipeSequenceNO = order.SequenceNO.ToString();
            string execDeptCode = order.StockDept.ID;
            //ɾ����ҩ����
            if (itemManager.DelApplyOut(recipeNO, recipeSequenceNO) <= 0)
            {
                this.Err = "ɾ����ҩ����ʧ�ܣ�" + itemManager.Err;
                return -1;
            }
            //���ݴ�����ִ�п��Ҳ�ѯҩƷ������Ϣ
            ArrayList drugFee = OutPatientfeeManager.GetDurgFeeByRecipeAndDept(recipeNO, execDeptCode);
            if (drugFee == null)
            {
                return -1;
            }
            if (drugFee.Count == 0)
            {
                if (drugStoreManager.DeleteDrugStoRecipe(recipeNO, execDeptCode) < 0)
                {
                    this.Err = "ɾ������ͷ����Ϣʧ�ܣ�" + drugStoreManager.Err;
                    return -1;
                }
            }
            else
            {
                decimal cost = 0m;
                int drugCount = drugFee.Count;
                foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList f in drugFee)
                {
                    cost += f.FT.OwnCost;
                }
                if (drugStoreManager.UpdateStoRecipe(recipeNO, execDeptCode, cost, drugCount) <= 0)
                {
                    this.Err = "���´���������ʧ�ܣ�" + drugStoreManager.Err;
                    return -1;
                }
            }
            return 1;
        }

        /// <summary>
        /// ����ʱɾ��ҩƷ��ҩ����
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="recipeSequenceNO">������Ŀ��ˮ��</param>
        /// <returns></returns>
        public int DelApplyOut(string recipeNO, string recipeSequenceNO)
        {
            this.SetDB(itemManager);
            return itemManager.DelApplyOut(recipeNO, recipeSequenceNO);
        }

        /// <summary>
        /// ɾ������ͷ��
        /// </summary>
        /// <param name="recipeNO">������</param>
        /// <param name="execDeptCode">ִ�п���</param>
        /// <returns></returns>
        public int DeleteDrugStoRecipe(string recipeNO, string execDeptCode)
        {
            this.SetDB(OutPatientfeeManager);
            return drugStoreManager.DeleteDrugStoRecipe(recipeNO, execDeptCode);
        }
        #endregion

        #region Э������
        /// <summary>
        /// ��ȡЭ������ҩƷ�б�
        /// </summary>
        /// <returns>�ɹ�����Э������ҩƷ���� ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryNostrumList()
        {
            this.SetDB(itemManager);

            return itemManager.QueryNostrumList("ALL");
        }

        /// <summary>
        /// ��ȡЭ������ҩƷ�б�
        /// </summary>
        /// <returns>�ɹ�����Э������ҩƷ���� ʧ�ܷ���null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryNostrumList(string DrugType)
        {
            this.SetDB(itemManager);

            return itemManager.QueryNostrumList(DrugType);
        }

        /// <summary>
        /// ��ѯЭ����������ϸ����ĵ���
        /// </summary>
        /// <param name="nostrumCode">Э����������</param>
        /// <returns>�����۸�0���ѯʧ�ܻ�û��ά��</returns>
        public decimal GetNostrumPrice(string nostrumCode)
        {
            this.SetDB(itemManager);

            return itemManager.GetNostrumPrice(nostrumCode);
        }

        /// <summary>
        /// ��ȡЭ��������ϸ��Ϣ
        /// </summary>
        /// <param name="packageCode">���ױ���</param>
        /// <returns>�ɹ�����1 ʧ�ܷ��أ�1</returns>
        public List<FS.HISFC.Models.Pharmacy.Nostrum> QueryNostrumDetail(string packageCode)
        {
            this.SetDB(itemManager);
            return itemManager.QueryNostrumDetail(packageCode);
        }

        #endregion

        #region ҩƷȫԺ���޹��� {1A398A34-0718-47ed-AAE9-36336430265E}

        /// <summary>
        /// �����ж���Ա�Ƿ����ޣ��������ж�ҽ�������Ƿ�������ҩƷ���о��ж��Ƿ��ޣ�
        /// </summary>
        /// <param name="alOrder">ҽ����</param>
        /// <param name="alReturn">���صĲ�����Ҫ���ҽ����</param>
        /// <returns></returns>
        public int CheckSpeDrug(ArrayList orderAl, ref ArrayList errOrderAl)
        {
            if (orderAl == null)
            {
                this.Err = "�����ҽ��������";
                return -1;
            }
            if (orderAl.Count <= 0)
            {
                return 0;
            }

            FS.HISFC.BizLogic.Pharmacy.Item itemPha = new FS.HISFC.BizLogic.Pharmacy.Item();
            FS.HISFC.BizLogic.Pharmacy.Constant constant = new FS.HISFC.BizLogic.Pharmacy.Constant();

            #region �������е�����ҩƷ�ı����������
            Hashtable itemHs = new Hashtable();
            ArrayList alItem = new ArrayList();
            alItem = constant.QueryAllSpeDrugCode();

            if (alItem == null)
            {
                this.Err = constant.Err;
                return -1;
            }

            foreach (FS.HISFC.Models.Pharmacy.Item item in alItem)
            {
                //{07AA9FFA-BC72-4443-99F2-85541A03E198}����ʹ�ð�װ��λ�洢������
                itemHs.Add(item.ID, item.Qty + item.OnceDose);//{58270036-8D3E-407a-8D59-3A09A0A91026}��������С�����ж�,item.Qty�Ѿ�����С��λ
            }

            #endregion

            #region �������е�ҽ�����߿��ҵı��������

            Hashtable doctorHs = new Hashtable();
            ArrayList alItem1 = new ArrayList();
            alItem1 = constant.QueryAllSpeDrugPerAndDep();

            if (alItem == null)
            {
                this.Err = constant.Err;
                return -1;
            }

            foreach (FS.HISFC.Models.Base.Employee empl in alItem1)
            {
                //doctorHs.Add(empl.ID, empl.User01);
                doctorHs.Add(empl.ID + empl.User01, empl.User01);
            }

            #endregion

            #region  ҽ���ı������������

            string doctorCode = string.Empty; ;
            string dept = string.Empty;
            Hashtable drugHash = new Hashtable();
            ArrayList drugList = new ArrayList();
            if (orderAl[0] is FS.HISFC.Models.Order.Inpatient.Order)
            {
                FS.HISFC.Models.Order.Inpatient.Order order1 = new FS.HISFC.Models.Order.Inpatient.Order();
                order1 = (FS.HISFC.Models.Order.Inpatient.Order)orderAl[0];
                doctorCode = order1.ReciptDoctor.ID;
                dept = order1.ReciptDept.ID;
                #region ����ҩƷ�ֱ�洢����ͬһ��ҩ��ҩƷ�����ϼ���һ��{4E90C59D-F06B-4de3-BFC1-C1E555BC08E8}

                foreach (FS.HISFC.Models.Order.Inpatient.Order orderObj1 in orderAl)
                {
                    orderObj1.Item.User01 = string.Empty;//���
                }

                for (int i = 0; i < orderAl.Count - 1; i++)//��������
                {

                    FS.HISFC.Models.Order.Inpatient.Order orderi = orderAl[i] as FS.HISFC.Models.Order.Inpatient.Order;
                    if (!string.IsNullOrEmpty(orderi.Item.SysClass.User02))//�ж�ͬһ��ҩƷ�Ƿ��Ѿ�������Ƚϼ���//��Ϊitem.user02ԭ����ֵ������sysclsaa��user02{B0EFCE08-DDBC-461f-A129-DFB2091086B5}
                    {
                        continue;
                    }
                    for (int j = i + 1; j < orderAl.Count; j++)
                    {
                        FS.HISFC.Models.Order.Inpatient.Order orderj = orderAl[j] as FS.HISFC.Models.Order.Inpatient.Order;
                        if (orderi.Item.ID == orderj.Item.ID)
                        {
                            decimal l = 0;
                            if (!string.IsNullOrEmpty(orderi.Item.User01))
                            {
                                l = Convert.ToDecimal(orderi.Item.User01);
                            }
                            #region {A33CB5A5-AEAB-4125-9EFD-55495CB94A71}
                            //ԭ���Ĵ��벻֪����λ����д�ļ��������⣬���¶����ˣ���λ����Ӧ������ȥ����
                            else
                            {
                                l = 0;
                            }
                            if (l == 0)
                            {
                                orderi.Item.User01 = (l + orderi.Item.Qty + orderj.Item.Qty).ToString();
                            }
                            else
                            {
                                orderi.Item.User01 = (l + orderj.Item.Qty).ToString();
                            }
                            #endregion
                            orderi.Item.SysClass.User02 = "have";//��Ϊitem.user02ԭ����ֵ������sysclsaa��user02{B0EFCE08-DDBC-461f-A129-DFB2091086B5}
                        }
                        else if (j == orderAl.Count - 1)
                        {
                            drugList.Add(orderj);
                        }

                    }
                    drugList.Add(orderi);
                }
                if (orderAl.Count == 1)
                {
                    drugList = orderAl;
                }
                foreach (FS.HISFC.Models.Order.Inpatient.Order orderObj in drugList)
                {
                    orderObj.Item.SysClass.User02 = string.Empty;//��Ϊitem.user02ԭ����ֵ������sysclsaa��user02{B0EFCE08-DDBC-461f-A129-DFB2091086B5}
                }


                #endregion

                #region ѭ������
                foreach (FS.HISFC.Models.Order.Inpatient.Order order in drugList)
                {
                    if (itemHs.Contains(order.Item.ID))
                    {
                        if (doctorHs.Contains(doctorCode + order.Item.ID) || doctorHs.Contains(dept + order.Item.ID))
                        {
                            continue;
                        }
                        else
                        {
                            DateTime beginTime = FS.FrameWork.Function.NConvert.ToDateTime(itemPha.GetSysDate());
                            int day = beginTime.Day;
                            beginTime = beginTime.AddDays(-day);
                            DateTime endTime = beginTime.AddMonths(1);
                            #region {902C80AC-58AE-4ff1-9B5A-0E72C80B025B}

                            //int consume = itemPha.GetSpeDrugConsume(order.Item.ID, beginTime, endTime);
                            //if (consume == -1)
                            //{
                            //    this.Err = "��ȡ����ҩƷ����������" + itemPha.Err;
                            //    return -1;
                            //}
                            FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe = itemPha.GetSpeDrugMaintenanceDrugByCode(order.Item.ID);
                            if (drugSpe == null)
                            {
                                this.Err = "��ȡ����ҩƷ������Ϣ����" + itemPha.Err;
                                return -1;
                            }
                            decimal quantity = drugSpe.Item.PriceCollection.RetailPrice;//��ҩƷ����������

                            //��������
                            //int consume = drugSpe.Item.PriceCollection.RetailPrice;

                            //decimal quantity = FS.NFC.Function.NConvert.ToDecimal(consume);//��ҩƷ����������
                            decimal d = FS.FrameWork.Function.NConvert.ToDecimal(itemHs[order.Item.ID]);
                            if (string.IsNullOrEmpty(order.Item.User01))
                            {
                                order.Item.User01 = order.Item.Qty.ToString();
                            }
                            if (Convert.ToDecimal(order.Item.User01) + quantity * order.Item.PackQty > d * order.Item.PackQty)
                            {
                                errOrderAl.Add(order);
                            }
                            #endregion
                        }
                    }
                }
                drugList.Clear();


                #endregion
            }
            else if (orderAl[0] is FS.HISFC.Models.Order.OutPatient.Order)
            {
                FS.HISFC.Models.Order.OutPatient.Order order1 = new FS.HISFC.Models.Order.OutPatient.Order();
                order1 = (FS.HISFC.Models.Order.OutPatient.Order)orderAl[0];
                doctorCode = order1.ReciptDoctor.ID;
                dept = order1.ReciptDept.ID;

                #region ����ҩƷ�ֱ�洢��{4E90C59D-F06B-4de3-BFC1-C1E555BC08E8}

                foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj1 in orderAl)
                {
                    orderObj1.Item.User01 = string.Empty;//���
                }

                for (int i = 0; i < orderAl.Count - 1; i++)//��������
                {

                    FS.HISFC.Models.Order.OutPatient.Order orderi = orderAl[i] as FS.HISFC.Models.Order.OutPatient.Order;
                    if (!string.IsNullOrEmpty(orderi.Item.SysClass.User02))//�ж�ͬһ��ҩƷ�Ƿ��Ѿ�������Ƚϼ���//��Ϊitem.user02ԭ����ֵ������sysclsaa��user02{B0EFCE08-DDBC-461f-A129-DFB2091086B5}
                    {
                        continue;
                    }
                    for (int j = i + 1; j < orderAl.Count; j++)
                    {
                        FS.HISFC.Models.Order.OutPatient.Order orderj = orderAl[j] as FS.HISFC.Models.Order.OutPatient.Order;
                        if (orderi.Item.ID == orderj.Item.ID)
                        {
                            decimal l = 0;
                            if (!string.IsNullOrEmpty(orderi.Item.User01))
                            {
                                l = Convert.ToDecimal(orderi.Item.User01);
                            }
                            #region {A33CB5A5-AEAB-4125-9EFD-55495CB94A71}
                            //ԭ���Ĵ��벻֪����λ����д�ļ��������⣬���¶����ˣ���λ����Ӧ������ȥ����
                            else
                            {
                                l = 0;
                            }
                            if (l == 0)
                            {
                                orderi.Item.User01 = (l + orderi.Item.Qty + orderj.Item.Qty).ToString();
                            }
                            else
                            {
                                orderi.Item.User01 = (l + orderj.Item.Qty).ToString();
                            }
                            #endregion
                            orderi.Item.SysClass.User02 = "have";//��Ϊitem.user02ԭ����ֵ������sysclsaa��user02{B0EFCE08-DDBC-461f-A129-DFB2091086B5}
                        }
                        else if (j == orderAl.Count - 1)
                        {
                            drugList.Add(orderj);
                        }

                    }
                    drugList.Add(orderi);
                }
                if (orderAl.Count == 1)
                {
                    drugList = orderAl;
                }
                foreach (FS.HISFC.Models.Order.OutPatient.Order orderObj in drugList)
                {
                    orderObj.Item.SysClass.User02 = string.Empty;//��Ϊitem.user02ԭ����ֵ������sysclsaa��user02{B0EFCE08-DDBC-461f-A129-DFB2091086B5}
                }


                #endregion


                #region ѭ������
                foreach (FS.HISFC.Models.Order.OutPatient.Order order in drugList)
                {
                    if (itemHs.Contains(order.Item.ID))
                    {
                        if (doctorHs.Contains(doctorCode + order.Item.ID) || doctorHs.Contains(dept + order.Item.ID))
                        {
                            continue;
                        }
                        else
                        {
                            DateTime beginTime = FS.FrameWork.Function.NConvert.ToDateTime(itemPha.GetDateTimeFromSysDateTime());
                            beginTime = new DateTime(beginTime.Year, beginTime.Month, 1, 0, 0, 0);
                            DateTime endTime = beginTime.AddMonths(1);

                            FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe = itemPha.GetSpeDrugMaintenanceDrugByCode(order.Item.ID);
                            if (drugSpe == null)
                            {
                                this.Err = "��ȡ����ҩƷ������Ϣ����" + itemPha.Err;
                                return -1;
                            }
                            decimal quantity = drugSpe.Item.PriceCollection.RetailPrice;//��ҩƷ����������
                            decimal d = FS.FrameWork.Function.NConvert.ToDecimal(itemHs[order.Item.ID]);
                            if (string.IsNullOrEmpty(order.Item.User01))
                            {
                                if (order.MinunitFlag != "1")//��װ��λʱ��ת������С��λ�ж�
                                {
                                    order.Item.User01 = string.Format("{0}", (order.Item.Qty * order.Item.PackQty)); //{58270036-8D3E-407a-8D59-3A09A0A91026}��������С�����ж�
                                }
                                else
                                {
                                    order.Item.User01 = string.Format("{0}", order.Item.Qty);
                                }
                            }
                            if (Convert.ToDecimal(order.Item.User01) + quantity * order.Item.PackQty > d * order.Item.PackQty)
                            {
                                errOrderAl.Add(order);
                            }

                        }
                    }
                }

                drugList.Clear();

                #endregion
            }
            else if (orderAl[0] is FS.HISFC.Models.Order.Order)
            {
                FS.HISFC.Models.Order.Order order1 = new FS.HISFC.Models.Order.Order();
                order1 = (FS.HISFC.Models.Order.Order)orderAl[0];
                doctorCode = order1.ReciptDoctor.ID;
                dept = order1.ReciptDept.ID;

                #region ����ҩƷ�ֱ�洢��{4E90C59D-F06B-4de3-BFC1-C1E555BC08E8}

                foreach (FS.HISFC.Models.Order.Order orderObj1 in orderAl)
                {
                    orderObj1.Item.User01 = string.Empty;//���
                }

                for (int i = 0; i < orderAl.Count - 1; i++)
                {

                    FS.HISFC.Models.Order.Order orderi = orderAl[i] as FS.HISFC.Models.Order.Order;
                    if (!string.IsNullOrEmpty(orderi.Item.SysClass.User02))//�ж�ͬһ��ҩƷ�Ƿ��Ѿ�������Ƚϼ���////��Ϊitem.user02ԭ����ֵ������sysclsaa��user02{B0EFCE08-DDBC-461f-A129-DFB2091086B5}
                    {
                        continue;
                    }
                    for (int j = i + 1; j < orderAl.Count; j++)
                    {
                        FS.HISFC.Models.Order.Order orderj = orderAl[j] as FS.HISFC.Models.Order.Order;
                        if (orderi.Item.ID == orderj.Item.ID)
                        {
                            decimal l = 0;
                            if (!string.IsNullOrEmpty(orderi.Item.User01))
                            {
                                l = Convert.ToDecimal(orderi.Item.User01);
                            }
                            #region {A33CB5A5-AEAB-4125-9EFD-55495CB94A71}
                            //ԭ���Ĵ��벻֪����λ����д�ļ��������⣬���¶����ˣ���λ����Ӧ������ȥ����
                            else
                            {
                                l = 0;
                            }
                            if (l == 0)
                            {
                                orderi.Item.User01 = (l + orderi.Item.Qty + orderj.Item.Qty).ToString();
                            }
                            else
                            {
                                orderi.Item.User01 = (l + orderj.Item.Qty).ToString();
                            }
                            #endregion
                            orderi.Item.SysClass.User02 = "have";//��Ϊitem.user02ԭ����ֵ������sysclsaa��user02{B0EFCE08-DDBC-461f-A129-DFB2091086B5}
                        }
                        else if (j == orderAl.Count - 1)
                        {
                            drugList.Add(orderj);
                        }

                    }
                    drugList.Add(orderi);
                }
                if (orderAl.Count == 1)
                {
                    drugList = orderAl;
                }
                foreach (FS.HISFC.Models.Order.Order orderObj in drugList)
                {
                    orderObj.Item.SysClass.User02 = string.Empty;//��Ϊitem.user02ԭ����ֵ������sysclsaa��user02{B0EFCE08-DDBC-461f-A129-DFB2091086B5}
                }


                #endregion

                #region ѭ������
                foreach (FS.HISFC.Models.Order.Order order in orderAl)
                {
                    if (itemHs.Contains(order.Item.ID))
                    {
                        if (doctorHs.Contains(doctorCode + order.Item.ID) || doctorHs.Contains(dept + order.Item.ID))
                        {
                            continue;
                        }
                        else
                        {
                            DateTime beginTime = FS.FrameWork.Function.NConvert.ToDateTime(itemPha.GetDateTimeFromSysDateTime());
                            beginTime = new DateTime(beginTime.Year, beginTime.Month, 1, 0, 0, 0);
                            DateTime endTime = beginTime.AddMonths(1);

                            #region {902C80AC-58AE-4ff1-9B5A-0E72C80B025B}
                            //int consume = itemPha.GetSpeDrugConsume(order.Item.ID, beginTime, endTime);
                            //if (consume == -1)
                            //{
                            //    this.Err = "��ȡ����ҩƷ����������" + itemPha.Err;
                            //    return -1;
                            //}

                            //decimal quantity = FS.NFC.Function.NConvert.ToDecimal(consume);//��ҩƷ����������
                            FS.HISFC.Models.Pharmacy.DrugSpecial drugSpe = itemPha.GetSpeDrugMaintenanceDrugByCode(order.Item.ID);
                            if (drugSpe == null)
                            {
                                this.Err = "��ȡ����ҩƷ������Ϣ����" + itemPha.Err;
                                return -1;
                            }
                            decimal quantity = drugSpe.Item.PriceCollection.RetailPrice;//��ҩƷ����������
                            decimal d = FS.FrameWork.Function.NConvert.ToDecimal(itemHs[order.Item.ID]);
                            if (string.IsNullOrEmpty(order.Item.User01))//{58270036-8D3E-407a-8D59-3A09A0A91026}��������С�����ж�
                            {
                                if (order.MinunitFlag == "0")//��С��λ
                                {
                                    order.Item.User01 = string.Format("{0}", (order.Item.Qty * order.Item.PackQty));
                                }
                                else
                                {
                                    order.Item.User01 = order.Item.Qty.ToString();
                                }
                            }
                            if (Convert.ToDecimal(order.Item.User01) + quantity * order.Item.PackQty > d * order.Item.PackQty)
                            {
                                errOrderAl.Add(order);
                            }
                            #endregion

                        }
                    }
                }
                drugList.Clear();

                #endregion

            }

            #endregion

            return 0;
        }

        /// <summary>
        /// �ж�һ������ҩƷ�Ƿ���Ҫ������������
        /// </summary>
        /// <param name="drugCode">ҩƷ����</param>
        /// <returns>��Ҫ����1����Ҫ����0���󷵻�-1</returns>
        public int CheckSpeDrugIsCountConsume(string drugCode, string deptCode, string doctorCode)
        {
            FS.HISFC.BizLogic.Pharmacy.Constant constant = new FS.HISFC.BizLogic.Pharmacy.Constant();
            #region �������е�����ҩƷ�ı����������

            ArrayList alItem = new ArrayList();
            Hashtable hsSpeDrug = new Hashtable();//ҩƷ��ϣ��
            alItem = constant.QueryAllSpeDrugCode();
            if (alItem == null)
            {
                this.Err = "��ȡ���е�����ҩƷά����Ϣ����" + constant.Err;
                return -1;
            }
            foreach (FS.HISFC.Models.Pharmacy.Item item in alItem)
            {
                hsSpeDrug.Add(item.ID, item.Qty + item.OnceDose);//{07AA9FFA-BC72-4443-99F2-85541A03E198}
            }

            #endregion

            #region �������е�ҽ�����߿��ҵı��������

            //Hashtable doctorHs = new Hashtable();
            ArrayList alItem1 = new ArrayList();
            alItem1 = constant.QueryAllSpeDrugPerAndDep();

            if (alItem == null)
            {
                this.Err = constant.Err;
                return -1;
            }

            //foreach (FS.HISFC.Models.Base.Employee empl in alItem1)
            //{
            //    doctorHs.Add(empl.ID, empl.User01);
            //}

            #endregion
            if (hsSpeDrug.Contains(drugCode))
            {
                foreach (FS.HISFC.Models.Base.Employee empl in alItem1)
                {
                    if ((empl.ID == deptCode && empl.User01 == drugCode) || (empl.ID == doctorCode && empl.User01 == drugCode))
                    {
                        return 0;
                    }
                }
                //{EAED7C02-D2BA-4e96-9C9A-51B9E39CAFC3}ʹ��Hashtable�жϲ�����
                //if (doctorHs.Contains(deptCode) || doctorHs.Contains(doctorCode))
                //{
                //    return 0;
                //}

                return 1;
            }

            return 0;
        }

        /// <summary>
        /// ����ҩƷ�����ȡ����ҩ�Ļ�����Ϣ {902C80AC-58AE-4ff1-9B5A-0E72C80B025B}
        /// </summary>
        /// <param name="drugCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Pharmacy.DrugSpecial GetSpeDrugMaintenanceDrugByCode(string drugCode)
        {
            this.SetDB(this.itemManager);
            return itemManager.GetSpeDrugMaintenanceDrugByCode(drugCode);
        }

        /// <summary>
        /// ����ҩƷ�����ȡ����ҩ�Ļ�����Ϣ
        /// </summary>
        /// <param name="drugCode"></param>
        /// <returns></returns>
        public int GetSpeDrugMaintenanceDrugByCode(string drugCode, FS.HISFC.Models.Pharmacy.DrugSpecial drugObj)
        {
            this.SetDB(this.itemManager);
            return itemManager.GetSpeDrugMaintenanceDrugByCode(drugCode, drugObj);
        }

        /// <summary>
        /// ����ҩƷ��������������
        /// </summary>
        /// <param name="drugCode"></param>
        /// <param name="dtReset"></param>
        /// <returns></returns>
        public int OutPutRestSpeDrugExpand(string drugCode, DateTime dtReset, decimal expandNum)
        {
            this.SetDB(this.itemManager);
            return itemManager.OutPutRestSpeDrugExpand(drugCode, dtReset, expandNum);
        }

        #endregion

        #region ���ﲿ�ַ�ҩ
        /// <summary>
        /// ���﷢ҩ����
        /// </summary>
        /// <param name="applyOutCollection">��ҩ������Ϣ</param>
        /// <param name="terminal">��ҩ�ն�</param>
        /// <param name="sendDept">��ҩ������Ϣ(�ۿ����)</param>
        /// <param name="sendOper">��ҩ��Ա��Ϣ</param>
        /// <param name="isDirectSave">�Ƿ�ֱ�ӱ��� (����ҩ����)</param>
        /// <param name="isUpdateAdjustParam">�Ƿ���´���������Ϣ</param>
        /// <returns>��ҩȷ�ϳɹ�����1 ʧ�ܷ���-1</returns>
        public int OutpatientPartSend(List<ApplyOut> applyOutCollection, NeuObject terminal, NeuObject sendDept, NeuObject sendOper, bool isDirectSave, bool isUpdateAdjustParam)
        {
            FS.FrameWork.Management.PublicTrans.BeginTransaction();
            this.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);

            FS.HISFC.BizLogic.Fee.Outpatient outPatientFeeManager = new FS.HISFC.BizLogic.Fee.Outpatient();

            //FS.FrameWork.Management.Transaction t = new FS.FrameWork.Management.Transaction(FS.FrameWork.Management.Connection.Instance);
            //t.BeginTransaction();
            //this.SetTrans(t.Trans);
            //outPatientFeeManager.SetTrans(t.Trans);

            DateTime sysTime = itemManager.GetDateTimeFromSysDateTime();

            int parm;
            ApplyOut info = new ApplyOut();
            for (int i = 0; i < applyOutCollection.Count; i++)
            {
                info = applyOutCollection[i] as ApplyOut;

                #region �������Ϣ����
                if (this.itemManager.UpdateApplyOutStateForPartSend(info, "2", sendOper.ID) < 1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���³����������ݳ���!" + itemManager.Err;
                    return -1;
                }
                if (info.ValidState == FS.HISFC.Models.Base.EnumValidState.Invalid)
                    continue;
                #endregion

                #region ���⴦��
                if (string.IsNullOrEmpty(info.DrugNO))
                {
                    info.DrugNO = "0";
                }
                //��ҩ��Ϣ ��ҩ���ҡ���ҩ��
                if (info.PrintState == "1" && info.BillClassNO != "")
                {
                    info.Operation.ApproveOper.Dept.ID = info.BillClassNO;
                }
                else
                {
                    info.Operation.ApproveOper.Dept = sendDept;
                }
                info.Operation.ApproveQty = info.Operation.ApplyQty;
                info.PrivType = "M1";

                info.Operation.ExamOper.ID = sendOper.ID;
                info.Operation.ExamOper.OperTime = sysTime;

                if (this.itemManager.Output(info) == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���ɳ����¼ ���¿�����  " + itemManager.Err;
                    return -1;
                }

                #endregion

                #region ���·��ñ���ȷ����Ϣ
                //0δȷ��/1��ȷ�� ���� 1δȷ��/2��ȷ��
                parm = itemManager.UpdateConfirmFlag(info.RecipeNO, info.OrderNO, "1", sendOper.ID, sendDept.ID, sysTime, info.Operation.ApplyQty);
                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "���·��ñ�ȷ�ϱ��ʧ��" + itemManager.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "δ��ȷ���·���ȷ�ϱ�� ���ݿ����ѱ���׼";
                    return -1;
                }
                #endregion

                #region �Ƿ���´���������Ϣ
                if (isUpdateAdjustParam || isDirectSave)
                {
                    //�������ҩ������� �Դ��ּ�¼�����и���
                    if (info.PrintState != "1" || info.BillClassNO == "")
                    {
                        //���������ն˴���ҩ��Ϣ ����-1ÿ�μ���1
                        FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipeTemp = new FS.HISFC.Models.Pharmacy.DrugRecipe();
                        string recipeState = "1";
                        if (isDirectSave)           //ֱ�ӷ�ҩ ״̬Ϊ "1"
                            recipeState = "1";
                        else                        //��/��ҩ���� ״̬Ϊ"2"
                            recipeState = "2";

                        drugRecipeTemp = drugStoreManager.GetDrugRecipe(info.StockDept.ID, "M1", info.RecipeNO, recipeState);
                        if (drugRecipeTemp != null)
                        {
                            if (drugStoreManager.UpdateTerminalAdjustInfo(drugRecipeTemp.DrugTerminal.ID, 0, -1, 0) == -1)
                            {
                                FS.FrameWork.Management.PublicTrans.RollBack();
                                this.Err = "���������ն�����ҩ��Ϣ����" + drugStoreManager.Err;
                                return -1;
                            }
                        }
                    }
                }
                #endregion

            }

            //��������������ڷ�ҩ��Ϣ		

            #region ���µ������ڷ�ҩ��Ϣ
            ArrayList al = itemManager.QueryApplyOutListForClinic(info.StockDept.ID, "M1", "1", info.RecipeNO);
            if (al != null && al.Count <= 0)
            {
                if (isDirectSave && isUpdateAdjustParam)           //ֱ�ӷ�ҩ  ���ȸ�����ҩ��Ϣ
                {
                    parm = drugStoreManager.UpdateDrugRecipeDrugedInfo(info.StockDept.ID, info.RecipeNO, "M1", sendOper.ID, sendDept.ID, applyOutCollection.Count);
                    if (parm == -1)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "����������ҩ���ݳ���!" + drugStoreManager.Err;
                        return -1;
                    }
                    else if (parm == 0)
                    {
                        FS.FrameWork.Management.PublicTrans.RollBack();
                        this.Err = "���ݿ����ѱ���׼! ��ˢ������" + drugStoreManager.Err;
                        return -1;
                    }

                    parm = drugStoreManager.UpdateDrugRecipeSendInfo(info.StockDept.ID, info.RecipeNO, "M1", "2", sendOper.ID, sendDept.ID, terminal.ID);
                }
                else                       //��/��ҩ���� 
                {
                    parm = 1;
                }

                if (parm == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "�������﷢ҩ���ݳ���!" + drugStoreManager.Err;
                    return -1;
                }
                else if (parm == 0)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    this.Err = "����ͷ����Ϣ�����ѱ���׼ ��ˢ������" + drugStoreManager.Err;
                    return -1;
                }
            }
            #endregion

            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

        /// <summary>
        /// �����շѵ��õĳ��⺯��
        /// </summary>
        /// <param name="patient">������Ϣʵ��</param>
        /// <param name="feeAl">������Ϣ����</param>
        /// <param name="feeWindow">�շѴ���</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="isModify">�Ƿ������˸�ҩ</param>
        /// <param name="drugSendInfo">����������Ϣ ��ҩҩ��+��ҩ����</param>
        /// <returns>1 �ɹ� ��1 ʧ��</returns>
        public int ApplyOut(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, DateTime operDate, string feeWindow, bool isModify, out string drugSendInfo)
        {
            FS.HISFC.BizLogic.Manager.Constant constantManager = new FS.HISFC.BizLogic.Manager.Constant();
            FS.HISFC.BizProcess.Integrate.Common.ControlParam ctrlParamIntegrate = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
            if (this.trans != null)
            {
                constantManager.SetTrans(this.trans);
                ctrlParamIntegrate.SetTrans(this.trans);
            }
            //���ڲ�ͬҩ������ʹ�ò�ͬ�ĵ�����ʽ ���Ե�����ʽ(����/ƽ��)��ҵ����ȡ

            this.SetDB(itemManager);

            Pharmacy.IsClinicPreOut = ctrlParamIntegrate.GetControlParam<bool>(FS.HISFC.BizProcess.Integrate.PharmacyConstant.OutDrug_Pre_Out, false, true);//�Ƿ�Ԥ�ۿ��
            if (Pharmacy.IsClinicPreOut)
            {
                Pharmacy.IsClinicPreOut = ctrlParamIntegrate.GetControlParam<bool>("P01015");//�շ�ʱ�Ƿ�Ԥ�� true�շ�ʱԤ�� false����ҽ��Ԥ��
            }
            return itemManager.ApplyOut(patient, feeAl, operDate, Pharmacy.IsClinicPreOut, isModify, out drugSendInfo);
        }
        #endregion

        #region IInterfaceContainer ��Ա

        public Type[] InterfaceTypes
        {
            get
            {
                return new Type[] { typeof(FS.HISFC.BizProcess.Interface.Pharmacy.ICompoundGroup) };
            }
        }

        #endregion

        #region Ԥ�۴���-���ϰ�
        /// <summary>
        /// ����Ԥ�ۼ�¼
        /// һ����סԺ��ʿִ��ʱ����
        /// </summary>
        /// <param name="applyOut">��������</param>
        /// <returns></returns>
        public int InsertPreoutStore(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            this.SetDB(itemManager);
            int param = this.itemManager.InsertPreoutStore(applyOut);
            if (param <= 0)
            {
                this.Err = this.itemManager.Err;
                this.ErrCode = this.itemManager.ErrCode;
            }
            return param;
        }
        /// <summary>
        /// ����Ԥ�ۼ�¼
        /// </summary>
        /// <param name="outPatientOrder">ҽ��ʵ��</param>
        /// <returns></returns>
        public int InsertPreoutStore(FS.HISFC.Models.Order.OutPatient.Order outPatientOrder)
        {
            this.SetDB(itemManager);
            int param = this.itemManager.InsertPreoutStore(outPatientOrder);
            if (param <= 0)
            {
                this.Err = this.itemManager.Err;
                this.ErrCode = this.itemManager.ErrCode;
            }
            return param;
        }

        /// <summary>
        /// ɾ��Ԥ�ۼ�¼
        /// </summary>
        /// <param name="applyOut"></param>
        /// <returns></returns>
        public int DeletePreoutStore(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            this.SetDB(itemManager);
            int param = this.itemManager.DeletePreoutStore(applyOut);
            if (param <= 0)
            {
                this.Err = this.itemManager.Err;
                this.ErrCode = this.itemManager.ErrCode;
            }
            return param;
        }

        /// <summary>
        /// ɾ��Ԥ�ۼ�¼
        /// </summary>
        /// <param name="outPatientOrder"></param>
        /// <returns></returns>
        public int DeletePreoutStore(FS.HISFC.Models.Order.OutPatient.Order outPatientOrder)
        {
            this.SetDB(itemManager);
            int param = this.itemManager.DeletePreoutStore(outPatientOrder);
            if (param <= 0)
            {
                this.Err = this.itemManager.Err;
                this.ErrCode = this.itemManager.ErrCode;
            }
            return param;
        }
        #endregion
    }
}
