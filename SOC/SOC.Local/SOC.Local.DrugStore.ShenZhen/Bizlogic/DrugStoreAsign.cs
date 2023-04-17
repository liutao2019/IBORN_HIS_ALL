using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.DrugStore.ShenZhen.Bizlogic
{
    /// <summary>
    /// 药房叫号业务层
    /// </summary>
    public class DrugStoreAsign:FrameWork.Management.Database
    {
        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <param name="drugStoreAsign"></param>
        /// <returns></returns>
        protected object[] myGetParmInsertQueue(FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign drugStoreAsign)
        {
            object[] strParm ={	
								  drugStoreAsign.recipeNO,
                                  drugStoreAsign.patientId,
                                  drugStoreAsign.cardNO,
                                  drugStoreAsign.patientName,
                                  drugStoreAsign.patientSex,
                                  drugStoreAsign.deptCode,
                                  drugStoreAsign.drugDeptCode,
                                  drugStoreAsign.sendTerminalCode,
                                  drugStoreAsign.sendTerminalName,
                                  drugStoreAsign.memo,
                                  drugStoreAsign.Oper.ID,
                                  drugStoreAsign.Oper.OperTime.ToString("yyyy-MM-dd HH:mm:ss")
							 };

            return strParm;

        }

        /// <summary>
        /// 插入队列
        /// </summary>
        /// <param name="nurseAssign"></param>
        /// <returns></returns>
        public int Insert(FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign drugStoreAsign)
        {
            string strSQL = "";
            //取插入操作的SQL语句

            if (this.Sql.GetSql("Pharmacy.CallQueue.Insert", ref strSQL) == -1)
            {
                this.Err = "找不到语句Pharmacy.CallQueue.Insert";
                this.WriteErr();
                return -1;
            }
            try
            {
                strSQL = String.Format(strSQL, this.myGetParmInsertQueue(drugStoreAsign));
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错:" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新处方调剂表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        public int UpdateRecipe(string deptCode, string recipeNO)
        {
            string strSql = "";
            if(this.Sql.GetSql("Pharmacy.CallQueue.UpdateRecipe",ref strSql)== -1)
            {
                this.Err = "找不到语句Pharmacy.CallQueue.UpdateRecipe";
                this.WriteErr();
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, deptCode, recipeNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句出错" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据处方号和科室代码删除队列
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <param name="drugDeptCode"></param>
        /// <returns></returns>
        public int DeleteByRecipeNO(string recipeNO, string drugDeptCode)
        {
            string strSQL = "";
            //取插入操作的SQL语句

            if (this.Sql.GetSql("Pharmacy.CallQueue.DeleteByRecipeNO", ref strSQL) == -1)
            {
                this.Err = "找不到语句Pharmacy.CallQueue.DeleteByRecipeNO";
                this.WriteErr();
                return -1;
            }
            try
            {
                strSQL = String.Format(strSQL, recipeNO, drugDeptCode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错:" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 根据药房编码查找对应的叫号申请信息
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <param name="noon"></param>
        /// <returns></returns>
        public List<FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign> Query(string drugDeptCode)
        {
            string strSQL = "";
            //取插入操作的SQL语句

            if (this.Sql.GetSql("Pharamacy.CallQueue.Query.ByDrugDeptCode", ref strSQL) == -1)
            {
                this.Err = "找不到语句Pharamacy.CallQueue.Query.ByDrugDeptCode";
                this.WriteErr();
                return null;
            }
            strSQL = string.Format(strSQL, drugDeptCode);

            return this.query(strSQL);
        }

        private List<FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign> query(string sql)
        { 
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "基本sql出错!" + sql;
                this.ErrCode = "基本sql出错!" + sql;
                return null;
            }

            List<FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign> list = null;
            try
            {
                list = new List<FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign>();
                FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign drugStoreAsign = null;
                while (this.Reader.Read())
                {
                    drugStoreAsign = new  FS.SOC.Local.DrugStore.ShenZhen.Models.DrugStoreAsign();
                    drugStoreAsign.recipeNO = this.Reader[0].ToString();//0
                    drugStoreAsign.patientId = this.Reader[1].ToString();
                    drugStoreAsign.cardNO = this.Reader[2].ToString();
                    drugStoreAsign.patientName = this.Reader[3].ToString();
                    drugStoreAsign.patientSex = this.Reader[4].ToString();
                    drugStoreAsign.deptCode = this.Reader[5].ToString();//5
                    drugStoreAsign.drugDeptCode = this.Reader[6].ToString();
                    drugStoreAsign.sendTerminalCode = this.Reader[7].ToString();
                    drugStoreAsign.sendTerminalName = this.Reader[8].ToString();
                    drugStoreAsign.memo = this.Reader[9].ToString();
                    drugStoreAsign.Oper.ID = this.Reader[10].ToString();
                    drugStoreAsign.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[11].ToString());//15

                    list.Add(drugStoreAsign);
                }
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

            return list;
        }
        
    }
}
