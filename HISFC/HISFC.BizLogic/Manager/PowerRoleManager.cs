using System;
using System.Collections;


using FS.FrameWork.Models;
using FS.HISFC.Models;

using FS.HISFC.Models.Admin;

namespace FS.HISFC.BizLogic.Manager
{

	/// <summary>
	///	 ��ɫ����
	/// </summary>
	public class PowerRoleManager : DataBase
	{

		private Hashtable entityCache = new Hashtable();

		/// <summary>
		/// 
		/// </summary>
		public PowerRoleManager()
		{

		}
		
		//������������Role���ӳټ���PowerRoleDetailsΪTrue.
		public PowerRole Load(string primaryKey)
		{
			return Load(primaryKey,true);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="primaryKey"></param>
		/// <param name="lazy"></param>
		/// <returns></returns>										z
		public PowerRole Load(string primaryKey,bool lazy)
		{
			string sqlstring = PrepareSQL("Manager.PowerRoleManager.LoadByPrimaryKey",new string[]{primaryKey});

			if(sqlstring == string.Empty)
				return null;

			try
			{

				this.ExecQuery(sqlstring);
				if(this.Reader.Read())
				{
					PowerRole role = new PowerRole();
					PrepareData(role);
					if(!lazy)
					{
						SelectPowerRoleDetail(role);
					}
					return role;

				}

			}   
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;  			
				 
			}

			return null;

		}
	
		
		public ArrayList LoadByRoleType(string roleTypeCode)
		{
			string sqlstring = PrepareSQL("Manager.PowerRoleManager.LoadByRoleType",new string[]{roleTypeCode});
		    
			if(sqlstring == string.Empty)
				return null;

			//RoleTypeManager typeMgr = new RoleTypeManager();
			

			ArrayList roles = new ArrayList();    
			try
			{
				this.ExecQuery(sqlstring);
				while(this.Reader.Read())
				{
					PowerRole role = new PowerRole();
					//���и�ֵ
					PrepareData(role);
					//RoleType roleType =	 typeMgr.LoadByPrimaryKey(role.RoleType);
					//if(roleType != null)
					//	role.Memo = roleType.TypeName;
					roles.Add(role);
				}  				 

			}   
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;  			
				 
				roles.Clear();
			}            

			 
			return roles;
			
		}



