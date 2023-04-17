using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Neusoft.HISFC.Models.Fee.Inpatient;
using Neusoft.HISFC.Models.Fee;
using Neusoft.HISFC.Models.RADT;
using Neusoft.FrameWork.Function;
using Neusoft.HISFC.Models.Base;
using System.Data;

namespace Neusoft.WinForms.Report.InpatientFee.Functions
{
     public class DayReport : Neusoft.FrameWork.Management.Database
    {
        #region "˽�з���"

        #region "������²���"
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
            if (this.Sql.GetSql(sqlIndex, ref sql) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + sqlIndex + "��SQL���";

                return -1;
            }

            return this.ExecNoQuery(sql, args);
        }

        /// <summary>
        /// ����ս������ַ�������
        /// </summary>
        /// <param name="dayReport">�ս���Ϣʵ��</param>
        /// <returns>�ɹ�: �ս������ַ������� ʧ��: null</returns>
        private string[] GetDayReportParams(Neusoft.HISFC.Models.Fee.DayReport dayReport)
        {

            string[] args ={

               //ͳ�����
                dayReport.StatNO,

               //��ʼʱ��

                dayReport.BeginDate.ToString(),
               //��������
                dayReport.EndDate.ToString(),

               //����Ա����

               dayReport.Oper.ID,
               //ͳ��ʱ��
               dayReport.Oper.OperTime.ToString(),

               //��ȡԤ������

                dayReport.PrepayCost.ToString(),
               //����֧Ʊ���

                dayReport.DebitCheckCost.ToString(),
               //�������п����

                dayReport.DebitBankCost.ToString(),
               //����Ԥ������

                dayReport.BalancePrepayCost.ToString(),
               //�跽֧Ʊ���

                dayReport.LenderCheckCost.ToString(),
               //�跽���п����

                dayReport.LenderBankCost.ToString(),
               //���Ѽ��ʽ��
                dayReport.BursaryPubCost.ToString(),
               //��ҽ���ʻ�֧�����

                dayReport.CityMedicarePayCost.ToString(),
               //��ҽ��ͳ����

                dayReport.CityMedicarePubCost.ToString(),
               //ʡҽ���ʻ�֧�����

                dayReport.ProvinceMedicarePayCost.ToString(),
               //ʡҽ��ͳ����
                 
                dayReport.ProvinceMedicarePubCost.ToString(),
               //�����Ͻɽ�
                dayReport.TurnInCash.ToString(),
               //Ԥ����Ʊ����

                dayReport.PrepayInvCount.ToString(),
               //���㷢Ʊ����
                dayReport.BalanceInvCount.ToString(),
               //����Ԥ����Ʊ����

                dayReport.PrepayWasteInvNO,
               //���Ͻ��㷢Ʊ����
                dayReport.BalanceWasteInvNO,
               //����Ԥ����Ʊ����

                dayReport.PrepayWasteInvCount.ToString(),
               //���Ͻ��㷢Ʊ����
                dayReport.BalanceWasteInvCount.ToString(),
               //Ԥ����Ʊ����

                dayReport.PrepayInvZone,
               //���㷢Ʊ����
                dayReport.BalanceInvZone,
               //�շ�Ա����

                dayReport.Oper.Dept.ID,
                //�����ܽ��

                dayReport.BalanceCost.ToString()
						   };

            return args;
        }

        #endregion


        /// <summary>
        /// ��ȡ����fin_ipb_dayReport��ȫ�����ݵ�sql
        /// </summary>
        /// <returns>�ɹ�: ����SQL��� ʧ�� null</returns>
        private string GetSqlForSelectAllDayReport()
        {
            string strSql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetDayReportAllInfo", ref strSql) == -1)
            {
                this.Err = "�Ҳ���Sql���Fee.FeeReport.GetDayReortInfo";
                return null;
            }
            return strSql;
        }

        /// <summary>
        /// ����SQL��ѯ�ս���Ϣ
        /// </summary>
        /// <param name="sql">SQL���</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�:����סԺ�ս�ʵ�弯�� ʧ�� null</returns>
        private ArrayList QueryDayReportsBySql(string sql, params string[] args)
        {
            if (this.ExecQuery(sql, args) == -1)
            {
                return null;
            }

            ArrayList alDayReport = new ArrayList();//�սἯ��
            Class.DayReport dayReport = null;//�ս�ʵ��

            try
            {
                //ѭ����ȡ����
                while (this.Reader.Read())
                {
                    dayReport = new Report.InpatientFee.Class.DayReport();

                    //ͳ�����
                    dayReport.StatNO = this.Reader[0].ToString();

                    //��ʼʱ��

                    dayReport.BeginDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[1].ToString());
                    //��������
                    dayReport.EndDate = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[2].ToString());

                    //����Ա����

                    dayReport.Oper.ID = this.Reader[3].ToString();
                    //ͳ��ʱ��
                    dayReport.Oper.OperTime = Neusoft.FrameWork.Function.NConvert.ToDateTime(this.Reader[4].ToString());

                    //��ȡԤ������

                    dayReport.PrepayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[5].ToString());
                    //�跽֧Ʊ���

