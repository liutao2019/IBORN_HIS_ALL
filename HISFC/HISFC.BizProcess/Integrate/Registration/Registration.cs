using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizProcess.Integrate.Registration
{
    public class Registration : IntegrateBase
    {
        public Registration()
        {
        }

        #region ����

        /// <summary>
        /// �ҺŹ���ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Register registerManager = new FS.HISFC.BizLogic.Registration.Register();

        /// <summary>
        /// ר���Ű����
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.DoctSchema doctSchemaMgr = new FS.HISFC.BizLogic.Registration.DoctSchema();

        /// <summary>
        /// �Ű����
        /// </summary>
        private FS.HISFC.BizLogic.Registration.Schema schemaManager = new FS.HISFC.BizLogic.Registration.Schema();

        private FS.HISFC.BizLogic.Manager.Constant constManager = new FS.HISFC.BizLogic.Manager.Constant();

        /// <summary>
        /// �Һż���ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.RegLevel regLevelManager = new FS.HISFC.BizLogic.Registration.RegLevel();

        /// <summary>
        /// �Һż���ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.RegLvlFee regLvlFeeManager = new FS.HISFC.BizLogic.Registration.RegLvlFee();

        /// <summary>
        /// �������ҵ���
        /// </summary>
        protected FS.HISFC.BizLogic.Nurse.Assign assingManager = new FS.HISFC.BizLogic.Nurse.Assign();

        /// <summary>
        /// �ޱ����
        /// </summary>
        protected FS.HISFC.BizLogic.Registration.Noon noonManager = new FS.HISFC.BizLogic.Registration.Noon();

        /// <summary>
        /// ������Ϣ������
        /// </summary>
        private FS.HISFC.BizLogic.Nurse.Queue queueBizLogic = new FS.HISFC.BizLogic.Nurse.Queue();

        private FS.HISFC.BizLogic.Registration.Schema schemaBizLogic = new FS.HISFC.BizLogic.Registration.Schema();
        #endregion

        #region ������

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;
            doctSchemaMgr.SetTrans(trans);
            registerManager.SetTrans(trans);
            regLvlFeeManager.SetTrans(trans);
            regLevelManager.SetTrans(trans);
            assingManager.SetTrans(trans);
            noonManager.SetTrans(trans);
        }

        #endregion

        #region ����

        /// <summary>
        /// ����һ���Һż�¼
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        [Obsolete("����,ʹ��Insert(FS.HISFC.Models.Registration.Register register)", true)]
        public int InsertByDoct(FS.HISFC.Models.Registration.Register reg)
        {
            this.SetDB(registerManager);
            return registerManager.Insert(reg);
        }

        /// <summary>
        /// ����Һż�¼��{E43E0363-0B22-4d2a-A56A-455CFB7CF211}
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Registration.Register register)
        {
            this.SetDB(registerManager);
            return registerManager.Insert(register);
        }
        #endregion

        #region ����

        #region ���¿�����Ϣ

        /// <summary>
        /// ���¿������
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="seeDeptID"></param>
        /// <param name="seeDoctID"></param>
        /// <returns></returns>
        public int UpdateDept(string clinicID, string seeDeptID, string seeDoctID)
        {
            this.SetDB(registerManager);
            return registerManager.UpdateDept(clinicID, seeDeptID, seeDoctID);
        }

        /// <summary>
        /// ���¿���
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public int UpdateSeeDone(string clinicNO)
        {
            this.SetDB(registerManager);
            return registerManager.UpdateSeeDone(clinicNO);
        }

        /// <summary>
        /// �����չҺŷѱ�־
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="operID"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int UpdateAccountFeeState(string clinicID, string operID, string dept, DateTime operDate)
        {
            this.SetDB(registerManager);
            return registerManager.UpdateAccountFeeState(clinicID, operID, dept, operDate);
        }

        /// <summary>
        /// ���¿������
        /// </summary>
        /// <param name="Type">1ҽ�� 2���� 4ȫԺ</param>
        /// <param name="seeDate">��������</param>
        /// <param name="Subject">Type=1ʱ,ҽ������;Type=2,���Ҵ���;Type=4,ALL</param>
        /// <param name="noonID">���</param>
        /// <returns></returns>
        public int UpdateSeeNo(string Type, DateTime seeDate, string Subject, string noonID)
        {
            this.SetDB(registerManager);
            return registerManager.UpdateSeeNo(Type, seeDate, Subject, noonID);
        }

        #endregion

        #region �����ѿ�����շѱ��

        /// <summary>
        /// �����ѿ�����շѱ��
        /// ��Ҫ���ڲ��Һ�
        /// </summary>
        /// <param name="clinicCode">������ˮ��</param>
        /// <returns></returns>
        public int UpdateYNSeeAndCharge(string clinicCode)
        {
            this.SetDB(this.registerManager);
            return this.registerManager.UpdateYNSeeAndCharge(clinicCode);
        }

        #endregion

        #region ���Ļ�����Ϣʱ�����¹Һ���Ϣ

        /// <summary>
        /// �޸Ļ��߻�����Ϣʱ�����¹ҺŲ�����Ϣ ����clinicCode
        /// �������Ա����ա�����ߺš���ͬ��λ
        /// </summary>
        /// <param name="patientInfo">���߻�����Ϣʵ��</param>
        /// <returns></returns>
        public int UpdateRegInfoByClinicCode(FS.HISFC.Models.Registration.Register patientInfo)
        {
            this.SetDB(this.registerManager);
            return this.registerManager.UpdateRegInfoByClinicCode(patientInfo);
        }

        /// <summary>
        /// �޸Ļ��߻�����Ϣʱ�����¹Һ������Ϣ
        /// �������Ա����ա�����ߺš���ͬ��λ
        /// </summary>
        /// <param name="patientInfo">���߻�����Ϣʵ��</param>
        /// <returns></returns>
        public int UpdateRegByPatientInfo(FS.HISFC.Models.RADT.PatientInfo patientInfo)
        {
            this.SetDB(this.registerManager);
            return this.registerManager.UpdateRegByPatientInfo(patientInfo);
        }

        #endregion


        #region
        /// <summary>
        /// �����Ű���Ϣ���»�ʿվ������Ч��
        /// </summary>
        /// <param name="validFlag"></param>
        /// <param name="seeDate"></param>
        /// <param name="noonCode"></param>
        /// <param name="deptCode"></param>
        /// <param name="doctCode"></param>
        /// <returns></returns>
        public int UpdateQueueValid(string validFlag, string seeDate, string noonCode, string deptCode, string doctCode)
        {
            this.SetDB(this.queueBizLogic);
            return this.queueBizLogic.UpdateQueueValid(validFlag, seeDate, noonCode, deptCode, doctCode);
        }

        /// <summary>
        /// ���ݶ�����Чά���Ű���Ϣ
        /// </summary>
        /// <param name="validFlag"></param>
        /// <param name="seeDate"></param>
        /// <param name="noonCode"></param>
        /// <param name="deptCode"></param>
        /// <param name="doctCode"></param>
        /// <returns></returns>
        public int UpdateDoctSchemaValid(string validFlag, string seeDate, string noonCode, string deptCode, string doctCode,string reasonNo,string reasonName, string stopOpcd, DateTime stopDate)
        {
            this.SetDB(this.schemaBizLogic);
            return this.schemaBizLogic.UpdateDoctSchemaValid(validFlag, seeDate, noonCode, deptCode, doctCode,reasonNo, reasonName, stopOpcd, stopDate);
        }
        #endregion





        #endregion

        #region ��ѯ

        #region ��ѯ��������

        /// <summary>
        /// ȡ���ݿ�����ֵ����Ϊ���￨��
        /// </summary>
        /// <returns>����ֵ</returns>
        public int AutoGetCardNO()
        {
            this.SetDB(this.registerManager);

            return registerManager.AutoGetCardNO();
        }

        #region �Һż���

        /// <summary>
        /// ��ȡ������Ч�ĹҺż���
        /// </summary>
        /// <returns>�ɹ� ������Ч�ĹҺż��𼯺� ʧ�� null</returns>
        public ArrayList QueryRegLevel()
        {
            this.SetDB(regLevelManager);

            return regLevelManager.Query(true);
        }

        /// <summary>
        /// ��ȡ���еĹҺż��� �������Ƿ���Ч
        /// </summary>
        /// <returns>�ɹ� ���еĹҺż��𼯺� ʧ�� null</returns>
        public ArrayList QueryAllRegLevel()
        {
            this.SetDB(regLevelManager);

            return regLevelManager.Query();
        }

        /// <summary>
        /// ͨ����ͬ��λ,�͹Һż����ùҺŷ�
        /// </summary>
        /// <param name="pactCode">��ͬ��λ����</param>
        /// <param name="regLevel">�Һż���</param>
        /// <returns>�ɹ� �Һŷ�ʵ�� ʧ�� null</returns>
        public FS.HISFC.Models.Registration.RegLvlFee GetRegLevelByPactCode(string pactCode, string regLevel)
        {
            this.SetDB(regLvlFeeManager);

            return regLvlFeeManager.Get(pactCode, regLevel);
        }

        /// <summary>
        /// ��ѯһ���Һż���
        /// </summary>
        /// <param name="regLevelCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.RegLevel QueryRegLevelByCode(string regLevelCode)
        {
            this.SetDB(regLevelManager);
            return regLevelManager.Query(regLevelCode);
        }

        //���ݸ��ֿ��ţ���ѯ�Һż���

        #endregion

        #region ����ҽ��ְ����ѯ��Ӧ��������Ŀ

        /// <summary>
        /// ����ҽ��ְ����ȡ��Ӧ��������Ŀ
        /// </summary>
        /// <param name="doctRank"></param>
        /// <returns></returns>
        [Obsolete("����", true)]
        public FS.HISFC.Models.Fee.Item.Undrug GetDiagItemCodeByDoctRank(string doctRank)
        {
            this.SetDB(noonManager);
            string itemCode = this.registerManager.GetDiagItemCodeByDoctRank(doctRank);
            //������Null
            if (itemCode == null)
            {
                this.Err = this.registerManager.Err;
                return null;
            }
            //Ϊ�գ���ʾû�в鵽
            else if (string.IsNullOrEmpty(itemCode))
            {
                this.Err = "û��ά����ְ����Ӧ��������Ŀ��";
                return null;
            }
            else
            {
                FS.HISFC.BizProcess.Integrate.Fee feeMgr=new Fee();
                FS.HISFC.Models.Fee.Item.Undrug diagItem = feeMgr.GetItem(itemCode);
                if (diagItem == null || string.IsNullOrEmpty(diagItem.ID))
                {
                    this.Err = "��ѯ��ҩƷ��Ŀ��������[" + itemCode + "] " + feeMgr.Err;
                    return null;
                }

                return diagItem;
            }
        }
        #endregion

        /// <summary>
        /// ��ѯ���
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryNoon()
        {
            this.SetDB(noonManager);
            return noonManager.Query();
        }

        #endregion

        #region �Ű����

        /// <summary>
        /// ��ѯ�Ű���Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList Query()
        {
            this.SetDB(doctSchemaMgr);
            return doctSchemaMgr.Query();
        }

        /// <summary>
        /// ��ȡ�Ű���Ϣ
        /// {231D1A80-6BF6-413f-8BBF-727DC2BF47D9}
        /// </summary>
        /// <param name="doctCode">ҽ������</param>
        /// <param name="time">ʱ��</param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Schema GetSchema(string doctCode, DateTime time)
        {
            this.SetDB(schemaManager);
            return schemaManager.GetSchema(doctCode, time);
        }

        #endregion

        #region ����

        /// <summary>
        /// ���ѷ����־
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="operID"></param>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public int Update(string clinicID, string operID, DateTime operDate)
        {
            this.SetDB(registerManager);
            return registerManager.Update(clinicID, operID, operDate);
        }

        /// <summary>
        /// ȡ������״̬
        /// </summary>
        /// <param name="clinicID"></param>
        /// <returns></returns>
        public int CancelTriage(string clinicID)
        {
            this.SetDB(registerManager);
            return registerManager.CancelTriage(clinicID);
        }

        /// <summary>
        /// ��ѯ����һ��ʱ��δ�������Ч��
        /// </summary>
        /// <param name="cardNo">���￨��</param>
        /// <param name="limitDate">�ֺ�ʱ��</param>
        /// <returns></returns>
        public ArrayList QueryUnionNurse(string cardNo, DateTime limitDate)
        {
            this.SetDB(registerManager);
            return registerManager.QueryUnionNurse(cardNo, limitDate);
        }

        /// <summary>
        /// ��ѯ����һ��ʱ�����Ч��
        /// �ѷ����δ�����������������״̬��
        /// </summary>
        /// <param name="cardNo">���￨��</param>
        /// <param name="limitDate">�ֺ�ʱ��</param>
        /// <returns></returns>
        public ArrayList QueryUnionNurseTriage(string cardNo, DateTime limitDate)
        {
            this.SetDB(registerManager);
            return registerManager.QueryUnionNurseTriage(cardNo, limitDate);
        }

        /// <summary>
        /// ����������жϹҺ���Ϣ�Ƿ����
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public bool QueryIsTriage(string clinicNo)
        {
            this.SetDB(registerManager);
            return registerManager.QueryIsTriage(clinicNo);
        } 
        
        /// <summary>
        /// ����������жϻ����Ƿ��ڷ��������
        /// </summary>
        /// <param name="clinicNo">�����</param>
        /// <returns>���ڵ���1������������л���  0�� û��  -1:��ѯ����</returns>
        public int JudgeInQueue(string clinicNo)
        {
            this.SetDB(assingManager);
            return assingManager.JudgeInQueue(clinicNo);
        }

        #endregion

        #region ��ѯ�Һż�¼

        /// <summary>
        /// ��û��߿������
        /// </summary>
        /// <param name="Type">Type:1ר����š�2������š�4ȫԺ���</param>
        /// <param name="current">��������</param>
        /// <param name="subject">Type=1ʱ,ҽ������;Type=2,���Ҵ���;Type=4,ALL</param>
        /// <param name="noonID">���</param>
        /// <param name="seeNo">��ǰ�����</param>
        /// <returns></returns>
        public int GetSeeNo(string Type, DateTime current, string subject, string noonID, ref int seeNo)
        {
            this.SetDB(registerManager);
            return registerManager.GetSeeNo(Type, current, subject, noonID, ref seeNo);
        }

        #region �ظ�����...

        /// <summary>
        /// ��ѯ����һ��ʱ���ڹҵ���Ч��
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="limitDate"></param>
        /// <returns></returns>
        public ArrayList Query(string cardNo, DateTime limitDate)
        {
            this.SetDB(registerManager);
            return registerManager.Query(cardNo, limitDate);
        }

        /// <summary>
        /// ͨ�����￨�Ų�ѯһ��ʱ���ڵ���Ч�ĹҺŻ���
        /// </summary>
        /// <param name="cardNO">����</param>
        /// <param name="limitDate">��Ч�Ľ���ʱ��</param>
        /// <returns>�ɹ� ���عҺŻ��ߵļ��� ʧ�� ���� null û�в��ҵ����ݷ��� ArrayList.Count == 0</returns>
        public ArrayList QueryValidPatientsByCardNO(string cardNO, DateTime limitDate)
        {
            this.SetDB(registerManager);

            return registerManager.Query(cardNO, limitDate);
        }

        /// <summary>
        /// ���ղ����Ų�ѯһ��ʱ���ڵĹҺż�¼
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="valide">�Ƿ���Ч 1 ��Ч��0 �˷ѣ�2 ���ϣ� ���� ȫ����¼</param>
        /// <returns></returns>
        public ArrayList QueryRegList(string cardNo, DateTime beginDate, DateTime endDate, string valide)
        {
            this.SetDB(registerManager);

            return registerManager.QueryRegList(cardNo, beginDate, endDate, valide);
        }

        #endregion

        /// <summary>
        /// ���ݲ����Ų�ѯ�ѿ������Ч�Һ���Ϣ
        /// </summary>
        /// <param name="cardNO">������</param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        public ArrayList GetRegisterByCardNODate(string cardNO, DateTime beginDate, DateTime endDate)
        {
            this.SetDB(registerManager);
            return registerManager.GetRegisterByCardNODate(cardNO, beginDate, endDate);
        }

        /// <summary>
        /// ͨ��������Ų�ѯһ��ʱ���ڵ���Ч�ĹҺŻ���
        /// </summary>
        /// <param name="seeNO">�������</param>
        /// <param name="limitDate">��Ч�Ľ���ʱ��</param>
        /// <returns>�ɹ� ���عҺŻ��ߵļ��� ʧ�� ���� null û�в��ҵ����ݷ��� ArrayList.Count == 0</returns>
        public ArrayList QueryValidPatientsBySeeNO(string seeNO, DateTime limitDate)
        {
            this.SetDB(registerManager);

            return registerManager.QueryBySeeNo(seeNO, limitDate);
        }

        /// <summary>
        /// ͨ��������ѯ��Ч�ĹҺŻ���
        /// </summary>
        /// <param name="name">��������</param>
        /// <returns>�ɹ� ���عҺŻ��ߵļ��� ʧ�� ���� null û�в��ҵ����ݷ��� ArrayList.Count == 0</returns>
        public ArrayList QueryValidPatientsByName(string name)
        {
            this.SetDB(registerManager);

            return registerManager.QueryByName(name);
        }

        /// <summary>
        /// ��������ˮ�Ų�ѯ�Һ���Ϣ
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Register GetByClinic(string clinicNo)
        {
            this.SetDB(registerManager);
            return registerManager.GetByClinic(clinicNo);
        }

        /// <summary>
        /// ͨ��һ��ʱ���� ĳ����վ�ĹҺŻ���{F044FCF3-6736-4aaa-AA04-4088BB194C20}
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="myNurseDept">����վ����</param>
        /// <returns></returns>
        public ArrayList QueryNoTriagebyNurse(DateTime begin, string NurseID)
        {
            this.SetDB(registerManager);
            return registerManager.QueryNoTriagebyNurse(begin, NurseID);
        }

        /// <summary>
        /// ͨ��һ��ʱ���� ĳ����վ��Ӧ���ҵĹҺŻ��� addby sunxh
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="myNurseDept">����վ����</param>
        /// <returns></returns>
        public ArrayList QueryNoTriagebyDept(DateTime begin, string myNurseDept)
        {
            this.SetDB(registerManager);
            return registerManager.QueryNoTriagebyDept(begin, myNurseDept);
        }

        /// <summary>
        /// ͨ��һ��ʱ���� ĳ����վ��Ӧ���ҵĹҺŻ���δ���� addby sunxh
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="myNurseDept">����վ����</param>
        /// <returns></returns>
        public ArrayList QueryNoTriagebyDeptUnSee(DateTime begin, string myNurseDept)
        {
            this.SetDB(registerManager);
            return registerManager.QueryNoTriagebyDeptUnSee(begin, myNurseDept);
        }

        /// <summary>
        /// ���Һſ��Ҳ�ѯĳһ��ʱ���ڹҵ���Ч��
        /// </summary>
        /// <param name="deptID">���ұ���</param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryByDept(string deptID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            this.SetDB(registerManager);
            return registerManager.QueryByDept(deptID, beginDate, endDate, isSee);
        }

        /// <summary>
        /// {2888444F-50BA-4956-A5F7-D71F0C6448BB}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="doctID"></param>
        /// <param name="beginDate"></param>
        /// <returns></returns>
        public int QueryRegisterByCardNODoctTime(string cardNO, string deptID,string doctID, DateTime beginDate)
        {
            return this.registerManager.QueryRegisterByCardNODoctTime(cardNO, deptID,doctID, beginDate);
        }
 
        #region �����߷���ִ�п��Ҳ�ѯ�Һ���Ϣ
        /// <summary>
        /// �����߷���ִ�п��Ҳ�ѯ�Һ���Ϣ
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByFeeExcuDept(string excuDeptID, string beginDate, string endDate)
        {
            this.SetDB(registerManager);
            return registerManager.QueryRegisterByFeeExcuDept(excuDeptID, beginDate, endDate);
        }

        #endregion

        #region �����߷���ִ�п����뿨�Ų�ѯ�Һ���Ϣ
        /// <summary>
        /// �����߷���ִ�п��Ҳ�ѯ�Һ���Ϣ
        /// 
        /// {FCC85123-05D4-4baa-AB14-3DB983608766}
        /// </summary>
        /// <param name="excuDeptID"></param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByFeeExcuDeptAndCardNo(string excuDeptID, string beginDate, string endDate, string cardNo)
        {
            this.SetDB(registerManager);
            return registerManager.QueryRegisterByFeeExcuDeptAndCardNo(excuDeptID, beginDate, endDate, cardNo);
        }

        #endregion


        #region ̫���ظ�������...
        /// <summary>
        /// ���Һ�ҽ����ѯĳһ��ʱ���ڹҵ���Ч��
        /// </summary>
        /// <param name="doctID">ҽ������</param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryByDoct(string doctID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            this.SetDB(registerManager);
            return registerManager.QueryByDoct(doctID, beginDate, endDate, isSee);
        }
        /// <summary>
        /// ������ҽ����ѯĳһ��ʱ���ڹҵ���Ч��
        /// </summary>
        /// <param name="docID">ҽ������</param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeDoc(string docID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            this.SetDB(registerManager);
            return registerManager.QueryBySeeDoc(docID, beginDate, endDate, isSee);
        } 
        
        /// <summary>
        /// ������ҽ������ʱ���ѯĳһ��ʱ���ڵ���Ч��
        /// </summary>
        /// <param name="docID">ҽ������</param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeDocAndSeeDate(string docID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            this.SetDB(registerManager);
            return registerManager.QueryBySeeDocAndSeeDate(docID, beginDate, endDate, isSee);
        }

        /// <summary>
        /// ������ҽ������ʱ���ѯĳһ��ʱ���ڵ���Ч�ţ��ڹ���������¼����������ʱ��{A448C42B-AEA2-4a36-889C-C5AB97C38A6B}
        /// </summary>
        /// <param name="docID">ҽ������</param>
        /// <param name="beginDate"></param>
        /// <param name="endDate"></param>
        /// <param name="isSee"></param>
        /// <returns></returns>
        public ArrayList QueryBySeeDocAndSeeDate2(string docID, DateTime beginDate, DateTime endDate, bool isSee)
        {
            this.SetDB(registerManager);
            return registerManager.QueryBySeeDocAndSeeDate2(docID, beginDate, endDate, isSee);
        }

        #endregion

        #region ����  �����ظ���

        /// <summary>
        /// ������ˮ�Ų�ѯ����Һż�¼
        /// </summary>
        /// <param name="clincNo"></param>
        /// <param name="state">0 ��Ч��1 ��Ч������ ȫ��</param>
        /// <returns></returns>
        public ArrayList QueryPatientByState(string clincNo, string state)
        {
            this.SetDB(registerManager);
            return registerManager.QueryPatientByState(clincNo, state);
        }

        /// <summary>
        ///����ס������ˮ�Ų�ѯ�Һ���Ϣ
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(string clinicNO)
        {
            this.SetDB(registerManager);
            return registerManager.QueryPatient(clinicNO);
        }

        #endregion

        #endregion

        #region �����ж�

        /// <summary>
        /// ����������жϹҺ���Ϣ�Ƿ�����
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <returns></returns>
        public bool QueryIsCancel(string clinicNo)
        {
            this.SetDB(registerManager);
            return registerManager.QueryIsCancel(clinicNo);
        }

        /// <summary>
        /// �����Ƿ�Ժ��ְ�����������֤����
        /// </summary>
        /// <param name="IdenNO">����ߺ���</param>
        /// <returns></returns>
        public bool CheckIsEmployee(string IdenNO)
        {
            this.SetDB(registerManager);
            return registerManager.CheckIsEmployee(IdenNO);
        }

        /// <summary>
        /// �����Ƿ�Ժ��ְ�������ݹҺ���Ϣ
        /// </summary>
        /// <param name="register">�Һ���Ϣ</param>
        /// <returns></returns>
        public bool CheckIsEmployee(FS.HISFC.Models.Registration.Register register)
        {
            this.SetDB(registerManager);
            return registerManager.CheckIsEmployee(register);
        }

        #endregion

        #region ��������

        /// <summary>
        /// ����ʿվ����Ժ״̬��ѯ�������ۻ���
        /// </summary>
        /// <param name="nursecellcode">��ʿվ����</param>
        /// <param name="status">����������Ա״̬</param>
        /// <returns>nullΪ��</returns>
        public ArrayList PatientQueryByNurseCell(string nursecellcode, string status)
        {
            this.SetDB(registerManager);
            return registerManager.PatientQueryByNurseCell(nursecellcode, status);
        } 
        
        /// <summary>
        /// ҽ��վ�������ۻ���
        /// </summary>
        /// <param name="nursecellcode">��ʿվ����</param>
        /// <param name="status">����������Ա״̬</param>
        /// <returns>nullΪ��</returns>
        public ArrayList PatientQueryByNurseCell(string deptCode)
        {
            this.SetDB(registerManager);
            return registerManager.PatientQueryByNurseCell(deptCode);
        }

        #endregion

        #region ���չҺŷ�

        /// <summary>
        /// ��������б�
        /// </summary>
        ArrayList alOrdinaryRegDept = null;

        /// <summary>
        /// ��ͨ�����Ӧ�ĹҺż���
        /// </summary>
        ArrayList alOrdinaryLevl = null;

        /// <summary>
        /// ��ȡ����ų���
        /// </summary>
        ArrayList emergRegLevlList = null;

        /// <summary>
        /// ͨ���������ҡ�ҽ��ְ����ȡ��Ӧ�ĹҺż���
        /// </summary>
        /// <param name="recipeDept"></param>
        /// <param name="doctLevl"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.RegLevel GetRegLevl(string recipeDept, string doctCode, string doctLevl)
        {
            #region �Һż���

            string regLevl = "";

            //�Ƿ��������
            bool isOrdinaryRegDept = false;

            #region ����Һſ���

            if (alOrdinaryRegDept == null)
            {
                alOrdinaryRegDept = new ArrayList();
                FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();
                alOrdinaryRegDept = conManager.GetList("OrdinaryRegLevlDept");
                if (alOrdinaryRegDept == null)
                {
                    Err = "��ȡ����Һſ���ʧ�ܣ�" + conManager.Err;
                    return null;
                }
            }

            foreach (FS.HISFC.Models.Base.Const constObj in alOrdinaryRegDept)
            {
                if (constObj.IsValid && constObj.ID.Trim() == recipeDept)
                {
                    isOrdinaryRegDept = true;
                    break;
                }
            }

            #endregion

            //����
            if (isOrdinaryRegDept)
            {
                if (alOrdinaryLevl == null)
                {
                    alOrdinaryLevl = new ArrayList();

                    FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();
                    alOrdinaryLevl = conManager.GetList("OrdinaryRegLevel");
                    if (alOrdinaryLevl == null || alOrdinaryLevl.Count == 0)
                    {
                        Err = "��ȡ��ͨ�����Ӧ�ĹҺż������" + conManager.Err;
                        return null;
                    }
                }

                foreach (FS.HISFC.Models.Base.Const constObj in alOrdinaryLevl)
                {
                    if (constObj.IsValid)
                    {
                        regLevl = constObj.ID.Trim();
                        break;
                    }
                }
            }
            else
            {
                //�Ƿ���
                bool isEmerg = this.IsEmergency(recipeDept);

                string diagItemCode = "";
                if (isEmerg)
                {
                    if (emergRegLevlList == null)
                    {
                        emergRegLevlList = new ArrayList();
                        FS.HISFC.BizLogic.Manager.Constant conManager = new FS.HISFC.BizLogic.Manager.Constant();
                        emergRegLevlList = conManager.GetList("EmergencyLevel");
                        if (emergRegLevlList == null || emergRegLevlList.Count == 0)
                        {
                            Err = "��ȡ�����ʧ�ܣ�" + conManager.Err;
                            return null;
                        }
                    }

                    if (emergRegLevlList.Count > 0)
                    {
                        regLevl = ((FS.FrameWork.Models.NeuObject)emergRegLevlList[0]).ID.Trim();
                    }
                    if (string.IsNullOrEmpty(regLevl))
                    {
                        Err = "��ȡ����Ŵ�������ϵ��Ϣ�ƣ�";
                        return null;
                    }
                }
                else
                {
                    if (this.GetSupplyRegInfo(doctCode, doctLevl, recipeDept, ref regLevl, ref diagItemCode) == -1)
                    {
                        return null;
                    }
                }
            }

            FS.HISFC.Models.Registration.RegLevel regLevlObj = this.regLevelManager.Query(regLevl);
            if (regLevlObj == null)
            {
                Err = "��ѯ�Һż�����󣬱���[" + regLevl + "]������ϵ��Ϣ������ά��" + Err;
                return null;
            }

            return regLevlObj;
            #endregion
        }

        /// <summary>
        /// ���ݹҺż����ȡ������Ŀ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="regLevl">�Һż������</param>
        /// <param name="diagItemCode">������Ŀ</param>
        /// <returns></returns>
        public int GetSupplyRegInfo(string deptCode, string regLevl, ref string diagItemCode)
        {
            return registerManager.GetSupplyRegInfo(deptCode, regLevl, ref diagItemCode);
        }

        /// <summary>
        /// ���ݹҺż����ȡ������Ŀ
        /// </summary>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="operLevel">ҽ��ְ������</param>
        /// <param name="regLevl">�Һż������</param>
        /// <param name="diagItemCode">������Ŀ</param>
        /// <returns></returns>
        public int GetSupplyRegInfo(string deptCode, string operLevel, string regLevl, ref string diagItemCode)
        {
            return registerManager.GetSupplyRegInfo(deptCode, operLevel, regLevl, ref diagItemCode);
        }

        /// <summary>
        /// ����ҽ��ְ����ȡ���ڵĹҺż��������
        /// </summary>
        /// <param name="doctLevl">ҽ��ְ������</param>
        /// <param name="deptCode">���ұ���</param>
        /// <param name="regLevl">�Һż������</param>
        /// <param name="diagItemCode">������Ŀ</param>
        /// <returns></returns>
        public int GetSupplyRegInfo(string doctCode, string doctLevl, string deptCode, ref string regLevl, ref string diagItemCode)
        {
            return registerManager.GetSupplyRegInfo(doctCode, doctLevl, deptCode, ref regLevl, ref diagItemCode);

            //FS.FrameWork.Models.NeuObject constObj = this.constManager.GetConstant("DOCLEVEL_REGLEVEL", doctLevl);
            //if (constObj == null || string.IsNullOrEmpty(constObj.ID))
            //{
            //    this.Err = "��ȡְ����Ӧ�ĹҺż������" + this.constManager.Err;
            //    return -1;
            //}
            //regLevl = constObj.Name.Trim();

            //constObj = this.constManager.GetConstant("REGLEVEL_DIAGFEE", regLevl);
            //if (constObj == null || string.IsNullOrEmpty(constObj.ID))
            //{
            //    this.Err = "��ȡְ����Ӧ��������Ŀ����" + this.constManager.Err;
            //    return -1;
            //}
            //diagItemCode = constObj.Name.Trim();

            //return 1;
        }

        /// <summary>
        /// �������
        /// </summary>
        static FS.FrameWork.Public.ObjectHelper emergencyDeptHelper = null;

        /// <summary>
        /// �Ǽ���ʱ���
        /// </summary>
        static FS.FrameWork.Public.ObjectHelper noEmergencyTimeHelper = null;

        /// <summary>
        /// �����ڼ���
        /// </summary>
        static FS.FrameWork.Public.ObjectHelper holidayHelper = null;

        DateTime begin1 = DateTime.MinValue;
        DateTime end1 = DateTime.MinValue;
        DateTime begin2 = DateTime.MinValue;
        DateTime end2 = DateTime.MinValue;

        /// <summary>
        /// �����Ƿ��Ǽ���ķ�ʽ������ֻ����ʯ��ÿ���жϵ�
        /// </summary>
        private string CheckEmergencyType;

        /// <summary>
        /// �Ƿ�����
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public bool IsEmergency(string deptCode)
        {
            DateTime now = this.registerManager.GetDateTimeFromSysDateTime();
            return this.IsEmergency(deptCode, now);
        }

        /// <summary>
        /// �ж������Ƿ�����
        /// </summary>
        /// <param name="deptCode">��������</param>
        /// <param name="operDate">����ʱ��</param>
        /// <returns></returns>
        public bool IsEmergency(string deptCode,DateTime operDate)
        {
            if (string.IsNullOrEmpty(this.CheckEmergencyType))
            {
                Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                this.CheckEmergencyType = controlMgr.GetControlParam<string>("HNGH01", true, "0");
            }

            return this.CheckIsEmergency(deptCode, operDate, CheckEmergencyType, true);
        }

        /// <summary>
        /// �Ƿ�����ң��ڼ��ջ���ĩ
        /// </summary>
        /// <param name="operDate"></param>
        /// <returns></returns>
        public bool IsEmergencyHolidays(string deptCode, DateTime operDate)
        {
            if (string.IsNullOrEmpty(this.CheckEmergencyType))
            {
                Common.ControlParam controlMgr = new FS.HISFC.BizProcess.Integrate.Common.ControlParam();
                this.CheckEmergencyType = controlMgr.GetControlParam<string>("HNGH01", true, "0");
            }

            return this.CheckIsEmergency(deptCode, operDate, CheckEmergencyType, false);
        }

        /// <summary>
        /// �ж������Ƿ�����
        /// </summary>
        /// <param name="deptCode">��������</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="checkEmergencyType">����ʱ��</param>
        /// <returns></returns>
        private bool CheckIsEmergency(string deptCode, DateTime operDate, string checkEmergencyType, bool isCheckTime)
        {
            if (checkEmergencyType == "1")
            {
                try
                {
                    string dateNow = operDate.ToString("yyyy-MM-dd");
                    ArrayList al = new ArrayList();

                    #region ��������ж�

                    if (emergencyDeptHelper == null)
                    {
                        al = this.constManager.GetList("EmergencyDept");
                        if (al == null)
                        {
                            this.Err = constManager.Err;
                        }
                        else
                        {
                            emergencyDeptHelper = new FS.FrameWork.Public.ObjectHelper(al);
                        }
                    }
                    if (emergencyDeptHelper != null)
                    {
                        if (emergencyDeptHelper.GetObjectFromID(deptCode) != null)
                        {
                            return true;
                        }
                    }

                    #endregion

                    #region �ڼ����ж�

                    if (holidayHelper == null)
                    {
                        al = this.constManager.GetList("Holiday");
                        if (al == null)
                        {
                            this.Err = constManager.Err;
                        }
                        else
                        {
                            holidayHelper = new FS.FrameWork.Public.ObjectHelper(al);
                        }
                    }
                    if (holidayHelper != null)
                    {
                        if (holidayHelper.GetObjectFromID(operDate.ToString("yyyy-MM-dd")) != null)
                        {
                            return true;
                        }
                    }

                    #endregion

                    #region ��ĩ�ж�
                    //ֻҪά�������� ����Ϊ��ĩ�������ﴦ��

                    //���ά���˳���������Ϊ��Ҫ�ж�
                    //al = this.constManager.GetList("WeekendIsEmergency");

                    //if (al.Count > 0)
                    //{
                    //    if (operDate.DayOfWeek.ToString().ToUpper() == "Saturday".ToUpper())
                    //    {
                    //        return true;
                    //    }
                    //    else if (operDate.DayOfWeek.ToString().ToUpper() == "Sunday".ToUpper())
                    //    {
                    //        return true;
                    //    }
                    //}
                    #endregion

                    if (isCheckTime)
                    {
                        #region ʱ����ж�

                        //name������
                        //memo�ǿ�ʼʱ��
                        //usercode�ǽ���ʱ��

                        if (noEmergencyTimeHelper == null)
                        {
                            al = this.constManager.GetList("EmergencyTimeSet");
                            if (al == null)
                            {
                                this.Err = constManager.Err;
                            }
                            else
                            {
                                noEmergencyTimeHelper = new FS.FrameWork.Public.ObjectHelper(al);
                            }
                        }

                        foreach (FS.HISFC.Models.Base.Const constObj in noEmergencyTimeHelper.ArrayObject)
                        {
                            if (constObj.Name.Trim() == this.GetDayOfWeek(operDate))
                            {
                                begin1 = FS.FrameWork.Function.NConvert.ToDateTime(dateNow + " " + constObj.Memo);
                                end1 = FS.FrameWork.Function.NConvert.ToDateTime(dateNow + " " + constObj.UserCode);


                                if (operDate >= begin1 && operDate <= end1)
                                {
                                    return false;
                                }
                            }
                        }
                        return true;

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return false;
                }

                return false;
            }
            else
            {

                try
                {
                    string dateNow = operDate.ToString("yyyy-MM-dd");
                    ArrayList al = new ArrayList();

                    #region ��������ж�

                    if (emergencyDeptHelper == null)
                    {
                        al = this.constManager.GetList("EmergencyDept");
                        if (al == null)
                        {
                            this.Err = constManager.Err;
                        }
                        else
                        {
                            emergencyDeptHelper = new FS.FrameWork.Public.ObjectHelper(al);
                        }
                    }
                    if (emergencyDeptHelper != null)
                    {
                        if (emergencyDeptHelper.GetObjectFromID(deptCode) != null)
                        {
                            return true;
                        }
                    }

                    #endregion

                    #region �ڼ����ж�

                    #region ��ĩ�ж�
                    //ֻҪά�������� ����Ϊ��ĩ�������ﴦ��
                    //���ά���˳���������Ϊ��Ҫ�ж�
                    al = this.constManager.GetList("WeekendIsEmergency");

                    if (al.Count > 0)
                    {
                        if (operDate.DayOfWeek.ToString().ToUpper() == "Saturday".ToUpper())
                        {
                            return true;
                        }
                        else if (operDate.DayOfWeek.ToString().ToUpper() == "Sunday".ToUpper())
                        {
                            return true;
                        }
                    }
                    #endregion

                    #region �����ж�

                    if (holidayHelper == null)
                    {
                        al = this.constManager.GetList("Holiday");
                        if (al == null)
                        {
                            this.Err = constManager.Err;
                        }
                        else
                        {
                            holidayHelper = new FS.FrameWork.Public.ObjectHelper(al);
                        }
                    }
                    if (holidayHelper != null)
                    {
                        if (holidayHelper.GetObjectFromID(operDate.ToString("yyyy-MM-dd")) != null)
                        {
                            return true;
                        }
                    }

                    #endregion

                    #endregion

                    if (isCheckTime)
                    {
                        #region ʱ����ж�

                        if (noEmergencyTimeHelper == null)
                        {
                            al = this.constManager.GetList("EmergencyTime");
                            if (al == null)
                            {
                                this.Err = constManager.Err;
                            }
                            else
                            {
                                noEmergencyTimeHelper = new FS.FrameWork.Public.ObjectHelper(al);
                            }
                        }

                        if (noEmergencyTimeHelper != null)
                        {
                            //4��ʱ��������ȷ��ʱ���
                            if (noEmergencyTimeHelper.ArrayObject.Count == 4)
                            {
                                if (begin1 == DateTime.MinValue || begin1.ToString("yyyy-MM-dd") != dateNow)
                                {
                                    begin1 = FS.FrameWork.Function.NConvert.ToDateTime(dateNow + " " + noEmergencyTimeHelper.GetName("1"));
                                }
                                if (end1 == DateTime.MinValue || end1.ToString("yyyy-MM-dd") != dateNow)
                                {
                                    end1 = FS.FrameWork.Function.NConvert.ToDateTime(dateNow + " " + noEmergencyTimeHelper.GetName("2"));
                                }
                                if (begin2 == DateTime.MinValue || begin2.ToString("yyyy-MM-dd") != dateNow)
                                {
                                    begin2 = FS.FrameWork.Function.NConvert.ToDateTime(dateNow + " " + noEmergencyTimeHelper.GetName("3"));
                                }
                                if (end2 == DateTime.MinValue || end2.ToString("yyyy-MM-dd") != dateNow)
                                {
                                    end2 = FS.FrameWork.Function.NConvert.ToDateTime(dateNow + " " + noEmergencyTimeHelper.GetName("4"));
                                }

                                if (!(operDate >= begin1 && operDate <= end1 || operDate >= begin2 && operDate <= end2))
                                {
                                    return true;
                                }
                            }
                        }

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return false;
                }

                return false;
            }
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private string GetDayOfWeek(DateTime date)
        {
            switch (date.DayOfWeek.ToString().ToUpper())
            {
                case "MONDAY":
                    return "����һ";
                    break;
                case "TUESDAY":
                    return "���ڶ�";
                    break;
                case "WEDNESDAY":
                    return "������";
                    break;
                case "THURSDAY":
                    return "������";
                    break;
                case "FRIDAY":
                    return "������";
                    break;
                case "SATURDAY":
                    return "������";
                    break;
                case "SUNDAY":
                    return "������";
                    break;
                default:
                    return null;
                    break;
            }
        }

        #endregion

        #endregion

        #region ҵ����

        //public int Get

        #endregion
    }

    #region ����

    ///// <summary>
    ///// �Һ�Ʊ��ӡ ͳһ�ŵ�HISFC.BizProcess.Interface��
    ///// </summary>
    //public interface IRegPrint
    //{
    //     ///<summary>
    //     ///���ݿ�����
    //     ///</summary>
    //    System.Data.IDbTransaction Trans
    //    {
    //        get;
    //        set;
    //    }
    //    /// <summary>
    //    /// ��ֵ
    //    /// </summary>
    //    /// <param name="register"></param>
    //    /// <param name="reglvlfee"></param>
    //    /// <returns></returns>

    //    int SetPrintValue(FS.HISFC.Models.Registration.Register register);

    //    /// <summary>
    //    /// ��ӡԤ��
    //    /// </summary>
    //    /// <returns>>�ɹ� 1 ʧ�� -1</returns>
    //    int PrintView();
    //    /// <summary>
    //   /// ��ӡ
    //   /// </summary>
    //    /// <returns>�ɹ� 1 ʧ�� -1</returns>

    //    int Print();

    //    /// <summary>
    //    /// ��յ�ǰ��Ϣ
    //    /// </summary>
    //    /// <returns>�ɹ� 1 ʧ�� -1</returns>
    //    int Clear();

    //    /// <summary>
    //    /// ���ñ������ݿ�����
    //    /// </summary>
    //    /// <param name="trans">���ݿ�����</param>
    //    void SetTrans(System.Data.IDbTransaction trans);
    //}
    ///// <summary>
    ///// �Һ�Ʊ��ӡ
    ///// </summary>
    //public interface IShowLED
    //{
    //    ///<summary>
    //    ///���ݿ�����
    //    ///</summary>
    //    //System.Data.IDbTransaction Trans
    //    //{
    //    //    get;
    //    //    set;
    //    //}
    //    /// <summary>
    //    /// ����
    //    /// </summary>
    //    /// <param name="register"></param>
    //    /// <param name="reglvlfee"></param>
    //    /// <returns></returns>

    //    string  Query();

      
    //    /// <summary>
    //    /// ��ʾfarpoint��ʽ
    //    /// </summary>
    //    /// <returns>�ɹ� 1 ʧ�� -1</returns>

    //    int SetFPFormat();

    //    /// <summary>
    //    ///  ����LED �ӿ� �����ʾ����LED
    //    /// </summary>
    //    /// <returns>�ɹ� 1 ʧ�� -1</returns>
    //    int CreateString();

    //    /// <summary>
    //    /// ���ñ������ݿ�����
    //    /// </summary>
    //    /// <param name="trans">���ݿ�����</param>
    //    //void SetTrans(System.Data.IDbTransaction trans);
    //}
    #endregion
}