		/// <summary>
		///	 ��������Role����Ϣ����������ϸ��Ϣ��
		/// </summary>
		/// <returns></returns>
		public ArrayList LoadAll()
		{
 
			string sqlstring = PrepareSQL("Manager.PowerRoleManager.LoadAll",null);
		    
			if(sqlstring == string.Empty)
				return null;

			RoleTypeManager typeMgr = new RoleTypeManager();
			

			ArrayList roles = new ArrayList();    
			try
			{
				this.ExecQuery(sqlstring);
				while(this.Reader.Read())
				{
					PowerRole role = new PowerRole();
					//���и�ֵ
					PrepareData(role);
					RoleType roleType =	 typeMgr.LoadByPrimaryKey(role.RoleType);
					if(roleType != null)
					role.Memo = roleType.TypeName;
					roles.Add(role);
				}  				 

			}   
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;  			
				 
				roles.Clear();
			}            

			 
			return roles;
		}	
			

		/// <summary>
		/// ����Role����ϸ�����role.RolePowerDetailsΪnull,����أ����򲻼���
		/// </summary>
		/// <param name="role"></param>
		public void SelectPowerRoleDetail(PowerRole role)
		{
			 SelectPowerRoleDetail(role,false);			 
		}

		public void SelectPowerRoleDetail(PowerRole role,bool must)
		{
			 
			if(role != null)
			{
				if(must)
				{
					role.RolePowerDetails = SelectPowerRoleDetail(role.RoleCode); 				
				}
				else
					if(role.RolePowerDetails != null)
						role.RolePowerDetails = SelectPowerRoleDetail(role.RoleCode);

			}
			 
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ArrayList SelectPowerRoleDetail(string roleCode)
		{   	                                         
			RolePowerDetails detailManager = new RolePowerDetails(); 
			return detailManager.SelectDetails(roleCode);			 
		}
		
		 
		 
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="role"></param>
		/// <returns></returns>
		public int InsertPowerRole(PowerRole role)
		{
			string strSql = "";
			
			if (this.GetSQL("Manager.PowerRoleManager.InsertPowerRole",ref strSql)==-1) return -1;
			try
			{
			//	strSql = string.Format(strSql,info.RoleCode, info.RoleName, info.RoleMeanint, info.Class2Code, info.Class2Name, info.Class3Code, info.Class3Name, info.Mark, this.Operator.ID,this.GetSysDateTime());
				strSql = string.Format(strSql,role.RoleName,role.RoleMeanint,role.RoleType,this.Operator.ID,this.Operator.Name,role.Mark);
                			
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return -1;
			}
			return this.ExecNoQuery(strSql);
		}
		        		
		


		public int DeletePowerRole(PowerRole info)
		{
			if(info == null)
				return -1;
			return DeletePowerRole(info.RoleCode);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="roleCode"></param>
		/// <returns></returns>
		public int DeletePowerRole(string roleCode)
		{
			string strSql = "";
			if (this.GetSQL("Manager.PowerRoleManager.DeletePowerRole",ref strSql)==-1) return -1;
				
			try
			{   				
				//strSql = string.Format(strSql,info.RoleCode, info.Class2Code, info.Class3Code);
				 strSql = string.Format(strSql,roleCode);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try
			{
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
		}
		



		private void PrepareData(PowerRole role)
		{
			try
			{
				role.RoleCode = this.Reader[0].ToString();
				role.RoleName = this.Reader[1].ToString();
				role.RoleMeanint = this.Reader[2].ToString();
				role.RoleType = this.Reader[3].ToString();
				role.Mark = this.Reader[4].ToString();
				role.ID = role.RoleCode;
				role.Name = role.RoleName ;
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;  			
				throw ex; 
			}

		}


		private string PrepareSQL(string sqlName,params string[] values)
		{
			string strSql = string.Empty;
			if (this.GetSQL(sqlName,ref  strSql) == 0 )
			{
				try
				{
					if(values != null)
						strSql= string.Format(strSql,values);
				}
				catch(Exception ex)
				{
					this.Err=ex.Message;
					this.ErrCode=ex.Message;
					strSql = string.Empty;
				}
			}
			return strSql;
		}


	}

	
	/// <summary>
	/// ��ɫ--Ȩ�� ��ϸ����
	/// </summary>
	public  class RolePowerDetails : DataBase
	{

		/// <summary>
		/// 
		/// </summary>
		public RolePowerDetails()
		{
		}
			
		public ArrayList SelectDetails(string roleCode)
		{
			string sql = PrepareSQL("Manager.RolePowerDetails.LoadDetailByRoleCode",new string[]{roleCode});
			if(sql == string.Empty)
			{
				return null;			
			}
			ArrayList details = new ArrayList();

			try
			{
				this.ExecQuery(sql);

				while(this.Reader.Read())
				{
					RolePowerDetail detail = new RolePowerDetail();
					detail.PkID = this.Reader[0].ToString();
					detail.RoleCode = this.Reader[1].ToString();
					detail.Class2Code = this.Reader[2].ToString();
					detail.Class2Name = this.Reader[3].ToString();
					detail.Class3Code = this.Reader[4].ToString();
					detail.Class3Name = this.Reader[5].ToString();
					detail.Mark = this.Reader[6].ToString();
					detail.ID = detail.PkID;

					details.Add(detail);
				}

			} 			
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;  			
				details.Clear();
			}
			return details;
			
		}
			
        
		private string PrepareSQL(string sqlName,params string[] values)
		{
			string strSql = string.Empty;
			if (this.GetSQL(sqlName,ref  strSql) == 0 )
			{
				try
				{
					if(values != null)
						strSql= string.Format(strSql,values);
				}
				catch(Exception ex)
				{
					this.Err=ex.Message;
					this.ErrCode=ex.Message;
					strSql = string.Empty;
				}
			}
			return strSql;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int InsertRolePowerDetail(RolePowerDetail info)
		{
			string strSql = "";
			
			if (this.GetSQL("Manager.RolePowerDetails.InsertRolePowerDetail",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,info.RoleCode, info.Class2Code, info.Class2Name, info.Class3Code, info.Class3Name, this.Operator.ID , this.Operator.Name, info.Mark);
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
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int DeleteRolePowerDetail(RolePowerDetail info)
		{
			 return DeleteRolePowerDetail(info.PkID);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="detailCode"></param>
		/// <returns></returns>
		public int DeleteRolePowerDetail(string detailCode)
		{
			string strSql = "";
			if (this.GetSQL("Manager.RolePowerDetails.DeleteRolePowerDetail",ref strSql)==-1) return -1;
				
			try
			{   				
				strSql = string.Format(strSql,detailCode);

			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try
			{
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
		}


		public int DeleteByRoleCode(string roleCode)
		{
			string strSql = "";
			if (this.GetSQL("Manager.RolePowerDetails.DeleteRolePowerDetail",ref strSql)==-1) return -1;
				
			try
			{   				
				strSql = string.Format(strSql,roleCode);

			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}      			

			try
			{
				return this.ExecNoQuery(strSql);
			}
			catch(Exception ex)
			{
				this.ErrCode=ex.Message;
				this.Err=ex.Message;
				return -1;
			}
		}
		
	}

	
}