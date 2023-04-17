using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.DrugStore.GuangZhou.Common
{
    public class LocalWorkLoadManager:FS.FrameWork.Management.Database
    {
        public int UpdateOutPatientWorkLoad(FS.FrameWork.Models.NeuObject neuObj,string operCode,string type)
        {
            int param = -1;
            string strSql = "";
            DateTime dateTime = this.GetDateTimeFromSysDateTime();
            string curOper = this.Operator.ID.PadLeft(6,'0');
            if (this.Sql.GetCommonSql("SOC.Pharmacy.Drugstore.WorkLoad.Query", ref strSql) == -1)
            {
                strSql = @" update pha_soc_drugstore_workload
   set work_oper_code = '{2}', oper_code = '{3}', oper_date = to_date('{4}','yyyy-mm-dd hh24:mi:ss')
 where bill_no = '{0}'
   and work_type = '{1}'";
            }
            try
            {
                strSql = string.Format(strSql, neuObj.ID, type,operCode,curOper,dateTime);
                param = this.ExecNoQuery(strSql);
            }
            catch (Exception ex)
            {
                this.Err = "执行sql语句错误" + ex.Message;
            }
            return param;

        }

        /// <summary>
        /// 根据配发药类型查询工作量
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <param name="drugDeptNO"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject QueryOutPatientWorkLoad(string recipeNO, string drugDeptNO, string type)
        {
            FS.FrameWork.Models.NeuObject neuObj = null;
            string strSql = "";
            if (this.Sql.GetCommonSql("SOC.Pharmacy.Drugstore.WorkLoad.Query", ref strSql) == -1)
            {
                strSql = @" select aa.bill_no,
                                  aa.drug_dept_code,
                                  aa.work_oper_code,
                                  aa.work_oper_date,
                                  aa.qty,
                                  aa.work_type
                                  from pha_soc_drugstore_workload aa
                                  where aa.drug_dept_code = '{0}'
   and aa.bill_no = '{1}'
   and aa.work_type = '{2}' ";
            }
            try
            {
                strSql = string.Format(strSql, drugDeptNO, recipeNO, type);
            }
            catch (Exception ex)
            {
                this.Err = "格式化sql语句错误" + ex.Message;
            }
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "执行SQL语句错误";
                return null;
            }
            try
            {
               
                while (this.Reader.Read())
                {
                    neuObj = new FS.FrameWork.Models.NeuObject();
                    neuObj.ID = this.Reader[0].ToString();//处方号
                    neuObj.Name = this.Reader[1].ToString();//科室编码
                    neuObj.Memo = this.Reader[2].ToString();//原有工号
                    neuObj.User01 = this.Reader[3].ToString();//操作日期
                    neuObj.User02 = this.Reader[4].ToString();//品种数
                    neuObj.User03 = this.Reader[5].ToString();//门诊/住院类型
                }
            }
            catch (Exception ex)
            {
                this.Err = "赋值错误" + ex.Message;
            }
            finally
            {
                this.Reader.Close();
            }
            return neuObj;
        }
    }
}
