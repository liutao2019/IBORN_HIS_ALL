using System;
using System.Collections;

namespace FS.HISFC.BizLogic.Nurse
{
    /// <summary>
    /// �Ű�ģ�������
    /// </summary>
    public class WorkTemplet : FS.FrameWork.Management.Database
    {
        public WorkTemplet()
        {
        }
        #region ����
        /// <summary>
		/// �Ǽ�һ����ʿ�Ű�ģ��
		/// </summary>
		/// <param name="templet"></param>
		/// <returns></returns>
		public int Insert(FS.HISFC.Models.Nurse.WorkTemplet templet)
		{
			string sql = "";

			if(this.Sql.GetCommonSql("Nurse.WorkTemplet.Insert",ref sql) == -1)return -1;

            #region SQL
     //       INSERT INTO met_nui_worktemplet   --�Ű�ģ���
     //     ( 
     //       id,   --���
     //       week,   --����
     //       nrs_cell_code, --��ʿվ���
     //       cell_name, --��ʿվ����
     //       dept_code,   --���Һ�
     //       dept_name,   --��������
     //       nrs_code,   --ҽ������
     //       nrs_name,   --ҽ������
     //       noon_code,   --���
     //       noon_name,    --�������
     //       valid_flag,   --0��Ч/1��Ч
     //       remark,   --��ע
     //       oper_code,   --����Ա����
     //       oper_date,  --����䶯����
     //       nrs_type,  --��ʿ���
     //       begin_time, --��ʼʱ��
     //       end_time,	--����ʱ��
     //         reason_no, --ԭ��
     //         reason_name --ԭ������
     //       )
     //VALUES 
     //     (  
     //       '{0}',   --���
     //       '{1}',   --����
     //       '{2}',   --��ʿվ���
     //       '{3}',   --��ʿվ����
     //       '{4}',   --���Һ�
     //       '{5}',   --��������
     //       '{6}',   --��ʿ����
     //       '{7}',   --��ʿ����
     //       '{8}',   --���
     //       '{9}',   --�������
     //       '{10}',   --0��Ч/1��Ч
     //       '{11}',   --��ע
     //       '{12}',  --����Ա����
     //       to_date('{13}','yyyy-mm-dd hh24:mi:ss'), --����䶯����
     //       '{14}',   --��ʿ���
     //       to_date('{15}','yyyy-mm-dd hh24:mi:ss'), --��ʼʱ��
     //       to_date('{16}','yyyy-mm-dd hh24:mi:ss'), --����ʱ��
     //       '{17}',  --ԭ��
     //       '{18}',  --ԭ������ )
            #endregion
			try
			{
                sql = string.Format(sql, templet.ID, (int)templet.Week, templet.NurseCell.ID, templet.NurseCell.Name,
                    templet.Dept.ID, templet.Dept.Name, templet.Employee.ID, templet.Employee.Name, templet.Noon.ID,
                    templet.Noon.Name, FS.FrameWork.Function.NConvert.ToInt32(templet.IsValid),
                    templet.Memo, templet.Oper.ID, templet.Oper.OperTime, templet.EmplType.ID,
                    templet.Begin.ToString(), templet.End.ToString(), templet.Reason.ID, templet.Reason.Name);
            }
			catch(Exception e)
			{
                this.Err = "[Nurse.WorkTemplet.Insert]��ʽ��ƥ��!" + e.Message;
				this.ErrCode = e.Message;
				return -1;
			}
			return this.ExecNoQuery(sql);
        }
        #endregion

        #region ɾ��
        /// <summary>
        /// ����IDɾ��һ����ʿģ���¼
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Delete(string ID)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.WorkTemplet.Delete", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, ID);
            }
            catch (Exception e)
            {
                this.Err = "[Nurse.WorkTemplet.Delete]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return -1;
            }

            return this.ExecNoQuery(sql);
        }
        #endregion

        #region ��ѯ
        /// <summary>
        /// �������ڡ����Ҳ�ѯ�Ű�ģ��
        /// </summary>
        /// <param name="schemaType"></param>
        /// <param name="week"></param>
        /// <param name="DeptID"></param>
        /// <returns></returns>
        public ArrayList Query(DayOfWeek week, string DeptID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.WorkTemplet.Query", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Nurse.WorkTemplet.Query.ByWeek&Dept", ref where) == -1) return null;

            sql = sql + " " + where;

            try
            {
                sql = string.Format(sql,(int)week, DeptID);
            }
            catch (Exception e)
            {
                this.Err = "[Nurse.WorkTemplet.Query.ByWeek&Dept]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return this.QueryBase(sql);
        }

        /// <summary>
        /// �������ڲ�ѯ�Ƿ���Чģ����Ϣ��
        /// ģ�����
        /// </summary>
        /// <param name="schemaType"></param>
        /// <param name="week"></param>
        /// <param name="IsValid"></param>
        /// <returns></returns>
        public ArrayList Query(DayOfWeek week, bool IsValid)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.WorkTemplet.Query", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Nurse.WorkTemplet.Query.ByWeek&VaildState", ref where) == -1) return null;

            sql = sql + " " + where;

            try
            {
                sql = string.Format(sql, (int)week, FS.FrameWork.Function.NConvert.ToInt32(IsValid));
            }
            catch (Exception e)
            {
                this.Err = "[Nurse.WorkTemplet.Query.ByWeek&VaildState]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return this.QueryBase(sql);
        }

        /// <summary>
        /// ģ��ʵ��
        /// </summary>
        protected FS.HISFC.Models.Nurse.WorkTemplet workTemplet;

        protected ArrayList al;
        /// <summary>
        /// ��ѯ
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private ArrayList QueryBase(string sql)
        {
            if (this.ExecQuery(sql) == -1) return null;

            this.al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    this.workTemplet = new FS.HISFC.Models.Nurse.WorkTemplet();

                    this.workTemplet.ID = this.Reader[2].ToString();
                    this.workTemplet.Week = (DayOfWeek)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString()));
                    this.workTemplet.NurseCell.ID = this.Reader[4].ToString();
                    this.workTemplet.NurseCell.Name = this.Reader[5].ToString();
                    this.workTemplet.Dept.ID = this.Reader[6].ToString();
                    this.workTemplet.Dept.Name = this.Reader[7].ToString();
                    this.workTemplet.Employee.ID = this.Reader[8].ToString();
                    this.workTemplet.Employee.Name = this.Reader[9].ToString();
                    this.workTemplet.Noon.ID = this.Reader[10].ToString();
                    this.workTemplet.Noon.Name = this.Reader[11].ToString();
                    this.workTemplet.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[12].ToString());
                    this.workTemplet.Memo = this.Reader[13].ToString();
                    this.workTemplet.Oper.ID = this.Reader[14].ToString();
                    this.workTemplet.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[15].ToString());
                    this.workTemplet.EmplType.ID = this.Reader[16].ToString();
                    this.workTemplet.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[17].ToString());
                    this.workTemplet.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
                    this.workTemplet.Reason.ID = this.Reader[19].ToString();
                    this.workTemplet.Reason.Name = this.Reader[20].ToString();

                    this.al.Add(this.workTemplet);
                }

                this.Reader.Close();
            }
            catch (Exception e)
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
