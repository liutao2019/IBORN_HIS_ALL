using System;

namespace FS.HISFC.BizLogic.Manager
{
	/// <summary>
	/// QueryCondition ��ժҪ˵����
	/// ��ѯ���� ������ ��Ӧ��COM_QUERY_CONDITION
	/// </summary>
	public class QueryCondition:DataBase
	{
		public QueryCondition()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ��ò�ѯ����
		/// </summary>
		/// <returns></returns>
		public string GetQueryCondtion(string formName,bool isDefault)
		{
			string sql1 ="Manager.QueryCondition.Get.1";
			string sql2 ="Manager.QueryCondition.Get.2";
			string sql = "";
			
			if(isDefault)//Ĭ�ϵ�����
			{
				if(this.GetSQL(sql2,ref sql)==-1) return "-1";
				if(this.ExecQuery(sql,formName,"")==-1) return  "-1";
			}
			else //��������
			{
				if(this.GetSQL(sql1,ref sql)==-1) return "-1";
				if(this.ExecQuery(sql,formName,this.Operator.ID)==-1) return  "-1";
			}
			if(this.Reader.Read())
			{
				return this.Reader[0].ToString();
			}
			else
			{
				return "";
			}
			
		}

		/// <summary>
		/// ��ò�ѯ����
		/// </summary>
		/// <param name="formName"></param>
		/// <returns></returns>
		public string GetQueryCondtion(string formName)
		{
			return GetQueryCondtion(formName,false);
		}
		/// <summary>
		/// ���ò�ѯ����
		/// </summary>
		/// <param name="formName"></param>
		/// <param name="xml"></param>
		/// <param name="isDefault"></param>
		/// <returns></returns>
		public int SetQueryCondition(string formName,string xml,bool isDefault)
		{
			string s = this.GetQueryCondtion(formName,isDefault);
			if(s == "-1") return -1;
			if(s =="") //insert
			{
				return this._InsertQueryCondtion(formName,xml,isDefault);
			}
			else //update
			{
				return this._UpdateQueryCondition(formName,xml,isDefault);
			}
		}
		/// <summary>
		/// ���ò�ѯ����
		/// </summary>
		/// <param name="formName"></param>
		/// <returns></returns>
		public int SetQueryCondition(string formName,string xml)
		{
			return SetQueryCondition(formName,xml,false);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formName"></param>
		/// <param name="xml"></param>
		/// <param name="isDefault"></param>
		/// <returns></returns>
		protected int _InsertQueryCondtion(string formName,string xml,bool isDefault)
		{
			string sql = "Manager.QueryCondition.Insert";
			if(this.GetSQL(sql,ref sql) == -1) return -1;
			if(isDefault)
			{
				return this.ExecNoQuery(sql,formName,"",xml);
			}
			else
			{
				return this.ExecNoQuery(sql,formName,this.Operator.ID,xml);
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="formName"></param>
		/// <param name="xml"></param>
		/// <param name="isDefault"></param>
		/// <returns></returns>
		protected int _UpdateQueryCondition(string formName,string xml,bool isDefault)
		{
			string sql = "Manager.QueryCondition.Update";
			if(this.GetSQL(sql,ref sql) == -1) return -1;
			if(isDefault)
			{
				return this.ExecNoQuery(sql,formName,"",xml);
			}
			else
			{
				return this.ExecNoQuery(sql,formName,this.Operator.ID,xml);
			}
		}
	}
}
