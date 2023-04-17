using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Registration
{
	/// <summary>
	/// 挂号级别管理类
	/// </summary>
	public class RegLevel:FS.FrameWork.Management.Database
	{
		/// <summary>
		/// 挂号级别管理类
		/// </summary>
		public RegLevel()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 定义
		/// <summary>
		/// 挂号级别实体
		/// </summary>
		protected FS.HISFC.Models.Registration.RegLevel regLvl = null;

		/// <summary>
		///ArrayList
		/// </summary>
		protected ArrayList al = null;
		#endregion

		#region 增加
		/// <summary>
		/// 插入一条挂号级别
		/// </summary>
		/// <param name="regLevel"></param>
		/// <returns></returns>
		public int Insert(FS.HISFC.Models.Registration.RegLevel regLevel)
		{
			string sql="";

			if(this.Sql.GetCommonSql("Registration.RegLevel.Insert",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,regLevel.ID,regLevel.Name,regLevel.UserCode,regLevel.SortID.ToString(),
					FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsValid).ToString(),
					FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsExpert).ToString(),
					FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsFaculty).ToString(),
					FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsDefault).ToString(),regLevel.Oper.ID,
					regLevel.Oper.OperTime.ToString(),FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsSpecial)
                    /*{156C449B-60A9-4536-B4FB-D00BC6F476A1}*/, FS.FrameWork.Function.NConvert.ToInt32(regLevel.IsEmergency));
			}
			catch(Exception e)
			{
				this.Err = "[Registration.RegLevel.Insert]格式不匹配!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}
		#endregion

		#region 删除
		/// <summary>
		/// 根据挂号级别代码删除一条挂号级别
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public int Delete(string ID)
		{
			string sql="";
			
			if(this.Sql.GetCommonSql("Registration.RegLevel.Delete",ref sql) == -1) return -1;

			try
			{
				sql=string.Format(sql,ID);
			}
			catch(Exception e)
			{
				this.Err = "[Registration.RegLevel.Delete]格式不匹配!" + e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}
		#endregion

		#region 查询
		/// <summary>
		/// 查询全部挂号级别
		/// </summary>
		/// <returns></returns>
		public ArrayList Query()
		{
			string sql="";

			if(this.Sql.GetCommonSql("Registration.RegLevel.Query.1",ref sql)==-1) return null;
			
			return this.queryBase(sql);
		}
		/// <summary>
		/// 按是否有效查询挂号级别
		/// </summary>
		/// <param name="isValid"></param>
		/// <returns></returns>
		public ArrayList Query(bool isValid)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.RegLevel.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.RegLevel.Query.3",ref where) == -1)return null;

			sql = sql + " " +where;
			try
			{
				sql = string.Format(sql,FS.FrameWork.Function.NConvert.ToInt32(isValid).ToString());
			}
			catch(Exception e)
			{
				this.Err = "[Registration.RegLevel.Query3]格式不匹配!" + e.Message;
				this.ErrCode = e.Message;
				return null;
			}

			return this.queryBase(sql);
		}
		/// <summary>
		/// 根据代码检索一条挂号级别
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Registration.RegLevel Query(string ID)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.RegLevel.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.RegLevel.Query.4",ref where) == -1)return null;

			sql = sql + " " +where;
			try
			{
				sql = string.Format(sql,ID);
			}
			catch(Exception e)
			{
				this.Err = "[Registration.RegLevel.Query4]格式不匹配!" + e.Message;
				this.ErrCode = e.Message;
				return null;
			}

			if(this.queryBase(sql) == null)return null ;

			if(al == null)
			{
				return null;
			}
			else if(al.Count == 0)
			{
				return new FS.HISFC.Models.Registration.RegLevel () ;
			}
			else
			{
				return (FS.HISFC.Models.Registration.RegLevel)al[0];
			}
		}
		/// <summary>
		/// 根据sql查询挂号级别信息
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private ArrayList queryBase(string sql)
		{
			if(this.ExecQuery(sql) == -1) return null;
			try
			{
				this.al = new ArrayList();

				while(this.Reader.Read())
				{
					this.regLvl = new FS.HISFC.Models.Registration.RegLevel();
					//序号
					this.regLvl.ID = this.Reader[2].ToString();
					//名称
					this.regLvl.Name = this.Reader[3].ToString();
					//助记码
					this.regLvl.UserCode = this.Reader[4].ToString();
					//显示顺序
					this.regLvl.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());
					//是否有效
					this.regLvl.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[6].ToString());
					//是否专家号
					this.regLvl.IsExpert = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[7].ToString());
					//是否专科号
					this.regLvl.IsFaculty = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[8].ToString());
					//是否特诊号
					this.regLvl.IsSpecial = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[9].ToString());
					//是否默认
					this.regLvl.IsDefault = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[10].ToString());
					//操作员
					this.regLvl.Oper.ID = this.Reader[11].ToString();
					//操作时间
					this.regLvl.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12].ToString());
                    //{156C449B-60A9-4536-B4FB-D00BC6F476A1}
                    this.regLvl.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[13].ToString());

					this.al.Add(this.regLvl);
				}
				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err = "查询挂号级别出错!" + e.Message;
				this.ErrCode = e.Message;
				return null;
			}

			return al;
		}
		#endregion
	}
}
