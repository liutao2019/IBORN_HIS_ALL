using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.WinForms.WorkStation.Classes
{
    public class Function : FS.FrameWork.Management.Database
    {
        #region 查询有医嘱变动的患者列表

        /// <summary>
        /// 查询有医嘱变动的患者列表
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryChangedOrder(string nurseCellCode, string deptCode)
        {
            string strSql = "";

            try
            {
                if (this.Sql.GetCommonSql("Order.Order.QueryChangedOrder", ref strSql) == -1)
                {
                    strSql = @"select distinct inpatient_no
                                from(
                                select t.inpatient_no
                                  from met_ipm_order t
                                 where t.mo_date > (select sysdate - 2 from dual)
                                   and t.mo_stat in ('0', '5', '6') --新开、需审核、暂存
                                   and t.confirm_flag = '0'
                                   AND t.SUBTBL_FLAG = '0'
                                   and t.nurse_cell_code = '{0}'

                                union all

                                select t.inpatient_no
                                  from met_ipm_order t
                                 where t.dc_date > (select sysdate - 2 from dual)
                                   and t.mo_stat in ('3', '7') --停止、重整、预停止 重整医嘱不做审核处理
                                   and t.dc_confirm_flag = '0'
                                   AND t.SUBTBL_FLAG = '0'
                                   and t.nurse_cell_code = '{0}'
                                )";
                    //return null;
                }

                strSql = string.Format(strSql, nurseCellCode, deptCode);

                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }

                ArrayList alPatient = new ArrayList();
                while (this.Reader.Read())
                {
                    alPatient.Add(Reader[0].ToString());
                }
                return alPatient;
            }
            catch (Exception ex)
            {
                Err = Err + ex.Message;
                return null;
            }
        }
        /// <summary>
        /// 查询有加急医嘱变动的患者列表// {06A6BD92-97A6-4f01-8B6E-533B35CBF2DD}
        /// </summary>
        /// <param name="nurseCellCode"></param>
        /// <param name="deptCode"></param>
        /// <returns></returns>
        public ArrayList QueryEmcChangedOrder(string nurseCellCode, string deptCode)
        {
            string strSql = "";

            try
            {
                if (this.Sql.GetCommonSql("Order.Order.QueryChangedOrder.1", ref strSql) == -1)
                {
                    strSql = @"select distinct inpatient_no
                                from(
                                select t.inpatient_no
                                  from met_ipm_order t
                                 where t.mo_date > (select sysdate - 2 from dual)
                                   and t.mo_stat in ('0', '5', '6') --新开、需审核、暂存
                                   and t.confirm_flag = '0'
                                   AND t.SUBTBL_FLAG = '0'
                                   and t.EMC_FLAG = '1'
                                   and t.nurse_cell_code = '{0}'

                                union all

                                select t.inpatient_no
                                  from met_ipm_order t
                                 where t.dc_date > (select sysdate - 2 from dual)
                                   and t.mo_stat in ('3', '7') --停止、重整、预停止 重整医嘱不做审核处理
                                   and t.dc_confirm_flag = '0'
                                   AND t.SUBTBL_FLAG = '0'
                                   and t.EMC_FLAG = '1'
                                   and t.nurse_cell_code = '{0}'
                                )";
                    //return null;
                }

                strSql = string.Format(strSql, nurseCellCode, deptCode);

                if (this.ExecQuery(strSql) == -1)
                {
                    return null;
                }

                ArrayList alPatient = new ArrayList();
                while (this.Reader.Read())
                {
                    alPatient.Add(Reader[0].ToString());
                }
                return alPatient;
            }
            catch (Exception ex)
            {
                Err = Err + ex.Message;
                return null;
            }
        }
        #endregion
    }
}
