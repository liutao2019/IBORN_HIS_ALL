using System;
using System.Collections;


using FS.FrameWork.Models;
using FS.HISFC.Models;

using FS.HISFC.Models.Admin;

namespace FS.HISFC.BizLogic.Manager
{

	/// <summary>
	/// 
	/// </summary>
	public class PowerLevelClass3ManagerImpl : DataBase
	{

		/// <summary>
		/// 
		/// </summary>
		public PowerLevelClass3ManagerImpl()
		{
		}
			
			
			
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public ArrayList SelectByPrimaryKey(string id0,string id1)
		{
			return null;
		}
		
		
		/// <summary>
		/// ��������2��3��Ȩ����Ϣ
		/// </summary>
		/// <returns></returns>
		public ArrayList SelectAll()
		{
			 ArrayList powers = new ArrayList();

			return powers;
		}
		
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int InsertPowerLevelClass3(PowerLevelClass3 info)
		{
			string strSql = "";
			
			if (this.GetSQL("Manager.PowerLevelClass3ManagerImpl.InsertPowerLevelClass3",ref strSql)==-1) return -1;
			try
			{
				strSql = string.Format(strSql,info.Class2Code, info.Class3Code, info.Class3Name, info.Class3MeaningCode, info.Class3MeaningName, info.FinFlag, info.DelFlag, info.GrantFlag, info.Class3JoinCode, info.JoinGroupCode, info.JoinGroupOrder, info.CheckFlag, info.Memo, this.Operator.ID,this.GetSysDateTime());
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
		public int UpdatePowerLevelClass3(PowerLevelClass3 info)
		{			
			string strSql = "";
			if (this.GetSQL("Manager.PowerLevelClass3ManagerImpl.UpdatePowerLevelClass3",ref strSql)==-1) return -1;
			
			try
			{   				
				strSql = string.Format(strSql,info.Class2Code, info.Class3Code, info.Class3Name, info.Class3MeaningCode, info.Class3MeaningName, info.FinFlag, info.DelFlag, info.GrantFlag, info.Class3JoinCode, info.JoinGroupCode, info.JoinGroupOrder, info.CheckFlag, info.Memo,this.Operator.ID);

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
		
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="info"></param>
		/// <returns></returns>
		public int Delete(PowerLevelClass3 info)
		{
			string strSql = "";
			if (this.GetSQL("Manager.PowerLevelClass3ManagerImpl.DeletePowerLevelClass3",ref strSql)==-1) return -1;
				
			try
			{   				
				strSql = string.Format(strSql,info.Class2Code, info.Class3Code);

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