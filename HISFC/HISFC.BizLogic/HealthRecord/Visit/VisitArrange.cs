using System;
using System.Collections.Generic;
using System.Text;
using FS.HISFC.Models.HealthRecord;
using FS.FrameWork.Function;
using System.Data;
using System.Collections;

namespace FS.HISFC.BizLogic.HealthRecord.Visit
{
    /// <summary>
    /// VisitArrange<br></br>
    /// [��������: ��ð���ҵ���]<br></br>
    /// [�� �� ��: ���S]<br></br>
    /// [����ʱ��: 2008-08-25]<br></br>
    /// <�޸ļ�¼
    ///		�޸���=''
    ///		�޸�ʱ��='yyyy-mm-dd'
    ///		�޸�Ŀ��=''
    ///		�޸�����=''
    ///  />
    /// </summary>
    public class VisitArrange : FS.FrameWork.Management.Database
    {
        #region ˽�з���

        /// <summary>
        /// ����sql��������ð���ʵ���ȡsql���
        /// </summary>
        /// <param name="sqlIndex"></param>
        /// <param name="visitArrange"></param>
        private string GetStrSql(string sqlIndex)
        {
            string strSQL = "";

            if ( this.Sql.GetSql(sqlIndex, ref strSQL) == -1 )
            {
                this.Err = "û���ҵ�" + sqlIndex + "�ֶΣ�";
                return null;
            }

            return strSQL;
        }

        /// <summary>
        /// ����ʵ���ȡsql����
        /// </summary>
        /// <param name="visitArrange"></param>
        /// <returns></returns>
        private string[] GetParam(FS.HISFC.Models.HealthRecord.Visit.VisitArrange visitArrange)
        {
            string[] strParm = new string[12];

            strParm[0] = visitArrange.CardNO;
            strParm[1] = visitArrange.PatientName;
            strParm[2] = visitArrange.LastDate.ToString();
            strParm[3] = visitArrange.VisitTimes.ToString();
            strParm[4] = visitArrange.State;
            strParm[5] = visitArrange.VisitOper.ID;
            strParm[6] = visitArrange.VisitOper.OperTime.ToString();
            strParm[7] = visitArrange.Oper.ID;
            strParm[8] = visitArrange.Oper.OperTime.ToString();
            strParm[9] = visitArrange.User01;
            strParm[10] = visitArrange.User02;
            strParm[11] = visitArrange.User03;

            //��������
            return strParm;
        }

        /// <summary>
        /// ִ��sql ����arraylist ���ڲ�ѯ��Ҫ��ð��ŵĻ���
        /// </summary>
        /// <param name="preDay"></param>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList ExecSql(ref string strSql, params object[] args)
        {
            try
            {
                strSql = string.Format(strSql, args);
            }
            catch ( Exception ex )
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                return null;
            }
            try
            {
                this.ExecQuery(strSql);

                ArrayList all = new ArrayList();
                while ( this.Reader.Read() )
                {
                    ArrayList al = new ArrayList();
                    al.Add(this.Reader[0]);//cardNO
                    al.Add(this.Reader[1]);//patientName
                    al.Add(this.Reader[2]);//lastTime
                    al.Add(this.Reader[3]);//visitTimes
                    al.Add(this.Reader[4]);//patientType
                    all.Add(al);
                }
                return all;
            }
            catch ( Exception ex )
            {
                this.Err = "ִ��sql���ʧ�ܣ�" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ���ݲ�ѯ�õ���ð��ŷ���
        /// </summary>
        /// <param name="strSql"></param>
        /// <param name="dtBegin"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private List<FS.HISFC.Models.HealthRecord.Visit.VisitArrange> GetVisitArrange(string strSql, params object[] args)
        {
            try
            {
                strSql = string.Format(strSql, args);

                this.ExecQuery(strSql);

                List<FS.HISFC.Models.HealthRecord.Visit.VisitArrange> list = new List<FS.HISFC.Models.HealthRecord.Visit.VisitArrange>();
                while ( this.Reader.Read() )
                {
                    FS.HISFC.Models.HealthRecord.Visit.VisitArrange va = new FS.HISFC.Models.HealthRecord.Visit.VisitArrange();

                    va.CardNO = this.Reader[0].ToString();
                    va.PatientName = this.Reader[1].ToString();
                    va.LastDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[2].ToString());
                    va.VisitTimes = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                    va.State = this.Reader[4].ToString();
                    va.VisitOper.ID = this.Reader[5].ToString();
                    va.VisitOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString());
                    va.Oper.ID = this.Reader[7].ToString();
                    va.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8].ToString());
                    va.User01 = this.Reader[9].ToString();
                    va.User02 = this.Reader[10].ToString();
                    va.User03 = this.Reader[11].ToString();

