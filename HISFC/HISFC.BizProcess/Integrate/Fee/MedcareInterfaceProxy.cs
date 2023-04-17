
using System;
using System.Collections.Generic;
using System.Collections;
using System.Reflection;
using FS.HISFC.BizProcess.Interface.FeeInterface;
namespace FS.HISFC.BizProcess.Integrate.FeeInterface
{
    public class MedcareInterfaceProxy : FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare, FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend, FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareBuDan
    {
        public MedcareInterfaceProxy()
        {

        }


        /// <summary>
        /// ֱ�Ӹ�ֵ��ͬ��λ
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        public MedcareInterfaceProxy(string pactCode, System.Data.IDbTransaction t)
        {
            this.pactCode = pactCode;
            this.SetPactCode(pactCode);
            this.trans = t;
        }

        /// <summary>
        /// �����������ʱ��,�ͷŽӿ�ʵ��
        /// </summary>
        ~MedcareInterfaceProxy()
        {
            this.medcaredInterface = null;
            //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
            interfaceHash.Clear();
        }

        /// <summary>
        /// ���ú�ͬ��λ
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int SetPactCode(string pactCode)
        {
            this.pactCode = pactCode;

            return this.GetInterfaceFromPact(pactCode);
        }

        /// <summary>
        /// ��֤�Һ���Ϣ�Ƿ�Ϸ�
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <returns>����: true ������ false</returns>
        private bool IsValid(FS.HISFC.Models.Registration.Register r)
        {
            if (this.medcaredInterface == null)
            {
                if (r == null)
                {
                    this.errMsg = "�Һ���ϢΪ��";

                    return false;
                }
                if (r.Pact == null || r.Pact.ID == null || r.Pact.ID == string.Empty)
                {
                    this.errMsg = "��ͬ��λΪ��!";

                    return false;
                }

                this.GetInterfaceFromPact(r.Pact.ID);

                if (this.medcaredInterface == null)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ��֤�Ǽ���Ϣ�Ƿ�Ϸ�
        /// </summary>
        /// <param name="patient">�Һ���Ϣ</param>
        /// <returns>����: true ������ false</returns>
        private bool IsValid(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            if (this.medcaredInterface == null)
            {
                if (patient == null)
                {
                    this.errMsg = "סԺ���߻�����Ϣ��ϢΪ��";

                    return false;
                }
                if (patient.Pact == null || patient.Pact.ID == null || patient.Pact.ID == string.Empty)
                {
                    this.errMsg = "��ͬ��λΪ��!";

                    return false;
                }

                this.GetInterfaceFromPact(patient.Pact.ID);

                if (this.medcaredInterface == null)
                {
                    return false;
                }
            }

            return true;
        }

        #region ����

        /// <summary>
        /// ҽ���ӿ�ʵ��
        /// </summary>
        private FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare medcaredInterface = null;

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        private System.Data.IDbTransaction trans = null;

        /// <summary>
        /// ��ǰ������Ϣ
        /// </summary>
        private string errMsg = string.Empty;

        /// <summary>
        /// ��ͬ��λ����
        /// </summary>
        private string pactCode = null;//��ͬ��λ����

        /// <summary>
        /// ��ͬ��λ����ҵ���
        /// </summary>
        private FS.HISFC.BizLogic.Fee.PactUnitInfo pactManager = new FS.HISFC.BizLogic.Fee.PactUnitInfo();

        /// <summary>
        /// ��ǰ����Ľӿ�����
        /// </summary>
        private static Hashtable interfaceHash = new Hashtable();

        #endregion

        #region ����

        /// <summary>
        /// ��ͬ��λ����
        /// </summary>
        public string PactCode
        {
            set
            {
                this.pactCode = value;
            }
            get
            {
                return this.pactCode;
            }
        }

        /// <summary>
        /// ��ǰ�ӿ�ʵ��
        /// </summary>
        public FS.HISFC.BizProcess.Interface.FeeInterface.IMedcare NowMedcaredInterface
        {
            get
            {
                return this.medcaredInterface;
            }
        }

        #endregion


        #region IMedcare ��Ա

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        public System.Data.IDbTransaction Trans
        {
            set
            {
                this.SetTrans(value);
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        public string ErrCode
        {
            get
            {
                if (this.medcaredInterface != null)
                {
                    return this.medcaredInterface.ErrCode;
                }
                else
                {
                    return "ʵ��Ϊ��!";
                }
            }
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string ErrMsg
        {
            get
            {
                return this.errMsg;
            }

        }

        /// <summary>
        /// �ӿ�������Ϣ
        /// </summary>
        public string Description
        {
            get
            {
                if (this.medcaredInterface != null)
                {
                    return this.medcaredInterface.Description;
                }

                return "ʵ��Ϊ��!";
            }
        }

        /// <summary>
        /// ͨ����ͬ��λ������
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int GetInterfaceFromPact(string pactCode)
        {
            //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
            if (MedcareInterfaceProxy.interfaceHash.ContainsKey(pactCode))
            {
                //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                this.medcaredInterface = (IMedcare)MedcareInterfaceProxy.interfaceHash[pactCode];
                if (this.medcaredInterface != null)
                {
                    return 1;
                }
            }

            if (this.trans != null)
            {
                this.pactManager.SetTrans(this.trans);
            }

            FS.HISFC.Models.Base.PactInfo pactInfo = this.pactManager.GetPactUnitInfoByPactCode(pactCode);
            if (pactInfo == null)
            {
                this.errMsg = "��û��ߺ�ͬ��λ����!(�ӿ�)" + this.pactManager.Err;

                return -1;
            }
            if (pactInfo.PactDllName == null || pactInfo.PactDllName == string.Empty)
            {
                this.errMsg = "���Ϊ: " + pactCode + "����Ϊ: " + pactInfo.Name + "�ĺ�ͬ��λû��ά�������㷨!";

                return -1;
            }

            try
            {
                // Assembly a = Assembly.LoadFrom(FS.FrameWork.WinForms.Classes.Function.PluginPath + "\\SI\\" + pactInfo.PactDllName);
                Assembly a = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "\\" + FS.FrameWork.WinForms.Classes.Function.PluginPath + "\\SI\\" + pactInfo.PactDllName);


                System.Type[] types = a.GetTypes();
                foreach (System.Type type in types)
                {
                    if (type.GetInterface("IMedcare") != null)
                    {
                        this.medcaredInterface = (IMedcare)System.Activator.CreateInstance(type);
                    }
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }

            //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
            MedcareInterfaceProxy.interfaceHash.Add(pactCode, this.medcaredInterface);

            return 1;
        }

        /// <summary>
        /// ���ñ������ݿ�����
        /// </summary>
        /// <param name="t">��ǰ���ݿ�����</param>
        public void SetTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
            try
            {
                if (this.medcaredInterface == null)
                {
                    //�����ǰ��ʵ��Ϊnull,���»��ҽ������ʵ��
                    this.GetInterfaceFromPact(pactCode);
                    if (medcaredInterface == null)
                    {
                        return;
                    }
                }

                medcaredInterface.SetTrans(t);
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return;
            }
        }

        public void SetPactTrans(System.Data.IDbTransaction t)
        {
            this.trans = t;
            this.pactManager.SetTrans(t);
            return;
        }
        /// <summary>
        /// ��ú�������Ϣ
        /// </summary>
        /// <param name="blackLists">��������Ϣ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int QueryBlackLists(ref ArrayList blackLists)
        {
            try
            {
                if (this.medcaredInterface == null)
                {
                    this.GetInterfaceFromPact(this.pactCode);

                    if (this.medcaredInterface == null)
                    {
                        this.errMsg = this.medcaredInterface.ErrMsg;
                        return -1;
                    }
                }

                int iReturn = 0;

                iReturn = this.medcaredInterface.QueryBlackLists(ref blackLists);

                if (iReturn <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// סԺ�����Ƿ��ٺ�������
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣ</param>
        /// <returns>�� true ���� false</returns>
        public bool IsInBlackList(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return true;
                }

                bool returnValue = this.medcaredInterface.IsInBlackList(patient);
                if (returnValue)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;

            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return true;
            }
        }

        /// <summary>
        /// ���ﻼ���Ƿ��ٺ�������
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <returns>�� true ���� false</returns>
        public bool IsInBlackList(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return false;
                }

                bool returnValue = this.medcaredInterface.IsInBlackList(r);
                if (returnValue == true)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;

            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return false;
            }
        }

        /// <summary>
        /// ���ҽ�����߹��ѷ�ҩƷ�б�
        /// </summary>
        /// <param name="undrugLists">��ҩƷ�б�</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int QueryUndrugLists(ref ArrayList undrugLists)
        {
            try
            {
                if (this.medcaredInterface == null)
                {
                    this.GetInterfaceFromPact(this.pactCode);

                    if (this.medcaredInterface == null)
                    {
                        this.errMsg = this.medcaredInterface.ErrMsg;
                        return -1;
                    }
                }

                int iReturn = 0;

                iReturn = this.medcaredInterface.QueryUndrugLists(ref undrugLists);

                if (iReturn <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ���ҽ�����߹���ҩƷ�б�
        /// </summary>
        /// <param name="drugLists">ҩƷ�б�</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int QueryDrugLists(ref ArrayList drugLists)
        {
            try
            {
                if (this.medcaredInterface == null)
                {
                    this.GetInterfaceFromPact(this.pactCode);

                    if (this.medcaredInterface == null)
                    {
                        this.errMsg = this.medcaredInterface.ErrMsg;
                        return -1;
                    }
                }

                int iReturn = 0;

                iReturn = this.medcaredInterface.QueryDrugLists(ref drugLists);

                if (iReturn <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                    return -1;
                }
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }

            return 1;
        }

        /// <summary>
        /// ����ҽ�����߹��ѵǼǺ���
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        public int UploadRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UploadRegInfoOutpatient(r);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// �������ҽ�����ѵǼ���Ϣ
        /// </summary>
        /// <param name="r">����Һ�ʵ��</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.GetRegInfoOutpatient(r);
                //{187A73EB-008A-4A25-A6CB-28CAE0E629A7}��ѯ����������Ϣ��ӱ�ע��Ϣ
                FS.HISFC.BizLogic.RADT.InPatient patientManger = new FS.HISFC.BizLogic.RADT.InPatient();
                r.Memo = patientManger.QueryComPatientInfo(r.PID.CardNO).Memo;
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// �������ҽ�����ѵǼ���Ϣ
        /// </summary>
        /// <param name="r">����Һ�ʵ��</param>
        /// <param name="isReadMCard">�Ƿ��ҽ�����ж���</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        public int GetRegInfoOutpatient(FS.HISFC.Models.Registration.Register r, bool isReadMCard)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.GetRegInfoOutpatient(r);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }


        /// <summary>
        /// ���ﵥ���ϴ�������ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="f">���������ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        public int UploadFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UploadFeeDetailOutpatient(r, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// ��������ϴ�������ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="feeDetails">������ϸʵ�弯��</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int UploadFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UploadFeeDetailsOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// ����ɾ�������Ѿ��ϴ���ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="f">������ϸ��Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        public int DeleteUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailOutpatient(r, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// ����ɾ�����ߵ����з����ϴ���ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int DeleteUploadedFeeDetailsAllOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailsAllOutpatient(r);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        ///���� ɾ��ָ�����ݼ�����ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="feeDetails">Ҫɾ���ķ���ʵ����ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int DeleteUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailsOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// �޸ĵ����������ϴ���ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="f">Ҫ�޸ĵķ���ʵ����ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int ModifyUploadedFeeDetailOutpatient(FS.HISFC.Models.Registration.Register r, FS.HISFC.Models.Fee.Outpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.ModifyUploadedFeeDetailOutpatient(r, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// �޸Ķ����������ϴ���ϸ
        /// </summary>
        /// <param name="r">�Һ���Ϣ</param>
        /// <param name="feeDetails">Ҫ�޸ĵķ���ʵ����ϸ����</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int ModifyUploadedFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.ModifyUploadedFeeDetailsOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// ����ҽ��Ԥ����
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int PreBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.PreBalanceOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// ����ҽ������
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int BalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.BalanceOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }


        /// <summary>
        /// ����ȡ������
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">Ҫȡ������Ļ��߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int CancelBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.CancelBalanceOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// ����ȡ�����㣨���ˣ�
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">Ҫȡ������Ļ��߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int CancelBalanceOutpatientHalf(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.CancelBalanceOutpatientHalf(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺ���·�����Ϣ
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <param name="f">������ϸ</param>
        /// <returns>�ɹ� 1 ʧ�� -1 û�и��µ����� 0</returns>
        public int UpdateFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UpdateFeeItemListInpatient(patient, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺ���¼��������ϸ
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣ</param>
        /// <param name="f">סԺ���õ�����ϸ</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int RecomputeFeeItemListInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.RecomputeFeeItemListInpatient(patient, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺ�ǼǺ���
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ��߻�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        public int UploadRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }
                int returnValue = this.medcaredInterface.UploadRegInfoInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// ���ҽ��סԺ�Ǽ���Ϣ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        public int GetRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.GetRegInfoInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺ�����ϴ�������ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="f">סԺ������ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        public int UploadFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UploadFeeDetailInpatient(patient, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺ�����ϴ�������ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">������ϸʵ�弯��</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int UploadFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.UploadFeeDetailsInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺɾ�������Ѿ��ϴ���ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="f">������ϸ��Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ 1 �ɹ�</returns>
        public int DeleteUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailInpatient(patient, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺɾ�����ߵ����з����ϴ���ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int DeleteUploadedFeeDetailsAllInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailsAllInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺɾ��ָ�����ݼ�����ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">Ҫɾ���ķ���ʵ����ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int DeleteUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.DeleteUploadedFeeDetailsInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// �޸ĵ���סԺ���ϴ���ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="f">Ҫ�޸ĵķ���ʵ����ϸ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int ModifyUploadedFeeDetailInpatient(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList f)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.ModifyUploadedFeeDetailInpatient(patient, f);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// �޸Ķ���סԺ���ϴ���ϸ
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">Ҫ�޸ĵķ���ʵ����ϸ����</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int ModifyUploadedFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.ModifyUploadedFeeDetailsInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺҽ��Ԥ����
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int PreBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.PreBalanceInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺҽ����;����
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int MidBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.MidBalanceInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺҽ������
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int BalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.BalanceInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// סԺȡ������
        /// </summary>
        /// <param name="patient">סԺ�Ǽǻ�����Ϣ</param>
        /// <param name="feeDetails">Ҫȡ������Ļ��߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int CancelBalanceInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.CancelBalanceInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        #endregion

        #region IMedcareTranscation ��Ա

        /// <summary>
        /// �ӿ�����,��ʼ������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public long Connect()
        {
            if (this.pactCode == null)
            {
                this.errMsg = "��ͬ��λû�и�ֵ";

                return -1;
            }
            if (this.medcaredInterface == null)
            {
                int returnValue = this.GetInterfaceFromPact(this.pactCode);
                if (returnValue == -1)
                {
                    //this.errMsg = this.medcaredInterface.ErrMsg;
                    return -1;
                }
            }
            //{87DE75DB-BF2E-4f68-9C28-15D043C1D49E}
            //return this.medcaredInterface.Connect();
            long returnV = this.medcaredInterface.Connect();
            if (returnV < 0)
            {
                this.errMsg = this.ErrMsg;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �رսӿ����� ��շ���
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public long Disconnect()
        {
            if (this.pactCode == null)
            {
                this.errMsg = "��ͬ��λû�и�ֵ";

                return -1;
            }
            if (this.medcaredInterface == null)
            {
                int returnValue = this.GetInterfaceFromPact(this.pactCode);
                if (returnValue == -1)
                {
                    // this.errMsg = this.medcaredInterface.ErrMsg;
                    return -1;
                }
            }

            //{87DE75DB-BF2E-4f68-9C28-15D043C1D49E}
            //return this.medcaredInterface.Disconnect();
            long returnV = this.medcaredInterface.Disconnect();
            if (returnV < 0)
            {
                this.errMsg = this.ErrMsg;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// �ӿ��ύ����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public long Commit()
        {
            //{7E721E47-0F64-4b43-8B42-36926A96D9CB}
            #region �ɴ���-����
            //if (this.pactCode == null)
            //{
            //    this.errMsg = "��ͬ��λû�и�ֵ";

            //    return -1;
            //}
            //if (this.medcaredInterface == null)
            //{
            //    int returnValue = this.GetInterfaceFromPact(this.pactCode);
            //    if (returnValue == -1)
            //    {
            //        this.errMsg = this.medcaredInterface.ErrMsg;
            //        return -1;
            //    }
            //}

            //return this.medcaredInterface.Commit();
            #endregion

            ArrayList pactList = this.pactManager.QueryPactUnitAll();
            if (pactList == null)
            {
                this.errMsg = "���Һ�ͬ��λʧ��";
                return -1;
            }
            if (pactList.Count == 0)
            {
                this.errMsg = "��ͬ��λδά��";
                return -1;
            }
            for (int i = 0; i < pactList.Count; i++)
            {
                FS.HISFC.Models.Base.PactInfo nowPactCode = pactList[i] as FS.HISFC.Models.Base.PactInfo;

                IMedcare myMedcare = null;

                //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                if (MedcareInterfaceProxy.interfaceHash.ContainsKey(nowPactCode.ID))
                {
                    myMedcare = (IMedcare)MedcareInterfaceProxy.interfaceHash[nowPactCode.ID];
                    if (myMedcare != null)
                    {
                        if (myMedcare.Commit() < 0)
                        {
                            return -1;
                        }
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// �ӿڻع�����
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public long Rollback()
        {
            //{21105AAF-1A77-4614-9D13-C3F3233E8DE9}
            #region �ɴ���-����
            //if (this.pactCode == null)
            //{
            //    this.errMsg = "��ͬ��λû�и�ֵ";

            //    return -1;
            //}
            //if (this.medcaredInterface == null)
            //{
            //    int returnValue = this.GetInterfaceFromPact(this.pactCode);
            //    if (returnValue == -1)
            //    {
            //        this.errMsg = this.medcaredInterface.ErrMsg;
            //        return -1;
            //    }
            //}

            //return this.medcaredInterface.Rollback();
            #endregion

            ArrayList pactList = this.pactManager.QueryPactUnitAll();
            if (pactList == null)
            {
                this.errMsg = "���Һ�ͬ��λʧ��";
                return -1;
            }
            if (pactList.Count == 0)
            {
                this.errMsg = "��ͬ��λδά��";
                return -1;
            }
            int i = 0;
            for (i = 0; i < pactList.Count; i++)
            {
                FS.HISFC.Models.Base.PactInfo nowPactCode = pactList[i] as FS.HISFC.Models.Base.PactInfo;

                if (this.pactCode != nowPactCode.ID) continue;

                IMedcare myMedcare = null;

                //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
                if (MedcareInterfaceProxy.interfaceHash.ContainsKey(nowPactCode.ID))
                {
                    myMedcare = (IMedcare)MedcareInterfaceProxy.interfaceHash[nowPactCode.ID];
                    if (myMedcare != null)
                    {
                        if (myMedcare.Rollback() < 0)
                        {
                            this.errMsg = myMedcare.ErrMsg;
                            continue;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                }
            }

            return -1;
        }

        /// <summary>
        /// �ӿڿ�ʼ����������
        /// </summary>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public void BeginTranscation()
        {
            if (this.pactCode == null)
            {
                this.errMsg = "��ͬ��λû�и�ֵ";

                return;
            }
            if (this.medcaredInterface == null)
            {
                int returnValue = this.GetInterfaceFromPact(this.pactCode);
                if (returnValue == -1)
                {
                    //this.errMsg = this.medcaredInterface.ErrMsg;
                    return;
                }
            }

            this.medcaredInterface.BeginTranscation();
        }

        #endregion

        public int OpenAll()
        {
            //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
            foreach (DictionaryEntry d in MedcareInterfaceProxy.interfaceHash)
            {
                ((IMedcare)d.Value).Connect();
            }

            return 0;
        }

        public int CloseAll()
        {
            //{6CBD45BC-F6C7-4ae0-A338-2E251423B418}
            foreach (DictionaryEntry d in MedcareInterfaceProxy.interfaceHash)
            {
                ((IMedcare)d.Value).Disconnect();
            }

            return 0;
        }

        #region IMedcare ��Ա

        /// <summary>
        /// ȡ��סԺ�ǼǷ���
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int CancelRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.CancelRegInfoInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }
        /// <summary>
        /// ��Ժ�ٻ�
        /// </summary>
        /// <param name="patient">סԺ���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int RecallRegInfoInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.RecallRegInfoInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        /// <summary>
        /// ��Ժ�ǼǷ���
        /// </summary>
        /// <param name="patient">���߻�����Ϣʵ��</param>
        /// <returns>�ɹ� 1 ʧ�� -1</returns>
        public int LogoutInpatient(FS.HISFC.Models.RADT.PatientInfo patient)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.LogoutInpatient(patient);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        #endregion

        #region IMedcare ��Ա

        /// <summary>
        /// �����˺�
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public int CancelRegInfoOutpatient(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.CancelRegInfoOutpatient(r);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        #endregion



        #region IMedcare ��Ա

        /// <summary>
        /// �������ʱ�Ƿ������ϴ�
        /// </summary>
        /// <remarks>true �����ϴ� false �����ϴ�</remarks>       
        public bool IsUploadAllFeeDetailsOutpatient
        {
            get
            {
                if (this.medcaredInterface != null)
                {
                    return this.medcaredInterface.IsUploadAllFeeDetailsOutpatient;
                }

                return true;
            }
        }

        #endregion

        #region IMedcare ��Ա

        #region {AD6E49F9-7409-48b1-A297-73610F0072C7}

        public int QueryFeeDetailsInpatient(FS.HISFC.Models.RADT.PatientInfo patient, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(patient))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.QueryFeeDetailsInpatient(patient, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        public int QueryFeeDetailsOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -1;
                }

                int returnValue = this.medcaredInterface.QueryFeeDetailsOutpatient(r, ref feeDetails);
                if (returnValue <= 0)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;
            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -1;
            }
        }

        #endregion
        #endregion

        #region IMedcareExtend ��Ա
        // {293FDD11-FC10-4ceb-8E4C-1A4304F22592}
        public bool IsLocalProcess
        {
            get
            {
                if (this.medcaredInterface != null)
                {
                    if (this.medcaredInterface is FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend)
                    {
                        return (this.medcaredInterface as FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend).IsLocalProcess;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            set
            {
                if (this.medcaredInterface != null)
                {
                    if (this.medcaredInterface is FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend)
                    {
                        (this.medcaredInterface as FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend).IsLocalProcess = value;
                    }
                }
            }
        }

        /// <summary>
        /// HIS�ڲ�ҽ�����㣬���δʵ�ִ˷�����Ĭ�ϴ���ɹ�
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <param name="arlOther">������Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int LocalBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails, ArrayList arlOther)
        {
            int iRes = 1;
            this.errMsg = "";

            if (this.medcaredInterface != null)
            {
                if (this.medcaredInterface is FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend)
                {
                    iRes = (this.medcaredInterface as FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareExtend).LocalBalanceOutpatient(r, ref feeDetails, arlOther);
                    //if (iRes <= 0)
                    //{
                    //    this.errMsg = this.medcaredInterface.ErrMsg;
                    //}

                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                else
                {
                    //iRes = -1;
                    //this.errMsg = this.Description + "  ��֧�ֱ��ؽ��㣬�뵽�շѴ����㣡";
                }
            }

            return iRes;
        }
        #endregion

        #region IMedcareBuDan ��Ա

        /// <summary>
        /// HIS�ڲ�ҽ���������㣬
        /// </summary>
        /// <param name="r">���߹Һ���Ϣ</param>
        /// <param name="feeDetails">���߷�����Ϣ</param>
        /// <returns>-1 ʧ�� 0 û�м�¼ >=1 �ɹ�</returns>
        public int BdBalanceOutpatient(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            int iRes = 1;
            this.errMsg = "";

            if (this.medcaredInterface != null)
            {
                if (this.medcaredInterface is FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareBuDan)
                {
                    iRes = (this.medcaredInterface as FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareBuDan).BdBalanceOutpatient(r, ref feeDetails);
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
            }

            return iRes;
        }
        /// <summary>
        ///Ԥ������¼�籣�Ľ�����Ϣ
        /// </summary>
        /// <param name="r"></param>
        /// <param name="feeDetails"></param>
        /// <returns></returns>
        public int BalanceOutpatientAfterPreBalance(FS.HISFC.Models.Registration.Register r, ref ArrayList feeDetails)
        {
            int iRes = 1;
            this.errMsg = "";

            if (this.medcaredInterface != null)
            {
                if (this.medcaredInterface is FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareBuDan)
                {
                    iRes = (this.medcaredInterface as FS.HISFC.BizProcess.Interface.FeeInterface.IMedcareBuDan).BalanceOutpatientAfterPreBalance(r, ref feeDetails);
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
            }

            return iRes;
        }

        /// <summary>
        /// ɨҽ����
        /// </summary>
        /// <param name="r"></param>
        /// <param name="codeNum"></param>
        /// <returns></returns>
        public string GetCodeScanningVerification(FS.HISFC.Models.Registration.Register r, string codeNum)
        {

            string idnum = "";

            idnum = this.medcaredInterface.GetCodeScanningVerification(r, codeNum);

            return idnum;
        }
        #endregion


        #region IMedcare ��Ա

        /// <summary>
        /// �ж�ָ�����ﲡ���Ƿ����ܴ�ҽ��
        /// {199EF4E9-EF21-4067-97A7-9AA97AF74CDE}
        /// </summary>
        /// <param name="r"></param>
        /// <returns>
        /// 0���������ܾ�������ҽ���������ҵ����ޱ�����¼
        /// -1�����������ܾ�������ҽ������
        /// -2��ʧ��
        /// ����ֵ���������ܾ�������ҽ���������ҵ����б�����¼������ֵΪ���ձ���������
        /// </returns>
        public int QueryCanMedicare(FS.HISFC.Models.Registration.Register r)
        {
            try
            {
                if (!this.IsValid(r))
                {
                    return -2;
                }

                int returnValue = this.medcaredInterface.QueryCanMedicare(r);
                if (returnValue == -2 || returnValue == -1)
                {
                    this.errMsg = this.medcaredInterface.ErrMsg;
                }
                return returnValue;

            }
            catch (Exception e)
            {
                this.errMsg = e.Message;

                return -2;
            }
        }

        #endregion

        #region IMedcare ��Ա


        //public int CancelSIBalance(FS.HISFC.Models.RADT.PatientInfo patient, ref string error)
        //{
        //    return this.medcaredInterface.CancelSIBalance(patient, ref error);
        //}

        //public int CancelSIBeforeBalance(FS.HISFC.Models.RADT.PatientInfo patient, ref string error)
        //{
        //    return this.medcaredInterface.CancelSIBeforeBalance(patient, ref error);
        //}

        #endregion
    }
}


