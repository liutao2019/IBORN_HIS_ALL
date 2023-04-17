using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace FS.SOC.Local.Order.ZhuHai.ZDWY.InPatient.OrderPrint
{
    public class Function : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// 医嘱单打印更新
        /// </summary>
        /// <param name="lineNO">行号</param>
        /// <param name="pageNO">页号</param>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="orderType">0 临嘱；1 长嘱；ALL 全部</param>
        /// <param name="prnFlag">打印标记</param>
        /// <param name="UCULFlag">0 非UCUL；1 UCUL；其他全部</param>
        /// <returns></returns>
        public int ResetOrderPrint(string lineNO, string pageNO, string inpatientNO, string orderType, string prnFlag, int UCULFlag)
        {
            string sql = @"UPDATE MET_IPM_ORDER o
                            SET o.rowno={0},
                                o.pageno={1},
                                o.get_flag='{4}'
                            WHERE o.INPATIENT_NO='{2}'
                            AND (o.DECMPS_STATE='{3}' OR '{3}'='ALL')";
            if (UCULFlag == 0)
            {
                sql += "\r\n" + " and o.class_code not in ('UC','UL')";
            }
            else if (UCULFlag == 1)
            {
                sql += "\r\n" + " and o.class_code in ('UC','UL')";
            }
            else
            {
            }

            sql = string.Format(sql, lineNO, pageNO, inpatientNO, orderType, prnFlag);
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        ///  医嘱单打印更新(根据主键更新)
        /// </summary>
        /// <param name="lineNO">行号</param>
        /// <param name="pageNO">页号</param>
        /// <param name="inpatientNO">住院流水号</param>
        /// <param name="orderType">0 临嘱；1 长嘱；ALL 全部</param>
        /// <param name="prnFlag">打印标记</param>
        /// <param name="number">根据行数</param>
        /// <param name="UCULFlag">0 非UCUL；1 UCUL；其他全部</param>
        /// <returns></returns>
        public int ResetOrderPrintPart(string lineNO, string pageNO, string inpatientNO, string orderType, string prnFlag, string number, int UCULFlag)
        {
            string sql = @"UPDATE MET_IPM_ORDER o
                            SET o.rowno={0},
                                o.pageno={1},
                                o.get_flag='{4}'
                            WHERE o.INPATIENT_NO='{2}'
                            AND (o.DECMPS_STATE='{3}' OR '{3}'='ALL')
                            AND o.mo_order in ({5})";

            if (UCULFlag == 0)
            {
                sql += "\r\n" + " and o.class_code not in ('UC','UL')";
            }
            else if (UCULFlag == 1)
            {
                sql += "\r\n" + " and o.class_code in ('UC','UL')";
            }
            else
            {
            }

            //if (this.Sql.GetCommonSql("Order.OrderPrn.UpdateOrderPrint.Part", ref sql) == -1)
            //{
            //    this.Err = "没有找到Order.OrderPrn.UpdateOrderPrint.Part字段!";
            //    return -1;
            //}

            sql = string.Format(sql, lineNO, pageNO, inpatientNO, orderType, prnFlag, number);
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 获取已打印的最大页码和行号
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="isLong"></param>
        /// <param name="maxPageNo"></param>
        /// <param name="maxRowNo"></param>
        /// <param name="UCULFlag">0 非UCUL；1 UCUL；其他全部</param>
        /// <returns></returns>
        public int GetPrintInfo(string inpatientNo, bool isLong, ref int maxPageNo, ref int maxRowNo, int UCULFlag)
        {
            string strSql = @"select t.pageno,t.rowno
                            from met_ipm_order t
                            where t.inpatient_no='{0}'
                            and t.DECMPS_STATE = '{1}'
                            ";

            if (UCULFlag == 0)
            {
                strSql += "\r\n" + " and t.class_code not in ('UC','UL')";
            }
            else if (UCULFlag == 1)
            {
                strSql += "\r\n" + " and t.class_code in ('UC','UL')";
            }
            else
            {
            }

            strSql += "\r\n" + "order by t.pageno desc,t.rowno desc";

            //if (this.Sql.GetCommonSql("Order.Order.GetMaxPageNo", ref strSql) == -1)
            //{
            //    this.Err = "Can't Find Sql:Order.Order.GetMaxPageNo";
            //    return -1;
            //}

            string orderType = "1";
            if (!isLong)
            {
                orderType = "0";
            }

            try
            {
                strSql = System.String.Format(strSql, inpatientNo, orderType);

                this.ExecQuery(strSql);
                while (Reader.Read())
                {
                    maxPageNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[0]);
                    maxRowNo = FS.FrameWork.Function.NConvert.ToInt32(Reader[1]);
                    break;
                }
            }
            catch (Exception ex)
            {
                Err = Err + "\r\n" + ex.Message;
                maxPageNo = 0;
                maxRowNo = 0;
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// 更新页码和打印标记
        /// </summary>
        /// <param name="inpatientNo"></param>
        /// <param name="moOrder"></param>
        /// <param name="pageNo"></param>
        /// <param name="rowNo"></param>
        /// <param name="newFlag"></param>
        /// <param name="oldFlag"></param>
        /// <returns></returns>
        public int UpdatePrintInfo(string inpatientNo, string moOrder, string pageNo, string rowNo, string newFlag, string oldFlag)
        {
            string strSql = "";
            /*
                update met_ipm_order o
                set o.pageno   = '{2}',
                    o.rowno    = '{3',
                    o.get_flag = '{4}'
                where o.inpatient_no = '{0}'
                  and o.mo_order = '{1}'
                  and o.get_flag = '{5}'
             */
            if (this.Sql.GetCommonSql("Order.Order.UpdatePageRowNoAndGetflag", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Order.Order.UpdatePageRowNoAndGetflag";
                return -1;
            }

            strSql = System.String.Format(strSql, inpatientNo, moOrder, pageNo, rowNo, newFlag, oldFlag);

            return this.ExecNoQuery(strSql); ;
        }

        /// <summary>
        /// 获取字节长度
        /// </summary>
        /// <param name="str"></param>
        /// <param name="padLength"></param>
        /// <returns></returns>
        private int GetStrLength(string str)
        {
            int len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (System.Text.RegularExpressions.Regex.IsMatch(str[i].ToString(), "[^\x00-\xff]"))
                {
                    len += 2;
                }
                else
                {
                    len += 1;
                }
            }

            return len;
        }
    }
}