                    list.Add(va);
                }
                return list;
            }
            catch ( Exception ex )
            {
                this.Err = "ִ��sql���ʧ�ܣ�" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }
        #endregion

        #region ���з���

        /// <summary>
        /// ������ð��ż�¼
        /// </summary>
        public int Insert(FS.HISFC.Models.HealthRecord.Visit.VisitArrange visitArrange)
        {
            string strSQL = this.GetStrSql("HealthReacord.Visit.VisitArrange.Insert");

            try
            {
                string[] strParm = this.GetParam(visitArrange);
                strSQL = string.Format(strSQL, strParm);
            }
            catch ( Exception ex )
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                return -1;
            }

            //��ִ��SQL������
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// ������ð��ż�¼
        /// </summary>
        /// <param name="visitArrange">��ð���</param>
        /// <returns>Ӱ���������-1:ʧ��</returns>
        public int Update(FS.HISFC.Models.HealthRecord.Visit.VisitArrange visitArrange)
        {
            string strSQL = this.GetStrSql("HealthReacord.Visit.VisitArrange.Update");

            try
            {
                string[] strParm = this.GetParam(visitArrange);
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
        /// ��ѯ����δ��û���
        /// </summary>
        /// <param name="preDay"></param>
        /// <returns></returns>
        public ArrayList QueryOutpatient(int preDay, int day)
        {
            string strSql = this.GetStrSql("HealthReacord.Visit.VisitArrange.QueryOutpatient");

            return ExecSql(ref strSql, preDay, day);
        }

        /// <summary>
        /// ��ѯסԺδ��û���
        /// </summary>
        /// <param name="preDay"></param>
        /// <returns></returns>
        public ArrayList QueryInpatient(int preDay, int day)
        {
            string strSql = this.GetStrSql("HealthReacord.Visit.VisitArrange.QueryInpatient");

            return ExecSql(ref strSql, preDay, day);
        }

        /// <summary>
        /// ��ѯ����û���
        /// </summary>
        /// <param name="preDay"></param>
        /// <returns></returns>
        public ArrayList GetVisitedPatient(string cardNO, int preDay, int day)
        {
            string strSql = this.GetStrSql("HealthReacord.Visit.VisitArrange.QueryVisitedPatient");

            try
            {
                strSql = string.Format(strSql, cardNO, preDay, day);
            }
            catch ( Exception ex )
            {
                this.Err = "��ֵʱ�����" + ex.Message;
                return null;
            }
            try
            {
                this.ExecQuery(strSql);

                ArrayList al = new ArrayList();
                while ( this.Reader.Read() )
                {
                    al.Add(this.Reader[0]);//cardNO
                    al.Add(this.Reader[1]);//patientName
                    al.Add(this.Reader[2]);//lastTime
                    al.Add(this.Reader[3]);//visitTimes
                    al.Add(this.Reader[4]);//patientType
                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err = "ִ��sql���ʧ�ܣ�" + ex.Message;

                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ��ѯÿ�����ߵ���ô���
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryVisitTimes()
        {
            string strSql = this.GetStrSql("HealthReacord.Visit.VisitArrange.QueryVisitTimes");

            try
            {
                this.ExecQuery(strSql);

                ArrayList al = new ArrayList();
                while ( this.Reader.Read() )
                {
                    ArrayList all = new ArrayList();
                    all.Add(this.Reader[0]);//cardNO
                    all.Add(this.Reader[1]);//count(cardNO)
                    al.Add(all);
                }
                return al;
            }
            catch ( Exception ex )
            {
                this.Err = "ִ��sql���ʧ�ܣ�" + ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// ��ѯ��ð�����ʷ��¼
        /// </summary>
        /// <param name="dtBegin"></param>
        /// <param name="endTime"></param>
        /// <param name="visitorID"></param>
        /// <param name="operType"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.HealthRecord.Visit.VisitArrange> QueryHistoryArrange(DateTime dtBegin, DateTime endTime, string visitorID, string operType)
        {
            string strSql = this.GetStrSql("HealthReacord.Visit.VisitArrange.QueryHistoryArrange");

            return this.GetVisitArrange(strSql, dtBegin, endTime, visitorID, operType);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemID"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public ArrayList QueryPatientByItem(string itemID, DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = this.GetStrSql("HealthReacord.Visit.VisitArrange.QueryPatientByItem");

            return this.ExecSql(ref strSql, itemID, dtBegin, dtEnd);
        }
        #endregion

    }
}
