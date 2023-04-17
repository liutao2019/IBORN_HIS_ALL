using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FS.HISFC.Models.Fee;
using System.Collections;
using FS.FrameWork.Function;

namespace FS.HISFC.BizLogic.Fee
{
    public class DiscountCardLogic : FS.FrameWork.Management.Database
    {
        #region 私有方法
        /// <summary>
        /// 检索卡卷信息
        /// </summary>
        /// <param name="sql">执行的查询SQL语句</param>
        /// <param name="args">SQL语句的参数</param>
        /// <returns>成功:卡卷信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        private ArrayList QueryDiscountCardBySql(string sql, params string[] args)
        {
            ArrayList DiscountCards = new ArrayList();

            //执行SQL语句
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            try
            {
                //循环读取数据
                while (this.Reader.Read())
                {
                    DiscountCard discountCard = new DiscountCard();

                    discountCard.GetDate = NConvert.ToDateTime(this.Reader[0].ToString());
                    discountCard.GetPersonCode = this.Reader[1].ToString();
                    discountCard.CardKind = this.Reader[2].ToString();
                    discountCard.CardName = this.Reader[3].ToString();
                    discountCard.StartNo = this.Reader[4].ToString();
                    discountCard.EndNo = this.Reader[5].ToString();
                    discountCard.UsedNo = this.Reader[6].ToString();
                    discountCard.UsedState = this.Reader[7].ToString();
                    discountCard.IsPub = this.Reader[8].ToString();
                    discountCard.OperCode = this.Reader[9].ToString();
                    discountCard.OperDate = NConvert.ToDateTime(this.Reader[10].ToString());
                    discountCard.QTY = this.Reader[11].ToString();

                    DiscountCards.Add(discountCard);
                }//循环结束

                this.Reader.Close();
            }
            catch (Exception e)
            {
                this.Err = e.Message;
                this.WriteErr();

                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }

                return null;
            }

            return DiscountCards;
        }

        /// <summary>
        /// 获得update或者insert退的传入参数数组
        /// </summary>
        /// <param name="invoice">卡卷实体类</param>
        /// <returns>参数数组</returns>
        private string[] GetDiscountCardParams(DiscountCard discountCard)
        {
            string[] args =
				{
					discountCard.GetDate.ToString(),
					discountCard.GetPersonCode,
					discountCard.CardKind,
					discountCard.CardName,
					discountCard.StartNo,
					discountCard.EndNo,
					discountCard.UsedNo,
					discountCard.UsedState,
					discountCard.IsPub,
					discountCard.OperCode,
					discountCard.OperDate.ToString(),
                    discountCard.QTY
				};

            return args;
        }

        #endregion 

        #region 公有方法

