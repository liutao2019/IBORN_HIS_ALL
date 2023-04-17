using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.Models.HealthRecord.EnumServer;
namespace FS.HISFC.BizProcess.Integrate.HealthRecord
{
    public class HealthRecordBase : IntegrateBase
    {
        protected FS.HISFC.BizLogic.HealthRecord.ICD icdMgr = new FS.HISFC.BizLogic.HealthRecord.ICD();
        protected FS.HISFC.BizLogic.HealthRecord.Diagnose diagMgr = new FS.HISFC.BizLogic.HealthRecord.Diagnose();
        public override void SetTrans(System.Data.IDbTransaction trans)
        {
            this.trans = trans;
            icdMgr.SetTrans(trans);
            diagMgr.SetTrans(trans);
        }
        /// <summary>
        /// �����Ӧ��ѯ����ICD��Ϣ 
        /// </summary>
        /// <param name="ICDType">�������ö��</param>
        /// <param name="queryType">��ѯ����ö��</param>
        public ArrayList ICDQuery(ICDTypes ICDType, QueryTypes queryType)
        {
            this.SetDB(icdMgr);
            return icdMgr.Query(ICDType, queryType);
        }

        /// <summary>
        /// �����Ӧ��ѯ����ICD��Ϣ (�����Ƴ�������
        /// </summary>
        /// <param name="ICDType">�������ö��</param>
        /// <param name="queryType">��ѯ����ö��</param>
        public ArrayList ICDQueryNew(ICDTypes ICDType, QueryTypes queryType, string deptCode)
        {
            this.SetDB(icdMgr);
            return icdMgr.QueryNew(ICDType, queryType, deptCode);
        }
 
         /// <summary>
        /// ɾ��һ�����ߵ����в��������Ϣ
        /// </summary>
        /// <param name="InpatientNO">string ����סԺ��ˮ��</param>
        /// <returns>int 0 �ɹ� -1 ʧ��</returns>
        public int DeleteDiagnoseAll(string InpatientNO, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType, FS.HISFC.Models.Base.ServiceTypes personType)
        {
            this.SetDB(diagMgr);
            return diagMgr.DeleteDiagnoseAll(InpatientNO, OperType, personType);
        }

        /// <summary>
        /// ���벡�������Ϣ
        /// </summary>
        /// <param name="Item">FS.HISFC.Models.HealthRecord.Diagnose</param>
        /// <returns>int 0 �ɹ� -1 ʧ��</returns>
        public int InsertDiagnose(FS.HISFC.Models.HealthRecord.Diagnose Item)
        {
            this.SetDB(diagMgr);
            return diagMgr.InsertDiagnose(Item);
        }
         /// <summary>
        /// ��ò�����ϱ��еĻ��������Ϣ,����Ѿ��в����Ļ��߲�ѯ 
        /// </summary>
        /// <param name="InpatientNO">סԺ��ˮ��</param>
        /// <param name="diagType">������� ������ϣ���Ժ��ϵ� ��ѯ���еĿ������� %</param>
        /// <param name="OperType">"DOC"��ѯҽ��վ¼��������Ϣ ��CAS" ��ѯ������¼��������Ϣ</param>
        /// <returns>�����Ϣ����Ԫ����: FS.HISFC.Models.HealthRecord.Diagnose</returns>
        public ArrayList QueryCaseDiagnose(string InpatientNO, string diagType, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType, FS.HISFC.Models.Base.ServiceTypes personType)
        {
            this.SetDB(diagMgr);
            return diagMgr.QueryCaseDiagnose(InpatientNO, diagType, OperType, personType);
        }
        /// <summary>
        /// ���²�����ϼ�¼
        /// </summary>
        /// <param name="dgMc">FS.HISFC.Models.HealthRecord.Diagnose</param>
        /// <returns>int 0 �ɹ� -1 ʧ��</returns>
        public int UpdateDiagnose(FS.HISFC.Models.HealthRecord.Diagnose dg)
        {
            this.SetDB(diagMgr);
            return diagMgr.UpdateDiagnose(dg);
        }
        public int DeleteDiagnoseSingle(string InpatientNO, int happenNO, FS.HISFC.Models.HealthRecord.EnumServer.frmTypes OperType, FS.HISFC.Models.Base.ServiceTypes personType)
        {
            this.SetDB(diagMgr);
            return diagMgr.DeleteDiagnoseSingle(InpatientNO, happenNO, OperType, personType);
        }

        /// <summary>
        /// ��ѯ�������,������������� met_com_diagnose {5F752A30-7971-4b65-A84B-D233EF2A4406}
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <returns></returns>
        public ArrayList QueryDiagnoseNoOps(string inpatientNO)
        {
            this.SetDB(diagMgr);
            return diagMgr.QueryDiagnoseNoOps(inpatientNO);
        }

        #region {6EF7D73B-4350-4790-B98C-C0BD0098516E}

        /// <summary>
        /// ��ѯ���пƳ������
        /// </summary>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QueryDeptDiag(string deptID)
        {
            this.SetDB(icdMgr);

            return icdMgr.QueryDeptDiag(deptID);
        }

        #endregion
       
        #region {17537415-C168-450d-BBCC-93CFFA19DB82}
        public ArrayList QueryDiagnoseByPatientNo(string inpatientNO)
        {
            this.SetDB(diagMgr);
            return diagMgr.QueryDiagnoseNoByPatientNo(inpatientNO);
        }
        #endregion

    }
}
