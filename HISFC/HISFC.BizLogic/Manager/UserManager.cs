using System;
using System.Data.OracleClient;
using FS.FrameWork.Models;
using System.Collections;

namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	///�û����������
	/// </summary>
	public class UserManager:DataBase 
	{
		public UserManager()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// У���û�����-�����û�id
		/// </summary>
		/// <param name="LoginID"></param>
		/// <param name="Password"></param>
		/// <returns>-1 ���� ���� UserID</returns>
		public string CheckPwd(string LoginID,string Password)
		{
			//����sql���
			string[] arg=new string[2];
			string sql="";
			if(this.GetSQL("UserManager.CheckPassword",ref sql)==-1) return "-1";
			arg[0]=LoginID;
			arg[1]=Password;
			sql=string.Format(sql,arg);
			//ִ��sql���
			this.ExecQuery(sql);
			try
			{
				 this.Reader.Read();
				 return this.Reader[0].ToString();
			}//�׳�����
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode="-1";
			}
			return "-1";
		}
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="LoginID"></param>
		/// <param name="OldPassword"></param>
		/// <param name="newPassword"></param>
		/// <returns></returns>
		public int ChangePassword(string LoginID,string OldPassword,string newPassword)
		{
			string sql ="";
			if(this.GetSQL("Manager.UserManager.ChangePassword",ref sql)==-1) return -1;
			try
			{
				sql = string.Format(sql,LoginID,OldPassword,newPassword);
			}
			catch{}
			if(this.ExecNoQuery(sql)<=0) return -1;
			return 0;
		}
		/// <summary>
		/// ͨ��ID�������Ա��Ϣ 
		/// </summary>
		/// <param name="ID">ID</param>
		/// <returns>Person��</returns>
		public FS.HISFC.Models.Base.Employee  GetPerson(string ID)
		{
			FS.HISFC.Models.Base.Employee Person = new FS.HISFC.Models.Base.Employee();
			
			//����sql���
			string[] arg = new string[1];
			string sql = "";
			arg[0] = ID;
			#region ����û�������Ϣ
			if(this.GetSQL("UserManager.2",ref sql) == -1) 
			{
				this.Err = this.Sql.Err;
				return Person;
			}
				sql = string.Format(sql,arg);
				//ִ��sql���
				if(this.ExecQuery(sql)==-1) return null;
				try
				{
					this.Reader.Read();
					Person.ID=ID;
					//******************************************
					//UserManager.2 Sql�����뷵�صĲ��� 
					//0 ����,
					//1   ְ��id
					//2         name
					//3      ����id
					//4         name
					//5      ����id
					//6         name
					//7       sexid
					//8       ��Ա����id
					//9           name
					//10      �Ƿ�ר�� 0,1
					//11      ר��id
					//12          name
					//13      �Ƿ���ҩ
					//14      �Ƿ����Ա
					Person.Name=this.Reader[0].ToString();
					Person.Duty.ID=this.Reader[1].ToString() +"";
					Person.Duty.Name=this.Reader[2].ToString() +"";
					Person.Dept.ID=this.Reader[3].ToString() +"";
					Person.Dept.Name=this.Reader[4].ToString() +"";

					Person.Nurse.ID=this.Reader[5].ToString() +"";
					Person.Nurse.Name=this.Reader[6].ToString()+"";
					{Person.Sex.ID =this.Reader[7].ToString();}
					Person.EmployeeType.ID=this.Reader[8].ToString()+"";
					//Person.PersonType.Name=this.Reader[9].ToString()+"";
					Person.IsExpert=FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[10]);
					Person.Expert.ID=this.Reader[11].ToString()+"";
					Person.Expert.Name=this.Reader[12].ToString()+"";
					try
					{
						Person.IsPermissionAnesthetic=FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[13]);
					}catch{}
					try
					{
						Person.IsManager = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[14]);
					}
					catch{}
                    Person.ValidState = (HISFC.Models.Base.EnumValidState)FS.FrameWork.Function.NConvert.ToInt32( this.Reader[15] );
					this.Reader.Close();
				}//�׳�����
				catch(Exception ex)
				{
					this.Err=ex.Message;
					this.ErrCode="-1";
					WriteErr();
				}
			#endregion
			#region ����û�Ȩ����Ϣ
            Person.PermissionGroup = this.GetPersonGroupList(ID);
			#endregion
			return Person;
		}
		
		/// <summary>
		/// ���õ�ǰ�û���
		/// </summary>
		/// <param name="Person"></param>
		/// <param name="index"></param>
		public void SetGroup(FS.HISFC.Models.Base.Employee Person,int index)
		{
			try
			{
				Person.CurrentGroup=(NeuObject)Person.PermissionGroup[index];
			}
			catch{}	
		}
		
		/// <summary>
		/// ����Ա�������ȡԱ����Ĭ�������ϵͳ��½��Ϣ
		/// </summary>
		/// <param name="emplCode">�û�����</param>
		/// <returns>�ɹ�����neuobject���� ʧ�ܷ���null δ�ҵ����ݷ����½�neuobject</returns>
		public ArrayList GetLoginInfoByEmplCode(string emplCode)
		{
			string strSql = "";
			if (this.GetSQL("Manager.UserManager.GetLoginInfo.EmplCode",ref strSql) == -1)
			{
				this.Err = "����sql���ʱ���� ����sql�����xml�ڲ�����";
				return null;
			}

			try
			{
				strSql = string.Format(strSql,emplCode);
			}
			catch (Exception ex)
			{
				this.Err = "��ʽ����ѯsql�ַ���ʱ����" + ex.Message;
				return null;
			}
			
			ArrayList al = new ArrayList();
			FS.FrameWork.Models.NeuObject info;
			try
			{
				this.ExecQuery(strSql); 
				while (this.Reader.Read())
				{
					info = new NeuObject();
					info.ID = this.Reader[0].ToString();			//�û�����
					info.Name = this.Reader[1].ToString();			//�û�����
					info.Memo = this.Reader[2].ToString();			//����Ա���
					info.User01 = this.Reader[3].ToString();		//��½��
					info.User02 = this.Reader[4].ToString();		//��½����

					al.Add(info);
				}
				this.Reader.Close();
			}
			catch (Exception ex)
			{
				this.Err = "ִ��sql������" + ex.Message;
				return null;
			}

			if (al.Count == 0)
			{
				info = new NeuObject();
				al.Add(info);
			}

			return al;
		}

        public ArrayList GetAllPeronList()
        {
            ArrayList al = new ArrayList();
            try
            {
                string sql = "";
                //�ӿ�˵�� 0 id ,1 name
                if (this.GetSQL("Manager.UserManager.GetAllPersonList", ref sql) == -1) return null;
                if (this.ExecQuery(sql) == -1) return null;

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Base.Employee obj = new FS.HISFC.Models.Base.Employee();
                    obj.ID = this.Reader[0].ToString();//id
                    obj.Name = this.Reader[1].ToString();//name
                    al.Add(obj);
                }
                this.Reader.Close();
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                al = null;
            }
            return al;
        }
		/// <summary>
		/// ���ȫ����Ա��Ϣ
		/// </summary>
		/// <returns></returns>
		public ArrayList GetPeronList()
		{
			ArrayList al=new ArrayList();
			try
			{
				string sql ="";
				//�ӿ�˵�� 0 id ,1 name,2 password,3 loginname,4 ismanager
				if(this.GetSQL("Manager.UserManager.GetPersonList", ref sql)==-1) return null;
				if(this.ExecQuery(sql)==-1) return null;
				
				while(this.Reader.Read())
				{
                    FS.HISFC.Models.Base.Employee obj = new FS.HISFC.Models.Base.Employee();
					obj.ID=this.Reader[0].ToString();//id
					obj.Name =this.Reader[1].ToString();//name
					obj.Password =this.Reader[2].ToString();//parent id
					obj.User01 =this.Reader[3].ToString();//��½��
					obj.IsManager =FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[4]);
					obj.SpellCode = this.Reader[6].ToString(); //ƴ����
					obj.WBCode =this.Reader[7].ToString(); //�����
					al.Add(obj);
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err= ee.Message;
				al = null;
			}
			return al;
		}

        /// <summary>
		/// ���ݿ��ұ����ȡȫ����Ա��Ϣ
        /// {1D7BC020-92AC-431b-B27B-1BFBEB0E566B}
		/// </summary>
		/// <returns></returns>
		public ArrayList GetPeronList(string deptCode)
		{
			ArrayList al=new ArrayList();
			try
			{
				string sql ="";
				//�ӿ�˵�� 0 id ,1 name,2 password,3 loginname,4 ismanager
                if (this.GetSQL("Manager.UserManager.GetPersonList.Dept", ref sql) == -1)
                {
                    return null;
                }
                sql = string.Format(sql, deptCode);

				if(this.ExecQuery(sql)==-1) return null;
				
				while(this.Reader.Read())
				{
                    FS.HISFC.Models.Base.Employee obj = new FS.HISFC.Models.Base.Employee();
					obj.ID=this.Reader[0].ToString();//id
					obj.Name =this.Reader[1].ToString();//name
					obj.Password =this.Reader[2].ToString();//parent id
					obj.User01 =this.Reader[3].ToString();//��½��
					obj.IsManager =FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[4]);
					obj.SpellCode = this.Reader[6].ToString(); //ƴ����
					obj.WBCode =this.Reader[7].ToString(); //�����
					al.Add(obj);
				}
				this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err= ee.Message;
				al = null;
			}
			return al;
		}

		/// <summary>
		/// �����Ա����
		/// </summary>
		/// <param name="PersonID"></param>
		/// <returns></returns>
		public ArrayList GetPersonGroupList(string PersonID)
		{
			string sql ="";
			//�ӿ�˵�� 0 id ,1 name
			if(this.GetSQL("Manager.UserManager.GetPersonGroupList", ref sql)==-1) return null;
			sql = string.Format(sql,PersonID,FS.FrameWork.Management.Connection.Hospital.ID);

			if(this.ExecQuery(sql)==-1) return null;

			ArrayList al=new ArrayList();
			
			while(this.Reader.Read())
			{
                FS.HISFC.Models.Admin.SysGroup obj = new FS.HISFC.Models.Admin.SysGroup();
				try
				{
					obj.ID=this.Reader[0].ToString();
					obj.Name =this.Reader[1].ToString();
                    obj.Memo = this.Reader[3].ToString();
                    obj.ParentGroup.ID = this.Reader[4].ToString();
                    obj.ParentGroup.Name = this.Reader[5].ToString();
				}
				catch
				{}
				al.Add(obj);
			}

			this.Reader.Close();
			return al;
		}

		/// <summary>
		/// ������Ա��
		/// </summary>
		/// <returns></returns>
		public int InsertPersonGroup(FS.HISFC.Models.Base.Employee Person,FS.FrameWork.Models.NeuObject Group)
		{
			string strSql="";
			if(this.GetSQL("Manager.UserManager.PersonGroup.Insert.1",ref strSql)==-1) return -1;
			//if(this.GetSQL("Manager.UserManager.PersonGroup.Update.1",ref strSql1)==-1) return -1;
			try
			{
				string[] s = new string[8];
				s[0] = Group.ID;
				s[1] = Group.Name;
				s[2] =  Person.ID;
				s[3] =  Person.Name;
				s[4] =  Person.Password;
				s[5] =  Person.User01;
				s[6] =  FS.FrameWork.Function.NConvert.ToInt32(Person.IsManager).ToString();
				s[7] =  this.Operator.ID;
				strSql=string.Format(strSql,s);
			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0)//insert 
			{
				return -1;
			}
			return 0;
		}
		
        /// <summary>
		/// ������Ա��
		/// </summary>
		/// <returns></returns>
		public int UpdatePersonGroup(FS.HISFC.Models.Base.Employee Person)
		{
			string strSql="";
			if(this.GetSQL("Manager.UserManager.PersonGroup.Update.1",ref strSql)==-1) return -1;
			try
			{
				string[] s = new string[8];
				s[0] = "";
				s[1] = "";
				s[2] =  Person.ID;
				s[3] =  Person.Name;
				s[4] =  Person.Password;
				s[5] =  Person.User01;
				s[6] =  FS.FrameWork.Function.NConvert.ToInt32(Person.IsManager).ToString();
				s[7] =  this.Operator.ID;
				strSql=string.Format(strSql,s);
			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
            //if(this.ExecNoQuery(strSql)<=0)//insert 
            //{
            //    return -1;
            //}
            //return 0;
            return this.ExecNoQuery(strSql);
		}
		
        /// <summary>
		/// ɾ����
		/// </summary>
		/// <param name="Person"></param>
		/// <param name="Group"></param>
		/// <returns></returns>
		public int DeletePersonGroup(FS.HISFC.Models.Base.Employee Person,FS.FrameWork.Models.NeuObject Group)
		{
			string strSql="";
			if(this.GetSQL("Manager.UserManager.PersonGroup.Delete.1",ref strSql)==-1) return -1;
			try
			{
				string[] s = new string[8];
				s[0] = Group.ID;
				s[1] = Group.Name;
				s[2] =  Person.ID;
				s[3] =  Person.Name;
				s[4] =  Person.Password;
				s[5] =  Person.User01;
				s[6] =  FS.FrameWork.Function.NConvert.ToInt32(Person.IsManager).ToString();
				s[7] =  this.Operator.ID;
				strSql=string.Format(strSql,s);
			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0)//insert 
			{
				return -1;
			}
			return 0;
		}
		
        public int  IsExistLoginName(string LoginName,string OperCode)
		{
			//select * from com_roleoperator where login_name = '{0}' and PARENT_CODE ='{1}' and CURRENT_CODE='{2}'
			string strSql = "";
			if (this.GetSQL("Management.UserManager.IsExistLoginName",ref strSql)==-1)return -1;
			try
			{
				strSql= string.Format(strSql,LoginName,OperCode);

				this.ExecQuery(strSql);
				if(this.Reader.Read() )
				{
					if(Reader[0]!=DBNull.Value) 
					{
						return 1;
					}
					else 
					{
						return 0;
					}
				}
				else
				{
					return 0;
				}
                this.Reader.Close();
			}
			catch(Exception ee)
			{
				this.Err = ee.Message;
				return -1;
			}
		}


        /// <summary>
        /// ����û�ʹ�����ݿ��sid��serial
        /// </summary>
        /// <param name="machineName"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetSidAndSerial(string machineName)
        {
            string strSql = "";
            if (this.GetSQL("Manager.UserManager.GetSidAndSerial", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql";
                return null;
            }
            strSql = System.String.Format(strSql, machineName);
            FS.FrameWork.Models.NeuObject obj = new NeuObject();
            if (this.ExecQuery(strSql) == -1)
                return null;
            while (this.Reader.Read())
            {
                obj.ID = this.Reader[0].ToString();
                obj.Name = this.Reader[1].ToString();
            }
            return obj;
        }

        /// <summary>
        ///  ����ϵͳ��¼����ID
        /// 
        /// {DEA84BD8-882A-440c-AF5B-3C244D16211D}
        /// </summary>
        /// <returns>��ȡϵͳ��¼����ID</returns>
        public string GetLoginSessionID()
        {
            string strSql = "";
            string sessionID = "";
            if (this.GetSQL("Manager.UserManager.GetLoginSessionID", ref strSql) == -1)
            {
                return null;
            }

            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            try
            {
                if (this.Reader.Read())
                {
                    sessionID = this.Reader[0].ToString();
                }
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return sessionID;
        }

        /// <summary>
        /// �����½�߲������ݿ���Ϣ
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertLogonInfo(FS.HISFC.Models.Base.Employee obj)
        {
            string strSql = "";
            FS.FrameWork.Models.NeuObject obj1 = this.GetSidAndSerial(obj.User02);
            if (this.GetSQL("Manager.UserManager.InsertLogonInfo", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql";
                return -1;
            }
            strSql = System.String.Format(strSql, obj.ID,//���
                obj.Name,//����
                obj.Memo,//���
                obj.User01,//����
                obj.User02,//������
                obj.User03,//����IP
                obj1.ID,//sid
                obj1.Name,//serial
                obj.CurrentGroup.ID,
                obj.CurrentGroup.Name,
                this.GetSysDateTime());//�������
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �����û���¼��־
        /// 
        /// {DEA84BD8-882A-440c-AF5B-3C244D16211D}
        /// </summary>
        /// <param name="loginUser">��¼�û�</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int InsertLoginLog(FS.HISFC.Models.Base.Employee loginUser,string loginSessionID,string ip,string hosName)
        {
            string strSql = "";
            FS.FrameWork.Models.NeuObject loginSID = this.GetSidAndSerial(hosName);

            //�����¼�û�Ȩ�޲��㹻ʱ���޷���ȡsid��Serial���򲻱���
            //{57751514-9E4E-46d6-BABE-CA099747D1DC} BY Maokb
            if (loginSID == null)
            {
                loginSID = new NeuObject();
            }

            if (this.GetSQL("Manager.UserManager.InsertLoginLog", ref strSql) == -1)
            {
                return -1;
            }

            DateTime sysTime = this.GetDateTimeFromSysDateTime();

            strSql = System.String.Format(strSql, loginSessionID,loginUser.ID,loginUser.Name,
                                                  sysTime,loginUser.Dept.ID,loginUser.Dept.Name,    //������Ϣ
                                                  loginUser.CurrentGroup.Name,  //��¼������
                                                  loginSID.ID,loginSID.Name,    //SID/Serial#
                                                  hosName,ip);

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// �����û�ע����־
        /// 
        /// {DEA84BD8-882A-440c-AF5B-3C244D16211D}
        /// </summary>
        /// <param name="loginSessionID">��¼�û�Session</param>
        /// <returns>�ɹ�����1 ʧ�ܷ���-1</returns>
        public int UpdateLogoutLog(string loginSessionID)
        {
            string strSql = "";

            if (this.GetSQL("Manager.UserManager.UpdateLogoutLog", ref strSql) == -1)
            {
                return -1;
            }

            DateTime sysTime = this.GetDateTimeFromSysDateTime();

            strSql = System.String.Format(strSql, loginSessionID, sysTime);

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ��ѯ�ϴε�¼��Ϣ
        /// {9DF35C63-1468-4fa5-BBEA-5D00197C0994} yangw 20100831
        /// </summary>
        /// <param name="emplCode">Ա������</param>
        /// <param name="dayRange">ʱ�䷶Χ�������ڣ�</param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject GetLastLoginInfo(string emplCode, int dayRange)
        {
            string sql = "";
            if (this.GetSQL("Manager.UserManager.GetLastLoginInfo", ref sql) == -1) return null;

            try
            {
                sql = string.Format(sql, emplCode, dayRange);
            }
            catch
            {
                this.Err = "Manager.UserManager.GetLastLoginInfo��ֵ����";
                return null;
            }
            FS.FrameWork.Models.NeuObject obj = null;
            if (this.ExecQuery(sql) == -1) return null;

            while (this.Reader.Read())
            {
                obj = new FS.FrameWork.Models.NeuObject();

                obj.ID = this.Reader[0].ToString();          //��¼���ұ���
                obj.Name = this.Reader[1].ToString();        //��¼����
                obj.Memo = this.Reader[2].ToString();        //��¼ʱ��
                obj.User01 = this.Reader[3].ToString();      //��¼IP
            }
            this.Reader.Close();
            return obj;
        }

        /// <summary>
        /// ����Ա����Ż�ȡ��¼��
        /// </summary>
        /// <param name="personId"></param>
        /// <returns></returns>
        public string GetLoginAccountByPersonId(string emplCode)
        {
            string sql = "";
            string account = string.Empty;
            if (this.GetSQL("Manager.UserManager.GetAccountByPersonId", ref sql) == -1) return null;

            try
            {
                sql = string.Format(sql, emplCode);
            }
            catch
            {
                this.Err = "Manager.UserManager.GetAccountByPersonId��ֵ����";
                return account;
            }
            if (this.ExecQuery(sql) == -1) return null;

            while (this.Reader.Read())
            {
                account = this.Reader[0].ToString(); 
            }
            this.Reader.Close();
            return account;
        }
	}
}