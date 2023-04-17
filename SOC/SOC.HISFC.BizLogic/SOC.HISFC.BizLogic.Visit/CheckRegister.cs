using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.HISFC.BizLogic.Visit
{
    public class CheckRegister:DataBase
    {
        /// <summary>
        /// 取科室已审核检查列表
        /// </summary>
        /// <param name="dtBegin">开始日期</param>
        /// <param name="dtEnd">结束日期</param>
        /// <returns>科室已审核检查数组，出错返回null</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryDeptCheckRegister(DateTime dtBegin,DateTime dtEnd)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.Sql.GetSql("MetCasCheckRegister.GetDeptList", ref strSQL) == -1)
            {
//                strSQL = @"select 
//un.dept_code,
//(select d.dept_name from com_department d where d.dept_code= un.dept_code) dept_name,
//count(distinct un.inpatient_no)
//from met_ipm_execundrug un,fin_ipr_inmaininfo i 
//where i.inpatient_no=un.inpatient_no
//and un.use_time between to_date('{0}','yyyy-mm-dd HH24:mi:ss') and to_date('{1}','yyyy-mm-dd HH24:mi:ss')
//and i.in_state='I' 
//and un.valid_flag=fun_get_valid()
//and un.class_code='UC'
//group by un.dept_code";
                strSQL = @"select aa.dept_code,aa.dept_name,count(distinct aa.inpatient_no) from 
(select 
un.exec_sqn,
un.dept_code,
(select d.dept_name from com_department d where d.dept_code= un.dept_code) dept_name,
un.inpatient_no
from met_ipm_execundrug un,fin_ipr_inmaininfo i 
where i.inpatient_no=un.inpatient_no
and un.use_time between  to_date('{0}','yyyy-mm-dd HH24:mi:ss') and to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and i.in_state='I' 
and un.valid_flag=fun_get_valid()
and un.class_code='UC'
) aa where not exists (select 1 from met_cas_checkregister c where c.exec_sqn=aa.exec_sqn and c.valid_flag='1')
group by aa.dept_code,aa.dept_name";
                this.CacheSQL("MetCasCheckRegister.GetDeptList",strSQL);
            }
            strSQL = string.Format(strSQL, dtBegin, dtEnd);
            //取科室已审核检查列表
            List<FS.FrameWork.Models.NeuObject> al = new List<FS.FrameWork.Models.NeuObject>();
            FS.FrameWork.Models.NeuObject obj; 
            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得科室常数时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //取查询结果中的记录
                    obj = new FS.FrameWork.Models.NeuObject();

                    obj.ID = this.Reader[0].ToString(); //0 部门编码
                    obj.Name = this.Reader[1].ToString(); //1 部门名称
                    obj.Memo = this.Reader[2].ToString();   //2 患者数量
                    al.Add(obj);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得科室常数信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                this.Reader.Close();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// 取科室已审核检查列表
        /// </summary>
        /// <param name="dtBegin">开始日期</param>
        /// <param name="dtEnd">结束日期</param>
        /// <param name="deptCode">科室编码</param>
        /// <returns>科室已审核检查数组，出错返回null</returns>
        public List<FS.HISFC.Models.Fee.Item.Undrug> QueryDeptDetailCheckRegister(DateTime dtBegin, DateTime dtEnd,string deptCode)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.Sql.GetSql("MetCasCheckRegister.GetDeptDetail", ref strSQL) == -1)
            {
                strSQL = @"select exec_sqn,dept_code,dept_name,inpatient_no,name,substr(bed_no,5),undrug_code,undrug_name,use_time
 from 
(select 
un.exec_sqn,un.dept_code,
(select d.dept_name from com_department d where d.dept_code=un.dept_code) dept_name,
un.inpatient_no,
i.name,
i.bed_no,m.sort_id,un.undrug_code,un.undrug_name,un.use_time
from met_ipm_execundrug un,fin_ipr_inmaininfo i,com_bedinfo m 
where i.inpatient_no=un.inpatient_no
and un.use_time between to_date('{0}','yyyy-mm-dd HH24:mi:ss') and to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and un.dept_code='{2}'
and i.in_state='I' 
and un.valid_flag=fun_get_valid()
and un.class_code='UC'
and i.bed_no=m.bed_no   
) a 
where   not exists (select 1 from met_cas_checkregister c where c.exec_sqn=a.exec_sqn /*and c.valid_flag='1'*/)
order by a.sort_id,inpatient_no,use_time";
                this.CacheSQL("MetCasCheckRegister.GetDeptDetail", strSQL);
            }
            strSQL = string.Format(strSQL, dtBegin, dtEnd,deptCode);
            //取科室已审核检查列表
            List<FS.HISFC.Models.Fee.Item.Undrug> al = new List<FS.HISFC.Models.Fee.Item.Undrug>();
            FS.HISFC.Models.Fee.Item.Undrug obj=null;
            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得科室常数时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //取查询结果中的记录
                    obj = new  FS.HISFC.Models.Fee.Item.Undrug();
                    obj.ID = this.Reader[0].ToString(); //0 执行单流水号
                    obj.Name = this.Reader[1].ToString(); //1 科室编码
                    obj.Memo = this.Reader[2].ToString();   //2 科室名称
                    obj.ApplicabilityArea = this.Reader[3].ToString();//3 住院流水号
                    obj.CheckApplyDept = this.Reader[4].ToString();//4患者姓名
                    obj.CheckBody = this.Reader[5].ToString();//床号
                    obj.DiseaseType.ID = this.Reader[6].ToString();//项目编码
                    obj.DiseaseType.Name = this.Reader[7].ToString();//项目名称
                    obj.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8].ToString());//执行时间
                    al.Add(obj);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得科室常数信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                this.Reader.Close();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

        /// <summary>
        /// 插入辅助检查安排信息
        /// </summary>
        /// <param name="info">辅助检查实体</param>
        /// <returns>0 success -1 fail</returns>
        public int InsertDeptCheckRegister(FS.HISFC.Models.Fee.Item.Undrug info)
        {

            string strSQL = "";
            //取SELECT语句
            if (this.Sql.GetSql("MetCasCheckRegister.Insert", ref strSQL) == -1)
            {
                strSQL = @"
insert into met_cas_checkregister
  (exec_sqn,
   dept_code,
   inpatient_no,
   bed_no,
   item_code,
   item_name,
   exec_fri_oper,
   exec_sec_oper,
   exec_date,
   send_type,
   valid_flag,
   oper_code,
   oper_date)
values
  ('{0}',
   '{1}',
   '{2}',
   '{3}',
   '{4}',
   '{5}',
   '{6}',
   '{7}',
   to_date('{8}', 'yyyy-mm-dd HH24:mi:ss'),
   '{9}',
   '{10}',
   '{11}',
   to_date('{12}', 'yyyy-mm-dd HH24:mi:ss'))";
                this.CacheSQL("MetCasCheckRegister.Insert", strSQL);
            }
            strSQL = string.Format(strSQL, info.ID, //0 执行单流水号
                    info.Name, //1 科室编码
                    info.ApplicabilityArea,//3 住院流水号
                    info.CheckBody,//床号
                    info.DiseaseType.ID,//项目编码
                    info.DiseaseType.Name,//项目名称
                    info.OperationInfo.ID,//执行者1
                   info.OperationInfo.Name,//执行者2
                   info.OperationInfo.Memo,//执行时间
                   info.OperationScale.Memo,//送检方式
                   info.ValidState,//有效性
                   info.Oper.ID,//操作员
                   info.Oper.OperTime);//操作日期
            if (strSQL == null) return -1;

            return this.ExecNoQuery(strSQL);
        }
        /// <summary>
        /// 更新辅助检查安排信息
        /// </summary>
        /// <param name="info">辅助检查实体</param>
        /// <returns>0 success -1 fail</returns>
        public int UpdateDeptCheckRegister(FS.HISFC.Models.Fee.Item.Undrug info)
        {

            string strSQL = "";
            //取SELECT语句
            if (this.Sql.GetSql("MetCasCheckRegister.update", ref strSQL) == -1)
            {
                strSQL = @"
update met_cas_checkregister
   set exec_sqn = '{0}',
       dept_code = '{1}',
       inpatient_no = '{2}',
       bed_no ='{3}',
       item_code ='{4}',
       item_name = '{5}',
       exec_fri_oper = '{6}',
       exec_sec_oper = '{7}',
       exec_date = to_date('{8}', 'yyyy-mm-dd HH24:mi:ss'),
       send_type =  '{9}',
       valid_flag =  '{10}',
       oper_code =  '{11}',
       oper_date =  to_date('{12}', 'yyyy-mm-dd HH24:mi:ss')
 where exec_sqn = '{0}'";
                this.CacheSQL("MetCasCheckRegister.update", strSQL);
            }
            strSQL = string.Format(strSQL, info.ID, //0 执行单流水号
                    info.Name, //1 科室编码
                    info.ApplicabilityArea,//3 住院流水号
                    info.CheckBody,//床号
                    info.DiseaseType.ID,//项目编码
                    info.DiseaseType.Name,//项目名称
                    info.OperationInfo.ID,//执行者1
                   info.OperationInfo.Name,//执行者2
                   info.OperationInfo.Memo,//执行时间
                   info.OperationScale.Memo,//送检方式
                   info.ValidState,//有效性
                   info.Oper.ID,//操作员
                   info.Oper.OperTime);//操作日期
            if (strSQL == null) return -1;
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 获取已安排辅助检查信息
        /// </summary>
        /// <param name="dtBegin">开始日期</param>
        /// <param name="dtEnd">结束日期</param>
        /// <param name="deptCode">科室编码</param>
        /// <returns>科室已审核检查数组，出错返回null</returns>
        public List<FS.HISFC.Models.Fee.Item.Undrug> QueryReadyCheckRegister(DateTime dtBegin, DateTime dtEnd, string deptCode)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.Sql.GetSql("MetCasCheckRegister.GetCheckRegister", ref strSQL) == -1)
            {
                strSQL = @"select t.exec_sqn,t.dept_code,
(select d.dept_name from com_department d where d.dept_code=t.dept_code) dept_name,
t.inpatient_no,i.name,
t.bed_no,t.item_code,t.item_name,t.exec_fri_oper,
t.exec_sec_oper,t.exec_date,t.send_type,t.valid_flag,t.oper_code,t.oper_date from met_cas_checkregister  t,fin_ipr_inmaininfo i, com_bedinfo a
where t.exec_date between to_date('{0}','yyyy-mm-dd HH24:mi:ss') 
and to_date('{1}','yyyy-mm-dd HH24:mi:ss')
and t.inpatient_no = i.inpatient_no
and a.bed_no = i.bed_no 
and t.dept_code='{2}'and t.bed_no=substr(a.bed_no,5) order by a.sort_id,t.exec_date";
                this.CacheSQL("MetCasCheckRegister.GetCheckRegister", strSQL);
            }
            strSQL = string.Format(strSQL, dtBegin, dtEnd, deptCode);
            //取科室已审核检查列表
            List<FS.HISFC.Models.Fee.Item.Undrug> al = new List<FS.HISFC.Models.Fee.Item.Undrug>();
            FS.HISFC.Models.Fee.Item.Undrug obj = null;
            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得科室常数时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //取查询结果中的记录
                    obj = new FS.HISFC.Models.Fee.Item.Undrug();
                    obj.ID = this.Reader[0].ToString(); //0 执行单流水号
                    obj.Name = this.Reader[1].ToString(); //1 科室编码
                    obj.Memo = this.Reader[2].ToString();   //2 科室名称
                    obj.ApplicabilityArea = this.Reader[3].ToString();//3 住院流水号
                    obj.CheckApplyDept = this.Reader[4].ToString();//4患者姓名
                    obj.CheckBody = this.Reader[5].ToString();//床号
                    obj.DiseaseType.ID = this.Reader[6].ToString();//项目编码
                    obj.DiseaseType.Name = this.Reader[7].ToString();//项目名称
                    obj.OperationInfo.ID = this.Reader[8].ToString();//执行者一
                    obj.OperationInfo.Name = this.Reader[9].ToString();//执行者二
                    obj.OperationInfo.Memo = this.Reader[10].ToString();//执行日期
                    obj.OperationScale.Memo = this.Reader[11].ToString();//送检方式
                    obj.ValidState = this.Reader[12].ToString();//有效性
                    obj.Oper.ID = this.Reader[13].ToString();//操作员
                    obj.Oper.OperTime =FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14].ToString());//操作日期
                    al.Add(obj);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得科室常数信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                this.Reader.Close();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }


        /// <summary>
        /// 取科室已审核检查列表
        /// </summary>
        /// <param name="dtBegin">开始日期</param>
        /// <param name="dtEnd">结束日期</param>
        /// <returns>科室已审核检查数组，出错返回null</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryReadyDeptCheckRegister(DateTime dtBegin, DateTime dtEnd)
        {
            string strSQL = "";
            //取SELECT语句
            if (this.Sql.GetSql("MetCasCheckRegister.GetDeptList.Ready", ref strSQL) == -1)
            {
                strSQL = @"select c.dept_code,(select d.dept_name from com_department d 
where d.dept_code= c.dept_code) dept_name,count(distinct c.inpatient_no)  from met_cas_checkregister c
where c.exec_date between  to_date('{0}','yyyy-mm-dd HH24:mi:ss') and to_date('{1}','yyyy-mm-dd HH24:mi:ss')
group by  c.dept_code";
                this.CacheSQL("MetCasCheckRegister.GetDeptList.Ready", strSQL);
            }
            strSQL = string.Format(strSQL, dtBegin, dtEnd);
            //取科室已审核检查列表
            List<FS.FrameWork.Models.NeuObject> al = new List<FS.FrameWork.Models.NeuObject>();
            FS.FrameWork.Models.NeuObject obj;
            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得科室常数时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //取查询结果中的记录
                    obj = new FS.FrameWork.Models.NeuObject();

                    obj.ID = this.Reader[0].ToString(); //0 部门编码
                    obj.Name = this.Reader[1].ToString(); //1 部门名称
                    obj.Memo = this.Reader[2].ToString();   //2 患者数量
                    al.Add(obj);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得科室常数信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                this.Reader.Close();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return al;
        }

    }
}
