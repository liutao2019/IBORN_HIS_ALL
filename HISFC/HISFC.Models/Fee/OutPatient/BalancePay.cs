using System;

namespace FS.HISFC.Models.Fee.Outpatient
{
	/// <summary>
	/// BalancePay<br></br>
	/// [��������: ����֧����ʽ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-13]<br></br>
	/// <�޸ļ�¼ 
	///		�޸���='' 
	///		�޸�ʱ��='yyyy-mm-dd' 
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    /// 
    [System.Serializable]
	public class BalancePay : BalancePayBase
	{
		#region ����

        /// <summary>
        /// {3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// Ժ����{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        private string hospital_name;
        

		/// <summary>
		/// ������ˮ��
		/// </summary>
		private string squence;

		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNO
		/// </summary>
		private string invoiceCombNO;

        /// <summary>
        /// �Ƿ����
        /// </summary>
        private bool isEmpPay = false;

        /// <summary>
        /// �������˺�
        /// </summary>
        private string accountNo = string.Empty;

        /// <summary>
        /// �����˻����ͱ���
        /// </summary>
        private string accountTypeCode = string.Empty;

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        private FS.HISFC.Models.RADT.PatientInfo patient = new FS.HISFC.Models.RADT.PatientInfo();

		#endregion

		#region ����

        /// <summary>
        /// ��������Ϣ
        /// </summary>
        public FS.HISFC.Models.RADT.PatientInfo EmpowerPatient
        {

            get
            {
                return this.patient;
            }
            set
            {
                this.patient = value;
            }
        }
        /// <summary>
        ///�Ƿ����
        /// </summary>
        public bool IsEmpPay
        {
            get
            {
                return this.isEmpPay;
            }
            set
            {
                this.isEmpPay = value;
            }
        }

        /// <summary>
        ///�������˺�
        /// </summary>
        public string AccountNo
        {
            get
            {
                return this.accountNo;
            }
            set
            {
                this.accountNo = value;
            }
        }

        /// <summary>
        ///�����˻����ͱ���
        /// </summary>
        public string AccountTypeCode
        {
            get
            {
                return this.accountTypeCode;
            }
            set
            {
                this.accountTypeCode = value;
            }
        }
		/// <summary>
		/// ������ˮ��
		/// </summary>
		public string Squence
		{
			get
			{
				return this.squence;
			}
			set
			{
				this.squence = value;
			}
		}

		/// <summary>
		/// ��Ʊ��ţ�һ�ν���������ŷ�Ʊ��combNO
		/// </summary>
		public string InvoiceCombNO
		{
			get
			{
				return this.invoiceCombNO;
			}
			set
			{
				this.invoiceCombNO = value;
			}
		}
        /// <summary>
        ///Ժ��id// {31E733E1-8329-4e47-A024-47BCD4C6976D} {3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// </summary>
        public string Hospital_id
        {
            get
            {
                return this.hospital_id;
            }
            set
            {
                this.hospital_id = value;
            }
        }

        // {31E733E1-8329-4e47-A024-47BCD4C6976D}{3C210DC7-ECCA-4b9d-81BB-B4E79F599C6D}
        /// <summary>
        /// Ժ����
        /// </summary>
        public string Hospital_name
        {
            get
            {
                return this.hospital_name;
            }
            set
            {
                this.hospital_name = value;
            }
        }
		#endregion

		#region ����
		
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ����ʵ��</returns>
		public new BalancePay Clone()
		{
			return base.Clone() as BalancePay;
		} 

		#endregion

		#endregion
	}
}
