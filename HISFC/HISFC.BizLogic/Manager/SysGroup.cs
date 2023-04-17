using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// SysGroup ��ժҪ˵����
	/// ϵͳ��
	/// </summary>
	public class SysGroup:DataBase,FS.HISFC.Models.Base.IManagement
	{
		public SysGroup()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		///SELECT parent_code,   --����ҽ�ƻ�������
		//       current_code,   --����ҽ�ƻ�������
		//       pargrp_code,   --����������
		//       pargrp_name,   --�����������
		//       grp_code,   --����������
		//       grp_name,   --�����������
		//       sort_id,   --˳���
		//       oper_code,   --����Ա
		//       oper_date    --����ʱ��
		//  FROM com_rolegroup   --ϵͳ���

		#region IManagement ��Ա

		public System.Collections.ArrayList GetList()
		{
			// TODO:  ��� SysGroup.GetList ʵ��
			string sql="";
			if(this.GetSQL("Manager.SysGroup.Select",ref sql)==-1) return null;
			return myList(sql);
		}

		public int Del(object obj)
		{
			// TODO:  ��� SysGroup.Del ʵ��
			string strSql="";
			if(this.GetSQL("Manager.SysGroup.Delete.1",ref strSql)==-1) return -1;
			try
			{
				string[] s = this.mySetInfo(obj);
				strSql=string.Format(strSql,s);			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0) return -1;
			return 0;
		}

		public int DeletePerson(string empcode)
		{
			// TODO:  ��� SysGroup.Del ʵ��
			string strSql="";
			if(this.GetSQL("Manager.UserManager.PersonGroup.DeletePerson",ref strSql)==-1) return -1;
			try
			{
				strSql=string.Format(strSql,empcode);			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0) return -1;
			return 0;
		}
		public int SetList(System.Collections.ArrayList al)
		{
			// TODO:  ��� SysGroup.SetList ʵ��
		

			return 0;
		}

		public FS.FrameWork.Models.NeuObject Get(object obj)
		{
			// TODO:  ��� SysGroup.Get ʵ��
			string sql="",sql1="";
			if(this.GetSQL("Manager.SysGroup.Select",ref sql)==-1) return null;
			if(this.GetSQL("Manager.SysGroup.Where.1",ref sql1)==-1) return null;
			sql = sql+""+ sql1;
			ArrayList al =myList(sql);
			if(al ==null || al.Count ==0) return null;
			return al[0] as FS.FrameWork.Models.NeuObject;
			
		}
		public int Insert(FS.FrameWork.Models.NeuObject obj)
		{
			string strSql="";
			if(this.GetSQL("Manager.SysGroup.Insert.1",ref strSql)==-1) return -1;
			try
			{
				string[] s = this.mySetInfo(obj);
				strSql=string.Format(strSql,s);			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0) return -1;
			return 0;
		}
		public int Update(FS.FrameWork.Models.NeuObject obj)
		{
			string strSql="";
			if(this.GetSQL("Manager.SysGroup.Update.1",ref strSql)==-1) return -1;
			try
			{
				string[] s = this.mySetInfo(obj);
				strSql=string.Format(strSql,s);			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0) return -1;
			return 0;
		}
		public int Set(FS.FrameWork.Models.NeuObject obj)
		{
				#region "�ӿ�"
				//�ӿ����� Manager.SysGroup.Update.1
				#endregion
				string strSql="",strSql1="";
				if(this.GetSQL("Manager.SysGroup.Insert.1",ref strSql)==-1) return -1;
    			if(this.GetSQL("Manager.SysGroup.Update.1",ref strSql1)==-1) return -1;
				try
				{
					string[] s = this.mySetInfo(obj);
					strSql=string.Format(strSql,s);
					strSql1=string.Format(strSql1,s);
			
				}
				catch(Exception ex)
				{
					this.Err="��ֵʱ�����"+ex.Message;
					this.WriteErr();
					return -1;
				}
				if(this.ExecNoQuery(strSql)<=0)//insert 
				{
					if(this.ExecNoQuery(strSql1)<=0) //update
					{
						return -1;
					}
					else
					{
						return 0;
					}
				}
				return 0;

		}
		/// <summary>
		/// ��������Ա
		/// </summary>
		/// <param name="GroupID"></param>
		/// <returns></returns>
		public ArrayList GetPeronFromGroup(string GroupID)
		{
			string sql ="";
			//�ӿ�˵�� 0 id ,1 name,2 password,3 loginname,4 ismanager
			if(this.GetSQL("Manager.SysGroup.GetPerson", ref sql)==-1) return null;
			sql = string.Format(sql,GroupID);
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al=new ArrayList();
			
			while(this.Reader.Read())
			{
				FS.HISFC.Models.Base.Employee obj=new FS.HISFC.Models.Base.Employee();
				try
				{
					obj.ID=this.Reader[0].ToString();//id
				}
				catch
				{}
				try
				{
					obj.Name =this.Reader[1].ToString();//name
				}
				catch
				{}
				try
				{
					obj.Password =this.Reader[2].ToString();//parent id
				}
				catch
				{}
				try
				{
					obj.User01 =this.Reader[3].ToString();//��½��
				}
				catch
				{}
				try
				{
					obj.IsManager =FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[4]);
				}
				catch
				{}
				try
				{
					obj.Sex.ID  = this.Reader[8].ToString();
				}
				catch
				{}
				al.Add(obj);
			}

			this.Reader.Close();
			return al;
		}
		#endregion
		#region ˽��
		/// <summary>
		/// ����б�
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private ArrayList myList(string sql)
		{
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al=new ArrayList();
			#region "�ӿ�"
			//�ӿ�����Manager.SysGroup.Select
			//<!--0 id, 1 name, 2 groupid, 3 groupname, 4 memo, 5 sortid,6 operator id,
			//	 7 name,8 operator time -->
			#endregion
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Admin.SysGroup obj = new FS.HISFC.Models.Admin.SysGroup();
					
					try
					{
						obj.ID=this.Reader[0].ToString();//id
					}
					catch
					{}
					try
					{
						obj.Name =this.Reader[1].ToString();//name
					}
					catch
					{}
					try
					{
						obj.ParentGroup.ID =this.Reader[2].ToString();//parent id
					}
					catch
					{}
					try
					{
						obj.ParentGroup.Name =this.Reader[3].ToString();//parent name
					}
					catch
					{}
					try
					{
						obj.Memo  =this.Reader[4].ToString();//��ע
					}
					catch
					{}
					
					try
					{
						obj.SortID=FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5]);//sortid
					}
					catch
					{}
					try
					{
						obj.User01 =this.Reader[6].ToString();//operator id
					}
					catch
					{}
					try
					{
						obj.User02 =this.Reader[7].ToString();//operator name
					}
					catch
					{}
					try
					{
						obj.User03 =this.Reader[8].ToString();//operator time
					}
					catch
					{}
					al.Add(obj);
				}
				this.Reader.Close();
				return al;
			}
			catch{return null;}
		}


		private string[] mySetInfo(object obj)
		{
			string[] s=new string[8];
			FS.HISFC.Models.Admin.SysGroup o = obj as FS.HISFC.Models.Admin.SysGroup;
			try
			{
				s[0]=o.ID ;//id
			}
			catch{}
			try
			{
				s[1]=o.Name ;//name
			}
			catch{}
			try
			{
				s[2]=o.ParentGroup.ID;//
			}
			catch{}
			try
			{
				s[3]=o.ParentGroup.Name;
			}
			catch{}
			try
			{
				s[4]=o.Memo;
			}
			catch{}
			try
			{
				s[5]=o.SortID.ToString();
			}
			catch{}
			try
			{
				s[6]=this.Operator.ID ;//
			}
			catch{}
			try
			{
				s[7]=this.Operator.Name;//operator naem
			}
			catch{}
//			try
//			{
//				s[8]=this.Operator.Name ;//operator name
//			}
//			catch{}
//			try
//			{
//				//s[9] = this.GetSysDate();//operator time
//			}
//			catch{}
			return s;
		}

		private string[] mySetConstInfo(string GroupID,object obj)
		{
			string[] s=new string[9];
			FS.HISFC.Models.Admin.SysModelFunction o = obj as FS.HISFC.Models.Admin.SysModelFunction;
			try
			{
				s[0]=GroupID ;//id
			}
			catch{}
			try
			{
				s[1]=o.ID ;//id
			}
			catch{}
			try
			{
				s[2]=o.Name ;//name
			}
			catch{}
			try
			{
				s[3]=o.WinName;//
			}
			catch{}
			try
			{
				s[4]=o.Memo;
			}
			catch{}
			try
			{
				s[5]=this.Operator.ID ;//
			}
			catch{}
			try
			{
				s[6]=this.Operator.Name;//operator naem
			}
			catch{}
			try
			{
				s[7]=o.Param;//operator naem
			}
			catch{}
			try{
				s[8] = o.SortID.ToString();	//˳���
			}
			catch{}
			return s;
		}
		#endregion
		#region �����
		/// <summary>
		/// �����Ա�����
		/// </summary>
		/// <param name="GroupID"></param>
		/// <returns></returns>
		public ArrayList GerDeptByPerson(string GroupID)
		{
			string sql ="";
			if(this.GetSQL("Manager.SysGroup.Dept.GetList",ref sql)==-1) return null;
			try
			{
				sql=string.Format(sql,GroupID);			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return null;
			}
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al = new ArrayList();
			try
			{
				while(this.Reader.Read())
				{
					FS.FrameWork.Models.NeuObject obj =new FS.FrameWork.Models.NeuObject();
					if(!this.Reader.IsDBNull(0))obj.ID = this.Reader[0].ToString();
					if(!this.Reader.IsDBNull(1))obj.Name = this.Reader[1].ToString();
					try
					{
						if(!this.Reader.IsDBNull(2))obj.User01 = this.Reader[2].ToString();
						if(!this.Reader.IsDBNull(3))obj.User02 = this.Reader[3].ToString();
					}
					catch{}
					al.Add(obj);
				}
				this.Reader.Close();
			}
			catch
			{
				return null;
			}	
			return al;
		}
		/// <summary>
		/// �������
		/// </summary>
		/// <param name="group"></param>
		/// <param name="dept"></param>
		/// <returns></returns>
		public int InsertDeptByPerson(FS.FrameWork.Models.NeuObject group,FS.FrameWork.Models.NeuObject dept)
		{
			string sql ="";
			if(this.GetSQL("Manager.SysGroup.Dept.Insert.1",ref sql)==-1) return -1;
			try
			{
				sql=string.Format(sql,group.ID,group.Name,dept.ID,dept.Name,this.Operator.ID);			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(sql)<=0) return -1;
			return 0;
		}
		/// <summary>
		/// ɾ������
		/// </summary>
		/// <param name="group"></param>
		/// <param name="dept"></param>
		/// <returns></returns>
		public int DeleteDeptByPerson(FS.FrameWork.Models.NeuObject group,FS.FrameWork.Models.NeuObject dept)
		{
			string sql ="";
			if(this.GetSQL("Manager.SysGroup.Dept.Delete.1",ref sql)==-1) return -1;
			try
			{
				sql=string.Format(sql,group.ID,group.Name,dept.ID,dept.Name,this.Operator.ID);			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(sql)<=0) return -1;
			return 0;
		}
		#endregion
		#region ����
		/// <summary>
		/// �������ó���-FS.HISFC.Models.Power.SysModelFunction 
		/// </summary>
		/// <param name="GroupID">��ID</param>
		/// <returns>ģ���б�FS.HISFC.Models.Power.SysModelFunction </returns>
		public ArrayList GetConstByGroup(string GroupID)
		{
			ArrayList al = new ArrayList();
			string sql = "";
			if(this.GetSQL("Manager.Group.Const.Select",ref sql) == -1) return null;
			try
			{
				sql = string.Format(sql,GroupID,FS.FrameWork.Management.Connection.Hospital.ID);
			}
			catch{this.Err ="����Manager.Group.Const.Select����";this.WriteErr();}
			if(this.ExecQuery(sql)==-1) return null;
			
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Admin.SysModelFunction obj = new FS.HISFC.Models.Admin.SysModelFunction();
					obj.ID  = this.Reader[0].ToString();
					obj.Name  = this.Reader[1].ToString();
					obj.FunName = this.Reader[1].ToString();
					obj.WinName  = this.Reader[2].ToString();
					obj.DllName   = this.Reader[3].ToString();
					obj.FormType  = this.Reader[4].ToString();
					obj.FormShowType  = this.Reader[5].ToString();
					obj.Memo   = this.Reader[6].ToString();
					obj.User01   = this.Reader[7].ToString();
					obj.User02  = this.Reader[8].ToString();
					obj.Param  = this.Reader[9].ToString();
					al.Add(obj);
				}
			
			this.Reader.Close();
			return al;
		}


		public int DelConst(string GroupID,FS.HISFC.Models.Admin.SysModelFunction obj)
		{
			// TODO:  ��� SysGroup.Del ʵ��
			string strSql="";
			if(this.GetSQL("Manager.Group.Const.Delete",ref strSql)==-1) return -1;
			try
			{
				string[] s = this.mySetConstInfo(GroupID,obj);
				strSql=string.Format(strSql,s);			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0) return -1;
			return 0;
		}

		public int InsertConst(string GroupID,FS.HISFC.Models.Admin.SysModelFunction obj)
		{
			string strSql="";
			if(this.GetSQL("Manager.Group.Const.Insert",ref strSql)==-1) return -1;
			try
			{
				string[] s = this.mySetConstInfo(GroupID,obj);
				strSql=string.Format(strSql,s);			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0) return -1;
			return 0;
		}
		public int UpdateConst(string GroupID,FS.HISFC.Models.Admin.SysModelFunction obj)
		{
			string strSql="";
			if(this.GetSQL("Manager.Group.Const.Update",ref strSql)==-1) return -1;
			try
			{
				string[] s = this.mySetConstInfo(GroupID,obj);
				strSql=string.Format(strSql,s);			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0) return -1;
			return 0;
		}
		public int SetConst(string GroupID,FS.HISFC.Models.Admin.SysModelFunction obj)
		{
			#region "�ӿ�"
			//�ӿ����� Manager.SysGroup.Update.1
			#endregion
			string strSql="",strSql1="";
			if(this.GetSQL("Manager.Group.Const.Insert",ref strSql)==-1) return -1;
			if(this.GetSQL("Manager.Group.Const.Update",ref strSql1)==-1) return -1;
			try
			{
				string[] s = this.mySetConstInfo(GroupID,obj);
				strSql=string.Format(strSql,s);
				strSql1=string.Format(strSql1,s);
			
			}
			catch(Exception ex)
			{
				this.Err="��ֵʱ�����"+ex.Message;
				this.WriteErr();
				return -1;
			}
			if(this.ExecNoQuery(strSql)<=0)//insert 
			{
				if(this.ExecNoQuery(strSql1)<=0) //update
				{
					return -1;
				}
				else
				{
					return 0;
				}
			}
			return 0;

		}
		#endregion
	}
}
