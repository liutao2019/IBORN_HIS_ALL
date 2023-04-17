using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Neusoft.HISFC.Management.PhysicalExamination.Enum;
namespace Neusoft.HISFC.Integrate.PhysicalExamination
{
    class Register : IntegrateBase
    {
        #region ����
        //���Ǽǹ�����
        protected static Neusoft.HISFC.Management.PhysicalExamination.Register mgrReg = new Neusoft.HISFC.Management.PhysicalExamination.Register();
        #endregion

        #region ������Ϣ
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="trans"></param>
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;
            mgrReg.SetTrans(trans);
        }
        #endregion 

        #region ��ѯһ��ʱ���������Ա��Ϣ ���� ��̬����
        /// <summary>
        /// ��ѯһ��ʱ���������Ա��Ϣ ���� ��̬����
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(string beginTime, string endTime)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryPatient(beginTime, endTime);
        }
        #endregion

        #region ���ݿ��Ż�ȡ���˻�����Ϣ  ע�ⲻ�ǵǼ���Ϣ
        /// <summary>
        /// ���ݿ��Ż�ȡ���˻�����Ϣ  ע�ⲻ�ǵǼ���Ϣ 
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public ArrayList QueryPatient(string cardNo)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryPatient(cardNo);
        }
        #endregion

        #region ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet
        /// <summary>
        /// ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet 
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int QueryPatient(string beginTime, string endTime, ref System.Data.DataSet ds)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryPatient(beginTime, endTime, ref ds);
        }
        #endregion

        #region  ���ӻ��޸�һ������
        /// <summary>
        /// ���ӻ��޸�һ������
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        public int AddOrUpdate(Neusoft.HISFC.Object.PhysicalExamination.Register register)
        {
            this.SetDB(mgrReg);
            return mgrReg.AddOrUpdate(register);
        }
        #endregion

        #region  ɾ��һ������
        /// <summary>
        /// ɾ��һ������
        /// </summary>
        /// <param name="register">���Ǽ�ʵ��</param>
        /// <returns></returns>
        protected int DeleteInfo(Neusoft.HISFC.Object.PhysicalExamination.Register register)
        {
            this.SetDB(mgrReg);
            return mgrReg.DeleteInfo(register);
        }
        #endregion
        #endregion

        #region ��Ա��Ϣ�Ǽ�
        #region ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet
        /// <summary>
        /// ��ȡĳ��ʱ����ڵ������Ա��Ϣ ���� DataSet 
        /// </summary>
        /// <param name="BeginTime"></param>
        /// <param name="EndTime"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetRegisterPatient(string beginTime, string endTime, ref System.Data.DataSet ds)
        {
            this.SetDB(mgrReg);
            return mgrReg.GetRegisterPatient(beginTime, endTime, ref ds);
        }
        #endregion

        #region �������ѯ�����Ա��Ϣ
        /// <summary>
        /// �������ѯ�����Ա��Ϣ
        /// </summary>
        /// <param name="ID">���� �򽡿�������,��쵥λ,���� </param>
        /// <param name="ds"></param>
        /// <param name="type">����</param>
        /// <returns></returns>
        public int GetRegisterPatient(string beginTime, string endTime, string ID, ref System.Data.DataSet ds, ExamType type)
        {
            this.SetDB(mgrReg);
            return mgrReg.GetRegisterPatient(beginTime, endTime, ID, ref ds, type);
        }
        #endregion

        #region �������Ż�ȡ���Ǽ���Ϣ
        /// <summary>
        /// �������Ż�ȡ���Ǽ���Ϣ
        /// </summary>
        /// <param name="ClinicNO">����</param>
        /// <returns></returns>
        public Neusoft.HISFC.Object.PhysicalExamination.Register GetRegisterByClinicNO(string clinicNO)
        {
            this.SetDB(mgrReg);
            return mgrReg.GetRegisterByClinicNO(clinicNO);
        }
        #endregion

        #region ���ݿ��Ų�ѯ
        /// <summary>
        /// ���ݿ��Ų�ѯ�Ǽ���Ϣ
        /// </summary>
        /// <param name="CardNo"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByCardNO(string cardNo)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryRegisterByCardNO(cardNo);
        }
        /// <summary>
        /// ���ݿ��Ż�ȡ�������ͼ������Ǽ���Ա��Ϣ ������
        /// </summary>
        /// <param name="CollectivityCode">��������</param>
        /// <param name="CardNo">����</param>
        /// <returns></returns>
        public ArrayList QueryCollectivityRegisterByCardNO(string cardNo)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryCollectivityRegisterByCardNO(cardNo);
        }
        #endregion

        #region ���ݽ��������Ų�ѯ�Ǽ���Ϣ
        /// <summary>
        /// ���ݽ��������Ų�ѯ�Ǽ���Ϣ
        /// </summary>
        /// <param name="ChkNO"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByArchivesNO(string archivesNO)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryRegisterByArchivesNO(archivesNO);
        }
        #endregion

        #region ���ӻ����ĳ������
        /// <summary>
        /// ���ӻ����ĳ������
        /// </summary>
        /// <param name="Register"></param>
        /// <returns></returns>
        public int AddOrUpdateRegister(Neusoft.HISFC.Object.PhysicalExamination.Register register)
        {
            this.SetDB(mgrReg);
            return mgrReg.AddOrUpdateRegister(register);
        }
        #endregion

        #region ɾ��һ������
        /// <summary>
        /// ���������ˮ�� ɾ��һ������
        /// </summary>
        /// <param name="ClinicNo"></param>
        /// <returns></returns>
        public int DeleteInfoRegister(string clinicNo)
        {
            this.SetDB(mgrReg);
            return mgrReg.DeleteInfoRegister(clinicNo);
        }

        #endregion

        #region ��ȡ �������
        /// <summary>
        /// ��ȡ��������¼
        /// </summary>
        /// <param name="strCompCode"></param>
        /// <returns></returns>
        public ArrayList QueryCollectivity(string compCode)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryCollectivity(compCode);
        }
        #endregion

        #region ���ݼ������Ż�ȡ�������Ա��Ϣ
        /// <summary>
        /// ���ݼ������Ż�ȡ�������Ա��Ϣ
        /// </summary>
        /// <param name="CollectivityCode"></param>
        /// <returns></returns>
        public ArrayList QueryRegisterByCollectivityCode(string collectivityCode)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryRegisterByCollectivityCode(collectivityCode);
        }
        #endregion

        #region ���ݼ������Ż�ȡ�������Ա��Ϣ
        /// <summary>
        /// ���ݼ������Ż�ȡ�������Ա��Ϣ
        /// </summary>
        /// <param name="CollectivityCode"></param>
        /// <returns></returns>
        public ArrayList QueryCompanyByCollectivityCode(string collectivityCode)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryCompanyByCollectivityCode(collectivityCode);
        }
        #endregion

        #endregion

        #region ������Ϣ
        #region  ���������ϸ��ȷ���ˣ�ȷ���¼�
        /// <summary>
        /// ���������ϸ��ȷ���ˣ�ȷ���¼�
        /// </summary>
        /// <param name="obj">Ҫȷ�ϵ�ʵ��</param>
        /// <returns></returns>
        public int UpdateConfirmInfo(Neusoft.HISFC.Object.PhysicalExamination.ItemList obj)
        {
            this.SetDB(mgrReg);
            return mgrReg.UpdateConfirmInfo(obj);
        }
        /// <summary>
        /// ����ȷ������
        /// </summary>
        /// <param name="moOrder"></param>
        /// <param name="?"></param>
        /// <returns></returns>
        public int UpdateConfirmAmount(string moOrder, decimal confirmNum)
        {
            this.SetDB(mgrReg);
            return mgrReg.UpdateConfirmAmount(moOrder, confirmNum);
        }
                #endregion

        #region  ��ȡ�����ϸ
        /// <summary>
        /// ��ȡ�����ϸ
        /// </summary>
        /// <param name="ClinicNo"></param>
        /// <returns></returns>
        public ArrayList QueryItemListByClinicNO(string clinicNO)
        {
            this.SetDB(mgrReg);
            return mgrReg.QueryItemListByClinicNO(clinicNO);
        }
        /// <summary>
        /// ������ˮ�Ż�ȡ�����Ŀ��ϸ
        /// </summary>
        /// <param name="SequenceNo"></param>
        /// <returns></returns>
        public Neusoft.HISFC.Object.PhysicalExamination.ItemList GetItemListBySequence(string sequenceNO)
        {
            this.SetDB(mgrReg);
            return mgrReg.GetItemListBySequence(sequenceNO);
        }
        #endregion

        #region ɾ��ĳһ�������ϸ
        /// <summary>
        /// ĳһ�������ϸ
        /// </summary>
        /// <param name="SeqenceNo"></param>
        /// <returns></returns>
        public int DeleteItemListBySeqenceNO(string seqenceNO)
        {
            this.SetDB(mgrReg);
            return mgrReg.DeleteItemListBySeqenceNO(seqenceNO);
        }
        #endregion
        #endregion 
    }
}
