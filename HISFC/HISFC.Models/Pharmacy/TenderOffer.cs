using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: �б���]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-10]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// ID    �б��ĺ�
	/// Name  ����ʱ���ã�
	/// </summary>
    [Serializable]
    public class TenderOffer : FS.FrameWork.Models.NeuObject
	{
		public TenderOffer()
		{

		}


		#region ����

		/// <summary>
		/// �Ƿ��б���ҩ
		/// </summary>
		private bool isTenderOffer;

		/// <summary>
		/// �б��
		/// </summary>
		private decimal price;

		/// <summary>
		/// �ɹ���ͬ��λ���
		/// </summary>
		private string contractNo;

		/// <summary>
		/// �ɹ���ʼ��ǰ
		/// </summary>
		private DateTime beginDate;

		/// <summary>
		/// �ɹ���������
		/// </summary>
		private DateTime endDate;

		/// <summary>
		/// �ɹ���λ
		/// </summary>
		private FS.FrameWork.Models.NeuObject company = new FS.FrameWork.Models.NeuObject();

		#endregion

		/// <summary>
		/// �Ƿ����б���ҩ
		/// </summary>
		public bool IsTenderOffer
		{
			get
			{
				return this.isTenderOffer;
			}
			set
			{
				this.isTenderOffer = value;
			}
		}


		/// <summary>
		/// �б��
		/// </summary>
		public decimal Price
		{
			get
			{
				return this.price;
			}
			set
			{
				this.price = value;
			}
		}


		/// <summary>
		/// �ɹ���ͬ���
		/// </summary>
		public string ContractNO
		{
			get
			{
				return this.contractNo;
			}
			set
			{
				this.contractNo = value;
			}
		}


		/// <summary>
		/// �ɹ���ʼ����
		/// </summary>
		public DateTime BeginTime
		{
			get
			{
				return this.beginDate;
			}
			set
			{
				this.beginDate = value;
			}
		}


		/// <summary>
		/// �ɹ���������
		/// </summary>
		public DateTime EndTime
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.endDate = value;
			}
		}


		/// <summary>
		/// �ɹ���λ
		/// </summary>
		public FS.FrameWork.Models.NeuObject Company
		{
			get
			{
				return this.company;
			}
			set
			{
				this.company = value;
			}
		}


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ��� </returns>
		public new TenderOffer Clone()
		{
			TenderOffer tenderOffer = base.Clone() as TenderOffer;

			tenderOffer.company = this.company.Clone();

			return tenderOffer;
		}


		#endregion

		#region ��Ч����

		/// <summary>
		/// �ɹ���ͬ��λ���
		/// </summary>
		private string contractCode;

		/// <summary>
		/// �ɹ���ͬ���
		/// </summary>
		[System.Obsolete("�����ع� ����ΪContractNO����")]
		public string ContractCode
		{
			get
			{
				return this.contractCode;
			}
			set
			{
				this.contractCode = value;
			}
		}


		/// <summary>
		/// �ɹ���ʼ����
		/// </summary>
		[System.Obsolete("�������� ����ΪBeginTime����",true)]
		public DateTime BeginDate
		{
			get
			{
				return this.beginDate;
			}
			set
			{
				this.beginDate = value;
			}
		}


		/// <summary>
		/// �ɹ���������
		/// </summary>
		[System.Obsolete("�������� ����ΪEndTime����",true)]
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.endDate = value;
			}
		}


		#endregion
	}
}
