using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Nurse
{
	/// <summary>
	/// ������Ϣ������
	/// </summary>
	public class Queue : FS.FrameWork.Management.Database
    {
        public Queue()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region ��������
        protected ArrayList al = null;
        protected FS.HISFC.Models.Nurse.Queue queue = null;
        #endregion

        /// <summary>
        /// ��ò�������б�
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string[] myGetParmInsertQueue(FS.HISFC.Models.Nurse.Queue obj)
        {
            string[] strParm ={	
								    obj.Dept.ID,//����1
									obj.Name,//��������2
									obj.Noon.ID,//���3
									obj.User01,//�������4
									obj.Order.ToString(),//˳��5
									obj.IsValid?"1":"0",//�Ƿ���Ч6
									obj.Memo,//��ע7
									obj.Oper.ID,//����Ա8
									obj.QueueDate.ToString(),//����ʱ��9
									obj.Doctor.ID,//ҽ������10
									obj.ID,//���к�11
									obj.SRoom.ID,//���Ҵ���12
									obj.SRoom.Name,//��������13
									obj.Console.ID,//��̨����14
									obj.Console.Name,//��̨����15
									obj.ExpertFlag,//ר��16
								    obj.AssignDept.ID,
									obj.AssignDept.Name
							 };

            return strParm;

        }
        /// <summary>
        /// ����޸Ķ��в����б�
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        protected string[] myGetParmUpdateQueue(FS.HISFC.Models.Nurse.Queue obj)
        {
            string[] strParm ={	
								obj.ID,//���к�
								obj.Dept.ID,//���Ҵ���
								obj.Name,//��������
								obj.Noon.ID,//���
								obj.User01,//�������
								obj.Order.ToString(),//˳��
								obj.IsValid?"1":"0",//�Ƿ���Ч
								obj.Memo,//��ע
								obj.Oper.ID,//����Ա
								obj.QueueDate.ToString(),//����ʱ��
								obj.Doctor.ID,//ҽ������
								obj.SRoom.ID,//���Ҵ���
								obj.SRoom.Name,//��������
								obj.Console.ID,//��̨
								obj.Console.Name,//��̨����
								 obj.ExpertFlag,//ר��16
								 obj.AssignDept.ID,
								 obj.AssignDept.Name
							 };
            return strParm;
        }

        /// <summary>
        /// ��ô�����
        /// </summary>
        /// <returns></returns>
        public string GetQueueNo()
        {
            return this.GetSequence("Nurse.GetRecipeNo.Select");
        }
        /// <summary>
        /// �������
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertQueue(FS.HISFC.Models.Nurse.Queue obj)
        {
            string strSQL = "";
            //ȡ���������SQL���
            string[] strParam;
            if (this.Sql.GetCommonSql("Nurse.Queue.InsertQueue", ref strSQL) == -1)
            {
                this.Err = "û���ҵ��ֶ�!";
                return -1;
            }
            try
            {
                if (obj.ID == null) return -1;
                obj.ID = "T" + this.GetQueueNo();
                strParam = this.myGetParmInsertQueue(obj);

            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL, strParam);


        }

        /// <summary>
        /// ����кŶ���
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="dept"></param>
        /// <param name="room"></param>
        /// <returns></returns>
        public int InsertCallQueue(FS.HISFC.Models.Registration.Register reg,FS.FrameWork.Models.NeuObject dept,
            FS.FrameWork.Models.NeuObject room)
        {
            string strSQL = "";
            //ȡ���������SQL���
       
            if (this.Sql.GetCommonSql("Nurse.Queue.InsertCallQueue", ref strSQL) == -1)
            {
                this.Err = "û���ҵ��ֶ�!";
                return -1;
            }
            try
            {
                strSQL = String.Format(strSQL,reg.ID,
                reg.Name,
                dept.ID,
                dept.Name,
                room.ID,
                room.Name,
                room.ID,
                room.Name.Substring(0, room.Name.IndexOf("-")),
                this.Operator.ID,"");

            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʱ����:" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);


        }

        /// <summary>
        /// ����޸Ķ��в����б�
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateQueue(FS.HISFC.Models.Nurse.Queue obj)
        {
            string strSql = "";
            string[] strParam;
            if (this.Sql.GetCommonSql("Nurse.Queue.UpdateQueue", ref strSql) == -1) return -1;
            try
            {
                //��ȡ�����б�
                strParam = this.myGetParmUpdateQueue(obj);
                strSql = string.Format(strSql, strParam);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �����Ű���Ϣ���»�ʿվ������Ч��
        /// </summary>
        /// <param name="validFlag"></param>
        /// <param name="seeDate"></param>
        /// <param name="noonCode"></param>
        /// <param name="deptCode"></param>
        /// <param name="doctCode"></param>
        /// <returns></returns>
        public int UpdateQueueValid(string validFlag, string seeDate, string noonCode, string deptCode, string doctCode)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Schema.UpdateQueueValid", ref sql) == -1)
            {
                sql = @"UPDATE met_nuo_queue   --ҽʦ�����
                        SET valid_flag='{0}'   --1����/0ͣ��
                        where Trunc(queue_date) = to_date('{1}','yyyy:mm:dd') 
                        and noon_code='{2}'
                        and dept_code='{3}'
                        and doct_code='{4}'";
            }
            try
            {
                sql = string.Format(sql, validFlag, seeDate, noonCode, deptCode, doctCode);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Update]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
            return this.ExecNoQuery(sql);
        }


        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="queueNo"></param>
        /// <returns></returns>
        public int DelQueue(string queueNo)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Nurse.DelQueue.1", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, queueNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ɾ���кŶ���
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public int DelCallQueue(string clinicCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Nurse.DelCallQueue.1", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, clinicCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ʿվ/��������/����ѯ���������Ϣ
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <param name="noonID"></param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, DateTime queueDate, string noonID)
        {
            return this.QueryBase("Nurse.Queue.Query.2", nurseID, queueDate.ToString(), noonID);
        }

        /// <summary>
        /// ����where������ѯ������Ϣ
        /// </summary>
        /// <param name="whereSQL"></param>
        /// <returns></returns>
        public ArrayList QueryBase(string whereSQL)
        {
            string sql_Select = "";

            if (this.Sql.GetCommonSql("Nurse.Queue.Query.1", ref sql_Select) == -1)
            {
                this.Err = "��ѯ���������Ϣ����![Nurse.Queue.Query.1]";
                return null;
            }

            sql_Select = sql_Select + "\r\n" + whereSQL;

            return this.Query(sql_Select);
        }

        /// <summary>
        /// ����SQLID��ѯ������Ϣ
        /// </summary>
        /// <param name="whereIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QueryBase(string whereIndex, params string[] args)
        {
            string whereSQL = "";

            if (this.Sql.GetCommonSql(whereIndex, ref whereSQL) == -1)
            {
                this.Err = "��ѯ���������Ϣ����![" + whereIndex + "]";
                return null;
            }

            try
            {
                whereSQL = string.Format(whereSQL, args);
            }
            catch (Exception e)
            {
                this.Err = "��ѯ���������Ϣ����![" + whereIndex + "]\r\n" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.QueryBase(whereSQL);
        }

        /// <summary>
        /// ����ʿվ/��������/���/���� ��ѯ���������Ϣ
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <param name="noonID"></param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, DateTime queueDate, string noonID, string deptCode)
        {
            return this.QueryBase("Nurse.Queue.Query.5", nurseID, queueDate.ToString(), noonID, deptCode);
        }
        /// <summary>
        /// �������к�ȥ������Ϣ
        /// </summary>
        /// <param name="queueID">���к�</param> 
        /// <returns></returns>
        public ArrayList QueryByQueueID(string queueID)
        {
            return this.QueryBase("Nurse.Queue.Query.7", queueID);
        }

        /// <summary>
        /// ��ѯmet_nuo_assignrecord���Ƿ��з��������������Ƿ�����
        /// </summary>
        /// <param name="roomID">���Ҵ���</param>
        /// <param name="currentDateStr"></param>
        /// <returns></returns>
        public int QueryQueueByQueueID(string roomID, string currentDateStr)
        {
            string strsql = string.Empty;
            if (this.Sql.GetCommonSql("Nurse.Room.GetQueueByQueueID", ref strsql) == -1)
            {
                this.Err = "�õ�Nurse.Room.GetQueueByQueueIDʧ��";
                return -1;
            }

            try
            {
                strsql = string.Format(strsql, roomID, currentDateStr);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strsql));
        }
        /// <summary>
        /// ��ѯ���������Ϣ queueDate��ʽΪ 2013-01-01
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <returns></returns>
        public ArrayList Query(string nurseID, string queueDate)
        {
            string strBegin = queueDate + " " + "00:00:00", strEnd = queueDate + " " + "23:59:59";

            return this.QueryBase("Nurse.Queue.Query.3", nurseID, strBegin, strEnd);
        }

        /// <summary>
        /// ����sql��ѯ������Ϣ
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList Query(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "����sql����!" + sql;
                this.ErrCode = "����sql����!" + sql;
                return null;
            }

            this.al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    this.queue = new FS.HISFC.Models.Nurse.Queue();

                    //������ʿվ
                    this.queue.Dept.ID = this.Reader[0].ToString();
                    //���д���
                    this.queue.ID = this.Reader[1].ToString();
                    //��������
                    this.queue.Name = this.Reader[2].ToString();
                    //������
                    this.queue.Noon.ID = this.Reader[3].ToString();

                    //��������[2007/03/27]
                    this.queue.User01 = this.Reader[4].ToString();

                    //��ʾ˳��
                    if (!this.Reader.IsDBNull(5))
                        this.queue.Order = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());
                    //�Ƿ���Ч
                    this.queue.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[6].ToString());
                    //��ע
                    this.queue.Memo = this.Reader[7].ToString();
                    //����Ա
                    this.queue.Oper.ID = this.Reader[8].ToString();
                    //����ʱ��
                    this.queue.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[9].ToString());
                    //��������
                    this.queue.QueueDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());
                    //����ҽ��
                    this.queue.Doctor.ID = this.Reader[11].ToString();
                    //����
                    this.queue.SRoom.ID = this.Reader[12].ToString();
                    this.queue.SRoom.Name = this.Reader[13].ToString();
                    //��̨
                    this.queue.Console.ID = this.Reader[14].ToString();
                    this.queue.Console.Name = this.Reader[15].ToString();
                    //ר�ұ�־
                    this.queue.ExpertFlag = this.Reader[16].ToString();
                    //�������
                    this.queue.AssignDept.ID = this.Reader[17].ToString();
                    this.queue.AssignDept.Name = this.Reader[18].ToString();
                    this.queue.WaitingCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[19]);
                    this.al.Add(this.queue);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                if (!this.Reader.IsClosed) this.Reader.Close();
                this.Err = "��ѯ���ﻤʿվ���������Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return this.al;
        }

        /// <summary>
        ///  ���ݻ���վ��ʱ�� ��ȡһ�����ٴ��������Ķ���ʵ��
        /// </summary>
        /// <param name="nurseID"></param>
        /// <param name="queueDate"></param>
        /// <param name="noonID"></param>
        /// <returns></returns>
        public ArrayList QueryMinCount(string nurseID, DateTime queueDate, string noonID, string deptCode)
        {
            return this.QueryBase("Nurse.Queue.Query.4", nurseID, queueDate.ToString(), noonID, deptCode);
        }


        /// <summary>
        /// ��ѯ������ж�����̨�� ����,������Ϣ
        /// </summary>
        /// <returns></returns>
        public ArrayList Query(string nurse, FS.HISFC.Models.Nurse.EnuTriageStatus status,
            DateTime dtfrom, DateTime dtto, string noon)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Assign.Auto.Query.3", ref sql) == -1)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Assign.Auto.Query.3]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Assign.Auto.Query.3]";
                return null;
            }
            try
            {
                sql = string.Format(sql, nurse, status, dtfrom.ToString(), dtto.ToString(), noon);
            }
            catch (Exception e)
            {
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "����sql����!" + sql;
                this.ErrCode = "����sql����!" + sql;
                return null;
            }
            this.al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    this.queue = new FS.HISFC.Models.Nurse.Queue();
                    this.queue.ID = this.Reader[0].ToString();
                    this.queue.Console.ID = this.Reader[1].ToString();
                    this.queue.WaitingCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2]);
                    this.al.Add(this.queue);
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

            return this.al;
        }
        /// <summary>
        /// ����������̨������ڶ������ڲ�ѯ��̨�Ƿ�����
        /// </summary>
        /// <param name="consoleCode">��̨</param>
        /// <param name="noonID">���</param>
        /// <param name="queueDate">����ʱ��</param>
        /// <returns>false��ȡsql��������̨�ѱ�ʹ�� true ��û�б�ʹ��</returns>
        public bool QueryUsed(string consoleCode,string noonID,string queueDate)
        {
            string sqlStr = string.Empty;
            int returnValue = this.Sql.GetCommonSql("Nurse.Room.GetQueueByConsolecodeNoonIdQDate", ref sqlStr);
            if (returnValue < 0)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Room.GetQueueByConsolecodeNoonIdQDate]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Room.GetQueueByConsolecodeNoonIdQDate]";
                return false;
            }
            try
            {
                sqlStr = string.Format(sqlStr,consoleCode, noonID, queueDate);
            }
            catch (Exception e)
            {
                
                this.Err = "�ַ�ת������!" + e.Message;
                this.ErrCode = e.Message;
            }
            int myReturn = FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sqlStr));
            if (myReturn > 0)
            {
                
                this.Err = "����̨�Ѿ���ʹ�ã���ѡ��������̨";

                return false;
            }
            else if (myReturn < 0)
            {
                this.Err = "��ѯʧ��";
                return false;
            }
            return true;

        }

        #region addby xuewj 2010-11-2 ���ӽкŰ�ť{5A8B39E0-76A8-4e68-AF14-E2E0F45617D1}
        /// <summary>
        /// ������̨������ڶ������ڲ�ѯ����ID
        /// </summary>
        /// <param name="consoleCode">��̨</param>
        /// <param name="noonID">���</param>
        /// <param name="queueDate">����ʱ��</param>
        /// <returns>false��ȡsql��������̨�ѱ�ʹ�� true ��û�б�ʹ��</returns>
        public string QueryQueueID(string consoleCode, string noonID, string queueDate)
        {
            string queue = "";
            string sqlStr = string.Empty;
            int returnValue = this.Sql.GetCommonSql("Nurse.Room.GetQueueIDByConsolecodeNoonIdQDate", ref sqlStr);
            if (returnValue < 0)
            {
                this.Err = "��ѯsql����,����Ϊ[Nurse.Room.GetQueueIDByConsolecodeNoonIdQDate]";
                this.ErrCode = "��ѯsql����,����Ϊ[Nurse.Room.GetRoomByConsolecodeNoonIdQDate]";
                return null;
            }
            try
            {
                sqlStr = string.Format(sqlStr, consoleCode, noonID, queueDate);
                this.ExecQuery(sqlStr);

                int count = 1;
                while (this.Reader.Read())
                {
                    if (count > 1)
                    {
                        this.Err = "����̨�Ѿ���ʹ�ã���ѡ��������̨";
                        break;
                    }
                    queue = this.Reader[0].ToString();
                    count++;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return queue;
        } 
        #endregion

        /// <summary>
        /// �ж��Ƿ��л���
        /// </summary>
        /// <param name="roomID">���ұ��</param>
        /// <param name="seatID">��̨���</param>
        /// <param name="queueID">���б��</param>
        /// <param name="noonID">�����</param>
        /// <returns>true,�л���</returns>
        public bool ExistPatient(string roomID, string seatID, string queueID, string noonID)
        {
            string strsql = "";
            if (this.Sql.GetCommonSql("Nurse.Queue.Query.6", ref strsql) == -1)
            {
                return true;
            }

            try
            {
                strsql = string.Format(strsql, roomID, seatID, queueID, noonID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return true;
            }
            try
            {
                this.ExecSqlReturnOne(strsql);
            }
            catch (Exception ex)
            {

                string aaaa = ex.Message;
            }
                    
               
            string retv = this.ExecSqlReturnOne(strsql);

            if (retv == null || retv.Trim().Equals("0"))
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// ����ҽ�����롢�������ڡ����ȡ�������
        /// </summary>
        /// <param name="doctID">ҽ��ID</param>
        /// <param name="queDate">��������</param>
        /// <param name="noonID">���ID</param>
        /// <returns></returns>
        public FS.HISFC.Models.Nurse.Queue GetQueueByDoct(string doctID, DateTime queDate, string noonID)
        {
            string where = "";

            FS.HISFC.Models.Nurse.Queue queue = null;

            if (this.Sql.GetCommonSql("Nurse.Queue.Query.Where.1", ref where) == -1)
            {
                this.Err = "��ѯ���������Ϣ����![Nurse.Queue.Query.Where.1]";
                if (string.IsNullOrEmpty(where))
                {
                    where = @"
                        where doct_code='{0}'
                        and trunc(queue_date)=trunc(to_date('{1}','yyyy-mm-dd hh24:mi:ss'))
                        and noon_code='{2}'
                        and valid_flag = '1'";
                }
            }

            if (string.IsNullOrEmpty(where))
            {
                where = @"
                        where doct_code='{0}'
                        and trunc(queue_date)=trunc(to_date('{1}','yyyy-mm-dd hh24:mi:ss'))
                        and noon_code='{2}'
                        and valid_flag = '1'";
            }

            try
            {
                where = string.Format(where, doctID, queDate.ToString(), noonID);
            }
            catch (Exception e)
            {
                this.Err = "��ѯ���������Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            //sql = sql + " " + where;

            ArrayList alQueue = this.QueryBase(where);

            if (alQueue != null && alQueue.Count > 0)
            {
                queue = alQueue[0] as FS.HISFC.Models.Nurse.Queue;
            }

            return queue;
        }

        /// <summary>
        /// ��鵱��ͬ������̨�Ƿ��ѱ�����ҽ��ռ��
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        private bool IsRoomInUse(FS.HISFC.Models.Nurse.Queue queue)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Nurse.AutoTriage.DocorQueue.Query", ref strSql) == -1)
            {
                strSql = @"select tt.doct_code from met_nuo_queue tt
                   where tt.queue_date > to_date(to_char(sysdate, 'yyyy-mm-dd')||' 00:00:00','yyyy-mm-dd hh24:mi:ss')
                     and tt.noon_code = '{0}'
                     and tt.room_id = '{1}'";
            }
            strSql = string.Format(strSql, queue.Noon.ID, queue.SRoom.ID);
            string currentDoctCode = this.ExecSqlReturnOne(strSql);
            return !currentDoctCode.Equals("-1") && !currentDoctCode.Equals(queue.Doctor.ID);
        }


        /// <summary>
        /// ����ǰ��¼��ҽ�����½�������б������·�������
        /// </summary>
        /// <returns>0 ��̨��ռ��  1 ���б�����д��  -1 ���� </returns>
        public int UpdateDoctQueue(FS.HISFC.Models.Nurse.Queue queue)
        {
            return this.IsRoomInUse(queue) ? 0 : this.InsertDoctQueue(queue);
        }

        /// <summary>
        /// д������ҽ�����б�
        /// </summary>
        /// <param name="queue"></param>
        /// <returns>1 д������  -1 д�����</returns>
        private int InsertDoctQueue(FS.HISFC.Models.Nurse.Queue queue)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Nurse.AutoTriage.DocorQueue.Insert", ref strSql) == -1)
            {
                strSql =
                @"insert into met_nuo_queue
                (
                    Nurse_cell_code,queue_code,queue_name,
                    noon_code,queue_flag,sort_id,
                    valid_flag,oper_code, oper_date,
                    queue_date,doct_code,room_id,
                    room_name,console_code,console_name,
                    expert_flag,waiting_count,dept_code,
                    dept_name
                )
                values
                (
                    '{0}',  --Nurse_cell_code
                    '{1}',	--queue_code
                    '{2}',	--queue_name
                    '{3}',	--noon_code
                    '{4}',	--queue_flag
                    '{5}',	--sort_id
                    '{6}',	--valid_flag
                    '{7}',	--oper_code
                    to_date('{8}','yyyy-mm-dd HH24:mi:ss'),	--oper_date
                    to_date('{9}','yyyy-mm-dd HH24:mi:ss'),	--queue_date
                    '{10}',	--doct_code
                    '{11}',	--room_id
                    '{12}',	--room_name
                    '{13}',	--console_code
                    '{14}',	--console_name
                    '{15}',	--expert_flag
                    '{16}',	--waiting_count
                    '{17}',	--dept_code
                    '{18}'	--dept_name
                )";
            }

            strSql = string.Format(strSql,
                queue.ID,
                "T" + this.GetQueueNo(), //queueCode
                queue.Doctor.Name,//queueName
                queue.Noon.ID,
                "1",  //ҽ�����б�־
                "1",  //���
                "1",  //��Ч��־
                queue.Doctor.ID,
                queue.QueueDate.ToString(),
                queue.QueueDate.ToString(),
                queue.Doctor.ID,
                queue.SRoom.ID,
                queue.SRoom.Name,
                queue.Console.ID,
                queue.Console.Name,
                queue.ExpertFlag,
                "0",  //waitingCount��ʼ��Ϊ0
                queue.Dept.ID,
                queue.Dept.Name
                );

            if (this.ExecNoQuery(strSql) <= 0)//���벻�ɹ�
            {
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ҽ���˳�ҽ��վʱ�Զ������ҽ���Ķ�����Ϣ
        /// </summary>
        /// <param name="queue"></param>
        /// <returns></returns>
        public int DeleteDoctQueue(FS.HISFC.Models.Nurse.Queue queue)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Nurse.AutoTriage.DocorQueue.Delete", ref strSql) == -1)
            {
                strSql = @"
                    delete from met_nuo_queue t
                    where t.doct_code = '{0}'
	                  and t.console_code = '{1}' --Ϊ��ucOutPatientTree��roomID���Ӧ���ݿ����consoleID
                      and t.noon_code = '{2}'
	                  and t.queue_date > to_date(to_char(sysdate, 'yyyy-mm-dd')||' 00:00:00','yyyy-mm-dd hh24:mi:ss')
                    ";
            }
            strSql = string.Format(strSql, queue.Doctor.ID, queue.SRoom.ID, queue.Noon.ID);
            return this.ExecNoQuery(strSql);
        }

    }
}
