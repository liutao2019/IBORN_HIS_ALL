using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.Nurse
{
    public class Seat : FS.FrameWork.Management.Database
    {
        public Seat()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		#region ���ֶ�
		//	CONSOLE_CODE,                           --��̨����
		//	CONSOLE_NAME,                           --��̨����
		//	INPUT_CODE,                             --������
		//	ROOM_CODE,                              --���Ҵ���
		//	ROOM_NAME,                              --��������
		//	VALID_FLAG,                             --1��Ч/0��Ч
		//	REMARK,                                 --��ע
		//	OPER_CODE,                              --����Ա
		//	OPER_DATE                               --����ʱ��
		#endregion

		#region ����
		/// <summary>
		/// �������Һ����ȡ��̨
		/// </summary>
		/// <param name="roomNo"></param>
		/// <returns></returns>
		public ArrayList Query(string roomNo)
		{
			ArrayList al = new ArrayList();
			string strSQL;
			string strWhere = "";
			strSQL = this.GetSqlInjectInfo();
			if(this.Sql.GetCommonSql("Nurse.Seat.Query.1",ref strWhere) == -1) return null;
			strSQL = strSQL + strWhere;
			strSQL = string.Format(strSQL,roomNo);
			al = this.myGetInfo(strSQL);
			return al;
		}
		/// <summary>
		/// �������Һ����ȡ��Ч��̨
		/// </summary>
		/// <param name="roomNo"></param>
		/// <returns></returns>
		public ArrayList QueryValid(string roomNo)
		{
			ArrayList al = new ArrayList();
			string strSQL;
			string strWhere = "";
			strSQL = this.GetSqlInjectInfo();
			if(this.Sql.GetCommonSql("Nurse.Seat.Query.2",ref strWhere) == -1) return null;
			strSQL = strSQL + strWhere;
			strSQL = string.Format(strSQL,roomNo);
			al = this.myGetInfo(strSQL);
			return al;
		}
		/// <summary>
		/// ����һ���µ���̨��Ϣ
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int Insert(FS.HISFC.Models.Nurse.Seat info)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Nurse.Seat.Insert",ref sql) == -1)return -1;

			try
			{
                sql = string.Format(sql, info.ID, info.Name, info.PRoom.InputCode, info.PRoom.ID, info.PRoom.Name,
                    info.PRoom.IsValid, info.Memo, info.Oper.ID, info.Oper.OperTime);
			}
			catch(Exception e)
			{
				this.Err = "ת������!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}
			return this.ExecNoQuery(sql);
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int Update(FS.HISFC.Models.Nurse.Seat info)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Nurse.Seat.Update",ref sql) == -1) return -1;

			try
			{
                sql = string.Format(sql, info.ID, info.Name, info.PRoom.InputCode, info.PRoom.ID, info.PRoom.Name,
                    info.PRoom.IsValid, info.Memo, info.Oper.ID, info.Oper.OperTime);
			}
			catch(Exception e)
			{
				this.Err ="ת������!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}
		/// <summary>
		/// ������̨����ɾ��
		/// </summary>
		/// <param name="strId"></param> 
		/// <returns></returns>
		public int DeleteByConsole(string strId)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Nurse.Seat.Delete.1",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,strId);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}			
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// �������Һ���ɾ��������Ϣ
		/// </summary>
		/// <param name="roomCode"></param>
		/// <returns></returns>
		public int DeleteByRoom(string roomCode)
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Nurse.Seat.Delete.2",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,roomCode);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}			
			return this.ExecNoQuery(strSql);
		}

        /// <summary>
        /// ��ѯҪɾ������̨�Ƿ񱻶���ά��
        /// </summary>
        /// <param name="roomID">��̨</param>
        /// <param name="strDate">ϵͳʱ��</param>
        /// <returns></returns>
        public int QueryConsole(string consoleID, string strDate)
        {
            string strsql = "";
            if (this.Sql.GetCommonSql("Nurse.Seat.GetConsoleUsed", ref strsql) == -1)
            {
                this.Err = "�õ�Nurse.Seat.GetConsoleUsedʧ��";
                return -1;
            }
            try
            {
                strsql = string.Format(strsql, consoleID, strDate);

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strsql));
        }

        /// <summary>
        /// ��ѯmet_nuo_assignrecord���Ƿ��з�����������̨�Ƿ�����
        /// </summary>
        /// <param name="roomID">��̨����</param>
        /// <returns></returns>
        public int QuerySeatByConsoleID(string consoleID)
        {
            string strsql = string.Empty;
            if (this.Sql.GetCommonSql("Nurse.Seat.GetRoomByConsoleID", ref strsql) == -1)
            {
                this.Err = "�õ�Nurse.Seat.GetRoomByConsoleIDʧ��";
                return -1;
            }

            try
            {
                strsql = string.Format(strsql, consoleID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            } return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strsql));
        }
		#endregion

		#region ������Ϣ
		/// <summary>
		/// ��ȡ����SQL���
		/// </summary>
		/// <returns></returns>
		public string GetSqlInjectInfo() 
		{
			string strSql = "";
			if (this.Sql.GetCommonSql("Nurse.Seat.Query",ref strSql)==-1) return null;
			return strSql;
		}
		/// <summary>
		/// ����SQL,��ȡʵ������
		/// </summary>
		/// <param name="SQLString"></param>
		/// <returns></returns>
		public ArrayList myGetInfo(string SQLString) 
		{
			ArrayList al=new ArrayList();         
			//ִ�в�ѯ���
			if (this.ExecQuery(SQLString)==-1) 
			{
				this.Err="���ע����Ϣʱ��ִ��SQL������"+this.Err; 
				this.ErrCode="-1";
				return null;
			}
			try 
			{
				while (this.Reader.Read())  
				{
					#region �����ת��Ϊʵ��
					FS.HISFC.Models.Nurse.Seat info = new FS.HISFC.Models.Nurse.Seat();
					
					info.ID = this.Reader[0].ToString();//��̨����
					info.Name = this.Reader[1].ToString();//��̨����
                    info.PRoom.InputCode = this.Reader[2].ToString();//������
                    info.PRoom.ID = this.Reader[3].ToString();//���Ҵ���
                    info.PRoom.Name = this.Reader[4].ToString();//��������
                    info.PRoom.IsValid = this.Reader[5].ToString();//1��Ч/0��Ч
                    info.PRoom.Memo = this.Reader[6].ToString();//��ע
					info.Oper.ID = this.Reader[7].ToString();//����Ա
					info.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);//����ʱ��
					info.CurrentCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9]);
					#endregion
					al.Add(info);
				}
			}//�׳�����
			catch(Exception ex) 
			{
				this.Err="�����̨��Ϣʱ����"+ex.Message;
				this.ErrCode="-1";
				return null;
			}
			this.Reader.Close();
			this.ProgressBarValue=-1;
			return al;
		}
		#endregion
	}
}