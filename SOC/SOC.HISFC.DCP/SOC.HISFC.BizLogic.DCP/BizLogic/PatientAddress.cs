using System;
using System.Collections;
using System.Data;

namespace FS.SOC.HISFC.BizLogic.DCP
{
	/// <summary>
	/// PatientAddress 的摘要说明。
	/// </summary>
    public class PatientAddress : FS.SOC.HISFC.BizLogic.DCP.BizLogic.DataBase 
	{
		public PatientAddress()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		/// <summary>
		/// 初始化省
		/// </summary>
		/// <returns></returns>
		public ArrayList InitProvince(string lever)
		{
			string sql="";
            if (this.GetSQL("DCP.PatientAddress.Init", ref sql) == -1) return null;
			sql = string.Format(sql, lever);
			ArrayList al=new ArrayList ();
			FS.HISFC.DCP.Object.PatientAddress  obj=null;


			if(this.ExecQuery(sql) == -1) return null;
 
			try
			{
				while (this.Reader.Read()) 
				{
					try
					{
						obj=new  FS.HISFC.DCP.Object.PatientAddress();
                        if (!this.Reader.IsDBNull(0)) obj.ID = this.Reader[0].ToString();// 地址编码
						if(!this.Reader.IsDBNull(0)) obj.Addr_Code = this.Reader[0].ToString();// 地址编码
                        if (!this.Reader.IsDBNull(1)) obj.Name = this.Reader[1].ToString();// 地址名称
						if(!this.Reader.IsDBNull(1)) obj.Addr_Name  = this.Reader[1].ToString();// 地址名称
						if(!this.Reader.IsDBNull(2)) obj.Senior_Address =this.Reader[2].ToString();//上级编码
						if(!this.Reader.IsDBNull(3)) obj.Lever =this.Reader[3].ToString();//级别
						if(!this.Reader.IsDBNull(4)) obj.SpellCode =this.Reader[4].ToString();//  拼音码						
						if(!this.Reader.IsDBNull(5)) obj.WBCode =this.Reader[5].ToString();//五笔码	
					}
					catch(Exception ex) 
					{ 
						this.Err=ex.Message;
						this.WriteErr();
						if(!Reader.IsClosed)
						{
							Reader.Close();
						}
						return null;
					}
					this.ProgressBarValue ++;
					al.Add (obj);

				}

			}
			catch(Exception ex) 
			{
				this.Err="获得Com_Address信息出错！"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				if(!Reader.IsClosed)
				{
					Reader.Close();
				}
				return al;
			}
			this.Reader.Close();
			
			this.ProgressBarValue=-1;
			return al;

		}

		/// <summary>
		/// 初始化市、区、镇
		/// </summary>
		/// <param name="lever"></param>
		/// <param name="senior_address"></param>
		/// <returns></returns>
		public ArrayList InitCityTownZhen(string lever,string senior_address)
		{
			
			string strselect="";
            if (this.GetSQL("DCP.PatientAddress.Init", ref strselect) == -1) return null;

			strselect = string.Format(strselect,lever);
			string strwhere="";
			if(senior_address!="")
			{
				
				if(senior_address !="")
				{
                    if (this.GetSQL("DCP.PatientAddress.Init.Where", ref strwhere) == -1) return null;
					strwhere = string.Format(strwhere, senior_address);
				}
			}
			string sql=strselect+strwhere;
			ArrayList al=new ArrayList ();
			FS.HISFC.DCP.Object.PatientAddress  obj=null;
			if(this.ExecQuery(sql) == -1) return null;
			try
			{
				while (this.Reader.Read()) 
				{
					try
					{
						obj=new  FS.HISFC.DCP.Object.PatientAddress();
                        if (!this.Reader.IsDBNull(0)) obj.ID = this.Reader[0].ToString();// 地址编码
                        if (!this.Reader.IsDBNull(0)) obj.Addr_Code = this.Reader[0].ToString();// 地址编码
                        if (!this.Reader.IsDBNull(1)) obj.Name = this.Reader[1].ToString();// 地址名称
                        if (!this.Reader.IsDBNull(1)) obj.Addr_Name = this.Reader[1].ToString();// 地址名称
                        if (!this.Reader.IsDBNull(2)) obj.Senior_Address = this.Reader[2].ToString();//上级编码
                        if (!this.Reader.IsDBNull(3)) obj.Lever = this.Reader[3].ToString();//级别
                        if (!this.Reader.IsDBNull(4)) obj.SpellCode = this.Reader[4].ToString();//  拼音码						
                        if (!this.Reader.IsDBNull(5)) obj.WBCode = this.Reader[5].ToString();//五笔码	
					}
					catch(Exception ex) 
					{ 
						this.Err=ex.Message;
						this.WriteErr();
						if(!Reader.IsClosed)
						{
							Reader.Close();
						}
						return null;
					}
					this.ProgressBarValue ++;
					al.Add (obj);
				}


			}
			catch(Exception ex) 
			{
				this.Err="获得省信息出错！"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				if(!Reader.IsClosed)
				{
					Reader.Close();
				}
				
				return al;
			}
			this.Reader.Close();
			
			this.ProgressBarValue=-1;
			return al;
 
		}

