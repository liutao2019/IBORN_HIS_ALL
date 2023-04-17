using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic
{
    class EmployeeInfoLogic : Neusoft.FrameWork.Management.Database
    {
        string strExceptions = "";

        #region 获取有效人员
        /// <summary>
        /// 获取有效人员
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmployee()
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select b.id as id,
                           b.empl_code code,
                           b.empl_name name,
                           b.spell_code py,
                           b.wb_code wb,
                           b.empl_type_code type,
                           b.levl_code as lcode,
                           b.dept_id did,
                           c.name as namet,
                           d.name rname,
                           d.id rid
                      from org_employee b, DAWN_CODE_CODE c,org_role_job_level a ,org_role d
                     where b.levl_code = c.value
                       and c.CODE_TYPE_ID = 50341
                       and c.VALID_FLAG = 1
                       and d.valid_flag=1
                       and a.role_id=d.id
                       and b.levl_code=a.job_level_code
                    ";
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

        #region 获取人员的额外角色
        /// <summary>
        /// 获取人员的额外角色
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmplRole()
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select b.name, a.*
                           from org_empl_role_alter a
                          inner join org_role b
                             on a.role_id = b.id
                         where b.valid_flag=1 ";
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

        #region 获取人员的额外权限
        /// <summary>
        /// 获取人员的额外权限
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmplPerm()
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select b.id pid,
                               e.id addid,
                               e.empl_id eid,
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
                               d.name as trname,
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
                               b.patient_source_code as pcode,
                               b.recd_type_code as trcode,
                               b.recd_state_code as rscode,
                               b.recd_oper_code as rocode
                          from org_recd_perm b, dawn_code_code d, org_empl_perm_alter e
                         where b.recd_type_code = d.value
                           and d.parent_id is not null
                           and b.id = e.perm_id";
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

        #region 添加人员的额外角色
        /// <summary>
        ///添加人员的额外角色.返回1表示成功.参数getseqid是产生的序列号,
        /// </summary>
        /// <returns></returns>
        public int InsertEmplRole(int empid, int roleid,ref int getseqid)
        {
            try
            {
                int seqid = 1;
                string sqlid = "";
                DataSet ds = new DataSet();
                sqlid = @" select seq_org_empl_role_alter.nextval from dual";
                this.ExecQuery(sqlid, ref ds);
                if (ds.Tables.Count == 0)
                {
                    return 0;
                }
                seqid = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                getseqid = seqid;
                strExceptions = "InsertEmplRole";
                string sql2 = @" insert into org_empl_role_alter values({0},{1},{2},'Add')";
                sql2 = string.Format(sql2, seqid, empid,roleid);
                this.ExecNoQuery(sql2);
                return 1;
            }
            catch (Exception ex)
            {
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteException(ex.Message + " \n " + strExceptions);
                return 0;
            }
        }
        #endregion

        #region 添加人员的额外权限，并且权限已经存在于其他角色中
        /// <summary>
        ///添加人员的额外权限，并且权限已经存在于其他角色中
        /// </summary>
        /// <returns></returns>
        public DataTable  InsertEmplPerm(DataTable dtnew,int empid)
        {
            try
            {
                List<string> lstpermid = new List<string>();
                string select = "";
                string obj = null;
                strExceptions = "InsertEmplPerm ";
                DataSet dsid = null;
                int k = dtnew.Rows.Count;
                for (int j = k-1; j >=0; j--)
                {
                    dsid = new DataSet();
                    select = " select a.id from org_recd_perm a where PATIENT_SOURCE_CODE='{0}' and RECD_TYPE_CODE='{1}' and  RECD_STATE_CODE='{2}' and RECD_OPER_CODE='{3}' ";
                    select = string.Format(select, dtnew.Rows[j].ItemArray);
                    this.ExecQuery(select, ref dsid);
                    if (dsid==null || dsid.Tables[0].Rows.Count == 0)
                    {
                        //return dtnew;
                        continue;
                    }
                    obj = dsid.Tables[0].Rows[0][0].ToString();
                     if (obj != "")
                    {
                        lstpermid.Add(obj);
                        DataRow drs = dtnew.Rows[j];
                        dtnew.Rows.Remove(drs);
                    }
                }
                strExceptions = "InsertEmplPerm "+select;
                int seqid = 1;
                string getseqid = "", sqlinsert = "";;
                DataSet ds = new DataSet();
                getseqid = @"select seq_org_empl_perm_alter.nextval from dual";
                this.ExecQuery(getseqid, ref ds);
                if (ds.Tables.Count == 0)
                {
                    return null;
                }
                seqid = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                for (int n = 0; n < lstpermid.Count; n++)
                {
                    sqlinsert = @"insert into org_empl_perm_alter values(" + seqid + "," + empid + "," + lstpermid[n] + ",'Add')";
                    this.ExecNoQuery(sqlinsert); //插入操作
                    this.ExecQuery(getseqid); //更新序列
                    seqid++;
                }
                return dtnew;
            }
            catch (Exception ex)
            {
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteException(ex.Message + " \n " + strExceptions);
                return null;
            }
        }
        #endregion

        #region 添加人员的额外权限，权限是全新的，不经存在其他角色中
        /// <summary>
        ///添加人员的额外权限，权限是全新的，不经存在其他角色中
        /// </summary>
        /// <returns></returns>
        public int InsertEmplPermNew(DataTable dtnew, int empid)
        {
            try
            {
                int seqid = 1;
                int seqid2 = 2;
                string getseqid = "";
                DataSet ds = new DataSet();
                getseqid = "select seq_org_permission.nextval id1,seq_org_empl_perm_alter.nextval id2 from dual";
                this.ExecQuery(getseqid, ref ds);
                if (ds.Tables.Count == 0)
                {
                    return 0;
                }
                seqid = Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                seqid2 = Convert.ToInt32(ds.Tables[0].Rows[0][1].ToString());
                strExceptions = "InsertEmplPermNew";
                string sql1 = "", sql2 = "", sql3 = "",sql4="";
                Neusoft.FrameWork.Management.PublicTrans.BeginTransaction();
                for (int k = 0; k < dtnew.Rows.Count; k++)
                {
                    sql1 = " insert into org_recd_perm  VALUES (" + seqid + ",'{0}','{1}', '{2}','{3}')";
                    sql2 = " insert into org_permission VALUES (" + seqid + ",1)  ";
                    sql3 = " insert into org_empl_perm_alter values(" + seqid2 + "," +empid+ "," + seqid + ",'Add')";
                    sql1 = string.Format(sql1, dtnew.Rows[k].ItemArray);
                    sql4 = "select seq_org_permission.nextval id1,seq_org_empl_perm_alter.nextval id2 from dual";
                    
                    this.ExecNoQuery(sql1);
                    this.ExecNoQuery(sql2);
                    this.ExecNoQuery(sql3);
                    this.ExecQuery(sql4);

                    seqid++;
                    seqid2++;
                }
                Neusoft.FrameWork.Management.PublicTrans.Commit();
                return 1;
            }
            catch (Exception ex)
            {
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteException(ex.Message + " \n " + strExceptions);
                Neusoft.FrameWork.Management.PublicTrans.RollBack();
                return 0;
            }
        }
        #endregion

        #region 删除人员的额外角色
        /// <summary>
        ///删除人员的额外角色.返回1表示成功.
        /// </summary>
        /// <returns></returns>
        public int DeleteEmplRole(int roleid)
        {
            try
            {

                DataSet ds = new DataSet();
                string sql = @" delete from org_empl_role_alter where rownum=1 and id=" + roleid;
                this.ExecQuery(sql);
                strExceptions = sql;
                return 1;
            }
            catch (Exception ex)
            {
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteException(ex.Message + " \n " + strExceptions);
                return 0;
            }
        }
        #endregion

        #region 删除人员的额外权限
        /// <summary>
        ///删除人员的额外权限.返回1表示成功.
        /// </summary>
        /// <returns></returns>
        public int DeleteEmplPerm( string permids)
        {
            try
            {

                DataSet ds = new DataSet();
                string sql = @" delete from org_empl_perm_alter where id in " + permids;
                this.ExecQuery(sql);
                strExceptions = sql;
                return 1;
            }
            catch (Exception ex)
            {
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteException(ex.Message + " \n " + strExceptions);
                return 0;
            }
        }
        #endregion
    }
}
