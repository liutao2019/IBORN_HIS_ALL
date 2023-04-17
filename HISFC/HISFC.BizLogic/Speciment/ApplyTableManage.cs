using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.Speciment;
using System.Collections;
using System.Data;

namespace FS.HISFC.BizLogic.Speciment
{
    /// <summary>
    /// Speciment <br></br>
    /// [��������: ��������]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2009-11-24]<br></br>
    /// <�޸ļ�¼ 
    ///		�޸���='�ֹ���' 
    ///		�޸�ʱ��='2011-09-30' 
    ///		�޸�Ŀ��='�汾ת��4.5ת5.0'
    ///		�޸�����='���汾ת���⣬��DB2���ݿ�Ǩ�Ƶ�ORACLE'
    ///  />
    /// </summary>
    public class ApplyTableManage : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        #region ʵ��������Է���������
        /// <summary>
        /// ʵ��������Է���������
        /// </summary>
        /// <param name="specBox">�걾��֯���Ͷ���</param>
        /// <returns></returns>
        private string[] GetParam(ApplyTable applyTable)
        {
            string[] str = new string[] 
            { 
                applyTable.ApplyId.ToString(),
                applyTable.ApplyUserId,
                applyTable.ImpDocId,
                applyTable.ImpEmail,
                applyTable.ImpTel,
                applyTable.DeptId,
                applyTable.DeptName,
                applyTable.SubjectName,
                applyTable.SubjectId,
                applyTable.ResPlan,
                applyTable.ResPlanAtt,
                applyTable.FundStartTime.ToString(),
                applyTable.FundEndTime.ToString(),
                applyTable.FundName,
                applyTable.FundId,                
                applyTable.ApplyEmail,
                applyTable.ApplyTel,
                applyTable.ApplyTime.ToString(),
                applyTable.DiseaseType,
                applyTable.SpecAmout.ToString(),
                applyTable.SpecDetAmout,
                applyTable.OtherDemand,
                applyTable.SpecType,
                applyTable.SpecIsCancer,
                applyTable.SpecList,
                applyTable.OutPutResult,
                applyTable.OutPutTime.ToString(),
                applyTable.OutPutOperDoc,
                applyTable.DeptFromComm,
                applyTable.DeptFromDate.ToString(),
                applyTable.SpecAdmComment,
                applyTable.SepcAdmDate.ToString(),
                applyTable.AcceptConfirm,
                applyTable.AcceptConfrimDate.ToString(),
                applyTable.IsImmdBackList,
                applyTable.SpecCountInDpet,
                applyTable.Percent,
                applyTable.LeftAmount,
                applyTable.ResearchResult,
                applyTable.Comment,
                applyTable.ApplyUserName,
                applyTable.ImpProcess,
                applyTable.ImpResult,
                applyTable.ImpName
            };
            return str;
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

        #region ������������
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="sqlIndex">sql�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateApplyTable(string sqlIndex, params string[] args)
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

        /// <summary>
        /// ����sql������ȡ������б�
        /// </summary>
        /// <param name="sqlIndex">sql����</param>
        /// <param name="args">����</param>
        /// <returns></returns>
        private ArrayList QueryApplyTable(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return null;
            }
            if (this.ExecQuery(sql, args) == -1)
                return null;
            ArrayList arrApplyTable = new ArrayList();
            while (this.Reader.Read())
            {
                ApplyTable appTable = SetApplyTable();
                arrApplyTable.Add(appTable);
            }
            this.Reader.Close();
            return arrApplyTable;
        }

        /// <summary>
        /// ����sql��ѯ������б�
        /// </summary>
        /// <param name="sql">sql���</param>
        /// <returns></returns>
        private ArrayList QueryApplyTable(string sql)
        {
            if (this.ExecQuery(sql) == -1)
                return null;
            ArrayList arrApplyTable = new ArrayList();
            while (this.Reader.Read())
            {
                ApplyTable appTable = SetApplyTable();
                arrApplyTable.Add(appTable);
            }
            this.Reader.Close();
            return arrApplyTable;
        }

