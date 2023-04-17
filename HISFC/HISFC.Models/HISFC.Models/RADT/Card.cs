using FS.FrameWork.Models;

namespace FS.HISFC.Models.RADT
{
	/// <summary>
	/// [��������: ���ߴſ�ʵ��]<br></br>
	/// [�� �� ��: ���Ʒ�]<br></br>
	/// [����ʱ��: 2006-09-05]<br></br>
	/// <�޸ļ�¼
	///		�޸���='����ΰ'
	///		�޸�ʱ��='2006-9-12'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary> 
    [System.Serializable]
    public class Card:NeuObject
	{

		/// <summary>
		/// ���캯��
		/// </summary>
		public Card()
		{
		}
		
		#region ����

		/// <summary>
		/// new����
		/// </summary>
		private string newPassword;

		/// <summary>
		/// ������
		/// </summary>
		private string oldPassword;

		/// <summary>
		/// iccard
		/// </summary>
		private NeuObject iCCard = new NeuObject();

		/// <summary>
		/// ԭ�˺Ž��
		/// </summary>
		private decimal oldAmount;

		/// <summary>
		/// ���˺Ž��
		/// </summary>
		private decimal newAmount;

		#endregion

		#region ����
		/// <summary>
		/// ������
		/// </summary>
		public string NewPassword
		{
			get
			{
				return this.newPassword  ;
			}
			set
			{
				this.newPassword = value;
			}
		}

		/// <summary>
		/// ������
		/// </summary>
		public string OldPassword
		{
			get
			{
				return this.oldPassword;
			}
			set
			{
				 this.oldPassword = value;
			}
	    }

        /// <summary>
        /// ����ҽ��֤
        /// </summary>
		public NeuObject ICCard
		{
			get
			{
				return this.iCCard  ;
			}
			set
			{
				this.iCCard = value ;
			}
	    }
		
		/// <summary>
		/// ���˻����
		/// </summary>
		public decimal NewAmount
		{
			get
			{
				return this.newAmount;
			}
			set
			{
				this.newAmount = value;
			}
		}
		
		/// <summary>
		/// ���˻����
		/// </summary>
		public decimal OldAmount
		{
			get
			{
				return this.oldAmount;
			}
			set
			{
				this.oldAmount = value;
			}
		}
		#endregion

		#region ����

		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new Card Clone()
		{
			Card card= base.Clone() as Card;
			card.ICCard = this.ICCard.Clone();
			return card;
		}

		#endregion
	}
}
