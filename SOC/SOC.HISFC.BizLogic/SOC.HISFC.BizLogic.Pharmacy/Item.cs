using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.BizLogic.Pharmacy
{
    /// <summary>
    /// [功能描述: 药品管理类]<br></br>
    /// [创 建 者: Cube]<br></br>
    /// [创建时间: 2011-06]<br></br>
    /// <修改记录>
    /// </修改记录>
    /// </summary>
    public class Item : DataBase
    {

        #region 静态量
        /// <summary>
        /// 是否允许扣除负库存
        /// </summary>
        public static bool MinusStore
        {
            get
            {
                FS.FrameWork.Management.ControlParam ctrlManager = new FS.FrameWork.Management.ControlParam();
                ctrlManager.SetTrans(FS.FrameWork.Management.PublicTrans.Trans);
                //negativeStore 1 负库存管理 0 不允许负库存管理
                string negativeStore = ctrlManager.QueryControlerInfo("S00024", false);

                return FS.FrameWork.Function.NConvert.ToBoolean(negativeStore);
            }
        }
       
        #endregion

        #region 取流水号

        /// <summary>
        /// 取药品出库单流水号
        /// </summary>
        /// <returns>失败返回null 非空返回新流水号</returns>
        public string GetNewOutputNO()
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.GetNewOutputID", ref strSQL) == -1)
                return null;
            string strReturn = this.ExecSqlReturnOne(strSQL);
            if (strReturn == "-1")
            {
                this.Err = "取药品出库单流水号时出错！" + this.Err;
                return null;
            }
            return strReturn;
        }

        /// <summary>
        /// 取摆药单流水号
        /// </summary>
        /// <returns>失败返回null 非空返回新流水号</returns>
        public string GetNewDrugBillNO()
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.GetNewDrugBillID", ref strSQL) == -1)
                return null;
            string strReturn = this.ExecSqlReturnOne(strSQL);
            if (strReturn == "-1")
            {
                this.Err = "取摆药单流水号时出错！" + this.Err;
                return null;
            }
            return strReturn;
        }
        /// <summary>
        /// 取摆药单流水号
        /// </summary>
        /// <returns>失败返回null 非空返回新流水号</returns>
        public string GetNewDrugBillNO(string approveDept)
        {
            string strSQL = "";
            string localRules = "0";
            if (this.GetSQL("Pharmacy.Item.GetNewDrugBillID", ref strSQL) == -1)
            {
                localRules = "1";
                if (this.GetSQL("Pharmacy.Item.GetNewDrugBillID.GetSequenceName", ref strSQL) > -1)
                {
                    strSQL = string.Format(strSQL, approveDept);
                    string sequenceName = this.ExecSqlReturnOne(strSQL);
                    strSQL = @"SELECT nvl({0}.NEXTVAL,1)-(select nvl(e.number_property,0) 
                            from com_extend e 
                            where e.extend_class = 'DEPT' 
                            and e.item_code = '{1}'
                            and e.property_code = 'DrugBillSequenceNO') FROM DUAL";
                    strSQL = string.Format(strSQL, sequenceName, approveDept);
                }
            }
            if (string.IsNullOrEmpty(strSQL))
            {
                strSQL = "SELECT SEQ_PHA_DRUGBILL_ID.NEXTVAL FROM DUAL";
            }

            string strReturn = this.ExecSqlReturnOne(strSQL);
            if (strReturn == "-1")
            {
                this.Err = "取摆药单流水号时出错！" + this.Err;
                return null;
            }

            if (localRules == "1")
            {
                strReturn = this.GetDateTimeFromSysDateTime().ToString("yyyyMMdd") + "-" + strReturn;
            }

            return strReturn;
        }

        /// <summary>
        /// 取新库存批次流水号
        /// </summary>
        /// <returns>成功返回新批次 失败返回null</returns>
        public string GetNewGroupNO()
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.GetNewGroupID", ref strSQL) == -1)
                return null;
            string strReturn = this.ExecSqlReturnOne(strSQL);
            if (strReturn == "-1")
            {
                this.Err = "取批次流水号时出错！" + this.Err;
                return null;
            }
            return strReturn;
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

        #endregion

        #region 药品基本信息

        #region 基础增、删、改操作

        /// <summary>
        /// 取药品部分基本信息列表，可能是一条或者多条药品记录
        /// 私有方法，在其他方法中调用
        /// </summary>
        /// <param name="SQLString">SQL语句</param>
        /// <returns>成功返回药品对象数组 失败返回null</returns>
        private List<FS.HISFC.Models.Pharmacy.Item> myGetItemSimple(string SQLString)
        {
            List<FS.HISFC.Models.Pharmacy.Item> al = new List<FS.HISFC.Models.Pharmacy.Item>();
            FS.HISFC.Models.Pharmacy.Item Item; //返回数组中的药品信息类

            try
            {
                this.ExecQuery(SQLString);

                while (this.Reader.Read())
                {
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
                    #endregion
                    try
                    {
                        Item.ID = this.Reader[0].ToString();                                  //0  药品编码
                        Item.Name = this.Reader[1].ToString();                                //1  商品名称
                        Item.PackQty = NConvert.ToDecimal(this.Reader[5].ToString());         //5  包装数量
                        Item.Specs = this.Reader[6].ToString();                               //6  规格
                        Item.SysClass.ID = this.Reader[7].ToString();                         //7  系统类别编码
                        Item.MinFee.ID = this.Reader[8].ToString();                           //8  最小费用代码
                        Item.PackUnit = this.Reader[21].ToString();                           //21 包装单位
                        Item.MinUnit = this.Reader[22].ToString();                            //22 最小单位
                        Item.Type.ID = this.Reader[26].ToString();                            //26 药品类别编码
                        Item.Quality.ID = this.Reader[27].ToString();                         //27 药品性质编码
                        Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[28].ToString());    //28 零售价
                        Item.Product.Producer.ID = this.Reader[37].ToString();                        //37 生产厂家编码
                        Item.ValidState = (FS.HISFC.Models.Base.EnumValidState)(NConvert.ToInt32(this.Reader[41]));

                        Item.IsValid = !Item.IsStop;


                        Item.SpellCode = this.Reader[2].ToString();                          //2  拼音码  
                        Item.WBCode = this.Reader[3].ToString();                             //3  五笔码
                        Item.UserCode = this.Reader[4].ToString();                           //4  自定义码
                        Item.NameCollection.RegularName = this.Reader[9].ToString();                         //9  药品通用名
                        Item.NameCollection.RegularSpell.SpellCode = this.Reader[10].ToString();        //10 通用名拼音码
                        Item.NameCollection.RegularSpell.WBCode = this.Reader[11].ToString();           //11 通用名五笔码
                        Item.NameCollection.RegularSpell.UserCode = this.Reader[72].ToString();         //72 通用名自定义码
                        Item.NameCollection.EnglishName = this.Reader[16].ToString();                        //16 英文商品名 
                        Item.IsNostrum = NConvert.ToBoolean(this.Reader[85].ToString());                     //85  协定处方标志
                    }
                    catch (Exception ex)
                    {
                        this.Err = "获得药品基本信息出错！" + ex.Message;
                        this.WriteErr();
                        return null;
                    }

                    al.Add(Item);
                }
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

            return al;
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

                while (this.Reader.Read())
                {
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
                    // 80 旧系统药品编码 81数据变动类型	   82数据变动时间	 83数据变动原因    84 门诊拆分属性
                    // 85 协定处方标志   94住院长嘱拆分    95住院临嘱拆分
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

                    Item.IsValid = !Item.IsStop;


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
                    Item.IsNostrum = NConvert.ToBoolean(this.Reader[85].ToString()); //85协定处方标志
                    if (this.Reader.FieldCount > 86)
                    {
                        //扩展信息-备用
                        Item.ExtendData1 = this.Reader[86].ToString();
                    }
                    if (this.Reader.FieldCount > 87)
                    {
                        Item.ExtendData2 = this.Reader[87].ToString();
                    }
                    if (this.Reader.FieldCount > 88)
                    {  //字典生成时间
                        Item.CreateTime = NConvert.ToDateTime(this.Reader[88]);
                    }
                    if (this.Reader.FieldCount > 89)
                    {  //第二零售价
                        Item.RetailPrice2 = NConvert.ToDecimal(this.Reader[89]);
                    }
                    if (this.Reader.FieldCount > 90)
                    {
                        Item.ExtNumber1 = NConvert.ToDecimal(this.Reader[90]);
                    }
                    if (this.Reader.FieldCount > 91)
                    {
                        Item.ExtNumber2 = NConvert.ToDecimal(this.Reader[91]);
                    }
                    if (this.Reader.FieldCount > 92)
                    {
                        Item.ExtendData3 = this.Reader[92].ToString();
                    }
                    if (this.Reader.FieldCount > 93)
                    {
                        Item.ExtendData4 = this.Reader[93].ToString();
                    }
                    if (this.Reader.FieldCount > 94)
                    {
                        Item.CDSplitType = this.Reader[94].ToString();
                    }
                    if (this.Reader.FieldCount > 95)
                    {
                        Item.LZSplitType = this.Reader[95].ToString();
                    }
                    if (this.Reader.FieldCount > 96)
                    {
                        Item.SecondBaseDose = NConvert.ToDecimal(this.Reader[96]);
                    }
                    if (this.Reader.FieldCount > 97)
                    {
                        Item.SecondDoseUnit = this.Reader[97].ToString();
                    }
                    if (this.Reader.FieldCount > 98)
                    {
                        Item.OnceDoseUnit = this.Reader[98].ToString();
                    }
                    if (this.Reader.FieldCount > 99)
                    {
                        Item.ProductID = this.Reader[99].ToString();//产品ID
                    }
                    if (this.Reader.FieldCount > 100)
                    {
                        Item.BigPackUnit = this.Reader[100].ToString();//大包装单位
                    }
                    if (this.Reader.FieldCount > 101)
                    {
                        Item.BigPackQty = this.Reader[101].ToString();//大包装数量
                    }
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
        /// 获得update或者insert药品字典表的传入参数数组
        /// </summary>
        /// <param name="Item">药品基本信息</param>
        /// <returns>成功返回参数数组 失败返回null</returns>
        private string[] myGetParmItem(FS.HISFC.Models.Pharmacy.Item Item)
        {
            #region "接口说明"
            //  0 药品编码        1 商品名称        2 拼音码          3 五笔码          4 自定义码 
            //  5 规格            6 最小费用代码    7 系统类别编码    8 包装数量        9 国际编码     
            // 10 药品类别编码   11 药品通用名     12 通用名拼音码   13 通用名五笔码   14 储藏条件 
            // 15 药品性质编码   16 学名           17 学名拼音码     18 学名五笔码     19 频次
            // 20 别名           21 英文商品名     22 英文通用名     23 英文别名       24 国家编码
            // 25 包装单位       26 最小单位       27 基本剂量       28 剂量单位       29 剂型编码
            // 30 生产厂家编码   31 批文信息       32 零售价         33 批发价         34 购入价       
            // 35 使用方法编码   36 注册商标       37 药理作用编码   38 二级药理作用   39 三级药理作用       
            // 40 备注           41 价格形式编码   42 是否停用       43 是否自制       44 是否新药
            // 45 是否GMP        46 是否OTC－处方药47 是否缺药       48 是否大屏幕显示 49 是否附材            
            // 50 注意事项       51 有效成份       52 一次用量       53 中药执行标准   54 药品简介
            // 55 药品等级       56 条形码         57 产地           58 最新供货公司   59 最高零售价       
            // 60 药品说明书内容 61 操作员代码     62 操作时间       63 是否需要试敏,  64 是否招标药品
            // 65 采购单位编码   66 中标价         67 采购合同编号   68 采购开始周期   69 采购结束周期
            // 70 别名拼音码     71 别名五笔码     72 别名自定义码   73 通用名自定义码 74 学名自定义码  
            // 75 省限制         76市限制          77自费项目        78特殊标记        79 特殊标记
            // 80 旧系统药品编码 81数据变动类型	   82数据变动时间	 83数据变动原因    84 拆分类型
            // 85 协定处方标志   86 扩展数据1      87扩展数据2       88字典建立时间    
            #endregion

            //{8ADD2D48-2427-48aa-A521-4B17EECBC8B4} 新增字段：扩展数据1 扩展数据2 字典建立时间,第二零售价，扩展数字01，扩展数字02扩展数据03扩展数据04长嘱拆分05临瞩拆分06

            string[] strParm ={   Item.ID,              Item.Name,             Item.SpellCode,                  Item.WBCode,                   Item.UserCode,        
								  Item.Specs,           Item.MinFee.ID,        Item.SysClass.ID.ToString(),     Item.PackQty.ToString(),       Item.NameCollection.InternationalCode,	   
								  Item.Type.ID,         Item.NameCollection.RegularName,      Item.NameCollection.RegularSpell.SpellCode, Item.NameCollection.RegularSpell.WBCode,  Item.Product.StoreCondition,    
								  Item.Quality.ID.ToString(), Item.NameCollection.FormalName, Item.NameCollection.FormalSpell.SpellCode,  Item.NameCollection.FormalSpell.WBCode,   Item.Frequency.ID,
								  Item.NameCollection.OtherName,       Item.NameCollection.EnglishName,      Item.NameCollection.EnglishRegularName,          Item.NameCollection.EnglishOtherName,          Item.NameCollection.GbCode,
								  Item.PackUnit,        Item.MinUnit,          Item.BaseDose.ToString(),         Item.DoseUnit,                  Item.DosageForm.ID,                           
								  Item.Product.Producer.ID,     Item.Product.ApprovalInfo,     Item.PriceCollection.RetailPrice.ToString(),      Item.PriceCollection.WholeSalePrice.ToString(), Item.PriceCollection.PurchasePrice.ToString(),  
								  Item.Usage.ID,        Item.Product.Label,            Item.PhyFunction1.ID,             Item.PhyFunction2.ID,           Item.PhyFunction3.ID,
								  Item.Memo,            Item.PriceCollection.PriceForm.ID,     ((int)Item.ValidState).ToString(),  NConvert.ToInt32(Item.Product.IsSelfMade).ToString(),NConvert.ToInt32(Item.IsNew).ToString(),
								  NConvert.ToInt32(Item.IsGMP).ToString(),NConvert.ToInt32(Item.IsOTC).ToString(), NConvert.ToInt32(Item.IsLack).ToString(),  NConvert.ToInt32(Item.IsShow).ToString() /*Item.ShowState*/,NConvert.ToInt32(Item.IsSubtbl).ToString(),
								  Item.Product.Caution,         Item.Ingredient,       Item.OnceDose.ToString(),         Item.ExecuteStandard,           Item.Product.BriefIntroduction,         
								  Item.Grade,           Item.Product.BarCode,          Item.Product.ProducingArea,               Item.Product.Company.ID,                Item.PriceCollection.TopRetailPrice.ToString(),
								  Item.Product.Manual,          this.Operator.ID,      Item.Oper.OperTime.ToString(),         NConvert.ToInt32(Item.IsAllergy).ToString(),	   NConvert.ToInt32(Item.TenderOffer.IsTenderOffer).ToString(),  
								  Item.TenderOffer.Company.ID, Item.TenderOffer.Price.ToString(), Item.TenderOffer.ContractNO, Item.TenderOffer.BeginTime.ToString(), Item.TenderOffer.EndTime.ToString(), 
								  Item.NameCollection.OtherSpell.SpellCode, Item.NameCollection.OtherSpell.WBCode, Item.NameCollection.OtherSpell.UserCode, Item.NameCollection.RegularSpell.UserCode, Item.NameCollection.FormalSpell.UserCode,
								  Item.SpecialFlag,     Item.SpecialFlag1,     Item.SpecialFlag2,                Item.SpecialFlag3,              Item.SpecialFlag4,
								  Item.OldDrugID,	   Item.ShiftType.ID.ToString(), Item.ShiftTime.ToString(),		Item.ShiftMark.ToString()  ,Item.SplitType , NConvert.ToInt32(Item.IsNostrum).ToString(),
                                  
                                  //第一期加入的扩展字段
                                  Item.ExtendData1,   
                                  Item.ExtendData2,       
                                  Item.CreateTime.ToString(),
                                  Item.RetailPrice2.ToString(),
                                  Item.ExtNumber1.ToString(),
                                  Item.ExtNumber2.ToString(),
                                  Item.ExtendData3,
                                  Item.ExtendData4,
                                  //第二期加入的扩展字段
                                  Item.CDSplitType,
                                  Item.LZSplitType,
                                  Item.SecondBaseDose.ToString(),
                                  Item.SecondDoseUnit,
                                  Item.OnceDoseUnit,
                                  //第三期加入的产品ID,大包装单位，大包装数量
                                  Item.ProductID,
                                  Item.BigPackUnit,
                                  Item.BigPackQty
							 };

            return strParm;
        }

        /// <summary>
        /// 向药品字典表中插入一条记录，药品编码采用oracle中的序列号
        /// </summary>
        /// <param name="item">药品基本信息</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int InsertItem(FS.HISFC.Models.Pharmacy.Item item)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.InsertItem", ref strSQL) == -1) return -1;
            string[] strParm;
            try
            {
                //取药品流水号
                item.ID = this.GetSequence("Pharmacy.Item.GetNewItemID");
                if (item.ID == null) return -1;
                item.ID = "Y" + item.ID.PadLeft(11, '0');

                strParm = myGetParmItem(item);  //取参数列表
                //strSQL = string.Format( strSQL , strParm );    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "付数值时候出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }

            return this.ExecNoQuery(strSQL, strParm);
        }

        /// <summary>
        /// 更新药品信息，以药品编码为主键
        /// </summary>
        /// <param name="item">药品基本信息</param>
        /// <returns>0没有更新 1成功 -1失败</returns>
        public int UpdateItem(FS.HISFC.Models.Pharmacy.Item item)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateItem", ref strSQL) == -1) return -1;
            string[] strParm;

            try
            {
                strParm = myGetParmItem(item);  //取参数列表
                //strSQL = string.Format( strSQL , strParm );    //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "付数值时候出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL, strParm);
        }

        /// <summary>
        /// 删除药品信息
        /// </summary>
        /// <param name="ID">药品编码</param>
        /// <returns>0没有删除 1成功 -1失败</returns>
        public int DeleteItem(string ID)
        {
            string strSQL = ""; //根据药品编码删除某一药品信息的DELETE语句
            if (this.GetSQL("Pharmacy.Item.DeleteItem", ref strSQL) == -1) return -1;
            try
            {
                strSQL = string.Format(strSQL, ID);
            }
            catch
            {
                this.Err = "传入参数不对！Pharmacy.Item.DeleteItem";
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 保存药品数据－－先执行更新操作，如果没有找到可以更新的数据，则插入一条新记录
        /// </summary>
        /// <param name="item">药品实体</param>
        /// <returns>1成功 -1失败</returns>
        public int SetItem(FS.HISFC.Models.Pharmacy.Item item)
        {
            int parm;
            //执行更新操作
            parm = UpdateItem(item);

            //如果没有找到可以更新的数据，则插入一条新记录
            if (parm == 0)
            {
                parm = InsertItem(item);
            }
            return parm;
        }

        #endregion

        #region 内部使用

        /// <summary>
        /// 取全部药品信息列表
        /// </summary>
        /// <returns>药品类数组</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemList()
        {
            string strSelect = "";  //获得全部药品信息的SELECT语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetItem(strSelect);
        }

        /// <summary>
        /// 根据药品类别取药品信息列表
        /// </summary>
        /// <param name="drugType">药品类别</param>
        /// <returns>成功返回对应药品信息数组 出错返回null</returns>
        [Obsolete("重构函数名称为QueryItemListForCheck", false)]
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemListForCheck(string drugType)
        {
            string strSelect = "";  //获得全部药品信息的SELECT语句
            string strWhere = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.OrderInfo字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetList.ForCheck", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetList.ForCheck字段!";
                return null;
            }
            try
            {
                strSelect = strSelect + strWhere;
                strSelect = string.Format(strSelect, drugType);
            }
            catch
            {
                this.Err = "SQL参数初始化失败";
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetItem(strSelect);
        }

        /// <summary>
        /// 获得全部药品信息列表，根据参数判断是否显示简单数据列
        /// </summary>
        /// <param name="IsShowSimple">是否显示简单数据列</param>
        /// <returns>成功返回药品信息简略数组 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemList(bool IsShowSimple)
        {
            string strSelect = "";  //获得全部药品信息的SELECT语句
            //string strWhere  ="";  //获得全部药品信息的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            if (IsShowSimple)
                return this.myGetItemSimple(strSelect);
            else
                return this.myGetItem(strSelect);
        }

        /// <summary>
        /// 通过自定义码获取是否存在有效的药品信息
        /// </summary>
        /// <param name="CustomCode">药品自定义码</param>
        /// <returns>成功返回药品数组 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryValidDrugByCustomCode(string CustomCode)
        {
            string strSelect = "";  //获得全部药品信息的SELECT语句
            string strWhere = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetList.IfHaveValid", ref strWhere) == -1)
            {

                this.Err = "没有找到Pharmacy.Item.GetList.IfHaveValid字段!";
                return null;
            }
            try
            {
                strSelect = strSelect + strWhere;
                strSelect = string.Format(strSelect, CustomCode);
            }
            catch
            {
                this.Err = "SQL参数初始化失败";
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetItem(strSelect);
        }

        /// <summary>
        /// 获得可用药品信息列表
        /// </summary>
        /// <returns>成功返回药品信息数组 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemAvailableList()
        {
            string strSelect = "";  //获得药品信息的SELECT语句
            string strWhere = "";  //获得可用药品信息的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetAvailableList.Where", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAvailableList.Where字段!";
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            return this.myGetItemSimple(strSelect + " " + strWhere);
        }

        /// <summary>
        /// 获得可用科常用药品信息列表
        /// </summary>
        /// <returns>成功返回药科常用品信息数组 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemAvailableListDept(string dept)
        {
            string strSelect = "";  //获得药品信息的SELECT语句
            string strWhere = "";  //获得可用药品信息的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Fee.Item.GetDeptAlwaysUsedItemdrug", ref strSelect) == -1)
            {
                this.Err = "没有找到Fee.Item.GetDeptAlwaysUsedItemdrug字段!";
                return null;
            }
            //格式化SQL语句
            try
            {
                strSelect = string.Format(strSelect, dept);
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                return null;
            }


            //根据SQL语句取药品类数组并返回数组
            return this.myGetItemSimple(strSelect);
        }

        /// <summary>
        /// 获得可用药品信息列表
        /// 可以通过参数选择是否显示部分基本信息字段
        /// </summary>
        /// <param name="IsShowSimple">是否显示简单信息</param>
        /// <returns>成功返回药品信息数组 失败返回null</returns>
        public List<FS.HISFC.Models.Pharmacy.Item> QueryItemAvailableList(bool IsShowSimple)
        {
            string strSelect = "";  //获得药品信息的SELECT语句
            string strWhere = "";  //获得可用药品信息的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetAvailableList.Where", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAvailableList.Where字段!";
                return null;
            }

            //根据SQL语句取药品类数组并返回数组
            if (IsShowSimple)
                return this.myGetItemSimple(strSelect + " " + strWhere);
            else
                return this.myGetItem(strSelect + " " + strWhere);
        }

        /// <summary>
        /// 获得可用药品信息列表
        /// </summary>
        /// <returns>成功返回药品信息 失败返回null</returns>
        public System.Data.DataSet QueryItemValidList()
        {
            string strSelect = "";  //获得药品信息的SELECT语句
            string strWhere = "";  //获得可用药品信息的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetAvailableList.Where", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAvailableList.Where字段!";
                return null;
            }

            System.Data.DataSet ds = new System.Data.DataSet();

            this.ExecQuery(strSelect + " " + strWhere, ref ds);

            if (ds == null || ds.Tables.Count <= 0)
                return null;
            else
                return ds;
        }

        /// <summary>
        /// 根据自定义码获取药品项目信息
        /// </summary>
        /// <param name="userCode">项目自定义码</param>
        /// <returns>成功返回药品项目实体 失败返回null</returns>
        public FS.HISFC.Models.Pharmacy.Item GetItemByUserCode(string userCode)
        {
            FS.HISFC.Models.Pharmacy.Item Item;
            List<FS.HISFC.Models.Pharmacy.Item> alItem = new List<FS.HISFC.Models.Pharmacy.Item>();
            string strSelect = "";  //获得药品信息的SELECT语句
            string strWhere = "";  //根据药品编码获得某一药品信息的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetItem.Where.UserCode", ref strWhere) == -1)
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
                Item = (FS.HISFC.Models.Pharmacy.Item)alItem[0];
                return Item;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 根据最新一次核准入库信息更新字典表内相关信息
        /// </summary>
        /// <param name="input">入库信息实体</param>
        /// <returns>更新成功返回1 无记录返回0 出错返回-1</returns>
        public int UpdateItemInputInfo(FS.HISFC.Models.Pharmacy.Input input)
        {
            string strSQL = "";
            if (this.GetSQL("Pharmacy.Item.UpdateItemInputInfo", ref strSQL) == -1) return -1;
            try
            {
                //2011-05-27 by 处理生产厂家
                string producerNO = input.Producer.ID;
                if (string.IsNullOrEmpty(producerNO))
                {
                    producerNO = input.Item.Product.Producer.ID;
                }
                //增加批准文号的更新 by Sunjh 2010-10-29 {8FB4AE2E-3992-4272-B348-72D3621206C0}
                strSQL = string.Format(strSQL, input.Item.ID, input.Item.PriceCollection.PurchasePrice, input.Company.ID, input.Item.Product.ApprovalInfo, producerNO,FrameWork.Function.NConvert.ToInt32(input.Item.TenderOffer.IsTenderOffer),input.Item.PriceCollection.WholeSalePrice);    //替换SQL语句中的参数。
                //end by
            }
            catch (Exception ex)
            {
                this.Err = "付数值时候出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        #endregion

        #region 对外提供

        /// <summary>
        /// 更新项目笔画码
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public int UpdateCharOrderString(FS.HISFC.Models.Pharmacy.Item itemInfo)
        {
            string strSql = string.Empty;
            if (this.GetSQL("Pharmacy.Item.UpdateItemCharOrderString", ref strSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.UpdateItemCharOrderString";
                return -1;
            }
            strSql = string.Format(strSql, itemInfo.ID, itemInfo.NameCollection.WBCode);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新生产厂家等信息
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <returns></returns>
        public int UpdateItemProductInfo(FS.HISFC.Models.Pharmacy.Item itemInfo)
        {
            string strSql = string.Empty;
            if (this.GetSQL("Pharmacy.Item.UpdateItemProductInfo", ref strSql) == -1)
            {
                this.Err = "没有找到SQL语句Pharmacy.Item.UpdateItemProductInfo";
                return -1;
            }
            strSql = string.Format(strSql, itemInfo.ID, itemInfo.ProductID, itemInfo.Product.Company.ID, itemInfo.Product.Producer.ID,this.Operator.ID);
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 获取药品最新零售价
        /// </summary>
        /// <param name="drugCode">药品编码</param>
        /// <param name="drugPrice">药品零售价</param>
        /// <returns>成功返回1 失败返回－1</returns>
        public int GetNowPrice(string drugCode, ref decimal drugPrice)
        {
            string strSql = "";
            if (this.GetSQL("Pharmacy.Item.GetNowPrice", ref strSql) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetNowPrice字段";
                return -1;
            }

            strSql = string.Format(strSql, drugCode);
            try
            {
                this.ExecQuery(strSql);
                if (this.Reader.Read())
                {
                    drugPrice = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[0].ToString());
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
        /// 根据药品编码获得某一药品信息
        /// </summary>
        /// <param name="ID">药品编码</param>
        /// <returns>成功返回药品实体 失败返回null</returns>
        public FS.HISFC.Models.Pharmacy.Item GetItem(string ID)
        {
            FS.HISFC.Models.Pharmacy.Item Item;
            List<FS.HISFC.Models.Pharmacy.Item> alItem = new List<FS.HISFC.Models.Pharmacy.Item>();
            string strSelect = "";  //获得药品信息的SELECT语句
            string strWhere = "";  //根据药品编码获得某一药品信息的WHERE条件语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            //取WHERE条件语句
            if (this.GetSQL("Pharmacy.Item.GetItem.Where", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetItem.Where字段!";
                return null;
            }

            try
            {
                strWhere = string.Format(strWhere, ID);
            }
            catch
            {
                return null;
            }

            //根据SQL语句取药品类数组并返回数组中的首条记录
            try
            {
                alItem = this.myGetItem(strSelect + " " + strWhere);
                //如果没有取到数据，则返回新实体
                Item = (FS.HISFC.Models.Pharmacy.Item)alItem[0];
                return Item;
            }
            catch
            {
                return null;
            }
        }

        #region 华南修改

        /// <summary>
        /// 根据药品编码和患者科室，获取对应取药药房的库存
        /// 增加发送类型 houwb 2011-5-30
        /// </summary>
        /// <param name="deptCode">患者科室</param>
        /// <param name="drugCode">药品编码</param>
        /// <param name="sendType">发送类型</param>
        /// <returns>药品库存实体 返回Null 发生错误 返回空实体 药房无该药品或库存为零</returns>
        public FS.HISFC.Models.Pharmacy.Storage GetItemStorage(string deptCode, string sendType, string drugCode)
        {
            #region 根据索引获取Sql语句

            string SQLString = "";  //获得药品信息的SELECT语句
            string strWhere = "";   //获得药品信息的where语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorage", ref SQLString) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorage字段!";
                return null;
            }

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetStorage.ByDrugCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetStorage.ByDrugCode字段!";
                return null;
            }

            #endregion

            SQLString = string.Format(SQLString + " " + strWhere, deptCode, sendType, drugCode);

            //根据SQL语句取药品类数组并返回数组
            //FS.HISFC.Models.Pharmacy.Item Item = new FS.HISFC.Models.Pharmacy.Item( ); //返回数组中的药品信息类
            FS.HISFC.Models.Pharmacy.Storage storage = new FS.HISFC.Models.Pharmacy.Storage();

            try
            {
                this.ExecQuery(SQLString);

                if (this.Reader.Read())
                {
                    storage.Item.ID = this.Reader[0].ToString();                               //0  药品编码
                    storage.Item.Name = this.Reader[1].ToString();                                //1  商品名称
                    storage.Item.PackQty = NConvert.ToDecimal(this.Reader[2].ToString());         //2  包装数量
                    storage.Item.Specs = this.Reader[3].ToString();                               //3  规格
                    storage.Item.MinFee.ID = this.Reader[4].ToString();                           //4  最小费用代码
                    storage.Item.SysClass.ID = this.Reader[5].ToString();                         //5  系统类别
                    storage.Item.PackUnit = this.Reader[6].ToString();                            //6  包装单位
                    storage.Item.MinUnit = this.Reader[7].ToString();                             //7  最小单位
                    storage.Item.Type.ID = this.Reader[8].ToString();                             //8  药品类别编码
                    storage.Item.Quality.ID = this.Reader[9].ToString();                          //9  药品性质编码
                    storage.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[10].ToString());    //10 零售价
                    storage.Item.Product.Producer.ID = this.Reader[11].ToString();                //11 生产厂家编码
                    storage.Item.SpellCode = this.Reader[12].ToString();                          //12 拼音码  
                    storage.Item.WBCode = this.Reader[13].ToString();                             //13 五笔码
                    storage.Item.UserCode = this.Reader[14].ToString();                           //14 自定义码
                    storage.Item.NameCollection.RegularName = this.Reader[15].ToString();         //15 药品通用名
                    storage.Item.NameCollection.RegularSpell.SpellCode = this.Reader[16].ToString();        //16 通用名拼音码
                    storage.Item.NameCollection.RegularSpell.WBCode = this.Reader[17].ToString(); //17 通用名五笔码
                    storage.Item.NameCollection.OtherSpell.SpellCode = this.Reader[18].ToString();//18 别名拼音码
                    storage.Item.NameCollection.EnglishName = this.Reader[19].ToString();         //19 英文商品名 

                    //storage.Item.User01 = this.Reader[20].ToString();                            //20 库存可用数量
                    //storage.Item.User02 = this.Reader[21].ToString();                            //21 药房编码
                    storage.StoreQty = NConvert.ToDecimal(this.Reader[20].ToString());
                    storage.StockDept.ID = this.Reader[21].ToString();

                    storage.Item.DoseUnit = this.Reader[22].ToString();                            //22 剂量单位
                    storage.Item.BaseDose = NConvert.ToDecimal(this.Reader[23].ToString());        //23 基本剂量
                    storage.Item.DosageForm.ID = this.Reader[24].ToString();					   //24 剂型编码
                    storage.Item.Usage.ID = this.Reader[25].ToString();							   //25 用法编码
                    storage.Item.Frequency.ID = this.Reader[26].ToString();						   //26 频次编码	
                    storage.Item.Grade = this.Reader[27].ToString();						       //27 药品等级：甲乙类
                    storage.Item.SpecialFlag = this.Reader[28].ToString();						   //28 省限
                    storage.Item.SpecialFlag1 = this.Reader[29].ToString();						   //29 市限	
                    storage.Item.SpecialFlag2 = this.Reader[30].ToString();					   //30 自费	
                    storage.Item.SpecialFlag3 = this.Reader[31].ToString();						   //31 特殊项目	

                    if (this.Reader.FieldCount > 32)
                    {
                        storage.Item.SplitType = this.Reader[32].ToString();//门诊可拆分属性
                    }

                    if (this.Reader.FieldCount > 33)
                    {
                        storage.Item.RetailPrice2 = NConvert.ToDecimal(this.Reader[33]); // 第二零售价
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得药品库存信息时，执行SQL语句出错！" + ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return storage;
        }

        #endregion

        /// <summary>
        /// 根据药品编码和患者科室，获取住院医嘱、收费使用的药品数据
        /// </summary>
        /// <param name="deptCode">患者科室</param>
        /// <param name="drugCode">药品编码</param>
        /// <returns>药品库存实体 返回Null 发生错误 返回空实体 药房无该药品或库存为零</returns>
        [Obsolete("重构整合 更改返回值类型为Storage", false)]
        public FS.HISFC.Models.Pharmacy.Storage GetItemForInpatient(string deptCode, string drugCode)
        {
            #region 根据索引获取Sql语句

            string SQLString = "";  //获得药品信息的SELECT语句
            string strWhere = "";   //获得药品信息的where语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetAvailableList.Inpatient", ref SQLString) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAvailableList.Inpatient字段!";
                return null;
            }

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetAvailableList.Inpatient.ByDrugCode", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAvailableList.Inpatient.ByDrugCode字段!";
                return null;
            }

            #endregion

            SQLString = string.Format(SQLString + " " + strWhere, deptCode, drugCode);

            //根据SQL语句取药品类数组并返回数组
            //FS.HISFC.Models.Pharmacy.Item Item = new FS.HISFC.Models.Pharmacy.Item( ); //返回数组中的药品信息类
            FS.HISFC.Models.Pharmacy.Storage storage = new FS.HISFC.Models.Pharmacy.Storage();

            try
            {
                this.ExecQuery(SQLString);

                if (this.Reader.Read())
                {
                    storage.Item.ID = this.Reader[0].ToString();                               //0  药品编码
                    storage.Item.Name = this.Reader[1].ToString();                                //1  商品名称
                    storage.Item.PackQty = NConvert.ToDecimal(this.Reader[2].ToString());         //2  包装数量
                    storage.Item.Specs = this.Reader[3].ToString();                               //3  规格
                    storage.Item.MinFee.ID = this.Reader[4].ToString();                           //4  最小费用代码
                    storage.Item.SysClass.ID = this.Reader[5].ToString();                         //5  系统类别
                    storage.Item.PackUnit = this.Reader[6].ToString();                            //6  包装单位
                    storage.Item.MinUnit = this.Reader[7].ToString();                             //7  最小单位
                    storage.Item.Type.ID = this.Reader[8].ToString();                             //8  药品类别编码
                    storage.Item.Quality.ID = this.Reader[9].ToString();                          //9  药品性质编码
                    storage.Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[10].ToString());    //10 零售价
                    storage.Item.Product.Producer.ID = this.Reader[11].ToString();                //11 生产厂家编码
                    storage.Item.SpellCode = this.Reader[12].ToString();                          //12 拼音码  
                    storage.Item.WBCode = this.Reader[13].ToString();                             //13 五笔码
                    storage.Item.UserCode = this.Reader[14].ToString();                           //14 自定义码
                    storage.Item.NameCollection.RegularName = this.Reader[15].ToString();         //15 药品通用名
                    storage.Item.NameCollection.RegularSpell.SpellCode = this.Reader[16].ToString();        //16 通用名拼音码
                    storage.Item.NameCollection.RegularSpell.WBCode = this.Reader[17].ToString(); //17 通用名五笔码
                    storage.Item.NameCollection.OtherSpell.SpellCode = this.Reader[18].ToString();//18 别名拼音码
                    storage.Item.NameCollection.EnglishName = this.Reader[19].ToString();         //19 英文商品名 

                    //storage.Item.User01 = this.Reader[20].ToString();                            //20 库存可用数量
                    //storage.Item.User02 = this.Reader[21].ToString();                            //21 药房编码
                    storage.StoreQty = NConvert.ToDecimal(this.Reader[20].ToString());
                    storage.StockDept.ID = this.Reader[21].ToString();

                    storage.Item.DoseUnit = this.Reader[22].ToString();                            //22 剂量单位
                    storage.Item.BaseDose = NConvert.ToDecimal(this.Reader[23].ToString());        //23 基本剂量
                    storage.Item.DosageForm.ID = this.Reader[24].ToString();					   //24 剂型编码
                    storage.Item.Usage.ID = this.Reader[25].ToString();							   //25 用法编码
                    storage.Item.Frequency.ID = this.Reader[26].ToString();						   //26 频次编码	
                    storage.Item.Grade = this.Reader[27].ToString();						       //27 药品等级：甲乙类
                    storage.Item.SpecialFlag = this.Reader[28].ToString();						   //28 省限
                    storage.Item.SpecialFlag1 = this.Reader[29].ToString();						   //29 市限	
                    storage.Item.SpecialFlag2 = this.Reader[30].ToString();					   //30 自费	
                    storage.Item.SpecialFlag3 = this.Reader[31].ToString();						   //31 特殊项目	

                    if (this.Reader.FieldCount > 32)
                    {
                        storage.Item.SplitType = this.Reader[32].ToString();//门诊可拆分属性
                    }

                    if (this.Reader.FieldCount > 33)
                    {
                        storage.Item.RetailPrice2 = NConvert.ToDecimal(this.Reader[33]); // 第二零售价
                    }
                }
            }
            catch (Exception ex)
            {
                this.Err = "获得药品库存信息时，执行SQL语句出错！" + ex.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return storage;
        }

        /// <summary>
        /// 获取门诊医嘱、收费使用的药品数据
        /// </summary>
        /// <param name="deptCode">取药病区</param>
        /// <returns>成功返回药品数组 失败返回null</returns>
        public ArrayList QueryItemAvailableListForClinic(string deptCode)
        {
            string SQLString = "";  //获得药品信息的SELECT语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetAvailableList.OutPatient", ref SQLString) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAvailableList.OutPatient字段!";
                return null;
            }

            SQLString = string.Format(SQLString, deptCode);
            //根据SQL语句取药品类数组并返回数组
            FS.HISFC.Models.Pharmacy.Item Item; //返回数组中的药品信息类
            ArrayList al = new ArrayList();

            try
            {
                this.ExecQuery(SQLString);

                while (this.Reader.Read())
                {
                    Item = new FS.HISFC.Models.Pharmacy.Item();

                    Item.ID = this.Reader[0].ToString();                                  //0  药品编码
                    Item.Name = this.Reader[1].ToString();                                //1  商品名称
                    Item.PackQty = NConvert.ToDecimal(this.Reader[2].ToString());         //2  包装数量
                    Item.Specs = this.Reader[3].ToString();                               //3  规格
                    Item.MinFee.ID = this.Reader[4].ToString();                           //4  最小费用代码
                    Item.SysClass.ID = this.Reader[5].ToString();                         //5  系统类别
                    Item.PackUnit = this.Reader[6].ToString();                            //6  包装单位
                    Item.MinUnit = this.Reader[7].ToString();                             //7  最小单位
                    Item.Type.ID = this.Reader[8].ToString();                             //8  药品类别编码
                    Item.Quality.ID = this.Reader[9].ToString();                          //9  药品性质编码
                    Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[10].ToString());    //10 零售价
                    Item.Product.Producer.ID = this.Reader[11].ToString();                        //11 生产厂家编码
                    Item.SpellCode = this.Reader[12].ToString();                         //12 拼音码  
                    Item.WBCode = this.Reader[13].ToString();                            //13 五笔码
                    Item.UserCode = this.Reader[14].ToString();                          //14 自定义码
                    Item.NameCollection.RegularName = this.Reader[15].ToString();                        //15 药品通用名
                    Item.NameCollection.RegularSpell.SpellCode = this.Reader[16].ToString();        //16 通用名拼音码
                    Item.NameCollection.RegularSpell.WBCode = this.Reader[17].ToString();           //17 通用名五笔码
                    Item.NameCollection.OtherSpell.SpellCode = this.Reader[18].ToString();          //18 别名拼音码
                    Item.NameCollection.EnglishName = this.Reader[19].ToString();                        //19 英文商品名 
                    Item.User01 = this.Reader[20].ToString();                             //20 库存可用数量
                    Item.User02 = this.Reader[21].ToString();                             //21 药房编码
                    Item.DoseUnit = this.Reader[22].ToString();                           //22 剂量单位
                    Item.BaseDose = NConvert.ToDecimal(this.Reader[23].ToString());       //23 基本剂量
                    Item.DosageForm.ID = this.Reader[24].ToString();					  //24 剂型编码
                    Item.Usage.ID = this.Reader[25].ToString();							  //25 用法编码
                    Item.Frequency.ID = this.Reader[26].ToString();						  //26 频次编码
                    //Item.Grade = this.Reader[27].ToString();						      //27 药品等级：甲乙类
                    Item.SpecialFlag = this.Reader[28].ToString();						  //28 省限
                    Item.SpecialFlag1 = this.Reader[29].ToString();						  //29 市限	
                    Item.SpecialFlag2 = this.Reader[30].ToString();						  //30 自费	
                    Item.SpecialFlag3 = this.Reader[31].ToString();						  //31 特殊项目	
                    Item.PhyFunction1.ID = this.Reader[32].ToString();                       //32 药理作用		

                    al.Add(Item);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = "获得药品基本信息时，执行SQL语句出错！" + ex.Message;
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
        /// 获取科常用的药品数据
        /// </summary>
        /// <param name="deptCode">取药病区</param>
        /// <returns>成功返回药品数组 失败返回null</returns>
        public ArrayList QueryDeptAlwaysUsedItem(string deptCode)
        {
            string SQLString = "";  //获得药品信息的SELECT语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetDeptAlwaysUsedDurg", ref SQLString) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetDeptAlwaysUsedDurg字段!";
                return null;
            }

            SQLString = string.Format(SQLString, deptCode);
            //根据SQL语句取药品类数组并返回数组
            FS.HISFC.Models.Pharmacy.Item Item; //返回数组中的药品信息类
            ArrayList al = new ArrayList();

            try
            {
                this.ExecQuery(SQLString);

                while (this.Reader.Read())
                {
                    Item = new FS.HISFC.Models.Pharmacy.Item();

                    Item.ID = this.Reader[0].ToString();                                  //0  药品编码
                    Item.Name = this.Reader[1].ToString();                                //1  商品名称
                    Item.PackQty = NConvert.ToDecimal(this.Reader[2].ToString());         //2  包装数量
                    Item.Specs = this.Reader[3].ToString();                               //3  规格
                    Item.MinFee.ID = this.Reader[4].ToString();                           //4  最小费用代码
                    Item.SysClass.ID = this.Reader[5].ToString();                         //5  系统类别
                    Item.PackUnit = this.Reader[6].ToString();                            //6  包装单位
                    Item.MinUnit = this.Reader[7].ToString();                             //7  最小单位
                    Item.Type.ID = this.Reader[8].ToString();                             //8  药品类别编码
                    Item.Quality.ID = this.Reader[9].ToString();                          //9  药品性质编码
                    Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[10].ToString());    //10 零售价
                    Item.Product.Producer.ID = this.Reader[11].ToString();                        //11 生产厂家编码
                    Item.SpellCode = this.Reader[12].ToString();                         //12 拼音码  
                    Item.WBCode = this.Reader[13].ToString();                            //13 五笔码
                    Item.UserCode = this.Reader[14].ToString();                          //14 自定义码
                    Item.NameCollection.RegularName = this.Reader[15].ToString();                        //15 药品通用名
                    Item.NameCollection.RegularSpell.SpellCode = this.Reader[16].ToString();        //16 通用名拼音码
                    Item.NameCollection.RegularSpell.WBCode = this.Reader[17].ToString();           //17 通用名五笔码
                    Item.NameCollection.OtherSpell.SpellCode = this.Reader[18].ToString();          //18 别名拼音码
                    Item.NameCollection.EnglishName = this.Reader[19].ToString();                        //19 英文商品名 
                    Item.User01 = this.Reader[20].ToString();                             //20 库存可用数量
                    Item.User02 = this.Reader[21].ToString();                             //21 药房编码
                    Item.DoseUnit = this.Reader[22].ToString();                           //22 剂量单位
                    Item.BaseDose = NConvert.ToDecimal(this.Reader[23].ToString());       //23 基本剂量
                    Item.DosageForm.ID = this.Reader[24].ToString();					  //24 剂型编码
                    Item.Usage.ID = this.Reader[25].ToString();							  //25 用法编码
                    Item.Frequency.ID = this.Reader[26].ToString();						  //26 频次编码
                    //Item.Grade = this.Reader[27].ToString();						      //27 药品等级：甲乙类
                    Item.SpecialFlag = this.Reader[28].ToString();						  //28 省限
                    Item.SpecialFlag1 = this.Reader[29].ToString();						  //29 市限	
                    Item.SpecialFlag2 = this.Reader[30].ToString();						  //30 自费	
                    Item.SpecialFlag3 = this.Reader[31].ToString();						  //31 特殊项目	

                    al.Add(Item);
                }
                return al;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得药品基本信息时，执行SQL语句出错！" + ex.Message;
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
        /// 获取住院医嘱、收费使用的药品数据
        /// </summary>
        /// <param name="deptCode">取药病区</param>
        /// <returns>成功返回药品数组 失败返回null</returns>
        public ArrayList QueryItemAvailableList(string deptCode)
        {
            string SQLString = "";  //获得药品信息的SELECT语句

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetAvailableList.Inpatient", ref SQLString) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAvailableList.Inpatient字段!";
                return null;
            }

            SQLString = string.Format(SQLString, deptCode);
            //根据SQL语句取药品类数组并返回数组
            FS.HISFC.Models.Pharmacy.Item Item; //返回数组中的药品信息类

            ArrayList al = new ArrayList();

            try
            {
                this.ExecQuery(SQLString);

                while (this.Reader.Read())
                {
                    Item = new FS.HISFC.Models.Pharmacy.Item();

                    Item.ID = this.Reader[0].ToString();                                  //0  药品编码
                    Item.Name = this.Reader[1].ToString();                                //1  商品名称
                    Item.PackQty = NConvert.ToDecimal(this.Reader[2].ToString());         //2  包装数量
                    Item.Specs = this.Reader[3].ToString();                               //3  规格
                    Item.MinFee.ID = this.Reader[4].ToString();                           //4  最小费用代码
                    Item.SysClass.ID = this.Reader[5].ToString();                         //5  系统类别
                    Item.PackUnit = this.Reader[6].ToString();                            //6  包装单位
                    Item.MinUnit = this.Reader[7].ToString();                             //7  最小单位
                    Item.Type.ID = this.Reader[8].ToString();                             //8  药品类别编码
                    Item.Quality.ID = this.Reader[9].ToString();                          //9  药品性质编码
                    Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[10].ToString());    //10 零售价
                    Item.Product.Producer.ID = this.Reader[11].ToString();                        //11 生产厂家编码
                    Item.SpellCode = this.Reader[12].ToString();                         //12 拼音码  
                    Item.WBCode = this.Reader[13].ToString();                            //13 五笔码
                    Item.UserCode = this.Reader[14].ToString();                          //14 自定义码
                    Item.NameCollection.RegularName = this.Reader[15].ToString();                        //15 药品通用名
                    Item.NameCollection.RegularSpell.SpellCode = this.Reader[16].ToString();        //16 通用名拼音码
                    Item.NameCollection.RegularSpell.WBCode = this.Reader[17].ToString();           //17 通用名五笔码
                    Item.NameCollection.OtherSpell.SpellCode = this.Reader[18].ToString();          //18 别名拼音码
                    Item.NameCollection.EnglishName = this.Reader[19].ToString();                        //19 英文商品名 
                    Item.User01 = this.Reader[20].ToString();                             //20 库存可用数量
                    Item.User02 = this.Reader[21].ToString();                             //21 药房编码
                    Item.DoseUnit = this.Reader[22].ToString();                           //22 剂量单位
                    Item.BaseDose = NConvert.ToDecimal(this.Reader[23].ToString());       //23 基本剂量
                    Item.DosageForm.ID = this.Reader[24].ToString();					  //24 剂型编码
                    Item.Usage.ID = this.Reader[25].ToString();							  //25 用法编码
                    Item.Frequency.ID = this.Reader[26].ToString();						  //26 频次编码
                    //Item.Grade = this.Reader[27].ToString();						      //27 药品等级：甲乙类
                    Item.SpecialFlag = this.Reader[28].ToString();						  //28 省限
                    Item.SpecialFlag1 = this.Reader[29].ToString();						  //29 市限	
                    Item.SpecialFlag2 = this.Reader[30].ToString();						  //30 自费	
                    Item.SpecialFlag3 = this.Reader[31].ToString();						  //31 特殊项目	

                    al.Add(Item);
                }
                return al;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得药品基本信息时，执行SQL语句出错！" + ex.Message;
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
        /// 获取住院医嘱、收费使用的某一类别的药品数据
        /// </summary>
        /// <param name="deptCode">取药病区</param>
        /// <param name="drugType">药品类别 传入ALL获取全部药品类别</param>
        /// <returns>成功返回药品列表 失败返回null</returns>
        public ArrayList QueryItemAvailableList(string deptCode, string drugType)
        {
            string SQLString = "";  //获得药品信息的SELECT语句
            string strWhere = "";

            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetAvailableList.Inpatient", ref SQLString) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAvailableList.Inpatient字段!";
                return null;
            }
            //取SELECT语句
            if (this.GetSQL("Pharmacy.Item.GetAvailableList.Inpatient.ByDrugType", ref strWhere) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.GetAvailableList.Inpatient.ByDrugType字段!";
                return null;
            }
            SQLString = string.Format(SQLString + " " + strWhere, deptCode, drugType);
            //根据SQL语句取药品类数组并返回数组
            FS.HISFC.Models.Pharmacy.Item Item; //返回数组中的药品信息类

            ArrayList al = new ArrayList();
            try
            {
                this.ExecQuery(SQLString);

                while (this.Reader.Read())
                {
                    Item = new FS.HISFC.Models.Pharmacy.Item();

                    Item.ID = this.Reader[0].ToString();                                  //0  药品编码
                    Item.Name = this.Reader[1].ToString();                                //1  商品名称
                    Item.PackQty = NConvert.ToDecimal(this.Reader[2].ToString());         //2  包装数量
                    Item.Specs = this.Reader[3].ToString();                               //3  规格
                    Item.MinFee.ID = this.Reader[4].ToString();                           //4  最小费用代码
                    Item.SysClass.ID = this.Reader[5].ToString();                         //5  系统类别
                    Item.PackUnit = this.Reader[6].ToString();                            //6  包装单位
                    Item.MinUnit = this.Reader[7].ToString();                             //7  最小单位
                    Item.Type.ID = this.Reader[8].ToString();                             //8  药品类别编码
                    Item.Quality.ID = this.Reader[9].ToString();                          //9  药品性质编码
                    Item.PriceCollection.RetailPrice = NConvert.ToDecimal(this.Reader[10].ToString());    //10 零售价
                    Item.Product.Producer.ID = this.Reader[11].ToString();                        //11 生产厂家编码
                    Item.SpellCode = this.Reader[12].ToString();                         //12 拼音码  
                    Item.WBCode = this.Reader[13].ToString();                            //13 五笔码
                    Item.UserCode = this.Reader[14].ToString();                          //14 自定义码
                    Item.NameCollection.RegularName = this.Reader[15].ToString();                        //15 药品通用名
                    Item.NameCollection.RegularSpell.SpellCode = this.Reader[16].ToString();        //16 通用名拼音码
                    Item.NameCollection.RegularSpell.WBCode = this.Reader[17].ToString();           //17 通用名五笔码
                    Item.NameCollection.RegularSpell.UserCode = this.Reader[18].ToString();         //18 通用名自定义码
                    Item.NameCollection.EnglishName = this.Reader[19].ToString();                        //19 英文商品名 
                    Item.User01 = this.Reader[20].ToString();                             //20 库存可用数量
                    Item.User02 = this.Reader[21].ToString();                             //21 药房编码
                    Item.DoseUnit = this.Reader[22].ToString();                           //22 剂量单位
                    Item.BaseDose = NConvert.ToDecimal(this.Reader[23].ToString());       //23 基本剂量
                    Item.DosageForm.ID = this.Reader[24].ToString();					  //24 剂型编码
                    Item.Usage.ID = this.Reader[25].ToString();							  //25 用法编码
                    Item.Frequency.ID = this.Reader[26].ToString();						  //26 频次编码
                    //Item.Grade = this.Reader[27].ToString();						      //27 药品等级：甲乙类
                    Item.SpecialFlag = this.Reader[28].ToString();						  //28 省限
                    Item.SpecialFlag1 = this.Reader[29].ToString();						  //29 市限	
                    Item.SpecialFlag2 = this.Reader[30].ToString();						  //30 自费	
                    Item.SpecialFlag3 = this.Reader[31].ToString();						  //31 特殊项目	

                    al.Add(Item);
                }
                return al;
            }//抛出错误
            catch (Exception ex)
            {
                this.Err = "获得药品基本信息时，执行SQL语句出错！" + ex.Message;
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

        #endregion
    }
}
