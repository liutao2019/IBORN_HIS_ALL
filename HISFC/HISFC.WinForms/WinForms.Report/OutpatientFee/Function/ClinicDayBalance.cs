/*----------------------------------------------------------------
            // ��Ȩ���С� 
            //
            // �ļ�����			ClinicDayBalance.cs
            // �ļ�����������	�����տ��ս᷽����
            //
            // 
            // ������ʶ��		2006-3-22
            //
            // �޸ı�ʶ��
            // �޸�������
            //
            // �޸ı�ʶ��
            // �޸�������
//----------------------------------------------------------------*/
using System;
using System.Collections;
using System.Data;
using FS.FrameWork.Models;
using FS.FrameWork.Function;
using System.Collections.Generic;

namespace FS.WinForms.Report.OutpatientFee.Function
{
    /// <summary>
    /// �����տ�Ա�ս�
    /// </summary>
    public class ClinicDayBalance : FS.FrameWork.Management.Database
    {
        public ClinicDayBalance()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        //
        // ����
        //
        #region ����
        /// <summary>
        ///  ����ֵ
        /// </summary>
        int intReturn = 0;

        /// <summary>
        /// ִ�в�ѯ��SQL���
        /// </summary>
        string SQL = "";

        /// <summary>
        /// ��ѯ���
        /// </summary>
        string stringSelect = "";

        /// <summary>
        /// �������
        /// </summary>
        string stringWhere = "";

        /// <summary>
        /// �������
        /// </summary>
        string stringGroup = "";

        /// <summary>
        /// �������
        /// </summary>
        string stringOrder = "";

        /// <summary>
        /// ����SQL���Ĳ���
        /// </summary>
        string[] parms = new string[26];
        #endregion

        //
        // ��������
        //
        #region ��ʼ������
        /// <summary>
        /// ��ʼ������
        /// </summary>
        private void InitVar()
        {
            this.intReturn = 0;
            this.SQL = "";
            this.stringSelect = "";
            this.stringWhere = "";
            this.stringGroup = "";
            this.stringOrder = "";
        }
        #endregion

        #region ����SQL���
        /// <summary>
        /// ����SQL���
        /// </summary>
        private void CreateSQL()
        {
            this.SQL = this.stringSelect + " " + this.stringWhere + " " + this.stringGroup + " " + this.stringOrder;
        }
        #endregion

        #region ��ղ�������
        /// <summary>
        /// ��ղ�������
        /// </summary>
        public void ClearParms()
        {
            for (int i = 0; i < parms.Length; i++)
            {
                parms[i] = "";
            }
        }
        #endregion

        #region ����SQL���ƥ�����
        /// <summary>
        /// ����SQL���ƥ�����
        /// </summary>
        /// <param name="clinicDayBalance">�ս�ʵ����</param>
        /// <param name="insert">�Ƿ����</param>
        public void FillParms(Class.ClinicDayBalance clinicDayBalance, bool insert)
        {
            // ��ղ�������
            this.ClearParms();

            // �ս����
            parms[0] = clinicDayBalance.BalanceSequence;
            // �ս����ݿ�ʼʱ��
            parms[1] = clinicDayBalance.BeginDate.ToString();
            // �ս����ݽ�ֹʱ��
            parms[2] = clinicDayBalance.EndDate.ToString();
            // ������
            parms[3] = clinicDayBalance.Cost.TotCost.ToString();
            // �տ�Ա����
            parms[4] = clinicDayBalance.BalanceOperator.ID;
            // �տ�Ա����
            parms[5] = clinicDayBalance.BalanceOperator.Name;
            // �ս����ʱ��
            parms[6] = clinicDayBalance.BalanceDate.ToString();
            // ��ע���1
            parms[7] = clinicDayBalance.UnValidNumber.ToString();
            // ��ע���2
            parms[8] = clinicDayBalance.BKNumber.ToString();
            // ��ע���3
            parms[9] = clinicDayBalance.Memo;
            // ������˱�־
            parms[10] = clinicDayBalance.CheckFlag;
            // ���������
            parms[11] = clinicDayBalance.CheckOperator.ID;
            // �������ʱ��
            parms[12] = clinicDayBalance.CheckDate.ToString();
            // �ս���Ŀ
            if (clinicDayBalance.BalanceItem == FS.HISFC.Models.Base.CancelTypes.Valid)
            {
                // ����
                parms[13] = "0";
            }
            else if (clinicDayBalance.BalanceItem == FS.HISFC.Models.Base.CancelTypes.Canceled)
            {
                // �˷�
                parms[13] = "1";
            }
            else if (clinicDayBalance.BalanceItem == FS.HISFC.Models.Base.CancelTypes.Reprint)
            {
                // �ش�
                parms[13] = "2";
            }
            else if (clinicDayBalance.BalanceItem == FS.HISFC.Models.Base.CancelTypes.LogOut)
            {
                // ע��
                parms[13] = "3";
            }
            // �ս���Ŀ��Ӧ��Ʊ��
            parms[14] = clinicDayBalance.InvoiceNo;
            // ʵ�ս��
            parms[15] = clinicDayBalance.Cost.OwnCost.ToString();
            // ���ʵ�����
            parms[16] = clinicDayBalance.AccountNumber.ToString();
            // ��չ�ֶ�
            parms[17] = clinicDayBalance.ExtendField;
            // ���ʽ��
            parms[18] = clinicDayBalance.Cost.LeftCost.ToString();
            //ˢ������
            parms[19] = clinicDayBalance.CDNumber.ToString();
            //�ֽ�
            parms[20] = clinicDayBalance.BackCost1.ToString();
            //ˢ��
            parms[21] = clinicDayBalance.BackCost2.ToString();
            //֧Ʊ
            parms[22] = clinicDayBalance.BackCost3.ToString();
            //�˷ѷ�Ʊ��ϸ
            parms[23] = clinicDayBalance.BKInvoiceNo;
            //���Ϸ�Ʊ��ϸ
            parms[24] = clinicDayBalance.UnValidInvoiceNo;
            //��Ʊ����
            parms[25] = clinicDayBalance.InvoiceBand;


        }
        #endregion

