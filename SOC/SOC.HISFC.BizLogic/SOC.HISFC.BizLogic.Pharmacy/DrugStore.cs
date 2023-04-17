using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.HISFC.Models.Pharmacy;
using FS.FrameWork.Function;
using FS.HISFC.Models.Base;

namespace FS.SOC.HISFC.BizLogic.Pharmacy
{
    /// <summary>
    /// [功能描述: 药品药房管理类]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2011-06]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public class DrugStore : Apply
    {
                
        #region 静态变量

        /// <summary>
        /// 科室地址 (返回发药信息  前置字符)
        /// </summary>
        public static System.Collections.Hashtable hsDeptAddress = null;

        /// <summary>
        /// 用法对照
        /// </summary>
        internal static System.Collections.Hashtable hsUsageContrast = null;

        /// <summary>
        /// 剂型对照
        /// </summary>
        internal static System.Collections.Hashtable hsDosageContrast = null;

        /// <summary>
        /// 是否已初始化收费窗口
        /// </summary>
        private static bool isInitSendWindow = false;

        /// <summary>
        /// 收费窗口
        /// </summary>
        private static string feeWindowNO = "";
        #endregion

        #region 摆药单分类

        #region 基础增、删、改操作

        /// <summary>
        /// 获得update或者insert摆药单分类表的传入参数数组
        /// </summary>
        /// <param name="FS.HISFC.Models.Pharmacy.DrugBillClass">入库申请类</param>
        /// <returns>成功返回字符串数组 失败返回null</returns>
        private string[] myGetParmDrugBillClass(FS.HISFC.Models.Pharmacy.DrugBillClass drugBillClass)
        {
            #region "接口说明"
            //1、摆药分类代码
            //2、摆药分类名称
            //3、打印类型1汇总2明细3草药4大处方
            //4、摆药类型
            //5、停用标记1-停用0－有效
            //6、操作员
            //7、操作时间
            //8、备注

            #endregion
            string[] strParm ={   drugBillClass.ID,
								 drugBillClass.Name,
								 drugBillClass.DrugAttribute.ID.ToString(),
								 drugBillClass.PrintType.ID.ToString(),
				NConvert.ToInt32(drugBillClass.IsValid).ToString(),
								 drugBillClass.Memo,
								 this.Operator.ID
							 };

            return strParm;
        }

        /// <summary>
        /// 取摆药单分类信息列表，可能是一条或者多条药品记录
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>摆药单分类数组</returns>
        private ArrayList myGetDrugBillClass(string SQLString)
        {
            ArrayList al = new ArrayList();  //用于返回药品信息的数组
            FS.HISFC.Models.Pharmacy.DrugBillClass info;            //返回数组中的摆药单分类信息

            this.ExecQuery(SQLString);
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.DrugBillClass();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                          //摆药单分类编码
                        info.Name = this.Reader[1].ToString();                        //摆药单分类名称
                        info.PrintType.ID = this.Reader[2].ToString();                //打印类型
                        info.IsValid = NConvert.ToBoolean(this.Reader[3].ToString()); //是否有效
                        info.Memo = this.Reader[4].ToString();                        //备注
                        info.Oper.ID = this.Reader[5].ToString();                //操作员编码
                        try { info.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[6].ToString()); }
                        catch { }//操作时间
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得摆药单分类信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得摆药单分类信息时，执行SQL语句出错！myGetDrugBillClass" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return al;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 插入一条摆药单分类记录
        /// </summary>
        /// <param name="info">摆药单分类实体</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int InsertDrugBillClass(FS.HISFC.Models.Pharmacy.DrugBillClass info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugBillClass", ref strSql) == -1) return -1;
            try
            {
                //取摆药单分类流水号
                string ID = "";
                ID = this.GetSysDateTime("yyMMddHHmmss");
                if (ID == "-1") return -1;

                //赋值给info.ID，以便调用此方法的对象使用此摆药单分类流水号
                if (info.ID != "P" && info.ID != "R")
                {
                    info.ID = ID;
                }

                string[] strParm = myGetParmDrugBillClass(info);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新一条摆药单分类记录
        /// </summary>
        /// <param name="info">摆药单分类实体</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int UpdateDrugBillClass(FS.HISFC.Models.Pharmacy.DrugBillClass info)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugBillClass", ref strSql) == -1) return -1;

            try
            {
                string[] strParm = myGetParmDrugBillClass(info);  //取参数列表
                strSql = string.Format(strSql, strParm);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据摆药单分类编码,删除一条记录
        /// </summary>
        /// <param name="ID">摆药单分类编码</param>
        /// <returns>成功返回删除条数 失败返回null</returns>
        public int DeleteDrugBillClass(string ID)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugBillClass", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.FS.HISFC.Models.Pharmacy.DrugBillClass";
                return -1;
            }
            int parm = this.ExecNoQuery(strSql);
            if (parm == -1) return -1;

            //删除摆药分类的同时，删除摆药分类明细数据
            return this.DeleteDrugBillList(ID);
        }

        /// <summary>
        /// 保存摆药单分类－－先执行更新操作，如果没有找到可以更新的数据，则插入一条新记录
        /// </summary>
        /// <param name="info">摆药单分类实体</param>
        /// <returns>0未更新，大于1成功，-1失败</returns>
        public int SetDrugBillClass(FS.HISFC.Models.Pharmacy.DrugBillClass info)
        {
            int parm;
            #region 先更新后插入
            //			//执行更新操作
            //			parm = UpdateDrugBillClass(info);
            //
            //			//如果没有找到可以更新的数据，则插入一条新记录
            //			if (parm == 0 )
            //			{
            //				parm = InsertDrugBillClass(info);
            //			}
            #endregion

            //如果是新增加的数据则插入一条新记录，否则更新此记录
            if (info.ID == "")
                parm = InsertDrugBillClass(info);
            else
                parm = UpdateDrugBillClass(info);

            return parm;
        }

        #endregion

        #region 内部使用

        /// <summary>
        /// 根据摆药单分类编码获得某一摆药单分类信息
        /// </summary>
        /// <param name="ID">摆药单分类编码</param>
        /// <returns>成功返回摆药单分类信息 失败返回null</returns>
        public FS.HISFC.Models.Pharmacy.DrugBillClass GetDrugBillClass(string ID)
        {
            string strSelect = "";  //获得摆药单分类信息的SELECT语句
            string strSQL = "";  //根据摆药单分类编码获得某一摆药单分类信息的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillClass", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugBillClass字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillClass.Where", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugBillClass.Where字段!";
                return null;
            }

            try
            {
                strSQL = string.Format(strSelect + " " + strSQL, ID);
            }
            catch
            {
                return null;
            }

            //根据SQL语句取摆药单分类数组并返回数组中的首条记录
            try
            {
                ArrayList al = this.myGetDrugBillClass(strSQL);
                //如果没有取到数据，提示错误
                if (al.Count == 0)
                {
                    this.Err = "没有找到对应的摆药单！ 编码：" + ID;
                    this.WriteErr();
                    return null;
                }
                return (FS.HISFC.Models.Pharmacy.DrugBillClass)al[0];
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据摆药单分类明细（医嘱类型，用法，药品类型，药品性质，药品剂型）获得某一摆药单分类信息
        /// </summary>
        /// <param name="orderType">医嘱类型</param>
        /// <param name="usageCode">用法</param>
        /// <param name="drugType">药品类型</param>
        /// <param name="drugQuality">药品性质</param>
        /// <param name="dosageFormCode">药品剂型</param>
        /// <returns>查找成功返回实体 失败返回null 未找到返回ErrCode -1</returns>
        public FS.HISFC.Models.Pharmacy.DrugBillClass GetDrugBillClass(string orderType, string usageCode, string drugType, string drugQuality, string dosageFormCode)
        {
            string strSQL = "";  //获得摆药单分类信息的SQL语句

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillClass.ByList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugBillClass.ByList字段!";
                return null;
            }

            try
            {
                string[] parm = {
									orderType,
									usageCode,
									drugType,
									drugQuality,
									dosageFormCode
								};
                strSQL = string.Format(strSQL, parm);
            }
            catch
            {
                return null;
            }

            //根据SQL语句取摆药单分类数组并返回数组中的首条记录
            try
            {
                ArrayList al = this.myGetDrugBillClass(strSQL);
                //如果没有取到数据，提示错误
                if (al.Count == 0)
                {
                    this.Err = "没有找到对应的摆药单，请检查是否在摆药单中维护了数据。\n医嘱类型:" + orderType
                        + " \n药品类型:" + drugType + " \n用法:" + usageCode + " \n药品性质:" + drugQuality + " \n药品剂型:" + dosageFormCode;
                    this.ErrCode = "-1";
                    this.WriteErr();
                    return null;
                }
                return (FS.HISFC.Models.Pharmacy.DrugBillClass)al[0];
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// 根据摆药单分类明细（医嘱类型，用法，药品类型，药品性质，药品剂型）获得某一摆药单分类信息
        /// </summary>
        /// <param name="isUsageDosageClass">摆药单是否使用用法/剂型大类</param>
        /// <param name="orderType">医嘱类型</param>
        /// <param name="usageCode">用法</param>
        /// <param name="drugType">药品类型</param>
        /// <param name="drugQuality">药品性质</param>
        /// <param name="dosageFormCode">药品剂型</param>
        /// <returns>查找成功返回实体 失败返回null 未找到返回ErrCode -1</returns>
        public FS.HISFC.Models.Pharmacy.DrugBillClass GetDrugBillClass(bool isUsageDosageClass, string orderType, string usageCode, string drugType, string drugQuality, string dosageFormCode)
        {
            //if (!isUsageDosageClass)
            //{
            //    return GetDrugBillClass(orderType, usageCode, drugType, drugQuality, dosageFormCode);
            //}
            //else
            //{
            //    //获取传入参数的用法/剂型对应的大类明细
            //    if (DrugStore.hsUsageContrast == null || DrugStore.hsDosageContrast == null)      //获取用法大类对照
            //    {
            //        Constant consManager = new Constant();
            //        if (this.Trans != null)
            //        {
            //            consManager.SetTrans(this.Trans);
            //        }
            //        //获取用法大类对照信息
            //        ArrayList alUsageContrast = consManager.GetList("USAGECONTRAST");
            //        if (alUsageContrast == null || alUsageContrast.Count == 0)
            //        {
            //            DrugStore.hsUsageContrast = new Hashtable();
            //        }
            //        else
            //        {
            //            foreach (FS.HISFC.Models.Base.Const usageContrast in alUsageContrast)
            //            {
            //                DrugStore.hsUsageContrast.Add(usageContrast.ID, usageContrast.Name);
            //            }
            //        }
            //        //获取剂型大类对照信息
            //        ArrayList alDosageContrast = consManager.GetList("DOSAGECONTRAST");
            //        if (alDosageContrast == null || alDosageContrast.Count == 0)
            //        {
            //            DrugStore.hsDosageContrast = new Hashtable();
            //        }
            //        else
            //        {
            //            foreach (FS.HISFC.Models.Base.Const dosageContrast in alDosageContrast)
            //            {
            //                DrugStore.hsDosageContrast.Add(dosageContrast.ID, dosageContrast.Name);
            //            }
            //        }
            //    }
            //    //获取对照大类
            //    if (DrugStore.hsDosageContrast.ContainsKey(dosageFormCode))
            //    {
            //        dosageFormCode = DrugStore.hsDosageContrast[dosageFormCode] as string;
            //    }
            //    //获取对照大类
            //    if (DrugStore.hsUsageContrast.ContainsKey(usageCode))
            //    {
            //        usageCode = DrugStore.hsUsageContrast[usageCode] as string;
            //    }

                return GetDrugBillClass(orderType, usageCode, drugType, drugQuality, dosageFormCode);
            //}
        }

        /// <summary>
        /// 获得摆药单分类信息列表
        /// </summary>
        /// <returns>摆药单分类数组</returns>
        public ArrayList QueryDrugBillClassList()
        {
            string strSelect = "";  //获得全部摆药单分类信息的SELECT语句
            string strOrder = "";  //获得全部摆药单分类信息的ORDER语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillClass", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugBillClass字段!";
                return null;
            }

            //取ORDER条件语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillClass.Order", ref strOrder) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugBillClass.Order字段!";
                return null;
            }

            //根据SQL语句取摆药单分类数组并返回数组
            return this.myGetDrugBillClass(strSelect + strOrder);
        }

        #endregion

        #endregion 摆药单分类

        #region 摆药单分类明细

        /// <summary>
        /// 根据摆药单分类编码获得摆药单分类明细
        /// </summary>
        /// <param name="drugBillClassCode">分类编码</param>
        /// <param name="column">指定去处重复的列名称</param>
        /// <returns>成功返回摆药单分类明细 失败返回null</returns>
        public ArrayList QueryDrugBillList(string drugBillClassCode, string column)
        {
            ArrayList al = new ArrayList();
            string strSelect = "";  //获得全部摆药单分类信息的SELECT语句

            //临时使用固定的sql语句，以后会有变化
            strSelect = "SELECT DISTINCT " + column + " FROM PHA_STO_BILLLIST WHERE BILLCLASS_CODE = '" + drugBillClassCode + "'";
            DrugBillList info;            //返回数组中的摆药单分类信息

            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "取摆药单分类明细时出错：" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    info = new DrugBillList();
                    try
                    {
                        info.ID = this.Reader[0].ToString();
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得摆药单分类明细信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }

                return al;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得摆药单分类信息时，执行SQL语句出错！myGetDrugBillClass" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        #region 基础增、删、改操作

        /// <summary>
        /// 插入摆药单分类明细记录
        /// </summary>
        /// <param name="info">摆药单分类明细实体</param>
        /// <returns>1成功，-1失败</returns>
        public int InsertDrugBillList(FS.HISFC.Models.Pharmacy.DrugBillList info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugBillList", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = myGetParmDrugBillList(info);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据摆药单分类编码,删除摆药单分类明细
        /// </summary>
        /// <param name="ID">摆药单分类编码</param>
        /// <returns>0没有更新的数据，1成功，-1失败</returns>
        public int DeleteDrugBillList(string ID)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugBillList", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.DeleteDrugBillList";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 获得update或者insert摆药单分类明细表的传入参数数组
        /// </summary>
        /// <param name="DrugBillList">入库申请类</param>
        /// <returns>成功返回字符串参数数组 失败返回null</returns>
        private string[] myGetParmDrugBillList(DrugBillList DrugBillList)
        {
            #region "接口说明"
            //摆药分类代码
            //医嘱类型编码
            //药品用法编码
            //药品类型编码
            //药品性质编码
            //剂型编码

            #endregion
            string[] strParm ={
								 DrugBillList.DrugBillClass.ID,
								 DrugBillList.OrderType.ID,
								 DrugBillList.Usage.ID,
								 DrugBillList.DrugType.ID,
								 DrugBillList.DrugQuality.ID,
								 DrugBillList.DosageForm.ID
							 };

            return strParm;
        }

        #endregion

        #endregion 摆药单分类明细

        #region 摆药单号

        public string GetDeptDrugBillNOLogo(string deptCode)
        {
            string deptLogo = string.Empty;
            try
            {
                string strSQL = @"select e.string_property from com_extend e
                            where e.extend_class = 'DEPT' 
                              and e.item_code = '{0}'
                              and e.property_code = 'DrugBillSequenceNO'";
                strSQL = string.Format(strSQL, deptCode);
                deptLogo = this.ExecSqlReturnOne(strSQL);
            }
            catch (Exception)
            {
            }
            return deptLogo;
        }
        #endregion

        #region 摆药台

        #region 内部使用

        /// <summary>
        /// 取摆药台流水号
        /// </summary>
        /// <returns>"-1"出错，oterhs 成功</returns>
        public string GetDrugControlNO()
        {
            #region //格式化时间错误 {35DE4ACA-F66C-47fd-845C-5AFF253731F7} wbo 2010-08-23
            //return this.GetSysDateTime("YYMMDDHH24MISS");
            string conNO = "-1";
            try
            {
                conNO = this.GetDateTimeFromSysDateTime().ToString("yyMMddHHmmss");
            }
            catch (Exception e)
            {
                conNO = "-1";
            }
            return conNO;
            #endregion
        }

        /// <summary>
        /// 根据科室编码和摆药性质，取摆药台信息
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="drugAtr">摆药台性质</param>
        /// <returns>成功返回摆药台数组 失败返回null</returns>
        public FS.HISFC.Models.Pharmacy.DrugControl GetDrugControl(string deptCode, FS.HISFC.Models.Pharmacy.DrugAttribute.enuDrugAttribute drugAtr)
        {
            string strSQL = "";  //获得某一科室全部摆药台信息的SQL语句

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugControl", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugControl字段!";
                return null;
            }

            //根据SQL语句取摆药台数组并返回数组中的首条记录
            try
            {
                string[] parm = { deptCode, drugAtr.ToString() };
                strSQL = string.Format(strSQL, parm);
                //取摆药台数组
                ArrayList al = this.myGetDrugControl(strSQL);
                //如果没有取到数据，则返回新实体
                if (al.Count == 0)
                {
                    DrugControl info = new DrugControl();
                    info.Dept.ID = deptCode;
                    info.DrugAttribute.ID = drugAtr;
                    return info;
                }
                //返回数组中的首条记录
                return al[0] as DrugControl;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 根据科室编码，取本科室的全部摆药台列表
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns>摆药台数组</returns>
        public ArrayList QueryDrugControlList(string deptCode)
        {
            ArrayList al = new ArrayList();
            string strSQL = "";  //获得某一科室全部摆药台信息的SELECT语句

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugControlList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugControlList字段!";
                return null;
            }

            strSQL = string.Format(strSQL, deptCode);
            //取摆药台数据列表			
            return this.myGetDrugControl(strSQL);
        }

        /// <summary>
        /// 根据摆药台编码，取此摆药台中的全部明细
        /// </summary>
        /// <param name="drugControlCode">摆药台编码</param>
        /// <returns>摆药单分类数组</returns>
        public ArrayList QueryDrugControlDetailList(string drugControlCode)
        {
            ArrayList al = new ArrayList();
            string strSelect = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugControlDetailList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugControlDetailList字段!";
                return null;
            }

            FS.HISFC.Models.Pharmacy.DrugBillClass info;   //摆药单分类实体			

            strSelect = string.Format(strSelect, drugControlCode);
            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "取摆药台明细列表时出错：" + this.Err;
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Pharmacy.DrugBillClass();
                    try
                    {
                        info.ID = this.Reader[0].ToString();                          //摆药单分类编码
                        info.Name = this.Reader[1].ToString();                        //摆药单分类名称
                        info.PrintType.ID = this.Reader[2].ToString();                //打印类型
                        info.IsValid = NConvert.ToBoolean(this.Reader[3].ToString()); //是否有效
                        info.Memo = this.Reader[4].ToString();                        //备注
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得摆药台明细列表时出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }
                return al; ;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得摆药台明细列表时，执行SQL语句出错！myGetDrugBillClass" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        #endregion

        #region 基础增、删、改操作

        /// <summary>
        /// 向摆药台表中插入一条记录
        /// </summary>
        /// <param name="info">摆药台实体</param>
        /// <returns>1成功，-1失败</returns>
        public int InsertDrugControl(DrugControl info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugControl", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = myGetParmDrugControl(info);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除一个摆药台（数据库中是多条记录）
        /// </summary>
        /// <param name="ID">摆药台编码</param>
        /// <returns>0没有更新的数据，1成功，-1失败</returns>
        public int DeleteDrugControl(string ID)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugControl", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, ID);

            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.DrugControl";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 获得update或者insert摆药单分类明细表的传入参数数组
        /// </summary>
        /// <param name="drugControl">入库申请类</param>
        /// <returns>成功返回字符串数组 失败返回null</returns>
        private string[] myGetParmDrugControl(DrugControl drugControl)
        {
            #region "接口说明"
            //摆药台编码
            //摆药台名称
            //摆药单分类编码
            //摆药单分类明处
            //摆药属性
            //科室编码

            #endregion
            string[] strParm ={
								 drugControl.ID,                         //摆药台编码
								 drugControl.Name,                       //摆药台名称
								 drugControl.DrugBillClass.ID,           //摆药单分类编码
								 drugControl.DrugBillClass.Name,         //摆药单分类明处
								 drugControl.DrugAttribute.ID.ToString(),//摆药属性
								 drugControl.SendType.ToString(),        //医嘱发送类型	
								 drugControl.Dept.ID,			         //科室编码					 
								 this.Operator.ID,                       //操作员
								 drugControl.Memo,                       //备注
								 drugControl.ShowLevel.ToString(),        //显示等级：0显示科室汇总，1显示科室明细，2显示患者明细
                                 NConvert.ToInt32(drugControl.IsAutoPrint).ToString(), //是否自动打印
                                 NConvert.ToInt32(drugControl.IsPrintLabel).ToString(),//出院带药是否打印门诊标签
                                 NConvert.ToInt32(drugControl.IsBillPreview).ToString(),//摆药单是否需要预览
                                 drugControl.ExtendFlag,
                                 drugControl.ExtendFlag1
							 };

            return strParm;
        }

        /// <summary>
        /// 取摆药台信息
        /// </summary>
        /// <param name="strSQL">取摆药台的SQL语句</param>
        /// <returns>成功返回摆药台数组 失败返回null</returns>
        private ArrayList myGetDrugControl(string strSQL)
        {
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取摆药台时出错：" + this.Err;
                return null;
            }
            ArrayList al = new ArrayList();
            try
            {
                DrugControl info;   //摆药台实体	
                while (this.Reader.Read())
                {
                    info = new DrugControl();
                    info.ID = this.Reader[0].ToString();                 //摆药台编码
                    info.Name = this.Reader[1].ToString();               //摆药台名称
                    info.DrugAttribute.ID = this.Reader[2].ToString();   //摆药属性
                    info.SendType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());//医嘱发送类型
                    info.Dept.ID = this.Reader[4].ToString();            //科室编码
                    info.Memo = this.Reader[5].ToString();               //备注
                    info.ShowLevel = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6].ToString());//显示等级：0显示科室汇总，1显示科室明细，2显示患者明细
                    info.IsAutoPrint = NConvert.ToBoolean(this.Reader[7].ToString());
                    info.IsPrintLabel = NConvert.ToBoolean(this.Reader[8].ToString());
                    info.IsBillPreview = NConvert.ToBoolean(this.Reader[9].ToString());
                    info.ExtendFlag = this.Reader[10].ToString();
                    info.ExtendFlag1 = this.Reader[11].ToString();

                    al.Add(info);
                }
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得摆药台时出错！" + ex.Message;
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

        #endregion

        #endregion 摆药台

        #region 摆药通知

        #region 内部使用

        /// <summary>
        /// 获得某一申请科室的未摆药通知列表
        /// </summary>
        /// <param name="sendDeptCode">申请科室编码</param>
        /// <returns>成功返回摆药通知信息 失败返回null</returns>
        public ArrayList QueryDrugMessageList(string sendDeptCode)
        {
            string strSQL = "";    //获得某一申请科室的全部摆药通知列表的SELECT语句

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.BySendDept", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugMessageList.BySendDept字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, sendDeptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.BySendDept";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

        /// <summary>
        /// 获得某一申请科室的全部摆药通知列表
        /// </summary>
        /// <param name="sendDeptCode">申请科室编码</param>
        /// <returns>成功返回摆药通知列表 失败返回null</returns>
        public ArrayList QueryAllDrugMessageList(string sendDeptCode)
        {
            string strSQL = "";    //获得某一申请科室的全部摆药通知列表的SELECT语句

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetAllDrugMessageList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetAllDrugMessageList字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, sendDeptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetAllDrugMessageList";
                return null;
            }

            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取摆药通知列表时出错：" + this.Err;
                return null;
            }
            try
            {
                DrugMessage info;   //摆药通知实体		
                while (this.Reader.Read())
                {
                    info = new DrugMessage();
                    try
                    {
                        info.StockDept.ID = this.Reader[0].ToString();          //发送科室编码
                        info.StockDept.Name = this.Reader[1].ToString();          //发送科室名称
                        info.DrugBillClass.ID = this.Reader[2].ToString();          //摆药单分类编码
                        info.Name = this.Reader[3].ToString();          //摆药单分类名称
                        info.SendType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());      //摆药类型1-集中摆药2-临时摆药
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得摆药通知信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }
                    al.Add(info);
                }
                return al;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得摆药通知信息时，执行SQL语句出错！myGetDrugBillClass" + ex.Message;
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
        /// 获得某一摆药台中全部摆药通知列表
        /// SendType=1集中，2临时
        /// 当SendType＝0时，显示全部类型的摆药通知。
        /// </summary>
        /// <param name="drugControl">摆药台</param>
        /// <returns>成功返回摆药通知数组 失败返回null</returns>
        public ArrayList QueryDrugMessageList(DrugControl drugControl)
        {
            //如果没有指定发送科室，则取全部发送科室的通知
            string strSQL = "";    //获得某一摆药台（摆药台中有科室信息）中全部摆药通知列表的SELECT语句

            #region 取手术室摆药单
            //取SQL语句
            //			if (drugControl.ID =="P") {
            //				if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.ByOPR",ref strSQL) == -1) {
            //					this.Err="没有找到Pharmacy.DrugStore.GetDrugMessageList.ByOPR字段!";
            //					return null;
            //				}
            //				try {
            //					string[] strParm={drugControl.Dept.ID};
            //					strSQL = string.Format(strSQL, strParm);
            //				}
            //				catch(Exception ex) {
            //					this.ErrCode=ex.Message;
            //					this.Err=ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByOPR";
            //					return null;
            //				}
            //			}
            #endregion

            if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.ByDrugControl", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugMessageList.ByDrugControl字段!";
                return null;
            }
            try
            {
                string[] strParm = { drugControl.ID };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByDrugControl";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

       

        /// <summary>
        /// 获得某一摆药通知的明细列表;
        /// </summary>
        /// <param name="drugMessage">摆药通知</param>
        /// <returns>成功返回摆药通知信息 失败返回null</returns>
        public ArrayList QueryDrugMessageList(DrugMessage drugMessage)
        {

            string strSQL = "";    //获得某一摆药通知的明细列表的SQL语句

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugMessageList.ByDrugMessage", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugMessageList.ByDrugMessage字段!";
                return null;
            }
            try
            {
                string[] strParm ={
									 drugMessage.StockDept.ID, 
									 drugMessage.DrugBillClass.ID, 
									 drugMessage.SendType.ToString()
								 };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByDrugMessage";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

        /// <summary>
        /// 成功返回摆药通知信息
        /// </summary>
        /// <param name="drugControlID">摆药台编码</param>
        /// <param name="drugMessage">摆药通知</param>
        /// <returns>成功返回摆药通知信息 失败返回null</returns>
        public ArrayList QueryDrugBillList(string drugControlID, DrugMessage drugMessage)
        {
            string strSQL = "";
            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugBillList.ByDept", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugBillList.ByDept字段!";
                return null;
            }
            try
            {
                string[] strParm ={
									 drugControlID,
									 drugMessage.ApplyDept.ID, 
									 drugMessage.StockDept.ID, 
									 drugMessage.SendType.ToString()
								 };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugBillList.ByDept";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

        /// <summary>
        /// 更新摆药通知状态
        /// </summary>
        /// <param name="drugDeptCode">发药药房</param>
        /// <param name="deptCode">申请科室</param>
        /// <param name="billClassCode">摆药单类别</param>
        /// <param name="sendType">发送类型 1 集中 2 临时</param>
        /// <param name="state">通知状态 0 通知 1 已摆</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int UpdateDrugMessage(string drugDeptCode, string deptCode, string billClassCode, int sendType, string state)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugMessageState", ref strSql) == -1) return -1;
            try
            {
                strSql = string.Format(strSql, drugDeptCode, deptCode, billClassCode, sendType.ToString(), state);       //
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确！" + "Pharmacy.DrugStore.UpdateDrugMessageState"; ;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region 基础增、删、改操作

        /// <summary>
        /// 向摆药台表中插入一条记录
        /// </summary>
        /// <param name="drugMessage">摆药台实体</param>
        /// <returns>1成功，-1失败</returns>
        public int InsertDrugMessage(DrugMessage drugMessage)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugMessage", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = myGetParmDrugMessage(drugMessage);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + "Pharmacy.DrugStore.InsertDrugMessage";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 向摆药通知表中插入一条记录
        /// </summary>
        /// <param name="drugMessage">摆药通知实体</param>
        /// <returns>1成功，-1失败</returns>
        public int UpdateDrugMessage(DrugMessage drugMessage)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugMessage", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = myGetParmDrugMessage(drugMessage);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确！" + "Pharmacy.DrugStore.UpdateDrugMessage"; ;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除一条摆药通知
        /// </summary>
        /// <param name="ID">摆药通知流水号</param>
        /// <returns>0没有更新的数据，1成功，-1失败</returns>
        public int DeleteDrugMessage(string ID)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugMessage", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.DeleteDrugMessage";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 设置摆药通知
        /// 先执行更新操作，如果数据库中没有记录则执行插入操作
        /// </summary>
        /// <param name="drugMessage">摆药通知</param>
        /// <returns></returns>
        public int SetDrugMessage(DrugMessage drugMessage)
        {
            //先执行更新操作
            int parm = UpdateDrugMessage(drugMessage);
            if (parm == 0)
            {
                //如果数据库中没有记录则执行插入操作
                parm = InsertDrugMessage(drugMessage);
            }
            return parm;
        }

        /// <summary>
        /// 获得update或者insert摆药通知传入参数数组
        /// </summary>
        /// <param name="drugMessage">摆药通知</param>
        /// <returns>成功返回字符串参数数组  失败返回null</returns>
        private string[] myGetParmDrugMessage(DrugMessage drugMessage)
        {
            string[] strParm ={
								 drugMessage.ApplyDept.ID,         //科室或者病区编码
								 drugMessage.ApplyDept.Name,       //科室或者病区编码
								 drugMessage.ID,    //摆药单分类代码
								 drugMessage.Name,  //摆药单分类名称
								 drugMessage.SendType.ToString(), //发送类型0全部,1-集中,2-临时
								 drugMessage.SendFlag.ToString(), //状态0-通知,1-已摆
								 drugMessage.StockDept.ID,		  //科室编码					 
								 this.Operator.ID,                //操作员编码				 
								 this.Operator.Name,              //操作员姓名
			};

            return strParm;
        }

        /// <summary>
        /// 根据SQL语句，取摆药通知数组
        /// </summary>
        /// <param name="strSQL">查询SQL语句</param>
        /// <returns>成功返回摆药通知数组 失败返回null</returns>
        private ArrayList myGetDrugMessage(string strSQL)
        {
            ArrayList al = new ArrayList();

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取摆药通知列表时出错：" + this.Err;
                return null;
            }
            try
            {
                DrugMessage info;   //摆药通知实体		
                while (this.Reader.Read())
                {
                    info = new DrugMessage();
                    try
                    {
                        info.ApplyDept.ID = this.Reader[0].ToString();          //发送科室编码
                        info.ApplyDept.Name = this.Reader[1].ToString();          //发送科室名称
                        info.DrugBillClass.ID = this.Reader[2].ToString();          //摆药单分类编码
                        info.DrugBillClass.Name = this.Reader[3].ToString();          //摆药单分类名称
                        info.SendType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());      //摆药类型1-集中摆药2-临时摆药
                        info.SendTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString()); //通知时间
                        info.SendFlag = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6].ToString());      //发送状态0－通知，1－已摆药
                        info.StockDept.ID = this.Reader[7].ToString();                 //发药科室编码				 
                        info.ID = this.Reader[8].ToString();                 //操作员编码				 
                        info.Name = this.Reader[9].ToString();                 //操作员姓名
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得摆药通知信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }
                return al;

            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得摆药通知信息时，执行SQL语句出错！myGetDrugBillClass" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
        }

        #endregion

        #endregion 摆药通知

        #region 摆药出库申请

        /// <summary>
        /// 取某一科室申请，某一目标本科室未核准的某一摆药单分类中的申请列表
        /// 例如，某一药房查看某一科室（病区）中某一张摆药单分类中的待摆药信息	
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="objectDeptCode">申请科室</param>
        /// <param name="drugBillClass">摆药单分类编码</param>
        /// <returns>成功返回摆药单内申请 失败返回null</returns>
        public ArrayList QueryDrugList(string deptCode, string objectDeptCode, string drugBillClass)
        {
            string strSelect = "";  //获得摆药信息的SELECT语句
            string strWhere = "";  //获得某一科室摆药信息信息的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugListByClass", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugListByClass字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugListByClass.Where", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugListByClass.Where字段!";
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetDrugList(strSelect + " " + strWhere);
        }

        /// <summary>
        /// 取药品基本信息列表，可能是一条或者多条药品记录
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>摆药单对象数组</returns>
        private ArrayList myGetDrugList(string SQLString)
        {
            ArrayList al = new ArrayList();            //用于返回药品信息的数组
            FS.HISFC.Models.Pharmacy.DrugControl DrugList;   //返回数组中的摆药信息类，每次在循环中创建实例。

            this.ExecQuery(SQLString);
            try
            {
                while (this.Reader.Read())
                {
                    DrugList = new FS.HISFC.Models.Pharmacy.DrugControl();
                    #region "接口说明"

                    #endregion

                    try
                    {
                        DrugList.ID = this.Reader[0].ToString();


                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得摆药信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(DrugList);
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

        #endregion

        #region 门诊发药窗、摆药台维护

        #region 基础增、删、改操作

        /// <summary>
        /// 获得Update或Insert门诊终端维护的传入参数数组
        /// </summary>
        /// <param name="drugTerminal">门诊终端实体</param>
        /// <returns>成功返回字符串数组、失败返回null</returns>
        protected string[] myGetParmDrugTerminal(DrugTerminal drugTerminal)
        {
            //操作时间在sql内通过sysdate取得
            string[] strParm = {
								   drugTerminal.ID,							              //0 终端编号
								   drugTerminal.Name,							          //1 终端名称
								   drugTerminal.Dept.ID,								  //2 所属库房编号
								   drugTerminal.TerminalType.GetHashCode().ToString(),	  //3 终端类别 0 发药窗口 1 配药台
								   drugTerminal.ReplaceTerminal.ID,						  //4 替代终端号
								   NConvert.ToInt32(drugTerminal.IsClose).ToString(),	  //5 是否关闭 0 开启 1 关闭
								   NConvert.ToInt32(drugTerminal.IsAutoPrint).ToString(), //6 是否自动打印 0 否 1 自动打印
								   drugTerminal.RefreshInterval1.ToString(),			  //7 程序刷新时间间隔
								   drugTerminal.RefreshInterval2.ToString(),			  //8 打印/显示 时间间隔
								   drugTerminal.TerminalProperty.GetHashCode().ToString(),						  //9 终端性质 0 普通 1 专科 2 特殊
								   drugTerminal.AlertQty.ToString(),					  //10 警戒线
								   drugTerminal.ShowQty.ToString(),						  //11 显示人数
								   drugTerminal.SendWindow.ID,								  //12 发药窗口编号
								   this.Operator.ID,									  //13 操作员
								   drugTerminal.Memo,									  //14 备注
                                   ((int)drugTerminal.TerimalPrintType).ToString()
							   };
            return strParm;
        }

        /// <summary>
        /// 获取门诊终端信息
        /// </summary>
        /// <param name="StrSQL">查询的sql语句</param>
        /// <returns>成功返回门诊终端实体数组、失败返回null</returns>
        protected ArrayList myGetDrugTerminal(string StrSQL)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(StrSQL) == -1)
            {
                this.Err = "获取门诊终端信息时出错" + this.Err;
                return null;
            }
            try
            {
                DrugTerminal info;
                while (this.Reader.Read())
                {
                    info = new DrugTerminal();

                    info.ID = this.Reader[0].ToString();							//0 终端编码
                    info.Name = this.Reader[1].ToString();							//1 终端名称
                    info.Dept.ID = this.Reader[2].ToString();								//2 所属库房编码
                    info.TerminalType = (EnumTerminalType)NConvert.ToInt32(this.Reader[3].ToString());							//3 终端类别
                    info.TerminalProperty = (EnumTerminalProperty)NConvert.ToInt32(this.Reader[4].ToString());						//4 终端性质
                    info.ReplaceTerminal.ID = this.Reader[5].ToString();							//5 替代终端号
                    info.IsClose = NConvert.ToBoolean(this.Reader[6].ToString());			//6 是否关闭
                    info.IsAutoPrint = NConvert.ToBoolean(this.Reader[7].ToString());		//7 是否自动打印
                    info.RefreshInterval1 = NConvert.ToDecimal(this.Reader[8].ToString());	//8 程序刷新时间间隔
                    info.RefreshInterval2 = NConvert.ToDecimal(this.Reader[9].ToString());	//9 打印/显示 刷新时间间隔
                    info.AlertQty = NConvert.ToInt32(this.Reader[10].ToString());			//10 警戒线
                    info.ShowQty = NConvert.ToInt32(this.Reader[11].ToString());			//11 显示人数
                    info.SendWindow.ID = this.Reader[12].ToString();							//12 发药窗口编码
                    info.Oper.ID = this.Reader[13].ToString();								//13 操作员
                    info.Oper.OperTime = NConvert.ToDateTime(this.Reader[14].ToString());		//14 操作时间
                    info.Memo = this.Reader[15].ToString();									//15 备注
                    info.SendQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());
                    info.DrugQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());
                    info.Average = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18].ToString());
                    if (this.Reader.FieldCount > 18)
                    {
                        info.TerimalPrintType = (EnumClinicPrintType)NConvert.ToInt32(this.Reader[19]);
                    }

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得门诊终端信息时，执行SQL语句出错" + ex.Message;
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
        /// 向门诊终端表内插入数据
        /// </summary>
        /// <param name="drugTerminal">门诊终端实体</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int InsertDrugTerminal(DrugTerminal drugTerminal)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugTerminal", ref strSql) == -1) return -1;
            try
            {
                //设置门诊终端号
                drugTerminal.ID = this.GetSequence("Pharmacy.Constant.GetNewCompanyID");
                if (drugTerminal.SendWindow == null || drugTerminal.SendWindow.ID == "")
                    drugTerminal.SendWindow.ID = drugTerminal.ID;
                string[] strParm = this.myGetParmDrugTerminal(drugTerminal);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + "Pharmacy.DrugStore.InsertDrugTerminal";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新门诊终端实体
        /// </summary>
        /// <param name="drugTerminal">门诊终端实体</param>
        /// <returns>成功返回1 失败返回－1 无更新返回0</returns>
        public int UpdateDrugTerminal(DrugTerminal drugTerminal)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugTerminal", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugTerminal(drugTerminal);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确！" + "Pharmacy.DrugStore.UpdateDrugTerminal"; ;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除一条门诊终端数据
        /// </summary>
        /// <param name="terminalCode">终端编号</param>
        /// <returns>无更新返回0 成功返回1 失败返回－1</returns>
        public int DeleteDrugTerminal(string terminalCode)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugTerminal", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, terminalCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.DeleteDrugTerminal";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新门诊终端实体信息，如无数据则插入
        /// </summary>
        /// <param name="drugTerminal">门诊终端实体</param>
        /// <returns>成功返回1，失败返回－1</returns>
        public int SetDrugTerminal(DrugTerminal drugTerminal)
        {
            int parm;
            parm = this.UpdateDrugTerminal(drugTerminal);
            if (parm == 0)
                parm = this.InsertDrugTerminal(drugTerminal);
            return parm;
        }

        #endregion

        #region 内部使用

        /// <summary>
        /// 获得某科室某类型门诊终端列表
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="terminalType">终端类型 0 发药窗 1 配药台</param>
        /// <returns>成功返回DrugTerminal的ArrayList数组，失败返回null</returns>
        public ArrayList QueryDrugTerminalByDeptCode(string deptCode, string terminalType)
        {
            string strSQL = "", strWhere = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugTerminal字段!";
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal.ByDeptCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugTerminal.ByDeptCode字段!";
                return null;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, deptCode, terminalType);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugTerminal.ByDeptCode";
                return null;
            }
            return this.myGetDrugTerminal(strSQL);
        }

        /// <summary>
        /// 获得某科室某类型门诊终端列表 按所属库房排序
        /// </summary>
        /// <param name="terminalType">终端类型 0 发药窗 1 配药台</param>
        /// <returns>成功返回DrugTerminal的ArrayList数组，失败返回null</returns>
        public ArrayList QueryDrugTerminalByTerminalType(string terminalType)
        {
            string strSQL = "", strWhere = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugTerminal字段!";
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal.ByTerminalType", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugTerminal.ByTerminalType字段!";
                return null;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, terminalType);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugTerminal.ByTerminalType";
                return null;
            }
            return this.myGetDrugTerminal(strSQL);
        }

        /// <summary>
        /// 根据终端编号、科室编码获得门诊终端信息
        /// </summary>
        /// <param name="terminalCode">终端编号</param>
        /// <returns>DrugTerminal、失败或没找到返回null</returns>
        public DrugTerminal GetDrugTerminalById(string terminalCode)
        {
            string strSQL = "", strWhere = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugTerminal字段!";
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal.ById", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugTerminal.ById字段!";
                return null;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, terminalCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugTerminal.ById";
                return null;
            }
            ArrayList al = this.myGetDrugTerminal(strSQL);
            if (al == null) return null;

            if (al.Count == 0) return new DrugTerminal();

            return al[0] as DrugTerminal;
        }

