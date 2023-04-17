using System;

namespace FS.HISFC.Models.Pharmacy
{
	/// <summary>
	/// [��������: �б��ı���]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2004-12]<br></br>
	/// <�޸ļ�¼
	///		�޸���='������'
	///		�޸�ʱ��='2006-09-13'
	///		�޸�Ŀ��='ϵͳ�ع�'
	///		�޸�����='�����淶���� '
	///  />
	///  ID �б��ĺ�
	/// </summary>
    [Serializable]
    public class InviteBidding : FS.FrameWork.Models.NeuObject
	{
		public InviteBidding()
		{
			
		}


		#region ����

		private bool isInviteBidding;

		private decimal price;

		private string contractNo;

		private DateTime beginTime;

		private DateTime endTime;

		#endregion


		/// <summary>
		/// �Ƿ����б���ҩ
		/// </summary>
		public bool IsInviteBidding
		{
			get
			{
				return this.isInviteBidding;
			}
			set
			{
				this.isInviteBidding = value;
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
				return this.beginTime;
			}
			set
			{
				this.beginTime = value;
			}
		}


		/// <summary>
		/// �ɹ���������
		/// </summary>
		public DateTime EndTime
		{
			get
			{
				return this.endTime;
			}
			set
			{
				this.endTime = value;
			}
		}


		#region ����

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns>�ɹ����ص�ǰʵ���ĸ���</returns>
		public new InviteBidding Clone()
		{
			return base.Clone() as InviteBidding;
		}

		#endregion

		#region ��Ч����

		/// <summary>
		/// �ɹ���ͬ���
		/// </summary>
		[System.Obsolete("�����ع� ����ΪContractNO����",true)]
		public string ContractCode;

		/// <summary>
		/// �ɹ���ʼ����
		/// </summary>
		[System.Obsolete("�����ع� ����ΪBeginTime����",true)]
		public DateTime BeginDate;

		/// <summary>
		/// �ɹ���������
		/// </summary>
		[System.Obsolete("�����ع� ����ΪEndTime����",true)]
		public DateTime EndDate;

		#endregion
	}
}