        #region 插入
        /// <summary>
        /// 插入一条卡卷信息.
        /// </summary>
        /// <param name="invoice">卡卷信息类</param>
        /// <returns> 成功: 1 失败: -1 没有插入记录: 0</returns>
        public int InsertDiscountCard(DiscountCard discountCard)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.DiscountCardLogic.InsertDiscountCard", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.DiscountCardLogic.InsertDiscountCard的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, this.GetDiscountCardParams(discountCard));
        }
		
        #endregion

        #region 删除
        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="invoice">卡卷信息类</param>
        /// <returns>成功: 删除的条目 失败: -1 没有删除记录: 0</returns>
        public int Delete(DiscountCard discountCard)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.DiscountCardLogic.DeleteDiscountCard", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.DiscountCardLogic.DeleteDiscountCard的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, discountCard.GetDate.ToString(), discountCard.GetPersonCode);
        }

        #endregion 

        #region 更新
        /// <summary>
        /// 更新卡卷信息
        /// </summary>
        /// <param name="invoice">卡卷信息类</param>
        /// <returns> 成功: 1 失败: -1 没有更新记录: 0</returns>
        public int UpdateDiscountCard(DiscountCard discountCard)
        {
            string sql = string.Empty;//更新SQL语句

            if (this.Sql.GetCommonSql("Fee.DiscountCardLogic.UpdateDiscountCard", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.DiscountCardLogic.UpdateDiscountCard的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, this.GetDiscountCardParams(discountCard));
        }

        /// <summary>
        /// 更新卡卷已用号
        /// </summary>
        /// <param name="discountCard"></param>
        public int UpdateDiscountCardUsedNo(string PersonCode, string cardKind, string UsedNo, string UsedState, string OperCode, string OperDate)
        {
            string sql = string.Empty;//更新SQL语句

            if (this.Sql.GetCommonSql("Fee.DiscountCardLogic.UpdateDiscountCard.ByPerson", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.DiscountCardLogic.UpdateDiscountCard.ByPerson的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, PersonCode, cardKind, UsedNo, OperCode, OperDate, UsedState);
        }


        /// <summary>
        /// 更新一条信息.回收专用
        /// </summary>
        /// <param name="invoice">卡卷信息类</param>
        /// <returns> 成功: 1 失败: -1 没有更新记录: 0</returns>
        public int UpdateDiscountCardUsedStateByPerson(DiscountCard discountCard)
        {
            string sql = string.Empty;//插入SQL语句

            if (this.Sql.GetCommonSql("Fee.DiscountCardLogic.UpdateDiscountCardUsedState.ByPerson", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.DiscountCardLogic.UpdateDiscountCardUsedState.ByPerson的SQL语句";

                return -1;
            }

            return this.ExecNoQuery(sql, this.GetDiscountCardParams(discountCard));
        }
        #endregion

        #region 查询

        /// <summary>
        /// 获取检索全部数据的sql
        /// </summary>
        /// <returns></returns>
        private string GetSelectAllDiscountCards()
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Fee.DiscountCardLogic.SelectALLDiscountCards", ref strSql) == -1)
            {
                return null;
            }
            return strSql;
        }


        /// <summary>
        /// 通过人员编号,和类别查询该人员的发票信息
        /// </summary>
        /// <param name="personID">人员编号</param>
        /// <param name="invoiceType">类别</param>
        /// <returns>成功:信息数组 失败:null 没有查找到数据返回元素数为0的ArrayList</returns>
        public ArrayList QueryDiscountCard(string personCode, string CardKind)
        {
            string sql = string.Empty; //查询SQL语句

            if (this.Sql.GetCommonSql("Fee.DiscountCardLogic.SelectDiscountCard.ByPersonAndCardKind", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.DiscountCardLogic.SelectDiscountCard.ByPersonAndCardKind的SQL语句";

                return null;
            }

            return this.QueryDiscountCardBySql(sql, personCode, CardKind);
        }


        // <summary>
        /// 检测所给的起始号和数量是否有效：
        /// </summary>
        /// <param name="startNO">起始号</param>
        /// <param name="endNO">数量</param>
        /// <param name="invoiceType">类型</param>
        /// <returns>有效true, 无效 false</returns>
        public bool DiscountCardIsValid(string startNO, string endNO, string cardKind)
        {

            if (Convert.ToInt32(endNO) - Convert.ToInt32(startNO) < 0)
            {
                this.Err = "输入的终止号大于起始号!";

                return false;
            }

            string sql = string.Empty;

            ArrayList dsiscountCards = new ArrayList();

            if (this.Sql.GetCommonSql("Fee.DiscountCardLogic.SelectDiscountCard.ByCardNo", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.DiscountCardLogic.SelectDiscountCard.ByCardNo的SQL语句";

                return false;
            }

            dsiscountCards = QueryDiscountCardBySql(sql, cardKind, startNO, endNO);

            //如果没有符合条件的卡,说明可以生成
            if (dsiscountCards == null || dsiscountCards.Count == 0)
            {
                return true;
            }


            return false;
        }

        /// <summary>
        /// 根据人员和卡类型获取可用卡号
        /// </summary>
        /// <returns></returns>
        public ArrayList QueryUsedCardByPersonAndCardKind(string personCode, string CardKind)
        {
            string sql = string.Empty; //查询SQL语句

            if (this.Sql.GetCommonSql("Fee.DiscountCardLogic.SelectUsedCard.ByPersonAndCardKind", ref sql) == -1)
            {
                this.Err = "没有找到索引为:Fee.DiscountCardLogic.SelectUsedCard.ByPersonAndCardKind的SQL语句";

                return null;
            }

            return this.QueryDiscountCardBySql(sql, personCode, CardKind);
        }

        #endregion 

        #endregion

    }
}
