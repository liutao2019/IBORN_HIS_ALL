using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FS.SOC.Local.DrugStore.GuangZhou.GYZL.Compound
{
    /// <summary>
    /// [功能描述: 药品管理中常数维护]<br></br>
    /// [创 建 者: Cuip]<br></br>
    /// [创建时间: 2005-02]<br></br>
    /// <修改记录>
    ///     1、屏蔽取药科室内部无用的函数
    ///     2、药品全院特限的权限医师授权启用有效性字段by Sunjh 2010-11-23 {B5995BC9-E571-44ba-84C9-D65382C64F16}
    /// </修改记录>
    /// </summary>
    public class Constant : FS.FrameWork.Management.Database
    {
        public Constant()
        {

        }


        #region 配液中心附材不收费时间设置

        /// <summary>
        /// 根据实体信息获取Updete或Insert语句参数数组
        /// </summary>
        /// <param name="orderGroup">医嘱批次设置信息</param>
        /// <returns>成功返回参数数组 失败返回null</returns>
        private string[] GetOrderGroupParam(FS.HISFC.Models.Pharmacy.OrderGroup orderGroup)
        {
            orderGroup.BeginTime = new DateTime(2000, 12, 12, orderGroup.BeginTime.Hour, orderGroup.BeginTime.Minute, orderGroup.BeginTime.Second);
            orderGroup.EndTime = new DateTime(2000, 12, 12, orderGroup.EndTime.Hour, orderGroup.EndTime.Minute, orderGroup.EndTime.Second);

            string[] strParam = new string[] {                                                
                                                orderGroup.ID,
                                                orderGroup.BeginTime.ToString(),
                                                orderGroup.EndTime.ToString(),
                                                orderGroup.Oper.ID,
                                                orderGroup.Oper.OperTime.ToString()
                                             };

            return strParam;
        }

        /// <summary>
        /// 执行Sql 获取OrderGroup信息
        /// </summary>
        /// <param name="strSql">需执行的Sql</param>
        /// <returns>成功返回1 失败返回-1</returns>
        private List<FS.HISFC.Models.Pharmacy.OrderGroup> ExecSqlForOrderGroup(string strSql)
        {
            List<FS.HISFC.Models.Pharmacy.OrderGroup> al = new List<FS.HISFC.Models.Pharmacy.OrderGroup>();
            FS.HISFC.Models.Pharmacy.OrderGroup orderGroup = new FS.HISFC.Models.Pharmacy.OrderGroup();

            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "执行" + strSql + "发生错误" + this.Err;
                return null;
            }

            try
            {
                while (this.Reader.Read())
                {
                    orderGroup = new FS.HISFC.Models.Pharmacy.OrderGroup();

                    orderGroup.ID = this.Reader[0].ToString();
                    orderGroup.BeginTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString());
                    orderGroup.EndTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[2].ToString());
                    orderGroup.Oper.ID = this.Reader[3].ToString();
                    orderGroup.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);

                    al.Add(orderGroup);
                }
            }
            catch
            {
                this.Err = "由Reader内读取数据发生异常";
                return null;
            }
            finally
            {
                this.Reader.Close();
            }

            return al;
        }

        /// <summary>
        /// 插入医嘱批次设置信息
        /// </summary>
        /// <param name="orderGroup">医嘱批次设置信息</param>
        /// <returns>成功返回1 失败返回-1</returns>
        public int InsertCompoundFeeDate(FS.HISFC.Models.Pharmacy.OrderGroup orderGroup)
        {
            string strSQL = "";
            if (this.Sql.GetSql("Pharmacy.Constant.InsertCompoundFeeDate", ref strSQL) == -1) return -1;
            try
            {
                string[] strParm = this.GetOrderGroupParam(orderGroup);   //取参数列表
                strSQL = string.Format(strSQL, strParm);                //替换SQL语句中的参数。
            }
            catch (Exception ex)
            {
                this.Err = "付数值时候出错！" + ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 删除所有医嘱批次设置信息
        /// </summary>
        /// <returns></returns>
        public int DelCompoundFeeDate()
        {
            string strSQL = "";
            //取SQL语句
            if (this.Sql.GetSql("Pharmacy.Constant.DelCompoundFeeDate", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Constant.DelCompoundFeeDate字段!";
                return -1;
            }

            return this.ExecNoQuery(strSQL);
        }

        /// <summary>
        /// 获取所有医嘱批次设置信息
        /// </summary>
        /// <returns></returns>
        public List<FS.HISFC.Models.Pharmacy.OrderGroup> QueryCompoundFeeDate()
        {
            string strSQL = "";

            //取SQL语句
            if (this.Sql.GetSql("Pharmacy.Constant.QueryCompoundFeeDate", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Constant.QueryCompoundFeeDate字段!";
                return null;
            }

            return this.ExecSqlForOrderGroup(strSQL);
        }


        /// <summary>
        /// 删除所有医嘱批次设置信息
        /// </summary>
        /// <returns></returns>
        public int DelCompoundFeeDate(string groupCode, DateTime dtBegin, DateTime dtEnd)
        {
            string strSQL = "";
            //取SQL语句
            if (this.Sql.GetSql("Pharmacy.Constant.DelCompoundFeeDate.ID", ref strSQL) == -1)
            {
                this.Err = "没有找到Pharmacy.Constant.DelCompoundFeeDate.ID字段!";
                return -1;
            }

            dtBegin = new DateTime(2000, 12, 12, dtBegin.Hour, dtBegin.Minute, dtBegin.Second);
            dtEnd = new DateTime(2000, 12, 12, dtEnd.Hour, dtEnd.Minute, dtEnd.Second);

            strSQL = string.Format(strSQL, groupCode, dtBegin.ToString(), dtEnd.ToString());

            return this.ExecNoQuery(strSQL);
        }


        #endregion


    }
}
