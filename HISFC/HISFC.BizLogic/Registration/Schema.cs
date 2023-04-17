using System;
using System.Collections;
using System.Data ;

namespace FS.HISFC.BizLogic.Registration
{
	/// <summary>
	/// �Ű������
	/// </summary>
    public class Schema : FS.FrameWork.Management.Database
	{
		/// <summary>
		/// 
		/// </summary>
		public Schema()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ����
		/// <summary>
		/// �Ǽ�һ���Ű��¼
		/// </summary>
		/// <param name="schema"></param>
		/// <returns></returns>
		public int Insert(FS.HISFC.Models.Registration.Schema schema)
		{
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Schema.Insert", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, schema.Templet.ID, (int)schema.Templet.EnumSchemaType, schema.SeeDate.ToString(),
                    (int)schema.Templet.Week, schema.Templet.Noon.ID, schema.Templet.Dept.ID, schema.Templet.Dept.Name,
                    schema.Templet.Doct.ID, schema.Templet.Doct.Name, schema.Templet.DoctType.ID, schema.Templet.RegQuota,
                    schema.RegedQTY, FS.FrameWork.Function.NConvert.ToInt32(schema.Templet.IsValid),
                    schema.Templet.StopReason.ID, schema.Templet.StopReason.Name, schema.Templet.Stop.ID, schema.Templet.Stop.OperTime.ToString(),
                    schema.Templet.Memo, schema.Templet.Oper.ID, schema.Templet.Oper.OperTime.ToString(),
                    schema.Templet.Begin.ToString(), schema.Templet.End.ToString(), schema.Templet.TelQuota,
                    schema.TeledQTY, schema.TelingQTY, schema.Templet.SpeQuota, schema.SpedQTY,
                    FS.FrameWork.Function.NConvert.ToInt32(schema.Templet.IsAppend),
                    schema.Templet.RegLevel.ID, schema.Templet.RegLevel.Name,
                    schema.Templet.Room.ID, schema.Templet.Room.Name,
                    schema.Templet.Console.ID, schema.Templet.Console.Name);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Insert]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
		}
		#endregion

		#region ɾ��
		/// <summary>
		/// ����IDɾ��һ���Ű��¼
		/// </summary>
		/// <param name="ID"></param>
		/// <returns></returns>
		public int Delete(string ID)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Registration.Schema.Delete",ref sql) == -1)return -1;

			try
			{
				sql = string.Format(sql,ID);
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Schema.Delete]��ʽ��ƥ��!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}
		#endregion

		#region �޸�
		/// <summary>
		/// ����ID�޸�һ���Ű��¼(��ʹ�õ�)
		/// </summary>
		/// <param name="schema"></param>
		/// <returns></returns>
		public int Update(FS.HISFC.Models.Registration.Schema schema)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Registration.Schema.Update",ref sql) == -1)return -1;

			try
			{
                sql = string.Format(sql, schema.Templet.RegQuota, FS.FrameWork.Function.NConvert.ToInt32(schema.Templet.IsValid),
                    schema.Templet.StopReason.ID, schema.Templet.StopReason.Name, schema.Templet.Stop.ID,
                    schema.Templet.Stop.OperTime.ToString(), schema.Templet.Memo, schema.Templet.Oper.ID,
                    schema.Templet.Oper.OperTime.ToString(), schema.Templet.TelQuota,
                    schema.Templet.SpeQuota, schema.Templet.ID, 
                    schema.Templet.RegLevel.ID, schema.Templet.RegLevel.Name,
                    schema.Templet.Room.ID, schema.Templet.Room.Name,
                    schema.Templet.Console.ID, schema.Templet.Console.Name
                );
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Schema.Update]��ʽ��ƥ��!"+e.Message;
				this.ErrCode = e.Message;
				return -1;
			}

			return this.ExecNoQuery(sql);
		}

        /// <summary>
        ///  ֹͣ���ﻤʿ���У�ֹͣ�����Ű�
        /// </summary>
        /// <returns></returns>
        public int UpdateDoctSchemaValid(string validFlag, string seeDate, string noonCode, string deptCode, string doctCode, string reasonNo, string reasonName, string stopOpcd, DateTime stopDate)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Schema.UpdateDoctSchemaValid", ref sql) == -1) 
            {
                sql = @"UPDATE fin_opr_schema   --ҽʦ�����
                        SET valid_flag='{0}',   --1����/0ͣ��
                        Reason_No ='{5}',
                        reason_name='{6}',
                        stop_opcd ='{7}',
                        stop_date =to_date('{8}','yyyy-mm-dd hh24:mi:ss')
                        where see_date= to_date('{1}','yyyy:mm:dd') 
                        and noon_code='{2}'
                        and dept_code='{3}'
                        and doct_code='{4}'";
            }
            try
            {
                sql = string.Format(sql, validFlag, seeDate, noonCode, deptCode, doctCode, reasonNo, reasonName, stopOpcd, stopDate);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Update]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }
            return this.ExecNoQuery(sql);
        }




        /// <summary>
        /// ����ID�����Ű���Ϣ(δʹ�õ�)
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public int UpdateUnused(FS.HISFC.Models.Registration.Schema schema)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Schema.Update.3", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, schema.Templet.ID, schema.Templet.Noon.ID, schema.Templet.Begin.ToString(),
                        schema.Templet.End.ToString(), schema.Templet.Dept.ID, schema.Templet.Dept.Name,
                        schema.Templet.Doct.ID, schema.Templet.Doct.Name, schema.Templet.DoctType.ID,
                        schema.Templet.RegQuota, schema.Templet.TelQuota, schema.Templet.SpeQuota,
                        FS.FrameWork.Function.NConvert.ToInt32(schema.Templet.IsValid),
                        FS.FrameWork.Function.NConvert.ToInt32(schema.Templet.IsAppend),
                        schema.Templet.StopReason.ID, schema.Templet.StopReason.Name, schema.Templet.Stop.ID,
                        schema.Templet.Stop.OperTime.ToString(), schema.Templet.Memo, schema.Templet.Oper.ID,
                        schema.Templet.Oper.OperTime.ToString(), schema.Templet.RegLevel.ID, schema.Templet.RegLevel.Name,
                    schema.Templet.Room.ID, schema.Templet.Room.Name,
                    schema.Templet.Console.ID, schema.Templet.Console.Name);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Update.3]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

		/// <summary>
		/// ���¿�������
		/// IsReged:�Ƿ������˹Һ�,IsTeling:�Ƿ�������ԤԼ,IsTeled:�Ƿ�����Һ�
		/// IsSped:�Ƿ�����Һ�
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="IsReged"></param>
		/// <param name="IsTeling"></param>
		/// <param name="IsTeled"></param>
		/// <param name="IsSped"></param>
		/// <returns></returns>
		public int Increase(string ID,bool IsReged,bool IsTeling, bool IsTeled,bool IsSped)
		{
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Schema.Update.4", ref sql) == -1) return -1;

            int reged = 0, teling = 0, teled = 0, sped = 0, add = 1;

            if (IsReged) reged = 1;
            if (IsTeling)//�绰ԤԼ�������ӿ������
            {
                teling = 1;
                add = 0;
            }
            if (IsTeled) teled = 1;
            if (IsSped) sped = 1;

            try
            {
                sql = string.Format(sql, reged, teled, teling, sped, ID, add);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Update.4]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
		}

		/// <summary>
		///  ���ٿ�������
		///  IsReged:�Ƿ������˹Һ�,IsTeling:�Ƿ�������ԤԼ,IsTeled:�Ƿ�����Һ�
		///  IsSped:�Ƿ�����Һ�
		/// </summary>
		/// <param name="ID"></param>
		/// <param name="IsReged"></param>
		/// <param name="IsTeling"></param>
		/// <param name="IsTeled"></param>
		/// <param name="IsSped"></param>
		/// <returns></returns>
		public int Reduce(string ID,bool IsReged,bool IsTeling, bool IsTeled,bool IsSped)
		{
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Schema.Update.1", ref sql) == -1) return -1;

            int reged = 0, teling = 0, teled = 0, sped = 0;

            if (IsReged) reged = -1;
            if (IsTeling) teling = -1;
            if (IsTeled) teled = -1;
            if (IsSped) sped = -1;

            try
            {
                sql = string.Format(sql, reged, teled, teling, sped, ID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Update.1]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
		}
        /// <summary>
        ///  ���ٿ�������
        ///  IsReged:�Ƿ������˹Һ�,IsTeling:�Ƿ�������ԤԼ,IsTeled:�Ƿ�����Һ�
        ///  IsSped:�Ƿ�����Һ�
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="IsReged"></param>
        /// <param name="IsTeling"></param>
        /// <param name="IsTeled"></param>
        /// <param name="IsSped"></param>
        /// <returns></returns>
        public int AddLimit(string ID, bool IsReged, bool IsTeling, bool IsTeled, bool IsSped)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Registration.Schema.Update.1", ref sql) == -1) return -1;

            int reged = 0, teling = 0, teled = 0, sped = 0;

            if (IsReged) reged = 1;
            if (IsTeling) teling = 1;
            if (IsTeled) teled = 1;
            if (IsSped) sped = 1;

            try
            {
                sql = string.Format(sql, reged, teled, teling, sped, ID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Update.1]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }
		#endregion

		#region ��ѯ

        /// <summary>
        /// ��ѯ��ǰʱ���ҽ�����Ű���Ϣ
        /// {231D1A80-6BF6-413f-8BBF-727DC2BF47D9}
        /// </summary>
        /// <param name="doctCode"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Schema GetSchema(string doctCode, DateTime time)
        {
            ArrayList al = QuerySchema("Fee.OutPatient.GetSchemaByDoctAndTime", time.ToString(), doctCode);
            if (al == null || al.Count == 0)
            {
                return null;
            }
            return al[0] as FS.HISFC.Models.Registration.Schema;
        }

        /// <summary>
        /// ������ѯ
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QuerySchema(string whereSql, params string[] args)
        {
            string strSql = string.Empty;
            int returnValue = this.Sql.GetCommonSql("Registration.Schema.Query.1", ref strSql);
            if (returnValue < 0)
            {
                this.Err = "û���ҵ�[Registration.Schema.Query.1]��Ӧ��sql���";
                return null;
            }
            returnValue = this.Sql.GetCommonSql(whereSql, ref whereSql);
            if (returnValue < 0)
            {
                this.Err = "û���ҵ�[" + whereSql + "]��Ӧ��sql���";
                return null;
            }

            try
            {
                whereSql = string.Format(whereSql, args);
            }
            catch (Exception ex)
            {

                this.Err = "[" + whereSql + "]��ʽ��ƥ��";
                return null;
            }

            strSql = strSql + " " + whereSql;

            return this.QueryBase(strSql);
        }

		/// <summary>
		/// 
		/// </summary>
		protected FS.HISFC.Models.Registration.Schema objSchema ;
		private ArrayList al ;

		/// <summary>
		/// ���ݳ���ʱ�䡢�Ű����ͼ����Ű���Ϣ
		/// </summary>
		/// <param name="seeDate"></param>
		/// <param name="type"></param>
		/// <param name="deptID"></param>
		/// <returns></returns>
		public ArrayList Query(DateTime seeDate,FS.HISFC.Models.Base.EnumSchemaType type,string deptID)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.Schema.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.Schema.Query.2",ref where ) == -1)return null;

			sql = sql + " " + where;

			try
			{
				sql = string.Format(sql,seeDate.ToString(),(int)type,deptID);
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Schema.Query.2]��ʽ��ƥ��!"+e.Message;
				this.ErrCode = e.Message;
				return null;
			}
			return this.QueryBase(sql);
		}

		#region ѡ��ҽ�������б�ʹ��
        
		/// <summary>
		/// ���ݳ���ʱ�䡢���ҡ������Ű���Ϣ
		/// </summary>
		/// <param name="seeDate"></param>
		/// <param name="deptID"></param>		
		/// <returns></returns>
		public ArrayList QueryByDept(DateTime seeDate,string deptID)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.Schema.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.Schema.Query.3",ref where ) == -1)return null;

			sql = sql + " " + where;

			try
			{
				sql = string.Format(sql,seeDate.Date.ToString(),deptID);
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Schema.Query.3]��ʽ��ƥ��!"+e.Message;
				this.ErrCode = e.Message;
				return null;
			}
			return this.QueryBase(sql);
		}

        /// <summary>
        /// ���ݳ���ʱ�䡢���ҡ������Ű���Ϣ
        /// </summary>
        /// <param name="seeDate"></param>
        /// <param name="deptID"></param>		
        /// <returns></returns>
        public ArrayList QueryByDeptAll(DateTime seeDate, string deptID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Schema.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Schema.Query.ByDept", ref where) == -1) return null;

            sql = sql + " " + where;

            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), deptID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Query.ByDept]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return this.QueryBase(sql);
        }


        /// <summary>
        /// ���ݳ���ʱ�䡢���ҡ������Ű���Ϣ
        /// </summary>
        /// <param name="seeDate"></param>
        /// <param name="deptID"></param>		
        /// <returns></returns>
        public ArrayList QueryByDept(DateTime seeDate, string deptID,string regLevelID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Schema.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Schema.QueryDept.ByDeptAndRegLevel", ref where) == -1)
            {
                //{DF90A779-384E-4334-81FD-D1F5D5BCF47C}
                where = @"  WHERE see_date = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                AND (dept_code = '{1}' or 'ALL'='{2}')
                                AND (schema_type = '0' or (schema_type='1' and (reglevl_code='{2}' or 'ALL'='{2}') ))
                                AND valid_flag = '1'
                              ORDER BY noon_code,append_flag,begin_time 
                             ";
            }

            sql = sql + " " + where;

            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), deptID, regLevelID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.QueryDept.ByDeptAndRegLevel]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return this.QueryBase(sql);
        }

		/// <summary>
		/// ���ݳ���ʱ�䡢ҽ��������ҽ���Ű���Ϣ
        /// �ʺϹҺŲ�ѡ�����,ֱ��ѡ��ҽ��,һ��ҽ���ɳ���������
		/// </summary>
		/// <param name="seeDate"></param>
		/// <param name="doctID"></param>
		/// <returns></returns>
		public ArrayList QueryByDoct(DateTime seeDate,string doctID)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.Schema.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.Schema.Query.4",ref where ) == -1)return null;

			sql = sql + " " + where;

			try
			{
				sql = string.Format(sql,seeDate.Date.ToString(),doctID);
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Schema.Query.4]��ʽ��ƥ��!"+e.Message;
				this.ErrCode = e.Message;
				return null;
			}
			return this.QueryBase(sql);
		}

        /// <summary>
        /// ���ݳ���ʱ�䡢���ҡ�ҽ��������ҽ���Ű���Ϣ
        /// �ʺϹҺ�ʱ��ѡ�����,��ѡ��ҽ��,һ��ҽ���ɳ���������,
        /// ֻ��ʾĳһ��������ҵ��Ű���Ϣ
        /// </summary>
        /// <param name="seeDate"></param>
        /// <param name="deptID"></param>
        /// <param name="doctID"></param>
        /// <returns></returns>
        public ArrayList QueryByDoct(DateTime seeDate, string deptID, string doctID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Schema.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Schema.Query.14", ref where) == -1) return null;

            sql = sql + " " + where;

            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), doctID, deptID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Query.14]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return this.QueryBase(sql);
        }
        
		/// <summary>
		/// �����Ӻ��Ű���Ϣʵ��
		/// </summary>
		/// <param name="seeDate"></param>
		/// <param name="deptID"></param>
		/// <param name="doctID"></param>
		/// <param name="noonID"></param>		
		/// <returns></returns>
		public FS.HISFC.Models.Registration.Schema QueryAppend(DateTime seeDate, string deptID, string doctID, string noonID)
		{
			string sql = "",where = "";

			if(this.Sql.GetCommonSql("Registration.Schema.Query.1",ref sql) == -1)return null;
			if(this.Sql.GetCommonSql("Registration.Schema.Query.5",ref where ) == -1)return null;

			sql = sql + " " + where;

			try
			{
				sql = string.Format(sql,seeDate.ToString("yyyy-MM-dd"),deptID,doctID,noonID	) ;
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Schema.Query.5]��ʽ��ƥ��!"+e.Message;
				this.ErrCode = e.Message;
				return null;
			}

			if(this.QueryBase(sql) == null) return null ;

			if(al == null)return null ;
			if(al.Count == 0) return new FS.HISFC.Models.Registration.Schema() ;

			return (FS.HISFC.Models.Registration.Schema)al[0] ;
		}      
		#endregion

		/// <summary>
		/// ��ѯר���ճ������/����ר�ҵĿ������
		/// </summary>
		/// <param name="seeDate"></param>
		/// <param name="endDate"></param>
		/// <param name="type"></param>
		/// <returns></returns>
		public DataSet QueryDept( DateTime seeDate,DateTime endDate, FS.HISFC.Models.Base.EnumSchemaType type)
		{
			string sql = "" ;
			DataSet ds = new DataSet() ;
 
			if(type == FS.HISFC.Models.Base.EnumSchemaType.Dept)
			{
				if(this.Sql.GetCommonSql("Registration.Schema.Query.6",ref sql) == -1) return null ;
				
				try
				{
					sql = string.Format(sql,seeDate.Date.ToString(), endDate.ToString()) ;
				}
				catch(Exception e)
				{
					this.Err = "[Registration.Schema.Query.6]��ʽ��ƥ��!"+e.Message;
					this.ErrCode = e.Message;
					return null ;
				}

				if(this.ExecQuery(sql, ref ds) == -1) return null; 

				return ds ;
			}
			else 
			{
				if(this.Sql.GetCommonSql("Registration.Schema.Query.7",ref sql) == -1) return null ;
				
				try
				{
					sql = string.Format(sql,seeDate.Date.ToString(), endDate.ToString()) ;
				}
				catch(Exception e)
				{
					this.Err = "[Registration.Schema.Query.7]��ʽ��ƥ��!"+e.Message;
					this.ErrCode = e.Message;
					return null ;
				}

				if(this.ExecQuery(sql, ref ds) == -1) return null; 

				return ds ;
			}
		}


        /// <summary>
        /// ��ѯר���ճ������/����ר�ҵĿ������
        /// </summary>
        /// <param name="seeDate"></param>
        /// <param name="endDate"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DataSet QueryDept(DateTime seeDate, DateTime endDate, FS.HISFC.Models.Base.EnumSchemaType type,string reglevelID)
        {
            string sql = "";
            DataSet ds = new DataSet();

            if (type == FS.HISFC.Models.Base.EnumSchemaType.Dept)
            {
                if (this.Sql.GetCommonSql("Registration.Schema.QueryDept.ByRegLevel", ref sql) == -1)
                {
                    sql = @"       SELECT 
                                              a.dept_code,
                                               a.dept_name,
                                               a.noon_code,
                                               a.begin_time,
                                               a.end_time,
                                               a.reg_lmt,
                                               a.reged,
                                               a.tel_lmt,
                                               a.tel_reged,
                                               a.append_flag,
                                               b.spell_code,
                                               b.wb_code,
                                               b.user_code
                                          FROM fin_opr_schema a, com_department b
                                         WHERE a.dept_code = b.dept_code
                                           AND a.see_date = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                           AND a.end_time >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                           AND (a.reglevl_code = '{2}' or 'ALL'='{2}')
                                           AND a.valid_flag = '1'
                                         ORDER BY a.dept_code, a.noon_code, a.append_flag, a.begin_time ";
                }

                try
                {
                    sql = string.Format(sql, seeDate.Date.ToString(), endDate.ToString(), reglevelID);
                }
                catch (Exception e)
                {
                    this.Err = "[Registration.Schema.QueryDept.ByRegLevel]��ʽ��ƥ��!" + e.Message;
                    this.ErrCode = e.Message;
                    return null;
                }

                if (this.ExecQuery(sql, ref ds) == -1) return null;

                return ds;
            }
            else
            {
                if (this.Sql.GetCommonSql("Registration.Schema.QueryDoct.ByRegLevel", ref sql) == -1)
                {
                    sql = @"SELECT DISTINCT a.dept_code,
                                                                   a.dept_name,       
                                                                   b.spell_code,
                                                                   b.wb_code,
                                                                   b.user_code
                                                              FROM fin_opr_schema a, com_department b
                                                             WHERE a.dept_code = b.dept_code
                                                               AND a.see_date = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                                               AND a.end_time >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                                               AND a.schema_type = '1'
                                                               and a.reglevl_code='{2}'
                                                               AND a.valid_flag = '1'";
                }

                try
                {
                    sql = string.Format(sql, seeDate.Date.ToString(), endDate.ToString(),reglevelID);
                }
                catch (Exception e)
                {
                    this.Err = "[Registration.Schema.Query.7]��ʽ��ƥ��!" + e.Message;
                    this.ErrCode = e.Message;
                    return null;
                }

                if (this.ExecQuery(sql, ref ds) == -1) return null;

                return ds;
            }
        }

		/// <summary>
		/// ��ѯĳ��ȫ������ר��
		/// </summary>
		/// <param name="seeDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public DataSet QueryDoct( DateTime seeDate,DateTime endDate )
		{
			string sql = "" ;
			DataSet ds = new DataSet() ;

			if(this.Sql.GetCommonSql("Registration.Schema.Query.8",ref sql) == -1) return null ;
				
			try
			{
				sql = string.Format(sql,seeDate.Date.ToString(), endDate.ToString()) ;
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Schema.Query.8]��ʽ��ƥ��!"+e.Message;
				this.ErrCode = e.Message;
				return null ;
			}

			if(this.ExecQuery(sql, ref ds) == -1) return null; 

			return ds ;
		}


        /// <summary>
        /// ��ѯĳ��ȫ������ר��
        /// </summary>
        /// <param name="seeDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DataSet QueryDoctForLED(DateTime seeDate, DateTime endDate)
        {
            string sql = "";
            DataSet ds = new DataSet();

            if (this.Sql.GetCommonSql("Registration.Schema.Query.99", ref sql) == -1) return null;

            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), endDate.ToString());
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Query.99]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            if (this.ExecQuery(sql, ref ds) == -1) return null;

            return ds;
        }
		/// <summary>
		/// ��ѯĳ�ա�ĳ���ҳ���ר��
		/// </summary>
		/// <param name="seeDate"></param>
		/// <param name="endDate"></param>
		/// <param name="deptID"></param>
		/// <returns></returns>
		public DataSet QueryDoct(DateTime seeDate, DateTime endDate, string deptID)
		{
			string sql = "" ;
			DataSet ds = new DataSet() ;

			if(this.Sql.GetCommonSql("Registration.Schema.Query.9",ref sql) == -1) return null ;
				
			try
			{
				sql = string.Format(sql,seeDate.Date.ToString(), endDate.ToString(), deptID) ;
			}
			catch(Exception e)
			{
				this.Err = "[Registration.Schema.Query.9]��ʽ��ƥ��!"+e.Message;
				this.ErrCode = e.Message;
				return null ;
			}

			if(this.ExecQuery(sql, ref ds) == -1) return null; 

			return ds ;
		}

        /// <summary>
        /// ��ѯĳ�ա�ĳ���ҳ���ר��
        /// </summary>
        /// <param name="seeDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public DataSet QueryDoct(DateTime seeDate, DateTime endDate, string deptID, string levelID)
        {
            string sql = "";
            DataSet ds = new DataSet();

            if (this.Sql.GetCommonSql("Registration.Schema.Query.ByDeptAndLevel", ref sql) == -1)
            {
                sql = @"SELECT a.doct_code,
                                           a.doct_name,
                                           a.noon_code,
                                           a.begin_time,
                                           a.end_time,
                                           a.reg_lmt,
                                           a.reged,
                                           a.tel_lmt,
                                           a.tel_reged,
                                           a.spe_lmt,
                                           a.spe_reged,
                                           a.append_flag,
                                           b.spell_code,
                                           b.wb_code,
                                           a.remark,
                                           decode(b.levl_code,'17','1','2','1','21','1','33','1','0')
                                      FROM fin_opr_schema a, com_employee b
                                     WHERE a.doct_code = b.empl_code
                                       AND a.see_date = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                       AND a.end_time >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                       AND a.dept_code= '{2}'
                                       and a.reglevl_code='{3}'
                                       AND a.schema_type = '1'
                                       AND a.valid_flag = '1'";
            }

            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), endDate.ToString(), deptID, levelID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Query.ByDeptAndLevel]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            if (this.ExecQuery(sql, ref ds) == -1)
                return null;

            return ds;
        }

        /// <summary>
        /// ��ѯĳ�ա�ĳ���ҳ���ר��
        /// </summary>
        /// <param name="seeDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public DataSet QueryDoctByRegLevel(DateTime seeDate, DateTime endDate, string levelID)
        {
            string sql = "";
            DataSet ds = new DataSet();

            if (this.Sql.GetCommonSql("Registration.Schema.Query.ByLevel", ref sql) == -1)
            {
                sql = @"SELECT  a.doct_code,
                                               a.doct_name,
                                               a.noon_code,
                                               a.begin_time,
                                               a.end_time,
                                               a.reg_lmt,
                                               a.reged,
                                               a.tel_lmt,
                                               a.tel_reged,
                                               a.spe_lmt,
                                               a.spe_reged,
                                               a.append_flag,
                                               b.spell_code,
                                               b.wb_code,
                                               a.remark,
                                               a.reglevl_code
                                      FROM fin_opr_schema a, com_employee b,fin_opr_noon c
                                     WHERE a.doct_code = b.empl_code
                                       AND a.see_date = to_date('{0}','yyyy-mm-dd hh24:mi:ss')
                                       AND a.end_time >= to_date('{1}','yyyy-mm-dd hh24:mi:ss')
                                       AND a.schema_type = '1'  
                                       AND a.valid_flag = '1'  
                                       and a.reglevl_code='{2}'
                                       and a.noon_code = c.noon_code
                                      order by a.see_date,a.dept_code,a.doct_code,a.noon_code,a.begin_time";
            }

            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), endDate.ToString(), levelID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Query.ByLevel]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            if (this.ExecQuery(sql, ref ds) == -1)
                return null;

            return ds;
        }


        /// <summary>
        /// �����Ű���Ż�ȡ�Ű�ʵ��
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Registration.Schema GetByID(string ID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Registration.Schema.Query.1", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Registration.Schema.Query.11", ref where) == -1) return null;

            sql = sql + " " + where;

            try
            {
                sql = string.Format(sql, ID);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Query.11]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            if (this.QueryBase(sql) == null) return null;

            if (al == null) return null;

            if (al.Count == 0) return new FS.HISFC.Models.Registration.Schema();

            return (FS.HISFC.Models.Registration.Schema)al[0];
        }

		/// <summary>
		/// ��ѯ
		/// </summary>
		/// <param name="sql"></param>
		/// <returns></returns>
		public ArrayList QueryBase(string sql)
		{
			if(this.ExecQuery(sql) == -1) return null;

			this.al = new ArrayList();

			try
			{
				while(this.Reader.Read())
				{
					this.objSchema = new FS.HISFC.Models.Registration.Schema();
					
					this.objSchema.Templet.ID = this.Reader[2].ToString();
					this.objSchema.Templet.EnumSchemaType = (FS.HISFC.Models.Base.EnumSchemaType)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString()));
					this.objSchema.SeeDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4].ToString());
					this.objSchema.Templet.Week = (DayOfWeek)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString()));
					this.objSchema.Templet.Noon.ID = this.Reader[6].ToString();
					this.objSchema.Templet.Dept.ID = this.Reader[7].ToString();
					this.objSchema.Templet.Dept.Name = this.Reader[8].ToString();
					this.objSchema.Templet.Doct.ID = this.Reader[9].ToString();
					this.objSchema.Templet.Doct.Name = this.Reader[10].ToString();
					this.objSchema.Templet.DoctType.ID = this.Reader[11].ToString();					
					this.objSchema.Templet.RegQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12].ToString());
					this.objSchema.RegedQTY = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13].ToString());
					this.objSchema.Templet.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[14].ToString());
					this.objSchema.Templet.StopReason.ID = this.Reader[15].ToString();
					this.objSchema.Templet.StopReason.Name = this.Reader[16].ToString();
					this.objSchema.Templet.Stop.ID = this.Reader[17].ToString();
					this.objSchema.Templet.Stop.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
					this.objSchema.Templet.Memo = this.Reader[19].ToString();
					this.objSchema.Templet.Oper.ID = this.Reader[20].ToString();
					this.objSchema.Templet.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[21].ToString());
					this.objSchema.Templet.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[22].ToString()) ;
					this.objSchema.Templet.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[23].ToString()) ;
					this.objSchema.Templet.TelQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24].ToString()) ;
					this.objSchema.TeledQTY = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[25].ToString()) ;
					this.objSchema.TelingQTY = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString()) ;
					this.objSchema.Templet.SpeQuota = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[27].ToString()) ;
					this.objSchema.SpedQTY = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[28].ToString()) ;
					this.objSchema.Templet.IsAppend = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[29].ToString()) ;
                    this.objSchema.SeeNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[30].ToString());
                    this.objSchema.Templet.RegLevel.ID = this.Reader[31].ToString();
                    this.objSchema.Templet.RegLevel.Name = this.Reader[32].ToString();

                    this.objSchema.Templet.Room.ID = this.Reader[33].ToString();
                    this.objSchema.Templet.Room.Name = this.Reader[34].ToString(); 
                    this.objSchema.Templet.Console.ID = this.Reader[35].ToString();
                    this.objSchema.Templet.Console.Name = this.Reader[36].ToString();

					this.al.Add(this.objSchema);
				}

				this.Reader.Close();
			}
			catch(Exception e)
			{
				this.Err = "��ѯ�Ű���Ϣ����!" + e.Message;
				this.ErrCode = e.Message;
				return null;
			}

			return al;
		}


		/// <summary>
		/// ��ȡ�������Ч�Ű���Ϣ
		/// </summary>
		/// <param name="schemaType"></param>		
		/// <param name="current"></param>
		/// <param name="deptID"></param>
		/// <param name="doctID"></param>
		/// <returns></returns>
		public FS.HISFC.Models.Registration.Schema Query(FS.HISFC.Models.Base.EnumSchemaType schemaType,
				DateTime current, string deptID, string doctID)
		{
			#region ������� ̫����Ľ�����,��дsql�ˣ� ����ǰҹ
			/*
			//ԤԼר��
			WHERE see_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
			AND schema_type = '1'
			AND doct_code = '{1}'
			AND valid_flag = '1'
			AND (tel_lmt > tel_reging OR append_flag = '1')
			ORDER BY noon_code,append_flag,begin_time

			//ԤԼר��
			WHERE see_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
			AND schema_type = '0'
			AND dept_code = '{1}'
			AND valid_flag = '1'
			AND (tel_lmt > tel_reging OR append_flag = '1')
			ORDER BY noon_code,append_flag,begin_time

			//ר��
			WHERE see_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
			AND schema_type = '0'
			AND dept_code = '{1}'
			AND valid_flag = '1'
			AND (reg_lmt > reged OR append_flag = '1')
			ORDER BY noon_code,append_flag,begin_time

			//ר��
			WHERE see_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
			AND schema_type = '1'
			AND doct_code = '{1}'
			AND valid_flag = '1'
			AND (reg_lmt > reged OR append_flag = '1')
			ORDER BY noon_code,append_flag,begin_time

			//����
			WHERE see_date >= to_date('{0}','yyyy-mm-dd hh24:mi:ss')
			AND schema_type = '0'
			AND doct_code = '{1}'
			AND valid_flag = '1'
			AND (spe_lmt > spe_reged OR append_flag = '1')
			ORDER BY noon_code,append_flag,begin_time*/
			#endregion

			string where = " WHERE see_date >= to_date('"+ current.Date.ToString() + "','yyyy-mm-dd hh24:mi:ss')" ;

			//�Ű�����
			where = where +" AND schema_type = '" + ((int)schemaType).ToString() + "'" ;
			
			
			if( schemaType == FS.HISFC.Models.Base.EnumSchemaType.Dept)
			{
				where = where + " AND dept_code = '" + deptID + "'" +
					" AND end_time > to_date('"+current.ToString() + "','yyyy-mm-dd hh24:mi:ss')" +
					" AND valid_flag = '1'" +
					" ORDER BY see_date,noon_code,append_flag,begin_time" ;
			}
			else
			{
				//ԤԼ��ר��
				where  = where + " AND doct_code = '" + doctID + "'" +
					" AND end_time > to_date('"+current.ToString() + "','yyyy-mm-dd hh24:mi:ss')" +
					" AND valid_flag = '1'" +
					" ORDER BY see_date,noon_code,append_flag,begin_time" ;
			}			

			string sql = "" ;

			if(this.Sql.GetCommonSql("Registration.Schema.Query.1",ref sql) == -1)return null;

			sql = sql + where ;

			if(this.QueryBase(sql) == null) return null ;

			if(al == null)return null ;

			if(al.Count == 0) return new FS.HISFC.Models.Registration.Schema() ;

			return (FS.HISFC.Models.Registration.Schema)al[0] ;
		}

        /// <summary>
        /// ����ģ��id����end_timeС�ڵ�ǰʱ��ļ�¼��
        /// by niuxy
        /// 2007-05-16
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int QuerySchemaById(string ID)
        {
            string strsql = "";
            int returnValue = this.Sql.GetCommonSql("Registration.Schema.Query.98", ref strsql);
            if (returnValue == -1)
            {
                this.Err = "�������Ϊ[Registration.Schema.Query.98]��sql���ʧ��";
                return -1;
            }
            try
            {
                strsql = string.Format(strsql, ID);
            }
            catch (Exception ex)
            {
                this.Err = "[Registration.Schema.Query.98]��ʽ��ƥ��!" + ex.Message;
                this.ErrCode = ex.Message;
                return -1;    
                
            }

            return  FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(strsql));
           
        }

        /// <summary>
        /// Registration.SchemaWeek.1
        /// </summary>
        /// <returns></returns>
        public DataSet  QuerySchemaForRegister(string fromDate,string toDate,string schemaType,string deptCode,string doctCode )
        {

            DataSet ds = new DataSet ();
            this.ExecQuery("Registration.SchemaForRegister.1", ref ds, fromDate, toDate, schemaType, deptCode, doctCode);
            return ds;

        }

        /// <summary>
        /// ��ѯĳ�ա�ĳ���ҳ���ר��(����������)
        /// </summary>
        /// <param name="seeDate"></param>
        /// <param name="endDate"></param>
        /// <param name="deptID"></param>
        /// <returns></returns>
        public DataSet QueryDoct(DateTime seeDate, DateTime endDate, string deptID, DateTime seeDateEnd, string noon)
        {
            string sql = "";
            DataSet ds = new DataSet();

            if (this.Sql.GetCommonSql("Registration.Schema.Query.88", ref sql) == -1) return null;

            try
            {
                sql = string.Format(sql, seeDate.Date.ToString(), endDate.ToString(), deptID, seeDateEnd.Date.ToString(), noon);
            }
            catch (Exception e)
            {
                this.Err = "[Registration.Schema.Query.88]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            if (this.ExecQuery(sql, ref ds) == -1) return null;

            return ds;
        }
		#endregion

	}
}
