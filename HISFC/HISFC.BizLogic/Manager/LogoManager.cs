using System;
using System.Collections;
namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// LogoManager ��ժҪ˵����
	/// ��־������
	/// </summary>
	public class LogoManager:DataBase
	{
		public LogoManager()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��ô�����־
		/// </summary>
		/// <param name="dtBegin"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public ArrayList GetLogoError(DateTime dtBegin,DateTime dtEnd)
		{
			string sql ="";
			if(this.GetSQL("Manager.LogoManager.GetLogoError",ref sql)==-1) return null;
			
			try
			{
				sql = string.Format(sql,dtBegin.ToString(),dtEnd.ToString());
			}
			catch{
				this.Err = "Manager.LogoManager.GetLogoError��ֵ����";
				this.WriteErr();
			}
			
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al = new ArrayList();
//			if(this.Reader.HasRows==false) return al;
			while(this.Reader.Read())
			{

				FS.HISFC.Models.Base.Logo obj =new FS.HISFC.Models.Base.Logo();
				try
				{
					if(!Reader.IsDBNull(0))obj.ID = this.Reader[0].ToString();
					if(!Reader.IsDBNull(1))obj.DBCode = this.Reader[1].ToString();
					if(!Reader.IsDBNull(2))obj.SqlCode = this.Reader[2].ToString();
					if(!Reader.IsDBNull(3))obj.SqlError = this.Reader[3].ToString();
					if(!Reader.IsDBNull(4))obj.User01 = this.Reader[4].ToString();
					if(!Reader.IsDBNull(5))obj.User02 = this.Reader[5].ToString();
					if(!Reader.IsDBNull(6))obj.CodeDescription = this.Reader[6].ToString();
					if(!Reader.IsDBNull(7))obj.Modual = this.Reader[7].ToString();
					obj.DebugType = 1; //error
				}
				catch{}
				al.Add(obj);
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// �����־
		/// </summary>
		/// <param name="dtBegin"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public ArrayList GetLogoDebug(DateTime dtBegin,DateTime dtEnd)
		{
			string sql ="";
			if(this.GetSQL("Manager.LogoManager.GetLogoDebug",ref sql)==-1) return null;
			
			try
			{
				sql = string.Format(sql,dtBegin.ToString(),dtEnd.ToString());
			}
			catch
			{
				this.Err = "Manager.LogoManager.GetLogoDebug��ֵ����";
				this.WriteErr();
			}
			
			if(this.ExecQuery(sql)==-1) return null;
			ArrayList al = new ArrayList();
//			if(this.Reader.HasRows==false) return al;
			while(this.Reader.Read())
			{

				FS.FrameWork.Models.NeuObject  obj =new FS.FrameWork.Models.NeuObject();
				try
				{              
					if(!Reader.IsDBNull(0))obj.ID = this.Reader[0].ToString();
					if(!Reader.IsDBNull(1))obj.Name = this.Reader[1].ToString();
					if(!Reader.IsDBNull(2))obj.Memo = this.Reader[2].ToString();
					if(!Reader.IsDBNull(3))obj.User01 = this.Reader[3].ToString();
					if(!Reader.IsDBNull(4))obj.User02 = this.Reader[4].ToString();
					if(!Reader.IsDBNull(5))obj.User03 = this.Reader[5].ToString();
				}
				catch{}
				al.Add(obj);
			}
			this.Reader.Close();
			return al;
		}
		/// <summary>
		/// ɾ��������־
		/// </summary>
		/// <returns></returns>
		public int DeleteLogoDebug()
		{
			string sql = "";
			if(this.GetSQL("Manager.LogoManager.DeleteLogoDebug",ref sql)== -1) return -1;
			return this.ExecQuery(sql);

		}
		/// <summary>
		/// ɾ��������־
		/// </summary>
		/// <returns></returns>
		public int DeleteLogoError()
		{
			string sql = "";
			if(this.GetSQL("Manager.LogoManager.DeleteLogoError",ref sql)== -1) return -1;
			return this.ExecQuery(sql);
		}
	}
}
