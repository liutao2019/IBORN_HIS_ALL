using System;
using System.Collections.Generic;
using System.Text;
using FS.FrameWork.Function;
using System.Collections;

namespace FS.HISFC.BizLogic.HealthRecord.Visit
{
    /// <summary>
    /// VisitSearches<br></br>
    /// [��������: ��ü����������ҵ���]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-09-10]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class VisitSearches : FS.FrameWork.Management.Database
    {
        #region ���ݿ��������

        /// <summary>
        /// �����������в���һ���µļ�¼
        /// </summary>
        /// <param name="visitSearches">��ü�������ʵ��</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int Insert(FS.HISFC.Models.HealthRecord.Visit.VisitSearches visitSearches)
        {
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitSearches.Insert", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitSearches.Insert�ֶΣ�";

                return -1;
            }
            try
            {
                //��ȡ������ˮ��
                visitSearches.ID = this.GetSequence("HealthReacord.Visit.VisitRecord.GetVisitSearchesID");
                //��ȡ������ˮ���Ƿ�ɹ�
                if (visitSearches.ID == null)
                {
                    this.Err = "��ȡ��ˮ�ų���";

                    return -1;
                }
                //��ȡ���ݲ�������
                string[] strParm = this.GetVisitSearchesParmItem(visitSearches);
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;

                return -1;
            }

            //ִ��SQL��䲢����
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// �޸���ü��������¼
        /// </summary>
        /// <param name="visitSearches">��ü�������ʵ��</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int Update(FS.HISFC.Models.HealthRecord.Visit.VisitSearches visitSearches)
        {
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitSearches.Update", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitSearches.Update�ֶΣ�";

                return -1;
            }

            try
            {
                //��ȡ���ݲ�������
                string[] strParm = this.GetVisitSearchesParmItem(visitSearches);
                strSQL = string.Format(strSQL, strParm);
                
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;

                return -1;
            }

            //ִ��SQL��䲢����
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ����������ˮ��ɾ��һ����ü��������¼
        /// </summary>
        /// <param name="applyID">��ü���������ˮ��</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int Delete(string applyID)
        {
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitSearches.Delete", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitSearches.Delete�ֶΣ�";

                return -1;
            }

            try
            {
                //�������
                strSQL = string.Format(strSQL, applyID);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;

                return -1;
            }

            //ִ��SQL��䲢����
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��ȡinsert��update�Ĳ���
        /// </summary>
        /// <param name="linkway">��ü�������ʵ��</param>
        /// <returns>���ز�������</returns>
        private string[] GetVisitSearchesParmItem(FS.HISFC.Models.HealthRecord.Visit.VisitSearches visitSearcher)
        {
            string[] strParm = new string[25];

            strParm[0] = visitSearcher.ID;
            strParm[1] = visitSearcher.DoctorOper.ID;
            strParm[2] = visitSearcher.Teacher.ID;
            strParm[3] = visitSearcher.Teacher.User01;
            strParm[4] = visitSearcher.SearchesContent;
            strParm[5] = visitSearcher.DoctorOper.OperTime.ToString();
            strParm[6] = visitSearcher.BookingTime.ToString();
            if (visitSearcher.IsCharge)
            {
                strParm[7] = "1";
            }
            else
            {
                strParm[7] = "0";
            }
            strParm[8] = visitSearcher.ChargeCost.ToString();
            strParm[9] = visitSearcher.IllType.ID;
            strParm[10] = visitSearcher.Years.ToString();
            strParm[11] = visitSearcher.Items.ID;
            strParm[12] = visitSearcher.Copy;
            strParm[13] = visitSearcher.Append;
            //����״̬
            if (visitSearcher.SearchesState == FS.HISFC.Models.HealthRecord.Visit.EnumSearchesState.Apply)
            {
                strParm[14] = "0";
            }
            //���������״̬
            if (visitSearcher.SearchesState == FS.HISFC.Models.HealthRecord.Visit.EnumSearchesState.Auditing)
            {
                strParm[14] = "1";
            }
            //��Ϣ�����״̬
            if (visitSearcher.SearchesState == FS.HISFC.Models.HealthRecord.Visit.EnumSearchesState.Notion)
            {
                strParm[14] = "2";
            }
            //ȷ��״̬
            if (visitSearcher.SearchesState == FS.HISFC.Models.HealthRecord.Visit.EnumSearchesState.Searches)
            {
                strParm[14] = "3";
            }
            strParm[15] = visitSearcher.AuditingOper.ID;
            strParm[16] = visitSearcher.AuditingOper.OperTime.ToString();
            strParm[17] = visitSearcher.SearchesOper.ID;
            strParm[18] = visitSearcher.SearchesOper.OperTime.ToString();
            strParm[19] = visitSearcher.NotionOper.ID;
            strParm[20] = visitSearcher.NotionOper.User01;
            strParm[21] = visitSearcher.NotionOper.OperTime.ToString();
            strParm[22] = visitSearcher.User01;
            strParm[23] = visitSearcher.User02;
            strParm[24] = visitSearcher.User03;
           
            //��������
            return strParm;
        }

        #endregion

        #region ��ѯ

        /// <summary>
        /// ͨ��״̬��ҽ�����������������¼
        /// </summary>
        /// <param name="docCode">ҽ������</param>
        /// <param name="searchesStat">����״̬</param>
        /// <returns>�������顢���󷵻�null</returns>
        public ArrayList QueryByDocCode(string docCode, string searchesStat)
        {
            string strSQL = "";
            string strWHERE = "";

            //��ȡSELECT���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitSearches.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitSearches.Select�ֶΣ�";

                return null;
            }
            //��ȡWHWRE����
            if (this.Sql.GetSql("HealthReacord.Visit.VisitSearches.SelectWhereByDocCodeAndStat", ref strWHERE) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitSearches.SelectWhereByDocCodeAndStat�ֶΣ�";

                return null;
            }
            try
            {
                strSQL = strSQL + "\n" + strWHERE;
                //�������
                strSQL = string.Format(strSQL, docCode, searchesStat);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����";

                return null;
            }
 
            //��������
            return this.ReadVisitSearchesInfo(strSQL);
        }

