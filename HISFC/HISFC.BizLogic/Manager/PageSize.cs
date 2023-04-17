using System;
using System.Drawing;

namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// PageSize ��ժҪ˵����
	/// ��ӡֽ�Ŵ�С����
	/// </summary>
	public class PageSize:DataBase
	{
		/// <summary>
		/// ���ô�ӡֽ�Ŵ�С
		/// </summary>
		public PageSize()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		
		/// <summary>
		/// ���ֽ�Ŵ�С
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="DeptCode"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Base.PageSize GetPageSize(string ID,string DeptCode)
		{
			string sql ="Manager.PageSize.GetPageSize.1";
			if(this.GetSQL(sql,ref sql)==-1)
			{
				this.Err = "�޷��ҵ�Manager.PageSize.GetPageSize.1";
				this.WriteErr();
				return null;
			}
			if(this.ExecQuery(sql,ID,DeptCode)==-1)
			{
				return null;
			}
			FS.HISFC.Models.Base.PageSize obj = new FS.HISFC.Models.Base.PageSize();
			
			
			try
			{
				this.Reader.Read();
				obj.ID  = this.Reader[0].ToString();
				obj.Name  = this.Reader[1].ToString();
				obj.Memo  = this.Reader[2].ToString();
				obj.Dept.ID  = this.Reader[3].ToString();
				try
				{
					obj.Dept.Name = this.Reader[4].ToString();
				}
				catch{}

				obj.Width   = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5]);
				obj.Height  = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6]);

				obj.Printer = this.Reader[7].ToString();
				obj.Top = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[8]);
				obj.Left = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9]);
				obj.OperEnvironment.ID = this.Reader[10].ToString();
				obj.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11]);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.WriteErr();
				return null;
			}
			return obj;
		}
		
		/// <summary>
		/// ���ֽ�Ŵ�С
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Base.PageSize GetPageSize(string ID)
		{
			string sql ="Manager.PageSize.GetPageSize.2";
			if(this.GetSQL(sql,ref sql)==-1)
			{
				this.Err = "�޷��ҵ�Manager.PageSize.GetPageSize.2";
				this.WriteErr();
				return null;
			}
			if(this.ExecQuery(sql,ID)==-1)
			{
				return null;
			}
			FS.HISFC.Models.Base.PageSize obj = new FS.HISFC.Models.Base.PageSize();
			
//			if(this.Reader.HasRows==false) return obj;
			
			try
			{
				this.Reader.Read();
				obj.ID  = this.Reader[0].ToString();
				obj.Name  = this.Reader[1].ToString();
				obj.Memo  = this.Reader[2].ToString();
				obj.Dept.ID  = this.Reader[3].ToString();
				try
				{
					obj.Dept.Name = this.Reader[4].ToString();
				}
				catch{}
				obj.Width   = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5]);
				obj.Height  = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6]);
				obj.Printer = this.Reader[7].ToString();
				obj.Top = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[8]);
				obj.Left = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9]);
				obj.OperEnvironment.ID = this.Reader[10].ToString();
				obj.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11]);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.WriteErr();
				return null;
			}
            //70C44B29-EC2A-4bc2-BFFE-ED55DC9C7560}������ݿ��ӡ����Ϊ������ң�Ϊ�յ�����Ĭ���򲻲���
            if (!string.IsNullOrEmpty(obj.Printer))
            {
                foreach (string internetPrinterName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    if (internetPrinterName == obj.Printer)
                    {
                        return obj;
                    }
                }
                foreach (string internetPrinterName in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
                {
                    if (internetPrinterName.Contains(obj.Printer))
                    {
                        obj.Printer = internetPrinterName;
                        return obj;
                    }
                }
            }
            
			return obj;
		}
		
		/// <summary>
		///  ����б�
		/// </summary>
		/// <returns></returns>
		public System.Collections.ArrayList GetList()
		{
			string sql ="Manager.PageSize.GetPageSize.List.1";
			if(this.GetSQL(sql,ref sql)==-1)
			{
				this.Err = "�޷��ҵ�Manager.PageSize.GetPageSize.List.1";
				this.WriteErr();
				return null;
			}
			if(this.ExecQuery(sql)==-1)
			{
				return null;
			}
			
			System.Collections.ArrayList al  = new System.Collections.ArrayList();		
//			if(this.Reader.HasRows==false) return al;
			
			try
			{
				while(this.Reader.Read())
				{
					FS.HISFC.Models.Base.PageSize obj = new FS.HISFC.Models.Base.PageSize();
					obj.ID  = this.Reader[0].ToString();
					obj.Name  = this.Reader[1].ToString();
					obj.Memo  = this.Reader[2].ToString();
					obj.Dept.ID  = this.Reader[3].ToString();
					try
					{
						obj.Dept.Name = this.Reader[4].ToString();
					}
					catch{}

					obj.Width   = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5]);
					obj.Height  = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6]);

					obj.Printer = this.Reader[7].ToString();
					obj.Top = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[8]);
					obj.Left = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9]);
					obj.OperEnvironment.ID = this.Reader[10].ToString();
					obj.OperEnvironment.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11]);
					al.Add(obj);
				}
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.WriteErr();
				return null;
			}
			return al;
		}
		
		/// <summary>
		/// ����PageSize ����ID,DeptCodeΪ����
		/// </summary>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		public int SetPageSize(FS.HISFC.Models.Base.PageSize pageSize)
		{
//			FS.HISFC.Models.Base.PageSize obj =this.GetPageSize(pageSize.ID,pageSize.Dept.ID);
//			if(obj == null)
//				return -1;

			string sqlInsert = "Manager.PageSize.InsertPageSize";
			string sqlUpdate = "Manager.PageSize.UpdatePageSize";
			string sql ="";
			if(this.myGetSql(sqlUpdate,pageSize,ref sql)==-1)return -1;
			if(this.ExecNoQuery(sql)<=0)
			{
				if(this.myGetSql(sqlInsert,pageSize,ref sql)==-1)return -1;
				return this.ExecNoQuery(sql);
			}
			
			return 0;
		}
		/// <summary>
		/// ��ò���
		/// </summary>
		/// <param name="pageSize"></param>
		/// <param name="Sql"></param>
		/// <returns></returns>
		protected int myGetSql(string sqlIndex,FS.HISFC.Models.Base.PageSize pageSize,ref string Sql)
		{
			string sql1 = "";
			if(this.GetSQL(sqlIndex,ref sql1)==-1)
			{
				this.Err = sqlIndex +"û���ҵ�!";
				this.WriteErr();
				return -1;
			}
			try
			{
				Sql = string.Format(sql1,pageSize.ID,pageSize.Name,pageSize.Memo,pageSize.Height,pageSize.Width,pageSize.Dept.ID,pageSize.Printer,pageSize.Top,pageSize.Left,this.Operator.ID);
			}
			catch(Exception ex)
			{
				this.Err = ex.Message;
				this.WriteErr();
				return -1;
			}
			return 0;
		}
		/// <summary>
		/// ɾ��PageSize
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="DeptCode"></param>
		/// <returns></returns>
		public int DelPageSize(string ID,string DeptCode)
		{
			return 0;
		}
	}
}
