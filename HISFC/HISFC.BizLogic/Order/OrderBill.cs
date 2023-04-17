using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.Order
{
    public class OrderBill : FS.FrameWork.Management.Database
    {
        /// <summary>
        /// ҽ����ӡ������
        /// </summary>
        public OrderBill()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        /// <summary>
        /// ����ҽ����ӡ��Ϣ
        /// </summary>
        /// <param name="orderBill"></param>
        /// <returns></returns>
        public int InsertOrderBill(FS.HISFC.Models.Order.OrderBill orderBill)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.InsertOderBill", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return -1;
            }
            System.Object[] s = {
									orderBill.Order.Patient.ID,
									orderBill.PrintSequence,
									orderBill.Order.ID ,
									orderBill.PageNO ,
									orderBill.LineNO ,
									FS.FrameWork.Function.NConvert.ToInt32(orderBill.Order.OrderType.IsDecompose) ,
									orderBill.PrintFlag ,
									orderBill.Order.Combo.ID ,
									orderBill.PrintDCFlag ,
									orderBill.Oper.ID ,
									orderBill.Oper.OperTime  				 	
								};
            try
            {
                strSql = string.Format(strSql, s);

            }
            catch
            {
                this.Err = "sql��ʽ������";
                return -1;
            }

            return this.ExecNoQuery(strSql);

        }


        /// <summary>
        /// ���ҽ����ӡ��Ϣ
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <param name="prnFlag">��ӡ��־��0δ��ӡ��1�Ѵ�ӡ��</param>
        /// <returns>ҽ����ӡ��Ϣ</returns>
        public ArrayList GetOrderBill(string inpatientNo, string prnFlag)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.GetOderBill", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSql = string.Format(strSql, inpatientNo, prnFlag);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "���ҽ����ӡ��Ϣ" + this.Err;
                return null;
            }
            FS.HISFC.Models.Order.OrderBill orderBill;
            try
            {
                while (this.Reader.Read())
                {
                    try
                    {
                        orderBill = new FS.HISFC.Models.Order.OrderBill();
                        orderBill.Order.Patient.ID = this.Reader[0].ToString();
                        orderBill.PrintSequence = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());
                        orderBill.Order.ID = this.Reader[2].ToString();
                        orderBill.PageNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                        orderBill.LineNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());
                        orderBill.Order.OrderType.ID = this.Reader[5].ToString();
                        orderBill.PrintFlag = this.Reader[6].ToString();
                        orderBill.Order.Combo.ID = this.Reader[7].ToString();
                        orderBill.PrintDCFlag = this.Reader[8].ToString();
                        orderBill.Oper.ID = this.Reader[9].ToString();
                        orderBill.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());
                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ����ӡ��Ϣ����" + ex.Message;
                        this.Reader.Close();
                        return null;
                    }
                    al.Add(orderBill);
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ����ӡ��Ϣ����" + ex.Message;
                this.Reader.Close();
                return null;
            }
            this.Reader.Close();
            return al;
        }

        /// <summary>
        /// ���ҽ����ӡ��Ϣ
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <param name="prnFlag">��ӡ��־��0δ��ӡ��1�Ѵ�ӡ��</param>
        /// <param name="pageNo">ҳ��</param>
        /// <returns>ҽ����ӡ��Ϣ</returns>
        public ArrayList GetOrderBill(string inpatientNo, string prnFlag, int pageNo)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.GetOderBill.pageNo", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSql = string.Format(strSql, inpatientNo, prnFlag, pageNo);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "���ҽ����ӡ��Ϣ" + this.Err;
                return null;
            }
            FS.HISFC.Models.Order.OrderBill orderBill;
            try
            {
                while (this.Reader.Read())
                {
                    try
                    {
                        orderBill = new FS.HISFC.Models.Order.OrderBill();
                        orderBill.Order.Patient.ID = this.Reader[0].ToString();
                        orderBill.PrintSequence = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());
                        orderBill.Order.ID = this.Reader[2].ToString();
                        orderBill.PageNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                        orderBill.LineNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());
                        orderBill.Order.OrderType.ID = this.Reader[5].ToString();
                        orderBill.PrintFlag = this.Reader[6].ToString();
                        orderBill.Order.Combo.ID = this.Reader[7].ToString();
                        orderBill.PrintDCFlag = this.Reader[8].ToString();
                        orderBill.Oper.ID = this.Reader[9].ToString();
                        orderBill.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());
                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ����ӡ��Ϣ����" + ex.Message;
                        this.Reader.Close();
                        return null;
                    }
                    al.Add(orderBill);
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ����ӡ��Ϣ����" + ex.Message;
                this.Reader.Close();
                return null;
            }
            this.Reader.Close();
            return al;
        }


        /// <summary>
        /// ����Ѵ�ӡҽ��
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <returns>��ӡ��Ϣ</returns>
        public ArrayList GetPrnOrderBill(string inpatientNo)
        {
            return this.GetOrderBill(inpatientNo, "1");

        }


        /// <summary>
        /// ���δ��ӡҽ��
        /// </summary>
        /// <param name="inpatientNo">סԺ��ˮ��</param>
        /// <returns>��ӡ��Ϣ</returns>
        public ArrayList GetUnPrnOrderBill(string inpatientNo)
        {
            return this.GetOrderBill(inpatientNo, "0");
        }

        /// <summary>
        /// ��ȡ�����Ѵ�ӡҽ������������,ҳ��,�к�
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="orderType">0��ʱҽ��1����ҽ��</param>
        /// <param name="orderSeq"></param>
        /// <param name="orderPageNO"></param>
        /// <param name="orderLineNO"></param>
        /// <returns></returns>
        public int GetLastOrderBillArg(string inpatientNO, string orderType, out int orderSeq, out int orderPageNO, out int orderLineNO)
        {
            orderSeq = 0;
            orderPageNO = 1;
            orderLineNO = 0;
            //�Ȼ�ȡ������ҳ��,Ȼ���ٻ�ȡ���ҳ���ϵ�����к�
            string strSql1 = "";
            string strSql2 = "";
            if (this.Sql.GetCommonSql("Order.OrderBill.GetLastOrderBillArg.1", ref strSql1) == -1)
            {
                this.Err = "Can not find the Sql Order.OrderBill.GetLastOrderBillArg.1";
                return -1;
            }
            if (this.Sql.GetCommonSql("Order.OrderBill.GetLastOrderBillArg.2", ref strSql2) == -1)
            {
                this.Err = "Can not find the Sql Order.OrderBill.GetLastOrderBillArg.1";
                return -1;
            }
            try
            {
                strSql1 = string.Format(strSql1, inpatientNO, orderType);
                this.ExecQuery(strSql1);
                while (this.Reader.Read())
                {
                    orderSeq = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1]);
                    orderPageNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                }
                //this.Reader.Close();
                if (orderPageNO > 0)
                {
                    strSql2 = string.Format(strSql2, inpatientNO, orderPageNO, orderType);
                    this.ExecQuery(strSql2);
                    while (this.Reader.Read())
                    {
                        orderLineNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                    }
                    this.Reader.Close();
                }
                else
                {
                    orderLineNO = 0;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ����ҽ���Ƿ��ӡ
        /// </summary>
        /// <param name="OrderNo">ҽ����ˮ��</param>
        /// <param name="prnFlag">��ӡ��־��0δ��ӡ��1�Ѵ�ӡ��</param>
        /// <returns></returns>
        public int UpdatePrnFlag(string OrderNo, string prnFlag)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.SetOderBillPrn", ref strSql) == -1)
            {
                this.Err = "���sql������" + this.Sql.Err + this.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, OrderNo, prnFlag);
            }
            catch
            {
                this.Err = "sql����ʽ������" + this.Err;
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }

        /// <summary>
        /// ����ҽ���Ƿ��ӡ
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="prnFlag"></param>
        /// <param name="Seq"></param>
        /// <param name="pageNO"></param>
        /// <param name="lineNO"></param>
        /// <returns></returns>
        public int UpdatePrnFlag(string OrderNo, string prnFlag, int Seq, int pageNO, int lineNO)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.UpdatePrnFlag", ref strSql) == -1)
            {
                this.Err = "���sql������" + this.Sql.Err + this.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, OrderNo, prnFlag, Seq, pageNO, lineNO);
            }
            catch
            {
                this.Err = "sql����ʽ������" + this.Err;
                return -1;
            }
            return this.ExecNoQuery(strSql);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <param name="orderBill"></param>
        /// <returns></returns>
        public int UpdateOderBill(string OrderNo, FS.HISFC.Models.Order.OrderBill orderBill)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.UpdateOderBill", ref strSql) == -1)
            {
                this.Err = "���sql������" + this.Sql.Err + this.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, OrderNo, orderBill.PrintFlag, orderBill.PrintSequence, orderBill.PageNO, orderBill.LineNO);
            }
            catch
            {
                this.Err = "sql����ʽ������" + this.Err;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ҽ������ӡ�кź�ҳ��
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="lineNo"></param>
        /// <param name="pageNo"></param>
        /// <returns></returns>
        public int UpdateLineNoPageNo(string orderID, int lineNo, int pageNo)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.UpdatePrnLinePage", ref strSql) == -1)
            {
                this.Err = "���sql������" + this.Sql.Err + this.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, orderID, pageNo.ToString(), lineNo.ToString());
            }
            catch
            {
                this.Err = "sql����ʽ������" + this.Err;
                return -1;
            }
            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ����ҽ���Ƿ�ֹͣ
        /// </summary>
        /// <param name="OrderNo">ҽ����ˮ��</param>
        /// <param name="stopFlag">ֹͣ��־��0δֹͣ��1��ֹͣ��</param>
        /// <returns></returns>
        public int UpdateStopFlag(string OrderNo, string stopFlag)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.SetOderBillStop", ref strSql) == -1)
            {
                this.Err = "���sql������" + this.Sql.Err + this.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, OrderNo, stopFlag);
            }
            catch
            {
                this.Err = "sql����ʽ������" + this.Err;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        /// <summary>
        /// ��ѯһ��ҽ������ӡ��Ϣ
        /// </summary>
        /// <param name="orderID">ҽ����ˮ��</param>
        /// <returns>OrderBill</returns>
        public FS.HISFC.Models.Order.OrderBill GetOrderBillByOrderID(string orderID)
        {
            ArrayList al = new ArrayList();
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.GetOrderBillByOrderID", ref strSql) == -1)
            {
                this.Err = this.Sql.Err;
                return null;
            }
            try
            {
                strSql = string.Format(strSql, orderID);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return null;
            }
            if (this.ExecQuery(strSql) == -1)
            {
                this.Err = "���ҽ����ӡ��Ϣ" + this.Err;
                return null;
            }
            FS.HISFC.Models.Order.OrderBill orderBill;
            try
            {
                while (this.Reader.Read())
                {
                    try
                    {
                        orderBill = new FS.HISFC.Models.Order.OrderBill();
                        orderBill.Order.Patient.ID = this.Reader[0].ToString();
                        orderBill.PrintSequence = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[1].ToString());
                        orderBill.Order.ID = this.Reader[2].ToString();
                        orderBill.PageNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[3].ToString());
                        orderBill.LineNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[4].ToString());
                        orderBill.Order.OrderType.ID = this.Reader[5].ToString();
                        orderBill.PrintFlag = this.Reader[6].ToString();
                        orderBill.Order.Combo.ID = this.Reader[7].ToString();
                        orderBill.PrintDCFlag = this.Reader[8].ToString();
                        orderBill.Oper.ID = this.Reader[9].ToString();
                        orderBill.Oper.OperTime = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[10].ToString());
                    }
                    catch (Exception ex)
                    {
                        this.Err = "���ҽ����ӡ��Ϣ����" + ex.Message;
                        this.Reader.Close();
                        return null;
                    }
                    al.Add(orderBill);
                }
            }
            catch (Exception ex)
            {
                this.Err = "���ҽ����ӡ��Ϣ����" + ex.Message;
                this.Reader.Close();
                return null;
            }
            this.Reader.Close();
            if (al.Count == 0) return null;

            return al[0] as FS.HISFC.Models.Order.OrderBill;
        }

        /// <summary>
        /// ����ҽ���Ƿ�ֹͣ
        /// </summary>
        /// <param name="OrderNo">ҽ����ˮ��</param>
        /// <param name="stopFlag">ֹͣ��־��0δֹͣ��1��ֹͣ��</param>
        /// <returns></returns>
        public int UpdatePrinStopFlag(string OrderNo)
        {
            string strSql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.SetOderBillPrintStop", ref strSql) == -1)
            {
                this.Err = "���sql������" + this.Sql.Err + this.Err;
                return -1;
            }
            try
            {
                strSql = string.Format(strSql, OrderNo, "1");
            }
            catch
            {
                this.Err = "sql����ʽ������" + this.Err;
                return -1;
            }

            return this.ExecNoQuery(strSql);
        }

        #region ������ҽ������ӡ

        /// <summary>
        /// ��ȡ�����Ѵ�ӡҽ������������,ҳ��,�к�
        /// </summary>
        /// <param name="inpatientNO"></param>
        /// <param name="orderType">0��ʱҽ��1����ҽ��</param>
        /// <param name="orderSeq"></param>
        /// <param name="orderPageNO"></param>
        /// <param name="orderLineNO"></param>
        /// <returns></returns>
        public int GetLastOrderBillArgNew(string inpatientNO, string orderType, out int orderPageNO, out int orderLineNO)
        {
            orderPageNO = 1;
            orderLineNO = 0;
            //�Ȼ�ȡ������ҳ��,Ȼ���ٻ�ȡ���ҳ���ϵ�����к�
            string strSql1 = "";
            string strSql2 = "";
            if (this.Sql.GetCommonSql("Order.OrderPrint.GetLastOrderBillArg.1", ref strSql1) == -1)
            {
                this.Err = "Can not find the Sql Order.OrderPrint.GetLastOrderBillArg.1";
                return -1;
            }
            if (this.Sql.GetCommonSql("Order.OrderPrint.GetLastOrderBillArg.2", ref strSql2) == -1)
            {
                this.Err = "Can not find the Sql Order.OrderPrint.GetLastOrderBillArg.1";
                return -1;
            }
            try
            {
                strSql1 = string.Format(strSql1, inpatientNO, orderType);
                this.ExecQuery(strSql1);
                while (this.Reader.Read())
                {
                    orderPageNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                }
                //this.Reader.Close();
                if (orderPageNO > 0)
                {
                    strSql2 = string.Format(strSql2, inpatientNO, orderPageNO, orderType);
                    this.ExecQuery(strSql2);
                    while (this.Reader.Read())
                    {
                        orderLineNO = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[0]);
                    }
                    this.Reader.Close();
                }
                else
                {
                    orderLineNO = 0;
                }
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return 0;
        }

        /// <summary>
        /// ҽ������ӡ����
        /// </summary>
        /// <param name="lineNO">�к�</param>
        /// <param name="pageNO">ҳ��</param>
        /// <param name="inpatientNO">סԺ��ˮ��</param>
        /// <param name="orderID">ҽ����ˮ�ţ�����=ALL��</param>
        /// <param name="prnFlag">��ӡ���</param>
        /// <returns></returns>
        public int ResetOrderPrint(string lineNO, string pageNO, string inpatientNO, string orderID, string prnFlag)
        {
            string sql = "";
            if (this.Sql.GetCommonSql("Order.OrderPrn.UpdateOrderPrint", ref sql) == -1)
            {
                this.Err = "û���ҵ�Order.OrderPrn.UpdateOrderPrint�ֶ�!";
                return -1;
            }
            sql = string.Format(sql, lineNO, pageNO, inpatientNO, orderID, prnFlag);
            return this.ExecNoQuery(sql);

        }

        #endregion
    }
}