                    dayReport.DebitCheckCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[6].ToString());
                    //�跽���п����

                    dayReport.DebitBankCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[7].ToString());
                    //����Ԥ������

                    dayReport.BalancePrepayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[8].ToString());
                    //����֧Ʊ���

                    dayReport.LenderCheckCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[9].ToString());
                    //�������п����

                    dayReport.LenderBankCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[10].ToString());
                    //���Ѽ��ʽ��
                    dayReport.BursaryPubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[11].ToString());
                    //��ҽ���ʻ�֧�����

                    dayReport.CityMedicarePayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[12].ToString());
                    //��ҽ��ͳ����

                    dayReport.CityMedicarePubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[13].ToString());
                    //ʡҽ���ʻ�֧�����

                    dayReport.ProvinceMedicarePayCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[14].ToString());
                    //ʡҽ��ͳ����

                    dayReport.ProvinceMedicarePubCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[15].ToString());
                    //�����Ͻɽ�
                    dayReport.TurnInCash = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[16].ToString());
                    //Ԥ����Ʊ����

                    dayReport.PrepayInvCount = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[17].ToString());
                    //���㷢Ʊ����
                    dayReport.BalanceInvCount = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[18].ToString());
                    //����Ԥ����Ʊ����

                    dayReport.PrepayWasteInvNO = this.Reader[19].ToString();
                    //���Ͻ��㷢Ʊ����
                    dayReport.BalanceWasteInvNO = this.Reader[20].ToString();
                    //����Ԥ����Ʊ����

                    dayReport.PrepayWasteInvCount = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[21].ToString());
                    //���Ͻ��㷢Ʊ����
                    dayReport.BalanceWasteInvCount = Neusoft.FrameWork.Function.NConvert.ToInt32(this.Reader[22].ToString());
                    //Ԥ����Ʊ����

                    dayReport.PrepayInvZone = this.Reader[23].ToString();
                    //���㷢Ʊ����
                    dayReport.BalanceInvZone = this.Reader[24].ToString();
                    //�շ�Ա����

                    dayReport.Oper.Dept.ID = this.Reader[25].ToString();
                    dayReport.BalanceCost = Neusoft.FrameWork.Function.NConvert.ToDecimal(this.Reader[26].ToString());

                    alDayReport.Add(dayReport);
                }//ѭ������

                this.Reader.Close();

                return alDayReport;
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
        }
        
        /// <summary>
        /// ͨ��Where������ѯ�ս���Ϣ
        /// </summary>
        /// <param name="whereIndex">Where����</param>
        /// <param name="args">����</param>
        /// <returns>�ɹ�:�����ս�ʵ�弯�� ʧ�� null</returns>
        private ArrayList QueryDayReports(string whereIndex, params string[] args)
        {
            string sql = string.Empty;//SELECT���
            string where = string.Empty;//WHERE���

            //���Where���
            if (this.Sql.GetSql(whereIndex, ref where) == -1)
            {
                this.Err = "û���ҵ�����Ϊ:" + whereIndex + "��SQL���";

                return null;
            }

            sql = this.GetSqlForSelectAllDayReport();

            return this.QueryDayReportsBySql(sql + " " + where, args);
        }

        #endregion

        #region "���з���"

        /// <summary>
        /// ���ս�ͳ�����
        /// </summary>
        /// <returns></returns>
        public string GetNewDayReportID()
        {
            string sql = "";
            if (this.Sql.GetSql("Fee.FeeReport.DayReport.GetID", ref sql) == -1) return null;
            string strReturn = this.ExecSqlReturnOne(sql);
            if (strReturn == "-1" || strReturn == "") return null;
            return strReturn;
        }

        /// <summary>
        /// �����ս���Ϣ
        /// </summary>
        /// <param name="dayReport">�ս���Ϣʵ��</param>
        /// <returns>�ɹ�: 1 ʧ�� -1 û�в������� 0</returns>
        public int InsertDayReport(Neusoft.HISFC.Models.Fee.DayReport dayReport)
        {
            return this.UpdateSingleTable("Fee.InpatientDayReport.InsertDayReport", this.GetDayReportParams(dayReport));
        }

        /// <summary>
        /// ��ȡ����Աʱ�����ʵ���ձ�ʵ��
        /// </summary>
        /// <param name="operID">����Ա����</param>
        /// <param name="dtBegin">��ʼʱ��</param>
        /// <param name="dtEnd">����ʱ��</param>
        /// <returns></returns>
        public ArrayList GetDayReportInfosForOper(string operID, DateTime dtBegin, DateTime dtEnd)
        {
            string[] args = 
                {
                    operID,
                    dtBegin.ToString(),
                    dtEnd.ToString()
                };
            return this.QueryDayReports("Fee.InpatientDayReport.GetDayReportInfosForOper", args);

        }
        /// <summary>
        /// ��ȡ����Ա���һ���ս���Ϣ
        /// </summary>
        /// <param name="operID">����ԱID</param>
        /// <returns>�ɹ� �����ս���Ϣʵ�� ʧ�� null</returns>
        public Class.DayReport GetOperLastDayReport(string operID)
        {
            ArrayList alDayReport = new ArrayList();

            alDayReport = this.QueryDayReports("Fee.InpatientDayReport.GetOperLastDayReport", operID);

            if (alDayReport == null || alDayReport.Count == 0)
            {
                return null;
            }
            return (Class.DayReport)alDayReport[0];
        }

        #region ��ȡ�ս������
        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա��ȡԤ�����ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetPrepayCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string prepayCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetPrepayCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����ս���ȡԤ���������!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            prepayCost = this.ExecSqlReturnOne(sql);

            return prepayCost;
        }


        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա����Ԥ�����ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetBalancedPrepayCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string prepayCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetBalancedPrepayCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����ս����Ԥ�����ܽ�����!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            prepayCost = this.ExecSqlReturnOne(sql);

            return prepayCost;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���ԱԤ�����ֽ��ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetPrepayCashCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string prepayCashCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetPrepayCashCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����ս�Ԥ�����ֽ������!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            prepayCashCost = this.ExecSqlReturnOne(sql);

            return prepayCashCost;
        }


        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���ԱԤ����֧Ʊ�ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetPrepayCheckCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string prepayCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetPrepayCheckCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����ս������!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            prepayCost = this.ExecSqlReturnOne(sql);

            return prepayCost;
        }


        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���ԱԤ�������п��ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetPrepayBankCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string prepayBankCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetPrepayBankCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����ս����п�������!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            prepayBankCost = this.ExecSqlReturnOne(sql);

            return prepayBankCost;
        }


        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա��������ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetBalancedCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string balanceCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetBalancedCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����ս�����ܽ�����!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            balanceCost = this.ExecSqlReturnOne(sql);

            return balanceCost;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա���ս���֧Ʊ��������ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetSupplyCheckCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string supplyCheckCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetSupplyCheckCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����սᲹ�ս���֧Ʊ�����ܽ�����!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            supplyCheckCost = this.ExecSqlReturnOne(sql);

            return supplyCheckCost;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա��������֧Ʊ��������ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetReturnCheckCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string returnCheckCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetReturnCheckCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����ս᷵������֧Ʊ�����ܽ�����!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            returnCheckCost = this.ExecSqlReturnOne(sql);

            return returnCheckCost;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա�����ֽ��ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetSupplyCashCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string supplyCashCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetSupplyCashCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����սᲹ�ս����ֽ��ܽ�����!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            supplyCashCost = this.ExecSqlReturnOne(sql);

            return supplyCashCost;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա�����ֽ��ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetReturnCashCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string returnCashCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetReturnCashCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����ս᷵���ֽ��ܽ�����!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            returnCashCost = this.ExecSqlReturnOne(sql);

            return returnCashCost;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա����һ��ͨ�ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetSupplyBankCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string supplyBankCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetSupplyBankCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����սᲹ�ս���һ��ͨ�����ܽ�����!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            supplyBankCost = this.ExecSqlReturnOne(sql);
            return supplyBankCost;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա�������п��ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetReturnBankCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string returnBankCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetReturnBankCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "����ս᷵�����п��ܽ�����!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            returnBankCost = this.ExecSqlReturnOne(sql);

            return returnBankCost;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա������ù��Ѽ��ʽ��

        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetBursaryCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string bursaryPubCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetBursaryCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "��ȡ����Ա������ù��Ѽ��ʽ�����!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            bursaryPubCost = this.ExecSqlReturnOne(sql);

            return bursaryPubCost;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա���������ҽ���ʻ����
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetCPayCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string payCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetCPayCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "��ȡ����Ա���������ҽ���ʻ�������!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            payCost = this.ExecSqlReturnOne(sql);

            return payCost;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա���������ҽ��ͳ����
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա</param>
        /// <returns></returns>
        public string GetCPubCostByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string pubCost = "";
            string sql = "";

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetCPubCostByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "��ȡ����Ա���������ҽ��ͳ�������!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            pubCost = this.ExecSqlReturnOne(sql);



            return pubCost;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���ԱԤ������Ч����
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա����</param>
        /// <returns></returns>
        public string GetValidPrepayInvoiceQtyByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string qty = "";
            string sql = "";
            if (this.Sql.GetSql("Fee.InpatientDayReport.GetValidPrepayInvoiceQtyByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "��ȡ����ԱԤ������Ч��������!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            qty = this.ExecSqlReturnOne(sql);


            return qty;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���ԱԤ������������
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա����</param>
        /// <returns></returns>
        public string GetWastePrepayInvoiceQtyByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string qty = "";
            string sql = "";
            if (this.Sql.GetSql("Fee.InpatientDayReport.GetWastePrepayInvoiceQtyByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "��ȡ����ԱԤ������Ч��������!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            qty = this.ExecSqlReturnOne(sql);


            return qty;
        }
        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���ԱԤ����Ʊ������ 
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա����</param>
        /// <returns>����object ID��С�� name����</returns>
        public Neusoft.FrameWork.Models.NeuObject GetPrepayInvoiceZoneByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string sql = string.Empty;
            Neusoft.FrameWork.Models.NeuObject prepayInvoiceZone = new Neusoft.FrameWork.Models.NeuObject();

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetPrepayInvoiceZoneByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "��ȡ����ԱԤ������Ч��������!";
                return null;
            }
            sql = string.Format(sql, beginDate, endDate, operID);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            while (this.Reader.Read())
            {
                prepayInvoiceZone.ID = this.Reader[0].ToString();
                prepayInvoiceZone.Name = this.Reader[1].ToString();
            }
            this.Reader.Close();

            return prepayInvoiceZone;


        }

        #region Ԥ����Ʊ������ luoff
         /// <summary>
         /// ��ȡԤ����Ʊ������
         /// </summary>
         /// <param name="beginTime">��ʼʱ��</param>
         /// <param name="endTime">����ʱ��</param>
         /// <param name="operID">����Ա����</param>
         /// <param name="dsResult">�������ݼ�</param>
         /// <returns>1���ɹ���-1��ʧ��</returns>
         public int GetPrepayInvoiceZoneNew(DateTime beginTime, DateTime endTime, string operID, ref DataSet dsResult)
         {
             string SQL = string.Empty;
             if (this.Sql.GetSql("Fee.InpatientDayReport.GetPrepayInvoiceZoneNew", ref SQL) == -1)
             {
                 this.Err = "����sql���ʧ�ܣ�";
                 return -1;
             }
             try
             {
                 SQL = string.Format(SQL, beginTime.ToString(), endTime.ToString(), operID);
             }
             catch (Exception ex)
             {
                 this.Err = ex.Message;
                 return -1;
             }
             if (this.ExecQuery(SQL, ref dsResult) == -1)
             {
                 this.Err = "ִ�в�ѯ������";
                 return -1;
             }
             return 1;
         }

        #endregion

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���ԱԤ����Ʊ������Ʊ��
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա����</param>
        /// <returns>����Ʊ�����飬ʧ�ܷ���null</returns>
        public ArrayList QueryWastePrepayInvNOByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string sql = string.Empty;
            ArrayList alPrepayInv = new ArrayList();
            Neusoft.HISFC.Models.Fee.Inpatient.Prepay prepay;

            if (this.Sql.GetSql("Fee.InpatientDayReport.QueryWastePrepayInvNOByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "��ȡ����ԱԤ����Ʊ������Ʊ�ų���!";
                return null;
            }
            sql = string.Format(sql, beginDate, endDate, operID);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            while (this.Reader.Read())
            {
                prepay = new Prepay();
                prepay.RecipeNO = this.Reader[0].ToString();
                alPrepayInv.Add(prepay);
            }
            this.Reader.Close();

            return alPrepayInv;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա������Ч����

        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա����</param>
        /// <returns></returns>
        public string GetValidBalanceInvoiceQtyByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string qty = "";
            string sql = "";
            if (this.Sql.GetSql("Fee.InpatientDayReport.GetValidBalanceInvoiceQtyByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "��ȡ����Ա������Ч��������!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            qty = this.ExecSqlReturnOne(sql);


            return qty;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա������������

        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա����</param>
        /// <returns></returns>
        public string GetWasteBalanceInvoiceQtyByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string qty = "";
            string sql = "";
            if (this.Sql.GetSql("Fee.InpatientDayReport.GetWasteBalanceInvoiceQtyByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "��ȡ����Ա����������������!";
                return "";
            }
            sql = string.Format(sql, beginDate, endDate, operID);
            qty = this.ExecSqlReturnOne(sql);


            return qty;
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա����Ʊ������ 
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա����</param>
        /// <returns>����object ID��С�� name����</returns>
        public Neusoft.FrameWork.Models.NeuObject GetBalanceInvoiceZoneByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string sql = string.Empty;
            Neusoft.FrameWork.Models.NeuObject BalanceInvoiceZone = new Neusoft.FrameWork.Models.NeuObject();

            if (this.Sql.GetSql("Fee.InpatientDayReport.GetBalanceInvoiceZoneByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "��ȡ����Ա����Ʊ���������!";
                return null;
            }
            sql = string.Format(sql, beginDate, endDate, operID);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            while (this.Reader.Read())
            {

                BalanceInvoiceZone.ID = this.Reader[0].ToString();
                BalanceInvoiceZone.Name = this.Reader[1].ToString();
            }
            this.Reader.Close();

            return BalanceInvoiceZone;


        }

        #region ��ȡ����Ʊ������ luoff
         /// <summary>
         /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա����Ʊ������
         /// </summary>
         /// <param name="beginTime">��ʼʱ��</param>
         /// <param name="endTime">����ʱ��</param>
         /// <param name="operID">����Ա����</param>
         /// <param name="dsResult">�������ݼ�</param>
         /// <returns>1���ɹ�/-1��ʧ��</returns>
         public int GetBalanceInvoiceZoneNew(DateTime beginTime, DateTime endTime, string operID, ref DataSet dsResult)
         {
             string sql = string.Empty;
             if (this.Sql.GetSql("Fee.InpatientDayReport.GetBalanceInvoiceZoneNew", ref sql) == -1)
             {
                 this.Err = "��ȡ����Ա����Ʊ���������";
                 return -1;
             }
             try
             {
                 sql = string.Format(sql, beginTime.ToString(), endTime.ToString(), operID);
             }
             catch (Exception ex)
             {
                 this.Err = ex.Message;
                 return -1;
             }
             if (this.ExecQuery(sql, ref dsResult) == -1)
             {
                 this.Err = "ִ�в���ԱƱ�������ѯ������";
                 return -1;
             }
             return 1;
         }
        #endregion

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա����Ʊ������Ʊ��

        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="operID">����Ա����</param>
        /// <returns>����Ʊ�����飬ʧ�ܷ���null</returns>
        public ArrayList QueryWasteBalanceInvNOByOperIDAndTime(DateTime beginDate, DateTime endDate, string operID)
        {
            string sql = string.Empty;
            ArrayList alWasteBalanceInv = new ArrayList();
            Neusoft.HISFC.Models.Fee.Inpatient.Balance balance;

            if (this.Sql.GetSql("Fee.InpatientDayReport.QueryWasteBalanceInvNOByOperIDAndTime", ref sql) == -1)
            {
                this.Err = "��ȡ����Ա����Ʊ������Ʊ�ų���!";
                return null;
            }
            sql = string.Format(sql, beginDate, endDate, operID);

            if (this.ExecQuery(sql) == -1)
            {
                return null;
            }
            while (this.Reader.Read())
            {
                balance = new Balance();
                balance.Invoice.ID = this.Reader[0].ToString();
                alWasteBalanceInv.Add(balance);
            }
            this.Reader.Close();

            return alWasteBalanceInv;
        }
        #endregion

        #region �ս��µ���Ŀ���

        /// <summary>
        /// �����ս���Ŀ��ϸ
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="OperID">����Ա����</param>
        /// <returns>null��ʧ�� NOTNULL�ɹ�</returns>
        public DataSet GetDayReportItem(DateTime beginDate, DateTime endDate, string OperID)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetSql("Local.Report.InpatientDayREport.SelectDayReportItem", ref sqlStr) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            try
            {
                sqlStr = string.Format(sqlStr, OperID, beginDate.ToString(), endDate.ToString());
            }
            catch (Exception ex)
            {

                this.Err = "��ʽ��SQL���ʧ�ܣ�" + ex.Message;
                return null;
            }
            DataSet ds = new DataSet();
            if (this.ExecQuery(sqlStr, ref ds) == -1)
            {
                this.Err = "��������ʧ�ܣ�";
                return null;
            }
            return ds;
        }

        #region ͳ�ƴ�����Ŀ��ϸ luoff
         /// <summary>
         /// ����ͳ�ƴ�����Ŀ��ϸ
         /// </summary>
         /// <param name="begionTime">��ʼʱ��</param>
         /// <param name="endTime">����ʱ��</param>
         /// <param name="operID">����Ա����</param>
         /// <returns>ʧ�ܷ���null,�ɹ�notnull</returns>
         public DataSet GetDayReportItemZYRJ(DateTime begionTime, DateTime endTime, string operID)
         {
             string sqlStr = string.Empty;
             if (this.Sql.GetSql("Local.Report.InpatientDayReport.SelectDayReportItemZYRJ", ref sqlStr) == -1)
             {
                 this.Err = "����sql���ʧ�ܣ�";
                 return null;
             }
             try
             {
                 sqlStr = string.Format(sqlStr, operID, begionTime.ToString(), endTime.ToString());
             }
             catch (Exception ex)
             {
                 this.Err = "��ʽ��sql���ʧ�ܣ�" + ex.Message;
                 return null;
             }
             DataSet ds = new DataSet();
             if (this.ExecQuery(sqlStr, ref ds) == -1)
             {
                 this.Err = "��������ʧ�ܣ�";
                 return null;
             }
             return ds;
         }
        #endregion

        /// <summary>
        /// ��ȡҽ��Ԥ�տ���ϸ
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="OperID">����Ա����</param>
        /// <param name="aMod">0:�跽 1������</param>
        /// <returns>DataTable</returns>
        public System.Data.DataSet GetLenderPrePayDetail(DateTime beginDate, DateTime endDate, string OperID, int aMod)
        {
            string sqlStr = string.Empty;
            string sqlIndex = string.Empty;
            if (aMod == 0)
            {
                sqlIndex = "Fee.InpatientDayReport.GetPrepayDetail";
            }
            else
            {
                sqlIndex = "Local.Report.InpatientDayReprot.SelectPrePayDetail";
            }
            if (this.Sql.GetSql(sqlIndex, ref sqlStr) == -1)
            {
                this.Err = "����SELECT���ʧ�ܣ�";
                return null;
            }
      
            try
            {
                sqlStr = string.Format(sqlStr, OperID, beginDate, endDate);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʧ�ܣ�" + ex.Message;
                return null;
            }
            DataSet ds = new DataSet();
            if (this.ExecQuery(sqlStr, ref ds) == -1)
            {
                this.Err = "��ѯ����ʧ�ܣ�";
                return null;
            }
            return ds;
        }
        
        /// <summary>
        /// ��ȡ����ʱҽ��Ԥ�տ���ϸ
        /// </summary>
        /// <param name="list">�ս�ʵ�弯��</param>
        /// <param name="aMod">0����1����</param>
        /// <returns></returns>
        public DataSet GetLenderPrePayDetail(List<Class.DayReport> list, int aMod)
        {
            string sqlStr = string.Empty;
            string sqlIndex = string.Empty;
            string execSql = string.Empty;
            if (aMod == 1)
            {
                sqlIndex = "Local.Report.InpatientDayReprot.SelectPrePayDetail";
            }
            else
            {
                sqlIndex = "Fee.InpatientDayReport.GetPrepayDetail";
            }
            if (this.Sql.GetSql(sqlIndex, ref sqlStr) == -1)
            {
                this.Err = "����SELECT���ʧ�ܣ�";
                return null;
            }
           
            try
            {               
                string tempStr = sqlStr;
                foreach (Class.DayReport dayReport in list)
                {
                    sqlStr = string.Format(tempStr, dayReport.Oper.ID, dayReport.BeginDate.ToString(), dayReport.EndDate.ToString());
                    execSql +=" " + sqlStr + " " + "union all";
                }
                execSql = execSql.Substring(0, execSql.LastIndexOf("union all"));
            }
            catch (Exception ex)
            {
                this.Err = "��������ʧ�ܣ�"+ex.Message;
                return null;
            }
            DataSet ds=new DataSet();
            if (this.ExecQuery(execSql, ref ds) == -1)
            {
                this.Err = "��������ʧ�ܣ�";
                return null;
            }
            return ds;
        }

        /// <summary>
        /// ����ҽ��Ӧ�տ���ϸ
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="OperID">����Ա</param>
        /// <returns></returns>
        public DataSet GetLenderPayDetail(DateTime beginDate, DateTime endDate, string OperID)
        {
            string sqlStr = string.Empty;
            string whereStr = string.Empty;
            if (this.Sql.GetSql("Fee.InpatientDayReport.GetPrepayDetail", ref sqlStr) == -1)
            {
                this.Err = "����SELECT���ʧ�ܣ�";
                return null;
            }
            try
            {
                sqlStr = string.Format(sqlStr, OperID, beginDate, endDate);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʧ�ܣ�" + ex.Message;
                return null;
            }
            DataSet ds = new DataSet();
            if (this.ExecQuery(sqlStr, ref ds) == -1)
            {
                this.Err = "��ѯ����ʧ�ܣ�";
                return null;
            }
            return ds;
        }

        /// <summary>
        /// ���һ���ʱҽ��Ӧ�տ���ϸ
        /// </summary>
        ///<param name="list">�ս�ʵ�弯��</param>
        /// <returns></returns>
        public DataSet GetLenderPayDetail(List<Class.DayReport> list)
        {
            string sqlStr = string.Empty;
            string execSql = string.Empty;
            
            if (this.Sql.GetSql("Fee.InpatientDayReport.GetPrepayDetail", ref sqlStr) == -1)
            {
                this.Err = "����SELECT���ʧ�ܣ�";
                return null;
            }

            try
            {
                string tempStr = sqlStr;
                foreach (Class.DayReport dayReport in list)
                {
                    sqlStr = string.Format(tempStr, dayReport.Oper.ID, dayReport.BeginDate.ToString(), dayReport.EndDate.ToString());
                    execSql += " " + sqlStr + " " + "union all";
                }
                execSql = execSql.Substring(0, execSql.LastIndexOf("union all"));
            }
            catch (Exception ex)
            {
                this.Err = "��������ʧ�ܣ�" + ex.Message;
                return null;
            }
            DataSet ds = new DataSet();
            if (this.ExecQuery(execSql, ref ds) == -1)
            {
                this.Err = "��������ʧ�ܣ�";
                return null;
            }
            return ds;
        }

        /// <summary>
        /// ����ʡ����ҽ��֧��
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">����Ա����</param>
        /// <returns></returns>
        public DataSet GetDayReportProtectPay(DateTime beginDate, DateTime endDate, string OperID)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetSql("Local.Clinic.GetProtectMoney", ref sqlStr) == -1)
            {
                this.Err = "����SQLʧ�ܣ�";
                return null;
            }
            try
            {
                //1���������ݡ�2��סԺ����
                sqlStr = string.Format(sqlStr, OperID, beginDate.ToString(), endDate.ToString(),"2");
            }
            catch
            {
                this.Err = "��ʽ��SQL���ʧ�ܣ�";
                return null;
            }
            DataSet ds = new DataSet();
            if (this.ExecQuery(sqlStr, ref ds) == -1)
            {
                this.Err = "��������ʧ�ܣ�";
                return null;
            }
            return ds;
        }

        /// <summary>
        /// ����ʱ��κ��շ�Ա������ҵ������
        /// </summary>
        /// <param name="stringIndex">SQL���Index</param>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">�շ�Ա����</param>
        /// <returns></returns>
        private string GetSingleCost(string stringIndex, DateTime beginDate, DateTime endDate, string OperID)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetSql(stringIndex, ref sqlStr) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return string.Empty;
            }
            try
            {
                sqlStr = string.Format(sqlStr, OperID, beginDate.ToString(), endDate.ToString());
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʧ�ܣ�" + ex.Message;
                return string.Empty;
            }
            return this.ExecSqlReturnOne(sqlStr);
        }

        /// <summary>
        /// ����ҽ��Ӧ�տ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="OperID">�տ�Ա����</param>
        /// <returns></returns>
        public string GetLenderPay(DateTime beginDate, DateTime endDate, string OperID)
        {
            return this.GetSingleCost("Fee.InpatientDayReport.GetLenderPayCost", beginDate, endDate, OperID);
        }

        /// <summary>
        /// ҽ�Ƽ�����
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">����ʱ��</param>
        /// <param name="OperID">����Ա����</param>
        /// <returns></returns>
        public string GetDayReportDerCost(DateTime beginDate, DateTime endDate, string OperID)
        {
            return this.GetSingleCost("Local.Report.InpatientDayReport.SelectDerCost", beginDate, endDate, OperID);
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���ԱԤ�������п��ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">�տ�Ա����</param>
        /// <returns></returns>
        public string GetPrepayBankCardCost(DateTime beginDate, DateTime endDate, string OperID)
        {
            return this.GetSingleCost("Fee.InpatientDayReport.GetPrepayBankCardCost", beginDate, endDate, OperID);
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���ԱԺ���ʻ��ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">�տ�Ա����</param>
        /// <returns></returns>
        public string GetYSCost(DateTime beginDate, DateTime endDate, string OperID)
        {
            return this.GetSingleCost("Fee.InpatientDayReport.GetYSCost", beginDate, endDate, OperID);
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա���㲹��Ժ���ʻ��ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">�տ�Ա����</param>
        /// <returns></returns>
        public string GetSupplyYSCost(DateTime beginDate, DateTime endDate, string OperID)
        {
            return this.GetSingleCost("Fee.InpatientDayReport.GetSupplyYSCost", beginDate, endDate, OperID);
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա�˻�Ժ���ʻ��ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">�տ�Ա����</param>
        /// <returns></returns>
        public string GetReturnYSCost(DateTime beginDate, DateTime endDate, string OperID)
        {
            return this.GetSingleCost("Fee.InpatientDayReport.GetReturnYSCost", beginDate, endDate, OperID);
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա����Ԥ���ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">�տ�Ա����</param>
        /// <returns></returns>
        public string GetOtherPrepayCost(DateTime beginDate, DateTime endDate, string OperID)
        {
            return this.GetSingleCost("Fee.InpatientDayReport.GetOtherPrePayCost", beginDate, endDate, OperID);
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա���㲹������Ԥ���ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">�տ�Ա����</param>
        /// <returns></returns>
        public string GetSupplyOtherPrePayCost(DateTime beginDate, DateTime endDate, string OperID)
        {
            return this.GetSingleCost("Fee.InpatientDayReport.GetSupplyOtherPrePayCost", beginDate, endDate, OperID);
        }

        /// <summary>
        /// ��ȡһ��ʱ�䷶Χ�ڲ���Ա�˻�����Ԥ���ܶ�
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">�տ�Ա����</param>
        /// <returns></returns>
        public string GetReturnOtherPrePayCost(DateTime beginDate, DateTime endDate, string OperID)
        {
            return this.GetSingleCost("Fee.InpatientDayReport.GetReturnOtherPrePayCost", beginDate, endDate, OperID);
        }

        /// <summary>
        /// ����סԺ�սᱨ��
        /// </summary>
        /// <param name="dayReport">�սᱨ��ʵ��</param>
        /// <returns>1:�ɹ� -1 ʧ��</returns>
        public int InsertDayReport(Class.DayReport dayReport)
        { 
            return this.UpdateSingleTable("Fee.InpatientDayReport.InsertDayReport1",this.GetInsertDayReportargs(dayReport));
        }

        /// <summary>
        /// ����ս������ַ�������
        /// </summary>
        /// <param name="dayReport">�ս�ʵ��</param>
        /// <returns></returns>
        private string[] GetInsertDayReportargs(Class.DayReport dayReport)
        {
            string[] arrayStr = {
                dayReport.StatNO.ToString(),//ͳ�����
                dayReport.BeginDate.ToString(),//��ʼʱ��
                dayReport.EndDate.ToString(),//��������
                dayReport.Oper.ID,//����Ա����
                dayReport.Oper.OperTime.ToString(),
                dayReport.PrepayCost.ToString(),//ҽ��Ԥ�տ����
                dayReport.DebitCheckCost.ToString(),//�跽֧Ʊ(���д��跽)
                dayReport.DebitBankCost.ToString(),//�跽���п�
                dayReport.BalancePrepayCost.ToString(),//ҽ��Ԥ�տ�跽
                dayReport.LenderCheckCost.ToString(),//����֧Ʊ(���д�����)
                dayReport.LenderBankCost.ToString(),//�������п�
                dayReport.BursaryPubCost.ToString(),//���Ѽ��ʽ��
                dayReport.CityMedicarePayCost.ToString(),//��ҽ���ʻ�֧�����
                dayReport.CityMedicarePubCost.ToString(),//��ҽ��ͳ����
                dayReport.ProvinceMedicarePayCost.ToString(),//ʡҽ���ʻ�֧�����
                dayReport.ProvinceMedicarePubCost.ToString(),//ʡҽ��ͳ����
                dayReport.PrepayInvCount.ToString(),//Ԥ����Ʊ����
                dayReport.BalanceInvCount.ToString(),//���㷢Ʊ����
                dayReport.PrepayWasteInvNO,//����Ԥ����Ʊ����
                dayReport.BalanceWasteInvNO,//���Ͻ��㷢Ʊ����
                dayReport.PrepayWasteInvCount.ToString(),//����Ԥ����Ʊ����
                dayReport.BalanceWasteInvCount.ToString(),//���Ͻ��㷢Ʊ����
                dayReport.PrepayInvZone,//Ԥ����Ʊ����,
                dayReport.BalanceInvZone,//���㷢Ʊ����
                dayReport.BalanceCost.ToString(),//ҽ��Ӧ�տ�(�����ܽ��)
                dayReport.DebitHos.ToString(),//Ժ���˻��跽
                dayReport.LenderHos.ToString(),//Ժ���˻�����
                dayReport.DebitOther.ToString(),//�跽����
                dayReport.LenderOther.ToString(),//��������
                dayReport.DerateCost.ToString(),//������
                dayReport.CityMedicareOverCost.ToString(),//�б����
                dayReport.ProvinceMedicareOverCost.ToString(),//ʡ�����
                dayReport.ProvinceMedicareOfficeCost.ToString(),//ʡ������Ա
                dayReport.DebitCash.ToString(),//����ֽ�跽
                dayReport.LenderCash.ToString()};//����ֽ����
            return arrayStr;
        }

        /// <summary>
        /// �����ս���ϸ
        /// </summary>
        /// <param name="dayReport">�ս�ʵ��</param>
        /// <returns>1:�ɹ� -1��ʧ��</returns>
        public int InsetDayReportDetail(Class.DayReport dayReport)
        {
            string Sqlstr = string.Empty;
            if (this.Sql.GetSql("Fee.InpatientDayReport.InsertDayReportDetail", ref Sqlstr) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return -1;
            }
            try
            {
                int resultValue = 0;
                string ExecSql = string.Empty;
                foreach (Class.Item item in dayReport.ItemList)
                {
                    ExecSql = string.Format(Sqlstr,
                                        dayReport.StatNO,//ͳ�����
                                        dayReport.BeginDate.ToString(),//��ʼʱ��
                                        dayReport.EndDate.ToString(),//����ʱ��
                                        dayReport.Oper.ID,
                                        item.StateCode,//ͳ�ƴ���
                                        item.TotCost,//���ý��
                                        item.OwnCost,//�Էѽ��
                                        item.PayCost,//�Ը�ҽ��
                                        item.PubCost,//����ҽ��
                                        item.Mark);//��ע
                    resultValue = this.ExecNoQuery(ExecSql);
                    if (resultValue == -1)
                        return -1;
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "�����ս���ϸʧ��" + ex.Message;
                return -1;
            }

        }

        /// <summary>
        /// �����ս�������Ϣ
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">����Ա</param>
        /// <returns></returns>
        public List<Class.DayReport> SelectDayReprotInfo(DateTime beginDate, DateTime endDate, string OperID)
        {
            string Sqlstr = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetSql("Fee.InpatientDayReport.QueryDayReportInfo", ref Sqlstr) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            if (this.Sql.GetSql("Fee.InpatientDayReport.QueryDayReportInfoWhere1", ref SqlWhere) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            try
            {
                SqlWhere = string.Format(SqlWhere, OperID, beginDate.ToString(), endDate.ToString());
                Sqlstr += " " + SqlWhere;
                if (this.ExecQuery(Sqlstr) == -1)
                {
                    return null;
                }
                Class.DayReport dayReport = null;
                List<Class.DayReport> list = new List<Report.InpatientFee.Class.DayReport>();
                while (this.Reader.Read())
                {
                    dayReport = new Report.InpatientFee.Class.DayReport();
                    dayReport.StatNO = this.Reader[0].ToString();//ͳ�����
                    dayReport.BeginDate = NConvert.ToDateTime(this.Reader[1]);//��ʼʱ��
                    dayReport.EndDate = NConvert.ToDateTime(this.Reader[2]);//��ֹʱ��
                    dayReport.Oper.ID = this.Reader[3].ToString(); //����Ա����
                    dayReport.Oper.Name = this.Reader[4].ToString();//����Ա����
                    dayReport.Oper.OperTime = NConvert.ToDateTime(this.Reader[5]);//ͳ��ʱ��
                    dayReport.BalanceInvZone = this.Reader[6].ToString();//���㷢Ʊ����
                    list.Add(dayReport);
                }
                return list;
            }
            catch
            {
                return null;
            }

        }
           /// <summary>
        /// �����ս�������Ϣ
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">����Ա</param>
        /// <returns></returns>
        public List<Class.DayReport> CollectDayReprotInfo(DateTime beginDate, DateTime endDate)
        {
            string Sqlstr = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetSql("Fee.InpatientDayReport.QueryDayReportInfo", ref Sqlstr) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            if (this.Sql.GetSql("Fee.InpatientDayReport.QueryDayReportInfoWhere2", ref SqlWhere) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            try
            {
                SqlWhere = string.Format(SqlWhere, beginDate.ToString(), endDate.ToString());
                Sqlstr += " " + SqlWhere;
                if (this.ExecQuery(Sqlstr) == -1)
                {
                    return null;
                }
                Class.DayReport dayReport = null;
                List<Class.DayReport> list = new List<Report.InpatientFee.Class.DayReport>();
                while (this.Reader.Read())
                {
                    dayReport = new Report.InpatientFee.Class.DayReport();
                    dayReport.StatNO = this.Reader[0].ToString();//ͳ�����
                    dayReport.BeginDate = NConvert.ToDateTime(this.Reader[1]);//��ʼʱ��
                    dayReport.EndDate = NConvert.ToDateTime(this.Reader[2]);//��ֹʱ��
                    dayReport.Oper.ID = this.Reader[3].ToString();//����Ա����
                    dayReport.Oper.Name = this.Reader[4].ToString();//����Ա����
                    dayReport.Oper.OperTime = NConvert.ToDateTime(this.Reader[5]);//ͳ��ʱ��
                    dayReport.BalanceInvZone = this.Reader[6].ToString();//���㷢Ʊ����
                    list.Add(dayReport);
                }
                return list;
            }
            catch
            {
                return null;
            }

        }
        #region {05FE6DC0-EE61-4aba-A00D-E57B853B3793}�ս���ܲ���
        /// <summary>
        /// �����ս�������Ϣ
        /// </summary>
        /// <param name="beginDate">��ʼʱ��</param>
        /// <param name="endDate">��ֹʱ��</param>
        /// <param name="OperID">����Ա</param>
        /// <returns></returns>
        public List<Class.DayReport> CollectCheckedDayReprotInfo(DateTime beginDate, DateTime endDate)
        {
            string Sqlstr = string.Empty;
            string SqlWhere = string.Empty;
            if (this.Sql.GetSql("Fee.InpatientDayReport.QueryCheckedDayReportInfo", ref Sqlstr) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            if (this.Sql.GetSql("Fee.InpatientDayReport.QueryCheckedDayReportInfoWhere2", ref SqlWhere) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            try
            {
                SqlWhere = string.Format(SqlWhere, beginDate.ToString(), endDate.ToString());
                Sqlstr += " " + SqlWhere;
                if (this.ExecQuery(Sqlstr) == -1)
                {
                    return null;
                }
                Class.DayReport dayReport = null;
                List<Class.DayReport> list = new List<Report.InpatientFee.Class.DayReport>();
                while (this.Reader.Read())
                {
                    dayReport = new Report.InpatientFee.Class.DayReport();
                    dayReport.StatNO = this.Reader[0].ToString();//ͳ�����
                    dayReport.BeginDate = NConvert.ToDateTime(this.Reader[1]);//��ʼʱ��
                    dayReport.EndDate = NConvert.ToDateTime(this.Reader[2]);//��ֹʱ��
                    dayReport.Oper.ID = this.Reader[3].ToString();//����Ա����
                    dayReport.Oper.Name = this.Reader[4].ToString();//����Ա����
                    dayReport.Oper.OperTime = NConvert.ToDateTime(this.Reader[5]);//ͳ��ʱ��
                    dayReport.BalanceInvZone = this.Reader[6].ToString();//���㷢Ʊ����
                    //���ʱ��
                    dayReport.Memo = this.Reader[7].ToString();
                    list.Add(dayReport);
                }
                return list;
            }
            catch
            {
                return null;
            }

        } 
        #endregion

        /// <summary>
        /// �����ս�����
        /// </summary>
        ///<param name="statcodes">ͳ�Ʊ����ַ���</param>
        /// <param name="aMod">0��ѯ����ͳ��</param>
        /// <returns></returns>
        public Class.DayReport SelectDayReport(string statcodes,int aMod)
        {
            Class.DayReport dayReprot = null;
            try
            {
                
                dayReprot = SetDayReport(statcodes,aMod);
                return dayReprot;
            }
            catch (Exception ex)
            {
                this.Err = "��������ʧ�ܣ�" + ex.Message;
                return null;
            }
        }

        /// <summary>
        /// �ս�����
        /// </summary>
        /// <param name="statcodes">ͳ�Ʊ����ַ���</param>
        /// <param name="aMod">0��ѯ����ͳ��</param>
        /// <returns></returns>
        private Class.DayReport SetDayReport( string statcodes,int aMod)
        {
            string Sqlstr = string.Empty;
            string sqlIndex = string.Empty;
            if (aMod == 0)
            {
                sqlIndex = "Fee.InpatientDayReport.SelectDayReport";
            }
            else
            {
                sqlIndex = "Fee.InpatientDayReport.CollectDayReprot";
            }
            if (this.Sql.GetSql(sqlIndex, ref Sqlstr) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            try
            {
                Sqlstr = string.Format(Sqlstr, statcodes);
                Class.DayReport dayReport = null;
                if (this.ExecQuery(Sqlstr) == -1) return null;
                while (this.Reader.Read())
                {
                    dayReport = new Report.InpatientFee.Class.DayReport();
                    dayReport.StatNO = this.Reader[0].ToString();//ͳ�����
                    dayReport.BeginDate = NConvert.ToDateTime(this.Reader[1]);//��ʼʱ��
                    dayReport.EndDate = NConvert.ToDateTime(this.Reader[2]);//��ֹʱ��
                    dayReport.Oper.Name = this.Reader[3].ToString();//����Ա����
                    dayReport.Oper.OperTime = NConvert.ToDateTime(this.Reader[4]);//ͳ��ʱ��
                    dayReport.PrepayCost = NConvert.ToDecimal(this.Reader[5]);//��ҽ��Ԥ�տ�
                    dayReport.DebitCheckCost = NConvert.ToDecimal(this.Reader[6]);//�����д��
                    dayReport.DebitBankCost = NConvert.ToDecimal(this.Reader[7]);//�����п�
                    dayReport.BalancePrepayCost = NConvert.ToDecimal(this.Reader[8]);//��ҽ��Ԥ�տ�
                    dayReport.LenderCheckCost = NConvert.ToDecimal(this.Reader[9]);//�����д��
                    dayReport.LenderBankCost = NConvert.ToDecimal(this.Reader[10]);//�����п�
                    dayReport.BursaryPubCost = NConvert.ToDecimal(this.Reader[11]);//���Ѽ��ʽ��
                    dayReport.CityMedicarePayCost = NConvert.ToDecimal(this.Reader[12]);//��ҽ���ʻ�֧�����
                    dayReport.CityMedicarePubCost = NConvert.ToDecimal(this.Reader[13]);//��ҽ��ͳ����
                    dayReport.ProvinceMedicarePayCost = NConvert.ToDecimal(this.Reader[14]);//ʡҽ���ʻ�֧�����
                    dayReport.ProvinceMedicarePubCost = NConvert.ToDecimal(this.Reader[15]);//ʡҽ��ͳ����
                    dayReport.PrepayInvCount = NConvert.ToInt32(this.Reader[16]);//Ԥ����Ʊ����
                    dayReport.BalanceInvCount = NConvert.ToInt32(this.Reader[17]);//���㷢Ʊ����
                    dayReport.PrepayWasteInvNO = this.Reader[18].ToString();//����Ԥ����Ʊ����
                    dayReport.BalanceWasteInvNO = this.Reader[19].ToString();//���Ͻ��㷢Ʊ����
                    dayReport.PrepayWasteInvCount = NConvert.ToInt32(this.Reader[20]);//����Ԥ����Ʊ����
                    dayReport.BalanceWasteInvCount = NConvert.ToInt32(this.Reader[21]);//���Ͻ��㷢Ʊ����
                    dayReport.PrepayInvZone = this.Reader[22].ToString();//Ԥ����Ʊ����
                    dayReport.BalanceInvZone = this.Reader[23].ToString();//���㷢Ʊ����
                    dayReport.BalanceCost = NConvert.ToDecimal(this.Reader[24]);//ҽ��Ӧ�տ�(�����ܽ��)
                    dayReport.DebitHos = NConvert.ToDecimal(this.Reader[25]);//Ժ���˻��跽
                    dayReport.LenderHos = NConvert.ToDecimal(this.Reader[26]);//Ժ���˻�����
                    dayReport.DebitOther = NConvert.ToDecimal(this.Reader[27]);//�跽����
                    dayReport.LenderOther = NConvert.ToDecimal(this.Reader[28]);//��������
                    dayReport.DerateCost = NConvert.ToDecimal(this.Reader[29]);//������
                    dayReport.CityMedicareOverCost = NConvert.ToDecimal(this.Reader[30]);//�б����
                    dayReport.ProvinceMedicareOverCost = NConvert.ToDecimal(this.Reader[31]);//ʡ�����
                    dayReport.ProvinceMedicareOfficeCost = NConvert.ToDecimal(this.Reader[32]);//ʡ������Ա
                    dayReport.DebitCash = NConvert.ToDecimal(this.Reader[33]);//����ֽ�跽
                    dayReport.LenderCash = NConvert.ToDecimal(this.Reader[34]);//����ֽ����
                    #region ��ϸ
                    if(aMod==0)
                        dayReport.ItemList = SelectDayReportDetail("Fee.InpatientDayReport.SelectDayReportDetail", statcodes);
                    else
                        dayReport.ItemList = SelectDayReportDetail("Fee.InpatientDayReport.CollectDayReprotDetail", statcodes);
                    #endregion
                }
                return dayReport;
            }
            catch
            {
                return null;
            }
        }

        private List<Class.Item> SelectDayReportDetail(string sqlIndex, string statcodes)
        {
            string Sqlstr = string.Empty;
            if (this.Sql.GetSql(sqlIndex, ref Sqlstr) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return null;
            }
            try
            {
                Sqlstr = string.Format(Sqlstr, statcodes);
                if (this.ExecQueryByTempReader(Sqlstr) == -1)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return null;
                }
                Class.Item item = null;
                List<Class.Item> list = new List<Report.InpatientFee.Class.Item>();
                while (this.TempReader.Read())
                {
                    item = new Report.InpatientFee.Class.Item();
                    item.StateCode = TempReader[0].ToString();
                    item.Mark = TempReader[2].ToString();
                    item.TotCost = NConvert.ToDecimal(TempReader[1]);
                    list.Add(item);
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// �����ս��������
        /// </summary>
        /// <param name="statNO">ͳ�Ʊ���</param>
        /// <param name="operDate">����ʱ��</param>
        /// <param name="operID">����Ա����</param>
        /// <returns></returns>
        public int SaveCollectData(string operID,DateTime operDate, string statNO)
        {
            string sqlStr = string.Empty;
            if (this.Sql.GetSql("Fee.InpatientDayReport.SaveCollectDayReprot", ref sqlStr) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return -1;
            }
            try
            {
                sqlStr = string.Format(sqlStr, "1", operID, operDate, statNO);
            }
            catch (Exception ex)
            {

                this.Err = "��ʽ��SQL���ʧ�ܣ�";
                return -1;
            }
            return this.ExecNoQuery(sqlStr);
                    
        }
        #endregion

         //{9B8D83F8-CB0F-48fb-8ECD-0BA4462A952A}
        /// <summary>
        /// �����ս���(fin_ipb_inprepay fin_ipb_balancehead)
        /// </summary>
        /// <param name="dayReportID"></param>
        /// <param name="operID"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public int UpdateDayReport(string dayReportID, string operID, DateTime operDate, DateTime dtBegin, DateTime dtEnd)
        {
            string sql = string.Empty;
            if (this.Sql.GetSql("Fee.InpatientDayReport.UpdatePrePayFlag", ref sql) == -1)
            {
                this.Err = "��ѯ����ΪFee.InpatientDayReport.UpdatePrePayFlag��SQL���ʧ�ܣ�";
                return -1;
            }
            sql = string.Format(sql, dtBegin.ToString(), dtEnd.ToString());
            if (this.ExecNoQuery(sql) < 0)
            {
                return -1;
            }
            if (this.Sql.GetSql("Fee.InpatientDayReport.UpdateBalanceHeadFlag", ref sql) == -1)
            {
                this.Err = "��ѯ����ΪFee.InpatientDayReport.UpdateBalanceHeadFlag��SQL���ʧ�ܣ�";
                return -1;
            }
            sql = string.Format(sql, dayReportID, operID, operDate.ToString(), dtBegin.ToString(), dtEnd.ToString());
            if (this.ExecNoQuery(sql) == -1)
            {
                return -1;
            }
            return 1;
        }
        #endregion
    }
}
