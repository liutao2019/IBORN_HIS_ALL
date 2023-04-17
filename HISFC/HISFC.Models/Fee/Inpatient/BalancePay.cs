using System;
using FS.FrameWork.Models;
using FS.HISFC.Models.Base;

namespace FS.HISFC.Models.Fee.Inpatient 
{

	/// <summary>
	/// BalancePayBase<br></br>
	/// [��������: סԺ֧����ʽ��]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-06]<br></br>
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
        /// Ժ��id
        /// </summary>
        private string hospital_id;

        /// <summary>
        /// Ժ����
        /// </summary>
        private string hospital_name;

		/// <summary>
		/// �������
		/// </summary>
		private int balanceNO;
		
		/// <summary>
		/// �������� ID: 0 Ԥ���� 1 �����
		/// </summary>
		private NeuObject transKind = new NeuObject();

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
        ///Ժ��id
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
		///�������� ID: 0 Ԥ���� 1 �����
		/// </summary>
		public NeuObject TransKind
		{
			get
			{
				return this.transKind;
			}
			set
			{
				this.transKind = value;
			}
		}

		/// <summary>
		/// �������
		/// </summary>
		public int BalanceNO
		{
			get
			{
				return this.balanceNO;
			}
			set
			{
				this.balanceNO = value;
			}
		}
       
		#endregion
		
        #region �������Ա���

		/// <summary>
		/// ��Ʊ��
		/// </summary>д�� ������ID���� �����з�Ʊ��Ϣ
		[Obsolete("����,�����Invoice.ID����", true)]
		public string InvoiceNo;
		
		
		/// <summary>
		/// ���
		/// </summary>ûд������ FT
		[Obsolete("����,����FT����", true)]
		public decimal Cost= 0m;

		/// <summary>
		/// ����ʱ��
		/// </summary>???ûд
		[Obsolete("����,����BalanceOper.OperTime����", true)]
		public System.DateTime DtBalance;
		/// <summary>
		/// �������
		/// </summary>

        [Obsolete("����,ʹ��BalanceNO����", true)]
		public int BalanceNo;
		
		#endregion
        
	    #region ����
        
		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ�����ʵ������</returns>
		public new BalancePay Clone()
		{
			return base.Clone() as BalancePay;
		}
		
		#endregion
        
		#endregion
	}
}
