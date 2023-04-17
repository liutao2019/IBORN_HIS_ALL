using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic
{
    class RoleInfoLogic:Neusoft.FrameWork.Management.Database
    {
        string strExceptions = "";
        
        #region 或取有效角色
        /// <summary>
        /// 或取有效角色
        /// </summary>
        /// <returns></returns>
        public DataTable GetUseRole()
        {
            try
            {   
                DataSet ds = new DataSet();
                string strSql = @"select  a.ID,a.Name from org_role a where a.valid_flag=1 order by a.ID";
                strExceptions = strSql;
                this.ExecQuery(strSql, ref ds);
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteException(ex.Message + " \n " + strExceptions);
                return null;
            }
        }
        #endregion

        #region 获取病历树
        /// <summary>
        ///获取病历树 
        /// </summary>
        /// <returns></returns>
        public DataTable GetTemplateTree()
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @" select   t.id,t.name,t.value,t.parent_id as PARENTID,0 flag
                                from     DAWN_CODE_CODE t
                                where    t.CODE_TYPE_ID = 9  
                                and t.valid_flag=1
                                order by t.SORT_NUM asc";
                strExceptions = strSql;
                this.ExecQuery(strSql, ref ds);
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteException(ex.Message + " \n " + strExceptions);
                return null;
            }
        }
        #endregion

        #region 取角色对应的权限
        /// <summary>
        ///取角色对应的权限,roleid角色id
        /// </summary>
        /// <param name="roleid">角色id</param>
        /// <returns></returns>
        public DataTable GetRolePrivileges(string roleid)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select b.id,
                           case b.patient_source_code
                             when 'SF' then
                              '分管'
                             when 'DP' then
                              '科室'
                             when 'ALL' then
                              '全院'
                             when 'RD' then
                              '转出'
                             when 'OP' then
                              '手术'
                             else
                              '会诊'
                           end as pname,
                           b.patient_source_code as pcode,
                           d.name as trname,
                           b.recd_type_code as trcode,
                           case b.recd_state_code
                             when 'Create' then
                              '创建'
                             when 'Save' then
                              '保存'
                             when 'ResDoctorSign' then
                              '住院医师'
                             when 'CharDoctorSign' then
                              '主治'
                             when 'DireDoctorSign' then
                              '主任'
                             when 'Submit' then 
                              '提交'
                           end as rsname,
                           b.recd_state_code as rscode,
                           
                           case b.recd_oper_code
                             when 'Create' then
                              '创建'
                             when 'Update' then
                              '保存'
                             when 'Sign' then
                              '签名'
                             when 'UnSign' then
                              '解签'
                             when 'Delete' then
                              '删除'
                             when 'WebQuery' then
                              'web查询'
                           end as roname,
                           b.recd_oper_code as rocode,
                           a.valid_flag as flag
                    from org_permission a, org_recd_perm b,org_role_perm c ,dawn_code_code d
                    where c.role_id=" + roleid + @"
                    and a.id=c.perm_id and a.id=b.id
                    and b.recd_type_code=d.value  
                    and d.parent_id is not null";
                strExceptions = strSql;
                this.ExecQuery(strSql, ref ds);
                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteException(ex.Message + " \n " + strExceptions);
                return null;
            }
        }
        #endregion
        
        #region 更新权限状态
        /// <summary>
        ///更新权限状态,dtprivilege,org_permission
        /// </summary>
        /// <returns></returns>
        public int UpdatePrivelegeStatus(DataTable dtprivilege)
        {
            try
            {
                strExceptions = "UpdatePrivelegeStatus(DataTable dtprivilege)";
                string sql = "";
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                for (int k = 0; k < dtprivilege.Rows.Count; k++)
                {
                    sql = " update org_permission  set VALID_FLAG={1} where ID={0} and rownum<2";
                    sql = string.Format(sql, dtprivilege.Rows[k].ItemArray);
                    this.ExecNoQuery(sql);
                }
                Neusoft.FrameWork.Management.PublicTrans.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteException(ex.Message + " \n " + strExceptions);
                return 0;
            }
        }
        #endregion

        #region 添加权限
        /// <summary>
        ///添加权限状态 dtinsert,roleid角色id.返回1表示成功.
        /// </summary>
        /// <returns></returns>
        public int InsertPriveleges(DataTable dtinsert, int  roleid)
        {
            try
            {
                int seqid = 1;
                int seqid2 = 2;
                string sqlid = "";
                DataSet ds = new DataSet();
                sqlid = "select seq_org_permission.nextval id1, seq_org_role_perm.nextval id2 from dual";
                this.ExecQuery(sqlid, ref ds);
                if (ds.Tables.Count == 0)
                {
                    return 0;
                }
                seqid = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                seqid2 = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
                strExceptions = "InsertPriveleges";

                string sql1 = "", sql2 = "", sql3 = "",sql4="";
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                for (int k = 0; k < dtinsert.Rows.Count; k++)
                {
                    sql1 = " insert into  org_recd_perm  VALUES (" + seqid + ",'{0}','{1}', '{2}','{3}')";
                    sql2 = " insert into org_permission VALUES (" + seqid + ",1)  ";
                    sql3 = " insert into org_role_perm values(" +  seqid2+ "," + roleid + "," + seqid + ")";
                    sql4="select seq_org_permission.nextval id1, seq_org_role_perm.nextval id2 from dual";
                    sql1 = string.Format(sql1, dtinsert.Rows[k].ItemArray);
                    this.ExecNoQuery(sql1);
                    this.ExecNoQuery(sql2);
                    this.ExecNoQuery(sql3);
                    this.ExecNoQuery(sql4);
                }
                Neusoft.FrameWork.Management.PublicTrans.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteException(ex.Message + " \n " + strExceptions);
                return 0;
            }
        }
        #endregion
    }
}
