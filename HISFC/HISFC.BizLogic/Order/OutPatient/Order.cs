using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
namespace FS.HISFC.BizLogic.Order.OutPatient
{
    /// <summary>
    /// Order 的摘要说明。
    /// 门诊医嘱
    /// </summary>
    public class Order : FS.FrameWork.Management.Database
    {
        public Order()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }


        #region 基本操作，增删改

        /// <summary>
        /// 插入一条
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int InsertOrder(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            string sql = "Order.OutPatient.Order.Insert";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            sql = this.myGetCommonSql(sql, order);
            if (sql == null) return -1;
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrder(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            if (this.DeleteOrder(order.SeeNO, FS.FrameWork.Function.NConvert.ToInt32(order.ID)) < 0)
            {
                return -1;//删除不成功
            }
            return this.InsertOrder(order);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="seeNo"></param>
        /// <param name="seqNo"></param>
        /// <returns></returns>
        public int DeleteOrder(string seeNo, int seqNo)
        {
            /*
             * DELETE 
             * FROM met_ord_recipedetail   --诊间处方明细表
                WHERE     see_no='{0}' and sequence_no = {1} 
                AND status = '0'
             * */

            string sql = "Order.OutPatient.Order.Delete";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                sql = string.Format(sql, seeNo, seqNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(sql);
        }

        #endregion

        #region 门诊医嘱变更表操作add by sunm

        public int InsertOrderChangeInfo(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            string sql = "Order.OutPatient.Order.InsertChangeInfo";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            sql = this.myGetCommonSql(sql, order);
            if (sql == null) return -1;
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }
        /// <summary>
        /// 更新医嘱变更纪录
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrderChangedInfo(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            string sql = "Order.OutPatient.Order.UpdateChangeInfo";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            sql = System.String.Format(sql, order.DCOper.ID, order.SeeNO, order.SequenceNO);
            if (sql == null) return -1;
            if (this.ExecNoQuery(sql) <= 0) return -1;
            return 0;
        }

        /// <summary>
        /// 作废医嘱
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrderBeCaceled(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            string sql = "Order.OutPatient.Order.CancelOrder";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = "Can't Find Sql:Order.OutPatient.Order.CancelOrder";
                return -1;
            }
            sql = System.String.Format(sql, order.ID);
            if (sql == null) return -1;
            return this.ExecNoQuery(sql);
        }

        #endregion

        #region 获得新的看诊序号
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public int GetNewSeeNo(string cardNo)
        {
            string sql = "Order.OutPatient.Order.GetNewSeeNo.1";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                sql = string.Format(sql, cardNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return FS.FrameWork.Function.NConvert.ToInt32(this.ExecSqlReturnOne(sql));
        }
        #endregion

        /// <summary>
        /// 获得新医嘱组合序号
        /// </summary>
        /// <returns></returns>
        public string GetNewOrderComboID()
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Management.Order.GetComboID", ref sql) == -1) return null;
            string strReturn = this.ExecSqlReturnOne(sql);
            if (strReturn == "-1" || strReturn == "") return null;
            return strReturn;
        }

        #region 更新医嘱已经收费
        /// <summary>
        /// 更新医嘱已经收费
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        public int UpdateOrderCharged(string orderID)
        {
            string sql = "Order.OutPatient.Order.Update.UpdateOrderCharged.2";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            return this.ExecNoQuery(sql, orderID);
        }
        /// <summary>
        /// 更新医嘱已经收费
        /// </summary>
        /// <param name="reciptNo"></param>
        /// <param name="seqNo"></param>
        /// <returns></returns>
        public int UpdateOrderCharged(string reciptNo, string seqNo)
        {
            string sql = "Order.OutPatient.Order.Update.UpdateOrderCharged.1";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            return this.ExecNoQuery(sql, reciptNo, seqNo, this.Operator.ID);
        }
        /// <summary>
        /// 更新医嘱已经收费
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="chargeOperID"></param>
        /// <returns></returns>
        public int UpdateOrderChargedByOrderID(string orderID, string chargeOperID)
        {
            string sql = "Order.OutPatient.Order.Update.UpdateOrderCharged.4";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            return this.ExecNoQuery(sql, orderID, chargeOperID);
        }
        //{5C7887F1-A4D5-4a66-A814-18D45367443E}
        /// <summary>
        /// 更新医嘱是否可以退费
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="chargeOperID"></param>
        /// <returns></returns>
        public int UpdateOrderCanChargeByOrderID(string orderID, string chargeOperID)
        {
            string sql = "Order.OutPatient.Order.Update.UpdateOrderCanCharge.1";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            return this.ExecNoQuery(sql, orderID, chargeOperID);
        }
        /// <summary>
        /// 更新医嘱是否可以退费
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="chargeOperID"></param>
        /// <param name="refundReason">退费原因</param>
        /// <returns></returns>
        public int UpdateOrderCanChargeByOrderID(string orderID, string chargeOperID, string refundReason)
        {
            string sql = "update met_ord_recipedetail  set quit_flag ='1' ,  quit_oper='{1}' ,refund_reason = '{2}'  ,  quit_date = sysdate  where sequence_no  = '{0}' and status='1'";
            //if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            return this.ExecNoQuery(sql, orderID, chargeOperID,refundReason);
        }
        #endregion

        #region  更新医嘱序号
        /// <summary>
        /// 更新医嘱序号
        /// 增加clinic_code优化查询速率{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="sortID"></param>
        /// <returns></returns>
        public int UpdateOrderSortID(string orderID, int sortID, string clinicCode)
        {
            string sql = "Order.OutPatient.Order.Update.UpdateOrderSortID.1";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                sql = string.Format(sql, orderID, sortID, clinicCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(sql);
        }

        #endregion

        #region  更新医嘱皮试结果
        /// <summary>
        /// 更新医嘱皮试结果//{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
        /// </summary>
        /// <param name="sequenceNO"></param>
        /// <returns></returns>
        public int UpdateOrderHyTest(string hytestValue, string sequenceNO)
        {
            string sql = "Order.OutPatient.Order.UpdateHyTest.1";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                sql = string.Format(sql, hytestValue, sequenceNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(sql);
        }
        /// <summary>
        /// 更新医嘱皮试结果{55BBD9DB-F5C9-4e0a-94E5-9F7FCB121350}
        /// </summary>
        /// <param name="sequenceNO"></param>
        /// <returns></returns>
        public int UpdateOrderHyTest(string hytestValue, string hytestName, string sequenceNO, string seeNO)
        {
            string sql = "Order.OutPatient.Order.UpdateHyTest.2";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                sql = string.Format(sql, hytestValue, hytestName, sequenceNO, seeNO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// 查询皮试处方信息
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="beginDtime"></param>
        /// <param name="endDtime"></param>
        /// <returns></returns>
        public List<FS.FrameWork.Models.NeuObject> QueryHytoRecord(string cardNO, string beginDtime, string endDtime)
        {
            string strSql = string.Empty;

            int returnValue = this.Sql.GetCommonSql("Order.OutPatient.Order.QueryHyRecord", ref strSql);

            if (returnValue < 0)
            {
                this.Err = "查询对应[Order.OutPatient.Order.QueryHyRecord]的sql语句失败";
                return null;
            }

            try
            {
                strSql = string.Format(strSql, cardNO, beginDtime, endDtime);
            }
            catch (Exception ex)
            {

                this.Err = "格式化出错！\n" + ex.Message;
                return null;
            }

            if (this.ExecQuery(strSql) < 0)
            {
                return null;
            }
            List<FS.FrameWork.Models.NeuObject> orderList = new List<FS.FrameWork.Models.NeuObject>();
            while (this.Reader.Read())
            {

                FS.FrameWork.Models.NeuObject order = new FS.FrameWork.Models.NeuObject();
                order.ID = this.Reader[0].ToString();
                order.Name = this.Reader[1].ToString();
                order.Memo = this.Reader[2].ToString();
                orderList.Add(order);
            }

            this.Reader.Close();

            return orderList;


        }

        // 根据病历号，门诊流水号，查询需要做皮试的有效医嘱
        /// <summary>
        /// 根据病历号，门诊流水号，查询需要做皮试的有效医嘱{55BBD9DB-F5C9-4e0a-94E5-9F7FCB121350}
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryOrderByCardNOClinicNO(string cardNO, string clinicNO)
        {
            string sql = "", sqlSelect = "", sqlWhere = "Order.OutPatient.Order.Query.Where.5";
            if (this.myGetSelectSql(ref sqlSelect) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            if (this.Sql.GetCommonSql(sqlWhere, ref sqlWhere) == -1) return null;
            sql = sqlSelect + " " + sqlWhere;
            sql = string.Format(sql, cardNO, clinicNO);
            return this.myGetExecOrder(sql);
        }

        /// <summary>
        /// 根据病历号，门诊流水号查询
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="clinicNO"></param>
        /// <returns></returns>
        public ArrayList QueryOrderByCardNOandClinicNO(string cardNO, string clinicNO)
        {
            string sql = "", sqlSelect = "", sqlWhere = "Order.OutPatient.Order.Query.Where.9";
            if (this.myGetSelectSql(ref sqlSelect) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            if (this.Sql.GetCommonSql(sqlWhere, ref sqlWhere) == -1) return null;
            sql = sqlSelect + " " + sqlWhere;
            sql = string.Format(sql, cardNO, clinicNO);
            return this.myGetExecOrder(sql);
        }

        /// <summary>
        /// 根据主键查询医嘱
        /// </summary>
        /// <param name="seeNO"></param>
        /// <param name="sqeNO"></param>
        /// <returns></returns>{55BBD9DB-F5C9-4e0a-94E5-9F7FCB121350}
        public ArrayList QueryOrderByKey(string seeNO, string sqeNO)
        {
            string sql = "", sqlSelect = "", sqlWhere = "Order.OutPatient.Order.Query.Where.6";
            if (this.myGetSelectSql(ref sqlSelect) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            if (this.Sql.GetCommonSql(sqlWhere, ref sqlWhere) == -1) return null;
            sql = sqlSelect + " " + sqlWhere;
            sql = string.Format(sql, seeNO, sqeNO);
            return this.myGetExecOrder(sql);
        }
        #endregion

        #region 查询

        /// <summary>
        /// 查询执行医嘱--通过看诊序号查询
        /// </summary>
        /// <param name="seeNo"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(string seeNo)
        {
            string sql = "", sqlSelect = "", sqlWhere = "Order.OutPatient.Order.Query.Where.1";
            if (this.myGetSelectSql(ref sqlSelect) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            if (this.Sql.GetCommonSql(sqlWhere, ref sqlWhere) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            sql = sqlSelect + " " + sqlWhere;
            sql = string.Format(sql, seeNo);
            return this.myGetExecOrder(sql);
        }

        /// <summary>
        /// 查询门诊处方
        /// </summary>
        /// <param name="clinicCode">门诊看诊流水号</param>
        /// <param name="seeNo">看诊序号</param>
        /// <returns></returns>
        public ArrayList QueryOrder(string clinicCode, string seeNo)
        {
            return this.QueryOrderBase("Order.OutPatient.Order.Query.ByClinicCodeSeeNo", clinicCode, seeNo);
        }

        /// <summary>
        /// 查询门诊处方
        /// </summary>
        /// <param name="clinicCode">门诊看诊流水号</param>
        /// <returns></returns>
        public ArrayList QueryOrderByClinicCode(string clinicCode)
        {
            return this.QueryOrderBase("Order.OutPatient.Order.Query.ByClinicCode", clinicCode);
        }

        /// <summary>
        /// 查询历史门诊处方执行次数
        /// //{63E3D61E-ECEE-4fcf-BF83-6597EAD9D81A}
        /// </summary>
        /// <param name="clinicCode">门诊看诊流水号</param>
        /// <param name="seeNo">看诊序号</param>
        /// <returns></returns>
        public ArrayList QueryOrderHistoryExec(string clinicCode, string seeNo)
        {
            return this.QueryOrderBaseExec("Order.OutPatient.Order.Query.ByClinicCodeSeeNo", clinicCode, seeNo);
        }

        /// <summary>
        /// 根据门诊号查询门诊处方
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(string whereSql, string clinicCode, string seeNO)
        {
            string sqlStr = "";
            if (this.myGetSelectSql(ref sqlStr) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            sqlStr = sqlStr + "\r\n" + whereSql;
            sqlStr = string.Format(sqlStr, clinicCode);
            return this.myGetExecOrder(sqlStr);
        }

        /// <summary>
        /// 根据自定义SQL查处方
        /// </summary>
        /// <param name="whereSql"></param>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public ArrayList QueryOrderForST(string whereSql)
        {
            string sqlStr = "";
            if (this.myGetSelectSql(ref sqlStr) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            sqlStr = sqlStr + "\r\n" + whereSql;
            return this.myGetExecOrder(sqlStr);
        }


        /// <summary>
        /// 根据whereIndex查询门诊处方
        /// </summary>
        /// <param name="SqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QueryOrderBase(string SqlIndex, params string[] args)
        {
            string sqlStr = "";
            if (this.myGetSelectSql(ref sqlStr) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            if (this.Sql.GetCommonSql(SqlIndex, ref SqlIndex) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            sqlStr = sqlStr + "\r\n" + SqlIndex;

            sqlStr = string.Format(sqlStr, args);

            return this.myGetExecOrder(sqlStr);
        }

        /// <summary>
        /// 根据whereIndex查询门诊处方执行次数
        /// </summary>
        /// <param name="SqlIndex"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private ArrayList QueryOrderBaseExec(string SqlIndex, params string[] args)
        {
            string sqlStr = "";
            if (this.myGetExecuteSelectSql(ref sqlStr) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            if (this.Sql.GetCommonSql(SqlIndex, ref SqlIndex) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            sqlStr = sqlStr + "\r\n" + SqlIndex;

            sqlStr = string.Format(sqlStr, args);

            return this.myGetExecOrder(sqlStr);
        }

        /// <summary>
        /// 根据SQL语句查询门诊处方
        /// </summary>
        /// <param name="whereSQL">这里是SQL语句，不是SQLID</param>
        /// <param name="args"></param>
        /// <returns></returns>
        public ArrayList QueryOrder(string whereSQL, params string[] args)
        {
            string sqlStr = "";
            if (this.myGetSelectSql(ref sqlStr) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            sqlStr = sqlStr + "\r\n" + whereSQL;

            sqlStr = string.Format(sqlStr, args);

            return this.myGetExecOrder(sqlStr);
        }

        /// <summary>
        /// 根据处方号查询医嘱
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        public ArrayList QueryOrderByRecipeNO(string clinicCode, string recipeNO)
        {
            return this.QueryOrderBase("Order.OutPatient.Order.Query.Where.4", clinicCode, recipeNO);
        }

        /// <summary>
        /// 根据处方号查询医嘱
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        public ArrayList QueryOrderByRecipeNO(string recipeNO)
        {
            return this.QueryOrderBase("Order.OutPatient.Order.Query.ByRecipeNO", recipeNO);
        }

        /// <summary>
        /// 查询一条医嘱
        /// 增加clinic_code优化查询速率{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.Order QueryOneOrder(string clinicCode, string sequenceNO)
        {
            ArrayList al = this.QueryOrderBase("Order.OutPatient.Order.Query.Where.2", sequenceNO, clinicCode);
            if (al == null)
            {
                return null;
            }
            if (al.Count <= 0)
            {
                return null;
            }
            return al[0] as FS.HISFC.Models.Order.OutPatient.Order;
        }
        /// <summary>
        /// 查询一条医嘱
        /// 增加clinic_code优化查询速率{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.Order QueryOneOrder(string clinicCode, string sequenceNO, string recipeNO)
        {
            ArrayList al = this.QueryOrderBase("Order.OutPatient.Order.Query.Where.8", sequenceNO, clinicCode, recipeNO);
            if (al == null)
            {
                return null;
            }
            if (al.Count <= 0)
            {
                return null;
            }
            return al[0] as FS.HISFC.Models.Order.OutPatient.Order;
        }
        /// <summary>
        /// 批量查询门诊处方
        /// 增加clinic_code优化查询速率{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ArrayList QueryBatchOrder(string clinicCode, string[] batchSeq)
        {
            string strBatchSeq = "''";
            for (int i = 0; i < batchSeq.Length; i++)
            {
                strBatchSeq += ",'" + batchSeq[i] + "'";
            }

            return this.QueryOrderBase("Order.OutPatient.Order.BatchQuery.ByClinicAndSeq", clinicCode, strBatchSeq);
        }

        /// <summary>
        /// 根据医嘱序号查询一条医嘱
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.Order QueryOneOrder(string sequenceNO)
        {
            ArrayList al = this.QueryOrderBase("Order.OutPatient.Order.Query.Where.7", sequenceNO);
            if (al == null)
            {
                return null;
            }
            if (al.Count <= 0)
            {
                return null;
            }
            return al[0] as FS.HISFC.Models.Order.OutPatient.Order;
        }

        /// <summary>
        /// 获得看诊序号列表
        /// </summary>
        /// <param name="cardNo">门诊卡号</param>
        /// <returns></returns>
        public ArrayList QuerySeeNoListByCardNo(string cardNo)
        {
            string sql = "Order.OutPatient.Order.GetSeeNoList";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                sql = string.Format(sql, cardNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();
                obj.Name = this.Reader[1].ToString();
                obj.Memo = this.Reader[2].ToString();
                try
                {
                    obj.User01 = this.Reader[3].ToString();
                    obj.User02 = this.Reader[4].ToString();
                    obj.User03 = this.Reader[5].ToString();
                }
                catch { }
                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }
        /// <summary>
        /// 获得看诊序号列表
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="cardNo"></param>
        /// <returns></returns>
        public ArrayList QuerySeeNoListByCardNo(string clinicNo, string cardNo)
        {
            string sql = "Order.OutPatient.Order.GetSeeNoList.2";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1) return null;
            try
            {
                sql = string.Format(sql, clinicNo, cardNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();
                obj.Name = this.Reader[1].ToString();
                obj.Memo = this.Reader[2].ToString();
                try
                {
                    obj.User01 = this.Reader[3].ToString();
                    obj.User02 = this.Reader[4].ToString();
                    if (Reader.FieldCount > 5)
                    {
                        obj.User03 = this.Reader[5].ToString();
                    }
                }
                catch { }
                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }
        /// <summary>
        /// 查询看诊序号根据名子
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ArrayList QuerySeeNoListByName(string name)
        {
            string sql = "Order.OutPatient.Order.GetSeeNoList.Name";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                sql = string.Format(sql, name);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList al = new ArrayList();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();
                obj.Name = this.Reader[1].ToString();
                obj.Memo = this.Reader[2].ToString();
                try
                {
                    obj.User01 = this.Reader[3].ToString();
                    obj.User02 = this.Reader[4].ToString();
                    obj.User03 = this.Reader[5].ToString();
                }
                catch { }
                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// 取得药品处方号通过门诊号和看诊号
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="seeNo"></param>
        /// <returns></returns>
        public ArrayList GetPhaRecipeNoByClinicNoAndSeeNo(string clinicNo, string seeNo)
        {
            string sql = "Order.OutPatient.Order.GetPhaRecipeNoByClinicNoAndSeeNo";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                sql = string.Format(sql, clinicNo, seeNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(sql) == -1) return null;
            ArrayList alRecipe = new ArrayList();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();

                alRecipe.Add(obj);
            }
            this.Reader.Close();
            return alRecipe;
        }

        /// <summary>
        /// 获取处方号通过门诊号和看诊号
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="seeNo"></param>
        /// <param name="flag">0：全部、1：药品、2非药品</param>
        /// <returns></returns>
        public IList<FS.FrameWork.Models.NeuObject> GetRecipeNoByClinicNoAndSeeNo(string clinicNo, string seeNo, string flag)
        {
            string sql = "Order.OutPatient.Order.GetRecipeNoByClinicNoAndSeeNo";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                sql = string.Format(sql, clinicNo, seeNo, flag);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(sql) == -1) return null;
            IList<FS.FrameWork.Models.NeuObject> iRecipe = new List<FS.FrameWork.Models.NeuObject>();
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();

                iRecipe.Add(obj);
            }
            this.Reader.Close();
            return iRecipe;
        }



        /// <summary>
        /// 根据发票号获取处方信息
        /// </summary>
        /// <param name="invociceNo"></param>
        /// <returns></returns>
        public ArrayList QueryRecipeListByInvoiceNo(string invociceNo)
        {
            string sql = "Order.OutPatient.Order.QueryRecipeNOByInvoiceNo";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                sql = string.Format(sql, invociceNo);

                if (this.ExecQuery(sql) == -1)
                {
                    return null;
                }

                ArrayList alRecipe = new ArrayList();
                FS.HISFC.Models.Base.Spell obj = null;
                while (this.Reader.Read())
                {
                    obj = new FS.HISFC.Models.Base.Spell();
                    //处方号
                    obj.ID = this.Reader[0].ToString();
                    //医生
                    obj.Name = this.Reader[1].ToString();
                    //操作时间
                    obj.Memo = this.Reader[2].ToString();
                    //卡号
                    obj.SpellCode = this.Reader[3].ToString();
                    //姓名
                    obj.WBCode = this.Reader[4].ToString();
                    //发票号
                    obj.UserCode = this.Reader[5].ToString();

                    alRecipe.Add(obj);
                }
                return alRecipe;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
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
        /// 根据卡号查询过往就诊未执行的医嘱记录
        /// </summary>
        /// <param name="CardNO"></param>
        /// <param name="ClincNo"></param>
        /// <returns></returns>
        public ArrayList QueryLastOrderListByCardNo(string CardNO, string ClincNo)
        {
            string sql = "", sqlSelect = "", sqlWhere = "Order.OutPatient.Order.QueryLastOrderByCardNO";

            this.Sql.GetCommonSql("Order.OutPatient.Order.Query.Select.LastOrder", ref sqlSelect);

            //if (this.myGetSelectSql(ref sqlSelect) == -1)
            //{
            //    this.Err = this.Sql.Err;
            //    return null;
            //}
            if (this.Sql.GetCommonSql(sqlWhere, ref sqlWhere) == -1) return null;
            sql = sqlSelect + " " + sqlWhere;
            sql = string.Format(sql, CardNO, ClincNo);
            return this.myGetExecOrder(sql);
        }
        #endregion

        #region 门诊病历

        #region 废弃的方法
        /// <summary>
        /// 根据传入的实体更新或者插入门诊病历
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="casehistory"></param>
        /// <returns></returns>

        //public int SetCaseHistory(FS.HISFC.Models.Registration.Register reg, FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory casehistory)
        //{
        //    int iReturn = this.UpdateCaseHistory(reg, casehistory);
        //    if (iReturn == -1)
        //        return -1;
        //    else if (iReturn == 0)
        //        return this.InsertCaseHistory(reg, casehistory);
        //    else
        //        return 1;
        //}
        #endregion

        /// <summary>
        /// 插入一条病历
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="casehistory"></param>
        /// <returns></returns>
        public int InsertCaseHistory(FS.HISFC.Models.Registration.Register reg, FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory casehistory)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.InsertCase", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = System.String.Format(strSql,
                                              reg.ID, //门诊流水号，需替换
                                              reg.PID.CardNO,
                                              reg.Name, //患者姓名
                                              reg.Sex.Name,
                                              reg.Age,
                                              reg.DoctorInfo.Templet.Dept.ID,
                                              reg.Pact.PayKind.Name,
                                              casehistory.CaseMain,
                                              casehistory.CaseNow,
                                              casehistory.CaseOld,
                                              casehistory.CaseAllery,
                                              casehistory.IsAllery == true ? "1" : "0",
                                              casehistory.IsInfect == true ? "1" : "0",
                                              casehistory.CheckBody,
                                              casehistory.CaseDiag,
                                              casehistory.Memo,
                                              this.Operator.ID,
                                              casehistory.User01,
                                              casehistory.Memo2,
                                              casehistory.Emr_Educational,  //{b2a1f044-36fb-4beb-b1d4-017d8a2b0c65}
                                              casehistory.EducationContent,
                                              casehistory.PatientDiagnose,
                                              casehistory.MedicationKnowledge,
                                              casehistory.DiteKnowledge,
                                              casehistory.DiseaseKnowledge,
                                              casehistory.EducationalEffect,
                                              casehistory.TrafficKnowledge,
                                              casehistory.SupExamination



                                              );  // //  {4694CFAC-9041-496a-93C1-FAE7863E055E}
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新一条病历
        /// </summary>
        /// <param name="reg"></param>
        /// <param name="casehistory"></param>
        /// <returns></returns>
        public int UpdateCaseHistory(FS.HISFC.Models.Registration.Register reg, FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory casehistory, string oldOperTime)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.UpdateCase", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                /*
                 UPDATE MET_CAS_HISTORY
                    SET    CASEMAIN = '{0}',--主诉
                           CASENOW = '{1}',--现病史
                           CASEOLD = '{2}',--既往史
                           CASEALLERY = '{3}',--过敏史
                           ALLERY_FLAG = '{4}',--是否过敏
                           INFECT_FLAG = '{5}',--是否传染病
                           CHECKBODY = '{6}',--查体 
                           DIAGNOSE = '{7}',--诊断
                           MEMO = '{8}',--备注
                           OPER_CODE = '{9}',--操作员
                           OPER_DATE = to_date('{10}','YYYY-MM-DD hh24:Mi:SS')--操作日期
                 *         memo2='{13}'
                    WHERE  CLINIC_CODE = '{11}'--门诊流水号 
                           and oper_date=to_date('{12}','YYYY-MM-DD hh24:Mi:SS')--操作时
                 */
                strSql = System.String.Format(strSql,
                                              casehistory.CaseMain,
                                              casehistory.CaseNow,
                                              casehistory.CaseOld,
                                              casehistory.CaseAllery,
                                              casehistory.IsAllery == true ? "1" : "0",
                                              casehistory.IsInfect == true ? "1" : "0",
                                              casehistory.CheckBody,
                                              casehistory.CaseDiag,
                                              casehistory.Memo,
                                              this.Operator.ID,
                                              casehistory.CaseOper.OperTime.ToString(),//本次操作时间
                                              reg.ID,
                                              oldOperTime, //上一次的操作时间
                                              casehistory.User01,
                                              casehistory.Memo2,    // //  {4694CFAC-9041-496a-93C1-FAE7863E055E}
                                              casehistory.Emr_Educational,   //{b2a1f044-36fb-4beb-b1d4-017d8a2b0c65}
                                              casehistory.EducationContent,
                                              casehistory.PatientDiagnose,
                                              casehistory.MedicationKnowledge,
                                              casehistory.DiteKnowledge,
                                              casehistory.DiseaseKnowledge,
                                              casehistory.EducationalEffect,
                                              casehistory.TrafficKnowledge,
                                              casehistory.SupExamination

                                              ); //门诊流水号，需替换  
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据门诊流水号查询一条门诊病历
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory QueryCaseHistoryByClinicCode(string clinicCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.GetCase", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            strSql = System.String.Format(strSql, clinicCode);
            ArrayList al = this.GetMyObject(strSql);
            if (al == null)
                return null;
            else if (al.Count == 0)
                return null;
            else
                return al[0] as FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory;
        }

        /// <summary>
        /// 根据门诊流水号和操作时间查询一条门诊病历
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory QueryCaseHistoryByClinicCode(string clinicCode, string operTime)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.GetCase1", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            strSql = System.String.Format(strSql, clinicCode, operTime);
            ArrayList al = this.GetMyObject(strSql);
            if (al == null)
                return null;
            else if (al.Count == 0)
                return null;
            else
                return al[0] as FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory;
        }

        /// <summary>
        /// 根据门诊号查询门诊所有病历
        /// </summary>
        /// <param name="CardNO"></param>
        /// <returns></returns>
        public ArrayList QueryAllCaseHistory(string CardNO)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.GetAllCase", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            strSql = System.String.Format(strSql, CardNO);
            return this.GetMyObjectByCardNO(strSql);
        }

        /// <summary>
        /// 通过门诊号取病历最大操作时间
        /// </summary>
        /// <param name="ClinicCode"></param>
        /// <returns></returns>
        public DateTime QueryMaxOperTimeByClinicCode(string ClinicCode)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.GetMaxOperDateByClinicCode", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return System.DateTime.MinValue;
            }
            strSql = System.String.Format(strSql, ClinicCode);
            string strReturn = "";
            strReturn = this.ExecSqlReturnOne(strSql);
            if (strReturn != "" && strReturn != null)
            {
                return FS.FrameWork.Function.NConvert.ToDateTime(strReturn);
            }
            else
            {
                return System.DateTime.MinValue;
            }
        }

        #region 私有函数

        /// <summary>
        /// 得到病历实体
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList GetMyObjectByCardNO(string strSql)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1) return null;
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
                obj.ID = this.Reader[0].ToString();//流水号
                obj.Name = this.Reader[1].ToString();//姓名
                if (!this.Reader.IsDBNull(2))
                    obj.Memo = this.Reader[2].ToString();
                //User01是操作时间 路志鹏 2007-5-9
                obj.User01 = this.Reader[3].ToString();
                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// 得到病历实体
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList GetMyObject(string strSql)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1) return null;
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory casehistory = new FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory();
                casehistory.CaseMain = this.Reader.GetValue(0).ToString();//主诉
                casehistory.CaseNow = this.Reader.GetValue(1).ToString();//现病史
                casehistory.CaseOld = this.Reader.GetValue(2).ToString();//既往史
                casehistory.CaseAllery = this.Reader.GetValue(3).ToString();//过敏史
                casehistory.CheckBody = this.Reader.GetValue(4).ToString();//查体
                casehistory.CaseDiag = this.Reader.GetValue(5).ToString();//诊断
                casehistory.Memo = this.Reader.GetValue(6).ToString();//备注
                casehistory.Name = this.Reader.GetValue(7).ToString();//姓名
                casehistory.ID = this.Reader.GetValue(8).ToString();//门诊流水号

                if (!this.Reader.IsDBNull(9))
                    casehistory.IsAllery = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader.GetValue(9).ToString());//是否过敏
                if (!this.Reader.IsDBNull(10))
                    casehistory.IsInfect = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader.GetValue(10).ToString());//是否传染病
                //操作时间
                casehistory.CaseOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader.GetValue(11));
                casehistory.User01 = this.Reader.GetValue(12).ToString();//处理
                casehistory.Memo2 = this.Reader.GetValue(13).ToString();//备注2  // //  {4694CFAC-9041-496a-93C1-FAE7863E055E}

                //{b2a1f044-36fb-4beb-b1d4-017d8a2b0c65}
                if (!this.Reader.IsDBNull(14))
                    casehistory.Emr_Educational = this.Reader.GetValue(14).ToString();
                if (!this.Reader.IsDBNull(15))
                    casehistory.EducationContent = this.Reader.GetValue(15).ToString();
                if (!this.Reader.IsDBNull(16))
                    casehistory.PatientDiagnose = this.Reader.GetValue(16).ToString();
                if (!this.Reader.IsDBNull(17))
                    casehistory.MedicationKnowledge = this.Reader.GetValue(17).ToString();
                if (!this.Reader.IsDBNull(18))
                    casehistory.DiteKnowledge = this.Reader.GetValue(18).ToString();
                if (!this.Reader.IsDBNull(19))
                    casehistory.DiseaseKnowledge = this.Reader.GetValue(19).ToString();
                if (!this.Reader.IsDBNull(20))
                    casehistory.EducationalEffect = this.Reader.GetValue(20).ToString();
                if (!this.Reader.IsDBNull(21))
                    casehistory.TrafficKnowledge = this.Reader.GetValue(21).ToString();
                if (this.Reader.FieldCount > 22)
                {
                    if (!this.Reader.IsDBNull(22))
                    {
                        casehistory.DeptID = this.Reader.GetValue(22).ToString();
                    }
                }

                if (this.Reader.FieldCount > 23)
                {
                    if (!this.Reader.IsDBNull(23))
                    {
                        casehistory.SupExamination = this.Reader.GetValue(23).ToString();
                    }
                }


                al.Add(casehistory);
            }
            this.Reader.Close();
            return al;
        }

        #endregion

        #endregion

        #region 门诊病历模板

        /// <summary>
        /// 获取病历模板流水号
        /// </summary>
        /// <returns></returns>
        public string GetModuleSeq()
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.GetModuleSeq", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return "";
            }
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "执行错误";
                return "";
            }
            string ID = "";
            while (this.Reader.Read())
            {
                ID = this.Reader[0].ToString();
            }
            this.Reader.Close();
            ID = ID.PadLeft(10, '0');
            return ID;
        }

        /// <summary>
        /// 根据传入的实体更新或者插入门诊病历模板
        /// </summary>
        /// <param name="casehistory"></param>
        /// <returns></returns>
        public int SetCaseModule(FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory casehistory)
        {
            int i = this.UpdateCaseModule(casehistory);
            if (i == -1)
                return -1;
            else if (i == 0)
                return this.InsertCaseModule(casehistory);
            else
                return 1;
        }

        /// <summary>
        /// 插入一条记录
        /// </summary>
        /// <param name="casehistory"></param>
        /// <returns></returns>
        public int InsertCaseModule(FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory casehistory)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.InsertModule", ref strSql) == -1)
            {
                this.Err = "没有找到Order.OutPatient.Case.InsertModule字段";
                return -1;
            }
            try
            {
                strSql = System.String.Format(strSql,
                                              casehistory.ID,
                                              casehistory.Name,
                                              casehistory.DeptID,
                                              casehistory.CaseMain,
                                              casehistory.CaseNow,
                                              casehistory.CaseOld,
                                              casehistory.CaseAllery,
                                              casehistory.CheckBody,
                                              casehistory.CaseDiag,
                                              casehistory.Memo,
                                              casehistory.ModuleType,
                                              casehistory.DoctID,
                                              this.Operator.ID,
                                              casehistory.User01);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新病历模板Type
        /// </summary>
        /// <param name="ModuleType">模板类型</param>
        /// <param name="Module_NO">模板ID</param>
        /// <returns></returns>
        public int UpdateCaseModuleType(string ModuleType, string Module_NO)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.UpdateModuleType", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = System.String.Format(strSql,
                                              ModuleType, Module_NO);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="casehistory"></param>
        /// <returns></returns>
        public int UpdateCaseModule(FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory casehistory)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.UpdateModule", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = System.String.Format(strSql,
                                              casehistory.Name,
                                              casehistory.DeptID,
                                              casehistory.ModuleType,
                                              casehistory.CaseMain,
                                              casehistory.CaseNow,
                                              casehistory.CaseOld,
                                              casehistory.CaseAllery,
                                              casehistory.CheckBody,
                                              casehistory.CaseDiag,
                                              casehistory.Memo,
                                              casehistory.DoctID,
                                              this.Operator.ID,
                                              casehistory.ID,
                                              casehistory.User01);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="moduleNo"></param>
        /// <returns></returns>
        public int DeleteCaseModule(string moduleNo)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.DelModule", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            try
            {
                strSql = System.String.Format(strSql, moduleNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// 根据模板流水号查询一条记录
        /// </summary>
        /// <param name="moduleNO"></param>
        /// <returns></returns>
        public FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory QueryCaseModule(string moduleNO)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.GetModule", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            strSql = System.String.Format(strSql, moduleNO);
            ArrayList al = this.GetMyModule(strSql);
            if (al == null)
                return null;
            else if (al.Count == 0)
                return new FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory();
            else
                return al[0] as FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory;
        }

        /// <summary>
        /// 根据类别获得所有模板
        /// </summary>
        /// <param name="moduletype"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public ArrayList QueryAllCaseModule(string moduletype, string Code)
        {
            string strSql = "";
            if (moduletype == "1")//科室
            {
                if (this.Sql.GetCommonSql("Order.OutPatient.Case.GetAllModuleByDeptCode", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return null;
                }
            }
            else
            {
                if (this.Sql.GetCommonSql("Order.OutPatient.Case.GetAllModuleByOperId", ref strSql) == -1)
                {
                    this.Err = this.Sql.Err;
                    return null;
                }
            }
            strSql = System.String.Format(strSql, moduletype, Code);
            return this.GetMyModule(strSql);
        }

        #region 私有函数
        /// <summary>
        /// 得到病历模板实体
        /// </summary>
        /// <param name="strSql"></param>
        /// <returns></returns>
        private ArrayList GetMyModule(string strSql)
        {
            ArrayList al = new ArrayList();
            if (this.ExecQuery(strSql) == -1) return null;
            while (this.Reader.Read())
            {
                FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory casehistory = new FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory();
                casehistory.CaseMain = this.Reader.GetValue(0).ToString();//主诉
                casehistory.CaseNow = this.Reader.GetValue(1).ToString();//现病史
                casehistory.CaseOld = this.Reader.GetValue(2).ToString();//既往史
                casehistory.CaseAllery = this.Reader.GetValue(3).ToString();//过敏史
                casehistory.CheckBody = this.Reader.GetValue(4).ToString();//查体
                casehistory.CaseDiag = this.Reader.GetValue(5).ToString();//诊断
                casehistory.Memo = this.Reader.GetValue(6).ToString();//备注
                casehistory.Name = this.Reader.GetValue(7).ToString();//模板名称
                casehistory.ID = this.Reader.GetValue(8).ToString();//模板流水号
                casehistory.ModuleType = this.Reader.GetValue(9).ToString();//类别
                casehistory.DoctID = this.Reader.GetValue(10).ToString();//医师编码
                casehistory.DeptID = this.Reader.GetValue(11).ToString();//科室
                casehistory.User01 = this.Reader.GetValue(12).ToString();//处理
                al.Add(casehistory);
            }
            this.Reader.Close();
            return al;
        }
        #endregion

        #endregion

        #region 私有函数

        /// <summary>
        /// 获得sql，传入参数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        protected string myGetCommonSql(string sql, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            #region sql
            //   0--看诊序号 ,1 --项目流水号,2 --门诊号,3   --病历号 ,4    --挂号日期
            //   5 --挂号科室,6   --项目代码,7   --项目名称, 8  --规格, 9  --1药品，2非药品
            //   10   --系统类别,   --最小费用代码,   --单价,   --开立数量,   --付数
            //    --包装数量,   --计价单位,   --自费金额0,   --自负金额0,   --报销金额0
            //   --基本剂量,   --自制药,   --药品性质，普药、贵药,   --每次用量
            //     --每次用量单位,   --剂型代码,   --频次,   --频次名称,   --使用方法
            //     --用法名称,   --用法英文缩写,   --执行科室代码,   --执行科室名称
            //      --主药标志,   --组合号,   --1不需要皮试/2需要皮试，未做/3皮试阳/4皮试阴
            //     --院内注射次数,   --备注,   --开立医生,   --开立医生名称,   --医生科室
            //     --开立时间,   --处方状态,1开立，2收费，3确认，4作废,   --作废人,   --作废时间
            //        --加急标记0普通/1加急,   --样本类型,   --检体,   --申请单号
            //     --0不是附材/1是附材,   --是否需要确认，1需要，0不需要,   --确认人
            //        --确认科室,   --确认时间,   --0未收费/1收费,   --收费员
            //       --收费时间,   --处方号,    --处方内流水号,     --发药药房，    
            //      --开立单位是否是最小单位 1 是 0 不是，      --医嘱类型（目前没有）
            #endregion

            //if(order.Item.IsPharmacy)//药品
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item pItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                //{9BAE643C-57BF-4dc5-889E-6B5F6B3E1E38} 由于接入电子申请单，apply_no字段赋order.ApplyNo20100505 yangw
                System.Object[] s = {
                                        order.SeeNO ,                                        
                                        FS.FrameWork.Function.NConvert.ToInt32(order.ID),
                                        order.Patient.ID,                                        
                                        order.Patient.PID.CardNO,                                        
                                        order.RegTime,                                        
										order.InDept.ID,                                        
                                        pItem.ID,                                        
                                        pItem.Name,                                        
                                        pItem.Specs,                                        
                                        "1",                                        
										order.Item.SysClass.ID,                                        
                                        order.Item.MinFee.ID,                                        
                                        order.Item.Price,
                                        order.Qty,
                                        order.HerbalQty,                                        
										pItem.PackQty,
                                        pItem.PriceUnit,
                                        order.FT.OwnCost ,
                                        order.FT.PayCost,
                                        order.FT.PubCost,                                        
										pItem.BaseDose,
                                        FS.FrameWork.Function.NConvert.ToInt32(pItem.Product.IsSelfMade),
                                        pItem.Quality.ID,
                                        order.DoseOnce,                                        
										order.DoseUnit,
                                        pItem.DosageForm.ID,
                                        order.Frequency.ID,
                                        order.Frequency.Name,
                                        order.Usage.ID,                                        
										order.Usage.Name,
                                        order.Usage.Memo,
                                        order.ExeDept.ID,
                                        order.ExeDept.Name,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug),
                                        order.Combo.ID,
                                        ((Int32)order.HypoTest).ToString(),
										order.InjectCount,
                                        order.Memo,
                                        order.ReciptDoctor.ID,
                                        order.ReciptDoctor.Name,
                                        order.ReciptDept.ID,                                        
										order.MOTime,                                        
                                        order.Status,
                                        order.DCOper.ID,
                                        order.DCOper.OperTime,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.IsEmergency),
                                        order.Sample.Name,
                                        order.CheckPartRecord,
                                        order.ApplyNo,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.IsSubtbl),
                                        FS.FrameWork.Function.NConvert.ToInt32(order.IsNeedConfirm),
                                        order.ConfirmOper.ID,                                        
										order.ConfirmOper.Dept.ID,
                                        order.ConfirmOper.OperTime,
                                        FS.FrameWork.Function.NConvert.ToInt32(order.IsHaveCharged),
                                        order.ChargeOper.ID,                                        
										order.ChargeOper.OperTime,
                                        order.ReciptNO,
                                        order.SequenceNO,                                        
                                        order.StockDept.ID,
                                        order.MinunitFlag,
                                        order.UseDays.ToString(),
                                        order.SubCombNO,
                                        order.ExtendFlag1,                                        
										order.ReciptSequence,
                                        order.NurseStation.Memo,
                                        order.SortID,
                                        order.DoseOnceDisplay,
                                        order.DoseUnitDisplay,
                                        order.FirstUseNum,
                                        //order.Patient.Pact.ID,         //{5C7887F1-A4D5-4a66-A814-18D45367443E} 表中无此字段
                                        //order.Patient.Pact.PayKind.ID, //{5C7887F1-A4D5-4a66-A814-18D45367443E} 表中无此字段
                                        order.QuitFlag.ToString(),    //{5C7887F1-A4D5-4a66-A814-18D45367443E} 允许退费的标识
                                        order.QuitOper.ID,            //{5C7887F1-A4D5-4a66-A814-18D45367443E} 操作人
                                        order.QuitOper.OperTime       //{5C7887F1-A4D5-4a66-A814-18D45367443E} 操作时间
                                    };

                try
                {
                    string sReturn = string.Format(sql, s);
                    return sReturn;
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                    return null;
                }
            }
            else//非药品
            {
                FS.HISFC.Models.Fee.Item.Undrug pItem = order.Item as FS.HISFC.Models.Fee.Item.Undrug;
                //{9BAE643C-57BF-4dc5-889E-6B5F6B3E1E38} 由于接入电子申请单，apply_no字段赋order.ApplyNo 20100505 yangw
                System.Object[] s = {
                                        order.SeeNO,
                                        FS.FrameWork.Function.NConvert.ToInt32(order.ID),
                                        order.Patient.ID,
                                        order.Patient.PID.CardNO,
                                        order.RegTime,                                        
										order.InDept.ID,
                                        pItem.ID,
                                        pItem.Name,
                                        pItem.Specs,
                                        "2",                                        
										order.Item.SysClass.ID,
                                        order.Item.MinFee.ID,
                                        order.Item.Price,
                                        order.Qty,
                                        order.HerbalQty,                                        
										pItem.PackQty,
                                        pItem.PriceUnit,
                                        order.FT.OwnCost ,
                                        order.FT.PayCost,
                                        order.FT.PubCost,                                        
										"0",
                                        0,
                                        "",
                                        order.DoseOnce,                                        
										order.DoseUnit,
                                        "",
                                        order.Frequency.ID,
                                        order.Frequency.Name,
                                        order.Usage.ID,                                        
										order.Usage.Name,
                                        order.Usage.Memo,
                                        order.ExeDept.ID,
                                        order.ExeDept.Name,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.Combo.IsMainDrug),
                                        order.Combo.ID,
                                        ((Int32)order.HypoTest).ToString(),                                        
										order.InjectCount,
                                        order.Memo,
                                        order.ReciptDoctor.ID,
                                        order.ReciptDoctor.Name,
                                        order.ReciptDept.ID,                                        
										order.MOTime,                                        
                                        order.Status,
                                        order.DCOper.ID,
                                        order.DCOper.OperTime,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.IsEmergency),
                                        order.Sample.Name,
                                        order.CheckPartRecord,
                                        order.ApplyNo,                                        
										FS.FrameWork.Function.NConvert.ToInt32(order.IsSubtbl),
                                        FS.FrameWork.Function.NConvert.ToInt32(order.IsNeedConfirm),
                                        order.ConfirmOper.ID,                                        
										order.ConfirmOper.Dept.ID,
                                        order.ConfirmOper.OperTime,
                                        FS.FrameWork.Function.NConvert.ToInt32(order.IsHaveCharged),
                                        order.ChargeOper.ID,                                        
										order.ChargeOper.OperTime,
                                        order.ReciptNO,
                                        order.SequenceNO,                                        
                                        order.StockDept.ID,
                                        order.MinunitFlag,
                                        "",                                        
                                        order.SubCombNO,
                                        order.ExtendFlag1,                                        
										order.ReciptSequence,
                                        order.NurseStation.Memo,
                                        order.SortID,
                                        order.DoseOnceDisplay,
                                        order.DoseUnitDisplay,
                                        order.FirstUseNum,
                                        //order.Patient.Pact.ID,         //{5C7887F1-A4D5-4a66-A814-18D45367443E} 表中无此字段
                                        //order.Patient.Pact.PayKind.ID, //{5C7887F1-A4D5-4a66-A814-18D45367443E} 表中无此字段
                                        order.QuitFlag.ToString(),    //{5C7887F1-A4D5-4a66-A814-18D45367443E} 允许退费的标识
                                        order.QuitOper.ID,            //{5C7887F1-A4D5-4a66-A814-18D45367443E} 操作人
                                        order.QuitOper.OperTime       //{5C7887F1-A4D5-4a66-A814-18D45367443E} 操作时间
                                    };
                try
                {
                    string sReturn = string.Format(sql, s);
                    return sReturn;
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    this.WriteErr();
                    return null;
                }
            }
        }


        /// <summary>
        /// 获得查询sql语句
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int myGetSelectSql(ref string sql)
        {
            return this.Sql.GetCommonSql("Order.OutPatient.Order.Query.Select", ref sql);
        }

        /// <summary>
        /// 获得查询sql语句
        /// </summary>{B039D312-F279-446b-974F-1201C4F32B64}
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int myGetExecuteSelectSql(ref string sql)
        {
            return this.Sql.GetCommonSql("Order.OutPatient.Order.Query.Select.execule", ref sql);
        }

        /// <summary>
        /// 获得执行医嘱信息
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected ArrayList myGetExecOrder(string sql)
        {
            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            ArrayList al = new ArrayList();

            while (this.Reader.Read())
            {
                FS.HISFC.Models.Order.OutPatient.Order order = new FS.HISFC.Models.Order.OutPatient.Order();
                try
                {
                    order.SeeNO = this.Reader[0].ToString();
                    order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());//项目流水好
                    order.ID = this.Reader[1].ToString();//项目流水好
                    order.Patient.ID = this.Reader[2].ToString();//门诊号
                    order.Patient.PID.CardNO = this.Reader[3].ToString();//病历卡号
                    order.RegTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);//挂号日期
                    order.ReciptDept.ID = this.Reader[5].ToString();//挂号科室 编码
                    if (this.Reader[9].ToString() == "1")//药品
                    {
                        FS.HISFC.Models.Pharmacy.Item item = new FS.HISFC.Models.Pharmacy.Item();
                        item.ID = this.Reader[6].ToString();
                        item.Name = this.Reader[7].ToString();
                        item.Specs = this.Reader[8].ToString();
                        item.SysClass.ID = this.Reader[10].ToString();
                        item.MinFee.ID = this.Reader[11].ToString();
                        item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                        item.BaseDose = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20]);
                        item.DoseUnit = this.Reader[24].ToString();
                        item.Product.IsSelfMade = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[21]);
                        item.Quality.ID = this.Reader[22].ToString();
                        item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15]);
                        item.DosageForm.ID = this.Reader[25].ToString();
                        item.PriceUnit = this.Reader[16].ToString();

                        //{6DBBDC62-2303-4d97-85EF-8BA2A622117A} 拆分属性 xuc
                        item.SplitType = this.Reader[61].ToString();

                        order.Item = item;

                    }
                    else if (this.Reader[9].ToString() == "2")//非药品
                    {
                        FS.HISFC.Models.Fee.Item.Undrug item = new FS.HISFC.Models.Fee.Item.Undrug();
                        item.ID = this.Reader[6].ToString();
                        item.Name = this.Reader[7].ToString();
                        item.Specs = this.Reader[8].ToString();
                        item.SysClass.ID = this.Reader[10].ToString();
                        item.MinFee.ID = this.Reader[11].ToString();
                        item.Price = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[12]);
                        item.PackQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[15]);
                        item.PriceUnit = this.Reader[16].ToString();
                        order.Item = item;

                    }
                    else
                    {
                        this.Err = "读取met_ord_recipedetail，区分药品非药品出错，drug_flag=" + this.Reader[9].ToString();
                        this.WriteErr();
                        return null;
                    }
                    order.Qty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[13]);
                    order.HerbalQty = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[14]);
                    order.Unit = this.Reader[16].ToString();
                    order.FT.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17]);
                    order.FT.PayCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[18]);
                    order.FT.PubCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[19]);

                    order.DoseOnce = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23]);
                    order.DoseUnit = this.Reader[24].ToString();

                    order.Frequency.ID = this.Reader[26].ToString();
                    order.Frequency.Name = this.Reader[27].ToString();
                    order.Usage.ID = this.Reader[28].ToString();
                    order.Usage.Name = this.Reader[29].ToString();
                    order.Usage.Memo = this.Reader[30].ToString();
                    order.ExeDept.ID = this.Reader[31].ToString();
                    order.ExeDept.Name = this.Reader[32].ToString();
                    order.Combo.IsMainDrug = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[33]);
                    order.Combo.ID = this.Reader[34].ToString();
                    order.HypoTest = (FS.HISFC.Models.Order.EnumHypoTest)FS.FrameWork.Function.NConvert.ToInt32(this.Reader[35]);
                    order.InjectCount = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[36]);
                    order.Memo = this.Reader[37].ToString();
                    order.ReciptDoctor.ID = this.Reader[38].ToString();
                    order.ReciptDoctor.Name = this.Reader[39].ToString();
                    order.ReciptDept.ID = this.Reader[40].ToString();
                    //order.ReciptDept.Name =this.Reader[41].ToString();
                    order.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[41]);
                    order.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[42]);
                    order.DCOper.ID = this.Reader[43].ToString();
                    order.DCOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[44]);
                    order.IsEmergency = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[45]);
                    order.Sample.Name = this.Reader[46].ToString();
                    order.CheckPartRecord = this.Reader[47].ToString();
                    order.ApplyNo = this.Reader[48].ToString();
                    order.IsSubtbl = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[49]);
                    order.IsNeedConfirm = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[50]);
                    order.ConfirmOper.ID = this.Reader[51].ToString();
                    order.ConfirmOper.Dept.ID = this.Reader[52].ToString();
                    order.ConfirmOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[53]);
                    order.IsHaveCharged = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader[54]);
                    order.ChargeOper.ID = this.Reader[55].ToString();
                    order.ChargeOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[56]);
                    order.ReciptNO = this.Reader[57].ToString();
                    order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[58]);
                    order.StockDept.ID = this.Reader[59].ToString();
                    order.MinunitFlag = this.Reader[60].ToString();//最小单位标志
                    order.UseDays = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[62]);//{08024C29-12FE-4629-B982-C50AE9034B82}
                    order.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[63].ToString());//附材组合号（检验）
                    order.ExtendFlag1 = this.Reader[64].ToString();//接瓶信息
                    order.ReciptSequence = this.Reader[65].ToString();//收费序列
                    order.NurseStation.Memo = this.Reader[66].ToString();
                    order.SortID = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[67]);
                    #region {C3DF9328-3458-4bb4-895E-5B122B6582BB}

                    if (this.Reader[9].ToString() == "1")
                    {
                        order.DoseOnceDisplay = this.Reader[68].ToString();
                        if (order.DoseOnceDisplay.Length <= 0)
                            order.DoseOnceDisplay = order.DoseOnce.ToString();
                    }

                    order.DoseUnitDisplay = this.Reader[69].ToString();
                    order.FirstUseNum = this.Reader[70].ToString();

                    //{5C7887F1-A4D5-4a66-A814-18D45367443E} 表中无此字段
                    //if (this.Reader.FieldCount > 71)
                    //{
                    //    order.Patient.Pact.ID = Reader[71].ToString();
                    //} 
                    //if (this.Reader.FieldCount > 72)
                    //{
                    //    order.Patient.Pact.PayKind.ID = Reader[72].ToString();
                    //}
                    if (this.Reader.FieldCount > 71) //允许退费标识
                    {
                        order.QuitFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[71]);
                    }
                    if (this.Reader.FieldCount > 72) //操作人
                    {
                        order.QuitOper.ID = Reader[72].ToString();
                    }
                    if (this.Reader.FieldCount > 73) //操作时间
                    {
                        order.QuitOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[73]);
                    }
                    if (this.Reader.FieldCount > 74) //检验检查执行状态// {A046F9F6-0367-4ab5-9966-9C3F29C38C41}
                    {
                        order.User01 = Reader[74].ToString();
                    }
                    if (this.Reader.FieldCount > 75) //终端确认执行状态// {95F53D43-61DC-47a1-B090-EEC34C84A09C}
                    {

                        string confirm = Reader[75].ToString();
                        if (confirm != null && confirm != "")
                        {
                            order.User01 += "已执行" + confirm + "次";
                        }
                    }

                    #endregion
                }
                catch (Exception ex)
                {
                    this.Err = ex.Message;
                    return null;
                }
                finally
                {
                    if (!this.Reader.IsClosed)
                    {
                        this.Reader.Close();
                    }
                }
                al.Add(order);
            }
            this.Reader.Close();
            return al;
        }
        #endregion

        #region
        /// <summary>
        /// 获得用法和用法所带的附材(旧界面fin_opb_inject)
        /// </summary>
        /// <returns></returns>
        public Hashtable GetUsageAndSub()
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.OutPatient.Order.GetUsageAndSub", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            if (this.ExecQuery(strSql) < 0)
            {
                this.Err = "Exec Err" + this.Err;
                return null;
            }

            string usageCode = "";

            Hashtable hsUsageAndSub = new Hashtable();

            while (this.Reader.Read())
            {
                usageCode = this.Reader[0].ToString();

                if (!hsUsageAndSub.Contains(usageCode))
                {
                    ArrayList al = new ArrayList();

                    FS.HISFC.Models.Order.OrderSubtbl o = new FS.HISFC.Models.Order.OrderSubtbl();

                    o.ID = this.Reader[1].ToString();
                    o.QtyRule = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());

                    al.Add(o);

                    hsUsageAndSub.Add(usageCode, al);
                }
                else
                {
                    FS.HISFC.Models.Order.OrderSubtbl o = new FS.HISFC.Models.Order.OrderSubtbl();

                    o.ID = this.Reader[1].ToString();
                    o.QtyRule = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());

                    (hsUsageAndSub[usageCode] as ArrayList).Add(o);
                }
            }
            this.Reader.Close();
            return hsUsageAndSub;
        }

        /// <summary>
        /// 得用法和用法所带的附材(新界面met_com_subtblitem)
        /// </summary>
        /// <returns></returns>
        public Hashtable GetNewUsageAndSub()
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.OutPatient.Order.GetNewUsageAndSub", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }

            if (this.ExecQuery(strSql) < 0)
            {
                this.Err = "Exec Err" + this.Err;
                return null;
            }

            string usageCode = "";

            Hashtable hsUsageAndSub = new Hashtable();

            while (this.Reader.Read())
            {
                usageCode = this.Reader[0].ToString();

                if (!hsUsageAndSub.Contains(usageCode))
                {
                    ArrayList al = new ArrayList();

                    FS.HISFC.Models.Order.OrderSubtbl o = new FS.HISFC.Models.Order.OrderSubtbl();

                    o.ID = this.Reader[1].ToString();
                    o.QtyRule = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());

                    al.Add(o);

                    hsUsageAndSub.Add(usageCode, al);
                }
                else
                {
                    FS.HISFC.Models.Order.OrderSubtbl o = new FS.HISFC.Models.Order.OrderSubtbl();

                    o.ID = this.Reader[1].ToString();
                    o.QtyRule = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[2].ToString());

                    (hsUsageAndSub[usageCode] as ArrayList).Add(o);
                }
            }
            this.Reader.Close();
            return hsUsageAndSub;
        }
        #endregion

        #region 门诊新处方输入
        ///add by liuww 2011-3-8


        /// <summary>
        /// 按照卡号查处方历史
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="recipeType"></param>
        /// <returns></returns>
        public ArrayList QueryPatientRecipeByCardAndType(string cardNo, string recipeType, DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Order.QueryPatientRecipeByCardAndType", ref strSql) == -1)
            {
                this.Err = "没有找到索引为 Order.Order.QueryPatientRecipeByCardAndType的SQL语句";
                return null;
            }

            strSql = string.Format(strSql, cardNo, recipeType, dtBegin, dtEnd);

            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            ArrayList orderList = new ArrayList();

            FS.HISFC.Models.Order.OutPatient.Order order = null;
            while (this.Reader.Read())
            {
                order = new FS.HISFC.Models.Order.OutPatient.Order();

                order.Patient.ID = this.Reader[0].ToString();
                order.ReciptSequence = this.Reader[1].ToString();
                order.RecipeType.ID = this.Reader[2].ToString();
                order.ReciptNO = this.Reader[4].ToString();
                order.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                order.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());

                orderList.Add(order);
            }

            this.Reader.Close();

            return orderList;
        }

        /// <summary>
        /// 根据处方类别,和患者流水号,查询处方信息
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="recipeType"></param>
        /// <returns></returns>
        public ArrayList QueryPatientRecipeByType(string clinicNO, string recipeType)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Order.QueryPatientRecipeByType", ref strSql) == -1)
            {
                this.Err = "没有找到索引为 Order.Order.QueryPatientRecipeByType的SQL语句";
                return null;
            }

            strSql = string.Format(strSql, clinicNO, recipeType);

            if (this.ExecQuery(strSql) == -1)
            {
                return null;
            }

            ArrayList orderList = new ArrayList();

            FS.HISFC.Models.Order.OutPatient.Order order = null;

            while (this.Reader.Read())
            {
                order = new FS.HISFC.Models.Order.OutPatient.Order();

                order.Patient.ID = this.Reader[0].ToString();
                order.ReciptSequence = this.Reader[1].ToString();
                order.RecipeType.ID = this.Reader[2].ToString();
                order.ReciptNO = this.Reader[4].ToString();
                order.MOTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[5].ToString());
                order.Status = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());

                orderList.Add(order);
            }

            this.Reader.Close();

            return orderList;
        }

        /// <summary>
        /// 更新健康信息：身高、体重、血压
        /// </summary>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="SBP"></param>
        /// <param name="DBP"></param>
        /// <param name="clinicCode"></param>
        /// <param name="tem"></param>
        /// <returns></returns>
        public int UpdateHealthInfo(string height, string weight, string SBP, string DBP, string clinicCode, string tem, string bloodGlu)
        {
            return this.ExecNoQueryByIndex("Order.OutPatient.HealthInfo.UpdateByClinicCode", height, weight, SBP, DBP, clinicCode, tem, bloodGlu);
        }

        /// <summary>
        /// 获取健康信息：身高、体重、血压
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="SBP">血压：收缩压</param>
        /// <param name="DBP">血压：舒张压</param>
        /// <returns></returns>
        public int GetHealthInfo(string sqlIndex, ref string height, ref string weight, ref string SBP, ref string DBP, ref string tem, ref string bloodGlu, params string[] param)
        {
            try
            {
                if (this.ExecQueryByIndex(sqlIndex, param) < 0)
                {
                    return -1;
                }

                if (this.Reader != null && this.Reader.Read())
                {
                    height = this.Reader[0].ToString();
                    weight = this.Reader[1].ToString();
                    SBP = this.Reader[2].ToString();
                    DBP = this.Reader[3].ToString();
                    tem = this.Reader[4].ToString();
                    bloodGlu = this.Reader[5].ToString();
                    return 1;
                }
                else
                {
                    Err = "未找到挂号信息！";
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Err = ex.Message;
                return -1;
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
        /// 根据门诊流水号获取健康信息：身高、体重、血压
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="SBP">血压：收缩压</param>
        /// <param name="DBP">血压：舒张压</param>
        /// <returns></returns>
        public int GetHealthInfo(string clinicCode, ref string height, ref string weight, ref string SBP, ref string DBP, ref string tem, ref string bloodGlu)
        {
            return this.GetHealthInfo("Order.OutPatient.HealthInfo.GetByClinicCode", ref height, ref weight, ref SBP, ref DBP, ref tem, ref bloodGlu, clinicCode);
        }

        /// <summary>
        /// 根据门诊卡号获取最近一次健康信息：身高、体重、血压
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="days">查询时间段</param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="SBP"></param>
        /// <param name="DBP"></param>
        /// <param name="tem"></param>
        /// <param name="bloodGlu"></param>
        /// <returns></returns>
        public int GetHealthInfo(string cardNo, int days, ref string height, ref string weight, ref string SBP, ref string DBP, ref string tem, ref string bloodGlu)
        {
            DateTime dt = this.GetDateTimeFromSysDateTime().Date.AddDays(0 - days);
            return this.GetHealthInfo("Order.OutPatient.HealthInfo.GetByCardNo", ref height, ref weight, ref SBP, ref DBP, ref tem, ref bloodGlu, cardNo, dt.ToString());
        }

        /// <summary>
        /// {FD42E841-E918-4AA7-AC8E-76E2DB2ACEBF}获取一体机体重身高血压
        /// </summary>
        /// <param name="phone">电话</param>
        /// <param name="date">日期</param>
        /// <param name="IDCard">身份证</param>
        /// <param name="height">身高</param>
        /// <param name="weight">体重</param>
        /// <param name="bmi">bmi</param>
        /// <param name="higp">收缩压</param>
        /// <param name="lowp">扩张压</param>
        /// <param name="pulse">脉搏</param>
        public int GetByPhoneAndDate(string phone, string date, string IDCard, ref string height, ref string weight, ref string bmi, ref string higp, ref string lowp, ref string pulse)
        {
            string sql = @"select * 
                             from (select pc.height, pc.weight, pc.bmi, 
                                          pc.highpressure, pc.lowpressure, pc.pulse   
                                     from physicalcheck pc 
                                    where mobile='{0}' 
                                      and to_char(to_date(pc.measuretime,'yyyy-mm-dd HH24:mi:ss'),'yyyy-mm-dd') = '{1}'
                                    order by ID desc) 
                            where rownum<2 ";

            sql = string.Format(sql, phone, date);
            DataSet ds = new DataSet();
            int ret = this.ExecQuery(sql, ref ds);
            DataTable dt = null;
            if (ds != null && ds.Tables.Count > 0)
                dt = ds.Tables[0];
            if (dt == null) dt = GetByCardAndDate(date, IDCard);
            else
            {
                if (dt.Rows.Count <= 0) dt = GetByCardAndDate(date, IDCard);
            }
            //{58D38350-7973-4c6c-8D31-B30ADB39CFAA}
            if (dt != null && dt.Rows.Count > 0)
            {
                height = dt.Rows[0][0].ToString();
                weight = dt.Rows[0][1].ToString();
                bmi = dt.Rows[0][2].ToString();
                higp = dt.Rows[0][3].ToString();
                lowp = dt.Rows[0][4].ToString();
                pulse = dt.Rows[0][5].ToString();
            }
            return ret;
        }

        /// <summary>
        /// {FD42E841-E918-4AA7-AC8E-76E2DB2ACEBF}获取一体机血药 体重 身高 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="IDCard"></param>
        /// <returns></returns>
        public DataTable GetByCardAndDate(string date, string IDCard)
        {
            string sql = @"select * from (select pc.height, pc.weight, pc.bmi, 
                                                 pc.highpressure, pc.lowpressure, pc.pulse 
                                            from physicalcheck pc 
                                           where idcode='{0}' 
                                             and to_char(to_date(pc.measuretime,'yyyy-mm-dd HH24:mi:ss'),'yyyy-mm-dd')='{1}'
                                           order by ID desc) where rownum<2 ";

            sql = string.Format(sql, IDCard, date);
            DataSet ds = new DataSet();
            int ret = this.ExecQuery(sql, ref ds);
            DataTable dt = null;
            if (ds != null && ds.Tables.Count > 0)
                dt = ds.Tables[0];
            return dt;
        }

        #endregion

        /// <summary>
        /// 根据卡号查询时间段内的处方号
        /// </summary>
        /// <param name="cardNO"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public ArrayList QueryRecipeNOByCardNO(string cardNO, DateTime dtBegin, DateTime dtEnd)
        {
            string sql = "";
            string errText = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Order.QueryRecipeNOByCardNO", ref sql) == -1)
            {
                return null;
            }
            sql = string.Format(sql, cardNO, dtBegin.ToString(), dtEnd.ToString());
            try
            {
                if (this.ExecQuery(sql) == -1)
                {
                    errText = "执行查询sql失败";
                    return null;
                }
                ArrayList al = new ArrayList();
                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.OutPatient.Order orderObj = new FS.HISFC.Models.Order.OutPatient.Order();
                    orderObj.ID = this.Reader[0].ToString();
                    orderObj.Name = this.Reader[1].ToString();
                    orderObj.Memo = this.Reader[2].ToString();
                    al.Add(orderObj);
                }
                return al;
            }
            catch (Exception ex)
            {
                this.Err = errText + ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }
        }

        /// <summary>
        /// 翻译皮试信息
        /// </summary>
        /// <param name="i"></param>
        /// <returns>1 [免试] 2 [需皮试] 3 [+] 4 [-]</returns>
        public string TransHypotest(FS.HISFC.Models.Order.EnumHypoTest HypotestCode)
        {
            //return FS.FrameWork.Public.EnumHelper.Current.GetName(HypotestCode);

            switch ((int)HypotestCode)
            {
                case 0:
                    //return "不需要皮试";
                    return "";
                case 1:
                    return "[免试]";
                case 2:
                    return "[需皮试]";
                case 3:
                    return "[+]";
                case 4:
                    return "[-]";
                default:
                    return "[免试]";
            }
        }

        /// <summary>
        /// 草药插入处方
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int InsertInRecipe(FS.HISFC.Models.Order.Inpatient.Order order)
        {
            string strsql = string.Empty;
            if (this.Sql.GetCommonSql("Order.Item.InpatientRecipe.Insert", ref strsql) == -1)
            {
                return -1;
            }
            try
            {
                strsql = string.Format(strsql, order.Patient.ID,
                    //order.Patient.FT.User08,
                                                           order.ID,
                                                           order.Combo.ID,
                                                           order.Item.ID,
                                                           order.Item.Name,
                                                           order.DoseOnce,
                                                           order.Unit,
                                                           order.HerbalQty
                    //,
                    //order.Usage.User08
                    //,
                    //order.Usage.Name,
                    //order.Usage.User07,
                    //"0",
                    //order.Doctor.User05,
                    //order.Doctor.User06,
                    //order.Doctor.User07,
                    //order.Doctor.User08,
                    //order.DoctorDept.User03,
                    //order.DoctorDept.User04,
                    //order.DoctorDept.User05,
                    //order.DoctorDept.User06,
                    //order.DoctorDept.User07,
                    //order.DoctorDept.User08
                                                           );
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }

            if (this.ExecQuery(strsql) == -1)
            {
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// 根据项目编号查询申请单类型{D793A341-AD35-4685-8817-5614217969AD} 2014-12-16 by lixuelong
        /// </summary>
        /// <param name="itemCode"></param>
        /// <returns></returns>
        public FS.FrameWork.Models.NeuObject QueryApplyTypeByItemCode(string itemCode)
        {
            string sql = "Order.OutPatient.Order.QueryApplyTypeByItemCode";
            if (this.Sql.GetCommonSql(sql, ref sql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                sql = string.Format(sql, itemCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                this.WriteErr();
                return null;
            }
            if (this.ExecQuery(sql) == -1) return null;
            FS.FrameWork.Models.NeuObject obj = new FS.FrameWork.Models.NeuObject();
            this.Reader.Read();
            try
            {
                obj.ID = this.Reader[0].ToString();
                obj.Name = this.Reader[1].ToString();
            }
            catch { }
            this.Reader.Close();
            return obj;
        }


        /// <summary>
        /// 查询超量的项目编码
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public Dictionary<string, decimal> GetExceededItem(string cardno)
        {
            string strSql = null;
            Dictionary<string, decimal> exceededItems = new Dictionary<string, decimal>();

            if (this.Sql.GetCommonSql("Nurse.Order.GetExceededItemByCardNo", ref strSql) == -1)
            {
                this.Err = "未找到 Nurse.Order.GetExceededItemByCardNo 语句。";
                return null;
            }

            try
            {
                strSql = string.Format(strSql, cardno);
                if (this.ExecQuery(strSql) == -1)
                {
                    this.Err = "执行查询sql失败";
                    return null;
                }
                ArrayList al = new ArrayList();
                while (this.Reader.Read())
                {

                    string code = this.Reader[0].ToString();
                    decimal num = decimal.Parse(this.Reader[1].ToString());

                    if (exceededItems.ContainsKey(code))
                    {
                        exceededItems[code] = exceededItems[code] + num;
                    }
                    else
                    {
                        exceededItems.Add(code, num);
                    }
                }
                return exceededItems;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }

        }

        public Dictionary<string, decimal> GetRecipExceededItem(string cardno, string seeno, string clinic)
        {
            string strSql = null;
            Dictionary<string, decimal> exceededItems = new Dictionary<string, decimal>();

            exceededItems = GetExceededItem(cardno);

            if (this.Sql.GetCommonSql("Nurse.Order.GetRecipExceededItemByCardNo", ref strSql) == -1)
            {
                this.Err = "未找到 Nurse.Order.GetRecipExceededItemByCardNo 语句。";
                return null;
            }

            if (exceededItems == null || exceededItems.Count == 0)
            {
                return exceededItems;
            }

            try
            {
                strSql = string.Format(strSql, clinic, cardno);

                if (seeno != "-1")
                {
                    string where = string.Format("  and  ord.SEE_NO !='{0}'  ", seeno);
                    strSql += where;
                }


                if (this.ExecQuery(strSql) == -1)
                {
                    this.Err = "执行查询sql失败";
                    return new Dictionary<string, decimal>();
                }

                while (this.Reader.Read())
                {
                    string code = this.Reader[0].ToString();
                    decimal num = decimal.Parse(this.Reader[1].ToString());

                    if (exceededItems.ContainsKey(code))
                    {
                        exceededItems[code] = exceededItems[code] - num;
                    }
                }
                return exceededItems;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return new Dictionary<string, decimal>();
            }
            finally
            {
                if (!this.Reader.IsClosed)
                {
                    this.Reader.Close();
                }
            }


        }
    }
}
