using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.SIInterface;
using System.Collections;
using FS.HISFC.Models.Fee.Item;
using FS.FrameWork.Function;

namespace GZSI.Controls
{
    class DownloadManager:FS.FrameWork.Management.Database 
    {
        /// <summary>
        /// 默认广州医保合同单位编码
        /// </summary>
        private static string gzybParamCode = string.Empty;

        /// <summary>
        /// 默认异地医保合同单位编码
        /// </summary>
        private static string ydybParamCode = string.Empty;

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        private static FS.HISFC.BizLogic.Manager.Constant conManager = null;

        /// <summary>
        /// 常数管理业务层
        /// </summary>
        public static FS.HISFC.BizLogic.Manager.Constant ConManager
        {
            get
            {
                if (conManager == null)
                {
                    conManager = new FS.HISFC.BizLogic.Manager.Constant();
                }

                return conManager;
            }
        }

        /// <summary>
        /// 常数缓存列表
        /// </summary>
        private static Dictionary<string, ArrayList> dicConList = null;

        /// <summary>
        /// 获取常数列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ArrayList GetConList(string type)
        {
            if (dicConList == null)
            {
                dicConList = new Dictionary<string, ArrayList>();
            }

            if (dicConList.ContainsKey(type))
            {
                return dicConList[type];
            }

            ArrayList alCon = ConManager.GetList(type);

            if (alCon != null)
            {
                dicConList.Add(type, alCon);
            }
            return alCon;
        }

