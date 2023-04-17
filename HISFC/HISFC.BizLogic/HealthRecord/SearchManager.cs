using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using FS.HISFC.Models.HealthRecord.EnumServer;
using System.Data;
namespace FS.HISFC.BizLogic.HealthRecord
{
    public  class SearchManager : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ��ȡ��ʷ��¼ �����кŲ�ѯ
        /// </summary>
        /// <param name="SeQuenceNo"></param>
        /// <param name="Type">1 ��ѯ������ϸ 2 ��ʷ��ѯ</param>
        /// <returns></returns>
        public ArrayList SelectContralDetail(string SeQuenceNo, string Type)
        {
            ArrayList List = new ArrayList();
            string strSql = "";
            if (this.Sql.GetSql("Case.SearchManager.SelectContralDetail", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, SeQuenceNo, Type);
                //��ѯ
                this.ExecQuery(strSql);
                FS.FrameWork.Models.NeuObject info = null;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.User02 = this.Reader[0].ToString(); //���
                    info.Name = this.Reader[1].ToString(); //�ؼ�����
                    info.ID = this.Reader[2].ToString();//�ؼ�ֵ
                    info.User01 = this.Reader[3].ToString(); //�ؼ�����
                    info.User03 = this.Reader[4].ToString(); //����ʱ��
                    List.Add(info);
                }

            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                List = null;
            }
            return List;
        }
        /// <summary>
        /// ��ʱ���ѯ 
        /// </summary>
        /// <param name="Type">1 ��ѯ������ϸ 2 ��ʷ��ѯ</param>
        /// <returns></returns>
        public ArrayList SelectContralDetail(string Type)
        {
            ArrayList List = new ArrayList();
            string strSql = "";
            if (this.Sql.GetSql("Case.SearchManager.SelectContralDetailbydate", ref strSql) == -1) return null;
            try
            {
                strSql = string.Format(strSql, Type);
                //��ѯ
                this.ExecQuery(strSql);
                FS.FrameWork.Models.NeuObject info = null;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.User02 = this.Reader[0].ToString(); //���
                    info.Name = this.Reader[1].ToString(); //�ؼ�����
                    info.ID = this.Reader[2].ToString();//�ؼ�ֵ
                    info.User01 = this.Reader[3].ToString(); //�ؼ�����
                    info.User03 = this.Reader[4].ToString(); //����ʱ��
                    List.Add(info);
                }

            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                List = null;
            }
            return List;
        }
        /// <summary>
        /// ������ϸ
        /// </summary>
        /// <returns></returns>
        public int InsertContralDetail(FS.FrameWork.Models.NeuObject info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.SearchManager.InsertContralDetail", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, info.User02, info.Name, info.ID, info.User01, info.User03);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ɾ����ϸ
        /// </summary>
        /// <param name="SequenceNo"></param>
        /// <returns></returns>
        public int DeleteContralDetail(string SequenceNo)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.SearchManager.DeleteContralDetail", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, SequenceNo);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        public int DeleteContralDetail(int daysBefore)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.SearchManager.DeleteContralDetail2", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, daysBefore);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ��ѯĿǰ���б����������
        /// </summary>
        /// <param name="strType"></param>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public ArrayList SelectContral(FS.HISFC.Models.HealthRecord.EnumServer.SelectTypes strType, string strCode)
        {
            ArrayList List = null;
            string strSql = "";
            if (this.Sql.GetSql("Case.SearchManager.SelectContral", ref strSql) == -1) return null;
            string strTemp = "";
            if (strType == SelectTypes.DEPT)
            {
                strTemp = "0";
            }
            else if (strType == SelectTypes.EMPOYE)
            {
                strTemp = "1";
            }
            try
            {
                strSql = string.Format(strSql, strTemp, strCode);
                //��ѯ
                this.ExecQuery(strSql);
                FS.FrameWork.Models.NeuObject info = null;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    info.User02 = this.Reader[0].ToString(); //�������
                    info.ID = this.Reader[1].ToString(); //��������
                    info.Name = this.Reader[2].ToString();//0 ��������  ,1��������
                    info.User01 = this.Reader[3].ToString(); //����������  �������״� Ա������ �������� ����ұ���
                    List.Add(info);
                }

            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                List = null;
            }
            return List;
        }

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertContral(FS.FrameWork.Models.NeuObject info)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.SearchManager.InsertContral", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, info.ID, info.Name, info.User01, info.User02);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ɾ����ϸ
        /// </summary>
        /// <param name="SequenceNo"></param>
        /// <returns></returns>
        public int DeleteContral(string SequenceNo)
        {
            string strSql = "";
            if (this.Sql.GetSql("Case.SearchManager.DeleteContral", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, SequenceNo);
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// ��ȡ��ѯ�ģ�����䡡
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string GetSelectSql(FS.HISFC.Models.HealthRecord.EnumServer.TablesName type)
        {
            string strSql = "";
            switch (type)
            {
                case TablesName.BASE:
                    if (this.Sql.GetSql("Case.SearchManager.GetSelectSql.BASE", ref strSql) == -1) return null;
                    break;
                case TablesName.DIAG:
                    if (this.Sql.GetSql("Case.SearchManager.GetSelectSql.DIAG", ref strSql) == -1) return null;
                    break;
                case TablesName.OPERATION:
                    if (this.Sql.GetSql("Case.SearchManager.GetSelectSql.OPERATION", ref strSql) == -1) return null;
                    break;
                case TablesName.BASEANDDIAG:
                    if (this.Sql.GetSql("Case.SearchManager.GetSelectSql.BASEANDDIAG", ref strSql) == -1) return null;
                    break;
                case TablesName.BASEANDOPERATION:
                    if (this.Sql.GetSql("Case.SearchManager.GetSelectSql.BASEANDOPERATION", ref strSql) == -1) return null;
                    break;
                case TablesName.DIAGANDOPERATION:
                    if (this.Sql.GetSql("Case.SearchManager.GetSelectSql.DIAGANDOPERATION", ref strSql) == -1) return null;
                    break;
                case TablesName.BASEANDDIAGANDOPERATION:
                    if (this.Sql.GetSql("Case.SearchManager.GetSelectSql.BASEANDDIAGANDOPERATION", ref strSql) == -1) return null;
                    break;
                case TablesName.BASESUB: //��ͬһ����Ժ �и���סԺ��ˮ�Ż�ȡסԺ�� �ٲ�ѯסԺ����ͬ��סԺ��ˮ��
                    if (this.Sql.GetSql("Case.SearchManager.GetSelectSql.BASESUB", ref strSql) == -1) return null;
                    break;
                case TablesName.DIAGSINGLE: //�����
                    if (this.Sql.GetSql("Case.SearchManager.GetSelectSql.DIAGSINGLE", ref strSql) == -1) return null;
                    break;
                case TablesName.OPERATIONSINGLE: //������
                    if (this.Sql.GetSql("Case.SearchManager.GetSelectSql.OPERATIONSINGLE", ref strSql) == -1) return null;
                    break;
            }
            return strSql;
        }
        /// <summary>
        /// ���ݲ�ѯ�����סԺ��ˮ�š�
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        public ArrayList GetInpatientNoList(string strSql, FS.HISFC.Models.HealthRecord.EnumServer.TablesName type)
        {
            ArrayList arrList = new ArrayList();
            try
            {
                this.ExecQuery(strSql);
                FS.FrameWork.Models.NeuObject info = null;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    if (type == TablesName.BASE)
                    {
                        info.ID = this.Reader[0].ToString(); //סԺ��ˮ��
                        info.User01 = this.Reader[1].ToString(); //סԺ��
                    }
                    else
                    {
                        info.ID = this.Reader[0].ToString();
                    }
                    arrList.Add(info);
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
                this.Err = ex.Message;
                arrList = null;
            }
            return arrList;
        }
        /// <summary>
        /// ������ҳ��ѯ������������ѯ���ݡ�
        /// </summary>
        /// <param name="TextStr"></param>
        /// <param name="ds"></param>
        /// <param name="InpatientNoList"></param>
        /// <returns></returns>
        public int GetInfoBySql(string TextStr,ref System.Data.DataSet ds, string InpatientNoList)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            try
            {
                switch (TextStr)
                {
                    case "��ʾ������":
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.SearchManager.GetInfoBySql.1", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.SearchManager.GetInfoBySql.1";
                            return -1;
                        }
                        break;
                    case "��ʾ�����źʹ���":
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.SearchManager.GetInfoBySql.2", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.SearchManager.GetInfoBySql.2";
                            return -1;
                        }
                        break;
                    case "��ʾ�·�ͳ��":
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.SearchManager.GetInfoBySql.3", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��, ����:Case.SearchManager.GetInfoBySql.3";
                            return -1;
                        }
                        break;
                    case "��ʾ���ͳ��":
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.SearchManager.GetInfoBySql.4", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.SearchManager.GetInfoBySql.4";
                            return -1;
                        }
                        break;
                    case "��ʾ���ͳ��(���ϼ�)":
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.SearchManager.GetInfoBySql.5", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.SearchManager.GetInfoBySql.5";
                            return -1;
                        }
                        break;
                    case "��ʾ������":
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.SearchManager.GetInfoBySql.6", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��, ����:Case.SearchManager.GetInfoBySql.6";
                            return -1;
                        }
                        break;
                    case "�����ֲ��˴α�":
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.SearchManager.GetInfoBySql.7", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.SearchManager.GetInfoBySql.7";
                            return -1;
                        }
                        break;
                    case "ְҵ���µ����":
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.SearchManager.GetInfoBySql.8", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:Case.SearchManager.GetInfoBySql.8";
                            return -1;
                        }
                        break;
                    case "��ʾ�ز�����":
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.SearchManager.GetInfoBySql.9", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��, ����:Case.SearchManager.GetInfoBySql.9";
                            return -1;
                        }
                        break;
                    case "��������ͳ�Ʊ�":
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.SearchManager.GetInfoBySql.10", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��, ����:Case.SearchManager.GetInfoBySql.10";
                            return -1;
                        }
                        break;
                    case "һ���ڸ���Ժͳ�Ʊ�":
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("Case.SearchManager.GetInfoBySql.11", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��, ����:Case.SearchManager.GetInfoBySql.10";
                            return -1;
                        }
                        break;
                }

                try
                {
                    //�齨��ѯ��� 
                    strQuerySql = string.Format(strQuerySql, InpatientNoList);
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return -1;
                }
                //ִ�в�ѯ����
                return this.ExecQuery(strQuerySql, ref ds);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message; //��ȡ������Ϣ
                return -1; //����δ����Ĵ���
            }
        }

        /// <summary>
        /// ���뱣����
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertResult(FS.FrameWork.Models.NeuObject obj)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            //��ȡ��ѯSQL���
            if (this.Sql.GetSql("Case.SearchManager.InsertResult", ref strQuerySql) == -1)
            {
                this.Err = "��ȡSQL���ʧ��, ����Case.SearchManager.InsertResult";
                return -1;
            }
            strQuerySql = string.Format(strQuerySql, obj.ID, obj.Name, obj.User01, obj.User02);
            return this.ExecNoQuery(strQuerySql);
        }
        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns></returns>
        public int DeleteResult(string sequence)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            //��ȡ��ѯSQL���
            if (this.Sql.GetSql("Case.SearchManager.DeleteResult", ref strQuerySql) == -1)
            {
                this.Err = "��ȡSQL���ʧ��, ����:Case.SearchManager.DeleteResult";
                return -1;
            }
            strQuerySql = string.Format(strQuerySql, sequence);
            return this.ExecNoQuery(strQuerySql);
        }
        public string SelectResult(string sequence)
        {
            string RetrunResult = "";
            string Result = "";
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            //��ȡ��ѯSQL���
            if (this.Sql.GetSql("Case.SearchManager.SelectResult", ref strQuerySql) == -1)
            {
                this.Err = "��ȡSQL���ʧ��, ����:Case.SearchManager.SelectResult";
                return null;
            }
            strQuerySql = string.Format(strQuerySql, sequence);
            //��ѯ
            this.ExecQuery(strQuerySql);
            while (this.Reader.Read())
            {
                if (Result == "")
                {
                    Result = this.Reader[0].ToString();
                }
                else
                {
                    Result = Result + "," + this.Reader[0].ToString();
                }
            }
            string[] str = Result.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                if (RetrunResult == "")
                {
                    RetrunResult = "'" + str[i].ToString() + "'";
                }
                else
                {
                    RetrunResult = RetrunResult + ",'" + str[i].ToString() + "'";
                }
            }
            return RetrunResult;
        }

        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="Index">sql������</param>
        /// <param name="ds">���ص��ַ���</param>
        /// <param name="strWhere">ɸѡ����</param>
        /// <returns></returns>
        public int GetSearchInfo(string Index, System.Data.DataSet ds, string strWhere)
        {
            try
            {
                string strSql = "";
                //��ȡ��ѯSQL���
                if (this.Sql.GetSql(Index, ref strSql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��, ����:Case.SearchManager.GetInfoBySql.10";
                    return -1;
                }
                strSql = strSql + strWhere;
                this.ExecQuery(strSql, ref ds);
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                ds = new System.Data.DataSet();
                return -1;
            }
        }

        /// <summary>
        /// ��ȡ��ص�sql
        /// </summary>
        /// <param name="Index"></param>
        /// <returns></returns>
        public string GetSqlByIndex(string Index)
        {
            string strSql = "";
            if (this.Sql.GetSql(Index, ref strSql) == -1) return null;
            return strSql;
        }
        /// <summary>
        /// ��ѯ��Ҫ׼�������Ĳ�����Ϣ
        /// </summary>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <returns></returns>
        public int QueryClinicPatientNeedCase(DateTime dtBegin,DateTime dtEnd ,ref DataSet ds)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            DateTime tempBeginTime = new DateTime(dtBegin.Year, dtBegin.Month, dtBegin.Day);
            DateTime tempEndTime = new DateTime(dtEnd.Year, dtEnd.Month, dtEnd.Day, 23, 59, 59);
            try
            {
                if (this.Sql.GetSql("Case.SearchManager.QueryClinicPatientNeedCase", ref strQuerySql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��, ����Case.SearchManager.QueryClinicPatientNeedCase";
                    return -1;
                }
                //�齨��ѯ��� 
                strQuerySql = string.Format(strQuerySql, tempBeginTime.ToString(), tempEndTime.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            //ִ�в�ѯ����
            return this.ExecQuery(strQuerySql, ref ds);

        }

        /// <summary>
        /// ��ѯһ��ʱ���ڣ������ҳ�Ժ���߲���������Ϣ
        /// </summary>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <returns></returns>
        public int QueryDeptCaseInfo(DateTime dtBegin, DateTime dtEnd, ref DataSet ds)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            try
            {
                if (this.Sql.GetSql("Case.SearchManager.QueryDeptCaseInfo", ref strQuerySql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��, ����Case.SearchManager.QueryDeptCaseInfo";
                    return -1;
                }
                //�齨��ѯ��� 
                strQuerySql = string.Format(strQuerySql, dtBegin.ToString(), dtEnd.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            //ִ�в�ѯ����
            return this.ExecQuery(strQuerySql, ref ds);
        }

        /// <summary>
        /// ��ѯһ��ʱ���ڣ������ҳ�Ժ����δ¼�벡����Ϣ��ϸ
        /// </summary>
        /// <param name="date">��Ժʱ��</param>
        /// <param name="dept">������ϸ</param>
        /// <returns></returns>
        public int QueryDeptCaseInfoDetail(DateTime date, string dept, ref DataSet ds)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            try
            {
                if (this.Sql.GetSql("Case.SearchManager.QueryDeptCaseInfoDetail", ref strQuerySql) == -1)
                {
                    this.Err = "��ȡSQL���ʧ��, ����Case.SearchManager.QueryDeptCaseInfoDetail";
                    return -1;
                }
                //�齨��ѯ��� 
                strQuerySql = string.Format(strQuerySql, date.ToString(), dept);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            //ִ�в�ѯ����
            return this.ExecQuery(strQuerySql, ref ds);
        }
    }

}
