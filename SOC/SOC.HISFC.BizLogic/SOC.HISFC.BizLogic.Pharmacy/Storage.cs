using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.BizLogic.Pharmacy
{
    /// <summary>
    /// [功能描述: 药品库存管理类]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2011-06]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public class Storage : Item
    {
        #region 库存表操作

        #region 外部接口

        /// <summary>
        /// 取某一药房中某一药品在库存汇总表中的数量
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <param name="deptCode">库房编码</param>
        /// <param name="storageNum">库存总数量（返回参数）</param>
        /// <returns>1成功，-1失败</returns>
        public int GetStorageNum(string deptCode, string drugCode, out decimal storageNum)
        {
            storageNum = 0;
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageNum.ByDrugCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageNum.ByDrugCode字段!";
                return -1;
            }
            //格式化SQL语句
            string[] parm = { deptCode, drugCode };
            strSQL = string.Format(strSQL, parm);

            try
            {
                //取药品库存总数量
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "执行取药品库存总数量SQL语句时出错：" + this.Err;
                    return -1;
                }

                if (this.Reader.Read())
                {
                    try
                    {
                        storageNum = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());  //药品库存总数量
                    }
                    catch (Exception ex)
                    {
                        this.Err = "取药品库存总数量时出错！" + ex.Message;
                        return -1;
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句 获取库存总数量发生错误" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }

        }

        /// <summary>
        /// 取某一药房中某一药品在库存汇总表中的数量 {5DDC1B83-0693-4949-93A0-98FAC0630510} 获取药房药库映射科室库存
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <param name="deptCode">库房编码</param>
        /// <param name="storageNum">库存总数量（返回参数）</param>
        /// <returns>1成功，-1失败</returns>
        public int GetStorageNum1(string deptCode, string drugCode, out decimal storageNum)
        {
            storageNum = 0;
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageNum.ByDrugCode1", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageNum.ByDrugCode字段!";
                return -1;
            }
            //格式化SQL语句
            string[] parm = { deptCode, drugCode };
            strSQL = string.Format(strSQL, parm);

            try
            {
                //取药品库存总数量
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "执行取药品库存总数量SQL语句时出错：" + this.Err;
                    return -1;
                }

                if (this.Reader.Read())
                {
                    try
                    {
                        storageNum = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());  //药品库存总数量
                    }
                    catch (Exception ex)
                    {
                        this.Err = "取药品库存总数量时出错！" + ex.Message;
                        return -1;
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句 获取库存总数量发生错误" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }

        }

        /// <summary>
        /// 取全院某一药品在库存汇总表中的数量  {5DDC1B83-0693-4949-93A0-98FAC0630510} 获取全院区库存数量
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <param name="deptCode">库房编码</param>
        /// <param name="storageNum">库存总数量（返回参数）</param>
        /// <returns>1成功，-1失败</returns>
        public int GetHospitalNum1(string drugCode, out decimal storageNum)
        {
            storageNum = 0;
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageNum.ByDrugCode2", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageNum.ByDrugCode2字段!";
                return -1;
            }
            //格式化SQL语句
          
            strSQL = string.Format(strSQL, drugCode);

            try
            {
                //取药品库存总数量
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "执行取药品全院库存总数量SQL语句时出错：" + this.Err;
                    return -1;
                }

                if (this.Reader.Read())
                {
                    try
                    {
                        storageNum = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());  //药品库存总数量
                    }
                    catch (Exception ex)
                    {
                        this.Err = "取药品全院库存总数量时出错！" + ex.Message;
                        return -1;
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句 获取库存总数量发生错误" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }

        }

        /// <summary>
        /// 取某一药房中某一药品在库存汇总表中的数量，最低库存量，最高库存量{613A769A-C540-4a2c-949D-28B31F0BC482}
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <param name="deptCode">库房编码</param>
        /// <param name="storageNum">库存总数量（返回参数）</param>
        /// <param name="storageNum">最低库存量（返回参数）</param>
        /// <param name="storageNum">最高库存量（返回参数）</param>
        /// <returns>1成功，-1失败</returns>
        public int GetStorageLowTopNum(string deptCode, string drugCode, out decimal storageNum, out decimal storageLowNum, out decimal storageTopNum)
        {
            storageNum = 0;
            storageLowNum = 0;
            storageTopNum = 0;
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageLowTopNum.ByDrugCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageLowTopNum.ByDrugCode字段!";
                return -1;
            }
            //格式化SQL语句
            string[] parm = { deptCode, drugCode };
            strSQL = string.Format(strSQL, parm);

            try
            {
                //取药品库存总数量
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "执行取药品库存总数量SQL语句时出错：" + this.Err;
                    return -1;
                }

                if (this.Reader.Read())
                {
                    try
                    {
                        storageNum = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());  //药品库存总数量
                        storageLowNum = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[1].ToString());//药品最低库存量
                        storageTopNum = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[2].ToString());//药品最高库存量
                    }
                    catch (Exception ex)
                    {
                        this.Err = "取药品库存总数量时出错！" + ex.Message;
                        return -1;
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句 获取库存总数量发生错误" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }

        }

        /// <summary>
        /// 取某一药房中某一药品在库存汇总表中的数量
        /// </summary>
        /// <param name="deptCode">药房编码</param>
        /// <param name="drugQuality">药品性质编码</param>
        /// <returns>成功返回库存记录数组，出错返回null</returns>
        public ArrayList QueryStockinfoList(string deptCode, string drugQuality)
        {
            string strSQL = "";
            string strWhere = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockinfoList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockinfoList字段!";
                return null;
            }

            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStockinfoList.ByQuality", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockinfoList.ByQuality字段!";
                return null;
            }

            //格式化SQL语句
            string[] parm = { deptCode, drugQuality };
            strSQL = string.Format(strSQL + strWhere, parm);

            //取药品库存总数量
            return this.myGetStockinfo(strSQL);
        }

        /// <summary>
        /// 取某一药房中在库存汇总表中的记录
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <returns>库存记录数组，出错返回null</returns>
        public ArrayList QueryStockinfoList(string deptCode)
        {
            return this.QueryStockinfoList(deptCode, "ALL");
        }

        /// <summary>
        /// 获取科室库存低于最低库存量的药品
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns>成功返回科室库存信息 失败返回null</returns>
        public ArrayList QueryWarnDrugStockInfoList(string deptCode)
        {
            string strSQL = "";
            string strWhere = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockinfoList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockinfoList字段!";
                return null;
            }

            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStockinfoList.WarnDrug", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockinfoList.WarnDrug字段!";
                return null;
            }

            //格式化SQL语句
            string[] parm = { deptCode };
            strSQL = string.Format(strSQL + strWhere, parm);

            //取药品库存总数量
            return this.myGetStockinfo(strSQL);
        }

        /// <summary>
        /// 获取科室内达到库存有效期警戒线的药品
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="warnDays">有效期警示天数</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public ArrayList QueryWarnValidDateStockInfoList(string deptCode, int warnDays)
        {
            DateTime sysTime = this.GetDateTimeFromSysDateTime();

            string strSQL = "";
            string strWhere = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockinfoList字段!";
                return null;
            }

            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStorageList.WarnValid", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList.WarnValid字段!";
                return null;
            }

            //格式化SQL语句
            string[] parm = { deptCode, (sysTime.AddDays(warnDays)).ToString() };
            strSQL = string.Format(strSQL + strWhere, parm);

            //取药品库存总数量
            return this.myGetStorage(strSQL);
        }

        
        /// <summary>
        /// 获取病区特殊药品取药信息 忽略药品职级限制的判断
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns>成功返回该类药品数组 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QuerySpeLocationItem(string deptCode)
        {
            string strNormalSql = "";  //获得药品信息的SELECT语句

            //取无限制药品 
            if (this.GetSQL("Pharmacy.Item.QuerySpeLocationItem", ref strNormalSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QuerySpeLocationItem字段!";
                return null;
            }

            strNormalSql = string.Format(strNormalSql, deptCode);

            List<FS.HISFC.Models.Pharmacy.Item> alNormal = this.myGetAvailableList(strNormalSql);
            if (alNormal == null)
            {
                this.Err = "获取无限制药品发生错误" + this.Err;
                return null;
            }

            return alNormal;
        }

        /// <summary>
        /// 获取医嘱、收费使用的药品数据
        /// </summary>
        /// <param name="deptCode">取药部门</param>
        /// <param name="doctCode">医生编码</param>
        /// <param name="drugGrade">药品等级</param>
        /// <returns>成功返回药品数组 失败返回null 无满足条件数据返回空数组</returns>
        public ArrayList QueryItemAvailableArrayList(string deptCode, string doctCode, string drugGrade)
        {
            List<FS.HISFC.Models.Pharmacy.Item> alList = this.QueryItemAvailableList(deptCode, doctCode, drugGrade);

            if (alList == null)
            {
                return null;
            }

            return new ArrayList(alList.ToArray());
        }

        /// <summary>
        /// 获取医嘱、收费使用的药品数据
        /// </summary>
        /// <param name="deptCode">取药部门</param>
        /// <param name="doctCode">医生编码</param>
        /// <param name="drugGrade">药品等级</param>
        /// <returns>成功返回药品数组 失败返回null 无满足条件数据返回空数组</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemAvailableList(string deptCode, string doctCode, string drugGrade)
        {
            string strNormalSql = "";  //获得药品信息的SELECT语句

            //取无限制药品 
            if (this.GetSQL("Pharmacy.Item.QueryItemAvailableList.Normal", ref strNormalSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryItemAvailableList.Normal字段!";
                return null;
            }

            strNormalSql = string.Format(strNormalSql, deptCode);

            List<FS.HISFC.Models.Pharmacy.Item> alNormal = this.myGetAvailableList(strNormalSql);
            if (alNormal == null)
            {
                this.Err = "获取无限制药品发生错误" + this.Err;
                return null;
            }

            /*
             * 特限药物及等级药物在医嘱开立时判断
             * 
            //获取病区特殊药品取药
            List<FS.HISFC.Models.Pharmacy.Item> alSpeLocation = this.QuerySpeLocationItem(deptCode);
            if (alSpeLocation == null)
            {
                this.Err = "获取病区特殊药品取药错误" + this.Err;
                return null;
            }

            alNormal.AddRange(alSpeLocation);

            //如果医生未维护职级对应药品等级 那么只能看到无限制药品 
            if (drugGrade == null || drugGrade == "")
            {
                return alNormal;
            }

            //取等级限制药品
            string strGradeSql = "";
            if (this.GetSQL("Pharmacy.Item.QueryItemAvailableList.Grade", ref strGradeSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryItemAvailableList.Grade字段!";
                return null;
            }

            strGradeSql = string.Format(strGradeSql, deptCode, drugGrade);

            List<FS.HISFC.Models.Pharmacy.Item> alGrade = this.myGetAvailableList(strGradeSql);
            if (alGrade == null)
            {
                this.Err = "获取等级限制药品发生错误" + this.Err;
                return null;
            }

            alNormal.AddRange(alGrade);

            //取特限药品
            string strSpeDrugSql = "";
            if (this.GetSQL("Pharmacy.Item.QueryItemAvailableList.SpeDrug", ref strSpeDrugSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryItemAvailableList.SpeDrug字段!";
                return null;
            }

            strSpeDrugSql = string.Format(strSpeDrugSql, deptCode, drugGrade, doctCode);

            List<FS.HISFC.Models.Pharmacy.Item> alSpeDrug = this.myGetAvailableList(strSpeDrugSql);
            if (alSpeDrug == null)
            {
                this.Err = "获取特限药品发送错误" + this.Err;
                return null;
            }

            alNormal.AddRange(alSpeDrug);
             
             */

            return alNormal;
        }

        /// <summary>
        /// 根据库房编码、项目、批次号获取最近一次入库单号
        /// </summary>
        /// <param name="drugDeptCode"></param>
        /// <param name="drugCode"></param>
        /// <param name="groupNO"></param>
        /// <returns></returns>
        public string GetLatestInListNO(string drugDeptCode, string drugCode, string groupNO)
        {
            string strSql = string.Empty;
            if (this.GetSQL("Pharmacy.Item.GetLatestInListNO", ref strSql) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.GetLatestInListNO";
                return string.Empty;
            }
            strSql = string.Format(strSql, drugDeptCode, drugCode, groupNO);
            return this.ExecSqlReturnOne(strSql, string.Empty);
        }

        #region 按照药品类别、发药类型获取库存明细

        /// <summary>
        /// 获取医嘱、收费使用的药品数据
        /// </summary>
        /// <param name="deptCode">取药部门</param>
        /// <param name="doctCode">医生编码</param>
        /// <param name="drugGrade">药品等级</param
        /// <param name="sendType">发药类型：O 门诊处方、I 住院医嘱</param>
        /// <returns>成功返回药品数组 失败返回null 无满足条件数据返回空数组</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemAvailableListBySendType(string deptCode, string doctCode, string drugGrade, string sendType)
        {
            string strNormalSql = "";  //获得药品信息的SELECT语句

            //取无限制药品 
            if (this.GetSQL("Pharmacy.Item.QueryItemAvailableList.Normal.BySendType", ref strNormalSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryItemAvailableList.Normal字段!";
                return null;
            }

            strNormalSql = string.Format(strNormalSql, deptCode, sendType);

            List<FS.HISFC.Models.Pharmacy.Item> alNormal = this.myGetAvailableList(strNormalSql);
            if (alNormal == null)
            {
                this.Err = "获取无限制药品发生错误" + this.Err;
                return null;
            }

            /*
             * 特限药物及等级药物在医嘱开立时判断
             * 
            //这里就不再判断药品类别和发药类型 
            //获取病区特殊药品取药
            List<FS.HISFC.Models.Pharmacy.Item> alSpeLocation = this.QuerySpeLocationItem(deptCode);
            if (alSpeLocation == null)
            {
                this.Err = "获取病区特殊药品取药错误" + this.Err;
                return null;
            }

            alNormal.AddRange(alSpeLocation);

            //如果医生未维护职级对应药品等级 那么只能看到无限制药品 
            if (drugGrade == null || drugGrade == "")
            {
                return alNormal;
            }

            //取等级限制药品
            string strGradeSql = "";
            if (this.GetSQL("Pharmacy.Item.QueryItemAvailableList.Grade.BySendType", ref strGradeSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryItemAvailableList.Grade字段!";
                return null;
            }

            strGradeSql = string.Format(strGradeSql, deptCode, drugGrade, sendType);

            List<FS.HISFC.Models.Pharmacy.Item> alGrade = this.myGetAvailableList(strGradeSql);
            if (alGrade == null)
            {
                this.Err = "获取等级限制药品发生错误" + this.Err;
                return null;
            }

            alNormal.AddRange(alGrade);

            //取特限药品
            string strSpeDrugSql = "";
            if (this.GetSQL("Pharmacy.Item.QueryItemAvailableList.SpeDrug.BySendType", ref strSpeDrugSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryItemAvailableList.SpeDrug字段!";
                return null;
            }

            strSpeDrugSql = string.Format(strSpeDrugSql, deptCode, drugGrade, doctCode, sendType);

            List<FS.HISFC.Models.Pharmacy.Item> alSpeDrug = this.myGetAvailableList(strSpeDrugSql);
            if (alSpeDrug == null)
            {
                this.Err = "获取特限药品发送错误" + this.Err;
                return null;
            }

            alNormal.AddRange(alSpeDrug);
             */

            return alNormal;
        }

        #endregion

        #endregion

        #region 入出库更新库存

        /// <summary>
        /// 根据入库信息更新库存
        /// </summary>
        /// <param name="input">入库信息</param>
        /// <param name="storageState">库存状态</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int UpdateStorageForInput(FS.HISFC.Models.Pharmacy.Input input, string storageState)
        {
            decimal dNowPrice = 0;
            if (this.GetNowPrice(input.Item.ID, ref dNowPrice) == -1)
            {
                this.Err = "根据入库记录更新库存 获取药品" + input.Item.Name + "零售价出错";
                return -1;
            }

            //如包装数量为0 则将包装数量赋值为1
            if (input.Item.PackQty == 0)
                input.Item.PackQty = 1;
            FS.HISFC.Models.Pharmacy.StorageBase storageBase;
            storageBase = input.Clone() as FS.HISFC.Models.Pharmacy.StorageBase;

            storageBase.Item.PriceCollection.RetailPrice = dNowPrice;					                //当前最新价格
            storageBase.Item.PriceCollection.PurchasePrice = input.Item.PriceCollection.PurchasePrice;	//最新购入价
            storageBase.Operation.Oper.OperTime = input.Operation.Oper.OperTime;
            storageBase.Class2Type = "0310";
            storageBase.PrivType = input.PrivType;

            //storageBase.PrivType = "0310" + input.PrivType;

            int parm;
            parm = this.UpdateStorageNum(storageBase);
            if (parm == -1)
            {
                this.Err = "更新申请科室库存时出错！";
                return -1;
            }
            if (parm == 0)
            {
                storageBase.State = storageState;		//库存状态
                parm = this.InsertStorage(storageBase);
                if (parm == -1)
                {
                    this.Err = "对申请科室增加库存出错！";
                    return -1;
                }
            }

            return 1;
        }

        #endregion

        #region 内部使用

        /// <summary>
        /// 获取药品最新零售价
        /// by cube 2011-05-27
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <param name="drugPrice">药品零售价</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int GetNowPrice(string drugDeptNO, string drugCode, ref decimal drugPrice)
        {
            string strSql = "";
            if (this.GetSQL("SOC.Pharmacy.Item.GetNowPrice", ref strSql) == -1)
            {
                //this.Err = "没有找到SOC.Pharmacy.Item.GetNowPrice字段";
                //return -1;
                strSql = @"select retail_price from pha_com_stockinfo where drug_dept_code = '{0}' and drug_code = '{1}'";
                this.CacheSQL("SOC.Pharmacy.Item.GetNowPrice", strSql);
            }

            strSql = string.Format(strSql, drugDeptNO, drugCode);
            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    drugPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());
                }
                else
                {
                    return GetNowPrice(drugCode, ref drugPrice);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获取最新药品零售价出错" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }
            return 1;
        }

        /// <summary>
        /// 取某一药品在全院的库存总条数
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <returns>返回库存数量大于零的总条数 失败返回-1</returns>
        public int GetDrugStorageRowNum(string drugCode)
        {
            int storageNum = 0;
            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetDrugStorageRowNum.ByDrugCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetDrugStorageRowNum.ByDrugCode字段!";
                return -1;
            }
            //格式化SQL语句
            string[] parm = { drugCode };
            strSQL = string.Format(strSQL, parm);

            try
            {
                //取药品库存总数量
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "取某一药品再全院的库存总条数SQL语句时出错：" + this.Err;
                    return -1;
                }

                if (this.Reader.Read())
                {
                    try
                    {
                        storageNum = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0].ToString());  //药品库存总数量
                    }
                    catch (Exception ex)
                    {
                        this.Err = "取某一药品再全院的库存总条数！" + ex.Message;
                        this.Reader.Close();
                        return -1;
                    }
                }
                return storageNum;
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句获取 库存总条目发生错误" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 获取根据有效期查询库存药品信息sql
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="validDateCautionDays"></param>
        /// <returns></returns>
        public string GetValidDateSql(string deptCode, int validDateCautionDays)
        {
            string sqlStr = "";
            if (this.GetSQL("SOC.Pharmacy.StorageManager.GetItemByValidDate", ref sqlStr) == -1)
            {
                return "";
            }
            else
            {
                sqlStr  = string.Format(sqlStr,deptCode,validDateCautionDays);
                return sqlStr;
            }
        }
        /// <summary>
        /// 取某一药房中某一药品某一批次在库存明细表中的数量
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <param name="deptCode">库房编码</param>
        /// <param name="groupNO">批次（如果为0，则取所有批次库存数量之和）</param>
        /// <param name="storageNum">库存总数量（返回参数）</param>
        /// <returns>1成功，-1失败</returns>
        public int GetStorageNum(string deptCode, string drugCode, decimal groupNO, out decimal storageNum)
        {
            storageNum = 0;
            //如果批次为零则取所有批次库存数量之和
            if (groupNO == 0) return GetStorageNum(deptCode, drugCode, out storageNum);

            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageNum.ByGroupNo", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageNum.ByGroupNo字段!";
                return -1;
            }
            //格式化SQL语句
            string[] parm = { deptCode, drugCode, groupNO.ToString() };
            strSQL = string.Format(strSQL, parm);

            try
            {
                //取药品库存总数量
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "执行取批次药品库存总数量SQL语句时出错：" + this.Err;
                    return -1;
                }

                if (this.Reader.Read())
                {
                    try
                    {
                        storageNum = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());  //药品库存总数量
                    }
                    catch (Exception ex)
                    {
                        this.Err = "取批次药品库存总数量时出错！" + ex.Message;
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句获取制定批次药品数量发生错误" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }
            return 1;
        }

        /// <summary>
        /// 取某一药房中某一药品某一批号在库存明细表中的数量
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <param name="deptCode">库房编码</param>
        /// <param name="batchNO">批号（如果为null或空字符串，则取所有批号库存数量之和）</param>
        /// <param name="storageNum">库存总数量（返回参数）</param>
        /// <returns>1成功，-1失败</returns>
        public int GetStorageNum(string deptCode, string drugCode, string batchNO, out decimal storageNum)
        {
            storageNum = 0;
            //如果批号为零则取所有批号库存数量之和
            if (batchNO == null || batchNO == "" || batchNO.ToUpper() == "ALL")
            {
                return GetStorageNum(deptCode, drugCode, out storageNum);
            }

            string strSQL = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageNum.ByBatchNO", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageNum.ByBatchNO字段!";
                return -1;
            }
            //格式化SQL语句
            string[] parm = { deptCode, drugCode, batchNO };
            strSQL = string.Format(strSQL, parm);

            try
            {
                //取药品库存总数量
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "执行取批号药品库存总数量SQL语句时出错：" + this.Err;
                    return -1;
                }

                if (this.Reader.Read())
                {
                    try
                    {
                        storageNum = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());  //药品库存总数量
                    }
                    catch (Exception ex)
                    {
                        this.Err = "取批号药品库存总数量时出错！" + ex.Message;
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = "执行Sql语句获取制定批号药品数量发生错误" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }
            return 1;
        }


        /// <summary>
        /// 取某一药房中某一药品在库存明细表中的数据
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <returns>成功返回库存记录数组 Storage实体，出错返回null</returns>
        public ArrayList QueryStorageList(string deptCode)
        {
            string strSQL = "";
            string strWhere = "";
            string strOrder = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList字段!";
                return null;
            }

            //取WHERE条件
            if (this.GetSQL("SOC.Pharmacy.Item.GetStorageList.ByDept", ref strWhere) == -1)
            {
                strWhere = @" where store_sum > 0 and drug_dept_code = '{0}'";
                this.CacheSQL("SOC.Pharmacy.Item.GetStorageList.ByDept", strWhere);
            }

            //格式化SQL语句
            string[] parm = { deptCode };
            strSQL = string.Format(strSQL + strWhere, parm);

            return this.myGetStorage(strSQL);
        }


        /// <summary>
        /// 取某一药房中某一药品最新
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="drugCode"></param>
        /// <returns>null 错误</returns>
        public FS.HISFC.Models.Pharmacy.Storage GetLatestStorage(string deptCode, string drugCode)
        {
            string strSQL = "";
            string strWhere = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList字段!";
                return null;
            }
            //取WHERE条件
            if (this.GetSQL("SOC.Pharmacy.Item.GetStorageListLatest.ByDrugCode", ref strWhere) == -1)
            {
                strWhere = @"
                            WHERE DRUG_DEPT_CODE = '{0}'
                            AND   DRUG_CODE = '{1}'
                            AND   GROUP_CODE = (
                                                  SELECT MAX(T.GROUP_CODE) 
                                                  FROM   PHA_COM_STORAGE T 
                                                  WHERE  T.DRUG_DEPT_CODE = '{0}'
                                                  AND    T.DRUG_CODE = '{1}'
                                                  )
                ";
            }

            //格式化SQL语句
            string[] parm = { deptCode, drugCode};
            strSQL = string.Format(strSQL + strWhere, parm);

            //取药品库存总数量
            ArrayList al = this.myGetStorage(strSQL);
            if (al == null)
            {
                return null;
            }
            if (al.Count >= 1)
            {
                return al[0] as FS.HISFC.Models.Pharmacy.Storage;
            }
            return new FS.HISFC.Models.Pharmacy.Storage();
        }
        /// <summary>
        /// 取某一药房中某一药品在库存明细表中的数据
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <param name="deptCode">库房编码</param>
        /// <returns>成功返回库存记录数组 Storage实体，出错返回null</returns>
        public ArrayList QueryStorageList(string deptCode, string drugCode)
        {
            string strSQL = "";
            string strWhere = "";
            string strOrder = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList字段!";
                return null;
            }

            //取WHERE条件
            if (this.GetSQL("Pharmacy.Item.GetStorageList.ByDrugCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList.ByDrugCode字段!";
                return null;
            }

            //取Order条件
            if (this.GetSQL("Pharmacy.Item.GetStorageList.OrderAsc", ref strOrder) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList.OrderAsc字段!";
                return null;
            }

            //格式化SQL语句
            string[] parm = { deptCode, drugCode, "0" };
            strSQL = string.Format(strSQL + strWhere + strOrder, parm);

            //取药品库存总数量

            return this.myGetStorage(strSQL);
        }

        /// <summary>
        /// 取某一药房中某一药品在库存明细表中的数量
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <param name="deptCode">库房编码</param>
        /// <param name="groupNo">批次</param>
        /// <returns>成功返回库存记录数组，出错返回null</returns>
        public ArrayList QueryStorageList(string deptCode, string drugCode, decimal groupNo)
        {
            string strSQL = "";
            string strWhere = "";
            string strOrder = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList字段!";
                return null;
            }

            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStorageList.ByDrugCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList.ByDrugCode字段!";
                return null;
            }

            //取Order条件
            if (this.GetSQL("Pharmacy.Item.GetStorageList.OrderAsc", ref strOrder) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList.OrderAsc字段!";
                return null;
            }

            //格式化SQL语句
            string[] parm = { deptCode, drugCode, groupNo.ToString() };
            strSQL = string.Format(strSQL + strWhere + strOrder, parm);

            //取药品库存总数量
            return this.myGetStorage(strSQL);
        }

        /// <summary>
        /// 取某一药房中某一药品在库存明细表中的数量
        /// 只获取有效的记录
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <param name="deptCode">库房编码</param>
        /// <param name="groupNo">批次</param>
        /// <returns>成功返回库存记录数组，出错返回null</returns>
        public ArrayList QueryStorageList(string deptCode, string drugCode, string batchNO)
        {
            string strSQL = "";
            string strWhere = "";
            string strOrder = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList字段!";
                return null;
            }

            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStorageList.ByBatchNO", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList.ByBatchNO字段!";
                return null;
            }

            //取Order条件
            if (this.GetSQL("Pharmacy.Item.GetStorageList.OrderAsc", ref strOrder) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList.OrderAsc字段!";
                return null;
            }

            //格式化SQL语句
            if (batchNO == null || batchNO == "" || batchNO.ToUpper() == "ALL")
            {
                batchNO = "ALL";
            }
            string[] parm = { deptCode, drugCode, batchNO };
            strSQL = string.Format(strSQL + strWhere + strOrder, parm);

            //取药品库存总数量
            return this.myGetStorage(strSQL);
        }

        /// <summary>
        /// 根据药品编码获取库存汇总信息(返回)
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="drugCode">药品编码</param>
        /// <returns>成功返回库存汇总信息 失败返回null 无记录返回空实体</returns>
        public ArrayList GetStockInfosByDrugCode(string deptCode, string drugCode)
        {
            string strSQL = "";
            string strWhere = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockinfoList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockinfoList字段!";
                return null;
            }

            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStockinfoList.ByDrugCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockinfoList.ByDrugCode字段!";
                return null;
            }
            //格式化SQL语句
            string[] parm = { deptCode, drugCode };
            strSQL = string.Format(strSQL + strWhere, parm);

            //取药品库存总数量
            ArrayList al = this.myGetStockinfo(strSQL);
            if (al == null)
            {
                return null;
            }

            //如果没有找到数据，则返回新实体。
            if (al.Count == 0)
            {
                return new ArrayList();
            }

            return al;
        }

        /// <summary>
        /// 根据药品编码获取库存汇总信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="drugCode">药品编码</param>
        /// <returns>成功返回库存汇总信息 失败返回null 无记录返回空实体</returns>
        public FS.HISFC.Models.Pharmacy.Storage GetStockInfoByDrugCode(string deptCode, string drugCode)
        {
            string strSQL = "";
            string strWhere = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStockinfoList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockinfoList字段!";
                return null;
            }

            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetStockinfoList.ByDrugCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockinfoList.ByDrugCode字段!";
                return null;
            }
            //格式化SQL语句
            string[] parm = { deptCode, drugCode };
            strSQL = string.Format(strSQL + strWhere, parm);

            //取药品库存总数量
            ArrayList al = this.myGetStockinfo(strSQL);
            if (al == null)
            {
                return null;
            }

            //如果没有找到数据，则返回新实体。
            if (al.Count == 0)
            {
                return new FS.HISFC.Models.Pharmacy.Storage();
            }

            return al[0] as FS.HISFC.Models.Pharmacy.Storage;
        }

        /// <summary>
        /// 根据是否按批号管理返回库存信息数组
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="isBatch">是否按批号管理</param>
        /// <returns>成功返回数组，失败返回null 无数据返回空数组</returns>
        public ArrayList QueryStorageList(string deptCode, bool isBatch)
        {
            string strSQL = "";
            string xmlSQL = "";
            //返回数组
            ArrayList al = new ArrayList();
            //用于库存信息存贮
            FS.HISFC.Models.Pharmacy.Item info;
            //确定在xml中sql语句的位置
            if (isBatch)
                xmlSQL = "Pharmacy.Item.GetStorageListByBatch";
            else
                xmlSQL = "Pharmacy.Item.GetStorageListNoBatch";
            //取sql语句
            if (this.GetSQL(xmlSQL, ref strSQL) == -1)
            {
                this.Err = "没有找到" + xmlSQL + "字段！";
                return null;
            }
            //格式化sql语句
            strSQL = string.Format(strSQL, deptCode);

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得库存信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.Item();
                    info.ID = this.Reader[0].ToString();							//0 药品编码
                    info.Name = this.Reader[1].ToString();							//1 药品名称
                    info.Specs = this.Reader[2].ToString();							//2 规格
                    info.User01 = this.Reader[3].ToString();						//3 批号
                    info.User02 = this.Reader[4].ToString();						//4 库位号
                    info.User03 = this.Reader[5].ToString();						//5 库存
                    info.SpellCode = this.Reader[6].ToString();					//6 拼音码
                    info.WBCode = this.Reader[7].ToString();						//7 五笔码
                    info.NameCollection.RegularSpell.SpellCode = this.Reader[8].ToString();	//8 通用名拼音码
                    info.NameCollection.RegularSpell.WBCode = this.Reader[9].ToString();		//9 通用名五笔码
                    if (this.Reader.FieldCount > 10)
                    {
                        info.NameCollection.OtherSpell.SpellCode = this.Reader[10].ToString();      //10 别名拼音码
                        info.NameCollection.OtherSpell.WBCode = this.Reader[11].ToString();         //11 别名五笔码
                        info.NameCollection.FormalSpell.SpellCode = this.Reader[12].ToString();     //12 学名拼音码
                        info.NameCollection.FormalSpell.WBCode = this.Reader[13].ToString();        //13 学名五笔码                    
                        info.PackQty = NConvert.ToDecimal(this.Reader[14]);                         //14 包装数量
                    }

                    al.Add(info);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获得库存信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 获取指定药品的库房库存列表
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public ArrayList QueryStoreDeptList(string drugCode)
        {
            string strSQL = "";
            //返回数组
            ArrayList al = new ArrayList();
            //取sql语句
            if (this.GetSQL("Pharmacy.Item.QueryStoreDeptList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryStoreDeptList字段！";
                return null;
            }
            //格式化sql语句
            strSQL = string.Format(strSQL, drugCode);

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得库存信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {

                //用于库存信息存贮
                FS.HISFC.Models.Pharmacy.Storage info;

                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.Storage();

                    info.Item.ID = this.Reader[0].ToString();							    //0 药品编码
                    info.Item.Name = this.Reader[1].ToString();							    //1 药品名称
                    info.Item.Specs = this.Reader[2].ToString();							//2 规格
                    info.BatchNO = this.Reader[3].ToString();						        //3 批号
                    info.StoreQty = NConvert.ToDecimal(this.Reader[4].ToString());		    //5 库存
                    info.Item.MinUnit = this.Reader[5].ToString();
                    info.StockDept.ID = this.Reader[6].ToString();                          //库存科室
                    info.StockDept.Name = this.Reader[7].ToString();
                    info.Item.Product.Producer.ID = this.Reader[8].ToString();
                    info.Item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
                    info.Item.PackUnit = this.Reader[10].ToString();

                    al.Add(info);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获得库存信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 更新库存明细表中的数量（正数是增加，负数是减少）
        /// 患者库存管理时更新有效期为操作日期
        /// </summary>
        /// <param name="storageBase">库存记录类</param>
        /// <param name="operDate">操作日期 </param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateStorageNum(FS.HISFC.Models.Pharmacy.StorageBase storageBase, DateTime operDate)
        {
            string strSQL = "";
            //取SQL语句。
            if (this.GetSQL("Pharmacy.Item.UpdateStorageNumAndValidDate", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateStorageNumAndValidDate字段!";
                return -1;
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   storageBase.StockDept.ID,                  //0库存科室编码
									   storageBase.Item.ID,                  //1药品编码
									   storageBase.GroupNO.ToString(),       //2批次
									   storageBase.Quantity.ToString(),      //3变化数量
									   (storageBase.Quantity * storageBase.Item.PriceCollection.RetailPrice / storageBase.Item.PackQty).ToString(),//4变化金额
									   storageBase.ID,                       //5出库单流水号
									   storageBase.SerialNO.ToString(),      //6出库单内序号
									   storageBase.TargetDept.ID,            //7领药部门
									   storageBase.Class2Type + "|" + storageBase.PrivType,				 //8权限类型
									   this.Operator.ID,                     //9操作人
									   operDate.ToString()					//10操作日期/有效期
								   };


                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新库存明细表中的数量的SQl参数赋值出错！Pharmacy.Item.UpdateStorageNumAndValidDate" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新库存明细表中的数量（正数是增加，负数是减少）
        /// </summary>
        /// <param name="storageBase">库存记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateStorageNum(FS.HISFC.Models.Pharmacy.StorageBase storageBase)
        {
            string strSQL = "";
            //取SQL语句。
            if (this.GetSQL("Pharmacy.Item.UpdateStorageNum", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateStorageNum字段!";
                return -1;
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   storageBase.StockDept.ID,                  //0库存科室编码
									   storageBase.Item.ID,                  //1药品编码
									   storageBase.GroupNO.ToString(),       //2批次
									   storageBase.Quantity.ToString(),      //3变化数量
									   (storageBase.Quantity * storageBase.Item.PriceCollection.RetailPrice / storageBase.Item.PackQty).ToString(),//4变化金额
									   storageBase.ID,                       //5出库单流水号
									   storageBase.SerialNO.ToString(),      //6出库单内序号
									   storageBase.TargetDept.ID,            //7领药部门
									   storageBase.Class2Type + "|" + storageBase.PrivType,				 //8权限类型
									   this.Operator.ID                      //9操作人
								   };


                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新库存明细表中的数量的SQl参数赋值出错！Pharmacy.Item.ExamStorage" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新一条库存记录的库存状态
        /// </summary>
        /// <param name="storageBase">库存记录类</param>
        /// <param name="storageState">库存状态 0 暂入库 1 正式入库</param>
        /// <param name="updateStorage">是否根据库存记录类更新库存 true  更新 false 不更新</param>
        /// <returns>0 没有更新 1 成功 －1 失败</returns>
        public int UpdateStorageState(FS.HISFC.Models.Pharmacy.StorageBase storageBase, string storageState, bool updateStorage)
        {
            string strSQL = "";
            //取SQL语句。
            if (this.GetSQL("Pharmacy.Item.UpdateStorageState", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateStorageState字段!";
                return -1;
            }
            try
            {
                decimal quantity = 0;
                decimal cost = 0;
                if (updateStorage)		//如更新库存
                {
                    quantity = storageBase.Quantity;
                    cost = storageBase.Quantity * storageBase.Item.PriceCollection.RetailPrice / storageBase.Item.PackQty;
                }
                //取参数列表
                string[] strParm = {
									   storageBase.StockDept.ID,                  //0库存科室编码
									   storageBase.Item.ID,                  //1药品编码
									   storageBase.GroupNO.ToString(),       //2批次
									   quantity.ToString(),					//3变化数量
									   cost.ToString(),						//4变化金额
									   storageBase.ID,                       //5出库单流水号
									   storageBase.SerialNO.ToString(),      //6出库单内序号
									   storageBase.TargetDept.ID,            //7领药部门
									   storageBase.Class2Type + "|" + storageBase.PrivType,				 //8权限类型
									   this.Operator.ID,                     //9操作人
									   storageState							 //10库存状态
								   };


                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新库存记录内状态的SQl参数赋值出错！Pharmacy.Item.UpdateStorageState" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新库存明细数据
        /// 先执行更新数量操作，如果数据库中没有记录则执行插入操作
        /// </summary>
        /// <param name="storageBase">库存记录类</param>
        /// <returns>成功返回操作条目数 失败返回－1</returns>
        public int SetStorage(FS.HISFC.Models.Pharmacy.StorageBase storageBase)
        {
            //先执行更新操作
            int parm = UpdateStorageNum(storageBase);
            if (parm == 0)
            {
                //如果数据库中没有记录则执行插入操作
                parm = InsertStorage(storageBase);
            }
            return parm;
        }

        /// <summary>
        /// 更新库存汇总表中的预扣数量（正数是增加，负数是减少）
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="drugCode">药品编码</param>
        /// <param name="alterStoreNum">预扣变化数量</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        [System.Obsolete("原有预扣库存管理模式作废 采用UpdateStockinfoPreOutNum代替", true)]
        public int UpdateStoragePreOutNum(string deptCode, string drugCode, decimal alterStoreNum)
        {
            string strSQL = "";
            //取SQL语句。
            if (this.GetSQL("Pharmacy.Item.UpdatePreOutNum", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdatePreOutNum字段!";
                return -1;
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   deptCode,                       //库存科室编码
									   drugCode,                       //药品编码
									   alterStoreNum.ToString(),          //预扣变化数量
									   this.Operator.ID                //操作人
								   };

                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新库存汇总表中的预扣数量时出错！Pharmacy.Item.UpdatePreOutNum" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新库存汇总表中的一条记录
        /// </summary>
        /// <param name="storage">库存记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateStockinfoModifyData(FS.HISFC.Models.Pharmacy.Storage storage)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateStockinfo", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateStockinfo字段!";
                return -1;
            }
            try
            {
                string[] strParm = {
									   storage.StockDept.ID,                        //0 科室编码
									   storage.Item.ID,                             //1 药品编码
									   storage.LowQty.ToString(),                   //2 最低库存量
									   storage.TopQty.ToString(),                   //3 最高库存量
									   NConvert.ToInt32(storage.IsCheck).ToString(),//4 日盘点
									   //NConvert.ToInt32(storage.IsStop).ToString(), //5 是否停用
                                       ((int)storage.ValidState).ToString(),
									   storage.Memo,                                //6 备注
									   this.Operator.ID,                            //7 操作人
									   storage.PlaceNO,			                    //8 货位号
                                       NConvert.ToInt32(storage.IsLack).ToString(),  //9 是否缺药
                                       storage.ManageQuality.ID,
                                       NConvert.ToInt32(storage.IsUseForOutpatient).ToString(),
                                       NConvert.ToInt32(storage.IsUseForInpatient).ToString(),
                                       NConvert.ToInt32(storage.IsLackForInpatient).ToString(),
                                       NConvert.ToInt32(storage.IsRadix).ToString(),
                                       storage.SplitType,
                                       storage.LZSplitType,
                                       storage.CDSplitType,

								   };     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新库存汇总记录SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新库存的缺药标记
        /// </summary>
        /// <param name="stockDeptNO">库存科室编码</param>
        /// <param name="drugNO">药品编码</param>
        /// <param name="isLackForOutpatient">门诊缺药</param>
        /// <param name="isLackForInpatient">住院缺药</param>
        /// <returns></returns>
        public int UpdateStockinfoModifyData(string stockDeptNO, string drugNO, bool isLackForOutpatient, bool isLackForInpatient)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Item.UpdateStockinfo.CommonModify", ref strSQL) == -1)
            {
                strSQL = @"
                           update pha_com_stockinfo s
                               set s.lack_flag = '{2}', 
                                   s.lack_inpatient_flag = '{3}',
                                   s.oper_code = '{4}'
                                   s.oper_date = sysdate
                             where s.drug_dept_code = '{0}'
                               and s.drug_code = '{1}'
                        ";
            }
            try
            {
                string[] strParm = { 
                                       stockDeptNO, 
                                       drugNO, 
                                       isLackForOutpatient ? "1" : "0", 
                                       isLackForInpatient ? "1" : "0",
                                       this.Operator.ID
                                   };   
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新库存汇总记录SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        } 

        ///<summary>
        ///获取某药品在全院、本库房的库存总量
        ///</summary>
        ///<param name="deptcode">库房编码</param>
        ///<param name="drugcode">药品编码</param>
        ///<param name="storeSum">返回库房总量</param>
        ///<param name="storeTotSum">返回全院总量</param>
        ///<returns>0 查找成功 -1 失败</returns>
        public int FindSum(string deptcode, string drugcode, ref decimal storeSum, ref decimal storeTotSum)
        {
            string strSelSQL = "";
            string strSQL = "";
            //取计算库存总量Select语句
            if (this.GetSQL("Pharmacy.Item.StockPlanFindSum", ref strSelSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.StockPlanFindSum字段!";
                return -1;
            }

            string strWhere = "";
            //取查询本科室库存量的where条件语句
            if (this.GetSQL("Pharmacy.Item.StockPlanFindSumList", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.StockPlanFindSumList字段!";
                return -1;
            }

            string strAllWhere = "";
            //取查询全院库存量的where条件语句
            if (this.GetSQL("Pharmacy.Item.StockPlanFindSumAllList", ref strAllWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.StockPlanFindSumAllList字段!";
                return -1;
            }


            //格式化SQL语句，查询本科室库存总量
            try
            {
                strSQL = strSelSQL + " " + strWhere;
                strSQL = string.Format(strSQL, deptcode, drugcode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.StockPlanFindSumList:" + ex.Message;
                return -1;
            }

            storeSum = NConvert.ToDecimal(this.ExecSqlReturnOne(strSQL));
            //格式化SQL语句，查询全院库存总量
            try
            {
                strSQL = strSelSQL + " " + strAllWhere;
                strSQL = string.Format(strSQL, drugcode);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句时出错Pharmacy.Item.StockPlanFindSumAllList:" + ex.Message;
                return -1;
            }

            storeTotSum = NConvert.ToDecimal(this.ExecSqlReturnOne(strSQL));
            return 0;
        }

        public ArrayList QueryValidDates(string deptCode)
        {
            string strSQL = "";
            //返回数组
            ArrayList al = new ArrayList();
            //取sql语句
            if (this.GetSQL("Pharmacy.Item.QueryValidDates", ref strSQL) == -1)
            {
                strSQL = @"select t.drug_code,min(t.valid_date)
                             from pha_com_storage t
                            where t.drug_dept_code = '{0}'
                              and t.store_sum > 0
                            group by t.drug_code";
            }
            //格式化sql语句
            strSQL = string.Format(strSQL, deptCode);

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得库存信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {

                //用于库存信息存贮
                FS.HISFC.Models.Pharmacy.Storage info;

                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.Storage();

                    info.Item.ID = this.Reader[0].ToString();							    //0 药品编码
                    info.ValidTime = NConvert.ToDateTime(this.Reader[1].ToString());							    //1 药品名称                    

                    al.Add(info);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获得库存信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        #endregion

        #region 为药柜管理添加

        /// <summary>
        /// 更新库存药柜管理数据
        /// </summary>
        /// <param name="storageBase">库存记录类</param>
        /// <returns>成功返回操作条目数 失败返回-1</returns>
        public int SetArkStorage(FS.HISFC.Models.Pharmacy.StorageBase storageBase)
        {
            string strSQL = "";
            //取SQL语句。
            if (this.GetSQL("Pharmacy.Item.SetArkStorage", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.SetArkStorage字段!";
                return -1;
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   storageBase.StockDept.ID,                        //库存科室编码
									   storageBase.Item.ID,                             //药品编码
                                       storageBase.GroupNO.ToString(),                  //库存序号
                                       storageBase.ArkQty.ToString()                    //变化量 加操作
								   };

                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新库存汇总表中的预扣数量时出错！Pharmacy.Item.SetArkStorage" + ex.Message;
                this.WriteErr();
                return -1;
            }
            int parma = this.ExecNoQuery(strSQL);
            if (parma != 1)
            {
                return parma;
            }

            //更新库存汇总信息内相应字段
            //取SQL语句。
            if (this.GetSQL("Pharmacy.Item.SetArkStockinfo", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.SetArkStockinfo字段!";
                return -1;
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   storageBase.StockDept.ID,                        //库存科室编码
									   storageBase.Item.ID,                             //药品编码
                                       storageBase.ArkQty.ToString()
								   };

                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新库存汇总表中的预扣数量时出错！Pharmacy.Item.SetArkStockinfo" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 根据科室编码、药柜管理标记获取科室库存药品列表
        /// </summary>
        /// <param name="deptCode">库存编码</param>
        /// <param name="isArk">是否药柜管理</param>
        /// <returns>成功返回药柜管理药品列表 失败返回null</returns>
        public ArrayList QueryArkFlagDrugByDeptCode(string deptCode, bool isArk)
        {
            string strSQL = "";
            string strWhere = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorageList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList字段!";
                return null;
            }

            //取WHERE条件
            if (this.GetSQL("Pharmacy.Item.GetStorageList.ForArk", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorageList.ForArk字段!";
                return null;
            }

            //格式化SQL语句
            string[] parm = { deptCode, NConvert.ToInt32(isArk).ToString() };
            strSQL = string.Format(strSQL + strWhere, parm);

            return this.myGetStorage(strSQL);
        }

        /// <summary>
        /// 根据科室编码、药品编码判断药品是否药柜管理
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="drugCode">药品编码</param>
        /// <returns>如果药品为药柜管理返回True 否则返回False</returns>
        public bool IsArkManager(string deptCode, string drugCode)
        {
            string strSQL = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.IsArkManager", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.IsArkManager字段!";
                return false;
            }

            //格式化SQL语句
            string[] parm = { deptCode, drugCode };
            strSQL = string.Format(strSQL, parm);

            try
            {
                //执行查询语句
                if (this.ExecQuery(strSQL) == -1)
                {
                    this.Err = "获得库存信息时，执行SQL语句出错！" + this.Err;
                    this.ErrCode = "-1";
                    return false;
                }

                if (this.Reader.Read())
                {
                    return NConvert.ToBoolean(this.Reader[0]);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得库存信息时，执行SQL语句出错！" + ex.Message;
                this.ErrCode = "-1";
                return false;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        #endregion

        #region 基础增、删、改操作

        /// <summary>
        /// 取库存明细信息列表，可能是一条或者多条库存记录
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>成功返回库存对象数组 失败返回null</returns>
        private ArrayList myGetStorage(string SQLString)
        {
            ArrayList al = new ArrayList();                //用于返回库存信息的数组
            FS.HISFC.Models.Pharmacy.Storage storage; //库存信息实体

            //执行查询语句
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得库存信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //取查询结果中的记录
                    storage = new FS.HISFC.Models.Pharmacy.Storage();
                    storage.StockDept.ID = this.Reader[0].ToString();               //0库存科室
                    storage.Item.ID = this.Reader[1].ToString();               //1药品编码
                    storage.GroupNO = NConvert.ToDecimal(this.Reader[2].ToString());    //2批次号  
                    storage.BatchNO = this.Reader[3].ToString();               //3批号
                    storage.Item.Name = this.Reader[4].ToString();             //4药品商品名
                    storage.Item.Specs = this.Reader[5].ToString();            //5规格
                    storage.Item.Type.ID = this.Reader[6].ToString();          //6药品类别
                    storage.Item.Quality.ID = this.Reader[7].ToString();       //7药品性质
                    storage.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[8].ToString());       //8零售价
                    storage.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[9].ToString());    //9批发价
                    storage.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[10].ToString());    //10实进价
                    storage.Item.PackUnit = this.Reader[11].ToString();                             //11包装单位
                    storage.Item.PackQty = NConvert.ToDecimal(this.Reader[12].ToString());          //12包装数
                    storage.Item.MinUnit = this.Reader[13].ToString();                              //13最小单位
                    storage.ShowState = this.Reader[14].ToString();                                 //14显示的单位标记
                    storage.ValidTime = NConvert.ToDateTime(this.Reader[15].ToString());            //15有效期
                    storage.StoreQty = NConvert.ToDecimal(this.Reader[16].ToString());              //16库存数量
                    storage.StoreCost = NConvert.ToDecimal(this.Reader[17].ToString());             //17库存金额
                    storage.PreOutQty = NConvert.ToDecimal(this.Reader[18].ToString());            //18预扣库存数量
                    storage.PreOutCost = NConvert.ToDecimal(this.Reader[19].ToString());           //19预扣库存金额

                    // storage.IsStop = NConvert.ToBoolean( this.Reader[ 20 ].ToString( ) );               //20有效性标志 1 在用 0 停用 2 废弃
                    storage.ValidState = (FS.HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[20].ToString());

                    storage.Producer.ID = this.Reader[21].ToString();                               //21生产厂家
                    storage.LastMonthQty = NConvert.ToDecimal(this.Reader[22].ToString());         //22最近一次月结的库存量
                    storage.PlaceNO = this.Reader[23].ToString();                                 //23货位号
                    storage.State = this.Reader[24].ToString();                                     //24在库状态（0-暂入库，1正式入库）
                    storage.Memo = this.Reader[25].ToString();                                      //25备注
                    storage.Operation.Oper.ID = this.Reader[26].ToString();                                  //26操作人编码
                    storage.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[27].ToString());             //27操作日期
                    storage.InvoiceNO = this.Reader[28].ToString();									//28发票号

                    storage.IsArkManager = NConvert.ToBoolean(this.Reader[29]);
                    storage.ArkQty = NConvert.ToDecimal(this.Reader[30]);

                    al.Add(storage);
                }

                return al;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得库存信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 获得update或者insert库存表的传入参数数组
        /// </summary>
        /// <param name="storageBase">库存类</param>
        /// <returns>成功返回字符串数组 失败返回null</returns>
        private string[] myGetParmStorage(FS.HISFC.Models.Pharmacy.StorageBase storageBase)
        {

            string[] strParm ={   
								 storageBase.StockDept.ID,                       //0库存科室
								 storageBase.Item.ID,                       //1药品编码
								 storageBase.GroupNO.ToString(),            //2批次号  
								 storageBase.BatchNO,                       //3批号
								 storageBase.Item.Name,                     //4药品商品名
								 storageBase.Item.Specs,                    //5规格
								 storageBase.Item.Type.ID,                  //6药品类别
								 storageBase.Item.Quality.ID.ToString(),    //7药品性质
								 storageBase.Item.PriceCollection.RetailPrice.ToString(),   //8零售价
								 storageBase.Item.PriceCollection.WholeSalePrice.ToString(),//9批发价
								 storageBase.Item.PriceCollection.PurchasePrice.ToString(), //10实进价
								 storageBase.Item.PackUnit,                 //11包装单位
								 storageBase.Item.PackQty.ToString(),       //12包装数
								 storageBase.Item.MinUnit.ToString(),       //13最小单位
								 storageBase.ShowState,                     //14显示的单位标记
								 storageBase.ShowUnit,                      //15显示的单位
								 storageBase.ValidTime.ToString(),          //16有效期
								 storageBase.Quantity.ToString(),           //17库存数量
								 (storageBase.Quantity * storageBase.Item.PriceCollection.RetailPrice / storageBase.Item.PackQty).ToString(),//18库存金额
								 storageBase.Producer.ID,                   //19生产厂家
								 storageBase.PlaceNO,                     //20货位号
								 storageBase.TargetDept.ID,                 //21目标科室
								 storageBase.ID,                            //22单据号
								 storageBase.SerialNO.ToString(),           //23单内序号
								 storageBase.Class2Type + "|" + storageBase.PrivType,						//24库存操作类型0310入库,0320出库……
								 storageBase.Memo,                          //25备注
								 this.Operator.ID,                          //26操作人编码
								 storageBase.Operation.Oper.OperTime.ToString(),            //27操作日期
								 storageBase.State,							//28 状态
								 storageBase.InvoiceNO						//29 发票号
							 };
            return strParm;
        }

        /// <summary>
        /// 执行Sql语句 返回药品库存信息数组列表
        /// </summary>
        /// <param name="strSql">需执行的Sql</param>
        /// <returns>成功返回药品数组列表 失败返回null</returns>
        private List<FS.HISFC.Models.Pharmacy.Item> myGetAvailableList(string strSql)
        {
            FS.HISFC.Models.Pharmacy.Item item; //返回数组中的药品信息类

            List<FS.HISFC.Models.Pharmacy.Item> alList = new List<FS.HISFC.Models.Pharmacy.Item>();
            try
            {
                if (this.ExecQuery(strSql) == -1)
                {
                    this.Err = "执行Sql语句发生错误" + this.Err;
                    return null;
                }

                while (this.Reader.Read())
                {
                    item = new FS.HISFC.Models.Pharmacy.Item();

                    item.ID = this.Reader[0].ToString();                                  //0  药品编码
                    item.Name = this.Reader[1].ToString();                                //1  商品名称
                    item.PackQty = NConvert.ToDecimal(this.Reader[2].ToString());         //2  包装数量
                    item.Specs = this.Reader[3].ToString();                               //3  规格
                    item.MinFee.ID = this.Reader[4].ToString();                           //4  最小费用代码
                    item.SysClass.ID = this.Reader[5].ToString();                         //5  系统类别
                    item.PackUnit = this.Reader[6].ToString();                            //6  包装单位
                    item.MinUnit = this.Reader[7].ToString();                             //7  最小单位
                    item.Type.ID = this.Reader[8].ToString();                             //8  药品类别编码
                    item.Quality.ID = this.Reader[9].ToString();                          //9  药品性质编码
                    item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[10].ToString());      //10 零售价
                    item.Product.Producer.ID = this.Reader[11].ToString();                                  //11 生产厂家编码
                    item.SpellCode = this.Reader[12].ToString();                         //12 拼音码  
                    item.WBCode = this.Reader[13].ToString();                            //13 五笔码
                    item.UserCode = this.Reader[14].ToString();                          //14 自定义码
                    item.NameCollection.RegularName = this.Reader[15].ToString();                           //15 药品通用名
                    item.NameCollection.RegularSpell.SpellCode = this.Reader[16].ToString();                //16 通用名拼音码
                    item.NameCollection.RegularSpell.WBCode = this.Reader[17].ToString();                   //17 通用名五笔码
                    item.NameCollection.RegularSpell.UserCode = this.Reader[18].ToString();                 //18 通用名自定义码
                    item.NameCollection.EnglishName = this.Reader[19].ToString();                           //19 英文商品名 
                    item.User01 = this.Reader[20].ToString();                              //20 库存可用数量
                    item.User02 = this.Reader[21].ToString();                             //21 药房编码
                    item.DoseUnit = this.Reader[22].ToString();                           //22 剂量单位
                    item.BaseDose = NConvert.ToDecimal(this.Reader[23].ToString());       //23 基本剂量
                    item.DosageForm.ID = this.Reader[24].ToString();					  //24 剂型编码
                    item.Usage.ID = this.Reader[25].ToString();							  //25 用法编码
                    item.Frequency.ID = this.Reader[26].ToString();						  //26 频次编码
                    item.Grade = this.Reader[27].ToString();						      //27 药品等级：甲乙类
                    item.SpecialFlag = this.Reader[28].ToString();						  //28 省限
                    item.SpecialFlag1 = this.Reader[29].ToString();						  //29 市限	
                    item.SpecialFlag2 = this.Reader[30].ToString();						  //30 自费	
                    item.SpecialFlag3 = this.Reader[31].ToString();						  //31 特殊项目 项目特限标记
                    item.SpecialFlag4 = this.Reader[32].ToString();                       //32 特殊项目

                    alList.Add(item);
                }

                return alList;

            }
            catch (Exception ex)
            {
                this.Err = "获得药品库存时，执行SQL语句出错！" + ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        /// <summary>
        /// 取库存明细信息列表，可能是一条或者多条库存记录
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>成功返回库存对象数组 失败返回null</returns>
        private ArrayList myGetStockinfo(string SQLString)
        {
            ArrayList al = new ArrayList();                  //用于返回库存信息的数组
            FS.HISFC.Models.Pharmacy.Storage storage; //库存信息实体

            //执行查询语句
            if (this.ExecQuery(SQLString) == -1)
            {
                this.Err = "获得库存信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    //取查询结果中的记录
                    storage = new FS.HISFC.Models.Pharmacy.Storage();
                    storage.StockDept.ID = this.Reader[0].ToString();                              //0库存科室
                    storage.Item.ID = this.Reader[1].ToString();                              //1药品编码
                    storage.Item.Name = this.Reader[2].ToString();                            //2药品商品名
                    storage.Item.Specs = this.Reader[3].ToString();                           //3规格
                    storage.Item.Type.ID = this.Reader[4].ToString();                         //4药品类别
                    storage.Item.Quality.ID = this.Reader[5].ToString();                      //5药品性质
                    storage.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[6].ToString());  //6零售价
                    storage.Item.PackUnit = this.Reader[7].ToString();                         //7包装单位
                    storage.Item.PackQty = NConvert.ToDecimal(this.Reader[8].ToString());      //8包装数
                    storage.Item.MinUnit = this.Reader[9].ToString();                          //9最小单位
                    storage.ShowState = this.Reader[10].ToString();                            //10显示的单位标记
                    storage.ValidTime = NConvert.ToDateTime(this.Reader[11].ToString());       //11有效期
                    storage.StoreQty = NConvert.ToDecimal(this.Reader[12].ToString());         //12库存数量
                    storage.StoreCost = NConvert.ToDecimal(this.Reader[13].ToString());        //13库存金额
                    storage.PreOutQty = NConvert.ToDecimal(this.Reader[14].ToString());       //14预扣库存数量
                    storage.PreOutCost = NConvert.ToDecimal(this.Reader[15].ToString());      //15预扣库存金额

                    //storage.IsStop = NConvert.ToBoolean( this.Reader[ 16 ].ToString( ) );          //16有效性标志 0 在用 1 停用 2 废弃
                    storage.ValidState = (FS.HISFC.Models.Base.EnumValidState)NConvert.ToInt32(this.Reader[16].ToString());

                    storage.LowQty = NConvert.ToDecimal(this.Reader[17].ToString());           //17最低库存量
                    storage.TopQty = NConvert.ToDecimal(this.Reader[18].ToString());           //18最高库存量
                    storage.PlaceNO = this.Reader[19].ToString();                            //19货位号
                    storage.IsCheck = NConvert.ToBoolean(this.Reader[20].ToString());          //20日盘点
                    storage.Memo = this.Reader[21].ToString();                                 //21备注
                    storage.Operation.Oper.ID = this.Reader[22].ToString();                             //22操作人编码
                    storage.Operation.Oper.OperTime = NConvert.ToDateTime(this.Reader[23].ToString());        //23操作日期
                    storage.Item.SpellCode = this.Reader[24].ToString();                      //24拼音码
                    storage.Item.WBCode = this.Reader[25].ToString();                         //25五笔码
                    storage.Item.UserCode = this.Reader[26].ToString();                       //26自定义码
                    storage.Item.NameCollection.RegularName = this.Reader[27].ToString();                     //27通用名
                    storage.Item.NameCollection.RegularSpell.SpellCode = this.Reader[28].ToString();     //28通用名拼音码
                    storage.Item.NameCollection.RegularSpell.WBCode = this.Reader[29].ToString();        //29通用名五笔码
                    storage.Item.NameCollection.RegularSpell.UserCode = this.Reader[30].ToString();      //30通用名自定义码

                    storage.Item.ValidState = (FS.HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[31]));
                    //storage.Item.IsStop = NConvert.ToBoolean( this.Reader[ 31 ].ToString( ) );     //31药库有效状态  -- zlw 2006-6-2

                    //by cube 2011-03-14 storage.Item表示字典信息storage表示库存信息
                    //storage.Item.IsLack = NConvert.ToBoolean(this.Reader[32].ToString());     //32 缺药标志     -- zlw 2006-7-7
                    storage.IsLack = NConvert.ToBoolean(this.Reader[32].ToString());     //32 缺药标志    
                    //end by

                    storage.IsArkManager = NConvert.ToBoolean(this.Reader[33].ToString());
                    storage.ArkQty = NConvert.ToDecimal(this.Reader[34]);

                    storage.ManageQuality.ID = this.Reader[35].ToString();

                    if (this.Reader.FieldCount > 36)
                    {
                        storage.Item.NameCollection.FormalName = this.Reader[36].ToString();
                        storage.Item.NameCollection.FormalSpell.SpellCode = this.Reader[37].ToString();
                        storage.Item.NameCollection.OtherName = this.Reader[38].ToString();
                        storage.Item.NameCollection.OtherSpell.SpellCode = this.Reader[39].ToString();
                        storage.Item.DosageForm.ID = this.Reader[40].ToString();
                        storage.Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[41]);
                    }
                    //by cube 2011-03-14 增加门诊、住院缺药等情况
                    if (this.Reader.FieldCount > 42)
                    {
                        if (this.Reader.IsDBNull(42))
                        {
                            storage.IsUseForOutpatient = true;
                        }
                        else
                        {
                            storage.IsUseForOutpatient = NConvert.ToBoolean(this.Reader[42]);
                        }
                    }
                    if (this.Reader.FieldCount > 43)
                    {
                        if (this.Reader.IsDBNull(43))
                        {
                            storage.IsUseForInpatient = true;
                        }
                        else
                        {
                            storage.IsUseForInpatient = NConvert.ToBoolean(this.Reader[43]);
                        }
                    }
                    if (this.Reader.FieldCount > 44)
                    {
                        if (this.Reader.IsDBNull(44))
                        {
                            storage.IsLackForInpatient = false;
                        }
                        else
                        {
                            storage.IsLackForInpatient = NConvert.ToBoolean(this.Reader[44]);
                        }
                    }
                    if (this.Reader.FieldCount > 45)
                    {
                        if (this.Reader.IsDBNull(45))
                        {
                            storage.IsRadix = false;
                        }
                        else
                        {
                            storage.IsRadix = NConvert.ToBoolean(this.Reader[45]);
                        }
                    }
                    if (this.Reader.FieldCount > 46)
                    {
                        if (this.Reader.IsDBNull(46))
                        {
                            storage.Item.IsLack = false;
                        }
                        else
                        {
                            storage.Item.IsLack = NConvert.ToBoolean(this.Reader[46]);
                        }
                    }
                    if (this.Reader.FieldCount > 47)
                    {
                        storage.Item.GBCode = this.Reader[47].ToString();
                    }
                    //end by

                    //by cao-lin 新增门诊拆分、长嘱拆分、临瞩拆分
                    if (this.Reader.FieldCount > 48)
                    {
                        storage.SplitType = this.Reader[48].ToString();
                    }
                    if (this.Reader.FieldCount > 49)
                    {
                        storage.LZSplitType = this.Reader[49].ToString();
                    }
                    if (this.Reader.FieldCount > 50)
                    {
                        storage.CDSplitType = this.Reader[50].ToString();
                    }
                    if (this.Reader.FieldCount > 51)
                    {
                        storage.Item.SpecialFlag3 = this.Reader[51].ToString();
                    }
                    //储藏条件
                    if (this.Reader.FieldCount > 52)
                    {
                        storage.Item.Product.StoreCondition = this.Reader[52].ToString();
                    }
                    if (this.Reader.FieldCount > 53)
                    {
                        storage.Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[53].ToString());
                    }
                    //end by
                    al.Add(storage);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得库存信息时出错！" + ex.Message;
                this.ErrCode = "-1";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 向库存明细表中插入一条记录
        /// </summary>
        /// <param name="storageBase">库存记录类</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertStorage(FS.HISFC.Models.Pharmacy.StorageBase storageBase)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InsertStorage", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertStorage字段!";
                return -1;
            }
            try
            {
                string[] strParm = myGetParmStorage(storageBase);     //取参数列表
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入库存记录SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除库存明细记录
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="drugCode">药品编码</param>
        /// <param name="groupNo">批次</param>
        /// <returns>0没有删除 1成功 -1失败</returns>
        public int DeleteStorage(string deptCode, string drugCode, int groupNo)
        {
            string strSQL = "";
            //根据库存记录流水号删除某一条库存记录的DELETE语句
            if (this.GetSQL("Pharmacy.Item.DeleteStorage", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, drugCode, deptCode, groupNo);
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.DeleteStorage";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #endregion

        #region 预扣库存管理  {C37BEC96-D671-46d1-BCDD-C634423755A4}

        /// <summary>
        /// 形成库存预扣信息
        /// </summary>
        /// <param name="applyOut"></param>
        /// <param name="alterStoreNum"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        protected int InsertPreoutStore(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, decimal alterStoreNum, decimal days)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InsertPreoutStore", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.InsertPreoutStore字段!";
                return -1;
            }
            try
            {
                string[] strParm = new string[] {   applyOut.ID,            //ApplyNum
                                                    applyOut.StockDept.ID,
                                                    applyOut.SystemType,
                                                    applyOut.Item.ID,
                                                    applyOut.Item.Name,
                                                    applyOut.Item.Specs,
                                                    applyOut.Operation.ApplyQty.ToString(),
                                                    applyOut.Days.ToString(),
                                                    applyOut.Operation.ApplyOper.ID,
                                                    applyOut.Operation.ApplyOper.OperTime.ToString(),
                                                    applyOut.PatientNO
                        
                                                };
                strSQL = string.Format(strSQL, strParm);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入库存记录SQl参数赋值时出错！" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 扣减库存预扣信息
        /// </summary>
        /// <param name="applyID"></param>
        /// <param name="alterStoreNum"></param>
        /// <param name="days"></param>
        /// <returns></returns>
        protected int DeletePreoutStore(string applyID, decimal alterStoreNum, decimal days)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.DeletePreoutStore", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.DeletePreoutStore字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, applyID);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "插入库存记录SQl参数赋值时出错！" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新库存汇总表中的预扣数量（正数是增加，负数是减少）
        /// </summary>
        /// <param name="applyOut">申请信息</param>
        /// <param name="alterStoreNum">预扣变化数量 正数是增加，负数是减少</param>
        /// <param name="days">付数</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateStockinfoPreOutNum(FS.HISFC.Models.Pharmacy.ApplyOut applyOut, decimal alterStoreNum, decimal days)
        {
            if (alterStoreNum > 0)
            {
                return this.InsertPreoutStore(applyOut, alterStoreNum, days);
            }
            else
            {
                //预扣库存方式修改（医生站预扣/收费预扣） by Sunjh 2010-9-28 {E0A27FD4-540B-465d-81C0-11C8DA2F96C5}
                FS.FrameWork.Management.ControlParam controlerManager = new FS.FrameWork.Management.ControlParam();
                string preOutType = controlerManager.QueryControlerInfo("P01015");
                if (preOutType == "0")
                {
                    return this.DeletePreoutStore(applyOut.OrderNO, alterStoreNum, days);
                }
                else
                {
                    return this.DeletePreoutStore(applyOut.ID, alterStoreNum, days);
                }
            }

        }

        /// <summary>
        /// 获取预扣库存量
        /// </summary>
        /// <param name="drugDeptCode">库房科室编码</param>
        /// <param name="drugCode">药品编码</param>
        /// <returns>成功返回预扣库存量 失败返回-1</returns>
        public decimal GetPreOutNum(string drugDeptCode, string drugCode)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.PreoutStore.GetPreOutNum", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.PreoutStore.GetPreOutNum字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, drugDeptCode, drugCode);            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "获取预扣库存量SQl参数赋值时出错！" + ex.Message;
                return -1;
            }

            string preOutStr = this.ExecSqlReturnOne(strSQL);

            if (string.IsNullOrEmpty(preOutStr) == true)        //没有找到相应数据
            {
                return 0;
            }
            else
            {
                decimal preOutNum = FS.FrameWork.Function.NConvert.ToDecimal(preOutStr);
                return preOutNum;
            }
        }
        #endregion

        #region 库存备份

        /// <summary>
        /// 库存备份
        /// </summary>
        /// <param name="stockDeptNO">库存科室编码</param>
        /// <param name="backTime">备份时间</param>
        /// <returns></returns>
        public int BackStorage(string stockDeptNO, DateTime backTime)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Item.BackStorage", ref strSQL) == -1)
            {
                //this.Err = "没有找到SOC.Pharmacy.Item.BackStorage字段!";
                //return -1;
                strSQL = @"
                            insert into pha_com_cstorage
                            select to_date('{1}','yyyy-mm-dd hh24:mi:ss'),
                                   t.group_code,
                                   t.drug_dept_code,
                                   t.drug_code,
                                   t.batch_no,
                                   t.trade_name,
                                   t.specs,
                                   t.drug_type,
                                   t.drug_quality,
                                   t.retail_price,
                                   t.wholesale_price,
                                   t.purchase_price,
                                   t.pack_unit,
                                   t.pack_qty,
                                   t.min_unit,
                                   t.show_flag,
                                   t.show_unit,
                                   t.valid_date,
                                   t.store_sum,
                                   t.store_cost,
                                   t.preout_sum,
                                   t.preout_cost,
                                   t.valid_flag,
                                   t.producer_code,
                                   t.last_month_num,
                                   t.place_code,
                                   t.store_seq,
                                   t.state,
                                   t.target_dept,
                                   t.bill_code,
                                   t.serial_code,
                                   t.mark,
                                   t.oper_code,
                                   t.oper_date,
                                   t.class2_code,
                                   t.invoice_no,
                                   t.ark_flag,
                                   t.ark_qty
                              from pha_com_storage t
                             where t.drug_dept_code = '{0}'
                                   and t.store_sum <> 0
                         ";

                this.CacheSQL("SOC.Pharmacy.Item.BackStorage", strSQL);
            }
            try
            {
                strSQL = string.Format(strSQL, stockDeptNO, backTime.ToString());            //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "备份库存SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }
        #endregion

        #region 台账更新
        /// <summary>
        /// 根据入库信息更新台账
        /// </summary>
        /// <returns>成功返回1 失败返回-1</returns>
        public int UpdateRecord(FS.HISFC.Models.Pharmacy.Input input)
        {
            string strSQL = "";
            //取SQL语句。
            if (this.GetSQL("SOC.Pharmacy.Item.UpdateRecord.ByInput", ref strSQL) == -1)
            {
                //this.Err = "没有找到SOC.Pharmacy.Item.UpdateRecord.ByInput字段!";
                //return -1;

                strSQL = @"
                            update pha_com_record r
                            set    r.goal_dept_code = '{0}',
                                   r.invoice_no = '{1}'
                            where  r.record_type = '{2}'
                            and    r.source_dept_code = '{3}'
                            and    r.drug_code = '{4}'
                            and    r.bill_code = '{5}'
                            and    r.oper_date > to_date('{6}','yyyy-mm-dd hh24:mi:ss')
                          ";
                this.CacheSQL("SOC.Pharmacy.Item.UpdateRecord.ByInput", strSQL);
            }
            try
            {
                //取参数列表
                string[] strParm = {
									   input.Company.ID,                       
									   input.InvoiceNO,                       
									   input.Class2Type,
                                       input.StockDept.ID,
                                       input.Item.ID,
                                       input.ID,
                                       input.InDate.AddDays(-10).ToString()

								   };

                strSQL = string.Format(strSQL, strParm);        //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "更新库存汇总表中的零售价时出错！SOC.Pharmacy.Item.UpdateRecord.ByInput" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region 医保相关
        /// <summary>
        /// 获取医保项目等级
        /// </summary>
        /// <param name="pactCode"></param>
        /// <returns></returns>
        public ArrayList GetCompareSiDrugItem(string pactCode)
        {
            ArrayList allCompareSiDrugItem = new ArrayList();
            string strSQL = "";
            string strWhere = "";
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.CompareSiDrugItem", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.CompareSiDrugItem字段!";
                return null;
            }

            //格式化SQL语句
            strSQL = string.Format(strSQL, pactCode);

            this.ExecQuery(strSQL);

            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject siItem = new FS.FrameWork.Models.NeuObject();
                    siItem.ID = this.Reader[0].ToString();
                    siItem.Name = this.Reader[1].ToString();
                    allCompareSiDrugItem.Add(siItem);
                }
            }
            catch (Exception ex) { this.Reader.Close(); return null; }
            finally
            {
                this.Reader.Close();
            }

            //取药品库存总数量
            return allCompareSiDrugItem;
        }

        #endregion

        /// <summary>
        /// 获取某药品在某药房的货位号
        /// </summary>
        /// <param name="deptCode"></param>
        /// <param name="drugCode"></param>
        /// <returns></returns>
        public string GetPlaceNO(string deptCode, string drugCode)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.GetStockPlaceNo.ByDrugCode.Optimize", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStockPlaceNo.ByDrugCode.Optimize字段!";
                return null;
            }

            strSQL = string.Format(strSQL, deptCode, drugCode);
            return this.ExecSqlReturnOne(strSQL, "");
        }

        /// <summary>
        /// 获取库存中药品类别列表
        /// </summary>
        /// <param name="deptCode">库存科室</param>
        /// <returns></returns>
        public Hashtable GetStockDrugTypeList(string deptCode)
        {
            string strSQL = "";
            if (this.GetSQL("SOC.Pharmacy.Storage.GetDrugTypeList", ref strSQL) == -1)
            {
                strSQL = @"select distinct s.drug_type
                             from pha_com_stockinfo s
                            where s.drug_dept_code = '{0}'
                              and s.valid_state = '1'";
                this.CacheSQL("SOC.Pharmacy.Storage.GetDrugTypeList", strSQL);
            }

            strSQL = string.Format(strSQL, deptCode);

            Hashtable hsDrugType = new Hashtable();

            //执行查询语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获得库存信息时，执行SQL语句出错！" + this.Err;
                this.ErrCode = "-1";
                return null;
            }
            while (this.Reader.Read())
            {
                if(hsDrugType.Contains(this.Reader[0].ToString()))
                {
                    continue;
                }
                hsDrugType.Add(this.Reader[0].ToString(), null);
            }

            return hsDrugType;
        }
    }
}
