using System;
using System.Data;

namespace Neusoft.HISFC.Management.Order {
	/// <summary>
	/// Report ��ժҪ˵����
	/// ҽ�������ѯ������
	/// </summary>
	public class Report : Neusoft.NFC.Management.Database {
		/// <summary>
		/// ���ݻ���סԺ��ˮ�ţ���ѯҩƷҽ��ִ�е���Ϣ
		/// writed by cupeng
		/// 2005-06
		/// </summary>
		/// <param name="inpatientNo">����סԺ��ˮ��</param>
		/// <returns></returns>
		public DataSet ExecDrugByInpatientNo(string inpatientNo) {
			string strSql ="";  
			DataSet dataSet = new DataSet();

			//ȡSQL���
			if (this.Sql.GetSql("Order.Report.ExecDrugByInpatientNo",ref strSql) == -1) {
				this.Err="û���ҵ�Order.Report.ExecDrugByInpatientNo�ֶ�!";
				return null;
			}
			try {  
				strSql=string.Format(strSql, inpatientNo);    //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err="����ֵʱ�����Order.Report.ExecDrugByInpatientNo��"+ex.Message;
				this.WriteErr();
				return null;
			}

			//����SQL���ȡ��ѯ���
			if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

			return dataSet;
		}

		
		/// <summary>
		/// ���ݻ���סԺ��ˮ�ţ���ѯ��ҩƷҽ��ִ�е���Ϣ
		/// writed by cupeng
		/// 2005-06
		/// </summary>
		/// <param name="inpatientNo">����סԺ��ˮ��</param>
		/// <returns></returns>
		public DataSet ExecUndrugByInpatientNo(string inpatientNo) {
			string strSql ="";  
			DataSet dataSet = new DataSet();

			//ȡSQL���
			if (this.Sql.GetSql("Order.Report.ExecUndrugByInpatientNo",ref strSql) == -1) {
				this.Err="û���ҵ�Order.Report.ExecUndrugByInpatientNo�ֶ�!";
				return null;
			}
			try {  
				strSql=string.Format(strSql, inpatientNo);    //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err="����ֵʱ�����Order.Report.ExecUndrugByInpatientNo��"+ex.Message;
				this.WriteErr();
				return null;
			}

			//����SQL���ȡ��ѯ���
			if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

			return dataSet;
		}


		/// <summary>
		/// ����סԺ��ˮ�š�ҩƷ���롢ʱ��μ��������ۼ���ҩ���  
		/// writed by liangjz 2005-06
		/// </summary>
		/// <param name="inpatientNo">סԺ��ˮ��</param>
		/// <param name="drugCode">ҩƷ����</param>
		/// <param name="myBeginTime">��ʼʱ��</param>
		/// <param name="myEndTime">��ֹʱ��</param>
		/// <returns>dataset</returns>
		public DataSet TotalUseDrug(string inpatientNo,string drugCode,DateTime myBeginTime,DateTime myEndTime) {
			string strSql = "";
			DataSet dataSet = new DataSet();
			
			//ȡSQL���
			if (this.Sql.GetSql("Order.Report.TotalUseDrug",ref strSql) == -1) {
				this.Err="û���ҵ�Order.Report.TotalUseDrug�ֶ�!";
				return null;
			}
			try {  
				strSql=string.Format(strSql, inpatientNo,drugCode,myBeginTime.ToString(),myEndTime.ToString());    //�滻SQL����еĲ�����
			}
			catch(Exception ex) {
				this.Err="����ֵʱ�����Order.Report.TotalUseDrug��"+ex.Message;
				this.WriteErr();
				return null;
			}

			//����SQL���ȡ��ѯ���
			if (this.ExecQuery(strSql, ref dataSet) == -1) return null;

			return dataSet;			
		}
        /// <summary>
        /// ����ʱ�䣬��С���ã����Ҳ�ѯ�շѵ�ҩƷ��Ϣ
        /// </summary>
        /// <param name="minFee"></param>
        /// <param name="deptCode"></param>
        /// <param name="dtBegin"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
		public DataSet QueryChargedMedicine(string minFee,string deptCode,string dtBegin,string dtEnd) {
			string strSql = "";
			DataSet dsMedicine = new DataSet();
			if(this.Sql.GetSql("Fee.Item.QueryChargedMedicine",ref strSql) == -1) {
			    this.Err = "Can't Find Sql";
				return null;
			}
			strSql = System.String.Format(strSql,minFee,deptCode,dtBegin,dtEnd);
			this.ExecQuery(strSql,ref dsMedicine);
			return dsMedicine;
		}
		/// <summary>
		/// ����ʱ�䣬��С���ã����Ҳ�ѯ�շѵķ�ҩƷ��Ϣ
		/// </summary>
		/// <param name="minFee"></param>
		/// <param name="deptCode"></param>
		/// <param name="dtBegin"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public DataSet QueryChargedItem(string minFee,string deptCode,string dtBegin,string dtEnd) {
			string strSql = "";
			DataSet dsItem = new DataSet();
			if(this.Sql.GetSql("Fee.Item.QueryChargedItem",ref strSql) == -1) {
				this.Err = "Can't Find Sql";
				return null;
			}
			strSql = System.String.Format(strSql,minFee,deptCode,dtBegin,dtEnd);
			this.ExecQuery(strSql,ref dsItem);
			return dsItem;
		}
		/// <summary>
		/// ����ʱ�䣬�����ѯ�շѵ�ҩƷ��ϸ��Ϣ
		/// </summary>
		/// <param name="code"></param>
		/// <param name="dtBegin"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public DataSet QueryChargedMedicineDetail(string code,string deptCode,string dtBegin,string dtEnd) {
			string strSql = "";
			DataSet dsMedicine = new DataSet();
			if(this.Sql.GetSql("Fee.Item.QueryChargedMedicineDetail",ref strSql) == -1) {
				this.Err = "Can't Find Sql";
				return null;
			}
			strSql = System.String.Format(strSql,code,deptCode,dtBegin,dtEnd);
			this.ExecQuery(strSql,ref dsMedicine);
			return dsMedicine;
		}
		/// <summary>
		/// ����ʱ�䣬�����ѯ�շѵķ�ҩƷ��ϸ��Ϣ
		/// </summary>
		/// <param name="code"></param>
		/// <param name="dtBegin"></param>
		/// <param name="dtEnd"></param>
		/// <returns></returns>
		public DataSet QueryChargedItemDetail(string code,string deptCode,string dtBegin,string dtEnd) {
			string strSql = "";
			DataSet dsItem = new DataSet();
			if(this.Sql.GetSql("Fee.Item.QueryChargedItemDetail",ref strSql) == -1) {
				this.Err = "Can't Find Sql";
				return null;
			}
			strSql = System.String.Format(strSql,code,deptCode,dtBegin,dtEnd);
			this.ExecQuery(strSql,ref dsItem);
			return dsItem;
		}
	}
}
