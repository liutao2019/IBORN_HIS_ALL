using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.Fee.BizLogic
{
    /// <summary>
    /// [功能描述:合同单位SOC业务层，从核心版本独立出来，只对SOC层有效]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public class PactInfo : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 获得合同单位数组
        /// </summary>
        /// <param name="pactUnit">合同单位实体</param>
        /// <returns>获得合同单位数组</returns>
        private string[] getPactUnitParams(FS.HISFC.Models.Base.PactInfo pactUnit)
        {
            string[] args =
				{	
					pactUnit.ID,//0
                    pactUnit.PayKind.ID ,
                    pactUnit.Rate.PubRate.ToString(),
                    pactUnit.Rate.PayRate.ToString(),
                    pactUnit.Rate.OwnRate.ToString(),
                    pactUnit.Rate.RebateRate.ToString(),//5
                    pactUnit.Rate.ArrearageRate.ToString(),
                    NConvert.ToInt32(pactUnit.Rate.IsBabyShared).ToString(),
                    NConvert.ToInt32(pactUnit.IsNeedMCard).ToString(),
                    NConvert.ToInt32(pactUnit.IsInControl).ToString(),
                    pactUnit.ItemType,//10
                    pactUnit.DayQuota.ToString(),
                    pactUnit.MonthQuota.ToString(),
                    pactUnit.YearQuota.ToString(),
                    pactUnit.OnceQuota.ToString(),
                    pactUnit.PriceForm,//15
                    pactUnit.BedQuota.ToString(),
                    pactUnit.AirConditionQuota.ToString(),
                    pactUnit.SortID.ToString(),
                    this.Operator.ID,
                    pactUnit.ShortName,//20
                    pactUnit.PactDllName,
                    pactUnit.PactDllDescription,
                    pactUnit.PactSystemType,
                    pactUnit.ValidState,
                    pactUnit.SpellCode,//25
                    pactUnit.WBCode,
                    pactUnit.Name  //added by huangchw 12-11-16
				};

            return args;
        }

        /// <summary>
        /// 获得插入合同单位信息数组
        /// </summary>
        /// <param name="pactUnit">合同单位实体</param>
        /// <returns>获得合同单位数组</returns>
        private string[] getInsertPactUnitParams(FS.HISFC.Models.Base.PactInfo pactUnit)
        {
            string[] args =
				{	
					pactUnit.ID,//0
                    pactUnit.Name,
                    pactUnit.PayKind.ID ,
                    pactUnit.PriceForm,
                    pactUnit.Rate.PubRate.ToString(),
                    pactUnit.Rate.PayRate.ToString(),//5
                    pactUnit.Rate.OwnRate.ToString(),
                    pactUnit.Rate.RebateRate.ToString(),
                    pactUnit.Rate.ArrearageRate.ToString(),
                    NConvert.ToInt32(pactUnit.Rate.IsBabyShared).ToString(),
                    NConvert.ToInt32(pactUnit.IsNeedMCard).ToString(),//10
                    NConvert.ToInt32(pactUnit.IsInControl).ToString(),
                    pactUnit.ItemType,
                    pactUnit.DayQuota.ToString(),
                    pactUnit.MonthQuota.ToString(),
                    pactUnit.YearQuota.ToString(),//15
                    pactUnit.OnceQuota.ToString(),
                    pactUnit.BedQuota.ToString(),
                    pactUnit.AirConditionQuota.ToString(),
                    pactUnit.SortID.ToString(),
                    this.Operator.ID,//20
                    pactUnit.ShortName,
                    pactUnit.PactDllName,
                    pactUnit.PactDllDescription,
                    pactUnit.PactSystemType,
                    pactUnit.ValidState,//25
                    pactUnit.SpellCode,
                    pactUnit.WBCode
				};

            return args;
        }

        private int myExecNoQuery(string strSql, params string[] param)
        {
            try
            {
                strSql = string.Format(strSql, param);
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
        /// 根据SQL语句查询合同单位信息
        /// </summary>
        /// <param name="sql">查询得SQL语句</param>
        /// <param name="args">参数</param>
        /// <returns>成功 合同单位信息集合 失败 null</returns>
        private ArrayList myQueryPactUnitBySql(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList pactUnitList = new ArrayList();//费用明细数组
            FS.HISFC.Models.Base.PactInfo pactUnit = null;

            try
            {
                while (this.Reader.Read())
                {
                    pactUnit = new FS.HISFC.Models.Base.PactInfo();
                    pactUnit.ID = this.Reader[0].ToString();//合同代码          
                    pactUnit.Name = this.Reader[1].ToString();//合同单位名称                    
                    pactUnit.PayKind.ID = this.Reader[2].ToString();//结算类别                    
                    pactUnit.Rate.PubRate = NConvert.ToDecimal(Reader[3].ToString().Trim());//公费比例                    
                    pactUnit.Rate.PayRate = NConvert.ToDecimal(Reader[4].ToString().Trim());//自付比例                   
                    pactUnit.Rate.OwnRate = NConvert.ToDecimal(Reader[5].ToString().Trim()); //自费比例                   
                    pactUnit.Rate.RebateRate = NConvert.ToDecimal(Reader[6].ToString().Trim()); //优惠比例                    
                    pactUnit.Rate.ArrearageRate = NConvert.ToDecimal(Reader[7].ToString().Trim());//欠费比例                    
                    pactUnit.Rate.IsBabyShared = NConvert.ToBoolean(Reader[8].ToString());//婴儿标志 0 无关 1 有关                
                    pactUnit.IsNeedMCard = NConvert.ToBoolean(Reader[9].ToString().Trim()); //是否要求必须有医疗证号 0 否 1 是                      
                    pactUnit.IsInControl = NConvert.ToBoolean(Reader[10].ToString().Trim());//是否受监控 1受监控0不受监控
                    pactUnit.ItemType = Reader[11].ToString().Trim(); //标志  0 全部 1 药品 2 非药品   
                    pactUnit.DayQuota = NConvert.ToDecimal(Reader[12].ToString().Trim());//日限额                     
                    pactUnit.MonthQuota = NConvert.ToDecimal(Reader[13].ToString().Trim()); //月限额                    
                    pactUnit.YearQuota = NConvert.ToDecimal(Reader[14].ToString().Trim());//年限额
                    pactUnit.OnceQuota = NConvert.ToDecimal(Reader[15].ToString().Trim());//一次限
                    pactUnit.PriceForm = Reader[16].ToString();
                    pactUnit.BedQuota = NConvert.ToDecimal(Reader[17].ToString());//床位限额
                    pactUnit.AirConditionQuota = NConvert.ToDecimal(Reader[18].ToString());//空调限额
                    pactUnit.SortID = NConvert.ToInt32(Reader[19]);//序号             
                    pactUnit.ShortName = Reader[20].ToString();//合同单位简称
                    pactUnit.PactDllName = Reader[21].ToString(); //待遇dll名称
                    pactUnit.PactDllDescription = Reader[22].ToString();//待遇dll说明
                    pactUnit.SpellCode = Reader[23].ToString();//拼音码
                    pactUnit.WBCode = Reader[24].ToString();//五笔码
                    pactUnit.PactSystemType = Reader[25].ToString().Trim();
                    pactUnit.ValidState = Reader[26].ToString().Trim();
                    pactUnitList.Add(pactUnit);

                }
                return pactUnitList;
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();
                return null;
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 获取所有的合同单位
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitAll()
        {
            return this.myQueryPactUnitBySql(FS.SOC.HISFC.Fee.Data.AbstractPactInfo.Current.SelectAll + FS.SOC.HISFC.Fee.Data.AbstractPactInfo.Current.OrderBySortID);
        }

        /// <summary>
        /// 获取住院用合同单位
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitForInpatient()
        {
            return this.myQueryPactUnitBySql(FS.SOC.HISFC.Fee.Data.AbstractPactInfo.Current.SelectAll + string.Format(FS.SOC.HISFC.Fee.Data.AbstractPactInfo.Current.WhereBySystemType, "'0','2'") + FS.SOC.HISFC.Fee.Data.AbstractPactInfo.Current.OrderBySortID);
        }

        /// <summary>
        /// 获取门诊用合同单位
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryPactUnitForOutpatient()
        {
            return this.myQueryPactUnitBySql(FS.SOC.HISFC.Fee.Data.AbstractPactInfo.Current.SelectAll + string.Format(FS.SOC.HISFC.Fee.Data.AbstractPactInfo.Current.WhereBySystemType, "'0','1'") + FS.SOC.HISFC.Fee.Data.AbstractPactInfo.Current.OrderBySortID);
        }

        /// <summary>
        /// 更新合同单位信息
        /// </summary>
        /// <param name="pactUnit">合同单位实体</param>
        /// <returns>成功 1 失败 -1</returns>
        public int Update(FS.HISFC.Models.Base.PactInfo pactUnit)
        {
            return this.myExecNoQuery(FS.SOC.HISFC.Fee.Data.AbstractPactInfo.Current.Update, this.getPactUnitParams(pactUnit));
        }

        /// <summary>
        /// 插入合同单位信息
        /// </summary>
        /// <param name="pactUnit">合同单位实体</param>
        /// <returns>成功 1 失败 -1</returns>
        public int Insert(FS.HISFC.Models.Base.PactInfo pactUnit)
        {
            return this.myExecNoQuery(FS.SOC.HISFC.Fee.Data.AbstractPactInfo.Current.Insert, this.getInsertPactUnitParams(pactUnit));
        }

        /// <summary>
        /// 获取新合同单位编号
        /// </summary>
        /// <returns></returns>
        public string GetNewPactCode()
        {
            return this.ExecSqlReturnOne(FS.SOC.HISFC.Fee.Data.AbstractPactInfo.Current.AutoID);
        }
    }
}
