using System;
using System.Collections;

namespace neusoft.HISFC.Management.Registration
{
	/// <summary>
	/// �����Ű������
	/// </summary>
	public class DeptSchema:neusoft.neuFC.Management.Database
	{
		/// <summary>
		/// 
		/// </summary>
		public DeptSchema()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		
		ArrayList al=null;

		private neusoft.HISFC.Object.Registration.DeptSchema schema=null;
		/// <summary>
		/// ������ҳ�����¼
		/// </summary>
		/// <param name="schema"></param>
		/// <returns></returns>
		public int Insert(neusoft.HISFC.Object.Registration.DeptSchema schema)
		{
			string sql="";
			
			if(this.Sql.GetSql("Registration.DeptSchema.Insert",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,schema.ID,schema.SeeDate.ToString(),schema.Week,schema.NoonID,
					schema.Dept.ID,schema.Dept.Name,schema.RegLevel,schema.RegLimit,
					schema.PreRegLimit,neusoft.neuFC.Function.NConvert.ToInt32(schema.IsValid),schema.StopReason.ID,schema.StopReason.Name,
					schema.StopID,schema.StopDate.ToString(),schema.Memo,schema.OperID);
				
				return this.ExecNoQuery(sql);				
			}
			catch(Exception e)
			{
				this.Err="����ר�Ƴ�����Ϣ�����!"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}			
		}
		/// <summary>
		/// ɾ���Ű��¼
		/// </summary>
		/// <param name="schema"></param>
		/// <returns></returns>
		public int Delete(neusoft.HISFC.Object.Registration.DeptSchema schema)
		{
			string sql="";
			if(this.Sql.GetSql("Registration.DeptSchema.Delete.1",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,schema.ID);

				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="ɾ�����ҳ����Ű���Ϣʱ����!"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
		}
		/// <summary>
		/// �����Ű��¼
		/// </summary>
		/// <param name="schema"></param>
		/// <returns></returns>
		public int Update(neusoft.HISFC.Object.Registration.DeptSchema schema)
		{
			string sql="";
			if(this.Sql.GetSql("Registration.DeptSchema.Update.1",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,schema.ID,schema.RegLimit,schema.PreRegLimit,neusoft.neuFC.Function.NConvert.ToInt32(schema.IsValid),
					schema.StopReason.ID,schema.StopReason.Name,schema.StopID,schema.StopDate.ToString());

				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="���¿��ҳ����Ű���Ϣʱ����!"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
		}
		/// <summary>
		/// ��ѯһ��ʱ�䷶Χ���Ű���Ϣ
		/// </summary>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public ArrayList Query(DateTime begin,DateTime end)
		{
			string sql="",where="";

			if(this.Sql.GetSql("Registration.DeptSchema.Query.1",ref sql)==-1)return null;
			if(this.Sql.GetSql("Registration.DeptSchema.Query.2",ref where)==-1)return null;

			where=string.Format(where,begin.ToString(),end.ToString());
			sql=sql + " "+where;

			return this.QuerySchema(sql);
		}		
		/// <summary>
		/// ��sql��ѯ�Ű���Ϣ
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private ArrayList QuerySchema(string sql)
		{
			al=new ArrayList();
			try
			{
				if(this.ExecQuery(sql)==-1)return null;
				while(this.Reader.Read())
				{
					schema=new neusoft.HISFC.Object.Registration.DeptSchema();
					schema.ID=this.Reader[2].ToString();
					schema.SeeDate=DateTime.Parse(this.Reader[3].ToString());//��������
					
					schema.NoonID=this.Reader[5].ToString();//���
					schema.Dept.ID=this.Reader[6].ToString();//���Ҵ���
					schema.Dept.Name=this.Reader[7].ToString();//��������

					schema.RegLevel=this.Reader[8].ToString();//�Һż���

					if(this.Reader.IsDBNull(9)==false)
						schema.RegLimit=int.Parse(this.Reader[9].ToString());//�Һ��޶�
					if(this.Reader.IsDBNull(10)==false)
						schema.PreRegLimit=int.Parse(this.Reader[10].ToString());//ԤԼ�Һ��޶�
					schema.HasReg=int.Parse(this.Reader[12].ToString());//�ѹ�
					schema.HasPreReg=int.Parse(this.Reader[13].ToString());//ԤԼ�ѹ�
					schema.IsValid=neusoft.neuFC.Function.NConvert.ToBoolean(this.Reader[16].ToString());
					schema.StopReason.ID=this.Reader[17].ToString();
					schema.StopReason.Name=this.Reader[18].ToString();//ͣ��ԭ��
					schema.StopID=this.Reader[19].ToString();//ֹͣ��

					if(this.Reader.IsDBNull(20)==false)
						schema.StopDate=DateTime.Parse(this.Reader[20].ToString());
					schema.Memo=this.Reader[21].ToString();//��ע
					schema.OperDate=DateTime.Parse(this.Reader[22].ToString());
					schema.OperID=this.Reader[23].ToString();//������

					al.Add(schema);
				}
				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err="��ȡ���ҳ����Ű���Ϣ����!["+sql+"]"+e.Message;
				this.ErrCode=e.Message;
				return null;
			}
			return al;
		}
	}
}
