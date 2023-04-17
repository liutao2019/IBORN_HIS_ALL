using System;

namespace Neusoft.HISFC.Object.Base
{
	/// <summary>
	/// FTRate<br></br>
	/// [��������: ���ַ��ñ���ʵ��]<br></br>
	/// [�� �� ��: ��һ��]<br></br>
	/// [����ʱ��: 2006-08-30]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class FTRate : Neusoft.NFC.Object.NeuObject
	{
		public FTRate()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ����
		/// <summary>
		/// �Ƿ��Ѿ��ͷ���Դ
		/// </summary>
		private bool alreadyDisposed = false;
		/// <summary>
		/// �Էѱ���
		/// </summary>
		private decimal ownRate=1.0M;
		/// <summary>
		/// �Ը�����
		/// </summary>
		private decimal payRate=1.0M;
		/// <summary>
		/// ���ѱ���
		/// </summary>
		private decimal pubRate=0.0M;
		/// <summary>
		/// �������
		/// </summary>
		private decimal derateRate =0.0M;
		/// <summary>
		/// Ƿ�ѱ���
		/// </summary>
		private decimal arrearageRate =0.0M;
		/// <summary>
		/// �Żݱ���
		/// </summary>
		private decimal rebateRate = 0.0M;
		/// <summary>
		/// Ӥ������
		/// </summary>
		private bool  isBabyShared ;
		#endregion

		#region ����
		/// <summary>
		/// �Էѱ���
		/// </summary>
		public decimal OwnRate
		{
			get
			{
				return this.ownRate;
			}
			set
			{
				this.ownRate = value;
			}
		}
		/// <summary>
		/// �Ը�����
		/// </summary>
		public decimal PayRate
		{
			get
			{
				return this.payRate;
			}
			set
			{
				this.payRate = value;
			}
		}
		/// <summary>
		/// ���ѱ���
		/// </summary>
		public decimal PubRate
		{
			get
			{
				return this.pubRate;
			}
			set
			{
				this.pubRate = value;
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		public decimal DerateRate
		{
			get
			{
				return this.derateRate;
			}
			set
			{
				this.derateRate = value;
			}
		}
		/// <summary>
		/// Ƿ�ѱ���
		/// </summary>
		public decimal ArrearageRate
		{
			get
			{
				return this.arrearageRate;
			}
			set
			{
				this.arrearageRate = value;
			}
		}
		/// <summary>
		/// �Żݱ���
		/// </summary>
		public decimal RebateRate
		{
			get
			{
				return this.rebateRate;
			}
			set
			{
				this.rebateRate = value;
			}
		}
		/// <summary>
		/// �Ƿ�Ӥ������
		/// </summary>
		public bool IsBabyShared
		{
			get
			{
				return this.isBabyShared;
			}
			set
			{
				this.isBabyShared = value;
			}
		}
		#endregion

		#region ����
		/// <summary>
		/// �ͷ���Դ
		/// </summary>
		/// <param name="isDisposing"></param>
		protected override void Dispose(bool isDisposing)
		{
			if (this.alreadyDisposed)
			{
				return;
			}
			base.Dispose (isDisposing);
			this.alreadyDisposed = true;
		}

		#endregion

		#region ��¡
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns></returns>
		public new FTRate Clone()
		{
			FTRate ftRate = base.Clone() as FTRate;
			return ftRate;
		}
		#endregion
	}
}
