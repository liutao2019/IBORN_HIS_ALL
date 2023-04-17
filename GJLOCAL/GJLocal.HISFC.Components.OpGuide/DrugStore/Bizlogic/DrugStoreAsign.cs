using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;

namespace GJLocal.HISFC.Components.OpGuide.DrugStore.Bizlogic
{
    /// <summary>
    /// 药房叫号业务层
    /// </summary>
    public class DrugStoreAsign:FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获取参数列表
        /// </summary>
        /// <param name="drugStoreAsign"></param>
        /// <returns></returns>
        protected object[] myGetParmInsertQueue(GJLocal.HISFC.Components.OpGuide.DrugStore.Models.DrugStoreAsign drugStoreAsign)
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
                                  drugStoreAsign.State,
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
        public int Insert(GJLocal.HISFC.Components.OpGuide.DrugStore.Models.DrugStoreAsign drugStoreAsign)
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
        /// 根据药房编码查找对应的叫号申请信息
        /// </summary>
        /// <param name="nurseCode"></param>
        /// <param name="noon"></param>
        /// <returns></returns>
        public List<FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign > Query(string drugDeptCode)
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

        public List<FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign > Query(string drugDeptCode, string terminalID)
        {
            string strSQL = "";
            //取插入操作的SQL语句

            if (this.Sql.GetSql("Pharamacy.CallQueue.Query.ByDrugDeptCodeAndTerminal", ref strSQL) == -1)
            {
                this.Err = "找不到语句Pharamacy.CallQueue.Query.ByDrugDeptCodeAndTerminal";
                this.WriteErr();
                return null;
            }
            strSQL = string.Format(strSQL, drugDeptCode);

            return this.query(strSQL);
        }

        private List<FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign > query(string sql)
        { 
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = "基本sql出错!" + sql;
                this.ErrCode = "基本sql出错!" + sql;
                return null;
            }

