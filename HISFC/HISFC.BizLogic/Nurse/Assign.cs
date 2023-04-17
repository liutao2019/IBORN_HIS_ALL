using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Nurse
{
    /// <summary>
    /// ���������
    /// </summary>
    public class Assign : FS.FrameWork.Management.Database
    {
        public Assign()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        protected FS.HISFC.Models.Nurse.Assign assign = null;

        //protected ArrayList al = null;

        #region ��������

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QueryAssignBase(string whereSql, params object[] args)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql(whereSql, ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[" + whereSql + "]";
                this.ErrCode = "��ѯsql����,����Ϊ[" + whereSql + "]";
                return null;
            }

            try
            {
                where = string.Format(where, args);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// ������Ϣ������ѯ
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList Query(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "��ѯ������Ϣ����!" + sql;
                return null;
            }

            ArrayList al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    #region ��ֵ
                    this.assign = new FS.HISFC.Models.Nurse.Assign();

                    //������ʿվ����
                    assign.Queue.Dept.ID = this.Reader[0].ToString();

                    //�����
                    this.assign.Register.ID = this.Reader[2].ToString();

                    //�������
                    this.assign.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                    this.assign.Register.DoctorInfo.SeeNO = this.assign.SeeNO;

                    //������
                    this.assign.Register.PID.CardNO = this.Reader[4].ToString();
                    this.assign.Register.Card.ID = this.assign.Register.PID.CardNO;
                    //�Һ�����
                    this.assign.Register.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                    //��������
                    this.assign.Register.Name = this.Reader[6].ToString();
                    //�Ա�
                    this.assign.Register.Sex.ID = this.Reader[7].ToString();
                    this.assign.Register.Sex.ID = this.assign.Register.Sex.ID;
                    //�������
                    this.assign.Register.Pact.PayKind.ID = this.Reader[8].ToString();
                    //�Ƿ���
                    this.assign.Register.DoctorInfo.Templet.RegLevel.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    //�Ƿ�ԤԼ
                    this.assign.Register.RegType = (FS.HISFC.Models.Base.EnumRegType)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]));
                    //�������
                    this.assign.Queue.Dept.ID = this.Reader[11].ToString();
                    this.assign.Queue.Dept.Name = this.Reader[12].ToString();

                    this.assign.Register.DoctorInfo.Templet.Dept = this.assign.Queue.Dept.Clone();
                    //��������
                    this.assign.Queue.SRoom.ID = this.Reader[14].ToString();
                    this.assign.Queue.SRoom.Name = this.Reader[16].ToString();
                    //�������
                    this.assign.Queue.ID = this.Reader[15].ToString();
                    this.assign.Queue.Name = this.Reader[13].ToString();
                    //����ҽ��
                    this.assign.Queue.Doctor.ID = this.Reader[17].ToString();

                    this.assign.Register.DoctorInfo.Templet.Doct = this.assign.Queue.Doctor.Clone();

                    //����ʱ��
                    this.assign.SeeTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
                    //����״̬
                    this.assign.TriageStatus = (FS.HISFC.Models.Nurse.EnuTriageStatus)
                                                    FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19].ToString());

                    //�������
                    this.assign.TriageDept = this.Reader[20].ToString();
                    //����ʱ��
                    this.assign.TirageTime = this.Reader.GetDateTime(21);
                    //����ʱ��
                    if (!this.Reader.IsDBNull(22))
                        this.assign.InTime = this.Reader.GetDateTime(22);
                    //����ʱ��
                    if (!this.Reader.IsDBNull(23))
                        this.assign.OutTime = this.Reader.GetDateTime(23);
                    //����Ա
                    this.assign.Oper.ID = this.Reader[24].ToString();
                    this.assign.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[25].ToString());
                    //��̨��Ϣ
                    this.assign.Queue.Console.ID = this.Reader[26].ToString();
                    this.assign.Queue.Console.Name = this.Reader[27].ToString();

                    if (Reader.FieldCount > 28)
                    {
                        assign.Register.InputOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[28]);
                    }
                    #endregion

                    al.Add(this.assign);
                }
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "��ѯ������Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            finally
            {
                if (this.Reader != null && !Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

            return al;
        }

        #endregion

        #region ��ѯ

        /// <summary>
        /// ����״̬��ѯ���һ�����ﻼ��
        /// </summary>
        /// <param name="nuerseID">���</param>
        /// <param name="today"></param>
        /// <param name="queueID">�������ID</param>
        /// <param name="status">����״̬</param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryLastAssignPatient(string nuerseID, DateTime today, string queueID, string status)
        {
            ArrayList al = this.QueryAssignBase("Nurse.Assign.Query.LastAssign", nuerseID, today, queueID, FS.FrameWork.Function.NConvert.ToInt32(status));
            if (al != null && al.Count > 0)
            {
                return al[0] as FS.HISFC.Models.Nurse.Assign;
            }
            return null;
        }

        /// <summary>
        /// �кź���½к�״̬
        /// </summary>
        /// <param name="consoleCode">��̨����</param>
        /// <param name="clinicID">�����</param>
        /// <param name="outDate">�������</param>
        /// <param name="doctID">ҽ������</param>
        /// <returns></returns>
        public int UpdateByCalled(string consoleCode, string clinicID, DateTime outDate, string doctID)
        {
            string sql = "";
            //{A614F96F-CF88-4ac8-81F1-FE21B7B08E59}
            if (this.Sql.GetCommonSql("Nurse.Assign.Update.ByCalled", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, outDate.ToString(), doctID);
            }
            catch (Exception e)
            {
                this.Err = "���·�����Ϣ�����![Nurse.Assign.Update.ByCalled]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ��ʱ�䡢���С�������Ҳ�ѯ������Ϣ(��Ҫ������Ч��)
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="queueID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, DateTime today, string queueID,
            FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.2", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.2]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.2]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, today.ToString(), queueID, (int)status);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// ������ѯ ��ʱ�䡢���С�������Ҳ�ѯ������Ϣ(��Ҫ������Ч��)
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="queueID"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList BatchQuery(string nurseID, DateTime today, string queueID, params FS.HISFC.Models.Nurse.EnuTriageStatus[] status)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.BatchQuery", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.BatchQuery]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.BatchQuery]";
                return null;
            }

            try
            {
                string strStatus = "''";
                for (int i = 0; i < status.Length; i++)
                {
                    strStatus = strStatus + ",'" + ((Int32)status[i]).ToString() + "'";
                }

                where = string.Format(where, nurseID, today.ToString(), queueID, strStatus);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// ��ʱ�䡢���ҡ�������Ҳ�ѯ������Ϣ
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, DateTime today, FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.4", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.4]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.4]";
                return null;
            }

            try
            {
                where = string.Format(where, nurseID, today.ToString(), (int)status);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// ��ʱ�䡢���ҡ�������Ҳ�ѯ������Ϣ
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="today"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public ArrayList QueryUnionRegister(string nurseID, DateTime today, FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.QuerySecond.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.QuerySecond.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.QuerySecond.1]";
                return null;
            }


            try
            {
                sql = string.Format(sql, nurseID, today.ToString(), (int)status);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }


            return this.QuerySecond(sql);
        }

        /// <summary>
        /// ��ѯ���е�ǰ�������,����ֻҪ��ֵ:ID
        /// </summary>
        /// <param name="queueID"></param>
        /// <returns></returns>
        public int Query(FS.FrameWork.Models.NeuObject queue)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.3", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.3]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.3]";
                return -1;
            }

            try
            {
                sql = string.Format(sql, queue.ID);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            string rtn = this.ExecSqlReturnOne(sql, "0");

            if (rtn == "") rtn = "0";

            return FS.FrameWork.Function.NConvert.ToInt32(rtn);
        }

        /// <summary>
        /// ���������Ҳ�ѯ���ﻼ��
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public ArrayList Query(string deptID, string roomID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.5", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.5]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.5]";
                return null;
            }

            FS.HISFC.BizLogic.Nurse.Dept dept = new Dept();

            string nurseID = dept.GetNurseByDeptID(deptID);

            try
            {
                where = string.Format(where, nurseID, roomID);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// ��������̨��ѯ���ﻼ��
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="roomID"></param>
        /// <returns></returns>
        public ArrayList QueryByConsole(string deptID, string consoleID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.7", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.7]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.7]";
                return null;
            }

            FS.HISFC.BizLogic.Nurse.Dept dept = new Dept();

            string nurseID = dept.GetNurseByDeptID(deptID);

            try
            {
                where = string.Format(where, nurseID, consoleID);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// ��ѯ�ѿ��ﻼ��
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public ArrayList Query(DateTime begin, DateTime end, string doctID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.6", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.6]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.6]";
                return null;
            }

            try
            {
                where = string.Format(where, begin.ToString(), end.ToString(), doctID);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="queueCode"></param>
        /// <returns></returns>
        public ArrayList QueryByQueueCode(string queueCode)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.9", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.9]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.9]";
                return null;
            }

            try
            {
                where = string.Format(where, queueCode);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        /// <summary>
        /// �����Ѿ����ﵫ���Ѿ��������ķ����¼
        /// </summary>
        /// <param name="strFromDate">��ʼʱ��</param>
        /// <param name="strToDate">����ʱ��</param>
        /// <param name="nurseID">���д���</param>
        /// <param name="noonID">������</param>
        /// <returns></returns>
        public ArrayList QueryUnInSee(string strFromDate, string strToDate, string nurseID, string noonID)
        {

            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.QueryUnInSee.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.QueryUnInSee.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.QueryUnInSee.1]";
                return null;
            }
            try
            {
                sql = string.Format(sql, strFromDate, strToDate, nurseID, noonID);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "��ѯ������Ϣ����!" + sql;
                return null;
            }

            ArrayList al = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    #region ��ֵ
                    this.assign = new FS.HISFC.Models.Nurse.Assign();

                    this.assign.Register.ID = this.Reader[0].ToString();//�����
                    this.assign.Queue.ID = this.Reader[1].ToString();//����ID

                    #endregion

                    al.Add(this.assign);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "��ѯ������Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;

        }

        protected ArrayList QuerySecond(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "��ѯ������Ϣ����!" + sql;
                return null;
            }

            ArrayList al = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    #region ��ֵ
                    this.assign = new FS.HISFC.Models.Nurse.Assign();

                    //�����
                    this.assign.Register.ID = this.Reader[2].ToString();

                    //�������

                    this.assign.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                    this.assign.Register.DoctorInfo.SeeNO = this.assign.SeeNO;

                    //������
                    this.assign.Register.PID.CardNO = this.Reader[4].ToString();
                    this.assign.Register.Card.ID = this.assign.Register.PID.CardNO;
                    //�Һ�����
                    this.assign.Register.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                    //��������
                    this.assign.Register.Name = this.Reader[6].ToString();
                    //�Ա�
                    this.assign.Register.Sex.ID = this.Reader[7].ToString();
                    this.assign.Register.Sex.ID = this.assign.Register.Sex.ID;
                    //�������
                    this.assign.Register.Pact.PayKind.ID = this.Reader[8].ToString();
                    //�Ƿ���
                    //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
                    //this.assign.Register.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    this.assign.Register.DoctorInfo.Templet.RegLevel.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    //�Ƿ�ԤԼ
                    this.assign.Register.RegType = (FS.HISFC.Models.Base.EnumRegType)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]));
                    //�������
                    this.assign.Queue.Dept.ID = this.Reader[11].ToString();
                    this.assign.Queue.Dept.Name = this.Reader[12].ToString();

                    this.assign.Register.DoctorInfo.Templet.Dept = this.assign.Queue.Dept.Clone();
                    //��������
                    this.assign.Queue.SRoom.ID = this.Reader[14].ToString();
                    this.assign.Queue.SRoom.Name = this.Reader[16].ToString();
                    //�������
                    this.assign.Queue.ID = this.Reader[15].ToString();
                    this.assign.Queue.Name = this.Reader[13].ToString();

                    //����ҽ��
                    this.assign.Queue.Doctor.ID = this.Reader[17].ToString();

                    this.assign.Register.DoctorInfo.Templet.Doct = this.assign.Queue.Doctor.Clone();

                    //����ʱ��
                    this.assign.SeeTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
                    //����״̬
                    this.assign.TriageStatus = (FS.HISFC.Models.Nurse.EnuTriageStatus)
                                                    FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19].ToString());

                    //�������
                    this.assign.TriageDept = this.Reader[20].ToString();
                    //����ʱ��
                    this.assign.TirageTime = this.Reader.GetDateTime(21);
                    //����ʱ��
                    if (!this.Reader.IsDBNull(22))
                        this.assign.InTime = this.Reader.GetDateTime(22);
                    //����ʱ��
                    if (!this.Reader.IsDBNull(23))
                        this.assign.OutTime = this.Reader.GetDateTime(23);
                    //����Ա
                    this.assign.Oper.ID = this.Reader[24].ToString();
                    this.assign.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[25].ToString());
                    //��̨��Ϣ
                    this.assign.Queue.Console.ID = this.Reader[26].ToString();
                    this.assign.Queue.Console.Name = this.Reader[27].ToString();
                    this.assign.Register.DoctorInfo.Templet.RegLevel.ID = this.Reader[28].ToString();
                    this.assign.Register.DoctorInfo.Templet.RegLevel.Name = this.Reader[29].ToString();
                    this.assign.Register.DoctorInfo.Templet.Doct.Name = this.Reader[30].ToString();
                    #endregion

                    al.Add(this.assign);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "��ѯ������Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;
        }

        /// <summary>
        /// ���ݲ����Ų�ѯ�û��߽�������һ�η�����Ϣ��
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign Query(DateTime dt, string cardNo)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.8", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.8]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.8]";
                return null;
            }

            try
            {
                where = string.Format(where, cardNo, dt);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            ArrayList al = new ArrayList();
            al = this.Query(sql);
            if (al == null || al.Count <= 0)
            {
                return null;
            }
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = (FS.HISFC.Models.Nurse.Assign)al[0];
            return info;
        }

        /// <summary>
        /// ���ݿ��һ�ȡ��������վ����
        /// </summary>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public string QueryNurseByDept(string deptCode)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Dept.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Dept.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Dept.Query.1]";
                return "";
            }
            sql = string.Format(sql, deptCode);

            return this.Sql.ExecSqlReturnOne(sql);
        }

        /// <summary>
        /// ����������ˮ�ţ������־��ȡһ��Ψһ������Ϣ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryByClinicID(string clinicCode)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.10", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.7]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.7]";
                return null;
            }

            try
            {
                where = string.Format(where, clinicCode);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            ArrayList al = new ArrayList();
            al = this.Query(sql);
            if (al == null || al.Count <= 0)
            {
                return null;
            }
            return (FS.HISFC.Models.Nurse.Assign)al[0];
        }

        /// <summary>
        /// ����״̬��ѯ���ﻼ��
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="consoleID">��̨����</param>
        /// <param name="state">״̬ 1.���ﻼ��   2.���ﻼ��</param>
        /// <returns>ArrayList (����ʵ������)</returns>
        public ArrayList QueryAssignPatientByState(DateTime beginTime, DateTime endTime, string consoleID, String state, string doctID)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Nurse.Assign.Query.ByState", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.ByState]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.ByState]";
                return null;
            }

            try
            {
                //state = "(" + state + ")";
                sql = string.Format(sql, beginTime, endTime, consoleID, state);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.QueryAssReg(sql);
        }

        /// <summary>
        /// ��ѯ������Ϣ
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="consoleID">��̨����</param>
        /// <param name="state">״̬ 1.���ﻼ��   2.���ﻼ��</param>
        /// <returns>ArrayList (����ʵ������)</returns>
        public ArrayList QueryPatient(DateTime beginTime, DateTime endTime, string consoleID, String state, string doctID)
        {
            string sql = "";
            //���ﻼ��
            if (state == "1")
            {
                if (this.Sql.GetCommonSql("Nurse.Assign.Query.11", ref sql) == -1)
                {
                    this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.11]";
                    this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.11]";
                    return null;
                }

                try
                {
                    sql = string.Format(sql, beginTime, endTime, consoleID);
                }
                catch (Exception e)
                {
                    this.Err = "�ַ�ת������!" + e.Message;
                    this.ErrCode = e.Message;
                    return null;
                }
            }
            else if (state == "2")
            {
                if (this.Sql.GetCommonSql("Nurse.Assign.Query.13", ref sql) == -1)
                {
                    this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.13]";
                    this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.13]";
                    return null;
                }

                try
                {
                    sql = string.Format(sql, beginTime, endTime, doctID);
                }
                catch (Exception e)
                {
                    this.Err = "�ַ�ת������!" + e.Message;
                    this.ErrCode = e.Message;
                    return null;
                }
            }

            return this.QueryAssReg(sql);
        }

        /// <summary>
        /// ������ҽ���õ�ȥ�Һźͷ���ļ���
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList QueryAssReg(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "��ѯ������Ϣ����!" + sql;
                return null;
            }

            ArrayList al = new ArrayList();
            try
            {
                while (this.Reader.Read())
                {
                    #region ��ֵ
                    this.assign = new FS.HISFC.Models.Nurse.Assign();

                    //�����
                    this.assign.Register.ID = this.Reader[2].ToString();

                    //�������

                    this.assign.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                    this.assign.Register.DoctorInfo.SeeNO = this.assign.SeeNO;

                    //������
                    this.assign.Register.PID.CardNO = this.Reader[4].ToString();
                    this.assign.Register.Card.ID = this.assign.Register.PID.CardNO;
                    //�Һ�����
                    this.assign.Register.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                    //��������
                    this.assign.Register.Name = this.Reader[6].ToString();
                    //�Ա�
                    this.assign.Register.Sex.ID = this.Reader[7].ToString();
                    this.assign.Register.Sex.ID = this.assign.Register.Sex.ID;
                    //�������
                    this.assign.Register.Pact.PayKind.ID = this.Reader[8].ToString();
                    //�Ƿ���
                    //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
                    //this.assign.Register.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
                    this.assign.Register.DoctorInfo.Templet.RegLevel.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());

                    //�Ƿ�ԤԼ
                    //switch( FS.HISFC.Models.Base.EnumRegType
                    this.assign.Register.RegType = (FS.HISFC.Models.Base.EnumRegType)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]));
                    //�������
                    this.assign.Queue.Dept.ID = this.Reader[11].ToString();
                    this.assign.Queue.Dept.Name = this.Reader[12].ToString();

                    this.assign.Register.DoctorInfo.Templet.Dept = this.assign.Queue.Dept.Clone();
                    //��������
                    this.assign.Queue.SRoom.ID = this.Reader[14].ToString();
                    this.assign.Queue.SRoom.Name = this.Reader[16].ToString();
                    //�������
                    this.assign.Queue.ID = this.Reader[15].ToString();
                    this.assign.Queue.Name = this.Reader[13].ToString();
                    //����ҽ��
                    this.assign.Queue.Doctor.ID = this.Reader[17].ToString();

                    //					this.assign.Register.RegDoct = this.assign.Queue.Doctor.Clone() ;

                    //����ʱ��
                    this.assign.SeeTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
                    //����״̬
                    this.assign.TriageStatus = (FS.HISFC.Models.Nurse.EnuTriageStatus)
                        FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19].ToString());

                    //�������
                    this.assign.TriageDept = this.Reader[20].ToString();
                    //����ʱ��
                    this.assign.TirageTime = this.Reader.GetDateTime(21);
                    //����ʱ��
                    if (!this.Reader.IsDBNull(22))
                        this.assign.InTime = this.Reader.GetDateTime(22);
                    //����ʱ��
                    if (!this.Reader.IsDBNull(23))
                        this.assign.OutTime = this.Reader.GetDateTime(23);
                    //����Ա
                    this.assign.Oper.ID = this.Reader[24].ToString();
                    this.assign.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[25].ToString());
                    //��̨��Ϣ
                    this.assign.Queue.Console.ID = this.Reader[26].ToString();
                    this.assign.Queue.Console.Name = this.Reader[27].ToString();
                    //�Һ���Ϣ
                    this.assign.Register.Birthday = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[28]);
                    this.assign.Register.Pact.PayKind.ID = this.Reader[29].ToString();
                    this.assign.Register.Pact.PayKind.Name = this.Reader[30].ToString();
                    this.assign.Register.Pact.ID = this.Reader[31].ToString();
                    this.assign.Register.Pact.Name = this.Reader[32].ToString();
                    this.assign.Register.AddressHome = this.Reader[33].ToString();
                    this.assign.Register.PhoneHome = this.Reader[34].ToString();
                    this.assign.Register.DoctorInfo.Templet.RegLevel.ID = this.Reader[35].ToString();
                    this.assign.Register.DoctorInfo.Templet.RegLevel.Name = this.Reader[36].ToString();
                    this.assign.Register.DoctorInfo.Templet.Doct.ID = this.Reader[37].ToString();
                    this.assign.Register.DoctorInfo.Templet.Doct.Name = this.Reader[38].ToString();
                    //					this.assign.Register.BeginTime = FS.neuFC.Function.NConvert.ToDateTime(this.Reader[39]);
                    //					this.assign.Register.EndTime = FS.neuFC.Function.NConvert.ToDateTime(this.Reader[40]);
                    #endregion
                    this.assign.Register.IsAccount = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[39].ToString());
                    assign.Register.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[40]);
                    assign.Register.IsFee = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[41]);
                    assign.Register.PVisit.InState.ID = this.Reader[42].ToString();//{BF3E846D-5961-4a8e-A188-C77739C7438A}���뻼��״̬������ҽ��վ���۵��ж�
                    //{2296D82A-7E97-4d94-90B7-052403F52DD4}�����Ƿ�������ҽ��վ���»��߷���״̬�ж�
                    assign.Register.IsTriage = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[43].ToString());
                    if (this.Reader.FieldCount >= 45)
                        assign.Register.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[44].ToString());
                    if (this.Reader.FieldCount >= 46)
                        assign.Register.DoctorInfo.Templet.RegLevel.ID = Reader[45].ToString();
                    if (this.Reader.FieldCount >= 47)
                        assign.Register.DoctorInfo.Templet.RegLevel.Name = Reader[46].ToString();
                    if (this.Reader.FieldCount >= 48)
                        assign.Register.InputOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[47]);

                    al.Add(this.assign);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "��ѯ������Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return al;
        }

        /// <summary>
        /// ����������жϻ����Ƿ��Ѿ����
        /// </summary>
        /// <param name="clinicCode">�����</param>
        /// <returns>0 ��� 1 δ��� -1 ��ѯ����</returns>
        public int JudgeOut(string clinicCode)
        {
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = this.QueryByClinicID(clinicCode);
            if (info == null || info.Register.ID == null || info.Register.ID == "")
            {
                return -1;//��ѯ����
            }
            if (info.TriageStatus == FS.HISFC.Models.Nurse.EnuTriageStatus.Out)
            {
                return 0;//�Ѿ����
            }
            else
            {
                return 1;//δ���
            }
        }

        /// <summary>
        /// ����������жϻ����Ƿ��Ѿ����
        /// </summary>
        /// <param name="clinicCode">�����</param>
        /// <returns>���ڵ���1������������л���  0�� û��  -1:��ѯ����</returns>
        public int JudgeInQueue(string clinicCode)
        {
            string strSql = string.Empty;
            int returnValue = this.Sql.GetCommonSql("Nurse.Assign.QueryByCinic.1", ref strSql);
            if (returnValue == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.QueryByCinic.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.QueryByCinic.1]";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, clinicCode);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�����ɴ���" + e.Message;
                return -1;
            }
            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strSql));

        }

        /// <summary>
        /// ��ѯ�ѿ��ﻼ��
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public ArrayList QueryCard(DateTime begin, DateTime end, string cardNo)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.12", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.12]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.12]";
                return null;
            }

            try
            {
                where = string.Format(where, begin.ToString(), end.ToString(), cardNo);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            return this.Query(sql);
        }

        #endregion

        #region ҵ��

        /// <summary>
        /// ���»��ߵķ���ҽ������̨����Ϣ
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        public int Update(FS.HISFC.Models.Nurse.Assign assign)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.8", ref sql) == -1)
            {
                sql = @"update met_nuo_assignrecord tt
                        set tt.queue_code = '{1}',
                            tt.queue_name = '{2}',
                            tt.doct_code = '{3}',
		                    tt.room_id = '{4}',
		                    tt.room_name = '{5}',
		                    tt.console_code = '{6}',
		                    tt.console_name = '{7}',
		                    tt.reglvl_code = '',  --����/ȡ������ ��չҺż���
		                    tt.reglvl_name = ''   --����/ȡ������ ��չҺż���
                        where tt.clinic_code = '{0}'";
            }
            sql = string.Format(sql,
                assign.Register.ID,  //�����
                assign.Queue.ID, //���д���
                assign.Queue.Name, //������
                assign.Queue.Doctor.ID, //����ҽ��code
                assign.Queue.SRoom.ID, //���Һ�
                assign.Queue.SRoom.Name, //��������
                assign.Queue.Console.ID, //��̨��
                assign.Queue.Console.Name //��̨����
                );

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// �����µķ����¼
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Nurse.Assign assign)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Insert.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql,
                        assign.Register.ID,
                        assign.SeeNO,
                        assign.Register.PID.CardNO,
                        assign.Register.DoctorInfo.SeeDate.ToString(),
                        assign.Register.Name,
                        assign.Register.Sex.ID,
                        assign.Register.Pact.PayKind.ID,
                        FS.FrameWork.Function.NConvert.ToInt32(assign.Register.DoctorInfo.Templet.RegLevel.IsEmergency),
                        FS.FrameWork.Function.NConvert.ToInt32(assign.Register.RegType),
                        assign.Register.DoctorInfo.Templet.Dept.ID,
                        assign.Register.DoctorInfo.Templet.Dept.Name,
                        assign.Queue.Name,
                        assign.Queue.SRoom.ID,
                        assign.Queue.ID,
                        assign.Queue.SRoom.Name,
                        assign.Queue.Doctor.ID,
                        assign.SeeTime.ToString(),
                        (int)assign.TriageStatus,
                        assign.TriageDept,
                        assign.TirageTime.ToString(),
                        assign.InTime.ToString(),
                        assign.OutTime.ToString(),
                        assign.Oper.ID,
                        assign.Oper.OperTime.ToString(),
                        assign.Queue.Console.ID,
                        assign.Queue.Console.Name,
                        assign.Register.DoctorInfo.Templet.RegLevel.ID,
                        assign.Register.DoctorInfo.Templet.RegLevel.Name,
                        assign.Register.OrderNO.ToString()
                        );
            }
            catch (Exception e)
            {
                this.Err = "���������Ϣ�����![Nurse.Assign.Insert.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);

        }

        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="assign"></param>
        /// <returns></returns>
        public int Delete(FS.HISFC.Models.Nurse.Assign assign)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Delete.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, assign.Register.ID);
            }
            catch (Exception e)
            {
                this.Err = "ɾ��������Ϣ�����![Nurse.Assgin.Delete.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ����CLINIC_CODEɾ�������¼
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public int Delete(string clinicCode)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Delete.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicCode);
            }
            catch (Exception e)
            {
                this.Err = "ɾ��������Ϣ�����![Nurse.Assgin.Delete.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ������(���±�־������ʱ��)
        /// </summary>
        /// <param name="room"></param>
        /// <param name="inDate"></param>
        /// <returns></returns>
        public int Update(string clinicID, FS.FrameWork.Models.NeuObject room, FS.FrameWork.Models.NeuObject console, DateTime inDate)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, room.ID, room.Name, console.ID, console.Name, inDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "���·�����Ϣ�����![Nurse.Assgin.Update.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
            //������̨��count
            if (this.UpdateConsole(console.ID, "1") == -1)
            {
                this.ErrCode = "������̨���ﻼ����������";
                return -1;
            }
            //����clinicID��ȡ������Ϣ
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = this.QueryByClinicID(clinicID);
            //���ٶ�����count
            if (this.UpdateQueue(info.Queue.ID, "-1") == -1)
            {
                this.ErrCode = "������̨���ﻼ����������";
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���(����ҽ��ֱ�ӵ�����ר��)
        /// </summary>
        /// <param name="consoleCode">��̨����</param>
        /// <param name="clinicID">�����</param>
        /// <param name="outDate">�������</param>
        /// <returns></returns>
        public int Update(string consoleCode, string clinicID, DateTime outDate, string doctID)
        {
            //����clinicID��ȡ������Ϣ
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = this.QueryByClinicID(clinicID);
            if (info == null)
            {
                this.ErrCode = "������̨���ﻼ����������";
                return -1;
            }
            //���ٶ�����count
            if (this.UpdateQueue(info.Queue.ID, "-1") == -1)
            {
                this.ErrCode = "������̨���ﻼ����������";
                return -1;
            }

            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.2", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, outDate.ToString(), doctID);
            }
            catch (Exception e)
            {
                this.Err = "���·�����Ϣ�����![Nurse.Assgin.Update.2]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            int ret = this.ExecNoQuery(sql);

            //if (ret > 0)
            //{
            //    if (consoleCode == "")
            //    {
            //        consoleCode = info.Queue.Console.ID;
            //    }
            //    //������̨��count
            //    return this.UpdateConsole(consoleCode, "-1");
            //}
            //else
            //{
            return ret;//����,����û�и��µ�
            //}
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="arriveDate"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public int UpdateArrive(string clinicCode, DateTime arriveDate, string doctID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.7", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicCode, arriveDate.ToString(), doctID);
            }
            catch (Exception e)
            {
                this.Err = "���·�����Ϣ�����![Nurse.Assgin.Update.7]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���(����ҽ������ҽ����ר��)
        /// </summary>
        /// <param name="consoleCode">��̨����</param>
        /// <param name="clinicID">�����</param>
        /// <param name="outDate">�������</param>
        /// <returns></returns>
        public int UpdateSaved(string consoleCode, string clinicID, DateTime outDate, string doctID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.2", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID, outDate.ToString(), doctID);
            }
            catch (Exception e)
            {
                this.Err = "���·�����Ϣ�����![Nurse.Assgin.Update.2]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            int ret = this.ExecNoQuery(sql);
            //if (ret > 0)
            //{
            //    //������̨��count
            //    return this.UpdateConsole(consoleCode, "-1");
            //}
            //else
            //{
            return ret;//����,����û�и��µ�
            //}
        }

        /// <summary>
        /// ȡ������ûر�־1��
        /// </summary>
        /// <param name="clinicID"></param>
        /// <returns></returns>
        public int CancelIn(string clinicID, string ConsoleCode)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.3", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicID);
            }
            catch (Exception e)
            {
                this.Err = "���·�����Ϣ�����![Nurse.Assgin.Update.3]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            //������̨��count
            if (this.UpdateConsole(ConsoleCode, "-1") == -1)
            {
                this.ErrCode = "������̨���ﻼ����������";
                return -1;
            }

            //����clinicID��ȡ������Ϣ
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = this.QueryByClinicID(clinicID);
            //���Ӷ�����count
            if (this.UpdateQueue(info.Queue.ID, "1") == -1)
            {
                this.ErrCode = "������̨���ﻼ����������";
                return -1;
            }
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���Ҫͣ����̨���ж���̨���Ƿ��л���.
        /// ֻ�ж�Ҫͣ�õ���̨
        /// </summary>
        /// <param name="seatID">��̨����</param>
        /// <param name="dateTime">����ʱ��</param>
        /// <returns>true:���ˣ�false:û��</returns>
        public bool ExistPatient(string seatID, string inTime)
        {
            string strsql = "";
            if (this.Sql.GetCommonSql("Nurse.Assign.ConsoleExistPatient", ref strsql) == -1)
            {
                return true;
            }
            try
            {
                strsql = string.Format(strsql, seatID, inTime);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return true;
            }

            string retv = this.ExecSqlReturnOne(strsql);
            if (FS.FrameWork.Function.NConvert.ToInt32(retv.Trim()) > 0 /* || retv == null */ )
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// ���·���״̬
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="doctID"></param>
        /// <param name="assign"></param>
        /// <returns></returns>
        public int UpdateAssignAfterCall(params object[] objs)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.AfterCall", ref sql) == -1)
            {
                this.Err = Sql.Err;
                this.ErrCode = Sql.ErrCode;
                return -1;
            }

            try
            {
                sql = string.Format(sql, objs);
            }
            catch (Exception e)
            {
                this.Err = "���·�����Ϣ�����![Nurse.Assign.Update.AssignFlag]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���·���״̬
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="doctID"></param>
        /// <param name="assign"></param>
        /// <returns></returns>
        public int UpdateAssignFlag(string clinicCode, string doctID, FS.HISFC.Models.Nurse.EnuTriageStatus assign)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.AssignFlag", ref sql) == -1)
            {
                this.Err = Sql.Err;
                this.ErrCode = Sql.ErrCode;
                return -1;
            }

            try
            {
                sql = string.Format(sql, clinicCode, doctID, ((Int32)assign).ToString());
            }
            catch (Exception e)
            {
                this.Err = "���·�����Ϣ�����![Nurse.Assign.Update.AssignFlag]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ͳһ���»��߱��
        /// </summary>
        /// <param name="dept"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public int UpdateAssignFlag(int assignflag, FS.FrameWork.Models.NeuObject dept, string doctID, string seeDate, int nowAssignflag)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.UpdateAssignFlag", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, assignflag, seeDate.ToString(), dept.ID, doctID, nowAssignflag);
            }
            catch (Exception e)
            {
                this.Err = "���·�����Ϣ�����![Nurse.Assgin.Update.4]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        #region ����ʵ�ʿ���˳��

        /// <summary>
        ///  ����ʵ�ʿ���˳��
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="seq"></param>
        /// <param name="strnum">�ı������ ��-1���ʾ����һ��</param>
        /// <returns></returns>
        public int Update(string clinicCode, string seq, int num)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.5", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, clinicCode, seq, num);
            }
            catch (Exception e)
            {
                this.Err = "���³���![Nurse.Assign.Update.5]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        #endregion

        #region ��֪����ʲô��

        /// <summary>
        /// ���¿����־
        /// </summary>
        /// <param name="clinicID"></param>
        /// <param name="seeDate"></param>
        /// <param name="dept"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public int Update(string clinicID, DateTime seeDate, FS.FrameWork.Models.NeuObject dept, string doctID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Update.4", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, seeDate.ToString(), dept.ID, dept.Name, doctID, clinicID);
            }
            catch (Exception e)
            {
                this.Err = "���·�����Ϣ�����![Nurse.Assgin.Update.4]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }


        #endregion
        #endregion

        #region ����

        #region �����л�������

        /// <summary>
        /// ���¶����к�������
        /// </summary>
        /// <param name="queueCode"></param>
        /// <param name="num">1 ����һ��  ��1 ����һ��</param>
        /// <returns></returns>
        public int UpdateQueue(string queueCode, string num)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Queue.Update.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, queueCode, num);
            }
            catch (Exception e)
            {
                this.Err = "���³���![Nurse.Queue.Update.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        #endregion

        #region ��̨�л�������

        /// <summary>
        /// ������̨�е����ڿ��������
        /// </summary>
        /// <param name="consoleCode"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public int UpdateConsole(string consoleCode, string num)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Seat.Update.1", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, consoleCode, num);
            }
            catch (Exception e)
            {
                this.Err = "���³���![Nurse.Seat.Update.1]" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        #endregion
        #endregion

        #region ����ҽ�����ѯ���ﻼ�ߵ���Ϣ

        /// <summary>
        /// �����������ѯ���ﻼ�ߵ���Ϣ
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="DoctID"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(DateTime fromtime, DateTime totime, string DoctID)
        {
            FS.HISFC.Models.Registration.Register reg = new FS.HISFC.Models.Registration.Register();
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Order.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Order.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Order.Query.1]";
                return null;
            }

            try
            {
                sql = string.Format(sql, fromtime.ToString(), totime.ToString(), DoctID);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.QueryOrder(sql);
        }

        /// <summary>
        /// ���ݿ��Ų�ѯ���������ﻼ�ߵ���Ϣ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="fromtime"></param>
        /// <param name="totime"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(string cardNo, DateTime fromtime, DateTime totime)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Order.Query.2", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Order.Query.2]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Order.Query.2]";
                return null;
            }

            try
            {
                sql = string.Format(sql, cardNo, fromtime.ToString(), totime.ToString());
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.QueryOrder(sql);
        }

        /// <summary>
        /// ʵ��
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList QueryOrder(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "��ѯ������Ϣ����!" + sql;
                return null;
            }
            ArrayList al = new ArrayList();
            FS.HISFC.Models.Registration.Register reg = new FS.HISFC.Models.Registration.Register();
            try
            {
                while (this.Reader.Read())
                {
                    reg = new FS.HISFC.Models.Registration.Register();
                    reg.DoctorInfo.Templet.Doct.ID = this.Reader[0].ToString();
                    reg.DoctorInfo.Templet.Doct.Name = this.Reader[1].ToString();
                    reg.PID.CardNO = this.Reader[2].ToString();
                    reg.Name = this.Reader[3].ToString();
                    reg.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4]);
                    reg.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5]);
                    reg.DoctorInfo.Templet.Dept.ID = this.Reader[6].ToString();
                    reg.DoctorInfo.Templet.Dept.Name = this.Reader[7].ToString();
                    reg.Sex.ID = this.Reader[8].ToString();
                    //reg.User01 = this.Reader[9].ToString();
                    reg.SeeDoct.Dept.ID = this.Reader[9].ToString();
                    reg.SeeDoct.ID = this.Reader[10].ToString();

                    al.Add(reg);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "��ѯ������Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return al;
        }

        #endregion

        #region �Զ�����Ĳ�ѯ

        /// <summary>
        /// ��ѯ��ǰʱ��,��ǰ�����е����Ƚ���ĺ�����Ϣ
        /// </summary>
        /// <param name="queueCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryWait(string queueCode, DateTime begin, DateTime end)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Auto.Query.1", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Auto.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Auto.Query.1]";
                return null;
            }

            try
            {
                where = string.Format(where, queueCode, begin, end);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            ArrayList al = new ArrayList();
            al = this.Query(sql);
            if (al == null || al.Count <= 0)
            {
                return null;
            }
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = (FS.HISFC.Models.Nurse.Assign)al[0];
            return info;
        }

        /// <summary>
        /// ��ѯ���������ڿ������Ϣ
        /// </summary>
        /// <param name="queueCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Assign QueryIn(string queueCode, DateTime begin, DateTime end)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Auto.Query.2", ref where) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Auto.Query.2]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Auto.Query.2]";
                return null;
            }

            try
            {
                where = string.Format(where, queueCode, begin, end);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            sql = sql + " " + where;

            ArrayList al = new ArrayList();
            al = this.Query(sql);
            if (al == null || al.Count <= 0)
            {
                return null;
            }
            FS.HISFC.Models.Nurse.Assign info = new FS.HISFC.Models.Nurse.Assign();
            info = (FS.HISFC.Models.Nurse.Assign)al[0];
            return info;
        }

        /// <summary>
        /// ��ѯĳ��̨��������
        /// </summary>
        /// <param name="consoleCode"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public int QueryConsoleNum(string consoleCode, DateTime begin, DateTime end, FS.HISFC.Models.Nurse.EnuTriageStatus status)
        {
            int i = -1;
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Auto.ConsoleNum", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Auto.ConsoleNum]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Auto.ConsoleNum]";
                return -1;
            }
            try
            {
                sql = string.Format(sql, begin, end, consoleCode, (int)status);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "��ѯ������Ϣ����!" + sql;
                return -1;
            }
            try
            {
                while (this.Reader.Read())
                {
                    i = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                }
                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "��ѯ������Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return i;

        }

        #endregion

        #region �Ż���ѯ

        /// <summary>
        /// ��ѯ�Һ���Ϣ �����ѯ
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QuerySimpleRegInfo(string whereIndex, params string[] args)
        {
            //��ѯ��SQL
            string sql = @"SELECT t.clinic_code,   --�����
                                   t.name,   --��������
                                   t.sex_code,   --�Ա�
                                   t.paykind_code,   --�������
                                   r.seeno,   --������� 
                                   t.queue_code,   --���д���assign.Queue.ID
                                   t.doct_code,   --����ҽ��assign.Queue.Doctor.ID
                                   t.room_id,   --��������assign.Queue.SRoom.ID
                                   t.console_code,--��̨��Ϣassign.Queue.Console.ID
                                   t.nurse_cell_code,   --�������assign.TriageDept
                                   t.assign_flag,   --1����/2����/3���
                                   r.card_no, --���ﲡ����
                                   r.order_no, --ÿ�����
                                   r.reglevl_code,
                                   r.reglevl_name,
                                   r.oper_date,
                                    r.reg_date,
                                    r.ynbook --ԤԼ
                              FROM met_nuo_assignrecord  t ,fin_opr_register r
                                where t.clinic_code = r.clinic_code
                             ";
            if (this.Sql.GetCommonSql(whereIndex, ref whereIndex) == -1)
            {
                this.Err = Sql.Err;
                this.ErrCode = Sql.ErrCode;
                return null;
            }

            try
            {
                sql = sql + "\r\n" + whereIndex;

                sql = string.Format(sql, args);

                if (this.ExecQuery(sql) == -1)
                {
                    return null;
                }

                ArrayList al = new ArrayList();

                FS.HISFC.Models.Nurse.Assign assignObj = null;
                while (this.Reader.Read())
                {
                    assignObj = new FS.HISFC.Models.Nurse.Assign();
                    assignObj.Register.ID = this.Reader[0].ToString();//������ˮ��
                    assignObj.Register.Name = this.Reader[1].ToString();//����
                    assignObj.Register.Sex.ID = this.Reader[2].ToString();//�Ա�
                    assignObj.Register.Pact.PayKind.ID = Reader[3].ToString();//�������
                    assignObj.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[4]);//�������
                    assignObj.Register.DoctorInfo.SeeNO = assignObj.SeeNO;

                    assignObj.Queue.ID = Reader[5].ToString();//���д���
                    assignObj.Queue.Doctor.ID = this.Reader[6].ToString(); //����ҽ��

                    assignObj.Queue.SRoom.ID = this.Reader[7].ToString();//����
                    assignObj.Queue.Console.ID = Reader[8].ToString();//��̨
                    assignObj.TriageDept = Reader[9].ToString();//�������
                    assignObj.TriageStatus = (FS.HISFC.Models.Nurse.EnuTriageStatus)
                                                    FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10].ToString());
                    assignObj.Register.PID.CardNO = Reader[11].ToString();//������
                    assignObj.Register.OrderNO = FS.FrameWork.Function.NConvert.ToInt32(Reader[12].ToString());//������
                    assignObj.Register.DoctorInfo.Templet.RegLevel.ID = Reader[13].ToString();
                    assignObj.Register.DoctorInfo.Templet.RegLevel.Name = Reader[14].ToString();

                    assignObj.Register.InputOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[15]);
                    assignObj.Register.DoctorInfo.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(Reader[16]);
                    assignObj.Register.RegType = (FS.HISFC.Models.Base.EnumRegType)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[17].ToString());
                    al.Add(assignObj);
                }

                return al;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;

                return null;
            }
            finally
            {
                if (this.Reader != null && !Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// �����������ҡ�����״̬��ѯ���ﻼ��
        /// </summary>
        /// <param name="deptID"></param>
        /// <param name="roomID"></param>
        /// <param name="assignFlag"></param>
        /// <returns></returns>
        public ArrayList QuerySimpleAssignByAssignFlag(string deptID, string roomID, string assignFlag)
        {
            return this.QueryAssignBase("Nurse.Assign.Query.SimpleAssignByAssignFlag", deptID, roomID, assignFlag);
        }

        /// <summary>
        /// ����״̬��ѯ���ﻼ��
        /// </summary>
        /// <param name="beginTime">��ʼʱ��</param>
        /// <param name="endTime">����ʱ��</param>
        /// <param name="consoleID">��̨����</param>
        /// <param name="state">״̬ 1.���ﻼ��   2.���ﻼ��</param>
        /// <returns>ArrayList (����ʵ������)</returns>
        public ArrayList QuerySimpleAssignPatientByState(DateTime beginTime, DateTime endTime, string consoleID, String state, string doctID, string dept_code)
        {
            return this.QuerySimpleRegInfo("Nurse.Assign.QuerySimple.ByState", beginTime.ToString(), endTime.ToString(), consoleID, state, doctID, dept_code);
        }

        /// <summary>
        /// ��ҽ����ѯ
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="state"></param>
        /// <param name="doctID"></param>
        ///  <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QuerySimpleAssignPatientByDoctID(DateTime beginTime, DateTime endTime, String state, string doctID, string deptID)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Nurse.Assign.Query.QuerySimpleAssignPatientByDoctID.New", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.QuerySimpleAssignPatientByDoctID.New]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.QuerySimpleAssignPatientByDoctID.New]";
                return null;
            }

            try
            {
                sql = string.Format(sql, beginTime, endTime, doctID, state);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.QueryAssReg(sql);
        }

        /// <summary>
        /// �����Ҳ�ѯ
        /// </summary>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <param name="state"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public ArrayList QuerySimpleAssignPatientByDeptID(DateTime beginTime, DateTime endTime, String state, string deptID)
        {

            string sql = "";
            if (this.Sql.GetCommonSql("Nurse.Assign.Query.QuerySimpleAssignPatientByDeptID.New", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.QuerySimpleAssignPatientByDeptID.New]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.QuerySimpleAssignPatientByDeptID.New]";
                return null;
            }

            try
            {
                sql = string.Format(sql, beginTime, endTime, deptID, state);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }


            return this.QueryAssReg(sql);
        }
        #endregion

        /// <summary>
        /// ��ѯ�����Ѿ��к�δ���ﻼ��
        /// </summary>
        /// <param name="doctCode"></param>
        /// <param name="clinicCode">��ǰ�кŵĻ��ߣ��������߿����ظ��к�</param>
        /// <returns></returns>
        public ArrayList QueryCalledList(string doctCode, string clinicCode)
        {
            string sql = "";
            string whereSQL = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.1", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Query.1]";
                return null;
            }

            if (this.Sql.GetCommonSql("Nurse.Assign.Query.QueryCalledList", ref whereSQL) == -1)
            {
                whereSQL = @" where oper_code = '{0}'
                               and trunc(oper_date) = trunc(sysdate)
                               and assign_flag in ('2', '4')
                               and clinic_code!='{1}'";
            }

            try
            {
                sql = sql + "\r\n" + whereSQL;
                sql = string.Format(sql, doctCode, clinicCode);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.Query(sql);
        }
    }
}
