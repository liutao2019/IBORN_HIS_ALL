using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Registration
{
	/// <summary>
	/// �Ű�ģ�������
	/// </summary>
	public class SchemaTemplet:FS.FrameWork.Management.Database
	{
		/// <summary>
		/// 
		/// </summary>
		public SchemaTemplet()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����
		/// <summary>
		/// �Ǽ�һ���Ű�ģ��
		/// </summary>
		/// <param name="templet"></param>
		/// <returns></returns>
        public int Insert(FS.HISFC.Models.Registration.SchemaTemplet templet)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Templet.Insert", ref sql) == -1)
                return -1;

            try
            {
                sql = string.Format(sql, 
                    templet.ID, 
                    (int)templet.Week, 
                    (int)templet.EnumSchemaType,
                    templet.Dept.ID,
                    templet.Dept.Name, 
                    templet.Doct.ID, 
                    templet.Doct.Name, 
                    templet.Noon.ID,
                    templet.RegQuota.ToString(), 
                    FS.FrameWork.Function.NConvert.ToInt32(templet.IsValid),
                    templet.Memo, 
                    templet.Oper.ID, 
                    templet.Oper.OperTime.ToString(), 
                    templet.DoctType.ID,
                    templet.Begin.ToString(), 
                    templet.End.ToString(), 
                    templet.TelQuota,
                    templet.SpeQuota,
                    FS.FrameWork.Function.NConvert.ToInt32(templet.IsAppend),
                    templet.StopReason.ID, 
                    templet.StopReason.Name,
                    templet.RegLevel.ID, 
                    templet.RegLevel.Name,
                    templet.Room.ID,
                    templet.Room.Name,
                    templet.Console.ID,
                    templet.Console.Name
                    );
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Templet.Insert]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

		#endregion

		#region ɾ��

		/// <summary>
		/// ����IDɾ��һ��ģ���¼
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public int Delete(string ID)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Registration.Templet.Delete",ref sql) == -1)return -1;

			try
			{
				sql = string.Format(sql,ID);
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Templet.Delete]��ʽ��ƥ��!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}
		#endregion

		#region ��ѯ
		/// <summary>
		/// ����ģ�����ͺ����ڡ����Ҳ�ѯ�Ű�ģ��
		/// </summary>
		/// <param name="schemaType"></param>
		/// <param name="week"></param>
		/// <param name="DeptID"></param>
		/// <returns></returns>
        public ArrayList Query(FS.HISFC.Models.Base.EnumSchemaType schemaType, DayOfWeek week, string DeptID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Templet.Query.1", ref sql) == -1)
                return null;
            if (this.Sql.GetCommonSql("Registration.Templet.Query.2", ref where) == -1)

                return null;

            sql = sql + " " + where;

            try
            {
                sql = string.Format(sql, (int)schemaType, (int)week, DeptID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Templet.Query.2]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return this.QueryBase(sql);
        }

		/// <summary>
		/// �������ڡ�ģ������ѯ�Ƿ���Чģ����Ϣ��
		/// ģ�����
		/// </summary>
		/// <param name="schemaType"></param>
		/// <param name="week"></param>
		/// <param name="IsValid"></param>
		/// <returns></returns>
		public ArrayList Query(FS.HISFC.Models.Base.EnumSchemaType schemaType,DayOfWeek week,bool IsValid)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.Templet.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.Templet.Query.3",ref where ) == -1)return null;

			sql = sql + " " + where;

			try
			{
				sql = string.Format(sql,(int)week,(int)schemaType,FS.FrameWork.Function.NConvert.ToInt32(IsValid));
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Templet.Query.3]��ʽ��ƥ��!"+e.Message;
				this.ErrCode = e.Message;
				return null;
			}
			return this.QueryBase(sql);
		}

		/// <summary>
		/// ģ��ʵ��
		/// </summary>
		protected FS.HISFC.Models.Registration.SchemaTemplet schemaTemplet;
		/// <summary>
		/// 
		/// </summary>
		protected ArrayList al ;
		/// <summary>
		/// ��ѯ
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		private ArrayList QueryBase(string sql)
		{
			if(this.ExecQuery(sql) == -1) return null;

			this.al = new ArrayList();

			try
			{
				while(this.Reader.Read())
				{
					this.schemaTemplet = new FS.HISFC.Models.Registration.SchemaTemplet();
					
					this.schemaTemplet.ID = this.Reader[2].ToString();
					this.schemaTemplet.Week = (DayOfWeek)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString()));
					this.schemaTemplet.EnumSchemaType = (FS.HISFC.Models.Base.EnumSchemaType)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString()));
					this.schemaTemplet.Dept.ID = this.Reader[5].ToString();
					this.schemaTemplet.Dept.Name = this.Reader[6].ToString();
					this.schemaTemplet.Doct.ID = this.Reader[7].ToString();
					this.schemaTemplet.Doct.Name = this.Reader[8].ToString();
					this.schemaTemplet.Noon.ID = this.Reader[9].ToString();
					this.schemaTemplet.RegQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());
					this.schemaTemplet.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[11].ToString());
					this.schemaTemplet.Memo = this.Reader[12].ToString();
					this.schemaTemplet.Oper.ID = this.Reader[13].ToString();
					this.schemaTemplet.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14].ToString());
					this.schemaTemplet.DoctType.ID = this.Reader[15].ToString() ;
					this.schemaTemplet.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[16].ToString()) ;
					this.schemaTemplet.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[17].ToString()) ;
					this.schemaTemplet.TelQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18].ToString()) ;
					this.schemaTemplet.SpeQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19].ToString()) ;
					this.schemaTemplet.IsAppend = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[20].ToString());
                    this.schemaTemplet.StopReason.ID = this.Reader[21].ToString();
                    this.schemaTemplet.StopReason.Name = this.Reader[22].ToString();
                    this.schemaTemplet.RegLevel.ID = this.Reader[23].ToString();
                    this.schemaTemplet.RegLevel.Name = this.Reader[24].ToString();
                    schemaTemplet.Room.ID = Reader[25].ToString();
                    schemaTemplet.Room.Name = Reader[26].ToString();
                    schemaTemplet.Console.ID = Reader[27].ToString();
                    schemaTemplet.Console.Name = Reader[28].ToString();

					this.al.Add(this.schemaTemplet);
				}

				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err = "��ѯ�Ű�ģ����Ϣ����!" + e.Message;
				this.ErrCode = e.Message;
				return null;
			}

			return al;
		}
		#endregion
	}
}
