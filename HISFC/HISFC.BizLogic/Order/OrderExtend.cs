using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.Order
{
    /// <summary>
    /// [功能描述：住院医嘱信息扩展业务类]
    /// [创 建 者：]
    /// [创建时间：]
    /// </summary>
    public class OrderExtend : FS.FrameWork.Management.Database
    {
        public OrderExtend()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 内部私有方法

        /// <summary>
        /// 根据SQL查询医嘱扩展信息
        /// </summary>
        /// <param name="wheSql">Whe子句</param>
        /// <returns>成功返回医嘱扩展信息实体 失败返回null</returns>
        private ArrayList QueryOrderExtends(string wheSql, params string[] args)
        {
            string strSql = "";
            string selSql = "";
            //取SELECT子句
            selSql = this.GetCommonSqlForSelectAllOrderExtends();

            //取WHERE子句
            try
            {
                if (!string.IsNullOrEmpty(wheSql))
                {
                    if (this.Sql.GetCommonSql(wheSql, ref wheSql) == -1)
                    {
                        this.Err = "没有找到" + wheSql + "字段!";
                        return null;
                    }
                    strSql = selSql + "\r\n" + wheSql;
                    strSql = string.Format(strSql, args);
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }

            ArrayList orderExtendList = new ArrayList();

            //执行Sql语句 
            try
            {
                this.ExecQuery(strSql);

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtend = new FS.HISFC.Models.Order.Inpatient.OrderExtend();
                    orderExtend.InPatientNo = this.Reader[0].ToString();  //住院流水号
                    orderExtend.MoOrder = this.Reader[1].ToString();   //医嘱流水号
                    orderExtend.Indications = this.Reader[2].ToString();//适应症信息
                    orderExtend.Extend1 = this.Reader[3].ToString(); //备注1
                    orderExtend.Extend2 = this.Reader[4].ToString();                                            //备注2 
                    orderExtend.Extend3 = this.Reader[5].ToString();                                       //备注3 
                    orderExtend.Extend4 = this.Reader[6].ToString();                                      //备注4 
                    orderExtend.Extend5 = this.Reader[7].ToString();                                         //备注5 
                    orderExtend.Extend6 = this.Reader[8].ToString();
                    orderExtend.Extend7 = this.Reader[9].ToString();
                    orderExtend.Extend8 = this.Reader[10].ToString();
                    orderExtend.Extend9 = this.Reader[11].ToString();
                    orderExtend.Extend10 = this.Reader[12].ToString();
                    orderExtend.Oper.ID = this.Reader[13].ToString();
                    orderExtend.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[14]);

                    orderExtendList.Add(orderExtend);
                }
                return orderExtendList;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (Reader != null)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 更新单表操作
        /// </summary>
        /// <param name="sqlIndex">SQL语句索引</param>
        /// <param name="args">参数</param>
        /// <returns>成功: >= 1 失败 -1 没有更新到数据 0</returns>
        private int UpdateSingleTable(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;//Update语句

            //获得Where语句
            if (this.Sql.GetCommonSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "没有找到索引为:" + sqlIndex + "的SQL语句";

                return -1;
            }
            sql = string.Format(sql, args);
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 获得医嘱扩展信息字符串数组
        /// </summary>
        /// <param name="prepay">医嘱扩展信息实体</param>
        /// <returns>成功: 医嘱扩展信息字符串数组 失败: null</returns>
        private string[] GetOrderExtendParams(FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtend)
        {
            string[] args ={
                               //住院流水号
                               orderExtend.InPatientNo,
                               //医嘱流水号
                               orderExtend.MoOrder,
							   //适应症
							   orderExtend.Indications,
							   //备注1
							   orderExtend.Extend1,
                                //备注2
							   orderExtend.Extend2,
                                //备注3
							   orderExtend.Extend3,
                                //备注4
							   orderExtend.Extend4,
				               //备注5
							   orderExtend.Extend5,
                                //备注6
							   orderExtend.Extend6,
                               orderExtend.Extend7,
                               orderExtend.Extend8,
                               orderExtend.Extend9,
                               orderExtend.Extend10,
                               orderExtend.Oper.ID,
                               orderExtend.Oper.OperTime.ToString()
						   };

            return args;
        }

        /// <summary>
        /// 获取检索met_ipm_order_extend的全部数据的sql
        /// </summary>
        /// <returns></returns>
        private string GetCommonSqlForSelectAllOrderExtends()
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.Extend.SelectAllOrderExtend", ref strSql) == -1)
            {
                return null;
            }
            return strSql;
        }

        #endregion

        #region 增删改

        /// <summary>
        /// 插入医嘱扩展信息
        /// </summary>
        /// <param name="prepay">医嘱扩展信息实体</param>
        /// <returns>成功: 1 失败 -1 没有插入数据 0</returns>
        public int InsertOrderExtend(FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtend)
        {
            string[] parms = new string[9];
            parms = this.GetOrderExtendParams(orderExtend);
            return this.UpdateSingleTable("Order.Extend.InsertOrderExtend", parms);
        }

        /// <summary>
        /// 更新医嘱扩展信息
        /// </summary>
        /// <param name="prepay">医嘱扩展信息实体</param>
        /// <returns></returns>
        public int UpdateOrderExtend(FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtend)
        {
            return this.UpdateSingleTable("Order.Extend.UpdateOrderExtend", this.GetOrderExtendParams(orderExtend));
        }

        /// <summary>
        /// 删除医嘱扩展信息
        /// </summary>
        /// <param name="prepay">医嘱扩展信息实体</param>
        /// <returns></returns>
        public int DeleteOrderExtend(FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtend)
        {
            return this.UpdateSingleTable("Order.Extend.DeleteOrderExtend", this.GetOrderExtendParams(orderExtend));
        }

        #endregion

        #region 查询函数

        /// <summary>
        /// 根据住院流水号、医嘱流水号取医嘱扩展信息
        /// </summary>
        /// <param name="prepay">医嘱扩展信息实体</param>
        /// <return></return></returns>
        public FS.HISFC.Models.Order.Inpatient.OrderExtend QueryByInpatineNoOrderID(string inpatientNO, string orderID)
        {
            ArrayList al = this.QueryOrderExtends("Order.Extend.QueryByInpatineNoOrderID", inpatientNO, orderID);

            if (al == null || al.Count == 0)
            {
                return null;
            }

            return al[0] as FS.HISFC.Models.Order.Inpatient.OrderExtend;
        }

        #endregion
    }
}
