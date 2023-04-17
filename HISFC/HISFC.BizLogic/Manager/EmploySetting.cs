using System;

namespace neusoft.HISFC.Management.Manager
{
	/// <summary>
	/// EmploySetting ��ժҪ˵����
	/// </summary>
	public class EmploySetting :neusoft.neuFC.Management.Database 
	{
		public EmploySetting()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ��ȡ
		/// </summary>
		/// <param name="EmployCode"></param>
		/// <returns></returns>
		public neusoft.HISFC.Object.Base.EmploySetting GetEmploySetting(string EmployCode) 
		{
			string strSql="";
			
			//ȡselect���
			if (this.Sql.GetSql("Manager.Person.GetEmploySetting",ref  strSql) == -1 ) 
			{
				this.Err = this.Sql.Err;
				return null;
			}
			strSql= string.Format(strSql,EmployCode);
			this.ExecQuery(strSql);
			neusoft.HISFC.Object.Base.EmploySetting emp = new neusoft.HISFC.Object.Base.EmploySetting();
			while(this.Reader.Read())
			{
				emp = new neusoft.HISFC.Object.Base.EmploySetting();
				emp.person.ID = this.Reader[0].ToString();
				emp.WaitTime = neusoft.neuFC.Function.NConvert.ToDecimal(this.Reader[1].ToString());
				emp.person.ValidState = neusoft.neuFC.Function.NConvert.ToInt32(this.Reader[2].ToString());
			}
			return emp;
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int InsertEmploySetting(neusoft.HISFC.Object.Base.EmploySetting obj)
		{
			string strSql="";
			
			//ȡselect���
			if (this.Sql.GetSql("Manager.Person.InsertEmploySetting",ref  strSql) == -1 ) 
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			strSql= string.Format(strSql,obj.person.ID,obj.WaitTime.ToString(),obj.person.ValidState.ToString());
			return  this.ExecNoQuery(strSql);
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int UpdateEmploySetting(neusoft.HISFC.Object.Base.EmploySetting obj)
		{
			string strSql="";
			
			//ȡselect���
			if (this.Sql.GetSql("Manager.Person.UpdateEmploySetting",ref  strSql) == -1 ) 
			{
				this.Err = this.Sql.Err;
				return -1;
			}
			strSql= string.Format(strSql,obj.person.ID,obj.WaitTime.ToString(),obj.person.ValidState.ToString());
			return  this.ExecNoQuery(strSql);
		}

	}
}
