using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.BizProcess.MessagePattern.BizLogic
{
    public  class DrugItem:FS.FrameWork.Management.Database
    {

        #region 欧娲摆药相关

        /// <summary>
        /// 获得允许欧娲设备的入药
        /// </summary>
        /// <param name="barCode">条形码</param>
        /// <returns>药品基本信息</returns>
        public FS.HISFC.Models.Pharmacy.Item GetItemForRowa(string barCode)
        {
            FS.HISFC.Models.Pharmacy.Item Item = new FS.HISFC.Models.Pharmacy.Item();
            List<FS.HISFC.Models.Pharmacy.Item> alItem = new List<FS.HISFC.Models.Pharmacy.Item>();
            string strSelect = "";  //获得药品信息的SELECT语句
            string strWhere = "";  //根据药品编码获得某一药品信息的WHERE条件语句

            //取SELECT语句
            if (this.Sql.GetSql("Pharmacy.Item.Info", ref strSelect) == -1)
            {
                this.Err = "没有找到Pharmacy.Item.Info字段!";
                return null;
            }

            strWhere = "   where pha_com_baseinfo.bar_code='{0}'";

            try
            {
                strWhere = string.Format(strWhere, barCode);
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
                if (alItem != null)
                {
                    Item = (FS.HISFC.Models.Pharmacy.Item)alItem[0];
                }
                return Item;
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


 

        #endregion

    }
}
