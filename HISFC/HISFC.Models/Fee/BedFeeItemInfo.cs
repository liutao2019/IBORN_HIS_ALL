using System;
using Neusoft.NFC.Object;
using Neusoft.HISFC.Object.Base;

namespace Neusoft.HISFC.Object.Fee
{
	/// <summary>
	/// BedFeeItemInfo<br></br>
	/// [��������: ��λ������ ID:��Ŀ���� Name:��Ŀ����]<br></br>
	/// [�� �� ��: ����]<br></br>
	/// [����ʱ��: 2006-09-06]<br></br>
	/// <�޸ļ�¼
	///		�޸���=''
	///		�޸�ʱ��='yyyy-mm-dd'
	///		�޸�Ŀ��=''
	///		�޸�����=''
	///  />
	/// </summary>
	public class BedFeeItem	: NeuObject, ISort
	{
		#region ����
		
		/// <summary>
		/// ���õȼ�����
		/// </summary>
		private string feeGradeCode;
		
		/// <summary>
		/// ����
		/// </summary>
		private decimal qty;
		
		/// <summary>
		/// �Ʒѿ�ʼʱ��
		/// </summary>
		private DateTime beginTime;
		
		/// <summary>
		/// �Ʒѽ���ʱ��
		/// </summary>
		private DateTime endTime;
		
		/// <summary>
		/// �Ƿ���Ӥ���й�
		/// </summary>
		private bool isBabyRelation;
		
		/// <summary>
		/// �Ƿ���ʱ���й�
		/// </summary>
		private bool isTimeRelation;

		/// <summary>
		/// ��Ч�Ա�ʶ 0 ���� 1 ͣ�� 2 ����
		/// </summary>
		private string validState;
		
		/// <summary>
		/// ��չ���(����Ժ�����Ƿ�Ʒ�0���Ʒ�,1�Ʒ�---�������,�Ҵ�)
		/// </summary>
		private string extendFlag;
		
		/// <summary>
		/// �����
		/// </summary>
		private int sortID;

		#endregion

		#region ����
		
		/// <summary>
		/// ���õȼ�����
		/// </summary>
		public string FeeGradeCode
		{
			get
			{
				return this.feeGradeCode;	
			}
			set
			{
				this.feeGradeCode = value;
			}
		}
		
		/// <summary>
		/// ����
		/// </summary>
		public decimal Qty
		{
			get
			{
				return this.qty;
			}
			set
			{
				this.qty = value;
			}
		}
		
		/// <summary>
		/// �Ʒѿ�ʼʱ��
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
		/// �Ʒѽ���ʱ��
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
		
		/// <summary>
		/// �Ƿ���Ӥ���й�
		/// </summary>
		public bool IsBaybRelation
		{
			get
			{
				return this.isBabyRelation;
			}
			set
			{
				this.isBabyRelation = value;
			}
		}

		/// <summary>
		/// �Ƿ���ʱ���й�
		/// </summary>
		public bool IsTimeRelation
		{
			get
			{
				return this.isTimeRelation;
			}
			set
			{
				this.isTimeRelation = value;
			}
		}

		/// <summary>
		/// ��Ч�Ա�ʶ 0 ���� 1 ͣ�� 2 ����
		/// </summary>
		public string ValidState
		{
			get
			{
				return this.validState;
			}
			set
			{
				this.validState = value;
			}
		}

		/// <summary>
		/// ��չ���(����Ժ�����Ƿ�Ʒ�0���Ʒ�,1�Ʒ�---�������,�Ҵ�)
		/// </summary>
		public string ExtendFlag
		{
			get
			{
				return this.extendFlag;
			}
			set
			{
				this.extendFlag = value;
			}
		}

		#endregion

		#region ����
		/// <summary>
		/// ��Ŀ����
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ�û����ID", true)]
		public string ItemCode;

		/// <summary>
		/// ��Ŀ����
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ�û����Name", true)]
		public string ItemName;

		/// <summary>
		/// ����
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ��Qty����", true)]
		public decimal Number;

		/// <summary>
		/// �Ʒѿ�ʼʱ��
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ��BeginTime����", true)]
		public DateTime StartTime;

		/// <summary>
		/// ������ȡ�Ƿ��Ӥ���й�
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ��IsBabyRelation����", true)]
		public bool HasRelationToBaby;

		/// <summary>
		/// ������ȡ�Ƿ��ʱ���й�
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ��IsTimeRelation����", true)]
		public bool HasRelationToTime;

		/// <summary>
		/// ��չ���(����Ժ�����Ƿ�Ʒ�0���Ʒ�,1�Ʒ�---�������,�Ҵ�)
		/// </summary>
		[Obsolete("�Ѿ�����,ʹ��ExtendFlag����", true)]
		public string ExtFlag;	
	
		#endregion

		#region ����

		#region ��¡
		
		/// <summary>
		/// ��¡
		/// </summary>
		/// <returns>���ص�ǰ���ʵ������</returns>
		public new BedFeeItem Clone()
		{
			return base.Clone() as BedFeeItem;
		}
		
		#endregion

		#endregion

		#region �ӿ�ʵ��

		#region ISort ��Ա

		/// <summary>
		/// �����
		/// </summary>
		public int SortID
		{
			get
			{
				return this.sortID;
			}
			set
			{
				this.sortID = value;
			}
		}

		#endregion

		#endregion
	}
}
