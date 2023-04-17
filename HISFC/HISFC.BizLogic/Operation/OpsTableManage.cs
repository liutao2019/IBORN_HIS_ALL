using System;
using System.Collections;
using System.Collections.Generic;
using FS.HISFC.Models.Operation;

namespace FS.HISFC.BizLogic.Operation
{
	/// <summary>
	/// OpsTableManage ��ժҪ˵����
	/// </summary>
	public class OpsTableManage : FS.FrameWork.Management.Database 
	{
		public OpsTableManage()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ��Ա����
		/// <summary>
		/// ����������̨���a
		/// </summary>
		/// <returns></returns>
		public string GetNewTableNo()
		{
			string strNewNo = string.Empty;
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableManage.GetNewConsoleNo.1",ref strSql) == -1) 
			{
				return strNewNo; //���ַ���
			}
			if (strSql == null) return strNewNo;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					strNewNo = Reader[0].ToString();
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return strNewNo;            
			}
			this.Reader.Close();
			strNewNo = strNewNo.PadLeft(4,'0');
			return strNewNo;
		}
		/// <summary>
		/// ��ָ����������������̨
		/// </summary>
		/// <param name="OpsTableAl">����̨��������</param>
		/// <returns>0 success -1 fail</returns>
		public int AddOpsTable(ArrayList OpsTableAl)
		{
			string strSql = string.Empty;			
			foreach(FS.HISFC.Models.Operation.OpsTable thisOpsTable in OpsTableAl)
			{
				strSql = string.Empty;
				if(this.Sql.GetSql("Operator.OpsTableManage.AddOpsTable.1",ref strSql) == -1) return -1;
				try
				{						
					strSql = string.Format(strSql,thisOpsTable.ID.ToString(),thisOpsTable.Name,
						thisOpsTable.InputCode,thisOpsTable.Dept.ID.ToString(),
						thisOpsTable.RoomID,thisOpsTable.Remark ,thisOpsTable.User.ID.ToString());
				}
				catch(Exception ex)
				{
					this.Err = ex.Message;
					this.ErrCode = ex.Message;
					return -1;            
				}
				if (strSql == null) return -1;				
				if(this.ExecNoQuery(strSql) == -1) return -1;
			}
			return 0;
		}

