using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
namespace FS.HISFC.BizLogic.Order.OutPatient
{
    /// <summary>
    /// Order ��ժҪ˵����
    /// ����ҽ��
    /// </summary>
    public class Order : FS.FrameWork.Management.Database
    {
        public Order()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }


        #region ������������ɾ��

        /// <summary>
        /// ����һ��
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
        /// ����
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public int UpdateOrder(FS.HISFC.Models.Order.OutPatient.Order order)
        {
            if (this.DeleteOrder(order.SeeNO, FS.FrameWork.Function.NConvert.ToInt32(order.ID)) < 0)
            {
                return -1;//ɾ�����ɹ�
            }
            return this.InsertOrder(order);
        }


        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="seeNo"></param>
        /// <param name="seqNo"></param>
        /// <returns></returns>
        public int DeleteOrder(string seeNo, int seqNo)
        {
            /*
             * DELETE 
             * FROM met_ord_recipedetail   --��䴦����ϸ��
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

        #region ����ҽ����������add by sunm

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
        /// ����ҽ�������¼
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
        /// ����ҽ��
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

        #region ����µĿ������
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
        /// �����ҽ��������
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

        #region ����ҽ���Ѿ��շ�
        /// <summary>
        /// ����ҽ���Ѿ��շ�
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
        /// ����ҽ���Ѿ��շ�
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
        /// ����ҽ���Ѿ��շ�
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
        /// ����ҽ���Ƿ�����˷�
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
        /// ����ҽ���Ƿ�����˷�
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="chargeOperID"></param>
        /// <param name="refundReason">�˷�ԭ��</param>
        /// <returns></returns>
        public int UpdateOrderCanChargeByOrderID(string orderID, string chargeOperID, string refundReason)
        {
            string sql = "update met_ord_recipedetail  set quit_flag ='1' ,  quit_oper='{1}' ,refund_reason = '{2}'  ,  quit_date = sysdate  where sequence_no  = '{0}' and status='1'";
            //if (this.Sql.GetCommonSql(sql, ref sql) == -1) return -1;
            return this.ExecNoQuery(sql, orderID, chargeOperID,refundReason);
        }
        #endregion

        #region  ����ҽ�����
        /// <summary>
        /// ����ҽ�����
        /// ����clinic_code�Ż���ѯ����{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
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

        #region  ����ҽ��Ƥ�Խ��
        /// <summary>
        /// ����ҽ��Ƥ�Խ��//{26E88889-B2CF-4965-AFD8-6D9BE4519EBF}
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
        /// ����ҽ��Ƥ�Խ��{55BBD9DB-F5C9-4e0a-94E5-9F7FCB121350}
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
        /// ��ѯƤ�Դ�����Ϣ
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
                this.Err = "��ѯ��Ӧ[Order.OutPatient.Order.QueryHyRecord]��sql���ʧ��";
                return null;
            }

            try
            {
                strSql = string.Format(strSql, cardNO, beginDtime, endDtime);
            }
            catch (Exception ex)
            {

                this.Err = "��ʽ������\n" + ex.Message;
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

        // ���ݲ����ţ�������ˮ�ţ���ѯ��Ҫ��Ƥ�Ե���Чҽ��
        /// <summary>
        /// ���ݲ����ţ�������ˮ�ţ���ѯ��Ҫ��Ƥ�Ե���Чҽ��{55BBD9DB-F5C9-4e0a-94E5-9F7FCB121350}
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
        /// ���ݲ����ţ�������ˮ�Ų�ѯ
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
        /// ����������ѯҽ��
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

        #region ��ѯ

        /// <summary>
        /// ��ѯִ��ҽ��--ͨ��������Ų�ѯ
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
        /// ��ѯ���ﴦ��
        /// </summary>
        /// <param name="clinicCode">���￴����ˮ��</param>
        /// <param name="seeNo">�������</param>
        /// <returns></returns>
        public ArrayList QueryOrder(string clinicCode, string seeNo)
        {
            return this.QueryOrderBase("Order.OutPatient.Order.Query.ByClinicCodeSeeNo", clinicCode, seeNo);
        }

        /// <summary>
        /// ��ѯ���ﴦ��
        /// </summary>
        /// <param name="clinicCode">���￴����ˮ��</param>
        /// <returns></returns>
        public ArrayList QueryOrderByClinicCode(string clinicCode)
        {
            return this.QueryOrderBase("Order.OutPatient.Order.Query.ByClinicCode", clinicCode);
        }

        /// <summary>
        /// ��ѯ��ʷ���ﴦ��ִ�д���
        /// //{63E3D61E-ECEE-4fcf-BF83-6597EAD9D81A}
        /// </summary>
        /// <param name="clinicCode">���￴����ˮ��</param>
        /// <param name="seeNo">�������</param>
        /// <returns></returns>
        public ArrayList QueryOrderHistoryExec(string clinicCode, string seeNo)
        {
            return this.QueryOrderBaseExec("Order.OutPatient.Order.Query.ByClinicCodeSeeNo", clinicCode, seeNo);
        }

        /// <summary>
        /// ��������Ų�ѯ���ﴦ��
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
        /// �����Զ���SQL�鴦��
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
        /// ����whereIndex��ѯ���ﴦ��
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
        /// ����whereIndex��ѯ���ﴦ��ִ�д���
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
        /// ����SQL����ѯ���ﴦ��
        /// </summary>
        /// <param name="whereSQL">������SQL��䣬����SQLID</param>
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
        /// ���ݴ����Ų�ѯҽ��
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        public ArrayList QueryOrderByRecipeNO(string clinicCode, string recipeNO)
        {
            return this.QueryOrderBase("Order.OutPatient.Order.Query.Where.4", clinicCode, recipeNO);
        }

        /// <summary>
        /// ���ݴ����Ų�ѯҽ��
        /// </summary>
        /// <param name="recipeNO"></param>
        /// <returns></returns>
        public ArrayList QueryOrderByRecipeNO(string recipeNO)
        {
            return this.QueryOrderBase("Order.OutPatient.Order.Query.ByRecipeNO", recipeNO);
        }

        /// <summary>
        /// ��ѯһ��ҽ��
        /// ����clinic_code�Ż���ѯ����{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
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
        /// ��ѯһ��ҽ��
        /// ����clinic_code�Ż���ѯ����{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
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
        /// ������ѯ���ﴦ��
        /// ����clinic_code�Ż���ѯ����{BE4B33A4-D86A-47da-87EF-1A9923780A5C}
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
        /// ����ҽ����Ų�ѯһ��ҽ��
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
        /// ��ÿ�������б�
        /// </summary>
        /// <param name="cardNo">���￨��</param>
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
        /// ��ÿ�������б�
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
        /// ��ѯ������Ÿ�������
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
        /// ȡ��ҩƷ������ͨ������źͿ����
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
        /// ��ȡ������ͨ������źͿ����
        /// </summary>
        /// <param name="clinicNo"></param>
        /// <param name="seeNo"></param>
        /// <param name="flag">0��ȫ����1��ҩƷ��2��ҩƷ</param>
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
        /// ���ݷ�Ʊ�Ż�ȡ������Ϣ
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
                    //������
                    obj.ID = this.Reader[0].ToString();
                    //ҽ��
                    obj.Name = this.Reader[1].ToString();
                    //����ʱ��
                    obj.Memo = this.Reader[2].ToString();
                    //����
                    obj.SpellCode = this.Reader[3].ToString();
                    //����
                    obj.WBCode = this.Reader[4].ToString();
                    //��Ʊ��
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
        /// ���ݿ��Ų�ѯ��������δִ�е�ҽ����¼
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

        #region ���ﲡ��

        #region �����ķ���
        /// <summary>
        /// ���ݴ����ʵ����»��߲������ﲡ��
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
        /// ����һ������
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
                                              reg.ID, //������ˮ�ţ����滻
                                              reg.PID.CardNO,
                                              reg.Name, //��������
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
        /// ����һ������
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
                    SET    CASEMAIN = '{0}',--����
                           CASENOW = '{1}',--�ֲ�ʷ
                           CASEOLD = '{2}',--����ʷ
                           CASEALLERY = '{3}',--����ʷ
                           ALLERY_FLAG = '{4}',--�Ƿ����
                           INFECT_FLAG = '{5}',--�Ƿ�Ⱦ��
                           CHECKBODY = '{6}',--���� 
                           DIAGNOSE = '{7}',--���
                           MEMO = '{8}',--��ע
                           OPER_CODE = '{9}',--����Ա
                           OPER_DATE = to_date('{10}','YYYY-MM-DD hh24:Mi:SS')--��������
                 *         memo2='{13}'
                    WHERE  CLINIC_CODE = '{11}'--������ˮ�� 
                           and oper_date=to_date('{12}','YYYY-MM-DD hh24:Mi:SS')--����ʱ
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
                                              casehistory.CaseOper.OperTime.ToString(),//���β���ʱ��
                                              reg.ID,
                                              oldOperTime, //��һ�εĲ���ʱ��
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

                                              ); //������ˮ�ţ����滻  
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
        /// ����������ˮ�Ų�ѯһ�����ﲡ��
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
        /// ����������ˮ�źͲ���ʱ���ѯһ�����ﲡ��
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
        /// ��������Ų�ѯ�������в���
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
        /// ͨ�������ȡ����������ʱ��
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

        #region ˽�к���

        /// <summary>
        /// �õ�����ʵ��
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
                obj.ID = this.Reader[0].ToString();//��ˮ��
                obj.Name = this.Reader[1].ToString();//����
                if (!this.Reader.IsDBNull(2))
                    obj.Memo = this.Reader[2].ToString();
                //User01�ǲ���ʱ�� ·־�� 2007-5-9
                obj.User01 = this.Reader[3].ToString();
                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// �õ�����ʵ��
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
                casehistory.CaseMain = this.Reader.GetValue(0).ToString();//����
                casehistory.CaseNow = this.Reader.GetValue(1).ToString();//�ֲ�ʷ
                casehistory.CaseOld = this.Reader.GetValue(2).ToString();//����ʷ
                casehistory.CaseAllery = this.Reader.GetValue(3).ToString();//����ʷ
                casehistory.CheckBody = this.Reader.GetValue(4).ToString();//����
                casehistory.CaseDiag = this.Reader.GetValue(5).ToString();//���
                casehistory.Memo = this.Reader.GetValue(6).ToString();//��ע
                casehistory.Name = this.Reader.GetValue(7).ToString();//����
                casehistory.ID = this.Reader.GetValue(8).ToString();//������ˮ��

                if (!this.Reader.IsDBNull(9))
                    casehistory.IsAllery = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader.GetValue(9).ToString());//�Ƿ����
                if (!this.Reader.IsDBNull(10))
                    casehistory.IsInfect = FS.FrameWork.Function.NConvert.ToBoolean(this.Reader.GetValue(10).ToString());//�Ƿ�Ⱦ��
                //����ʱ��
                casehistory.CaseOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader.GetValue(11));
                casehistory.User01 = this.Reader.GetValue(12).ToString();//����
                casehistory.Memo2 = this.Reader.GetValue(13).ToString();//��ע2  // //  {4694CFAC-9041-496a-93C1-FAE7863E055E}

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

        #region ���ﲡ��ģ��

        /// <summary>
        /// ��ȡ����ģ����ˮ��
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
                this.Err = "ִ�д���";
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
        /// ���ݴ����ʵ����»��߲������ﲡ��ģ��
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
        /// ����һ����¼
        /// </summary>
        /// <param name="casehistory"></param>
        /// <returns></returns>
        public int InsertCaseModule(FS.HISFC.Models.Order.OutPatient.ClinicCaseHistory casehistory)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OutPatient.Case.InsertModule", ref strSql) == -1)
            {
                this.Err = "û���ҵ�Order.OutPatient.Case.InsertModule�ֶ�";
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
        /// ���²���ģ��Type
        /// </summary>
        /// <param name="ModuleType">ģ������</param>
        /// <param name="Module_NO">ģ��ID</param>
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
        /// ����һ����¼
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
        /// ɾ��һ����¼
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
        /// ����ģ����ˮ�Ų�ѯһ����¼
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
        /// �������������ģ��
        /// </summary>
        /// <param name="moduletype"></param>
        /// <param name="Code"></param>
        /// <returns></returns>
        public ArrayList QueryAllCaseModule(string moduletype, string Code)
        {
            string strSql = "";
            if (moduletype == "1")//����
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

        #region ˽�к���
        /// <summary>
        /// �õ�����ģ��ʵ��
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
                casehistory.CaseMain = this.Reader.GetValue(0).ToString();//����
                casehistory.CaseNow = this.Reader.GetValue(1).ToString();//�ֲ�ʷ
                casehistory.CaseOld = this.Reader.GetValue(2).ToString();//����ʷ
                casehistory.CaseAllery = this.Reader.GetValue(3).ToString();//����ʷ
                casehistory.CheckBody = this.Reader.GetValue(4).ToString();//����
                casehistory.CaseDiag = this.Reader.GetValue(5).ToString();//���
                casehistory.Memo = this.Reader.GetValue(6).ToString();//��ע
                casehistory.Name = this.Reader.GetValue(7).ToString();//ģ������
                casehistory.ID = this.Reader.GetValue(8).ToString();//ģ����ˮ��
                casehistory.ModuleType = this.Reader.GetValue(9).ToString();//���
                casehistory.DoctID = this.Reader.GetValue(10).ToString();//ҽʦ����
                casehistory.DeptID = this.Reader.GetValue(11).ToString();//����
                casehistory.User01 = this.Reader.GetValue(12).ToString();//����
                al.Add(casehistory);
            }
            this.Reader.Close();
            return al;
        }
        #endregion

        #endregion

        #region ˽�к���

        /// <summary>
        /// ���sql���������
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        protected string myGetCommonSql(string sql, FS.HISFC.Models.Order.OutPatient.Order order)
        {
            #region sql
            //   0--������� ,1 --��Ŀ��ˮ��,2 --�����,3   --������ ,4    --�Һ�����
            //   5 --�Һſ���,6   --��Ŀ����,7   --��Ŀ����, 8  --���, 9  --1ҩƷ��2��ҩƷ
            //   10   --ϵͳ���,   --��С���ô���,   --����,   --��������,   --����
            //    --��װ����,   --�Ƽ۵�λ,   --�Էѽ��0,   --�Ը����0,   --�������0
            //   --��������,   --����ҩ,   --ҩƷ���ʣ���ҩ����ҩ,   --ÿ������
            //     --ÿ��������λ,   --���ʹ���,   --Ƶ��,   --Ƶ������,   --ʹ�÷���
            //     --�÷�����,   --�÷�Ӣ����д,   --ִ�п��Ҵ���,   --ִ�п�������
            //      --��ҩ��־,   --��Ϻ�,   --1����ҪƤ��/2��ҪƤ�ԣ�δ��/3Ƥ����/4Ƥ����
            //     --Ժ��ע�����,   --��ע,   --����ҽ��,   --����ҽ������,   --ҽ������
            //     --����ʱ��,   --����״̬,1������2�շѣ�3ȷ�ϣ�4����,   --������,   --����ʱ��
            //        --�Ӽ����0��ͨ/1�Ӽ�,   --��������,   --����,   --���뵥��
            //     --0���Ǹ���/1�Ǹ���,   --�Ƿ���Ҫȷ�ϣ�1��Ҫ��0����Ҫ,   --ȷ����
            //        --ȷ�Ͽ���,   --ȷ��ʱ��,   --0δ�շ�/1�շ�,   --�շ�Ա
            //       --�շ�ʱ��,   --������,    --��������ˮ��,     --��ҩҩ����    
            //      --������λ�Ƿ�����С��λ 1 �� 0 ���ǣ�      --ҽ�����ͣ�Ŀǰû�У�
            #endregion

            //if(order.Item.IsPharmacy)//ҩƷ
            if (order.Item.ItemType == FS.HISFC.Models.Base.EnumItemType.Drug)
            {
                FS.HISFC.Models.Pharmacy.Item pItem = order.Item as FS.HISFC.Models.Pharmacy.Item;
                //{9BAE643C-57BF-4dc5-889E-6B5F6B3E1E38} ���ڽ���������뵥��apply_no�ֶθ�order.ApplyNo20100505 yangw
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
                                        //order.Patient.Pact.ID,         //{5C7887F1-A4D5-4a66-A814-18D45367443E} �����޴��ֶ�
                                        //order.Patient.Pact.PayKind.ID, //{5C7887F1-A4D5-4a66-A814-18D45367443E} �����޴��ֶ�
                                        order.QuitFlag.ToString(),    //{5C7887F1-A4D5-4a66-A814-18D45367443E} �����˷ѵı�ʶ
                                        order.QuitOper.ID,            //{5C7887F1-A4D5-4a66-A814-18D45367443E} ������
                                        order.QuitOper.OperTime       //{5C7887F1-A4D5-4a66-A814-18D45367443E} ����ʱ��
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
            else//��ҩƷ
            {
                FS.HISFC.Models.Fee.Item.Undrug pItem = order.Item as FS.HISFC.Models.Fee.Item.Undrug;
                //{9BAE643C-57BF-4dc5-889E-6B5F6B3E1E38} ���ڽ���������뵥��apply_no�ֶθ�order.ApplyNo 20100505 yangw
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
                                        //order.Patient.Pact.ID,         //{5C7887F1-A4D5-4a66-A814-18D45367443E} �����޴��ֶ�
                                        //order.Patient.Pact.PayKind.ID, //{5C7887F1-A4D5-4a66-A814-18D45367443E} �����޴��ֶ�
                                        order.QuitFlag.ToString(),    //{5C7887F1-A4D5-4a66-A814-18D45367443E} �����˷ѵı�ʶ
                                        order.QuitOper.ID,            //{5C7887F1-A4D5-4a66-A814-18D45367443E} ������
                                        order.QuitOper.OperTime       //{5C7887F1-A4D5-4a66-A814-18D45367443E} ����ʱ��
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
        /// ��ò�ѯsql���
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int myGetSelectSql(ref string sql)
        {
            return this.Sql.GetCommonSql("Order.OutPatient.Order.Query.Select", ref sql);
        }

        /// <summary>
        /// ��ò�ѯsql���
        /// </summary>{B039D312-F279-446b-974F-1201C4F32B64}
        /// <param name="sql"></param>
        /// <returns></returns>
        protected int myGetExecuteSelectSql(ref string sql)
        {
            return this.Sql.GetCommonSql("Order.OutPatient.Order.Query.Select.execule", ref sql);
        }

        /// <summary>
        /// ���ִ��ҽ����Ϣ
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
                    order.SequenceNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());//��Ŀ��ˮ��
                    order.ID = this.Reader[1].ToString();//��Ŀ��ˮ��
                    order.Patient.ID = this.Reader[2].ToString();//�����
                    order.Patient.PID.CardNO = this.Reader[3].ToString();//��������
                    order.RegTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);//�Һ�����
                    order.ReciptDept.ID = this.Reader[5].ToString();//�Һſ��� ����
                    if (this.Reader[9].ToString() == "1")//ҩƷ
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

                        //{6DBBDC62-2303-4d97-85EF-8BA2A622117A} ������� xuc
                        item.SplitType = this.Reader[61].ToString();

                        order.Item = item;

                    }
                    else if (this.Reader[9].ToString() == "2")//��ҩƷ
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
                        this.Err = "��ȡmet_ord_recipedetail������ҩƷ��ҩƷ����drug_flag=" + this.Reader[9].ToString();
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
                    order.MinunitFlag = this.Reader[60].ToString();//��С��λ��־
                    order.UseDays = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[62]);//{08024C29-12FE-4629-B982-C50AE9034B82}
                    order.SubCombNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[63].ToString());//������Ϻţ����飩
                    order.ExtendFlag1 = this.Reader[64].ToString();//��ƿ��Ϣ
                    order.ReciptSequence = this.Reader[65].ToString();//�շ�����
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

                    //{5C7887F1-A4D5-4a66-A814-18D45367443E} �����޴��ֶ�
                    //if (this.Reader.FieldCount > 71)
                    //{
                    //    order.Patient.Pact.ID = Reader[71].ToString();
                    //} 
                    //if (this.Reader.FieldCount > 72)
                    //{
                    //    order.Patient.Pact.PayKind.ID = Reader[72].ToString();
                    //}
                    if (this.Reader.FieldCount > 71) //�����˷ѱ�ʶ
                    {
                        order.QuitFlag = FS.FrameWork.Function.NConvert.ToInt32(Reader[71]);
                    }
                    if (this.Reader.FieldCount > 72) //������
                    {
                        order.QuitOper.ID = Reader[72].ToString();
                    }
                    if (this.Reader.FieldCount > 73) //����ʱ��
                    {
                        order.QuitOper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(Reader[73]);
                    }
                    if (this.Reader.FieldCount > 74) //������ִ��״̬// {A046F9F6-0367-4ab5-9966-9C3F29C38C41}
                    {
                        order.User01 = Reader[74].ToString();
                    }
                    if (this.Reader.FieldCount > 75) //�ն�ȷ��ִ��״̬// {95F53D43-61DC-47a1-B090-EEC34C84A09C}
                    {

                        string confirm = Reader[75].ToString();
                        if (confirm != null && confirm != "")
                        {
                            order.User01 += "��ִ��" + confirm + "��";
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
        /// ����÷����÷������ĸ���(�ɽ���fin_opb_inject)
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
        /// ���÷����÷������ĸ���(�½���met_com_subtblitem)
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

        #region �����´�������
        ///add by liuww 2011-3-8


        /// <summary>
        /// ���տ��Ų鴦����ʷ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="recipeType"></param>
        /// <returns></returns>
        public ArrayList QueryPatientRecipeByCardAndType(string cardNo, string recipeType, DateTime dtBegin, DateTime dtEnd)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Order.QueryPatientRecipeByCardAndType", ref strSql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ Order.Order.QueryPatientRecipeByCardAndType��SQL���";
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
        /// ���ݴ������,�ͻ�����ˮ��,��ѯ������Ϣ
        /// </summary>
        /// <param name="clinicNO"></param>
        /// <param name="recipeType"></param>
        /// <returns></returns>
        public ArrayList QueryPatientRecipeByType(string clinicNO, string recipeType)
        {
            string strSql = "";

            if (this.Sql.GetCommonSql("Order.Order.QueryPatientRecipeByType", ref strSql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ Order.Order.QueryPatientRecipeByType��SQL���";
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
        /// ���½�����Ϣ����ߡ����ء�Ѫѹ
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
        /// ��ȡ������Ϣ����ߡ����ء�Ѫѹ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="SBP">Ѫѹ������ѹ</param>
        /// <param name="DBP">Ѫѹ������ѹ</param>
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
                    Err = "δ�ҵ��Һ���Ϣ��";
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
        /// ����������ˮ�Ż�ȡ������Ϣ����ߡ����ء�Ѫѹ
        /// </summary>
        /// <param name="clinicCode"></param>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <param name="SBP">Ѫѹ������ѹ</param>
        /// <param name="DBP">Ѫѹ������ѹ</param>
        /// <returns></returns>
        public int GetHealthInfo(string clinicCode, ref string height, ref string weight, ref string SBP, ref string DBP, ref string tem, ref string bloodGlu)
        {
            return this.GetHealthInfo("Order.OutPatient.HealthInfo.GetByClinicCode", ref height, ref weight, ref SBP, ref DBP, ref tem, ref bloodGlu, clinicCode);
        }

        /// <summary>
        /// �������￨�Ż�ȡ���һ�ν�����Ϣ����ߡ����ء�Ѫѹ
        /// </summary>
        /// <param name="cardNo"></param>
        /// <param name="days">��ѯʱ���</param>
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
        /// {FD42E841-E918-4AA7-AC8E-76E2DB2ACEBF}��ȡһ����������Ѫѹ
        /// </summary>
        /// <param name="phone">�绰</param>
        /// <param name="date">����</param>
        /// <param name="IDCard">���֤</param>
        /// <param name="height">���</param>
        /// <param name="weight">����</param>
        /// <param name="bmi">bmi</param>
        /// <param name="higp">����ѹ</param>
        /// <param name="lowp">����ѹ</param>
        /// <param name="pulse">����</param>
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
        /// {FD42E841-E918-4AA7-AC8E-76E2DB2ACEBF}��ȡһ���Ѫҩ ���� ��� 
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
        /// ���ݿ��Ų�ѯʱ����ڵĴ�����
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
                    errText = "ִ�в�ѯsqlʧ��";
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
        /// ����Ƥ����Ϣ
        /// </summary>
        /// <param name="i"></param>
        /// <returns>1 [����] 2 [��Ƥ��] 3 [+] 4 [-]</returns>
        public string TransHypotest(FS.HISFC.Models.Order.EnumHypoTest HypotestCode)
        {
            //return FS.FrameWork.Public.EnumHelper.Current.GetName(HypotestCode);

            switch ((int)HypotestCode)
            {
                case 0:
                    //return "����ҪƤ��";
                    return "";
                case 1:
                    return "[����]";
                case 2:
                    return "[��Ƥ��]";
                case 3:
                    return "[+]";
                case 4:
                    return "[-]";
                default:
                    return "[����]";
            }
        }

        /// <summary>
        /// ��ҩ���봦��
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
        /// ������Ŀ��Ų�ѯ���뵥����{D793A341-AD35-4685-8817-5614217969AD} 2014-12-16 by lixuelong
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
        /// ��ѯ��������Ŀ����
        /// </summary>
        /// <param name="cardno"></param>
        /// <returns></returns>
        public Dictionary<string, decimal> GetExceededItem(string cardno)
        {
            string strSql = null;
            Dictionary<string, decimal> exceededItems = new Dictionary<string, decimal>();

            if (this.Sql.GetCommonSql("Nurse.Order.GetExceededItemByCardNo", ref strSql) == -1)
            {
                this.Err = "δ�ҵ� Nurse.Order.GetExceededItemByCardNo ��䡣";
                return null;
            }

            try
            {
                strSql = string.Format(strSql, cardno);
                if (this.ExecQuery(strSql) == -1)
                {
                    this.Err = "ִ�в�ѯsqlʧ��";
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
                this.Err = "δ�ҵ� Nurse.Order.GetRecipExceededItemByCardNo ��䡣";
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
                    this.Err = "ִ�в�ѯsqlʧ��";
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
