using System;
using System.Timers;
namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// ��ͬ��λ���� 
	/// </summary>
	public class PactUnitInfo :NFC.Object.NeuObject,Neusoft.HISFC.Object.Base.ISort
	{
		//id��ͬ��λ����
		//name ��ͬ��λ����
		//�������
		public PayKind  PayKind = new PayKind();
		/// <summary>
		/// 
		/// �۸���ʽ
		/// </summary>
		public string  PriceForm ;
		/// <summary>
		/// ����
		/// </summary>
		public FTRate   FTRate = new FTRate();
		/// <summary>
		/// �Ƿ�Ҫ�������ҽ��֤��
		/// </summary>
		public string  IsNeedMcard;
		/// <summary>
		/// �Ƿ��ܼ��
		/// </summary>
		public string  IsInControl;
		/// <summary>
		/// ��Ŀ����� 0 ȫ����1 ҩƷ��2 ��ҩƷ
		/// </summary>
		public string  ItemType ;
		/// <summary>
		/// ���޶�
		/// </summary>
		public decimal LimitDay;
		/// <summary>
		/// ���޶�
		/// </summary>
		public decimal LimitMonth;
		//���޶�
		public decimal LimitYear;
		//һ���޶�
		public decimal LimitOnce;

		//��λ��׼
		public  decimal BedLimit ;
		//�յ���׼
		public decimal AirLimit;
		//˳���
		protected int sortid;
		//��ͬ��λ��� 
		public string sampleName;

		public PactUnitInfo()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		public  new PactUnitInfo Clone()
		{
			PactUnitInfo info = (PactUnitInfo) base.Clone();
			info.ID = this.ID;
			info.Name = this.Name;
			info.PayKind = this.PayKind.Clone();
			info.IsNeedMcard = this.IsNeedMcard;;
			info.IsInControl = this.IsInControl;
			info.ItemType = this.ItemType;
			info.LimitDay = this.LimitDay;
			info.LimitYear = this.LimitYear;
			info.LimitMonth = this.LimitMonth;
			info.LimitOnce = this.LimitOnce ;
			info.sortid = this.sortid;
			info.sampleName = sampleName;
			return info;
		}
		#region ISort ��Ա

		public int SortID
		{
			get
			{
				// TODO:  ��� PactUnitInfo.SortID getter ʵ��
				return this.sortid ;
			}
			set
			{
				// TODO:  ��� PactUnitInfo.SortID setter ʵ��
				this.sortid = value;
			}
		}

		#endregion
	}
}