        /// <summary>
        /// ��ָ����������������̨
        /// </summary>
        /// <param name="tables">����̨��������</param>
        /// <returns>0 success -1 fail</returns>
        /// Robin   2007-01-19
        public int AddOpsTable(List<OpsTable> tables)
        {
            string strSql = string.Empty;
            foreach (FS.HISFC.Models.Operation.OpsTable thisOpsTable in tables)
            {
                strSql = string.Empty;
                if (this.Sql.GetSql("Operator.OpsTableManage.AddOpsTable.1", ref strSql) == -1) return -1;
                try
                {
                    strSql = string.Format(strSql, thisOpsTable.ID, thisOpsTable.Name,
                        thisOpsTable.InputCode, thisOpsTable.Dept.ID,
                        thisOpsTable.Room.ID, thisOpsTable.Memo, thisOpsTable.User.ID.ToString(), FS.FrameWork.Function.NConvert.ToInt32(thisOpsTable.IsValid).ToString());
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.ErrCode = ex.Message;
                    return -1;
                }
                if (strSql == null) return -1;
                if (this.ExecNoQuery(strSql) == -1) return -1;
            }
            return 0;
        }
		/// <summary>
		/// �޸�����̨��Ϣ
		/// </summary>
		/// <param name="OpsTable">����̨����</param>
		/// <returns>0 success -1 fail</returns>
		public int UpdateOpsTable(FS.HISFC.Models.Operation.OpsTable OpsTable)
		{
			string strSql = string.Empty;
			string strValid = FS.FrameWork.Function.NConvert.ToInt32(OpsTable.IsValid).ToString();
			if(this.Sql.GetSql("Operator.OpsTableManage.UpdateOpsTable.1",ref strSql) == -1) return -1;			
			
			try
			{								
				strSql = string.Format(strSql,OpsTable.ID.ToString(),OpsTable.Name,OpsTable.InputCode,
					OpsTable.Dept.ID.ToString(),OpsTable.RoomID,strValid,OpsTable.Remark,OpsTable.User.ID.ToString());
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			if (strSql == null) return -1;			
			if(this.ExecNoQuery(strSql) == -1) return -1;
			return 0;
		}
		/// <summary>
		/// ɾ������̨
		/// </summary>
		/// <param name="OpsTable">����̨���</param>
		/// <returns>0 success -1 fail</returns>
		public int DelOpsTable(string OpsTableId)
		{
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableManage.DelOpsTable.1",ref strSql) == -1) return -1;
			
			try
			{	
				strSql = string.Format(strSql,OpsTableId);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			if (strSql == null) return -1;			
			return this.ExecNoQuery(strSql);
		}

        /// <summary>
        /// ɾ������������������̨
        /// </summary>
        /// <param name="roomID">������ID</param>
        /// <returns></returns>
        /// Robin   2007-01-19
        public int DelOpsTables(string roomID)
        {
            string strSql = string.Empty;

            if (this.Sql.GetSql("Operator.OpsTableManage.DelOpsRoom.2", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, roomID);
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
		/// ��ȡָ�������ҵ�����̨�б�
		/// </summary>
		/// <param name="OpsRoomID">�������</param>
		/// <returns>����̨��������</returns>
		public ArrayList GetOpsTable(string OpsRoomID)
		{
			ArrayList OpsTableAl = new ArrayList();
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableManage.GetOpsTable.1",ref strSql) == -1) return OpsTableAl;
			try
			{
				strSql = string.Format(strSql,OpsRoomID);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return OpsTableAl;            
			}
			if (strSql == null) return OpsTableAl;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Operation.OpsTable thisTable = new FS.HISFC.Models.Operation.OpsTable();

					thisTable.ID = Reader[0].ToString();//����̨����
									
					thisTable.Name = Reader[1].ToString();//����̨����
					
					thisTable.InputCode = Reader[2].ToString();//������
					
					thisTable.Dept.ID = Reader[3].ToString();//�����ұ���
					
					thisTable.RoomID = Reader[4].ToString();//��������
					
					string strValid = Reader[5].ToString();//1��Ч0��Ч
					thisTable.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(strValid); 
					
					thisTable.Memo= Reader[6].ToString();//��ע
					
					OpsTableAl.Add(thisTable);
				}
			}
			catch(Exception ex)
			{
				this.Err="�������̨��Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return OpsTableAl;
			}
			this.Reader.Close();
			return OpsTableAl;
		}

        /// <summary>
        /// ��ȡָ�������ҵ�����̨�б�
        /// </summary>
        /// <param name="roomID">�������</param>
        /// <returns>����̨��������</returns>
        /// Robin   2007-01-16
        public List<OpsTable> GetOperationTables(string roomID)
        {
            ArrayList tables = this.GetOpsTable(roomID);
            List<OpsTable> ret = new List<OpsTable>();
            foreach(OpsTable table in tables)
            {
                ret.Add(table);
            }

            return ret;
        }
		/// <summary>
		/// ��ȡָ�������ҵ�����̨�б�
		/// </summary>
		/// <param name="OpsRoomID">���ұ���</param>
		/// <returns>����̨��������</returns>
		public ArrayList GetOpsTableByDept(string DeptID)
		{
			ArrayList OpsTableAl = new ArrayList();
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableManage.GetOpsTable.2",ref strSql) == -1) return OpsTableAl;
			try
			{
				strSql = string.Format(strSql,DeptID);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return OpsTableAl;            
			}
			if (strSql == null) return OpsTableAl;
			this.ExecQuery(strSql);
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Operation.OpsTable thisTable = new FS.HISFC.Models.Operation.OpsTable();

					thisTable.ID = Reader[0].ToString();//����̨����
									
					thisTable.Name = Reader[1].ToString();//����̨����
					
					thisTable.InputCode = Reader[2].ToString();//������
					
					thisTable.Dept.ID = Reader[3].ToString();//�����ұ���
					
					thisTable.RoomID = Reader[4].ToString();//��������
					
					string strValid = Reader[5].ToString();//1��Ч0��Ч
					thisTable.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(strValid); 
					
					thisTable.Remark= Reader[6].ToString();//��ע
					
					OpsTableAl.Add(thisTable);
				}
			}
			catch(Exception ex)
			{
				this.Err="�������̨��Ϣ����"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				return OpsTableAl;
			}
			this.Reader.Close();
			return OpsTableAl;
		}
		/// <summary>
		/// ��������̨��Ż������̨����
		/// </summary>
		/// <param name="strID">����̨���</param>
		/// <returns>����̨����</returns>
		public string GetTableNameFromID(string strID)
		{
			string strSql = string.Empty;
			string strName = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableManage.GetOpsTableFromID.1",ref strSql) == -1) return strName;
			try
			{
				strSql = string.Format(strSql,strID);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return strName;            
			}
			if (strSql == null) return strName;
			this.ExecQuery(strSql);
			while(this.Reader.Read())
			{
				try
				{
					strName = Reader[0].ToString();//����̨����
				}
				catch(Exception ex)
				{
					this.Err=ex.Message;
					this.WriteErr();
				}
			}
			this.Reader.Close();
			return strName;
		}
		/// <summary>
		/// ���������
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
		public int AddOpsRooms(ArrayList rooms)
		{
			string strSql = string.Empty,strValid=string.Empty;			
			foreach(FS.HISFC.Models.Operation.OpsRoom room in rooms)
			{
				strSql = string.Empty;
				if(this.Sql.GetSql("Operator.OpsTableManage.AddOpsRooms.1",ref strSql) == -1) return -1;
				try
				{	
					if(room.IsValid)
						strValid="1";
					else
						strValid="0";

					strSql = string.Format(strSql,room.ID,room.Name,room.InputCode,room.DeptID,
						strValid,room.OperCode);
				}
				catch(Exception ex)
				{
					this.Err = ex.Message;
					this.ErrCode = ex.Message;
					return -1;            
				}
				if (strSql == null) return -1;				
				if(this.ExecNoQuery(strSql) == -1) return -1;
			}
			return 0;
		}

