using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.HealthRecord;
using FS.FrameWork.Function;
using System.Data;
using System.Collections;
using FS.HISFC.Models.HealthRecord.EnumServer;

namespace FS.HISFC.BizLogic.HealthRecord.Visit
{
    /// <summary>
    /// Visit<br></br>
    /// [��������: �������¼����ҵ���]<br></br>
    /// [�� �� ��: ����]<br></br>
    /// [����ʱ��: 2007-08-21]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=���
    ///		�޸�ʱ��='2009-09-08'
    ///		�޸�Ŀ��='������ù���'
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class Visit : FS.FrameWork.Management.Database
    {
        #region ���ݿ��������

        /// <summary>
        /// �����������¼
        /// </summary>
        /// <param name="visit">�������¼��</param>
        /// <returns>Ӱ���������-1 ʧ��</returns>
        public int Insert(FS.HISFC.Models.HealthRecord.Visit.Visit visit)
        {
            string strSQL = "";

            if(this.Sql.GetSql("HealthReacord.Visit.Vist.Insert", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.Vist.Insert�ֶΣ�";
                return -1;
            }

            try
            {
                string[] strParm = GetVisitParmItem(visit);
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                return -1;
            }

            //��ִ��SQL������
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// �����������¼
        /// </summary>
        /// <param name="visit">�������¼��</param>
        /// <returns>Ӱ���������-1��ʧ��</returns>
        public int Update(FS.HISFC.Models.HealthRecord.Visit.Visit visit)
        {
            string strSQL = "";

            if (this.Sql.GetSql("HealthReacord.Visit.Vist.Update", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.Vist.Update�ֶΣ�";
                return -1;
            }
            try
            {
                string[] strParm = GetVisitParmItem(visit);
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                return -1;
            }

            //��ִ��SQL��䷵��
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ��ĳ�����ߵ����״̬��Ϊֹͣ���
        /// </summary>
        /// <param name="cardNo">������</param>
        /// <returns>1 �ɹ���-1��ʧ��</returns>
        public int UpdateStat(string cardNo)
        {
            string strSQL = "";

            if (this.Sql.GetSql("HealthReacord.Visit.Vist.UpdateStat", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.Vist.UpdateStat�ֶΣ�";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, cardNo);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                return -1;
            }

            //��ִ��SQL���
            if (this.ExecNoQuery(strSQL) != 1)
            {
                this.Err = "���µĲ���һ����¼!";

                return -1;
            }
            else
            {
                return 1;
            }
        }

        /// <summary>
        /// ���update����insert��ñ�Ĵ����������
        /// </summary>
        /// <param name="company">�������¼��Ϣ</param>
        /// <returns>��������</returns>
        private string[] GetVisitParmItem(FS.HISFC.Models.HealthRecord.Visit.Visit visit)
        {
            string[] strParm = new string[16];

            strParm[0] = visit.Patient.PID.CardNO;
            strParm[1] = visit.Linkway.Address;
            strParm[2] = visit.Linkway.Mail;
            strParm[3] = visit.Linkway.Phone;
            strParm[4] = visit.LastVisitTime.ToString();
            strParm[5] = visit.Linkway.LinkWayType.ID;
            strParm[6] = visit.Linkway.ZIP;
            if (visit.VisitState == FS.HISFC.Models.HealthRecord.Visit.EnumVisitState.Normal)
            {
                strParm[7] = "1";
            }
            else
            {
                strParm[7] = "0";
            }
            if (visit.LastIsPassive)
            {
                strParm[8] = "1";
            }
            else
            {
                strParm[8] = "0";
            }
            strParm[9] = visit.Linkway.OtherLinkway;
            strParm[10] = visit.Linkway.LinkMan.Name;
            if (visit.Linkway.IsLinkMan)
            {
                strParm[11] = "1";
            }
            else
            {
                strParm[11] = "0";
            }
            strParm[12] = visit.Linkway.Relation.ID;
            strParm[13] = visit.User01;
            strParm[14] = visit.User02;
            strParm[15] = visit.User03;

            //��������
            return strParm;             
        }

        #endregion

        #region ��ѯ

        /// <summary>
        ///�����ݲ����Ż�ȡ���ߵ��������¼
        /// </summary>
        /// <param name="visit">�������¼��</param>
        /// <param name="cardNo">���߲�����</param>
        /// <returns>1-�ɹ���0-û�в�ѯ�������-1-ʧ��</returns>
        public int Select(ref FS.HISFC.Models.HealthRecord.Visit.Visit visit, string cardNo)
        {
            string strSQL = "";

            //��ȡSQL���
            if (this.Sql.GetSql("HealthReacord.Visit.Visit.Select", ref strSQL) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.Visit.Select�ֶΣ�";
                return -1;
            }
            try
            {
                //���ݲ����Ų���
                strSQL = string.Format(strSQL, cardNo);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return -1;
            }

            ArrayList alVisit = new ArrayList();

            this.ExecQuery(strSQL);

            try
            {
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.HealthRecord.Visit.Visit visitTemp = new FS.HISFC.Models.HealthRecord.Visit.Visit();

                    visitTemp.Patient.PID.CardNO = this.Reader[0].ToString();
                    visitTemp.Linkway.Address = this.Reader[1].ToString();
                    visitTemp.Linkway.Mail = this.Reader[2].ToString();
                    visitTemp.Linkway.Phone = this.Reader[3].ToString();
                    visitTemp.LastVisitTime = NConvert.ToDateTime(this.Reader[4].ToString());
                    visitTemp.Linkway.LinkWayType.ID = this.Reader[5].ToString();
                    visitTemp.Linkway.ZIP = this.Reader[6].ToString();
                    //���״̬
                    if (this.Reader[7].ToString().Equals("0"))
                    {
                        visitTemp.VisitState = FS.HISFC.Models.HealthRecord.Visit.EnumVisitState.Stop;
                    }
                    else
                    {
                        visitTemp.VisitState = FS.HISFC.Models.HealthRecord.Visit.EnumVisitState.Normal;
                    }
                    if (this.Reader[8].ToString().Equals("1"))
                    {
                        visitTemp.LastIsPassive = true;
                    }
                    else
                    {
                        visitTemp.LastIsPassive = false;
                    }
                    visitTemp.Linkway.OtherLinkway = this.Reader[9].ToString();
                    visitTemp.Linkway.LinkMan.Name = this.Reader[10].ToString();
                    if (this.Reader[11].ToString().Equals("1"))
                    {
                        visitTemp.Linkway.IsLinkMan = true;
                    }
                    else
                    {
                        visitTemp.Linkway.IsLinkMan = false;
                    }
                    visitTemp.Linkway.Relation.ID = this.Reader[12].ToString();
                    visitTemp.User01 = this.Reader[13].ToString();
                    visitTemp.User02 = this.Reader[14].ToString();
                    visitTemp.User03 = this.Reader[15].ToString();
                    visitTemp.Linkway.LinkWayType.Name = this.Reader[16].ToString();
                    visitTemp.Linkway.Relation.Name = this.Reader[17].ToString();

                    alVisit.Add(visitTemp);
                }
            }
            catch (Exception ex)
            {
                this.Err = "��ȡ�������¼����" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }

            if (alVisit.Count == 0)
            {
                return 0;
            }
            else if (alVisit.Count == 1)
            {
                visit = alVisit[0] as FS.HISFC.Models.HealthRecord.Visit.Visit;

                return 1;
            }
            else
            {
                this.Err = "���ڶ�����¼��";

                return -1;
            }
        }

        /// <summary>
        /// ���벡�����жϸû����Ƿ��Ѿ�ֹͣ���
        /// </summary>
        /// <param name="cardNo">������</param>
        /// <returns>-1 ʧ�ܡ�0 ֹͣ��á�1 �������</returns>
        public int IsVisitStop(string cardNo)
        {
            FS.HISFC.Models.HealthRecord.Visit.Visit visit = new FS.HISFC.Models.HealthRecord.Visit.Visit();

            int intReturn = this.Select(ref visit, cardNo);
            if (intReturn == -1 || intReturn == 0)
            {
                return -1;
            }

            if (visit.VisitState == FS.HISFC.Models.HealthRecord.Visit.EnumVisitState.Stop)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        #endregion

        #region {E9F858A6-BDBC-4052-BA57-68755055FB80}

        /// <summary>
        /// ��ѯ�ط�ICD�б�
        /// </summary>
        /// <param name="ICDType">�������ö��</param>
        /// <param name="ds">�������������ݼ�</param>
        /// <returns>����δ֪���� ���� -1 �ɹ����� 1</returns>
        public int QueryVisitICD(ICDTypes ICDType, ref DataSet ds)
        {
            //�����ַ����� ,�洢��ѯ����SQL���
            string strQuerySql = "";
            //�����ַ�����, �洢��ѯ����
            try
            {
                switch (ICDType)
                {
                    case ICDTypes.ICD10:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("HealthReacord.Visit.Query.ICD10", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:HealthReacord.Visit.Query.ICD10";
                            return -1;
                        }
                        break;
                    case ICDTypes.ICD9:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("HealthReacord.Visit.Query.ICD9", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��,����:HealthReacord.Visit.Query.ICD9";
                            return -1;
                        }
                        break;
                    case ICDTypes.ICDOperation:
                        //��ȡ��ѯSQL���
                        if (this.Sql.GetSql("HealthReacord.Visit.Query.ICDoperation", ref strQuerySql) == -1)
                        {
                            this.Err = "��ȡSQL���ʧ��, ����:HealthReacord.Visit.Query.ICDoperation";
                            return -1;
                        }
                        break;
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
        /// �������ICD��Χ
        /// </summary>
        /// <param name="begin">��ʼICD����</param>
        /// <param name="end">����ICD����</param>
        /// <returns>�ɹ����� 0 ; ʧ�ܷ��� -1</returns>
        public int InsertVisitICD(string begin,string end)
        {

            string strSql = string.Empty;

            if (begin != end)
            {
                string headStr = begin.Substring(0, 1).ToUpper();

                string beginInt = begin.Substring(1, begin.IndexOf('.') - 1) + begin.Substring(begin.IndexOf('.') + 1, 3);

                string endInt = end.Substring(1, end.IndexOf('.') - 1) + end.Substring(end.IndexOf('.') + 1, 3);

                if (this.Sql.GetSql("HealthReacord.Visit.Insert.VISITICD10", ref strSql) == -1)
                {
                    this.Err = "û���ҵ�HealthReacord.Visit.Insert.VISITICD10�ֶΣ�";
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql, headStr, beginInt, endInt, begin + "-" + end,
                        this.Operator.ID, this.GetSysDateTime());
                }
                catch (Exception ex)
                {
                    this.Err = "��ֵʱ����" + ex.Message;
                    return -1;
                }
            }
            else
            {
                if (this.Sql.GetSql("HealthReacord.Visit.Insert.VISITONEICD10", ref strSql) == -1)
                {
                    this.Err = "û���ҵ�HealthReacord.Visit.Insert.VISITONEICD10�ֶΣ�";
                    return -1;
                }
                try
                {
                    strSql = string.Format(strSql,begin, begin,
                        this.Operator.ID, this.GetSysDateTime());
                }
                catch (Exception ex)
                {
                    this.Err = "��ֵʱ����" + ex.Message;
                    return -1;
                }
            }


            return this.ExecQuery(strSql);

        }

        /// <summary>
        /// ɾ�����ICD
        /// </summary>
        /// <param name="icdNo">icd��ˮ��</param>
        /// <returns>�ɹ����� 0 ; ʧ�ܷ��� -1</returns>
        public int DelVisitICD(string icdNo)
        {
            string strSql = "";

            if (this.Sql.GetSql("HealthReacord.Visit.Delete.VISITICD10", ref strSql) == -1)
            {
                this.Err = "û���ҵ�HealthReacord.Visit.Delete.VISITICD10�ֶΣ�";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, icdNo);
            }
            catch (Exception ex)
            {
                this.Err = "��ֵʱ����" + ex.Message;
                return -1;
            }


            return this.ExecQuery(strSql);
        }

        

        #endregion
    }
}