        /// <summary>
        /// 根据发药窗口编码获取在用的一个配药台编码
        /// </summary>
        /// <param name="sendWindow">发药窗口编码</param>
        /// <returns>成功返回对应的配药台信息 失败返回null</returns>
        public DrugTerminal GetDrugTerminalBySendWindow(string sendWindow)
        {
            string strSQL = "", strWhere = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugTerminal字段!";
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal.BySendWindow", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugTerminal.BySendWindow字段!";
                return null;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, sendWindow);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugTerminal.BySendWindow";
                return null;
            }
            ArrayList al = this.myGetDrugTerminal(strSQL);
            if (al == null) return null;

            if (al.Count == 0) return new DrugTerminal();

            return al[0] as DrugTerminal;
        }

        /// <summary>
        /// 根据终端编码配药台信息 如果该配药台关闭 则循环查找替代终端信息
        /// </summary>
        /// <param name="terminalCode">配药终端编码</param>
        /// <returns>成功返回未关闭的配药终端 失败返回null 无满足条件的配药终端返回空实体</returns>
        public DrugTerminal GetDrugTerminal(string terminalCode)
        {
            FS.HISFC.Models.Pharmacy.DrugTerminal info = null;
            info = this.GetDrugTerminalById(terminalCode);
            if (info == null)
                return null;
            if (info.ID != "")
            {
                while (info.IsClose)
                {
                    if (info.ReplaceTerminal.ID == null || info.ReplaceTerminal.ID == "")
                    {
                        info = new DrugTerminal();
                        break;
                    }
                    info = this.GetDrugTerminalById(info.ReplaceTerminal.ID);
                    if (info == null || info.ID == "")
                        break;
                    //防止循环查找
                    if (info.ID == terminalCode)
                    {
                        if (info.IsClose)
                            info = new DrugTerminal();
                        break;
                    }
                }
            }
            return info;
        }

        /// <summary>
        /// 判断该终端是否为其他终端的替代终端
        /// </summary>
        /// <param name="terminalCode">终端编码</param>
        /// <returns>如为替代终端返回1 否则返回0 出错返回-1</returns>
        public int IsReplaceFlag(string terminalCode)
        {
            string strSQL = "", strWhere = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugTerminal字段!";
                return -1;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal.IsReplaceFlag", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugTerminal.IsReplaceFlag字段!";
                return -1;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, terminalCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.IsReplaceFlag";
                return -1;
            }
            ArrayList al = this.myGetDrugTerminal(strSQL);

            if (al == null) return -1;

            if (al.Count == 0)		//不是其他终端的替代终端
                return 0;
            else					//为其他终端的替代终端
                return 1;
        }

        /// <summary>
        /// 对处方调剂后 更新已发送、待配药、均分次数信息
        /// </summary>
        /// <param name="terminalCode">终端编码</param>
        /// <param name="sendNum">当日已发送处方品种数</param>
        /// <param name="drugNum">当日待配药处方品种数</param>
        /// <param name="averageNum">当日均分次数</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int UpdateTerminalAdjustInfo(string terminalCode, decimal sendNum, decimal drugNum, decimal averageNum)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateTerminalAdjustInfo", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, terminalCode, sendNum.ToString(), drugNum.ToString(), averageNum.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据处方号 更新已发送、待配药信息 作废调剂信息时调用
        /// </summary>
        /// <param name="recipeNo">处方号</param>
        /// <param name="sendNum">当日已发送处方品种数</param>
        /// <param name="drugNum">当然待配药处方品种数</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int UpdateTerminalAdjustInfo(string recipeNo, decimal sendNum, decimal drugNum)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateTerminalAdjustInfo.1", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, recipeNo, sendNum.ToString(), drugNum.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新一类终端 是否关闭 状态
        /// </summary>
        /// <param name="terminalType">终端类别 0 发药窗口 1 配药台</param>
        /// <param name="closeFlag">关闭状态 0 开放 1 关闭</param>
        /// <returns>成功返回受影响行数 失败返回null</returns>
        public int UpdateTerminalCloseFlag(string terminalType, string closeFlag)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateTerminalCloseFlag", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, terminalType, closeFlag);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新一类终端 是否关闭 状态
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="terminalType">终端类别</param>
        /// <param name="closeFlag">关闭状态</param>
        /// <returns>成功返回受影响行数 失败返回null</returns>
        public int UpdateTerminalCloseFlag(string deptCode, string terminalType, string closeFlag)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDeptTerminalCloseFlag", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, deptCode, terminalType, closeFlag);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据类别 寻找满足条件的配药终端
        /// </summary>
        /// <param name="deptCode">药房编码</param>
        /// <param name="type">类别 1 已发送的处方品种数最少的配药台 2 待配药的处方品种数最少的配药台</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public FS.HISFC.Models.Pharmacy.DrugTerminal TerminalStatInfo(string deptCode, string type)
        {
            string strSQL = "", strWhere = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugTerminal", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugTerminal字段!";
                return null;
            }

            if (this.GetSQL("Pharmacy.DrugStore.TerminalStatInfo" + "." + type, ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.TerminalStatInfo" + "." + type + "字段!";
                return null;
            }
            try
            {
                strSQL = strSQL + strWhere;
                strSQL = string.Format(strSQL, deptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.TerminalStatInfo";
                return null;
            }
            ArrayList al = this.myGetDrugTerminal(strSQL);
            if (al == null) return null;

            if (al.Count == 0) return new DrugTerminal();

            return al[0] as DrugTerminal;
        }

        /// <summary>
        /// 检查该终端是否仍存在未执行的数据
        /// </summary>
        /// <param name="terminalNO">终端编码</param>
        /// <returns></returns>
        public bool IsHaveRecipe(string terminalNO)
        {
            string strSQL = "", strWhere = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.IsHaveRecipe", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.IsHaveRecipe字段!";
                return false;
            }
            try
            {
                strSQL = string.Format(strSQL, terminalNO);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.IsHaveRecipe";
                return false;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "检查该终端是否仍存在未执行的数据" + this.Err;
                return false;
            }

            if (this.Reader.Read())
            {
                this.Reader.Close();

                return true;
            }

            this.Reader.Close();

            return false;
        }

        #endregion

        #endregion

        #region 门诊特殊配药台维护

        #region 基础增、删、改操作

        /// <summary>
        /// 获得update或insert传入的参数数组
        /// </summary>
        /// <param name="drugSPETerminal">门诊特殊配药台信息实体</param>
        /// <returns>成功返回字符串数组、失败返回null</returns>
        protected string[] myGetParmDrugSPETerminal(DrugSPETerminal drugSPETerminal)
        {
            //操作时间通过sql内sysdate取得
            string[] strParm = {
								   drugSPETerminal.Terminal.ID,		//0 终端编号(配药台编号)
								   drugSPETerminal.ItemType,					//1 项目类别 1 药品 2 专科 3 结算类别 4 特定收费窗口
								   drugSPETerminal.Item.ID,					//2 项目编码
								   drugSPETerminal.Item.Name,					//3 项目名称
								   this.Operator.ID,							//4 操作员
								   drugSPETerminal.Memo,						//5 备注
			};
            return strParm;
        }

        /// <summary>
        /// 获得门诊特殊配药台信息
        /// </summary>
        /// <param name="StrSQL">执行的SQL语句</param>
        /// <returns>成功返回数组、失败返回null</returns>
        protected ArrayList myGetDrugSPETerminal(string StrSQL)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(StrSQL) == -1)
            {
                this.Err = "获取门诊特殊配药台信息时出错" + this.Err;
                return null;
            }
            try
            {
                DrugSPETerminal info;
                while (this.Reader.Read())
                {
                    info = new DrugSPETerminal();

                    info.Terminal.ID = this.Reader[0].ToString();
                    info.ItemType = this.Reader[1].ToString();									//1 项目类别
                    info.Item.ID = this.Reader[2].ToString();									//2 项目编码
                    info.Item.Name = this.Reader[3].ToString();									//3 项目名称
                    info.Oper.ID = this.Reader[4].ToString();									//4 操作员
                    info.Oper.OperTime = NConvert.ToDateTime(this.Reader[5].ToString());				//5 操作时间
                    info.Memo = this.Reader[6].ToString();										//6 备注

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得门诊特殊配药台信息时，执行SQL语句出错" + ex.Message;
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
        /// 向门诊特殊配药台插入数据
        /// </summary>
        /// <param name="drugSPETerminal">门诊特殊配药台实体</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int InsertDrugSPETerminal(DrugSPETerminal drugSPETerminal)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugSPETerminal", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugSPETerminal(drugSPETerminal);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + "Pharmacy.DrugStore.InsertDrugSPETerminal";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新门诊特殊配药台数据
        /// </summary>
        /// <param name="drugSPETerminal">门诊特殊配药台实体</param>
        /// <returns>成功返回1 失败返回－1 无更新返回0</returns>
        public int UpdateDrugSPETerminal(DrugSPETerminal drugSPETerminal)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugSPETerminal", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugSPETerminal(drugSPETerminal);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确！" + "Pharmacy.DrugStore.UpdateDrugSPETerminal"; ;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除一条门诊特殊配药台信息
        /// </summary>
        /// <param name="speInfo">特殊配药台实体</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int DeleteDrugSPETerminal(FS.HISFC.Models.Pharmacy.DrugSPETerminal speInfo)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugSPETerminal", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, speInfo.Terminal.ID, speInfo.ItemType, speInfo.Item.ID);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.DeleteDrugSPETerminal";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        #endregion

        #region 内部使用

        /// <summary>
        /// 删除指定药房、指定类型的特殊配药终端信息
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <param name="itemType">终端类型</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int DeleteDrugSPETerminal(string deptCode, string itemType)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugSPETerminal.DeptItemType", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, itemType, deptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.Item.DeleteDrugSPETerminal.DeptItemType";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除某类别特殊配药台信息
        /// </summary>
        /// <param name="itemType">项目类别 1 药品类别 2 专科类别 3 结算类别 4 收费窗口</param>
        /// <returns>成功返回删除数据数目 失败返回－1 无操作返回0</returns>
        public int DeleteDrugSPETerminal(string itemType)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugSPETerminal.ItemType", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, itemType);
            }
            catch (Exception ex)
            {
                this.Err = "格式化SQL语句传入参数出错!" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 某类型门诊特殊配药台列表
        /// </summary>
        /// <param name="itemType">类别 1 药品 2 专科 3 结算类别 4 特定收费窗口 "A"所有 </param>
        /// <returns>成功返回DrugSPETerminal的ArrayList数组，失败返回null</returns>
        public ArrayList QueryDrugSPETerminalByType(string itemType)
        {
            string strSQL = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugSPETerminal.ByType", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugSPETerminal.ByType字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, itemType);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugSPETerminal.ByType";
                return null;
            }
            return this.myGetDrugSPETerminal(strSQL);
        }

        /// <summary>
        /// 某科室、某类型门诊特殊配药台列表 类型为"A"代表所有类别
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="itemType">类别  1 药品 2 专科 3 结算类别 4 特定收费窗口 "A"所有</param>
        /// <returns>成功返回DrugSPETerminal的ArrayList数组、失败返回null</returns>
        public ArrayList QueryDrugSPETerminalByDeptCode(string deptCode, string itemType)
        {
            ArrayList al = this.QueryDrugSPETerminalByType(itemType);
            ArrayList myAl = new ArrayList();
            DrugSPETerminal info;
            for (int i = 0; i < al.Count; i++)
            {
                info = al[i] as DrugSPETerminal;
                info.Terminal = this.GetDrugTerminalById(info.Terminal.ID);
                if (info.Terminal == null) return null;

                if (info.Terminal.Dept.ID == deptCode)
                    myAl.Add(info);
            }
            return myAl;
        }

        /// <summary>
        /// 根据终端编号获得门诊特殊配药台信息
        /// </summary>
        /// <param name="terminalCode">终端编号</param>
        /// <returns>DrugSPETerminal、失败返回null</returns>
        public DrugSPETerminal GetDrugSPETerminalById(string terminalCode)
        {
            string strSQL = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugSPETerminal.ById", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugSPETerminal.ById字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, terminalCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugSPETerminal.ById";
                return null;
            }
            ArrayList al = this.myGetDrugSPETerminal(strSQL);
            if (al == null) return null;

            if (al.Count == 0) return null;

            return al[0] as DrugSPETerminal;
        }

        /// <summary>
        /// 处方调剂过程中调用
        /// 根据特殊项目编码获取特殊配药终端信息 返回优先级别最高的配药终端
        /// sql语句使用in条件语句
        /// </summary>
        /// <param name="adjustType">调剂方式 0 平均 1 竞争</param>
        /// <param name="itemCode">特殊项目编码</param>
        /// <returns>成功返回特殊项目实体 失败返回null 无记录返回空实体</returns>
        public FS.HISFC.Models.Pharmacy.DrugSPETerminal GetDrugSPETerminalByItemCode(string adjustType, string deptCode, params string[] itemCode)
        {
            string strSQL = "";
            //SQL语句内通过In实现
            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugSPETerminal.ByItemCode" + "." + adjustType, ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugSPETerminal.ByItemCode." + adjustType + "字段!";
                return null;
            }
            try
            {
                string strParm = "";
                foreach (string str in itemCode)
                {
                    if (strParm == "")
                        strParm = "'" + str + "'";
                    else
                        strParm = strParm + "," + "'" + str + "'";
                }
                strSQL = string.Format(strSQL, deptCode, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugSPETerminal.ByItemCode";
                return null;
            }
            ArrayList al = this.myGetDrugSPETerminal(strSQL);
            if (al == null) return null;
            if (al.Count == 0) return new FS.HISFC.Models.Pharmacy.DrugSPETerminal();
            return al[0] as DrugSPETerminal;
        }

        /// <summary>
        /// 更新门诊特殊配药台实体信息，如无数据则插入
        /// </summary>
        /// <param name="drugSPETerminal">门诊特殊配药台实体</param>
        /// <returns>成功返回1，失败返回－1</returns>
        public int SetDrugSPETerminal(DrugSPETerminal drugSPETerminal)
        {
            int parm;
            parm = this.UpdateDrugSPETerminal(drugSPETerminal);
            if (parm == 0)
                parm = this.InsertDrugSPETerminal(drugSPETerminal);
            return parm;
        }

        #endregion

        #endregion

        #region 门诊配药台(发药窗口)模板维护

        #region 基础增、删、改操作

        /// <summary>
        /// 获得Update或Insert传入参数数组
        /// </summary>
        /// <param name="obj">模版neuObject实体</param>
        /// <returns>成功返回字符串数组 失败返回null</returns>
        protected string[] myGetParmDrugOpenTerminal(FS.FrameWork.Models.NeuObject obj)
        {
            string[] strParm = {
								   obj.ID,						//模板编码
								   obj.Name,					//模板名称
								   obj.User01,					//配药台编码
								   obj.User02,					//是否关闭 0 开放 1 关闭
								   obj.User03,					//所属库房编码
								   obj.Memo					//备注
							   };
            return strParm;
        }

        /// <summary>
        /// 获得门诊模板信息
        /// </summary>
        /// <param name="StrSQL">查询sql字符串</param>
        /// <returns>成功返回neuobject数组 失败返回null</returns>
        protected ArrayList myGetDrugOpenTerminal(string StrSQL)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(StrSQL) == -1)
            {
                this.Err = "获取门诊模板信息时出错" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject info;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[1].ToString();		//模板编码
                    info.Name = this.Reader[2].ToString();		//模板名称
                    info.User01 = this.Reader[3].ToString();	//配药台编码
                    info.User02 = this.Reader[4].ToString();	//是否关闭 0 开发 1 关闭
                    info.User03 = this.Reader[0].ToString();	//所属库房编码
                    info.Memo = this.Reader[5].ToString();		//备注

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得门诊模板信息时，执行SQL语句出错" + ex.Message;
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
        /// 插入一条数据进入门诊模板
        /// </summary>
        /// <param name="info">neuobject实体</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int InsertDrugOpenTerminal(FS.FrameWork.Models.NeuObject info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugOpenTerminal", ref strSql) == -1) return -1;
            try
            {
                //				if (info.ID == null || info.ID == "")				//获取模板编号
                //                    info.ID = this.GetSequence("Pharmacy.Constant.GetNewCompanyID");
                string[] strParm = this.myGetParmDrugOpenTerminal(info);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + "Pharmacy.DrugStore.InsertDrugOpenTerminal";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据库房编码 模板编号、终端编号 更新一条门诊配药台（发药窗）模板数据
        /// </summary>
        /// <param name="info">neuobject实体</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int UpdateDrugOpenTerminal(FS.FrameWork.Models.NeuObject info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugOpenTerminal", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugOpenTerminal(info);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确！" + "Pharmacy.DrugStore.UpdateDrugOpenTerminal"; ;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据模板编号、终端类型删除模板信息
        /// </summary>
        /// <param name="templateCode">模板编号</param>
        /// <param name="terminalType">终端类型 (0 发药窗口 1 配药台) "A"所有类型</param>
        /// <returns>成功返回删除条数 失败返回－1</returns>
        public int DeleteDrugOpenTerminalByType(string templateCode, string terminalType)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugOpenTerminalByType", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, templateCode, terminalType);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.DrugStore.DeleteDrugOpenTerminalByType";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据模板编号删除所有该模板数据
        /// </summary>
        /// <param name="templateCode">模板编号</param>
        /// <returns>成功返回删除条数 失败返回－1</returns>
        public int DeleteDrugOpenTerminalByTemplateCode(string templateCode)
        {
            return this.DeleteDrugOpenTerminalByType(templateCode, "A");
        }

        /// <summary>
        /// 根据模板编号、终端编号删除一条模板信息
        /// </summary>
        /// <param name="templateCode">模板编号</param>
        /// <param name="terminalCode">终端编号</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int DeleteDrugOpenTerminalById(string templateCode, string terminalCode)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.DeleteDrugOpenTerminalById", ref strSql) == -1) return -1;

            try
            {
                strSql = string.Format(strSql, templateCode, terminalCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = "传入参数不正确！Pharmacy.DrugStore.DeleteDrugOpenTerminalById";
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据终端编号删除在模板表内所有信息
        /// </summary>
        /// <param name="terminalCode">终端编号</param>
        /// <returns>成功返回删除数、失败返回null</returns>
        public int DeleteDrugOpenTerminalById(string terminalCode)
        {
            return this.DeleteDrugOpenTerminalById("AAAA", terminalCode);
        }

        #endregion

        #region 内部使用

        /// <summary>
        /// 根据科室编号获取该科室模板列表
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <returns>成功返回neuobject数组(ID 模板编号 Name 模板名称)、失败返回null</returns>
        public ArrayList QueryDrugOpenTerminalByDeptCode(string deptCode)
        {
            string strSQL = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugOpenTerminalByDeptCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugOpenTerminalByDeptCode字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugOpenTerminalByDeptCode";
                return null;
            }
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "获取门诊模板信息时出错" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject info;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[1].ToString();		//模板编码
                    info.Name = this.Reader[2].ToString();		//模板名称
                    info.User03 = this.Reader[0].ToString();	//所属库房编码

                    al.Add(info);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得门诊模板信息时，执行SQL语句出错" + ex.Message;
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
        /// 根据模板编号获取模板详细信息
        /// </summary>
        /// <param name="templateCode">模板编号</param>
        /// <returns>成功返回数组 失败返回null</returns>
        public ArrayList QueryDrugOpenTerminalById(string templateCode)
        {
            string strSQL = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.GetDrugOpenTerminalById", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.GetDrugOpenTerminal.ById字段!";
                return null;
            }
            try
            {
                strSQL = string.Format(strSQL, templateCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.GetDrugOpenTerminalById";
                return null;
            }
            return this.myGetDrugOpenTerminal(strSQL);
        }

        /// <summary>
        /// 更新门诊模板信息、如无数据则插入一条新数据
        /// </summary>
        /// <param name="info">neuobject实体</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int SetDrugOpenTerminal(FS.FrameWork.Models.NeuObject info)
        {
            int parm;
            parm = this.UpdateDrugOpenTerminal(info);
            if (parm == 0)
                parm = this.InsertDrugOpenTerminal(info);
            return parm;
        }

        /// <summary>
        /// 执行选定模板
        /// </summary>
        /// <param name="templateCode">模板编号</param>
        /// <returns>成功返回更新数量 失败返回-1</returns>
        public int ExecOpenTerminal(string deptCode, string templateCode)
        {
            if (this.UpdateTerminalCloseFlag(deptCode, "1", "1") == -1)
            {
                this.Err = "执行关闭全部配药台失败" + this.Err;
                return -1;
            }

            string strSQL = "";

            //取SQL语句
            if (this.GetSQL("Pharmacy.DrugStore.ExecOpenTerminal", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.DrugStore.ExecOpenTerminal字段!";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, templateCode);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "Pharmacy.DrugStore.ExecOpenTerminal";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #endregion

        #region 门诊摆药处方(处方调剂)

        #region 基础增、删、改操作

        /// <summary>
        /// 获取Update或Insert数组传入参数数组 
        /// </summary>
        /// <param name="info">门诊摆药处方实体</param>
        /// <returns>成功返回string参数数组 失败返回null</returns>
        protected string[] myGetParmDrugRecipe(FS.HISFC.Models.Pharmacy.DrugRecipe info)
        {
            string[] strParm = {
								   info.StockDept.ID,							//药房编码(发药药房)
								   info.RecipeNO,								//处方号
								   info.SystemType,								//出库申请分类
								   info.TransType,								//交易类型 1 正交易 2 反交易								
								   info.RecipeState,							//处方状态
								   info.ClinicNO,								//门诊号
								   info.CardNO,									//病历号
								   info.PatientName,							//患者姓名
								   info.Sex.ID.ToString(),						//性别
								   info.Age.ToString(),							//年龄
								   info.PayKind.ID,								//结算类别
								   info.PatientDept.ID,							//患者科室
								   info.RegTime.ToString(),						//挂号日期
								   info.Doct.ID,								//开发医生
								   info.DoctDept.ID,							//开方医生科室
								   info.DrugTerminal.ID,						//配药终端
								   info.SendTerminal.ID,						//发药终端
								   info.FeeOper.ID,								//收费人
								   info.FeeOper.OperTime.ToString(),			//收费时间
								   info.InvoiceNO,								//票据号
								   info.Cost.ToString(),						//处方金额
								   info.RecipeQty.ToString(),					//处方内药品品种数
								   info.DrugedQty.ToString(),					//已配药药品品种数
								   info.DrugedOper.ID,							//配药人
								   info.DrugedOper.Dept.ID,							//配药科室
								   info.DrugedOper.OperTime.ToString(),			//配药日期
								   info.SendOper.ID,							//发药人
								   info.SendOper.OperTime.ToString(),			//发药日期
								   info.SendOper.Dept.ID,							//发药科室

								   ((int)info.ValidState).ToString(),		    //有效状态 1 有效 0 无效 2 发药后退费
								   NConvert.ToInt32(info.IsModify).ToString(),	//退/改药状态 0 否 1 是

								   info.BackOper.ID,							//还药人
								   info.BackOper.OperTime.ToString(),			//还药时间
								   info.CancelOper.ID,							//取消人
								   info.CancelOper.OperTime.ToString(),			//取消时间
								   info.Memo,									//备注
                                   info.SumDays.ToString()						//处方内药品剂数合计
							   };
            return strParm;
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
                    info = new DrugRecipe();

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

        /// <summary>
        /// 向门诊摆药处方(处方调剂)内加入一条数据
        /// </summary>
        /// <param name="info">门诊摆药处方(处方调剂)实体</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int InsertDrugRecipeInfo(FS.HISFC.Models.Pharmacy.DrugRecipe info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugRecipeInfo", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugRecipe(info);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + "Pharmacy.DrugStore.InsertDrugRecipeInfo";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 向 门诊摆药处方(处方调剂) 更新数据
        /// </summary>
        /// <param name="info">门诊摆药处方(处方调剂)实体</param>
        /// <returns>成功返回1 失败返回－1 无更新返回0</returns>
        public int UpdateDrugRecipeInfo(FS.HISFC.Models.Pharmacy.DrugRecipe info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo", ref strSql) == -1) return -1;
            try
            {
                string[] strParm = this.myGetParmDrugRecipe(info);  //取参数列表
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + "Pharmacy.DrugStore.UpdateDrugRecipeInfo";
                this.ErrCode = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        ///  先进行数据删除在进行插入操作
        /// </summary>
        /// <param name="info">门诊摆药处方(处方调剂)实体</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int SetDrugTerminalOper(FS.HISFC.Models.Pharmacy.DrugRecipe info)
        {
            int parm;
            parm = this.UpdateDrugRecipeInfo(info);
            if (parm == 0)
                parm = this.InsertDrugRecipeInfo(info);
            return parm;
        }

        #endregion

        #region 内部使用

        /// <summary>
        /// 更新记录有效/无效状态
        /// </summary>
        /// <param name="recipeNo">处方号</param>
        /// <param name="class3MenaingCode">出库申请类别 M1/M2/AA</param>
        /// <param name="validState">状态 0 有效 1 无效</param>
        /// <returns>成功返回1 失败返回－1 无操作返回0</returns>
        public int UpdateDrugRecipeValidState(string recipeNo, string class3MenaingCode, FS.HISFC.Models.Base.EnumValidState validState)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeValidState", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, recipeNo, class3MenaingCode, ((int)validState).ToString(), this.Operator.ID);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新记录状态
        /// </summary>
        /// <param name="deptCode">药房编码</param>
        /// <param name="recipeNo">处方号</param>
        /// <param name="class3MeaningCode">出库申请类别 M1/M2</param>
        /// <param name="oldState">原状态</param>
        /// <param name="newState">新状态</param>		
        /// <returns>成功返回1 失败返回-1 无操作返回0</returns>
        public int UpdateDrugRecipeState(string deptCode, string recipeNo, string class3MeaningCode, string oldState, string newState)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeState", ref strSql) == -1)
                return -1;
            try
            {
                strSql = string.Format(strSql, deptCode, recipeNo, class3MeaningCode, oldState, newState);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新配药信息 根据本次已配药数量改变处方状态 不更新配药终端 
        /// </summary>
        /// <param name="drugDept">库房编码</param>
        /// <param name="recipeNo">处方号</param>
        /// <param name="class3MeaningCode">出库分类</param>
        /// <param name="drugOper">配药人</param>
        /// <param name="drugedDept">配药科室</param>
        /// <param name="drugedNum">本次已配药数量</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int UpdateDrugRecipeDrugedInfo(string drugDept, string recipeNo, string class3MeaningCode, string drugOper, string drugedDept, decimal drugedNum)
        {
            #region 原Sql语句
            /*
			 UPDATE PHA_STO_RECIPE T
				   SET T.Druged_Oper = '{3}',
					   T.DRUGED_DEPT = '{4}',
					   T.DRUGED_DATE = sysdate,
					   T.DRUGED_QTY = {5},
					   T.RECIPE_STATE = DECODE(T.RECIPE_QTY - T.DRUGED_QTY - 1,0,'2','1')
				WHERE  T.PARENT_CODE = '000010'
				  AND  T.CURRENT_CODE = '004004'
				  AND  T.RECIPE_STATE = '1'
				  AND  T.CLASS3_MEANING_CODE = '{2}'
				  AND  T.DRUG_DEPT_CODE = '{0}'
				  AND  T.RECIPE_NO = '{1}'
			现更改为 暂不考虑配药部分确认 因为存在未知问题 导致 状态更新不对
			  UPDATE PHA_STO_RECIPE T
				   SET T.Druged_Oper = '{3}',
					   T.DRUGED_DEPT = '{4}',
					   T.DRUGED_DATE = sysdate,
					   T.DRUGED_QTY = {5},
					   T.RECIPE_STATE = '2'
				WHERE  T.PARENT_CODE = '000010'
				  AND  T.CURRENT_CODE = '004004'
				  AND  T.RECIPE_STATE = '1'
				  AND  T.CLASS3_MEANING_CODE = '{2}'
				  AND  T.DRUG_DEPT_CODE = '{0}'
				  AND  T.RECIPE_NO = '{1}'
			*/
            #endregion
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Druged", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm = {	
									   drugDept,							//药房
									   recipeNo,							//处方号
									   class3MeaningCode,					//出库分类
									   drugOper,							//配药人
									   drugedDept,							//配药科室
									   drugedNum.ToString(),				//本次已配药数量
				};
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新配药信息 根据本次已配药数量改变处方状态 更新配药终端
        /// </summary>
        /// <param name="drugDept">库房编码</param>
        /// <param name="recipeNo">处方号</param>
        /// <param name="class3MeaningCode">出库分类</param>
        /// <param name="drugOper">配药人</param>
        /// <param name="drugedDept">配药科室</param>
        /// <param name="drugedNum">本次已配药数量</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int UpdateDrugRecipeDrugedInfo(string drugDept, string recipeNo, string class3MeaningCode, string drugOper, string drugedDept, string drugedTerminal, decimal drugedNum)
        {
            #region 原Sql语句
            /*
			 UPDATE PHA_STO_RECIPE T
				   SET T.Druged_Oper = '{3}',
					   T.DRUGED_DEPT = '{4}',
					   T.DRUGED_DATE = sysdate,
					   T.DRUGED_QTY = {5},
					   T.RECIPE_STATE = DECODE(T.RECIPE_QTY - T.DRUGED_QTY - 1,0,'2','1')
				WHERE  T.PARENT_CODE = '000010'
				  AND  T.CURRENT_CODE = '004004'
				  AND  T.RECIPE_STATE = '1'
				  AND  T.CLASS3_MEANING_CODE = '{2}'
				  AND  T.DRUG_DEPT_CODE = '{0}'
				  AND  T.RECIPE_NO = '{1}'
			现更改为 暂不考虑配药部分确认 因为存在未知问题 导致 状态更新不对
			  UPDATE PHA_STO_RECIPE T
				   SET T.Druged_Oper = '{3}',
					   T.DRUGED_DEPT = '{4}',
					   T.DRUGED_DATE = sysdate,
                       T.DRUGED_TERMINAL = '{5}',
					   T.DRUGED_QTY = {6},
					   T.RECIPE_STATE = '2',
                       T.EXT_FLAG = T.DRUGED_TERMINAL
				WHERE  T.PARENT_CODE = '000010'
				  AND  T.CURRENT_CODE = '004004'
				  AND  T.RECIPE_STATE = '1'
				  AND  T.CLASS3_MEANING_CODE = '{2}'
				  AND  T.DRUG_DEPT_CODE = '{0}'
				  AND  T.RECIPE_NO = '{1}'
			*/
            #endregion
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Druged.Other", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm = {	
									   drugDept,							//药房
									   recipeNo,							//处方号
									   class3MeaningCode,					//出库分类
									   drugOper,							//配药人
									   drugedDept,							//配药科室
                                       drugedTerminal,                      //配药终端
									   drugedNum.ToString(),				//本次已配药数量
				};
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }


        /// <summary>
        /// 更新发药信息
        /// </summary>
        /// <param name="drugDept">库房编码</param>
        /// <param name="recipeNo">处方号</param>
        /// <param name="class3MeaningCode">出库分类</param>
        /// <param name="type">1 普通门诊 2 急诊发药</param>
        /// <param name="sendOper">发药人</param>
        /// <param name="sendDept">发药科室</param>
        /// <param name="sendTerminal">发药终端</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int UpdateDrugRecipeSendInfo(string drugDept, string recipeNo, string class3MeaningCode, string type, string sendOper, string sendDept, string sendTerminal)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Send", ref strSql) == -1)
                return -1;
            try
            {
                if (type == "1")			//普通门诊
                {
                    string[] strParm = {
										   drugDept,					//药房编码
										   recipeNo,					//处方号
										   class3MeaningCode,			//出库分类
										   "2",
										   sendOper,					//发药人
										   sendDept,					//发药科室
										   sendTerminal,				//发药终端
									   };
                    strSql = string.Format(strSql, strParm);
                }
                else if (type == "2")	   //急诊发药
                {
                    string[] strParm = {
										   drugDept,					//药房编码
										   recipeNo,					//处方号
										   class3MeaningCode,			//出库分类
										   "A",
										   sendOper,					//发药人
										   sendDept,					//发药科室
										   sendTerminal
									   };
                    strSql = string.Format(strSql, strParm);
                }

            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 还药确认
        /// </summary>
        /// <param name="drugDept">库房编码</param>
        /// <param name="recipeNo">处方号</param>
        /// <param name="class3MeaningCode">出库分类</param>
        /// <param name="drugOper">还药人</param>
        /// <param name="oldState">如需判断并发 指定数据原状态 不需判断 传为"A"</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int UpdateDrugRecipeBackInfo(string drugDept, string recipeNo, string class3MeaningCode, string drugOper, string oldState)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Back", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm = {	
									   drugDept,							//药房
									   recipeNo,							//处方号
									   class3MeaningCode,					//出库分类
									   drugOper,							//还药人
									   oldState,							//指定数据原状态
				};
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 对退改药更新处方状态、收费时间、退改药标记
        /// </summary>
        /// <param name="drugDept">发药药房</param>
        /// <param name="recipeNo">处方号</param>
        /// <param name="class3MeaningCode">权限码</param>
        /// <param name="newState">新处方状态</param>
        /// <param name="feeDate">收费时间</param>
        /// <param name="isModify">是否退改药</param>
        /// <returns>成功更新返回1 无记录返回0 出错返回0</returns>
        public int UpdateDrugRecipeModifyInfo(string drugDept, string recipeNo, string class3MeaningCode, string newState, DateTime feeDate, bool isModify)
        {
            string strSql = "";
            /*
             *
                    UPDATE PHA_STO_RECIPE T
                       SET T.Recipe_State = '{3}',
                           T.FEE_DATE = TO_DATE('{4}','YYYY-MM-DD HH24:MI:SS'),
                 T.MODIFY_FLAG = '{5}'
                    WHERE  T.PARENT_CODE = '000010'
                      AND  T.CURRENT_CODE = '004004'
                      AND  T.DRUG_DEPT_CODE = '{0}'
                      AND  T.RECIPE_NO = '{1}'
            AND  T.CLASS3_MEANING_CODE = '{2}' 
            */
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Modify", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm;
                if (isModify)
                {
                    strParm = new string[]{	
											  drugDept,							//药房
											  recipeNo,							//处方号
											  class3MeaningCode,					//出库分类
											  newState,							//新处方状态
											  feeDate.ToString(),					//收费时间
											  "1"
										  };
                }
                else
                {
                    strParm = new string[]{	
											  drugDept,							//药房
											  recipeNo,							//处方号
											  class3MeaningCode,					//出库分类
											  newState,							//新处方状态
											  feeDate.ToString(),					//收费时间
											  "0"
										  };
                }
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 对退改药更新处方状态、药品数量、处方金额、收费时间、退改药标记、处方号、有效性状态等
        /// </summary>
        /// <param name="modifyRecipeInfo">更改处方信息</param>
        /// <returns>成功更新返回1 无记录返回0 出错返回-1</returns>
        public int UpdateDrugRecipeModifyInfo(FS.HISFC.Models.Pharmacy.DrugRecipe modifyRecipeInfo)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.Modify.Recipe", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm;
                strParm = new string[]{	
										  modifyRecipeInfo.StockDept.ID,							//药房
										  modifyRecipeInfo.RecipeNO,							//处方号
										  modifyRecipeInfo.SystemType,							//出库分类
										  modifyRecipeInfo.RecipeState,							//新处方状态
										  modifyRecipeInfo.RecipeQty.ToString(),				//处方内品种数量
										  modifyRecipeInfo.Cost.ToString(),						//处方金额
										  modifyRecipeInfo.FeeOper.OperTime.ToString(),					//收费时间
										  modifyRecipeInfo.InvoiceNO,							//发票号
										  ((int)modifyRecipeInfo.ValidState).ToString(),							//有效性
										  NConvert.ToInt32(modifyRecipeInfo.IsModify).ToString()//退改药 0 否 1 是
									  };
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据旧发票号更新新发票号
        /// </summary>
        /// <param name="oldInvoiceNo">旧发票号</param>
        /// <param name="newInvoiceNo">新发票号</param>
        /// <returns>成功返回1 失败返回-1 无记录返回0</returns>
        public int UpdateDrugRecipeInvoiceN0(string oldInvoiceNo, string newInvoiceNo)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipeInfo.UpdateInvoiceNo", ref strSql) == -1)
                return -1;
            try
            {
                string[] strParm;
                strParm = new string[]{	
										 oldInvoiceNo,
										 newInvoiceNo
									  };
                strSql = string.Format(strSql, strParm);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 获取发送到指定药房、指定终端的处方列表
        /// </summary>
        /// <param name="deptCode">药房编码</param>
        /// <param name="terminalCode">终端编码</param>
        /// <param name="type">终端类别 0发药窗口/1配药台</param>
        /// <param name="state">处方状态</param>
        /// <returns>成功返回门诊摆药实体数组 失败返回null</returns>
        public ArrayList QueryList(string deptCode, string terminalCode, string type, string state)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Where1", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, terminalCode, type, state);
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
        /// 获取指定收费时间后发送到指定药房、指定终端的处方列表
        /// </summary>
        /// <param name="deptCode">药房编码</param>
        /// <param name="terminalCode">终端编码</param>
        /// <param name="type">终端类别  0发药窗口/1配药台/2还药/3直接发药</param>
        /// <param name="state">处方状态</param>
        /// <param name="queryDate">收费时间</param>
        /// <returns>成功返回门诊摆药实体数组 失败返回null</returns>
        public ArrayList QueryList(string deptCode, string terminalCode, string type, string state, DateTime queryDate)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (type == "1")		//配药
            {
                if (this.GetSQL("Pharmacy.DrugStore.GetList.Druged", ref strSqlWhere) == -1)
                {
                    return null;
                }
            }
            else if (type == "0")   //发药
            {
                if (this.GetSQL("Pharmacy.DrugStore.GetList.Send", ref strSqlWhere) == -1)
                {
                    return null;
                }
            }
            else if (type == "3")   //直接发药
            {
                if (this.GetSQL("Pharmacy.DrugStore.GetList.DirectSend", ref strSqlWhere) == -1)
                {
                    return null;
                }
            }

            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, terminalCode, state, queryDate.ToString());
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
        /// 获取某科室所有未发药患者列表
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns>成功返回患者列表 失败返回null</returns>
        public ArrayList QueryUnSendList(string deptCode)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetList.UnSend", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return null;
            }

            ArrayList al = this.myGetDrugRecipeInfo(strSqlSelect);

            return al;
        }

        /// <summary>
        /// 根据处方号获取处方调剂信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="class3MeaningCode">出库分类</param>
        /// <param name="recipeNo">处方号</param>
        /// <param name="state">处方状态</param>
        /// <returns>成功返回DrugRecipe实体 失败返回null 未找到返回空实体</returns>
        public FS.HISFC.Models.Pharmacy.DrugRecipe GetDrugRecipe(string deptCode, string class3MeaningCode, string recipeNo, string state)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Where3", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, class3MeaningCode, recipeNo, state);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            if (al == null)
                return null;
            if (al.Count == 0)
                return new FS.HISFC.Models.Pharmacy.DrugRecipe();
            return al[0] as FS.HISFC.Models.Pharmacy.DrugRecipe;
        }

        /// <summary>
        /// 根据处方号获取处方调剂信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="recipeNO">处方号</param>
        /// <returns>成功返回DrugRecipe实体,失败返回null 未找到返回空实体</returns>
        public FS.HISFC.Models.Pharmacy.DrugRecipe GetDrugRecipe(string deptCode, string recipeNO)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Where.Recipe", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, recipeNO);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            if (al == null)
                return null;
            if (al.Count == 0)
                return new FS.HISFC.Models.Pharmacy.DrugRecipe();
            return al[0] as FS.HISFC.Models.Pharmacy.DrugRecipe;
        }

        /// <summary>
        /// 根据发票号获取处方调剂信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="recipeNO">发票号</param>
        /// <returns>成功返回DrugRecipe实体,失败返回null 未找到返回空实体</returns>
        public ArrayList GetDrugRecipeList(string deptCode, string invoiceNO)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Where.Invoice", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, invoiceNO);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            if (al == null)
                return null;
            if (al.Count == 0)
                return new ArrayList();
            return al;
        }

        /// <summary>
        /// 根据单据号 获取 处方调剂信息
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="class3MeaningCode">出库分类 M1 门诊出库 M2  门诊退库</param>
        /// <param name="recipeState">处方状态</param>
        /// <param name="billType">单据类型 0 处方号 1 发票号 2 病历卡号</param>
        /// <param name="billNo">单据号</param>
        /// <returns>成功返回DrugRecipe数组 失败返回null 未找到返回空数组</returns>
        public ArrayList QueryDrugRecipe(string deptCode, string class3MeaningCode, string recipeState, int billType, string billNo)
        {
            string strSqlSelect = "", strSqlWhere = "";
            string strWhereIndex = "";				//SQL语句Where条件 索引
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            switch (billType)
            {
                case 0:			//处方号
                    strWhereIndex = "Pharmacy.DrugStore.GetList.Where3";
                    break;
                case 1:			//发票号
                    strWhereIndex = "Pharmacy.DrugStore.GetList.Where4";
                    break;
                default:		//病历卡号
                    strWhereIndex = "Pharmacy.DrugStore.GetList.Where5";
                    break;
            }
            if (this.GetSQL(strWhereIndex, ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode, class3MeaningCode, billNo, recipeState);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return null;
            }
            ArrayList al = new ArrayList();
            al = this.myGetDrugRecipeInfo(strSqlSelect);
            if (al == null)
                return null;
            return al;
        }

        /// <summary>
        /// 判断患者是否存在未取药的处方 如存在 则返回上一张处方的发药窗口号
        /// 如不存在未取药的处方 则返回发药窗口号为空
        /// 如上一张处方的发药窗口已关闭 则返回空
        /// </summary>
        /// <param name="deptCode">取药药房</param>
        /// <param name="clinicNo">门诊流水号</param>
        /// <param name="sendWindow">发药窗口号 为空表示不存在未取药处方</param>
        /// <returns>1 返回成功 －1 出错 </returns>
        public int JudegPatientRecipe(string deptCode, string clinicNo, out string sendWindow)
        {
            sendWindow = "";

            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.JudegPatientRecipe", ref strSql) == -1)
            {
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, deptCode, clinicNo);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return -1;
            }
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "获取未取药处方信息出错" + this.Err;
                return -1;
            }
            try
            {
                while (this.Reader.Read())
                {
                    sendWindow = this.Reader[0].ToString();
                }
            }
            catch (Exception ex)
            {
                this.Err = "" + ex.Message;
                return -1;
            }
            finally
            {
                this.Reader.Close();
            }
            return 1;
        }

        #endregion

        #endregion

        #region 处方调剂

        /// <summary>
        /// 获取调剂方式 
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns>成功返回处方调剂方式 0 平均 1 竞争</returns>
        public string GetAdjustType(string deptCode)
        {
            FS.FrameWork.Management.ExtendParam extManager = new FS.FrameWork.Management.ExtendParam();
            extManager.SetTrans(this.Trans);

            string adjustType = "0";

            try
            {
                FS.HISFC.Models.Base.ExtendInfo deptExt = extManager.GetComExtInfo(FS.HISFC.Models.Base.EnumExtendClass.DEPT, "TerminalAdjust", deptCode);
                if (deptExt == null)
                {
                    this.Err = "获取科室扩展属性内配药调剂参数失败！";

                    adjustType = "0";
                }

                if (deptExt.StringProperty == "1")		//竞争
                {
                    adjustType = "1";
                }
                else									//平均
                {
                    adjustType = "0";
                }
            }
            catch { }

            return adjustType;
        }

        /// <summary>
        /// 收费过程中调用 插入处方调剂表
        /// 返回处方调剂信息 发药药房+发药窗口
        /// </summary>
        /// <param name="patient">患者信息实体</param>
        /// <param name="feeAl">费用信息数组</param>
        /// <param name="feeWindow">收费窗口号</param>
        /// <param name="drugSendInfo">处方调剂信息 发药药房+发药窗口</param>        
        /// <returns>成功返回1 失败返回-1 </returns>
        public int DrugRecipe(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, string feeWindow, out string drugSendInfo)
        {
            return DrugRecipe(patient, feeAl, feeWindow, null, out drugSendInfo);
        }

        /// <summary>
        /// 收费过程中调用 插入处方调剂表
        /// 返回处方调剂信息 发药药房+发药窗口
        /// </summary>
        /// <param name="patient">患者信息实体</param>
        /// <param name="feeAl">费用信息数组</param>
        /// <param name="feeWindow">收费窗口号</param>
        /// <param name="hsDeptAddress">药房位置信息</param>
        /// <param name="drugSendInfo">处方调剂信息 发药药房+发药窗口</param>        
        /// <returns>成功返回1 失败返回-1 </returns>
        public int DrugRecipe(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, string feeWindow, System.Collections.Hashtable hsDeptAddress, out string drugSendInfo)
        {
            if (hsDeptAddress == null)
            {
                hsDeptAddress = new Hashtable();

                #region 以下代码以后挪到组合业务层

                //FS.HISFC.BizLogic.Manager.Constant consMgr = new FS.HISFC.BizLogic.Manager.Constant();
                ////consMgr.SetTrans(this.Transaction);
                //ArrayList alDeptAddress = consMgr.GetList("DeptAddress");
                //if (alDeptAddress != null)
                //{
                //    DrugStore.hsDeptAddress = new Hashtable();
                //    foreach (FS.HISFC.Models.Base.Const consInfo in alDeptAddress)
                //    {
                //        hsDeptAddress.Add(consInfo.ID, consInfo.Name);
                //    }
                //}

                #endregion
            }

            string adjustType = "0";            //0 平均调剂 1 竞争调剂

            drugSendInfo = "";

            #region 对费用信息数组按照发药药房进行分组
            ArrayList feeTempAl = new ArrayList();			//二维数组 存储分组后的费用信息
            ArrayList feeNowTemp = new ArrayList(); 		//二维数组 存储上一次的费用

            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeTemp;
            string privDrugDept = "";
            try
            {
                FeeSort feeSortInterface = new FeeSort();
                feeAl.Sort(feeSortInterface);
            }
            catch (Exception ex)
            {
                this.Err = "处理患者费用信息排序时发生错误" + ex.Message;
                return -1;
            }
            for (int i = 0; i < feeAl.Count; i++)
            {
                feeTemp = feeAl[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (feeTemp == null) continue;
                if (feeTemp.ExecOper.Dept.ID == privDrugDept)
                {
                    feeNowTemp.Add(feeTemp);
                }
                else
                {
                    feeNowTemp = new ArrayList();
                    feeNowTemp.Add(feeTemp);
                    feeTempAl.Add(feeNowTemp);
                    privDrugDept = feeTemp.ExecOper.Dept.ID;
                }
            }
            #endregion

            FS.HISFC.Models.Pharmacy.DrugRecipe info;		//处方调剂信息实体
            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeInfo = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            foreach (ArrayList temp in feeTempAl)
            {
                if (temp.Count == 0) continue;
                info = new DrugRecipe();
                info.Cost = 0;
                info.SumDays = 0;

                string recipeNo = "";
                ArrayList alTemp = new ArrayList();
                Hashtable comboHs = new Hashtable();

                try
                {
                    RecipeSort feeRecipeSort = new RecipeSort();
                    temp.Sort(feeRecipeSort);
                }
                catch (Exception ex)
                {
                    this.Err = "处理患者费用信息处方排序时发生错误" + ex.Message;
                    return -1;
                }

                //设置临时变量处理分方问题 避免设置了特殊药品的情况下，如果特殊药品处于第二个处方 那么会出现
                //一张医生处方分到了不同的配药台。同时对于处方调剂参数的更新也会出现只更新了一张处方的量
                DrugTerminal drugTerminalTemp = new DrugTerminal();
                DrugTerminal sendTerminalTemp = new DrugTerminal();
                bool isArrangeDrugTerminal = false;

                for (int i = 0; i < temp.Count; i++)
                {
                    feeInfo = temp[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                    if (feeInfo == null)
                    {
                        this.Err = "根据传入的费用实体数组 获取费用实体实例时发生类型转换错误";
                        return -1;
                    }
                    if (recipeNo != "" && recipeNo != feeInfo.RecipeNO)
                    {
                        if (alTemp.Count > 0)
                        {
                            //{24CF1B4D-1422-45da-B6E9-7075978ECF5A}  同一组费用可能存在不同的发票号
                            FS.HISFC.Models.Fee.Outpatient.FeeItemList tempFeeInfo = alTemp[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                            #region 根据费用信息实体对处方调剂信息实体进行赋值
                            info.StockDept.ID = feeInfo.ExecOper.Dept.ID;						//药房编码(发药药房)
                            info.RecipeNO = recipeNo;									        //处方号
                            info.SystemType = "M1";												//出库申请分类
                            info.TransType = "1";												//交易类型 1 正交易 2 反交易								
                            info.RecipeState = "0";												//处方状态
                            info.ClinicNO = feeInfo.Patient.ID;								            //门诊号
                            info.CardNO = feeInfo.Patient.PID.CardNO;							//病历号
                            info.PatientName = patient.Name;									//患者姓名
                            info.Sex = patient.Sex;												//性别
                            info.Age = patient.Birthday;										//年龄
                            info.PayKind.ID = patient.Pact.PayKind.ID;								//结算类别
                            //患者科室 ＝ 挂号科室
                            info.PatientDept = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.Templet.Dept;								//患者科室
                            info.RegTime = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.SeeDate;										//挂号日期
                            info.Doct = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.Templet.Doct;									//开方医生
                            info.DoctDept = feeInfo.RecipeOper.Dept;							//开方医生科室

                            //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                            if (patient.IsAccount)
                            {
                                info.FeeOper.OperTime = feeInfo.ChargeOper.OperTime;
                                info.FeeOper.ID = feeInfo.ChargeOper.ID;
                            }
                            else
                            {
                                info.FeeOper.OperTime = feeInfo.FeeOper.OperTime;					//收费时间
                                info.FeeOper.ID = feeInfo.FeeOper.ID;
                            }

                            //{24CF1B4D-1422-45da-B6E9-7075978ECF5A}  同一组费用可能存在不同的发票号
                            info.InvoiceNO = tempFeeInfo.Invoice.ID;									//票据号
                            info.RecipeQty = alTemp.Count;										//处方内药品品种数
                            info.DrugedQty = 0;													//已配药药品品种数
                            info.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;	//有效状态 1 有效 0 无效 2 发药后退费
                            info.IsModify = false;												//退/改药状态 0 否 1 是
                            //info.Memo = feeInfo.Memo;											//备注
                            #endregion

                            #region 获取处方调剂方式

                            adjustType = this.GetAdjustType(info.StockDept.ID);

                            #endregion

                            #region 根据处方调剂规则获取配药台、发药窗口编码

                            //DrugTerminal drugTerminalTemp = new DrugTerminal(),sendTerminalTemp = new DrugTerminal();
                            if (isArrangeDrugTerminal == false)     //对本数组第一次进行调剂
                            {
                                if (this.RecipeAdjust(patient, alTemp, feeWindow, adjustType, out drugTerminalTemp, out sendTerminalTemp) == -1)
                                    return -1;
                                isArrangeDrugTerminal = true;
                            }
                            else
                            {
                                int averageNum = 0;
                                if (adjustType == "1")
                                {
                                    averageNum = 1;
                                }
                                //更新调剂参数 在调剂函数外更新避免出现多更新
                                if (this.UpdateTerminalAdjustInfo(drugTerminalTemp.ID, alTemp.Count, alTemp.Count, averageNum) == -1)
                                {
                                    this.Err = "更新配药台已发送、待配药数量时出错" + this.Err;
                                    return -1;
                                }
                            }

                            info.DrugTerminal.ID = drugTerminalTemp.ID;
                            info.SendTerminal.ID = sendTerminalTemp.ID;
                            if (drugTerminalTemp.Memo != null)
                            {
                                info.Memo = drugTerminalTemp.Memo;
                            }

                            if (info.DrugTerminal.ID == "" || info.SendTerminal.ID == "")
                            {
                                this.Err = "处方调剂执行错误 未获取正确的配药台/发药窗口编码";
                                return -1;
                            }
                            if (drugSendInfo == "")
                            {
                                if (feeInfo.UndrugComb.User03 == null || feeInfo.UndrugComb.User03 == "")
                                {
                                    if (hsDeptAddress.ContainsKey(feeInfo.ExecOper.Dept.ID))
                                        drugSendInfo = drugSendInfo + hsDeptAddress[feeInfo.ExecOper.Dept.ID].ToString() + feeInfo.ExecOper.Dept.Name + sendTerminalTemp.Name;
                                    else
                                        drugSendInfo = drugSendInfo + feeInfo.ExecOper.Dept.Name + sendTerminalTemp.Name;	//取药药房 + 发药窗口
                                }
                            }
                            else
                            {
                                if (feeInfo.UndrugComb.User03 == null || feeInfo.UndrugComb.User03 == "")
                                {
                                    if (hsDeptAddress.ContainsKey(feeInfo.ExecOper.Dept.ID))
                                        drugSendInfo = drugSendInfo + "|" + hsDeptAddress[feeInfo.ExecOper.Dept.ID].ToString() + feeInfo.ExecOper.Dept.Name + sendTerminalTemp.Name;
                                    else
                                        drugSendInfo = drugSendInfo + "|" + feeInfo.ExecOper.Dept.Name + sendTerminalTemp.Name;	//取药药房 + 发药窗口
                                }
                            }
                            #endregion

                            if (this.InsertDrugRecipeInfo(info) == -1)
                            {
                                if (this.DBErrCode != 1)
                                {
                                    return -1;
                                }
                                else
                                {
                                    #region 对退/改药情况 对处方调剂头表进行状态更新
                                    int parm = this.UpdateDrugRecipeModifyInfo(info);
                                    if (parm == -1)
                                    {
                                        return parm;
                                    }
                                    else if (parm == 0)
                                    {
                                        this.Err = "未正确找到退改药需要更新的处方调剂头表数据 可能数据已发生变化 ";
                                        return -1;
                                    }
                                    #endregion
                                }
                            }
                        }

                        recipeNo = feeInfo.RecipeNO;

                        alTemp = new ArrayList();
                        comboHs = new Hashtable();
                        alTemp.Add(feeInfo);
                        comboHs.Add(feeInfo.Order.Combo, feeInfo.Days);
                        info.Cost = 0;
                        info.Cost = info.Cost + feeInfo.FT.TotCost;
                        info.SumDays = 0;
                        info.SumDays = info.SumDays + feeInfo.Days;
                    }
                    else
                    {
                        recipeNo = feeInfo.RecipeNO;
                        alTemp.Add(feeInfo);

                        if (!comboHs.ContainsKey(feeInfo.Order.Combo))
                        {
                            comboHs.Add(feeInfo.Order.Combo, feeInfo.Days);
                            info.SumDays = info.SumDays + feeInfo.Days;
                        }

                        info.Cost = info.Cost + feeInfo.FT.TotCost;
                    }

                }
                #region 保存最后一组
                if (alTemp != null && alTemp.Count > 0)
                {
                    //{24CF1B4D-1422-45da-B6E9-7075978ECF5A}  同一组费用可能存在不同的发票号
                    FS.HISFC.Models.Fee.Outpatient.FeeItemList tempFeeInfo = alTemp[0] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;

                    #region 根据费用信息实体对处方调剂信息实体进行赋值
                    info.StockDept.ID = feeInfo.ExecOper.Dept.ID;							//药房编码(发药药房)
                    info.RecipeNO = recipeNo;									            //处方号
                    info.SystemType = "M1";												    //出库申请分类
                    info.TransType = "1";												    //交易类型 1 正交易 2 反交易								
                    info.RecipeState = "0";												    //处方状态
                    info.ClinicNO = feeInfo.Patient.ID;								        //门诊号
                    info.CardNO = feeInfo.Patient.PID.CardNO;								//病历号
                    info.PatientName = patient.Name;									    //患者姓名
                    info.Sex = patient.Sex;												    //性别
                    info.Age = patient.Birthday;										    //年龄
                    info.PayKind.ID = patient.Pact.PayKind.ID;								//结算类别
                    //患者科室 ＝ 挂号科室
                    info.PatientDept = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.Templet.Dept;								//患者科室
                    info.RegTime = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.SeeDate;										//挂号日期
                    info.Doct = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.Templet.Doct;										//开方医生
                    info.DoctDept = feeInfo.RecipeOper.Dept;								//开方医生科室
                    //info.FeeOper.ID = feeInfo.FeeOper.ID;								    //收费人
                    //info.FeeOper.OperTime = feeInfo.FeeOper.OperTime;						//收费时间

                    //{6FC43DF1-86E1-4720-BA3F-356C25C74F16}
                    if (patient.IsAccount)
                    {
                        info.FeeOper.OperTime = feeInfo.ChargeOper.OperTime;
                        info.FeeOper.ID = feeInfo.ChargeOper.ID;
                    }
                    else
                    {
                        info.FeeOper.OperTime = feeInfo.FeeOper.OperTime;					//收费时间
                        info.FeeOper.ID = feeInfo.FeeOper.ID;
                    }

                    //{24CF1B4D-1422-45da-B6E9-7075978ECF5A}  同一组费用可能存在不同的发票号
                    info.InvoiceNO = tempFeeInfo.Invoice.ID;									//票据号
                    info.RecipeQty = alTemp.Count;										    //处方内药品品种数
                    info.DrugedQty = 0;													    //已配药药品品种数

                    info.ValidState = FS.HISFC.Models.Base.EnumValidState.Valid;

                    info.IsModify = false;												    //退/改药状态 0 否 1 是
                    //info.Memo = feeInfo.Memo;											    //备注
                    #endregion

                    #region 获取处方调剂方式

                    adjustType = this.GetAdjustType(info.StockDept.ID);

                    #endregion

                    #region 根据处方调剂规则获取配药台、发药窗口编码

                    //DrugTerminal drugTerminalTemp = new DrugTerminal(),sendTerminalTemp = new DrugTerminal();
                    if (isArrangeDrugTerminal == false)
                    {
                        if (this.RecipeAdjust(patient, alTemp, feeWindow, adjustType, out drugTerminalTemp, out sendTerminalTemp) == -1)
                            return -1;
                        isArrangeDrugTerminal = true;
                    }
                    else
                    {
                        int averageNum = 0;
                        if (adjustType == "1")
                        {
                            averageNum = 1;
                        }
                        //更新调剂参数
                        if (this.UpdateTerminalAdjustInfo(drugTerminalTemp.ID, alTemp.Count, alTemp.Count, averageNum) == -1)
                        {
                            this.Err = "更新配药台已发送、待配药数量时出错" + this.Err;
                            return -1;
                        }
                    }

                    info.DrugTerminal.ID = drugTerminalTemp.ID;
                    info.SendTerminal.ID = sendTerminalTemp.ID;
                    if (drugTerminalTemp.Memo != null)
                    {
                        info.Memo = drugTerminalTemp.Memo;
                    }
                    if (info.DrugTerminal.ID == "" || info.SendTerminal.ID == "")
                    {
                        this.Err = "处方调剂执行错误 未获取正确的配药台/发药窗口编码";
                        return -1;
                    }
                    if (drugSendInfo == "")
                    {
                        if (feeInfo.UndrugComb.User03 == null || feeInfo.UndrugComb.User03 == "")
                        {
                            if (hsDeptAddress.ContainsKey(feeInfo.ExecOper.Dept.ID))
                                drugSendInfo = drugSendInfo + hsDeptAddress[feeInfo.ExecOper.Dept.ID].ToString() + feeInfo.ExecOper.Dept.Name + sendTerminalTemp.Name;
                            else
                                drugSendInfo = drugSendInfo + feeInfo.ExecOper.Dept.Name + sendTerminalTemp.Name;	//取药药房 + 发药窗口
                        }
                    }
                    else
                    {
                        if (feeInfo.UndrugComb.User03 == null || feeInfo.UndrugComb.User03 == "")
                        {
                            if (hsDeptAddress.ContainsKey(feeInfo.ExecOper.Dept.ID))
                                drugSendInfo = drugSendInfo + "|" + hsDeptAddress[feeInfo.ExecOper.Dept.ID].ToString() + feeInfo.ExecOper.Dept.Name + sendTerminalTemp.Name;
                            else
                                drugSendInfo = drugSendInfo + "|" + feeInfo.ExecOper.Dept.Name + sendTerminalTemp.Name;	//取药药房 + 发药窗口
                        }
                    }
                    #endregion

                    if (this.InsertDrugRecipeInfo(info) == -1)
                    {
                        if (this.DBErrCode != 1)
                        {
                            return -1;
                        }
                        else
                        {
                            #region 对退/改药情况 对处方调剂头表进行状态更新
                            int parm = this.UpdateDrugRecipeModifyInfo(info);
                            if (parm == -1)
                            {
                                return parm;
                            }
                            else if (parm == 0)
                            {
                                this.Err = "未正确找到退改药需要更新的处方调剂头表数据 可能数据已发生变化 ";
                                return -1;
                            }
                            #endregion
                        }
                    }
                }
                #endregion
            }

            return 1;
        }

        /// <summary>
        /// 根据所传入的费用信息数组 根据调剂规则判断应发送的配药台、发药窗编号 
        /// 并返回 调剂后的发药窗口号、配药终端号
        /// </summary>
        /// <param name="patient">患者信息实体</param>
        /// <param name="feeAl">费用信息数组</param>
        /// <param name="feeWindow">收费窗口编码</param>
        /// <param name="adjustType">处方调剂类别 0 平均调剂 1 竞争调剂</param>
        /// <param name="drugTerminalObject">配药终端实体</param>
        /// <param name="sendTerminalObject">发药终端实体</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int RecipeAdjust(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, string feeWindow, string adjustType, out DrugTerminal drugTerminalObject, out DrugTerminal sendTerminalObject)
        {

            drugTerminalObject = new DrugTerminal();				//调剂结果返回的配药终端
            sendTerminalObject = new DrugTerminal();				//调剂结果返回的发药终端
            string drugTerminal = "";								//本组调剂分配后的配药台编码
            string sendTerminal = "";								//本组调剂分配后的发药窗编码

            string adjustLevel = "a";								//处方调剂级别 字符越大级别越高
            int drugKindNum = feeAl.Count;							//本处方品种数
            int averageNum = 0;										//均分次数

            if (adjustType != "1")
                adjustType = "0";

            FS.HISFC.Models.Fee.Outpatient.FeeItemList feeTemp = new FS.HISFC.Models.Fee.Outpatient.FeeItemList();
            if (feeAl.Count <= 0)
                return 1;

            for (int i = 0; i < feeAl.Count; i++)
            {
                #region 调剂规则计算
                feeTemp = feeAl[i] as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (feeTemp == null)
                {
                    this.Err = "处理费用信息时 发生类型转换错误";
                    return -1;
                }

                #region 根据特殊配药台的调剂规则进行判断 如返回不为空 判断是否为退改费的处方
                //判断是否满足特殊配药台需求 
                FS.HISFC.Models.Pharmacy.DrugSPETerminal speTerminalTemp = new DrugSPETerminal();
                //调剂中的专科根据什么判断 当前先根据挂号科室判断
                string strDept = ((FS.HISFC.Models.Registration.Register)feeTemp.Patient).DoctorInfo.Templet.Dept.ID;
                //feeItemList实体继承自BaseItem feeTemp.ID存储项目编码 
                speTerminalTemp = this.GetDrugSPETerminalByItemCode(adjustType, feeTemp.ExecOper.Dept.ID, feeWindow, feeTemp.Item.ID, strDept, patient.Pact.ID);
                //返回不为空 说明满足特殊配药台调剂条件 进入判断
                if (speTerminalTemp != null && speTerminalTemp.Terminal.ID != null && speTerminalTemp.Terminal.ID != "")
                {
                    if (adjustType == "1")
                        averageNum = 1;

                    FS.HISFC.Models.Pharmacy.DrugTerminal tempTerminal = this.GetDrugTerminal(speTerminalTemp.Terminal.ID);
                    if (tempTerminal != null && tempTerminal.ID != "" && !tempTerminal.IsClose)
                    {
                        speTerminalTemp.Terminal = tempTerminal;
                        if (speTerminalTemp.ItemType != null && speTerminalTemp.ItemType.CompareTo(adjustLevel) >= 0) //本次调剂级别高于已有配药台的调剂级别时才进行更改
                        {
                            drugTerminal = speTerminalTemp.Terminal.ID;					//根据调剂条件得到的配药台
                            adjustLevel = speTerminalTemp.ItemType;					//调剂级别 'a'～'z' 字符级大级别越高
                            drugTerminalObject = null;
                        }
                        if (speTerminalTemp.ItemType == "z")	//满足收费窗口的调剂规则 不需继续进行判断 可直接返回
                        {
                            //收费窗口为最高级别的调剂规则 肯定会对已有的配药台进行更改
                            drugTerminal = speTerminalTemp.Terminal.ID;
                            adjustLevel = "z";					//最高级别
                            drugTerminalObject = null;
                            break;
                        }
                        continue;
                    }
                }
                #endregion

                #region 判断该患者是否未取药的取药处方  此处取未关闭的配药台
                if (adjustLevel.CompareTo("d") < 0)				//原调剂级别优先级小于本级别时才进行下一步判断
                {
                    //发药药房 = 执行科室
                    this.JudegPatientRecipe(feeTemp.ExecOper.Dept.ID, feeTemp.Patient.PID.CardNO, out sendTerminal);
                    //存在未取药的处方
                    if (sendTerminal != "")
                    {
                        FS.HISFC.Models.Pharmacy.DrugTerminal terminalTemp = new DrugTerminal();
                        terminalTemp = this.GetDrugTerminalBySendWindow(sendTerminal);
                        if (terminalTemp != null && terminalTemp.ID != "")
                        {
                            drugTerminal = terminalTemp.ID;			//配药台编码
                            adjustLevel = "d";
                            drugTerminalObject = null;
                            continue;
                        }
                        else
                        {
                            sendTerminal = "";
                        }
                    }
                }
                #endregion

                #region 调剂规则判定 平均调剂/竞争调剂  此处取未关闭的、参与调剂的普通配药台
                if (adjustType != "1")
                {
                    #region 平均调剂
                    if (adjustLevel.CompareTo("c") < 0)			//上次调剂级别小于本级时 
                    {
                        drugTerminalObject = this.TerminalStatInfo(feeTemp.ExecOper.Dept.ID, "1");
                        if (drugTerminalObject == null)
                            return -1;
                        if (drugTerminalObject.ID != "")
                        {
                            averageNum = 0;
                            drugTerminal = drugTerminalObject.ID;
                            adjustLevel = "c";
                            continue;
                        }
                        else
                        {
                            this.Err = "在" + feeTemp.ExecOper.Dept.ID + "内未找到满足调剂条件的开放的配药台 请与药房管理人员联系";
                            return -1;
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 竞争调剂
                    if (adjustLevel.CompareTo("b") < 0)			//上次调剂级别小于本级时
                    {
                        drugTerminalObject = this.TerminalStatInfo(feeTemp.ExecOper.Dept.ID, "2");
                        if (drugTerminalObject == null)
                            return -1;
                        if (drugTerminalObject.ID != "")
                        {
                            averageNum = 1;
                            drugTerminal = drugTerminalObject.ID;
                            adjustLevel = "b";
                            continue;
                        }
                        else
                        {
                            this.Err = "在" + feeTemp.ExecOper.Dept.ID + "内未找到满足调剂条件的开放的配药台 请与药房管理人员联系";
                            return -1;
                        }
                    }
                    #endregion
                }
                #endregion

                #endregion
            }
            if (drugTerminal != "")
            {
                #region 根据该配药台编码 获取对应的发药窗口编码 更新已发送处方品种数信息 并返回对应的取药信息字符串
                if (drugTerminalObject == null || drugTerminalObject.ID == "")
                {
                    drugTerminalObject = this.GetDrugTerminal(drugTerminal);
                    if (drugTerminalObject == null)
                    {
                        this.Err = "获取调剂后的配药终端详细信息时出错" + this.Err;
                        return -1;
                    }
                    if (drugTerminalObject.ID == "")
                    {
                        this.Err = "根据处方调剂规则 无法找到满足条件且开放的配药台/发药窗口";
                        return -1;
                    }
                }
                //发药窗口编码为空 根据配药台获取对应的发药窗口编码
                if (sendTerminalObject == null || sendTerminalObject.ID == "")
                {
                    if (sendTerminal != null && sendTerminal != "")
                        sendTerminalObject = this.GetDrugTerminalById(sendTerminal);
                    else
                        sendTerminalObject = this.GetDrugTerminalById(drugTerminalObject.SendWindow.ID);
                    if (sendTerminalObject == null)
                    {
                        this.Err = "获取调剂后的发药终端详细信息时出错" + this.Err;
                        return -1;
                    }
                    if (sendTerminalObject.ID == "")
                    {
                        this.Err = "根据处方调剂规则 无法找到满足条件且开放的配药台/发药窗口" + this.Err;
                        return -1;
                    }
                }
                //更新已发送、待配药的处方品种数信息
                if (this.UpdateTerminalAdjustInfo(drugTerminalObject.ID, drugKindNum, drugKindNum, averageNum) == -1)
                {
                    this.Err = "更新配药台已发送、待配药数量时出错" + this.Err;
                    return -1;
                }

                //记录调剂原因
                drugTerminalObject.Memo = adjustLevel;
                return 1;
                #endregion
            }

            this.Err = "根据处方调剂规则 无法找到满足条件且开放的配药台/发药窗口" + this.Err;
            return -1;
        }

        public class FeeSort : System.Collections.IComparer
        {
            public FeeSort() { }


            #region IComparer 成员

            public int Compare(object x, object y)
            {
                // TODO:  添加 FeeSort.Compare 实现
                FS.HISFC.Models.Fee.Outpatient.FeeItemList f1 = x as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                FS.HISFC.Models.Fee.Outpatient.FeeItemList f2 = y as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (f1 == null || f2 == null)
                {
                    throw new Exception("数组内必须为OutPatient.FeeItemList类型");
                }
                string oX = f1.ExecOper.Dept.ID;          //执行科室
                string oY = f2.ExecOper.Dept.ID;          //执行科室

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

            #endregion

        }
        public class RecipeSort : System.Collections.IComparer
        {
            public RecipeSort() { }


            #region IComparer 成员

            public int Compare(object x, object y)
            {
                // TODO:  添加 FeeSort.Compare 实现
                FS.HISFC.Models.Fee.Outpatient.FeeItemList f1 = x as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                FS.HISFC.Models.Fee.Outpatient.FeeItemList f2 = y as FS.HISFC.Models.Fee.Outpatient.FeeItemList;
                if (f1 == null || f2 == null)
                    throw new Exception("数组内必须为OutPatient.FeeItemList类型");
                string oX = f1.RecipeNO;          //处方号
                string oY = f2.RecipeNO;          //处方号

                int nComp;

                if (oX == null)
                {
                    nComp = (oY != null) ? -1 : 0;
                }
                else if (oY == null)
                {
                    nComp = 1;
                }
                else
                {
                    nComp = string.Compare(oX.ToString(), oY.ToString());
                }

                return nComp;
            }

            #endregion
        }

        #endregion


        #region 华南版本新加

        
        /// <summary>
        /// 获取住院摆药通知的患者列表
        /// </summary>
        /// <param name="drugControlNO">摆药台</param>
        /// <returns>DrugMessage摆药通知实体</returns>
        public ArrayList QueryDrugMessageInpatientList(FS.HISFC.Models.Pharmacy.DrugControl drugControl)
        {
            return this.QueryDrugMessageInpatientList(drugControl, 10);
        }

        /// <summary>
        /// 获取住院摆药通知的患者列表
        /// </summary>
        /// <param name="drugControlNO">摆药台</param>
        /// <returns>DrugMessage摆药通知实体</returns>
        public ArrayList QueryDrugMessageInpatientList(FS.HISFC.Models.Pharmacy.DrugControl drugControl, int daySpan)
        {
            string strSQL = "";

            //取SQL语句ByDrugControlAndMessage
            if (this.GetSQL("Pharmacy.DrugStore.GetInpatientList.ByDrugControlAndTime", ref strSQL) == -1)
            {
                strSQL = @"select  u.billclass_code,
                                   u.dept_code,
                                   u.patient_id,
                                   i.bed_no,
                                   i.name,
                                   u.send_type
                            from                           
                            (                        
                                  select distinct a.billclass_code,
                                         a.dept_code,
                                         a.patient_id,
                                         a.send_type
                                  from   pha_com_applyout a,pha_sto_control c,pha_sto_msg m
                                  where  a.drug_dept_code = m.med_dept_code
                                  and    a.billclass_code = m.billclass_code
                                  and    a.apply_state = '0'
                                  
                                  and    a.valid_state = '1'
                                  and    m.billclass_code = c.billclass_code
                                  and    m.med_dept_code = c.dept_code
                                  and    ((m.send_type = c.send_type and    a.send_type = m.send_type) or c.send_type = '0' or m.send_type = '0' or a.send_type = '0')
                                  and    m.dept_code = a.dept_code
                                  and    c.control_code = '{0}'
                                  and    a.apply_date > sysdate - {1}
                            ) u,fin_ipr_inmaininfo i
                            where u.patient_id = i.inpatient_no
                            order by i.bed_no";
                this.CacheSQL("Pharmacy.DrugStore.GetInpatientList.ByDrugControlAndTime", strSQL);
            }
            try
            {
                string[] strParm = { drugControl.ID ,daySpan.ToString()};
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByDrugControlAndTime";
                return null;
            }

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取摆药通知列表时出错：" + this.Err;
                return null;
            }

            ArrayList al = new ArrayList();
            try
            {
                DrugMessage info;   //摆药通知实体		
                while (this.Reader.Read())
                {
                    info = new DrugMessage();
                    try
                    {
                        info.DrugBillClass.ID = this.Reader[0].ToString();          //摆药单分类编码
                        info.ApplyDept.ID = this.Reader[1].ToString();          //发送科室编码
                        info.ID = this.Reader[2].ToString();                 //住院流水号
                        info.Memo = this.Reader[3].ToString();                 //床号
                        info.Name = this.Reader[4].ToString();                 //姓名
                        info.User01 = info.ID;                       

                        info.StockDept.ID = drugControl.Dept.ID;
                        if (this.Reader.FieldCount > 5)
                        {
                            info.SendType = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[5].ToString());
                        }
                        else
                        {
                            info.SendType = drugControl.SendType;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得摆药通知信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(info);
                }
                return al;

            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得摆药通知信息时，执行SQL语句出错！myGetDrugBillClass" + ex.Message;
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
        /// 插入一条摆药单分类记录
        /// </summary>
        /// <param name="info">摆药单分类实体</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int InsertOneDrugBillClass(FS.HISFC.Models.Pharmacy.DrugBillClass info)
        {
            string strSql = "";

            if (this.GetSQL("Pharmacy.DrugStore.InsertDrugBillClass", ref strSql) == -1) return -1;
            try
            {
                //取摆药单分类流水号
                if (string.IsNullOrEmpty(info.ID))
                {
                    string ID = "";
                    ID = this.GetSysDateTime("yyMMddHHmmss");
                    if (ID == "-1") return -1;
                    info.ID = ID;
                }

                string[] strParm = myGetParmDrugBillClass(info);  //取参数列表
                strSql = string.Format(strSql, strParm);       //
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.ErrCode = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 查询处方
        /// </summary>
        /// <param name="deptCode">科室编码（满足in）</param>
        /// <param name="recipeNO">处方号</param>
        /// <param name="invoiceNO">发票号</param>
        /// <param name="cardNO">病历号</param>
        /// <param name="patientName">患者姓名</param>
        /// <param name="beginTime">开始时间，参考收费时间</param>
        /// <param name="endTime">结束时间，参考收费时间</param>
        /// <returns></returns>
        public ArrayList QueryRecipeList(string deptCode, string recipeNO, string invoiceNO, string cardNO, string patientName, DateTime beginTime, DateTime endTime)
        {
            string strSQLlSelect = "";
            string strSQLWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSQLlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("SOC.DrugStore.Recipe.MutiWhere", ref strSQLWhere) == -1)
            {
                strSQLWhere = @"
                          where drug_dept_code in ({0})
                          and   (recipe_no = '{1}' or '{1}' = 'All')
                          and   (invoice_no = '{2}' or '{2}' = 'All')
                          and   (card_no = '{3}' or '{3}' = 'All')
                          and   (patient_name = '{4}' or '{4}' = 'All')
                          and   fee_date >= to_date('{5}','yyyy-mm-dd hh24:mi:ss')
                          and   fee_date <  to_date('{6}','yyyy-mm-dd hh24:mi:ss')
                          order by card_no,patient_name
                            ";
                this.CacheSQL("SOC.DrugStore.Recipe.MutiQuery", strSQLWhere);
            }
            try
            {
                string[] strParm = { deptCode, recipeNO, invoiceNO, cardNO, patientName, beginTime.ToString(), endTime.ToString() };

                strSQLWhere = string.Format(strSQLWhere, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|Pharmacy.DrugStore.GetDrugMessageList.ByDrugControl";
                return null;
            }

            return this.myGetDrugRecipeInfo(strSQLlSelect + strSQLWhere);
        }

        #endregion

        #region 住院药房调用

        /// <summary>
        /// 根据处方号与处方内项目流水号获取 未核准申请信息 状态为 '0' '1'
        /// </summary>
        ///<param name="recipeNo">处方号</param>
        /// <param name="sequenceNo">项目流水号</param>
        /// <returns>成功返回摆药实体 失败返回null 无数据返回空实体</returns>
        public FS.HISFC.Models.Pharmacy.ApplyOut GetApplyOut(string recipeNo, int sequenceNo)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.ByRecipeNo", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.ByRecipeNo字段!";
                return null;
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, recipeNo, sequenceNo.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            ArrayList al = this.myGetApplyOut(strSelect);
            if (al == null) return null;

            if (al.Count == 0)
                return new FS.HISFC.Models.Pharmacy.ApplyOut();
            else
                return al[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
        }

        /// <summary>
        /// 根据执行档流水号获取 未核准的申请信息 状态为 '0' '1'
        /// </summary>
        /// <param name="orderExecNO">执行档流水号</param>
        /// <returns>成功返回出库申请实体信息 失败返回null 无数据返回空实体</returns>
        public FS.HISFC.Models.Pharmacy.ApplyOut GetApplyOutByExecNO(string orderExecNO)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.ByOrderExecNO", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.ByOrderExecNO字段!";
                return null;
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, orderExecNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            ArrayList al = this.myGetApplyOut(strSelect);
            if (al == null) return null;

            if (al.Count == 0)
                return new FS.HISFC.Models.Pharmacy.ApplyOut();
            else
                return al[0] as FS.HISFC.Models.Pharmacy.ApplyOut;
        }      

        /// <summary>
        /// 取某一处方号所有未核准的申请列表
        /// </summary>
        /// <param name="recipeNo">处方号</param>
        /// <returns>成功返回出库申请数据数组 失败返回null</returns>
        public ArrayList QueryApplyOut(string recipeNo)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.ByRecipeNo.1", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.ByRecipeNo.1字段!";
                return null;
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, recipeNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            ArrayList al = this.myGetApplyOut(strSelect);
            if (al == null) return null;
            return al;
        }

        /// <summary>
        /// 获取所有满足条件的申请明细信息	
        /// </summary>
        ///<param name="recipeNo">处方号</param>
        /// <param name="sequenceNo">项目流水号</param>
        /// <returns>成功返回摆药实体数组 失败返回null 无数据返回空实体</returns>
        public ArrayList QueryApplyOut(string recipeNo, int sequenceNo)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.ByRecipeNo", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.ByRecipeNo字段!";
                return null;
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, recipeNo, sequenceNo.ToString());
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
        /// 取某一申请科室未被核准的申请列表	状态为 0
        /// </summary>
        /// <param name="applyDeptCode">申请科室编码</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutList(string applyDeptCode)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.ByApplyDept", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.ByApplyDept字段!";
                return null;
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, applyDeptCode);
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
        /// 取某一药房，某一摆药通知中待摆药数据列表
        /// 传入参数前，需要将摆药台中的SendType赋给通知实体
        /// </summary>
        /// <param name="drugMessage">摆药通知信息</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutList(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage)
        {
            string strSQL = "";  //取某一药房中某一中摆药单、某一科室待摆药数据的SQL语句
            string strWhere = "";  //取某一药房中某一中摆药单、某一科室待摆药数据的WHERE语句
            //如果摆药通知类型为集中或者临时，则取相应的出库申请数据。
            //取SQL语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.Patient", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.ByMessage", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.ByMessage字段!";
                return null;
            }

            try
            {
                string[] strParm = { drugMessage.ApplyDept.ID, drugMessage.StockDept.ID, drugMessage.DrugBillClass.ID, drugMessage.SendType.ToString(), };
                strSQL = string.Format(strSQL + strWhere, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSQL);
        }

        /// <summary>
        /// 取某一药房，某一摆药通知中待摆药数据列表
        /// 传入参数前，需要将摆药台中的SendType赋给通知实体
        /// </summary>
        /// <param name="drugMessage">摆药通知信息</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutList(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, int daySpan)
        {
            string strSQL = "";  //取某一药房中某一中摆药单、某一科室待摆药数据的SQL语句
            string strWhere = "";  //取某一药房中某一中摆药单、某一科室待摆药数据的WHERE语句
            //如果摆药通知类型为集中或者临时，则取相应的出库申请数据。
            //取SQL语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.Patient", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }
            //取WHERE语句
            if (this.GetSQL("SOC.Pharmacy.Item.GetApplyOutList.ByMessage", ref strWhere) == -1)
            {
                strWhere = @"
                                AND  PHA_COM_APPLYOUT.APPLY_STATE =  '0'           --申请状态（0申请，1打印，2摆药）
                                AND  PHA_COM_APPLYOUT.VALID_STATE =  fun_get_valid            --有效状态（0有效，1无效，2不摆药）
                                AND  PHA_COM_APPLYOUT.DEPT_CODE =  '{0}'
                                AND  PHA_COM_APPLYOUT.DRUG_DEPT_CODE =  '{1}'
                                AND  PHA_COM_APPLYOUT.BILLCLASS_CODE =  '{2}'
                                AND  (PHA_COM_APPLYOUT.SEND_TYPE =  {3} OR  {3} = 0)
                                AND  (PHA_COM_APPLYOUT.DRUGED_BILL IS NULL OR PHA_COM_APPLYOUT.DRUGED_BILL = '0')
                                AND  PHA_COM_APPLYOUT.APPLY_DATE > SYSDATE - {4}
                                ORDER BY FIN_IPR_INMAININFO.BED_NO,FIN_IPR_INMAININFO.NAME,PHA_COM_APPLYOUT.CANCEL_DATE,PHA_COM_APPLYOUT.COMB_NO
                ";
                this.CacheSQL("SOC.Pharmacy.Item.GetApplyOutList.ByMessage", strWhere);
            }

            try
            {
                string[] strParm = { drugMessage.ApplyDept.ID, drugMessage.StockDept.ID, drugMessage.DrugBillClass.ID, drugMessage.SendType.ToString(), daySpan.ToString()};
                strSQL = string.Format(strSQL + strWhere, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSQL);
        }

        /// <summary>
        /// 取某一药房，某一摆药通知中待摆药数据列表
        /// 传入参数前，需要将摆药台中的SendType赋给通知实体
        /// </summary>
        /// <param name="drugMessage">摆药通知信息</param>
        /// <param name="dtBgn">查询起始时间</param>
        /// <param name="dtEnd">查询终止时间</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutListByTime(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage, DateTime dtBgn, DateTime dtEnd)
        {
            string strSQL = "";  //取某一药房中某一中摆药单、某一科室待摆药数据的SQL语句
            string strWhere = "";  //取某一药房中某一中摆药单、某一科室待摆药数据的WHERE语句
            string strWhereIndex = "";
            //如果摆药通知类型为集中或者临时，则取相应的出库申请数据。
            //取SQL语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.Patient", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }
            if (drugMessage.SendType == 1)
                strWhereIndex = "Pharmacy.Item.GetApplyOutList.ByTime.1";
            else
                strWhereIndex = "Pharmacy.Item.GetApplyOutList.ByTime.2";
            //取WHERE语句
            if (this.GetSQL(strWhereIndex, ref strWhere) == -1)
            {
                this.Err = "没有找到 " + strWhereIndex + " 字段!";
                return null;
            }

            try
            {
                string[] strParm = {drugMessage.ApplyDept.ID, drugMessage.StockDept.ID, drugMessage.DrugBillClass.ID, drugMessage.SendType.ToString(),
									   dtBgn.ToString(),dtEnd.ToString()};
                strSQL = string.Format(strSQL + strWhere, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSQL);
        }

        /// <summary>
        /// 取某一药房，某一摆药通知中待摆药数据列表
        /// </summary>
        /// <param name="applyDeptNO"></param>
        /// <param name="dtBgn">查询起始时间</param>
        /// <param name="dtEnd">查询终止时间</param>
        /// <param name="patient">住院号、姓名，All全部</param>
        /// <param name="drugNO">药品编码</param>
        /// <param name="state">All全部</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutList(string applyDeptNO,string recipeDeptId, DateTime dtBgn, DateTime dtEnd, string patient, string drugNO, string state)
        {
            string strSQL = "";  //取某一药房中某一中摆药单、某一科室待摆药数据的SQL语句
            string strWhere = "";  //取某一药房中某一中摆药单、某一科室待摆药数据的WHERE语句
            string strWhereIndex = "";
            //如果摆药通知类型为集中或者临时，则取相应的出库申请数据。
            //取SQL语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.Patient", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }
            strWhereIndex = "SOC.Pharmacy.DrugStore.GetPatientDrugApplyOutList.Order";
            //取WHERE语句
            if (this.GetSQL(strWhereIndex, ref strWhere) == -1)
            {
                //this.Err = "没有找到 " + strWhereIndex + " 字段!";
                //return null;
                strWhere = @"
	                          AND (FIN_IPR_INMAININFO.PATIENT_NO = lpad('{4}',10,'0') OR FIN_IPR_INMAININFO.NAME = '{4}' OR '{4}' = 'All')
	                          AND (PHA_COM_APPLYOUT.DRUG_CODE = '{6}' OR '{6}' = 'All')
                              AND (PHA_COM_APPLYOUT.APPLY_STATE = '{5}' OR '{5}' = 'All') 
                              AND (PHA_COM_APPLYOUT.DEPT_CODE = '{0}' OR '{0}' = 'All') 
                              AND (PHA_COM_APPLYOUT.RECIPE_DEPT='{1}'OR '{1}'='All')
                              AND PHA_COM_APPLYOUT.APPLY_DATE >= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                              AND PHA_COM_APPLYOUT.APPLY_DATE <  to_date('{3}','yyyy-mm-dd hh24:mi:ss')
                              ORDER BY FIN_IPR_INMAININFO.BED_NO,FIN_IPR_INMAININFO.NAME,PHA_COM_APPLYOUT.CANCEL_DATE,PHA_COM_APPLYOUT.COMB_NO
                            ";

                this.CacheSQL(strWhereIndex, strWhere);
            }

            try
            {
                string[] strParm = { applyDeptNO,recipeDeptId, dtBgn.ToString(), dtEnd.ToString(), patient, state, drugNO };
                strSQL = string.Format(strSQL + strWhere, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSQL);
        }

        /// <summary>
        /// 取某一药房，某一摆药通知中基数药品待摆药数据列表
        /// </summary>
        /// <param name="applyDeptNO"></param>
        /// <param name="dtBgn">查询起始时间</param>
        /// <param name="dtEnd">查询终止时间</param>
        /// <param name="patient">住院号、姓名，All全部</param>
        /// <param name="drugNO">药品编码</param>
        /// <param name="state">All全部</param>
        /// <param name="applyInState">请领状态：0未请领，1已经请领，All全部</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryRadixApplyOutList(string applyDeptNO,string recipeDeptId, DateTime dtBgn, DateTime dtEnd, string patient, string drugNO, string state, string applyInState)
        {
            string strSQL = "";  //取某一药房中某一中摆药单、某一科室待摆药数据的SQL语句
            string strWhere = "";  //取某一药房中某一中摆药单、某一科室待摆药数据的WHERE语句
            string strWhereIndex = "";
            //如果摆药通知类型为集中或者临时，则取相应的出库申请数据。
            //取SQL语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.Patient", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }
            strWhereIndex = "SOC.Pharmacy.DrugStore.GetPatientDrugApplyOutList";
            //取WHERE语句
            if (this.GetSQL(strWhereIndex, ref strWhere) == -1)
            {
                //this.Err = "没有找到 " + strWhereIndex + " 字段!";
                //return null;
                strWhere = @"
	                          AND (FIN_IPR_INMAININFO.PATIENT_NO = lpad('{4}',10,'0') OR FIN_IPR_INMAININFO.NAME = '{4}' OR '{4}' = 'All')
	                          AND (PHA_COM_APPLYOUT.DRUG_CODE = '{6}' OR '{6}' = 'All')
                              AND (PHA_COM_APPLYOUT.APPLY_STATE = '{5}' OR '{5}' = 'All') 
                              AND (PHA_COM_APPLYOUT.DEPT_CODE = '{0}' OR '{0}' = 'All') 
                              AND (PHA_COM_APPLYOUT.RECIPE_DEPT='{1}'OR '{1}'='All')
                              AND PHA_COM_APPLYOUT.APPLY_DATE >= to_date('{2}','yyyy-mm-dd hh24:mi:ss')
                              AND PHA_COM_APPLYOUT.APPLY_DATE <  to_date('{3}','yyyy-mm-dd hh24:mi:ss')
                            ";
                this.CacheSQL(strWhereIndex, strWhere);
            }
            string strRadixApplyInWhere = "";
             //取WHERE语句
            if (this.GetSQL("SOC.Pharmacy.DrugStore.GetPatientDrugApplyOutList.Radix", ref strRadixApplyInWhere) == -1)
            {
                strRadixApplyInWhere = @"
                                             AND ((PHA_COM_APPLYOUT.APPLY_BILLCODE is null AND '0' = '{6}') 
                                                   OR (PHA_COM_APPLYOUT.APPLY_BILLCODE = '1' AND '1' = '{6}') 
                                                   OR 'All' = '{6}')
                                             AND PHA_COM_APPLYOUT.DRUG_CODE IN 
                                             (SELECT DRUG_CODE FROM PHA_COM_STOCKINFO WHERE RADIX_FLAG = '1' AND DRUG_DEPT_CODE = '{0}')
                                             ORDER BY FIN_IPR_INMAININFO.BED_NO,FIN_IPR_INMAININFO.NAME,PHA_COM_APPLYOUT.CANCEL_DATE,PHA_COM_APPLYOUT.COMB_NO
                                            ";

                this.CacheSQL("SOC.Pharmacy.DrugStore.GetPatientDrugApplyOutList.Radix", strRadixApplyInWhere);
            }
            try
            {
                string[] strParm = { applyDeptNO,recipeDeptId, dtBgn.ToString(), dtEnd.ToString(), patient, state, drugNO, applyInState };
                strSQL = string.Format(strSQL + strWhere + strRadixApplyInWhere, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSQL);
        }


        /// <summary>
        /// 取某一药房，某一摆药通知中某一患者待摆药数据列表
        /// 传入参数前，需要将摆药台中的SendType赋给通知实体
        /// 患者信息住院流水号User01，姓名User02，床号User03
        /// </summary>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutListByPatient(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage)
        {
            string strSQL = "";  //取某一药房，某一摆药通知中某一患者待摆药数据列表的SQL语句
            string strWhere = "";  //取某一药房，某一摆药通知中某一患者待摆药数据列表的WHERE语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.Patient", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutListByPatient", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutListByPatient字段!";
                return null;
            }

            try
            {
                string[] strParm = {
									   drugMessage.ApplyDept.ID,             //0申请科室
									   drugMessage.StockDept.ID,              //1药房编码
									   drugMessage.DrugBillClass.ID,        //2摆药单分类编码
									   drugMessage.SendType.ToString(),     //3发送类型
									   drugMessage.User01                   //4患者住院流水号
								   };
                strSQL = string.Format(strSQL + strWhere, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSQL);
        }


        /// <summary>
        /// 取某一药房，某一摆药通知中某一患者待摆药数据列表
        /// 传入参数前，需要将摆药台中的SendType赋给通知实体
        /// 患者信息住院流水号User01，姓名User02，床号User03
        /// </summary>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutListByPatient(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage,int daySpan)
        {
            string strSQL = "";  //取某一药房，某一摆药通知中某一患者待摆药数据列表的SQL语句
            string strWhere = "";  //取某一药房，某一摆药通知中某一患者待摆药数据列表的WHERE语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.Patient", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }
            //取WHERE语句
            if (this.GetSQL("SOC.Pharmacy.Item.GetApplyOutListByPatient", ref strWhere) == -1)
            {
                strWhere = @"
                                                    
                                AND	PHA_COM_APPLYOUT.APPLY_STATE =  '0' 		      --申请状态（0申请，1打印，2摆药）
                                AND	PHA_COM_APPLYOUT.VALID_STATE =  fun_get_valid			      --有效状态（0有效，1无效，2不摆药）
                                AND	PHA_COM_APPLYOUT.DEPT_CODE   =  '{0}'
                                AND	PHA_COM_APPLYOUT.DRUG_DEPT_CODE =  '{1}'
                                AND	PHA_COM_APPLYOUT.BILLCLASS_CODE =  '{2}'
                                AND	(PHA_COM_APPLYOUT.SEND_TYPE  =  {3} OR  {3} = 0)
                                AND	PHA_COM_APPLYOUT.PATIENT_ID  =  '{4}'
                                AND  (PHA_COM_APPLYOUT.DRUGED_BILL IS NULL OR PHA_COM_APPLYOUT.DRUGED_BILL = '0')
                                AND  PHA_COM_APPLYOUT.APPLY_DATE > SYSDATE - {5}
                                ORDER BY PHA_COM_APPLYOUT.PATIENT_ID,PHA_COM_APPLYOUT.CANCEL_DATE,PHA_COM_APPLYOUT.COMB_NO
                ";

                this.CacheSQL("SOC.Pharmacy.Item.GetApplyOutListByPatient", strWhere);
            }

            try
            {
                string[] strParm = {
									   drugMessage.ApplyDept.ID,             //0申请科室
									   drugMessage.StockDept.ID,              //1药房编码
									   drugMessage.DrugBillClass.ID,        //2摆药单分类编码
									   drugMessage.SendType.ToString(),     //3发送类型
									   drugMessage.User01,                   //4患者住院流水号
                                       daySpan.ToString()
								   };
                strSQL = string.Format(strSQL + strWhere, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetApplyOut(strSQL);
        }

        /// <summary>
        /// 取某一张摆药单中的摆药数据
        /// </summary>
        /// <param name="billCode">摆药单号</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutListByBill(string billCode, string state)
        {
            string strSQL = "";  //取某一药房，某一张摆药单中的摆药数据的SQL语句
            string strWhere = "";  //取某一药房，某一张摆药单中的摆药数据的WHERE语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.Patient", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }
            //取WHERE语句
            if (this.GetSQL("SOC.Pharmacy.Item.GetApplyOutListByBill.Where", ref strWhere) == -1)
            {
                strWhere = @"
                                AND PHA_COM_APPLYOUT.DRUGED_BILL IN ({0})
                                AND	 PHA_COM_APPLYOUT.APPLY_STATE IN ({1})
                                ORDER BY   FIN_IPR_INMAININFO.BED_NO,PHA_COM_APPLYOUT.CANCEL_DATE, PHA_COM_APPLYOUT.COMB_NO
                ";
            }

            if (billCode.IndexOf("'") == -1)
            {
                billCode = "'" + billCode + "'";
            }
            if (state.IndexOf("'") == -1)
            {
                state = "'" + state + "'";
            }

            try
            {
                strSQL = string.Format(strSQL + strWhere, billCode, state);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            //根据SQL语句取摆药数据数组并返回数组
            return this.myGetApplyOut(strSQL);
        }

        /// <summary>
        /// 护士站领药单明细打印{B22172AC-5DE2-4897-9923-598503E86E2A}
        /// </summary>
        /// <param name="billCode">摆药单号</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutListDetailByBillClassCode(string billClassCode, string deptCode, string startDate, string endDate, string drugedType)
        {
            #region 屏蔽
            //string strSQL = "";  //取某一药房，某一张摆药单中的摆药数据的SQL语句
            //string strWhere = "";  //取某一药房，某一张摆药单中的摆药数据的WHERE语句

            ////取SELECT语句
            //if (this.GetSQL("Pharmacy.Item.GetApplyOutList.Patient", ref strSQL) == -1)
            //{
            //    this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
            //    return null;
            //}
            ////取WHERE语句
            //if (this.GetSQL("Pharmacy.Item.GetApplyOutListByBillClassCode.Where", ref strWhere) == -1)
            //{
            //    this.Err = "没有找到Pharmacy.Item.GetApplyOutListByBillClassCode.Where字段!";
            //    return null;
            //}

            ////if (billCode.IndexOf("'") == -1)
            ////{
            ////    billCode = "'" + billCode + "'";
            ////}

            //try
            //{
            //    strSQL = string.Format(strSQL + strWhere, billClassCode, deptCode, startDate, endDate, drugedType);
            //}
            //catch (Exception ex)
            //{
            //    this.Err = ex.Message;
            //    return null;
            //}
            ////根据SQL语句取摆药数据数组并返回数组
            //return this.myGetApplyOut(strSQL);
            #endregion
            string strSQL = "";
            //取SQL语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutListDetailByBillClassCode", ref strSQL) == -1)//{19858F06-C495-45cf-A21C-85E855241034}
            {
                this.Err = "没有找到Pharmacy.Item.GetDrugBillDetail字段!";
                return null;
            }

            //if (drugBillCode.IndexOf("'") == -1)
            //{
            //    drugBillCode = "'" + drugBillCode + "'";
            //}

            strSQL = string.Format(strSQL, billClassCode, deptCode, startDate, endDate, drugedType);

            //根据SQL语句取数组并返回数组
            ArrayList arrayObject = new ArrayList();

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取明细摆药单时出错：" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut obj; //患者信息科室编码User01，摆药单号User02

                while (this.Reader.Read())
                {
                    obj = new FS.HISFC.Models.Pharmacy.ApplyOut();
                    obj.ApplyDept.ID = this.Reader[0].ToString();                   //0申请部门编码（科室或者病区）
                    obj.StockDept.Name = this.Reader[1].ToString();                  //1发药部门编码 
                    obj.Item.ID = this.Reader[2].ToString();                        //2药品编码
                    obj.Item.Name = this.Reader[3].ToString();                      //3药品商品名
                    obj.Item.Specs = this.Reader[4].ToString();                     //4规格
                    obj.Item.PackUnit = this.Reader[5].ToString();                  //5包装单位
                    obj.Item.PackQty = NConvert.ToDecimal(this.Reader[6].ToString());//6包装数
                    obj.Item.MinUnit = this.Reader[7].ToString();                   //7最小单位
                    obj.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[8].ToString()); //8零售价
                    obj.Days = NConvert.ToDecimal(this.Reader[9].ToString());       //9付数
                    obj.User01 = this.Reader[10].ToString();                        //10患者姓名
                    obj.User02 = this.Reader[11].ToString();                        //11床号
                    obj.DoseOnce = NConvert.ToDecimal(this.Reader[12].ToString());  //12每次剂量
                    obj.Item.DoseUnit = this.Reader[13].ToString();                 //13剂量单位
                    obj.Usage.ID = this.Reader[14].ToString();                      //14用法代码
                    obj.Usage.Name = this.Reader[15].ToString();                    //15用法名称
                    obj.Frequency.ID = this.Reader[16].ToString();                  //16频次代码
                    obj.Frequency.Name = this.Reader[17].ToString();                //17频次名称
                    obj.Operation.ApplyQty = NConvert.ToDecimal(this.Reader[18].ToString());  //18申请出库量
                    obj.DrugNO = this.Reader[19].ToString();                      //19摆药单号
                    obj.PrintState = this.Reader[20].ToString();                    //20打印状态（0未打印，1已打印）
                    obj.Operation.ExamOper.ID = this.Reader[21].ToString();                  //21打印人
                    obj.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[22].ToString()); //22打印日期
                    obj.CombNO = this.Reader[23].ToString();						//23组合序号
                    obj.Memo = this.Reader[24].ToString();							//24医嘱备注
                    obj.PlaceNO = this.Reader[25].ToString();						//25货位号
                    obj.User03 = this.Reader[26].ToString();
                    obj.OrderNO = this.Reader[27].ToString();					//医嘱流水号
                    obj.SendType = NConvert.ToInt32(this.Reader[28].ToString());//发送类型 1 集中 2 临时 0 全部
                    obj.State = this.Reader[29].ToString();				//单据状态                    
                    arrayObject.Add(obj);
                }
                return arrayObject;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得明细摆药单时，执行SQL语句出错！GetDrugBillDetail" + ex.Message;
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
        /// 护士站领药单汇总打印{CC985758-A2AE-41da-9394-34AFCEB0E30E}
        /// </summary>
        /// <param name="billCode">摆药单号</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutListTotByBillClassCode(string billClassCode, string deptCode, string startDate, string endDate, string drugedType)
        {
            string strSQL = "";  //取某一药房，某一张摆药单中的摆药数据的SQL语句
            string strWhere = "";  //取某一药房，某一张摆药单中的摆药数据的WHERE语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutListTotByBillClassCode", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }
            //取WHERE语句
            //if (this.GetSQL("Pharmacy.Item.GetApplyOutListByBillClassCode.Where", ref strWhere) == -1)
            //{
            //    this.Err = "没有找到Pharmacy.Item.GetApplyOutListByBillClassCode.Where字段!";
            //    return null;
            //}

            //if (billCode.IndexOf("'") == -1)
            //{
            //    billCode = "'" + billCode + "'";
            //}

            try
            {
                strSQL = string.Format(strSQL, billClassCode, deptCode, startDate, endDate, drugedType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            ArrayList arrayObject = new ArrayList();

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取汇总摆药单时出错：" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut obj = null;
                while (this.Reader.Read())
                {
                    obj = new FS.HISFC.Models.Pharmacy.ApplyOut();
                    obj.ApplyDept.ID = this.Reader[0].ToString();                        //0申请部门编码（科室或者病区）
                    obj.StockDept.Name = this.Reader[1].ToString();                     //1发药部门编码 
                    obj.Item.ID = this.Reader[2].ToString();                             //2药品编码
                    obj.Item.Name = this.Reader[3].ToString();                           //3药品商品名
                    obj.Item.Specs = this.Reader[4].ToString();                          //4规格
                    obj.Item.PackUnit = this.Reader[5].ToString();                       //5包装单位
                    obj.Item.PackQty = NConvert.ToDecimal(this.Reader[6].ToString());    //6包装数
                    obj.Item.MinUnit = this.Reader[7].ToString();                        //7最小单位
                    obj.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[8].ToString());//8零售价
                    obj.Operation.ApplyQty = NConvert.ToDecimal(this.Reader[9].ToString());        //9申请出库量
                    obj.DrugNO = this.Reader[10].ToString();                           //10摆药单号
                    obj.PrintState = this.Reader[11].ToString();                         //11打印状态（0未打印，1已打印）
                    obj.Operation.ExamOper.ID = this.Reader[12].ToString();                       //12打印人
                    obj.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[13].ToString());      //13打印日期
                    obj.PlaceNO = this.Reader[14].ToString();							 //14货位号
                    obj.SendType = NConvert.ToInt32(this.Reader[15].ToString());	//15 发送标志                    
                    arrayObject.Add(obj);
                }
                return arrayObject;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得汇总摆药单时，执行SQL语句出错！GetDrugBillTotal" + ex.Message;
                this.ErrCode = "-1";
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            //根据SQL语句取摆药数据数组并返回数组
            //return this.myGetApplyOut(strSQL);
        }

        /// <summary>
        /// 取某一药房，某一摆药通知中待摆药的患者列表
        /// 传入参数前，需要将摆药台中的SendType赋给通知实体
        /// 如果摆药通知类型为集中或者临时，则取相应的出库申请数据
        /// </summary>
        /// <returns>neuObject数组，患者信息住院流水号ID，姓名Name，床号Memo 失败返回null</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryApplyOutPatientList(FS.HISFC.Models.Pharmacy.DrugMessage drugMessage)
        {
            string strSQL = "";  //取某一药房中某一中摆药单、某一科室待摆药患者列表的SQL语句

            if (this.GetSQL("Pharmacy.Item.GetApplyOutPatientList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutPatientList字段!";
                return null;
            }
            string[] strParm = {
								   drugMessage.ApplyDept.ID,             //0申请科室
								   drugMessage.StockDept.ID,              //1药房编码
								   drugMessage.DrugBillClass.ID,        //2摆药单分类编码
								   drugMessage.SendType.ToString(),     //3发送类型
			};
            strSQL = string.Format(strSQL, strParm);

            //根据SQL语句取数组并返回数组
            List<FS.FrameWork.Models.NeuObject> neuObjectList = new List<FS.FrameWork.Models.NeuObject>();

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取待摆药患者列表时出错：" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject obj; //患者信息住院流水号ID，姓名Name，床号Memo	
                while (this.Reader.Read())
                {
                    obj = new FS.FrameWork.Models.NeuObject();
                    obj.ID = this.Reader[0].ToString();                   //住院流水号
                    obj.Name = this.Reader[1].ToString();                 //姓名
                    obj.Memo = this.Reader[2].ToString();                 //床号

                    neuObjectList.Add(obj);
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得待摆药患者列表时，执行SQL语句出错！myGetDrugBillClass" + ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return neuObjectList;
        }

        /// <summary>
        /// 取某一药房，某一天的摆药单列表
        /// 摆药单分类，摆药单号，
        /// </summary>
        /// <param name="deptCode">药房编码</param>
        /// <param name="dateTime">日期</param>
        /// <returns>成功返回摆药单列表 失败返回null</returns>
        public ArrayList QueryDrugBillByDay(string deptCode, DateTime dateTime)
        {
            string strSQL = "";
            //取SQL语句
            if (this.GetSQL("Pharmacy.Item.GetDrugBillByDay", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetDrugBillByDay字段!";
                return null;
            }
            string[] strParm = {
								   deptCode,             //0摆药科室编码
								   dateTime.ToString()   //1日期
							   };
            strSQL = string.Format(strSQL, strParm);

            //根据SQL语句取数组并返回数组
            ArrayList arrayObject = new ArrayList();

            this.ProgressBarText = "正在检索患者信息...";
            this.ProgressBarValue = 0;

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取待摆药患者列表时出错：" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.DrugBillClass obj;
                while (this.Reader.Read())
                {
                    obj = new FS.HISFC.Models.Pharmacy.DrugBillClass();
                    obj.ID = this.Reader[0].ToString();                 //摆药单分类编码
                    obj.Name = this.Reader[1].ToString();               //摆药单分类名称
                    obj.PrintType.ID = this.Reader[2].ToString();       //打印类型
                    obj.Oper.ID = this.Reader[3].ToString();            //配药核准人编码
                    obj.Oper.OperTime = NConvert.ToDateTime(this.Reader[4].ToString());//打印摆药单时间
                    obj.DrugBillNO = this.Reader[5].ToString();         //摆药单号
                    obj.ApplyState = this.Reader[6].ToString();         //申请状态
                    obj.ApplyDept.Name = this.Reader[7].ToString();     //发送科室名称
                    obj.ApplyDept.ID = this.Reader[8].ToString();     //发送科室代码
                    this.ProgressBarValue++;
                    arrayObject.Add(obj);
                }
                return arrayObject;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得待摆药患者列表时，执行SQL语句出错！myGetDrugBillClass" + ex.Message;
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
        /// 获取病区已经发送到药房的摆药申请单
        /// 在病区申请时形成单号，这个流程忽略摆药通知档
        /// </summary>
        /// <param name="drugControl">摆药台编码</param>
        /// <returns>成功返回摆药单列表 失败返回null</returns>
        public ArrayList QuerySendedDrugBill(FS.HISFC.Models.Pharmacy.DrugControl drugControl, int daySpan)
        {
            string strSQL = "";
            //取SQL语句
            if (this.GetSQL("SOC.Pharmacy.DrugStore.GetSendedDrugBill", ref strSQL) == -1)
            {
                strSQL = @"
                        SELECT DISTINCT PHA_STO_BILLCLASS.BILLCLASS_CODE,
                                        PHA_STO_BILLCLASS.BILLCLASS_NAME,
                                        PHA_STO_BILLCLASS.PRINT_TYPE,
                                        PHA_COM_APPLYOUT.APPLY_OPERCODE,
                                        PHA_COM_APPLYOUT.APPLY_DATE,
                                        PHA_COM_APPLYOUT.DRUGED_BILL,
                                        PHA_COM_APPLYOUT.APPLY_STATE,
                                        COM_DEPARTMENT.DEPT_NAME,
                                        COM_DEPARTMENT.DEPT_CODE
                          FROM PHA_STO_BILLCLASS, PHA_COM_APPLYOUT, COM_DEPARTMENT, PHA_STO_CONTROL
                         WHERE COM_DEPARTMENT.DEPT_CODE = PHA_COM_APPLYOUT.DEPT_CODE
                           AND PHA_STO_BILLCLASS.BILLCLASS_CODE = PHA_COM_APPLYOUT.BILLCLASS_CODE
                           AND PHA_COM_APPLYOUT.APPLY_STATE IN ('0')
                           AND PHA_COM_APPLYOUT.DRUGED_BILL IS NOT NULL
                           AND PHA_COM_APPLYOUT.DRUGED_BILL <> '0'
                           AND PHA_COM_APPLYOUT.CLASS3_MEANING_CODE IN ('Z1', 'Z2')
                           AND PHA_STO_CONTROL.CONTROL_CODE = '{0}'
                           AND PHA_STO_CONTROL.BILLCLASS_CODE = PHA_COM_APPLYOUT.BILLCLASS_CODE
                           AND PHA_STO_CONTROL.DEPT_CODE = PHA_COM_APPLYOUT.DRUG_DEPT_CODE
                           AND PHA_COM_APPLYOUT.APPLY_DATE >= SYSDATE - 30
                         ORDER BY COM_DEPARTMENT.DEPT_CODE, PHA_COM_APPLYOUT.DRUGED_BILL, PHA_COM_APPLYOUT.APPLY_DATE


                        ";
            }
            string[] strParm = {
								   drugControl.ID,             //0摆药台编码
								   daySpan.ToString()   //1日期
							   };
            strSQL = string.Format(strSQL, strParm);

            //根据SQL语句取数组并返回数组
            ArrayList arrayObject = new ArrayList();

            this.ProgressBarText = "正在检索患者信息...";
            this.ProgressBarValue = 0;

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取待摆药患者列表时出错：" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.DrugBillClass obj;
                Hashtable hs = new Hashtable();

                while (this.Reader.Read())
                {
                    obj = new FS.HISFC.Models.Pharmacy.DrugBillClass();
                    obj.ID = this.Reader[0].ToString();                 //摆药单分类编码
                    obj.Name = this.Reader[1].ToString();               //摆药单分类名称
                    obj.PrintType.ID = this.Reader[2].ToString();       //打印类型
                    obj.Oper.ID = this.Reader[3].ToString();            //申请人
                    obj.Oper.OperTime = NConvert.ToDateTime(this.Reader[4].ToString());//申请时间
                    obj.DrugBillNO = this.Reader[5].ToString();         //摆药单号
                    obj.ApplyState = this.Reader[6].ToString();         //申请状态
                    obj.ApplyDept.Name = this.Reader[7].ToString();     //发送科室名称
                    obj.ApplyDept.ID = this.Reader[8].ToString();     //发送科室代码
                    this.ProgressBarValue++;

                    if (!hs.Contains(obj.DrugBillNO))
                    {
                        arrayObject.Add(obj);
                        hs.Add(obj.DrugBillNO, null);
                    }
                }
                return arrayObject;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得待摆药患者列表时，执行SQL语句出错！myGetDrugBillClass" + ex.Message;
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
        /// 取汇总摆药单
        /// </summary>
        /// <param name="drugBillCode">摆药单号</param>
        /// <returns>成功返回摆药申请信息 失败返回null</returns>
        public ArrayList QueryDrugBillTotal(string drugBillCode)
        {
            string strSQL = "";
            //取SQL语句
            if (this.GetSQL("Pharmacy.Item.GetDrugBillTotal", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetDrugBillTotal字段!";
                return null;
            }

            if (drugBillCode.IndexOf("'") == -1)
            {
                drugBillCode = "'" + drugBillCode + "'";
            }

            strSQL = string.Format(strSQL, drugBillCode);

            //根据SQL语句取数组并返回数组
            ArrayList arrayObject = new ArrayList();

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取汇总摆药单时出错：" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut obj = null;
                while (this.Reader.Read())
                {
                    obj = new FS.HISFC.Models.Pharmacy.ApplyOut();
                    obj.ApplyDept.ID = this.Reader[0].ToString();                        //0申请部门编码（科室或者病区）
                    obj.StockDept.Name = this.Reader[1].ToString();                     //1发药部门编码 
                    obj.Item.ID = this.Reader[2].ToString();                             //2药品编码
                    obj.Item.Name = this.Reader[3].ToString();                           //3药品商品名
                    obj.Item.Specs = this.Reader[4].ToString();                          //4规格
                    obj.Item.PackUnit = this.Reader[5].ToString();                       //5包装单位
                    obj.Item.PackQty = NConvert.ToDecimal(this.Reader[6].ToString());    //6包装数
                    obj.Item.MinUnit = this.Reader[7].ToString();                        //7最小单位
                    obj.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[8].ToString());//8零售价
                    obj.Operation.ApplyQty = NConvert.ToDecimal(this.Reader[9].ToString());        //9申请出库量
                    obj.DrugNO = this.Reader[10].ToString();                           //10摆药单号
                    obj.PrintState = this.Reader[11].ToString();                         //11打印状态（0未打印，1已打印）
                    obj.Operation.ExamOper.ID = this.Reader[12].ToString();                       //12打印人
                    obj.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[13].ToString());      //13打印日期
                    obj.PlaceNO = this.Reader[14].ToString();							 //14货位号
                    obj.SendType = NConvert.ToInt32(this.Reader[15].ToString());	//15 发送标志
                    arrayObject.Add(obj);
                }
                return arrayObject;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得汇总摆药单时，执行SQL语句出错！GetDrugBillTotal" + ex.Message;
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
        /// 取明细摆药单
        /// //患者信息科室编码User01，摆药单号User02
        /// </summary>
        /// <param name="drugBillCode">摆药单号</param>
        /// <returns>成功返回摆药申请信息 失败返回null</returns>
        public ArrayList QueryDrugBillDetail(string drugBillCode)
        {
            string strSQL = "";
            //取SQL语句
            if (this.GetSQL("Pharmacy.Item.GetDrugBillDetail", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetDrugBillDetail字段!";
                return null;
            }

            if (drugBillCode.IndexOf("'") == -1)
            {
                drugBillCode = "'" + drugBillCode + "'";
            }

            strSQL = string.Format(strSQL, drugBillCode);

            //根据SQL语句取数组并返回数组
            ArrayList arrayObject = new ArrayList();

            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = "取明细摆药单时出错：" + this.Err;
                return null;
            }
            try
            {
                FS.HISFC.Models.Pharmacy.ApplyOut obj; //患者信息科室编码User01，摆药单号User02

                while (this.Reader.Read())
                {
                    obj = new FS.HISFC.Models.Pharmacy.ApplyOut();
                    obj.ApplyDept.ID = this.Reader[0].ToString();                   //0申请部门编码（科室或者病区）
                    obj.StockDept.Name = this.Reader[1].ToString();                  //1发药部门编码 
                    obj.Item.ID = this.Reader[2].ToString();                        //2药品编码
                    obj.Item.Name = this.Reader[3].ToString();                      //3药品商品名
                    obj.Item.Specs = this.Reader[4].ToString();                     //4规格
                    obj.Item.PackUnit = this.Reader[5].ToString();                  //5包装单位
                    obj.Item.PackQty = NConvert.ToDecimal(this.Reader[6].ToString());//6包装数
                    obj.Item.MinUnit = this.Reader[7].ToString();                   //7最小单位
                    obj.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[8].ToString()); //8零售价
                    obj.Days = NConvert.ToDecimal(this.Reader[9].ToString());       //9付数
                    obj.User01 = this.Reader[10].ToString();                        //10患者姓名
                    obj.User02 = this.Reader[11].ToString();                        //11床号
                    obj.DoseOnce = NConvert.ToDecimal(this.Reader[12].ToString());  //12每次剂量
                    obj.Item.DoseUnit = this.Reader[13].ToString();                 //13剂量单位
                    obj.Usage.ID = this.Reader[14].ToString();                      //14用法代码
                    obj.Usage.Name = this.Reader[15].ToString();                    //15用法名称
                    obj.Frequency.ID = this.Reader[16].ToString();                  //16频次代码
                    obj.Frequency.Name = this.Reader[17].ToString();                //17频次名称
                    obj.Operation.ApplyQty = NConvert.ToDecimal(this.Reader[18].ToString());  //18申请出库量
                    obj.DrugNO = this.Reader[19].ToString();                      //19摆药单号
                    obj.PrintState = this.Reader[20].ToString();                    //20打印状态（0未打印，1已打印）
                    obj.Operation.ExamOper.ID = this.Reader[21].ToString();                  //21打印人
                    obj.Operation.ExamOper.OperTime = NConvert.ToDateTime(this.Reader[22].ToString()); //22打印日期
                    obj.CombNO = this.Reader[23].ToString();						//23组合序号
                    obj.Memo = this.Reader[24].ToString();							//24医嘱备注
                    obj.PlaceNO = this.Reader[25].ToString();						//25货位号
                    obj.User03 = this.Reader[26].ToString();
                    obj.OrderNO = this.Reader[27].ToString();					//医嘱流水号
                    obj.SendType = NConvert.ToInt32(this.Reader[28].ToString());//发送类型 1 集中 2 临时 0 全部
                    obj.State = this.Reader[29].ToString();				//单据状态

                    arrayObject.Add(obj);
                }
                return arrayObject;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得明细摆药单时，执行SQL语句出错！GetDrugBillDetail" + ex.Message;
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
                if (this.GetSQL("Pharmacy.Item.ExamApplyOut", ref strSQL) == -1)
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
        /// 审核出库申请单信息
        /// 用来记录打印摆药单的状态，摆药单状态由前台传入（打印摆药单时直接核准扣库存则为1，否则为2）
        /// 如果此方法返回0，则表示有并发操作。
        /// </summary>
        /// <param name="applyOut">出库申请记录</param>
        /// <returns>0没有更新（并发） 1成功 -1失败</returns>
        public int ApproveApplyOut(FS.HISFC.Models.Pharmacy.ApplyOut applyOut)
        {
            string strSQL = "";

            try
            {
                //确认发药，更新申请状态，发药数量，发药人，核准日期，核准科室
                if (this.GetSQL("Pharmacy.Item.ApproveApplyOut", ref strSQL) == -1)
                {
                    this.Err = "没有找到SQL语句Pharmacy.Item.ApproveApplyOut";
                    return -1;
                }

                //取参数列表
                string[] strParm = {
									   applyOut.ID,                     //出库申请单流水号
									   applyOut.Operation.ApproveOper.ID,        //核准人
									   applyOut.Operation.ApproveOper.Dept.ID          //核准科室
								   };

                strSQL = string.Format(strSQL, strParm);          //替换SQL语句中的参数。

            }
            catch (Exception ex)
            {
                this.Err = "核准出库申请SQl参数赋值时出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 根据执行档流水号更新申请有效性
        /// </summary>
        /// <param name="orderExecNO">执行档流水号</param>
        /// <param name="isValid">是否有效 True 有效 False 无效</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int UpdateApplyOutValidByExecNO(string orderExecNO, bool isValid)
        {
            string strSQL = "";
            //根据执行档流水号，作废出库申请记录的Update语句
            if (this.GetSQL("Pharmacy.Item.CancelApplyOut.OrderExecNO", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.CancelApplyOut.OrderExecNO";
                return -1;
            }

            //1 恢复申请有效性 0 作废申请
            if (isValid)
                strSQL = string.Format(strSQL, orderExecNO, this.Operator.ID, ((int)FS.HISFC.Models.Base.EnumValidState.Valid).ToString());
            else
                strSQL = string.Format(strSQL, orderExecNO, this.Operator.ID, ((int)FS.HISFC.Models.Base.EnumValidState.Invalid).ToString());

            int parm = this.ExecNoQuery(strSQL);
            if (parm != 1)
                return parm;
            return 1;
        }

        /// <summary>
        /// 根据处方号更新申请有效性
        /// </summary>
        /// <param name="recipeNO">处方号</param>
        /// <param name="sequenceNO">处方内项目流水号</param>
        /// <param name="isValid">是否有效 True 有效 False 无效</param>
        /// <returns>成功返回1 失败返回-1</returns>
        protected int UpdateApplyOutValidByRecipeNO(string recipeNO, int sequenceNO, bool isValid)
        {
            string strSQL = "";
            //根据处方流水号和处方内序号，作废出库申请记录的Update语句
            if (this.GetSQL("Pharmacy.Item.CancelApplyOut", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.CancelApplyOut";
                return -1;
            }

            //1 恢复申请有效性 0 作废申请
            if (isValid)
                strSQL = string.Format(strSQL, recipeNO, sequenceNO.ToString(), this.Operator.ID, ((int)FS.HISFC.Models.Base.EnumValidState.Valid).ToString());
            else
                strSQL = string.Format(strSQL, recipeNO, sequenceNO.ToString(), this.Operator.ID, ((int)FS.HISFC.Models.Base.EnumValidState.Invalid).ToString());

            int parm = this.ExecNoQuery(strSQL);
            if (parm != 1)
                return parm;

            return 1;
        }

        /// <summary>
        /// 根据患者信息 获取用药申请信息
        /// </summary>
        /// <param name="patientID">患者住院流水号</param>
        /// <param name="drugDeptCode">库存药房</param>
        /// <param name="beginTime">起始时间</param>
        /// <param name="endTime">截至时间</param>
        /// <returns>成功返回申请信息 失败返回null</returns>
        public ArrayList GetPatientApply(string patientID, string drugDeptCode, DateTime beginTime, DateTime endTime, string state)
        {
            return this.GetPatientApply(patientID, drugDeptCode, "AAAA", beginTime, endTime, state);
        }

        /// <summary>
        /// 根据患者信息 获取用药申请信息
        /// </summary>
        /// <param name="patientID">患者住院流水号</param>
        /// <param name="drugDeptCode">库存药房</param>
        /// <param name="applyDept">申请科室</param>
        /// <param name="beginTime">起始时间</param>
        /// <param name="endTime">截至时间</param>
        /// <param name="state">状态</param>
        /// <returns>成功返回申请信息 失败返回null</returns>
        public ArrayList GetPatientApply(string patientID, string drugDeptCode, string applyDept, DateTime beginTime, DateTime endTime, string state)
        {
            string strSelect = "";  //取某一科室申请，某一目标本科室未核准的SELECT语句
            string strWhere = "";  //取某一科室申请，某一目标本科室未核准的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.PatientValidApply", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.PatientValidApply字段!";
                return null;
            }

            try
            {
                string[] strParm = { patientID, drugDeptCode, applyDept, beginTime.ToString(), endTime.ToString(), state };
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
        /// 获取时间段内的有效的患者用药申请信息
        /// 返回值为NeuObject ID 患者流水号 Name 患者姓名 Memo 申请科室
        /// </summary>
        /// <param name="drugDeptCode">库存药房</param>
        /// <param name="dtBegin">起始时间</param>
        /// <param name="dtEnd">终止时间</param>
        /// <param name="state">申请状态</param>
        /// <returns>成功返回用药申请信息 失败返回null</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryInPatientApplyOutList(string drugDeptCode, DateTime dtBegin, DateTime dtEnd, string state)
        {
            string strSelect = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.QueryInPatientApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryInPatientApplyOutList字段!";
                return null;
            }

            try
            {
                string[] strParm = { drugDeptCode, dtBegin.ToString(), dtEnd.ToString(), state };
                strSelect = string.Format(strSelect, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取数组并返回数组
            List<FS.FrameWork.Models.NeuObject> patientApplyList = new List<FS.FrameWork.Models.NeuObject>();

            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "取汇总摆药单时出错：" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject info = null;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();            //患者ID流水号
                    info.Name = this.Reader[1].ToString();          //患者姓名
                    info.Memo = this.Reader[2].ToString();          //申请科室

                    patientApplyList.Add(info);
                }

                return patientApplyList;
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
        /// 获取时间段内的有效的患者用药申请信息
        /// 返回值为NeuObject ID 患者流水号 Name 患者姓名 Memo 申请科室
        /// </summary>
        /// <param name="drugDeptCode">库存药房</param>
        /// <param name="dtBegin">起始时间</param>
        /// <param name="dtEnd">终止时间</param>
        /// <param name="stateCollection">申请状态</param>
        /// <returns>成功返回用药申请信息 失败返回null</returns>
        public List<FS.FrameWork.Models.NeuObject> QueryOutPatientApplyOutList(string drugDeptCode, DateTime dtBegin, DateTime dtEnd, params string[] stateCollection)
        {
            string strSelect = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.QueryOutPatientApplyOutList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryOutPatientApplyOutList字段!";
                return null;
            }

            try
            {
                string strState = "";
                foreach (string str in stateCollection)
                {
                    strState = str + "','" + strState;
                }
                string[] strParm = { drugDeptCode, dtBegin.ToString(), dtEnd.ToString(), strState };
                strSelect = string.Format(strSelect, strParm);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            //根据SQL语句取数组并返回数组
            List<FS.FrameWork.Models.NeuObject> patientApplyList = new List<FS.FrameWork.Models.NeuObject>();

            if (this.ExecQuery(strSelect) == -1)
            {
                this.Err = "取汇总摆药单时出错：" + this.Err;
                return null;
            }
            try
            {
                FS.FrameWork.Models.NeuObject info = null;
                while (this.Reader.Read())
                {
                    info = new FS.FrameWork.Models.NeuObject();

                    info.ID = this.Reader[0].ToString();            //患者ID流水号
                    info.Name = this.Reader[1].ToString();          //患者姓名
                    info.Memo = this.Reader[2].ToString();          //申请科室

                    patientApplyList.Add(info);
                }

                return patientApplyList;
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

        #region 配置中心调用处理

        /// <summary>
        /// 获取配置中心列表
        /// </summary>
        /// <param name="drugDeptCode">库存科室</param>
        /// <param name="groupCode">批次</param>
        /// <returns>成功返回待配置患者列表 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.ApplyOut> QueryCompoundList(string drugDeptCode, string groupCode, string state)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.QueryCompoundList", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.QueryCompoundList字段!";
                return null;
            }

            try
            {
                strSelect = string.Format(strSelect + " " + strWhere, drugDeptCode, groupCode, state);
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
            if (this.GetSQL("Pharmacy.Item.QueryCompoundList.ExecState", ref strSelect) == -1)
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
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.Patient", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.QueryCompoundApplyOut.Patient.GroupCode.ApplyDept", ref strWhere) == -1)
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
        /// 配置信息检索。根据批次流水号
        /// </summary>
        /// <param name="compoundGroup">批次流水号</param>
        /// <returns></returns>
        public ArrayList QueryCompoundApplyOut(string compoundGroup)
        {
            string strSelect = "";  //取某一申请科室未被核准数据的SELECT语句
            string strWhere = "";  //取某一申请科室未被核准数据的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList.Patient", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList.Patient字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.QueryCompoundApplyOut.CompoundGroup", ref strWhere) == -1)
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
            if (this.GetSQL("Pharmacy.Item.UpdateCompoundApplyOut", ref strSQL) == -1)
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
            if (this.GetSQL("Pharmacy.Item.UpdateCompoundGroupNO", ref strSQL) == -1)
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
        #endregion

        #endregion

        #region 门诊药房调用

        /// <summary>
        /// 获取门诊处方明细
        /// </summary>
        /// <param name="drugDept">库房编码</param>
        /// <param name="class3MeaningCode">出库分类</param>
        /// <param name="state">出库状态</param>
        /// <param name="recipeNo">处方号</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutListForClinic(string drugDept, string class3MeaningCode, string state, string recipeNo)
        {
            string strSQL = "";  //取某一药房，某一张摆药单中的摆药数据的SQL语句
            string strWhere = "";  //取某一药房，某一张摆药单中的摆药数据的WHERE语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutListForClinic.Where", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutListForClinic.Where字段!";
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL + strWhere, drugDept, class3MeaningCode, state, recipeNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            //根据SQL语句取摆药数据数组并返回数组
            return this.myGetApplyOut(strSQL);
        }

        /// <summary>
        /// 根据发票号查询处方调剂实体
        /// </summary>
        /// <param name="drugDept"></param>
        /// <param name="class3MeaningCode"></param>
        /// <returns></returns>
        public ArrayList QueryDrugRecipeByInvoice(string drugDept, string class3MeaningCode,string invoice)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.DrugStore.GetList.Select", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.DrugStore.GetList.ByInvioce", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect,drugDept, class3MeaningCode, invoice);
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
        /// 获取门诊处方明细
        /// </summary>
        /// <param name="drugDept">库房编码</param>
        /// <param name="class3MeaningCode">出库分类</param>
        /// <param name="state">出库状态</param>
        /// <param name="recipeNo">处方号</param>
        /// <returns>成功返回申请信息数组 失败返回null</returns>
        public ArrayList QueryApplyOutListForInvoiceNO(string drugDept, string class3MeaningCode, string state, string invoiceNO)
        {
            string strSQL = "";  //取某一药房，某一张摆药单中的摆药数据的SQL语句
            string strWhere = "";  //取某一药房，某一张摆药单中的摆药数据的WHERE语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }
            //取WHERE语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutListForInvoiceNO.Where", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutListForInvoiceNO.Where字段!";
                return null;
            }

            try
            {
                strSQL = string.Format(strSQL + strWhere, drugDept, class3MeaningCode, state, invoiceNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            //根据SQL语句取摆药数据数组并返回数组
            return this.myGetApplyOut(strSQL);
        }


        /// <summary>
        /// 根据病历号获取处方信息
        /// </summary>
        /// <param name="drugDept"></param>
        /// <param name="class3MeaningCode"></param>
        /// <param name="state"></param>
        /// <param name="invoceNO"></param>
        /// <param name="drugerDete"></param>
        /// <returns></returns>
        public ArrayList QueryApplyOutListForCardNo(string drugDept, string class3MeaningCode, string state, string cardNo, DateTime operTime)
        {
            string strSQL = "";  //取某一药房，某一张摆药单中的摆药数据的SQL语句
            string strWhere = "";  //取某一药房，某一张摆药单中的摆药数据的WHERE语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetApplyOutList字段!";
                return null;
            }
            switch(state)
            {
                case "0":
                    if (this.GetSQL("Pharmacy.Item.GetApplyOutListForCardNO.Where0", ref strWhere) == -1)
                     {
                        this.Err = "没有找到Pharmacy.Item.GetApplyOutListForCardNO.Where0字段!";
                        return null;
                     }
                    break;
                case "1":
                    if (this.GetSQL("Pharmacy.Item.GetApplyOutListForCardNO.Where1", ref strWhere) == -1)
                    {
                        this.Err = "没有找到Pharmacy.Item.GetApplyOutListForCardNO.Where1字段!";
                        return null;
                    }
                    break;
                default:
                    if (this.GetSQL("Pharmacy.Item.GetApplyOutListForCardNO.Where1", ref strWhere) == -1)
                    {
                        this.Err = "没有找到Pharmacy.Item.GetApplyOutListForCardNO.Where1字段!";
                        return null;
                    }
                    break;
            }
            try
            {
                strSQL = string.Format(strSQL + strWhere, drugDept, class3MeaningCode, state, cardNo, operTime);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            //根据SQL语句取摆药数据数组并返回数组
            return this.myGetApplyOut(strSQL);

        }

        /// <summary>
        /// 门诊配药更新申请数据状态
        /// </summary>
        /// <param name="deptCode">库房编码</param>
        /// <param name="class3MenaingCode">出库分类</param>
        /// <param name="recipeNo">处方号</param>
        /// <param name="sequenceNo">处方内项目序号</param>
        /// <param name="state">处方状态</param>
        /// <param name="operID">配药人</param>
        /// <param name="drugedNum">配药数量</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int UpdateApplyOutStateForDruged(string deptCode, string class3MenaingCode, string recipeNo, int sequenceNo, string state, string operID, decimal drugedNum)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateApplyOutState.Druged", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateApplyOutState.Druged";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, deptCode, class3MenaingCode, recipeNo, sequenceNo, state, operID, drugedNum.ToString());
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.UpdateApplyOutState.Druged";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 门诊发药更新申请数据状态
        /// </summary>
        /// <param name="info">出库申请实体</param>
        /// <param name="state">处方状态</param>
        /// <param name="operID">发药人</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int UpdateApplyOutStateForSend(FS.HISFC.Models.Pharmacy.ApplyOut info, string state, string operID)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateApplyOutState.Send", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateApplyOutState.Send";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, info.StockDept.ID, info.SystemType, info.RecipeNO, info.SequenceNO.ToString(), state, info.Operation.ApproveOper.Dept.ID, operID, info.Operation.ApproveQty.ToString(), info.OutBillNO);
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.UpdateApplyOutState.Send";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 获取某科室所有未发药患者药品列表
        /// </summary>
        /// <param name="deptCode">科室编码</param>
        /// <returns></returns>
        public ArrayList QueryClinicUnSendList(string deptCode)
        {
            string strSqlSelect = "", strSqlWhere = "";
            if (this.GetSQL("Pharmacy.Item.GetApplyOutList", ref strSqlSelect) == -1)
            {
                return null;
            }
            if (this.GetSQL("Pharmacy.Item.GetList.UnSend", ref strSqlWhere) == -1)
            {
                return null;
            }
            try
            {
                strSqlSelect = strSqlSelect + strSqlWhere;
                strSqlSelect = string.Format(strSqlSelect, deptCode);
            }
            catch (Exception ex)
            {
                this.Err = "参数不正确" + ex.Message;
                return null;
            }

            ArrayList al = this.myGetApplyOut(strSqlSelect);

            return al;
        }
        #endregion

        #region 住院申请操作


        /// <summary>
        /// 查询住院患者是否有未确认的退药申请
        /// </summary>
        /// <param name="inpatientNO">患者住院流水号</param>
        /// <returns>成功 > 0 记录 0 没有记录 -1 错误</returns>
        public int QueryNoConfirmQuitApply(string inpatientNO)
        {
            string sql = string.Empty;

            int returnValue = this.GetSQL("Pharmacy.Item.QueryNoConfirmQuitApply.Select.1", ref sql);
            if (returnValue < 0)
            {
                this.Err = "没有找到SQL为Pharmacy.Item.QueryNoConfirmQuitApply.Select.1的SQL语句";

                return -1;
            }
            try
            {
                sql = string.Format(sql, inpatientNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;

                return -1;
            }

            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
        }


        /// <summary>
        /// 申请出库－－对费用公开的函数
        /// </summary>
        /// <param name="patient">患者信息实体</param>
        /// <param name="feeItem">患者费用信息实体</param>
        /// <param name="operDate">操作时间</param>
        /// <param name="isPreOut">是否预出库</param>
        /// <param name="applyDeptType">申请科室类型 0 科室 1 护理站</param>
        /// <param name="getStockDept">是否根据申请科室获取取药药房</param>
        /// <returns>0没有删除 1成功 -1失败</returns>
        public int ApplyOut(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem, DateTime operDate, bool isPreOut, string applyDeptType, bool getStockDept)
        {
            #region 函数执行操作 将FeeItemList对象转为出库申请对象，然后插入出库申请表
            // 执行操作：
            // 1、FeeItemList对象转为出库申请对象
            // 2、取药品的所属的摆药单
            // 3、插入摆药通知
            // 4、插入出库申请
            // 5、预扣库存
            #endregion

            FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();

            try
            {
                #region ApplyOut实体赋值

                applyOut.Item = (FS.HISFC.Models.Pharmacy.Item)feeItem.Clone().Item;           //药品实体
                applyOut.Item.PriceCollection.RetailPrice = feeItem.Item.Price;            //零售价
                applyOut.Item.MinUnit = feeItem.Item.PriceUnit;                             //最小单位＝记价单位

                #region 申请科室/发药药房获取

                if (applyDeptType == "0")                               //申请科室＝开方科室
                    applyOut.ApplyDept = feeItem.ExecOper.Dept;
                else                                                    //申请科室＝发送护士站
                    applyOut.ApplyDept = ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.NurseCell;

                applyOut.StockDept = feeItem.StockOper.Dept;            //发药科室＝医嘱药房
                if (getStockDept)
                {
                    string strErr = "";
                    FS.FrameWork.Models.NeuObject stockOjb = this.GetStockDeptByDeptCode(applyOut.ApplyDept.ID, applyOut.Item.Type.ID, applyOut.Item.ID, feeItem.Item.Qty, this.Trans, ref strErr);
                    if (stockOjb != null)
                    {
                        applyOut.StockDept.ID = stockOjb.ID;
                        applyOut.StockDept.Name = stockOjb.Name;
                    }
                    else
                    {
                        //this.Err = applyOut.ApplyDept.Name + "[" + applyOut.ApplyDept.ID + "]未维护取药药房";
                        this.Err = applyOut.ApplyDept.Name + "[" + applyOut.ApplyDept.ID + "] " + strErr;
                        return -1;
                    }
                }

                #endregion

                #region 库存判断

                //2011-03-14 by cube 停用、缺药标志的判断重新整理
                FS.HISFC.Models.Pharmacy.Item item = this.GetItem(feeItem.Item.ID);
                if (item == null)
                {
                    this.Err = "获取药品基本信息失败" + this.Err;
                    return -1;
                }
                if (item.ValidState != EnumValidState.Valid)
                {
                    this.Err = item.Name + "－ 药库已停用 不能进行发药收费！";
                    return -1;
                }
                FS.HISFC.Models.Pharmacy.Storage storage = this.GetStockInfoByDrugCode(applyOut.StockDept.ID, feeItem.Item.ID);
                if (storage == null || storage.Item.ID == "")
                {
                    this.Err = item.Name + "－ 在该药房不存在库存 无法进行发药收费！" + this.Err;
                    return -1;
                }
                if (storage.ValidState != EnumValidState.Valid)
                {
                    this.Err = item.Name + "－ 在药房已停用 不能进行发药收费！";
                    return -1;
                }
                if (storage.IsLackForInpatient)
                {
                    this.Err = item.Name + "－ 在药房已缺药 不能进行发药收费！";
                    return -1;
                }
                decimal validStoreQty = storage.StoreQty;
                if (isPreOut)
                {
                    validStoreQty = storage.StoreQty - storage.PreOutQty;
                }
                //对允许扣除负库存时 不进行此项判断
                if (!Item.MinusStore && storage.StoreQty < feeItem.Item.Qty)
                {
                    this.Err = item.Name + "－ 在药房库存不足以进行本次收费发药 不能收费！";
                    return -1;
                }
                //end by
                #endregion

                #region 批次信息赋值

                //applyOut.CompoundGroup = consManager.GetOrderGroup(operDate);
                //if (applyOut.CompoundGroup == null)
                //{
                //    applyOut.CompoundGroup = "4";
                //}
                //applyOut.CompoundGroup = applyOut.CompoundGroup + operDate.ToString("yyMMdd") + feeItem.Order.Combo.ID + "C";

                #endregion

                #region ApplyOut赋值

                //by cube 2011-08-03 购入价补充
                applyOut.Item.PriceCollection.PurchasePrice = item.PriceCollection.PurchasePrice;
                applyOut.Item.PriceCollection.WholeSalePrice = item.PriceCollection.WholeSalePrice;
                //end

                applyOut.SystemType = "Z1";                             //申请类型＝"Z1" 
                applyOut.Operation.ApplyOper.OperTime = operDate;       //申请时间＝操作时间

                //by cube 2010-12-29 于佛山南庄
                //医生站假借字段，西药天数存入付数字段，在此处理
                //天数*每天的量=总量 付数*每付的量=总量都必须满足才可以取消药品业务层的转换
                //if (item.SysClass.ID.ToString() == "PCC")
                //{
                //    applyOut.Days = feeItem.Days == 0 ? 1 : feeItem.Days;                       //草药付数
                //}
                //else
                //{
                //    applyOut.Days = 1;
                //}
                //end by
                applyOut.Days = feeItem.Days;

                applyOut.IsPreOut = isPreOut;                           //是否预扣库存
                applyOut.IsCharge = true;                               //是否收费
                applyOut.PatientNO = patient.ID;                        //患者住院流水号,传入参数
                applyOut.PatientDept = ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.Dept;//患者所在科室
                applyOut.DoseOnce = feeItem.Order.DoseOnce;             //每次剂量
                applyOut.Frequency = feeItem.Order.Frequency;           //频次
                applyOut.Usage = feeItem.Order.Usage;                   //用法

                applyOut.OrderType = feeItem.Order.OrderType; //医嘱类型

                applyOut.OrderNO = feeItem.Order.ID;                    //医嘱流水号
                applyOut.CombNO = feeItem.Order.Combo.ID;               //组合序号
                applyOut.ExecNO = feeItem.ExecOrder.ID;                     //医嘱执行单流水号
                applyOut.RecipeNO = feeItem.RecipeNO;                   //处方号
                applyOut.SequenceNO = feeItem.SequenceNO;               //处方内流水号
                applyOut.SendType = 2;                                  //发送类型1集中，2临时
                applyOut.State = "0";							        //出库申请状态:0申请,1摆药,2核准
                applyOut.ShowState = "0";

                #endregion

                //费用表中的数量是乘以付数以后的总数量,药品表中保存的是每付的量,在此转换.
                //applyOut.Operation.ApplyQty = feeItem.Item.Qty / applyOut.Days;
                applyOut.Operation.ApplyQty = feeItem.Item.Qty;

                applyOut.RecipeInfo = feeItem.RecipeOper;
                applyOut.IsBaby = feeItem.IsBaby;

                #endregion

                if (applyOut.RecipeNO == null || applyOut.RecipeNO == "")
                {
                    this.Err = "医嘱传入处方号为空值!";
                    return -1;
                }
            }
            catch (Exception ex)
            {
                this.Err = "将费用实体转换成出库申请实体时出错！" + ex.Message;
                return -1;
            }

            #region 摆药通知处理

            //插入摆药通知记录
            FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
            drugMessage.ApplyDept = applyOut.ApplyDept;      //科室或者病区
            drugMessage.DrugBillClass.ID = "P";             //摆药单分类编码：非医嘱摆药单 P
            drugMessage.DrugBillClass.Name = "非医嘱摆药单";//摆药单分类名称：非医嘱摆药单
            drugMessage.SendType = 2;                       //发送类型0全部,1-集中,2-临时
            drugMessage.SendFlag = 0;                       //状态0-通知,1-已摆
            drugMessage.StockDept = applyOut.StockDept;     //发药科室

            if (this.SetDrugMessage(drugMessage) != 1)
            {
                 return -1;
            }

            #endregion

            #region 出库申请 预扣库存操作

            //将分类编码存入出库申请表中
            applyOut.BillClassNO = "P";
            //插入出库申请表
            int parm = this.InsertApplyOut(applyOut);
            if (parm == -1) return parm;

            //预扣库存（加操作）
            if (isPreOut)
            {
                ////{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                parm = this.UpdateStockinfoPreOutNum(applyOut, applyOut.Operation.ApplyQty, applyOut.Days);
                if (parm == -1) return parm;
            }

            #endregion

            return 1;
        }

        /// <summary>
        /// 申请退库－－对费用子系统公开的函数
        /// </summary>
        /// <param name="patient">患者信息实体</param>
        /// <param name="feeItem">费用信息实体</param>
        /// <param name="operDate">操作时间</param>
        /// <param name="applyDeptType">申请科室类型 0 科室 1 护理站</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int ApplyOutReturn(FS.HISFC.Models.RADT.PatientInfo patient, FS.HISFC.Models.Fee.Inpatient.FeeItemList feeItem, DateTime operDate, string applyDeptType)
        {
            #region 执行操作
            // 将FeeItemList对象转为退库申请对象，然后插入出库申请表
            // 执行操作：
            // 1、FeeItemList对象转为退库申请对象
            // 2、插入摆药通知
            // 3、插入出库申请
            #endregion

            FS.HISFC.Models.Pharmacy.ApplyOut applyOut = null;
            //记不清当初为什么先通过执行挡流水号获取信息了 
            //测试时执行挡流水号重复会发生错误
            //applyOut = this.GetApplyOutByExecNO( feeItem.ExecOrder.ID );


            //未找到相应的申请记录或者申请记录已确认 插入新的申请
            if (applyOut == null || applyOut.ID == "" || applyOut.State != "0" || applyOut.BillClassNO != "R")
            {
                applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();

                #region ApplyOut实体赋值

                try
                {
                    decimal tempPrice = feeItem.Item.Price;

                    applyOut.Item = (FS.HISFC.Models.Pharmacy.Item)feeItem.Item;                 //药品实体
                    applyOut.Item.Price = tempPrice;
                    applyOut.Item.PriceCollection.RetailPrice = applyOut.Item.Price;                    //零售价
                    applyOut.Item.MinUnit = feeItem.Item.PriceUnit;                                     //最小单位＝记价单位

                    if (applyDeptType == "1")                   //申请科室为护理站
                    {
                        applyOut.ApplyDept = ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.NurseCell;
                    }
                    else                                       //如果是医嘱发生的费用,则申请科室＝患者科室,否则为开方科室
                    {

                        #region {915E3F34-C8D7-41af-A016-9D0FACDBF850}
                        //不是医嘱的费用，再做退药确认时插入APPLOUT表里的申请科室改成执行科室。
                        //applyOut.ApplyDept = feeItem.Order.ID == "" ? feeItem.RecipeOper.Dept : ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.Dept;
                        applyOut.ApplyDept = feeItem.Order.ID == "" ? feeItem.ExecOper.Dept : ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.Dept;
                        #endregion
                    }

                    //by cube 2011-08-03 赋值最新价格
                    FS.HISFC.Models.Pharmacy.Item item = this.GetItem(applyOut.Item.ID);
                    if (item != null)
                    {
                        applyOut.Item.PriceCollection.PurchasePrice = item.PriceCollection.PurchasePrice;
                        applyOut.Item.PriceCollection.WholeSalePrice = item.PriceCollection.WholeSalePrice;
                        applyOut.Item.PackUnit = item.PackUnit;
                    }
                    //end by

                    //退费时不能重新获取取药药房
                    applyOut.StockDept = feeItem.StockOper.Dept;                                     //发药科室＝医嘱药房
                    applyOut.SystemType = "Z2";                                                      //申请类型＝"Z2" ，住院退药申请
                    applyOut.Operation.ApplyOper.OperTime = operDate;                                //申请时间＝操作时间
                    applyOut.Days = feeItem.Order.HerbalQty == 0 ? 1 : feeItem.Order.HerbalQty;      //草药付数
                    applyOut.IsPreOut = false;                                                       //是否预扣库存
                    applyOut.IsCharge = true;                                                        //是否收费
                    applyOut.PatientNO = patient.ID;                                                 //患者住院流水号,传入参数
                    applyOut.PatientDept = ((FS.HISFC.Models.RADT.PatientInfo)feeItem.Patient).PVisit.PatientLocation.Dept;        //患者所在科室
                    applyOut.DoseOnce = feeItem.Order.DoseOnce;                                      //每次剂量
                    applyOut.Frequency = feeItem.Order.Frequency;                                    //频次
                    applyOut.Usage = feeItem.Order.Usage;                                            //用法

                    applyOut.OrderType = feeItem.Order.OrderType; //医嘱类型

                    applyOut.OrderNO = feeItem.Order.ID;                                             //医嘱流水号
                    applyOut.CombNO = feeItem.Order.Combo.ID;                                        //组合序号
                    applyOut.ExecNO = feeItem.ExecOrder.ID;                                              //医嘱执行单流水号
                    applyOut.RecipeNO = feeItem.RecipeNO;                                            //处方号
                    applyOut.SequenceNO = feeItem.SequenceNO;                                        //处方内流水号
                    applyOut.SendType = 2;                                                           //发送类型0全部，1集中，2临时
                    applyOut.OutBillNO = feeItem.SendSequence.ToString();                            //对应出库单的流水号
                    //退药申请 申请单据号
                    applyOut.BillNO = feeItem.User02;
                    applyOut.ShowState = "0";

                    applyOut.State = "0";

                    //费用表中的数量是乘以付数以后的总数量,药品表中保存的是每付的量,在此转换.
                    applyOut.Operation.ApplyQty = feeItem.Item.Qty / applyOut.Days;
                }
                catch (Exception ex)
                {
                    this.Err = "将费用实体转换成出库申请实体时出错！" + ex.Message;
                    return -1;
                }

                #endregion

                if (applyOut.OutBillNO == null || applyOut.OutBillNO == "")
                {
                    this.Err = "出库单流水号为空 无对应的出库记录 不能做退库申请";
                    return -1;
                }

                #region 出库申请处理

                //将分类编码存入出库申请表中，退药单"R"
                applyOut.BillClassNO = "R";

                //插入出库申请表
                int parm = this.InsertApplyOut(applyOut);
                if (parm != 1) return parm;

                #endregion

            }
            else
            {
                applyOut.Operation.ApplyQty = feeItem.Item.Qty / applyOut.Days + applyOut.Operation.ApplyQty;

                if (this.SetApplyOut(applyOut) != 1)
                    return -1;
            }

            #region 插入摆药通知记录

            FS.HISFC.Models.Pharmacy.DrugMessage drugMessage = new FS.HISFC.Models.Pharmacy.DrugMessage();
            drugMessage.ApplyDept = applyOut.ApplyDept;    //科室或者病区
            drugMessage.DrugBillClass.ID = "R";           //摆药单分类编码：退药单
            drugMessage.DrugBillClass.Name = "退药单";    //摆药单分类名称：退药单
            drugMessage.SendType = 2;                     //发送类型0全部,1-集中,2-临时
            drugMessage.SendFlag = 0;                     //状态0-通知,1-已摆
            drugMessage.StockDept = applyOut.StockDept;   //发药科室

            if (this.SetDrugMessage(drugMessage) != 1)
            {
                 return -1;
            }

            #endregion

            return 1;
        }

        #endregion

        #region 门诊申请操作

        /// <summary>
        /// 门诊收费调用的出库函数
        /// </summary>
        /// <param name="patient">患者信息实体</param>
        /// <param name="feeAl">费用信息数组</param>
        /// <param name="operDate">操作时间</param>
        /// <param name="isPreOut">是否预出库</param>
        /// <param name="isModify">是否门诊退改药</param>
        /// <param name="alConstant">不发申请信息 直接扣库存科室</param>
        /// <param name="drugSendInfo">处方调剂信息 发药药房+发药窗口</param>
        /// <returns>1 成功 －1 失败</returns>
        public int ApplyOut(FS.HISFC.Models.Registration.Register patient, ArrayList feeAl, DateTime operDate, bool isPreOut, bool isModify, ArrayList alConstant, out string drugSendInfo)
        {
            string feeWindow = "";
            drugSendInfo = "";
            //定义药房管理类
          
            if (alConstant == null)
            {
                alConstant = new ArrayList();
            }

            #region 收费窗口参数初始化

            if (isInitSendWindow)
            {
                feeWindow = feeWindowNO;
            }
            else
            {
                string strErr = "";
                ArrayList alWindow = FS.FrameWork.WinForms.Classes.Function.GetDefaultValue("Fee", "Window", out strErr);

                if (alWindow != null && alWindow.Count > 0)
                {
                    feeWindowNO = alWindow[0] as string;

                    feeWindow = feeWindowNO;
                }

                isInitSendWindow = true;
            }

            #endregion

            bool isSendApply = false;
            FS.HISFC.Models.Pharmacy.ApplyOut applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
            DateTime feeDate = System.DateTime.MinValue;
            foreach (FS.HISFC.Models.Fee.Outpatient.FeeItemList feeInfo in feeAl)
            {
                #region 申请明细表操作

                #region ApplyOut实体赋值
                applyOut = new FS.HISFC.Models.Pharmacy.ApplyOut();
                try
                {
                    //2011-03-14 by cube 停用、缺药标志的判断重新整理
                    FS.HISFC.Models.Pharmacy.Item item = this.GetItem(feeInfo.Item.ID);
                    if (item == null)
                    {
                        this.Err = "获取药品基本信息失败" + this.Err;
                        return -1;
                    }
                    if (item.ValidState != EnumValidState.Valid)
                    {
                        this.Err = item.Name + "－ 药库已停用 不能进行发药收费！";
                        return -1;
                    }
                    //判断替代药房
                    string deptCode = feeInfo.ExecOper.Dept.ID;
                    applyOut.DrugNO = "0";
                    if (!string.IsNullOrEmpty(feeInfo.UndrugComb.MedicalRecord))
                    {
                        applyOut.DrugNO = feeInfo.UndrugComb.MedicalRecord;
                        deptCode = feeInfo.UndrugComb.MedicalRecord;
                    }
                    FS.HISFC.Models.Pharmacy.Storage storage = this.GetStockInfoByDrugCode(deptCode, feeInfo.Item.ID);
                    if (storage == null || storage.Item.ID == "")
                    {
                        this.Err = item.Name + "－ 在该药房不存在库存 无法进行发药收费！" + this.Err;
                        return -1;
                    }
                    if (storage.ValidState != EnumValidState.Valid)
                    {
                        this.Err = item.Name + "－ 在药房已停用 不能进行发药收费！";
                        return -1;
                    }
                    if (storage.IsLack)
                    {
                        this.Err = item.Name + "－ 在药房已缺药 不能进行发药收费！";
                        return -1;
                    }
                    decimal validStoreQty = storage.StoreQty;
                    if (isPreOut)
                    {
                        validStoreQty = storage.StoreQty - storage.PreOutQty;
                    }
                    //对允许扣除负库存时 不进行此项判断
                    if (!Item.MinusStore && storage.StoreQty < feeInfo.Item.Qty)
                    {
                        this.Err = item.Name + "－ 在药房库存不足以进行本次收费发药 不能收费！";
                        return -1;
                    }
                    //end by

                    //by cube 2011-08-03 购入价补充
                    applyOut.Item.PriceCollection.PurchasePrice = item.PriceCollection.PurchasePrice;
                    applyOut.Item.PriceCollection.WholeSalePrice = item.PriceCollection.WholeSalePrice;
                    //end

                    applyOut.Item.MinUnit = item.MinUnit;			                            //最小单位
                    applyOut.Item.PackUnit = item.PackUnit;
                    applyOut.Item.PriceCollection.RetailPrice = feeInfo.Item.Price;			    //零售价
                    applyOut.Item.ID = feeInfo.Item.ID;					                            //药品编码
                    applyOut.Item.Name = feeInfo.Item.Name;				                            //药品名称
                    applyOut.Item.Type = item.Type;						                        //药品类别
                    applyOut.Item.Quality = ((FS.HISFC.Models.Pharmacy.Item)feeInfo.Item).Quality;	        //药品性质
                    applyOut.Item.Specs = feeInfo.Item.Specs;				                    //规格
                    applyOut.Item.PackQty = feeInfo.Item.PackQty;			                    //包装数量
                    applyOut.ApplyDept = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.Templet.Dept;			  //申请科室＝开方科室 
                    applyOut.StockDept = feeInfo.ExecOper.Dept;                             //发药药房＝执行科室

                    applyOut.SystemType = "M1";                                                 //申请类型＝"M1" 
                    applyOut.Operation.ApplyOper.OperTime = operDate;                           //申请时间＝操作时间

                    //by cube 2010-12-29 于佛山南庄
                    //医生站假借字段，西药天数存入付数字段，在此处理
                    //天数*每天的量=总量 付数*每付的量=总量都必须满足才可以取消药品业务层的转换
                    if (item.SysClass.ID.ToString() == "PCC")
                    {
                        applyOut.Days = feeInfo.Days == 0 ? 1 : feeInfo.Days;                       //草药付数
                    }
                    else
                    {
                        applyOut.Days = 1;
                    }
                    //end by

                    applyOut.IsPreOut = isPreOut;                                               //是否预扣库存
                    applyOut.IsCharge = true;                                                   //是否收费
                    applyOut.PatientNO = feeInfo.Patient.ID;                                    //患者门诊流水号
                    applyOut.PatientDept = ((FS.HISFC.Models.Registration.Register)feeInfo.Patient).DoctorInfo.Templet.Dept;           //患者挂号科室 
                    applyOut.DoseOnce = feeInfo.Order.DoseOnce;		                            //每次剂量
                    applyOut.Item.DoseUnit = feeInfo.Order.DoseUnit;			                //每次剂量单位
                    applyOut.Frequency.ID = feeInfo.Order.Frequency.ID;			                //频次编码
                    applyOut.Frequency.Name = feeInfo.Order.Frequency.Name;	                    //频次名称
                    applyOut.Usage = feeInfo.Order.Usage;			                            //用法
                    applyOut.Item.DosageForm = ((FS.HISFC.Models.Pharmacy.Item)feeInfo.Item).DosageForm;		  //剂型
                    applyOut.OrderNO = feeInfo.Order.ID;				                        //医嘱流水号
                    applyOut.CombNO = feeInfo.Order.Combo.ID;				                    //组合序号

                    //暂时使用执行档流水号 表示院注次数
                    applyOut.ExecNO = feeInfo.InjectCount.ToString();                           //院注次数
                    //有效性标记为 3 表示 退改药
                    if (isModify)
                    {
                        applyOut.ValidState = FS.HISFC.Models.Base.EnumValidState.Extend;
                    }

                    applyOut.RecipeNO = feeInfo.RecipeNO;			                            //处方号
                    applyOut.SequenceNO = feeInfo.SequenceNO;		                            //处方内流水号
                    applyOut.State = "0";							                            //出库申请状态:0申请,1摆药,2核准
                    //费用表中的数量是乘以付数以后的总数量,药品表中保存的是每付的量,在此转换.
                    applyOut.ShowState = "0";
                    applyOut.Operation.ApplyQty = feeInfo.Item.Qty / applyOut.Days;
                    feeDate = feeInfo.FeeOper.OperTime;
                }
                catch (Exception ex)
                {
                    this.Err = "将费用实体转换成出库申请实体时出错！" + ex.Message;
                    return -1;
                }

                #endregion

                #region 是否发生申请判断

                bool isApply = true;
                if (alConstant != null)
                {
                    foreach (FS.HISFC.Models.Base.Const cons in alConstant)
                    {
                        if (cons.ID == applyOut.ApplyDept.ID)
                        {
                            isApply = false;
                            break;
                        }
                    }
                }

                #endregion

                if (isApply)
                {
                    #region 申请信息发送

                    isSendApply = true;
                    //插入出库申请表
                    int parm = this.InsertApplyOut(applyOut);
                    if (parm == -1)
                    {
                        return parm;
                    }
                    if (parm == 0)
                    {
                        this.Err = feeInfo.Name + "未正确插入出库申请表";
                        return -1;
                    }
                    //预扣库存（加操作）
                    if (isPreOut)
                    {
                        //{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                        parm = this.UpdateStockinfoPreOutNum(applyOut, applyOut.Operation.ApplyQty, applyOut.Days);
                        if (parm == -1) return parm;
                    }

                    #endregion
                }
                else
                {
                    #region 直接出库

                    applyOut.Operation.ApproveOper.Dept = applyOut.StockDept;
                    applyOut.Operation.ApproveQty = applyOut.Operation.ApplyQty;
                    applyOut.DrugNO = "1";
                    applyOut.State = "2";
                    //#cube#if (this.Output(applyOut) != 1)
                    {
                        this.Err = "对" + feeInfo.ExecOper.Dept.Name + " 进行直接出库操作失败 \n" + this.Err;
                        return -1;
                    }

                    #endregion
                }

                #endregion
            }

            if (isSendApply)
            {
                #region 申请头表
                if (isModify)
                {
                    #region 退改药更新原记录 处方状态 退/改药标记
                    int parm = this.UpdateDrugRecipeModifyInfo(applyOut.StockDept.ID, applyOut.RecipeNO, "M1", "0", feeDate, isModify);
                    if (parm == -1)
                    {
                        return parm;
                    }
                    else if (parm == 0)
                    {
                        this.Err = "未正确找到需要更新的数据 可能数据已发生变化 ";
                        return -1;
                    }
                    #endregion
                }
                else
                {
                    #region 向调剂头表内插入数据
                    if (this.DrugRecipe(patient, feeAl, feeWindow, out drugSendInfo) == -1)
                    {
                        return -1;
                    }
                    #endregion
                }
                #endregion
            }

            return 1;
        }

        #endregion

        #region 申请作废
        /// <summary>
        /// 取消门诊发药申请
        /// 根据处方流水号，作废门诊发药申请
        /// </summary>
        /// <param name="recipeNo">处方号</param>
        /// <param name="sequenceNo">处方内项目流水号</param>
        /// <param name="isPreOut">是否预扣库存</param>
        /// <returns>正确1,没找到数据0,错误－1</returns>
        public int CancelApplyOutClinic(string recipeNo, int sequenceNo, bool isPreOut)
        {
            

            #region 作废申请明细信息
            string strSQL = "";
            if (sequenceNo != -1)
            {
                #region 作废一张处方内某条项目
                /*
				 *
			  UPDATE	PHA_COM_APPLYOUT  
				SET    	PHA_COM_APPLYOUT.VALID_STATE  = '{3}',			        --有效标记（0有效，1无效，2不摆药）
						PHA_COM_APPLYOUT.CANCEL_EMPL  = '{2}', 				--操作人
						PHA_COM_APPLYOUT.CANCEL_DATE   = SYSDATE 			--操作时间
				WHERE	PHA_COM_APPLYOUT.PARENT_CODE  = '000010'   
				AND		PHA_COM_APPLYOUT.CURRENT_CODE = '004004' 
				AND		PHA_COM_APPLYOUT.RECIPE_NO    = '{0}' 				--处方流水号
				AND		PHA_COM_APPLYOUT.SEQUENCE_NO  = {1}  				--处方内序号
				AND		PHA_COM_APPLYOUT.VALID_STATE <> '{3}'  				--有效标记（0有效，1无效，2不摆药） 
				AND     PHA_COM_APPLYOUT.APPLY_STATE in ('0','1') 
				*/
                //根据处方流水号和处方内序号，作废出库申请记录的Update语句
                if (this.GetSQL("Pharmacy.Item.CancelApplyOut.Clinic.SingleRecipe", ref strSQL) == -1)
                {
                    this.Err = "没有找到SQL语句Pharmacy.Item.CancelApplyOut.Clinic.SingleRecipe";
                    return -1;
                }
                try
                {
                    //"0"表示作废此申请
                    strSQL = string.Format(strSQL, recipeNo, sequenceNo.ToString(), this.Operator.ID, ((int)FS.HISFC.Models.Base.EnumValidState.Invalid).ToString());
                }
                catch
                {
                    this.Err = "传入参数不正确！Pharmacy.Item.CancelApplyOut";
                    return -1;
                }
                #endregion
            }
            else
            {
                #region 作废整张处方
                //根据处方流水号作废该处方的所有申请 门诊调用Update语句
                /*
                 *原Sql
                 UPDATE	 PHA_COM_APPLYOUT  
                 SET     PHA_COM_APPLYOUT.VALID_STATE  = '{2}',			        --有效标记（0有效，1无效，2不摆药）
                         PHA_COM_APPLYOUT.CANCEL_EMPL  = '{1}', 				--操作人
                         PHA_COM_APPLYOUT.CANCEL_DATE   = SYSDATE 			--操作时间
                WHERE	 PHA_COM_APPLYOUT.PARENT_CODE  = '000010'   
                AND		 PHA_COM_APPLYOUT.CURRENT_CODE = '004004' 
                AND		 PHA_COM_APPLYOUT.RECIPE_NO    = '{0}' 				--处方流水号
                AND		 PHA_COM_APPLYOUT.VALID_STATE <> '{2}'  				--有效标记（0有效，1无效，2不摆药） 
                AND      PHA_COM_APPLYOUT.APPLY_STATE = '0'
                应改为
                UPDATE	 PHA_COM_APPLYOUT  
                 SET     PHA_COM_APPLYOUT.VALID_STATE  = '{2}',			        --有效标记（0有效，1无效，2不摆药）
                         PHA_COM_APPLYOUT.CANCEL_EMPL  = '{1}', 				--操作人
                         PHA_COM_APPLYOUT.CANCEL_DATE   = SYSDATE 			--操作时间
                WHERE	 PHA_COM_APPLYOUT.PARENT_CODE  = '000010'   
                AND		 PHA_COM_APPLYOUT.CURRENT_CODE = '004004' 
                AND		 PHA_COM_APPLYOUT.RECIPE_NO    = '{0}' 				--处方流水号
                AND		 PHA_COM_APPLYOUT.VALID_STATE <> '{2}'  				--有效标记（0有效，1无效，2不摆药） 
                AND      PHA_COM_APPLYOUT.APPLY_STATE in('0','1')
                 * 
                */
                if (this.GetSQL("Pharmacy.Item.CancelApplyOut.Clinic", ref strSQL) == -1)
                {
                    this.Err = "没有找到SQL语句Pharmacy.Item.CancelApplyOut.Clinic";
                    return -1;
                }
                try
                {
                    //"0"表示作废此申请
                    strSQL = string.Format(strSQL, recipeNo, this.Operator.ID, ((int)FS.HISFC.Models.Base.EnumValidState.Invalid).ToString());
                }
                catch
                {
                    this.Err = "传入参数不正确！Pharmacy.Item.CancelApplyOut.Clinic";
                    return -1;
                }
                #endregion
            }

            //取消出库申请
            int parm = this.ExecNoQuery(strSQL);
            if (parm < 0)
            {
                return parm;
            }
            else if (parm == 0)
            {
                this.Err = "未正确找到需作废的数据 可能数据已发生变化";
                return parm;
            }
            #endregion


            //{22995EEE-0F07-4f0e-A130-AFC738AAE873}  先进行预扣库存处理
            //如果预扣库存,则在取消出库申请的时候,还回预扣的库存
            if (isPreOut)
            {
                if (sequenceNo == -1)
                {
                    #region 还整张处方预扣库存
                    //取摆药申请数据
                    ArrayList al = this.QueryApplyOut(recipeNo);
                    if (al == null) return -1;

                    //还回预扣库存
                    //取消摆药申请时预扣减少（负数），取消退药申请时不处理预扣库存（退药确认时处理）
                    foreach (FS.HISFC.Models.Pharmacy.ApplyOut applyOut in al)
                    {
                        if (applyOut.BillClassNO != "R")
                        {
                            //预扣库存处理 //{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                            if (this.UpdateStockinfoPreOutNum(applyOut, -applyOut.Operation.ApplyQty, applyOut.Days) == -1)
                            {
                                return -1;
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    #region 还处方内一条记录库存
                    //取摆药申请数据
                    FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.GetApplyOut(recipeNo, sequenceNo);
                    if (applyOut == null) return -1;

                    //还回预扣库存
                    //取消摆药申请时预扣减少（负数），取消退药申请时不处理预扣库存（退药确认时处理）
                    if (applyOut.BillClassNO != "R")
                    {
                        //预扣库存处理 //{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                        if (this.UpdateStockinfoPreOutNum(applyOut, -applyOut.Operation.ApplyQty, applyOut.Days) == -1)
                        {
                            return -1;
                        }
                    }
                    #endregion
                }
            }

            //{22995EEE-0F07-4f0e-A130-AFC738AAE873}  先进行预扣库存处理
            //作废处方调剂表
            parm = this.UpdateDrugRecipeValidState(recipeNo, "M1", FS.HISFC.Models.Base.EnumValidState.Invalid);
            if (parm < 0)
            {
                return parm;
            }
            else if (parm == 0)
            {
                this.Err = "该申请信息已发药 不能再次作废发药申请";
                this.ErrCode = "2";
                return 0;
            }

            return 1;
        }

        /// <summary>
        /// 取消出库申请
        /// 根据处方流水号和处方内序号，作废出库申请
        /// </summary>
        /// <param name="recipeNo">处方流水号</param>
        /// <param name="sequenceNo">处方内序号</param>
        /// <param name="isPreOut">是否预扣库存</param>
        /// <returns>正确1,没找到数据0,错误－1</returns>
        public int CancelApplyOut(string recipeNo, int sequenceNo, bool isPreOut)
        {
            int parm = this.UpdateApplyOutValidByRecipeNO(recipeNo, sequenceNo, false);
            if (parm < 1)
            {
                if (parm == 0)
                {
                    this.Err = "该条药品已发药或做过退费申请，不能退费";
                }

                return -1;
            }

            //如果预扣库存,则在取消出库申请的时候,还回预扣的库存
            if (isPreOut)
            {
                //取摆药申请数据
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.GetApplyOut(recipeNo, sequenceNo);
                if (applyOut == null)
                    return -1;

                //还回预扣库存  取消摆药申请时预扣减少（负数），取消退药申请时不处理预扣库存（退药确认时处理）
                if (applyOut.BillClassNO != "R")
                {
                    //预扣库存处理 //{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                    if (this.UpdateStockinfoPreOutNum(applyOut, -applyOut.Operation.ApplyQty, applyOut.Days) == -1)
                    {
                        return -1;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 撤销取消出库申请（取消申请的逆过程）
        /// 根据处方流水号和处方内序号，撤销作废出库申请
        /// </summary>
        /// <param name="recipeNo">处方流水号</param>
        /// <param name="sequenceNo">处方内序号</param>
        /// <param name="isPreOut">是否预扣库存</param>
        /// <returns>正确1,没找到数据0,错误－1</returns>
        public int UndoCancelApplyOut(string recipeNo, int sequenceNo, bool isPreOut)
        {
            //int parm = this.UpdateApplyOutValidByRecipeNO( recipeNo , sequenceNo , true );
            //if( parm != 1 )
            //    return parm;

            DrugStore drugStoreManager = new DrugStore();
            drugStoreManager.SetTrans(this.Trans);
            //获取摆药申请信息
            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.GetApplyOut(recipeNo, sequenceNo);
            if (applyOutTemp == null)
                return -1;

            int parm = this.UpdateApplyOutValidByID(applyOutTemp.ID, true);
            if (parm != 1)
                return parm;


            if (drugStoreManager.UpdateDrugMessage(applyOutTemp.StockDept.ID, applyOutTemp.ApplyDept.ID, applyOutTemp.BillClassNO, applyOutTemp.SendType, "0") != 1)
            {
                this.Err = "更新摆药通知记录发生错误" + drugStoreManager.Err;
                return -1;
            }

            //如果预扣库存,则在取消出库申请的时候,还回预扣的库存
            if (isPreOut)
            {
                //还回预扣库存 恢复摆药申请时预扣增加（正数）
                if (applyOutTemp.BillClassNO != "R")
                {
                    ////{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                    if (this.UpdateStockinfoPreOutNum(applyOutTemp, applyOutTemp.Operation.ApplyQty, applyOutTemp.Days) == -1)
                    {
                        return -1;
                    }
                }
            }
            return 1;
        }

        /// <summary>
        /// 作废出库申请信息
        /// </summary>
        /// <param name="orderExecNO">执行档流水号</param>
        /// <param name="isPreOut">是否预出库</param>
        /// <returns>成功返回受影响条数 失败返回-1</returns>
        public int CancelApplyOut(string orderExecNO, bool isPreOut)
        {
            //申请信息作废
            int parm = this.UpdateApplyOutValidByExecNO(orderExecNO, false);
            if (parm != 1)
                return parm;

            //如果预扣库存,则在取消出库申请的时候,还回预扣的库存
            if (isPreOut)
            {
                //取摆药申请数据
                FS.HISFC.Models.Pharmacy.ApplyOut applyOut = this.GetApplyOutByExecNO(orderExecNO);
                if (applyOut == null)
                    return -1;

                //还回预扣库存       //取消摆药申请时预扣减少（负数），取消退药申请时不处理预扣库存（退药确认时处理）
                if (applyOut.BillClassNO != "R")
                {
                    ////{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                    if (this.UpdateStockinfoPreOutNum(applyOut, -applyOut.Operation.ApplyQty, applyOut.Days) == -1)
                    {
                        return -1;
                    }
                }
            }

            return 1;
        }

        /// <summary>
        /// 撤销取消出库申请（取消申请的逆过程）
        /// 根据申请档流水号进行更新
        /// </summary>
        /// <param name="orderExecNO">执行档流水号</param>
        /// <param name="isPreOut">是否预扣库存</param>
        /// <returns>正确1,没找到数据0,错误－1</returns>
        public int UndoCancelApplyOut(string orderExecNO, bool isPreOut)
        {
            //申请信息置为有效
            int parm = this.UpdateApplyOutValidByExecNO(orderExecNO, true);
            if (parm != 1)
                return parm;

            //定义药房管理类
            DrugStore drugStoreManager = new DrugStore();
            drugStoreManager.SetTrans(this.Trans);
            FS.HISFC.Models.Pharmacy.ApplyOut applyOutTemp = this.GetApplyOutByExecNO(orderExecNO);
            if (applyOutTemp == null)
                return -1;

            if (drugStoreManager.UpdateDrugMessage(applyOutTemp.StockDept.ID, applyOutTemp.ApplyDept.ID, applyOutTemp.BillClassNO, applyOutTemp.SendType, "0") != 1)
            {
                this.Err = "更新摆药通知记录发生错误" + drugStoreManager.Err;
                return -1;
            }

            //如果预扣库存,则在取消出库申请的时候,还回预扣的库存
            if (isPreOut)
            {
                //还回预扣库存       //恢复摆药申请时预扣增加（正数），恢复退药申请时不处理预扣（退药确认时处理）
                if (applyOutTemp.BillClassNO != "R")
                {
                    //{9CBE5D4D-9FDB-4543-B7CA-8C07A67B41AF}
                    if (this.UpdateStockinfoPreOutNum(applyOutTemp, applyOutTemp.Operation.ApplyQty, applyOutTemp.Days) == -1)
                    {
                        return -1;
                    }
                }
            }
            return 1;
        }
        #endregion

        /// <summary>
        /// 根据申请科室 获取 取药药房
        /// </summary>
        /// <param name="deptCode">申请科室</param>
        /// <param name="drugType">药品类别</param>
        /// <param name="drugCode">申请药品编码</param>
        /// <param name="applyQty">申请数量</param>
        /// <param name="trans">事务</param>
        /// <returns>成功返回取药药房</returns>
        public FS.FrameWork.Models.NeuObject GetStockDeptByDeptCode(string deptCode, string drugType, string drugCode, decimal applyQty, System.Data.IDbTransaction trans, ref string strErr)
        {
            Constant phaConsManager = new Constant();
            if (trans != null)
            {
                phaConsManager.SetTrans(trans);
            }

            strErr = "";

            List<FS.FrameWork.Models.NeuObject> alStockDept = phaConsManager.GetRecipeDrugDept(deptCode, drugType);
            if (alStockDept == null || alStockDept.Count == 0)
            {
                strErr = "未设置取药药房";
                return null;
            }

            foreach (FS.FrameWork.Models.NeuObject stockDept in alStockDept)
            {
                decimal storeQty = 0;
                this.GetStorageNum(stockDept.ID.ToString(), drugCode, out storeQty);
                if (storeQty >= applyQty)
                {
                    return stockDept;
                }
            }

            strErr = "对应取药药房库存不足";
            return null;
        }
       
        /// <summary>
        /// 更新摆药申请处方号
        /// </summary>
        /// <param name="oldRecipeNo">旧处方号</param>
        /// <param name="oldSeqNo">旧处方内项目序号</param>
        /// <param name="newRecipeNo">新处方号</param>
        /// <param name="newSeqNo">新处方内项目许号</param>
        /// <returns>成功返回1 出错返回-1</returns>
        public int UpdateApplyOutRecipe(string oldRecipeNo, int oldSeqNo, string newRecipeNo, int newSeqNo)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateApplyOutRecipe", ref strSQL) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateApplyOutRecipe";
                return -1;
            }
            try
            {
                strSQL = string.Format(strSQL, oldRecipeNo, oldSeqNo.ToString(), newRecipeNo, newSeqNo.ToString());
            }
            catch
            {
                this.Err = "传入参数不正确！Pharmacy.Item.UpdateApplyOutRecipe";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 更新处方调剂头表PHA_STO_RECIPE的打印状态 lfhm
        /// </summary>
        /// <param name="state">更新的状态</param>
        /// <param name="invoiceNo">发票号</param>
        /// <param name="recipeNo">处方号</param>
        /// <returns></returns>
        public int UpdateDrugRecipePrintState(string state, string invoiceNo, string recipeNo)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateDrugRecipePrintState", ref strSql) == -1)
                return -1;
            strSql = string.Format(strSql, state, invoiceNo, recipeNo);
            return this.ExecNoQuery(strSql);
        }
        /// <summary>
        /// 更新出库申请表PHA_COM_APPLYOUT的打印状态
        /// </summary>
        /// <param name="appleState"></param>
        /// <param name="printState"></param>
        /// <param name="operCode"></param>
        /// <param name="applyNum"></param>
        /// <returns></returns>
        public int UpdateAppleOutPrintState(string appleState, string printState, string operCode, string applyNum)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.DrugStore.UpdateApplyOutPrintState", ref strSql) == -1)
                return -1;
            strSql = string.Format(strSql, appleState, printState, operCode, applyNum);
            return this.ExecNoQuery(strSql);
        }


        #region 集中发送
        /// <summary>
        /// 获取药品集中发送信息
        /// </summary>
        /// <param name="applyDeptNO">申请科室</param>
        /// <param name="info">如果发送，则返回发送信息</param>
        /// <returns>-1发生错误 0未集中发送 1已经集中发送</returns>
        public int GetDrugConcentratedSendInfo(string applyDeptNO, ref string info)
        {
            string curSQL = "";
            if (this.GetSQL("SOC.Pharmacy.DrugStore.ConcentratedSendInfo.Query", ref curSQL) == -1)
            {
                curSQL = @"
                            select m.send_oper_name || '(' || m.send_oper_code || ')' || ' 已于' ||
                                   to_char(m.send_oper_date, 'yyyy-mm-dd hh24:mi:ss') || '集中发送'
                              from pha_sto_msg m
                             where m.dept_code = '{0}'
                               and m.send_type = '1'
                               --and m.send_flag = '0'
                               --and m.billclass_code not in ('P', 'R')
                               and m.send_oper_date >= trunc(sysdate)
                               and rownum = 1
                    ";
                this.CacheSQL("SOC.Pharmacy.DrugStore.ConcentratedSendInfo.Query", curSQL);
            }
            try
            {
                curSQL = string.Format(curSQL, applyDeptNO);
            }
            catch (Exception ex)
            {
                info = ex.Message;
                return -1;
            }
            int param = this.ExecQuery(curSQL);
            if (param == -1)
            {
                info = this.Err;
                return -1;
            }
            else
            {
                if (this.Reader.Read())
                {
                    info = this.Reader[0].ToString();
                }
                else
                {
                    info = "本科室还没有集中发送";
                    return 0;
                }
            }
            return 1;
        }

        /// <summary>
        /// 删除集中发送信息
        /// </summary>
        /// <param name="applyDeptNO">申请科室</param>
        /// <returns></returns>
        public int DeleteDrugConcentratedSendInfo(string applyDeptNO)
        {
            string curSQL = "";
            if (this.GetSQL("SOC.Pharmacy.DrugStore.ConcentratedSendInfo.Delete", ref curSQL) == -1)
            {
                curSQL = @"
                            delete from pha_sto_msg m
                             where m.dept_code = '{0}'
                               and m.send_type = '1'
                               --and m.billclass_code not in ('P','R')
                               and m.send_oper_date < trunc(sysdate)
                                                ";
                this.CacheSQL("SOC.Pharmacy.DrugStore.ConcentratedSendInfo.Delete", curSQL);
            }
            try
            {
                curSQL = string.Format(curSQL, applyDeptNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(curSQL);

        }

        /// <summary>
        /// 更新发药申请的发送状态
        /// </summary>
        /// <param name="applyDeptNO">申请科室</param>
        /// <param name="sendType">发送状态</param>
        /// <param name="oldSendType">原发送状态</param>
        /// <param name="daySpan">数据有效时间天数</param>
        /// <returns></returns>
        public int UpdateApplyOutSendType(string applyDeptNO, string sendType, string oldSendType, int daySpan)
        {
            string curSQL = "";
            if (this.GetSQL("SOC.Pharmacy.DrugStore.ConcentratedSendInfo.UpdateApply", ref curSQL) == -1)
            {
                curSQL = @"
                           update pha_com_applyout a
                               set a.send_type = '{1}'
                             where a.dept_code = '{0}'
                               and a.apply_state = '0'
                               and a.send_type = '{2}'--临时发送更改为集中发送
                               --and a.billclass_code <> 'P'--非医嘱摆药不需要集中发送
                               --and a.class3_meaning_code = 'Z1'--住院退药为Z2，不集中发送
                               and a.apply_date > sysdate - {3}   
                                                ";
                this.CacheSQL("SOC.Pharmacy.DrugStore.ConcentratedSendInfo.UpdateApply", curSQL);
            }
            try
            {
                curSQL = string.Format(curSQL, applyDeptNO, sendType, oldSendType, daySpan.ToString());
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(curSQL);
        }

        /// <summary>
        /// 更新发药申请的发送状态
        /// </summary>
        /// <param name="applyNO">发药申请的唯一标识符</param>
        /// <param name="sendType">发送状态</param>
        /// <param name="oldSendType">原发送状态</param>
        /// <returns></returns>
        public int UpdateApplyOutSendType(string applyNO, string sendType, string oldSendType)
        {
            string curSQL = "";
            if (this.GetSQL("SOC.Pharmacy.DrugStore.ConcentratedSendInfo.UpdateOneApply", ref curSQL) == -1)
            {
                curSQL = @"
                           update pha_com_applyout a
                               set a.send_type = '{1}'
                             where a.apply_number = '{0}'
                               and a.apply_state = '0'
                               and a.send_type = '{2}'--4是紧急发送
                               --and a.billclass_code <> 'P'--非医嘱摆药不需要集中发送
                               --and a.class3_meaning_code = 'Z1'--住院退药为Z2，不集中发送
                                                ";
                this.CacheSQL("SOC.Pharmacy.DrugStore.ConcentratedSendInfo.UpdateOneApply", curSQL);
            }
            try
            {
                curSQL = string.Format(curSQL, applyNO, sendType, oldSendType);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(curSQL);
        }

        /// <summary>
        /// 在摆药通知档中插入集中发送信息
        /// </summary>
        /// <param name="applyDeptNO">申请科室</param>
        /// <param name="daySpan">发药申请数据有效天数</param>
        /// <returns></returns>
        public int InsertDrugConcentratedSendInfo(string applyDeptNO, int daySpan)
        {
            string curSQL = "";
            if (this.GetSQL("SOC.Pharmacy.DrugStore.ConcentratedSendInfo.Insert", ref curSQL) == -1)
            {
                curSQL = @"
                           insert into pha_sto_msg
                                select u.dept_code,
                                       (select d.dept_name from com_department d where d.dept_code = u.dept_code) dept_name,
                                       u.billclass_code,
                                       (select c.billclass_name from pha_sto_billclass c where c.billclass_code = u.billclass_code) billclass_name,
                                       u.send_type,
                                       sysdate send_dtime,
                                       '0' send_flag,
                                       u.drug_dept_code med_dept_code,
                                       '{0}' oper_code,
                                       '{1}' oper_name,
                                       sysdate oper_date,
                                       '{0}' send_oper_code,
                                       '{1}' send_oper_name,  
                                       sysdate  send_oper_date
                                from 
                                  (
                                  select distinct t.dept_code,
                                                  t.billclass_code,
                                                  t.send_type,
                                                  t.drug_dept_code
                                    from pha_com_applyout t
                                   where t.class3_meaning_code in ('Z1','Z2')
                                   and   t.apply_state = '0'
                                   and   t.send_type = '1'
                                   --and   t.billclass_code <> 'P'
                                   and   t.dept_code = '{3}'
                                   and   t.apply_date > sysdate - {2}
                                   ) u
                                                ";
                this.CacheSQL("SOC.Pharmacy.DrugStore.ConcentratedSendInfo.Insert", curSQL);
            }
            try
            {
                curSQL = string.Format(curSQL, this.Operator.ID, this.Operator.Name, daySpan.ToString(), applyDeptNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(curSQL);
        }

        /// <summary>
        /// 获得某一摆药台中全部摆药通知列表
        /// SendType=1集中，2临时
        /// 当SendType＝0时，显示全部类型的摆药通知。
        /// </summary>
        /// <param name="drugControl">摆药台</param>
        /// <returns>成功返回摆药通知数组 失败返回null</returns>
        public ArrayList QueryDrugMessageListAfterConcentratedSend(DrugControl drugControl, int daySpan)
        {
            //如果没有指定发送科室，则取全部发送科室的通知
            string strSQL = "";    //获得某一摆药台（摆药台中有科室信息）中全部摆药通知列表的SELECT语句

            if (this.GetSQL("SOC.Pharmacy.DrugStore.GetDrugMessageList.AfterConcentratedSend", ref strSQL) == -1)
            {
                strSQL = @"
				            SELECT  
						            PHA_STO_MSG.DEPT_CODE,                              --发送部门编码
						            PHA_STO_MSG.DEPT_NAME,                              --发送部门名称
						            PHA_STO_MSG.BILLCLASS_CODE,                         --摆药单分类代码
						            PHA_STO_MSG.BILLCLASS_NAME,                         --摆药单分类名称
						            PHA_STO_MSG.SEND_TYPE,                          --发送类型0-全部1-集中2-临时
						            PHA_STO_MSG.SEND_DTIME,                             --发送时间
						            PHA_STO_MSG.SEND_FLAG,                              --摆药标记0-通知1-已摆
						            PHA_STO_MSG.MED_DEPT_CODE,                          --取药科室
						            PHA_STO_MSG.OPER_CODE,                              --操作员
                        PHA_STO_MSG.OPER_NAME                               --操作员姓名
                    FROM  PHA_STO_MSG,PHA_STO_CONTROL
                    WHERE  PHA_STO_MSG.BILLCLASS_CODE= PHA_STO_CONTROL.BILLCLASS_CODE
                    AND  PHA_STO_MSG.MED_DEPT_CODE = PHA_STO_CONTROL.DEPT_CODE  
                    AND  (PHA_STO_MSG.SEND_TYPE    = PHA_STO_CONTROL.SEND_TYPE OR PHA_STO_CONTROL.SEND_TYPE = 0)
                    AND  PHA_STO_MSG.SEND_FLAG     = '0' 
                    AND  PHA_STO_CONTROL.CONTROL_CODE = '{0}'
                    AND  EXISTS(
                              SELECT *
                                FROM PHA_STO_MSG M
                               WHERE M.DEPT_CODE = PHA_STO_MSG.DEPT_CODE
                                 AND ((M.SEND_TYPE = '1'
                                 AND M.BILLCLASS_CODE NOT IN ('P', 'R')) or M.BILLCLASS_CODE IN ('P','R'))
                                 AND nvl(M.SEND_OPER_DATE,M.Send_Dtime) >= TRUNC(SYSDATE) - {1}
                    )
                    AND  EXISTS
                            (
                             SELECT *
                               FROM PHA_COM_APPLYOUT A
                              WHERE A.DRUG_DEPT_CODE = PHA_STO_MSG.MED_DEPT_CODE
                                AND A.DEPT_CODE = PHA_STO_MSG.DEPT_CODE
                                AND (A.SEND_TYPE = PHA_STO_MSG.SEND_TYPE or PHA_STO_MSG.SEND_TYPE='0' or A.SEND_TYPE='0')
                                AND A.BILLCLASS_CODE = PHA_STO_MSG.BILLCLASS_CODE
                                AND (A.DRUGED_BILL IS NULL OR A.DRUGED_BILL = '0')
                                AND A.APPLY_DATE > SYSDATE - {1}

                            )
				            ORDER BY PHA_STO_MSG.DEPT_CODE,PHA_STO_MSG.BILLCLASS_CODE,PHA_STO_MSG.OPER_DATE DESC
				
                ";

                this.CacheSQL("SOC.Pharmacy.DrugStore.GetDrugMessageList.AfterConcentratedSend", strSQL);
            }
            try
            {
                string[] strParm = { drugControl.ID, daySpan.ToString() };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|SOC.Pharmacy.DrugStore.GetDrugMessageList.AfterConcentratedSend";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

        /// <summary>
        /// 获得某一摆药台中全部摆药通知列表
        /// SendType=1集中，2临时
        /// 当SendType＝0时，显示全部类型的摆药通知。
        /// </summary>
        /// <param name="drugControl">摆药台</param>
        /// <returns>成功返回摆药通知数组 失败返回null</returns>
        public ArrayList QueryEmergencyDrugMessageList(DrugControl drugControl, int daySpan)
        {
            //如果没有指定发送科室，则取全部发送科室的通知
            string strSQL = "";    //获得某一摆药台（摆药台中有科室信息）中全部摆药通知列表的SELECT语句

            if (this.GetSQL("SOC.Pharmacy.DrugStore.GetEmergencyDrugMessageList.ByDrugControl", ref strSQL) == -1)
            {
                strSQL = @"
				            SELECT  
						            PHA_STO_MSG.DEPT_CODE,                              --发送部门编码
						            PHA_STO_MSG.DEPT_NAME,                              --发送部门名称
						            PHA_STO_MSG.BILLCLASS_CODE,                         --摆药单分类代码
						            PHA_STO_MSG.BILLCLASS_NAME,                         --摆药单分类名称
						            PHA_STO_MSG.SEND_TYPE,                          --发送类型0-全部1-集中2-临时
						            PHA_STO_MSG.SEND_DTIME,                             --发送时间
						            PHA_STO_MSG.SEND_FLAG,                              --摆药标记0-通知1-已摆
						            PHA_STO_MSG.MED_DEPT_CODE,                          --取药科室
						            PHA_STO_MSG.OPER_CODE,                              --操作员
                        PHA_STO_MSG.OPER_NAME                               --操作员姓名
                    FROM  PHA_STO_MSG,PHA_STO_CONTROL
                    WHERE  PHA_STO_MSG.BILLCLASS_CODE= PHA_STO_CONTROL.BILLCLASS_CODE
                    AND  PHA_STO_MSG.MED_DEPT_CODE = PHA_STO_CONTROL.DEPT_CODE  
                    AND  (PHA_STO_MSG.SEND_TYPE    = PHA_STO_CONTROL.SEND_TYPE OR PHA_STO_CONTROL.SEND_TYPE = 0)
                    AND  PHA_STO_MSG.SEND_FLAG     = '0' 
                    AND  PHA_STO_CONTROL.CONTROL_CODE = '{0}'
                    AND  PHA_STO_MSG.SEND_TYPE = '4'
                    AND  EXISTS
                            (
                             SELECT *
                               FROM PHA_COM_APPLYOUT A
                              WHERE A.DRUG_DEPT_CODE = PHA_STO_MSG.MED_DEPT_CODE
                                AND A.DEPT_CODE = PHA_STO_MSG.DEPT_CODE
                                AND (A.SEND_TYPE = PHA_STO_MSG.SEND_TYPE or PHA_STO_MSG.SEND_TYPE = '0' or A.SEND_TYPE = '0')
                                AND A.BILLCLASS_CODE = PHA_STO_MSG.BILLCLASS_CODE
                                AND (A.DRUGED_BILL IS NULL OR A.DRUGED_BILL = '0')
                                AND A.APPLY_DATE > SYSDATE - {1}
                            )
				    ORDER BY PHA_STO_MSG.DEPT_CODE,PHA_STO_MSG.BILLCLASS_CODE,PHA_STO_MSG.OPER_DATE DESC
				
                ";

                this.CacheSQL("SOC.Pharmacy.DrugStore.GetEmergencyDrugMessageList.ByDrugControl", strSQL);
            }
            try
            {
                string[] strParm = { drugControl.ID, daySpan.ToString() };
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|SOC.Pharmacy.DrugStore.GetEmergencyDrugMessageList.ByDrugControl";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

        /// <summary>
        /// 获得某一摆药台中全部摆药通知列表
        /// SendType=1集中，2临时
        /// 当SendType＝0时，显示全部类型的摆药通知。
        /// </summary>
        /// <param name="drugControl">摆药台</param>
        /// <param name="daySpan">发药申请数据的有效天数</param>
        /// <returns>成功返回摆药通知数组 失败返回null</returns>
        public ArrayList QueryDrugMessageList(DrugControl drugControl, int daySpan)
        {
            string strSQL = "";  

            if (this.GetSQL("SOC.Pharmacy.DrugStore.GetDrugMessageList.ByDrugControlAndTime", ref strSQL) == -1)
            {
                strSQL = @"
                            SELECT  
                                PHA_STO_MSG.DEPT_CODE,                              --发送部门编码
                                PHA_STO_MSG.DEPT_NAME,                              --发送部门名称
                                PHA_STO_MSG.BILLCLASS_CODE,                         --摆药单分类代码
                                PHA_STO_MSG.BILLCLASS_NAME,                         --摆药单分类名称
                                PHA_STO_MSG.SEND_TYPE,                          --发送类型0-全部1-集中2-临时
                                PHA_STO_MSG.SEND_DTIME,                             --发送时间
                                PHA_STO_MSG.SEND_FLAG,                              --摆药标记0-通知1-已摆
                                PHA_STO_MSG.MED_DEPT_CODE,                          --取药科室
                                PHA_STO_MSG.OPER_CODE,                              --操作员
                                PHA_STO_MSG.OPER_NAME                               --操作员姓名
                            FROM  PHA_STO_MSG,PHA_STO_CONTROL
                            WHERE  PHA_STO_MSG.BILLCLASS_CODE= PHA_STO_CONTROL.BILLCLASS_CODE
                            AND  PHA_STO_MSG.MED_DEPT_CODE = PHA_STO_CONTROL.DEPT_CODE  
                            AND  (PHA_STO_MSG.SEND_TYPE    = PHA_STO_CONTROL.SEND_TYPE OR PHA_STO_CONTROL.SEND_TYPE = 0)
                            AND  PHA_STO_MSG.SEND_FLAG     = '0' 
                            AND  PHA_STO_CONTROL.CONTROL_CODE = '{0}'
                            AND  EXISTS
                            (
                             SELECT *
                               FROM PHA_COM_APPLYOUT A
                              WHERE A.DRUG_DEPT_CODE = PHA_STO_MSG.MED_DEPT_CODE
                                AND A.DEPT_CODE = PHA_STO_MSG.DEPT_CODE
                                AND (A.SEND_TYPE = PHA_STO_MSG.SEND_TYPE or PHA_STO_MSG.SEND_TYPE = '0' or A.SEND_TYPE = '0')
                                AND A.BILLCLASS_CODE = PHA_STO_MSG.BILLCLASS_CODE
                                AND (A.DRUGED_BILL IS NULL OR A.DRUGED_BILL = '0')
                                AND A.APPLY_DATE > SYSDATE - {1}
                            )
				                    ORDER BY PHA_STO_MSG.DEPT_CODE,PHA_STO_MSG.BILLCLASS_CODE,PHA_STO_MSG.OPER_DATE DESC
            ";
                this.CacheSQL("SOC.Pharmacy.DrugStore.GetDrugMessageList.ByDrugControlAndTime", strSQL);
            }
            try
            {
                string[] strParm = { drugControl.ID, daySpan .ToString()};
                strSQL = string.Format(strSQL, strParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message + "|SOC.Pharmacy.DrugStore.GetDrugMessageList.ByDrugControlAndTime";
                return null;
            }
            return myGetDrugMessage(strSQL);
        }

        /// <summary>
        /// 获取病区集中发送情况
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryNurseDruged()
        {
            ArrayList alNurseDruged = new ArrayList();

            string strSQL = "";

            if (this.GetSQL("SOC.Pharmacy.DrugStore.GetNurseDruged", ref strSQL) == -1)
            {
                strSQL = @"
                                select aa.nurse_cell_code 科室编码,
        nurse_cell_name 科室名称,
        bb.发送时间,
        bb.针剂配药时间,
        bb.口服配药时间
   from (select distinct r.nurse_cell_code nurse_cell_code,
                         fun_get_dept_name(r.nurse_cell_code) nurse_cell_name
           from fin_ipr_inmaininfo r
          where r.in_state = 'I') aa
   left join (select distinct t.dept_code dept_code,
                (select max(dd.send_oper_date)
                   from pha_sto_msg dd
                  where dd.med_dept_code = t.drug_dept_code
                    and dd.dept_code = t.dept_code
                    and dd.send_type = '1' and dd.billclass_code in ('TL','DL') and dd.send_oper_date > trunc(sysdate)) 发送时间,
                 (select max(cc.druged_date)
                   from pha_com_applyout cc
                  where cc.drug_dept_code = t.drug_dept_code
                    and cc.dept_code = t.dept_code
                    and cc.send_type = '1' and cc.apply_state = '2' and cc.billclass_code = 'TL'  and cc.druged_date > trunc(sysdate)) 针剂配药时间,
                      (select max(cc.druged_date)
                   from pha_com_applyout cc
                  where cc.drug_dept_code = t.drug_dept_code
                    and cc.dept_code = t.dept_code
                    and cc.send_type = '1' and cc.apply_state = '2' and cc.billclass_code = 'DL'  and cc.druged_date > trunc(sysdate)) 口服配药时间
  from pha_com_applyout t
 where t.class3_meaning_code in ('Z1', 'Z2')
   and t.apply_date > trunc(sysdate)
   and t.drug_dept_code = '{0}'
   and t.send_type = '1'
 group by t.dept_code, t.send_type, t.drug_dept_code) bb on aa.nurse_cell_code =
                                                        bb.dept_code
  order by aa.nurse_cell_code
  
            ";
                strSQL = string.Format(strSQL,((FS.HISFC.Models.Base.Employee)this.Operator).Dept.ID);
            }
            if (this.ExecQuery(strSQL) == -1)
            {
                return null;
            }
            try
            {
                while (this.Reader.Read())
                {
                    FS.FrameWork.Models.NeuObject deptInfo = new FS.FrameWork.Models.NeuObject();
                    deptInfo.ID = this.Reader[0].ToString();
                    deptInfo.Name = this.Reader[1].ToString();
                    deptInfo.User01 = this.Reader[2].ToString();
                    deptInfo.User02 = this.Reader[3].ToString();
                    deptInfo.User03 = this.Reader[4].ToString();
                    alNurseDruged.Add(deptInfo);
                }
            }
            catch (Exception ex)
            {
                this.Reader.Close();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }
            return alNurseDruged;
        }
        #endregion


        #region 按单据发送
        /// <summary>
        /// 更新住院发药申请的单据号
        /// </summary>
        /// <param name="applyNO">申请唯一标示符</param>
        /// <param name="operTime">操作时间</param>
        /// <param name="drugBillNO">单据号</param>
        /// <returns></returns>
        public int UpdateApplyOutDrugBillNO(string applyNO, DateTime operTime, string drugBillNO)
        {
            string curSQL = "";
            if (this.GetSQL("SOC.Pharmacy.DrugStore.UpdateApplyOutDrugBillNO", ref curSQL) == -1)
            {
                curSQL = @"
                           update pha_com_applyout a
                               set a.druged_bill = '{3}',
                                   --a.apply_date = to_date('{1}','yyyy-MM-dd hh24:mi:ss'),
                                   --a.apply_opercode = '{2}',
                                   a.apply_billcode = '{3}'
                             where a.apply_number = '{0}'
                               and (a.druged_bill is null or a.druged_bill = '0')
                               and a.apply_state = '0'
                                                ";
                this.CacheSQL("SOC.Pharmacy.DrugStore.UpdateApplyOutDrugBillNO", curSQL);
            }
            try
            {
                curSQL = string.Format(curSQL, applyNO, operTime.ToString(), this.Operator.ID, drugBillNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(curSQL);
        }

        /// <summary>
        /// 更新住院发药申请的单据号
        /// </summary>
        /// <param name="applyDeptNO">申请科室</param>
        /// <param name="drugDeptNO">发药药房</param>
        /// <param name="drugBillClassNO">单据类型</param>
        /// <param name="daySpan">记录有效时间天数</param>
        /// <param name="operTime">操作时间</param>
        /// <param name="drugBillNO">单据号</param>
        /// <returns></returns>
        public int UpdateApplyOutDrugBillNO(string applyDeptNO, string drugDeptNO, string drugBillClassNO, int daySpan, DateTime operTime, string drugBillNO)
        {
            string curSQL = "";
            if (this.GetSQL("SOC.Pharmacy.DrugStore.UpdateApplyOutDrugBillNO", ref curSQL) == -1)
            {
                curSQL = @"
                           update pha_com_applyout a
                               set a.druged_bill = '{6}', 
                                   --a.apply_date = to_date('{4}','yyyy-MM-dd hh24:mi:ss'),
                                   --a.apply_opercode = '{5}',
                                   a.apply_billcode = '{6}'
                             where a.dept_code = '{0}'
                               and a.drug_dept_code = '{1}'
                               and a.billclass_code = '{2}'
                               and (a.druged_bill is null or a.druged_bill = '0')
                               and a.apply_state = '0'
                               and a.apply_date > sysdate - {3}
                                                ";
                this.CacheSQL("SOC.Pharmacy.DrugStore.UpdateApplyOutDrugBillNO", curSQL);
            }
            try
            {
                curSQL = string.Format(curSQL, applyDeptNO, drugDeptNO, drugBillClassNO, daySpan.ToString(), operTime.ToString(), this.Operator.ID, drugBillNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(curSQL);
        }
        #endregion
    }
}