        /// <summary>
        /// ��ȡ�������Ϣ
        /// </summary>
        /// <returns>�����ʵ��</returns>
        private ApplyTable SetApplyTable()
        {
            ApplyTable applyTable = new ApplyTable();
            try
            {
                applyTable.ApplyId = Convert.ToInt32(this.Reader["APPLICATIONID"].ToString());
                applyTable.ApplyUserId = this.Reader["APPLYUSERID"].ToString();
                applyTable.ImpDocId = this.Reader["IMPLEMENTDOCID"].ToString();
                applyTable.ImpEmail = this.Reader["IMPLEMENTEMAIL"].ToString();
                applyTable.ImpTel = this.Reader["IMPLEMENTTEL"].ToString();
                applyTable.DeptId = this.Reader["DEPARTMENTID"].ToString();
                applyTable.DeptName = this.Reader["DEPARTMENTNAME"].ToString();
                applyTable.SubjectName = this.Reader["SUBJECTNAME"].ToString();
                applyTable.SubjectId = this.Reader["SUBJECTID"].ToString();
                applyTable.ResPlan = this.Reader["RESEARCHPLAN"].ToString();
                applyTable.ResPlanAtt = this.Reader["RESEARCHPLANATT"].ToString();
                applyTable.FundStartTime = Convert.ToDateTime(this.Reader["FUNDSTARTTIME"].ToString());
                applyTable.FundEndTime = Convert.ToDateTime(this.Reader["FUNDENDTIME"].ToString());
                applyTable.FundName = this.Reader["FUNDNAME"].ToString();
                applyTable.FundId = this.Reader["FUNDID"].ToString();
                applyTable.ApplyEmail = this.Reader["APPLYEMAIL"].ToString();
                applyTable.ApplyTel = this.Reader["APPLYTEL"].ToString();
                applyTable.ApplyTime = Convert.ToDateTime(this.Reader["APPLYTIME"].ToString());
                applyTable.DiseaseType = this.Reader["DISEASETYPE"].ToString();
                applyTable.SpecAmout = Convert.ToInt32(this.Reader["SPECAMOUNT"].ToString());
                applyTable.SpecDetAmout = this.Reader["SPECDETAMOUNT"].ToString();
                applyTable.OtherDemand = this.Reader["OTHERDEMAND"].ToString();
                applyTable.SpecType = this.Reader["SPECTYPE"].ToString();
                applyTable.SpecIsCancer = this.Reader["SPECISCANCER"].ToString();
                applyTable.SpecList = this.Reader["SPECLIST"].ToString();
                applyTable.OutPutResult = this.Reader["OUTPUTRESULT"].ToString();
                applyTable.OutPutTime = Convert.ToDateTime(this.Reader["OUTPUTTIME"].ToString());
                applyTable.OutPutOperDoc = this.Reader["OUTPUTOPERDOC"].ToString();
                applyTable.DeptFromComm = this.Reader["SPECDEPTFROM"].ToString();
                applyTable.DeptFromDate = Convert.ToDateTime(this.Reader["DEPTDATE"].ToString());
                applyTable.SpecAdmComment = this.Reader["SPECADM"].ToString();
                applyTable.SepcAdmDate = Convert.ToDateTime(this.Reader["SPECADMDATE"].ToString());
                applyTable.AcceptConfirm = this.Reader["ACCEPTCONFRIM"].ToString();
                applyTable.AcceptConfrimDate = Convert.ToDateTime(this.Reader["ACCEPTCONDATE"].ToString());
                applyTable.IsImmdBackList = this.Reader["ISIMMEDBACKLIST"].ToString();
                applyTable.SpecCountInDpet = this.Reader["SPECCOUNTINDEPT"].ToString();
                applyTable.Percent = this.Reader["PERINALL"].ToString();
                applyTable.LeftAmount = this.Reader["LEFTAMOUT"].ToString();
                applyTable.ResearchResult = this.Reader["RESREACHRESULT"].ToString();
                applyTable.Comment = this.Reader["MARK"].ToString();
                applyTable.ApplyUserName = this.Reader["APPLYUSERNAME"].ToString();
                applyTable.ImpProcess = this.Reader["IMPPROCESS"].ToString();
                applyTable.ImpResult = this.Reader["IMPRESLUT"].ToString();
                applyTable.ImpName = this.Reader["IMPLEMENTNAME"].ToString();
            }
            catch (Exception e)
            {
                this.ErrCode = e.Message;
                this.Err = e.Message;
                return null;
            }
            return applyTable;
        }
        #endregion

        #endregion

        #region ���з���


