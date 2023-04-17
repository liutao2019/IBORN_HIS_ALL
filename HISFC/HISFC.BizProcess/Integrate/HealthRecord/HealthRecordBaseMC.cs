using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizProcess.Integrate.HealthRecord
{
    /// <summary>
    /// ������<br>HealthRecordBaseMC</br>
    /// <Font color='#FF1111'>ҽ�������Ϣ¼�롢��ѯ��</Font><br></br>
    /// [�� �� ��: ]<br>������</br>
    /// [����ʱ��: ]<br>2007-08-13</br>
    /// <�޸ļ�¼ 
    ///		�޸���='' 
    ///		�޸�ʱ��='yyyy-mm-dd' 
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///		/>
    /// </summary>
    public class HealthRecordBaseMC : IntegrateBase
    {
        	/// <summary>
	/// ���캯��
	/// </summary>
        public HealthRecordBaseMC()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ����

        #region ˽��
        #endregion

        #region ����

        protected static FS.HISFC.BizLogic.HealthRecord.ICDMedicare icdMgr = new FS.HISFC.BizLogic.HealthRecord.ICDMedicare();
        protected static FS.HISFC.BizLogic.HealthRecord.DiagnoseMedicare diagMgr = new FS.HISFC.BizLogic.HealthRecord.DiagnoseMedicare();

        #endregion

        #region ����
        #endregion

        #endregion

        #region ����
        #endregion

        #region ����

        #region ˽��
        #endregion

        #region ����
        #endregion

        #region ����

        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;
            icdMgr.SetTrans(trans);
            diagMgr.SetTrans(trans);
        }
        /// <summary>
        /// ��ѯҽ��ICD
        /// </summary>
        /// <param name="dType">��ѯ��� 0 ȫ����1 ICD10��2 ��ҽ����3 ʡҽ��</param>
        /// <returns></returns>
        public ArrayList ICDQueryMc(String dType)
        {
            this.SetDB(icdMgr);
            return icdMgr.Query(dType);
        } 
        /// <summary>
        /// ���벡�������Ϣ
        /// </summary>
        /// <param name="Item">FS.HISFC.Models.HealthRecord.Diagnose</param>
        /// <returns>int 0 �ɹ� -1 ʧ��</returns>
        public int InsertDiagnoseMC(FS.HISFC.Models.HealthRecord.Diagnose Item)
        {
            this.SetDB(diagMgr);
            return diagMgr.InsertDiagnoseMedicare(Item);
        }
        /// <summary>
        /// ��ѯҽ�������Ϣ
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ��</param>
        /// <param name="HappenNo">������� ��ѯ���п�����%</param>
        /// <returns>�����Ϣ����Ԫ����: FS.HISFC.Models.HealthRecord.Diagnose</returns>
        public ArrayList QueryCaseDiagnose(string InpatientNO, string HappenNo)
        {
            this.SetDB(diagMgr);
            return diagMgr.QueryDiagnoseMedicare(InpatientNO, HappenNo);
        }
        /// <summary>
        /// ��ѯҽ���벡��ȫ���������Ϣ
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ��</param>
        /// <returns>�����Ϣ����Ԫ����: FS.HISFC.Models.HealthRecord.Diagnose</returns>
        public ArrayList QueryDiagnoseBoth(string InpatientNO)
        {
            this.SetDB(diagMgr);
            return diagMgr.QueryDiagnoseBoth(InpatientNO);
        }
        /// <summary>
        /// ����ҽ����ϼ�¼
        /// </summary>
        /// <param name="dgMc">FS.HISFC.Models.HealthRecord.Diagnose</param>
        /// <returns>int 0 �ɹ� -1 ʧ��</returns>
        public int UpdateDiagnoseMedicare(FS.HISFC.Models.HealthRecord.Diagnose dgMc)
        {
            this.SetDB(diagMgr);
            return diagMgr.UpdateDiagnoseMedicare(dgMc);
        }
        public int DeleteDiagnoseMedicare(String InpatientNO, int happenNO)
        {
            this.SetDB(diagMgr);
            return diagMgr.DeleteDiagnoseMedicare(InpatientNO, happenNO);
        }
        /// <summary>
        /// ��ȡ��Ժ�����
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <returns></returns>
        public ArrayList GetOutMainDiagnose(string inpatientNo)
        {
            this.SetDB(diagMgr);
            return diagMgr.GetOutMainDiagnose(inpatientNo);
        }
        #endregion

        #endregion

        #region �ӿ�ʵ��
        #endregion


    }
}