        #region ת�����ص�Reader��ʵ����
        /// <summary>
        /// ת�����ص�Reader��ʵ����
        /// </summary>
        /// <param name="clinicDayBalance">���ص��ս�ʵ��</param>
        public void ChangeReaderToClass(ref Class.ClinicDayBalance clinicDayBalance)
        {
            // ���ReaderΪ�գ���ô���ؿ�
            if (this.Reader == null)
            {
                clinicDayBalance = null;
            }

            // ת��
            if (this.Reader.Read())
            {
                // �ս����
                clinicDayBalance.BalanceSequence = this.Reader[2].ToString();
                // �ս����ݿ�ʼʱ��
                try
                {
                    clinicDayBalance.BeginDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3]);
                }
                catch
                {
                    clinicDayBalance.BeginDate = DateTime.MinValue;
                }
                // �ս����ݽ�ֹʱ��
                try
                {
                    clinicDayBalance.EndDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);
                }
                catch
                {
                    clinicDayBalance.EndDate = DateTime.MinValue;
                }
                // ������
                clinicDayBalance.Cost.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                // �տ�Ա����
                clinicDayBalance.BalanceOperator.ID = this.Reader[6].ToString();
                // �տ�Ա����
                clinicDayBalance.BalanceOperator.Name = this.Reader[7].ToString();
                // �ս����ʱ��
                try
                {
                    clinicDayBalance.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                }
                catch
                {
                    clinicDayBalance.BalanceDate = DateTime.MinValue;
                }
                // ��ע���1
                clinicDayBalance.UnValidNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9]);
                // ��ע���2
                clinicDayBalance.BKNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]);
                // ��ע���3
                clinicDayBalance.User03 = this.Reader[11].ToString();
                // ������˱�־
                clinicDayBalance.CheckFlag = this.Reader[12].ToString();
                // ���������
                clinicDayBalance.CheckOperator.ID = this.Reader[13].ToString();
                // �������ʱ��
                clinicDayBalance.CheckDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14]);
                // �ս���Ŀ
                if (this.Reader[15].ToString() == "0")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Valid;
                }
                else if (this.Reader[15].ToString() == "1")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Canceled;
                }
                else if (this.Reader[15].ToString() == "2")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Reprint;
                }
                else if (this.Reader[15].ToString() == "3")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.LogOut;
                }
                // �ս���Ŀ��Ӧ��Ʊ��
                clinicDayBalance.InvoiceNo = this.Reader[16].ToString();
                // ʵ�ս��
                clinicDayBalance.Cost.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());
                // ���ʵ�����
                clinicDayBalance.AccountNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[18].ToString());
                // ��չ�ֶ�
                clinicDayBalance.ExtendField = this.Reader[19].ToString();
                // ���ʽ��
                clinicDayBalance.Cost.LeftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                //ˢ������
                if (this.Reader[21] != null)
                {
                    clinicDayBalance.CDNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[21].ToString());
                }
                //ˢ��
                if (this.Reader[22] != null)
                {
                    clinicDayBalance.BackCost2 = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[22].ToString());
                }
                //�ֽ�
                if (this.Reader[23] != null)
                {
                    clinicDayBalance.BackCost1 = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[23].ToString());
                }
                //֧Ʊ
                if (this.Reader[24] != null)
                {
                    clinicDayBalance.BackCost3 = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[24].ToString());
                }
                //Ʊ������
                clinicDayBalance.RecipeBand = this.Reader[25].ToString();
                //��Ʊ����
                clinicDayBalance.InvoiceBand = this.Reader[26].ToString();
                //���Ϸ�Ʊ��ϸ
                clinicDayBalance.UnValidInvoiceNo = this.Reader[27].ToString();
                //�˷ѷ�Ʊ��ϸ
                clinicDayBalance.BKInvoiceNo = this.Reader[28].ToString();
            }
        }
        #endregion

        #region ת�����ص�Reader��ʵ��������
        /// <summary>
        /// ת�����ص�Reader��ʵ��������
        /// </summary>
        /// <param name="alClinicDayBalance">���ص��ս�ʵ������</param>
        public void ChangeReaderToClass(ref ArrayList alClinicDayBalance)
        {


            // ���ReaderΪ�գ���ô���ؿ�
            if (this.Reader == null)
            {
                return;
            }

            // ת��
            while (this.Reader.Read())
            {
                Class.ClinicDayBalance clinicDayBalance = new Class.ClinicDayBalance();
                // �ս����
                clinicDayBalance.BalanceSequence = this.Reader[2].ToString();
                // �ս����ݿ�ʼʱ��
                try
                {
                    clinicDayBalance.BeginDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[3]);
                }
                catch
                {
                    clinicDayBalance.BeginDate = DateTime.MinValue;
                }
                // �ս����ݽ�ֹʱ��
                try
                {
                    clinicDayBalance.EndDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[4]);
                }
                catch
                {
                    clinicDayBalance.EndDate = DateTime.MinValue;
                }
                // ������
                clinicDayBalance.Cost.TotCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[5]);
                // �տ�Ա����
                clinicDayBalance.BalanceOperator.ID = this.Reader[6].ToString();
                // �տ�Ա����
                clinicDayBalance.BalanceOperator.Name = this.Reader[7].ToString();
                // �ս����ʱ��
                try
                {
                    clinicDayBalance.BalanceDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[8]);
                }
                catch
                {
                    clinicDayBalance.BalanceDate = DateTime.MinValue;
                }
                // ��ע���1
                clinicDayBalance.UnValidNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[9]);
                // ��ע���2
                clinicDayBalance.BKNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[10]);
                // ��ע���3
                clinicDayBalance.User03 = this.Reader[11].ToString();
                // ������˱�־
                clinicDayBalance.CheckFlag = this.Reader[12].ToString();
                // ���������
                clinicDayBalance.CheckOperator.ID = this.Reader[13].ToString();
                // �������ʱ��
                clinicDayBalance.CheckDate = FS.FrameWork.Function.NConvert.ToDateTime(this.Reader[14]);
                // �ս���Ŀ
                if (this.Reader[15].ToString() == "0")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Valid;
                }
                else if (this.Reader[15].ToString() == "1")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Canceled;
                }
                else if (this.Reader[15].ToString() == "2")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.Reprint;
                }
                else if (this.Reader[15].ToString() == "3")
                {
                    clinicDayBalance.BalanceItem = FS.HISFC.Models.Base.CancelTypes.LogOut;
                }
                // �ս���Ŀ��Ӧ��Ʊ��
                clinicDayBalance.InvoiceNo = this.Reader[16].ToString();
                // ʵ�ս��
                clinicDayBalance.Cost.OwnCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[17].ToString());
                // ���ʵ�����
                clinicDayBalance.AccountNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[18].ToString());
                // ��չ�ֶ�
                clinicDayBalance.ExtendField = this.Reader[19].ToString();
                // ���ʽ��
                clinicDayBalance.Cost.LeftCost = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[20].ToString());
                //ˢ������
                if (this.Reader[21] != null)
                {
                    clinicDayBalance.CDNumber = FS.FrameWork.Function.NConvert.ToInt32(this.Reader[21].ToString());
                }
                //ˢ��
                if (this.Reader[22] != null)
                {
                    clinicDayBalance.BackCost2 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[22].ToString());
                }
                //�ֽ�
                if (this.Reader[23] != null)
                {
                    clinicDayBalance.BackCost1 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[23].ToString());
                }
                //֧Ʊ
                if (this.Reader[24] != null)
                {
                    clinicDayBalance.BackCost3 = FS.FrameWork.Function.NConvert.ToDecimal(this.Reader[24].ToString());
                }
                //Ʊ������
                clinicDayBalance.RecipeBand = this.Reader[25].ToString();
                //��Ʊ����
                clinicDayBalance.InvoiceBand = this.Reader[16].ToString();
                //���Ϸ�Ʊ��ϸ
                clinicDayBalance.UnValidInvoiceNo = this.Reader[27].ToString();
                //�˷ѷ�Ʊ��ϸ
                clinicDayBalance.BKInvoiceNo = this.Reader[28].ToString();
                // ���ʵ��
                alClinicDayBalance.Add(clinicDayBalance);
            }
        }
        #endregion

        #region ���㷢Ʊ����
        /// <summary>
        /// ���㷢Ʊ����
        /// </summary>
        /// <param name="invoiceCode">��Ʊ�Ż�Ʊ����</param>
        /// <returns>��Ʊ����</returns>
        public int GetInvoiceCount(string invoiceCode)
        {
            // ��������
            int intCount = 0;
            int intLeft = 0;
            int intRight = 0;
            int intLength = 0;
            string stringSub = "";

            // ��ȡ���Ⱥͷָ��λ��
            intLength = invoiceCode.Length;
            intCount = invoiceCode.IndexOf("��");

            // ���û�зָ������ô��Ʊ����Ϊ1
            if (intCount == -1)
            {
                intCount = 1;
            }
            else
            {
                intLeft = int.Parse(invoiceCode.Substring(0, intCount));
                stringSub = invoiceCode.Substring(intCount + 1, intLength - intCount - 1);
                intRight = int.Parse(stringSub);
                intCount = intRight - intLeft + 1;

            }

            return intCount;
        }
        #endregion

        //
        // ��ѯ��ȡ
        //
        #region �����տ�Ա���Ż�ȡ�ϴ��ս�ʱ��(1���ɹ�/0��û�������ս�/-1��ʧ��)
        /// <summary>
        /// �����տ�Ա���Ż�ȡ�ϴ��ս�ʱ��(1���ɹ�/0��û�������ս�/-1��ʧ��)
        /// </summary>
        /// <param name="employee">����Ա</param>
        /// <param name="lastDate">�����ϴ��ս��ֹʱ��</param>
        /// <returns>1���ɹ�/0��û�������ս�/-1��ʧ��</returns>
        public int GetLastBalanceDate(FS.FrameWork.Models.NeuObject employee, ref string lastDate)
        {
            //
            // ��ʼ������
            //
            this.InitVar();

            //
            // ��ȡSQL���
            //

            // ��ȡ��ѯ���
            intReturn = this.Sql.GetSql("Local.Clinic.Function.GetLastBalanceDate.Select", ref stringSelect);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            // ��ȡ�������
            intReturn = this.Sql.GetSql("Local.Clinic.Function.GetLastBalanceDate.Where", ref stringWhere);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            this.CreateSQL();

            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL, employee.ID);
            }
            catch (Exception e)
            {
                this.InitVar();
                this.Err = "��ʽ��SQL���ʧ��(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(this.SQL);
            if (intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��" + this.Err;
                return -1;
            }

            //
            // ����ִ�н��
            //
            if (this.Reader == null)
            {
                lastDate = DateTime.MinValue.ToString();
                return 0;
            }
            this.Reader.Read();
            lastDate = this.Reader[0].ToString();
            if (lastDate == "")
            {
                lastDate = System.DateTime.MinValue.ToString();
            }

            // �������ݣ�����1
            return 1;
        }
        /// <summary>
        /// �õ��ս�����
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dtBegin"></param>
        /// <param name="?"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetZSYDayBalanceData(string employeeID, string dtBegin, string dtEnd, ref DataSet ds)
        {
            string strSql = "";
            string strWhere = "";
            int intReturn = -1;
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Select", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Select";
                return -1;
            }
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Where1", ref strWhere) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Where1";
                return -1;
            }
            strSql = strSql + strWhere;
            strSql = System.String.Format(strSql, employeeID, dtBegin, dtEnd);
            intReturn = this.ExecQuery(strSql, ref ds);
            if (intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��" + this.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// �õ��˷�����
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dtBegin"></param>
        /// <param name="?"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetZSYDayBalanceReturnData(string employeeID, string dtBegin, string dtEnd, ref DataSet ds)
        {
            string strSql = "";
            string strWhere = "";
            int intReturn = -1;
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Select", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Select";
                return -1;
            }
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Where2", ref strWhere) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Where1";
                return -1;
            }
            strSql = strSql + strWhere;
            strSql = System.String.Format(strSql, employeeID, dtBegin, dtEnd);
            intReturn = this.ExecQuery(strSql, ref ds);
            if (intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��" + this.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// �õ���Ч����
        /// </summary>
        /// <param name="employeeID"></param>
        /// <param name="dtBegin"></param>
        /// <param name="?"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public int GetZSYDayBalanceUnvalidData(string employeeID, string dtBegin, string dtEnd, ref DataSet ds)
        {
            string strSql = "";
            string strWhere = "";
            int intReturn = -1;
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Select", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Select";
                return -1;
            }
            if (this.Sql.GetSql("Local.Clinic.GetZSYDayBalanceData.Where3", ref strWhere) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetZSYDayBalanceData.Where1";
                return -1;
            }
            strSql = strSql + strWhere;
            strSql = System.String.Format(strSql, employeeID, dtBegin, dtEnd);
            intReturn = this.ExecQuery(strSql, ref ds);
            if (intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��" + this.Err;
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// �������ս��
        /// </summary>
        /// <param name="operCode"></param>
        /// <returns></returns>
        public string GetMaxBalanceNoByOper(string operCode)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Clinic.Function.GetMaxBalanceNo.Select", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.Function.GetLastBalanceDate.Select";
                return "";
            }
            strSql = System.String.Format(strSql, operCode);
            return this.ExecSqlReturnOne(strSql);
        }
        #endregion

        #region ��ȡ�����տ�Ա���ս�����(1���ɹ�/-1��ʧ��)
        /// <summary>
        /// ��ȡ�����տ�Ա���ս�����(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="employeeID">�����տ�Ա���</param>
        /// <param name="dateBegin">�ս���ʼʱ��</param>
        /// <param name="dateEnd">�ս��ֹʱ��</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetDayBalanceData(string employeeID, string dateBegin,
                                        string dateEnd, ref System.Data.DataSet dsResult)
        {
            //
            // ��ʼ������
            //
            this.InitVar();

            //
            // ��ȡSQL���
            //

            // ��ȡ��ѯ���
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceData.Select", ref stringSelect);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            // ��ȡ�������
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceData.Where", ref stringWhere);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            // ��ȡ�������
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceData.Group", ref stringGroup);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            // ��ȡ�������
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceData.Order", ref stringOrder);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            // ����SQL���
            this.CreateSQL();

            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception e)
            {
                this.InitVar();
                this.Err = "��ʽ��SQL���ʧ��(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(this.SQL, ref dsResult);
            if (intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��" + this.Err;
                return -1;
            }

            return 1;
        }
        #endregion

        #region ����ʱ�䷶Χ��ȡ��Ӧ���ս��¼������ϸ��
        /// <summary>
        /// ����ʱ�䷶Χ��ȡ��Ӧ���ս��¼������ϸ��
        /// </summary>
        /// <param name="employee">����Ա��Ϣ</param>
        /// <param name="dtFrom">��ʼʱ��</param>
        /// <param name="dtTo">��ֹʱ��</param>
        /// <param name="clinicDayBalance">���ص��ս��¼����</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetBalanceRecord(FS.FrameWork.Models.NeuObject employee, DateTime dtFrom, DateTime dtTo,
                                    ref ArrayList clinicDayBalance)
        {
            //
            // ��ʼ������
            //
            this.InitVar();
            // �ս��¼
            FS.FrameWork.Models.NeuObject balanceRecord = new NeuObject();

            //
            // ��ȡSQL���
            //

            // ��ȡ��ѯ���
            intReturn = this.Sql.GetSql("Local.Clinic.GetBalanceRecord.Select", ref stringSelect);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            // ��ȡ�������
            intReturn = this.Sql.GetSql("Local.Clinic.GetBalanceRecord.Where", ref stringWhere);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            // ����SQL���
            this.CreateSQL();

            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL, employee.ID, dtFrom, dtTo);
            }
            catch (Exception e)
            {
                this.InitVar();
                this.Err = "��ʽ��SQL���ʧ��(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(this.SQL);
            if (intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��" + this.Err;
                return -1;
            }

            //
            // ��ֵ
            //
            while (this.Reader.Read())
            {
                balanceRecord = new NeuObject();
                balanceRecord.ID = this.Reader[0].ToString();
                balanceRecord.Name = this.Reader[1].ToString();
                balanceRecord.Memo = this.Reader[2].ToString();
                balanceRecord.User01 = this.Reader[3].ToString();
                clinicDayBalance.Add(balanceRecord);
            }

            return 1;
        }
        /// <summary>
        /// ����ʱ�䷶Χ��ȡ��Ӧ���ս��¼������ϸ��
        /// </summary>
        /// <param name="employee">����Ա��Ϣ</param>
        /// <param name="empDept">����Ա����</param>
        /// <param name="dtFrom">��ʼʱ��</param>
        /// <param name="dtTo">��ֹʱ��</param>
        /// <param name="clinicDayBalance">���ص��ս��¼����</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetBalanceRecord(FS.FrameWork.Models.NeuObject employee, FS.FrameWork.Models.NeuObject empDept, DateTime dtFrom, DateTime dtTo,
            ref ArrayList clinicDayBalance)
        {
            //
            // ��ʼ������
            //
            this.InitVar();
            // �ս��¼
            FS.FrameWork.Models.NeuObject balanceRecord = new NeuObject();

            //
            // ��ȡSQL���
            //

            // ��ȡ��ѯ���
            intReturn = this.Sql.GetSql("Local.Clinic.GetBalanceRecord.Select", ref stringSelect);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            // ��ȡ�������
            intReturn = this.Sql.GetSql("Local.Clinic.GetBalanceRecord.ByOperDeptAndOperCode", ref stringWhere);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            // ����SQL���
            this.CreateSQL();

            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL, employee.ID, dtFrom, dtTo, empDept.ID);
            }
            catch (Exception e)
            {
                this.InitVar();
                this.Err = "��ʽ��SQL���ʧ��(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(this.SQL);
            if (intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��" + this.Err;
                return -1;
            }

            //
            // ��ֵ
            //
            while (this.Reader.Read())
            {
                balanceRecord = new NeuObject();
                balanceRecord.ID = this.Reader[0].ToString();
                balanceRecord.Name = this.Reader[1].ToString();
                balanceRecord.Memo = this.Reader[2].ToString();
                balanceRecord.User01 = this.Reader[3].ToString();
                clinicDayBalance.Add(balanceRecord);
            }

            return 1;
        }
        #endregion

        #region �����ս���ˮ�Ż�ȡ�ս���ϸ(1���ɹ�/-1��ʧ��)
        /// <summary>
        /// �����ս���ˮ�Ż�ȡ�ս���ϸ(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="stringSequece">�ս���ˮ��</param>
        /// <param name="balanceDetail">���ص��ս���ϸ</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetDayBalanceDetail(string stringSequece, ref ArrayList balanceDetail)
        {
            //
            // ��ʼ������
            //
            this.InitVar();

            //
            // ��ȡSQL���
            //

            // ��ȡ��ѯ���
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceDetail.Select", ref stringSelect);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            // ��ȡ�������
            intReturn = this.Sql.GetSql("Local.Clinic.GetDayBalanceDetail.Where", ref stringWhere);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            // ����SQL���
            this.CreateSQL();

            //
            // ��ʽ��SQL���
            //
            try
            {
                this.SQL = string.Format(this.SQL, stringSequece);
            }
            catch (Exception e)
            {
                this.InitVar();
                this.Err = "��ʽ��SQL���ʧ��(" + this.Err + ")(" + e.Message + ")";
                return -1;
            }

            //
            // ִ��SQL���
            //
            intReturn = this.ExecQuery(this.SQL);
            if (intReturn == -1)
            {
                this.Err = "ִ��SQL���ʧ��" + this.Err;
                return -1;
            }

            //
            // ��ֵ
            //
            this.ChangeReaderToClass(ref balanceDetail);

            return 1;
        }
        #endregion

        #region ��ȡ�ս����(1���ɹ�/-1��ʧ��)
        /// <summary>
        /// ��ȡ�ս����(1���ɹ�/-1��ʧ��)
        /// </summary>
        /// <param name="sequence">�����ս����к�</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetBalanceSequence(ref string sequence)
        {
            // ��ȡ�ս����
            sequence = this.GetSequence("Local.Clinic.Function.CreateClinicDayBalance.GetInsertSequence");
            if (sequence == null)
            {
                this.Err = "��ȡ��ˮ��ʧ�ܣ�" + this.Err;
                return -1;
            }
            return 1;
        }
        #endregion

        #region ���ݷ�Ʊ�Ż�ö�Ӧ��֧����ʽ�ͽ��
        /// <summary>
        /// ��÷�Ʊ��Ӧ֧����ʽ
        /// </summary>
        /// <param name="invoiceNo"></param>
        /// <returns></returns>
        public ArrayList GetPayModeByInvoiceNo(string invoiceNo, string invoice_seq, string transType)
        {
            string strSql = "";//sql ���

            ArrayList al = new ArrayList();//��������
            //
            //�Ҳ���sql
            //
            if (this.Sql.GetSql("Local.Clinic.GetPayModeByInvoiceNo", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.GetPayModeByInvoiceNo";
                return null;
            }
            strSql = System.String.Format(strSql, invoiceNo, invoice_seq, transType);
            //
            //ִ�г���
            //
            if (this.ExecQuery(strSql) < 0)
            {
                this.Err = "Execute Sql Err";
                return null;
            }
            while (this.Reader.Read())
            {
                FS.FrameWork.Models.NeuObject obj = new NeuObject();
                obj.ID = this.Reader[0].ToString();//��Ʊ��
                obj.Name = this.Reader[1].ToString();//֧����ʽ
                obj.Memo = this.Reader[2].ToString();//���
                al.Add(obj);
            }
            this.Reader.Close();
            return al;
        }
        #endregion

        #region  �����ս���ˮ�Ÿ����ս���Ʊ�ݷ�Χ���ֶ�
        /// <summary>
        /// �����ս���ˮ�Ÿ����ս���Ʊ�ݷ�Χ���ֶ�
        /// </summary>
        /// <param name="balanceID"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int UpdateOtherByBalanceID(string balanceID, FS.FrameWork.Models.NeuObject obj)
        {
            string strSql = "";
            if (this.Sql.GetSql("Local.Clinic.UpdateOtherByBalanceID", ref strSql) == -1)
            {
                this.Err = "Can't Find Sql:Local.Clinic.UpdateOtherByBalanceID";
                return -1;
            }
            strSql = System.String.Format(strSql, obj.ID, obj.Name, obj.Memo, obj.User01, obj.User02, obj.User03, balanceID);
            if (this.ExecNoQuery(strSql) < 0)
            {
                this.Err = "Execute Err";
                return -1;
            }
            return 1;
        }
        #endregion

        //
        // ���ݲ���
        //
        #region �����ս��(-1��ʧ��/1���ɹ�)
        /// <summary>
        /// �����ս��(-1��ʧ��/1���ɹ�)
        /// </summary>
        /// <param name="clinicDayBalance">������ս�ʵ��</param>
        /// <returns>-1��ʧ��/1</returns>
        public int CreateClinicDayBalance(Class.ClinicDayBalance clinicDayBalance)
        {
            // ��ʼ������
            this.InitVar();


            // ��ȡSQL���
            this.intReturn = this.Sql.GetSql("Local.Clinic.Function.CreateClinicDayBalance", ref this.stringSelect);
            if (intReturn == -1)
            {
                this.Err = "��ȡSQL���ʧ��" + this.Err;
                return -1;
            }
            this.CreateSQL();

            // ƥ�����
            this.FillParms(clinicDayBalance, true);

            // ��ʽ�����
            try
            {
                this.SQL = string.Format(this.SQL, this.parms);
            }
            catch (Exception e)
            {
                this.Err = "��ʽ��SQL���ʧ��" + e.Message;
                return -1;
            }

            // ִ��SQL���
            intReturn = this.ExecNoQuery(this.SQL);
            if (intReturn <= 0)
            {
                this.Err = "ִ��SQL���ʧ��" + this.Err;
                return -1;
            }

            return 1;
        }
        #endregion

        #region ���ս�
        /// <summary>
        /// ��ȡ�ս���Ŀ����
        /// </summary>
        /// <param name="employeeID">�տ�Ա����</param>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">����ʱ��</param>
        /// <param name="dsResult">�������ݼ�</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetDayBalanceDataNew(string employeeID, string dateBegin,
                                       string dateEnd, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("Local.Clinic.GetDayBalanceDataNew.Select", ref SQL) == -1)
            {
                this.Err = "����Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;

            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "ִ��SQL���ʧ�ܣ�";
                return -1;
            }
            return 1;
        }

        #region �����ս� luoff

        public int GetDayBalanceDataMZRJ(string employeeID, string dateBegin,
                                         string dateEnd, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("Local.Clinic.GetDayBalanceDataMZRJ.Select", ref SQL) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "ִ��SQL���ʧ�ܣ�";
                return -1;
            }
            return 1;

        }
        #endregion
        /// <summary>
        /// ��ȡ�սᷢƱ����
        /// </summary>
        /// <param name="employeeID">�տ�Ա����</param>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">����ʱ��</param>
        /// <param name="dsResult">�������ݼ�</param>
        /// <returns>1���ɹ�/-1��ʧ��</returns>
        public int GetDayInvoiceDataNew(string employeeID, string dateBegin,
                                       string dateEnd, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("Local.Clinic.GetDayInvoiceDataNew.Select", ref SQL) == -1)
            {
                this.Err = "����Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;

            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "ִ��SQL���ʧ�ܣ�";
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ���ɽ�������
        /// </summary>
        /// <param name="clicicDayBlanceNew">��������ʵ��</param>
        /// <returns>1�ɹ�-1ʧ��</returns>
        public int InsertClinicDayBalance(Class.ClinicDayBalanceNew clicicDayBlanceNew)
        {
            if (this.Sql.GetSql("Local.Clinic.InsertDayBalanceDataNew", ref SQL) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL,
                                  clicicDayBlanceNew.BlanceNO,
                                  clicicDayBlanceNew.BeginTime.ToString(),
                                  clicicDayBlanceNew.EndTime.ToString(),
                                  clicicDayBlanceNew.TotCost,
                                  clicicDayBlanceNew.Oper.ID,
                                  clicicDayBlanceNew.Oper.Name,
                                  clicicDayBlanceNew.Oper.OperTime.ToString(),
                                  clicicDayBlanceNew.InvoiceNO.ID,
                                  clicicDayBlanceNew.InvoiceNO.Name,
                                  clicicDayBlanceNew.BegionInvoiceNO,
                                  clicicDayBlanceNew.EndInvoiceNo,
                                  clicicDayBlanceNew.FalseInvoiceNo,
                                  clicicDayBlanceNew.CancelInvoiceNo,
                                  clicicDayBlanceNew.TypeStr,
                                  clicicDayBlanceNew.SortID);

            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            return this.ExecNoQuery(SQL);
        }

        /// <summary>
        /// �����ս�����
        /// </summary>
        /// <param name="employeeID">�տ�Ա</param>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">��ֹʱ��</param>
        /// <returns></returns>
        public int GetDayBalanceRecord(string strSequence, ref DataSet dsResult)
        {
            if (this.Sql.GetSql("Local.Clinic.SelectDayBalanceRecord", ref SQL) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, strSequence);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref dsResult) == -1)
            {
                this.Err = "ִ��SQL���ʧ�ܣ�";
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// ��ȡ�ս��˷ѽ��
        /// </summary>
        /// <param name="employeeID">����Ա����</param>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">��ֹʱ��</param>
        /// <param name="cancelMoney">���ص��˷ѽ��</param>
        /// <returns>1�ɹ�-1ʧ��</returns>
        public int GetDayBalanceCancelMoney(string employeeID, string dateBegin, string dateEnd,ref decimal cancelMoney)
        {
            if (this.Sql.GetSql("Local.Clinic.GetDayInvoiceCancelMoney", ref SQL) == -1)
            {
                this.Err = "����Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                cancelMoney = NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
                return 1;
            }
            catch(Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ��ȡ�ս����Ͻ��
        /// </summary>
        /// <param name="employeeID">����Ա����</param>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">��ֹʱ��</param>
        /// <param name="falseMoney">���ص����Ͻ��</param>
        /// <returns>1�ɹ�-1ʧ��</returns>
        public int GetDayBalanceFalseMoney(string employeeID, string dateBegin, string dateEnd, ref decimal falseMoney)
        { 
            if (this.Sql.GetSql("Local.Clinic.GetDayInvoiceFalseMoney.Select", ref SQL) == -1)
            {
                this.Err = "����Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                falseMoney = NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
                return 1;
            }
            catch(Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ��ȡ�ս�����������
        /// </summary>
        /// <param name="employeeID">����Ա����</param>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">��ֹʱ��</param>
        /// <param name="modeMoney">��������������</param>
        /// <returns>1�ɹ�-1ʧ��</returns>
        public int GetDayBalanceModeMoney(string employeeID, string dateBegin, string dateEnd, ref decimal modeMoney)
        { 
            if (this.Sql.GetSql("Local.Clinic.GetDayModeMoney", ref SQL) == -1)
            {
                this.Err = "����Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
                modeMoney = NConvert.ToDecimal(this.ExecSqlReturnOne(SQL));
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// ��ȡ���ѡ�ʡ����ҽ�����
        /// </summary>
        /// <param name="employeeID">����Ա����</param>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">��ֹʱ��</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int GetDayBalanceProtectMoney(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.GetProtectMoney", ref SQL) == -1)
            {
                this.Err = "����Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                //1���������ݡ�2��סԺ����
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd,"1");
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "ִ��SQL���ʧ�ܣ�";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��ȡ����ҽ�����
        /// </summary>
        /// <param name="employeeID">����Ա����</param>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">��ֹʱ��</param>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int GetDayBalancePublicMoney(string employeeID, string dateBegin, string dateEnd,string pactCode, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.GetPublicMoney", ref SQL) == -1)
            {
                this.Err = "����Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                //1���������ݡ�2��סԺ����
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd,pactCode);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "ִ��SQL���ʧ�ܣ�";
                return -1;
            }
            return 1;
        }
        //{745DF4AC-4A2D-47e8-A4D1-8D8A80D6C2B8}
        /// <summary>
        /// ��ȡ������
        /// </summary>
        /// <param name="employeeID">����Ա����</param>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">��ֹʱ��</param>
        /// <param name="pactCode">��ͬ��λ</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int GetDayBalanceRebateMoney(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.GetRebateMoney", ref SQL) == -1)
            {
                this.Err = "����Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                //1���������ݡ�2��סԺ����
                this.SQL = string.Format(SQL, employeeID, dateBegin, dateEnd);
            }
            catch (Exception ex)
            {
                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "ִ��SQL���ʧ�ܣ�";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ��֧����ʽ���ҽ��
        /// </summary>
        /// <param name="employeeID">����Ա����</param>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">��ֹʱ��</param>
        /// <param name="ds">dataSet</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int GetDayBalancePayTypeMoney(string employeeID, string dateBegin, string dateEnd, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.GetPayTypeMoney", ref SQL) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL,
                                  employeeID,
                                  dateBegin,
                                  dateEnd);
            }
            catch (Exception ex)
            {

                this.Err = ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "ִ��SQL���ʧ�ܣ�";
                return -1;
            }
            return 1;
        }

        /// <summary>
        /// ���һ���������Ϣ
        /// </summary>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">����ʱ��</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int GetCollectDayBalanceInfo(string dateBegin, string dateEnd, ref List<Class.ClinicDayBalanceNew> list)
        {
            if (this.Sql.GetSql("Local.Clinic.GetCollectDayBalanceInfo", ref SQL) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, dateBegin, dateEnd);
                if (this.ExecQuery(SQL) == -1)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return -1;
                }
                Class.ClinicDayBalanceNew obj = null;
                while (this.Reader.Read())
                {
                    obj = new Report.OutpatientFee.Class.ClinicDayBalanceNew();
                    obj.BlanceNO = Reader[0].ToString();
                    obj.Oper.Name = Reader[1].ToString();
                    obj.BeginTime = NConvert.ToDateTime(Reader[2]);
                    obj.EndTime = NConvert.ToDateTime(Reader[3]);
                    obj.BegionInvoiceNO = Reader[4].ToString();
                    obj.EndInvoiceNo = Reader[5].ToString();
                    list.Add(obj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʧ�ܣ�" + ex.Message;
                return -1;
            }

        }   /// <summary>
        #region {A233C411-4B52-4831-AF89-8D7C2CE8D09E} �ս���ܼӲ�����
        /// ���һ���������Ϣ
        /// </summary>
        /// <param name="dateBegin">��ʼʱ��</param>
        /// <param name="dateEnd">����ʱ��</param>
        /// <param name="list"></param>
        /// <returns></returns>
        public int GetCheckedCollectDayBalanceInfo(string dateBegin, string dateEnd, ref List<Class.ClinicDayBalanceNew> list)
        {
            if (this.Sql.GetSql("Local.Clinic.GetCheckCollectDayBalanceInfo", ref SQL) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, dateBegin, dateEnd);
                if (this.ExecQuery(SQL) == -1)
                {
                    this.Err = "��������ʧ�ܣ�";
                    return -1;
                }
                Class.ClinicDayBalanceNew obj = null;
                while (this.Reader.Read())
                {
                    obj = new Report.OutpatientFee.Class.ClinicDayBalanceNew();
                    obj.BlanceNO = Reader[0].ToString();
                    obj.Oper.Name = Reader[1].ToString();
                    obj.BeginTime = NConvert.ToDateTime(Reader[2]);
                    obj.EndTime = NConvert.ToDateTime(Reader[3]);
                    obj.BegionInvoiceNO = Reader[4].ToString();
                    obj.EndInvoiceNo = Reader[5].ToString();
                    obj.Memo = Reader[6].ToString();
                    list.Add(obj);
                }
                return 1;
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʧ�ܣ�" + ex.Message;
                return -1;
            }

        } 
        #endregion

        /// <summary>
        /// �����ս��������
        /// </summary>
        ///<param name="balanceNos">�ս������</param>
        /// <param name="ds">DataSet</param>
        /// <returns>1���ɹ�-1ʧ��</returns>
        public int GetCollectDayBalanceData(string balanceNos, ref DataSet ds)
        {
            if (this.Sql.GetSql("Local.Clinic.SelecCollectDayBalanceData", ref SQL) == -1)
            {
                this.Err = "����Sql���ʧ�ܣ�";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, balanceNos);
            }
            catch (Exception ex)
            {
                this.Err = "��ʽ��SQL���ʧ�ܣ�" + ex.Message;
                return -1;
            }
            if (this.ExecQuery(SQL, ref ds) == -1)
            {
                this.Err = "ִ��SQL���ʧ�ܣ�";
                return -1;
            }
            return 1;
        }
        /// <summary>
        /// �����������
        /// </summary>
        /// <param name="operID">����˱���</param>
        /// <param name="operTime">���ʱ��</param>
        /// <param name="balanceNos">�������</param>
        /// <returns>1�ɹ� -1ʧ��</returns>
        public int SaveCollectData(string operID, DateTime operTime, string balanceNos)
        {
            if (this.Sql.GetSql("Local.Clinic.SaveDayBalanceCollectData", ref SQL) == -1)
            {
                this.Err = "����SQL���ʧ�ܣ�";
                return -1;
            }
            try
            {
                SQL = string.Format(SQL, operID, operTime.ToString(), balanceNos);
            }
            catch
            { 
                this.Err="��ʽ��SQL���ʧ�ܣ�";
                return -1;
            }
            return this.ExecNoQuery(SQL);
        }
        #endregion

    }
}
