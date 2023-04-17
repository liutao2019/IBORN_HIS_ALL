using System;
using System.Data;

namespace FS.HISFC.BizLogic.Pharmacy {
	/// <summary>
	/// Report ��ժҪ˵����
	/// </summary>
    public class Report : DataBase
    {
		public Report() {
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ��ҩ���ܲ�ѯ
		/// ����outType���ж����������סԺ��ҩ
		/// </summary>
		/// <param name="dateBegin">��ʼʱ��</param>
		/// <param name="dateEnd">��ֹʱ��</param>
		/// <param name="drugDeptCode">ҩ������</param>
		/// <param name="drugQuality">ҩƷ���ʣ���ΪAAʱ����ѯȫ����</param>
		/// <param name="outType">"M"�����ҩ��"Z"סԺ��ҩ</param>
		/// <returns></returns>
		public DataSet DrugTotal(DateTime dateBegin, DateTime dateEnd, string drugDeptCode, string drugQuality, string outType) {
			string strSql ="";  
			DataSet dataSet = new DataSet();

			//ȡSQL���
			if (this.GetSQL("Pharmacy.Report.DrugTotal",ref strSql) == -1) {
				this.Err="û���ҵ�Pharmacy.Report.DrugTotal�ֶ�!";
				return null;
			}
			try {  
				strSql=string.Format(strSql, dateBegin.ToString(), dateEnd.ToString(), drugDeptCode, drugQuality, outType);    //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err="����ֵʱ�����Pharmacy.Report.DrugTotal��"+ex.Message;
				this.WriteErr();
				return null;
			}

			//����SQL���ȡ��ѯ���
			if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

			return dataSet;
		}


		/// <summary>
		/// סԺҩ����ҩ���ܲ�ѯ
		/// ����outType���ж����������סԺ��ҩ
		/// </summary>
		/// <param name="dateBegin">��ʼʱ��</param>
		/// <param name="dateEnd">��ֹʱ��</param>
		/// <param name="drugDeptCode">ҩ������</param>
		/// <param name="drugQuality">ҩƷ���ʣ���ΪAAʱ����ѯȫ����</param>
		/// <returns></returns>
		public DataSet InpatientDrugTotal(DateTime dateBegin, DateTime dateEnd, string drugDeptCode, string drugQuality) {
			return this.DrugTotal(dateBegin, dateEnd, drugDeptCode, drugQuality, "Z");
		}


		/// <summary>
		/// ��ҩ��ϸ��ѯ
		/// ����outType���ж����������סԺ��ҩ
		/// </summary>
		/// <param name="dateBegin">��ʼʱ��</param>
		/// <param name="dateEnd">��ֹʱ��</param>
		/// <param name="drugDeptCode">ҩ������</param>
		/// <param name="drugCode">ҩƷ����</param>
		/// <param name="outType">"M"�����ҩ��"Z"סԺ��ҩ</param>
		/// <returns></returns>
		public DataSet DrugDetail(DateTime dateBegin, DateTime dateEnd, string drugDeptCode, string drugCode, string outType) {
			string strSql ="";  
			DataSet dataSet = new DataSet();

			//ȡSQL���
			if (this.GetSQL("Pharmacy.Report.DrugDetail",ref strSql) == -1) {
				this.Err="û���ҵ�Pharmacy.Report.DrugDetail�ֶ�!";
				return null;
			}
			try {  
				strSql=string.Format(strSql, dateBegin.ToString(), dateEnd.ToString(), drugDeptCode, drugCode, outType);    //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err="����ֵʱ�����Pharmacy.Report.DrugDetail��"+ex.Message;
				this.WriteErr();
				return null;
			}

			//����SQL���ȡ��ѯ���
			if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

			return dataSet;
		}


		/// <summary>
		/// סԺҩ����ҩ��ϸ��ѯ
		/// </summary>
		/// <returns></returns>
		public DataSet InpatientDrugDetail(DateTime dateBegin, DateTime dateEnd, string drugDeptCode, string drugCode) {
			return this.DrugDetail(dateBegin, dateEnd, drugDeptCode, drugCode, "Z");
		}           	

		/// <summary>
		/// ҩ���ۺϲ�ѯ����
		/// </summary>
		/// <param name="deptCode">�ⷿ����</param>
		/// <param name="myBeginTime">��ʼʱ��</param>
		/// <param name="myEndTime">��ֹʱ��</param>
		/// <param name="privCode">Ȩ�ޱ���</param>
		/// <param name="SQLString">��ȡ��SQL</param>
		/// <returns>������null���ɹ�����dataSet</returns>
        public DataSet PharmacyReportQueryBase(string deptCode, DateTime myBeginTime, DateTime myEndTime, string privCode, string SQLString)
        {
            string strSql = "";
            DataSet dataSet = new DataSet();

            //ȡSQL���
            if (this.GetSQL("Pharmacy.Report." + SQLString, ref strSql) == -1)
            {
                this.Err = "û���ҵ�Pharmacy.Report." + SQLString + "�ֶΣ�";
                return null;
            }
            try
            {
                strSql = string.Format(strSql, deptCode, myBeginTime.ToString(), myEndTime.ToString(), privCode);
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����Pharmacy.Report." + SQLString + ex.Message;
                this.WriteErr();
                return null;
            }
            //����SQL���ȡ��ѯ���
            if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

            return dataSet;
        }

        /// <summary>
        /// ҩ���ۺϲ�ѯ����
        /// </summary>
        /// <param name="sql">��ȡ��SQL</param>
        /// <param name="param">��չ����</param>
        /// <returns>������null���ɹ�����dataSet</returns>
        public DataSet PharmacyReport(string sql, params string[] param)
        {
            string strSql = "";
            DataSet dataSet = new DataSet();
           
            try
            {                
                strSql = string.Format(sql, param);
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����Pharmacy.Report." + sql + ex.Message;
                this.WriteErr();
                return null;
            }
            //����SQL���ȡ��ѯ���
            if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

            return dataSet;
        }

        /// <summary>
        /// ҩ���ۺϲ�ѯ����
        /// </summary>
        /// <param name="sqlIndex">��ȡ��SQL����</param>
        /// <param name="param">��չ����</param>
        /// <returns>������null���ɹ�����dataSet</returns>
        public DataSet PharmacyReport(string[] sqlIndex, params string[] param)
        {
            string sqlSelect = "";

            //ȡSQL���
            if (this.GetSQL(sqlIndex[0], ref sqlSelect) == -1)
            {
                this.Err = "û���ҵ�" + sqlIndex[0] + "�ֶΣ�";
                return null;
            }

            if (sqlIndex.Length > 1)
            {
                for (int i = 1; i < sqlIndex.Length; i++)
                {
                    string strWhere = "";
                    //ȡSQL���
                    if (this.GetSQL(sqlIndex[i], ref strWhere) == -1)
                    {
                        this.Err = "û���ҵ�" + sqlIndex[i] + "�ֶΣ�";
                        return null;
                    }

                    sqlSelect +=" " + strWhere;
                }
            }

            return this.PharmacyReport(sqlSelect,param);

        }

        /// <summary>
        /// ��Ŀ��ѯ����
        /// </summary>
        /// <param name="SQLString"></param>
        /// <param name="SQLStrAddOne"></param>
        /// <param name="SQLStrAddTwo"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public DataSet PharmacyReportQueryBaseOne(string SQLString, string SQLStrAddOne, string SQLStrAddTwo, params string[] parms)
        {
            string strSql = "";
            string strSqlOne = "";
            string strSqlTwo = "";
            DataSet dataSet = new DataSet();

            //ȡSQL���
            if (this.Sql.GetSql(SQLString, ref strSql) == -1)
            {
                this.Err = SQLString + "�ֶΣ�";
                return null;
            }
            //ȡSQL�������1
            if (this.Sql.GetSql(SQLStrAddOne, ref strSqlOne) == -1)
            {
                strSqlOne = "";
            }
            //ȡSQL�������2
            if (this.Sql.GetSql(SQLStrAddTwo, ref strSqlTwo) == -1)
            {
                strSqlTwo = "";
            }
            else
            {
                strSqlTwo = string.Format(strSqlTwo, FS.FrameWork.Function.NConvert.ToInt32(parms[parms.Length - 1]));
            }
            //strSqlOne\strSqlTwo�����Ϊ������ԭstrSql����
            if (strSqlOne != "")
            {
                strSql = strSql + " " + strSqlOne;
            }
            if (strSqlTwo != "")
            {
                strSql = "select * from (" + strSql + ") a" + strSqlTwo;
            }
            try
            {
                strSql = string.Format(strSql, parms);
            }
            catch (Exception ex)
            {
                this.Err = "����ֵʱ�����Pharmacy.Report." + SQLString + ex.Message;
                this.WriteErr();
                return null;
            }
            //����SQL���ȡ��ѯ���
            if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

            return dataSet;
        }
      
        /*  ���ú��� ����

    /// <summary>
    /// ҩƷ�ɹ����ܲ�ѯ
    /// </summary>
    /// <param name="deptCode">�ⷿ����</param>
    /// <param name="myBeginTime">�ƻ���ʼʱ��</param>
    /// <param name="myEndTime">�ƻ���ֹʱ��</param>
    /// <param name="privCode">Ȩ�ޱ���</param>
    /// <returns></returns>
    public DataSet PharmacyStockTotal(string deptCode,DateTime myBeginTime,DateTime myEndTime,string privCode) {
        string strSql ="";  
        DataSet dataSet = new DataSet();

        //ȡSQL���
        if (this.GetSQL("Pharmacy.Report.PharmacyStockTotal",ref strSql) == -1) {
            this.Err="û���ҵ�Pharmacy.Report.PharmacyStockTotal�ֶ�!";
            return null;
        }
        try {  
            strSql=string.Format(strSql, deptCode,myBeginTime.ToString(), myEndTime.ToString());    //�滻SQL����еĲ�����
        }
        catch(Exception ex) {
            this.Err="����ֵʱ�����Pharmacy.Report.PharmacyStockTotal��"+ex.Message;
            this.WriteErr();
            return null;
        }

        //����SQL���ȡ��ѯ���
        if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

        return dataSet;
    }

        
    /// <summary>
    /// ҩƷ�ɹ����ܲ�ѯ��������
    /// </summary>
    /// <param name="deptCode">�ⷿ����</param>
    /// <param name="myBeginTime">�ƻ���ʼʱ��</param>
    /// <param name="myEndTime">�ƻ���ֹʱ��</param>
    /// <param name="privCode">Ȩ�ޱ���</param>
    /// <returns></returns>
    public DataSet PharmacyStockBillTotal(string deptCode,DateTime myBeginTime,DateTime myEndTime,string privCode) {
        string strSql = "";
        DataSet dataSet = new DataSet();

        //ȡSQl���
        if (this.GetSQL("Pharmacy.Report.PharmacyStockBillTotal",ref strSql) == -1) {
            this.Err = "û���ҵ�Pharmacy.Report.PharmacyStockBillTotal�ֶ�!";
            return null;
        }
        try{
            strSql=string.Format(strSql, deptCode,myBeginTime.ToString(), myEndTime.ToString());    //�滻SQL����еĲ�����
        }
        catch (Exception ex) {
            this.Err="����ֵʱ�����Pharmacy.Report.PharmacyStockBillTotal��"+ex.Message;
            this.WriteErr();
            return null;
        }
        //����SQL���ȡ��ѯ���
        if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

        return dataSet;
    }

    /// <summary>
    /// ҩƷ�ɹ����ܲ�ѯ����������˾
    /// </summary>
    /// <param name="deptCode">�ⷿ����</param>
    /// <param name="myBeginTime">�ƻ���ʼʱ��</param>
    /// <param name="myEndTime">�ƻ���ֹʱ��</param>
    /// <param name="privCode">Ȩ�ޱ���</param>
    /// <returns></returns>
    public DataSet PharmacyStockCompanyTotal(string deptCode,DateTime myBeginTime,DateTime myEndTime,string privCode) {
        string strSql = "";
        DataSet dataSet = new DataSet();

        //ȡSQl���
        if (this.GetSQL("Pharmacy.Report.PharmacyStockCompanyTotal",ref strSql) == -1) {
            this.Err = "û���ҵ�Pharmacy.Report.PharmacyStockCompanyTotal�ֶ�!";
            return null;
        }
        try{
            strSql=string.Format(strSql, deptCode,myBeginTime.ToString(), myEndTime.ToString());    //�滻SQL����еĲ�����
        }
        catch (Exception ex) {
            this.Err="����ֵʱ�����Pharmacy.Report.PharmacyStockCompanyTotal��"+ex.Message;
            this.WriteErr();
            return null;
        }
        //����SQL���ȡ��ѯ���
        if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

        return dataSet;
    }
        
		
    /// <summary>
    /// ȡҩƷ�ɹ���ϸ��Ϣ
    /// </summary>
    /// <param name="deptCode">�ⷿ����</param>
    /// <param name="myBeginTime">�ƻ���ʼʱ��</param>
    /// <param name="myEndTime">�ƻ���ֹʱ��</param>
    /// <param name="privCode">Ȩ�ޱ���</param>
    /// <returns></returns>
    public DataSet PharmacyStockDetail(string deptCode,DateTime myBeginTime,DateTime myEndTime,string privCode) {
        string strSql = "";
        DataSet dataSet = new DataSet();

        //ȡSQl���
        if (this.GetSQL("Pharmacy.Report.PharmacyStockDetail",ref strSql) == -1) {
            this.Err = "û���ҵ�Pharmacy.Report.PharmacyStockDetail�ֶ�!";
            return null;
        }
        try{
            strSql=string.Format(strSql, deptCode,myBeginTime.ToString(), myEndTime.ToString());    //�滻SQL����еĲ�����
        }
        catch (Exception ex) {
            this.Err="����ֵʱ�����Pharmacy.Report.PharmacyStockDetail��"+ex.Message;
            this.WriteErr();
            return null;
        }
        //����SQL���ȡ��ѯ���
        if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

        return dataSet;
    }

		
    /// <summary>
    /// ȡ��ҩҩƷ������Ϣ
    /// </summary>
    /// <param name="deptCode">������ұ���</param>
    /// <param name="myBeginTime">��ʼʱ��</param>
    /// <param name="myEndTime">��ֹʱ��</param>
    /// <param name="privCode">��ҩ���� M �����ҩ,Z סԺ��ҩ</param>
    /// <returns></returns>
    public DataSet PharmacyInPatientByDrug(string deptCode,DateTime myBeginTime,DateTime myEndTime,string privCode) {
        string strSql = "";
        DataSet dataSet = new DataSet();

        //ȡSQl���
        if (this.GetSQL("Pharmacy.Report.PharmacyInPatientByDrug",ref strSql) == -1) {
            this.Err = "û���ҵ�Pharmacy.Report.PharmacyInPatientByDrug�ֶ�!";
            return null;
        }
        try{
            strSql=string.Format(strSql, deptCode,myBeginTime.ToString(), myEndTime.ToString(),privCode);    //�滻SQL����еĲ�����
        }
        catch (Exception ex) {
            this.Err="����ֵʱ�����Pharmacy.Report.PharmacyInPatientByDrug��"+ex.Message;
            this.WriteErr();
            return null;
        }
        //����SQL���ȡ��ѯ���
        if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

        return dataSet;
    }


    /// <summary>
    /// ȡ��ҩ��ҩ���һ�����Ϣ
    /// </summary>
    /// <param name="deptCode">������ұ���</param>
    /// <param name="myBeginTime">��ʼʱ��</param>
    /// <param name="myEndTime">��ֹʱ��</param>
    /// <param name="privCode">��ҩ���� M �����ҩ,Z סԺ��ҩ</param>
    /// <returns></returns>
    public DataSet PharmacyInPatientByCompany(string deptCode,DateTime myBeginTime,DateTime myEndTime,string privCode) {
        string strSql = "";
        DataSet dataSet = new DataSet();

        //ȡSQl���
        if (this.GetSQL("Pharmacy.Report.PharmacyInPatientByCompany",ref strSql) == -1) {
            this.Err = "û���ҵ�Pharmacy.Report.PharmacyInPatientByCompany�ֶ�!";
            return null;
        }
        try{
            strSql=string.Format(strSql, deptCode,myBeginTime.ToString(), myEndTime.ToString(),privCode);    //�滻SQL����еĲ�����
        }
        catch (Exception ex) {
            this.Err="����ֵʱ�����Pharmacy.Report.PharmacyInPatientByCompany��"+ex.Message;
            this.WriteErr();
            return null;
        }
        //����SQL���ȡ��ѯ���
        if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

        return dataSet;
    }


    /// <summary>
    /// ȡ��ҩ��ϸ��Ϣ
    /// </summary>
    /// <param name="deptCode">������ұ���</param>
    /// <param name="myBeginTime">��ʼʱ��</param>
    /// <param name="myEndTime">��ֹʱ��</param>
    /// <param name="privCode">��ҩ���� M �����ҩ,Z סԺ��ҩ</param>
    /// <returns></returns>
    public DataSet PharmacyInPatientByDetail(string deptCode,DateTime myBeginTime,DateTime myEndTime,string privCode) {
        string strSql = "";
        DataSet dataSet = new DataSet();

        //ȡSQl���
        if (this.GetSQL("Pharmacy.Report.PharmacyInPatientByDetail",ref strSql) == -1) {
            this.Err = "û���ҵ�Pharmacy.Report.PharmacyInPatientByDetail�ֶ�!";
            return null;
        }
        try{
            strSql=string.Format(strSql, deptCode,myBeginTime.ToString(), myEndTime.ToString(),privCode);    //�滻SQL����еĲ�����
        }
        catch (Exception ex) {
            this.Err="����ֵʱ�����Pharmacy.Report.PharmacyInPatientByDetail��"+ex.Message;
            this.WriteErr();
            return null;
        }
        //����SQL���ȡ��ѯ���
        if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

        return dataSet;
    }
		
		
    /// <summary>
    /// ��ҩƷ����̨����Ϣ�����롢�����̵㡢����
    /// </summary>
    /// <param name="deptCode">�ⷿ����</param>
    /// <param name="myBeginTime">��ʼʱ��</param>
    /// <param name="myEndTime">��ֹʱ��</param>
    /// <param name="privCode">Ȩ�ޱ���</param>
    /// <returns></returns>
    public DataSet PharmacyRecordByDrug(string deptCode,DateTime myBeginTime,DateTime myEndTime,string privCode) {
        string strSql = "";
        DataSet dataSet = new DataSet();

        //ȡSQL���
        if (this.GetSQL("Pharmacy.Report.PharmacyRecordByDrug",ref strSql) == -1) {
            this.Err = "û���ҵ�Pharmacy.Report.PharmacyRecordByDrug�ֶ�!";
            return null;
        }
        try{
            strSql=string.Format(strSql,deptCode,myBeginTime.ToString(), myEndTime.ToString());    //�滻SQL����еĲ�����
        }
        catch (Exception ex) {
            this.Err="����ֵʱ�����Pharmacy.Report.PharmacyRecordByDrug��"+ex.Message;
            this.WriteErr();
            return null;
        }
        //����SQL���ȡ��ѯ���
        if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

        return dataSet;
    }
         * 
         * 		/// <summary>
		/// �̨ⷿ����ϸ��ѯ
		/// </summary>
		/// <param name="deptCode">�ⷿ����</param>
		/// <param name="myBeginTime">��ʼʱ��</param>
		/// <param name="myEndTime">��ֹʱ��</param>
		/// <param name="drugCode">ҩƷ����</param>
		/// <returns></returns>        
		public DataSet PharmacyRecordByDetail(string deptCode,DateTime myBeginTime,DateTime myEndTime,string drugCode) {
			string strSql = "";
			DataSet dataSet = new DataSet();

			//ȡSQL���
			if (this.GetSQL("Pharmacy.Report.PharmacyRecordByDetail",ref strSql) == -1) {
				this.Err = "û���ҵ�Pharmacy.Report.PharmacyRecordByDetail�ֶ�!";
				return null;
			}
			try{
				strSql=string.Format(strSql,deptCode,myBeginTime.ToString(), myEndTime.ToString(),drugCode);    //�滻SQL����еĲ�����
			}
			catch (Exception ex) {
				this.Err="����ֵʱ�����Pharmacy.Report.PharmacyRecordByDetail��"+ex.Message;
				this.WriteErr();
				return null;
			}
			//����SQL���ȡ��ѯ���
			if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

			return dataSet;
		}

    */

    }
}
