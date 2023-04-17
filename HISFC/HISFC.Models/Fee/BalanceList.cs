using System;
using System.Collections;
using Neusoft.NFC.Object;
namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// BalanceList ��ժҪ˵����Write By ���峬
	/// ID  BalanceNo �������
	/// </summary>
	public class BalanceList:Neusoft.NFC.Object.NeuObject,
		Neusoft.HISFC.Object.Base.IBaby,
		Neusoft.HISFC.Object.Base.ISort		
	{
		public BalanceList()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		/// <summary>
		/// ������
		/// </summary>
		public Balance Balance = new Balance();
		/// <summary>
		/// ͳ�ƴ���
		/// </summary>
		public NeuObject StatClass = new NeuObject();
		/// <summary>
		/// ��ӡ˳��
		/// </summary>
		protected int SortId;

		/// <summary>
		/// Ӥ�����
		/// </summary>
		protected bool bBabyFlag;


		#region IBaby ��Ա

		public string BabyNo
		{
			get
			{
				// TODO:  ��� BalanceList.BabyNo getter ʵ��
				return "0";
			}
			set
			{
				// TODO:  ��� BalanceList.BabyNo setter ʵ��
			}
		}

		public bool IsBaby
		{
			get
			{
				// TODO:  ��� BalanceList.IsBaby getter ʵ��
				return this.bBabyFlag;
			}
			set
			{
				// TODO:  ��� BalanceList.IsBaby setter ʵ��
				this.bBabyFlag=value;
			}
		}

		#endregion

		#region ISort ��Ա

		public int SortID
		{
			get
			{
				// TODO:  ��� BalanceList.SortID getter ʵ��
				return this.SortId;
			}
			set
			{
				this.SortId = value;
				// TODO:  ��� BalanceList.SortID setter ʵ��
			}
		}

		#endregion
		public new BalanceList Clone()
		{
			BalanceList obj = base.Clone() as BalanceList;
			obj.Balance=this.Balance.Clone();
			obj.StatClass=this.StatClass.Clone();
			return obj;
		}
	}
}