            List<FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign> list = null;
            try
            {
                list = new List<FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign>();
                FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign drugStoreAsign = null;
                while (this.Reader.Read())
                {
                    drugStoreAsign = new FS.SOC.Local.DrugStore.ZhuHai.ZDWY.Outpatient.Models.DrugStoreAsign();
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
                    drugStoreAsign.State = this.Reader[10].ToString();
                    drugStoreAsign.Oper.ID = this.Reader[11].ToString();
                    drugStoreAsign.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[12].ToString());//15

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

        public ArrayList GetUnSendData(FS.HISFC.Models.Pharmacy.DrugRecipe drugRecipe)
        {
            string strSQL = "";
            //取插入操作的SQL语句

            if (this.Sql.GetSql("Pharamacy.CallQueue.Query.GetUnSendPatientList", ref strSQL) == -1)
            {
                this.Err = "找不到语句Pharamacy.CallQueue.Query.GetUnSendPatientList";
                this.WriteErr();
                return null;
            }
            strSQL = string.Format(strSQL, drugRecipe.DrugedOper.Dept.ID,drugRecipe.SendTerminal.ID,drugRecipe.CardNO);

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "基本sql出错!" + strSQL;
                this.ErrCode = "基本sql出错!" + strSQL;
                return null;
            }

            ArrayList allGetUnSendData = null;
            try
            {
                allGetUnSendData = new ArrayList();
                FS.FrameWork.Models.NeuObject patientObj = null;
                while (this.Reader.Read())
                {
                    patientObj = new FS.FrameWork.Models.NeuObject();
                    patientObj.ID = this.Reader[0].ToString();//0
                    patientObj.Name = this.Reader[1].ToString();
                    allGetUnSendData.Add(patientObj);
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
            return allGetUnSendData;
        }

        public int UpdateQueueState(string recipeNO, string deptNO, string state)
        {
            string strSQL = "";
            //取插入操作的SQL语句

            if (this.Sql.GetSql("Pharmacy.CallQueue.UpdateState", ref strSQL) == -1)
            {
                this.Err = "找不到语句Pharmacy.CallQueue.UpdateState";
                this.WriteErr();
                return -1;
            }
            try
            {
                strSQL = String.Format(strSQL, recipeNO,deptNO,state);
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
        /// 获取处方状态
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        public string GetRecipeState(string recipeNO)
        {
            string strSql = @"SELECT Recipe_State FROM PHA_STO_RECIPE WHERE RECIPE_NO = '{0}'";
            try
            {
                strSql = String.Format(strSql, recipeNO);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错:" + ex.Message;
                this.WriteErr();
                return string.Empty;
            }
            return this.ExecSqlReturnOne(strSql,string.Empty);
        }

        /// <summary>
        /// 获取所有未取药的患者列表
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="terminalCode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public string GetUnFetchPerson(string deptCode, string terminalCode, string state)
        {
            string strSql = @"    select replace(wm_concat(distinct patient_name), ',', '     ') from pha_soc_callqueue e where e.state = '{2}'
                                           and e.drug_dept_code = '{0}'
                                           and e.send_terminal_code = '{1}'
                                           and e.oper_date > trunc(sysdate)
                                           and exists (select 1
                                                  from pha_sto_recipe f
                                                 where f.recipe_no = e.recipe_no
                                                   and f.drug_dept_code = e.drug_dept_code
                                                   and f.recipe_state in ('0', '1', '2'))
                                ";
            strSql = string.Format(strSql, deptCode, terminalCode, state);
            return this.ExecSqlReturnOne(strSql, string.Empty);
        
        }

        /// <summary>
        /// 根据终端编码获取终端名称
        /// </summary>
        /// <param name="terminalId"></param>
        /// <returns></returns>
        public  string GetTerminalNameById(string terminalId)
        {
            FS.FrameWork.Management.DataBaseManger dbMgr = new FS.FrameWork.Management.DataBaseManger();
            string terminalName = dbMgr.ExecSqlReturnOne("select t_name from pha_sto_terminal  where t_code='" + terminalId + "'");
            if (terminalName != "-1")
            {
                return terminalName;
            }
            return "";
        }

        /// <summary>
        /// 按收费时间获取处方信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="termninalCode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ArrayList QueryDrugRecipeByFeeDate(string deptCode, string termninalCode, string state)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.Sql.GetSql("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.Sql.GetSql("Pharmacy.DrugStore.GetList.ByFeeDate", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, termninalCode, state);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            return al;
        }

        /// <summary>
        /// 按收费时间获取处方信息
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="termninalCode"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public ArrayList QueryDrugRecipeByFeeDateNoCall(string deptCode, string termninalCode, string state)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.Sql.GetSql("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.Sql.GetSql("Pharmacy.DrugStore.GetList.ByFeeDate.NoCall", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, termninalCode, state);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            return al;
        }

        /// <summary>
        /// 获得门诊摆药处方(处方调剂)信息
        /// </summary>
        /// <param name="strSQL">查询的SQl语句</param>
        /// <returns>成功返回数组 失败返回null</returns>
        protected ArrayList myGetDrugRecipeInfo(string strSQL)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获取门诊处方调剂信息出错" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.DrugRecipe info;
                while (this.Reader.Read())
                {
                    #region 由结果集内读取数据
                    info = new FS.HISFC.Models.Pharmacy.DrugRecipe();

                    info.StockDept.ID = this.Reader[0].ToString();						//药房编码
                    info.RecipeNO = this.Reader[1].ToString();							//处方号
                    info.SystemType = this.Reader[2].ToString();						//出库申请分类
                    info.TransType = this.Reader[3].ToString();							//交易类型,1正交易，2反交易
                    info.RecipeState = this.Reader[4].ToString();						//处方状态: 0申请,1打印,2配药,3发药,4还药(当天未发的药品返回货价)
                    info.ClinicNO = this.Reader[5].ToString();						//门诊号
                    info.CardNO = this.Reader[6].ToString();							//病历卡号
                    info.PatientName = this.Reader[7].ToString();						//患者姓名
                    info.Sex.ID = this.Reader[8].ToString();							//性别
                    info.Age = NConvert.ToDateTime(this.Reader[9].ToString());			//年龄
                    info.PayKind.ID = this.Reader[10].ToString();						//结算类别代码
                    info.PatientDept.ID = this.Reader[11].ToString();					//患者科室编码
                    info.RegTime = NConvert.ToDateTime(this.Reader[12].ToString());		//挂号日期
                    info.Doct.ID = this.Reader[13].ToString();							//开方医师
                    info.DoctDept.ID = this.Reader[14].ToString();						//开方医师所在科室
                    info.DrugTerminal.ID = this.Reader[15].ToString();					//配药终端（打印台）
                    info.SendTerminal.ID = this.Reader[16].ToString();					//发药终端（发药窗口）
                    info.FeeOper.ID = this.Reader[17].ToString();							//收费人编码(申请人编码)
                    info.FeeOper.OperTime = NConvert.ToDateTime(this.Reader[18].ToString());		//收费时间(申请时间)
                    info.InvoiceNO = this.Reader[19].ToString();						//票据号
                    info.Cost = NConvert.ToDecimal(this.Reader[20].ToString());			//处方金额（零售金额）
                    info.RecipeQty = NConvert.ToDecimal(this.Reader[21].ToString());	//处方中药品数量(中山一用品种数)
                    info.DrugedQty = NConvert.ToDecimal(this.Reader[22].ToString());	//已配药的药品数量(中山一用品种数)
                    info.DrugedOper.ID = this.Reader[23].ToString();						//配药人
                    info.DrugedOper.Dept.ID = this.Reader[24].ToString();					    //配药科室
                    info.DrugedOper.OperTime = NConvert.ToDateTime(this.Reader[25].ToString());	//配药日期
                    info.SendOper.ID = this.Reader[26].ToString();							//发药人
                    info.SendOper.OperTime = NConvert.ToDateTime(this.Reader[27].ToString());	//发药时间
                    info.SendOper.Dept.ID = this.Reader[28].ToString();						//发药科室

                    info.ValidState = (FS.HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[29]));					//有效状态：0有效，1无效 2 发药后退费
                    info.IsModify = NConvert.ToBoolean(this.Reader[30].ToString());						//退药改药0否1是

                    info.BackOper.ID = this.Reader[31].ToString();							//-还药人
                    info.BackOper.OperTime = NConvert.ToDateTime(this.Reader[32].ToString());	//还药时间
                    info.CancelOper.ID = this.Reader[33].ToString();						//取消操作员
                    info.CancelOper.OperTime = NConvert.ToDateTime(this.Reader[34].ToString());	//取消日期
                    info.Memo = this.Reader[35].ToString();								//备注
                    info.SumDays = NConvert.ToDecimal(this.Reader[36].ToString());

                    al.Add(info);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取门诊处方调剂信息出错，执行SQL语句出错" + ex.Message;
                this.ErrCode = ex.ToString();
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
