using System;
using System.Collections;

namespace neusoft.HISFC.Management.Manager
{
	/// <summary>
	/// Control ��ժҪ˵����
	/// </summary>
	public class Control:neusoft.neuFC.Management.Database
	{
		public Control()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��ӿ�����Ϣ
		/// </summary>
		/// <param name="Control"></param>
		/// <returns></returns>
		public int AddControlInfo(neusoft.HISFC.Object.Base.Control Control)
		{
			string strSql = "";
			if (this.Sql.GetSql("AddControlInfo.1",ref strSql)==-1)return -1;
			try
			{
				//0���Ʋ�������1���Ʋ�������2���Ʋ���ֵ3��ʾ���4����Ա5����ʱ��
				strSql = string.Format(strSql,Control.ID,Control.Name,Control.ControlValue,Control.VisibleFlag,
					this.Operator.ID,this.GetSysDateTime());
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
		/// ���¿�����Ϣ
		/// </summary>
		/// <param name="Control"></param>
		/// <returns></returns>
		public int UpdateControlInfo(neusoft.HISFC.Object.Base.Control Control)
		{
			string strSql = "";
			if (this.Sql.GetSql("UpdateControlInfo.1",ref strSql)==-1)return -1;
			try
			{
				//0���Ʋ�������1���Ʋ�������2���Ʋ���ֵ3��ʾ���4����Ա5����ʱ��
				strSql = string.Format(strSql,Control.ID,Control.Name,Control.ControlValue,Control.VisibleFlag,
					this.Operator.ID,this.GetSysDateTime());
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
		/// ����������Ϣ ֻ��ʾ�ÿͻ����Կ�������Ϣ
		/// </summary>
		/// <returns></returns>
		public ArrayList QueryControlInfo()
		{
			string strSql = "";
			ArrayList al = new ArrayList();
			if (this.Sql.GetSql("QueryControlInfo.1",ref strSql)==-1)return null;
			this.ExecQuery(strSql);
			//0���Ʋ�������1���Ʋ�������2���Ʋ���ֵ3��ʾ���
			while (this.Reader.Read())
			{
				neusoft.HISFC.Object.Base.Control Control = new neusoft.HISFC.Object.Base.Control();
				try
				{
					Control.ID = this.Reader[0].ToString();
					Control.Name= this.Reader[1].ToString();
					Control.ControlValue=this.Reader[3].ToString();
					Control.VisibleFlag=this.Reader[4].ToString();
				}
				catch(Exception ex)
				{
					this.Err="��ѯ������Ϣ��ֵ����!"+ex.Message;
					this.ErrCode=ex.Message;
					return null;
				}
				
				al.Add(Control);

			}
			this.Reader.Close();

			return al;
		}
		/// <summary>
		/// ���ݿ������������������͵�ֵ
		/// </summary>
		/// <param name="ControlCode"></param>
		/// <returns></returns>
		public string QueryControlInfo(string ControlCode)
		{
			string strSql = "";
			if (this.Sql.GetSql("QueryControlInfo.2",ref strSql)==-1)return "";
			try
			{
				//0���Ʋ�������
				strSql = string.Format(strSql,ControlCode);
			}
			catch(Exception ex)
			{
				this.Err=ex.Message;
				this.ErrCode=ex.Message;
				return "";
			}
			return this.ExecSqlReturnOne(strSql);
		}
		/// <summary>
		/// ɾ��������Ϣ ��ʱû��
		/// </summary>
		/// <param name="Control"></param>
		/// <returns></returns>
		public int DeleteControlInfo(neusoft.HISFC.Object.Base.Control Control)
		{
			return 0;
		}
	}
}
