using System;
using System.Collections;
using System.Data;

namespace FS.SOC.HISFC.BizLogic.DCP
{
	/// <summary>
	/// PatientAddress ��ժҪ˵����
	/// </summary>
    public class PatientAddress : FS.SOC.HISFC.BizLogic.DCP.BizLogic.DataBase 
	{
		public PatientAddress()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��ʼ��ʡ
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
                        if (!this.Reader.IsDBNull(0)) obj.ID = this.Reader[0].ToString();// ��ַ����
						if(!this.Reader.IsDBNull(0)) obj.Addr_Code = this.Reader[0].ToString();// ��ַ����
                        if (!this.Reader.IsDBNull(1)) obj.Name = this.Reader[1].ToString();// ��ַ����
						if(!this.Reader.IsDBNull(1)) obj.Addr_Name  = this.Reader[1].ToString();// ��ַ����
						if(!this.Reader.IsDBNull(2)) obj.Senior_Address =this.Reader[2].ToString();//�ϼ�����
						if(!this.Reader.IsDBNull(3)) obj.Lever =this.Reader[3].ToString();//����
						if(!this.Reader.IsDBNull(4)) obj.SpellCode =this.Reader[4].ToString();//  ƴ����						
						if(!this.Reader.IsDBNull(5)) obj.WBCode =this.Reader[5].ToString();//�����	
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
				this.Err="���Com_Address��Ϣ����"+ex.Message;
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
		/// ��ʼ���С�������
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
                        if (!this.Reader.IsDBNull(0)) obj.ID = this.Reader[0].ToString();// ��ַ����
                        if (!this.Reader.IsDBNull(0)) obj.Addr_Code = this.Reader[0].ToString();// ��ַ����
                        if (!this.Reader.IsDBNull(1)) obj.Name = this.Reader[1].ToString();// ��ַ����
                        if (!this.Reader.IsDBNull(1)) obj.Addr_Name = this.Reader[1].ToString();// ��ַ����
                        if (!this.Reader.IsDBNull(2)) obj.Senior_Address = this.Reader[2].ToString();//�ϼ�����
                        if (!this.Reader.IsDBNull(3)) obj.Lever = this.Reader[3].ToString();//����
                        if (!this.Reader.IsDBNull(4)) obj.SpellCode = this.Reader[4].ToString();//  ƴ����						
                        if (!this.Reader.IsDBNull(5)) obj.WBCode = this.Reader[5].ToString();//�����	
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
				this.Err="���ʡ��Ϣ����"+ex.Message;
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
		/// ����ʡ���С�������
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
						if(!this.Reader.IsDBNull(0)) obj.Addr_Code = strAddrcode;// ��ַ����
                        if (!this.Reader.IsDBNull(0)) obj.ID = strAddrcode;// ��ַ����
						if(!this.Reader.IsDBNull(1)) obj.Addr_Name  = this.Reader["NAME"].ToString();// ��ַ����
                        if (!this.Reader.IsDBNull(1)) obj.Name = this.Reader["NAME"].ToString();// ��ַ����
						if(!this.Reader.IsDBNull(2)) obj.Senior_Address =this.Reader["SENIOR_ADDRESS"].ToString();//�ϼ�����
						if(!this.Reader.IsDBNull(3)) obj.Lever =this.Reader["LEVER"].ToString();//����
						if(!this.Reader.IsDBNull(4)) obj.SpellCode =this.Reader["PINYINGCODE"].ToString();//  ƴ����						
						if(!this.Reader.IsDBNull(5)) obj.WBCode =this.Reader["WBCODE"].ToString();//�����	
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
				this.Err="���Com_Address��Ϣ����"+ex.Message;
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
		/// ��������Ų�ѯ��ַ����
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
		/// ���ݵ�ַ��Ż�ȡ��ַ����
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