        /// <summary>
        /// 下载医保项目
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int LoadSIItem(FS.HISFC.Models.SIInterface.Item item)
        {
            int iReturn = this.UpdateSIItem(item);
            if (iReturn == 0)
            {
                return InsertSIItem(item);
            }
            else
            {
                return iReturn;
            }
        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 下载异地医保项目
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int LoadYDSIItem(FS.HISFC.Models.SIInterface.Item item)
        {
            int iReturn = this.UpdateYDSIItem(item);
            if (iReturn == 0)
            {
                return InsertYDSIItem(item);
            }
            else
            {
                return iReturn;
            }
        }

        /// <summary>
        ///  增量下载医保项目
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int LoadSIItem(FS.HISFC.Models.SIInterface.Item item,ArrayList alPact)
        {
            int iReturn = this.UpdateSIItem(item);
            if (iReturn == 0)
            {
                foreach (FS.HISFC.Models.Base.Const con in alPact)
                {
                    if (-1 == InsertSIItem(item, con.ID))
                    return -1;

                }
                return 1;
            }
            else
            {
                return iReturn;
            }
        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        ///  增量下载异地医保项目
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int LoadYDSIItem(FS.HISFC.Models.SIInterface.Item item, ArrayList alYDPact)
        {
            int iReturn = this.UpdateYDSIItem(item);
            if (iReturn == 0)
            {
                foreach (FS.HISFC.Models.Base.Const con in alYDPact)
                {
                    if (-1 == InsertSIItem(item, con.ID))
                        return -1;

                }
                return 1;
            }
            else
            {
                return iReturn;
            }
        }

        string gzybParam = "";
        private string getGzybParam()
        {
            //FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            //System.Collections.ArrayList al=conMgr.QueryConstantList("gzyb");

            if (!string.IsNullOrEmpty(DownloadManager.gzybParamCode))
            {
                return DownloadManager.gzybParamCode;
            }
            else
            {
                System.Collections.ArrayList al = DownloadManager.GetConList("gzyb");

                foreach (FS.HISFC.Models.Base.Const con in al)
                {
                    if (con.Memo == "param")
                    {
                        DownloadManager.gzybParamCode = con.ID;
                        return con.ID;
                    }
                }
            }
            return "";
        }

        string gzydybParam = "";
        private string getGzydybParam()
        {
            //FS.HISFC.BizProcess.Integrate.Manager conMgr = new FS.HISFC.BizProcess.Integrate.Manager();
            //System.Collections.ArrayList al = conMgr.QueryConstantList("ydyb");

            if (!string.IsNullOrEmpty(DownloadManager.gzybParamCode))
            {
                return DownloadManager.ydybParamCode;
            }
            else
            {
                System.Collections.ArrayList al = DownloadManager.GetConList("ydyb");

                foreach (FS.HISFC.Models.Base.Const con in al)
                {
                    if (con.Memo == "param")
                    {
                        DownloadManager.ydybParamCode = con.ID;
                        return con.ID;
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// Update异地医保项目信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int UpdateYDSIItem(FS.HISFC.Models.SIInterface.Item item)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.InsertYDSIItem.Update", ref strSql) == -1)
                return -1;
            try
            {
                return this.ExecNoQuery(strSql, GetYDSIItemParm(item));
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        private string[] GetYDSIItemParm(FS.HISFC.Models.SIInterface.Item item)
        {
            gzydybParam = getGzydybParam();
            string[] SIItemParm = 
                {
                    item.ID,
                    item.SysClass,
                    item.Name, 
                    item.EnglishName,
                    item.Specs, 
                    item.DoseCode, 
                    item.SpellCode, 
                    item.FeeCode, 
                    item.ItemType,
                    item.ItemGrade,
                    item.Rate.ToString(),
                    this.Operator.ID,
                    gzydybParam
                };
            return SIItemParm;
        }
        
        /// <summary>
        /// Update医保项目信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int UpdateSIItem(FS.HISFC.Models.SIInterface.Item item)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.InsertSIItem.Update", ref strSql) == -1)
                return -1;
            try
            {
                return this.ExecNoQuery(strSql, GetSIItemParm(item));
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        private string[] GetSIItemParm(FS.HISFC.Models.SIInterface.Item item)
        {
            gzybParam = getGzybParam();
            string[] SIItemParm = 
                {
                    item.ID,
                    item.SysClass,
                    item.Name, 
                    item.EnglishName,
                    item.Specs, 
                    item.DoseCode, 
                    item.SpellCode, 
                    item.FeeCode, 
                    item.ItemType,
                    item.ItemGrade,
                    item.Rate.ToString(),
                    this.Operator.ID,
                    gzybParam
                };
            return SIItemParm;
        }

        private string[] GetSIItemParm(FS.HISFC.Models.SIInterface.Item item,string pactCode)
        {
            string[] SIItemParm = 
                {
                    item.ID,
                    item.SysClass,
                    item.Name, 
                    item.EnglishName,
                    item.Specs, 
                    item.DoseCode, 
                    item.SpellCode, 
                    item.FeeCode, 
                    item.ItemType,
                    item.ItemGrade,
                    item.Rate.ToString(),
                    this.Operator.ID,
                    pactCode
                };
            return SIItemParm;
        }

        /// <summary>
        /// {1AD4F611-F1AA-45b1-B05F-F314F32426D5} zhang-wx 2017-01-11 增加异地医保的自动对照功能
        /// 插入异地医保项目信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int InsertYDSIItem(FS.HISFC.Models.SIInterface.Item item)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.InsertSIItem.Insert", ref strSql) == -1)
                return -1;
            try
            {
                return this.ExecNoQuery(strSql, GetYDSIItemParm(item));
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>s
        /// 插入医保项目信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int InsertSIItem(FS.HISFC.Models.SIInterface.Item item)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.InsertSIItem.Insert", ref strSql) == -1)
                return -1;
            try
            {
                return this.ExecNoQuery(strSql, GetSIItemParm(item));
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>s
        /// 插入医保项目信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int InsertSIItem(FS.HISFC.Models.SIInterface.Item item,string pactCode)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.InsertSIItem.Insert", ref strSql) == -1)
                return -1;
            try
            {
                return this.ExecNoQuery(strSql, GetSIItemParm(item,pactCode));
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        /// <summary>
        /// 更新一条医保对照数据
        /// </summary>
        /// <param name="compareObj">医保实体</param>
        /// <returns></returns>
        public int UpdateCompareItem(Compare compareObj)
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.Interface.UpdateCompareItem.1", ref strSql) == -1)
                return -1;

            string[] objParm = GetCompareItemParm(compareObj);
            try
            {
                return this.ExecNoQuery(strSql, objParm);
            }
            catch (Exception ex)
            {
                this.ErrCode = ex.Message;
                this.Err = ex.Message;
                return -1;
            }
        }

        private string[] GetCompareItemParm(Compare obj)
        {
            string[] compareItemParm = 
                {
                    obj.CenterItem.PactCode, 
                    obj.HisCode, 
                    obj.CenterItem.ID,
                    obj.CenterItem.SysClass,
                    obj.CenterItem.Name,
                    obj.CenterItem.EnglishName,
                    obj.CenterItem.Specs,
                    obj.CenterItem.DoseCode, 
                    obj.CenterItem.SpellCode,
                    obj.CenterItem.FeeCode,
                    obj.CenterItem.ItemType, 
                    obj.CenterItem.ItemGrade,
                    obj.CenterItem.Rate.ToString(),
                    obj.CenterItem.Price.ToString(),
                    obj.CenterItem.Memo,
                    obj.SpellCode.SpellCode,
                    obj.SpellCode.WBCode, 
                    obj.SpellCode.UserCode,
                    obj.Specs,
                    obj.Price.ToString(), 
                    obj.DoseCode, 
                    obj.CenterItem.OperCode,
                    obj.Name, 
                    obj.RegularName
                };
            return compareItemParm;
        }

        /// <summary>
        /// 根据自定义编码获得非药品信息
        /// </summary>
        /// <param name="userCode">项目自定义码</param>
        /// <returns>成功返回非药品项目实体 失败返回null</returns>
        public System.Collections.ArrayList GetItemsByUserCode(string userCode)
        {
            string sql = null;//SQL语句

            //取SELECT语句
            if (this.Sql.GetSql("Fee.Item.Info.UserCode", ref sql) == -1)
            {
                this.Err = "没有找到Fee.Item.UserCode字段!";

                return null;
            }
            //格式化SQL语句
            try
            {
                sql = string.Format(sql, userCode);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return null;
            }

            //根据SQL语句取非药品类数组并返回数组
            ArrayList items = this.GetItemsBySql(sql);

            return items;
        }

        /// <summary>
        /// 获得未对照的非药品信息
        /// </summary>
        /// <param name="pactCode">合同单位代码</param>
        /// <returns>成功返回非药品项目实体 失败返回null</returns>
        public System.Collections.ArrayList GetItemsForAddCompare(string pactCode)
        {
            string sql = null;//SQL语句

            //取SELECT语句
            if (this.Sql.GetSql("Fee.Item.Info.GetItemsForAddCompare", ref sql) == -1)
            {
                #region 默认sql
                sql = @"select item_code,  --编码
                                item_name,  --名称
                                sys_class, --系统类别
                                fee_code,  --最小费用代码 
                                input_code,  -- 输入码
                                spell_code,  --拼音码
                                wb_code,  -- 五笔码
                                gb_code,  -- 国家编码
                                international_code,--国际编码 
                                unit_price,  --三甲价
                                stock_unit, --单位
                                emerg_scale,   -- 急诊加成比例
                                family_plane, -- 计划生育标记 
                                special_item, --特定诊疗项目
                                item_grade,   --甲乙类标志
                                confirm_flag, --确认标志 1 需要确认 0 不需要确认 
                                valid_state,   --有效性标识 0 在用 1 停用 2 废弃    
                                specs, --规格
                                exedept_code, --执行科室
                                facility_no,   --设备编号 用 | 区分 
                                default_sample, --默认检查部位或标本
                                operate_code,   -- 手术编码 
                                operate_kind,   -- 手术分类
                                operate_type,   --手术规模 
                                collate_flag,   --是否有物资项目与之对照(1有，0没有) 
                                mark ,    --备注     
                                UNIT_PRICE1, --儿童价
                                UNIT_PRICE2, --特诊价
                                SPECIAL_FLAG , --省限制
                                SPECIAL_FLAG1 ,--市限制 
                                SPECIAL_FLAG2 , --自费项目
                                SPECIAL_FLAG3 ,--特殊标识
                                SPECIAL_FLAG4 ,--特殊标识
                                UNIT_PRICE3,  --单价2
                                UNIT_PRICE4  ,--单价2    
                                DISEASE_CLASS, -- 疾病分类  
                                SPECIAL_DEPT , -- 专科名称  
                                MARK1,  --病史及检查
                                MARK2,--检查要求 
                                MARK3, --  注意事项      
                                CONSENT_FLAG ,
                                MARK4,    --  检查申请单名称  
                                NEEDBESPEAK,
                                      m.item_area ,
                                       m.item_noarea,
                                            unitflag  ,    --单位标识(0,明细; 1,组套) 
                                APPLICABILITYAREA
                                FROM fin_com_undruginfo m
                                WHERE   m.input_code is not null 		
				                        and m.unitflag=0
		                            and not exists (select 1 from fin_com_compare c where c.pact_code='{0}' and c.his_user_code= m.input_code and c.his_code=m.item_code )";
                #endregion
            }
            //格式化SQL语句
            try
            {
                sql = string.Format(sql, pactCode);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return null;
            }

            //根据SQL语句取非药品类数组并返回数组
            ArrayList items = this.GetItemsBySql(sql);

            return items;
        }

        /// <summary>
        /// 取非药品基本信息数组
        /// </summary>
        /// <param name="sql">当前Sql语句</param>
        /// <returns>成功返回非药品数组 失败返回null</returns>
        private System.Collections.ArrayList GetItemsBySql(string sql)
        {
            ArrayList items = new ArrayList(); //用于返回非药品信息的数组

            //执行当前Sql语句
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = this.Sql.Err;

                return null;
            }

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Item.Undrug item = new Undrug();

                    item.ID = this.Reader[0].ToString();//非药品编码 
                    item.Name = this.Reader[1].ToString(); //非药品名称 
                    item.SysClass.ID = this.Reader[2].ToString(); //系统类别
                    item.MinFee.ID = this.Reader[3].ToString();  //最小费用代码 
                    item.UserCode = this.Reader[4].ToString(); //输入码
                    item.SpellCode = this.Reader[5].ToString(); //拼音码
                    item.WBCode = this.Reader[6].ToString();    //五笔码
                    item.GBCode = this.Reader[7].ToString();    //国家编码
                    item.NationCode = this.Reader[8].ToString();//国际编码
                    item.Price = NConvert.ToDecimal(this.Reader[9].ToString()); //默认价
                    item.PriceUnit = this.Reader[10].ToString();  //计价单位
                    item.FTRate.EMCRate = NConvert.ToDecimal(this.Reader[11].ToString()); // 急诊加成比例
                    item.IsFamilyPlanning = NConvert.ToBoolean(this.Reader[12].ToString()); // 计划生育标记 
                    item.User01 = this.Reader[13].ToString(); //特定诊疗项目
                    item.Grade = this.Reader[14].ToString();//甲乙类标志
                    item.IsNeedConfirm = NConvert.ToBoolean(this.Reader[15].ToString());//确认标志 1 需要确认 0 不需要确认
                    item.ValidState = this.Reader[16].ToString(); //有效性标识 在用 1 停用 0 废弃 2   
                    item.Specs = this.Reader[17].ToString(); //规格
                    item.ExecDept = this.Reader[18].ToString();//执行科室
                    item.MachineNO = this.Reader[19].ToString(); //设备编号 用 | 区分 
                    item.CheckBody = this.Reader[20].ToString(); //默认检查部位或标本
                    item.OperationInfo.ID = this.Reader[21].ToString(); // 手术编码 
                    item.OperationType.ID = this.Reader[22].ToString(); // 手术分类
                    item.OperationScale.ID = this.Reader[23].ToString(); //手术规模 
                    item.IsCompareToMaterial = NConvert.ToBoolean(this.Reader[24].ToString());//是否有物资项目与之对照(1有，0没有) 
                    item.Memo = this.Reader[25].ToString(); //备注  
                    item.ChildPrice = NConvert.ToDecimal(this.Reader[26].ToString()); //儿童价
                    item.SpecialPrice = NConvert.ToDecimal(this.Reader[27].ToString()); //特诊价
                    item.SpecialFlag = this.Reader[28].ToString(); //省限制
                    item.SpecialFlag1 = this.Reader[29].ToString(); //市限制
                    item.SpecialFlag2 = this.Reader[30].ToString(); //自费项目
                    item.SpecialFlag3 = this.Reader[31].ToString();// 特殊检查
                    item.SpecialFlag4 = this.Reader[32].ToString();// 备用		
                    item.DiseaseType.ID = this.Reader[35].ToString(); //疾病分类
                    item.SpecialDept.ID = this.Reader[36].ToString();  //专科名称
                    item.MedicalRecord = this.Reader[37].ToString(); //  --病史及检查
                    item.CheckRequest = this.Reader[38].ToString();//--检查要求
                    item.Notice = this.Reader[39].ToString();//--  注意事项  
                    item.IsConsent = NConvert.ToBoolean(this.Reader[40].ToString());
                    item.CheckApplyDept = this.Reader[41].ToString();//检查申请单名称
                    item.IsNeedBespeak = NConvert.ToBoolean(this.Reader[42].ToString());//是否需要预约
                    item.ItemArea = this.Reader[43].ToString();//项目范围
                    item.ItemException = this.Reader[44].ToString();//项目约束
                    item.UnitFlag = this.Reader[45].ToString();// []单位标识

                    items.Add(item);
                }//循环结束

                //关闭Reader
                this.Reader.Close();

                return items;
            }
            catch (Exception e)
            {
                this.Err = "获得非药品基本信息出错！" + e.Message;
                this.WriteErr();

                //如果还没有关闭Reader 关闭之
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                items = null;

                return null;
            }
        }

        /// <summary>
        /// 根据学名编码获取药品项目信息
        /// </summary>
        /// <param name="formalCode">项目学名编码</param>
        /// <returns>成功返回药品项目实体 失败返回null</returns>
        public FS.HISFC.Models.Pharmacy.Item GetItemByFormalCode(string formalCode)
        {
            FS.HISFC.Models.Pharmacy.Item Item;
            List<FS.HISFC.Models.Pharmacy.Item> alItem = new List<FS.HISFC.Models.Pharmacy.Item>();
            string strSelect = "";  //获得药品信息的SELECT语句
            string strWhere = "";  //根据药品编码获得某一药品信息的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.Sql.GetSql("Pharmacy.Item.GetItem.Where.FormalCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetItem.Where.FormalCode字段!";
                return null;
            }

            try
            {
                strWhere = string.Format(strWhere, formalCode);
            }
            catch
            {
                return null;
            }

            //根据SQL语句取药品类数组并返回数组中的首条记录
            try
            {
                alItem = this.myGetItem(strSelect + " " + strWhere);
                Item = (FS.HISFC.Models.Pharmacy.Item)alItem[0];
                return Item;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 根据学名编码获取药品项目信息(包括停用)
        /// </summary>
        /// <param name="formalCode">项目学名编码</param>
        /// <returns>成功返回药品项目实体 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> GetArrayItemByFormalCode(string formalCode)
        {

            List<FS.HISFC.Models.Pharmacy.Item> alItem = new List<FS.HISFC.Models.Pharmacy.Item>();
            string strSelect = "";  //获得药品信息的SELECT语句
            string strWhere = "";  //根据药品编码获得某一药品信息的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.Sql.GetSql("Pharmacy.Item.GetItem.Where.FormalCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetItem.Where.FormalCode字段!";
                return null;
            }

            try
            {
                strWhere = string.Format(strWhere, formalCode);
            }
            catch
            {
                return null;
            }

            //根据SQL语句取药品类数组并返回数组中的首条记录
            try
            {
                alItem = this.myGetItem(strSelect + " " + strWhere);

                return alItem;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 根据自定义码获取药品项目信息(包括停用)
        /// </summary>
        /// <param name="userCode">药品自定义码</param>
        /// <returns>成功返回药品项目实体 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> GetArrayItemByUserCode(string userCode)
        {

            List<FS.HISFC.Models.Pharmacy.Item> alItem = new List<FS.HISFC.Models.Pharmacy.Item>();
            string strSelect = "";  //获得药品信息的SELECT语句
            string strWhere = "";  //根据药品编码获得某一药品信息的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.Sql.GetSql("Pharmacy.Item.GetItem.Where.UserCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetItem.Where.UserCode字段!";
                return null;
            }

            try
            {
                strWhere = string.Format(strWhere, userCode);
            }
            catch
            {
                return null;
            }

            //根据SQL语句取药品类数组并返回数组中的首条记录
            try
            {
                alItem = this.myGetItem(strSelect + " " + strWhere);

                return alItem;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取未对照的药品项目信息
        /// </summary>
        /// <param name="pactCode">合同单位代码</param>
        /// <returns>成功返回药品项目实体 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> GetMedItemByForAddCompare(string pactCode)
        {
            FS.HISFC.Models.Pharmacy.Item Item;
            List<FS.HISFC.Models.Pharmacy.Item> alItem = new List<FS.HISFC.Models.Pharmacy.Item>();
            string strSelect = "";  //获得药品信息的SELECT语句
            string strWhere = "";  //根据药品编码获得某一药品信息的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.Sql.GetSql("Pharmacy.Item.GetItem.Where.GetItemsForAddCompare", ref strWhere) == -1)
            {
                strWhere = @" where (pha_com_baseinfo.custom_code is not null)
                                and not exists (select 1 from fin_com_compare c 
                                                 where c.pact_code='{0}' 
                                                   and c.his_user_code= pha_com_baseinfo.custom_code 
                                                   and c.his_code=pha_com_baseinfo.drug_code )";
            }

            try
            {
                strWhere = string.Format(strWhere, pactCode);
            }
            catch
            {
                return null;
            }

            //根据SQL语句取药品类数组并返回数组中的首条记录
            try
            {
                alItem = this.myGetItem(strSelect + " " + strWhere);
                return alItem;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 取药品基本信息列表，可能是一条或者多条药品记录
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>成功返回药品对象数组 失败返回null</returns>
        private List<FS.HISFC.Models.Pharmacy.Item> myGetItem(string SQLString)
        {
            List<FS.HISFC.Models.Pharmacy.Item> al = new List<FS.HISFC.Models.Pharmacy.Item>();
            FS.HISFC.Models.Pharmacy.Item Item; //返回数组中的药品信息类

            try
            {
                this.ExecQuery(SQLString);
                int i = 0;
                while (this.Reader.Read())
                {
                    i++;
                    Item = new FS.HISFC.Models.Pharmacy.Item();
                    #region "接口说明"
                    //  0 药品编码        1 商品名称        2 拼音码          3 五笔码          4 自定义码 
                    //  5 包装数量        6 规格            7 系统类别编码    8 最小费用代码    9 药品通用名     
                    // 10 通用名拼音码   11 通用名五笔码   12 学名           13 学名拼音码     14 学名五笔码     
                    // 15 别名           16 英文商品名     17 英文通用名     18 英文别名       19 国际编码
                    // 20 国家编码       21 包装单位       22 最小单位       23 基本剂量       24 剂量单位       
                    // 25 剂型编码       26 药品类别编码   27 药品性质编码   28 零售价	        29 批发价         
                    // 30 购入价         31 最高零售价     32 药理作用(一级) 33 储藏条件       34 使用方法       
                    // 35 一次用量       36 频次		    37 生产厂家编码   38 批准文号       39 注册商标       
                    // 40 价格形式编码   41 是否停用       42 是否自制       43 是否GMP        44 是否OTC（处方药） 
                    // 45 是否新药       46 是否缺药       47 是否大屏幕显示 48 是否附材       49 注意事项       
                    // 50 药品等级       51 条形码         52 产地           53 最新供货公司   54 有效成份       
                    // 55 中药执行标准   56 药品简介       57 药品说明书内容 58 二级药理作用   59 三级药理作用   
                    // 60 是否是招标用药 61 中标价         62 采购合同编号   63 采购开始周期   64 采购结束周期   
                    // 65 采购单位编码   66 备注           67 操作员代码     68 操作时间       69 别名拼音码     
                    // 70 别名五笔码     71 别名自定义码   72 通用名自定义码 73 学名自定义码   74 是否需要试敏
                    // 75 省限制         76市限制          77自费项目        78特殊标记        79 特殊标记
                    // 80 旧系统药品编码 81数据变动类型	   82数据变动时间	 83数据变动原因
                    #endregion
                    Item.ID = this.Reader[0].ToString();
                    Item.Name = this.Reader[1].ToString();
                    Item.SpellCode = this.Reader[2].ToString();
                    Item.WBCode = this.Reader[3].ToString();
                    Item.UserCode = this.Reader[4].ToString();
                    Item.PackQty = NConvert.ToDecimal(this.Reader[5].ToString());
                    Item.Specs = this.Reader[6].ToString();
                    Item.SysClass.ID = this.Reader[7].ToString();
                    Item.MinFee.ID = this.Reader[8].ToString();
                    Item.NameCollection.RegularName = this.Reader[9].ToString();
                    Item.NameCollection.RegularSpell.SpellCode = this.Reader[10].ToString();
                    Item.NameCollection.RegularSpell.WBCode = this.Reader[11].ToString();
                    Item.NameCollection.FormalName = this.Reader[12].ToString();
                    Item.NameCollection.FormalSpell.SpellCode = this.Reader[13].ToString();
                    Item.NameCollection.FormalSpell.WBCode = this.Reader[14].ToString();
                    Item.NameCollection.OtherName = this.Reader[15].ToString();
                    Item.NameCollection.EnglishName = this.Reader[16].ToString();
                    Item.NameCollection.EnglishRegularName = this.Reader[17].ToString();
                    Item.NameCollection.EnglishOtherName = this.Reader[18].ToString();
                    Item.NameCollection.InternationalCode = this.Reader[19].ToString();
                    Item.NameCollection.GbCode = this.Reader[20].ToString();
                    Item.PackUnit = this.Reader[21].ToString();
                    Item.MinUnit = this.Reader[22].ToString();
                    Item.BaseDose = NConvert.ToDecimal(this.Reader[23].ToString());
                    Item.DoseUnit = this.Reader[24].ToString();
                    Item.DosageForm.ID = this.Reader[25].ToString();
                    Item.Type.ID = this.Reader[26].ToString();
                    Item.Quality.ID = this.Reader[27].ToString();
                    Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[28].ToString());
                    Item.PriceCollection.WholeSalePrice = NConvert.ToDecimal(this.Reader[29].ToString());
                    Item.PriceCollection.PurchasePrice = NConvert.ToDecimal(this.Reader[30].ToString());
                    Item.PriceCollection.TopRetailPrice = NConvert.ToDecimal(this.Reader[31].ToString());
                    Item.PhyFunction1.ID = this.Reader[32].ToString();
                    Item.Product.StoreCondition = this.Reader[33].ToString();
                    Item.Usage.ID = this.Reader[34].ToString();
                    Item.OnceDose = NConvert.ToDecimal(this.Reader[35].ToString());
                    Item.Frequency.ID = this.Reader[36].ToString();
                    Item.Product.Producer.ID = this.Reader[37].ToString();
                    Item.Product.ApprovalInfo = this.Reader[38].ToString();
                    Item.Product.Label = this.Reader[39].ToString();
                    Item.PriceCollection.PriceForm.ID = this.Reader[40].ToString();

                    //有效性 1 有效 0 无效 2 废弃
                    Item.ValidState = (FS.HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[41]));
                    //Item.IsStop = NConvert.ToBoolean( this.Reader[ 41 ].ToString( ) );
                    Item.IsValid = !Item.IsStop;
                    //if (Item.IsStop)
                    //    Item.ValidState = "0";
                    //else
                    //    Item.ValidState = "1";

                    Item.Product.IsSelfMade = NConvert.ToBoolean(this.Reader[42].ToString());
                    Item.IsGMP = NConvert.ToBoolean(this.Reader[43].ToString());
                    Item.IsOTC = NConvert.ToBoolean(this.Reader[44].ToString());
                    Item.IsNew = NConvert.ToBoolean(this.Reader[45].ToString());
                    Item.IsLack = NConvert.ToBoolean(this.Reader[46].ToString());
                    Item.IsShow = true;//modified by zlw 2006-6-5
                    Item.IsShow = NConvert.ToBoolean(this.Reader[47].ToString());
                    Item.IsSubtbl = NConvert.ToBoolean(this.Reader[48].ToString());
                    Item.Product.Caution = this.Reader[49].ToString();
                    Item.Grade = this.Reader[50].ToString();
                    Item.Product.BarCode = this.Reader[51].ToString();
                    Item.Product.ProducingArea = this.Reader[52].ToString();
                    Item.Product.Company.ID = this.Reader[53].ToString();
                    Item.Ingredient = this.Reader[54].ToString();
                    Item.ExecuteStandard = this.Reader[55].ToString();
                    Item.Product.BriefIntroduction = this.Reader[56].ToString();
                    Item.Product.Manual = this.Reader[57].ToString();
                    Item.PhyFunction2.ID = this.Reader[58].ToString();
                    Item.PhyFunction3.ID = this.Reader[59].ToString();
                    Item.TenderOffer.IsTenderOffer = NConvert.ToBoolean(this.Reader[60].ToString());
                    Item.TenderOffer.Price = NConvert.ToDecimal(this.Reader[61].ToString());
                    Item.TenderOffer.ContractNO = this.Reader[62].ToString();
                    Item.TenderOffer.BeginTime = NConvert.ToDateTime(this.Reader[63].ToString());
                    Item.TenderOffer.EndTime = NConvert.ToDateTime(this.Reader[64].ToString());
                    Item.TenderOffer.Company.ID = this.Reader[65].ToString();
                    Item.Memo = this.Reader[66].ToString();
                    Item.Oper.ID = this.Reader[67].ToString();
                    Item.Oper.OperTime = NConvert.ToDateTime(this.Reader[68].ToString());
                    Item.NameCollection.OtherSpell.SpellCode = this.Reader[69].ToString();
                    Item.NameCollection.OtherSpell.WBCode = this.Reader[70].ToString();
                    Item.NameCollection.OtherSpell.UserCode = this.Reader[71].ToString();
                    Item.NameCollection.RegularSpell.UserCode = this.Reader[72].ToString();
                    Item.NameCollection.FormalSpell.UserCode = this.Reader[73].ToString();
                    Item.IsAllergy = NConvert.ToBoolean(this.Reader[74].ToString());
                    Item.SpecialFlag = this.Reader[75].ToString();		//75省限标记
                    Item.SpecialFlag1 = this.Reader[76].ToString();		//76市限标记
                    Item.SpecialFlag2 = this.Reader[77].ToString();		//77自费项目
                    Item.SpecialFlag3 = this.Reader[78].ToString();		//78特殊标记
                    Item.SpecialFlag4 = this.Reader[79].ToString();		//79特殊标记
                    Item.OldDrugID = this.Reader[80].ToString();		//80旧系统药品编码
                    Item.ShiftType.ID = this.Reader[81].ToString();		//81数据变动类型
                    Item.ShiftTime = NConvert.ToDateTime(this.Reader[82].ToString());//82数据变动日期
                    Item.ShiftMark = this.Reader[83].ToString();		//83数据变动原因
                    Item.SplitType = this.Reader[84].ToString();     //84拆分类型 0 可拆分 1 不可拆分
                    Item.ShowState = this.Reader[47].ToString();     //显示 属性 0全院 1 住院处 2 门诊
                    al.Add(Item);
                }
            }
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

            return al;
        }

        /// <summary>
        /// 更新医保等级(拓展表)
        /// </summary>
        /// <param name="his_code"></param>
        /// <returns></returns>
        public int UpdateCenterGrade(string his_code, string centerGrade)
        {
            string sql = @"update fin_com_item_extendinfo f set f.item_grade={1} where f.item_code='{0}'";
					
            try
            {
                sql = string.Format(sql, his_code,centerGrade);
                return this.Sql.ExecNoQuery(sql);
            }
            catch (Exception exe)
            {
                this.Err = "更新药品医保等级信息出错！";
                return -1;
            }
        }

        /// <summary>
        /// 更新非药品医保等级
        /// </summary>
        /// <param name="his_code"></param>
        /// <param name="centerGrade"></param>
        /// <returns></returns>
        public int UpdateUndrugCenterGrade(string his_code, string centerGrade)
        {
            string sql = @"update fin_com_undruginfo f set f.item_grade={1} where f.item_code='{0}'"; ;
            try
            {
                sql = string.Format(sql, his_code, centerGrade);
                return this.Sql.ExecNoQuery(sql);
            }
            catch (Exception exe)
            {
                this.Err = "更新非药品医保等级信息出错！"+exe.Message;
                return -1;
            }
        }
        
    }
}
