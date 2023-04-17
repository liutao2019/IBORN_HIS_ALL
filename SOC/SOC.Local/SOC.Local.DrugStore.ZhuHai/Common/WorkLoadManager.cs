using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FS.SOC.Local.DrugStore.ZhuHai.Common
{
    /// <summary>
    /// [功能描述: 药房工作量本地化]<br></br>
    /// [创 建 者: cube]<br></br>
    /// [创建时间: 2010-12]<br></br>
    /// </summary>
    public class WorkLoadManager
    {
        /// <summary>
        /// 数据库表信息
        /// </summary>
        private string tableInfo = @"
        表名：pha_soc_drugstore_workload
        字段：
                bill_no 字符串，住院摆药单号或者门诊处方号，sql参数编号：'{0}'
                drug_dept_code 字符串，药房编码，sql参数编号：'{1}'
                approve_dept_cod 字符创，药房编码，sql参数编号：'{2}'
                bill_type 字符串，0表示门诊，1表示住院，sql参数编号：'{3}'
                work_type 字符串，0表示配药，1表示发药，2表示核对，sql参数编号：'{4}'
                work_oper_code 字符串，工号，sql参数编号：'{5}'
                work_oper_date 时间，记录工作量的时间，sql不作为参数    
                qty 数字，门诊处方品种数，住院摆药记录数，sql参数编号：'{6}'           
                oper_code 字符串，操作人工号，sql参数编号：'{7}'
                oper_date 时间，操作时间，sql不作为参数

        主键：  bill_no，bill_type，approve_dept_code，work_type
        索引：  work_oper_date

        SQL：   插入：SOC.Pharmacy.Drugstore.WorkLoad.Insert，记录工作量的时间、操作时间取数据系统时间
                更新：SOC.Pharmacy.Drugstore.WorkLoad.Update，where调剂为主键
";

        FS.FrameWork.Management.DataBaseManger dataBaseManger = new FS.FrameWork.Management.DataBaseManger();

        /// <summary>
        /// 设置门诊工作量，函数内部设置事务
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="drugDetpNO">原处方调剂的药房编码</param>
        /// <param name="operDeptNO">处方实际调配科室</param>
        /// <param name="workType">0表示配药 1表示发药 2表示核对</param>
        /// <param name="emplNO">工作量计入工号</param>
        /// <returns></returns>
        private int SetWorkLoad(string type, string recipeNO, string drugDetpNO, string operDeptNO, string workType, string emplNO, decimal qty)
        {
            string sqlInsert = "";
            if (dataBaseManger.Sql.GetSql("SOC.Pharmacy.Drugstore.WorkLoad.Insert", ref sqlInsert) == -1)
            {
                sqlInsert = @"insert into pha_soc_drugstore_workload
                                                   (bill_no,
                                                   drug_dept_code,
                                                   approve_dept_code,
                                                   bill_type,
                                                   work_type,
                                                   work_oper_code,
                                                   work_oper_date,
                                                   qty,
                                                   oper_code,
                                                   oper_date
                                            )values
                                            (
                                                   '{0}',--bill_no,
                                                   '{1}',--drug_dept_code,
                                                   '{2}',--approve_dept_code,
                                                   '{3}',--bill_type,
                                                   '{4}',--work_type,
                                                   '{5}',--work_oper_code,
                                                   sysdate,--work_oper_date,
                                                   '{6}',--qty,
                                                   '{7}',--oper_code,
                                                   sysdate--oper_date
                                            )";
            }
            try
            {
                sqlInsert = string.Format(sqlInsert, recipeNO, drugDetpNO, operDeptNO, type, workType, emplNO, qty.ToString(), this.dataBaseManger.Operator.ID);
            }
            catch (Exception ex)
            {
                MessageBox.Show("工作量设置失败：sql参数化失败" + ex.Message + "\n" + sqlInsert);
                return -1;
            }
            FS.FrameWork.Management.PublicTrans.BeginTransaction();

            int param = this.dataBaseManger.ExecNoQuery(sqlInsert);
            if (param == -1)
            {
                string sqlUpdate = "";
                if (dataBaseManger.Sql.GetSql("SOC.Pharmacy.Drugstore.WorkLoad.Update", ref sqlUpdate) == -1)
                {
                    sqlUpdate = @"update pha_soc_drugstore_workload 
                                            set    drug_dept_code = '{1}',--drug_dept_code,
                                                   work_oper_code= '{5}',--work_oper_code,
                                                   work_oper_date= sysdate,--work_oper_date,
                                                   qty = '{6}',--qty,
                                                   oper_code = '{7}',--oper_code,
                                                   oper_date = sysdate--oper_date
                                            where  bill_no = '{0}'--bill_no,
                                            and    approve_dept_code = '{2}'--approve_dept_code,
                                            and    bill_type = '{3}'--bill_type,
                                            and    work_type= '{4}'--work_type,";
                }
                try
                {
                    sqlUpdate = string.Format(sqlUpdate, recipeNO, drugDetpNO, operDeptNO, type, workType, emplNO, qty.ToString(), this.dataBaseManger.Operator.ID);
                }
                catch (Exception ex)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("工作量设置失败：sql参数化失败" + ex.Message + "\n" + sqlUpdate);
                    return -1;
                }
                param = this.dataBaseManger.ExecNoQuery(sqlUpdate);
                if (param == -1)
                {
                    FS.FrameWork.Management.PublicTrans.RollBack();
                    MessageBox.Show("工作量设置失败：执行sql失败" + sqlUpdate);
                    return -1;
                }

                FS.FrameWork.Management.PublicTrans.Commit();

            }

            FS.FrameWork.Management.PublicTrans.Commit();
            return 1;
        }

        /// <summary>
        /// 设置门诊工作量，函数内部设置事务
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="drugDetpNO">原处方调剂的药房编码</param>
        /// <param name="operDeptNO">处方实际调配科室</param>
        /// <param name="workType">0表示配药 1表示发药 2表示核对</param>
        /// <param name="emplNO">工作量计入工号</param>
        /// <returns></returns>
        public int SetOutpatientWorkLoad(string recipeNO, string drugDetpNO, string operDeptNO, string workType, string emplNO, decimal qty)
        {
            return this.SetWorkLoad("0", recipeNO, drugDetpNO, operDeptNO, workType, emplNO, qty);
        }

        /// <summary>
        /// 设置住院工作量，函数内部设置事务
        /// </summary>
        /// <param name="drugBIllNO">摆药单号</param>
        /// <param name="drugDetpNO">原调剂的药房编码</param>
        /// <param name="operDeptNO">实际调配科室</param>
        /// <param name="workType">0表示配药 1表示发药 2表示核对</param>
        /// <param name="emplNO">工作量计入工号</param>
        /// <returns></returns>
        public int SetInpatientWorkLoad(string drugBIllNO, string drugDetpNO, string operDeptNO, string workType, string emplNO, decimal qty)
        {
            return this.SetWorkLoad("1", drugBIllNO, drugDetpNO, operDeptNO, workType, emplNO, qty);
        }

    }
}
