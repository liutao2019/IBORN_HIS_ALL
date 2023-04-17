using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace FS.HISFC.BizLogic.Order
{
    /// <summary>
    /// [����������סԺҽ����Ϣ��չҵ����]
    /// [�� �� �ߣ�]
    /// [����ʱ�䣺]
    /// </summary>
    public class OrderExtend : FS.FrameWork.Management.Database
    {
        public OrderExtend()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        #region �ڲ�˽�з���

        /// <summary>
        /// ����SQL��ѯҽ����չ��Ϣ
        /// </summary>
        /// <param name="wheSql">Whe�Ӿ�</param>
        /// <returns>�ɹ�����ҽ����չ��Ϣʵ�� ʧ�ܷ���null</returns>
        private ArrayList QueryOrderExtends(string wheSql, params string[] args)
        {
            string strSql = "";
            string selSql = "";
            //ȡSELECT�Ӿ�
            selSql = this.GetCommonSqlForSelectAllOrderExtends();

            //ȡWHERE�Ӿ�
            try
            {
                if (!string.IsNullOrEmpty(wheSql))
                {
                    if (this.Sql.GetCommonSql(wheSql, ref wheSql) == -1)
                    {
                        this.Err = "û���ҵ�" + wheSql + "�ֶ�!";
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

            //ִ��Sql��� 
            try
            {
                this.ExecQuery(strSql);

                while (this.Reader.Read())
                {
                    FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtend = new FS.HISFC.Models.Order.Inpatient.OrderExtend();
                    orderExtend.InPatientNo = this.Reader[0].ToString();  //סԺ��ˮ��
                    orderExtend.MoOrder = this.Reader[1].ToString();   //ҽ����ˮ��
                    orderExtend.Indications = this.Reader[2].ToString();//��Ӧ֢��Ϣ
                    orderExtend.Extend1 = this.Reader[3].ToString(); //��ע1
                    orderExtend.Extend2 = this.Reader[4].ToString();                                            //��ע2 
                    orderExtend.Extend3 = this.Reader[5].ToString();                                       //��ע3 
                    orderExtend.Extend4 = this.Reader[6].ToString();                                      //��ע4 
                    orderExtend.Extend5 = this.Reader[7].ToString();                                         //��ע5 
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
        /// ���µ������
        /// </summary>
        /// <param name="sqlIndex">SQL�������</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�: >= 1 ʧ�� -1 û�и��µ����� 0</returns>
        private int UpdateSingleTable(string sqlIndex, params string[] args)
        {
            string sql = string.Empty;//Update���

            //���Where���
            if (this.Sql.GetCommonSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return -1;
            }
            sql = string.Format(sql, args);
            return this.ExecNoQuery(sql);
        }

        /// <summary>
        /// ���ҽ����չ��Ϣ�ַ�������
        /// </summary>
        /// <param name="prepay">ҽ����չ��Ϣʵ��</param>
        /// <returns>�ɹ�: ҽ����չ��Ϣ�ַ������� ʧ��: null</returns>
        private string[] GetOrderExtendParams(FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtend)
        {
            string[] args ={
                               //סԺ��ˮ��
                               orderExtend.InPatientNo,
                               //ҽ����ˮ��
                               orderExtend.MoOrder,
							   //��Ӧ֢
							   orderExtend.Indications,
							   //��ע1
							   orderExtend.Extend1,
                                //��ע2
							   orderExtend.Extend2,
                                //��ע3
							   orderExtend.Extend3,
                                //��ע4
							   orderExtend.Extend4,
				               //��ע5
							   orderExtend.Extend5,
                                //��ע6
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
        /// ��ȡ����met_ipm_order_extend��ȫ�����ݵ�sql
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

        #region ��ɾ��

        /// <summary>
        /// ����ҽ����չ��Ϣ
        /// </summary>
        /// <param name="prepay">ҽ����չ��Ϣʵ��</param>
        /// <returns>�ɹ�: 1 ʧ�� -1 û�в������� 0</returns>
        public int InsertOrderExtend(FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtend)
        {
            string[] parms = new string[9];
            parms = this.GetOrderExtendParams(orderExtend);
            return this.UpdateSingleTable("Order.Extend.InsertOrderExtend", parms);
        }

        /// <summary>
        /// ����ҽ����չ��Ϣ
        /// </summary>
        /// <param name="prepay">ҽ����չ��Ϣʵ��</param>
        /// <returns></returns>
        public int UpdateOrderExtend(FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtend)
        {
            return this.UpdateSingleTable("Order.Extend.UpdateOrderExtend", this.GetOrderExtendParams(orderExtend));
        }

        /// <summary>
        /// ɾ��ҽ����չ��Ϣ
        /// </summary>
        /// <param name="prepay">ҽ����չ��Ϣʵ��</param>
        /// <returns></returns>
        public int DeleteOrderExtend(FS.HISFC.Models.Order.Inpatient.OrderExtend orderExtend)
        {
            return this.UpdateSingleTable("Order.Extend.DeleteOrderExtend", this.GetOrderExtendParams(orderExtend));
        }

        #endregion

        #region ��ѯ����

        /// <summary>
        /// ����סԺ��ˮ�š�ҽ����ˮ��ȡҽ����չ��Ϣ
        /// </summary>
        /// <param name="prepay">ҽ����չ��Ϣʵ��</param>
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
