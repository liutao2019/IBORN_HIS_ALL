using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.EPR
{
    /// <summary>
    /// �ʿ���Ϣͳ�����ݿ������
    /// </summary>
    public class QCStat : FS.FrameWork.Management.Database 
    {
        public QCStat()
        {
        }

        #region ���
        /// <summary>
        /// ����ͳ�ƽ��
        /// </summary>
        /// <param name="result">FS.FrameWork.Models.NeuObject,IDΪ���߱��룬MemoΪ������Ժ���ڣ�NameΪ�ʿ�ID��User01Ϊ�ʿ����ƣ�User02Ϊ�ʿؽ����User03Ϊͳ������</param>
        /// <returns></returns>
        public int Insert(FS.FrameWork.Models.NeuObject result){
            string sqlIndex = "EPR.QCNightStat.Insert";
            string sql = string.Empty;

            if (this.Sql.GetSql(sqlIndex, ref sql) == -1) return -1;
            sql = string.Format(sql,result.ID,result.Memo,result.Name,result.User01,result.User02,result.User03);
            return this.ExecNoQuery(sql);
        }
        #endregion

        #region ɾ��
        /// <summary>
        /// ɾ�����ǽ���ͳ�ƽ��
        /// </summary>
        /// <returns></returns>
        public int Delete()
        {
            string sqlIndex = "EPR.QCNightStat.Delete";
            string sql = string.Empty;

            if (this.Sql.GetSql(sqlIndex, ref sql) == -1) return -1;
            return this.ExecNoQuery(sql);
        }
        #endregion

        #region ����
        /// <summary>
        /// ����ͳ��ʱ�����ͳ�ƽ��
        /// </summary>
        /// <param name="beginDate">ͳ��ʱ����ʼʱ��</param>
        /// <param name="endDate">ͳ��ʱ����ֹʱ��</param>
        /// <returns>ArrayList ��ÿ��ItemΪFS.FrameWork.Models.NeuObject,IDΪ���߱��룬MemoΪ������Ժ���ڣ�NameΪ�ʿ�ID��User01Ϊ�ʿ����ƣ�User02Ϊ�ʿؽ����User03Ϊͳ������</returns>
        public ArrayList QueryByStatDate(DateTime beginDate, DateTime endDate)
        {
            string sqlQueryIndex = "EPR.QCNightStat.Query";
            string sqlWhereIndex = "EPR.QCNightStat.WhereByStatDate";
            string sqlQuery = string.Empty;
            string sqlWhere = string.Empty;

            if (this.Sql.GetSql(sqlQueryIndex, ref sqlQuery) == -1) return null;
            if (this.Sql.GetSql(sqlWhereIndex, ref sqlWhere) == -1) return null;
            sqlWhere = string.Format(sqlWhere, beginDate, endDate);

            return this.FillDate(sqlQuery + sqlWhere);


        }
        /// <summary>
        /// ���ݻ�����Ժʱ�����ͳ�ƽ��
        /// </summary>
        /// <param name="beginDate">������Ժʱ����ʼʱ��</param>
        /// <param name="endDate">������Ժʱ����ֹʱ��</param>
        /// <returns>ArrayList ��ÿ��ItemΪFS.FrameWork.Models.NeuObject,IDΪ���߱��룬MemoΪ������Ժ���ڣ�NameΪ�ʿ�ID��User01Ϊ�ʿ����ƣ�User02Ϊ�ʿؽ����User03Ϊͳ������</returns>
        public ArrayList QueryByInDate(DateTime beginDate, DateTime endDate)
        {
            string sqlQueryIndex = "EPR.QCNightStat.Query";
            string sqlWhereIndex = "EPR.QCNightStat.WhereByInDate";
            string sqlQuery = string.Empty;
            string sqlWhere = string.Empty;

            if (this.Sql.GetSql(sqlQueryIndex, ref sqlQuery) == -1) return null;
            if (this.Sql.GetSql(sqlWhereIndex, ref sqlWhere) == -1) return null;
            sqlWhere = string.Format(sqlWhere,beginDate,endDate);

            return this.FillDate(sqlQuery + sqlWhere);

        }
        ///// <summary>
        ///// ���ݲ��ż���ͳ�ƽ��
        ///// </summary>
        ///// <param name="deptCode">���ű���</param>
        ///// <param name="state">����״̬</param>
        ///// <returns>ArrayList ��ÿ��ItemΪFS.FrameWork.Models.NeuObject,IDΪ���߱��룬MemoΪ������Ժ���ڣ�NameΪ�ʿ�ID��User01Ϊ�ʿ����ƣ�User02Ϊ�ʿؽ����User03Ϊͳ������</returns>
        //public ArrayList QueryByDept(string deptCode, string state)
        //{
        //    string sqlQueryIndex = "EPR.QCNightStat.Query";
        //    string sqlWhereIndex = "EPR.QCNightStat.WhereByPatientNO";
        //    string sqlQuery = string.Empty;
        //    string sqlWhere = string.Empty;
        //    string patienNOs = string.Empty;

        //    ArrayList alPatients = FS.HISFC.Management.Factory.Function.IntegrateRADT.QueryPatientByDept(deptCode, state);
        //    foreach (FS.HISFC.Models.RADT.Patient patient in alPatients)
        //    {
        //        patienNOs += "," + patient.ID;
        //    }
        //    if (!string.IsNullOrEmpty)
        //    {
        //        patienNOs = patienNOs.Substring(1); //ȥ��ǰ���","  
        //    }
        //    else
        //    {
        //        return null;//��������ڵ�patienΪ�������������ֱ�ӷ���
        //    }
        //    if (this.Sql.GetSql(sqlQueryIndex, ref sqlQuery) == -1) return null;
        //    if (this.Sql.GetSql(sqlWhereIndex, ref sqlWhere) == -1) return null;
        //    sqlWhere = string.Format(sqlWhere, patientNs);

        //    return this.FillDate(sqlWhere + sqlWhere);

        //}
        /// <summary>
        /// ���ݻ���ID����ͳ�ƽ��
        /// </summary>
        /// <param name="patientNo">����ID</param>
        /// <returns>ArrayList ��ÿ��ItemΪFS.FrameWork.Models.NeuObject,IDΪ���߱��룬MemoΪ������Ժ���ڣ�NameΪ�ʿ�ID��User01Ϊ�ʿ����ƣ�User02Ϊ�ʿؽ����User03Ϊͳ������</returns>
        public ArrayList QueryByPatienNo(string patientNo)
        {
            string sqlQueryIndex = "EPR.QCNightStat.Query";
            string sqlWhereIndex = "EPR.QCNightStat.WhereByPatientNO";
            string sqlQuery = string.Empty;
            string sqlWhere = string.Empty;

            if (this.Sql.GetSql(sqlQueryIndex, ref sqlQuery) == -1) return null;
            if (this.Sql.GetSql(sqlWhereIndex, ref sqlWhere) == -1) return null;
            sqlWhere = string.Format(sqlWhere, patientNo);

            return this.FillDate(sqlQuery + sqlWhere);

        }
        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="sql">��ѯ��sql</param>
        /// <returns>ArrayList ��ÿ��ItemΪFS.FrameWork.Models.NeuObject,IDΪ���߱��룬MemoΪ������Ժ���ڣ�NameΪ�ʿ�ID��User01Ϊ�ʿ����ƣ�User02Ϊ�ʿؽ����User03Ϊͳ������</returns>
        private ArrayList FillDate(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject result = new FS.FrameWork.Models.NeuObject();
                result.ID = this.Reader[0].ToString();//���߱���
                result.Memo = this.Reader[1].ToString();//������Ժ����
                result.Name = this.Reader[2].ToString();//�ʿ�ID
                result.User01 = this.Reader[6].ToString();//��������
                result.User02 = this.Reader[3].ToString();//�ʿؽ��
                //result.User03 = this.Reader[5].ToString();//������Ա��û�б�Ҫ��ʾ���û�
                result.User03 = this.Reader[5].ToString();//ͳ������
                al.Add(result);
            }
            this.Reader.Close();
            return al;
        }
        #endregion
    }
}
