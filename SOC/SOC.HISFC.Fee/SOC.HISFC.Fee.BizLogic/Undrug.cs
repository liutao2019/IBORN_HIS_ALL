using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Function;
using System.Collections;
using FS.HISFC.Models.Fee.Item;

namespace FS.SOC.HISFC.Fee.BizLogic
{
    /// <summary>
    /// [功能描述: 非药品SOC业务层，从核心版本独立出来，只对SOC层有效]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class Undrug : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获得新的非药品编码
        /// </summary>
        /// <returns>新的非药品编码</returns>
        public string GetUndrugCode()
        {
            string tempUndrugCode = null;//临时非药品编码
            string sql = null;//SQL语句

            //取SELECT语句
            if (this.Sql.GetSql("Fee.Item.UndrugCode", ref sql) == -1)
            {
                this.Err = "获得非药品流水号查询字段Fee.Item.UndrugCode出错!";

                return null;
            }

            tempUndrugCode = this.ExecSqlReturnOne(sql);

            tempUndrugCode = "F" + tempUndrugCode.PadLeft(11, '0');

            return tempUndrugCode;
        }

        public FS.HISFC.Models.Base.Const GetUndrugMTType(string UndrugItemCode)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("Fee.Item.UndrugCode.QueryMtType", ref sql) == -1)
            {
                this.Err = "获得非药品流水号查询字段Fee.Item.UndrugCode出错!";

                return null;
            }
            sql = string.Format(sql, UndrugItemCode);
            if (this.ExecQuery(sql) == -1) return null;
            FS.HISFC.Models.Base.Const mtType = new FS.HISFC.Models.Base.Const();
            while (this.Reader.Read())
            {
                mtType.ID = Reader[0].ToString();
                mtType.Name = Reader[1].ToString();
            }
            return mtType;
        }

        #region 操作

        /// <summary>
        /// 插入非药品物价信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Insert(FS.SOC.HISFC.Fee.Models.Undrug item)
        {
            string sql = FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.Insert;

            //格式化SQL语句
            try
            {
                item.ID = this.GetUndrugCode();
                //取参数列表
                string[] parms = this.getItemParams(item);
                //替换SQL语句中的参数。
                sql = string.Format(sql, parms);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 更新项目笔画码
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public int UpdateCharOrderString(FS.HISFC.Models.Base.Item itemInfo)
        {
            string strSql = string.Empty;
            if (this.Sql.GetSql("Fee.Item.Undrug.UpdateItemCharOrderString", ref strSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateItemCharOrderString";
                return -1;
            }
            strSql = string.Format(strSql, itemInfo.ID, itemInfo.WBCode);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 插入非药品物价信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int Update(FS.SOC.HISFC.Fee.Models.Undrug item)
        {
            string sql = FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.Update;
            //格式化SQL语句
            try
            {
                //取参数列表
                string[] parms = this.getItemParams(item);
                //替换SQL语句中的参数。
                sql = string.Format(sql, parms);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        public int Invalid(FS.SOC.HISFC.Fee.Models.Undrug item)
        {

            string sql = FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.Invalid;
            //格式化SQL语句
            try
            {
                //取参数列表
                // string[] parms = this.getItemParams(item);
                //替换SQL语句中的参数。
                sql = string.Format(sql, item.ID);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(sql);

        }
        /// <summary>
        /// 物资项目与非药品项目同步是否停用标识 {B8775AA4-49B6-42a6-B57D-82C0D86B969B}
        /// </summary>
        /// <returns></returns>
        public int UpdateUndrugValidState(FS.HISFC.Models.Fee.Item.Undrug unDrugItem, FS.SOC.HISFC.Fee.Models.Undrug currentItem)
        {
            string sql = "update fin_com_undruginfo t set t.valid_state ='{0}' where t.item_code ='{1}' and t.item_name = '{2}'";
            //格式化SQL语句
            try
            {
                //取参数列表
                // string[] parms = this.getItemParams(item);
                //替换SQL语句中的参数。
                sql = string.Format(sql, currentItem.ValidState, unDrugItem.ID, unDrugItem.Name);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 非药品信息（停用，修改价格）针对该项目的复合项目明细进行停用，相应复合项目价格更新
        /// </summary>
        /// <param name="itemcode"></param>
        /// <param name="validstate"></param>
        /// <param name="opercode"></param>
        /// <returns></returns>
        public int ExecForUndrugztInfo(string itemcode, string validstate, string opercode, string oldvalidstate, int newprice, int oldprice)
        {
            string strSQL = "";
            if (this.Sql.GetSql("Fee.Item.Undrug.ForUndrugztInfo", ref strSQL) == -1)
            {
                //this.Err = "找不到存储过程执行语句Fee.Item.Undrug.ForUndrugztInfo";
                //return -1;
                //strSQL = @"prc_undruginfo_forundrugztinfo,t_itemcode,22,1,{0},t_validstate,22,1,{1},t_opercode,22,1,{2},t_oldvalidstate,22,1,{3},t_newprice,33,1,{4},t_oldprice,33,1,{5}";
                return 0;
            }

            try
            {
                strSQL = string.Format(strSQL, itemcode, validstate, opercode, oldvalidstate, newprice, oldprice);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            string strReturn = "No Return";
            if (this.ExecEvent(strSQL, ref strReturn) == -1)
            {
                this.Err = strReturn + "执行存储过程出错!prc_undruginfo_forundrugztinfo:" + this.Err;
                this.ErrCode = "prc_undruginfo_forundrugztinfo";
                this.WriteErr();
                return -1;

            }

            if (strReturn != "")
            {
                string[] strParam = strReturn.Split(',');
                if (strParam.Length > 1)
                {
                    if (strParam[0] == "-1")
                    {
                        this.Err = this.Err + strParam[1];
                        return -1;
                    }
                }
            }
            return 1;
        }

        #endregion

        #region 查询

        /// <summary>
        /// 所有有效的组套信息 {21a8d31b-c7f1-4ffd-ab81-ced4b7c82c5c}
        /// </summary>
        /// <param name="lstUndrugzt">返回的数据集</param>
        /// <returns>1,成功; -1,失败</returns>
        public List<FS.SOC.HISFC.Fee.Models.Undrug> QueryAllValidItemGroup()
        {
            string strsql = FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.SelectAll + FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.WhereValidByUnitFlag;

            return this.query(strsql);
        }

        /// <summary>
        /// 获得所有项目信息
        /// </summary>
        /// <returns>成功 所有项目信息, 失败 null</returns>
        public List<FS.SOC.HISFC.Fee.Models.Undrug> QueryAllItemList()
        {
            string sql = FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.SelectAll + FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.WhereValidByItemCode;

            return this.query(string.Format(sql, "All", "All"));
        }

        /// <summary>
        /// 获取简洁的项目信息，只有编码、名称、自定义码等
        /// </summary>
        /// <param name="unitFlag"></param>
        /// <returns></returns>
        public ArrayList QueryBriefList(string unitFlag)
        {
            string sql = FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.SelectValidBriefByUnitFlg;

            return this.queryBriefItemsBySql(string.Format(sql, unitFlag));
        }

        /// <summary>
        /// 判断自定义码是否已经存在,返回查到的列表
        /// </summary>
        /// <param name="unitFlag"></param>
        /// <returns></returns>
        public List<FS.SOC.HISFC.Fee.Models.Undrug> QueryCountByUserCode(string userCode)
        {
            string sql = FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.SelectAll +
                FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.WhereExistsByUserCode;

            List<FS.SOC.HISFC.Fee.Models.Undrug> undrugList = this.query(string.Format(sql, userCode));

            return undrugList;
        }

        /// <summary>
        /// 根据复合项目信息获取该复合项目的非药品信息数组
        /// </summary>
        /// <param name="undrugCode"></param>
        /// <returns></returns>
        public List<FS.SOC.HISFC.Fee.Models.Undrug> GetUndrugList(string undrugCode)
        {
            string sqlSQL = "";
            string sql = FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.SelectAll;

            string sqlWhere = "";

            if (this.Sql.GetSql("Fee.Item.UndrugztInfo.GetUndrug", ref sqlWhere) == -1)
            {
                sqlWhere = @" where item_code in (select b.package_code from fin_com_undrugztinfo b where b.item_code  = '{0}')";
            }
            sqlSQL = sql + sqlWhere;

            try
            {
                sqlSQL = string.Format(sqlSQL, undrugCode);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                return null;
            }

            List<FS.SOC.HISFC.Fee.Models.Undrug> list = this.query(sqlSQL);
            if (list == null)
            {
                return null;
            }

            if (list.Count == 0)
            {
                return list;
            }

            return list;
        }

        /// <summary>
        /// 根据非药品信息获取该项目的所有复合项目信息
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Fee.Item.UndrugComb> GetUndrugCombList(FS.SOC.HISFC.Fee.Models.Undrug item)
        {
            string sqlSQL = "";

            if (this.Sql.GetSql("Fee.Item.UndrugztInfo.GetUndrugCombList", ref sqlSQL) == -1)
            {
                sqlSQL = @"select 
                                u.package_code, 
                                u.package_name, 
                                u.item_code, 
                                a.item_name, 
                                u.sort_id, 
                                u.qty, 
                                u.valid_state valid_state,
                                a.spell_code, 
                                a.wb_code, 
                                a.input_code, 
                                a.unit_price price,
                                a.unit_price1 childprice,
                                a.unit_price2 specialPrice,
                                (select f.empl_name from com_employee f where f.empl_code= u.oper_code and f.valid_state='1'),
                                u.oper_date,
                                (select a.sys_class from fin_com_undruginfo a  where a.item_code=u.package_code)
                                from fin_com_undrugztinfo u , fin_com_undruginfo a  where u.item_code=a.item_code and 
                              u.item_code = '{0}'";
            }

            try
            {
                sqlSQL = string.Format(sqlSQL, item.ID);
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                ErrCode = ex.Message;
                return null;
            }

            //执行当前Sql语句
            if (this.ExecQuery(sqlSQL) == -1)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }

            List<FS.HISFC.Models.Fee.Item.UndrugComb> al = new List<FS.HISFC.Models.Fee.Item.UndrugComb>();

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Fee.Item.UndrugComb undrugComb = new FS.HISFC.Models.Fee.Item.UndrugComb();
                    undrugComb.Package.ID = this.Reader[0].ToString();
                    undrugComb.Package.Name = this.Reader[1].ToString();
                    undrugComb.ID = this.Reader[2].ToString();
                    //FS.HISFC.Models.Fee.Item.UndrugComb undrug = item as FS.HISFC.Models.Fee.Item.UndrugComb;
                    //undrugComb = undrug.Clone();
                    undrugComb.Name = this.Reader[3].ToString();
                    undrugComb.SortID = NConvert.ToInt32(this.Reader[4].ToString());
                    undrugComb.Qty = NConvert.ToInt32(this.Reader[5].ToString());
                    undrugComb.ValidState = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[6].ToString()).ToString();

                    undrugComb.SpellCode = this.Reader.IsDBNull(7) ? "" : this.Reader.GetString(7);
                    undrugComb.WBCode = this.Reader.IsDBNull(8) ? "" : this.Reader.GetString(8);
                    undrugComb.UserCode = this.Reader.IsDBNull(9) ? "" : this.Reader.GetString(9);
                    undrugComb.Price = this.Reader.IsDBNull(10) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(10));
                    undrugComb.ChildPrice = this.Reader.IsDBNull(11) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(11));
                    undrugComb.SpecialPrice = this.Reader.IsDBNull(12) ? 0 : Convert.ToDecimal(this.Reader.GetDecimal(12));
                    undrugComb.Oper.Name = this.Reader.IsDBNull(13) ? "" : this.Reader.GetString(13);
                    undrugComb.Oper.OperTime = this.Reader.IsDBNull(14) ? DateTime.MinValue : this.Reader.GetDateTime(14);

                    undrugComb.SysClass.ID = this.Reader[5].ToString();
                    al.Add(undrugComb);
                }
                return al;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                //如果还没有关闭Reader 关闭之
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        public FS.SOC.HISFC.Fee.Models.Undrug GetUndrug(string undrugCode)
        {
            string sql = FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.SelectAll + FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.WhereValidByItemCode;

            List<FS.SOC.HISFC.Fee.Models.Undrug> list = this.query(string.Format(sql, undrugCode, "All"));
            if (list == null)
            {
                return null;
            }

            if (list.Count == 0)
            {
                return null;
            }

            return list[0];
        }

        public FS.SOC.HISFC.Fee.Models.Undrug GetUndrugByName(string undrugName)
        {
            string sql = FS.SOC.HISFC.Fee.Data.AbstractUndrug.Current.SelectAll + " where Item_name = '{0}' order by oper_date desc ";

            List<FS.SOC.HISFC.Fee.Models.Undrug> list = this.query(string.Format(sql, undrugName));
            if (list == null)
            {
                return null;
            }

            if (list.Count == 0)
            {
                return null;
            }

            return list[0];
        }

        #endregion

        #region 内置

        /// <summary>
        /// 根据sql获取项目信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        private List<FS.SOC.HISFC.Fee.Models.Undrug> query(string sql)
        {
            List<FS.SOC.HISFC.Fee.Models.Undrug> items = new List<FS.SOC.HISFC.Fee.Models.Undrug>(); //用于返回非药品信息的数组

            //执行当前Sql语句
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = this.Err + "\r\n\r\n" + this.Sql.Err;
                this.WriteErr();
                return null;
            }

            if (this.Reader == null)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    FS.SOC.HISFC.Fee.Models.Undrug item = new FS.SOC.HISFC.Fee.Models.Undrug();
                    item.ID = this.Reader[0].ToString();//非药品编码 
                    item.Name = this.Reader[1].ToString(); //非药品名称 
                    item.SysClass.ID = this.Reader[2].ToString(); //系统类别
                    item.MinFee.ID = this.Reader[3].ToString();  //最小费用代码 
                    item.UserCode = this.Reader[4].ToString(); //输入码
                    item.SpellCode = this.Reader[5].ToString(); //拼音码
                    item.WBCode = this.Reader[6].ToString();    //五笔码
                    item.NameCollection.GbCode = this.Reader[7].ToString();    //国家编码
                    item.NameCollection.InternationalCode = this.Reader[8].ToString();//国际编码
                    item.Price = NConvert.ToDecimal(this.Reader[9].ToString()); //默认价
                    item.PriceUnit = this.Reader[10].ToString();  //计价单位
                    item.FTRate.EMCRate = NConvert.ToDecimal(this.Reader[11].ToString()); // 急诊加成比例
                    item.IsFamilyPlanning = NConvert.ToBoolean(this.Reader[12].ToString()); // 计划生育标记 
                    item.User01 = this.Reader[13].ToString(); //特定诊疗项目
                    item.Grade.ID = this.Reader[14].ToString();//甲乙类标志

                    if (string.IsNullOrEmpty(this.Reader[15].ToString()))
                    {
                        item.IsNeedConfirm = false;
                        item.NeedConfirm = EnumNeedConfirm.None;
                    }
                    else
                    {
                        if (Enum.IsDefined(typeof(FS.HISFC.Models.Fee.Item.EnumNeedConfirm),
                            FS.FrameWork.Function.NConvert.ToInt32(this.Reader[15].ToString())))
                        {
                            item.NeedConfirm = (FS.HISFC.Models.Fee.Item.EnumNeedConfirm)Enum.Parse(typeof(FS.HISFC.Models.Fee.Item.EnumNeedConfirm), this.Reader[15].ToString());
                            if (item.NeedConfirm == EnumNeedConfirm.All || item.NeedConfirm == EnumNeedConfirm.Inpatient)
                            {
                                item.IsNeedConfirm = true;
                            }
                        }
                    }

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
                    item.DefPrice = NConvert.ToDecimal(this.Reader[34].ToString());//进价
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
                    item.UnitFlag = this.Reader[45].ToString(); //[]单位标识
                    item.ApplicabilityArea = this.Reader[46].ToString();
                    item.DeptList = this.Reader[47].ToString(); //允许开立科室
                    item.ItemPriceType = this.Reader[48].ToString();  //物价费用类别
                    item.IsOrderPrint = this.Reader[49].ToString();   //医嘱单打印标示
                    item.Oper.ID = this.Reader[50].ToString();   //申请人
                    if (item.ValidState == "0")
                    {
                        item.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[51].ToString()); //停止时间
                    }

                    //增加信息
                    item.NameCollection.OtherName = this.Reader[52].ToString();
                    item.NameCollection.OtherSpell.SpellCode = this.Reader[53].ToString();
                    item.NameCollection.OtherSpell.WBCode = this.Reader[54].ToString();
                    item.NameCollection.OtherSpell.UserCode = this.Reader[55].ToString();
                    item.NameCollection.EnglishName = this.Reader[56].ToString();
                    item.NameCollection.EnglishOtherName = this.Reader[57].ToString();
                    item.NameCollection.EnglishRegularName = this.Reader[58].ToString();
                    item.RegisterCode = this.Reader[59].ToString();//注册码
                    item.RegisterDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[60]);
                    item.Producer = this.Reader[61].ToString();
                    item.MTType.ID = this.Reader[62].ToString();//[添加医技类型]
                    item.MTType.Name = this.Reader[63].ToString();//[添加医技类型]
                    item.DefaultExecDeptForOut = Reader[64].ToString();
                    item.DefaultExecDeptForIn = Reader[65].ToString();
                    item.ManageClass.ID = Reader[66].ToString();//{6F68AB52-332C-4efa-A6DD-F6BDB37B1283}
                    item.ItemSecondPriceType = this.Reader[67].ToString();  //物价费用类别二
                    item.IsDiscount = this.Reader[68].ToString();  //折扣
                    item.YbCode = this.Reader[69].ToString();
                    items.Add(item);
                }//循环结束

                return items;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                //如果还没有关闭Reader 关闭之
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 获得update或者insert非药品字典表的传入参数数组
        /// </summary>
        /// <param name="undrug">非药品实体</param>
        /// <returns>参数数组</returns>
        private string[] getItemParams(FS.SOC.HISFC.Fee.Models.Undrug undrug)
        {
            string[] args = 
			{	
				undrug.ID, 
				undrug.Name,
				undrug.SysClass.ID.ToString(),
				undrug.MinFee.ID.ToString(),
				undrug.UserCode, 
				undrug.SpellCode,
				undrug.WBCode,
				undrug.GBCode, 
				undrug.NationCode,
				undrug.Price.ToString(),
				undrug.PriceUnit,						
				undrug.FTRate.EMCRate.ToString(),
				NConvert.ToInt32(undrug.IsFamilyPlanning).ToString(),	
				"",  				        
				undrug.Grade.ID,					
				((int)undrug.NeedConfirm).ToString(),
				FS.FrameWork.Function.NConvert.ToInt32(undrug.ValidState).ToString(),		
				undrug.Specs,					
				undrug.ExecDept,					
				undrug.MachineNO,
				undrug.CheckBody,				
				undrug.OperationInfo.ID,                 
				undrug.OperationType.ID,			
				undrug.OperationScale.ID,
				NConvert.ToInt32(undrug.IsCompareToMaterial).ToString(),		
				undrug.Memo,					
				undrug.Oper.ID ,	
				undrug.ChildPrice.ToString(),            
				undrug.SpecialPrice.ToString(),         
				undrug.SpecialFlag,                   
				undrug.SpecialFlag1,                          
				undrug.SpecialFlag2,
				undrug.SpecialFlag3,                     
				undrug.SpecialFlag4,                  
				"0",     
               // "0",
                undrug.DefPrice.ToString(),//进价
				undrug.DiseaseType.ID ,
				undrug.SpecialDept.ID,
				NConvert.ToInt32(undrug.IsConsent).ToString(),
				undrug.MedicalRecord,                        
				undrug.CheckRequest,                          
				undrug.Notice,					
				undrug.CheckApplyDept,
				NConvert.ToInt32(undrug.IsNeedBespeak).ToString(),
			    undrug.ItemArea,
			    undrug.ItemException,
                undrug.UnitFlag,/*[2007/01/19]后加的字段,单位标识46*/
                undrug.ApplicabilityArea,
                undrug.DeptList,
                undrug.ItemPriceType,
                undrug.IsOrderPrint,
                undrug.NameCollection.OtherName,
                undrug.NameCollection.OtherSpell.SpellCode,
                undrug.NameCollection.OtherSpell.WBCode,
                undrug.NameCollection.OtherSpell.UserCode,
                undrug.NameCollection.EnglishName,
                undrug.NameCollection.EnglishOtherName,
                undrug.NameCollection.EnglishRegularName,
                undrug.RegisterCode,
                undrug.RegisterDate.ToString("yyyy-MM-dd HH:mm:ss"),
                undrug.Producer,
                undrug.MTType.ID,//[添加医技类型]
                undrug.MTType.Name,//[添加医技类型]
                undrug.DefaultExecDeptForOut, //默认执行科室（门诊）
                undrug.DefaultExecDeptForIn, //默认执行科室（住院）
                undrug.ManageClass.ID.ToString(),
                undrug.ItemSecondPriceType,
                undrug.IsDiscount,
                undrug.YbCode
			};

            return args;
        }

        /// <summary>
        /// 单独获取维护的执行科室信息
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public FS.SOC.HISFC.Fee.Models.Undrug GetExecInfo(string itemCode)
        {
            string strSQL = @"select exedept_code,exec_dept_out,exec_dept_in from fin_com_undruginfo where item_code='{0}'";
            strSQL = string.Format(strSQL, itemCode);

            //执行当前Sql语句
            if (this.ExecQuery(strSQL) == -1)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }

            try
            {
                ArrayList items = new ArrayList(); //用于返回非药品信息的数组
                //循环读取数据
                while (this.Reader.Read())
                {
                    FS.SOC.HISFC.Fee.Models.Undrug item = new FS.SOC.HISFC.Fee.Models.Undrug();
                    item.ID = itemCode;
                    item.ExecDept = Reader[0].ToString();
                    item.DefaultExecDeptForOut = Reader[1].ToString();
                    item.DefaultExecDeptForIn = Reader[2].ToString();

                    return item;
                }
            }
            catch (Exception e)
            {
                this.Err = "获得非药品基本信息出错！" + e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }

            return null;
        }

        /// <summary>
        /// 取非药品基本信息数组
        /// </summary>
        /// <param name="sql">当前Sql语句</param>
        /// <returns>成功返回非药品数组 失败返回null</returns>
        private ArrayList queryBriefItemsBySql(string sql)
        {
            //执行当前Sql语句
            if (this.ExecQuery(sql) == -1)
            {
                this.Err = this.Sql.Err;
                this.WriteErr();
                return null;
            }

            try
            {
                ArrayList items = new ArrayList(); //用于返回非药品信息的数组
                //循环读取数据
                while (this.Reader.Read())
                {
                    FS.SOC.HISFC.Fee.Models.Undrug item = new FS.SOC.HISFC.Fee.Models.Undrug();
                    item.ID = this.Reader[0].ToString();//非药品编码 
                    item.Name = this.Reader[1].ToString(); //非药品名称 
                    item.SysClass.ID = this.Reader[2].ToString(); //系统类别
                    item.MinFee.ID = this.Reader[3].ToString();  //最小费用代码 
                    item.UserCode = this.Reader[4].ToString(); //输入码
                    item.SpellCode = this.Reader[5].ToString(); //拼音码
                    item.WBCode = this.Reader[6].ToString();    //五笔码
                    item.Price = NConvert.ToDecimal(this.Reader[7].ToString()); //默认价 
                    item.ChildPrice = NConvert.ToDecimal(this.Reader[8].ToString()); //儿童价
                    item.SpecialPrice = NConvert.ToDecimal(this.Reader[9].ToString()); //特诊价
                    item.ValidState = this.Reader[10].ToString(); //有效性标识 在用 1 停用 0 废弃 2  
                    item.Memo = this.Reader[11].ToString(); //备注  
                    item.GBCode = this.Reader[12].ToString(); //国家码
                    items.Add(item);
                }//循环结束

                return items;
            }
            catch (Exception e)
            {
                this.Err = "获得非药品基本信息出错！" + e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && this.Reader.IsClosed == false)
                {
                    this.Reader.Close();
                }
            }
        }

        #endregion
    }
}
