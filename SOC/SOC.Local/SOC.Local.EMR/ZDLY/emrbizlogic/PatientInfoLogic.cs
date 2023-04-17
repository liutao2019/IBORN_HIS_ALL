using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;
using Neusoft.SOC.Local.EMR.ZDLY.Class;
namespace Neusoft.SOC.Local.EMR.ZDLY.emrbizlogic
{
    class PatientInfoLogic : Neusoft.FrameWork.Management.Database
    {
        string strExceptions = "";
        public DataSet  PatientInfo()
        {
            try
            {
                DataSet ds = new DataSet();
                string strPatient = " select a.patient_no ,a.name ,a.in_date,a.sex_code from fin_ipr_inmaininfo  a where a.in_state='O' and rownum<21";
                strExceptions = strPatient;
                this.ExecQuery(strPatient, ref ds);
                if (ds!=null)
                {
                    return ds;
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

        /// <summary>
        /// strArr数组长度为5 ：0 医生站|护士站，1-科室|病区编码，2-住院|出院标识,3 出院开始日期，4出院结束日期
        /// </summary>
        /// <param name="strArr"></param>
        /// <returns></returns>
        public DataTable GetPatientInfo(string[] strArr)
        {  
            string strWhere = "";
            switch (strArr[0])
            {
                case "医生站": strWhere = " and  a.dept_code='{1}' "; break;
                case "护士站": strWhere = " and  a.nurse_cell_code='{1}' "; break;
                default: strWhere = "  and  a.dept_code='{1}' ";
                break;
            }
            strWhere = (strArr[2] == "O") ? strWhere + " and  a.out_date between to_date('{3}','yyyy-mm-dd') and to_date('{4}','yyyy-mm-dd')" : strWhere;
            try
            {
                DataSet ds = new DataSet();
                string strPatientInfo = @"select ltrim(a.patient_no,'0') patient_no,
                                           a.in_times,
                                           a.pact_name," +
                                        "     case a.sex_code when 'F' then '女' when 'M' then '男' else '' end SEX_CODE,  "+
                                         "          case when length(replace(a.bed_no,a.nurse_cell_code,''))<2  then '0'||replace(a.bed_no,a.nurse_cell_code,'') "+
                                         " else replace(a.bed_no,a.nurse_cell_code,'') end   bed_no," +
                                         "    a.name,"+
                                          "   to_char(in_date,'yyyy-mm-dd hh24:mi:ss') in_date," +
                                         "    a.dept_name,"+
                                           "  a.nurse_cell_name,"+
                                         "    a.out_date,"+
                                          "   a.inpatient_no,"+
                                          " a.house_doc_name,a.charge_doc_name,a.chief_doc_name, "+
                                          "   to_char(b.oper_date,'yyyy-mm-dd hh24:mi:ss') oper_date" +
                                    "    from  fin_ipr_inmaininfo a"+
                                    "   inner join com_shiftdata b"+
                                         "    on a.inpatient_no = b.clinic_no"+
                                   "    where "+
                                          "  b.shift_type = 'K' and a.In_state ='{2}' "; // 
                strPatientInfo = strPatientInfo + strWhere + "  order by bed_no";
                strPatientInfo = string.Format(strPatientInfo, strArr);
                strExceptions = strPatientInfo;
                Neusoft.SOC.Local.EMR.ZDLY.Class.GetEmrLog.WriteSql(" \n " + strExceptions);
                this.ExecQuery(strPatientInfo, ref ds);
                if (ds.Tables.Count>0)
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

        #region 取用户可以登录的科室
        public DataTable GetLoginDept(string strusercode)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @" select distinct a.dept_code 编码, a.dept_name 科室
                                  from (SELECT node_kind, STAT_CODE, dept_code, dept_name, PARDEP_CODE,dept_type
                                          FROM COM_DEPTSTAT cds
                                         WHERE STAT_CODE = '00'
                                           AND VALID_STATE = fun_get_valid) a
                                 where node_kind = 1 and instr(dept_name,'诊')=0  and dept_type in ('N','I') 
                                 START WITH DEPT_CODE IN
                                            (SELECT COM_PRIV_USER.DEPT_CODE
                                               FROM COM_PRIV_USER, COM_PRIV_CLASS2
                                              WHERE COM_PRIV_USER.CLASS2_CODE = COM_PRIV_CLASS2.CLASS2_CODE
                                                AND COM_PRIV_USER.USER_CODE = '" + strusercode + "'" + @" --登录人编码
                                                AND COM_PRIV_USER.CLASS2_CODE = '0000' --登录多科室权限
                                                and (COM_PRIV_USER.class3_code = '236' or
                                                    COM_PRIV_USER.class3_code = '01'))
                                        and STAT_CODE = '00'
                                CONNECT BY PRIOR DEPT_CODE = PARDEP_CODE
                                and instr(dept_name,'诊')=0  and dept_type in ('N','I') ";
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

        #region 取病历加锁记录
        public DataTable GetRecordLock(string strusercode)
        {
            try
            {
                DataSet ds = new DataSet();
                string strSql = @"select ltrim(f.patient_no, '0') patient_no,
                                   f.name as patientname,
                                   r.name,
                                   f.dept_name,
                                   to_char(l.oper_time, 'yyyy-mm-dd hh24:mi:ss') oper_time,
                                   e.empl_name,
                                   e.empl_code,
                                   l.id,
                                   l.inpatient_id,
                                   l.in_record_id
                              from rcd_in_record_lock   l,
                                   rcd_inpatient_record r,
                                   org_employee         e,
                                   fin_ipr_inmaininfo   f
                             where l.in_record_id = r.id
                               and l.oper_id = e.id
                               and l.inpatient_id = f.inpatient_no
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

        #region 解锁操作
        public int DoUnLock(string id)
        {
            int intRet = -1;
            string strSql=@"delete from rcd_in_record_lock  where id in "+id;
            intRet = this.ExecNoQuery(strSql);
            return intRet;

        }
        #endregion
    }
}
