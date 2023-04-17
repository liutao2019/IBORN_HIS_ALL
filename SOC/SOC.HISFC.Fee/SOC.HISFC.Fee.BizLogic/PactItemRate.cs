using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.HISFC.Fee.BizLogic
{
    /// <summary>
    /// [功能描述:合同单位项目比例SOC业务层，从核心版本独立出来，只对SOC层有效]<br></br>
    /// [创 建 者: zhaoj]<br></br>
    /// [创建时间: 2011-10]<br></br>
    /// </summary>
    public class PactItemRate : FS.FrameWork.Management.Database
    {

        private object[] getParams(FS.HISFC.Models.Base.PactItemRate info)
        {
            return new Object[] { 
                   info.ID, 
                    info.ItemType, 
                    info.PactItem.ID, 
                    info.Rate.PubRate, 
                    info.Rate.OwnRate, 
                    info.Rate.PayRate, 
                    info.Rate.RebateRate, 
                    info.Rate.ArrearageRate, 
                    info.Rate.Quota,
                    this.Operator.ID
            }
                ;
        }

        /// <summary>
        /// 获取项目的对照信息
        /// </summary>
        /// <returns></returns>
        public List<FS.HISFC.Models.Base.PactItemRate> QueryGroupByItem()
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractPactItemRate.Current.SelectItemGroup;

            try
            {
                if (this.ExecQuery(strSql) < 0)
                {
                    this.WriteDebug(this.Err);
                    return null;
                }
                List<FS.HISFC.Models.Base.PactItemRate> list = new List<FS.HISFC.Models.Base.PactItemRate>();
                FS.HISFC.Models.Base.PactItemRate info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Base.PactItemRate();
                    info.ItemType = Reader[0].ToString();
                    info.PactItem.ID = Reader[1].ToString();
                    info.PactItem.Name = Reader[2].ToString();
                    info.SpellCode = this.Reader[3].ToString();
                    info.WBCode = this.Reader[4].ToString();
                    info.UserCode = this.Reader[5].ToString();

                    list.Add(info);

                }
                return list;
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                this.WriteErr();
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return null;
        }

        /// <summary>
        /// 获取项目的对照信息
        /// </summary>
        /// <returns></returns>
        public List<FS.HISFC.Models.Base.PactItemRate> QueryGroupByItem(string pactCode)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractPactItemRate.Current.SelectItemGroupByPact;

            try
            {
                if (this.ExecQuery(string.Format(strSql,pactCode)) < 0)
                {
                    this.WriteDebug(this.Err);
                    return null;
                }
                List<FS.HISFC.Models.Base.PactItemRate> list = new List<FS.HISFC.Models.Base.PactItemRate>();
                FS.HISFC.Models.Base.PactItemRate info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Base.PactItemRate();
                    info.ItemType = Reader[0].ToString();
                    info.PactItem.ID = Reader[1].ToString();
                    info.PactItem.Name = Reader[2].ToString();
                    info.SpellCode = this.Reader[3].ToString();
                    info.WBCode = this.Reader[4].ToString();
                    info.UserCode = this.Reader[5].ToString();

                    list.Add(info);

                }
                return list;
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                this.WriteErr();
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return null;
        }

        /// <summary>
        /// 获取项目的对照信息（根据项目类型和项目编码）
        /// </summary>
        /// <param name="itemType">0 最小费用 1 药品 2 非药品</param>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public List<FS.HISFC.Models.Base.PactItemRate> QueryByItem(string itemType, string itemCode,string pactCodes)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractPactItemRate.Current.SelectByItemTypeAndCode;
            try
            {
                if (this.ExecQuery(string.Format(strSql,itemType,itemCode,pactCodes) )< 0)
                {
                    this.WriteDebug(this.Err);
                    return null;
                }
                List<FS.HISFC.Models.Base.PactItemRate> list = new List<FS.HISFC.Models.Base.PactItemRate>();
                FS.HISFC.Models.Base.PactItemRate info = null;
                while (this.Reader.Read())
                {
                    info = new FS.HISFC.Models.Base.PactItemRate();
                    info.ID = this.Reader[0].ToString();
                    info.Name = Reader[1].ToString();
                    info.ItemType = Reader[2].ToString();
                    info.Rate.PubRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[3]);
                    info.Rate.OwnRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[4]);
                    info.Rate.PayRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                    info.Rate.RebateRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[6]);
                    info.Rate.ArrearageRate = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[7]);
                    info.PactItem.ID = this.Reader[8].ToString();
                    info.Rate.Quota = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[9]);
                    info.SpellCode = this.Reader[10].ToString();
                    info.WBCode = this.Reader[11].ToString();
                    list.Add(info);

                }

                return list;
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                this.WriteErr();
            }
            finally
            {
                if (this.Reader != null && !this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
            return null;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public int Delete(FS.HISFC.Models.Base.PactItemRate info)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractPactItemRate.Current.Delete;

            try
            {
                strSql = string.Format(strSql, info.ID, info.PactItem.ID);
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
        /// 插入
        /// </summary>
        /// <returns></returns>
        public int Insert(FS.HISFC.Models.Base.PactItemRate info)
        {
            string strSql = FS.SOC.HISFC.Fee.Data.AbstractPactItemRate.Current.Insert;
            try
            {
                strSql = string.Format(strSql, this.getParams(info));
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <returns></returns>
        public int Update(FS.HISFC.Models.Base.PactItemRate info)
        {
            string strSql =FS.SOC.HISFC.Fee.Data.AbstractPactItemRate.Current.Update;
            try
            {
                strSql = string.Format(strSql, this.getParams(info));
            }
            catch (Exception ee)
            {
                this.Err = ee.Message;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }
    }

}
