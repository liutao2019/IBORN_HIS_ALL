using System;

namespace FS.HISFC.Models.IMA
{
	/// <summary>
	/// [��������: ҩƷ���ʹ���۸���Ϣ]<br></br>
	/// [�� �� ��: ��˹]<br></br>
	/// [����ʱ��: 2006-09-11]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
    [Serializable]
    public class PriceService:FS.FrameWork.Models.NeuObject
	{
		public PriceService()
		{

		}


		#region ����

		/// <summary>
		/// ���ۼ�
		/// </summary>
		private decimal retailPrice;

		/// <summary>
		/// ������
		/// </summary>
		private decimal wholeSalePrice;

		/// <summary>
		/// �����
		/// </summary>
		private decimal purchasePrice;

		/// <summary>
		/// ������ۼ�
		/// </summary>
		private decimal topRetailPrice;

		/// <summary>
		/// �۸����
		/// </summary>
		private decimal priceRate;

		/// <summary>
		/// ������ʽ
		/// </summary>
		private FS.FrameWork.Models.NeuObject priceForm = new FS.FrameWork.Models.NeuObject();

		#endregion

		/// <summary>
		/// ���ۼ�
		/// </summary>
		public decimal RetailPrice
		{
			get
			{
				return this.retailPrice;
			}
			set
			{
				this.retailPrice = value;
			}
		}


		/// <summary>
		/// ������
		/// </summary>
		public decimal WholeSalePrice
		{
			get
			{
				return this.wholeSalePrice;
			}
			set
			{
				this.wholeSalePrice = value;
			}
		}


		/// <summary>
		/// �����
		/// </summary>
		public decimal PurchasePrice
		{
			get
			{
				return this.purchasePrice;
			}
			set
			{
				this.purchasePrice = value;
			}
		}


		/// <summary>
		/// ������ۼ�
		/// </summary>
		public decimal TopRetailPrice
		{
			get
			{
				return this.topRetailPrice;
			}
			set
			{
				this.topRetailPrice = value;
			}
		}


		/// <summary>
		/// �۸����
		/// </summary>
		public decimal PriceRate
		{
			get
			{
				return this.priceRate;
			}
			set
			{
				this.priceRate = value;
			}
		}


		/// <summary>
		/// �۸���ʽ ���Ҷ��ۡ��б궨��
		/// </summary>
		public FS.FrameWork.Models.NeuObject PriceForm
		{
			get
			{
				return this.priceForm;
			}
			set
			{
				this.priceForm = value;
			}
		}


		#region ����

		/// <summary>
		/// ��¡���� 
		/// </summary>
		/// <returns>�ɹ����ؿ�¡��ʵ��</returns>
		public new PriceService Clone()
		{
			PriceService ps = base.Clone() as PriceService;

			ps.PriceForm = this.PriceForm.Clone();

			return ps;
		}


		#endregion
	}
}