        /// <summary>
        /// ��ȡ�µ�Sequence(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">��ȡ���µ�Sequence</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetNextSequence(ref string sequence)
        {
            //
            // ִ��SQL���
            //
            sequence = this.GetSequence("Speciment.BizLogic.ApplyTableManage.GetNextSequence");
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
            return 1;
        }

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="applyTable">�����ʵ��</param>
        /// <returns></returns>
        public int InsertApplyTable(ApplyTable applyTable)
        {
            return this.UpdateApplyTable("Speciment.BizLogic.ApplyTableManage.Insert", this.GetParam(applyTable));

        }

        /// <summary>
        /// ��������ID��ȡ�����
        /// </summary>
        /// <param name="applyNum"></param>
        /// <returns></returns>
        public ApplyTable QueryApplyByID(string applyNum)
        {
            ArrayList arrTmp = new ArrayList();
            string sql = "SELECT * FROM SPEC_APPLICATIONTABLE WHERE APPLICATIONID = " + applyNum;
            arrTmp = this.QueryApplyTable(sql);
            ApplyTable appTable = new ApplyTable();
            foreach (ApplyTable a in arrTmp)
            {
                appTable = a;
                break;
            }

            #region ���Ӹ���ǰ�������
            try
            {
                string sqlIdx = @"select b.SCHEDULE �������� from SPEC_USERAPPLICATION b where 
                                        b.USERAPPLID = (select max(USERAPPLID) from SPEC_USERAPPLICATION where APPLICATIONID = {0})";
                sqlIdx = string.Format(sqlIdx, applyNum);
                if (this.ExecQuery(sqlIdx) == -1)
                    return null;
                while (this.Reader.Read())
                {
                    appTable.User03 = this.Reader[0].ToString();
                }
                this.Reader.Close();
            }
            catch
            {
            }
            #endregion

            return appTable;
        }

        /// <summary>
        /// ����������ID��ȡ�����
        /// ����impprocess��־��ȡ�Ѿ������ģ���O�����������У���U������ȡ������C�����������
        /// </summary>
        /// <param name="applyUserNum"></param>
        /// <param name="impprocess"></param>
        /// <returns></returns>
        public ArrayList QueryApplyByApplyUserID(string applyUserId, string impprocess)
        {
            ArrayList arrApplyListApplyId = new ArrayList();
            //ArrayList arrTmp = new ArrayList();
            string sql = "SELECT SPEC_APPLICATIONTABLE.APPLICATIONID FROM SPEC_APPLICATIONTABLE WHERE APPLYUSERID = " + applyUserId + " and IMPPROCESS =" + impprocess;
            arrApplyListApplyId = this.QueryApplyTable(sql);
            return arrApplyListApplyId;
        }

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="applyTable"></param>
        /// <returns></returns>
        public int UpdateApplyTable(ApplyTable applyTable)
        {
            return this.UpdateApplyTable("Speciment.BizLogic.ApplyTableManage.Update", GetParam(applyTable));
        }

        /// <summary>
        /// ��ȡ��һ������ID
        /// </summary>
        /// <returns></returns>
        public int GetMaxApplyId()
        {
            try
            {
                return Convert.ToInt32(this.ExecSqlReturnOne("select max(a.APPLICATIONID) + 1	from SPEC_APPLICATIONTABLE a"));
            }
            catch
            {
                return -1;
            }

        }

        /// <summary>
        /// ���������ˡ����Ҽ���ֹʱ���ѯ�����¼
        /// </summary>
        /// <param name="empl"></param>
        /// <param name="dept"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public ArrayList GetSubApply(string empl, string dept, DateTime dtBegin, DateTime dtEnd)
        {
            ArrayList arrApplyList = new ArrayList();
            string sql = @"SELECT * FROM SPEC_APPLICATIONTABLE WHERE APPLYUSERID like '{0}' 
                           and DEPARTMENTID like '{1}' 
                           and APPLYTIME between timestamp('{2}') and timestamp('{3}')";
            try
            {
                sql = string.Format(sql, empl, dept, dtBegin, dtEnd);
                arrApplyList = this.QueryApplyTable(sql);
            }
            catch
            {
                return null;
            }
            return arrApplyList;
        }

        public int appExecQuery(string sqlIdx, ref DataSet ds)
        {
            return this.ExecQuery(sqlIdx, ref ds);
        }
        #endregion
    }
}