        /// <summary>
        /// ���������
        /// </summary>
        /// <param name="room"></param>
        /// <returns></returns>
        public int AddOpsRoom(OpsRoom room)
        {
            string strSql = string.Empty, strValid = string.Empty;	
            strSql = string.Empty;
            if (this.Sql.GetSql("Operator.OpsTableManage.AddOpsRooms.1", ref strSql) == -1) return -1;
            try
            {
                if (room.IsValid)
                    strValid = "1";
                else
                    strValid = "0";

                strSql = string.Format(strSql, room.ID, room.Name, room.InputCode, room.DeptID,
                    strValid, room.OperCode,room.IpAddress);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            if (strSql == null) return -1;
            return this.ExecNoQuery(strSql);
        }
		/// <summary>
		/// ����������
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
		public int UpdateOpsRoom(FS.HISFC.Models.Operation.OpsRoom room)
		{
			string strSql = string.Empty;
			string strValid = FS.FrameWork.Function.NConvert.ToInt32(room.IsValid).ToString();
			if(this.Sql.GetSql("Operator.OpsTableManage.UpdateOpsRoom.1",ref strSql) == -1) return -1;			
			
			try
			{								
				strSql = string.Format(strSql,room.Name,room.InputCode,
					strValid,room.DeptID,room.OperCode,room.ID,room.IpAddress);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1;            
			}
			if (strSql == null) return -1;	
		
			return this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ɾ��������
		/// </summary>
		/// <param name="room"></param>
		/// <returns></returns>
		public int DelOpsRoom(FS.HISFC.Models.Operation.OpsRoom room)
		{
			string strSql=string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableManage.DelOpsRoom.1",ref strSql)==-1)return -1;
			try
			{
				strSql=string.Format(strSql,room.ID);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1; 
			}
			if(this.ExecNoQuery(strSql)==-1)return -1;
			//ɾ���������µ�����̨
			if(this.Sql.GetSql("Operator.OpsTableManage.DelOpsRoom.2",ref strSql)==-1)return -1;
			try
			{
				strSql=string.Format(strSql,room.ID);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return -1; 
			}
			if(this.ExecNoQuery(strSql)==-1)return -1;
			return 0;
		}
		/// <summary>
		/// ���ݿ��һ�ÿ�����������
		/// </summary>
		/// <param name="deptID"></param>
		/// <returns></returns>
		public ArrayList GetRoomsByDept(string deptID)
		{
			ArrayList rooms = new ArrayList();
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsTableManage.GetRoomsByDept.1",ref strSql) == -1) return rooms;
			try
			{
				strSql = string.Format(strSql,deptID);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return rooms;
			}
			if (strSql == null) 
				return rooms;
			if(this.ExecQuery(strSql)==-1)return rooms;
			try
			{
				while(Reader.Read())
				{
					FS.HISFC.Models.Operation.OpsRoom room=new FS.HISFC.Models.Operation.OpsRoom();
					room.ID=Reader[2].ToString();//����
					room.Name=Reader[3].ToString();//����
					room.InputCode=Reader[4].ToString();//������
					room.DeptID=deptID;//��������
					room.IsValid=FS.FrameWork.Function.NConvert.ToBoolean(Reader[6].ToString());
					room.OperCode=Reader[7].ToString();
					room.OperDate=FS.FrameWork.Function.NConvert.ToDateTime(Reader[8].ToString());
                    if (this.Reader.FieldCount > 9)
                    {
                        room.IpAddress = this.Reader[9].ToString();
                    }
					rooms.Add(room);
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				if(this.Reader.IsClosed==false)
					Reader.Close();
				return rooms;
			}

			Reader.Close();
			return rooms;
		}
		/// <summary>
		/// ���ݷ���Ż����������
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Operation.OpsRoom GetRoomByID(string ID)
		{
			string sql=string.Empty;
			FS.HISFC.Models.Operation.OpsRoom room=null;

			if(this.Sql.GetSql("Operator.OpsTableManage.GetRoomsByID.1",ref sql)==-1)return null;
			try
			{
				sql = string.Format(sql,ID);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				return null;    
			}
		
			if(this.ExecQuery(sql)==-1)return null;
			try
			{
				while(Reader.Read())
				{
					room=new FS.HISFC.Models.Operation.OpsRoom();
					room.ID=Reader[2].ToString();//����
					room.Name=Reader[3].ToString();//����
					room.InputCode=Reader[4].ToString();//������
					room.DeptID=Reader[5].ToString();//��������
					room.IsValid=FS.FrameWork.Function.NConvert.ToBoolean(Reader[6].ToString());
					room.OperCode=Reader[7].ToString();
					room.OperDate=FS.FrameWork.Function.NConvert.ToDateTime(Reader[8].ToString());					
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.ErrCode = ex.Message;
				if(this.Reader.IsClosed==false)Reader.Close();
				return null;
			}

			Reader.Close();
			return room;
		}
		/// <summary>
		/// ��ȡ�·����
		/// </summary>
		/// <returns></returns>
		public string GetNewRoomID()
		{
			string strSql = string.Empty;
			if(this.Sql.GetSql("Operator.OpsRoomManage.GetNewRoomID.1",ref strSql) == -1) 
			{
				return string.Empty;
			}
			
			return this.ExecSqlReturnOne(strSql);			
		}
        /// <summary>
        /// �жϵ�ǰ������̨�Ƿ����Ѱ��Ź���¼
        /// </summary>
        /// <param name="OpsTableID"> ����̨��</param>
        /// <returns>������-1 �а��ż�¼1,û�а��ż�¼0</returns>
        private int OpsTableIsUsing(string OpsTableID)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.OpsTableManage.OpsTableIsUsing.1", ref strSql) == -1)
            {
                this.Err = "û���ҵ�SQL Operator.OpsTableManage.OpsTableIsUsing.1 ";
                return -1;
            }
            strSql = string.Format(strSql, OpsTableID);
            this.ExecQuery(strSql);
            int i = 0;
            try
            {
                while (this.Reader.Read())
                {
                    i++;
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Reader.Close();
                this.Err = ex.Message;
                return -1;
            }
            return i;
        }
        /// <summary>
        /// �жϵ�ǰ�����������Ƿ����Ѱ��Ź���¼
        /// </summary>
        /// <param name="OpsRoomID">���������</param>
        /// <returns>������-1 �а��ż�¼1,û�а��ż�¼0</returns>
        private int OpsRoomIsUsing(string OpsRoomID)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.OpsTableManage.OpsRoomIsUsing.1", ref strSql) == -1)
            {
                this.Err = "û���ҵ�SQL Operator.OpsTableManage.OpsRoomIsUsing.1 ";
                return -1;
            }
            strSql = string.Format(strSql, OpsRoomID);
            this.ExecQuery(strSql);
            int i = 0;
            try
            {
                while (this.Reader.Read())
                {
                    return i++;
                }
                this.Reader.Close();
            }
            catch (Exception ex)
            {
                this.Reader.Close();
                this.Err = ex.Message;
                return -1;
            }
            return i;
        }
		#endregion

        #region ��ѯ����̨��Ӧ��״̬�б�
        public ArrayList GetOpsState(DateTime dateTime)
        {
            ArrayList allOpsState = new ArrayList();
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.OpsTableManage.OpsRoomOpsState.select", ref strSql) == -1)
            {
                this.Err = "û���ҵ�SQL Operator.OpsTableManage.OpsRoomOpsState.select";
                return null;
            }
            strSql = string.Format(strSql,dateTime);
            this.ExecQuery(strSql);
            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject opsState = new FS.FrameWork.Models.NeuObject();
                    opsState.ID = this.Reader[0].ToString();
                    opsState.Name = this.Reader[1].ToString();
                    opsState.Memo = this.Reader[2].ToString();
                    allOpsState.Add(opsState);
                }
            }
            catch (Exception ex)
            {
                this.Reader.Close();
                this.Err = ex.Message;
                return null;
            }
            return allOpsState;

        }

        /// <summary>
        ///��������������Ϣ
        /// </summary>
        /// <param name="?"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateOpsState(FS.HISFC.Models.Operation.OperationAppllication opsInfo,string state)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.OpsTableManage.OpsRoomOpsState.update", ref strSql) == -1)
            {
                this.Err = "û���ҵ�SQL Operator.OpsTableManage.OpsRoomOpsState.update";
                return -1;
            }
            strSql = string.Format(strSql,opsInfo.ID, state, opsInfo.PreDate, this.Operator.ID, this.GetDateTimeFromSysDateTime());
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����������Ϣ
        /// </summary>
        /// <param name="opsInfo"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int InsertOpsState(FS.HISFC.Models.Operation.OperationAppllication opsInfo, string state)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Operator.OpsTableManage.OpsRoomOpsState.insert", ref strSql) == -1)
            {
                this.Err = "û���ҵ�SQL Operator.OpsTableManage.OpsRoomOpsState.insert";
                return -1;
            }
            strSql = string.Format(strSql, opsInfo.ID, state, opsInfo.PreDate, this.Operator.ID, this.GetDateTimeFromSysDateTime());
            return this.ExecNoQuery(strSql);
        }
        #endregion
    }
}
