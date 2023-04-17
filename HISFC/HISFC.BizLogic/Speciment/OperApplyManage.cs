using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// OperApply <br></br>
    /// [��������: �����������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2010-02-22]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-10-13' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class OperApplyManage : FS.FrameWork.Management.Database
    {

        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ���������뵥ʵ��ת��Ϊ�����б�
        /// </summary>
        /// <param name="operApply">�������뵥ʵ��</param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.Speciment.OperApply operApply)
        {
            string[] str = new string[]
                        {
                            operApply.OperApplyId.ToString(),
                            operApply.OperId,
                            operApply.OperName,
                            operApply.OperDeptName,
                            operApply.OperDeptId,
                            operApply.InHosNum,
                            operApply.PatientName, 
                            operApply.MediDoc.MainDoc.ID,
                            operApply.MediDoc.MainDoc.Name,
                            operApply.MediDoc.MainDoc1.ID,
                            operApply.MediDoc.MainDoc1.Name,
                            operApply.MediDoc.MainDoc2.Name,  
                            operApply.MediDoc.MainDoc2.ID,
                            operApply.MediDoc.MainDoc3.ID,                                                      
                            operApply.MediDoc.MainDoc3.Name,
                            operApply.OperTime.ToString(),
                            operApply.HadCollect,
                            operApply.NoColReason,
                            operApply.ColTime.ToString(),
                            operApply.HadOperInfo,
                            operApply.GetOperInfoTime.ToString(),
                            operApply.GetPeriod,                           
                            operApply.MainDiaICD,
                            operApply.MainDiaName,
                            operApply.MainDiaICD1,
                            operApply.MainDiaName1,
                            operApply.MainDiaICD2, 
                            operApply.MainDiaName2,                          
                            operApply.Comment,
                            operApply.OperLocation,
                            operApply.TumorPor,
                            operApply.OperPosId,
                            operApply.OperPosName,
                            operApply.OrgOrBlood,
                            operApply.OrderId
                        };
            return str;
        }

        /// <summary>
        /// ��ȡOperApply������ʵ����Ϣ
        /// </summary>
        /// <returns>�������뵥</returns>
        private OperApply SetOperApply()
        {
            OperApply operApply = new OperApply();
            try
            {
                operApply.OperApplyId = Convert.ToInt32(this.Reader["APPID"].ToString());
                operApply.OperId = this.Reader["OPERID"].ToString();
                operApply.OperName = this.Reader["OPERNAME"].ToString();
                operApply.OperDeptName = this.Reader["OPERDEPTNAME"].ToString();
                operApply.OperDeptId = this.Reader["OPERDEPTID"].ToString();
                operApply.InHosNum = this.Reader["INHOSNUM"].ToString();
                operApply.PatientName = this.Reader["PATIENTNAME"].ToString();
                operApply.MediDoc.MainDoc.ID = this.Reader["MAIN_DOCTOR"].ToString();
                operApply.MediDoc.MainDoc.Name = this.Reader["MAIN_DOCNAME"].ToString();
                operApply.MediDoc.MainDoc1.ID = this.Reader["ASS_DOC1"].ToString();
                operApply.MediDoc.MainDoc1.Name = this.Reader["ASS_DOCNAME1"].ToString();
                operApply.MediDoc.MainDoc2.Name = this.Reader["ASS_DOCNAME2"].ToString();
                operApply.MediDoc.MainDoc2.ID = this.Reader["ASS_DOC2"].ToString();
                operApply.MediDoc.MainDoc3.ID = this.Reader["ASS_DOC3"].ToString();
                operApply.MediDoc.MainDoc3.Name = this.Reader["ASS_DOCNAME3"].ToString();
                operApply.OperTime = Convert.ToDateTime(this.Reader["OPERTIME"].ToString());
                operApply.HadCollect = this.Reader["HADCOLLECT"].ToString();
                operApply.NoColReason = this.Reader["NOCOLREASON"].ToString();
                operApply.ColTime = Convert.ToDateTime(this.Reader["COLTIME"].ToString());
                operApply.HadOperInfo = this.Reader["HADOPERINFO"].ToString();
                operApply.GetOperInfoTime = Convert.ToDateTime(this.Reader["GETOPERINFOTIME"].ToString());
                operApply.GetPeriod = this.Reader["GETPEORID"].ToString();               
                operApply.MainDiaICD = this.Reader["MAIN_DIACODE"].ToString();
                operApply.MainDiaName = this.Reader["MAIN_DIANAME"].ToString();
                operApply.MainDiaICD1 = this.Reader["MAIN_DIACODE1"].ToString();
                operApply.MainDiaName1 = this.Reader["MAIN_DIANAME1"].ToString();
                operApply.MainDiaICD2 = this.Reader["MAIN_DIACODE2"].ToString();
                operApply.MainDiaName2 = this.Reader["MAIN_DIANAME2"].ToString();               
                operApply.Comment = this.Reader["MARK"].ToString();
                operApply.OperLocation = this.Reader["OPERLOCATION"].ToString();
                operApply.TumorPor = this.Reader["TUMORPOR"].ToString();
                operApply.OrgOrBlood = this.Reader["ORGORBLOOD"].ToString();
                operApply.OrderId = this.Reader["ORDERID"].ToString();
                 
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return operApply;
        }

        /// <summary>
        /// ����sql����ȡ��OperApply
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="parm"></param>
        /// <returns></returns>
        private ArrayList SelectOperApply(string sqlIndex, string[] parm)
        {
            string sql = "";
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
                return null;
            if (this.ExecQuery(sql, parm) == -1)
                return null;
            OperApply operApply;
            ArrayList arrOperApply = new ArrayList();
            while (this.Reader.Read())
            {
                operApply = SetOperApply();
                arrOperApply.Add(operApply);
            }
            this.Reader.Close();
            return arrOperApply;
        }

        #region ���ô�����Ϣ
        /// <summary>
        /// ���ô�����Ϣ
        /// </summary>
        /// <param name="errorCode">������뷢������</param>
        /// <param name="errorText">������Ϣ</param>
        private void SetError(string errorCode, string errorText)
        {
            this.ErrCode = errorCode;
            this.Err = errorText + "[" + this.Err + "]"; // + "��ShelfSpecManage.cs�ĵ�" + argErrorCode + "�д���";
            this.WriteErr();
        }
        #endregion
        #endregion

        #region ���²���
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateOperApply(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, args);
        }
        #endregion

        #endregion


        /// <summary>
        /// ��ȡ�µ�Sequence(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">��ȡ���µ�Sequence</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetSequence()
        {
            //
            // ִ��SQL���
            //
            string sequence = this.GetSequence("Speciment.BizLogic.OperApplyManage.GetNextSequence");
            //
            // �������NULL�����ȡʧ��
            //
            if (sequence == null)
            {
                this.SetError("", "��ȡSequenceʧ��");
                return -1;
            }
            //
            // �ɹ�����
            //
            return Convert.ToInt32(sequence);
        }

        /// <summary>
        /// �������뵥����
        /// </summary>
        /// <param name="specSource"></param>
        /// <returns></returns>
        public int InsertOperApply(FS.HISFC.Models.Speciment.OperApply operApply)
        {
            try
            {
                return this.UpdateOperApply("Speciment.BizLogic.OperApplyManage.Insert", this.GetParam(operApply));
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }


        /// <summary>
        /// �����������뵥
        /// </summary>
        /// <param name="operApply"></param>
        /// <returns></returns>
        public int UpdateOperApply(OperApply operApply)
        {
            try
            {
                return this.UpdateOperApply("Speciment.BizLogic.OperApplyManage.Update", this.GetParam(operApply));
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ����ȫ����Ϣ
        /// </summary>
        /// <param name="operApply"></param>
        /// <returns></returns>
        public int UpdateOperApply1(OperApply operApply)
        {
            try
            {
                return this.UpdateOperApply("Speciment.BizLogic.OperApplyManage.Update1", this.GetParam(operApply));
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// �����ռ���־
        /// </summary>
        /// <param name="operId">ID</param>
        /// <param name="flag">��־0�����ռ���1 δ�ռ���2 ��ʿվ �ѷ��� 3 ȡ���걾</param>
        /// <returns></returns>
        public int UpdateColFlag(string operApplyId, string flag)
        {
            try
            {
                return this.UpdateOperApply("Speciment.BizLogic.OperApplyManage.UpdateColFlag", new string[] { operApplyId, flag });
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ɾ���������뵥
        /// </summary>
        /// <param name="operId">HIS�������뵥��</param>
        /// <returns></returns>
        public int DeleteOperApply(string operId)
        {
            try
            {
                return this.UpdateOperApply("Speciment.BizLogic.OperApplyManage.DeleteByOperId", new string[] { operId });
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ����ҽ����ɾ����¼
        /// </summary>
        /// <param name="orderId">ҽ����ˮ��</param>
        /// <returns></returns>
        public int DeleteByOrderId(string orderId)
        {
            try
            {
                return this.UpdateOperApply("Speciment.BizLogic.OperApplyManage.DeleteByOrderId", new string[] { orderId });
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ����sql��ȡ�������뵥
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList GetOperApplyBySql(string sql)
        {
            this.ExecQuery(sql);
            OperApply operApply;
            ArrayList arrList = new ArrayList();
            while (this.Reader.Read())
            {
                operApply = new OperApply();
                operApply = SetOperApply();
                arrList.Add(operApply);
            }
            this.Reader.Close();
            return arrList;
        }

        /// <summary>
        /// ����ҽ����ˮ�Ż�ȡ
        /// </summary>
        /// <param name="orderId">ҽ����ˮ��</param>
        /// <returns></returns>
        public ArrayList GetOperApplyByOrderId(string orderId)
        {
            return this.GetOperApplyBySql("select * from spec_operapply where orderid = '" + orderId + "'");
        }

        /// <summary>
        /// ȡ��û��¼��������Ϣ�����м�¼
        /// </summary>
        /// <returns></returns>
        public System.Data.DataSet GetOperInfoAll()
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery("Speciment.BizLogic.OperApplyManage.OperInfoAll", ref ds, new string[] { });
            return ds;
        }

        /// <summary>
        /// ����סԺ��ˮ��ȡ��û��¼��������Ϣ�ļ�¼
        /// </summary>
        /// <param name="inHosNum"></param>
        /// <returns></returns>
        public System.Data.DataSet GetOperInfoByInPatientNo(string inHosNum)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            this.ExecQuery("Speciment.BizLogic.OperApplyManage.OperInfoByInpatientno", ref ds, new string[] { inHosNum });
            return ds;
        }

        /// <summary>
        /// ����סԺ��ˮ�Ų���Ƿ�¼��걾
        /// </summary>
        /// <param name="inHosNum"></param>
        /// <param name="type">B:Ѫ�걾��O��֯�걾</param>
        /// <returns></returns>
        public ArrayList GetByInPatientNo(string inHosNum,string type)
        {
            return this.SelectOperApply("Speciment.BizLogic.OperApplyManage.OperApplyInfoByInpatientno", new string[] { inHosNum, type });
        }

        /// <summary>
        /// ��ѯ�����첡�˵�Ѫ�걾ִ�е�
        /// </summary>
        /// <param name="inHosNum">סԺ��ˮ��</param>
        /// <param name="dateTime">ʱ���</param>
        /// <param name="hadCol">�Ƿ��ռ�</param>
        /// <returns></returns>
        public OperApply GetByInPatientNoAndOperTime(string inHosNum, string startTime,string endTime,string hadCol,string type)
        {

            ArrayList arr = new ArrayList();
            if (hadCol != "")
            {
                arr = SelectOperApply("Speciment.BizLogic.OperApplyManage.GetByInPatientNoAndOperTime", new string[] { inHosNum, startTime, endTime, hadCol, type });

            }
            else
            {
                arr = SelectOperApply("Speciment.BizLogic.OperApplyManage.GetByInPatientNoAndOperTimeAll", new string[] { inHosNum, startTime, endTime, type });
            }
            if (arr != null && arr.Count > 0)
            {
                return arr[0] as OperApply;
            }
            return null; 
        }

        /// <summary>
        /// ����Id��ȡ
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public OperApply GetById(string Id, string type)
        {
            ArrayList arr = SelectOperApply("Speciment.BizLogic.OperApplyManage.GetById", new string[] { Id, type });
            if (arr != null && arr.Count > 0)
            {
                return arr[0] as OperApply;
            }
            return null;
        }

        /// <summary>
        /// �������������ID���� δȡ�걾����
        /// </summary>
        /// <param name="reason">����</param>
        /// <param name="operApplyId">Id</param>
        /// <returns></returns>
        public int UpdateReason(string reason,string operApplyId)
        {
            try
            {
                return this.UpdateOperApply("Speciment.BizLogic.OperApplyManage.UpdateColReason", new string[] { reason, operApplyId });
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// ��ѯ��ĳһҽ������ʱ����û��ʵ�����յ��걾�ļ�¼
        /// </summary>
        /// <param name="docId">ҽ��Id</param>
        /// <param name="operTime">����ʱ��</param>
        /// <param name="noColReason">û���յ���Ե��</param>
        /// <param name="colFlag">�Ƿ��յ��ı��</param>
        /// <returns></returns>
        public ArrayList GetNoReasonApply(string docId, string operTime, string noColReason,string colFlag)
        {
            try
            {
                return this.SelectOperApply("Speciment.BizLogic.OperApplyManage.NoColSpec", new string[] { docId, operTime, noColReason, colFlag });
            }
            catch(Exception ex)
            {
                this.ErrCode = ex.Message;
                return null;
            } 
        }

        /// <summary>
        /// �����������뵥Id��ȡ��¼
        /// </summary>
        /// <param name="operId">�������뵥</param>
        /// <returns></returns>
        public ArrayList GetByOperId(string operId)
        {
            try
            {
                return this.SelectOperApply("Speciment.BizLogic.OperApplyManage.GetByOperId", new string[] { operId });
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                return null;
            } 
        }

        /// <summary>
        /// ����סԺ��ˮ��ȡ��Ժ���
        /// </summary>
        /// <param name="inhosNum"></param>
        /// <returns></returns>
        public string GetDiaFromInMain(string inhosNum)
        {
            string sql = "select DIAG_NAME from FIN_IPR_INMAININFO where INPATIENT_NO='" + inhosNum + "'";
            return this.ExecSqlReturnOne(sql);
        }

        /// <summary>
        /// ����סԺ��ˮ�Ÿ������
        /// </summary>
        /// <param name="inHosNum"></param>
        /// <param name="diagNose"></param>
        /// <returns></returns>
        public int UpdateDiagInMain(string inHosNum,string diagNose)
        {
            string sql = "update FIN_IPR_INMAININFO set DIAG_NAME = '" + diagNose + "' where INPATIENT_NO='" + inHosNum + "'";
            return this.ExecNoQuery(sql);
        }
    }
}
