using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.BizLogic.Pharmacy
{
    /// <summary>
    /// 【功能描述：配置中心管理类】
    /// 【创建者：曹霖】
    /// 【创建时间：2011-06]】
    /// <修改记录>
    /// <修改记录>
    /// </summary>
    public class Compound:DataBase
    {
        #region 配置中心

        /// <summary>
        /// 去配置确认表中确认对应的医嘱流水号是否已经审核过
        /// </summary>
        /// <param name="appInfo"></param>
        /// <returns></returns>
        public string GetCompoundState(FS.HISFC.Models.Pharmacy.ApplyOut appInfo)
        {
            string state = string.Empty;
            string strSql = string.Empty;
            if (this.Sql.GetSql("Pharmacy.Item.GetCompoundFlagByMorder", ref strSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetCompoundFlagByMorder字段！";
                return state;
            }
            try
            { 
                strSql = string.Format(strSql,appInfo.OrderNO);
            }
            catch(Exception ex)
            {
                this.Err = "格式化sql语句错误";
                return state;
            }
            state = this.ExecSqlReturnOne(strSql,"0") ;
            return state;
        }

        /// <summary>
        /// 取某一申请科室未被核准的申请列表	
        /// </summary>
        /// <param name="drugDeptCode">库存科室</param>
        /// <param name="applyDeptCode">申请科室编码</param>
        /// <param name="groupCode">批次</param>
        /// <param name="patientID">患者住院流水号</param>
        /// <param name="state">申请数据状态</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryCompoundApplyOut(string drugDeptCode, string applyDeptCode, string groupCode, string patientID, string state, bool isExec)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.GetApplyOutList.Patient", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.Sql.GetSql("Pharmacy.Item.QueryCompoundApplyOut.Patient.GroupCode.ApplyDept", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryCompoundApplyOut.Patient.GroupCode.ApplyDept字段!";
                return null;
            }

            #region Sql语句格式化

            if (groupCode == null)
            {
                groupCode = "U";
            }
            if (patientID == null)
            {
                patientID = "ALL";
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, drugDeptCode, applyDeptCode, groupCode, patientID, state, NConvert.ToInt32(isExec).ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            #endregion

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSelect);
        }

        /// <summary>
        /// 取某一申请科室未被核准的申请列表	
        /// </summary>
        /// <param name="drugDeptCode">库存科室</param>
        /// <param name="applyDeptCode">申请科室编码</param>
        /// <param name="groupCode">批次</param>
        /// <param name="patientID">患者住院流水号</param>
        /// <param name="state">申请数据状态</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryCompoundApplyOutByNurse(string drugDeptCode, string applyDeptCode, string groupCode, string patientID, string state, bool isExec)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.GetApplyOutList.Patient", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.Sql.GetSql("Pharmacy.Item.QueryCompoundApplyOut.Patient.GroupCode.DrugDept", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryCompoundApplyOut.Patient.GroupCode.DrugDept字段!";
                return null;
            }

            #region Sql语句格式化

            if (groupCode == null)
            {
                groupCode = "U";
            }
            if (patientID == null)
            {
                patientID = "ALL";
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, drugDeptCode, applyDeptCode, groupCode, patientID, state, NConvert.ToInt32(isExec).ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            #endregion

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSelect);
        }

        /// <summary>
        /// 根据条件取需要核对的数据
        /// </summary>
        /// <param name="strParm">查询条件</param>
        /// <returns></returns>
        public ArrayList QueryApplyOutListForJPConfirm(string[] strParm)
        {
            string strSelect = "";  //取某一科室申请，某一目标本科室未核准的SELECT语句
            string strWhere = "";  //取某一科室申请，某一目标本科室未核准的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.GetApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.Sql.GetSql("Pharmacy.Item.GetApplyOutList.CompoundConfirm", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.CompoundConfirm字段!";
                return null;
            }

            try
            {
                /// <param name="applyDeptCode">申请科室</param>
                /// <param name="targetDeptCode">库存科室</param>
                /// <param name="dtStart">开始时间</param>
                /// <param name="dtEnd">结束时间</param>
                /// <param name="drugBill">摆药单号</param>
                //string[] strParm = { applyDeptCode, targetDeptCode, dtStart, dtEnd, drugBill };
                strSelect = string.Format(strSelect + " " + strWhere, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSelect);
        }

        /// <summary>
        /// 更新批次流水号为流水号 (原始批次流水号位数过多)
        /// </summary>
        /// <param name="compoundGroup"></param>
        /// <returns></returns>
        public int UpdateCompoundGroupNO(string compoundGroup, ref string newCompoundGroupNO)
        {
            newCompoundGroupNO = this.GetNewCompoundGroup();
            if (newCompoundGroupNO == null)
            {
                return -1;
            }

            newCompoundGroupNO = compoundGroup.Substring(0, 1) + "-" + newCompoundGroupNO;

            string strSQL = "";
            //根据处方流水号和处方内序号，作废出库申请记录的Update语句
            if (this.Sql.GetSql("Pharmacy.Item.UpdateCompoundGroupNO", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateCompoundGroupNO";
                return -1;
            }

            strSQL = string.Format(strSQL, compoundGroup, newCompoundGroupNO);

            int parm = this.ExecNoQuery(strSQL);
            if (parm != 1)
            {
                return parm;
            }

            return 1;
        }

        /// <summary>
        /// 获取新配置批次流水号
        /// </summary>
        /// <returns></returns>
        public string GetNewCompoundGroup()
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.GetNewCompoundGroup", ref strSQL) == -1)
                return null;
            string strReturn = this.ExecSqlReturnOne(strSQL);
            if (strReturn == "-1")
            {
                this.Err = "获取新配置批次流水号时出错！" + this.Err;
                return null;
            }
            return strReturn;
        }


        /// <summary>
        /// 获取配置中心列表
        /// </summary>
        /// <param name="drugDeptCode">库存科室</param>
        /// <param name="groupCode">批次</param>
        /// <param name="state">状态</param>
        /// <param name="drugCode">药品编码</param>
        /// <param name="isOnlyValid">是否仅包含有效数据 True 仅包含有效数据</param>
        /// <returns>成功返回待配置患者列表 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.ApplyOut> QueryCompoundList(string drugDeptCode, string groupCode, string state, string drugCode, bool isOnlyValid)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (isOnlyValid == true)            //仅检索有效数据
            {
                if (this.Sql.GetSql("Pharmacy.Item.QueryCompoundList", ref strSelect) == -1)
                {
                    this.Err = "没有找到Pharmacy.Item.QueryCompoundList字段!";
                    return null;
                }
            }
            else                       //包含有效、无效数据
            {
                if (this.Sql.GetSql("Pharmacy.Item.QueryCompoundList.ContainUnValid", ref strSelect) == -1)
                {
                    this.Err = "没有找到Pharmacy.Item.QueryCompoundList.ContainUnValid字段!";
                    return null;
                }
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, drugDeptCode, groupCode, state, drugCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            #region 执行Sql语句由Reader内获取数据

            //根据SQL语句取数组并返回数组
            List<FS.HISFC.Models.Pharmacy.ApplyOut> applyList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "获取待配置列表时发生错误：" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.ApplyOut();

                    info.StockDept.ID = drugDeptCode;
                    info.ApplyDept.ID = this.Reader[0].ToString();              //申请科室
                    info.PatientNO = this.Reader[1].ToString();                 //患者住院流水号
                    info.User01 = this.Reader[2].ToString();                    //床号
                    info.User02 = this.Reader[3].ToString();                    //姓名

                    applyList.Add(info);
                }

                return applyList;
            }
            catch (Exception ex)
            {
                this.Err = "获得申请患者列表时，执行SQL语句出错" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            #endregion
        }

        /// <summary>
        /// 根据时间获取已发药数据
        /// </summary>
        /// <param name="drugDeptCode"></param>
        /// <param name="groupCode"></param>
        /// <param name="state"></param>
        /// <param name="drugCode"></param>
        /// <param name="isOnlyValid"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.ApplyOut> QueryCompoundListByUseTime(string drugDeptCode, string groupCode, string state, string drugCode, bool isOnlyValid, DateTime dtBegin, DateTime dtEnd)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (isOnlyValid == true)            //仅检索有效数据
            {
                if (this.Sql.GetSql("Pharmacy.Item.QueryCompoundListByDate", ref strSelect) == -1)
                {
                    this.Err = "没有找到Pharmacy.Item.QueryCompoundListByDate字段!";
                    return null;
                }
            }
            else                       //包含有效、无效数据
            {
                if (this.Sql.GetSql("Pharmacy.Item.QueryCompoundList.ContainUnValid", ref strSelect) == -1)
                {
                    this.Err = "没有找到Pharmacy.Item.QueryCompoundList.ContainUnValid字段!";
                    return null;
                }
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, drugDeptCode, groupCode, state, drugCode,dtBegin.ToString(),dtEnd.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            #region 执行Sql语句由Reader内获取数据

            //根据SQL语句取数组并返回数组
            List<FS.HISFC.Models.Pharmacy.ApplyOut> applyList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "获取待配置列表时发生错误：" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.ApplyOut();

                    info.StockDept.ID = drugDeptCode;
                    info.ApplyDept.ID = this.Reader[0].ToString();              //申请科室
                    info.PatientNO = this.Reader[1].ToString();                 //患者住院流水号
                    info.User01 = this.Reader[2].ToString();                    //床号
                    info.User02 = this.Reader[3].ToString();                    //姓名

                    applyList.Add(info);
                }

                return applyList;
            }
            catch (Exception ex)
            {
                this.Err = "获得申请患者列表时，执行SQL语句出错" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            #endregion
        }


        /// <summary>
        /// 获取配置中心列表
        /// </summary>
        /// <param name="drugDeptCode">库存科室</param>
        /// <param name="state">状态</param>        
        /// <param name="isExecCompound">是否已执行配置</param>
        /// <returns>成功返回待配置患者列表 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.ApplyOut> QueryCompoundList(string drugDeptCode, string state, bool isExecCompound)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.QueryCompoundList.ExecState", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryCompoundList.ExecState字段!";
                return null;
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, drugDeptCode, state, NConvert.ToInt32(isExecCompound).ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            #region 执行Sql语句由Reader内获取数据

            //根据SQL语句取数组并返回数组
            List<FS.HISFC.Models.Pharmacy.ApplyOut> applyList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "获取待配置列表时发生错误：" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.ApplyOut();

                    info.StockDept.ID = drugDeptCode;
                    info.ApplyDept.ID = this.Reader[0].ToString();              //申请科室
                    info.PatientNO = this.Reader[1].ToString();                 //患者住院流水号
                    info.User01 = this.Reader[2].ToString();                    //床号
                    info.User02 = this.Reader[3].ToString();                    //姓名

                    applyList.Add(info);
                }

                return applyList;
            }
            catch (Exception ex)
            {
                this.Err = "获得申请患者列表时，执行SQL语句出错" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            #endregion
        }

        /// <summary>
        /// 按时间查询配置中心列表
        /// </summary>
        /// <param name="drugDeptCode"></param>
        /// <param name="applyDeptCode"></param>
        /// <param name="groupCode"></param>
        /// <param name="patientID"></param>
        /// <param name="state"></param>
        /// <param name="isExec"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public ArrayList QueryCompoundApplyOutByUseTime(string drugDeptCode, string applyDeptCode, string groupCode, string patientID, string state, bool isExec,DateTime dtBegin,DateTime dtEnd)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.GetApplyOutList.Patient", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.Sql.GetSql("Pharmacy.Item.QueryCompoundApplyOut.Patient.GroupCode.ApplyDept.Date", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryCompoundApplyOut.Patient.GroupCode.ApplyDept.Date字段!";
                return null;
            }

            #region Sql语句格式化

            if (groupCode == null)
            {
                groupCode = "U";
            }
            if (patientID == null)
            {
                patientID = "ALL";
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, drugDeptCode, applyDeptCode, groupCode, patientID, state, NConvert.ToInt32(isExec).ToString(),dtBegin.ToString(),dtEnd.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            #endregion

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSelect);
        }


        /// <summary>
        /// 获取配置中心列表
        /// </summary>
        /// <param name="drugDeptCode">库存科室</param>
        /// <param name="groupCode">批次</param>
        /// <param name="state">状态</param>
        /// <param name="drugCode">药品编码</param>
        /// <param name="isOnlyValid">是否仅包含有效数据 True 仅包含有效数据</param>
        /// <returns>成功返回待配置患者列表 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.ApplyOut> QueryCompoundList(string drugDeptCode, string groupCode, string state, string drugCode, bool isOnlyValid, bool isPrint)
        {
            if (!isPrint)
            {
                return this.QueryCompoundList(drugDeptCode, groupCode, state, drugCode, isOnlyValid);
            }

            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.QueryCompoundList.ConFrimPrint", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryCompoundList字段!";
                return null;
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, drugDeptCode, groupCode, state, drugCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }


            //根据SQL语句取数组并返回数组
            List<FS.HISFC.Models.Pharmacy.ApplyOut> applyList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "获取待配置列表时发生错误：" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.ApplyOut();

                    info.StockDept.ID = drugDeptCode;
                    info.ApplyDept.ID = this.Reader[0].ToString();              //申请科室
                    info.PatientNO = this.Reader[1].ToString();                 //患者住院流水号
                    info.User01 = this.Reader[2].ToString();                    //床号
                    info.User02 = this.Reader[3].ToString();                    //姓名

                    applyList.Add(info);
                }

                return applyList;
            }
            catch (Exception ex)
            {
                this.Err = "获得申请患者列表时，执行SQL语句出错" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 根据申请流水号更新申请状态
        /// </summary>
        /// <param name="applyID">申请流水号</param>
        /// <param name="oldState">就状态</param>
        /// <param name="newState">新状态</param>
        /// <returns></returns>
        public int UpdateApplyOutState(decimal applyID, string oldState, string newState)
        {
            string strSQL = "";
            if (this.Sql.GetSql("Pharmacy.Item.UpdateApplyOutState.Compound", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateApplyOutState.Compound";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, applyID.ToString(), oldState, newState);
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.UpdateApplyOutState.Compound";
                return -1;
            }
            return this.ExecNoQuery(strSQL);

        }


        /// <summary>
        /// 更新已打印标记
        /// </summary>
        /// <param name="applyID">申请流水号</param>
        /// <param name="isPrint">是否已打印</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int UpdateApplyOutPrintState(string applyID, bool isPrint)
        {
            string strSQL = "";
            if (this.Sql.GetSql("Pharmacy.Item.UpdateApplyOutPrintState", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateApplyOutPrintState";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, applyID, NConvert.ToInt32(isPrint));
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.UpdateApplyOutPrintState";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }


        /// <summary>
        /// 更新出库申请表中的打印状态为已打印
        /// 需要的数据：出库申请单流水号
        /// </summary>
        /// <param name="applyOut">出库申请记录</param>
        /// <returns>0没有更新（并发） 1成功 -1失败</returns>
        public int ExamApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            string strSQL = "";

            try
            {
                // 只打印摆药单。更新摆药状态为1
                if (applyOut.State == "1")
                {
                    //审批出库申请（打印摆药单），更新出库申请表中的打印状态为已打印，摆药单流水号，打印人，打印日期（系统时间）

                    //清空核准数据项中的数值
                    applyOut.Operation.ApproveOper.ID = "";            //核准人
                    applyOut.Operation.ApproveOper.OperTime = DateTime.MinValue; //核准日期
                    applyOut.Operation.ApproveOper.Dept.ID = "";             //核准科室
                }

                //取SQL语句
                if (this.Sql.GetSql("Pharmacy.Item.ExamApplyOut", ref strSQL) == -1)
                {
                    this.Err = "没有找到SQL语句Pharmacy.Item.ExamApplyOut";
                    return -1;
                }

                //取参数列表
                string[] strParm = {
									   applyOut.ID,                                         //出库申请单流水号
									   applyOut.State,                                      //出库申请状态
									   applyOut.Operation.ApproveOper.ID,                   //核准人
									   applyOut.Operation.ApproveOper.OperTime.ToString(),  //核准日期
									   applyOut.Operation.ApproveOper.Dept.ID,              //核准科室
									   applyOut.DrugNO,                                     //摆药单流水号
									   applyOut.Operation.ApproveQty.ToString(),            //核准数量
									   this.Operator.ID,                                    //打印人
									   applyOut.Operation.ExamOper.OperTime.ToString(),    //打印时间
									   applyOut.PlaceNO,     		                        //货位号
                                       NConvert.ToInt32(applyOut.IsCharge).ToString(),      //收费标记
                                       applyOut.RecipeNO,                                   //处方号
                                       applyOut.SequenceNO.ToString()                       //处方内项目流水号
								   };


                strSQL = string.Format(strSQL, strParm);          //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "审批出库申请SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新出库信息
        /// </summary>
        /// <param name="inputStore"></param>
        /// <returns></returns>
        public int UpdateOutputInfo(FS.HISFC.Models.IMA.IMAStoreBase inputStore)
        {
            FS.HISFC.Models.Pharmacy.Input input = inputStore as FS.HISFC.Models.Pharmacy.Input;

            int parm;
            ArrayList alOutput;

            alOutput = this.QueryOutputList(input.OutBillNO);
            if (alOutput == null)
            {
                this.Err = "更新出库记录过程中 获取出库记录出错！";
                return -1;
            }

            FS.HISFC.Models.Pharmacy.Output output;
            for (int i = 0; i < alOutput.Count; i++)
            {
                output = alOutput[i] as FS.HISFC.Models.Pharmacy.Output;
                if (output == null)
                {
                    this.Err = "更新出库记录过程中 数据类型转换出错！";
                    return -1;
                }
                output.State = "2";
                output.InListNO = input.InListNO;
                output.InBillNO = input.ID;

                parm = this.UpdateOutput(output);
                if (parm == -1)
                {
                    this.Err = "更新出库记录执行出错！";
                    return -1;
                }
            }

            return 1;
        }

        /// <summary>
        /// 按出库单流水号查询出库记录（可能多条）
        /// </summary>
        /// <returns>成功返回满足条件的出库记录 失败返回null</returns>
        public ArrayList QueryOutputList(string outputID)
        {
            string strSQL = "";
            string strWhere = "";

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.GetOutputList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.Sql.GetSql("Pharmacy.Item.GetOutputList.ByID", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetOutputList.ByID字段!";
                return null;
            }

            strSQL = string.Format(strSQL + strWhere, outputID);

            //根据SQL语句取药品类数组并返回数组
            return this.myGetOutput(strSQL);
        }

        /// <summary>
        /// 配置确认
        /// </summary>
        /// <param name="info">待确认数据</param>
        /// <param name="compoundOper">配置确认人</param>
        /// <param name="isExec">是否执行</param>
        /// <returns>成功返回大于1 更新函数 失败返回－1</returns>
        public int UpdateCompoundApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut info, FS.HISFC.Models.Base.OperEnvironment compoundOper, bool isExec)
        {
            string strSQL = "";
            //根据处方流水号和处方内序号，作废出库申请记录的Update语句
            if (this.Sql.GetSql("Pharmacy.Item.UpdateCompoundApplyOut", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateCompoundApplyOut";
                return -1;
            }

            strSQL = string.Format(strSQL, info.ID, compoundOper.ID, compoundOper.OperTime.ToString(), NConvert.ToInt32(isExec));
            int parm = this.ExecNoQuery(strSQL);
            if (parm != 1)
            {
                return parm;
            }

            return 1;
        }

        /// <summary>
        /// 向审核表中存入已审核的数据，主要为了对长期医嘱的校验
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int InsertCheckApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut info)
        {
            string strSQL = "";
            if (this.Sql.GetSql("Pharmacy.Item.InsertCheckApplyOut", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.InsertCheckApplyOut";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, info.OrderNO);
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.InsertCheckApplyOut";
                return -1;
            }
            return this.ExecNoQuery(strSQL);

        }

        /// <summary>
        /// 更新批次号{432F8D1A-80F9-45e1-9FE6-7332C49487BA}feng.ch
        /// </summary>
        /// <param name="applyNo">申请流水号</param>
        /// <param name="newCompoundGroup">新批次号</param>
        /// <returns>成功返回1，否则-1</returns>
        public int UpdateCompoundGroup(string applyNo, string newCompoundGroup)
        {
            string strSQL = "";
            if (this.Sql.GetSql("Pharmacy.Item.UpdateCompoundGroup", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateCompoundGroup";
                return -1;
            }
            strSQL = string.Format(strSQL, applyNo, newCompoundGroup);

            int parm = this.ExecNoQuery(strSQL);
            if (parm != 1)
            {
                return parm;
            }

            return 1;
        }

        /// <summary>
        /// 更新出库申请记录
        /// </summary>
        /// <param name="applyOut">出库申请记录</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            return this.UpdateApplyOut(applyOut, false);
        }

        /// <summary>
        /// 更新出库申请记录
        /// {EE05DA01-8969-404d-9A6B-EE8AD0BC1CD0}
        /// </summary>
        /// <param name="applyOut">出库申请记录</param>
        /// <param name="isApplyState">是否判断是申请状态</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, bool isJudgeApplyState)
        {
            string strSQL = "";
            if (this.Sql.GetSql("Pharmacy.Item.UpdateApplyOut", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = myGetParmApplyOut(applyOut);  //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新出库申请SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            string strWhere = "";
            if (isJudgeApplyState)
            {
                if (this.Sql.GetSql("Pharmacy.Item.UpdateApplyOutByApplyState", ref strWhere) == -1)
                {
                    this.Err += "获取Pharmacy.Item.UpdateApplyOutByApplyState语句出错！";
                    return -1;
                }
            }
            return this.ExecNoQuery(strSQL + strWhere);
        }

        /// <summary>
        /// 获得update或者insert出库申请表的传入参数数组
        /// 
        /// </summary>
        /// <param name="ApplyOut">出库申请类</param>
        /// <returns>成功返回参数字符串数组 失败返回null</returns>
        private string[] myGetParmApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut ApplyOut)
        {
            //默认申请状态为:0申请状态
            if (ApplyOut.State == null || ApplyOut.State == "")
                ApplyOut.State = "0";
            if (ApplyOut.User03 == null || ApplyOut.User03 == "")
            {
                ApplyOut.User03 = System.DateTime.MinValue.ToString();
            }
            string applyOper = ApplyOut.Operation.ApplyOper.ID;
            if (applyOper == "")
            {
                applyOper = this.Operator.ID;
            }

            string[] strParm ={   ApplyOut.ID,                                 //0申请流水号
								 ApplyOut.ApplyDept.ID,                       //1申请部门编码（科室或者病区）
								 ApplyOut.StockDept.ID,                      //2发药部门编码
								 ApplyOut.SystemType,                          //3出库申请分类
								 ApplyOut.GroupNO.ToString(),                 //4批次号
								 ApplyOut.Item.ID,                            //5药品编码
								 ApplyOut.Item.Name,                          //6药品商品名
								 ApplyOut.BatchNO,                            //7批号
								 ApplyOut.Item.Type.ID,                       //8药品类别
								 ApplyOut.Item.Quality.ID.ToString(),         //9药品性质
								 ApplyOut.Item.Specs,                         //10规格
								 ApplyOut.Item.PackUnit,                      //11包装单位
								 ApplyOut.Item.PackQty.ToString(),            //12包装数
								 ApplyOut.Item.MinUnit,                       //13最小单位
								 ApplyOut.ShowState,                          //14显示的单位标记
								 ApplyOut.ShowUnit,                           //15显示的单位
								 ApplyOut.Item.PriceCollection.RetailPrice.ToString(),        //16零售价
								 ApplyOut.Item.PriceCollection.WholeSalePrice.ToString(),     //17批发价
								 ApplyOut.Item.PriceCollection.PurchasePrice.ToString(),      //18购入价
								 ApplyOut.BillNO,                           //19申请单号
								 applyOper,                                 //20申请人编码
								 ApplyOut.Operation.ApplyOper.OperTime.ToString(),               //21申请日期
								 ApplyOut.State,                         //22申请状态 0申请，1核准（出库），2作废，3暂不摆药
								 ApplyOut.Operation.ApplyQty.ToString(),                //23申请出库量(每付的总数量)
								 ApplyOut.Days.ToString(),                    //24付数（草药）
								 NConvert.ToInt32(ApplyOut.IsPreOut).ToString(), //25预扣库存状态（'0'不预扣库存，'1'预扣库存）
								 NConvert.ToInt32(ApplyOut.IsCharge).ToString(), //26收费状态：0未收费，1已收费
								 ApplyOut.PatientNO,                          //27患者编号
								 ApplyOut.PatientDept.ID,                     //28患者科室
								 ApplyOut.DrugNO,                           //29摆药单号
								 ApplyOut.Operation.ApproveOper.Dept.ID,                     //30摆药科室
								 ApplyOut.Operation.ApproveOper.ID,                    //31摆药人
								 ApplyOut.Operation.ApproveOper.OperTime.ToString(),             //32摆药日期
								 ApplyOut.Operation.ApproveQty.ToString(),              //33摆药数量
								 ApplyOut.DoseOnce.ToString(),                //34每次剂量
								 ApplyOut.Item.DoseUnit,                      //35剂量单位
								 ApplyOut.Usage.ID,                           //36用法代码
								 ApplyOut.Usage.Name,                         //37用法名称
								 ApplyOut.Frequency.ID,                       //38频次代码
								 ApplyOut.Frequency.Name,                     //39频次名称
								 ApplyOut.Item.DosageForm.ID,                 //40剂型编码
								 ApplyOut.OrderType.ID,                       //41医嘱类型
								 ApplyOut.OrderNO,                            //42医嘱流水号
								 ApplyOut.CombNO,                             //43组合序号
								 ApplyOut.ExecNO,                             //44执行单流水号
								 ApplyOut.RecipeNO,                           //45处方号
								 ApplyOut.SequenceNO.ToString(),              //46处方内项目流水号
								 ApplyOut.SendType.ToString(),                //47医嘱发送类型1集中，2临时
								 ApplyOut.BillClassNO,                        //48摆药单分类
								 ApplyOut.PrintState,                         //49打印状态
								 ApplyOut.OutBillNO,                          //50出库单号（退库申请时，保存出库时对应的记录）
								 ((int)ApplyOut.ValidState).ToString(),	      //51有效标记（1有效，0无效，不摆药）
								 ApplyOut.Memo,								  //52医嘱备注
								 ApplyOut.PlaceNO,						      //53货位号
								 ApplyOut.User03,							  //54取消日期(用药时间)
                                 ApplyOut.RecipeInfo.Dept.ID,
                                 ApplyOut.RecipeInfo.ID,
                                 NConvert.ToInt32(ApplyOut.IsBaby).ToString(),
                                 ApplyOut.ExtFlag,
                                 ApplyOut.ExtFlag1,
                                 ApplyOut.CompoundGroup,
                                 NConvert.ToInt32(ApplyOut.Compound.IsNeedCompound).ToString(),
                                 NConvert.ToInt32(ApplyOut.Compound.IsExec).ToString(),
                                 ApplyOut.Compound.CompoundOper.ID,
                                 ApplyOut.Compound.CompoundOper.OperTime.ToString(),
							 };

            return strParm;
        }

        /// <summary>
        /// 配置信息检索。根据批次流水号
        /// </summary>
        /// <param name="compoundGroup">批次流水号</param>
        /// <returns></returns>
        public ArrayList QueryCompoundApplyOut(string compoundGroup)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.GetApplyOutList.Patient", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.Sql.GetSql("Pharmacy.Item.QueryCompoundApplyOut.CompoundGroup", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryCompoundApplyOut.CompoundGroup字段!";
                return null;
            }

            #region Sql语句格式化

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, compoundGroup);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            #endregion

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSelect);
        }


        /// <summary>
        /// 根据条件查询配液信息
        /// </summary>
        /// <param name="parm">传入参数</param>
        /// <returns></returns>
        public ArrayList QueryCompoundInfoByParm(string[] parm)
        {
            string strSelect = @"SELECT DISTINCT A.DEPT_CODE,
                                                FUN_GET_DEPT_NAME(A.DEPT_CODE),
                                                A.COMPOUND_GROUP,
                                                A.CANCEL_DATE
                                  FROM PHA_COM_APPLYOUT A
                                WHERE (A.DRUG_DEPT_CODE = '{0}' OR '{0}' = 'A') 
                               AND (A.DRUGED_BILL = '{1}' OR '{1}' = 'A')
                               AND (A.CANCEL_DATE >= TO_DATE('{2}', 'yyyy-MM-dd hh24:mi:ss') OR '{2}'='A')
                               AND (A.CANCEL_DATE <= TO_DATE('{3}', 'yyyy-MM-dd hh24:mi:ss') OR '{3}'='A')
                               AND (A.COMPOUND_GROUP = '{4}' OR '{4}' = 'A')
                               AND A.APPLY_STATE = '{5}'
                               AND A.CLASS3_MEANING_CODE = '{6}'
                               AND A.VALID_STATE = '1'
                             GROUP BY A.DEPT_CODE, A.COMPOUND_GROUP, A.CANCEL_DATE
                             ORDER BY A.DEPT_CODE, A.COMPOUND_GROUP, A.CANCEL_DATE";
            #region Sql语句格式化

            try
            {
                strSelect = string.Format(strSelect, parm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            #endregion

            ArrayList al = new ArrayList();              //用于返回出库申请信息的数组
            FS.FrameWork.Models.NeuObject info; //返回数组中的出库申请类

            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "获取配液中心配液信息出错！" + this.Err;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                                  //申请科室编码
                        info.Name = this.Reader[1].ToString();                                //申请科室名称
                        info.Memo = this.Reader[2].ToString();                                //批次号
                        info.User01 = this.Reader[3].ToString();                              //用药时间
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获取配液中心配液信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }
                return al;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获取配液中心配液信息出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 根据摆药单号取配液中心摆药信息
        /// //{EAE4936B-9BD8-433a-BBB1-C00924104155}
        /// </summary>
        /// <param name="drugBillNo">摆药单号</param>
        /// <param name="isDrugBillNo">是否是摆药单号</param>
        /// <returns></returns>
        public ArrayList QueryCompoundApplyOutForJP(string drugBillNo, bool isDrugBillNo, string drugDept, string sysType)
        {
            string strSelect = "";

            string strWhere = "";
            if (isDrugBillNo)
            {
                strWhere = @"  AND PHA_COM_APPLYOUT.DRUGED_BILL = '{0}'
                               AND PHA_COM_APPLYOUT.DRUG_DEPT_CODE = '{1}'
                               AND PHA_COM_APPLYOUT.CLASS3_MEANING_CODE = '{2}'
                               AND PHA_COM_APPLYOUT.APPLY_STATE = '2'
                             ORDER BY PHA_COM_APPLYOUT.DEPT_CODE,
                                      substr(PHA_COM_APPLYOUT.PATIENT_ID,5,10),
                                      PHA_COM_APPLYOUT.COMB_NO,
                                      PHA_COM_APPLYOUT.CANCEL_DATE ASC";
            }
            else
            {
                strWhere = @"  AND PHA_COM_APPLYOUT.COMPOUND_GROUP = '{0}'
                               AND PHA_COM_APPLYOUT.DRUG_DEPT_CODE = '{1}'
                               AND PHA_COM_APPLYOUT.CLASS3_MEANING_CODE = '{2}'
                               AND PHA_COM_APPLYOUT.APPLY_STATE = '2'
                             ORDER BY PHA_COM_APPLYOUT.DEPT_CODE,      
                                      substr(PHA_COM_APPLYOUT.PATIENT_ID,5,10),
                                      PHA_COM_APPLYOUT.COMB_NO,
                                      PHA_COM_APPLYOUT.CANCEL_DATE ASC";
            }
            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.GetApplyOutList.PatientForJP", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.PatientForJP字段!";
                return null;
            }

            #region Sql语句格式化

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, drugBillNo, drugDept, sysType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            #endregion

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSelect);
        }

        /// <summary>
        /// 取药品基本信息列表，可能是一条或者多条药品记录
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>药品对象数组</returns>
        private ArrayList myGetOutput(string SQLString)
        {
            ArrayList al = new ArrayList();                //用于返回药品信息的数组
            FS.HISFC.Models.Pharmacy.Output output; //返回数组中的出库实体

            this.ExecQuery(SQLString);
            try
            {
                while (this.Reader.Read())
                {
                    output = new FS.HISFC.Models.Pharmacy.Output();
                    try
                    {
                        #region 由结果集读取数据
                        output.StockDept.ID = this.Reader[0].ToString();                                  //0出库科室编码
                        output.ID = this.Reader[1].ToString();                                       //1出库单流水号
                        output.SerialNO = NConvert.ToInt32(this.Reader[2].ToString());               //2序号
                        output.GroupNO = NConvert.ToDecimal(this.Reader[3].ToString());                //3批次号
                        output.OutListNO = this.Reader[4].ToString();                              //4出库单据号
                        output.PrivType = this.Reader[5].ToString();                                 //5出库类型
                        output.SystemType = this.Reader[6].ToString();                               //6出库分类
                        output.InBillNO = this.Reader[7].ToString();                               //7入库单流水号
                        output.InSerialNO = NConvert.ToInt32(this.Reader[8].ToString());             //8入库单序号
                        output.InListNO = this.Reader[9].ToString();                               //9入库单据号
                        output.Item.ID = this.Reader[10].ToString();                                 //10药品编码
                        output.Item.Name = this.Reader[11].ToString();                               //11药品商品名
                        output.Item.Type.ID = this.Reader[12].ToString();                            //12药品类别
                        output.Item.Quality.ID = this.Reader[13].ToString();                         //13药品性质
                        output.Item.Specs = this.Reader[14].ToString();                              //14规格
                        output.Item.PackUnit = this.Reader[15].ToString();                           //15包装单位
                        output.Item.PackQty = NConvert.ToDecimal(this.Reader[16].ToString());        //16包装数
                        output.Item.MinUnit = this.Reader[17].ToString();                            //17最小单位
                        output.ShowState = this.Reader[18].ToString();                               //18显示的单位标记
                        output.BatchNO = this.Reader[19].ToString();                                 //19批号
                        output.ValidTime = NConvert.ToDateTime(this.Reader[20].ToString());          //20有效期
                        output.Producer.ID = this.Reader[21].ToString();                             //21生产厂家代码
                        output.Company.ID = this.Reader[22].ToString();                              //22供货单位代码
                        output.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[23].ToString());    //23零售价
                        output.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[24].ToString()); //24批发价
                        output.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[25].ToString());  //25购入价
                        output.Quantity = NConvert.ToDecimal(this.Reader[26].ToString());            //26出库量
                        output.RetailCost = NConvert.ToDecimal(this.Reader[27].ToString());          //27零售金额
                        output.WholeSaleCost = NConvert.ToDecimal(this.Reader[28].ToString());       //28批发金额
                        output.PurchaseCost = NConvert.ToDecimal(this.Reader[29].ToString());        //39购入金额
                        output.StoreQty = NConvert.ToDecimal(this.Reader[30].ToString());            //30出库后库存数量
                        output.StoreCost = NConvert.ToDecimal(this.Reader[31].ToString());           //31出库后库存总金额
                        output.SpecialFlag = this.Reader[32].ToString();                             //32特殊标记。1是，0否
                        output.State = this.Reader[33].ToString();                                   //33出库状态 0申请、1审批、2核准
                        output.Operation.ApplyQty = NConvert.ToDecimal(this.Reader[34].ToString());            //34申请数量
                        output.Operation.ApplyOper.ID = this.Reader[35].ToString();                           //35申请出库人
                        output.Operation.ApplyOper.OperTime = NConvert.ToDateTime(this.Reader[36].ToString());          //36申请出库日期
                        output.Operation.ExamQty = NConvert.ToDecimal(this.Reader[37].ToString());            //37审批数量
                        output.Operation.ExamOper.ID = this.Reader[38].ToString();                            //38审批人
                        output.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[39].ToString());           //39审批日期
                        output.Operation.ApproveOper.ID = this.Reader[40].ToString();                         //40核准人
                        output.Operation.ApproveOper.OperTime = NConvert.ToDateTime(this.Reader[41].ToString());        //41核准日期
                        output.PlaceNO = this.Reader[42].ToString();                               //42货位号
                        output.Operation.ReturnQty = NConvert.ToDecimal(this.Reader[43].ToString());          //43退库数量
                        output.DrugedBillNO = this.Reader[44].ToString();                          //44摆药单号
                        output.MedNO = this.Reader[45].ToString();                                   //45制剂序号－生产序号或检验序号
                        output.TargetDept.ID = this.Reader[46].ToString();                           //46领药单位编码
                        output.RecipeNO = this.Reader[47].ToString();                                //47处方号
                        output.SequenceNO = NConvert.ToInt32(this.Reader[48].ToString());           //48处方流水号
                        output.GetPerson = this.Reader[49].ToString();                               //49领药人
                        output.Memo = this.Reader[50].ToString();                                    //50备注
                        output.Operation.Oper.ID = this.Reader[51].ToString();                                //51操作员
                        output.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[52].ToString());           //52操作日期
                        output.IsArkManager = NConvert.ToBoolean(this.Reader[53]);
                        output.ArkOutNO = this.Reader[54].ToString();

                        #endregion
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得药品基本信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(output);
                }

                return al;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得药品基本信息时，执行SQL语句出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 更新一条出库记录
        /// </summary>
        /// <param name="output">出库记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateOutput(FS.HISFC.Models.Pharmacy.Output output)
        {
            string strSQL = "";
            if (this.Sql.GetSql("Pharmacy.Item.UpdateOutput", ref strSQL) == -1)
            {
                this.Err = "找不到SQL语句！Pharmacy.Item.UpdateOutput";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmOutput(output);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新出库记录SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 获得update或者insert出库表的传入参数数组
        /// </summary>
        /// <param name="output">出库类</param>
        /// <returns>成功返回字符串数组 失败返回null</returns>
        private string[] myGetParmOutput(FS.HISFC.Models.Pharmacy.Output output)
        {
            #region "接口说明"

            #endregion

            string arkNO = "0";
            if (output.ArkOutNO != null && output.ArkOutNO != "")
            {
                arkNO = output.ArkOutNO;
            }

            string[] strParm ={
								 output.StockDept.ID,                        //0出库科室编码
								 output.ID,                             //1出库单流水号
								 output.SerialNO.ToString(),            //2序号
								 output.GroupNO.ToString(),             //3批次号
								 output.OutListNO,                    //4出库单据号
								 output.PrivType,                       //5出库类型
								 output.SystemType,                     //6出库分类
								 output.InBillNO,                     //7入库单流水号
								 output.InSerialNO.ToString(),          //8入库单序号
								 output.InListNO,                     //9入库单据号
								 output.Item.ID,                        //10药品编码
								 output.Item.Name,                      //11药品商品名
								 output.Item.Type.ID,                   //12药品类别
								 output.Item.Quality.ID.ToString(),     //13药品性质
								 output.Item.Specs,                     //14规格
								 output.Item.PackUnit,                  //15包装单位
								 output.Item.PackQty.ToString(),        //16包装数
								 output.Item.MinUnit,                   //17最小单位
								 output.ShowState,                      //18显示的单位标记
								 output.ShowUnit,                       //19显示的单位
								 output.BatchNO,                        //20批号
								 output.ValidTime.ToString(),           //21有效期
								 output.Producer.ID,                    //22生产厂家代码
								 output.Company.ID,                     //23供货单位代码
								 output.Item.PriceCollection.RetailPrice.ToString(),    //24零售价
								 output.Item.PriceCollection.WholeSalePrice.ToString(), //25批发价
								 output.Item.PriceCollection.PurchasePrice.ToString(),  //26购入价
								 output.Quantity.ToString(),            //27出库量
								 (output.Quantity * output.Item.PriceCollection.RetailPrice / output.Item.PackQty).ToString(),          //28零售金额
                                 (output.Quantity * output.Item.PriceCollection.WholeSalePrice / output.Item.PackQty).ToString(),       //29批发金额
								 (output.Quantity * output.Item.PriceCollection.PurchasePrice / output.Item.PackQty).ToString(),        //30购入金额
								 output.StoreQty.ToString(),            //31出库后库存数量
								 output.StoreCost.ToString(),           //32出库后库存总金额
								 output.SpecialFlag,                    //33特殊标记。1是，0否
								 output.State,                          //34出库状态 0申请、1审批、2核准
								 output.Operation.ApplyQty.ToString(),            //35申请数量
								 output.Operation.ApplyOper.ID,                  //36申请出库人
								 output.Operation.ApplyOper.OperTime.ToString(),           //37申请出库日期
								 output.Operation.ExamQty.ToString(),             //38审批数量
								 output.Operation.ExamOper.ID,                   //39审批人
								 output.Operation.ExamOper.OperTime.ToString(),            //40审批日期
								 output.Operation.ApproveOper.ID,                //41核准人
								 output.PlaceNO,                      //42货位号
								 output.Operation.ReturnQty.ToString(),           //43退库数量
								 output.DrugedBillNO,                 //44摆药单号
								 output.MedNO,                          //45制剂序号－生产序号或检验序号
								 output.TargetDept.ID,                  //46领药单位编码
								 output.RecipeNO,                       //47处方号
								 output.SequenceNO.ToString(),          //48处方流水号
								 output.GetPerson,                      //49领药人
								 output.Memo,                           //50备注
								 this.Operator.ID,                      //51操作员
                                 NConvert.ToInt32(output.IsArkManager).ToString(),
                                 arkNO,
                                 //{F46D26C1-FBA7-44bc-9323-BEC9CD2115F9}  出/退库记录发生时间
                                 output.OutDate.ToString(),
                                 //output.Laboratory_Date.ToString(),      // {D1D2B4D0-6C6A-4e64-9F2A-F37FF405484F} youxq  2012-9-10 
                                 //output.Laboratory_Dept.ToString()      //{D1D2B4D0-6C6A-4e64-9F2A-F37FF405484F}  youxq  2012-9-10 

			};
            return strParm;
        }

        /// <summary>
        /// 取出库申请表中的全部字段数据
        /// 私有方法，在其他方法中调用  
        /// 使用该函数的索引 : Pharmacy.Item.GetApplyOutList Pharmacy.Item.GetApplyOutList.Patient
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>成功返回出库申请实体数组 失败返回null</returns>
        private ArrayList myGetApplyOut(string SQLString)
        {
            ArrayList al = new ArrayList();              //用于返回出库申请信息的数组
            FS.HISFC.Models.Pharmacy.ApplyOut info; //返回数组中的出库申请类

            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得出库申请信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.ApplyOut();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                                  //申请流水号
                        info.ApplyDept.ID = this.Reader[1].ToString();                        //申请部门编码（科室或者病区）
                        info.StockDept.ID = this.Reader[2].ToString();                       //发药部门编码
                        info.SystemType = this.Reader[3].ToString();                           //出库申请分类
                        info.GroupNO = NConvert.ToDecimal(this.Reader[4].ToString());                  //批次号
                        info.Item.ID = this.Reader[5].ToString();                             //药品编码
                        info.Item.Name = this.Reader[6].ToString();                           //药品商品名
                        info.BatchNO = this.Reader[7].ToString();                             //批号
                        info.Item.Type.ID = this.Reader[8].ToString();                        //药品类别
                        info.Item.Quality.ID = this.Reader[9].ToString();                      //药品性质
                        info.Item.Specs = this.Reader[10].ToString();                         //规格
                        info.Item.PackUnit = this.Reader[11].ToString();                      //包装单位
                        info.Item.PackQty = NConvert.ToDecimal(this.Reader[12].ToString());   //包装数
                        info.Item.MinUnit = this.Reader[13].ToString();                       //最小单位
                        info.ShowState = this.Reader[14].ToString();                          //显示的单位标记
                        info.ShowUnit = this.Reader[15].ToString();                           //显示的单位
                        info.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[16].ToString());    //零售价
                        info.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[17].ToString()); //批发价
                        info.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[18].ToString());  //购入价
                        info.BillNO = this.Reader[19].ToString();                           //申请单号
                        info.Operation.ApplyOper.ID = this.Reader[20].ToString();                      //申请人编码
                        info.Operation.ApplyOper.OperTime = NConvert.ToDateTime(this.Reader[21].ToString());     //申请日期
                        info.State = this.Reader[22].ToString();                         //申请状态 0申请，1核准（出库），2作废，3暂不摆药
                        info.Operation.ApplyQty = NConvert.ToDecimal(this.Reader[23].ToString());       //申请出库量(每付的总数量)
                        info.Days = NConvert.ToDecimal(this.Reader[24].ToString());           //付数（草药）
                        info.IsPreOut = NConvert.ToBoolean(this.Reader[25].ToString());       //是否预扣库存：0未预扣，1已预扣
                        info.IsCharge = NConvert.ToBoolean(this.Reader[26].ToString());       //是否收费：0未收费，1已收费
                        info.PatientNO = this.Reader[27].ToString();                          //患者编号
                        info.PatientDept.ID = this.Reader[28].ToString();                     //患者科室
                        info.DrugNO = this.Reader[29].ToString();                           //摆药单号
                        info.Operation.ApproveOper.Dept.ID = this.Reader[30].ToString();                     //摆药科室
                        info.Operation.ApproveOper.ID = this.Reader[31].ToString();                    //摆药人
                        info.Operation.ApproveOper.OperTime = NConvert.ToDateTime(this.Reader[32].ToString());   //摆药日期
                        info.Operation.ApproveQty = NConvert.ToDecimal(this.Reader[33].ToString());     //摆药数量

                        info.Operation.ExamQty = info.Operation.ApproveQty;

                        info.DoseOnce = NConvert.ToDecimal(this.Reader[34].ToString());       //每次剂量
                        info.Item.DoseUnit = this.Reader[35].ToString();                      //剂量单位
                        info.Usage.ID = this.Reader[36].ToString();                           //用法代码
                        info.Usage.Name = this.Reader[37].ToString();                         //用法名称
                        info.Frequency.ID = this.Reader[38].ToString();                       //频次代码
                        info.Frequency.Name = this.Reader[39].ToString();                     //频次名称
                        info.Item.DosageForm.ID = this.Reader[40].ToString();                 //剂型编码
                        info.OrderType.ID = this.Reader[41].ToString();                       //医嘱类型编码
                        info.OrderNO = this.Reader[42].ToString();                            //医嘱流水号
                        info.CombNO = this.Reader[43].ToString();                             //组合序号
                        info.ExecNO = this.Reader[44].ToString();                             //执行单流水号
                        info.RecipeNO = this.Reader[45].ToString();                           //处方号
                        info.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[46].ToString());              //处方内项目流水号
                        info.SendType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[47].ToString());                //医嘱发送类型0全部，1集中，2临时
                        info.BillClassNO = this.Reader[48].ToString();                      //摆药单分类
                        info.PrintState = this.Reader[49].ToString();                         //打印状态
                        info.Operation.ExamOper.ID = this.Reader[50].ToString();                       //审批人（打印人）
                        info.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[51].ToString());      //审批时间（打印时间）
                        info.OutBillNO = this.Reader[52].ToString();                        //出库单号（退库申请时，保存出库时对应的记录）
                        info.ValidState = (FS.HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[53]);                         //有效标记（0有效，1无效，2不摆药）
                        info.User01 = this.Reader[54].ToString();                             //患者床位号
                        info.User02 = this.Reader[55].ToString();                             //患者姓名
                        info.Memo = this.Reader[56].ToString();								  //医嘱备注
                        info.RecipeInfo.Dept.ID = this.Reader[57].ToString();
                        info.RecipeInfo.ID = this.Reader[58].ToString();
                        info.IsBaby = NConvert.ToBoolean(this.Reader[59]);
                        info.ExtFlag = this.Reader[60].ToString();
                        info.ExtFlag1 = this.Reader[61].ToString();
                        info.CompoundGroup = this.Reader[62].ToString();
                        info.Compound.IsNeedCompound = NConvert.ToBoolean(this.Reader[63].ToString());
                        info.Compound.IsExec = NConvert.ToBoolean(this.Reader[64].ToString());
                        info.Compound.CompoundOper.ID = this.Reader[65].ToString();
                        info.Compound.CompoundOper.OperTime = NConvert.ToDateTime(this.Reader[66].ToString());

                        info.UseTime = NConvert.ToDateTime(this.Reader[67].ToString());
                        //核心无退药原因字段 暂屏蔽
                        //info.ReturnReason = this.Reader[68].ToString();//退药原因{657DE66E-52E2-40ef-AB0E-C1B0CC7D4017}
                        //{432F8D1A-80F9-45e1-9FE6-7332C49487BA}feng.ch用于检索配液信息的SQL
                        if (this.Reader.FieldCount > 69)
                        {
                            info.Item.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[69].ToString()); //基本剂量
                            info.Item.ShiftMark = this.Reader[70].ToString();  //存储条件
                        }
                        //{85F7F83C-A2F5-496a-88CF-5D8E7E7D4293}
                        if (this.Reader.FieldCount > 71)
                        {
                            info.Item.Product.Producer.Name = this.Reader[71].ToString();//生产厂商

                        }
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得出库申请信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }
                return al;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得出库申请信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }


        /// <summary>
        /// 获取医嘱批次信息
        /// </summary>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.OrderGroup> QueryOrderGroup()
        {
            List<FS.HISFC.Models.Pharmacy.OrderGroup> allOrderGroup = new List<FS.HISFC.Models.Pharmacy.OrderGroup>();
            for (int i = 1; i <= 5; i++)
            {
                FS.HISFC.Models.Pharmacy.OrderGroup orderGroup = new FS.HISFC.Models.Pharmacy.OrderGroup();
                orderGroup.ID = i.ToString();
                orderGroup.Name = "100" + i;
                allOrderGroup.Add(orderGroup);
            }
            return allOrderGroup;
        }

        /// <summary>
        /// 获取配置中心列表
        /// </summary>
        /// <param name="drugDeptCode">库存科室</param>
        /// <param name="groupCode">批次</param>
        /// <param name="state">状态</param>
        /// <param name="drugCode">药品编码</param>
        /// <param name="isOnlyValid">是否仅包含有效数据 True 仅包含有效数据</param>
        /// <returns>成功返回待配置患者列表 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.ApplyOut> QueryCompoundListByNurse(string drugDeptCode, string groupCode, string state, string drugCode, string applyDept)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            if (this.Sql.GetSql("Pharmacy.Item.QueryCompoundList.ByNurse", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryCompoundList.ByNurse字段!";
                return null;
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, drugDeptCode, groupCode, state, drugCode, applyDept);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            #region 执行Sql语句由Reader内获取数据

            //根据SQL语句取数组并返回数组
            List<FS.HISFC.Models.Pharmacy.ApplyOut> applyList = new List<FS.HISFC.Models.Pharmacy.ApplyOut>();

            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "获取待配置列表时发生错误：" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut info;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.ApplyOut();

                    info.StockDept.ID = drugDeptCode;
                    info.ApplyDept.ID = this.Reader[0].ToString();              //申请科室
                    info.PatientNO = this.Reader[1].ToString();                 //患者住院流水号
                    info.User01 = this.Reader[2].ToString();                    //床号
                    info.User02 = this.Reader[3].ToString();                    //姓名

                    applyList.Add(info);
                }

                return applyList;
            }
            catch (Exception ex)
            {
                this.Err = "获得申请患者列表时，执行SQL语句出错" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            #endregion
        }



        /// <summary>
        /// 插入配置原因
        /// </summary>
        /// <param name="patiendID"></param>
        /// <param name="orderNO"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public int InsertCompoundNode(string patiendID,string orderNO,string result,string state,string operCode,DateTime operTime)
        {
            string strSql = string.Empty;

            if (this.Sql.GetSql("Pharmacy.Item.Compound.InsertCompoundResult", ref strSql) == -1)
            {
                this.Err = "没有找到sql语句：Pharmacy.Item.Compound.InsertCompoundResul";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, patiendID, orderNO, result,state,operCode,operTime);
            }
            catch(Exception ex)
            {
                this.Err = "格式化Sql语句Pharmacy.Item.Compound.InsertCompoundResul错误" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除配置原因
        /// </summary>
        /// <param name="patientID"></param>
        /// <param name="orderNO"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public int UpdateCompoundNode(string patientID, string orderNO, string state,string result)
        {
            string strSql = string.Empty;

            if (this.Sql.GetSql("Pharmacy.Item.Compound.UpdateCompoundResult", ref strSql) == -1)
            {
                this.Err = "没有找到sql语句：Pharmacy.Item.Compound.UpdateCompoundResult";
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, patientID, orderNO, state,result);
            }
            catch (Exception ex)
            {
                this.Err = "格式化Sql语句Pharmacy.Item.Compound.UpdateCompoundResult错误" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);  
        }

        /// <summary>
        /// 根据患者编号，医嘱流水号获取配置原因
        /// </summary>
        /// <param name="patiendID"></param>
        /// <param name="orderNO"></param>
        /// <returns></returns>
        public string GetCompoundNode(string patiendID, string orderNO,string state)
        {
            string strSql = string.Empty;

            if (this.Sql.GetSql("Pharmacy.Item.Compound.GetCompoundResult", ref strSql) == -1)
            {
                this.Err = "没有找到sql语句：Pharmacy.Item.Compound.GetCompoundResult";
                return "";
            }
            try
            {
                strSql = string.Format(strSql, patiendID, orderNO, state);
            }
            catch (Exception ex)
            {
                this.Err = "格式化Sql语句Pharmacy.Item.Compound.GetCompoundResult错误" + ex.Message;
                return "";
            }
            return this.ExecSqlReturnOne(strSql, "");
        }
        #endregion
    }
}