		/// <summary>
		/// 设置省、市、区、镇
		/// </summary>
		/// <param name="strAddrcode"></param>
        public FS.HISFC.DCP.Object.PatientAddress SetProCityTownZhen(string strAddrcode)
		{
			string sql="";
            if (this.GetSQL("DCP.PatientAddress.Set", ref sql) == -1) return null;
			sql = string.Format(sql, strAddrcode);
            FS.HISFC.DCP.Object.PatientAddress obj = null;
			if(this.ExecQuery(sql)==-1)return null;
			try
			{
				while(this.Reader.Read())
				{
					try
					{
                        obj = new FS.HISFC.DCP.Object.PatientAddress();
						if(!this.Reader.IsDBNull(0)) obj.Addr_Code = strAddrcode;// 地址编码
                        if (!this.Reader.IsDBNull(0)) obj.ID = strAddrcode;// 地址编码
						if(!this.Reader.IsDBNull(1)) obj.Addr_Name  = this.Reader["NAME"].ToString();// 地址名称
                        if (!this.Reader.IsDBNull(1)) obj.Name = this.Reader["NAME"].ToString();// 地址名称
						if(!this.Reader.IsDBNull(2)) obj.Senior_Address =this.Reader["SENIOR_ADDRESS"].ToString();//上级编码
						if(!this.Reader.IsDBNull(3)) obj.Lever =this.Reader["LEVER"].ToString();//级别
						if(!this.Reader.IsDBNull(4)) obj.SpellCode =this.Reader["PINYINGCODE"].ToString();//  拼音码						
						if(!this.Reader.IsDBNull(5)) obj.WBCode =this.Reader["WBCODE"].ToString();//五笔码	
					}
					catch(Exception ex) 
					{ 
						this.Err=ex.Message;
						this.WriteErr();
						if(!Reader.IsClosed)
						{
							Reader.Close();
						}
						return null;
					}

				}
			}
			catch(Exception ex) 
			{
				this.Err="获得Com_Address信息出错！"+ex.Message;
				this.ErrCode="-1";
				this.WriteErr();
				if(!Reader.IsClosed)
				{
					Reader.Close();
				}
				return null;
			}
			this.Reader.Close();
			return obj;

		}
		/// <summary>
		/// 根据门诊号查询地址编码
		/// </summary>
		/// <param name="cardno"></param>
		/// <returns></returns>
		public static string SelectAddrCode( string cardno)

		{	
			string sql="";
			FS.HISFC.BizLogic.Manager.Constant Constant=new  FS.HISFC.BizLogic.Manager.Constant();
            if (Constant.Sql.GetSql("DCP.PatientAddress.SelectAddrCode", ref sql) == -1) return null;
			sql = string.Format(sql, cardno);	
			return Constant.ExecSqlReturnOne(sql,"");
	
		}
		/// <summary>
		/// 根据地址编号获取地址名称
		/// </summary>
		/// <param name="strAddressCode"></param>
		/// <returns></returns>
		public static string GetAddressName(string strAddressCode)
		{

			if(strAddressCode.Length ==0) return null;
            FS.HISFC.BizLogic.Manager.Constant Constant = new FS.HISFC.BizLogic.Manager.Constant();
			string strHome="";
			string sql="";
            if (Constant.Sql.GetSql("DCP.PatientAddress.GetAddressName", ref sql) == -1)
			{
				return null ;
			}
			sql = string.Format(sql, strAddressCode);
			DataSet ds=new DataSet ();
			int result=Constant.ExecQuery (sql,ref ds);
			if(result==-1) return null;
			if(ds.Tables [0].Rows .Count >0)
			{
				strHome =ds.Tables [0].Rows [0][0].ToString ();
			}
			return strHome;

		}
	
	}	
}
