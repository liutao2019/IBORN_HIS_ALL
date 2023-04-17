using System;
using System.Collections;
using System.Data;

namespace FS.HISFC.BizLogic.Nurse
{
   /// <summary>
   /// �Ű������
   /// </summary>
    public class Work : FS.FrameWork.Management.Database
    {
        public Work()
        {

        }
        #region ����
        /// <summary>
        /// ����һ���Ű��¼
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Nurse.Work work)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Work.Insert", ref sql) == -1) return -1;
 
            try
            {
                #region SQL
     //          INSERT INTO met_nui_work  --�Ű�ģ���
     //     ( 
     //       id,   --���
     //       week,   --����
     //       nrs_date, --��ʿ�Ű�����
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
     //       '{2}',   --��ʿ�Ű�����
     //       '{3}',   --��ʿվ���
     //       '{4}',   --��ʿվ����
     //       '{5}',   --���Һ�
     //       '{6}',   --��������
     //       '{7}',   --��ʿ����
     //       '{8}',   --��ʿ����
     //       '{9}',   --���
     //       '{10}',   --�������
     //       '{11}',   --0��Ч/1��Ч
     //       '{12}',   --��ע
     //       '{13}',  --����Ա����
     //       to_date('{14}','yyyy-mm-dd hh24:mi:ss'), --����䶯����
     //       '{15}',   --��ʿ���
     //       to_date('{16}','yyyy-mm-dd hh24:mi:ss'), --��ʼʱ��
     //       to_date('{17}','yyyy-mm-dd hh24:mi:ss'), --����ʱ��
     //       '{18}',  --ԭ��
     //       '{19}' --ԭ������)
                #endregion
                sql = string.Format(sql, work.Templet.ID, (int)work.Templet.Week, work.WorkDate.ToString(),
                    work.Templet.NurseCell.ID.ToString(), work.Templet.NurseCell.Name.ToString(), work.Templet.Dept.ID,
                    work.Templet.Dept.Name, work.Templet.Employee.ID, work.Templet.Employee.Name, work.Templet.Noon.ID,
                    work.Templet.Noon.Name, FS.FrameWork.Function.NConvert.ToInt32(work.Templet.IsValid.ToString()),
                    work.Templet.Memo, work.Templet.Oper.ID, work.Templet.Oper.OperTime, work.Templet.EmplType.ID,
                    work.Templet.Begin.ToString(), work.Templet.End.ToString(), work.Templet.Reason.ID,
                    work.Templet.Reason.Name);
            }
            catch (Exception e)
            {
                this.Err = "[Nurse.Work.Insert]��ʽ��ƥ��!" + e.Message;
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

            if (this.Sql.GetCommonSql("Nurse.Work.Delete", ref sql) == -1) return -1;

            try
            {
                sql = string.Format(sql, ID);
            }
            catch (Exception e)
            {
                this.Err = "[Nurse.Work.Delete]��ʽ��ƥ��!" + e.Message;
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
        public int Update(FS.HISFC.Models.Nurse.Work work)
        {
            string sql = "";

            if (this.Sql.GetCommonSql("Nurse.Work.Update.ById", ref sql) == -1) return -1;
            #region SQL
//       update met_nui_work
//     SET
//       valid_flag='{0}',   --1����/0ͣ��
//       noon_code='{1}',--���
//       noon_name='{2}',--�������
//       begin_time='{3}',--��ʼʱ��
//       end_tiem='{4}',--����ʱ��
//       reason_no='{5}',   --ԭ��
//       reason_name='{6}',   --ԭ������
//       oper_code='{7}',   --����Ա
//       oper_date=to_date('{8}','yyyy-mm-dd hh24:mi:ss'),    --����Ķ�����
//where id='{9}'
            #endregion
            try
            {
                sql = string.Format(sql, FS.FrameWork.Function.NConvert.ToInt32(work.Templet.IsValid), work.Templet.Noon.ID,
                    work.Templet.Noon.Name, work.Templet.Begin.ToString(), work.Templet.End.ToString(),
                    work.Templet.Reason.ID, work.Templet.Reason.Name, work.Templet.Oper.ID,
                    work.Templet.Oper.OperTime.ToString(), work.Templet.ID);
            }
            catch (Exception e)
            {
                this.Err = "[Nurse.Work.Update.ById]��ʽ��ƥ��!" + e.Message;
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
        public ArrayList Query(DateTime day, string DeptID)
        {
            string sql = "", where = "";

            if (this.Sql.GetCommonSql("Nurse.Work.Query", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Nurse.Work.Query.ByDay&Dept", ref where) == -1) return null;

            sql = sql + " " + where;

            try
            {
                sql = string.Format(sql, day, DeptID);
            }
            catch (Exception e)
            {
                this.Err = "[Nurse.Work.Query.ByDay&Dept]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return this.QueryBase(sql);
        }

        public ArrayList QueryHistory(int week, string DeptID)
        {
            string sql = string.Empty;
            string where = string.Empty;

            if (this.Sql.GetCommonSql("Nurse.Work.Query", ref sql) == -1) return null;
            if (this.Sql.GetCommonSql("Nurse.Work.Query.ByWeek&Dept", ref where) == -1) return null;
            sql = sql + " " + where;

            try
            {
                sql = string.Format(sql, week, DeptID);
            }
            catch (Exception e)
            {
                this.Err = "[Nurse.Work.Query.ByWeek&Dept]��ʽ��ƥ��!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }
            return this.QueryBase(sql);

        }

        #region ���ݿ�����������վ �Ű�ʱ���ѯ�Ű���Ϣ
        #endregion

        #region ���ݻ�ʿ��� �Ű�ʱ���ѯ�Ű���Ϣ
        #endregion

        #region ���ݿ��� �Ű�ʱ���ѯ�Ű���Ϣ
        #endregion

        #region ��������Ч�Ļ�ʿ�Ű���Ϣ
        #endregion

        #region �����Ű���Ż���Ű�ʵ��
        #endregion

        /// <summary>
        /// �Ű���Ϣʵ��
        /// </summary>
        FS.HISFC.Models.Nurse.Work objWork;
        private ArrayList al;

        /// <summary>
        /// ��ѯ���ݿ�
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public ArrayList QueryBase(string sql)
        {

            if (this.ExecQuery(sql) == -1) return null;

            this.al = new ArrayList();

            try
            {
                while (this.Reader.Read())
                {
                    this.objWork = new FS.HISFC.Models.Nurse.Work();

                    this.objWork.Templet.ID = this.Reader[2].ToString();
                    this.objWork.Templet.Week = (DayOfWeek)(FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString()));
                    this.objWork.WorkDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4].ToString());
                    this.objWork.Templet.NurseCell.ID = this.Reader[5].ToString();
                    this.objWork.Templet.NurseCell.Name = this.Reader[6].ToString();
                    this.objWork.Templet.Dept.ID = this.Reader[7].ToString();
                    this.objWork.Templet.Dept.Name = this.Reader[8].ToString();
                    this.objWork.Templet.Employee.ID = this.Reader[9].ToString();
                    this.objWork.Templet.Employee.Name = this.Reader[10].ToString();
                    this.objWork.Templet.Noon.ID = this.Reader[11].ToString();
                    this.objWork.Templet.Noon.Name = this.Reader[12].ToString();
                    this.objWork.Templet.IsValid = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[13].ToString());
                    this.objWork.Templet.Memo = this.Reader[14].ToString();
                    this.objWork.Templet.Oper.ID = this.Reader[15].ToString();
                    this.objWork.Templet.Oper.OperTime =FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[16].ToString());
                    this.objWork.Templet.EmplType.ID = this.Reader[17].ToString();
                    this.objWork.Templet.Begin = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[18].ToString());
                    this.objWork.Templet.End = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[19].ToString());
                    this.objWork.Templet.Reason.ID = this.Reader[20].ToString();
                    this.objWork.Templet.Reason.Name = this.Reader[21].ToString();

                    this.al.Add(this.objWork);
                }

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = "��ѯ�Ű���Ϣ����!" + e.Message;
                this.ErrCode = e.Message;
                return null;
            }

            return al;
        }

        #endregion
    }
}
