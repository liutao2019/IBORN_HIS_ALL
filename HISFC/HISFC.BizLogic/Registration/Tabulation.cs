using System;
using System.Collections;

namespace neusoft.HISFC.Management.Registration
{
	/// <summary>
	/// �Ű������
	/// </summary>
	public class Tabulation:neusoft.neuFC.Management.Database
	{
		/// <summary>
		/// �Ű������
		/// ed.huangxw
		/// </summary>
		public Tabulation()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// �Ű�ʵ��
		/// </summary>
		private neusoft.HISFC.Object.Registration.Tabulation tabulation;
		private neusoft.HISFC.Object.Registration.WorkType worktype;
		private neusoft.HISFC.Object.Registration.TabularType tabulartype;
		private ArrayList al=null;

		#region ����ɾ����
		/// <summary>
		/// ����������_goa_med_worktype
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public int Insert(neusoft.HISFC.Object.Registration.WorkType type)
		{
			#region sql
			//			INSERT INTO goa_med_worktype   --�������ͱ�
			//          ( parent_code,   --����ҽ�ƻ�������
			//            current_code,   --����ҽ�ƻ�������
			//            id,   --�������ͱ���
			//            name,   --������������
			//            spell_code,   --ƴ����
			//            class_code,   --�������ʹ���
			//            sign,   --�������ͷ���
			//            begin_time,   --������ʼʱ��
			//            end_time,   --���ڽ���ʱ��
			//            re_quotiety,   --�ۿ�ϵ��
			//            positive_days,   --��Ӧ��������
			//            minus_days,   --��Ӧȱ������
			//            remark,   --��ע
			//            valid_flag,   --��Ч 1��/0��
			//            oper_code,   --����Ա
			//            oper_date )  --��������
			//     VALUES 
			//          ( '[��������]',   --����ҽ�ƻ�������
			//            '[��������]',   --����ҽ�ƻ�������
			//            '{0}',   --�������ͱ���
			//            '{1}',   --������������
			//            '{2}',   --ƴ����
			//            '{3}',   --�������ʹ���
			//            '{4}',   --�������ͷ���
			//            to_date('{5}','yyyy-mm-dd HH24:mi:ss'),   --������ʼʱ��
			//            to_date('{6}','yyyy-mm-dd HH24:mi:ss'),   --���ڽ���ʱ��
			//            '{7}',   --�ۿ�ϵ��
			//            '{8}',   --��Ӧ��������
			//            '{9}',   --��Ӧȱ������
			//            '{10}',   --��ע
			//            '{11}',   --��Ч 1��/0��
			//            '{12}',   --����Ա
			//            sysdate) --��������
			#endregion
			string sql="";
			if(this.Sql.GetSql("Registration.Tabulation.Insert.1",ref sql)==-1)return -1;

			#region ��֤
			if(valid(type)==-1)return -1;
			#endregion
			try
			{
				sql=string.Format(sql,type.ID,type.Name,type.SpellCode,type.ClassID,
					type.Sign,type.BeginTime.ToString(),type.EndTime.ToString(),type.Quotiety.ToString(),
					type.PositiveDays.ToString(),type.MinusDays.ToString(),type.Memo,neusoft.neuFC.Function.NConvert.ToInt32(type.Isvalid),
					type.OperID,type.ForeColor);
				return this.ExecNoQuery(sql);				
			}
			catch(Exception e)
			{
				this.Err="��������������![Registration.Tabulation.Insert.1]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}			
		}
		/// <summary>
		/// ����Ƴ��ó������_goa_med_depttype,�������ʵ��,ʵ��ֻҪ��ֵid,OperID
		/// </summary>
		/// <param name="deptID"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public int Insert(string deptID,neusoft.HISFC.Object.Registration.WorkType type)
		{
			#region sql
			//			 INSERT INTO goa_med_depttype   --�Ƴ��ó������ͱ�
			//          ( parent_code,   --����ҽ�ƻ�������
			//            current_code,   --����ҽ�ƻ�������
			//            dept_code,   --���ұ���
			//            id,   --�������ͱ���
			//            oper_code,   --����Ա
			//            oper_date )  --��������
			//     VALUES 
			//          ( '[��������]',   --����ҽ�ƻ�������
			//            '[��������]',   --����ҽ�ƻ�������
			//            '{0}',   --���ұ���
			//            '{1}',   --�������ͱ���
			//            '{2}',   --����Ա
			//            sysdate ) --��������
			#endregion
			string sql="";
			if(this.Sql.GetSql("Registration.Tabulation.Insert.2",ref sql)==-1)return -1;

			try
			{
				sql=string.Format(sql,deptID,type.ID,type.OperID);
				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="����Ƴ��ó��ڱ����![Registration.Tabulation.Insert.2]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}			
		}
		/// <summary>
		/// �����Ű�_goa_med_tabulation
		/// </summary>
		/// <param name="tabular"></param>
		/// <returns></returns>
		public int Insert(neusoft.HISFC.Object.Registration.Tabulation tabular)
		{
			#region sql
			/*INSERT INTO goa_med_tabular   --�Ű��
		  ( parent_code,   --����ҽ�ƻ�������
			current_code,   --����ҽ�ƻ�������
			empl_code,   --Ա�����
			dept_code,   --���ұ���
			work_date,   --��������
			worktype_id,   --��������
			worktype_name,   --������������
			class_code,   --�Ű����
			begin_time,   --����������ʼʱ��
			end_time,   --�������ͽ���ʱ��
			minus_days,   --������Ȩֵ
			positive_days,   --��������Ȩֵ
			arrange_code,   --�Ű����-�Ű࿪ʼʱ��+����ʱ��
			oper_code,   --�Ű���
			oper_date,   --�Ű�ʱ��
			check_flag,   --�Ƿ���� 1��/0��
			remark )  --��ע
	 VALUES 
		  ( '[��������]',   --����ҽ�ƻ�������
			'[��������]',   --����ҽ�ƻ�������
			'{0}',   --Ա�����
			'{1}',   --���ұ���
			to_date('{2}','yyyy-mm-dd HH24:mi:ss'),   --��������
			'{3}',   --��������
			'{4}',   --������������
			'{5}',   --�Ű����
			to_date('{6}','yyyy-mm-dd HH24:mi:ss'),   --����������ʼʱ��
			to_date('{7}','yyyy-mm-dd HH24:mi:ss'),   --�������ͽ���ʱ��
			'{8}',   --������Ȩֵ
			'{9}',   --��������Ȩֵ
			'{10}',   --�Ű����-�Ű࿪ʼʱ��+����ʱ��
			'{11}',   --�Ű���
			sysdate,   --�Ű�ʱ��
			'{12}',   --�Ƿ���� 1��/0��
			'{13}' ) --��ע*/
			#endregion
			string sql="";
			if(this.Sql.GetSql("Registration.Tabulation.Insert.3",ref sql)==-1)return -1;
			
			if(neusoft.neuFC.Public.String.ValidMaxLengh(tabular.Memo,100)==false)
			{
				this.Err="��ע���ܴ���50���ַ�!";
				return -1;
			}

			try
			{
				sql=string.Format(sql,tabular.EmplID,tabular.DeptID,tabular.Workdate.ToString(),tabular.Kind.ID,
					tabular.Kind.Name,tabular.Kind.ClassID,tabular.Kind.BeginTime.ToString(),tabular.Kind.EndTime.ToString(),
					tabular.Kind.MinusDays.ToString(),tabular.Kind.PositiveDays.ToString(),tabular.arrangeID,tabular.OperID,
					"0",tabular.Memo,tabular.SortID);

				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="�����Ű���Ϣ�����![Registration.Tabulation.Insert.3]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}			
		}
		/// <summary>
		/// ɾ���Ƴ��ó������
		/// </summary>
		/// <param name="deptID"></param>
		/// <param name="ID"></param>
		/// <returns></returns>
		public int Delete(string deptID,string ID)
		{
			string sql="";
			if(this.Sql.GetSql("Registration.Tabulation.Delete.1",ref sql)==-1)return -1;
			
			try
			{
				sql=string.Format(sql,deptID,ID);
				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="ɾ���Ƴ��ó����������![Registration.Tabulation.Delete.1]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
		}
		/// <summary>
		/// ɾ���������
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public int Delete(string ID)
		{
			string sql="";
			if(this.Sql.GetSql("Registration.Tabulation.Delete.2",ref sql)==-1)return -1;
			
			try
			{
				sql=string.Format(sql,ID);
				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="ɾ�������������![Registration.Tabulation.Delete.2]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
		}
		/// <summary>
		/// �����Ű����ɾ���Ű��¼
		/// </summary>
		/// <param name="arrangeID"></param>
		/// <param name="deptID"></param>
		/// <returns></returns>
		public int DeleteTabular(string arrangeID,string deptID)
		{
			string sql="";
			if(this.Sql.GetSql("Registration.Tabulation.Delete.3",ref sql)==-1)return -1;
			
			try
			{
				sql=string.Format(sql,arrangeID,deptID);
				return this.ExecNoQuery(sql);
			}
			catch(Exception e)
			{
				this.Err="ɾ���Ƴ��ó����������![Registration.Tabulation.Delete.3]"+e.Message;
				this.ErrCode=e.Message;
				return -1;
			}
		}
		#endregion

		#region ��ѯ
		/// <summary>
		/// ��������Ų�ѯ�Ű���Ϣ
		/// </summary>
		/// <param name="arrangeID"></param>
		/// <param name="deptID"></param>
		/// <returns></returns>
		public ArrayList Query(string arrangeID,string deptID)
		{
			#region sql
			/*SELECT parent_code,   --����ҽ�ƻ�������
	   current_code,   --����ҽ�ƻ�������
	   empl_code,   --Ա�����
	   dept_code,   --���ұ���
	   work_date,   --��������
	   worktype_id,   --��������
	   worktype_name,   --������������
	   class_code,   --�Ű����
	   begin_time,   --����������ʼʱ��
	   end_time,   --�������ͽ���ʱ��
	   minus_days,   --������Ȩֵ
	   positive_days,   --��������Ȩֵ
	   arrange_code,   --�Ű����-�Ű࿪ʼʱ��+����ʱ��
	   oper_code,   --�Ű���
	   oper_date,   --�Ű�ʱ��
	   check_flag,   --�Ƿ���� 1��/0��
	   remark    --��ע
  FROM goa_med_tabular   --�Ű��
 WHERE parent_code='[��������]'
   AND current_code='[��������]'
   AND arrange_code='{0}'
   AND dept_code='{1}'*/
			#endregion
			string sql="";
			if(this.Sql.GetSql("Registration.Tabulation.Query.1",ref sql)==-1)return null;
			
			try
			{
				sql=string.Format(sql,arrangeID,deptID);
				
				if(QueryTabular(sql)==-1)return null;
			}
			catch(Exception e)
			{
				this.Err="��ȡ�����Ű���Ϣ����![Registration.Tabulation.Query.1]"+e.Message;
				this.ErrCode=e.Message;
				if(Reader!=null&&Reader.IsClosed==false)Reader.Close();
				return null;
			}
			return al;
		}
		/// <summary>
		/// ���������ڡ����Ҳ�ѯ�Ű���Ϣ
		/// </summary>
		/// <param name="workDate"></param>
		/// <param name="dept"></param>
		/// <returns></returns>
		public ArrayList Query(DateTime workDate,neusoft.neuFC.Object.neuObject dept)
		{
			string sql="";
			if(this.Sql.GetSql("Registration.Tabulation.Query.2",ref sql)==-1)return null;

			try
			{
				sql=string.Format(sql,workDate.ToString(),dept.ID);
				if(QueryTabular(sql)==-1)return null;
			}
			catch(Exception e)
			{
				this.Err="��ȡ�����Ű���Ϣ����![Registration.Tabulation.Query.2]"+e.Message;
				this.ErrCode=e.Message;
				if(Reader!=null&&Reader.IsClosed==false)Reader.Close();
				return null;
			}
			return al;
		}
		/// <summary>
		/// ��ʱ��Ρ����Ҳ�ѯ�Ű����
		/// </summary>
		/// <param name="beginDate"></param>
		/// <param name="deptID"></param>
		/// <returns></returns>
		public ArrayList Query(DateTime beginDate,string deptID)
		{
			string sql="";
			if(this.Sql.GetSql("Registration.Tabulation.Query.7",ref sql)==-1)return null;

			try
			{
				sql=string.Format(sql,beginDate.ToString(),deptID);
				if(this.ExecQuery(sql)==-1)return null;

				al=new ArrayList();

				while(Reader.Read())
				{
					al.Add(Reader[0].ToString());
				}
				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err="��ȡ�����Ű���Ϣ����![Registration.Tabulation.Query.7]"+e.Message;
				this.ErrCode=e.Message;
				if(Reader!=null&&Reader.IsClosed==false)Reader.Close();
				return null;
			}
			return al;
		}

		/// <summary>
		/// ��ѯ�Ƴ��ó������
		/// </summary>
		/// <param name="deptID"></param>
		/// <returns></returns>
		public ArrayList Query(string deptID)
		{
			string sql="";
			if(this.Sql.GetSql("Registration.Tabulation.Query.3",ref sql)==-1)return null;

			try
			{
				sql=string.Format(sql,deptID);
				if(QueryType(sql)==-1)return null;
			}
			catch(Exception e)
			{
				this.Err="��ȡ�Ƴ��ó��������Ϣ����![Registration.Tabulation.Query.3]"+e.Message;
				this.ErrCode=e.Message;
				if(Reader!=null&&Reader.IsClosed==false)Reader.Close();
				return null;
			}
			
			return al;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="begin"></param>
		/// <param name="end"></param>
		/// <param name="deptcode"></param>
		/// <param name="classcode"></param>
		/// <returns></returns>
		public ArrayList QueryTabular(DateTime begin,DateTime end,string deptcode,string classcode)
		{
			string sql="",where="";
			if(this.Sql.GetSql("Registration.Tabular.Query.0",ref sql)==-1)return null;
			if(classcode!=null&&classcode!="")
			{
				if(this.Sql.GetSql("Registration.Tabular.Query.1",ref where)==-1)return null;
				sql=sql + where;
			}
			try
			{
				sql=string.Format(sql,begin.ToString(),end.ToString(),deptcode,classcode);
				if(QueryTabularType(sql)==-1)return null;
			}
			catch(Exception e)
			{
				this.Err="��ȡ�����Ű���Ϣ����[Registration.Tabulation.Query.0]"+e.Message;
				this.ErrCode=e.Message;
				if(Reader!=null&&Reader.IsClosed==false)Reader.Close();
				return null;
			}
			return al;
		}


		/// <summary>
		/// ��ѯȫ������Ч����Ч�������
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public ArrayList Query(QueryState state)
		{
			string sql="",where="";
			if(this.Sql.GetSql("Registration.Tabulation.Query.4",ref sql)==-1)return null;
			if(state==QueryState.Valid)
			{
				if(this.Sql.GetSql("Registration.Tabulation.Query.5",ref where)==-1)return null;
				sql=sql + where;
			}
			else if(state==QueryState.Invalid)
			{
				if(this.Sql.GetSql("Registration.Tabulation.Query.6",ref where)==-1)return null;
				sql=sql + where;
			}

			try
			{
				if(QueryType(sql)==-1)return null;
			}
			catch(Exception e)
			{
				this.Err="��ȡ���������Ϣ����![Registration.Tabulation.Query.4]"+e.Message;
				this.ErrCode=e.Message;
				if(Reader!=null&&Reader.IsClosed==false)Reader.Close();
				return null;
			}

			return al;
		}
		/// <summary>
		/// �Ű�Ļ�����ѯ
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private int QueryTabular(string sql)
		{
			if(this.ExecQuery(sql)==-1)return -1;
				
			al=new ArrayList();
			while(Reader.Read())
			{
				tabulation=new neusoft.HISFC.Object.Registration.Tabulation();
				tabulation.EmplID=Reader[2].ToString();//Ա������
				tabulation.DeptID=Reader[3].ToString();//���Ҵ���
				tabulation.Workdate=DateTime.Parse(Reader[4].ToString());//��������
				tabulation.Kind.ID=Reader[5].ToString();//�������
				tabulation.Kind.Name=Reader[6].ToString();//�����������
				tabulation.Kind.ClassID=Reader[7].ToString();//�������
				if(Reader.IsDBNull(8)==false)tabulation.Kind.BeginTime=DateTime.Parse(Reader[8].ToString());
				if(Reader.IsDBNull(9)==false)tabulation.Kind.EndTime=DateTime.Parse(Reader[9].ToString());
				if(Reader.IsDBNull(10)==false)tabulation.Kind.MinusDays=decimal.Parse(Reader[10].ToString());
				if(Reader.IsDBNull(11)==false)tabulation.Kind.PositiveDays=decimal.Parse(Reader[11].ToString());
				tabulation.arrangeID=Reader[12].ToString();//�Ű����
				tabulation.OperID=Reader[13].ToString();//����Ա
				if(Reader.IsDBNull(14)==false)tabulation.OperDate=DateTime.Parse(Reader[14].ToString());//����ʱ��
				tabulation.Memo=Reader[16].ToString();//��ע
				tabulation.SortID = int.Parse(Reader[17].ToString());

				al.Add(tabulation);
			}
			Reader.Close();
		
			return 0;
		}
		/// <summary>
		/// ������������ѯ
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private int QueryType(string sql)
		{
			if(this.ExecQuery(sql)==-1)return -1;
				
			al=new ArrayList();
			while(Reader.Read())
			{
				worktype=new neusoft.HISFC.Object.Registration.WorkType();
				worktype.ID=Reader[2].ToString();//id
				worktype.Name=Reader[3].ToString();//Name
				worktype.SpellCode=Reader[4].ToString();//ƴ����
				worktype.ClassID=Reader[5].ToString();//�������
				worktype.Sign=Reader[6].ToString();//���
				if(Reader.IsDBNull(7)==false)worktype.BeginTime=DateTime.Parse(Reader[7].ToString());
				if(Reader.IsDBNull(8)==false)worktype.EndTime=DateTime.Parse(Reader[8].ToString());
				if(Reader.IsDBNull(9)==false)worktype.Quotiety=decimal.Parse(Reader[9].ToString());//�ۿ�ϵ��
				if(Reader.IsDBNull(10)==false)worktype.PositiveDays=decimal.Parse(Reader[10].ToString());//����Ȩֵ
				if(Reader.IsDBNull(11)==false)worktype.MinusDays=decimal.Parse(Reader[11].ToString());//ȱ��Ȩֵ
				worktype.Memo=Reader[12].ToString();//��ע
				if(Reader.IsDBNull(13)==false)worktype.Isvalid=neusoft.neuFC.Function.NConvert.ToBoolean(Reader[13].ToString());
				worktype.OperID=Reader[14].ToString();//����Ա
				if(Reader.IsDBNull(15)==false)worktype.OperDate=DateTime.Parse(Reader[15].ToString());
				worktype.ForeColor=Reader[16].ToString();//ǰ��ɫ

				al.Add(worktype);
			}
			Reader.Close();
			return 0;
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public int QueryTabularType(string sql)
		{
			if(this.ExecQuery(sql)==-1)return -1;
			al=new ArrayList();
			while(Reader.Read())
			{
				tabulartype=new neusoft.HISFC.Object.Registration.TabularType();
				tabulartype.EmpCode=Reader[2].ToString();//Ա������
				tabulartype.DeptCode=Reader[3].ToString();
				if(Reader.IsDBNull(4)==false)tabulartype.WorkDate=DateTime.Parse(Reader[4].ToString());
				tabulartype.Name=Reader[5].ToString();
				tabulartype.ClassCode=Reader[6].ToString();
				if(Reader.IsDBNull(7)==false)tabulartype.PositiveDays=decimal.Parse(Reader[7].ToString());
				if(Reader.IsDBNull(8)==false)tabulartype.MinusDays=decimal.Parse(Reader[8].ToString());
				tabulartype.OperCode=Reader[9].ToString();
				if(Reader.IsDBNull(10)==false)tabulartype.OperDate=DateTime.Parse(Reader[10].ToString());
				if(Reader.IsDBNull(11)==false)tabulartype.IsChecked=neusoft.neuFC.Function.NConvert.ToBoolean(Reader[11].ToString());
				tabulartype.Remark=Reader[12].ToString();

				al.Add(tabulartype);
			}
			Reader.Close();
			return 0;
		}

		#endregion


		#region ��֤
		/// <summary>
		/// �������ʵ��Ϸ�����֤
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private int valid(neusoft.HISFC.Object.Registration.WorkType type)
		{
			type.Quotiety=decimal.Round(type.Quotiety,2);//ȡС����λ
			if(type.Quotiety>99.99m||type.Quotiety<0m)
			{
				this.Err="���ۿ�ϵ������С��100,�Ҵ���0!";
				return -1;
			}
			type.PositiveDays=decimal.Round(type.PositiveDays,1);
			if(type.PositiveDays<0m||type.PositiveDays>99.9m)
			{
				this.Err="����ϵ������С��100,�Ҵ���0!";
				return -1;
			}
			type.MinusDays=decimal.Round(type.MinusDays,1);
			if(type.MinusDays<0m||type.MinusDays>99.9m)
			{
				this.Err="ȱ��ϵ������С��100,�Ҵ���0!";
				return -1;
			}
			
			if(neusoft.neuFC.Public.String.ValidMaxLengh(type.Name,20)==false)
			{
				this.Err="����������Ʋ�����10������!";
				return -1;
			}
			if(type.Name==null||type.Name=="")
			{
				this.Err="����������Ʋ���Ϊ��!";
				return -1;
			}
			if(neusoft.neuFC.Public.String.ValidMaxLengh(type.Memo,100)==false)
			{
				this.Err="��ע���벻����50������!";
				return -1;
			}
			if(neusoft.neuFC.Public.String.ValidMaxLengh(type.SpellCode,8)==false)
			{
				this.Err="ƴ������벻����8���ַ�!";
				return -1;
			}
			if(neusoft.neuFC.Public.String.ValidMaxLengh(type.ClassID,2)==false)
			{
				this.Err="���ڴ��������벻����2���ַ�!";
				return -1;
			}
			if(type.ClassID==null||type.ClassID=="")
			{
				this.Err="���ڴ�����벻��Ϊ��!";
				return -1;
			}
			if(neusoft.neuFC.Public.String.ValidMaxLengh(type.Sign,8)==false)
			{
				this.Err="��Ʊ��벻����8���ַ�!";
				return -1;
			}
			if(neusoft.neuFC.Public.String.ValidMaxLengh(type.ID,3)==false)
			{
				this.Err="���������벻����3���ַ�!";
				return -1;
			}
			if(type.ID==null||type.ID=="")
			{
				this.Err="���������벻��Ϊ��!";
				return -1;
			}

			return 0;
		}
		#endregion
	}
	/// <summary>
	/// ��ѯ�����������
	/// </summary>
	public enum QueryState
	{
		/// <summary>
		/// ����
		/// </summary>
		All,
		/// <summary>
		/// ��Ч
		/// </summary>
		Valid,
		/// <summary>
		/// ��Ч
		/// </summary>
		Invalid
	}
}
