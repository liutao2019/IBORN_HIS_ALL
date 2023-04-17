using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Fee.Item;
using System.Collections;
using System.Data;
using FS.FrameWork.Function;

namespace FS.SOC.HISFC.InpatientFee.BizLogic
{
    /// <summary>
    /// [功能描述: 规则收费业务类]<br></br>
    /// [创 建 者: jing.zhao]<br></br>
    /// [创建时间: 2012-3]<br></br>
    /// </summary>
    public class FeeRegular:FS.FrameWork.Management.Database
    {
        private object[] myGetParams(UndrugFeeRegular undrugFeeRegular)
        {
            return new object[] {
                Int32.Parse(undrugFeeRegular.ID), 
                undrugFeeRegular.Item.ID.ToString(), 
                undrugFeeRegular.Item.Name.ToString(), 
                undrugFeeRegular.LimitCondition.ToString(), 
                undrugFeeRegular.Regular.ID.ToString(), 
                NConvert.ToInt32(undrugFeeRegular.DayLimit.ToString()), 
                undrugFeeRegular.LimitItem.ID.ToString(), 
                undrugFeeRegular.Oper.ID.ToString(), 
                undrugFeeRegular.Oper.OperTime.ToString("yyyy-MM-dd HH:mm:ss"), 
                NConvert.ToInt32(undrugFeeRegular.IsOutFee) 
            };
        }

        private List<FS.HISFC.Models.Fee.Item.UndrugFeeRegular> myQueryList(string sql, params object[] param)
        {
            try
            {
                if (this.ExecQuery(sql) == -1)
                {
                    this.Err = "执行Sql语句出错";
                    return null;
                }
                List<FS.HISFC.Models.Fee.Item.UndrugFeeRegular> feeRegularList = new List<FS.HISFC.Models.Fee.Item.UndrugFeeRegular>();
                UndrugFeeRegular undrugFeeRegular = null;
                while (this.Reader.Read())
                {
                    undrugFeeRegular = new UndrugFeeRegular();
                    undrugFeeRegular.ID = this.Reader[0].ToString();
                    undrugFeeRegular.Item.ID = this.Reader[1].ToString();
                    undrugFeeRegular.Item.Name = this.Reader[2].ToString();
                    undrugFeeRegular.LimitCondition = this.Reader[3].ToString();
                    undrugFeeRegular.Regular.ID = this.Reader[4].ToString();
                    undrugFeeRegular.DayLimit = NConvert.ToDecimal(this.Reader[5].ToString());
                    undrugFeeRegular.LimitItem.ID = this.Reader[6].ToString();
                    undrugFeeRegular.Oper.ID = this.Reader[7].ToString();
                    undrugFeeRegular.Oper.OperTime = NConvert.ToDateTime(this.Reader[8].ToString());
                    undrugFeeRegular.IsOutFee = NConvert.ToBoolean(this.Reader[9].ToString());

                    feeRegularList.Add(undrugFeeRegular);
                }

                return feeRegularList;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                WriteErr();
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
        /// 查找单个非药品收费规则信息
        /// </summary>
        /// <param name="itemcode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Fee.Item.UndrugFeeRegular> QueryByItemCode(string itemcode)
        {
            string querySql = FS.SOC.HISFC.InpatientFee.Data.AbstractFeeRegular.Current.SelectAll + FS.SOC.HISFC.InpatientFee.Data.AbstractFeeRegular.Current.WhereByItemCode;

            return this.myQueryList(querySql, itemcode);
        }

        /// <summary>
        /// 更新非药品收费规则
        /// </summary>
        /// <param name="itemcode"></param>
        /// <returns></returns>
        public int Update(UndrugFeeRegular undrugFeeRegular)
        {
            string updateSql = FS.SOC.HISFC.InpatientFee.Data.AbstractFeeRegular.Current.Update;

            try
            {
                updateSql = string.Format(updateSql, this.myGetParams(undrugFeeRegular));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(updateSql);
        }

        /// <summary>
        /// 插入非药品收费规则信息
        /// </summary>
        /// <param name="undrugFeeRegular"></param>
        /// <returns></returns>
        public int Insert(UndrugFeeRegular undrugFeeRegular)
        {
            string insertSql = FS.SOC.HISFC.InpatientFee.Data.AbstractFeeRegular.Current.Insert;

            try
            {
                insertSql = string.Format(insertSql, this.myGetParams(undrugFeeRegular));
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(insertSql);
        }

        /// <summary>
        /// 删除非药品收费规则
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public int Delete(string ruleId)
        {
            string deleteSql = FS.SOC.HISFC.InpatientFee.Data.AbstractFeeRegular.Current.Delete;
            try
            {
                deleteSql = string.Format(deleteSql, ruleId);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            return this.ExecNoQuery(deleteSql);
        }

        /// <summary>
        /// 获得按规则收费相关项目编码
        /// </summary>
        /// <param name="itemCode"></param>
        /// <param name="relateItems"></param>
        /// <returns></returns>
        public int GetRelateItems(string itemCode, ref string relateItems)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("DongGuan.InpatientFee.QueryRelateItems", ref sql) < 0)
            {
                this.Err = "查找索引为DongGuan.InpatientFee.QueryRelateItems的SQL语句失败！";
                return -1;
            }
            sql = string.Format(sql, itemCode);

            relateItems = this.ExecSqlReturnOne(sql);
            return 1;
        }

        /// <summary>
        /// 查询所有非药品收费规则
        /// </summary>
        /// <returns></returns>
        public DataSet QueryAllForDataSet()
        {
            string sql = FS.SOC.HISFC.InpatientFee.Data.AbstractFeeRegular.Current.SelectAll;

            DataSet ds = new DataSet();

            if (this.ExecQuery(sql, ref ds) < 0)
            {
                this.Err = "查询非药品收费规则失败！";
                return null;
            }
            return ds;
        }

        /// <summary>
        /// 获取所有的项目
        /// </summary>
        /// <returns></returns>
        public List<FS.HISFC.Models.Fee.Item.UndrugFeeRegular> QueryAll()
        {
            return this.myQueryList(FS.SOC.HISFC.InpatientFee.Data.AbstractFeeRegular.Current.SelectAll);
        }

        /// <summary>
        /// 通过规则编码获取收费规则实体
        /// </summary>
        /// <param name="ruleId"></param>
        /// <returns></returns>
        public UndrugFeeRegular Get(string ruleId)
        {
            string querySql = FS.SOC.HISFC.InpatientFee.Data.AbstractFeeRegular.Current.SelectAll + FS.SOC.HISFC.InpatientFee.Data.AbstractFeeRegular.Current.WhereByKey;

            List<UndrugFeeRegular> list = this.myQueryList(querySql, ruleId);

            if (list == null)
            {
                return null;
            }
            else if (list.Count == 0)
            {
                return new UndrugFeeRegular();
            }
            else
            {
                return list[0];
            }
        }

        /// <summary>
        /// 获取收费规则序号
        /// </summary>
        /// <returns></returns>
        public string GetID()
        {
            return this.ExecSqlReturnOne(FS.SOC.HISFC.InpatientFee.Data.AbstractFeeRegular.Current.AutoID);
        }
    }
}