        /// <summary>
        /// ͨ��״̬������������¼
        /// </summary>
        /// <param name="searchesStat">����״̬</param>
        /// <returns>�������顢���󷵻�null</returns>
        public ArrayList QueryByStat(string searchesStat)
        {
            return this.QueryByDocCode("A", searchesStat);
        }

        /// <summary>
        /// ͨ��������ˮ�ż�����������¼
        /// </summary>
        /// <param name="applyId"></param>
        /// <returns></returns>
        public ArrayList QueryByApplyId(string applyId)
        {
            string strSQL = "";
            string strWHERE = "";

            //��ȡSELECT���
            if (this.Sql.GetSql("HealthReacord.Visit.VisitSearches.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitSearches.Select�ֶΣ�";

                return null;
            }
            //��ȡWHWRE����
            if (this.Sql.GetSql("HealthReacord.Visit.VisitSearches.SelectWhereByApplyId", ref strWHERE) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.VisitSearches.SelectWhereByApplyId�ֶΣ�";

                return null;
            }
            try
            {
                strSQL = strSQL + "\n" + strWHERE;
                //�������
                strSQL = string.Format(strSQL, applyId);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����";

                return null;
            }

            //��������
            return this.ReadVisitSearchesInfo(strSQL);
        }

        /// <summary>
        /// ͨ��ִ��SQL��䣬����ѯ������Ϣ����ArrayList��
        /// </summary>
        /// <param name="strSQL">��Ҫִ�е�SQL���</param>
        /// <returns>���ض�ȡ�������顢���󷵻�null</returns>
        private ArrayList ReadVisitSearchesInfo(string strSQL)
        {
            //ִ��SQL���
            this.ExecQuery(strSQL);

            ArrayList list = new ArrayList();
            FS.HISFC.Models.HealthRecord.Visit.VisitSearches visitSearches = null;
            try
            {
                while (this.Reader.Read())
                {
                    visitSearches = new FS.HISFC.Models.HealthRecord.Visit.VisitSearches();
                    visitSearches.ID = this.Reader[0].ToString();
                    visitSearches.DoctorOper.ID = this.Reader[1].ToString();
                    visitSearches.Teacher.ID = this.Reader[2].ToString();
                    visitSearches.Teacher.User01 = this.Reader[3].ToString();
                    visitSearches.SearchesContent = this.Reader[4].ToString();
                    visitSearches.DoctorOper.OperTime = NConvert.ToDateTime(this.Reader[5].ToString());
                    visitSearches.BookingTime = NConvert.ToDateTime(this.Reader[6].ToString());
                    //�Ƿ��շ�
                    if (this.Reader[7].ToString() == "1")
                    {
                        visitSearches.IsCharge = true;
                    }
                    else
                    {
                        visitSearches.IsCharge = false;
                    }
                    visitSearches.ChargeCost = NConvert.ToDecimal(this.Reader[8].ToString());
                    visitSearches.IllType.ID = this.Reader[9].ToString();
                    visitSearches.Years = NConvert.ToDecimal(this.Reader[10].ToString());
                    visitSearches.Items.ID = this.Reader[11].ToString();
                    visitSearches.Copy = this.Reader[12].ToString();
                    visitSearches.Append = this.Reader[13].ToString();
                    string stat = this.Reader[14].ToString();
                    if (stat == "0")
                    {
                        //����
                        visitSearches.SearchesState = FS.HISFC.Models.HealthRecord.Visit.EnumSearchesState.Apply;
                    }
                    else
                    {
                        if (stat == "1")
                        {
                            //���������
                            visitSearches.SearchesState = FS.HISFC.Models.HealthRecord.Visit.EnumSearchesState.Auditing;
                        }
                        else
                        {
                            if (stat == "2")
                            {
                                //��Ϣ�Ƴ����
                                visitSearches.SearchesState = FS.HISFC.Models.HealthRecord.Visit.EnumSearchesState.Notion;
                            }
                            else
                            {
                                //����ȷ��
                                visitSearches.SearchesState = FS.HISFC.Models.HealthRecord.Visit.EnumSearchesState.Searches;
                            }
                        }
                    }
                    visitSearches.AuditingOper.ID = this.Reader[15].ToString();
                    visitSearches.AuditingOper.OperTime = NConvert.ToDateTime(this.Reader[16].ToString());
                    visitSearches.SearchesOper.ID = this.Reader[17].ToString();
                    visitSearches.SearchesOper.OperTime = NConvert.ToDateTime(this.Reader[18].ToString());
                    visitSearches.NotionOper.ID = this.Reader[19].ToString();
                    visitSearches.NotionOper.User01 = this.Reader[20].ToString();
                    visitSearches.NotionOper.OperTime = NConvert.ToDateTime(this.Reader[21].ToString());
                    visitSearches.User01 = this.Reader[22].ToString();
                    visitSearches.User02 = this.Reader[23].ToString();
                    visitSearches.User03 = this.Reader[24].ToString();

                    //��ʵ����ӵ�������
                    list.Add(visitSearches);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ȡ��ü����������ݳ���" + ex.Message;

                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            //��������
            return list;
        }

        #endregion
    }
}
