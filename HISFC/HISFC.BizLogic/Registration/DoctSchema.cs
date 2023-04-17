using System;
using System.Collections;

namespace neusoft.HISFC.Management.Registration
{
	/// <summary>
	/// ҽ�������Ű������
	/// </summary>
	public class DoctSchema:neusoft.neuFC.Management.Database
	{
		/// <summary>
		/// 
		/// </summary>
		public DoctSchema()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		private ArrayList al=null;
		/// <summary>
		/// ���ʵ��
		/// </summary>
		private neusoft.HISFC.Object.Registration.Noon noon=null;
		/// <summary>
		/// �Ű�ʵ��
		/// </summary>
		private neusoft.HISFC.Object.Registration.DoctSchema schema=null;

		/// <summary>
		/// �����Ű��¼
		/// </summary>
		/// <param name="schema"></param>
		/// <returns></returns>
		public int Insert(neusoft.HISFC.Object.Registration.DoctSchema schema)
		{	
			string sql="";
			
			if(this.Sql.GetSql("Registration.DoctSchema.Insert.1",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,schema.SeeDate,schema.Week,schema.NoonID,schema.Doctor.ID,
					schema.Doctor.Name,schema.Dept,schema.Room.ID,schema.Room.Name,
					schema.Estrade,schema.DoctType,schema.RegLevel,schema.RegLimit,
					schema.PreRegLimit,neusoft.neuFC.Function.NConvert.ToInt32(schema.IsValid),schema.StopReason.ID,schema.StopReason.Name,
					schema.StopID,schema.StopDate,schema.Memo,schema.OperID,
					schema.ID);

				
				return this.ExecNoQuery(sql);
				
			}
			catch(Exception e)
			{
				this.Err="����ҽ��������Ϣ�����!"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}			
		}
		/// <summary>
		/// ��������
		/// </summary>
		/// <param name="noon"></param>
		/// <returns></returns>
		public int Insert(neusoft.HISFC.Object.Registration.Noon noon)
		{
			string sql="";
			
			if(this.Sql.GetSql("Registration.DoctSchema.Insert.2",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,noon.ID,noon.Name,noon.BeginTime.ToString(),noon.EndTime.ToString(),
					noon.OperID,noon.OperDate.ToString());
				
				return this.ExecNoQuery(sql);
				
			}
			catch(Exception e)
			{
				this.Err="���������Ϣ�����!"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
		}
		/// <summary>
		/// ɾ���Ű��¼
		/// </summary>
		/// <param name="schema"></param>
		/// <returns></returns>
		public int Delete(neusoft.HISFC.Object.Registration.DoctSchema schema)
		{
			string sql="";
			if(this.Sql.GetSql("Registration.DoctSchema.Delete.1",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,schema.ID);

				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="ɾ��ҽ�������Ű���Ϣʱ����!"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
		}
		/// <summary>
		/// ɾ��һ������¼
		/// </summary>
		/// <param name="noon"></param>
		/// <returns></returns>
		public int Delete(neusoft.HISFC.Object.Registration.Noon noon)
		{
			string sql="";
			if(this.Sql.GetSql("Registration.DoctSchema.Delete.2",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,noon.ID);

				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="ɾ�������Ϣʱ����!"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
		}
		/// <summary>
		/// �����Ű��¼
		/// </summary>
		/// <param name="schema"></param>
		/// <returns></returns>
		public int Update(neusoft.HISFC.Object.Registration.DoctSchema schema)
		{
			string sql="";
			if(this.Sql.GetSql("Registration.DoctSchema.Update.1",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,schema.ID,schema.RegLimit,schema.PreRegLimit,neusoft.neuFC.Function.NConvert.ToInt32(schema.IsValid),
					schema.StopReason.ID,schema.StopReason.Name,schema.StopID,schema.StopDate.ToString());

				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="����ҽ�������Ű���Ϣʱ����!"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
		}
		/// <summary>
		/// ��ѯ���
		/// </summary>
		/// <returns></returns>
		public ArrayList Query()
		{
			string sql="";

			if(this.Sql.GetSql("Registration.DoctSchema.Query.1",ref sql)==-1)return null;
			if(this.ExecQuery(sql)==-1)return null;

			al=new ArrayList();
			try
			{
				while(this.Reader.Read())
				{
					noon=new neusoft.HISFC.Object.Registration.Noon();
					noon.ID=this.Reader[2].ToString();//id
					noon.Name=this.Reader[3].ToString();//name

					if(Reader.IsDBNull(4)==false)
						noon.BeginTime=DateTime.Parse(Reader[4].ToString());//��ʼʱ��
					if(Reader.IsDBNull(5)==false)
						noon.EndTime=DateTime.Parse(Reader[5].ToString());//����ʱ��
					if(Reader.IsDBNull(6)==false)
						noon.IsUrg=neusoft.neuFC.Function.NConvert.ToBoolean(Reader[6].ToString());//�Ƿ���
					
					noon.OperID=this.Reader[7].ToString();//����Ա
                    noon.OperDate=DateTime.Parse(Reader[8].ToString());

					al.Add(noon);
				}
				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err="��ȡ������"+e.Message;
				this.ErrCode=e.Message;
				return null;
			}
			return al;
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

			if(this.Sql.GetSql("Registration.DoctSchema.Query.2",ref sql)==-1)return null;
			if(this.Sql.GetSql("Registration.DoctSchema.Query.3",ref where)==-1)return null;

			where=string.Format(where,begin.ToString(),end.ToString());
			sql=sql + " "+where;

			return this.QuerySchema(sql);
		}
		/// <summary>
		/// ��ѯĳ�տ���ҽ���������
		/// </summary>
		/// <param name="seatDate"></param>
		/// <param name="dept"></param>
		/// <param name="noonID"></param>
		/// <returns></returns>
		public ArrayList Query(DateTime seatDate,neusoft.neuFC.Object.neuObject dept,string noonID)
		{
			string sql="",where="";

			if(this.Sql.GetSql("Registration.DoctSchema.Query.2",ref sql)==-1)return null;
			if(this.Sql.GetSql("Registration.DoctSchema.Query.5",ref where)==-1)return null;

			where=string.Format(where,seatDate.ToString(),dept.ID,noonID);
			sql=sql+" "+where;

			return QuerySchema(sql);
		}
		/// <summary>
		/// ��ѯĳ�졢ĳ�˵ĳ�����Ϣ
		/// </summary>
		/// <param name="seeDate"></param>
		/// <param name="noon"></param>
		/// <param name="doctID"></param>
		/// <returns></returns>
		public neusoft.HISFC.Object.Registration.DoctSchema Query(DateTime seeDate,string noon,string doctID)
		{
			string sql="",where="";

			if(this.Sql.GetSql("Registration.DoctSchema.Query.2",ref sql)==-1)return null;
			if(this.Sql.GetSql("Registration.DoctSchema.Query.4",ref where)==-1)return null;

			where=string.Format(where,seeDate.ToString(),doctID,noon);
			sql=sql+" "+where;

			QuerySchema(sql);
			if(al==null||al.Count==0)
				return null;
			else
				return (neusoft.HISFC.Object.Registration.DoctSchema)al[0];
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
					schema=new neusoft.HISFC.Object.Registration.DoctSchema();
					schema.SeeDate=DateTime.Parse(this.Reader[2].ToString());//����ʱ��
					
					schema.NoonID=this.Reader[4].ToString();//���
					schema.Doctor.ID=this.Reader[5].ToString();//ҽ������
					schema.Doctor.Name=this.Reader[6].ToString();//ҽ������
					schema.Dept=this.Reader[7].ToString();//�������
					schema.Room.Name=this.Reader[9].ToString();//����
					schema.Estrade=this.Reader[10].ToString();//��̨
					schema.DoctType=this.Reader[11].ToString();//ҽ�����
					schema.RegLevel=this.Reader[12].ToString();//�Һż���

					if(this.Reader.IsDBNull(13)==false)
						schema.RegLimit=int.Parse(this.Reader[13].ToString());//�Һ��޶�
					if(this.Reader.IsDBNull(14)==false)
                        schema.PreRegLimit=int.Parse(this.Reader[14].ToString());//ԤԼ�Һ��޶�
					schema.HasReg=int.Parse(this.Reader[16].ToString());//�ѹ�
					schema.HasPreReg=int.Parse(this.Reader[17].ToString());//ԤԼ�ѹ�
					schema.IsValid=neusoft.neuFC.Function.NConvert.ToBoolean(this.Reader[20].ToString());
					schema.StopReason.ID=this.Reader[21].ToString();
					schema.StopReason.Name=this.Reader[22].ToString();//ͣ��ԭ��
					schema.StopID=this.Reader[23].ToString();//ֹͣ��

					if(this.Reader.IsDBNull(24)==false)
					schema.StopDate=DateTime.Parse(this.Reader[24].ToString());
					schema.Memo=this.Reader[25].ToString();//��ע
					schema.OperID=this.Reader[26].ToString();//����ʱ��
					schema.OperDate=DateTime.Parse(this.Reader[27].ToString());
					schema.ID=this.Reader[28].ToString();

					al.Add(schema);
				}
				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err="��ȡҽ�������Ű���Ϣ����!"+e.Message;
				this.ErrCode=e.Message;
				return null;
			}
			return al;
		}
	}
	/// <summary>
	/// ִ��״̬
	/// </summary>
	public enum status
	{
		/// <summary>
		/// ��ѯ���
		/// </summary>
		QueryNoon
	}
}
